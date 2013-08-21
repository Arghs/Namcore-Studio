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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Quests_interface))
        Me.qst_lst = New System.Windows.Forms.ListView()
        Me.qstid = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.qstname = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.finished = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.rewarded = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.header = New System.Windows.Forms.Panel()
        Me.closepanel = New System.Windows.Forms.Panel()
        Me.highlighter1 = New System.Windows.Forms.PictureBox()
        Me.highlighter2 = New System.Windows.Forms.PictureBox()
        Me.header.SuspendLayout()
        Me.closepanel.SuspendLayout()
        CType(Me.highlighter1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.highlighter2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'qst_lst
        '
        Me.qst_lst.BackColor = System.Drawing.Color.FromArgb(CType(CType(139, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(172, Byte), Integer))
        Me.qst_lst.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.qstid, Me.qstname, Me.finished, Me.rewarded})
        Me.qst_lst.FullRowSelect = True
        Me.qst_lst.Location = New System.Drawing.Point(4, 34)
        Me.qst_lst.Name = "qst_lst"
        Me.qst_lst.Size = New System.Drawing.Size(694, 494)
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
        'Quests_interface
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.Namcore_Studio.My.Resources.Resources.cleanbg
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(701, 533)
        Me.Controls.Add(Me.header)
        Me.Controls.Add(Me.qst_lst)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Quests_interface"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Quests_interface"
        Me.header.ResumeLayout(False)
        Me.closepanel.ResumeLayout(False)
        CType(Me.highlighter1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.highlighter2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

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
End Class
