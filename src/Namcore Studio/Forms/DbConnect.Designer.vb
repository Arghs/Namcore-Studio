﻿Imports NamCore_Studio.Forms.Extension

Namespace Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class DbConnect
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DbConnect))
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
            Me.port_ud = New System.Windows.Forms.NumericUpDown()
            Me.Label9 = New System.Windows.Forms.Label()
            Me.Label8 = New System.Windows.Forms.Label()
            Me.Label7 = New System.Windows.Forms.Label()
            Me.chardbname_combo = New System.Windows.Forms.ComboBox()
            Me.Label6 = New System.Windows.Forms.Label()
            Me.realmdbname_combo = New System.Windows.Forms.ComboBox()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.Label5 = New System.Windows.Forms.Label()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.db_address_txtbox = New System.Windows.Forms.TextBox()
            Me.password_txtbox = New System.Windows.Forms.TextBox()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.userid_txtbox = New System.Windows.Forms.TextBox()
            Me.savelogin_bt = New System.Windows.Forms.Button()
            Me.connect_header_label = New System.Windows.Forms.Label()
            Me.trinity335picker = New System.Windows.Forms.RadioButton()
            Me.mangos335picker = New System.Windows.Forms.RadioButton()
            Me.GroupBox3 = New System.Windows.Forms.GroupBox()
            Me.connect_panel.SuspendLayout()
            Me.login1_panel.SuspendLayout()
            Me.GroupBox2.SuspendLayout()
            Me.GroupBox1.SuspendLayout()
            CType(Me.port_ud, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.GroupBox3.SuspendLayout()
            Me.SuspendLayout()
            '
            'connect_panel
            '
            Me.connect_panel.BackColor = System.Drawing.Color.Transparent
            Me.connect_panel.Controls.Add(Me.login1_panel)
            Me.connect_panel.Controls.Add(Me.connect_header_label)
            Me.connect_panel.Location = New System.Drawing.Point(12, 84)
            Me.connect_panel.Name = "connect_panel"
            Me.connect_panel.Size = New System.Drawing.Size(514, 424)
            Me.connect_panel.TabIndex = 1
            '
            'login1_panel
            '
            Me.login1_panel.Controls.Add(Me.GroupBox2)
            Me.login1_panel.Controls.Add(Me.viaserver_radio)
            Me.login1_panel.Controls.Add(Me.defaultconn_radio)
            Me.login1_panel.Controls.Add(Me.GroupBox1)
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
            Me.Label11.ForeColor = System.Drawing.Color.Black
            Me.Label11.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.Label11.Location = New System.Drawing.Point(374, 45)
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
            Me.Label14.ForeColor = System.Drawing.Color.Black
            Me.Label14.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.Label14.Location = New System.Drawing.Point(6, 12)
            Me.Label14.Name = "Label14"
            Me.Label14.Size = New System.Drawing.Size(197, 21)
            Me.Label14.TabIndex = 14
            Me.Label14.Text = "NamCore Remote Server" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
            '
            'Label15
            '
            Me.Label15.AutoSize = True
            Me.Label15.BackColor = System.Drawing.Color.Transparent
            Me.Label15.Font = New System.Drawing.Font("Franklin Gothic Medium", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label15.ForeColor = System.Drawing.Color.Black
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
            Me.Label16.ForeColor = System.Drawing.Color.Black
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
            Me.Label17.ForeColor = System.Drawing.Color.Black
            Me.Label17.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.Label17.Location = New System.Drawing.Point(16, 72)
            Me.Label17.Name = "Label17"
            Me.Label17.Size = New System.Drawing.Size(57, 17)
            Me.Label17.TabIndex = 21
            Me.Label17.Text = "User-Id:"
            '
            'serveraddress_txtbox
            '
            Me.serveraddress_txtbox.Enabled = False
            Me.serveraddress_txtbox.Location = New System.Drawing.Point(146, 44)
            Me.serveraddress_txtbox.Name = "serveraddress_txtbox"
            Me.serveraddress_txtbox.Size = New System.Drawing.Size(222, 20)
            Me.serveraddress_txtbox.TabIndex = 15
            '
            'rmpass_txtbox
            '
            Me.rmpass_txtbox.Enabled = False
            Me.rmpass_txtbox.Location = New System.Drawing.Point(146, 97)
            Me.rmpass_txtbox.Name = "rmpass_txtbox"
            Me.rmpass_txtbox.Size = New System.Drawing.Size(169, 20)
            Me.rmpass_txtbox.TabIndex = 19
            '
            'rmuser_txtbox
            '
            Me.rmuser_txtbox.Enabled = False
            Me.rmuser_txtbox.Location = New System.Drawing.Point(146, 71)
            Me.rmuser_txtbox.Name = "rmuser_txtbox"
            Me.rmuser_txtbox.Size = New System.Drawing.Size(169, 20)
            Me.rmuser_txtbox.TabIndex = 18
            '
            'viaserver_radio
            '
            Me.viaserver_radio.AutoSize = True
            Me.viaserver_radio.Enabled = False
            Me.viaserver_radio.Font = New System.Drawing.Font("Franklin Gothic Medium", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.viaserver_radio.ForeColor = System.Drawing.Color.Black
            Me.viaserver_radio.Location = New System.Drawing.Point(3, 28)
            Me.viaserver_radio.Name = "viaserver_radio"
            Me.viaserver_radio.Size = New System.Drawing.Size(264, 21)
            Me.viaserver_radio.TabIndex = 212
            Me.viaserver_radio.Text = "Connect via NamCore Remote Server"
            Me.viaserver_radio.UseVisualStyleBackColor = True
            '
            'defaultconn_radio
            '
            Me.defaultconn_radio.AutoSize = True
            Me.defaultconn_radio.Checked = True
            Me.defaultconn_radio.Font = New System.Drawing.Font("Franklin Gothic Medium", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.defaultconn_radio.ForeColor = System.Drawing.Color.Black
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
            Me.connect_bt.Location = New System.Drawing.Point(21, 559)
            Me.connect_bt.Name = "connect_bt"
            Me.connect_bt.Size = New System.Drawing.Size(147, 42)
            Me.connect_bt.TabIndex = 208
            Me.connect_bt.Text = "Connect"
            Me.connect_bt.UseVisualStyleBackColor = False
            '
            'GroupBox1
            '
            Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
            Me.GroupBox1.Controls.Add(Me.port_ud)
            Me.GroupBox1.Controls.Add(Me.Label9)
            Me.GroupBox1.Controls.Add(Me.Label8)
            Me.GroupBox1.Controls.Add(Me.Label7)
            Me.GroupBox1.Controls.Add(Me.chardbname_combo)
            Me.GroupBox1.Controls.Add(Me.Label6)
            Me.GroupBox1.Controls.Add(Me.realmdbname_combo)
            Me.GroupBox1.Controls.Add(Me.Label1)
            Me.GroupBox1.Controls.Add(Me.Label2)
            Me.GroupBox1.Controls.Add(Me.Label5)
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
            'port_ud
            '
            Me.port_ud.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.port_ud.Location = New System.Drawing.Point(183, 64)
            Me.port_ud.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
            Me.port_ud.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
            Me.port_ud.Name = "port_ud"
            Me.port_ud.Size = New System.Drawing.Size(74, 21)
            Me.port_ud.TabIndex = 29
            Me.port_ud.Value = New Decimal(New Integer() {80, 0, 0, 0})
            '
            'Label9
            '
            Me.Label9.AutoSize = True
            Me.Label9.BackColor = System.Drawing.Color.Transparent
            Me.Label9.Font = New System.Drawing.Font("Franklin Gothic Medium", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label9.ForeColor = System.Drawing.Color.Black
            Me.Label9.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.Label9.Location = New System.Drawing.Point(263, 66)
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
            Me.Label8.ForeColor = System.Drawing.Color.Black
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
            Me.Label7.ForeColor = System.Drawing.Color.Black
            Me.Label7.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.Label7.Location = New System.Drawing.Point(16, 170)
            Me.Label7.Name = "Label7"
            Me.Label7.Size = New System.Drawing.Size(135, 17)
            Me.Label7.TabIndex = 26
            Me.Label7.Text = "Character db name:"
            '
            'chardbname_combo
            '
            Me.chardbname_combo.Location = New System.Drawing.Point(183, 169)
            Me.chardbname_combo.Name = "chardbname_combo"
            Me.chardbname_combo.Size = New System.Drawing.Size(169, 21)
            Me.chardbname_combo.TabIndex = 25
            Me.chardbname_combo.Text = "arc_characters"
            '
            'Label6
            '
            Me.Label6.AutoSize = True
            Me.Label6.BackColor = System.Drawing.Color.Transparent
            Me.Label6.Font = New System.Drawing.Font("Franklin Gothic Medium", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label6.ForeColor = System.Drawing.Color.Black
            Me.Label6.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.Label6.Location = New System.Drawing.Point(16, 144)
            Me.Label6.Name = "Label6"
            Me.Label6.Size = New System.Drawing.Size(150, 17)
            Me.Label6.TabIndex = 24
            Me.Label6.Text = "Realm/Auth db name:"
            '
            'realmdbname_combo
            '
            Me.realmdbname_combo.Location = New System.Drawing.Point(183, 143)
            Me.realmdbname_combo.Name = "realmdbname_combo"
            Me.realmdbname_combo.Size = New System.Drawing.Size(169, 21)
            Me.realmdbname_combo.TabIndex = 23
            Me.realmdbname_combo.Text = "arc_auth"
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.BackColor = System.Drawing.Color.Transparent
            Me.Label1.Font = New System.Drawing.Font("Franklin Gothic Medium", 12.0!, System.Drawing.FontStyle.Bold)
            Me.Label1.ForeColor = System.Drawing.Color.Black
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
            Me.Label2.ForeColor = System.Drawing.Color.Black
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
            Me.Label5.ForeColor = System.Drawing.Color.Black
            Me.Label5.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.Label5.Location = New System.Drawing.Point(16, 118)
            Me.Label5.Name = "Label5"
            Me.Label5.Size = New System.Drawing.Size(74, 17)
            Me.Label5.TabIndex = 22
            Me.Label5.Text = "Password:"
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.BackColor = System.Drawing.Color.Transparent
            Me.Label4.Font = New System.Drawing.Font("Franklin Gothic Medium", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label4.ForeColor = System.Drawing.Color.Black
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
            Me.Label3.ForeColor = System.Drawing.Color.Black
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
            'savelogin_bt
            '
            Me.savelogin_bt.BackColor = System.Drawing.Color.DimGray
            Me.savelogin_bt.Cursor = System.Windows.Forms.Cursors.Hand
            Me.savelogin_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.savelogin_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.savelogin_bt.Location = New System.Drawing.Point(393, 567)
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
            Me.connect_header_label.ForeColor = System.Drawing.Color.Black
            Me.connect_header_label.Location = New System.Drawing.Point(5, 10)
            Me.connect_header_label.Name = "connect_header_label"
            Me.connect_header_label.Size = New System.Drawing.Size(234, 24)
            Me.connect_header_label.TabIndex = 0
            Me.connect_header_label.Text = "Connect to source server"
            '
            'trinity335picker
            '
            Me.trinity335picker.AutoSize = True
            Me.trinity335picker.BackColor = System.Drawing.Color.Transparent
            Me.trinity335picker.Checked = True
            Me.trinity335picker.Font = New System.Drawing.Font("Franklin Gothic Medium", 9.75!, System.Drawing.FontStyle.Bold)
            Me.trinity335picker.Location = New System.Drawing.Point(12, 19)
            Me.trinity335picker.Name = "trinity335picker"
            Me.trinity335picker.Size = New System.Drawing.Size(134, 21)
            Me.trinity335picker.TabIndex = 226
            Me.trinity335picker.TabStop = True
            Me.trinity335picker.Text = "TrinityCore 3.3.5"
            Me.trinity335picker.UseVisualStyleBackColor = False
            '
            'mangos335picker
            '
            Me.mangos335picker.AutoSize = True
            Me.mangos335picker.BackColor = System.Drawing.Color.Transparent
            Me.mangos335picker.Font = New System.Drawing.Font("Franklin Gothic Medium", 9.75!, System.Drawing.FontStyle.Bold)
            Me.mangos335picker.Location = New System.Drawing.Point(169, 19)
            Me.mangos335picker.Name = "mangos335picker"
            Me.mangos335picker.Size = New System.Drawing.Size(119, 21)
            Me.mangos335picker.TabIndex = 227
            Me.mangos335picker.Text = "MaNGOS 3.3.5"
            Me.mangos335picker.UseVisualStyleBackColor = False
            '
            'GroupBox3
            '
            Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
            Me.GroupBox3.Controls.Add(Me.trinity335picker)
            Me.GroupBox3.Controls.Add(Me.mangos335picker)
            Me.GroupBox3.Font = New System.Drawing.Font("Franklin Gothic Medium", 9.75!, System.Drawing.FontStyle.Bold)
            Me.GroupBox3.Location = New System.Drawing.Point(24, 501)
            Me.GroupBox3.Name = "GroupBox3"
            Me.GroupBox3.Size = New System.Drawing.Size(490, 44)
            Me.GroupBox3.TabIndex = 228
            Me.GroupBox3.TabStop = False
            Me.GroupBox3.Text = " Pick core"
            '
            'DbConnect
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackgroundImage = Global.NamCore_Studio.My.Resources.Resources.HUD_bg
            Me.ClientSize = New System.Drawing.Size(541, 608)
            Me.Controls.Add(Me.GroupBox3)
            Me.Controls.Add(Me.connect_panel)
            Me.Controls.Add(Me.connect_bt)
            Me.Controls.Add(Me.savelogin_bt)
            Me.DoubleBuffered = True
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "DbConnect"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "DB_connect"
            Me.Controls.SetChildIndex(Me.savelogin_bt, 0)
            Me.Controls.SetChildIndex(Me.connect_bt, 0)
            Me.Controls.SetChildIndex(Me.connect_panel, 0)
            Me.Controls.SetChildIndex(Me.GroupBox3, 0)
            Me.connect_panel.ResumeLayout(False)
            Me.connect_panel.PerformLayout()
            Me.login1_panel.ResumeLayout(False)
            Me.login1_panel.PerformLayout()
            Me.GroupBox2.ResumeLayout(False)
            Me.GroupBox2.PerformLayout()
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            CType(Me.port_ud, System.ComponentModel.ISupportInitialize).EndInit()
            Me.GroupBox3.ResumeLayout(False)
            Me.GroupBox3.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents connect_panel As System.Windows.Forms.Panel
        Friend WithEvents login1_panel As System.Windows.Forms.Panel
        Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
        Friend WithEvents Label11 As System.Windows.Forms.Label
        Friend WithEvents Label14 As System.Windows.Forms.Label
        Friend WithEvents Label15 As System.Windows.Forms.Label
        Friend WithEvents Label16 As System.Windows.Forms.Label
        Friend WithEvents Label17 As System.Windows.Forms.Label
        Friend WithEvents serveraddress_txtbox As System.Windows.Forms.TextBox
        Friend WithEvents rmpass_txtbox As System.Windows.Forms.TextBox
        Friend WithEvents rmuser_txtbox As System.Windows.Forms.TextBox
        Friend WithEvents viaserver_radio As System.Windows.Forms.RadioButton
        Friend WithEvents defaultconn_radio As System.Windows.Forms.RadioButton
        Friend WithEvents connect_bt As System.Windows.Forms.Button
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents Label9 As System.Windows.Forms.Label
        Friend WithEvents Label8 As System.Windows.Forms.Label
        Friend WithEvents Label7 As System.Windows.Forms.Label
        Friend WithEvents chardbname_combo As System.Windows.Forms.ComboBox
        Friend WithEvents Label6 As System.Windows.Forms.Label
        Friend WithEvents realmdbname_combo As System.Windows.Forms.ComboBox
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents Label5 As System.Windows.Forms.Label
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents db_address_txtbox As System.Windows.Forms.TextBox
        Friend WithEvents password_txtbox As System.Windows.Forms.TextBox
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents userid_txtbox As System.Windows.Forms.TextBox
        Friend WithEvents savelogin_bt As System.Windows.Forms.Button
        Friend WithEvents connect_header_label As System.Windows.Forms.Label
        Friend WithEvents port_ud As System.Windows.Forms.NumericUpDown
        Friend WithEvents trinity335picker As System.Windows.Forms.RadioButton
        Friend WithEvents mangos335picker As System.Windows.Forms.RadioButton
        Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    End Class
End Namespace