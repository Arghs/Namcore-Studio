Namespace Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    <Global.System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726")> _
    Partial Class LoginExplorer
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
        Friend WithEvents UsernameTextBox As System.Windows.Forms.TextBox
        Friend WithEvents PasswordTextBox As System.Windows.Forms.TextBox

        'Wird vom Windows Form-Designer benötigt.
        Private components As System.ComponentModel.IContainer

        'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
        'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
        'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Me.UsernameTextBox = New System.Windows.Forms.TextBox()
            Me.PasswordTextBox = New System.Windows.Forms.TextBox()
            Me.login_label = New System.Windows.Forms.Label()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.GroupBox = New System.Windows.Forms.GroupBox()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.ok_bt = New System.Windows.Forms.Button()
            Me.cancel_bt = New System.Windows.Forms.Button()
            Me.PictureBox1 = New System.Windows.Forms.PictureBox()
            Me.GroupBox.SuspendLayout()
            CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'UsernameTextBox
            '
            Me.UsernameTextBox.Location = New System.Drawing.Point(9, 28)
            Me.UsernameTextBox.Name = "UsernameTextBox"
            Me.UsernameTextBox.Size = New System.Drawing.Size(220, 20)
            Me.UsernameTextBox.TabIndex = 1
            '
            'PasswordTextBox
            '
            Me.PasswordTextBox.Location = New System.Drawing.Point(9, 72)
            Me.PasswordTextBox.Name = "PasswordTextBox"
            Me.PasswordTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
            Me.PasswordTextBox.Size = New System.Drawing.Size(220, 20)
            Me.PasswordTextBox.TabIndex = 3
            '
            'login_label
            '
            Me.login_label.AutoSize = True
            Me.login_label.BackColor = System.Drawing.Color.Transparent
            Me.login_label.Font = New System.Drawing.Font("Franklin Gothic Medium", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.login_label.ForeColor = System.Drawing.Color.Black
            Me.login_label.Location = New System.Drawing.Point(11, 9)
            Me.login_label.Name = "login_label"
            Me.login_label.Size = New System.Drawing.Size(220, 24)
            Me.login_label.TabIndex = 243
            Me.login_label.Text = "Enter login information"
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.BackColor = System.Drawing.Color.Transparent
            Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
            Me.Label3.ForeColor = System.Drawing.Color.Black
            Me.Label3.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.Label3.Location = New System.Drawing.Point(6, 10)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(77, 15)
            Me.Label3.TabIndex = 244
            Me.Label3.Text = "Username:"
            '
            'GroupBox
            '
            Me.GroupBox.BackColor = System.Drawing.Color.Transparent
            Me.GroupBox.Controls.Add(Me.Label1)
            Me.GroupBox.Controls.Add(Me.Label3)
            Me.GroupBox.Controls.Add(Me.UsernameTextBox)
            Me.GroupBox.Controls.Add(Me.PasswordTextBox)
            Me.GroupBox.Location = New System.Drawing.Point(13, 120)
            Me.GroupBox.Name = "GroupBox"
            Me.GroupBox.Size = New System.Drawing.Size(238, 102)
            Me.GroupBox.TabIndex = 245
            Me.GroupBox.TabStop = False
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.BackColor = System.Drawing.Color.Transparent
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
            Me.Label1.ForeColor = System.Drawing.Color.Black
            Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.Label1.Location = New System.Drawing.Point(6, 54)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(73, 15)
            Me.Label1.TabIndex = 245
            Me.Label1.Text = "Password:"
            '
            'ok_bt
            '
            Me.ok_bt.BackColor = System.Drawing.Color.DimGray
            Me.ok_bt.Cursor = System.Windows.Forms.Cursors.Hand
            Me.ok_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.ok_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.ok_bt.Location = New System.Drawing.Point(13, 228)
            Me.ok_bt.Name = "ok_bt"
            Me.ok_bt.Size = New System.Drawing.Size(109, 27)
            Me.ok_bt.TabIndex = 246
            Me.ok_bt.Text = "OK"
            Me.ok_bt.UseVisualStyleBackColor = False
            '
            'cancel_bt
            '
            Me.cancel_bt.BackColor = System.Drawing.Color.DimGray
            Me.cancel_bt.Cursor = System.Windows.Forms.Cursors.Hand
            Me.cancel_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.cancel_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.cancel_bt.Location = New System.Drawing.Point(142, 228)
            Me.cancel_bt.Name = "cancel_bt"
            Me.cancel_bt.Size = New System.Drawing.Size(109, 27)
            Me.cancel_bt.TabIndex = 247
            Me.cancel_bt.Text = "Cancel"
            Me.cancel_bt.UseVisualStyleBackColor = False
            '
            'PictureBox1
            '
            Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
            Me.PictureBox1.BackgroundImage = Global.NamCore_Studio.My.Resources.Resources.finalnclogo
            Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.PictureBox1.Location = New System.Drawing.Point(12, 36)
            Me.PictureBox1.Name = "PictureBox1"
            Me.PictureBox1.Size = New System.Drawing.Size(275, 81)
            Me.PictureBox1.TabIndex = 248
            Me.PictureBox1.TabStop = False
            '
            'LoginExplorer
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackgroundImage = Global.NamCore_Studio.My.Resources.Resources.HUD_bg
            Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.ClientSize = New System.Drawing.Size(296, 263)
            Me.Controls.Add(Me.PictureBox1)
            Me.Controls.Add(Me.cancel_bt)
            Me.Controls.Add(Me.ok_bt)
            Me.Controls.Add(Me.GroupBox)
            Me.Controls.Add(Me.login_label)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "LoginExplorer"
            Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "LoginExplorer"
            Me.TopMost = True
            Me.GroupBox.ResumeLayout(False)
            Me.GroupBox.PerformLayout()
            CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents login_label As System.Windows.Forms.Label
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents GroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents ok_bt As System.Windows.Forms.Button
        Friend WithEvents cancel_bt As System.Windows.Forms.Button
        Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox

    End Class
End Namespace