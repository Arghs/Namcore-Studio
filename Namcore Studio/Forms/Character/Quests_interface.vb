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
        Dim qst() As String = currentViewedCharSet.FinishedQuests.Split(","c)
        Dim cnt As Integer = 0
        Dim par1 As Integer = qst.Length / 2 - 1
        Dim par2 As Integer = qst.Length - par1 - 1
        Dim par1qst(par1 - 1) As String
        Dim par2qst(qst.Length - par1 - 1) As String
        For i = 0 To par1 - 1
            par1qst(i) = qst(i)
        Next
        For i = 0 To par2
            par2qst(i) = qst(par1 + i)
        Next
        completed = False
        m_handler.doOperate_qst(1, qst)
        'm_handler.doOperate_qst(1, par1qst)
        'm_handler.doOperate_qst(2, par2qst)
    End Sub
    Public Function continueOperation(ByVal operation_count As Integer, ByVal quests() As String) As String
        If operation_count = 2 Then
            Thread.Sleep(2000)
        End If
        Dim cnt As Integer = 0
        While cnt < quests.Length
            Dim str(3) As String
            str(0) = quests(cnt)
            Dim qstname As String = GetQuestNameById(TryInt(str(0)))
                If qstname = "error" Then
                    str(1) = "not loaded" 'getNameOfQuest(str(0))
                Else
                    str(1) = qstname
                End If
                str(2) = "1"
                str(3) = "1"
                Dim itm As New ListViewItem(str)
                itm.Tag = TryInt(str(0))
                qst_lst.BeginInvoke(New AddItemDelegate(AddressOf DelegateControlAdding), itm)
                cnt += 1
        End While
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

    Private Sub qst_lst_SelectedIndexChanged(sender As Object, e As EventArgs) Handles qst_lst.SelectedIndexChanged

    End Sub
End Class