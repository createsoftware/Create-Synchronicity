'This file is part of Create Synchronicity.
'
'Create Synchronicity is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
'Create Synchronicity is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with Create Synchronicity.  If not, see <http://www.gnu.org/licenses/>.
'Created by:	Clément Pit--Claudel.
'Web site:		http://synchronicity.sourceforge.net.

Public Class MainForm
    Dim Quiet As Boolean
    Dim SettingsArray As Dictionary(Of String, SettingsHandler)

    Dim Translation As LanguageHandler = LanguageHandler.GetSingleton

#Region " Events "
    Private Sub MainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = ConfigOptions.GetIcon()

        IO.Directory.CreateDirectory(ConfigOptions.LogRootDir)
        IO.Directory.CreateDirectory(ConfigOptions.ConfigRootDir)
        IO.Directory.CreateDirectory(ConfigOptions.LanguageRootDir)

#If DEBUG Then
        MessageBox.Show(Translation.Translate("\DEBUG_WARNING"), Translation.Translate("\DEBUG_MODE"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
#End If

        ConfigOptions.LoadProgramSettings()
        If Not ConfigOptions.ProgramSettingsSet(ConfigOptions.AutoUpdates) Or Not ConfigOptions.ProgramSettingsSet(ConfigOptions.Language) Then
            If Not ConfigOptions.ProgramSettingsSet(ConfigOptions.AutoUpdates) Then
                If Microsoft.VisualBasic.MsgBox(Translation.Translate("\WELCOME_MSG"), Microsoft.VisualBasic.MsgBoxStyle.YesNo Or Microsoft.VisualBasic.MsgBoxStyle.Question, "First Run") = Microsoft.VisualBasic.MsgBoxResult.Yes Then
                    ConfigOptions.SetProgramSetting(ConfigOptions.AutoUpdates, "True")
                Else
                    ConfigOptions.SetProgramSetting(ConfigOptions.AutoUpdates, "False")
                End If
            End If

            If Not ConfigOptions.ProgramSettingsSet(ConfigOptions.Language) Then
                ConfigOptions.SetProgramSetting(ConfigOptions.Language, ConfigOptions.DefaultLanguage)
            End If

            ConfigOptions.SaveProgramSettings()
        End If

        If ConfigOptions.GetProgramSetting(ConfigOptions.AutoUpdates, "False") Then
            Dim UpdateThread As New Threading.Thread(AddressOf ConfigOptions.CheckForUpdates)
            UpdateThread.Start(True)
        End If

        Translation.TranslateControl(Me)
        Translation.TranslateControl(Me.Main_ActionsMenu)
        Main_ReloadConfigs()

        Dim TaskToRun As String = ""
        Dim ShowPreview As Boolean = False
        Dim ArgsList As New List(Of String)(Environment.GetCommandLineArgs())

        If ArgsList.Count > 1 Then
            If ArgsList.IndexOf("/quiet") <> -1 Then
                Quiet = True
            End If

            ShowPreview = (ArgsList.IndexOf("/preview") <> -1)

            Dim RunArgIndex As Integer = ArgsList.IndexOf("/run")
            If RunArgIndex <> -1 AndAlso RunArgIndex + 1 < ArgsList.Count Then
                TaskToRun = ArgsList(RunArgIndex + 1)
            End If
        End If

        If TaskToRun <> "" Then
            If SettingsArray.ContainsKey(TaskToRun) Then
                If SettingsArray(TaskToRun).ValidateConfigFile() Then
                    Dim SyncForm As New SynchronizeForm(TaskToRun, ShowPreview, Not Quiet, True)

                    'TODO: Yuck
                    Me.Opacity = 0
                    Me.WindowState = FormWindowState.Minimized
                    Me.ShowInTaskbar = False

                    SyncForm.Show()
                Else
                    Microsoft.VisualBasic.MsgBox(Translation.Translate("\INVALID_CONFIG"), Microsoft.VisualBasic.MsgBoxStyle.OkOnly Or Microsoft.VisualBasic.MsgBoxStyle.Critical, "Invalid command-line arguments")
                End If
            Else
                Microsoft.VisualBasic.MsgBox(Translation.Translate("\INVALID_PROFILE"), Microsoft.VisualBasic.MsgBoxStyle.OkOnly Or Microsoft.VisualBasic.MsgBoxStyle.Critical, "Invalid command-line arguments")
            End If
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
        If e.Label = "" OrElse e.Label.IndexOfAny(IO.Path.GetInvalidFileNameChars) >= 0 Then 'OrElse IO.File.Exists(ConfigOptions.GetConfigPath(e.Label)) Then
            Exit Sub
        End If
        Dim SettingsForm As New Settings(e.Label)
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

        Main_Display_Options(Main_Actions.SelectedItems(0).Text, False)
    End Sub

    Private Sub Main_AboutLinkLabel_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles Main_AboutLinkLabel.LinkClicked
        Dim About As New AboutForm
        About.ShowDialog()
    End Sub

    Private Sub PreviewMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PreviewMenuItem.Click
        If Not CheckValidity() Then Exit Sub
        Dim SyncForm As New SynchronizeForm(Main_Actions.SelectedItems(0).Text, True)
        SyncForm.ShowDialog()
    End Sub

    Private Sub SynchronizeMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SynchronizeMenuItem.Click
        If Not CheckValidity() Then Exit Sub

        Dim SyncForm As New SynchronizeForm(Main_Actions.SelectedItems(0).Text, False)
        SyncForm.ShowDialog()
        SyncForm.Dispose()
    End Sub

    Private Sub ChangeSettingsMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Main_ChangeSettingsMenuItem.Click
        Dim SettingsForm As New Settings(Main_Actions.SelectedItems(0).Text)
        SettingsForm.ShowDialog()
        Main_ReloadConfigs()
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If Microsoft.VisualBasic.MsgBox(String.Format(Translation.Translate("\DELETE_PROFILE"), Main_Actions.SelectedItems(0).Text), Microsoft.VisualBasic.MsgBoxStyle.YesNo Or Microsoft.VisualBasic.MsgBoxStyle.Information, Translation.Translate("\CONFIRM_DELETION")) = Microsoft.VisualBasic.MsgBoxResult.Yes Then
            SettingsArray(Main_Actions.SelectedItems(0).Text).DeleteConfigFile()
            SettingsArray(Main_Actions.SelectedItems(0).Text) = Nothing
            Main_Actions.Items.RemoveAt(Main_Actions.SelectedIndices(0))
        End If
    End Sub

    Private Sub ViewLogMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewLogMenuItem.Click
        If Not IO.File.Exists(ConfigOptions.GetLogPath(Main_Actions.SelectedItems(0).Text)) Then Exit Sub
        Diagnostics.Process.Start(ConfigOptions.GetLogPath(Main_Actions.SelectedItems(0).Text))
    End Sub
#End Region

#Region " Functions and Routines "
    Sub Main_ReloadConfigs()
        SettingsArray = New Dictionary(Of String, SettingsHandler)
        Dim CreateProfileItem As ListViewItem = Main_Actions.Items(0)

        Main_Actions.Items.Clear()
        Main_Actions.Items.Add(CreateProfileItem).Group = Main_Actions.Groups(0)

        For Each ConfigFile As String In IO.Directory.GetFiles(ConfigOptions.ConfigRootDir, "*.sync")
            Dim Name As String = ConfigFile.Substring(ConfigFile.LastIndexOf("\") + 1)
            Name = Name.Substring(0, Name.LastIndexOf("."))

            SettingsArray.Add(Name, New SettingsHandler(Name))

            Dim NewItem As ListViewItem = Main_Actions.Items.Add(Name)
            NewItem.Group = Main_Actions.Groups(1)
            NewItem.ImageIndex = CInt(SettingsArray(Name).GetSetting(ConfigOptions.Method))
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

        Main_Source.Text = SettingsArray(Name).GetSetting(ConfigOptions.Source)
        Main_Destination.Text = SettingsArray(Name).GetSetting(ConfigOptions.Destination)

        Select Case CInt(SettingsArray(Name).GetSetting(ConfigOptions.Restrictions, "0"))
            Case 0
                Main_LimitedCopy.Text = Translation.Translate("\NO")
            Case 1, 2
                Main_LimitedCopy.Text = Translation.Translate("\YES")
            Case 1
                Main_FileTypes.Text = SettingsArray(Name).GetSetting(ConfigOptions.IncludedTypes, "")
            Case 2
                Main_FileTypes.Text = "-" & SettingsArray(Name).GetSetting(ConfigOptions.ExcludedTypes, "")
        End Select
    End Sub

    Function GetMethodName(ByVal Name As String) As String
        Select Case SettingsArray(Name).GetSetting(ConfigOptions.Method, "")
            Case "1"
                Return Translation.Translate("\LR_INCREMENTAL")
            Case "2"
                Return Translation.Translate("\TWOWAYS_INCREMENTAL")
            Case Else
                Return Translation.Translate("\LR_MIRROR")
        End Select
    End Function

    Function CheckValidity() As Boolean
        If Not SettingsArray(Main_Actions.SelectedItems(0).Text).ValidateConfigFile() Then
            Microsoft.VisualBasic.MsgBox(Translation.Translate("\INVALID_CONFIG"), Microsoft.VisualBasic.MsgBoxStyle.OkOnly Or Microsoft.VisualBasic.MsgBoxStyle.Critical, "Error")
            Return False
        End If
        Return True
    End Function
#End Region

#If 0 Then
    Private Sub MainForm_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged
        'Me.Visible = Not Quiet
    End Sub
#End If
End Class