﻿'This file is part of Create Synchronicity.
'
'Create Synchronicity is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
'Create Synchronicity is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with Create Synchronicity.  If not, see <http://www.gnu.org/licenses/>.
'Created by:	Clément Pit--Claudel.
'Web site:		http://synchronicity.sourceforge.net.

Public Class SynchronizeForm
    Dim Log As LogHandler
    Dim Handler As ProfileHandler

    Dim ValidFiles As New Dictionary(Of String, Boolean)
    Dim SyncingList As New Dictionary(Of SideOfSource, List(Of SyncingItem))
    Dim IncludedPatterns As New List(Of FileNamePattern)
    Dim ExcludedPatterns As New List(Of FileNamePattern)
    Dim ExcludedDirPatterns As New List(Of FileNamePattern)

    Dim Quiet As Boolean 'This Quiet parameter is not a duplicate ; it is used when eg the scheduler needs to tell the form to keep quiet, although the "quiet" command-line flag wasn't used.
    Dim Catchup As Boolean 'Indicates whether this operation was started due to catchup rules.
    Dim NoStop As Boolean = CommandLine.NoStop
    Dim [STOP] As Boolean
    Dim Failed As Boolean : Dim FailureMsg As String

    Structure Status
        Dim Junk As Boolean
        Shared StartTime As Date
        Shared BytesCopied As Long
        Shared FilesScanned As Long
        Shared ActionsDone As Integer
        Shared CreatedFiles As Integer
        Shared CreatedFolders As Integer
        Shared FilesToCreate As Integer
        Shared FoldersToCreate As Integer
        Shared DeletedFiles As Integer
        Shared DeletedFolders As Integer
        Shared FilesToDelete As Integer
        Shared FoldersToDelete As Integer
        Shared TotalActionsCount As Integer
        Shared CurrentStep As Integer
        Shared TimeElapsed As TimeSpan
        Shared MillisecondsSpeed As Double
    End Structure

    Dim ColumnSorter As ListViewColumnSorter
    Dim Preview As Boolean, PreviewFinished As Boolean

    Dim FullSyncThread As Threading.Thread
    Dim FirstSyncThread As Threading.Thread
    Dim SecondSyncThread As Threading.Thread

    Delegate Sub Action() 'LATER: replace with .Net 4.0 standards.
    Delegate Sub TaskDoneCallBack(ByVal Id As Integer)
    Delegate Sub LabelCallBack(ByVal Id As Integer, ByVal Text As String)
    Delegate Sub SetElapsedTimeCallBack(ByVal CurrentTimeSpan As TimeSpan)
    Delegate Sub ProgressSetMaxCallBack(ByVal Id As Integer, ByVal Max As Integer)
    Delegate Sub SetProgressCallBack(ByVal Id As Integer, ByVal Progress As Integer)

    Friend Event OnFormClosedAfterSyncFinished()

    'Not evaluating file size gives better performance (See FileLen_Speed_Test.vb for tests):
    'With size evaluation: 1'20, 46'', 36'', 35'', 31''
    'Without:                    41'', 42'', 26'', 29''

