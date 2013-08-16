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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Filter_characters))
        Me.ApplyFilter = New System.Windows.Forms.Button()
        Me.classcheck = New System.Windows.Forms.CheckBox()
        Me.racecheck = New System.Windows.Forms.CheckBox()
        Me.namecheck = New System.Windows.Forms.CheckBox()
        Me.guidcheck = New System.Windows.Forms.CheckBox()
        Me.nametxtbox1 = New System.Windows.Forms.TextBox()
        Me.namecombo1 = New System.Windows.Forms.ComboBox()
        Me.guidtxtbox2 = New System.Windows.Forms.TextBox()
        Me.guidcombo2 = New System.Windows.Forms.ComboBox()
        Me.guidtxtbox1 = New System.Windows.Forms.TextBox()
        Me.guidcombo1 = New System.Windows.Forms.ComboBox()
        Me.levelcheck = New System.Windows.Forms.CheckBox()
        Me.leveltxtbox2 = New System.Windows.Forms.TextBox()
        Me.levelcombo2 = New System.Windows.Forms.ComboBox()
        Me.leveltxtbox1 = New System.Windows.Forms.TextBox()
        Me.levelcombo1 = New System.Windows.Forms.ComboBox()
        Me.racecombo = New System.Windows.Forms.ComboBox()
        Me.classcombo = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'ApplyFilter
        '
        Me.ApplyFilter.BackColor = System.Drawing.Color.DimGray
        Me.ApplyFilter.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ApplyFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ApplyFilter.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.ApplyFilter.Location = New System.Drawing.Point(197, 210)
        Me.ApplyFilter.Name = "ApplyFilter"
        Me.ApplyFilter.Size = New System.Drawing.Size(121, 33)
        Me.ApplyFilter.TabIndex = 212
        Me.ApplyFilter.Text = "Apply"
        Me.ApplyFilter.UseVisualStyleBackColor = False
        '
        'classcheck
        '
        Me.classcheck.AutoSize = True
        Me.classcheck.Location = New System.Drawing.Point(12, 92)
        Me.classcheck.Name = "classcheck"
        Me.classcheck.Size = New System.Drawing.Size(51, 17)
        Me.classcheck.TabIndex = 259
        Me.classcheck.Text = "Class"
        Me.classcheck.UseVisualStyleBackColor = True
        '
        'racecheck
        '
        Me.racecheck.AutoSize = True
        Me.racecheck.Location = New System.Drawing.Point(12, 65)
        Me.racecheck.Name = "racecheck"
        Me.racecheck.Size = New System.Drawing.Size(52, 17)
        Me.racecheck.TabIndex = 258
        Me.racecheck.Text = "Race"
        Me.racecheck.UseVisualStyleBackColor = True
        '
        'namecheck
        '
        Me.namecheck.AutoSize = True
        Me.namecheck.Location = New System.Drawing.Point(12, 38)
        Me.namecheck.Name = "namecheck"
        Me.namecheck.Size = New System.Drawing.Size(54, 17)
        Me.namecheck.TabIndex = 257
        Me.namecheck.Text = "Name"
        Me.namecheck.UseVisualStyleBackColor = True
        '
        'guidcheck
        '
        Me.guidcheck.AutoSize = True
        Me.guidcheck.Location = New System.Drawing.Point(12, 12)
        Me.guidcheck.Name = "guidcheck"
        Me.guidcheck.Size = New System.Drawing.Size(53, 17)
        Me.guidcheck.TabIndex = 256
        Me.guidcheck.Text = "GUID"
        Me.guidcheck.UseVisualStyleBackColor = True
        '
        'nametxtbox1
        '
        Me.nametxtbox1.Location = New System.Drawing.Point(163, 38)
        Me.nametxtbox1.Name = "nametxtbox1"
        Me.nametxtbox1.Size = New System.Drawing.Size(155, 20)
        Me.nametxtbox1.TabIndex = 247
        '
        'namecombo1
        '
        Me.namecombo1.FormattingEnabled = True
        Me.namecombo1.Items.AddRange(New Object() {"", "=", "contains"})
        Me.namecombo1.Location = New System.Drawing.Point(94, 36)
        Me.namecombo1.Name = "namecombo1"
        Me.namecombo1.Size = New System.Drawing.Size(63, 21)
        Me.namecombo1.TabIndex = 246
        Me.namecombo1.Text = "Select"
        '
        'guidtxtbox2
        '
        Me.guidtxtbox2.Location = New System.Drawing.Point(393, 10)
        Me.guidtxtbox2.Name = "guidtxtbox2"
        Me.guidtxtbox2.Size = New System.Drawing.Size(155, 20)
        Me.guidtxtbox2.TabIndex = 245
        '
        'guidcombo2
        '
        Me.guidcombo2.FormattingEnabled = True
        Me.guidcombo2.Items.AddRange(New Object() {"", ">", "<"})
        Me.guidcombo2.Location = New System.Drawing.Point(324, 10)
        Me.guidcombo2.Name = "guidcombo2"
        Me.guidcombo2.Size = New System.Drawing.Size(63, 21)
        Me.guidcombo2.TabIndex = 244
        Me.guidcombo2.Text = "Select"
        '
        'guidtxtbox1
        '
        Me.guidtxtbox1.Location = New System.Drawing.Point(163, 11)
        Me.guidtxtbox1.Name = "guidtxtbox1"
        Me.guidtxtbox1.Size = New System.Drawing.Size(155, 20)
        Me.guidtxtbox1.TabIndex = 243
        '
        'guidcombo1
        '
        Me.guidcombo1.FormattingEnabled = True
        Me.guidcombo1.Items.AddRange(New Object() {"", "=", ">", "<"})
        Me.guidcombo1.Location = New System.Drawing.Point(94, 9)
        Me.guidcombo1.Name = "guidcombo1"
        Me.guidcombo1.Size = New System.Drawing.Size(63, 21)
        Me.guidcombo1.TabIndex = 242
        Me.guidcombo1.Text = "Select"
        '
        'levelcheck
        '
        Me.levelcheck.AutoSize = True
        Me.levelcheck.Location = New System.Drawing.Point(12, 123)
        Me.levelcheck.Name = "levelcheck"
        Me.levelcheck.Size = New System.Drawing.Size(52, 17)
        Me.levelcheck.TabIndex = 272
        Me.levelcheck.Text = "Level"
        Me.levelcheck.UseVisualStyleBackColor = True
        '
        'leveltxtbox2
        '
        Me.leveltxtbox2.Location = New System.Drawing.Point(393, 121)
        Me.leveltxtbox2.Name = "leveltxtbox2"
        Me.leveltxtbox2.Size = New System.Drawing.Size(155, 20)
        Me.leveltxtbox2.TabIndex = 271
        '
        'levelcombo2
        '
        Me.levelcombo2.FormattingEnabled = True
        Me.levelcombo2.Items.AddRange(New Object() {"", ">", "<"})
        Me.levelcombo2.Location = New System.Drawing.Point(324, 121)
        Me.levelcombo2.Name = "levelcombo2"
        Me.levelcombo2.Size = New System.Drawing.Size(63, 21)
        Me.levelcombo2.TabIndex = 270
        Me.levelcombo2.Text = "Select"
        '
        'leveltxtbox1
        '
        Me.leveltxtbox1.Location = New System.Drawing.Point(163, 122)
        Me.leveltxtbox1.Name = "leveltxtbox1"
        Me.leveltxtbox1.Size = New System.Drawing.Size(155, 20)
        Me.leveltxtbox1.TabIndex = 269
        '
        'levelcombo1
        '
        Me.levelcombo1.FormattingEnabled = True
        Me.levelcombo1.Items.AddRange(New Object() {"", "=", ">", "<"})
        Me.levelcombo1.Location = New System.Drawing.Point(94, 120)
        Me.levelcombo1.Name = "levelcombo1"
        Me.levelcombo1.Size = New System.Drawing.Size(63, 21)
        Me.levelcombo1.TabIndex = 268
        Me.levelcombo1.Text = "Select"
        '
        'racecombo
        '
        Me.racecombo.FormattingEnabled = True
        Me.racecombo.Items.AddRange(New Object() {"Human", "Orc", "Dwarf", "Night Elf", "Undead", "Tauren", "Gnome", "Troll", "Goblin", "Blood Elf", "Draenei", "Worgen"})
        Me.racecombo.Location = New System.Drawing.Point(94, 63)
        Me.racecombo.Name = "racecombo"
        Me.racecombo.Size = New System.Drawing.Size(128, 21)
        Me.racecombo.TabIndex = 273
        Me.racecombo.Text = "Select"
        '
        'classcombo
        '
        Me.classcombo.FormattingEnabled = True
        Me.classcombo.Items.AddRange(New Object() {"Warrior", "Paladin", "Hunter", "Rogue", "Priest", "Death Knight", "Shaman", "Mage", "Warlock", "Druid"})
        Me.classcombo.Location = New System.Drawing.Point(94, 90)
        Me.classcombo.Name = "classcombo"
        Me.classcombo.Size = New System.Drawing.Size(128, 21)
        Me.classcombo.TabIndex = 274
        Me.classcombo.Text = "Select"
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
        Me.Controls.Add(Me.classcheck)
        Me.Controls.Add(Me.racecheck)
        Me.Controls.Add(Me.namecheck)
        Me.Controls.Add(Me.guidcheck)
        Me.Controls.Add(Me.nametxtbox1)
        Me.Controls.Add(Me.namecombo1)
        Me.Controls.Add(Me.guidtxtbox2)
        Me.Controls.Add(Me.guidcombo2)
        Me.Controls.Add(Me.guidtxtbox1)
        Me.Controls.Add(Me.guidcombo1)
        Me.Controls.Add(Me.ApplyFilter)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Filter_characters"
        Me.Text = "Filter_characters"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ApplyFilter As System.Windows.Forms.Button
    Friend WithEvents classcheck As System.Windows.Forms.CheckBox
    Friend WithEvents racecheck As System.Windows.Forms.CheckBox
    Friend WithEvents namecheck As System.Windows.Forms.CheckBox
    Friend WithEvents guidcheck As System.Windows.Forms.CheckBox
    Friend WithEvents nametxtbox1 As System.Windows.Forms.TextBox
    Friend WithEvents namecombo1 As System.Windows.Forms.ComboBox
    Friend WithEvents guidtxtbox2 As System.Windows.Forms.TextBox
    Friend WithEvents guidcombo2 As System.Windows.Forms.ComboBox
    Friend WithEvents guidtxtbox1 As System.Windows.Forms.TextBox
    Friend WithEvents guidcombo1 As System.Windows.Forms.ComboBox
    Friend WithEvents levelcheck As System.Windows.Forms.CheckBox
    Friend WithEvents leveltxtbox2 As System.Windows.Forms.TextBox
    Friend WithEvents levelcombo2 As System.Windows.Forms.ComboBox
    Friend WithEvents leveltxtbox1 As System.Windows.Forms.TextBox
    Friend WithEvents levelcombo1 As System.Windows.Forms.ComboBox
    Friend WithEvents racecombo As System.Windows.Forms.ComboBox
    Friend WithEvents classcombo As System.Windows.Forms.ComboBox
End Class
