'This file is part of Create Synchronicity.
'
'Create Synchronicity is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
'Create Synchronicity is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with Create Synchronicity.  If not, see <http://www.gnu.org/licenses/>.
'Created by:	Clément Pit--Claudel.
'Web site:		http://synchronicity.sourceforge.net.

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
        If Not IO.File.Exists(GetConfigFilePath()) Then Exit Function
        Dim FileReader As New IO.StreamReader(GetConfigFilePath())

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
            Dim FileWriter As New IO.StreamWriter(GetConfigFilePath())

            For Each Setting As KeyValuePair(Of String, String) In Configuration
                FileWriter.WriteLine(Setting.Key & "=" & Setting.Value)
            Next

            FileWriter.Close()
            Return True
        Catch Ex As Exception
            Return False
        End Try
    End Function

    Sub DeleteConfigFile()
        IO.File.Delete(GetConfigFilePath())
    End Sub

    Function CheckConfigValidity()
        Return IO.Directory.Exists(GetSetting("From")) And IO.Directory.Exists(GetSetting("To"))
    End Function

    Function GetConfigFilePath() As String
        Return ConfigPath & ConfigName & ".sync"
    End Function

    Sub SetSetting(ByVal SettingName As String, ByVal Value As Object)
        Configuration(SettingName) = Value
    End Sub

    Sub SetSetting(ByVal SettingName As String, ByRef SettingField As Object, ByVal LoadSetting As Boolean)
        If LoadSetting Then
            SettingField = GetSetting(SettingName, SettingField)
        Else
            Configuration(SettingName) = SettingField
        End If
    End Sub

    Function GetSetting(ByVal SettingName As String, Optional ByRef DefaultVal As Object = Nothing) As String
        If Configuration.ContainsKey(SettingName) Then
            Return Configuration(SettingName)
        Else
            Return DefaultVal
        End If
    End Function
End Class
