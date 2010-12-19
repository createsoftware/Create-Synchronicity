'This file is part of Create Synchronicity.
'
'Create Synchronicity is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
'Create Synchronicity is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with Create Synchronicity.  If not, see <http://www.gnu.org/licenses/>.
'Created by:	Clément Pit--Claudel.
'Web site:		http://synchronicity.sourceforge.net.

Public Class SettingsForm
    Dim Handler As ProfileHandler
    Dim ProcessingNodes As Boolean = False 'Some background activity is occuring, don't record to events.
    Dim InhibitAutocheck As Boolean = False 'Record events, but don't treat them as user input.
    Dim AdvancedSelection As Boolean = False 'Controls recursive selection.
    Dim ClickedRightTreeView As Boolean = False

    'Note:
    'The list called Handler.(left|right)CheckedNodes contains pathes not ending with "*", associated with booleans indicating whether all subfolders /path/ are to be synced.
    'The boolean value is stored as a * appended at the end of the file name.
    'In fact, we have two steps : 
    '   1. Loading and saving the file
    '       1.1 Saving: Booleans calculated as "*"
    '       2.2 Loading: "*" are converted to booleans 
    '   2. Searching the list, were pathes never end with "*"
    'The 'Tag' Property is used as a flag denoting that the treenode originally had all its subnodes checked.
    '
    'Careful: When calling Settings_Update(False), the Handler.Left/RightCheckNodes object is used to hold pathes containing * chars. Therefore, trying to reload the tree from it after invoking Settings_Update(False) cannot be done.

