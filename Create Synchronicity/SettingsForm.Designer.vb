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
        Me.Settings_FromTextBox = New System.Windows.Forms.TextBox()
        Me.Settings_DirectoriesBox = New System.Windows.Forms.GroupBox()
        Me.Settings_SwapButton = New System.Windows.Forms.Button()
        Me.Settings_BrowseRButton = New System.Windows.Forms.Button()
        Me.Settings_BrowseLButton = New System.Windows.Forms.Button()
        Me.Settings_ToTextBox = New System.Windows.Forms.TextBox()
        Me.Settings_ToLabel = New System.Windows.Forms.Label()
        Me.Settings_FromLabel = New System.Windows.Forms.Label()
        Me.Settings_ViewsLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.Settings_LeftView = New System.Windows.Forms.TreeView()
        Me.Settings_TreeViewMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.Settings_SynchronizeFolderAndSubfoldersMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.Settings_SynchronizeFilesOnlyMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Settings_SynchronizeSubFoldersOnlyMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.Settings_DontSynchronizeMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Settings_RightView = New System.Windows.Forms.TreeView()
        Me.Settings_ReloadButton = New System.Windows.Forms.Button()
        Me.Settings_LeftViewLabel = New System.Windows.Forms.Label()
        Me.Settings_RightViewLabel = New System.Windows.Forms.Label()
        Me.Settings_Loading = New System.Windows.Forms.Label()
        Me.Settings_ViewsBox = New System.Windows.Forms.GroupBox()
        Me.Settings_SynchronizationMethodBox = New System.Windows.Forms.GroupBox()
        Me.Settings_StrictMirrorOption = New System.Windows.Forms.CheckBox()
        Me.Settings_MethodLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.Settings_TwoWaysIncrementalMethodOption = New System.Windows.Forms.RadioButton()
        Me.Settings_LRIncrementalMethodOption = New System.Windows.Forms.RadioButton()
        Me.Settings_LRMirrorMethodOption = New System.Windows.Forms.RadioButton()
        Me.Settings_DescriptionLabel = New System.Windows.Forms.Label()
        Me.Settings_Tips = New System.Windows.Forms.Label()
        Me.Settings_IncludeExcludeBox = New System.Windows.Forms.GroupBox()
        Me.Settings_CopyAllFilesCheckBox = New System.Windows.Forms.CheckBox()
        Me.Settings_IncludeExcludeLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.Settings_IncludedTypesTextBox = New System.Windows.Forms.TextBox()
        Me.Settings_ExcludedTypesTextBox = New System.Windows.Forms.TextBox()
        Me.Settings_IncludeFilesOption = New System.Windows.Forms.RadioButton()
        Me.Settings_ExcludeFilesOption = New System.Windows.Forms.RadioButton()
        Me.Settings_ReplicateEmptyDirectoriesOption = New System.Windows.Forms.CheckBox()
        Me.Settings_PropagateUpdatesOption = New System.Windows.Forms.CheckBox()
        Me.Settings_ActionsPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.Settings_CancelButton = New System.Windows.Forms.Button()
        Me.Settings_SaveButton = New System.Windows.Forms.Button()
        Me.Settings_FolderBrowser = New System.Windows.Forms.FolderBrowserDialog()
        Me.Settings_ComputeHashOption = New System.Windows.Forms.CheckBox()
        Me.Settings_AdvancedBox = New System.Windows.Forms.GroupBox()
        Me.Settings_StrictDateComparisonOption = New System.Windows.Forms.CheckBox()
        Me.Settings_TimeOffsetLabel = New System.Windows.Forms.Label()
        Me.Settings_TimeOffset = New System.Windows.Forms.NumericUpDown()
        Me.Settings_TimeOffsetHoursLabel = New System.Windows.Forms.Label()
        Me.Settings_BottomDescLabel = New System.Windows.Forms.Label()
        Me.Settings_DirectoriesBox.SuspendLayout()
        Me.Settings_ViewsLayoutPanel.SuspendLayout()
        Me.Settings_TreeViewMenuStrip.SuspendLayout()
        Me.Settings_ViewsBox.SuspendLayout()
        Me.Settings_SynchronizationMethodBox.SuspendLayout()
        Me.Settings_MethodLayoutPanel.SuspendLayout()
        Me.Settings_IncludeExcludeBox.SuspendLayout()
        Me.Settings_IncludeExcludeLayoutPanel.SuspendLayout()
        Me.Settings_ActionsPanel.SuspendLayout()
        Me.Settings_AdvancedBox.SuspendLayout()
        CType(Me.Settings_TimeOffset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Settings_FromTextBox
        '
        Me.Settings_FromTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Settings_FromTextBox.Location = New System.Drawing.Point(62, 19)
        Me.Settings_FromTextBox.Name = "Settings_FromTextBox"
        Me.Settings_FromTextBox.Size = New System.Drawing.Size(509, 21)
        Me.Settings_FromTextBox.TabIndex = 1
        '
        'Settings_DirectoriesBox
        '
        Me.Settings_DirectoriesBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Settings_DirectoriesBox.Controls.Add(Me.Settings_SwapButton)
        Me.Settings_DirectoriesBox.Controls.Add(Me.Settings_BrowseRButton)
        Me.Settings_DirectoriesBox.Controls.Add(Me.Settings_BrowseLButton)
        Me.Settings_DirectoriesBox.Controls.Add(Me.Settings_ToTextBox)
        Me.Settings_DirectoriesBox.Controls.Add(Me.Settings_FromTextBox)
        Me.Settings_DirectoriesBox.Controls.Add(Me.Settings_ToLabel)
        Me.Settings_DirectoriesBox.Controls.Add(Me.Settings_FromLabel)
        Me.Settings_DirectoriesBox.Location = New System.Drawing.Point(12, 12)
        Me.Settings_DirectoriesBox.Name = "Settings_DirectoriesBox"
        Me.Settings_DirectoriesBox.Size = New System.Drawing.Size(674, 71)
        Me.Settings_DirectoriesBox.TabIndex = 0
        Me.Settings_DirectoriesBox.TabStop = False
        Me.Settings_DirectoriesBox.Text = "\DIRECTORIES"
        '
        'Settings_SwapButton
        '
        Me.Settings_SwapButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Settings_SwapButton.Location = New System.Drawing.Point(612, 19)
        Me.Settings_SwapButton.Name = "Settings_SwapButton"
        Me.Settings_SwapButton.Size = New System.Drawing.Size(56, 49)
        Me.Settings_SwapButton.TabIndex = 6
        Me.Settings_SwapButton.Text = "\SWAP"
        Me.Settings_SwapButton.UseVisualStyleBackColor = True
        '
        'Settings_BrowseRButton
        '
        Me.Settings_BrowseRButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Settings_BrowseRButton.Location = New System.Drawing.Point(577, 44)
        Me.Settings_BrowseRButton.Name = "Settings_BrowseRButton"
        Me.Settings_BrowseRButton.Size = New System.Drawing.Size(29, 24)
        Me.Settings_BrowseRButton.TabIndex = 5
        Me.Settings_BrowseRButton.Text = "..."
        Me.Settings_BrowseRButton.UseVisualStyleBackColor = True
        '
        'Settings_BrowseLButton
        '
        Me.Settings_BrowseLButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Settings_BrowseLButton.Location = New System.Drawing.Point(577, 18)
        Me.Settings_BrowseLButton.Name = "Settings_BrowseLButton"
        Me.Settings_BrowseLButton.Size = New System.Drawing.Size(29, 24)
        Me.Settings_BrowseLButton.TabIndex = 2
        Me.Settings_BrowseLButton.Text = "..."
        Me.Settings_BrowseLButton.UseVisualStyleBackColor = True
        '
        'Settings_ToTextBox
        '
        Me.Settings_ToTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Settings_ToTextBox.Location = New System.Drawing.Point(62, 45)
        Me.Settings_ToTextBox.Name = "Settings_ToTextBox"
        Me.Settings_ToTextBox.Size = New System.Drawing.Size(509, 21)
        Me.Settings_ToTextBox.TabIndex = 4
        '
        'Settings_ToLabel
        '
        Me.Settings_ToLabel.Location = New System.Drawing.Point(6, 44)
        Me.Settings_ToLabel.Name = "Settings_ToLabel"
        Me.Settings_ToLabel.Size = New System.Drawing.Size(59, 21)
        Me.Settings_ToLabel.TabIndex = 3
        Me.Settings_ToLabel.Text = "\TO"
        Me.Settings_ToLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Settings_FromLabel
        '
        Me.Settings_FromLabel.Location = New System.Drawing.Point(3, 18)
        Me.Settings_FromLabel.Name = "Settings_FromLabel"
        Me.Settings_FromLabel.Size = New System.Drawing.Size(62, 21)
        Me.Settings_FromLabel.TabIndex = 0
        Me.Settings_FromLabel.Text = "\FROM"
        Me.Settings_FromLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Settings_ViewsLayoutPanel
        '
        Me.Settings_ViewsLayoutPanel.ColumnCount = 3
        Me.Settings_ViewsLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.Settings_ViewsLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.Settings_ViewsLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.Settings_ViewsLayoutPanel.Controls.Add(Me.Settings_LeftView, 0, 1)
        Me.Settings_ViewsLayoutPanel.Controls.Add(Me.Settings_RightView, 2, 1)
        Me.Settings_ViewsLayoutPanel.Controls.Add(Me.Settings_ReloadButton, 1, 1)
        Me.Settings_ViewsLayoutPanel.Controls.Add(Me.Settings_LeftViewLabel, 0, 0)
        Me.Settings_ViewsLayoutPanel.Controls.Add(Me.Settings_RightViewLabel, 2, 0)
        Me.Settings_ViewsLayoutPanel.Controls.Add(Me.Settings_Loading, 1, 2)
        Me.Settings_ViewsLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Settings_ViewsLayoutPanel.Location = New System.Drawing.Point(3, 17)
        Me.Settings_ViewsLayoutPanel.Name = "Settings_ViewsLayoutPanel"
        Me.Settings_ViewsLayoutPanel.RowCount = 3
        Me.Settings_ViewsLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.Settings_ViewsLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.Settings_ViewsLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.Settings_ViewsLayoutPanel.Size = New System.Drawing.Size(668, 140)
        Me.Settings_ViewsLayoutPanel.TabIndex = 0
        '
        'Settings_LeftView
        '
        Me.Settings_LeftView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Settings_LeftView.CheckBoxes = True
        Me.Settings_LeftView.ContextMenuStrip = Me.Settings_TreeViewMenuStrip
        Me.Settings_LeftView.Location = New System.Drawing.Point(3, 16)
        Me.Settings_LeftView.Name = "Settings_LeftView"
        Me.Settings_ViewsLayoutPanel.SetRowSpan(Me.Settings_LeftView, 2)
        Me.Settings_LeftView.Size = New System.Drawing.Size(310, 121)
        Me.Settings_LeftView.TabIndex = 1
        '
        'Settings_TreeViewMenuStrip
        '
        Me.Settings_TreeViewMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Settings_SynchronizeFolderAndSubfoldersMenuItem, Me.ToolStripSeparator1, Me.Settings_SynchronizeFilesOnlyMenuItem, Me.Settings_SynchronizeSubFoldersOnlyMenuItem, Me.ToolStripSeparator2, Me.Settings_DontSynchronizeMenuItem})
        Me.Settings_TreeViewMenuStrip.Name = "Settings_TreeViewMenuStrip"
        Me.Settings_TreeViewMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.Settings_TreeViewMenuStrip.ShowImageMargin = False
        Me.Settings_TreeViewMenuStrip.Size = New System.Drawing.Size(201, 104)
        '
        'Settings_SynchronizeFolderAndSubfoldersMenuItem
        '
        Me.Settings_SynchronizeFolderAndSubfoldersMenuItem.Name = "Settings_SynchronizeFolderAndSubfoldersMenuItem"
        Me.Settings_SynchronizeFolderAndSubfoldersMenuItem.Size = New System.Drawing.Size(200, 22)
        Me.Settings_SynchronizeFolderAndSubfoldersMenuItem.Text = "\FOLDER_AND_SUBFOLDERS"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(197, 6)
        '
        'Settings_SynchronizeFilesOnlyMenuItem
        '
        Me.Settings_SynchronizeFilesOnlyMenuItem.Name = "Settings_SynchronizeFilesOnlyMenuItem"
        Me.Settings_SynchronizeFilesOnlyMenuItem.Size = New System.Drawing.Size(200, 22)
        Me.Settings_SynchronizeFilesOnlyMenuItem.Text = "\FILES_ONLY"
        '
        'Settings_SynchronizeSubFoldersOnlyMenuItem
        '
        Me.Settings_SynchronizeSubFoldersOnlyMenuItem.Name = "Settings_SynchronizeSubFoldersOnlyMenuItem"
        Me.Settings_SynchronizeSubFoldersOnlyMenuItem.Size = New System.Drawing.Size(200, 22)
        Me.Settings_SynchronizeSubFoldersOnlyMenuItem.Text = "\SUBFOLDERS_ONLY"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(197, 6)
        '
        'Settings_DontSynchronizeMenuItem
        '
        Me.Settings_DontSynchronizeMenuItem.Name = "Settings_DontSynchronizeMenuItem"
        Me.Settings_DontSynchronizeMenuItem.Size = New System.Drawing.Size(200, 22)
        Me.Settings_DontSynchronizeMenuItem.Text = "\NO_SYNC"
        '
        'Settings_RightView
        '
        Me.Settings_RightView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Settings_RightView.Location = New System.Drawing.Point(354, 16)
        Me.Settings_RightView.Name = "Settings_RightView"
        Me.Settings_ViewsLayoutPanel.SetRowSpan(Me.Settings_RightView, 2)
        Me.Settings_RightView.Size = New System.Drawing.Size(311, 121)
        Me.Settings_RightView.TabIndex = 3
        '
        'Settings_ReloadButton
        '
        Me.Settings_ReloadButton.Image = CType(resources.GetObject("Settings_ReloadButton.Image"), System.Drawing.Image)
        Me.Settings_ReloadButton.Location = New System.Drawing.Point(319, 16)
        Me.Settings_ReloadButton.Name = "Settings_ReloadButton"
        Me.Settings_ReloadButton.Size = New System.Drawing.Size(29, 29)
        Me.Settings_ReloadButton.TabIndex = 4
        Me.Settings_ReloadButton.UseVisualStyleBackColor = False
        '
        'Settings_LeftViewLabel
        '
        Me.Settings_LeftViewLabel.AutoSize = True
        Me.Settings_LeftViewLabel.Location = New System.Drawing.Point(3, 0)
        Me.Settings_LeftViewLabel.Name = "Settings_LeftViewLabel"
        Me.Settings_LeftViewLabel.Size = New System.Drawing.Size(74, 13)
        Me.Settings_LeftViewLabel.TabIndex = 0
        Me.Settings_LeftViewLabel.Text = "\LEFT_SIDE"
        '
        'Settings_RightViewLabel
        '
        Me.Settings_RightViewLabel.AutoSize = True
        Me.Settings_RightViewLabel.Location = New System.Drawing.Point(354, 0)
        Me.Settings_RightViewLabel.Name = "Settings_RightViewLabel"
        Me.Settings_RightViewLabel.Size = New System.Drawing.Size(85, 13)
        Me.Settings_RightViewLabel.TabIndex = 2
        Me.Settings_RightViewLabel.Text = "\RIGHT_SIDE"
        '
        'Settings_Loading
        '
        Me.Settings_Loading.Image = CType(resources.GetObject("Settings_Loading.Image"), System.Drawing.Image)
        Me.Settings_Loading.Location = New System.Drawing.Point(319, 48)
        Me.Settings_Loading.Name = "Settings_Loading"
        Me.Settings_Loading.Size = New System.Drawing.Size(29, 29)
        Me.Settings_Loading.TabIndex = 5
        Me.Settings_Loading.Visible = False
        '
        'Settings_ViewsBox
        '
        Me.Settings_ViewsBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Settings_ViewsBox.Controls.Add(Me.Settings_ViewsLayoutPanel)
        Me.Settings_ViewsBox.Location = New System.Drawing.Point(12, 89)
        Me.Settings_ViewsBox.Name = "Settings_ViewsBox"
        Me.Settings_ViewsBox.Size = New System.Drawing.Size(674, 160)
        Me.Settings_ViewsBox.TabIndex = 1
        Me.Settings_ViewsBox.TabStop = False
        Me.Settings_ViewsBox.Text = "\SUBDIRECTORIES"
        '
        'Settings_SynchronizationMethodBox
        '
        Me.Settings_SynchronizationMethodBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Settings_SynchronizationMethodBox.Controls.Add(Me.Settings_StrictMirrorOption)
        Me.Settings_SynchronizationMethodBox.Controls.Add(Me.Settings_MethodLayoutPanel)
        Me.Settings_SynchronizationMethodBox.Controls.Add(Me.Settings_DescriptionLabel)
        Me.Settings_SynchronizationMethodBox.Controls.Add(Me.Settings_Tips)
        Me.Settings_SynchronizationMethodBox.Location = New System.Drawing.Point(12, 255)
        Me.Settings_SynchronizationMethodBox.Name = "Settings_SynchronizationMethodBox"
        Me.Settings_SynchronizationMethodBox.Size = New System.Drawing.Size(674, 110)
        Me.Settings_SynchronizationMethodBox.TabIndex = 2
        Me.Settings_SynchronizationMethodBox.TabStop = False
        Me.Settings_SynchronizationMethodBox.Text = "\SYNC_METHOD"
        '
        'Settings_StrictMirrorOption
        '
        Me.Settings_StrictMirrorOption.AutoSize = True
        Me.Settings_StrictMirrorOption.Location = New System.Drawing.Point(6, 87)
        Me.Settings_StrictMirrorOption.Name = "Settings_StrictMirrorOption"
        Me.Settings_StrictMirrorOption.Size = New System.Drawing.Size(169, 17)
        Me.Settings_StrictMirrorOption.TabIndex = 2
        Me.Settings_StrictMirrorOption.Text = "\STRICT_MIRROR_DESC"
        Me.Settings_StrictMirrorOption.UseVisualStyleBackColor = True
        '
        'Settings_MethodLayoutPanel
        '
        Me.Settings_MethodLayoutPanel.ColumnCount = 3
        Me.Settings_MethodLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.Settings_MethodLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
        Me.Settings_MethodLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
        Me.Settings_MethodLayoutPanel.Controls.Add(Me.Settings_TwoWaysIncrementalMethodOption, 2, 0)
        Me.Settings_MethodLayoutPanel.Controls.Add(Me.Settings_LRIncrementalMethodOption, 1, 0)
        Me.Settings_MethodLayoutPanel.Controls.Add(Me.Settings_LRMirrorMethodOption, 0, 0)
        Me.Settings_MethodLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.Settings_MethodLayoutPanel.Location = New System.Drawing.Point(3, 17)
        Me.Settings_MethodLayoutPanel.Name = "Settings_MethodLayoutPanel"
        Me.Settings_MethodLayoutPanel.RowCount = 1
        Me.Settings_MethodLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.Settings_MethodLayoutPanel.Size = New System.Drawing.Size(668, 23)
        Me.Settings_MethodLayoutPanel.TabIndex = 0
        '
        'Settings_TwoWaysIncrementalMethodOption
        '
        Me.Settings_TwoWaysIncrementalMethodOption.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Settings_TwoWaysIncrementalMethodOption.AutoSize = True
        Me.Settings_TwoWaysIncrementalMethodOption.Location = New System.Drawing.Point(466, 3)
        Me.Settings_TwoWaysIncrementalMethodOption.Name = "Settings_TwoWaysIncrementalMethodOption"
        Me.Settings_TwoWaysIncrementalMethodOption.Size = New System.Drawing.Size(180, 17)
        Me.Settings_TwoWaysIncrementalMethodOption.TabIndex = 2
        Me.Settings_TwoWaysIncrementalMethodOption.Tag = "\TWOWAYS_INCREMENTAL_TAG"
        Me.Settings_TwoWaysIncrementalMethodOption.Text = "\TWOWAYS_INCREMENTAL"
        Me.Settings_TwoWaysIncrementalMethodOption.UseVisualStyleBackColor = True
        '
        'Settings_LRIncrementalMethodOption
        '
        Me.Settings_LRIncrementalMethodOption.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Settings_LRIncrementalMethodOption.AutoSize = True
        Me.Settings_LRIncrementalMethodOption.Location = New System.Drawing.Point(266, 3)
        Me.Settings_LRIncrementalMethodOption.Name = "Settings_LRIncrementalMethodOption"
        Me.Settings_LRIncrementalMethodOption.Size = New System.Drawing.Size(133, 17)
        Me.Settings_LRIncrementalMethodOption.TabIndex = 1
        Me.Settings_LRIncrementalMethodOption.Tag = "\LR_INCREMENTAL_TAG"
        Me.Settings_LRIncrementalMethodOption.Text = "\LR_INCREMENTAL"
        Me.Settings_LRIncrementalMethodOption.UseVisualStyleBackColor = True
        '
        'Settings_LRMirrorMethodOption
        '
        Me.Settings_LRMirrorMethodOption.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Settings_LRMirrorMethodOption.AutoSize = True
        Me.Settings_LRMirrorMethodOption.Checked = True
        Me.Settings_LRMirrorMethodOption.Location = New System.Drawing.Point(62, 3)
        Me.Settings_LRMirrorMethodOption.Name = "Settings_LRMirrorMethodOption"
        Me.Settings_LRMirrorMethodOption.Size = New System.Drawing.Size(98, 17)
        Me.Settings_LRMirrorMethodOption.TabIndex = 0
        Me.Settings_LRMirrorMethodOption.TabStop = True
        Me.Settings_LRMirrorMethodOption.Tag = "\LR_MIRROR_TAG"
        Me.Settings_LRMirrorMethodOption.Text = "\LR_MIRROR"
        Me.Settings_LRMirrorMethodOption.UseVisualStyleBackColor = True
        '
        'Settings_DescriptionLabel
        '
        Me.Settings_DescriptionLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Settings_DescriptionLabel.Location = New System.Drawing.Point(6, 43)
        Me.Settings_DescriptionLabel.Name = "Settings_DescriptionLabel"
        Me.Settings_DescriptionLabel.Size = New System.Drawing.Size(662, 64)
        Me.Settings_DescriptionLabel.TabIndex = 1
        Me.Settings_DescriptionLabel.Tag = "\MOUSE_OVER"
        Me.Settings_DescriptionLabel.Text = "\MOUSE_OVER"
        '
        'Settings_Tips
        '
        Me.Settings_Tips.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Settings_Tips.Location = New System.Drawing.Point(3, 17)
        Me.Settings_Tips.Name = "Settings_Tips"
        Me.Settings_Tips.Size = New System.Drawing.Size(668, 90)
        Me.Settings_Tips.TabIndex = 8
        Me.Settings_Tips.Text = "\SETTINGS_TIPS"
        Me.Settings_Tips.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Settings_Tips.Visible = False
        '
        'Settings_IncludeExcludeBox
        '
        Me.Settings_IncludeExcludeBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Settings_IncludeExcludeBox.Controls.Add(Me.Settings_CopyAllFilesCheckBox)
        Me.Settings_IncludeExcludeBox.Controls.Add(Me.Settings_IncludeExcludeLayoutPanel)
        Me.Settings_IncludeExcludeBox.Controls.Add(Me.Settings_ReplicateEmptyDirectoriesOption)
        Me.Settings_IncludeExcludeBox.Location = New System.Drawing.Point(12, 371)
        Me.Settings_IncludeExcludeBox.Name = "Settings_IncludeExcludeBox"
        Me.Settings_IncludeExcludeBox.Size = New System.Drawing.Size(674, 97)
        Me.Settings_IncludeExcludeBox.TabIndex = 3
        Me.Settings_IncludeExcludeBox.TabStop = False
        Me.Settings_IncludeExcludeBox.Text = "\INCLUDE_EXCLUDE"
        '
        'Settings_CopyAllFilesCheckBox
        '
        Me.Settings_CopyAllFilesCheckBox.AutoSize = True
        Me.Settings_CopyAllFilesCheckBox.Checked = True
        Me.Settings_CopyAllFilesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Settings_CopyAllFilesCheckBox.Location = New System.Drawing.Point(6, 20)
        Me.Settings_CopyAllFilesCheckBox.Name = "Settings_CopyAllFilesCheckBox"
        Me.Settings_CopyAllFilesCheckBox.Size = New System.Drawing.Size(85, 17)
        Me.Settings_CopyAllFilesCheckBox.TabIndex = 0
        Me.Settings_CopyAllFilesCheckBox.Text = "\ALL_FILES"
        Me.Settings_CopyAllFilesCheckBox.UseVisualStyleBackColor = True
        '
        'Settings_IncludeExcludeLayoutPanel
        '
        Me.Settings_IncludeExcludeLayoutPanel.ColumnCount = 2
        Me.Settings_IncludeExcludeLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.Settings_IncludeExcludeLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.Settings_IncludeExcludeLayoutPanel.Controls.Add(Me.Settings_IncludedTypesTextBox, 0, 1)
        Me.Settings_IncludeExcludeLayoutPanel.Controls.Add(Me.Settings_ExcludedTypesTextBox, 0, 1)
        Me.Settings_IncludeExcludeLayoutPanel.Controls.Add(Me.Settings_IncludeFilesOption, 0, 0)
        Me.Settings_IncludeExcludeLayoutPanel.Controls.Add(Me.Settings_ExcludeFilesOption, 1, 0)
        Me.Settings_IncludeExcludeLayoutPanel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Settings_IncludeExcludeLayoutPanel.Enabled = False
        Me.Settings_IncludeExcludeLayoutPanel.Location = New System.Drawing.Point(3, 43)
        Me.Settings_IncludeExcludeLayoutPanel.Name = "Settings_IncludeExcludeLayoutPanel"
        Me.Settings_IncludeExcludeLayoutPanel.RowCount = 2
        Me.Settings_IncludeExcludeLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.Settings_IncludeExcludeLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.Settings_IncludeExcludeLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.Settings_IncludeExcludeLayoutPanel.Size = New System.Drawing.Size(668, 51)
        Me.Settings_IncludeExcludeLayoutPanel.TabIndex = 2
        '
        'Settings_IncludedTypesTextBox
        '
        Me.Settings_IncludedTypesTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Settings_IncludedTypesTextBox.Location = New System.Drawing.Point(3, 26)
        Me.Settings_IncludedTypesTextBox.Name = "Settings_IncludedTypesTextBox"
        Me.Settings_IncludedTypesTextBox.Size = New System.Drawing.Size(328, 21)
        Me.Settings_IncludedTypesTextBox.TabIndex = 1
        '
        'Settings_ExcludedTypesTextBox
        '
        Me.Settings_ExcludedTypesTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Settings_ExcludedTypesTextBox.Location = New System.Drawing.Point(337, 26)
        Me.Settings_ExcludedTypesTextBox.Name = "Settings_ExcludedTypesTextBox"
        Me.Settings_ExcludedTypesTextBox.Size = New System.Drawing.Size(328, 21)
        Me.Settings_ExcludedTypesTextBox.TabIndex = 3
        '
        'Settings_IncludeFilesOption
        '
        Me.Settings_IncludeFilesOption.AutoSize = True
        Me.Settings_IncludeFilesOption.Location = New System.Drawing.Point(3, 3)
        Me.Settings_IncludeFilesOption.Name = "Settings_IncludeFilesOption"
        Me.Settings_IncludeFilesOption.Size = New System.Drawing.Size(104, 17)
        Me.Settings_IncludeFilesOption.TabIndex = 0
        Me.Settings_IncludeFilesOption.TabStop = True
        Me.Settings_IncludeFilesOption.Text = "\THESE_ONLY"
        Me.Settings_IncludeFilesOption.UseVisualStyleBackColor = True
        '
        'Settings_ExcludeFilesOption
        '
        Me.Settings_ExcludeFilesOption.AutoSize = True
        Me.Settings_ExcludeFilesOption.Location = New System.Drawing.Point(337, 3)
        Me.Settings_ExcludeFilesOption.Name = "Settings_ExcludeFilesOption"
        Me.Settings_ExcludeFilesOption.Size = New System.Drawing.Size(128, 17)
        Me.Settings_ExcludeFilesOption.TabIndex = 2
        Me.Settings_ExcludeFilesOption.TabStop = True
        Me.Settings_ExcludeFilesOption.Text = "\EXCLUDE_THESE"
        Me.Settings_ExcludeFilesOption.UseVisualStyleBackColor = True
        '
        'Settings_ReplicateEmptyDirectoriesOption
        '
        Me.Settings_ReplicateEmptyDirectoriesOption.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Settings_ReplicateEmptyDirectoriesOption.AutoSize = True
        Me.Settings_ReplicateEmptyDirectoriesOption.Checked = True
        Me.Settings_ReplicateEmptyDirectoriesOption.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Settings_ReplicateEmptyDirectoriesOption.Location = New System.Drawing.Point(538, 20)
        Me.Settings_ReplicateEmptyDirectoriesOption.Name = "Settings_ReplicateEmptyDirectoriesOption"
        Me.Settings_ReplicateEmptyDirectoriesOption.Size = New System.Drawing.Size(133, 17)
        Me.Settings_ReplicateEmptyDirectoriesOption.TabIndex = 1
        Me.Settings_ReplicateEmptyDirectoriesOption.Text = "\REPLICATE_EMPTY"
        Me.Settings_ReplicateEmptyDirectoriesOption.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Settings_ReplicateEmptyDirectoriesOption.UseVisualStyleBackColor = True
        '
        'Settings_PropagateUpdatesOption
        '
        Me.Settings_PropagateUpdatesOption.AutoSize = True
        Me.Settings_PropagateUpdatesOption.Checked = True
        Me.Settings_PropagateUpdatesOption.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Settings_PropagateUpdatesOption.Location = New System.Drawing.Point(6, 20)
        Me.Settings_PropagateUpdatesOption.Name = "Settings_PropagateUpdatesOption"
        Me.Settings_PropagateUpdatesOption.Size = New System.Drawing.Size(97, 17)
        Me.Settings_PropagateUpdatesOption.TabIndex = 0
        Me.Settings_PropagateUpdatesOption.Tag = "\PROPAGATE_TAG"
        Me.Settings_PropagateUpdatesOption.Text = "\PROPAGATE"
        Me.Settings_PropagateUpdatesOption.UseVisualStyleBackColor = True
        '
        'Settings_ActionsPanel
        '
        Me.Settings_ActionsPanel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Settings_ActionsPanel.ColumnCount = 2
        Me.Settings_ActionsPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.Settings_ActionsPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.Settings_ActionsPanel.Controls.Add(Me.Settings_CancelButton, 1, 0)
        Me.Settings_ActionsPanel.Controls.Add(Me.Settings_SaveButton, 0, 0)
        Me.Settings_ActionsPanel.Location = New System.Drawing.Point(486, 548)
        Me.Settings_ActionsPanel.Name = "Settings_ActionsPanel"
        Me.Settings_ActionsPanel.RowCount = 1
        Me.Settings_ActionsPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.Settings_ActionsPanel.Size = New System.Drawing.Size(200, 31)
        Me.Settings_ActionsPanel.TabIndex = 6
        '
        'Settings_CancelButton
        '
        Me.Settings_CancelButton.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Settings_CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Settings_CancelButton.Location = New System.Drawing.Point(103, 3)
        Me.Settings_CancelButton.Name = "Settings_CancelButton"
        Me.Settings_CancelButton.Size = New System.Drawing.Size(94, 25)
        Me.Settings_CancelButton.TabIndex = 1
        Me.Settings_CancelButton.Text = "\CANCEL"
        Me.Settings_CancelButton.UseVisualStyleBackColor = True
        '
        'Settings_SaveButton
        '
        Me.Settings_SaveButton.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Settings_SaveButton.Location = New System.Drawing.Point(3, 3)
        Me.Settings_SaveButton.Name = "Settings_SaveButton"
        Me.Settings_SaveButton.Size = New System.Drawing.Size(94, 25)
        Me.Settings_SaveButton.TabIndex = 0
        Me.Settings_SaveButton.Text = "\SAVE"
        Me.Settings_SaveButton.UseVisualStyleBackColor = True
        '
        'Settings_FolderBrowser
        '
        Me.Settings_FolderBrowser.RootFolder = System.Environment.SpecialFolder.MyComputer
        '
        'Settings_ComputeHashOption
        '
        Me.Settings_ComputeHashOption.AutoSize = True
        Me.Settings_ComputeHashOption.Location = New System.Drawing.Point(6, 44)
        Me.Settings_ComputeHashOption.Name = "Settings_ComputeHashOption"
        Me.Settings_ComputeHashOption.Size = New System.Drawing.Size(120, 17)
        Me.Settings_ComputeHashOption.TabIndex = 1
        Me.Settings_ComputeHashOption.Tag = "\COMPUTEHASH_TAG"
        Me.Settings_ComputeHashOption.Text = "\COMPUTE_HASH"
        Me.Settings_ComputeHashOption.UseVisualStyleBackColor = True
        '
        'Settings_AdvancedBox
        '
        Me.Settings_AdvancedBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Settings_AdvancedBox.Controls.Add(Me.Settings_StrictDateComparisonOption)
        Me.Settings_AdvancedBox.Controls.Add(Me.Settings_TimeOffsetLabel)
        Me.Settings_AdvancedBox.Controls.Add(Me.Settings_TimeOffset)
        Me.Settings_AdvancedBox.Controls.Add(Me.Settings_PropagateUpdatesOption)
        Me.Settings_AdvancedBox.Controls.Add(Me.Settings_ComputeHashOption)
        Me.Settings_AdvancedBox.Controls.Add(Me.Settings_TimeOffsetHoursLabel)
        Me.Settings_AdvancedBox.Location = New System.Drawing.Point(12, 474)
        Me.Settings_AdvancedBox.Name = "Settings_AdvancedBox"
        Me.Settings_AdvancedBox.Size = New System.Drawing.Size(674, 68)
        Me.Settings_AdvancedBox.TabIndex = 4
        Me.Settings_AdvancedBox.TabStop = False
        Me.Settings_AdvancedBox.Text = "\ADVANCED_OPTS"
        '
        'Settings_StrictDateComparisonOption
        '
        Me.Settings_StrictDateComparisonOption.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Settings_StrictDateComparisonOption.AutoSize = True
        Me.Settings_StrictDateComparisonOption.Checked = True
        Me.Settings_StrictDateComparisonOption.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Settings_StrictDateComparisonOption.Location = New System.Drawing.Point(520, 20)
        Me.Settings_StrictDateComparisonOption.Name = "Settings_StrictDateComparisonOption"
        Me.Settings_StrictDateComparisonOption.Size = New System.Drawing.Size(148, 17)
        Me.Settings_StrictDateComparisonOption.TabIndex = 2
        Me.Settings_StrictDateComparisonOption.Tag = "\STRICTCOMPARISON_TAG"
        Me.Settings_StrictDateComparisonOption.Text = "\STRICT_COMPARISON"
        Me.Settings_StrictDateComparisonOption.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Settings_StrictDateComparisonOption.UseVisualStyleBackColor = True
        '
        'Settings_TimeOffsetLabel
        '
        Me.Settings_TimeOffsetLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Settings_TimeOffsetLabel.Location = New System.Drawing.Point(425, 45)
        Me.Settings_TimeOffsetLabel.Name = "Settings_TimeOffsetLabel"
        Me.Settings_TimeOffsetLabel.Size = New System.Drawing.Size(143, 13)
        Me.Settings_TimeOffsetLabel.TabIndex = 3
        Me.Settings_TimeOffsetLabel.Text = "\TIME_OFFSET"
        Me.Settings_TimeOffsetLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Settings_TimeOffset
        '
        Me.Settings_TimeOffset.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Settings_TimeOffset.Location = New System.Drawing.Point(574, 43)
        Me.Settings_TimeOffset.Maximum = New Decimal(New Integer() {2, 0, 0, 0})
        Me.Settings_TimeOffset.Minimum = New Decimal(New Integer() {2, 0, 0, -2147483648})
        Me.Settings_TimeOffset.Name = "Settings_TimeOffset"
        Me.Settings_TimeOffset.Size = New System.Drawing.Size(35, 21)
        Me.Settings_TimeOffset.TabIndex = 4
        '
        'Settings_TimeOffsetHoursLabel
        '
        Me.Settings_TimeOffsetHoursLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Settings_TimeOffsetHoursLabel.AutoSize = True
        Me.Settings_TimeOffsetHoursLabel.Location = New System.Drawing.Point(615, 45)
        Me.Settings_TimeOffsetHoursLabel.Name = "Settings_TimeOffsetHoursLabel"
        Me.Settings_TimeOffsetHoursLabel.Size = New System.Drawing.Size(53, 13)
        Me.Settings_TimeOffsetHoursLabel.TabIndex = 5
        Me.Settings_TimeOffsetHoursLabel.Text = "\HOURS"
        '
        'Settings_BottomDescLabel
        '
        Me.Settings_BottomDescLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Settings_BottomDescLabel.Location = New System.Drawing.Point(12, 548)
        Me.Settings_BottomDescLabel.Name = "Settings_BottomDescLabel"
        Me.Settings_BottomDescLabel.Size = New System.Drawing.Size(471, 31)
        Me.Settings_BottomDescLabel.TabIndex = 5
        '
        'SettingsForm
        '
        Me.AcceptButton = Me.Settings_SaveButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Settings_CancelButton
        Me.ClientSize = New System.Drawing.Size(698, 591)
        Me.Controls.Add(Me.Settings_BottomDescLabel)
        Me.Controls.Add(Me.Settings_ActionsPanel)
        Me.Controls.Add(Me.Settings_AdvancedBox)
        Me.Controls.Add(Me.Settings_ViewsBox)
        Me.Controls.Add(Me.Settings_DirectoriesBox)
        Me.Controls.Add(Me.Settings_SynchronizationMethodBox)
        Me.Controls.Add(Me.Settings_IncludeExcludeBox)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "SettingsForm"
        Me.ShowInTaskbar = False
        Me.Text = "\SETTINGS"
        Me.Settings_DirectoriesBox.ResumeLayout(False)
        Me.Settings_DirectoriesBox.PerformLayout()
        Me.Settings_ViewsLayoutPanel.ResumeLayout(False)
        Me.Settings_ViewsLayoutPanel.PerformLayout()
        Me.Settings_TreeViewMenuStrip.ResumeLayout(False)
        Me.Settings_ViewsBox.ResumeLayout(False)
        Me.Settings_SynchronizationMethodBox.ResumeLayout(False)
        Me.Settings_SynchronizationMethodBox.PerformLayout()
        Me.Settings_MethodLayoutPanel.ResumeLayout(False)
        Me.Settings_MethodLayoutPanel.PerformLayout()
        Me.Settings_IncludeExcludeBox.ResumeLayout(False)
        Me.Settings_IncludeExcludeBox.PerformLayout()
        Me.Settings_IncludeExcludeLayoutPanel.ResumeLayout(False)
        Me.Settings_IncludeExcludeLayoutPanel.PerformLayout()
        Me.Settings_ActionsPanel.ResumeLayout(False)
        Me.Settings_AdvancedBox.ResumeLayout(False)
        Me.Settings_AdvancedBox.PerformLayout()
        CType(Me.Settings_TimeOffset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Settings_FromTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Settings_DirectoriesBox As System.Windows.Forms.GroupBox
    Friend WithEvents Settings_ToTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Settings_BrowseRButton As System.Windows.Forms.Button
    Friend WithEvents Settings_BrowseLButton As System.Windows.Forms.Button
    Friend WithEvents Settings_FromLabel As System.Windows.Forms.Label
    Friend WithEvents Settings_ToLabel As System.Windows.Forms.Label
    Friend WithEvents Settings_ViewsLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Settings_LeftView As System.Windows.Forms.TreeView
    Friend WithEvents Settings_RightView As System.Windows.Forms.TreeView
    Friend WithEvents Settings_ReloadButton As System.Windows.Forms.Button
    Friend WithEvents Settings_LeftViewLabel As System.Windows.Forms.Label
    Friend WithEvents Settings_RightViewLabel As System.Windows.Forms.Label
    Friend WithEvents Settings_ViewsBox As System.Windows.Forms.GroupBox
    Friend WithEvents Settings_SynchronizationMethodBox As System.Windows.Forms.GroupBox
    Friend WithEvents Settings_DescriptionLabel As System.Windows.Forms.Label
    Friend WithEvents Settings_LRMirrorMethodOption As System.Windows.Forms.RadioButton
    Friend WithEvents Settings_MethodLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Settings_LRIncrementalMethodOption As System.Windows.Forms.RadioButton
    Friend WithEvents Settings_TwoWaysIncrementalMethodOption As System.Windows.Forms.RadioButton
    Friend WithEvents Settings_IncludeExcludeBox As System.Windows.Forms.GroupBox
    Friend WithEvents Settings_IncludeExcludeLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Settings_ExcludedTypesTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Settings_IncludedTypesTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Settings_CopyAllFilesCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents Settings_IncludeFilesOption As System.Windows.Forms.RadioButton
    Friend WithEvents Settings_ExcludeFilesOption As System.Windows.Forms.RadioButton
    Friend WithEvents Settings_ActionsPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Settings_CancelButton As System.Windows.Forms.Button
    Friend WithEvents Settings_SaveButton As System.Windows.Forms.Button
    Friend WithEvents Settings_FolderBrowser As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents Settings_TreeViewMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents Settings_DontSynchronizeMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Settings_ReplicateEmptyDirectoriesOption As System.Windows.Forms.CheckBox
    Friend WithEvents Settings_ComputeHashOption As System.Windows.Forms.CheckBox
    Friend WithEvents Settings_PropagateUpdatesOption As System.Windows.Forms.CheckBox
    Friend WithEvents Settings_Tips As System.Windows.Forms.Label
    Friend WithEvents Settings_AdvancedBox As System.Windows.Forms.GroupBox
    Friend WithEvents Settings_BottomDescLabel As System.Windows.Forms.Label
    Friend WithEvents Settings_StrictMirrorOption As System.Windows.Forms.CheckBox
    Friend WithEvents Settings_TimeOffset As System.Windows.Forms.NumericUpDown
    Friend WithEvents Settings_TimeOffsetHoursLabel As System.Windows.Forms.Label
    Friend WithEvents Settings_TimeOffsetLabel As System.Windows.Forms.Label
    Friend WithEvents Settings_StrictDateComparisonOption As System.Windows.Forms.CheckBox
    Friend WithEvents Settings_SwapButton As System.Windows.Forms.Button
    Friend WithEvents Settings_SynchronizeFolderAndSubfoldersMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Settings_SynchronizeFilesOnlyMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Settings_SynchronizeSubFoldersOnlyMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Settings_Loading As System.Windows.Forms.Label
End Class
