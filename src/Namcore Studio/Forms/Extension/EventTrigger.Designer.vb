Namespace Forms.Extension
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class EventTrigger
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
            Me.about_bt = New System.Windows.Forms.PictureBox()
            Me.settings_bt = New System.Windows.Forms.PictureBox()
            Me.highlighter1 = New System.Windows.Forms.PictureBox()
            Me.highlighter2 = New System.Windows.Forms.PictureBox()
            Me.header.SuspendLayout()
            Me.closepanel.SuspendLayout()
            CType(Me.about_bt, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.settings_bt, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.highlighter1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.highlighter2, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'header
            '
            Me.header.BackColor = System.Drawing.Color.Transparent
            Me.header.BackgroundImage = Global.Namcore_Studio.My.Resources.Resources.header
            Me.header.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
            Me.header.Controls.Add(Me.closepanel)
            Me.header.Location = New System.Drawing.Point(5, 4)
            Me.header.Name = "header"
            Me.header.Size = New System.Drawing.Size(762, 88)
            Me.header.TabIndex = 225
            '
            'closepanel
            '
            Me.closepanel.BackColor = System.Drawing.Color.Transparent
            Me.closepanel.Controls.Add(Me.about_bt)
            Me.closepanel.Controls.Add(Me.settings_bt)
            Me.closepanel.Controls.Add(Me.highlighter1)
            Me.closepanel.Controls.Add(Me.highlighter2)
            Me.closepanel.Location = New System.Drawing.Point(637, 1)
            Me.closepanel.Name = "closepanel"
            Me.closepanel.Size = New System.Drawing.Size(123, 25)
            Me.closepanel.TabIndex = 174
            '
            'about_bt
            '
            Me.about_bt.BackColor = System.Drawing.Color.Transparent
            Me.about_bt.BackgroundImage = Global.Namcore_Studio.My.Resources.Resources.bt_about
            Me.about_bt.Cursor = System.Windows.Forms.Cursors.Hand
            Me.about_bt.Location = New System.Drawing.Point(2, 2)
            Me.about_bt.Name = "about_bt"
            Me.about_bt.Size = New System.Drawing.Size(25, 20)
            Me.about_bt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.about_bt.TabIndex = 176
            Me.about_bt.TabStop = False
            '
            'settings_bt
            '
            Me.settings_bt.BackColor = System.Drawing.Color.Transparent
            Me.settings_bt.BackgroundImage = Global.Namcore_Studio.My.Resources.Resources.bt_settings
            Me.settings_bt.Cursor = System.Windows.Forms.Cursors.Hand
            Me.settings_bt.Location = New System.Drawing.Point(33, 2)
            Me.settings_bt.Name = "settings_bt"
            Me.settings_bt.Size = New System.Drawing.Size(25, 20)
            Me.settings_bt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.settings_bt.TabIndex = 175
            Me.settings_bt.TabStop = False
            '
            'highlighter1
            '
            Me.highlighter1.BackColor = System.Drawing.Color.Transparent
            Me.highlighter1.BackgroundImage = Global.Namcore_Studio.My.Resources.Resources.bt_minimize
            Me.highlighter1.Cursor = System.Windows.Forms.Cursors.Hand
            Me.highlighter1.Location = New System.Drawing.Point(64, 2)
            Me.highlighter1.Name = "highlighter1"
            Me.highlighter1.Size = New System.Drawing.Size(25, 20)
            Me.highlighter1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.highlighter1.TabIndex = 174
            Me.highlighter1.TabStop = False
            '
            'highlighter2
            '
            Me.highlighter2.BackColor = System.Drawing.Color.Transparent
            Me.highlighter2.BackgroundImage = Global.Namcore_Studio.My.Resources.Resources.bt_close
            Me.highlighter2.Cursor = System.Windows.Forms.Cursors.Hand
            Me.highlighter2.Location = New System.Drawing.Point(95, 2)
            Me.highlighter2.Name = "highlighter2"
            Me.highlighter2.Size = New System.Drawing.Size(25, 20)
            Me.highlighter2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.highlighter2.TabIndex = 173
            Me.highlighter2.TabStop = False
            '
            'EventTrigger
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackgroundImage = Global.Namcore_Studio.My.Resources.Resources.HUD_bg
            Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.ClientSize = New System.Drawing.Size(771, 453)
            Me.Controls.Add(Me.header)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            Me.Name = "EventTrigger"
            Me.Text = "EventTrigger"
            Me.header.ResumeLayout(False)
            Me.closepanel.ResumeLayout(False)
            CType(Me.about_bt, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.settings_bt, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.highlighter1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.highlighter2, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents header As System.Windows.Forms.Panel
        Friend WithEvents closepanel As System.Windows.Forms.Panel
        Friend WithEvents about_bt As System.Windows.Forms.PictureBox
        Friend WithEvents settings_bt As System.Windows.Forms.PictureBox
        Friend WithEvents highlighter1 As System.Windows.Forms.PictureBox
        Friend WithEvents highlighter2 As System.Windows.Forms.PictureBox
    End Class
End Namespace