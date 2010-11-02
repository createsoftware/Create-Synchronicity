'This file is part of Create Synchronicity.
'
'Create Synchronicity is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
'Create Synchronicity is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with Create Synchronicity.  If not, see <http://www.gnu.org/licenses/>.
'Created by:	Clément Pit--Claudel.
'Web site:		http://synchronicity.sourceforge.net.

Public Class MainForm
#Region " Events "
    Private Sub MainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = ProgramConfig.GetIcon()

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
        RedoSchedulerRegistration()
    End Sub

    Private Sub MainForm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Interaction.HideStatusIcon()
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
        RedoSchedulerRegistration()
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
    Sub Main_SetVisible(ByVal _Visible As Boolean)
        If Me.IsDisposed Then Exit Sub
        Me.Visible = _Visible
    End Sub

    Sub Main_ReloadConfigs()
        If Me.IsDisposed Then Exit Sub

        Dim CreateProfileItem As ListViewItem = Main_Actions.Items(0)

        ReloadProfiles()
        Main_Actions.Items.Clear()
        Main_Actions.Items.Add(CreateProfileItem).Group = Main_Actions.Groups(0)

        Dim Groups As New List(Of String)
        For Each ProfilePair As KeyValuePair(Of String, ProfileHandler) In Profiles
            Dim ProfileName As String = ProfilePair.Key
            Dim NewItem As ListViewItem = Main_Actions.Items.Add(ProfileName)

            NewItem.Group = Main_Actions.Groups(1)
            NewItem.ImageIndex = CInt(Profiles(ProfileName).GetSetting(ConfigOptions.Method))
            NewItem.SubItems.Add(GetMethodName(ProfileName)).ForeColor = Drawing.Color.DarkGray

            Dim GroupName As String = Profiles(ProfileName).GetSetting(ConfigOptions.Group)
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