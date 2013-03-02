<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Live_View
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
        Me.components = New System.ComponentModel.Container()
        Me.connect_panel = New System.Windows.Forms.Panel()
        Me.login1_panel = New System.Windows.Forms.Panel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.serveraddress_txtbox = New System.Windows.Forms.TextBox()
        Me.rmpass_txtbox = New System.Windows.Forms.TextBox()
        Me.rmuser_txtbox = New System.Windows.Forms.TextBox()
        Me.viaserver_radio = New System.Windows.Forms.RadioButton()
        Me.defaultconn_radio = New System.Windows.Forms.RadioButton()
        Me.connect_bt = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.chardbname_txtbox = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.realmdbname_txtbox = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.port_txtbox = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.db_address_txtbox = New System.Windows.Forms.TextBox()
        Me.password_txtbox = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.userid_txtbox = New System.Windows.Forms.TextBox()
        Me.getlogin_bt = New System.Windows.Forms.Button()
        Me.savelogin_bt = New System.Windows.Forms.Button()
        Me.connect_header_label = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
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
        Me.connect_panel.SuspendLayout()
        Me.login1_panel.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.accountcontext.SuspendLayout()
        Me.SuspendLayout()
        '
        'connect_panel
        '
        Me.connect_panel.BackColor = System.Drawing.Color.Transparent
        Me.connect_panel.Controls.Add(Me.login1_panel)
        Me.connect_panel.Controls.Add(Me.connect_header_label)
        Me.connect_panel.Location = New System.Drawing.Point(3, 61)
        Me.connect_panel.Name = "connect_panel"
        Me.connect_panel.Size = New System.Drawing.Size(514, 490)
        Me.connect_panel.TabIndex = 0
        '
        'login1_panel
        '
        Me.login1_panel.Controls.Add(Me.GroupBox2)
        Me.login1_panel.Controls.Add(Me.viaserver_radio)
        Me.login1_panel.Controls.Add(Me.defaultconn_radio)
        Me.login1_panel.Controls.Add(Me.connect_bt)
        Me.login1_panel.Controls.Add(Me.GroupBox1)
        Me.login1_panel.Controls.Add(Me.getlogin_bt)
        Me.login1_panel.Controls.Add(Me.savelogin_bt)
        Me.login1_panel.Location = New System.Drawing.Point(9, 37)
        Me.login1_panel.Name = "login1_panel"
        Me.login1_panel.Size = New System.Drawing.Size(499, 440)
        Me.login1_panel.TabIndex = 2
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.Label14)
        Me.GroupBox2.Controls.Add(Me.Label15)
        Me.GroupBox2.Controls.Add(Me.Label16)
        Me.GroupBox2.Controls.Add(Me.Label17)
        Me.GroupBox2.Controls.Add(Me.serveraddress_txtbox)
        Me.GroupBox2.Controls.Add(Me.rmpass_txtbox)
        Me.GroupBox2.Controls.Add(Me.rmuser_txtbox)
        Me.GroupBox2.Location = New System.Drawing.Point(40, 47)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(422, 126)
        Me.GroupBox2.TabIndex = 24
        Me.GroupBox2.TabStop = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Franklin Gothic Medium", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.SteelBlue
        Me.Label11.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label11.Location = New System.Drawing.Point(364, 45)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(27, 17)
        Me.Label11.TabIndex = 27
        Me.Label11.Text = "(IP)"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Franklin Gothic Medium", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label14.ForeColor = System.Drawing.Color.SteelBlue
        Me.Label14.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label14.Location = New System.Drawing.Point(6, 12)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(195, 21)
        Me.Label14.TabIndex = 14
        Me.Label14.Text = "Namcore Remote Server" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Franklin Gothic Medium", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.SteelBlue
        Me.Label15.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label15.Location = New System.Drawing.Point(16, 45)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(108, 17)
        Me.Label15.TabIndex = 16
        Me.Label15.Text = "Server address:"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Font = New System.Drawing.Font("Franklin Gothic Medium", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.SteelBlue
        Me.Label16.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label16.Location = New System.Drawing.Point(16, 98)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(74, 17)
        Me.Label16.TabIndex = 22
        Me.Label16.Text = "Password:"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Font = New System.Drawing.Font("Franklin Gothic Medium", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.SteelBlue
        Me.Label17.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label17.Location = New System.Drawing.Point(16, 72)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(57, 17)
        Me.Label17.TabIndex = 21
        Me.Label17.Text = "User-Id:"
        '
        'serveraddress_txtbox
        '
        Me.serveraddress_txtbox.Location = New System.Drawing.Point(136, 44)
        Me.serveraddress_txtbox.Name = "serveraddress_txtbox"
        Me.serveraddress_txtbox.Size = New System.Drawing.Size(222, 20)
        Me.serveraddress_txtbox.TabIndex = 15
        '
        'rmpass_txtbox
        '
        Me.rmpass_txtbox.Location = New System.Drawing.Point(136, 97)
        Me.rmpass_txtbox.Name = "rmpass_txtbox"
        Me.rmpass_txtbox.Size = New System.Drawing.Size(169, 20)
        Me.rmpass_txtbox.TabIndex = 19
        '
        'rmuser_txtbox
        '
        Me.rmuser_txtbox.Location = New System.Drawing.Point(136, 71)
        Me.rmuser_txtbox.Name = "rmuser_txtbox"
        Me.rmuser_txtbox.Size = New System.Drawing.Size(169, 20)
        Me.rmuser_txtbox.TabIndex = 18
        '
        'viaserver_radio
        '
        Me.viaserver_radio.AutoSize = True
        Me.viaserver_radio.Font = New System.Drawing.Font("Franklin Gothic Medium", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viaserver_radio.ForeColor = System.Drawing.Color.SteelBlue
        Me.viaserver_radio.Location = New System.Drawing.Point(3, 28)
        Me.viaserver_radio.Name = "viaserver_radio"
        Me.viaserver_radio.Size = New System.Drawing.Size(262, 21)
        Me.viaserver_radio.TabIndex = 212
        Me.viaserver_radio.Text = "Connect via Namcore Remote Server"
        Me.viaserver_radio.UseVisualStyleBackColor = True
        '
        'defaultconn_radio
        '
        Me.defaultconn_radio.AutoSize = True
        Me.defaultconn_radio.Checked = True
        Me.defaultconn_radio.Font = New System.Drawing.Font("Franklin Gothic Medium", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.defaultconn_radio.ForeColor = System.Drawing.Color.SteelBlue
        Me.defaultconn_radio.Location = New System.Drawing.Point(3, 3)
        Me.defaultconn_radio.Name = "defaultconn_radio"
        Me.defaultconn_radio.Size = New System.Drawing.Size(149, 21)
        Me.defaultconn_radio.TabIndex = 211
        Me.defaultconn_radio.TabStop = True
        Me.defaultconn_radio.Text = "Default connection"
        Me.defaultconn_radio.UseVisualStyleBackColor = True
        '
        'connect_bt
        '
        Me.connect_bt.BackColor = System.Drawing.Color.DimGray
        Me.connect_bt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.connect_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.connect_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.connect_bt.Location = New System.Drawing.Point(22, 385)
        Me.connect_bt.Name = "connect_bt"
        Me.connect_bt.Size = New System.Drawing.Size(147, 42)
        Me.connect_bt.TabIndex = 208
        Me.connect_bt.Text = "Connect"
        Me.connect_bt.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.chardbname_txtbox)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.realmdbname_txtbox)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.port_txtbox)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.db_address_txtbox)
        Me.GroupBox1.Controls.Add(Me.password_txtbox)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.userid_txtbox)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 177)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(490, 202)
        Me.GroupBox1.TabIndex = 23
        Me.GroupBox1.TabStop = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Franklin Gothic Medium", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.SteelBlue
        Me.Label9.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label9.Location = New System.Drawing.Point(257, 67)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(86, 17)
        Me.Label9.TabIndex = 28
        Me.Label9.Text = "(3316/3306)"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Franklin Gothic Medium", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.SteelBlue
        Me.Label8.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label8.Location = New System.Drawing.Point(409, 38)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(66, 17)
        Me.Label8.TabIndex = 27
        Me.Label8.Text = "(localhost)"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Franklin Gothic Medium", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.SteelBlue
        Me.Label7.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label7.Location = New System.Drawing.Point(16, 170)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(135, 17)
        Me.Label7.TabIndex = 26
        Me.Label7.Text = "Character db name:"
        '
        'chardbname_txtbox
        '
        Me.chardbname_txtbox.Location = New System.Drawing.Point(183, 169)
        Me.chardbname_txtbox.Name = "chardbname_txtbox"
        Me.chardbname_txtbox.Size = New System.Drawing.Size(169, 20)
        Me.chardbname_txtbox.TabIndex = 25
        Me.chardbname_txtbox.Text = "arc_characters"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Franklin Gothic Medium", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.SteelBlue
        Me.Label6.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label6.Location = New System.Drawing.Point(16, 144)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(150, 17)
        Me.Label6.TabIndex = 24
        Me.Label6.Text = "Realm/Auth db name:"
        '
        'realmdbname_txtbox
        '
        Me.realmdbname_txtbox.Location = New System.Drawing.Point(183, 143)
        Me.realmdbname_txtbox.Name = "realmdbname_txtbox"
        Me.realmdbname_txtbox.Size = New System.Drawing.Size(169, 20)
        Me.realmdbname_txtbox.TabIndex = 23
        Me.realmdbname_txtbox.Text = "arc_auth"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Franklin Gothic Medium", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.Color.SteelBlue
        Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label1.Location = New System.Drawing.Point(6, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(144, 21)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "MySQL Login Info" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Franklin Gothic Medium", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.SteelBlue
        Me.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label2.Location = New System.Drawing.Point(16, 38)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(129, 17)
        Me.Label2.TabIndex = 16
        Me.Label2.Text = "Database address:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Franklin Gothic Medium", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.SteelBlue
        Me.Label5.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label5.Location = New System.Drawing.Point(16, 118)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(74, 17)
        Me.Label5.TabIndex = 22
        Me.Label5.Text = "Password:"
        '
        'port_txtbox
        '
        Me.port_txtbox.Location = New System.Drawing.Point(183, 64)
        Me.port_txtbox.Name = "port_txtbox"
        Me.port_txtbox.Size = New System.Drawing.Size(69, 20)
        Me.port_txtbox.TabIndex = 17
        Me.port_txtbox.Text = "3306"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Franklin Gothic Medium", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.SteelBlue
        Me.Label4.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label4.Location = New System.Drawing.Point(16, 92)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(57, 17)
        Me.Label4.TabIndex = 21
        Me.Label4.Text = "User-Id:"
        '
        'db_address_txtbox
        '
        Me.db_address_txtbox.Location = New System.Drawing.Point(183, 37)
        Me.db_address_txtbox.Name = "db_address_txtbox"
        Me.db_address_txtbox.Size = New System.Drawing.Size(222, 20)
        Me.db_address_txtbox.TabIndex = 15
        Me.db_address_txtbox.Text = "localhost"
        '
        'password_txtbox
        '
        Me.password_txtbox.Location = New System.Drawing.Point(183, 117)
        Me.password_txtbox.Name = "password_txtbox"
        Me.password_txtbox.Size = New System.Drawing.Size(169, 20)
        Me.password_txtbox.TabIndex = 19
        Me.password_txtbox.Text = "mangos"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Franklin Gothic Medium", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.SteelBlue
        Me.Label3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label3.Location = New System.Drawing.Point(16, 66)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 17)
        Me.Label3.TabIndex = 20
        Me.Label3.Text = "Port:"
        '
        'userid_txtbox
        '
        Me.userid_txtbox.Location = New System.Drawing.Point(183, 91)
        Me.userid_txtbox.Name = "userid_txtbox"
        Me.userid_txtbox.Size = New System.Drawing.Size(169, 20)
        Me.userid_txtbox.TabIndex = 18
        Me.userid_txtbox.Text = "mangos"
        '
        'getlogin_bt
        '
        Me.getlogin_bt.BackColor = System.Drawing.Color.DimGray
        Me.getlogin_bt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.getlogin_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.getlogin_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.getlogin_bt.Location = New System.Drawing.Point(234, 393)
        Me.getlogin_bt.Name = "getlogin_bt"
        Me.getlogin_bt.Size = New System.Drawing.Size(121, 34)
        Me.getlogin_bt.TabIndex = 209
        Me.getlogin_bt.Text = "Standard login data"
        Me.getlogin_bt.UseVisualStyleBackColor = False
        '
        'savelogin_bt
        '
        Me.savelogin_bt.BackColor = System.Drawing.Color.DimGray
        Me.savelogin_bt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.savelogin_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.savelogin_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.savelogin_bt.Location = New System.Drawing.Point(361, 393)
        Me.savelogin_bt.Name = "savelogin_bt"
        Me.savelogin_bt.Size = New System.Drawing.Size(121, 34)
        Me.savelogin_bt.TabIndex = 210
        Me.savelogin_bt.Text = "Save as standard"
        Me.savelogin_bt.UseVisualStyleBackColor = False
        '
        'connect_header_label
        '
        Me.connect_header_label.AutoSize = True
        Me.connect_header_label.Font = New System.Drawing.Font("Franklin Gothic Medium", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.connect_header_label.ForeColor = System.Drawing.Color.SteelBlue
        Me.connect_header_label.Location = New System.Drawing.Point(5, 10)
        Me.connect_header_label.Name = "connect_header_label"
        Me.connect_header_label.Size = New System.Drawing.Size(234, 24)
        Me.connect_header_label.TabIndex = 0
        Me.connect_header_label.Text = "Connect to source server"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chartotal)
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
        Me.Panel1.Location = New System.Drawing.Point(523, 61)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(974, 607)
        Me.Panel1.TabIndex = 1
        '
        'chartotal
        '
        Me.chartotal.AutoSize = True
        Me.chartotal.BackColor = System.Drawing.Color.Transparent
        Me.chartotal.Font = New System.Drawing.Font("Franklin Gothic Medium", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chartotal.ForeColor = System.Drawing.Color.SteelBlue
        Me.chartotal.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.chartotal.Location = New System.Drawing.Point(527, 444)
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
        Me.acctotal.ForeColor = System.Drawing.Color.SteelBlue
        Me.acctotal.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.acctotal.Location = New System.Drawing.Point(108, 444)
        Me.acctotal.Name = "acctotal"
        Me.acctotal.Size = New System.Drawing.Size(17, 17)
        Me.acctotal.TabIndex = 28
        Me.acctotal.Text = "..."
        '
        'uncheckall_char
        '
        Me.uncheckall_char.AutoSize = True
        Me.uncheckall_char.LinkColor = System.Drawing.Color.Red
        Me.uncheckall_char.Location = New System.Drawing.Point(451, 466)
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
        Me.checkall_char.Location = New System.Drawing.Point(451, 448)
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
        Me.uncheckall_acc.Location = New System.Drawing.Point(27, 466)
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
        Me.checkall_acc.Location = New System.Drawing.Point(27, 448)
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
        Me.Label10.ForeColor = System.Drawing.Color.SteelBlue
        Me.Label10.Location = New System.Drawing.Point(9, 10)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(380, 24)
        Me.Label10.TabIndex = 6
        Me.Label10.Text = "List of all found accounts and characters"
        '
        'filter_char
        '
        Me.filter_char.AutoSize = True
        Me.filter_char.LinkColor = System.Drawing.Color.Red
        Me.filter_char.Location = New System.Drawing.Point(552, 48)
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
        Me.filter_acc.Location = New System.Drawing.Point(15, 48)
        Me.filter_acc.Name = "filter_acc"
        Me.filter_acc.Size = New System.Drawing.Size(76, 13)
        Me.filter_acc.TabIndex = 4
        Me.filter_acc.TabStop = True
        Me.filter_acc.Text = "Filter accounts"
        '
        'characterview
        '
        Me.characterview.CheckBoxes = True
        Me.characterview.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.charguid, Me.caccid, Me.charname, Me.charrace, Me.charclass, Me.chargender, Me.charlevel})
        Me.characterview.FullRowSelect = True
        Me.characterview.Location = New System.Drawing.Point(474, 65)
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
        '
        'accountview
        '
        Me.accountview.CheckBoxes = True
        Me.accountview.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.accid, Me.accname, Me.gmlevel, Me.lastlogin, Me.email})
        Me.accountview.FullRowSelect = True
        Me.accountview.Location = New System.Drawing.Point(13, 65)
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
        Me.accountcontext.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RemoveToolStripMenuItem, Me.EditToolStripMenuItem})
        Me.accountcontext.Name = "accountcontext"
        Me.accountcontext.Size = New System.Drawing.Size(118, 48)
        '
        'RemoveToolStripMenuItem
        '
        Me.RemoveToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SelectedAccountsToolStripMenuItem, Me.CheckedAccountsToolStripMenuItem1})
        Me.RemoveToolStripMenuItem.Name = "RemoveToolStripMenuItem"
        Me.RemoveToolStripMenuItem.Size = New System.Drawing.Size(117, 22)
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
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(117, 22)
        Me.EditToolStripMenuItem.Text = "Edit"
        '
        'Live_View
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Blue
        Me.ClientSize = New System.Drawing.Size(1525, 702)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.connect_panel)
        Me.Name = "Live_View"
        Me.Text = "Live_View"
        Me.connect_panel.ResumeLayout(False)
        Me.connect_panel.PerformLayout()
        Me.login1_panel.ResumeLayout(False)
        Me.login1_panel.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.accountcontext.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents connect_panel As System.Windows.Forms.Panel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents port_txtbox As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents db_address_txtbox As System.Windows.Forms.TextBox
    Friend WithEvents password_txtbox As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents userid_txtbox As System.Windows.Forms.TextBox
    Friend WithEvents connect_header_label As System.Windows.Forms.Label
    Friend WithEvents connect_bt As System.Windows.Forms.Button
    Friend WithEvents getlogin_bt As System.Windows.Forms.Button
    Friend WithEvents savelogin_bt As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents chardbname_txtbox As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents realmdbname_txtbox As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents serveraddress_txtbox As System.Windows.Forms.TextBox
    Friend WithEvents rmpass_txtbox As System.Windows.Forms.TextBox
    Friend WithEvents rmuser_txtbox As System.Windows.Forms.TextBox
    Friend WithEvents login1_panel As System.Windows.Forms.Panel
    Friend WithEvents viaserver_radio As System.Windows.Forms.RadioButton
    Friend WithEvents defaultconn_radio As System.Windows.Forms.RadioButton
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
End Class
