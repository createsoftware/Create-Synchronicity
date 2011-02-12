'This file is part of Create Synchronicity.
'
'Create Synchronicity is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
'Create Synchronicity is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with Create Synchronicity.  If not, see <http://www.gnu.org/licenses/>.
'Created by:	Clément Pit--Claudel.
'Web site:		http://synchronicity.sourceforge.net.

Public Class GZipCompressor
    Implements Create_Synchronicity.Compressor
    Sub CompressFile(ByVal SourceFile As String, ByVal DestFile As String, ByRef Progress As Long) Implements Create_Synchronicity.Compressor.CompressFile
        Using Input As New IO.FileStream(SourceFile, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)
            Using Output As New IO.FileStream(DestFile, IO.FileMode.Create)
                Using Compress As New zlib.ZOutputStream(Output, zlib.zlibConst.Z_DEFAULT_COMPRESSION)
                    'DONE: Figure out the right buffer size.
                    Dim Buffer(524228) As Byte '281 739 264 Bytes -> 268435456:27s ; 67108864:32s ; 2097152:29s ; 524228:27s ; 4:32s
                    Dim ReadBytes As Integer = 0

                    While True
                        ReadBytes = Input.Read(Buffer, 0, Buffer.Length)
                        If ReadBytes <= 0 Then Exit While
                        Compress.Write(Buffer, 0, ReadBytes)
                        Progress += ReadBytes
                    End While
                End Using
            End Using
        End Using
    End Sub

#If 0 Then
    'Broken code: Microsoft didn't implement GZip correctly.
    Sub CompressFile(ByVal SourceFile As String, ByVal DestFile As String)
        Using Input As New IO.FileStream(SourceFile, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)
            Using outFile As IO.FileStream = IO.File.Create(DestFile)
                Using Compress As IO.Compression.GZipStream = New IO.Compression.GZipStream(outFile, IO.Compression.CompressionMode.Compress)
                    'DONE: Figure out the right buffer size.
                    Dim Buffer(524228) As Byte '281 739 264 Bytes -> 268435456:27s ; 67108864:32s ; 2097152:29s ; 524228:27s ; 4:32s
                    Dim ReadBytes As Integer = 0

                    While True
                        ReadBytes = Input.Read(Buffer, 0, Buffer.Length)
                        If ReadBytes <= 0 Then Exit While
                        Compress.Write(Buffer, 0, ReadBytes)
                        Status.BytesCopied += ReadBytes
                    End While
                End Using
            End Using
        End Using
    End Sub
#End If
End Class
