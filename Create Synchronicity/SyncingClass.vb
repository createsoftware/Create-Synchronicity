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

Public Class SyncingAction
    Public Action As TypeOfAction
    Public Source As SideOfSource
    Public SourcePath As String
    Public DestinationPath As String
End Class