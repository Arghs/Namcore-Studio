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

Imports Namcore_Studio.EventLogging
Imports Namcore_Studio.Basics
Imports Namcore_Studio.GlobalVariables
Imports Namcore_Studio.Conversions
Imports Namcore_Studio.CommandHandler
Imports Namcore_Studio.SpellCreation
Imports Namcore_Studio.SkillCreation
Imports Namcore_Studio.CharacterCreationLite
Imports MySql.Data.MySqlClient
Public Class CharacterCreationAdvanced
    Public Shared Sub CreateNewAdvancedCharacter(ByVal charname As String, ByVal accountName As String, ByVal setId As Integer, Optional forceNameChange As Boolean = False)
        LogAppend("Creating new character: " & charname & " for account : " & accountName, "CharacterCreationAdvanced_CreateNewAdvancedCharacter", True)
        Select Case targetCore
            Case "arcemu"
                createAtArcemu(charname, accountName, setId, forceNameChange)
            Case "trinity"
                createAtTrinity(charname, accountName, setId, forceNameChange)
            Case "trinitytbc"

            Case "mangos"
                createAtMangos(charname, accountName, setId, forceNameChange)
            Case Else

        End Select
    End Sub

    Private Shared Sub createAtArcemu(ByVal charactername As String, ByVal accname As String, ByVal targetSetId As Integer, ByVal NameChange As Boolean)
        LogAppend("Creating at arcemu", "CharacterCreationAdvanced_createAtArcemu", False)
        Dim newcharguid As Integer = TryInt(runSQLCommand_characters_string("SELECT " & sourceStructure.char_guid_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) &
                                                                            "=(SELECT MAX(" & sourceStructure.char_guid_col(0) & ") FROM " & sourceStructure.character_tbl(0) & ")", True)) + 1
        Dim accid As Integer = TryInt(runSQLCommand_realm_string("SELECT " & sourceStructure.acc_id_col(0) & " FROM " & sourceStructure.account_tbl(0) & " WHERE " & sourceStructure.acc_name_col(0) & "='" & accname & "'", True))
        Dim sqlstring As String = "INSERT INTO " & sourceStructure.character_tbl(0) & " ( `" & sourceStructure.char_guid_col(0) & "`, `" & sourceStructure.char_accountId_col(0) & "`, `" & sourceStructure.char_name_col(0) &
            "`, `" & sourceStructure.char_race_col(0) & "`, `" & sourceStructure.char_class_col(0) & "`, `" & sourceStructure.char_gender_col(0) & "`, `" & sourceStructure.char_level_col(0) & "`, `" &
            sourceStructure.char_xp_col(0) & "`, `" & sourceStructure.char_gold_col(0) & "`, `" & sourceStructure.char_playerBytes_col(0) & "`, `" & sourceStructure.char_playerBytes2_col(0) & "`, `" &
            sourceStructure.char_playerFlags_col(0) & "`, `" & sourceStructure.char_posX_col(0) & "`, " & "" & sourceStructure.char_posY_col(0) & ", " & sourceStructure.char_posZ_col(0) & ", " & sourceStructure.char_map_col(0) & ", " & sourceStructure.char_orientation_col(0) & ", " & sourceStructure.char_taximask_col(0) & ", " & sourceStructure.char_totaltime_col(0) & ", " & sourceStructure.char_stableSlots_col(0) & ", " & sourceStructure.char_zone_col(0) & ", " & sourceStructure.char_watchedFaction_col(0) & ", current_hp, " &
                                    "" & sourceStructure.char_speccount_col(0) & ", " & sourceStructure.char_activeSpec_col(0) & ", " & sourceStructure.char_exploredZones_col(0) & ", " & sourceStructure.char_knownTitles_col(0) & " ) VALUES " &
                                    "( @guid, @accid, @name, '0', '0', '0', '1', '0', '0', @pBytes, pBytes2, @pFlags, @posx, @posy, @posz, @map, '4,40671', @taxi, '0 0 0 ', @stable, @zone, " &
                                    "@title, @wFaction, '1000', @speccpunt, @activespec, @exploredZones, @knownTitles )"
        Dim tempcommand As New MySqlCommand(sqlstring, TargetConnection_Realm)
        tempcommand.Parameters.AddWithValue("@accid", accid.ToString())
        tempcommand.Parameters.AddWithValue("@guid", newcharguid.ToString())
        tempcommand.Parameters.AddWithValue("@name", charactername)
        tempcommand.Parameters.AddWithValue("@class", GetTemporaryCharacterInformation("@character_class", targetSetId))
        tempcommand.Parameters.AddWithValue("@race", GetTemporaryCharacterInformation("@character_race", targetSetId))
        tempcommand.Parameters.AddWithValue("@gender", GetTemporaryCharacterInformation("@character_gender", targetSetId))
        tempcommand.Parameters.AddWithValue("@level", GetTemporaryCharacterInformation("@character_level", targetSetId))
        tempcommand.Parameters.AddWithValue("@pBytes", GetTemporaryCharacterInformation("@character_playerBytes", targetSetId))
        tempcommand.Parameters.AddWithValue("@pBytes2", GetTemporaryCharacterInformation("@character_playerBytes2", targetSetId))
        tempcommand.Parameters.AddWithValue("@pFlags", GetTemporaryCharacterInformation("@character_playerFlags", targetSetId))
        tempcommand.Parameters.AddWithValue("@posx", GetTemporaryCharacterInformation("@character_posX", targetSetId))
        tempcommand.Parameters.AddWithValue("@posy", GetTemporaryCharacterInformation("@character_posY", targetSetId))
        tempcommand.Parameters.AddWithValue("@posz", (TryInt(GetTemporaryCharacterInformation("@character_posZ", targetSetId)) + 1).ToString)
        tempcommand.Parameters.AddWithValue("@map", GetTemporaryCharacterInformation("@character_map", targetSetId))
        tempcommand.Parameters.AddWithValue("@taxi", GetTemporaryCharacterInformation("@character_taximask", targetSetId))
        tempcommand.Parameters.AddWithValue("@stable", GetTemporaryCharacterInformation("@character_stableSlots", targetSetId))
        tempcommand.Parameters.AddWithValue("@zone", GetTemporaryCharacterInformation("@character_zone", targetSetId))
        tempcommand.Parameters.AddWithValue("@wFaction", GetTemporaryCharacterInformation("@character_watchedFaction", targetSetId))
        tempcommand.Parameters.AddWithValue("@speccpunt", GetTemporaryCharacterInformation("@character_speccount", targetSetId))
        tempcommand.Parameters.AddWithValue("@activespec", GetTemporaryCharacterInformation("@character_activespec", targetSetId))
        tempcommand.Parameters.AddWithValue("@exploredZones", GetTemporaryCharacterInformation("@character_exploredZones", targetSetId))
        tempcommand.Parameters.AddWithValue("@knownTitles", GetTemporaryCharacterInformation("@character_knownTitles", targetSetId))
        'PlayerBytes column might not be correct! check player_bytes, bytes, bytes2
        Try
            tempcommand.ExecuteNonQuery()
            If NameChange = True Then
                runSQLCommand_characters_string("UPDATE " & sourceStructure.character_tbl(0) & " SET forced_rename_pending='1' WHERE " & sourceStructure.char_guid_col(0) & "='" & newcharguid.ToString & "'", True)
            Else
                If CharacterExist(charactername) = True Then
                    runSQLCommand_characters_string("UPDATE " & sourceStructure.character_tbl(0) & " SET forced_rename_pending='1' WHERE " & sourceStructure.char_guid_col(0) & "='" & newcharguid.ToString & "'", True)
                End If
            End If
            'Creating hearthstone
            LogAppend("Creating character hearthstone", "CharacterCreationAdvanced_createAtArcemu", False)
            Dim newitemguid As Integer = (TryInt(runSQLCommand_characters_string("SELECT " & sourceStructure.char_guid_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " & sourceStructure.itmins_guid_col(0) &
                                                                                 "=(SELECT MAX(" & sourceStructure.itmins_guid_col(0) & ") FROM " & sourceStructure.item_instance_tbl(0) & ")", True)) + 5)
            runSQLCommand_characters_string("INSERT INTO " & sourceStructure.item_instance_tbl(0) & " ( " & sourceStructure.itmins_ownerGuid_col(0) & ", " & sourceStructure.itmins_guid_col(0) &
                                            ", " & sourceStructure.itmins_itemEntry_col(0) & ", flags, " & sourceStructure.itmins_container_col(0) & ", " & sourceStructure.itmins_slot_col(0) &
                                            " ) VALUES ( '" & newcharguid.ToString() &
                "', '" & newitemguid.ToString() & "', '6948', '1', '-1', '23' )", True)
            AddSpells("6603,")
            Dim tempinfo As String = GetTemporaryCharacterInformation("@character_customFaction", targetSetId)
            If Not tempinfo = "" Then runSQLCommand_characters_string("UPDATE " & sourceStructure.character_tbl(0) & " SET " & sourceStructure.char_customFaction_col(0) & "='" & tempinfo &
                "' WHERE " & sourceStructure.char_guid_col(0) & "='" & newcharguid.ToString & "'")
            'Setting tutorials
            runSQLCommand_characters_string("INSERT INTO `tutorials` ( playerId ) VALUES ( " & accid.ToString() & " )", True)
            'Set home
            LogAppend("Setting character homebind", "CharacterCreationAdvanced_createAtArcemu", False)
            Dim tmpstring As String = GetTemporaryCharacterInformation("@character_homebind", targetSetId)
            runSQLCommand_characters_string("UPDATE " & sourceStructure.character_tbl(0) & " SET " & sourceStructure.char_bindposX_col(0) & "='" & splitList(tmpstring, "position_x") &
                                            "' WHERE " & sourceStructure.char_guid_col(0) & "='" & newcharguid.ToString() & "'")
            runSQLCommand_characters_string("UPDATE " & sourceStructure.character_tbl(0) & " SET " & sourceStructure.char_bindposY_col(0) & "='" & splitList(tmpstring, "position_y") &
                                            "' WHERE " & sourceStructure.char_guid_col(0) & "='" & newcharguid.ToString() & "'")
            runSQLCommand_characters_string("UPDATE " & sourceStructure.character_tbl(0) & " SET " & sourceStructure.char_bindposZ_col(0) & "='" & splitList(tmpstring, "position_z") &
                                            "' WHERE " & sourceStructure.char_guid_col(0) & "='" & newcharguid.ToString() & "'")
            runSQLCommand_characters_string("UPDATE " & sourceStructure.character_tbl(0) & " SET " & sourceStructure.char_bindmapid_col(0) & "='" & splitList(tmpstring, "map") &
                                            "' WHERE " & sourceStructure.char_guid_col(0) & "='" & newcharguid.ToString() & "'")
            runSQLCommand_characters_string("UPDATE " & sourceStructure.character_tbl(0) & " SET " & sourceStructure.char_bindzoneid_col(0) & "='" & splitList(tmpstring, "zone") &
                                            "' WHERE " & sourceStructure.char_guid_col(0) & "='" & newcharguid.ToString() & "'")
        Catch ex As Exception
            LogAppend("Something went wrong while creating the account -> Skipping! -> Error message is: " & ex.ToString(), "CharacterCreationAdvanced_createAtArcemu", False, True)
        End Try
    End Sub
    Private Shared Sub createAtTrinity(ByVal charactername As String, ByVal accname As String, ByVal targetSetId As Integer, ByVal NameChange As Boolean)
        LogAppend("Creating at Trinity", "CharacterCreationAdvanced_createAtTrinity", False)
        Dim newcharguid As Integer = TryInt(runSQLCommand_characters_string("SELECT " & sourceStructure.char_guid_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) &
                                                                            "=(SELECT MAX(" & sourceStructure.char_guid_col(0) & ") FROM " & sourceStructure.character_tbl(0) & ")", True)) + 1
        Dim accid As Integer = TryInt(runSQLCommand_realm_string("SELECT `" & sourceStructure.acc_id_col(0) & "` FROM " & sourceStructure.account_tbl(0) & " WHERE " & sourceStructure.acc_name_col(0) & "='" & accname & "'", True))
        Dim sqlstring As String = "INSERT INTO " & sourceStructure.character_tbl(0) & " ( `" & sourceStructure.char_guid_col(0) & "`, `" & sourceStructure.char_accountId_col(0) & "`, `" & sourceStructure.char_name_col(0) &
            "`, `" & sourceStructure.char_race_col(0) & "`, `" & sourceStructure.char_class_col(0) & "`, `" & sourceStructure.char_gender_col(0) & "`, `" & sourceStructure.char_level_col(0) & "`, `" &
            sourceStructure.char_xp_col(0) & "`, `" & sourceStructure.char_gold_col(0) & "`, `" & sourceStructure.char_playerBytes_col(0) & "`, `" & sourceStructure.char_playerBytes2_col(0) & "`, " &
            "`" & sourceStructure.char_playerFlags_col(0) & "`, `" & sourceStructure.char_posX_col(0) & "`, " & sourceStructure.char_posY_col(0) & ", " & sourceStructure.char_posZ_col(0) & ", " &
            sourceStructure.char_map_col(0) & ", " & sourceStructure.char_orientation_col(0) & ", " & sourceStructure.char_taximask_col(0) & ", " & sourceStructure.char_cinematic_col(0) & ", " &
            sourceStructure.char_totaltime_col(0) & ", " & sourceStructure.char_leveltime_col(0) & ", " & sourceStructure.char_extraFlags_col(0) & ", " & sourceStructure.char_stableSlots_col(0) & ", " &
            sourceStructure.char_atlogin_col(0) & ", " & sourceStructure.char_zone_col(0) & ", " & sourceStructure.char_chosenTitle_col(0) & ", " &
            "" & sourceStructure.char_knownCurrencies_col(0) & ", " & sourceStructure.char_watchedFaction_col(0) & ", `" & sourceStructure.char_health_col(0) & "`, " & sourceStructure.char_speccount_col(0) & ", " &
            sourceStructure.char_activeSpec_col(0) & ", " & sourceStructure.char_exploredZones_col(0) & ", " & sourceStructure.char_knownTitles_col(0) & ", " & sourceStructure.char_actionBars_col(0) & " ) VALUES " &
            "( @guid, @accid, @name, '0', '0', '0', '1', '0', '0', @pBytes, @pBytes2, @pFlags, @posx, @posy, @posz, @map, '4,40671', @taxi, '1', @totaltime, leveltime, @extraflags, " &
            "@stable, @login, @zone, @title, @knownCurrencies, @wFaction, '5000', @speccount, @activespec, @exploredZones, @knownTitles, @action )"
        Dim tempcommand As New MySqlCommand(sqlstring, TargetConnection_Realm)
        tempcommand.Parameters.AddWithValue("@accid", accid.ToString())
        tempcommand.Parameters.AddWithValue("@guid", newcharguid.ToString())
        tempcommand.Parameters.AddWithValue("@name", charactername)
        tempcommand.Parameters.AddWithValue("@class", GetTemporaryCharacterInformation("@character_class", targetSetId))
        tempcommand.Parameters.AddWithValue("@race", GetTemporaryCharacterInformation("@character_race", targetSetId))
        tempcommand.Parameters.AddWithValue("@gender", GetTemporaryCharacterInformation("@character_gender", targetSetId))
        tempcommand.Parameters.AddWithValue("@level", GetTemporaryCharacterInformation("@character_level", targetSetId))
        tempcommand.Parameters.AddWithValue("@pBytes", GetTemporaryCharacterInformation("@character_playerBytes", targetSetId))
        tempcommand.Parameters.AddWithValue("@pBytes2", GetTemporaryCharacterInformation("@character_playerBytes2", targetSetId))
        tempcommand.Parameters.AddWithValue("@pFlags", GetTemporaryCharacterInformation("@character_playerFlags", targetSetId))
        tempcommand.Parameters.AddWithValue("@posx", GetTemporaryCharacterInformation("@character_posX", targetSetId))
        tempcommand.Parameters.AddWithValue("@posy", GetTemporaryCharacterInformation("@character_posY", targetSetId))
        tempcommand.Parameters.AddWithValue("@posz", (TryInt(GetTemporaryCharacterInformation("@character_posZ", targetSetId)) + 1).ToString)
        tempcommand.Parameters.AddWithValue("@map", GetTemporaryCharacterInformation("@character_map", targetSetId))
        tempcommand.Parameters.AddWithValue("@taxi", GetTemporaryCharacterInformation("@character_taximask", targetSetId))
        tempcommand.Parameters.AddWithValue("@stable", GetTemporaryCharacterInformation("@character_stableSlots", targetSetId))
        tempcommand.Parameters.AddWithValue("@totaltime", GetTemporaryCharacterInformation("@character_totaltime", targetSetId))
        tempcommand.Parameters.AddWithValue("@leveltime", GetTemporaryCharacterInformation("@character_leveltime", targetSetId))
        tempcommand.Parameters.AddWithValue("@extraflags", GetTemporaryCharacterInformation("@character_extraFlags", targetSetId))
        tempcommand.Parameters.AddWithValue("@login", GetTemporaryCharacterInformation("@character_atlogin", targetSetId))
        tempcommand.Parameters.AddWithValue("@zone", GetTemporaryCharacterInformation("@character_zone", targetSetId))
        tempcommand.Parameters.AddWithValue("@knownCurrencies", GetTemporaryCharacterInformation("@character_knownCurrencies", targetSetId))
        tempcommand.Parameters.AddWithValue("@action", GetTemporaryCharacterInformation("@character_action", targetSetId))
        tempcommand.Parameters.AddWithValue("@title", GetTemporaryCharacterInformation("@character_chosenTitle", targetSetId))
        tempcommand.Parameters.AddWithValue("@wFaction", GetTemporaryCharacterInformation("@character_watchedFaction", targetSetId))
        tempcommand.Parameters.AddWithValue("@speccpunt", GetTemporaryCharacterInformation("@character_speccount", targetSetId))
        tempcommand.Parameters.AddWithValue("@activespec", GetTemporaryCharacterInformation("@character_activespec", targetSetId))
        tempcommand.Parameters.AddWithValue("@exploredZones", GetTemporaryCharacterInformation("@character_exploredZones", targetSetId))
        tempcommand.Parameters.AddWithValue("@knownTitles", GetTemporaryCharacterInformation("@character_knownTitles", targetSetId))
        Try
            tempcommand.ExecuteNonQuery()
            If NameChange = True Then
                runSQLCommand_characters_string("UPDATE " & sourceStructure.character_tbl(0) & " SET " & sourceStructure.char_atlogin_col(0) & "='1' WHERE " & sourceStructure.char_guid_col(0) & "='" &
                                                newcharguid.ToString & "'", True)
            Else
                If CharacterExist(charactername) = True Then
                    runSQLCommand_characters_string("UPDATE " & sourceStructure.character_tbl(0) & " SET " & sourceStructure.char_atlogin_col(0) & "='1' WHERE " & sourceStructure.char_guid_col(0) & "='" &
                                                    newcharguid.ToString & "'", True)
                End If
            End If
            'Creating hearthstone
            LogAppend("Creating character hearthstone", "CharacterCreationAdvanced_createAtArcemu", False)
            Dim newitemguid As Integer = (TryInt(runSQLCommand_characters_string("SELECT " & sourceStructure.itmins_guid_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " &
                                                                                 sourceStructure.itmins_guid_col(0) & "=(SELECT MAX(" & sourceStructure.itmins_guid_col(0) & ") FROM " &
                                                                                 sourceStructure.item_instance_tbl(0) & ")", True)) + 1)
            runSQLCommand_characters_string("INSERT INTO " & sourceStructure.item_instance_tbl(0) & " ( " & sourceStructure.itmins_guid_col(0) & ", " & sourceStructure.itmins_itemEntry_col(0) & ", " &
                                            sourceStructure.itmins_ownerGuid_col(0) & ", " & sourceStructure.itmins_count_col(0) & ", charges, " & sourceStructure.itmins_enchantments_col(0) & ", " &
                                            sourceStructure.itmins_durability_col(0) & " ) VALUES ( '" & newitemguid.ToString & "', '6948', '" & accid.ToString() & "', '1', '0 0 0 0 0 ', '" & newitemguid.ToString() &
               " 1191182336 3 6948 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )", True)
            runSQLCommand_characters_string("INSERT INTO " & sourceStructure.character_inventory_tbl(0) & " ( " & sourceStructure.invent_guid_col(0) & ", " & sourceStructure.invent_bag_col(0) & ", `" &
                                            sourceStructure.invent_slot_col(0) & "`, `" & sourceStructure.invent_item_col(0) & "` ) VALUES ( '" & newcharguid.ToString() & "', '0', '23', '" & newitemguid.ToString() & "')", True)
            'Set home
            LogAppend("Setting character homebind", "CharacterCreationAdvanced_createAtTrinity", False)
            Dim tmpstring As String = GetTemporaryCharacterInformation("@character_homebind", targetSetId)
            runSQLCommand_characters_string("INSERT INTO " & sourceStructure.character_homebind_tbl(0) & " ( " & sourceStructure.homebind_guid_col(0) & ", " & sourceStructure.homebind_map_col(0) & ", " &
                                            sourceStructure.homebind_zone_col(0) & ", " & sourceStructure.char_posX_col(0) & ", " & sourceStructure.char_posY_col(0) & ", " & sourceStructure.char_posZ_col(0) &
                                            " ) VALUES ( '" & newcharguid.ToString() & "', '" & splitList(tmpstring, "map") & "', '" & splitList(tmpstring, "zone") & "', '" & splitList(tmpstring, "position_x") &
                                            "', '" & splitList(tmpstring, "position_y") & "', '" & splitList(tmpstring, "position_z") & "' )")
        Catch ex As Exception
            LogAppend("Something went wrong while creating the account -> Skipping! -> Error message is: " & ex.ToString(), "CharacterCreationAdvanced_createAtTrinity", False, True)
        End Try
    End Sub
    Private Shared Sub createAtMangos(ByVal charactername As String, ByVal accname As String, ByVal targetSetId As Integer, ByVal NameChange As Boolean)
        LogAppend("Creating at Mangos", "CharacterCreationAdvanced_createAtMangos", False)
        Dim newcharguid As Integer = TryInt(runSQLCommand_characters_string("SELECT " & sourceStructure.char_guid_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) &
                                                                            "=(SELECT MAX(" & sourceStructure.char_guid_col(0) & ") FROM " & sourceStructure.character_tbl(0) & ")", True)) + 1
        Dim accid As Integer = TryInt(runSQLCommand_realm_string("SELECT `" & sourceStructure.acc_id_col(0) & "` FROM " & sourceStructure.account_tbl(0) & " WHERE " & sourceStructure.acc_name_col(0) & "='" & accname & "'", True))
        Dim sqlstring As String = "INSERT INTO " & sourceStructure.character_tbl(0) & " ( `" & sourceStructure.char_guid_col(0) & "`, `" & sourceStructure.char_accountId_col(0) & "`, `" & sourceStructure.char_name_col(0) &
            "`, `" & sourceStructure.char_race_col(0) & "`, `" & sourceStructure.char_class_col(0) & "`, `" & sourceStructure.char_gender_col(0) & "`, `" & sourceStructure.char_level_col(0) & "`, `" &
            sourceStructure.char_xp_col(0) & "`, `" & sourceStructure.char_gold_col(0) & "`, `" & sourceStructure.char_playerBytes_col(0) & "`, `" & sourceStructure.char_playerBytes2_col(0) & "`, " &
            "`" & sourceStructure.char_playerFlags_col(0) & "`, `" & sourceStructure.char_posX_col(0) & "`, " & sourceStructure.char_posY_col(0) & ", " & sourceStructure.char_posZ_col(0) & ", " &
            sourceStructure.char_map_col(0) & ", " & sourceStructure.char_orientation_col(0) & ", " & sourceStructure.char_taximask_col(0) & ", " & sourceStructure.char_cinematic_col(0) & ", " &
            sourceStructure.char_totaltime_col(0) & ", " & sourceStructure.char_leveltime_col(0) & ", " & sourceStructure.char_extraFlags_col(0) & ", " & sourceStructure.char_stableSlots_col(0) & ", " &
            sourceStructure.char_atlogin_col(0) & ", " & sourceStructure.char_zone_col(0) & ", " & sourceStructure.char_chosenTitle_col(0) & ", " &
            "" & sourceStructure.char_knownCurrencies_col(0) & ", " & sourceStructure.char_watchedFaction_col(0) & ", `" & sourceStructure.char_health_col(0) & "`, " & sourceStructure.char_speccount_col(0) & ", " &
            sourceStructure.char_activeSpec_col(0) & ", " & sourceStructure.char_exploredZones_col(0) & ", " & sourceStructure.char_knownTitles_col(0) & ", " & sourceStructure.char_actionBars_col(0) & " ) VALUES " &
            "( @guid, @accid, @name, '0', '0', '0', '1', '0', '0', @pBytes, @pBytes2, @pFlags, @posx, @posy, @posz, @map, '4,40671', @taxi, '1', @totaltime, leveltime, @extraflags, " &
            "@stable, @login, @zone, @title, @knownCurrencies, @wFaction, '5000', @speccount, @activespec, @exploredZones, @knownTitles, @action )"
        Dim tempcommand As New MySqlCommand(sqlstring, TargetConnection_Realm)
        tempcommand.Parameters.AddWithValue("@accid", accid.ToString())
        tempcommand.Parameters.AddWithValue("@guid", newcharguid.ToString())
        tempcommand.Parameters.AddWithValue("@name", charactername)
        tempcommand.Parameters.AddWithValue("@class", GetTemporaryCharacterInformation("@character_class", targetSetId))
        tempcommand.Parameters.AddWithValue("@race", GetTemporaryCharacterInformation("@character_race", targetSetId))
        tempcommand.Parameters.AddWithValue("@gender", GetTemporaryCharacterInformation("@character_gender", targetSetId))
        tempcommand.Parameters.AddWithValue("@level", GetTemporaryCharacterInformation("@character_level", targetSetId))
        tempcommand.Parameters.AddWithValue("@pBytes", GetTemporaryCharacterInformation("@character_playerBytes", targetSetId))
        tempcommand.Parameters.AddWithValue("@pBytes2", GetTemporaryCharacterInformation("@character_playerBytes2", targetSetId))
        tempcommand.Parameters.AddWithValue("@pFlags", GetTemporaryCharacterInformation("@character_playerFlags", targetSetId))
        tempcommand.Parameters.AddWithValue("@posx", GetTemporaryCharacterInformation("@character_posX", targetSetId))
        tempcommand.Parameters.AddWithValue("@posy", GetTemporaryCharacterInformation("@character_posY", targetSetId))
        tempcommand.Parameters.AddWithValue("@posz", (TryInt(GetTemporaryCharacterInformation("@character_posZ", targetSetId)) + 1).ToString)
        tempcommand.Parameters.AddWithValue("@map", GetTemporaryCharacterInformation("@character_map", targetSetId))
        tempcommand.Parameters.AddWithValue("@taxi", GetTemporaryCharacterInformation("@character_taximask", targetSetId))
        tempcommand.Parameters.AddWithValue("@stable", GetTemporaryCharacterInformation("@character_stableSlots", targetSetId))
        tempcommand.Parameters.AddWithValue("@totaltime", GetTemporaryCharacterInformation("@character_totaltime", targetSetId))
        tempcommand.Parameters.AddWithValue("@leveltime", GetTemporaryCharacterInformation("@character_leveltime", targetSetId))
        tempcommand.Parameters.AddWithValue("@extraflags", GetTemporaryCharacterInformation("@character_extraFlags", targetSetId))
        tempcommand.Parameters.AddWithValue("@login", GetTemporaryCharacterInformation("@character_atlogin", targetSetId))
        tempcommand.Parameters.AddWithValue("@zone", GetTemporaryCharacterInformation("@character_zone", targetSetId))
        tempcommand.Parameters.AddWithValue("@knownCurrencies", GetTemporaryCharacterInformation("@character_knownCurrencies", targetSetId))
        tempcommand.Parameters.AddWithValue("@action", GetTemporaryCharacterInformation("@character_action", targetSetId))
        tempcommand.Parameters.AddWithValue("@title", GetTemporaryCharacterInformation("@character_chosenTitle", targetSetId))
        tempcommand.Parameters.AddWithValue("@wFaction", GetTemporaryCharacterInformation("@character_watchedFaction", targetSetId))
        tempcommand.Parameters.AddWithValue("@speccpunt", GetTemporaryCharacterInformation("@character_speccount", targetSetId))
        tempcommand.Parameters.AddWithValue("@activespec", GetTemporaryCharacterInformation("@character_activespec", targetSetId))
        tempcommand.Parameters.AddWithValue("@exploredZones", GetTemporaryCharacterInformation("@character_exploredZones", targetSetId))
        tempcommand.Parameters.AddWithValue("@knownTitles", GetTemporaryCharacterInformation("@character_knownTitles", targetSetId))
        Try
            tempcommand.ExecuteNonQuery()
            If NameChange = True Then
                runSQLCommand_characters_string("UPDATE " & sourceStructure.character_tbl(0) & " SET " & sourceStructure.char_atlogin_col(0) & "='1' WHERE " & sourceStructure.char_guid_col(0) &
                                                "='" & newcharguid.ToString & "'", True)
            Else
                If CharacterExist(charactername) = True Then
                    runSQLCommand_characters_string("UPDATE " & sourceStructure.character_tbl(0) & " SET " & sourceStructure.char_atlogin_col(0) & "='1' WHERE " & sourceStructure.char_guid_col(0) & "='" &
                                                    newcharguid.ToString & "'", True)
                End If
            End If
            'Creating hearthstone
            LogAppend("Creating character hearthstone", "CharacterCreationAdvanced_createAtArcemu", False)
            Dim newitemguid As Integer = (TryInt(runSQLCommand_characters_string("SELECT " & sourceStructure.itmins_guid_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " &
                                                                                 sourceStructure.itmins_guid_col(0) & "=(SELECT MAX(" & sourceStructure.itmins_guid_col(0) & ") FROM " &
                                                                                 sourceStructure.item_instance_tbl(0) & "))", True)) + 1)
            If expansion >= 3 Then
                runSQLCommand_characters_string("INSERT INTO " & sourceStructure.item_instance_tbl(0) & " ( " & sourceStructure.itmins_guid_col(0) & ", " & sourceStructure.itmins_ownerGuid_col(0) & ", " &
                                                sourceStructure.itmins_data_col(0) & " ) VALUES ( '" & newitemguid.ToString() & "', '" & newcharguid.ToString() & "', '" & newitemguid.ToString() &
                                                " 1191182336 3 6948 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ' )", True)
            Else
                'MaNGOS < 3.3 Core: watch data length
                runSQLCommand_characters_string(
                    "INSERT INTO " & sourceStructure.item_instance_tbl(0) & " ( " & sourceStructure.itmins_guid_col(0) & ", " & sourceStructure.itmins_ownerGuid_col(0) & ", " & sourceStructure.itmins_data_col(0) &
                    " ) VALUES ( '" & newitemguid.ToString() & "', '" & accid.ToString() & "', '" & newitemguid.ToString() &
                    " 1191182336 3 6948 1065353216 0 8 0 8 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ' )", True)
            End If
            runSQLCommand_characters_string("INSERT INTO " & sourceStructure.character_inventory_tbl(0) & " ( " & sourceStructure.invent_guid_col(0) & ", " & sourceStructure.invent_bag_col(0) & ", " &
                                            sourceStructure.invent_slot_col(0) & ", " & sourceStructure.invent_item_col(0) & ", " & sourceStructure.invent_item_template_col(0) &
                                            " ) VALUES ( '" & accid.ToString() & "', '0', '23', '" & newitemguid.ToString() & "', '6948')")
            'Set home
            LogAppend("Setting character homebind", "CharacterCreationAdvanced_createAtMangos", False)
            Dim tmpstring As String = GetTemporaryCharacterInformation("@character_homebind", targetSetId)
            runSQLCommand_characters_string("INSERT INTO " & sourceStructure.character_homebind_tbl(0) & " ( " & sourceStructure.homebind_guid_col(0) & ", " & sourceStructure.homebind_map_col(0) & ", " &
                                            sourceStructure.homebind_zone_col(0) & ", " & sourceStructure.homebind_posx_col(0) & ", " & sourceStructure.char_posY_col(0) & ", " & sourceStructure.char_posZ_col(0) &
                                            " ) VALUES ( '" & newcharguid.ToString() & "', '" & splitList(tmpstring, "map") & "', '" & splitList(tmpstring, "zone") &
            "', '" & splitList(tmpstring, "position_x") & "', '" & splitList(tmpstring, "position_y") & "', '" & splitList(tmpstring, "position_z") & "' )")
        Catch ex As Exception
            LogAppend("Something went wrong while creating the account -> Skipping! -> Error message is: " & ex.ToString(), "CharacterCreationAdvanced_createAtMangos", False, True)
        End Try
    End Sub
End Class
