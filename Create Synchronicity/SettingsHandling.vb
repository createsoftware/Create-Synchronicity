'This file is part of Create Synchronicity.
'
'Create Synchronicity is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
'Create Synchronicity is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with Create Synchronicity.  If not, see <http://www.gnu.org/licenses/>.
'Created by:	Clément Pit--Claudel.
'Web site:		http://synchronicity.sourceforge.net.

Friend Module Updates
    Public Sub CheckForUpdates(Optional ByVal RoutineCheck As Boolean = True)
        Dim UpdateClient As New Net.WebClient
        Try
            UpdateClient.Headers.Add("version", Application.ProductVersion)
#If CONFIG = "Linux" Then
            UpdateClient.Headers.Add("os", "Linux")
#Else
            UpdateClient.UseDefaultCredentials = True 'Needed? -- Does no harm
            UpdateClient.Proxy = System.Net.HttpWebRequest.DefaultWebProxy 'Tracker #2976549
            UpdateClient.Proxy.Credentials = Net.CredentialCache.DefaultCredentials
#End If
            Dim LatestVersion As String
            Dim Url As String = ProgramSetting.Website & If(CommandLine.RunAs = CommandLine.RunMode.Scheduler, "code/scheduler-version.txt", "code/version.txt")
            Dim SecondaryUrl As String = ProgramSetting.UserWeb & "code/synchronicity-version.txt"
            Try
                LatestVersion = UpdateClient.DownloadString(Url)
            Catch ex As Net.WebException
                LatestVersion = UpdateClient.DownloadString(SecondaryUrl)
            End Try

            If ((New Version(LatestVersion)) > (New Version(Application.ProductVersion))) Then
                If Interaction.ShowMsg(String.Format(Translation.Translate("\UPDATE_MSG"), Application.ProductVersion, LatestVersion), Translation.Translate("\UPDATE_TITLE"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Interaction.StartProcess(ProgramSetting.Website & "update.html")
                    If ProgramConfig.CanGoOn Then MainFormInstance.Invoke(New Action(AddressOf Application.Exit))
                End If
            Else
                If Not RoutineCheck Then Interaction.ShowMsg(Translation.Translate("\NO_UPDATES"), , , MessageBoxIcon.Information)
            End If
        Catch Ex As InvalidOperationException
            'Some form couldn't close properly because of thread accesses
            Interaction.ShowDebug(Ex.ToString)
        Catch Ex As Exception
            Interaction.ShowMsg(Translation.Translate("\UPDATE_ERROR") & Environment.NewLine & Ex.Message, Translation.Translate("\UPDATE_ERROR_TITLE"), , MessageBoxIcon.Error)
            Interaction.ShowDebug(Ex.Message & Environment.NewLine & Ex.StackTrace)
        Finally
            UpdateClient.Dispose()
        End Try
    End Sub
End Module

Friend Module Interaction
    Friend InvariantCulture As Globalization.CultureInfo = Globalization.CultureInfo.InvariantCulture
    Friend StatusIcon As NotifyIcon = New NotifyIcon() With {.BalloonTipTitle = "Create Synchronicity", .BalloonTipIcon = ToolTipIcon.Info}
    Private SharedToolTip As ToolTip = New ToolTip() With {.UseFading = False, .UseAnimation = False, .ToolTipIcon = ToolTipIcon.Info}

    Public Sub LoadStatusIcon()
        Static Loaded As Boolean = False

        If Not Loaded Then
            Loaded = True
            AddHandler StatusIcon.BalloonTipClicked, AddressOf Interaction.BallonClick
            Dim Assembly As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly()
            StatusIcon.Icon = New Drawing.Icon(Assembly.GetManifestResourceStream("CS.create-synchronicity-icon-16x16.ico"))
        End If
    End Sub

    Private Function RemoveNewLines(ByVal Msg As String) As String
        Return Msg.Replace(Environment.NewLine, " // ")
    End Function

    Public Sub ToggleStatusIcon(ByVal Status As Boolean)
        StatusIcon.Visible = Status And (Not CommandLine.Silent)
    End Sub

    Public Sub ShowBalloonTip(ByVal Msg As String, Optional ByVal File As String = "")
        If CommandLine.Silent Or Not StatusIcon.Visible Then
            ConfigHandler.LogAppEvent(String.Format("Interaction: Balloon tip discarded. The message was ""{0}"".", RemoveNewLines(Msg)))
            Exit Sub
        End If

        CurrentFileToOpen = File
        StatusIcon.BalloonTipText = Msg
        StatusIcon.ShowBalloonTip(2000)
    End Sub

    Public Sub ShowToolTip(ByVal Ctrl As Control)
        Dim T As TreeView = TryCast(Ctrl, TreeView)
        If T IsNot Nothing AndAlso Not T.CheckBoxes Then Exit Sub

        Dim Offset As Integer = If(TypeOf Ctrl Is RadioButton Or TypeOf Ctrl Is CheckBox, 12, 1)
        Dim Pair As String() = String.Format(CStr(Ctrl.Tag), Ctrl.Text).Split(";".ToCharArray, 2)

        Try
            Dim Pos As New Drawing.Point(0, Ctrl.Height + Offset)
            If Pair.GetLength(0) = 1 Then
                SharedToolTip.ToolTipTitle = ""
                SharedToolTip.Show(Pair(0), Ctrl, Pos)
            ElseIf Pair.GetLength(0) > 1 Then
                SharedToolTip.ToolTipTitle = Pair(0)
                SharedToolTip.Show(Pair(1), Ctrl, Pos)
            End If
        Catch ex As InvalidOperationException
            'See bug #3076129
        End Try
    End Sub

    Public Sub HideToolTip(ByVal sender As Control)
        SharedToolTip.Hide(sender)
    End Sub

    <Diagnostics.Conditional("Debug")>
    Public Sub ShowDebug(ByVal Text As String, Optional ByVal Caption As String = "", Optional ByVal Icon As MessageBoxIcon = MessageBoxIcon.Warning)
#If DEBUG Then
        ShowMsg(Text, Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
#End If
    End Sub

    Public Function ShowMsg(ByVal Text As String, Optional ByVal Caption As String = "", Optional ByVal Buttons As MessageBoxButtons = MessageBoxButtons.OK, Optional ByVal Icon As MessageBoxIcon = MessageBoxIcon.None) As DialogResult
        If CommandLine.Silent Then
            ConfigHandler.LogAppEvent(String.Format("Interaction: Message Box discarded with default answer. The message was ""{0}"", and the caption was ""{1}"".", RemoveNewLines(Text), RemoveNewLines(Caption)))
            Return DialogResult.OK
        End If

        Return MessageBox.Show(Text, Caption, Buttons, Icon)
    End Function

    Private CurrentFileToOpen As String = ""
    Private Sub BallonClick(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not CurrentFileToOpen = "" Then StartProcess(CurrentFileToOpen)
    End Sub

    Public Sub StartProcess(ByVal Address As String, Optional ByVal Args As String = "")
        Try
            Diagnostics.Process.Start(Address, Args)
        Catch
        End Try
    End Sub
End Module

Public NotInheritable Class ListViewColumnSorter
    Implements System.Collections.IComparer

    Public Order As SortOrder
    Public SortColumn As Integer

    Public Sub New(ByVal ColumnId As Integer)
        SortColumn = ColumnId
        Order = SortOrder.Ascending
    End Sub

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements Collections.IComparer.Compare
        Dim xl As ListViewItem = DirectCast(x, ListViewItem), yl As ListViewItem = DirectCast(y, ListViewItem)
        If xl.SubItems.Count <= SortColumn Or yl.SubItems.Count <= SortColumn Then
            Return 0
        Else
            Return If(Order = SortOrder.Ascending, 1, -1) * String.Compare(xl.SubItems(SortColumn).Text, yl.SubItems(SortColumn).Text, True)
        End If
    End Function
End Class
