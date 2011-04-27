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
