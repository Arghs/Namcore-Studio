Imports NamCore_Studio.Forms.Extension

Namespace Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class TemplateExplorer
        Inherits EventTrigger

        'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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

        'Wird vom Windows Form-Designer benötigt.
        Private components As System.ComponentModel.IContainer

        'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
        'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
        'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TemplateExplorer))
        Me.openfile_bt = New System.Windows.Forms.Button()
        Me.back_bt = New System.Windows.Forms.Button()
        Me.reference_template_panel = New System.Windows.Forms.Panel()
            Me.reference_rating_rater = New ShaperRater.Rater()
            Me.reference_downloadcounter_lbl = New System.Windows.Forms.Label()
            Me.reference_download_bt = New System.Windows.Forms.Button()
            Me.reference_description_panel = New System.Windows.Forms.Panel()
            Me.reference_description_lbl = New System.Windows.Forms.Label()
            Me.reference_author_lbl = New System.Windows.Forms.Label()
            Me.reference_date_lbl = New System.Windows.Forms.Label()
            Me.reference_templatename_lbl = New System.Windows.Forms.Label()
            Me.template_layout_panel = New System.Windows.Forms.FlowLayoutPanel()
            Me.login_bt = New System.Windows.Forms.Button()
            Me.login_lbl = New System.Windows.Forms.Label()
            Me.reference_template_panel.SuspendLayout()
            CType(Me.reference_rating_rater, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.reference_description_panel.SuspendLayout()
            Me.SuspendLayout()
            '
            'openfile_bt
            '
            Me.openfile_bt.BackColor = System.Drawing.Color.DimGray
            Me.openfile_bt.Cursor = System.Windows.Forms.Cursors.Hand
            Me.openfile_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.openfile_bt.ForeColor = System.Drawing.Color.Black
            Me.openfile_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.openfile_bt.Location = New System.Drawing.Point(12, 490)
            Me.openfile_bt.Name = "openfile_bt"
            Me.openfile_bt.Size = New System.Drawing.Size(135, 41)
            Me.openfile_bt.TabIndex = 167
            Me.openfile_bt.Text = "Load file"
            Me.openfile_bt.UseVisualStyleBackColor = False
            '
            'back_bt
            '
            Me.back_bt.BackColor = System.Drawing.Color.DimGray
            Me.back_bt.Cursor = System.Windows.Forms.Cursors.Hand
            Me.back_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.back_bt.ForeColor = System.Drawing.Color.Black
            Me.back_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.back_bt.Location = New System.Drawing.Point(627, 490)
            Me.back_bt.Name = "back_bt"
            Me.back_bt.Size = New System.Drawing.Size(135, 41)
            Me.back_bt.TabIndex = 226
            Me.back_bt.Text = "Back"
            Me.back_bt.UseVisualStyleBackColor = False
            '
            'reference_template_panel
            '
            Me.reference_template_panel.BackColor = System.Drawing.SystemColors.ControlDark
            Me.reference_template_panel.Controls.Add(Me.reference_rating_rater)
            Me.reference_template_panel.Controls.Add(Me.reference_downloadcounter_lbl)
            Me.reference_template_panel.Controls.Add(Me.reference_download_bt)
            Me.reference_template_panel.Controls.Add(Me.reference_description_panel)
            Me.reference_template_panel.Controls.Add(Me.reference_author_lbl)
            Me.reference_template_panel.Controls.Add(Me.reference_date_lbl)
            Me.reference_template_panel.Controls.Add(Me.reference_templatename_lbl)
            Me.reference_template_panel.Location = New System.Drawing.Point(810, 131)
            Me.reference_template_panel.Name = "reference_template_panel"
            Me.reference_template_panel.Size = New System.Drawing.Size(588, 158)
            Me.reference_template_panel.TabIndex = 227
            '
            'reference_rating_rater
            '
            Me.reference_rating_rater.BackColor = System.Drawing.SystemColors.ControlDark
            Me.reference_rating_rater.CurrentRating = 0
            Me.reference_rating_rater.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.reference_rating_rater.LabelAlignment = System.Drawing.ContentAlignment.MiddleCenter
            Me.reference_rating_rater.LabelShow = False
            Me.reference_rating_rater.LabelText = "RateLabel"
            Me.reference_rating_rater.LabelTextItems = New String() {"Poor", "Fair", "Good", "Better", "Best"}
            Me.reference_rating_rater.Location = New System.Drawing.Point(147, 132)
            Me.reference_rating_rater.Margin = New System.Windows.Forms.Padding(4)
            Me.reference_rating_rater.Name = "reference_rating_rater"
            Me.reference_rating_rater.RadiusInner = 0.0!
            Me.reference_rating_rater.RadiusOuter = 10.0!
            Me.reference_rating_rater.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.reference_rating_rater.Shape = ShaperRater.Rater.eShape.Circle
            Me.reference_rating_rater.ShapeColorFill = System.Drawing.Color.Cornsilk
            Me.reference_rating_rater.Size = New System.Drawing.Size(131, 23)
            Me.reference_rating_rater.TabIndex = 230
            '
            'reference_downloadcounter_lbl
            '
            Me.reference_downloadcounter_lbl.Font = New System.Drawing.Font("Verdana", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.reference_downloadcounter_lbl.Location = New System.Drawing.Point(435, 138)
            Me.reference_downloadcounter_lbl.Name = "reference_downloadcounter_lbl"
            Me.reference_downloadcounter_lbl.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.reference_downloadcounter_lbl.Size = New System.Drawing.Size(150, 16)
            Me.reference_downloadcounter_lbl.TabIndex = 229
            Me.reference_downloadcounter_lbl.Text = "10 Downloads"
            Me.reference_downloadcounter_lbl.TextAlign = System.Drawing.ContentAlignment.TopRight
            '
            'reference_download_bt
            '
            Me.reference_download_bt.BackColor = System.Drawing.Color.DimGray
            Me.reference_download_bt.Cursor = System.Windows.Forms.Cursors.Hand
            Me.reference_download_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.reference_download_bt.ForeColor = System.Drawing.Color.Black
            Me.reference_download_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.reference_download_bt.Location = New System.Drawing.Point(8, 132)
            Me.reference_download_bt.Name = "reference_download_bt"
            Me.reference_download_bt.Size = New System.Drawing.Size(132, 22)
            Me.reference_download_bt.TabIndex = 228
            Me.reference_download_bt.Text = "Download"
            Me.reference_download_bt.UseVisualStyleBackColor = False
            '
            'reference_description_panel
            '
            Me.reference_description_panel.AutoScroll = True
            Me.reference_description_panel.Controls.Add(Me.reference_description_lbl)
            Me.reference_description_panel.Location = New System.Drawing.Point(8, 43)
            Me.reference_description_panel.Name = "reference_description_panel"
            Me.reference_description_panel.Size = New System.Drawing.Size(577, 85)
            Me.reference_description_panel.TabIndex = 3
            '
            'reference_description_lbl
            '
            Me.reference_description_lbl.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.reference_description_lbl.Location = New System.Drawing.Point(2, 1)
            Me.reference_description_lbl.Name = "reference_description_lbl"
            Me.reference_description_lbl.Size = New System.Drawing.Size(553, 236)
            Me.reference_description_lbl.TabIndex = 3
            Me.reference_description_lbl.Text = resources.GetString("reference_description_lbl.Text")
            '
            'reference_author_lbl
            '
            Me.reference_author_lbl.AutoSize = True
            Me.reference_author_lbl.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.reference_author_lbl.Location = New System.Drawing.Point(7, 26)
            Me.reference_author_lbl.Name = "reference_author_lbl"
            Me.reference_author_lbl.Size = New System.Drawing.Size(82, 14)
            Me.reference_author_lbl.TabIndex = 2
            Me.reference_author_lbl.Text = "By Megasus"
            '
            'reference_date_lbl
            '
            Me.reference_date_lbl.AutoSize = True
            Me.reference_date_lbl.Font = New System.Drawing.Font("Verdana", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.reference_date_lbl.Location = New System.Drawing.Point(495, 6)
            Me.reference_date_lbl.Name = "reference_date_lbl"
            Me.reference_date_lbl.Size = New System.Drawing.Size(90, 16)
            Me.reference_date_lbl.TabIndex = 1
            Me.reference_date_lbl.Text = "08.05.2014"
            '
            'reference_templatename_lbl
            '
            Me.reference_templatename_lbl.AutoSize = True
            Me.reference_templatename_lbl.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.reference_templatename_lbl.Location = New System.Drawing.Point(5, 6)
            Me.reference_templatename_lbl.Name = "reference_templatename_lbl"
            Me.reference_templatename_lbl.Size = New System.Drawing.Size(131, 18)
            Me.reference_templatename_lbl.TabIndex = 0
            Me.reference_templatename_lbl.Text = "Templatename"
            '
            'template_layout_panel
            '
            Me.template_layout_panel.AutoScroll = True
            Me.template_layout_panel.BackColor = System.Drawing.Color.FromArgb(CType(CType(105, Byte), Integer), CType(CType(137, Byte), Integer), CType(CType(158, Byte), Integer))
            Me.template_layout_panel.Location = New System.Drawing.Point(153, 98)
            Me.template_layout_panel.Name = "template_layout_panel"
            Me.template_layout_panel.Size = New System.Drawing.Size(609, 381)
            Me.template_layout_panel.TabIndex = 228
            '
            'login_bt
            '
            Me.login_bt.BackColor = System.Drawing.Color.DimGray
            Me.login_bt.Cursor = System.Windows.Forms.Cursors.Hand
            Me.login_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.login_bt.ForeColor = System.Drawing.Color.Black
            Me.login_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.login_bt.Location = New System.Drawing.Point(7, 122)
            Me.login_bt.Name = "login_bt"
            Me.login_bt.Size = New System.Drawing.Size(135, 41)
            Me.login_bt.TabIndex = 229
            Me.login_bt.Text = "Login"
            Me.login_bt.UseVisualStyleBackColor = False
            '
            'login_lbl
            '
            Me.login_lbl.AutoSize = True
            Me.login_lbl.BackColor = System.Drawing.Color.Red
            Me.login_lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
            Me.login_lbl.Location = New System.Drawing.Point(9, 98)
            Me.login_lbl.Name = "login_lbl"
            Me.login_lbl.Size = New System.Drawing.Size(93, 15)
            Me.login_lbl.TabIndex = 0
            Me.login_lbl.Text = "Not logged in"
            '
            'TemplateExplorer
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(772, 543)
            Me.Controls.Add(Me.login_lbl)
            Me.Controls.Add(Me.login_bt)
            Me.Controls.Add(Me.template_layout_panel)
            Me.Controls.Add(Me.reference_template_panel)
            Me.Controls.Add(Me.back_bt)
            Me.Controls.Add(Me.openfile_bt)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "TemplateExplorer"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TemplateExplorer"
            Me.Controls.SetChildIndex(Me.openfile_bt, 0)
            Me.Controls.SetChildIndex(Me.back_bt, 0)
            Me.Controls.SetChildIndex(Me.reference_template_panel, 0)
            Me.Controls.SetChildIndex(Me.template_layout_panel, 0)
            Me.Controls.SetChildIndex(Me.login_bt, 0)
            Me.Controls.SetChildIndex(Me.login_lbl, 0)
            Me.reference_template_panel.ResumeLayout(False)
            Me.reference_template_panel.PerformLayout()
            CType(Me.reference_rating_rater, System.ComponentModel.ISupportInitialize).EndInit()
            Me.reference_description_panel.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents openfile_bt As System.Windows.Forms.Button
        Friend WithEvents back_bt As System.Windows.Forms.Button
        Friend WithEvents reference_template_panel As System.Windows.Forms.Panel
        Friend WithEvents reference_date_lbl As System.Windows.Forms.Label
        Friend WithEvents reference_templatename_lbl As System.Windows.Forms.Label
        Friend WithEvents reference_downloadcounter_lbl As System.Windows.Forms.Label
        Friend WithEvents reference_download_bt As System.Windows.Forms.Button
        Friend WithEvents reference_description_panel As System.Windows.Forms.Panel
        Friend WithEvents reference_description_lbl As System.Windows.Forms.Label
        Friend WithEvents reference_author_lbl As System.Windows.Forms.Label
        Friend WithEvents reference_rating_rater As ShaperRater.Rater
        Friend WithEvents template_layout_panel As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents login_bt As System.Windows.Forms.Button
        Friend WithEvents login_lbl As System.Windows.Forms.Label
    End Class
End Namespace