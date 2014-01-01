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
'*      /Filename:      CoreHandler
'*      /Description:   Handles account/character loading requests
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++s++++++++++++++++++++++++++
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Modules

Namespace Framework.Core

    Public Class CoreHandler

        Public Sub HandleLoadingRequests(ByVal account As Account, ByVal setId As Integer)
            LogAppend("Loading character with setId " & setId.ToString() & " from database",
                      "CoreHandler_handleLoadingRequests")
            If GlobalVariables.GlobalConnection.State = ConnectionState.Closed Then GlobalVariables.GlobalConnection.Open()
            If GlobalVariables.GlobalConnection_Realm.State = ConnectionState.Closed Then _
                GlobalVariables.GlobalConnection_Realm.Open()
            GlobalVariables.forceTargetConnectionUsage = False
            Dim tmpPlayer As Character = GetCharacterSetBySetId(setId, Account)
            If tmpPlayer Is Nothing Then
                LogAppend("Character with setId " & setId.ToString() & " not found!",
                    "CoreHandler_handleLoadingRequests", True, True)
                Exit Sub
            End If
            Dim mBasicsHandler As New CharacterBasicsHandler
            mBasicsHandler.GetBasicCharacterInformation(tmpPlayer.Guid, setId, Account)
            Dim mAvHandler As New CharacterAchievementHandler
            mAvHandler.GetCharacterAchievement(tmpPlayer.Guid, setId, Account)
            Dim mActionsHandler As New CharacterActionsHandler
            mActionsHandler.GetCharacterActions(tmpPlayer.Guid, setId, Account)
            Dim mInventoryHandler As New CharacterInventoryHandler
            mInventoryHandler.GetCharacterInventory(tmpPlayer.Guid, setId, Account)
            Dim mArmorHandler As New CharacterArmorHandler
            mArmorHandler.GetCharacterArmor(tmpPlayer.Guid, setId, Account)
            Dim mGlyphsHandler As New CharacterGlyphsHandler
            mGlyphsHandler.GetCharacterGlyphs(tmpPlayer.Guid, setId, Account)
            Dim mQuestlogHandler As New CharacterQuestlogHandler
            mQuestlogHandler.GetCharacterQuestlog(tmpPlayer.Guid, setId, Account)
            Dim mReputationHandler As New CharacterReputationHandler
            mReputationHandler.GetCharacterReputation(tmpPlayer.Guid, setId, Account)
            Dim mSkillsHandler As New CharacterSkillsHandler
            mSkillsHandler.GetCharacterSkills(tmpPlayer.Guid, setId, Account)
            Dim mSpellsHandler As New CharacterSpellsHandler
            mSpellsHandler.GetCharacterSpells(tmpPlayer.Guid, setId, Account)
            Dim mTalentHandler As New CharacterTalentsHandler
            mTalentHandler.GetCharacterTalents(tmpPlayer.Guid, setId, Account)
            Dim mEnchantmentsHandler As New CharacterEnchantmentsHandler
            mEnchantmentsHandler.HandleEnchantments(setId, Account)
            LogAppend("Loading finished!", "CoreHandler_handleLoadingRequests")
        End Sub
    End Class
End Namespace