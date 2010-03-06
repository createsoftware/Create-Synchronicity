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
    End Sub

    Private Sub Scheduling_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Scheduling_EveryDay.CheckedChanged, Scheduling_EveryWeek.CheckedChanged, Scheduling_EveryMonth.CheckedChanged
        Static Refreshing As Boolean = False

        If Refreshing Then Exit Sub
        Dim Checked As Boolean = CType(sender, RadioButton).Checked

        Refreshing = True
        Uncheck_RadioButtons()
        CType(sender, RadioButton).Checked = Checked
        Refreshing = False
    End Sub

    Private Sub Scheduling_Enable_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Scheduling_Enable.CheckedChanged
        Uncheck_RadioButtons()
        Scheduling_Panel.Enabled = Scheduling_Enable.Checked
        Scheduling_TimeSelectionPanel.Enabled = Scheduling_Enable.Checked
    End Sub

    Sub Uncheck_RadioButtons()
        Scheduling_EveryDay.Checked = False
        Scheduling_EveryWeek.Checked = False
        Scheduling_EveryMonth.Checked = False
    End Sub
End Class
