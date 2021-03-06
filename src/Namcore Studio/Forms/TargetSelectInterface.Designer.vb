﻿Imports NamCore_Studio.Forms.Extension

Namespace Forms

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class TargetSelectInterface
        Inherits Form

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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TargetSelectInterface))
            Me.newtemplate_bt = New System.Windows.Forms.Button()
            Me.Label10 = New System.Windows.Forms.Label()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.opentemplate_bt = New System.Windows.Forms.Button()
            Me.connect_bt = New System.Windows.Forms.Button()
            Me.header = New System.Windows.Forms.Panel()
            Me.closepanel = New System.Windows.Forms.Panel()
            Me.highlighter2 = New System.Windows.Forms.PictureBox()
            Me.GroupBox1.SuspendLayout()
            Me.header.SuspendLayout()
            Me.closepanel.SuspendLayout()
            CType(Me.highlighter2, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'newtemplate_bt
            '
            Me.newtemplate_bt.BackColor = System.Drawing.Color.DimGray
            Me.newtemplate_bt.Cursor = System.Windows.Forms.Cursors.Hand
            Me.newtemplate_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.newtemplate_bt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
            Me.newtemplate_bt.ForeColor = System.Drawing.Color.Black
            Me.newtemplate_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.newtemplate_bt.Location = New System.Drawing.Point(9, 28)
            Me.newtemplate_bt.Name = "newtemplate_bt"
            Me.newtemplate_bt.Size = New System.Drawing.Size(256, 36)
            Me.newtemplate_bt.TabIndex = 226
            Me.newtemplate_bt.Text = "Create new template file"
            Me.newtemplate_bt.UseVisualStyleBackColor = False
            '
            'Label10
            '
            Me.Label10.AutoSize = True
            Me.Label10.BackColor = System.Drawing.Color.Transparent
            Me.Label10.Font = New System.Drawing.Font("Franklin Gothic Medium", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label10.ForeColor = System.Drawing.Color.Black
            Me.Label10.Location = New System.Drawing.Point(3, 37)
            Me.Label10.Name = "Label10"
            Me.Label10.Size = New System.Drawing.Size(349, 40)
            Me.Label10.TabIndex = 227
            Me.Label10.Text = "Choose the target " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "- Later you can transfer characters/accounts -"
            '
            'GroupBox1
            '
            Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
            Me.GroupBox1.Controls.Add(Me.opentemplate_bt)
            Me.GroupBox1.Controls.Add(Me.newtemplate_bt)
            Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.GroupBox1.ForeColor = System.Drawing.Color.Black
            Me.GroupBox1.Location = New System.Drawing.Point(48, 84)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(275, 112)
            Me.GroupBox1.TabIndex = 228
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Create template file"
            '
            'opentemplate_bt
            '
            Me.opentemplate_bt.BackColor = System.Drawing.Color.DimGray
            Me.opentemplate_bt.Cursor = System.Windows.Forms.Cursors.Hand
            Me.opentemplate_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.opentemplate_bt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
            Me.opentemplate_bt.ForeColor = System.Drawing.Color.Black
            Me.opentemplate_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.opentemplate_bt.Location = New System.Drawing.Point(9, 70)
            Me.opentemplate_bt.Name = "opentemplate_bt"
            Me.opentemplate_bt.Size = New System.Drawing.Size(256, 36)
            Me.opentemplate_bt.TabIndex = 229
            Me.opentemplate_bt.Text = "Open existing template file"
            Me.opentemplate_bt.UseVisualStyleBackColor = False
            '
            'connect_bt
            '
            Me.connect_bt.BackColor = System.Drawing.Color.DimGray
            Me.connect_bt.Cursor = System.Windows.Forms.Cursors.Hand
            Me.connect_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.connect_bt.ForeColor = System.Drawing.Color.Black
            Me.connect_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.connect_bt.Location = New System.Drawing.Point(57, 213)
            Me.connect_bt.Name = "connect_bt"
            Me.connect_bt.Size = New System.Drawing.Size(256, 36)
            Me.connect_bt.TabIndex = 229
            Me.connect_bt.Text = "Connect to database"
            Me.connect_bt.UseVisualStyleBackColor = False
            '
            'header
            '
            Me.header.BackgroundImage = CType(resources.GetObject("header.BackgroundImage"), System.Drawing.Image)
            Me.header.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
            Me.header.Controls.Add(Me.closepanel)
            Me.header.Location = New System.Drawing.Point(2, 2)
            Me.header.Name = "header"
            Me.header.Size = New System.Drawing.Size(372, 30)
            Me.header.TabIndex = 230
            '
            'closepanel
            '
            Me.closepanel.BackColor = System.Drawing.Color.Transparent
            Me.closepanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
            Me.closepanel.Controls.Add(Me.highlighter2)
            Me.closepanel.Location = New System.Drawing.Point(340, 0)
            Me.closepanel.Name = "closepanel"
            Me.closepanel.Size = New System.Drawing.Size(31, 28)
            Me.closepanel.TabIndex = 0
            '
            'highlighter2
            '
            Me.highlighter2.BackColor = System.Drawing.Color.Transparent
            Me.highlighter2.BackgroundImage = CType(resources.GetObject("highlighter2.BackgroundImage"), System.Drawing.Image)
            Me.highlighter2.Cursor = System.Windows.Forms.Cursors.Hand
            Me.highlighter2.Location = New System.Drawing.Point(3, 4)
            Me.highlighter2.Name = "highlighter2"
            Me.highlighter2.Size = New System.Drawing.Size(25, 20)
            Me.highlighter2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.highlighter2.TabIndex = 218
            Me.highlighter2.TabStop = False
            '
            'TargetSelectInterface
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackgroundImage = Global.NamCore_Studio.My.Resources.Resources.HUD_bg
            Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.ClientSize = New System.Drawing.Size(376, 260)
            Me.Controls.Add(Me.header)
            Me.Controls.Add(Me.connect_bt)
            Me.Controls.Add(Me.GroupBox1)
            Me.Controls.Add(Me.Label10)
            Me.ForeColor = System.Drawing.Color.Black
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            Me.Name = "TargetSelectInterface"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TargetSelectInterface"
            Me.TopMost = True
            Me.GroupBox1.ResumeLayout(False)
            Me.header.ResumeLayout(False)
            Me.closepanel.ResumeLayout(False)
            CType(Me.highlighter2, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents newtemplate_bt As System.Windows.Forms.Button
        Friend WithEvents Label10 As System.Windows.Forms.Label
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents opentemplate_bt As System.Windows.Forms.Button
        Friend WithEvents connect_bt As System.Windows.Forms.Button
        Friend WithEvents header As System.Windows.Forms.Panel
        Friend WithEvents closepanel As System.Windows.Forms.Panel
        Friend WithEvents highlighter2 As System.Windows.Forms.PictureBox
    End Class
End Namespace