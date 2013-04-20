Public Class Process_status

    Private Sub close_bt_Click(sender As System.Object, e As System.EventArgs) Handles close_bt.Click
        Me.Close()
    End Sub

    Private Sub Process_status_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub
    Public Sub appendEvent(ByVal _status As String)
        process_tb.AppendText(_status)
    End Sub
End Class