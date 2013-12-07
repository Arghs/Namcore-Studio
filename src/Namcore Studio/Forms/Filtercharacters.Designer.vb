Namespace Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FilterCharacters
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FilterCharacters))
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
            Me.filter_label = New System.Windows.Forms.Label()
            Me.ApplyFilter = New System.Windows.Forms.Button()
            Me.close_bt = New System.Windows.Forms.Button()
            Me.SuspendLayout()
            '
            'classcheck
            '
            Me.classcheck.AutoSize = True
            Me.classcheck.BackColor = System.Drawing.Color.Transparent
            Me.classcheck.ForeColor = System.Drawing.Color.Black
            Me.classcheck.Location = New System.Drawing.Point(8, 121)
            Me.classcheck.Name = "classcheck"
            Me.classcheck.Size = New System.Drawing.Size(51, 17)
            Me.classcheck.TabIndex = 259
            Me.classcheck.Text = "Class"
            Me.classcheck.UseVisualStyleBackColor = False
            '
            'racecheck
            '
            Me.racecheck.AutoSize = True
            Me.racecheck.BackColor = System.Drawing.Color.Transparent
            Me.racecheck.ForeColor = System.Drawing.Color.Black
            Me.racecheck.Location = New System.Drawing.Point(8, 93)
            Me.racecheck.Name = "racecheck"
            Me.racecheck.Size = New System.Drawing.Size(52, 17)
            Me.racecheck.TabIndex = 258
            Me.racecheck.Text = "Race"
            Me.racecheck.UseVisualStyleBackColor = False
            '
            'namecheck
            '
            Me.namecheck.AutoSize = True
            Me.namecheck.BackColor = System.Drawing.Color.Transparent
            Me.namecheck.ForeColor = System.Drawing.Color.Black
            Me.namecheck.Location = New System.Drawing.Point(8, 66)
            Me.namecheck.Name = "namecheck"
            Me.namecheck.Size = New System.Drawing.Size(54, 17)
            Me.namecheck.TabIndex = 257
            Me.namecheck.Text = "Name"
            Me.namecheck.UseVisualStyleBackColor = False
            '
            'guidcheck
            '
            Me.guidcheck.AutoSize = True
            Me.guidcheck.BackColor = System.Drawing.Color.Transparent
            Me.guidcheck.ForeColor = System.Drawing.Color.Black
            Me.guidcheck.Location = New System.Drawing.Point(8, 40)
            Me.guidcheck.Name = "guidcheck"
            Me.guidcheck.Size = New System.Drawing.Size(53, 17)
            Me.guidcheck.TabIndex = 256
            Me.guidcheck.Text = "GUID"
            Me.guidcheck.UseVisualStyleBackColor = False
            '
            'nametxtbox1
            '
            Me.nametxtbox1.Location = New System.Drawing.Point(159, 64)
            Me.nametxtbox1.Name = "nametxtbox1"
            Me.nametxtbox1.Size = New System.Drawing.Size(155, 20)
            Me.nametxtbox1.TabIndex = 247
            '
            'namecombo1
            '
            Me.namecombo1.FormattingEnabled = True
            Me.namecombo1.Items.AddRange(New Object() {"", "=", "contains"})
            Me.namecombo1.Location = New System.Drawing.Point(90, 64)
            Me.namecombo1.Name = "namecombo1"
            Me.namecombo1.Size = New System.Drawing.Size(63, 21)
            Me.namecombo1.TabIndex = 246
            Me.namecombo1.Text = "Select"
            '
            'guidtxtbox2
            '
            Me.guidtxtbox2.Location = New System.Drawing.Point(389, 36)
            Me.guidtxtbox2.Name = "guidtxtbox2"
            Me.guidtxtbox2.Size = New System.Drawing.Size(155, 20)
            Me.guidtxtbox2.TabIndex = 245
            '
            'guidcombo2
            '
            Me.guidcombo2.FormattingEnabled = True
            Me.guidcombo2.Items.AddRange(New Object() {"", ">", "<"})
            Me.guidcombo2.Location = New System.Drawing.Point(320, 36)
            Me.guidcombo2.Name = "guidcombo2"
            Me.guidcombo2.Size = New System.Drawing.Size(63, 21)
            Me.guidcombo2.TabIndex = 244
            Me.guidcombo2.Text = "Select"
            '
            'guidtxtbox1
            '
            Me.guidtxtbox1.Location = New System.Drawing.Point(159, 37)
            Me.guidtxtbox1.Name = "guidtxtbox1"
            Me.guidtxtbox1.Size = New System.Drawing.Size(155, 20)
            Me.guidtxtbox1.TabIndex = 243
            '
            'guidcombo1
            '
            Me.guidcombo1.FormattingEnabled = True
            Me.guidcombo1.Items.AddRange(New Object() {"", "=", ">", "<"})
            Me.guidcombo1.Location = New System.Drawing.Point(90, 37)
            Me.guidcombo1.Name = "guidcombo1"
            Me.guidcombo1.Size = New System.Drawing.Size(63, 21)
            Me.guidcombo1.TabIndex = 242
            Me.guidcombo1.Text = "Select"
            '
            'levelcheck
            '
            Me.levelcheck.AutoSize = True
            Me.levelcheck.BackColor = System.Drawing.Color.Transparent
            Me.levelcheck.ForeColor = System.Drawing.Color.Black
            Me.levelcheck.Location = New System.Drawing.Point(8, 151)
            Me.levelcheck.Name = "levelcheck"
            Me.levelcheck.Size = New System.Drawing.Size(52, 17)
            Me.levelcheck.TabIndex = 272
            Me.levelcheck.Text = "Level"
            Me.levelcheck.UseVisualStyleBackColor = False
            '
            'leveltxtbox2
            '
            Me.leveltxtbox2.Location = New System.Drawing.Point(389, 148)
            Me.leveltxtbox2.Name = "leveltxtbox2"
            Me.leveltxtbox2.Size = New System.Drawing.Size(155, 20)
            Me.leveltxtbox2.TabIndex = 271
            '
            'levelcombo2
            '
            Me.levelcombo2.FormattingEnabled = True
            Me.levelcombo2.Items.AddRange(New Object() {"", ">", "<"})
            Me.levelcombo2.Location = New System.Drawing.Point(320, 148)
            Me.levelcombo2.Name = "levelcombo2"
            Me.levelcombo2.Size = New System.Drawing.Size(63, 21)
            Me.levelcombo2.TabIndex = 270
            Me.levelcombo2.Text = "Select"
            '
            'leveltxtbox1
            '
            Me.leveltxtbox1.Location = New System.Drawing.Point(159, 148)
            Me.leveltxtbox1.Name = "leveltxtbox1"
            Me.leveltxtbox1.Size = New System.Drawing.Size(155, 20)
            Me.leveltxtbox1.TabIndex = 269
            '
            'levelcombo1
            '
            Me.levelcombo1.FormattingEnabled = True
            Me.levelcombo1.Items.AddRange(New Object() {"", "=", ">", "<"})
            Me.levelcombo1.Location = New System.Drawing.Point(90, 148)
            Me.levelcombo1.Name = "levelcombo1"
            Me.levelcombo1.Size = New System.Drawing.Size(63, 21)
            Me.levelcombo1.TabIndex = 268
            Me.levelcombo1.Text = "Select"
            '
            'racecombo
            '
            Me.racecombo.FormattingEnabled = True
            Me.racecombo.Items.AddRange(New Object() {"Human", "Orc", "Dwarf", "Night Elf", "Undead", "Tauren", "Gnome", "Troll", "Goblin", "Blood Elf", "Draenei", "Worgen"})
            Me.racecombo.Location = New System.Drawing.Point(90, 91)
            Me.racecombo.Name = "racecombo"
            Me.racecombo.Size = New System.Drawing.Size(128, 21)
            Me.racecombo.TabIndex = 273
            Me.racecombo.Text = "Select"
            '
            'classcombo
            '
            Me.classcombo.FormattingEnabled = True
            Me.classcombo.Items.AddRange(New Object() {"Warrior", "Paladin", "Hunter", "Rogue", "Priest", "Death Knight", "Shaman", "Mage", "Warlock", "Druid"})
            Me.classcombo.Location = New System.Drawing.Point(90, 119)
            Me.classcombo.Name = "classcombo"
            Me.classcombo.Size = New System.Drawing.Size(128, 21)
            Me.classcombo.TabIndex = 274
            Me.classcombo.Text = "Select"
            '
            'filter_label
            '
            Me.filter_label.AutoSize = True
            Me.filter_label.BackColor = System.Drawing.Color.Transparent
            Me.filter_label.Font = New System.Drawing.Font("Franklin Gothic Medium", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.filter_label.ForeColor = System.Drawing.Color.Black
            Me.filter_label.Location = New System.Drawing.Point(6, 6)
            Me.filter_label.Name = "filter_label"
            Me.filter_label.Size = New System.Drawing.Size(159, 24)
            Me.filter_label.TabIndex = 275
            Me.filter_label.Text = "Filter characters"
            '
            'ApplyFilter
            '
            Me.ApplyFilter.BackColor = System.Drawing.Color.DimGray
            Me.ApplyFilter.Cursor = System.Windows.Forms.Cursors.Hand
            Me.ApplyFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.ApplyFilter.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.ApplyFilter.Location = New System.Drawing.Point(8, 176)
            Me.ApplyFilter.Name = "ApplyFilter"
            Me.ApplyFilter.Size = New System.Drawing.Size(180, 40)
            Me.ApplyFilter.TabIndex = 276
            Me.ApplyFilter.Text = "Apply"
            Me.ApplyFilter.UseVisualStyleBackColor = False
            '
            'close_bt
            '
            Me.close_bt.BackColor = System.Drawing.Color.DimGray
            Me.close_bt.Cursor = System.Windows.Forms.Cursors.Hand
            Me.close_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.close_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.close_bt.Location = New System.Drawing.Point(194, 176)
            Me.close_bt.Name = "close_bt"
            Me.close_bt.Size = New System.Drawing.Size(180, 40)
            Me.close_bt.TabIndex = 277
            Me.close_bt.Text = "Close"
            Me.close_bt.UseVisualStyleBackColor = False
            '
            'FilterCharacters
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackgroundImage = Global.Namcore_Studio.My.Resources.Resources.HUD_bg
            Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.ClientSize = New System.Drawing.Size(566, 221)
            Me.Controls.Add(Me.close_bt)
            Me.Controls.Add(Me.ApplyFilter)
            Me.Controls.Add(Me.filter_label)
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
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FilterCharacters"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Filter_characters"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
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
        Friend WithEvents filter_label As System.Windows.Forms.Label
        Friend WithEvents ApplyFilter As System.Windows.Forms.Button
        Friend WithEvents close_bt As System.Windows.Forms.Button
    End Class
End Namespace