#Region " Events "
    Sub New(ByVal ConfigName As String, ByVal DisplayPreview As Boolean, ByVal _Quiet As Boolean, Optional ByVal _Catchup As Boolean = False)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        [STOP] = False
        Failed = False

        Quiet = _Quiet
        Catchup = _Catchup
        Preview = DisplayPreview
        PreviewFinished = Not Preview

        SyncBtn.Enabled = False
        SyncBtn.Visible = Preview

        Status.BytesCopied = 0
        Status.FilesScanned = 0
        Status.ActionsDone = 0
        Status.CreatedFiles = 0
        Status.CreatedFolders = 0
        Status.FilesToCreate = 0
        Status.FoldersToCreate = 0
        Status.DeletedFiles = 0
        Status.DeletedFolders = 0
        Status.FilesToDelete = 0
        Status.FoldersToDelete = 0
        Status.TotalActionsCount = 0
        Status.CurrentStep = 1
        Status.StartTime = Date.Now ' NOTE: This call should be useless; it however seems that when the messagebox.show method is called when a profile is not found, the syncingtimecounter starts ticking. This is not suitable, but until the cause is found there this call remains, for display consistency.

        Log = New LogHandler(ConfigName)
        Handler = New ProfileHandler(ConfigName)

        ColumnSorter = New ListViewColumnSorter(3)
        PreviewList.ListViewItemSorter = ColumnSorter

        FileNamePattern.LoadPatternsList(IncludedPatterns, Handler.GetSetting(ConfigOptions.IncludedTypes, ""))
        FileNamePattern.LoadPatternsList(ExcludedPatterns, Handler.GetSetting(ConfigOptions.ExcludedTypes, ""))
        FileNamePattern.LoadPatternsList(ExcludedDirPatterns, Handler.GetSetting(ConfigOptions.ExcludedFolders, ""), True)

        FullSyncThread = New Threading.Thread(AddressOf Synchronize)
        FirstSyncThread = New Threading.Thread(AddressOf Do_FirstStep)
        SecondSyncThread = New Threading.Thread(AddressOf Do_SecondThirdStep)

        Me.CreateHandle()
        Translation.TranslateControl(Me)
        Me.Icon = ProgramConfig.GetIcon()
        Me.Text = String.Format(Me.Text, Handler.ProfileName, Handler.GetSetting(ConfigOptions.Source), Handler.GetSetting(ConfigOptions.Destination)) 'Feature requests #3037548, #3055740
    End Sub

    Function StartSynchronization(ByVal CalledShowModal As Boolean) As Boolean
        ProgramConfig.CanGoOn = False

        If Quiet Then
            Me.Visible = False

            Interaction.StatusIcon.ContextMenuStrip = Nothing
            AddHandler Interaction.StatusIcon.Click, AddressOf StatusIcon_Click

            Interaction.LoadStatusIcon()
            Interaction.StatusIcon.Text = Translation.Translate("\RUNNING")

            Interaction.ShowStatusIcon()
            If Catchup Then
                Dim LastRun As Date = Handler.GetLastRun()
                Interaction.ShowBalloonTip(String.Format(Translation.Translate("\CATCHING_UP"), Handler.ProfileName, (Date.Now - LastRun).Days, (Date.Now - LastRun).Hours, LastRun.ToString))
            Else
                Interaction.ShowBalloonTip(String.Format(Translation.Translate("\RUNNING_TASK"), Handler.ProfileName))
            End If
        Else
            If Not CalledShowModal Then Me.Visible = True 'Me.Show?
        End If

        FailureMsg = ""
        Dim IsValid As Boolean = Handler.ValidateConfigFile(False, True, Quiet, FailureMsg)
        Failed = Not IsValid

        If IsValid Then
            'ProgramConfig.IncrementSyncsCount() 'TODO: Enable. Problem of concurrent savings of the main config file.
            Handler.SetLastRun() 'TODO: Move to the end of the sync? 'Only set LastRun when the synchronization actually happens. But there would be a problem with the return value.
            If Preview Then
                PreviewList.Items.Clear()
                FirstSyncThread.Start()
            Else
                FullSyncThread.Start()
            End If
        Else
            Log.SaveAndDispose(Handler.GetSetting(ConfigOptions.Source), Handler.GetSetting(ConfigOptions.Destination), FailureMsg)
            EndAll()
        End If

        Return IsValid
    End Function

    Private Sub SynchronizeForm_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Control Then
            If e.KeyCode = Keys.L AndAlso Status.CurrentStep = -1 Then
                Interaction.StartProcess(ProgramConfig.GetLogPath(Handler.ProfileName))
            ElseIf e.KeyCode = Keys.D And PreviewList.SelectedIndices.Count <> 0 Then
                Dim DiffProgram As String = ProgramConfig.GetProgramSetting(ConfigOptions.DiffProgram, "")
                Dim DiffArguments As String = ProgramConfig.GetProgramSetting(ConfigOptions.DiffArguments, "")
                Dim NewFile As String = "", OldFile As String = ""
                If Not GetPathFromSelectedItem(NewFile, OldFile) Then Exit Sub
                Try
                    If DiffProgram <> "" AndAlso IO.File.Exists(OldFile) AndAlso IO.File.Exists(NewFile) Then Diagnostics.Process.Start(DiffProgram.Trim, DiffArguments.Replace("%o", OldFile).Replace("%n", NewFile))
                Catch Ex As Exception
                    Interaction.ShowMsg("Error loading diff: " & Ex.ToString)
                End Try
            End If
        End If
    End Sub

    Private Sub SynchronizeForm_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        EndAll()
        ProgramConfig.CanGoOn = True
        Interaction.StatusIcon.ContextMenuStrip = MainForm.StatusIconMenu
        RemoveHandler Interaction.StatusIcon.Click, AddressOf StatusIcon_Click

        Interaction.StatusIcon.Text = Translation.Translate("\WAITING")
        RaiseEvent OnFormClosedAfterSyncFinished()
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

        SecondSyncThread.Start()
    End Sub

    Private Sub StatusIcon_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.Visible = Not Me.Visible
        Me.WindowState = FormWindowState.Normal
    End Sub

    Private Sub SynchronizeForm_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Me.WindowState = FormWindowState.Minimized And Quiet Then Me.Visible = False
    End Sub

    Private Sub SyncingTimeCounter_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SyncingTimeCounter.Tick
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
        If Not GetPathFromSelectedItem(Source, Dest) Then Exit Sub

        If IO.File.Exists(Source) Or IO.Directory.Exists(Source) Then Interaction.StartProcess(If(My.Computer.Keyboard.CtrlKeyDown, Source.Substring(0, Source.LastIndexOf(ConfigOptions.DirSep)), Source))
    End Sub

    Function GetPathFromSelectedItem(ByRef Source As String, ByRef Dest As String) As Boolean
        If PreviewList.SelectedIndices.Count = 0 Then Return False

        Dim CurItem As ListViewItem = PreviewList.SelectedItems(0)
        If CurItem.Tag Is Nothing OrElse CurItem.SubItems.Count < 3 Then Return False

        Dim Left As String, Right As String
        Left = ProfileHandler.TranslatePath(Handler.GetSetting(ConfigOptions.Source)) & CurItem.SubItems(3).Text
        Right = ProfileHandler.TranslatePath(Handler.GetSetting(ConfigOptions.Destination)) & CurItem.SubItems(3).Text

        Select Case CurItem.Tag.ToString
            Case "LR"
                Source = Left : Dest = Right
            Case "RL"
                Source = Right : Dest = Left
            Case Else
                'In errors list
                Return False
        End Select

        Return True
    End Function

    Function FormatSize(ByVal Size As Long) As String
        Select Case Size
            Case Is > 1024 * 1000 * 1000
                Return Math.Round(Size / (1024 * 1000 * 1000), 2).ToString & " GB"
            Case Is > 1024 * 1000
                Return Math.Round(Size / (1024 * 1000), 2).ToString & " MB"
            Case Is > 1024
                Return Math.Round(Size / 1024, 2).ToString & " kB"
            Case Else
                Return Math.Round(Size, 2).ToString & " B"
        End Select
    End Function

    Sub UpdateStatuses()
        Status.TimeElapsed = DateTime.Now - Status.StartTime
        ElapsedTime.Text = If(Status.TimeElapsed.Hours = 0, "", Status.TimeElapsed.Hours.ToString & "h, ") & If(Status.TimeElapsed.Minutes = 0, "", Status.TimeElapsed.Minutes.ToString & "m, ") & Status.TimeElapsed.Seconds.ToString & "s."

        If Status.TimeElapsed.TotalMilliseconds = 0 Then Status.TimeElapsed = New System.TimeSpan(1)

        If Status.CurrentStep = 1 Then
            Speed.Text = Math.Round(Status.FilesScanned / (Status.TimeElapsed.TotalMilliseconds / 1000)).ToString & " files/s"
        Else
            Status.MillisecondsSpeed = Status.BytesCopied / (Status.TimeElapsed.TotalMilliseconds / 1000)
            Speed.Text = FormatSize(Status.MillisecondsSpeed) & "/s"
        End If

        If Not Status.CurrentStep = 1 Then
            Done.Text = Status.ActionsDone & "/" & Status.TotalActionsCount
            FilesDeleted.Text = Status.DeletedFiles & "/" & Status.FilesToDelete
            FilesCreated.Text = Status.CreatedFiles & "/" & Status.FilesToCreate & " (" & FormatSize(Status.BytesCopied) & ")"
            FoldersDeleted.Text = Status.DeletedFolders & "/" & Status.FoldersToDelete
            FoldersCreated.Text = Status.CreatedFolders & "/" & Status.FoldersToCreate
        End If
    End Sub
