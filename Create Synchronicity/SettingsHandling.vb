'This file is part of Create Synchronicity.
'
'Create Synchronicity is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
'Create Synchronicity is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with Create Synchronicity.  If not, see <http://www.gnu.org/licenses/>.
'Created by:	Clément Pit--Claudel.
'Web site:		http://synchronicity.sourceforge.net.

Public Module ConfigOptions
    Public Const Source As String = "Source Directory"
    Public Const Destination As String = "Destination Directory"
    Public Const IncludedTypes As String = "Included Filetypes"
    Public Const ExcludedTypes As String = "Excluded FileTypes"
    Public Const ReplicateEmptyDirectories As String = "Replicate Empty Directories"
    Public Const Method As String = "Synchronization Method"
    Public Const Restrictions As String = "Files restrictions"
    Public Const LeftSubFolders As String = "Source folders to be synchronized"
    Public Const RightSubFolders As String = "Destination folders to be synchronized"
    Public Const ComputeHash As String = "Compute Hash"
    Public Const PropagateUpdates As String = "Propagate Updates"

    Public Const AutoUpdates = "Auto updates"

    Dim ProgramSettingsLoaded As Boolean = False
    Public ConfigRootDir As String = Application.StartupPath & "\config"
    Public LogRootDir As String = Application.StartupPath & "\log"
    Public MainConfigFile As String = ConfigRootDir & "\mainconfig.ini"

    Dim ProgramSettings As New Dictionary(Of String, String)

    Public Function GetConfigPath(ByVal Name As String) As String
        Return ConfigRootDir & "\" & Name & ".sync"
    End Function

    Public Function GetLogPath(ByVal Name As String) As String
        Return LogRootDir & "\" & Name & ".log"
    End Function

    Public Sub CheckForUpdates(ByVal RoutineCheck As Boolean)
        Try
            Dim CurrentVersion As String = (New System.Net.WebClient).DownloadString("http://synchronicity.sourceforge.net/code/version.txt")
            If CurrentVersion = "" Then Throw New Exception()
            If (CurrentVersion <> Application.ProductVersion) Then
                If Microsoft.VisualBasic.MsgBox("A new version of Create Synchronicity is available!" & Microsoft.VisualBasic.vbNewLine & "Installed version: " & Application.ProductVersion & Microsoft.VisualBasic.vbNewLine & "Current version: " & CurrentVersion & Microsoft.VisualBasic.vbNewLine & "Visit download website?", Microsoft.VisualBasic.MsgBoxStyle.Question + Microsoft.VisualBasic.MsgBoxStyle.YesNo, "New version available!") = Microsoft.VisualBasic.MsgBoxResult.Yes Then
                    Diagnostics.Process.Start("http://synchronicity.sourceforge.net/downloads.html")
                End If
            Else
                If Not RoutineCheck Then Microsoft.VisualBasic.MsgBox("No updates available", Microsoft.VisualBasic.MsgBoxStyle.OkOnly + Microsoft.VisualBasic.MsgBoxStyle.Information)
            End If
        Catch Ex As Exception
            Microsoft.VisualBasic.MsgBox("Unable to connect to ""http://synchronicity.sourceforge.net"". Disable searching for updates by clicking ""About"".", Microsoft.VisualBasic.MsgBoxStyle.OkOnly + Microsoft.VisualBasic.MsgBoxStyle.Exclamation, "Network error!")
        End Try
    End Sub

    Public Function GetProgramSetting(ByVal Key As String, ByVal DefaultVal As String) As String
        If (ProgramSettings.ContainsKey(Key)) Then
            Return ProgramSettings(Key)
        Else
            Return DefaultVal
        End If
    End Function

    Public Sub SetProgramSetting(ByVal Key As String, ByVal Value As String)
        ProgramSettings(Key) = Value
    End Sub

    Public Sub LoadProgramSettings()
        If ProgramSettingsLoaded Then Exit Sub

        If Not IO.File.Exists(MainConfigFile) Then
            IO.File.Create(MainConfigFile).Close()
            Exit Sub
        End If

        Dim ConfigString As String = My.Computer.FileSystem.ReadAllText(MainConfigFile)
        Dim ConfigArray As New List(Of String)(ConfigString.Split(";"))
        For Each Setting As String In ConfigArray
            Dim Pair As String() = Setting.Split(":")
            If Pair.Length() < 2 Then Continue For
            If ProgramSettings.ContainsKey(Pair(0)) Then ProgramSettings.Remove(Pair(0))
            ProgramSettings.Add(Pair(0), Pair(1))
        Next

        ProgramSettingsLoaded = True
    End Sub

    Public Sub SaveProgramSettings()
        Dim ConfigString As String = ""

        For Each Setting As KeyValuePair(Of String, String) In ProgramSettings
            ConfigString = ConfigString & Setting.Key & ":" & Setting.Value & ";"
        Next

        My.Computer.FileSystem.WriteAllText(MainConfigFile, ConfigString, False)
    End Sub

    Public Function ProgramSettingsSet() As Boolean
        Return ProgramSettings.ContainsKey(ConfigOptions.AutoUpdates)
    End Function
End Module

