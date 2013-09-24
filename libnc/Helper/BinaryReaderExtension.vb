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
'*      /Filename:      BinaryReaderExtensions
'*      /Description:   Binary Reader Extensions
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.IO
Imports System.Runtime.CompilerServices

Namespace Helper

    Public Module BinaryReaderExtensions
        Public ReadValue As New Dictionary(Of Type, Func(Of BinaryReader, Object))() From { _
            {GetType(Boolean), Function(br) br.ReadBoolean()}, _
            {GetType(SByte), Function(br) br.ReadSByte()}, _
            {GetType(Byte), Function(br) br.ReadByte()}, _
            {GetType(Short), Function(br) br.ReadInt16()}, _
            {GetType(UShort), Function(br) br.ReadUInt16()}, _
            {GetType(Integer), Function(br) br.ReadInt32()}, _
            {GetType(UInteger), Function(br) br.ReadUInt32()}, _
            {GetType(Single), Function(br) br.ReadSingle()}, _
            {GetType(Long), Function(br) br.ReadInt64()}, _
            {GetType(ULong), Function(br) br.ReadUInt64()}, _
            {GetType(Double), Function(br) br.ReadDouble()} _
            }

        <Extension>
        Public Function Read(Of T)(br As BinaryReader) As T
            Return DirectCast(ReadValue(GetType(T))(br), T)
        End Function
    End Module
End Namespace