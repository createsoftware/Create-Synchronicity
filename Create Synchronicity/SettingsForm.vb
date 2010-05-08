'This file is part of Create Synchronicity.
'
'Create Synchronicity is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
'Create Synchronicity is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with Create Synchronicity.  If not, see <http://www.gnu.org/licenses/>.
'Created by:	Clément Pit--Claudel.
'Web site:		http://synchronicity.sourceforge.net.

Public Class SettingsForm
    Dim Handler As ProfileHandler
    Dim ProcessingNodes As Boolean = False
    Dim ClickedRightTreeView As Boolean = False

    Dim Translation As LanguageHandler = LanguageHandler.GetSingleton
    Dim ProgramConfig As ConfigHandler = ConfigHandler.GetSingleton
    'Note:
    'The list called Handler.(left|right)CheckedNodes contains pathes not ending with "*", associated with booleans indicating whether all subfolders /path/ are to be synced.
    'The boolean value is stored as a * appended at the end of the file name.
    'In fact, we have two steps : 
    '   1. Loading and saving the file
    '       1.1 Saving: Booleans calculated as "*"
    '       2.2 Loading: "*" are converted to booleans 
    '   2. Searching the list, were pathes never end with "*"
    'The 'Tag' Property is used as a flag denoting that the treenode originally had all its subnodes checked.

