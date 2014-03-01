Namespace Forms

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class LiveView
        Inherits Form
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LiveView))
            Me.connect_bt = New System.Windows.Forms.Button()
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.refreshdb = New System.Windows.Forms.PictureBox()
            Me.chartotal = New System.Windows.Forms.Label()
            Me.acctotal = New System.Windows.Forms.Label()
            Me.uncheckall_char = New System.Windows.Forms.LinkLabel()
            Me.checkall_char = New System.Windows.Forms.LinkLabel()
            Me.uncheckall_acc = New System.Windows.Forms.LinkLabel()
            Me.checkall_acc = New System.Windows.Forms.LinkLabel()
            Me.Label10 = New System.Windows.Forms.Label()
            Me.filter_char = New System.Windows.Forms.LinkLabel()
            Me.filter_acc = New System.Windows.Forms.LinkLabel()
            Me.characterview = New System.Windows.Forms.ListView()
            Me.charguid = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.caccid = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.charname = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.charrace = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.charclass = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.chargender = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.charlevel = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.accountview = New System.Windows.Forms.ListView()
            Me.accid = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.accname = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.gmlevel = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.lastlogin = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.email = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.accountcontext = New System.Windows.Forms.ContextMenuStrip(Me.components)
            Me.RemoveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.SelectedAccountsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.CheckedAccountsToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
            Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.TransferToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.SelectedAccountToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.CheckedAccountsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.connect_bt_target = New System.Windows.Forms.Button()
            Me.target_accounts_tree = New System.Windows.Forms.TreeView()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.GroupBox2 = New System.Windows.Forms.GroupBox()
            Me.createTemplate_bt = New System.Windows.Forms.Button()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.info2_lbl = New System.Windows.Forms.Label()
            Me.info1_lbl = New System.Windows.Forms.Label()
            Me.charactercontext = New System.Windows.Forms.ContextMenuStrip(Me.components)
            Me.RemoveToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
            Me.SelectedCharacterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.CheckedCharactersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.EditToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
            Me.PrepareTransferToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.SelectedCharacterToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
            Me.CheckedCharactersToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
            Me.targetacccontext = New System.Windows.Forms.ContextMenuStrip(Me.components)
            Me.RemoveToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
            Me.targetcharcontext = New System.Windows.Forms.ContextMenuStrip(Me.components)
            Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
            Me.Transfer_bt = New System.Windows.Forms.Button()
            Me.back_bt = New System.Windows.Forms.Button()
            Me.mainpanel = New System.Windows.Forms.Panel()
            Me.header = New System.Windows.Forms.Panel()
            Me.closepanel = New System.Windows.Forms.Panel()
            Me.about_pic = New System.Windows.Forms.PictureBox()
            Me.settings_pic = New System.Windows.Forms.PictureBox()
            Me.highlighter5 = New System.Windows.Forms.PictureBox()
            Me.highlighter4 = New System.Windows.Forms.PictureBox()
            Me.Panel1.SuspendLayout()
            CType(Me.refreshdb, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.accountcontext.SuspendLayout()
            Me.GroupBox1.SuspendLayout()
            Me.GroupBox2.SuspendLayout()
            Me.charactercontext.SuspendLayout()
            Me.targetacccontext.SuspendLayout()
            Me.targetcharcontext.SuspendLayout()
            Me.mainpanel.SuspendLayout()
            Me.header.SuspendLayout()
            Me.closepanel.SuspendLayout()
            CType(Me.about_pic, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.settings_pic, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.highlighter5, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.highlighter4, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'connect_bt
            '
            Me.connect_bt.BackColor = System.Drawing.Color.DimGray
            Me.connect_bt.Cursor = System.Windows.Forms.Cursors.Hand
            Me.connect_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.connect_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.connect_bt.Location = New System.Drawing.Point(804, 4)
            Me.connect_bt.Name = "connect_bt"
            Me.connect_bt.Size = New System.Drawing.Size(147, 42)
            Me.connect_bt.TabIndex = 208
            Me.connect_bt.Text = "Connect to source db"
            Me.connect_bt.UseVisualStyleBackColor = False
            '
            'Panel1
            '
            Me.Panel1.Controls.Add(Me.refreshdb)
            Me.Panel1.Controls.Add(Me.chartotal)
            Me.Panel1.Controls.Add(Me.connect_bt)
            Me.Panel1.Controls.Add(Me.acctotal)
            Me.Panel1.Controls.Add(Me.uncheckall_char)
            Me.Panel1.Controls.Add(Me.checkall_char)
            Me.Panel1.Controls.Add(Me.uncheckall_acc)
            Me.Panel1.Controls.Add(Me.checkall_acc)
            Me.Panel1.Controls.Add(Me.Label10)
            Me.Panel1.Controls.Add(Me.filter_char)
            Me.Panel1.Controls.Add(Me.filter_acc)
            Me.Panel1.Controls.Add(Me.characterview)
            Me.Panel1.Controls.Add(Me.accountview)
            Me.Panel1.Location = New System.Drawing.Point(6, 13)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(986, 469)
            Me.Panel1.TabIndex = 1
            '
            'refreshdb
            '
            Me.refreshdb.BackColor = System.Drawing.Color.Transparent
            Me.refreshdb.BackgroundImage = Global.NamCore_Studio.My.Resources.Resources.refresh
            Me.refreshdb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.refreshdb.Cursor = System.Windows.Forms.Cursors.Hand
            Me.refreshdb.Location = New System.Drawing.Point(768, 14)
            Me.refreshdb.Name = "refreshdb"
            Me.refreshdb.Size = New System.Drawing.Size(30, 30)
            Me.refreshdb.TabIndex = 232
            Me.refreshdb.TabStop = False
            Me.refreshdb.Visible = False
            '
            'chartotal
            '
            Me.chartotal.AutoSize = True
            Me.chartotal.BackColor = System.Drawing.Color.Transparent
            Me.chartotal.Font = New System.Drawing.Font("Franklin Gothic Medium", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.chartotal.ForeColor = System.Drawing.Color.Black
            Me.chartotal.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.chartotal.Location = New System.Drawing.Point(567, 429)
            Me.chartotal.Name = "chartotal"
            Me.chartotal.Size = New System.Drawing.Size(17, 17)
            Me.chartotal.TabIndex = 29
            Me.chartotal.Text = "..."
            '
            'acctotal
            '
            Me.acctotal.AutoSize = True
            Me.acctotal.BackColor = System.Drawing.Color.Transparent
            Me.acctotal.Font = New System.Drawing.Font("Franklin Gothic Medium", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.acctotal.ForeColor = System.Drawing.Color.Black
            Me.acctotal.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.acctotal.Location = New System.Drawing.Point(108, 429)
            Me.acctotal.Name = "acctotal"
            Me.acctotal.Size = New System.Drawing.Size(17, 17)
            Me.acctotal.TabIndex = 28
            Me.acctotal.Text = "..."
            '
            'uncheckall_char
            '
            Me.uncheckall_char.AutoSize = True
            Me.uncheckall_char.LinkColor = System.Drawing.Color.Red
            Me.uncheckall_char.Location = New System.Drawing.Point(491, 451)
            Me.uncheckall_char.Name = "uncheckall_char"
            Me.uncheckall_char.Size = New System.Drawing.Size(64, 13)
            Me.uncheckall_char.TabIndex = 10
            Me.uncheckall_char.TabStop = True
            Me.uncheckall_char.Text = "Uncheck all"
            '
            'checkall_char
            '
            Me.checkall_char.AutoSize = True
            Me.checkall_char.LinkColor = System.Drawing.Color.Red
            Me.checkall_char.Location = New System.Drawing.Point(491, 433)
            Me.checkall_char.Name = "checkall_char"
            Me.checkall_char.Size = New System.Drawing.Size(51, 13)
            Me.checkall_char.TabIndex = 9
            Me.checkall_char.TabStop = True
            Me.checkall_char.Text = "Check all"
            '
            'uncheckall_acc
            '
            Me.uncheckall_acc.AutoSize = True
            Me.uncheckall_acc.LinkColor = System.Drawing.Color.Red
            Me.uncheckall_acc.Location = New System.Drawing.Point(27, 451)
            Me.uncheckall_acc.Name = "uncheckall_acc"
            Me.uncheckall_acc.Size = New System.Drawing.Size(64, 13)
            Me.uncheckall_acc.TabIndex = 8
            Me.uncheckall_acc.TabStop = True
            Me.uncheckall_acc.Text = "Uncheck all"
            '
            'checkall_acc
            '
            Me.checkall_acc.AutoSize = True
            Me.checkall_acc.LinkColor = System.Drawing.Color.Red
            Me.checkall_acc.Location = New System.Drawing.Point(27, 433)
            Me.checkall_acc.Name = "checkall_acc"
            Me.checkall_acc.Size = New System.Drawing.Size(51, 13)
            Me.checkall_acc.TabIndex = 7
            Me.checkall_acc.TabStop = True
            Me.checkall_acc.Text = "Check all"
            '
            'Label10
            '
            Me.Label10.AutoSize = True
            Me.Label10.Font = New System.Drawing.Font("Franklin Gothic Medium", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label10.ForeColor = System.Drawing.Color.Black
            Me.Label10.Location = New System.Drawing.Point(9, 3)
            Me.Label10.Name = "Label10"
            Me.Label10.Size = New System.Drawing.Size(372, 24)
            Me.Label10.TabIndex = 6
            Me.Label10.Text = "Source - Found accounts and characters"
            '
            'filter_char
            '
            Me.filter_char.AutoSize = True
            Me.filter_char.LinkColor = System.Drawing.Color.Red
            Me.filter_char.Location = New System.Drawing.Point(471, 33)
            Me.filter_char.Name = "filter_char"
            Me.filter_char.Size = New System.Drawing.Size(82, 13)
            Me.filter_char.TabIndex = 5
            Me.filter_char.TabStop = True
            Me.filter_char.Text = "Filter characters"
            '
            'filter_acc
            '
            Me.filter_acc.AutoSize = True
            Me.filter_acc.LinkColor = System.Drawing.Color.Red
            Me.filter_acc.Location = New System.Drawing.Point(10, 33)
            Me.filter_acc.Name = "filter_acc"
            Me.filter_acc.Size = New System.Drawing.Size(76, 13)
            Me.filter_acc.TabIndex = 4
            Me.filter_acc.TabStop = True
            Me.filter_acc.Text = "Filter accounts"
            '
            'characterview
            '
            Me.characterview.BackColor = System.Drawing.Color.FromArgb(CType(CType(126, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(156, Byte), Integer))
            Me.characterview.CheckBoxes = True
            Me.characterview.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.charguid, Me.caccid, Me.charname, Me.charrace, Me.charclass, Me.chargender, Me.charlevel})
            Me.characterview.FullRowSelect = True
            Me.characterview.Location = New System.Drawing.Point(474, 50)
            Me.characterview.MultiSelect = False
            Me.characterview.Name = "characterview"
            Me.characterview.Size = New System.Drawing.Size(477, 372)
            Me.characterview.TabIndex = 1
            Me.characterview.UseCompatibleStateImageBehavior = False
            Me.characterview.View = System.Windows.Forms.View.Details
            '
            'charguid
            '
            Me.charguid.Text = "GUID"
            Me.charguid.Width = 41
            '
            'caccid
            '
            Me.caccid.Text = "Account"
            Me.caccid.Width = 55
            '
            'charname
            '
            Me.charname.Text = "Name"
            Me.charname.Width = 134
            '
            'charrace
            '
            Me.charrace.Text = "Race"
            '
            'charclass
            '
            Me.charclass.Text = "Class"
            '
            'chargender
            '
            Me.chargender.Text = "Gender"
            '
            'charlevel
            '
            Me.charlevel.Text = "Level"
            Me.charlevel.Width = 41
            '
            'accountview
            '
            Me.accountview.BackColor = System.Drawing.Color.FromArgb(CType(CType(126, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(156, Byte), Integer))
            Me.accountview.CheckBoxes = True
            Me.accountview.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.accid, Me.accname, Me.gmlevel, Me.lastlogin, Me.email})
            Me.accountview.FullRowSelect = True
            Me.accountview.Location = New System.Drawing.Point(13, 50)
            Me.accountview.MultiSelect = False
            Me.accountview.Name = "accountview"
            Me.accountview.Size = New System.Drawing.Size(448, 372)
            Me.accountview.TabIndex = 0
            Me.accountview.UseCompatibleStateImageBehavior = False
            Me.accountview.View = System.Windows.Forms.View.Details
            '
            'accid
            '
            Me.accid.Text = "ID"
            Me.accid.Width = 32
            '
            'accname
            '
            Me.accname.Text = "Name"
            Me.accname.Width = 132
            '
            'gmlevel
            '
            Me.gmlevel.Text = "GM Level"
            Me.gmlevel.Width = 59
            '
            'lastlogin
            '
            Me.lastlogin.Text = "Last Login"
            Me.lastlogin.Width = 97
            '
            'email
            '
            Me.email.Text = "Email"
            Me.email.Width = 124
            '
            'accountcontext
            '
            Me.accountcontext.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RemoveToolStripMenuItem, Me.EditToolStripMenuItem, Me.TransferToolStripMenuItem})
            Me.accountcontext.Name = "accountcontext"
            Me.accountcontext.Size = New System.Drawing.Size(158, 70)
            '
            'RemoveToolStripMenuItem
            '
            Me.RemoveToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SelectedAccountsToolStripMenuItem, Me.CheckedAccountsToolStripMenuItem1})
            Me.RemoveToolStripMenuItem.Name = "RemoveToolStripMenuItem"
            Me.RemoveToolStripMenuItem.Size = New System.Drawing.Size(157, 22)
            Me.RemoveToolStripMenuItem.Text = "Remove"
            '
            'SelectedAccountsToolStripMenuItem
            '
            Me.SelectedAccountsToolStripMenuItem.Name = "SelectedAccountsToolStripMenuItem"
            Me.SelectedAccountsToolStripMenuItem.Size = New System.Drawing.Size(171, 22)
            Me.SelectedAccountsToolStripMenuItem.Text = "Selected account"
            '
            'CheckedAccountsToolStripMenuItem1
            '
            Me.CheckedAccountsToolStripMenuItem1.Name = "CheckedAccountsToolStripMenuItem1"
            Me.CheckedAccountsToolStripMenuItem1.Size = New System.Drawing.Size(171, 22)
            Me.CheckedAccountsToolStripMenuItem1.Text = "Checked accounts"
            '
            'EditToolStripMenuItem
            '
            Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
            Me.EditToolStripMenuItem.Size = New System.Drawing.Size(157, 22)
            Me.EditToolStripMenuItem.Text = "Edit"
            '
            'TransferToolStripMenuItem
            '
            Me.TransferToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SelectedAccountToolStripMenuItem, Me.CheckedAccountsToolStripMenuItem})
            Me.TransferToolStripMenuItem.Name = "TransferToolStripMenuItem"
            Me.TransferToolStripMenuItem.Size = New System.Drawing.Size(157, 22)
            Me.TransferToolStripMenuItem.Text = "Prepare transfer"
            '
            'SelectedAccountToolStripMenuItem
            '
            Me.SelectedAccountToolStripMenuItem.Name = "SelectedAccountToolStripMenuItem"
            Me.SelectedAccountToolStripMenuItem.Size = New System.Drawing.Size(171, 22)
            Me.SelectedAccountToolStripMenuItem.Text = "Selected account"
            '
            'CheckedAccountsToolStripMenuItem
            '
            Me.CheckedAccountsToolStripMenuItem.Name = "CheckedAccountsToolStripMenuItem"
            Me.CheckedAccountsToolStripMenuItem.Size = New System.Drawing.Size(171, 22)
            Me.CheckedAccountsToolStripMenuItem.Text = "Checked accounts"
            '
            'connect_bt_target
            '
            Me.connect_bt_target.BackColor = System.Drawing.Color.DimGray
            Me.connect_bt_target.Cursor = System.Windows.Forms.Cursors.Hand
            Me.connect_bt_target.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.connect_bt_target.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.connect_bt_target.Location = New System.Drawing.Point(11, 16)
            Me.connect_bt_target.Name = "connect_bt_target"
            Me.connect_bt_target.Size = New System.Drawing.Size(147, 42)
            Me.connect_bt_target.TabIndex = 209
            Me.connect_bt_target.Text = "Connect to target db or create template"
            Me.connect_bt_target.UseVisualStyleBackColor = False
            '
            'target_accounts_tree
            '
            Me.target_accounts_tree.AllowDrop = True
            Me.target_accounts_tree.BackColor = System.Drawing.Color.FromArgb(CType(CType(126, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(156, Byte), Integer))
            Me.target_accounts_tree.Location = New System.Drawing.Point(11, 63)
            Me.target_accounts_tree.Name = "target_accounts_tree"
            Me.target_accounts_tree.Size = New System.Drawing.Size(363, 372)
            Me.target_accounts_tree.TabIndex = 210
            '
            'GroupBox1
            '
            Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
            Me.GroupBox1.Controls.Add(Me.Panel1)
            Me.GroupBox1.Location = New System.Drawing.Point(3, -5)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(989, 482)
            Me.GroupBox1.TabIndex = 211
            Me.GroupBox1.TabStop = False
            '
            'GroupBox2
            '
            Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
            Me.GroupBox2.Controls.Add(Me.createTemplate_bt)
            Me.GroupBox2.Controls.Add(Me.Label3)
            Me.GroupBox2.Controls.Add(Me.info2_lbl)
            Me.GroupBox2.Controls.Add(Me.info1_lbl)
            Me.GroupBox2.Controls.Add(Me.connect_bt_target)
            Me.GroupBox2.Controls.Add(Me.target_accounts_tree)
            Me.GroupBox2.Location = New System.Drawing.Point(990, -5)
            Me.GroupBox2.Name = "GroupBox2"
            Me.GroupBox2.Size = New System.Drawing.Size(388, 482)
            Me.GroupBox2.TabIndex = 212
            Me.GroupBox2.TabStop = False
            '
            'createTemplate_bt
            '
            Me.createTemplate_bt.BackColor = System.Drawing.Color.DimGray
            Me.createTemplate_bt.Cursor = System.Windows.Forms.Cursors.Hand
            Me.createTemplate_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.createTemplate_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.createTemplate_bt.Location = New System.Drawing.Point(123, 441)
            Me.createTemplate_bt.Name = "createTemplate_bt"
            Me.createTemplate_bt.Size = New System.Drawing.Size(147, 34)
            Me.createTemplate_bt.TabIndex = 214
            Me.createTemplate_bt.Text = "Create template file"
            Me.createTemplate_bt.UseVisualStyleBackColor = False
            Me.createTemplate_bt.Visible = False
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Font = New System.Drawing.Font("Franklin Gothic Medium", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label3.ForeColor = System.Drawing.Color.Black
            Me.Label3.Location = New System.Drawing.Point(160, 13)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(233, 48)
            Me.Label3.TabIndex = 213
            Me.Label3.Text = "Target - Found accounts " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "and characters"
            '
            'info2_lbl
            '
            Me.info2_lbl.AutoSize = True
            Me.info2_lbl.BackColor = System.Drawing.Color.Yellow
            Me.info2_lbl.Location = New System.Drawing.Point(8, 462)
            Me.info2_lbl.Name = "info2_lbl"
            Me.info2_lbl.Size = New System.Drawing.Size(262, 13)
            Me.info2_lbl.TabIndex = 212
            Me.info2_lbl.Text = "Character name conflict / forced namechange at login"
            Me.info2_lbl.Visible = False
            '
            'info1_lbl
            '
            Me.info1_lbl.AutoSize = True
            Me.info1_lbl.BackColor = System.Drawing.Color.Green
            Me.info1_lbl.Location = New System.Drawing.Point(8, 442)
            Me.info1_lbl.Name = "info1_lbl"
            Me.info1_lbl.Size = New System.Drawing.Size(124, 13)
            Me.info1_lbl.TabIndex = 211
            Me.info1_lbl.Text = "Character can be copied"
            Me.info1_lbl.Visible = False
            '
            'charactercontext
            '
            Me.charactercontext.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RemoveToolStripMenuItem1, Me.EditToolStripMenuItem1, Me.PrepareTransferToolStripMenuItem})
            Me.charactercontext.Name = "charactercontext"
            Me.charactercontext.Size = New System.Drawing.Size(158, 70)
            '
            'RemoveToolStripMenuItem1
            '
            Me.RemoveToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SelectedCharacterToolStripMenuItem, Me.CheckedCharactersToolStripMenuItem})
            Me.RemoveToolStripMenuItem1.Name = "RemoveToolStripMenuItem1"
            Me.RemoveToolStripMenuItem1.Size = New System.Drawing.Size(157, 22)
            Me.RemoveToolStripMenuItem1.Text = "Remove"
            '
            'SelectedCharacterToolStripMenuItem
            '
            Me.SelectedCharacterToolStripMenuItem.Name = "SelectedCharacterToolStripMenuItem"
            Me.SelectedCharacterToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
            Me.SelectedCharacterToolStripMenuItem.Text = "Selected character"
            '
            'CheckedCharactersToolStripMenuItem
            '
            Me.CheckedCharactersToolStripMenuItem.Name = "CheckedCharactersToolStripMenuItem"
            Me.CheckedCharactersToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
            Me.CheckedCharactersToolStripMenuItem.Text = "Checked characters"
            '
            'EditToolStripMenuItem1
            '
            Me.EditToolStripMenuItem1.Name = "EditToolStripMenuItem1"
            Me.EditToolStripMenuItem1.Size = New System.Drawing.Size(157, 22)
            Me.EditToolStripMenuItem1.Text = "Edit"
            '
            'PrepareTransferToolStripMenuItem
            '
            Me.PrepareTransferToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SelectedCharacterToolStripMenuItem1, Me.CheckedCharactersToolStripMenuItem1})
            Me.PrepareTransferToolStripMenuItem.Name = "PrepareTransferToolStripMenuItem"
            Me.PrepareTransferToolStripMenuItem.Size = New System.Drawing.Size(157, 22)
            Me.PrepareTransferToolStripMenuItem.Text = "Prepare transfer"
            '
            'SelectedCharacterToolStripMenuItem1
            '
            Me.SelectedCharacterToolStripMenuItem1.Name = "SelectedCharacterToolStripMenuItem1"
            Me.SelectedCharacterToolStripMenuItem1.Size = New System.Drawing.Size(177, 22)
            Me.SelectedCharacterToolStripMenuItem1.Text = "Selected character"
            '
            'CheckedCharactersToolStripMenuItem1
            '
            Me.CheckedCharactersToolStripMenuItem1.Name = "CheckedCharactersToolStripMenuItem1"
            Me.CheckedCharactersToolStripMenuItem1.Size = New System.Drawing.Size(177, 22)
            Me.CheckedCharactersToolStripMenuItem1.Text = "Checked characters"
            '
            'targetacccontext
            '
            Me.targetacccontext.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RemoveToolStripMenuItem2})
            Me.targetacccontext.Name = "targetacccontext"
            Me.targetacccontext.Size = New System.Drawing.Size(118, 26)
            '
            'RemoveToolStripMenuItem2
            '
            Me.RemoveToolStripMenuItem2.Name = "RemoveToolStripMenuItem2"
            Me.RemoveToolStripMenuItem2.Size = New System.Drawing.Size(117, 22)
            Me.RemoveToolStripMenuItem2.Text = "Remove"
            '
            'targetcharcontext
            '
            Me.targetcharcontext.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1})
            Me.targetcharcontext.Name = "targetacccontext"
            Me.targetcharcontext.Size = New System.Drawing.Size(118, 26)
            '
            'ToolStripMenuItem1
            '
            Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
            Me.ToolStripMenuItem1.Size = New System.Drawing.Size(117, 22)
            Me.ToolStripMenuItem1.Text = "Remove"
            '
            'Transfer_bt
            '
            Me.Transfer_bt.BackColor = System.Drawing.Color.DimGray
            Me.Transfer_bt.Cursor = System.Windows.Forms.Cursors.Hand
            Me.Transfer_bt.Enabled = False
            Me.Transfer_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.Transfer_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.Transfer_bt.Location = New System.Drawing.Point(360, 599)
            Me.Transfer_bt.Name = "Transfer_bt"
            Me.Transfer_bt.Size = New System.Drawing.Size(251, 42)
            Me.Transfer_bt.TabIndex = 214
            Me.Transfer_bt.Text = "Start Transfer"
            Me.Transfer_bt.UseVisualStyleBackColor = False
            '
            'back_bt
            '
            Me.back_bt.BackColor = System.Drawing.Color.DimGray
            Me.back_bt.Cursor = System.Windows.Forms.Cursors.Hand
            Me.back_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.back_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.back_bt.Location = New System.Drawing.Point(838, 598)
            Me.back_bt.Name = "back_bt"
            Me.back_bt.Size = New System.Drawing.Size(147, 42)
            Me.back_bt.TabIndex = 215
            Me.back_bt.Text = "Back to main menu"
            Me.back_bt.UseVisualStyleBackColor = False
            '
            'mainpanel
            '
            Me.mainpanel.AutoScroll = True
            Me.mainpanel.BackColor = System.Drawing.Color.Transparent
            Me.mainpanel.Controls.Add(Me.GroupBox1)
            Me.mainpanel.Controls.Add(Me.GroupBox2)
            Me.mainpanel.Location = New System.Drawing.Point(5, 89)
            Me.mainpanel.Name = "mainpanel"
            Me.mainpanel.Size = New System.Drawing.Size(991, 495)
            Me.mainpanel.TabIndex = 219
            '
            'header
            '
            Me.header.BackColor = System.Drawing.Color.Transparent
            Me.header.BackgroundImage = CType(resources.GetObject("header.BackgroundImage"), System.Drawing.Image)
            Me.header.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
            Me.header.Controls.Add(Me.closepanel)
            Me.header.Location = New System.Drawing.Point(6, 3)
            Me.header.Name = "header"
            Me.header.Size = New System.Drawing.Size(989, 82)
            Me.header.TabIndex = 226
            '
            'closepanel
            '
            Me.closepanel.BackColor = System.Drawing.Color.Transparent
            Me.closepanel.Controls.Add(Me.about_pic)
            Me.closepanel.Controls.Add(Me.settings_pic)
            Me.closepanel.Controls.Add(Me.highlighter5)
            Me.closepanel.Controls.Add(Me.highlighter4)
            Me.closepanel.Location = New System.Drawing.Point(864, 1)
            Me.closepanel.Name = "closepanel"
            Me.closepanel.Size = New System.Drawing.Size(123, 25)
            Me.closepanel.TabIndex = 173
            '
            'about_pic
            '
            Me.about_pic.BackColor = System.Drawing.Color.Transparent
            Me.about_pic.BackgroundImage = Global.NamCore_Studio.My.Resources.Resources.bt_about
            Me.about_pic.Cursor = System.Windows.Forms.Cursors.Hand
            Me.about_pic.Location = New System.Drawing.Point(2, 2)
            Me.about_pic.Name = "about_pic"
            Me.about_pic.Size = New System.Drawing.Size(25, 20)
            Me.about_pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.about_pic.TabIndex = 176
            Me.about_pic.TabStop = False
            '
            'settings_pic
            '
            Me.settings_pic.BackColor = System.Drawing.Color.Transparent
            Me.settings_pic.BackgroundImage = Global.NamCore_Studio.My.Resources.Resources.bt_settings
            Me.settings_pic.Cursor = System.Windows.Forms.Cursors.Hand
            Me.settings_pic.Location = New System.Drawing.Point(33, 2)
            Me.settings_pic.Name = "settings_pic"
            Me.settings_pic.Size = New System.Drawing.Size(25, 20)
            Me.settings_pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.settings_pic.TabIndex = 175
            Me.settings_pic.TabStop = False
            '
            'highlighter5
            '
            Me.highlighter5.BackColor = System.Drawing.Color.Transparent
            Me.highlighter5.BackgroundImage = Global.NamCore_Studio.My.Resources.Resources.bt_minimize
            Me.highlighter5.Cursor = System.Windows.Forms.Cursors.Hand
            Me.highlighter5.Location = New System.Drawing.Point(64, 2)
            Me.highlighter5.Name = "highlighter5"
            Me.highlighter5.Size = New System.Drawing.Size(25, 20)
            Me.highlighter5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.highlighter5.TabIndex = 174
            Me.highlighter5.TabStop = False
            '
            'highlighter4
            '
            Me.highlighter4.BackColor = System.Drawing.Color.Transparent
            Me.highlighter4.BackgroundImage = Global.NamCore_Studio.My.Resources.Resources.bt_close
            Me.highlighter4.Cursor = System.Windows.Forms.Cursors.Hand
            Me.highlighter4.Location = New System.Drawing.Point(95, 2)
            Me.highlighter4.Name = "highlighter4"
            Me.highlighter4.Size = New System.Drawing.Size(25, 20)
            Me.highlighter4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.highlighter4.TabIndex = 173
            Me.highlighter4.TabStop = False
            '
            'LiveView
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Control
            Me.BackgroundImage = Global.NamCore_Studio.My.Resources.Resources.HUD_bg
            Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.ClientSize = New System.Drawing.Size(1001, 646)
            Me.Controls.Add(Me.header)
            Me.Controls.Add(Me.mainpanel)
            Me.Controls.Add(Me.back_bt)
            Me.Controls.Add(Me.Transfer_bt)
            Me.DoubleBuffered = True
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.KeyPreview = True
            Me.MaximumSize = New System.Drawing.Size(1392, 646)
            Me.MinimumSize = New System.Drawing.Size(800, 646)
            Me.Name = "LiveView"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "liveview"
            Me.Panel1.ResumeLayout(False)
            Me.Panel1.PerformLayout()
            CType(Me.refreshdb, System.ComponentModel.ISupportInitialize).EndInit()
            Me.accountcontext.ResumeLayout(False)
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox2.ResumeLayout(False)
            Me.GroupBox2.PerformLayout()
            Me.charactercontext.ResumeLayout(False)
            Me.targetacccontext.ResumeLayout(False)
            Me.targetcharcontext.ResumeLayout(False)
            Me.mainpanel.ResumeLayout(False)
            Me.header.ResumeLayout(False)
            Me.closepanel.ResumeLayout(False)
            CType(Me.about_pic, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.settings_pic, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.highlighter5, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.highlighter4, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents connect_bt As System.Windows.Forms.Button
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents uncheckall_char As System.Windows.Forms.LinkLabel
        Friend WithEvents checkall_char As System.Windows.Forms.LinkLabel
        Friend WithEvents uncheckall_acc As System.Windows.Forms.LinkLabel
        Friend WithEvents checkall_acc As System.Windows.Forms.LinkLabel
        Friend WithEvents Label10 As System.Windows.Forms.Label
        Friend WithEvents filter_char As System.Windows.Forms.LinkLabel
        Friend WithEvents filter_acc As System.Windows.Forms.LinkLabel
        Friend WithEvents characterview As System.Windows.Forms.ListView
        Friend WithEvents accountview As System.Windows.Forms.ListView
        Friend WithEvents charguid As System.Windows.Forms.ColumnHeader
        Friend WithEvents accid As System.Windows.Forms.ColumnHeader
        Friend WithEvents chartotal As System.Windows.Forms.Label
        Friend WithEvents acctotal As System.Windows.Forms.Label
        Friend WithEvents accname As System.Windows.Forms.ColumnHeader
        Friend WithEvents gmlevel As System.Windows.Forms.ColumnHeader
        Friend WithEvents lastlogin As System.Windows.Forms.ColumnHeader
        Friend WithEvents email As System.Windows.Forms.ColumnHeader
        Friend WithEvents caccid As System.Windows.Forms.ColumnHeader
        Friend WithEvents charname As System.Windows.Forms.ColumnHeader
        Friend WithEvents charrace As System.Windows.Forms.ColumnHeader
        Friend WithEvents charclass As System.Windows.Forms.ColumnHeader
        Friend WithEvents chargender As System.Windows.Forms.ColumnHeader
        Friend WithEvents charlevel As System.Windows.Forms.ColumnHeader
        Friend WithEvents accountcontext As System.Windows.Forms.ContextMenuStrip
        Friend WithEvents RemoveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents SelectedAccountsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents CheckedAccountsToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents connect_bt_target As System.Windows.Forms.Button
        Friend WithEvents target_accounts_tree As System.Windows.Forms.TreeView
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
        Friend WithEvents charactercontext As System.Windows.Forms.ContextMenuStrip
        Friend WithEvents RemoveToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents SelectedCharacterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents CheckedCharactersToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents EditToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents targetacccontext As System.Windows.Forms.ContextMenuStrip
        Friend WithEvents RemoveToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents targetcharcontext As System.Windows.Forms.ContextMenuStrip
        Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents TransferToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents SelectedAccountToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents CheckedAccountsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents PrepareTransferToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents SelectedCharacterToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents CheckedCharactersToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents Transfer_bt As System.Windows.Forms.Button
        Friend WithEvents info2_lbl As System.Windows.Forms.Label
        Friend WithEvents info1_lbl As System.Windows.Forms.Label
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents back_bt As System.Windows.Forms.Button
        Friend WithEvents mainpanel As System.Windows.Forms.Panel
        Friend WithEvents header As System.Windows.Forms.Panel
        Friend WithEvents createTemplate_bt As System.Windows.Forms.Button
        Friend WithEvents closepanel As System.Windows.Forms.Panel
        Friend WithEvents about_pic As System.Windows.Forms.PictureBox
        Friend WithEvents settings_pic As System.Windows.Forms.PictureBox
        Friend WithEvents highlighter5 As System.Windows.Forms.PictureBox
        Friend WithEvents highlighter4 As System.Windows.Forms.PictureBox
        Friend WithEvents refreshdb As System.Windows.Forms.PictureBox
    End Class
End Namespace