Namespace Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FilterAccounts
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FilterAccounts))
            Me.ApplyFilter = New System.Windows.Forms.Button()
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
            Me.filter_label = New System.Windows.Forms.Label()
            Me.close_bt = New System.Windows.Forms.Button()
            Me.SuspendLayout()
            '
            'ApplyFilter
            '
            Me.ApplyFilter.BackColor = System.Drawing.Color.DimGray
            Me.ApplyFilter.Cursor = System.Windows.Forms.Cursors.Hand
            Me.ApplyFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.ApplyFilter.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.ApplyFilter.Location = New System.Drawing.Point(11, 170)
            Me.ApplyFilter.Name = "ApplyFilter"
            Me.ApplyFilter.Size = New System.Drawing.Size(180, 40)
            Me.ApplyFilter.TabIndex = 211
            Me.ApplyFilter.Text = "Apply"
            Me.ApplyFilter.UseVisualStyleBackColor = False
            '
            'idcombo1
            '
            Me.idcombo1.FormattingEnabled = True
            Me.idcombo1.Items.AddRange(New Object() {"", "=", ">", "<"})
            Me.idcombo1.Location = New System.Drawing.Point(88, 30)
            Me.idcombo1.Name = "idcombo1"
            Me.idcombo1.Size = New System.Drawing.Size(63, 21)
            Me.idcombo1.TabIndex = 213
            Me.idcombo1.Text = "Select"
            '
            'idtxtbox1
            '
            Me.idtxtbox1.Location = New System.Drawing.Point(157, 32)
            Me.idtxtbox1.Name = "idtxtbox1"
            Me.idtxtbox1.Size = New System.Drawing.Size(155, 20)
            Me.idtxtbox1.TabIndex = 214
            '
            'idtxtbox2
            '
            Me.idtxtbox2.Location = New System.Drawing.Point(387, 31)
            Me.idtxtbox2.Name = "idtxtbox2"
            Me.idtxtbox2.Size = New System.Drawing.Size(155, 20)
            Me.idtxtbox2.TabIndex = 216
            '
            'idcombo2
            '
            Me.idcombo2.FormattingEnabled = True
            Me.idcombo2.Items.AddRange(New Object() {"", ">", "<"})
            Me.idcombo2.Location = New System.Drawing.Point(318, 31)
            Me.idcombo2.Name = "idcombo2"
            Me.idcombo2.Size = New System.Drawing.Size(63, 21)
            Me.idcombo2.TabIndex = 215
            Me.idcombo2.Text = "Select"
            '
            'nametxtbox1
            '
            Me.nametxtbox1.Location = New System.Drawing.Point(157, 59)
            Me.nametxtbox1.Name = "nametxtbox1"
            Me.nametxtbox1.Size = New System.Drawing.Size(155, 20)
            Me.nametxtbox1.TabIndex = 219
            '
            'namecombo1
            '
            Me.namecombo1.FormattingEnabled = True
            Me.namecombo1.Items.AddRange(New Object() {"", "=", "contains"})
            Me.namecombo1.Location = New System.Drawing.Point(88, 57)
            Me.namecombo1.Name = "namecombo1"
            Me.namecombo1.Size = New System.Drawing.Size(63, 21)
            Me.namecombo1.TabIndex = 218
            Me.namecombo1.Text = "Select"
            '
            'gmtxtbox2
            '
            Me.gmtxtbox2.Location = New System.Drawing.Point(387, 85)
            Me.gmtxtbox2.Name = "gmtxtbox2"
            Me.gmtxtbox2.Size = New System.Drawing.Size(155, 20)
            Me.gmtxtbox2.TabIndex = 226
            '
            'gmcombo2
            '
            Me.gmcombo2.FormattingEnabled = True
            Me.gmcombo2.Items.AddRange(New Object() {">", "<"})
            Me.gmcombo2.Location = New System.Drawing.Point(318, 85)
            Me.gmcombo2.Name = "gmcombo2"
            Me.gmcombo2.Size = New System.Drawing.Size(63, 21)
            Me.gmcombo2.TabIndex = 225
            Me.gmcombo2.Text = "Select"
            '
            'gmtxtbox1
            '
            Me.gmtxtbox1.Location = New System.Drawing.Point(157, 86)
            Me.gmtxtbox1.Name = "gmtxtbox1"
            Me.gmtxtbox1.Size = New System.Drawing.Size(155, 20)
            Me.gmtxtbox1.TabIndex = 224
            '
            'gmcombo1
            '
            Me.gmcombo1.FormattingEnabled = True
            Me.gmcombo1.Items.AddRange(New Object() {"", "=", ">", "<"})
            Me.gmcombo1.Location = New System.Drawing.Point(88, 84)
            Me.gmcombo1.Name = "gmcombo1"
            Me.gmcombo1.Size = New System.Drawing.Size(63, 21)
            Me.gmcombo1.TabIndex = 223
            Me.gmcombo1.Text = "Select"
            '
            'logincombo2
            '
            Me.logincombo2.FormattingEnabled = True
            Me.logincombo2.Items.AddRange(New Object() {"", ">=", "<="})
            Me.logincombo2.Location = New System.Drawing.Point(318, 112)
            Me.logincombo2.Name = "logincombo2"
            Me.logincombo2.Size = New System.Drawing.Size(63, 21)
            Me.logincombo2.TabIndex = 230
            Me.logincombo2.Text = "Select"
            '
            'logincombo1
            '
            Me.logincombo1.FormattingEnabled = True
            Me.logincombo1.Items.AddRange(New Object() {"", "=", ">=", "<="})
            Me.logincombo1.Location = New System.Drawing.Point(88, 111)
            Me.logincombo1.Name = "logincombo1"
            Me.logincombo1.Size = New System.Drawing.Size(63, 21)
            Me.logincombo1.TabIndex = 228
            Me.logincombo1.Text = "Select"
            '
            'emailtxtbox1
            '
            Me.emailtxtbox1.Location = New System.Drawing.Point(157, 140)
            Me.emailtxtbox1.Name = "emailtxtbox1"
            Me.emailtxtbox1.Size = New System.Drawing.Size(155, 20)
            Me.emailtxtbox1.TabIndex = 234
            '
            'emailcombo1
            '
            Me.emailcombo1.FormattingEnabled = True
            Me.emailcombo1.Items.AddRange(New Object() {"", "=", "contains"})
            Me.emailcombo1.Location = New System.Drawing.Point(88, 138)
            Me.emailcombo1.Name = "emailcombo1"
            Me.emailcombo1.Size = New System.Drawing.Size(63, 21)
            Me.emailcombo1.TabIndex = 233
            Me.emailcombo1.Text = "Select"
            '
            'idcheck
            '
            Me.idcheck.AutoSize = True
            Me.idcheck.BackColor = System.Drawing.Color.Transparent
            Me.idcheck.ForeColor = System.Drawing.Color.SteelBlue
            Me.idcheck.Location = New System.Drawing.Point(12, 33)
            Me.idcheck.Name = "idcheck"
            Me.idcheck.Size = New System.Drawing.Size(37, 17)
            Me.idcheck.TabIndex = 235
            Me.idcheck.Text = "ID"
            Me.idcheck.UseVisualStyleBackColor = False
            '
            'namecheck
            '
            Me.namecheck.AutoSize = True
            Me.namecheck.BackColor = System.Drawing.Color.Transparent
            Me.namecheck.ForeColor = System.Drawing.Color.SteelBlue
            Me.namecheck.Location = New System.Drawing.Point(12, 59)
            Me.namecheck.Name = "namecheck"
            Me.namecheck.Size = New System.Drawing.Size(54, 17)
            Me.namecheck.TabIndex = 236
            Me.namecheck.Text = "Name"
            Me.namecheck.UseVisualStyleBackColor = False
            '
            'gmcheck
            '
            Me.gmcheck.AutoSize = True
            Me.gmcheck.BackColor = System.Drawing.Color.Transparent
            Me.gmcheck.ForeColor = System.Drawing.Color.SteelBlue
            Me.gmcheck.Location = New System.Drawing.Point(12, 86)
            Me.gmcheck.Name = "gmcheck"
            Me.gmcheck.Size = New System.Drawing.Size(72, 17)
            Me.gmcheck.TabIndex = 237
            Me.gmcheck.Text = "GM Level"
            Me.gmcheck.UseVisualStyleBackColor = False
            '
            'logincheck
            '
            Me.logincheck.AutoSize = True
            Me.logincheck.BackColor = System.Drawing.Color.Transparent
            Me.logincheck.ForeColor = System.Drawing.Color.SteelBlue
            Me.logincheck.Location = New System.Drawing.Point(12, 113)
            Me.logincheck.Name = "logincheck"
            Me.logincheck.Size = New System.Drawing.Size(75, 17)
            Me.logincheck.TabIndex = 238
            Me.logincheck.Text = "Last Login"
            Me.logincheck.UseVisualStyleBackColor = False
            '
            'emailcheck
            '
            Me.emailcheck.AutoSize = True
            Me.emailcheck.BackColor = System.Drawing.Color.Transparent
            Me.emailcheck.ForeColor = System.Drawing.Color.SteelBlue
            Me.emailcheck.Location = New System.Drawing.Point(12, 140)
            Me.emailcheck.Name = "emailcheck"
            Me.emailcheck.Size = New System.Drawing.Size(51, 17)
            Me.emailcheck.TabIndex = 239
            Me.emailcheck.Text = "Email"
            Me.emailcheck.UseVisualStyleBackColor = False
            '
            'datemin
            '
            Me.datemin.CustomFormat = "yyy-MM-dd HH:mm:ss"
            Me.datemin.Format = System.Windows.Forms.DateTimePickerFormat.Custom
            Me.datemin.Location = New System.Drawing.Point(157, 114)
            Me.datemin.Name = "datemin"
            Me.datemin.Size = New System.Drawing.Size(155, 20)
            Me.datemin.TabIndex = 240
            '
            'datemax
            '
            Me.datemax.CustomFormat = "yyy-MM-dd HH:mm:ss"
            Me.datemax.Format = System.Windows.Forms.DateTimePickerFormat.Custom
            Me.datemax.Location = New System.Drawing.Point(387, 114)
            Me.datemax.Name = "datemax"
            Me.datemax.Size = New System.Drawing.Size(155, 20)
            Me.datemax.TabIndex = 241
            '
            'filter_label
            '
            Me.filter_label.AutoSize = True
            Me.filter_label.BackColor = System.Drawing.Color.Transparent
            Me.filter_label.Font = New System.Drawing.Font("Franklin Gothic Medium", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.filter_label.ForeColor = System.Drawing.Color.SteelBlue
            Me.filter_label.Location = New System.Drawing.Point(7, 6)
            Me.filter_label.Name = "filter_label"
            Me.filter_label.Size = New System.Drawing.Size(145, 24)
            Me.filter_label.TabIndex = 242
            Me.filter_label.Text = "Filter accounts"
            '
            'close_bt
            '
            Me.close_bt.BackColor = System.Drawing.Color.DimGray
            Me.close_bt.Cursor = System.Windows.Forms.Cursors.Hand
            Me.close_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.close_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.close_bt.Location = New System.Drawing.Point(197, 170)
            Me.close_bt.Name = "close_bt"
            Me.close_bt.Size = New System.Drawing.Size(180, 40)
            Me.close_bt.TabIndex = 278
            Me.close_bt.Text = "Close"
            Me.close_bt.UseVisualStyleBackColor = False
            '
            'FilterAccounts
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackgroundImage = Global.Namcore_Studio.My.Resources.Resources.cleanbg
            Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.ClientSize = New System.Drawing.Size(553, 216)
            Me.Controls.Add(Me.close_bt)
            Me.Controls.Add(Me.filter_label)
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
            Me.Controls.Add(Me.ApplyFilter)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FilterAccounts"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Filter_accounts"
            Me.TopMost = True
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents ApplyFilter As System.Windows.Forms.Button
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
        Friend WithEvents filter_label As System.Windows.Forms.Label
        Friend WithEvents close_bt As System.Windows.Forms.Button
    End Class
End Namespace