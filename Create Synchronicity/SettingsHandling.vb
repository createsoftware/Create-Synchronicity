Class SettingsHandler
    Public ConfigName As String
    Public Configuration As New Dictionary(Of String, String)
    Public LeftCheckedNodes As New SortedList(Of String, Boolean)
    Public RightCheckedNodes As New SortedList(Of String, Boolean)
    Private ConfigPath As String = Application.StartupPath & "\config\"

    Public Sub New(ByVal Name As String)
        ConfigName = Name
        LoadConfigFile()
    End Sub

    Function LoadConfigFile() As Boolean
        Dim ConfigString As String = ""
        If Not IO.File.Exists(ConfigPath & ConfigName & ".sync") Then Exit Function
        Dim FileReader As New IO.StreamReader(ConfigPath & ConfigName & ".sync")

        Configuration.Clear()
        While Not FileReader.EndOfStream
            Dim ConfigLine As String = FileReader.ReadLine()
            Configuration.Add(ConfigLine.Split("="c)(0), ConfigLine.Split("="c)(1))
        End While

        FileReader.Close()

        LeftCheckedNodes.Clear()
        RightCheckedNodes.Clear()
        For Each Dir As String In Configuration("EnabledLeftSubFolders").Split(";"c)
            If Not (Dir = "") Then
                If Dir.EndsWith("/") Then
                    LeftCheckedNodes.Add(Dir.Substring(0, Dir.Length - 1), True)
                Else
                    LeftCheckedNodes.Add(Dir, False)
                End If
            End If
        Next
        For Each Dir As String In Configuration("EnabledRightSubFolders").Split(";"c)
            If Not (Dir = "") Then
                If Dir.EndsWith("/") Then
                    RightCheckedNodes.Add(Dir.Substring(0, Dir.Length - 1), True)
                Else
                    RightCheckedNodes.Add(Dir, False)
                End If
            End If
        Next

        Return True
    End Function

    Function SaveConfigFile() As Boolean
        Try
            Dim ConfigString As String = ""
            Dim FileWriter As New IO.StreamWriter(ConfigPath & ConfigName & ".sync")

            For Each Setting As KeyValuePair(Of String, String) In Configuration
                FileWriter.WriteLine(Setting.Key & "=" & Setting.Value)
            Next

            FileWriter.Close()
            Return True
        Catch Ex As Exception
            Return False
        End Try
    End Function

    Sub SetSetting(ByVal SettingName As String, ByRef SettingField As Object, ByVal LoadSetting As Boolean)
        If LoadSetting Then
            SettingField = GetSetting(SettingName, SettingField)
        Else
            Configuration(SettingName) = SettingField
        End If
    End Sub

    Sub SetSetting(ByVal SettingName As String, ByVal Value As Object)
        Configuration(SettingName) = Value
    End Sub

    Function GetSetting(ByVal SettingName As String, Optional ByRef DefaultVal As Object = Nothing) As String
        If Configuration.ContainsKey(SettingName) Then
            Return Configuration(SettingName)
        Else
            Return DefaultVal
        End If
    End Function
End Class