#End Region

#Region " Processes interaction "
    Sub UpdateLabel(ByVal Id As Integer, ByVal Text As String)
        Dim StatusText As String = Text
        If Text.Length > 30 Then
            StatusText = "..." & Text.Substring(Text.Length - 30, 30)
        End If

        Select Case Id
            Case 1
                Step1StatusLabel.Text = Text
                Interaction.StatusIcon.Text = String.Format(Translation.Translate("\STEP_1_STATUS"), StatusText)
            Case 2
                Step2StatusLabel.Text = Text
                Interaction.StatusIcon.Text = String.Format(Translation.Translate("\STEP_2_STATUS"), Step2ProgressBar.Value, Step2ProgressBar.Maximum, StatusText)
            Case 3
                Step3StatusLabel.Text = Text
                Interaction.StatusIcon.Text = String.Format(Translation.Translate("\STEP_3_STATUS"), Step3ProgressBar.Value, Step3ProgressBar.Maximum, StatusText)
        End Select
    End Sub

    Sub SetProgess(ByVal Id As Integer, ByVal Progress As Integer)
        Select Case Id
            Case 1
                If Step1ProgressBar.Value + Progress < Step1ProgressBar.Maximum Then Step1ProgressBar.Value += Progress
            Case 2
                If Step2ProgressBar.Value + Progress < Step2ProgressBar.Maximum Then Step2ProgressBar.Value += Progress
            Case 3
                If Step3ProgressBar.Value + Progress < Step3ProgressBar.Maximum Then Step3ProgressBar.Value += Progress
        End Select
    End Sub

    Sub SetMaxProgess(ByVal Id As Integer, ByVal MaxValue As Integer)
        Select Case Id
            Case 1
                If MaxValue = -1 Then
                    Step1ProgressBar.Style = ProgressBarStyle.Marquee
                Else
                    Step1ProgressBar.Style = ProgressBarStyle.Blocks
                    Step1ProgressBar.Value = 0
                    Step1ProgressBar.Maximum = MaxValue
                End If
            Case 2
                If MaxValue = -1 Then
                    Step2ProgressBar.Style = ProgressBarStyle.Marquee
                Else
                    Step2ProgressBar.Style = ProgressBarStyle.Blocks
                    Step2ProgressBar.Value = 0
                    Step2ProgressBar.Maximum = MaxValue
                End If
            Case 3
                If MaxValue = -1 Then
                    Step3ProgressBar.Style = ProgressBarStyle.Marquee
                Else
                    Step3ProgressBar.Style = ProgressBarStyle.Blocks
                    Step3ProgressBar.Value = 0
                    Step3ProgressBar.Maximum = MaxValue
                End If
        End Select
    End Sub

    Sub TaskDone(ByVal Id As Integer)
        If Not Status.CurrentStep = Id Then Exit Sub 'Prevents infinite exit loop.

        Select Case Id
            Case 1
                Status.CurrentStep = 2
                UpdateLabel(1, Translation.Translate("\FINISHED"))
                Step1ProgressBar.Maximum = 100
                Step1ProgressBar.Value = Step1ProgressBar.Maximum
                Step1ProgressBar.Style = ProgressBarStyle.Blocks
                If Not PreviewFinished Then
                    UpdatePreviewList()
                    StopBtn.Text = StopBtn.Tag.ToString.Split(";"c)(1)
                End If
                UpdateStatuses()
                SyncingTimeCounter.Stop()

            Case 2
                Status.CurrentStep = 3
                UpdateLabel(2, Translation.Translate("\FINISHED"))
                Step2ProgressBar.Maximum = 100
                Step2ProgressBar.Value = Step2ProgressBar.Maximum
                Step2ProgressBar.Style = ProgressBarStyle.Blocks

            Case 3
                Status.CurrentStep = -1 'Done.
                UpdateLabel(3, Translation.Translate("\FINISHED"))
                Step3ProgressBar.Maximum = 100
                Step3ProgressBar.Value = Step3ProgressBar.Maximum
                Step3ProgressBar.Style = ProgressBarStyle.Blocks

                UpdateStatuses()
                If Log.Errors.Count > 0 Or Failed Then
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
                        If Failed Then
                            Interaction.ShowBalloonTip(FailureMsg)
                        Else
                            Interaction.ShowBalloonTip(String.Format(Translation.Translate("\SYNCED_W_ERRORS"), Handler.ProfileName), ProgramConfig.GetLogPath(Handler.ProfileName))
                        End If
                    End If
                Else
                    If Quiet Then Interaction.ShowBalloonTip(String.Format(Translation.Translate("\SYNCED_OK"), Handler.ProfileName), ProgramConfig.GetLogPath(Handler.ProfileName))
                End If

                SyncingTimeCounter.Stop()
                Log.SaveAndDispose(Handler.GetSetting(ConfigOptions.Source), Handler.GetSetting(ConfigOptions.Destination))

                If ((Quiet And Not Me.Visible) Or NoStop) Then
                    Me.Close()
                Else
                    StopBtn.Text = StopBtn.Tag.ToString.Split(";"c)(1)
                End If
        End Select
    End Sub

    Sub UpdatePreviewList()
        PreviewList.Visible = True

        If Not PreviewList.Items.Count = 0 Then
            For Each C As ColumnHeader In PreviewList.Columns
                C.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent)
            Next
        End If

        PreviewFinished = True
        If Not [STOP] Then SyncBtn.Enabled = True
    End Sub

    Private Sub AddPreviewItem(ByRef Item As SyncingItem, ByVal Side As SideOfSource)
        Dim ListItem As New ListViewItem
        ListItem = PreviewList.Items.Add(Item.FormatType())
        ListItem.SubItems.Add(Item.FormatAction())
        ListItem.SubItems.Add(Item.FormatDirection(Side))
        ListItem.SubItems.Add(Item.Path)

        ListItem.Tag = If(Side = SideOfSource.Left, "LR", "RL")

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

    Sub LaunchTimer()
        Status.BytesCopied = 0
        Status.StartTime = DateTime.Now
        SyncingTimeCounter.Start()
    End Sub

    Sub EndAll()
        [STOP] = True
        FullSyncThread.Abort()
        FirstSyncThread.Abort() : SecondSyncThread.Abort()
        TaskDone(1) : TaskDone(2) : TaskDone(3)
    End Sub
