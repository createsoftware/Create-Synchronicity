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
        Me.MainLayoutPanel = New System.Windows.Forms.TableLayoutPanel
        Me.Step3LayoutPanel = New System.Windows.Forms.TableLayoutPanel
        Me.TableLayoutPanel6 = New System.Windows.Forms.TableLayoutPanel
        Me.Step3StatusLabel = New System.Windows.Forms.Label
        Me.Step3ProgressBar = New System.Windows.Forms.ProgressBar
        Me.Step3Label = New System.Windows.Forms.Label
        Me.Step2LayoutPanel = New System.Windows.Forms.TableLayoutPanel
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel
        Me.Step2StatusLabel = New System.Windows.Forms.Label
        Me.Step2ProgressBar = New System.Windows.Forms.ProgressBar
        Me.Step2Label = New System.Windows.Forms.Label
        Me.Step1LayoutPanel = New System.Windows.Forms.TableLayoutPanel
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel
        Me.Step1StatusLabel = New System.Windows.Forms.Label
        Me.Step1ProgressBar = New System.Windows.Forms.ProgressBar
        Me.Step1Label = New System.Windows.Forms.Label
        Me.ButtonsLayoutPanel = New System.Windows.Forms.TableLayoutPanel
        Me.StopBtn = New System.Windows.Forms.Button
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.ElapsedTime = New System.Windows.Forms.Label
        Me.ElapsedTimeLabel = New System.Windows.Forms.Label
        Me.SyncingTimeCounter = New System.Windows.Forms.Timer(Me.components)
        Me.MainLayoutPanel.SuspendLayout()
        Me.Step3LayoutPanel.SuspendLayout()
        Me.TableLayoutPanel6.SuspendLayout()
        Me.Step2LayoutPanel.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.Step1LayoutPanel.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.ButtonsLayoutPanel.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
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
        Me.MainLayoutPanel.Size = New System.Drawing.Size(482, 249)
        Me.MainLayoutPanel.TabIndex = 0
        '
        'Step3LayoutPanel
        '
        Me.Step3LayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset
        Me.Step3LayoutPanel.ColumnCount = 1
        Me.Step3LayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.Step3LayoutPanel.Controls.Add(Me.TableLayoutPanel6, 0, 1)
        Me.Step3LayoutPanel.Controls.Add(Me.Step3Label, 0, 0)
        Me.Step3LayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Step3LayoutPanel.Location = New System.Drawing.Point(4, 168)
        Me.Step3LayoutPanel.Name = "Step3LayoutPanel"
        Me.Step3LayoutPanel.RowCount = 2
        Me.Step3LayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.Step3LayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.Step3LayoutPanel.Size = New System.Drawing.Size(474, 77)
        Me.Step3LayoutPanel.TabIndex = 2
        '
        'TableLayoutPanel6
        '
        Me.TableLayoutPanel6.ColumnCount = 1
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel6.Controls.Add(Me.Step3StatusLabel, 0, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.Step3ProgressBar, 0, 1)
        Me.TableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(5, 30)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 2
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(464, 42)
        Me.TableLayoutPanel6.TabIndex = 0
        '
        'Step3StatusLabel
        '
        Me.Step3StatusLabel.AutoEllipsis = True
        Me.Step3StatusLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Step3StatusLabel.Location = New System.Drawing.Point(3, 0)
        Me.Step3StatusLabel.Name = "Step3StatusLabel"
        Me.Step3StatusLabel.Size = New System.Drawing.Size(458, 13)
        Me.Step3StatusLabel.TabIndex = 2
        Me.Step3StatusLabel.Text = "Waiting..."
        '
        'Step3ProgressBar
        '
        Me.Step3ProgressBar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Step3ProgressBar.Location = New System.Drawing.Point(3, 16)
        Me.Step3ProgressBar.Name = "Step3ProgressBar"
        Me.Step3ProgressBar.Size = New System.Drawing.Size(458, 23)
        Me.Step3ProgressBar.TabIndex = 3
        '
        'Step3Label
        '
        Me.Step3Label.AutoSize = True
        Me.Step3Label.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Step3Label.Location = New System.Drawing.Point(5, 2)
        Me.Step3Label.Name = "Step3Label"
        Me.Step3Label.Padding = New System.Windows.Forms.Padding(0, 5, 0, 5)
        Me.Step3Label.Size = New System.Drawing.Size(464, 23)
        Me.Step3Label.TabIndex = 1
        Me.Step3Label.Text = "Step 3 : Cleaning up"
        '
        'Step2LayoutPanel
        '
        Me.Step2LayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset
        Me.Step2LayoutPanel.ColumnCount = 1
        Me.Step2LayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.Step2LayoutPanel.Controls.Add(Me.TableLayoutPanel4, 0, 1)
        Me.Step2LayoutPanel.Controls.Add(Me.Step2Label, 0, 0)
        Me.Step2LayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Step2LayoutPanel.Location = New System.Drawing.Point(4, 86)
        Me.Step2LayoutPanel.Name = "Step2LayoutPanel"
        Me.Step2LayoutPanel.RowCount = 2
        Me.Step2LayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.Step2LayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.Step2LayoutPanel.Size = New System.Drawing.Size(474, 75)
        Me.Step2LayoutPanel.TabIndex = 1
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 1
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.Step2StatusLabel, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.Step2ProgressBar, 0, 1)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(5, 30)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 2
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(464, 40)
        Me.TableLayoutPanel4.TabIndex = 0
        '
        'Step2StatusLabel
        '
        Me.Step2StatusLabel.AutoEllipsis = True
        Me.Step2StatusLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Step2StatusLabel.Location = New System.Drawing.Point(3, 0)
        Me.Step2StatusLabel.Name = "Step2StatusLabel"
        Me.Step2StatusLabel.Size = New System.Drawing.Size(458, 13)
        Me.Step2StatusLabel.TabIndex = 2
        Me.Step2StatusLabel.Text = "Waiting..."
        '
        'Step2ProgressBar
        '
        Me.Step2ProgressBar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Step2ProgressBar.Location = New System.Drawing.Point(3, 16)
        Me.Step2ProgressBar.Name = "Step2ProgressBar"
        Me.Step2ProgressBar.Size = New System.Drawing.Size(458, 21)
        Me.Step2ProgressBar.TabIndex = 3
        '
        'Step2Label
        '
        Me.Step2Label.AutoSize = True
        Me.Step2Label.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Step2Label.Location = New System.Drawing.Point(5, 2)
        Me.Step2Label.Name = "Step2Label"
        Me.Step2Label.Padding = New System.Windows.Forms.Padding(0, 5, 0, 5)
        Me.Step2Label.Size = New System.Drawing.Size(464, 23)
        Me.Step2Label.TabIndex = 1
        Me.Step2Label.Text = "Step 2 : Copying files both ways"
        '
        'Step1LayoutPanel
        '
        Me.Step1LayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset
        Me.Step1LayoutPanel.ColumnCount = 1
        Me.Step1LayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.Step1LayoutPanel.Controls.Add(Me.TableLayoutPanel2, 0, 1)
        Me.Step1LayoutPanel.Controls.Add(Me.Step1Label, 0, 0)
        Me.Step1LayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Step1LayoutPanel.Location = New System.Drawing.Point(4, 4)
        Me.Step1LayoutPanel.Name = "Step1LayoutPanel"
        Me.Step1LayoutPanel.RowCount = 2
        Me.Step1LayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.Step1LayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.Step1LayoutPanel.Size = New System.Drawing.Size(474, 75)
        Me.Step1LayoutPanel.TabIndex = 0
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.Step1StatusLabel, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Step1ProgressBar, 0, 1)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(5, 30)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(464, 40)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'Step1StatusLabel
        '
        Me.Step1StatusLabel.AutoEllipsis = True
        Me.Step1StatusLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Step1StatusLabel.Location = New System.Drawing.Point(3, 0)
        Me.Step1StatusLabel.Name = "Step1StatusLabel"
        Me.Step1StatusLabel.Size = New System.Drawing.Size(458, 13)
        Me.Step1StatusLabel.TabIndex = 2
        Me.Step1StatusLabel.Text = "Waiting..."
        '
        'Step1ProgressBar
        '
        Me.Step1ProgressBar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Step1ProgressBar.Location = New System.Drawing.Point(3, 16)
        Me.Step1ProgressBar.MarqueeAnimationSpeed = 50
        Me.Step1ProgressBar.Name = "Step1ProgressBar"
        Me.Step1ProgressBar.Size = New System.Drawing.Size(458, 21)
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
        Me.Step1Label.Size = New System.Drawing.Size(464, 23)
        Me.Step1Label.TabIndex = 1
        Me.Step1Label.Text = "Step 1 : Building files list"
        '
        'ButtonsLayoutPanel
        '
        Me.ButtonsLayoutPanel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonsLayoutPanel.ColumnCount = 1
        Me.ButtonsLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ButtonsLayoutPanel.Controls.Add(Me.StopBtn, 0, 0)
        Me.ButtonsLayoutPanel.Location = New System.Drawing.Point(370, 301)
        Me.ButtonsLayoutPanel.Name = "ButtonsLayoutPanel"
        Me.ButtonsLayoutPanel.RowCount = 1
        Me.ButtonsLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ButtonsLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.ButtonsLayoutPanel.Size = New System.Drawing.Size(124, 35)
        Me.ButtonsLayoutPanel.TabIndex = 1
        '
        'StopBtn
        '
        Me.StopBtn.Dock = System.Windows.Forms.DockStyle.Fill
        Me.StopBtn.Location = New System.Drawing.Point(3, 3)
        Me.StopBtn.Name = "StopBtn"
        Me.StopBtn.Size = New System.Drawing.Size(118, 29)
        Me.StopBtn.TabIndex = 1
        Me.StopBtn.Tag = "Stop;Done!"
        Me.StopBtn.Text = "Stop"
        Me.StopBtn.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.TableLayoutPanel1.ColumnCount = 4
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.ElapsedTime, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ElapsedTimeLabel, 0, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(12, 267)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(352, 69)
        Me.TableLayoutPanel1.TabIndex = 2
        '
        'ElapsedTime
        '
        Me.ElapsedTime.AutoSize = True
        Me.ElapsedTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ElapsedTime.Location = New System.Drawing.Point(109, 1)
        Me.ElapsedTime.Name = "ElapsedTime"
        Me.ElapsedTime.Size = New System.Drawing.Size(63, 16)
        Me.ElapsedTime.TabIndex = 1
        Me.ElapsedTime.Text = "0s."
        Me.ElapsedTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ElapsedTimeLabel
        '
        Me.ElapsedTimeLabel.AutoSize = True
        Me.ElapsedTimeLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ElapsedTimeLabel.Location = New System.Drawing.Point(4, 1)
        Me.ElapsedTimeLabel.Name = "ElapsedTimeLabel"
        Me.ElapsedTimeLabel.Size = New System.Drawing.Size(98, 16)
        Me.ElapsedTimeLabel.TabIndex = 0
        Me.ElapsedTimeLabel.Text = "Elapsed time:"
        Me.ElapsedTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'SyncingTimeCounter
        '
        Me.SyncingTimeCounter.Enabled = True
        Me.SyncingTimeCounter.Interval = 1000
        '
        'SynchronizeForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(506, 348)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.ButtonsLayoutPanel)
        Me.Controls.Add(Me.MainLayoutPanel)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "SynchronizeForm"
        Me.Text = "Synchronizing"
        Me.MainLayoutPanel.ResumeLayout(False)
        Me.Step3LayoutPanel.ResumeLayout(False)
        Me.Step3LayoutPanel.PerformLayout()
        Me.TableLayoutPanel6.ResumeLayout(False)
        Me.Step2LayoutPanel.ResumeLayout(False)
        Me.Step2LayoutPanel.PerformLayout()
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.Step1LayoutPanel.ResumeLayout(False)
        Me.Step1LayoutPanel.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.ButtonsLayoutPanel.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents MainLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Step1LayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Step3LayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel6 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Step3StatusLabel As System.Windows.Forms.Label
    Friend WithEvents Step3ProgressBar As System.Windows.Forms.ProgressBar
    Friend WithEvents Step3Label As System.Windows.Forms.Label
    Friend WithEvents Step2LayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel4 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Step2StatusLabel As System.Windows.Forms.Label
    Friend WithEvents Step2ProgressBar As System.Windows.Forms.ProgressBar
    Friend WithEvents Step2Label As System.Windows.Forms.Label
    Friend WithEvents Step1StatusLabel As System.Windows.Forms.Label
    Friend WithEvents Step1ProgressBar As System.Windows.Forms.ProgressBar
    Friend WithEvents Step1Label As System.Windows.Forms.Label
    Friend WithEvents ButtonsLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents StopBtn As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ElapsedTimeLabel As System.Windows.Forms.Label
    Friend WithEvents ElapsedTime As System.Windows.Forms.Label
    Friend WithEvents SyncingTimeCounter As System.Windows.Forms.Timer
End Class
