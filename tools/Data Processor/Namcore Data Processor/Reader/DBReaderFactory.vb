Imports System.IO
Imports Namcore_Data_Processor.Reader

Public Class DBReaderFactory
    Public Shared Function GetReader(file As String) As IWowClientDBReader
        Dim reader As IWowClientDBReader
        Dim ext = Path.GetExtension(file).ToUpperInvariant()
        If ext = ".DBC" Then
            reader = New DbReader(file)
        ElseIf ext = ".DB2" Then
            reader = New DB2Reader(file)
        Else
            Throw New InvalidDataException([String].Format("Unknown file type {0}", ext))
        End If
        Return reader
    End Function
End Class
