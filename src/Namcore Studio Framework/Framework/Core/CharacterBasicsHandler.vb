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
'*      /Filename:      CharacterBasicsHandler
'*      /Description:   Contains functions for extracting information from the characters
'*                      table for a specific character
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Database
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Modules

Namespace Framework.Core
    Public Class CharacterBasicsHandler
        '// Declaration
        Private _tempResult As String
        '// Declaration

        Public Sub GetBasicCharacterInformation(ByVal characterGuid As Integer, ByVal setId As Integer,
                                                ByVal account As Account)
            LogAppend("Loading basic character information for characterGuid: " & characterGuid & " and setId: " & setId,
                      "CharacterBasicsHandler_GetBasicCharacterInformation", True)
            Select Case GlobalVariables.sourceCore
                Case "arcemu"
                    LoadAtArcemu(characterGuid, setId, account)
                Case "trinity"
                    LoadAtTrinity(characterGuid, setId, account)
                Case "trinitytbc"
                    LoadAtTrinityTbc(characterGuid, setId, account)
                Case "mangos"
                    LoadAtMangos(characterGuid, setId, account)
            End Select
        End Sub

        Private Sub LoadAtArcemu(ByVal charguid As Integer, ByVal tarSetId As Integer, ByVal account As Account)
            'Character Table
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_name_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            Dim tmpCharacter As Character = GetCharacterSetBySetId(tarSetId, account)
            tmpCharacter.Name = _tempResult
            tmpCharacter.Guid = charguid
            tmpCharacter.CreatedGuid = charguid
            tmpCharacter.SourceCore = "arcemu"
            tmpCharacter.SetIndex = tarSetId
            LogAppend(
                "Loaded character name info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtArcemu", True)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_race_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.Race = TryInt(_tempResult)
            LogAppend(
                "Loaded character race info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtArcemu", True)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_class_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.Cclass = TryInt(_tempResult)
            LogAppend(
                "Loaded character class info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtArcemu", True)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_gender_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.Gender = TryInt(_tempResult)
            LogAppend(
                "Loaded character gender info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtArcemu", True)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_level_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.Level = TryInt(_tempResult)
            LogAppend(
                "Loaded character level info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtArcemu", True)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_accountId_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.AccountId = TryInt(_tempResult)
            LogAppend(
                "Loaded character accountId info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtArcemu", True)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_xp_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.Xp = TryInt(_tempResult)
            LogAppend(
                "Loaded character xp info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtArcemu", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_gold_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.Gold = TryInt(_tempResult)
            LogAppend(
                "Loaded character gold info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtArcemu", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_playerBytes_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.PlayerBytes = TryInt(_tempResult)
            LogAppend(
                "Loaded character playerBytes info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtArcemu", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_playerBytes2_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.PlayerBytes2 = TryInt(_tempResult)
            LogAppend(
                "Loaded character playerBytes2 info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtArcemu", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_playerFlags_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.PlayerFlags = TryInt(_tempResult)
            LogAppend(
                "Loaded character playerFlags info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtArcemu", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_posX_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.PositionX = TryInt(_tempResult)
            LogAppend(
                "Loaded character posX info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtArcemu", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_posY_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.PositionY = TryInt(_tempResult)
            LogAppend(
                "Loaded character posY info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtArcemu", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_posZ_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.PositionZ = TryInt(_tempResult)
            LogAppend(
                "Loaded character posZ info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtArcemu", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_map_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.Map = TryInt(_tempResult)
            LogAppend(
                "Loaded character map info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtArcemu", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_instanceId_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.InstanceId = TryInt(_tempResult)
            LogAppend(
                "Loaded character instanceId info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtArcemu", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_orientation_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.Orientation = TryInt(_tempResult)
            LogAppend(
                "Loaded character orientation info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtArcemu", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_taximask_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.Taximask = _tempResult
            LogAppend(
                "Loaded character taximask info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtArcemu", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_cinematic_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.Cinematic = TryInt(_tempResult)
            LogAppend(
                "Loaded character cinematic info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtArcemu", False)
            Dim parts() As String =
                    (runSQLCommand_characters_string(
                        "SELECT " & GlobalVariables.sourceStructure.char_arcemuPlayedTime_col(0) & " FROM " &
                        GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                        GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")).Split(" "c)
            tmpCharacter.TotalTime = TryInt(parts(0))
            LogAppend(
                "Loaded character totaltime info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & parts(0), "CharacterBasicsHandler_LoadAtArcemu", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_stableSlots_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.StableSlots = TryInt(_tempResult)
            LogAppend(
                "Loaded character stableSlots info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtArcemu", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_zone_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.Zone = TryInt(_tempResult)
            LogAppend(
                "Loaded character zone info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtArcemu", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_arenaPoints_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.ArenaPoints = TryInt(_tempResult)
            LogAppend(
                "Loaded character arenaPoints info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtArcemu", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_totalHonorPoints_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.TotalHonorPoints = TryInt(_tempResult)
            LogAppend(
                "Loaded character totalHonorPoints info for characterGuid: " & charguid.ToString & " and setId: " &
                tarSetId & " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtArcemu", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_totalKills_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.TotalKills = TryInt(_tempResult)
            LogAppend(
                "Loaded character totalKills info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtArcemu", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_chosenTitle_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.ChosenTitle = TryInt(_tempResult)
            LogAppend(
                "Loaded character chosenTitle info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtArcemu", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_watchedFaction_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.WatchedFaction = TryInt(_tempResult)
            LogAppend(
                "Loaded character watchedFaction info for characterGuid: " & charguid.ToString & " and setId: " &
                tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtArcemu", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_health_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.Health = TryInt(_tempResult)
            LogAppend(
                "Loaded character healt info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtArcemu", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_speccount_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.SpecCount = TryInt(_tempResult)
            LogAppend(
                "Loaded character speccount info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtArcemu", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_activeSpec_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.ActiveSpec = TryInt(_tempResult)
            LogAppend(
                "Loaded character activeSpec info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtArcemu", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_exploredZones_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.ExploredZones = _tempResult
            LogAppend(
                "Loaded character exploredZones info for characterGuid: " & charguid.ToString & " and setId: " &
                tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtArcemu", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_knownTitles_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.KnownTitles = _tempResult
            LogAppend(
                "Loaded character knownTitles info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtArcemu", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_arcemuTalentPoints_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.ArcEmuTalentPoints = _tempResult
            LogAppend(
                "Loaded character arcemuTalentPoints info for characterGuid: " & charguid.ToString & " and setId: " &
                tarSetId & " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtArcemu", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_finishedQuests_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.FinishedQuests = _tempResult
            LogAppend(
                "Loaded character finishedQuests info for characterGuid: " & charguid.ToString & " and setId: " &
                tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtArcemu", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_customFaction_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.CustomFaction = TryInt(_tempResult)
            LogAppend(
                "Loaded character customFaction info for characterGuid: " & charguid.ToString & " and setId: " &
                tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtArcemu", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_bindmapid_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.BindMapId = TryInt(_tempResult)
            LogAppend(
                "Loaded character bindmapid info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtArcemu", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_bindzoneid_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.BindZoneId = TryInt(_tempResult)
            LogAppend(
                "Loaded character bindzoneid info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtArcemu", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_bindposX_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.BindPositionX = TryInt(_tempResult)
            LogAppend(
                "Loaded character bindposX info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtArcemu", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_bindposY_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.BindPositionY = TryInt(_tempResult)
            LogAppend(
                "Loaded character bindposY info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtArcemu", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_bindposZ_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.BindPositionZ = TryInt(_tempResult)
            LogAppend(
                "Loaded character bindposZ info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtArcemu", False)
            tmpCharacter.HomeBind = "<map>" & tmpCharacter.BindMapId.ToString() & "</map><zone>" &
                                    tmpCharacter.BindZoneId.ToString() & "</zone><position_x>" &
                                    tmpCharacter.BindPositionX.ToString() & "</position_x><position_y>" &
                                    tmpCharacter.BindPositionY.ToString() & "</position_y><position_z>" &
                                    tmpCharacter.BindPositionZ.ToString() & "</position_z>"

              SetCharacterSet(tarSetId, tmpCharacter, account)
        End Sub

        Private Sub LoadAtTrinity(ByVal charguid As Integer, ByVal tarSetId As Integer, ByVal account As Account)

            'Character Table
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_name_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            Dim tmpCharacter As Character = GetCharacterSetBySetId(tarSetId, account)
            tmpCharacter.Name = _tempResult
            tmpCharacter.Guid = charguid
            tmpCharacter.CreatedGuid = charguid
            tmpCharacter.SourceCore = "trinity"
            tmpCharacter.SetIndex = tarSetId
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_race_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.Race = TryInt(_tempResult)
            LogAppend(
                "Loaded character race info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinity", True)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_race_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.Cclass = TryInt(_tempResult)
            LogAppend(
                "Loaded character class info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinity", True)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_gender_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.Gender = TryInt(_tempResult)
            LogAppend(
                "Loaded character gender info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinity", True)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_level_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.Level = TryInt(_tempResult)
            LogAppend(
                "Loaded character level info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinity", True)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_accountId_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.AccountId = TryInt(_tempResult)
            LogAppend(
                "Loaded character accountId info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinity", True)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_xp_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.Xp = TryInt(_tempResult)
            LogAppend(
                "Loaded character xp info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinity", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_gold_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.Gold = TryInt(_tempResult)
            LogAppend(
                "Loaded character gold info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinity", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_playerBytes_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.PlayerBytes = TryInt(_tempResult)
            LogAppend(
                "Loaded character playerBytes info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinity", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_playerBytes2_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.PlayerBytes2 = TryInt(_tempResult)
            LogAppend(
                "Loaded character playerBytes2 info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinity", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_playerFlags_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.PlayerFlags = TryInt(_tempResult)
            LogAppend(
                "Loaded character playerFlags info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinity", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_posX_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.PositionX = TryInt(_tempResult)
            LogAppend(
                "Loaded character posX info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinity", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_posY_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.PositionY = TryInt(_tempResult)
            LogAppend(
                "Loaded character posY info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinity", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_posZ_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.PositionZ = TryInt(_tempResult)
            LogAppend(
                "Loaded character posZ info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinity", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_map_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.Map = TryInt(_tempResult)
            LogAppend(
                "Loaded character map info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinity", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_instanceId_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.InstanceId = TryInt(_tempResult)
            LogAppend(
                "Loaded character instanceId info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinity", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_instanceModeMask_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.InstanceModeMask = TryInt(_tempResult)
            LogAppend(
                "Loaded character instanceModeMask info for characterGuid: " & charguid.ToString & " and setId: " &
                tarSetId & " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinity", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_orientation_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.Orientation = TryInt(_tempResult)
            LogAppend(
                "Loaded character orientation info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinity", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_taximask_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.Taximask = _tempResult
            LogAppend(
                "Loaded character taximask info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinity", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_cinematic_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.Cinematic = TryInt(_tempResult)
            LogAppend(
                "Loaded character cinematic info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinity", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_totaltime_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.TotalTime = TryInt(_tempResult)
            LogAppend(
                "Loaded character totaltime info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinity", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_leveltime_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.LevelTime = TryInt(_tempResult)
            LogAppend(
                "Loaded character leveltime info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinity", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_extraFlags_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.ExtraFlags = TryInt(_tempResult)
            LogAppend(
                "Loaded character extraFlags info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinity", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_stableSlots_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.StableSlots = TryInt(_tempResult)
            LogAppend(
                "Loaded character stableSlots info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinity", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_atlogin_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.AtLogin = TryInt(_tempResult)
            LogAppend(
                "Loaded character atlogin info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinity", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_zone_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.Zone = TryInt(_tempResult)
            LogAppend(
                "Loaded character zone info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinity", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_arenaPoints_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.ArenaPoints = TryInt(_tempResult)
            LogAppend(
                "Loaded character arenaPoints info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinity", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_totalHonorPoints_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.TotalHonorPoints = TryInt(_tempResult)
            LogAppend(
                "Loaded character totalHonorPoints info for characterGuid: " & charguid.ToString & " and setId: " &
                tarSetId & " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinity", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_totalKills_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.TotalKills = TryInt(_tempResult)
            LogAppend(
                "Loaded character totalKills info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinity", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_chosenTitle_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.ChosenTitle = TryInt(_tempResult)
            LogAppend(
                "Loaded character chosenTitle info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinity", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_knownCurrencies_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.KnownCurrencies = TryInt(_tempResult)
            LogAppend(
                "Loaded character knownCurrencies info for characterGuid: " & charguid.ToString & " and setId: " &
                tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinity", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_watchedFaction_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.WatchedFaction = TryInt(_tempResult)
            LogAppend(
                "Loaded character watchedFaction info for characterGuid: " & charguid.ToString & " and setId: " &
                tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinity", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_health_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.Health = TryInt(_tempResult)
            LogAppend(
                "Loaded character healt info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinity", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_speccount_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.SpecCount = TryInt(_tempResult)
            LogAppend(
                "Loaded character speccount info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinity", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_activeSpec_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.ActiveSpec = TryInt(_tempResult)
            LogAppend(
                "Loaded character activeSpec info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinity", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_exploredZones_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.ExploredZones = _tempResult
            LogAppend(
                "Loaded character exploredZones info for characterGuid: " & charguid.ToString & " and setId: " &
                tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinity", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_knownTitles_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.KnownTitles = _tempResult
            LogAppend(
                "Loaded character knownTitles info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinity", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_actionBars_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.ActionBars = TryInt(_tempResult)
            LogAppend(
                "Loaded character actionBars info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinity", False)
            'temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_finishedQuests_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            'tmpCharacter.FinishedQuests = temp_result
            'LogAppend("Loaded character finishedQuests info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)
            'temp_result = runSQLCommand_characters_string("SELECT " & sourceStructure.char_customFaction_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            'tmpCharacter.CustomFaction = TryInt(temp_result)
            'LogAppend("Loaded character customFaction info for characterGuid: " & charguid.ToString & " and setId: " & tar_setId & " // result is: " & temp_result, "CharacterBasicsHandler_LoadAtTrinity", False)

            'Character_homebind Table
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.homebind_map_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_homebind_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.homebind_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.BindMapId = TryInt(_tempResult)
            LogAppend(
                "Loaded homebind bindmapid info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinity", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.homebind_zone_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_homebind_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.homebind_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.BindZoneId = TryInt(_tempResult)
            LogAppend(
                "Loaded homebind bindzoneid info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinity", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.homebind_posx_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_homebind_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.homebind_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.BindPositionX = TryInt(_tempResult)
            LogAppend(
                "Loaded homebind bindposX info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinity", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.homebind_posy_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_homebind_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.homebind_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.BindPositionY = TryInt(_tempResult)
            LogAppend(
                "Loaded homebind bindposY info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinity", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.homebind_posz_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_homebind_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.homebind_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.BindPositionZ = TryInt(_tempResult)
            LogAppend(
                "Loaded homebind bindposZ info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinity", False)
            tmpCharacter.HomeBind = "<map>" & tmpCharacter.BindMapId.ToString() & "</map><zone>" &
                                    tmpCharacter.BindZoneId.ToString() & "</zone><position_x>" &
                                    tmpCharacter.BindPositionX.ToString() & "</position_x><position_y>" &
                                    tmpCharacter.BindPositionY.ToString() & "</position_y><position_z>" &
                                    tmpCharacter.BindPositionZ.ToString() & "</position_z>"


            SetCharacterSet(tarSetId, tmpCharacter, account)
        End Sub

        Private Sub LoadAtTrinityTbc(ByVal charguid As Integer, ByVal tarSetId As Integer, ByVal account As Account)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_name_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            Dim tmpCharacter As Character = GetCharacterSetBySetId(tarSetId, account)
            tmpCharacter.Name = _tempResult
            tmpCharacter.Guid = charguid
            tmpCharacter.CreatedGuid = charguid
            tmpCharacter.SourceCore = "trinitytbc"
            tmpCharacter.SetIndex = tarSetId

            'Character Table
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_race_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.Race = TryInt(_tempResult)
            LogAppend(
                "Loaded character race info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinityTBC", True)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_race_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.Cclass = TryInt(_tempResult)
            LogAppend(
                "Loaded character class info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinityTBC", True)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 242), ' ', -1) AS UNSIGNED) AS `gender` FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "'")
            tmpCharacter.Gender = TryInt(_tempResult)
            LogAppend(
                "Loaded character gender info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinityTBC", True)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 35), ' ', -1) AS UNSIGNED) AS `level` FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.Level = TryInt(_tempResult)
            LogAppend(
                "Loaded character level info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinityTBC", True)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_accountId_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.AccountId = TryInt(_tempResult)
            LogAppend(
                "Loaded character accountId info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinityTBC", True)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 927), ' ', -1) AS UNSIGNED) AS `xp` FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.Xp = TryInt(_tempResult)
            LogAppend(
                "Loaded character xp info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 1398), ' ', -1) AS UNSIGNED) AS `money` FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.Gold = TryInt(_tempResult)
            LogAppend(
                "Loaded character gold info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 240), ' ', -1) AS UNSIGNED) AS `playerBytes` FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.PlayerBytes = TryInt(_tempResult)
            LogAppend(
                "Loaded character playerBytes info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 241), ' ', -1) AS UNSIGNED) AS `playerBytes2` FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.PlayerBytes2 = TryInt(_tempResult)
            LogAppend(
                "Loaded character playerBytes2 info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 237), ' ', -1) AS UNSIGNED) AS `playerFlags` FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.PlayerFlags = TryInt(_tempResult)
            LogAppend(
                "Loaded character playerFlags info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_posX_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.PositionX = TryInt(_tempResult)
            LogAppend(
                "Loaded character posX info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_posY_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.PositionY = TryInt(_tempResult)
            LogAppend(
                "Loaded character posY info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_posZ_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.PositionZ = TryInt(_tempResult)
            LogAppend(
                "Loaded character posZ info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_map_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.Map = TryInt(_tempResult)
            LogAppend(
                "Loaded character map info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_instanceId_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.InstanceId = TryInt(_tempResult)
            LogAppend(
                "Loaded character instanceId info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_instanceModeMask_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.InstanceModeMask = TryInt(_tempResult)
            LogAppend(
                "Loaded character instanceModeMask info for characterGuid: " & charguid.ToString & " and setId: " &
                tarSetId & " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_orientation_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.Orientation = TryInt(_tempResult)
            LogAppend(
                "Loaded character orientation info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_taximask_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.Taximask = _tempResult
            LogAppend(
                "Loaded character taximask info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_cinematic_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.Cinematic = TryInt(_tempResult)
            LogAppend(
                "Loaded character cinematic info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_totaltime_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.TotalTime = TryInt(_tempResult)
            LogAppend(
                "Loaded character totaltime info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_leveltime_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.LevelTime = TryInt(_tempResult)
            LogAppend(
                "Loaded character leveltime info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_extraFlags_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.ExtraFlags = TryInt(_tempResult)
            LogAppend(
                "Loaded character extraFlags info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_stableSlots_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.StableSlots = TryInt(_tempResult)
            LogAppend(
                "Loaded character stableSlots info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_atlogin_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.AtLogin = TryInt(_tempResult)
            LogAppend(
                "Loaded character atlogin info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_zone_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.Zone = TryInt(_tempResult)
            LogAppend(
                "Loaded character zone info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 1500), ' ', -1) AS UNSIGNED) AS `arenaPoints` FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.ArenaPoints = TryInt(_tempResult)
            LogAppend(
                "Loaded character arenaPoints info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 1499), ' ', -1) AS UNSIGNED) AS `totalHonorPoints` FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.TotalHonorPoints = TryInt(_tempResult)
            LogAppend(
                "Loaded character totalHonorPoints info for characterGuid: " & charguid.ToString & " and setId: " &
                tarSetId & " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 1451), ' ', -1) AS UNSIGNED) AS `totalHonorPoints` FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.TotalKills = TryInt(_tempResult)
            LogAppend(
                "Loaded character totalKills info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 649), ' ', -1) AS UNSIGNED) AS `chosenTitle` FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.ChosenTitle = TryInt(_tempResult)
            LogAppend(
                "Loaded character chosenTitle info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 1456), ' ', -1) AS UNSIGNED) AS `watchedFaction` FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.KnownCurrencies = TryInt(_tempResult)
            LogAppend(
                "Loaded character knownCurrencies info for characterGuid: " & charguid.ToString & " and setId: " &
                tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_watchedFaction_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.WatchedFaction = TryInt(_tempResult)
            LogAppend(
                "Loaded character watchedFaction info for characterGuid: " & charguid.ToString & " and setId: " &
                tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 23), ' ', -1) AS UNSIGNED) AS `health` FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.Health = TryInt(_tempResult)
            LogAppend(
                "Loaded character healt info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_speccount_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.SpecCount = TryInt(_tempResult)
            LogAppend(
                "Loaded character speccount info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 1333), ' ', -1) AS UNSIGNED) AS `exploredZones` FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.ActiveSpec = TryInt(_tempResult)
            LogAppend(
                "Loaded character activeSpec info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_exploredZones_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.ExploredZones = _tempResult
            LogAppend(
                "Loaded character exploredZones info for characterGuid: " & charguid.ToString & " and setId: " &
                tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 925), ' ', -1) AS UNSIGNED) AS `knownTitles` FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.KnownTitles = _tempResult
            LogAppend(
                "Loaded character knownTitles info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_actionBars_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.ActionBars = TryInt(_tempResult)
            LogAppend(
                "Loaded character actionBars info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_finishedQuests_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.FinishedQuests = _tempResult
            LogAppend(
                "Loaded character finishedQuests info for characterGuid: " & charguid.ToString & " and setId: " &
                tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_customFaction_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.CustomFaction = TryInt(_tempResult)
            LogAppend(
                "Loaded character customFaction info for characterGuid: " & charguid.ToString & " and setId: " &
                tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinityTBC", False)

            'Character_homebind Table
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.homebind_map_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_homebind_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.homebind_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.BindMapId = TryInt(_tempResult)
            LogAppend(
                "Loaded homebind bindmapid info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.homebind_zone_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_homebind_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.homebind_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.BindZoneId = TryInt(_tempResult)
            LogAppend(
                "Loaded homebind bindzoneid info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.homebind_posx_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_homebind_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.homebind_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.BindPositionX = TryInt(_tempResult)
            LogAppend(
                "Loaded homebind bindposX info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.homebind_posy_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_homebind_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.homebind_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.BindPositionY = TryInt(_tempResult)
            LogAppend(
                "Loaded homebind bindposY info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.homebind_posz_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_homebind_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.homebind_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.BindPositionZ = TryInt(_tempResult)
            LogAppend(
                "Loaded homebind bindposZ info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtTrinityTBC", False)
            tmpCharacter.HomeBind = "<map>" & tmpCharacter.BindMapId.ToString() & "</map><zone>" &
                                    tmpCharacter.BindZoneId.ToString() & "</zone><position_x>" &
                                    tmpCharacter.BindPositionX.ToString() & "</position_x><position_y>" &
                                    tmpCharacter.BindPositionY.ToString() & "</position_y><position_z>" &
                                    tmpCharacter.BindPositionZ.ToString() & "</position_z>"

               SetCharacterSet(tarSetId, tmpCharacter, account)
        End Sub

        Private Sub LoadAtMangos(ByVal charguid As Integer, ByVal tarSetId As Integer, ByVal account As Account)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_name_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            Dim tmpCharacter As Character = GetCharacterSetBySetId(tarSetId, account)
            tmpCharacter.Name = _tempResult
            tmpCharacter.Guid = charguid
            tmpCharacter.CreatedGuid = charguid
            tmpCharacter.SourceCore = "mangos"
            tmpCharacter.SetIndex = tarSetId

            'Character Table
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_race_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.Race = TryInt(_tempResult)
            LogAppend(
                "Loaded character race info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtMangos", True)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_race_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.Cclass = TryInt(_tempResult)
            LogAppend(
                "Loaded character class info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtMangos", True)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_gender_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.Gender = TryInt(_tempResult)
            LogAppend(
                "Loaded character gender info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtMangos", True)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_level_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.Level = TryInt(_tempResult)
            LogAppend(
                "Loaded character level info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtMangos", True)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_name_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.AccountId = TryInt(_tempResult)
            LogAppend(
                "Loaded character accountId info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtMangos", True)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_xp_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.Xp = TryInt(_tempResult)
            LogAppend(
                "Loaded character xp info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtMangos", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_gold_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.Gold = TryInt(_tempResult)
            LogAppend(
                "Loaded character gold info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtMangos", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_playerBytes_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.PlayerBytes = TryInt(_tempResult)
            LogAppend(
                "Loaded character playerBytes info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtMangos", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_playerBytes2_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.PlayerBytes2 = TryInt(_tempResult)
            LogAppend(
                "Loaded character playerBytes2 info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtMangos", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_playerFlags_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.PlayerFlags = TryInt(_tempResult)
            LogAppend(
                "Loaded character playerFlags info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtMangos", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_posX_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.PositionX = TryInt(_tempResult)
            LogAppend(
                "Loaded character posX info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtMangos", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_posY_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.PositionY = TryInt(_tempResult)
            LogAppend(
                "Loaded character posY info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtMangos", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_posZ_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.PositionZ = TryInt(_tempResult)
            LogAppend(
                "Loaded character posZ info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtMangos", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_map_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.Map = TryInt(_tempResult)
            LogAppend(
                "Loaded character map info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtMangos", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_instanceId_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.InstanceId = TryInt(_tempResult)
            LogAppend(
                "Loaded character instanceId info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtMangos", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_instanceModeMask_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.InstanceModeMask = TryInt(_tempResult)
            LogAppend(
                "Loaded character instanceModeMask info for characterGuid: " & charguid.ToString & " and setId: " &
                tarSetId & " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtMangos", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_orientation_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.Orientation = TryInt(_tempResult)
            LogAppend(
                "Loaded character orientation info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtMangos", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_taximask_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.Taximask = _tempResult
            LogAppend(
                "Loaded character taximask info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtMangos", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_cinematic_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.Cinematic = TryInt(_tempResult)
            LogAppend(
                "Loaded character cinematic info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtMangos", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_totaltime_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.TotalTime = TryInt(_tempResult)
            LogAppend(
                "Loaded character totaltime info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtMangos", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_leveltime_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.LevelTime = TryInt(_tempResult)
            LogAppend(
                "Loaded character leveltime info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtMangos", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_extraFlags_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.ExtraFlags = TryInt(_tempResult)
            LogAppend(
                "Loaded character extraFlags info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtMangos", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_stableSlots_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.StableSlots = TryInt(_tempResult)
            LogAppend(
                "Loaded character stableSlots info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtMangos", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_atlogin_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.AtLogin = TryInt(_tempResult)
            LogAppend(
                "Loaded character atlogin info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtMangos", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_zone_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.Zone = TryInt(_tempResult)
            LogAppend(
                "Loaded character zone info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtMangos", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_arenaPoints_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.ArenaPoints = TryInt(_tempResult)
            LogAppend(
                "Loaded character arenaPoints info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtMangos", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_totalHonorPoints_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.TotalHonorPoints = TryInt(_tempResult)
            LogAppend(
                "Loaded character totalHonorPoints info for characterGuid: " & charguid.ToString & " and setId: " &
                tarSetId & " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtMangos", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_totalKills_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.TotalKills = TryInt(_tempResult)
            LogAppend(
                "Loaded character totalKills info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtMangos", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_chosenTitle_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.ChosenTitle = TryInt(_tempResult)
            LogAppend(
                "Loaded character chosenTitle info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtMangos", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_knownCurrencies_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.KnownCurrencies = TryInt(_tempResult)
            LogAppend(
                "Loaded character knownCurrencies info for characterGuid: " & charguid.ToString & " and setId: " &
                tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtMangos", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_watchedFaction_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.WatchedFaction = TryInt(_tempResult)
            LogAppend(
                "Loaded character watchedFaction info for characterGuid: " & charguid.ToString & " and setId: " &
                tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtMangos", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_health_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.Health = TryInt(_tempResult)
            LogAppend(
                "Loaded character healt info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtMangos", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_speccount_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.SpecCount = TryInt(_tempResult)
            LogAppend(
                "Loaded character speccount info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtMangos", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_activeSpec_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.ActiveSpec = TryInt(_tempResult)
            LogAppend(
                "Loaded character activeSpec info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtMangos", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_exploredZones_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.ExploredZones = _tempResult
            LogAppend(
                "Loaded character exploredZones info for characterGuid: " & charguid.ToString & " and setId: " &
                tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtMangos", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_knownTitles_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.KnownTitles = _tempResult
            LogAppend(
                "Loaded character knownTitles info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtMangos", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_actionBars_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.ActionBars = TryInt(_tempResult)
            LogAppend(
                "Loaded character actionBars info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtMangos", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_finishedQuests_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.FinishedQuests = _tempResult
            LogAppend(
                "Loaded character finishedQuests info for characterGuid: " & charguid.ToString & " and setId: " &
                tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtMangos", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_customFaction_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.CustomFaction = TryInt(_tempResult)
            LogAppend(
                "Loaded character customFaction info for characterGuid: " & charguid.ToString & " and setId: " &
                tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtMangos", False)

            'Character_homebind Table
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.homebind_map_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_homebind_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.homebind_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.BindMapId = TryInt(_tempResult)
            LogAppend(
                "Loaded homebind bindmapid info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtMangos", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.homebind_zone_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_homebind_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.homebind_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.BindZoneId = TryInt(_tempResult)
            LogAppend(
                "Loaded homebind bindzoneid info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtMangos", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.homebind_posx_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_homebind_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.homebind_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.BindPositionX = TryInt(_tempResult)
            LogAppend(
                "Loaded homebind bindposX info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtMangos", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.homebind_posy_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_homebind_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.homebind_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.BindPositionY = TryInt(_tempResult)
            LogAppend(
                "Loaded homebind bindposY info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtMangos", False)
            _tempResult =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.homebind_posz_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_homebind_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.homebind_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.BindPositionZ = TryInt(_tempResult)
            LogAppend(
                "Loaded homebind bindposZ info for characterGuid: " & charguid.ToString & " and setId: " & tarSetId &
                " // result is: " & _tempResult, "CharacterBasicsHandler_LoadAtMangos", False)
            tmpCharacter.HomeBind = "<map>" & tmpCharacter.BindMapId.ToString() & "</map><zone>" &
                                    tmpCharacter.BindZoneId.ToString() & "</zone><position_x>" &
                                    tmpCharacter.BindPositionX.ToString() & "</position_x><position_y>" &
                                    tmpCharacter.BindPositionY.ToString() & "</position_y><position_z>" &
                                    tmpCharacter.BindPositionZ.ToString() & "</position_z>"

             SetCharacterSet(tarSetId, tmpCharacter, account)
        End Sub
    End Class
End Namespace