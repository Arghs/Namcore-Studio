<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Filter_accounts
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
        Me.savelogin_bt = New System.Windows.Forms.Button()
        Me.idcombo1 = New System.Windows.Forms.ComboBox()
        Me.idtxtbox1 = New System.Windows.Forms.TextBox()
        Me.idtxtbox2 = New System.Windows.Forms.TextBox()
        Me.idcombo2 = New System.Windows.Forms.ComboBox()
        Me.nametxtbox1 = New System.Windows.Forms.TextBox()
        Me.namecombo1 = New System.Windows.Forms.ComboBox()
        Me.gmtxtbox2 = New System.Windows.Forms.TextBox()
        Me.gmcombo2 = New System.Windows.Forms.ComboBox()
        Me.gmtxtbox1 = New System.Windows.Forms.TextBox()
        Me.gmcombo1 = New System.Windows.Forms.ComboBox()
        Me.logincombo2 = New System.Windows.Forms.ComboBox()
        Me.logincombo1 = New System.Windows.Forms.ComboBox()
        Me.emailtxtbox1 = New System.Windows.Forms.TextBox()
        Me.emailcombo1 = New System.Windows.Forms.ComboBox()
        Me.idcheck = New System.Windows.Forms.CheckBox()
        Me.namecheck = New System.Windows.Forms.CheckBox()
        Me.gmcheck = New System.Windows.Forms.CheckBox()
        Me.logincheck = New System.Windows.Forms.CheckBox()
        Me.emailcheck = New System.Windows.Forms.CheckBox()
        Me.datemin = New System.Windows.Forms.DateTimePicker()
        Me.datemax = New System.Windows.Forms.DateTimePicker()
        Me.SuspendLayout()
        '
        'savelogin_bt
        '
        Me.savelogin_bt.BackColor = System.Drawing.Color.DimGray
        Me.savelogin_bt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.savelogin_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.savelogin_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.savelogin_bt.Location = New System.Drawing.Point(201, 191)
        Me.savelogin_bt.Name = "savelogin_bt"
        Me.savelogin_bt.Size = New System.Drawing.Size(121, 33)
        Me.savelogin_bt.TabIndex = 211
        Me.savelogin_bt.Text = "Apply"
        Me.savelogin_bt.UseVisualStyleBackColor = False
        '
        'idcombo1
        '
        Me.idcombo1.FormattingEnabled = True
        Me.idcombo1.Items.AddRange(New Object() {"", "=", ">", "<"})
        Me.idcombo1.Location = New System.Drawing.Point(97, 19)
        Me.idcombo1.Name = "idcombo1"
        Me.idcombo1.Size = New System.Drawing.Size(63, 21)
        Me.idcombo1.TabIndex = 213
        Me.idcombo1.Text = "Select"
        '
        'idtxtbox1
        '
        Me.idtxtbox1.Location = New System.Drawing.Point(166, 21)
        Me.idtxtbox1.Name = "idtxtbox1"
        Me.idtxtbox1.Size = New System.Drawing.Size(155, 20)
        Me.idtxtbox1.TabIndex = 214
        '
        'idtxtbox2
        '
        Me.idtxtbox2.Location = New System.Drawing.Point(396, 20)
        Me.idtxtbox2.Name = "idtxtbox2"
        Me.idtxtbox2.Size = New System.Drawing.Size(155, 20)
        Me.idtxtbox2.TabIndex = 216
        '
        'idcombo2
        '
        Me.idcombo2.FormattingEnabled = True
        Me.idcombo2.Items.AddRange(New Object() {"", ">", "<"})
        Me.idcombo2.Location = New System.Drawing.Point(327, 20)
        Me.idcombo2.Name = "idcombo2"
        Me.idcombo2.Size = New System.Drawing.Size(63, 21)
        Me.idcombo2.TabIndex = 215
        Me.idcombo2.Text = "Select"
        '
        'nametxtbox1
        '
        Me.nametxtbox1.Location = New System.Drawing.Point(166, 48)
        Me.nametxtbox1.Name = "nametxtbox1"
        Me.nametxtbox1.Size = New System.Drawing.Size(155, 20)
        Me.nametxtbox1.TabIndex = 219
        '
        'namecombo1
        '
        Me.namecombo1.FormattingEnabled = True
        Me.namecombo1.Items.AddRange(New Object() {"", "=", "contains"})
        Me.namecombo1.Location = New System.Drawing.Point(97, 46)
        Me.namecombo1.Name = "namecombo1"
        Me.namecombo1.Size = New System.Drawing.Size(63, 21)
        Me.namecombo1.TabIndex = 218
        Me.namecombo1.Text = "Select"
        '
        'gmtxtbox2
        '
        Me.gmtxtbox2.Location = New System.Drawing.Point(396, 74)
        Me.gmtxtbox2.Name = "gmtxtbox2"
        Me.gmtxtbox2.Size = New System.Drawing.Size(155, 20)
        Me.gmtxtbox2.TabIndex = 226
        '
        'gmcombo2
        '
        Me.gmcombo2.FormattingEnabled = True
        Me.gmcombo2.Items.AddRange(New Object() {">", "<"})
        Me.gmcombo2.Location = New System.Drawing.Point(327, 74)
        Me.gmcombo2.Name = "gmcombo2"
        Me.gmcombo2.Size = New System.Drawing.Size(63, 21)
        Me.gmcombo2.TabIndex = 225
        Me.gmcombo2.Text = "Select"
        '
        'gmtxtbox1
        '
        Me.gmtxtbox1.Location = New System.Drawing.Point(166, 75)
        Me.gmtxtbox1.Name = "gmtxtbox1"
        Me.gmtxtbox1.Size = New System.Drawing.Size(155, 20)
        Me.gmtxtbox1.TabIndex = 224
        '
        'gmcombo1
        '
        Me.gmcombo1.FormattingEnabled = True
        Me.gmcombo1.Items.AddRange(New Object() {"", "=", ">", "<"})
        Me.gmcombo1.Location = New System.Drawing.Point(97, 73)
        Me.gmcombo1.Name = "gmcombo1"
        Me.gmcombo1.Size = New System.Drawing.Size(63, 21)
        Me.gmcombo1.TabIndex = 223
        Me.gmcombo1.Text = "Select"
        '
        'logincombo2
        '
        Me.logincombo2.FormattingEnabled = True
        Me.logincombo2.Items.AddRange(New Object() {"", ">=", "<="})
        Me.logincombo2.Location = New System.Drawing.Point(327, 101)
        Me.logincombo2.Name = "logincombo2"
        Me.logincombo2.Size = New System.Drawing.Size(63, 21)
        Me.logincombo2.TabIndex = 230
        Me.logincombo2.Text = "Select"
        '
        'logincombo1
        '
        Me.logincombo1.FormattingEnabled = True
        Me.logincombo1.Items.AddRange(New Object() {"", "=", ">=", "<="})
        Me.logincombo1.Location = New System.Drawing.Point(97, 100)
        Me.logincombo1.Name = "logincombo1"
        Me.logincombo1.Size = New System.Drawing.Size(63, 21)
        Me.logincombo1.TabIndex = 228
        Me.logincombo1.Text = "Select"
        '
        'emailtxtbox1
        '
        Me.emailtxtbox1.Location = New System.Drawing.Point(166, 129)
        Me.emailtxtbox1.Name = "emailtxtbox1"
        Me.emailtxtbox1.Size = New System.Drawing.Size(155, 20)
        Me.emailtxtbox1.TabIndex = 234
        '
        'emailcombo1
        '
        Me.emailcombo1.FormattingEnabled = True
        Me.emailcombo1.Items.AddRange(New Object() {"", "=", "contains"})
        Me.emailcombo1.Location = New System.Drawing.Point(97, 127)
        Me.emailcombo1.Name = "emailcombo1"
        Me.emailcombo1.Size = New System.Drawing.Size(63, 21)
        Me.emailcombo1.TabIndex = 233
        Me.emailcombo1.Text = "Select"
        '
        'idcheck
        '
        Me.idcheck.AutoSize = True
        Me.idcheck.Location = New System.Drawing.Point(21, 22)
        Me.idcheck.Name = "idcheck"
        Me.idcheck.Size = New System.Drawing.Size(37, 17)
        Me.idcheck.TabIndex = 235
        Me.idcheck.Text = "ID"
        Me.idcheck.UseVisualStyleBackColor = True
        '
        'namecheck
        '
        Me.namecheck.AutoSize = True
        Me.namecheck.Location = New System.Drawing.Point(21, 48)
        Me.namecheck.Name = "namecheck"
        Me.namecheck.Size = New System.Drawing.Size(54, 17)
        Me.namecheck.TabIndex = 236
        Me.namecheck.Text = "Name"
        Me.namecheck.UseVisualStyleBackColor = True
        '
        'gmcheck
        '
        Me.gmcheck.AutoSize = True
        Me.gmcheck.Location = New System.Drawing.Point(21, 75)
        Me.gmcheck.Name = "gmcheck"
        Me.gmcheck.Size = New System.Drawing.Size(72, 17)
        Me.gmcheck.TabIndex = 237
        Me.gmcheck.Text = "GM Level"
        Me.gmcheck.UseVisualStyleBackColor = True
        '
        'logincheck
        '
        Me.logincheck.AutoSize = True
        Me.logincheck.Location = New System.Drawing.Point(21, 102)
        Me.logincheck.Name = "logincheck"
        Me.logincheck.Size = New System.Drawing.Size(75, 17)
        Me.logincheck.TabIndex = 238
        Me.logincheck.Text = "Last Login"
        Me.logincheck.UseVisualStyleBackColor = True
        '
        'emailcheck
        '
        Me.emailcheck.AutoSize = True
        Me.emailcheck.Location = New System.Drawing.Point(21, 129)
        Me.emailcheck.Name = "emailcheck"
        Me.emailcheck.Size = New System.Drawing.Size(51, 17)
        Me.emailcheck.TabIndex = 239
        Me.emailcheck.Text = "Email"
        Me.emailcheck.UseVisualStyleBackColor = True
        '
        'datemin
        '
        Me.datemin.CustomFormat = "yyy-MM-dd HH:mm:ss"
        Me.datemin.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.datemin.Location = New System.Drawing.Point(166, 103)
        Me.datemin.Name = "datemin"
        Me.datemin.Size = New System.Drawing.Size(155, 20)
        Me.datemin.TabIndex = 240
        '
        'datemax
        '
        Me.datemax.CustomFormat = "yyy-MM-dd HH:mm:ss"
        Me.datemax.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.datemax.Location = New System.Drawing.Point(396, 103)
        Me.datemax.Name = "datemax"
        Me.datemax.Size = New System.Drawing.Size(155, 20)
        Me.datemax.TabIndex = 241
        '
        'Filter_accounts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(556, 236)
        Me.Controls.Add(Me.datemax)
        Me.Controls.Add(Me.datemin)
        Me.Controls.Add(Me.emailcheck)
        Me.Controls.Add(Me.logincheck)
        Me.Controls.Add(Me.gmcheck)
        Me.Controls.Add(Me.namecheck)
        Me.Controls.Add(Me.idcheck)
        Me.Controls.Add(Me.emailtxtbox1)
        Me.Controls.Add(Me.emailcombo1)
        Me.Controls.Add(Me.logincombo2)
        Me.Controls.Add(Me.logincombo1)
        Me.Controls.Add(Me.gmtxtbox2)
        Me.Controls.Add(Me.gmcombo2)
        Me.Controls.Add(Me.gmtxtbox1)
        Me.Controls.Add(Me.gmcombo1)
        Me.Controls.Add(Me.nametxtbox1)
        Me.Controls.Add(Me.namecombo1)
        Me.Controls.Add(Me.idtxtbox2)
        Me.Controls.Add(Me.idcombo2)
        Me.Controls.Add(Me.idtxtbox1)
        Me.Controls.Add(Me.idcombo1)
        Me.Controls.Add(Me.savelogin_bt)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Filter_accounts"
        Me.Text = "Filter_accounts"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents savelogin_bt As System.Windows.Forms.Button
    Friend WithEvents idcombo1 As System.Windows.Forms.ComboBox
    Friend WithEvents idtxtbox1 As System.Windows.Forms.TextBox
    Friend WithEvents idtxtbox2 As System.Windows.Forms.TextBox
    Friend WithEvents idcombo2 As System.Windows.Forms.ComboBox
    Friend WithEvents nametxtbox1 As System.Windows.Forms.TextBox
    Friend WithEvents namecombo1 As System.Windows.Forms.ComboBox
    Friend WithEvents gmtxtbox2 As System.Windows.Forms.TextBox
    Friend WithEvents gmcombo2 As System.Windows.Forms.ComboBox
    Friend WithEvents gmtxtbox1 As System.Windows.Forms.TextBox
    Friend WithEvents gmcombo1 As System.Windows.Forms.ComboBox
    Friend WithEvents logincombo2 As System.Windows.Forms.ComboBox
    Friend WithEvents logincombo1 As System.Windows.Forms.ComboBox
    Friend WithEvents emailtxtbox1 As System.Windows.Forms.TextBox
    Friend WithEvents emailcombo1 As System.Windows.Forms.ComboBox
    Friend WithEvents idcheck As System.Windows.Forms.CheckBox
    Friend WithEvents namecheck As System.Windows.Forms.CheckBox
    Friend WithEvents gmcheck As System.Windows.Forms.CheckBox
    Friend WithEvents logincheck As System.Windows.Forms.CheckBox
    Friend WithEvents emailcheck As System.Windows.Forms.CheckBox
    Friend WithEvents datemin As System.Windows.Forms.DateTimePicker
    Friend WithEvents datemax As System.Windows.Forms.DateTimePicker
End Class
