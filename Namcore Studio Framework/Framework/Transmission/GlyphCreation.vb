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

Imports NCFramework.EventLogging
Imports NCFramework.CommandHandler
Imports NCFramework.GlobalVariables
Imports NCFramework.Basics
Imports NCFramework.Conversions
Imports NCFramework.SpellItem_Information
Public Class GlyphCreation
    Public Sub SetCharacterGlyphs(ByVal setId As Integer, Optional charguid As Integer = 0)
        If charguid = 0 Then charguid = characterGUID
        LogAppend("Creating Glyphs for character: " & charguid.ToString() & " // setId is : " & setId.ToString(), "GlyphCreation_SetCharacterGlyphs", True)
        Select Case targetCore
            Case "arcemu"
                createAtArcemu(charguid, setId)
            Case "trinity"
                createAtTrinity(charguid, setId)
            Case "trinitytbc"

            Case "mangos"
                createAtMangos(charguid, setId)
            Case Else

        End Select
    End Sub
    Private Sub createAtArcemu(ByVal characterguid As Integer, ByVal targetSetId As Integer)
        LogAppend("Creating at arcemu", "GlyphCreation_createAtArcemu", False)
        runSQLCommand_characters_string("DELETE " & targetStructure.char_glyphs1_col(0) & " FROM " & targetStructure.character_tbl(0) & " WHERE " & targetStructure.char_guid_col(0) & " = '" & characterguid.ToString() & "'", True)
        runSQLCommand_characters_string("DELETE " & targetStructure.char_glyphs2_col(0) & " FROM " & targetStructure.character_tbl(0) & " WHERE " & targetStructure.char_guid_col(0) & " = '" & characterguid.ToString() & "'", True)
        Dim glyphstring1 As String = "major1,minor1,minor2,major2,minor3,major3,"
        Dim glyphstring2 As String = glyphstring1
        Dim player As Character = GetCharacterSetBySetId(targetSetId)
        glyphstring1 = glyphstring1.Replace("minor1", (GetGlyphIdByItemId(GetCharacterGlyph(player, "minorglyph1").id).ToString))
        glyphstring1 = glyphstring1.Replace("minor2", (GetGlyphIdByItemId(GetCharacterGlyph(player, "minorglyph2").id).ToString))
        glyphstring1 = glyphstring1.Replace("minor3", (GetGlyphIdByItemId(GetCharacterGlyph(player, "minorglyph3").id).ToString))
        glyphstring1 = glyphstring1.Replace("major1", (GetGlyphIdByItemId(GetCharacterGlyph(player, "majorglyph1").id).ToString))
        glyphstring1 = glyphstring1.Replace("major2", (GetGlyphIdByItemId(GetCharacterGlyph(player, "majorglyph2").id).ToString))
        glyphstring1 = glyphstring1.Replace("major3", (GetGlyphIdByItemId(GetCharacterGlyph(player, "majorglyph3").id).ToString))
        glyphstring2 = glyphstring2.Replace("minor1", (GetGlyphIdByItemId(GetCharacterGlyph(player, "secminorglyph1").id).ToString))
        glyphstring2 = glyphstring2.Replace("minor2", (GetGlyphIdByItemId(GetCharacterGlyph(player, "secminorglyph2").id).ToString))
        glyphstring2 = glyphstring2.Replace("minor3", (GetGlyphIdByItemId(GetCharacterGlyph(player, "secminorglyph3").id).ToString))
        glyphstring2 = glyphstring2.Replace("major1", (GetGlyphIdByItemId(GetCharacterGlyph(player, "secmajorglyph1").id).ToString))
        glyphstring2 = glyphstring2.Replace("major2", (GetGlyphIdByItemId(GetCharacterGlyph(player, "secmajorglyph2").id).ToString))
        glyphstring2 = glyphstring2.Replace("major3", (GetGlyphIdByItemId(GetCharacterGlyph(player, "secmajorglyph3").id).ToString))
        runSQLCommand_characters_string("UPDATE " & targetStructure.character_tbl(0) & " SET " & targetStructure.char_glyphs1_col(0) & "='" & glyphstring1 & "' WHERE " & targetStructure.char_guid_col(0) & "='" & characterguid.ToString() & "'", True)
        runSQLCommand_characters_string("UPDATE " & targetStructure.character_tbl(0) & " SET " & targetStructure.char_glyphs2_col(0) & "='" & glyphstring2 & "' WHERE " & targetStructure.char_guid_col(0) & "='" & characterguid.ToString() & "'", True)
    End Sub
    Private Sub createAtTrinity(ByVal characterguid As Integer, ByVal targetSetId As Integer)
        LogAppend("Creating at Trinity", "GlyphCreation_createAtTrinity", False)
        runSQLCommand_characters_string("DELETE FROM " & targetStructure.character_glyphs_tbl(0) & " WHERE " & targetStructure.glyphs_guid_col(0) & " = '" & characterguid.ToString() & "' AND " & targetStructure.glyphs_spec_col(0) & "='0'", True)
        runSQLCommand_characters_string("DELETE FROM " & targetStructure.character_glyphs_tbl(0) & " WHERE " & targetStructure.glyphs_guid_col(0) & " = '" & characterguid.ToString() & "' AND " & targetStructure.glyphs_spec_col(0) & "='1'", True)
        Dim player As Character = GetCharacterSetBySetId(targetSetId)
        If targetExpansion = 4 Then
            runSQLCommand_characters_string("INSERT INTO " & targetStructure.character_glyphs_tbl(0) & " ( " & targetStructure.glyphs_guid_col(0) & ", " & targetStructure.glyphs_spec_col(0) & ", " &
                                            targetStructure.glyphs_glyph1_col(0) & ", " & targetStructure.glyphs_glyph2_col(0) & ", " & targetStructure.glyphs_glyph3_col(0) & ", " & targetStructure.glyphs_glyph4_col(0) &
                                            ", " & targetStructure.glyphs_glyph5_col(0) & ", " & targetStructure.glyphs_glyph6_col(0) & ", " & targetStructure.glyphs_glyph7_col(0) & ", " & targetStructure.glyphs_glyph8_col(0) &
                                            ", " & targetStructure.glyphs_glyph9_col(0) & " ) VALUES ( '" & characterguid.ToString() & "', '0', '" &
                                                    (GetGlyphIdByItemId(GetCharacterGlyph(player, "majorglyph1").id)).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetCharacterGlyph(player, "minorglyph2").id)).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetCharacterGlyph(player, "minorglyph3").id)).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetCharacterGlyph(player, "majorglyph2").id)).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetCharacterGlyph(player, "minorglyph1").id)).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetCharacterGlyph(player, "majorglyph3").id)).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetCharacterGlyph(player, "primeglyph1").id)).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetCharacterGlyph(player, "primeglyph2").id)).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetCharacterGlyph(player, "primeglyph3").id)).ToString & "' )", True)
            runSQLCommand_characters_string("INSERT INTO " & targetStructure.character_glyphs_tbl(0) & " ( " & targetStructure.glyphs_guid_col(0) & ", " & targetStructure.glyphs_spec_col(0) & ", " &
                                            targetStructure.glyphs_glyph1_col(0) & ", " & targetStructure.glyphs_glyph2_col(0) & ", " & targetStructure.glyphs_glyph3_col(0) & ", " & targetStructure.glyphs_glyph4_col(0) &
                                            ", " & targetStructure.glyphs_glyph5_col(0) & ", " & targetStructure.glyphs_glyph6_col(0) & ", " & targetStructure.glyphs_glyph7_col(0) & ", " & targetStructure.glyphs_glyph8_col(0) &
                                            ", " & targetStructure.glyphs_glyph9_col(0) & " ) VALUES ( '" & characterguid.ToString() & "', '1', '" &
                                                    (GetGlyphIdByItemId(GetCharacterGlyph(player, "secmajorglyph1").id)).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetCharacterGlyph(player, "secminorglyph2").id)).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetCharacterGlyph(player, "secminorglyph3").id)).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetCharacterGlyph(player, "secmajorglyph2").id)).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetCharacterGlyph(player, "secminorglyph1").id)).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetCharacterGlyph(player, "secmajorglyph3").id)).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetCharacterGlyph(player, "secprimeglyph1").id)).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetCharacterGlyph(player, "secprimeglyph2").id)).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetCharacterGlyph(player, "secprimeglyph3").id)).ToString & "' )", True)
        ElseIf targetExpansion = 3 Then
            runSQLCommand_characters_string("INSERT INTO " & targetStructure.character_glyphs_tbl(0) & " ( " & targetStructure.glyphs_guid_col(0) & ", " & targetStructure.glyphs_spec_col(0) & ", " &
                                            targetStructure.glyphs_glyph1_col(0) & ", " & targetStructure.glyphs_glyph2_col(0) & ", " & targetStructure.glyphs_glyph3_col(0) & ", " & targetStructure.glyphs_glyph4_col(0) &
                                            ", " & targetStructure.glyphs_glyph5_col(0) & ", " & targetStructure.glyphs_glyph6_col(0) & " ) VALUES ( '" & characterguid.ToString() & "', '0', '" &
                                                    (GetGlyphIdByItemId(GetCharacterGlyph(player, "majorglyph1").id)).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetCharacterGlyph(player, "minorglyph2").id)).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetCharacterGlyph(player, "minorglyph3").id)).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetCharacterGlyph(player, "majorglyph2").id)).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetCharacterGlyph(player, "minorglyph1").id)).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetCharacterGlyph(player, "majorglyph3").id)).ToString & "' )", True)
            runSQLCommand_characters_string("INSERT INTO " & targetStructure.character_glyphs_tbl(0) & " ( " & targetStructure.glyphs_guid_col(0) & ", " & targetStructure.glyphs_spec_col(0) & ", " &
                                            targetStructure.glyphs_glyph1_col(0) & ", " & targetStructure.glyphs_glyph2_col(0) & ", " & targetStructure.glyphs_glyph3_col(0) & ", " & targetStructure.glyphs_glyph4_col(0) &
                                            ", " & targetStructure.glyphs_glyph5_col(0) & ", " & targetStructure.glyphs_glyph6_col(0) & " ) VALUES ( '" & characterguid.ToString() & "', '1', '" &
                                                    (GetGlyphIdByItemId(GetCharacterGlyph(player, "secmajorglyph1").id)).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetCharacterGlyph(player, "secminorglyph2").id)).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetCharacterGlyph(player, "secminorglyph3").id)).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetCharacterGlyph(player, "secmajorglyph2").id)).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetCharacterGlyph(player, "secminorglyph1").id)).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetCharacterGlyph(player, "secmajorglyph3").id)).ToString & "' )", True)
        End If
    End Sub
    Private Sub createAtMangos(ByVal characterguid As Integer, ByVal targetSetId As Integer)
        LogAppend("Creating at Mangos", "GlyphCreation_createAtMangos", False)
        Dim player As Character = GetCharacterSetBySetId(targetSetId)
        runSQLCommand_characters_string("DELETE FROM " & targetStructure.character_glyphs_tbl(0) & " WHERE " & targetStructure.glyphs_guid_col(0) & " = '" & characterguid.ToString() & "'", True)
        runSQLCommand_characters_string("INSERT INTO " & targetStructure.character_glyphs_tbl(0) & " ( " & targetStructure.glyphs_guid_col(0) & ", " & targetStructure.glyphs_spec_col(0) & ", " &
                                        targetStructure.glyphs_slot_col(0) & ", " & targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() & "', '0', '4', '" &
                                            (GetGlyphIdByItemId(GetCharacterGlyph(player, "minorglyph1").id)).tostring() & "' )", True)
        runSQLCommand_characters_string("INSERT INTO " & targetStructure.character_glyphs_tbl(0) & " ( " & targetStructure.glyphs_guid_col(0) & ", " & targetStructure.glyphs_spec_col(0) & ", " &
                                        targetStructure.glyphs_slot_col(0) & ", " & targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() & "', '0', '1', '" &
                                        (GetGlyphIdByItemId(GetCharacterGlyph(player, "minorglyph2").id)).tostring() & "' )", True)
        runSQLCommand_characters_string("INSERT INTO " & targetStructure.character_glyphs_tbl(0) & " ( " & targetStructure.glyphs_guid_col(0) & ", " & targetStructure.glyphs_spec_col(0) & ", " &
                                        targetStructure.glyphs_slot_col(0) & ", " & targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() & "', '0', '2', '" &
                                        (GetGlyphIdByItemId(GetCharacterGlyph(player, "minorglyph3").id)).tostring() & "' )", True)
        runSQLCommand_characters_string("INSERT INTO " & targetStructure.character_glyphs_tbl(0) & " ( " & targetStructure.glyphs_guid_col(0) & ", " & targetStructure.glyphs_spec_col(0) & ", " &
                                        targetStructure.glyphs_slot_col(0) & ", " & targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() & "', '0', '0', '" &
                                        (GetGlyphIdByItemId(GetCharacterGlyph(player, "majorglyph1").id)).tostring() & "' )", True)
        runSQLCommand_characters_string("INSERT INTO " & targetStructure.character_glyphs_tbl(0) & " ( " & targetStructure.glyphs_guid_col(0) & ", " & targetStructure.glyphs_spec_col(0) & ", " &
                                        targetStructure.glyphs_slot_col(0) & ", " & targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() & "', '0', '3', '" &
                                        (GetGlyphIdByItemId(GetCharacterGlyph(player, "majorglyph2").id)).tostring() & "' )", True)
        runSQLCommand_characters_string("INSERT INTO " & targetStructure.character_glyphs_tbl(0) & " ( " & targetStructure.glyphs_guid_col(0) & ", " & targetStructure.glyphs_spec_col(0) & ", " &
                                        targetStructure.glyphs_slot_col(0) & ", " & targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() & "', '0', '5', '" &
                                        (GetGlyphIdByItemId(GetCharacterGlyph(player, "majorglyph3").id)).tostring() & "' )", True)

        runSQLCommand_characters_string("INSERT INTO " & targetStructure.character_glyphs_tbl(0) & " ( " & targetStructure.glyphs_guid_col(0) & ", " & targetStructure.glyphs_spec_col(0) & ", " &
                                        targetStructure.glyphs_slot_col(0) & ", " & targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() & "', '1', '4', '" &
                                           (GetGlyphIdByItemId(GetCharacterGlyph(player, "secminorglyph1").id)).tostring() & "' )", True)
        runSQLCommand_characters_string("INSERT INTO " & targetStructure.character_glyphs_tbl(0) & " ( " & targetStructure.glyphs_guid_col(0) & ", " & targetStructure.glyphs_spec_col(0) & ", " &
                                        targetStructure.glyphs_slot_col(0) & ", " & targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() & "', '1', '1', '" &
                                        (GetGlyphIdByItemId(GetCharacterGlyph(player, "secminorglyph2").id)).tostring() & "' )", True)
        runSQLCommand_characters_string("INSERT INTO " & targetStructure.character_glyphs_tbl(0) & " ( " & targetStructure.glyphs_guid_col(0) & ", " & targetStructure.glyphs_spec_col(0) & ", " &
                                        targetStructure.glyphs_slot_col(0) & ", " & targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() & "', '1', '2', '" &
                                        (GetGlyphIdByItemId(GetCharacterGlyph(player, "secminorglyph3").id)).tostring() & "' )", True)
        runSQLCommand_characters_string("INSERT INTO " & targetStructure.character_glyphs_tbl(0) & " ( " & targetStructure.glyphs_guid_col(0) & ", " & targetStructure.glyphs_spec_col(0) & ", " &
                                        targetStructure.glyphs_slot_col(0) & ", " & targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() & "', '1', '0', '" &
                                        (GetGlyphIdByItemId(GetCharacterGlyph(player, "secmajorglyph1").id)).tostring() & "' )", True)
        runSQLCommand_characters_string("INSERT INTO " & targetStructure.character_glyphs_tbl(0) & " ( " & targetStructure.glyphs_guid_col(0) & ", " & targetStructure.glyphs_spec_col(0) & ", " &
                                        targetStructure.glyphs_slot_col(0) & ", " & targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() & "', '1', '3', '" &
                                        (GetGlyphIdByItemId(GetCharacterGlyph(player, "secmajorglyph2").id)).tostring() & "' )", True)
        runSQLCommand_characters_string("INSERT INTO " & targetStructure.character_glyphs_tbl(0) & " ( " & targetStructure.glyphs_guid_col(0) & ", " & targetStructure.glyphs_spec_col(0) & ", " &
                                        targetStructure.glyphs_slot_col(0) & ", " & targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() & "', '1', '5', '" &
                                        (GetGlyphIdByItemId(GetCharacterGlyph(player, "secmajorglyph3").id)).tostring() & "' )", True)
        If targetExpansion = 4 Then
            runSQLCommand_characters_string("INSERT INTO " & targetStructure.character_glyphs_tbl(0) & " ( " & targetStructure.glyphs_guid_col(0) & ", " & targetStructure.glyphs_spec_col(0) & ", " &
                                        targetStructure.glyphs_slot_col(0) & ", " & targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() & "', '0', '6', '" &
                                            (GetGlyphIdByItemId(GetCharacterGlyph(player, "primeglyph1").id)).ToString() & "' )", True)
            runSQLCommand_characters_string("INSERT INTO " & targetStructure.character_glyphs_tbl(0) & " ( " & targetStructure.glyphs_guid_col(0) & ", " & targetStructure.glyphs_spec_col(0) & ", " &
                                        targetStructure.glyphs_slot_col(0) & ", " & targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() & "', '0', '7', '" &
                                            (GetGlyphIdByItemId(GetCharacterGlyph(player, "primeglyph2").id)).ToString() & "' )", True)
            runSQLCommand_characters_string("INSERT INTO " & targetStructure.character_glyphs_tbl(0) & " ( " & targetStructure.glyphs_guid_col(0) & ", " & targetStructure.glyphs_spec_col(0) & ", " &
                                        targetStructure.glyphs_slot_col(0) & ", " & targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() & "', '0', '8', '" &
                                            (GetGlyphIdByItemId(GetCharacterGlyph(player, "primeglyph3").id)).ToString() & "' )", True)

            runSQLCommand_characters_string("INSERT INTO " & targetStructure.character_glyphs_tbl(0) & " ( " & targetStructure.glyphs_guid_col(0) & ", " & targetStructure.glyphs_spec_col(0) & ", " &
                                        targetStructure.glyphs_slot_col(0) & ", " & targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() & "', '1', '6', '" &
                                            (GetGlyphIdByItemId(GetCharacterGlyph(player, "secprimeglyph1").id)).ToString() & "' )", True)
            runSQLCommand_characters_string("INSERT INTO " & targetStructure.character_glyphs_tbl(0) & " ( " & targetStructure.glyphs_guid_col(0) & ", " & targetStructure.glyphs_spec_col(0) & ", " &
                                        targetStructure.glyphs_slot_col(0) & ", " & targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() & "', '1', '7', '" &
                                            (GetGlyphIdByItemId(GetCharacterGlyph(player, "secprimeglyph2").id)).ToString() & "' )", True)
            runSQLCommand_characters_string("INSERT INTO " & targetStructure.character_glyphs_tbl(0) & " ( " & targetStructure.glyphs_guid_col(0) & ", " & targetStructure.glyphs_spec_col(0) & ", " &
                                        targetStructure.glyphs_slot_col(0) & ", " & targetStructure.glyphs_glyph_col(0) & " ) VALUES ( '" & characterguid.ToString() & "', '1', '8', '" &
                                            (GetGlyphIdByItemId(GetCharacterGlyph(player, "secprimeglyph3").id)).ToString() & "' )", True)
        End If
    End Sub
End Class
