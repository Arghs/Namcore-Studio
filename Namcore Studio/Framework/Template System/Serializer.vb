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
'*      /Filename:      Serializer
'*      /Description:   Used to serialize objects for later saving
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Imports System.Runtime.Serialization.Formatters.Binary
Imports System.IO
Imports System.IO.Compression
Imports System.Text.Encoding
Public Class Serializer
    Public Shared Function Serialize(Of T)(ByVal compression As Boolean, ByVal instance As T) As MemoryStream

        Try
            Dim fs As Stream = New MemoryStream()
            Dim bf As New BinaryFormatter
            If compression Then fs = New GZipStream(fs, CompressionMode.Compress)

            bf.Serialize(fs, instance)
            fs.Close()
            
            Return fs
        Catch ex As Exception
            MessageBox.Show(ex.Message, Application.ProductName, _
              MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function
    
    Public Shared Function Serialize(Of T)(ByVal instance As T) As MemoryStream
        Return Serialize(False, instance)
    End Function

    Public Shared Function DeSerialize(Of T)(ByVal compression As Boolean, _
      ByVal serialString As String, ByVal defaultInstance As T) As T

        'Try
        '    Dim b As Byte() = UTF8.GetBytes(serialString)
        '    Dim s As New MemoryStream(b)
        '    Dim fs As Stream = s
        '    Dim bf As New BinaryFormatter
        '    If compression Then fs = New GZipStream(fs, CompressionMode.Decompress)
        '    DeSerialize = CType(bf.Deserialize(fs), T)
        '    fs.Close()
        'Catch ex As Exception
        '    Return defaultInstance
        'End Try
        Try
            If Not File.Exists(My.Computer.FileSystem.SpecialDirectories.Desktop & "/tryit.txt") Then
                Return defaultInstance
            End If
            Dim fs As Stream = New FileStream(My.Computer.FileSystem.SpecialDirectories.Desktop & "/tryit.txt", FileMode.OpenOrCreate)
            Dim bf As New BinaryFormatter
            If compression Then fs = New GZipStream(fs, CompressionMode.Decompress)

            DeSerialize = CType(bf.Deserialize(fs), T)
            fs.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, Application.ProductName, _
              MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Public Shared Function DeSerialize(Of T)(ByVal serialString As String, _
      ByVal defaultInstance As T) As T

        Return DeSerialize(Of T)(False, serialString, defaultInstance)
    End Function

    Public Shared Function DeSerialize(Of T As New)(ByVal serialString As String) As T
        Return DeSerialize(Of T)(serialString, New T)
    End Function

    Public Shared Function DeSerialize(Of T As New)( _
      ByVal compression As Boolean, ByVal serialString As String) As T

        Return DeSerialize(Of T)(compression, serialString, New T)
    End Function
End Class