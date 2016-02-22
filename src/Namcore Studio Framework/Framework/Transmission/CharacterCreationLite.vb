'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013-2016 NamCore Studio <https://github.com/megasus/Namcore-Studio>
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
'*      /Filename:      CharacterCreationLite
'*      /Description:   Includes functions for creating a new character which has been
'*                      parsed from the wow armory
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports MySql.Data.MySqlClient
Imports NCFramework.Framework.Database
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Modules
Imports NCFramework.My.Resources

Namespace Framework.Transmission
    Public Class CharacterCreationLite
        Public Function CreateNewLiteCharacter(charname As String, accountId As Integer,
                                               player As Character,
                                               Optional forceNameChange As Boolean = False) As Boolean
            LogAppend("Creating new character: " & charname & " for account : " & accountId.ToString,
                      "CharacterCreationLite_CreateNewLiteCharacter", True)
            Select Case GlobalVariables.targetCore
                Case Modules.Core.ARCEMU
                    Return CreateAtArcemu(charname, accountId, player, forceNameChange)
                Case Modules.Core.TRINITY
                    Return CreateAtTrinity(charname, accountId, player, forceNameChange)
                Case Modules.Core.MANGOS
                    Return CreateAtMangos(charname, accountId, player, forceNameChange)
                Case Else
                    Return False
            End Select
        End Function

        Public Function CharacterExist(charname As String) As Boolean
            Select Case GlobalVariables.targetCore
                Case Modules.Core.ARCEMU, Modules.Core.TRINITY, Modules.Core.MANGOS
                    If _
                        ReturnResultCount(
                            "SELECT * FROM " & GlobalVariables.targetStructure.character_tbl(0) & " WHERE " &
                            GlobalVariables.targetStructure.char_name_col(0) & "='" & charname & "'", True) = 1 _
                        Then _
                        Return False Else Return True
                Case Else
                    Return Nothing
            End Select
        End Function

        Private Function CreateAtArcemu(charactername As String, accId As Integer, ByRef player As Character,
                                        nameChange As Boolean) As Boolean
            LogAppend("Creating at arcemu", "CharacterCreationLite_createAtArcemu", False)
            Dim newcharguid As Integer = TryInt(
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.targetStructure.char_guid_col(0) & " FROM " &
                    GlobalVariables.targetStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.targetStructure.char_guid_col(0) & "=(SELECT MAX(" &
                    GlobalVariables.targetStructure.char_guid_col(0) & ") FROM " &
                    GlobalVariables.targetStructure.character_tbl(0) & ")", True)) + 1
            player.CreatedGuid = newcharguid
            Dim sqlstring As String = "INSERT INTO " & GlobalVariables.targetStructure.character_tbl(0) & " ( `" &
                                      GlobalVariables.targetStructure.char_accountId_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_guid_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_name_col(0) &
                                      "`, `" & GlobalVariables.targetStructure.char_race_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_class_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_gender_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_level_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_xp_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_gold_col(0) & "`, current_hp, `" &
                                      GlobalVariables.targetStructure.char_playerBytes_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_posX_col(0) & "`, " &
                                      "" & GlobalVariables.targetStructure.char_posY_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_posZ_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_orientation_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_map_col(0) &
                                      ", " & GlobalVariables.targetStructure.char_taximask_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_arcemuPlayedTime_col(0) & " ) " &
                                      "VALUES ( @accid, @guid, @name, @race, @class, @gender, @level, '0', '0', '1000', @pBytes, '-14305.7', '514.08', '10', '4.30671', '0', '0 0 0 0 0 0 0 0 0 0 0 0 ', '98 98 5 ' )"
            Dim tempcommand As New MySqlCommand(sqlstring, GlobalVariables.TargetConnection)
            tempcommand.Parameters.AddWithValue("@accid", accId.ToString())
            tempcommand.Parameters.AddWithValue("@guid", newcharguid.ToString())
            tempcommand.Parameters.AddWithValue("@name", charactername)
            tempcommand.Parameters.AddWithValue("@class", player.Cclass.ToString())
            tempcommand.Parameters.AddWithValue("@race", player.Race.ToString())
            tempcommand.Parameters.AddWithValue("@gender", player.Gender.ToString())
            tempcommand.Parameters.AddWithValue("@level", player.Level.ToString())
            tempcommand.Parameters.AddWithValue("@pBytes", player.PlayerBytes.ToString())
            Try
                tempcommand.ExecuteNonQuery()
                If nameChange = True Then
                    runSQLCommand_characters_string(
                        "UPDATE " & GlobalVariables.targetStructure.character_tbl(0) &
                        " SET forced_rename_pending='1' WHERE guid='" & newcharguid.ToString & "'", True)
                Else
                    If CharacterExist(charactername) = True Then
                        runSQLCommand_characters_string(
                            "UPDATE " & GlobalVariables.targetStructure.character_tbl(0) &
                            " SET forced_rename_pending='1' WHERE guid='" & newcharguid.ToString & "'", True)
                    End If
                End If
                'Creating hearthstone
                LogAppend("Creating character hearthstone", "CharacterCreationLite_createAtArcemu", False)
                Dim newitemguid As Integer =
                        (TryInt(
                            runSQLCommand_characters_string(
                                "SELECT " & GlobalVariables.targetStructure.itmins_guid_col(0) & " FROM " &
                                GlobalVariables.targetStructure.item_instance_tbl(0) & " WHERE " &
                                GlobalVariables.targetStructure.itmins_guid_col(0) &
                                "=(SELECT MAX(" & GlobalVariables.targetStructure.itmins_guid_col(0) & ") FROM " &
                                GlobalVariables.targetStructure.item_instance_tbl(0) & ")", True)) + 1)
                runSQLCommand_characters_string(
                    "INSERT INTO " & GlobalVariables.targetStructure.item_instance_tbl(0) & " ( " &
                    GlobalVariables.targetStructure.itmins_ownerGuid_col(0) & ", " &
                    GlobalVariables.targetStructure.itmins_guid_col(0) & ", " &
                    GlobalVariables.targetStructure.itmins_itemEntry_col(0) & ", flags, " &
                    GlobalVariables.targetStructure.itmins_container_col(0) & ", " &
                    GlobalVariables.targetStructure.itmins_slot_col(0) & " ) VALUES ( '" & newcharguid.ToString() &
                    "', '" & newitemguid.ToString() & "', '6948', '1', '-1', '23' )", True)
                '//Adding special skills & spells
                GetRaceSpells(player)
                GetClassSpells(player)
                AddSpells("6603,", player)
                '// Setting tutorials
                runSQLCommand_characters_string(
                    "INSERT INTO `tutorials` ( playerId ) VALUES ( " & accId.ToString() & " )",
                    True)
                Return True
            Catch ex As Exception
                LogAppend(
                    "Something went wrong while creating the character -> Skipping! -> Error message is: " &
                    ex.ToString(),
                    "CharacterCreationLite_createAtArcemu", False, True)
                MsgBox(MSG_FATALDURINGCHARCREATION, MsgBoxStyle.Critical, MSG_ERROR)
                Return False
            End Try
        End Function

        Private Function CreateAtTrinity(charactername As String, accid As Integer,
                                         ByRef player As Character,
                                         nameChange As Boolean) As Boolean
            LogAppend("Creating at Trinity", "CharacterCreationLite_createAtTrinity", False)
            Dim newcharguid As Integer = TryInt(
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.targetStructure.char_guid_col(0) & " FROM " &
                    GlobalVariables.targetStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.targetStructure.char_guid_col(0) &
                    "=(SELECT MAX(" & GlobalVariables.targetStructure.char_guid_col(0) & ") FROM " &
                    GlobalVariables.targetStructure.character_tbl(0) & ")", True)) + 1
            player.CreatedGuid = newcharguid
            Dim sqlstring As String = "INSERT INTO characters ( `" & GlobalVariables.targetStructure.char_guid_col(0) &
                                      "`, `" & GlobalVariables.targetStructure.char_accountId_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_name_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_race_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_class_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_gender_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_level_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_xp_col(0) &
                                      "`, `" & GlobalVariables.targetStructure.char_gold_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_playerBytes_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_posX_col(0) & "`, " &
                                      GlobalVariables.targetStructure.char_posY_col(0) &
                                      ", " & GlobalVariables.targetStructure.char_posZ_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_map_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_orientation_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_taximask_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_cinematic_col(0) & ", `" &
                                      GlobalVariables.targetStructure.char_health_col(0) & "` ) VALUES " &
                                      "( @guid, @accid, @name, @race, @class, @gender, @level, '0', '0', @pBytes, '-14306', '515', '10', '0', '5', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1', '1000' )"
            Dim tempcommand As New MySqlCommand(sqlstring, GlobalVariables.TargetConnection)
            tempcommand.Prepare()
            tempcommand.Parameters.AddWithValue("@accid", accid)
            tempcommand.Parameters.AddWithValue("@guid", newcharguid)
            tempcommand.Parameters.AddWithValue("@name", charactername)
            tempcommand.Parameters.AddWithValue("@class", player.Cclass)
            tempcommand.Parameters.AddWithValue("@race", player.Race)
            tempcommand.Parameters.AddWithValue("@gender", player.Gender)
            tempcommand.Parameters.AddWithValue("@level", player.Level)
            tempcommand.Parameters.AddWithValue("@pBytes", player.PlayerBytes)
            Try
                tempcommand.ExecuteNonQuery()
                If nameChange = True Then
                    runSQLCommand_characters_string(
                        "UPDATE " & GlobalVariables.targetStructure.character_tbl(0) & " SET " &
                        GlobalVariables.targetStructure.char_atlogin_col(0) & "='1' WHERE " &
                        GlobalVariables.targetStructure.char_guid_col(0) & "='" & newcharguid.ToString & "'", True)
                Else
                    If CharacterExist(charactername) = True Then
                        runSQLCommand_characters_string(
                            "UPDATE " & GlobalVariables.targetStructure.character_tbl(0) & " SET " &
                            GlobalVariables.targetStructure.char_atlogin_col(0) & "='1' WHERE " &
                            GlobalVariables.targetStructure.char_guid_col(0) & "='" & newcharguid.ToString & "'", True)
                    End If
                End If
                '// Special Spells
                GetRaceSpells(player)
                GetClassSpells(player)
                '//Creating hearthstone
                LogAppend("Creating character hearthstone", "CharacterCreationLite_createAtTrinity", False)
                Dim newitemguid As Integer =
                        (TryInt(
                            runSQLCommand_characters_string(
                                "SELECT " & GlobalVariables.targetStructure.itmins_guid_col(0) & " FROM " &
                                GlobalVariables.targetStructure.item_instance_tbl(0) & " WHERE " &
                                GlobalVariables.targetStructure.itmins_guid_col(0) & "=(SELECT MAX(" &
                                GlobalVariables.targetStructure.char_guid_col(0) & ") FROM " &
                                GlobalVariables.targetStructure.item_instance_tbl(0) &
                                ")", True)) + 1)
                runSQLCommand_characters_string(
                    "INSERT INTO " & GlobalVariables.targetStructure.item_instance_tbl(0) & " ( " &
                    GlobalVariables.targetStructure.itmins_guid_col(0) & ", " &
                    GlobalVariables.targetStructure.itmins_itemEntry_col(0) & ", " &
                    GlobalVariables.targetStructure.itmins_ownerGuid_col(0) & ", " &
                    GlobalVariables.targetStructure.itmins_count_col(0) & ", charges, " &
                    GlobalVariables.targetStructure.itmins_enchantments_col(0) & ", " &
                    GlobalVariables.targetStructure.itmins_durability_col(0) & " ) VALUES ( '" &
                    newitemguid.ToString & "', '6948', '" & newcharguid.ToString() & "', '1', '0 0 0 0 0 ', '" &
                    newitemguid.ToString() &
                    " 1191182336 3 6948 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )",
                    True)
                runSQLCommand_characters_string(
                    "INSERT INTO " & GlobalVariables.targetStructure.character_inventory_tbl(0) & " ( " &
                    GlobalVariables.targetStructure.invent_guid_col(0) & ", " &
                    GlobalVariables.targetStructure.invent_bag_col(0) & ", `" &
                    GlobalVariables.targetStructure.invent_slot_col(0) & "`, `" &
                    GlobalVariables.targetStructure.invent_item_col(0) & "` ) VALUES ( '" & newcharguid.ToString() &
                    "', '0', '23', '" & newitemguid.ToString() & "')", True)
                Return True
            Catch ex As Exception
                LogAppend(
                    "Something went wrong while creating the character -> Skipping! -> Error message is: " &
                    ex.ToString(),
                    "CharacterCreationLite_createAtTrinity", False, True)
                MsgBox(MSG_FATALDURINGCHARCREATION, MsgBoxStyle.Critical, MSG_ERROR)
                Return False
            End Try
        End Function

        Private Function CreateAtMangos(charactername As String, accid As Integer, ByRef player As Character,
                                        nameChange As Boolean) As Boolean
            LogAppend("Creating at Mangos", "CharacterCreationLite_createAtMangos", False)
            Dim newcharguid As Integer = TryInt(
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.targetStructure.char_guid_col(0) & " FROM " &
                    GlobalVariables.targetStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.targetStructure.char_guid_col(0) &
                    "=(SELECT MAX(" & GlobalVariables.targetStructure.char_guid_col(0) & ") FROM " &
                    GlobalVariables.targetStructure.character_tbl(0) & ")", True)) + 1
            player.CreatedGuid = newcharguid
            Dim sqlstring As String = "INSERT INTO characters ( `" & GlobalVariables.targetStructure.char_guid_col(0) &
                                      "`, `" & GlobalVariables.targetStructure.char_accountId_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_name_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_race_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_class_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_gender_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_level_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_xp_col(0) &
                                      "`, `" & GlobalVariables.targetStructure.char_gold_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_playerBytes_col(0) & "`, `" &
                                      GlobalVariables.targetStructure.char_posX_col(0) & "`, " &
                                      GlobalVariables.targetStructure.char_posY_col(0) &
                                      ", " & GlobalVariables.targetStructure.char_posZ_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_map_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_orientation_col(0) & ", " &
                                      GlobalVariables.targetStructure.char_taximask_col(0) & ", `health` ) VALUES " &
                                      "( @guid, @accid, @name, @race, @class, @gender, @level, '0', '0', @pBytes, '-14305.7', '514.08', '10', '0', '4.30671', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 ','1000' )"
            Dim tempcommand As New MySqlCommand(sqlstring, GlobalVariables.TargetConnection)
            tempcommand.Parameters.AddWithValue("@accid", accid.ToString())
            tempcommand.Parameters.AddWithValue("@guid", newcharguid.ToString())
            tempcommand.Parameters.AddWithValue("@name", charactername)
            tempcommand.Parameters.AddWithValue("@pBytes", player.PlayerBytes.ToString())
            tempcommand.Parameters.AddWithValue("@class", player.Cclass.ToString())
            tempcommand.Parameters.AddWithValue("@race", player.Race.ToString())
            tempcommand.Parameters.AddWithValue("@gender", player.Gender.ToString())
            tempcommand.Parameters.AddWithValue("@level", player.Level.ToString())
            Try
                tempcommand.ExecuteNonQuery()
                If nameChange = True Then
                    runSQLCommand_characters_string(
                        "UPDATE " & GlobalVariables.targetStructure.character_tbl(0) & " SET " &
                        GlobalVariables.targetStructure.char_atlogin_col(0) & "='1' WHERE " &
                        GlobalVariables.targetStructure.char_guid_col(0) & "='" & newcharguid.ToString &
                        "'", True)
                Else
                    If CharacterExist(charactername) = True Then
                        runSQLCommand_characters_string(
                            "UPDATE " & GlobalVariables.targetStructure.character_tbl(0) & " SET " &
                            GlobalVariables.targetStructure.char_atlogin_col(0) & "='1' WHERE " &
                            GlobalVariables.targetStructure.char_guid_col(0) & "='" & newcharguid.ToString &
                            "'", True)
                    End If
                End If
                '// Special Spells
                GetRaceSpells(player)
                GetClassSpells(player)
                '// Creating hearthstone
                LogAppend("Creating character hearthstone", "CharacterCreationLite_createAtArcemu", False)
                Dim newitemguid As Integer =
                        (TryInt(
                            runSQLCommand_characters_string(
                                "SELECT " & GlobalVariables.targetStructure.itmins_guid_col(0) & " FROM " &
                                GlobalVariables.targetStructure.item_instance_tbl(0) & " WHERE " &
                                GlobalVariables.targetStructure.itmins_guid_col(0) &
                                "=(SELECT MAX(" & GlobalVariables.targetStructure.itmins_guid_col(0) & ") FROM " &
                                GlobalVariables.targetStructure.item_instance_tbl(0) & "))", True)) + 1)
                If GlobalVariables.targetExpansion >= 3 Then
                    runSQLCommand_characters_string(
                        "INSERT INTO " & GlobalVariables.targetStructure.item_instance_tbl(0) & " ( " &
                        GlobalVariables.targetStructure.itmins_guid_col(0) & ", " &
                        GlobalVariables.targetStructure.itmins_ownerGuid_col(0) & ", " &
                        GlobalVariables.targetStructure.itmins_data_col(0) & " ) VALUES ( '" & newitemguid.ToString() &
                        "', '" & accid.ToString() & "', '" & newitemguid.ToString() &
                        " 1191182336 3 6948 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ' )",
                        True)
                Else
                    'MaNGOS < 3.3 Core: watch data length
                    runSQLCommand_characters_string(
                        "INSERT INTO " & GlobalVariables.targetStructure.item_instance_tbl(0) & " ( " &
                        GlobalVariables.targetStructure.itmins_guid_col(0) & ", " &
                        GlobalVariables.targetStructure.itmins_ownerGuid_col(0) & ", " &
                        GlobalVariables.targetStructure.itmins_data_col(0) &
                        " ) VALUES ( '" & newitemguid.ToString() & "', '" & newcharguid.ToString() & "', '" &
                        newitemguid.ToString() &
                        " 1191182336 3 6948 1065353216 0 8 0 8 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ' )",
                        True)
                End If
                runSQLCommand_characters_string(
                    "INSERT INTO " & GlobalVariables.targetStructure.character_inventory_tbl(0) & " ( " &
                    GlobalVariables.targetStructure.invent_guid_col(0) & ", " &
                    GlobalVariables.targetStructure.invent_bag_col(0) & ", " &
                    GlobalVariables.targetStructure.invent_slot_col(0) & ", " &
                    GlobalVariables.targetStructure.invent_item_col(0) & ", " &
                    GlobalVariables.targetStructure.invent_item_template_col(0) & " ) VALUES ( '" &
                    newcharguid.ToString() & "', '0', '23', '" & newitemguid.ToString() & "', '6948')")
                Return True
            Catch ex As Exception
                LogAppend(
                    "Something went wrong while creating the character -> Skipping! -> Error message is: " &
                    ex.ToString(),
                    "CharacterCreationLite_createAtMangos", False, True)
                MsgBox(MSG_FATALDURINGCHARCREATION, MsgBoxStyle.Critical, MSG_ERROR)
                Return False
            End Try
        End Function
    End Class
End Namespace