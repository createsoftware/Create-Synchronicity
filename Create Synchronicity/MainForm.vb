'This file is part of Create Synchronicity.
'
'Create Synchronicity is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
'Create Synchronicity is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with Create Synchronicity.  If not, see <http://www.gnu.org/licenses/>.
'Created by:	Clément Pit--Claudel.
'Web site:		http://synchronicity.sourceforge.net.

Public Class MainForm
    Dim Quiet As Boolean
    Dim Profiles As Dictionary(Of String, ProfileHandler)

    Dim Translation As LanguageHandler = LanguageHandler.GetSingleton
    Dim ProgramConfig As ConfigHandler = ConfigHandler.GetSingleton

    Dim ExitScheduler As Boolean = False

#Region " Events "
    Private Sub MainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = ProgramConfig.GetIcon()

        IO.Directory.CreateDirectory(ProgramConfig.LogRootDir)
        IO.Directory.CreateDirectory(ProgramConfig.ConfigRootDir)
        IO.Directory.CreateDirectory(ProgramConfig.LanguageRootDir)

#If DEBUG Then
        Interaction.ShowMsg(Translation.Translate("\DEBUG_WARNING"), Translation.Translate("\DEBUG_MODE"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
#End If

        ProgramConfig.LoadProgramSettings()
        If Not ProgramConfig.ProgramSettingsSet(ConfigOptions.AutoUpdates) Or Not ProgramConfig.ProgramSettingsSet(ConfigOptions.Language) Then
            If Not ProgramConfig.ProgramSettingsSet(ConfigOptions.AutoUpdates) Then
                If Interaction.ShowMsg(Translation.Translate("\WELCOME_MSG"), Translation.Translate("\FIRST_RUN"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    ProgramConfig.SetProgramSetting(ConfigOptions.AutoUpdates, "True")
                Else
                    ProgramConfig.SetProgramSetting(ConfigOptions.AutoUpdates, "False")
                End If
            End If

            If Not ProgramConfig.ProgramSettingsSet(ConfigOptions.Language) Then
                ProgramConfig.SetProgramSetting(ConfigOptions.Language, ConfigOptions.DefaultLanguage)
            End If

            ProgramConfig.SaveProgramSettings()
        End If

        If ProgramConfig.GetProgramSetting(ConfigOptions.AutoUpdates, "False") Then
            Dim UpdateThread As New Threading.Thread(AddressOf Updates.CheckForUpdates)
            UpdateThread.Start(True)
        End If

        Translation.TranslateControl(Me)
        Translation.TranslateControl(Me.Main_ActionsMenu)

        Main_ReloadConfigs()
        Main_TryUnregStartAtBoot()

        Dim TaskToRun As String = ""
        Dim ShowPreview As Boolean = False
        Dim ArgsList As New List(Of String)(Environment.GetCommandLineArgs())

        If ArgsList.Count > 1 Then
            Quiet = ArgsList.Contains("/quiet")
            ShowPreview = ArgsList.Contains("/preview")


            Dim RunArgIndex As Integer = ArgsList.IndexOf("/run")
            If RunArgIndex <> -1 AndAlso RunArgIndex + 1 < ArgsList.Count Then
                TaskToRun = ArgsList(RunArgIndex + 1)
            End If
        End If

        If TaskToRun <> "" Then
            If Profiles.ContainsKey(TaskToRun) Then
                If Profiles(TaskToRun).ValidateConfigFile() Then
                    Dim SyncForm As New SynchronizeForm(TaskToRun, ShowPreview, Not Quiet, True)
                    Main_HideForm()
                Else
                    Interaction.ShowMsg(Translation.Translate("\INVALID_CONFIG"), Translation.Translate("\INVALID_CMD"), , MessageBoxIcon.Error)
                End If
            Else
                Interaction.ShowMsg(Translation.Translate("\INVALID_PROFILE"), Translation.Translate("\INVALID_CMD"), , MessageBoxIcon.Error)
            End If
        ElseIf ArgsList.Contains("/scheduler") Then
            Main_HideForm()
            ApplicationTimer.Start()
        End If
    End Sub

    Private Sub Main_Actions_Click(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Main_Actions.MouseClick
        If Main_Actions.SelectedItems.Count = 0 OrElse Main_Actions.SelectedIndices(0) = 0 Then Exit Sub
        Main_ActionsMenu.Show(Main_Actions, e.Location)
    End Sub

    Private Sub Main_Actions_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Main_Actions.DoubleClick
        If Main_Actions.SelectedItems.Count = 0 OrElse Not Main_Actions.SelectedIndices(0) = 0 Then Exit Sub

        Main_Actions.LabelEdit = True
        Main_Actions.SelectedItems(0).BeginEdit()
    End Sub

    Private Sub Main_Actions_AfterLabelEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LabelEditEventArgs) Handles Main_Actions.AfterLabelEdit
        e.CancelEdit = True
        Main_Actions.LabelEdit = False
        If e.Label = "" OrElse e.Label.IndexOfAny(IO.Path.GetInvalidFileNameChars) >= 0 Then Exit Sub

        Dim SettingsForm As New SettingsForm(e.Label)
        SettingsForm.ShowDialog()
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
        Dim SyncForm As New SynchronizeForm(CurrentProfile, True)
        Me.Visible = False : SyncForm.ShowDialog() : Me.Visible = True
        SyncForm.Dispose()
    End Sub

    Private Sub SynchronizeMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SynchronizeMenuItem.Click
        If Not CheckValidity() Then Exit Sub

        Dim SyncForm As New SynchronizeForm(CurrentProfile, False)
        Me.Visible = False : SyncForm.ShowDialog() : Me.Visible = True
        SyncForm.Dispose()
    End Sub

    Private Sub ChangeSettingsMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Main_ChangeSettingsMenuItem.Click
        Dim SettingsForm As New SettingsForm(CurrentProfile)
        SettingsForm.ShowDialog()
        Main_ReloadConfigs()
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If Interaction.ShowMsg(String.Format(Translation.Translate("\DELETE_PROFILE"), CurrentProfile), Translation.Translate("\CONFIRM_DELETION"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Profiles(CurrentProfile).DeleteConfigFile()
            Profiles(CurrentProfile) = Nothing
            Main_Actions.Items.RemoveAt(Main_Actions.SelectedIndices(0))
        End If
    End Sub

    Private Sub ViewLogMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewLogMenuItem.Click
        If Not IO.File.Exists(ProgramConfig.GetLogPath(CurrentProfile)) Then Exit Sub
        Diagnostics.Process.Start(ProgramConfig.GetLogPath(CurrentProfile))
    End Sub

    Private Sub ClearLogMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearLogMenuItem.Click
        Profiles(CurrentProfile).DeleteLogFile()
    End Sub

    Private Sub Main_ScheduleMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Main_ScheduleMenuItem.Click
        Dim SchedForm As New SchedulingForm(CurrentProfile)
        SchedForm.ShowDialog()
        Main_ReloadConfigs()
    End Sub

    Private Sub ApplicationTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ApplicationTimer.Tick
        Static ProfilesQueue As Queue(Of KeyValuePair(Of String, Date))
        If ProfilesQueue Is Nothing Then
            ProfilesQueue = New Queue(Of KeyValuePair(Of String, Date))
            Dim ProfilesToRun As New List(Of KeyValuePair(Of Date, String))

            For Each Profile As KeyValuePair(Of String, ProfileHandler) In Profiles
                If Profile.Value.Scheduler.Frequency <> ScheduleInfo.NEVER Then ProfilesToRun.Add(New KeyValuePair(Of Date, String)(Profile.Value.Scheduler.NextRun(), Profile.Key))
            Next

            ProfilesToRun.Sort()
            For Each P As KeyValuePair(Of Date, String) In ProfilesToRun : ProfilesQueue.Enqueue(New KeyValuePair(Of String, Date)(P.Value, P.Key)) : Next
        End If

        If ProfilesQueue.Peek().Value < Date.Now Then
            Dim NextProfile As KeyValuePair(Of String, Date) = ProfilesQueue.Dequeue()
            Dim SyncForm As New SynchronizeForm(NextProfile.Key, False, False, False)
            ProfilesQueue.Enqueue(New KeyValuePair(Of String, Date)(NextProfile.Key, Profiles(NextProfile.Key).Scheduler.NextRun()))
        End If
    End Sub
#End Region

#Region " Functions and Routines "
    Sub Main_HideForm()
        'TODO: Yuck
        Me.Opacity = 0
        Me.WindowState = FormWindowState.Minimized
        Me.ShowInTaskbar = False
    End Sub

    Sub Main_ReloadConfigs()
        Profiles = New Dictionary(Of String, ProfileHandler)
        Dim CreateProfileItem As ListViewItem = Main_Actions.Items(0)

        Main_Actions.Items.Clear()
        Main_Actions.Items.Add(CreateProfileItem).Group = Main_Actions.Groups(0)

        For Each ConfigFile As String In IO.Directory.GetFiles(ProgramConfig.ConfigRootDir, "*.sync")
            Dim Name As String = ConfigFile.Substring(ConfigFile.LastIndexOf("\") + 1)
            Name = Name.Substring(0, Name.LastIndexOf("."))

            Profiles.Add(Name, New ProfileHandler(Name))

            Dim NewItem As ListViewItem = Main_Actions.Items.Add(Name)
            NewItem.Group = Main_Actions.Groups(1)
            NewItem.ImageIndex = CInt(Profiles(Name).GetSetting(ConfigOptions.Method))
            NewItem.SubItems.Add(GetMethodName(Name)).ForeColor = Drawing.Color.DarkGray
        Next
    End Sub

    Sub Main_Display_Options(ByVal Name As String, ByVal Clear As Boolean)
        Main_Name.Text = Name

        Main_Method.Text = ""
        Main_Source.Text = ""
        Main_Destination.Text = ""
        Main_LimitedCopy.Text = ""
        Main_FileTypes.Text = ""

        If Clear Then
            Exit Sub
        End If
        Main_Method.Text = GetMethodName(Name)

        Main_Source.Text = Profiles(Name).GetSetting(ConfigOptions.Source)
        Main_Destination.Text = Profiles(Name).GetSetting(ConfigOptions.Destination)

        Select Case CInt(Profiles(Name).GetSetting(ConfigOptions.Restrictions, "0"))
            Case 0
                Main_LimitedCopy.Text = Translation.Translate("\NO")
            Case 1, 2
                Main_LimitedCopy.Text = Translation.Translate("\YES")
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
        Next

        Try
            If Not NeedToRunAtBootTime Then
                If My.Computer.Registry.GetValue(ConfigOptions.RegistryRootedBootKey, ConfigOptions.RegistryBootVal, Nothing) IsNot Nothing Then My.Computer.Registry.CurrentUser.OpenSubKey(ConfigOptions.RegistryBootKey, True)
            End If
        Catch Ex As Exception
            Interaction.ShowMsg(Translation.Translate("\UNREG_ERROR"), Translation.Translate("\ERROR"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Function CurrentProfile() As String
        Return Main_Actions.SelectedItems(0).Text
    End Function
#End Region
End Class