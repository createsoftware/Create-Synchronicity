'This file is part of Create Synchronicity.
'
'Create Synchronicity is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
'Create Synchronicity is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with Create Synchronicity.  If not, see <http://www.gnu.org/licenses/>.
'Created by:	Clément Pit--Claudel.
'Web site:		http://synchronicity.sourceforge.net.

Imports System
Imports Microsoft.VisualBasic
Imports System.Collections.Generic 

Module FileLen_Speed_Test
    Const DIR As String = "C:\"

    Dim Rnd As New Random()
    Dim SW As New Diagnostics.Stopwatch()

    Sub Main()
        Try
            'FileSize()
            Concat()
        Catch Ex As Exception
            Console.WriteLine(Ex.ToString())
        End Try
        Console.ReadLine()
    End Sub

    Sub FileSize()
        Dim FileName As String = IO.Path.GetRandomFileName()
        Dim Path As String = IO.Path.Combine(DIR, FileName)

        Dim Count As Integer = 1 << 25
        Dim FileBytes(Count) As Byte
        Rnd.NextBytes(FileBytes)

        Using FS As New IO.FileStream(Path, IO.FileMode.CreateNew)
            FS.Write(FileBytes, 0, Count)
        End Using

        SW.Reset() : SW.Start()
        For TestId As Integer = 1 To 50000
            Dim FSize As Long = (New System.IO.FileInfo(Path)).Length
        Next
        SW.Stop()
        Console.WriteLine("Scanning file size using System.IO.FileInfo 50000 times took {0} ms", SW.ElapsedMilliseconds)

        SW.Reset() : SW.Start()
        For TestId As Integer = 1 To 50000
            Dim FSize As Long = My.Computer.FileSystem.GetFileInfo(Path).Length
        Next
        SW.Stop()
        Console.WriteLine("Scanning file size using My.Computer.FileSystem.GetFileInfo 50000 times took {0} ms", SW.ElapsedMilliseconds)

        SW.Reset() : SW.Start()
        For TestId As Integer = 1 To 50000
            Dim FSize As Long = Microsoft.VisualBasic.FileSystem.FileLen(Path)
        Next
        SW.Stop()
        Console.WriteLine("Scanning file size using Microsoft.VisualBasic.FileSystem.FileLen 50000 times took {0} ms", SW.ElapsedMilliseconds)

        IO.File.Delete(Path)
    End Sub

    Sub Concat()
		Dim Elems As New List(Of String)
		For Id As Integer = 1 To 10
			Elems.Add(System.Guid.NewGuid.ToString)
		Next
		Dim Strings() As String = Elems.ToArray()

#If 0
        SW.Reset() : SW.Start()
        Dim Str1 As String = ""
        For Id As Integer = 0 To 5000 - 1
            Str1 = Str1 & Strings(Id)
			If Id Mod 1000 = 0 then Console.WriteLine(Id)
        Next
        SW.Stop()
        Console.WriteLine("Running 5000000 concatenations of 6 strings using & took {0} ms", SW.ElapsedMilliseconds)
#End If
		
        SW.Reset() : SW.Start()
        Dim Str2 As String
		For TestId As Long = 1 To 50000000
        	Str2 = String.Concat(Strings)
        Next
        SW.Stop()
        Console.WriteLine("Concatenating 5000 strings, 5000000 times using String.Concat took {0} ms", SW.ElapsedMilliseconds)

        SW.Reset() : SW.Start()
        Dim Str3 As String
		For TestId As Long = 1 To 50000000
		    Str3 = String.Join("", Strings)
        Next
		SW.Stop()
        Console.WriteLine("Concatenating 5000 strings, 5000000 times using String.Join took {0} ms", SW.ElapsedMilliseconds)
    End Sub

    'Results :
    ' Ran in different orders to check whether some buffering or something was done
    ' Scanning file size through FileLen 50000 times took 1906,25 ms
    ' Scanning file size through My.Comp 50000 times took 7343,75 ms
    ' Scanning file size through Syst.IO 50000 times took 953,125 ms
    '
    ' Scanning file size through Syst.IO 50000 times took 968,75 ms
    ' Scanning file size through My.Comp 50000 times took 7375 ms
    ' Scanning file size through FileLen 50000 times took 1890,625 ms
    ' 
    ' System.Io.FileInfo is the best.
End Module
