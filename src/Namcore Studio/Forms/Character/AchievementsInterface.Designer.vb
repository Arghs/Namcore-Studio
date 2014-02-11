Imports NamCore_Studio.Forms.Extension

Namespace Forms.Character
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class AchievementsInterface
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
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AchievementsInterface))
            Me.AVLayoutPanel = New System.Windows.Forms.FlowLayoutPanel()
            Me.cat_id_92_bt = New System.Windows.Forms.Button()
            Me.cat_id_96_bt = New System.Windows.Forms.Button()
            Me.cat_id_97_bt = New System.Windows.Forms.Button()
            Me.cat_id_95_bt = New System.Windows.Forms.Button()
            Me.cat_id_168_bt = New System.Windows.Forms.Button()
            Me.cat_id_169_bt = New System.Windows.Forms.Button()
            Me.cat_id_201_bt = New System.Windows.Forms.Button()
            Me.cat_id_15165_bt = New System.Windows.Forms.Button()
            Me.cat_id_155_bt = New System.Windows.Forms.Button()
            Me.cat_id_15219_bt = New System.Windows.Forms.Button()
            Me.cat_id_81_bt = New System.Windows.Forms.Button()
            Me.addpanel = New System.Windows.Forms.Panel()
            Me.add_pic = New System.Windows.Forms.PictureBox()
            Me.referencePanel = New System.Windows.Forms.Panel()
            Me.reference_delete_pic = New System.Windows.Forms.PictureBox()
            Me.reference_date_lbl = New System.Windows.Forms.Label()
            Me.reference_description_lbl = New System.Windows.Forms.Label()
            Me.reference_subcat_lbl = New System.Windows.Forms.Label()
            Me.reference_icon_pic = New System.Windows.Forms.PictureBox()
            Me.reference_name_lbl = New System.Windows.Forms.Label()
            Me.add_bt = New System.Windows.Forms.Button()
            Me.browse_tb = New System.Windows.Forms.TextBox()
            Me.subcat_combo = New System.Windows.Forms.ComboBox()
            Me.callbacktimer = New System.Windows.Forms.Timer(Me.components)
            Me.waitpanel = New System.Windows.Forms.Panel()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.search_bt = New System.Windows.Forms.Button()
            Me.addpanel.SuspendLayout()
            CType(Me.add_pic, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.referencePanel.SuspendLayout()
            CType(Me.reference_delete_pic, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.reference_icon_pic, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.waitpanel.SuspendLayout()
            Me.SuspendLayout()
            '
            'AVLayoutPanel
            '
            Me.AVLayoutPanel.AutoScroll = True
            Me.AVLayoutPanel.BackColor = System.Drawing.Color.Transparent
            Me.AVLayoutPanel.Location = New System.Drawing.Point(161, 86)
            Me.AVLayoutPanel.Name = "AVLayoutPanel"
            Me.AVLayoutPanel.Size = New System.Drawing.Size(850, 434)
            Me.AVLayoutPanel.TabIndex = 5
            '
            'cat_id_92_bt
            '
            Me.cat_id_92_bt.BackColor = System.Drawing.Color.DimGray
            Me.cat_id_92_bt.Cursor = System.Windows.Forms.Cursors.Hand
            Me.cat_id_92_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.cat_id_92_bt.ForeColor = System.Drawing.Color.Black
            Me.cat_id_92_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.cat_id_92_bt.Location = New System.Drawing.Point(12, 86)
            Me.cat_id_92_bt.Name = "cat_id_92_bt"
            Me.cat_id_92_bt.Size = New System.Drawing.Size(143, 34)
            Me.cat_id_92_bt.TabIndex = 165
            Me.cat_id_92_bt.Text = "General"
            Me.cat_id_92_bt.UseVisualStyleBackColor = False
            '
            'cat_id_96_bt
            '
            Me.cat_id_96_bt.BackColor = System.Drawing.Color.DimGray
            Me.cat_id_96_bt.Cursor = System.Windows.Forms.Cursors.Hand
            Me.cat_id_96_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.cat_id_96_bt.ForeColor = System.Drawing.Color.Black
            Me.cat_id_96_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.cat_id_96_bt.Location = New System.Drawing.Point(12, 126)
            Me.cat_id_96_bt.Name = "cat_id_96_bt"
            Me.cat_id_96_bt.Size = New System.Drawing.Size(143, 34)
            Me.cat_id_96_bt.TabIndex = 166
            Me.cat_id_96_bt.Text = "Quests"
            Me.cat_id_96_bt.UseVisualStyleBackColor = False
            '
            'cat_id_97_bt
            '
            Me.cat_id_97_bt.BackColor = System.Drawing.Color.DimGray
            Me.cat_id_97_bt.Cursor = System.Windows.Forms.Cursors.Hand
            Me.cat_id_97_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.cat_id_97_bt.ForeColor = System.Drawing.Color.Black
            Me.cat_id_97_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.cat_id_97_bt.Location = New System.Drawing.Point(12, 166)
            Me.cat_id_97_bt.Name = "cat_id_97_bt"
            Me.cat_id_97_bt.Size = New System.Drawing.Size(143, 34)
            Me.cat_id_97_bt.TabIndex = 167
            Me.cat_id_97_bt.Text = "Exploration"
            Me.cat_id_97_bt.UseVisualStyleBackColor = False
            '
            'cat_id_95_bt
            '
            Me.cat_id_95_bt.BackColor = System.Drawing.Color.DimGray
            Me.cat_id_95_bt.Cursor = System.Windows.Forms.Cursors.Hand
            Me.cat_id_95_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.cat_id_95_bt.ForeColor = System.Drawing.Color.Black
            Me.cat_id_95_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.cat_id_95_bt.Location = New System.Drawing.Point(12, 206)
            Me.cat_id_95_bt.Name = "cat_id_95_bt"
            Me.cat_id_95_bt.Size = New System.Drawing.Size(143, 34)
            Me.cat_id_95_bt.TabIndex = 168
            Me.cat_id_95_bt.Text = "Player vs. Player"
            Me.cat_id_95_bt.UseVisualStyleBackColor = False
            '
            'cat_id_168_bt
            '
            Me.cat_id_168_bt.BackColor = System.Drawing.Color.DimGray
            Me.cat_id_168_bt.Cursor = System.Windows.Forms.Cursors.Hand
            Me.cat_id_168_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.cat_id_168_bt.ForeColor = System.Drawing.Color.Black
            Me.cat_id_168_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.cat_id_168_bt.Location = New System.Drawing.Point(12, 246)
            Me.cat_id_168_bt.Name = "cat_id_168_bt"
            Me.cat_id_168_bt.Size = New System.Drawing.Size(143, 34)
            Me.cat_id_168_bt.TabIndex = 169
            Me.cat_id_168_bt.Text = "Dungeons and Raids"
            Me.cat_id_168_bt.UseVisualStyleBackColor = False
            '
            'cat_id_169_bt
            '
            Me.cat_id_169_bt.BackColor = System.Drawing.Color.DimGray
            Me.cat_id_169_bt.Cursor = System.Windows.Forms.Cursors.Hand
            Me.cat_id_169_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.cat_id_169_bt.ForeColor = System.Drawing.Color.Black
            Me.cat_id_169_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.cat_id_169_bt.Location = New System.Drawing.Point(12, 286)
            Me.cat_id_169_bt.Name = "cat_id_169_bt"
            Me.cat_id_169_bt.Size = New System.Drawing.Size(143, 34)
            Me.cat_id_169_bt.TabIndex = 170
            Me.cat_id_169_bt.Text = "Professions"
            Me.cat_id_169_bt.UseVisualStyleBackColor = False
            '
            'cat_id_201_bt
            '
            Me.cat_id_201_bt.BackColor = System.Drawing.Color.DimGray
            Me.cat_id_201_bt.Cursor = System.Windows.Forms.Cursors.Hand
            Me.cat_id_201_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.cat_id_201_bt.ForeColor = System.Drawing.Color.Black
            Me.cat_id_201_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.cat_id_201_bt.Location = New System.Drawing.Point(12, 326)
            Me.cat_id_201_bt.Name = "cat_id_201_bt"
            Me.cat_id_201_bt.Size = New System.Drawing.Size(143, 34)
            Me.cat_id_201_bt.TabIndex = 171
            Me.cat_id_201_bt.Text = "Reputation"
            Me.cat_id_201_bt.UseVisualStyleBackColor = False
            '
            'cat_id_15165_bt
            '
            Me.cat_id_15165_bt.BackColor = System.Drawing.Color.DimGray
            Me.cat_id_15165_bt.Cursor = System.Windows.Forms.Cursors.Hand
            Me.cat_id_15165_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.cat_id_15165_bt.ForeColor = System.Drawing.Color.Black
            Me.cat_id_15165_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.cat_id_15165_bt.Location = New System.Drawing.Point(12, 366)
            Me.cat_id_15165_bt.Name = "cat_id_15165_bt"
            Me.cat_id_15165_bt.Size = New System.Drawing.Size(143, 34)
            Me.cat_id_15165_bt.TabIndex = 172
            Me.cat_id_15165_bt.Text = "Scenarios"
            Me.cat_id_15165_bt.UseVisualStyleBackColor = False
            '
            'cat_id_155_bt
            '
            Me.cat_id_155_bt.BackColor = System.Drawing.Color.DimGray
            Me.cat_id_155_bt.Cursor = System.Windows.Forms.Cursors.Hand
            Me.cat_id_155_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.cat_id_155_bt.ForeColor = System.Drawing.Color.Black
            Me.cat_id_155_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.cat_id_155_bt.Location = New System.Drawing.Point(12, 406)
            Me.cat_id_155_bt.Name = "cat_id_155_bt"
            Me.cat_id_155_bt.Size = New System.Drawing.Size(143, 34)
            Me.cat_id_155_bt.TabIndex = 173
            Me.cat_id_155_bt.Text = "World Events"
            Me.cat_id_155_bt.UseVisualStyleBackColor = False
            '
            'cat_id_15219_bt
            '
            Me.cat_id_15219_bt.BackColor = System.Drawing.Color.DimGray
            Me.cat_id_15219_bt.Cursor = System.Windows.Forms.Cursors.Hand
            Me.cat_id_15219_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.cat_id_15219_bt.ForeColor = System.Drawing.Color.Black
            Me.cat_id_15219_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.cat_id_15219_bt.Location = New System.Drawing.Point(12, 446)
            Me.cat_id_15219_bt.Name = "cat_id_15219_bt"
            Me.cat_id_15219_bt.Size = New System.Drawing.Size(143, 34)
            Me.cat_id_15219_bt.TabIndex = 174
            Me.cat_id_15219_bt.Text = "Pet Battles"
            Me.cat_id_15219_bt.UseVisualStyleBackColor = False
            '
            'cat_id_81_bt
            '
            Me.cat_id_81_bt.BackColor = System.Drawing.Color.DimGray
            Me.cat_id_81_bt.Cursor = System.Windows.Forms.Cursors.Hand
            Me.cat_id_81_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.cat_id_81_bt.ForeColor = System.Drawing.Color.Black
            Me.cat_id_81_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.cat_id_81_bt.Location = New System.Drawing.Point(12, 486)
            Me.cat_id_81_bt.Name = "cat_id_81_bt"
            Me.cat_id_81_bt.Size = New System.Drawing.Size(143, 34)
            Me.cat_id_81_bt.TabIndex = 175
            Me.cat_id_81_bt.Text = "Feats of Strength"
            Me.cat_id_81_bt.UseVisualStyleBackColor = False
            '
            'addpanel
            '
            Me.addpanel.Controls.Add(Me.add_pic)
            Me.addpanel.Location = New System.Drawing.Point(1122, 382)
            Me.addpanel.Name = "addpanel"
            Me.addpanel.Size = New System.Drawing.Size(827, 106)
            Me.addpanel.TabIndex = 177
            '
            'add_pic
            '
            Me.add_pic.Cursor = System.Windows.Forms.Cursors.Hand
            Me.add_pic.Dock = System.Windows.Forms.DockStyle.Fill
            Me.add_pic.Image = Global.NamCore_Studio.My.Resources.Resources.addrep1
            Me.add_pic.Location = New System.Drawing.Point(0, 0)
            Me.add_pic.Name = "add_pic"
            Me.add_pic.Size = New System.Drawing.Size(827, 106)
            Me.add_pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.add_pic.TabIndex = 0
            Me.add_pic.TabStop = False
            '
            'referencePanel
            '
            Me.referencePanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(190, Byte), Integer))
            Me.referencePanel.Controls.Add(Me.reference_delete_pic)
            Me.referencePanel.Controls.Add(Me.reference_date_lbl)
            Me.referencePanel.Controls.Add(Me.reference_description_lbl)
            Me.referencePanel.Controls.Add(Me.reference_subcat_lbl)
            Me.referencePanel.Controls.Add(Me.reference_icon_pic)
            Me.referencePanel.Controls.Add(Me.reference_name_lbl)
            Me.referencePanel.Location = New System.Drawing.Point(1122, 517)
            Me.referencePanel.Name = "referencePanel"
            Me.referencePanel.Size = New System.Drawing.Size(827, 106)
            Me.referencePanel.TabIndex = 176
            '
            'reference_delete_pic
            '
            Me.reference_delete_pic.Image = Global.NamCore_Studio.My.Resources.Resources.trash__delete__16x16
            Me.reference_delete_pic.Location = New System.Drawing.Point(806, 2)
            Me.reference_delete_pic.Name = "reference_delete_pic"
            Me.reference_delete_pic.Size = New System.Drawing.Size(18, 18)
            Me.reference_delete_pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.reference_delete_pic.TabIndex = 6
            Me.reference_delete_pic.TabStop = False
            '
            'reference_date_lbl
            '
            Me.reference_date_lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.reference_date_lbl.Location = New System.Drawing.Point(715, 80)
            Me.reference_date_lbl.Name = "reference_date_lbl"
            Me.reference_date_lbl.RightToLeft = System.Windows.Forms.RightToLeft.Yes
            Me.reference_date_lbl.Size = New System.Drawing.Size(108, 23)
            Me.reference_date_lbl.TabIndex = 5
            Me.reference_date_lbl.Text = "15.08.2013"
            '
            'reference_description_lbl
            '
            Me.reference_description_lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.reference_description_lbl.Location = New System.Drawing.Point(78, 35)
            Me.reference_description_lbl.Name = "reference_description_lbl"
            Me.reference_description_lbl.Size = New System.Drawing.Size(647, 56)
            Me.reference_description_lbl.TabIndex = 4
            Me.reference_description_lbl.Text = "Gewinnt einen Wurf für Gier bei einem Gegenstand überragender Qualität oder einem" & _
        " besseren Gegenstand mit einer höheren Stufe als 185, indem Ihr eine 100 würfelt" & _
        "."
            '
            'reference_subcat_lbl
            '
            Me.reference_subcat_lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.reference_subcat_lbl.Location = New System.Drawing.Point(600, 11)
            Me.reference_subcat_lbl.Name = "reference_subcat_lbl"
            Me.reference_subcat_lbl.RightToLeft = System.Windows.Forms.RightToLeft.Yes
            Me.reference_subcat_lbl.Size = New System.Drawing.Size(201, 23)
            Me.reference_subcat_lbl.TabIndex = 3
            Me.reference_subcat_lbl.Text = "Subcategory"
            '
            'reference_icon_pic
            '
            Me.reference_icon_pic.Cursor = System.Windows.Forms.Cursors.Hand
            Me.reference_icon_pic.Image = Global.NamCore_Studio.My.Resources.Resources.INV_Misc_QuestionMark
            Me.reference_icon_pic.Location = New System.Drawing.Point(16, 35)
            Me.reference_icon_pic.Name = "reference_icon_pic"
            Me.reference_icon_pic.Size = New System.Drawing.Size(56, 56)
            Me.reference_icon_pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.reference_icon_pic.TabIndex = 2
            Me.reference_icon_pic.TabStop = False
            '
            'reference_name_lbl
            '
            Me.reference_name_lbl.AutoSize = True
            Me.reference_name_lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.reference_name_lbl.Location = New System.Drawing.Point(13, 11)
            Me.reference_name_lbl.Name = "reference_name_lbl"
            Me.reference_name_lbl.Size = New System.Drawing.Size(55, 16)
            Me.reference_name_lbl.TabIndex = 1
            Me.reference_name_lbl.Text = "Label1"
            '
            'add_bt
            '
            Me.add_bt.BackColor = System.Drawing.Color.DimGray
            Me.add_bt.Cursor = System.Windows.Forms.Cursors.Hand
            Me.add_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.add_bt.ForeColor = System.Drawing.Color.Black
            Me.add_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.add_bt.Location = New System.Drawing.Point(893, 51)
            Me.add_bt.Name = "add_bt"
            Me.add_bt.Size = New System.Drawing.Size(118, 29)
            Me.add_bt.TabIndex = 225
            Me.add_bt.Text = "Add new"
            Me.add_bt.UseVisualStyleBackColor = False
            '
            'browse_tb
            '
            Me.browse_tb.ForeColor = System.Drawing.SystemColors.WindowFrame
            Me.browse_tb.Location = New System.Drawing.Point(310, 57)
            Me.browse_tb.Name = "browse_tb"
            Me.browse_tb.Size = New System.Drawing.Size(280, 20)
            Me.browse_tb.TabIndex = 226
            Me.browse_tb.Text = "Enter achievement name or id"
            '
            'subcat_combo
            '
            Me.subcat_combo.FormattingEnabled = True
            Me.subcat_combo.Location = New System.Drawing.Point(766, 56)
            Me.subcat_combo.Name = "subcat_combo"
            Me.subcat_combo.Size = New System.Drawing.Size(121, 21)
            Me.subcat_combo.TabIndex = 227
            Me.subcat_combo.Text = "Pick category"
            '
            'callbacktimer
            '
            '
            'waitpanel
            '
            Me.waitpanel.BackColor = System.Drawing.SystemColors.ControlDarkDark
            Me.waitpanel.Controls.Add(Me.Label1)
            Me.waitpanel.Location = New System.Drawing.Point(1122, 210)
            Me.waitpanel.Name = "waitpanel"
            Me.waitpanel.Size = New System.Drawing.Size(413, 118)
            Me.waitpanel.TabIndex = 228
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(31, 48)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(339, 24)
            Me.Label1.TabIndex = 0
            Me.Label1.Text = "Pick category to load achievements"
            '
            'search_bt
            '
            Me.search_bt.BackColor = System.Drawing.Color.DimGray
            Me.search_bt.Cursor = System.Windows.Forms.Cursors.Hand
            Me.search_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.search_bt.ForeColor = System.Drawing.Color.Black
            Me.search_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.search_bt.Location = New System.Drawing.Point(596, 51)
            Me.search_bt.Name = "search_bt"
            Me.search_bt.Size = New System.Drawing.Size(118, 29)
            Me.search_bt.TabIndex = 229
            Me.search_bt.Text = "Search"
            Me.search_bt.UseVisualStyleBackColor = False
            '
            'AchievementsInterface
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackgroundImage = Global.NamCore_Studio.My.Resources.Resources.HUD_bg
            Me.ClientSize = New System.Drawing.Size(1023, 533)
            Me.Controls.Add(Me.search_bt)
            Me.Controls.Add(Me.waitpanel)
            Me.Controls.Add(Me.subcat_combo)
            Me.Controls.Add(Me.browse_tb)
            Me.Controls.Add(Me.add_bt)
            Me.Controls.Add(Me.referencePanel)
            Me.Controls.Add(Me.addpanel)
            Me.Controls.Add(Me.cat_id_81_bt)
            Me.Controls.Add(Me.cat_id_15219_bt)
            Me.Controls.Add(Me.cat_id_155_bt)
            Me.Controls.Add(Me.cat_id_15165_bt)
            Me.Controls.Add(Me.cat_id_201_bt)
            Me.Controls.Add(Me.cat_id_169_bt)
            Me.Controls.Add(Me.cat_id_168_bt)
            Me.Controls.Add(Me.cat_id_95_bt)
            Me.Controls.Add(Me.cat_id_97_bt)
            Me.Controls.Add(Me.cat_id_96_bt)
            Me.Controls.Add(Me.cat_id_92_bt)
            Me.Controls.Add(Me.AVLayoutPanel)
            Me.DoubleBuffered = True
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "AchievementsInterface"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Achievements_interface"
            Me.Controls.SetChildIndex(Me.AVLayoutPanel, 0)
            Me.Controls.SetChildIndex(Me.cat_id_92_bt, 0)
            Me.Controls.SetChildIndex(Me.cat_id_96_bt, 0)
            Me.Controls.SetChildIndex(Me.cat_id_97_bt, 0)
            Me.Controls.SetChildIndex(Me.cat_id_95_bt, 0)
            Me.Controls.SetChildIndex(Me.cat_id_168_bt, 0)
            Me.Controls.SetChildIndex(Me.cat_id_169_bt, 0)
            Me.Controls.SetChildIndex(Me.cat_id_201_bt, 0)
            Me.Controls.SetChildIndex(Me.cat_id_15165_bt, 0)
            Me.Controls.SetChildIndex(Me.cat_id_155_bt, 0)
            Me.Controls.SetChildIndex(Me.cat_id_15219_bt, 0)
            Me.Controls.SetChildIndex(Me.cat_id_81_bt, 0)
            Me.Controls.SetChildIndex(Me.addpanel, 0)
            Me.Controls.SetChildIndex(Me.referencePanel, 0)
            Me.Controls.SetChildIndex(Me.add_bt, 0)
            Me.Controls.SetChildIndex(Me.browse_tb, 0)
            Me.Controls.SetChildIndex(Me.subcat_combo, 0)
            Me.Controls.SetChildIndex(Me.waitpanel, 0)
            Me.Controls.SetChildIndex(Me.search_bt, 0)
            Me.addpanel.ResumeLayout(False)
            CType(Me.add_pic, System.ComponentModel.ISupportInitialize).EndInit()
            Me.referencePanel.ResumeLayout(False)
            Me.referencePanel.PerformLayout()
            CType(Me.reference_delete_pic, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.reference_icon_pic, System.ComponentModel.ISupportInitialize).EndInit()
            Me.waitpanel.ResumeLayout(False)
            Me.waitpanel.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents AVLayoutPanel As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents cat_id_92_bt As System.Windows.Forms.Button
        Friend WithEvents cat_id_96_bt As System.Windows.Forms.Button
        Friend WithEvents cat_id_97_bt As System.Windows.Forms.Button
        Friend WithEvents cat_id_95_bt As System.Windows.Forms.Button
        Friend WithEvents cat_id_168_bt As System.Windows.Forms.Button
        Friend WithEvents cat_id_169_bt As System.Windows.Forms.Button
        Friend WithEvents cat_id_201_bt As System.Windows.Forms.Button
        Friend WithEvents cat_id_15165_bt As System.Windows.Forms.Button
        Friend WithEvents cat_id_155_bt As System.Windows.Forms.Button
        Friend WithEvents cat_id_15219_bt As System.Windows.Forms.Button
        Friend WithEvents cat_id_81_bt As System.Windows.Forms.Button
        Friend WithEvents addpanel As System.Windows.Forms.Panel
        Friend WithEvents referencePanel As System.Windows.Forms.Panel
        Friend WithEvents reference_name_lbl As System.Windows.Forms.Label
        Friend WithEvents reference_delete_pic As System.Windows.Forms.PictureBox
        Friend WithEvents reference_date_lbl As System.Windows.Forms.Label
        Friend WithEvents reference_description_lbl As System.Windows.Forms.Label
        Friend WithEvents reference_subcat_lbl As System.Windows.Forms.Label
        Friend WithEvents reference_icon_pic As System.Windows.Forms.PictureBox
        Friend WithEvents add_pic As System.Windows.Forms.PictureBox
        Friend WithEvents add_bt As System.Windows.Forms.Button
        Friend WithEvents browse_tb As System.Windows.Forms.TextBox
        Friend WithEvents subcat_combo As System.Windows.Forms.ComboBox
        Friend WithEvents callbacktimer As System.Windows.Forms.Timer
        Friend WithEvents waitpanel As System.Windows.Forms.Panel
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents search_bt As System.Windows.Forms.Button
    End Class
End Namespace