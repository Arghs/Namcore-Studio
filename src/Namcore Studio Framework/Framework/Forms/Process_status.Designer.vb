Namespace Framework.Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class ProcessStatus
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ProcessStatus))
            Me.close_bt = New System.Windows.Forms.Button()
            Me.cancel_bt = New System.Windows.Forms.Button()
            Me.header = New System.Windows.Forms.Panel()
            Me.closepanel = New System.Windows.Forms.Panel()
            Me.highlighter1 = New System.Windows.Forms.PictureBox()
            Me.highlighter2 = New System.Windows.Forms.PictureBox()
            Me.fctb = New FastColoredTextBoxNS.FastColoredTextBox()
            Me.header.SuspendLayout()
            Me.closepanel.SuspendLayout()
            CType(Me.highlighter1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.highlighter2, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.fctb, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'close_bt
            '
            Me.close_bt.BackColor = System.Drawing.Color.DimGray
            Me.close_bt.Cursor = System.Windows.Forms.Cursors.Hand
            Me.close_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.close_bt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.close_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.close_bt.Location = New System.Drawing.Point(13, 294)
            Me.close_bt.Name = "close_bt"
            Me.close_bt.Size = New System.Drawing.Size(198, 42)
            Me.close_bt.TabIndex = 209
            Me.close_bt.Text = "Close"
            Me.close_bt.UseVisualStyleBackColor = False
            '
            'cancel_bt
            '
            Me.cancel_bt.BackColor = System.Drawing.Color.DimGray
            Me.cancel_bt.Cursor = System.Windows.Forms.Cursors.Hand
            Me.cancel_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.cancel_bt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.cancel_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.cancel_bt.Location = New System.Drawing.Point(413, 294)
            Me.cancel_bt.Name = "cancel_bt"
            Me.cancel_bt.Size = New System.Drawing.Size(198, 42)
            Me.cancel_bt.TabIndex = 211
            Me.cancel_bt.Text = "Cancel"
            Me.cancel_bt.UseVisualStyleBackColor = False
            '
            'header
            '
            Me.header.BackgroundImage = Global.NCFramework.My.Resources.Resources.namcore_header_new
            Me.header.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
            Me.header.Controls.Add(Me.closepanel)
            Me.header.Location = New System.Drawing.Point(3, 2)
            Me.header.Name = "header"
            Me.header.Size = New System.Drawing.Size(616, 30)
            Me.header.TabIndex = 222
            '
            'closepanel
            '
            Me.closepanel.BackColor = System.Drawing.Color.Transparent
            Me.closepanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
            Me.closepanel.Controls.Add(Me.highlighter1)
            Me.closepanel.Controls.Add(Me.highlighter2)
            Me.closepanel.Location = New System.Drawing.Point(552, 0)
            Me.closepanel.Name = "closepanel"
            Me.closepanel.Size = New System.Drawing.Size(62, 28)
            Me.closepanel.TabIndex = 0
            '
            'highlighter1
            '
            Me.highlighter1.BackColor = System.Drawing.Color.Transparent
            Me.highlighter1.BackgroundImage = Global.NCFramework.My.Resources.Resources.bt_minimize
            Me.highlighter1.Cursor = System.Windows.Forms.Cursors.Hand
            Me.highlighter1.Location = New System.Drawing.Point(5, 5)
            Me.highlighter1.Name = "highlighter1"
            Me.highlighter1.Size = New System.Drawing.Size(25, 20)
            Me.highlighter1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.highlighter1.TabIndex = 217
            Me.highlighter1.TabStop = False
            '
            'highlighter2
            '
            Me.highlighter2.BackColor = System.Drawing.Color.Transparent
            Me.highlighter2.BackgroundImage = Global.NCFramework.My.Resources.Resources.bt_close
            Me.highlighter2.Cursor = System.Windows.Forms.Cursors.Hand
            Me.highlighter2.Location = New System.Drawing.Point(33, 5)
            Me.highlighter2.Name = "highlighter2"
            Me.highlighter2.Size = New System.Drawing.Size(25, 20)
            Me.highlighter2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.highlighter2.TabIndex = 218
            Me.highlighter2.TabStop = False
            '
            'fctb
            '
            Me.fctb.AutoCompleteBracketsList = New Char() {Global.Microsoft.VisualBasic.ChrW(40), Global.Microsoft.VisualBasic.ChrW(41), Global.Microsoft.VisualBasic.ChrW(123), Global.Microsoft.VisualBasic.ChrW(125), Global.Microsoft.VisualBasic.ChrW(91), Global.Microsoft.VisualBasic.ChrW(93), Global.Microsoft.VisualBasic.ChrW(34), Global.Microsoft.VisualBasic.ChrW(34), Global.Microsoft.VisualBasic.ChrW(39), Global.Microsoft.VisualBasic.ChrW(39)}
            Me.fctb.AutoScrollMinSize = New System.Drawing.Size(25, 15)
            Me.fctb.BackBrush = Nothing
            Me.fctb.BackColor = System.Drawing.Color.DimGray
            Me.fctb.CharHeight = 15
            Me.fctb.CharWidth = 7
            Me.fctb.Cursor = System.Windows.Forms.Cursors.IBeam
            Me.fctb.DisabledColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer))
            Me.fctb.Font = New System.Drawing.Font("Consolas", 9.75!)
            Me.fctb.IndentBackColor = System.Drawing.Color.FromArgb(CType(CType(105, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(158, Byte), Integer))
            Me.fctb.IsReplaceMode = False
            Me.fctb.LineNumberColor = System.Drawing.Color.Black
            Me.fctb.Location = New System.Drawing.Point(2, 31)
            Me.fctb.Name = "fctb"
            Me.fctb.Paddings = New System.Windows.Forms.Padding(0)
            Me.fctb.ReadOnly = True
            Me.fctb.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.fctb.Size = New System.Drawing.Size(617, 257)
            Me.fctb.TabIndex = 5
            Me.fctb.Zoom = 100
            '
            'ProcessStatus
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackgroundImage = Global.NCFramework.My.Resources.Resources.HUD_bg
            Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.ClientSize = New System.Drawing.Size(622, 343)
            Me.Controls.Add(Me.fctb)
            Me.Controls.Add(Me.header)
            Me.Controls.Add(Me.cancel_bt)
            Me.Controls.Add(Me.close_bt)
            Me.DoubleBuffered = True
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "ProcessStatus"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Process_status"
            Me.TopMost = True
            Me.header.ResumeLayout(False)
            Me.closepanel.ResumeLayout(False)
            CType(Me.highlighter1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.highlighter2, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.fctb, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents close_bt As System.Windows.Forms.Button
        Friend WithEvents cancel_bt As System.Windows.Forms.Button
        Friend WithEvents header As System.Windows.Forms.Panel
        Friend WithEvents closepanel As System.Windows.Forms.Panel
        Friend WithEvents highlighter1 As System.Windows.Forms.PictureBox
        Friend WithEvents highlighter2 As System.Windows.Forms.PictureBox
        Private WithEvents fctb As FastColoredTextBoxNS.FastColoredTextBox
    End Class
End Namespace