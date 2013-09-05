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
'*      /Filename:      CoreHandler
'*      /Description:   Handles account/character loading requests
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++s++++++++++++++++++++++++++
Imports NCFramework.GlobalVariables

Public Class CoreHandler
    Public Sub handleLoadingRequests(ByVal setId As Integer)
        LogAppend("Loading character with setId " & setId.ToString() & " from database", "CoreHandler_handleLoadingRequests")
        If GlobalConnection.State = ConnectionState.Closed Then GlobalConnection.Open()
        If GlobalConnection_Realm.State = ConnectionState.Closed Then GlobalConnection_Realm.Open()
        forceTargetConnectionUsage = False
        Dim tmpPlayer As Character = GetCharacterSetBySetId(setId)
        Dim m_basicsHandler As New CharacterBasicsHandler
        m_basicsHandler.GetBasicCharacterInformation(tmpPlayer.Guid, setId, tmpPlayer.AccountId)
        Dim m_avHandler As New CharacterAchievementHandler
        m_avHandler.GetCharacterAchievement(tmpPlayer.Guid, setId, tmpPlayer.AccountId)
        Dim m_actionsHandler As New CharacterActionsHandler
        m_actionsHandler.GetCharacterActions(tmpPlayer.Guid, setId, tmpPlayer.AccountId)
        Dim m_inventoryHandler As New CharacterInventoryHandler
        m_inventoryHandler.GetCharacterInventory(tmpPlayer.Guid, setId, tmpPlayer.AccountId)
        Dim m_armorHandler As New CharacterArmorHandler
        m_armorHandler.GetCharacterArmor(tmpPlayer.Guid, setId, tmpPlayer.AccountId)
        Dim m_glyphsHandler As New CharacterGlyphsHandler
        m_glyphsHandler.GetCharacterGlyphs(tmpPlayer.Guid, setId, tmpPlayer.AccountId)
        Dim m_questlogHandler As New CharacterQuestlogHandler
        m_questlogHandler.GetCharacterQuestlog(tmpPlayer.Guid, setId, tmpPlayer.AccountId)
        Dim m_reputationHandler As New CharacterReputationHandler
        m_reputationHandler.GetCharacterReputation(tmpPlayer.Guid, setId, tmpPlayer.AccountId)
        Dim m_skillsHandler As New CharacterSkillsHandler
        m_skillsHandler.GetCharacterSkills(tmpPlayer.Guid, setId, tmpPlayer.AccountId)
        Dim m_spellsHandler As New CharacterSpellsHandler
        m_spellsHandler.GetCharacterSpells(tmpPlayer.Guid, setId, tmpPlayer.AccountId)
        Dim m_talentHandler As New CharacterTalentsHandler
        m_talentHandler.GetCharacterTalents(tmpPlayer.Guid, setId, tmpPlayer.AccountId)
        Dim m_enchantmentsHandler As New CharacterEnchantmentsHandler
        m_enchantmentsHandler.HandleEnchantments(setId)
        LogAppend("Loading finished!", "CoreHandler_handleLoadingRequests")
    End Sub
End Class
