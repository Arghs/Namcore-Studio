<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Quests_interface
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Quests_interface))
        Me.qst_lst = New System.Windows.Forms.ListView()
        Me.qstid = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.qstname = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.finished = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.rewarded = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.header = New System.Windows.Forms.Panel()
        Me.closepanel = New System.Windows.Forms.Panel()
        Me.highlighter1 = New System.Windows.Forms.PictureBox()
        Me.highlighter2 = New System.Windows.Forms.PictureBox()
        Me.qstContext = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.RemoveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToggleFinishedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.finished_0 = New System.Windows.Forms.ToolStripMenuItem()
        Me.finished_1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToggleRewardedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.rewarded_0 = New System.Windows.Forms.ToolStripMenuItem()
        Me.rewarded_1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.add_bt = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.header.SuspendLayout
        Me.closepanel.SuspendLayout
        CType(Me.highlighter1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.highlighter2,System.ComponentModel.ISupportInitialize).BeginInit
        Me.qstContext.SuspendLayout
        Me.SuspendLayout
        '
        'qst_lst
        '
        Me.qst_lst.BackColor = System.Drawing.Color.FromArgb(CType(CType(139,Byte),Integer), CType(CType(158,Byte),Integer), CType(CType(172,Byte),Integer))
        Me.qst_lst.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.qstid, Me.qstname, Me.finished, Me.rewarded})
        Me.qst_lst.FullRowSelect = true
        Me.qst_lst.Location = New System.Drawing.Point(4, 81)
        Me.qst_lst.Name = "qst_lst"
        Me.qst_lst.Size = New System.Drawing.Size(694, 447)
        Me.qst_lst.TabIndex = 0
        Me.qst_lst.UseCompatibleStateImageBehavior = false
        Me.qst_lst.View = System.Windows.Forms.View.Details
        '
        'qstid
        '
        Me.qstid.Text = "Quest ID"
        Me.qstid.Width = 67
        '
        'qstname
        '
        Me.qstname.Text = "Name"
        Me.qstname.Width = 400
        '
        'finished
        '
        Me.finished.Text = "Finished"
        Me.finished.Width = 106
        '
        'rewarded
        '
        Me.rewarded.Text = "Rewarded"
        Me.rewarded.Width = 70
        '
        'header
        '
        Me.header.BackgroundImage = Global.Namcore_Studio.My.Resources.Resources.namcore_header
        Me.header.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.header.Controls.Add(Me.closepanel)
        Me.header.Location = New System.Drawing.Point(4, 5)
        Me.header.Name = "header"
        Me.header.Size = New System.Drawing.Size(694, 30)
        Me.header.TabIndex = 225
        '
        'closepanel
        '
        Me.closepanel.BackgroundImage = Global.Namcore_Studio.My.Resources.Resources.minclose
        Me.closepanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.closepanel.Controls.Add(Me.highlighter1)
        Me.closepanel.Controls.Add(Me.highlighter2)
        Me.closepanel.Location = New System.Drawing.Point(636, 0)
        Me.closepanel.Name = "closepanel"
        Me.closepanel.Size = New System.Drawing.Size(56, 28)
        Me.closepanel.TabIndex = 1
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
        Me.highlighter1.TabStop = false
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
        Me.highlighter2.TabStop = false
        '
        'qstContext
        '
        Me.qstContext.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RemoveToolStripMenuItem, Me.ToggleFinishedToolStripMenuItem, Me.ToggleRewardedToolStripMenuItem})
        Me.qstContext.Name = "qstContext"
        Me.qstContext.Size = New System.Drawing.Size(201, 70)
        '
        'RemoveToolStripMenuItem
        '
        Me.RemoveToolStripMenuItem.Name = "RemoveToolStripMenuItem"
        Me.RemoveToolStripMenuItem.Size = New System.Drawing.Size(200, 22)
        Me.RemoveToolStripMenuItem.Text = "Remove selected quests"
        '
        'ToggleFinishedToolStripMenuItem
        '
        Me.ToggleFinishedToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.finished_0, Me.finished_1})
        Me.ToggleFinishedToolStripMenuItem.Name = "ToggleFinishedToolStripMenuItem"
        Me.ToggleFinishedToolStripMenuItem.Size = New System.Drawing.Size(200, 22)
        Me.ToggleFinishedToolStripMenuItem.Text = "Set 'Finished' status"
        '
        'finished_0
        '
        Me.finished_0.Name = "finished_0"
        Me.finished_0.Size = New System.Drawing.Size(152, 22)
        Me.finished_0.Text = "0"
        '
        'finished_1
        '
        Me.finished_1.Name = "finished_1"
        Me.finished_1.Size = New System.Drawing.Size(152, 22)
        Me.finished_1.Text = "1"
        '
        'ToggleRewardedToolStripMenuItem
        '
        Me.ToggleRewardedToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.rewarded_0, Me.rewarded_1})
        Me.ToggleRewardedToolStripMenuItem.Name = "ToggleRewardedToolStripMenuItem"
        Me.ToggleRewardedToolStripMenuItem.Size = New System.Drawing.Size(200, 22)
        Me.ToggleRewardedToolStripMenuItem.Text = "Set 'Rewarded' status"
        '
        'rewarded_0
        '
        Me.rewarded_0.Name = "rewarded_0"
        Me.rewarded_0.Size = New System.Drawing.Size(152, 22)
        Me.rewarded_0.Text = "0"
        '
        'rewarded_1
        '
        Me.rewarded_1.Name = "rewarded_1"
        Me.rewarded_1.Size = New System.Drawing.Size(152, 22)
        Me.rewarded_1.Text = "1"
        '
        'add_bt
        '
        Me.add_bt.BackColor = System.Drawing.Color.DimGray
        Me.add_bt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.add_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.add_bt.ForeColor = System.Drawing.Color.Black
        Me.add_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.add_bt.Location = New System.Drawing.Point(9, 41)
        Me.add_bt.Name = "add_bt"
        Me.add_bt.Size = New System.Drawing.Size(155, 34)
        Me.add_bt.TabIndex = 227
        Me.add_bt.Text = "Add"
        Me.add_bt.UseVisualStyleBackColor = false
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(139,Byte),Integer), CType(CType(158,Byte),Integer), CType(CType(172,Byte),Integer))
        Me.Label1.Location = New System.Drawing.Point(462, 59)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(234, 16)
        Me.Label1.TabIndex = 228
        Me.Label1.Text = "Right click to open context menu!"
        '
        'Quests_interface
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.Namcore_Studio.My.Resources.Resources.cleanbg
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(701, 533)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.add_bt)
        Me.Controls.Add(Me.header)
        Me.Controls.Add(Me.qst_lst)
        Me.DoubleBuffered = true
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Name = "Quests_interface"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Quests_interface"
        Me.header.ResumeLayout(false)
        Me.closepanel.ResumeLayout(false)
        CType(Me.highlighter1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.highlighter2,System.ComponentModel.ISupportInitialize).EndInit
        Me.qstContext.ResumeLayout(false)
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents qst_lst As System.Windows.Forms.ListView
    Friend WithEvents qstid As System.Windows.Forms.ColumnHeader
    Friend WithEvents qstname As System.Windows.Forms.ColumnHeader
    Friend WithEvents finished As System.Windows.Forms.ColumnHeader
    Friend WithEvents rewarded As System.Windows.Forms.ColumnHeader
    Friend WithEvents header As System.Windows.Forms.Panel
    Friend WithEvents closepanel As System.Windows.Forms.Panel
    Friend WithEvents highlighter1 As System.Windows.Forms.PictureBox
    Friend WithEvents highlighter2 As System.Windows.Forms.PictureBox
    Friend WithEvents qstContext As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents RemoveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToggleFinishedToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents finished_0 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents finished_1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToggleRewardedToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents rewarded_0 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents rewarded_1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents add_bt As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
