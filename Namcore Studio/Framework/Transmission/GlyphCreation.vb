Imports Namcore_Studio.EventLogging
Imports Namcore_Studio.CommandHandler
Imports Namcore_Studio.GlobalVariables
Imports Namcore_Studio.Basics
Imports Namcore_Studio.Conversions
Imports Namcore_Studio.SpellItem_Information
Public Class GlyphCreation
    Public Shared Sub SetCharacterGlyphs(ByVal setId As Integer, Optional charguid As Integer = 0)
        If charguid = 0 Then charguid = characterGUID
        LogAppend("Creating Glyphs for character: " & charguid.ToString() & " // setId is : " & setId.ToString(), "GlyphCreation_SetCharacterGlyphs", True)
        Select Case sourceCore
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
    Private Shared Sub createAtArcemu(ByVal characterguid As Integer, ByVal targetSetId As Integer)
        LogAppend("Creating at arcemu", "GlyphCreation_createAtArcemu", False)
        runSQLCommand_characters_string("DELETE glyphs1 FROM characters WHERE guid = '" & characterguid.ToString() & "'")
        runSQLCommand_characters_string("DELETE glyphs2 FROM characters WHERE guid = '" & characterguid.ToString() & "'")
        Dim glyphstring1 As String = "major1,minor1,minor2,major2,minor3,major3,"
        Dim glyphstring2 As String = glyphstring1
        glyphstring1 = glyphstring1.Replace("minor1", (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_minorglyph1", targetSetId))).ToString)
        glyphstring1 = glyphstring1.Replace("minor2", (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_minorglyph2", targetSetId))).ToString)
        glyphstring1 = glyphstring1.Replace("minor3", (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_minorglyph3", targetSetId))).ToString)
        glyphstring1 = glyphstring1.Replace("major1", (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_majorglyph1", targetSetId))).ToString)
        glyphstring1 = glyphstring1.Replace("major2", (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_majorglyph2", targetSetId))).ToString)
        glyphstring1 = glyphstring1.Replace("major3", (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_majorglyph3", targetSetId))).ToString)
        glyphstring2 = glyphstring2.Replace("minor1", (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_secminorglyph1", targetSetId))).ToString)
        glyphstring2 = glyphstring2.Replace("minor2", (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_secminorglyph2", targetSetId))).ToString)
        glyphstring2 = glyphstring2.Replace("minor3", (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_secminorglyph3", targetSetId))).ToString)
        glyphstring2 = glyphstring2.Replace("major1", (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_secmajorglyph1", targetSetId))).ToString)
        glyphstring2 = glyphstring2.Replace("major2", (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_secmajorglyph2", targetSetId))).ToString)
        glyphstring2 = glyphstring2.Replace("major3", (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_secmajorglyph3", targetSetId))).ToString)
        runSQLCommand_characters_string("UPDATE characters SET glyphs1='" & glyphstring1 & "' WHERE guid='" & characterguid.ToString() & "'")
        runSQLCommand_characters_string("UPDATE characters SET glyphs2='" & glyphstring2 & "' WHERE guid='" & characterguid.ToString() & "'")
    End Sub
    Private Shared Sub createAtTrinity(ByVal characterguid As Integer, ByVal targetSetId As Integer)
        LogAppend("Creating at Trinity", "GlyphCreation_createAtTrinity", False)
        runSQLCommand_characters_string("DELETE FROM character_glyphs WHERE guid = '" & characterguid.ToString() & "' AND spec='0'")
        runSQLCommand_characters_string("DELETE FROM character_glyphs WHERE guid = '" & characterguid.ToString() & "' AND spec='1'")
        If expansion = 4 Then
            runSQLCommand_characters_string("INSERT INTO character_glyphs ( guid, spec, glyph1, glyph2, glyph3, glyph4, glyph5, glyph6, glyph7, glyph8, glyph9 ) VALUES ( '" & characterguid.ToString() & "', '0', '" &
                                                    (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_majorglyph1", targetSetId))).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_minorglyph2", targetSetId))).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_minorglyph3", targetSetId))).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_majorglyph2", targetSetId))).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_minorglyph1", targetSetId))).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_majorglyph3", targetSetId))).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_primeglyph1", targetSetId))).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_primeglyph2", targetSetId))).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_primeglyph3", targetSetId))).ToString & "' )")
            runSQLCommand_characters_string("INSERT INTO character_glyphs ( guid, spec, glyph1, glyph2, glyph3, glyph4, glyph5, glyph6, glyph7, glyph8, glyph9 ) VALUES ( '" & characterguid.ToString() & "', '1', '" &
                                                    (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_secmajorglyph1", targetSetId))).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_secminorglyph2", targetSetId))).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_secminorglyph3", targetSetId))).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_secmajorglyph2", targetSetId))).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_secminorglyph1", targetSetId))).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_secmajorglyph3", targetSetId))).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_secprimeglyph1", targetSetId))).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_secprimeglyph2", targetSetId))).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_secprimeglyph3", targetSetId))).ToString & "' )")
        ElseIf expansion = 3 Then
            runSQLCommand_characters_string("INSERT INTO character_glyphs ( guid, spec, glyph1, glyph2, glyph3, glyph4, glyph5, glyph6, glyph7, glyph8, glyph9 ) VALUES ( '" & characterguid.ToString() & "', '0', '" &
                                                    (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_majorglyph1", targetSetId))).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_minorglyph2", targetSetId))).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_minorglyph3", targetSetId))).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_majorglyph2", targetSetId))).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_minorglyph1", targetSetId))).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_majorglyph3", targetSetId))).ToString & "' )")
            runSQLCommand_characters_string("INSERT INTO character_glyphs ( guid, spec, glyph1, glyph2, glyph3, glyph4, glyph5, glyph6, glyph7, glyph8, glyph9 ) VALUES ( '" & characterguid.ToString() & "', '1', '" &
                                                    (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_secmajorglyph1", targetSetId))).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_secminorglyph2", targetSetId))).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_secminorglyph3", targetSetId))).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_secmajorglyph2", targetSetId))).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_secminorglyph1", targetSetId))).ToString & "', " &
                                                "'" & (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_secmajorglyph3", targetSetId))).ToString & "' )")
        End If
    End Sub
    Private Shared Sub createAtMangos(ByVal characterguid As Integer, ByVal targetSetId As Integer)
        LogAppend("Creating at Mangos", "GlyphCreation_createAtMangos", False)
        runSQLCommand_characters_string("DELETE FROM character_glyphs WHERE guid = '" & characterguid.ToString() & "'")
        runSQLCommand_characters_string("INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & characterguid.ToString() & "', '0', '4', '" &
                                            (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_minorglyph1", targetSetId))).ToString() & "' )")
        runSQLCommand_characters_string("INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & characterguid.ToString() & "', '0', '1', '" &
                                        (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_minorglyph2", targetSetId))).ToString() & "' )")
        runSQLCommand_characters_string("INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & characterguid.ToString() & "', '0', '2', '" &
                                        (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_minorglyph3", targetSetId))).ToString() & "' )")
        runSQLCommand_characters_string("INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & characterguid.ToString() & "', '0', '0', '" &
                                        (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_majorglyph1", targetSetId))).ToString() & "' )")
        runSQLCommand_characters_string("INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & characterguid.ToString() & "', '0', '3', '" &
                                        (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_majorglyph2", targetSetId))).ToString() & "' )")
        runSQLCommand_characters_string("INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & characterguid.ToString() & "', '0', '5', '" &
                                        (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_majorglyph3", targetSetId))).ToString() & "' )")

        runSQLCommand_characters_string("INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & characterguid.ToString() & "', '1', '4', '" &
                                           (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_secminorglyph1", targetSetId))).ToString() & "' )")
        runSQLCommand_characters_string("INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & characterguid.ToString() & "', '1', '1', '" &
                                        (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_secminorglyph2", targetSetId))).ToString() & "' )")
        runSQLCommand_characters_string("INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & characterguid.ToString() & "', '1', '2', '" &
                                        (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_secminorglyph3", targetSetId))).ToString() & "' )")
        runSQLCommand_characters_string("INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & characterguid.ToString() & "', '1', '0', '" &
                                        (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_secmajorglyph1", targetSetId))).ToString() & "' )")
        runSQLCommand_characters_string("INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & characterguid.ToString() & "', '1', '3', '" &
                                        (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_secmajorglyph2", targetSetId))).ToString() & "' )")
        runSQLCommand_characters_string("INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & characterguid.ToString() & "', '1', '5', '" &
                                        (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_secmajorglyph3", targetSetId))).ToString() & "' )")
        If expansion = 4 Then
            runSQLCommand_characters_string("INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & characterguid.ToString() & "', '0', '6', '" &
                                            (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_primeglyph1", targetSetId))).ToString() & "' )")
            runSQLCommand_characters_string("INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & characterguid.ToString() & "', '0', '7', '" &
                                            (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_primeglyph2", targetSetId))).ToString() & "' )")
            runSQLCommand_characters_string("INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & characterguid.ToString() & "', '0', '8', '" &
                                            (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_primeglyph3", targetSetId))).ToString() & "' )")

            runSQLCommand_characters_string("INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & characterguid.ToString() & "', '1', '6', '" &
                                            (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_secprimeglyph1", targetSetId))).ToString() & "' )")
            runSQLCommand_characters_string("INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & characterguid.ToString() & "', '1', '7', '" &
                                            (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_secprimeglyph2", targetSetId))).ToString() & "' )")
            runSQLCommand_characters_string("INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & characterguid.ToString() & "', '1', '8', '" &
                                            (GetGlyphIdByItemId(GetTemporaryCharacterInformation("@character_secprimeglyph3", targetSetId))).ToString() & "' )")
        End If
    End Sub
End Class
