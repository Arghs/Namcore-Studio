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
'*      /Filename:      TransmissionHandler
'*      /Description:   Handles account/character migration requests
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.Core
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Modules

Namespace Framework.Transmission
    Public Class TransmissionHandler
        Public Sub HandleMigrationRequests(lite As Boolean)
            NewProcessStatus()
            LogAppend("Handling migration requests", "TransmissionHandler_HandleMigrationRequests", True)
            If GlobalVariables.TargetConnection.State = ConnectionState.Closed Then _
                GlobalVariables.TargetConnection.Open()
            If GlobalVariables.TargetConnection_Realm.State = ConnectionState.Closed Then _
                GlobalVariables.TargetConnection_Realm.Open()
            '// Creating new Accounts
            ' ReSharper disable RedundantAssignment
            For Each account As Account In GlobalVariables.accountsToCreate
                ' ReSharper restore RedundantAssignment
                'CreateNewAccount(accountInfo.Item(index).name
            Next
            '// Creating Characters
            ResetTempDataTables()
            For Each playerCharacter As Character In GlobalVariables.charactersToCreate
                LogAppend("Migrating character " & playerCharacter.Name, "TransmissionHandler_HandleMigrationRequests",
                          True)
                Dim playerAccount As Account = playerCharacter.TargetAccount
                Dim accountId As Integer = playerAccount.Id
                Dim renamePending As Boolean = playerCharacter.RenamePending
                Dim charname As String = playerCharacter.Name
                Dim mCharCreationLite As New CharacterCreationLite
                Dim mCharCreationAdvanced As New CharacterCreationAdvanced
                Dim mCharLoading As New CoreHandler
                If playerCharacter.Loaded = False Then
                    GlobalVariables.forceTargetConnectionUsage = False
                    mCharLoading.HandleLoadingRequests(GetAccountSetBySetId(playerCharacter.AccountSet),
                                                       playerCharacter.SetIndex)
                End If
                GlobalVariables.forceTargetConnectionUsage = True
                If lite Then
                    If _
                        mCharCreationLite.CreateNewLiteCharacter(charname, accountId, playerCharacter, renamePending) =
                        False Then Continue For
                Else
                    If mCharCreationAdvanced.CreateNewAdvancedCharacter(charname, accountId.ToString, playerCharacter,
                                                                        renamePending) = False Then Continue For
                End If
                playerCharacter.Guid = playerCharacter.CreatedGuid
                Dim mCharArmorCreation As New ArmorCreation
                Dim mCharGlyphCreation As New GlyphCreation
                Dim mCharQuestCreation As New QuestCreation
                Dim mCharTalentCreation As New TalentCreation
                Dim mCharReputationCreation As New ReputationCreation
                Dim mCharAchievementCreation As New AchievementCreation
                Dim mCharProfessionCreation As New ProfessionCreation
                Dim mCharInventoryCreation As New InventoryCreation
                Dim mCharActionCreation As New ActionCreation
                mCharArmorCreation.AddCharacterArmor(playerCharacter)
                mCharInventoryCreation.AddCharacterInventory(playerCharacter)
                mCharGlyphCreation.SetCharacterGlyphs(playerCharacter)
                mCharQuestCreation.SetCharacterQuests(playerCharacter)
                mCharReputationCreation.AddCharacterReputation(playerCharacter)
                mCharAchievementCreation.AddCharacterAchievements(playerCharacter)
                mCharProfessionCreation.AddCharacterProfessions(playerCharacter)
                mCharActionCreation.SetCharacterActions(playerCharacter)
                AddCharacterSkills(playerCharacter)
                AddCharacterSpells(playerCharacter)
                If Not lite Then mCharTalentCreation.SetCharacterTalents(playerCharacter)
                LogAppend("Character has been created!", "TransmissionHandler_HandleMigrationRequests", True)
            Next
            LogAppend("All migration requests handled", "TransmissionHandler_HandleMigrationRequests", True)
            CloseProcessStatus()
            GlobalVariables.forceTargetConnectionUsage = False
            If GlobalVariables.accountsToCreate IsNot Nothing Then GlobalVariables.accountsToCreate.Clear()
            If GlobalVariables.charactersToCreate IsNot Nothing Then GlobalVariables.charactersToCreate.Clear()
            If GlobalVariables.trans_acclist IsNot Nothing Then GlobalVariables.trans_acclist.Clear()
            If GlobalVariables.trans_charlist IsNot Nothing Then GlobalVariables.trans_charlist.Clear()
        End Sub
    End Class
End Namespace