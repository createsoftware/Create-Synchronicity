Imports System
Imports System.Collections.Generic
Imports System.Text.RegularExpressions

Module PackageSize
    Structure PackageInfo
        Dim Id As Integer
        Dim Name As String
        Dim Size As Long
        Shared GlobalRegex As String = "Create_Synchronicity(_Setup)?-r(\d+)(-DEBUG)?"

        Sub New(ByVal Path As String)
            Name = IO.Path.GetFileNameWithoutExtension(Path)
            Size = (New System.IO.FileInfo(Path)).Length
            Id = CInt(Regex.Replace(Name, GlobalRegex, "$2"))
        End Sub
    End Structure

    Sub Main()
        Dim ZipFiles As New List(Of PackageInfo)
        Dim DebugZipFiles As New List(Of PackageInfo)
        Dim SetupFiles As New List(Of PackageInfo)

        ' VB.Net executable directory: System.AppDomain.CurrentDomain.BaseDirectory
        For Each File As String In IO.Directory.GetFiles(System.AppDomain.CurrentDomain.BaseDirectory, "*.*", IO.SearchOption.AllDirectories)
            Dim FName As String = IO.Path.GetFileName(File)

            If Regex.IsMatch(FName, "Create_Synchronicity-r\d+\.zip") Then
                ZipFiles.Add(New PackageInfo(File))
            ElseIf Regex.IsMatch(FName, "Create_Synchronicity-r\d+-DEBUG\.zip") Then
                DebugZipFiles.Add(New PackageInfo(File))
            ElseIf Regex.IsMatch(FName, "Create_Synchronicity_Setup-r\d+\.exe") Then
                SetupFiles.Add(New PackageInfo(File))
            End If
        Next

        Dim PrintRoutine = Sub(Package As PackageInfo) Console.WriteLine("{0}	{1}", Package.Id, Package.Size)
        Dim Comparer As New System.Comparison(Of PackageInfo)(Function(PI1 As PackageInfo, PI2 As PackageInfo) PI1.Id < PI2.Id)

        ZipFiles.Sort(Comparer)
        DebugZipFiles.Sort(Comparer)
        SetupFiles.Sort(Comparer)

        Console.WriteLine("Rev	Size    (Zip)")
        ZipFiles.ForEach(PrintRoutine)
        Console.WriteLine("Rev	Size    (DEBUG Zip)")
        DebugZipFiles.ForEach(PrintRoutine)
        Console.WriteLine("Rev	Size    Setup")
        SetupFiles.ForEach(PrintRoutine)
    End Sub
End Module