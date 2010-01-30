'This file is part of Create Synchronicity.
'
'Create Synchronicity is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
'Create Synchronicity is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with Create Synchronicity.  If not, see <http://www.gnu.org/licenses/>.
'Created by:	Clément Pit--Claudel.
'Web site:		http://synchronicity.sourceforge.net.

Public Class AboutForm
    Private Sub About_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Translation As LanguageHandler = LanguageHandler.GetSingleton
        Translation.TranslateControl(Me)

        About_VersionInfo.Text = About_VersionInfo.Text.Replace("%version%", Application.ProductVersion)
        About_VersionInfo.LinkArea = New LinkArea(About_VersionInfo.Text.IndexOf("(") + 1, About_VersionInfo.Text.Length - (About_VersionInfo.Text.IndexOf("(") + 1) - 1)
        ConfigOptions.LoadProgramSettings()
        About_Updates.Checked = ConfigOptions.GetProgramSetting(ConfigOptions.AutoUpdates, "False")
    End Sub

    Private Sub About_LinkToProductPage_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles About_LinkToProductPage.LinkClicked
        Diagnostics.Process.Start("http://synchronicity.sourceforge.net/")
    End Sub

    Private Sub About_LinkToWebsite_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles About_LinkToWebsite.LinkClicked
        Diagnostics.Process.Start("http://createsoftware.users.sourceforge.net/")
    End Sub

    Private Sub About_VersionInfo_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles About_VersionInfo.LinkClicked
        ConfigOptions.CheckForUpdates(False)
    End Sub

    Private Sub About_ContactLink_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles About_ContactLink.LinkClicked
        Diagnostics.Process.Start("http://createsoftware.users.sourceforge.net/")
    End Sub

    Private Sub About_LinkToLicense_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles About_LinkToLicense.LinkClicked
        Diagnostics.Process.Start("http://www.gnu.org/licenses/gpl.html")
    End Sub

    Private Sub About_BugReport_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles About_BugReport.LinkClicked
        Diagnostics.Process.Start("http://sourceforge.net/tracker/?group_id=264348&atid=1130882")
    End Sub

    Private Sub About_Updates_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles About_Updates.CheckedChanged
        If About_Updates.Checked Then
            ConfigOptions.SetProgramSetting(ConfigOptions.AutoUpdates, "True")
        Else
            ConfigOptions.SetProgramSetting(ConfigOptions.AutoUpdates, "False")
        End If
    End Sub

    Private Sub AboutForm_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        ConfigOptions.SaveProgramSettings()
    End Sub
End Class
