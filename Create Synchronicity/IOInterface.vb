#If 0 Then
Public Class GenericFS
    'Needed?
End Class

Public Class FtpFS
    Inherits GenericFS
    Implements IOOperator
End Class

Public Interface FileSystem
    ReadOnly Property DirectorySeparatorChar As String

    Sub File_Copy(ByVal Path As String)
    Sub File_Delete(ByVal Path As String)
    Sub Directory_Delete(ByVal Path As String)
    Sub Directory_CreateDirectory(ByVal Path As String)

    Sub File_SetCreationTimeUtc(ByVal Path As String, ByVal Time As Date)
    Sub Directory_SetCreationTimeUtc(ByVal Path As String, ByVal Time As Date)

    Function File_GetLength(ByVal Path As String) As Long 'Replaces `(New System.IO.FileInfo(SourceFile)).Length`
    Function File_GetAttributes(ByVal Path As String) As IO.FileAttributes
    Sub File_SetAttributes(ByVal Path As String, ByVal FileAttributes As IO.FileAttributes)
    Sub Directory_SetAttributes(ByVal Path As String, ByVal FileAttributes As IO.FileAttributes) 'Replaces `Dim DirInfo As New IO.DirectoryInfo(Source & Entry.Path) : DirInfo.Attributes = IO.FileAttributes.Normal`

    Function File_Exists(ByVal Path As String) As Boolean
    Function Directory_Exists(ByVal Path As String) As Boolean
    Function Directory_GetFiles(ByVal Path As String) As String()
    Function Directory_GetDirectories(ByVal Path As String) As String()
End Interface
#End If
