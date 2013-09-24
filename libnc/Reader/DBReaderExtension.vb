'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013 Namcore Studio <https://github.com/megasus/Namcore-Studio>
'*
'* This program is free software; you can redistribute it and/or modify it
'* under the terms of the GNU General Public License as published by the
'* Free Software Foundation; either version 2 of the License, or (at your
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
'*
'* //FileInfo//
'*      /Filename:      DbReaderExtension
'*      /Description:   DBC reader extension
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.Runtime.CompilerServices
Imports System.IO
Imports System.Text

Namespace Reader

    Public Module DbReaderExtensions
      
        <Extension>
        Public Function ReadSByte(br As BinaryReader, count As Integer) As SByte()
            Dim arr = New SByte(count - 1) {}
            For i As Integer = 0 To count - 1
                arr(i) = br.ReadSByte()
            Next

            Return arr
        End Function

        <Extension>
        Public Function ReadByte(br As BinaryReader, count As Integer) As Byte()
            Dim arr = New Byte(count - 1) {}
            For i As Integer = 0 To count - 1
                arr(i) = br.ReadByte()
            Next

            Return arr
        End Function

        <Extension>
        Public Function ReadInt32(br As BinaryReader, count As Integer) As Integer()
            Dim arr = New Integer(count - 1) {}
            For i As Integer = 0 To count - 1
                arr(i) = br.ReadInt32()
            Next

            Return arr
        End Function

        <Extension>
        Public Function ReadUInt32(br As BinaryReader, count As Integer) As UInteger()
            Dim arr = New UInteger(count - 1) {}
            For i As Integer = 0 To count - 1
                arr(i) = br.ReadUInt32()
            Next

            Return arr
        End Function

        <Extension>
        Public Function ReadSingle(br As BinaryReader, count As Integer) As Single()
            Dim arr = New Single(count - 1) {}
            For i As Integer = 0 To count - 1
                arr(i) = br.ReadSingle()
            Next

            Return arr
        End Function

        <Extension>
        Public Function ReadInt64(br As BinaryReader, count As Integer) As Long()
            Dim arr = New Long(count - 1) {}
            For i As Integer = 0 To count - 1
                arr(i) = br.ReadInt64()
            Next

            Return arr
        End Function

        <Extension>
        Public Function ReadUInt64(br As BinaryReader, count As Integer) As ULong()
            Dim arr = New ULong(count - 1) {}
            For i As Integer = 0 To count - 1
                arr(i) = br.ReadUInt64()
            Next

            Return arr
        End Function

        <Extension>
        Public Function ReadCString(br As BinaryReader) As String
            Dim tmpString As New StringBuilder()
            Dim tmpChar As Char = br.ReadChar()
            Dim tmpEndChar As Char = Convert.ToChar(Encoding.UTF8.GetString(New Byte() {0}))

            While tmpChar <> tmpEndChar
                tmpString.Append(tmpChar)
                tmpChar = br.ReadChar()
            End While

            Return tmpString.ToString()
        End Function

        <Extension>
        Public Function ReadString(br As BinaryReader, count As Integer) As String
            Dim stringArray As Byte() = br.ReadBytes(count)
            Return Encoding.ASCII.GetString(stringArray)
        End Function
    End Module
End Namespace