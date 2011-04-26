Imports System.Windows.Forms

Public Class SchedulingForm
    Dim Handler As ProfileHandler

    Sub New(ByVal Name As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
#If CONFIG = "Linux" Then
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
#End If

        ' Add any initialization after the InitializeComponent() call.
        Handler = New ProfileHandler(Name)
    End Sub

    Private Sub SchedulingForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Translation.TranslateControl(Me)
        Time.CustomFormat = "HH" & Translation.Translate("\H_M_SEP") & "mm"
        WeekDay.Items.AddRange(Translation.Translate("\WEEK_DAYS").Split(";"c))
        If WeekDay.Items.Count > 0 Then WeekDay.SelectedIndex() = 0

        LoadToForm()
    End Sub

    Private Sub Enable_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Enable.CheckedChanged
        Panel.Enabled = Enable.Checked
        Catchup.Enabled = Enable.Checked
        TimeSelectionPanel.Enabled = Enable.Checked
    End Sub

    Sub LoadToForm()
        Enable.Checked = True
        Select Case Handler.Scheduler.Frequency
            Case ScheduleInfo.Freq.Never
                Enable.Checked = False
            Case Else
                Time.Value = New Date(2011, 1, 1, Handler.Scheduler.Hour, Handler.Scheduler.Minute, 0)

                Select Case Handler.Scheduler.Frequency
                    Case ScheduleInfo.Freq.Daily
                        DailyBtn.Checked = True
                    Case ScheduleInfo.Freq.Weekly
                        WeeklyBtn.Checked = True
                        WeekDay.SelectedIndex = Handler.Scheduler.WeekDay
                    Case ScheduleInfo.Freq.Monthly
                        MonthlyBtn.Checked = True
                        MonthDay.Value = Handler.Scheduler.MonthDay
                End Select
        End Select

        Handler.CopySetting(ConfigOptions.CatchUpSync, Catchup.Checked, True)
    End Sub

    Private Sub Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Save.Click
        Try
            ConfigHandler.RegisterBoot()

            If Not Enable.Checked Then
                Handler.Scheduler.Frequency = ScheduleInfo.Freq.Never
            Else
                Handler.Scheduler.Hour = Time.Value.Hour
                Handler.Scheduler.Minute = Time.Value.Minute
                Handler.Scheduler.WeekDay = WeekDay.SelectedIndex
                Handler.Scheduler.MonthDay = CInt(MonthDay.Value)
                Handler.Scheduler.Frequency = If(DailyBtn.Checked, ScheduleInfo.Freq.Daily, If(WeeklyBtn.Checked, ScheduleInfo.Freq.Weekly, ScheduleInfo.Freq.Monthly))
            End If

            Handler.SetSetting(Of Boolean)(ConfigOptions.CatchUpSync, Catchup.Checked)

            Handler.SaveScheduler()
            Handler.SaveConfigFile()
        Catch ex As Exception
            Interaction.ShowMsg(Translation.Translate("\REG_ERROR"), Translation.Translate("\ERROR"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Me.Close()
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Close()
    End Sub

    Private Sub Catchup_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Catchup.MouseEnter
        Interaction.ShowToolTip(CType(sender, Control))
    End Sub

    Private Sub Catchup_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Catchup.MouseLeave
        Interaction.HideToolTip(CType(sender, Control))
    End Sub
End Class
