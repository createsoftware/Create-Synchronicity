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

    Public Const Scheduling As String = "Scheduling"
    Public Const SchedulingSettingsCount As Integer = 5 'Frequency;WeekDay;MonthDay;Hour;Minute

    'Main program settings
    Public Const Language As String = "Language"
    Public Const DefaultLanguage As String = "english"
    Public Const AutoUpdates As String = "Auto updates"

    Public Const ConfigFolderName As String = "config"
    Public Const LogFolderName As String = "log"
    Public Const SettingsFileName As String = "mainconfig.ini"

    Public Const RegistryBootVal As String = "Create Synchronicity - Scheduler"
    Public Const RegistryBootKey As String = "Software\Microsoft\Windows\CurrentVersion\Run"
    Public Const RegistryRootedBootKey As String = "HKEY_CURRENT_USER\" & RegistryBootKey
End Module

Public Class ConfigHandler
    Private Shared Instance As ConfigHandler

    Public ConfigRootDir As String
    Public LogRootDir As String
    Public MainConfigFile As String

    Public LanguageRootDir As String = Application.StartupPath & "\languages"

    Dim ProgramSettingsLoaded As Boolean = False
    Dim ProgramSettings As New Dictionary(Of String, String)

    Protected Sub New()
        ConfigRootDir = GetUserFilesRootDir() & ConfigFolderName
        LogRootDir = GetUserFilesRootDir() & LogFolderName
        MainConfigFile = ConfigRootDir & "\" & SettingsFileName
    End Sub

    Public Shared Function GetSingleton() As ConfigHandler
        If Instance Is Nothing Then Instance = New ConfigHandler()
        Return Instance
    End Function

    Public Function GetConfigPath(ByVal Name As String) As String
        Return ConfigRootDir & "\" & Name & ".sync"
    End Function

    Public Function GetIcon() As Drawing.Icon
        Static Icon As Drawing.Icon
        If Icon Is Nothing Then Icon = Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath)

        Return Icon
    End Function

    Public Function GetLogPath(ByVal Name As String) As String
#If DEBUG Then
        Return LogRootDir & "\" & Name & ".log"
#Else
        Return LogRootDir & "\" & Name & ".log.html"