#Region " Events "
    Private Sub Settings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Translation.TranslateControl(Me)
        Settings_CreateDestOption.Visible = ProgramConfig.GetProgramSetting(ConfigOptions.ExpertMode, "False")

        'TODO: Find a way to avoid delays. Trees should be loaded in background (there already is a waiting indicator).
        Settings_Update(True)
        Settings_RightView.Sorted = True : Settings_LeftView.Sorted = True
        Me.Text = String.Format(Translation.Translate("\PROFILE_SETTINGS"), Handler.ProfileName)
    End Sub

    Private Sub Settings_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_CopyAllFilesCheckBox.CheckedChanged, Settings_IncludeFilesOption.CheckedChanged, Settings_ExcludeFilesOption.CheckedChanged
        Settings_Update_Form_Enabled_Components()
    End Sub

    Private Sub Settings_To_FromTextBox_KeyDown(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles Settings_FromTextBox.KeyDown, Settings_ToTextBox.KeyDown
        Interaction.ShowToolTip(CType(sender, Control))
    End Sub

    Private Sub Settings_FromTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_FromTextBox.TextChanged
        BlinkIfInvalidPath(Settings_FromTextBox)
        DisableTree(Settings_LeftReloadButton, Settings_LeftView)
    End Sub

    Private Sub Settings_ToTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_ToTextBox.TextChanged, Settings_CreateDestOption.CheckedChanged
        BlinkIfInvalidPath(Settings_ToTextBox, Settings_CreateDestOption.Checked)
        DisableTree(Settings_RightReloadButton, Settings_RightView)
    End Sub

    Private Sub BlinkIfInvalidPath(ByVal PathBox As TextBox, Optional ByVal ForceWhite As Boolean = False)
        If ForceWhite Or PathBox.Text = "" Or IO.Directory.Exists(ProfileHandler.TranslatePath(PathBox.Text)) Then
            PathBox.BackColor = Drawing.Color.White
        Else
            PathBox.BackColor = Drawing.Color.LightPink
        End If
    End Sub

    Private Sub Settings_SaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_SaveButton.Click
        Settings_Update(False)
        If Handler.ValidateConfigFile() AndAlso Handler.SaveConfigFile() Then Me.Close()
    End Sub

    Private Sub Settings_CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_CancelButton.Click
        Me.Close()
    End Sub

    Private Sub Settings_BrowseLButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_BrowseLButton.Click
        BrowseTo(Translation.Translate("\CHOOSE_SOURCE"), Settings_FromTextBox, Settings_LeftReloadButton, Settings_LeftView)
    End Sub

    Private Sub Settings_BrowseRButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_BrowseRButton.Click
        BrowseTo(Translation.Translate("\CHOOSE_DEST"), Settings_ToTextBox, Settings_RightReloadButton, Settings_RightView)
    End Sub

    Private Sub BrowseTo(ByVal DialogMessage As String, ByRef TextboxField As TextBox, ByVal Btn As Button, ByVal Tree As TreeView)
        Settings_FolderBrowser.Description = DialogMessage
        If Not TextboxField.Text = "" AndAlso IO.Directory.Exists(ProfileHandler.TranslatePath(TextboxField.Text)) Then
            Settings_FolderBrowser.SelectedPath = ProfileHandler.TranslatePath(TextboxField.Text)
        End If
        If Settings_FolderBrowser.ShowDialog = Windows.Forms.DialogResult.OK Then
            If TextboxField.Text.StartsWith("""") Then
                TextboxField.Text = ProfileHandler.TranslatePath_Inverse(Settings_FolderBrowser.SelectedPath)
            Else
                TextboxField.Text = Settings_FolderBrowser.SelectedPath
            End If
            DisableTree(Btn, Tree)
        End If
    End Sub

    Private Sub DisableTree(ByVal Btn As Button, ByVal Tree As TreeView)
        Settings_ReloadButton.BackColor = System.Drawing.Color.Orange
        Btn.Visible = True : Tree.Enabled = False
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
        Settings_ReloadTrees(True)
    End Sub

    Private Sub Settings_LeftRightReloadButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_LeftReloadButton.Click, Settings_RightReloadButton.Click
        Settings_ReloadTrees(False)
    End Sub

    Private Sub Settings_LRMirrorMethodOption_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_LRMirrorMethodOption.CheckedChanged
        Settings_StrictMirrorOption.Visible = Settings_LRMirrorMethodOption.Checked
    End Sub

    Private Sub Settings_View_AfterCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles Settings_RightView.AfterCheck, Settings_LeftView.AfterCheck
        If ProcessingNodes Then Exit Sub
        If Not InhibitAutocheck Then Settings_CheckNodeTree(e.Node.Checked, e.Node)
        If Not (Settings_OverAllCheckStatus(e.Node) = If(e.Node.Checked, 1, 0)) And e.Node.Nodes.Count > 0 Then e.Node.FirstNode.EnsureVisible()
    End Sub

    Private Sub Settings_View_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Settings_RightView.MouseClick, Settings_LeftView.MouseClick
        If e.Button = Windows.Forms.MouseButtons.Right Then
            ClickedRightTreeView = (CType(sender, Control).Name = "Settings_RightView")
            If ClickedRightTreeView Then
                Settings_RightView.SelectedNode = Settings_RightView.GetNodeAt(e.Location)
            Else
                Settings_LeftView.SelectedNode = Settings_LeftView.GetNodeAt(e.Location)
            End If
        End If
    End Sub

    Private Sub Settings_CouldShowTip(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_RightView.MouseEnter, Settings_LeftView.MouseEnter, Settings_FromTextBox.GotFocus, Settings_ToTextBox.GotFocus, Settings_FromTextBox.MouseEnter, Settings_ToTextBox.MouseEnter, Settings_LRMirrorMethodOption.MouseEnter, Settings_LRIncrementalMethodOption.MouseEnter, Settings_TwoWaysIncrementalMethodOption.MouseEnter, Settings_IncludedTypesTextBox.MouseEnter, Settings_ExcludedTypesTextBox.MouseEnter
        Interaction.ShowToolTip(CType(sender, Control))
    End Sub

    Private Sub Settings_ShouldHideTip(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_RightView.MouseLeave, Settings_LeftView.MouseLeave, Settings_FromTextBox.LostFocus, Settings_ToTextBox.LostFocus, Settings_FromTextBox.MouseLeave, Settings_ToTextBox.MouseLeave, Settings_LRMirrorMethodOption.MouseLeave, Settings_LRIncrementalMethodOption.MouseLeave, Settings_TwoWaysIncrementalMethodOption.MouseLeave, Settings_IncludedTypesTextBox.MouseLeave, Settings_ExcludedTypesTextBox.MouseLeave
        Interaction.HideToolTip(CType(sender, Control))
    End Sub

    Private Sub Settings_Bottom_Showtag(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_PropagateUpdatesOption.MouseEnter, Settings_StrictDateComparisonOption.MouseEnter
        Settings_BottomDescLabel.Text = CType(sender, Control).Tag
    End Sub

    Private Sub Settings_Bottom_HideTag(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_PropagateUpdatesOption.MouseLeave, Settings_StrictDateComparisonOption.MouseLeave
        Settings_BottomDescLabel.Text = ""
    End Sub

    Private Sub Settings_AfterExpand(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles Settings_LeftView.AfterExpand, Settings_RightView.AfterExpand
        ClickedRightTreeView = (CType(sender, Control).Name = "Settings_RightView")
        For Each Node As TreeNode In e.Node.Nodes
            If Node.Nodes.Count <> 0 Then Continue For
            Try
                For Each Dir As String In IO.Directory.GetDirectories(ProfileHandler.TranslatePath(Node.FullPath))
                    Dim NewNode As TreeNode = Node.Nodes.Add(Dir.Substring(Dir.LastIndexOf(ConfigOptions.DirSep) + 1))
                    NewNode.Checked = (Node.ToolTipText = "*" And Node.Checked)
                    NewNode.ToolTipText = Node.ToolTipText
                Next
            Catch Ex As Exception
#If DEBUG Then
                Interaction.ShowMsg("Exception while loading tree: " & Ex.ToString)
#End If
            End Try
        Next
    End Sub

    Private Sub Settings_SynchronizeAllSubfoldersMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_SynchronizeFolderAndSubfoldersMenuItem.Click
        Settings_CheckNodeTree(True)
    End Sub

    Private Sub Settings_SynchronizeFilesOnlyMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_SynchronizeFilesOnlyMenuItem.Click
        Settings_CheckNodeTree(False)
        Settings_CheckSelectedNode(True)
    End Sub

    Private Sub Settings_SynchronizeSubFoldersOnlyMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_SynchronizeSubFoldersOnlyMenuItem.Click
        Settings_CheckNodeTree(True)
        Settings_CheckSelectedNode(False)
    End Sub

    Private Sub Settings_DontSynchronizeMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_DontSynchronizeMenuItem.Click
        Settings_CheckNodeTree(False)
    End Sub

    Private Sub Settings_ToggleMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_ToggleMenuItem.Click
        Settings_CheckSelectedNode(Not If(ClickedRightTreeView, Settings_RightView.SelectedNode.Checked, Settings_LeftView.SelectedNode.Checked))
    End Sub

    Private Sub Settings_TwoWaysIncrementalMethodOption_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_TwoWaysIncrementalMethodOption.CheckedChanged
        Settings_RightView.CheckBoxes = Settings_TwoWaysIncrementalMethodOption.Checked
        If Settings_RightView.CheckBoxes Then
            Settings_RightView.ContextMenuStrip = Settings_TreeViewMenuStrip
        Else
            Settings_RightView.ContextMenuStrip = Nothing
        End If

        'When the CheckBoxes' display is switched on, the checked property is not taken into account for the display.
        'That is, if Node.Checked = True but TreeView.CheckBoxes = False, then when Chekboxes = true Node

        'Therefore, re-check the tree (if it has already been loaded)
        If Settings_RightView.CheckBoxes AndAlso Settings_RightView.Nodes.Count > 0 Then
            Settings_LoadCheckState(False) 'LoadTree(Settings_RightView, Settings_ToTextBox.Text & ConfigOptions.DirSep)
        End If
    End Sub

    Public Sub New(ByVal Name As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Handler = New ProfileHandler(Name)
    End Sub

    Private Sub Settings_HelpLink_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Settings_HelpLink.Click
        Interaction.StartProcess("http://synchronicity.sourceforge.net/help.html")
    End Sub
#End Region

#Region " Form and TreeView manipulation "
    Sub Settings_Update_Form_Enabled_Components()
        Settings_IncludeExcludeLayoutPanel.Enabled = Not Settings_CopyAllFilesCheckBox.Checked
        Settings_IncludedTypesTextBox.Enabled = Settings_IncludeFilesOption.Checked
        Settings_ExcludedTypesTextBox.Enabled = Settings_ExcludeFilesOption.Checked
    End Sub

    Sub Settings_CheckSelectedNode(ByVal Checked As Boolean)
        InhibitAutocheck = True
        If ClickedRightTreeView Then
            Settings_RightView.SelectedNode.Checked = Checked
        Else
            Settings_LeftView.SelectedNode.Checked = Checked
        End If
        InhibitAutocheck = False
    End Sub

    Sub Settings_CheckNodeTree(ByVal Checked As Boolean, Optional ByVal NodeParam As TreeNode = Nothing)
        ProcessingNodes = True
        If ClickedRightTreeView Then
            Settings_Inner_CheckNodeTree(If(NodeParam Is Nothing, Settings_RightView.SelectedNode, NodeParam), Checked)
        Else
            Settings_Inner_CheckNodeTree(If(NodeParam Is Nothing, Settings_LeftView.SelectedNode, NodeParam), Checked)
        End If
        ProcessingNodes = False
    End Sub

    Sub Settings_Inner_CheckNodeTree(ByVal Root As TreeNode, ByVal Status As Boolean)
        Root.ToolTipText = "*"
        For Each SubNode As TreeNode In Root.Nodes
            SubNode.Checked = Status
            Settings_Inner_CheckNodeTree(SubNode, Status)
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

    Sub Settings_SetRootPathDisplay(ByVal Show As Boolean)
        If Show Then
            If Not Settings_FromTextBox.Text = "" And Settings_LeftView.Nodes.Count > 0 Then Settings_LeftView.Nodes(0).Text = Settings_FromTextBox.Text
            If Not Settings_ToTextBox.Text = "" And Settings_RightView.Nodes.Count > 0 Then Settings_RightView.Nodes(0).Text = Settings_ToTextBox.Text
        Else
            If Settings_LeftView.Nodes.Count > 0 Then Settings_LeftView.Nodes(0).Text = ""
            If Settings_RightView.Nodes.Count > 0 Then Settings_RightView.Nodes(0).Text = ""
        End If
    End Sub

    Sub Settings_ReloadTrees(Optional ByVal AllowFullReload As Boolean = False)
        Static CurrentLeft As String = "-1" 'Initiate to an invalid path value to force reloading.
        Static CurrentRight As String = "-1"

        Settings_ReloadButton.BackColor = System.Drawing.SystemColors.Control
        Settings_ReloadButton.Enabled = False : Settings_SaveButton.Enabled = False
        Settings_Loading.Visible = True : InhibitAutocheck = True

        'No changes: reload all trees if AllowFullReload is true (middle button clicked)
        'Otherwise: Reload trees where paths have changed.
        'Check for changes *before* normalizing the paths.
        Dim FullReload As Boolean = (AllowFullReload And CurrentLeft = Settings_FromTextBox.Text And CurrentRight = Settings_ToTextBox.Text)

        Settings_Cleanup_Paths()
        Settings_LeftView.Enabled = True : Settings_RightView.Enabled = True

        If FullReload Or CurrentLeft <> Settings_FromTextBox.Text Then
            LoadTree(Settings_LeftView, Settings_FromTextBox.Text)
        End If
        If FullReload Or CurrentRight <> Settings_ToTextBox.Text Then
            LoadTree(Settings_RightView, Settings_ToTextBox.Text, Settings_CreateDestOption.Checked)
        End If

        Settings_LeftReloadButton.Visible = Not Settings_LeftView.Enabled
        Settings_RightReloadButton.Visible = Not Settings_RightView.Enabled

        Settings_SetRootPathDisplay(True)
        Settings_Loading.Visible = False : InhibitAutocheck = False
        Settings_ReloadButton.Enabled = True : Settings_SaveButton.Enabled = True

        CurrentLeft = Settings_FromTextBox.Text
        CurrentRight = Settings_ToTextBox.Text
    End Sub

    Sub LoadTree(ByVal Tree As TreeView, ByVal OriginalPath As String, Optional ByVal ForceLoad As Boolean = False)
        Tree.Nodes.Clear()

        Dim Path As String = ProfileHandler.TranslatePath(OriginalPath) & ConfigOptions.DirSep
        Tree.Enabled = OriginalPath <> "" AndAlso (ForceLoad OrElse IO.Directory.Exists(Path)) 'Linux
        If Tree.Enabled Then
            Tree.BackColor = Drawing.Color.White
            Tree.Nodes.Add("")
            Settings_SetRootPathDisplay(True) 'Needed for the FullPath method, see tracker #3006324
            If Not ForceLoad Then
                Try
                    For Each Dir As String In IO.Directory.GetDirectories(Path)
                        Application.DoEvents()
                        Tree.Nodes(0).Nodes.Add(Dir.Substring(Dir.LastIndexOf(ConfigOptions.DirSep) + 1))
                    Next

                    Tree.Nodes(0).Expand()
                    Settings_LoadCheckState(Tree.Name = "Settings_LeftView")
                Catch Ex As Exception
                    Tree.Nodes.Clear()
                    Tree.Enabled = False
                End Try
            End If
        Else
            Tree.BackColor = Drawing.Color.LightGray
        End If
    End Sub

    Sub Settings_LoadCheckState(ByVal Left As Boolean)
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
                Settings_Inner_CheckNodeTree(BaseNode, True)
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
        Settings_Cleanup_Paths()

        'Careful: Using .ToString here would break the ByRef passing of the second argument.
        Handler.SetSetting(ConfigOptions.Source, Settings_FromTextBox.Text, LoadToForm)
        Handler.SetSetting(ConfigOptions.Destination, Settings_ToTextBox.Text, LoadToForm)
        Handler.SetSetting(ConfigOptions.IncludedTypes, Settings_IncludedTypesTextBox.Text, LoadToForm)
        Handler.SetSetting(ConfigOptions.ExcludedTypes, Settings_ExcludedTypesTextBox.Text, LoadToForm)
        Handler.SetSetting(ConfigOptions.ReplicateEmptyDirectories, Settings_ReplicateEmptyDirectoriesOption.Checked, LoadToForm)
        Handler.SetSetting(ConfigOptions.MayCreateDestination, Settings_CreateDestOption.Checked, LoadToForm)
        Handler.SetSetting(ConfigOptions.StrictDateComparison, Settings_StrictDateComparisonOption.Checked, LoadToForm)
        Handler.SetSetting(ConfigOptions.PropagateUpdates, Settings_PropagateUpdatesOption.Checked, LoadToForm)
        Handler.SetSetting(ConfigOptions.StrictMirror, Settings_StrictMirrorOption.Checked, LoadToForm)
        Handler.SetSetting(ConfigOptions.TimeOffset, Settings_TimeOffset.Value, LoadToForm)
        'Fuzzy DST setting is among the hidden settings, thus not added here

        'Note: Behaves correctly when no radio button is checked, although CopyAllFiles is unchecked.
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
                Settings_SetRootPathDisplay(False)
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
                Settings_SetRootPathDisplay(True)
            Case True
                Settings_ReloadTrees()
        End Select

        If LoadToForm Then Settings_Update_Form_Enabled_Components()
    End Sub

    Sub Settings_Cleanup_Paths() 'LINUX: Careful with root path.
        Settings_FromTextBox.Text = Settings_FromTextBox.Text.TrimEnd(New Char() {ConfigOptions.DirSep, " "})
        Settings_ToTextBox.Text = Settings_ToTextBox.Text.TrimEnd(New Char() {ConfigOptions.DirSep, " "})
    End Sub

    Function Settings_GetString(ByRef Table As Dictionary(Of String, Boolean)) As String
        Dim ListString As New System.Text.StringBuilder
        For Each Node As String In Table.Keys
            ListString.Append(Node).Append(";")
        Next
        Return ListString.ToString
    End Function
#End Region
End Class
