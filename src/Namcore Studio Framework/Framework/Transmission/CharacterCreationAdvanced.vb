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
'*      /Filename:      CharacterCreationAdvanced
'*      /Description:   Includes functions for creating a new character which has not been
'*                      parsed from the wow armory
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports MySql.Data.MySqlClient
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Database
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Modules
Imports NCFramework.Framework.Functions.ResourceHandler

Namespace Framework.Transmission
    Public Class CharacterCreationAdvanced
        Public Function CreateNewAdvancedCharacter(ByVal charname As String, ByVal accountId As String,
                                              ByRef player As Character,
                                              Optional forceNameChange As Boolean = False) As Boolean
            LogAppend("Creating new character: " & charname & " for account : " & accountId.ToString,
                      "CharacterCreationAdvanced_CreateNewAdvancedCharacter", True)
            Select Case GlobalVariables.targetCore
                Case "arcemu"
                    Return CreateAtArcemu(charname, accountId.ToString, player, forceNameChange)
                Case "trinity"
                    Return CreateAtTrinity(charname, accountId.ToString, player, forceNameChange)
                Case "trinitytbc"
                    Return False
                Case "mangos"
                    Return CreateAtMangos(charname, accountId.ToString, player, forceNameChange)
                Case Else
                    Return False
            End Select
        End Function

        Private Function CreateAtArcemu(ByVal charactername As String, ByVal accid As Integer, ByRef player As Character,
                                   ByVal nameChange As Boolean) As Boolean
            LogAppend("Creating at arcemu", "CharacterCreationAdvanced_createAtArcemu", False)
            Dim newcharguid As Integer = TryInt(
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.targetStructure.char_guid_col(0) & " FROM " &
                    GlobalVariables.targetStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.targetStructure.char_guid_col(0) &
                    "=(SELECT MAX(" & GlobalVariables.targetStructure.char_guid_col(0) & ") FROM " &
                    GlobalVariables.targetStructure.character_tbl(0) & ")", True)) + 1
            Dim sqlstring As String = "INSERT INTO " & GlobalVariables.targetStructure.character_tbl(0) & " ( `" &
                                      GlobalVariables.targetStructure.char_guid_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_accountId_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_name_col(0) &
                                      "`, `" & GlobalVariables.targetStructure.char_race_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_class_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_gender_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_level_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_xp_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_gold_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_playerBytes_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_playerBytes2_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_playerFlags_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_posX_col(0) & "`, " & "" &
                                      GlobalVariables.targetStructure.char_posY_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_posZ_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_map_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_orientation_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_taximask_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_arcemuPlayedTime_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_stableSlots_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_zone_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_watchedFaction_col(0) & ", current_hp, " &
                                      "" & GlobalVariables.targetStructure.char_speccount_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_activeSpec_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_exploredZones_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_knownTitles_col(0) & " ) VALUES " &
                                      "( @guid, @accid, @name, @race, @class, @gender, @level, @xp, @gold, @pBytes, @pBytes2, @pFlags, @posx, @posy, @posz, @map, '4,40671', @taxi, '0 0 0 ', @stable, @zone, " &
                                      "@title, @wFaction, '1000', @speccpunt, @activespec, @exploredZones, @knownTitles )"
            Dim tempcommand As New MySqlCommand(sqlstring, GlobalVariables.TargetConnection)
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
            tempcommand.Parameters.AddWithValue("@pFlags", player.PlayerFlags.GetHashCode().ToString)
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
            Dim mCharCreationLite As New CharacterCreationLite
            Try
                tempcommand.ExecuteNonQuery()
                If nameChange = True Then
                    runSQLCommand_characters_string(
                        "UPDATE " & GlobalVariables.targetStructure.character_tbl(0) &
                        " SET forced_rename_pending='1' WHERE " & GlobalVariables.targetStructure.char_guid_col(0) &
                        "='" &
                        newcharguid.ToString & "'", True)
                Else
                    If mCharCreationLite.CharacterExist(charactername) = True Then
                        runSQLCommand_characters_string(
                            "UPDATE " & GlobalVariables.targetStructure.character_tbl(0) &
                            " SET forced_rename_pending='1' WHERE " & GlobalVariables.targetStructure.char_guid_col(0) &
                            "='" & newcharguid.ToString & "'", True)
                    End If
                End If
                AddSpells("6603,", player)
                Dim tempinfo As String = player.CustomFaction.ToString
                If Not tempinfo = "" Then _
                    runSQLCommand_characters_string(
                        "UPDATE " & GlobalVariables.targetStructure.character_tbl(0) & " SET " &
                        GlobalVariables.targetStructure.char_customFaction_col(0) & "='" & tempinfo &
                        "' WHERE " & GlobalVariables.targetStructure.char_guid_col(0) & "='" & newcharguid.ToString &
                        "'")
                'Setting tutorials
                runSQLCommand_characters_string(
                    "INSERT INTO `tutorials` ( playerId ) VALUES ( " & accid.ToString() & " )",
                    True)
                'Set home
                LogAppend("Setting character homebind", "CharacterCreationAdvanced_createAtArcemu", False)
                Dim tmpstring As String = player.HomeBind
                runSQLCommand_characters_string(
                    "UPDATE " & GlobalVariables.targetStructure.character_tbl(0) & " SET " &
                    GlobalVariables.targetStructure.char_bindposX_col(0) & "='" & SplitList(tmpstring, "position_x") &
                    "' WHERE " & GlobalVariables.targetStructure.char_guid_col(0) & "='" & newcharguid.ToString() & "'")
                runSQLCommand_characters_string(
                    "UPDATE " & GlobalVariables.targetStructure.character_tbl(0) & " SET " &
                    GlobalVariables.targetStructure.char_bindposY_col(0) & "='" & SplitList(tmpstring, "position_y") &
                    "' WHERE " & GlobalVariables.targetStructure.char_guid_col(0) & "='" & newcharguid.ToString() & "'")
                runSQLCommand_characters_string(
                    "UPDATE " & GlobalVariables.targetStructure.character_tbl(0) & " SET " &
                    GlobalVariables.targetStructure.char_bindposZ_col(0) & "='" & SplitList(tmpstring, "position_z") &
                    "' WHERE " & GlobalVariables.targetStructure.char_guid_col(0) & "='" & newcharguid.ToString() & "'")
                runSQLCommand_characters_string(
                    "UPDATE " & GlobalVariables.targetStructure.character_tbl(0) & " SET " &
                    GlobalVariables.targetStructure.char_bindmapid_col(0) & "='" & SplitList(tmpstring, "map") &
                    "' WHERE " & GlobalVariables.targetStructure.char_guid_col(0) & "='" & newcharguid.ToString() & "'")
                runSQLCommand_characters_string(
                    "UPDATE " & GlobalVariables.targetStructure.character_tbl(0) & " SET " &
                    GlobalVariables.targetStructure.char_bindzoneid_col(0) & "='" & SplitList(tmpstring, "zone") &
                    "' WHERE " & GlobalVariables.targetStructure.char_guid_col(0) & "='" & newcharguid.ToString() & "'")
                player.CreatedGuid = newcharguid
                Return True
            Catch ex As Exception
                LogAppend(
                    "Something went wrong while creating the character -> Skipping! -> Error message is: " & ex.ToString(),
                    "CharacterCreationAdvanced_createAtArcemu", False, True)
                MsgBox(GetUserMessage("errCharacterCreation"), MsgBoxStyle.Critical, "Error")
                Return False
            End Try
        End Function

        Private Function CreateAtTrinity(ByVal charactername As String, ByVal accid As Integer, ByRef player As Character,
                                    ByVal nameChange As Boolean) As Boolean
            LogAppend("Creating at Trinity", "CharacterCreationAdvanced_createAtTrinity", False)
            Dim newcharguid As Integer = TryInt(
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.targetStructure.char_guid_col(0) & " FROM " &
                    GlobalVariables.targetStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.targetStructure.char_guid_col(0) &
                    "=(SELECT MAX(" & GlobalVariables.targetStructure.char_guid_col(0) & ") FROM " &
                    GlobalVariables.targetStructure.character_tbl(0) & ")", True)) + 1
            Dim sqlstring As String = "INSERT INTO " & GlobalVariables.targetStructure.character_tbl(0) & " ( `" &
                                      GlobalVariables.targetStructure.char_guid_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_accountId_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_name_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_race_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_class_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_gender_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_level_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_xp_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_gold_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_playerBytes_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_playerBytes2_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_playerFlags_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_posX_col(0) & "`, " &
                                      GlobalVariables.targetStructure.char_posY_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_posZ_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_map_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_orientation_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_taximask_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_cinematic_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_totaltime_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_leveltime_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_extraFlags_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_stableSlots_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_atlogin_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_zone_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_chosenTitle_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_knownCurrencies_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_watchedFaction_col(0) & ", `" &
                                      GlobalVariables.targetStructure.char_health_col(0) & "`, " &
                                      GlobalVariables.targetStructure.char_speccount_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_activeSpec_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_exploredZones_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_knownTitles_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_actionBars_col(0) & " ) VALUES " &
                                      "( @guid, @accid, @name, @race, @class, @gender, @level, @xp, @gold, @pBytes, @pBytes2, @pFlags, @posx, @posy, @posz, @map, '4,40671', @taxi, '1', @totaltime, @leveltime, @extraflags, " &
                                      "@stable, @login, @zone, @title, @knownCurrencies, @wFaction, '5000', @speccount, @activespec, @exploredZones, @knownTitles, @action )"
            Dim tempcommand As New MySqlCommand(sqlstring, GlobalVariables.TargetConnection)
            tempcommand.Parameters.AddWithValue("@accid", accid.ToString())
            tempcommand.Parameters.AddWithValue("@guid", newcharguid.ToString())
            tempcommand.Parameters.AddWithValue("@name", charactername)
            tempcommand.Parameters.AddWithValue("@class", player.Cclass.ToString)
            tempcommand.Parameters.AddWithValue("@race", player.Race.ToString)
            tempcommand.Parameters.AddWithValue("@gender", player.Gender.ToString)
            tempcommand.Parameters.AddWithValue("@level", player.Level.ToString)
            tempcommand.Parameters.AddWithValue("@xp", player.Xp.ToString)
            tempcommand.Parameters.AddWithValue("@gold", player.Gold)
            tempcommand.Parameters.AddWithValue("@pBytes", player.PlayerBytes.ToString)
            tempcommand.Parameters.AddWithValue("@pBytes2", player.PlayerBytes2.ToString)
            tempcommand.Parameters.AddWithValue("@pFlags", player.PlayerFlags.GetHashCode().ToString)
            tempcommand.Parameters.AddWithValue("@posx", player.PositionX.ToString)
            tempcommand.Parameters.AddWithValue("@posy", player.PositionY.ToString)
            tempcommand.Parameters.AddWithValue("@posz", (player.PositionZ + 1).ToString)
            tempcommand.Parameters.AddWithValue("@map", player.Map.ToString)
            tempcommand.Parameters.AddWithValue("@taxi", player.Taximask)
            tempcommand.Parameters.AddWithValue("@stable", player.StableSlots.ToString)
            tempcommand.Parameters.AddWithValue("@totaltime", player.TotalTime)
            tempcommand.Parameters.AddWithValue("@leveltime", player.LevelTime.ToString)
            tempcommand.Parameters.AddWithValue("@extraflags", player.ExtraFlags.GetHashCode().ToString)
            tempcommand.Parameters.AddWithValue("@login", player.AtLogin.GetHashCode().ToString)
            tempcommand.Parameters.AddWithValue("@zone", player.Zone.ToString)
            tempcommand.Parameters.AddWithValue("@knownCurrencies", player.KnownCurrencies.ToString)
            tempcommand.Parameters.AddWithValue("@action", player.ActionBars.ToString)
            tempcommand.Parameters.AddWithValue("@title", player.ChosenTitle.ToString)
            tempcommand.Parameters.AddWithValue("@wFaction", player.WatchedFaction.ToString)
            tempcommand.Parameters.AddWithValue("@speccount", player.SpecCount.ToString)
            tempcommand.Parameters.AddWithValue("@activespec", player.ActiveSpec.ToString)
            tempcommand.Parameters.AddWithValue("@exploredZones", player.ExploredZones)
            tempcommand.Parameters.AddWithValue("@knownTitles", player.KnownTitles.ToString)
            Dim mCharCreationLite As New CharacterCreationLite
            Try
                tempcommand.ExecuteNonQuery()
                If nameChange = True Then
                    runSQLCommand_characters_string(
                        "UPDATE " & GlobalVariables.targetStructure.character_tbl(0) & " SET " &
                        GlobalVariables.targetStructure.char_atlogin_col(0) & "='1' WHERE " &
                        GlobalVariables.targetStructure.char_guid_col(0) & "='" &
                        newcharguid.ToString & "'", True)
                Else
                    If mCharCreationLite.CharacterExist(charactername) = True Then
                        runSQLCommand_characters_string(
                            "UPDATE " & GlobalVariables.targetStructure.character_tbl(0) & " SET " &
                            GlobalVariables.targetStructure.char_atlogin_col(0) & "='1' WHERE " &
                            GlobalVariables.targetStructure.char_guid_col(0) & "='" &
                            newcharguid.ToString & "'", True)
                    End If
                End If
                'Set home
                LogAppend("Setting character homebind", "CharacterCreationAdvanced_createAtTrinity", False)
                Dim tmpstring As String = player.HomeBind
                runSQLCommand_characters_string(
                    "INSERT INTO " & GlobalVariables.targetStructure.character_homebind_tbl(0) & " ( " &
                    GlobalVariables.targetStructure.homebind_guid_col(0) & ", " &
                    GlobalVariables.targetStructure.homebind_map_col(0) & ", " &
                    GlobalVariables.targetStructure.homebind_zone_col(0) & ", " &
                    GlobalVariables.targetStructure.homebind_posx_col(0) & ", " &
                    GlobalVariables.targetStructure.homebind_posy_col(0) & ", " &
                    GlobalVariables.targetStructure.homebind_posz_col(0) &
                    " ) VALUES ( '" & newcharguid.ToString() & "', '" & SplitList(tmpstring, "map") & "', '" &
                    SplitList(tmpstring, "zone") & "', '" & SplitList(tmpstring, "position_x") &
                    "', '" & SplitList(tmpstring, "position_y") & "', '" & SplitList(tmpstring, "position_z") & "' )")
                player.CreatedGuid = newcharguid
                Return True
            Catch ex As Exception
                LogAppend(
                    "Something went wrong while creating the character -> Skipping! -> Error message is: " & ex.ToString(),
                    "CharacterCreationAdvanced_createAtTrinity", False, True)
                MsgBox(GetUserMessage("errCharacterCreation"), MsgBoxStyle.Critical, "Error")
                Return False
            End Try
        End Function

        Private Function CreateAtMangos(ByVal charactername As String, ByVal accid As Integer, ByRef player As Character,
                                   ByVal nameChange As Boolean) As Boolean
            LogAppend("Creating at Mangos", "CharacterCreationAdvanced_createAtMangos", False)
            Dim newcharguid As Integer = TryInt(
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.targetStructure.char_guid_col(0) & " FROM " &
                    GlobalVariables.targetStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.targetStructure.char_guid_col(0) &
                    "=(SELECT MAX(" & GlobalVariables.targetStructure.char_guid_col(0) & ") FROM " &
                    GlobalVariables.targetStructure.character_tbl(0) & ")", True)) + 1
            Dim sqlstring As String = "INSERT INTO " & GlobalVariables.targetStructure.character_tbl(0) & " ( `" &
                                      GlobalVariables.targetStructure.char_guid_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_accountId_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_name_col(0) &
                                      "`, `" & GlobalVariables.targetStructure.char_race_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_class_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_gender_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_level_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_xp_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_gold_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_playerBytes_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_playerBytes2_col(0) & "`, " &
                                      "`" & GlobalVariables.targetStructure.char_playerFlags_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_posX_col(0) & "`, " &
                                      GlobalVariables.targetStructure.char_posY_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_posZ_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_map_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_orientation_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_taximask_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_cinematic_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_totaltime_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_leveltime_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_extraFlags_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_stableSlots_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_atlogin_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_zone_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_chosenTitle_col(0) & ", " &
                                      "" & GlobalVariables.targetStructure.char_knownCurrencies_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_watchedFaction_col(0) & ", `" &
                                      GlobalVariables.targetStructure.char_health_col(0) & "`, " &
                                      GlobalVariables.targetStructure.char_speccount_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_activeSpec_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_exploredZones_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_knownTitles_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_actionBars_col(0) & " ) VALUES " &
                                      "( @guid, @accid, @name, @race, @class, @gender, @level, @xp, @gold, @pBytes, @pBytes2, @pFlags, @posx, @posy, @posz, @map, '4,40671', @taxi, '1', @totaltime, @leveltime, @extraflags, " &
                                      "@stable, @login, @zone, @title, @knownCurrencies, @wFaction, '5000', @speccount, @activespec, @exploredZones, @knownTitles, @action )"
            Dim tempcommand As New MySqlCommand(sqlstring, GlobalVariables.TargetConnection)
            tempcommand.Parameters.AddWithValue("@accid", accid.ToString())
            tempcommand.Parameters.AddWithValue("@guid", newcharguid.ToString())
            tempcommand.Parameters.AddWithValue("@name", charactername)
            tempcommand.Parameters.AddWithValue("@class", player.Cclass.ToString())
            tempcommand.Parameters.AddWithValue("@race", player.Race.ToString())
            tempcommand.Parameters.AddWithValue("@gender", player.Gender.ToString())
            tempcommand.Parameters.AddWithValue("@level", player.Level.ToString())
            tempcommand.Parameters.AddWithValue("@xp", player.Xp.ToString())
            tempcommand.Parameters.AddWithValue("@gold", player.Gold)
            tempcommand.Parameters.AddWithValue("@pBytes", player.PlayerBytes.ToString())
            tempcommand.Parameters.AddWithValue("@pBytes2", player.PlayerBytes2.ToString())
            tempcommand.Parameters.AddWithValue("@pFlags", player.PlayerFlags.GetHashCode().ToString())
            tempcommand.Parameters.AddWithValue("@posx", player.PositionX.ToString())
            tempcommand.Parameters.AddWithValue("@posy", player.PositionY.ToString())
            tempcommand.Parameters.AddWithValue("@posz", (player.PositionZ + 1).ToString())
            tempcommand.Parameters.AddWithValue("@map", player.Map.ToString())
            tempcommand.Parameters.AddWithValue("@taxi", player.Taximask)
            tempcommand.Parameters.AddWithValue("@stable", player.StableSlots.ToString())
            tempcommand.Parameters.AddWithValue("@totaltime", player.TotalTime)
            tempcommand.Parameters.AddWithValue("@leveltime", player.LevelTime.ToString())
            tempcommand.Parameters.AddWithValue("@extraflags", player.ExtraFlags.GetHashCode().ToString())
            tempcommand.Parameters.AddWithValue("@login", player.AtLogin.GetHashCode().ToString())
            tempcommand.Parameters.AddWithValue("@zone", player.Zone.ToString())
            tempcommand.Parameters.AddWithValue("@knownCurrencies", player.KnownCurrencies.ToString())
            tempcommand.Parameters.AddWithValue("@action", player.ActionBars.ToString())
            tempcommand.Parameters.AddWithValue("@title", player.ChosenTitle.ToString())
            tempcommand.Parameters.AddWithValue("@wFaction", player.WatchedFaction.ToString())
            tempcommand.Parameters.AddWithValue("@speccpunt", player.SpecCount.ToString())
            tempcommand.Parameters.AddWithValue("@activespec", player.ActiveSpec.ToString())
            tempcommand.Parameters.AddWithValue("@exploredZones", player.ExploredZones)
            tempcommand.Parameters.AddWithValue("@knownTitles", player.KnownTitles)
            Dim mCharCreationLite As New CharacterCreationLite
            Try
                tempcommand.ExecuteNonQuery()
                If nameChange = True Then
                    runSQLCommand_characters_string(
                        "UPDATE " & GlobalVariables.targetStructure.character_tbl(0) & " SET " &
                        GlobalVariables.targetStructure.char_atlogin_col(0) & "='1' WHERE " &
                        GlobalVariables.targetStructure.char_guid_col(0) &
                        "='" & newcharguid.ToString & "'", True)
                Else
                    If mCharCreationLite.CharacterExist(charactername) = True Then
                        runSQLCommand_characters_string(
                            "UPDATE " & GlobalVariables.targetStructure.character_tbl(0) & " SET " &
                            GlobalVariables.targetStructure.char_atlogin_col(0) & "='1' WHERE " &
                            GlobalVariables.targetStructure.char_guid_col(0) & "='" &
                            newcharguid.ToString & "'", True)
                    End If
                End If
                'Set home
                LogAppend("Setting character homebind", "CharacterCreationAdvanced_createAtMangos", False)
                Dim tmpstring As String = player.HomeBind
                runSQLCommand_characters_string(
                    "INSERT INTO " & GlobalVariables.targetStructure.character_homebind_tbl(0) & " ( " &
                    GlobalVariables.targetStructure.homebind_guid_col(0) & ", " &
                    GlobalVariables.targetStructure.homebind_map_col(0) & ", " &
                    GlobalVariables.targetStructure.homebind_zone_col(0) & ", " &
                    GlobalVariables.targetStructure.homebind_posx_col(0) & ", " &
                    GlobalVariables.targetStructure.homebind_posy_col(0) & ", " &
                    GlobalVariables.targetStructure.homebind_posz_col(0) &
                    " ) VALUES ( '" & newcharguid.ToString() & "', '" & SplitList(tmpstring, "map") & "', '" &
                    SplitList(tmpstring, "zone") &
                    "', '" & SplitList(tmpstring, "position_x") & "', '" & SplitList(tmpstring, "position_y") & "', '" &
                    SplitList(tmpstring, "position_z") & "' )")

                player.CreatedGuid = newcharguid
                Return True
            Catch ex As Exception
                LogAppend(
                    "Something went wrong while creating the character -> Skipping! -> Error message is: " & ex.ToString(),
                    "CharacterCreationAdvanced_createAtMangos", False, True)
                MsgBox(GetUserMessage("errCharacterCreation"), MsgBoxStyle.Critical, "Error")
                Return False
            End Try
        End Function
    End Class
End Namespace