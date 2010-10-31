Public Class LanguageForm
    Dim ProgramConfig As ConfigHandler = ConfigHandler.GetSingleton
    Dim Translation As LanguageHandler = LanguageHandler.GetSingleton

    Private Sub LanguageForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = ProgramConfig.GetIcon()

        Translation.FillLanguageListBox(Lng_LanguagesList)
    End Sub

    Private Sub LanguageForm_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        If Lng_LanguagesList.SelectedIndex <> -1 Then ProgramConfig.SetProgramSetting(ConfigOptions.Language, Lng_LanguagesList.SelectedItem.ToString.Split("-")(0).Trim)
    End Sub

    Private Sub Lng_OkBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lng_OkBtn.Click
        Me.Close()
    End Sub
End Class