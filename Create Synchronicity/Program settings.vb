'This file is part of Create Synchronicity.
'
'Create Synchronicity is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
'Create Synchronicity is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with Create Synchronicity.  If not, see <http://www.gnu.org/licenses/>.
'Created by:	Clément Pit--Claudel.
'Web site:		http://synchronicity.sourceforge.net.

Friend Module ProgramSetting
    'Main program settings
    Public Const Language As String = "Language"
    Public Const DefaultLanguage As String = "english"
    'Public Const SyncsCount As String = "Syncs count" 'LATER: Problem with concurrent savings of the config file.
    Public Const AutoUpdates As String = "Auto updates"
    Public Const MaxLogEntries As String = "Archived log entries"
    Public Const MainView As String = "Main view"
    Public Const FontSize As String = "Font size"
    Public Const MainFormAttributes As String = "Window size and position"
    Public Const ExpertMode As String = "Expert mode"
    Public Const DiffProgram As String = "Diff program"
    Public Const DiffArguments As String = "Diff arguments"
    Public Const TextLogs As String = "Text logs"

    'Program files
    Public Const ConfigFolderName As String = "config"
    Public Const LogFolderName As String = "log"
    Public Const SettingsFileName As String = "mainconfig.ini"
    Public Const AppLogName As String = "app.log"
    Public Const DllName As String = "compress.dll"
    'Public CompressionThreshold As Integer = 0 'Better not filter at all

    Public Const EnqueuingSeparator As Char = "|"c
#If CONFIG = "Linux" Then
    Public Const DirSep As Char = "/"c
#Else
    Public Const DirSep As Char = "\"c
#End If

#If DEBUG Then
    Public Const Debug As Boolean = True
#Else
    Public Const Debug As Boolean = False
#End If

    Public Const RegistryBootVal As String = "Create Synchronicity - Scheduler"
    Public Const RegistryBootKey As String = "Software\Microsoft\Windows\CurrentVersion\Run"
    Public Const RegistryRootedBootKey As String = "HKEY_CURRENT_USER\" & RegistryBootKey

    Public Const Website As String = "http://synchronicity.sourceforge.net/"
    Public Const UserWeb As String = "http://createsoftware.users.sourceforge.net/"
End Module

