'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013-2016 NamCore Studio <https://github.com/megasus/Namcore-Studio>
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
'*      /Filename:      JsonExtension
'*      /Description:   Extens JProperty
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.Runtime.CompilerServices

Namespace Framework.Extension
    Module ObjectExtension
        <Extension>
        Public Sub SafeAddRange (Of T)(ByRef lst As List(Of T), collection As IEnumerable(Of T))
            If lst Is Nothing Then lst = New List(Of T)()
            lst.AddRange(collection)
        End Sub

        <Extension>
        Public Sub SafeAdd (Of T)(ByRef lst As List(Of T), value As T)
            If lst Is Nothing Then lst = New List(Of T)()
            lst.Add(value)
        End Sub
    End Module
End Namespace