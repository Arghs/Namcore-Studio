<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Updater
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
        Me.header = New System.Windows.Forms.Panel()
        Me.closepanel = New System.Windows.Forms.Panel()
        Me.highlighter1 = New System.Windows.Forms.PictureBox()
        Me.highlighter2 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.start_bt = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.globalprogress_bar = New System.Windows.Forms.ProgressBar()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.filestatus = New System.Windows.Forms.Label()
        Me.globalprogress_lbl = New System.Windows.Forms.Label()
        Me.subprogress_lbl = New System.Windows.Forms.Label()
        Me.subprogress_bar = New System.Windows.Forms.ProgressBar()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.speed = New System.Windows.Forms.Label()
        Me.currentfile = New System.Windows.Forms.Label()
        Me.header.SuspendLayout()
        Me.closepanel.SuspendLayout()
        CType(Me.highlighter1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.highlighter2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'header
        '
        Me.header.BackgroundImage = Global.NCLauncher.My.Resources.Resources.namcore_header
        Me.header.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.header.Controls.Add(Me.closepanel)
        Me.header.Location = New System.Drawing.Point(4, 3)
        Me.header.Name = "header"
        Me.header.Size = New System.Drawing.Size(579, 30)
        Me.header.TabIndex = 226
        '
        'closepanel
        '
        Me.closepanel.BackgroundImage = Global.NCLauncher.My.Resources.Resources.minclose
        Me.closepanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.closepanel.Controls.Add(Me.highlighter1)
        Me.closepanel.Controls.Add(Me.highlighter2)
        Me.closepanel.Location = New System.Drawing.Point(521, 0)
        Me.closepanel.Name = "closepanel"
        Me.closepanel.Size = New System.Drawing.Size(56, 28)
        Me.closepanel.TabIndex = 1
        '
        'highlighter1
        '
        Me.highlighter1.BackColor = System.Drawing.Color.Transparent
        Me.highlighter1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.highlighter1.Location = New System.Drawing.Point(5, 5)
        Me.highlighter1.Name = "highlighter1"
        Me.highlighter1.Size = New System.Drawing.Size(20, 20)
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
        Me.highlighter2.Size = New System.Drawing.Size(20, 20)
        Me.highlighter2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.highlighter2.TabIndex = 218
        Me.highlighter2.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(12, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(142, 17)
        Me.Label1.TabIndex = 227
        Me.Label1.Text = "Updates available!"
        '
        'start_bt
        '
        Me.start_bt.BackColor = System.Drawing.Color.DimGray
        Me.start_bt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.start_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.start_bt.ForeColor = System.Drawing.Color.Black
        Me.start_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.start_bt.Location = New System.Drawing.Point(15, 81)
        Me.start_bt.Name = "start_bt"
        Me.start_bt.Size = New System.Drawing.Size(139, 30)
        Me.start_bt.TabIndex = 228
        Me.start_bt.Text = "Start download"
        Me.start_bt.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.DimGray
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.ForeColor = System.Drawing.Color.Black
        Me.Button1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Button1.Location = New System.Drawing.Point(160, 81)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(139, 30)
        Me.Button1.TabIndex = 229
        Me.Button1.Text = "Skip"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'globalprogress_bar
        '
        Me.globalprogress_bar.Location = New System.Drawing.Point(15, 159)
        Me.globalprogress_bar.Name = "globalprogress_bar"
        Me.globalprogress_bar.Size = New System.Drawing.Size(559, 23)
        Me.globalprogress_bar.TabIndex = 230
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(12, 247)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 15)
        Me.Label2.TabIndex = 231
        Me.Label2.Text = "Speed:"
        '
        'filestatus
        '
        Me.filestatus.AutoSize = True
        Me.filestatus.BackColor = System.Drawing.Color.Transparent
        Me.filestatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.filestatus.ForeColor = System.Drawing.Color.White
        Me.filestatus.Location = New System.Drawing.Point(12, 132)
        Me.filestatus.Name = "filestatus"
        Me.filestatus.Size = New System.Drawing.Size(115, 15)
        Me.filestatus.TabIndex = 232
        Me.filestatus.Text = "Loading file 1/10"
        '
        'globalprogress_lbl
        '
        Me.globalprogress_lbl.BackColor = System.Drawing.SystemColors.WindowText
        Me.globalprogress_lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.globalprogress_lbl.ForeColor = System.Drawing.Color.White
        Me.globalprogress_lbl.Location = New System.Drawing.Point(197, 163)
        Me.globalprogress_lbl.Name = "globalprogress_lbl"
        Me.globalprogress_lbl.Size = New System.Drawing.Size(201, 15)
        Me.globalprogress_lbl.TabIndex = 233
        Me.globalprogress_lbl.Text = "5.12MB / 11.8MB"
        Me.globalprogress_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'subprogress_lbl
        '
        Me.subprogress_lbl.BackColor = System.Drawing.SystemColors.WindowText
        Me.subprogress_lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.subprogress_lbl.ForeColor = System.Drawing.Color.White
        Me.subprogress_lbl.Location = New System.Drawing.Point(197, 219)
        Me.subprogress_lbl.Name = "subprogress_lbl"
        Me.subprogress_lbl.Size = New System.Drawing.Size(201, 15)
        Me.subprogress_lbl.TabIndex = 234
        Me.subprogress_lbl.Text = "1.1 MB / 2 MB"
        Me.subprogress_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'subprogress_bar
        '
        Me.subprogress_bar.Location = New System.Drawing.Point(15, 215)
        Me.subprogress_bar.Name = "subprogress_bar"
        Me.subprogress_bar.Size = New System.Drawing.Size(559, 23)
        Me.subprogress_bar.TabIndex = 235
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(12, 190)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(82, 15)
        Me.Label6.TabIndex = 236
        Me.Label6.Text = "Current file:"
        '
        'speed
        '
        Me.speed.AutoSize = True
        Me.speed.BackColor = System.Drawing.Color.Transparent
        Me.speed.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.speed.ForeColor = System.Drawing.Color.White
        Me.speed.Location = New System.Drawing.Point(70, 247)
        Me.speed.Name = "speed"
        Me.speed.Size = New System.Drawing.Size(45, 15)
        Me.speed.TabIndex = 237
        Me.speed.Text = "0 kb/s"
        '
        'currentfile
        '
        Me.currentfile.AutoSize = True
        Me.currentfile.BackColor = System.Drawing.Color.Transparent
        Me.currentfile.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.currentfile.ForeColor = System.Drawing.Color.White
        Me.currentfile.Location = New System.Drawing.Point(100, 190)
        Me.currentfile.Name = "currentfile"
        Me.currentfile.Size = New System.Drawing.Size(139, 15)
        Me.currentfile.TabIndex = 238
        Me.currentfile.Text = "NamCore Studio.exe"
        '
        'Updater
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.NCLauncher.My.Resources.Resources.cleanbg
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(586, 275)
        Me.Controls.Add(Me.currentfile)
        Me.Controls.Add(Me.speed)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.subprogress_lbl)
        Me.Controls.Add(Me.subprogress_bar)
        Me.Controls.Add(Me.globalprogress_lbl)
        Me.Controls.Add(Me.filestatus)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.globalprogress_bar)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.start_bt)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.header)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Updater"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Updater"
        Me.header.ResumeLayout(False)
        Me.closepanel.ResumeLayout(False)
        CType(Me.highlighter1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.highlighter2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents header As System.Windows.Forms.Panel
    Friend WithEvents closepanel As System.Windows.Forms.Panel
    Friend WithEvents highlighter1 As System.Windows.Forms.PictureBox
    Friend WithEvents highlighter2 As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents start_bt As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents globalprogress_bar As System.Windows.Forms.ProgressBar
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents filestatus As System.Windows.Forms.Label
    Friend WithEvents globalprogress_lbl As System.Windows.Forms.Label
    Friend WithEvents subprogress_lbl As System.Windows.Forms.Label
    Friend WithEvents subprogress_bar As System.Windows.Forms.ProgressBar
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents speed As System.Windows.Forms.Label
    Friend WithEvents currentfile As System.Windows.Forms.Label

End Class
