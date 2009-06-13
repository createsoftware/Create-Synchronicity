'This file is part of Create Synchronicity.
'
'Create Synchronicity is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
'Create Synchronicity is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with Create Synchronicity.  If not, see <http://www.gnu.org/licenses/>.
'Created by:	Clément Pit--Claudel.
'Web site:		http://synchronicity.sourceforge.net.

Public Class Settings
    Dim Handler As SettingsHandler
    Dim ProcessingNodes As Boolean = False
    Dim ClickedRightTreeView As Boolean = False

    'Note:
    'The list called Handler.(left|right)CheckedNodes contains pathes not ending with "/", associated with booleans indicating whether all subfolders /path/ are to be synced.
    'The boolean value is stored as a / appended at the end of the file name.
    'In fact, we have two steps : 
    '   1. Loading and saving the file
    '       1.1 Saving: Booleans calculated as "/"
    '       2.2 Loading: "/" are converted to booleans 
    '   2. Searching the list, were pathes never end with "/"

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
        Settings_FolderBrowser.Description = Settings_FolderBrowser.Tag
    End Sub

    Private Sub Settings_BrowseRButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_BrowseRButton.Click
        Settings_FolderBrowser.Description = Settings_FolderBrowser.Description.Replace("%", "to")
        If Settings_FolderBrowser.ShowDialog = Windows.Forms.DialogResult.OK Then Settings_ToTextBox.Text = Settings_FolderBrowser.SelectedPath
        Settings_FolderBrowser.Description = Settings_FolderBrowser.Tag
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
        If Not (Settings_OverAllCheckStatus(e.Node) = If(e.Node.Checked, 1, 0)) And e.Node.Nodes.Count > 0 Then e.Node.FirstNode.EnsureVisible()
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

    Private Sub Settings_AfterExpand(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles Settings_LeftView.AfterExpand, Settings_RightView.AfterExpand
        ClickedRightTreeView = (sender.Name = "Settings_RightView")
        For Each Node As TreeNode In e.Node.Nodes
            If Node.Nodes.Count <> 0 Then Continue For
            For Each Dir As String In IO.Directory.GetDirectories(If(ClickedRightTreeView, Handler.GetSetting("To"), Handler.GetSetting("From")) & Node.FullPath)
                Node.Nodes.Add(Dir.Substring(Dir.LastIndexOf("\") + 1))
            Next
        Next
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
            Settings_CheckNodeAndSubNodes(Settings_RightView.SelectedNode, Checked)
            Settings_RightView.SelectedNode.Expand()
        Else
            Settings_CheckNodeAndSubNodes(Settings_LeftView.SelectedNode, Checked)
            Settings_LeftView.SelectedNode.Expand()
        End If
        ProcessingNodes = False
    End Sub

    Sub Settings_CheckNodeAndSubNodes(ByVal Root As TreeNode, ByVal Status As Boolean)
        For Each SubNode As TreeNode In Root.Nodes
            SubNode.Checked = Status
            Settings_CheckNodeAndSubNodes(SubNode, Status)
        Next
        Root.Checked = Status
    End Sub

    Sub Settings_GetCheckedNodes(ByRef NodesList As SortedList(Of String, Boolean), ByVal Node As TreeNode)
        Dim OverAllNodeStatus As Integer = Settings_OverAllCheckStatus(Node)

        If Node.Checked OrElse Node.TreeView.CheckBoxes = False Then
            If OverAllNodeStatus = 1 Then
                NodesList.Add(Node.FullPath & "/", True)
                Exit Sub
            Else
                NodesList.Add(Node.FullPath, False)
            End If
        Else
            If OverAllNodeStatus = 0 Then Exit Sub 'No checked subnode
        End If

        'If node isn't checked
        For NodeId As Integer = 0 To Node.Nodes.Count - 1
            If OverAllNodeStatus = 1 Then
                NodesList.Add(Node.Nodes(NodeId).FullPath & "/", True)
            Else
                Settings_GetCheckedNodes(NodesList, Node.Nodes(NodeId))
            End If
        Next
    End Sub

    Function Settings_OverAllCheckStatus(ByVal Node As TreeNode) As Integer '0 All clear, 1 All checked, -1 different states
        If Not Node.TreeView.CheckBoxes Then Return 1
        If (Node.Nodes.Count = 0) Then Return If(Node.Checked, 1, 0)

        Dim AllChecked As Boolean = True, AllClear As Boolean = True
        For Each SubNode As TreeNode In Node.Nodes
            Dim CurrentStatus As Integer = Settings_OverAllCheckStatus(SubNode)
            AllChecked = AllChecked And (CurrentStatus = 1)
            AllClear = AllClear And (CurrentStatus = 0)
        Next

        If AllChecked Then Return 1
        If AllClear Then Return 0
        Return -1
    End Function

    Sub Settings_ReloadTrees()
        Me.Settings_LeftView.Nodes.Clear()
        Me.Settings_RightView.Nodes.Clear()

        Settings_LeftView.Nodes.Add("") : Settings_RightView.Nodes.Add("")
        For Each Dir As String In IO.Directory.GetDirectories(Handler.GetSetting("From"))
            Settings_LeftView.Nodes(0).Nodes.Add(Dir.Substring(Dir.LastIndexOf("\") + 1))
        Next
        For Each Dir As String In IO.Directory.GetDirectories(Handler.GetSetting("To"))
            Settings_RightView.Nodes(0).Nodes.Add(Dir.Substring(Dir.LastIndexOf("\") + 1))
        Next

        Settings_LeftView.Nodes(0).Expand() : Settings_RightView.Nodes(0).Expand()
        Settings_CheckTree(True)
        Settings_CheckTree(False)
    End Sub

    Sub Settings_CheckTree(ByVal Left As Boolean)
        Select Case Left
            Case True
                Dim BaseNode As TreeNode = Settings_LeftView.Nodes(0)
                For Each CheckedPath As KeyValuePair(Of String, Boolean) In Handler.LeftCheckedNodes
                    Settings_CheckAccordingToPath(BaseNode, New List(Of String)(CheckedPath.Key.Split("\")), CheckedPath.Value)
                Next
            Case False
                Dim BaseNode As TreeNode = Settings_RightView.Nodes(0)
                For Each CheckedPath As KeyValuePair(Of String, Boolean) In Handler.RightCheckedNodes
                    Settings_CheckAccordingToPath(BaseNode, New List(Of String)(CheckedPath.Key.Split("\")), CheckedPath.Value)
                Next
        End Select
    End Sub

    Sub Settings_CheckAccordingToPath(ByVal BaseNode As TreeNode, ByRef Path As List(Of String), ByVal FullCheck As Boolean)
        If Path.Count <> 0 AndAlso Path(0) = "" Then Path.RemoveAt(0)

        If Path.Count = 0 Then
            If FullCheck Then
                Settings_CheckNodeAndSubNodes(BaseNode, True)
            Else
                BaseNode.Checked = True
            End If

            Exit Sub
        End If

        For Each Node As TreeNode In BaseNode.Nodes
            If Node.Text = Path(0) Then
                Node.Expand() : Path.RemoveAt(0)
                Settings_CheckAccordingToPath(Node, Path, FullCheck)
                Exit For
            End If
        Next
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
