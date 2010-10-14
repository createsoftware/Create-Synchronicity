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
    Public Const FuzzyDstCompensation As String = "Fuzzy DST compensation" 'TODO
    Public Const PropagateUpdates As String = "Propagate Updates"
    Public Const StrictMirror As String = "Strict mirror"
    Public Const TimeOffset As String = "Time Offset"
    Public Const LastRun As String = "Last run"
    Public Const CatchUpSync As String = "Catch up if missed" 'TODO
    Public Const Group As String = "Group" 'TODO

    Public Const Scheduling As String = "Scheduling"
    Public Const SchedulingSettingsCount As Integer = 5 'Frequency;WeekDay;MonthDay;Hour;Minute

    'Main program settings
    Public Const Language As String = "Language"
    Public Const DefaultLanguage As String = "english"
    Public Const AutoUpdates As String = "Auto updates"

    Public Const ConfigFolderName As String = "config"
    Public Const LogFolderName As String = "log"
    Public Const SettingsFileName As String = "mainconfig.ini"
    Public Const AppLogName As String = "app.log"

    Public Const EnqueuingSeparator As Char = "|"c
#If LINUX Then
    Public Const DirSep As Char = "/"c
#Else
    Public Const DirSep As Char = "\"c
#End If

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
    Public CanGoOn As Boolean = True 'To check whether a synchronization is already running (in scheduler mode, or when queuing multiple profiles).

#If DEBUG Then
    Const Debug As Boolean = True
#Else
    Const Debug As Boolean = False
#End If
    Dim ProgramSettingsLoaded As Boolean = False
    Dim ProgramSettings As New Dictionary(Of String, String)

    Protected Sub New()
        ConfigRootDir = GetUserFilesRootDir() & ConfigOptions.ConfigFolderName
        LogRootDir = GetUserFilesRootDir() & ConfigOptions.LogFolderName
        MainConfigFile = ConfigRootDir & ConfigOptions.DirSep & ConfigOptions.SettingsFileName
    End Sub

    Public Shared Function GetSingleton() As ConfigHandler
        If Instance Is Nothing Then Instance = New ConfigHandler()
        Return Instance
    End Function

    Public Function GetConfigPath(ByVal Name As String) As String
        Return ConfigRootDir & ConfigOptions.DirSep & Name & ".sync"
    End Function

    Public Function GetIcon() As Drawing.Icon
        Static Icon As Drawing.Icon
        If Icon Is Nothing Then
            Try
                Icon = Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath)
            Catch
                Icon = Drawing.Icon.FromHandle((New Drawing.Bitmap(32, 32)).GetHicon)
            End Try
        End If

        Return Icon
    End Function

    Public Function GetLogPath(ByVal Name As String) As String
#If DEBUG Then
        Return LogRootDir & ConfigOptions.DirSep & Name & ".log"
#Else
        Return LogRootDir & ConfigOptions.DirSep & Name & ".log.html"
