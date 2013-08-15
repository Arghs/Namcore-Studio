<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Achievements_interface
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
        Me.avcompleted_lst = New System.Windows.Forms.ListView()
        Me.avid = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.avname = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.avcat = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.avcatsub = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.avgained = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.avdescription = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.resultcnt_lbl = New System.Windows.Forms.Label()
        Me.nxt100_bt = New System.Windows.Forms.Button()
        Me.prev100_bt = New System.Windows.Forms.Button()
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
        Me.reference_name_lbl = New System.Windows.Forms.Label()
        Me.reference_icon_pic = New System.Windows.Forms.PictureBox()
        Me.reference_subcat_lbl = New System.Windows.Forms.Label()
        Me.reference_description_lbl = New System.Windows.Forms.Label()
        Me.reference_date_lbl = New System.Windows.Forms.Label()
        Me.reference_delete_pic = New System.Windows.Forms.PictureBox()
        Me.addpanel.SuspendLayout()
        CType(Me.add_pic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.referencePanel.SuspendLayout()
        CType(Me.reference_icon_pic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.reference_delete_pic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'avcompleted_lst
        '
        Me.avcompleted_lst.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.avid, Me.avname, Me.avcat, Me.avcatsub, Me.avgained, Me.avdescription})
        Me.avcompleted_lst.FullRowSelect = True
        Me.avcompleted_lst.Location = New System.Drawing.Point(1091, 40)
        Me.avcompleted_lst.Name = "avcompleted_lst"
        Me.avcompleted_lst.Size = New System.Drawing.Size(834, 248)
        Me.avcompleted_lst.TabIndex = 0
        Me.avcompleted_lst.UseCompatibleStateImageBehavior = False
        Me.avcompleted_lst.View = System.Windows.Forms.View.Details
        '
        'avid
        '
        Me.avid.Text = "ID"
        Me.avid.Width = 54
        '
        'avname
        '
        Me.avname.Text = "Name"
        Me.avname.Width = 166
        '
        'avcat
        '
        Me.avcat.Text = "Category"
        Me.avcat.Width = 94
        '
        'avcatsub
        '
        Me.avcatsub.Text = "Subcategory"
        Me.avcatsub.Width = 133
        '
        'avgained
        '
        Me.avgained.Text = "Gain Date"
        Me.avgained.Width = 92
        '
        'avdescription
        '
        Me.avdescription.Text = "Description"
        Me.avdescription.Width = 234
        '
        'resultcnt_lbl
        '
        Me.resultcnt_lbl.AutoSize = True
        Me.resultcnt_lbl.Location = New System.Drawing.Point(21, 40)
        Me.resultcnt_lbl.Name = "resultcnt_lbl"
        Me.resultcnt_lbl.Size = New System.Drawing.Size(39, 13)
        Me.resultcnt_lbl.TabIndex = 2
        Me.resultcnt_lbl.Text = "Label1"
        '
        'nxt100_bt
        '
        Me.nxt100_bt.Enabled = False
        Me.nxt100_bt.Location = New System.Drawing.Point(307, 16)
        Me.nxt100_bt.Name = "nxt100_bt"
        Me.nxt100_bt.Size = New System.Drawing.Size(106, 37)
        Me.nxt100_bt.TabIndex = 3
        Me.nxt100_bt.Text = "Load next 100"
        Me.nxt100_bt.UseVisualStyleBackColor = True
        '
        'prev100_bt
        '
        Me.prev100_bt.Enabled = False
        Me.prev100_bt.Location = New System.Drawing.Point(195, 16)
        Me.prev100_bt.Name = "prev100_bt"
        Me.prev100_bt.Size = New System.Drawing.Size(106, 37)
        Me.prev100_bt.TabIndex = 4
        Me.prev100_bt.Text = "Load previous 100"
        Me.prev100_bt.UseVisualStyleBackColor = True
        '
        'AVLayoutPanel
        '
        Me.AVLayoutPanel.AutoScroll = True
        Me.AVLayoutPanel.Location = New System.Drawing.Point(173, 147)
        Me.AVLayoutPanel.Name = "AVLayoutPanel"
        Me.AVLayoutPanel.Size = New System.Drawing.Size(844, 434)
        Me.AVLayoutPanel.TabIndex = 5
        '
        'cat_id_92_bt
        '
        Me.cat_id_92_bt.BackColor = System.Drawing.Color.DimGray
        Me.cat_id_92_bt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cat_id_92_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cat_id_92_bt.ForeColor = System.Drawing.Color.Black
        Me.cat_id_92_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.cat_id_92_bt.Location = New System.Drawing.Point(24, 147)
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
        Me.cat_id_96_bt.Location = New System.Drawing.Point(24, 187)
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
        Me.cat_id_97_bt.Location = New System.Drawing.Point(24, 227)
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
        Me.cat_id_95_bt.Location = New System.Drawing.Point(24, 267)
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
        Me.cat_id_168_bt.Location = New System.Drawing.Point(24, 307)
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
        Me.cat_id_169_bt.Location = New System.Drawing.Point(24, 347)
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
        Me.cat_id_201_bt.Location = New System.Drawing.Point(24, 387)
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
        Me.cat_id_15165_bt.Location = New System.Drawing.Point(24, 427)
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
        Me.cat_id_155_bt.Location = New System.Drawing.Point(24, 467)
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
        Me.cat_id_15219_bt.Location = New System.Drawing.Point(24, 507)
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
        Me.cat_id_81_bt.Location = New System.Drawing.Point(24, 547)
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
        Me.add_pic.Image = Global.Namcore_Studio.My.Resources.Resources.addrep1
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
        'reference_icon_pic
        '
        Me.reference_icon_pic.Cursor = System.Windows.Forms.Cursors.Hand
        Me.reference_icon_pic.Image = Global.Namcore_Studio.My.Resources.Resources.INV_Misc_QuestionMark
        Me.reference_icon_pic.Location = New System.Drawing.Point(16, 35)
        Me.reference_icon_pic.Name = "reference_icon_pic"
        Me.reference_icon_pic.Size = New System.Drawing.Size(56, 56)
        Me.reference_icon_pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.reference_icon_pic.TabIndex = 2
        Me.reference_icon_pic.TabStop = False
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
        'reference_delete_pic
        '
        Me.reference_delete_pic.Image = Global.Namcore_Studio.My.Resources.Resources.trash__delete__16x16
        Me.reference_delete_pic.Location = New System.Drawing.Point(806, 2)
        Me.reference_delete_pic.Name = "reference_delete_pic"
        Me.reference_delete_pic.Size = New System.Drawing.Size(18, 18)
        Me.reference_delete_pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.reference_delete_pic.TabIndex = 6
        Me.reference_delete_pic.TabStop = False
        '
        'Achievements_interface
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1023, 635)
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
        Me.Controls.Add(Me.prev100_bt)
        Me.Controls.Add(Me.nxt100_bt)
        Me.Controls.Add(Me.resultcnt_lbl)
        Me.Controls.Add(Me.avcompleted_lst)
        Me.Name = "Achievements_interface"
        Me.Text = "Achievements_interface"
        Me.addpanel.ResumeLayout(False)
        CType(Me.add_pic,System.ComponentModel.ISupportInitialize).EndInit
        Me.referencePanel.ResumeLayout(false)
        Me.referencePanel.PerformLayout
        CType(Me.reference_icon_pic,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.reference_delete_pic,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents avcompleted_lst As System.Windows.Forms.ListView
    Friend WithEvents avid As System.Windows.Forms.ColumnHeader
    Friend WithEvents avname As System.Windows.Forms.ColumnHeader
    Friend WithEvents avcat As System.Windows.Forms.ColumnHeader
    Friend WithEvents avgained As System.Windows.Forms.ColumnHeader
    Friend WithEvents avdescription As System.Windows.Forms.ColumnHeader
    Friend WithEvents avcatsub As System.Windows.Forms.ColumnHeader
    Friend WithEvents resultcnt_lbl As System.Windows.Forms.Label
    Friend WithEvents nxt100_bt As System.Windows.Forms.Button
    Friend WithEvents prev100_bt As System.Windows.Forms.Button
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
    Friend WithEvents add_pic As System.Windows.Forms.PictureBox
    Friend WithEvents referencePanel As System.Windows.Forms.Panel
    Friend WithEvents reference_name_lbl As System.Windows.Forms.Label
    Friend WithEvents reference_delete_pic As System.Windows.Forms.PictureBox
    Friend WithEvents reference_date_lbl As System.Windows.Forms.Label
    Friend WithEvents reference_description_lbl As System.Windows.Forms.Label
    Friend WithEvents reference_subcat_lbl As System.Windows.Forms.Label
    Friend WithEvents reference_icon_pic As System.Windows.Forms.PictureBox
End Class
