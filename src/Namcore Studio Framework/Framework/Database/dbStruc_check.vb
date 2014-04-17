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
'*      /Filename:      dbStruc_check
'*      /Description:   Contains functions for checking database compatibility
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Modules
Imports MySql.Data.MySqlClient

Namespace Framework.Database
    Public Class DbStrucCheck
        '// Declaration
        Dim _tmpConn As New MySqlConnection
        Dim _tmpConnRealm As New MySqlConnection
        Dim _tmpConnInfo As MySqlConnection
        Dim _characterDb As String
        Dim _authDb As String
        Dim _dbReport As String
        Dim _tablescheme As String
        Dim _xpac As Integer
        '// Declaration

        Public Sub StartCheck(ByVal coreString As String, ByVal expansion As Integer, ByVal sqlconn As MySqlConnection,
                              ByVal realmsqlconn As MySqlConnection, ByVal infosqlconn As MySqlConnection,
                              ByVal characterDbname As String, ByVal authDbname As String, ByVal target As Boolean)
            If target = True Then
                GlobalVariables.targetStructure = Nothing
                GlobalVariables.targetExpansion = expansion
            Else
                GlobalVariables.sourceStructure = Nothing
                GlobalVariables.sourceExpansion = expansion
            End If
            _tmpConn = sqlconn
            _tmpConnRealm = realmsqlconn
            _tmpConnInfo = infosqlconn
            _characterDb = characterDbname
            _authDb = authDBNAME
            _xpac = expansion
            _dbReport = ""
            _tablescheme = ""
            Dim dbstruc As New DBStructure
            Select Case coreString
                Case "trinity"
                    dbstruc.coreName = coreString
                    'auth
                    dbstruc.account_tbl = {"account"}
                    dbstruc.accountAccess_tbl = {"account_access"}
                    '_account
                    dbstruc.acc_id_col = {"id"}
                    dbstruc.acc_name_col = {"username"}
                    dbstruc.acc_sessionkey_col = {"sessionkey"}
                    dbstruc.acc_passHash_col = {"sha_pass_hash"}
                    dbstruc.acc_email_col = {"email"}
                    dbstruc.acc_joindate_col = {"joindate"}
                    dbstruc.acc_lastip_col = {"last_ip"}
                    dbstruc.acc_lastlogin_col = {"last_login"}
                    dbstruc.acc_locked_col = {"locked"}
                    dbstruc.acc_expansion_col = {"expansion"}
                    dbstruc.acc_locale_col = {"locale"}
                    '_account_access
                    dbstruc.accAcc_accid_col = {"id"}
                    dbstruc.accAcc_gmLevel_col = {"gmlevel"}
                    dbstruc.accAcc_realmId_col = {"RealmID"}

                    'characters
                    dbstruc.character_tbl = {"characters"}
                    dbstruc.character_achievement_tbl = {"character_achievement"}
                    dbstruc.character_action_tbl = {"character_action"}
                    dbstruc.character_glyphs_tbl = {"character_glyphs"}
                    dbstruc.character_homebind_tbl = {"character_homebind"}
                    dbstruc.character_inventory_tbl = {"character_inventory"}
                    dbstruc.character_queststatus_tbl = {"character_queststatus"}
                    dbstruc.character_queststatus_rewarded_tbl = {"character_queststatus_rewarded"}
                    dbstruc.character_reputation_tbl = {"character_reputation"}
                    dbstruc.character_skills_tbl = {"character_skills"}
                    dbstruc.character_spells_tbl = {"character_spell"}
                    dbstruc.character_talent_tbl = {"character_talent"}
                    dbstruc.item_instance_tbl = {"item_instance"}
                    '_characters
                    dbstruc.char_guid_col = {"guid"}
                    dbstruc.char_accountId_col = {"account"}
                    dbstruc.char_name_col = {"name"}
                    dbstruc.char_race_col = {"race"}
                    dbstruc.char_class_col = {"class"}
                    dbstruc.char_gender_col = {"gender"}
                    dbstruc.char_level_col = {"level"}
                    dbstruc.char_gold_col = {"money"}
                    dbstruc.char_xp_col = {"xp"}
                    dbstruc.char_playerBytes_col = {"playerBytes"}
                    dbstruc.char_playerBytes2_col = {"playerBytes2"}
                    dbstruc.char_playerFlags_col = {"playerFlags"}
                    dbstruc.char_posX_col = {"position_x"}
                    dbstruc.char_posY_col = {"position_y"}
                    dbstruc.char_posZ_col = {"position_z"}
                    dbstruc.char_map_col = {"map"}
                    dbstruc.char_instanceId_col = {"instance_id"}
                    dbstruc.char_instanceModeMask_col = {"instance_mode_mask"}
                    dbstruc.char_orientation_col = {"orientation"}
                    dbstruc.char_taximask_col = {"taximask"}
                    dbstruc.char_cinematic_col = {"cinematic"}
                    dbstruc.char_totaltime_col = {"totaltime"}
                    dbstruc.char_leveltime_col = {"leveltime"}
                    dbstruc.char_extraFlags_col = {"extra_flags"}
                    dbstruc.char_health_col = {"health"}
                    dbstruc.char_stableSlots_col = {"stable_slots"}
                    dbstruc.char_atlogin_col = {"at_login"}
                    dbstruc.char_zone_col = {"zone"}
                    dbstruc.char_arenaPoints_col = {"arenaPoints"}
                    dbstruc.char_totalHonorPoints_col = {"totalHonorPoints"}
                    dbstruc.char_totalKills_col = {"totalKills"}
                    dbstruc.char_chosenTitle_col = {"chosenTitle"}
                    dbstruc.char_knownCurrencies_col = {"knownCurrencies"}
                    dbstruc.char_watchedFaction_col = {"watchedFaction"}
                    dbstruc.char_speccount_col = {"speccount"}
                    dbstruc.char_activeSpec_col = {"activespec"}
                    dbstruc.char_exploredZones_col = {"exploredZones"}
                    dbstruc.char_equipmentCache_col = {"equipmentCache"}
                    dbstruc.char_knownTitles_col = {"knownTitles"}
                    dbstruc.char_actionBars_col = {"actionBars"}
                    '_character_achievement
                    dbstruc.av_guid_col = {"guid"}
                    dbstruc.av_achievement_col = {"achievement"}
                    dbstruc.av_date_col = {"date"}
                    '_character_action
                    dbstruc.action_guid_col = {"guid"}
                    dbstruc.action_spec_col = {"spec"}
                    dbstruc.action_button_col = {"button"}
                    dbstruc.action_action_col = {"action"}
                    dbstruc.action_type_col = {"type"}
                    '_character_glyphs
                    dbstruc.glyphs_guid_col = {"guid"}
                    dbstruc.glyphs_spec_col = {"spec"}
                    dbstruc.glyphs_glyph1_col = {"glyph1"}
                    dbstruc.glyphs_glyph2_col = {"glyph2"}
                    dbstruc.glyphs_glyph3_col = {"glyph3"}
                    dbstruc.glyphs_glyph4_col = {"glyph4"}
                    dbstruc.glyphs_glyph5_col = {"glyph5"}
                    dbstruc.glyphs_glyph6_col = {"glyph6"}
                    '_character_homebind
                    dbstruc.homebind_guid_col = {"guid"}
                    dbstruc.homebind_map_col = {"mapId"}
                    dbstruc.homebind_zone_col = {"zoneId"}
                    dbstruc.homebind_posx_col = {"position_x", "posX"}
                    dbstruc.homebind_posy_col = {"position_y", "posY"}
                    dbstruc.homebind_posz_col = {"position_z", "posZ"}
                    '_character_inventory
                    dbstruc.invent_guid_col = {"guid"}
                    dbstruc.invent_bag_col = {"bag"}
                    dbstruc.invent_slot_col = {"slot"}
                    dbstruc.invent_item_col = {"item"}
                    '_character_queststatus
                    dbstruc.qst_guid_col = {"guid"}
                    dbstruc.qst_quest_col = {"quest"}
                    dbstruc.qst_status_col = {"status"}
                    dbstruc.qst_explored_col = {"explored"}
                    dbstruc.qst_timer_col = {"timer"}
                    '_character_queststatus_rewarded
                    dbstruc.qstre_guid_col = {"guid"}
                    dbstruc.qstre_quest_col = {"quest"}
                    '_character_reputation
                    dbstruc.rep_guid_col = {"guid"}
                    dbstruc.rep_faction_col = {"faction"}
                    dbstruc.rep_standing_col = {"standing"}
                    dbstruc.rep_flags_col = {"flags"}
                    '_character_skills
                    dbstruc.skill_guid_col = {"guid"}
                    dbstruc.skill_skill_col = {"skill"}
                    dbstruc.skill_value_col = {"value"}
                    dbstruc.skill_max_col = {"max"}
                    '_character_spell
                    dbstruc.spell_guid_col = {"guid"}
                    dbstruc.spell_spell_col = {"spell"}
                    dbstruc.spell_active_col = {"active"}
                    dbstruc.spell_disabled_col = {"disabled"}
                    '_caracter_talent
                    dbstruc.talent_guid_col = {"guid"}
                    dbstruc.talent_spell_col = {"spell"}
                    dbstruc.talent_spec_col = {"spec"}
                    '_item_instance
                    dbstruc.itmins_guid_col = {"guid"}
                    dbstruc.itmins_itemEntry_col = {"itemEntry"}
                    dbstruc.itmins_ownerGuid_col = {"owner_guid"}
                    dbstruc.itmins_count_col = {"count"}
                    dbstruc.itmins_durability_col = {"durability"}
                    dbstruc.itmins_enchantments_col = {"enchantments"}

                    check_accounts(dbstruc)
                    check_accountAccess(dbstruc)
                    check_characters(dbstruc)
                    check_character_achievement(dbstruc)
                    check_character_action(dbstruc)
                    If _xpac >= 3 Then check_character_glyphs(dbstruc)
                    check_character_homebind(dbstruc)
                    check_character_inventory(dbstruc)
                    check_character_queststatus(dbstruc)
                    check_character_queststatus_rewarded(dbstruc)
                    check_character_reputation(dbstruc)
                    check_character_skills(dbstruc)
                    check_character_spell(dbstruc)
                    check_character_talent(dbstruc)
                    check_item_instance(dbstruc)
                Case "mangos"
                    dbstruc.CoreName = coreString
                    'auth
                    dbstruc.account_tbl = {"account"}
                    '_account
                    dbstruc.acc_id_col = {"id"}
                    dbstruc.acc_name_col = {"username"}
                    dbstruc.acc_sessionkey_col = {"sessionkey"}
                    dbstruc.acc_passHash_col = {"sha_pass_hash"}
                    dbstruc.acc_email_col = {"email"}
                    dbstruc.acc_joindate_col = {"joindate"}
                    dbstruc.acc_lastip_col = {"last_ip"}
                    dbstruc.acc_lastlogin_col = {"last_login"}
                    dbstruc.acc_locked_col = {"locked"}
                    dbstruc.acc_expansion_col = {"expansion"}
                    dbstruc.acc_locale_col = {"locale"}
                    dbstruc.acc_gmlevel_col = {"gmlevel"}

                    'characters
                    dbstruc.character_tbl = {"characters"}
                    dbstruc.character_achievement_tbl = {"character_achievement"}
                    dbstruc.character_action_tbl = {"character_action"}
                    dbstruc.character_glyphs_tbl = {"character_glyphs"}
                    dbstruc.character_homebind_tbl = {"character_homebind"}
                    dbstruc.character_inventory_tbl = {"character_inventory"}
                    dbstruc.character_queststatus_tbl = {"character_queststatus"}
                    dbstruc.character_reputation_tbl = {"character_reputation"}
                    dbstruc.character_skills_tbl = {"character_skills"}
                    dbstruc.character_spells_tbl = {"character_spell"}
                    dbstruc.character_talent_tbl = {"character_talent"}
                    dbstruc.item_instance_tbl = {"item_instance"}
                    '_characters
                    dbstruc.char_guid_col = {"guid"}
                    dbstruc.char_accountId_col = {"account"}
                    dbstruc.char_name_col = {"name"}
                    dbstruc.char_race_col = {"race"}
                    dbstruc.char_class_col = {"class"}
                    dbstruc.char_gender_col = {"gender"}
                    dbstruc.char_level_col = {"level"}
                    dbstruc.char_gold_col = {"money"}
                    dbstruc.char_xp_col = {"xp"}
                    dbstruc.char_playerBytes_col = {"playerBytes"}
                    dbstruc.char_playerBytes2_col = {"playerBytes2"}
                    dbstruc.char_playerFlags_col = {"playerFlags"}
                    dbstruc.char_posX_col = {"position_x"}
                    dbstruc.char_posY_col = {"position_y"}
                    dbstruc.char_posZ_col = {"position_z"}
                    dbstruc.char_map_col = {"map"}
                    dbstruc.char_orientation_col = {"orientation"}
                    dbstruc.char_taximask_col = {"taximask"}
                    dbstruc.char_cinematic_col = {"cinematic"}
                    dbstruc.char_totaltime_col = {"totaltime"}
                    dbstruc.char_leveltime_col = {"leveltime"}
                    dbstruc.char_extraFlags_col = {"extra_flags"}
                    dbstruc.char_health_col = {"health"}
                    dbstruc.char_stableSlots_col = {"stable_slots"}
                    dbstruc.char_atlogin_col = {"at_login"}
                    dbstruc.char_zone_col = {"zone"}
                    dbstruc.char_arenaPoints_col = {"arenaPoints"}
                    dbstruc.char_totalHonorPoints_col = {"totalHonorPoints"}
                    dbstruc.char_totalKills_col = {"totalKills"}
                    dbstruc.char_chosenTitle_col = {"chosenTitle"}
                    dbstruc.char_knownCurrencies_col = {"knownCurrencies"}
                    dbstruc.char_watchedFaction_col = {"watchedFaction"}
                    dbstruc.char_speccount_col = {"specCount"}
                    dbstruc.char_activeSpec_col = {"activespec"}
                    dbstruc.char_exploredZones_col = {"exploredZones"}
                    dbstruc.char_equipmentCache_col = {"equipmentCache"}
                    dbstruc.char_knownTitles_col = {"knownTitles"}
                    dbstruc.char_actionBars_col = {"actionBars"}
                    '_character_achievement
                    dbstruc.av_guid_col = {"guid"}
                    dbstruc.av_achievement_col = {"achievement"}
                    dbstruc.av_date_col = {"date"}
                    '_character_action
                    dbstruc.action_guid_col = {"guid"}
                    dbstruc.action_spec_col = {"spec"}
                    dbstruc.action_button_col = {"button"}
                    dbstruc.action_action_col = {"action"}
                    dbstruc.action_type_col = {"type"}
                    '_character_glyphs
                    dbstruc.glyphs_guid_col = {"guid"}
                    dbstruc.glyphs_spec_col = {"spec"}
                    dbstruc.glyphs_slot_col = {"slot"}
                    dbstruc.glyphs_glyph_col = {"glyph"}
                    '_character_homebind
                    dbstruc.homebind_guid_col = {"guid"}
                    dbstruc.homebind_map_col = {"map", "mapId"}
                    dbstruc.homebind_zone_col = {"zone", "zoneId"}
                    dbstruc.homebind_posx_col = {"position_x", "posX", "positionX"}
                    dbstruc.homebind_posy_col = {"position_y", "posY", "positionY"}
                    dbstruc.homebind_posz_col = {"position_z", "posZ", "positionZ"}
                    '_character_inventory
                    dbstruc.invent_guid_col = {"guid"}
                    dbstruc.invent_bag_col = {"bag"}
                    dbstruc.invent_slot_col = {"slot"}
                    dbstruc.invent_item_col = {"item"}
                    dbstruc.invent_item_template_col = {"item_template"}
                    '_character_queststatus
                    dbstruc.qst_guid_col = {"guid"}
                    dbstruc.qst_quest_col = {"quest"}
                    dbstruc.qst_status_col = {"status"}
                    dbstruc.qst_explored_col = {"explored"}
                    dbstruc.qst_timer_col = {"timer"}
                    dbstruc.qst_rewarded_col = {"rewarded"}
                    '_character_reputation
                    dbstruc.rep_guid_col = {"guid"}
                    dbstruc.rep_faction_col = {"faction"}
                    dbstruc.rep_standing_col = {"standing"}
                    dbstruc.rep_flags_col = {"flags"}
                    '_character_skills
                    dbstruc.skill_guid_col = {"guid"}
                    dbstruc.skill_skill_col = {"skill"}
                    dbstruc.skill_value_col = {"value"}
                    dbstruc.skill_max_col = {"max"}
                    '_character_spell
                    dbstruc.spell_guid_col = {"guid"}
                    dbstruc.spell_spell_col = {"spell"}
                    dbstruc.spell_active_col = {"active"}
                    dbstruc.spell_disabled_col = {"disabled"}
                    '_caracter_talent
                    dbstruc.talent_guid_col = {"guid"}
                    dbstruc.talent_talent_col = {"talent_id"}
                    dbstruc.talent_rank_col = {"current_rank"}
                    dbstruc.talent_spec_col = {"spec"}
                    '_item_instance
                    dbstruc.itmins_guid_col = {"guid"}
                    dbstruc.itmins_ownerGuid_col = {"owner_guid"}
                    dbstruc.itmins_data_col = {"data"}

                    check_accounts(dbstruc)
                    check_accountAccess(dbstruc)
                    check_characters(dbstruc)
                    check_character_achievement(dbstruc)
                    check_character_action(dbstruc)
                    If _xpac >= 3 Then check_character_glyphs(dbstruc)
                    check_character_homebind(dbstruc)
                    check_character_inventory(dbstruc)
                    check_character_queststatus(dbstruc)
                    check_character_queststatus_rewarded(dbstruc)
                    check_character_reputation(dbstruc)
                    check_character_skills(dbstruc)
                    check_character_spell(dbstruc)
                    check_character_talent(dbstruc)
                    check_item_instance(dbstruc)
            End Select
            If target = True Then
                GlobalVariables.targetStructure = dbstruc
            Else
                GlobalVariables.sourceStructure = dbstruc
            End If
            sqlconn.Open()
        End Sub

        Private Sub check_accounts(ByVal struc As DBStructure)
            Dim tmpReport As String = _dbReport
            _dbReport = ""
            tbl_check_realm(struc.account_tbl)
            col_check_realm(struc.acc_id_col, struc.account_tbl)
            col_check_realm(struc.acc_name_col, struc.account_tbl)
            col_check_realm(struc.acc_passHash_col, struc.account_tbl)
            col_check_realm(struc.acc_email_col, struc.account_tbl)
            col_check_realm(struc.acc_joindate_col, struc.account_tbl)
            col_check_realm(struc.acc_lastip_col, struc.account_tbl)
            col_check_realm(struc.acc_locked_col, struc.account_tbl)
            col_check_realm(struc.acc_lastlogin_col, struc.account_tbl)
            col_check_realm(struc.acc_expansion_col, struc.account_tbl)
            col_check_realm(struc.acc_locale_col, struc.account_tbl)
            If Not _dbReport = "" Then gettablescheme(struc.account_tbl(0), _authDb)
            _dbReport = tmpReport & vbNewLine & _dbReport
        End Sub

        Private Sub check_accountAccess(ByVal struc As DBStructure)
            Dim tmpReport As String = _dbReport
            _dbReport = ""
            tbl_check_realm(struc.accountAccess_tbl)
            col_check_realm(struc.accAcc_accid_col, struc.accountAccess_tbl)
            col_check_realm(struc.accAcc_gmLevel_col, struc.accountAccess_tbl)
            col_check_realm(struc.accAcc_realmId_col, struc.accountAccess_tbl)
            If Not _dbReport = "" Then gettablescheme(struc.accountAccess_tbl(0), _authDb)
            _dbReport = tmpReport & vbNewLine & _dbReport
        End Sub

        Private Sub check_characters(ByVal struc As DBStructure)
            Dim tmpReport As String = _dbReport
            _dbReport = ""
            tbl_check(struc.character_tbl)
            col_check(struc.char_race_col, struc.character_tbl)
            col_check(struc.char_class_col, struc.character_tbl)
            col_check(struc.char_gender_col, struc.character_tbl)
            col_check(struc.char_guid_col, struc.character_tbl)
            col_check(struc.char_level_col, struc.character_tbl)
            col_check(struc.char_name_col, struc.character_tbl)
            col_check(struc.char_accountId_col, struc.character_tbl)
            col_check(struc.char_xp_col, struc.character_tbl)
            col_check(struc.char_gold_col, struc.character_tbl)
            col_check(struc.char_playerBytes_col, struc.character_tbl)
            col_check(struc.char_playerBytes2_col, struc.character_tbl)
            col_check(struc.char_playerFlags_col, struc.character_tbl)
            col_check(struc.char_posX_col, struc.character_tbl)
            col_check(struc.char_posY_col, struc.character_tbl)
            col_check(struc.char_posZ_col, struc.character_tbl)
            col_check(struc.char_map_col, struc.character_tbl)
            col_check(struc.char_instanceId_col, struc.character_tbl)
            col_check(struc.char_orientation_col, struc.character_tbl)
            col_check(struc.char_taximask_col, struc.character_tbl)
            col_check(struc.char_cinematic_col, struc.character_tbl)
            col_check(struc.char_totaltime_col, struc.character_tbl)
            col_check(struc.char_leveltime_col, struc.character_tbl)
            col_check(struc.char_stableSlots_col, struc.character_tbl)
            col_check(struc.char_zone_col, struc.character_tbl)
            col_check(struc.char_arenaPoints_col, struc.character_tbl)
            col_check(struc.char_totalHonorPoints_col, struc.character_tbl)
            col_check(struc.char_totalKills_col, struc.character_tbl)
            col_check(struc.char_chosenTitle_col, struc.character_tbl)
            col_check(struc.char_watchedFaction_col, struc.character_tbl)
            col_check(struc.char_extraFlags_col, struc.character_tbl)
            col_check(struc.char_health_col, struc.character_tbl)
            col_check(struc.char_speccount_col, struc.character_tbl)
            col_check(struc.char_activeSpec_col, struc.character_tbl)
            col_check(struc.char_exploredZones_col, struc.character_tbl)
            col_check(struc.char_knownTitles_col, struc.character_tbl)
            col_check(struc.char_atlogin_col, struc.character_tbl)
            col_check(struc.char_knownCurrencies_col, struc.character_tbl)
            col_check(struc.char_equipmentCache_col, struc.character_tbl)
            col_check(struc.char_actionBars_col, struc.character_tbl)
            If Not _dbReport = "" Then gettablescheme(struc.character_tbl(0), _characterDb)
            _dbReport = tmpReport & vbNewLine & _dbReport
        End Sub

        Private Sub check_character_achievement(ByVal struc As DBStructure)
            Dim tmpReport As String = _dbReport
            _dbReport = ""
            tbl_check(struc.character_achievement_tbl)
            col_check(struc.av_guid_col, struc.character_achievement_tbl)
            col_check(struc.av_achievement_col, struc.character_achievement_tbl)
            col_check(struc.av_date_col, struc.character_achievement_tbl)
            If Not _dbReport = "" Then gettablescheme(struc.character_achievement_tbl(0), _characterDb)
            _dbReport = tmpReport & vbNewLine & _dbReport
        End Sub

        Private Sub check_character_action(ByVal struc As DBStructure)
            Dim tmpReport As String = _dbReport
            _dbReport = ""
            tbl_check(struc.character_action_tbl)
            col_check(struc.action_guid_col, struc.character_action_tbl)
            col_check(struc.action_spec_col, struc.character_action_tbl)
            col_check(struc.action_button_col, struc.character_action_tbl)
            col_check(struc.action_action_col, struc.character_action_tbl)
            col_check(struc.action_type_col, struc.character_action_tbl)
            If Not _dbReport = "" Then gettablescheme(struc.character_action_tbl(0), _characterDb)
            _dbReport = tmpReport & vbNewLine & _dbReport
        End Sub

        Private Sub check_character_glyphs(ByVal struc As DBStructure)
            Dim tmpReport As String = _dbReport
            _dbReport = ""
            tbl_check(struc.character_glyphs_tbl)
            col_check(struc.glyphs_guid_col, struc.character_glyphs_tbl)
            col_check(struc.glyphs_spec_col, struc.character_glyphs_tbl)
            col_check(struc.glyphs_glyph1_col, struc.character_glyphs_tbl)
            col_check(struc.glyphs_glyph2_col, struc.character_glyphs_tbl)
            col_check(struc.glyphs_glyph3_col, struc.character_glyphs_tbl)
            col_check(struc.glyphs_glyph4_col, struc.character_glyphs_tbl)
            col_check(struc.glyphs_glyph5_col, struc.character_glyphs_tbl)
            col_check(struc.glyphs_glyph6_col, struc.character_glyphs_tbl)
            If _xpac = 4 Then
                col_check(struc.glyphs_glyph7_col, struc.character_glyphs_tbl)
                col_check(struc.glyphs_glyph8_col, struc.character_glyphs_tbl)
                col_check(struc.glyphs_glyph9_col, struc.character_glyphs_tbl)
            End If
            If Not _dbReport = "" Then gettablescheme(struc.character_glyphs_tbl(0), _characterDb)
            _dbReport = tmpReport & vbNewLine & _dbReport
        End Sub

        Private Sub check_character_homebind(ByVal struc As DBStructure)
            Dim tmpReport As String = _dbReport
            _dbReport = ""
            tbl_check(struc.character_homebind_tbl)
            col_check(struc.homebind_guid_col, struc.character_homebind_tbl)
            col_check(struc.homebind_map_col, struc.character_homebind_tbl)
            col_check(struc.homebind_zone_col, struc.character_homebind_tbl)
            col_check(struc.homebind_posx_col, struc.character_homebind_tbl)
            col_check(struc.homebind_posy_col, struc.character_homebind_tbl)
            col_check(struc.homebind_posz_col, struc.character_homebind_tbl)
            If Not _dbReport = "" Then gettablescheme(struc.character_homebind_tbl(0), _characterDb)
            _dbReport = tmpReport & vbNewLine & _dbReport
        End Sub

        Private Sub check_character_inventory(ByVal struc As DBStructure)
            Dim tmpReport As String = _dbReport
            _dbReport = ""
            tbl_check(struc.character_inventory_tbl)
            col_check(struc.invent_guid_col, struc.character_inventory_tbl)
            col_check(struc.invent_bag_col, struc.character_inventory_tbl)
            col_check(struc.invent_slot_col, struc.character_inventory_tbl)
            col_check(struc.invent_item_col, struc.character_inventory_tbl)
            If Not _dbReport = "" Then gettablescheme(struc.character_inventory_tbl(0), _characterDb)
            _dbReport = tmpReport & vbNewLine & _dbReport
        End Sub

        Private Sub check_character_queststatus(ByVal struc As DBStructure)
            Dim tmpReport As String = _dbReport
            _dbReport = ""
            tbl_check(struc.character_queststatus_tbl)
            col_check(struc.qst_guid_col, struc.character_queststatus_tbl)
            col_check(struc.qst_quest_col, struc.character_queststatus_tbl)
            col_check(struc.qst_status_col, struc.character_queststatus_tbl)
            col_check(struc.qst_explored_col, struc.character_queststatus_tbl)
            col_check(struc.qst_timer_col, struc.character_queststatus_tbl)
            If Not _dbReport = "" Then gettablescheme(struc.character_queststatus_tbl(0), _characterDb)
            _dbReport = tmpReport & vbNewLine & _dbReport
        End Sub

        Private Sub check_character_queststatus_rewarded(ByVal struc As DBStructure)
            Dim tmpReport As String = _dbReport
            _dbReport = ""
            tbl_check(struc.character_queststatus_rewarded_tbl)
            col_check(struc.qstre_guid_col, struc.character_queststatus_rewarded_tbl)
            col_check(struc.qstre_quest_col, struc.character_queststatus_rewarded_tbl)
            If Not _dbReport = "" Then gettablescheme(struc.character_queststatus_rewarded_tbl(0), _characterDb)
            _dbReport = tmpReport & vbNewLine & _dbReport
        End Sub

        Private Sub check_character_reputation(ByVal struc As DBStructure)
            Dim tmpReport As String = _dbReport
            _dbReport = ""
            tbl_check(struc.character_reputation_tbl)
            col_check(struc.rep_guid_col, struc.character_reputation_tbl)
            col_check(struc.rep_faction_col, struc.character_reputation_tbl)
            col_check(struc.rep_standing_col, struc.character_reputation_tbl)
            col_check(struc.rep_flags_col, struc.character_reputation_tbl)
            If Not _dbReport = "" Then gettablescheme(struc.character_reputation_tbl(0), _characterDb)
            _dbReport = tmpReport & vbNewLine & _dbReport
        End Sub

        Private Sub check_character_skills(ByVal struc As DBStructure)
            Dim tmpReport As String = _dbReport
            _dbReport = ""
            tbl_check(struc.character_skills_tbl)
            col_check(struc.skill_guid_col, struc.character_skills_tbl)
            col_check(struc.skill_skill_col, struc.character_skills_tbl)
            col_check(struc.skill_value_col, struc.character_skills_tbl)
            col_check(struc.skill_max_col, struc.character_skills_tbl)
            If Not _dbReport = "" Then gettablescheme(struc.character_skills_tbl(0), _characterDb)
            _dbReport = tmpReport & vbNewLine & _dbReport
        End Sub

        Private Sub check_character_spell(ByVal struc As DBStructure)
            Dim tmpReport As String = _dbReport
            _dbReport = ""
            tbl_check(struc.character_spells_tbl)
            col_check(struc.spell_guid_col, struc.character_spells_tbl)
            col_check(struc.spell_spell_col, struc.character_spells_tbl)
            col_check(struc.spell_active_col, struc.character_spells_tbl)
            col_check(struc.spell_disabled_col, struc.character_spells_tbl)
            If Not _dbReport = "" Then gettablescheme(struc.character_spells_tbl(0), _characterDb)
            _dbReport = tmpReport & vbNewLine & _dbReport
        End Sub

        Private Sub check_character_talent(ByVal struc As DBStructure)
            Dim tmpReport As String = _dbReport
            _dbReport = ""
            tbl_check(struc.character_talent_tbl)
            col_check(struc.talent_guid_col, struc.character_talent_tbl)
            col_check(struc.talent_spell_col, struc.character_talent_tbl)
            col_check(struc.talent_spec_col, struc.character_talent_tbl)
            If Not _dbReport = "" Then gettablescheme(struc.character_talent_tbl(0), _characterDb)
            _dbReport = tmpReport & vbNewLine & _dbReport
        End Sub

        Private Sub check_item_instance(ByVal struc As DBStructure)
            Dim tmpReport As String = _dbReport
            _dbReport = ""
            tbl_check(struc.item_instance_tbl)
            col_check(struc.itmins_guid_col, struc.item_instance_tbl)
            col_check(struc.itmins_itemEntry_col, struc.item_instance_tbl)
            col_check(struc.itmins_ownerGuid_col, struc.item_instance_tbl)
            col_check(struc.itmins_count_col, struc.item_instance_tbl)
            col_check(struc.itmins_enchantments_col, struc.item_instance_tbl)
            col_check(struc.itmins_durability_col, struc.item_instance_tbl)
            If Not _dbReport = "" Then gettablescheme(struc.item_instance_tbl(0), _characterDb)
            _dbReport = tmpReport & vbNewLine & _dbReport
        End Sub

        Private Sub Gettablescheme(ByVal table As String, ByVal db As String)
            _tablescheme = _tablescheme & "######## " & table & " ########" & vbNewLine
            Dim _
                da As _
                    New MySqlDataAdapter(
                        "SELECT COLUMN_NAME FROM COLUMNS WHERE TABLE_NAME='" & table & "' AND TABLE_SCHEMA='" & db & "'",
                        _tmpConnInfo)
            Dim dt As New DataTable
            Try
                _tmpConnInfo.Open()
            Catch ex As Exception
                _tmpConnInfo.Close()
                _tmpConnInfo.Dispose()
            End Try
            Try
                da.Fill(dt)
                Try
                    _tmpConnInfo.Close()
                    _tmpConnInfo.Dispose()
                Catch
                End Try
                Dim lastcount As Integer = CInt(Val(dt.Rows.Count.ToString))
                Dim count As Integer = 0
                If Not lastcount = 0 Then
                    Do
                        Dim readedcode As String = (dt.Rows(count).Item(0)).ToString
                        Dim column As String = readedcode
                        _tablescheme = _tablescheme & column & vbNewLine
                        count += 1
                    Loop Until count = lastcount
                End If
            Catch ex As Exception
            End Try
        End Sub

        Private Sub tbl_check(ByVal tablename() As String)
            Dim i As Integer = tablename.Length
            Dim counter As Integer = 0
            Do
                If Not _tmpConn.State = ConnectionState.Open Then
                    _tmpConn.Open()
                End If
                Dim myAdapter As New MySqlDataAdapter
                Dim sqlquery = ("SELECT * FROM " & tablename(counter) & " LIMIT 1")
                Dim myCommand As New MySqlCommand()
                myCommand.Connection = _tmpConn
                myCommand.CommandText = sqlquery
                myAdapter.SelectCommand = myCommand
                Dim myData As MySqlDataReader
                Try
                    myData = myCommand.ExecuteReader()
                    If CInt(myData.HasRows) = 0 Then
                        tablename(0) = tablename(counter)
                        LogAppend("Using table " & tablename(counter) & "!", "dbStruc_check_tbl_check")
                        Exit Do
                    Else
                        tablename(0) = tablename(counter)
                        LogAppend("Using table " & tablename(counter) & "!", "dbStruc_check_tbl_check")
                        Exit Do
                    End If
                Catch
                    Select Case i
                        Case 1, counter + 1
                            LogAppend("Table " & tablename(counter) & " not found! -> No alternatives!",
                                      "dbStruc_check_tbl_check", True, True)
                            _dbReport = _dbReport & "// Table " & tablename(counter) & " does not exist!"
                    End Select
                End Try
                _tmpConn.Close()
                _tmpConn.Dispose()
                counter += 1
            Loop Until counter = i
            _tmpConn.Close()
            _tmpConn.Dispose()
        End Sub

        Private Sub col_check(ByRef columnname() As String, ByVal tablename() As String)
            Dim i As Integer = columnname.Length
            Dim counter As Integer = 0
            Do
                If Not _tmpConn.State = ConnectionState.Open Then
                    _tmpConn.Open()
                End If
                Dim myAdapter As New MySqlDataAdapter
                Dim sqlquery = ("SELECT " & columnname(counter) & " FROM " & tablename(0))
                Dim myCommand As New MySqlCommand()
                myCommand.Connection = _tmpConn
                myCommand.CommandText = sqlquery
                myAdapter.SelectCommand = myCommand
                Dim myData As MySqlDataReader
                Try
                    myData = myCommand.ExecuteReader()
                    If CInt(myData.HasRows) = 0 Then
                        columnname(0) = columnname(counter)
                        LogAppend("Using column " & columnname(counter) & " in table " & tablename(0) & "!",
                                  "dbStruc_check_col_check")
                        Exit Do
                    Else
                        columnname(0) = columnname(counter)
                        LogAppend("Using column " & columnname(counter) & " in table " & tablename(0) & "!",
                                  "dbStruc_check_col_check")
                        Exit Do
                    End If
                Catch
                    Select Case i
                        Case 1, counter + 1
                            LogAppend(
                                "Column " & columnname(counter) & " not found in " & tablename(0) &
                                "! -> No alternatives!",
                                "dbStruc_check_col_check", True, True)
                            _dbReport = _dbReport & "// Column " & columnname(counter) & " does not exist in " &
                                        tablename(0) &
                                        "!"
                    End Select
                End Try
                _tmpConn.Close()
                _tmpConn.Dispose()
                counter += 1
            Loop Until counter = i
            _tmpConn.Close()
            _tmpConn.Dispose()
        End Sub

        Private Sub tbl_check_realm(ByVal tablename() As String)
            Dim i As Integer = tablename.Length
            Dim counter As Integer = 0
            Do
                If Not _tmpConnRealm.State = ConnectionState.Open Then
                    _tmpConnRealm.Open()
                End If
                Dim myAdapter As New MySqlDataAdapter
                Dim sqlquery = ("SELECT * FROM " & tablename(counter) & " LIMIT 1")
                Dim myCommand As New MySqlCommand()
                myCommand.Connection = _tmpConnRealm
                myCommand.CommandText = sqlquery
                myAdapter.SelectCommand = myCommand
                Dim myData As MySqlDataReader
                Try
                    myData = myCommand.ExecuteReader()
                    If CInt(myData.HasRows) = 0 Then
                        tablename(0) = tablename(counter)
                        LogAppend("Using table " & tablename(counter) & "!", "dbStruc_check_tbl_check")
                        Exit Do
                    Else
                        tablename(0) = tablename(counter)
                        LogAppend("Using table " & tablename(counter) & "!", "dbStruc_check_tbl_check")
                        Exit Do
                    End If
                Catch
                    Select Case i
                        Case 1, counter + 1
                            LogAppend("Table " & tablename(counter) & " not found! -> No alternatives!",
                                      "dbStruc_check_tbl_check", True, True)
                            _dbReport = _dbReport & "// Table " & tablename(counter) & " does not exist!"
                    End Select
                End Try
                _tmpConnRealm.Close()
                _tmpConnRealm.Dispose()
                counter += 1
            Loop Until counter = i
            _tmpConnRealm.Close()
            _tmpConnRealm.Dispose()
        End Sub

        Private Sub col_check_realm(ByRef columnname() As String, ByVal tablename() As String)
            Dim i As Integer = columnname.Length
            Dim counter As Integer = 0
            Do
                If Not _tmpConnRealm.State = ConnectionState.Open Then
                    _tmpConnRealm.Open()
                End If
                Dim myAdapter As New MySqlDataAdapter
                Dim sqlquery = ("SELECT " & columnname(counter) & " FROM " & tablename(0))
                Dim myCommand As New MySqlCommand()
                myCommand.Connection = _tmpConnRealm
                myCommand.CommandText = sqlquery
                myAdapter.SelectCommand = myCommand
                Dim myData As MySqlDataReader
                Try
                    myData = myCommand.ExecuteReader()
                    If CInt(myData.HasRows) = 0 Then
                        columnname(0) = columnname(counter)
                        LogAppend("Using column " & columnname(counter) & " in table " & tablename(0) & "!",
                                  "dbStruc_check_col_check")
                        Exit Do
                    Else
                        columnname(0) = columnname(counter)
                        LogAppend("Using column " & columnname(counter) & " in table " & tablename(0) & "!",
                                  "dbStruc_check_col_check")
                        Exit Do
                    End If
                Catch
                    Select Case i
                        Case 1, counter + 1
                            LogAppend(
                                "Column " & columnname(counter) & " not found in " & tablename(0) &
                                "! -> No alternatives!",
                                "dbStruc_check_col_check", True, True)
                            _dbReport = _dbReport & "// Column " & columnname(counter) & " does not exist in " &
                                        tablename(0) &
                                        "!"
                    End Select
                End Try
                _tmpConnRealm.Close()
                _tmpConnRealm.Dispose()
                counter += 1
            Loop Until counter = i
            _tmpConnRealm.Close()
            _tmpConnRealm.Dispose()
        End Sub
    End Class
End Namespace