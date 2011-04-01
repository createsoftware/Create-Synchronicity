'This file is part of Create Synchronicity.
'
'Create Synchronicity is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
'Create Synchronicity is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with Create Synchronicity.  If not, see <http://www.gnu.org/licenses/>.
'Created by:	Clément Pit--Claudel.
'Web site:		http://synchronicity.sourceforge.net.

Structure ErrorItem
    Dim Ex As Exception
    Dim Details As String

    Sub New(ByVal _Ex As Exception, ByVal _Details As String)
        Ex = _Ex : Details = _Details
    End Sub
End Structure

Structure LogItem
    Dim Item As SyncingItem
    Dim Side As SideOfSource
    Dim Success As Boolean
    'Dim ErrorId As Integer

    Sub New(ByVal _Item As SyncingItem, ByVal _Side As SideOfSource, ByVal _Success As Boolean) ', Optional ByVal _ErrorId As Integer = -1)
        Item = _Item : Side = _Side : Success = _Success ' : ErrorId = _ErrorId
    End Sub
End Structure

Class LogHandler
    Dim LogName As String
    Public Errors As List(Of ErrorItem)
    Public Log As List(Of LogItem)
#If DEBUG Then
    Public DebugInfo As List(Of String)
#End If

    Private Disposed As Boolean

    Sub New(ByVal _LogName As String)
        IO.Directory.CreateDirectory(ProgramConfig.LogRootDir)

        Disposed = False
        LogName = _LogName
        Errors = New List(Of ErrorItem)
        Log = New List(Of LogItem)

#If DEBUG Then
        DebugInfo = New List(Of String)
#End If
    End Sub

    Sub HandleError(ByVal Ex As Exception, Optional ByVal Details As String = "")
        If TypeOf Ex Is Threading.ThreadAbortException Then Exit Sub
        Errors.Add(New ErrorItem(Ex, Details))
    End Sub

    Sub LogAction(ByVal Item As SyncingItem, ByVal Side As SideOfSource, ByVal Success As Boolean)
        Log.Add(New LogItem(Item, Side, Success))
    End Sub

#If DEBUG Then
    Sub LogInfo(ByVal Info As String)
        DebugInfo.Add(Info)
    End Sub
