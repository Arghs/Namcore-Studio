Imports NamCore_Studio.Forms.Extension

Namespace Forms.Character
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class SpellSkillInterface
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
            Me.tabcontrol = New System.Windows.Forms.TabControl()
            Me.spellPage = New System.Windows.Forms.TabPage()
            Me.resultstatusSpell_lbl = New System.Windows.Forms.Label()
            Me.search_tb = New System.Windows.Forms.TextBox()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.add_bt = New System.Windows.Forms.Button()
            Me.spellList = New System.Windows.Forms.ListView()
            Me.spellId = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.spellName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.skillPage = New System.Windows.Forms.TabPage()
            Me.resultstatusSkill_lbl = New System.Windows.Forms.Label()
            Me.TextBox1 = New System.Windows.Forms.TextBox()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.Button1 = New System.Windows.Forms.Button()
            Me.skillList = New System.Windows.Forms.ListView()
            Me.skillId = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.skillName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.skillValue = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.skillMax = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.tabcontrol.SuspendLayout()
            Me.spellPage.SuspendLayout()
            Me.skillPage.SuspendLayout()
            Me.SuspendLayout()
            '
            'tabcontrol
            '
            Me.tabcontrol.Controls.Add(Me.spellPage)
            Me.tabcontrol.Controls.Add(Me.skillPage)
            Me.tabcontrol.Location = New System.Drawing.Point(9, 83)
            Me.tabcontrol.Name = "tabcontrol"
            Me.tabcontrol.SelectedIndex = 0
            Me.tabcontrol.Size = New System.Drawing.Size(701, 458)
            Me.tabcontrol.TabIndex = 0
            '
            'spellPage
            '
            Me.spellPage.BackColor = System.Drawing.Color.FromArgb(CType(CType(126, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(156, Byte), Integer))
            Me.spellPage.Controls.Add(Me.resultstatusSpell_lbl)
            Me.spellPage.Controls.Add(Me.search_tb)
            Me.spellPage.Controls.Add(Me.Label1)
            Me.spellPage.Controls.Add(Me.add_bt)
            Me.spellPage.Controls.Add(Me.spellList)
            Me.spellPage.Location = New System.Drawing.Point(4, 22)
            Me.spellPage.Name = "spellPage"
            Me.spellPage.Padding = New System.Windows.Forms.Padding(3)
            Me.spellPage.Size = New System.Drawing.Size(693, 432)
            Me.spellPage.TabIndex = 0
            Me.spellPage.Text = "Spells"
            '
            'resultstatusSpell_lbl
            '
            Me.resultstatusSpell_lbl.AutoSize = True
            Me.resultstatusSpell_lbl.BackColor = System.Drawing.Color.Transparent
            Me.resultstatusSpell_lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.resultstatusSpell_lbl.ForeColor = System.Drawing.Color.Black
            Me.resultstatusSpell_lbl.Location = New System.Drawing.Point(262, 78)
            Me.resultstatusSpell_lbl.Name = "resultstatusSpell_lbl"
            Me.resultstatusSpell_lbl.Size = New System.Drawing.Size(78, 16)
            Me.resultstatusSpell_lbl.TabIndex = 234
            Me.resultstatusSpell_lbl.Text = "No results"
            '
            'search_tb
            '
            Me.search_tb.ForeColor = System.Drawing.SystemColors.WindowFrame
            Me.search_tb.Location = New System.Drawing.Point(262, 55)
            Me.search_tb.Name = "search_tb"
            Me.search_tb.Size = New System.Drawing.Size(100, 20)
            Me.search_tb.TabIndex = 233
            Me.search_tb.Text = "Enter spell id"
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.BackColor = System.Drawing.Color.Transparent
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.ForeColor = System.Drawing.Color.Black
            Me.Label1.Location = New System.Drawing.Point(453, 410)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(234, 16)
            Me.Label1.TabIndex = 232
            Me.Label1.Text = "Right click to open context menu!"
            '
            'add_bt
            '
            Me.add_bt.BackColor = System.Drawing.Color.DimGray
            Me.add_bt.Cursor = System.Windows.Forms.Cursors.Hand
            Me.add_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.add_bt.ForeColor = System.Drawing.Color.Black
            Me.add_bt.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.add_bt.Location = New System.Drawing.Point(262, 6)
            Me.add_bt.Name = "add_bt"
            Me.add_bt.Size = New System.Drawing.Size(155, 34)
            Me.add_bt.TabIndex = 231
            Me.add_bt.Text = "Add spell"
            Me.add_bt.UseVisualStyleBackColor = False
            '
            'spellList
            '
            Me.spellList.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
            Me.spellList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.spellId, Me.spellName})
            Me.spellList.Location = New System.Drawing.Point(6, 6)
            Me.spellList.Name = "spellList"
            Me.spellList.Size = New System.Drawing.Size(250, 420)
            Me.spellList.TabIndex = 0
            Me.spellList.UseCompatibleStateImageBehavior = False
            Me.spellList.View = System.Windows.Forms.View.Details
            '
            'spellId
            '
            Me.spellId.Text = "Id"
            '
            'spellName
            '
            Me.spellName.Text = "Name"
            Me.spellName.Width = 163
            '
            'skillPage
            '
            Me.skillPage.BackColor = System.Drawing.Color.Black
            Me.skillPage.Controls.Add(Me.resultstatusSkill_lbl)
            Me.skillPage.Controls.Add(Me.TextBox1)
            Me.skillPage.Controls.Add(Me.Label3)
            Me.skillPage.Controls.Add(Me.Button1)
            Me.skillPage.Controls.Add(Me.skillList)
            Me.skillPage.Location = New System.Drawing.Point(4, 22)
            Me.skillPage.Name = "skillPage"
            Me.skillPage.Padding = New System.Windows.Forms.Padding(3)
            Me.skillPage.Size = New System.Drawing.Size(693, 432)
            Me.skillPage.TabIndex = 1
            Me.skillPage.Text = "Skills"
            '
            'resultstatusSkill_lbl
            '
            Me.resultstatusSkill_lbl.AutoSize = True
            Me.resultstatusSkill_lbl.BackColor = System.Drawing.Color.Transparent
            Me.resultstatusSkill_lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.resultstatusSkill_lbl.ForeColor = System.Drawing.Color.Black
            Me.resultstatusSkill_lbl.Location = New System.Drawing.Point(361, 78)
            Me.resultstatusSkill_lbl.Name = "resultstatusSkill_lbl"
            Me.resultstatusSkill_lbl.Size = New System.Drawing.Size(78, 16)
            Me.resultstatusSkill_lbl.TabIndex = 238
            Me.resultstatusSkill_lbl.Text = "No results"
            '
            'TextBox1
            '
            Me.TextBox1.ForeColor = System.Drawing.SystemColors.WindowFrame
            Me.TextBox1.Location = New System.Drawing.Point(361, 55)
            Me.TextBox1.Name = "TextBox1"
            Me.TextBox1.Size = New System.Drawing.Size(100, 20)
            Me.TextBox1.TabIndex = 237
            Me.TextBox1.Text = "Enter skill id"
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.BackColor = System.Drawing.Color.Transparent
            Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label3.ForeColor = System.Drawing.Color.Black
            Me.Label3.Location = New System.Drawing.Point(453, 408)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(234, 16)
            Me.Label3.TabIndex = 236
            Me.Label3.Text = "Right click to open context menu!"
            '
            'Button1
            '
            Me.Button1.BackColor = System.Drawing.Color.DimGray
            Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
            Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.Button1.ForeColor = System.Drawing.Color.Black
            Me.Button1.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.Button1.Location = New System.Drawing.Point(361, 6)
            Me.Button1.Name = "Button1"
            Me.Button1.Size = New System.Drawing.Size(155, 34)
            Me.Button1.TabIndex = 235
            Me.Button1.Text = "Add skill"
            Me.Button1.UseVisualStyleBackColor = False
            '
            'skillList
            '
            Me.skillList.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
            Me.skillList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.skillId, Me.skillName, Me.skillValue, Me.skillMax})
            Me.skillList.Location = New System.Drawing.Point(5, 4)
            Me.skillList.Name = "skillList"
            Me.skillList.Size = New System.Drawing.Size(350, 420)
            Me.skillList.TabIndex = 1
            Me.skillList.UseCompatibleStateImageBehavior = False
            Me.skillList.View = System.Windows.Forms.View.Details
            '
            'skillId
            '
            Me.skillId.Text = "Id"
            '
            'skillName
            '
            Me.skillName.Text = "Name"
            Me.skillName.Width = 163
            '
            'skillValue
            '
            Me.skillValue.Text = "Value"
            '
            'skillMax
            '
            Me.skillMax.Text = "Maximum"
            '
            'SpellSkillInterface
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackgroundImage = Global.NamCore_Studio.My.Resources.Resources.HUD_bg
            Me.ClientSize = New System.Drawing.Size(719, 547)
            Me.Controls.Add(Me.tabcontrol)
            Me.DoubleBuffered = True
            Me.Name = "SpellSkillInterface"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "SpellSkill_interface"
            Me.Controls.SetChildIndex(Me.tabcontrol, 0)
            Me.tabcontrol.ResumeLayout(False)
            Me.spellPage.ResumeLayout(False)
            Me.spellPage.PerformLayout()
            Me.skillPage.ResumeLayout(False)
            Me.skillPage.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents tabcontrol As System.Windows.Forms.TabControl
        Friend WithEvents spellPage As System.Windows.Forms.TabPage
        Friend WithEvents skillPage As System.Windows.Forms.TabPage
        Friend WithEvents spellList As System.Windows.Forms.ListView
        Friend WithEvents spellId As System.Windows.Forms.ColumnHeader
        Friend WithEvents spellName As System.Windows.Forms.ColumnHeader
        Friend WithEvents resultstatusSpell_lbl As System.Windows.Forms.Label
        Friend WithEvents search_tb As System.Windows.Forms.TextBox
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents add_bt As System.Windows.Forms.Button
        Friend WithEvents resultstatusSkill_lbl As System.Windows.Forms.Label
        Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents Button1 As System.Windows.Forms.Button
        Friend WithEvents skillList As System.Windows.Forms.ListView
        Friend WithEvents skillId As System.Windows.Forms.ColumnHeader
        Friend WithEvents skillName As System.Windows.Forms.ColumnHeader
        Friend WithEvents skillValue As System.Windows.Forms.ColumnHeader
        Friend WithEvents skillMax As System.Windows.Forms.ColumnHeader
    End Class
End Namespace