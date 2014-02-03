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
'*      /Filename:      UpdateGlyphsHandler
'*      /Description:   Handles character glyph update requests
'++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.Database
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Modules
Imports libnc.Provider

Namespace Framework.Core.Update
    Public Class UpdateGlyphsHandler
        Public Sub UpdateGlyphs(ByVal player As Character, ByVal modPlayer As Character)
            LogAppend("Updating character glyphs", "UpdateGlyphsHandler_UpdateGlyphs", True)
            If GlobalVariables.sourceExpansion < 3 Then
                '// Cannot create glyphs in pre WotLK db
                LogAppend("Cannot create glyphs in pre WotLK db!", "UpdateGlyphsHandler_UpdateGlyphs", True, True)
                Exit Sub
            End If
            '// Any new glyphs?
            For Each gly As Glyph In _
                From gly1 In modPlayer.PlayerGlyphs Where Not gly1 Is Nothing
                    Let result = player.PlayerGlyphs.Find(Function(glyph) glyph.Id = gly1.Id) Where result Is Nothing
                    Select gly1
                CreateGlyph(modPlayer, gly)
            Next
            '// Any deleted glyphs?
            For Each gly As Glyph In _
                From gly1 In player.PlayerGlyphs Where Not gly1 Is Nothing
                    Let result = modPlayer.PlayerGlyphs.Find(Function(glyph) glyph.Id = gly1.Id) Where result Is Nothing
                    Select gly1
                DeleteGlyph(modPlayer, gly)
            Next
        End Sub

        Private Sub CreateGlyph(ByVal player As Character, ByVal glyph2Add As Glyph)
            Select Case GlobalVariables.sourceCore
                Case "arcemu"
                    Try
                        If glyph2Add.Type = 3 Then
                            '// Cannot create primary glyphs in pre Cataclysm db
                            LogAppend("Cannot create primary glyphs in pre Cataclysm db!",
                                      "UpdateGlyphsHandler_CreateGlyph", True, True)
                            Exit Sub
                        End If
                        Dim useGlyphCol As String
                        Select Case glyph2Add.Spec
                            Case 0 : useGlyphCol = GlobalVariables.sourceStructure.char_glyphs1_col(0)
                            Case Else : useGlyphCol = GlobalVariables.sourceStructure.char_glyphs2_col(0)
                        End Select
                        Dim glyphString As String = runSQLCommand_characters_string(
                            "SELECT `" & useGlyphCol &
                            "` FROM `" & GlobalVariables.sourceStructure.character_tbl(0) &
                            "` WHERE `" & GlobalVariables.sourceStructure.char_guid_col(0) & "`='" &
                            player.Guid.ToString & "'", False)
                        If glyphString = "" Then
                            If GlobalVariables.sourceExpansion = 4 Then
                                glyphString = ",,,,,,,,,"
                            Else
                                glyphString = ",,,,,,"
                            End If
                        End If
                        Dim parts() As String = glyphString.Split(","c)
                        Dim baseInt As UInteger
                        Select Case glyph2Add.Type
                            Case 1 : baseInt = 0
                            Case 2 : baseInt = 3
                            Case 3 : baseInt = 6
                        End Select
                        If glyph2Add.Slotname.Contains("1") Then
                            parts(baseInt) = GetGlyphIdByItemId(glyph2Add.Id, GlobalVariables.targetExpansion)
                        ElseIf glyph2Add.Slotname.Contains("2") Then
                            parts(baseInt + 1) = GetGlyphIdByItemId(glyph2Add.Id, GlobalVariables.targetExpansion)
                        Else
                            parts(baseInt + 2) = GetGlyphIdByItemId(glyph2Add.Id, GlobalVariables.targetExpansion)
                        End If
                        glyphString = String.Join(",", parts)
                        runSQLCommand_characters_string(
                            "UPDATE `" & GlobalVariables.sourceStructure.character_tbl(0) &
                            "` SET `" & useGlyphCol & "`='" & glyphString & "'" &
                            " WHERE `" & GlobalVariables.sourceStructure.char_guid_col(0) & "`='" & player.Guid.ToString &
                            "'", False)
                    Catch ex As Exception
                        LogAppend("Exception occured during glyph creation: " & ex.ToString,
                                  "UpdateGlyphsHandler_CreateGlyph", False, True)
                    End Try
                Case "trinity"
                    Try
                        If glyph2Add.Type > GlobalVariables.sourceExpansion Then
                            '// Cannot create primary glyphs in pre Cataclysm db
                            LogAppend("Cannot create primary glyphs in pre Cataclysm db!",
                                      "UpdateGlyphsHandler_CreateGlyph", True, True)
                            Exit Sub
                        End If
                        Dim targetCol As String = ""
                        Select Case True
                            Case glyph2Add.Slotname.Contains("minorglyph1")
                                targetCol = GlobalVariables.sourceStructure.glyphs_glyph5_col(0)
                            Case glyph2Add.Slotname.Contains("minorglyph2")
                                targetCol = GlobalVariables.sourceStructure.glyphs_glyph2_col(0)
                            Case glyph2Add.Slotname.Contains("minorglyph3")
                                targetCol = GlobalVariables.sourceStructure.glyphs_glyph3_col(0)
                            Case glyph2Add.Slotname.Contains("majorglyph1")
                                targetCol = GlobalVariables.sourceStructure.glyphs_glyph1_col(0)
                            Case glyph2Add.Slotname.Contains("majorglyph2")
                                targetCol = GlobalVariables.sourceStructure.glyphs_glyph4_col(0)
                            Case glyph2Add.Slotname.Contains("majorglyph3")
                                targetCol = GlobalVariables.sourceStructure.glyphs_glyph6_col(0)
                            Case glyph2Add.Slotname.Contains("primeglyph1")
                                targetCol = GlobalVariables.sourceStructure.glyphs_glyph7_col(0)
                            Case glyph2Add.Slotname.Contains("primeglyph2")
                                targetCol = GlobalVariables.sourceStructure.glyphs_glyph8_col(0)
                            Case glyph2Add.Slotname.Contains("primeglyph3")
                                targetCol = GlobalVariables.sourceStructure.glyphs_glyph9_col(0)
                            Case Else
                                LogAppend("Invalid slotname: " & glyph2Add.Slotname, "UpdateGlyphsHandler_CreateGlyph",
                                          False, True)
                        End Select
                        runSQLCommand_characters_string(
                            "UPDATE `" & GlobalVariables.targetStructure.character_glyphs_tbl(0) & "` SET `" & targetCol &
                            "`='" &
                            GetGlyphIdByItemId(glyph2Add.Id, GlobalVariables.sourceExpansion).ToString() & "' WHERE `" &
                            GlobalVariables.targetStructure.glyphs_spec_col(0) & "`='" & glyph2Add.Spec.ToString() &
                            "' AND `" &
                            GlobalVariables.targetStructure.glyphs_guid_col(0) & "`='" & player.Guid.ToString() & "'")
                    Catch ex As Exception
                        LogAppend("Exception occured during glyph creation: " & ex.ToString,
                                  "UpdateGlyphsHandler_CreateGlyph", False, True)
                    End Try
                Case "mangos"
                    Try
                        If glyph2Add.Type > GlobalVariables.sourceExpansion Then
                            '// Cannot create primary glyphs in pre Cataclysm db
                            LogAppend("Cannot create primary glyphs in pre Cataclysm db!",
                                      "UpdateGlyphsHandler_CreateGlyph", True, True)
                            Exit Sub
                        End If
                        Dim targetSlot As Integer = 0
                        Select Case True
                            Case glyph2Add.Slotname.Contains("minorglyph1") : targetSlot = 4
                            Case glyph2Add.Slotname.Contains("minorglyph2") : targetSlot = 1
                            Case glyph2Add.Slotname.Contains("minorglyph3") : targetSlot = 2
                            Case glyph2Add.Slotname.Contains("majorglyph1") : targetSlot = 0
                            Case glyph2Add.Slotname.Contains("majorglyph2") : targetSlot = 3
                            Case glyph2Add.Slotname.Contains("majorglyph3") : targetSlot = 5
                            Case glyph2Add.Slotname.Contains("primeglyph1") : targetSlot = 6
                            Case glyph2Add.Slotname.Contains("primeglyph2") : targetSlot = 7
                            Case glyph2Add.Slotname.Contains("primeglyph3") : targetSlot = 8
                            Case Else
                                LogAppend("Invalid slotname: " & glyph2Add.Slotname, "UpdateGlyphsHandler_CreateGlyph",
                                          False, True)
                        End Select
                        runSQLCommand_characters_string(
                            "UPDATE " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " SET `" &
                            GlobalVariables.sourceStructure.glyphs_glyph_col(0) & "` = '" &
                            GetGlyphIdByItemId(glyph2Add.Id, GlobalVariables.sourceExpansion).ToString() & "' WHERE `" &
                            GlobalVariables.targetStructure.glyphs_guid_col(0) & "` = '" & player.Guid.ToString() &
                            "' AND `" &
                            GlobalVariables.targetStructure.glyphs_spec_col(0) & "` = '" & glyph2Add.Spec.ToString() &
                            "'`" &
                            GlobalVariables.targetStructure.glyphs_slot_col(0) & "` = '" & targetSlot.ToString() & "'")
                    Catch ex As Exception
                        LogAppend("Exception occured during glyph creation: " & ex.ToString,
                                  "UpdateGlyphsHandler_CreateGlyph", False, True)
                    End Try
            End Select
        End Sub

        Private Sub DeleteGlyph(ByVal player As Character, ByVal glyph2Delete As Glyph)
            Select Case GlobalVariables.sourceCore
                Case "arcemu"
                    Try
                        Dim useGlyphCol As String
                        Select Case glyph2Delete.Spec
                            Case 0 : useGlyphCol = GlobalVariables.sourceStructure.char_glyphs1_col(0)
                            Case Else : useGlyphCol = GlobalVariables.sourceStructure.char_glyphs2_col(0)
                        End Select
                        Dim glyphString As String = runSQLCommand_characters_string(
                            "SELECT `" & useGlyphCol &
                            "` FROM `" & GlobalVariables.sourceStructure.character_tbl(0) &
                            "` WHERE `" & GlobalVariables.sourceStructure.char_guid_col(0) & "`='" &
                            player.Guid.ToString & "'", False)
                        If glyphString = "" Then
                            If GlobalVariables.sourceExpansion = 4 Then
                                glyphString = ",,,,,,,,,"
                            Else
                                glyphString = ",,,,,,"
                            End If
                        End If
                        Dim parts() As String = glyphString.Split(","c)
                        Dim baseInt As UInteger
                        Select Case glyph2Delete.Type
                            Case 1 : baseInt = 0
                            Case 2 : baseInt = 3
                            Case 3 : baseInt = 6
                        End Select
                        If glyph2Delete.Slotname.Contains("1") Then
                            parts(baseInt) = ""
                        ElseIf glyph2Delete.Slotname.Contains("2") Then
                            parts(baseInt + 1) = ""
                        Else
                            parts(baseInt + 2) = ""
                        End If
                        glyphString = String.Join(",", parts)
                        runSQLCommand_characters_string(
                            "UPDATE `" & GlobalVariables.sourceStructure.character_tbl(0) &
                            "` SET `" & useGlyphCol & "`='" & glyphString & "'" &
                            " WHERE `" & GlobalVariables.sourceStructure.char_guid_col(0) & "`='" & player.Guid.ToString &
                            "'", False)
                    Catch ex As Exception
                        LogAppend("Exception occured during glyph creation: " & ex.ToString,
                                  "UpdateGlyphsHandler_DeleteGlyph", False, True)
                    End Try
                Case "trinity"
                    Try
                        Dim targetCol As String = ""
                        Select Case True
                            Case glyph2Delete.Slotname.Contains("minorglyph1")
                                targetCol = GlobalVariables.sourceStructure.glyphs_glyph5_col(0)
                            Case glyph2Delete.Slotname.Contains("minorglyph2")
                                targetCol = GlobalVariables.sourceStructure.glyphs_glyph2_col(0)
                            Case glyph2Delete.Slotname.Contains("minorglyph3")
                                targetCol = GlobalVariables.sourceStructure.glyphs_glyph3_col(0)
                            Case glyph2Delete.Slotname.Contains("majorglyph1")
                                targetCol = GlobalVariables.sourceStructure.glyphs_glyph1_col(0)
                            Case glyph2Delete.Slotname.Contains("majorglyph2")
                                targetCol = GlobalVariables.sourceStructure.glyphs_glyph4_col(0)
                            Case glyph2Delete.Slotname.Contains("majorglyph3")
                                targetCol = GlobalVariables.sourceStructure.glyphs_glyph6_col(0)
                            Case glyph2Delete.Slotname.Contains("primeglyph1")
                                targetCol = GlobalVariables.sourceStructure.glyphs_glyph7_col(0)
                            Case glyph2Delete.Slotname.Contains("primeglyph2")
                                targetCol = GlobalVariables.sourceStructure.glyphs_glyph8_col(0)
                            Case glyph2Delete.Slotname.Contains("primeglyph3")
                                targetCol = GlobalVariables.sourceStructure.glyphs_glyph9_col(0)
                            Case Else
                                LogAppend("Invalid slotname: " & glyph2Delete.Slotname,
                                          "UpdateGlyphsHandler_DeleteGlyph", False, True)
                        End Select
                        runSQLCommand_characters_string(
                            "UPDATE `" & GlobalVariables.targetStructure.character_glyphs_tbl(0) & "` SET `" & targetCol &
                            "`='' WHERE `" &
                            GlobalVariables.targetStructure.glyphs_spec_col(0) & "`='" & glyph2Delete.Spec.ToString() &
                            "' AND `" &
                            GlobalVariables.targetStructure.glyphs_guid_col(0) & "`='" & player.Guid.ToString() & "'")
                    Catch ex As Exception
                        LogAppend("Exception occured during glyph deletion: " & ex.ToString,
                                  "UpdateGlyphsHandler_DeleteGlyph", False, True)
                    End Try
                Case "mangos"
                    Try
                        Dim targetSlot As Integer = 0
                        Select Case True
                            Case glyph2Delete.Slotname.Contains("minorglyph1") : targetSlot = 4
                            Case glyph2Delete.Slotname.Contains("minorglyph2") : targetSlot = 1
                            Case glyph2Delete.Slotname.Contains("minorglyph3") : targetSlot = 2
                            Case glyph2Delete.Slotname.Contains("majorglyph1") : targetSlot = 0
                            Case glyph2Delete.Slotname.Contains("majorglyph2") : targetSlot = 3
                            Case glyph2Delete.Slotname.Contains("majorglyph3") : targetSlot = 5
                            Case glyph2Delete.Slotname.Contains("primeglyph1") : targetSlot = 6
                            Case glyph2Delete.Slotname.Contains("primeglyph2") : targetSlot = 7
                            Case glyph2Delete.Slotname.Contains("primeglyph3") : targetSlot = 8
                            Case Else
                                LogAppend("Invalid slotname: " & glyph2Delete.Slotname,
                                          "UpdateGlyphsHandler_CreateGlyph",
                                          False, True)
                        End Select
                        runSQLCommand_characters_string(
                            "DELETE FROM " & GlobalVariables.targetStructure.character_glyphs_tbl(0) & " WHERE `" &
                            GlobalVariables.targetStructure.glyphs_guid_col(0) & "` = '" & player.Guid.ToString() &
                            "' AND `" &
                            GlobalVariables.targetStructure.glyphs_spec_col(0) & "` = '" & glyph2Delete.Spec.ToString() &
                            "'`" &
                            GlobalVariables.targetStructure.glyphs_slot_col(0) & "` = '" & targetSlot.ToString() & "'")
                    Catch ex As Exception
                        LogAppend("Exception occured during glyph deletion: " & ex.ToString,
                                  "UpdateGlyphsHandler_DeleteGlyph", False, True)
                    End Try
            End Select
        End Sub
    End Class
End Namespace