'This file is part of Create Synchronicity.
'
'Create Synchronicity is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
'Create Synchronicity is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with Create Synchronicity.  If not, see <http://www.gnu.org/licenses/>.
'Created by:	Clément Pit--Claudel.
'Web site:		http://synchronicity.sourceforge.net.

Friend Module ProfileSetting
    Public Const Source As String = "Source Directory"
    Public Const Destination As String = "Destination Directory"
    Public Const IncludedTypes As String = "Included Filetypes"
    Public Const ExcludedTypes As String = "Excluded FileTypes"
    Public Const ReplicateEmptyDirectories As String = "Replicate Empty Directories"
    Public Const Method As String = "Synchronization Method"
    Public Const Restrictions As String = "Files restrictions"
    Public Const LeftSubFolders As String = "Source folders to be synchronized"
    Public Const RightSubFolders As String = "Destination folders to be synchronized"
    Public Const MayCreateDestination As String = "Create destination folder"
    Public Const StrictDateComparison As String = "Strict date comparison"
    Public Const PropagateUpdates As String = "Propagate Updates"
    Public Const StrictMirror As String = "Strict mirror"
    Public Const TimeOffset As String = "Time Offset"
    Public Const LastRun As String = "Last run"
    Public Const CatchUpSync As String = "Catch up if missed"
    Public Const CompressionExt As String = "Compress"
    Public Const Group As String = "Group"
    Public Const CheckFileSize As String = "Check file size"
    Public Const FuzzyDstCompensation As String = "Fuzzy DST compensation"
    Public Const Checksum As String = "Checksum"

    'Next settings are hidden, not automatically appended to config files.
    Public Const ExcludedFolders As String = "Excluded folder patterns"
    Public Const Turbo As String = "Turbo mode"
    '</>

    Public Const Scheduling As String = "Scheduling"
    Public Const SchedulingSettingsCount As Integer = 5 'Frequency;WeekDay;MonthDay;Hour;Minute
End Module