Class SettingsHandler
    Public ConfigName As String
    Public Configuration As New Dictionary(Of String, String)
    Public LeftCheckedNodes As New Dictionary(Of String, Boolean)
    Public RightCheckedNodes As New Dictionary(Of String, Boolean)

    Private PredicateConfigMatchingList As Dictionary(Of String, String)

    Public Sub New(ByVal Name As String)
        ConfigName = Name
        LoadConfigFile()

        PredicateConfigMatchingList = New Dictionary(Of String, String)
        PredicateConfigMatchingList.Add(ConfigOptions.IncludedTypes, "(([a-zA-Z0-9]+;)*[a-zA-Z0-9])?")
        PredicateConfigMatchingList.Add(ConfigOptions.ExcludedTypes, "(([a-zA-Z0-9]+;)*[a-zA-Z0-9])?")
        PredicateConfigMatchingList.Add(ConfigOptions.LeftSubFolders, ".*")
        PredicateConfigMatchingList.Add(ConfigOptions.RightSubFolders, ".*")
        PredicateConfigMatchingList.Add(ConfigOptions.Source, ".*")
        PredicateConfigMatchingList.Add(ConfigOptions.Destination, ".*")
        PredicateConfigMatchingList.Add(ConfigOptions.Method, "[012]")
        PredicateConfigMatchingList.Add(ConfigOptions.Restrictions, "[012]")
        PredicateConfigMatchingList.Add(ConfigOptions.ReplicateEmptyDirectories, "True|False")
    End Sub

    Function LoadConfigFile() As Boolean
        If Not IO.File.Exists(ConfigOptions.GetConfigPath(ConfigName)) Then Exit Function
        Dim FileReader As New IO.StreamReader(ConfigOptions.GetConfigPath(ConfigName))

        Configuration.Clear()
        While Not FileReader.EndOfStream
            Try
                Dim ConfigLine As String = FileReader.ReadLine()
                Dim Key As String = ConfigLine.Substring(0, ConfigLine.IndexOf(":"))
                Dim Value As String = ConfigLine.Substring(ConfigLine.IndexOf(":") + 1)
                If Not Configuration.ContainsKey(Key) Then Configuration.Add(Key, Value)
            Catch ex As Exception 'TODO: Catch ex.
            End Try
        End While

        FileReader.Close()

        LoadSubFoldersList(ConfigOptions.LeftSubFolders, LeftCheckedNodes)
        LoadSubFoldersList(ConfigOptions.RightSubFolders, RightCheckedNodes)
        Return True
    End Function

    Sub LoadSubFoldersList(ByVal ConfigLine As String, ByRef Subfolders As Dictionary(Of String, Boolean))
        Subfolders.Clear()

        Dim ConfigCheckedFoldersList As New List(Of String)(Configuration(ConfigLine).Split(";"c))
        ConfigCheckedFoldersList.RemoveAt(ConfigCheckedFoldersList.Count - 1) 'Removes the last, empty element

        For Each Dir As String In ConfigCheckedFoldersList
            If Not Subfolders.ContainsKey(Dir) Then
                If Dir.EndsWith("*") Then
                    Subfolders.Add(Dir.Substring(0, Dir.Length - 1), True)
                Else
                    Subfolders.Add(Dir, False)
                End If
            End If
        Next
    End Sub

    Function SaveConfigFile() As Boolean
        Try
            Dim ConfigString As String = ""
            Dim FileWriter As New IO.StreamWriter(ConfigOptions.GetConfigPath(ConfigName))

            For Each Setting As KeyValuePair(Of String, String) In Configuration
                FileWriter.WriteLine(Setting.Key & ":" & Setting.Value)
            Next

            FileWriter.Close()
            Return True
        Catch Ex As Exception
            Return False
        End Try
    End Function

    Function ValidateConfigFile() As Boolean
        Dim IsValid As Boolean = True
        Dim InvalidListing As New List(Of String)

        If Not IO.Directory.Exists(GetSetting(ConfigOptions.Source)) Then
            InvalidListing.Add("Source directory is not valid.")
            IsValid = False
        End If

        If Not IO.Directory.Exists(GetSetting(ConfigOptions.Destination)) Then
            InvalidListing.Add("Destination directory is not valid.")
            IsValid = False
        End If

        For Each Pair As KeyValuePair(Of String, String) In PredicateConfigMatchingList
            If Not Configuration.ContainsKey(Pair.Key) Then
                IsValid = False
                InvalidListing.Add("""" & Pair.Key & """ setting is not set.")
            Else
                If Not System.Text.RegularExpressions.Regex.IsMatch(GetSetting(Pair.Key), Pair.Value) Then
                    IsValid = False
                    InvalidListing.Add("Value for """ & Pair.Key & """ setting is invalid.")
                End If
            End If
        Next
        If Not IsValid Then
            Microsoft.VisualBasic.MsgBox(ListToString(InvalidListing, Microsoft.VisualBasic.vbNewLine), Microsoft.VisualBasic.MsgBoxStyle.OkOnly + Microsoft.VisualBasic.MsgBoxStyle.Exclamation, "Invalid configuration")
        End If
        Return IsValid
    End Function

    Sub DeleteConfigFile()
        IO.File.Delete(ConfigOptions.GetConfigPath(ConfigName))
        IO.File.Delete(ConfigOptions.GetLogPath(ConfigName))
    End Sub

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

    Function ListToString(ByVal StrList As List(Of String), ByVal Separator As Char) As String
        Dim ReturnStr As String = ""
        For Each Str As String In StrList
            ReturnStr &= Str & Separator
        Next
        If ReturnStr.EndsWith(Separator) Then ReturnStr = ReturnStr.Substring(0, ReturnStr.Length - 1)
        Return ReturnStr
    End Function
End Class
