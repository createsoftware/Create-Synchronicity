Imports System.Windows.Forms

Public Class SchedulingForm
    Dim Handler As ProfileHandler

    Sub New(ByVal Name As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Handler = New ProfileHandler(Name)
    End Sub

    Private Sub SchedulingForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Translation.TranslateControl(Me)
        Scheduling_WeekDay.Items.AddRange(Translation.Translate("\WEEK_DAYS").Split(";"c))
        If Scheduling_WeekDay.Items.Count > 0 Then Scheduling_WeekDay.SelectedIndex() = 0

        LoadToForm()
    End Sub

    Private Sub Scheduling_Enable_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Scheduling_Enable.CheckedChanged
        Scheduling_Panel.Enabled = Scheduling_Enable.Checked
        Scheduling_Catchup.Enabled = Scheduling_Enable.Checked
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
                        DailyBtn.Checked = True
                    Case ScheduleInfo.WEEKLY
                        WeeklyBtn.Checked = True
                        Scheduling_WeekDay.SelectedIndex = Handler.Scheduler.WeekDay
                    Case ScheduleInfo.MONTHLY
                        MonthlyBtn.Checked = True
                        Scheduling_MonthDay.Value = Handler.Scheduler.MonthDay
                End Select
        End Select

        Handler.SetSetting(ConfigOptions.CatchUpSync, Scheduling_Catchup.Checked, True)
    End Sub

    Private Sub Scheduling_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Scheduling_Save.Click
        Try
            ConfigHandler.RegisterBoot()

            If Not Scheduling_Enable.Checked Then
                Handler.Scheduler.Frequency = ScheduleInfo.NEVER
            Else
                Handler.Scheduler.Hour = Scheduling_Hour.Value
                Handler.Scheduler.Minute = Scheduling_Minute.Value
                Handler.Scheduler.WeekDay = Scheduling_WeekDay.SelectedIndex
                Handler.Scheduler.MonthDay = Scheduling_MonthDay.Value
                Handler.Scheduler.Frequency = If(DailyBtn.Checked, ScheduleInfo.DAILY, If(WeeklyBtn.Checked, ScheduleInfo.WEEKLY, ScheduleInfo.MONTHLY))
            End If

            Handler.SetSetting(ConfigOptions.CatchUpSync, Scheduling_Catchup.Checked)

            Handler.SaveScheduler()
            Handler.SaveConfigFile()
        Catch ex As Exception
            Interaction.ShowMsg(Translation.Translate("\REG_ERROR"), Translation.Translate("\ERROR"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Me.Close()
    End Sub

    Private Sub Scheduling_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Scheduling_Cancel.Click
        Me.Close()
        Application.Exit()
    End Sub

    Private Sub Scheduling_Catchup_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Scheduling_Catchup.MouseEnter
        Interaction.ShowToolTip(CType(sender, Control))
    End Sub

    Private Sub Scheduling_Catchup_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Scheduling_Catchup.MouseLeave
        Interaction.HideToolTip(CType(sender, Control))
    End Sub
End Class
