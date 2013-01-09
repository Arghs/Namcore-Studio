Imports Namcore_Studio.EventLogging
Public Class Basics
    Public Shared Sub SetTemporaryCharacterInformation(ByVal field As String, ByVal value As String, ByVal targetSetId As Integer)

    End Sub
    Public Shared Sub AppendTemporaryCharacterInformation(ByVal field As String, ByVal value As String, ByVal targetSetId As Integer)

    End Sub
    Public Shared Function GetTemporaryCharacterInformation(ByVal field As String, ByVal targetSetId As Integer) As String

    End Function
    Public Shared Sub AbortProcess()

    End Sub
    Public Shared Function splitString(ByVal source As String, ByVal start As String, ByVal ending As String) As String
        LogAppend("Splitting a string. Sourcelength/-/Start/-/End: " & source.Length.ToString & "/-/" & start & "/-/" & ending, "Basics_splitString", False)
        Try
            Dim quellcode As String = source
            Dim _start As String = start
            Dim _end As String = ending
            Dim quellcodeSplit As String
            quellcodeSplit = Split(quellcode, _start, 5)(1)
            Return Split(quellcodeSplit, _end, 6)(0)
        Catch ex As Exception
            LogAppend("Error while splitting string! -> Returning nothing -> Exception is: ###START###" & ex.ToString() & "###END###", "Basics_splitString", False, True)
            Return Nothing
        End Try


    End Function
End Class
