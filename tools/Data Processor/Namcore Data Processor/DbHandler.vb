Imports System.IO
Imports System.Globalization
Imports Namcore_Data_Processor.Reader

Module DbHandler
    Public Function ReadDb(ByVal name As String, ByVal dic As Dictionary(Of Integer, String())) As DataTable
        Try
            Dim file As String = "D:\World of Warcraft\3.3.5a\dbc\" & name
            Dim mReader As IWowClientDbReader = DBReaderFactory.GetReader(file)
            Dim mDataTable As New DataTable(Path.GetFileName(file))
            mDataTable.Locale = CultureInfo.InvariantCulture
            For z = 0 To mReader.FieldsCount - 1
                If dic.ContainsKey(z) Then
                    mDataTable.Columns.Add(dic(z)(1))
                Else
                    mDataTable.Columns.Add()
                End If
            Next
            For i = 0 To mReader.RecordsCount - 1
                Dim dr As DataRow = mDataTable.NewRow()
                Dim br As BinaryReader = mReader(i)
                For j = 0 To mReader.FieldsCount - 1
                    If Not dic.ContainsKey(j) Then
                        dr(j) = br.ReadInt32()
                        Continue For
                    End If
                    Select Case dic(j)(0)
                        Case "int"
                            dr(j) = br.ReadInt32()
                        Case "string"
                            dr(j) = mReader.StringTable(br.ReadInt32())
                        Case Else
                            dr(j) = br.ReadInt32()
                    End Select

                Next j
                mDataTable.Rows.Add(dr)
            Next i
            Return mDataTable
        Catch ex As Exception
            Log("Exception occured while reading db: " & ex.ToString(), LogLevel.CRITICAL)
            Return Nothing
        End Try
    End Function
End Module
