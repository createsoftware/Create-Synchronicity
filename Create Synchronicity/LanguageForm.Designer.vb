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
        Me.Lng_OkBtn = New System.Windows.Forms.Button()
        Me.Lng_LanguagesList = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'Lng_OkBtn
        '
        Me.Lng_OkBtn.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Lng_OkBtn.Location = New System.Drawing.Point(232, 12)
        Me.Lng_OkBtn.Name = "Lng_OkBtn"
        Me.Lng_OkBtn.Size = New System.Drawing.Size(87, 23)
        Me.Lng_OkBtn.TabIndex = 0
        Me.Lng_OkBtn.Text = "Ok"
        Me.Lng_OkBtn.UseVisualStyleBackColor = True
        '
        'Lng_LanguagesList
        '
        Me.Lng_LanguagesList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Lng_LanguagesList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Lng_LanguagesList.Location = New System.Drawing.Point(12, 13)
        Me.Lng_LanguagesList.Name = "Lng_LanguagesList"
        Me.Lng_LanguagesList.Size = New System.Drawing.Size(214, 21)
        Me.Lng_LanguagesList.TabIndex = 4
        '
        'LanguageForm
        '
        Me.AcceptButton = Me.Lng_OkBtn
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(331, 47)
        Me.Controls.Add(Me.Lng_LanguagesList)
        Me.Controls.Add(Me.Lng_OkBtn)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "LanguageForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Please select your language"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Lng_OkBtn As System.Windows.Forms.Button
    Friend WithEvents Lng_LanguagesList As System.Windows.Forms.ComboBox
End Class
