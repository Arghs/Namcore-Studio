<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AccountOverview
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
        Me.header = New System.Windows.Forms.Panel()
        Me.closepanel = New System.Windows.Forms.Panel()
        Me.highlighter1 = New System.Windows.Forms.PictureBox()
        Me.highlighter2 = New System.Windows.Forms.PictureBox()
        Me.charname_lbl = New System.Windows.Forms.Label()
        Me.characterview = New System.Windows.Forms.ListView()
        Me.charguid = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.caccid = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.charname = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.charrace = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.charclass = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chargender = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.charlevel = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label2 = New System.Windows.Forms.Label()
        Me.level_lbl = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.port_ud = New System.Windows.Forms.NumericUpDown()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.reset_bt = New System.Windows.Forms.Button()
        Me.savechanges_bt = New System.Windows.Forms.Button()
        Me.exit_bt = New System.Windows.Forms.Button()
        Me.header.SuspendLayout()
        Me.closepanel.SuspendLayout()
        CType(Me.highlighter1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.highlighter2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.port_ud, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'header
        '
        Me.header.BackgroundImage = Global.Namcore_Studio.My.Resources.Resources.namcore_header
        Me.header.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.header.Controls.Add(Me.closepanel)
        Me.header.Location = New System.Drawing.Point(3, 4)
        Me.header.Name = "header"
        Me.header.Size = New System.Drawing.Size(496, 30)
        Me.header.TabIndex = 223
        '
        'closepanel
        '
        Me.closepanel.BackgroundImage = Global.Namcore_Studio.My.Resources.Resources.minclose
        Me.closepanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.closepanel.Controls.Add(Me.highlighter1)
        Me.closepanel.Controls.Add(Me.highlighter2)
        Me.closepanel.Location = New System.Drawing.Point(438, 0)
        Me.closepanel.Name = "closepanel"
        Me.closepanel.Size = New System.Drawing.Size(56, 28)
        Me.closepanel.TabIndex = 0
        '
        'highlighter1
        '
        Me.highlighter1.BackColor = System.Drawing.Color.Transparent
        Me.highlighter1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.highlighter1.Location = New System.Drawing.Point(5, 5)
        Me.highlighter1.Name = "highlighter1"
        Me.highlighter1.Size = New System.Drawing.Size(20, 20)
        Me.highlighter1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.highlighter1.TabIndex = 217
        Me.highlighter1.TabStop = False
        '
        'highlighter2
        '
        Me.highlighter2.BackColor = System.Drawing.Color.Transparent
        Me.highlighter2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.highlighter2.Location = New System.Drawing.Point(33, 5)
        Me.highlighter2.Name = "highlighter2"
        Me.highlighter2.Size = New System.Drawing.Size(20, 20)
        Me.highlighter2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.highlighter2.TabIndex = 218
        Me.highlighter2.TabStop = False
        '
        'charname_lbl
        '
        Me.charname_lbl.AutoSize = True
        Me.charname_lbl.BackColor = System.Drawing.Color.Transparent
        Me.charname_lbl.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.charname_lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.charname_lbl.ForeColor = System.Drawing.Color.FromArgb(CType(CType(126, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(156, Byte), Integer))
        Me.charname_lbl.Location = New System.Drawing.Point(12, 40)
        Me.charname_lbl.Name = "charname_lbl"
        Me.charname_lbl.Size = New System.Drawing.Size(119, 20)
        Me.charname_lbl.TabIndex = 224
        Me.charname_lbl.Text = "Accountname"
        '
        'characterview
        '
        Me.characterview.BackColor = System.Drawing.Color.FromArgb(CType(CType(126, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(156, Byte), Integer))
        Me.characterview.CheckBoxes = True
        Me.characterview.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.charguid, Me.caccid, Me.charname, Me.charrace, Me.charclass, Me.chargender, Me.charlevel})
        Me.characterview.FullRowSelect = True
        Me.characterview.Location = New System.Drawing.Point(12, 193)
        Me.characterview.MultiSelect = False
        Me.characterview.Name = "characterview"
        Me.characterview.Size = New System.Drawing.Size(478, 197)
        Me.characterview.TabIndex = 225
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
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(126, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(156, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(6, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 16)
        Me.Label2.TabIndex = 226
        Me.Label2.Text = "Joindate:"
        '
        'level_lbl
        '
        Me.level_lbl.AutoSize = True
        Me.level_lbl.BackColor = System.Drawing.Color.Transparent
        Me.level_lbl.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.level_lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.level_lbl.ForeColor = System.Drawing.Color.FromArgb(CType(CType(126, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(156, Byte), Integer))
        Me.level_lbl.Location = New System.Drawing.Point(84, 22)
        Me.level_lbl.Name = "level_lbl"
        Me.level_lbl.Size = New System.Drawing.Size(141, 15)
        Me.level_lbl.TabIndex = 227
        Me.level_lbl.Text = "2012-10-28 19:15:50"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(126, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(156, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(6, 45)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 16)
        Me.Label1.TabIndex = 228
        Me.Label1.Text = "Last ip:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(126, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(156, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(84, 46)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(67, 15)
        Me.Label3.TabIndex = 229
        Me.Label3.Text = "127.0.0.1"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(126, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(156, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(6, 68)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(79, 16)
        Me.Label4.TabIndex = 230
        Me.Label4.Text = "Last login:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(126, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(156, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(84, 69)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(141, 15)
        Me.Label5.TabIndex = 231
        Me.Label5.Text = "2012-10-28 19:15:50"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(438, 44)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(52, 16)
        Me.Label6.TabIndex = 232
        Me.Label6.Text = "Online"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(126, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(156, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(262, 114)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(84, 16)
        Me.Label7.TabIndex = 234
        Me.Label7.Text = "Expansion:"
        '
        'port_ud
        '
        Me.port_ud.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.port_ud.Location = New System.Drawing.Point(346, 112)
        Me.port_ud.Maximum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.port_ud.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.port_ud.Name = "port_ud"
        Me.port_ud.Size = New System.Drawing.Size(37, 21)
        Me.port_ud.TabIndex = 235
        Me.port_ud.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.port_ud.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(126, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(156, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(262, 139)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(51, 16)
        Me.Label8.TabIndex = 236
        Me.Label8.Text = "Email:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(126, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(156, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(319, 139)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(157, 15)
        Me.Label9.TabIndex = 237
        Me.Label9.Text = "admin@yourserver.com"
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.BackColor = System.Drawing.Color.Transparent
        Me.CheckBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold)
        Me.CheckBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(126, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(156, Byte), Integer))
        Me.CheckBox1.Location = New System.Drawing.Point(265, 89)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(118, 20)
        Me.CheckBox1.TabIndex = 238
        Me.CheckBox1.Text = "Lock account"
        Me.CheckBox1.UseVisualStyleBackColor = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(126, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(156, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(13, 174)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(87, 16)
        Me.Label10.TabIndex = 239
        Me.Label10.Text = "Characters:"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.level_lbl)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(126, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(156, Byte), Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(22, 68)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(235, 93)
        Me.GroupBox1.TabIndex = 240
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "General"
        '
        'reset_bt
        '
        Me.reset_bt.BackColor = System.Drawing.Color.DimGray
        Me.reset_bt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.reset_bt.Enabled = False
        Me.reset_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.reset_bt.ForeColor = System.Drawing.Color.Black
        Me.reset_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.reset_bt.Location = New System.Drawing.Point(12, 396)
        Me.reset_bt.Name = "reset_bt"
        Me.reset_bt.Size = New System.Drawing.Size(155, 34)
        Me.reset_bt.TabIndex = 243
        Me.reset_bt.Text = "Reset"
        Me.reset_bt.UseVisualStyleBackColor = False
        '
        'savechanges_bt
        '
        Me.savechanges_bt.BackColor = System.Drawing.Color.DimGray
        Me.savechanges_bt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.savechanges_bt.Enabled = False
        Me.savechanges_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.savechanges_bt.ForeColor = System.Drawing.Color.Black
        Me.savechanges_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.savechanges_bt.Location = New System.Drawing.Point(173, 396)
        Me.savechanges_bt.Name = "savechanges_bt"
        Me.savechanges_bt.Size = New System.Drawing.Size(155, 34)
        Me.savechanges_bt.TabIndex = 242
        Me.savechanges_bt.Text = "Save changes"
        Me.savechanges_bt.UseVisualStyleBackColor = False
        '
        'exit_bt
        '
        Me.exit_bt.BackColor = System.Drawing.Color.DimGray
        Me.exit_bt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.exit_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.exit_bt.ForeColor = System.Drawing.Color.Black
        Me.exit_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.exit_bt.Location = New System.Drawing.Point(335, 396)
        Me.exit_bt.Name = "exit_bt"
        Me.exit_bt.Size = New System.Drawing.Size(155, 34)
        Me.exit_bt.TabIndex = 241
        Me.exit_bt.Text = "Exit"
        Me.exit_bt.UseVisualStyleBackColor = False
        '
        'AccountOverview
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.Namcore_Studio.My.Resources.Resources.cleanbg
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(501, 442)
        Me.Controls.Add(Me.reset_bt)
        Me.Controls.Add(Me.savechanges_bt)
        Me.Controls.Add(Me.exit_bt)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.port_ud)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.characterview)
        Me.Controls.Add(Me.charname_lbl)
        Me.Controls.Add(Me.header)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "AccountOverview"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AccountOverview"
        Me.header.ResumeLayout(False)
        Me.closepanel.ResumeLayout(False)
        CType(Me.highlighter1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.highlighter2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.port_ud, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents header As System.Windows.Forms.Panel
    Friend WithEvents closepanel As System.Windows.Forms.Panel
    Friend WithEvents highlighter1 As System.Windows.Forms.PictureBox
    Friend WithEvents highlighter2 As System.Windows.Forms.PictureBox
    Friend WithEvents charname_lbl As System.Windows.Forms.Label
    Friend WithEvents characterview As System.Windows.Forms.ListView
    Friend WithEvents charguid As System.Windows.Forms.ColumnHeader
    Friend WithEvents caccid As System.Windows.Forms.ColumnHeader
    Friend WithEvents charname As System.Windows.Forms.ColumnHeader
    Friend WithEvents charrace As System.Windows.Forms.ColumnHeader
    Friend WithEvents charclass As System.Windows.Forms.ColumnHeader
    Friend WithEvents chargender As System.Windows.Forms.ColumnHeader
    Friend WithEvents charlevel As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents level_lbl As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents port_ud As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents reset_bt As System.Windows.Forms.Button
    Friend WithEvents savechanges_bt As System.Windows.Forms.Button
    Friend WithEvents exit_bt As System.Windows.Forms.Button
End Class
