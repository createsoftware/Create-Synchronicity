'This file is part of Create Synchronicity.
'
'Create Synchronicity is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
'Create Synchronicity is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with Create Synchronicity.  If not, see <http://www.gnu.org/licenses/>.
'Created by:	Clément Pit--Claudel.
'Web site:		http://synchronicity.sourceforge.net.

Public Enum TypeOfItem As Integer
    File = 0
    Folder = 1
End Enum

Public Enum TypeOfAction As Integer
    Delete = -1
    None = 0
    Create = 1
End Enum

Public Enum SideOfSource As Integer
    Left = -1
    Right = 1
End Enum

Public Class SyncingAction
    Public Action As TypeOfAction
    Public Source As SideOfSource
    Public SourcePath As String
    Public DestinationPath As String
End Class

Public Class SyncingItem
    Public Path As String
    Public Type As TypeOfItem
    Public Action As TypeOfAction

    Private Translation As LanguageHandler = LanguageHandler.GetSingleton

    Sub New(ByVal _Path As String, ByVal _Type As TypeOfItem, ByVal _Action As TypeOfAction)
        Path = _Path
        Type = _Type
        Action = _Action
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
            Case TypeOfAction.Create
                Return Translation.Translate("\CREATE")
            Case TypeOfAction.Delete
                Return Translation.Translate("\DELETE")
            Case Else
                Return Translation.Translate("\NONE")
        End Select
    End Function

    Function FormatDirection(ByVal Side As SideOfSource) As String
        Select Case Side
            Case SideOfSource.Left
                Return If(Action = TypeOfAction.Create, Translation.Translate("\LR"), Translation.Translate("\LEFT"))
            Case SideOfSource.Right
                Return If(Action = TypeOfAction.Create, Translation.Translate("\RL"), Translation.Translate("\RIGHT"))
            Case Else
                Return ""
        End Select
    End Function
End Class

Public Class FileNamePattern
    Public Enum PatternType
        FileExt
        FileName
        Regex
    End Enum

    Public Type As PatternType
    Public Pattern As String

    Sub New(ByVal _Type As PatternType, ByVal _Pattern As String)
        Type = _Type
        Pattern = _Pattern
    End Sub

    Shared Function GetPattern(ByVal Pattern As String) As FileNamePattern
        If Pattern.StartsWith("""") And Pattern.EndsWith("""") Then 'Filename
            Return New FileNamePattern(PatternType.FileName, Pattern.Substring(1, Pattern.Length - 2).ToLower)
        ElseIf Pattern.StartsWith("/") And Pattern.EndsWith("/") Then 'Regex
            Return New FileNamePattern(PatternType.Regex, Pattern.Substring(1, Pattern.Length - 2).ToLower)
        Else
            Return New FileNamePattern(PatternType.FileExt, Pattern.ToLower)
        End If
    End Function

    Shared Sub LoadPatternsList(ByRef PatternsList As List(Of FileNamePattern), ByVal Patterns As String())
        PatternsList = New List(Of FileNamePattern)

        For Each Pattern As String In Patterns
            If Not Pattern = "" Then PatternsList.Add(GetPattern(Pattern))
        Next
    End Sub
End Class