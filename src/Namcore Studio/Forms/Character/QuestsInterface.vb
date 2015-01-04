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
'*      /Filename:      QuestsInterface
'*      /Description:   Provides an interface to display character's questlog
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.Linq
Imports NCFramework.My
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Modules
Imports NamCore_Studio.Modules.Interface
Imports NCFramework.Framework.Extension
Imports NamCore_Studio.Forms.Extension
Imports System.Threading
Imports NCFramework.My.Resources
Imports libnc.Provider

Namespace Forms.Character
    Public Class QuestsInterface
        Inherits EventTrigger

        '// Declaration
        Shared _firstuse As Boolean = True
        Shared _loaded As Boolean = False
        Private ReadOnly _context As SynchronizationContext = SynchronizationContext.Current
        Public Event QstCompleted As EventHandler(Of CompletedEventArgs)
        Shared _lstitems As List(Of ListViewItem)
        Private WithEvents _mHandler As New TrdQueueHandler
        Private _cmpFileListViewComparer As ListViewComparer

        Delegate Sub AddItemDelegate(itm As ListViewItem)
        '// Declaration

        Protected Overridable Sub OnCompleted(ByVal e As CompletedEventArgs)
            RaiseEvent QstCompleted(Me, e)
        End Sub

        Public Sub PrepareInterface(ByVal setId As Integer)
            Hide()
            qst_lst.Items.Clear()
            _cmpFileListViewComparer = New ListViewComparer(qst_lst)
            If GlobalVariables.currentEditedCharSet Is Nothing Then
                GlobalVariables.currentEditedCharSet = DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet)
            End If
            Dim realQstLst As List(Of Quest) = (From qst In GlobalVariables.currentEditedCharSet.FinishedQuests Select New Quest With {.Id = qst, .Rewarded = 1, .Explored = 1, .Status = 1}).ToList()
            If GlobalVariables.currentEditedCharSet.Quests IsNot Nothing Then
                If GlobalVariables.currentEditedCharSet.Quests.Count > 0 Then
                    realQstLst.AddRange(GlobalVariables.currentEditedCharSet.Quests)
                End If
            End If
            _mHandler.doOperate_qst(1, realQstLst)
            _loaded = True
        End Sub

        Private Sub QuestCompleted() Handles Me.QstCompleted
            resultstatus_lbl.Text = qst_lst.Items.Count.ToString & MISC_PROFRESULTS
            If _firstuse = True Then
                If _lstitems Is Nothing Then _lstitems = New List(Of ListViewItem)
                For Each itm As ListViewItem In qst_lst.Items
                    Dim itmnew As ListViewItem = CType(itm.Clone(), ListViewItem)
                    _lstitems.Add(itmnew)
                Next
                _firstuse = False
            End If
            Userwait.Close()
            Application.DoEvents()
            Show()
            CloseProcessStatus()
        End Sub

        Private Sub qst_lst_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles qst_lst.ColumnClick
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
            qst_lst.Sort()
        End Sub

        Public Function ContinueOperation(ByVal operationCount As Integer, ByVal questLst As List(Of Quest)) As String
            LogAppend("Loading quests", "Quests_interface_continueOperation", True)
            For Each pQuest As Quest In questLst
                Dim str(3) As String
                str(0) = pQuest.Id.ToString
                Dim questname As String
                If pQuest.Name Is Nothing Then
                    questname = GetQuestTitleById(pQuest.Id, MySettings.Default.language)
                Else
                    questname = pQuest.Name
                End If
                str(1) = questname
                str(2) = pQuest.Status.ToString
                If pQuest.Status <> 1 Then str(2) = "0"
                str(3) = pQuest.Rewarded.ToString
                Dim itm As New ListViewItem(str)
                itm.Tag = pQuest
                qst_lst.BeginInvoke(New AddItemDelegate(AddressOf DelegateControlAdding), itm)
            Next
            ThreadExtensions.ScSend(_context, New Action(Of CompletedEventArgs)(AddressOf OnCompleted),
                                    New CompletedEventArgs())
            Return ""
        End Function

        Private Sub DelegateControlAdding(additm As ListViewItem)
            qst_lst.Items.Add(additm)
        End Sub

        Private Sub Quests_interface_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            AddHandler highlighter2.Click, AddressOf highlighter2_Click
            _loaded = False
            _firstuse = True
            Dim controlLst As List(Of Control)
            controlLst = FindAllChildren()
            For Each itemControl As Control In controlLst
                itemControl.SetDoubleBuffered()
            Next
        End Sub

        Private Sub highlighter2_Click(sender As Object, e As EventArgs)
            qst_lst.Items.Clear()
            Close()
        End Sub

        Private Sub qst_lst_MouseUp(sender As Object, e As MouseEventArgs) Handles qst_lst.MouseUp
            Userwait.Close()
            If e.Button = MouseButtons.Right Then
                If qst_lst.SelectedItems.Count = 0 Then Exit Sub
                qstContext.Show(qst_lst, e.X, e.Y)
            End If
        End Sub

        Private Sub RemoveToolStripMenuItem_Click(sender As Object, e As EventArgs) _
            Handles RemoveToolStripMenuItem.Click
            qst_lst.BeginUpdate()
            For Each qstitm As ListViewItem In qst_lst.SelectedItems
                Dim qst As Quest = CType(qstitm.Tag, Quest)
                If qst.Rewarded = 1 Then
                    GlobalVariables.currentEditedCharSet.FinishedQuests.Remove(qst.Id)
                Else
                    If GlobalVariables.currentEditedCharSet.Quests Is Nothing Then _
                        GlobalVariables.currentEditedCharSet.Quests = New List(Of Quest)()
                    For Each pquest As Quest In _
                        From pquest1 In DeepCloneHelper.DeepClone(GlobalVariables.currentEditedCharSet.Quests)
                            Where pquest1.Id = qst.Id
                        GlobalVariables.currentEditedCharSet.Quests.Remove(pquest)
                    Next
                End If
                For i = 0 To _lstitems.Count - 1
                    Dim qitm As ListViewItem = _lstitems(i)
                    If CType(qitm.Tag, Quest).Id = qst.Id Then
                        _lstitems.Remove(qitm)
                        Exit For
                    End If
                Next
                qst_lst.Items.Remove(qstitm)
            Next
            qst_lst.EndUpdate()
        End Sub

        Private Sub finished_0_Click(sender As Object, e As EventArgs) Handles finished_0.Click
            qst_lst.BeginUpdate()
            For Each qstitm As ListViewItem In qst_lst.SelectedItems
                Dim qst As Quest = CType(qstitm.Tag, Quest)
                If qst.Status = 1 Then
                    If qst.Rewarded = 1 Then
                        GlobalVariables.currentEditedCharSet.FinishedQuests.Remove(qst.Id)
                        qst.Status = 0
                        qst.Rewarded = 0
                        If GlobalVariables.currentEditedCharSet.Quests Is Nothing Then _
                            GlobalVariables.currentEditedCharSet.Quests = New List(Of Quest)()
                        GlobalVariables.currentEditedCharSet.Quests.Add(qst)
                        qstitm.SubItems(2).Text = "0"
                        qstitm.SubItems(3).Text = "0"
                        For Each qitm As ListViewItem In _lstitems
                            Dim subqst As Quest = CType(qitm.Tag, Quest)
                            If subqst.Id = qst.Id Then
                                subqst.Status = 0
                                subqst.Rewarded = 0
                                qitm.Tag = subqst
                            End If
                        Next
                    Else
                        For Each pquest As Quest In GlobalVariables.currentEditedCharSet.Quests
                            If pquest.Id = qst.Id Then
                                pquest.Status = 0
                                qstitm.SubItems(2).Text = "0"
                                For Each qitm As ListViewItem In _lstitems
                                    Dim subqst As Quest = CType(qitm.Tag, Quest)
                                    If subqst.Id = qst.Id Then
                                        subqst.Status = 0
                                        qitm.Tag = subqst
                                    End If
                                Next
                            End If
                        Next
                    End If
                End If
            Next
            qst_lst.EndUpdate()
        End Sub

        Private Sub finished_1_Click(sender As Object, e As EventArgs) Handles finished_1.Click
            qst_lst.BeginUpdate()
            For Each qstitm As ListViewItem In qst_lst.SelectedItems
                Dim qst As Quest = CType(qstitm.Tag, Quest)
                If qst.Status = 0 Then
                    For Each pquest As Quest In GlobalVariables.currentEditedCharSet.Quests
                        If pquest.Id = qst.Id Then
                            pquest.Status = 1
                            qstitm.SubItems(2).Text = "1"
                            For Each qitm As ListViewItem In _lstitems
                                Dim subqst As Quest = CType(qitm.Tag, Quest)
                                If subqst.Id = qst.Id Then
                                    subqst.Status = 1
                                    qitm.Tag = subqst
                                End If
                            Next
                        End If
                    Next
                End If
            Next
            qst_lst.EndUpdate()
        End Sub

        Private Sub rewarded_0_Click(sender As Object, e As EventArgs) Handles rewarded_0.Click
            qst_lst.BeginUpdate()
            For Each qstitm As ListViewItem In qst_lst.SelectedItems
                Dim qst As Quest = CType(qstitm.Tag, Quest)
                If qst.Rewarded = 1 Then
                    GlobalVariables.currentEditedCharSet.FinishedQuests.Remove(qst.Id)
                    qstitm.SubItems(3).Text = "0"
                    qst.Rewarded = 0
                    If GlobalVariables.currentEditedCharSet.Quests Is Nothing Then _
                        GlobalVariables.currentEditedCharSet.Quests = New List(Of Quest)()
                    GlobalVariables.currentEditedCharSet.Quests.Add(qst)
                    For Each qitm As ListViewItem In _lstitems
                        Dim subqst As Quest = CType(qitm.Tag, Quest)
                        If subqst.Id = qst.Id Then
                            subqst.Rewarded = 0
                            qitm.Tag = subqst
                        End If
                    Next
                End If
            Next
            qst_lst.EndUpdate()
        End Sub

        Private Sub rewarded_1_Click(sender As Object, e As EventArgs) Handles rewarded_1.Click
            qst_lst.BeginUpdate()
            For Each qstitm As ListViewItem In qst_lst.SelectedItems
                Dim qst As Quest = CType(qstitm.Tag, Quest)
                If qst.Rewarded = 0 Then
                    GlobalVariables.currentEditedCharSet.FinishedQuests.SafeAdd(qst.Id)
                    qstitm.SubItems(2).Text = "1"
                    qstitm.SubItems(3).Text = "1"
                    If GlobalVariables.currentEditedCharSet.Quests Is Nothing Then _
                        GlobalVariables.currentEditedCharSet.Quests = New List(Of Quest)()
                    For Each pqst As Quest In From pqst1 In GlobalVariables.currentEditedCharSet.Quests Where pqst1.Id = qst.Id
                        GlobalVariables.currentEditedCharSet.Quests.Remove(pqst)
                    Next
                    For Each qitm As ListViewItem In _lstitems
                        Dim subqst As Quest = CType(qitm.Tag, Quest)
                        If subqst.Id = qst.Id Then
                            subqst.Status = 1
                            subqst.Rewarded = 1
                            qitm.Tag = subqst
                        End If
                    Next
                End If
            Next
            qst_lst.EndUpdate()
        End Sub

        Private Sub add_bt_Click(sender As Object, e As EventArgs) Handles add_bt.Click
            '// Add new quest
            Dim retnvalue As Integer = TryInt(InputBox(MSG_ENTERQUESTID, MSG_ADDQUEST, "0"))
            If Not retnvalue = 0 Then
                If GlobalVariables.currentEditedCharSet.FinishedQuests.Contains(retnvalue) Then
                    MsgBox(MSG_QUESTALREADYPRESENT, MsgBoxStyle.Critical, MSG_ERROR)
                    Exit Sub
                End If
                For Each qst As Quest In GlobalVariables.currentEditedCharSet.Quests
                    If qst.Id = retnvalue Then
                        MsgBox(MSG_QUESTALREADYPRESENT, MsgBoxStyle.Critical, MSG_ERROR)
                        Exit Sub
                    End If
                Next
                Dim qrewarded As Integer = TryInt(InputBox(MSG_SETQSTREWARDEDSTATUS, MSG_ADDQUEST, "0"))
                Dim newqst As New Quest
                newqst.Id = retnvalue
                newqst.Name = GetQuestTitleById(newqst.Id, MySettings.Default.language)
                If qrewarded = 1 Then
                    newqst.Rewarded = 1
                    newqst.Status = 1
                    GlobalVariables.currentEditedCharSet.FinishedQuests.SafeAdd(retnvalue)
                    Dim str(3) As String
                    str(0) = retnvalue.ToString
                    str(1) = newqst.Name
                    str(2) = "1"
                    str(3) = "1"
                    Dim itm As New ListViewItem(str)
                    itm.Tag = newqst
                    qst_lst.Items.Add(itm)
                    qst_lst.Update()
                    MsgBox(MSG_QUESTADDED)
                ElseIf qrewarded = 0 Then
                    newqst.Rewarded = 0
                    Dim qfinished As Integer = TryInt(InputBox(MSG_SETQSTFINISHEDSTATUS,
                                                               MSG_ADDQUEST,
                                                               "0"))
                    If qfinished = 0 Or qfinished = 1 Then
                        newqst.Status = qfinished
                        If GlobalVariables.currentEditedCharSet.Quests Is Nothing Then _
                            GlobalVariables.currentEditedCharSet.Quests = New List(Of Quest)()
                        GlobalVariables.currentEditedCharSet.Quests.Add(newqst)
                        Dim str(3) As String
                        str(0) = retnvalue.ToString
                        str(1) = newqst.Name
                        str(2) = qfinished.ToString
                        str(3) = "0"
                        Dim itm As New ListViewItem(str)
                        itm.Tag = newqst
                        qst_lst.Items.Add(itm)
                        qst_lst.Update()
                        _lstitems.Add(itm)
                        MsgBox(MSG_QUESTADDED)
                    Else
                        MsgBox(MSG_INVALIDENTRY, MsgBoxStyle.Critical, MSG_ERROR)
                        Exit Sub
                    End If

                Else
                    MsgBox(MSG_INVALIDENTRY, MsgBoxStyle.Critical, MSG_ERROR)
                    Exit Sub
                End If
            End If
        End Sub

        Private Sub search_tb_Enter(sender As Object, e As EventArgs) Handles search_tb.Enter
            search_tb.Text = ""
        End Sub

        Private Sub search_tb_Leave(sender As Object, e As EventArgs) Handles search_tb.Leave
            If search_tb.Text = "" Then
                search_tb.Text = MSG_ENTERQUESTID
            End If
        End Sub

        Private Sub search_tb_TextChanged(sender As Object, e As EventArgs) Handles search_tb.TextChanged
            If _loaded = False Then Exit Sub
            If search_tb.Text = MSG_ENTERQUESTID Or search_tb.Text = "" Then
                If _lstitems Is Nothing Then Exit Sub
                If _lstitems.Count = 0 Then Exit Sub
                qst_lst.Items.Clear()
                For Each itm As ListViewItem In _lstitems
                    qst_lst.Items.Add(itm)
                Next
                qst_lst.Update()
                resultstatus_lbl.Text = qst_lst.Items.Count.ToString & MISC_PROFRESULTS
                Exit Sub
            End If
            Dim value As Integer = TryInt(search_tb.Text)
            Dim resultcounter As Integer = 0
            Dim itmstoshow As New List(Of ListViewItem)
            If Not value = 0 Then
                qst_lst.Items.Clear()
                For Each itm As ListViewItem In _lstitems
                    Dim qst As Quest = CType(itm.Tag, Quest)
                    If qst.Id.ToString.Contains(CType(value, String)) Then
                        resultcounter += 1
                        itmstoshow.Add(itm)
                    End If
                Next
                For Each qstitm In itmstoshow
                    qst_lst.Items.Add(qstitm)
                Next
                resultstatus_lbl.Text = resultcounter.ToString & MISC_PROFRESULTS
            Else
                qst_lst.Items.Clear()
                For Each itm As ListViewItem In _lstitems
                    qst_lst.Items.Add(itm)
                Next
                search_tb.Text = MSG_ENTERQUESTID
            End If
            qst_lst.Update()
        End Sub
    End Class
End Namespace