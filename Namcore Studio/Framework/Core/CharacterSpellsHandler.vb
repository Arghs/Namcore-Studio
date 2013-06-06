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
'*      /Filename:      CharacterSpellsHandler
'*      /Description:   Contains functions for extracting information about the known spells 
'*                      of a specific character
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Imports Namcore_Studio.EventLogging
Imports Namcore_Studio.Basics
Imports Namcore_Studio.GlobalVariables
Imports Namcore_Studio.CommandHandler
Imports Namcore_Studio.Conversions
Public Class CharacterSpellsHandler
    Public Shared Sub GetCharacterSpells(ByVal characterGuid As Integer, ByVal setId As Integer, ByVal accountId As Integer)
        LogAppend("Loading character spells for characterGuid: " & characterGuid & " and setId: " & setId, "CharacterSpellsHandler_GetCharacterSpells", True)
        Select Case sourceCore
            Case "arcemu"
                loadAtArcemu(characterGuid, setId, accountId)
            Case "trinity"
                loadAtTrinity(characterGuid, setId, accountId)
            Case "trinitytbc"
                loadAtTrinityTBC(characterGuid, setId, accountId)
            Case "mangos"
                loadAtMangos(characterGuid, setId, accountId)
            Case Else

        End Select

    End Sub
    Private Shared Sub loadAtArcemu(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)
        LogAppend("Loading character spells @loadAtArcemu", "CharacterSpellsHandler_loadAtArcemu", False)
        Dim tempdt As DataTable = ReturnDataTable("SELECT " & sourceStructure.char_spells_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString() & "'")
        Dim player As Character = GetCharacterSetBySetId(tar_setId)
        Try
            Dim lastcount As Integer = tempdt.Rows.Count
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim readedcode As String = (tempdt.Rows(count).Item(0)).ToString
                    Dim excounter As Integer = UBound(readedcode.Split(CChar(",")))
                    Dim partscounter As Integer = 0
                    Do
                        Dim parts() As String = readedcode.Split(","c)
                        Dim spl As New Spell
                        spl.id = TryInt(parts(partscounter).ToString)
                        spl.active = 1
                        spl.disabled = 0
                        partscounter += 1
                        LogAppend("Adding spellId: " & spl.id.ToString(), "CharacterSpellsHandler_LoadAtArcemu", True)
                        player.Spells.Add(spl)
                    Loop Until partscounter = excounter - 1
                    count += 1
                Loop Until count = lastcount
            Else
                LogAppend("No spells found!", "CharacterSpellsHandler_loadAtArcemu", True)
            End If
        Catch ex As Exception
            LogAppend("Something went wrong while loading character spells! -> skipping -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterSpellsHandler_loadAtArcemu", True, True)
            Exit Sub
        End Try
        SetCharacterSet(tar_setId, player)
    End Sub
    Private Shared Sub loadAtTrinity(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)
        LogAppend("Loading character spells @loadAtTrinity", "CharacterSpellsHandler_loadAtTrinity", False)
        Dim tempdt As DataTable = ReturnDataTable("SELECT " & sourceStructure.spell_spell_col(0) & ", " & sourceStructure.spell_active_col(0) & ", " & sourceStructure.spell_disabled_col(0) &
                                                  " FROM " & sourceStructure.character_spells_tbl(0) & " WHERE " & sourceStructure.spell_guid_col(0) & "='" & charguid.ToString() & "'")
        Dim player As Character = GetCharacterSetBySetId(tar_setId)
        Try
            Dim lastcount As Integer = tempdt.Rows.Count
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim readedcode As String = (tempdt.Rows(count).Item(0)).ToString
                    Dim spl As New Spell
                    spl.id = TryInt(readedcode)
                    spl.active = TryInt((tempdt.Rows(count).Item(1)).ToString)
                    spl.disabled = TryInt((tempdt.Rows(count).Item(2)).ToString)
                    player.Spells.Add(spl)
                    count += 1
                Loop Until count = lastcount
            Else
                LogAppend("No spells found!", "CharacterSpellsHandler_loadAtTrinity", True)
            End If
        Catch ex As Exception
            LogAppend("Something went wrong while loading character spells! -> skipping -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterSpellsHandler_loadAtTrinity", True, True)
            Exit Sub
        End Try
        SetCharacterSet(tar_setId, player)
    End Sub
    Private Shared Sub loadAtTrinityTBC(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)

    End Sub
    Private Shared Sub loadAtMangos(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)
        LogAppend("Loading character spells @loadAtMangos", "CharacterSpellsHandler_loadAtMangos", False)
        Dim tempdt As DataTable = ReturnDataTable("SELECT " & sourceStructure.spell_spell_col(0) & ", " & sourceStructure.spell_active_col(0) & ", " & sourceStructure.spell_disabled_col(0) &
                                                  " FROM " & sourceStructure.character_spells_tbl(0) & " WHERE " & sourceStructure.spell_guid_col(0) & "='" & charguid.ToString() & "'")
        Dim player As Character = GetCharacterSetBySetId(tar_setId)
        Try
            Dim lastcount As Integer = tempdt.Rows.Count
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim readedcode As String = (tempdt.Rows(count).Item(0)).ToString
                    Dim spl As New Spell
                    spl.id = TryInt(readedcode)
                    spl.active = TryInt((tempdt.Rows(count).Item(1)).ToString)
                    spl.disabled = TryInt((tempdt.Rows(count).Item(2)).ToString)
                    player.Spells.Add(spl)
                    count += 1
                Loop Until count = lastcount
            Else
                LogAppend("No spells found!", "CharacterSpellsHandler_loadAtMangos", True)
            End If
        Catch ex As Exception
            LogAppend("Something went wrong while loading character spells! -> skipping -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterSpellsHandler_loadAtMangos", True, True)
            Exit Sub
        End Try
        SetCharacterSet(tar_setId, player)
    End Sub
End Class