NotInheritable Class ProfileHandler
    Public ProfileName As String
    Public IsNewProfile As Boolean
    Public Scheduler As New ScheduleInfo()
    Public Configuration As New Dictionary(Of String, String)
    Public LeftCheckedNodes As New Dictionary(Of String, Boolean)
    Public RightCheckedNodes As New Dictionary(Of String, Boolean)

    'NOTE: Only vital settings should be checked for correctness, since the config will be rejected if a mismatch occurs.
    Private Shared ReadOnly RequiredSettings() As String = {ProfileSetting.Source, ProfileSetting.Destination, ProfileSetting.ExcludedTypes, ProfileSetting.IncludedTypes, ProfileSetting.LeftSubFolders, ProfileSetting.RightSubFolders, ProfileSetting.Method, ProfileSetting.Restrictions, ProfileSetting.ReplicateEmptyDirectories}

    Public Sub New(ByVal Name As String)
        ProfileName = Name
        IsNewProfile = Not LoadConfigFile()
        If GetSetting(Of Boolean)(ProfileSetting.MayCreateDestination, False) And GetSetting(Of String)(ProfileSetting.RightSubFolders) Is Nothing Then SetSetting(Of String)(ProfileSetting.RightSubFolders, "*")
    End Sub

    Function LoadConfigFile() As Boolean
        If Not IO.File.Exists(ProgramConfig.GetConfigPath(ProfileName)) Then Return False

        Configuration.Clear()
        Using FileReader As New IO.StreamReader(ProgramConfig.GetConfigPath(ProfileName))
            While Not FileReader.EndOfStream
                Dim ConfigLine As String = ""
                Try
                    ConfigLine = FileReader.ReadLine()
                    Dim Param() As String = ConfigLine.Split(":".ToCharArray, 2)
                    If Not Configuration.ContainsKey(Param(0)) Then Configuration.Add(Param(0), Param(1))
                Catch ex As Exception
                    Interaction.ShowMsg(String.Format(Translation.Translate("\INVALID_SETTING"), ConfigLine))
                End Try
            End While
        End Using

        LoadScheduler()
        LoadSubFoldersList(ProfileSetting.LeftSubFolders, LeftCheckedNodes)
        LoadSubFoldersList(ProfileSetting.RightSubFolders, RightCheckedNodes)
        Return True
    End Function

    Function SaveConfigFile() As Boolean
        Try
            Using FileWriter As New IO.StreamWriter(ProgramConfig.GetConfigPath(ProfileName))
                For Each Setting As KeyValuePair(Of String, String) In Configuration
                    FileWriter.WriteLine(Setting.Key & ":" & Setting.Value)
                Next
            End Using

            Return True
        Catch Ex As Exception
            ConfigHandler.LogAppEvent("Unable to save config file for " & ProfileName & Environment.NewLine & Ex.ToString)
            Return False
        End Try
    End Function

    ' `ReturnString` is used to pass locally generated error messages to caller.
    Function ValidateConfigFile(Optional ByVal WarnUnrootedPaths As Boolean = False, Optional ByVal TryCreateDest As Boolean = False, Optional ByVal Silent As Boolean = False, Optional ByRef FailureMsg As String = Nothing) As Boolean
        Dim IsValid As Boolean = True
        Dim InvalidListing As New List(Of String)

        If Not IO.Directory.Exists(TranslatePath(GetSetting(Of String)(ProfileSetting.Source))) Then
            InvalidListing.Add(Translation.Translate("\INVALID_SOURCE"))
            IsValid = False
        End If

        Dim Dest As String = TranslatePath(GetSetting(Of String)(ProfileSetting.Destination))
        Dim _MayCreateDest As Boolean = GetSetting(Of Boolean)(ProfileSetting.MayCreateDestination, False)
        If _MayCreateDest And TryCreateDest Then
            Try
                IO.Directory.CreateDirectory(Dest)
            Catch Ex As Exception
                InvalidListing.Add(String.Format(Translation.Translate("\FOLDER_FAILED"), Dest, Ex.Message))
            End Try
        End If

        If (Not IO.Directory.Exists(Dest)) And (TryCreateDest Or (Not _MayCreateDest)) Then
            InvalidListing.Add(Translation.Translate("\INVALID_DEST"))
            IsValid = False
        End If

        For Each Key As String In RequiredSettings
            If Not Configuration.ContainsKey(Key) Then
                IsValid = False
                InvalidListing.Add(String.Format(Translation.Translate("\SETTING_UNSET"), Key))
            End If
        Next

        If Configuration.ContainsKey(ProfileSetting.CompressionExt) AndAlso Configuration(ProfileSetting.CompressionExt) <> "" Then
            If Array.IndexOf({".gz", ".bz2"}, Configuration(ProfileSetting.CompressionExt)) < 0 Then
                IsValid = False
                InvalidListing.Add("Unknown compression extension, or missing ""."":" & Configuration(ProfileSetting.CompressionExt))
            End If

            If Not IO.File.Exists(ProgramConfig.CompressionDll) Then
                IsValid = False
                InvalidListing.Add(String.Format("{0} not found!", ProgramConfig.CompressionDll))
            End If
        End If

        If Not IsValid Then
            Dim ErrorsList As String = String.Join(Environment.NewLine, InvalidListing.ToArray)
            Dim ErrMsg As String = String.Format("{0} - {1}{2}{3}", ProfileName, Translation.Translate("\INVALID_CONFIG"), Environment.NewLine, ErrorsList)

            If Not FailureMsg Is Nothing Then FailureMsg = ErrMsg
            If Not Silent Then Interaction.ShowMsg(ErrMsg, Translation.Translate("\INVALID_CONFIG"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        Else
            If WarnUnrootedPaths Then
                If Not IO.Path.IsPathRooted(TranslatePath(GetSetting(Of String)(ProfileSetting.Source))) Then
                    If Interaction.ShowMsg(String.Format(Translation.Translate("\LEFT_UNROOTED"), IO.Path.GetFullPath(GetSetting(Of String)(ProfileSetting.Source))), , MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Return False
                End If

                If Not IO.Path.IsPathRooted(TranslatePath(GetSetting(Of String)(ProfileSetting.Destination))) Then
                    If Interaction.ShowMsg(String.Format(Translation.Translate("\RIGHT_UNROOTED"), IO.Path.GetFullPath(GetSetting(Of String)(ProfileSetting.Source))), , MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Return False
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

    Sub SetSetting(Of T)(ByVal SettingName As String, ByVal Value As T)
        Configuration(SettingName) = Value.ToString 'LATER: There might be a problem here, when serializing dates (locale-dependent problems).
    End Sub

    Sub CopySetting(Of T)(ByVal Key As String, ByRef Value As T, ByVal Load As Boolean)
        If Load Then
            Value = GetSetting(Of T)(Key, Value) 'Passes the current value as default answer.
        Else
            Configuration(Key) = If(Value IsNot Nothing, Value.ToString, Nothing)
        End If
    End Sub

    Function GetSetting(Of T)(ByVal Key As String, Optional ByVal DefaultVal As T = Nothing) As T
        Dim Val As String = ""
        If Configuration.TryGetValue(Key, Val) AndAlso Not String.IsNullOrEmpty(Val) Then
            Try
                Return CType(CObj(Val), T)
            Catch
                SetSetting(Of T)(Key, DefaultVal) 'Couldn't convert the value to a proper format; resetting.
            End Try
        End If
        Return DefaultVal
    End Function

    Sub LoadScheduler()
        Dim Opts() As String = GetSetting(Of String)(ProfileSetting.Scheduling, "").Split(";".ToCharArray, StringSplitOptions.RemoveEmptyEntries)

        If Opts.GetLength(0) = ProfileSetting.SchedulingSettingsCount Then
            Scheduler = New ScheduleInfo(Opts(0), Opts(1), Opts(2), Opts(3), Opts(4))
        Else
            Scheduler = New ScheduleInfo() With {.Frequency = ScheduleInfo.Freq.Never} 'NOTE: Wrong strings default to never
        End If
    End Sub

    Sub SaveScheduler()
        SetSetting(Of String)(ProfileSetting.Scheduling, String.Join(";", New String() {Scheduler.Frequency.ToString, Scheduler.WeekDay.ToString, Scheduler.MonthDay.ToString, Scheduler.Hour.ToString, Scheduler.Minute.ToString}))
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
        Return TranslatePath_Unsafe(Path).TrimEnd(ProgramSetting.DirSep) 'Careful with Linux root
        'This part is just for the extra safety, since a fix is also included in TranslatePath_Unsafe.
        'Prevents a very annoying bug, where the presence of a slash at the end of the base directory would confuse the engine (#3052979)
    End Function

    Public Shared Function TranslatePath_Inverse(ByVal Path As String) As String
#If CONFIG <> "Linux" Then
        If Text.RegularExpressions.Regex.IsMatch(Path, "^(?<driveletter>[A-Z]\:)(\\(?<relativepath>.*))?$") Then
            Dim Label As String = ""
            For Each Drive As IO.DriveInfo In IO.DriveInfo.GetDrives
                If Drive.Name(0) = Path(0) Then Label = Drive.VolumeLabel
            Next
            If Label <> "" Then Return String.Format("""{0}""\{1}", Label, Path.Substring(2).Trim(ProgramSetting.DirSep)).TrimEnd(ProgramSetting.DirSep)
        End If
#End If

        Return Path
    End Function

    Private Shared Function TranslatePath_Unsafe(ByVal Path As String) As String
        Dim Translated_Path As String = Path

#If CONFIG <> "Linux" Then
        Dim Label As String, RelativePath As String
        If Path.StartsWith("""") Or Path.StartsWith(":") Then
            Dim ClosingPos As Integer = Path.LastIndexOfAny(""":".ToCharArray)
            If ClosingPos = 0 Then Return "" 'LINUX: Currently returns "" (aka linux root) if no closing op is found.

            Label = Path.Substring(1, ClosingPos - 1)
            RelativePath = Path.Substring(ClosingPos + 1)

            If Path.StartsWith("""") And Not Label = "" Then
                For Each Drive As IO.DriveInfo In IO.DriveInfo.GetDrives
                    'The drive's name ends with a "\". If RelativePath = "", then TrimEnd on the RelativePath won't do anything.
                    'This is the line why this function is called unsafe, but it's been made safe anyway: dirty code that get fixed later on crosses me. The point is that a source/destination path should *never* end with a DirSep, otherwise the system gets confused as to what is a relative path and what is the base path.
                    If Not Drive.Name(0) = "A"c AndAlso Drive.IsReady AndAlso String.Compare(Drive.VolumeLabel, Label, True) = 0 Then
                        Translated_Path = (Drive.Name & RelativePath.TrimStart(ProgramSetting.DirSep)).TrimEnd(ProgramSetting.DirSep) 'Bug #3052979
                        Exit For
                    End If
                Next
            End If
        End If
#End If

        ' Use a path-friendly version of the DATE constant.
        Environment.SetEnvironmentVariable("MMMYYYY", Date.Today.ToString("MMMYYYY").ToLower(Interaction.InvariantCulture))
        Environment.SetEnvironmentVariable("DATE", Date.Today.ToShortDateString.Replace("/"c, "-"c))
        Environment.SetEnvironmentVariable("DAY", Date.Today.Day.ToString)
        Environment.SetEnvironmentVariable("MONTH", Date.Today.Month.ToString)
        Environment.SetEnvironmentVariable("YEAR", Date.Today.Year.ToString)

        Return Environment.ExpandEnvironmentVariables(Translated_Path)
    End Function

    Public Function GetLastRun() As Date
        Try
            Return GetSetting(Of Date)(ProfileSetting.LastRun, ScheduleInfo.DATE_NEVER) 'TODO: Check conversion
        Catch
            Return ScheduleInfo.DATE_NEVER
        End Try
    End Function

    Public Sub SetLastRun()
        SetSetting(Of Date)(ProfileSetting.LastRun, Date.Now)
        SaveConfigFile()
    End Sub
End Class

Structure SchedulerEntry
    Dim Name As String
    Dim NextRun As Date
    Dim CatchUp As Boolean
    Dim HasFailed As Boolean

    Sub New(ByVal _Name As String, ByVal _NextRun As Date, ByVal _Catchup As Boolean, ByVal _HasFailed As Boolean)
        Name = _Name
        NextRun = _NextRun
        CatchUp = _Catchup
        HasFailed = _HasFailed
    End Sub
End Structure

Structure ScheduleInfo
    Enum Freq
        Never
        Daily
        Weekly
        Monthly
    End Enum

    Public Frequency As Freq
    Public WeekDay, MonthDay, Hour, Minute As Integer 'Sunday = 0

    Public Shared ReadOnly DATE_NEVER As Date = Date.MaxValue
    Public Shared ReadOnly DATE_CATCHUP As Date = Date.MinValue

    Sub New(ByVal Frq As String, ByVal _WeekDay As String, ByVal _MonthDay As String, ByVal _Hour As String, ByVal _Minute As String)
        Try
            Hour = CInt(_Hour)
            Minute = CInt(_Minute)
            WeekDay = CInt(_WeekDay)
            MonthDay = CInt(_MonthDay)
            Frequency = Str2Freq(Frq)
        Catch Ex As FormatException
        Catch Ex As OverflowException
        End Try
    End Sub

    Private Shared Function Str2Freq(ByVal Str As String) As Freq
        Try
            Return CType([Enum].Parse(GetType(Freq), Str, True), Freq)
        Catch Ex As ArgumentException
            Return Freq.Never
        End Try
    End Function

    Function GetInterval() As TimeSpan
        Dim Interval As TimeSpan
        Select Case Frequency
            Case Freq.Daily
                Interval = New TimeSpan(1, 0, 0, 0)
            Case Freq.Weekly
                Interval = New TimeSpan(7, 0, 0, 0)
            Case Freq.Monthly
                Interval = Date.Today.AddMonths(1) - Date.Today
            Case Freq.Never
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
            Case Freq.Daily
                RunAt = Today.AddHours(Hour).AddMinutes(Minute)
            Case Freq.Weekly
                RunAt = Today.AddDays(WeekDay - Today.DayOfWeek).AddHours(Hour).AddMinutes(Minute)
            Case Freq.Monthly
                RunAt = Today.AddDays(MonthDay - Today.Day).AddHours(Hour).AddMinutes(Minute)
            Case Else
                Return DATE_NEVER
        End Select

        '">=" prevents double-syncing. Using ">" could cause the scheduler to queue Date.Now as next run time.
        While Now >= RunAt : RunAt += Interval : End While 'Loop needed (eg when today = jan 1 and schedule = every 1st month day)
        Return RunAt
    End Function
End Structure
