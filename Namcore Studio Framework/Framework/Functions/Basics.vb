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

Imports Namcore_Studio_Framework.EventLogging
Imports Namcore_Studio_Framework.GlobalVariables
Imports Namcore_Studio_Framework.GlobalCharVars
Imports System.Net
Imports Namcore_Studio_Framework.Conversions
Imports System.Drawing
Public Module Basics
    Public tmpset As Integer
    Public Function GetCharacterSetBySetId(ByVal setId As Integer) As Character
        If tmpset = setId Then
            Return TempCharacter
        End If
        If globChars.CharacterSetsIndex.Contains("setId:" & setId.ToString() & "|") Then
            'found
            tmpset = setId
            TempCharacter = globChars.CharacterSets(TryInt(splitString(globChars.CharacterSetsIndex, "[setId:" & setId.ToString() & "|@", "]")))
            Return TempCharacter
        Else
            'not found
            Return Nothing
        End If
    End Function
    Public Sub AddCharacterSet(ByVal setId As Integer, ByVal player As Character)
        globChars.CharacterSets.Add(player)
        globChars.CharacterSetsIndex = globChars.CharacterSetsIndex & "[setId:" & setId.ToString & "|@" & (globChars.CharacterSets.Count - 1).ToString & "]"
    End Sub
    Public Sub SetCharacterSet(ByVal setId As Integer, ByVal TCharacter As Character)
        If globChars.CharacterSetsIndex.Contains("setId:" & setId.ToString() & "|") Then
            'found
            globChars.CharacterSets(TryInt(splitString(globChars.CharacterSetsIndex, "[setId:" & setId.ToString() & "|@", "]"))) = TCharacter
        Else
            'not found
        End If
    End Sub
    Public Sub AddCharacterArmorItem(ByRef player As Character, ByVal itm As Item)
        If player.ArmorItems Is Nothing Then player.ArmorItems = New List(Of Item)
        player.ArmorItems.Add(itm)
        player.ArmorItemsIndex = player.ArmorItemsIndex & "[slot:" & itm.slotname & "|@" & (player.ArmorItems.Count - 1).ToString & "]"
        player.ArmorItemsIndex = player.ArmorItemsIndex & "[slotnum:" & itm.slot.ToString & "|@" & (player.ArmorItems.Count - 1).ToString & "]"
    End Sub
    Public Sub SetCharacterArmorItem(ByRef player As Character, ByVal itm As Item)
        If player.ArmorItemsIndex.Contains("[slot:" & itm.slotname & "|@") Or player.ArmorItemsIndex.Contains("[slotnum:" & itm.slot.ToString & "|@") Then
            player.ArmorItems(TryInt(splitString(player.ArmorItemsIndex, "[slot:" & itm.slotname & "|@", "]"))) = itm
            player.ArmorItems(TryInt(splitString(player.ArmorItemsIndex, "[slotnum:" & itm.slot.ToString & "|@", "]"))) = itm
        Else

        End If
    End Sub
    Public Function GetCharacterArmorItem(ByVal player As Character, ByVal slot As String, Optional isint As Boolean = False) As Item
        If player.ArmorItemsIndex.Contains("[slot:" & slot & "|@") Or player.ArmorItemsIndex.Contains("[slotnum:" & slot & "|@") Then
            If isint = True Then
                Return player.ArmorItems(TryInt(splitString(player.ArmorItemsIndex, "[slotnum:" & slot & "|@", "]")))
            Else
                Return player.ArmorItems(TryInt(splitString(player.ArmorItemsIndex, "[slot:" & slot & "|@", "]")))
            End If

        Else
            Return Nothing
        End If
    End Function
    Public Sub AddCharacterGlyph(ByRef player As Character, ByVal gly As Glyph)
        If player.PlayerGlyphs Is Nothing Then player.PlayerGlyphs = New List(Of Glyph)
        player.PlayerGlyphs.Add(gly)
        player.PlayerGlyphsIndex = player.PlayerGlyphsIndex & "[slot:" & gly.slotname & "|@" & (player.PlayerGlyphs.Count - 1).ToString & "]"
    End Sub
    Public Sub SetCharacterGlyph(ByRef player As Character, ByVal glph As Glyph)
        If player.PlayerGlyphsIndex.Contains("[slot:" & glph.slotname & "|@") Then
            player.PlayerGlyphs(TryInt(splitString(player.PlayerGlyphsIndex, "[slot:" & glph.slotname & "|@", "]"))) = glph
        Else

        End If
    End Sub
    Public Function GetCharacterGlyph(ByVal player As Character, ByVal slot As String) As Glyph
        If player.PlayerGlyphsIndex Is Nothing Then Return Nothing
        If player.PlayerGlyphsIndex.Contains("[slot:" & slot & "|@") Then
            Return player.PlayerGlyphs(TryInt(splitString(player.PlayerGlyphsIndex, "[slot:" & slot & "|@", "]")))
        Else
            Return Nothing
        End If
    End Function
   

    Public Sub AbortProcess()

    End Sub
    Public Function splitString(ByVal source As String, ByVal start As String, ByVal ending As String) As String
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
    Public Function splitList(ByVal source As String, ByVal category As String) As String
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
    Public Function LoadImageFromUrl(ByRef url As String) As Image
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
    Private Function NotNull(ByVal obj As Object) As String
        If obj Is Nothing Then
            Return ""
        Else
            Return obj.ToString()
        End If
    End Function
End Module
