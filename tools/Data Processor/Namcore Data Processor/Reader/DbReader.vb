'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013-2014 NamCore Studio <https://github.com/megasus/Namcore-Studio>
'* Copyright (C) 2010-2013 TOM_RUS' dbcviewer <https://github.com/tomrus88/dbcviewer>
'*
'* This program is free software; you can redistribute it and/or modify it
'* under the terms of the GNU General Public License as published by the
'* Free Software Foundation; either version 3 of the License, or (at your
'* option) any later version.
'*
'* This program is distributed in the hope that it will be useful, but WITHOUT
'* ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
'* FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for
'* more details.
'*
'* You should have received a copy of the GNU General Public License along
'* with this program. If not, see <http://www.gnu.org/licenses/>.
'*
'* Developed by Alcanmage/megasus
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.IO
Imports System.Text

Namespace Reader
    Public Class DbReader
        Implements IWowClientDbReader
        Private Const HeaderSize As UInteger = 20
        Private Const DbcFmtSig As UInteger = &H43424457
        ' WDBC
        Public Property RecordsCount() As Integer Implements IWowClientDbReader.RecordsCount
            Get
                Return _mRecordsCount
            End Get
            Private Set
                _mRecordsCount = Value
            End Set
        End Property

        Private _mRecordsCount As Integer

        Public Property FieldsCount() As Integer Implements IWowClientDbReader.FieldsCount
            Get
                Return _mFieldsCount
            End Get
            Private Set
                _mFieldsCount = Value
            End Set
        End Property

        Private _mFieldsCount As Integer

        Public Property RecordSize() As Integer Implements IWowClientDbReader.RecordSize
            Get
                Return _mRecordSize
            End Get
            Private Set
                _mRecordSize = Value
            End Set
        End Property

        Private _mRecordSize As Integer

        Public Property StringTableSize() As Integer Implements IWowClientDbReader.StringTableSize
            Get
                Return _mStringTableSize
            End Get
            Private Set
                _mStringTableSize = Value
            End Set
        End Property

        Private _mStringTableSize As Integer

        Public Property StringTable() As Dictionary(Of Integer, String) Implements IWowClientDbReader.StringTable
            Get
                Return _mStringTable
            End Get
            Private Set
                _mStringTable = Value
            End Set
        End Property

        Private _mStringTable As Dictionary(Of Integer, String)
        Private ReadOnly _mRows As Byte()()

        Public Function GetRowAsByteArray(row As Integer) As Byte() Implements IWowClientDbReader.GetRowAsByteArray
            Return _mRows(row)
        End Function

        Default Public ReadOnly Property Item(row As Integer) As BinaryReader Implements IWowClientDbReader.Item
            Get
                Return New BinaryReader(New MemoryStream(_mRows(row)), Encoding.UTF8)
            End Get
        End Property

        Public Sub New(fileName As String)
            Using reader = FromFile(fileName)
                If reader.BaseStream.Length < HeaderSize Then
                    Throw New InvalidDataException([String].Format("File {0} is corrupted!", fileName))
                End If
                If reader.ReadUInt32() <> DbcFmtSig Then
                    Throw New InvalidDataException([String].Format("File {0} isn't valid DBC file!", fileName))
                End If
                RecordsCount = reader.ReadInt32()
                FieldsCount = reader.ReadInt32()
                RecordSize = reader.ReadInt32()
                StringTableSize = reader.ReadInt32()
                _mRows = New Byte(RecordsCount - 1)() {}
                For i As Integer = 0 To RecordsCount - 1
                    _mRows(i) = reader.ReadBytes(RecordSize)
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