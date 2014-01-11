Namespace Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class About
        Inherits Form

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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(About))
            Me.header = New System.Windows.Forms.Panel()
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.highlighter3 = New System.Windows.Forms.PictureBox()
            Me.highlighter4 = New System.Windows.Forms.PictureBox()
            Me.PictureBox1 = New System.Windows.Forms.PictureBox()
            Me.version_lbl = New System.Windows.Forms.Label()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.framework_lbl = New System.Windows.Forms.Label()
            Me.libncadvanced_lbl = New System.Windows.Forms.Label()
            Me.libnc_lbl = New System.Windows.Forms.Label()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.LinkLabel3 = New System.Windows.Forms.LinkLabel()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.LinkLabel2 = New System.Windows.Forms.LinkLabel()
            Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
            Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
            Me.header.SuspendLayout()
            Me.Panel1.SuspendLayout()
            CType(Me.highlighter3, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.highlighter4, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.GroupBox1.SuspendLayout()
            Me.SuspendLayout()
            '
            'header
            '
            Me.header.BackgroundImage = Global.NamCore_Studio.My.Resources.Resources.namcore_header_new
            Me.header.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
            Me.header.Controls.Add(Me.Panel1)
            Me.header.Location = New System.Drawing.Point(3, 1)
            Me.header.Name = "header"
            Me.header.Size = New System.Drawing.Size(596, 30)
            Me.header.TabIndex = 224
            '
            'Panel1
            '
            Me.Panel1.BackColor = System.Drawing.Color.Transparent
            Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
            Me.Panel1.Controls.Add(Me.highlighter3)
            Me.Panel1.Controls.Add(Me.highlighter4)
            Me.Panel1.Location = New System.Drawing.Point(535, 0)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(62, 28)
            Me.Panel1.TabIndex = 1
            '
            'highlighter3
            '
            Me.highlighter3.BackColor = System.Drawing.Color.Transparent
            Me.highlighter3.BackgroundImage = Global.NamCore_Studio.My.Resources.Resources.bt_minimize
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
            Me.highlighter4.BackgroundImage = Global.NamCore_Studio.My.Resources.Resources.bt_close
            Me.highlighter4.Cursor = System.Windows.Forms.Cursors.Hand
            Me.highlighter4.Location = New System.Drawing.Point(33, 5)
            Me.highlighter4.Name = "highlighter4"
            Me.highlighter4.Size = New System.Drawing.Size(25, 20)
            Me.highlighter4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.highlighter4.TabIndex = 218
            Me.highlighter4.TabStop = False
            '
            'PictureBox1
            '
            Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
            Me.PictureBox1.BackgroundImage = Global.NamCore_Studio.My.Resources.Resources.finalnclogo
            Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.PictureBox1.Location = New System.Drawing.Point(13, 25)
            Me.PictureBox1.Name = "PictureBox1"
            Me.PictureBox1.Size = New System.Drawing.Size(275, 81)
            Me.PictureBox1.TabIndex = 225
            Me.PictureBox1.TabStop = False
            '
            'version_lbl
            '
            Me.version_lbl.AutoSize = True
            Me.version_lbl.BackColor = System.Drawing.Color.Transparent
            Me.version_lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.version_lbl.ForeColor = System.Drawing.Color.Black
            Me.version_lbl.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.version_lbl.Location = New System.Drawing.Point(17, 117)
            Me.version_lbl.Name = "version_lbl"
            Me.version_lbl.Size = New System.Drawing.Size(201, 16)
            Me.version_lbl.TabIndex = 226
            Me.version_lbl.Text = "Version 0.0.11 (15699) Indev"
            '
            'GroupBox1
            '
            Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
            Me.GroupBox1.Controls.Add(Me.framework_lbl)
            Me.GroupBox1.Controls.Add(Me.libncadvanced_lbl)
            Me.GroupBox1.Controls.Add(Me.libnc_lbl)
            Me.GroupBox1.Controls.Add(Me.Label2)
            Me.GroupBox1.Controls.Add(Me.LinkLabel3)
            Me.GroupBox1.Controls.Add(Me.Label1)
            Me.GroupBox1.Controls.Add(Me.PictureBox1)
            Me.GroupBox1.Controls.Add(Me.version_lbl)
            Me.GroupBox1.Controls.Add(Me.LinkLabel2)
            Me.GroupBox1.Controls.Add(Me.LinkLabel1)
            Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold)
            Me.GroupBox1.Location = New System.Drawing.Point(12, 38)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(577, 203)
            Me.GroupBox1.TabIndex = 227
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "NamCore Studio"
            '
            'framework_lbl
            '
            Me.framework_lbl.BackColor = System.Drawing.Color.Transparent
            Me.framework_lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.framework_lbl.ForeColor = System.Drawing.Color.Black
            Me.framework_lbl.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.framework_lbl.Location = New System.Drawing.Point(308, 171)
            Me.framework_lbl.Name = "framework_lbl"
            Me.framework_lbl.RightToLeft = System.Windows.Forms.RightToLeft.Yes
            Me.framework_lbl.Size = New System.Drawing.Size(263, 16)
            Me.framework_lbl.TabIndex = 234
            Me.framework_lbl.Text = "NCFramework: 0.0.12.46103"
            '
            'libncadvanced_lbl
            '
            Me.libncadvanced_lbl.BackColor = System.Drawing.Color.Transparent
            Me.libncadvanced_lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.libncadvanced_lbl.ForeColor = System.Drawing.Color.Black
            Me.libncadvanced_lbl.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.libncadvanced_lbl.Location = New System.Drawing.Point(308, 144)
            Me.libncadvanced_lbl.Name = "libncadvanced_lbl"
            Me.libncadvanced_lbl.RightToLeft = System.Windows.Forms.RightToLeft.Yes
            Me.libncadvanced_lbl.Size = New System.Drawing.Size(263, 16)
            Me.libncadvanced_lbl.TabIndex = 233
            Me.libncadvanced_lbl.Text = "libncadvanced: 5.4.0.24973"
            '
            'libnc_lbl
            '
            Me.libnc_lbl.BackColor = System.Drawing.Color.Transparent
            Me.libnc_lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.libnc_lbl.ForeColor = System.Drawing.Color.Black
            Me.libnc_lbl.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.libnc_lbl.Location = New System.Drawing.Point(308, 117)
            Me.libnc_lbl.Name = "libnc_lbl"
            Me.libnc_lbl.RightToLeft = System.Windows.Forms.RightToLeft.Yes
            Me.libnc_lbl.Size = New System.Drawing.Size(263, 16)
            Me.libnc_lbl.TabIndex = 232
            Me.libnc_lbl.Text = "libnc: 5.4.0.24973"
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.BackColor = System.Drawing.Color.Transparent
            Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.ForeColor = System.Drawing.Color.Black
            Me.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.Label2.Location = New System.Drawing.Point(17, 171)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(123, 16)
            Me.Label2.TabIndex = 230
            Me.Label2.Text = "License: GPL 3.0"
            '
            'LinkLabel3
            '
            Me.LinkLabel3.AutoSize = True
            Me.LinkLabel3.BackColor = System.Drawing.Color.Transparent
            Me.LinkLabel3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.LinkLabel3.Location = New System.Drawing.Point(454, 85)
            Me.LinkLabel3.Name = "LinkLabel3"
            Me.LinkLabel3.Size = New System.Drawing.Size(84, 16)
            Me.LinkLabel3.TabIndex = 231
            Me.LinkLabel3.TabStop = True
            Me.LinkLabel3.Text = "Bugreports"
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.BackColor = System.Drawing.Color.Transparent
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.ForeColor = System.Drawing.Color.Black
            Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.Label1.Location = New System.Drawing.Point(17, 144)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(242, 16)
            Me.Label1.TabIndex = 227
            Me.Label1.Text = "© 2013-2014 megasus, alcanmage"
            '
            'LinkLabel2
            '
            Me.LinkLabel2.AutoSize = True
            Me.LinkLabel2.BackColor = System.Drawing.Color.Transparent
            Me.LinkLabel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.LinkLabel2.Location = New System.Drawing.Point(454, 57)
            Me.LinkLabel2.Name = "LinkLabel2"
            Me.LinkLabel2.Size = New System.Drawing.Size(116, 16)
            Me.LinkLabel2.TabIndex = 229
            Me.LinkLabel2.TabStop = True
            Me.LinkLabel2.Text = "Developer blog"
            '
            'LinkLabel1
            '
            Me.LinkLabel1.AutoSize = True
            Me.LinkLabel1.BackColor = System.Drawing.Color.Transparent
            Me.LinkLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.LinkLabel1.Location = New System.Drawing.Point(454, 29)
            Me.LinkLabel1.Name = "LinkLabel1"
            Me.LinkLabel1.Size = New System.Drawing.Size(84, 16)
            Me.LinkLabel1.TabIndex = 228
            Me.LinkLabel1.TabStop = True
            Me.LinkLabel1.Text = "Repository"
            '
            'RichTextBox1
            '
            Me.RichTextBox1.BackColor = System.Drawing.Color.LightGray
            Me.RichTextBox1.Location = New System.Drawing.Point(12, 247)
            Me.RichTextBox1.Name = "RichTextBox1"
            Me.RichTextBox1.ReadOnly = True
            Me.RichTextBox1.Size = New System.Drawing.Size(577, 218)
            Me.RichTextBox1.TabIndex = 230
            Me.RichTextBox1.Text = resources.GetString("RichTextBox1.Text")
            '
            'About
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackgroundImage = Global.NamCore_Studio.My.Resources.Resources.HUD_bg
            Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.ClientSize = New System.Drawing.Size(601, 482)
            Me.Controls.Add(Me.RichTextBox1)
            Me.Controls.Add(Me.GroupBox1)
            Me.Controls.Add(Me.header)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            Me.Name = "About"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "About"
            Me.header.ResumeLayout(False)
            Me.Panel1.ResumeLayout(False)
            CType(Me.highlighter3, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.highlighter4, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents header As System.Windows.Forms.Panel
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents highlighter3 As System.Windows.Forms.PictureBox
        Friend WithEvents highlighter4 As System.Windows.Forms.PictureBox
        Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
        Friend WithEvents version_lbl As System.Windows.Forms.Label
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents LinkLabel2 As System.Windows.Forms.LinkLabel
        Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents RichTextBox1 As System.Windows.Forms.RichTextBox
        Friend WithEvents LinkLabel3 As System.Windows.Forms.LinkLabel
        Friend WithEvents libnc_lbl As System.Windows.Forms.Label
        Friend WithEvents framework_lbl As System.Windows.Forms.Label
        Friend WithEvents libncadvanced_lbl As System.Windows.Forms.Label
    End Class
End Namespace