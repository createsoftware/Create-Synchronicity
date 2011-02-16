<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LanguageForm
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
        Me.OkBtn = New System.Windows.Forms.Button()
        Me.LanguagesList = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'OkBtn
        '
        Me.OkBtn.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OkBtn.Location = New System.Drawing.Point(232, 12)
        Me.OkBtn.Name = "OkBtn"
        Me.OkBtn.Size = New System.Drawing.Size(87, 23)
        Me.OkBtn.TabIndex = 0
        Me.OkBtn.Text = "Ok"
        Me.OkBtn.UseVisualStyleBackColor = True
        '
        'LanguagesList
        '
        Me.LanguagesList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LanguagesList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.LanguagesList.Location = New System.Drawing.Point(12, 13)
        Me.LanguagesList.Name = "LanguagesList"
        Me.LanguagesList.Size = New System.Drawing.Size(214, 21)
        Me.LanguagesList.TabIndex = 4
        '
        'LanguageForm
        '
        Me.AcceptButton = Me.OkBtn
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(331, 47)
        Me.Controls.Add(Me.LanguagesList)
        Me.Controls.Add(Me.OkBtn)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "LanguageForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Please select your language"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents OkBtn As System.Windows.Forms.Button
    Friend WithEvents LanguagesList As System.Windows.Forms.ComboBox
End Class
