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
        Me.avid = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.avname = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.avcat = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.avgained = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.avdescription = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.avcatsub = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.resultcnt_lbl = New System.Windows.Forms.Label()
        Me.nxt100_bt = New System.Windows.Forms.Button()
        Me.prev100_bt = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'avcompleted_lst
        '
        Me.avcompleted_lst.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.avid, Me.avname, Me.avcat, Me.avcatsub, Me.avgained, Me.avdescription})
        Me.avcompleted_lst.Location = New System.Drawing.Point(12, 56)
        Me.avcompleted_lst.Name = "avcompleted_lst"
        Me.avcompleted_lst.Size = New System.Drawing.Size(899, 397)
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
        'avgained
        '
        Me.avgained.Text = "Gain Date"
        Me.avgained.Width = 129
        '
        'avdescription
        '
        Me.avdescription.Text = "Description"
        Me.avdescription.Width = 289
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"Spieler gegen Spieler", "Heldentaten", "Allgemein", "Quests", "Erkundung", "Dungeons & Schlachtzüge", "Berufe", "Ruf"})
        Me.ComboBox1.Location = New System.Drawing.Point(484, 29)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(175, 21)
        Me.ComboBox1.TabIndex = 1
        '
        'avcatsub
        '
        Me.avcatsub.Text = "Subcategory"
        Me.avcatsub.Width = 161
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
        'Achievements_interface
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(925, 465)
        Me.Controls.Add(Me.prev100_bt)
        Me.Controls.Add(Me.nxt100_bt)
        Me.Controls.Add(Me.resultcnt_lbl)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.avcompleted_lst)
        Me.Name = "Achievements_interface"
        Me.Text = "Achievements_interface"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents avcompleted_lst As System.Windows.Forms.ListView
    Friend WithEvents avid As System.Windows.Forms.ColumnHeader
    Friend WithEvents avname As System.Windows.Forms.ColumnHeader
    Friend WithEvents avcat As System.Windows.Forms.ColumnHeader
    Friend WithEvents avgained As System.Windows.Forms.ColumnHeader
    Friend WithEvents avdescription As System.Windows.Forms.ColumnHeader
    Friend WithEvents avcatsub As System.Windows.Forms.ColumnHeader
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents resultcnt_lbl As System.Windows.Forms.Label
    Friend WithEvents nxt100_bt As System.Windows.Forms.Button
    Friend WithEvents prev100_bt As System.Windows.Forms.Button
End Class
