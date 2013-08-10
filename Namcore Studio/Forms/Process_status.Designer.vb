<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Process_status
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
        Me.process_tb = New System.Windows.Forms.RichTextBox()
        Me.close_bt = New System.Windows.Forms.Button()
        Me.cancel_bt = New System.Windows.Forms.Button()
        Me.highlighter2 = New System.Windows.Forms.PictureBox()
        Me.highlighter1 = New System.Windows.Forms.PictureBox()
        CType(Me.highlighter2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.highlighter1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'process_tb
        '
        Me.process_tb.BackColor = System.Drawing.Color.FromArgb(CType(CType(126, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(156, Byte), Integer))
        Me.process_tb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.process_tb.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.process_tb.Location = New System.Drawing.Point(13, 36)
        Me.process_tb.Name = "process_tb"
        Me.process_tb.ReadOnly = True
        Me.process_tb.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        Me.process_tb.Size = New System.Drawing.Size(598, 231)
        Me.process_tb.TabIndex = 0
        Me.process_tb.Text = ""
        '
        'close_bt
        '
        Me.close_bt.BackColor = System.Drawing.Color.DimGray
        Me.close_bt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.close_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.close_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.close_bt.Location = New System.Drawing.Point(158, 285)
        Me.close_bt.Name = "close_bt"
        Me.close_bt.Size = New System.Drawing.Size(147, 42)
        Me.close_bt.TabIndex = 209
        Me.close_bt.Text = "Close"
        Me.close_bt.UseVisualStyleBackColor = False
        '
        'cancel_bt
        '
        Me.cancel_bt.BackColor = System.Drawing.Color.DimGray
        Me.cancel_bt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cancel_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cancel_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.cancel_bt.Location = New System.Drawing.Point(325, 285)
        Me.cancel_bt.Name = "cancel_bt"
        Me.cancel_bt.Size = New System.Drawing.Size(147, 42)
        Me.cancel_bt.TabIndex = 211
        Me.cancel_bt.Text = "Cancel"
        Me.cancel_bt.UseVisualStyleBackColor = False
        '
        'highlighter2
        '
        Me.highlighter2.BackColor = System.Drawing.Color.Transparent
        Me.highlighter2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.highlighter2.Location = New System.Drawing.Point(593, 8)
        Me.highlighter2.Name = "highlighter2"
        Me.highlighter2.Size = New System.Drawing.Size(21, 18)
        Me.highlighter2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.highlighter2.TabIndex = 213
        Me.highlighter2.TabStop = False
        '
        'highlighter1
        '
        Me.highlighter1.BackColor = System.Drawing.Color.Transparent
        Me.highlighter1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.highlighter1.Location = New System.Drawing.Point(564, 8)
        Me.highlighter1.Name = "highlighter1"
        Me.highlighter1.Size = New System.Drawing.Size(21, 18)
        Me.highlighter1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.highlighter1.TabIndex = 212
        Me.highlighter1.TabStop = False
        '
        'Process_status
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.Namcore_Studio.My.Resources.Resources.norm_bg_slim
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(626, 343)
        Me.Controls.Add(Me.highlighter2)
        Me.Controls.Add(Me.highlighter1)
        Me.Controls.Add(Me.cancel_bt)
        Me.Controls.Add(Me.close_bt)
        Me.Controls.Add(Me.process_tb)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Process_status"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Process_status"
        Me.TopMost = True
        CType(Me.highlighter2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.highlighter1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents close_bt As System.Windows.Forms.Button
    Friend WithEvents process_tb As System.Windows.Forms.RichTextBox
    Friend WithEvents cancel_bt As System.Windows.Forms.Button
    Friend WithEvents highlighter2 As System.Windows.Forms.PictureBox
    Friend WithEvents highlighter1 As System.Windows.Forms.PictureBox
End Class
