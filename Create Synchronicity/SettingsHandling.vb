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
    Public Const StrictDateComparison As String = "Strict date comparison"
    Public Const PropagateUpdates As String = "Propagate Updates"
    Public Const StrictMirror As String = "Strict mirror"
    Public Const TimeOffset As String = "Time Offset"

    'Main program settings
    Public Const Language As String = "Language"
    Public Const DefaultLanguage As String = "english"
    Public Const AutoUpdates As String = "Auto updates"

    Public ConfigRootDir As String = Application.StartupPath & "\config"
    Public LogRootDir As String = Application.StartupPath & "\log"
    Public LanguageRootDir As String = Application.StartupPath & "\languages"
    Public MainConfigFile As String = ConfigRootDir & "\mainconfig.ini"

    Dim ProgramSettingsLoaded As Boolean = False
    Dim ProgramSettings As New Dictionary(Of String, String)

    Dim Translation As LanguageHandler = LanguageHandler.GetSingleton

    Public Function GetConfigPath(ByVal Name As String) As String
        Return ConfigRootDir & "\" & Name & ".sync"
    End Function

    Public Function GetIcon()
        Static Icon As Drawing.Icon

        If Icon Is Nothing Then
            Icon = Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath)
        End If

        Return Icon
    End Function

    Public Function GetLogPath(ByVal Name As String) As String
#If DEBUG Then
        Return LogRootDir & "\" & Name & ".log"
#Else
        Return LogRootDir & "\" & Name & ".log.html"
#End If
    End Function

    Public Sub CheckForUpdates(ByVal RoutineCheck As Boolean)
        Try
            Dim CurrentVersion As String = (New System.Net.WebClient).DownloadString("http://synchronicity.sourceforge.net/code/version.txt")
            If CurrentVersion = "" Then Throw New Exception()
            If (CurrentVersion <> Application.ProductVersion) Then
                If Microsoft.VisualBasic.MsgBox(String.Format(Translation.Translate("\UPDATE_MSG"), Application.ProductVersion, CurrentVersion), Microsoft.VisualBasic.MsgBoxStyle.Question Or Microsoft.VisualBasic.MsgBoxStyle.YesNo, Translation.Translate("\UPDATE_TITLE")) = Microsoft.VisualBasic.MsgBoxResult.Yes Then
                    Diagnostics.Process.Start("http://synchronicity.sourceforge.net/downloads.html")
                End If
            Else
                If Not RoutineCheck Then Microsoft.VisualBasic.MsgBox(Translation.Translate("\NO_UPDATES"), Microsoft.VisualBasic.MsgBoxStyle.OkOnly Or Microsoft.VisualBasic.MsgBoxStyle.Information)
            End If
        Catch Ex As Exception
            Microsoft.VisualBasic.MsgBox(Translation.Translate("\UPDATE_ERROR"), Microsoft.VisualBasic.MsgBoxStyle.OkOnly Or Microsoft.VisualBasic.MsgBoxStyle.Exclamation, Translation.Translate("\UPDATE_ERROR_TITLE"))
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

        IO.Directory.CreateDirectory(ConfigOptions.ConfigRootDir)
        If Not IO.File.Exists(MainConfigFile) Then
            IO.File.Create(MainConfigFile).Close()
            Exit Sub
        End If

        Dim ConfigString As String = My.Computer.FileSystem.ReadAllText(MainConfigFile)
        Dim ConfigArray As New List(Of String)(ConfigString.Split(";"c))
        For Each Setting As String In ConfigArray
            Dim Pair As String() = Setting.Split(":"c)
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

    Public Function ProgramSettingsSet(ByVal Setting As String) As Boolean
        Return ProgramSettings.ContainsKey(Setting)
    End Function
End Module

