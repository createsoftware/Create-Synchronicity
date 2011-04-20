Public Class LanguageForm
    Sub New()
        ' This call is required by the designer.
        InitializeComponent()
#If CONFIG = "Linux" Then
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
#End If

        Me.Icon = ProgramConfig.GetIcon()
        LanguageHandler.FillLanguageListBox(LanguagesList)
    End Sub

    Private Sub LanguageForm_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        If LanguagesList.SelectedIndex <> -1 Then ProgramConfig.SetProgramSetting(ConfigOptions.Language, LanguagesList.SelectedItem.ToString.Split("-"c)(0).Trim)
    End Sub

    Private Sub OkBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OkBtn.Click
        Me.Close()
    End Sub
End Class