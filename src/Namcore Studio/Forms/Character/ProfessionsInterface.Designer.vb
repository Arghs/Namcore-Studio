Imports NamCore_Studio.Forms.Extension

Namespace Forms.Character
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class ProfessionsInterface
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ProfessionsInterface))
        Me.prof_lst = New System.Windows.Forms.ListView()
        Me.profid = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.profname = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.profskill = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.profContext = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.LearnToolStrip = New System.Windows.Forms.ToolStripMenuItem()
            Me.LearnAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.search_tb = New System.Windows.Forms.TextBox()
            Me.resultstatus_lbl = New System.Windows.Forms.Label()
            Me.menupanel = New System.Windows.Forms.Panel()
            Me.minprof4_select = New System.Windows.Forms.Panel()
            Me.minprof4_pic = New System.Windows.Forms.PictureBox()
            Me.minprof4_lbl = New System.Windows.Forms.Label()
            Me.minprof3_select = New System.Windows.Forms.Panel()
            Me.minprof3_pic = New System.Windows.Forms.PictureBox()
            Me.minprof3_lbl = New System.Windows.Forms.Label()
            Me.minprof2_select = New System.Windows.Forms.Panel()
            Me.minprof2_pic = New System.Windows.Forms.PictureBox()
            Me.minprof2_lbl = New System.Windows.Forms.Label()
            Me.minprof1_select = New System.Windows.Forms.Panel()
            Me.minprof1_pic = New System.Windows.Forms.PictureBox()
            Me.minprof1_lbl = New System.Windows.Forms.Label()
            Me.mainprof2_select = New System.Windows.Forms.Panel()
            Me.mainprof2_pic = New System.Windows.Forms.PictureBox()
            Me.mainprof2_lbl = New System.Windows.Forms.Label()
            Me.mainprof1_select = New System.Windows.Forms.Panel()
            Me.mainprof1_pic = New System.Windows.Forms.PictureBox()
            Me.mainprof1_lbl = New System.Windows.Forms.Label()
            Me.disp2 = New System.Windows.Forms.Label()
            Me.disp1 = New System.Windows.Forms.Label()
            Me.learned_bt = New System.Windows.Forms.Button()
            Me.nyl_bt = New System.Windows.Forms.Button()
            Me.rank_panel = New System.Windows.Forms.Panel()
            Me.rank_slider = New System.Windows.Forms.TrackBar()
            Me.rank_color_panel = New System.Windows.Forms.Panel()
            Me.rankname_lbl = New System.Windows.Forms.Label()
            Me.progress_lbl = New System.Windows.Forms.Label()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.add_helper_panel = New System.Windows.Forms.Panel()
            Me.apply_bt = New System.Windows.Forms.PictureBox()
            Me.profession_combo = New System.Windows.Forms.ComboBox()
            Me.add_helper_closebox = New System.Windows.Forms.PictureBox()
            Me.profImageList = New System.Windows.Forms.ImageList(Me.components)
            Me.profContext.SuspendLayout()
            Me.menupanel.SuspendLayout()
            Me.minprof4_select.SuspendLayout()
            CType(Me.minprof4_pic, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.minprof3_select.SuspendLayout()
            CType(Me.minprof3_pic, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.minprof2_select.SuspendLayout()
            CType(Me.minprof2_pic, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.minprof1_select.SuspendLayout()
            CType(Me.minprof1_pic, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.mainprof2_select.SuspendLayout()
            CType(Me.mainprof2_pic, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.mainprof1_select.SuspendLayout()
            CType(Me.mainprof1_pic, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.rank_panel.SuspendLayout()
            CType(Me.rank_slider, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.GroupBox1.SuspendLayout()
            Me.add_helper_panel.SuspendLayout()
            CType(Me.apply_bt, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.add_helper_closebox, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'prof_lst
            '
            Me.prof_lst.BackColor = System.Drawing.Color.FromArgb(CType(CType(126, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(156, Byte), Integer))
            Me.prof_lst.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.profid, Me.profname, Me.profskill})
            Me.prof_lst.FullRowSelect = True
            Me.prof_lst.Location = New System.Drawing.Point(222, 148)
            Me.prof_lst.Name = "prof_lst"
            Me.prof_lst.Size = New System.Drawing.Size(650, 422)
            Me.prof_lst.TabIndex = 0
            Me.prof_lst.UseCompatibleStateImageBehavior = False
            Me.prof_lst.View = System.Windows.Forms.View.Details
            '
            'profid
            '
            Me.profid.Text = "Id"
            Me.profid.Width = 44
            '
            'profname
            '
            Me.profname.Text = "Name"
            Me.profname.Width = 531
            '
            'profskill
            '
            Me.profskill.Text = "Skill"
            Me.profskill.Width = 53
            '
            'profContext
            '
            Me.profContext.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LearnToolStrip, Me.LearnAllToolStripMenuItem})
            Me.profContext.Name = "qstContext"
            Me.profContext.Size = New System.Drawing.Size(119, 48)
            '
            'LearnToolStrip
            '
            Me.LearnToolStrip.Name = "LearnToolStrip"
            Me.LearnToolStrip.Size = New System.Drawing.Size(118, 22)
            Me.LearnToolStrip.Text = "Learn"
            '
            'LearnAllToolStripMenuItem
            '
            Me.LearnAllToolStripMenuItem.Name = "LearnAllToolStripMenuItem"
            Me.LearnAllToolStripMenuItem.Size = New System.Drawing.Size(118, 22)
            Me.LearnAllToolStripMenuItem.Text = "Learn all"
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.BackColor = System.Drawing.Color.Transparent
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.ForeColor = System.Drawing.Color.Black
            Me.Label1.Location = New System.Drawing.Point(629, 114)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(234, 16)
            Me.Label1.TabIndex = 228
            Me.Label1.Text = "Right click to open context menu!"
            '
            'search_tb
            '
            Me.search_tb.ForeColor = System.Drawing.SystemColors.WindowFrame
            Me.search_tb.Location = New System.Drawing.Point(7, 16)
            Me.search_tb.Name = "search_tb"
            Me.search_tb.Size = New System.Drawing.Size(139, 20)
            Me.search_tb.TabIndex = 229
            Me.search_tb.Text = "Enter profession id"
            '
            'resultstatus_lbl
            '
            Me.resultstatus_lbl.AutoSize = True
            Me.resultstatus_lbl.BackColor = System.Drawing.Color.Transparent
            Me.resultstatus_lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.resultstatus_lbl.ForeColor = System.Drawing.Color.Black
            Me.resultstatus_lbl.Location = New System.Drawing.Point(156, 17)
            Me.resultstatus_lbl.Name = "resultstatus_lbl"
            Me.resultstatus_lbl.Size = New System.Drawing.Size(78, 16)
            Me.resultstatus_lbl.TabIndex = 230
            Me.resultstatus_lbl.Text = "No results"
            '
            'menupanel
            '
            Me.menupanel.BackColor = System.Drawing.Color.Transparent
            Me.menupanel.BackgroundImage = Global.NamCore_Studio.My.Resources.Resources.profession_bg
            Me.menupanel.Controls.Add(Me.minprof4_select)
            Me.menupanel.Controls.Add(Me.minprof3_select)
            Me.menupanel.Controls.Add(Me.minprof2_select)
            Me.menupanel.Controls.Add(Me.minprof1_select)
            Me.menupanel.Controls.Add(Me.mainprof2_select)
            Me.menupanel.Controls.Add(Me.mainprof1_select)
            Me.menupanel.Controls.Add(Me.disp2)
            Me.menupanel.Controls.Add(Me.disp1)
            Me.menupanel.Location = New System.Drawing.Point(3, 275)
            Me.menupanel.Name = "menupanel"
            Me.menupanel.Size = New System.Drawing.Size(218, 244)
            Me.menupanel.TabIndex = 231
            '
            'minprof4_select
            '
            Me.minprof4_select.Controls.Add(Me.minprof4_pic)
            Me.minprof4_select.Controls.Add(Me.minprof4_lbl)
            Me.minprof4_select.Cursor = System.Windows.Forms.Cursors.Hand
            Me.minprof4_select.Location = New System.Drawing.Point(1, 206)
            Me.minprof4_select.Name = "minprof4_select"
            Me.minprof4_select.Size = New System.Drawing.Size(217, 28)
            Me.minprof4_select.TabIndex = 240
            '
            'minprof4_pic
            '
            Me.minprof4_pic.BackgroundImage = Global.NamCore_Studio.My.Resources.Resources.trade_fishing
            Me.minprof4_pic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.minprof4_pic.Location = New System.Drawing.Point(3, 3)
            Me.minprof4_pic.Name = "minprof4_pic"
            Me.minprof4_pic.Size = New System.Drawing.Size(22, 22)
            Me.minprof4_pic.TabIndex = 234
            Me.minprof4_pic.TabStop = False
            '
            'minprof4_lbl
            '
            Me.minprof4_lbl.AutoSize = True
            Me.minprof4_lbl.BackColor = System.Drawing.Color.Transparent
            Me.minprof4_lbl.Enabled = False
            Me.minprof4_lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.minprof4_lbl.ForeColor = System.Drawing.Color.Black
            Me.minprof4_lbl.Location = New System.Drawing.Point(28, 7)
            Me.minprof4_lbl.Name = "minprof4_lbl"
            Me.minprof4_lbl.Size = New System.Drawing.Size(58, 16)
            Me.minprof4_lbl.TabIndex = 233
            Me.minprof4_lbl.Text = "Fishing"
            '
            'minprof3_select
            '
            Me.minprof3_select.Controls.Add(Me.minprof3_pic)
            Me.minprof3_select.Controls.Add(Me.minprof3_lbl)
            Me.minprof3_select.Cursor = System.Windows.Forms.Cursors.Hand
            Me.minprof3_select.Location = New System.Drawing.Point(1, 177)
            Me.minprof3_select.Name = "minprof3_select"
            Me.minprof3_select.Size = New System.Drawing.Size(217, 28)
            Me.minprof3_select.TabIndex = 239
            '
            'minprof3_pic
            '
            Me.minprof3_pic.BackgroundImage = Global.NamCore_Studio.My.Resources.Resources.spell_holy_sealofsacrifice
            Me.minprof3_pic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.minprof3_pic.Location = New System.Drawing.Point(3, 3)
            Me.minprof3_pic.Name = "minprof3_pic"
            Me.minprof3_pic.Size = New System.Drawing.Size(22, 22)
            Me.minprof3_pic.TabIndex = 234
            Me.minprof3_pic.TabStop = False
            '
            'minprof3_lbl
            '
            Me.minprof3_lbl.AutoSize = True
            Me.minprof3_lbl.BackColor = System.Drawing.Color.Transparent
            Me.minprof3_lbl.Enabled = False
            Me.minprof3_lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.minprof3_lbl.ForeColor = System.Drawing.Color.Black
            Me.minprof3_lbl.Location = New System.Drawing.Point(28, 7)
            Me.minprof3_lbl.Name = "minprof3_lbl"
            Me.minprof3_lbl.Size = New System.Drawing.Size(65, 16)
            Me.minprof3_lbl.TabIndex = 233
            Me.minprof3_lbl.Text = "First Aid"
            '
            'minprof2_select
            '
            Me.minprof2_select.Controls.Add(Me.minprof2_pic)
            Me.minprof2_select.Controls.Add(Me.minprof2_lbl)
            Me.minprof2_select.Cursor = System.Windows.Forms.Cursors.Hand
            Me.minprof2_select.Location = New System.Drawing.Point(1, 148)
            Me.minprof2_select.Name = "minprof2_select"
            Me.minprof2_select.Size = New System.Drawing.Size(217, 28)
            Me.minprof2_select.TabIndex = 238
            '
            'minprof2_pic
            '
            Me.minprof2_pic.BackgroundImage = Global.NamCore_Studio.My.Resources.Resources.inv_misc_food_15
            Me.minprof2_pic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.minprof2_pic.Location = New System.Drawing.Point(3, 3)
            Me.minprof2_pic.Name = "minprof2_pic"
            Me.minprof2_pic.Size = New System.Drawing.Size(22, 22)
            Me.minprof2_pic.TabIndex = 234
            Me.minprof2_pic.TabStop = False
            '
            'minprof2_lbl
            '
            Me.minprof2_lbl.AutoSize = True
            Me.minprof2_lbl.BackColor = System.Drawing.Color.Transparent
            Me.minprof2_lbl.Enabled = False
            Me.minprof2_lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.minprof2_lbl.ForeColor = System.Drawing.Color.Black
            Me.minprof2_lbl.Location = New System.Drawing.Point(28, 7)
            Me.minprof2_lbl.Name = "minprof2_lbl"
            Me.minprof2_lbl.Size = New System.Drawing.Size(65, 16)
            Me.minprof2_lbl.TabIndex = 233
            Me.minprof2_lbl.Text = "Cooking"
            '
            'minprof1_select
            '
            Me.minprof1_select.Controls.Add(Me.minprof1_pic)
            Me.minprof1_select.Controls.Add(Me.minprof1_lbl)
            Me.minprof1_select.Cursor = System.Windows.Forms.Cursors.Hand
            Me.minprof1_select.Location = New System.Drawing.Point(1, 119)
            Me.minprof1_select.Name = "minprof1_select"
            Me.minprof1_select.Size = New System.Drawing.Size(217, 28)
            Me.minprof1_select.TabIndex = 237
            '
            'minprof1_pic
            '
            Me.minprof1_pic.BackgroundImage = Global.NamCore_Studio.My.Resources.Resources.trade_archaeology
            Me.minprof1_pic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.minprof1_pic.Location = New System.Drawing.Point(3, 3)
            Me.minprof1_pic.Name = "minprof1_pic"
            Me.minprof1_pic.Size = New System.Drawing.Size(22, 22)
            Me.minprof1_pic.TabIndex = 234
            Me.minprof1_pic.TabStop = False
            '
            'minprof1_lbl
            '
            Me.minprof1_lbl.AutoSize = True
            Me.minprof1_lbl.BackColor = System.Drawing.Color.Transparent
            Me.minprof1_lbl.Enabled = False
            Me.minprof1_lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.minprof1_lbl.ForeColor = System.Drawing.Color.Black
            Me.minprof1_lbl.Location = New System.Drawing.Point(28, 7)
            Me.minprof1_lbl.Name = "minprof1_lbl"
            Me.minprof1_lbl.Size = New System.Drawing.Size(96, 16)
            Me.minprof1_lbl.TabIndex = 233
            Me.minprof1_lbl.Text = "Archaeology"
            '
            'mainprof2_select
            '
            Me.mainprof2_select.Controls.Add(Me.mainprof2_pic)
            Me.mainprof2_select.Controls.Add(Me.mainprof2_lbl)
            Me.mainprof2_select.Cursor = System.Windows.Forms.Cursors.Hand
            Me.mainprof2_select.Location = New System.Drawing.Point(1, 60)
            Me.mainprof2_select.Name = "mainprof2_select"
            Me.mainprof2_select.Size = New System.Drawing.Size(217, 28)
            Me.mainprof2_select.TabIndex = 236
            '
            'mainprof2_pic
            '
            Me.mainprof2_pic.BackgroundImage = Global.NamCore_Studio.My.Resources.Resources.inv_misc_gem_01
            Me.mainprof2_pic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.mainprof2_pic.Location = New System.Drawing.Point(3, 3)
            Me.mainprof2_pic.Name = "mainprof2_pic"
            Me.mainprof2_pic.Size = New System.Drawing.Size(22, 22)
            Me.mainprof2_pic.TabIndex = 234
            Me.mainprof2_pic.TabStop = False
            '
            'mainprof2_lbl
            '
            Me.mainprof2_lbl.AutoSize = True
            Me.mainprof2_lbl.BackColor = System.Drawing.Color.Transparent
            Me.mainprof2_lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.mainprof2_lbl.ForeColor = System.Drawing.Color.Black
            Me.mainprof2_lbl.Location = New System.Drawing.Point(28, 7)
            Me.mainprof2_lbl.Name = "mainprof2_lbl"
            Me.mainprof2_lbl.Size = New System.Drawing.Size(94, 16)
            Me.mainprof2_lbl.TabIndex = 233
            Me.mainprof2_lbl.Text = "Profession 1"
            '
            'mainprof1_select
            '
            Me.mainprof1_select.Controls.Add(Me.mainprof1_pic)
            Me.mainprof1_select.Controls.Add(Me.mainprof1_lbl)
            Me.mainprof1_select.Cursor = System.Windows.Forms.Cursors.Hand
            Me.mainprof1_select.Location = New System.Drawing.Point(1, 31)
            Me.mainprof1_select.Name = "mainprof1_select"
            Me.mainprof1_select.Size = New System.Drawing.Size(217, 28)
            Me.mainprof1_select.TabIndex = 235
            '
            'mainprof1_pic
            '
            Me.mainprof1_pic.BackgroundImage = Global.NamCore_Studio.My.Resources.Resources.inv_misc_gem_01
            Me.mainprof1_pic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.mainprof1_pic.Location = New System.Drawing.Point(3, 3)
            Me.mainprof1_pic.Name = "mainprof1_pic"
            Me.mainprof1_pic.Size = New System.Drawing.Size(22, 22)
            Me.mainprof1_pic.TabIndex = 234
            Me.mainprof1_pic.TabStop = False
            '
            'mainprof1_lbl
            '
            Me.mainprof1_lbl.AutoSize = True
            Me.mainprof1_lbl.BackColor = System.Drawing.Color.Transparent
            Me.mainprof1_lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.mainprof1_lbl.ForeColor = System.Drawing.Color.Black
            Me.mainprof1_lbl.Location = New System.Drawing.Point(28, 7)
            Me.mainprof1_lbl.Name = "mainprof1_lbl"
            Me.mainprof1_lbl.Size = New System.Drawing.Size(94, 16)
            Me.mainprof1_lbl.TabIndex = 233
            Me.mainprof1_lbl.Text = "Profession 1"
            '
            'disp2
            '
            Me.disp2.AutoSize = True
            Me.disp2.BackColor = System.Drawing.Color.Transparent
            Me.disp2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.disp2.ForeColor = System.Drawing.Color.Black
            Me.disp2.Location = New System.Drawing.Point(1, 95)
            Me.disp2.Name = "disp2"
            Me.disp2.Size = New System.Drawing.Size(141, 20)
            Me.disp2.TabIndex = 232
            Me.disp2.Text = "Secondary Skills"
            '
            'disp1
            '
            Me.disp1.AutoSize = True
            Me.disp1.BackColor = System.Drawing.Color.Transparent
            Me.disp1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.disp1.ForeColor = System.Drawing.Color.Black
            Me.disp1.Location = New System.Drawing.Point(1, 7)
            Me.disp1.Name = "disp1"
            Me.disp1.Size = New System.Drawing.Size(167, 20)
            Me.disp1.TabIndex = 231
            Me.disp1.Text = "Primary Professions"
            '
            'learned_bt
            '
            Me.learned_bt.BackColor = System.Drawing.Color.DimGray
            Me.learned_bt.Cursor = System.Windows.Forms.Cursors.Hand
            Me.learned_bt.Enabled = False
            Me.learned_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.learned_bt.ForeColor = System.Drawing.Color.Black
            Me.learned_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.learned_bt.Location = New System.Drawing.Point(21, 193)
            Me.learned_bt.Name = "learned_bt"
            Me.learned_bt.Size = New System.Drawing.Size(177, 34)
            Me.learned_bt.TabIndex = 232
            Me.learned_bt.Text = "Learned (180)"
            Me.learned_bt.UseVisualStyleBackColor = False
            '
            'nyl_bt
            '
            Me.nyl_bt.BackColor = System.Drawing.Color.DimGray
            Me.nyl_bt.Cursor = System.Windows.Forms.Cursors.Hand
            Me.nyl_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.nyl_bt.ForeColor = System.Drawing.Color.Black
            Me.nyl_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.nyl_bt.Location = New System.Drawing.Point(21, 233)
            Me.nyl_bt.Name = "nyl_bt"
            Me.nyl_bt.Size = New System.Drawing.Size(177, 34)
            Me.nyl_bt.TabIndex = 233
            Me.nyl_bt.Text = "Not Yet Learned (180)"
            Me.nyl_bt.UseVisualStyleBackColor = False
            '
            'rank_panel
            '
            Me.rank_panel.BackColor = System.Drawing.Color.Gray
            Me.rank_panel.Controls.Add(Me.rank_slider)
            Me.rank_panel.Controls.Add(Me.rank_color_panel)
            Me.rank_panel.Location = New System.Drawing.Point(3, 103)
            Me.rank_panel.Name = "rank_panel"
            Me.rank_panel.Size = New System.Drawing.Size(213, 63)
            Me.rank_panel.TabIndex = 234
            Me.rank_panel.Visible = False
            '
            'rank_slider
            '
            Me.rank_slider.BackColor = System.Drawing.Color.FromArgb(CType(CType(126, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(156, Byte), Integer))
            Me.rank_slider.Location = New System.Drawing.Point(-5, 31)
            Me.rank_slider.Maximum = 600
            Me.rank_slider.Name = "rank_slider"
            Me.rank_slider.Size = New System.Drawing.Size(211, 45)
            Me.rank_slider.TabIndex = 235
            Me.rank_slider.TickStyle = System.Windows.Forms.TickStyle.None
            '
            'rank_color_panel
            '
            Me.rank_color_panel.BackColor = System.Drawing.Color.LimeGreen
            Me.rank_color_panel.Location = New System.Drawing.Point(0, 0)
            Me.rank_color_panel.Name = "rank_color_panel"
            Me.rank_color_panel.Size = New System.Drawing.Size(210, 30)
            Me.rank_color_panel.TabIndex = 235
            '
            'rankname_lbl
            '
            Me.rankname_lbl.AutoSize = True
            Me.rankname_lbl.BackColor = System.Drawing.Color.Black
            Me.rankname_lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.rankname_lbl.ForeColor = System.Drawing.Color.White
            Me.rankname_lbl.Location = New System.Drawing.Point(6, 111)
            Me.rankname_lbl.Name = "rankname_lbl"
            Me.rankname_lbl.Size = New System.Drawing.Size(85, 16)
            Me.rankname_lbl.TabIndex = 235
            Me.rankname_lbl.Text = "Zen Master"
            '
            'progress_lbl
            '
            Me.progress_lbl.AutoSize = True
            Me.progress_lbl.BackColor = System.Drawing.SystemColors.WindowText
            Me.progress_lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.progress_lbl.ForeColor = System.Drawing.Color.White
            Me.progress_lbl.Location = New System.Drawing.Point(156, 111)
            Me.progress_lbl.Name = "progress_lbl"
            Me.progress_lbl.Size = New System.Drawing.Size(61, 16)
            Me.progress_lbl.TabIndex = 236
            Me.progress_lbl.Text = "600/600"
            '
            'GroupBox1
            '
            Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
            Me.GroupBox1.Controls.Add(Me.search_tb)
            Me.GroupBox1.Controls.Add(Me.resultstatus_lbl)
            Me.GroupBox1.Location = New System.Drawing.Point(227, 97)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(280, 45)
            Me.GroupBox1.TabIndex = 235
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Browse"
            '
            'add_helper_panel
            '
            Me.add_helper_panel.BackColor = System.Drawing.Color.FromArgb(CType(CType(126, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(156, Byte), Integer))
            Me.add_helper_panel.Controls.Add(Me.apply_bt)
            Me.add_helper_panel.Controls.Add(Me.profession_combo)
            Me.add_helper_panel.Controls.Add(Me.add_helper_closebox)
            Me.add_helper_panel.Location = New System.Drawing.Point(958, 313)
            Me.add_helper_panel.Name = "add_helper_panel"
            Me.add_helper_panel.Size = New System.Drawing.Size(212, 27)
            Me.add_helper_panel.TabIndex = 237
            '
            'apply_bt
            '
            Me.apply_bt.BackgroundImage = Global.NamCore_Studio.My.Resources.Resources.Refresh_icon
            Me.apply_bt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.apply_bt.Cursor = System.Windows.Forms.Cursors.Hand
            Me.apply_bt.Location = New System.Drawing.Point(158, 4)
            Me.apply_bt.Name = "apply_bt"
            Me.apply_bt.Size = New System.Drawing.Size(20, 20)
            Me.apply_bt.TabIndex = 2
            Me.apply_bt.TabStop = False
            '
            'profession_combo
            '
            Me.profession_combo.FormattingEnabled = True
            Me.profession_combo.Location = New System.Drawing.Point(3, 3)
            Me.profession_combo.Name = "profession_combo"
            Me.profession_combo.Size = New System.Drawing.Size(150, 21)
            Me.profession_combo.TabIndex = 1
            '
            'add_helper_closebox
            '
            Me.add_helper_closebox.BackgroundImage = Global.NamCore_Studio.My.Resources.Resources.bt_close
            Me.add_helper_closebox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.add_helper_closebox.Cursor = System.Windows.Forms.Cursors.Hand
            Me.add_helper_closebox.Location = New System.Drawing.Point(191, 1)
            Me.add_helper_closebox.Name = "add_helper_closebox"
            Me.add_helper_closebox.Size = New System.Drawing.Size(20, 20)
            Me.add_helper_closebox.TabIndex = 0
            Me.add_helper_closebox.TabStop = False
            '
            'profImageList
            '
            Me.profImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
            Me.profImageList.ImageSize = New System.Drawing.Size(16, 16)
            Me.profImageList.TransparentColor = System.Drawing.Color.Transparent
            '
            'ProfessionsInterface
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackgroundImage = Global.NamCore_Studio.My.Resources.Resources.HUD_bg
            Me.ClientSize = New System.Drawing.Size(875, 572)
            Me.Controls.Add(Me.add_helper_panel)
            Me.Controls.Add(Me.rankname_lbl)
            Me.Controls.Add(Me.progress_lbl)
            Me.Controls.Add(Me.GroupBox1)
            Me.Controls.Add(Me.rank_panel)
            Me.Controls.Add(Me.nyl_bt)
            Me.Controls.Add(Me.learned_bt)
            Me.Controls.Add(Me.menupanel)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.prof_lst)
            Me.DoubleBuffered = True
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "ProfessionsInterface"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Quests_interface"
            Me.Controls.SetChildIndex(Me.prof_lst, 0)
            Me.Controls.SetChildIndex(Me.Label1, 0)
            Me.Controls.SetChildIndex(Me.menupanel, 0)
            Me.Controls.SetChildIndex(Me.learned_bt, 0)
            Me.Controls.SetChildIndex(Me.nyl_bt, 0)
            Me.Controls.SetChildIndex(Me.rank_panel, 0)
            Me.Controls.SetChildIndex(Me.GroupBox1, 0)
            Me.Controls.SetChildIndex(Me.progress_lbl, 0)
            Me.Controls.SetChildIndex(Me.rankname_lbl, 0)
            Me.Controls.SetChildIndex(Me.add_helper_panel, 0)
            Me.profContext.ResumeLayout(False)
            Me.menupanel.ResumeLayout(False)
            Me.menupanel.PerformLayout()
            Me.minprof4_select.ResumeLayout(False)
            Me.minprof4_select.PerformLayout()
            CType(Me.minprof4_pic, System.ComponentModel.ISupportInitialize).EndInit()
            Me.minprof3_select.ResumeLayout(False)
            Me.minprof3_select.PerformLayout()
            CType(Me.minprof3_pic, System.ComponentModel.ISupportInitialize).EndInit()
            Me.minprof2_select.ResumeLayout(False)
            Me.minprof2_select.PerformLayout()
            CType(Me.minprof2_pic, System.ComponentModel.ISupportInitialize).EndInit()
            Me.minprof1_select.ResumeLayout(False)
            Me.minprof1_select.PerformLayout()
            CType(Me.minprof1_pic, System.ComponentModel.ISupportInitialize).EndInit()
            Me.mainprof2_select.ResumeLayout(False)
            Me.mainprof2_select.PerformLayout()
            CType(Me.mainprof2_pic, System.ComponentModel.ISupportInitialize).EndInit()
            Me.mainprof1_select.ResumeLayout(False)
            Me.mainprof1_select.PerformLayout()
            CType(Me.mainprof1_pic, System.ComponentModel.ISupportInitialize).EndInit()
            Me.rank_panel.ResumeLayout(False)
            Me.rank_panel.PerformLayout()
            CType(Me.rank_slider, System.ComponentModel.ISupportInitialize).EndInit()
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.add_helper_panel.ResumeLayout(False)
            CType(Me.apply_bt, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.add_helper_closebox, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents prof_lst As System.Windows.Forms.ListView
        Friend WithEvents profid As System.Windows.Forms.ColumnHeader
        Friend WithEvents profname As System.Windows.Forms.ColumnHeader
        Friend WithEvents profskill As System.Windows.Forms.ColumnHeader
        Friend WithEvents profContext As System.Windows.Forms.ContextMenuStrip
        Friend WithEvents LearnToolStrip As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents search_tb As System.Windows.Forms.TextBox
        Friend WithEvents resultstatus_lbl As System.Windows.Forms.Label
        Friend WithEvents menupanel As System.Windows.Forms.Panel
        Friend WithEvents disp2 As System.Windows.Forms.Label
        Friend WithEvents disp1 As System.Windows.Forms.Label
        Friend WithEvents minprof4_select As System.Windows.Forms.Panel
        Friend WithEvents minprof4_pic As System.Windows.Forms.PictureBox
        Friend WithEvents minprof4_lbl As System.Windows.Forms.Label
        Friend WithEvents minprof3_select As System.Windows.Forms.Panel
        Friend WithEvents minprof3_pic As System.Windows.Forms.PictureBox
        Friend WithEvents minprof3_lbl As System.Windows.Forms.Label
        Friend WithEvents minprof2_select As System.Windows.Forms.Panel
        Friend WithEvents minprof2_pic As System.Windows.Forms.PictureBox
        Friend WithEvents minprof2_lbl As System.Windows.Forms.Label
        Friend WithEvents minprof1_select As System.Windows.Forms.Panel
        Friend WithEvents minprof1_pic As System.Windows.Forms.PictureBox
        Friend WithEvents minprof1_lbl As System.Windows.Forms.Label
        Friend WithEvents mainprof2_select As System.Windows.Forms.Panel
        Friend WithEvents mainprof2_pic As System.Windows.Forms.PictureBox
        Friend WithEvents mainprof2_lbl As System.Windows.Forms.Label
        Friend WithEvents mainprof1_select As System.Windows.Forms.Panel
        Friend WithEvents mainprof1_pic As System.Windows.Forms.PictureBox
        Friend WithEvents mainprof1_lbl As System.Windows.Forms.Label
        Friend WithEvents learned_bt As System.Windows.Forms.Button
        Friend WithEvents nyl_bt As System.Windows.Forms.Button
        Friend WithEvents rank_panel As System.Windows.Forms.Panel
        Friend WithEvents rank_color_panel As System.Windows.Forms.Panel
        Friend WithEvents rankname_lbl As System.Windows.Forms.Label
        Friend WithEvents rank_slider As System.Windows.Forms.TrackBar
        Friend WithEvents progress_lbl As System.Windows.Forms.Label
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents LearnAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents add_helper_panel As System.Windows.Forms.Panel
        Friend WithEvents add_helper_closebox As System.Windows.Forms.PictureBox
        Friend WithEvents profession_combo As System.Windows.Forms.ComboBox
        Friend WithEvents profImageList As System.Windows.Forms.ImageList
        Friend WithEvents apply_bt As System.Windows.Forms.PictureBox
    End Class
End Namespace