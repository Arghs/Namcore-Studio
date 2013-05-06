<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Armory_interface
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.addURL_bt = New System.Windows.Forms.Button()
        Me.url_tb = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.charname = New System.Windows.Forms.TextBox()
        Me.realmname = New System.Windows.Forms.TextBox()
        Me.globalregion = New System.Windows.Forms.ComboBox()
        Me.addChar_bt = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.char_lst = New System.Windows.Forms.ListView()
        Me.lstvregion = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Realm = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Charactername = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.hyperlink = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.load_bt = New System.Windows.Forms.Button()
        Me.back_bt = New System.Windows.Forms.Button()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.removeItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.addURL_bt)
        Me.GroupBox1.Controls.Add(Me.url_tb)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.charname)
        Me.GroupBox1.Controls.Add(Me.realmname)
        Me.GroupBox1.Controls.Add(Me.globalregion)
        Me.GroupBox1.Controls.Add(Me.addChar_bt)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.GroupBox1.ForeColor = System.Drawing.Color.RoyalBlue
        Me.GroupBox1.Location = New System.Drawing.Point(12, 193)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(696, 98)
        Me.GroupBox1.TabIndex = 166
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Enter link or specify character"
        '
        'addURL_bt
        '
        Me.addURL_bt.BackColor = System.Drawing.Color.DimGray
        Me.addURL_bt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.addURL_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.addURL_bt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.addURL_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.addURL_bt.Location = New System.Drawing.Point(648, 61)
        Me.addURL_bt.Name = "addURL_bt"
        Me.addURL_bt.Size = New System.Drawing.Size(42, 26)
        Me.addURL_bt.TabIndex = 166
        Me.addURL_bt.Text = "Add"
        Me.addURL_bt.UseVisualStyleBackColor = False
        '
        'url_tb
        '
        Me.url_tb.Location = New System.Drawing.Point(70, 64)
        Me.url_tb.Name = "url_tb"
        Me.url_tb.Size = New System.Drawing.Size(560, 21)
        Me.url_tb.TabIndex = 165
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label5.ForeColor = System.Drawing.Color.RoyalBlue
        Me.Label5.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label5.Location = New System.Drawing.Point(7, 67)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(39, 15)
        Me.Label5.TabIndex = 164
        Me.Label5.Text = "URL:"
        '
        'charname
        '
        Me.charname.Location = New System.Drawing.Point(504, 25)
        Me.charname.Name = "charname"
        Me.charname.Size = New System.Drawing.Size(126, 21)
        Me.charname.TabIndex = 162
        '
        'realmname
        '
        Me.realmname.Location = New System.Drawing.Point(266, 25)
        Me.realmname.Name = "realmname"
        Me.realmname.Size = New System.Drawing.Size(117, 21)
        Me.realmname.TabIndex = 161
        '
        'globalregion
        '
        Me.globalregion.FormattingEnabled = True
        Me.globalregion.Items.AddRange(New Object() {"EU", "US", "KR", "TW"})
        Me.globalregion.Location = New System.Drawing.Point(70, 25)
        Me.globalregion.Name = "globalregion"
        Me.globalregion.Size = New System.Drawing.Size(95, 23)
        Me.globalregion.TabIndex = 160
        Me.globalregion.Text = "Select"
        '
        'addChar_bt
        '
        Me.addChar_bt.BackColor = System.Drawing.Color.DimGray
        Me.addChar_bt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.addChar_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.addChar_bt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.addChar_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.addChar_bt.Location = New System.Drawing.Point(648, 22)
        Me.addChar_bt.Name = "addChar_bt"
        Me.addChar_bt.Size = New System.Drawing.Size(42, 26)
        Me.addChar_bt.TabIndex = 163
        Me.addChar_bt.Text = "Add"
        Me.addChar_bt.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.ForeColor = System.Drawing.Color.RoyalBlue
        Me.Label4.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label4.Location = New System.Drawing.Point(389, 28)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(109, 15)
        Me.Label4.TabIndex = 163
        Me.Label4.Text = "Charactername:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label3.ForeColor = System.Drawing.Color.RoyalBlue
        Me.Label3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label3.Location = New System.Drawing.Point(171, 28)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(89, 15)
        Me.Label3.TabIndex = 161
        Me.Label3.Text = "Realmname:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.ForeColor = System.Drawing.Color.RoyalBlue
        Me.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label2.Location = New System.Drawing.Point(7, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 15)
        Me.Label2.TabIndex = 159
        Me.Label2.Text = "Region:"
        '
        'char_lst
        '
        Me.char_lst.BackColor = System.Drawing.SystemColors.Control
        Me.char_lst.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.lstvregion, Me.Realm, Me.Charactername, Me.hyperlink})
        Me.char_lst.FullRowSelect = True
        Me.char_lst.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.char_lst.Location = New System.Drawing.Point(12, 33)
        Me.char_lst.MultiSelect = False
        Me.char_lst.Name = "char_lst"
        Me.char_lst.Size = New System.Drawing.Size(696, 154)
        Me.char_lst.TabIndex = 165
        Me.char_lst.UseCompatibleStateImageBehavior = False
        Me.char_lst.View = System.Windows.Forms.View.Details
        '
        'lstvregion
        '
        Me.lstvregion.Text = "Region"
        Me.lstvregion.Width = 59
        '
        'Realm
        '
        Me.Realm.Text = "Realm"
        Me.Realm.Width = 231
        '
        'Charactername
        '
        Me.Charactername.Text = "Charactername"
        Me.Charactername.Width = 378
        '
        'hyperlink
        '
        Me.hyperlink.Text = ""
        Me.hyperlink.Width = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.Color.RoyalBlue
        Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label1.Location = New System.Drawing.Point(9, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(148, 15)
        Me.Label1.TabIndex = 164
        Me.Label1.Text = "Add characters below:"
        '
        'load_bt
        '
        Me.load_bt.BackColor = System.Drawing.Color.DimGray
        Me.load_bt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.load_bt.Enabled = False
        Me.load_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.load_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.load_bt.Location = New System.Drawing.Point(251, 297)
        Me.load_bt.Name = "load_bt"
        Me.load_bt.Size = New System.Drawing.Size(213, 48)
        Me.load_bt.TabIndex = 163
        Me.load_bt.Text = "Load characters" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.load_bt.UseVisualStyleBackColor = False
        '
        'back_bt
        '
        Me.back_bt.BackColor = System.Drawing.Color.DimGray
        Me.back_bt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.back_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.back_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.back_bt.Location = New System.Drawing.Point(576, 318)
        Me.back_bt.Name = "back_bt"
        Me.back_bt.Size = New System.Drawing.Size(126, 27)
        Me.back_bt.TabIndex = 160
        Me.back_bt.Text = "Back"
        Me.back_bt.UseVisualStyleBackColor = False
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.removeItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(118, 26)
        '
        'removeItem
        '
        Me.removeItem.Name = "removeItem"
        Me.removeItem.Size = New System.Drawing.Size(117, 22)
        Me.removeItem.Text = "Remove"
        '
        'RichTextBox1
        '
        Me.RichTextBox1.Location = New System.Drawing.Point(66, 312)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(100, 96)
        Me.RichTextBox1.TabIndex = 167
        Me.RichTextBox1.Text = ""
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(172, 312)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 168
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Armory_interface
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(718, 352)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.RichTextBox1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.char_lst)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.load_bt)
        Me.Controls.Add(Me.back_bt)
        Me.Name = "Armory_interface"
        Me.Text = "Load armory characters"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents addURL_bt As System.Windows.Forms.Button
    Friend WithEvents url_tb As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents charname As System.Windows.Forms.TextBox
    Friend WithEvents realmname As System.Windows.Forms.TextBox
    Friend WithEvents globalregion As System.Windows.Forms.ComboBox
    Friend WithEvents addChar_bt As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents char_lst As System.Windows.Forms.ListView
    Friend WithEvents lstvregion As System.Windows.Forms.ColumnHeader
    Friend WithEvents Realm As System.Windows.Forms.ColumnHeader
    Friend WithEvents Charactername As System.Windows.Forms.ColumnHeader
    Friend WithEvents hyperlink As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents load_bt As System.Windows.Forms.Button
    Friend WithEvents back_bt As System.Windows.Forms.Button
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents removeItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RichTextBox1 As System.Windows.Forms.RichTextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
