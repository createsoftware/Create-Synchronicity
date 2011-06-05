'This file is part of Create Synchronicity.
'
'Create Synchronicity is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
'Create Synchronicity is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with Create Synchronicity.  If not, see <http://www.gnu.org/licenses/>.
'Created by:	Clément Pit--Claudel.
'Web site:		http://synchronicity.sourceforge.net.

Option Strict On

Public Class SynchronizeForm
    Private Log As LogHandler
    Private Handler As ProfileHandler

    Private ValidFiles As New Dictionary(Of String, Boolean)
    Private SyncingList As New Dictionary(Of SideOfSource, List(Of SyncingItem))
    Private IncludedPatterns As New List(Of FileNamePattern)
    Private ExcludedPatterns As New List(Of FileNamePattern)
    Private ExcludedDirPatterns As New List(Of FileNamePattern)

    Private Labels() As String = Nothing
    Private StatusLabel As String = ""
    Private Lock As New Object()

    Private Quiet As Boolean 'This Quiet parameter is not a duplicate ; it is used when eg the scheduler needs to tell the form to keep quiet, although the "quiet" command-line flag wasn't used.
    Private Catchup As Boolean 'Indicates whether this operation was started due to catchup rules.
    Private Preview As Boolean 'Should show a preview.

    Private Status As StatusData
    Private ColumnSorter As ListViewColumnSorter

    Private FullSyncThread As Threading.Thread
    Private ScanThread As Threading.Thread
    Private SyncThread As Threading.Thread

    Private Delegate Sub TaskDoneCall(ByVal Id As StatusData.SyncStep)
    Private Delegate Sub SetIntCall(ByVal Id As StatusData.SyncStep, ByVal Max As Integer)

    Friend Event SyncFinished(ByVal Name As String, ByVal Completed As Boolean)

    'Not evaluating file size gives better performance (See speed-test.vb for tests):
    'With size evaluation: 1'20, 46'', 36'', 35'', 31''
    'Without:                    41'', 42'', 26'', 29''

#Region " Events "
    Public Sub New(ByVal ConfigName As String, ByVal DisplayPreview As Boolean, ByVal _Quiet As Boolean, ByVal _Catchup As Boolean)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Quiet = _Quiet
        Catchup = _Catchup
        Preview = DisplayPreview
        SyncBtn.Enabled = False
        SyncBtn.Visible = Preview

        Status.Failed = False
        Status.Cancel = False
        Status.CurrentStep = StatusData.SyncStep.Scan
        Status.StartTime = Date.Now ' NOTE: This call should be useless; it however seems that when the messagebox.show method is called when a profile is not found, the syncingtimecounter starts ticking. This is not suitable, but until the cause is found there this call remains, for display consistency.

        Log = New LogHandler(ConfigName)
        Handler = New ProfileHandler(ConfigName)

        ColumnSorter = New ListViewColumnSorter(3)
        PreviewList.ListViewItemSorter = ColumnSorter

        FileNamePattern.LoadPatternsList(IncludedPatterns, Handler.GetSetting(Of String)(ProfileSetting.IncludedTypes, ""))
        FileNamePattern.LoadPatternsList(ExcludedPatterns, Handler.GetSetting(Of String)(ProfileSetting.ExcludedTypes, ""))
        FileNamePattern.LoadPatternsList(ExcludedDirPatterns, Handler.GetSetting(Of String)(ProfileSetting.ExcludedFolders, ""), True)

        FullSyncThread = New Threading.Thread(AddressOf FullSync)
        ScanThread = New Threading.Thread(AddressOf Scan)
        SyncThread = New Threading.Thread(AddressOf Sync)

        Me.CreateHandle()
        Translation.TranslateControl(Me)
        Me.Icon = ProgramConfig.Icon
        Me.Text = String.Format(Me.Text, Handler.ProfileName, Handler.GetSetting(Of String)(ProfileSetting.Source), Handler.GetSetting(Of String)(ProfileSetting.Destination)) 'Feature requests #3037548, #3055740

        Labels = New String() {"", Step1StatusLabel.Text, Step2StatusLabel.Text, Step3StatusLabel.Text}

#If LINUX Then
        Step1ProgressBar.MarqueeAnimationSpeed = 5000
        SyncingTimer.Interval = 1000
#End If
    End Sub

    Sub StartSynchronization(ByVal CalledShowModal As Boolean)
        ProgramConfig.CanGoOn = False

#If DEBUG Then
        Log.LogInfo("Synchronization started.")
        Log.LogInfo("Profile settings:")
        For Each Pair As KeyValuePair(Of String, String) In Handler.Configuration
            Log.LogInfo(String.Format("    {0,-50}: {1}", Pair.Key, Pair.Value))
        Next
        Log.LogInfo("Done.")
