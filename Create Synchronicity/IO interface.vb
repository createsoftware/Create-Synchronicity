#If 0 Then
Imports System.IO
Imports System.Net

Public Class GenericFS
    'Needed?
End Class

Public Class FtpFS
    Inherits GenericFS
    Implements FileSystem

    Private RequestHandler As FtpWebRequest

    Private HostName As String
    Private Port As Integer
    Private Passive As Boolean
    Private UseSsl As Boolean
    Private UserName As String
    Private Password As String

    'TODO: Handle

    'Blank UserName & blank password: Anonymous connection.
    Sub New(ByVal _HostName As String, ByVal _Port As Integer, ByVal _Passive As Boolean, ByVal _UseSsl As Boolean, ByVal _UserName As String, ByVal _Password As String)
        HostName = _HostName
        Port = _Port
        Passive = _Passive
        UseSsl = _UseSsl
        UserName = _UserName
        Password = _Password
    End Sub

    Private Sub CreateRequest(ByVal Path As String)
        RequestHandler = FtpWebRequest.Create(HostName & Path) 'Create("ftp://" & UserName & ":" & Password & "@" & HostName & ":" & Port)
        RequestHandler.KeepAlive = True
        RequestHandler.UseBinary = True
        If Not (UserName = "" & Password = "") Then
            Static Credentials As NetworkCredential = New NetworkCredential(UserName, Password)
            RequestHandler.Credentials = Credentials
        End If
    End Sub

    Public Sub Directory_CreateDirectory(ByVal path As String) Implements FileSystem.Directory_CreateDirectory
        CreateRequest(path)
        RequestHandler.Method = WebRequestMethods.Ftp.MakeDirectory
        RequestHandler.GetResponse().GetResponseStream.Close()
    End Sub

    Public Sub Directory_Delete(ByVal path As String) Implements FileSystem.Directory_Delete
        CreateRequest(path)
        RequestHandler.Method = WebRequestMethods.Ftp.RemoveDirectory
        RequestHandler.GetResponse().GetResponseStream.Close()
    End Sub

    Public Function Directory_Exists(ByVal path As String) As Boolean Implements FileSystem.Directory_Exists
        CreateRequest(path)
        Try
            RequestHandler.Method = WebRequestMethods.Ftp.ListDirectory
            Return True
        Catch Ex As WebException
            If Ex.Response IsNot Nothing AndAlso DirectCast(Ex.Response, FtpWebResponse).StatusCode = FtpStatusCode.ActionNotTakenFileUnavailable Then
                Return False
            Else
                Throw
            End If
        End Try
    End Function

    Public Function Directory_GetDirectories(ByVal path As String) As String() Implements FileSystem.Directory_GetDirectories
        CreateRequest(path)
        RequestHandler.Method = WebRequestMethods.Ftp.RemoveDirectory

        Dim ResponseReader As New StreamReader(RequestHandler.GetResponse().GetResponseStream())
        While Not ResponseReader.EndOfStream()
            Console.WriteLine(ResponseReader.ReadLine())
        End While
        ResponseReader.Close()
        Return New String() {"TODO!"}
    End Function

    Public Function Directory_GetFiles(ByVal path As String) As String() Implements FileSystem.Directory_GetFiles
        Return Directory_GetDirectories("") 'TODO!!
    End Function

    Public Sub Directory_SetAttributes(ByVal path As String, ByVal fileAttributes As System.IO.FileAttributes) Implements FileSystem.Directory_SetAttributes

    End Sub

    Public Sub Directory_SetCreationTimeUtc(ByVal path As String, ByVal creationTimeUtc As Date) Implements FileSystem.Directory_SetCreationTimeUtc

    End Sub

    Public ReadOnly Property DirectorySeparatorChar As String Implements FileSystem.DirectorySeparatorChar
        Get

        End Get
    End Property

    Public Sub File_Copy(ByVal sourceFileName As String, ByVal destFileName As String) Implements FileSystem.File_Copy

    End Sub

    Public Sub File_Delete(ByVal path As String) Implements FileSystem.File_Delete

    End Sub

    Public Function File_Exists(ByVal path As String) As Boolean Implements FileSystem.File_Exists

    End Function

    Public Function File_GetAttributes(ByVal path As String) As System.IO.FileAttributes Implements FileSystem.File_GetAttributes

    End Function

    Public Function File_GetLength(ByVal fileName As String) As Long Implements FileSystem.File_GetLength

    End Function

    Public Sub File_SetAttributes(ByVal path As String, ByVal fileAttributes As System.IO.FileAttributes) Implements FileSystem.File_SetAttributes

    End Sub

    Public Sub File_SetCreationTimeUtc(ByVal path As String, ByVal creationTimeUtc As Date) Implements FileSystem.File_SetCreationTimeUtc

    End Sub
