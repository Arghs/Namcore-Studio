Imports Namcore_Studio.Forms.Extension

Namespace Forms.Character
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class QuestsInterface
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(QuestsInterface))
            Me.qst_lst = New System.Windows.Forms.ListView()
            Me.qstid = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.qstname = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.finished = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.rewarded = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
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
            Me.search_tb = New System.Windows.Forms.TextBox()
            Me.resultstatus_lbl = New System.Windows.Forms.Label()
            Me.qstContext.SuspendLayout()
            Me.SuspendLayout()
            '
            'qst_lst
            '
            Me.qst_lst.BackColor = System.Drawing.Color.FromArgb(CType(CType(126, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(156, Byte), Integer))
            Me.qst_lst.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.qstid, Me.qstname, Me.finished, Me.rewarded})
            Me.qst_lst.FullRowSelect = True
            Me.qst_lst.Location = New System.Drawing.Point(4, 122)
            Me.qst_lst.Name = "qst_lst"
            Me.qst_lst.Size = New System.Drawing.Size(694, 447)
            Me.qst_lst.TabIndex = 0
            Me.qst_lst.UseCompatibleStateImageBehavior = False
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
            Me.finished_0.Size = New System.Drawing.Size(80, 22)
            Me.finished_0.Text = "0"
            '
            'finished_1
            '
            Me.finished_1.Name = "finished_1"
            Me.finished_1.Size = New System.Drawing.Size(80, 22)
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
            Me.rewarded_0.Size = New System.Drawing.Size(80, 22)
            Me.rewarded_0.Text = "0"
            '
            'rewarded_1
            '
            Me.rewarded_1.Name = "rewarded_1"
            Me.rewarded_1.Size = New System.Drawing.Size(80, 22)
            Me.rewarded_1.Text = "1"
            '
            'add_bt
            '
            Me.add_bt.BackColor = System.Drawing.Color.DimGray
            Me.add_bt.Cursor = System.Windows.Forms.Cursors.Hand
            Me.add_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.add_bt.ForeColor = System.Drawing.Color.Black
            Me.add_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.add_bt.Location = New System.Drawing.Point(9, 83)
            Me.add_bt.Name = "add_bt"
            Me.add_bt.Size = New System.Drawing.Size(155, 34)
            Me.add_bt.TabIndex = 227
            Me.add_bt.Text = "Add"
            Me.add_bt.UseVisualStyleBackColor = False
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.BackColor = System.Drawing.Color.Transparent
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.ForeColor = System.Drawing.Color.Black
            Me.Label1.Location = New System.Drawing.Point(462, 101)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(234, 16)
            Me.Label1.TabIndex = 228
            Me.Label1.Text = "Right click to open context menu!"
            '
            'search_tb
            '
            Me.search_tb.ForeColor = System.Drawing.SystemColors.WindowFrame
            Me.search_tb.Location = New System.Drawing.Point(170, 97)
            Me.search_tb.Name = "search_tb"
            Me.search_tb.Size = New System.Drawing.Size(100, 20)
            Me.search_tb.TabIndex = 229
            Me.search_tb.Text = "Enter quest id"
            '
            'resultstatus_lbl
            '
            Me.resultstatus_lbl.AutoSize = True
            Me.resultstatus_lbl.BackColor = System.Drawing.Color.Transparent
            Me.resultstatus_lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.resultstatus_lbl.ForeColor = System.Drawing.Color.Black
            Me.resultstatus_lbl.Location = New System.Drawing.Point(276, 101)
            Me.resultstatus_lbl.Name = "resultstatus_lbl"
            Me.resultstatus_lbl.Size = New System.Drawing.Size(78, 16)
            Me.resultstatus_lbl.TabIndex = 230
            Me.resultstatus_lbl.Text = "No results"
            '
            'QuestsInterface
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackgroundImage = Global.Namcore_Studio.My.Resources.Resources.HUD_bg
            Me.ClientSize = New System.Drawing.Size(701, 572)
            Me.Controls.Add(Me.resultstatus_lbl)
            Me.Controls.Add(Me.search_tb)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.add_bt)
            Me.Controls.Add(Me.qst_lst)
            Me.DoubleBuffered = True
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "QuestsInterface"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Quests_interface"
            Me.Controls.SetChildIndex(Me.qst_lst, 0)
            Me.Controls.SetChildIndex(Me.add_bt, 0)
            Me.Controls.SetChildIndex(Me.Label1, 0)
            Me.Controls.SetChildIndex(Me.search_tb, 0)
            Me.Controls.SetChildIndex(Me.resultstatus_lbl, 0)
            Me.qstContext.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents qst_lst As System.Windows.Forms.ListView
        Friend WithEvents qstid As System.Windows.Forms.ColumnHeader
        Friend WithEvents qstname As System.Windows.Forms.ColumnHeader
        Friend WithEvents finished As System.Windows.Forms.ColumnHeader
        Friend WithEvents rewarded As System.Windows.Forms.ColumnHeader
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
        Friend WithEvents search_tb As System.Windows.Forms.TextBox
        Friend WithEvents resultstatus_lbl As System.Windows.Forms.Label
    End Class
End Namespace