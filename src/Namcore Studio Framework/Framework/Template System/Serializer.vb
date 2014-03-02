'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013-2014 NamCore Studio <https://github.com/megasus/Namcore-Studio>
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
'*
'* //FileInfo//
'*      /Filename:      Serializer
'*      /Description:   Used to serialize objects for later saving
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.IO
Imports System.IO.Compression
Imports System.Runtime.Serialization.Formatters.Binary
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Modules

Namespace Framework
    Public Class Serializer
        Public Function Serialize(Of T)(ByVal compression As Boolean, ByVal instance As T) As MemoryStream
            Dim fs As Stream = New MemoryStream()
            Try
                Dim bf As New BinaryFormatter
                If compression Then fs = New GZipStream(fs, CompressionMode.Compress)
                bf.Serialize(fs, instance)
                fs.Close()
                fs.Dispose()
                Return fs
            Catch ex As Exception
                fs.Close()
                fs.Dispose()
                LogAppend("Error during serialization: " & ex.ToString, "Serializer_Serialize", True, True)
                Return New MemoryStream
            End Try
        End Function

        Public Function Serialize(Of T)(ByVal instance As T) As MemoryStream
            Return Serialize(False, instance)
        End Function

        Public Shared Function DeSerialize(Of T)(ByVal compression As Boolean,
                                                  ByVal serialString As String, ByVal defaultInstance As T) As T
            GlobalVariables.DeserializationSuccessfull = False
            If serialString = "" Then
                If Not File.Exists(My.Computer.FileSystem.SpecialDirectories.Temp & "/lastset.ncsf") Then
                    Return defaultInstance
                End If
                serialString = My.Computer.FileSystem.SpecialDirectories.Temp & "/lastset.ncsf"
            End If

            Dim fs As Stream = New FileStream(serialString,
                                              FileMode.OpenOrCreate)
            Try
                Dim bf As New BinaryFormatter
                If compression Then fs = New GZipStream(fs, CompressionMode.Decompress)
                DeSerialize = CType(bf.Deserialize(fs), T)
                fs.Close()
                fs.Dispose()
            Catch ex As Exception
                fs.Close()
                fs.Dispose()
                LogAppend("Error during deserialization: " & ex.ToString, "Serializer_DeSerialize", True, True)
                Return defaultInstance
            End Try
        End Function

        Public Function DeSerialize(Of T)(ByVal serialString As String,
                                           ByVal defaultInstance As T) As T

            Return DeSerialize(Of T)(False, serialString, defaultInstance)
        End Function

        Public Function DeSerialize(Of T As New)(ByVal serialString As String) As T
            Return DeSerialize(Of T)(serialString, New T)
        End Function

        Public Function DeSerialize(Of T As New)(
                                                  ByVal compression As Boolean, ByVal serialString As String) As T

            Return DeSerialize(Of T)(compression, serialString, New T)
        End Function
    End Class
End Namespace