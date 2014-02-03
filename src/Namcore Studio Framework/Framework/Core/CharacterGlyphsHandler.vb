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
'*      /Filename:      CharacterGlyphsHandler
'*      /Description:   Contains functions for extracting information about the equipped 
'*                      primary and secondary glyphs of a specific character
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.Database
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Modules
Imports libnc.Provider

Namespace Framework.Core
    Public Class CharacterGlyphsHandler
        Public Sub GetCharacterGlyphs(ByVal charguid As Integer, ByVal setId As Integer, ByVal account As Account)
            LogAppend("Loading character Glyphs for charguid: " & charguid & " and setId: " & setId,
                      "CharacterGlyphsHandler_GetCharacterGlyphs", True)
            Dim player As Character = GetCharacterSetBySetId(setId, account)
            player.PlayerGlyphs = New List(Of Glyph)()
            SetCharacterSet(setId, player, account)
            Select Case GlobalVariables.sourceCore
                Case "arcemu"
                    LoadAtArcemu(charguid, setId, account)
                Case "trinity"
                    LoadAtTrinity(charguid, setId, account)
                Case "trinitytbc"
                    'todo LoadAtTrinityTBC(charguid, setId, accountId)
                Case "mangos"
                    LoadAtMangos(charguid, setId, account)
            End Select
        End Sub

        Private Sub LoadAtArcemu(ByVal charguid As Integer, ByVal tarSetId As Integer, ByVal account As Account)
            LogAppend("Loading character Glyphs @LoadAtArcemu", "CharacterGlyphsHandler_LoadAtArcemu", False)
            Dim glyphstring As String =
                    runSQLCommand_characters_string(
                        "SELECT " & GlobalVariables.sourceStructure.char_glyphs1_col(0) & " from " &
                        GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                        GlobalVariables.sourceStructure.char_guid_col(0) &
                        "='" & charguid.ToString & "'")
            Dim secglyphstring As String =
                    runSQLCommand_characters_string(
                        "SELECT " & GlobalVariables.sourceStructure.char_glyphs2_col(0) & " from " &
                        GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                        GlobalVariables.sourceStructure.char_guid_col(0) &
                        "='" & charguid.ToString & "'")
            'Spec 0
            Dim player As Character = GetCharacterSetBySetId(tarSetId, account)
            Try
                Dim parts() As String = glyphstring.Split(","c)
                Dim prevglyphid As Integer = TryInt(parts(0))
                Dim tmpGlyph As New Glyph
                If prevglyphid > 1 Then
                    tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                    tmpGlyph.Slotname = "majorglyph1"
                    tmpGlyph.Spec = 0
                    tmpGlyph.Type = 2
                    AddCharacterGlyph(player, tmpGlyph)
                    SetCharacterSet(tarSetId, player, account)
                End If
            Catch ex As Exception
                LogAppend("Error while loading majorglyph1! -> Exception is: ###START###" & ex.ToString() & "###END###",
                          "CharacterGlyphsHandler_LoadAtArcemu", False, True)
            End Try
            Try
                Dim parts() As String = glyphstring.Split(","c)
                Dim prevglyphid As Integer = TryInt(parts(3))
                Dim tmpGlyph As New Glyph
                If prevglyphid > 1 Then
                    tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                    tmpGlyph.Slotname = "majorglyph2"
                    tmpGlyph.Spec = 0
                    tmpGlyph.Type = 2
                    AddCharacterGlyph(player, tmpGlyph)
                    SetCharacterSet(tarSetId, player, account)
                End If
            Catch ex As Exception
                LogAppend("Error while loading majorglyph2! -> Exception is: ###START###" & ex.ToString() & "###END###",
                          "CharacterGlyphsHandler_LoadAtArcemu", False, True)
            End Try
            Try
                Dim parts() As String = glyphstring.Split(","c)
                Dim prevglyphid As Integer = TryInt(parts(5))
                Dim tmpGlyph As New Glyph
                If prevglyphid > 1 Then
                    tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                    tmpGlyph.Slotname = "majorglyph3"
                    tmpGlyph.Spec = 0
                    tmpGlyph.Type = 2
                    AddCharacterGlyph(player, tmpGlyph)
                    SetCharacterSet(tarSetId, player, account)
                End If
            Catch ex As Exception
                LogAppend("Error while loading majorglyph3! -> Exception is: ###START###" & ex.ToString() & "###END###",
                          "CharacterGlyphsHandler_LoadAtArcemu", False, True)
            End Try
            Try
                Dim parts() As String = glyphstring.Split(","c)
                Dim prevglyphid As Integer = TryInt(parts(1))
                Dim tmpGlyph As New Glyph
                If prevglyphid > 1 Then
                    tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                    tmpGlyph.Slotname = "minorglyph1"
                    tmpGlyph.Spec = 0
                    tmpGlyph.Type = 1
                    AddCharacterGlyph(player, tmpGlyph)
                    SetCharacterSet(tarSetId, player, account)
                End If
            Catch ex As Exception
                LogAppend("Error while loading minorglyph1! -> Exception is: ###START###" & ex.ToString() & "###END###",
                          "CharacterGlyphsHandler_LoadAtArcemu", False, True)
            End Try
            Try
                Dim parts() As String = glyphstring.Split(","c)
                Dim prevglyphid As Integer = TryInt(parts(2))
                Dim tmpGlyph As New Glyph
                If prevglyphid > 1 Then
                    tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                    tmpGlyph.Slotname = "minorglyph2"
                    tmpGlyph.Spec = 0
                    tmpGlyph.Type = 1
                    AddCharacterGlyph(player, tmpGlyph)
                    SetCharacterSet(tarSetId, player, account)
                End If
            Catch ex As Exception
                LogAppend("Error while loading minorglyph2! -> Exception is: ###START###" & ex.ToString() & "###END###",
                          "CharacterGlyphsHandler_LoadAtArcemu", False, True)
            End Try
            Try
                Dim parts() As String = glyphstring.Split(","c)
                Dim prevglyphid As Integer = TryInt(parts(4))
                Dim tmpGlyph As New Glyph
                If prevglyphid > 1 Then
                    tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                    tmpGlyph.Slotname = "minorglyph3"
                    tmpGlyph.Spec = 0
                    tmpGlyph.Type = 1
                    AddCharacterGlyph(player, tmpGlyph)
                    SetCharacterSet(tarSetId, player, account)
                End If
            Catch ex As Exception
                LogAppend("Error while loading minorglyph3! -> Exception is: ###START###" & ex.ToString() & "###END###",
                          "CharacterGlyphsHandler_LoadAtArcemu", False, True)
            End Try
            'Spec 1
            Try
                Dim parts() As String = secglyphstring.Split(","c)
                Dim prevglyphid As Integer = TryInt(parts(0))
                Dim tmpGlyph As New Glyph
                If prevglyphid > 1 Then
                    tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                    tmpGlyph.Slotname = "secmajorglyph1"
                    tmpGlyph.Spec = 1
                    tmpGlyph.Type = 2
                    AddCharacterGlyph(player, tmpGlyph)
                    SetCharacterSet(tarSetId, player, account)
                End If
            Catch ex As Exception
                LogAppend(
                    "Error while loading secmajorglyph1! -> Exception is: ###START###" & ex.ToString() & "###END###",
                    "CharacterGlyphsHandler_LoadAtArcemu", False, True)
            End Try
            Try
                Dim parts() As String = secglyphstring.Split(","c)
                Dim prevglyphid As Integer = TryInt(parts(3))
                Dim tmpGlyph As New Glyph
                If prevglyphid > 1 Then
                    tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                    tmpGlyph.Slotname = "secmajorglyph2"
                    tmpGlyph.Spec = 1
                    tmpGlyph.Type = 2
                    AddCharacterGlyph(player, tmpGlyph)
                    SetCharacterSet(tarSetId, player, account)
                End If
            Catch ex As Exception
                LogAppend(
                    "Error while loading secmajorglyph2! -> Exception is: ###START###" & ex.ToString() & "###END###",
                    "CharacterGlyphsHandler_LoadAtArcemu", False, True)
            End Try
            Try
                Dim parts() As String = secglyphstring.Split(","c)
                Dim prevglyphid As Integer = TryInt(parts(5))
                Dim tmpGlyph As New Glyph
                If prevglyphid > 1 Then
                    tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                    tmpGlyph.Slotname = "secmajorglyph3"
                    tmpGlyph.Spec = 1
                    tmpGlyph.Type = 2
                    AddCharacterGlyph(player, tmpGlyph)
                    SetCharacterSet(tarsetId, player, account)
                End If
            Catch ex As Exception
                LogAppend(
                    "Error while loading secmajorglyph3! -> Exception is: ###START###" & ex.ToString() & "###END###",
                    "CharacterGlyphsHandler_LoadAtArcemu", False, True)
            End Try
            Try
                Dim parts() As String = secglyphstring.Split(","c)
                Dim prevglyphid As Integer = TryInt(parts(1))
                Dim tmpGlyph As New Glyph
                If prevglyphid > 1 Then
                    tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                    tmpGlyph.Slotname = "secminorglyph1"
                    tmpGlyph.Spec = 1
                    tmpGlyph.Type = 1
                    AddCharacterGlyph(player, tmpGlyph)
                    SetCharacterSet(tarsetId, player, account)
                End If
            Catch ex As Exception
                LogAppend(
                    "Error while loading secminorglyph1! -> Exception is: ###START###" & ex.ToString() & "###END###",
                    "CharacterGlyphsHandler_LoadAtArcemu", False, True)
            End Try
            Try
                Dim parts() As String = secglyphstring.Split(","c)
                Dim prevglyphid As Integer = TryInt(parts(2))
                Dim tmpGlyph As New Glyph
                If prevglyphid > 1 Then
                    tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                    tmpGlyph.Slotname = "secminorglyph2"
                    tmpGlyph.Spec = 1
                    tmpGlyph.Type = 1
                    AddCharacterGlyph(player, tmpGlyph)
                    SetCharacterSet(tarsetId, player, account)
                End If
            Catch ex As Exception
                LogAppend(
                    "Error while loading secminorglyph2! -> Exception is: ###START###" & ex.ToString() & "###END###",
                    "CharacterGlyphsHandler_LoadAtArcemu", False, True)
            End Try
            Try
                Dim parts() As String = secglyphstring.Split(","c)
                Dim prevglyphid As Integer = TryInt(parts(4))
                Dim tmpGlyph As New Glyph
                If prevglyphid > 1 Then
                    tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                    tmpGlyph.Slotname = "secminorglyph3"
                    tmpGlyph.Spec = 1
                    tmpGlyph.Type = 1
                    AddCharacterGlyph(player, tmpGlyph)
                    SetCharacterSet(tarsetId, player, account)
                End If
            Catch ex As Exception
                LogAppend(
                    "Error while loading secminorglyph3! -> Exception is: ###START###" & ex.ToString() & "###END###",
                    "CharacterGlyphsHandler_LoadAtArcemu", False, True)
            End Try
        End Sub

        Private Sub LoadAtTrinity(ByVal charguid As Integer, ByVal tarSetId As Integer, ByVal account As Account)
            LogAppend("Loading character Glyphs @LoadAtTrinity", "CharacterGlyphsHandler_LoadAtTrinity", False)
            Dim tempdt As DataTable
            Dim tempdtsec As DataTable
            If GlobalVariables.sourceExpansion = 3 Then
                tempdt =
                    ReturnDataTable(
                        "SELECT " & GlobalVariables.sourceStructure.glyphs_glyph1_col(0) & ", " &
                        GlobalVariables.sourceStructure.glyphs_glyph2_col(0) & ", " &
                        GlobalVariables.sourceStructure.glyphs_glyph3_col(0) &
                        ", " & GlobalVariables.sourceStructure.glyphs_glyph4_col(0) & ", " &
                        GlobalVariables.sourceStructure.glyphs_glyph5_col(0) & ", " &
                        GlobalVariables.sourceStructure.glyphs_glyph6_col(0) &
                        " FROM " & GlobalVariables.sourceStructure.character_glyphs_tbl(0) & " WHERE " &
                        GlobalVariables.sourceStructure.glyphs_guid_col(0) & "='" & charguid.ToString & "' AND " &
                        GlobalVariables.sourceStructure.glyphs_spec_col(0) & "='0'")
                tempdtsec =
                    ReturnDataTable(
                        "SELECT " & GlobalVariables.sourceStructure.glyphs_glyph1_col(0) & ", " &
                        GlobalVariables.sourceStructure.glyphs_glyph2_col(0) & ", " &
                        GlobalVariables.sourceStructure.glyphs_glyph3_col(0) &
                        ", " & GlobalVariables.sourceStructure.glyphs_glyph4_col(0) & ", " &
                        GlobalVariables.sourceStructure.glyphs_glyph5_col(0) & ", " &
                        GlobalVariables.sourceStructure.glyphs_glyph6_col(0) &
                        " FROM " & GlobalVariables.sourceStructure.character_glyphs_tbl(0) & " WHERE " &
                        GlobalVariables.sourceStructure.glyphs_guid_col(0) & "='" & charguid.ToString & "' AND " &
                        GlobalVariables.sourceStructure.glyphs_spec_col(0) & "='1'")
            Else
                tempdt =
                    ReturnDataTable(
                        "SELECT " & GlobalVariables.sourceStructure.glyphs_glyph1_col(0) & ", " &
                        GlobalVariables.sourceStructure.glyphs_glyph2_col(0) & ", " &
                        GlobalVariables.sourceStructure.glyphs_glyph3_col(0) &
                        ", " & GlobalVariables.sourceStructure.glyphs_glyph4_col(0) & ", " &
                        GlobalVariables.sourceStructure.glyphs_glyph5_col(0) & ", " &
                        GlobalVariables.sourceStructure.glyphs_glyph6_col(0) &
                        ", " & GlobalVariables.sourceStructure.glyphs_glyph7_col(0) & ", " &
                        GlobalVariables.sourceStructure.glyphs_glyph8_col(0) & ", " &
                        GlobalVariables.sourceStructure.glyphs_glyph9_col(0) &
                        " FROM " & GlobalVariables.sourceStructure.character_glyphs_tbl(0) & " WHERE " &
                        GlobalVariables.sourceStructure.glyphs_guid_col(0) & "='" & charguid.ToString & "' AND " &
                        GlobalVariables.sourceStructure.glyphs_spec_col(0) & "='0'")
                tempdtsec =
                    ReturnDataTable(
                        "SELECT " & GlobalVariables.sourceStructure.glyphs_glyph1_col(0) & ", " &
                        GlobalVariables.sourceStructure.glyphs_glyph2_col(0) & ", " &
                        GlobalVariables.sourceStructure.glyphs_glyph3_col(0) &
                        ", " & GlobalVariables.sourceStructure.glyphs_glyph4_col(0) & ", " &
                        GlobalVariables.sourceStructure.glyphs_glyph5_col(0) & ", " &
                        GlobalVariables.sourceStructure.glyphs_glyph6_col(0) &
                        ", " & GlobalVariables.sourceStructure.glyphs_glyph7_col(0) & ", " &
                        GlobalVariables.sourceStructure.glyphs_glyph8_col(0) & ", " &
                        GlobalVariables.sourceStructure.glyphs_glyph9_col(0) &
                        " FROM " & GlobalVariables.sourceStructure.character_glyphs_tbl(0) & " WHERE " &
                        GlobalVariables.sourceStructure.glyphs_guid_col(0) & "='" & charguid.ToString & "' AND " &
                        GlobalVariables.sourceStructure.glyphs_spec_col(0) & "='1'")
            End If
            Dim prevglyphid As Integer
            Dim player As Character = GetCharacterSetBySetId(tarSetId, account)
            Dim lastcount As Integer = tempdt.Rows.Count
            If Not lastcount = 0 Then
                prevglyphid = TryInt((tempdt.Rows(0).Item(0)).ToString)
                If prevglyphid > 1 Then
                    Dim tmpGlyph As New Glyph
                    tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                    tmpGlyph.Slotname = "majorglyph1"
                    tmpGlyph.Spec = 0
                    tmpGlyph.Type = 2
                    AddCharacterGlyph(player, tmpGlyph)
                    SetCharacterSet(tarsetId, player, account)
                End If
                prevglyphid = TryInt((tempdt.Rows(0).Item(1)).ToString)
                If prevglyphid > 1 Then
                    Dim tmpGlyph As New Glyph
                    tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                    tmpGlyph.Slotname = "minorglyph1"
                    tmpGlyph.Spec = 0
                    tmpGlyph.Type = 1
                    AddCharacterGlyph(player, tmpGlyph)
                    SetCharacterSet(tarsetId, player, account)
                End If
                prevglyphid = TryInt((tempdt.Rows(0).Item(2)).ToString)
                If prevglyphid > 1 Then
                    Dim tmpGlyph As New Glyph
                    tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                    tmpGlyph.Slotname = "minorglyph2"
                    tmpGlyph.Spec = 0
                    tmpGlyph.Type = 1
                    AddCharacterGlyph(player, tmpGlyph)
                    SetCharacterSet(tarsetId, player, account)
                End If
                prevglyphid = TryInt((tempdt.Rows(0).Item(3)).ToString)
                If prevglyphid > 1 Then
                    Dim tmpGlyph As New Glyph
                    tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                    tmpGlyph.Slotname = "majorglyph2"
                    tmpGlyph.Spec = 0
                    tmpGlyph.Type = 2
                    AddCharacterGlyph(player, tmpGlyph)
                    SetCharacterSet(tarsetId, player, account)
                End If
                prevglyphid = TryInt((tempdt.Rows(0).Item(4)).ToString)
                If prevglyphid > 1 Then
                    Dim tmpGlyph As New Glyph
                    tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                    tmpGlyph.Slotname = "minorglyph3"
                    tmpGlyph.Spec = 0
                    tmpGlyph.Type = 1
                    AddCharacterGlyph(player, tmpGlyph)
                    SetCharacterSet(tarsetId, player, account)
                End If
                prevglyphid = TryInt((tempdt.Rows(0).Item(5)).ToString)
                If prevglyphid > 1 Then
                    Dim tmpGlyph As New Glyph
                    tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                    tmpGlyph.Slotname = "majorglyph3"
                    tmpGlyph.Spec = 0
                    tmpGlyph.Type = 2
                    AddCharacterGlyph(player, tmpGlyph)
                    SetCharacterSet(tarsetId, player, account)
                End If
                If GlobalVariables.sourceExpansion = 4 Then
                    prevglyphid = TryInt((tempdt.Rows(0).Item(6)).ToString)
                    If prevglyphid > 1 Then
                        Dim tmpGlyph As New Glyph
                        tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                        tmpGlyph.Slotname = "primeglyph1"
                        tmpGlyph.Spec = 0
                        tmpGlyph.Type = 3
                        AddCharacterGlyph(player, tmpGlyph)
                        SetCharacterSet(tarsetId, player, account)
                    End If
                    prevglyphid = TryInt((tempdt.Rows(0).Item(7)).ToString)
                    If prevglyphid > 1 Then
                        Dim tmpGlyph As New Glyph
                        tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                        tmpGlyph.Slotname = "primeglyph2"
                        tmpGlyph.Spec = 0
                        tmpGlyph.Type = 3
                        AddCharacterGlyph(player, tmpGlyph)
                        SetCharacterSet(tarsetId, player, account)
                    End If
                    prevglyphid = TryInt((tempdt.Rows(0).Item(8)).ToString)
                    If prevglyphid > 1 Then
                        Dim tmpGlyph As New Glyph
                        tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                        tmpGlyph.Slotname = "primeglyph3"
                        tmpGlyph.Spec = 0
                        tmpGlyph.Type = 3
                        AddCharacterGlyph(player, tmpGlyph)
                        SetCharacterSet(tarsetId, player, account)
                    End If
                End If
            Else
                LogAppend("No Glyphs found (spec 0)!", "CharacterGlyphsHandler_LoadAtTrinity", True)
            End If
            lastcount = tempdtsec.Rows.Count
            If Not lastcount = 0 Then
                prevglyphid = TryInt((tempdtsec.Rows(0).Item(0)).ToString)
                If prevglyphid > 1 Then
                    Dim tmpGlyph As New Glyph
                    tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                    tmpGlyph.Slotname = "secmajorglyph1"
                    tmpGlyph.Spec = 1
                    tmpGlyph.Type = 2
                    AddCharacterGlyph(player, tmpGlyph)
                    SetCharacterSet(tarsetId, player, account)
                End If
                prevglyphid = TryInt((tempdtsec.Rows(0).Item(1)).ToString)
                If prevglyphid > 1 Then
                    Dim tmpGlyph As New Glyph
                    tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                    tmpGlyph.Slotname = "secminorglyph1"
                    tmpGlyph.Spec = 1
                    tmpGlyph.Type = 1
                    AddCharacterGlyph(player, tmpGlyph)
                    SetCharacterSet(tarsetId, player, account)
                End If
                prevglyphid = TryInt((tempdtsec.Rows(0).Item(2)).ToString)
                If prevglyphid > 1 Then
                    Dim tmpGlyph As New Glyph
                    tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                    tmpGlyph.Slotname = "secminorglyph2"
                    tmpGlyph.Spec = 1
                    tmpGlyph.Type = 1
                    AddCharacterGlyph(player, tmpGlyph)
                    SetCharacterSet(tarsetId, player, account)
                End If
                prevglyphid = TryInt((tempdtsec.Rows(0).Item(3)).ToString)
                If prevglyphid > 1 Then
                    Dim tmpGlyph As New Glyph
                    tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                    tmpGlyph.Slotname = "secmajorglyph2"
                    tmpGlyph.Spec = 1
                    tmpGlyph.Type = 2
                    AddCharacterGlyph(player, tmpGlyph)
                    SetCharacterSet(tarsetId, player, account)
                End If
                prevglyphid = TryInt((tempdtsec.Rows(0).Item(4)).ToString)
                If prevglyphid > 1 Then
                    Dim tmpGlyph As New Glyph
                    tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                    tmpGlyph.Slotname = "secminorglyph3"
                    tmpGlyph.Spec = 1
                    tmpGlyph.Type = 1
                    AddCharacterGlyph(player, tmpGlyph)
                    SetCharacterSet(tarsetId, player, account)
                End If
                prevglyphid = TryInt((tempdtsec.Rows(0).Item(5)).ToString)
                If prevglyphid > 1 Then
                    Dim tmpGlyph As New Glyph
                    tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                    tmpGlyph.Slotname = "secmajorglyph3"
                    tmpGlyph.Spec = 1
                    tmpGlyph.Type = 2
                    AddCharacterGlyph(player, tmpGlyph)
                    SetCharacterSet(tarsetId, player, account)
                End If
                If GlobalVariables.sourceExpansion = 4 Then
                    prevglyphid = TryInt((tempdtsec.Rows(0).Item(6)).ToString)
                    If prevglyphid > 1 Then
                        Dim tmpGlyph As New Glyph
                        tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                        tmpGlyph.Spec = 1
                        tmpGlyph.Type = 3
                        AddCharacterGlyph(player, tmpGlyph)
                        SetCharacterSet(tarsetId, player, account)
                    End If
                    prevglyphid = TryInt((tempdtsec.Rows(0).Item(7)).ToString)
                    If prevglyphid > 1 Then
                        Dim tmpGlyph As New Glyph
                        tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                        tmpGlyph.Slotname = "secprimeglyph2"
                        tmpGlyph.Spec = 1
                        tmpGlyph.Type = 3
                        AddCharacterGlyph(player, tmpGlyph)
                        SetCharacterSet(tarsetId, player, account)
                    End If
                    prevglyphid = TryInt((tempdtsec.Rows(0).Item(8)).ToString)
                    If prevglyphid > 1 Then
                        Dim tmpGlyph As New Glyph
                        tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                        tmpGlyph.Slotname = "secprimeglyph3"
                        tmpGlyph.Spec = 1
                        tmpGlyph.Type = 3
                        AddCharacterGlyph(player, tmpGlyph)
                        SetCharacterSet(tarsetId, player, account)
                    End If
                End If
            Else
                LogAppend("No Glyphs found (spec 1)!", "CharacterGlyphsHandler_LoadAtTrinity", True)
            End If
        End Sub

        Private Sub LoadAtMangos(ByVal charguid As Integer, ByVal tarSetId As Integer, ByVal account As Account)
            LogAppend("Loading character Glyphs @LoadAtMangos", "CharacterGlyphsHandler_LoadAtMangos", False)
            Dim tempdt As DataTable =
                    ReturnDataTable(
                        "SELECT " & GlobalVariables.sourceStructure.glyphs_glyph_col(0) & ", " &
                        GlobalVariables.sourceStructure.glyphs_slot_col(0) & ", " &
                        GlobalVariables.sourceStructure.glyphs_spec_col(0) &
                        " FROM " & GlobalVariables.sourceStructure.character_glyphs_tbl(0) & " WHERE " &
                        GlobalVariables.sourceStructure.glyphs_guid_col(0) & "='" & charguid.ToString & "'")
            Dim prevglyphid As Integer
            Dim slot As Integer
            Dim spec As Integer
            Dim resultquantity As Integer = tempdt.Rows.Count
            Dim proccounter As Integer = 0
            Dim player As Character = GetCharacterSetBySetId(tarSetId, account)
            Do
                Try
                    prevglyphid = TryInt((tempdt.Rows(proccounter).Item(0)).ToString)
                    If prevglyphid > 1 Then
                        slot = TryInt((tempdt.Rows(proccounter).Item(1)).ToString)
                        spec = TryInt((tempdt.Rows(proccounter).Item(2)).ToString)
                        Select Case spec
                            Case 0
                                Select Case slot
                                    Case 0
                                        Dim tmpGlyph As New Glyph
                                        tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                                        tmpGlyph.slotname = "majorglyph1"
                                        tmpGlyph.spec = 0
                                        tmpGlyph.Type = 2
                                        AddCharacterGlyph(player, tmpGlyph)
                                        SetCharacterSet(tarSetId, player, account)
                                    Case 1
                                        Dim tmpGlyph As New Glyph
                                        tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                                        tmpGlyph.Slotname = "minorglyph2"
                                        tmpGlyph.Spec = 0
                                        tmpGlyph.Type = 1
                                        AddCharacterGlyph(player, tmpGlyph)
                                        SetCharacterSet(tarSetId, player, account)
                                    Case 2
                                        Dim tmpGlyph As New Glyph
                                        tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                                        tmpGlyph.Slotname = "minorglyph3"
                                        tmpGlyph.Spec = 0
                                        tmpGlyph.Type = 1
                                        AddCharacterGlyph(player, tmpGlyph)
                                        SetCharacterSet(tarSetId, player, account)
                                    Case 3
                                        Dim tmpGlyph As New Glyph
                                        tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                                        tmpGlyph.Slotname = "majorglyph2"
                                        tmpGlyph.Spec = 0
                                        tmpGlyph.Type = 2
                                        AddCharacterGlyph(player, tmpGlyph)
                                        SetCharacterSet(tarSetId, player, account)
                                    Case 4
                                        Dim tmpGlyph As New Glyph
                                        tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                                        tmpGlyph.Slotname = "minorglyph1"
                                        tmpGlyph.Spec = 0
                                        tmpGlyph.Type = 1
                                        AddCharacterGlyph(player, tmpGlyph)
                                        SetCharacterSet(tarSetId, player, account)
                                    Case 5
                                        Dim tmpGlyph As New Glyph
                                        tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                                        tmpGlyph.Slotname = "majorglyph3"
                                        tmpGlyph.Spec = 0
                                        tmpGlyph.Type = 2
                                        AddCharacterGlyph(player, tmpGlyph)
                                        SetCharacterSet(tarSetId, player, account)
                                    Case 6
                                        Dim tmpGlyph As New Glyph
                                        tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                                        tmpGlyph.Slotname = "primeglyph1"
                                        tmpGlyph.Spec = 0
                                        tmpGlyph.Type = 3
                                        AddCharacterGlyph(player, tmpGlyph)
                                        SetCharacterSet(tarSetId, player, account)
                                    Case 7
                                        Dim tmpGlyph As New Glyph
                                        tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                                        tmpGlyph.Slotname = "primeglyph2"
                                        tmpGlyph.Spec = 0
                                        tmpGlyph.Type = 3
                                        AddCharacterGlyph(player, tmpGlyph)
                                        SetCharacterSet(tarSetId, player, account)
                                    Case 8
                                        Dim tmpGlyph As New Glyph
                                        tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                                        tmpGlyph.Slotname = "primeglyph3"
                                        tmpGlyph.Spec = 0
                                        tmpGlyph.Type = 3
                                        AddCharacterGlyph(player, tmpGlyph)
                                        SetCharacterSet(tarSetId, player, account)
                                End Select
                            Case 1
                                Select Case slot
                                    Case 0
                                        Dim tmpGlyph As New Glyph
                                        tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                                        tmpGlyph.Slotname = "secmajorglyph1"
                                        tmpGlyph.Spec = 1
                                        tmpGlyph.Type = 2
                                        AddCharacterGlyph(player, tmpGlyph)
                                        SetCharacterSet(tarSetId, player, account)
                                    Case 1
                                        Dim tmpGlyph As New Glyph
                                        tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                                        tmpGlyph.Slotname = "secminorglyph2"
                                        tmpGlyph.Spec = 1
                                        tmpGlyph.Type = 1
                                        AddCharacterGlyph(player, tmpGlyph)
                                        SetCharacterSet(tarSetId, player, account)
                                    Case 2
                                        Dim tmpGlyph As New Glyph
                                        tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                                        tmpGlyph.Slotname = "secminorglyph3"
                                        tmpGlyph.Spec = 1
                                        tmpGlyph.Type = 1
                                        AddCharacterGlyph(player, tmpGlyph)
                                        SetCharacterSet(tarSetId, player, account)
                                    Case 3
                                        Dim tmpGlyph As New Glyph
                                        tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                                        tmpGlyph.Slotname = "secmajorglyph2"
                                        tmpGlyph.Spec = 1
                                        tmpGlyph.Type = 2
                                        AddCharacterGlyph(player, tmpGlyph)
                                        SetCharacterSet(tarSetId, player, account)
                                    Case 4
                                        Dim tmpGlyph As New Glyph
                                        tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                                        tmpGlyph.Slotname = "secminorglyph1"
                                        tmpGlyph.Spec = 1
                                        tmpGlyph.Type = 1
                                        AddCharacterGlyph(player, tmpGlyph)
                                        SetCharacterSet(tarSetId, player, account)
                                    Case 5
                                        Dim tmpGlyph As New Glyph
                                        tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                                        tmpGlyph.Slotname = "secmajorglyph3"
                                        tmpGlyph.Spec = 1
                                        tmpGlyph.Type = 2
                                        AddCharacterGlyph(player, tmpGlyph)
                                        SetCharacterSet(tarSetId, player, account)
                                    Case 6
                                        Dim tmpGlyph As New Glyph
                                        tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                                        tmpGlyph.Slotname = "secprimeglyph1"
                                        tmpGlyph.Spec = 1
                                        tmpGlyph.Type = 3
                                        AddCharacterGlyph(player, tmpGlyph)
                                        SetCharacterSet(tarSetId, player, account)
                                    Case 7
                                        Dim tmpGlyph As New Glyph
                                        tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                                        tmpGlyph.Slotname = "secprimeglyph2"
                                        tmpGlyph.Spec = 1
                                        tmpGlyph.Type = 3
                                        AddCharacterGlyph(player, tmpGlyph)
                                        SetCharacterSet(tarSetId, player, account)
                                    Case 8
                                        Dim tmpGlyph As New Glyph
                                        tmpGlyph.Id = GetItemIdByGlyphId(prevglyphid, GlobalVariables.sourceExpansion)
                                        tmpGlyph.Slotname = "secprimeglyph3"
                                        tmpGlyph.Spec = 1
                                        tmpGlyph.Type = 3
                                        AddCharacterGlyph(player, tmpGlyph)
                                        SetCharacterSet(tarsetId, player, account)
                                End Select
                        End Select
                    End If
                Catch ex As Exception
                    LogAppend(
                        "Something went wrong while loading character Glyphs -> Exception is: ###START###" &
                        ex.ToString() &
                        "###END###", "CharacterGlyphsHandler_LoadAtMangos", True, True)
                End Try
                proccounter += 1
            Loop Until proccounter = resultquantity
        End Sub
    End Class
End Namespace