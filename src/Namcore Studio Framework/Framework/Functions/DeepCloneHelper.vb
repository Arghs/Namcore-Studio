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
'*      /Filename:      DeepCloneHelper
'*      /Description:   Provides deep cloning services
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.Reflection

Namespace Framework.Functions
    Public Class DeepCloneHelper
    ''' <summary>
    '''     Get the deep clone of an object.
    ''' </summary>
    ''' <typeparam name="T">The type of the obj.</typeparam>
    ''' <param name="obj">It is the object used to deep clone.</param>
    ''' <returns>Return the deep clone.</returns>
                                Public Shared Function DeepClone (Of T)(ByVal obj As T) As T
            If obj Is Nothing Then
                Throw New ArgumentNullException("Object " & "is null!")
            End If
            Return CType(CloneProcedure(obj), T)
        End Function

                                
                                ''' <summary>
                                '''     This method implements deep clone using reflection.
                                ''' </summary>
                                ''' <param name="obj">It is the object used to deep clone.</param>
                                ''' <returns>Return the deep clone.</returns>
                                Private Shared Function CloneProcedure(ByVal obj As Object) As Object
            If obj Is Nothing Then
                Return Nothing
            End If

            Dim type As Type = obj.GetType()

            ' If the type of object is the value type, we will always get a new object when 
            ' the original object is assigned to another variable. So if the type of the 
            ' object is the value type, we just return the object. 
            ' If the string variables contain the same chars, they always refer to the same 
            ' string in the heap. So if the type of the object is string, we also return the 
            ' object.
            If type.IsValueType OrElse type Is GetType(String) Then
                Return obj
                ' If the type of the object is the Array, we use the CreateInstance method to get
                ' a new instance of the array. We also process recursively this method in the 
                ' elements of the original array because the type of the element may be the reference 
                ' type.
            ElseIf type.IsArray Then
                Dim typeElement As Type = type.GetType(type.FullName.Replace("[]", String.Empty))
                Dim array = TryCast(obj, Array)
                Dim copiedArray As Array = array.CreateInstance(typeElement, array.Length)
                For i As Integer = 0 To array.Length - 1
                    ' Get the deep clone of the element in the original array and assign the 
                    ' clone to the new array.
                    copiedArray.SetValue(CloneProcedure(array.GetValue(i)), i)

                Next i
                Return copiedArray
                ' If the type of the object is class, it may contain the reference fields,so we use  
                ' reflection and process recursively this method in the fields of the object to get 
                ' the deep clone of the object. 
            ElseIf type.IsClass Then
                If type.Name = "Bitmap" Then
                    Return obj
                End If
                Dim copiedObject As Object = Activator.CreateInstance(obj.GetType())
                ' Get all FieldInfo.
                Dim fields() As FieldInfo =
                        type.GetFields(BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Instance)
                For Each field As FieldInfo In fields
                    Dim fieldValue As Object = field.GetValue(obj)
                    If fieldValue IsNot Nothing Then
                        ' Get the deep clone of the field in the original object and assign the 
                        ' clone to the field in the new object.
                        field.SetValue(copiedObject, CloneProcedure(fieldValue))
                    End If

                Next field
                Return copiedObject
            Else
                Throw New ArgumentException("The object is unknown type")
            End If
        End Function
    End Class
End Namespace
