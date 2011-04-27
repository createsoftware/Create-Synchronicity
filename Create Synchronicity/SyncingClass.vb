'This file is part of Create Synchronicity.
'
'Create Synchronicity is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
'Create Synchronicity is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with Create Synchronicity.  If not, see <http://www.gnu.org/licenses/>.
'Created by:	Clément Pit--Claudel.
'Web site:		http://synchronicity.sourceforge.net.

Structure StatusData
    Enum SyncStep
        Done = 0
        Scan = 1
        SyncLR = 2
        SyncRL = 3
    End Enum

    Dim StartTime As Date
    Dim BytesCopied As Long
    Dim BytesScanned As Long
    Dim FilesScanned As Long
    Dim ActionsDone As Long
    Dim CreatedFiles As Long
    Dim CreatedFolders As Long
    Dim FilesToCreate As Long
    Dim FoldersToCreate As Long
    Dim DeletedFiles As Long
    Dim DeletedFolders As Long
    Dim FilesToDelete As Long
    Dim FoldersToDelete As Long
    Dim TotalActionsCount As Long
    Dim CurrentStep As SyncStep
    Dim TimeElapsed As TimeSpan
    Dim Speed As Double

    Dim Cancel As Boolean
    Dim Failed As Boolean
    Dim FailureMsg As String
End Structure

Public Enum TypeOfItem As Integer
    File = 0
    Folder = 1
End Enum

Public Enum TypeOfAction As Integer
    Delete = -1
    None = 0
    Copy = 1
End Enum

Public Enum SideOfSource As Integer
    Left = 0
    Right = 1
End Enum

Structure SyncingAction
    Public Action As TypeOfAction
    Public Source As SideOfSource
    Public SourcePath As String
    Public DestinationPath As String
End Structure

Structure SyncingItem
    Public Path As String
    Public Type As TypeOfItem

    Public IsUpdate As Boolean
    Public Action As TypeOfAction

    Sub New(ByVal _Path As String, ByVal _Type As TypeOfItem, ByVal _Action As TypeOfAction, ByVal _IsUpdate As Boolean)
        Path = _Path
        Type = _Type
        Action = _Action
        IsUpdate = _IsUpdate
    End Sub

    Function FormatType() As String
        Select Case Type
            Case TypeOfItem.File
                Return Translation.Translate("\FILE")
            Case Else
                Return Translation.Translate("\FOLDER")
        End Select
    End Function

    Function FormatAction() As String
        Select Case Action
            Case TypeOfAction.Copy
                Return If(IsUpdate, Translation.Translate("\UPDATE"), Translation.Translate("\CREATE")) 'FIXME: s/\UPDATE/\CREATE if translations are not ok.
            Case TypeOfAction.Delete
                Return Translation.Translate("\DELETE")
            Case Else
                Return Translation.Translate("\NONE")
        End Select
    End Function

    Function FormatDirection(ByVal Side As SideOfSource) As String
        Select Case Side
            Case SideOfSource.Left
                Return If(Action = TypeOfAction.Copy, Translation.Translate("\LR"), Translation.Translate("\LEFT"))
            Case SideOfSource.Right
                Return If(Action = TypeOfAction.Copy, Translation.Translate("\RL"), Translation.Translate("\RIGHT"))
            Case Else
                Return ""
        End Select
    End Function
End Structure

Friend NotInheritable Class FileNamePattern
    Public Enum PatternType
        FileExt
        FileName
        FolderName
        Regex
    End Enum

    Public Type As PatternType
    Public Pattern As String

    Sub New(ByVal _Type As PatternType, ByVal _Pattern As String)
        Type = _Type
        Pattern = _Pattern
    End Sub

    Private Shared Function IsBoxed(ByVal Frame As Char, ByVal Str As String) As Boolean
        Return (Str.StartsWith(Frame) And Str.EndsWith(Frame) And Str.Length > 2)
    End Function

    Private Shared Function Unbox(ByVal Str As String) As String
        Return Str.Substring(1, Str.Length - 2).ToLower(Interaction.InvariantCulture) ' ToLower: Careful on linux ; No need to check length, MatchesPattern has done so before.
    End Function

    Shared Function GetPattern(ByVal Str As String, Optional ByVal IsFolder As Boolean = False) As FileNamePattern
        If IsBoxed(""""c, Str) Then 'Filename
            Return New FileNamePattern(If(IsFolder, PatternType.FolderName, PatternType.FileName), Unbox(Str))
        ElseIf IsBoxed("/"c, Str) Then 'Regex
            Return New FileNamePattern(PatternType.Regex, Unbox(Str))
        Else
            Return New FileNamePattern(PatternType.FileExt, Str.ToLower(Interaction.InvariantCulture))
        End If
    End Function

    Private Shared Function SharpInclude(ByVal FileName As String) As String
        Dim Path As String = ProgramConfig.ConfigRootDir & ProgramSetting.DirSep & FileName
        Return If(IO.File.Exists(Path), IO.File.ReadAllText(Path), FileName)
    End Function

    Friend Shared Sub LoadPatternsList(ByRef PatternsList As List(Of FileNamePattern), ByVal PatternsStr As String, Optional ByVal IsFolder As Boolean = False)
        PatternsList = New List(Of FileNamePattern)
        Dim Patterns As New List(Of String)(PatternsStr.Split(";".ToCharArray, StringSplitOptions.RemoveEmptyEntries))

        While Patterns.Count > 0 And Patterns.Count < 1024 'Prevent circular references
            If IsBoxed(":"c, Patterns(0)) Then
                Dim SubPatterns As String = SharpInclude(Unbox(Patterns(0)))
                Patterns.AddRange(SubPatterns.Split(";".ToCharArray, StringSplitOptions.RemoveEmptyEntries))
            Else
                PatternsList.Add(GetPattern(Patterns(0), IsFolder))
            End If

            Patterns.RemoveAt(0)
        End While
    End Sub
End Class

Module FileHandling
    Friend Function GetFileOrFolderName(ByVal Path As String) As String
        Return Path.Substring(Path.LastIndexOf(ProgramSetting.DirSep) + 1) 'IO.Path.* -> Bad because of separate file/folder handling.
    End Function
End Module