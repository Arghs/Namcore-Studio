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
        Public Sub SetCharacterGlyphs(ByVal setId As Integer, Optional charguid As Integer = 0)
            If charguid = 0 Then charguid = GetCharacterSetBySetId(setId).Guid
            LogAppend("Creating Glyphs for character: " & charguid.ToString() & " // setId is : " & setId.ToString(),
                      "GlyphCreation_SetCharacterGlyphs", True)
            Select Case GlobalVariables.targetCore
                Case "arcemu"
                    createAtArcemu(charguid, setId)
                Case "trinity"
                    createAtTrinity(charguid, setId)
                Case "trinitytbc"

                Case "mangos"
                    createAtMangos(charguid, setId)
                    End Select
        End Sub

        Private Sub CreateAtArcemu(ByVal characterguid As Integer, ByVal targetSetId As Integer)
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
            Dim player As Character = GetCharacterSetBySetId(targetSetId)
            glyphstring1 = glyphstring1.Replace("minor1",
                                                (GetGlyphIdBySpellId(GetCharacterGlyph(player, "minorglyph1").Id, GlobalVariables.targetExpansion).ToString))
            glyphstring1 = glyphstring1.Replace("minor2",
                                                (GetGlyphIdBySpellId(GetCharacterGlyph(player, "minorglyph2").Id, GlobalVariables.targetExpansion).ToString))
            glyphstring1 = glyphstring1.Replace("minor3",
                                                (GetGlyphIdBySpellId(GetCharacterGlyph(player, "minorglyph3").Id, GlobalVariables.targetExpansion).ToString))
            glyphstring1 = glyphstring1.Replace("major1",
                                                (GetGlyphIdBySpellId(GetCharacterGlyph(player, "majorglyph1").Id, GlobalVariables.targetExpansion).ToString))
            glyphstring1 = glyphstring1.Replace("major2",
                                                (GetGlyphIdBySpellId(GetCharacterGlyph(player, "majorglyph2").Id, GlobalVariables.targetExpansion).ToString))
            glyphstring1 = glyphstring1.Replace("major3",
                                                (GetGlyphIdBySpellId(GetCharacterGlyph(player, "majorglyph3").Id, GlobalVariables.targetExpansion).ToString))
            glyphstring2 = glyphstring2.Replace("minor1",
                                                (GetGlyphIdBySpellId(GetCharacterGlyph(player, "secminorglyph1").Id, GlobalVariables.targetExpansion).ToString))
            glyphstring2 = glyphstring2.Replace("minor2",
                                                (GetGlyphIdBySpellId(GetCharacterGlyph(player, "secminorglyph2").Id, GlobalVariables.targetExpansion).ToString))
            glyphstring2 = glyphstring2.Replace("minor3",
                                                (GetGlyphIdBySpellId(GetCharacterGlyph(player, "secminorglyph3").Id, GlobalVariables.targetExpansion).ToString))
            glyphstring2 = glyphstring2.Replace("major1",
                                                (GetGlyphIdBySpellId(GetCharacterGlyph(player, "secmajorglyph1").Id, GlobalVariables.targetExpansion).ToString))
            glyphstring2 = glyphstring2.Replace("major2",
                                                (GetGlyphIdBySpellId(GetCharacterGlyph(player, "secmajorglyph2").Id, GlobalVariables.targetExpansion).ToString))
            glyphstring2 = glyphstring2.Replace("major3",
                                                (GetGlyphIdBySpellId(GetCharacterGlyph(player, "secmajorglyph3").Id, GlobalVariables.targetExpansion).ToString))
            runSQLCommand_characters_string(
                "UPDATE " & GlobalVariables.targetStructure.character_tbl(0) & " SET " &
                GlobalVariables.targetStructure.char_glyphs1_col(0) & "='" & glyphstring1 & "' WHERE " &
                GlobalVariables.targetStructure.char_guid_col(0) & "='" & characterguid.ToString() & "'", True)
            runSQLCommand_characters_string(
                "UPDATE " & GlobalVariables.targetStructure.character_tbl(0) & " SET " &
                GlobalVariables.targetStructure.char_glyphs2_col(0) & "='" & glyphstring2 & "' WHERE " &
                GlobalVariables.targetStructure.char_guid_col(0) & "='" & characterguid.ToString() & "'", True)
        End Sub

        Private Sub CreateAtTrinity(ByVal characterguid As Integer, ByVal targetSetId As Integer)
            LogAppend("Creating at Trinity", "GlyphCreation_createAtTrinity", False)
            runSQLCommand_characters_string(
                "DELETE FROM " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " WHERE " &
                GlobalVariables.targetStructure.glyphs_guid_col(0) & " = '" & characterguid.ToString() & "' AND " &
                GlobalVariables.targetStructure.glyphs_spec_col(0) & "='0'", True)
            runSQLCommand_characters_string(
                "DELETE FROM " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " WHERE " &
                GlobalVariables.targetStructure.glyphs_guid_col(0) & " = '" & characterguid.ToString() & "' AND " &
                GlobalVariables.targetStructure.glyphs_spec_col(0) & "='1'", True)
            Dim player As Character = GetCharacterSetBySetId(targetSetId)
            If GlobalVariables.targetExpansion = 4 Then
                runSQLCommand_characters_string(
                    "INSERT INTO " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " ( " &
                    GlobalVariables.targetStructure.glyphs_guid_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_spec_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph1_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph2_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph3_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph4_col(0) &
                    ", " & GlobalVariables.targetStructure.glyphs_glyph5_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph6_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph7_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph8_col(0) &
                    ", " & GlobalVariables.targetStructure.glyphs_glyph9_col(0) & " ) VALUES ( '" & characterguid.ToString() &
                    "', '0', '" &
                    (GetGlyphIdBySpellId(GetCharacterGlyph(player, "majorglyph1").Id, GlobalVariables.targetExpansion)).ToString & "', " &
                    "'" & (GetGlyphIdBySpellId(GetCharacterGlyph(player, "minorglyph2").Id, GlobalVariables.targetExpansion)).ToString & "', " &
                    "'" & (GetGlyphIdBySpellId(GetCharacterGlyph(player, "minorglyph3").Id, GlobalVariables.targetExpansion)).ToString & "', " &
                    "'" & (GetGlyphIdBySpellId(GetCharacterGlyph(player, "majorglyph2").Id, GlobalVariables.targetExpansion)).ToString & "', " &
                    "'" & (GetGlyphIdBySpellId(GetCharacterGlyph(player, "minorglyph1").Id, GlobalVariables.targetExpansion)).ToString & "', " &
                    "'" & (GetGlyphIdBySpellId(GetCharacterGlyph(player, "majorglyph3").Id, GlobalVariables.targetExpansion)).ToString & "', " &
                    "'" & (GetGlyphIdBySpellId(GetCharacterGlyph(player, "primeglyph1").Id, GlobalVariables.targetExpansion)).ToString & "', " &
                    "'" & (GetGlyphIdBySpellId(GetCharacterGlyph(player, "primeglyph2").Id, GlobalVariables.targetExpansion)).ToString & "', " &
                    "'" & (GetGlyphIdBySpellId(GetCharacterGlyph(player, "primeglyph3").Id, GlobalVariables.targetExpansion)).ToString & "' )", True)
                runSQLCommand_characters_string(
                    "INSERT INTO " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " ( " &
                    GlobalVariables.targetStructure.glyphs_guid_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_spec_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph1_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph2_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph3_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph4_col(0) &
                    ", " & GlobalVariables.targetStructure.glyphs_glyph5_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph6_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph7_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph8_col(0) &
                    ", " & GlobalVariables.targetStructure.glyphs_glyph9_col(0) & " ) VALUES ( '" & characterguid.ToString() &
                    "', '1', '" &
                    (GetGlyphIdBySpellId(GetCharacterGlyph(player, "secmajorglyph1").Id, GlobalVariables.targetExpansion)).ToString & "', " &
                    "'" & (GetGlyphIdBySpellId(GetCharacterGlyph(player, "secminorglyph2").Id, GlobalVariables.targetExpansion)).ToString & "', " &
                    "'" & (GetGlyphIdBySpellId(GetCharacterGlyph(player, "secminorglyph3").Id, GlobalVariables.targetExpansion)).ToString & "', " &
                    "'" & (GetGlyphIdBySpellId(GetCharacterGlyph(player, "secmajorglyph2").Id, GlobalVariables.targetExpansion)).ToString & "', " &
                    "'" & (GetGlyphIdBySpellId(GetCharacterGlyph(player, "secminorglyph1").Id, GlobalVariables.targetExpansion)).ToString & "', " &
                    "'" & (GetGlyphIdBySpellId(GetCharacterGlyph(player, "secmajorglyph3").Id, GlobalVariables.targetExpansion)).ToString & "', " &
                    "'" & (GetGlyphIdBySpellId(GetCharacterGlyph(player, "secprimeglyph1").Id, GlobalVariables.targetExpansion)).ToString & "', " &
                    "'" & (GetGlyphIdBySpellId(GetCharacterGlyph(player, "secprimeglyph2").Id, GlobalVariables.targetExpansion)).ToString & "', " &
                    "'" & (GetGlyphIdBySpellId(GetCharacterGlyph(player, "secprimeglyph3").Id, GlobalVariables.targetExpansion)).ToString & "' )", True)
            ElseIf GlobalVariables.targetExpansion = 3 Then
                runSQLCommand_characters_string(
                    "INSERT INTO " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " ( " &
                    GlobalVariables.targetStructure.glyphs_guid_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_spec_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph1_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph2_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph3_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph4_col(0) &
                    ", " & GlobalVariables.targetStructure.glyphs_glyph5_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph6_col(0) & " ) VALUES ( '" & characterguid.ToString() &
                    "', '0', '" &
                    (GetGlyphIdBySpellId(GetCharacterGlyph(player, "majorglyph1").Id, GlobalVariables.targetExpansion)).ToString & "', " &
                    "'" & (GetGlyphIdBySpellId(GetCharacterGlyph(player, "minorglyph2").Id, GlobalVariables.targetExpansion)).ToString & "', " &
                    "'" & (GetGlyphIdBySpellId(GetCharacterGlyph(player, "minorglyph3").Id, GlobalVariables.targetExpansion)).ToString & "', " &
                    "'" & (GetGlyphIdBySpellId(GetCharacterGlyph(player, "majorglyph2").Id, GlobalVariables.targetExpansion)).ToString & "', " &
                    "'" & (GetGlyphIdBySpellId(GetCharacterGlyph(player, "minorglyph1").Id, GlobalVariables.targetExpansion)).ToString & "', " &
                    "'" & (GetGlyphIdBySpellId(GetCharacterGlyph(player, "majorglyph3").Id, GlobalVariables.targetExpansion)).ToString & "' )", True)
                runSQLCommand_characters_string(
                    "INSERT INTO " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " ( " &
                    GlobalVariables.targetStructure.glyphs_guid_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_spec_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph1_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph2_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph3_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph4_col(0) &
                    ", " & GlobalVariables.targetStructure.glyphs_glyph5_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph6_col(0) & " ) VALUES ( '" & characterguid.ToString() &
                    "', '1', '" &
                    (GetGlyphIdBySpellId(GetCharacterGlyph(player, "secmajorglyph1").Id, GlobalVariables.targetExpansion)).ToString & "', " &
                    "'" & (GetGlyphIdBySpellId(GetCharacterGlyph(player, "secminorglyph2").Id, GlobalVariables.targetExpansion)).ToString & "', " &
                    "'" & (GetGlyphIdBySpellId(GetCharacterGlyph(player, "secminorglyph3").Id, GlobalVariables.targetExpansion)).ToString & "', " &
                    "'" & (GetGlyphIdBySpellId(GetCharacterGlyph(player, "secmajorglyph2").Id, GlobalVariables.targetExpansion)).ToString & "', " &
                    "'" & (GetGlyphIdBySpellId(GetCharacterGlyph(player, "secminorglyph1").Id, GlobalVariables.targetExpansion)).ToString & "', " &
                    "'" & (GetGlyphIdBySpellId(GetCharacterGlyph(player, "secmajorglyph3").Id, GlobalVariables.targetExpansion)).ToString & "' )", True)
            End If
        End Sub

        Private Sub CreateAtMangos(ByVal characterguid As Integer, ByVal targetSetId As Integer)
            LogAppend("Creating at Mangos", "GlyphCreation_createAtMangos", False)
            Dim player As Character = GetCharacterSetBySetId(targetSetId)
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
                (GetGlyphIdBySpellId(GetCharacterGlyph(player, "minorglyph1").Id, GlobalVariables.targetExpansion)).ToString() & "' )", True)
            runSQLCommand_characters_string(
                "INSERT INTO " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " ( " &
                GlobalVariables.targetStructure.glyphs_guid_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_spec_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_slot_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() &
                "', '0', '1', '" &
                (GetGlyphIdBySpellId(GetCharacterGlyph(player, "minorglyph2").Id, GlobalVariables.targetExpansion)).ToString() & "' )", True)
            runSQLCommand_characters_string(
                "INSERT INTO " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " ( " &
                GlobalVariables.targetStructure.glyphs_guid_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_spec_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_slot_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() &
                "', '0', '2', '" &
                (GetGlyphIdBySpellId(GetCharacterGlyph(player, "minorglyph3").Id, GlobalVariables.targetExpansion)).ToString() & "' )", True)
            runSQLCommand_characters_string(
                "INSERT INTO " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " ( " &
                GlobalVariables.targetStructure.glyphs_guid_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_spec_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_slot_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() &
                "', '0', '0', '" &
                (GetGlyphIdBySpellId(GetCharacterGlyph(player, "majorglyph1").Id, GlobalVariables.targetExpansion)).ToString() & "' )", True)
            runSQLCommand_characters_string(
                "INSERT INTO " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " ( " &
                GlobalVariables.targetStructure.glyphs_guid_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_spec_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_slot_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() &
                "', '0', '3', '" &
                (GetGlyphIdBySpellId(GetCharacterGlyph(player, "majorglyph2").Id, GlobalVariables.targetExpansion)).ToString() & "' )", True)
            runSQLCommand_characters_string(
                "INSERT INTO " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " ( " &
                GlobalVariables.targetStructure.glyphs_guid_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_spec_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_slot_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() &
                "', '0', '5', '" &
                (GetGlyphIdBySpellId(GetCharacterGlyph(player, "majorglyph3").Id, GlobalVariables.targetExpansion)).ToString() & "' )", True)

            runSQLCommand_characters_string(
                "INSERT INTO " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " ( " &
                GlobalVariables.targetStructure.glyphs_guid_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_spec_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_slot_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() &
                "', '1', '4', '" &
                (GetGlyphIdBySpellId(GetCharacterGlyph(player, "secminorglyph1").Id, GlobalVariables.targetExpansion)).ToString() & "' )", True)
            runSQLCommand_characters_string(
                "INSERT INTO " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " ( " &
                GlobalVariables.targetStructure.glyphs_guid_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_spec_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_slot_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() &
                "', '1', '1', '" &
                (GetGlyphIdBySpellId(GetCharacterGlyph(player, "secminorglyph2").Id, GlobalVariables.targetExpansion)).ToString() & "' )", True)
            runSQLCommand_characters_string(
                "INSERT INTO " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " ( " &
                GlobalVariables.targetStructure.glyphs_guid_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_spec_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_slot_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() &
                "', '1', '2', '" &
                (GetGlyphIdBySpellId(GetCharacterGlyph(player, "secminorglyph3").Id, GlobalVariables.targetExpansion)).ToString() & "' )", True)
            runSQLCommand_characters_string(
                "INSERT INTO " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " ( " &
                GlobalVariables.targetStructure.glyphs_guid_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_spec_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_slot_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() &
                "', '1', '0', '" &
                (GetGlyphIdBySpellId(GetCharacterGlyph(player, "secmajorglyph1").Id, GlobalVariables.targetExpansion)).ToString() & "' )", True)
            runSQLCommand_characters_string(
                "INSERT INTO " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " ( " &
                GlobalVariables.targetStructure.glyphs_guid_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_spec_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_slot_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() &
                "', '1', '3', '" &
                (GetGlyphIdBySpellId(GetCharacterGlyph(player, "secmajorglyph2").Id, GlobalVariables.targetExpansion)).ToString() & "' )", True)
            runSQLCommand_characters_string(
                "INSERT INTO " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " ( " &
                GlobalVariables.targetStructure.glyphs_guid_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_spec_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_slot_col(0) & ", " &
                GlobalVariables.targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() &
                "', '1', '5', '" &
                (GetGlyphIdBySpellId(GetCharacterGlyph(player, "secmajorglyph3").Id, GlobalVariables.targetExpansion)).ToString() & "' )", True)
            If GlobalVariables.targetExpansion = 4 Then
                runSQLCommand_characters_string(
                    "INSERT INTO " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " ( " &
                    GlobalVariables.targetStructure.glyphs_guid_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_spec_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_slot_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() &
                    "', '0', '6', '" &
                    (GetGlyphIdBySpellId(GetCharacterGlyph(player, "primeglyph1").Id, GlobalVariables.targetExpansion)).ToString() & "' )", True)
                runSQLCommand_characters_string(
                    "INSERT INTO " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " ( " &
                    GlobalVariables.targetStructure.glyphs_guid_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_spec_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_slot_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() &
                    "', '0', '7', '" &
                    (GetGlyphIdBySpellId(GetCharacterGlyph(player, "primeglyph2").Id, GlobalVariables.targetExpansion)).ToString() & "' )", True)
                runSQLCommand_characters_string(
                    "INSERT INTO " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " ( " &
                    GlobalVariables.targetStructure.glyphs_guid_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_spec_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_slot_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() &
                    "', '0', '8', '" &
                    (GetGlyphIdBySpellId(GetCharacterGlyph(player, "primeglyph3").Id, GlobalVariables.targetExpansion)).ToString() & "' )", True)

                runSQLCommand_characters_string(
                    "INSERT INTO " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " ( " &
                    GlobalVariables.targetStructure.glyphs_guid_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_spec_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_slot_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() &
                    "', '1', '6', '" &
                    (GetGlyphIdBySpellId(GetCharacterGlyph(player, "secprimeglyph1").Id, GlobalVariables.targetExpansion)).ToString() & "' )", True)
                runSQLCommand_characters_string(
                    "INSERT INTO " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " ( " &
                    GlobalVariables.targetStructure.glyphs_guid_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_spec_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_slot_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() &
                    "', '1', '7', '" &
                    (GetGlyphIdBySpellId(GetCharacterGlyph(player, "secprimeglyph2").Id, GlobalVariables.targetExpansion)).ToString() & "' )", True)
                runSQLCommand_characters_string(
                    "INSERT INTO " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " ( " &
                    GlobalVariables.targetStructure.glyphs_guid_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_spec_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_slot_col(0) & ", " &
                    GlobalVariables.targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() &
                    "', '1', '8', '" &
                    (GetGlyphIdBySpellId(GetCharacterGlyph(player, "secprimeglyph3").Id, GlobalVariables.targetExpansion)).ToString() & "' )", True)
            End If
        End Sub
    End Class
End Namespace