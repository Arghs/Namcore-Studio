﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Reputation_interface
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
        Me.referencePanel = New System.Windows.Forms.Panel()
        Me.reference_standing_combo = New System.Windows.Forms.ComboBox()
        Me.reference_txtbox = New System.Windows.Forms.TextBox()
        Me.reference_counter_lbl = New System.Windows.Forms.Label()
        Me.reference_trackbar = New System.Windows.Forms.TrackBar()
        Me.reference_sliderbg_panel = New System.Windows.Forms.Panel()
        Me.reference_percentage_panel = New System.Windows.Forms.Panel()
        Me.reference_name_lbl = New System.Windows.Forms.Label()
        Me.RepLayoutPanel = New System.Windows.Forms.FlowLayoutPanel()
        Me.referencePanel.SuspendLayout()
        CType(Me.reference_trackbar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.reference_sliderbg_panel.SuspendLayout()
        Me.SuspendLayout()
        '
        'referencePanel
        '
        Me.referencePanel.Controls.Add(Me.reference_standing_combo)
        Me.referencePanel.Controls.Add(Me.reference_txtbox)
        Me.referencePanel.Controls.Add(Me.reference_counter_lbl)
        Me.referencePanel.Controls.Add(Me.reference_trackbar)
        Me.referencePanel.Controls.Add(Me.reference_sliderbg_panel)
        Me.referencePanel.Controls.Add(Me.reference_name_lbl)
        Me.referencePanel.Location = New System.Drawing.Point(1069, 429)
        Me.referencePanel.Name = "referencePanel"
        Me.referencePanel.Size = New System.Drawing.Size(737, 106)
        Me.referencePanel.TabIndex = 0
        '
        'reference_standing_combo
        '
        Me.reference_standing_combo.FormattingEnabled = True
        Me.reference_standing_combo.Location = New System.Drawing.Point(616, 17)
        Me.reference_standing_combo.Name = "reference_standing_combo"
        Me.reference_standing_combo.Size = New System.Drawing.Size(102, 21)
        Me.reference_standing_combo.TabIndex = 7
        '
        'reference_txtbox
        '
        Me.reference_txtbox.Location = New System.Drawing.Point(616, 54)
        Me.reference_txtbox.Name = "reference_txtbox"
        Me.reference_txtbox.Size = New System.Drawing.Size(77, 20)
        Me.reference_txtbox.TabIndex = 6
        '
        'reference_counter_lbl
        '
        Me.reference_counter_lbl.AutoSize = True
        Me.reference_counter_lbl.BackColor = System.Drawing.Color.Black
        Me.reference_counter_lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.reference_counter_lbl.ForeColor = System.Drawing.Color.White
        Me.reference_counter_lbl.Location = New System.Drawing.Point(359, 22)
        Me.reference_counter_lbl.Name = "reference_counter_lbl"
        Me.reference_counter_lbl.Size = New System.Drawing.Size(54, 16)
        Me.reference_counter_lbl.TabIndex = 2
        Me.reference_counter_lbl.Text = "50/1000"
        '
        'reference_trackbar
        '
        Me.reference_trackbar.Location = New System.Drawing.Point(136, 54)
        Me.reference_trackbar.Maximum = 3000
        Me.reference_trackbar.Name = "reference_trackbar"
        Me.reference_trackbar.Size = New System.Drawing.Size(474, 45)
        Me.reference_trackbar.TabIndex = 3
        Me.reference_trackbar.TickStyle = System.Windows.Forms.TickStyle.None
        '
        'reference_sliderbg_panel
        '
        Me.reference_sliderbg_panel.BackgroundImage = Global.Namcore_Studio.My.Resources.Resources.repbg
        Me.reference_sliderbg_panel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.reference_sliderbg_panel.Controls.Add(Me.reference_percentage_panel)
        Me.reference_sliderbg_panel.Location = New System.Drawing.Point(142, 8)
        Me.reference_sliderbg_panel.Name = "reference_sliderbg_panel"
        Me.reference_sliderbg_panel.Size = New System.Drawing.Size(460, 43)
        Me.reference_sliderbg_panel.TabIndex = 5
        '
        'reference_percentage_panel
        '
        Me.reference_percentage_panel.BackColor = System.Drawing.Color.Yellow
        Me.reference_percentage_panel.Location = New System.Drawing.Point(3, 3)
        Me.reference_percentage_panel.Name = "reference_percentage_panel"
        Me.reference_percentage_panel.Size = New System.Drawing.Size(5, 37)
        Me.reference_percentage_panel.TabIndex = 4
        '
        'reference_name_lbl
        '
        Me.reference_name_lbl.AutoSize = True
        Me.reference_name_lbl.Location = New System.Drawing.Point(13, 26)
        Me.reference_name_lbl.Name = "reference_name_lbl"
        Me.reference_name_lbl.Size = New System.Drawing.Size(39, 13)
        Me.reference_name_lbl.TabIndex = 1
        Me.reference_name_lbl.Text = "Label1"
        '
        'RepLayoutPanel
        '
        Me.RepLayoutPanel.AutoScroll = True
        Me.RepLayoutPanel.Location = New System.Drawing.Point(12, 12)
        Me.RepLayoutPanel.Name = "RepLayoutPanel"
        Me.RepLayoutPanel.Size = New System.Drawing.Size(806, 618)
        Me.RepLayoutPanel.TabIndex = 1
        '
        'Reputation_interface
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1841, 642)
        Me.Controls.Add(Me.RepLayoutPanel)
        Me.Controls.Add(Me.referencePanel)
        Me.Name = "Reputation_interface"
        Me.Text = "Reputation_interface"
        Me.referencePanel.ResumeLayout(False)
        Me.referencePanel.PerformLayout()
        CType(Me.reference_trackbar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.reference_sliderbg_panel.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents referencePanel As System.Windows.Forms.Panel
    Friend WithEvents reference_name_lbl As System.Windows.Forms.Label
    Friend WithEvents reference_counter_lbl As System.Windows.Forms.Label
    Friend WithEvents reference_trackbar As System.Windows.Forms.TrackBar
    Friend WithEvents reference_percentage_panel As System.Windows.Forms.Panel
    Friend WithEvents reference_sliderbg_panel As System.Windows.Forms.Panel
    Friend WithEvents reference_txtbox As System.Windows.Forms.TextBox
    Friend WithEvents RepLayoutPanel As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents reference_standing_combo As System.Windows.Forms.ComboBox
End Class