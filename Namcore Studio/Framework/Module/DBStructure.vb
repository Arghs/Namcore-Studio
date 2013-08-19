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
'*      /Filename:      DBStructure
'*      /Description:   DBStructure object - Database structure information 
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Public Class DBStructure
    Public coreName As String
    'DB table names
    '# Character DB
    Public character_tbl() As String
    Public character_homebind_tbl() As String
    Public character_achievement_tbl() As String
    Public character_action_tbl() As String
    Public character_glyphs_tbl() As String
    Public character_inventory_tbl() As String
    Public character_queststatus_tbl() As String
    Public character_queststatus_rewarded_tbl() As String
    Public character_reputation_tbl() As String
    Public character_skills_tbl() As String
    Public character_spells_tbl() As String
    Public character_talent_tbl() As String
    Public item_instance_tbl() As String

    '# Realm DB
    Public account_tbl() As String
    Public accountAccess_tbl() As String

    'DB column names
    '# Character DB
    '## Character Table
    Public char_race_col() As String
    Public char_class_col() As String
    Public char_gender_col() As String
    Public char_guid_col() As String
    Public char_level_col() As String
    Public char_name_col() As String
    Public char_accountId_col() As String
    Public char_xp_col() As String
    Public char_gold_col() As String
    Public char_playerBytes_col() As String
    Public char_playerBytes2_col() As String
    Public char_playerFlags_col() As String
    Public char_posX_col() As String
    Public char_posY_col() As String
    Public char_posZ_col() As String
    Public char_map_col() As String
    Public char_instanceId_col() As String
    Public char_orientation_col() As String
    Public char_taximask_col() As String
    Public char_cinematic_col() As String
    Public char_totaltime_col() As String
    Public char_leveltime_col() As String
    Public char_stableSlots_col() As String
    Public char_zone_col() As String
    Public char_arenaPoints_col() As String
    Public char_totalHonorPoints_col() As String
    Public char_totalKills_col() As String
    Public char_chosenTitle_col() As String
    Public char_watchedFaction_col() As String
    Public char_extraFlags_col() As String
    Public char_health_col() As String
    Public char_speccount_col() As String
    Public char_activeSpec_col() As String
    Public char_exploredZones_col() As String
    Public char_knownTitles_col() As String
    Public char_arcemuTalentPoints_col() As String
    Public char_finishedQuests_col() As String
    Public char_customFaction_col() As String
    Public char_bindmapid_col() As String
    Public char_bindzoneid_col() As String
    Public char_bindposX_col() As String
    Public char_bindposY_col() As String
    Public char_bindposZ_col() As String
    Public char_arcemuPlayedTime_col() As String
    Public char_instanceModeMask_col() As String
    Public char_atlogin_col() As String
    Public char_knownCurrencies_col() As String
    Public char_equipmentCache_col() As String
    Public char_actionBars_col() As String
    Public char_actions1_col() As String
    Public char_actions2_col() As String
    Public char_glyphs1_col() As String
    Public char_glyphs2_col() As String
    Public char_reputation_col() As String
    Public char_skills_col() As String
    Public char_spells_col() As String
    Public char_talent1_col() As String
    Public char_talent2_col() As String

    '## Character_achievement Table
    Public av_guid_col() As String
    Public av_achievement_col() As String
    Public av_date_col() As String

    '## Character_action Table
    Public action_guid_col() As String
    Public action_spec_col() As String
    Public action_button_col() As String
    Public action_action_col() As String
    Public action_type_col() As String

    '## Character_glyphs Table
    Public glyphs_guid_col() As String
    Public glyphs_spec_col() As String
    Public glyphs_glyph_col() As String
    Public glyphs_slot_col() As String
    Public glyphs_glyph1_col() As String
    Public glyphs_glyph2_col() As String
    Public glyphs_glyph3_col() As String
    Public glyphs_glyph4_col() As String
    Public glyphs_glyph5_col() As String
    Public glyphs_glyph6_col() As String
    Public glyphs_glyph7_col() As String
    Public glyphs_glyph8_col() As String
    Public glyphs_glyph9_col() As String

    '## Character_homebind Table
    Public homebind_guid_col() As String
    Public homebind_map_col() As String
    Public homebind_zone_col() As String
    Public homebind_posx_col() As String
    Public homebind_posy_col() As String
    Public homebind_posz_col() As String

    '## Character_inventory Table
    Public invent_guid_col() As String
    Public invent_bag_col() As String
    Public invent_slot_col() As String
    Public invent_item_col() As String
    Public invent_item_template_col() As String

    '## Character_queststatus Table
    Public qst_guid_col() As String
    Public qst_quest_col() As String
    Public qst_status_col() As String
    Public qst_explored_col() As String
    Public qst_completed_col() As String
    Public qst_timer_col() As String
    Public qst_slot_col() As String
    Public qst_rewarded_col() As String

    '## Character_queststatus_rewarded Table
    Public qstre_guid_col() As String
    Public qstre_quest_col() As String

    '## Character_reputation Table
    Public rep_guid_col() As String
    Public rep_faction_col() As String
    Public rep_standing_col() As String
    Public rep_flags_col() As String

    '## Character_skills Table
    Public skill_guid_col() As String
    Public skill_skill_col() As String
    Public skill_value_col() As String
    Public skill_max_col() As String

    '## Character_spell Table
    Public spell_guid_col() As String
    Public spell_spell_col() As String
    Public spell_active_col() As String
    Public spell_disabled_col() As String

    '## Character_talent Table
    Public talent_guid_col() As String
    Public talent_spell_col() As String
    Public talent_spec_col() As String
    Public talent_rank_col() As String
    Public talent_talent_col() As String

    '## Item_instance Table
    Public itmins_guid_col() As String
    Public itmins_itemEntry_col() As String
    Public itmins_ownerGuid_col() As String
    Public itmins_count_col() As String
    Public itmins_enchantments_col() As String
    Public itmins_data_col() As String
    Public itmins_durability_col() As String
    Public itmins_slot_col() As String
    Public itmins_container_col() As String

    '# Realm DB
    '## Account Table
    Public acc_name_col() As String
    Public acc_id_col() As String
    Public acc_v_col() As String
    Public acc_s_col() As String
    Public acc_sessionkey_col() As String
    Public acc_gmlevel_col() As String
    Public acc_passHash_col() As String
    Public acc_arcemuPass_col() As String
    Public acc_email_col() As String
    Public acc_joindate_col() As String
    Public acc_lastlogin_col() As String
    Public acc_expansion_col() As String
    Public acc_arcemuFlags_col() As String
    Public acc_locale_col() As String
    Public acc_arcemuGmLevel_col() As String
    Public acc_realmID_col() As String
    '## Account Access Table
    Public accAcc_gmLevel_col() As String
    Public accAcc_realmId_col() As String
    Public accAcc_accid_col() As String
    Public Sub New()

    End Sub
End Class