#End If

        If Quiet Then
            Me.Visible = False

            Interaction.StatusIcon.ContextMenuStrip = Nothing
            AddHandler Interaction.StatusIcon.Click, AddressOf StatusIcon_Click

            Interaction.StatusIcon.Text = Translation.Translate("\RUNNING")

            Interaction.ToggleStatusIcon(True)
            If Catchup Then
                Dim LastRun As Date = Handler.GetLastRun()
                Interaction.ShowBalloonTip(String.Format(Translation.Translate("\CATCHING_UP"), Handler.ProfileName, (Date.Now - LastRun).Days, (Date.Now - LastRun).Hours, LastRun.ToString))
            Else
                Interaction.ShowBalloonTip(String.Format(Translation.Translate("\RUNNING_TASK"), Handler.ProfileName))
            End If
        Else
            If Not CalledShowModal Then Me.Visible = True 'Me.Show?
        End If

        Status.FailureMsg = ""
        Dim IsValid As Boolean = Handler.ValidateConfigFile(False, True, Quiet, Status.FailureMsg)
        Status.Failed = Not IsValid

        If IsValid Then
            ProgramConfig.IncrementSyncsCount()
            If Preview Then
                PreviewList.Items.Clear()
                ScanThread.Start()
            Else
                FullSyncThread.Start()
            End If
        Else
            EndAll() 'Also saves the log file
        End If
    End Sub

    Private Sub SynchronizeForm_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Control Then
            If e.KeyCode = Keys.L AndAlso Status.CurrentStep = StatusData.SyncStep.Done Then
                Interaction.StartProcess(ProgramConfig.GetLogPath(Handler.ProfileName))
            ElseIf e.KeyCode = Keys.D And PreviewList.SelectedIndices.Count <> 0 Then
                Dim DiffProgram As String = ProgramConfig.GetProgramSetting(Of String)(ProgramSetting.DiffProgram, "")
                Dim DiffArguments As String = ProgramConfig.GetProgramSetting(Of String)(ProgramSetting.DiffArguments, "")
                Dim NewFile As String = "", OldFile As String = ""
                If Not SetPathFromSelectedItem(NewFile, OldFile) Then Exit Sub
                Try
                    If DiffProgram <> "" AndAlso IO.File.Exists(OldFile) AndAlso IO.File.Exists(NewFile) Then Interaction.StartProcess(DiffProgram.Trim, DiffArguments.Replace("%o", OldFile).Replace("%n", NewFile))
                Catch Ex As Exception
                    Interaction.ShowMsg("Error loading diff: " & Ex.ToString)
                End Try
            End If
        End If
    End Sub

    Private Sub SynchronizeForm_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        EndAll()
        ProgramConfig.CanGoOn = True
        Interaction.StatusIcon.ContextMenuStrip = MainFormInstance.StatusIconMenu
        RemoveHandler Interaction.StatusIcon.Click, AddressOf StatusIcon_Click

        Interaction.StatusIcon.Text = Translation.Translate("\WAITING")
        RaiseEvent SyncFinished(Handler.ProfileName, Not (Status.Failed Or Status.Cancel))
    End Sub

    Private Sub CancelBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StopBtn.Click
        Select Case StopBtn.Text
            Case StopBtn.Tag.ToString.Split(";"c)(0)
                EndAll()
            Case StopBtn.Tag.ToString.Split(";"c)(1)
                Me.Close()
        End Select
    End Sub

    Private Sub SyncBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SyncBtn.Click
        PreviewList.Visible = False
        SyncBtn.Visible = False
        StopBtn.Text = StopBtn.Tag.ToString.Split(";"c)(0)

        SyncThread.Start()
    End Sub

    Private Sub StatusIcon_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.Visible = Not Me.Visible
        Me.WindowState = FormWindowState.Normal
        If Me.Visible Then Me.Activate()
    End Sub

    Private Sub SynchronizeForm_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Me.WindowState = FormWindowState.Minimized And Quiet Then Me.Visible = False
    End Sub

    Private Sub SyncingTimeCounter_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SyncingTimer.Tick
        UpdateStatuses()
    End Sub

    Private Sub PreviewList_ColumnClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles PreviewList.ColumnClick
        If e.Column = ColumnSorter.SortColumn Then
            ColumnSorter.Order = If(ColumnSorter.Order = SortOrder.Ascending, SortOrder.Descending, SortOrder.Ascending)
        Else
            ColumnSorter.SortColumn = e.Column
            ColumnSorter.Order = SortOrder.Ascending
        End If

        PreviewList.Sort()
    End Sub

    Private Sub PreviewList_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PreviewList.DoubleClick
        Dim Source As String = "", Dest As String = ""
        If Not SetPathFromSelectedItem(Source, Dest) Then Exit Sub

        If IO.File.Exists(Source) Or IO.Directory.Exists(Source) Then Interaction.StartProcess(If((Control.ModifierKeys And Keys.Control) = Keys.Control, IO.Path.GetDirectoryName(Source), Source))
    End Sub

    Private Function SetPathFromSelectedItem(ByRef Source As String, ByRef Dest As String) As Boolean
        If PreviewList.SelectedIndices.Count = 0 Then Return False

        Dim CurItem As ListViewItem = PreviewList.SelectedItems(0)
        If CurItem.Tag Is Nothing OrElse CurItem.SubItems.Count < 3 Then Return False

        Dim Left As String, Right As String
        Left = ProfileHandler.TranslatePath(Handler.GetSetting(Of String)(ProfileSetting.Source)) & CurItem.SubItems(3).Text
        Right = ProfileHandler.TranslatePath(Handler.GetSetting(Of String)(ProfileSetting.Destination)) & CurItem.SubItems(3).Text

        Select Case CType(CurItem.Tag, StatusData.SyncStep)
            Case StatusData.SyncStep.SyncLR
                Source = Left : Dest = Right
            Case StatusData.SyncStep.SyncRL
                Source = Right : Dest = Left
            Case Else
                'In errors list
                Return False
        End Select

        Return True
    End Function

    Private Shared Function FormatSize(ByVal Size As Double) As String
        Select Case Size
            Case Is >= (1 << 30)
                Return Math.Round(Size / (1 << 30), 2).ToString & " GB"
            Case Is >= (1 << 20)
                Return Math.Round(Size / (1 << 20), 2).ToString & " MB"
            Case Is >= (1 << 10)
                Return Math.Round(Size / (1 << 10), 2).ToString & " kB"
            Case Else
                Return Math.Round(Size, 2).ToString & " B"
        End Select
    End Function

    Private Shared Function FormatTimespan(ByVal T As TimeSpan) As String
        Dim Hours As Integer = CInt(Math.Truncate(T.TotalHours))
        Return If(Hours = 0, "", Hours & "h, ") & If(T.Minutes = 0, "", T.Minutes.ToString & "m, ") & T.Seconds.ToString & "s"
    End Function

    Private Sub UpdateStatuses()
        Status.TimeElapsed = (DateTime.Now - Status.StartTime) + New TimeSpan(1000000) ' ie +0.1s

        If Status.CurrentStep = StatusData.SyncStep.Scan Then
            Speed.Text = Math.Round(Status.FilesScanned / Status.TimeElapsed.TotalSeconds).ToString & " files/s"
        Else
            Status.Speed = Status.BytesCopied / Status.TimeElapsed.TotalSeconds
            Speed.Text = FormatSize(Status.Speed) & "/s"
        End If

        Dim EstimateString As String = ""
        If Status.CurrentStep = StatusData.SyncStep.SyncLR And Status.TimeElapsed.TotalSeconds > 60 And ProgramConfig.GetProgramSetting(Of Boolean)(ProfileSetting.Forecast, True) Then
            Dim RemainingSeconds As Double = Math.Min(Integer.MaxValue / 2, (Status.BytesScanned / (1 + Status.Speed)) - Status.TimeElapsed.TotalSeconds)
            EstimateString = String.Format(" [/ ~{0}]", FormatTimespan(New TimeSpan(0, 0, CInt(RemainingSeconds)))) ' LATER: RemainingSeconds = 120 * Math.Ceiling(RemainingSeconds / 120)
        End If
        ElapsedTime.Text = FormatTimespan(Status.TimeElapsed) & EstimateString

        Done.Text = Status.ActionsDone & "/" & Status.TotalActionsCount
        FilesDeleted.Text = Status.DeletedFiles & "/" & Status.FilesToDelete
        FilesCreated.Text = Status.CreatedFiles & "/" & Status.FilesToCreate & " (" & FormatSize(Status.BytesCopied) & ")"
        FoldersDeleted.Text = Status.DeletedFolders & "/" & Status.FoldersToDelete
        FoldersCreated.Text = Status.CreatedFolders & "/" & Status.FoldersToCreate

        SyncLock Lock
            Step1StatusLabel.Text = Labels(1)
            Step2StatusLabel.Text = Labels(2)
            Step3StatusLabel.Text = Labels(3)
            Interaction.StatusIcon.Text = StatusLabel
        End SyncLock
    End Sub