#Region " Events "
    Private Sub Settings_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_CopyAllFilesCheckBox.CheckedChanged, Settings_IncludeFilesOption.CheckedChanged, Settings_ExcludeFilesOption.CheckedChanged
        Settings_Update_Form_Enabled_Components()
    End Sub

    Private Sub Settings_From_To_TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_FromTextBox.TextChanged, Settings_ToTextBox.TextChanged
        Settings_ReloadButton.BackColor = System.Drawing.Color.Orange
    End Sub

    Private Sub Settings_SaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_SaveButton.Click
        Settings_Update(False)
        If Handler.ValidateConfigFile() Then
            If Handler.SaveConfigFile() Then Me.Close()
        End If
    End Sub

    Private Sub Settings_CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_CancelButton.Click
        Me.Close()
    End Sub

    Private Sub Settings_BrowseLButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_BrowseLButton.Click
        Settings_FolderBrowser.Description = Translation.Translate("\CHOOSE_SOURCE")
        If Settings_FolderBrowser.ShowDialog = Windows.Forms.DialogResult.OK Then Settings_FromTextBox.Text = Settings_FolderBrowser.SelectedPath
    End Sub

    Private Sub Settings_BrowseRButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_BrowseRButton.Click
        Settings_FolderBrowser.Description = Translation.Translate("\CHOOSE_DEST")
        If Settings_FolderBrowser.ShowDialog = Windows.Forms.DialogResult.OK Then Settings_ToTextBox.Text = Settings_FolderBrowser.SelectedPath
    End Sub

    Private Sub Settings_SwapButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_SwapButton.Click
        If Interaction.ShowMsg(Translation.Translate("\WARNING_SWAP"), "\WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = Windows.Forms.DialogResult.Yes Then
            Dim Settings_FromTextBox_Text As String = Settings_FromTextBox.Text
            Settings_FromTextBox.Text = Settings_ToTextBox.Text
            Settings_ToTextBox.Text = Settings_FromTextBox_Text
            Settings_ReloadTrees()
        End If
    End Sub

    Private Sub Settings_ReloadButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_ReloadButton.Click
        Settings_ReloadTrees()
        Settings_ReloadButton.BackColor = System.Drawing.SystemColors.Control
    End Sub

    Private Sub Settings_MethodOption_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_TwoWaysIncrementalMethodOption.MouseEnter, Settings_LRMirrorMethodOption.MouseEnter, Settings_LRIncrementalMethodOption.MouseEnter
        Settings_DescriptionLabel.Text = sender.Tag.Replace("%s", sender.Text)
    End Sub

    Private Sub Settings_MethodOption_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_TwoWaysIncrementalMethodOption.MouseLeave, Settings_LRMirrorMethodOption.MouseLeave, Settings_LRIncrementalMethodOption.MouseLeave
        Settings_DescriptionLabel.Text = Settings_DescriptionLabel.Tag.ToString
    End Sub

    Private Sub Settings_LRMirrorMethodOption_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_LRMirrorMethodOption.CheckedChanged
        Settings_StrictMirrorOption.Visible = Settings_LRMirrorMethodOption.Checked
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

    Private Sub Settings_Views_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_RightView.MouseEnter, Settings_LeftView.MouseEnter
        Settings_Tips.Visible = True
    End Sub

    Private Sub Settings_Views_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_RightView.MouseLeave, Settings_LeftView.MouseLeave
        Settings_Tips.Visible = False
    End Sub

    Private Sub Settings_Bottom_Showtag(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_PropagateUpdatesOption.MouseEnter, Settings_ComputeHashOption.MouseEnter, Settings_StrictDateComparisonOption.MouseEnter
        Settings_BottomDescLabel.Text = sender.Tag.ToString
    End Sub

    Private Sub Settings_Bottom_HideTag(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_PropagateUpdatesOption.MouseLeave, Settings_ComputeHashOption.MouseLeave, Settings_StrictDateComparisonOption.MouseLeave
        Settings_BottomDescLabel.Text = ""
    End Sub

    Private Sub Settings_AfterExpand(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles Settings_LeftView.AfterExpand, Settings_RightView.AfterExpand
        ClickedRightTreeView = (sender.Name = "Settings_RightView")
        For Each Node As TreeNode In e.Node.Nodes
            If Node.Nodes.Count <> 0 Then Continue For
            Try
                For Each Dir As String In IO.Directory.GetDirectories(If(ClickedRightTreeView, Settings_ToTextBox.Text, Settings_FromTextBox.Text) & Node.FullPath)
                    Dim NewNode As TreeNode = Node.Nodes.Add(Dir.Substring(Dir.LastIndexOf(ConfigOptions.DirSep) + 1))
                    NewNode.Checked = (Node.ToolTipText = "*" And Node.Checked)
                    NewNode.ToolTipText = Node.ToolTipText
                Next
            Catch Ex As Exception
            End Try
        Next
    End Sub

    Private Sub Settings_SynchronizeAllSubfoldersMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_SynchronizeAllSubfoldersMenuItem.Click
        Settings_Update_CheckStatus(True)
    End Sub

    Private Sub Settings_DontSynchronizeSubfoldersMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_DontSynchronizeSubfoldersMenuItem.Click
        Settings_Update_CheckStatus(False)
    End Sub

    Private Sub Settings_TwoWaysIncrementalMethodOption_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_TwoWaysIncrementalMethodOption.CheckedChanged
        Settings_RightView.CheckBoxes = Settings_TwoWaysIncrementalMethodOption.Checked

        'When the CheckBoxes' display is switched on, the checked property is not taken into account for the display.
        'That is, ir Node.Checked = True but TreeView.CheckBoxes = False, then when Chekboxes = true Node

        'Therefore, re-check the tree (if it has already been loaded)
        If Settings_RightView.CheckBoxes AndAlso Settings_RightView.Nodes.Count > 0 Then
            Settings_CheckTree(False) 'LoadTree(Settings_RightView, Settings_ToTextBox.Text & ConfigOptions.DirSep)
        End If
    End Sub

    Public Sub New(ByVal Name As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Handler = New ProfileHandler(Name)
    End Sub
#End Region

#Region " Form and TreeView manipulation "
    Sub Settings_Update_Form_Enabled_Components()
        Settings_IncludeExcludeLayoutPanel.Enabled = Not Settings_CopyAllFilesCheckBox.Checked
        Settings_IncludedTypesTextBox.Enabled = Settings_IncludeFilesOption.Checked
        Settings_ExcludedTypesTextBox.Enabled = Settings_ExcludeFilesOption.Checked
    End Sub

    Sub Settings_Update_CheckStatus(ByVal Checked As Boolean)
        ProcessingNodes = True
        If ClickedRightTreeView Then
            Settings_CheckNodeAndSubNodes(Settings_RightView.SelectedNode, Checked)
        Else
            Settings_CheckNodeAndSubNodes(Settings_LeftView.SelectedNode, Checked)
        End If
        ProcessingNodes = False
    End Sub

    Sub Settings_CheckNodeAndSubNodes(ByVal Root As TreeNode, ByVal Status As Boolean)
        Root.ToolTipText = "*"
        For Each SubNode As TreeNode In Root.Nodes
            SubNode.Checked = Status
            Settings_CheckNodeAndSubNodes(SubNode, Status)
        Next
        Root.Checked = Status
    End Sub

    Sub Settings_BuildCheckedNodesList(ByRef NodesList As Dictionary(Of String, Boolean), ByVal Node As TreeNode)
        Dim OverAllNodeStatus As Integer = Settings_OverAllCheckStatus(Node)

        If Node.Checked OrElse Node.TreeView.CheckBoxes = False Then
            If OverAllNodeStatus = 1 Then
                NodesList.Add(Node.FullPath & "*", True)
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
                NodesList.Add(Node.Nodes(NodeId).FullPath & "*", True)
            Else
                Settings_BuildCheckedNodesList(NodesList, Node.Nodes(NodeId))
            End If
        Next
    End Sub

    Function Settings_OverAllCheckStatus(ByVal Node As TreeNode) As Integer '0 All clear, 1 All checked, -1 different states
        If Not Node.TreeView.CheckBoxes Then Return 1
        If (Node.Nodes.Count = 0) Then Return If(Node.Checked, 1, 0)

        Dim AllChecked As Boolean = Node.Checked, AllClear As Boolean = Not Node.Checked
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
        LoadTree(Settings_LeftView, If(Settings_FromTextBox.Text = "", "", Settings_FromTextBox.Text & ConfigOptions.DirSep)) 'TODO: Ok to return empty path?
        LoadTree(Settings_RightView, If(Settings_ToTextBox.Text = "", "", Settings_ToTextBox.Text & ConfigOptions.DirSep))
    End Sub

    Sub LoadTree(ByVal Tree As TreeView, ByVal Path As String)
        Tree.Nodes.Clear()

        Tree.Enabled = Path <> "" AndAlso IO.Directory.Exists(Path)  'TODO: Check if empty path change is truly ok. 'Potential problem for moving to linux -> / is a valid path.
        If Tree.Enabled Then
            Tree.BackColor = Drawing.Color.White
            Tree.Nodes.Add("")
            Try
                For Each Dir As String In IO.Directory.GetDirectories(Path)
                    Tree.Nodes(0).Nodes.Add(Dir.Substring(Dir.LastIndexOf(ConfigOptions.DirSep) + 1))
                Next

                Tree.Nodes(0).Expand()
                Settings_CheckTree(Tree.Name = "Settings_LeftView")
            Catch Ex As Exception
                Tree.Nodes.Clear()
                Tree.Enabled = False
            End Try
        Else
            Tree.BackColor = Drawing.Color.LightGray
        End If
    End Sub

    Sub Settings_CheckTree(ByVal Left As Boolean)
        Select Case Left
            Case True
                Dim BaseNode As TreeNode = Settings_LeftView.Nodes(0)
                For Each CheckedPath As KeyValuePair(Of String, Boolean) In Handler.LeftCheckedNodes
                    Settings_CheckAccordingToPath(BaseNode, New List(Of String)(CheckedPath.Key.Split(ConfigOptions.DirSep)), CheckedPath.Value)
                Next
            Case False
                Dim BaseNode As TreeNode = Settings_RightView.Nodes(0)
                For Each CheckedPath As KeyValuePair(Of String, Boolean) In Handler.RightCheckedNodes
                    Settings_CheckAccordingToPath(BaseNode, New List(Of String)(CheckedPath.Key.Split(ConfigOptions.DirSep)), CheckedPath.Value)
                Next
        End Select
    End Sub

    Sub Settings_CheckAccordingToPath(ByVal BaseNode As TreeNode, ByRef Path As List(Of String), ByVal FullCheck As Boolean)
        If Path.Count <> 0 AndAlso Path(0) = "" Then Path.RemoveAt(0)

        If Path.Count = 0 Then
            If FullCheck Then
                ProcessingNodes = True
                Settings_CheckNodeAndSubNodes(BaseNode, True)
                BaseNode.Collapse()
                ProcessingNodes = False
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
        Settings_FromTextBox.Text = Settings_FromTextBox.Text.TrimEnd(ConfigOptions.DirSep)
        Settings_ToTextBox.Text = Settings_ToTextBox.Text.TrimEnd(ConfigOptions.DirSep)

        Handler.SetSetting(ConfigOptions.Source, Settings_FromTextBox.Text, LoadToForm)
        Handler.SetSetting(ConfigOptions.Destination, Settings_ToTextBox.Text, LoadToForm)
        Handler.SetSetting(ConfigOptions.IncludedTypes, Settings_IncludedTypesTextBox.Text, LoadToForm)
        Handler.SetSetting(ConfigOptions.ExcludedTypes, Settings_ExcludedTypesTextBox.Text, LoadToForm)
        Handler.SetSetting(ConfigOptions.ReplicateEmptyDirectories, Settings_ReplicateEmptyDirectoriesOption.Checked.ToString, LoadToForm)
        Handler.SetSetting(ConfigOptions.ComputeHash, Settings_ComputeHashOption.Checked, LoadToForm)
        Handler.SetSetting(ConfigOptions.StrictDateComparison, Settings_StrictDateComparisonOption.Checked, LoadToForm)
        Handler.SetSetting(ConfigOptions.PropagateUpdates, Settings_PropagateUpdatesOption.Checked, LoadToForm)
        Handler.SetSetting(ConfigOptions.StrictMirror, Settings_StrictMirrorOption.Checked, LoadToForm)
        Handler.SetSetting(ConfigOptions.TimeOffset, Settings_TimeOffset.Value, LoadToForm)

        Dim Restrictions As String = (If(Settings_CopyAllFilesCheckBox.Checked, 0, 1) * (If(Settings_IncludeFilesOption.Checked, 1, 0) + 2 * If(Settings_ExcludeFilesOption.Checked, 1, 0))).ToString
        Dim Method As String = (If(Settings_LRIncrementalMethodOption.Checked, 1, 0) * 1 + If(Settings_TwoWaysIncrementalMethodOption.Checked, 1, 0) * 2).ToString

        Select Case LoadToForm
            Case False
                Handler.SetSetting(ConfigOptions.Method, Method)
                Handler.SetSetting(ConfigOptions.Restrictions, Restrictions)
            Case True
                Select Case Handler.GetSetting(ConfigOptions.Method)
                    Case "1"
                        Settings_LRIncrementalMethodOption.Checked = True
                    Case "2"
                        Settings_TwoWaysIncrementalMethodOption.Checked = True
                    Case Else
                        Settings_LRMirrorMethodOption.Checked = True
                End Select

                Settings_CopyAllFilesCheckBox.Checked = False
                Select Case Handler.GetSetting(ConfigOptions.Restrictions)
                    Case "1"
                        Settings_IncludeFilesOption.Checked = True
                    Case "2"
                        Settings_ExcludeFilesOption.Checked = True
                    Case Else
                        Settings_CopyAllFilesCheckBox.Checked = True
                End Select
        End Select

        Select Case LoadToForm
            Case False
                If Settings_LeftView.Enabled Then
                    Handler.LeftCheckedNodes.Clear()
                    Settings_BuildCheckedNodesList(Handler.LeftCheckedNodes, Settings_LeftView.Nodes(0))
                    Handler.SetSetting(ConfigOptions.LeftSubFolders, Settings_GetString(Handler.LeftCheckedNodes))
                End If

                If Settings_RightView.Enabled Then
                    If Settings_RightView.CheckBoxes Or Handler.GetSetting(ConfigOptions.RightSubFolders) Is Nothing Then
                        Handler.RightCheckedNodes.Clear()
                        Settings_BuildCheckedNodesList(Handler.RightCheckedNodes, Settings_RightView.Nodes(0))
                        Handler.SetSetting(ConfigOptions.RightSubFolders, Settings_GetString(Handler.RightCheckedNodes))
                    End If
                End If
            Case True
                Settings_ReloadTrees()
        End Select

        If LoadToForm Then Settings_Update_Form_Enabled_Components()
    End Sub

    Function Settings_GetString(ByRef Table As Dictionary(Of String, Boolean)) As String
        Dim ListString As String = ""
        For Each Node As String In Table.Keys
            ListString &= Node & ";"
        Next
        Return ListString
    End Function

    Private Sub Settings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Settings_Tips.BringToFront()
        Translation.TranslateControl(Me)

        Settings_Update(True)
        Me.Text = String.Format(Translation.Translate("\PROFILE_SETTINGS"), Handler.ProfileName)
    End Sub
#End Region
End Class
