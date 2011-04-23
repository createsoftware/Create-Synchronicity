'This file is part of Create Synchronicity.
'
'Create Synchronicity is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
'Create Synchronicity is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with Create Synchronicity.  If not, see <http://www.gnu.org/licenses/>.
'Created by:	Clément Pit--Claudel.
'Web site:		http://synchronicity.sourceforge.net.

Option Strict On

Public Class SettingsForm
    Dim Handler As ProfileHandler
    Dim ProcessingNodes As Boolean = False 'Some background activity is occuring, don't record events.
    Dim InhibitAutocheck As Boolean = False 'Record events, but don't treat them as user input.
    Dim ClickedRightTreeView As Boolean = False

    Dim NewProfile As Boolean
    Dim PrevLeft As String = "-1" 'Initiate to an invalid path value to force reloading.
    Dim PrevRight As String = "-1" 'These values are used to check whether the folder tree should be reloaded.

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
    'Careful: When calling Update(False), the Handler.Left/RightCheckNodes object is used to hold pathes containing * chars. Therefore, trying to reload the tree from it after invoking Update(False) cannot be done.

    'Path handling: Always trim traliing path separator chars: it makes everything much simpler. Only exception: '/' in Linux

#Region " Events "
    Public Sub New(ByVal Name As String, ByVal _NewProfile As Boolean)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
