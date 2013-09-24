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
'*      /Filename:      DbReader
'*      /Description:   DBC reader
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.IO

Imports libnc.Helper

Namespace Reader

    Public Class DbReader
        Public Shared Function Read(Of T As New)(dbcFile As Byte()) As List(Of T)
            Dim tempList As List(Of T) = Nothing

            Try
                Using dbReader = New BinaryReader(New MemoryStream(dbcFile))
                    Dim header As New DbHeader() With {
                            .Signature = dbReader.ReadString(4), _
                            .RecordCount = dbReader.Read(Of UInteger)(), _
                            .FieldCount = dbReader.Read(Of UInteger)(), _
                            .RecordSize = dbReader.Read(Of UInteger)(), _
                            .StringBlockSize = dbReader.Read(Of UInteger)() _
                            }

                    If header.IsValidDb2File Then
                        ' ReSharper disable UnusedVariable
                        Dim hash = dbReader.Read(Of UInteger)()
                        Dim wowBuild = dbReader.Read(Of UInteger)()
                        Dim unknown = dbReader.Read(Of UInteger)()
                        Dim min = dbReader.Read(Of Integer)()
                        Dim max = dbReader.Read(Of Integer)()
                        Dim locale = dbReader.Read(Of Integer)()
                        Dim unknown2 = dbReader.Read(Of Integer)()
                        ' ReSharper restore UnusedVariable
                        If max <> 0 Then
                            Dim diff = (max - min) + 1

                            dbReader.ReadBytes(diff * 4)
                            dbReader.ReadBytes(diff * 2)
                        End If
                    End If

                    If header.IsValidDbcFile OrElse header.IsValidDb2File Then
                        tempList = New List(Of T)()
                        Dim fields = GetType(T).GetFields()
                        Const lastStringOffset As Integer = 0
                        Const lastString As String = ""

                        For i = 0 To header.RecordCount - 1
                            Dim newObj As New T()

                            For Each f In fields
                                Select Case f.FieldType.Name
                                    Case "SByte"
                                        f.SetValue(newObj, dbReader.ReadSByte())
                                        Exit Select
                                    Case "Byte"
                                        f.SetValue(newObj, dbReader.ReadByte())
                                        Exit Select
                                    Case "Int32"
                                        f.SetValue(newObj, dbReader.ReadInt32())
                                        Exit Select
                                    Case "UInt32"
                                        f.SetValue(newObj, dbReader.ReadUInt32())
                                        Exit Select
                                    Case "Int64"
                                        f.SetValue(newObj, dbReader.ReadInt64())
                                        Exit Select
                                    Case "UInt64"
                                        f.SetValue(newObj, dbReader.ReadUInt64())
                                        Exit Select
                                    Case "Single"
                                        f.SetValue(newObj, dbReader.ReadSingle())
                                        Exit Select
                                    Case "Boolean"
                                        f.SetValue(newObj, dbReader.ReadBoolean())
                                        Exit Select
                                    Case "SByte[]"
                                        f.SetValue(newObj,
                                                   dbReader.ReadSByte(DirectCast(f.GetValue(newObj), SByte()).Length))
                                        Exit Select
                                    Case "Byte[]"
                                        f.SetValue(newObj, dbReader.ReadByte(DirectCast(f.GetValue(newObj), Byte()).Length))
                                        Exit Select
                                    Case "Int32[]"
                                        f.SetValue(newObj,
                                                   dbReader.ReadInt32(DirectCast(f.GetValue(newObj), Integer()).Length))
                                        Exit Select
                                    Case "UInt32[]"
                                        f.SetValue(newObj,
                                                   dbReader.ReadUInt32(DirectCast(f.GetValue(newObj), UInteger()).Length))
                                        Exit Select
                                    Case "Single[]"
                                        f.SetValue(newObj,
                                                   dbReader.ReadSingle(DirectCast(f.GetValue(newObj), Single()).Length))
                                        Exit Select
                                    Case "Int64[]"
                                        f.SetValue(newObj, dbReader.ReadInt64(DirectCast(f.GetValue(newObj), Long()).Length))
                                        Exit Select
                                    Case "UInt64[]"
                                        f.SetValue(newObj,
                                                   dbReader.ReadUInt64(DirectCast(f.GetValue(newObj), ULong()).Length))
                                        Exit Select
                                    Case "String"
                                        If True Then
                                            Dim stringOffset = dbReader.ReadUInt32()

                                            If stringOffset <> lastStringOffset Then
                                                Dim currentPos = dbReader.BaseStream.Position
                                                Dim stringStart = (header.RecordCount * header.RecordSize) + 20 + stringOffset
                                                dbReader.BaseStream.Seek(stringStart, 0)

                                                f.SetValue(newObj, dbReader.ReadCString())
                                                dbReader.BaseStream.Seek(currentPos, 0)
                                            Else
                                                f.SetValue(newObj, lastString)
                                            End If

                                            Exit Select
                                        End If
                                    Case Else
                                        dbReader.BaseStream.Position += 4
                                        Exit Select
                                End Select
                            Next

                            tempList.Add(newObj)
                        Next
                    End If
                End Using

                CliDB.Count += 1
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try

            Return tempList
        End Function
    End Class
End Namespace