#End Region

#Region " Interface "
    Private Sub UpdateLabel(ByVal Id As StatusData.SyncStep, ByVal Text As String)
        Dim StatusText As String = Text
        If Text.Length > 30 Then
            StatusText = "..." & Text.Substring(Text.Length - 30, 30)
        End If

        Select Case Id
            Case StatusData.SyncStep.Scan
                StatusText = String.Format(Translation.Translate("\STEP_1_STATUS"), StatusText)
            Case StatusData.SyncStep.SyncLR
                StatusText = String.Format(Translation.Translate("\STEP_2_STATUS"), Step2ProgressBar.Value, Step2ProgressBar.Maximum, StatusText)
            Case StatusData.SyncStep.SyncRL
                StatusText = String.Format(Translation.Translate("\STEP_3_STATUS"), Step3ProgressBar.Value, Step3ProgressBar.Maximum, StatusText)
        End Select

        SyncLock Lock
            Labels(Id) = Text
            StatusLabel = StatusText
        End SyncLock
    End Sub

    Private Function GetProgressBar(ByVal Id As StatusData.SyncStep) As ProgressBar
        Select Case Id
            Case StatusData.SyncStep.Scan
                Return Step1ProgressBar
            Case StatusData.SyncStep.SyncLR
                Return Step2ProgressBar
            Case Else
                Return Step3ProgressBar
        End Select
    End Function

    Private Sub Increment(ByVal Id As StatusData.SyncStep, ByVal Progress As Integer)
        Dim CurBar As ProgressBar = GetProgressBar(Id)
        If CurBar.Value + Progress < CurBar.Maximum Then CurBar.Value += Progress
    End Sub

    Private Sub SetMax(ByVal Id As StatusData.SyncStep, ByVal MaxValue As Integer, Optional ByVal Finished As Boolean = False) 'Careful: MaxValue is an Integer.
        Dim CurBar As ProgressBar = GetProgressBar(Id)

        CurBar.Style = ProgressBarStyle.Blocks
        CurBar.Maximum = Math.Max(0, MaxValue)
        CurBar.Value = If(Finished, MaxValue, 0)
    End Sub

    Private Sub TaskDone(ByVal Id As StatusData.SyncStep)
        If Not Status.CurrentStep = Id Then Exit Sub 'Prevents infinite exit loop.

        SetMax(Id, 100, True)
        UpdateLabel(Id, Translation.Translate("\FINISHED"))
        UpdateStatuses()

        Select Case Id
            Case StatusData.SyncStep.Scan
                SyncingTimer.Stop()
                Status.CurrentStep = StatusData.SyncStep.SyncLR
                If Preview Then
                    UpdatePreviewList()
                    StopBtn.Text = StopBtn.Tag.ToString.Split(";"c)(1)
                End If

            Case StatusData.SyncStep.SyncLR
                Status.CurrentStep = StatusData.SyncStep.SyncRL

            Case StatusData.SyncStep.SyncRL
                SyncingTimer.Stop()
                Status.CurrentStep = StatusData.SyncStep.Done

                If Log.Errors.Count > 0 Or Status.Failed Then
                    PreviewList.Visible = True
                    PreviewList.Items.Clear()
                    PreviewList.Columns.Clear()
                    PreviewList.Columns.Add(Translation.Translate("\ERROR"))
                    PreviewList.Columns.Add(Translation.Translate("\PATH"))
                    Dim ErrorColumn As ColumnHeader = PreviewList.Columns.Add(Translation.Translate("\ERROR_DETAIL"))
                    ColumnSorter.SortColumn = ErrorColumn.Index

                    Dim ErrorsList As New List(Of ErrorItem)(Log.Errors)
                    For Each Err As ErrorItem In ErrorsList
                        Dim ErrorListItem As New ListViewItem(Err.Ex.Source)
                        ErrorListItem.SubItems.Add(Err.Details)
                        ErrorListItem.SubItems.Add(Err.Ex.Message)
                        PreviewList.Items.Add(ErrorListItem)
                        ErrorListItem.ImageIndex = 7
                    Next

                    PreviewList.Columns(0).AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent)
                    ErrorColumn.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent)

                    If Quiet Then 'TODO: Show ballon tip every time? -> Remember to modify init function to show icon if so.
                        If Status.Failed Then
                            System.Threading.Thread.Sleep(5000) 'Wait a little before failing
                            Interaction.ShowBalloonTip(Status.FailureMsg)
                        Else
                            Interaction.ShowBalloonTip(String.Format(Translation.Translate("\SYNCED_W_ERRORS"), Handler.ProfileName), ProgramConfig.GetLogPath(Handler.ProfileName))
                        End If
                    End If
                Else
                    'LATER: Add ballon to say the sync was cancelled.
                    If Quiet And Not Status.Cancel Then Interaction.ShowBalloonTip(String.Format(Translation.Translate("\SYNCED_OK"), Handler.ProfileName), ProgramConfig.GetLogPath(Handler.ProfileName))
                End If

                ' Set last run only if the profile hasn't failed, and has synced completely.
                ' Checking for Status.Cancel allows to resync if eg. computer was stopped during sync.
                ' EndAll() sets Status.Cancel to true, but if the sync completes successfully, this part executes before the call to EndAll 
                If Not (Status.Failed Or Status.Cancel) Then Handler.SetLastRun()

                Log.SaveAndDispose(Handler.GetSetting(Of String)(ProfileSetting.Source), Handler.GetSetting(Of String)(ProfileSetting.Destination), Status)

                If (Quiet And Not Me.Visible) Or CommandLine.NoStop Then
                    Me.Close()
                Else
                    StopBtn.Text = StopBtn.Tag.ToString.Split(";"c)(1)
                End If
        End Select
    End Sub

    Private Sub UpdatePreviewList()
        PreviewList.Visible = True
        If PreviewList.Items.Count > 0 Then PreviewList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
        If Not Status.Cancel Then SyncBtn.Enabled = True
    End Sub

    Private Sub AddPreviewItem(ByRef Item As SyncingItem, ByVal Side As SideOfSource)
        Dim ListItem As New ListViewItem
        ListItem = PreviewList.Items.Add(Item.FormatType())
        ListItem.SubItems.Add(Item.FormatAction())
        ListItem.SubItems.Add(Item.FormatDirection(Side))
        ListItem.SubItems.Add(Item.Path)

        ListItem.Tag = If(Side = SideOfSource.Left, StatusData.SyncStep.SyncLR, StatusData.SyncStep.SyncRL)

        Select Case Item.Action
            Case TypeOfAction.Copy
                If Item.Type = TypeOfItem.Folder Then
                    ListItem.ImageIndex = 5
                    Status.FoldersToCreate += 1
                End If
                If Item.Type = TypeOfItem.File Then
                    Select Case Side
                        Case SideOfSource.Left
                            ListItem.ImageIndex = If(Item.IsUpdate, 1, 0)
                        Case SideOfSource.Right
                            ListItem.ImageIndex = If(Item.IsUpdate, 3, 2)
                    End Select
                    Status.FilesToCreate += 1
                End If
            Case TypeOfAction.Delete
                If Item.Type = TypeOfItem.Folder Then
                    ListItem.ImageIndex = 6
                    Status.FoldersToDelete += 1
                End If
                If Item.Type = TypeOfItem.File Then
                    ListItem.ImageIndex = 4
                    Status.FilesToDelete += 1
                End If
        End Select
        Status.TotalActionsCount += 1
    End Sub

    Private Sub LaunchTimer()
        Status.BytesCopied = 0
        Status.StartTime = DateTime.Now
        SyncingTimer.Start()
    End Sub

    Private Sub EndAll()
        Status.Cancel = Status.Cancel Or (Status.CurrentStep <> StatusData.SyncStep.Done)
        FullSyncThread.Abort()
        ScanThread.Abort() : SyncThread.Abort()
        TaskDone(StatusData.SyncStep.Scan) : TaskDone(StatusData.SyncStep.SyncLR) : TaskDone(StatusData.SyncStep.SyncRL) 'This call will sleep for 5s after displaying its failure message if the backup failed.
    End Sub