#If CONFIG = "Linux" Then
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
#End If

        ' Add any initialization after the InitializeComponent() call.
        Handler = New ProfileHandler(Name)
        NewProfile = _NewProfile
    End Sub

    Private Sub Settings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Translation.TranslateControl(Me)
        Translation.TranslateControl(ExpertMenu)
        LeftView.PathSeparator = ConfigOptions.DirSep
        RightView.PathSeparator = ConfigOptions.DirSep
        MoreLabel.Visible = ProgramConfig.GetProgramSetting(Of Boolean)(ConfigOptions.ExpertMode, False)

        'TODO: Find a way to avoid delays. Trees should be loaded in background (there already is a waiting indicator).
        If Not NewProfile Then UpdateSettings(True)
        CheckSettings()
        RightView.Sorted = True : LeftView.Sorted = True
        Me.Text = String.Format(Translation.Translate("\PROFILE_SETTINGS"), Handler.ProfileName)
    End Sub

    Private Sub CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyAllFilesCheckBox.CheckedChanged, IncludeFilesOption.CheckedChanged, ExcludeFilesOption.CheckedChanged
        Update_Form_Enabled_Components()
    End Sub

    Private Sub To_FromTextBox_KeyDown(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles FromTextBox.KeyDown, ToTextBox.KeyDown
        Interaction.ShowToolTip(CType(sender, Control))
    End Sub

    Private Sub To_FromTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FromTextBox.TextChanged, ToTextBox.TextChanged
        CheckSettings()
    End Sub

    Private Sub CreateDestOption_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreateDestOption.CheckedChanged
        ReloadTrees(False, True)
        CheckSettings()
    End Sub


    Private Sub SaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton.Click
        UpdateSettings(False)
        If Handler.ValidateConfigFile() AndAlso Handler.SaveConfigFile() Then Me.Close()
    End Sub

    Private Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelBtn.Click
        Me.Close()
    End Sub

    Private Sub BrowseLButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BrowseLButton.Click
        BrowseTo(Translation.Translate("\CHOOSE_SOURCE"), FromTextBox)
    End Sub

    Private Sub BrowseRButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BrowseRButton.Click
        BrowseTo(Translation.Translate("\CHOOSE_DEST"), ToTextBox)
    End Sub

    Private Sub BrowseTo(ByVal DialogMessage As String, ByRef TextboxField As TextBox)
        FolderBrowser.Description = DialogMessage
        If Not TextboxField.Text = "" AndAlso IO.Directory.Exists(ProfileHandler.TranslatePath(TextboxField.Text)) Then
            FolderBrowser.SelectedPath = ProfileHandler.TranslatePath(TextboxField.Text)
        End If
        If FolderBrowser.ShowDialog = Windows.Forms.DialogResult.OK Then
            If TextboxField.Text.StartsWith("""") Then
                TextboxField.Text = ProfileHandler.TranslatePath_Inverse(FolderBrowser.SelectedPath)
            Else
                TextboxField.Text = FolderBrowser.SelectedPath
            End If
        End If
    End Sub

    Private Sub SwapButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SwapButton.Click
        If Interaction.ShowMsg(Translation.Translate("\WARNING_SWAP"), "\WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = Windows.Forms.DialogResult.Yes Then
            Dim FromTextBox_Text As String = FromTextBox.Text 'TODO: Better swapping?
            FromTextBox.Text = ToTextBox.Text
            ToTextBox.Text = FromTextBox_Text
            ReloadTrees(False)
        End If
    End Sub

    Private Sub ReloadButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReloadButton.Click
        ReloadTrees(True)
    End Sub

    Private Sub LeftRightReloadButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LeftReloadButton.Click, RightReloadButton.Click
        ReloadTrees(False)
    End Sub

    Private Sub LRMirrorMethodOption_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LRMirrorMethodOption.CheckedChanged
        StrictMirrorOption.Visible = LRMirrorMethodOption.Checked
    End Sub

    Private Sub View_AfterCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles RightView.AfterCheck, LeftView.AfterCheck
        If ProcessingNodes Then Exit Sub
        If Not InhibitAutocheck Then CheckNodeTree(e.Node.Checked, e.Node) 'NB: Expanding a node can lead here, but in this case e.Node has no children.
        If Not (OverAllCheckStatus(e.Node) = If(e.Node.Checked, 1, 0)) And e.Node.Nodes.Count > 0 Then e.Node.FirstNode.EnsureVisible()
    End Sub

    Private Sub View_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles RightView.MouseClick, LeftView.MouseClick
        If e.Button = Windows.Forms.MouseButtons.Right Then
            ClickedRightTreeView = (CType(sender, Control).Name = "RightView")
            If ClickedRightTreeView Then
                RightView.SelectedNode = RightView.GetNodeAt(e.Location)
            Else
                LeftView.SelectedNode = LeftView.GetNodeAt(e.Location)
            End If
        End If
    End Sub

    Private Sub CouldShowTip(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RightView.MouseEnter, LeftView.MouseEnter, FromTextBox.GotFocus, ToTextBox.GotFocus, FromTextBox.MouseEnter, ToTextBox.MouseEnter, LRMirrorMethodOption.MouseEnter, LRIncrementalMethodOption.MouseEnter, TwoWaysIncrementalMethodOption.MouseEnter, IncludedTypesTextBox.MouseEnter, ExcludedTypesTextBox.MouseEnter
        Interaction.ShowToolTip(CType(sender, Control))
    End Sub

    Private Sub ShouldHideTip(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RightView.MouseLeave, LeftView.MouseLeave, FromTextBox.LostFocus, ToTextBox.LostFocus, FromTextBox.MouseLeave, ToTextBox.MouseLeave, LRMirrorMethodOption.MouseLeave, LRIncrementalMethodOption.MouseLeave, TwoWaysIncrementalMethodOption.MouseLeave, IncludedTypesTextBox.MouseLeave, ExcludedTypesTextBox.MouseLeave
        Interaction.HideToolTip(CType(sender, Control))
    End Sub

    Private Sub Bottom_Showtag(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PropagateUpdatesOption.MouseEnter, StrictDateComparisonOption.MouseEnter
        BottomDescLabel.Text = CStr(CType(sender, Control).Tag)
    End Sub

    Private Sub Bottom_HideTag(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PropagateUpdatesOption.MouseLeave, StrictDateComparisonOption.MouseLeave
        BottomDescLabel.Text = ""
    End Sub

    Private Sub AfterExpand(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles LeftView.AfterExpand, RightView.AfterExpand
        ClickedRightTreeView = (DirectCast(sender, Control).Name = "RightView")
        For Each Node As TreeNode In e.Node.Nodes
            If Node.Nodes.Count <> 0 Then Continue For 'Node already loaded
            Try
                For Each Dir As String In IO.Directory.GetDirectories(ProfileHandler.TranslatePath(Node.FullPath))
                    Dim NewNode As TreeNode = Node.Nodes.Add(GetFileOrFolderName(Dir))
                    NewNode.Checked = (Node.ToolTipText = "*" And Node.Checked)
                    NewNode.ToolTipText = Node.ToolTipText
                Next
            Catch Ex As Exception 'Typically UnauthorizedAccess exceptions.
#If DEBUG Then
                Interaction.ShowMsg("Exception while loading tree: " & Ex.ToString)
#End If
            End Try
        Next
    End Sub

    Private Sub SynchronizeAllSubfoldersMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SynchronizeFolderAndSubfoldersMenuItem.Click
        CheckNodeTree(True)
    End Sub

    Private Sub SynchronizeFilesOnlyMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SynchronizeFilesOnlyMenuItem.Click
        CheckNodeTree(False)
        CheckSelectedNode(True)
    End Sub

    Private Sub SynchronizeSubFoldersOnlyMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SynchronizeSubFoldersOnlyMenuItem.Click
        CheckNodeTree(True)
        CheckSelectedNode(False)
    End Sub

    Private Sub DontSynchronizeMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DontSynchronizeMenuItem.Click
        CheckNodeTree(False)
    End Sub

    Private Sub ToggleMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToggleMenuItem.Click
        CheckSelectedNode(Not If(ClickedRightTreeView, RightView.SelectedNode.Checked, LeftView.SelectedNode.Checked))
    End Sub

    Private Sub TwoWaysIncrementalMethodOption_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TwoWaysIncrementalMethodOption.CheckedChanged
        RightView.CheckBoxes = TwoWaysIncrementalMethodOption.Checked
        If RightView.CheckBoxes Then
            RightView.ContextMenuStrip = TreeViewMenuStrip
        Else
            RightView.ContextMenuStrip = Nothing
        End If

        'When the CheckBoxes' display is switched on, the checked property is not taken into account for the display.
        'Therefore, re-check the tree (if it has already been loaded)
        If RightView.CheckBoxes AndAlso RightView.Nodes.Count > 0 Then
            LoadCheckState(RightView, Handler.RightCheckedNodes) 'InhibitAutoCheck? -> Enabled in LoadCheckState anyway.
        End If
    End Sub

    Private Sub HelpLink_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HelpLink.Click
        Interaction.StartProcess(ConfigOptions.Website & "settings-help.html")
    End Sub
#End Region

#Region " Form and TreeView manipulation "
    Private Sub Update_Form_Enabled_Components()
        IncludeExcludeLayoutPanel.Enabled = Not CopyAllFilesCheckBox.Checked
        IncludedTypesTextBox.Enabled = IncludeFilesOption.Checked
        ExcludedTypesTextBox.Enabled = ExcludeFilesOption.Checked
    End Sub

    Private Sub CheckSettings()
        CheckPath(FromTextBox, False)
        CheckPath(ToTextBox, CreateDestOption.Checked)

        ToggleTree(PrevLeft, FromTextBox, LeftView, LeftReloadButton)
        ToggleTree(PrevRight, ToTextBox, RightView, RightReloadButton)

        SaveButton.Enabled = LeftView.Enabled And RightView.Enabled
        ReloadButton.BackColor = If(SaveButton.Enabled, System.Drawing.SystemColors.Control, System.Drawing.Color.Orange)
    End Sub

    Private Shared Sub ToggleTree(ByVal PrevPath As String, ByVal Box As TextBox, ByVal Tree As TreeView, ByVal Btn As Button)
        Tree.Enabled = (Cleanup(Box.Text) = PrevPath)
        Btn.Visible = Not Tree.Enabled
    End Sub

    Private Shared Sub CheckPath(ByVal PathBox As TextBox, ByVal Force As Boolean)
        If PathBox.Text = "" Or Force OrElse IO.Directory.Exists(ProfileHandler.TranslatePath(PathBox.Text)) Then
            PathBox.BackColor = Drawing.Color.White
        Else
            PathBox.BackColor = Drawing.Color.LightPink
        End If
    End Sub

    Private Sub CheckSelectedNode(ByVal Checked As Boolean)
        InhibitAutocheck = True
        If ClickedRightTreeView Then
            RightView.SelectedNode.Checked = Checked
        Else
            LeftView.SelectedNode.Checked = Checked
        End If
        InhibitAutocheck = False
    End Sub

    Private Sub CheckNodeTree(ByVal Checked As Boolean, Optional ByVal NodeParam As TreeNode = Nothing)
        ProcessingNodes = True
        If ClickedRightTreeView Then
            Inner_CheckNodeTree(If(NodeParam Is Nothing, RightView.SelectedNode, NodeParam), Checked)
        Else
            Inner_CheckNodeTree(If(NodeParam Is Nothing, LeftView.SelectedNode, NodeParam), Checked)
        End If
        ProcessingNodes = False
    End Sub

    Private Sub Inner_CheckNodeTree(ByVal Root As TreeNode, ByVal Status As Boolean)
        Root.ToolTipText = "*"
        For Each SubNode As TreeNode In Root.Nodes
            SubNode.Checked = Status
            Inner_CheckNodeTree(SubNode, Status)
        Next
        Root.Checked = Status
    End Sub

    Private Sub BuildCheckedNodesList(ByRef NodesList As Dictionary(Of String, Boolean), ByVal Node As TreeNode)
        Dim OverAllNodeStatus As Integer = OverAllCheckStatus(Node)

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
                BuildCheckedNodesList(NodesList, Node.Nodes(NodeId))
            End If
        Next
    End Sub

    Private Function OverAllCheckStatus(ByVal Node As TreeNode) As Integer '0 All clear, 1 All checked, -1 different states
        If Not Node.TreeView.CheckBoxes Then Return 1
        If (Node.Nodes.Count = 0) Then Return If(Node.Checked, 1, 0)

        Dim AllChecked As Boolean = Node.Checked, AllClear As Boolean = Not Node.Checked
        For Each SubNode As TreeNode In Node.Nodes
            Dim CurrentStatus As Integer = OverAllCheckStatus(SubNode)
            AllChecked = AllChecked And (CurrentStatus = 1)
            AllClear = AllClear And (CurrentStatus = 0)
        Next

        If AllChecked Then Return 1
        If AllClear Then Return 0
        Return -1
    End Function

    Private Sub SetRootPathDisplay(ByVal Show As Boolean)
        If Show Then
            If Not FromTextBox.Text = "" And LeftView.Nodes.Count > 0 Then LeftView.Nodes(0).Text = FromTextBox.Text
            If Not ToTextBox.Text = "" And RightView.Nodes.Count > 0 Then RightView.Nodes(0).Text = ToTextBox.Text
        Else
            If LeftView.Nodes.Count > 0 Then LeftView.Nodes(0).Text = ""
            If RightView.Nodes.Count > 0 Then RightView.Nodes(0).Text = ""
        End If
    End Sub

    Private Sub ReloadTrees(ByVal AllowFullReload As Boolean, Optional ByVal ForceRight As Boolean = False)
        ReloadButton.Enabled = False
        SaveButton.Enabled = False
        Loading.Visible = True

        'Only reload fully if the user didn't input any text. Adding an extra trailing "\" for example will disable FullReload.
        Dim FullReload As Boolean = AllowFullReload And FromTextBox.Text = PrevLeft And ToTextBox.Text = PrevRight And LeftView.Enabled And RightView.Enabled

        Cleanup_Paths()
        'Unless FullReload is true, and no path has changed, only the trees where paths have changed are reloaded.
        If FullReload Or PrevLeft <> FromTextBox.Text Then
            LoadTree(LeftView, FromTextBox.Text, Handler.LeftCheckedNodes)
        End If
        If FullReload Or PrevRight <> ToTextBox.Text Or ForceRight Then
            LoadTree(RightView, ToTextBox.Text, Handler.RightCheckedNodes, CreateDestOption.Checked)
        End If

        Loading.Visible = False
        ReloadButton.Enabled = True

        PrevLeft = If(LeftView.Enabled, FromTextBox.Text, "-1")
        PrevRight = If(RightView.Enabled, ToTextBox.Text, "-1")

        CheckSettings()
    End Sub

    Private Sub LoadTree(ByVal Tree As TreeView, ByVal OriginalPath As String, ByVal CheckedNodes As Dictionary(Of String, Boolean), Optional ByVal ForceLoad As Boolean = False)
        Tree.Nodes.Clear()

        Dim Path As String = ProfileHandler.TranslatePath(OriginalPath) & ConfigOptions.DirSep
        Tree.Enabled = OriginalPath <> "" AndAlso (ForceLoad OrElse IO.Directory.Exists(Path))

        If Tree.Enabled Then
            Tree.BackColor = Drawing.Color.White
            Tree.Nodes.Add("")
            SetRootPathDisplay(True) 'Needed for the FullPath method, see tracker #3006324
            If Not ForceLoad Then
                Try
                    For Each Dir As String In IO.Directory.GetDirectories(Path)
                        Application.DoEvents()
                        Tree.Nodes(0).Nodes.Add(GetFileOrFolderName(Dir))
                    Next

                    'No need to expand root here, since children were already added.
                    LoadCheckState(Tree, CheckedNodes)
                    Tree.Nodes(0).Expand()
                Catch Ex As Exception
                    Tree.Nodes.Clear() 'TODO: When does this happen?
                    Tree.Enabled = False
                End Try
            End If
        End If

        If Not Tree.Enabled Then Tree.BackColor = Drawing.Color.LightGray
    End Sub

    Private Sub LoadCheckState(ByVal Tree As TreeView, ByVal CheckedNodes As Dictionary(Of String, Boolean))
        Dim BaseNode As TreeNode = Tree.Nodes(0)
        If CheckedNodes.Count = 0 Then CheckedNodes.Add("", True)
        InhibitAutocheck = True
        For Each CheckedPath As KeyValuePair(Of String, Boolean) In CheckedNodes
            CheckAccordingToPath(BaseNode, New List(Of String)(CheckedPath.Key.Split(ConfigOptions.DirSep)), CheckedPath.Value)
        Next
        InhibitAutocheck = False
    End Sub

    Private Sub CheckAccordingToPath(ByVal BaseNode As TreeNode, ByRef Path As List(Of String), ByVal FullCheck As Boolean)
        If Path.Count <> 0 AndAlso Path(0) = "" Then Path.RemoveAt(0) 'Path is the path to the node, splitted at separator chars.

        If Path.Count = 0 Then 'End of the path
            If FullCheck Then 'Check all subnodes
                ProcessingNodes = True
                Inner_CheckNodeTree(BaseNode, True)
                BaseNode.Collapse()
                ProcessingNodes = False
            Else
                BaseNode.Checked = True 'Only check this one.
            End If
        Else
            For Each Node As TreeNode In BaseNode.Nodes 'Search for next node
                If Node.Text = Path(0) Then
                    Node.Expand() : Path.RemoveAt(0)
                    CheckAccordingToPath(Node, Path, FullCheck)
                    Exit For
                End If
            Next
        End If
    End Sub
#End Region

#Region " Settings Handling "
    Private Sub UpdateSettings(ByVal LoadToForm As Boolean)
        Cleanup_Paths()

        'Careful: Using .ToString here would break the ByRef passing of the second argument. Problem with Option strict.
        Handler.CopySetting(ConfigOptions.Source, FromTextBox.Text, LoadToForm)
        Handler.CopySetting(ConfigOptions.Destination, ToTextBox.Text, LoadToForm)
        Handler.CopySetting(ConfigOptions.IncludedTypes, IncludedTypesTextBox.Text, LoadToForm)
        Handler.CopySetting(ConfigOptions.ExcludedTypes, ExcludedTypesTextBox.Text, LoadToForm)
        Handler.CopySetting(ConfigOptions.ReplicateEmptyDirectories, ReplicateEmptyDirectoriesOption.Checked, LoadToForm)
        Handler.CopySetting(ConfigOptions.MayCreateDestination, CreateDestOption.Checked, LoadToForm)
        Handler.CopySetting(ConfigOptions.StrictDateComparison, StrictDateComparisonOption.Checked, LoadToForm)
        Handler.CopySetting(ConfigOptions.PropagateUpdates, PropagateUpdatesOption.Checked, LoadToForm)
        Handler.CopySetting(ConfigOptions.StrictMirror, StrictMirrorOption.Checked, LoadToForm)
        Handler.CopySetting(ConfigOptions.TimeOffset, TimeOffset.Value, LoadToForm)
        Handler.CopySetting(ConfigOptions.Checksum, ChecksumOption.Checked, LoadToForm)
        Handler.CopySetting(ConfigOptions.CheckFileSize, CheckFileSizeOption.Checked, LoadToForm)
        Handler.CopySetting(ConfigOptions.Group, GroupTextBox.Text, LoadToForm)
        'Hidden settings are not added here

        'Note: Behaves correctly when no radio button is checked, although CopyAllFiles is unchecked.
        Dim Restrictions As String = (If(CopyAllFilesCheckBox.Checked, 0, 1) * (If(IncludeFilesOption.Checked, 1, 0) + 2 * If(ExcludeFilesOption.Checked, 1, 0))).ToString
        Dim Method As String = (If(LRIncrementalMethodOption.Checked, 1, 0) * 1 + If(TwoWaysIncrementalMethodOption.Checked, 1, 0) * 2).ToString

        If LoadToForm Then
            Select Case Handler.GetSetting(Of Integer)(ConfigOptions.Method)
                Case 1
                    LRIncrementalMethodOption.Checked = True
                Case 2
                    TwoWaysIncrementalMethodOption.Checked = True
                Case Else
                    LRMirrorMethodOption.Checked = True
            End Select

            CopyAllFilesCheckBox.Checked = False
            Select Case Handler.GetSetting(Of Integer)(ConfigOptions.Restrictions)
                Case 1
                    IncludeFilesOption.Checked = True
                Case 2
                    ExcludeFilesOption.Checked = True
                Case Else
                    CopyAllFilesCheckBox.Checked = True
            End Select

            ReloadTrees(True)
            Update_Form_Enabled_Components()
        Else
            Handler.SetSetting(ConfigOptions.Method, Method)
            Handler.SetSetting(ConfigOptions.Restrictions, Restrictions)

            SetRootPathDisplay(False)
            If LeftView.Enabled Then
                Handler.LeftCheckedNodes.Clear()
                BuildCheckedNodesList(Handler.LeftCheckedNodes, LeftView.Nodes(0))
                Handler.SetSetting(ConfigOptions.LeftSubFolders, GetString(Handler.LeftCheckedNodes))
            End If

            If RightView.Enabled Then
                If RightView.CheckBoxes Or Handler.GetSetting(Of String)(ConfigOptions.RightSubFolders) Is Nothing Then
                    Handler.RightCheckedNodes.Clear()
                    BuildCheckedNodesList(Handler.RightCheckedNodes, RightView.Nodes(0))
                    Handler.SetSetting(ConfigOptions.RightSubFolders, GetString(Handler.RightCheckedNodes))
                End If
            End If
            SetRootPathDisplay(True)
        End If
    End Sub

    Private Shared Function Cleanup(ByVal Path As String) As String
#If LINUX Then
        Return If(Path.Contains("/"), "/", "") & Path.TrimEnd(New Char() {ConfigOptions.DirSep, " "})
#Else
        Return Path.TrimEnd(New Char() {ConfigOptions.DirSep, " "c})
#End If
    End Function

    Private Sub Cleanup_Paths()
        FromTextBox.Text = Cleanup(FromTextBox.Text)
        ToTextBox.Text = Cleanup(ToTextBox.Text)
    End Sub

    Private Shared Function GetString(ByRef Table As Dictionary(Of String, Boolean)) As String
        Dim ListString As New System.Text.StringBuilder
        For Each Node As String In Table.Keys
            ListString.Append(Node).Append(";")
        Next
        Return ListString.ToString
    End Function
#End Region

    Private Sub MoreLabel_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MoreLabel.MouseClick
        ExpertMenu.Show(MoreLabel, e.Location)
    End Sub

    Private Sub GroupTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupTextBox.TextChanged
        GroupOption.Checked = GroupTextBox.Text <> ""
    End Sub
End Class
