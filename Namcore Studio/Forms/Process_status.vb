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
'*      /Filename:      Process_status
'*      /Description:   Provides an interface to display operation status
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Imports Namcore_Studio.GlobalVariables
Imports System.ComponentModel
Imports Namcore_Studio.ArmoryHandler
Imports Namcore_Studio.Interface_Operator
Imports Namcore_Studio.Basics
Imports Namcore_Studio.TransmissionHandler
Imports Namcore_Studio.Armory_interface

Public Class Process_status
    Public WithEvents ArmoryWorker As BackgroundWorker
    Public WithEvents TransferWorker As BackgroundWorker
    Public Shared proccessTXT As String
    Private Delegate Sub UpdateTextHandler(ByVal Text As String)
    Private Sub close_bt_Click(sender As System.Object, e As System.EventArgs) Handles close_bt.Click
        Me.Close()
    End Sub

    Private Sub Process_status_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load


    End Sub
   

    Public Sub ArmoryWorker_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) _
        Handles ArmoryWorker.DoWork
        Dim d As Data2Thread = CType(e.Argument, Data2Thread)
        Dim charLst As List(Of String) = d.charLST
        '  LoadArmoryCharacters(charLst)
    End Sub
    Public Sub UpdateGui()
        cancel_bt.Visible = ArmoryWorker.IsBusy


    End Sub
    Public Sub ArmoryWorker_RunWorkerCompleted( _
        ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) _
        Handles ArmoryWorker.RunWorkerCompleted
        UpdateGui()
        If e.Cancelled Then
            process_tb.Text = "Canceled"
            Return
        End If
        'Dim prepLive As New Interface_Operator
        'prepLive.prepareLive_armory()
    End Sub

    Public Sub TransferWorker_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) _
    Handles TransferWorker.DoWork
        Dim d As Live_View.Data2Thread = CType(e.Argument, Live_View.Data2Thread)
        Dim lite As Boolean = d.lite
        Dim m_transmissionHndler As New TransmissionHandler
        m_transmissionHndler.handleMigrationRequests(lite)
        MsgBox("hi")
    End Sub
    Public Sub UpdateGuiTrans()
        cancel_bt.Visible = TransferWorker.IsBusy


    End Sub
    Public Sub TransferWorker_RunWorkerCompleted( _
        ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) _
        Handles TransferWorker.RunWorkerCompleted
        UpdateGuiTrans()
        If e.Cancelled Then
            process_tb.Text = "Canceled"
            Return
        End If
        MsgBox("Transfer completed!")
        Live_View.Show()
    End Sub
    Private Delegate Sub AppendTextBoxDelegate(ByVal txt As String)
    Public Sub appendProc(ByVal status As String)
        If process_tb.InvokeRequired Then
            process_tb.Invoke(New AppendTextBoxDelegate(AddressOf appendProc), New Object() {proccessTXT})
        Else
            process_tb.Text = proccessTXT
        End If
      

    End Sub
    Public Sub ArmoryWorker_ProgressChanged( _
         ByVal sender As Object, ByVal e As ProgressChangedEventArgs) _
         Handles ArmoryWorker.ProgressChanged

        process_tb.Text = proccessTXT
        'process_tb.SelectionStart = process_tb.Text.Length
        'process_tb.ScrollToCaret()
    End Sub

    Public Sub TransferWorker_ProgressChanged( _
        ByVal sender As Object, ByVal e As ProgressChangedEventArgs) _
        Handles TransferWorker.ProgressChanged

        process_tb.Text = proccessTXT
        'process_tb.SelectionStart = process_tb.Text.Length
        'process_tb.ScrollToCaret()
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
  
End Class