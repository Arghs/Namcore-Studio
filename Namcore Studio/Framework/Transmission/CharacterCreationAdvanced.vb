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
        Select Case sourceCore
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
        Dim newcharguid As Integer = TryInt(runSQLCommand_characters_string("SELECT guid FROM characters WHERE guid=(SELECT MAX(guid) FROM characters)", True)) + 1
        Dim accid As Integer = TryInt(runSQLCommand_realm_string("SELECT acct FROM accounts WHERE login='" & accname & "'", True))
        Const sqlstring As String = "INSERT INTO characters ( `guid`, `acct`, `name`, `race`, `class`, `gender`, `level`, `xp`, `gold`, `bytes`, `bytes2`, `player_flags`, `positionX`, " &
                                    "positionY, positionZ, mapId, orientation, taximask, playedtime, totalstableslots, zoneId, selected_pvp_title, watched_faction_index, current_hp, " &
                                    "numspecs, currentspec, exploration_data, available_pvp_titles, pl ) VALUES " &
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
        tempcommand.Parameters.AddWithValue("@title", GetTemporaryCharacterInformation("@character_chosenTitle", targetSetId))
        tempcommand.Parameters.AddWithValue("@wFaction", GetTemporaryCharacterInformation("@character_watchedFaction", targetSetId))
        tempcommand.Parameters.AddWithValue("@speccpunt", GetTemporaryCharacterInformation("@character_speccount", targetSetId))
        tempcommand.Parameters.AddWithValue("@activespec", GetTemporaryCharacterInformation("@character_activespec", targetSetId))
        tempcommand.Parameters.AddWithValue("@exploredZones", GetTemporaryCharacterInformation("@character_exploredZones", targetSetId))
        tempcommand.Parameters.AddWithValue("@knownTitles", GetTemporaryCharacterInformation("@character_knownTitles", targetSetId))
        'PlayerBytes column might not be correct! check player_bytes, bytes, bytes2
        Try
            tempcommand.ExecuteNonQuery()
            If NameChange = True Then
                runSQLCommand_characters_string("UPDATE characters SET forced_rename_pending='1' WHERE guid='" & newcharguid.ToString & "'", True)
            Else
                If CharacterExist(charactername) = True Then
                    runSQLCommand_characters_string("UPDATE characters SET forced_rename_pending='1' WHERE guid='" & newcharguid.ToString & "'", True)
                End If
            End If
            'Creating hearthstone
            LogAppend("Creating character hearthstone", "CharacterCreationAdvanced_createAtArcemu", False)
            Dim newitemguid As Integer = (TryInt(runSQLCommand_characters_string("SELECT guid FROM playeritems WHERE guid=(SELECT MAX(guid) FROM playeritems)", True)) + 5)
            runSQLCommand_characters_string("INSERT INTO playeritems ( ownerguid, guid, entry, flags, containerslot, slot ) VALUES ( '" & newcharguid.ToString() &
                "', '" & newitemguid.ToString() & "', '6948', '1', '-1', '23' )", True)
            AddSpells("6603,")
            Dim tempinfo As String = GetTemporaryCharacterInformation("@character_customFaction", targetSetId)
            If Not tempinfo = "" Then runSQLCommand_characters_string("UPDATE characters SET custom_faction='" & tempinfo & "' WHERE guid='" & newcharguid.ToString & "'")
            'Setting tutorials
            runSQLCommand_characters_string("INSERT INTO `tutorials` ( playerId ) VALUES ( " & accid.ToString() & " )", True)
            'Set home
            LogAppend("Setting character homebind", "CharacterCreationAdvanced_createAtArcemu", False)
            Dim tmpstring As String = GetTemporaryCharacterInformation("@character_homebind", targetSetId)
            runSQLCommand_characters_string("UPDATE characters SET bindpositionX='" & splitList(tmpstring, "position_x") & "' WHERE guid='" & newcharguid.ToString() & "'")
            runSQLCommand_characters_string("UPDATE characters SET bindpositionY='" & splitList(tmpstring, "position_y") & "' WHERE guid='" & newcharguid.ToString() & "'")
            runSQLCommand_characters_string("UPDATE characters SET bindpositionZ='" & splitList(tmpstring, "position_z") & "' WHERE guid='" & newcharguid.ToString() & "'")
            runSQLCommand_characters_string("UPDATE characters SET bindmapId='" & splitList(tmpstring, "map") & "' WHERE guid='" & newcharguid.ToString() & "'")
            runSQLCommand_characters_string("UPDATE characters SET bindzoneId='" & splitList(tmpstring, "zone") & "' WHERE guid='" & newcharguid.ToString() & "'")
        Catch ex As Exception
            LogAppend("Something went wrong while creating the account -> Skipping! -> Error message is: " & ex.ToString(), "CharacterCreationAdvanced_createAtArcemu", False, True)
        End Try
    End Sub
    Private Shared Sub createAtTrinity(ByVal charactername As String, ByVal accname As String, ByVal targetSetId As Integer, ByVal NameChange As Boolean)
        LogAppend("Creating at Trinity", "CharacterCreationAdvanced_createAtTrinity", False)
        Dim newcharguid As Integer = TryInt(runSQLCommand_characters_string("SELECT guid FROM characters WHERE guid=(SELECT MAX(guid) FROM characters)", True)) + 1
        Dim accid As Integer = TryInt(runSQLCommand_realm_string("SELECT `id` FROM account WHERE username='" & accname & "'", True))
        Const sqlstring As String = "INSERT INTO characters ( `guid`, `account`, `name`, `race`, `class`, `gender`, `level`, `xp`, `money`, `playerBytes`, `playerBytes2`, " &
            "`playerFlags`, `position_x`, position_y, position_z, map, orientation, taximask, cinematic, totaltime, leveltime, extra_flags, stable_slots, at_login, zone, chosenTitle, " &
            "knownCurrencies, watchedFaction, `health`, speccount, activespec, exploredZones, knownTitles, actionBars ) VALUES " &
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
                runSQLCommand_characters_string("UPDATE characters SET at_login='1' WHERE guid='" & newcharguid.ToString & "'", True)
            Else
                If CharacterExist(charactername) = True Then
                    runSQLCommand_characters_string("UPDATE characters SET at_login='1' WHERE guid='" & newcharguid.ToString & "'", True)
                End If
            End If
            'Creating hearthstone
            LogAppend("Creating character hearthstone", "CharacterCreationAdvanced_createAtArcemu", False)
            Dim newitemguid As Integer = (TryInt(runSQLCommand_characters_string("SELECT guid FROM item_instance WHERE guid=(SELECT MAX(guid) FROM item_instance)", True)) + 1)
            runSQLCommand_characters_string("INSERT INTO item_instance ( guid, itemEntry, owner_guid, count, charges, enchantments, durability ) VALUES ( '" &
               newitemguid.ToString & "', '6948', '" & accid.ToString() & "', '1', '0 0 0 0 0 ', '" & newitemguid.ToString() &
               " 1191182336 3 6948 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )", True)
            runSQLCommand_characters_string("INSERT INTO character_inventory ( guid, bag, `slot`, `item` ) VALUES ( '" & newcharguid.ToString() & "', '0', '23', '" & newitemguid.ToString() & "')", True)
            'Set home
            LogAppend("Setting character homebind", "CharacterCreationAdvanced_createAtTrinity", False)
            Dim tmpstring As String = GetTemporaryCharacterInformation("@character_homebind", targetSetId)
            runSQLCommand_characters_string("INSERT INTO character_homebind ( guid, " & homebind_map_columnname & ", " & homebind_zone_columnname & ", " & homebind_posx_columnname & ", " &
                                            homebind_posy_columnname & ", " & homebind_posz_columnname & " ) VALUES ( '" & newcharguid.ToString() & "', '" & splitList(tmpstring, "map") &
                                            "', '" & splitList(tmpstring, "zone") & "', '" & splitList(tmpstring, "position_x") & "', '" & splitList(tmpstring, "position_y") & "', '" &
                                            splitList(tmpstring, "position_z") & "' )")
        Catch ex As Exception
            LogAppend("Something went wrong while creating the account -> Skipping! -> Error message is: " & ex.ToString(), "CharacterCreationAdvanced_createAtTrinity", False, True)
        End Try
    End Sub
    Private Shared Sub createAtMangos(ByVal charactername As String, ByVal accname As String, ByVal targetSetId As Integer, ByVal NameChange As Boolean)
        LogAppend("Creating at Mangos", "CharacterCreationAdvanced_createAtMangos", False)
        Dim newcharguid As Integer = TryInt(runSQLCommand_characters_string("SELECT guid FROM characters WHERE guid=(SELECT MAX(guid) FROM characters)", True)) + 1
        Dim accid As Integer = TryInt(runSQLCommand_realm_string("SELECT `id` FROM account WHERE username='" & accname & "'", True))
        Const sqlstring As String = "INSERT INTO characters ( `guid`, `account`, `name`, `race`, `class`, `gender`, `level`, `xp`, `money`, `playerBytes`, `position_x`, position_y, position_z, map, orientation, taximask `health` ) VALUES " &
            "( @guid, @accid, @name, @race, @class, @gender, @level, '0', '0', @pBytes, '-14305.7', '514.08', '10', '0', '4.30671', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 ','1000' )"
        Dim tempcommand As New MySqlCommand(sqlstring, TargetConnection_Realm)
        tempcommand.Parameters.AddWithValue("@accid", accid.ToString())
        tempcommand.Parameters.AddWithValue("@guid", newcharguid.ToString())
        tempcommand.Parameters.AddWithValue("@name", charactername)
        tempcommand.Parameters.AddWithValue("@pBytes", GetTemporaryCharacterInformation("@character_playerBytes", targetSetId))
        tempcommand.Parameters.AddWithValue("@class", GetTemporaryCharacterInformation("@character_class", targetSetId))
        tempcommand.Parameters.AddWithValue("@race", GetTemporaryCharacterInformation("@character_race", targetSetId))
        tempcommand.Parameters.AddWithValue("@gender", GetTemporaryCharacterInformation("@character_gender", targetSetId))
        tempcommand.Parameters.AddWithValue("@level", GetTemporaryCharacterInformation("@character_level", targetSetId))
        Try
            tempcommand.ExecuteNonQuery()
            If NameChange = True Then
                runSQLCommand_characters_string("UPDATE characters SET at_login='1' WHERE guid='" & newcharguid.ToString & "'", True)
            Else
                If CharacterExist(charactername) = True Then
                    runSQLCommand_characters_string("UPDATE characters SET at_login='1' WHERE guid='" & newcharguid.ToString & "'", True)
                End If
            End If
            'Creating hearthstone
            LogAppend("Creating character hearthstone", "CharacterCreationAdvanced_createAtArcemu", False)
            Dim newitemguid As Integer = (TryInt(runSQLCommand_characters_string("SELECT guid FROM item_instance WHERE guid=(SELECT MAX(guid) FROM item_instance))", True)) + 1)
            If expansion >= 3 Then
                runSQLCommand_characters_string("INSERT INTO item_instance ( guid, owner_guid, data ) VALUES ( '" & newitemguid.ToString() & "', '" & newcharguid.ToString() & "', '" & newitemguid.ToString() &
                                                " 1191182336 3 6948 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ' )", True)
            Else
                'MaNGOS < 3.3 Core: watch data length
                runSQLCommand_characters_string(
                    "INSERT INTO item_instance ( guid, owner_guid, data ) VALUES ( '" & newitemguid.ToString() & "', '" & accid.ToString() & "', '" & newitemguid.ToString() &
                    " 1191182336 3 6948 1065353216 0 8 0 8 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ' )", True)
            End If
            runSQLCommand_characters_string("INSERT INTO character_inventory ( guid, bag, slot, item, item_template ) VALUES ( '" & accid.ToString() & "', '0', '23', '" & newitemguid.ToString() & "', '6948')")
            'Set home
            LogAppend("Setting character homebind", "CharacterCreationAdvanced_createAtMangos", False)
            Dim tmpstring As String = GetTemporaryCharacterInformation("@character_homebind", targetSetId)
            runSQLCommand_characters_string("INSERT INTO character_homebind ( guid, " & homebind_map_columnname & ", " & homebind_zone_columnname & ", " &
            homebind_posx_columnname & ", " & homebind_posy_columnname & ", " & homebind_posz_columnname & " ) VALUES ( '" & newcharguid.ToString() & "', '" & splitList(tmpstring, "map") & "', '" & splitList(tmpstring, "zone") &
            "', '" & splitList(tmpstring, "position_x") & "', '" & splitList(tmpstring, "position_y") & "', '" & splitList(tmpstring, "position_z") & "' )")
        Catch ex As Exception
            LogAppend("Something went wrong while creating the account -> Skipping! -> Error message is: " & ex.ToString(), "CharacterCreationAdvanced_createAtMangos", False, True)
        End Try
    End Sub
End Class
