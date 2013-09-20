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
'*      /Filename:      UpdateGlyphsHandler
'*      /Description:   Handles character glyph update requests
'++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.Database
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Module

Namespace Framework.Transmission.Update
    Public Class UpdateGlyphsHandler
        Public Sub UpdateGlyphs(ByVal player As Character, ByVal modPlayer As Character)
            LogAppend("Updating character glyphs", "UpdateGlyphsHandler_UpdateGlyphs", True)
            If GlobalVariables.sourceExpansion < 3 Then
                '// Cannot create glyphs in pre WotLK db
                LogAppend("Cannot create glyphs in pre WotLK db!", "UpdateGlyphsHandler_UpdateGlyphs", True, True)
                Exit Sub
            End If
            '// Any new glyphs?
            For Each gly As Glyph In modPlayer.PlayerGlyphs
                Dim result As Glyph = player.PlayerGlyphs.Find(Function(glyph) glyph.Id = gly.Id)
                If result Is Nothing Then CreateGlyph(player, gly)
            Next
            '// Any deleted glyphs?
            For Each gly As Glyph In player.PlayerGlyphs
                Dim result As Glyph = modPlayer.PlayerGlyphs.Find(Function(glyph) glyph.Id = gly.Id)
                If result Is Nothing Then DeleteGlyph(player, gly)
            Next
        End Sub

        Private Sub CreateGlyph(ByVal player As Character, ByVal glyph2Add As Glyph)
            Select Case GlobalVariables.sourceCore
                Case "trinity"
                    Try
                        If glyph2Add.Type = 3 Then
                            '// Cannot create primary glyphs in pre Cataclysm db
                            LogAppend("Cannot create primary glyphs in pre Cataclysm db!", "UpdateGlyphsHandler_CreateGlyph", True, True)
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
                            "` WHERE `" & GlobalVariables.sourceStructure.char_guid_col(0) & "`='" & player.Guid.ToString & "'", False)
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
                            parts(baseInt) = glyph2Add.Id
                        ElseIf glyph2Add.Slotname.Contains("2") Then
                            parts(baseInt + 1) = glyph2Add.Id
                        Else
                            parts(baseInt + 2) = glyph2Add.Id
                        End If
                        glyphString = String.Join(",", parts)
                        runSQLCommand_characters_string(
                           "UPDATE `" & GlobalVariables.sourceStructure.character_tbl(0) &
                           "` SET `" & useGlyphCol & "`='" & glyphString & "'" &
                           " WHERE `" & GlobalVariables.sourceStructure.char_guid_col(0) & "`='" & player.Guid.ToString & "'", False)
                    Catch ex As Exception
                        LogAppend("Exception occured during glyph creation: " & ex.ToString, "UpdateGlyphsHandler_CreateGlyph", False, True)
                    End Try
            End Select
        End Sub

        Private Sub DeleteGlyph(ByVal player As Character, ByVal glyph2Delete As Glyph)
            Select Case GlobalVariables.sourceCore
                Case "trinity"
                    Try
                        Dim useGlyphCol As String
                        Select Case glyph2Delete.Spec
                            Case 0 : useGlyphCol = GlobalVariables.sourceStructure.char_glyphs1_col(0)
                            Case Else : useGlyphCol = GlobalVariables.sourceStructure.char_glyphs2_col(0)
                        End Select
                        Dim glyphString As String = runSQLCommand_characters_string(
                            "SELECT `" & useGlyphCol &
                            "` FROM `" & GlobalVariables.sourceStructure.character_tbl(0) &
                            "` WHERE `" & GlobalVariables.sourceStructure.char_guid_col(0) & "`='" & player.Guid.ToString & "'", False)
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
                           " WHERE `" & GlobalVariables.sourceStructure.char_guid_col(0) & "`='" & player.Guid.ToString & "'", False)
                    Catch ex As Exception
                        LogAppend("Exception occured during glyph creation: " & ex.ToString, "UpdateGlyphsHandler_DeleteGlyph", False, True)
                    End Try
            End Select
        End Sub
    End Class
End Namespace