#End If
    End Function

    Public Function GetUserFilesRootDir() As String 'Return the place were config files are stored
        Static UserFilesRootDir As String = ""
        If Not UserFilesRootDir = "" Then Return UserFilesRootDir

        Dim UserPath As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & ConfigOptions.DirSep & "Create Software" & ConfigOptions.DirSep & "Create Synchronicity" & ConfigOptions.DirSep

        'http://support.microsoft.com/default.aspx?scid=kb;EN-US;326549
        Dim WriteNeededFiles As New List(Of String)
        Dim WriteNeededFolders As New List(Of String)
        Dim PotentialWriteNeededFolders As String() = {Application.StartupPath & ConfigOptions.DirSep & LogFolderName, Application.StartupPath & ConfigOptions.DirSep & ConfigFolderName}

        WriteNeededFolders.Add(Application.StartupPath)
        For Each Folder As String In PotentialWriteNeededFolders
            If IO.Directory.Exists(Folder) Then
                WriteNeededFolders.Add(Folder)
                WriteNeededFiles.AddRange(IO.Directory.GetFiles(Folder))
            End If
        Next

        Dim Writable As Boolean = True
        Dim ProgramPathExists As Boolean = IO.Directory.Exists(Application.StartupPath & ConfigOptions.DirSep & ConfigFolderName)

        For Each Folder As String In WriteNeededFolders
            Dim FolderInfo As New IO.DirectoryInfo(Folder)
            Writable = Writable And (Not (FolderInfo.Attributes And IO.FileAttributes.ReadOnly) = IO.FileAttributes.ReadOnly)

            Try
                IO.File.Create(Folder & ConfigOptions.DirSep & "write-permissions").Close()
                IO.File.Delete(Folder & ConfigOptions.DirSep & "write-permissions")
            Catch
                Writable = False
            End Try
        Next
        For Each File As String In WriteNeededFiles
            Writable = Writable And (Not (IO.File.GetAttributes(File) And IO.FileAttributes.ReadOnly) = IO.FileAttributes.ReadOnly)
        Next

        ' When a user folder exists, and no config folder exists in the install dir, use the user's folder.
        If Writable And (ProgramPathExists Or Not IO.Directory.Exists(UserPath)) Then
            UserFilesRootDir = Application.StartupPath & ConfigOptions.DirSep
        Else
            'Not translated, since it happens before loading translation files
            If ProgramPathExists Then Interaction.ShowMsg("Create Synchronicity cannot write to your installation directory, although it contains configuration files. Your Application Data folder will therefore be used instead.", "Information", , MessageBoxIcon.Information)
            UserFilesRootDir = UserPath
        End If

        Return UserFilesRootDir
    End Function

    Public Function GetProgramSetting(ByVal Key As String, ByVal DefaultVal As String) As String
        Return If(ProgramSettings.ContainsKey(Key), ProgramSettings(Key), DefaultVal)
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

    Public Shared Sub LogAppEvent(ByVal EventData As String)
        If Debug Or CommandLine.Silent Or CommandLine.Log Then
            Dim Instance As ConfigHandler = ConfigHandler.GetSingleton()
            Dim AppLog As New IO.StreamWriter(Instance.GetUserFilesRootDir() & ConfigOptions.AppLogName, True)
            AppLog.WriteLine(String.Format("[{0}] {1}", Date.Now.ToString(), EventData))
            AppLog.Close()
        End If
    End Sub

    Public Shared Sub RegisterBoot()
        If My.Computer.Registry.GetValue(ConfigOptions.RegistryRootedBootKey, ConfigOptions.RegistryBootVal, Nothing) Is Nothing Then
            ConfigHandler.LogAppEvent("Registering program in startup list")
            My.Computer.Registry.SetValue(ConfigOptions.RegistryRootedBootKey, ConfigOptions.RegistryBootVal, Application.ExecutablePath & " /scheduler")
        End If
    End Sub
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
        PredicateConfigMatchingList.Add(ConfigOptions.Source, ".*")
        PredicateConfigMatchingList.Add(ConfigOptions.Destination, ".*")
        PredicateConfigMatchingList.Add(ConfigOptions.ExcludedTypes, "(([a-zA-Z0-9]+;)*[a-zA-Z0-9])?")
        PredicateConfigMatchingList.Add(ConfigOptions.IncludedTypes, "(([a-zA-Z0-9]+;)*[a-zA-Z0-9])?")
        PredicateConfigMatchingList.Add(ConfigOptions.LeftSubFolders, ".*")
        PredicateConfigMatchingList.Add(ConfigOptions.RightSubFolders, ".*")
        PredicateConfigMatchingList.Add(ConfigOptions.Method, "[012]")
        PredicateConfigMatchingList.Add(ConfigOptions.Restrictions, "[012]")
        PredicateConfigMatchingList.Add(ConfigOptions.ReplicateEmptyDirectories, "True|False")
        'NOTE: Only vital settings should be checked for correctness, since the config will be rejected if a mismatch occurs.
    End Sub

    Function LoadConfigFile() As Boolean
        If Not IO.File.Exists(ProgramConfig.GetConfigPath(ProfileName)) Then Return False
        Dim FileReader As New IO.StreamReader(ProgramConfig.GetConfigPath(ProfileName))

        Configuration.Clear()
        While Not FileReader.EndOfStream
            Dim ConfigLine As String = ""
            Try
                ConfigLine = FileReader.ReadLine()
                Dim Key As String = ConfigLine.Substring(0, ConfigLine.IndexOf(":"))
                Dim Value As String = ConfigLine.Substring(ConfigLine.IndexOf(":") + 1)
                If Not Configuration.ContainsKey(Key) Then Configuration.Add(Key, Value)
            Catch ex As Exception
                Interaction.ShowMsg(String.Format(Translation.Translate("\INVALID_SETTING"), ConfigLine))
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

        If Not IO.Directory.Exists(TranslatePath(GetSetting(ConfigOptions.Source))) Then
            InvalidListing.Add(Translation.Translate("\INVALID_SOURCE"))
            IsValid = False
        End If

        If Not IO.Directory.Exists(TranslatePath(GetSetting(ConfigOptions.Destination))) Then
            InvalidListing.Add(Translation.Translate("\INVALID_DEST"))
            IsValid = False
        End If

        For Each Pair As KeyValuePair(Of String, String) In PredicateConfigMatchingList
            If Not Configuration.ContainsKey(Pair.Key) Then
                IsValid = False
                InvalidListing.Add(String.Format(Translation.Translate("\SETTING_UNSET"), Pair.Key))
            ElseIf Not System.Text.RegularExpressions.Regex.IsMatch(GetSetting(Pair.Key), Pair.Value) Then
                IsValid = False
                InvalidListing.Add(String.Format(Translation.Translate("\INVALID_SETTING"), Pair.Key))
            End If
        Next

        If Not IsValid Then
            Interaction.ShowMsg(ListToString(InvalidListing, Microsoft.VisualBasic.vbNewLine.ToCharArray()(0)), Translation.Translate("\INVALID_CONFIG"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        Else
            If WarnUnrootedPaths Then
                If Not IO.Path.IsPathRooted(TranslatePath(GetSetting(ConfigOptions.Source))) Then
                    If Interaction.ShowMsg(Translation.Translate("\LEFT_UNROOTED"), , MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Return False
                End If

                If Not IO.Path.IsPathRooted(TranslatePath(GetSetting(ConfigOptions.Destination))) Then
                    If Interaction.ShowMsg(Translation.Translate("\RIGHT_UNROOTED"), , MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Return False
                End If
            End If

            Return True
        End If
    End Function

    Function RenameProfile(ByVal NewName As String) As Boolean
        If IO.File.Exists(ProgramConfig.GetLogPath(NewName)) Or IO.File.Exists(ProgramConfig.GetConfigPath(NewName)) Then Return False

        Try
            If IO.File.Exists(ProgramConfig.GetLogPath(ProfileName)) Then IO.File.Move(ProgramConfig.GetLogPath(ProfileName), ProgramConfig.GetLogPath(NewName))
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
        'Passes the current value as default answer.
        If LoadSetting Then
            SettingField = GetSetting(SettingName, SettingField)
        Else
            Configuration(SettingName) = SettingField
        End If
    End Sub

    Function GetSetting(ByVal SettingName As String, Optional ByRef DefaultVal As String = Nothing) As String
        Return If(Configuration.ContainsKey(SettingName), Configuration(SettingName), DefaultVal)
    End Function

    Sub LoadScheduler()
        Dim Opts() As String = GetSetting(ConfigOptions.Scheduling, "").Split(";"c)

        Select Case Opts.GetLength(0)
            Case 0
                Scheduler = New ScheduleInfo(ScheduleInfo.NEVER)
            Case ConfigOptions.SchedulingSettingsCount
                Scheduler = New ScheduleInfo(Opts(0), Opts(1), Opts(2), Opts(3), Opts(4))
            Case Else
                Scheduler = New ScheduleInfo(ScheduleInfo.NEVER) 'NOTE: Wrong strings default to never
        End Select
    End Sub

    Sub SaveScheduler()
        SetSetting(ConfigOptions.Scheduling, Scheduler.Frequency & ";" & Scheduler.WeekDay & ";" & Scheduler.MonthDay & ";" & Scheduler.Hour & ";" & Scheduler.Minute)
    End Sub

    Sub LoadSubFoldersList(ByVal ConfigLine As String, ByRef Subfolders As Dictionary(Of String, Boolean))
        Subfolders.Clear()
        Dim ConfigCheckedFoldersList As New List(Of String)(If(Configuration.ContainsKey(ConfigLine), Configuration(ConfigLine), "").Split(";"c))
        ConfigCheckedFoldersList.RemoveAt(ConfigCheckedFoldersList.Count - 1) 'Removes the last, empty element 'WARNING: All lists should end with a comma then.

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

    Public Shared Function TranslatePath(ByVal Path As String) As String
        Return TranslatePath_Unsafe(Path).TrimEnd(ConfigOptions.DirSep) 'Careful with Linux root
        'This part is just for the extra safety, since a fix is also included in TranslatePath_Unsafe.
        'Prevents a very annoying bug, where the presence of a slash at the end of the base directory would confuse the engine (#3052979)
    End Function

    Public Shared Function TranslatePath_Inverse(ByVal Path As String) As String
#If Not LINUX Then
        If Text.RegularExpressions.Regex.IsMatch(Path, "^(?<driveletter>[A-Z]\:)(\\(?<relativepath>.*))?$") Then
            Dim Label As String = ""
            For Each Drive As IO.DriveInfo In IO.DriveInfo.GetDrives
                If Drive.Name(0) = Path(0) Then Label = Drive.VolumeLabel
            Next
            If Label <> "" Then Return String.Format("""{0}""\{1}", Label, Path.Substring(2).Trim(ConfigOptions.DirSep)).TrimEnd(ConfigOptions.DirSep)
        End If
#End If

        Return Path
    End Function

    Private Shared Function TranslatePath_Unsafe(ByVal Path As String) As String
        Dim Translated_Path As String = Path

#If Not LINUX Then
        Dim Label As String, RelativePath As String
        If Path.StartsWith("""") Or Path.StartsWith(":") Then
            Dim ClosingPos As Integer = Path.LastIndexOfAny(New Char() {""""c, ":"c})
            If ClosingPos = 0 Then Return "" 'TODO: Currently returns "" if no closing op is found.

            Label = Path.Substring(1, ClosingPos - 1)
            RelativePath = Path.Substring(ClosingPos + 1)

            If Path.StartsWith("""") And Not Label = "" Then
                For Each Drive As IO.DriveInfo In IO.DriveInfo.GetDrives
                    'The drive's name ends with a "\". If RelativePath = "", then TrimEnd on the RelativePath won't do anything.
                    'This is the line why this function is called unsafe, but it's been made safe anyway: dirty code that get fixed later on crosses me. The point is that a source/destination path should *never* end with a DirSep, otherwise the system gets confused as to what is a relative path and what is the base path.
                    If Not Drive.Name(0) = "A"c AndAlso Drive.IsReady AndAlso String.Compare(Drive.VolumeLabel, Label, True) = 0 Then
                        Translated_Path = (Drive.Name & RelativePath.TrimStart(ConfigOptions.DirSep)).TrimEnd(ConfigOptions.DirSep) 'Bug #3052979
                        Exit For
                    End If
                Next
            End If
        End If
#End If

        Return Environment.ExpandEnvironmentVariables(Translated_Path)
    End Function

    Public Function GetLastRun() As Date
        Try
            Return CDate(GetSetting(ConfigOptions.LastRun, ScheduleInfo.DATE_NEVER.ToString))
        Catch
            Return ScheduleInfo.DATE_NEVER
        End Try
    End Function

    Public Sub SetLastRun()
        SetSetting(ConfigOptions.LastRun, Date.Now.ToString)
        SaveConfigFile()
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

    Public WeekDay As Integer 'Sunday = 0
    Public MonthDay As Integer
    Public Hour, Minute As Integer

    Public Const NEVER = "never"
    Public Const DAILY = "daily"
    Public Const WEEKLY = "weekly"
    Public Const MONTHLY = "monthly"

    Public Shared ReadOnly DATE_NEVER As Date = Date.MaxValue
    Public Shared ReadOnly DATE_CATCHUP As Date = Date.MinValue

    Sub New(ByVal _Frequency As String)
        Frequency = _Frequency
    End Sub

    Sub New(ByVal _Frequency As String, ByVal _WeekDay As Integer, ByVal _MonthDay As Integer, ByVal _Hour As Integer, ByVal _Minute As Integer)
        Frequency = _Frequency

        Hour = _Hour
        Minute = _Minute
        WeekDay = _WeekDay
        MonthDay = _MonthDay
    End Sub

    Function GetInterval(Optional ByVal Multiplicator As Integer = 1) As TimeSpan
        Dim Interval As TimeSpan
        Dim Today As Date = Date.Today

        Select Case Frequency
            Case DAILY
                Interval = New TimeSpan(Multiplicator * 1, 0, 0, 0)
            Case WEEKLY
                Interval = New TimeSpan(Multiplicator * 7, 0, 0, 0)
            Case MONTHLY
                Interval = Today.AddMonths(Multiplicator * 1) - Today
            Case Else
                Interval = New TimeSpan(0)
        End Select

        Return Interval
    End Function

    Function NextRun() As Date
        Dim Now As Date = Date.Now
        Dim Today As Date = Date.Today

        Dim RunAt As Date
        Dim Interval As TimeSpan = GetInterval()

        Select Case Frequency
            Case DAILY
                RunAt = Today.AddHours(Hour).AddMinutes(Minute)
            Case WEEKLY
                RunAt = Today.AddDays(WeekDay - Today.DayOfWeek).AddHours(Hour).AddMinutes(Minute)
            Case MONTHLY
                RunAt = Today.AddDays(MonthDay - Today.Day).AddHours(Hour).AddMinutes(Minute)
            Case Else
                Return DATE_NEVER
        End Select

        '">=" prevents double-syncing. Using ">" could cause the scheduler to queue Date.Now as next run time.
        While Now >= RunAt : RunAt += Interval : End While 'Loop needed (eg when today = jan 1 and schedule = every 1st month day)
        Return RunAt
    End Function
End Structure

Public Module Updates
    Dim Translation As LanguageHandler = LanguageHandler.GetSingleton
    Dim ProgramConfig As ConfigHandler = ConfigHandler.GetSingleton

    Public Sub CheckForUpdates(ByVal RoutineCheck As Boolean)
        Try
            Dim UpdateClient As New Net.WebClient
            UpdateClient.UseDefaultCredentials = True 'Needed? -- Does no harm
            UpdateClient.Proxy = System.Net.HttpWebRequest.DefaultWebProxy 'Tracker #2976549
            UpdateClient.Proxy.Credentials = Net.CredentialCache.DefaultCredentials
            Dim CurrentVersion As String = UpdateClient.DownloadString("http://synchronicity.sourceforge.net/code/version.txt")

            If CurrentVersion = "" Then Throw New Exception()
            If (CurrentVersion <> Application.ProductVersion) Then
                If Interaction.ShowMsg(String.Format(Translation.Translate("\UPDATE_MSG"), Application.ProductVersion, CurrentVersion), Translation.Translate("\UPDATE_TITLE"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Diagnostics.Process.Start("http://synchronicity.sourceforge.net/downloads.html")
                    Application.Exit()
                End If
            Else
                If Not RoutineCheck Then Interaction.ShowMsg(Translation.Translate("\NO_UPDATES"), , , MessageBoxIcon.Information)
            End If
        Catch Ex As Exception
            Interaction.ShowMsg(Translation.Translate("\UPDATE_ERROR") & Microsoft.VisualBasic.vbNewLine & Ex.Message, Translation.Translate("\UPDATE_ERROR_TITLE"), , MessageBoxIcon.Error)
#If DEBUG Then
            Interaction.ShowMsg(Ex.Message & Microsoft.VisualBasic.vbNewLine & Ex.StackTrace)
#End If
        End Try
    End Sub
End Module

Structure CommandLine
    Shared Help As Boolean = False
    Shared Quiet As Boolean = False
    Shared TasksToRun As String = ""
    Shared ShowPreview As Boolean = False
    Shared RunAsScheduler As Boolean = False 'TODO: Enum for RunAs: Scheduler, Enqueuing, Normal
    Shared Silent As Boolean = False
    Shared Log As Boolean = False
End Structure

Public Module Interaction
    Public StatusIcon As NotifyIcon = New NotifyIcon() With {.BalloonTipTitle = "Create Synchronicity", .BalloonTipIcon = ToolTipIcon.Info}
    Private SharedToolTip As ToolTip = New ToolTip() With {.UseFading = False, .UseAnimation = False, .ToolTipIcon = ToolTipIcon.Info}

    Public Sub LoadStatusIcon()
        Dim Assembly As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly()
        StatusIcon.Icon = New Drawing.Icon(Assembly.GetManifestResourceStream("Create_Synchronicity.create-synchronicity-icon-16x16.ico"))
    End Sub

    Private Function RemoveNewLines(ByVal Msg As String) As String
        Return Msg.Replace(Environment.NewLine, " // ")
    End Function

    Public Sub ShowStatusIcon()
        StatusIcon.Visible = Not CommandLine.Silent
    End Sub

    Public Sub HideStatusIcon()
        StatusIcon.Visible = False
    End Sub

    Public Sub ShowBallonTip(ByVal Msg As String)
        If CommandLine.Silent Or Not StatusIcon.Visible Then
            ConfigHandler.LogAppEvent(String.Format("Interaction: Balloon tip discarded. The message was ""{0}"".", RemoveNewLines(Msg)))
            Exit Sub
        End If

        StatusIcon.BalloonTipText = Msg
        StatusIcon.ShowBalloonTip(2000)
    End Sub

    Public Sub ShowToolTip(ByVal sender As Control)
        If TypeOf sender Is TreeView AndAlso Not CType(sender, TreeView).CheckBoxes Then Exit Sub

        Dim Offset As Integer = If(TypeOf sender Is RadioButton Or TypeOf sender Is CheckBox, 12, 1)
        Dim Pair As String() = sender.Tag.ToString.Replace("%s", sender.Text).Split(New Char() {";"c}, 2)

        Try
            If Pair.GetLength(0) = 1 Then
                SharedToolTip.ToolTipTitle = ""
                SharedToolTip.Show(Pair(0), sender, New Drawing.Point(0, sender.Height + Offset))
            ElseIf Pair.GetLength(0) > 1 Then
                SharedToolTip.ToolTipTitle = Pair(0)
                SharedToolTip.Show(Pair(1), sender, New Drawing.Point(0, sender.Height + Offset))
            End If
        Catch ex As Exception
            'See bug #3076129
        End Try
    End Sub

    Public Sub HideToolTip(ByVal sender As Control)
        SharedToolTip.Hide(sender)
    End Sub

    Public Function ShowMsg(ByVal Text As String, Optional ByVal Caption As String = "", Optional ByVal Buttons As MessageBoxButtons = MessageBoxButtons.OK, Optional ByVal Icon As MessageBoxIcon = MessageBoxIcon.None) As DialogResult
        If CommandLine.Silent Then
            ConfigHandler.LogAppEvent(String.Format("Interaction: Message Box discarded with default answer. The message was ""{0}"", and the caption was ""{1}"".", RemoveNewLines(Text), RemoveNewLines(Caption)))
            Return DialogResult.OK
        End If

        Return MessageBox.Show(Text, Caption, Buttons, Icon)
    End Function
End Module

Public Class ListViewColumnSorter
    Implements System.Collections.IComparer

    Public Order As SortOrder
    Public SortColumn As Integer
    Private ObjectCompare As Collections.CaseInsensitiveComparer

    Public Sub New(Optional ByVal ColumnId As Integer = 0)
        SortColumn = ColumnId
        Order = SortOrder.Ascending
        ObjectCompare = New Collections.CaseInsensitiveComparer()
    End Sub

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements Collections.IComparer.Compare
        Return If(Order = SortOrder.Ascending, 1, If(Order = SortOrder.Descending, -1, 0)) * ObjectCompare.Compare(CType(x, ListViewItem).SubItems(SortColumn).Text, CType(y, ListViewItem).SubItems(SortColumn).Text)
    End Function
End Class
