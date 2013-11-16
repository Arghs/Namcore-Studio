Imports Namcore_Studio.Forms.Extension

Namespace Forms.Character
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class BankInterface
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BankInterface))
            Me.bag1Panel = New System.Windows.Forms.Panel()
            Me.bag1Pic = New System.Windows.Forms.PictureBox()
            Me.bag2Panel = New System.Windows.Forms.Panel()
            Me.bag2Pic = New System.Windows.Forms.PictureBox()
            Me.bag3Panel = New System.Windows.Forms.Panel()
            Me.bag3Pic = New System.Windows.Forms.PictureBox()
            Me.bag4Panel = New System.Windows.Forms.Panel()
            Me.PictureBox3 = New System.Windows.Forms.PictureBox()
            Me.bag4Pic = New System.Windows.Forms.PictureBox()
            Me.bag5Panel = New System.Windows.Forms.Panel()
            Me.bag5Pic = New System.Windows.Forms.PictureBox()
            Me.BankLayoutPanel = New System.Windows.Forms.FlowLayoutPanel()
            Me.bag6Panel = New System.Windows.Forms.Panel()
            Me.bag6Pic = New System.Windows.Forms.PictureBox()
            Me.bag7Panel = New System.Windows.Forms.Panel()
            Me.Bag7Pic = New System.Windows.Forms.PictureBox()
            Me.reference_itm_panel = New System.Windows.Forms.Panel()
            Me.reference_itm_pic = New System.Windows.Forms.PictureBox()
            Me.label2 = New System.Windows.Forms.Label()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.BagItemPanel = New System.Windows.Forms.FlowLayoutPanel()
            Me.referenceItmPanel = New System.Windows.Forms.Panel()
            Me.referenceItmPic = New System.Windows.Forms.PictureBox()
            Me.InfoToolTip = New System.Windows.Forms.ToolTip(Me.components)
            Me.BackPanel = New System.Windows.Forms.Panel()
            Me.bag1Panel.SuspendLayout()
            CType(Me.bag1Pic, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.bag2Panel.SuspendLayout()
            CType(Me.bag2Pic, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.bag3Panel.SuspendLayout()
            CType(Me.bag3Pic, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.bag4Panel.SuspendLayout()
            CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.bag4Pic, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.bag5Panel.SuspendLayout()
            CType(Me.bag5Pic, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.bag6Panel.SuspendLayout()
            CType(Me.bag6Pic, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.bag7Panel.SuspendLayout()
            CType(Me.Bag7Pic, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.reference_itm_panel.SuspendLayout()
            CType(Me.reference_itm_pic, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.referenceItmPanel.SuspendLayout()
            CType(Me.referenceItmPic, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.BackPanel.SuspendLayout()
            Me.SuspendLayout()
            '
            'bag1Panel
            '
            Me.bag1Panel.BackColor = System.Drawing.SystemColors.ActiveBorder
            Me.bag1Panel.Controls.Add(Me.bag1Pic)
            Me.bag1Panel.Location = New System.Drawing.Point(5, 291)
            Me.bag1Panel.Name = "bag1Panel"
            Me.bag1Panel.Size = New System.Drawing.Size(56, 56)
            Me.bag1Panel.TabIndex = 230
            '
            'bag1Pic
            '
            Me.bag1Pic.BackgroundImage = CType(resources.GetObject("bag1Pic.BackgroundImage"), System.Drawing.Image)
            Me.bag1Pic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.bag1Pic.Cursor = System.Windows.Forms.Cursors.Hand
            Me.bag1Pic.Location = New System.Drawing.Point(3, 3)
            Me.bag1Pic.Name = "bag1Pic"
            Me.bag1Pic.Size = New System.Drawing.Size(50, 50)
            Me.bag1Pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.bag1Pic.TabIndex = 0
            Me.bag1Pic.TabStop = False
            '
            'bag2Panel
            '
            Me.bag2Panel.BackColor = System.Drawing.SystemColors.ActiveBorder
            Me.bag2Panel.Controls.Add(Me.bag2Pic)
            Me.bag2Panel.Location = New System.Drawing.Point(62, 291)
            Me.bag2Panel.Name = "bag2Panel"
            Me.bag2Panel.Size = New System.Drawing.Size(56, 56)
            Me.bag2Panel.TabIndex = 231
            '
            'bag2Pic
            '
            Me.bag2Pic.BackgroundImage = CType(resources.GetObject("bag2Pic.BackgroundImage"), System.Drawing.Image)
            Me.bag2Pic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.bag2Pic.Cursor = System.Windows.Forms.Cursors.Hand
            Me.bag2Pic.Location = New System.Drawing.Point(3, 3)
            Me.bag2Pic.Name = "bag2Pic"
            Me.bag2Pic.Size = New System.Drawing.Size(50, 50)
            Me.bag2Pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.bag2Pic.TabIndex = 0
            Me.bag2Pic.TabStop = False
            '
            'bag3Panel
            '
            Me.bag3Panel.BackColor = System.Drawing.SystemColors.ActiveBorder
            Me.bag3Panel.Controls.Add(Me.bag3Pic)
            Me.bag3Panel.Location = New System.Drawing.Point(119, 291)
            Me.bag3Panel.Name = "bag3Panel"
            Me.bag3Panel.Size = New System.Drawing.Size(56, 56)
            Me.bag3Panel.TabIndex = 232
            '
            'bag3Pic
            '
            Me.bag3Pic.BackgroundImage = CType(resources.GetObject("bag3Pic.BackgroundImage"), System.Drawing.Image)
            Me.bag3Pic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.bag3Pic.Cursor = System.Windows.Forms.Cursors.Hand
            Me.bag3Pic.Location = New System.Drawing.Point(3, 3)
            Me.bag3Pic.Name = "bag3Pic"
            Me.bag3Pic.Size = New System.Drawing.Size(50, 50)
            Me.bag3Pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.bag3Pic.TabIndex = 0
            Me.bag3Pic.TabStop = False
            '
            'bag4Panel
            '
            Me.bag4Panel.BackColor = System.Drawing.SystemColors.ActiveBorder
            Me.bag4Panel.Controls.Add(Me.PictureBox3)
            Me.bag4Panel.Controls.Add(Me.bag4Pic)
            Me.bag4Panel.Location = New System.Drawing.Point(176, 291)
            Me.bag4Panel.Name = "bag4Panel"
            Me.bag4Panel.Size = New System.Drawing.Size(56, 56)
            Me.bag4Panel.TabIndex = 233
            '
            'PictureBox3
            '
            Me.PictureBox3.BackgroundImage = CType(resources.GetObject("PictureBox3.BackgroundImage"), System.Drawing.Image)
            Me.PictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.PictureBox3.Location = New System.Drawing.Point(-114, 4)
            Me.PictureBox3.Name = "PictureBox3"
            Me.PictureBox3.Size = New System.Drawing.Size(50, 50)
            Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.PictureBox3.TabIndex = 0
            Me.PictureBox3.TabStop = False
            '
            'bag4Pic
            '
            Me.bag4Pic.BackgroundImage = CType(resources.GetObject("bag4Pic.BackgroundImage"), System.Drawing.Image)
            Me.bag4Pic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.bag4Pic.Cursor = System.Windows.Forms.Cursors.Hand
            Me.bag4Pic.Location = New System.Drawing.Point(3, 3)
            Me.bag4Pic.Name = "bag4Pic"
            Me.bag4Pic.Size = New System.Drawing.Size(50, 50)
            Me.bag4Pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.bag4Pic.TabIndex = 0
            Me.bag4Pic.TabStop = False
            '
            'bag5Panel
            '
            Me.bag5Panel.BackColor = System.Drawing.SystemColors.ActiveBorder
            Me.bag5Panel.Controls.Add(Me.bag5Pic)
            Me.bag5Panel.Location = New System.Drawing.Point(233, 291)
            Me.bag5Panel.Name = "bag5Panel"
            Me.bag5Panel.Size = New System.Drawing.Size(56, 56)
            Me.bag5Panel.TabIndex = 234
            '
            'bag5Pic
            '
            Me.bag5Pic.BackgroundImage = CType(resources.GetObject("bag5Pic.BackgroundImage"), System.Drawing.Image)
            Me.bag5Pic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.bag5Pic.Cursor = System.Windows.Forms.Cursors.Hand
            Me.bag5Pic.Location = New System.Drawing.Point(3, 3)
            Me.bag5Pic.Name = "bag5Pic"
            Me.bag5Pic.Size = New System.Drawing.Size(50, 50)
            Me.bag5Pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.bag5Pic.TabIndex = 0
            Me.bag5Pic.TabStop = False
            '
            'BankLayoutPanel
            '
            Me.BankLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.BankLayoutPanel.BackColor = System.Drawing.Color.Transparent
            Me.BankLayoutPanel.Location = New System.Drawing.Point(4, 25)
            Me.BankLayoutPanel.MaximumSize = New System.Drawing.Size(500, 10000)
            Me.BankLayoutPanel.MinimumSize = New System.Drawing.Size(100, 10)
            Me.BankLayoutPanel.Name = "BankLayoutPanel"
            Me.BankLayoutPanel.Size = New System.Drawing.Size(406, 232)
            Me.BankLayoutPanel.TabIndex = 236
            '
            'bag6Panel
            '
            Me.bag6Panel.BackColor = System.Drawing.SystemColors.ActiveBorder
            Me.bag6Panel.Controls.Add(Me.bag6Pic)
            Me.bag6Panel.Location = New System.Drawing.Point(290, 291)
            Me.bag6Panel.Name = "bag6Panel"
            Me.bag6Panel.Size = New System.Drawing.Size(56, 56)
            Me.bag6Panel.TabIndex = 238
            '
            'bag6Pic
            '
            Me.bag6Pic.BackgroundImage = CType(resources.GetObject("bag6Pic.BackgroundImage"), System.Drawing.Image)
            Me.bag6Pic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.bag6Pic.Cursor = System.Windows.Forms.Cursors.Hand
            Me.bag6Pic.Location = New System.Drawing.Point(3, 3)
            Me.bag6Pic.Name = "bag6Pic"
            Me.bag6Pic.Size = New System.Drawing.Size(50, 50)
            Me.bag6Pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.bag6Pic.TabIndex = 0
            Me.bag6Pic.TabStop = False
            '
            'bag7Panel
            '
            Me.bag7Panel.BackColor = System.Drawing.SystemColors.ActiveBorder
            Me.bag7Panel.Controls.Add(Me.Bag7Pic)
            Me.bag7Panel.Location = New System.Drawing.Point(347, 291)
            Me.bag7Panel.Name = "bag7Panel"
            Me.bag7Panel.Size = New System.Drawing.Size(56, 56)
            Me.bag7Panel.TabIndex = 239
            '
            'Bag7Pic
            '
            Me.Bag7Pic.BackgroundImage = CType(resources.GetObject("Bag7Pic.BackgroundImage"), System.Drawing.Image)
            Me.Bag7Pic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.Bag7Pic.Cursor = System.Windows.Forms.Cursors.Hand
            Me.Bag7Pic.Location = New System.Drawing.Point(3, 3)
            Me.Bag7Pic.Name = "Bag7Pic"
            Me.Bag7Pic.Size = New System.Drawing.Size(50, 50)
            Me.Bag7Pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.Bag7Pic.TabIndex = 0
            Me.Bag7Pic.TabStop = False
            '
            'reference_itm_panel
            '
            Me.reference_itm_panel.BackColor = System.Drawing.SystemColors.ActiveBorder
            Me.reference_itm_panel.Controls.Add(Me.reference_itm_pic)
            Me.reference_itm_panel.Location = New System.Drawing.Point(587, 484)
            Me.reference_itm_panel.Margin = New System.Windows.Forms.Padding(1)
            Me.reference_itm_panel.Name = "reference_itm_panel"
            Me.reference_itm_panel.Size = New System.Drawing.Size(56, 56)
            Me.reference_itm_panel.TabIndex = 240
            '
            'reference_itm_pic
            '
            Me.reference_itm_pic.BackgroundImage = Global.Namcore_Studio.My.Resources.Resources.bank_empty
            Me.reference_itm_pic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.reference_itm_pic.Cursor = System.Windows.Forms.Cursors.Hand
            Me.reference_itm_pic.Location = New System.Drawing.Point(3, 3)
            Me.reference_itm_pic.Name = "reference_itm_pic"
            Me.reference_itm_pic.Size = New System.Drawing.Size(50, 50)
            Me.reference_itm_pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.reference_itm_pic.TabIndex = 0
            Me.reference_itm_pic.TabStop = False
            '
            'label2
            '
            Me.label2.AutoSize = True
            Me.label2.BackColor = System.Drawing.Color.Transparent
            Me.label2.Cursor = System.Windows.Forms.Cursors.IBeam
            Me.label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.label2.ForeColor = System.Drawing.Color.Black
            Me.label2.Location = New System.Drawing.Point(162, 3)
            Me.label2.Name = "label2"
            Me.label2.Size = New System.Drawing.Size(88, 20)
            Me.label2.TabIndex = 241
            Me.label2.Text = "Item slots"
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.BackColor = System.Drawing.Color.Transparent
            Me.Label1.Cursor = System.Windows.Forms.Cursors.IBeam
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.ForeColor = System.Drawing.Color.Black
            Me.Label1.Location = New System.Drawing.Point(162, 265)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(84, 20)
            Me.Label1.TabIndex = 242
            Me.Label1.Text = "Bag slots"
            '
            'BagItemPanel
            '
            Me.BagItemPanel.AutoSize = True
            Me.BagItemPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.BagItemPanel.BackColor = System.Drawing.Color.Transparent
            Me.BagItemPanel.Location = New System.Drawing.Point(418, 26)
            Me.BagItemPanel.MaximumSize = New System.Drawing.Size(329, 10000)
            Me.BagItemPanel.MinimumSize = New System.Drawing.Size(329, 10)
            Me.BagItemPanel.Name = "BagItemPanel"
            Me.BagItemPanel.Size = New System.Drawing.Size(329, 10)
            Me.BagItemPanel.TabIndex = 243
            '
            'referenceItmPanel
            '
            Me.referenceItmPanel.BackColor = System.Drawing.SystemColors.ActiveBorder
            Me.referenceItmPanel.Controls.Add(Me.referenceItmPic)
            Me.referenceItmPanel.Location = New System.Drawing.Point(519, 495)
            Me.referenceItmPanel.Margin = New System.Windows.Forms.Padding(1)
            Me.referenceItmPanel.Name = "referenceItmPanel"
            Me.referenceItmPanel.Size = New System.Drawing.Size(45, 45)
            Me.referenceItmPanel.TabIndex = 244
            '
            'referenceItmPic
            '
            Me.referenceItmPic.BackgroundImage = CType(resources.GetObject("referenceItmPic.BackgroundImage"), System.Drawing.Image)
            Me.referenceItmPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.referenceItmPic.Location = New System.Drawing.Point(3, 3)
            Me.referenceItmPic.Name = "referenceItmPic"
            Me.referenceItmPic.Size = New System.Drawing.Size(39, 39)
            Me.referenceItmPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.referenceItmPic.TabIndex = 0
            Me.referenceItmPic.TabStop = False
            '
            'BackPanel
            '
            Me.BackPanel.BackColor = System.Drawing.Color.Transparent
            Me.BackPanel.BackgroundImage = Global.Namcore_Studio.My.Resources.Resources.bank_bg
            Me.BackPanel.Controls.Add(Me.label2)
            Me.BackPanel.Controls.Add(Me.bag5Panel)
            Me.BackPanel.Controls.Add(Me.BagItemPanel)
            Me.BackPanel.Controls.Add(Me.bag4Panel)
            Me.BackPanel.Controls.Add(Me.Label1)
            Me.BackPanel.Controls.Add(Me.bag3Panel)
            Me.BackPanel.Controls.Add(Me.bag2Panel)
            Me.BackPanel.Controls.Add(Me.bag1Panel)
            Me.BackPanel.Controls.Add(Me.bag7Panel)
            Me.BackPanel.Controls.Add(Me.BankLayoutPanel)
            Me.BackPanel.Controls.Add(Me.bag6Panel)
            Me.BackPanel.Location = New System.Drawing.Point(2, 83)
            Me.BackPanel.Name = "BackPanel"
            Me.BackPanel.Size = New System.Drawing.Size(754, 350)
            Me.BackPanel.TabIndex = 245
            '
            'BankInterface
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackgroundImage = Global.Namcore_Studio.My.Resources.Resources.HUD_bg
            Me.ClientSize = New System.Drawing.Size(761, 438)
            Me.Controls.Add(Me.BackPanel)
            Me.Controls.Add(Me.referenceItmPanel)
            Me.Controls.Add(Me.reference_itm_panel)
            Me.DoubleBuffered = True
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "BankInterface"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Reputation_interface"
            Me.Controls.SetChildIndex(Me.reference_itm_panel, 0)
            Me.Controls.SetChildIndex(Me.referenceItmPanel, 0)
            Me.Controls.SetChildIndex(Me.BackPanel, 0)
            Me.bag1Panel.ResumeLayout(False)
            CType(Me.bag1Pic, System.ComponentModel.ISupportInitialize).EndInit()
            Me.bag2Panel.ResumeLayout(False)
            CType(Me.bag2Pic, System.ComponentModel.ISupportInitialize).EndInit()
            Me.bag3Panel.ResumeLayout(False)
            CType(Me.bag3Pic, System.ComponentModel.ISupportInitialize).EndInit()
            Me.bag4Panel.ResumeLayout(False)
            CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.bag4Pic, System.ComponentModel.ISupportInitialize).EndInit()
            Me.bag5Panel.ResumeLayout(False)
            CType(Me.bag5Pic, System.ComponentModel.ISupportInitialize).EndInit()
            Me.bag6Panel.ResumeLayout(False)
            CType(Me.bag6Pic, System.ComponentModel.ISupportInitialize).EndInit()
            Me.bag7Panel.ResumeLayout(False)
            CType(Me.Bag7Pic, System.ComponentModel.ISupportInitialize).EndInit()
            Me.reference_itm_panel.ResumeLayout(False)
            CType(Me.reference_itm_pic, System.ComponentModel.ISupportInitialize).EndInit()
            Me.referenceItmPanel.ResumeLayout(False)
            CType(Me.referenceItmPic, System.ComponentModel.ISupportInitialize).EndInit()
            Me.BackPanel.ResumeLayout(False)
            Me.BackPanel.PerformLayout()
            Me.ResumeLayout(False)

End Sub
        Friend WithEvents bag1Panel As System.Windows.Forms.Panel
        Friend WithEvents bag1Pic As System.Windows.Forms.PictureBox
        Friend WithEvents bag2Panel As System.Windows.Forms.Panel
        Friend WithEvents bag2Pic As System.Windows.Forms.PictureBox
        Friend WithEvents bag3Panel As System.Windows.Forms.Panel
        Friend WithEvents bag3Pic As System.Windows.Forms.PictureBox
        Friend WithEvents bag4Panel As System.Windows.Forms.Panel
        Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
        Friend WithEvents bag4Pic As System.Windows.Forms.PictureBox
        Friend WithEvents bag5Panel As System.Windows.Forms.Panel
        Friend WithEvents bag5Pic As System.Windows.Forms.PictureBox
        Friend WithEvents BankLayoutPanel As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents bag6Panel As System.Windows.Forms.Panel
        Friend WithEvents bag6Pic As System.Windows.Forms.PictureBox
        Friend WithEvents bag7Panel As System.Windows.Forms.Panel
        Friend WithEvents Bag7Pic As System.Windows.Forms.PictureBox
        Friend WithEvents reference_itm_panel As System.Windows.Forms.Panel
        Friend WithEvents reference_itm_pic As System.Windows.Forms.PictureBox
        Friend WithEvents label2 As System.Windows.Forms.Label
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents BagItemPanel As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents referenceItmPanel As System.Windows.Forms.Panel
        Friend WithEvents referenceItmPic As System.Windows.Forms.PictureBox
        Friend WithEvents InfoToolTip As System.Windows.Forms.ToolTip
        Friend WithEvents BackPanel As System.Windows.Forms.Panel
    End Class
End Namespace