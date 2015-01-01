'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013-2015 NamCore Studio <https://github.com/megasus/Namcore-Studio>
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
'*      /Filename:      CharacterSpellsHandler
'*      /Description:   Contains functions for extracting information about the known spells 
'*                      of a specific character
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Database
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Modules
Imports libnc.Provider

Namespace Framework.Core
    Public Class CharacterSpellsHandler
        Public Sub GetCharacterSpells(ByVal characterGuid As Integer, ByVal setId As Integer, ByVal account As Account)
            LogAppend("Loading character spells for characterGuid: " & characterGuid & " and setId: " & setId,
                      "CharacterSpellsHandler_GetCharacterSpells", True)
            Select Case GlobalVariables.sourceCore
                Case Modules.Core.ARCEMU
                    LoadAtArcemu(characterGuid, setId, account)
                Case Modules.Core.TRINITY
                    LoadAtTrinity(characterGuid, setId, account)
                Case Modules.Core.MANGOS
                    LoadAtMangos(characterGuid, setId, account)
            End Select
        End Sub

        Private Sub LoadAtArcemu(ByVal charguid As Integer, ByVal tarSetId As Integer, ByVal account As Account)
            LogAppend("Loading character spells @LoadAtArcemu", "CharacterSpellsHandler_LoadAtArcemu", False)
            Dim tempdt As DataTable =
                    ReturnDataTable(
                        "SELECT " & GlobalVariables.sourceStructure.char_spells_col(0) & " FROM " &
                        GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                        GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString() & "'")
            Dim player As Character = GetCharacterSetBySetId(tarSetId, account)
            If player.Spells Is Nothing Then player.Spells = New List(Of Spell)()
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
                            spl.Id = TryInt(parts(partscounter).ToString)
                            spl.Active = 1
                            spl.Disabled = 0
                            partscounter += 1
                            LogAppend("Adding spellId: " & spl.Id.ToString(), "CharacterSpellsHandler_LoadAtArcemu",
                                      True)
                            Dim professionId As Integer = GetSkillIdBySpellId(spl.Id)
                            If IsProfession(professionId) Then
                                player.AddRecipeToProfession(professionId, spl.Id)
                            Else
                                player.Spells.Add(spl)
                            End If
                        Loop Until partscounter = excounter - 1
                        count += 1
                    Loop Until count = lastcount
                Else
                    LogAppend("No spells found!", "CharacterSpellsHandler_LoadAtArcemu", True)
                End If
            Catch ex As Exception
                LogAppend(
                    "Something went wrong while loading character spells! -> skipping -> Exception is: ###START###" &
                    ex.ToString() & "###END###", "CharacterSpellsHandler_LoadAtArcemu", True, True)
                Exit Sub
            End Try
            SetCharacterSet(tarSetId, player, account)
        End Sub

        Private Sub LoadAtTrinity(ByVal charguid As Integer, ByVal tarSetId As Integer, ByVal account As Account)
            LogAppend("Loading character spells @LoadAtTrinity", "CharacterSpellsHandler_LoadAtTrinity", False)
            Dim tempdt As DataTable =
                    ReturnDataTable(
                        "SELECT " & GlobalVariables.sourceStructure.spell_spell_col(0) & ", " &
                        GlobalVariables.sourceStructure.spell_active_col(0) & ", " &
                        GlobalVariables.sourceStructure.spell_disabled_col(0) &
                        " FROM " & GlobalVariables.sourceStructure.character_spells_tbl(0) & " WHERE " &
                        GlobalVariables.sourceStructure.spell_guid_col(0) & "='" & charguid.ToString() & "'")
            Dim player As Character = GetCharacterSetBySetId(tarSetId, account)
            If player.Spells Is Nothing Then player.Spells = New List(Of Spell)()
            If player.Professions Is Nothing Then player.Professions = New List(Of Profession)()
            Try
                Dim lastcount As Integer = tempdt.Rows.Count
                Dim count As Integer = 0
                If Not lastcount = 0 Then
                    Do
                        Dim readedcode As String = (tempdt.Rows(count).Item(0)).ToString
                        Dim spl As New Spell
                        spl.Id = TryInt(readedcode)
                        spl.Active = TryInt((tempdt.Rows(count).Item(1)).ToString)
                        spl.Disabled = TryInt((tempdt.Rows(count).Item(2)).ToString)
                        Dim professionId As Integer = GetSkillIdBySpellId(spl.Id)
                        If IsProfession(professionId) Then
                            player.AddRecipeToProfession(professionId, spl.Id)
                        Else
                            player.Spells.Add(spl)
                        End If
                        count += 1
                    Loop Until count = lastcount
                Else
                    LogAppend("No spells found!", "CharacterSpellsHandler_LoadAtTrinity", True)
                End If
            Catch ex As Exception
                LogAppend(
                    "Something went wrong while loading character spells! -> skipping -> Exception is: ###START###" &
                    ex.ToString() & "###END###", "CharacterSpellsHandler_LoadAtTrinity", True, True)
                Exit Sub
            End Try
            SetCharacterSet(tarSetId, player, account)
        End Sub

        Private Sub LoadAtMangos(ByVal charguid As Integer, ByVal tarSetId As Integer, ByVal account As Account)
            LogAppend("Loading character spells @LoadAtMangos", "CharacterSpellsHandler_LoadAtMangos", False)
            Dim tempdt As DataTable =
                    ReturnDataTable(
                        "SELECT " & GlobalVariables.sourceStructure.spell_spell_col(0) & ", " &
                        GlobalVariables.sourceStructure.spell_active_col(0) & ", " &
                        GlobalVariables.sourceStructure.spell_disabled_col(0) &
                        " FROM " & GlobalVariables.sourceStructure.character_spells_tbl(0) & " WHERE " &
                        GlobalVariables.sourceStructure.spell_guid_col(0) & "='" & charguid.ToString() & "'")
            Dim player As Character = GetCharacterSetBySetId(tarSetId, account)
            If player.Spells Is Nothing Then player.Spells = New List(Of Spell)()
            Try
                Dim lastcount As Integer = tempdt.Rows.Count
                Dim count As Integer = 0
                If Not lastcount = 0 Then
                    Do
                        Dim readedcode As String = (tempdt.Rows(count).Item(0)).ToString
                        Dim spl As New Spell
                        spl.Id = TryInt(readedcode)
                        spl.Active = TryInt((tempdt.Rows(count).Item(1)).ToString)
                        spl.Disabled = TryInt((tempdt.Rows(count).Item(2)).ToString)
                        Dim professionId As Integer = GetSkillIdBySpellId(spl.Id)
                        If IsProfession(professionId) Then
                            player.AddRecipeToProfession(professionId, spl.Id)
                        Else
                            player.Spells.Add(spl)
                        End If
                        count += 1
                    Loop Until count = lastcount
                Else
                    LogAppend("No spells found!", "CharacterSpellsHandler_LoadAtMangos", True)
                End If
            Catch ex As Exception
                LogAppend(
                    "Something went wrong while loading character spells! -> skipping -> Exception is: ###START###" &
                    ex.ToString() & "###END###", "CharacterSpellsHandler_LoadAtMangos", True, True)
                Exit Sub
            End Try
            SetCharacterSet(tarSetId, player, account)
        End Sub
    End Class
End Namespace