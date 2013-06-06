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
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++s++++++++++++++++++++++++++
Imports Namcore_Studio.GlobalVariables
Imports Namcore_Studio.AccountCreation
Imports Namcore_Studio.CharacterCreationAdvanced
Imports Namcore_Studio.CharacterCreationLite
Imports Namcore_Studio.Conversions
Imports Namcore_Studio.Basics
Imports Namcore_Studio.ArmorCreation
Imports Namcore_Studio.GlyphCreation
Imports Namcore_Studio.QuestCreation
Imports Namcore_Studio.TalentCreation

Public Class TransmissionHandler
    Public Shared Sub handleMigrationRequests(ByVal lite As Boolean)
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
            If lite Then
                CreateNewLiteCharacter(charname, accountId, setId, renamePending)
            Else
                CreateNewAdvancedCharacter(charname, accountId.ToString, setId, renamePending)
            End If
            AddCharacterArmor(setId)
            SetCharacterGlyphs(setId)
            SetCharacterQuests(setId)
            SetCharacterTalents(setId)
        Next

    End Sub
End Class
