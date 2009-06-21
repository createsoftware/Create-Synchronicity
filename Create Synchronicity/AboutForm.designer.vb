'This file is part of Create Synchronicity.
'
'Create Synchronicity is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
'Create Synchronicity is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with Create Synchronicity.  If not, see <http://www.gnu.org/licenses/>.
'Created by:	Clément Pit--Claudel.
'Web site:		http://synchronicity.sourceforge.net.

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AboutForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AboutForm))
        Me.About_Logo = New System.Windows.Forms.PictureBox
        Me.About_FlowLayoutPanel = New System.Windows.Forms.FlowLayoutPanel
        Me.About_LinkToProductPage = New System.Windows.Forms.LinkLabel
        Me.About_LinkToWebsite = New System.Windows.Forms.LinkLabel
        Me.About_VersionInfo = New System.Windows.Forms.LinkLabel
        Me.About_ContactLink = New System.Windows.Forms.LinkLabel
        Me.About_LinkToLicense = New System.Windows.Forms.LinkLabel
        Me.About_BugReport = New System.Windows.Forms.LinkLabel
        CType(Me.About_Logo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.About_FlowLayoutPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'About_Logo
        '
        Me.About_Logo.Image = CType(resources.GetObject("About_Logo.Image"), System.Drawing.Image)
        Me.About_Logo.Location = New System.Drawing.Point(12, 12)
        Me.About_Logo.Name = "About_Logo"
        Me.About_Logo.Size = New System.Drawing.Size(128, 129)
        Me.About_Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.About_Logo.TabIndex = 0
        Me.About_Logo.TabStop = False
        '
        'About_FlowLayoutPanel
        '
        Me.About_FlowLayoutPanel.Controls.Add(Me.About_LinkToProductPage)
        Me.About_FlowLayoutPanel.Controls.Add(Me.About_LinkToWebsite)
        Me.About_FlowLayoutPanel.Controls.Add(Me.About_VersionInfo)
        Me.About_FlowLayoutPanel.Controls.Add(Me.About_ContactLink)
        Me.About_FlowLayoutPanel.Controls.Add(Me.About_LinkToLicense)
        Me.About_FlowLayoutPanel.Controls.Add(Me.About_BugReport)
        Me.About_FlowLayoutPanel.Location = New System.Drawing.Point(146, 12)
        Me.About_FlowLayoutPanel.Name = "About_FlowLayoutPanel"
        Me.About_FlowLayoutPanel.Size = New System.Drawing.Size(328, 129)
        Me.About_FlowLayoutPanel.TabIndex = 1
        '
        'About_LinkToProductPage
        '
        Me.About_LinkToProductPage.Location = New System.Drawing.Point(3, 0)
        Me.About_LinkToProductPage.Name = "About_LinkToProductPage"
        Me.About_LinkToProductPage.Size = New System.Drawing.Size(301, 20)
        Me.About_LinkToProductPage.TabIndex = 0
        Me.About_LinkToProductPage.TabStop = True
        Me.About_LinkToProductPage.Text = "Create Synchronicity"
        '
        'About_LinkToWebsite
        '
        Me.About_LinkToWebsite.LinkArea = New System.Windows.Forms.LinkArea(18, 6)
        Me.About_LinkToWebsite.Location = New System.Drawing.Point(3, 20)
        Me.About_LinkToWebsite.Name = "About_LinkToWebsite"
        Me.About_LinkToWebsite.Size = New System.Drawing.Size(301, 20)
        Me.About_LinkToWebsite.TabIndex = 1
        Me.About_LinkToWebsite.TabStop = True
        Me.About_LinkToWebsite.Text = "An application by Create (Clément Pit--Claudel)"
        Me.About_LinkToWebsite.UseCompatibleTextRendering = True
        '
        'About_VersionInfo
        '
        Me.About_VersionInfo.LinkArea = New System.Windows.Forms.LinkArea(19, 18)
        Me.About_VersionInfo.Location = New System.Drawing.Point(3, 40)
        Me.About_VersionInfo.Name = "About_VersionInfo"
        Me.About_VersionInfo.Size = New System.Drawing.Size(301, 20)
        Me.About_VersionInfo.TabIndex = 2
        Me.About_VersionInfo.TabStop = True
        Me.About_VersionInfo.Text = "Version %version% (Search for updates)"
        Me.About_VersionInfo.UseCompatibleTextRendering = True
        '
        'About_ContactLink
        '
        Me.About_ContactLink.LinkArea = New System.Windows.Forms.LinkArea(0, 18)
        Me.About_ContactLink.Location = New System.Drawing.Point(3, 60)
        Me.About_ContactLink.Name = "About_ContactLink"
        Me.About_ContactLink.Size = New System.Drawing.Size(301, 20)
        Me.About_ContactLink.TabIndex = 3
        Me.About_ContactLink.TabStop = True
        Me.About_ContactLink.Text = "Contact the author (createcorpsoftware@yahoo.fr)"
        Me.About_ContactLink.UseCompatibleTextRendering = True
        '
        'About_LinkToLicense
        '
        Me.About_LinkToLicense.LinkArea = New System.Windows.Forms.LinkArea(15, 7)
        Me.About_LinkToLicense.Location = New System.Drawing.Point(3, 80)
        Me.About_LinkToLicense.Name = "About_LinkToLicense"
        Me.About_LinkToLicense.Size = New System.Drawing.Size(301, 20)
        Me.About_LinkToLicense.TabIndex = 4
        Me.About_LinkToLicense.TabStop = True
        Me.About_LinkToLicense.Text = "Licensed under GNU GPL"
        Me.About_LinkToLicense.UseCompatibleTextRendering = True
        '
        'About_BugReport
        '
        Me.About_BugReport.LinkArea = New System.Windows.Forms.LinkArea(0, 12)
        Me.About_BugReport.Location = New System.Drawing.Point(3, 100)
        Me.About_BugReport.Name = "About_BugReport"
        Me.About_BugReport.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.About_BugReport.Size = New System.Drawing.Size(301, 20)
        Me.About_BugReport.TabIndex = 5
        Me.About_BugReport.TabStop = True
        Me.About_BugReport.Text = "Report a bug"
        '
        'AboutForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(486, 153)
        Me.Controls.Add(Me.About_FlowLayoutPanel)
        Me.Controls.Add(Me.About_Logo)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "AboutForm"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "About"
        CType(Me.About_Logo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.About_FlowLayoutPanel.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents About_Logo As System.Windows.Forms.PictureBox
    Friend WithEvents About_FlowLayoutPanel As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents About_LinkToProductPage As System.Windows.Forms.LinkLabel
    Friend WithEvents About_LinkToWebsite As System.Windows.Forms.LinkLabel
    Friend WithEvents About_VersionInfo As System.Windows.Forms.LinkLabel
    Friend WithEvents About_ContactLink As System.Windows.Forms.LinkLabel
    Friend WithEvents About_LinkToLicense As System.Windows.Forms.LinkLabel
    Friend WithEvents About_BugReport As System.Windows.Forms.LinkLabel
End Class
