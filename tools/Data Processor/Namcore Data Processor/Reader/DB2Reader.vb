Imports System.IO
Imports System.Text

Namespace Reader
    Public Class DB2Reader
        Implements IWowClientDbReader
        Private Const HeaderSize As Integer = 48
        Private Const DB2FmtSig As UInteger = &H32424457
        Private m_RecordsCount As Integer
        Private m_FieldsCount As Integer
        Private m_RecordSize As Integer
        Private m_StringTableSize As Integer
        Private m_StringTable As Dictionary(Of Integer, String)
        Private m_rows As Byte()()

        Public Property FieldsCount As Integer Implements IWowClientDbReader.FieldsCount
            Get
                Return m_FieldsCount
            End Get
            Private Set
                m_FieldsCount = Value
            End Set
        End Property

        Default Public ReadOnly Property Item(row As Integer) As BinaryReader Implements IWowClientDbReader.Item
            Get
                Return New BinaryReader(New MemoryStream(m_rows(row)), Encoding.UTF8)
            End Get
        End Property

        Public Property RecordsCount As Integer Implements IWowClientDbReader.RecordsCount
            Get
                Return m_RecordsCount
            End Get
            Private Set
                m_RecordsCount = Value
            End Set
        End Property

        Public Property RecordSize As Integer Implements IWowClientDbReader.RecordSize
            Get
                Return m_RecordSize
            End Get
            Private Set
                m_RecordSize = Value
            End Set
        End Property

        Public Property StringTable As Dictionary(Of Integer, String) Implements IWowClientDbReader.StringTable
            Get
                Return m_StringTable
            End Get
            Private Set
                m_StringTable = Value
            End Set
        End Property

        Public Property StringTableSize As Integer Implements IWowClientDbReader.StringTableSize
            Get
                Return m_StringTableSize
            End Get
            Private Set
                m_StringTableSize = Value
            End Set
        End Property

        Public Function GetRowAsByteArray(row As Integer) As Byte() Implements IWowClientDbReader.GetRowAsByteArray
            Return m_rows(row)
        End Function

        Public Sub New(fileName As String)
            Using reader = BinaryReaderExtensions.FromFile(fileName)
                If reader.BaseStream.Length < HeaderSize Then
                    Throw New InvalidDataException([String].Format("File {0} is corrupted!", fileName))
                End If
                If reader.ReadUInt32() <> DB2FmtSig Then
                    Throw New InvalidDataException([String].Format("File {0} isn't valid DBC file!", fileName))
                End If
                RecordsCount = reader.ReadInt32()
                FieldsCount = reader.ReadInt32()
                RecordSize = reader.ReadInt32()
                StringTableSize = reader.ReadInt32()
                ' WDB2 specific fields
                Dim tableHash As UInteger = reader.ReadUInt32()
                ' new field in WDB2
                Dim build As UInteger = reader.ReadUInt32()
                ' new field in WDB2
                Dim unk1 As UInteger = reader.ReadUInt32()
                ' new field in WDB2
                If build > 12880 Then
                    ' new extended header
                    Dim MinId As Integer = reader.ReadInt32()
                    ' new field in WDB2
                    Dim MaxId As Integer = reader.ReadInt32()
                    ' new field in WDB2
                    Dim locale As Integer = reader.ReadInt32()
                    ' new field in WDB2
                    Dim unk5 As Integer = reader.ReadInt32()
                    ' new field in WDB2
                    If MaxId <> 0 Then
                        Dim diff = MaxId - MinId + 1
                        ' blizzard is weird people...
                        reader.ReadBytes(diff * 4)
                        ' an index for rows
                        ' a memory allocation bank
                        reader.ReadBytes(diff * 2)
                    End If
                End If
                m_rows = New Byte(RecordsCount - 1)() {}
                For i As Integer = 0 To RecordsCount - 1
                    m_rows(i) = reader.ReadBytes(RecordSize)
                Next
                Dim stringTableStart As Integer = CInt(reader.BaseStream.Position)
                StringTable = New Dictionary(Of Integer, String)()
                While reader.BaseStream.Position <> reader.BaseStream.Length
                    Dim index As Integer = CInt(reader.BaseStream.Position) - stringTableStart
                    StringTable(index) = reader.ReadStringNull()
                End While
            End Using
        End Sub
    End Class
End Namespace