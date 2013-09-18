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
            Me.highlighter1 = New System.Windows.Forms.PictureBox()
            Me.highlighter2 = New System.Windows.Forms.PictureBox()
            Me.header.SuspendLayout()
            Me.closepanel.SuspendLayout()
            CType(Me.highlighter1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.highlighter2, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'header
            '
            Me.header.BackgroundImage = Global.Namcore_Studio.My.Resources.Resources.namcore_header
            Me.header.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
            Me.header.Controls.Add(Me.closepanel)
            Me.header.Location = New System.Drawing.Point(4, 5)
            Me.header.Name = "header"
            Me.header.Size = New System.Drawing.Size(644, 30)
            Me.header.TabIndex = 225
            '
            'closepanel
            '
            Me.closepanel.BackgroundImage = Global.Namcore_Studio.My.Resources.Resources.minclose
            Me.closepanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
            Me.closepanel.Controls.Add(Me.highlighter1)
            Me.closepanel.Controls.Add(Me.highlighter2)
            Me.closepanel.Location = New System.Drawing.Point(586, -1)
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
            'EventTrigger
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackgroundImage = Global.Namcore_Studio.My.Resources.Resources.cleanbg
            Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.ClientSize = New System.Drawing.Size(652, 448)
            Me.Controls.Add(Me.header)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            Me.Name = "EventTrigger"
            Me.Text = "EventTrigger"
            Me.header.ResumeLayout(False)
            Me.closepanel.ResumeLayout(False)
            CType(Me.highlighter1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.highlighter2, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents header As System.Windows.Forms.Panel
        Friend WithEvents closepanel As System.Windows.Forms.Panel
        Friend WithEvents highlighter1 As System.Windows.Forms.PictureBox
        Friend WithEvents highlighter2 As System.Windows.Forms.PictureBox
    End Class
End Namespace