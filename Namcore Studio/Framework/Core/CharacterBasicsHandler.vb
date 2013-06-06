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
'*      /Filename:      CharacterBasicsHandler
'*      /Description:   Contains functions for extracting information from the characters
'*                      table for a specific character
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Imports Namcore_Studio.EventLogging
Imports Namcore_Studio.Conversions
Imports Namcore_Studio.Basics
Imports Namcore_Studio.GlobalVariables
Imports Namcore_Studio.CommandHandler
Public Class CharacterBasicsHandler
    Private Shared temp_result As String
    Public Shared Sub GetBasicCharacterInformation(ByVal characterGuid As Integer, ByVal setId As Integer, ByVal accountId As Integer)
        LogAppend("Loading basic character information for characterGuid: " & characterGuid & " and setId: " & setId, "CharacterBasicsHandler_GetBasicCharacterInformation", True)
        Select Case sourceCore
            Case "arcemu"
                loadAtArcemu(characterGuid, setId, accountId)
            Case "trinity"
                loadAtTrinity(characterGuid, setId, accountId)
            Case "trinitytbc"
                loadAtTrinityTBC(characterGuid, setId, accountId)
            Case "mangos"
                loadAtMangos(characterGuid, setId, accountId)
            Case Else : End Select

    End Sub
    Private Shared Sub loadAtArcemu(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)

        'Character Table
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_name_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        Dim tmpCharacter As New Character(temp_result, charguid)
        tmpCharacter.SourceCore = "arcemu"
        tmpCharacter.SetIndex = tar_setId
        LogAppend("Loaded character name info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtArcemu", True)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_race_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.Race = TryInt(temp_result)
        LogAppend("Loaded character race info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtArcemu", True)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_class_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.Cclass = TryInt(temp_result)
        LogAppend("Loaded character class info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtArcemu", True)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_gender_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.Gender = TryInt(temp_result)
        LogAppend("Loaded character gender info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtArcemu", True)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_level_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.Level = TryInt(temp_result)
        LogAppend("Loaded character level info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtArcemu", True)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_accountId_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.AccountId = TryInt(temp_result)
        LogAppend("Loaded character accountId info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtArcemu", True)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_xp_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.Xp = TryInt(temp_result)
        LogAppend("Loaded character xp info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtArcemu", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_gold_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.Gold = temp_result
        LogAppend("Loaded character gold info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtArcemu", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_playerBytes_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.PlayerBytes = TryInt(temp_result)
        LogAppend("Loaded character playerBytes info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtArcemu", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_playerBytes2_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.PlayerBytes2 = TryInt(temp_result)
        LogAppend("Loaded character playerBytes2 info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtArcemu", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_playerFlags_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.PlayerFlags = TryInt(temp_result)
        LogAppend("Loaded character playerFlags info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtArcemu", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_posX_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.PositionX = TryInt(temp_result)
        LogAppend("Loaded character posX info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtArcemu", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_posY_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.PositionY = TryInt(temp_result)
        LogAppend("Loaded character posY info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtArcemu", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_posZ_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.PositionZ = TryInt(temp_result)
        LogAppend("Loaded character posZ info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtArcemu", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_map_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.Map = TryInt(temp_result)
        LogAppend("Loaded character map info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtArcemu", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_instanceId_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.InstanceId = TryInt(temp_result)
        LogAppend("Loaded character instanceId info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtArcemu", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_orientation_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.Orientation = TryInt(temp_result)
        LogAppend("Loaded character orientation info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtArcemu", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_taximask_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.Taximask = temp_result
        LogAppend("Loaded character taximask info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtArcemu", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_cinematic_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.Cinematic = TryInt(temp_result)
        LogAppend("Loaded character cinematic info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtArcemu", False)
        Dim parts() As String = (runSQLCommand_characters_string("SELECT " & sourceStructure.char_arcemuPlayedTime_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")).Split(" "c)
        tmpCharacter.TotalTime = TryInt(parts(0))
        LogAppend("Loaded character totaltime info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & parts(0), "CharacterBasicsHandler_LoadAtArcemu", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_stableSlots_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.StableSlots = TryInt(temp_result)
        LogAppend("Loaded character stableSlots info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtArcemu", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_zone_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.Zone = TryInt(temp_result)
        LogAppend("Loaded character zone info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtArcemu", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_arenaPoints_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.ArenaPoints = TryInt(temp_result)
        LogAppend("Loaded character arenaPoints info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtArcemu", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_totalHonorPoints_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.TotalHonorPoints = TryInt(temp_result)
        LogAppend("Loaded character totalHonorPoints info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtArcemu", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_totalKills_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.TotalKills = TryInt(temp_result)
        LogAppend("Loaded character totalKills info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtArcemu", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_chosenTitle_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.ChosenTitle = TryInt(temp_result)
        LogAppend("Loaded character chosenTitle info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtArcemu", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_watchedFaction_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.WatchedFaction = TryInt(temp_result)
        LogAppend("Loaded character watchedFaction info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtArcemu", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_health_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.Health = TryInt(temp_result)
        LogAppend("Loaded character healt info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtArcemu", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_speccount_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.SpecCount = TryInt(temp_result)
        LogAppend("Loaded character speccount info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtArcemu", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_activeSpec_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.ActiveSpec = TryInt(temp_result)
        LogAppend("Loaded character activeSpec info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtArcemu", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_exploredZones_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.ExploredZones = temp_result
        LogAppend("Loaded character exploredZones info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtArcemu", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_knownTitles_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.KnownTitles = temp_result
        LogAppend("Loaded character knownTitles info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtArcemu", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_arcemuTalentPoints_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.ArcEmuTalentPoints = temp_result
        LogAppend("Loaded character arcemuTalentPoints info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtArcemu", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_finishedQuests_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.FinishedQuests = temp_result
        LogAppend("Loaded character finishedQuests info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtArcemu", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_customFaction_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.CustomFaction = TryInt(temp_result)
        LogAppend("Loaded character customFaction info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtArcemu", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_bindmapid_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.BindMapId = TryInt(temp_result)
        LogAppend("Loaded character bindmapid info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtArcemu", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_bindzoneid_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.BindZoneId = TryInt(temp_result)
        LogAppend("Loaded character bindzoneid info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtArcemu", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_bindposX_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.BindPositionX = TryInt(temp_result)
        LogAppend("Loaded character bindposX info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtArcemu", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_bindposY_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.BindPositionY = TryInt(temp_result)
        LogAppend("Loaded character bindposY info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtArcemu", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_bindposZ_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.BindPositionZ = TryInt(temp_result)
        LogAppend("Loaded character bindposZ info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtArcemu", False)
        tmpCharacter.HomeBind = "<map>" & tmpCharacter.BindMapId.ToString() & "</map><zone>" & tmpCharacter.BindZoneId.ToString() & "</zone><position_x>" & tmpCharacter.BindPositionX.ToString() & "</position_x><position_y>" &
                                         tmpCharacter.BindPositionY.ToString() & "</position_y><position_z>" & tmpCharacter.BindPositionZ.ToString() & "</position_z>"
        'Account Table
        temp_result = runSQLCommand_realm_string("SELECT " & sourceStructure.acc_name_col(0) & " FROM " & sourceStructure.account_tbl(0) & " WHERE " & sourceStructure.acc_id_col(0)(0) & "='" & tar_accountId.ToString & "'")
        tmpCharacter.AccountName = temp_result
        LogAppend("Loaded account name info for accountId: " & tar_accountId.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtArcemu", True)
        temp_result = runSQLCommand_realm_string("SELECT " & sourceStructure.acc_arcemuPass_col(0) & " FROM " & sourceStructure.account_tbl(0) & " WHERE " & sourceStructure.acc_id_col(0)(0) & "='" & tar_accountId.ToString & "'")
        tmpCharacter.ArcEmuPass = temp_result
        LogAppend("Loaded account arcemuPass info for accountId: " & tar_accountId.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtArcemu", False)
        temp_result = runSQLCommand_realm_string("SELECT " & sourceStructure.acc_passHash_col(0) & " FROM " & sourceStructure.account_tbl(0) & " WHERE " & sourceStructure.acc_id_col(0)(0) & "='" & tar_accountId.ToString & "'")
        tmpCharacter.PassHash = temp_result
        LogAppend("Loaded account passHash info for accountId: " & tar_accountId.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtArcemu", False)
        temp_result = runSQLCommand_realm_string("SELECT " & sourceStructure.acc_arcemuFlags_col(0) & " FROM " & sourceStructure.account_tbl(0) & " WHERE " & sourceStructure.acc_id_col(0)(0) & "='" & tar_accountId.ToString & "'")
        tmpCharacter.ArcEmuFlags = TryInt(temp_result)
        LogAppend("Loaded account arcemuFlags info for accountId: " & tar_accountId.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtArcemu", False)
        temp_result = runSQLCommand_realm_string("SELECT " & sourceStructure.acc_locale_col(0) & " FROM " & sourceStructure.account_tbl(0) & " WHERE " & sourceStructure.acc_id_col(0)(0) & "='" & tar_accountId.ToString & "'")
        tmpCharacter.Locale = TryInt(temp_result)
        LogAppend("Loaded account locale info for accountId: " & tar_accountId.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtArcemu", False)
        temp_result = runSQLCommand_realm_string("SELECT " & sourceStructure.acc_arcemuGmLevel_col(0) & " FROM " & sourceStructure.account_tbl(0) & " WHERE " & sourceStructure.acc_id_col(0)(0) & "='" & tar_accountId.ToString & "'")
        tmpCharacter.ArcEmuGmLevel = temp_result
        LogAppend("Loaded account arcemuGmLevel info for accountId: " & tar_accountId.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtArcemu", False)
        'todo Expansion!
        Dim templevel As Integer
        Select Case temp_result
            Case "AZ"
                templevel = 4
            Case "A"
                templevel = 3
            Case "0"
                templevel = 0
            Case Else
                templevel = 0
        End Select
        tmpCharacter.GmLevel = templevel
        tmpCharacter.RealmId = 0 'TODO multi realm support

        CharacterSets.Add(tmpCharacter)
        CharacterSetsIndex = CharacterSetsIndex & "[setId:" & tar_setId.ToString & "|@" & (CharacterSets.Count - 1).ToString() & "]"
    End Sub
    Private Shared Sub loadAtTrinity(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)

        'Character Table
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_name_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        Dim tmpCharacter As New Character(temp_result, charguid)
        tmpCharacter.SourceCore = "trinity"
        tmpCharacter.SetIndex = tar_setId
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_race_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.Race = TryInt(temp_result)
        LogAppend("Loaded character race info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", True)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_race_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.Cclass = TryInt(temp_result)
        LogAppend("Loaded character class info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", True)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_gender_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.Gender = TryInt(temp_result)
        LogAppend("Loaded character gender info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", True)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_level_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.Level = TryInt(temp_result)
        LogAppend("Loaded character level info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", True)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_accountId_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.AccountId = TryInt(temp_result)
        LogAppend("Loaded character accountId info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", True)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_xp_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.Xp = TryInt(temp_result)
        LogAppend("Loaded character xp info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_gold_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.Gold = temp_result
        LogAppend("Loaded character gold info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_playerBytes_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.PlayerBytes = TryInt(temp_result)
        LogAppend("Loaded character playerBytes info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_playerBytes2_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.PlayerBytes2 = TryInt(temp_result)
        LogAppend("Loaded character playerBytes2 info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_playerFlags_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.PlayerFlags = TryInt(temp_result)
        LogAppend("Loaded character playerFlags info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_posX_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.PositionX = TryInt(temp_result)
        LogAppend("Loaded character posX info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_posY_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.PositionY = TryInt(temp_result)
        LogAppend("Loaded character posY info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_posZ_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.PositionZ = TryInt(temp_result)
        LogAppend("Loaded character posZ info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_map_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.Map = TryInt(temp_result)
        LogAppend("Loaded character map info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_instanceId_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.InstanceId = TryInt(temp_result)
        LogAppend("Loaded character instanceId info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_instanceModeMask_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.instanceModeMask = TryInt(temp_result)
        LogAppend("Loaded character instanceModeMask info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_orientation_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.Orientation = TryInt(temp_result)
        LogAppend("Loaded character orientation info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_taximask_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.Taximask = temp_result
        LogAppend("Loaded character taximask info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_cinematic_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.Cinematic = TryInt(temp_result)
        LogAppend("Loaded character cinematic info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_totaltime_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.TotalTime = TryInt(temp_result)
        LogAppend("Loaded character totaltime info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_leveltime_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.leveltime = TryInt(temp_result)
        LogAppend("Loaded character leveltime info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_extraFlags_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.ExtraFlags = TryInt(temp_result)
        LogAppend("Loaded character extraFlags info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_stableSlots_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.StableSlots = TryInt(temp_result)
        LogAppend("Loaded character stableSlots info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_atlogin_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.atlogin = TryInt(temp_result)
        LogAppend("Loaded character atlogin info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_zone_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.Zone = TryInt(temp_result)
        LogAppend("Loaded character zone info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_arenaPoints_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.ArenaPoints = TryInt(temp_result)
        LogAppend("Loaded character arenaPoints info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_totalHonorPoints_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.TotalHonorPoints = TryInt(temp_result)
        LogAppend("Loaded character totalHonorPoints info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_totalKills_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.TotalKills = TryInt(temp_result)
        LogAppend("Loaded character totalKills info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_chosenTitle_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.ChosenTitle = TryInt(temp_result)
        LogAppend("Loaded character chosenTitle info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_knownCurrencies_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.knownCurrencies = TryInt(temp_result)
        LogAppend("Loaded character knownCurrencies info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_watchedFaction_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.WatchedFaction = TryInt(temp_result)
        LogAppend("Loaded character watchedFaction info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_health_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.Health = TryInt(temp_result)
        LogAppend("Loaded character healt info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_speccount_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.SpecCount = TryInt(temp_result)
        LogAppend("Loaded character speccount info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_activeSpec_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.ActiveSpec = TryInt(temp_result)
        LogAppend("Loaded character activeSpec info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_exploredZones_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.ExploredZones = temp_result
        LogAppend("Loaded character exploredZones info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_knownTitles_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.KnownTitles = temp_result
        LogAppend("Loaded character knownTitles info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_actionBars_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.actionBars = TryInt(temp_result)
        LogAppend("Loaded character actionBars info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_finishedQuests_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.FinishedQuests = temp_result
        LogAppend("Loaded character finishedQuests info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_customFaction_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.CustomFaction = TryInt(temp_result)
        LogAppend("Loaded character customFaction info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)

        'Character_homebind Table
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.homebind_map_col(0) & " FROM " & sourceStructure.character_homebind_tbl(0) & " WHERE " & sourceStructure.homebind_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.BindMapId = TryInt(temp_result)
        LogAppend("Loaded homebind bindmapid info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.homebind_zone_col(0) & " FROM " & sourceStructure.character_homebind_tbl(0) & " WHERE " & sourceStructure.homebind_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.BindZoneId = TryInt(temp_result)
        LogAppend("Loaded homebind bindzoneid info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.homebind_posx_col(0) & " FROM " & sourceStructure.character_homebind_tbl(0) & " WHERE " & sourceStructure.homebind_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.BindPositionX = TryInt(temp_result)
        LogAppend("Loaded homebind bindposX info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.homebind_posy_col(0) & " FROM " & sourceStructure.character_homebind_tbl(0) & " WHERE " & sourceStructure.homebind_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.BindPositionY = TryInt(temp_result)
        LogAppend("Loaded homebind bindposY info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.homebind_posz_col(0) & " FROM " & sourceStructure.character_homebind_tbl(0) & " WHERE " & sourceStructure.homebind_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.BindPositionZ = TryInt(temp_result)
        LogAppend("Loaded homebind bindposZ info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)
        tmpCharacter.HomeBind = "<map>" & tmpCharacter.BindMapId.ToString() & "</map><zone>" & tmpCharacter.BindZoneId.ToString() & "</zone><position_x>" & tmpCharacter.BindPositionX.ToString() & "</position_x><position_y>" &
                                         tmpCharacter.BindPositionY.ToString() & "</position_y><position_z>" & tmpCharacter.BindPositionZ.ToString() & "</position_z>"


        'Account Table
        temp_result = runSQLCommand_realm_string("SELECT " & sourceStructure.acc_name_col(0) & " FROM " & sourceStructure.account_tbl(0) & " WHERE " & sourceStructure.acc_id_col(0)(0) & "='" & tar_accountId.ToString & "'")
        tmpCharacter.AccountName = temp_result
        LogAppend("Loaded account name info for accountId: " & tar_accountId.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", True)
        temp_result = runSQLCommand_realm_string("SELECT " & sourceStructure.acc_passHash_col(0) & " FROM " & sourceStructure.account_tbl(0) & " WHERE " & sourceStructure.acc_id_col(0)(0) & "='" & tar_accountId.ToString & "'")
        tmpCharacter.PassHash = temp_result
        LogAppend("Loaded account passHash info for accountId: " & tar_accountId.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)
        temp_result = runSQLCommand_realm_string("SELECT " & sourceStructure.acc_sessionkey_col(0) & " FROM " & sourceStructure.account_tbl(0) & " WHERE " & sourceStructure.acc_id_col(0)(0) & "='" & tar_accountId.ToString & "'")
        tmpCharacter.sessionkey = temp_result
        LogAppend("Loaded account sessionkey info for accountId: " & tar_accountId.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)
        temp_result = runSQLCommand_realm_string("SELECT " & sourceStructure.acc_locale_col(0) & " FROM " & sourceStructure.account_tbl(0) & " WHERE " & sourceStructure.acc_id_col(0)(0) & "='" & tar_accountId.ToString & "'")
        tmpCharacter.Locale = TryInt(temp_result)
        LogAppend("Loaded account locale info for accountId: " & tar_accountId.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)
        temp_result = runSQLCommand_realm_string("SELECT " & sourceStructure.acc_joindate_col(0) & " FROM " & sourceStructure.account_tbl(0) & " WHERE " & sourceStructure.acc_id_col(0)(0) & "='" & tar_accountId.ToString & "'")
        tmpCharacter.joindate = TryInt(temp_result)
        LogAppend("Loaded account joindate info for accountId: " & tar_accountId.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)
        'todo Expansion!
        'Account Access Table
        temp_result = runSQLCommand_realm_string("SELECT " & sourceStructure.accAcc_gmLevel_col(0) & " FROM " & sourceStructure.accountAccess_tbl(0) & " WHERE " & sourceStructure.accAcc_accid_col(0) & "='" & tar_accountId.ToString & "'")
        tmpCharacter.GmLevel = TryInt(temp_result)
        LogAppend("Loaded accountAccess gmlevel info for accountId: " & tar_accountId.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)
        temp_result = runSQLCommand_realm_string("SELECT " & sourceStructure.accAcc_realmId_col(0) & " FROM " & sourceStructure.accountAccess_tbl(0) & " WHERE " & sourceStructure.accAcc_accid_col(0) & "='" & tar_accountId.ToString & "'")
        tmpCharacter.RealmId = TryInt(temp_result)
        LogAppend("Loaded accountAccess realmId info for accountId: " & tar_accountId.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)

        CharacterSets.Add(tmpCharacter)
        CharacterSetsIndex = CharacterSetsIndex & "[setId:" & tar_setId.ToString & "|@" & (CharacterSets.Count - 1).ToString() & "]"
    End Sub
    Private Shared Sub loadAtTrinityTBC(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_name_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        Dim tmpCharacter As New Character(temp_result, charguid)
        tmpCharacter.SourceCore = "trinitytbc"
        tmpCharacter.SetIndex = tar_setId

        'Character Table
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_race_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.race = TryInt(temp_result)
        LogAppend("Loaded character race info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", True)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_race_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.Cclass = TryInt(temp_result)
        LogAppend("Loaded character class info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", True)
        temp_result = runSQLCommand_characters_string("SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 242), ' ', -1) AS UNSIGNED) AS `gender` FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "'")
        tmpCharacter.gender = TryInt(temp_result)
        LogAppend("Loaded character gender info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", True)
        temp_result = runSQLCommand_characters_string("SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 35), ' ', -1) AS UNSIGNED) AS `level` FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.level = TryInt(temp_result)
        LogAppend("Loaded character level info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", True)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_accountId_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.accountId = TryInt(temp_result)
        LogAppend("Loaded character accountId info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", True)
        temp_result = runSQLCommand_characters_string("SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 927), ' ', -1) AS UNSIGNED) AS `xp` FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.xp = TryInt(temp_result)
        LogAppend("Loaded character xp info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
        temp_result = runSQLCommand_characters_string("SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 1398), ' ', -1) AS UNSIGNED) AS `money` FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.Gold = temp_result
        LogAppend("Loaded character gold info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
        temp_result = runSQLCommand_characters_string("SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 240), ' ', -1) AS UNSIGNED) AS `playerBytes` FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.playerBytes = TryInt(temp_result)
        LogAppend("Loaded character playerBytes info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
        temp_result = runSQLCommand_characters_string("SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 241), ' ', -1) AS UNSIGNED) AS `playerBytes2` FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.playerBytes2 = TryInt(temp_result)
        LogAppend("Loaded character playerBytes2 info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
        temp_result = runSQLCommand_characters_string("SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 237), ' ', -1) AS UNSIGNED) AS `playerFlags` FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.playerFlags = TryInt(temp_result)
        LogAppend("Loaded character playerFlags info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_posX_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.PositionX = TryInt(temp_result)
        LogAppend("Loaded character posX info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_posY_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.PositionY = TryInt(temp_result)
        LogAppend("Loaded character posY info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_posZ_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.PositionZ = TryInt(temp_result)
        LogAppend("Loaded character posZ info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_map_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.map = TryInt(temp_result)
        LogAppend("Loaded character map info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_instanceId_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.instanceId = TryInt(temp_result)
        LogAppend("Loaded character instanceId info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_instanceModeMask_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.instanceModeMask = TryInt(temp_result)
        LogAppend("Loaded character instanceModeMask info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_orientation_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.orientation = TryInt(temp_result)
        LogAppend("Loaded character orientation info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_taximask_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.Taximask = temp_result
        LogAppend("Loaded character taximask info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_cinematic_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.cinematic = TryInt(temp_result)
        LogAppend("Loaded character cinematic info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_totaltime_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.totaltime = TryInt(temp_result)
        LogAppend("Loaded character totaltime info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_leveltime_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.leveltime = TryInt(temp_result)
        LogAppend("Loaded character leveltime info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_extraFlags_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.extraFlags = TryInt(temp_result)
        LogAppend("Loaded character extraFlags info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_stableSlots_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.stableSlots = TryInt(temp_result)
        LogAppend("Loaded character stableSlots info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_atlogin_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.atlogin = TryInt(temp_result)
        LogAppend("Loaded character atlogin info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_zone_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.zone = TryInt(temp_result)
        LogAppend("Loaded character zone info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
        temp_result = runSQLCommand_characters_string("SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 1500), ' ', -1) AS UNSIGNED) AS `arenaPoints` FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.arenaPoints = TryInt(temp_result)
        LogAppend("Loaded character arenaPoints info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
        temp_result = runSQLCommand_characters_string("SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 1499), ' ', -1) AS UNSIGNED) AS `totalHonorPoints` FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.totalHonorPoints = TryInt(temp_result)
        LogAppend("Loaded character totalHonorPoints info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
        temp_result = runSQLCommand_characters_string("SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 1451), ' ', -1) AS UNSIGNED) AS `totalHonorPoints` FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.totalKills = TryInt(temp_result)
        LogAppend("Loaded character totalKills info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
        temp_result = runSQLCommand_characters_string("SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 649), ' ', -1) AS UNSIGNED) AS `chosenTitle` FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.chosenTitle = TryInt(temp_result)
        LogAppend("Loaded character chosenTitle info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
        temp_result = runSQLCommand_characters_string("SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 1456), ' ', -1) AS UNSIGNED) AS `watchedFaction` FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.knownCurrencies = TryInt(temp_result)
        LogAppend("Loaded character knownCurrencies info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_watchedFaction_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.watchedFaction = TryInt(temp_result)
        LogAppend("Loaded character watchedFaction info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
        temp_result = runSQLCommand_characters_string("SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 23), ' ', -1) AS UNSIGNED) AS `health` FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.health = TryInt(temp_result)
        LogAppend("Loaded character healt info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_speccount_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.speccount = TryInt(temp_result)
        LogAppend("Loaded character speccount info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
        temp_result = runSQLCommand_characters_string("SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 1333), ' ', -1) AS UNSIGNED) AS `exploredZones` FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.activeSpec = TryInt(temp_result)
        LogAppend("Loaded character activeSpec info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_exploredZones_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.ExploredZones = temp_result
        LogAppend("Loaded character exploredZones info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
        temp_result = runSQLCommand_characters_string("SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 925), ' ', -1) AS UNSIGNED) AS `knownTitles` FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.KnownTitles = temp_result
        LogAppend("Loaded character knownTitles info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_actionBars_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.actionBars = TryInt(temp_result)
        LogAppend("Loaded character actionBars info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_finishedQuests_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.FinishedQuests = temp_result
        LogAppend("Loaded character finishedQuests info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_customFaction_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.customFaction = TryInt(temp_result)
        LogAppend("Loaded character customFaction info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", False)

        'Character_homebind Table
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.homebind_map_col(0) & " FROM " & sourceStructure.character_homebind_tbl(0) & " WHERE " & sourceStructure.homebind_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.bindmapid = TryInt(temp_result)
        LogAppend("Loaded homebind bindmapid info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.homebind_zone_col(0) & " FROM " & sourceStructure.character_homebind_tbl(0) & " WHERE " & sourceStructure.homebind_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.bindzoneid = TryInt(temp_result)
        LogAppend("Loaded homebind bindzoneid info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.homebind_posx_col(0) & " FROM " & sourceStructure.character_homebind_tbl(0) & " WHERE " & sourceStructure.homebind_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.BindPositionX = TryInt(temp_result)
        LogAppend("Loaded homebind bindposX info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.homebind_posy_col(0) & " FROM " & sourceStructure.character_homebind_tbl(0) & " WHERE " & sourceStructure.homebind_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.BindPositionY = TryInt(temp_result)
        LogAppend("Loaded homebind bindposY info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.homebind_posz_col(0) & " FROM " & sourceStructure.character_homebind_tbl(0) & " WHERE " & sourceStructure.homebind_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.BindPositionZ = TryInt(temp_result)
        LogAppend("Loaded homebind bindposZ info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
        tmpCharacter.HomeBind = "<map>" & tmpCharacter.BindMapId.ToString() & "</map><zone>" & tmpCharacter.BindZoneId.ToString() & "</zone><position_x>" & tmpCharacter.BindPositionX.ToString() & "</position_x><position_y>" &
                                              tmpCharacter.BindPositionY.ToString() & "</position_y><position_z>" & tmpCharacter.BindPositionZ.ToString() & "</position_z>"

        
        'Account Table
        temp_result = runSQLCommand_realm_string("SELECT " & sourceStructure.acc_name_col(0) & " FROM " & sourceStructure.account_tbl(0) & " WHERE " & sourceStructure.acc_id_col(0)(0) & "='" & tar_accountId.ToString & "'")
        tmpCharacter.AccountName = temp_result
        LogAppend("Loaded account name info for accountId: " & tar_accountId.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", True)
        temp_result = runSQLCommand_realm_string("SELECT " & sourceStructure.acc_passHash_col(0) & " FROM " & sourceStructure.account_tbl(0) & " WHERE " & sourceStructure.acc_id_col(0)(0) & "='" & tar_accountId.ToString & "'")
        tmpCharacter.PassHash = temp_result
        LogAppend("Loaded account passHash info for accountId: " & tar_accountId.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
        temp_result = runSQLCommand_realm_string("SELECT " & sourceStructure.acc_sessionkey_col(0) & " FROM " & sourceStructure.account_tbl(0) & " WHERE " & sourceStructure.acc_id_col(0)(0) & "='" & tar_accountId.ToString & "'")
        tmpCharacter.SessionKey = temp_result
        LogAppend("Loaded account sessionkey info for accountId: " & tar_accountId.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
        temp_result = runSQLCommand_realm_string("SELECT " & sourceStructure.acc_locale_col(0) & " FROM " & sourceStructure.account_tbl(0) & " WHERE " & sourceStructure.acc_id_col(0)(0) & "='" & tar_accountId.ToString & "'")
        tmpCharacter.locale = TryInt(temp_result)
        LogAppend("Loaded account locale info for accountId: " & tar_accountId.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
        temp_result = runSQLCommand_realm_string("SELECT " & sourceStructure.acc_joindate_col(0) & " FROM " & sourceStructure.account_tbl(0) & " WHERE " & sourceStructure.acc_id_col(0)(0) & "='" & tar_accountId.ToString & "'")
        tmpCharacter.joindate = TryInt(temp_result)
        LogAppend("Loaded account joindate info for accountId: " & tar_accountId.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
        temp_result = runSQLCommand_realm_string("SELECT " & sourceStructure.acc_v_col(0) & " FROM " & sourceStructure.account_tbl(0) & " WHERE " & sourceStructure.acc_id_col(0)(0) & "='" & tar_accountId.ToString & "'")
        tmpCharacter.V = temp_result
        LogAppend("Loaded account v info for accountId: " & tar_accountId.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)
        temp_result = runSQLCommand_realm_string("SELECT " & sourceStructure.acc_s_col(0) & " FROM " & sourceStructure.account_tbl(0) & " WHERE " & sourceStructure.acc_id_col(0)(0) & "='" & tar_accountId.ToString & "'")
        tmpCharacter.S = temp_result
        LogAppend("Loaded account s info for accountId: " & tar_accountId.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)
        'todo Expansion!
        'Account Access Table
        temp_result = runSQLCommand_realm_string("SELECT " & sourceStructure.accAcc_gmLevel_col(0) & " FROM " & sourceStructure.accountAccess_tbl(0) & " WHERE " & sourceStructure.accAcc_accid_col(0) & "='" & tar_accountId.ToString & "'")
        tmpCharacter.gmlevel = TryInt(temp_result)
        LogAppend("Loaded accountAccess gmlevel info for accountId: " & tar_accountId.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
        temp_result = runSQLCommand_realm_string("SELECT " & sourceStructure.accAcc_realmId_col(0) & " FROM " & sourceStructure.accountAccess_tbl(0) & " WHERE " & sourceStructure.accAcc_accid_col(0) & "='" & tar_accountId.ToString & "'")
        tmpCharacter.realmId = TryInt(temp_result)
        LogAppend("Loaded accountAccess realmId info for accountId: " & tar_accountId.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinityTBC", False)

        CharacterSets.Add(tmpCharacter)
        CharacterSetsIndex = CharacterSetsIndex & "[setId:" & tar_setId.ToString & "|@" & (CharacterSets.Count - 1).ToString() & "]"
    End Sub
    Private Shared Sub loadAtMangos(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_name_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        Dim tmpCharacter As New Character(temp_result, charguid)
        tmpCharacter.SourceCore = "mangos"
        tmpCharacter.SetIndex = tar_setId

        'Character Table
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_race_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.race = TryInt(temp_result)
        LogAppend("Loaded character race info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", True)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_race_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.Cclass = TryInt(temp_result)
        LogAppend("Loaded character class info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", True)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_gender_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.gender = TryInt(temp_result)
        LogAppend("Loaded character gender info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", True)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_level_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.level = TryInt(temp_result)
        LogAppend("Loaded character level info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", True)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_name_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.accountId = TryInt(temp_result)
        LogAppend("Loaded character accountId info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", True)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_xp_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.xp = TryInt(temp_result)
        LogAppend("Loaded character xp info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_gold_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.Gold = temp_result
        LogAppend("Loaded character gold info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_playerBytes_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.playerBytes = TryInt(temp_result)
        LogAppend("Loaded character playerBytes info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_playerBytes2_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.playerBytes2 = TryInt(temp_result)
        LogAppend("Loaded character playerBytes2 info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_playerFlags_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.playerFlags = TryInt(temp_result)
        LogAppend("Loaded character playerFlags info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_posX_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.PositionX = TryInt(temp_result)
        LogAppend("Loaded character posX info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_posY_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.PositionY = TryInt(temp_result)
        LogAppend("Loaded character posY info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_posZ_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.PositionZ = TryInt(temp_result)
        LogAppend("Loaded character posZ info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_map_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.map = TryInt(temp_result)
        LogAppend("Loaded character map info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_instanceId_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.instanceId = TryInt(temp_result)
        LogAppend("Loaded character instanceId info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_instanceModeMask_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.instanceModeMask = TryInt(temp_result)
        LogAppend("Loaded character instanceModeMask info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_orientation_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.orientation = TryInt(temp_result)
        LogAppend("Loaded character orientation info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_taximask_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.Taximask = temp_result
        LogAppend("Loaded character taximask info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_cinematic_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.cinematic = TryInt(temp_result)
        LogAppend("Loaded character cinematic info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_totaltime_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.totaltime = TryInt(temp_result)
        LogAppend("Loaded character totaltime info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_leveltime_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.leveltime = TryInt(temp_result)
        LogAppend("Loaded character leveltime info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_extraFlags_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.extraFlags = TryInt(temp_result)
        LogAppend("Loaded character extraFlags info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_stableSlots_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.stableSlots = TryInt(temp_result)
        LogAppend("Loaded character stableSlots info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_atlogin_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.atlogin = TryInt(temp_result)
        LogAppend("Loaded character atlogin info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_zone_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.zone = TryInt(temp_result)
        LogAppend("Loaded character zone info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_arenaPoints_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.arenaPoints = TryInt(temp_result)
        LogAppend("Loaded character arenaPoints info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_totalHonorPoints_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.totalHonorPoints = TryInt(temp_result)
        LogAppend("Loaded character totalHonorPoints info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_totalKills_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.totalKills = TryInt(temp_result)
        LogAppend("Loaded character totalKills info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_chosenTitle_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.chosenTitle = TryInt(temp_result)
        LogAppend("Loaded character chosenTitle info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_knownCurrencies_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.knownCurrencies = TryInt(temp_result)
        LogAppend("Loaded character knownCurrencies info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_watchedFaction_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.watchedFaction = TryInt(temp_result)
        LogAppend("Loaded character watchedFaction info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_health_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.health = TryInt(temp_result)
        LogAppend("Loaded character healt info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_speccount_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.speccount = TryInt(temp_result)
        LogAppend("Loaded character speccount info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_activeSpec_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.activeSpec = TryInt(temp_result)
        LogAppend("Loaded character activeSpec info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_exploredZones_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.ExploredZones = temp_result
        LogAppend("Loaded character exploredZones info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_knownTitles_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.KnownTitles = temp_result
        LogAppend("Loaded character knownTitles info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_actionBars_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.actionBars = TryInt(temp_result)
        LogAppend("Loaded character actionBars info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_finishedQuests_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.FinishedQuests = temp_result
        LogAppend("Loaded character finishedQuests info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_customFaction_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.customFaction = TryInt(temp_result)
        LogAppend("Loaded character customFaction info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)

        'Character_homebind Table
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.homebind_map_col(0) & " FROM " & sourceStructure.character_homebind_tbl(0) & " WHERE " & sourceStructure.homebind_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.bindmapid = TryInt(temp_result)
        LogAppend("Loaded homebind bindmapid info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.homebind_zone_col(0) & " FROM " & sourceStructure.character_homebind_tbl(0) & " WHERE " & sourceStructure.homebind_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.bindzoneid = TryInt(temp_result)
        LogAppend("Loaded homebind bindzoneid info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.homebind_posx_col(0) & " FROM " & sourceStructure.character_homebind_tbl(0) & " WHERE " & sourceStructure.homebind_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.BindPositionX = TryInt(temp_result)
        LogAppend("Loaded homebind bindposX info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.homebind_posy_col(0) & " FROM " & sourceStructure.character_homebind_tbl(0) & " WHERE " & sourceStructure.homebind_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.BindPositionY = TryInt(temp_result)
        LogAppend("Loaded homebind bindposY info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)
        temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.homebind_posz_col(0) & " FROM " & sourceStructure.character_homebind_tbl(0) & " WHERE " & sourceStructure.homebind_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.BindPositionZ = TryInt(temp_result)
        LogAppend("Loaded homebind bindposZ info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)
        tmpCharacter.HomeBind = "<map>" & tmpCharacter.BindMapId.ToString() & "</map><zone>" & tmpCharacter.BindZoneId.ToString() & "</zone><position_x>" & tmpCharacter.BindPositionX.ToString() & "</position_x><position_y>" &
                                                     tmpCharacter.BindPositionY.ToString() & "</position_y><position_z>" & tmpCharacter.BindPositionZ.ToString() & "</position_z>"


        'Account Table
        temp_result = runSQLCommand_realm_string("SELECT " & sourceStructure.acc_name_col(0) & " FROM " & sourceStructure.account_tbl(0) & " WHERE " & sourceStructure.acc_id_col(0)(0) & "='" & tar_accountId.ToString & "'")
        tmpCharacter.AccountName = temp_result
        LogAppend("Loaded account name info for accountId: " & tar_accountId.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", True)
        temp_result = runSQLCommand_realm_string("SELECT " & sourceStructure.acc_passHash_col(0) & " FROM " & sourceStructure.account_tbl(0) & " WHERE " & sourceStructure.acc_id_col(0)(0) & "='" & tar_accountId.ToString & "'")
        tmpCharacter.PassHash = temp_result
        LogAppend("Loaded account passHash info for accountId: " & tar_accountId.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)
        temp_result = runSQLCommand_realm_string("SELECT " & sourceStructure.acc_v_col(0) & " FROM " & sourceStructure.account_tbl(0) & " WHERE " & sourceStructure.acc_id_col(0)(0) & "='" & tar_accountId.ToString & "'")
        tmpCharacter.V = temp_result
        LogAppend("Loaded account v info for accountId: " & tar_accountId.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)
        temp_result = runSQLCommand_realm_string("SELECT " & sourceStructure.acc_s_col(0) & " FROM " & sourceStructure.account_tbl(0) & " WHERE " & sourceStructure.acc_id_col(0)(0) & "='" & tar_accountId.ToString & "'")
        tmpCharacter.S = temp_result
        LogAppend("Loaded account s info for accountId: " & tar_accountId.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)
        temp_result = runSQLCommand_realm_string("SELECT " & sourceStructure.acc_sessionkey_col(0) & " FROM " & sourceStructure.account_tbl(0) & " WHERE " & sourceStructure.acc_id_col(0)(0) & "='" & tar_accountId.ToString & "'")
        tmpCharacter.SessionKey = temp_result
        LogAppend("Loaded account sessionkey info for accountId: " & tar_accountId.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)
        temp_result = runSQLCommand_realm_string("SELECT " & sourceStructure.acc_locale_col(0) & " FROM " & sourceStructure.account_tbl(0) & " WHERE " & sourceStructure.acc_id_col(0)(0) & "='" & tar_accountId.ToString & "'")
        tmpCharacter.locale = TryInt(temp_result)
        LogAppend("Loaded account locale info for accountId: " & tar_accountId.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)
        temp_result = runSQLCommand_realm_string("SELECT " & sourceStructure.acc_joindate_col(0) & " FROM " & sourceStructure.account_tbl(0) & " WHERE " & sourceStructure.acc_id_col(0)(0) & "='" & tar_accountId.ToString & "'")
        tmpCharacter.joindate = TryInt(temp_result)
        LogAppend("Loaded account joindate info for accountId: " & tar_accountId.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)
        temp_result = runSQLCommand_realm_string("SELECT " & sourceStructure.accAcc_gmLevel_col(0) & " FROM " & sourceStructure.account_tbl(0) & " WHERE " & sourceStructure.acc_id_col(0)(0) & "='" & tar_accountId.ToString & "'")
        tmpCharacter.gmlevel = TryInt(temp_result)
        LogAppend("Loaded account gmlevel info for accountId: " & tar_accountId.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtMangos", False)
        temp_result = runSQLCommand_realm_string("SELECT " & sourceStructure.accAcc_realmId_col(0) & " FROM " & sourceStructure.account_tbl(0) & " WHERE " & sourceStructure.acc_id_col(0)(0) & "='" & tar_accountId.ToString & "'")
        tmpCharacter.realmId = TryInt(temp_result)
        LogAppend("Loaded account realmId info for accountId: " & tar_accountId.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)
        'todo Expansion!
        CharacterSets.Add(tmpCharacter)
        CharacterSetsIndex = CharacterSetsIndex & "[setId:" & tar_setId.ToString & "|@" & (CharacterSets.Count - 1).ToString() & "]"
    End Sub
End Class