#End Region

#Region " Syncing code "
    Sub Synchronize()
        Do_FirstStep()
        Do_SecondThirdStep()
    End Sub

    Sub Do_FirstStep()
        Dim Context As New SyncingAction
        Dim TaskDoneDelegate As New TaskDoneCallBack(AddressOf TaskDone)

        'Pass 1: Create actions L->R for files/folder copy, and mark dest files that should be kept
        'Pass 2: Create actions R->L for files/folder copy/deletion, based on what was marked as ValidFile, aka based on what should be kept.

        SyncingList.Clear()
        SyncingList.Add(SideOfSource.Left, New List(Of SyncingItem))
        SyncingList.Add(SideOfSource.Right, New List(Of SyncingItem))

        ValidFiles.Clear()

        Dim Source As String = ProfileHandler.TranslatePath(Handler.GetSetting(ConfigOptions.Source))
        Dim Destination As String = ProfileHandler.TranslatePath(Handler.GetSetting(ConfigOptions.Destination))

        Me.Invoke(New Action(AddressOf LaunchTimer))
        Context.Source = SideOfSource.Left
        Context.SourcePath = Source
        Context.DestinationPath = Destination
        Context.Action = TypeOfAction.Copy
        Init_Synchronization(Handler.LeftCheckedNodes, Context)

        Context.Source = SideOfSource.Right
        Context.SourcePath = Destination
        Context.DestinationPath = Source
        Select Case Handler.GetSetting(ConfigOptions.Method)
            Case "0"
                Context.Action = TypeOfAction.Delete
                Init_Synchronization(Handler.RightCheckedNodes, Context)
            Case "2"
                Context.Action = TypeOfAction.Copy
                Init_Synchronization(Handler.RightCheckedNodes, Context)
        End Select
        Me.Invoke(TaskDoneDelegate, 1)

        'NOTE: [to sysadmins] (March 13, 2010) -- Moved to FAQ
    End Sub

    Sub Do_SecondThirdStep()
        Dim TaskDoneDelegate As New TaskDoneCallBack(AddressOf TaskDone)
        Dim SetProgessDelegate As New SetProgressCallBack(AddressOf SetProgess)
        Dim ProgessSetMaxCallBack As New ProgressSetMaxCallBack(AddressOf SetMaxProgess)
        Dim LabelDelegate As New LabelCallBack(AddressOf UpdateLabel)

        Dim Left As String = ProfileHandler.TranslatePath(Handler.GetSetting(ConfigOptions.Source))
        Dim Right As String = ProfileHandler.TranslatePath(Handler.GetSetting(ConfigOptions.Destination))

        Me.Invoke(New Action(AddressOf LaunchTimer))
        Me.Invoke(ProgessSetMaxCallBack, New Object() {2, SyncingList(SideOfSource.Left).Count})
        Do_Task(SideOfSource.Left, SyncingList(SideOfSource.Left), Left, Right, 2)
        Me.Invoke(TaskDoneDelegate, 2)

        Me.Invoke(ProgessSetMaxCallBack, New Object() {3, SyncingList(SideOfSource.Right).Count})
        Do_Task(SideOfSource.Right, SyncingList(SideOfSource.Right), Right, Left, 3)
        Me.Invoke(TaskDoneDelegate, 3)
    End Sub

    '"Source" is "current side", with the corresponding side set to "Side"
    Private Sub Do_Task(ByVal Side As SideOfSource, ByRef ListOfActions As List(Of SyncingItem), ByVal Source As String, ByVal Destination As String, ByVal CurrentStep As Integer)
        Dim SetProgessDelegate As New SetProgressCallBack(AddressOf SetProgess)
        Dim LabelDelegate As New LabelCallBack(AddressOf UpdateLabel)

        For Each Entry As SyncingItem In ListOfActions
            Try
                Me.Invoke(LabelDelegate, New Object() {CurrentStep, Destination & Entry.Path})

                Select Case Entry.Type
                    Case TypeOfItem.File
                        Select Case Entry.Action
                            Case TypeOfAction.Copy
                                CopyFile(Entry.Path, Source, Destination)
                            Case TypeOfAction.Delete
                                IO.File.SetAttributes(Source & Entry.Path, IO.FileAttributes.Normal)
                                IO.File.Delete(Source & Entry.Path)
                                Status.DeletedFiles += 1
                        End Select

                    Case TypeOfItem.Folder
                        Select Case Entry.Action
                            Case TypeOfAction.Copy
                                IO.Directory.CreateDirectory(Destination & Entry.Path)
                                IO.Directory.SetCreationTimeUtc(Destination & Entry.Path, IO.Directory.GetCreationTimeUtc(Source & Entry.Path).AddHours(Handler.GetSetting(ConfigOptions.TimeOffset, "0")))
                                Status.CreatedFolders += 1
                            Case TypeOfAction.Delete
                                If IO.Directory.GetFiles(Source & Entry.Path).GetLength(0) = 0 Then
                                    Try
                                        IO.Directory.Delete(Source & Entry.Path)
                                    Catch ex As Exception
                                        Dim DirInfo As New IO.DirectoryInfo(Source & Entry.Path)
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
                Log.HandleError(ex, Source & Entry.Path)
                Log.LogAction(Entry, Side, False) 'Side parameter is only used for logging purposes.
            End Try

            If Not [STOP] Then Me.Invoke(SetProgessDelegate, New Object() {CurrentStep, 1})
        Next
    End Sub

    Private Sub Init_Synchronization(ByRef FoldersList As Dictionary(Of String, Boolean), ByVal Context As SyncingAction)
        For Each Folder As String In FoldersList.Keys
