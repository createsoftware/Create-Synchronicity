Public Class MainForm
    Dim SettingsArray As Dictionary(Of String, SettingsHandler)

    Private Sub Main_Actions_Click(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Main_Actions.MouseClick
        If Not (Main_Actions.SelectedItems.Count = 0 OrElse Main_Actions.SelectedIndices(0) = 0) Then Main_ActionsMenu.Show(Main_Actions, e.Location)
    End Sub

    Private Sub Main_Actions_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Main_Actions.SelectedIndexChanged
        If Main_Actions.SelectedIndices.Count = 0 Then
            Main_Display_Options("", True)
        ElseIf Main_Actions.SelectedIndices(0) = 0 Then
            Main_Display_Options("Create a new profile", True)
            Main_ActionsMenu.Close()
            Exit Sub
        End If

        If Main_Actions.SelectedIndices.Count = 0 OrElse Main_Actions.SelectedIndices(0) = 0 Then
            Main_ActionsMenu.Close()
            Exit Sub
        End If

        Main_Display_Options(Main_Actions.SelectedItems(0).Text, False)
    End Sub

    Private Sub MainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Main_ReloadConfigs()
    End Sub

    Sub Main_ReloadConfigs()
        SettingsArray = New Dictionary(Of String, SettingsHandler)
        Dim CreateProfileItem As ListViewItem = Main_Actions.Items(0)
        Main_Actions.Items.Clear() : Main_Actions.Items.Add(CreateProfileItem).Group = Main_Actions.Groups(0)

        IO.Directory.CreateDirectory(Get_BaseFolder())

        For Each ConfigFile As String In IO.Directory.GetFiles(Get_BaseFolder(), "*.sync")
            Dim Name As String = ConfigFile.Substring(ConfigFile.LastIndexOf("\") + 1)
            Name = Name.Substring(0, Name.LastIndexOf("."))

            SettingsArray.Add(Name, New SettingsHandler(Name))

            Dim NewItem As ListViewItem = Main_Actions.Items.Add(Name)
            NewItem.Group = Main_Actions.Groups("Profiles")
            NewItem.ImageIndex = CInt(SettingsArray(Name).GetSetting("Method"))
            NewItem.SubItems.Add(GetMethodName(Name)).ForeColor = Drawing.Color.DarkGray
        Next
    End Sub

    Function Get_BaseFolder() As String
        Return Application.StartupPath & "\config"
    End Function

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

        Main_Source.Text = SettingsArray(Name).GetSetting("From")
        Main_Destination.Text = SettingsArray(Name).GetSetting("To")
        Main_LimitedCopy.Text = If(CBool(SettingsArray(Name).GetSetting("LimitedCopy", "False")), "Yes", "No")
        If Main_LimitedCopy.Text = "Yes" Then Main_FileTypes.Text = If(CBool(SettingsArray(Name).GetSetting("CopyOnly", "False")), SettingsArray(Name).GetSetting("IncludedTypes", ""), "-" & SettingsArray(Name).GetSetting("ExcludedTypes", ""))
    End Sub

    Function GetMethodName(ByVal Name As String) As String
        Select Case SettingsArray(Name).GetSetting("Method", "")
            Case "1"
                Return "Left to Right (Incremental)"
            Case "2"
                Return "Two-ways incremental"
            Case Else
                Return "Left to Right (Mirror)"
        End Select
    End Function

    Private Sub ChangeSettingsMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Main_ChangeSettingsMenuItem.Click
        Dim SettingsForm As New Settings(Main_Actions.SelectedItems(0).Text)
        'Main_ActionsMenu.Close()
        SettingsForm.ShowDialog()
        Main_ReloadConfigs()
    End Sub

    Private Sub Main_Actions_AfterLabelEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LabelEditEventArgs) Handles Main_Actions.AfterLabelEdit
        Dim SettingsForm As New Settings(e.Label)
        e.CancelEdit() = True
        Main_Actions.LabelEdit = False
        'Main_ActionsMenu.Close()
        SettingsForm.ShowDialog()
        Main_ReloadConfigs()
    End Sub

    Private Sub SynchronizeMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SynchronizeMenuItem.Click
        Dim SyncForm As New SynchronizeForm(Main_Actions.SelectedItems(0).Text, False)
        'Main_ActionsMenu.Close()
        SyncForm.ShowDialog()
        SyncForm.Dispose()
    End Sub

    Private Sub PreviewMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PreviewMenuItem.Click
        Dim SyncForm As New SynchronizeForm(Main_Actions.SelectedItems(0).Text, True)
        'Main_ActionsMenu.Close()
        SyncForm.ShowDialog()
    End Sub

    Private Sub Main_Actions_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Main_Actions.DoubleClick
        If Main_Actions.SelectedItems.Count = 0 OrElse Not Main_Actions.SelectedIndices(0) = 0 Then Exit Sub

        Main_Actions.LabelEdit = True
        Main_Actions.SelectedItems(0).BeginEdit()
    End Sub

    Private Sub Main_AboutLinkLabel_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles Main_AboutLinkLabel.LinkClicked
        Dim About As New AboutForm
        About.Show()
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If Microsoft.VisualBasic.MsgBox("Do you really want to delete """ & Main_Actions.SelectedItems(0).Text & """ profile ?", Microsoft.VisualBasic.MsgBoxStyle.YesNo + Microsoft.VisualBasic.MsgBoxStyle.Information, "Confirm deletion") = Microsoft.VisualBasic.MsgBoxResult.Yes Then
            IO.File.Delete(Get_BaseFolder() & "\" & Main_Actions.SelectedItems(0).Text)
            Main_Actions.Items.RemoveAt(Main_Actions.SelectedIndices(0))
        End If
    End Sub
End Class