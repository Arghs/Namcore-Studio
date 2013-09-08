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
Imports System.IO
Imports System.Resources
Imports System.Net
Imports System.drawing


Public Module Conversions

    Public Function ConvertListToString(ByVal _list As List(Of String)) As String
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
    Public Function ConvertStringToList(ByVal _string As String) As List(Of String)
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
    Public Function TryInt(ByVal _string As String) As Integer
        LogAppend("Converting string to integer: " & _string, "Conversions_TryInt", False)
        Try
            Dim parseResult As Integer = CInt(Integer.TryParse(_string, Nothing))
            If parseResult = 0 Then
                Return 0
            Else
                Return CInt(_string)
            End If
        Catch ex As Exception
            LogAppend("Exception during TryInt() : " & ex.ToString(), "Conversions_TryInt", False, True)
            Return 0
        End Try

    End Function
    Public Function GetRaceNameById(ByVal raceid As Integer) As String
        LogAppend("Loading race name by id: " & raceid.ToString(), "Conversions_GetRaceNameById", False)
        Dim RM As New ResourceManager("NCFramework.UserMessages", System.Reflection.Assembly.GetExecutingAssembly())
        Select Case raceid
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
            Case 25 : Return RM.GetString("pandaren")
            Case 26 : Return RM.GetString("pandaren")
            Case Else : LogAppend("Invalid RaceId: " & raceid.ToString() & " // Returning nothing!", "Conversions_GetRaceNameById") : Return Nothing
        End Select
    End Function
    Public Function GetClassNameById(ByVal classid As Integer) As String
        LogAppend("Loading class name by id: " & classid.ToString(), "Conversions_GetClassNameById", False)
        Dim RM As New ResourceManager("NCFramework.UserMessages", System.Reflection.Assembly.GetExecutingAssembly())
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
            Case 10 : Return RM.GetString("monk")
            Case 11 : Return RM.GetString("druid")
            Case Else : LogAppend("Invalid ClassId: " & classid.ToString() & " // Returning nothing!", "Conversions_GetClassNameById") : Return Nothing
        End Select
    End Function
    Public Function GetRaceIdByName(ByVal racename As String) As Integer
        LogAppend("Loading race id by name: " & racename.ToString(), "Conversions_GetRaceIdByName", False)
        Select Case racename
            Case "human" : Return 1
            Case "orc" : Return 2
            Case "dwarf" : Return 3
            Case "night-elf" : Return 4
            Case "undead" : Return 5
            Case "tauren" : Return 6
            Case "gnome" : Return 7
            Case "troll" : Return 8
            Case "goblin" : Return 9
            Case "blood-elf" : Return 10
            Case "draenei" : Return 11
            Case "worgen" : Return 22
            Case "pandaren" : Return 25
            Case Else : LogAppend("Invalid Race name: " & racename & " // Returning nothing!", "Conversions_GetRaceIdByName") : Return Nothing
        End Select
    End Function
    Public Function GetClassIdByName(ByVal classname As String) As Integer
        LogAppend("Loading class id by name: " & classname.ToString(), "Conversions_GetClassIdByName", False)
        Select Case classname
            Case "warrior" : Return 1
            Case "paladin" : Return 2
            Case "hunter" : Return 3
            Case "rogue" : Return 4
            Case "priest" : Return 5
            Case "death-knight" : Return 6
            Case "shaman" : Return 7
            Case "mage" : Return 8
            Case "warlock" : Return 9
            Case "monk" : Return 10
            Case "druid" : Return 11
            Case Else : LogAppend("Invalid Class name: " & classname & " // Returning nothing!", "Conversions_GetClassIdByName") : Return Nothing
        End Select
    End Function
    Public Function GetGenderNameById(ByVal genderid As Integer) As String
        LogAppend("Loading gender name by id: " & genderid.ToString(), "Conversions_GetGenderNameById", False)
        Dim RM As New ResourceManager("NCFramework.UserMessages", System.Reflection.Assembly.GetExecutingAssembly())
        Select Case genderid
            Case 0 : Return RM.GetString("male")
            Case 1 : Return RM.GetString("female")
            Case Else : LogAppend("Invalid GenderId: " & genderid.ToString() & " // Returning nothing!", "Conversions_GetGenderNameById") : Return Nothing
        End Select
    End Function
    Public Function ConvertImageToString(ByVal _img As Image) As String
        LogAppend("Converting image to string", "Conversions_ConvertImageToString", False)
        If _img Is Nothing Then Return ""
        Dim Result As String = String.Empty
        Try
            Dim img As Image = _img
            Using ms As MemoryStream = New MemoryStream
                img.Save(ms, img.RawFormat)
                Dim Bytes() As Byte = ms.ToArray()
                Result = Convert.ToBase64String(Bytes)
            End Using
        Catch ex As Exception
            LogAppend("Exception during converting process: " & ex.ToString(), "Conversions_ConvertImageToString", False, True)
        End Try
        Return Result
    End Function
    Public Function ConvertStringToImage(ByVal Base64String As String) As Image
        LogAppend("Converting string to image", "Conversions_ConvertStringToImage", False)
        If Base64String = "" Then Return Nothing
        Dim img As Image = Nothing
        If Base64String Is Nothing Then
            LogAppend("Base64String is nothing!", "Conversions_ConvertStringToImage", False, True)
        Else
            Try
                Dim Bytes() As Byte = Convert.FromBase64String(Base64String)
                img = Image.FromStream(New MemoryStream(Bytes))
            Catch ex As Exception
                LogAppend("Exception during converting process: " & ex.ToString(), "Conversions_ConvertStringToImage", False, True)
            End Try
        End If
        Return img
    End Function
   
    Public Function getNameOfQuest(ByVal questid As Integer) As String
        LogAppend("Loading quest name of id " & questid.ToString(), "Conversions_getNameOfQuest")
        Try
            Dim client As New WebClient
            client.CheckProxy()
            Dim qstcontext As String = client.DownloadString("http://www.wowhead.com/quest=" & questid.ToString)
            Return splitString(qstcontext, "<title>", " - Quest - World")
        Catch ex As Exception
            LogAppend("Failed to load name of quest // Returning error", "Conversions_getNameOfQuest")
            Return "error"
        End Try
    End Function
End Module