#If DEBUG Then
            Log.LogInfo(String.Format("=> Scanning top-level folders from side {0}: Folder ""{1}""", Context.SourcePath, Folder))
#End If
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

    Sub AddValidFile(ByVal File As String)
        If Not IsValidFile(File) Then ValidFiles.Add(File.ToLower, Nothing)
    End Sub

    Private Sub AddValidAncestors(ByVal Folder As String)
#If DEBUG Then
        Log.LogInfo(String.Format("Folder ""{0}"" is a top level folder, adding it's ancestors.", Folder))
#End If
        Dim CurrentAncestor As New System.Text.StringBuilder
        Dim Ancestors As New List(Of String)(Folder.Split(New Char() {ConfigOptions.DirSep}, StringSplitOptions.RemoveEmptyEntries))

        For Depth As Integer = 0 To (Ancestors.Count - 1) - 1 'The last ancestor is the folder itself, and will be added in SearchForChanges.
            CurrentAncestor.Append(ConfigOptions.DirSep).Append(Ancestors(Depth))
            AddValidFile(CurrentAncestor.ToString)
#If DEBUG Then
            Log.LogInfo(String.Format("Folder ""{0}"" is an ancestor of a top level folder, has been marked valid, and will not be deleted.", CurrentAncestor.ToString))
