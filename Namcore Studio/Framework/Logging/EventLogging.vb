Imports Namcore_Studio.GlobalVariables

Public Class EventLogging

    Public Shared Sub LogAppend(ByVal _event As String, ByVal location As String, Optional userOut As Boolean = False, Optional iserror As Boolean = False)
        If iserror = False Then
            If userOut = True Then
                eventlog = eventlog & vbNewLine & "[" & Now.TimeOfDay.ToString & "]" & _event
                eventlog_full = eventlog_full & vbNewLine & "[" & Date.Today.ToString & " " & Now.TimeOfDay.ToString & "]" & _event
            Else

            End If
        Else

        End If
    End Sub

    Public Shared Sub LogClear()
        eventlog = ""
    End Sub
End Class
