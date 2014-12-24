Imports System.IO
Imports System.Globalization
Imports Namcore_Data_Processor.Reader

Module DbHandler
    Public Function ReadDb(ByVal name As String, ByVal dic As Dictionary(Of Integer, String())) As DataTable
        Try
            Log("Reading file...", LogLevel.LOW, , False)
            Dim file As String = Environment.CurrentDirectory & name
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
            If mReader.FieldsCount < dic.Last().Key Then
                Log(" failed!", LogLevel.LOW, False)
                Log("FieldCount does not match internal structure", LogLevel.CRITICAL)
                Log("Expected at least " & dic.Last().Key.ToString() & ", file has " & mReader.FieldsCount.ToString(),
                    LogLevel.CRITICAL)
                Return Nothing
            End If
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
                            Dim key As Integer = br.ReadInt32()
                            If mReader.StringTable.ContainsKey(key) Then
                                dr(j) = mReader.StringTable(key)
                            Else
                                Log("Key '" & key.ToString() & "' not found in string table", LogLevel.WARNING)
                            End If
                        Case Else
                            dr(j) = br.ReadInt32()
                    End Select

                Next j
                mDataTable.Rows.Add(dr)
            Next i
            Log(" finished!", LogLevel.LOW, False, False)
            Log(" - Entries: " & mDataTable.Rows.Count.ToString(), LogLevel.LOW, False)
            Return mDataTable
        Catch ex As Exception
            Log(" failed!", LogLevel.LOW, False)
            Log("Exception occured while reading db: " & ex.ToString(), LogLevel.CRITICAL)
            Return Nothing
        End Try
    End Function
End Module