#End Region

#Region " Syncing code "
    Private Sub FullSync()
        Scan()
        Sync()
    End Sub

    Private Sub Scan()
        Dim Context As New SyncingAction
        Dim TaskDoneCallback As New TaskDoneCall(AddressOf TaskDone)

        'Pass 1: Create actions L->R for files/folder copy, and mark dest files that should be kept
        'Pass 2: Create actions R->L for files/folder copy/deletion, based on what was marked as ValidFile, aka based on what should be kept.

        SyncingList.Clear()
        SyncingList.Add(SideOfSource.Left, New List(Of SyncingItem))
        SyncingList.Add(SideOfSource.Right, New List(Of SyncingItem))

        ValidFiles.Clear()

        Dim Source As String = ProfileHandler.TranslatePath(Handler.GetSetting(Of String)(ProfileSetting.Source))
        Dim Destination As String = ProfileHandler.TranslatePath(Handler.GetSetting(Of String)(ProfileSetting.Destination))

        Me.Invoke(New Action(AddressOf LaunchTimer))
        Context.Source = SideOfSource.Left
        Context.SourcePath = Source
        Context.DestinationPath = Destination
        Context.Action = TypeOfAction.Copy
        Init_Synchronization(Handler.LeftCheckedNodes, Context)

        Context.Source = SideOfSource.Right
        Context.SourcePath = Destination
        Context.DestinationPath = Source
        Select Case Handler.GetSetting(Of Integer)(ProfileSetting.Method)
            Case 0
                Context.Action = TypeOfAction.Delete
                Init_Synchronization(Handler.RightCheckedNodes, Context)
            Case 2
                Context.Action = TypeOfAction.Copy
                Init_Synchronization(Handler.RightCheckedNodes, Context)
        End Select
        Me.Invoke(TaskDoneCallback, StatusData.SyncStep.Scan)

        'NOTE: [to sysadmins] (March 13, 2010) --> Moved to FAQ (http://synchronicity.sourceforge.net/faq.html)
    End Sub

    Private Sub Sync()
        Dim TaskDoneCallback As New TaskDoneCall(AddressOf TaskDone)
        Dim SetMaxCallback As New SetIntCall(AddressOf SetMax)

        Dim Left As String = ProfileHandler.TranslatePath(Handler.GetSetting(Of String)(ProfileSetting.Source))
        Dim Right As String = ProfileHandler.TranslatePath(Handler.GetSetting(Of String)(ProfileSetting.Destination))

        Me.Invoke(New Action(AddressOf LaunchTimer))
        Me.Invoke(SetMaxCallback, New Object() {StatusData.SyncStep.SyncLR, SyncingList(SideOfSource.Left).Count})
        Do_Task(SideOfSource.Left, SyncingList(SideOfSource.Left), Left, Right, StatusData.SyncStep.SyncLR)
        Me.Invoke(TaskDoneCallback, StatusData.SyncStep.SyncLR)

        Me.Invoke(SetMaxCallback, New Object() {StatusData.SyncStep.SyncRL, SyncingList(SideOfSource.Right).Count})
        Do_Task(SideOfSource.Right, SyncingList(SideOfSource.Right), Right, Left, StatusData.SyncStep.SyncRL)
        Me.Invoke(TaskDoneCallback, StatusData.SyncStep.SyncRL)
    End Sub

    '"Source" is "current side", with the corresponding side set to "Side"
    Private Sub Do_Task(ByVal Side As SideOfSource, ByRef ListOfActions As List(Of SyncingItem), ByVal Source As String, ByVal Destination As String, ByVal CurrentStep As StatusData.SyncStep)
        Dim IncrementCallback As New SetIntCall(AddressOf Increment)

        For Each Entry As SyncingItem In ListOfActions
            Dim SourcePath As String = Source & Entry.Path
            Dim DestPath As String = Destination & Entry.Path

            Try
                UpdateLabel(CurrentStep, If(Entry.Action = TypeOfAction.Delete, SourcePath, DestPath))

                Select Case Entry.Type
                    Case TypeOfItem.File
                        Select Case Entry.Action
                            Case TypeOfAction.Copy
                                CopyFile(SourcePath, DestPath)
                            Case TypeOfAction.Delete
                                IO.File.SetAttributes(SourcePath, IO.FileAttributes.Normal)
                                IO.File.Delete(SourcePath)
                                Status.DeletedFiles += 1
                        End Select

                    Case TypeOfItem.Folder
                        Select Case Entry.Action
                            Case TypeOfAction.Copy
                                IO.Directory.CreateDirectory(DestPath)
                                IO.Directory.SetCreationTimeUtc(DestPath, IO.Directory.GetCreationTimeUtc(SourcePath).AddHours(Handler.GetSetting(Of Integer)(ProfileSetting.TimeOffset, 0)))
                                Status.CreatedFolders += 1
                            Case TypeOfAction.Delete
                                If IO.Directory.GetFiles(SourcePath).GetLength(0) = 0 Then
                                    Try
                                        IO.Directory.Delete(SourcePath)
                                    Catch ex As Exception
                                        Dim DirInfo As New IO.DirectoryInfo(SourcePath)
                                        DirInfo.Attributes = IO.FileAttributes.Normal
                                        DirInfo.Delete()
                                    End Try
                                    Status.DeletedFolders += 1
                                End If
                        End Select
                End Select
                Status.ActionsDone += 1
                Log.LogAction(Entry, Side, True)

            Catch StopEx As Threading.ThreadAbortException
                Exit Sub

            Catch ex As Exception
                Log.HandleError(ex, SourcePath)
                Log.LogAction(Entry, Side, False) 'Side parameter is only used for logging purposes.
            End Try

            If Not Status.Cancel Then Me.Invoke(IncrementCallback, New Object() {CurrentStep, 1})
        Next
    End Sub

    Private Sub Init_Synchronization(ByRef FoldersList As Dictionary(Of String, Boolean), ByVal Context As SyncingAction)
        For Each Folder As String In FoldersList.Keys
            Log.LogInfo(String.Format("=> Scanning ""{0}"" top level folders: ""{1}""", Context.SourcePath, Folder))
            If IO.Directory.Exists(CombinePathes(Context.SourcePath, Folder)) Then
                If Context.Action = TypeOfAction.Copy Then
                    'FIXED-BUG: Every ancestor of this folder should be added too.
                    'Careful with this, for it's a performance issue. Ancestors should only be added /once/.
                    'How to do that? Well, if ancestors of a folder have not been scanned, it means that this folder wasn't reached by a recursive call, but by a initial call.
                    'Therefore, only the folders in the sync config file should be added.
                    AddValidAncestors(Folder)
                    SearchForChanges(Folder, FoldersList(Folder), Context)
                ElseIf Context.Action = TypeOfAction.Delete Then
                    SearchForCrap(Folder, FoldersList(Folder), Context)
                End If
            End If
        Next
    End Sub

    Private Sub AddToSyncingList(ByVal Side As SideOfSource, ByRef Entry As SyncingItem, Optional ByVal Suffix As String = "")
        SyncingList(Side).Add(Entry)
        SyncPreviewList(Side, 1)
        If Entry.Action <> TypeOfAction.Delete Then AddValidFile(Entry.Path & Suffix)
    End Sub

    Private Sub AddValidFile(ByVal File As String)
        If Not IsValidFile(File) Then ValidFiles.Add(File.ToLower(Interaction.InvariantCulture), Nothing)
    End Sub

    Private Sub AddValidAncestors(ByVal Folder As String)
        Log.LogInfo(String.Format("AddValidAncestors: Folder ""{0}"" is a top level folder, adding it's ancestors.", Folder))
        Dim CurrentAncestor As New System.Text.StringBuilder
        Dim Ancestors As New List(Of String)(Folder.Split(New Char() {ProgramSetting.DirSep}, StringSplitOptions.RemoveEmptyEntries))

        For Depth As Integer = 0 To (Ancestors.Count - 1) - 1 'The last ancestor is the folder itself, and will be added in SearchForChanges.
            CurrentAncestor.Append(ProgramSetting.DirSep).Append(Ancestors(Depth))
            AddValidFile(CurrentAncestor.ToString)
            Log.LogInfo(String.Format("AddValidAncestors: [Valid folder] ""{0}""", CurrentAncestor.ToString))
        Next
    End Sub

    Private Sub RemoveValidFile(ByVal File As String)
        If IsValidFile(File) Then ValidFiles.Remove(File.ToLower(Interaction.InvariantCulture))
    End Sub

    Private Function IsValidFile(ByVal File As String) As Boolean
        Return ValidFiles.ContainsKey(File.ToLower(Interaction.InvariantCulture))
    End Function

    Private Sub RemoveFromSyncingList(ByVal Side As SideOfSource)
        ValidFiles.Remove(SyncingList(Side)(SyncingList(Side).Count - 1).Path)
        SyncingList(Side).RemoveAt(SyncingList(Side).Count - 1)
        SyncPreviewList(Side, -1)
    End Sub

    ' This procedure searches for changes in the source directory, in regards
    ' to the status of the destination directory.
    Private Sub SearchForChanges(ByVal Folder As String, ByVal Recursive As Boolean, ByVal Context As SyncingAction)
        If Not HasAcceptedDirname(Folder) Then Exit Sub
        Log.LogInfo(String.Format("=> Scanning folder ""{0}"" for new or updated files.", Folder))

        Dim Src_FilePath As String = CombinePathes(Context.SourcePath, Folder)
        Dim Dest_FilePath As String = CombinePathes(Context.DestinationPath, Folder)
        UpdateLabel(StatusData.SyncStep.Scan, Src_FilePath)

        Dim PropagateUpdates As Boolean = Handler.GetSetting(Of Boolean)(ProfileSetting.PropagateUpdates, True)
        Dim EmptyDirectories As Boolean = Handler.GetSetting(Of Boolean)(ProfileSetting.ReplicateEmptyDirectories, True)

        Dim InitialCount As Integer
        Dim IsSingularity As Boolean
        IsSingularity = Not IO.Directory.Exists(Dest_FilePath)

        If IsSingularity Then
            AddToSyncingList(Context.Source, New SyncingItem(Folder, TypeOfItem.Folder, Context.Action, False))
            Log.LogInfo(String.Format("SearchForUpdates: [New folder] ""{0}"" ({1})", Dest_FilePath, Folder))
        Else
            AddValidFile(Folder)
            Log.LogInfo(String.Format("SearchForUpdates: [Valid folder] ""{0}"" ({1})", Dest_FilePath, Folder))
        End If

        InitialCount = ValidFiles.Count

        Try
            For Each SourceFile As String In IO.Directory.GetFiles(Src_FilePath)
                Dim Suffix As String = If(CompressionEnabled(), Handler.GetSetting(Of String)(ProfileSetting.CompressionExt, ""), "")
                Dim DestinationFile As String = CombinePathes(Dest_FilePath, IO.Path.GetFileName(SourceFile) & Suffix)

                Log.LogInfo("Scanning " & SourceFile)
                'First check if the file is part of the synchronization profile.
                'Then, check whether it requires updating.
                If HasAcceptedFilename(SourceFile) Then
                    Dim DestinationExists As Boolean = IO.File.Exists(DestinationFile)
                    Dim RelativeFilePath As String = SourceFile.Substring(Context.SourcePath.Length)
                    If Not DestinationExists OrElse (PropagateUpdates AndAlso SourceIsMoreRecent(SourceFile, DestinationFile)) Then
                        AddToSyncingList(Context.Source, New SyncingItem(RelativeFilePath, TypeOfItem.File, Context.Action, DestinationExists), Suffix)
                        Log.LogInfo(String.Format("SearchForUpdates: {0} ""{1}"" ({2}).", If(DestinationExists, "[Update]", "[New File]"), SourceFile, SourceFile.Substring(Context.SourcePath.Length)))
                    Else
                        'Adds an entry to not delete this when cleaning up the other side.
                        AddValidFile(RelativeFilePath & Suffix)
                        Log.LogInfo(String.Format("SearchForUpdates: [Valid] ""{0}"" ({1})", SourceFile, SourceFile.Substring(Context.SourcePath.Length)))
                    End If
                Else
                    Log.LogInfo(String.Format("SearchForUpdates: [Invalid filename] ""{0}"" ({1})", SourceFile, SourceFile.Substring(Context.SourcePath.Length)))
                End If

                Status.FilesScanned += 1
                If ProgramConfig.GetProgramSetting(Of Boolean)(ProfileSetting.Forecast, False) Then Status.BytesScanned += GetSize(SourceFile) 'Degrades performance.
            Next
        Catch Ex As Exception
