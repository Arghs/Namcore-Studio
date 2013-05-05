Imports Namcore_Studio.GlobalVariables
Imports System.ComponentModel
Imports Namcore_Studio.Armory_interface
Imports Namcore_Studio.ArmoryHandler
Imports Namcore_Studio.Interface_Operator
Public Class Process_status
    Public WithEvents ArmoryWorker As BackgroundWorker
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
        Dim prepLive As New Interface_Operator
        prepLive.prepareLive_armory()
    End Sub
    Public Sub ArmoryWorker_ProgressChanged( _
         ByVal sender As Object, ByVal e As ProgressChangedEventArgs) _
         Handles ArmoryWorker.ProgressChanged
        progressbar.Value = e.ProgressPercentage
        process_tb.Text = proccessTXT
        process_tb.SelectionStart = process_tb.Text.Length
        process_tb.ScrollToCaret()
    End Sub
    Private Sub cancel_bt_Click(sender As Object, e As EventArgs) Handles cancel_bt.Click

    End Sub
End Class