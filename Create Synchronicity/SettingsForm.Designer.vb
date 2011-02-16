'This file is part of Create Synchronicity.
'
'Create Synchronicity is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
'Create Synchronicity is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with Create Synchronicity.  If not, see <http://www.gnu.org/licenses/>.
'Created by:	Clément Pit--Claudel.
'Web site:		http://synchronicity.sourceforge.net.

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SettingsForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SettingsForm))
        Me.FromTextBox = New System.Windows.Forms.TextBox()
        Me.DirectoriesBox = New System.Windows.Forms.GroupBox()
        Me.SwapButton = New System.Windows.Forms.Button()
        Me.BrowseRButton = New System.Windows.Forms.Button()
        Me.BrowseLButton = New System.Windows.Forms.Button()
        Me.ToTextBox = New System.Windows.Forms.TextBox()
        Me.ToLabel = New System.Windows.Forms.Label()
        Me.FromLabel = New System.Windows.Forms.Label()
        Me.ViewsLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.ReloadButton = New System.Windows.Forms.Button()
        Me.LeftViewLabel = New System.Windows.Forms.Label()
        Me.RightViewLabel = New System.Windows.Forms.Label()
        Me.Loading = New System.Windows.Forms.Label()
        Me.HelpLink = New System.Windows.Forms.Label()
        Me.LeftViewPanel = New System.Windows.Forms.Panel()
        Me.LeftReloadButton = New System.Windows.Forms.Button()
        Me.LeftView = New System.Windows.Forms.TreeView()
        Me.TreeViewMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SynchronizeFolderAndSubfoldersMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.SynchronizeFilesOnlyMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SynchronizeSubFoldersOnlyMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.DontSynchronizeMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToggleMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RightViewPanel = New System.Windows.Forms.Panel()
        Me.RightReloadButton = New System.Windows.Forms.Button()
        Me.RightView = New System.Windows.Forms.TreeView()
        Me.ViewsBox = New System.Windows.Forms.GroupBox()
        Me.SynchronizationMethodBox = New System.Windows.Forms.GroupBox()
        Me.StrictMirrorOption = New System.Windows.Forms.CheckBox()
        Me.MethodLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.TwoWaysIncrementalMethodOption = New System.Windows.Forms.RadioButton()
        Me.LRIncrementalMethodOption = New System.Windows.Forms.RadioButton()
        Me.LRMirrorMethodOption = New System.Windows.Forms.RadioButton()
        Me.IncludeExcludeBox = New System.Windows.Forms.GroupBox()
        Me.CopyAllFilesCheckBox = New System.Windows.Forms.CheckBox()
        Me.IncludeExcludeLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.IncludedTypesTextBox = New System.Windows.Forms.TextBox()
        Me.ExcludedTypesTextBox = New System.Windows.Forms.TextBox()
        Me.IncludeFilesOption = New System.Windows.Forms.RadioButton()
        Me.ExcludeFilesOption = New System.Windows.Forms.RadioButton()
        Me.ReplicateEmptyDirectoriesOption = New System.Windows.Forms.CheckBox()
        Me.PropagateUpdatesOption = New System.Windows.Forms.CheckBox()
        Me.ActionsPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.CancelBtn = New System.Windows.Forms.Button()
        Me.SaveButton = New System.Windows.Forms.Button()
        Me.FolderBrowser = New System.Windows.Forms.FolderBrowserDialog()
        Me.AdvancedBox = New System.Windows.Forms.GroupBox()
        Me.StrictDateComparisonOption = New System.Windows.Forms.CheckBox()
        Me.TimeOffsetLabel = New System.Windows.Forms.Label()
        Me.MoreLabel = New System.Windows.Forms.LinkLabel()
        Me.TimeOffset = New System.Windows.Forms.NumericUpDown()
        Me.TimeOffsetHoursLabel = New System.Windows.Forms.Label()
        Me.BottomDescLabel = New System.Windows.Forms.Label()
        Me.ExpertMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CreateDestOption = New System.Windows.Forms.ToolStripMenuItem()
        Me.CheckFileSizeOption = New System.Windows.Forms.ToolStripMenuItem()
        Me.ChecksumOption = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupOption = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupTextBox = New System.Windows.Forms.ToolStripTextBox()
        Me.DirectoriesBox.SuspendLayout()
        Me.ViewsLayoutPanel.SuspendLayout()
        Me.LeftViewPanel.SuspendLayout()
        Me.TreeViewMenuStrip.SuspendLayout()
        Me.RightViewPanel.SuspendLayout()
        Me.ViewsBox.SuspendLayout()
        Me.SynchronizationMethodBox.SuspendLayout()
        Me.MethodLayoutPanel.SuspendLayout()
        Me.IncludeExcludeBox.SuspendLayout()
        Me.IncludeExcludeLayoutPanel.SuspendLayout()
        Me.ActionsPanel.SuspendLayout()
        Me.AdvancedBox.SuspendLayout()
        CType(Me.TimeOffset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ExpertMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'FromTextBox
        '
        Me.FromTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FromTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.FromTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories
        Me.FromTextBox.Location = New System.Drawing.Point(62, 19)
        Me.FromTextBox.Name = "FromTextBox"
        Me.FromTextBox.Size = New System.Drawing.Size(535, 21)
        Me.FromTextBox.TabIndex = 1
        Me.FromTextBox.Tag = "\PATH_TIPS"
        '
        'DirectoriesBox
        '
        Me.DirectoriesBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DirectoriesBox.Controls.Add(Me.SwapButton)
        Me.DirectoriesBox.Controls.Add(Me.BrowseRButton)
        Me.DirectoriesBox.Controls.Add(Me.BrowseLButton)
        Me.DirectoriesBox.Controls.Add(Me.ToTextBox)
        Me.DirectoriesBox.Controls.Add(Me.FromTextBox)
        Me.DirectoriesBox.Controls.Add(Me.ToLabel)
        Me.DirectoriesBox.Controls.Add(Me.FromLabel)
        Me.DirectoriesBox.Location = New System.Drawing.Point(12, 12)
        Me.DirectoriesBox.Name = "DirectoriesBox"
        Me.DirectoriesBox.Size = New System.Drawing.Size(700, 71)
        Me.DirectoriesBox.TabIndex = 0
        Me.DirectoriesBox.TabStop = False
        Me.DirectoriesBox.Text = "\DIRECTORIES"
        '
        'SwapButton
        '
        Me.SwapButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SwapButton.Location = New System.Drawing.Point(638, 19)
        Me.SwapButton.Name = "SwapButton"
        Me.SwapButton.Size = New System.Drawing.Size(56, 49)
        Me.SwapButton.TabIndex = 6
        Me.SwapButton.Text = "\SWAP"
        Me.SwapButton.UseVisualStyleBackColor = True
        '
        'BrowseRButton
        '
        Me.BrowseRButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BrowseRButton.Location = New System.Drawing.Point(603, 44)
        Me.BrowseRButton.Name = "BrowseRButton"
        Me.BrowseRButton.Size = New System.Drawing.Size(29, 24)
        Me.BrowseRButton.TabIndex = 5
        Me.BrowseRButton.Text = "..."
        Me.BrowseRButton.UseVisualStyleBackColor = True
        '
        'BrowseLButton
        '
        Me.BrowseLButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BrowseLButton.Location = New System.Drawing.Point(603, 18)
        Me.BrowseLButton.Name = "BrowseLButton"
        Me.BrowseLButton.Size = New System.Drawing.Size(29, 24)
        Me.BrowseLButton.TabIndex = 2
        Me.BrowseLButton.Text = "..."
        Me.BrowseLButton.UseVisualStyleBackColor = True
        '
        'ToTextBox
        '
        Me.ToTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ToTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.ToTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories
        Me.ToTextBox.Location = New System.Drawing.Point(62, 45)
        Me.ToTextBox.Name = "ToTextBox"
        Me.ToTextBox.Size = New System.Drawing.Size(535, 21)
        Me.ToTextBox.TabIndex = 4
        Me.ToTextBox.Tag = "\PATH_TIPS"
        '
        'ToLabel
        '
        Me.ToLabel.Location = New System.Drawing.Point(6, 44)
        Me.ToLabel.Name = "ToLabel"
        Me.ToLabel.Size = New System.Drawing.Size(59, 21)
        Me.ToLabel.TabIndex = 3
        Me.ToLabel.Text = "\TO"
        Me.ToLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FromLabel
        '
        Me.FromLabel.Location = New System.Drawing.Point(3, 18)
        Me.FromLabel.Name = "FromLabel"
        Me.FromLabel.Size = New System.Drawing.Size(62, 21)
        Me.FromLabel.TabIndex = 0
        Me.FromLabel.Text = "\FROM"
        Me.FromLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ViewsLayoutPanel
        '
        Me.ViewsLayoutPanel.ColumnCount = 3
        Me.ViewsLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.ViewsLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.ViewsLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.ViewsLayoutPanel.Controls.Add(Me.ReloadButton, 1, 1)
        Me.ViewsLayoutPanel.Controls.Add(Me.LeftViewLabel, 0, 0)
        Me.ViewsLayoutPanel.Controls.Add(Me.RightViewLabel, 2, 0)
        Me.ViewsLayoutPanel.Controls.Add(Me.Loading, 1, 2)
        Me.ViewsLayoutPanel.Controls.Add(Me.HelpLink, 1, 3)
        Me.ViewsLayoutPanel.Controls.Add(Me.LeftViewPanel, 0, 1)
        Me.ViewsLayoutPanel.Controls.Add(Me.RightViewPanel, 2, 1)
        Me.ViewsLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ViewsLayoutPanel.Location = New System.Drawing.Point(3, 17)
        Me.ViewsLayoutPanel.Name = "ViewsLayoutPanel"
        Me.ViewsLayoutPanel.RowCount = 4
        Me.ViewsLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.ViewsLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.ViewsLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.ViewsLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ViewsLayoutPanel.Size = New System.Drawing.Size(694, 140)
        Me.ViewsLayoutPanel.TabIndex = 0
        '
        'ReloadButton
        '
        Me.ReloadButton.Image = CType(resources.GetObject("ReloadButton.Image"), System.Drawing.Image)
        Me.ReloadButton.Location = New System.Drawing.Point(332, 16)
        Me.ReloadButton.Name = "ReloadButton"
        Me.ReloadButton.Size = New System.Drawing.Size(29, 29)
        Me.ReloadButton.TabIndex = 4
        Me.ReloadButton.UseVisualStyleBackColor = False
        '
        'LeftViewLabel
        '
        Me.LeftViewLabel.AutoSize = True
        Me.LeftViewLabel.Location = New System.Drawing.Point(3, 0)
        Me.LeftViewLabel.Name = "LeftViewLabel"
        Me.LeftViewLabel.Size = New System.Drawing.Size(74, 13)
        Me.LeftViewLabel.TabIndex = 0
        Me.LeftViewLabel.Text = "\LEFT_SIDE"
        '
        'RightViewLabel
        '
        Me.RightViewLabel.AutoSize = True
        Me.RightViewLabel.Location = New System.Drawing.Point(367, 0)
        Me.RightViewLabel.Name = "RightViewLabel"
        Me.RightViewLabel.Size = New System.Drawing.Size(85, 13)
        Me.RightViewLabel.TabIndex = 2
        Me.RightViewLabel.Text = "\RIGHT_SIDE"
        '
        'Loading
        '
        Me.Loading.Image = CType(resources.GetObject("Loading.Image"), System.Drawing.Image)
        Me.Loading.Location = New System.Drawing.Point(332, 48)
        Me.Loading.Name = "Loading"
        Me.Loading.Size = New System.Drawing.Size(29, 29)
        Me.Loading.TabIndex = 5
        Me.Loading.Visible = False
        '
        'HelpLink
        '
        Me.HelpLink.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.HelpLink.Image = CType(resources.GetObject("HelpLink.Image"), System.Drawing.Image)
        Me.HelpLink.Location = New System.Drawing.Point(332, 112)
        Me.HelpLink.Name = "HelpLink"
        Me.HelpLink.Size = New System.Drawing.Size(28, 28)
        Me.HelpLink.TabIndex = 6
        '
        'LeftViewPanel
        '
        Me.LeftViewPanel.Controls.Add(Me.LeftReloadButton)
        Me.LeftViewPanel.Controls.Add(Me.LeftView)
        Me.LeftViewPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LeftViewPanel.Location = New System.Drawing.Point(3, 16)
        Me.LeftViewPanel.Name = "LeftViewPanel"
        Me.ViewsLayoutPanel.SetRowSpan(Me.LeftViewPanel, 3)
        Me.LeftViewPanel.Size = New System.Drawing.Size(323, 121)
        Me.LeftViewPanel.TabIndex = 7
        '
        'LeftReloadButton
        '
        Me.LeftReloadButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LeftReloadButton.Location = New System.Drawing.Point(111, 27)
        Me.LeftReloadButton.Name = "LeftReloadButton"
        Me.LeftReloadButton.Size = New System.Drawing.Size(100, 66)
        Me.LeftReloadButton.TabIndex = 7
        Me.LeftReloadButton.Text = "\RELOAD_TREES"
        Me.LeftReloadButton.UseVisualStyleBackColor = True
        '
        'LeftView
        '
        Me.LeftView.CheckBoxes = True
        Me.LeftView.ContextMenuStrip = Me.TreeViewMenuStrip
        Me.LeftView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LeftView.Location = New System.Drawing.Point(0, 0)
        Me.LeftView.Name = "LeftView"
        Me.LeftView.Size = New System.Drawing.Size(323, 121)
        Me.LeftView.TabIndex = 1
        Me.LeftView.Tag = "\TREEVIEW_TIPS"
        '
        'TreeViewMenuStrip
        '
        Me.TreeViewMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SynchronizeFolderAndSubfoldersMenuItem, Me.ToolStripSeparator1, Me.SynchronizeFilesOnlyMenuItem, Me.SynchronizeSubFoldersOnlyMenuItem, Me.ToolStripSeparator2, Me.DontSynchronizeMenuItem, Me.ToolStripSeparator3, Me.ToggleMenuItem})
        Me.TreeViewMenuStrip.Name = "TreeViewMenuStrip"
        Me.TreeViewMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.TreeViewMenuStrip.ShowImageMargin = False
        Me.TreeViewMenuStrip.Size = New System.Drawing.Size(201, 132)
        '
        'SynchronizeFolderAndSubfoldersMenuItem
        '
        Me.SynchronizeFolderAndSubfoldersMenuItem.Name = "SynchronizeFolderAndSubfoldersMenuItem"
        Me.SynchronizeFolderAndSubfoldersMenuItem.Size = New System.Drawing.Size(200, 22)
        Me.SynchronizeFolderAndSubfoldersMenuItem.Text = "\FOLDER_AND_SUBFOLDERS"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(197, 6)
        '
        'SynchronizeFilesOnlyMenuItem
        '
        Me.SynchronizeFilesOnlyMenuItem.Name = "SynchronizeFilesOnlyMenuItem"
        Me.SynchronizeFilesOnlyMenuItem.Size = New System.Drawing.Size(200, 22)
        Me.SynchronizeFilesOnlyMenuItem.Text = "\FILES_ONLY"
        '
        'SynchronizeSubFoldersOnlyMenuItem
        '
        Me.SynchronizeSubFoldersOnlyMenuItem.Name = "SynchronizeSubFoldersOnlyMenuItem"
        Me.SynchronizeSubFoldersOnlyMenuItem.Size = New System.Drawing.Size(200, 22)
        Me.SynchronizeSubFoldersOnlyMenuItem.Text = "\SUBFOLDERS_ONLY"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(197, 6)
        '
        'DontSynchronizeMenuItem
        '
        Me.DontSynchronizeMenuItem.Name = "DontSynchronizeMenuItem"
        Me.DontSynchronizeMenuItem.Size = New System.Drawing.Size(200, 22)
        Me.DontSynchronizeMenuItem.Text = "\NO_SYNC"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(197, 6)
        '
        'ToggleMenuItem
        '
        Me.ToggleMenuItem.Name = "ToggleMenuItem"
        Me.ToggleMenuItem.Size = New System.Drawing.Size(200, 22)
        Me.ToggleMenuItem.Tag = ""
        Me.ToggleMenuItem.Text = "\TOGGLE"
        '
        'RightViewPanel
        '
        Me.RightViewPanel.Controls.Add(Me.RightReloadButton)
        Me.RightViewPanel.Controls.Add(Me.RightView)
        Me.RightViewPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RightViewPanel.Location = New System.Drawing.Point(367, 16)
        Me.RightViewPanel.Name = "RightViewPanel"
        Me.ViewsLayoutPanel.SetRowSpan(Me.RightViewPanel, 3)
        Me.RightViewPanel.Size = New System.Drawing.Size(324, 121)
        Me.RightViewPanel.TabIndex = 8
        '
        'RightReloadButton
        '
        Me.RightReloadButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.RightReloadButton.Location = New System.Drawing.Point(112, 27)
        Me.RightReloadButton.Name = "RightReloadButton"
        Me.RightReloadButton.Size = New System.Drawing.Size(100, 66)
        Me.RightReloadButton.TabIndex = 8
        Me.RightReloadButton.Text = "\RELOAD_TREES"
        Me.RightReloadButton.UseVisualStyleBackColor = True
        '
        'RightView
        '
        Me.RightView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RightView.Location = New System.Drawing.Point(0, 0)
        Me.RightView.Name = "RightView"
        Me.RightView.Size = New System.Drawing.Size(324, 121)
        Me.RightView.TabIndex = 3
        Me.RightView.Tag = "\TREEVIEW_TIPS"
        '
        'ViewsBox
        '
        Me.ViewsBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ViewsBox.Controls.Add(Me.ViewsLayoutPanel)
        Me.ViewsBox.Location = New System.Drawing.Point(12, 89)
        Me.ViewsBox.Name = "ViewsBox"
        Me.ViewsBox.Size = New System.Drawing.Size(700, 160)
        Me.ViewsBox.TabIndex = 1
        Me.ViewsBox.TabStop = False
        Me.ViewsBox.Text = "\SUBDIRECTORIES"
        '
        'SynchronizationMethodBox
        '
        Me.SynchronizationMethodBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SynchronizationMethodBox.Controls.Add(Me.StrictMirrorOption)
        Me.SynchronizationMethodBox.Controls.Add(Me.MethodLayoutPanel)
        Me.SynchronizationMethodBox.Location = New System.Drawing.Point(12, 255)
        Me.SynchronizationMethodBox.Name = "SynchronizationMethodBox"
        Me.SynchronizationMethodBox.Size = New System.Drawing.Size(700, 69)
        Me.SynchronizationMethodBox.TabIndex = 2
        Me.SynchronizationMethodBox.TabStop = False
        Me.SynchronizationMethodBox.Text = "\SYNC_METHOD"
        '
        'StrictMirrorOption
        '
        Me.StrictMirrorOption.AutoSize = True
        Me.StrictMirrorOption.Location = New System.Drawing.Point(6, 46)
        Me.StrictMirrorOption.Name = "StrictMirrorOption"
        Me.StrictMirrorOption.Size = New System.Drawing.Size(155, 17)
        Me.StrictMirrorOption.TabIndex = 2
        Me.StrictMirrorOption.Text = "\STRICT_MIRROR_DESC"
        Me.StrictMirrorOption.UseVisualStyleBackColor = True
        '
        'MethodLayoutPanel
        '
        Me.MethodLayoutPanel.ColumnCount = 3
        Me.MethodLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.MethodLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
        Me.MethodLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
        Me.MethodLayoutPanel.Controls.Add(Me.TwoWaysIncrementalMethodOption, 2, 0)
        Me.MethodLayoutPanel.Controls.Add(Me.LRIncrementalMethodOption, 1, 0)
        Me.MethodLayoutPanel.Controls.Add(Me.LRMirrorMethodOption, 0, 0)
        Me.MethodLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.MethodLayoutPanel.Location = New System.Drawing.Point(3, 16)
        Me.MethodLayoutPanel.Name = "MethodLayoutPanel"
        Me.MethodLayoutPanel.RowCount = 1
        Me.MethodLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.MethodLayoutPanel.Size = New System.Drawing.Size(694, 23)
        Me.MethodLayoutPanel.TabIndex = 0
        '
        'TwoWaysIncrementalMethodOption
        '
        Me.TwoWaysIncrementalMethodOption.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TwoWaysIncrementalMethodOption.AutoSize = True
        Me.TwoWaysIncrementalMethodOption.Location = New System.Drawing.Point(488, 3)
        Me.TwoWaysIncrementalMethodOption.Name = "TwoWaysIncrementalMethodOption"
        Me.TwoWaysIncrementalMethodOption.Size = New System.Drawing.Size(180, 17)
        Me.TwoWaysIncrementalMethodOption.TabIndex = 2
        Me.TwoWaysIncrementalMethodOption.Tag = "\TWOWAYS_INCREMENTAL_TAG"
        Me.TwoWaysIncrementalMethodOption.Text = "\TWOWAYS_INCREMENTAL"
        Me.TwoWaysIncrementalMethodOption.UseVisualStyleBackColor = True
        '
        'LRIncrementalMethodOption
        '
        Me.LRIncrementalMethodOption.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LRIncrementalMethodOption.AutoSize = True
        Me.LRIncrementalMethodOption.Location = New System.Drawing.Point(280, 3)
        Me.LRIncrementalMethodOption.Name = "LRIncrementalMethodOption"
        Me.LRIncrementalMethodOption.Size = New System.Drawing.Size(133, 17)
        Me.LRIncrementalMethodOption.TabIndex = 1
        Me.LRIncrementalMethodOption.Tag = "\LR_INCREMENTAL_TAG"
        Me.LRIncrementalMethodOption.Text = "\LR_INCREMENTAL"
        Me.LRIncrementalMethodOption.UseVisualStyleBackColor = True
        '
        'LRMirrorMethodOption
        '
        Me.LRMirrorMethodOption.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LRMirrorMethodOption.AutoSize = True
        Me.LRMirrorMethodOption.Checked = True
        Me.LRMirrorMethodOption.Location = New System.Drawing.Point(66, 3)
        Me.LRMirrorMethodOption.Name = "LRMirrorMethodOption"
        Me.LRMirrorMethodOption.Size = New System.Drawing.Size(98, 17)
        Me.LRMirrorMethodOption.TabIndex = 0
        Me.LRMirrorMethodOption.TabStop = True
        Me.LRMirrorMethodOption.Tag = "\LR_MIRROR_TAG"
        Me.LRMirrorMethodOption.Text = "\LR_MIRROR"
        Me.LRMirrorMethodOption.UseVisualStyleBackColor = True
        '
        'IncludeExcludeBox
        '
        Me.IncludeExcludeBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IncludeExcludeBox.Controls.Add(Me.CopyAllFilesCheckBox)
        Me.IncludeExcludeBox.Controls.Add(Me.IncludeExcludeLayoutPanel)
        Me.IncludeExcludeBox.Controls.Add(Me.ReplicateEmptyDirectoriesOption)
        Me.IncludeExcludeBox.Location = New System.Drawing.Point(12, 330)
        Me.IncludeExcludeBox.Name = "IncludeExcludeBox"
        Me.IncludeExcludeBox.Size = New System.Drawing.Size(700, 97)
        Me.IncludeExcludeBox.TabIndex = 3
        Me.IncludeExcludeBox.TabStop = False
        Me.IncludeExcludeBox.Text = "\INCLUDE_EXCLUDE"
        '
        'CopyAllFilesCheckBox
        '
        Me.CopyAllFilesCheckBox.AutoSize = True
        Me.CopyAllFilesCheckBox.Checked = True
        Me.CopyAllFilesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CopyAllFilesCheckBox.Location = New System.Drawing.Point(6, 20)
        Me.CopyAllFilesCheckBox.Name = "CopyAllFilesCheckBox"
        Me.CopyAllFilesCheckBox.Size = New System.Drawing.Size(85, 17)
        Me.CopyAllFilesCheckBox.TabIndex = 0
        Me.CopyAllFilesCheckBox.Text = "\ALL_FILES"
        Me.CopyAllFilesCheckBox.UseVisualStyleBackColor = True
        '
        'IncludeExcludeLayoutPanel
        '
        Me.IncludeExcludeLayoutPanel.ColumnCount = 2
        Me.IncludeExcludeLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.IncludeExcludeLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.IncludeExcludeLayoutPanel.Controls.Add(Me.IncludedTypesTextBox, 0, 1)
        Me.IncludeExcludeLayoutPanel.Controls.Add(Me.ExcludedTypesTextBox, 0, 1)
        Me.IncludeExcludeLayoutPanel.Controls.Add(Me.IncludeFilesOption, 0, 0)
        Me.IncludeExcludeLayoutPanel.Controls.Add(Me.ExcludeFilesOption, 1, 0)
        Me.IncludeExcludeLayoutPanel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.IncludeExcludeLayoutPanel.Enabled = False
        Me.IncludeExcludeLayoutPanel.Location = New System.Drawing.Point(3, 43)
        Me.IncludeExcludeLayoutPanel.Name = "IncludeExcludeLayoutPanel"
        Me.IncludeExcludeLayoutPanel.RowCount = 3
        Me.IncludeExcludeLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.IncludeExcludeLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.IncludeExcludeLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.IncludeExcludeLayoutPanel.Size = New System.Drawing.Size(694, 51)
        Me.IncludeExcludeLayoutPanel.TabIndex = 2
        '
        'IncludedTypesTextBox
        '
        Me.IncludedTypesTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IncludedTypesTextBox.Location = New System.Drawing.Point(3, 26)
        Me.IncludedTypesTextBox.Name = "IncludedTypesTextBox"
        Me.IncludedTypesTextBox.Size = New System.Drawing.Size(341, 21)
        Me.IncludedTypesTextBox.TabIndex = 1
        Me.IncludedTypesTextBox.Tag = "\FILEEXT_TIPS"
        '
        'ExcludedTypesTextBox
        '
        Me.ExcludedTypesTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ExcludedTypesTextBox.Location = New System.Drawing.Point(350, 26)
        Me.ExcludedTypesTextBox.Name = "ExcludedTypesTextBox"
        Me.ExcludedTypesTextBox.Size = New System.Drawing.Size(341, 21)
        Me.ExcludedTypesTextBox.TabIndex = 3
        Me.ExcludedTypesTextBox.Tag = "\FILEEXT_TIPS"
        '
        'IncludeFilesOption
        '
        Me.IncludeFilesOption.AutoSize = True
        Me.IncludeFilesOption.Location = New System.Drawing.Point(3, 3)
        Me.IncludeFilesOption.Name = "IncludeFilesOption"
        Me.IncludeFilesOption.Size = New System.Drawing.Size(104, 17)
        Me.IncludeFilesOption.TabIndex = 0
        Me.IncludeFilesOption.TabStop = True
        Me.IncludeFilesOption.Text = "\THESE_ONLY"
        Me.IncludeFilesOption.UseVisualStyleBackColor = True
        '
        'ExcludeFilesOption
        '
        Me.ExcludeFilesOption.AutoSize = True
        Me.ExcludeFilesOption.Location = New System.Drawing.Point(350, 3)
        Me.ExcludeFilesOption.Name = "ExcludeFilesOption"
        Me.ExcludeFilesOption.Size = New System.Drawing.Size(128, 17)
        Me.ExcludeFilesOption.TabIndex = 2
        Me.ExcludeFilesOption.TabStop = True
        Me.ExcludeFilesOption.Text = "\EXCLUDE_THESE"
        Me.ExcludeFilesOption.UseVisualStyleBackColor = True
        '
        'ReplicateEmptyDirectoriesOption
        '
        Me.ReplicateEmptyDirectoriesOption.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ReplicateEmptyDirectoriesOption.AutoSize = True
        Me.ReplicateEmptyDirectoriesOption.Checked = True
        Me.ReplicateEmptyDirectoriesOption.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ReplicateEmptyDirectoriesOption.Location = New System.Drawing.Point(564, 20)
        Me.ReplicateEmptyDirectoriesOption.Name = "ReplicateEmptyDirectoriesOption"
        Me.ReplicateEmptyDirectoriesOption.Size = New System.Drawing.Size(133, 17)
        Me.ReplicateEmptyDirectoriesOption.TabIndex = 1
        Me.ReplicateEmptyDirectoriesOption.Text = "\REPLICATE_EMPTY"
        Me.ReplicateEmptyDirectoriesOption.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ReplicateEmptyDirectoriesOption.UseVisualStyleBackColor = True
        '
        'PropagateUpdatesOption
        '
        Me.PropagateUpdatesOption.AutoSize = True
        Me.PropagateUpdatesOption.Checked = True
        Me.PropagateUpdatesOption.CheckState = System.Windows.Forms.CheckState.Checked
        Me.PropagateUpdatesOption.Location = New System.Drawing.Point(6, 20)
        Me.PropagateUpdatesOption.Name = "PropagateUpdatesOption"
        Me.PropagateUpdatesOption.Size = New System.Drawing.Size(101, 17)
        Me.PropagateUpdatesOption.TabIndex = 0
        Me.PropagateUpdatesOption.Tag = "\PROPAGATE_TAG"
        Me.PropagateUpdatesOption.Text = "\PROPAGATE"
        Me.PropagateUpdatesOption.UseVisualStyleBackColor = True
        '
        'ActionsPanel
        '
        Me.ActionsPanel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ActionsPanel.ColumnCount = 2
        Me.ActionsPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.ActionsPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.ActionsPanel.Controls.Add(Me.CancelBtn, 1, 0)
        Me.ActionsPanel.Controls.Add(Me.SaveButton, 0, 0)
        Me.ActionsPanel.Location = New System.Drawing.Point(512, 507)
        Me.ActionsPanel.Name = "ActionsPanel"
        Me.ActionsPanel.RowCount = 1
        Me.ActionsPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.ActionsPanel.Size = New System.Drawing.Size(200, 31)
        Me.ActionsPanel.TabIndex = 6
        '
        'CancelBtn
        '
        Me.CancelBtn.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.CancelBtn.Location = New System.Drawing.Point(103, 3)
        Me.CancelBtn.Name = "CancelBtn"
        Me.CancelBtn.Size = New System.Drawing.Size(94, 25)
        Me.CancelBtn.TabIndex = 1
        Me.CancelBtn.Text = "\CANCEL"
        Me.CancelBtn.UseVisualStyleBackColor = True
        '
        'SaveButton
        '
        Me.SaveButton.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SaveButton.Location = New System.Drawing.Point(3, 3)
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.Size = New System.Drawing.Size(94, 25)
        Me.SaveButton.TabIndex = 0
        Me.SaveButton.Text = "\SAVE"
        Me.SaveButton.UseVisualStyleBackColor = True
        '
        'FolderBrowser
        '
        Me.FolderBrowser.RootFolder = System.Environment.SpecialFolder.MyComputer
        '
        'AdvancedBox
        '
        Me.AdvancedBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AdvancedBox.Controls.Add(Me.StrictDateComparisonOption)
        Me.AdvancedBox.Controls.Add(Me.TimeOffsetLabel)
        Me.AdvancedBox.Controls.Add(Me.MoreLabel)
        Me.AdvancedBox.Controls.Add(Me.TimeOffset)
        Me.AdvancedBox.Controls.Add(Me.PropagateUpdatesOption)
        Me.AdvancedBox.Controls.Add(Me.TimeOffsetHoursLabel)
        Me.AdvancedBox.Location = New System.Drawing.Point(12, 433)
        Me.AdvancedBox.Name = "AdvancedBox"
        Me.AdvancedBox.Size = New System.Drawing.Size(700, 68)
        Me.AdvancedBox.TabIndex = 4
        Me.AdvancedBox.TabStop = False
        Me.AdvancedBox.Text = "\ADVANCED_OPTS"
        '
        'StrictDateComparisonOption
        '
        Me.StrictDateComparisonOption.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.StrictDateComparisonOption.AutoSize = True
        Me.StrictDateComparisonOption.Checked = True
        Me.StrictDateComparisonOption.CheckState = System.Windows.Forms.CheckState.Checked
        Me.StrictDateComparisonOption.Location = New System.Drawing.Point(532, 20)
        Me.StrictDateComparisonOption.Name = "StrictDateComparisonOption"
        Me.StrictDateComparisonOption.Size = New System.Drawing.Size(162, 17)
        Me.StrictDateComparisonOption.TabIndex = 2
        Me.StrictDateComparisonOption.Tag = "\STRICTCOMPARISON_TAG"
        Me.StrictDateComparisonOption.Text = "\STRICT_COMPARISON"
        Me.StrictDateComparisonOption.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.StrictDateComparisonOption.UseVisualStyleBackColor = True
        '
        'TimeOffsetLabel
        '
        Me.TimeOffsetLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TimeOffsetLabel.Location = New System.Drawing.Point(451, 45)
        Me.TimeOffsetLabel.Name = "TimeOffsetLabel"
        Me.TimeOffsetLabel.Size = New System.Drawing.Size(143, 13)
        Me.TimeOffsetLabel.TabIndex = 3
        Me.TimeOffsetLabel.Text = "\TIME_OFFSET"
        Me.TimeOffsetLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'MoreLabel
        '
        Me.MoreLabel.AutoSize = True
        Me.MoreLabel.Location = New System.Drawing.Point(3, 45)
        Me.MoreLabel.Name = "MoreLabel"
        Me.MoreLabel.Size = New System.Drawing.Size(45, 13)
        Me.MoreLabel.TabIndex = 7
        Me.MoreLabel.TabStop = True
        Me.MoreLabel.Text = "\MORE"
        '
        'TimeOffset
        '
        Me.TimeOffset.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TimeOffset.Location = New System.Drawing.Point(600, 43)
        Me.TimeOffset.Maximum = New Decimal(New Integer() {2, 0, 0, 0})
        Me.TimeOffset.Minimum = New Decimal(New Integer() {2, 0, 0, -2147483648})
        Me.TimeOffset.Name = "TimeOffset"
        Me.TimeOffset.Size = New System.Drawing.Size(35, 21)
        Me.TimeOffset.TabIndex = 4
        '
        'TimeOffsetHoursLabel
        '
        Me.TimeOffsetHoursLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TimeOffsetHoursLabel.AutoSize = True
        Me.TimeOffsetHoursLabel.Location = New System.Drawing.Point(641, 45)
        Me.TimeOffsetHoursLabel.Name = "TimeOffsetHoursLabel"
        Me.TimeOffsetHoursLabel.Size = New System.Drawing.Size(53, 13)
        Me.TimeOffsetHoursLabel.TabIndex = 5
        Me.TimeOffsetHoursLabel.Text = "\HOURS"
        '
        'BottomDescLabel
        '
        Me.BottomDescLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BottomDescLabel.Location = New System.Drawing.Point(12, 507)
        Me.BottomDescLabel.Name = "BottomDescLabel"
        Me.BottomDescLabel.Size = New System.Drawing.Size(497, 31)
        Me.BottomDescLabel.TabIndex = 5
        '
        'ExpertMenu
        '
        Me.ExpertMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CreateDestOption, Me.CheckFileSizeOption, Me.ChecksumOption, Me.GroupOption})
        Me.ExpertMenu.Name = "ExpertMenu"
        Me.ExpertMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ExpertMenu.ShowCheckMargin = True
        Me.ExpertMenu.ShowImageMargin = False
        Me.ExpertMenu.Size = New System.Drawing.Size(163, 92)
        '
        'CreateDestOption
        '
        Me.CreateDestOption.CheckOnClick = True
        Me.CreateDestOption.Name = "CreateDestOption"
        Me.CreateDestOption.Size = New System.Drawing.Size(162, 22)
        Me.CreateDestOption.Text = "\CREATE_DEST"
        '
        'CheckFileSizeOption
        '
        Me.CheckFileSizeOption.CheckOnClick = True
        Me.CheckFileSizeOption.Name = "CheckFileSizeOption"
        Me.CheckFileSizeOption.Size = New System.Drawing.Size(162, 22)
        Me.CheckFileSizeOption.Text = "\COMPARE_SIZE"
        '
        'ChecksumOption
        '
        Me.ChecksumOption.CheckOnClick = True
        Me.ChecksumOption.Name = "ChecksumOption"
        Me.ChecksumOption.Size = New System.Drawing.Size(162, 22)
        Me.ChecksumOption.Text = "\MD5"
        '
        'GroupOption
        '
        Me.GroupOption.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GroupTextBox})
        Me.GroupOption.Name = "GroupOption"
        Me.GroupOption.Size = New System.Drawing.Size(162, 22)
        Me.GroupOption.Text = "\GROUP"
        '
        'GroupTextBox
        '
        Me.GroupTextBox.Name = "GroupTextBox"
        Me.GroupTextBox.Size = New System.Drawing.Size(100, 23)
        '
        'SettingsForm
        '
        Me.AcceptButton = Me.SaveButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.CancelBtn
        Me.ClientSize = New System.Drawing.Size(724, 550)
        Me.Controls.Add(Me.ActionsPanel)
        Me.Controls.Add(Me.AdvancedBox)
        Me.Controls.Add(Me.ViewsBox)
        Me.Controls.Add(Me.DirectoriesBox)
        Me.Controls.Add(Me.SynchronizationMethodBox)
        Me.Controls.Add(Me.IncludeExcludeBox)
        Me.Controls.Add(Me.BottomDescLabel)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "SettingsForm"
        Me.ShowInTaskbar = False
        Me.Text = "\SETTINGS"
        Me.DirectoriesBox.ResumeLayout(False)
        Me.DirectoriesBox.PerformLayout()
        Me.ViewsLayoutPanel.ResumeLayout(False)
        Me.ViewsLayoutPanel.PerformLayout()
        Me.LeftViewPanel.ResumeLayout(False)
        Me.TreeViewMenuStrip.ResumeLayout(False)
        Me.RightViewPanel.ResumeLayout(False)
        Me.ViewsBox.ResumeLayout(False)
        Me.SynchronizationMethodBox.ResumeLayout(False)
        Me.SynchronizationMethodBox.PerformLayout()
        Me.MethodLayoutPanel.ResumeLayout(False)
        Me.MethodLayoutPanel.PerformLayout()
        Me.IncludeExcludeBox.ResumeLayout(False)
        Me.IncludeExcludeBox.PerformLayout()
        Me.IncludeExcludeLayoutPanel.ResumeLayout(False)
        Me.IncludeExcludeLayoutPanel.PerformLayout()
        Me.ActionsPanel.ResumeLayout(False)
        Me.AdvancedBox.ResumeLayout(False)
        Me.AdvancedBox.PerformLayout()
        CType(Me.TimeOffset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ExpertMenu.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FromTextBox As System.Windows.Forms.TextBox
    Friend WithEvents DirectoriesBox As System.Windows.Forms.GroupBox
    Friend WithEvents ToTextBox As System.Windows.Forms.TextBox
    Friend WithEvents BrowseRButton As System.Windows.Forms.Button
    Friend WithEvents BrowseLButton As System.Windows.Forms.Button
    Friend WithEvents FromLabel As System.Windows.Forms.Label
    Friend WithEvents ToLabel As System.Windows.Forms.Label
    Friend WithEvents ViewsLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LeftView As System.Windows.Forms.TreeView
    Friend WithEvents RightView As System.Windows.Forms.TreeView
    Friend WithEvents ReloadButton As System.Windows.Forms.Button
    Friend WithEvents LeftViewLabel As System.Windows.Forms.Label
    Friend WithEvents RightViewLabel As System.Windows.Forms.Label
    Friend WithEvents ViewsBox As System.Windows.Forms.GroupBox
    Friend WithEvents SynchronizationMethodBox As System.Windows.Forms.GroupBox
    Friend WithEvents LRMirrorMethodOption As System.Windows.Forms.RadioButton
    Friend WithEvents MethodLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LRIncrementalMethodOption As System.Windows.Forms.RadioButton
    Friend WithEvents TwoWaysIncrementalMethodOption As System.Windows.Forms.RadioButton
    Friend WithEvents IncludeExcludeBox As System.Windows.Forms.GroupBox
    Friend WithEvents IncludeExcludeLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ExcludedTypesTextBox As System.Windows.Forms.TextBox
    Friend WithEvents IncludedTypesTextBox As System.Windows.Forms.TextBox
    Friend WithEvents CopyAllFilesCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents IncludeFilesOption As System.Windows.Forms.RadioButton
    Friend WithEvents ExcludeFilesOption As System.Windows.Forms.RadioButton
    Friend WithEvents ActionsPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents CancelBtn As System.Windows.Forms.Button
    Friend WithEvents SaveButton As System.Windows.Forms.Button
    Friend WithEvents FolderBrowser As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents TreeViewMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents DontSynchronizeMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReplicateEmptyDirectoriesOption As System.Windows.Forms.CheckBox
    Friend WithEvents PropagateUpdatesOption As System.Windows.Forms.CheckBox
    Friend WithEvents AdvancedBox As System.Windows.Forms.GroupBox
    Friend WithEvents BottomDescLabel As System.Windows.Forms.Label
    Friend WithEvents StrictMirrorOption As System.Windows.Forms.CheckBox
    Friend WithEvents TimeOffset As System.Windows.Forms.NumericUpDown
    Friend WithEvents TimeOffsetHoursLabel As System.Windows.Forms.Label
    Friend WithEvents TimeOffsetLabel As System.Windows.Forms.Label
    Friend WithEvents StrictDateComparisonOption As System.Windows.Forms.CheckBox
    Friend WithEvents SwapButton As System.Windows.Forms.Button
    Friend WithEvents SynchronizeFolderAndSubfoldersMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SynchronizeFilesOnlyMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SynchronizeSubFoldersOnlyMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Loading As System.Windows.Forms.Label
    Friend WithEvents HelpLink As System.Windows.Forms.Label
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToggleMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RightReloadButton As System.Windows.Forms.Button
    Friend WithEvents LeftReloadButton As System.Windows.Forms.Button
    Friend WithEvents LeftViewPanel As System.Windows.Forms.Panel
    Friend WithEvents RightViewPanel As System.Windows.Forms.Panel
    Friend WithEvents MoreLabel As System.Windows.Forms.LinkLabel
    Friend WithEvents ExpertMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents CheckFileSizeOption As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ChecksumOption As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GroupOption As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GroupTextBox As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CreateDestOption As System.Windows.Forms.ToolStripMenuItem
End Class
