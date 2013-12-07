Namespace Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class PrepChartrans
        Inherits System.Windows.Forms.Form

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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PrepChartrans))
            Me.accnames_txtbox = New System.Windows.Forms.TextBox()
            Me.ApplyTrans = New System.Windows.Forms.Button()
            Me.specific_radio = New System.Windows.Forms.RadioButton()
            Me.all_radio = New System.Windows.Forms.RadioButton()
            Me.GroupBox2 = New System.Windows.Forms.GroupBox()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.header = New System.Windows.Forms.Panel()
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.highlighter3 = New System.Windows.Forms.PictureBox()
            Me.highlighter4 = New System.Windows.Forms.PictureBox()
            Me.closepanel = New System.Windows.Forms.Panel()
            Me.highlighter1 = New System.Windows.Forms.PictureBox()
            Me.highlighter2 = New System.Windows.Forms.PictureBox()
            Me.GroupBox2.SuspendLayout()
            Me.header.SuspendLayout()
            Me.Panel1.SuspendLayout()
            CType(Me.highlighter3, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.highlighter4, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.closepanel.SuspendLayout()
            CType(Me.highlighter1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.highlighter2, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'accnames_txtbox
            '
            Me.accnames_txtbox.Location = New System.Drawing.Point(95, 39)
            Me.accnames_txtbox.Multiline = True
            Me.accnames_txtbox.Name = "accnames_txtbox"
            Me.accnames_txtbox.Size = New System.Drawing.Size(161, 154)
            Me.accnames_txtbox.TabIndex = 0
            '
            'ApplyTrans
            '
            Me.ApplyTrans.BackColor = System.Drawing.Color.DimGray
            Me.ApplyTrans.Cursor = System.Windows.Forms.Cursors.Hand
            Me.ApplyTrans.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.ApplyTrans.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.ApplyTrans.Location = New System.Drawing.Point(74, 271)
            Me.ApplyTrans.Name = "ApplyTrans"
            Me.ApplyTrans.Size = New System.Drawing.Size(121, 33)
            Me.ApplyTrans.TabIndex = 213
            Me.ApplyTrans.Text = "Apply"
            Me.ApplyTrans.UseVisualStyleBackColor = False
            '
            'specific_radio
            '
            Me.specific_radio.AutoSize = True
            Me.specific_radio.Location = New System.Drawing.Point(5, 16)
            Me.specific_radio.Name = "specific_radio"
            Me.specific_radio.Size = New System.Drawing.Size(253, 17)
            Me.specific_radio.TabIndex = 215
            Me.specific_radio.TabStop = True
            Me.specific_radio.Text = "Create selected characters on specific accounts"
            Me.specific_radio.UseVisualStyleBackColor = True
            '
            'all_radio
            '
            Me.all_radio.AutoSize = True
            Me.all_radio.BackColor = System.Drawing.Color.Transparent
            Me.all_radio.Checked = True
            Me.all_radio.Location = New System.Drawing.Point(12, 37)
            Me.all_radio.Name = "all_radio"
            Me.all_radio.Size = New System.Drawing.Size(227, 17)
            Me.all_radio.TabIndex = 216
            Me.all_radio.TabStop = True
            Me.all_radio.Text = "Create selected characters on all accounts"
            Me.all_radio.UseVisualStyleBackColor = False
            '
            'GroupBox2
            '
            Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
            Me.GroupBox2.Controls.Add(Me.Label1)
            Me.GroupBox2.Controls.Add(Me.accnames_txtbox)
            Me.GroupBox2.Controls.Add(Me.specific_radio)
            Me.GroupBox2.Location = New System.Drawing.Point(7, 56)
            Me.GroupBox2.Name = "GroupBox2"
            Me.GroupBox2.Size = New System.Drawing.Size(264, 208)
            Me.GroupBox2.TabIndex = 217
            Me.GroupBox2.TabStop = False
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(6, 42)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(79, 52)
            Me.Label1.TabIndex = 218
            Me.Label1.Text = "Accountname1" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Accountname2" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Accountname3" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "..."
            '
            'header
            '
            Me.header.BackgroundImage = Global.Namcore_Studio.My.Resources.Resources.namcore_header_new
            Me.header.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
            Me.header.Controls.Add(Me.Panel1)
            Me.header.Controls.Add(Me.closepanel)
            Me.header.Location = New System.Drawing.Point(1, 1)
            Me.header.Name = "header"
            Me.header.Size = New System.Drawing.Size(272, 30)
            Me.header.TabIndex = 223
            '
            'Panel1
            '
            Me.Panel1.BackColor = System.Drawing.Color.Transparent
            Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
            Me.Panel1.Controls.Add(Me.highlighter3)
            Me.Panel1.Controls.Add(Me.highlighter4)
            Me.Panel1.Location = New System.Drawing.Point(208, 0)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(62, 28)
            Me.Panel1.TabIndex = 1
            '
            'highlighter3
            '
            Me.highlighter3.BackColor = System.Drawing.Color.Transparent
            Me.highlighter3.BackgroundImage = Global.Namcore_Studio.My.Resources.Resources.bt_minimize
            Me.highlighter3.Cursor = System.Windows.Forms.Cursors.Hand
            Me.highlighter3.Location = New System.Drawing.Point(5, 5)
            Me.highlighter3.Name = "highlighter3"
            Me.highlighter3.Size = New System.Drawing.Size(25, 20)
            Me.highlighter3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.highlighter3.TabIndex = 217
            Me.highlighter3.TabStop = False
            '
            'highlighter4
            '
            Me.highlighter4.BackColor = System.Drawing.Color.Transparent
            Me.highlighter4.BackgroundImage = Global.Namcore_Studio.My.Resources.Resources.bt_close
            Me.highlighter4.Cursor = System.Windows.Forms.Cursors.Hand
            Me.highlighter4.Location = New System.Drawing.Point(33, 5)
            Me.highlighter4.Name = "highlighter4"
            Me.highlighter4.Size = New System.Drawing.Size(25, 20)
            Me.highlighter4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.highlighter4.TabIndex = 218
            Me.highlighter4.TabStop = False
            '
            'closepanel
            '
            Me.closepanel.BackColor = System.Drawing.Color.Transparent
            Me.closepanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
            Me.closepanel.Controls.Add(Me.highlighter1)
            Me.closepanel.Controls.Add(Me.highlighter2)
            Me.closepanel.Location = New System.Drawing.Point(552, 0)
            Me.closepanel.Name = "closepanel"
            Me.closepanel.Size = New System.Drawing.Size(62, 28)
            Me.closepanel.TabIndex = 0
            '
            'highlighter1
            '
            Me.highlighter1.BackColor = System.Drawing.Color.Transparent
            Me.highlighter1.Cursor = System.Windows.Forms.Cursors.Hand
            Me.highlighter1.Location = New System.Drawing.Point(5, 5)
            Me.highlighter1.Name = "highlighter1"
            Me.highlighter1.Size = New System.Drawing.Size(25, 20)
            Me.highlighter1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.highlighter1.TabIndex = 217
            Me.highlighter1.TabStop = False
            '
            'highlighter2
            '
            Me.highlighter2.BackColor = System.Drawing.Color.Transparent
            Me.highlighter2.Cursor = System.Windows.Forms.Cursors.Hand
            Me.highlighter2.Location = New System.Drawing.Point(33, 5)
            Me.highlighter2.Name = "highlighter2"
            Me.highlighter2.Size = New System.Drawing.Size(25, 20)
            Me.highlighter2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.highlighter2.TabIndex = 218
            Me.highlighter2.TabStop = False
            '
            'PrepChartrans
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackgroundImage = Global.Namcore_Studio.My.Resources.Resources.HUD_bg
            Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.ClientSize = New System.Drawing.Size(274, 311)
            Me.Controls.Add(Me.header)
            Me.Controls.Add(Me.GroupBox2)
            Me.Controls.Add(Me.all_radio)
            Me.Controls.Add(Me.ApplyTrans)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "PrepChartrans"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Prepare character transfer"
            Me.GroupBox2.ResumeLayout(False)
            Me.GroupBox2.PerformLayout()
            Me.header.ResumeLayout(False)
            Me.Panel1.ResumeLayout(False)
            CType(Me.highlighter3, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.highlighter4, System.ComponentModel.ISupportInitialize).EndInit()
            Me.closepanel.ResumeLayout(False)
            CType(Me.highlighter1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.highlighter2, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents accnames_txtbox As System.Windows.Forms.TextBox
        Friend WithEvents ApplyTrans As System.Windows.Forms.Button
        Friend WithEvents specific_radio As System.Windows.Forms.RadioButton
        Friend WithEvents all_radio As System.Windows.Forms.RadioButton
        Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents header As System.Windows.Forms.Panel
        Friend WithEvents closepanel As System.Windows.Forms.Panel
        Friend WithEvents highlighter1 As System.Windows.Forms.PictureBox
        Friend WithEvents highlighter2 As System.Windows.Forms.PictureBox
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents highlighter3 As System.Windows.Forms.PictureBox
        Friend WithEvents highlighter4 As System.Windows.Forms.PictureBox
    End Class
End Namespace