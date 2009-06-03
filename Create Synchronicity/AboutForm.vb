Public Class AboutForm
    Private Sub About_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        About_VersionInfo.Text = About_VersionInfo.Text.Replace("%version%", Application.ProductVersion)
        Dim LinkAreaStart As Integer = About_VersionInfo.Text.IndexOf("(") + 1
        About_VersionInfo.LinkArea = New LinkArea(LinkAreaStart, About_VersionInfo.Text.Length - LinkAreaStart - 1)
    End Sub

    Private Sub About_LinkToProductPage_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles About_LinkToProductPage.LinkClicked
        Diagnostics.Process.Start("http://synchronicity.sourceforge.net/")
    End Sub

    Private Sub About_LinkToWebsite_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles About_LinkToWebsite.LinkClicked
        Diagnostics.Process.Start("http://createsoftware.users.sourceforge.net/")
    End Sub

    Private Sub About_VersionInfo_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles About_VersionInfo.LinkClicked
        'Diagnostics.Process.Start("http://coversearch.sourceforge.net/create-coversearch/vercheck/ver=" & Application.ProductVersion)
    End Sub

    Private Sub About_ContactLink_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles About_ContactLink.LinkClicked
        Diagnostics.Process.Start("http://createsoftware.users.sourceforge.net/")
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Diagnostics.Process.Start("http://www.gnu.org/licenses/gpl.html")
    End Sub
End Class