#End If
        Next
    End Sub

    Private Sub RemoveValidFile(ByVal File As String)
        If IsValidFile(File) Then ValidFiles.Remove(File.ToLower)
    End Sub

    Private Function IsValidFile(ByVal File As String) As Boolean
        Return ValidFiles.ContainsKey(File.ToLower)
    End Function

    Sub RemoveFromSyncingList(ByVal Side As SideOfSource)
        ValidFiles.Remove(SyncingList(Side)(SyncingList(Side).Count - 1).Path)
        SyncingList(Side).RemoveAt(SyncingList(Side).Count - 1)
        SyncPreviewList(Side, -1)
    End Sub

    Private Function CombinePathes(ByVal Dir As String, ByVal File As String) As String 'LATER: Should be optimized; IO.Path?
        Return Dir.TrimEnd(IO.Path.DirectorySeparatorChar) & IO.Path.DirectorySeparatorChar & File.TrimStart(IO.Path.DirectorySeparatorChar)
    End Function

    ' This procedure searches for changes in the source directory, in regards
    ' to the status of the destination directory.
    Private Sub SearchForChanges(ByVal Folder As String, ByVal Recursive As Boolean, ByVal Context As SyncingAction)
        If Not HasAcceptedDirname(Folder) Then Exit Sub

        Dim LabelDelegate As New LabelCallBack(AddressOf UpdateLabel)

        Dim Src_FilePath As String = CombinePathes(Context.SourcePath, Folder)
        Dim Dest_FilePath As String = CombinePathes(Context.DestinationPath, Folder)
        Me.Invoke(LabelDelegate, New Object() {1, Src_FilePath})

        Dim PropagateUpdates As Boolean = (Handler.GetSetting(ConfigOptions.PropagateUpdates, "True") = "True")
        Dim EmptyDirectories As Boolean = (Handler.GetSetting(ConfigOptions.ReplicateEmptyDirectories, "False") = "True")

        Dim InitialCount As Integer
        Dim IsSingularity As Boolean
        IsSingularity = Not IO.Directory.Exists(Dest_FilePath)

        If IsSingularity Then
            AddToSyncingList(Context.Source, New SyncingItem(Folder, TypeOfItem.Folder, Context.Action, False))
