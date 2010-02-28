'This file is part of Create Synchronicity.
'
'Create Synchronicity is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
'Create Synchronicity is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with Create Synchronicity.  If not, see <http://www.gnu.org/licenses/>.
'Created by:	Clément Pit--Claudel.
'Web site:		http://synchronicity.sourceforge.net.

Imports Microsoft.VisualBasic

Module NTFSToFatCopy_Test
    Const NTFS_DRIVE_LETTER As String = "C"
    Const FAT_DRIVE_LETTER As String = "G"

    Dim NTFSPath As String = NTFS_DRIVE_LETTER & ":\testfiles\"
    Dim FATPath As String = FAT_DRIVE_LETTER & ":\testfiles\"

    Sub Main()
        Dim BaseTime As Date = #1/1/2000#

        Try
            If IO.Directory.Exists(NTFSPath) Then IO.Directory.Delete(NTFSPath, True)
            If IO.Directory.Exists(FATPath) Then IO.Directory.Delete(FATPath, True)
            IO.Directory.CreateDirectory(NTFSPath)
            IO.Directory.CreateDirectory(FATPath)

            Dim NewTime As Date = BaseTime
            For AddedMilliseconds As Integer = 0 To 2500 Step 100
                NewTime = BaseTime.AddMilliseconds(AddedMilliseconds)
                Dim fName As String = NewTime.Ticks.ToString
                IO.File.Create(NTFSPath & fName).Close()
                IO.File.SetLastWriteTime(NTFSPath & fName, NewTime)
                IO.File.Copy(NTFSPath & fName, FATPath & fName)

                Dim NTFSLastAccessTime As Date = IO.File.GetLastWriteTime(NTFSPath & fName)
                Dim FATLastAccessTime As Date = IO.File.GetLastWriteTime(FATPath & fName)

                Console.WriteLine("{0}s and {1}ms	-> {2}s and {3}ms", _
                                   NTFSLastAccessTime.Second, NTFSLastAccessTime.Millisecond, _
                                   FATLastAccessTime.Second, FATLastAccessTime.Millisecond)

            Next

            Console.ReadLine()
        Catch
        End Try
    End Sub
End Module
