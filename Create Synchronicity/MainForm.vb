'This file is part of Create Synchronicity.
'
'Create Synchronicity is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
'Create Synchronicity is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with Create Synchronicity.  If not, see <http://www.gnu.org/licenses/>.
'Created by:	Clément Pit--Claudel.
'Web site:		http://synchronicity.sourceforge.net.

Public Class MainForm
    Dim SettingsArray As Dictionary(Of String, SettingsHandler)

#Region " Events "
    Private Sub MainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        IO.Directory.CreateDirectory(Get_LogFolder())
        IO.Directory.CreateDirectory(Get_ConfigFolder())
        Main_ReloadConfigs()
    End Sub

    Private Sub Main_Actions_Click(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Main_Actions.MouseClick
        If Not (Main_Actions.SelectedItems.Count = 0 OrElse Main_Actions.SelectedIndices(0) = 0) Then Main_ActionsMenu.Show(Main_Actions, e.Location)
    End Sub

    Private Sub Main_Actions_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Main_Actions.DoubleClick
        If Main_Actions.SelectedItems.Count = 0 OrElse Not Main_Actions.SelectedIndices(0) = 0 Then Exit Sub

        Main_Actions.LabelEdit = True
        Main_Actions.SelectedItems(0).BeginEdit()
    End Sub

    Private Sub Main_Actions_AfterLabelEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LabelEditEventArgs) Handles Main_Actions.AfterLabelEdit
        e.CancelEdit = True
        Main_Actions.LabelEdit = False
        If e.Label = "" OrElse e.Label.IndexOfAny(IO.Path.GetInvalidFileNameChars) >= 0 OrElse IO.File.Exists(ConfigOptions.GetConfigPath(e.Label)) Then
            Exit Sub
        End If
        Dim SettingsForm As New Settings(e.Label)
        SettingsForm.ShowDialog()
        Main_ReloadConfigs()
    End Sub

    Private Sub Main_Actions_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Main_Actions.SelectedIndexChanged
        If Main_Actions.SelectedIndices.Count = 0 Then
            Main_Display_Options("", True)
        ElseIf Main_Actions.SelectedIndices(0) = 0 Then
            Main_Display_Options("Create a new profile", True)
        End If

        If Main_Actions.SelectedIndices.Count = 0 OrElse Main_Actions.SelectedIndices(0) = 0 Then
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
        If Microsoft.VisualBasic.MsgBox("Do you really want to delete """ & Main_Actions.SelectedItems(0).Text & """ profile ?", Microsoft.VisualBasic.MsgBoxStyle.YesNo + Microsoft.VisualBasic.MsgBoxStyle.Information, "Confirm deletion") = Microsoft.VisualBasic.MsgBoxResult.Yes Then
            SettingsArray(Main_Actions.SelectedItems(0).Text).DeleteConfigFile()
            SettingsArray(Main_Actions.SelectedItems(0).Text) = Nothing
            Main_Actions.Items.RemoveAt(Main_Actions.SelectedIndices(0))
        End If
    End Sub

    Private Sub ViewLogMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewLogMenuItem.Click
        Diagnostics.Process.Start(Get_LogFolder() & "\" & Main_Actions.SelectedItems(0).Text & ".log")
    End Sub
#End Region

#Region " Functions and Routines "
    Sub Main_ReloadConfigs()
        SettingsArray = New Dictionary(Of String, SettingsHandler)
        Dim CreateProfileItem As ListViewItem = Main_Actions.Items(0)
        Main_Actions.Items.Clear() : Main_Actions.Items.Add(CreateProfileItem).Group = Main_Actions.Groups(0)

        For Each ConfigFile As String In IO.Directory.GetFiles(Get_ConfigFolder(), "*.sync")
            Dim Name As String = ConfigFile.Substring(ConfigFile.LastIndexOf("\") + 1)
            Name = Name.Substring(0, Name.LastIndexOf("."))

            SettingsArray.Add(Name, New SettingsHandler(Name))

            Dim NewItem As ListViewItem = Main_Actions.Items.Add(Name)
            NewItem.Group = Main_Actions.Groups("Profiles")
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
                Main_LimitedCopy.Text = "No"
            Case 1, 2
                Main_LimitedCopy.Text = "Yes"
            Case 1
                Main_FileTypes.Text = SettingsArray(Name).GetSetting(ConfigOptions.IncludedTypes, "")
            Case 2
                Main_FileTypes.Text = "-" & SettingsArray(Name).GetSetting(ConfigOptions.ExcludedTypes, "")
        End Select
     End Sub

    Function Get_ConfigFolder() As String
        Return Application.StartupPath & "\config"
    End Function

    Function Get_LogFolder() As String
        Return Application.StartupPath & "\log"
    End Function

    Function GetMethodName(ByVal Name As String) As String
        Select Case SettingsArray(Name).GetSetting(ConfigOptions.Method, "")
            Case "1"
                Return "Left to Right (Incremental)"
            Case "2"
                Return "Two-ways incremental"
            Case Else
                Return "Left to Right (Mirror)"
        End Select
    End Function

    Function CheckValidity() As Boolean
        If Not SettingsArray(Main_Actions.SelectedItems(0).Text).ValidateConfigFile() Then
            Microsoft.VisualBasic.MsgBox("Invalid Config !", Microsoft.VisualBasic.MsgBoxStyle.OkOnly + Microsoft.VisualBasic.MsgBoxStyle.Critical, "Error")
            Return False
        End If
        Return True
    End Function
#End Region

End Class