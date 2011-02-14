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

        ' Initialize ProgramConfig, Translation 
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
        If CommandLine.RunAs = CommandLine.RunMode.Scheduler AndAlso SchedulerAlreadyRunning() Then
            ConfigHandler.LogAppEvent("Scheduler already runnning from " & Application.ExecutablePath)
            Exit Sub
        End If

        ' Setup settings
        ReloadProfiles()
        ProgramConfig.LoadProgramSettings()
        If Not ProgramConfig.ProgramSettingsSet(ConfigOptions.AutoUpdates) Or Not ProgramConfig.ProgramSettingsSet(ConfigOptions.Language) Then
            HandleFirstRun()
        End If

        ' Initialize Main, Updates
        InitializeForms()

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
        If Not CommandLine.RunAs = CommandLine.RunMode.Scheduler Then ProgramConfig.SaveProgramSettings()

        'Calling ReleaseMutex would be the same, since Blocker necessary holds the mutex at this point (otherwise the app would have closed already).
        If CommandLine.RunAs = CommandLine.RunMode.Scheduler Then Blocker.Close()
        ConfigHandler.LogAppEvent("Program exited")

#If DEBUG And 0 Then
        VariousTests()
#End If
    End Sub

    Sub InitializeSharedObjects()
        ' Load program configuration
        ProgramConfig = ConfigHandler.GetSingleton
        Translation = LanguageHandler.GetSingleton

        ' Create required folders
        IO.Directory.CreateDirectory(ProgramConfig.LogRootDir)
        IO.Directory.CreateDirectory(ProgramConfig.ConfigRootDir)
        IO.Directory.CreateDirectory(ProgramConfig.LanguageRootDir)
    End Sub

    Sub InitializeForms()
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
            Dim AutoUpdates As Boolean = If(Interaction.ShowMsg(Translation.Translate("\WELCOME_MSG"), Translation.Translate("\FIRST_RUN"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes, True, False)
            ProgramConfig.SetProgramSetting(ConfigOptions.AutoUpdates, AutoUpdates.ToString)
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
        Dim MutexName As String = "[[Create Synchronicity scheduler]] " & Application.ExecutablePath.Replace(ConfigOptions.DirSep, "!"c).ToLower
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
                If CommandLine.RunAs = CommandLine.RunMode.Normal Then Diagnostics.Process.Start(Application.ExecutablePath, "/scheduler /noupdates" & If(CommandLine.Log, " /log", ""))
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
        MainFormInstance.ApplicationTimer.Interval = 2000 'Leave two seconds between each queued profile

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
            Dim NextInQueue As SchedulerEntry = ScheduledProfiles(0)
            Dim Status As String = String.Format(Translation.Translate("\SCH_WAITING"), NextInQueue.Name, If(NextInQueue.NextRun = ScheduleInfo.DATE_CATCHUP, "", NextInQueue.NextRun.ToString))
            Interaction.StatusIcon.Text = If(Status.Length >= 64, Status.Substring(0, 63), Status)

            If Date.Compare(NextInQueue.NextRun, Date.Now) <= 0 Then
                ConfigHandler.LogAppEvent("Scheduler: Launching " & NextInQueue.Name)

                Dim SyncForm As New SynchronizeForm(NextInQueue.Name, False, True, NextInQueue.Catchup)
                If SyncForm.StartSynchronization(False) Then
                    ConfigHandler.LogAppEvent("Scheduler: " & NextInQueue.Name & " exited successfully.")
                    ScheduledProfiles.Add(New SchedulerEntry(NextInQueue.Name, Profiles(NextInQueue.Name).Scheduler.NextRun(), False, False))
                Else
                    ConfigHandler.LogAppEvent("Scheduler: " & NextInQueue.Name & " reported an error, will run again in 4 hours.")
                    ScheduledProfiles.Add(New SchedulerEntry(NextInQueue.Name, Date.Now.AddHours(4), True, True))
                End If
                ScheduledProfiles.RemoveAt(0)
            End If
        End If
    End Sub

    Private Sub ReloadProfilesScheduler(ByVal ProfilesToRun As List(Of SchedulerEntry))
        ReloadProfiles() 'Needed! This allows to detect config changes.

        For Each Profile As KeyValuePair(Of String, ProfileHandler) In Profiles
            Dim Name As String = Profile.Key
            Dim Handler As ProfileHandler = Profile.Value
            Static OneDay As TimeSpan = New TimeSpan(1, 0, 0, 0)

            If Handler.Scheduler.Frequency <> ScheduleInfo.NEVER Then
                Dim NewEntry As New SchedulerEntry(Name, Handler.Scheduler.NextRun(), False, False)

                'Logic of this function:
                ' A new entry is created. The need for catching up is calculated regardless of the current state of the list.
                ' Then, a corresponding entry (same name) is searched for. If not found, then the new entry is simply added to the list.
                ' OOH, if a corresponding entry is found, then
                '    If it's already late, or if changes would postpone it, then nothing happens.
                '    But if it's not late, and the change will bring the sync forward, then the new entry superseedes the previous one.
                '       Note: In the latter case, if current entry is marked as failed, then the next run time is loaded from it
                '             (that's to avoid infinite loops when eg. the backup medium is unplugged)

                '<catchup>
                Dim LastRun As Date = Handler.GetLastRun()
                'LATER: Customizable time span?
                If Handler.GetSetting(ConfigOptions.CatchUpSync, False) And LastRun <> ScheduleInfo.DATE_NEVER And NewEntry.NextRun - LastRun > Handler.Scheduler.GetInterval() + OneDay Then
                    ConfigHandler.LogAppEvent("Scheduler: Profile " & Name & " was last executed on " & LastRun.ToString & ", marked for catching up.")
                    NewEntry.NextRun = ScheduleInfo.DATE_CATCHUP
                    NewEntry.CatchUp = True
                End If
                '</catchup>

                Dim ProfileIndex As Integer = ProfilesToRun.FindIndex(New Predicate(Of SchedulerEntry)(Function(Item As SchedulerEntry) Item.Name = Name))
                If ProfileIndex <> -1 Then
                    Dim CurEntry As SchedulerEntry = ProfilesToRun(ProfileIndex)

                    If NewEntry.NextRun <> CurEntry.NextRun And CurEntry.NextRun >= Date.Now() Then 'Don't postpone queued late backups
                        NewEntry.HasFailed = CurEntry.HasFailed
                        If CurEntry.HasFailed Then NewEntry.NextRun = CurEntry.NextRun

                        ProfilesToRun.RemoveAt(ProfileIndex)
                        ProfilesToRun.Add(NewEntry)
                        ConfigHandler.LogAppEvent("Scheduler: Re-registered profile for delayed run on " & NewEntry.NextRun.ToString & ": " & Name)
                    End If
                Else
                    ProfilesToRun.Add(NewEntry)
                    ConfigHandler.LogAppEvent("Scheduler: Registered profile for delayed run on " & NewEntry.NextRun.ToString & ": " & Name)
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

#If DEBUG Then
    Sub VariousTests()
        MessageBox.Show(Nothing = "")
        MessageBox.Show("" = Nothing)
        'MessageBox.Show(Nothing.ToString = "")
        'MessageBox.Show(Nothing.ToString = String.Empty)
        MessageBox.Show(CStr(Nothing) = "")
        MessageBox.Show(CStr(Nothing) = String.Empty)

        'MessageBox.Show(CBool(""))
        'If "" Then MessageBox.Show(""""" -> True")
        If Nothing Then MessageBox.Show("Nothing -> True")
        If Not Nothing Then MessageBox.Show("Nothing -> False")
        MessageBox.Show(CBool(Nothing))
    End Sub
#End If
End Module
