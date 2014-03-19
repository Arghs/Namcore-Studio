﻿'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
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
'*      /Filename:      CharacterRemoveHandler
'*      /Description:   Contains functions for removing characters
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.Database
Imports NCFramework.Framework.Logging
Imports MySql.Data.MySqlClient
Imports NCFramework.Framework.Modules

Namespace Framework.Core.Remove
    Public Class CharacterRemoveHandler
        Public Sub RemoveCharacterFromDb(ByVal character As Character, ByVal connection As MySqlConnection,
                                         ByVal dbstruc As DbStructure, ByVal core As String)
            LogAppend("Removing character " & character.Name & " from database",
                      "CharacterRemoveHandler_RemoveCharacterFromDb")
            Select Case core
                Case "trinity"
                    runSQLCommand_characters_string_setconn(
                        "REMOVE FROM `" & dbstruc.character_tbl(0) & "` WHERE `" & dbstruc.char_guid_col(0) & "` = '" &
                        character.Guid.ToString() & "'", connection)
                    runSQLCommand_characters_string_setconn(
                        "REMOVE FROM `" & dbstruc.av_achievement_col(0) & "` WHERE `" & dbstruc.av_guid_col(0) & "` = '" &
                        character.Guid.ToString() & "'", connection)
                    runSQLCommand_characters_string_setconn(
                        "REMOVE FROM `arena_team` WHERE `captainGuid` = '" & character.Guid.ToString() & "'", connection)
                    runSQLCommand_characters_string_setconn(
                        "REMOVE FROM `arena_team_member` WHERE `guid` = '" & character.Guid.ToString() & "'", connection)
                    runSQLCommand_characters_string_setconn(
                        "REMOVE FROM `auctionhouse` WHERE `itemowner` = '" & character.Guid.ToString() & "'", connection)
                    runSQLCommand_characters_string_setconn(
                        "REMOVE FROM `character_account_data` WHERE `guid` = '" & character.Guid.ToString() & "'",
                        connection)
                    runSQLCommand_characters_string_setconn(
                        "REMOVE FROM `character_achievement_progress` WHERE `guid` = '" & character.Guid.ToString() &
                        "'", connection)
                    runSQLCommand_characters_string_setconn(
                        "REMOVE FROM `" & dbstruc.character_action_tbl(0) & "` WHERE `" & dbstruc.action_guid_col(0) &
                        "` = '" & character.Guid.ToString() & "'", connection)
                    runSQLCommand_characters_string_setconn(
                        "REMOVE FROM `character_arena_stats` WHERE `guid` = '" & character.Guid.ToString() & "'",
                        connection)
                    runSQLCommand_characters_string_setconn(
                        "REMOVE FROM `character_aura` WHERE `guid` = '" & character.Guid.ToString() & "'", connection)
                    runSQLCommand_characters_string_setconn(
                        "REMOVE FROM `character_banned` WHERE `guid` = '" & character.Guid.ToString() & "'", connection)
                    runSQLCommand_characters_string_setconn(
                        "REMOVE FROM `character_battleground_data` WHERE `guid` = '" & character.Guid.ToString() & "'",
                        connection)
                    runSQLCommand_characters_string_setconn(
                        "REMOVE FROM `character_battleground_random` WHERE `guid` = '" & character.Guid.ToString() & "'",
                        connection)
                    runSQLCommand_characters_string_setconn(
                        "REMOVE FROM `character_declinedname` WHERE `guid` = '" & character.Guid.ToString() & "'",
                        connection)
                    runSQLCommand_characters_string_setconn(
                        "REMOVE FROM `character_equipmentsets` WHERE `guid` = '" & character.Guid.ToString() & "'",
                        connection)
                    runSQLCommand_characters_string_setconn(
                        "REMOVE FROM `character_gifts` WHERE `guid` = '" & character.Guid.ToString() & "'", connection)
                    runSQLCommand_characters_string_setconn(
                        "REMOVE FROM `" & dbstruc.character_glyphs_tbl(0) & "` WHERE `" & dbstruc.glyphs_guid_col(0) &
                        "` = '" & character.Guid.ToString() & "'", connection)
                    runSQLCommand_characters_string_setconn(
                        "REMOVE FROM `" & dbstruc.character_homebind_tbl(0) & "` WHERE `" & dbstruc.homebind_guid_col(0) &
                        "` = '" & character.Guid.ToString() & "'", connection)
                    runSQLCommand_characters_string_setconn(
                        "REMOVE FROM `character_instance` WHERE `guid` = '" & character.Guid.ToString() & "'",
                        connection)
                    runSQLCommand_characters_string_setconn(
                        "REMOVE FROM `" & dbstruc.character_inventory_tbl(0) & "` WHERE `" & dbstruc.invent_guid_col(0) &
                        "` = '" & character.Guid.ToString() & "'", connection)
                    runSQLCommand_characters_string_setconn(
                        "REMOVE FROM `character_pet` WHERE `guid` = '" & character.Guid.ToString() & "'", connection)
                    runSQLCommand_characters_string_setconn(
                        "REMOVE FROM `character_pet_declinedname` WHERE `guid` = '" & character.Guid.ToString() & "'",
                        connection)
                    runSQLCommand_characters_string_setconn(
                        "REMOVE FROM `" & dbstruc.character_queststatus_tbl(0) & "` WHERE `" & dbstruc.qst_guid_col(0) &
                        "` = '" & character.Guid.ToString() & "'", connection)
                    runSQLCommand_characters_string_setconn(
                        "REMOVE FROM `character_queststatus_daily` WHERE `guid` = '" & character.Guid.ToString() & "'",
                        connection)
                    runSQLCommand_characters_string_setconn(
                        "REMOVE FROM `character_queststatus_monthly` WHERE `guid` = '" & character.Guid.ToString() & "'",
                        connection)
                    runSQLCommand_characters_string_setconn(
                        "REMOVE FROM `" & dbstruc.character_queststatus_rewarded_tbl(0) & "` WHERE `" &
                        dbstruc.qstre_guid_col(0) & "` = '" & character.Guid.ToString() & "'", connection)
                    runSQLCommand_characters_string_setconn(
                        "REMOVE FROM `character_queststatus_seasonal` WHERE `guid` = '" & character.Guid.ToString() &
                        "'", connection)
                    runSQLCommand_characters_string_setconn(
                        "REMOVE FROM `character_queststatus_weekly` WHERE `guid` = '" & character.Guid.ToString() & "'",
                        connection)
                    runSQLCommand_characters_string_setconn(
                        "REMOVE FROM `" & dbstruc.character_reputation_tbl(0) & "` WHERE `" & dbstruc.rep_guid_col(0) &
                        "` = '" & character.Guid.ToString() & "'", connection)
                    runSQLCommand_characters_string_setconn(
                        "REMOVE FROM `" & dbstruc.character_skills_tbl(0) & "` WHERE `" & dbstruc.skill_guid_col(0) &
                        "` = '" & character.Guid.ToString() & "'", connection)
                    runSQLCommand_characters_string_setconn(
                        "REMOVE FROM `character_social` WHERE `guid` = '" & character.Guid.ToString() & "'", connection)
                    runSQLCommand_characters_string_setconn(
                        "REMOVE FROM `" & dbstruc.character_spells_tbl(0) & "` WHERE `" & dbstruc.spell_guid_col(0) &
                        "` = '" & character.Guid.ToString() & "'", connection)
                    runSQLCommand_characters_string_setconn(
                        "REMOVE FROM `character_spell_cooldown` WHERE `guid` = '" & character.Guid.ToString() & "'",
                        connection)
                    runSQLCommand_characters_string_setconn(
                        "REMOVE FROM `character_stats` WHERE `guid` = '" & character.Guid.ToString() & "'", connection)
                    runSQLCommand_characters_string_setconn(
                        "REMOVE FROM `" & dbstruc.character_talent_tbl(0) & "` WHERE `" & dbstruc.talent_guid_col(0) &
                        "` = '" & character.Guid.ToString() & "'", connection)
                    runSQLCommand_characters_string_setconn(
                        "REMOVE FROM `corpse` WHERE `guid` = '" & character.Guid.ToString() & "'", connection)
                    runSQLCommand_characters_string_setconn(
                        "REMOVE FROM `group_member` WHERE `memberGuid` = '" & character.Guid.ToString() & "'",
                        connection)
                    runSQLCommand_characters_string_setconn(
                        "REMOVE FROM `groups` WHERE `leaderGuid` = '" & character.Guid.ToString() & "'", connection)
                    runSQLCommand_characters_string_setconn(
                        "REMOVE FROM `guild_member` WHERE `guid` = '" & character.Guid.ToString() & "'", connection)
                    runSQLCommand_characters_string_setconn(
                        "REMOVE FROM `" & dbstruc.item_instance_tbl(0) & "` WHERE `" & dbstruc.itmins_guid_col(0) &
                        "` = '" & character.Guid.ToString() & "'", connection)
                    runSQLCommand_characters_string_setconn(
                        "REMOVE FROM `item_refund_instance` WHERE `player_guid` = '" & character.Guid.ToString() & "'",
                        connection)
                    runSQLCommand_characters_string_setconn(
                        "REMOVE FROM `mail` WHERE `receiver` = '" & character.Guid.ToString() & "'", connection)
                    runSQLCommand_characters_string_setconn(
                        "REMOVE FROM `mail_items` WHERE `receiver` = '" & character.Guid.ToString() & "'", connection)
                    runSQLCommand_characters_string_setconn(
                        "REMOVE FROM `pet_aura` WHERE `caster_guid` = '" & character.Guid.ToString() & "'", connection)
                    runSQLCommand_characters_string_setconn(
                        "REMOVE FROM `petition` WHERE `ownerguid` = '" & character.Guid.ToString() & "'", connection)
                    runSQLCommand_characters_string_setconn(
                        "REMOVE FROM `petition_sign` WHERE `ownerguid` = '" & character.Guid.ToString() & "'",
                        connection)
                    runSQLCommand_characters_string_setconn(
                        "REMOVE FROM `petition_sign` WHERE `playerguid` = '" & character.Guid.ToString() & "'",
                        connection)
            End Select
        End Sub
    End Class
End Namespace