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
'*      /Filename:      TransmissionHandler
'*      /Description:   Handles account/character migration requests
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Module

Namespace Framework.Transmission
    Public Class TransmissionHandler
        Public Sub HandleMigrationRequests(ByVal lite As Boolean)
            If GlobalVariables.TargetConnection.State = ConnectionState.Closed Then GlobalVariables.TargetConnection.Open()
            If GlobalVariables.TargetConnection_Realm.State = ConnectionState.Closed Then _
                GlobalVariables.TargetConnection_Realm.Open()
            GlobalVariables.forceTargetConnectionUsage = True
            '// Creating new Accounts
            ' ReSharper disable RedundantAssignment
            For Each index As Integer In GlobalVariables.createAccountsIndex
                ' ReSharper restore RedundantAssignment
                'CreateNewAccount(accountInfo.Item(index).name
            Next
            '// Creating Characters
            For Each playerCharacter In GlobalVariables.charactersToCreate
                Dim accountId As Integer = TryInt(SplitString(playerCharacter, "{AccountId}", "{/AccountId}"))
                Dim setId As Integer = TryInt(SplitString(playerCharacter, "{setId}", "{/setId}"))
                Dim renamePending As Boolean
                Select Case SplitString(playerCharacter, "{renamePending}", "{/renamePending}")
                    Case "0" : renamePending = False
                    Case "1" : renamePending = True
                    Case Else : renamePending = False
                End Select
                Dim player As Character = GetCharacterSetBySetId(setId)
                Dim charname As String = player.Name
                Dim mCharCreationLite As New CharacterCreationLite
                Dim mCharCreationAdvanced As New CharacterCreationAdvanced
                If lite Then
                    mCharCreationLite.CreateNewLiteCharacter(charname, accountId, setId, renamePending)
                Else
                    mCharCreationAdvanced.CreateNewAdvancedCharacter(charname, accountId.ToString, setId, renamePending)
                End If
                Dim mCharArmorCreation As New ArmorCreation
                Dim mCharGlyphCreation As New GlyphCreation
                Dim mCharQuestCreation As New QuestCreation
                Dim mCharTalentCreation As New TalentCreation
                mCharArmorCreation.AddCharacterArmor(setId)
                mCharGlyphCreation.SetCharacterGlyphs(setId)
                mCharQuestCreation.SetCharacterQuests(setId)
                If Not lite Then mCharTalentCreation.SetCharacterTalents(setId)
            Next
            GlobalVariables.forceTargetConnectionUsage = False
        End Sub
    End Class
End Namespace