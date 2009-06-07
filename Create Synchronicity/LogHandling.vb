Class LogHandler
    Dim LogName As String
    Dim Errors As List(Of Exception)
    Dim Log As Dictionary(Of SyncingItem, Boolean)

    Sub New(ByVal _LogName As String)
        LogName = LogName
        Errors = New List(Of Exception)
        Log = New Dictionary(Of SyncingItem, Boolean)
    End Sub

    Sub HandleError(ByVal Ex As Exception)
        Errors.Add(Ex)
    End Sub

    Sub LogAction(ByVal Item As SyncingItem, ByVal Success As Boolean)
        Log.Add(Item, Success)
    End Sub

    Sub SaveAndDispose()
        Dim LogWriter As New IO.StreamWriter(Application.StartupPath & "\logs\" & LogName & ".log", True)
        LogWriter.WriteLine(" -- " & Microsoft.VisualBasic.DateAndTime.DateString & ", " & Microsoft.VisualBasic.DateAndTime.TimeString & " -- ")
        For Each Pair As KeyValuePair(Of SyncingItem, Boolean) In Log
            LogWriter.WriteLine(If(Pair.Value, "Succeded", "Failed") & "	" & Pair.Key.Type & "	" & Pair.Key.Action & "	" & Pair.Key.Path)
        Next
        LogWriter.Flush()
        LogWriter.Close()
        LogWriter.Dispose()
    End Sub
End Class