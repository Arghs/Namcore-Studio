'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013-2015 NamCore Studio <https://github.com/megasus/Namcore-Studio>
'*
'* This program is free software; you can redistribute it and/or modify it
'* under the terms of the GNU General Public License as published by the
'* Free Software Foundation; either version 3 of the License, or (at your
'* option) any later version.
'*
'* This program is distributed in the hope that it will be useful, but WITHOUT
'* ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
'* FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for
'* more details.
'*
'* You should have received a copy of the GNU General Public License along
'* with this program. If not, see <http://www.gnu.org/licenses/>.
'*
'* Developed by Alcanmage/megasus
'*
'* //FileInfo//
'*      /Filename:      SpellSkillInterface
'*      /Description:   Provides an interface to display character reputation information
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.Linq
Imports NCFramework.My
Imports NCFramework.Framework.Modules
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Functions
Imports NamCore_Studio.Modules.Interface
Imports NCFramework.Framework.Extension
Imports NamCore_Studio.Forms.Extension
Imports System.Threading
Imports NCFramework.My.Resources
Imports libnc.Provider

Namespace Forms.Character
    Public Class SpellSkillInterface
        Inherits EventTrigger

        '// Declaration
        Private _spellItemList As New List(Of ListViewItem)
        Private _skillItemList As New List(Of ListViewItem)
        Public Event ThisCompleted As EventHandler(Of CompletedEventArgs)
        Private ReadOnly _context As SynchronizationContext = SynchronizationContext.Current
        Private WithEvents _mHandler As New TrdQueueHandler
        Private _cmpFileListViewComparer As ListViewComparer
        Private _cmpFileListViewComparer2 As ListViewComparer

        Delegate Sub AddItemDelegate(itm As ListViewItem, control As ListView)
        '// Declaration

        Private Sub highlighter2_Click(sender As Object, e As EventArgs)
            Close()
        End Sub

        Private Sub SpellSkill_interface_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            AddHandler highlighter2.Click, AddressOf highlighter2_Click
            Dim controlLst As List(Of Control)
            controlLst = FindAllChildren()
            For Each itemControl As Control In controlLst
                itemControl.SetDoubleBuffered()
            Next
        End Sub

        Protected Overridable Sub OnCompleted(ByVal e As CompletedEventArgs)
            RaiseEvent ThisCompleted(Me, e)
        End Sub

        Public Sub PrepareInterface(ByVal setId As Integer)
            Hide()
            _cmpFileListViewComparer = New ListViewComparer(spellList)
            _cmpFileListViewComparer2 = New ListViewComparer(skillList)
            _mHandler.doOperate_spellSkill(setId)
        End Sub

        Private Sub MeCompleted() Handles Me.ThisCompleted
            resultstatusSpell_lbl.Text = spellList.Items.Count.ToString & MISC_SPELLSKILLRESULTS
            resultstatusSkill_lbl.Text = skillList.Items.Count.ToString & MISC_PROFRESULTS
            If _spellItemList Is Nothing Then _spellItemList = New List(Of ListViewItem)()
            If _skillItemList Is Nothing Then _skillItemList = New List(Of ListViewItem)()
            For Each spellItm As ListViewItem In spellList.Items
                _spellItemList.Add(spellItm)
            Next
            For Each skillItm As ListViewItem In skillList.Items
                _skillItemList.Add(skillItm)
            Next
            Userwait.Close()
            Application.DoEvents()
            Show()
            CloseProcessStatus()
        End Sub

        Public Function ContinueOperation(ByVal setId As Integer) As String
            LogAppend("Loading Spells/Skills", "SpellSkill_interface_continueOperation", True)
            If GlobalVariables.currentEditedCharSet Is Nothing Then
                GlobalVariables.currentEditedCharSet = DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet)
            End If
            If Not GlobalVariables.currentEditedCharSet.Spells Is Nothing Then
                For Each pSpell As Spell In _
                    From pSpell1 In GlobalVariables.currentEditedCharSet.Spells Where Not pSpell1.Id = 0
                    If pSpell.Name Is Nothing Then
                        pSpell.Name = GetSpellNameBySpellId(pSpell.Id, MySettings.Default.language)
                    End If
                    Dim itm As New ListViewItem(New String() {pSpell.Id.ToString(), pSpell.Name})
                    itm.Tag = pSpell
                    spellList.BeginInvoke(New AddItemDelegate(AddressOf DelegateControlAdding), itm, spellList)
                Next
            End If
            If Not GlobalVariables.currentEditedCharSet.Skills Is Nothing Then
                For i As Integer = 0 To GlobalVariables.currentEditedCharSet.Skills.Count - 1
                    Dim pSkill As Skill = GlobalVariables.currentEditedCharSet.Skills(i)
                    If Not pSkill.Id = 0 Then
                        If pSkill.Name Is Nothing Then
                            pSkill.Name = GetSkillNameById(pSkill.Id, MySettings.Default.language)
                        End If
                        Dim _
                            itm As _
                                New ListViewItem(
                                    New String() _
                                                    {pSkill.Id.ToString(), pSkill.Name, pSkill.Value.ToString(),
                                                     pSkill.Max.ToString()})
                        itm.Tag = pSkill
                        skillList.BeginInvoke(New AddItemDelegate(AddressOf DelegateControlAdding), itm, skillList)
                    End If
                Next
            End If
            ThreadExtensions.ScSend(_context, New Action(Of CompletedEventArgs)(AddressOf OnCompleted),
                                    New CompletedEventArgs())
            Return Nothing
        End Function

        Private Sub DelegateControlAdding(additm As ListViewItem, control As ListView)
            control.Items.Add(additm)
        End Sub

        Private Sub SpellList_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles spellList.ColumnClick
            If e.Column = _cmpFileListViewComparer.SortColumn Then
                If _cmpFileListViewComparer.SortOrder = SortOrder.Ascending Then
                    _cmpFileListViewComparer.SortOrder = SortOrder.Descending
                Else
                    _cmpFileListViewComparer.SortOrder = SortOrder.Ascending
                End If
            Else
                _cmpFileListViewComparer.SortOrder = SortOrder.Ascending
            End If

            _cmpFileListViewComparer.SortColumn = e.Column
            spellList.Sort()
        End Sub

        Private Sub SkillList_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles skillList.ColumnClick
            If e.Column = _cmpFileListViewComparer2.SortColumn Then
                If _cmpFileListViewComparer2.SortOrder = SortOrder.Ascending Then
                    _cmpFileListViewComparer2.SortOrder = SortOrder.Descending
                Else
                    _cmpFileListViewComparer2.SortOrder = SortOrder.Ascending
                End If
            Else
                _cmpFileListViewComparer2.SortOrder = SortOrder.Ascending
            End If

            _cmpFileListViewComparer2.SortColumn = e.Column
            skillList.Sort()
        End Sub

        Private Sub AddSpell_bt_Click(sender As Object, e As EventArgs) Handles AddSpell_bt.Click
            Dim pSpellId As Integer = TryInt(Spell_tb.Text)
            If pSpellId = 0 Then
                MsgBox(MSG_INVALIDSPELLID, MsgBoxStyle.Critical, MSG_ATTENTION)
                Spell_tb.Text = MSG_ENTERSPELLID
                Spell_tb.ForeColor = SystemColors.WindowFrame
            Else
                If _
                    spellList.Items.Cast (Of ListViewItem)().Any(
                        Function(existingSpell) existingSpell.SubItems(0).Text = pSpellId.ToString()) Then
                    MsgBox(MSG_SPELLALREADYPRESENT, MsgBoxStyle.Critical, MSG_ATTENTION)
                Else
                    Dim spell As New Spell With {.Active = 1, .Disabled = 0, .Id = pSpellId}
                    spell.Name = GetSpellNameBySpellId(pSpellId, MySettings.Default.language)
                    GlobalVariables.currentEditedCharSet.Spells.Add(spell)
                    Dim spellItem As New ListViewItem({spell.Id.ToString, spell.Name})
                    spellItem.Tag = spell
                    spellList.Items.Add(spellItem)
                    spellList.Update()
                    spellList.Sort()
                    Spell_tb.Text = MSG_ENTERSPELLID
                    Spell_tb.ForeColor = SystemColors.WindowFrame
                    resultstatusSpell_lbl.Text = spellList.Items.Count.ToString & MISC_SPELLSKILLRESULTS
                End If
            End If
        End Sub

        Private Sub searchSpell_tb_Enter(sender As Object, e As EventArgs) Handles Spell_tb.Enter
            If Spell_tb.Text = MSG_ENTERSPELLID Then
                Spell_tb.Text = ""
                Skill_tb.ForeColor = Color.Black
            End If
        End Sub

        Private Sub searchSpell_tb_Leave(sender As Object, e As EventArgs) Handles Spell_tb.Leave
            If Spell_tb.Text.Length = 0 Then
                Spell_tb.Text = MSG_ENTERSPELLID
                Spell_tb.ForeColor = SystemColors.WindowFrame
            End If
        End Sub

        Private Sub spellList_MouseUp(sender As Object, e As MouseEventArgs) Handles spellList.MouseUp
            If e.Button = MouseButtons.Right Then
                If spellList.SelectedItems.Count = 0 Then Exit Sub
                spellContext.Show(spellList, e.X, e.Y)
            End If
        End Sub

        Private Sub RemoveSelectedToolStripMenuItem_Click(sender As Object, e As EventArgs) _
            Handles RemoveSelectedToolStripMenuItem.Click
            For i = 0 To spellList.SelectedItems.Count - 1
                Dim itm As ListViewItem = spellList.SelectedItems(0)
                GlobalVariables.currentEditedCharSet.Spells.Remove(CType(itm.Tag, Spell))
                spellList.Items.Remove(itm)
                Application.DoEvents()
            Next
            resultstatusSpell_lbl.Text = spellList.Items.Count.ToString & MISC_SPELLSKILLRESULTS
        End Sub

        Private Sub AddSkill_bt_Click(sender As Object, e As EventArgs) Handles AddSkill_bt.Click
            Dim pSkillId As Integer = TryInt(Skill_tb.Text)
            If pSkillId = 0 Then
                MsgBox(MSG_INVALIDSKILLID, MsgBoxStyle.Critical, MSG_ATTENTION)
                Spell_tb.Text = MSG_ENTERSKILLID
                Spell_tb.ForeColor = SystemColors.WindowFrame
            Else
                If _
                    skillList.Items.Cast (Of ListViewItem)().Any(
                        Function(existingSkill) existingSkill.SubItems(0).Text = pSkillId.ToString()) Then
                    MsgBox(MSG_SKILLALREADYPRESENT, MsgBoxStyle.Critical, MSG_ATTENTION)
                Else
                    Dim skill As New Skill With {.Value = 1, .Max = 600, .Id = pSkillId}
                    skill.Name = GetSkillNameById(pSkillId, MySettings.Default.language)
                    GlobalVariables.currentEditedCharSet.Skills.Add(skill)
                    Dim _
                        skillItem As _
                            New ListViewItem(
                                New String() _
                                                {skill.Id.ToString, skill.Name, skill.Value.ToString(),
                                                 skill.Max.ToString()})
                    skillItem.Tag = skill
                    skillList.Items.Add(skillItem)
                    skillList.Update()
                    skillList.Sort()
                    Spell_tb.Text = MSG_ENTERSKILLID
                    Spell_tb.ForeColor = SystemColors.WindowFrame
                    resultstatusSkill_lbl.Text = skillList.Items.Count.ToString & MISC_SPELLSKILLRESULTS
                End If
            End If
        End Sub

        Private Sub searchSkill_tb_Enter(sender As Object, e As EventArgs) Handles Skill_tb.Enter
            If Skill_tb.Text = MSG_ENTERSKILLID Then
                Skill_tb.Text = ""
                Skill_tb.ForeColor = Color.Black
            End If
        End Sub

        Private Sub searchSkill_tb_Leave(sender As Object, e As EventArgs) Handles Skill_tb.Leave
            If Skill_tb.Text.Length = 0 Then
                Skill_tb.Text = MSG_ENTERSKILLID
                Skill_tb.ForeColor = SystemColors.WindowFrame
            End If
        End Sub

        Private Sub skillList_MouseUp(sender As Object, e As MouseEventArgs) Handles skillList.MouseUp
            If e.Button = MouseButtons.Right Then
                If skillList.SelectedItems.Count = 0 Then Exit Sub
                ToolStripValueTextBox.Text = skillList.SelectedItems(0).SubItems(2).Text
                ToolStripMaxTextBox.Text = skillList.SelectedItems(0).SubItems(3).Text
                skillContext.Show(skillList, e.X, e.Y)
            End If
        End Sub

        Private Sub RemoveSelectedToolStripMenuItem1_Click(sender As Object, e As EventArgs) _
            Handles RemoveSelectedToolStripMenuItem1.Click
            For i = 0 To skillList.SelectedItems.Count - 1
                Dim itm As ListViewItem = skillList.SelectedItems(0)
                GlobalVariables.currentEditedCharSet.Skills.Remove(CType(itm.Tag, Skill))
                skillList.Items.Remove(itm)
                Application.DoEvents()
            Next
            resultstatusSkill_lbl.Text = skillList.Items.Count.ToString & MISC_SPELLSKILLRESULTS
        End Sub

        Private Sub ToolStripValueTextBox_KeyDown(sender As Object, e As KeyEventArgs) _
            Handles ToolStripValueTextBox.KeyDown
            If e.KeyCode = Keys.Enter Then
                ToolStripValueTextBox_Leave(sender, New EventArgs)
            End If
        End Sub

        Private Sub ToolStripMaxTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles ToolStripMaxTextBox.KeyDown
            If e.KeyCode = Keys.Enter Then
                ToolStripMaxTextBox_Leave(sender, New EventArgs)
            End If
        End Sub

        Private Sub ToolStripValueTextBox_Leave(sender As Object, e As EventArgs) _
            Handles ToolStripValueTextBox.LostFocus
            Dim newValue As String = ToolStripValueTextBox.Text
            If newValue = "0" OrElse TryInt(newValue) <> 0 Then
                skillList.SelectedItems(0).SubItems(2).Text = newValue
                Dim itm As ListViewItem = skillList.SelectedItems(0)
                Dim pSkill As Skill = CType(itm.Tag, Skill)
                pSkill.Value = TryInt(newValue)
                itm.Tag = pSkill
                skillList.SelectedItems(0).Tag = pSkill
                skillList.Update()
                Try
                    GlobalVariables.currentEditedCharSet.Skills(
                        GlobalVariables.currentEditedCharSet.Skills.FindIndex(Function(skill) skill.Id = pSkill.Id)).
                        Value = pSkill.Value
                Catch ex As Exception
                    LogAppend("Exception occured: " & ex.ToString(), "SpellSkillInterface_ToolStripValueTextBox_Leave",
                              False, True)
                End Try
                skillContext.Hide()
            End If
        End Sub

        Private Sub ToolStripMaxTextBox_Leave(sender As Object, e As EventArgs) Handles ToolStripMaxTextBox.LostFocus
            Dim newValue As String = ToolStripMaxTextBox.Text
            If newValue = "0" OrElse TryInt(newValue) <> 0 Then
                skillList.SelectedItems(0).SubItems(3).Text = newValue
                Dim itm As ListViewItem = skillList.SelectedItems(0)
                Dim pSkill As Skill = CType(itm.Tag, Skill)
                pSkill.Value = TryInt(newValue)
                itm.Tag = pSkill
                skillList.SelectedItems(0).Tag = pSkill
                skillList.Update()
                Try
                    GlobalVariables.currentEditedCharSet.Skills(
                        GlobalVariables.currentEditedCharSet.Skills.FindIndex(Function(skill) skill.Id = pSkill.Id)).Max _
                        = pSkill.Max
                Catch ex As Exception
                    LogAppend("Exception occured: " & ex.ToString(), "SpellSkillInterface_ToolStripMaxTextBox_Leave",
                              False, True)
                End Try
                skillContext.Hide()
            End If
        End Sub
    End Class
End Namespace