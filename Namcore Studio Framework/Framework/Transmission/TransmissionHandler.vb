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
Imports NCFramework.GlobalVariables
Public Class TransmissionHandler
    Public Sub handleMigrationRequests(ByVal lite As Boolean)
        If TargetConnection.State = ConnectionState.Closed Then TargetConnection.Open()
        If TargetConnection_Realm.State = ConnectionState.Closed Then TargetConnection_Realm.Open()
        forceTargetConnectionUsage = True
        '// Creating new Accounts
        For Each index As Integer In createAccountsIndex
            'CreateNewAccount(accountInfo.Item(index).name
        Next
        '// Creating Characters
        For Each playerCharacter In charactersToCreate
            Dim accountId As Integer = TryInt(splitString(playerCharacter, "{AccountId}", "{/AccountId}"))
            Dim setId As Integer = TryInt(splitString(playerCharacter, "{setId}", "{/setId}"))
            Dim renamePending As Boolean
            Select Case splitString(playerCharacter, "{renamePending}", "{/renamePending}")
                Case "0" : renamePending = False
                Case "1" : renamePending = True
                Case Else : renamePending = False
            End Select
            Dim player As Character = GetCharacterSetBySetId(setId)
            Dim charname As String = player.Name
            Dim m_charCreationLite As New CharacterCreationLite
            Dim m_charCreationAdvanced As New CharacterCreationAdvanced
            If lite Then
                m_charCreationLite.CreateNewLiteCharacter(charname, accountId, setId, renamePending)
            Else
                m_charCreationAdvanced.CreateNewAdvancedCharacter(charname, accountId.ToString, setId, renamePending)
            End If
            Dim m_charArmorCreation As New ArmorCreation
            Dim m_charGlyphCreation As New GlyphCreation
            Dim m_charQuestCreation As New QuestCreation
            Dim m_charTalentCreation As New TalentCreation
            m_charArmorCreation.AddCharacterArmor(setId)
            m_charGlyphCreation.SetCharacterGlyphs(setId)
            m_charQuestCreation.SetCharacterQuests(setId)
            If Not lite Then m_charTalentCreation.SetCharacterTalents(setId)
        Next
        forceTargetConnectionUsage = False
    End Sub
End Class
