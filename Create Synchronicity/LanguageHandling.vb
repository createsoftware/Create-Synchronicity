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
        LoadProgramSettings()

        Strings = New Dictionary(Of String, String)
        Dim DictFile As String = ConfigOptions.LanguageRootDir & "\" & ConfigOptions.GetProgramSetting(ConfigOptions.Language, ConfigOptions.DefaultLanguage) & ".lng"

        If Not IO.File.Exists(DictFile) Then DictFile = ConfigOptions.LanguageRootDir & "\" & ConfigOptions.DefaultLanguage & ".lng"
        If Not IO.File.Exists(DictFile) Then MessageBox.Show("No language file found!")

        Dim Reader As New IO.StreamReader(DictFile, Text.Encoding.UTF8)

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

    Public Function Translate(ByVal Code As String, Optional ByVal Default_Value As String = "")
        If Code = Nothing OrElse Code = String.Empty Then Return Default_Value
        Return If(Strings.ContainsKey(Code), Strings(Code), Code)
    End Function

    Public Sub TranslateControl(ByVal Ctrl As Control)
        If Ctrl Is Nothing Then Exit Sub

        'Add ; in tags so as to avoid errors when tag properties are split.
        Ctrl.Text = Translate(Ctrl.Text)
        TranslateControl(Ctrl.ContextMenuStrip)

        If TypeOf Ctrl Is ListView Then
            For Each Group As ListViewGroup In CType(Ctrl, ListView).Groups
                Group.Header = Translate(Group.Header)
            Next

            For Each Column As ColumnHeader In CType(Ctrl, ListView).Columns
                Column.Text = Translate(Column.Text)
            Next

            For Each Item As ListViewItem In CType(Ctrl, ListView).Items
                For Each SubItem As ListViewItem.ListViewSubItem In Item.SubItems
                    SubItem.Text = Translate(SubItem.Text)
                    SubItem.Tag = Translate(SubItem.Tag, ";")
                Next
            Next

        ElseIf TypeOf Ctrl Is ContextMenuStrip Then
            For Each Item As ToolStripItem In CType(Ctrl, ContextMenuStrip).Items
                Item.Text = Translate(Item.Text)
            Next

        ElseIf TypeOf Ctrl Is Button Then
            CType(Ctrl, Button).Tag = Translate(CType(Ctrl, Button).Tag, ";")

        ElseIf TypeOf Ctrl Is Label Then
            CType(Ctrl, Label).Tag = Translate(CType(Ctrl, Label).Tag, ";")

        ElseIf TypeOf Ctrl Is CheckBox Then
            CType(Ctrl, CheckBox).Tag = Translate(CType(Ctrl, CheckBox).Tag, ";")

        ElseIf TypeOf Ctrl Is RadioButton Then
            CType(Ctrl, RadioButton).Tag = Translate(CType(Ctrl, RadioButton).Tag, ";")
        End If

        For Each ChildCtrl As Control In Ctrl.Controls
            TranslateControl(ChildCtrl)
        Next
    End Sub
End Class
