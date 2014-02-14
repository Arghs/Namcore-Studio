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
'*      /Filename:      GlyphCreation
'*      /Description:   Includes functions for creating the glyphs of a specific character
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.Database
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Modules
Imports libnc.Provider

Namespace Framework.Transmission
    Public Class GlyphCreation
        Public Sub SetCharacterGlyphs(ByVal player As Character, Optional charguid As Integer = 0)
            If charguid = 0 Then charguid = player.Guid
            LogAppend("Creating Glyphs for character: " & charguid.ToString(),
                      "GlyphCreation_SetCharacterGlyphs", True)
            Try
                Select Case GlobalVariables.targetCore
                    Case "arcemu"
                        CreateAtArcemu(charguid, player)
                    Case "trinity"
                        CreateAtTrinity(charguid, player)
                    Case "trinitytbc"

                    Case "mangos"
                        CreateAtMangos(charguid, player)
                End Select
            Catch ex As Exception
                LogAppend("Exception occured: " & ex.ToString(),
                      "GlyphCreation_SetCharacterGlyphs", False, True)
            End Try
        End Sub

        Private Sub CreateAtArcemu(ByVal characterguid As Integer, ByVal player As Character)
            LogAppend("Creating at arcemu", "GlyphCreation_createAtArcemu", False)
            runSQLCommand_characters_string(
                "DELETE " & GlobalVariables.targetStructure.char_glyphs1_col(0) & " FROM " &
                GlobalVariables.targetStructure.character_tbl(0) & " WHERE " &
                GlobalVariables.targetStructure.char_guid_col(0) & " = '" & characterguid.ToString() & "'", True)
            runSQLCommand_characters_string(
                "DELETE " & GlobalVariables.targetStructure.char_glyphs2_col(0) & " FROM " &
                GlobalVariables.targetStructure.character_tbl(0) & " WHERE " &
                GlobalVariables.targetStructure.char_guid_col(0) & " = '" & characterguid.ToString() & "'", True)
            Dim glyphstring1 As String = "major1,minor1,minor2,major2,minor3,major3,"
            Dim glyphstring2 As String = glyphstring1
            glyphstring1 = glyphstring1.Replace("minor1",
                                                (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "minorglyph1")).Id,
                                                                    GlobalVariables.targetExpansion).ToString))
            glyphstring1 = glyphstring1.Replace("minor2",
                                                (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "minorglyph2")).Id,
                                                                    GlobalVariables.targetExpansion).ToString))
            glyphstring1 = glyphstring1.Replace("minor3",
                                                (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "minorglyph3")).Id,
                                                                    GlobalVariables.targetExpansion).ToString))
            glyphstring1 = glyphstring1.Replace("major1",
                                                (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "majorglyph1")).Id,
                                                                    GlobalVariables.targetExpansion).ToString))
            glyphstring1 = glyphstring1.Replace("major2",
                                                (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "majorglyph2")).Id,
                                                                    GlobalVariables.targetExpansion).ToString))
            glyphstring1 = glyphstring1.Replace("major3",
                                                (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "majorglyph3")).Id,
                                                                    GlobalVariables.targetExpansion).ToString))
            glyphstring2 = glyphstring2.Replace("minor1",
                                                (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "secminorglyph1")).Id,
                                                                    GlobalVariables.targetExpansion).ToString))
            glyphstring2 = glyphstring2.Replace("minor2",
                                                (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "secminorglyph2")).Id,
                                                                    GlobalVariables.targetExpansion).ToString))
            glyphstring2 = glyphstring2.Replace("minor3",
                                                (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "secminorglyph3")).Id,
                                                                    GlobalVariables.targetExpansion).ToString))
            glyphstring2 = glyphstring2.Replace("major1",
                                                (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "secmajorglyph1")).Id,
                                                                    GlobalVariables.targetExpansion).ToString))
            glyphstring2 = glyphstring2.Replace("major2",
                                                (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "secmajorglyph2")).Id,
                                                                    GlobalVariables.targetExpansion).ToString))
            glyphstring2 = glyphstring2.Replace("major3",
                                                (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "secmajorglyph3")).Id,
                                                                    GlobalVariables.targetExpansion).ToString))
            runSQLCommand_characters_string(
                "UPDATE " & GlobalVariables.targetStructure.character_tbl(0) & " SET " &
                GlobalVariables.targetStructure.char_glyphs1_col(0) & "='" & glyphstring1 & "' WHERE " &
                GlobalVariables.targetStructure.char_guid_col(0) & "='" & characterguid.ToString() & "'", True)
            runSQLCommand_characters_string(
                "UPDATE " & GlobalVariables.targetStructure.character_tbl(0) & " SET " &
                GlobalVariables.targetStructure.char_glyphs2_col(0) & "='" & glyphstring2 & "' WHERE " &
                GlobalVariables.targetStructure.char_guid_col(0) & "='" & characterguid.ToString() & "'", True)
        End Sub

        Private Sub CreateAtTrinity(ByVal characterguid As Integer, ByVal player As Character)
            LogAppend("Creating at Trinity", "GlyphCreation_createAtTrinity", False)
            runSQLCommand_characters_string(
                "DELETE FROM " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " WHERE " &
                GlobalVariables.targetStructure.glyphs_guid_col(0) & " = '" & characterguid.ToString() & "' AND " &
                GlobalVariables.targetStructure.glyphs_spec_col(0) & "='0'", True)
            runSQLCommand_characters_string(
                "DELETE FROM " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " WHERE " &
                GlobalVariables.targetStructure.glyphs_guid_col(0) & " = '" & characterguid.ToString() & "' AND " &
                GlobalVariables.targetStructure.glyphs_spec_col(0) & "='1'", True)
            If GlobalVariables.targetExpansion = 4 Then
                runSQLCommand_characters_string(
                    "INSERT INTO " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " ( " &
                    GlobalVariables.targetStructure.glyphs_guid_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_spec_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph1_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph2_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph3_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph4_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph5_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph6_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph7_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph8_col(0) &
                    ", " & GlobalVariables.targetStructure.glyphs_glyph9_col(0) & " ) VALUES ( '" &
                    characterguid.ToString() &
                    "', '0', '" &
                    (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "majorglyph1")).Id, GlobalVariables.targetExpansion)).
                        ToString & "', " &
                    "'" &
                    (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "minorglyph2")).Id, GlobalVariables.targetExpansion)).
                        ToString & "', " &
                    "'" &
                    (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "minorglyph3")).Id, GlobalVariables.targetExpansion)).
                        ToString & "', " &
                    "'" &
                    (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "majorglyph2")).Id, GlobalVariables.targetExpansion)).
                        ToString & "', " &
                    "'" &
                    (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "minorglyph1")).Id, GlobalVariables.targetExpansion)).
                        ToString & "', " &
                    "'" &
                    (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "majorglyph3")).Id, GlobalVariables.targetExpansion)).
                        ToString & "', " &
                    "'" &
                    (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "primeglyph1")).Id, GlobalVariables.targetExpansion)).
                        ToString & "', " &
                    "'" &
                    (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "primeglyph2")).Id, GlobalVariables.targetExpansion)).
                        ToString & "', " &
                    "'" &
                    (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "primeglyph3")).Id, GlobalVariables.targetExpansion)).
                        ToString & "' )", True)
                runSQLCommand_characters_string(
                    "INSERT INTO " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " ( " &
                    GlobalVariables.targetStructure.glyphs_guid_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_spec_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph1_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph2_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph3_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph4_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph5_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph6_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph7_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph8_col(0) &
                    ", " & GlobalVariables.targetStructure.glyphs_glyph9_col(0) & " ) VALUES ( '" &
                    characterguid.ToString() &
                    "', '1', '" &
                    (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "secmajorglyph1")).Id, GlobalVariables.targetExpansion)) _
                        .ToString & "', " &
                    "'" &
                    (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "secminorglyph2")).Id, GlobalVariables.targetExpansion)) _
                        .ToString & "', " &
                    "'" &
                    (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "secminorglyph3")).Id, GlobalVariables.targetExpansion)) _
                        .ToString & "', " &
                    "'" &
                    (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "secmajorglyph2")).Id, GlobalVariables.targetExpansion)) _
                        .ToString & "', " &
                    "'" &
                    (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "secminorglyph1")).Id, GlobalVariables.targetExpansion)) _
                        .ToString & "', " &
                    "'" &
                    (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "secmajorglyph3")).Id, GlobalVariables.targetExpansion)) _
                        .ToString & "', " &
                    "'" &
                    (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "secprimeglyph1")).Id, GlobalVariables.targetExpansion)) _
                        .ToString & "', " &
                    "'" &
                    (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "secprimeglyph2")).Id, GlobalVariables.targetExpansion)) _
                        .ToString & "', " &
                    "'" &
                    (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "secprimeglyph3")).Id, GlobalVariables.targetExpansion)) _
                        .ToString & "' )", True)
            ElseIf GlobalVariables.targetExpansion = 3 Then
                runSQLCommand_characters_string(
                    "INSERT INTO " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " ( " &
                    GlobalVariables.targetStructure.glyphs_guid_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_spec_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph1_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph2_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph3_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph4_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph5_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph6_col(0) & " ) VALUES ( '" & characterguid.ToString() &
                    "', '0', '" &
                    (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "majorglyph1")).Id, GlobalVariables.targetExpansion)).
                        ToString & "', " &
                    "'" &
                    (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "minorglyph2")).Id, GlobalVariables.targetExpansion)).
                        ToString & "', " &
                    "'" &
                    (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "minorglyph3")).Id, GlobalVariables.targetExpansion)).
                        ToString & "', " &
                    "'" &
                    (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "majorglyph2")).Id, GlobalVariables.targetExpansion)).
                        ToString & "', " &
                    "'" &
                    (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "minorglyph1")).Id, GlobalVariables.targetExpansion)).
                        ToString & "', " &
                    "'" &
                    (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "majorglyph3")).Id, GlobalVariables.targetExpansion)).
                        ToString & "' )", True)
                runSQLCommand_characters_string(
                    "INSERT INTO " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " ( " &
                    GlobalVariables.targetStructure.glyphs_guid_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_spec_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph1_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph2_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph3_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph4_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph5_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph6_col(0) & " ) VALUES ( '" & characterguid.ToString() &
                    "', '1', '" &
                    (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "secmajorglyph1")).Id, GlobalVariables.targetExpansion)) _
                        .ToString & "', " &
                    "'" &
                    (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "secminorglyph2")).Id, GlobalVariables.targetExpansion)) _
                        .ToString & "', " &
                    "'" &
                    (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "secminorglyph3")).Id, GlobalVariables.targetExpansion)) _
                        .ToString & "', " &
                    "'" &
                    (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "secmajorglyph2")).Id, GlobalVariables.targetExpansion)) _
                        .ToString & "', " &
                    "'" &
                    (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "secminorglyph1")).Id, GlobalVariables.targetExpansion)) _
                        .ToString & "', " &
                    "'" &
                    (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "secmajorglyph3")).Id, GlobalVariables.targetExpansion)) _
                        .ToString & "' )", True)
            End If
        End Sub

        Private Sub CreateAtMangos(ByVal characterguid As Integer, ByVal player As Character)
            LogAppend("Creating at Mangos", "GlyphCreation_createAtMangos", False)
            runSQLCommand_characters_string(
                "DELETE FROM " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " WHERE " &
                GlobalVariables.targetStructure.glyphs_guid_col(0) & " = '" & characterguid.ToString() & "'", True)
            runSQLCommand_characters_string(
                "INSERT INTO " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " ( " &
                GlobalVariables.targetStructure.glyphs_guid_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_spec_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_slot_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() &
                "', '0', '4', '" &
                (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "minorglyph1")).Id, GlobalVariables.targetExpansion)).
                    ToString() & "' )", True)
            runSQLCommand_characters_string(
                "INSERT INTO " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " ( " &
                GlobalVariables.targetStructure.glyphs_guid_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_spec_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_slot_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() &
                "', '0', '1', '" &
                (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "minorglyph2")).Id, GlobalVariables.targetExpansion)).
                    ToString() & "' )", True)
            runSQLCommand_characters_string(
                "INSERT INTO " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " ( " &
                GlobalVariables.targetStructure.glyphs_guid_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_spec_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_slot_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() &
                "', '0', '2', '" &
                (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "minorglyph3")).Id, GlobalVariables.targetExpansion)).
                    ToString() & "' )", True)
            runSQLCommand_characters_string(
                "INSERT INTO " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " ( " &
                GlobalVariables.targetStructure.glyphs_guid_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_spec_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_slot_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() &
                "', '0', '0', '" &
                (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "majorglyph1")).Id, GlobalVariables.targetExpansion)).
                    ToString() & "' )", True)
            runSQLCommand_characters_string(
                "INSERT INTO " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " ( " &
                GlobalVariables.targetStructure.glyphs_guid_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_spec_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_slot_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() &
                "', '0', '3', '" &
                (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "majorglyph2")).Id, GlobalVariables.targetExpansion)).
                    ToString() & "' )", True)
            runSQLCommand_characters_string(
                "INSERT INTO " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " ( " &
                GlobalVariables.targetStructure.glyphs_guid_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_spec_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_slot_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() &
                "', '0', '5', '" &
                (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "majorglyph3")).Id, GlobalVariables.targetExpansion)).
                    ToString() & "' )", True)

            runSQLCommand_characters_string(
                "INSERT INTO " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " ( " &
                GlobalVariables.targetStructure.glyphs_guid_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_spec_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_slot_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() &
                "', '1', '4', '" &
                (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "secminorglyph1")).Id, GlobalVariables.targetExpansion)).
                    ToString() & "' )", True)
            runSQLCommand_characters_string(
                "INSERT INTO " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " ( " &
                GlobalVariables.targetStructure.glyphs_guid_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_spec_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_slot_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() &
                "', '1', '1', '" &
                (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "secminorglyph2")).Id, GlobalVariables.targetExpansion)).
                    ToString() & "' )", True)
            runSQLCommand_characters_string(
                "INSERT INTO " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " ( " &
                GlobalVariables.targetStructure.glyphs_guid_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_spec_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_slot_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() &
                "', '1', '2', '" &
                (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "secminorglyph3")).Id, GlobalVariables.targetExpansion)).
                    ToString() & "' )", True)
            runSQLCommand_characters_string(
                "INSERT INTO " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " ( " &
                GlobalVariables.targetStructure.glyphs_guid_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_spec_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_slot_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() &
                "', '1', '0', '" &
                (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "secmajorglyph1")).Id, GlobalVariables.targetExpansion)).
                    ToString() & "' )", True)
            runSQLCommand_characters_string(
                "INSERT INTO " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " ( " &
                GlobalVariables.targetStructure.glyphs_guid_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_spec_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_slot_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() &
                "', '1', '3', '" &
                (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "secmajorglyph2")).Id, GlobalVariables.targetExpansion)).
                    ToString() & "' )", True)
            runSQLCommand_characters_string(
                "INSERT INTO " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " ( " &
                GlobalVariables.targetStructure.glyphs_guid_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_spec_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_slot_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() &
                "', '1', '5', '" &
                (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "secmajorglyph3")).Id, GlobalVariables.targetExpansion)).
                    ToString() & "' )", True)
            If GlobalVariables.targetExpansion = 4 Then
                runSQLCommand_characters_string(
                    "INSERT INTO " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " ( " &
                    GlobalVariables.targetStructure.glyphs_guid_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_spec_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_slot_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() &
                    "', '0', '6', '" &
                    (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "primeglyph1")).Id, GlobalVariables.targetExpansion)).
                        ToString() & "' )", True)
                runSQLCommand_characters_string(
                    "INSERT INTO " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " ( " &
                    GlobalVariables.targetStructure.glyphs_guid_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_spec_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_slot_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() &
                    "', '0', '7', '" &
                    (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "primeglyph2")).Id, GlobalVariables.targetExpansion)).
                        ToString() & "' )", True)
                runSQLCommand_characters_string(
                    "INSERT INTO " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " ( " &
                    GlobalVariables.targetStructure.glyphs_guid_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_spec_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_slot_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() &
                    "', '0', '8', '" &
                    (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "primeglyph3")).Id, GlobalVariables.targetExpansion)).
                        ToString() & "' )", True)

                runSQLCommand_characters_string(
                    "INSERT INTO " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " ( " &
                    GlobalVariables.targetStructure.glyphs_guid_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_spec_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_slot_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() &
                    "', '1', '6', '" &
                    (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "secprimeglyph1")).Id, GlobalVariables.targetExpansion)) _
                        .ToString() & "' )", True)
                runSQLCommand_characters_string(
                    "INSERT INTO " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " ( " &
                    GlobalVariables.targetStructure.glyphs_guid_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_spec_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_slot_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() &
                    "', '1', '7', '" &
                    (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "secprimeglyph2")).Id, GlobalVariables.targetExpansion)) _
                        .ToString() & "' )", True)
                runSQLCommand_characters_string(
                    "INSERT INTO " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " ( " &
                    GlobalVariables.targetStructure.glyphs_guid_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_spec_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_slot_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() &
                    "', '1', '8', '" &
                    (GetGlyphIdByItemId(EscGly(GetCharacterGlyph(player, "secprimeglyph3")).Id, GlobalVariables.targetExpansion)) _
                        .ToString() & "' )", True)
            End If
        End Sub

        Private Function EscGly(ByVal glyph As Glyph)
            If glyph Is Nothing Then
                Return New Glyph() With {.Id = 0}
            Else
                Return glyph
            End If
        End Function
    End Class
End Namespace