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

        Updated.Remove("")
        NewVars.Remove("")
        DelVars.Remove("")

        Dim TODOList As New IO.StreamWriter(Application.StartupPath & "\" & "TODO.txt")

        For Each File As String In Languages
            Try
                Dim Reader As New IO.StreamReader(File, System.Text.Encoding.UTF8)
                Dim Output As New List(Of String)
                Dim FName = IO.Path.GetFileNameWithoutExtension(File)

                Dim TODO As Integer = 0

                While Reader.Peek() > 0
                    Dim Line As String = Reader.ReadLine
                    If Line.StartsWith("#") Then
                        Output.Add(Line)
                    Else
                        Dim Contents() As String = Line.Split("=")

                        Try
                            Dim Key As String = If(Contents(0).StartsWith("->"), Contents(0).Remove(0, "->".Length), Contents(0))

                            If Not DelVars.Contains(Key) Then
                                If Key <> Contents(0) Or Updated.Contains(Key) Then
                                    TODO += 1 : Output.Add("->" & Key & "=" & Contents(1))
                                    Console.WriteLine("    " & FName & ": " & Key & " should be updated")
                                Else
                                    Output.Add(Line)
                                End If
                            Else
                                Console.WriteLine("    " & FName & ": " & Key & " has been removed")
                            End If
                        Catch ex As Exception
                Console.WriteLine("Exception in " & FName & " at line " & Line)
            End Try
                    End If
                End While
                Reader.Close()

                For Each NewString As String In NewVars
                    Output.Add("->" & NewString & "=")
                Next
                TODO += NewVars.Count

                Dim LanguageName As String = File.Remove(File.LastIndexOf(".")).Substring(File.LastIndexOf("\") + 1)
                TODOList.WriteLine(LanguageName & ":" & TODO)

                Dim Writer As New IO.StreamWriter(File, False, System.Text.Encoding.UTF8)
                For Each Line As String In Output
                    Writer.WriteLine(Line)
                Next
                Writer.Close()

                Console.WriteLine("Updated " & File)
            Catch Ex As Exception
                Console.WriteLine("Exception " & Ex.Message & Microsoft.VisualBasic.vbNewLine & Ex.StackTrace)
            End Try
        Next

        TODOList.Close()
        Console.ReadLine()
    End Sub
End Module
