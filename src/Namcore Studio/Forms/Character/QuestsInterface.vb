'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013 NamCore Studio <https://github.com/megasus/Namcore-Studio>
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
Imports NCFramework.My
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Modules
Imports NamCore_Studio.Modules.Interface
Imports NCFramework.Framework.Extension
Imports NamCore_Studio.Forms.Extension
Imports System.Threading
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

        Delegate Sub AddItemDelegate(itm As ListViewItem)
        '// Declaration

        Protected Overridable Sub OnCompleted(ByVal e As CompletedEventArgs)
            RaiseEvent QstCompleted(Me, e)
        End Sub

        Public Sub PrepareInterface(ByVal setId As Integer)
            Hide()
            Dim realQstLst As New List(Of Quest)
            If GlobalVariables.currentEditedCharSet Is Nothing Then
                GlobalVariables.currentEditedCharSet = DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet)
            End If
            Dim qst() As String = GlobalVariables.currentEditedCharSet.FinishedQuests.Split(","c)
            For i = 0 To qst.Length - 1
                realQstLst.Add(New Quest With {.id = TryInt(qst(i)), .rewarded = 1, .explored = 1, .status = 1})
            Next
            If GlobalVariables.currentEditedCharSet.Quests IsNot Nothing Then
                If GlobalVariables.currentEditedCharSet.Quests.Count > 0 Then
                    realQstLst.AddRange(GlobalVariables.currentEditedCharSet.Quests)
                End If
            End If
            _mHandler.doOperate_qst(1, realQstLst)
            _loaded = True
        End Sub

        Private Sub QuestCompleted() Handles Me.QstCompleted
            resultstatus_lbl.Text = qst_lst.Items.Count.ToString & " results!"
            If _firstuse = True Then
                If _lstitems Is Nothing Then _lstitems = New List(Of ListViewItem)
                For Each itm As ListViewItem In qst_lst.Items
                    Dim itmnew As ListViewItem = itm.Clone()
                    _lstitems.Add(itmnew)
                Next
                _firstuse = False
            End If
            Userwait.Close()
            Application.DoEvents()
            Show()
            CloseProcessStatus()
        End Sub

        Public Function ContinueOperation(ByVal operationCount As Integer, ByVal questLst As List(Of Quest)) As String
            LogAppend("Loading quests", "Quests_interface_continueOperation", True)
            For Each pQuest As Quest In questLst
                Dim str(3) As String
                str(0) = pQuest.id.ToString
                Dim questname As String
                If pQuest.name Is Nothing Then
                    questname = GetQuestTitleById(pQuest.Id, MySettings.Default.language)
                Else
                    questname = pQuest.name
                End If
                str(1) = questname
                str(2) = pQuest.status.ToString
                str(3) = pQuest.rewarded.ToString
                Dim itm As New ListViewItem(str)
                itm.Tag = pQuest
                qst_lst.BeginInvoke(New AddItemDelegate(AddressOf DelegateControlAdding), itm)
            Next
            ThreadExtensions.ScSend(_context, New Action(Of CompletedEventArgs)(AddressOf OnCompleted),
                                    New CompletedEventArgs())
            ' ReSharper disable VBWarnings::BC42105
        End Function
        ' ReSharper restore VBWarnings::BC42105

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
                Dim qst As Quest = qstitm.Tag
                If qst.rewarded = 1 Then
                    GlobalVariables.currentEditedCharSet.FinishedQuests =
                        GlobalVariables.currentEditedCharSet.FinishedQuests.Replace("," & qst.id.ToString & ",", ",")
                Else
                    If GlobalVariables.currentEditedCharSet.Quests Is Nothing Then _
                        GlobalVariables.currentEditedCharSet.Quests = New List(Of Quest)()
                    For Each pquest As Quest In GlobalVariables.currentEditedCharSet.Quests
                        If pquest.id = qst.id Then
                            GlobalVariables.currentEditedCharSet.Quests.Remove(pquest)
                        End If
                    Next
                End If
                For Each qitm As ListViewItem In _lstitems
                    If qitm.Tag.id = qst.id Then
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
                Dim qst As Quest = qstitm.Tag
                If qst.status = 1 Then
                    If qst.rewarded = 1 Then
                        GlobalVariables.currentEditedCharSet.FinishedQuests =
                            GlobalVariables.currentEditedCharSet.FinishedQuests.Replace("," & qst.id.ToString & ",", ",")
                        qst.status = 0
                        qst.rewarded = 0
                        If GlobalVariables.currentEditedCharSet.Quests Is Nothing Then _
                            GlobalVariables.currentEditedCharSet.Quests = New List(Of Quest)()
                        GlobalVariables.currentEditedCharSet.Quests.Add(qst)
                        qstitm.SubItems(2).Text = "0"
                        qstitm.SubItems(3).Text = "0"
                        For Each qitm As ListViewItem In _lstitems
                            Dim subqst As Quest = qitm.Tag
                            If subqst.id = qst.id Then
                                subqst.status = 0
                                subqst.rewarded = 0
                                qitm.Tag = subqst
                            End If
                        Next
                    Else
                        For Each pquest As Quest In GlobalVariables.currentEditedCharSet.Quests
                            If pquest.id = qst.id Then
                                pquest.status = 0
                                qstitm.SubItems(2).Text = "0"
                                For Each qitm As ListViewItem In _lstitems
                                    Dim subqst As Quest = qitm.Tag
                                    If subqst.id = qst.id Then
                                        subqst.status = 0
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
                Dim qst As Quest = qstitm.Tag
                If qst.status = 0 Then
                    For Each pquest As Quest In GlobalVariables.currentEditedCharSet.Quests
                        If pquest.id = qst.id Then
                            pquest.status = 1
                            qstitm.SubItems(2).Text = "1"
                            For Each qitm As ListViewItem In _lstitems
                                Dim subqst As Quest = qitm.Tag
                                If subqst.id = qst.id Then
                                    subqst.status = 1
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
                Dim qst As Quest = qstitm.Tag
                If qst.rewarded = 1 Then
                    GlobalVariables.currentEditedCharSet.FinishedQuests =
                        GlobalVariables.currentEditedCharSet.FinishedQuests.Replace("," & qst.id.ToString & ",", ",")
                    qstitm.SubItems(3).Text = "0"
                    qst.rewarded = 0
                    If GlobalVariables.currentEditedCharSet.Quests Is Nothing Then _
                        GlobalVariables.currentEditedCharSet.Quests = New List(Of Quest)()
                    GlobalVariables.currentEditedCharSet.Quests.Add(qst)
                    For Each qitm As ListViewItem In _lstitems
                        Dim subqst As Quest = qitm.Tag
                        If subqst.id = qst.id Then
                            subqst.rewarded = 0
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
                Dim qst As Quest = qstitm.Tag
                If qst.rewarded = 0 Then
                    GlobalVariables.currentEditedCharSet.FinishedQuests =
                        GlobalVariables.currentEditedCharSet.FinishedQuests & qst.id.ToString & ","
                    qstitm.SubItems(2).Text = "1"
                    qstitm.SubItems(3).Text = "1"
                    If GlobalVariables.currentEditedCharSet.Quests Is Nothing Then _
                        GlobalVariables.currentEditedCharSet.Quests = New List(Of Quest)()
                    For Each pqst As Quest In GlobalVariables.currentEditedCharSet.Quests
                        If pqst.id = qst.id Then GlobalVariables.currentEditedCharSet.Quests.Remove(pqst)
                    Next
                    For Each qitm As ListViewItem In _lstitems
                        Dim subqst As Quest = qitm.Tag
                        If subqst.id = qst.id Then
                            subqst.status = 1
                            subqst.rewarded = 1
                            qitm.Tag = subqst
                        End If
                    Next
                End If
            Next
            qst_lst.EndUpdate()
        End Sub

        Private Sub add_bt_Click(sender As Object, e As EventArgs) Handles add_bt.Click
            'Add new quest
            Dim retnvalue As Integer = TryInt(InputBox(ResourceHandler.GetUserMessage("enterqstid"), "Add quest", "0"))
            If Not retnvalue = 0 Then
                If GlobalVariables.currentEditedCharSet.FinishedQuests.Contains("," & retnvalue.ToString & ",") Then
                    MsgBox(ResourceHandler.GetUserMessage("qstexist"), MsgBoxStyle.Critical, "Error")
                    Exit Sub
                End If
                For Each qst As Quest In GlobalVariables.currentEditedCharSet.Quests
                    If qst.id = retnvalue Then
                        MsgBox(ResourceHandler.GetUserMessage("qstexist"), MsgBoxStyle.Critical, "Error")
                        Exit Sub
                    End If
                Next
                Dim qrewarded As Integer = TryInt(InputBox(ResourceHandler.GetUserMessage("enterrewarded"), "Add quest",
                                                           "0"))
                Dim newqst As New Quest
                newqst.id = retnvalue
                newqst.Name = GetQuestTitleById(newqst.Id, MySettings.Default.language)
                If qrewarded = 1 Then
                    newqst.rewarded = 1
                    newqst.status = 1
                    GlobalVariables.currentEditedCharSet.FinishedQuests =
                        GlobalVariables.currentEditedCharSet.FinishedQuests & "," & retnvalue.ToString
                    Dim str(3) As String
                    str(0) = retnvalue.ToString
                    str(1) = newqst.name
                    str(2) = "1"
                    str(3) = "1"
                    Dim itm As New ListViewItem
                    itm.Tag = newqst
                    qst_lst.Items.Add(itm)
                    MsgBox(ResourceHandler.GetUserMessage("qstadded"))
                ElseIf qrewarded = 0 Then
                    newqst.rewarded = 0
                    Dim qfinished As Integer = TryInt(InputBox(ResourceHandler.GetUserMessage("enterfinished"),
                                                               "Add quest",
                                                               "0"))
                    If qfinished = 0 Or qfinished = 1 Then
                        newqst.status = qfinished
                        If GlobalVariables.currentEditedCharSet.Quests Is Nothing Then _
                            GlobalVariables.currentEditedCharSet.Quests = New List(Of Quest)()
                        GlobalVariables.currentEditedCharSet.Quests.Add(newqst)
                        Dim str(3) As String
                        str(0) = retnvalue.ToString
                        str(1) = newqst.name
                        str(2) = finished.ToString
                        str(3) = "0"
                        Dim itm As New ListViewItem
                        itm.Tag = newqst
                        qst_lst.Items.Add(itm)
                        _lstitems.Add(itm)
                        MsgBox(ResourceHandler.GetUserMessage("qstadded"))
                    Else
                        MsgBox(ResourceHandler.GetUserMessage("invalidentry"), MsgBoxStyle.Critical, "Error")
                        Exit Sub
                    End If

                Else
                    MsgBox(ResourceHandler.GetUserMessage("invalidentry"), MsgBoxStyle.Critical, "Error")
                    Exit Sub
                End If
            End If
        End Sub

        Private Sub search_tb_Enter(sender As Object, e As EventArgs) Handles search_tb.Enter
            search_tb.Text = ""
        End Sub

        Private Sub search_tb_Leave(sender As Object, e As EventArgs) Handles search_tb.Leave
            If search_tb.Text = "" Then
                search_tb.Text = "Enter quest id"
            End If
        End Sub

        Private Sub search_tb_TextChanged(sender As Object, e As EventArgs) Handles search_tb.TextChanged
            If _loaded = False Then Exit Sub
            If search_tb.Text = "Enter quest id" Or search_tb.Text = "" Then
                If _lstitems Is Nothing Then Exit Sub
                If _lstitems.Count = 0 Then Exit Sub
                qst_lst.Items.Clear()
                For Each itm As ListViewItem In _lstitems
                    qst_lst.Items.Add(itm)
                Next
                qst_lst.Update()
                resultstatus_lbl.Text = qst_lst.Items.Count.ToString & " results!"
                Exit Sub
            End If
            Dim value As Integer = TryInt(search_tb.Text)
            Dim resultcounter As Integer = 0
            Dim itmstoshow As New List(Of ListViewItem)
            If Not value = 0 Then
                qst_lst.Items.Clear()
                For Each itm As ListViewItem In _lstitems
                    Dim qst As Quest = itm.Tag
                    If qst.id.ToString.Contains(value) Then
                        resultcounter += 1
                        itmstoshow.Add(itm)
                    End If
                Next
                For Each qstitm In itmstoshow
                    qst_lst.Items.Add(qstitm)
                Next
                resultstatus_lbl.Text = resultcounter.ToString & " results!"
            Else
                qst_lst.Items.Clear()
                For Each itm As ListViewItem In _lstitems
                    qst_lst.Items.Add(itm)
                Next
                search_tb.Text = "Enter quest id"
            End If
            qst_lst.Update()
        End Sub
    End Class
End Namespace