'This file is part of Create Synchronicity.
'
'Create Synchronicity is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
'Create Synchronicity is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with Create Synchronicity.  If not, see <http://www.gnu.org/licenses/>.
'Created by:	Clément Pit--Claudel.
'Web site:		http://synchronicity.sourceforge.net.

Public Class LanguageHandler
    Private Shared Instance As LanguageHandler

    Protected Sub New()
        Strings = New Dictionary(Of String, String)
        Dim File As String = ConfigOptions.LanguageRootDir & "\" & ConfigOptions.GetProgramSetting(ConfigOptions.Language, "en") & ".lng"

        If Not IO.File.Exists(File) Then Exit Sub
        Dim Reader As New IO.StreamReader(File, Text.Encoding.UTF8)

        While Reader.Peek() > -1
            Dim Line As String = Reader.ReadLine

            If Line.StartsWith("#") Then Continue While

            Dim Pair() As String = Line.Split("=")
            If Pair.Length < 2 Then Continue While 'Invalid entry

            Try
                Strings.Add("\" & Pair(0), Pair(1).Replace("\n", Environment.NewLine))
            Catch Ex As Exception
                'TODO
            End Try
        End While
    End Sub

    Public Shared Function GetSingleton() As LanguageHandler
        If Instance Is Nothing Then Instance = New LanguageHandler()
        Return Instance
    End Function

    Dim Strings As Dictionary(Of String, String)

    Public Function Translate(ByVal Code As String)
        If Code = Nothing Then Return String.Empty
        Return If(Strings.ContainsKey(Code), Strings(Code), Code)
    End Function

    Public Sub TranslateControl(ByVal Ctrl As Control)
        For Each C As Control In Ctrl.Controls
            C.Text = Translate(C.Text)

            If TypeOf C Is ListView Then
                For Each H As ListViewGroup In CType(C, ListView).Groups
                    H.Header = Translate(H.Header)
                Next
            ElseIf TypeOf C Is Label Then
                CType(C, Label).Tag = Translate(CType(C, Label).Tag)
            ElseIf TypeOf C Is CheckBox Then
                CType(C, CheckBox).Tag = Translate(CType(C, CheckBox).Tag)
            ElseIf TypeOf C Is RadioButton Then
                CType(C, RadioButton).Tag = Translate(CType(C, RadioButton).Tag)
            End If

            TranslateControl(C)
        Next
    End Sub
End Class
