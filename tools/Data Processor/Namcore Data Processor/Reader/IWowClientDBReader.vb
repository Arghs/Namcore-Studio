'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013-2015 NamCore Studio <https://github.com/megasus/Namcore-Studio>
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

Namespace Reader
    Public Interface IWowClientDbReader
        ReadOnly Property RecordsCount() As Integer
        ReadOnly Property FieldsCount() As Integer
        ReadOnly Property RecordSize() As Integer
        ReadOnly Property StringTableSize() As Integer
        ReadOnly Property StringTable() As Dictionary(Of Integer, String)
        Function GetRowAsByteArray(row As Integer) As Byte()
        Default ReadOnly Property Item(row As Integer) As BinaryReader
    End Interface
End Namespace