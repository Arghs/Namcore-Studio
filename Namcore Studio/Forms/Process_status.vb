Imports Namcore_Studio.GlobalVariables
Public Class Process_status

    Private Sub close_bt_Click(sender As System.Object, e As System.EventArgs) Handles close_bt.Click
        Me.Close()
    End Sub

    Private Sub Process_status_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Control.CheckForIllegalCrossThreadCalls = False
    End Sub
 
    Public Shared Sub appendEvent(ByVal _status As String)
        process_tb.AppendText(_status)
    End Sub
End Class