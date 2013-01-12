Imports System.Text
Imports Namcore_Studio.EventLogging
Public Class Conversions

    Public Shared Function ConvertListToString(ByVal _list As List(Of String)) As String
        LogAppend("Converting a list to a string", "Conversions_ConvertListToString", False)
        Try
            Dim builder As StringBuilder = New StringBuilder()
            For Each val As String In _list
                builder.Append(val).Append("|")
            Next
            Return builder.ToString()
        Catch ex As Exception
            LogAppend("Error while converting list to string! -> Returning nothing -> Exception is: ###START###" & ex.ToString() & "###END###", "Conversions_ConvertListToString", False, True)
            Return ""
        End Try
    End Function
    Public Shared Function TryInt(ByVal _string As String) As Integer
        Dim parseResult As Integer = Integer.TryParse(_string, Nothing)
        If parseResult = 0 Then
            Return 0
        Else
            Return CInt(parseResult)
        End If
        End Function
End Class
