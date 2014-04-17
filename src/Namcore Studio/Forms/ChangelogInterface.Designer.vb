Imports NamCore_Studio.Forms.Extension

Namespace Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class ChangelogInterface
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
            Me.continue_bt = New System.Windows.Forms.Button()
            Me.version_lbl = New System.Windows.Forms.Label()
            Me.changelog_txtbox = New System.Windows.Forms.RichTextBox()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.GroupBox1.SuspendLayout()
            Me.SuspendLayout()
            '
            'continue_bt
            '
            Me.continue_bt.BackColor = System.Drawing.Color.DimGray
            Me.continue_bt.Cursor = System.Windows.Forms.Cursors.Hand
            Me.continue_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.continue_bt.ForeColor = System.Drawing.Color.Black
            Me.continue_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.continue_bt.Location = New System.Drawing.Point(529, 301)
            Me.continue_bt.Name = "continue_bt"
            Me.continue_bt.Size = New System.Drawing.Size(140, 32)
            Me.continue_bt.TabIndex = 226
            Me.continue_bt.Text = "Continue"
            Me.continue_bt.UseVisualStyleBackColor = False
            '
            'version_lbl
            '
            Me.version_lbl.AutoSize = True
            Me.version_lbl.BackColor = System.Drawing.Color.Transparent
            Me.version_lbl.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.version_lbl.Location = New System.Drawing.Point(4, 9)
            Me.version_lbl.Name = "version_lbl"
            Me.version_lbl.Size = New System.Drawing.Size(296, 21)
            Me.version_lbl.TabIndex = 227
            Me.version_lbl.Text = "NamCore Studio version 0.1.1 (15699)"
            '
            'changelog_txtbox
            '
            Me.changelog_txtbox.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
            Me.changelog_txtbox.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.changelog_txtbox.Location = New System.Drawing.Point(6, 35)
            Me.changelog_txtbox.Name = "changelog_txtbox"
            Me.changelog_txtbox.ReadOnly = True
            Me.changelog_txtbox.Size = New System.Drawing.Size(650, 170)
            Me.changelog_txtbox.TabIndex = 228
            Me.changelog_txtbox.Text = "General:" & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(10) & " > Fixed critical issues"
            '
            'GroupBox1
            '
            Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
            Me.GroupBox1.Controls.Add(Me.changelog_txtbox)
            Me.GroupBox1.Controls.Add(Me.version_lbl)
            Me.GroupBox1.Location = New System.Drawing.Point(13, 84)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(662, 211)
            Me.GroupBox1.TabIndex = 229
            Me.GroupBox1.TabStop = False
            '
            'ChangelogInterface
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(684, 342)
            Me.Controls.Add(Me.GroupBox1)
            Me.Controls.Add(Me.continue_bt)
            Me.Name = "ChangelogInterface"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TemplateExplorer"
            Me.Controls.SetChildIndex(Me.continue_bt, 0)
            Me.Controls.SetChildIndex(Me.GroupBox1, 0)
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents continue_bt As System.Windows.Forms.Button
        Friend WithEvents version_lbl As System.Windows.Forms.Label
        Friend WithEvents changelog_txtbox As System.Windows.Forms.RichTextBox
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    End Class
End Namespace