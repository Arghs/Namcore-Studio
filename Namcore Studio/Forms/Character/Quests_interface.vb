'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013 Namcore Studio <https://github.com/megasus/Namcore-Studio>
'*
'* This program is free software; you can redistribute it and/or modify it
'* under the terms of the GNU General Public License as published by the
'* Free Software Foundation; either version 2 of the License, or (at your
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
'*      /Filename:      Glyphs_interface
'*      /Description:   Provides an interface to display character's questlog
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Imports NCFramework.Basics
Imports NCFramework.Conversions
Imports NCFramework.GlobalVariables
Imports NCFramework
Imports System.Threading
Imports NCFramework.ResourceHandler

Public Class Quests_interface
    Shared addlst As New List(Of ListViewItem)
    Shared addlst2 As New List(Of ListViewItem)
    Shared arr1 As Array
    Shared arr2 As Array
    Shared completed As Boolean = False
    Shared globPlayer As Character
    Private context As Threading.SynchronizationContext = Threading.SynchronizationContext.Current
    Public Event QSTCompleted As EventHandler(Of CompletedEventArgs)
    Protected Overridable Sub OnCompleted(ByVal e As CompletedEventArgs)
        RaiseEvent QSTCompleted(Me, e)
    End Sub
    Private WithEvents m_handler As New FlowLayoutPanelHandler
    Public Sub prepareInterface(ByVal setId As Integer)
        Dim real_qst_lst As New List(Of Quest)
        Dim qst() As String = currentViewedCharSet.FinishedQuests.Split(","c)
        For i = 0 To qst.Length - 1
            real_qst_lst.Add(New Quest With {.id = TryInt(qst(i)), .rewarded = 1, .explored = 1, .status = 1})
        Next
        If currentViewedCharSet.Quests IsNot Nothing Then
            If currentViewedCharSet.Quests.Count > 0 Then
                For Each player_qst As Quest In currentViewedCharSet.Quests
                    real_qst_lst.Add(player_qst)
                Next
            End If
        End If
        m_handler.doOperate_qst(1, real_qst_lst)
    End Sub
    Public Function continueOperation(ByVal operation_count As Integer, ByVal questLst As List(Of Quest)) As String
        For Each pQuest As Quest In questLst
            Dim str(3) As String
            str(0) = pQuest.id.ToString
            Dim questname As String
            If pQuest.name Is Nothing Then
                questname = GetQuestNameById(pQuest.id)
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
        ThreadExtensions.ScSend(context, New Action(Of CompletedEventArgs)(AddressOf OnCompleted), New CompletedEventArgs())
    End Function
    Delegate Sub AddItemDelegate(itm As ListViewItem)
    Private Sub DelegateControlAdding(additm As ListViewItem)
        qst_lst.Items.Add(additm)
    End Sub
    Private Sub Quests_interface_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim controlLST As List(Of Control)
        controlLST = FindAllChildren()
        For Each item_control As Control In controlLST
            item_control.SetDoubleBuffered()
        Next
    End Sub
    Private ptMouseDownLocation As Point
    Private Sub me_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            ptMouseDownLocation = e.Location
        End If
    End Sub

    Private Sub me_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me.Location = e.Location - ptMouseDownLocation + Location
        End If
    End Sub
    Private Sub highlighter_MouseEnter(sender As Object, e As EventArgs) Handles highlighter1.MouseEnter, highlighter2.MouseEnter
        sender.backgroundimage = My.Resources.highlight
    End Sub

    Private Sub highlighter_MouseLeave(sender As Object, e As EventArgs) Handles highlighter1.MouseLeave, highlighter2.MouseLeave
        sender.backgroundimage = Nothing
    End Sub

    Private Sub highlighter2_Click(sender As Object, e As EventArgs) Handles highlighter2.Click
        Me.Close()
    End Sub

    Private Sub highlighter1_Click(sender As Object, e As EventArgs) Handles highlighter1.Click
        WindowState = FormWindowState.Minimized
    End Sub

    Private Sub header_MouseDown(sender As Object, e As MouseEventArgs) Handles header.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            ptMouseDownLocation = e.Location
        End If
    End Sub
    Private Sub header_MouseMove(sender As Object, e As MouseEventArgs) Handles header.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me.Location = e.Location - ptMouseDownLocation + Location
        End If
    End Sub
  
    Private Sub header_Paint(sender As Object, e As PaintEventArgs) Handles header.Paint

    End Sub

    Private Sub qst_lst_MouseUp(sender As Object, e As MouseEventArgs) Handles qst_lst.MouseUp
        If e.Button = MouseButtons.Right Then
            If qst_lst.SelectedItems.Count = 0 Then Exit Sub
            qstContext.Show(qst_lst, e.X, e.Y)
        End If
    End Sub

    Private Sub qst_lst_SelectedIndexChanged(sender As Object, e As EventArgs) Handles qst_lst.SelectedIndexChanged

    End Sub

    Private Sub RemoveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RemoveToolStripMenuItem.Click
        qst_lst.BeginUpdate()
        For Each qstitm As ListViewItem In qst_lst.SelectedItems
            Dim qst As Quest = qstitm.Tag
            If currentEditedCharSet Is Nothing Then currentEditedCharSet = currentViewedCharSet
            If qst.rewarded = 1 Then
                currentEditedCharSet.FinishedQuests = currentEditedCharSet.FinishedQuests.Replace("," & qst.id.ToString & ",", ",")
            Else
                For Each pquest As Quest In currentEditedCharSet.Quests
                    If pquest.id = qst.id Then
                        currentEditedCharSet.Quests.Remove(pquest)
                    End If
                Next
            End If
            qst_lst.Items.Remove(qstitm)
        Next
        qst_lst.EndUpdate()
    End Sub

    Private Sub finished_0_Click(sender As Object, e As EventArgs) Handles finished_0.Click
        qst_lst.BeginUpdate()
        For Each qstitm As ListViewItem In qst_lst.SelectedItems
            Dim qst As Quest = qstitm.Tag
            If qst.status = 1 Then
                If currentEditedCharSet Is Nothing Then currentEditedCharSet = currentViewedCharSet
                If qst.rewarded = 1 Then
                    currentEditedCharSet.FinishedQuests = currentEditedCharSet.FinishedQuests.Replace("," & qst.id.ToString & ",", ",")
                    qst.status = 0
                    qst.rewarded = 0
                    currentEditedCharSet.Quests.Add(qst)
                    qstitm.SubItems(2).Text = "0"
                    qstitm.SubItems(3).Text = "0"
                Else
                    For Each pquest As Quest In currentEditedCharSet.Quests
                        If pquest.id = qst.id Then
                            pquest.status = 0
                            qstitm.SubItems(2).Text = "0"
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
                If currentEditedCharSet Is Nothing Then currentEditedCharSet = currentViewedCharSet
                    For Each pquest As Quest In currentEditedCharSet.Quests
                        If pquest.id = qst.id Then
                        pquest.status = 1
                        qstitm.SubItems(2).Text = "1"
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
                If currentEditedCharSet Is Nothing Then currentEditedCharSet = currentViewedCharSet
                currentEditedCharSet.FinishedQuests = currentEditedCharSet.FinishedQuests.Replace("," & qst.id.ToString & ",", ",")
                qstitm.SubItems(3).Text = "0"
                qst.rewarded = 0
                currentEditedCharSet.Quests.Add(qst)
            End If
        Next
        qst_lst.EndUpdate()
    End Sub

    Private Sub rewarded_1_Click(sender As Object, e As EventArgs) Handles rewarded_1.Click
        qst_lst.BeginUpdate()
        For Each qstitm As ListViewItem In qst_lst.SelectedItems
            Dim qst As Quest = qstitm.Tag
            If qst.rewarded = 0 Then
                If currentEditedCharSet Is Nothing Then currentEditedCharSet = currentViewedCharSet
                currentEditedCharSet.FinishedQuests = currentEditedCharSet.FinishedQuests & qst.id.ToString & ","
                qstitm.SubItems(2).Text = "1"
                qstitm.SubItems(3).Text = "1"
                For Each pqst As Quest In currentEditedCharSet.Quests
                    If pqst.id = qst.id Then currentEditedCharSet.Quests.Remove(pqst)
                Next
            End If
        Next
        qst_lst.EndUpdate()
    End Sub

    Private Sub add_bt_Click(sender As Object, e As EventArgs) Handles add_bt.Click
        'Add new quest
        Dim retnvalue As Integer = TryInt(InputBox(GetUserMessage("enterqstid"), "Add quest", "0"))
        If Not retnvalue = 0 Then
            If currentEditedCharSet Is Nothing Then currentEditedCharSet = currentViewedCharSet
            If currentEditedCharSet.FinishedQuests.Contains("," & retnvalue.ToString & ",") Then
                MsgBox(GetUserMessage("qstexist"), MsgBoxStyle.Critical, "Error")
                Exit Sub
            End If
            For Each qst As Quest In currentEditedCharSet.Quests
                If qst.id = retnvalue Then
                    MsgBox(GetUserMessage("qstexist"), MsgBoxStyle.Critical, "Error")
                    Exit Sub
                End If
            Next
            Dim rewarded As Integer = TryInt(InputBox(GetUserMessage("enterrewarded"), "Add quest", "0"))
            Dim newqst As New Quest
            newqst.id = retnvalue
            newqst.name = getNameOfQuest(newqst.id)
            If rewarded = 1 Then
                newqst.rewarded = 1
                newqst.status = 1
                currentEditedCharSet.FinishedQuests = currentEditedCharSet.FinishedQuests & "," & retnvalue.ToString
                Dim str(3) As String
                str(0) = retnvalue.ToString
                str(1) = newqst.name
                str(2) = "1"
                str(3) = "1"
                Dim itm As New ListViewItem
                itm.Tag = newqst
                qst_lst.Items.Add(itm)
                MsgBox(GetUserMessage("qstadded"))
            ElseIf rewarded = 0 Then
                newqst.rewarded = 0
                Dim finished As Integer = TryInt(InputBox(GetUserMessage("enterfinished"), "Add quest", "0"))
                If finished = 0 Or finished = 1 Then
                    newqst.status = finished
                    currentEditedCharSet.Quests.Add(newqst)
                    Dim str(3) As String
                    str(0) = retnvalue.ToString
                    str(1) = newqst.name
                    str(2) = finished.ToString
                    str(3) = "0"
                    Dim itm As New ListViewItem
                    itm.Tag = newqst
                    qst_lst.Items.Add(itm)
                    MsgBox(GetUserMessage("qstadded"))
                Else
                    MsgBox(GetUserMessage("invalidentry"), MsgBoxStyle.Critical, "Error")
                    Exit Sub
                End If

            Else
                MsgBox(GetUserMessage("invalidentry"), MsgBoxStyle.Critical, "Error")
                Exit Sub
            End If
        End If


    End Sub
End Class