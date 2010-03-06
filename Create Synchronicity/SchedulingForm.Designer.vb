<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SchedulingForm
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
        Me.Settings_ActionsPanel = New System.Windows.Forms.TableLayoutPanel
        Me.Scheduling_Cancel = New System.Windows.Forms.Button
        Me.Scheduling_Save = New System.Windows.Forms.Button
        Me.Scheduling_WarningLabel = New System.Windows.Forms.Label
        Me.Scheduling_Enable = New System.Windows.Forms.CheckBox
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel
        Me.Scheduling_EveryDay = New System.Windows.Forms.RadioButton
        Me.Scheduling_Panel = New System.Windows.Forms.Panel
        Me.FlowLayoutPanel3 = New System.Windows.Forms.FlowLayoutPanel
        Me.Scheduling_EveryMonth = New System.Windows.Forms.RadioButton
        Me.Scheduling_MonthDay = New System.Windows.Forms.NumericUpDown
        Me.FlowLayoutPanel2 = New System.Windows.Forms.FlowLayoutPanel
        Me.Scheduling_EveryWeek = New System.Windows.Forms.RadioButton
        Me.Scheduling_WeekDay = New System.Windows.Forms.ComboBox
        Me.Scheduling_AtLabel = New System.Windows.Forms.Label
        Me.Scheduling_TimeSelectionPanel = New System.Windows.Forms.FlowLayoutPanel
        Me.Scheduling_Hour = New System.Windows.Forms.NumericUpDown
        Me.Scheduling_HMSepLabel = New System.Windows.Forms.Label
        Me.Scheduling_Minute = New System.Windows.Forms.NumericUpDown
        Me.Settings_ActionsPanel.SuspendLayout()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.Scheduling_Panel.SuspendLayout()
        Me.FlowLayoutPanel3.SuspendLayout()
        CType(Me.Scheduling_MonthDay, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FlowLayoutPanel2.SuspendLayout()
        Me.Scheduling_TimeSelectionPanel.SuspendLayout()
        CType(Me.Scheduling_Hour, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Scheduling_Minute, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Settings_ActionsPanel
        '
        Me.Settings_ActionsPanel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Settings_ActionsPanel.ColumnCount = 2
        Me.Settings_ActionsPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.Settings_ActionsPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.Settings_ActionsPanel.Controls.Add(Me.Scheduling_Cancel, 1, 0)
        Me.Settings_ActionsPanel.Controls.Add(Me.Scheduling_Save, 0, 0)
        Me.Settings_ActionsPanel.Location = New System.Drawing.Point(362, 169)
        Me.Settings_ActionsPanel.Name = "Settings_ActionsPanel"
        Me.Settings_ActionsPanel.RowCount = 1
        Me.Settings_ActionsPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.Settings_ActionsPanel.Size = New System.Drawing.Size(200, 31)
        Me.Settings_ActionsPanel.TabIndex = 8
        '
        'Scheduling_Cancel
        '
        Me.Scheduling_Cancel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Scheduling_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Scheduling_Cancel.Location = New System.Drawing.Point(103, 3)
        Me.Scheduling_Cancel.Name = "Scheduling_Cancel"
        Me.Scheduling_Cancel.Size = New System.Drawing.Size(94, 25)
        Me.Scheduling_Cancel.TabIndex = 1
        Me.Scheduling_Cancel.Text = "\CANCEL"
        Me.Scheduling_Cancel.UseVisualStyleBackColor = True
        '
        'Scheduling_Save
        '
        Me.Scheduling_Save.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Scheduling_Save.Location = New System.Drawing.Point(3, 3)
        Me.Scheduling_Save.Name = "Scheduling_Save"
        Me.Scheduling_Save.Size = New System.Drawing.Size(94, 25)
        Me.Scheduling_Save.TabIndex = 0
        Me.Scheduling_Save.Text = "\SAVE"
        Me.Scheduling_Save.UseVisualStyleBackColor = True
        '
        'Scheduling_WarningLabel
        '
        Me.Scheduling_WarningLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.Scheduling_WarningLabel.BackColor = System.Drawing.Color.Orange
        Me.Scheduling_WarningLabel.Location = New System.Drawing.Point(0, 0)
        Me.Scheduling_WarningLabel.Name = "Scheduling_WarningLabel"
        Me.Scheduling_WarningLabel.Size = New System.Drawing.Size(574, 51)
        Me.Scheduling_WarningLabel.TabIndex = 9
        Me.Scheduling_WarningLabel.Text = "\SCHEDULE_WARNING"
        Me.Scheduling_WarningLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Scheduling_Enable
        '
        Me.Scheduling_Enable.AutoSize = True
        Me.Scheduling_Enable.Location = New System.Drawing.Point(12, 63)
        Me.Scheduling_Enable.Name = "Scheduling_Enable"
        Me.Scheduling_Enable.Size = New System.Drawing.Size(144, 17)
        Me.Scheduling_Enable.TabIndex = 10
        Me.Scheduling_Enable.Text = "\SCHEDULE_ENABLE"
        Me.Scheduling_Enable.UseVisualStyleBackColor = True
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.AutoSize = True
        Me.FlowLayoutPanel1.Controls.Add(Me.Scheduling_EveryDay)
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(550, 23)
        Me.FlowLayoutPanel1.TabIndex = 13
        '
        'Scheduling_EveryDay
        '
        Me.Scheduling_EveryDay.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.Scheduling_EveryDay.AutoSize = True
        Me.Scheduling_EveryDay.Checked = True
        Me.Scheduling_EveryDay.Location = New System.Drawing.Point(3, 3)
        Me.Scheduling_EveryDay.Name = "Scheduling_EveryDay"
        Me.Scheduling_EveryDay.Size = New System.Drawing.Size(98, 17)
        Me.Scheduling_EveryDay.TabIndex = 0
        Me.Scheduling_EveryDay.TabStop = True
        Me.Scheduling_EveryDay.Text = "\EVERY_DAY"
        Me.Scheduling_EveryDay.UseVisualStyleBackColor = True
        '
        'Scheduling_Panel
        '
        Me.Scheduling_Panel.Controls.Add(Me.FlowLayoutPanel3)
        Me.Scheduling_Panel.Controls.Add(Me.FlowLayoutPanel2)
        Me.Scheduling_Panel.Controls.Add(Me.FlowLayoutPanel1)
        Me.Scheduling_Panel.Enabled = False
        Me.Scheduling_Panel.Location = New System.Drawing.Point(12, 86)
        Me.Scheduling_Panel.Name = "Scheduling_Panel"
        Me.Scheduling_Panel.Size = New System.Drawing.Size(550, 77)
        Me.Scheduling_Panel.TabIndex = 14
        '
        'FlowLayoutPanel3
        '
        Me.FlowLayoutPanel3.AutoSize = True
        Me.FlowLayoutPanel3.Controls.Add(Me.Scheduling_EveryMonth)
        Me.FlowLayoutPanel3.Controls.Add(Me.Scheduling_MonthDay)
        Me.FlowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.FlowLayoutPanel3.Location = New System.Drawing.Point(0, 50)
        Me.FlowLayoutPanel3.Name = "FlowLayoutPanel3"
        Me.FlowLayoutPanel3.Size = New System.Drawing.Size(550, 27)
        Me.FlowLayoutPanel3.TabIndex = 15
        '
        'Scheduling_EveryMonth
        '
        Me.Scheduling_EveryMonth.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.Scheduling_EveryMonth.AutoSize = True
        Me.Scheduling_EveryMonth.Location = New System.Drawing.Point(3, 3)
        Me.Scheduling_EveryMonth.Name = "Scheduling_EveryMonth"
        Me.Scheduling_EveryMonth.Size = New System.Drawing.Size(115, 21)
        Me.Scheduling_EveryMonth.TabIndex = 0
        Me.Scheduling_EveryMonth.TabStop = True
        Me.Scheduling_EveryMonth.Text = "\EVERY_MONTH"
        Me.Scheduling_EveryMonth.UseVisualStyleBackColor = True
        '
        'Scheduling_MonthDay
        '
        Me.Scheduling_MonthDay.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.Scheduling_MonthDay.AutoSize = True
        Me.Scheduling_MonthDay.Location = New System.Drawing.Point(124, 3)
        Me.Scheduling_MonthDay.Maximum = New Decimal(New Integer() {28, 0, 0, 0})
        Me.Scheduling_MonthDay.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.Scheduling_MonthDay.Name = "Scheduling_MonthDay"
        Me.Scheduling_MonthDay.Size = New System.Drawing.Size(37, 21)
        Me.Scheduling_MonthDay.TabIndex = 4
        Me.Scheduling_MonthDay.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'FlowLayoutPanel2
        '
        Me.FlowLayoutPanel2.AutoSize = True
        Me.FlowLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.FlowLayoutPanel2.Controls.Add(Me.Scheduling_EveryWeek)
        Me.FlowLayoutPanel2.Controls.Add(Me.Scheduling_WeekDay)
        Me.FlowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.FlowLayoutPanel2.Location = New System.Drawing.Point(0, 23)
        Me.FlowLayoutPanel2.Name = "FlowLayoutPanel2"
        Me.FlowLayoutPanel2.Size = New System.Drawing.Size(550, 27)
        Me.FlowLayoutPanel2.TabIndex = 14
        '
        'Scheduling_EveryWeek
        '
        Me.Scheduling_EveryWeek.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.Scheduling_EveryWeek.AutoSize = True
        Me.Scheduling_EveryWeek.Location = New System.Drawing.Point(3, 3)
        Me.Scheduling_EveryWeek.Name = "Scheduling_EveryWeek"
        Me.Scheduling_EveryWeek.Size = New System.Drawing.Size(107, 21)
        Me.Scheduling_EveryWeek.TabIndex = 0
        Me.Scheduling_EveryWeek.TabStop = True
        Me.Scheduling_EveryWeek.Text = "\EVERY_WEEK"
        Me.Scheduling_EveryWeek.UseVisualStyleBackColor = True
        '
        'Scheduling_WeekDay
        '
        Me.Scheduling_WeekDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Scheduling_WeekDay.FormattingEnabled = True
        Me.Scheduling_WeekDay.Location = New System.Drawing.Point(116, 3)
        Me.Scheduling_WeekDay.Name = "Scheduling_WeekDay"
        Me.Scheduling_WeekDay.Size = New System.Drawing.Size(121, 21)
        Me.Scheduling_WeekDay.TabIndex = 15
        '
        'Scheduling_AtLabel
        '
        Me.Scheduling_AtLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.Scheduling_AtLabel.AutoSize = True
        Me.Scheduling_AtLabel.Location = New System.Drawing.Point(3, 0)
        Me.Scheduling_AtLabel.Name = "Scheduling_AtLabel"
        Me.Scheduling_AtLabel.Size = New System.Drawing.Size(27, 27)
        Me.Scheduling_AtLabel.TabIndex = 15
        Me.Scheduling_AtLabel.Text = "\AT"
        Me.Scheduling_AtLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Scheduling_TimeSelectionPanel
        '
        Me.Scheduling_TimeSelectionPanel.Controls.Add(Me.Scheduling_AtLabel)
        Me.Scheduling_TimeSelectionPanel.Controls.Add(Me.Scheduling_Hour)
        Me.Scheduling_TimeSelectionPanel.Controls.Add(Me.Scheduling_HMSepLabel)
        Me.Scheduling_TimeSelectionPanel.Controls.Add(Me.Scheduling_Minute)
        Me.Scheduling_TimeSelectionPanel.Enabled = False
        Me.Scheduling_TimeSelectionPanel.Location = New System.Drawing.Point(12, 169)
        Me.Scheduling_TimeSelectionPanel.Name = "Scheduling_TimeSelectionPanel"
        Me.Scheduling_TimeSelectionPanel.Size = New System.Drawing.Size(344, 27)
        Me.Scheduling_TimeSelectionPanel.TabIndex = 16
        '
        'Scheduling_Hour
        '
        Me.Scheduling_Hour.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.Scheduling_Hour.AutoSize = True
        Me.Scheduling_Hour.Location = New System.Drawing.Point(36, 3)
        Me.Scheduling_Hour.Maximum = New Decimal(New Integer() {23, 0, 0, 0})
        Me.Scheduling_Hour.Name = "Scheduling_Hour"
        Me.Scheduling_Hour.Size = New System.Drawing.Size(37, 21)
        Me.Scheduling_Hour.TabIndex = 4
        '
        'Scheduling_HMSepLabel
        '
        Me.Scheduling_HMSepLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.Scheduling_HMSepLabel.AutoSize = True
        Me.Scheduling_HMSepLabel.Location = New System.Drawing.Point(79, 0)
        Me.Scheduling_HMSepLabel.Name = "Scheduling_HMSepLabel"
        Me.Scheduling_HMSepLabel.Size = New System.Drawing.Size(65, 27)
        Me.Scheduling_HMSepLabel.TabIndex = 16
        Me.Scheduling_HMSepLabel.Text = "\H_M_SEP"
        Me.Scheduling_HMSepLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Scheduling_Minute
        '
        Me.Scheduling_Minute.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.Scheduling_Minute.AutoSize = True
        Me.Scheduling_Minute.Location = New System.Drawing.Point(150, 3)
        Me.Scheduling_Minute.Maximum = New Decimal(New Integer() {59, 0, 0, 0})
        Me.Scheduling_Minute.Name = "Scheduling_Minute"
        Me.Scheduling_Minute.Size = New System.Drawing.Size(37, 21)
        Me.Scheduling_Minute.TabIndex = 17
        '
        'SchedulingForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(574, 212)
        Me.Controls.Add(Me.Scheduling_TimeSelectionPanel)
        Me.Controls.Add(Me.Scheduling_Panel)
        Me.Controls.Add(Me.Scheduling_Enable)
        Me.Controls.Add(Me.Scheduling_WarningLabel)
        Me.Controls.Add(Me.Settings_ActionsPanel)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SchedulingForm"
        Me.ShowInTaskbar = False
        Me.Text = "SchedulingForm"
        Me.Settings_ActionsPanel.ResumeLayout(False)
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.FlowLayoutPanel1.PerformLayout()
        Me.Scheduling_Panel.ResumeLayout(False)
        Me.Scheduling_Panel.PerformLayout()
        Me.FlowLayoutPanel3.ResumeLayout(False)
        Me.FlowLayoutPanel3.PerformLayout()
        CType(Me.Scheduling_MonthDay, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FlowLayoutPanel2.ResumeLayout(False)
        Me.FlowLayoutPanel2.PerformLayout()
        Me.Scheduling_TimeSelectionPanel.ResumeLayout(False)
        Me.Scheduling_TimeSelectionPanel.PerformLayout()
        CType(Me.Scheduling_Hour, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Scheduling_Minute, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Scheduling_Cancel As System.Windows.Forms.Button
    Friend WithEvents Scheduling_Save As System.Windows.Forms.Button
    Friend WithEvents Settings_ActionsPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Scheduling_WarningLabel As System.Windows.Forms.Label
    Friend WithEvents Scheduling_Enable As System.Windows.Forms.CheckBox
    Friend WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents Scheduling_Panel As System.Windows.Forms.Panel
    Friend WithEvents FlowLayoutPanel3 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents Scheduling_EveryMonth As System.Windows.Forms.RadioButton
    Friend WithEvents FlowLayoutPanel2 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents Scheduling_EveryWeek As System.Windows.Forms.RadioButton
    Friend WithEvents Scheduling_MonthDay As System.Windows.Forms.NumericUpDown
    Friend WithEvents Scheduling_EveryDay As System.Windows.Forms.RadioButton
    Friend WithEvents Scheduling_WeekDay As System.Windows.Forms.ComboBox
    Friend WithEvents Scheduling_AtLabel As System.Windows.Forms.Label
    Friend WithEvents Scheduling_TimeSelectionPanel As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents Scheduling_Hour As System.Windows.Forms.NumericUpDown
    Friend WithEvents Scheduling_HMSepLabel As System.Windows.Forms.Label
    Friend WithEvents Scheduling_Minute As System.Windows.Forms.NumericUpDown

End Class
