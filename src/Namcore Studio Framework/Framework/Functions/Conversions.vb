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
'*      /Filename:      Conversions
'*      /Description:   Includes frequently used functions for converting various objects
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.Drawing
Imports System.IO
Imports NCFramework.Framework.Logging
Imports System.Text

Namespace Framework.Functions
    Public Module Conversions
        Public Function ConvertListToString(ByVal list As List(Of String)) As String
            LogAppend("Converting a list to a string", "Conversions_ConvertListToString", False)
            Try
                Dim builder As StringBuilder = New StringBuilder()
                For Each val As String In List
                    builder.Append(val).Append("|")
                Next
                Return builder.ToString()
            Catch ex As Exception
                LogAppend(
                    "Error while converting list to string! -> Returning nothing -> Exception is: ###START###" &
                    ex.ToString() & "###END###", "Conversions_ConvertListToString", False, True)
                Return ""
            End Try
        End Function

        Public Function ConvertStringToList(ByVal mystring As String) As List(Of String)
            LogAppend("Converting a string to a list", "Conversions_ConvertStringToList", False)
            Try
                Dim stringlist As String() = mystring.Split("|"c)
                Dim position As Integer = 0
                Dim xlist As List(Of String) = New List(Of String)
                Do
                    Try
                        Dim temp As String = stringlist(position)
                        If Not temp = "" Then xlist.Add(temp)
                        position += 1
                    Catch ex As Exception
                        Exit Do
                    End Try
                Loop
                Return xlist
            Catch ex As Exception
                LogAppend(
                    "Error while converting string to list! -> Returning nothing -> Exception is: ###START###" &
                    ex.ToString() & "###END###", "Conversions_ConvertStringToList", False, True)
                Dim emptylist As List(Of String) = New List(Of String)
                Return emptylist
            End Try
        End Function

        Public Function TryInt(ByVal mystring As String) As Integer
            Try
                Dim parseResult As Integer = CInt(Integer.TryParse(mystring, Nothing))
                If parseResult = 0 Then
                    Return 0
                Else
                    Return CInt(mystring)
                End If
            Catch ex As Exception
                LogAppend("Exception during TryInt() : " & ex.ToString(), "Conversions_TryInt", False, True)
                Return 0
            End Try
        End Function

        Public Function ConvertImageToString(ByVal myimg As Image) As String
            LogAppend("Converting image to string", "Conversions_ConvertImageToString", False)
            If myimg Is Nothing Then Return ""
            Dim result As String = String.Empty
            Try
                Dim img As Image = myimg
                Using ms As MemoryStream = New MemoryStream
                    img.Save(ms, img.RawFormat)
                    Dim bytes() As Byte = ms.ToArray()
                    result = Convert.ToBase64String(bytes)
                End Using
            Catch ex As Exception
                LogAppend("Exception during converting process: " & ex.ToString(), "Conversions_ConvertImageToString",
                          False,
                          True)
            End Try
            Return result
        End Function

        Public Function ConvertStringToImage(ByVal base64String As String) As Image
            LogAppend("Converting string to image", "Conversions_ConvertStringToImage", False)
            If Base64String = "" Then Return Nothing
            Dim img As Image = Nothing
            If Base64String Is Nothing Then
                LogAppend("Base64String is nothing!", "Conversions_ConvertStringToImage", False, True)
            Else
                Try
                    Dim bytes() As Byte = Convert.FromBase64String(base64String)
                    img = Image.FromStream(New MemoryStream(bytes))
                Catch ex As Exception
                    LogAppend("Exception during converting process: " & ex.ToString(),
                              "Conversions_ConvertStringToImage",
                              False, True)
                End Try
            End If
            Return img
        End Function
    End Module
End Namespace