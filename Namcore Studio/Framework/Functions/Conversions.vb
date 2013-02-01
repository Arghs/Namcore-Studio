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
    Public Shared Function ConvertStringToList(ByVal _string As String) As List(Of String)
        LogAppend("Converting a string to a list", "Conversions_ConvertStringToList", False)
        Try
            Dim stringlist As String() = _string.Split("|"c)
            Dim position As Integer = 0
            Dim xlist As List(Of String) = New List(Of String)
            Do
                Try
                    Dim temp As String = stringlist(position)
                    If Not temp = "" Then xlist.Add(temp)
                    position += 1
                Catch ex As Exception
                    Exit Do
                End Try
            Loop
            Return xlist
        Catch ex As Exception
            LogAppend("Error while converting string to list! -> Returning nothing -> Exception is: ###START###" & ex.ToString() & "###END###", "Conversions_ConvertStringToList", False, True)
            Dim emptylist As List(Of String) = New List(Of String)
            Return emptylist
        End Try
    End Function
    Public Shared Function TryInt(ByVal _string As String) As Integer
        Dim parseResult As Integer = Integer.TryParse(_string, Nothing)
        If parseResult = 0 Then
            Return 0
        Else
            Return tryint(parseResult)
        End If
        End Function
End Class
