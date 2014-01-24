'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013-2014 NamCore Studio <https://github.com/megasus/Namcore-Studio>
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
            _mHandler.doOperate_spellSkill(setId)
        End Sub

        Private Sub MeCompleted() Handles Me.ThisCompleted
            resultstatusSpell_lbl.Text = spellList.Items.Count.ToString & " results"
            resultstatusSkill_lbl.Text = skillList.Items.Count.ToString & " results!"
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

        Public Function ContinueOperation(ByVal setId As Integer)
            LogAppend("Loading Spells/Skills", "SpellSkill_interface_continueOperation", True)
            If Not GlobalVariables.currentViewedCharSet.Spells Is Nothing Then
                For Each pSpell As Spell In From pSpell1 In GlobalVariables.currentViewedCharSet.Spells Where Not pSpell1.id = 0
                    If pSpell.Name Is Nothing Then
                        pSpell.Name = GetSpellNameBySpellId(pSpell.Id, MySettings.Default.language)
                    End If
                    Dim itm As New ListViewItem({pSpell.Id.ToString, pSpell.Name})
                    itm.Tag = pSpell
                    spellList.BeginInvoke(New AddItemDelegate(AddressOf DelegateControlAdding), itm, spellList)
                Next
            End If
            If Not GlobalVariables.currentViewedCharSet.Skills Is Nothing Then
                For i As Integer = 0 To GlobalVariables.currentViewedCharSet.Skills.Count - 1
                    Dim pSkill As Skill = GlobalVariables.currentViewedCharSet.Skills(i)
                    If Not pSkill.Id = 0 Then
                        If pSkill.Name Is Nothing Then
                            pSkill.Name = GetSkillNameById(pSkill.Id, MySettings.Default.language)
                        End If
                        Dim _
                            itm As _
                                New ListViewItem(
                                    {pSkill.Id.ToString, pSkill.Name, pSkill.Value.ToString, pSkill.Max.ToString})
                        itm.Tag = pSkill
                        skillList.BeginInvoke(New AddItemDelegate(AddressOf DelegateControlAdding), itm, skillList)
                    End If
                Next
            End If
            ThreadExtensions.ScSend(_context, New Action(Of CompletedEventArgs)(AddressOf OnCompleted),
                                    New CompletedEventArgs())
        End Function

        Private Sub DelegateControlAdding(additm As ListViewItem, control As ListView)
            control.Items.Add(additm)
        End Sub
    End Class
End Namespace