#If DEBUG Then
            Log.LogInfo(String.Format("""{0}"" ({1}) [Folder] added to the list, will be copied.", Dest_FilePath, Folder))
#End If
        Else
            AddValidFile(Folder)
#If DEBUG Then
            Log.LogInfo(String.Format("""{0}"" ({1}) [Folder] added to the list, will not be deleted.", Dest_FilePath, Folder))
#End If
        End If

        InitialCount = ValidFiles.Count

        Try
            For Each SourceFile As String In IO.Directory.GetFiles(Src_FilePath)
                Dim Suffix As String = If(ShouldCompress(SourceFile), Handler.GetSetting(ConfigOptions.CompressionExt, ""), "")
                Dim DestinationFile As String = CombinePathes(Dest_FilePath, IO.Path.GetFileName(SourceFile) & Suffix)

#If DEBUG Then
                Log.LogInfo("Scanning " & SourceFile)
#End If

                'First check if the file is part of the synchronization profile.
                'Then, check whether it requires updating.
                If HasAcceptedFilename(SourceFile) Then
                    Dim DestinationExists As Boolean = IO.File.Exists(DestinationFile)
                    Dim RelativeFilePath As String = SourceFile.Substring(Context.SourcePath.Length)
                    If Not DestinationExists OrElse (PropagateUpdates AndAlso SourceIsMoreRecent(SourceFile, DestinationFile)) Then
                        AddToSyncingList(Context.Source, New SyncingItem(RelativeFilePath, TypeOfItem.File, Context.Action, DestinationExists), Suffix)
#If DEBUG Then
                        Log.LogInfo(String.Format("""{0}"" ({1}) added to the list, will be copied.", SourceFile, SourceFile.Substring(Context.SourcePath.Length)))
#End If
                    Else
                        'Adds an entry to not delete this when cleaning up the other side.
                        AddValidFile(RelativeFilePath & Suffix)
#If DEBUG Then
                        Log.LogInfo(String.Format("""{0}"" ({1}) added to the list, will not be deleted.", SourceFile, SourceFile.Substring(Context.SourcePath.Length)))
#End If
                    End If
#If DEBUG Then
                Else
                    Log.LogInfo(String.Format("""{0}"" ({1}) NOT added to the list, will be deleted, since its extension is not valid.", SourceFile, SourceFile.Substring(Context.SourcePath.Length)))
#End If
                End If

                Status.FilesScanned += 1
                'Status.BytesCopied += GetSize(SourceFile)
            Next
        Catch Ex As Exception
#If DEBUG Then
            Log.HandleError(Ex)
#End If
            'Error with entering the folder
        End Try

        If Recursive Then
            Try
                For Each SubFolder As String In IO.Directory.GetDirectories(Src_FilePath)
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
        Dim LabelDelegate As New LabelCallBack(AddressOf UpdateLabel)

        Dim Src_FilePath As String = CombinePathes(Context.SourcePath, Folder)
        Dim Dest_FilePath As String = CombinePathes(Context.DestinationPath, Folder)
        Me.Invoke(LabelDelegate, New Object() {1, Src_FilePath})

        'Dim PropagateUpdates As Boolean = (Handler.GetSetting(ConfigOptions.PropagateUpdates, "True") = "True")
        'Dim EmptyDirectories As Boolean = (Handler.GetSetting(ConfigOptions.ReplicateEmptyDirectories, "False") = "True"

#If DEBUG Then
        Log.LogInfo(String.Format("=> Scanning folder ""{0}"" for files to delete.", Folder))
#End If
        Try
            For Each File As String In IO.Directory.GetFiles(Src_FilePath)
                Dim RelativeFName As String = File.Substring(Context.SourcePath.Length)
                If Not IsValidFile(RelativeFName) Then
                    AddToSyncingList(Context.Source, New SyncingItem(RelativeFName, TypeOfItem.File, Context.Action, False))
#If DEBUG Then
                    Log.LogInfo(String.Format("""{0}"" ({1}) does NOT belong to the list, so will be deleted.", File, RelativeFName))
                Else
                    Log.LogInfo(String.Format("""{0}"" ({1}) belongs to the list, so won't be deleted.", File, RelativeFName))
#End If
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
#If DEBUG Then
            Log.LogInfo(String.Format("""{0}"" ({1}) [Folder] does NOT belong to the list, so will be deleted.", Dest_FilePath, Folder))
#End If
            AddToSyncingList(Context.Source, New SyncingItem(Folder, TypeOfItem.Folder, Context.Action, False))
        End If
    End Sub

    Sub SyncPreviewList(ByVal Side As SideOfSource, ByVal Count As Integer)
        If Count > 0 Then
            AddPreviewItem(SyncingList(Side)(SyncingList(Side).Count - 1), Side)
        ElseIf Count < 0 Then
            PreviewList.Items.RemoveAt(PreviewList.Items.Count - 1)
        End If
    End Sub

    Function HasAcceptedFilename(ByVal Path As String) As Boolean
        Try
            Select Case Handler.GetSetting(ConfigOptions.Restrictions)
                'LATER: Add an option to allow for simultaneous inclusion and exclusion (useful because of regex patterns)
                Case "1"
                    Return MatchesPattern(GetFileOrFolderName(Path), IncludedPatterns)
                Case "2"
                    Return Not MatchesPattern(GetFileOrFolderName(Path), ExcludedPatterns)
            End Select
        Catch Ex As Exception
#If DEBUG Then
            Log.HandleError(Ex)
#End If
        End Try

        Return True
    End Function

    Function HasAcceptedDirname(ByVal Path As String) As Boolean
        Return Not MatchesPattern(Path, ExcludedDirPatterns)
    End Function

    Sub CopyFile(ByVal Path As String, ByVal Source As String, ByVal Dest As String)
        Dim SourceFile As String = Source & Path : Dim DestFile As String = Dest & Path 'TODO: CombinePathes?

        Dim Compression As Boolean = ShouldCompress(SourceFile)
        If Compression Then DestFile &= Handler.GetSetting(ConfigOptions.CompressionExt, "")

        If IO.File.Exists(DestFile) Then IO.File.SetAttributes(DestFile, IO.FileAttributes.Normal)
        If Compression Then
            Static GZipCompressor As Compressor = LoadCompressionDll()
            GZipCompressor.CompressFile(SourceFile, DestFile, Sub(Progress As Long) Status.BytesCopied += Progress) ', ByRef ContinueRunning As Boolean) 'ContinueRunning = Not [STOP]
        Else
            Using TestForAccess As New IO.FileStream(SourceFile, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.None) : End Using 'Checks whether the file can be accessed before trying to copy it. This line was added because if the file is only partially locked, CopyFileEx starts copying it, then fails on the way, and deletes the destination.
            IO.File.Copy(SourceFile, DestFile, True)
        End If

        If Handler.GetSetting(ConfigOptions.TimeOffset, "0") <> "0" Then 'Updating attributes is needed.
#If DEBUG Then
            Log.LogInfo("DST: """ & DestFile & """ has been copied with attributes " & IO.File.GetAttributes(DestFile) & " , now setting attributes to Normal before setting Last Write Time")
#End If
            IO.File.SetAttributes(DestFile, IO.FileAttributes.Normal) 'Tracker #2999436
#If DEBUG Then
            Log.LogInfo("DST: Attributes set to" & IO.File.GetAttributes(DestFile) & " on """ & Path & """, now setting last write time.")
#End If
            'TODO: Check if s/DestFile/SourceFile would change something (I guess it would)
            IO.File.SetLastWriteTimeUtc(DestFile, IO.File.GetLastWriteTimeUtc(DestFile).AddHours(Handler.GetSetting(ConfigOptions.TimeOffset, "0")))
        End If

#If DEBUG Then
        Log.LogInfo("CopyFile: now setting attributes on """ & Path & """")
#End If
        IO.File.SetAttributes(DestFile, IO.File.GetAttributes(SourceFile))

        Status.CreatedFiles += 1
        If Not Compression Then Status.BytesCopied += GetSize(SourceFile)
        If Handler.GetSetting(ConfigOptions.Checksum, "False") AndAlso Md5(SourceFile) <> Md5(DestFile) Then Throw New System.Security.Cryptography.CryptographicException("MD5 validation: failed.")
    End Sub
#End Region

#Region " Functions "
    Private Function GetFileOrFolderName(ByVal Path As String) As String
        Return Path.Substring(Path.LastIndexOf(ConfigOptions.DirSep) + 1) 'IO.Path.* -> Bad because of separate file/folder handling.
    End Function

    Private Function GetExtension(ByVal File As String) As String
        Return File.Substring(File.LastIndexOf("."c) + 1) 'Not used when dealing with a folder.
    End Function

    Function GetSize(ByVal File As String) As Long
        Return (New System.IO.FileInfo(File)).Length 'Faster than My.Computer.FileSystem.GetFileInfo().Length (See FileLen_Speed_Test.vb)
    End Function

    Function ShouldCompress(ByVal File As String) As Boolean
        Return Handler.GetSetting(ConfigOptions.CompressionExt, "") <> "" 'AndAlso GetSize(File) > ConfigOptions.CompressionThreshold
    End Function

    Function LoadCompressionDll() As Compressor
        Dim DLL As Reflection.Assembly = Reflection.Assembly.LoadFrom(ProgramConfig.CompressionDll)

        For Each SubType As Type In DLL.GetTypes
            If GetType(Compressor).IsAssignableFrom(SubType) Then Return CType(Activator.CreateInstance(SubType), Compressor)
        Next

        Throw New ArgumentException("Invalid DLL: " & ProgramConfig.CompressionDll)
    End Function

    Private Function MatchesPattern(ByVal PathOrFileName As String, ByRef Patterns As List(Of FileNamePattern)) As Boolean
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

    Function Md5(ByVal Path As String) As String
        Dim CryptObject As New System.Security.Cryptography.MD5CryptoServiceProvider()
        Using DataStream As New IO.StreamReader(Path)
            Return Convert.ToBase64String(CryptObject.ComputeHash(DataStream.BaseStream))
        End Using
    End Function

    Function SourceIsMoreRecent(ByVal Source As String, ByVal Destination As String) As Boolean 'Assumes Source and Destination exist.
        If Handler.GetSetting(ConfigOptions.PropagateUpdates, "True") = "False" Then Return False

        Dim SourceFATTime As Date = NTFSToFATTime(IO.File.GetLastWriteTimeUtc(Source)).AddHours(Handler.GetSetting(ConfigOptions.TimeOffset, "0"))
        Dim DestFATTime As Date = NTFSToFATTime(IO.File.GetLastWriteTimeUtc(Destination))

        If Handler.GetSetting(ConfigOptions.FuzzyDstCompensation, "False") = "True" Then
            Dim HoursDiff As Integer = CInt((SourceFATTime - DestFATTime).TotalHours)
            If Math.Abs(HoursDiff) = 1 Then DestFATTime = DestFATTime.AddHours(HoursDiff)
        End If

        'User-enabled checks
        If Handler.GetSetting(ConfigOptions.Checksum, "False") AndAlso Md5(Source) <> Md5(Destination) Then Return True
        If Handler.GetSetting(ConfigOptions.CheckFileSize, "False") AndAlso GetSize(Source) <> GetSize(Destination) Then Return True

        If Handler.GetSetting(ConfigOptions.StrictDateComparison, "True") = "True" Then
            If SourceFATTime = DestFATTime Then Return False
        Else
            If Math.Abs((SourceFATTime - DestFATTime).TotalSeconds) <= 4 Then Return False 'Note: NTFSToFATTime introduces additional fuzzyness (justifies the <= ('=')).
        End If

        If SourceFATTime < DestFATTime And Handler.GetSetting(ConfigOptions.StrictMirror, "False") = "False" Then Return False

        Return True
    End Function

    Function NTFSToFATTime(ByVal NTFSTime As Date) As Date
        'Return NTFSTime.AddSeconds(If(NTFSTime.Millisecond = 0, NTFSTime.Second Mod 2, 2 - (NTFSTime.Second Mod 2))).AddMilliseconds(-NTFSTime.Millisecond)
        Return (New Date(NTFSTime.Year, NTFSTime.Month, NTFSTime.Day, NTFSTime.Hour, NTFSTime.Minute, NTFSTime.Second).AddSeconds(If(NTFSTime.Millisecond = 0, NTFSTime.Second Mod 2, 2 - (NTFSTime.Second Mod 2))))
    End Function
#End Region
End Class