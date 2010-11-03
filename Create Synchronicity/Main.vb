Module MessageLoop
    Friend MainFormInstance As MainForm
    Friend Translation As LanguageHandler
    Friend ProgramConfig As ConfigHandler
    Friend Profiles As Dictionary(Of String, ProfileHandler)

    Private Blocker As Threading.Mutex = Nothing

    <STAThread()> _
    Sub Main()
        ' Must come first
        Application.EnableVisualStyles()

        ' Initialize ProgramConfig, Translation, Main, Updates 
        InitializeSharedObjects()

        'Read command line settings
        CommandLine.ReadArgs(New List(Of String)(Environment.GetCommandLineArgs()))

        ' Start logging
        ConfigHandler.LogAppEvent("Program started, configuration loaded")
        ConfigHandler.LogAppEvent(String.Format("Profiles will be loaded from {0}.", ProgramConfig.ConfigRootDir))
#If DEBUG Then
        Interaction.ShowMsg(Translation.Translate("\DEBUG_WARNING"), Translation.Translate("\DEBUG_MODE"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
#End If

        ' Check if multiple instances are allowed.
        If CommandLine.RunAs = CommandLine.RunMode.Scheduler Then
            If SchedulerAlreadyRunning() Then
                ConfigHandler.LogAppEvent("Scheduler already runnning from " & Application.ExecutablePath)
                Exit Sub
            End If
        End If

        ' Setup settings
        ReloadProfiles()
        ProgramConfig.LoadProgramSettings()
        If Not ProgramConfig.ProgramSettingsSet(ConfigOptions.AutoUpdates) Or Not ProgramConfig.ProgramSettingsSet(ConfigOptions.Language) Then
            HandleFirstRun()
        End If

        ' Look for updates
        If (Not CommandLine.NoUpdates) And ProgramConfig.GetProgramSetting(ConfigOptions.AutoUpdates, "False") Then
            Dim UpdateThread As New Threading.Thread(AddressOf Updates.CheckForUpdates)
            UpdateThread.Start(True)
        End If

        If CommandLine.Help Then
            Interaction.ShowMsg(String.Format("Create Synchronicity, version {1}.{0}{0}Profiles are loaded from ""{2}"".{0}{0}Available commands:{0}    /help,{0}    /scheduler,{0}    /log,{0}    [/preview] [/quiet|/silent] /run ""ProfileName1|ProfileName2|ProfileName3[|...]""{0}{0}License information: See ""Release notes.txt"".{0}{0}Help: See http://synchronicity.sourceforge.net/help.html.{0}{0}You can help this software! See http://synchronicity.sourceforge.net/contribute.html.{0}{0}Happy syncing!", Environment.NewLine, Application.ProductVersion, ProgramConfig.ConfigRootDir), "Help!")
        Else
            If CommandLine.RunAs = CommandLine.RunMode.Queue Or CommandLine.RunAs = CommandLine.RunMode.Scheduler Then
                Interaction.ShowStatusIcon()
                MainFormInstance.ApplicationTimer.Start()
                Application.Run()
                Interaction.HideStatusIcon()
            Else
                Application.Run(MainFormInstance)
            End If
        End If

        'Calling ReleaseMutex would be the same, since Blocker necessary holds the mutex at this point (otherwise the app would have closed already).
        If CommandLine.RunAs = CommandLine.RunMode.Scheduler Then Blocker.Close()
        ConfigHandler.LogAppEvent("Program exited")
    End Sub

    Sub InitializeSharedObjects()
        ' Load program configuration
        ProgramConfig = ConfigHandler.GetSingleton
        Translation = LanguageHandler.GetSingleton

        ' Create required folders
        IO.Directory.CreateDirectory(ProgramConfig.LogRootDir)
        IO.Directory.CreateDirectory(ProgramConfig.ConfigRootDir)
        IO.Directory.CreateDirectory(ProgramConfig.LanguageRootDir)

        ' Create MainForm
        MainFormInstance = New MainForm()
        AddHandler MainFormInstance.ApplicationTimer.Tick, AddressOf ApplicationTimer_Tick

        'Load status icon
        Interaction.LoadStatusIcon()
        Interaction.StatusIcon.ContextMenuStrip = MainFormInstance.StatusIconMenu

        'Give updates a way to notify exit
        Updates.SetParent(MainFormInstance) 'TODO: CHeck if a callback is really needed
    End Sub

    Sub HandleFirstRun()
        If Not ProgramConfig.ProgramSettingsSet(ConfigOptions.Language) Then
            Application.Run(New LanguageForm)
            Translation = LanguageHandler.GetSingleton(True)
        End If

        If Not ProgramConfig.ProgramSettingsSet(ConfigOptions.AutoUpdates) Then
            If Interaction.ShowMsg(Translation.Translate("\WELCOME_MSG"), Translation.Translate("\FIRST_RUN"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                ProgramConfig.SetProgramSetting(ConfigOptions.AutoUpdates, "True")
            Else
                ProgramConfig.SetProgramSetting(ConfigOptions.AutoUpdates, "False")
            End If
        End If

        ProgramConfig.SaveProgramSettings()
    End Sub

    Sub ReloadProfiles()
        Profiles = New Dictionary(Of String, ProfileHandler)

        For Each ConfigFile As String In IO.Directory.GetFiles(ProgramConfig.ConfigRootDir, "*.sync")
            Dim Name As String = IO.Path.GetFileNameWithoutExtension(ConfigFile)
            Profiles.Add(Name, New ProfileHandler(Name))
        Next
    End Sub

#Region "Scheduling"
    Function SchedulerAlreadyRunning() As Boolean
        Dim MutexName As String = "[[Create Synchronicity scheduler]] " & Application.ExecutablePath.Replace("\"c, "!"c).ToLower
#If DEBUG Then
        ConfigHandler.LogAppEvent(String.Format("Trying to register mutex ""{0}""", MutexName))
#End If

        Try
            Blocker = New Threading.Mutex(False, MutexName)
        Catch Ex As Threading.AbandonedMutexException
#If DEBUG Then
            ConfigHandler.LogAppEvent("Abandoned mutex detected")
#End If
            Return False
        End Try

        Return (Not Blocker.WaitOne(0, False))
    End Function

    Sub RedoSchedulerRegistration()
        Dim NeedToRunAtBootTime As Boolean = False
        For Each Profile As ProfileHandler In Profiles.Values
            NeedToRunAtBootTime = NeedToRunAtBootTime Or (Profile.Scheduler.Frequency <> ScheduleInfo.NEVER)
            If Profile.Scheduler.Frequency <> ScheduleInfo.NEVER Then ConfigHandler.LogAppEvent(String.Format("Profile {0} requires the scheduler to run.", Profile.ProfileName))
        Next

        Try
            If NeedToRunAtBootTime Then
                ConfigHandler.RegisterBoot()
                ConfigHandler.LogAppEvent("Registered program in startup list, trying to start scheduler")
                If CommandLine.RunAs = CommandLine.RunMode.Normal Then Diagnostics.Process.Start(Application.ExecutablePath, "/scheduler /noupdates")
            Else
                If My.Computer.Registry.GetValue(ConfigOptions.RegistryRootedBootKey, ConfigOptions.RegistryBootVal, Nothing) IsNot Nothing Then
                    ConfigHandler.LogAppEvent("Unregistering program from startup list")
                    My.Computer.Registry.CurrentUser.OpenSubKey(ConfigOptions.RegistryBootKey, True).DeleteValue(ConfigOptions.RegistryBootVal)
                End If
            End If
        Catch Ex As Exception
            Interaction.ShowMsg(Translation.Translate("\UNREG_ERROR"), Translation.Translate("\ERROR"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ApplicationTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Static ProfilesToRun As List(Of KeyValuePair(Of Date, String))

        If ProfilesToRun Is Nothing Then
            ProgramConfig.CanGoOn = False 'Stop tick events from happening
            ConfigHandler.LogAppEvent("Worker thread started")
            MainFormInstance.ApplicationTimer.Interval = 20000 'First tick was forced by the very low ticking interval.

            ProfilesToRun = New List(Of KeyValuePair(Of Date, String))

            If CommandLine.RunAs = CommandLine.RunMode.Scheduler Then
                ConfigHandler.LogAppEvent("Worker thread: Running as scheduler")
                ReloadProfilesScheduler(ProfilesToRun)
            Else '(CommandLine.RunAs = CommandLine.RunMode.Queue): A list of profiles has been provided.
                ConfigHandler.LogAppEvent("Worker thread: Running as batch sync engine")
                For Each Profile As String In CommandLine.TasksToRun.Split(ConfigOptions.EnqueuingSeparator)
                    If Profiles.ContainsKey(Profile) Then
                        If Profiles(Profile).ValidateConfigFile() Then
                            ConfigHandler.LogAppEvent("Worker thread: Registered profile for immediate run: " & Profile)
                            ProfilesToRun.Add(New KeyValuePair(Of Date, String)(Date.Now.AddDays(-1), Profile)) 'Make sure it runs immediately
                        Else
                            Interaction.ShowMsg(Translation.Translate("\INVALID_CONFIG"), Translation.Translate("\INVALID_CMD"), , MessageBoxIcon.Error)
                        End If
                    Else
                        Interaction.ShowMsg(Translation.Translate("\INVALID_PROFILE"), Translation.Translate("\INVALID_CMD"), , MessageBoxIcon.Error)
                    End If
                Next
            End If

            ProgramConfig.CanGoOn = True
        End If

        If ProgramConfig.CanGoOn = False Then Exit Sub 'Don't start next sync yet.
        If ProfilesToRun.Count = 0 Then
            Interaction.HideStatusIcon()
            ConfigHandler.LogAppEvent("Worker thread: Synced all profiles.")
            Application.Exit()
            Exit Sub
        End If

        If CommandLine.RunAs = CommandLine.RunMode.Scheduler Then ReloadProfilesScheduler(ProfilesToRun)

        Dim NextRun As Date = ProfilesToRun(0).Key
        Dim Status As String = String.Format(Translation.Translate("\SCH_WAITING"), ProfilesToRun(0).Value, If(NextRun = ScheduleInfo.DATE_CATCHUP, "", NextRun.ToString))
        Interaction.StatusIcon.Text = If(Status.Length >= 64, Status.Substring(0, 63), Status)

        If Date.Compare(ProfilesToRun(0).Key, Date.Now) <= 0 Then
            Dim NextInQueue As New KeyValuePair(Of Date, String)(ProfilesToRun(0).Key, ProfilesToRun(0).Value) 'Copy. TODO: Could be removed. -> Why?
            ConfigHandler.LogAppEvent("Worker thread: Launching " & NextInQueue.Value)
            ProfilesToRun.RemoveAt(0)

            If CommandLine.RunAs = CommandLine.RunMode.Scheduler Then
                Dim SyncForm As New SynchronizeForm(NextInQueue.Value, False, False, True)
                ProfilesToRun.Add(New KeyValuePair(Of Date, String)(Profiles(NextInQueue.Value).Scheduler.NextRun(), NextInQueue.Value))
            Else
                Dim SyncForm As New SynchronizeForm(NextInQueue.Value, CommandLine.ShowPreview, False, CommandLine.Quiet)
            End If
        End If
    End Sub

    Private Sub ReloadProfilesScheduler(ByVal ProfilesToRun As List(Of KeyValuePair(Of Date, String)))
        ' Note: (TODO?)
        ' This sub will update profiles scheduling settings
        ' However, for the sake of simplicity, a change that would postpone a backup will not be detected.
        ' This is a limitation due to the fact that we allow for catching up missed syncs.
        ' It is therefore impossible - in the current state of things - to say if the backup was postponed:
        '   1. due to its being rescheduled
        '   2. due to its having been previously marked as needing to be caught up. 'TODO: Catching up is currently disabled (4.3)
        ' It would be possible though to force updates of scheduling settings for profiles which are not 'catching-up' enabled.
        ' Yet this would rather introduce a lack of coherence, unsuitable above all.

        ReloadProfiles() 'Needed! This allows to detect config changes.
        For Each Profile As KeyValuePair(Of String, ProfileHandler) In Profiles
            If Profile.Value.Scheduler.Frequency <> ScheduleInfo.NEVER Then
                Dim DateOfNextRun As Date = Profile.Value.Scheduler.NextRun()
                'TODO: Catching up is currently disabled (5.0)
                'Catchup problem: any newly scheduled profile is immediately caught up.
                'Test catchup, and show a ballon to say which profiles will be catched up.
                '<catchup>
                'If Profile.Value.GetSetting(ConfigOptions.CatchUpSync, True) And DateOfNextRun - Profile.Value.GetLastRun() > Profile.Value.Scheduler.GetInterval(2) Then
                '    DateOfNextRun = ScheduleInfo.DATE_CATCHUP
                'End If
                '</catchup>

                Dim ProfileName As String = Profile.Value.ProfileName
                Dim ProfileIndex As Integer = ProfilesToRun.FindIndex(New Predicate(Of KeyValuePair(Of Date, String))(Function(Item As KeyValuePair(Of Date, String)) Item.Value = ProfileName))
                If ProfileIndex <> -1 Then
                    If DateOfNextRun < ProfilesToRun(ProfileIndex).Key Then 'The schedules may be brought forward, never postponed.
                        ProfilesToRun.RemoveAt(ProfileIndex)
                        ProfilesToRun.Add(New KeyValuePair(Of Date, String)(DateOfNextRun, Profile.Key))
                        ConfigHandler.LogAppEvent("Worker thread: Re-registered profile for delayed run on " & DateOfNextRun.ToString & ": " & Profile.Key)
                    End If
                Else
                    ProfilesToRun.Add(New KeyValuePair(Of Date, String)(DateOfNextRun, Profile.Key))
                    ConfigHandler.LogAppEvent("Worker thread: Registered profile for delayed run on " & DateOfNextRun.ToString & ": " & Profile.Key)
                End If
            End If
        Next

        'Remove deleted or disabled profiles
        For ProfileIndex As Integer = ProfilesToRun.Count - 1 To 0 Step -1
            If Not Profiles.ContainsKey(ProfilesToRun(ProfileIndex).Value) OrElse Profiles(ProfilesToRun(ProfileIndex).Value).Scheduler.Frequency = ScheduleInfo.NEVER Then
                ProfilesToRun.RemoveAt(ProfileIndex)
            End If
        Next

        'Tracker #3000728
        ProfilesToRun.Sort(Function(First As KeyValuePair(Of Date, String), Second As KeyValuePair(Of Date, String)) First.Key.CompareTo(Second.Key))
    End Sub
#End Region
End Module
