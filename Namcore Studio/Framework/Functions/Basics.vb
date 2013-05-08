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
Imports Namcore_Studio.GlobalVariables
Imports System.Net
Imports Namcore_Studio.Conversions
Public Class Basics
    Public Shared Sub SetTemporaryCharacterInformation(ByVal field As String, ByVal value As String, ByVal targetSetId As Integer)
        If temporaryCharacterInformation Is Nothing Then temporaryCharacterInformation = New List(Of String)
        With temporaryCharacterInformation
            Select Case .Count
                Case 0 : .Add("") : .Add("")
                Case Is <= targetSetId : .Add("")
            End Select
            If Not .Item(targetSetId).Contains("[[#INFORMATIONSET" & targetSetId.ToString & "]]") Then
                .Item(targetSetId) = .Item(targetSetId) & "[[#INFORMATIONSET" & targetSetId.ToString() & "]]" & vbNewLine & "[[END#INFORMATIONSET" & targetSetId.ToString() & "]]"
            End If
            .Item(targetSetId) = .Item(targetSetId).Replace("[[END#INFORMATIONSET" & targetSetId.ToString() & "]]", "[" & field & "]" & value & "[/" & field & "]" & vbNewLine &
                                                                                  "[[END#INFORMATIONSET" & targetSetId.ToString() & "]]")
        End With
    End Sub
    Public Shared Sub AppendTemporaryCharacterInformation(ByVal field As String, ByVal value As String, ByVal targetSetId As Integer)
        With temporaryCharacterInformation
            Dim CharacterContext As String = splitString(.Item(targetSetId), "[[#INFORMATIONSET" & targetSetId.ToString() & "]]", "[[END#INFORMATIONSET" & targetSetId.ToString() & "]]")
            If Not CharacterContext.Contains("[" & field & "]") Then
                .Item(targetSetId) = .Item(targetSetId).Replace("[[END#INFORMATIONSET" & targetSetId.ToString() & "]]", "[" & field & "]" & value & "#VAL#[/" & field & "]" & vbNewLine &
                                                                              "[[END#INFORMATIONSET" & targetSetId.ToString() & "]]")
            End If
            Dim newCharContext As String = CharacterContext.Replace("#VAL#[/" & field & "]", value & "#VAL#[/" & field & "]")
            .Item(targetSetId) = .Item(targetSetId).Replace(CharacterContext, newCharContext)
        End With
    End Sub
    Public Shared Sub SetTCI_Item(ByVal itm As Item, ByVal targetSetId As Integer)
        Dim itemContext As String = "[itm:" & itm.slotname & "][itm:" & itm.slot.ToString & "]" &
            "{slot}" & itm.slotname & "{/slot}" &
            "{id}" & NotNull(itm.id) & "{/id}" &
            "{name}" & NotNull(itm.name) & "{/name}" &
            "{rarity}" & NotNull(itm.rarity) & "{/rarity}" &
            "{socket1ID}" & NotNull(itm.socket1_id) & "{/socket1ID}" &
            "{socket2ID}" & NotNull(itm.socket2_id) & "{/socket2ID}" &
            "{socket3ID}" & NotNull(itm.socket3_id) & "{/socket3ID}" &
            "{socket1NAME}" & NotNull(itm.socket1_name) & "{/socket1NAME}" &
            "{socket2NAME}" & NotNull(itm.socket2_name) & "{/socket2NAME}" &
            "{socket3NAME}" & NotNull(itm.socket3_name) & "{/socket3NAME}" &
            "{socket1COLOR}" & ConvertImageToString(itm.socket1_pic) & "{/socket1COLOR}" &
            "{socket2COLOR}" & ConvertImageToString(itm.socket2_pic) & "{/socket2COLOR}" &
            "{socket3COLOR}" & ConvertImageToString(itm.socket3_pic) & "{/socket3COLOR}" &
            "{enchantmentID}" & NotNull(itm.enchantment_id) & "{/enchantmentID}" &
            "{enchantmentNAME}" & NotNull(itm.enchantment_name) & "{/enchantmentNAME}" &
            "{enchantmentTYPE}" & NotNull(itm.enchantment_type) & "{/enchantmentTYPE}" &
            "{image}" & ConvertImageToString(itm.image) & "{/image}" &
            "[/itm:" & itm.slotname & "]"
        temporaryCharacterInformation.Item(targetSetId) = temporaryCharacterInformation.Item(targetSetId).Replace("[[END#INFORMATIONSET" & targetSetId.ToString() & "]]", itemContext & vbNewLine &
                                                                      "[[END#INFORMATIONSET" & targetSetId.ToString() & "]]")

    End Sub
    Public Shared Sub SetTCI_Glyph(ByVal glyph As Glyph, ByVal targetSetId As Integer)
        Dim GlyphContext As String = "[glyph:" & glyph.slotname & "]" &
            "{slot}" & glyph.slotname & "{/slot}" &
            "{id}" & NotNull(glyph.id) & "{/id}" &
            "{name}" & NotNull(glyph.name) & "{/name}" &
            "{type}" & NotNull(glyph.type) & "{/type}" &
            "{spec}" & NotNull(glyph.spec) & "{/spec}" &
            "{image}" & ConvertImageToString(glyph.image) & "{/image}" &
            "[/glyph:" & glyph.slotname & "]"
        temporaryCharacterInformation.Item(targetSetId) = temporaryCharacterInformation.Item(targetSetId).Replace("[[END#INFORMATIONSET" & targetSetId.ToString() & "]]", GlyphContext & vbNewLine &
                                                                      "[[END#INFORMATIONSET" & targetSetId.ToString() & "]]")
    End Sub
    Public Shared Function GetTCI_Item(ByVal slot As String, ByVal targetSetId As Integer) As Item
        Dim itemContext As String = GetTemporaryCharacterInformation(("itm:" & slot), targetSetId)
        Dim itm As New Item
        If itemContext Is Nothing Then Return itm
        itm.slotname = splitString(itemContext, "{slot}", "{/slot}")
        itm.id = TryInt(splitString(itemContext, "{id}", "{/id}"))
        itm.name = splitString(itemContext, "{name}", "{/name}")
        itm.rarity = TryInt(splitString(itemContext, "{rarity}", "{/rarity}"))
        itm.socket1_id = TryInt(splitString(itemContext, "{socket1ID}", "{/socket1ID}"))
        itm.socket2_id = TryInt(splitString(itemContext, "{socket2ID}", "{/socket2ID}"))
        itm.socket3_id = TryInt(splitString(itemContext, "{socket3ID}", "{/socket3ID}"))
        itm.socket1_name = splitString(itemContext, "{socket1NAME}", "{/socket1NAME}")
        itm.socket2_name = splitString(itemContext, "{socket2NAME}", "{/socket2NAME}")
        itm.socket3_name = splitString(itemContext, "{socket3NAME}", "{/socket3NAME}")
        itm.socket1_pic = ConvertStringToImage(splitString(itemContext, "{socket1COLOR}", "{/socket1COLOR}"))
        itm.socket2_pic = ConvertStringToImage(splitString(itemContext, "{socket2COLOR}", "{/socket2COLOR}"))
        itm.socket3_pic = ConvertStringToImage(splitString(itemContext, "{socket3COLOR}", "{/socket3COLOR}"))
        itm.enchantment_id = TryInt(splitString(itemContext, "{enchantmentID}", "{/enchantmentID}"))
        itm.enchantment_name = splitString(itemContext, "{enchantmentNAME}", "{/enchantmentNAME}")
        itm.enchantment_type = TryInt(splitString(itemContext, "{enchantmentTYPE}", "{/enchantmentTYPE}"))
        itm.image = ConvertStringToImage(splitString(itemContext, "{image}", "{/image}"))
        Return itm
    End Function
    Public Shared Function GetTCI_Glyph(ByVal glyph As Glyph, ByVal targetSetId As Integer)
        Dim GlyphContext As String = "[glyph:" & glyph.slotname & "]" &
            "{slot}" & glyph.slotname & "{/slot}" &
            "{id}" & NotNull(glyph.id) & "{/id}" &
            "{name}" & NotNull(glyph.name) & "{/name}" &
            "{type}" & NotNull(glyph.type) & "{/type}" &
            "{spec}" & NotNull(glyph.spec) & "{/spec}" &
            "{image}" & ConvertImageToString(glyph.image) & "{/image}" &
            "[/glyph:" & glyph.slotname & "]"
        temporaryCharacterInformation.Item(targetSetId) = temporaryCharacterInformation.Item(targetSetId).Replace("[[END#INFORMATIONSET" & targetSetId.ToString() & "]]", GlyphContext & vbNewLine &
                                                                      "[[END#INFORMATIONSET" & targetSetId.ToString() & "]]")
    End Function
    Public Shared Function GetTemporaryCharacterInformation(ByVal field As String, ByVal targetSetId As Integer) As String
        Dim CharContext As String = temporaryCharacterInformation.Item(targetSetId)
        If CharContext.Contains("[" & field & "]") Then
            Dim returnstring As String = splitString(CharContext, "[" & field & "]", "[/" & field & "]")
            Return returnstring
        Else
            Return Nothing
        End If
    End Function
   
    Public Shared Sub AbortProcess()

    End Sub
    Public Shared Function splitString(ByVal source As String, ByVal start As String, ByVal ending As String) As String
        If source Is Nothing Or start Is Nothing Or ending Is Nothing Then
            LogAppend("Failed to split a string: source might be nothing", "Basics_splitString", False, True)
            Return Nothing
        End If
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
    Private Shared Function NotNull(ByVal obj As Object) As String
        If obj Is Nothing Then
            Return ""
        Else
            Return obj.ToString()
        End If
    End Function
End Class
