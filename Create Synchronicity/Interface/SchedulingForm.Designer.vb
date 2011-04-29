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
        Me.ActionsPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.Cancel = New System.Windows.Forms.Button()
        Me.Save = New System.Windows.Forms.Button()
        Me.WarningLabel = New System.Windows.Forms.Label()
        Me.Enable = New System.Windows.Forms.CheckBox()
        Me.FrequencyLayoutPanel = New System.Windows.Forms.FlowLayoutPanel()
        Me.DailyBtn = New System.Windows.Forms.RadioButton()
        Me.WeeklyBtn = New System.Windows.Forms.RadioButton()
        Me.WeekDay = New System.Windows.Forms.ComboBox()
        Me.MonthlyBtn = New System.Windows.Forms.RadioButton()
        Me.MonthDay = New System.Windows.Forms.NumericUpDown()
        Me.Panel = New System.Windows.Forms.Panel()
        Me.AtLabel = New System.Windows.Forms.Label()
        Me.TimeSelectionPanel = New System.Windows.Forms.FlowLayoutPanel()
        Me.Time = New System.Windows.Forms.DateTimePicker()
        Me.OptionsLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.Catchup = New System.Windows.Forms.CheckBox()
        Me.ActionsPanel.SuspendLayout()
        Me.FrequencyLayoutPanel.SuspendLayout()
        CType(Me.MonthDay, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel.SuspendLayout()
        Me.TimeSelectionPanel.SuspendLayout()
        Me.OptionsLayoutPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'ActionsPanel
        '
        Me.ActionsPanel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ActionsPanel.ColumnCount = 2
        Me.ActionsPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.ActionsPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.ActionsPanel.Controls.Add(Me.Cancel, 1, 0)
        Me.ActionsPanel.Controls.Add(Me.Save, 0, 0)
        Me.ActionsPanel.Location = New System.Drawing.Point(362, 173)
        Me.ActionsPanel.Name = "ActionsPanel"
        Me.ActionsPanel.RowCount = 1
        Me.ActionsPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.ActionsPanel.Size = New System.Drawing.Size(200, 31)
        Me.ActionsPanel.TabIndex = 4
        '
        'Cancel
        '
        Me.Cancel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel.Location = New System.Drawing.Point(103, 3)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(94, 25)
        Me.Cancel.TabIndex = 1
        Me.Cancel.Text = "\CANCEL"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'Save
        '
        Me.Save.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Save.Location = New System.Drawing.Point(3, 3)
        Me.Save.Name = "Save"
        Me.Save.Size = New System.Drawing.Size(94, 25)
        Me.Save.TabIndex = 0
        Me.Save.Text = "\SAVE"
        Me.Save.UseVisualStyleBackColor = True
        '
        'WarningLabel
        '
        Me.WarningLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.WarningLabel.BackColor = System.Drawing.Color.Orange
        Me.WarningLabel.Location = New System.Drawing.Point(0, 0)
        Me.WarningLabel.Name = "WarningLabel"
        Me.WarningLabel.Padding = New System.Windows.Forms.Padding(2)
        Me.WarningLabel.Size = New System.Drawing.Size(574, 51)
        Me.WarningLabel.TabIndex = 0
        Me.WarningLabel.Text = "\SCHEDULE_WARNING"
        Me.WarningLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Enable
        '
        Me.Enable.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Enable.AutoSize = True
        Me.Enable.Location = New System.Drawing.Point(3, 3)
        Me.Enable.Name = "Enable"
        Me.Enable.Size = New System.Drawing.Size(144, 20)
        Me.Enable.TabIndex = 1
        Me.Enable.Text = "\SCHEDULE_ENABLE"
        Me.Enable.UseVisualStyleBackColor = True
        '
        'FrequencyLayoutPanel
        '
        Me.FrequencyLayoutPanel.AutoSize = True
        Me.FrequencyLayoutPanel.Controls.Add(Me.DailyBtn)
        Me.FrequencyLayoutPanel.Controls.Add(Me.WeeklyBtn)
        Me.FrequencyLayoutPanel.Controls.Add(Me.WeekDay)
        Me.FrequencyLayoutPanel.Controls.Add(Me.MonthlyBtn)
        Me.FrequencyLayoutPanel.Controls.Add(Me.MonthDay)
        Me.FrequencyLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FrequencyLayoutPanel.Location = New System.Drawing.Point(0, 0)
        Me.FrequencyLayoutPanel.Name = "FrequencyLayoutPanel"
        Me.FrequencyLayoutPanel.Size = New System.Drawing.Size(550, 77)
        Me.FrequencyLayoutPanel.TabIndex = 0
        '
        'DailyBtn
        '
        Me.DailyBtn.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.DailyBtn.AutoSize = True
        Me.DailyBtn.Checked = True
        Me.FrequencyLayoutPanel.SetFlowBreak(Me.DailyBtn, True)
        Me.DailyBtn.Location = New System.Drawing.Point(3, 3)
        Me.DailyBtn.Name = "DailyBtn"
        Me.DailyBtn.Size = New System.Drawing.Size(65, 17)
        Me.DailyBtn.TabIndex = 2
        Me.DailyBtn.TabStop = True
        Me.DailyBtn.Text = "\DAILY"
        Me.DailyBtn.UseVisualStyleBackColor = True
        '
        'WeeklyBtn
        '
        Me.WeeklyBtn.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.WeeklyBtn.AutoSize = True
        Me.WeeklyBtn.Location = New System.Drawing.Point(3, 26)
        Me.WeeklyBtn.Margin = New System.Windows.Forms.Padding(3, 3, 0, 3)
        Me.WeeklyBtn.Name = "WeeklyBtn"
        Me.WeeklyBtn.Size = New System.Drawing.Size(76, 21)
        Me.WeeklyBtn.TabIndex = 3
        Me.WeeklyBtn.TabStop = True
        Me.WeeklyBtn.Text = "\WEEKLY"
        Me.WeeklyBtn.UseVisualStyleBackColor = True
        '
        'WeekDay
        '
        Me.WeekDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.FrequencyLayoutPanel.SetFlowBreak(Me.WeekDay, True)
        Me.WeekDay.FormattingEnabled = True
        Me.WeekDay.Location = New System.Drawing.Point(79, 26)
        Me.WeekDay.Margin = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.WeekDay.Name = "WeekDay"
        Me.WeekDay.Size = New System.Drawing.Size(121, 21)
        Me.WeekDay.TabIndex = 1
        '
        'MonthlyBtn
        '
        Me.MonthlyBtn.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.MonthlyBtn.AutoSize = True
        Me.MonthlyBtn.Location = New System.Drawing.Point(3, 53)
        Me.MonthlyBtn.Margin = New System.Windows.Forms.Padding(3, 3, 0, 3)
        Me.MonthlyBtn.Name = "MonthlyBtn"
        Me.MonthlyBtn.Size = New System.Drawing.Size(84, 21)
        Me.MonthlyBtn.TabIndex = 5
        Me.MonthlyBtn.TabStop = True
        Me.MonthlyBtn.Text = "\MONTHLY"
        Me.MonthlyBtn.UseVisualStyleBackColor = True
        '
        'MonthDay
        '
        Me.MonthDay.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.MonthDay.AutoSize = True
        Me.MonthDay.Location = New System.Drawing.Point(87, 53)
        Me.MonthDay.Margin = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.MonthDay.Maximum = New Decimal(New Integer() {28, 0, 0, 0})
        Me.MonthDay.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.MonthDay.Name = "MonthDay"
        Me.MonthDay.Size = New System.Drawing.Size(37, 21)
        Me.MonthDay.TabIndex = 4
        Me.MonthDay.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Panel
        '
        Me.Panel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel.Controls.Add(Me.FrequencyLayoutPanel)
        Me.Panel.Enabled = False
        Me.Panel.Location = New System.Drawing.Point(12, 90)
        Me.Panel.Name = "Panel"
        Me.Panel.Size = New System.Drawing.Size(550, 77)
        Me.Panel.TabIndex = 2
        '
        'AtLabel
        '
        Me.AtLabel.AutoSize = True
        Me.AtLabel.Dock = System.Windows.Forms.DockStyle.Left
        Me.AtLabel.Location = New System.Drawing.Point(3, 0)
        Me.AtLabel.Name = "AtLabel"
        Me.AtLabel.Size = New System.Drawing.Size(27, 27)
        Me.AtLabel.TabIndex = 0
        Me.AtLabel.Text = "\AT"
        Me.AtLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TimeSelectionPanel
        '
        Me.TimeSelectionPanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TimeSelectionPanel.Controls.Add(Me.AtLabel)
        Me.TimeSelectionPanel.Controls.Add(Me.Time)
        Me.TimeSelectionPanel.Enabled = False
        Me.TimeSelectionPanel.Location = New System.Drawing.Point(12, 173)
        Me.TimeSelectionPanel.Name = "TimeSelectionPanel"
        Me.TimeSelectionPanel.Size = New System.Drawing.Size(344, 31)
        Me.TimeSelectionPanel.TabIndex = 3
        '
        'Time
        '
        Me.Time.CustomFormat = "HH:mm"
        Me.Time.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Time.Location = New System.Drawing.Point(36, 3)
        Me.Time.Name = "Time"
        Me.Time.ShowUpDown = True
        Me.Time.Size = New System.Drawing.Size(63, 21)
        Me.Time.TabIndex = 6
        Me.Time.Value = New Date(2011, 1, 1, 0, 0, 0, 0)
        '
        'OptionsLayoutPanel
        '
        Me.OptionsLayoutPanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OptionsLayoutPanel.ColumnCount = 2
        Me.OptionsLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.OptionsLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.OptionsLayoutPanel.Controls.Add(Me.Catchup, 1, 0)
        Me.OptionsLayoutPanel.Controls.Add(Me.Enable, 0, 0)
        Me.OptionsLayoutPanel.Location = New System.Drawing.Point(12, 58)
        Me.OptionsLayoutPanel.Name = "OptionsLayoutPanel"
        Me.OptionsLayoutPanel.RowCount = 1
        Me.OptionsLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.OptionsLayoutPanel.Size = New System.Drawing.Size(550, 26)
        Me.OptionsLayoutPanel.TabIndex = 5
        '
        'Catchup
        '
        Me.Catchup.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Catchup.AutoSize = True
        Me.Catchup.Enabled = False
        Me.Catchup.Location = New System.Drawing.Point(344, 3)
        Me.Catchup.Name = "Catchup"
        Me.Catchup.Size = New System.Drawing.Size(203, 20)
        Me.Catchup.TabIndex = 2
        Me.Catchup.Tag = "\CATCHUP_MISSED_BACKUPS_TAG"
        Me.Catchup.Text = "\CATCHUP_MISSED_BACKUPS"
        Me.Catchup.UseVisualStyleBackColor = True
        '
        'SchedulingForm
        '
        Me.AcceptButton = Me.Save
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel
        Me.ClientSize = New System.Drawing.Size(574, 216)
        Me.Controls.Add(Me.OptionsLayoutPanel)
        Me.Controls.Add(Me.TimeSelectionPanel)
        Me.Controls.Add(Me.Panel)
        Me.Controls.Add(Me.WarningLabel)
        Me.Controls.Add(Me.ActionsPanel)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SchedulingForm"
        Me.ShowInTaskbar = False
        Me.Text = "\SCHEDULING"
        Me.ActionsPanel.ResumeLayout(False)
        Me.FrequencyLayoutPanel.ResumeLayout(False)
        Me.FrequencyLayoutPanel.PerformLayout()
        CType(Me.MonthDay, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel.ResumeLayout(False)
        Me.Panel.PerformLayout()
        Me.TimeSelectionPanel.ResumeLayout(False)
        Me.TimeSelectionPanel.PerformLayout()
        Me.OptionsLayoutPanel.ResumeLayout(False)
        Me.OptionsLayoutPanel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents Save As System.Windows.Forms.Button
    Friend WithEvents ActionsPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents WarningLabel As System.Windows.Forms.Label
    Friend WithEvents Enable As System.Windows.Forms.CheckBox
    Friend WithEvents FrequencyLayoutPanel As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents Panel As System.Windows.Forms.Panel
    Friend WithEvents MonthDay As System.Windows.Forms.NumericUpDown
    Friend WithEvents WeekDay As System.Windows.Forms.ComboBox
    Friend WithEvents AtLabel As System.Windows.Forms.Label
    Friend WithEvents TimeSelectionPanel As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents OptionsLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Catchup As System.Windows.Forms.CheckBox
    Friend WithEvents DailyBtn As System.Windows.Forms.RadioButton
    Friend WithEvents MonthlyBtn As System.Windows.Forms.RadioButton
    Friend WithEvents WeeklyBtn As System.Windows.Forms.RadioButton
    Friend WithEvents Time As System.Windows.Forms.DateTimePicker

End Class
