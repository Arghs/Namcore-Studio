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
        Me.progressbar = New System.Windows.Forms.ProgressBar()
        Me.cancel_bt = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'process_tb
        '
        Me.process_tb.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.process_tb.Location = New System.Drawing.Point(12, 12)
        Me.process_tb.Name = "process_tb"
        Me.process_tb.ReadOnly = True
        Me.process_tb.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        Me.process_tb.Size = New System.Drawing.Size(598, 218)
        Me.process_tb.TabIndex = 0
        Me.process_tb.Text = ""
        '
        'close_bt
        '
        Me.close_bt.BackColor = System.Drawing.Color.DimGray
        Me.close_bt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.close_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.close_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.close_bt.Location = New System.Drawing.Point(225, 279)
        Me.close_bt.Name = "close_bt"
        Me.close_bt.Size = New System.Drawing.Size(147, 42)
        Me.close_bt.TabIndex = 209
        Me.close_bt.Text = "Close"
        Me.close_bt.UseVisualStyleBackColor = False
        '
        'progressbar
        '
        Me.progressbar.Location = New System.Drawing.Point(12, 241)
        Me.progressbar.Name = "progressbar"
        Me.progressbar.Size = New System.Drawing.Size(598, 27)
        Me.progressbar.TabIndex = 210
        '
        'cancel_bt
        '
        Me.cancel_bt.BackColor = System.Drawing.Color.DimGray
        Me.cancel_bt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cancel_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cancel_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.cancel_bt.Location = New System.Drawing.Point(392, 279)
        Me.cancel_bt.Name = "cancel_bt"
        Me.cancel_bt.Size = New System.Drawing.Size(147, 42)
        Me.cancel_bt.TabIndex = 211
        Me.cancel_bt.Text = "Cancel"
        Me.cancel_bt.UseVisualStyleBackColor = False
        '
        'Process_status
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(622, 333)
        Me.Controls.Add(Me.cancel_bt)
        Me.Controls.Add(Me.progressbar)
        Me.Controls.Add(Me.close_bt)
        Me.Controls.Add(Me.process_tb)
        Me.Name = "Process_status"
        Me.Text = "Process_status"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents close_bt As System.Windows.Forms.Button
    Friend WithEvents progressbar As System.Windows.Forms.ProgressBar
    Friend WithEvents process_tb As System.Windows.Forms.RichTextBox
    Friend WithEvents cancel_bt As System.Windows.Forms.Button
End Class
