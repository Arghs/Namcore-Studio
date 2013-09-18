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
            Me.GroupBox2.SuspendLayout()
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
            Me.ApplyTrans.Location = New System.Drawing.Point(74, 263)
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
            Me.all_radio.Checked = True
            Me.all_radio.Location = New System.Drawing.Point(12, 12)
            Me.all_radio.Name = "all_radio"
            Me.all_radio.Size = New System.Drawing.Size(227, 17)
            Me.all_radio.TabIndex = 216
            Me.all_radio.TabStop = True
            Me.all_radio.Text = "Create selected characters on all accounts"
            Me.all_radio.UseVisualStyleBackColor = True
            '
            'GroupBox2
            '
            Me.GroupBox2.Controls.Add(Me.Label1)
            Me.GroupBox2.Controls.Add(Me.accnames_txtbox)
            Me.GroupBox2.Controls.Add(Me.specific_radio)
            Me.GroupBox2.Location = New System.Drawing.Point(7, 31)
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
            'Prep_chartrans
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(274, 311)
            Me.Controls.Add(Me.GroupBox2)
            Me.Controls.Add(Me.all_radio)
            Me.Controls.Add(Me.ApplyTrans)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "PrepChartrans"
            Me.Text = "Prepare character transfer"
            Me.GroupBox2.ResumeLayout(False)
            Me.GroupBox2.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents accnames_txtbox As System.Windows.Forms.TextBox
        Friend WithEvents ApplyTrans As System.Windows.Forms.Button
        Friend WithEvents specific_radio As System.Windows.Forms.RadioButton
        Friend WithEvents all_radio As System.Windows.Forms.RadioButton
        Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
        Friend WithEvents Label1 As System.Windows.Forms.Label
    End Class
End Namespace