#If DEBUG Then
            Log.HandleError(Ex)
#End If
            'Error with entering the folder || Thread aborted.
        End Try

        If Recursive Then
            Try
                For Each SubFolder As String In IO.Directory.GetDirectories(Src_FilePath)
#If LINUX Then
                    If IsSymLink(SubFolder) Then Continue For
#End If
                    SearchForChanges(SubFolder.Substring(Context.SourcePath.Length), True, Context)
                Next
            Catch Ex As Exception
#If DEBUG Then
                Log.HandleError(Ex)
#End If
            End Try
        End If

        If InitialCount = ValidFiles.Count Then
            If Not EmptyDirectories Then
                'IsSingularity => Don't copy this folder over (not present yet)
                If IsSingularity Then
                    Status.FoldersToCreate -= 1
                    Status.TotalActionsCount -= 1
                    RemoveFromSyncingList(Context.Source)
                End If
                '(Could be Else =>) Delete it (aka don't mark it for preservation).
                'LATER: This could normally be safely put in an else case, since no folder can be a singularity (=not in dest) and a valid file (=it's in dest and should stay there).
                RemoveValidFile(Folder)
                'Problem: What if ancestors of a folder have been marked valid, and the folder is empty?
                'If the folder didn't exist, it's ancestors won't be created, since only the folder itself is added.
                'Yet if ancestors exist, should they be removed? Let's say NO for now.
            End If
        End If
    End Sub

    Private Sub SearchForCrap(ByVal Folder As String, ByVal Recursive As Boolean, ByVal Context As SyncingAction)
        If Not HasAcceptedDirname(Folder) Then Exit Sub

        'Here, Source is set to be the right folder, and dest to be the left folder
        Dim Src_FilePath As String = CombinePathes(Context.SourcePath, Folder)
        Dim Dest_FilePath As String = CombinePathes(Context.DestinationPath, Folder)
        UpdateLabel(StatusData.SyncStep.Scan, Src_FilePath)

        'Dim PropagateUpdates As Boolean = Handler.GetSetting(Of Boolean)(ConfigOptions.PropagateUpdates, True)
        'Dim EmptyDirectories As Boolean = Handler.GetSetting(Of Boolean)(ConfigOptions.ReplicateEmptyDirectories, False)

        Log.LogInfo(String.Format("=> Scanning folder ""{0}"" for files to delete.", Folder))
        Try
            For Each File As String In IO.Directory.GetFiles(Src_FilePath)
                Dim RelativeFName As String = File.Substring(Context.SourcePath.Length)
                If Not IsValidFile(RelativeFName) Then
                    AddToSyncingList(Context.Source, New SyncingItem(RelativeFName, TypeOfItem.File, Context.Action, False))
                    Log.LogInfo(String.Format("Cleanup: [Delete] ""{0}"" ({1})", File, RelativeFName))
                Else
                    Log.LogInfo(String.Format("Cleanup: [Keep] ""{0}"" ({1})", File, RelativeFName))
                End If

                Status.FilesScanned += 1
            Next
        Catch Ex As Exception
