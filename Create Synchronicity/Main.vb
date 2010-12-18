Module MessageLoop
    Friend ReloadNeeded As Boolean
    Friend MainFormInstance As MainForm
    Friend Translation As LanguageHandler
    Friend ProgramConfig As ConfigHandler
    Friend Profiles As Dictionary(Of String, ProfileHandler)

    Private Blocker As Threading.Mutex = Nothing

#Region "Main program loop & first run"
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

                If CommandLine.RunAs = CommandLine.RunMode.Queue Then
                    AddHandler MainFormInstance.ApplicationTimer.Tick, AddressOf ProcessProfilesQueue
                ElseIf CommandLine.RunAs = CommandLine.RunMode.Scheduler Then
                    AddHandler MainFormInstance.ApplicationTimer.Tick, AddressOf Scheduling_Tick
                End If

                MainFormInstance.ApplicationTimer.Start()
                Application.Run() 'This call won't exit until Application.Exit is called from elsewhere.
                Interaction.HideStatusIcon()
            Else
                Do
                    If ReloadNeeded Then MainFormInstance = New MainForm
                    Application.Run(MainFormInstance)
                Loop While ReloadNeeded
            End If
        End If

        ' Save last window information.
        ProgramConfig.SaveProgramSettings()

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

        'Load status icon
        Interaction.LoadStatusIcon()
        Interaction.StatusIcon.ContextMenuStrip = MainFormInstance.StatusIconMenu

        'Give updates a way to notify exit
        Updates.SetParent(MainFormInstance) 'Using a callback is really needed.
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
#End Region

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

    Sub ProcessProfilesQueue(ByVal sender As System.Object, ByVal e As System.EventArgs)
        MainFormInstance.ApplicationTimer.Stop()
        MainFormInstance.ApplicationTimer.Interval = 2000

        Static ProfilesQueue As List(Of String) = Nothing

        If ProfilesQueue Is Nothing Then
            ProfilesQueue = New List(Of String)

            ConfigHandler.LogAppEvent("Profiles queue: Queue created.")
            For Each Profile As String In CommandLine.TasksToRun.Split(ConfigOptions.EnqueuingSeparator)
                If Profiles.ContainsKey(Profile) Then
                    If Profiles(Profile).ValidateConfigFile() Then
                        ConfigHandler.LogAppEvent("Profiles queue: Registered profile " & Profile)
                        ProfilesQueue.Add(Profile)
                    Else
                        Interaction.ShowMsg(Translation.Translate("\INVALID_CONFIG"), Translation.Translate("\INVALID_CMD"), , MessageBoxIcon.Error)
                    End If
                Else
                    Interaction.ShowMsg(Translation.Translate("\INVALID_PROFILE"), Translation.Translate("\INVALID_CMD"), , MessageBoxIcon.Error)
                End If
            Next
        End If

        If ProfilesQueue.Count = 0 Then
            ConfigHandler.LogAppEvent("Profiles queue: Synced all profiles.")
            Application.Exit()
        Else
            Dim SyncForm As New SynchronizeForm(ProfilesQueue(0), CommandLine.ShowPreview, CommandLine.Quiet)
            AddHandler SyncForm.OnFormClosedAfterSyncFinished, Sub() MainFormInstance.ApplicationTimer.Start()
            SyncForm.StartSynchronization(False)
            ProfilesQueue.RemoveAt(0)
        End If
    End Sub

    Private Sub Scheduling_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Static ScheduledProfiles As List(Of SchedulerEntry) = Nothing

        If ScheduledProfiles Is Nothing Then
            ProgramConfig.CanGoOn = False 'Stop tick events from happening
            MainFormInstance.ApplicationTimer.Interval = 15000 'First tick was forced by the very low ticking interval.
            ScheduledProfiles = New List(Of SchedulerEntry)

            ConfigHandler.LogAppEvent("Scheduler: Started application timer.")
            ReloadProfilesScheduler(ScheduledProfiles)

            ProgramConfig.CanGoOn = True
        End If

        If ProgramConfig.CanGoOn = False Then Exit Sub 'Don't start next sync yet.

        ReloadProfilesScheduler(ScheduledProfiles)
        If ScheduledProfiles.Count = 0 Then
            ConfigHandler.LogAppEvent("Scheduler: No profiles left to run, exiting.")
            Application.Exit()
            Exit Sub
        Else
            Dim NextRun As Date = ScheduledProfiles(0).NextRun
            Dim NextInQueue As SchedulerEntry = ScheduledProfiles(0)
            Dim Status As String = String.Format(Translation.Translate("\SCH_WAITING"), NextInQueue.Name, If(NextRun = ScheduleInfo.DATE_CATCHUP, "", NextRun.ToString))
            Interaction.StatusIcon.Text = If(Status.Length >= 64, Status.Substring(0, 63), Status)

            If Date.Compare(NextInQueue.NextRun, Date.Now) <= 0 Then
                ConfigHandler.LogAppEvent("Scheduler: Launching " & NextInQueue.Name)

                Dim SyncForm As New SynchronizeForm(NextInQueue.Name, False, True, NextInQueue.NextRun = ScheduleInfo.DATE_CATCHUP)
                SyncForm.StartSynchronization(False)
                ScheduledProfiles.Add(New SchedulerEntry(NextInQueue.Name, Profiles(NextInQueue.Name).Scheduler.NextRun()))

                ScheduledProfiles.RemoveAt(0)
            End If
        End If
    End Sub

    Private Sub ReloadProfilesScheduler(ByVal ProfilesToRun As List(Of SchedulerEntry))
        ReloadProfiles() 'Needed! This allows to detect config changes.

        For Each Profile As KeyValuePair(Of String, ProfileHandler) In Profiles
            Dim Name As String = Profile.Key 'TODO: Handler.ProfileName was used in the predicate find function. Should be the same as Profile.Key
            Dim Handler As ProfileHandler = Profile.Value
            If Handler.Scheduler.Frequency <> ScheduleInfo.NEVER Then
                Dim NextRun As Date = Handler.Scheduler.NextRun()
                'TODO: Test catchup
                'TODO: A message should appear when syncing starts.
                '<catchup>
                Dim LastRun As Date = Handler.GetLastRun()
                'TODO: Choose default value for catchup.
                If Handler.GetSetting(ConfigOptions.CatchUpSync, False) And LastRun <> ScheduleInfo.DATE_NEVER And NextRun - LastRun > Handler.Scheduler.GetInterval(2) Then
                    NextRun = ScheduleInfo.DATE_CATCHUP
                End If
                '</catchup>

                Dim ProfileIndex As Integer = ProfilesToRun.FindIndex(New Predicate(Of SchedulerEntry)(Function(Item As SchedulerEntry) Item.Name = Name))
                If ProfileIndex <> -1 Then
                    If NextRun <> ProfilesToRun(ProfileIndex).NextRun And ProfilesToRun(ProfileIndex).NextRun >= Date.Now() Then
                        'Don't postpone already late backups
                        ProfilesToRun.RemoveAt(ProfileIndex)
                        ProfilesToRun.Add(New SchedulerEntry(Name, NextRun))
                        ConfigHandler.LogAppEvent("Scheduler: Re-registered profile for delayed run on " & NextRun.ToString & ": " & Name)
                    End If
                Else
                    ProfilesToRun.Add(New SchedulerEntry(Name, NextRun))
                    ConfigHandler.LogAppEvent("Scheduler: Registered profile for delayed run on " & NextRun.ToString & ": " & Name)
                End If
            End If
        Next

        'Remove deleted or disabled profiles
        For ProfileIndex As Integer = ProfilesToRun.Count - 1 To 0 Step -1
            If Not Profiles.ContainsKey(ProfilesToRun(ProfileIndex).Name) OrElse Profiles(ProfilesToRun(ProfileIndex).Name).Scheduler.Frequency = ScheduleInfo.NEVER Then
                ProfilesToRun.RemoveAt(ProfileIndex)
            End If
        Next

        'Tracker #3000728
        ProfilesToRun.Sort(Function(First As SchedulerEntry, Second As SchedulerEntry) First.NextRun.CompareTo(Second.NextRun))
    End Sub
#End Region
End Module