Class SettingsHandler
    Public ConfigName As String
    Public Configuration As New Dictionary(Of String, String)
    Public LeftCheckedNodes As New Dictionary(Of String, Boolean)
    Public RightCheckedNodes As New Dictionary(Of String, Boolean)

    Private PredicateConfigMatchingList As Dictionary(Of String, String)

    Dim Translation As LanguageHandler = LanguageHandler.GetSingleton

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
            InvalidListing.Add(Translation.Translate("\INVALID_SOURCE"))
            IsValid = False
        End If

        If Not IO.Directory.Exists(GetSetting(ConfigOptions.Destination)) Then
            InvalidListing.Add(Translation.Translate("\INVALID_DEST"))
            IsValid = False
        End If

        For Each Pair As KeyValuePair(Of String, String) In PredicateConfigMatchingList
            If Not Configuration.ContainsKey(Pair.Key) Then
                IsValid = False
                InvalidListing.Add(String.Format(Translation.Translate("SETTING_UNSET"), Pair.Key))
            Else
                If Not System.Text.RegularExpressions.Regex.IsMatch(GetSetting(Pair.Key), Pair.Value) Then
                    IsValid = False
                    InvalidListing.Add(String.Format(Translation.Translate("INVALID_SETTING"), Pair.Key))
                End If
            End If
        Next
        If Not IsValid Then
            Microsoft.VisualBasic.MsgBox(ListToString(InvalidListing, Microsoft.VisualBasic.vbNewLine.ToCharArray()(0)), Microsoft.VisualBasic.MsgBoxStyle.OkOnly Or Microsoft.VisualBasic.MsgBoxStyle.Exclamation, "Invalid configuration")
        End If
        Return IsValid
    End Function

    Sub DeleteConfigFile()
        IO.File.Delete(ConfigOptions.GetConfigPath(ConfigName))
        IO.File.Delete(ConfigOptions.GetLogPath(ConfigName))
    End Sub

    Sub SetSetting(ByVal SettingName As String, ByVal Value As String)
        Configuration(SettingName) = Value
    End Sub

    Sub SetSetting(ByVal SettingName As String, ByRef SettingField As String, ByVal LoadSetting As Boolean)
        If LoadSetting Then
            SettingField = GetSetting(SettingName, SettingField)
        Else
            Configuration(SettingName) = SettingField
        End If
    End Sub

    Function GetSetting(ByVal SettingName As String, Optional ByRef DefaultVal As String = Nothing) As String
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

Class Scheduler 'schtasks.exe
    Enum TaskFrequency '/SC
        DAILY '/D *
        WEEKLY '/D _TaskDayOfWeek_
        MONTHLY '/D _1-31_
    End Enum

    Enum TaskDayOfWeek
        MON
        TUE
        WED
        THU
        FRI
        SAT
        SUN
    End Enum

    Public Name As String '/TN
    Public Frequency As TaskFrequency

    Public WeekDay As TaskDayOfWeek
    Public MonthDay As Integer
    Public Hour, Minute As Integer '/ST

    '/V1 for compatibility
    '/F to force task creation

    Sub New(ByVal _Name As String, ByVal _Hour As Integer, ByVal _Minute As Integer)
        Frequency = TaskFrequency.DAILY

        Name = _Name
        Hour = _Hour
        Minute = _Minute
    End Sub

    Sub New(ByVal _Name As String, ByVal _WeekDay As TaskDayOfWeek, ByVal _Hour As Integer, ByVal _Minute As Integer)
        Frequency = TaskFrequency.WEEKLY

        Name = _Name
        WeekDay = _WeekDay

        Hour = _Hour
        Minute = _Minute
    End Sub

    Sub New(ByVal _Name As String, ByVal _MonthDay As Integer, ByVal _Hour As Integer, ByVal _Minute As Integer)
        Frequency = TaskFrequency.MONTHLY

        Name = _Name
        MonthDay = _MonthDay

        Hour = _Hour
        Minute = _Minute
    End Sub

    Sub RegisterTask()
        Dim Arguments As String = ""
        Arguments &= " /Create "
        Arguments &= " /TN " & "Create_Synchronicity_" & Name
        Arguments &= " /SC " & Frequency.ToString
        Select Case Frequency
            Case TaskFrequency.WEEKLY
                Arguments &= " /D " & WeekDay.ToString

            Case TaskFrequency.MONTHLY
                Arguments &= " /D " & MonthDay
        End Select
        Arguments &= " /ST " & """" & Hour.ToString.PadLeft(2, "0") & ":" & Minute.ToString.PadLeft(2, "0") & """"
        Arguments &= " /TR " & """" & "'" & Application.ExecutablePath & "'" & " /quiet /run " & Name & """"

        Arguments &= " /RU SYSTEM /V1 /F"

        Dim StartInfo As New Diagnostics.ProcessStartInfo("schtasks.exe", Arguments)
        StartInfo.UseShellExecute = False
        StartInfo.CreateNoWindow = True
        StartInfo.RedirectStandardOutput = True
        StartInfo.RedirectStandardError = True
        StartInfo.StandardOutputEncoding = Text.Encoding.GetEncoding(850)
        StartInfo.StandardErrorEncoding = Text.Encoding.GetEncoding(850)

        ' Make the process and set its start information.
        Dim RegProcess As New Diagnostics.Process()
        RegProcess.StartInfo = StartInfo
        RegProcess.Start()

        Dim Output As String = RegProcess.StandardOutput.ReadToEnd
        Dim ErrorOutput As String = RegProcess.StandardError.ReadToEnd

        RegProcess.StandardOutput.Close()
        RegProcess.StandardError.Close()

        RegProcess.Close()

        MessageBox.Show("Output: " & Environment.NewLine & Output & Environment.NewLine & ErrorOutput)
    End Sub
End Class
