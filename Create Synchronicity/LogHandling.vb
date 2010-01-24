'This file is part of Create Synchronicity.
'
'Create Synchronicity is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
'Create Synchronicity is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with Create Synchronicity.  If not, see <http://www.gnu.org/licenses/>.
'Created by:	Clément Pit--Claudel.
'Web site:		http://synchronicity.sourceforge.net.

Class LogHandler
    Dim LogName As String
    Public Errors As List(Of Exception)
    Public Log As Dictionary(Of SyncingItem, Boolean)
#If DEBUG Then
    Public DebugInfo As List(Of String)
#End If

    Private Disposed As Boolean

    Sub New(ByVal _LogName As String)
        LogName = _LogName
        Disposed = False

#If DEBUG Then
        DebugInfo = New List(Of String)
#End If

        Errors = New List(Of Exception)
        Log = New Dictionary(Of SyncingItem, Boolean)
    End Sub

    Sub HandleError(ByVal Ex As Exception)
        Errors.Add(Ex)
    End Sub

    Sub LogAction(ByVal Item As SyncingItem, ByVal Success As Boolean)
        Log.Add(Item, Success)
    End Sub

#If DEBUG Then
    Sub LogInfo(ByVal Info As String)
        DebugInfo.Add(Info)
    End Sub
#End If

    Sub OpenHTMLHeaders(ByRef LogW As IO.StreamWriter)
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
        LogW.WriteLine("				width: 60%;")
        LogW.WriteLine("			}")
        LogW.WriteLine("			")
        LogW.WriteLine("			table {")
        LogW.WriteLine("				border-collapse: collapse;")
        LogW.WriteLine("			}")
        LogW.WriteLine("			")
        LogW.WriteLine("			th, td {")
        LogW.WriteLine("				border: solid grey;")
        LogW.WriteLine("				border-width: 1px 0 0 0;")
        LogW.WriteLine("				padding: 1em;")
        LogW.WriteLine("			}")
        LogW.WriteLine("		</style>")
        LogW.WriteLine("	</head>")
        LogW.WriteLine("	<body>")
        LogW.WriteLine("		<h1>Create Synchronicity - Log for " & LogName & "</h1>")
    End Sub

    Sub CloseHTMLHeaders(ByRef LogW As IO.StreamWriter)
        LogW.WriteLine("	</body>")
        LogW.WriteLine("</html>")
      End Sub

    Sub OpenSyncHeaders(ByRef LogW As IO.StreamWriter)
        LogW.WriteLine("<h2>" & Microsoft.VisualBasic.DateAndTime.DateString & ", " & Microsoft.VisualBasic.DateAndTime.TimeString & "</h2>")
        LogW.WriteLine("<table><tr><th>Type</th><th>Contents</th></tr>")
    End Sub

    Sub CloseSyncHeaders(ByRef LogW As IO.StreamWriter)
        LogW.WriteLine("</table>")
    End Sub

    Sub PutLine(ByVal Title As String, ByVal Contents As String, ByRef LogW As IO.StreamWriter)
#If DEBUG Then
        LogW.WriteLine(Title & Microsoft.VisualBasic.vbTab & Contents)
#Else
        LogW.WriteLine("<tr><td>" & Title & "</td><td>" & Contents & "</td></tr>")
#End If
    End Sub

    Sub SaveAndDispose()
        If Disposed Then Exit Sub
        Disposed = True

        Try
            Dim NewLog As Boolean = Not IO.File.Exists(ConfigOptions.GetLogPath(LogName))

            Dim LogWriter As New IO.StreamWriter(ConfigOptions.GetLogPath(LogName), True)

            Try
#If Not DEBUG Then
                If NewLog Then OpenHTMLHeaders(LogWriter)
                OpenSyncHeaders(LogWriter)
#End If

#If DEBUG Then
                For Each Info As String In DebugInfo
                    PutLine("Info", Info, LogWriter)
                Next
#End If
                For Each Pair As KeyValuePair(Of SyncingItem, Boolean) In Log
                    PutLine(If(Pair.Value, "Succeded", "Failed"), Pair.Key.FormatType() & "	" & Pair.Key.FormatAction() & "	" & Pair.Key.Path, LogWriter)
                Next
                For Each Ex As Exception In Errors
                    PutLine("Error", Ex.Message & "	" & Ex.StackTrace.Replace(Microsoft.VisualBasic.vbNewLine, "\n"), LogWriter)
                Next

#If Not DEBUG Then
                CloseSyncHeaders(LogWriter)
                If NewLog Then CloseHTMLHeaders(LogWriter)
#End If
            Catch Ex As Exception
                Warning("Error writing to the log file!", Ex)

            Finally
                LogWriter.Flush()
                LogWriter.Close()
                LogWriter.Dispose()
            End Try
        Catch Ex As Exception
            Warning("Unable to open the log file!", Ex)
        End Try
    End Sub

    Sub Warning(ByVal Message As String, Optional ByVal Ex As Exception = Nothing)
        MessageBox.Show(Message & Microsoft.VisualBasic.ControlChars.NewLine & If(Ex Is Nothing, "", Ex.Message))
    End Sub
End Class