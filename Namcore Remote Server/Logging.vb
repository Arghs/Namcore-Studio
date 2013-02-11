Public Class Logging
    Public Shared Sub LogMessage(ByVal Message As String, ByVal LogFileName As String)
        Try
            Dim oStream As IO.StreamWriter = System.IO.File.AppendText(LogFileName + ".log")
            oStream.WriteLine(Message)

            oStream.Close()

        Catch ex As Exception

        End Try
    End Sub
End Class
