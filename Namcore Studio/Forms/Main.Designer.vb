<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
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
        Me.highlighter1 = New System.Windows.Forms.PictureBox()
        Me.highlighter2 = New System.Windows.Forms.PictureBox()
        Me.highlighter3 = New System.Windows.Forms.PictureBox()
        Me.highlighter4 = New System.Windows.Forms.PictureBox()
        Me.highlighter5 = New System.Windows.Forms.PictureBox()
        Me.version_lbl = New System.Windows.Forms.Label()
        CType(Me.highlighter1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.highlighter2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.highlighter3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.highlighter4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.highlighter5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'highlighter1
        '
        Me.highlighter1.BackColor = System.Drawing.Color.Transparent
        Me.highlighter1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.highlighter1.Location = New System.Drawing.Point(58, 71)
        Me.highlighter1.Name = "highlighter1"
        Me.highlighter1.Size = New System.Drawing.Size(941, 76)
        Me.highlighter1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.highlighter1.TabIndex = 0
        Me.highlighter1.TabStop = False
        '
        'highlighter2
        '
        Me.highlighter2.BackColor = System.Drawing.Color.Transparent
        Me.highlighter2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.highlighter2.Location = New System.Drawing.Point(58, 170)
        Me.highlighter2.Name = "highlighter2"
        Me.highlighter2.Size = New System.Drawing.Size(941, 76)
        Me.highlighter2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.highlighter2.TabIndex = 1
        Me.highlighter2.TabStop = False
        '
        'highlighter3
        '
        Me.highlighter3.BackColor = System.Drawing.Color.Transparent
        Me.highlighter3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.highlighter3.Location = New System.Drawing.Point(58, 269)
        Me.highlighter3.Name = "highlighter3"
        Me.highlighter3.Size = New System.Drawing.Size(941, 76)
        Me.highlighter3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.highlighter3.TabIndex = 2
        Me.highlighter3.TabStop = False
        '
        'highlighter4
        '
        Me.highlighter4.BackColor = System.Drawing.Color.Transparent
        Me.highlighter4.Cursor = System.Windows.Forms.Cursors.Hand
        Me.highlighter4.Location = New System.Drawing.Point(1023, 9)
        Me.highlighter4.Name = "highlighter4"
        Me.highlighter4.Size = New System.Drawing.Size(22, 20)
        Me.highlighter4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.highlighter4.TabIndex = 3
        Me.highlighter4.TabStop = False
        '
        'highlighter5
        '
        Me.highlighter5.BackColor = System.Drawing.Color.Transparent
        Me.highlighter5.Cursor = System.Windows.Forms.Cursors.Hand
        Me.highlighter5.Location = New System.Drawing.Point(992, 9)
        Me.highlighter5.Name = "highlighter5"
        Me.highlighter5.Size = New System.Drawing.Size(22, 20)
        Me.highlighter5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.highlighter5.TabIndex = 4
        Me.highlighter5.TabStop = False
        '
        'version_lbl
        '
        Me.version_lbl.AutoSize = True
        Me.version_lbl.BackColor = System.Drawing.Color.Transparent
        Me.version_lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.version_lbl.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.version_lbl.Location = New System.Drawing.Point(556, 372)
        Me.version_lbl.Name = "version_lbl"
        Me.version_lbl.Size = New System.Drawing.Size(492, 18)
        Me.version_lbl.TabIndex = 5
        Me.version_lbl.Text = "NamCore Studio - Development - 0.0.8.45283 - © megasus 2013"
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.Namcore_Studio.My.Resources.Resources.bgNav5
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1057, 397)
        Me.Controls.Add(Me.version_lbl)
        Me.Controls.Add(Me.highlighter5)
        Me.Controls.Add(Me.highlighter4)
        Me.Controls.Add(Me.highlighter3)
        Me.Controls.Add(Me.highlighter2)
        Me.Controls.Add(Me.highlighter1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.Name = "Main"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Main"
        CType(Me.highlighter1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.highlighter2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.highlighter3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.highlighter4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.highlighter5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents highlighter1 As System.Windows.Forms.PictureBox
    Friend WithEvents highlighter2 As System.Windows.Forms.PictureBox
    Friend WithEvents highlighter3 As System.Windows.Forms.PictureBox
    Friend WithEvents highlighter4 As System.Windows.Forms.PictureBox
    Friend WithEvents highlighter5 As System.Windows.Forms.PictureBox
    Friend WithEvents version_lbl As System.Windows.Forms.Label
End Class
