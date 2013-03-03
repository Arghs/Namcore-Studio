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
'*      /Filename:      Conversions
'*      /Description:   Includes frequently used functions for converting various objects
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Imports System.Text
Imports Namcore_Studio.EventLogging
Imports System.Resources

Public Class Conversions

    Public Shared Function ConvertListToString(ByVal _list As List(Of String)) As String
        LogAppend("Converting a list to a string", "Conversions_ConvertListToString", False)
        Try
            Dim builder As StringBuilder = New StringBuilder()
            For Each val As String In _list
                builder.Append(val).Append("|")
            Next
            Return builder.ToString()
        Catch ex As Exception
            LogAppend("Error while converting list to string! -> Returning nothing -> Exception is: ###START###" & ex.ToString() & "###END###", "Conversions_ConvertListToString", False, True)
            Return ""
        End Try
    End Function
    Public Shared Function ConvertStringToList(ByVal _string As String) As List(Of String)
        LogAppend("Converting a string to a list", "Conversions_ConvertStringToList", False)
        Try
            Dim stringlist As String() = _string.Split("|"c)
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
            LogAppend("Error while converting string to list! -> Returning nothing -> Exception is: ###START###" & ex.ToString() & "###END###", "Conversions_ConvertStringToList", False, True)
            Dim emptylist As List(Of String) = New List(Of String)
            Return emptylist
        End Try
    End Function
    Public Shared Function TryInt(ByVal _string As String) As Integer
        Dim parseResult As Integer = Integer.TryParse(_string, Nothing)
        If parseResult = 0 Then
            Return 0
        Else
            Return CInt(_string)
        End If
    End Function
    Public Shared Function GetRaceNameById(ByVal raceid As Integer) As String
        Dim RM As New ResourceManager("Namcore_Studio.UserMessages", System.Reflection.Assembly.GetExecutingAssembly())
        Select raceid
            Case 1 : Return RM.GetString("human")
            Case 2 : Return RM.GetString("orc")
            Case 3 : Return RM.GetString("dwarf")
            Case 4 : Return RM.GetString("nightelf")
            Case 5 : Return RM.GetString("undead")
            Case 6 : Return RM.GetString("tauren")
            Case 7 : Return RM.GetString("gnome")
            Case 8 : Return RM.GetString("troll")
            Case 9 : Return RM.GetString("goblin")
            Case 10 : Return RM.GetString("bloodelf")
            Case 11 : Return RM.GetString("draenei")
            Case 22 : Return RM.GetString("worgen")
            Case Else : LogAppend("Invalid RaceId: " & raceid.ToString() & " // Returning nothing!", "Conversions_GetRaceNameById") : Return Nothing
        End Select
    End Function
    Public Shared Function GetClassNameById(ByVal classid As Integer) As String
        Dim RM As New ResourceManager("Namcore_Studio.UserMessages", System.Reflection.Assembly.GetExecutingAssembly())
        Select Case classid
            Case 1 : Return RM.GetString("warrior")
            Case 2 : Return RM.GetString("paladin")
            Case 3 : Return RM.GetString("hunter")
            Case 4 : Return RM.GetString("rogue")
            Case 5 : Return RM.GetString("priest")
            Case 6 : Return RM.GetString("deathknight")
            Case 7 : Return RM.GetString("shaman")
            Case 8 : Return RM.GetString("mage")
            Case 9 : Return RM.GetString("warlock")
            Case 11 : Return RM.GetString("druid")
            Case Else : LogAppend("Invalid ClassId: " & classid.ToString() & " // Returning nothing!", "Conversions_GetClassNameById") : Return Nothing
        End Select
    End Function
    Public Shared Function GetGenderNameById(ByVal genderid As Integer) As String
        Dim RM As New ResourceManager("Namcore_Studio.UserMessages", System.Reflection.Assembly.GetExecutingAssembly())
        Select Case genderid
            Case 0 : Return RM.GetString("male")
            Case 1 : Return RM.GetString("female")
            Case Else : LogAppend("Invalid GenderId: " & genderid.ToString() & " // Returning nothing!", "Conversions_GetGenderNameById") : Return Nothing
        End Select
    End Function
End Class
