'This file is part of Create Synchronicity.
'
'Create Synchronicity is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
'Create Synchronicity is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with Create Synchronicity.  If not, see <http://www.gnu.org/licenses/>.
'Created by:	Clément Pit--Claudel.
'Web site:		http://synchronicity.sourceforge.net.

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SynchronizeForm
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
        Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New System.Windows.Forms.ListViewItem.ListViewSubItem() {New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, "Folder"), New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, "Create", System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, New System.Drawing.Font("Verdana", 8.25!)), New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, "Left->Right"), New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, "C:\Documents\Clément\file.xxx", System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, New System.Drawing.Font("Verdana", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte)))}, 3)
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SynchronizeForm))
        Me.MainLayoutPanel = New System.Windows.Forms.TableLayoutPanel
        Me.Step3LayoutPanel = New System.Windows.Forms.TableLayoutPanel
        Me.Step3_ProgressLayout = New System.Windows.Forms.TableLayoutPanel
        Me.Step3StatusLabel = New System.Windows.Forms.Label
        Me.Step3ProgressBar = New System.Windows.Forms.ProgressBar
        Me.Step3Label = New System.Windows.Forms.Label
        Me.Step2LayoutPanel = New System.Windows.Forms.TableLayoutPanel
        Me.Step2ProgressLayout = New System.Windows.Forms.TableLayoutPanel
        Me.Step2StatusLabel = New System.Windows.Forms.Label
        Me.Step2ProgressBar = New System.Windows.Forms.ProgressBar
        Me.Step2Label = New System.Windows.Forms.Label
        Me.Step1LayoutPanel = New System.Windows.Forms.TableLayoutPanel
        Me.Step1ProgressLayout = New System.Windows.Forms.TableLayoutPanel
        Me.Step1StatusLabel = New System.Windows.Forms.Label
        Me.Step1ProgressBar = New System.Windows.Forms.ProgressBar
        Me.Step1Label = New System.Windows.Forms.Label
        Me.ButtonsLayoutPanel = New System.Windows.Forms.TableLayoutPanel
        Me.SyncBtn = New System.Windows.Forms.Button
        Me.StopBtn = New System.Windows.Forms.Button
        Me.StatisticsPanel = New System.Windows.Forms.TableLayoutPanel
        Me.FoldersCreated = New System.Windows.Forms.Label
        Me.FilesCreatedLabel = New System.Windows.Forms.Label
        Me.FilesCreated = New System.Windows.Forms.Label
        Me.FoldersCreatedLabel = New System.Windows.Forms.Label
        Me.TotalCount = New System.Windows.Forms.Label
        Me.TotalCountLabel = New System.Windows.Forms.Label
        Me.Done = New System.Windows.Forms.Label
        Me.DoneLabel = New System.Windows.Forms.Label
        Me.Speed = New System.Windows.Forms.Label
        Me.SpeedLabel = New System.Windows.Forms.Label
        Me.ElapsedTime = New System.Windows.Forms.Label
        Me.ElapsedTimeLabel = New System.Windows.Forms.Label
        Me.BlankMargin = New System.Windows.Forms.Label
        Me.SyncingTimeCounter = New System.Windows.Forms.Timer(Me.components)
        Me.PreviewList = New System.Windows.Forms.ListView
        Me.TypeColumn = New System.Windows.Forms.ColumnHeader
        Me.ActionColumn = New System.Windows.Forms.ColumnHeader
        Me.DirectionColumn = New System.Windows.Forms.ColumnHeader
        Me.PathColumn = New System.Windows.Forms.ColumnHeader
        Me.SyncingIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.MainLayoutPanel.SuspendLayout()
        Me.Step3LayoutPanel.SuspendLayout()
        Me.Step3_ProgressLayout.SuspendLayout()
        Me.Step2LayoutPanel.SuspendLayout()
        Me.Step2ProgressLayout.SuspendLayout()
        Me.Step1LayoutPanel.SuspendLayout()
        Me.Step1ProgressLayout.SuspendLayout()
        Me.ButtonsLayoutPanel.SuspendLayout()
        Me.StatisticsPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainLayoutPanel
        '
        Me.MainLayoutPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MainLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.MainLayoutPanel.ColumnCount = 1
        Me.MainLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.MainLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23.0!))
        Me.MainLayoutPanel.Controls.Add(Me.Step3LayoutPanel, 0, 2)
        Me.MainLayoutPanel.Controls.Add(Me.Step2LayoutPanel, 0, 1)
        Me.MainLayoutPanel.Controls.Add(Me.Step1LayoutPanel, 0, 0)
        Me.MainLayoutPanel.Location = New System.Drawing.Point(12, 12)
        Me.MainLayoutPanel.Name = "MainLayoutPanel"
        Me.MainLayoutPanel.RowCount = 3
        Me.MainLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.MainLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.MainLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.MainLayoutPanel.Size = New System.Drawing.Size(474, 263)
        Me.MainLayoutPanel.TabIndex = 0
        '
        'Step3LayoutPanel
        '
        Me.Step3LayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset
        Me.Step3LayoutPanel.ColumnCount = 1
        Me.Step3LayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.Step3LayoutPanel.Controls.Add(Me.Step3_ProgressLayout, 0, 1)
        Me.Step3LayoutPanel.Controls.Add(Me.Step3Label, 0, 0)
        Me.Step3LayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Step3LayoutPanel.Location = New System.Drawing.Point(4, 178)
        Me.Step3LayoutPanel.Name = "Step3LayoutPanel"
        Me.Step3LayoutPanel.RowCount = 2
        Me.Step3LayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.Step3LayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.Step3LayoutPanel.Size = New System.Drawing.Size(466, 81)
        Me.Step3LayoutPanel.TabIndex = 2
        '
        'Step3_ProgressLayout
        '
        Me.Step3_ProgressLayout.ColumnCount = 1
        Me.Step3_ProgressLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.Step3_ProgressLayout.Controls.Add(Me.Step3StatusLabel, 0, 0)
        Me.Step3_ProgressLayout.Controls.Add(Me.Step3ProgressBar, 0, 1)
        Me.Step3_ProgressLayout.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Step3_ProgressLayout.Location = New System.Drawing.Point(5, 30)
        Me.Step3_ProgressLayout.Name = "Step3_ProgressLayout"
        Me.Step3_ProgressLayout.RowCount = 2
        Me.Step3_ProgressLayout.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.Step3_ProgressLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.Step3_ProgressLayout.Size = New System.Drawing.Size(456, 46)
        Me.Step3_ProgressLayout.TabIndex = 0
        '
        'Step3StatusLabel
        '
        Me.Step3StatusLabel.AutoEllipsis = True
        Me.Step3StatusLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Step3StatusLabel.Location = New System.Drawing.Point(3, 0)
        Me.Step3StatusLabel.Name = "Step3StatusLabel"
        Me.Step3StatusLabel.Size = New System.Drawing.Size(450, 13)
        Me.Step3StatusLabel.TabIndex = 2
        Me.Step3StatusLabel.Text = "Waiting..."
        '
        'Step3ProgressBar
        '
        Me.Step3ProgressBar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Step3ProgressBar.Location = New System.Drawing.Point(3, 16)
        Me.Step3ProgressBar.Name = "Step3ProgressBar"
        Me.Step3ProgressBar.Size = New System.Drawing.Size(450, 27)
        Me.Step3ProgressBar.TabIndex = 3
        '
        'Step3Label
        '
        Me.Step3Label.AutoSize = True
        Me.Step3Label.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Step3Label.Location = New System.Drawing.Point(5, 2)
        Me.Step3Label.Name = "Step3Label"
        Me.Step3Label.Padding = New System.Windows.Forms.Padding(0, 5, 0, 5)
        Me.Step3Label.Size = New System.Drawing.Size(456, 23)
        Me.Step3Label.TabIndex = 1
        Me.Step3Label.Text = "Step 3 : Cleaning up and copying files right to left"
        '
        'Step2LayoutPanel
        '
        Me.Step2LayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset
        Me.Step2LayoutPanel.ColumnCount = 1
        Me.Step2LayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.Step2LayoutPanel.Controls.Add(Me.Step2ProgressLayout, 0, 1)
        Me.Step2LayoutPanel.Controls.Add(Me.Step2Label, 0, 0)
        Me.Step2LayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Step2LayoutPanel.Location = New System.Drawing.Point(4, 91)
        Me.Step2LayoutPanel.Name = "Step2LayoutPanel"
        Me.Step2LayoutPanel.RowCount = 2
        Me.Step2LayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.Step2LayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.Step2LayoutPanel.Size = New System.Drawing.Size(466, 80)
        Me.Step2LayoutPanel.TabIndex = 1
        '
        'Step2ProgressLayout
        '
        Me.Step2ProgressLayout.ColumnCount = 1
        Me.Step2ProgressLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.Step2ProgressLayout.Controls.Add(Me.Step2StatusLabel, 0, 0)
        Me.Step2ProgressLayout.Controls.Add(Me.Step2ProgressBar, 0, 1)
        Me.Step2ProgressLayout.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Step2ProgressLayout.Location = New System.Drawing.Point(5, 30)
        Me.Step2ProgressLayout.Name = "Step2ProgressLayout"
        Me.Step2ProgressLayout.RowCount = 2
        Me.Step2ProgressLayout.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.Step2ProgressLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.Step2ProgressLayout.Size = New System.Drawing.Size(456, 45)
        Me.Step2ProgressLayout.TabIndex = 0
        '
        'Step2StatusLabel
        '
        Me.Step2StatusLabel.AutoEllipsis = True
        Me.Step2StatusLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Step2StatusLabel.Location = New System.Drawing.Point(3, 0)
        Me.Step2StatusLabel.Name = "Step2StatusLabel"
        Me.Step2StatusLabel.Size = New System.Drawing.Size(450, 13)
        Me.Step2StatusLabel.TabIndex = 2
        Me.Step2StatusLabel.Text = "Waiting..."
        '
        'Step2ProgressBar
        '
        Me.Step2ProgressBar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Step2ProgressBar.Location = New System.Drawing.Point(3, 16)
        Me.Step2ProgressBar.Name = "Step2ProgressBar"
        Me.Step2ProgressBar.Size = New System.Drawing.Size(450, 26)
        Me.Step2ProgressBar.TabIndex = 3
        '
        'Step2Label
        '
        Me.Step2Label.AutoSize = True
        Me.Step2Label.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Step2Label.Location = New System.Drawing.Point(5, 2)
        Me.Step2Label.Name = "Step2Label"
        Me.Step2Label.Padding = New System.Windows.Forms.Padding(0, 5, 0, 5)
        Me.Step2Label.Size = New System.Drawing.Size(456, 23)
        Me.Step2Label.TabIndex = 1
        Me.Step2Label.Text = "Step 2 : Copying files left to right"
        '
        'Step1LayoutPanel
        '
        Me.Step1LayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset
        Me.Step1LayoutPanel.ColumnCount = 1
        Me.Step1LayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.Step1LayoutPanel.Controls.Add(Me.Step1ProgressLayout, 0, 1)
        Me.Step1LayoutPanel.Controls.Add(Me.Step1Label, 0, 0)
        Me.Step1LayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Step1LayoutPanel.Location = New System.Drawing.Point(4, 4)
        Me.Step1LayoutPanel.Name = "Step1LayoutPanel"
        Me.Step1LayoutPanel.RowCount = 2
        Me.Step1LayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.Step1LayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.Step1LayoutPanel.Size = New System.Drawing.Size(466, 80)
        Me.Step1LayoutPanel.TabIndex = 0
        '
        'Step1ProgressLayout
        '
        Me.Step1ProgressLayout.ColumnCount = 1
        Me.Step1ProgressLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.Step1ProgressLayout.Controls.Add(Me.Step1StatusLabel, 0, 0)
        Me.Step1ProgressLayout.Controls.Add(Me.Step1ProgressBar, 0, 1)
        Me.Step1ProgressLayout.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Step1ProgressLayout.Location = New System.Drawing.Point(5, 30)
        Me.Step1ProgressLayout.Name = "Step1ProgressLayout"
        Me.Step1ProgressLayout.RowCount = 2
        Me.Step1ProgressLayout.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.Step1ProgressLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.Step1ProgressLayout.Size = New System.Drawing.Size(456, 45)
        Me.Step1ProgressLayout.TabIndex = 0
        '
        'Step1StatusLabel
        '
        Me.Step1StatusLabel.AutoEllipsis = True
        Me.Step1StatusLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Step1StatusLabel.Location = New System.Drawing.Point(3, 0)
        Me.Step1StatusLabel.Name = "Step1StatusLabel"
        Me.Step1StatusLabel.Size = New System.Drawing.Size(450, 13)
        Me.Step1StatusLabel.TabIndex = 2
        Me.Step1StatusLabel.Text = "Waiting..."
        '
        'Step1ProgressBar
        '
        Me.Step1ProgressBar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Step1ProgressBar.Location = New System.Drawing.Point(3, 16)
        Me.Step1ProgressBar.MarqueeAnimationSpeed = 50
        Me.Step1ProgressBar.Name = "Step1ProgressBar"
        Me.Step1ProgressBar.Size = New System.Drawing.Size(450, 26)
        Me.Step1ProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.Step1ProgressBar.TabIndex = 3
        Me.Step1ProgressBar.Value = 100
        '
        'Step1Label
        '
        Me.Step1Label.AutoSize = True
        Me.Step1Label.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Step1Label.Location = New System.Drawing.Point(5, 2)
        Me.Step1Label.Name = "Step1Label"
        Me.Step1Label.Padding = New System.Windows.Forms.Padding(0, 5, 0, 5)
        Me.Step1Label.Size = New System.Drawing.Size(456, 23)
        Me.Step1Label.TabIndex = 1
        Me.Step1Label.Text = "Step 1 : Building files list"
        '
        'ButtonsLayoutPanel
        '
        Me.ButtonsLayoutPanel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonsLayoutPanel.ColumnCount = 1
        Me.ButtonsLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ButtonsLayoutPanel.Controls.Add(Me.SyncBtn, 0, 0)
        Me.ButtonsLayoutPanel.Controls.Add(Me.StopBtn, 0, 1)
        Me.ButtonsLayoutPanel.Location = New System.Drawing.Point(362, 281)
        Me.ButtonsLayoutPanel.Name = "ButtonsLayoutPanel"
        Me.ButtonsLayoutPanel.RowCount = 2
        Me.ButtonsLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.ButtonsLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.ButtonsLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.ButtonsLayoutPanel.Size = New System.Drawing.Size(124, 69)
        Me.ButtonsLayoutPanel.TabIndex = 1
        '
        'SyncBtn
        '
        Me.SyncBtn.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SyncBtn.Location = New System.Drawing.Point(3, 3)
        Me.SyncBtn.Name = "SyncBtn"
        Me.SyncBtn.Size = New System.Drawing.Size(118, 28)
        Me.SyncBtn.TabIndex = 4
        Me.SyncBtn.Tag = ""
        Me.SyncBtn.Text = "Synchronize"
        Me.SyncBtn.UseVisualStyleBackColor = True
        '
        'StopBtn
        '
        Me.StopBtn.Dock = System.Windows.Forms.DockStyle.Fill
        Me.StopBtn.Location = New System.Drawing.Point(3, 37)
        Me.StopBtn.Name = "StopBtn"
        Me.StopBtn.Size = New System.Drawing.Size(118, 29)
        Me.StopBtn.TabIndex = 1
        Me.StopBtn.Tag = "Cancel;Close"
        Me.StopBtn.Text = "Cancel"
        Me.StopBtn.UseVisualStyleBackColor = True
        '
        'StatisticsPanel
        '
        Me.StatisticsPanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.StatisticsPanel.ColumnCount = 4
        Me.StatisticsPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.0!))
        Me.StatisticsPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.0!))
        Me.StatisticsPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.StatisticsPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.StatisticsPanel.Controls.Add(Me.FoldersCreated, 3, 2)
        Me.StatisticsPanel.Controls.Add(Me.FilesCreatedLabel, 0, 2)
        Me.StatisticsPanel.Controls.Add(Me.FilesCreated, 1, 2)
        Me.StatisticsPanel.Controls.Add(Me.FoldersCreatedLabel, 2, 2)
        Me.StatisticsPanel.Controls.Add(Me.TotalCount, 3, 3)
        Me.StatisticsPanel.Controls.Add(Me.TotalCountLabel, 2, 3)
        Me.StatisticsPanel.Controls.Add(Me.Done, 1, 3)
        Me.StatisticsPanel.Controls.Add(Me.DoneLabel, 0, 3)
        Me.StatisticsPanel.Controls.Add(Me.Speed, 3, 0)
        Me.StatisticsPanel.Controls.Add(Me.SpeedLabel, 2, 0)
        Me.StatisticsPanel.Controls.Add(Me.ElapsedTime, 1, 0)
        Me.StatisticsPanel.Controls.Add(Me.ElapsedTimeLabel, 0, 0)
        Me.StatisticsPanel.Controls.Add(Me.BlankMargin, 0, 1)
        Me.StatisticsPanel.Location = New System.Drawing.Point(12, 281)
        Me.StatisticsPanel.Name = "StatisticsPanel"
        Me.StatisticsPanel.RowCount = 4
        Me.StatisticsPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.StatisticsPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.StatisticsPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.StatisticsPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.StatisticsPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.StatisticsPanel.Size = New System.Drawing.Size(344, 69)
        Me.StatisticsPanel.TabIndex = 2
        '
        'FoldersCreated
        '
        Me.FoldersCreated.AutoSize = True
        Me.FoldersCreated.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FoldersCreated.Location = New System.Drawing.Point(260, 34)
        Me.FoldersCreated.Name = "FoldersCreated"
        Me.FoldersCreated.Size = New System.Drawing.Size(81, 17)
        Me.FoldersCreated.TabIndex = 11
        Me.FoldersCreated.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'FilesCreatedLabel
        '
        Me.FilesCreatedLabel.AutoSize = True
        Me.FilesCreatedLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FilesCreatedLabel.Location = New System.Drawing.Point(3, 34)
        Me.FilesCreatedLabel.Name = "FilesCreatedLabel"
        Me.FilesCreatedLabel.Size = New System.Drawing.Size(90, 17)
        Me.FilesCreatedLabel.TabIndex = 10
        Me.FilesCreatedLabel.Text = "Files created:"
        Me.FilesCreatedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'FilesCreated
        '
        Me.FilesCreated.AutoSize = True
        Me.FilesCreated.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FilesCreated.Location = New System.Drawing.Point(99, 34)
        Me.FilesCreated.Name = "FilesCreated"
        Me.FilesCreated.Size = New System.Drawing.Size(69, 17)
        Me.FilesCreated.TabIndex = 9
        Me.FilesCreated.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'FoldersCreatedLabel
        '
        Me.FoldersCreatedLabel.AutoSize = True
        Me.FoldersCreatedLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FoldersCreatedLabel.Location = New System.Drawing.Point(174, 34)
        Me.FoldersCreatedLabel.Name = "FoldersCreatedLabel"
        Me.FoldersCreatedLabel.Size = New System.Drawing.Size(80, 17)
        Me.FoldersCreatedLabel.TabIndex = 8
        Me.FoldersCreatedLabel.Text = "Folders:"
        Me.FoldersCreatedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TotalCount
        '
        Me.TotalCount.AutoSize = True
        Me.TotalCount.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TotalCount.Location = New System.Drawing.Point(260, 51)
        Me.TotalCount.Name = "TotalCount"
        Me.TotalCount.Size = New System.Drawing.Size(81, 18)
        Me.TotalCount.TabIndex = 7
        Me.TotalCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TotalCountLabel
        '
        Me.TotalCountLabel.AutoSize = True
        Me.TotalCountLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TotalCountLabel.Location = New System.Drawing.Point(174, 51)
        Me.TotalCountLabel.Name = "TotalCountLabel"
        Me.TotalCountLabel.Size = New System.Drawing.Size(80, 18)
        Me.TotalCountLabel.TabIndex = 6
        Me.TotalCountLabel.Text = "Total:"
        Me.TotalCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Done
        '
        Me.Done.AutoSize = True
        Me.Done.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Done.Location = New System.Drawing.Point(99, 51)
        Me.Done.Name = "Done"
        Me.Done.Size = New System.Drawing.Size(69, 18)
        Me.Done.TabIndex = 5
        Me.Done.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'DoneLabel
        '
        Me.DoneLabel.AutoSize = True
        Me.DoneLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DoneLabel.Location = New System.Drawing.Point(3, 51)
        Me.DoneLabel.Name = "DoneLabel"
        Me.DoneLabel.Size = New System.Drawing.Size(90, 18)
        Me.DoneLabel.TabIndex = 4
        Me.DoneLabel.Text = "Done:"
        Me.DoneLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Speed
        '
        Me.Speed.AutoSize = True
        Me.Speed.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Speed.Location = New System.Drawing.Point(260, 0)
        Me.Speed.Name = "Speed"
        Me.Speed.Size = New System.Drawing.Size(81, 17)
        Me.Speed.TabIndex = 3
        Me.Speed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'SpeedLabel
        '
        Me.SpeedLabel.AutoSize = True
        Me.SpeedLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SpeedLabel.Location = New System.Drawing.Point(174, 0)
        Me.SpeedLabel.Name = "SpeedLabel"
        Me.SpeedLabel.Size = New System.Drawing.Size(80, 17)
        Me.SpeedLabel.TabIndex = 2
        Me.SpeedLabel.Text = "Speed:"
        Me.SpeedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ElapsedTime
        '
        Me.ElapsedTime.AutoSize = True
        Me.ElapsedTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ElapsedTime.Location = New System.Drawing.Point(99, 0)
        Me.ElapsedTime.Name = "ElapsedTime"
        Me.ElapsedTime.Size = New System.Drawing.Size(69, 17)
        Me.ElapsedTime.TabIndex = 1
        Me.ElapsedTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ElapsedTimeLabel
        '
        Me.ElapsedTimeLabel.AutoSize = True
        Me.ElapsedTimeLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ElapsedTimeLabel.Location = New System.Drawing.Point(3, 0)
        Me.ElapsedTimeLabel.Name = "ElapsedTimeLabel"
        Me.ElapsedTimeLabel.Size = New System.Drawing.Size(90, 17)
        Me.ElapsedTimeLabel.TabIndex = 0
        Me.ElapsedTimeLabel.Text = "Elapsed time:"
        Me.ElapsedTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'BlankMargin
        '
        Me.BlankMargin.AutoSize = True
        Me.StatisticsPanel.SetColumnSpan(Me.BlankMargin, 4)
        Me.BlankMargin.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlankMargin.Location = New System.Drawing.Point(3, 17)
        Me.BlankMargin.Name = "BlankMargin"
        Me.BlankMargin.Size = New System.Drawing.Size(338, 17)
        Me.BlankMargin.TabIndex = 12
        '
        'SyncingTimeCounter
        '
        Me.SyncingTimeCounter.Enabled = True
        Me.SyncingTimeCounter.Interval = 50
        '
        'PreviewList
        '
        Me.PreviewList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PreviewList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.TypeColumn, Me.ActionColumn, Me.DirectionColumn, Me.PathColumn})
        Me.PreviewList.FullRowSelect = True
        Me.PreviewList.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1})
        Me.PreviewList.Location = New System.Drawing.Point(12, 12)
        Me.PreviewList.MultiSelect = False
        Me.PreviewList.Name = "PreviewList"
        Me.PreviewList.Size = New System.Drawing.Size(474, 263)
        Me.PreviewList.SmallImageList = Me.SyncingIcons
        Me.PreviewList.TabIndex = 4
        Me.PreviewList.UseCompatibleStateImageBehavior = False
        Me.PreviewList.View = System.Windows.Forms.View.Details
        Me.PreviewList.Visible = False
        '
        'TypeColumn
        '
        Me.TypeColumn.Text = "Type"
        Me.TypeColumn.Width = 80
        '
        'ActionColumn
        '
        Me.ActionColumn.Text = "Action"
        Me.ActionColumn.Width = 80
        '
        'DirectionColumn
        '
        Me.DirectionColumn.Text = "Direction"
        Me.DirectionColumn.Width = 80
        '
        'PathColumn
        '
        Me.PathColumn.Text = "Path"
        Me.PathColumn.Width = 230
        '
        'SyncingIcons
        '
        Me.SyncingIcons.ImageStream = CType(resources.GetObject("SyncingIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.SyncingIcons.TransparentColor = System.Drawing.Color.Transparent
        Me.SyncingIcons.Images.SetKeyName(0, "go-next.png")
        Me.SyncingIcons.Images.SetKeyName(1, "go-previous.png")
        Me.SyncingIcons.Images.SetKeyName(2, "list-remove.png")
        Me.SyncingIcons.Images.SetKeyName(3, "folder-new.png")
        Me.SyncingIcons.Images.SetKeyName(4, "delete-folder.png")
        Me.SyncingIcons.Images.SetKeyName(5, "process-stop.png")
        '
        'SynchronizeForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(498, 362)
        Me.Controls.Add(Me.PreviewList)
        Me.Controls.Add(Me.StatisticsPanel)
        Me.Controls.Add(Me.ButtonsLayoutPanel)
        Me.Controls.Add(Me.MainLayoutPanel)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "SynchronizeForm"
        Me.Text = "Synchronizing"
        Me.MainLayoutPanel.ResumeLayout(False)
        Me.Step3LayoutPanel.ResumeLayout(False)
        Me.Step3LayoutPanel.PerformLayout()
        Me.Step3_ProgressLayout.ResumeLayout(False)
        Me.Step2LayoutPanel.ResumeLayout(False)
        Me.Step2LayoutPanel.PerformLayout()
        Me.Step2ProgressLayout.ResumeLayout(False)
        Me.Step1LayoutPanel.ResumeLayout(False)
        Me.Step1LayoutPanel.PerformLayout()
        Me.Step1ProgressLayout.ResumeLayout(False)
        Me.ButtonsLayoutPanel.ResumeLayout(False)
        Me.StatisticsPanel.ResumeLayout(False)
        Me.StatisticsPanel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents MainLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Step1LayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Step1ProgressLayout As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Step3LayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Step3_ProgressLayout As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Step3StatusLabel As System.Windows.Forms.Label
    Friend WithEvents Step3ProgressBar As System.Windows.Forms.ProgressBar
    Friend WithEvents Step3Label As System.Windows.Forms.Label
    Friend WithEvents Step2LayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Step2ProgressLayout As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Step2StatusLabel As System.Windows.Forms.Label
    Friend WithEvents Step2ProgressBar As System.Windows.Forms.ProgressBar
    Friend WithEvents Step2Label As System.Windows.Forms.Label
    Friend WithEvents Step1StatusLabel As System.Windows.Forms.Label
    Friend WithEvents Step1ProgressBar As System.Windows.Forms.ProgressBar
    Friend WithEvents Step1Label As System.Windows.Forms.Label
    Friend WithEvents ButtonsLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents StopBtn As System.Windows.Forms.Button
    Friend WithEvents StatisticsPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ElapsedTimeLabel As System.Windows.Forms.Label
    Friend WithEvents ElapsedTime As System.Windows.Forms.Label
    Friend WithEvents SyncingTimeCounter As System.Windows.Forms.Timer
    Friend WithEvents SyncBtn As System.Windows.Forms.Button
    Friend WithEvents PreviewList As System.Windows.Forms.ListView
    Friend WithEvents PathColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents ActionColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents DirectionColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents TypeColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents SpeedLabel As System.Windows.Forms.Label
    Friend WithEvents TotalCount As System.Windows.Forms.Label
    Friend WithEvents TotalCountLabel As System.Windows.Forms.Label
    Friend WithEvents Done As System.Windows.Forms.Label
    Friend WithEvents DoneLabel As System.Windows.Forms.Label
    Friend WithEvents Speed As System.Windows.Forms.Label
    Friend WithEvents FoldersCreated As System.Windows.Forms.Label
    Friend WithEvents FilesCreatedLabel As System.Windows.Forms.Label
    Friend WithEvents FilesCreated As System.Windows.Forms.Label
    Friend WithEvents FoldersCreatedLabel As System.Windows.Forms.Label
    Friend WithEvents SyncingIcons As System.Windows.Forms.ImageList
    Friend WithEvents BlankMargin As System.Windows.Forms.Label
End Class
