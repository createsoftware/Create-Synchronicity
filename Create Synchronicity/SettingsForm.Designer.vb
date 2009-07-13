'This file is part of Create Synchronicity.
'
'Create Synchronicity is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
'Create Synchronicity is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with Create Synchronicity.  If not, see <http://www.gnu.org/licenses/>.
'Created by:	Clément Pit--Claudel.
'Web site:		http://synchronicity.sourceforge.net.

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Settings
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Settings))
        Me.Settings_FromTextBox = New System.Windows.Forms.TextBox
        Me.Settings_DirectoriesBox = New System.Windows.Forms.GroupBox
        Me.Settings_BrowseRButton = New System.Windows.Forms.Button
        Me.Settings_BrowseLButton = New System.Windows.Forms.Button
        Me.Settings_ToTextBox = New System.Windows.Forms.TextBox
        Me.Settings_ToLabel = New System.Windows.Forms.Label
        Me.Settings_FromLabel = New System.Windows.Forms.Label
        Me.Settings_ViewsLayoutPanel = New System.Windows.Forms.TableLayoutPanel
        Me.Settings_LeftView = New System.Windows.Forms.TreeView
        Me.Settings_TreeViewMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.Settings_SynchronizeAllSubfoldersMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.Settings_DontSynchronizeSubfoldersMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.Settings_RightView = New System.Windows.Forms.TreeView
        Me.Settings_ReloadButton = New System.Windows.Forms.Button
        Me.Settings_LeftViewLabel = New System.Windows.Forms.Label
        Me.Settings_RightViewLabel = New System.Windows.Forms.Label
        Me.Settings_ViewsBox = New System.Windows.Forms.GroupBox
        Me.Settings_SynchronizationMethodBox = New System.Windows.Forms.GroupBox
        Me.Settings_Tips = New System.Windows.Forms.Label
        Me.Settings_MethodLayoutPanel = New System.Windows.Forms.TableLayoutPanel
        Me.Settings_TwoWaysIncrementalMethodOption = New System.Windows.Forms.RadioButton
        Me.Settings_LRIncrementalMethodOption = New System.Windows.Forms.RadioButton
        Me.Settings_LRMirrorMethodOption = New System.Windows.Forms.RadioButton
        Me.Settings_DescriptionLabel = New System.Windows.Forms.Label
        Me.Settings_IncludeExcludeBox = New System.Windows.Forms.GroupBox
        Me.Settings_CopyAllFilesCheckBox = New System.Windows.Forms.CheckBox
        Me.Settings_IncludeExcludeLayoutPanel = New System.Windows.Forms.TableLayoutPanel
        Me.Settings_IncludedTypesTextBox = New System.Windows.Forms.TextBox
        Me.Settings_ExcludedTypesTextBox = New System.Windows.Forms.TextBox
        Me.Settings_IncludeFilesOption = New System.Windows.Forms.RadioButton
        Me.Settings_ExcludeFilesOption = New System.Windows.Forms.RadioButton
        Me.Settings_ReplicateEmptyDirectoriesOption = New System.Windows.Forms.CheckBox
        Me.Settings_PropagateUpdatesOption = New System.Windows.Forms.CheckBox
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.Settings_CancelButton = New System.Windows.Forms.Button
        Me.Settings_SaveButton = New System.Windows.Forms.Button
        Me.Settings_FolderBrowser = New System.Windows.Forms.FolderBrowserDialog
        Me.Settings_ComputeHashOption = New System.Windows.Forms.CheckBox
        Me.Settings_AdvancedBox = New System.Windows.Forms.GroupBox
        Me.Settings_BottomDescLabel = New System.Windows.Forms.Label
        Me.Settings_DirectoriesBox.SuspendLayout()
        Me.Settings_ViewsLayoutPanel.SuspendLayout()
        Me.Settings_TreeViewMenuStrip.SuspendLayout()
        Me.Settings_ViewsBox.SuspendLayout()
        Me.Settings_SynchronizationMethodBox.SuspendLayout()
        Me.Settings_MethodLayoutPanel.SuspendLayout()
        Me.Settings_IncludeExcludeBox.SuspendLayout()
        Me.Settings_IncludeExcludeLayoutPanel.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Settings_AdvancedBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'Settings_FromTextBox
        '
        Me.Settings_FromTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Settings_FromTextBox.Location = New System.Drawing.Point(62, 19)
        Me.Settings_FromTextBox.Name = "Settings_FromTextBox"
        Me.Settings_FromTextBox.Size = New System.Drawing.Size(571, 21)
        Me.Settings_FromTextBox.TabIndex = 0
        '
        'Settings_DirectoriesBox
        '
        Me.Settings_DirectoriesBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Settings_DirectoriesBox.Controls.Add(Me.Settings_BrowseRButton)
        Me.Settings_DirectoriesBox.Controls.Add(Me.Settings_BrowseLButton)
        Me.Settings_DirectoriesBox.Controls.Add(Me.Settings_ToTextBox)
        Me.Settings_DirectoriesBox.Controls.Add(Me.Settings_FromTextBox)
        Me.Settings_DirectoriesBox.Controls.Add(Me.Settings_ToLabel)
        Me.Settings_DirectoriesBox.Controls.Add(Me.Settings_FromLabel)
        Me.Settings_DirectoriesBox.Location = New System.Drawing.Point(12, 12)
        Me.Settings_DirectoriesBox.Name = "Settings_DirectoriesBox"
        Me.Settings_DirectoriesBox.Size = New System.Drawing.Size(674, 71)
        Me.Settings_DirectoriesBox.TabIndex = 1
        Me.Settings_DirectoriesBox.TabStop = False
        Me.Settings_DirectoriesBox.Text = "Directories"
        '
        'Settings_BrowseRButton
        '
        Me.Settings_BrowseRButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Settings_BrowseRButton.Location = New System.Drawing.Point(639, 44)
        Me.Settings_BrowseRButton.Name = "Settings_BrowseRButton"
        Me.Settings_BrowseRButton.Size = New System.Drawing.Size(29, 24)
        Me.Settings_BrowseRButton.TabIndex = 3
        Me.Settings_BrowseRButton.Text = "..."
        Me.Settings_BrowseRButton.UseVisualStyleBackColor = True
        '
        'Settings_BrowseLButton
        '
        Me.Settings_BrowseLButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Settings_BrowseLButton.Location = New System.Drawing.Point(639, 18)
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
        Me.Settings_ToTextBox.Size = New System.Drawing.Size(571, 21)
        Me.Settings_ToTextBox.TabIndex = 1
        '
        'Settings_ToLabel
        '
        Me.Settings_ToLabel.Location = New System.Drawing.Point(6, 44)
        Me.Settings_ToLabel.Name = "Settings_ToLabel"
        Me.Settings_ToLabel.Size = New System.Drawing.Size(59, 21)
        Me.Settings_ToLabel.TabIndex = 4
        Me.Settings_ToLabel.Text = "To: "
        Me.Settings_ToLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Settings_FromLabel
        '
        Me.Settings_FromLabel.Location = New System.Drawing.Point(3, 18)
        Me.Settings_FromLabel.Name = "Settings_FromLabel"
        Me.Settings_FromLabel.Size = New System.Drawing.Size(62, 21)
        Me.Settings_FromLabel.TabIndex = 2
        Me.Settings_FromLabel.Text = "From: "
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
        Me.Settings_ViewsLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Settings_ViewsLayoutPanel.Location = New System.Drawing.Point(3, 17)
        Me.Settings_ViewsLayoutPanel.Name = "Settings_ViewsLayoutPanel"
        Me.Settings_ViewsLayoutPanel.RowCount = 2
        Me.Settings_ViewsLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.Settings_ViewsLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.Settings_ViewsLayoutPanel.Size = New System.Drawing.Size(668, 139)
        Me.Settings_ViewsLayoutPanel.TabIndex = 3
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
        Me.Settings_LeftView.Size = New System.Drawing.Size(310, 120)
        Me.Settings_LeftView.TabIndex = 2
        '
        'Settings_TreeViewMenuStrip
        '
        Me.Settings_TreeViewMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Settings_SynchronizeAllSubfoldersMenuItem, Me.Settings_DontSynchronizeSubfoldersMenuItem})
        Me.Settings_TreeViewMenuStrip.Name = "Settings_TreeViewMenuStrip"
        Me.Settings_TreeViewMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.Settings_TreeViewMenuStrip.ShowImageMargin = False
        Me.Settings_TreeViewMenuStrip.Size = New System.Drawing.Size(200, 48)
        '
        'Settings_SynchronizeAllSubfoldersMenuItem
        '
        Me.Settings_SynchronizeAllSubfoldersMenuItem.Name = "Settings_SynchronizeAllSubfoldersMenuItem"
        Me.Settings_SynchronizeAllSubfoldersMenuItem.Size = New System.Drawing.Size(199, 22)
        Me.Settings_SynchronizeAllSubfoldersMenuItem.Text = "Synchronize all subfolders"
        '
        'Settings_DontSynchronizeSubfoldersMenuItem
        '
        Me.Settings_DontSynchronizeSubfoldersMenuItem.Name = "Settings_DontSynchronizeSubfoldersMenuItem"
        Me.Settings_DontSynchronizeSubfoldersMenuItem.Size = New System.Drawing.Size(199, 22)
        Me.Settings_DontSynchronizeSubfoldersMenuItem.Text = "Don't Synchronize subfolders"
        '
        'Settings_RightView
        '
        Me.Settings_RightView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Settings_RightView.ContextMenuStrip = Me.Settings_TreeViewMenuStrip
        Me.Settings_RightView.Location = New System.Drawing.Point(354, 16)
        Me.Settings_RightView.Name = "Settings_RightView"
        Me.Settings_RightView.Size = New System.Drawing.Size(311, 120)
        Me.Settings_RightView.TabIndex = 3
        '
        'Settings_ReloadButton
        '
        Me.Settings_ReloadButton.Image = CType(resources.GetObject("Settings_ReloadButton.Image"), System.Drawing.Image)
        Me.Settings_ReloadButton.Location = New System.Drawing.Point(319, 16)
        Me.Settings_ReloadButton.Name = "Settings_ReloadButton"
        Me.Settings_ReloadButton.Size = New System.Drawing.Size(29, 29)
        Me.Settings_ReloadButton.TabIndex = 4
        Me.Settings_ReloadButton.UseVisualStyleBackColor = True
        '
        'Settings_LeftViewLabel
        '
        Me.Settings_LeftViewLabel.AutoSize = True
        Me.Settings_LeftViewLabel.Location = New System.Drawing.Point(3, 0)
        Me.Settings_LeftViewLabel.Name = "Settings_LeftViewLabel"
        Me.Settings_LeftViewLabel.Size = New System.Drawing.Size(60, 13)
        Me.Settings_LeftViewLabel.TabIndex = 5
        Me.Settings_LeftViewLabel.Text = "Left side:"
        '
        'Settings_RightViewLabel
        '
        Me.Settings_RightViewLabel.AutoSize = True
        Me.Settings_RightViewLabel.Location = New System.Drawing.Point(354, 0)
        Me.Settings_RightViewLabel.Name = "Settings_RightViewLabel"
        Me.Settings_RightViewLabel.Size = New System.Drawing.Size(68, 13)
        Me.Settings_RightViewLabel.TabIndex = 6
        Me.Settings_RightViewLabel.Text = "Right side:"
        '
        'Settings_ViewsBox
        '
        Me.Settings_ViewsBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Settings_ViewsBox.Controls.Add(Me.Settings_ViewsLayoutPanel)
        Me.Settings_ViewsBox.Location = New System.Drawing.Point(12, 89)
        Me.Settings_ViewsBox.Name = "Settings_ViewsBox"
        Me.Settings_ViewsBox.Size = New System.Drawing.Size(674, 159)
        Me.Settings_ViewsBox.TabIndex = 4
        Me.Settings_ViewsBox.TabStop = False
        Me.Settings_ViewsBox.Text = "Subdirectories"
        '
        'Settings_SynchronizationMethodBox
        '
        Me.Settings_SynchronizationMethodBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Settings_SynchronizationMethodBox.Controls.Add(Me.Settings_Tips)
        Me.Settings_SynchronizationMethodBox.Controls.Add(Me.Settings_MethodLayoutPanel)
        Me.Settings_SynchronizationMethodBox.Controls.Add(Me.Settings_DescriptionLabel)
        Me.Settings_SynchronizationMethodBox.Location = New System.Drawing.Point(12, 254)
        Me.Settings_SynchronizationMethodBox.Name = "Settings_SynchronizationMethodBox"
        Me.Settings_SynchronizationMethodBox.Size = New System.Drawing.Size(674, 97)
        Me.Settings_SynchronizationMethodBox.TabIndex = 5
        Me.Settings_SynchronizationMethodBox.TabStop = False
        Me.Settings_SynchronizationMethodBox.Text = "Synchronization method"
        '
        'Settings_Tips
        '
        Me.Settings_Tips.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Settings_Tips.Location = New System.Drawing.Point(3, 17)
        Me.Settings_Tips.Name = "Settings_Tips"
        Me.Settings_Tips.Size = New System.Drawing.Size(668, 77)
        Me.Settings_Tips.TabIndex = 8
        Me.Settings_Tips.Text = resources.GetString("Settings_Tips.Text")
        Me.Settings_Tips.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Settings_Tips.Visible = False
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
        Me.Settings_MethodLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.Settings_MethodLayoutPanel.Size = New System.Drawing.Size(668, 23)
        Me.Settings_MethodLayoutPanel.TabIndex = 6
        '
        'Settings_TwoWaysIncrementalMethodOption
        '
        Me.Settings_TwoWaysIncrementalMethodOption.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Settings_TwoWaysIncrementalMethodOption.AutoSize = True
        Me.Settings_TwoWaysIncrementalMethodOption.Location = New System.Drawing.Point(479, 3)
        Me.Settings_TwoWaysIncrementalMethodOption.Name = "Settings_TwoWaysIncrementalMethodOption"
        Me.Settings_TwoWaysIncrementalMethodOption.Size = New System.Drawing.Size(153, 17)
        Me.Settings_TwoWaysIncrementalMethodOption.TabIndex = 2
        Me.Settings_TwoWaysIncrementalMethodOption.Tag = "%s copies all files both ways, without deleting any.\nNew and modified files are " & _
            "copied both ways, nothing is deleted."
        Me.Settings_TwoWaysIncrementalMethodOption.Text = "Two-ways incremental"
        Me.Settings_TwoWaysIncrementalMethodOption.UseVisualStyleBackColor = True
        '
        'Settings_LRIncrementalMethodOption
        '
        Me.Settings_LRIncrementalMethodOption.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Settings_LRIncrementalMethodOption.AutoSize = True
        Me.Settings_LRIncrementalMethodOption.Location = New System.Drawing.Point(244, 3)
        Me.Settings_LRIncrementalMethodOption.Name = "Settings_LRIncrementalMethodOption"
        Me.Settings_LRIncrementalMethodOption.Size = New System.Drawing.Size(177, 17)
        Me.Settings_LRIncrementalMethodOption.TabIndex = 1
        Me.Settings_LRIncrementalMethodOption.Tag = "%s saves source files to destination.\nNew and modified files are copied left to " & _
            "right, but no files are deleted."
        Me.Settings_LRIncrementalMethodOption.Text = "Left to Right (Incremental)"
        Me.Settings_LRIncrementalMethodOption.UseVisualStyleBackColor = True
        '
        'Settings_LRMirrorMethodOption
        '
        Me.Settings_LRMirrorMethodOption.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Settings_LRMirrorMethodOption.AutoSize = True
        Me.Settings_LRMirrorMethodOption.Checked = True
        Me.Settings_LRMirrorMethodOption.Location = New System.Drawing.Point(40, 3)
        Me.Settings_LRMirrorMethodOption.Name = "Settings_LRMirrorMethodOption"
        Me.Settings_LRMirrorMethodOption.Size = New System.Drawing.Size(142, 17)
        Me.Settings_LRMirrorMethodOption.TabIndex = 0
        Me.Settings_LRMirrorMethodOption.TabStop = True
        Me.Settings_LRMirrorMethodOption.Tag = "%s creates a clone of the left side on the right and copies all files from source" & _
            " to destination.\nNew and modified files are copied left to right, deletes on th" & _
            "e left are repeated on the right."
        Me.Settings_LRMirrorMethodOption.Text = "Left to Right (Mirror)"
        Me.Settings_LRMirrorMethodOption.UseVisualStyleBackColor = True
        '
        'Settings_DescriptionLabel
        '
        Me.Settings_DescriptionLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Settings_DescriptionLabel.Location = New System.Drawing.Point(6, 43)
        Me.Settings_DescriptionLabel.Name = "Settings_DescriptionLabel"
        Me.Settings_DescriptionLabel.Size = New System.Drawing.Size(662, 51)
        Me.Settings_DescriptionLabel.TabIndex = 1
        Me.Settings_DescriptionLabel.Tag = "Move your mouse over an option to see a more detailed description"
        Me.Settings_DescriptionLabel.Text = "Move your mouse over an option to see a more detailed description"
        '
        'Settings_IncludeExcludeBox
        '
        Me.Settings_IncludeExcludeBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Settings_IncludeExcludeBox.Controls.Add(Me.Settings_CopyAllFilesCheckBox)
        Me.Settings_IncludeExcludeBox.Controls.Add(Me.Settings_IncludeExcludeLayoutPanel)
        Me.Settings_IncludeExcludeBox.Controls.Add(Me.Settings_ReplicateEmptyDirectoriesOption)
        Me.Settings_IncludeExcludeBox.Location = New System.Drawing.Point(12, 357)
        Me.Settings_IncludeExcludeBox.Name = "Settings_IncludeExcludeBox"
        Me.Settings_IncludeExcludeBox.Size = New System.Drawing.Size(674, 97)
        Me.Settings_IncludeExcludeBox.TabIndex = 6
        Me.Settings_IncludeExcludeBox.TabStop = False
        Me.Settings_IncludeExcludeBox.Text = "Include or exclude file types"
        '
        'Settings_CopyAllFilesCheckBox
        '
        Me.Settings_CopyAllFilesCheckBox.AutoSize = True
        Me.Settings_CopyAllFilesCheckBox.Checked = True
        Me.Settings_CopyAllFilesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Settings_CopyAllFilesCheckBox.Location = New System.Drawing.Point(6, 20)
        Me.Settings_CopyAllFilesCheckBox.Name = "Settings_CopyAllFilesCheckBox"
        Me.Settings_CopyAllFilesCheckBox.Size = New System.Drawing.Size(100, 17)
        Me.Settings_CopyAllFilesCheckBox.TabIndex = 5
        Me.Settings_CopyAllFilesCheckBox.Text = "Copy all files"
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
        Me.Settings_IncludeExcludeLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.Settings_IncludeExcludeLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.Settings_IncludeExcludeLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.Settings_IncludeExcludeLayoutPanel.Size = New System.Drawing.Size(668, 51)
        Me.Settings_IncludeExcludeLayoutPanel.TabIndex = 4
        '
        'Settings_IncludedTypesTextBox
        '
        Me.Settings_IncludedTypesTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Settings_IncludedTypesTextBox.Location = New System.Drawing.Point(3, 26)
        Me.Settings_IncludedTypesTextBox.Name = "Settings_IncludedTypesTextBox"
        Me.Settings_IncludedTypesTextBox.Size = New System.Drawing.Size(328, 21)
        Me.Settings_IncludedTypesTextBox.TabIndex = 8
        '
        'Settings_ExcludedTypesTextBox
        '
        Me.Settings_ExcludedTypesTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Settings_ExcludedTypesTextBox.Location = New System.Drawing.Point(337, 26)
        Me.Settings_ExcludedTypesTextBox.Name = "Settings_ExcludedTypesTextBox"
        Me.Settings_ExcludedTypesTextBox.Size = New System.Drawing.Size(328, 21)
        Me.Settings_ExcludedTypesTextBox.TabIndex = 7
        '
        'Settings_IncludeFilesOption
        '
        Me.Settings_IncludeFilesOption.AutoSize = True
        Me.Settings_IncludeFilesOption.Location = New System.Drawing.Point(3, 3)
        Me.Settings_IncludeFilesOption.Name = "Settings_IncludeFilesOption"
        Me.Settings_IncludeFilesOption.Size = New System.Drawing.Size(162, 17)
        Me.Settings_IncludeFilesOption.TabIndex = 9
        Me.Settings_IncludeFilesOption.TabStop = True
        Me.Settings_IncludeFilesOption.Text = "Include these files only:"
        Me.Settings_IncludeFilesOption.UseVisualStyleBackColor = True
        '
        'Settings_ExcludeFilesOption
        '
        Me.Settings_ExcludeFilesOption.AutoSize = True
        Me.Settings_ExcludeFilesOption.Location = New System.Drawing.Point(337, 3)
        Me.Settings_ExcludeFilesOption.Name = "Settings_ExcludeFilesOption"
        Me.Settings_ExcludeFilesOption.Size = New System.Drawing.Size(136, 17)
        Me.Settings_ExcludeFilesOption.TabIndex = 10
        Me.Settings_ExcludeFilesOption.TabStop = True
        Me.Settings_ExcludeFilesOption.Text = "Exclude these files:"
        Me.Settings_ExcludeFilesOption.UseVisualStyleBackColor = True
        '
        'Settings_ReplicateEmptyDirectoriesOption
        '
        Me.Settings_ReplicateEmptyDirectoriesOption.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Settings_ReplicateEmptyDirectoriesOption.AutoSize = True
        Me.Settings_ReplicateEmptyDirectoriesOption.Checked = True
        Me.Settings_ReplicateEmptyDirectoriesOption.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Settings_ReplicateEmptyDirectoriesOption.Location = New System.Drawing.Point(489, 20)
        Me.Settings_ReplicateEmptyDirectoriesOption.Name = "Settings_ReplicateEmptyDirectoriesOption"
        Me.Settings_ReplicateEmptyDirectoriesOption.Size = New System.Drawing.Size(182, 17)
        Me.Settings_ReplicateEmptyDirectoriesOption.TabIndex = 8
        Me.Settings_ReplicateEmptyDirectoriesOption.Text = "Replicate empty directories"
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
        Me.Settings_PropagateUpdatesOption.Size = New System.Drawing.Size(133, 17)
        Me.Settings_PropagateUpdatesOption.TabIndex = 9
        Me.Settings_PropagateUpdatesOption.Tag = "Checked, updated files are copied. Unchecked, only new and deleted files are sync" & _
            "hronized."
        Me.Settings_PropagateUpdatesOption.Text = "Propagate updates"
        Me.Settings_PropagateUpdatesOption.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Settings_CancelButton, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Settings_SaveButton, 0, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(486, 511)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(200, 31)
        Me.TableLayoutPanel1.TabIndex = 7
        '
        'Settings_CancelButton
        '
        Me.Settings_CancelButton.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Settings_CancelButton.Location = New System.Drawing.Point(103, 3)
        Me.Settings_CancelButton.Name = "Settings_CancelButton"
        Me.Settings_CancelButton.Size = New System.Drawing.Size(94, 25)
        Me.Settings_CancelButton.TabIndex = 1
        Me.Settings_CancelButton.Text = "Cancel"
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
        Me.Settings_SaveButton.Text = "Save"
        Me.Settings_SaveButton.UseVisualStyleBackColor = True
        '
        'Settings_FolderBrowser
        '
        Me.Settings_FolderBrowser.Description = "Select a folder to copy the files %."
        Me.Settings_FolderBrowser.RootFolder = System.Environment.SpecialFolder.MyComputer
        Me.Settings_FolderBrowser.Tag = "Select a folder to copy the files %."
        '
        'Settings_ComputeHashOption
        '
        Me.Settings_ComputeHashOption.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Settings_ComputeHashOption.AutoSize = True
        Me.Settings_ComputeHashOption.Location = New System.Drawing.Point(336, 20)
        Me.Settings_ComputeHashOption.Name = "Settings_ComputeHashOption"
        Me.Settings_ComputeHashOption.Size = New System.Drawing.Size(332, 17)
        Me.Settings_ComputeHashOption.TabIndex = 8
        Me.Settings_ComputeHashOption.Text = "Compute hash of modified files before copying (slow)"
        Me.Settings_ComputeHashOption.UseVisualStyleBackColor = True
        '
        'Settings_AdvancedBox
        '
        Me.Settings_AdvancedBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Settings_AdvancedBox.Controls.Add(Me.Settings_PropagateUpdatesOption)
        Me.Settings_AdvancedBox.Controls.Add(Me.Settings_ComputeHashOption)
        Me.Settings_AdvancedBox.Location = New System.Drawing.Point(12, 460)
        Me.Settings_AdvancedBox.Name = "Settings_AdvancedBox"
        Me.Settings_AdvancedBox.Size = New System.Drawing.Size(674, 45)
        Me.Settings_AdvancedBox.TabIndex = 9
        Me.Settings_AdvancedBox.TabStop = False
        Me.Settings_AdvancedBox.Text = "Advanced options"
        '
        'Settings_BottomDescLabel
        '
        Me.Settings_BottomDescLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Settings_BottomDescLabel.Location = New System.Drawing.Point(12, 511)
        Me.Settings_BottomDescLabel.Name = "Settings_BottomDescLabel"
        Me.Settings_BottomDescLabel.Size = New System.Drawing.Size(471, 31)
        Me.Settings_BottomDescLabel.TabIndex = 10
        '
        'Settings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(698, 554)
        Me.Controls.Add(Me.Settings_BottomDescLabel)
        Me.Controls.Add(Me.Settings_AdvancedBox)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.Settings_ViewsBox)
        Me.Controls.Add(Me.Settings_DirectoriesBox)
        Me.Controls.Add(Me.Settings_IncludeExcludeBox)
        Me.Controls.Add(Me.Settings_SynchronizationMethodBox)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "Settings"
        Me.ShowInTaskbar = False
        Me.Text = "Settings"
        Me.Settings_DirectoriesBox.ResumeLayout(False)
        Me.Settings_DirectoriesBox.PerformLayout()
        Me.Settings_ViewsLayoutPanel.ResumeLayout(False)
        Me.Settings_ViewsLayoutPanel.PerformLayout()
        Me.Settings_TreeViewMenuStrip.ResumeLayout(False)
        Me.Settings_ViewsBox.ResumeLayout(False)
        Me.Settings_SynchronizationMethodBox.ResumeLayout(False)
        Me.Settings_MethodLayoutPanel.ResumeLayout(False)
        Me.Settings_MethodLayoutPanel.PerformLayout()
        Me.Settings_IncludeExcludeBox.ResumeLayout(False)
        Me.Settings_IncludeExcludeBox.PerformLayout()
        Me.Settings_IncludeExcludeLayoutPanel.ResumeLayout(False)
        Me.Settings_IncludeExcludeLayoutPanel.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Settings_AdvancedBox.ResumeLayout(False)
        Me.Settings_AdvancedBox.PerformLayout()
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
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Settings_CancelButton As System.Windows.Forms.Button
    Friend WithEvents Settings_SaveButton As System.Windows.Forms.Button
    Friend WithEvents Settings_FolderBrowser As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents Settings_TreeViewMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents Settings_SynchronizeAllSubfoldersMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Settings_DontSynchronizeSubfoldersMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Settings_ReplicateEmptyDirectoriesOption As System.Windows.Forms.CheckBox
    Friend WithEvents Settings_ComputeHashOption As System.Windows.Forms.CheckBox
    Friend WithEvents Settings_PropagateUpdatesOption As System.Windows.Forms.CheckBox
    Friend WithEvents Settings_Tips As System.Windows.Forms.Label
    Friend WithEvents Settings_AdvancedBox As System.Windows.Forms.GroupBox
    Friend WithEvents Settings_BottomDescLabel As System.Windows.Forms.Label
End Class
