'This file is part of Create Synchronicity.
'
'Create Synchronicity is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
'Create Synchronicity is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with Create Synchronicity.  If not, see <http://www.gnu.org/licenses/>.
'Created by:	Clément Pit--Claudel.
'Web site:		http://synchronicity.sourceforge.net.

Public Class AboutForm
    Dim Translation As LanguageHandler = LanguageHandler.GetSingleton

    Private Sub SetLinkArea(ByVal Link As LinkLabel)
        If Link.Text.IndexOf("\") = -1 Or Link.Text.IndexOf("/") = -1 Then Exit Sub

        Dim Area As New LinkArea
        Area.Start = Link.Text.IndexOf("\")
        Link.Text = Link.Text.Remove(Area.Start, 1)
        Area.Length = Link.Text.IndexOf("/") - Area.Start
        Link.Text = Link.Text.Remove(Area.Start + Area.Length, 1)
        Link.LinkArea = Area
    End Sub

    Private Sub About_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Translation.TranslateControl(Me)

        About_VersionInfo.Text = About_VersionInfo.Text.Replace("%version%", Application.ProductVersion)

        SetLinkArea(About_BugReport)
        SetLinkArea(About_ContactLink)
        SetLinkArea(About_LinkToLicense)
        SetLinkArea(About_LinkToProductPage)
        SetLinkArea(About_LinkToWebsite)
        SetLinkArea(About_VersionInfo)

        About_LanguagesList.Items.Clear()
        For Each File As String In IO.Directory.GetFiles(ConfigOptions.LanguageRootDir)
            About_LanguagesList.Items.Add(File.Remove(File.LastIndexOf(".")).Substring(File.LastIndexOf("\") + 1))
        Next

        ConfigOptions.LoadProgramSettings()
        About_Updates.Checked = ConfigOptions.GetProgramSetting(ConfigOptions.AutoUpdates, "False")
        About_LanguagesList.SelectedIndex = About_LanguagesList.Items.IndexOf(ConfigOptions.GetProgramSetting(ConfigOptions.Language, ""))
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

    Private Sub AboutForm_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        If ConfigOptions.GetProgramSetting(ConfigOptions.Language, ConfigOptions.DefaultLanguage) <> About_LanguagesList.SelectedItem.ToString Then
            MessageBox.Show(Translation.Translate("\RESTART"), Translation.Translate("\RESTART_NEEDED"))
        End If

        ConfigOptions.SetProgramSetting(ConfigOptions.AutoUpdates, About_Updates.Checked)
        ConfigOptions.SetProgramSetting(ConfigOptions.Language, About_LanguagesList.SelectedItem)
        ConfigOptions.SaveProgramSettings()
    End Sub
End Class
