'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013 NamCore Studio <https://github.com/megasus/Namcore-Studio>
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
Imports System.Text
Imports System.Resources
Imports System.Reflection
Imports System.IO
Imports NCFramework.Framework.Logging

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

        Public Function GetRaceNameById(ByVal raceid As Integer) As String
            LogAppend("Loading race name by id: " & raceid.ToString(), "Conversions_GetRaceNameById", False)
            Dim rm As New ResourceManager("NCFramework.UserMessages", Assembly.GetExecutingAssembly())
            Select Case raceid
                Case 1 : Return rm.GetString("human")
                Case 2 : Return rm.GetString("orc")
                Case 3 : Return rm.GetString("dwarf")
                Case 4 : Return rm.GetString("nightelf")
                Case 5 : Return rm.GetString("undead")
                Case 6 : Return rm.GetString("tauren")
                Case 7 : Return rm.GetString("gnome")
                Case 8 : Return rm.GetString("troll")
                Case 9 : Return rm.GetString("goblin")
                Case 10 : Return rm.GetString("bloodelf")
                Case 11 : Return rm.GetString("draenei")
                Case 22 : Return rm.GetString("worgen")
                Case 25 : Return rm.GetString("pandaren")
                Case 26 : Return rm.GetString("pandaren")
                Case Else _
                    : LogAppend("Invalid RaceId: " & raceid.ToString() & " // Returning nothing!",
                                "Conversions_GetRaceNameById")
                    Return Nothing
            End Select
        End Function

        Public Function GetClassNameById(ByVal classid As Integer) As String
            LogAppend("Loading class name by id: " & classid.ToString(), "Conversions_GetClassNameById", False)
            Dim rm As New ResourceManager("NCFramework.UserMessages", Assembly.GetExecutingAssembly())
            Select Case classid
                Case 1 : Return rm.GetString("warrior")
                Case 2 : Return rm.GetString("paladin")
                Case 3 : Return rm.GetString("hunter")
                Case 4 : Return rm.GetString("rogue")
                Case 5 : Return rm.GetString("priest")
                Case 6 : Return rm.GetString("deathknight")
                Case 7 : Return rm.GetString("shaman")
                Case 8 : Return rm.GetString("mage")
                Case 9 : Return rm.GetString("warlock")
                Case 10 : Return rm.GetString("monk")
                Case 11 : Return rm.GetString("druid")
                Case Else _
                    : LogAppend("Invalid ClassId: " & classid.ToString() & " // Returning nothing!",
                                "Conversions_GetClassNameById")
                    Return Nothing
            End Select
        End Function

        Public Function GetRaceIdByName(ByVal racename As String) As Integer
            LogAppend("Loading race id by name: " & racename.ToString(), "Conversions_GetRaceIdByName", False)
            Select Case racename.ToLower()
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
                Case Else _
                    : LogAppend("Invalid Race name: " & racename & " // Returning nothing!", "Conversions_GetRaceIdByName")
                    Return Nothing
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
                Case Else _
                    : LogAppend("Invalid Class name: " & classname & " // Returning nothing!", "Conversions_GetClassIdByName")
                    Return Nothing
            End Select
        End Function

        Public Function GetGenderNameById(ByVal genderid As Integer) As String
            LogAppend("Loading gender name by id: " & genderid.ToString(), "Conversions_GetGenderNameById", False)
            Dim rm As New ResourceManager("NCFramework.UserMessages", Assembly.GetExecutingAssembly())
            Select Case genderid
                Case 0 : Return rm.GetString("male")
                Case 1 : Return rm.GetString("female")
                Case Else _
                    : LogAppend("Invalid GenderId: " & genderid.ToString() & " // Returning nothing!",
                                "Conversions_GetGenderNameById")
                    Return Nothing
            End Select
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
                LogAppend("Exception during converting process: " & ex.ToString(), "Conversions_ConvertImageToString", False,
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
                    LogAppend("Exception during converting process: " & ex.ToString(), "Conversions_ConvertStringToImage",
                              False, True)
                End Try
            End If
            Return img
        End Function
    End Module
End Namespace