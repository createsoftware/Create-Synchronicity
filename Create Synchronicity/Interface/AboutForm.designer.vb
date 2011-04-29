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
        Me.Logo = New System.Windows.Forms.PictureBox()
        Me.FlowLayoutPanel = New System.Windows.Forms.FlowLayoutPanel()
        Me.LinkToProductPage = New System.Windows.Forms.LinkLabel()
        Me.LinkToWebsite = New System.Windows.Forms.LinkLabel()
        Me.VersionInfo = New System.Windows.Forms.LinkLabel()
        Me.ContactLink = New System.Windows.Forms.LinkLabel()
        Me.LinkToLicense = New System.Windows.Forms.LinkLabel()
        Me.BugReport = New System.Windows.Forms.LinkLabel()
        Me.UpdatesOption = New System.Windows.Forms.CheckBox()
        Me.LanguageLabel = New System.Windows.Forms.Label()
        Me.LanguagesList = New System.Windows.Forms.ComboBox()
        CType(Me.Logo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FlowLayoutPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'Logo
        '
        Me.Logo.Image = CType(resources.GetObject("Logo.Image"), System.Drawing.Image)
        Me.Logo.Location = New System.Drawing.Point(12, 12)
        Me.Logo.Name = "Logo"
        Me.Logo.Size = New System.Drawing.Size(128, 129)
        Me.Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.Logo.TabIndex = 0
        Me.Logo.TabStop = False
        '
        'FlowLayoutPanel
        '
        Me.FlowLayoutPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FlowLayoutPanel.AutoScroll = True
        Me.FlowLayoutPanel.Controls.Add(Me.LinkToProductPage)
        Me.FlowLayoutPanel.Controls.Add(Me.LinkToWebsite)
        Me.FlowLayoutPanel.Controls.Add(Me.VersionInfo)
        Me.FlowLayoutPanel.Controls.Add(Me.ContactLink)
        Me.FlowLayoutPanel.Controls.Add(Me.LinkToLicense)
        Me.FlowLayoutPanel.Controls.Add(Me.BugReport)
        Me.FlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.FlowLayoutPanel.Location = New System.Drawing.Point(146, 12)
        Me.FlowLayoutPanel.Name = "FlowLayoutPanel"
        Me.FlowLayoutPanel.Size = New System.Drawing.Size(368, 129)
        Me.FlowLayoutPanel.TabIndex = 0
        Me.FlowLayoutPanel.WrapContents = False
        '
        'LinkToProductPage
        '
        Me.LinkToProductPage.AutoSize = True
        Me.LinkToProductPage.Location = New System.Drawing.Point(3, 0)
        Me.LinkToProductPage.MinimumSize = New System.Drawing.Size(0, 20)
        Me.LinkToProductPage.Name = "LinkToProductPage"
        Me.LinkToProductPage.Size = New System.Drawing.Size(127, 20)
        Me.LinkToProductPage.TabIndex = 0
        Me.LinkToProductPage.TabStop = True
        Me.LinkToProductPage.Text = "Create Synchronicity"
        '
        'LinkToWebsite
        '
        Me.LinkToWebsite.AutoSize = True
        Me.LinkToWebsite.Location = New System.Drawing.Point(3, 20)
        Me.LinkToWebsite.MinimumSize = New System.Drawing.Size(0, 20)
        Me.LinkToWebsite.Name = "LinkToWebsite"
        Me.LinkToWebsite.Size = New System.Drawing.Size(144, 20)
        Me.LinkToWebsite.TabIndex = 1
        Me.LinkToWebsite.TabStop = True
        Me.LinkToWebsite.Text = "\LINKTOWEBSITE_TEXT"
        '
        'VersionInfo
        '
        Me.VersionInfo.AutoSize = True
        Me.VersionInfo.Location = New System.Drawing.Point(3, 40)
        Me.VersionInfo.MinimumSize = New System.Drawing.Size(0, 20)
        Me.VersionInfo.Name = "VersionInfo"
        Me.VersionInfo.Size = New System.Drawing.Size(98, 20)
        Me.VersionInfo.TabIndex = 2
        Me.VersionInfo.TabStop = True
        Me.VersionInfo.Text = "\VERSION_TEXT"
        Me.VersionInfo.UseCompatibleTextRendering = True
        '
        'ContactLink
        '
        Me.ContactLink.AutoSize = True
        Me.ContactLink.Location = New System.Drawing.Point(3, 60)
        Me.ContactLink.MinimumSize = New System.Drawing.Size(0, 20)
        Me.ContactLink.Name = "ContactLink"
        Me.ContactLink.Size = New System.Drawing.Size(99, 20)
        Me.ContactLink.TabIndex = 3
        Me.ContactLink.TabStop = True
        Me.ContactLink.Text = "\CONTACT_LINK"
        Me.ContactLink.UseCompatibleTextRendering = True
        '
        'LinkToLicense
        '
        Me.LinkToLicense.AutoSize = True
        Me.LinkToLicense.Location = New System.Drawing.Point(3, 80)
        Me.LinkToLicense.MinimumSize = New System.Drawing.Size(0, 20)
        Me.LinkToLicense.Name = "LinkToLicense"
        Me.LinkToLicense.Size = New System.Drawing.Size(95, 20)
        Me.LinkToLicense.TabIndex = 4
        Me.LinkToLicense.TabStop = True
        Me.LinkToLicense.Text = "\LICENSE_TEXT"
        Me.LinkToLicense.UseCompatibleTextRendering = True
        '
        'BugReport
        '
        Me.BugReport.AutoSize = True
        Me.BugReport.Location = New System.Drawing.Point(3, 100)
        Me.BugReport.MinimumSize = New System.Drawing.Size(0, 20)
        Me.BugReport.Name = "BugReport"
        Me.BugReport.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.BugReport.Size = New System.Drawing.Size(87, 20)
        Me.BugReport.TabIndex = 5
        Me.BugReport.TabStop = True
        Me.BugReport.Text = "\BUG_REPORT"
        Me.BugReport.UseCompatibleTextRendering = True
        '
        'UpdatesOption
        '
        Me.UpdatesOption.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UpdatesOption.Location = New System.Drawing.Point(12, 147)
        Me.UpdatesOption.Name = "UpdatesOption"
        Me.UpdatesOption.Size = New System.Drawing.Size(502, 34)
        Me.UpdatesOption.TabIndex = 1
        Me.UpdatesOption.Text = "\UPDATES"
        Me.UpdatesOption.UseVisualStyleBackColor = False
        '
        'LanguageLabel
        '
        Me.LanguageLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LanguageLabel.AutoSize = True
        Me.LanguageLabel.Location = New System.Drawing.Point(12, 190)
        Me.LanguageLabel.Name = "LanguageLabel"
        Me.LanguageLabel.Size = New System.Drawing.Size(75, 13)
        Me.LanguageLabel.TabIndex = 2
        Me.LanguageLabel.Text = "\LANGUAGE"
        Me.LanguageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LanguagesList
        '
        Me.LanguagesList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LanguagesList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.LanguagesList.Location = New System.Drawing.Point(288, 187)
        Me.LanguagesList.Name = "LanguagesList"
        Me.LanguagesList.Size = New System.Drawing.Size(226, 21)
        Me.LanguagesList.TabIndex = 3
        '
        'AboutForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(526, 220)
        Me.Controls.Add(Me.LanguageLabel)
        Me.Controls.Add(Me.LanguagesList)
        Me.Controls.Add(Me.UpdatesOption)
        Me.Controls.Add(Me.FlowLayoutPanel)
        Me.Controls.Add(Me.Logo)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "AboutForm"
        Me.ShowInTaskbar = False
        Me.Text = "\ABOUT"
        CType(Me.Logo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FlowLayoutPanel.ResumeLayout(False)
        Me.FlowLayoutPanel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Logo As System.Windows.Forms.PictureBox
    Friend WithEvents FlowLayoutPanel As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents LinkToProductPage As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkToWebsite As System.Windows.Forms.LinkLabel
    Friend WithEvents VersionInfo As System.Windows.Forms.LinkLabel
    Friend WithEvents ContactLink As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkToLicense As System.Windows.Forms.LinkLabel
    Friend WithEvents BugReport As System.Windows.Forms.LinkLabel
    Friend WithEvents UpdatesOption As System.Windows.Forms.CheckBox
    Friend WithEvents LanguageLabel As System.Windows.Forms.Label
    Friend WithEvents LanguagesList As System.Windows.Forms.ComboBox
End Class
