﻿'This file is part of Create Synchronicity.
'
'Create Synchronicity is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
'Create Synchronicity is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with Create Synchronicity.  If not, see <http://www.gnu.org/licenses/>.
'Created by:	Clément Pit--Claudel.
'Web site:		http://synchronicity.sourceforge.net.

Friend NotInheritable Class LanguageHandler
    Private Shared Singleton As LanguageHandler

    Structure LanguageInfo
        Dim LocalName As String
        Dim IsoLanguageName As String
    End Structure

    Private Sub New()
        ProgramConfig.LoadProgramSettings()
        IO.Directory.CreateDirectory(ProgramConfig.LanguageRootDir)

        Strings = New Dictionary(Of String, String)
        Dim DictFile As String = ProgramConfig.LanguageRootDir & ProgramSetting.DirSep & ProgramConfig.GetProgramSetting(Of String)(ProgramSetting.Language, ProgramSetting.DefaultLanguage) & ".lng"

        If Not IO.File.Exists(DictFile) Then DictFile = ProgramConfig.LanguageRootDir & ProgramSetting.DirSep & ProgramSetting.DefaultLanguage & ".lng"
        If Not IO.File.Exists(DictFile) Then
            Interaction.ShowMsg("No language file found!")
        Else
            Using Reader As New IO.StreamReader(DictFile, Text.Encoding.UTF8)
                While Not Reader.EndOfStream()
                    Dim Line As String = Reader.ReadLine()
                    If Line.StartsWith("#") OrElse (Not Line.Contains("=")) Then Continue While

                    Dim Pair() As String = Line.Split("=".ToCharArray(), 2)
                    Try
                        If Pair(0).StartsWith("->") Then Pair(0) = Pair(0).Remove(0, 2)
                        Strings.Add("\" & Pair(0), Pair(1).Replace("\n", Environment.NewLine))
                    Catch Ex As ArgumentException
                        Interaction.ShowDebug("Duplicate line in translation: " & Line)
                    End Try
                End While
            End Using
        End If
    End Sub

    Public Shared Function GetSingleton(Optional ByVal Reload As Boolean = False) As LanguageHandler
        If Reload Or (Singleton Is Nothing) Then Singleton = New LanguageHandler()
        Return Singleton
    End Function

    Dim Strings As Dictionary(Of String, String)

    Public Function Translate(ByVal Code As String, Optional ByVal DefaultValue As String = "") As String
        If Code = Nothing OrElse Code = "" Then Return DefaultValue
        Return If(Strings.ContainsKey(Code), Strings(Code), If(DefaultValue = "", Code, DefaultValue))
    End Function

    Public Sub TranslateControl(ByVal Ctrl As Control)
        If Ctrl Is Nothing Then Exit Sub

        'Add ; in tags so as to avoid errors when tag properties are split.
        Ctrl.Text = Translate(Ctrl.Text)
        TranslateControl(Ctrl.ContextMenuStrip)

        If TypeOf Ctrl Is ListView Then
            Dim List As ListView = DirectCast(Ctrl, ListView)

            For Each Group As ListViewGroup In List.Groups
                Group.Header = Translate(Group.Header)
            Next

            For Each Column As ColumnHeader In List.Columns
                Column.Text = Translate(Column.Text)
            Next

            For Each Item As ListViewItem In List.Items
                For Each SubItem As ListViewItem.ListViewSubItem In Item.SubItems
                    SubItem.Text = Translate(SubItem.Text)
                    SubItem.Tag = Translate(CStr(SubItem.Tag), ";")
                Next
            Next
        End If

        If TypeOf Ctrl Is ContextMenuStrip Then
            Dim ContextMenu As ContextMenuStrip = DirectCast(Ctrl, ContextMenuStrip)
            For Each Item As ToolStripItem In ContextMenu.Items
                Item.Text = Translate(Item.Text)
                Item.Tag = Translate(CStr(Item.Tag), ";")
            Next
        End If

        Ctrl.Tag = Translate(CStr(Ctrl.Tag), ";")
        For Each ChildCtrl As Control In Ctrl.Controls
            TranslateControl(ChildCtrl)
        Next
    End Sub

    Public Shared Sub FillLanguageListBox(ByVal LanguagesComboBox As ComboBox)
        Dim LanguageProps As New Dictionary(Of String, LanguageHandler.LanguageInfo)

        If IO.File.Exists(ProgramConfig.LocalNamesFile) Then
            Using PropsReader As New IO.StreamReader(ProgramConfig.LocalNamesFile)
                While Not PropsReader.EndOfStream
                    Dim CurLanguage() As String = PropsReader.ReadLine.Split(";".ToCharArray)
                    Try
                        LanguageProps.Add(CurLanguage(0), New LanguageHandler.LanguageInfo With {.LocalName = CurLanguage(1), .IsoLanguageName = CurLanguage(2)})
                    Catch Ex As IndexOutOfRangeException
                        Interaction.ShowMsg("Invalid local-names file.")
                    End Try
                End While
            End Using
        End If

        Dim SystemLanguageIndex As Integer = -1
        Dim ProgramLanguageIndex As Integer = -1
        Dim DefaultLanguageIndex As Integer = -1
        Dim CurProgramLanguage As String = ProgramConfig.GetProgramSetting(Of String)(ProgramSetting.Language, "")
        Dim LanguageCode As String = Globalization.CultureInfo.InstalledUICulture.TwoLetterISOLanguageName 'FIXME: Currently traditional and simplified chinese have the same codes. 

        LanguagesComboBox.Items.Clear()
        For Each File As String In IO.Directory.GetFiles(ProgramConfig.LanguageRootDir, "*.lng")
            Dim EnglishLanguageName As String = IO.Path.GetFileNameWithoutExtension(File)

            If EnglishLanguageName = CurProgramLanguage Then ProgramLanguageIndex = LanguagesComboBox.Items.Count
            If EnglishLanguageName = ProgramSetting.DefaultLanguage Then DefaultLanguageIndex = LanguagesComboBox.Items.Count

            If LanguageProps.ContainsKey(EnglishLanguageName) Then
                Dim LangInfo As LanguageHandler.LanguageInfo = LanguageProps(EnglishLanguageName)
                If LangInfo.IsoLanguageName = LanguageCode Then SystemLanguageIndex = LanguagesComboBox.Items.Count
                LanguagesComboBox.Items.Add(String.Format("{0} - {1} ({2})", EnglishLanguageName, LangInfo.LocalName, LangInfo.IsoLanguageName))
            Else
                LanguagesComboBox.Items.Add(EnglishLanguageName)
            End If
        Next

        ProgramConfig.LoadProgramSettings()
        If ProgramLanguageIndex <> -1 Then
            LanguagesComboBox.SelectedIndex = ProgramLanguageIndex
        ElseIf SystemLanguageIndex <> -1 Then
            LanguagesComboBox.SelectedIndex = SystemLanguageIndex
        ElseIf DefaultLanguageIndex <> -1 Then
            LanguagesComboBox.SelectedIndex = DefaultLanguageIndex
        ElseIf LanguagesComboBox.Items.Count > 0 Then
            LanguagesComboBox.SelectedIndex = 0
        End If
    End Sub
End Class