#End If
    End Function

    Public Function GetUserFilesRootDir() As String 'Return the place were config files are stored
        Static UserFilesRootDir As String = ""
        If Not UserFilesRootDir = "" Then Return UserFilesRootDir

        Dim UserPath As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Create Software\Create Synchronicity\"

        'http://support.microsoft.com/default.aspx?scid=kb;EN-US;326549
        Dim WriteNeededFiles As New List(Of String)
        Dim WriteNeededFolders As New List(Of String)
        Dim PotentialWriteNeededFolders As String() = {Application.StartupPath & "\" & LogFolderName, Application.StartupPath & "\" & ConfigFolderName}

        WriteNeededFolders.Add(Application.StartupPath)
        For Each Folder As String In PotentialWriteNeededFolders
            If IO.Directory.Exists(Folder) Then
                WriteNeededFolders.Add(Folder)
                WriteNeededFiles.AddRange(IO.Directory.GetFiles(Folder))
            End If
        Next

        Dim Writable As Boolean = True
        Dim ProgramPathExists As Boolean = IO.Directory.Exists(Application.StartupPath & "\" & ConfigFolderName)

        For Each Folder As String In WriteNeededFolders
            Dim FolderInfo As New IO.DirectoryInfo(Folder)
            Writable = Writable And (Not (FolderInfo.Attributes And IO.FileAttributes.ReadOnly) = IO.FileAttributes.ReadOnly)
        Next
        For Each File As String In WriteNeededFiles
            Writable = Writable And (Not (IO.File.GetAttributes(File) And IO.FileAttributes.ReadOnly) = IO.FileAttributes.ReadOnly)
        Next

        If Writable And (ProgramPathExists Or Not IO.Directory.Exists(UserPath)) Then
            UserFilesRootDir = Application.StartupPath & "\"
        Else
            If ProgramPathExists Then Interaction.ShowMsg("Create Synchronicity cannot write to your installation directory, although it contains configuration files. Your Application Data folder will therefore be used instead.", "Information", , MessageBoxIcon.Information)
            UserFilesRootDir = UserPath
        End If

        Return UserFilesRootDir
    End Function

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

        IO.Directory.CreateDirectory(ConfigRootDir)
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
End Class

Class ProfileHandler
    Public ProfileName As String
    Public Scheduler As New ScheduleInfo()
    Public Configuration As New Dictionary(Of String, String)
    Public LeftCheckedNodes As New Dictionary(Of String, Boolean)
    Public RightCheckedNodes As New Dictionary(Of String, Boolean)

    Private PredicateConfigMatchingList As Dictionary(Of String, String)

    Dim Translation As LanguageHandler = LanguageHandler.GetSingleton
    Dim ProgramConfig As ConfigHandler = ConfigHandler.GetSingleton

    Public Sub New(ByVal Name As String)
        ProfileName = Name
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
        'TODO: add regexps for new settings.
    End Sub

    Function LoadConfigFile() As Boolean
        If Not IO.File.Exists(ProgramConfig.GetConfigPath(ProfileName)) Then Return False
        Dim FileReader As New IO.StreamReader(ProgramConfig.GetConfigPath(ProfileName))

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

        LoadScheduler()
        LoadSubFoldersList(ConfigOptions.LeftSubFolders, LeftCheckedNodes)
        LoadSubFoldersList(ConfigOptions.RightSubFolders, RightCheckedNodes)
        Return True
    End Function

    Function SaveConfigFile() As Boolean
        Try
            Dim ConfigString As String = ""
            Dim FileWriter As New IO.StreamWriter(ProgramConfig.GetConfigPath(ProfileName))

            For Each Setting As KeyValuePair(Of String, String) In Configuration
                FileWriter.WriteLine(Setting.Key & ":" & Setting.Value)
            Next

            FileWriter.Close()
            Return True
        Catch Ex As Exception
            Return False
        End Try
    End Function

    Function ValidateConfigFile(Optional ByVal WarnUnrootedPaths As Boolean = False) As Boolean
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
                InvalidListing.Add(String.Format(Translation.Translate("\SETTING_UNSET"), Pair.Key))
            Else
                If Not System.Text.RegularExpressions.Regex.IsMatch(GetSetting(Pair.Key), Pair.Value) Then
                    IsValid = False
                    InvalidListing.Add(String.Format(Translation.Translate("\INVALID_SETTING"), Pair.Key))
                End If
            End If
        Next


        If Not IsValid Then
            Interaction.ShowMsg(ListToString(InvalidListing, Microsoft.VisualBasic.vbNewLine.ToCharArray()(0)), Translation.Translate("\INVALID_CONFIG"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        Else
            If WarnUnrootedPaths Then
                If Not IO.Path.IsPathRooted(GetSetting(ConfigOptions.Source)) Then
                    If Interaction.ShowMsg(Translation.Translate("\LEFT_UNROOTED"), , MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Return False
                End If

                If Not IO.Path.IsPathRooted(GetSetting(ConfigOptions.Source)) Then
                    If Interaction.ShowMsg(Translation.Translate("\RIGHT_UNROOTED"), , MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Return False
                End If
            End If

            Return True
        End If
    End Function

    Function RenameProfile(ByVal NewName As String) As Boolean
        If IO.File.Exists(ProgramConfig.GetLogPath(NewName)) Or IO.File.Exists(ProgramConfig.GetConfigPath(NewName)) Then Return False

        Try
            IO.File.Move(ProgramConfig.GetLogPath(ProfileName), ProgramConfig.GetLogPath(NewName))
            IO.File.Move(ProgramConfig.GetConfigPath(ProfileName), ProgramConfig.GetConfigPath(NewName))
        Catch
            Return False
        End Try
        Return True
    End Function

    Sub DeleteConfigFile()
        IO.File.Delete(ProgramConfig.GetConfigPath(ProfileName))
        DeleteLogFile()
    End Sub

    Sub DeleteLogFile()
        IO.File.Delete(ProgramConfig.GetLogPath(ProfileName))
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

    Sub LoadScheduler()
        Dim Opts() As String = GetSetting(ConfigOptions.Scheduling, "").Split(";")

        Select Case Opts.GetLength(0)
            Case 0
                Scheduler = New ScheduleInfo(ScheduleInfo.NEVER)
            Case ConfigOptions.SchedulingSettingsCount
                Scheduler = New ScheduleInfo(Opts(0), Opts(1), Opts(2), Opts(3), Opts(4))
            Case Else
                Scheduler = New ScheduleInfo(ScheduleInfo.NEVER) 'TODO: Invalid string
        End Select
    End Sub

    Sub SaveScheduler()
        SetSetting(ConfigOptions.Scheduling, Scheduler.Frequency & ";" & Scheduler.WeekDay & ";" & Scheduler.MonthDay & ";" & Scheduler.Hour & ";" & Scheduler.Minute)
    End Sub

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

    Private Shared Function ListToString(ByVal StrList As List(Of String), ByVal Separator As Char) As String
        Dim ReturnStr As String = ""
        For Each Str As String In StrList
            ReturnStr &= Str & Separator
        Next
        If ReturnStr.EndsWith(Separator) Then ReturnStr = ReturnStr.Substring(0, ReturnStr.Length - 1)
        Return ReturnStr
    End Function
End Class

Structure ScheduleInfo
    Public Frequency As String

    Public WeekDay As String 'Sunday = 0
    Public MonthDay As Integer
    Public Hour, Minute As Integer

    Public Const NEVER = "never"
    Public Const DAILY = "daily"
    Public Const WEEKLY = "weekly"
    Public Const MONTHLY = "monthly"

    Sub New(ByVal _Frequency As String)
        Frequency = _Frequency
    End Sub

    Sub New(ByVal _Frequency As String, ByVal _WeekDay As String, ByVal _MonthDay As Integer, ByVal _Hour As Integer, ByVal _Minute As Integer)
        Frequency = _Frequency

        Hour = _Hour
        Minute = _Minute
        WeekDay = _WeekDay
        MonthDay = _MonthDay
    End Sub

    Function NextRun() As Date
        Dim Now As Date = Date.Now
        Dim Today As Date = Date.Today

        Dim RunAt As Date
        Dim Interval As TimeSpan

        Select Case Frequency
            Case DAILY
                Interval = New TimeSpan(1, 0, 0, 0)
                RunAt = Today.AddHours(Hour).AddMinutes(Minute)
            Case WEEKLY
                Interval = New TimeSpan(7, 0, 0, 0)
                RunAt = Today.AddDays(WeekDay - Today.DayOfWeek).AddHours(Hour).AddMinutes(Minute)
            Case MONTHLY
                Interval = Today.AddMonths(1) - Today
                RunAt = Today.AddDays(MonthDay - Today.Day).AddHours(Hour).AddMinutes(Minute) 'Cint works, because Interval represents a whole number of days
            Case Else
                Return Date.Now.AddYears(-1)
        End Select

        While Now > RunAt : RunAt += Interval : End While 'Loop needed (eg when today = jan 1 and schedule = every 1st month day
        Return RunAt
    End Function
End Structure

Public Module Updates
    Dim Translation As LanguageHandler = LanguageHandler.GetSingleton
    Dim ProgramConfig As ConfigHandler = ConfigHandler.GetSingleton

    Public Sub CheckForUpdates(ByVal RoutineCheck As Boolean)
        Try
            Dim CurrentVersion As String = (New System.Net.WebClient).DownloadString("http://synchronicity.sourceforge.net/code/version.txt")
            If CurrentVersion = "" Then Throw New Exception()
            If (CurrentVersion <> Application.ProductVersion) Then
                If Interaction.ShowMsg(String.Format(Translation.Translate("\UPDATE_MSG"), Application.ProductVersion, CurrentVersion), Translation.Translate("\UPDATE_TITLE"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Diagnostics.Process.Start("http://synchronicity.sourceforge.net/downloads.html")
                End If
            Else
                If Not RoutineCheck Then Interaction.ShowMsg(Translation.Translate("\NO_UPDATES"), , , MessageBoxIcon.Information)
            End If
        Catch Ex As Exception
            Interaction.ShowMsg(Translation.Translate("\UPDATE_ERROR"), Translation.Translate("\UPDATE_ERROR_TITLE"), , MessageBoxIcon.Error)
        End Try
    End Sub
End Module

Public Module Interaction
    Public AsAService As Boolean = False

    Public Function ShowMsg(ByVal Text As String, Optional ByVal Caption As String = "", Optional ByVal Buttons As MessageBoxButtons = MessageBoxButtons.OK, Optional ByVal Icon As MessageBoxIcon = MessageBoxIcon.None) As DialogResult
        If AsAService Then
            Return MessageBox.Show(Text, Caption, Buttons, Icon, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification)
        Else
            Return MessageBox.Show(Text, Caption, Buttons, Icon)
        End If
    End Function
End Module

#If 0 Then
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

        Interaction.ShowMsg("Output: " & Microsoft.VisualBasic.vbNewLine & Output & Microsoft.VisualBasic.vbNewLine & ErrorOutput)
    End Sub
End Class
#End If