#If DEBUG Then
            Log.HandleError(Ex)
#End If
        End Try

        If Recursive Then
            Try
                For Each SubFolder As String In IO.Directory.GetDirectories(Src_FilePath)
#If LINUX Then
                    If IsSymLink(SubFolder) Then Continue For
#End If
                    SearchForCrap(SubFolder.Substring(Context.SourcePath.Length), True, Context)
                Next
            Catch Ex As Exception
#If DEBUG Then
                Log.HandleError(Ex)
#End If
            End Try
        End If

        ' Folder.Length = 0 <=> This is the root folder, not to be deleted.
        If Folder.Length <> 0 AndAlso Not IsValidFile(Folder) Then
            Log.LogInfo(String.Format("Cleanup: [Delete folder] ""{0}"" ({1}).", Dest_FilePath, Folder))
            AddToSyncingList(Context.Source, New SyncingItem(Folder, TypeOfItem.Folder, Context.Action, False))
        End If
    End Sub

    Private Sub SyncPreviewList(ByVal Side As SideOfSource, ByVal Count As Integer)
        If Count > 0 Then
            AddPreviewItem(SyncingList(Side)(SyncingList(Side).Count - 1), Side)
        ElseIf Count < 0 Then
            PreviewList.Items.RemoveAt(PreviewList.Items.Count - 1) 'The callers already take care of updating the folders counts correctly.
        End If
    End Sub

    Private Sub CopyFile(ByVal SourceFile As String, ByVal DestFile As String)
        Dim Compression As Boolean = CompressionEnabled()
        If Compression Then DestFile &= Handler.GetSetting(Of String)(ProfileSetting.CompressionExt, "")

        Log.LogInfo(String.Format("CopyFile: Source: {0}, Destination: {1}", SourceFile, DestFile))

        If IO.File.Exists(DestFile) Then IO.File.SetAttributes(DestFile, IO.FileAttributes.Normal)
        If Compression Then
            Static GZipCompressor As Compressor = LoadCompressionDll()
            GZipCompressor.CompressFile(SourceFile, DestFile, Sub(Progress As Long) Status.BytesCopied += Progress) ', ByRef ContinueRunning As Boolean) 'ContinueRunning = Not [STOP]
        Else
            If IO.File.Exists(DestFile) Then
                Try
                    Using TestForAccess As New IO.FileStream(SourceFile, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.None) : End Using 'Checks whether the file can be accessed before trying to copy it. This line was added because if the file is only partially locked, CopyFileEx starts copying it, then fails on the way, and deletes the destination.
                    IO.File.Copy(SourceFile, DestFile, True)
                Catch Ex As IO.IOException
                    Dim TempDest As String = DestFile & IO.Path.GetRandomFileName(), DestBack As String = DestFile & IO.Path.GetRandomFileName()
                    IO.File.Copy(SourceFile, TempDest, False) 'Don't overwrite, in case of a random filename collision.
                    IO.File.Move(DestFile, DestBack) : IO.File.Move(TempDest, DestFile)
                    IO.File.Delete(DestBack)
                End Try
            Else
                IO.File.Copy(SourceFile, DestFile)
            End If
        End If

        If Handler.GetSetting(Of Integer)(ProfileSetting.TimeOffset, 0) <> 0 Then 'Updating attributes is needed.
            Log.LogInfo("CopyFile: DST: Setting attributes to normal; current attributes: " & IO.File.GetAttributes(DestFile))
            IO.File.SetAttributes(DestFile, IO.FileAttributes.Normal) 'Tracker #2999436
            Log.LogInfo("CopyFile: DST: Setting last write time")
            'Reading must happen through IO.File.GetLastWriteTimeUtc(DestFile), because after the copy IO.File.GetLastWriteTimeUtc(SourceFile) may differ from IO.File.GetLastWriteTimeUtc(DestFile) (rounding, DST, ...)
            IO.File.SetLastWriteTimeUtc(DestFile, IO.File.GetLastWriteTimeUtc(DestFile).AddHours(Handler.GetSetting(Of Integer)(ProfileSetting.TimeOffset, 0)))
            Log.LogInfo("CopyFile: DST: Last write time set to " & IO.File.GetLastWriteTimeUtc(DestFile))
        End If

        Log.LogInfo("CopyFile: Setting attributes to " & IO.File.GetAttributes(SourceFile))
        IO.File.SetAttributes(DestFile, IO.File.GetAttributes(SourceFile))
        Log.LogInfo("CopyFile: Attributes set to " & IO.File.GetAttributes(DestFile))

        Status.CreatedFiles += 1
        If Not Compression Then Status.BytesCopied += GetSize(SourceFile)
        If Handler.GetSetting(Of Boolean)(ProfileSetting.Checksum, False) AndAlso Md5(SourceFile) <> Md5(DestFile) Then Throw New System.Security.Cryptography.CryptographicException("MD5 validation: failed.")
    End Sub
