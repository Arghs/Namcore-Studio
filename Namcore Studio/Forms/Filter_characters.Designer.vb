<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Filter_characters
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
        Me.classcheck = New System.Windows.Forms.CheckBox()
        Me.racecheck = New System.Windows.Forms.CheckBox()
        Me.namecheck = New System.Windows.Forms.CheckBox()
        Me.idcheck = New System.Windows.Forms.CheckBox()
        Me.nametxtbox1 = New System.Windows.Forms.TextBox()
        Me.namecombo1 = New System.Windows.Forms.ComboBox()
        Me.idtxtbox2 = New System.Windows.Forms.TextBox()
        Me.idcombo2 = New System.Windows.Forms.ComboBox()
        Me.idtxtbox1 = New System.Windows.Forms.TextBox()
        Me.idcombo1 = New System.Windows.Forms.ComboBox()
        Me.accidcheck = New System.Windows.Forms.CheckBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.accidcombo1 = New System.Windows.Forms.ComboBox()
        Me.levelcheck = New System.Windows.Forms.CheckBox()
        Me.leveltxtbox2 = New System.Windows.Forms.TextBox()
        Me.levelcombo2 = New System.Windows.Forms.ComboBox()
        Me.leveltxtbox1 = New System.Windows.Forms.TextBox()
        Me.levelcombo1 = New System.Windows.Forms.ComboBox()
        Me.racecombo = New System.Windows.Forms.ComboBox()
        Me.classcombo = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'savelogin_bt
        '
        Me.savelogin_bt.BackColor = System.Drawing.Color.DimGray
        Me.savelogin_bt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.savelogin_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.savelogin_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.savelogin_bt.Location = New System.Drawing.Point(197, 210)
        Me.savelogin_bt.Name = "savelogin_bt"
        Me.savelogin_bt.Size = New System.Drawing.Size(121, 33)
        Me.savelogin_bt.TabIndex = 212
        Me.savelogin_bt.Text = "Apply"
        Me.savelogin_bt.UseVisualStyleBackColor = False
        '
        'classcheck
        '
        Me.classcheck.AutoSize = True
        Me.classcheck.Location = New System.Drawing.Point(12, 126)
        Me.classcheck.Name = "classcheck"
        Me.classcheck.Size = New System.Drawing.Size(51, 17)
        Me.classcheck.TabIndex = 259
        Me.classcheck.Text = "Class"
        Me.classcheck.UseVisualStyleBackColor = True
        '
        'racecheck
        '
        Me.racecheck.AutoSize = True
        Me.racecheck.Location = New System.Drawing.Point(12, 99)
        Me.racecheck.Name = "racecheck"
        Me.racecheck.Size = New System.Drawing.Size(52, 17)
        Me.racecheck.TabIndex = 258
        Me.racecheck.Text = "Race"
        Me.racecheck.UseVisualStyleBackColor = True
        '
        'namecheck
        '
        Me.namecheck.AutoSize = True
        Me.namecheck.Location = New System.Drawing.Point(12, 72)
        Me.namecheck.Name = "namecheck"
        Me.namecheck.Size = New System.Drawing.Size(54, 17)
        Me.namecheck.TabIndex = 257
        Me.namecheck.Text = "Name"
        Me.namecheck.UseVisualStyleBackColor = True
        '
        'idcheck
        '
        Me.idcheck.AutoSize = True
        Me.idcheck.Location = New System.Drawing.Point(12, 12)
        Me.idcheck.Name = "idcheck"
        Me.idcheck.Size = New System.Drawing.Size(53, 17)
        Me.idcheck.TabIndex = 256
        Me.idcheck.Text = "GUID"
        Me.idcheck.UseVisualStyleBackColor = True
        '
        'nametxtbox1
        '
        Me.nametxtbox1.Location = New System.Drawing.Point(163, 72)
        Me.nametxtbox1.Name = "nametxtbox1"
        Me.nametxtbox1.Size = New System.Drawing.Size(155, 20)
        Me.nametxtbox1.TabIndex = 247
        '
        'namecombo1
        '
        Me.namecombo1.FormattingEnabled = True
        Me.namecombo1.Items.AddRange(New Object() {"", "=", "contains"})
        Me.namecombo1.Location = New System.Drawing.Point(94, 70)
        Me.namecombo1.Name = "namecombo1"
        Me.namecombo1.Size = New System.Drawing.Size(63, 21)
        Me.namecombo1.TabIndex = 246
        Me.namecombo1.Text = "Select"
        '
        'idtxtbox2
        '
        Me.idtxtbox2.Location = New System.Drawing.Point(393, 10)
        Me.idtxtbox2.Name = "idtxtbox2"
        Me.idtxtbox2.Size = New System.Drawing.Size(155, 20)
        Me.idtxtbox2.TabIndex = 245
        '
        'idcombo2
        '
        Me.idcombo2.FormattingEnabled = True
        Me.idcombo2.Items.AddRange(New Object() {"", ">", "<"})
        Me.idcombo2.Location = New System.Drawing.Point(324, 10)
        Me.idcombo2.Name = "idcombo2"
        Me.idcombo2.Size = New System.Drawing.Size(63, 21)
        Me.idcombo2.TabIndex = 244
        Me.idcombo2.Text = "Select"
        '
        'idtxtbox1
        '
        Me.idtxtbox1.Location = New System.Drawing.Point(163, 11)
        Me.idtxtbox1.Name = "idtxtbox1"
        Me.idtxtbox1.Size = New System.Drawing.Size(155, 20)
        Me.idtxtbox1.TabIndex = 243
        '
        'idcombo1
        '
        Me.idcombo1.FormattingEnabled = True
        Me.idcombo1.Items.AddRange(New Object() {"", "=", ">", "<"})
        Me.idcombo1.Location = New System.Drawing.Point(94, 9)
        Me.idcombo1.Name = "idcombo1"
        Me.idcombo1.Size = New System.Drawing.Size(63, 21)
        Me.idcombo1.TabIndex = 242
        Me.idcombo1.Text = "Select"
        '
        'accidcheck
        '
        Me.accidcheck.AutoSize = True
        Me.accidcheck.Location = New System.Drawing.Point(12, 40)
        Me.accidcheck.Name = "accidcheck"
        Me.accidcheck.Size = New System.Drawing.Size(80, 17)
        Me.accidcheck.TabIndex = 267
        Me.accidcheck.Text = "Account ID"
        Me.accidcheck.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(393, 38)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(155, 20)
        Me.TextBox1.TabIndex = 266
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"", ">", "<"})
        Me.ComboBox1.Location = New System.Drawing.Point(324, 38)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(63, 21)
        Me.ComboBox1.TabIndex = 265
        Me.ComboBox1.Text = "Select"
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(163, 39)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(155, 20)
        Me.TextBox2.TabIndex = 264
        '
        'accidcombo1
        '
        Me.accidcombo1.FormattingEnabled = True
        Me.accidcombo1.Items.AddRange(New Object() {"", "=", ">", "<"})
        Me.accidcombo1.Location = New System.Drawing.Point(94, 37)
        Me.accidcombo1.Name = "accidcombo1"
        Me.accidcombo1.Size = New System.Drawing.Size(63, 21)
        Me.accidcombo1.TabIndex = 263
        Me.accidcombo1.Text = "Select"
        '
        'levelcheck
        '
        Me.levelcheck.AutoSize = True
        Me.levelcheck.Location = New System.Drawing.Point(12, 157)
        Me.levelcheck.Name = "levelcheck"
        Me.levelcheck.Size = New System.Drawing.Size(52, 17)
        Me.levelcheck.TabIndex = 272
        Me.levelcheck.Text = "Level"
        Me.levelcheck.UseVisualStyleBackColor = True
        '
        'leveltxtbox2
        '
        Me.leveltxtbox2.Location = New System.Drawing.Point(393, 155)
        Me.leveltxtbox2.Name = "leveltxtbox2"
        Me.leveltxtbox2.Size = New System.Drawing.Size(155, 20)
        Me.leveltxtbox2.TabIndex = 271
        '
        'levelcombo2
        '
        Me.levelcombo2.FormattingEnabled = True
        Me.levelcombo2.Items.AddRange(New Object() {"", ">", "<"})
        Me.levelcombo2.Location = New System.Drawing.Point(324, 155)
        Me.levelcombo2.Name = "levelcombo2"
        Me.levelcombo2.Size = New System.Drawing.Size(63, 21)
        Me.levelcombo2.TabIndex = 270
        Me.levelcombo2.Text = "Select"
        '
        'leveltxtbox1
        '
        Me.leveltxtbox1.Location = New System.Drawing.Point(163, 156)
        Me.leveltxtbox1.Name = "leveltxtbox1"
        Me.leveltxtbox1.Size = New System.Drawing.Size(155, 20)
        Me.leveltxtbox1.TabIndex = 269
        '
        'levelcombo1
        '
        Me.levelcombo1.FormattingEnabled = True
        Me.levelcombo1.Items.AddRange(New Object() {"", "=", ">", "<"})
        Me.levelcombo1.Location = New System.Drawing.Point(94, 154)
        Me.levelcombo1.Name = "levelcombo1"
        Me.levelcombo1.Size = New System.Drawing.Size(63, 21)
        Me.levelcombo1.TabIndex = 268
        Me.levelcombo1.Text = "Select"
        '
        'racecombo
        '
        Me.racecombo.FormattingEnabled = True
        Me.racecombo.Location = New System.Drawing.Point(94, 97)
        Me.racecombo.Name = "racecombo"
        Me.racecombo.Size = New System.Drawing.Size(128, 21)
        Me.racecombo.TabIndex = 273
        '
        'classcombo
        '
        Me.classcombo.FormattingEnabled = True
        Me.classcombo.Location = New System.Drawing.Point(94, 124)
        Me.classcombo.Name = "classcombo"
        Me.classcombo.Size = New System.Drawing.Size(128, 21)
        Me.classcombo.TabIndex = 274
        '
        'Filter_characters
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(566, 273)
        Me.Controls.Add(Me.classcombo)
        Me.Controls.Add(Me.racecombo)
        Me.Controls.Add(Me.levelcheck)
        Me.Controls.Add(Me.leveltxtbox2)
        Me.Controls.Add(Me.levelcombo2)
        Me.Controls.Add(Me.leveltxtbox1)
        Me.Controls.Add(Me.levelcombo1)
        Me.Controls.Add(Me.accidcheck)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.accidcombo1)
        Me.Controls.Add(Me.classcheck)
        Me.Controls.Add(Me.racecheck)
        Me.Controls.Add(Me.namecheck)
        Me.Controls.Add(Me.idcheck)
        Me.Controls.Add(Me.nametxtbox1)
        Me.Controls.Add(Me.namecombo1)
        Me.Controls.Add(Me.idtxtbox2)
        Me.Controls.Add(Me.idcombo2)
        Me.Controls.Add(Me.idtxtbox1)
        Me.Controls.Add(Me.idcombo1)
        Me.Controls.Add(Me.savelogin_bt)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Filter_characters"
        Me.Text = "Filter_characters"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents savelogin_bt As System.Windows.Forms.Button
    Friend WithEvents classcheck As System.Windows.Forms.CheckBox
    Friend WithEvents racecheck As System.Windows.Forms.CheckBox
    Friend WithEvents namecheck As System.Windows.Forms.CheckBox
    Friend WithEvents idcheck As System.Windows.Forms.CheckBox
    Friend WithEvents nametxtbox1 As System.Windows.Forms.TextBox
    Friend WithEvents namecombo1 As System.Windows.Forms.ComboBox
    Friend WithEvents idtxtbox2 As System.Windows.Forms.TextBox
    Friend WithEvents idcombo2 As System.Windows.Forms.ComboBox
    Friend WithEvents idtxtbox1 As System.Windows.Forms.TextBox
    Friend WithEvents idcombo1 As System.Windows.Forms.ComboBox
    Friend WithEvents accidcheck As System.Windows.Forms.CheckBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents accidcombo1 As System.Windows.Forms.ComboBox
    Friend WithEvents levelcheck As System.Windows.Forms.CheckBox
    Friend WithEvents leveltxtbox2 As System.Windows.Forms.TextBox
    Friend WithEvents levelcombo2 As System.Windows.Forms.ComboBox
    Friend WithEvents leveltxtbox1 As System.Windows.Forms.TextBox
    Friend WithEvents levelcombo1 As System.Windows.Forms.ComboBox
    Friend WithEvents racecombo As System.Windows.Forms.ComboBox
    Friend WithEvents classcombo As System.Windows.Forms.ComboBox
End Class
