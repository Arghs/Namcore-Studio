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
    Private Sub close_bt_Click(sender As System.Object, e As System.EventArgs) Handles close_bt.Click
        Me.Close()
    End Sub

    Private Sub Process_status_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Control.CheckForIllegalCrossThreadCalls = False

    End Sub
   

    Public Sub ArmoryWorker_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) _
        Handles ArmoryWorker.DoWork
        Dim d As Data2Thread = CType(e.Argument, Data2Thread)
        Dim charLst As List(Of String) = d.charLST
        LoadArmoryCharacters(charLst)
    End Sub
    Public Sub UpdateGui()
        cancel_bt.Visible = ArmoryWorker.IsBusy
        progressbar.Visible = ArmoryWorker.IsBusy
        If ArmoryWorker.IsBusy Then progressbar.Value = 0
    End Sub
    Public Sub ArmoryWorker_RunWorkerCompleted( _
        ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) _
        Handles ArmoryWorker.RunWorkerCompleted
        UpdateGui()
        If e.Cancelled Then
            process_tb.Text = "Canceled"
            Return
        End If
        Interface_Operator.prepareLive_armory()
    End Sub

    Public Sub TransferWorker_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) _
    Handles TransferWorker.DoWork
        Dim d As Live_View.Data2Thread = CType(e.Argument, Live_View.Data2Thread)
        Dim lite As Boolean = d.lite
        handleMigrationRequests(lite)
        MsgBox("hi")
    End Sub
    Public Sub UpdateGuiTrans()
        cancel_bt.Visible = TransferWorker.IsBusy
        progressbar.Visible = TransferWorker.IsBusy
        If TransferWorker.IsBusy Then progressbar.Value = 0
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
    Public Sub appendProc()
        Try
            process_tb.Text = proccessTXT
            process_tb.SelectionStart = process_tb.Text.Length
            process_tb.ScrollToCaret()
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try

    End Sub
    Public Sub ArmoryWorker_ProgressChanged( _
         ByVal sender As Object, ByVal e As ProgressChangedEventArgs) _
         Handles ArmoryWorker.ProgressChanged
        progressbar.Value = e.ProgressPercentage
        process_tb.Text = proccessTXT
        process_tb.SelectionStart = process_tb.Text.Length
        process_tb.ScrollToCaret()
    End Sub

    Public Sub TransferWorker_ProgressChanged( _
        ByVal sender As Object, ByVal e As ProgressChangedEventArgs) _
        Handles TransferWorker.ProgressChanged
        progressbar.Value = e.ProgressPercentage
        process_tb.Text = proccessTXT
        process_tb.SelectionStart = process_tb.Text.Length
        process_tb.ScrollToCaret()
    End Sub
    Private Sub cancel_bt_Click(sender As Object, e As EventArgs) Handles cancel_bt.Click

    End Sub
End Class