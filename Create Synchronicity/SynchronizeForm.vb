Public Class SynchronizeForm
    Dim StartTime As Date
    Dim Handler As SettingsHandler
    Dim SyncingList As New Dictionary(Of SideOfSource, List(Of SyncingItem))

    Dim SyncThread As New Threading.Thread(AddressOf Synchronize)
    Delegate Sub LabelCallBack(ByVal Id As Integer, ByVal Text As String)
    Delegate Sub SetProgessCallBack(ByVal Id As Integer, ByVal Progress As Integer)
    Delegate Sub ProgressSetMaxCallBack(ByVal Id As Integer, ByVal Max As Integer)
    Delegate Sub TaskDoneCallBack(ByVal Id As Integer)
    Delegate Sub SetElapsedTimeCallBack(ByVal CurrentTimeSpan As TimeSpan)

#Region " Events "
    Sub New(ByVal ConfigName As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Handler = New SettingsHandler(ConfigName)
        Handler.LoadConfigFile()
        SyncThread.Start()
    End Sub

    Private Sub StopBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StopBtn.Click
        Select Case StopBtn.Text
            Case StopBtn.Tag.ToString.Split(";"c)(0)
                SyncThread.Abort()
                TaskDone(1) : TaskDone(2) : TaskDone(3)
            Case StopBtn.Tag.ToString.Split(";"c)(1)
                Close()
        End Select
    End Sub

    Private Sub SyncingTimeCounter_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SyncingTimeCounter.Tick
        ElapsedTime.Text = If((DateTime.Now - StartTime).Hours = 0, "", (DateTime.Now - StartTime).Hours.ToString & "h, ") & If((DateTime.Now - StartTime).Minutes = 0, "", (DateTime.Now - StartTime).Minutes.ToString & "m, ") & (DateTime.Now - StartTime).Seconds.ToString & "s."
    End Sub
#End Region

#Region " Processes interaction "
    Sub UpdateLabel(ByVal Id As Integer, ByVal Text As String)
        Select Case Id
            Case 1
                Step1StatusLabel.Text = Text
            Case 2
                Step2StatusLabel.Text = Text
            Case 3
                Step3StatusLabel.Text = Text
        End Select
    End Sub

    Sub SetProgess(ByVal Id As Integer, ByVal Progress As Integer)
        Select Case Id
            Case 1
                Step1ProgressBar.Value += Progress
            Case 2
                Step2ProgressBar.Value += Progress
            Case 3
                Step3ProgressBar.Value += Progress
        End Select
    End Sub

    Sub SetMaxProgess(ByVal Id As Integer, ByVal MaxValue As Integer)
        Select Case Id
            Case 1
                If MaxValue = -1 Then
                    Step1ProgressBar.Style = ProgressBarStyle.Marquee
                Else
                    Step1ProgressBar.Style = ProgressBarStyle.Blocks
                    Step1ProgressBar.Maximum = MaxValue
                End If
            Case 2
                If MaxValue = -1 Then
                    Step2ProgressBar.Style = ProgressBarStyle.Marquee
                Else
                    Step2ProgressBar.Style = ProgressBarStyle.Blocks
                    Step2ProgressBar.Maximum = MaxValue
                End If
            Case 3
                If MaxValue = -1 Then
                    Step3ProgressBar.Style = ProgressBarStyle.Marquee
                Else
                    Step3ProgressBar.Style = ProgressBarStyle.Blocks
                    Step3ProgressBar.Maximum = MaxValue
                End If
        End Select
    End Sub

    Sub TaskDone(ByVal Id As Integer)
        Select Case Id
            Case 1
                UpdateLabel(1, "Done !")
                Step1ProgressBar.Maximum = 100
                Step1ProgressBar.Value = Step1ProgressBar.Maximum
                Step1ProgressBar.Style = ProgressBarStyle.Blocks
            Case 2
                UpdateLabel(2, "Done !")
                Step2ProgressBar.Maximum = 100
                Step2ProgressBar.Value = Step2ProgressBar.Maximum
                Step2ProgressBar.Style = ProgressBarStyle.Blocks
            Case 3
                UpdateLabel(3, "Done !")
                Step3ProgressBar.Maximum = 100
                Step3ProgressBar.Value = Step3ProgressBar.Maximum
                Step3ProgressBar.Style = ProgressBarStyle.Blocks

                SyncingTimeCounter.Stop()
                StopBtn.Text = StopBtn.Tag.ToString.Split(";"c)(1)
        End Select
    End Sub

    Sub SetElapsedTime(ByVal CurrentTimeSpan As TimeSpan)
        ElapsedTime.Text = CurrentTimeSpan.ToString
    End Sub
#End Region

#Region " Syncing code "
    Sub Synchronize()
        Dim TaskDoneDelegate As New TaskDoneCallBack(AddressOf TaskDone)
        Dim SetProgessDelegate As New SetProgessCallBack(AddressOf SetProgess)
        Dim ProgessSetMaxCallBack As New ProgressSetMaxCallBack(AddressOf SetMaxProgess)
        Dim LabelDelegate As New LabelCallBack(AddressOf UpdateLabel)
        Dim SetElapsedTimeCallBack As New SetElapsedTimeCallBack(AddressOf SetElapsedTime)

        StartTime = DateTime.Now
        SyncingTimeCounter.Start()
        Dim Context As New SyncingAction

        SyncingList.Clear()
        SyncingList.Add(SideOfSource.Left, New List(Of SyncingItem))
        SyncingList.Add(SideOfSource.Right, New List(Of SyncingItem))

        Context.Source = SideOfSource.Left
        Context.SourcePath = Handler.GetSetting("From")
        Context.DestinationPath = Handler.GetSetting("To")
        Context.Type = TypeOfAction.Create
        Init_Synchronization(Handler.LeftCheckedNodes, Context)

        Context.Source = SideOfSource.Right
        Context.SourcePath = Handler.GetSetting("To")
        Context.DestinationPath = Handler.GetSetting("From")
        Select Case Handler.GetSetting("Method")
            Case "0"
                Context.Type = TypeOfAction.Delete
                Init_Synchronization(Handler.RightCheckedNodes, Context)
            Case "2"
                Context.Type = TypeOfAction.Create
                Init_Synchronization(Handler.RightCheckedNodes, Context)
        End Select
        Me.Invoke(TaskDoneDelegate, 1)

        Dim Left As String = Handler.GetSetting("From")
        Dim Right As String = Handler.GetSetting("To")

        Try
            Me.Invoke(ProgessSetMaxCallBack, New Object() {2, SyncingList(SideOfSource.Left).Count})
            For Each Entry As SyncingItem In SyncingList(SideOfSource.Left)
                Select Case Entry.Type
                    Case TypeOfItem.File
                        CopyFile(Entry.Path, Left, Right)
                    Case TypeOfItem.Folder
                        IO.Directory.CreateDirectory(Right & Entry.Path)
                End Select

                Me.Invoke(SetProgessDelegate, New Object() {2, 1})
                Me.Invoke(LabelDelegate, New Object() {2, Right & Entry.Path})
            Next
            Me.Invoke(TaskDoneDelegate, 2)

            Me.Invoke(ProgessSetMaxCallBack, New Object() {3, SyncingList(SideOfSource.Right).Count})
            For Each Entry As SyncingItem In SyncingList(SideOfSource.Right)
                Select Case Entry.Type
                    Case TypeOfItem.File
                        Select Case Entry.Action
                            Case TypeOfAction.Create
                                CopyFile(Entry.Path, Right, Left)
                            Case TypeOfAction.Delete
                                IO.File.Delete(Right & Entry.Path)
                        End Select

                    Case TypeOfItem.Folder
                        Select Case Entry.Action
                            Case TypeOfAction.Create
                                IO.Directory.CreateDirectory(Left & Entry.Path)
                            Case TypeOfAction.Delete
                                Select Case Handler.GetSetting("Restrictions")
                                    Case "0"
                                        If Not IO.Directory.Exists(Left & Entry.Path) Then IO.Directory.Delete(Right & Entry.Path)
                                    Case Else
                                        If IO.Directory.GetFiles(Right & Entry.Path).GetLength(0) = 0 Then IO.Directory.Delete(Right & Entry.Path)
                                End Select

                        End Select
                End Select

                'Me.Invoke(SetProgessDelegate, New Object() {3, 1})
                Me.Invoke(LabelDelegate, New Object() {3, Right & Entry.Path})
            Next
            Me.Invoke(TaskDoneDelegate, 3)
        Catch Ex As Exception
            ErrorHandler.HandleError(Ex)
        Finally

        End Try
    End Sub

    Sub Init_Synchronization(ByRef FoldersList As SortedList(Of String, Boolean), ByVal Context As SyncingAction)
        For Each Folder As String In FoldersList.Keys
            BuildList(Folder, FoldersList(Folder), Context)
        Next
    End Sub

    Sub BuildList(ByVal Folder As String, ByVal Recursive As Boolean, ByVal Context As SyncingAction)
        If Not Folder.StartsWith("\") Then Folder = "\" & Folder
        Dim AbsolutePath As String = Context.SourcePath & Folder

        If Recursive Then
            For Each SubFolder As String In IO.Directory.GetDirectories(AbsolutePath)
                BuildList(SubFolder.Substring(Context.SourcePath.Length), True, Context)
            Next
        End If

        For Each File As String In IO.Directory.GetFiles(AbsolutePath)
            Dim SourceFile As String = File
            Dim DestinationFile As String = Context.DestinationPath & "\" & Folder & "\" & GetFileOrFolderName(File)
            If (Not HasValidExtension(File)) OrElse (IO.File.Exists(DestinationFile)) OrElse (IO.File.GetLastWriteTime(SourceFile) = IO.File.GetLastWriteTime(DestinationFile)) Then Continue For

            SyncingList(Context.Source).Add(New SyncingItem(File.Substring(Context.SourcePath.Length), TypeOfItem.File, Context.Type))
        Next

        SyncingList(Context.Source).Add(New SyncingItem(Folder, TypeOfItem.Folder, Context.Type))
    End Sub

    Function HasValidExtension(ByVal Path As String) As Boolean
        Select Case Handler.GetSetting("Restrictions")
            Case "1"
                Return InArray(GetExtension(Path), Handler.GetSetting("IncludedTypes").Split(";"c))
            Case "2"
                Return Not InArray(GetExtension(Path), Handler.GetSetting("ExcludedTypes").Split(";"c))
        End Select
        Return True
    End Function

    Sub CopyFile(ByVal Path As String, ByVal Source As String, ByVal Dest As String)
        Try
            IO.Directory.CreateDirectory(Dest & Path.Substring(0, Path.LastIndexOf("\") + 1))
            IO.File.Copy(Source & Path, Dest & Path)
        Catch Ex As Exception
            ErrorHandler.HandleError(Ex)
        End Try
    End Sub
#End Region

#Region " Functions "
    Function GetFileOrFolderName(ByVal Path As String) As String
        Return Path.Substring(Path.LastIndexOf("\") + 1)
    End Function

    Function GetExtension(ByVal Path As String) As String
        Return Path.Substring(Path.LastIndexOf(".") + 1)
    End Function

    Function InArray(ByVal Str As String, ByRef ListObject As String()) As Boolean
        For Each SubStr As String In ListObject
            If Str = SubStr Then Return True
        Next
        Return False
    End Function
#End Region
End Class