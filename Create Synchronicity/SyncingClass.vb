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

    Sub New(ByVal _Path As String, ByVal _Type As TypeOfItem, ByVal _Action As TypeOfAction)
        Path = _Path
        Type = _Type
        Action = _Action
    End Sub

    Function FormatType() As String
        Select Case Type
            Case TypeOfItem.File
                Return "File"
            Case Else
                Return "Folder"
        End Select
    End Function

    Function FormatAction() As String
        Select Case Action
            Case TypeOfAction.Create
                Return "Create"
            Case TypeOfAction.Delete
                Return "Delete"
            Case Else
                Return "None"
        End Select
    End Function
End Class