NotInheritable Class ConfigHandler
    Private Shared Singleton As ConfigHandler

    Public LogRootDir As String
    Public ConfigRootDir As String
    Public LanguageRootDir As String

    Public CompressionDll As String
    Public LocalNamesFile As String
    Public MainConfigFile As String

    Public CanGoOn As Boolean = True 'To check whether a synchronization is already running (in scheduler mode only, queuing uses callbacks).

    Friend Icon As Drawing.Icon
    Private SettingsLoaded As Boolean = False
    Private Settings As New Dictionary(Of String, String)

    Private Sub New()
        LogRootDir = GetUserFilesRootDir() & ProgramSetting.LogFolderName
        ConfigRootDir = GetUserFilesRootDir() & ProgramSetting.ConfigFolderName
        LanguageRootDir = Application.StartupPath & ProgramSetting.DirSep & "languages"

        LocalNamesFile = LanguageRootDir & ProgramSetting.DirSep & "local-names.txt"
        MainConfigFile = ConfigRootDir & ProgramSetting.DirSep & ProgramSetting.SettingsFileName
        CompressionDll = Application.StartupPath & ProgramSetting.DirSep & ProgramSetting.DllName

        Try
            Icon = Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath)
        Catch
            Icon = Drawing.Icon.FromHandle((New Drawing.Bitmap(32, 32)).GetHicon)
        End Try
    End Sub

    Public Shared Function GetSingleton() As ConfigHandler
        If Singleton Is Nothing Then Singleton = New ConfigHandler()
        Return Singleton
    End Function

    Public Function GetConfigPath(ByVal Name As String) As String
        Return ConfigRootDir & ProgramSetting.DirSep & Name & ".sync"
    End Function

    Public Function GetLogPath(ByVal Name As String) As String
        Return LogRootDir & ProgramSetting.DirSep & Name & ".log" & If(ProgramSetting.Debug Or ProgramConfig.GetProgramSetting(Of Boolean)(ProgramSetting.TextLogs, False), "", ".html")
    End Function

    Public Function GetUserFilesRootDir() As String 'Return the place were config files are stored
        Static UserFilesRootDir As String = ""
        If Not UserFilesRootDir = "" Then Return UserFilesRootDir

        Dim UserPath As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & ProgramSetting.DirSep & "Create Software" & ProgramSetting.DirSep & "Create Synchronicity" & ProgramSetting.DirSep

        'http://support.microsoft.com/default.aspx?scid=kb;EN-US;326549
        Dim WriteNeededFiles As New List(Of String)
        Dim WriteNeededFolders As New List(Of String)
        Dim PotentialWriteNeededFolders As String() = {Application.StartupPath & ProgramSetting.DirSep & LogFolderName, Application.StartupPath & ProgramSetting.DirSep & ConfigFolderName}

        WriteNeededFolders.Add(Application.StartupPath)
        For Each Folder As String In PotentialWriteNeededFolders
            If IO.Directory.Exists(Folder) Then
                WriteNeededFolders.Add(Folder)
                WriteNeededFiles.AddRange(IO.Directory.GetFiles(Folder))
            End If
        Next

        Dim Writable As Boolean = True
        Dim ProgramPathExists As Boolean = IO.Directory.Exists(Application.StartupPath & ProgramSetting.DirSep & ConfigFolderName)

        For Each Folder As String In WriteNeededFolders
            Dim FolderInfo As New IO.DirectoryInfo(Folder)
            Writable = Writable And (Not (FolderInfo.Attributes And IO.FileAttributes.ReadOnly) = IO.FileAttributes.ReadOnly)

            Try
                Dim TestPath As String = Folder & ProgramSetting.DirSep & "write-permissions"
                IO.File.Create(TestPath).Close()
                IO.File.Delete(TestPath)
            Catch
                Writable = False
            End Try
        Next
        For Each File As String In WriteNeededFiles
            Writable = Writable And (Not (IO.File.GetAttributes(File) And IO.FileAttributes.ReadOnly) = IO.FileAttributes.ReadOnly)
        Next

        ' When a user folder exists, and no config folder exists in the install dir, use the user's folder.
        If Writable And (ProgramPathExists Or Not IO.Directory.Exists(UserPath)) Then
            UserFilesRootDir = Application.StartupPath & ProgramSetting.DirSep
        Else
            'Not translated, since it happens before loading translation files
            If ProgramPathExists Then Interaction.ShowMsg("Create Synchronicity cannot write to your installation directory, although it contains configuration files. Your Application Data folder will therefore be used instead.", "Information", , MessageBoxIcon.Information)
            UserFilesRootDir = UserPath
        End If

        Return UserFilesRootDir
    End Function

    Public Function GetProgramSetting(Of T)(ByVal Key As String, ByVal DefaultVal As T) As T
        Dim Val As String = ""
        If Settings.TryGetValue(Key, Val) AndAlso Not String.IsNullOrEmpty(Val) Then
            Try
                Return CType(CObj(Val), T)
            Catch
                SetProgramSetting(Of T)(Key, DefaultVal) 'Couldn't convert the value to a proper format; resetting.
            End Try
        End If
        Return DefaultVal
    End Function

    Public Sub SetProgramSetting(Of T)(ByVal Key As String, ByVal Value As T)
        Settings(Key) = Value.ToString
    End Sub

    Public Sub LoadProgramSettings()
        If SettingsLoaded Then Exit Sub

        IO.Directory.CreateDirectory(ConfigRootDir)
        If Not IO.File.Exists(MainConfigFile) Then
            IO.File.Create(MainConfigFile).Close()
            Exit Sub
        End If

        Dim ConfigString As String = IO.File.ReadAllText(MainConfigFile)
        Dim ConfigArray As New List(Of String)(ConfigString.Split(";"c))
        For Each Setting As String In ConfigArray
            Dim Pair As String() = Setting.Split(":".ToCharArray, 2)
            If Pair.Length() < 2 Then Continue For
            If Settings.ContainsKey(Pair(0)) Then Settings.Remove(Pair(0))
            Settings.Add(Pair(0).Trim, Pair(1).Trim)
        Next

        SettingsLoaded = True
    End Sub

    Public Sub SaveProgramSettings()
        Dim ConfigStrB As New Text.StringBuilder
        For Each Setting As KeyValuePair(Of String, String) In Settings
            ConfigStrB.AppendFormat("{0}:{1};", Setting.Key, Setting.Value)
        Next

        Try
            IO.File.WriteAllText(MainConfigFile, ConfigStrB.ToString) 'IO.File.WriteAllText overwrites the file.
        Catch
            Interaction.ShowDebug("Unable to save main config file.", , MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function ProgramSettingsSet(ByVal Setting As String) As Boolean
        Return Settings.ContainsKey(Setting)
    End Function

    <Diagnostics.Conditional("Debug")>
    Public Shared Sub LogDebugEvent(ByVal EventData As String)
#If DEBUG Then
        LogAppEvent(EventData)
#End If
    End Sub

    Public Shared Sub LogAppEvent(ByVal EventData As String)
        If ProgramSetting.Debug Or CommandLine.Silent Or CommandLine.Log Then
            Static UniqueID As String = Guid.NewGuid().ToString

            Using AppLog As New IO.StreamWriter(Singleton.GetUserFilesRootDir() & ProgramSetting.AppLogName, True)
                AppLog.WriteLine(String.Format("[{0}][{1}] {2}", UniqueID, Date.Now.ToString(), EventData))
            End Using
        End If
    End Sub

    Public Shared Sub RegisterBoot()
        If Microsoft.Win32.Registry.GetValue(ProgramSetting.RegistryRootedBootKey, ProgramSetting.RegistryBootVal, Nothing) Is Nothing Then
            ConfigHandler.LogAppEvent("Registering program in startup list")
            Microsoft.Win32.Registry.SetValue(ProgramSetting.RegistryRootedBootKey, ProgramSetting.RegistryBootVal, Application.ExecutablePath & " /scheduler")
        End If
    End Sub

#If 0 Then
    ' This method requires saving mainconfig.ini, thus overwriting customizations made when the scheduler was running. Problem.
    Public Sub IncrementSyncsCount()
        Dim Count As Integer
        Try
            Count = GetProgramSetting(ConfigOptions.SyncsCount, "0")
        Catch
            Count = 0
        End Try
        SetProgramSetting(Of Integer)(ConfigOptions.SyncsCount, Count + 1)
    End Sub
#End If
End Class

Structure CommandLine
    Enum RunMode
        Normal
        Scheduler
        Queue
    End Enum

    Shared Help As Boolean '= False
    Shared Quiet As Boolean '= False
    Shared TasksToRun As String = ""
    Shared ShowPreview As Boolean '= False
    Shared RunAs As RunMode '= RunMode.Normal
    Shared Silent As Boolean '= False
    Shared Log As Boolean '= False
    Shared NoUpdates As Boolean '= False
    Shared NoStop As Boolean '= False

    Shared Sub ReadArgs(ByVal ArgsList As List(Of String))
        If ArgsList.Count > 1 Then
            CommandLine.Help = ArgsList.Contains("/help")
            CommandLine.Quiet = ArgsList.Contains("/quiet")
            CommandLine.ShowPreview = ArgsList.Contains("/preview")
            CommandLine.Silent = ArgsList.Contains("/silent")
            CommandLine.Log = ArgsList.Contains("/log")
            CommandLine.NoUpdates = ArgsList.Contains("/noupdates")
            CommandLine.NoStop = ArgsList.Contains("/nostop")

            CommandLine.Quiet = CommandLine.Quiet Or CommandLine.Silent

            Dim RunArgIndex As Integer = ArgsList.IndexOf("/run")
            If RunArgIndex <> -1 AndAlso RunArgIndex + 1 < ArgsList.Count Then
                CommandLine.TasksToRun = ArgsList(RunArgIndex + 1)
            End If
        End If

        If CommandLine.TasksToRun <> "" Then
            CommandLine.RunAs = CommandLine.RunMode.Queue
        ElseIf ArgsList.Contains("/scheduler") Then
            CommandLine.RunAs = CommandLine.RunMode.Scheduler
        End If
    End Sub
End Structure
