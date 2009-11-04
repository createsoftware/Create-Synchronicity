Imports Microsoft.VisualBasic

Module FileLen_Speed_Test
    Const FNAME As String = "C:\test.cab"

    Sub Main()
        Try
            If IO.File.Exists(FNAME) Then
                Dim CurTime As Date

                CurTime = Microsoft.VisualBasic.DateAndTime.Now
                For TestId As Integer = 0 To 50000
                    Dim FSize As Long = (New System.IO.FileInfo(FNAME)).Length
                Next
                Console.WriteLine("Scanning file size through Syst.IO 50000 times took {0} ms", (Microsoft.VisualBasic.DateAndTime.Now - CurTime).TotalMilliseconds)

                CurTime = Microsoft.VisualBasic.DateAndTime.Now
                For TestId As Integer = 0 To 50000
                    Dim FSize As Long = My.Computer.FileSystem.GetFileInfo(FNAME).Length
                Next
                Console.WriteLine("Scanning file size through My.Comp 50000 times took {0} ms", (Microsoft.VisualBasic.DateAndTime.Now - CurTime).TotalMilliseconds)

                CurTime = Microsoft.VisualBasic.DateAndTime.Now
                For TestId As Integer = 0 To 50000
                    Dim FSize As Long = Microsoft.VisualBasic.FileSystem.FileLen(FNAME)
                Next
                Console.WriteLine("Scanning file size through FileLen 50000 times took {0} ms", (Microsoft.VisualBasic.DateAndTime.Now - CurTime).TotalMilliseconds)

                Console.ReadLine()
            End If
        Catch
        End Try
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
