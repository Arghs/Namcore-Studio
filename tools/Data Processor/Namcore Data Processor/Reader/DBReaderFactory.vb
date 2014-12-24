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

Namespace Reader
    Public Class DbReaderFactory
        Public Shared Function GetReader(file As String) As IWowClientDbReader
            Dim reader As IWowClientDbReader
            Dim ext = Path.GetExtension(file).ToUpperInvariant()
            If ext = ".DBC" Then
                reader = New DbReader(file)
            ElseIf ext = ".DB2" Then
                reader = New Db2Reader(file)
            Else
                Throw New InvalidDataException([String].Format("Unknown file type {0}", ext))
            End If
            Return reader
        End Function
    End Class
End Namespace