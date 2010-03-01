'This file is part of Create Synchronicity.
'
'Create Synchronicity is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
'Create Synchronicity is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with Create Synchronicity.  If not, see <http://www.gnu.org/licenses/>.
'Created by:	Clément Pit--Claudel.
'Web site:		http://synchronicity.sourceforge.net.

Imports System
Imports System.Windows.Forms
Imports System.Collections.Generic

Imports Microsoft.VisualBasic

Module Update_Languages
    Sub Main()
        Dim Languages As New List(Of String)

        Languages.AddRange(IO.Directory.GetFiles(Application.StartupPath, "*.lng"))

        Dim Input As String
        Console.WriteLine("Please input a space-separated list of all updated strings")
        Input = Console.ReadLine() : Dim Updated As New List(Of String)(Input.Split(" "c))

        Console.WriteLine("Please input a space-separated list of all added strings")
        Input = Console.ReadLine() : Dim NewVars As New List(Of String)(Input.Split(" "c))

        Console.WriteLine("Please input a space-separated list of all removed strings")
        Input = Console.ReadLine() : Dim DelVars As New List(Of String)(Input.Split(" "c))

        For Each File As String In Languages
            Try
                Dim Reader As New IO.StreamReader(File, System.Text.Encoding.UTF8)
                Dim Output As New System.Text.StringBuilder

                While Reader.Peek() > 0
                    Dim Line As String = Reader.ReadLine
                    If Line.StartsWith("#") Then
                        Try
                        Output.AppendLine(Line)
						Catch Ex As Exception
							Console.WriteLine("Wooops " & Ex.Message & Microsoft.VisualBasic.vbNewLine & Ex.StackTrace)
						End Try
                    Else
                        Dim Contents() As String = Line.Split("=")

                        Try
                            If Contents(0).StartsWith("->") Then
                                Contents(0) = Contents(0).Remove(0, "->".Length)
                            End If

                            If Updated.Contains(Contents(0)) Then
                                Output.AppendLine("->" & Line)
                            ElseIf Not DelVars.Contains(Contents(0)) Then
                                Output.AppendLine(Line)
                            End If
                        Catch ex As Exception
                            Console.WriteLine("Exception in " & File & " at line " & Line)
                        End Try
                    End If
                End While
                Reader.Close()

                For Each NewString As String In NewVars
                    Output.AppendLine("->" & NewString & "=")
                Next

                My.Computer.FileSystem.WriteAllText(File, Output.ToString(), False, System.Text.Encoding.UTF8)
                Console.WriteLine("Updated " & File)
            Catch Ex As Exception
                Console.WriteLine("Exception " & Ex.Message & Microsoft.VisualBasic.vbNewLine & Ex.StackTrace)
            End Try
        Next

        Console.ReadLine()
    End Sub
End Module
