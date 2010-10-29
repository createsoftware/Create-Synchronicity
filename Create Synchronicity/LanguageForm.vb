Public Class LanguageForm
    Dim ProgramConfig As ConfigHandler = ConfigHandler.GetSingleton

    Private Sub LanguageForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = ProgramConfig.GetIcon()

        'TODO: Duplicate code.
        Lng_LanguagesList.Items.Clear()
        For Each File As String In IO.Directory.GetFiles(ProgramConfig.LanguageRootDir)
            Lng_LanguagesList.Items.Add(File.Remove(File.LastIndexOf(".")).Substring(File.LastIndexOf(ConfigOptions.DirSep) + 1))
        Next

        ProgramConfig.LoadProgramSettings()
        If Lng_LanguagesList.Items.Contains(ConfigOptions.DefaultLanguage) Then
            Lng_LanguagesList.SelectedIndex = Lng_LanguagesList.Items.IndexOf(ConfigOptions.DefaultLanguage)
        ElseIf Lng_LanguagesList.Items.Count > 0 Then
            Lng_LanguagesList.SelectedIndex = 0
        End If
    End Sub

    Private Sub LanguageForm_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        If Lng_LanguagesList.SelectedIndex <> -1 Then ProgramConfig.SetProgramSetting(ConfigOptions.Language, Lng_LanguagesList.SelectedItem.ToString)
    End Sub

    Private Sub Lng_OkBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lng_OkBtn.Click
        Me.Close()
    End Sub
End Class