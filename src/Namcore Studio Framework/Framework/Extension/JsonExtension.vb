'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013-2015 NamCore Studio <https://github.com/megasus/Namcore-Studio>
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
Imports Newtonsoft.Json.Linq

Namespace Framework.Extension
    Module JsonExtension
        <Extension()>
        Public Function HasChildren(ByVal prop As JProperty) As Boolean
            prop.CreateReader()
            If prop.Values.Count > 1 Then
                Return True
            Else
                Return False
            End If
        End Function

        <Extension()>
        Public Function GetChildren(ByVal prop As JProperty) As List(Of JProperty)
            prop.CreateReader()
            Return prop.Values().Cast(Of JProperty)().ToList()
        End Function

        <Extension()>
        Public Function GetChild(ByVal prop As JProperty, ByVal itemName As String) As JProperty
            prop.CreateReader()
            Return prop.Values().Cast(Of JProperty)().ToList().Find(Function(jProperty) jProperty.Name = itemName)
        End Function

        <Extension()>
        Public Function HasItem(ByVal prop As JProperty, ByVal name As String) As Boolean
            prop.CreateReader()
            Return prop.GetChildren().Find(Function(jtoken) jtoken.Name = name) IsNot Nothing
        End Function

        <Extension()>
        Public Function GetValue(ByVal prop As JProperty, ByVal name As String) As String
            prop.CreateReader()
            Return CType(prop.GetChildren().Single(Function(jtoken) jtoken.Name = name).Value, String)
        End Function

        <Extension()>
        Public Function GetValues(ByVal prop As JProperty, ByVal name As String) As String()
            prop.CreateReader()
            Return CType(prop.GetChildren().Single(Function(jtoken) jtoken.Name = name).Value, JArray).ToStringArray()
        End Function

        <Extension()>
        Private Function ToStringArray(ByVal jarr As JArray) As String()
            Dim str(jarr.Count - 1) As String
            For i = 0 To jarr.Count - 1
                str(i) = CType(jarr(i), JValue).ToString()
            Next i
            Return str
        End Function
    End Module
End Namespace