#End Region

#Region " Functions "
    Private Function HasAcceptedFilename(ByVal Path As String) As Boolean
        Try
            Select Case Handler.GetSetting(Of Integer)(ProfileSetting.Restrictions)
                'LATER: Add an option to allow for simultaneous inclusion and exclusion (useful because of regex patterns)
                Case 1
                    Return MatchesPattern(GetFileOrFolderName(Path), IncludedPatterns)
                Case 2
                    Return Not MatchesPattern(GetFileOrFolderName(Path), ExcludedPatterns)
            End Select
        Catch Ex As Exception
#If DEBUG Then
            Log.HandleError(Ex)
#End If
        End Try

        Return True
    End Function

    Private Function HasAcceptedDirname(ByVal Path As String) As Boolean
        Return Not MatchesPattern(Path, ExcludedDirPatterns)
    End Function

    Private Function CompressionEnabled() As Boolean
        Return Handler.GetSetting(Of String)(ProfileSetting.CompressionExt, "") <> "" 'AndAlso GetSize(File) > ConfigOptions.CompressionThreshold
    End Function

    Private Function SourceIsMoreRecent(ByVal Source As String, ByVal Destination As String) As Boolean 'Assumes Source and Destination exist.
        If (Not Handler.GetSetting(Of Boolean)(ProfileSetting.PropagateUpdates, True)) Then Return False

        Log.LogInfo(String.Format("SourceIsMoreRecent: {0}, {1}", Source, Destination))

        Dim SourceFATTime As Date = NTFSToFATTime(IO.File.GetLastWriteTimeUtc(Source)).AddHours(Handler.GetSetting(Of Integer)(ProfileSetting.TimeOffset, 0))
        Dim DestFATTime As Date = NTFSToFATTime(IO.File.GetLastWriteTimeUtc(Destination))
        Log.LogInfo(String.Format("SourceIsMoreRecent: S:({0}, {1}); D:({2}, {3})", FormatDate(IO.File.GetLastWriteTimeUtc(Source)), FormatDate(SourceFATTime), FormatDate(IO.File.GetLastWriteTimeUtc(Destination)), FormatDate(DestFATTime)))

        If Handler.GetSetting(Of Boolean)(ProfileSetting.FuzzyDstCompensation, False) Then
            Dim HoursDiff As Integer = CInt((SourceFATTime - DestFATTime).TotalHours)
            If Math.Abs(HoursDiff) = 1 Then DestFATTime = DestFATTime.AddHours(HoursDiff)
        End If

        'User-enabled checks
        If Handler.GetSetting(Of Boolean)(ProfileSetting.Checksum, False) AndAlso Md5(Source) <> Md5(Destination) Then Return True
        If Handler.GetSetting(Of Boolean)(ProfileSetting.CheckFileSize, False) AndAlso GetSize(Source) <> GetSize(Destination) Then Return True

        If Handler.GetSetting(Of Boolean)(ProfileSetting.StrictDateComparison, True) Then
            If SourceFATTime = DestFATTime Then Return False
        Else
            If Math.Abs((SourceFATTime - DestFATTime).TotalSeconds) <= 4 Then Return False 'Note: NTFSToFATTime introduces additional fuzziness (justifies the <= ('=')).
        End If
        Log.LogInfo("SourceIsMoreRecent: Filetimes differ")

        If SourceFATTime < DestFATTime AndAlso (Not Handler.GetSetting(Of Boolean)(ProfileSetting.StrictMirror, False)) Then Return False

        Return True
    End Function

#If LINUX Then
    Private Function IsSymLink(ByVal SubFolder As String) As Boolean
        If (IO.File.GetAttributes(SubFolder) And IO.FileAttributes.ReparsePoint) <> 0 Then
            Log.LogInfo(String.Format("Symlink detected: {0}; not following.", SubFolder))
            Return True
        End If
        Return False
    End Function
#End If
#End Region

