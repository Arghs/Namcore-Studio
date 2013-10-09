<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TemplateExplorer
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
        Me.openfile_bt = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'openfile_bt
        '
        Me.openfile_bt.BackColor = System.Drawing.Color.DimGray
        Me.openfile_bt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.openfile_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.openfile_bt.ForeColor = System.Drawing.Color.Black
        Me.openfile_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.openfile_bt.Location = New System.Drawing.Point(243, 247)
        Me.openfile_bt.Name = "openfile_bt"
        Me.openfile_bt.Size = New System.Drawing.Size(135, 41)
        Me.openfile_bt.TabIndex = 167
        Me.openfile_bt.Text = "Load file"
        Me.openfile_bt.UseVisualStyleBackColor = False
        '
        'TemplateExplorer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(390, 300)
        Me.Controls.Add(Me.openfile_bt)
        Me.Name = "TemplateExplorer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "TemplateExplorer"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents openfile_bt As System.Windows.Forms.Button
End Class
