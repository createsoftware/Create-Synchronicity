'This file is part of Create Synchronicity.
'
'Create Synchronicity is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
'Create Synchronicity is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with Create Synchronicity.  If not, see <http://www.gnu.org/licenses/>.
'Created by:	Clément Pit--Claudel.
'Web site:		http://synchronicity.sourceforge.net.

Public Class MainForm
    Private Profiles As Dictionary(Of String, ProfileHandler)

    Dim Translation As LanguageHandler = LanguageHandler.GetSingleton
    Dim ProgramConfig As ConfigHandler = ConfigHandler.GetSingleton

#Region " Events "
    Private Sub MainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = ProgramConfig.GetIcon()
        ConfigHandler.LogAppEvent("Program started")
        ConfigHandler.LogAppEvent(String.Format("Profiles will be loaded from {0}.", ProgramConfig.ConfigRootDir))
#If DEBUG Then
        Interaction.ShowMsg(Translation.Translate("\DEBUG_WARNING"), Translation.Translate("\DEBUG_MODE"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
#End If

        ''''''''''''''''''''''''''''''''''''''
        ' First-run checks & initialisations '
        ''''''''''''''''''''''''''''''''''''''
        IO.Directory.CreateDirectory(ProgramConfig.LogRootDir)
        IO.Directory.CreateDirectory(ProgramConfig.ConfigRootDir)
        IO.Directory.CreateDirectory(ProgramConfig.LanguageRootDir)

        Interaction.LoadStatusIcon()
        ProgramConfig.LoadProgramSettings()
        Updates.SetParent(Me)
        If Not ProgramConfig.ProgramSettingsSet(ConfigOptions.AutoUpdates) Or Not ProgramConfig.ProgramSettingsSet(ConfigOptions.Language) Then
            If Not ProgramConfig.ProgramSettingsSet(ConfigOptions.Language) Then
                Dim LngForm As New LanguageForm
                LngForm.ShowDialog()
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
        End If

        ''''''''''''''''''''''''''''''''
        ' Read command line parameters '
        ''''''''''''''''''''''''''''''''
        Dim ArgsList As New List(Of String)(Environment.GetCommandLineArgs())

        If ArgsList.Count > 1 Then
            CommandLine.Help = ArgsList.Contains("/help")
            CommandLine.Quiet = ArgsList.Contains("/quiet")
            CommandLine.ShowPreview = ArgsList.Contains("/preview")
            CommandLine.Silent = ArgsList.Contains("/silent")
            CommandLine.Log = ArgsList.Contains("/log")

            CommandLine.Quiet = CommandLine.Quiet Or CommandLine.Silent

            Dim RunArgIndex As Integer = ArgsList.IndexOf("/run")
            If RunArgIndex <> -1 AndAlso RunArgIndex + 1 < ArgsList.Count Then
                CommandLine.TasksToRun = ArgsList(RunArgIndex + 1)
            End If
        End If

        ''''''''''''''''''''''''''
        ' Display help if needed '
        ''''''''''''''''''''''''''
        If CommandLine.Help Then
            Interaction.ShowMsg(String.Format("Create Synchronicity, version {1}.{0}Profiles are loaded from ""{2}"".{0}License information: See ""Release notes.txt"".{0}Help: See http://synchronicity.sourceforge.net/help.html.{0}You can help this software! See http://synchronicity.sourceforge.net/contribute.html.{0}Happy syncing!", Environment.NewLine & Environment.NewLine, Application.ProductVersion, ProgramConfig.ConfigRootDir), "Help!")
            Application.Exit()
            Exit Sub
        End If

        ''''''''''''''''''''
        ' Look for updates '
        ''''''''''''''''''''
        If ProgramConfig.GetProgramSetting(ConfigOptions.AutoUpdates, "False") Then
            Dim UpdateThread As New Threading.Thread(AddressOf Updates.CheckForUpdates)
            UpdateThread.Start(True)
        End If

        '''''''''''''''''''''
        ' Load translations '
        '''''''''''''''''''''
        Translation.TranslateControl(Me)
        Translation.TranslateControl(Me.Main_ActionsMenu)
        Translation.TranslateControl(Me.StatusIconMenu)

        'Position the "About" label correctly
        Dim PreviousWidth As Integer = Main_AboutLinkLabel.Width
        Main_AboutLinkLabel.AutoSize = True
        Main_AboutLinkLabel.Location += New Drawing.Point(PreviousWidth - Main_AboutLinkLabel.Width, 0)

        '''''''''''''''''''''''''''''''''''
        ' Load profiles and start working '
        '''''''''''''''''''''''''''''''''''
        Main_ReloadConfigs()
        Main_TryUnregStartAtBoot()

        If CommandLine.TasksToRun <> "" Then
            CommandLine.RunAs = CommandLine.RunMode.Queue 'Mostly explicative

        ElseIf ArgsList.Contains("/scheduler") Then
            CommandLine.RunAs = CommandLine.RunMode.Scheduler
#If 0 Then
            'TODO: Register a mutex to prevent multi-instances scheduler. Decide if this should also run in RELEASE mode.
            'This code is not functional
            Dim Blocker As New Threading.Mutex(False, Application.ExecutablePath.Replace("\"c, "_"c))
            If Not Blocker.WaitOne(0, False) Then
                ConfigHandler.LogAppEvent("Scheduler already runnning from " & Application.ExecutablePath)
                Application.Exit()
                Exit Sub
            End If
#End If
            Interaction.StatusIcon.ContextMenuStrip = StatusIconMenu
            Interaction.ShowStatusIcon()
        End If

        If CommandLine.RunAs = CommandLine.RunMode.Queue Or CommandLine.RunAs = CommandLine.RunMode.Scheduler Then
            Main_ConcealForm()
            ApplicationTimer.Start()
        End If
    End Sub

    Private Sub MainForm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Interaction.HideStatusIcon()
        ConfigHandler.LogAppEvent("Program exited")
    End Sub

    Private Sub MainForm_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        'Requires PreviewKeys to be set to true to work, otherwise the form won't catch the keypress.
        If e.KeyCode = Keys.F1 Then
            Diagnostics.Process.Start("http://synchronicity.sourceforge.net/help.html")
        ElseIf e.Control Then
            Select Case e.KeyCode
                Case Keys.N
                    Main_Actions.LabelEdit = True
                    Main_Actions.Items(0).BeginEdit()
                Case Keys.O
                    Diagnostics.Process.Start(ProgramConfig.ConfigRootDir)
            End Select
        End If
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
        Application.Exit()
    End Sub

    Private Sub Main_Actions_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Main_Actions.KeyDown
        If Main_Actions.SelectedItems.Count = 0 Then Exit Sub
        If e.KeyCode = Keys.Enter Then
            If Main_Actions.SelectedIndices(0) = 0 Then
                Main_Actions.LabelEdit = True
                Main_Actions.SelectedItems(0).BeginEdit()
            Else
                Main_ActionsMenu.Show(Main_Actions, New Drawing.Point(Main_Actions.SelectedItems(0).Bounds.Location.X, Main_Actions.SelectedItems(0).Bounds.Location.Y + Main_Actions.SelectedItems(0).Bounds.Height))
            End If
        ElseIf e.KeyCode = Keys.F2 And Not Main_Actions.SelectedIndices(0) = 0 Then
            Main_Actions.LabelEdit = True
            Main_Actions.SelectedItems(0).BeginEdit()
        End If
    End Sub

    Private Sub Main_Actions_Click(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Main_Actions.MouseClick
        If Main_Actions.SelectedItems.Count = 0 Then Exit Sub

        If Main_Actions.SelectedIndices(0) = 0 Then
            If e.Button = Windows.Forms.MouseButtons.Right Then Exit Sub
            Main_Actions.LabelEdit = True
            Main_Actions.SelectedItems(0).BeginEdit()
        Else
            Main_ActionsMenu.Show(Main_Actions, e.Location)
        End If
    End Sub

    Private Sub Main_Actions_AfterLabelEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LabelEditEventArgs) Handles Main_Actions.AfterLabelEdit
        Main_Actions.LabelEdit = False
        If e.Label = "" OrElse e.Label.IndexOfAny(IO.Path.GetInvalidFileNameChars) >= 0 Then
            e.CancelEdit = True
            Exit Sub
        End If

        If e.Item = 0 Then
            e.CancelEdit = True
            Dim SettingsForm As New SettingsForm(e.Label)
            SettingsForm.ShowDialog()
        Else
            If Not Profiles(Main_Actions.Items(e.Item).Text).RenameProfile(e.Label) Then e.CancelEdit = True
        End If
        Main_ReloadConfigs()
    End Sub

    Private Sub Main_Actions_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Main_Actions.SelectedIndexChanged
        If Main_Actions.SelectedIndices.Count = 0 OrElse Main_Actions.SelectedIndices(0) = 0 Then
            If Main_Actions.SelectedIndices.Count = 0 Then
                Main_Display_Options("", True)
            ElseIf Main_Actions.SelectedIndices(0) = 0 Then
                Main_Display_Options(Translation.Translate("\NEW_PROFILE"), True)
            End If

            Main_ActionsMenu.Close()
            Exit Sub
        End If

        Main_Display_Options(CurrentProfile, False)
    End Sub

    Private Sub Main_AboutLinkLabel_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles Main_AboutLinkLabel.LinkClicked
        Dim About As New AboutForm
        About.ShowDialog()
    End Sub

    Private Sub Main_ActionsMenu_Opening(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Main_ActionsMenu.Opening
        Dim FileSize As Integer = If(IO.File.Exists(ProgramConfig.GetLogPath(CurrentProfile)), CInt((New System.IO.FileInfo(ProgramConfig.GetLogPath(CurrentProfile))).Length / 1000), 0)
        ClearLogMenuItem.Text = String.Format(ClearLogMenuItem.Tag, FileSize)
    End Sub

    Private Sub PreviewMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PreviewMenuItem.Click
        If Not CheckValidity() Then Exit Sub

        Dim SyncForm As New SynchronizeForm(CurrentProfile, True, True, False)
        Main_SetVisible(False) : SyncForm.ShowDialog() : Main_SetVisible(True)
        SyncForm.Dispose()
    End Sub

    Private Sub SynchronizeMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SynchronizeMenuItem.Click
        If Not CheckValidity() Then Exit Sub

        Dim SyncForm As New SynchronizeForm(CurrentProfile, False, True, False)
        Main_SetVisible(False) : SyncForm.ShowDialog() : Main_SetVisible(True)
        SyncForm.Dispose()
    End Sub

    Private Sub ChangeSettingsMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangeSettingsMenuItem.Click
        Dim SettingsForm As New SettingsForm(CurrentProfile)
        Main_SetVisible(False) : SettingsForm.ShowDialog() : Main_SetVisible(True)
        Main_ReloadConfigs()
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If Interaction.ShowMsg(String.Format(Translation.Translate("\DELETE_PROFILE"), CurrentProfile), Translation.Translate("\CONFIRM_DELETION"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Profiles(CurrentProfile).DeleteConfigFile()
            Profiles(CurrentProfile) = Nothing
            Main_Actions.Items.RemoveAt(Main_Actions.SelectedIndices(0))
        End If
    End Sub

    Private Sub RenameMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RenameMenuItem.Click
        Main_Actions.LabelEdit = True
        Main_Actions.SelectedItems(0).BeginEdit()
    End Sub

    Private Sub ViewLogMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewLogMenuItem.Click
        If Not IO.File.Exists(ProgramConfig.GetLogPath(CurrentProfile)) Then Exit Sub
        Diagnostics.Process.Start(ProgramConfig.GetLogPath(CurrentProfile))
    End Sub

    Private Sub ClearLogMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearLogMenuItem.Click
        Profiles(CurrentProfile).DeleteLogFile()
    End Sub

    Private Sub Main_ScheduleMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ScheduleMenuItem.Click
        Dim SchedForm As New SchedulingForm(CurrentProfile)
        SchedForm.ShowDialog()
        Main_ReloadConfigs()
        Main_TryUnregStartAtBoot()
    End Sub

    Private Sub ApplicationTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ApplicationTimer.Tick
        Static ProfilesToRun As List(Of KeyValuePair(Of Date, String))

        If ProfilesToRun Is Nothing Then
            ProgramConfig.CanGoOn = False 'Stop tick events from happening
            ConfigHandler.LogAppEvent("Worker thread started")
            ApplicationTimer.Interval = 20000 'First tick was forced by the very low ticking interval.

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

        Main_ReloadConfigs() 'Needed! This allows to detect config changes.
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
        'Check this comparison function (ordering and first item) -- Done ; it's cool.
        ProfilesToRun.Sort(Function(First As KeyValuePair(Of Date, String), Second As KeyValuePair(Of Date, String)) First.Key.CompareTo(Second.Key))
    End Sub

    Private Sub Main_Donate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Main_Donate.Click
        Diagnostics.Process.Start("http://synchronicity.sourceforge.net/contribute.html")
    End Sub

    Private Sub Main_Donate_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Main_Donate.MouseEnter
        Main_Donate.BackColor = Drawing.Color.LightGray
    End Sub

    Private Sub Main_Donate_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Main_Donate.MouseLeave
        Main_Donate.BackColor = Drawing.Color.White
    End Sub
#End Region

#Region " Functions and Routines "
    Sub Main_ConcealForm()
        Me.Opacity = 0
        Me.WindowState = FormWindowState.Minimized
        Me.ShowInTaskbar = False
    End Sub

    Sub Main_SetVisible(ByVal _Visible As Boolean)
        If Me.IsDisposed Then Exit Sub
        Me.Visible = _Visible
    End Sub

    Sub Main_ReloadConfigs()
        If Me.IsDisposed Then Exit Sub

        Profiles = New Dictionary(Of String, ProfileHandler)
        Dim CreateProfileItem As ListViewItem = Main_Actions.Items(0)

        Main_Actions.Items.Clear()
        Main_Actions.Items.Add(CreateProfileItem).Group = Main_Actions.Groups(0)

        Dim Groups As New List(Of String)

        For Each ConfigFile As String In IO.Directory.GetFiles(ProgramConfig.ConfigRootDir, "*.sync")
            Dim Name As String = ConfigFile.Substring(ConfigFile.LastIndexOf(ConfigOptions.DirSep) + 1)
            Name = Name.Substring(0, Name.LastIndexOf("."))

            Profiles.Add(Name, New ProfileHandler(Name))

            Dim NewItem As ListViewItem = Main_Actions.Items.Add(Name)
            NewItem.Group = Main_Actions.Groups(1)
            NewItem.ImageIndex = CInt(Profiles(Name).GetSetting(ConfigOptions.Method))
            NewItem.SubItems.Add(GetMethodName(Name)).ForeColor = Drawing.Color.DarkGray

            Dim GroupName As String = Profiles(Name).GetSetting(ConfigOptions.Group)
            If GroupName IsNot Nothing AndAlso GroupName <> "" Then
                If Not Groups.Contains(GroupName) Then
                    Groups.Add(GroupName)
                    Main_Actions.Groups.Add(New ListViewGroup(GroupName, GroupName))
                End If

                NewItem.Group = Main_Actions.Groups.Item(GroupName)
            End If
        Next
    End Sub

    Sub Main_Display_Options(ByVal Name As String, ByVal Clear As Boolean)
        Main_Name.Text = Name

        Main_Method.Text = ""
        Main_Source.Text = ""
        Main_Destination.Text = ""
        Main_LimitedCopy.Text = ""
        Main_FileTypes.Text = ""
        Main_Scheduling.Text = ""
        Main_TimeOffset.Text = ""

        If Clear Then Exit Sub

        Main_Method.Text = GetMethodName(Name)
        Main_Source.Text = Profiles(Name).GetSetting(ConfigOptions.Source)
        Main_Destination.Text = Profiles(Name).GetSetting(ConfigOptions.Destination)

        Main_Scheduling.Text = Translation.Translate("\" & Profiles(Name).Scheduler.Frequency.ToUpper)

        Select Case Profiles(Name).Scheduler.Frequency
            Case ScheduleInfo.WEEKLY
                Dim Day As String = Translation.Translate("\WEEK_DAYS", ";;;;;;").Split(";"c)(Profiles(Name).Scheduler.WeekDay)
                Main_Scheduling.Text &= Day
            Case ScheduleInfo.MONTHLY
                Main_Scheduling.Text &= Profiles(Name).Scheduler.MonthDay
        End Select

        If Profiles(Name).Scheduler.Frequency = ScheduleInfo.NEVER Then
            Main_Scheduling.Text = ""
        Else
            Main_Scheduling.Text &= ", " & Profiles(Name).Scheduler.Hour.ToString.PadLeft(2, "0"c) & Translation.Translate("\H_M_SEP") & Profiles(Name).Scheduler.Minute.ToString.PadLeft(2, "0"c)
        End If

        Main_TimeOffset.Text = Profiles(Name).GetSetting(ConfigOptions.TimeOffset)

        Select Case CInt(Profiles(Name).GetSetting(ConfigOptions.Restrictions, "0"))
            Case 0
                Main_LimitedCopy.Text = Translation.Translate("\NO")
            Case 1, 2
                Main_LimitedCopy.Text = Translation.Translate("\YES")
        End Select

        Select Case CInt(Profiles(Name).GetSetting(ConfigOptions.Restrictions, "0"))
            Case 1
                Main_FileTypes.Text = Profiles(Name).GetSetting(ConfigOptions.IncludedTypes, "")
            Case 2
                Main_FileTypes.Text = "-" & Profiles(Name).GetSetting(ConfigOptions.ExcludedTypes, "")
        End Select
    End Sub

    Function GetMethodName(ByVal Name As String) As String
        Select Case Profiles(Name).GetSetting(ConfigOptions.Method, "")
            Case "1"
                Return Translation.Translate("\LR_INCREMENTAL")
            Case "2"
                Return Translation.Translate("\TWOWAYS_INCREMENTAL")
            Case Else
                Return Translation.Translate("\LR_MIRROR")
        End Select
    End Function

    Function CheckValidity() As Boolean
        If Not Profiles(CurrentProfile).ValidateConfigFile(True) Then
            Interaction.ShowMsg(Translation.Translate("\INVALID_CONFIG"), Translation.Translate("\ERROR"), , MessageBoxIcon.Error)
            Return False
        End If
        Return True
    End Function

    Sub Main_TryUnregStartAtBoot()
        Dim NeedToRunAtBootTime As Boolean = False
        For Each Profile As ProfileHandler In Profiles.Values
            NeedToRunAtBootTime = NeedToRunAtBootTime Or (Profile.Scheduler.Frequency <> ScheduleInfo.NEVER)
            If Profile.Scheduler.Frequency <> ScheduleInfo.NEVER Then ConfigHandler.LogAppEvent(String.Format("Profile {0} requires the scheduler to run.", Profile.ProfileName))
        Next

        Try
            If NeedToRunAtBootTime Then
                ConfigHandler.LogAppEvent("Registered program in startup list")
                ConfigHandler.RegisterBoot()
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

    Function CurrentProfile() As String
        Return Main_Actions.SelectedItems(0).Text
    End Function

    Public Delegate Sub ExitAppCallBack()
    Public Sub ExitApp()
        Me.Close()
        Application.Exit()
    End Sub
#End Region

End Class