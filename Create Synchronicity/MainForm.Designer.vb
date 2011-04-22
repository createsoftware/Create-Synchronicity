'This file is part of Create Synchronicity.
'
'Create Synchronicity is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
'Create Synchronicity is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with Create Synchronicity.  If not, see <http://www.gnu.org/licenses/>.
'Created by:	Clément Pit--Claudel.
'Web site:		http://synchronicity.sourceforge.net.

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
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
        Dim ListViewGroup1 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("\ACTIONS", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup2 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("\PROFILES", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New System.Windows.Forms.ListViewItem.ListViewSubItem() {New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, "\NEW_PROFILE_LABEL"), New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, "\NEW_PROFILE", System.Drawing.Color.DarkGray, System.Drawing.SystemColors.Window, New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)))}, 3)
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.Actions = New System.Windows.Forms.ListView()
        Me.Actions_NameColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.MethodsColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SyncIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.InfoPanel = New System.Windows.Forms.Panel()
        Me.InfoLayout = New System.Windows.Forms.TableLayoutPanel()
        Me.TimeOffset = New System.Windows.Forms.Label()
        Me.Scheduling = New System.Windows.Forms.Label()
        Me.SchedulingLabel = New System.Windows.Forms.Label()
        Me.Destination = New System.Windows.Forms.Label()
        Me.DestinationLabel = New System.Windows.Forms.Label()
        Me.Source = New System.Windows.Forms.Label()
        Me.SourceLabel = New System.Windows.Forms.Label()
        Me.FileTypes = New System.Windows.Forms.Label()
        Me.FileTypesLabel = New System.Windows.Forms.Label()
        Me.Method = New System.Windows.Forms.Label()
        Me.MethodLabel = New System.Windows.Forms.Label()
        Me.LimitedCopy = New System.Windows.Forms.Label()
        Me.LimitedCopyLabel = New System.Windows.Forms.Label()
        Me.ProfileName = New System.Windows.Forms.Label()
        Me.NameLabel = New System.Windows.Forms.Label()
        Me.TimeOffsetLabel = New System.Windows.Forms.Label()
        Me.ActionsMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.PreviewMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SynchronizeMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ChangeSettingsMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ActionsMenuToolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RenameMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewLogMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ClearLogMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ActionsMenuToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ScheduleMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutLinkLabel = New System.Windows.Forms.LinkLabel()
        Me.ApplicationTimer = New System.Windows.Forms.Timer(Me.components)
        Me.StatusIconMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Donate = New System.Windows.Forms.PictureBox()
        Me.ToolStripHeader = New System.Windows.Forms.ToolStripMenuItem()
        Me.HeaderSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.InfoPanel.SuspendLayout()
        Me.InfoLayout.SuspendLayout()
        Me.ActionsMenu.SuspendLayout()
        Me.StatusIconMenu.SuspendLayout()
        CType(Me.Donate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Actions
        '
        Me.Actions.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Actions_NameColumn, Me.MethodsColumn})
        Me.Actions.Dock = System.Windows.Forms.DockStyle.Fill
        ListViewGroup1.Header = "\ACTIONS"
        ListViewGroup1.Name = "Actions"
        ListViewGroup2.Header = "\PROFILES"
        ListViewGroup2.Name = "Profiles"
        Me.Actions.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup1, ListViewGroup2})
        ListViewItem1.Group = ListViewGroup1
        ListViewItem1.StateImageIndex = 0
        Me.Actions.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1})
        Me.Actions.LargeImageList = Me.SyncIcons
        Me.Actions.Location = New System.Drawing.Point(0, 0)
        Me.Actions.MultiSelect = False
        Me.Actions.Name = "Actions"
        Me.Actions.Size = New System.Drawing.Size(355, 262)
        Me.Actions.TabIndex = 0
        Me.Actions.TileSize = New System.Drawing.Size(160, 40)
        Me.Actions.UseCompatibleStateImageBehavior = False
        Me.Actions.View = System.Windows.Forms.View.Tile
        '
        'Actions_NameColumn
        '
        Me.Actions_NameColumn.Text = "\NAME"
        '
        'MethodsColumn
        '
        Me.MethodsColumn.Text = "\METHOD"
        '
        'SyncIcons
        '
        Me.SyncIcons.ImageStream = CType(resources.GetObject("SyncIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.SyncIcons.TransparentColor = System.Drawing.Color.Empty
        Me.SyncIcons.Images.SetKeyName(0, "edit-redo.png")
        Me.SyncIcons.Images.SetKeyName(1, "edit-redo-add.png")
        Me.SyncIcons.Images.SetKeyName(2, "view-refresh.png")
        Me.SyncIcons.Images.SetKeyName(3, "document-new.png")
        '
        'InfoPanel
        '
        Me.InfoPanel.Controls.Add(Me.InfoLayout)
        Me.InfoPanel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.InfoPanel.Location = New System.Drawing.Point(0, 262)
        Me.InfoPanel.Name = "InfoPanel"
        Me.InfoPanel.Size = New System.Drawing.Size(355, 160)
        Me.InfoPanel.TabIndex = 2
        '
        'InfoLayout
        '
        Me.InfoLayout.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.InfoLayout.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.InfoLayout.ColumnCount = 4
        Me.InfoLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.InfoLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.InfoLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.InfoLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.InfoLayout.Controls.Add(Me.TimeOffset, 3, 2)
        Me.InfoLayout.Controls.Add(Me.Scheduling, 1, 2)
        Me.InfoLayout.Controls.Add(Me.SchedulingLabel, 0, 2)
        Me.InfoLayout.Controls.Add(Me.Destination, 1, 4)
        Me.InfoLayout.Controls.Add(Me.DestinationLabel, 0, 4)
        Me.InfoLayout.Controls.Add(Me.Source, 1, 3)
        Me.InfoLayout.Controls.Add(Me.SourceLabel, 0, 3)
        Me.InfoLayout.Controls.Add(Me.FileTypes, 3, 1)
        Me.InfoLayout.Controls.Add(Me.FileTypesLabel, 2, 1)
        Me.InfoLayout.Controls.Add(Me.Method, 1, 1)
        Me.InfoLayout.Controls.Add(Me.MethodLabel, 0, 1)
        Me.InfoLayout.Controls.Add(Me.LimitedCopy, 3, 0)
        Me.InfoLayout.Controls.Add(Me.LimitedCopyLabel, 2, 0)
        Me.InfoLayout.Controls.Add(Me.ProfileName, 1, 0)
        Me.InfoLayout.Controls.Add(Me.NameLabel, 0, 0)
        Me.InfoLayout.Controls.Add(Me.TimeOffsetLabel, 2, 2)
        Me.InfoLayout.Location = New System.Drawing.Point(12, 6)
        Me.InfoLayout.Name = "InfoLayout"
        Me.InfoLayout.RowCount = 5
        Me.InfoLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.InfoLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.InfoLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.InfoLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.InfoLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.InfoLayout.Size = New System.Drawing.Size(331, 142)
        Me.InfoLayout.TabIndex = 0
        '
        'TimeOffset
        '
        Me.TimeOffset.AutoSize = True
        Me.TimeOffset.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TimeOffset.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.TimeOffset.Location = New System.Drawing.Point(296, 57)
        Me.TimeOffset.Name = "TimeOffset"
        Me.TimeOffset.Size = New System.Drawing.Size(31, 27)
        Me.TimeOffset.TabIndex = 15
        Me.TimeOffset.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Scheduling
        '
        Me.Scheduling.AutoSize = True
        Me.Scheduling.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Scheduling.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.Scheduling.Location = New System.Drawing.Point(104, 57)
        Me.Scheduling.Name = "Scheduling"
        Me.Scheduling.Size = New System.Drawing.Size(79, 27)
        Me.Scheduling.TabIndex = 14
        Me.Scheduling.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'SchedulingLabel
        '
        Me.SchedulingLabel.AutoSize = True
        Me.SchedulingLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SchedulingLabel.Location = New System.Drawing.Point(4, 57)
        Me.SchedulingLabel.Name = "SchedulingLabel"
        Me.SchedulingLabel.Size = New System.Drawing.Size(93, 27)
        Me.SchedulingLabel.TabIndex = 13
        Me.SchedulingLabel.Text = "\SCH_LABEL"
        Me.SchedulingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Destination
        '
        Me.Destination.AutoSize = True
        Me.InfoLayout.SetColumnSpan(Me.Destination, 3)
        Me.Destination.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Destination.Location = New System.Drawing.Point(104, 113)
        Me.Destination.Name = "Destination"
        Me.Destination.Size = New System.Drawing.Size(223, 28)
        Me.Destination.TabIndex = 11
        Me.Destination.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Destination.UseMnemonic = False
        '
        'DestinationLabel
        '
        Me.DestinationLabel.AutoSize = True
        Me.DestinationLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DestinationLabel.Location = New System.Drawing.Point(4, 113)
        Me.DestinationLabel.Name = "DestinationLabel"
        Me.DestinationLabel.Size = New System.Drawing.Size(93, 28)
        Me.DestinationLabel.TabIndex = 10
        Me.DestinationLabel.Text = "\DESTINATION"
        Me.DestinationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Source
        '
        Me.Source.AutoSize = True
        Me.InfoLayout.SetColumnSpan(Me.Source, 3)
        Me.Source.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Source.Location = New System.Drawing.Point(104, 85)
        Me.Source.Name = "Source"
        Me.Source.Size = New System.Drawing.Size(223, 27)
        Me.Source.TabIndex = 9
        Me.Source.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Source.UseMnemonic = False
        '
        'SourceLabel
        '
        Me.SourceLabel.AutoSize = True
        Me.SourceLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SourceLabel.Location = New System.Drawing.Point(4, 85)
        Me.SourceLabel.Name = "SourceLabel"
        Me.SourceLabel.Size = New System.Drawing.Size(93, 27)
        Me.SourceLabel.TabIndex = 8
        Me.SourceLabel.Text = "\SOURCE"
        Me.SourceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'FileTypes
        '
        Me.FileTypes.AutoSize = True
        Me.FileTypes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FileTypes.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.FileTypes.Location = New System.Drawing.Point(296, 29)
        Me.FileTypes.Name = "FileTypes"
        Me.FileTypes.Size = New System.Drawing.Size(31, 27)
        Me.FileTypes.TabIndex = 7
        Me.FileTypes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'FileTypesLabel
        '
        Me.FileTypesLabel.AutoSize = True
        Me.FileTypesLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FileTypesLabel.Location = New System.Drawing.Point(190, 29)
        Me.FileTypesLabel.Name = "FileTypesLabel"
        Me.FileTypesLabel.Size = New System.Drawing.Size(99, 27)
        Me.FileTypesLabel.TabIndex = 6
        Me.FileTypesLabel.Text = "\FILETYPES"
        Me.FileTypesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Method
        '
        Me.Method.AutoSize = True
        Me.Method.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Method.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.Method.Location = New System.Drawing.Point(104, 29)
        Me.Method.Name = "Method"
        Me.Method.Size = New System.Drawing.Size(79, 27)
        Me.Method.TabIndex = 5
        Me.Method.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MethodLabel
        '
        Me.MethodLabel.AutoSize = True
        Me.MethodLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MethodLabel.Location = New System.Drawing.Point(4, 29)
        Me.MethodLabel.Name = "MethodLabel"
        Me.MethodLabel.Size = New System.Drawing.Size(93, 27)
        Me.MethodLabel.TabIndex = 4
        Me.MethodLabel.Text = "\METHOD"
        Me.MethodLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LimitedCopy
        '
        Me.LimitedCopy.AutoSize = True
        Me.LimitedCopy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LimitedCopy.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.LimitedCopy.Location = New System.Drawing.Point(296, 1)
        Me.LimitedCopy.Name = "LimitedCopy"
        Me.LimitedCopy.Size = New System.Drawing.Size(31, 27)
        Me.LimitedCopy.TabIndex = 3
        Me.LimitedCopy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LimitedCopyLabel
        '
        Me.LimitedCopyLabel.AutoSize = True
        Me.LimitedCopyLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LimitedCopyLabel.Location = New System.Drawing.Point(190, 1)
        Me.LimitedCopyLabel.Name = "LimitedCopyLabel"
        Me.LimitedCopyLabel.Size = New System.Drawing.Size(99, 27)
        Me.LimitedCopyLabel.TabIndex = 2
        Me.LimitedCopyLabel.Text = "\LIMITED_COPY"
        Me.LimitedCopyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ProfileName
        '
        Me.ProfileName.AutoSize = True
        Me.ProfileName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ProfileName.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.ProfileName.Location = New System.Drawing.Point(104, 1)
        Me.ProfileName.Name = "ProfileName"
        Me.ProfileName.Size = New System.Drawing.Size(79, 27)
        Me.ProfileName.TabIndex = 1
        Me.ProfileName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'NameLabel
        '
        Me.NameLabel.AutoSize = True
        Me.NameLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.NameLabel.Location = New System.Drawing.Point(4, 1)
        Me.NameLabel.Name = "NameLabel"
        Me.NameLabel.Size = New System.Drawing.Size(93, 27)
        Me.NameLabel.TabIndex = 0
        Me.NameLabel.Text = "\NAME"
        Me.NameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TimeOffsetLabel
        '
        Me.TimeOffsetLabel.AutoSize = True
        Me.TimeOffsetLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TimeOffsetLabel.Location = New System.Drawing.Point(190, 57)
        Me.TimeOffsetLabel.Name = "TimeOffsetLabel"
        Me.TimeOffsetLabel.Size = New System.Drawing.Size(99, 27)
        Me.TimeOffsetLabel.TabIndex = 12
        Me.TimeOffsetLabel.Text = "\TIME_OFFSET"
        Me.TimeOffsetLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ActionsMenu
        '
        Me.ActionsMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PreviewMenuItem, Me.SynchronizeMenuItem, Me.ChangeSettingsMenuItem, Me.ActionsMenuToolStripSeparator, Me.DeleteToolStripMenuItem, Me.RenameMenuItem, Me.ViewLogMenuItem, Me.ClearLogMenuItem, Me.ActionsMenuToolStripSeparator2, Me.ScheduleMenuItem})
        Me.ActionsMenu.Name = "ActionsMenu"
        Me.ActionsMenu.Size = New System.Drawing.Size(185, 192)
        '
        'PreviewMenuItem
        '
        Me.PreviewMenuItem.Image = CType(resources.GetObject("PreviewMenuItem.Image"), System.Drawing.Image)
        Me.PreviewMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.PreviewMenuItem.Name = "PreviewMenuItem"
        Me.PreviewMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.PreviewMenuItem.Text = "\PREVIEW"
        '
        'SynchronizeMenuItem
        '
        Me.SynchronizeMenuItem.Image = CType(resources.GetObject("SynchronizeMenuItem.Image"), System.Drawing.Image)
        Me.SynchronizeMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.SynchronizeMenuItem.Name = "SynchronizeMenuItem"
        Me.SynchronizeMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.SynchronizeMenuItem.Text = "\SYNC"
        '
        'ChangeSettingsMenuItem
        '
        Me.ChangeSettingsMenuItem.Image = CType(resources.GetObject("ChangeSettingsMenuItem.Image"), System.Drawing.Image)
        Me.ChangeSettingsMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ChangeSettingsMenuItem.Name = "ChangeSettingsMenuItem"
        Me.ChangeSettingsMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.ChangeSettingsMenuItem.Text = "\CHANGE_SETTINGS"
        '
        'ActionsMenuToolStripSeparator
        '
        Me.ActionsMenuToolStripSeparator.Name = "ActionsMenuToolStripSeparator"
        Me.ActionsMenuToolStripSeparator.Size = New System.Drawing.Size(181, 6)
        '
        'DeleteToolStripMenuItem
        '
        Me.DeleteToolStripMenuItem.Image = CType(resources.GetObject("DeleteToolStripMenuItem.Image"), System.Drawing.Image)
        Me.DeleteToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        Me.DeleteToolStripMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.DeleteToolStripMenuItem.Text = "\DELETE"
        '
        'RenameMenuItem
        '
        Me.RenameMenuItem.Image = CType(resources.GetObject("RenameMenuItem.Image"), System.Drawing.Image)
        Me.RenameMenuItem.Name = "RenameMenuItem"
        Me.RenameMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.RenameMenuItem.Text = "\RENAME"
        '
        'ViewLogMenuItem
        '
        Me.ViewLogMenuItem.Image = CType(resources.GetObject("ViewLogMenuItem.Image"), System.Drawing.Image)
        Me.ViewLogMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ViewLogMenuItem.Name = "ViewLogMenuItem"
        Me.ViewLogMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.ViewLogMenuItem.Text = "\VIEW_LOG"
        '
        'ClearLogMenuItem
        '
        Me.ClearLogMenuItem.Image = CType(resources.GetObject("ClearLogMenuItem.Image"), System.Drawing.Image)
        Me.ClearLogMenuItem.Name = "ClearLogMenuItem"
        Me.ClearLogMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.ClearLogMenuItem.Tag = "\CLEAR_LOG"
        '
        'ActionsMenuToolStripSeparator2
        '
        Me.ActionsMenuToolStripSeparator2.Name = "ActionsMenuToolStripSeparator2"
        Me.ActionsMenuToolStripSeparator2.Size = New System.Drawing.Size(181, 6)
        '
        'ScheduleMenuItem
        '
        Me.ScheduleMenuItem.Image = CType(resources.GetObject("ScheduleMenuItem.Image"), System.Drawing.Image)
        Me.ScheduleMenuItem.Name = "ScheduleMenuItem"
        Me.ScheduleMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.ScheduleMenuItem.Text = "\SCHEDULING"
        '
        'AboutLinkLabel
        '
        Me.AboutLinkLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AboutLinkLabel.BackColor = System.Drawing.Color.White
        Me.AboutLinkLabel.Location = New System.Drawing.Point(230, 3)
        Me.AboutLinkLabel.Name = "AboutLinkLabel"
        Me.AboutLinkLabel.Size = New System.Drawing.Size(118, 13)
        Me.AboutLinkLabel.TabIndex = 1
        Me.AboutLinkLabel.TabStop = True
        Me.AboutLinkLabel.Text = "\ABOUT_SETTINGS"
        Me.AboutLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.AboutLinkLabel.VisitedLinkColor = System.Drawing.Color.Blue
        '
        'ApplicationTimer
        '
        Me.ApplicationTimer.Interval = 2000
        '
        'StatusIconMenu
        '
        Me.StatusIconMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripHeader, Me.HeaderSeparator, Me.ExitToolStripMenuItem})
        Me.StatusIconMenu.Name = "StatusIconMenu"
        Me.StatusIconMenu.Size = New System.Drawing.Size(184, 76)
        '
        'ToolStripHeader
        '
        Me.ToolStripHeader.Enabled = False
        Me.ToolStripHeader.Name = "ToolStripHeader"
        Me.ToolStripHeader.Size = New System.Drawing.Size(183, 22)
        Me.ToolStripHeader.Text = "Create Synchronicity"
        '
        'HeaderSeparator
        '
        Me.HeaderSeparator.Name = "HeaderSeparator"
        Me.HeaderSeparator.Size = New System.Drawing.Size(180, 6)
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(183, 22)
        '
        'Donate
        '
        Me.Donate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Donate.BackColor = System.Drawing.Color.White
        Me.Donate.Image = CType(resources.GetObject("Donate.Image"), System.Drawing.Image)
        Me.Donate.Location = New System.Drawing.Point(314, 22)
        Me.Donate.Name = "Donate"
        Me.Donate.Size = New System.Drawing.Size(34, 34)
        Me.Donate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.Donate.TabIndex = 3
        Me.Donate.TabStop = False
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(355, 422)
        Me.Controls.Add(Me.AboutLinkLabel)
        Me.Controls.Add(Me.Donate)
        Me.Controls.Add(Me.Actions)
        Me.Controls.Add(Me.InfoPanel)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.KeyPreview = True
        Me.Name = "MainForm"
        Me.Text = "Create Synchronicity"
        Me.InfoPanel.ResumeLayout(False)
        Me.InfoLayout.ResumeLayout(False)
        Me.InfoLayout.PerformLayout()
        Me.ActionsMenu.ResumeLayout(False)
        Me.StatusIconMenu.ResumeLayout(False)
        CType(Me.Donate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Actions As System.Windows.Forms.ListView
    Friend WithEvents InfoPanel As System.Windows.Forms.Panel
    Friend WithEvents InfoLayout As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ProfileName As System.Windows.Forms.Label
    Friend WithEvents NameLabel As System.Windows.Forms.Label
    Friend WithEvents Destination As System.Windows.Forms.Label
    Friend WithEvents DestinationLabel As System.Windows.Forms.Label
    Friend WithEvents Source As System.Windows.Forms.Label
    Friend WithEvents SourceLabel As System.Windows.Forms.Label
    Friend WithEvents FileTypes As System.Windows.Forms.Label
    Friend WithEvents FileTypesLabel As System.Windows.Forms.Label
    Friend WithEvents Method As System.Windows.Forms.Label
    Friend WithEvents MethodLabel As System.Windows.Forms.Label
    Friend WithEvents LimitedCopy As System.Windows.Forms.Label
    Friend WithEvents LimitedCopyLabel As System.Windows.Forms.Label
    Friend WithEvents ActionsMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents SynchronizeMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PreviewMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ChangeSettingsMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewLogMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SyncIcons As System.Windows.Forms.ImageList
    Friend WithEvents Actions_NameColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents MethodsColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents ActionsMenuToolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents DeleteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutLinkLabel As System.Windows.Forms.LinkLabel
    Friend WithEvents ClearLogMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ActionsMenuToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ScheduleMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ApplicationTimer As System.Windows.Forms.Timer
    Friend WithEvents TimeOffset As System.Windows.Forms.Label
    Friend WithEvents Scheduling As System.Windows.Forms.Label
    Friend WithEvents SchedulingLabel As System.Windows.Forms.Label
    Friend WithEvents TimeOffsetLabel As System.Windows.Forms.Label
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RenameMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Donate As System.Windows.Forms.PictureBox
    Friend WithEvents ToolStripHeader As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HeaderSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents StatusIconMenu As System.Windows.Forms.ContextMenuStrip
End Class
