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
'*      /Filename:      CharacterGlyphsHandler
'*      /Description:   Contains functions for extracting information about the equipped 
'*                      primary and secondary glyphs of a specific character
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Imports Namcore_Studio.EventLogging
Imports Namcore_Studio.Basics
Imports Namcore_Studio.GlobalVariables
Imports Namcore_Studio.CommandHandler
Imports Namcore_Studio.Conversions
Imports Namcore_Studio.SpellItem_Information
Public Class CharacterGlyphsHandler

    Public Shared Sub GetCharacterGlyphs(ByVal charguid As Integer, ByVal setId As Integer, ByVal accountId As Integer)
        LogAppend("Loading character Glyphs for charguid: " & charguid & " and setId: " & setId, "CharacterGlyphsHandler_GetCharacterGlyphs", True)
        Select Case sourceCore
            Case "arcemu"
                loadAtArcemu(charguid, setId, accountId)
            Case "trinity"
                loadAtTrinity(charguid, setId, accountId)
            Case "trinitytbc"
                loadAtTrinityTBC(charguid, setId, accountId)
            Case "mangos"
                loadAtMangos(charguid, setId, accountId)
            Case Else

        End Select

    End Sub
    Private Shared Sub loadAtArcemu(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)
        LogAppend("Loading character Glyphs @loadAtArcemu", "CharacterGlyphsHandler_loadAtArcemu", False)
        Dim glyphstring As String = runSQLCommand_characters_string("SELECT " & sourceStructure.char_glyphs1_col(0) & " from " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) &
                                                                    "='" & charguid.ToString & "'")
        Dim secglyphstring As String = runSQLCommand_characters_string("SELECT " & sourceStructure.char_glyphs2_col(0) & " from " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) &
                                                                       "='" & charguid.ToString & "'")
        'Spec 0
        Dim player As Character = GetCharacterSetBySetId(tar_setId)
        Try
            Dim parts() As String = glyphstring.Split(","c)
            Dim prevglyphid As Integer = TryInt(parts(0))
            Dim tmpGlyph As New Glyph
            If prevglyphid > 1 Then
                tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                tmpGlyph.slotname = "majorglyph1"
                tmpGlyph.spec = 0
                tmpGlyph.type = 2
                AddCharacterGlyph(player, tmpGlyph)
                SetCharacterSet(tar_setId, player)
            End If
        Catch ex As Exception
            LogAppend("Error while loading majorglyph1! -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterGlyphsHandler_loadAtArcemu", False, True)
        End Try
        Try
            Dim parts() As String = glyphstring.Split(","c)
            Dim prevglyphid As Integer = TryInt(parts(3))
            Dim tmpGlyph As New Glyph
            If prevglyphid > 1 Then
                tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                tmpGlyph.slotname = "majorglyph2"
                tmpGlyph.spec = 0
                tmpGlyph.type = 2
                AddCharacterGlyph(player, tmpGlyph)
                SetCharacterSet(tar_setId, player)
            End If
        Catch ex As Exception
            LogAppend("Error while loading majorglyph2! -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterGlyphsHandler_loadAtArcemu", False, True)
        End Try
        Try
            Dim parts() As String = glyphstring.Split(","c)
            Dim prevglyphid As Integer = TryInt(parts(5))
            Dim tmpGlyph As New Glyph
            If prevglyphid > 1 Then
                tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                tmpGlyph.slotname = "majorglyph3"
                tmpGlyph.spec = 0
                tmpGlyph.type = 2
                AddCharacterGlyph(player, tmpGlyph)
                SetCharacterSet(tar_setId, player)
            End If
        Catch ex As Exception
            LogAppend("Error while loading majorglyph3! -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterGlyphsHandler_loadAtArcemu", False, True)
        End Try
        Try
            Dim parts() As String = glyphstring.Split(","c)
            Dim prevglyphid As Integer = TryInt(parts(1))
            Dim tmpGlyph As New Glyph
            If prevglyphid > 1 Then
                tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                tmpGlyph.slotname = "minorglyph1"
                tmpGlyph.spec = 0
                tmpGlyph.type = 1
                AddCharacterGlyph(player, tmpGlyph)
                SetCharacterSet(tar_setId, player)
            End If
        Catch ex As Exception
            LogAppend("Error while loading minorglyph1! -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterGlyphsHandler_loadAtArcemu", False, True)
        End Try
        Try
            Dim parts() As String = glyphstring.Split(","c)
            Dim prevglyphid As Integer = TryInt(parts(2))
            Dim tmpGlyph As New Glyph
            If prevglyphid > 1 Then
                tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                tmpGlyph.slotname = "minorglyph2"
                tmpGlyph.spec = 0
                tmpGlyph.type = 1
                AddCharacterGlyph(player, tmpGlyph)
                SetCharacterSet(tar_setId, player)
            End If
        Catch ex As Exception
            LogAppend("Error while loading minorglyph2! -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterGlyphsHandler_loadAtArcemu", False, True)
        End Try
        Try
            Dim parts() As String = glyphstring.Split(","c)
            Dim prevglyphid As Integer = TryInt(parts(4))
            Dim tmpGlyph As New Glyph
            If prevglyphid > 1 Then
                tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                tmpGlyph.slotname = "minorglyph3"
                tmpGlyph.spec = 0
                tmpGlyph.type = 1
                AddCharacterGlyph(player, tmpGlyph)
                SetCharacterSet(tar_setId, player)
            End If
        Catch ex As Exception
            LogAppend("Error while loading minorglyph3! -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterGlyphsHandler_loadAtArcemu", False, True)
        End Try
        'Spec 1
        Try
            Dim parts() As String = secglyphstring.Split(","c)
            Dim prevglyphid As Integer = TryInt(parts(0))
            Dim tmpGlyph As New Glyph
            If prevglyphid > 1 Then
                tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                tmpGlyph.slotname = "secmajorglyph1"
                tmpGlyph.spec = 1
                tmpGlyph.type = 2
                AddCharacterGlyph(player, tmpGlyph)
                SetCharacterSet(tar_setId, player)
            End If
        Catch ex As Exception
            LogAppend("Error while loading secmajorglyph1! -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterGlyphsHandler_loadAtArcemu", False, True)
        End Try
        Try
            Dim parts() As String = secglyphstring.Split(","c)
            Dim prevglyphid As Integer = TryInt(parts(3))
            Dim tmpGlyph As New Glyph
            If prevglyphid > 1 Then
                tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                tmpGlyph.slotname = "secmajorglyph2"
                tmpGlyph.spec = 1
                tmpGlyph.type = 2
                AddCharacterGlyph(player, tmpGlyph)
                SetCharacterSet(tar_setId, player)
            End If
        Catch ex As Exception
            LogAppend("Error while loading secmajorglyph2! -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterGlyphsHandler_loadAtArcemu", False, True)
        End Try
        Try
            Dim parts() As String = secglyphstring.Split(","c)
            Dim prevglyphid As Integer = TryInt(parts(5))
            Dim tmpGlyph As New Glyph
            If prevglyphid > 1 Then
                tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                tmpGlyph.slotname = "secmajorglyph3"
                tmpGlyph.spec = 1
                tmpGlyph.type = 2
                AddCharacterGlyph(player, tmpGlyph)
                SetCharacterSet(tar_setId, player)
            End If
        Catch ex As Exception
            LogAppend("Error while loading secmajorglyph3! -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterGlyphsHandler_loadAtArcemu", False, True)
        End Try
        Try
            Dim parts() As String = secglyphstring.Split(","c)
            Dim prevglyphid As Integer = TryInt(parts(1))
            Dim tmpGlyph As New Glyph
            If prevglyphid > 1 Then
                tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                tmpGlyph.slotname = "secminorglyph1"
                tmpGlyph.spec = 1
                tmpGlyph.type = 1
                AddCharacterGlyph(player, tmpGlyph)
                SetCharacterSet(tar_setId, player)
            End If
        Catch ex As Exception
            LogAppend("Error while loading secminorglyph1! -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterGlyphsHandler_loadAtArcemu", False, True)
        End Try
        Try
            Dim parts() As String = secglyphstring.Split(","c)
            Dim prevglyphid As Integer = TryInt(parts(2))
            Dim tmpGlyph As New Glyph
            If prevglyphid > 1 Then
                tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                tmpGlyph.slotname = "secminorglyph2"
                tmpGlyph.spec = 1
                tmpGlyph.type = 1
                AddCharacterGlyph(player, tmpGlyph)
                SetCharacterSet(tar_setId, player)
            End If
        Catch ex As Exception
            LogAppend("Error while loading secminorglyph2! -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterGlyphsHandler_loadAtArcemu", False, True)
        End Try
        Try
            Dim parts() As String = secglyphstring.Split(","c)
            Dim prevglyphid As Integer = TryInt(parts(4))
            Dim tmpGlyph As New Glyph
            If prevglyphid > 1 Then
                tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                tmpGlyph.slotname = "secminorglyph3"
                tmpGlyph.spec = 1
                tmpGlyph.type = 1
                AddCharacterGlyph(player, tmpGlyph)
                SetCharacterSet(tar_setId, player)
            End If
        Catch ex As Exception
            LogAppend("Error while loading secminorglyph3! -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterGlyphsHandler_loadAtArcemu", False, True)
        End Try
    End Sub
    Private Shared Sub loadAtTrinity(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)
        LogAppend("Loading character Glyphs @loadAtTrinity", "CharacterGlyphsHandler_loadAtTrinity", False)
        Dim tempdt As New DataTable
        Dim tempdtsec As New DataTable
        If expansion = 3 Then
            tempdt = ReturnDataTable("SELECT " & sourceStructure.glyphs_glyph1_col(0) & ", " & sourceStructure.glyphs_glyph2_col(0) & ", " & sourceStructure.glyphs_glyph3_col(0) &
                                     ", " & sourceStructure.glyphs_glyph4_col(0) & ", " & sourceStructure.glyphs_glyph5_col(0) & ", " & sourceStructure.glyphs_glyph6_col(0) &
                                     " FROM " & sourceStructure.character_glyphs_tbl(0) & " WHERE " & sourceStructure.glyphs_guid_col(0) & "='" & charguid.ToString & "' AND " & sourceStructure.glyphs_spec_col(0) & "='0'")
            tempdtsec = ReturnDataTable("SELECT " & sourceStructure.glyphs_glyph1_col(0) & ", " & sourceStructure.glyphs_glyph2_col(0) & ", " & sourceStructure.glyphs_glyph3_col(0) &
                                     ", " & sourceStructure.glyphs_glyph4_col(0) & ", " & sourceStructure.glyphs_glyph5_col(0) & ", " & sourceStructure.glyphs_glyph6_col(0) &
                                     " FROM " & sourceStructure.character_glyphs_tbl(0) & " WHERE " & sourceStructure.glyphs_guid_col(0) & "='" & charguid.ToString & "' AND " & sourceStructure.glyphs_spec_col(0) & "='1'")
        Else
            tempdt = ReturnDataTable("SELECT " & sourceStructure.glyphs_glyph1_col(0) & ", " & sourceStructure.glyphs_glyph2_col(0) & ", " & sourceStructure.glyphs_glyph3_col(0) &
                                     ", " & sourceStructure.glyphs_glyph4_col(0) & ", " & sourceStructure.glyphs_glyph5_col(0) & ", " & sourceStructure.glyphs_glyph6_col(0) &
                                     ", " & sourceStructure.glyphs_glyph7_col(0) & ", " & sourceStructure.glyphs_glyph8_col(0) & ", " & sourceStructure.glyphs_glyph9_col(0) &
                                     " FROM " & sourceStructure.character_glyphs_tbl(0) & " WHERE " & sourceStructure.glyphs_guid_col(0) & "='" & charguid.ToString & "' AND " & sourceStructure.glyphs_spec_col(0) & "='0'")
            tempdtsec = ReturnDataTable("SELECT " & sourceStructure.glyphs_glyph1_col(0) & ", " & sourceStructure.glyphs_glyph2_col(0) & ", " & sourceStructure.glyphs_glyph3_col(0) &
                                     ", " & sourceStructure.glyphs_glyph4_col(0) & ", " & sourceStructure.glyphs_glyph5_col(0) & ", " & sourceStructure.glyphs_glyph6_col(0) &
                                     ", " & sourceStructure.glyphs_glyph7_col(0) & ", " & sourceStructure.glyphs_glyph8_col(0) & ", " & sourceStructure.glyphs_glyph9_col(0) &
                                     " FROM " & sourceStructure.character_glyphs_tbl(0) & " WHERE " & sourceStructure.glyphs_guid_col(0) & "='" & charguid.ToString & "' AND " & sourceStructure.glyphs_spec_col(0) & "='1'")
        End If
        Dim prevglyphid As Integer
        Dim player As Character = GetCharacterSetBySetId(tar_setId)
        Dim lastcount As Integer = tempdt.Rows.Count
        If Not lastcount = 0 Then
            prevglyphid = TryInt((tempdt.Rows(0).Item(0)).ToString)
            If prevglyphid > 1 Then
                Dim tmpGlyph As New Glyph
                tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                tmpGlyph.slotname = "majorglyph1"
                tmpGlyph.spec = 0
                tmpGlyph.type = 2
                AddCharacterGlyph(player, tmpGlyph)
                SetCharacterSet(tar_setId, player)
            End If
            prevglyphid = TryInt((tempdt.Rows(0).Item(1)).ToString)
            If prevglyphid > 1 Then
                Dim tmpGlyph As New Glyph
                tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                tmpGlyph.slotname = "minorglyph1"
                tmpGlyph.spec = 0
                tmpGlyph.type = 1
                AddCharacterGlyph(player, tmpGlyph)
                SetCharacterSet(tar_setId, player)
            End If
            prevglyphid = TryInt((tempdt.Rows(0).Item(2)).ToString)
            If prevglyphid > 1 Then
                Dim tmpGlyph As New Glyph
                tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                tmpGlyph.slotname = "minorglyph2"
                tmpGlyph.spec = 0
                tmpGlyph.type = 1
                AddCharacterGlyph(player, tmpGlyph)
                SetCharacterSet(tar_setId, player)
            End If
            prevglyphid = TryInt((tempdt.Rows(0).Item(3)).ToString)
            If prevglyphid > 1 Then
                Dim tmpGlyph As New Glyph
                tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                tmpGlyph.slotname = "majorglyph2"
                tmpGlyph.spec = 0
                tmpGlyph.type = 2
                AddCharacterGlyph(player, tmpGlyph)
                SetCharacterSet(tar_setId, player)
            End If
            prevglyphid = TryInt((tempdt.Rows(0).Item(4)).ToString)
            If prevglyphid > 1 Then
                Dim tmpGlyph As New Glyph
                tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                tmpGlyph.slotname = "minorglyph3"
                tmpGlyph.spec = 0
                tmpGlyph.type = 1
                AddCharacterGlyph(player, tmpGlyph)
                SetCharacterSet(tar_setId, player)
            End If
            prevglyphid = TryInt((tempdt.Rows(0).Item(5)).ToString)
            If prevglyphid > 1 Then
                Dim tmpGlyph As New Glyph
                tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                tmpGlyph.slotname = "majorglyph3"
                tmpGlyph.spec = 0
                tmpGlyph.type = 2
                AddCharacterGlyph(player, tmpGlyph)
                SetCharacterSet(tar_setId, player)
            End If
            If expansion = 4 Then
                prevglyphid = TryInt((tempdt.Rows(0).Item(6)).ToString)
                If prevglyphid > 1 Then
                    Dim tmpGlyph As New Glyph
                    tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                    tmpGlyph.slotname = "primeglyph1"
                    tmpGlyph.spec = 0
                    tmpGlyph.type = 3
                    AddCharacterGlyph(player, tmpGlyph)
                    SetCharacterSet(tar_setId, player)
                End If
                prevglyphid = TryInt((tempdt.Rows(0).Item(7)).ToString)
                If prevglyphid > 1 Then
                    Dim tmpGlyph As New Glyph
                    tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                    tmpGlyph.slotname = "primeglyph2"
                    tmpGlyph.spec = 0
                    tmpGlyph.type = 3
                    AddCharacterGlyph(player, tmpGlyph)
                    SetCharacterSet(tar_setId, player)
                End If
                prevglyphid = TryInt((tempdt.Rows(0).Item(8)).ToString)
                If prevglyphid > 1 Then
                    Dim tmpGlyph As New Glyph
                    tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                    tmpGlyph.slotname = "primeglyph3"
                    tmpGlyph.spec = 0
                    tmpGlyph.type = 3
                    AddCharacterGlyph(player, tmpGlyph)
                    SetCharacterSet(tar_setId, player)
                End If
            End If
        Else
            LogAppend("No Glyphs found (spec 0)!", "CharacterGlyphsHandler_loadAtTrinity", True)
        End If
        lastcount = tempdtsec.Rows.Count
        If Not lastcount = 0 Then
            prevglyphid = TryInt((tempdtsec.Rows(0).Item(0)).ToString)
            If prevglyphid > 1 Then
                Dim tmpGlyph As New Glyph
                tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                tmpGlyph.slotname = "secmajorglyph1"
                tmpGlyph.spec = 1
                tmpGlyph.type = 2
                AddCharacterGlyph(player, tmpGlyph)
                SetCharacterSet(tar_setId, player)
            End If
            prevglyphid = TryInt((tempdtsec.Rows(0).Item(1)).ToString)
            If prevglyphid > 1 Then
                Dim tmpGlyph As New Glyph
                tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                tmpGlyph.slotname = "secminorglyph1"
                tmpGlyph.spec = 1
                tmpGlyph.type = 1
                AddCharacterGlyph(player, tmpGlyph)
                SetCharacterSet(tar_setId, player)
            End If
            prevglyphid = TryInt((tempdtsec.Rows(0).Item(2)).ToString)
            If prevglyphid > 1 Then
                Dim tmpGlyph As New Glyph
                tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                tmpGlyph.slotname = "secminorglyph2"
                tmpGlyph.spec = 1
                tmpGlyph.type = 1
                AddCharacterGlyph(player, tmpGlyph)
                SetCharacterSet(tar_setId, player)
            End If
            prevglyphid = TryInt((tempdtsec.Rows(0).Item(3)).ToString)
            If prevglyphid > 1 Then
                Dim tmpGlyph As New Glyph
                tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                tmpGlyph.slotname = "secmajorglyph2"
                tmpGlyph.spec = 1
                tmpGlyph.type = 2
                AddCharacterGlyph(player, tmpGlyph)
                SetCharacterSet(tar_setId, player)
            End If
            prevglyphid = TryInt((tempdtsec.Rows(0).Item(4)).ToString)
            If prevglyphid > 1 Then
                Dim tmpGlyph As New Glyph
                tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                tmpGlyph.slotname = "secminorglyph3"
                tmpGlyph.spec = 1
                tmpGlyph.type = 1
                AddCharacterGlyph(player, tmpGlyph)
                SetCharacterSet(tar_setId, player)
            End If
            prevglyphid = TryInt((tempdtsec.Rows(0).Item(5)).ToString)
            If prevglyphid > 1 Then
                Dim tmpGlyph As New Glyph
                tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                tmpGlyph.slotname = "secmajorglyph3"
                tmpGlyph.spec = 1
                tmpGlyph.type = 2
                AddCharacterGlyph(player, tmpGlyph)
                SetCharacterSet(tar_setId, player)
            End If
            If expansion = 4 Then
                prevglyphid = TryInt((tempdtsec.Rows(0).Item(6)).ToString)
                If prevglyphid > 1 Then
                    Dim tmpGlyph As New Glyph
                    tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                    tmpGlyph.slotname = "secprimeglyph1"
                    tmpGlyph.spec = 1
                    tmpGlyph.type = 3
                    AddCharacterGlyph(player, tmpGlyph)
                    SetCharacterSet(tar_setId, player)
                End If
                prevglyphid = TryInt((tempdtsec.Rows(0).Item(7)).ToString)
                If prevglyphid > 1 Then
                    Dim tmpGlyph As New Glyph
                    tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                    tmpGlyph.slotname = "secprimeglyph2"
                    tmpGlyph.spec = 1
                    tmpGlyph.type = 3
                    AddCharacterGlyph(player, tmpGlyph)
                    SetCharacterSet(tar_setId, player)
                End If
                prevglyphid = TryInt((tempdtsec.Rows(0).Item(8)).ToString)
                If prevglyphid > 1 Then
                    Dim tmpGlyph As New Glyph
                    tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                    tmpGlyph.slotname = "secprimeglyph3"
                    tmpGlyph.spec = 1
                    tmpGlyph.type = 3
                    AddCharacterGlyph(player, tmpGlyph)
                    SetCharacterSet(tar_setId, player)
                End If
            End If
        Else
            LogAppend("No Glyphs found (spec 1)!", "CharacterGlyphsHandler_loadAtTrinity", True)
        End If
    End Sub
    Private Shared Sub loadAtTrinityTBC(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)

    End Sub
    Private Shared Sub loadAtMangos(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)
        LogAppend("Loading character Glyphs @loadAtMangos", "CharacterGlyphsHandler_loadAtMangos", False)
        Dim tempdt As DataTable = ReturnDataTable("SELECT " & sourceStructure.glyphs_glyph_col(0) & ", " & sourceStructure.glyphs_slot_col(0) & ", " & sourceStructure.glyphs_spec_col(0) &
                                                  " FROM " & sourceStructure.character_glyphs_tbl(0) & " WHERE " & sourceStructure.glyphs_guid_col(0) & "='" & charguid.ToString & "'")
        Dim prevglyphid As Integer
        Dim slot As Integer
        Dim spec As Integer
        Dim resultquantity As Integer = tempdt.Rows.Count
        Dim proccounter As Integer = 0
        Dim player As Character = GetCharacterSetBySetId(tar_setId)
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
                                    tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                                    tmpGlyph.slotname = "majorglyph1"
                                    tmpGlyph.spec = 0
                                    tmpGlyph.type = 2
                                    AddCharacterGlyph(player, tmpGlyph)
                                    SetCharacterSet(tar_setId, player)
                                Case 1
                                    Dim tmpGlyph As New Glyph
                                    tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                                    tmpGlyph.slotname = "minorglyph2"
                                    tmpGlyph.spec = 0
                                    tmpGlyph.type = 1
                                    AddCharacterGlyph(player, tmpGlyph)
                                    SetCharacterSet(tar_setId, player)
                                Case 2
                                    Dim tmpGlyph As New Glyph
                                    tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                                    tmpGlyph.slotname = "minorglyph3"
                                    tmpGlyph.spec = 0
                                    tmpGlyph.type = 1
                                    AddCharacterGlyph(player, tmpGlyph)
                                    SetCharacterSet(tar_setId, player)
                                Case 3
                                    Dim tmpGlyph As New Glyph
                                    tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                                    tmpGlyph.slotname = "majorglyph2"
                                    tmpGlyph.spec = 0
                                    tmpGlyph.type = 2
                                    AddCharacterGlyph(player, tmpGlyph)
                                    SetCharacterSet(tar_setId, player)
                                Case 4
                                    Dim tmpGlyph As New Glyph
                                    tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                                    tmpGlyph.slotname = "minorglyph1"
                                    tmpGlyph.spec = 0
                                    tmpGlyph.type = 1
                                    AddCharacterGlyph(player, tmpGlyph)
                                    SetCharacterSet(tar_setId, player)
                                Case 5
                                    Dim tmpGlyph As New Glyph
                                    tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                                    tmpGlyph.slotname = "majorglyph3"
                                    tmpGlyph.spec = 0
                                    tmpGlyph.type = 2
                                    AddCharacterGlyph(player, tmpGlyph)
                                    SetCharacterSet(tar_setId, player)
                                Case 6
                                    Dim tmpGlyph As New Glyph
                                    tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                                    tmpGlyph.slotname = "primeglyph1"
                                    tmpGlyph.spec = 0
                                    tmpGlyph.type = 3
                                    AddCharacterGlyph(player, tmpGlyph)
                                    SetCharacterSet(tar_setId, player)
                                Case 7
                                    Dim tmpGlyph As New Glyph
                                    tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                                    tmpGlyph.slotname = "primeglyph2"
                                    tmpGlyph.spec = 0
                                    tmpGlyph.type = 3
                                    AddCharacterGlyph(player, tmpGlyph)
                                    SetCharacterSet(tar_setId, player)
                                Case 8
                                    Dim tmpGlyph As New Glyph
                                    tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                                    tmpGlyph.slotname = "primeglyph3"
                                    tmpGlyph.spec = 0
                                    tmpGlyph.type = 3
                                    AddCharacterGlyph(player, tmpGlyph)
                                    SetCharacterSet(tar_setId, player)
                                Case Else : End Select
                        Case 1
                            Select Case slot
                                Case 0
                                    Dim tmpGlyph As New Glyph
                                    tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                                    tmpGlyph.slotname = "secmajorglyph1"
                                    tmpGlyph.spec = 1
                                    tmpGlyph.type = 2
                                    AddCharacterGlyph(player, tmpGlyph)
                                    SetCharacterSet(tar_setId, player)
                                Case 1
                                    Dim tmpGlyph As New Glyph
                                    tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                                    tmpGlyph.slotname = "secminorglyph2"
                                    tmpGlyph.spec = 1
                                    tmpGlyph.type = 1
                                    AddCharacterGlyph(player, tmpGlyph)
                                    SetCharacterSet(tar_setId, player)
                                Case 2
                                    Dim tmpGlyph As New Glyph
                                    tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                                    tmpGlyph.slotname = "secminorglyph3"
                                    tmpGlyph.spec = 1
                                    tmpGlyph.type = 1
                                    AddCharacterGlyph(player, tmpGlyph)
                                    SetCharacterSet(tar_setId, player)
                                Case 3
                                    Dim tmpGlyph As New Glyph
                                    tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                                    tmpGlyph.slotname = "secmajorglyph2"
                                    tmpGlyph.spec = 1
                                    tmpGlyph.type = 2
                                    AddCharacterGlyph(player, tmpGlyph)
                                    SetCharacterSet(tar_setId, player)
                                Case 4
                                    Dim tmpGlyph As New Glyph
                                    tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                                    tmpGlyph.slotname = "secminorglyph1"
                                    tmpGlyph.spec = 1
                                    tmpGlyph.type = 1
                                    AddCharacterGlyph(player, tmpGlyph)
                                    SetCharacterSet(tar_setId, player)
                                Case 5
                                    Dim tmpGlyph As New Glyph
                                    tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                                    tmpGlyph.slotname = "secmajorglyph3"
                                    tmpGlyph.spec = 1
                                    tmpGlyph.type = 2
                                    AddCharacterGlyph(player, tmpGlyph)
                                    SetCharacterSet(tar_setId, player)
                                Case 6
                                    Dim tmpGlyph As New Glyph
                                    tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                                    tmpGlyph.slotname = "secprimeglyph1"
                                    tmpGlyph.spec = 1
                                    tmpGlyph.type = 3
                                    AddCharacterGlyph(player, tmpGlyph)
                                    SetCharacterSet(tar_setId, player)
                                Case 7
                                    Dim tmpGlyph As New Glyph
                                    tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                                    tmpGlyph.slotname = "secprimeglyph2"
                                    tmpGlyph.spec = 1
                                    tmpGlyph.type = 3
                                    AddCharacterGlyph(player, tmpGlyph)
                                    SetCharacterSet(tar_setId, player)
                                Case 8
                                    Dim tmpGlyph As New Glyph
                                    tmpGlyph.id = GetGlyphIdByItemId(prevglyphid)
                                    tmpGlyph.slotname = "secprimeglyph3"
                                    tmpGlyph.spec = 1
                                    tmpGlyph.type = 3
                                    AddCharacterGlyph(player, tmpGlyph)
                                    SetCharacterSet(tar_setId, player)
                                Case Else : End Select
                        Case Else : End Select
                End If
            Catch ex As Exception
                LogAppend("Something went wrong while loading character Glyphs -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterGlyphsHandler_loadAtMangos", True, True)
            End Try
            proccounter += 1
        Loop Until proccounter = resultquantity
    End Sub
End Class
