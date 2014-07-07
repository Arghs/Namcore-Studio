Imports NamCore_Studio.Forms.Extension

Namespace Forms

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class AccountOverview
        Inherits eventtrigger

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
        Me.accname_lbl = New System.Windows.Forms.Label()
        Me.characterview = New System.Windows.Forms.ListView()
        Me.charguid = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.charname = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.charrace = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.charclass = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.chargender = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.charlevel = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.Label2 = New System.Windows.Forms.Label()
        Me.joindate_lbl = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lastip_lbl = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lastlogin_lbl = New System.Windows.Forms.Label()
        Me.online_lbl = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.expansion_ud = New System.Windows.Forms.NumericUpDown()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.mail_lbl = New System.Windows.Forms.Label()
        Me.lockaccount_cb = New System.Windows.Forms.CheckBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.reset_bt = New System.Windows.Forms.Button()
        Me.savechanges_bt = New System.Windows.Forms.Button()
        Me.exit_bt = New System.Windows.Forms.Button()
        Me.charactercontext = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.RemoveToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.SelectedCharacterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CheckedCharactersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.changepanel = New System.Windows.Forms.Panel()
        Me.updatePic = New System.Windows.Forms.PictureBox()
        Me.changeText_tb = New System.Windows.Forms.TextBox()
        CType(Me.expansion_ud,System.ComponentModel.ISupportInitialize).BeginInit
        Me.GroupBox1.SuspendLayout
        Me.charactercontext.SuspendLayout
        Me.changepanel.SuspendLayout
        CType(Me.updatePic,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'accname_lbl
        '
        Me.accname_lbl.AutoSize = true
        Me.accname_lbl.BackColor = System.Drawing.Color.Transparent
        Me.accname_lbl.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.accname_lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.accname_lbl.ForeColor = System.Drawing.Color.Black
        Me.accname_lbl.Location = New System.Drawing.Point(237, 63)
        Me.accname_lbl.Name = "accname_lbl"
        Me.accname_lbl.Size = New System.Drawing.Size(119, 20)
        Me.accname_lbl.TabIndex = 224
        Me.accname_lbl.Text = "Accountname"
        '
        'characterview
        '
        Me.characterview.BackColor = System.Drawing.Color.FromArgb(CType(CType(126,Byte),Integer), CType(CType(144,Byte),Integer), CType(CType(156,Byte),Integer))
        Me.characterview.CheckBoxes = true
        Me.characterview.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.charguid, Me.charname, Me.charrace, Me.charclass, Me.chargender, Me.charlevel})
        Me.characterview.FullRowSelect = true
        Me.characterview.Location = New System.Drawing.Point(12, 212)
        Me.characterview.MultiSelect = false
        Me.characterview.Name = "characterview"
        Me.characterview.Size = New System.Drawing.Size(478, 197)
        Me.characterview.TabIndex = 225
        Me.characterview.UseCompatibleStateImageBehavior = false
        Me.characterview.View = System.Windows.Forms.View.Details
        '
        'charguid
        '
        Me.charguid.Text = "GUID"
        Me.charguid.Width = 41
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
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(6, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 16)
        Me.Label2.TabIndex = 226
        Me.Label2.Text = "Joindate:"
        '
        'joindate_lbl
        '
        Me.joindate_lbl.AutoSize = true
        Me.joindate_lbl.BackColor = System.Drawing.Color.Transparent
        Me.joindate_lbl.Cursor = System.Windows.Forms.Cursors.Default
        Me.joindate_lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic),System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.joindate_lbl.ForeColor = System.Drawing.Color.Black
        Me.joindate_lbl.Location = New System.Drawing.Point(84, 22)
        Me.joindate_lbl.Name = "joindate_lbl"
        Me.joindate_lbl.Size = New System.Drawing.Size(141, 15)
        Me.joindate_lbl.TabIndex = 227
        Me.joindate_lbl.Text = "2012-10-28 19:15:50"
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(6, 45)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 16)
        Me.Label1.TabIndex = 228
        Me.Label1.Text = "Last ip:"
        '
        'lastip_lbl
        '
        Me.lastip_lbl.AutoSize = true
        Me.lastip_lbl.BackColor = System.Drawing.Color.Transparent
        Me.lastip_lbl.Cursor = System.Windows.Forms.Cursors.Default
        Me.lastip_lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic),System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lastip_lbl.ForeColor = System.Drawing.Color.Black
        Me.lastip_lbl.Location = New System.Drawing.Point(84, 46)
        Me.lastip_lbl.Name = "lastip_lbl"
        Me.lastip_lbl.Size = New System.Drawing.Size(67, 15)
        Me.lastip_lbl.TabIndex = 229
        Me.lastip_lbl.Text = "127.0.0.1"
        '
        'Label4
        '
        Me.Label4.AutoSize = true
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(6, 68)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(79, 16)
        Me.Label4.TabIndex = 230
        Me.Label4.Text = "Last login:"
        '
        'lastlogin_lbl
        '
        Me.lastlogin_lbl.AutoSize = true
        Me.lastlogin_lbl.BackColor = System.Drawing.Color.Transparent
        Me.lastlogin_lbl.Cursor = System.Windows.Forms.Cursors.Default
        Me.lastlogin_lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic),System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lastlogin_lbl.ForeColor = System.Drawing.Color.Black
        Me.lastlogin_lbl.Location = New System.Drawing.Point(84, 69)
        Me.lastlogin_lbl.Name = "lastlogin_lbl"
        Me.lastlogin_lbl.Size = New System.Drawing.Size(141, 15)
        Me.lastlogin_lbl.TabIndex = 231
        Me.lastlogin_lbl.Text = "2012-10-28 19:15:50"
        '
        'online_lbl
        '
        Me.online_lbl.AutoSize = true
        Me.online_lbl.BackColor = System.Drawing.Color.Transparent
        Me.online_lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.online_lbl.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(192,Byte),Integer), CType(CType(0,Byte),Integer))
        Me.online_lbl.Location = New System.Drawing.Point(438, 65)
        Me.online_lbl.Name = "online_lbl"
        Me.online_lbl.Size = New System.Drawing.Size(52, 16)
        Me.online_lbl.TabIndex = 232
        Me.online_lbl.Text = "Online"
        '
        'Label7
        '
        Me.Label7.AutoSize = true
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(262, 133)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(84, 16)
        Me.Label7.TabIndex = 234
        Me.Label7.Text = "Expansion:"
        '
        'expansion_ud
        '
        Me.expansion_ud.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.expansion_ud.Location = New System.Drawing.Point(346, 131)
        Me.expansion_ud.Maximum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.expansion_ud.Name = "expansion_ud"
        Me.expansion_ud.Size = New System.Drawing.Size(37, 21)
        Me.expansion_ud.TabIndex = 235
        Me.expansion_ud.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.expansion_ud.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'Label8
        '
        Me.Label8.AutoSize = true
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(262, 158)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(51, 16)
        Me.Label8.TabIndex = 236
        Me.Label8.Text = "Email:"
        '
        'mail_lbl
        '
        Me.mail_lbl.AutoSize = true
        Me.mail_lbl.BackColor = System.Drawing.Color.Transparent
        Me.mail_lbl.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.mail_lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic),System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.mail_lbl.ForeColor = System.Drawing.Color.Black
        Me.mail_lbl.Location = New System.Drawing.Point(319, 158)
        Me.mail_lbl.Name = "mail_lbl"
        Me.mail_lbl.Size = New System.Drawing.Size(157, 15)
        Me.mail_lbl.TabIndex = 237
        Me.mail_lbl.Text = "admin@yourserver.com"
        '
        'lockaccount_cb
        '
        Me.lockaccount_cb.AutoSize = true
        Me.lockaccount_cb.BackColor = System.Drawing.Color.Transparent
        Me.lockaccount_cb.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold)
        Me.lockaccount_cb.ForeColor = System.Drawing.Color.Black
        Me.lockaccount_cb.Location = New System.Drawing.Point(265, 108)
        Me.lockaccount_cb.Name = "lockaccount_cb"
        Me.lockaccount_cb.Size = New System.Drawing.Size(118, 20)
        Me.lockaccount_cb.TabIndex = 238
        Me.lockaccount_cb.Text = "Lock account"
        Me.lockaccount_cb.UseVisualStyleBackColor = false
        '
        'Label10
        '
        Me.Label10.AutoSize = true
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(13, 193)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(87, 16)
        Me.Label10.TabIndex = 239
        Me.Label10.Text = "Characters:"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.lastlogin_lbl)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.joindate_lbl)
        Me.GroupBox1.Controls.Add(Me.lastip_lbl)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.Black
        Me.GroupBox1.Location = New System.Drawing.Point(14, 89)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(235, 93)
        Me.GroupBox1.TabIndex = 240
        Me.GroupBox1.TabStop = false
        Me.GroupBox1.Text = "General"
        '
        'reset_bt
        '
        Me.reset_bt.BackColor = System.Drawing.Color.DimGray
        Me.reset_bt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.reset_bt.Enabled = false
        Me.reset_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.reset_bt.ForeColor = System.Drawing.Color.Black
        Me.reset_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.reset_bt.Location = New System.Drawing.Point(12, 415)
        Me.reset_bt.Name = "reset_bt"
        Me.reset_bt.Size = New System.Drawing.Size(155, 34)
        Me.reset_bt.TabIndex = 243
        Me.reset_bt.Text = "Reset"
        Me.reset_bt.UseVisualStyleBackColor = false
        '
        'savechanges_bt
        '
        Me.savechanges_bt.BackColor = System.Drawing.Color.DimGray
        Me.savechanges_bt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.savechanges_bt.Enabled = false
        Me.savechanges_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.savechanges_bt.ForeColor = System.Drawing.Color.Black
        Me.savechanges_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.savechanges_bt.Location = New System.Drawing.Point(173, 415)
        Me.savechanges_bt.Name = "savechanges_bt"
        Me.savechanges_bt.Size = New System.Drawing.Size(155, 34)
        Me.savechanges_bt.TabIndex = 242
        Me.savechanges_bt.Text = "Save changes"
        Me.savechanges_bt.UseVisualStyleBackColor = false
        '
        'exit_bt
        '
        Me.exit_bt.BackColor = System.Drawing.Color.DimGray
        Me.exit_bt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.exit_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.exit_bt.ForeColor = System.Drawing.Color.Black
        Me.exit_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.exit_bt.Location = New System.Drawing.Point(335, 415)
        Me.exit_bt.Name = "exit_bt"
        Me.exit_bt.Size = New System.Drawing.Size(155, 34)
        Me.exit_bt.TabIndex = 241
        Me.exit_bt.Text = "Exit"
        Me.exit_bt.UseVisualStyleBackColor = false
        '
        'charactercontext
        '
        Me.charactercontext.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RemoveToolStripMenuItem1, Me.EditToolStripMenuItem1})
        Me.charactercontext.Name = "charactercontext"
        Me.charactercontext.Size = New System.Drawing.Size(118, 48)
        '
        'RemoveToolStripMenuItem1
        '
        Me.RemoveToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SelectedCharacterToolStripMenuItem, Me.CheckedCharactersToolStripMenuItem})
        Me.RemoveToolStripMenuItem1.Name = "RemoveToolStripMenuItem1"
        Me.RemoveToolStripMenuItem1.Size = New System.Drawing.Size(117, 22)
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
        Me.EditToolStripMenuItem1.Size = New System.Drawing.Size(117, 22)
        Me.EditToolStripMenuItem1.Text = "Edit"
        '
        'changepanel
        '
        Me.changepanel.BackColor = System.Drawing.Color.Transparent
        Me.changepanel.Controls.Add(Me.updatePic)
        Me.changepanel.Controls.Add(Me.changeText_tb)
        Me.changepanel.Location = New System.Drawing.Point(799, 212)
        Me.changepanel.Name = "changepanel"
        Me.changepanel.Size = New System.Drawing.Size(133, 24)
        Me.changepanel.TabIndex = 244
        '
        'updatePic
        '
        Me.updatePic.Cursor = System.Windows.Forms.Cursors.Hand
        Me.updatePic.Image = Global.NamCore_Studio.My.Resources.Resources.Refresh_icon
        Me.updatePic.Location = New System.Drawing.Point(115, 4)
        Me.updatePic.Name = "updatePic"
        Me.updatePic.Size = New System.Drawing.Size(16, 16)
        Me.updatePic.TabIndex = 174
        Me.updatePic.TabStop = false
        '
        'changeText_tb
        '
        Me.changeText_tb.Location = New System.Drawing.Point(3, 2)
        Me.changeText_tb.Name = "changeText_tb"
        Me.changeText_tb.Size = New System.Drawing.Size(106, 20)
        Me.changeText_tb.TabIndex = 0
        '
        'AccountOverview
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.NamCore_Studio.My.Resources.Resources.HUD_bg
        Me.ClientSize = New System.Drawing.Size(500, 458)
        Me.Controls.Add(Me.changepanel)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.lockaccount_cb)
        Me.Controls.Add(Me.mail_lbl)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.expansion_ud)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.reset_bt)
        Me.Controls.Add(Me.savechanges_bt)
        Me.Controls.Add(Me.exit_bt)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.online_lbl)
        Me.Controls.Add(Me.characterview)
        Me.Controls.Add(Me.accname_lbl)
        Me.DoubleBuffered = true
        Me.Name = "AccountOverview"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AccountOverview"
        Me.Controls.SetChildIndex(Me.accname_lbl, 0)
        Me.Controls.SetChildIndex(Me.characterview, 0)
        Me.Controls.SetChildIndex(Me.online_lbl, 0)
        Me.Controls.SetChildIndex(Me.Label10, 0)
        Me.Controls.SetChildIndex(Me.exit_bt, 0)
        Me.Controls.SetChildIndex(Me.savechanges_bt, 0)
        Me.Controls.SetChildIndex(Me.reset_bt, 0)
        Me.Controls.SetChildIndex(Me.Label7, 0)
        Me.Controls.SetChildIndex(Me.expansion_ud, 0)
        Me.Controls.SetChildIndex(Me.Label8, 0)
        Me.Controls.SetChildIndex(Me.mail_lbl, 0)
        Me.Controls.SetChildIndex(Me.lockaccount_cb, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.changepanel, 0)
        CType(Me.expansion_ud,System.ComponentModel.ISupportInitialize).EndInit
        Me.GroupBox1.ResumeLayout(false)
        Me.GroupBox1.PerformLayout
        Me.charactercontext.ResumeLayout(false)
        Me.changepanel.ResumeLayout(false)
        Me.changepanel.PerformLayout
        CType(Me.updatePic,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
        Friend WithEvents accname_lbl As System.Windows.Forms.Label
        Friend WithEvents characterview As System.Windows.Forms.ListView
        Friend WithEvents charguid As System.Windows.Forms.ColumnHeader
        Friend WithEvents charname As System.Windows.Forms.ColumnHeader
        Friend WithEvents charrace As System.Windows.Forms.ColumnHeader
        Friend WithEvents charclass As System.Windows.Forms.ColumnHeader
        Friend WithEvents chargender As System.Windows.Forms.ColumnHeader
        Friend WithEvents charlevel As System.Windows.Forms.ColumnHeader
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents joindate_lbl As System.Windows.Forms.Label
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents lastip_lbl As System.Windows.Forms.Label
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents lastlogin_lbl As System.Windows.Forms.Label
        Friend WithEvents online_lbl As System.Windows.Forms.Label
        Friend WithEvents Label7 As System.Windows.Forms.Label
        Friend WithEvents expansion_ud As System.Windows.Forms.NumericUpDown
        Friend WithEvents Label8 As System.Windows.Forms.Label
        Friend WithEvents mail_lbl As System.Windows.Forms.Label
        Friend WithEvents lockaccount_cb As System.Windows.Forms.CheckBox
        Friend WithEvents Label10 As System.Windows.Forms.Label
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents reset_bt As System.Windows.Forms.Button
        Friend WithEvents savechanges_bt As System.Windows.Forms.Button
        Friend WithEvents exit_bt As System.Windows.Forms.Button
        Friend WithEvents charactercontext As System.Windows.Forms.ContextMenuStrip
        Friend WithEvents RemoveToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents SelectedCharacterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents CheckedCharactersToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents EditToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents changepanel As System.Windows.Forms.Panel
        Friend WithEvents updatePic As System.Windows.Forms.PictureBox
        Friend WithEvents changeText_tb As System.Windows.Forms.TextBox
    End Class
End Namespace