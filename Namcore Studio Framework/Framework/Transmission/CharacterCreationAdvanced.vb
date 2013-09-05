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
'*      /Filename:      CharacterCreationAdvanced
'*      /Description:   Includes functions for creating a new character which has not been
'*                      parsed from the wow armory
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Imports NCFramework.EventLogging
Imports NCFramework.Basics
Imports NCFramework.GlobalVariables
Imports NCFramework.Conversions
Imports NCFramework.CommandHandler
Imports NCFramework.SpellCreation
Imports NCFramework.SkillCreation
Imports NCFramework.CharacterCreationLite
Imports MySql.Data.MySqlClient
Public Class CharacterCreationAdvanced
    Public Sub CreateNewAdvancedCharacter(ByVal charname As String, ByVal accountId As String, ByVal setId As Integer, Optional forceNameChange As Boolean = False)
        LogAppend("Creating new character: " & charname & " for account : " & accountId.ToString, "CharacterCreationAdvanced_CreateNewAdvancedCharacter", True)
        Select Case targetCore
            Case "arcemu"
                createAtArcemu(charname, accountId.ToString, setId, forceNameChange)
            Case "trinity"
                createAtTrinity(charname, accountId.ToString, setId, forceNameChange)
            Case "trinitytbc"

            Case "mangos"
                createAtMangos(charname, accountId.ToString, setId, forceNameChange)
            Case Else

        End Select
    End Sub

    Private Sub createAtArcemu(ByVal charactername As String, ByVal accid As Integer, ByVal targetSetId As Integer, ByVal NameChange As Boolean)
        LogAppend("Creating at arcemu", "CharacterCreationAdvanced_createAtArcemu", False)
        Dim newcharguid As Integer = TryInt(runSQLCommand_characters_string("SELECT " & targetStructure.char_guid_col(0) & " FROM " & targetStructure.character_tbl(0) & " WHERE " & targetStructure.char_guid_col(0) &
                                                                            "=(SELECT MAX(" & targetStructure.char_guid_col(0) & ") FROM " & targetStructure.character_tbl(0) & ")", True)) + 1
        ' Dim accid As Integer = TryInt(runSQLCommand_realm_string("SELECT " & targetStructure.acc_id_col(0) & " FROM " & targetStructure.account_tbl(0) & " WHERE " & targetStructure.acc_name_col(0) & "='" & accname & "'", True))
        Dim sqlstring As String = "INSERT INTO " & targetStructure.character_tbl(0) & " ( `" & targetStructure.char_guid_col(0) & "`, `" & targetStructure.char_accountId_col(0) & "`, `" & targetStructure.char_name_col(0) &
            "`, `" & targetStructure.char_race_col(0) & "`, `" & targetStructure.char_class_col(0) & "`, `" & targetStructure.char_gender_col(0) & "`, `" & targetStructure.char_level_col(0) & "`, `" &
            targetStructure.char_xp_col(0) & "`, `" & targetStructure.char_gold_col(0) & "`, `" & targetStructure.char_playerBytes_col(0) & "`, `" & targetStructure.char_playerBytes2_col(0) & "`, `" &
            targetStructure.char_playerFlags_col(0) & "`, `" & targetStructure.char_posX_col(0) & "`, " & "" & targetStructure.char_posY_col(0) & ", " & targetStructure.char_posZ_col(0) & ", " &
            targetStructure.char_map_col(0) & ", " & targetStructure.char_orientation_col(0) & ", " & targetStructure.char_taximask_col(0) & ", " & targetStructure.char_arcemuPlayedTime_col(0) & ", " &
            targetStructure.char_stableSlots_col(0) & ", " & targetStructure.char_zone_col(0) & ", " & targetStructure.char_watchedFaction_col(0) & ", current_hp, " &
                                    "" & targetStructure.char_speccount_col(0) & ", " & targetStructure.char_activeSpec_col(0) & ", " & targetStructure.char_exploredZones_col(0) & ", " &
                                    targetStructure.char_knownTitles_col(0) & " ) VALUES " &
                                    "( @guid, @accid, @name, @race, @class, @gender, @level, @xp, gold, @pBytes, pBytes2, @pFlags, @posx, @posy, @posz, @map, '4,40671', @taxi, '0 0 0 ', @stable, @zone, " &
                                    "@title, @wFaction, '1000', @speccpunt, @activespec, @exploredZones, @knownTitles )"
        Dim tempcommand As New MySqlCommand(sqlstring, TargetConnection)
        Dim player As Character = GetCharacterSetBySetId(targetSetId)
        tempcommand.Parameters.AddWithValue("@accid", accid.ToString)
        tempcommand.Parameters.AddWithValue("@guid", newcharguid.ToString())
        tempcommand.Parameters.AddWithValue("@name", charactername)
        tempcommand.Parameters.AddWithValue("@class", player.Cclass.ToString)
        tempcommand.Parameters.AddWithValue("@race", player.Race.ToString)
        tempcommand.Parameters.AddWithValue("@gender", player.Gender.ToString)
        tempcommand.Parameters.AddWithValue("@xp", player.Xp.ToString)
        tempcommand.Parameters.AddWithValue("@gold", player.Gold)
        tempcommand.Parameters.AddWithValue("@level", player.Level.ToString)
        tempcommand.Parameters.AddWithValue("@pBytes", player.PlayerBytes.ToString)
        tempcommand.Parameters.AddWithValue("@pBytes2", player.PlayerBytes2.ToString)
        tempcommand.Parameters.AddWithValue("@pFlags", player.PlayerFlags.ToString)
        tempcommand.Parameters.AddWithValue("@posx", player.PositionX.ToString)
        tempcommand.Parameters.AddWithValue("@posy", player.PositionY.ToString)
        tempcommand.Parameters.AddWithValue("@posz", (player.PositionZ + 1).ToString)
        tempcommand.Parameters.AddWithValue("@map", player.Map.ToString)
        tempcommand.Parameters.AddWithValue("@taxi", player.Taximask)
        tempcommand.Parameters.AddWithValue("@stable", player.StableSlots.ToString)
        tempcommand.Parameters.AddWithValue("@zone", player.Zone.ToString)
        tempcommand.Parameters.AddWithValue("@wFaction", player.WatchedFaction.ToString)
        tempcommand.Parameters.AddWithValue("@speccpunt", player.SpecCount.ToString)
        tempcommand.Parameters.AddWithValue("@activespec", player.ActiveSpec.ToString)
        tempcommand.Parameters.AddWithValue("@exploredZones", player.ExploredZones)
        tempcommand.Parameters.AddWithValue("@knownTitles", player.KnownTitles)
        'PlayerBytes column might not be correct! check player_bytes, bytes, bytes2
        Dim m_charCreationLite As New CharacterCreationLite
        Try
            tempcommand.ExecuteNonQuery()
            If NameChange = True Then
                runSQLCommand_characters_string("UPDATE " & targetStructure.character_tbl(0) & " SET forced_rename_pending='1' WHERE " & targetStructure.char_guid_col(0) & "='" & newcharguid.ToString & "'", True)
            Else
                If m_charCreationLite.CharacterExist(charactername) = True Then
                    runSQLCommand_characters_string("UPDATE " & targetStructure.character_tbl(0) & " SET forced_rename_pending='1' WHERE " & targetStructure.char_guid_col(0) & "='" & newcharguid.ToString & "'", True)
                End If
            End If
            'Creating hearthstone
            LogAppend("Creating character hearthstone", "CharacterCreationAdvanced_createAtArcemu", False)
            Dim newitemguid As Integer = (TryInt(runSQLCommand_characters_string("SELECT " & targetStructure.char_guid_col(0) & " FROM " & targetStructure.item_instance_tbl(0) & " WHERE " & targetStructure.itmins_guid_col(0) &
                                                                                 "=(SELECT MAX(" & targetStructure.itmins_guid_col(0) & ") FROM " & targetStructure.item_instance_tbl(0) & ")", True)) + 5)
            runSQLCommand_characters_string("INSERT INTO " & targetStructure.item_instance_tbl(0) & " ( " & targetStructure.itmins_ownerGuid_col(0) & ", " & targetStructure.itmins_guid_col(0) &
                                            ", " & targetStructure.itmins_itemEntry_col(0) & ", flags, " & targetStructure.itmins_container_col(0) & ", " & targetStructure.itmins_slot_col(0) &
                                            " ) VALUES ( '" & newcharguid.ToString() &
                "', '" & newitemguid.ToString() & "', '6948', '1', '-1', '23' )", True)
            AddSpells("6603,")
            Dim tempinfo As String = player.CustomFaction.ToString
            If Not tempinfo = "" Then runSQLCommand_characters_string("UPDATE " & targetStructure.character_tbl(0) & " SET " & targetStructure.char_customFaction_col(0) & "='" & tempinfo &
                "' WHERE " & targetStructure.char_guid_col(0) & "='" & newcharguid.ToString & "'")
            'Setting tutorials
            runSQLCommand_characters_string("INSERT INTO `tutorials` ( playerId ) VALUES ( " & accid.ToString() & " )", True)
            'Set home
            LogAppend("Setting character homebind", "CharacterCreationAdvanced_createAtArcemu", False)
            Dim tmpstring As String = player.HomeBind
            runSQLCommand_characters_string("UPDATE " & targetStructure.character_tbl(0) & " SET " & targetStructure.char_bindposX_col(0) & "='" & splitList(tmpstring, "position_x") &
                                            "' WHERE " & targetStructure.char_guid_col(0) & "='" & newcharguid.ToString() & "'")
            runSQLCommand_characters_string("UPDATE " & targetStructure.character_tbl(0) & " SET " & targetStructure.char_bindposY_col(0) & "='" & splitList(tmpstring, "position_y") &
                                            "' WHERE " & targetStructure.char_guid_col(0) & "='" & newcharguid.ToString() & "'")
            runSQLCommand_characters_string("UPDATE " & targetStructure.character_tbl(0) & " SET " & targetStructure.char_bindposZ_col(0) & "='" & splitList(tmpstring, "position_z") &
                                            "' WHERE " & targetStructure.char_guid_col(0) & "='" & newcharguid.ToString() & "'")
            runSQLCommand_characters_string("UPDATE " & targetStructure.character_tbl(0) & " SET " & targetStructure.char_bindmapid_col(0) & "='" & splitList(tmpstring, "map") &
                                            "' WHERE " & targetStructure.char_guid_col(0) & "='" & newcharguid.ToString() & "'")
            runSQLCommand_characters_string("UPDATE " & targetStructure.character_tbl(0) & " SET " & targetStructure.char_bindzoneid_col(0) & "='" & splitList(tmpstring, "zone") &
                                            "' WHERE " & targetStructure.char_guid_col(0) & "='" & newcharguid.ToString() & "'")
        Catch ex As Exception
            LogAppend("Something went wrong while creating the account -> Skipping! -> Error message is: " & ex.ToString(), "CharacterCreationAdvanced_createAtArcemu", False, True)
        End Try
        characterGUID = newcharguid
    End Sub
    Private Sub createAtTrinity(ByVal charactername As String, ByVal accid As Integer, ByVal targetSetId As Integer, ByVal NameChange As Boolean)
        LogAppend("Creating at Trinity", "CharacterCreationAdvanced_createAtTrinity", False)
        Dim newcharguid As Integer = TryInt(runSQLCommand_characters_string("SELECT " & targetStructure.char_guid_col(0) & " FROM " & targetStructure.character_tbl(0) & " WHERE " & targetStructure.char_guid_col(0) &
                                                                            "=(SELECT MAX(" & targetStructure.char_guid_col(0) & ") FROM " & targetStructure.character_tbl(0) & ")", True)) + 1
        '   Dim accid As Integer = TryInt(runSQLCommand_realm_string("SELECT `" & targetStructure.acc_id_col(0) & "` FROM " & targetStructure.account_tbl(0) & " WHERE " & targetStructure.acc_name_col(0) & "='" & accname & "'", True))
        Dim sqlstring As String = "INSERT INTO " & targetStructure.character_tbl(0) & " ( `" & targetStructure.char_guid_col(0) & "`, `" & targetStructure.char_accountId_col(0) & "`, `" & targetStructure.char_name_col(0) &
            "`, `" & targetStructure.char_race_col(0) & "`, `" & targetStructure.char_class_col(0) & "`, `" & targetStructure.char_gender_col(0) & "`, `" & targetStructure.char_level_col(0) & "`, `" &
            targetStructure.char_xp_col(0) & "`, `" & targetStructure.char_gold_col(0) & "`, `" & targetStructure.char_playerBytes_col(0) & "`, `" & targetStructure.char_playerBytes2_col(0) & "`, " &
            "`" & targetStructure.char_playerFlags_col(0) & "`, `" & targetStructure.char_posX_col(0) & "`, " & targetStructure.char_posY_col(0) & ", " & targetStructure.char_posZ_col(0) & ", " &
            targetStructure.char_map_col(0) & ", " & targetStructure.char_orientation_col(0) & ", " & targetStructure.char_taximask_col(0) & ", " & targetStructure.char_cinematic_col(0) & ", " &
            targetStructure.char_totaltime_col(0) & ", " & targetStructure.char_leveltime_col(0) & ", " & targetStructure.char_extraFlags_col(0) & ", " & targetStructure.char_stableSlots_col(0) & ", " &
            targetStructure.char_atlogin_col(0) & ", " & targetStructure.char_zone_col(0) & ", " & targetStructure.char_chosenTitle_col(0) & ", " &
            "" & targetStructure.char_knownCurrencies_col(0) & ", " & targetStructure.char_watchedFaction_col(0) & ", `" & targetStructure.char_health_col(0) & "`, " & targetStructure.char_speccount_col(0) & ", " &
            targetStructure.char_activeSpec_col(0) & ", " & targetStructure.char_exploredZones_col(0) & ", " & targetStructure.char_knownTitles_col(0) & ", " & targetStructure.char_actionBars_col(0) & " ) VALUES " &
            "( @guid, @accid, @name, @race, @class, @gender, @level, @xp, @gold, @pBytes, @pBytes2, @pFlags, @posx, @posy, @posz, @map, '4,40671', @taxi, '1', @totaltime, leveltime, @extraflags, " &
            "@stable, @login, @zone, @title, @knownCurrencies, @wFaction, '5000', @speccount, @activespec, @exploredZones, @knownTitles, @action )"
        Dim tempcommand As New MySqlCommand(sqlstring, TargetConnection)
        Dim player As Character = GetCharacterSetBySetId(targetSetId)
        tempcommand.Parameters.AddWithValue("@accid", accid.ToString())
        tempcommand.Parameters.AddWithValue("@guid", newcharguid.ToString())
        tempcommand.Parameters.AddWithValue("@name", charactername)
        tempcommand.Parameters.AddWithValue("@class", player.Cclass.ToString)
        tempcommand.Parameters.AddWithValue("@race", player.Race.ToString)
        tempcommand.Parameters.AddWithValue("@gender", player.Gender.ToString)
        tempcommand.Parameters.AddWithValue("@level", player.Level.ToString)
        tempcommand.Parameters.AddWithValue("@xp", player.Xp.ToString)
        tempcommand.Parameters.AddWithValue("@gold", player.Gold)
        tempcommand.Parameters.AddWithValue("@pBytes2", player.PlayerBytes2.ToString)
        tempcommand.Parameters.AddWithValue("@pFlags", player.PlayerFlags.ToString)
        tempcommand.Parameters.AddWithValue("@posx", player.PositionX.ToString)
        tempcommand.Parameters.AddWithValue("@posy", player.PositionY.ToString)
        tempcommand.Parameters.AddWithValue("@posz", (player.PositionZ + 1).ToString)
        tempcommand.Parameters.AddWithValue("@map", player.Map.ToString)
        tempcommand.Parameters.AddWithValue("@taxi", player.Taximask)
        tempcommand.Parameters.AddWithValue("@stable", player.StableSlots.ToString)
        tempcommand.Parameters.AddWithValue("@totaltime", player.TotalTime)
        tempcommand.Parameters.AddWithValue("@leveltime", player.LevelTime.ToString)
        tempcommand.Parameters.AddWithValue("@extraflags", player.ExtraFlags.ToString)
        tempcommand.Parameters.AddWithValue("@login", player.AtLogin.ToString)
        tempcommand.Parameters.AddWithValue("@zone", player.Zone.ToString)
        tempcommand.Parameters.AddWithValue("@knownCurrencies", player.KnownCurrencies.ToString)
        tempcommand.Parameters.AddWithValue("@action", player.ActionBars.ToString)
        tempcommand.Parameters.AddWithValue("@title", player.ChosenTitle.ToString)
        tempcommand.Parameters.AddWithValue("@wFaction", player.WatchedFaction.ToString)
        tempcommand.Parameters.AddWithValue("@speccpunt", player.SpecCount.ToString)
        tempcommand.Parameters.AddWithValue("@activespec", player.ActiveSpec.ToString)
        tempcommand.Parameters.AddWithValue("@exploredZones", player.ExploredZones)
        tempcommand.Parameters.AddWithValue("@knownTitles", player.KnownTitles.ToString)
        Dim m_charCreationLite As New CharacterCreationLite
        Try
            tempcommand.ExecuteNonQuery()
            If NameChange = True Then
                runSQLCommand_characters_string("UPDATE " & targetStructure.character_tbl(0) & " SET " & targetStructure.char_atlogin_col(0) & "='1' WHERE " & targetStructure.char_guid_col(0) & "='" &
                                                newcharguid.ToString & "'", True)
            Else
                If m_charCreationLite.CharacterExist(charactername) = True Then
                    runSQLCommand_characters_string("UPDATE " & targetStructure.character_tbl(0) & " SET " & targetStructure.char_atlogin_col(0) & "='1' WHERE " & targetStructure.char_guid_col(0) & "='" &
                                                    newcharguid.ToString & "'", True)
                End If
            End If
            'Creating hearthstone
            LogAppend("Creating character hearthstone", "CharacterCreationAdvanced_createAtArcemu", False)
            Dim newitemguid As Integer = (TryInt(runSQLCommand_characters_string("SELECT " & targetStructure.itmins_guid_col(0) & " FROM " & targetStructure.item_instance_tbl(0) & " WHERE " &
                                                                                 targetStructure.itmins_guid_col(0) & "=(SELECT MAX(" & targetStructure.itmins_guid_col(0) & ") FROM " &
                                                                                 targetStructure.item_instance_tbl(0) & ")", True)) + 1)
            runSQLCommand_characters_string("INSERT INTO " & targetStructure.item_instance_tbl(0) & " ( " & targetStructure.itmins_guid_col(0) & ", " & targetStructure.itmins_itemEntry_col(0) & ", " &
                                            targetStructure.itmins_ownerGuid_col(0) & ", " & targetStructure.itmins_count_col(0) & ", charges, " & targetStructure.itmins_enchantments_col(0) & ", " &
                                            targetStructure.itmins_durability_col(0) & " ) VALUES ( '" & newitemguid.ToString & "', '6948', '" & accid.ToString() & "', '1', '0 0 0 0 0 ', '" & newitemguid.ToString() &
               " 1191182336 3 6948 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )", True)
            runSQLCommand_characters_string("INSERT INTO " & targetStructure.character_inventory_tbl(0) & " ( " & targetStructure.invent_guid_col(0) & ", " & targetStructure.invent_bag_col(0) & ", `" &
                                            targetStructure.invent_slot_col(0) & "`, `" & targetStructure.invent_item_col(0) & "` ) VALUES ( '" & newcharguid.ToString() & "', '0', '23', '" & newitemguid.ToString() & "')", True)
            'Set home
            LogAppend("Setting character homebind", "CharacterCreationAdvanced_createAtTrinity", False)
            Dim tmpstring As String = player.HomeBind
            runSQLCommand_characters_string("INSERT INTO " & targetStructure.character_homebind_tbl(0) & " ( " & targetStructure.homebind_guid_col(0) & ", " & targetStructure.homebind_map_col(0) & ", " &
                                            targetStructure.homebind_zone_col(0) & ", " & targetStructure.char_posX_col(0) & ", " & targetStructure.char_posY_col(0) & ", " & targetStructure.char_posZ_col(0) &
                                            " ) VALUES ( '" & newcharguid.ToString() & "', '" & splitList(tmpstring, "map") & "', '" & splitList(tmpstring, "zone") & "', '" & splitList(tmpstring, "position_x") &
                                            "', '" & splitList(tmpstring, "position_y") & "', '" & splitList(tmpstring, "position_z") & "' )")
        Catch ex As Exception
            LogAppend("Something went wrong while creating the account -> Skipping! -> Error message is: " & ex.ToString(), "CharacterCreationAdvanced_createAtTrinity", False, True)
        End Try
        characterGUID = newcharguid
    End Sub
    Private Sub createAtMangos(ByVal charactername As String, ByVal accid As Integer, ByVal targetSetId As Integer, ByVal NameChange As Boolean)
        LogAppend("Creating at Mangos", "CharacterCreationAdvanced_createAtMangos", False)
        Dim newcharguid As Integer = TryInt(runSQLCommand_characters_string("SELECT " & targetStructure.char_guid_col(0) & " FROM " & targetStructure.character_tbl(0) & " WHERE " & targetStructure.char_guid_col(0) &
                                                                            "=(SELECT MAX(" & targetStructure.char_guid_col(0) & ") FROM " & targetStructure.character_tbl(0) & ")", True)) + 1
        ' Dim accid As Integer = TryInt(runSQLCommand_realm_string("SELECT `" & targetStructure.acc_id_col(0) & "` FROM " & targetStructure.account_tbl(0) & " WHERE " & targetStructure.acc_name_col(0) & "='" & accname & "'", True))
        Dim sqlstring As String = "INSERT INTO " & targetStructure.character_tbl(0) & " ( `" & targetStructure.char_guid_col(0) & "`, `" & targetStructure.char_accountId_col(0) & "`, `" & targetStructure.char_name_col(0) &
            "`, `" & targetStructure.char_race_col(0) & "`, `" & targetStructure.char_class_col(0) & "`, `" & targetStructure.char_gender_col(0) & "`, `" & targetStructure.char_level_col(0) & "`, `" &
            targetStructure.char_xp_col(0) & "`, `" & targetStructure.char_gold_col(0) & "`, `" & targetStructure.char_playerBytes_col(0) & "`, `" & targetStructure.char_playerBytes2_col(0) & "`, " &
            "`" & targetStructure.char_playerFlags_col(0) & "`, `" & targetStructure.char_posX_col(0) & "`, " & targetStructure.char_posY_col(0) & ", " & targetStructure.char_posZ_col(0) & ", " &
            targetStructure.char_map_col(0) & ", " & targetStructure.char_orientation_col(0) & ", " & targetStructure.char_taximask_col(0) & ", " & targetStructure.char_cinematic_col(0) & ", " &
            targetStructure.char_totaltime_col(0) & ", " & targetStructure.char_leveltime_col(0) & ", " & targetStructure.char_extraFlags_col(0) & ", " & targetStructure.char_stableSlots_col(0) & ", " &
            targetStructure.char_atlogin_col(0) & ", " & targetStructure.char_zone_col(0) & ", " & targetStructure.char_chosenTitle_col(0) & ", " &
            "" & targetStructure.char_knownCurrencies_col(0) & ", " & targetStructure.char_watchedFaction_col(0) & ", `" & targetStructure.char_health_col(0) & "`, " & targetStructure.char_speccount_col(0) & ", " &
            targetStructure.char_activeSpec_col(0) & ", " & targetStructure.char_exploredZones_col(0) & ", " & targetStructure.char_knownTitles_col(0) & ", " & targetStructure.char_actionBars_col(0) & " ) VALUES " &
            "( @guid, @accid, @name, @race, @class, @gender, @level, @xp, @gold, @pBytes, @pBytes2, @pFlags, @posx, @posy, @posz, @map, '4,40671', @taxi, '1', @totaltime, leveltime, @extraflags, " &
            "@stable, @login, @zone, @title, @knownCurrencies, @wFaction, '5000', @speccount, @activespec, @exploredZones, @knownTitles, @action )"
        Dim tempcommand As New MySqlCommand(sqlstring, TargetConnection)
        Dim player As Character = GetCharacterSetBySetId(targetSetId)
        tempcommand.Parameters.AddWithValue("@accid", accid.ToString())
        tempcommand.Parameters.AddWithValue("@guid", newcharguid.ToString())
        tempcommand.Parameters.AddWithValue("@name", charactername)
        tempcommand.Parameters.AddWithValue("@class", player.Cclass.ToString())
        tempcommand.Parameters.AddWithValue("@race", player.race.tostring())
        tempcommand.Parameters.AddWithValue("@gender", player.gender.tostring())
        tempcommand.Parameters.AddWithValue("@level", player.level.tostring())
        tempcommand.Parameters.AddWithValue("@xp", player.Xp.ToString())
        tempcommand.Parameters.AddWithValue("@gold", player.Gold)
        tempcommand.Parameters.AddWithValue("@pBytes", player.playerBytes.tostring())
        tempcommand.Parameters.AddWithValue("@pBytes2", player.playerBytes2.tostring())
        tempcommand.Parameters.AddWithValue("@pFlags", player.playerFlags.tostring())
        tempcommand.Parameters.AddWithValue("@posx", player.PositionX.ToString())
        tempcommand.Parameters.AddWithValue("@posy", player.PositionY.ToString())
        tempcommand.Parameters.AddWithValue("@posz", (player.PositionZ + 1).ToString())
        tempcommand.Parameters.AddWithValue("@map", player.map.tostring())
        tempcommand.Parameters.AddWithValue("@taxi", player.Taximask)
        tempcommand.Parameters.AddWithValue("@stable", player.stableSlots.tostring())
        tempcommand.Parameters.AddWithValue("@totaltime", player.TotalTime)
        tempcommand.Parameters.AddWithValue("@leveltime", player.leveltime.tostring())
        tempcommand.Parameters.AddWithValue("@extraflags", player.extraFlags.tostring())
        tempcommand.Parameters.AddWithValue("@login", player.atlogin.tostring())
        tempcommand.Parameters.AddWithValue("@zone", player.zone.tostring())
        tempcommand.Parameters.AddWithValue("@knownCurrencies", player.knownCurrencies.tostring())
        tempcommand.Parameters.AddWithValue("@action", player.ActionBars.ToString())
        tempcommand.Parameters.AddWithValue("@title", player.chosenTitle.tostring())
        tempcommand.Parameters.AddWithValue("@wFaction", player.watchedFaction.tostring())
        tempcommand.Parameters.AddWithValue("@speccpunt", player.speccount.tostring())
        tempcommand.Parameters.AddWithValue("@activespec", player.activespec.tostring())
        tempcommand.Parameters.AddWithValue("@exploredZones", player.ExploredZones)
        tempcommand.Parameters.AddWithValue("@knownTitles", player.KnownTitles)
        Dim m_charCreationLite As New CharacterCreationLite
        Try
            tempcommand.ExecuteNonQuery()
            If NameChange = True Then
                runSQLCommand_characters_string("UPDATE " & targetStructure.character_tbl(0) & " SET " & targetStructure.char_atlogin_col(0) & "='1' WHERE " & targetStructure.char_guid_col(0) &
                                                "='" & newcharguid.ToString & "'", True)
            Else
                If m_charCreationLite.CharacterExist(charactername) = True Then
                    runSQLCommand_characters_string("UPDATE " & targetStructure.character_tbl(0) & " SET " & targetStructure.char_atlogin_col(0) & "='1' WHERE " & targetStructure.char_guid_col(0) & "='" &
                                                    newcharguid.ToString & "'", True)
                End If
            End If
            'Creating hearthstone
            LogAppend("Creating character hearthstone", "CharacterCreationAdvanced_createAtArcemu", False)
            Dim newitemguid As Integer = (TryInt(runSQLCommand_characters_string("SELECT " & targetStructure.itmins_guid_col(0) & " FROM " & targetStructure.item_instance_tbl(0) & " WHERE " &
                                                                                 targetStructure.itmins_guid_col(0) & "=(SELECT MAX(" & targetStructure.itmins_guid_col(0) & ") FROM " &
                                                                                 targetStructure.item_instance_tbl(0) & "))", True)) + 1)
            If targetExpansion >= 3 Then
                runSQLCommand_characters_string("INSERT INTO " & targetStructure.item_instance_tbl(0) & " ( " & targetStructure.itmins_guid_col(0) & ", " & targetStructure.itmins_ownerGuid_col(0) & ", " &
                                                targetStructure.itmins_data_col(0) & " ) VALUES ( '" & newitemguid.ToString() & "', '" & newcharguid.ToString() & "', '" & newitemguid.ToString() &
                                                " 1191182336 3 6948 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ' )", True)
            Else
                'MaNGOS < 3.3 Core: watch data length
                runSQLCommand_characters_string(
                    "INSERT INTO " & targetStructure.item_instance_tbl(0) & " ( " & targetStructure.itmins_guid_col(0) & ", " & targetStructure.itmins_ownerGuid_col(0) & ", " & targetStructure.itmins_data_col(0) &
                    " ) VALUES ( '" & newitemguid.ToString() & "', '" & accid.ToString() & "', '" & newitemguid.ToString() &
                    " 1191182336 3 6948 1065353216 0 8 0 8 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ' )", True)
            End If
            runSQLCommand_characters_string("INSERT INTO " & targetStructure.character_inventory_tbl(0) & " ( " & targetStructure.invent_guid_col(0) & ", " & targetStructure.invent_bag_col(0) & ", " &
                                            targetStructure.invent_slot_col(0) & ", " & targetStructure.invent_item_col(0) & ", " & targetStructure.invent_item_template_col(0) &
                                            " ) VALUES ( '" & accid.ToString() & "', '0', '23', '" & newitemguid.ToString() & "', '6948')")
            'Set home
            LogAppend("Setting character homebind", "CharacterCreationAdvanced_createAtMangos", False)
            Dim tmpstring As String = player.HomeBind
            runSQLCommand_characters_string("INSERT INTO " & targetStructure.character_homebind_tbl(0) & " ( " & targetStructure.homebind_guid_col(0) & ", " & targetStructure.homebind_map_col(0) & ", " &
                                            targetStructure.homebind_zone_col(0) & ", " & targetStructure.homebind_posx_col(0) & ", " & targetStructure.char_posY_col(0) & ", " & targetStructure.char_posZ_col(0) &
                                            " ) VALUES ( '" & newcharguid.ToString() & "', '" & splitList(tmpstring, "map") & "', '" & splitList(tmpstring, "zone") &
            "', '" & splitList(tmpstring, "position_x") & "', '" & splitList(tmpstring, "position_y") & "', '" & splitList(tmpstring, "position_z") & "' )")
        Catch ex As Exception
            LogAppend("Something went wrong while creating the account -> Skipping! -> Error message is: " & ex.ToString(), "CharacterCreationAdvanced_createAtMangos", False, True)
        End Try
        characterGUID = newcharguid
    End Sub
End Class
