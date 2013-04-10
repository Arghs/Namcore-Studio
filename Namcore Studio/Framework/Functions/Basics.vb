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
'*      /Filename:      Basics
'*      /Description:   Includes basic and frequently used functions
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Imports Namcore_Studio.EventLogging
Imports System.Net

Public Class Basics
    Public Shared Sub SetTemporaryCharacterInformation(ByVal field As String, ByVal value As String, ByVal targetSetId As Integer)

    End Sub
    Public Shared Sub AppendTemporaryCharacterInformation(ByVal field As String, ByVal value As String, ByVal targetSetId As Integer)

    End Sub
    Public Shared Sub SetTCI_Item(ByVal itm As Item, ByVal targetSetId As Integer)

    End Sub
    Public Shared Function GetTemporaryCharacterInformation(ByVal field As String, ByVal targetSetId As Integer) As String

    End Function
    Public Shared Sub AbortProcess()

    End Sub
    Public Shared Function splitString(ByVal source As String, ByVal start As String, ByVal ending As String) As String
        LogAppend("Splitting a string. Sourcelength/-/Start/-/End: " & source.Length.ToString & "/-/" & start & "/-/" & ending, "Basics_splitString", False)
        Try
            Dim quellcode As String = source
            Dim _start As String = start
            Dim _end As String = ending
            Dim quellcodeSplit As String
            quellcodeSplit = Split(quellcode, _start, 5)(1)
            Return Split(quellcodeSplit, _end, 6)(0)
        Catch ex As Exception
            LogAppend("Error while splitting string! -> Returning nothing -> Exception is: ###START###" & ex.ToString() & "###END###", "Basics_splitString", False, True)
            Return Nothing
        End Try
    End Function
    Public Shared Function splitList(ByVal source As String, ByVal category As String) As String
        LogAppend("Splitting a list. Sourcelength/-/Start/-/End: " & source.Length.ToString & "/-/" & category, "Basics_splitList", False)
        Try
            Dim quellcode As String = source
            Dim _start As String = "<" & category & ">"
            Dim _end As String = "</" & category & ">"
            Dim quellcodeSplit As String
            quellcodeSplit = Split(quellcode, _start, 5)(1)
            quellcodeSplit = Split(quellcodeSplit, _end, 6)(0)
            Return quellcodeSplit
        Catch ex As Exception
            LogAppend("Error while splitting list! -> Returning nothing -> Exception is: ###START###" & ex.ToString() & "###END###", "Basics_splitList", False, True)
            Return Nothing
        End Try
    End Function
    Public Shared Function LoadImageFromUrl(ByRef url As String) As Image
        Try
            Dim request As HttpWebRequest = DirectCast(HttpWebRequest.Create(url), HttpWebRequest)
            Dim response As HttpWebResponse = DirectCast(request.GetResponse, HttpWebResponse)
            Dim img As Image = Image.FromStream(response.GetResponseStream())
            response.Close()
            Return img
        Catch ex As Exception
            Return Nothing
        End Try

    End Function
End Class
