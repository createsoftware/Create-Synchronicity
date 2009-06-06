Public Class Settings
    Dim Handler As SettingsHandler
    Dim ProcessingNodes As Boolean = False
    Dim ClickedRightTreeView As Boolean = False

#Region " Events "
    Private Sub Settings_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_IncludeExcludeCheckBox.CheckedChanged, Settings_IncludeFilesOption.CheckedChanged, Settings_ExcludeFilesOption.CheckedChanged
        Settings_Update_Form_Enabled_Components()
    End Sub

    Private Sub Settings_SaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_SaveButton.Click
        Settings_Update(False)
        If Handler.SaveConfigFile() Then Me.Close()
    End Sub

    Private Sub Settings_CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_CancelButton.Click
        Me.Close()
    End Sub

    Private Sub Settings_BrowseLButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_BrowseLButton.Click
        Settings_FolderBrowser.Description = Settings_FolderBrowser.Description.Replace("%", "from")
        If Settings_FolderBrowser.ShowDialog = Windows.Forms.DialogResult.OK Then Settings_FromTextBox.Text = Settings_FolderBrowser.SelectedPath
        Settings_FolderBrowser.Description = "Select a folder to copy the files %."
    End Sub

    Private Sub Settings_BrowseRButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_BrowseRButton.Click
        Settings_FolderBrowser.Description = Settings_FolderBrowser.Description.Replace("%", "to")
        If Settings_FolderBrowser.ShowDialog = Windows.Forms.DialogResult.OK Then Settings_ToTextBox.Text = Settings_FolderBrowser.SelectedPath
        Settings_FolderBrowser.Description = "Select a folder to copy the files %."
    End Sub

    Private Sub Settings_ReloadButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_ReloadButton.Click
        Settings_ReloadTrees()
    End Sub

    Private Sub Settings_MethodOption_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_TwoWaysIncrementalMethodOption.MouseEnter, Settings_LRMirrorMethodOption.MouseEnter, Settings_LRIncrementalMethodOption.MouseEnter
        Settings_DescriptionLabel.Text = sender.Tag.Replace("%s", sender.Text).Replace("\n", Microsoft.VisualBasic.vbNewLine)
    End Sub

    Private Sub Settings_MethodOption_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_TwoWaysIncrementalMethodOption.MouseLeave, Settings_LRMirrorMethodOption.MouseLeave, Settings_LRIncrementalMethodOption.MouseLeave
        Settings_DescriptionLabel.Text = Settings_DescriptionLabel.Tag.ToString
    End Sub

    Private Sub Settings_View_AfterCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles Settings_RightView.AfterCheck, Settings_LeftView.AfterCheck
        If ProcessingNodes Then Exit Sub
        If Not (Settings_OverAllCheckStatus(e.Node) = CInt(e.Node.Checked)) And e.Node.Nodes.Count > 0 Then e.Node.FirstNode.EnsureVisible()
    End Sub

    Private Sub Settings_View_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Settings_RightView.MouseClick, Settings_LeftView.MouseClick
        If e.Button = Windows.Forms.MouseButtons.Right Then
            ClickedRightTreeView = (sender.Name = "Settings_RightView")
            If ClickedRightTreeView Then
                Settings_RightView.SelectedNode = Settings_RightView.GetNodeAt(e.Location)
            Else
                Settings_LeftView.SelectedNode = Settings_LeftView.GetNodeAt(e.Location)
            End If
        End If
    End Sub

    Private Sub Settings_SynchronizeAllSubfoldersMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_SynchronizeAllSubfoldersMenuItem.Click
        Settings_Update_CheckStatus(True)
    End Sub

    Private Sub Settings_DontSynchronizeSubfoldersMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_DontSynchronizeSubfoldersMenuItem.Click
        Settings_Update_CheckStatus(False)
    End Sub

    Private Sub Settings_LRMirrorMethodOption_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_LRMirrorMethodOption.CheckedChanged
        Settings_RightView.CheckBoxes = Not Settings_LRMirrorMethodOption.Checked
    End Sub

    Public Sub New(ByVal Name As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Handler = New SettingsHandler(Name)
    End Sub
#End Region

#Region " Form and TreeView manipulation "
    Sub Settings_Update_Form_Enabled_Components()
        Settings_IncludeExcludeLayoutPanel.Enabled = Settings_IncludeExcludeCheckBox.Checked
        Settings_IncludedTypesTextBox.Enabled = Settings_IncludeFilesOption.Checked
        Settings_ExcludedTypesTextBox.Enabled = Settings_ExcludeFilesOption.Checked
    End Sub

    Sub Settings_Update_CheckStatus(ByVal Checked As Boolean)
        ProcessingNodes = True
        If ClickedRightTreeView Then
            Settings_RightView.SelectedNode.Checked = Checked
            Settings_CheckChildNodes(Settings_RightView.SelectedNode)
        Else
            Settings_LeftView.SelectedNode.Checked = Checked
            Settings_CheckChildNodes(Settings_LeftView.SelectedNode)
        End If
        ProcessingNodes = False
    End Sub

    Sub Settings_CheckChildNodes(ByVal Root As TreeNode)
        For Each SubNode As TreeNode In Root.Nodes
            SubNode.Checked = Root.Checked
            Settings_CheckChildNodes(SubNode)
        Next
    End Sub

    Sub Settings_GetCheckedNodes(ByRef NodesList As SortedList(Of String, Boolean), ByRef Node As TreeNode)
        Dim AppendedString As String = ""
        Dim OverAllNodeStatus As Integer = Settings_OverAllCheckStatus(Node)

        Select Case OverAllNodeStatus
            Case -1, 0
                AppendedString = ""
            Case 1
                AppendedString = "/"
        End Select

        If Node.Checked OrElse Node.TreeView.CheckBoxes = False Then NodesList.Add(Node.FullPath & AppendedString, True)

        If OverAllNodeStatus = 1 And (Node.Checked OrElse Node.TreeView.CheckBoxes = False) Then Exit Sub
        For NodeId As Integer = 0 To Node.Nodes.Count - 1
            If OverAllNodeStatus = 1 Then
                NodesList.Add(Node.Nodes(NodeId).FullPath & AppendedString, True)
            Else
                Settings_GetCheckedNodes(NodesList, Node.Nodes(NodeId))
            End If
        Next
    End Sub

    Function Settings_OverAllCheckStatus(ByRef Node As TreeNode) As Integer '0 All clear, 1 All checked, -1 different states
        If Not Node.TreeView.CheckBoxes Then Return 1
        If (Node.Nodes.Count = 0) Then Return If(Node.Checked, 1, 0)

        Dim AllChecked As Boolean = True, AllClear As Boolean = True
        For Each SubNode As TreeNode In Node.Nodes
            Dim CurrentStatus As Boolean = (Settings_OverAllCheckStatus(SubNode) = 1)
            AllChecked = AllChecked And CurrentStatus
            AllClear = AllClear And Not CurrentStatus
        Next

        If AllChecked Then Return 1
        If AllClear Then Return 0
        Return -1
    End Function

    Sub Settings_ReloadTrees()
        Me.Settings_LeftView.Nodes.Clear()
        Me.Settings_RightView.Nodes.Clear()

        Settings_LoadTree(Me.Settings_LeftView.Nodes.Add(""), Settings_FromTextBox.Text, Handler.LeftCheckedNodes)
        Settings_LoadTree(Me.Settings_RightView.Nodes.Add(""), Settings_ToTextBox.Text, Handler.RightCheckedNodes)
    End Sub

    Sub Settings_LoadTree(ByRef Node As TreeNode, ByVal Path As String, ByRef CheckedPathes As SortedList(Of String, Boolean)) 'TODO: Optimiser la recherche dans la liste
        If Not IO.Directory.Exists(Path) Then Exit Sub

        Dim InsertedNode As TreeNode
        For Each Dir As String In IO.Directory.GetDirectories(Path)
            InsertedNode = Node.Nodes.Add(Dir.Substring(Dir.LastIndexOf("\") + 1))
            Settings_LoadTree(InsertedNode, Dir, CheckedPathes)
        Next

        If CheckedPathes.ContainsKey(Node.FullPath) Then
            Node.Checked = True
            If CheckedPathes(Node.FullPath) = True Then
                Settings_CheckChildNodes(Node)
                Node.Collapse()
            End If
        End If
    End Sub
#End Region

#Region " Settings Handling "
    Sub Settings_Update(ByVal LoadToForm As Boolean)
        Handler.SetSetting("From", Settings_FromTextBox.Text, LoadToForm)
        Handler.SetSetting("To", Settings_ToTextBox.Text, LoadToForm)
        Handler.SetSetting("LimitedCopy", Settings_IncludeExcludeCheckBox.Checked, LoadToForm)
        Handler.SetSetting("IncludedTypes", Settings_IncludedTypesTextBox.Text, LoadToForm)
        Handler.SetSetting("ExcludedTypes", Settings_ExcludedTypesTextBox.Text, LoadToForm)
        Handler.SetSetting("ReplicateEmptyDirectories", Settings_ReplicateEmptyDirectoriesOption.Checked, LoadToForm)

        Dim Restrictions As String = (If(Settings_IncludeExcludeCheckBox.Checked, 1, 0) * (If(Settings_IncludeFilesOption.Checked, 1, 0) + 2 * If(Settings_ExcludeFilesOption.Checked, 1, 0))).ToString
        Dim Method As String = (If(Settings_LRIncrementalMethodOption.Checked, 1, 0) * 1 + If(Settings_TwoWaysIncrementalMethodOption.Checked, 1, 0) * 2).ToString
        Select Case LoadToForm
            Case False
                Handler.SetSetting("Method", Method)
                Handler.SetSetting("Restrictions", Restrictions)
            Case True
                Select Case Handler.GetSetting("Method")
                    Case "1"
                        Settings_LRIncrementalMethodOption.Checked = True
                    Case "2"
                        Settings_TwoWaysIncrementalMethodOption.Checked = True
                    Case Else
                        Settings_LRMirrorMethodOption.Checked = True
                End Select

                Settings_IncludeExcludeCheckBox.Checked = True
                Select Case Handler.GetSetting("Restrictions")
                    Case "1"
                        Settings_IncludeFilesOption.Checked = True
                    Case "2"
                        Settings_ExcludeFilesOption.Checked = True
                    Case Else
                        Settings_IncludeExcludeCheckBox.Checked = False
                End Select
        End Select

        Select Case LoadToForm
            Case False
                Handler.LeftCheckedNodes.Clear()
                Handler.RightCheckedNodes.Clear()
                Settings_GetCheckedNodes(Handler.LeftCheckedNodes, Settings_LeftView.Nodes(0))
                Settings_GetCheckedNodes(Handler.RightCheckedNodes, Settings_RightView.Nodes(0))

                Handler.SetSetting("EnabledLeftSubFolders", Settings_GetString(Handler.LeftCheckedNodes))
                Handler.SetSetting("EnabledRightSubFolders", Settings_GetString(Handler.RightCheckedNodes))
            Case True
                Settings_ReloadTrees()
        End Select

        If LoadToForm Then Settings_Update_Form_Enabled_Components()
    End Sub

    Function Settings_GetString(ByRef Table As SortedList(Of String, Boolean)) As String
        Dim ListString As String = ""
        For Each Node As String In Table.Keys
            ListString &= Node & ";"
        Next
        Return ListString
    End Function

    Private Sub Settings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Settings_Update(True)
        Me.Text = "Settings for " & Handler.ConfigName & " profile"
    End Sub
#End Region
End Class