End Class

Public Class NativeFS
    Inherits GenericFS
    Implements FileSystem

    Public Sub Directory_CreateDirectory(ByVal path As String) Implements FileSystem.Directory_CreateDirectory
        Directory.CreateDirectory(path)
    End Sub

    Public Sub Directory_Delete(ByVal path As String) Implements FileSystem.Directory_Delete
        Directory.Delete(path)
    End Sub

    Public Function Directory_Exists(ByVal path As String) As Boolean Implements FileSystem.Directory_Exists
        Return Directory.Exists(path)
    End Function

    Public Function Directory_GetDirectories(ByVal path As String) As String() Implements FileSystem.Directory_GetDirectories
        Return Directory.GetDirectories(path)
    End Function

    Public Function Directory_GetFiles(ByVal path As String) As String() Implements FileSystem.Directory_GetFiles
        Return Directory.GetFiles(path)
    End Function

    Public Sub Directory_SetAttributes(ByVal path As String, ByVal fileAttributes As System.IO.FileAttributes) Implements FileSystem.Directory_SetAttributes
        Dim DirInfo As New IO.DirectoryInfo(path)
        DirInfo.Attributes = fileAttributes
    End Sub

    Public Sub Directory_SetCreationTimeUtc(ByVal path As String, ByVal creationTimeUtc As Date) Implements FileSystem.Directory_SetCreationTimeUtc
        Directory.SetCreationTimeUtc(path, creationTimeUtc)
    End Sub

    Public ReadOnly Property DirectorySeparatorChar As String Implements FileSystem.DirectorySeparatorChar
        Get
            Return Path.DirectorySeparatorChar
        End Get
    End Property

    Public Sub File_Copy(ByVal sourceFileName As String, ByVal destFileName As String) Implements FileSystem.File_Copy
        File.Copy(sourceFileName, destFileName)
    End Sub

    Public Sub File_Delete(ByVal path As String) Implements FileSystem.File_Delete
        File.Delete(path)
    End Sub

    Public Function File_Exists(ByVal path As String) As Boolean Implements FileSystem.File_Exists
        Return File.Exists(path)
    End Function

    Public Function File_GetAttributes(ByVal path As String) As System.IO.FileAttributes Implements FileSystem.File_GetAttributes
        Return File.GetAttributes(path)
    End Function

    Public Function File_GetLength(ByVal fileName As String) As Long Implements FileSystem.File_GetLength
        Return (New System.IO.FileInfo(fileName)).Length
    End Function

    Public Sub File_SetAttributes(ByVal path As String, ByVal fileAttributes As System.IO.FileAttributes) Implements FileSystem.File_SetAttributes
        File.SetAttributes(path, fileAttributes)
    End Sub

    Public Sub File_SetCreationTimeUtc(ByVal path As String, ByVal creationTimeUtc As Date) Implements FileSystem.File_SetCreationTimeUtc
        File.SetCreationTimeUtc(path, creationTimeUtc)
    End Sub
End Class

Public Interface FileSystem
    ReadOnly Property DirectorySeparatorChar As String

    Sub File_Copy(ByVal sourceFileName As String, ByVal destFileName As String)
    Sub File_Delete(ByVal path As String)
    Sub File_SetCreationTimeUtc(ByVal path As String, ByVal creationTimeUtc As Date)
    Function File_Exists(ByVal path As String) As Boolean
    Function File_GetLength(ByVal fileName As String) As Long 'Replaces `(New System.IO.FileInfo(SourceFile)).Length`
    Function File_GetAttributes(ByVal path As String) As IO.FileAttributes
    Sub File_SetAttributes(ByVal path As String, ByVal fileAttributes As IO.FileAttributes)

    Sub Directory_Delete(ByVal path As String)
    Sub Directory_CreateDirectory(ByVal path As String)
    Sub Directory_SetCreationTimeUtc(ByVal path As String, ByVal creationTimeUtc As Date)
    Sub Directory_SetAttributes(ByVal path As String, ByVal fileAttributes As IO.FileAttributes) 'Replaces `Dim DirInfo As New IO.DirectoryInfo(Source & Entry.path) : DirInfo.Attributes = IO.FileAttributes.Normal`
    Function Directory_Exists(ByVal path As String) As Boolean
    Function Directory_GetFiles(ByVal path As String) As String()
    Function Directory_GetDirectories(ByVal path As String) As String()
End Interface
#End If
