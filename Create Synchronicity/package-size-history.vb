Imports System

Module PackageSize
    Structure PackageInfo
        Dim Id As Integer
        Dim Name As String
        Dim Size As Long
        Shared GlobalRegex As String = "Create_Synchronicity(_Setup)?-r(\d+)(-DEBUG)?\.(zip|exe)"

        Sub New(ByVal Path As String)
            Name = IO.Path.GetFileNameWithoutExtension(Path)
            Size = (New System.IO.FileInfo(Path)).Length
            Id = CInt(Text.RegularExpressions.Regex.Replace(Name, GlobalRegex, "$2"))
        End Sub
    End Structure

    Sub Main()
        Dim ZipFiles As New List(Of PackageInfo)
        Dim DebugZipFiles As New List(Of PackageInfo)
        Dim SetupFiles As New List(Of PackageInfo)


        For Each File As String In IO.Directory.GetFiles("..\build\", "", IO.SearchOption.AllDirectories)
            Dim FName As String = IO.Path.GetFileName(File)

            If Text.RegularExpressions.Regex.IsMatch(FName, "Create_Synchronicity-r\d+\.zip") Then
                ZipFiles.Add(New PackageInfo(File))
            ElseIf Text.RegularExpressions.Regex.IsMatch(FName, "Create_Synchronicity-r\d+-DEBUG\.zip") Then
                DebugZipFiles.Add(New PackageInfo(File))
            ElseIf Text.RegularExpressions.Regex.IsMatch(FName, "Create_Synchronicity_SETUP-r\d+\.exe") Then
                SetupFiles.Add(New PackageInfo(File))
            End If
        Next

        Dim Print = Sub(Package As PackageInfo) Console.WriteLine("{0}	{1}", Package.Id, Package.Size)

        Console.WriteLine("== Zip Files ==")
        ZipFiles.ForEach(Print)
        Console.WriteLine("== DEBUG Zip Files ==")
        ZipFiles.ForEach(Print)
        Console.WriteLine("== Setup Files ==")
        ZipFiles.ForEach(Print)
    End Sub
End Module