#Region " Shared functions "
    Private Shared Function CombinePathes(ByVal Dir As String, ByVal File As String) As String 'COULDDO: Should be optimized; IO.Path?
        Return Dir.TrimEnd(ProgramSetting.DirSep) & ProgramSetting.DirSep & File.TrimStart(ProgramSetting.DirSep)
    End Function

    Private Shared Function GetExtension(ByVal File As String) As String
        Return File.Substring(File.LastIndexOf("."c) + 1) 'Not used when dealing with a folder.
    End Function

    Private Shared Function GetSize(ByVal File As String) As Long
        Return (New System.IO.FileInfo(File)).Length 'Faster than My.Computer.FileSystem.GetFileInfo().Length (See FileLen_Speed_Test.vb)
    End Function

    Private Shared Function LoadCompressionDll() As Compressor
        Dim DLL As Reflection.Assembly = Reflection.Assembly.LoadFrom(ProgramConfig.CompressionDll)

        For Each SubType As Type In DLL.GetTypes
            If GetType(Compressor).IsAssignableFrom(SubType) Then Return CType(Activator.CreateInstance(SubType), Compressor)
        Next

        Throw New ArgumentException("Invalid DLL: " & ProgramConfig.CompressionDll)
    End Function

    Private Shared Function MatchesPattern(ByVal PathOrFileName As String, ByRef Patterns As List(Of FileNamePattern)) As Boolean
        Dim Extension As String = GetExtension(PathOrFileName)

        For Each Pattern As FileNamePattern In Patterns 'LINUX: Problem with IgnoreCase
            Select Case Pattern.Type
                Case FileNamePattern.PatternType.FileExt
                    If String.Compare(Extension, Pattern.Pattern, True) = 0 Then Return True
                Case FileNamePattern.PatternType.FileName
                    If String.Compare(PathOrFileName, Pattern.Pattern, True) = 0 Then Return True
                Case FileNamePattern.PatternType.FolderName
                    If PathOrFileName.EndsWith(Pattern.Pattern, StringComparison.CurrentCultureIgnoreCase) Then Return True
                Case FileNamePattern.PatternType.Regex
                    If System.Text.RegularExpressions.Regex.IsMatch(PathOrFileName, Pattern.Pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase) Then Return True
            End Select
        Next

        Return False
    End Function

    Private Shared Function Md5(ByVal Path As String) As String
        Using DataStream As New IO.StreamReader(Path), CryptObject As New System.Security.Cryptography.MD5CryptoServiceProvider()
            Return Convert.ToBase64String(CryptObject.ComputeHash(DataStream.BaseStream))
        End Using
    End Function

    'TODO: This could be a useful function for NAS drives known to round NTFS timestamps, but currently only DLink does, and they do it incorrectly (there's a bug in their drivers)
    Private Shared Function RoundToSecond(ByVal NTFSTime As Date) As Date
        Return (New Date(NTFSTime.Year, NTFSTime.Month, NTFSTime.Day, NTFSTime.Hour, NTFSTime.Minute, NTFSTime.Second).AddSeconds(If(NTFSTime.Millisecond > 500, 1, 0)))
    End Function

    Private Shared Function NTFSToFATTime(ByVal NTFSTime As Date) As Date
        Return (New Date(NTFSTime.Year, NTFSTime.Month, NTFSTime.Day, NTFSTime.Hour, NTFSTime.Minute, NTFSTime.Second).AddSeconds(If(NTFSTime.Millisecond = 0, NTFSTime.Second Mod 2, 2 - (NTFSTime.Second Mod 2))))
    End Function
#End Region

#Region "Tests"
    Private Shared Function FormatDate(ByVal Value As Date) As String
#If DEBUG Then
        Return Value.ToString("yyyy/MM/dd hh:mm:ss.fff")
#End If
    End Function

#If DEBUG Then
    Structure DatePair
        Dim Ntfs, FAT As Date

        <Diagnostics.DebuggerStepThrough()>
        Sub New(ByVal NtfsTime As Date, ByVal FatTime As Date)
            Ntfs = NtfsTime
            FAT = FatTime
        End Sub
    End Structure

    Public Shared Sub Check_NTFSToFATTime()
        Check_StaticFATTimes()
        Check_HardwareFATTimes()
    End Sub

    Public Shared Sub Check_StaticFATTimes()
        System.Diagnostics.Debug.WriteLine("Starting hardcoded NTFS -> FAT tests")
        Dim Tests As New List(Of DatePair) From {New DatePair(#7:31:00 AM#, #7:31:00 AM#), New DatePair(#7:31:00 AM#.AddMilliseconds(1), #7:31:02 AM#), New DatePair(#7:31:01 AM#, #7:31:02 AM#), New DatePair(#7:31:01 AM#.AddMilliseconds(999), #7:31:02 AM#)}
        For Each Test As DatePair In Tests
            Dim Actual As Date = NTFSToFATTime(Test.Ntfs)
            Dim Result As String = String.Format("Check_NTFSToFATTime: {0} -> {1} ({2} expected) --> {3}", Test.Ntfs, Actual, Test.FAT, If(Actual = Test.FAT, "Ok", "Failed"))
            System.Diagnostics.Debug.WriteLine(Result)
        Next
        System.Diagnostics.Debug.WriteLine("Done!")
    End Sub

    Public Shared Sub Check_HardwareFATTimes()
        Using LogWriter As New IO.StreamWriter("C:\FatTimes.txt", False)
            LogWriter.WriteLine("Starting dynamic NTFS -> FAT tests")
            Dim Source As String = "C:\NtfsTests", Destination As String = "Z:\NtfsTests"
            If IO.Directory.Exists(Source) Then IO.Directory.Delete(Source, True)
            If IO.Directory.Exists(Destination) Then IO.Directory.Delete(Destination, True)

            IO.Directory.CreateDirectory(Source)
            IO.Directory.CreateDirectory(Destination)

            Dim BaseDate As Date = Date.Today.AddHours(8)
            Dim FormatString As String = "{0,-15}{1,-15}{2,-15}{3,-15}{4,-15}{5,-15}{6,-15}"

            LogWriter.WriteLine(String.Format(FormatString, "Input", "Source", "Dest (Created)", "Dest (Copied)", "ForecastedDate", "Rounded", "Equal?"))

            For ms As Integer = 0 To 61000 Step 71
                Dim InputDate As Date = BaseDate.AddMilliseconds(ms)
                Dim SourcePath As String = IO.Path.Combine(Source, ms.ToString)
                Dim DestPath_Created As String = IO.Path.Combine(Destination, ms.ToString & "-created")
                Dim DestPath_Copied As String = IO.Path.Combine(Destination, ms.ToString & "-copied")
                IO.File.Create(SourcePath).Close()
                IO.File.Create(DestPath_Created).Close()

                IO.File.SetLastWriteTime(SourcePath, InputDate)
                IO.File.SetLastWriteTime(DestPath_Created, InputDate)
                IO.File.Copy(SourcePath, DestPath_Copied)

                Dim SourceDate As Date = IO.File.GetLastWriteTime(SourcePath)
                Dim DestCreatedDate As Date = IO.File.GetLastWriteTime(DestPath_Created)
                Dim DestCopiedDate As Date = IO.File.GetLastWriteTime(DestPath_Copied)
                Dim ForecastedDate As Date = NTFSToFATTime(InputDate)
                Dim RoundedDate As Date = RoundToSecond(InputDate)
                Dim Equal As Boolean = InputDate = SourceDate And DestCreatedDate = DestCopiedDate And DestCopiedDate = ForecastedDate

                IO.File.Delete(SourcePath)
                IO.File.Delete(DestPath_Copied)
                IO.File.Delete(DestPath_Created)

                LogWriter.WriteLine(FormatString, FormatDate(InputDate), FormatDate(SourceDate), FormatDate(DestCreatedDate), FormatDate(DestCopiedDate), FormatDate(ForecastedDate), FormatDate(RoundedDate), Equal)
            Next

            IO.Directory.Delete(Source, True)
            IO.Directory.Delete(Destination, True)

            LogWriter.WriteLine("Done!")
        End Using
    End Sub
#End If
#End Region
End Class