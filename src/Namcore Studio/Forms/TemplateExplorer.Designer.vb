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
            Me.SuspendLayout()
            '
            'openfile_bt
            '
            Me.openfile_bt.BackColor = System.Drawing.Color.DimGray
            Me.openfile_bt.Cursor = System.Windows.Forms.Cursors.Hand
            Me.openfile_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.openfile_bt.ForeColor = System.Drawing.Color.Black
            Me.openfile_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.openfile_bt.Location = New System.Drawing.Point(12, 98)
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
            Me.back_bt.Location = New System.Drawing.Point(540, 292)
            Me.back_bt.Name = "back_bt"
            Me.back_bt.Size = New System.Drawing.Size(135, 41)
            Me.back_bt.TabIndex = 226
            Me.back_bt.Text = "Back"
            Me.back_bt.UseVisualStyleBackColor = False
            '
            'TemplateExplorer
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(684, 342)
            Me.Controls.Add(Me.back_bt)
            Me.Controls.Add(Me.openfile_bt)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "TemplateExplorer"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TemplateExplorer"
            Me.Controls.SetChildIndex(Me.openfile_bt, 0)
            Me.Controls.SetChildIndex(Me.back_bt, 0)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents openfile_bt As System.Windows.Forms.Button
        Friend WithEvents back_bt As System.Windows.Forms.Button
    End Class
End Namespace