#End If

    Private Sub OpenHTMLHeaders(ByRef LogW As IO.StreamWriter)
        If Not (ConfigOptions.Debug Or ProgramConfig.GetProgramSetting(Of Boolean)(ConfigOptions.TextLogs, False)) Then
            LogW.WriteLine("<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.1//EN"" ""http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd"">")
            LogW.WriteLine("<html xmlns=""http://www.w3.org/1999/xhtml"" xml:lang=""en"" encoding=""utf-8"">")
            LogW.WriteLine("	<head>")
            LogW.WriteLine("		<title>Create Synchronicity - Log for " & LogName & "</title>")
            LogW.WriteLine("		<meta http-equiv=""Content-Type"" content=""text/html;charset=utf-8"" />")
            LogW.WriteLine("		<style type=""text/css"">")
            LogW.WriteLine("			body {")
            LogW.WriteLine("				font-family: verdana, courier;")
            LogW.WriteLine("				font-size: 0.8em;")
            LogW.WriteLine("				margin: auto;")
            LogW.WriteLine("				width: 80%;")
            LogW.WriteLine("			}")
            LogW.WriteLine("			table {")
            LogW.WriteLine("				border-collapse: collapse;")
            LogW.WriteLine("				width: 100%;")
            LogW.WriteLine("			}")
            LogW.WriteLine("			table tr td:nth-child(3n+2) {word-break: break-all;}")
            LogW.WriteLine("			th, td {")
            LogW.WriteLine("				min-width: 5em;")
            LogW.WriteLine("				border: solid grey;")
            LogW.WriteLine("				border-width: 1px 0 0 0;")
            LogW.WriteLine("				padding: 1em;")
            LogW.WriteLine("			}")
            LogW.WriteLine("		</style>")
            LogW.WriteLine("	</head>")
            LogW.WriteLine("	<body>")
        End If

        LogW.WriteLine(String.Format(Translation.Translate("\LOG_TITLE"), LogName))
    End Sub

    Private Sub CloseHTMLHeaders(ByRef LogW As IO.StreamWriter)
        If ConfigOptions.Debug Or ProgramConfig.GetProgramSetting(Of Boolean)(ConfigOptions.TextLogs, False) Then
            LogW.WriteLine()
        Else
            LogW.WriteLine("	</body>")
            LogW.WriteLine("</html>")
        End If
    End Sub

    Private Sub PutFormatted(ByVal Title As String, ByVal Contents As String(), ByRef LogW As IO.StreamWriter)
        If ConfigOptions.Debug Or ProgramConfig.GetProgramSetting(Of Boolean)(ConfigOptions.TextLogs, False) Then
            LogW.WriteLine(Title & "	" & String.Join("	", Contents))
        Else
            LogW.WriteLine("<tr>")
            LogW.WriteLine("	<td>" & Title & "</td>")
            For Each Cell As String In Contents
                LogW.WriteLine("	<td>" & Cell & "</td>")
            Next
            LogW.WriteLine("</tr>")
        End If
    End Sub

    Private Sub PutHTML(ByVal LogWriter As IO.StreamWriter, ByVal Line As String)
        If Not (ConfigOptions.Debug Or ProgramConfig.GetProgramSetting(Of Boolean)(ConfigOptions.TextLogs, False)) Then LogWriter.WriteLine(Line)
    End Sub

    Sub SaveAndDispose(ByVal Left As String, ByVal Right As String, Optional ByVal SpecialMsg As String = Nothing)
        If Disposed Then Exit Sub
        Disposed = True

        Try
            Dim LogWriter As IO.StreamWriter
            Dim NewLog As Boolean = Not IO.File.Exists(ProgramConfig.GetLogPath(LogName))

            'Load the contents of the previous log, excluding the closing tags
            Dim ArchivesCount As Integer = ProgramConfig.GetProgramSetting(Of Integer)(ConfigOptions.MaxLogEntries, 7)
            Dim Archives As New List(Of Text.StringBuilder)
            Archives.Add(New Text.StringBuilder())

            If Not NewLog Then
                Dim LogReader As New IO.StreamReader(ProgramConfig.GetLogPath(LogName))
                While Not LogReader.EndOfStream
                    Dim Line As String = LogReader.ReadLine()
                    If Line.Contains("<h2>") Then
                        Archives.Add(New Text.StringBuilder())
                        If Archives.Count > ArchivesCount Then Archives.RemoveAt(0) 'Don't store more than ConfigOptions.MaxLogEntries in memory
                    End If
                    If Not Line.Contains("<h1>") And Not Line.Contains("</body>") And Not Line.Contains("</html>") Then Archives(Archives.Count - 1).AppendLine(Line)
                End While
                LogReader.Close()
            End If

            'This erases log contents.
            LogWriter = New IO.StreamWriter(ProgramConfig.GetLogPath(LogName), False, Text.Encoding.UTF8)

            OpenHTMLHeaders(LogWriter)
            For LogId As Integer = 0 To Archives.Count - 1
                LogWriter.Write(Archives(LogId).ToString)
            Next

            Try
                'Log format: <h2>, then two <table>s (info, errors)
                LogWriter.WriteLine("<h2>" & Date.Now.ToString("g") & "</h2>")

                PutHTML(LogWriter, "<p>")
                LogWriter.WriteLine(String.Format("Create Synchronicity v{0}", Application.ProductVersion))
                PutHTML(LogWriter, "<br />")
                LogWriter.WriteLine(String.Format("{0}: {1}", Translation.Translate("\LEFT"), Left))
                PutHTML(LogWriter, "<br />")
                LogWriter.WriteLine(String.Format("{0}: {1}", Translation.Translate("\RIGHT"), Right))
                If SpecialMsg IsNot Nothing Then
                    PutHTML(LogWriter, "<br />")
                    LogWriter.WriteLine(SpecialMsg)
                End If
                PutHTML(LogWriter, "</p>")

#If DEBUG Then
                For Each Info As String In DebugInfo
                    PutFormatted("Info", New String() {Info}, LogWriter)
                Next
#End If

                PutHTML(LogWriter, "<table>")
                For Each Record As LogItem In Log
                    PutFormatted(If(Record.Success, Translation.Translate("\SUCCEDED"), Translation.Translate("\FAILED")), New String() {Record.Item.FormatType(), Record.Item.FormatAction(), Record.Item.FormatDirection(Record.Side), Record.Item.Path}, LogWriter)
                Next
                PutHTML(LogWriter, "</table>")

                PutHTML(LogWriter, "<table>")
                For Each Err As ErrorItem In Errors
                    PutFormatted(Translation.Translate("\ERROR"), New String() {Err.Details, Err.Ex.Message, Err.Ex.StackTrace.Replace(Microsoft.VisualBasic.vbNewLine, "\n")}, LogWriter)
                Next
                PutHTML(LogWriter, "</table>")

                CloseHTMLHeaders(LogWriter)

            Catch Ex As Threading.ThreadAbortException
                Exit Sub

            Catch Ex As Exception
                Interaction.ShowMsg(Translation.Translate("\LOGFILE_WRITE_ERROR") & Microsoft.VisualBasic.vbNewLine & Ex.Message & Microsoft.VisualBasic.vbNewLine & Microsoft.VisualBasic.vbNewLine & Ex.ToString)

            Finally
                LogWriter.Close()
            End Try
        Catch Ex As Exception
            Interaction.ShowMsg(Translation.Translate("\LOGFILE_OPEN_ERROR") & Microsoft.VisualBasic.vbNewLine & Ex.Message & Microsoft.VisualBasic.vbNewLine & Microsoft.VisualBasic.vbNewLine & Ex.ToString)
        End Try
    End Sub
End Class