Imports Namcore_Studio.SpellItem_Information
Module date2timestamp
    ''' <summary>
    ''' timestamp converter
    ''' </summary>
    <System.Runtime.CompilerServices.Extension()>
    Public Function toDate(ByRef Stamp As Integer) As DateTime
        Try
            Dim Span As TimeSpan
            Dim Startdate As Date = #1/1/1970#
            If Stamp = 0 Then Return Startdate
            Span = New TimeSpan(0, 0, Stamp)
            Return Startdate.Add(Span)
        Catch ex As Exception
            Return DateTime.Today
        End Try

    End Function
    Public Function toTimeStamp(ByRef dt As DateTime) As Integer
        Try
            Dim Startdate As DateTime = #1/1/1970#
            Dim Spanne As TimeSpan
            Spanne = dt.Subtract(Startdate)
            Return CType(Math.Abs(Spanne.TotalSeconds()), Integer)
        Catch ex As Exception
            Return 0
        End Try
    End Function
End Module
