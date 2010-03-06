Imports System.Windows.Forms

Public Class SchedulingForm
    Dim Handler As ProfileHandler
    Dim Translation As LanguageHandler = LanguageHandler.GetSingleton

    Sub New(ByVal Name As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Handler = New ProfileHandler(Name)
    End Sub

    Private Sub SchedulingForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Translation.TranslateControl(Me)
        Scheduling_WeekDay.Items.AddRange(Translation.Translate("\WEEK_DAYS").Split(";"))
        If Scheduling_WeekDay.Items.Count > 0 Then Scheduling_WeekDay.SelectedIndex() = 0

        LoadToForm()
    End Sub

    Private Sub Scheduling_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Scheduling_EveryDay.CheckedChanged, Scheduling_EveryWeek.CheckedChanged, Scheduling_EveryMonth.CheckedChanged
        Static Refreshing As Boolean = False

        If Refreshing Then Exit Sub
        Dim Checked As Boolean = CType(sender, RadioButton).Checked

        Refreshing = True
        Scheduling_EveryDay.Checked = False
        Scheduling_EveryWeek.Checked = False
        Scheduling_EveryMonth.Checked = False
        CType(sender, RadioButton).Checked = Checked
        Refreshing = False
    End Sub

    Private Sub Scheduling_Enable_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Scheduling_Enable.CheckedChanged
        Scheduling_Panel.Enabled = Scheduling_Enable.Checked
        Scheduling_TimeSelectionPanel.Enabled = Scheduling_Enable.Checked
    End Sub

    Sub LoadToForm()
        Scheduling_Enable.Checked = True
        Select Case Handler.Scheduler.Frequency
            Case ScheduleInfo.NEVER
                Scheduling_Enable.Checked = False
            Case Else
                Scheduling_Hour.Value = Handler.Scheduler.Hour
                Scheduling_Minute.Value = Handler.Scheduler.Minute

                Select Case Handler.Scheduler.Frequency
                    Case ScheduleInfo.DAILY
                        Scheduling_EveryDay.Checked = True
                    Case ScheduleInfo.WEEKLY
                        Scheduling_EveryWeek.Checked = True
                        Scheduling_WeekDay.SelectedIndex = Handler.Scheduler.WeekDay
                    Case ScheduleInfo.MONTHLY
                        Scheduling_EveryMonth.Checked = True
                        Scheduling_MonthDay.Value = Handler.Scheduler.MonthDay
                    Case Else
                        'TODO
                End Select
        End Select
    End Sub

    Sub SaveFromForm()
    End Sub

    Private Sub Scheduling_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Scheduling_Save.Click
        If Not Scheduling_Enable.Checked Then
            Handler.Scheduler.Frequency = ScheduleInfo.NEVER
        Else
            Handler.Scheduler.Hour = Scheduling_Hour.Value
            Handler.Scheduler.Minute = Scheduling_Minute.Value
            Handler.Scheduler.WeekDay = Scheduling_WeekDay.SelectedIndex
            Handler.Scheduler.MonthDay = Scheduling_MonthDay.Value
            Handler.Scheduler.Frequency = If(Scheduling_EveryDay.Checked, ScheduleInfo.DAILY, If(Scheduling_EveryWeek.Checked, ScheduleInfo.WEEKLY, ScheduleInfo.MONTHLY))
        End If

        Handler.SaveScheduler()
        Handler.SaveConfigFile()
        Me.Close()
    End Sub

    Private Sub Scheduling_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Scheduling_Cancel.Click
        Me.Close()
    End Sub
End Class
