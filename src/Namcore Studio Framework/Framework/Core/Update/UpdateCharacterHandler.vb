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
'*      /Filename:      UpdateCharacterHandler
'*      /Description:   Handles character update requests
'++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Database
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Modules

Namespace Framework.Core.Update

    Public Class UpdateCharacterHandler
        Public Sub UpdateCharacter(ByVal comparePlayer As Character, ByVal newPlayer As Character)
            LogAppend("Updating character " & comparePlayer.Name, "UpdateCharacterHandler_UpdateCharacter", True)
            If GlobalVariables.GlobalConnection.State = ConnectionState.Closed Then GlobalVariables.GlobalConnection.Open()
            If GlobalVariables.GlobalConnection_Realm.State = ConnectionState.Closed Then _
                GlobalVariables.GlobalConnection_Realm.Open()
            GlobalVariables.forceTargetConnectionUsage = False
            If Not newPlayer.AccountId = comparePlayer.AccountId Then
                '// Account changed
                Select Case GlobalVariables.sourceCore
                    Case "trinity"
                        runSQLCommand_characters_string(
                            "UPDATE `" & GlobalVariables.sourceStructure.character_tbl(0) & "` SET `" &
                            GlobalVariables.sourceStructure.char_accountId_col(0) &
                            "`='" & newPlayer.AccountId.ToString() & "' WHERE `" &
                            GlobalVariables.sourceStructure.char_guid_col(0) & "`='" & newPlayer.Guid.ToString() & "'")
                    Case Else
                        runSQLCommand_characters_string(
                            "UPDATE `" & GlobalVariables.sourceStructure.character_tbl(0) & "` SET `" &
                            GlobalVariables.sourceStructure.char_accountId_col(0) &
                            "`='" & newPlayer.AccountId.ToString() & "' WHERE `" &
                            GlobalVariables.sourceStructure.char_guid_col(0) & "`='" & newPlayer.Guid.ToString() & "'")
                End Select

            End If
            If Not newPlayer.Name = comparePlayer.Name Then
                '// Name changed
                runSQLCommand_characters_string(
                    "UPDATE `" & GlobalVariables.sourceStructure.character_tbl(0) & "` SET `" &
                    GlobalVariables.sourceStructure.char_name_col(0) &
                    "`='" & newPlayer.Name & "' WHERE `" & GlobalVariables.sourceStructure.char_guid_col(0) & "`='" &
                    newPlayer.Guid.ToString() & "'")
            End If
            If Not newPlayer.Race = comparePlayer.Race Then
                '// Race changed
                runSQLCommand_characters_string(
                    "UPDATE `" & GlobalVariables.sourceStructure.character_tbl(0) & "` SET `" &
                    GlobalVariables.sourceStructure.char_race_col(0) &
                    "`='" & newPlayer.Race.ToString() & "' WHERE `" & GlobalVariables.sourceStructure.char_guid_col(0) &
                    "`='" & newPlayer.Guid.ToString() & "'")
            End If
            If Not newPlayer.Cclass = comparePlayer.Cclass Then
                '// Class changed
                runSQLCommand_characters_string(
                    "UPDATE `" & GlobalVariables.sourceStructure.character_tbl(0) & "` SET `" &
                    GlobalVariables.sourceStructure.char_class_col(0) &
                    "`='" & newPlayer.Cclass.ToString() & "' WHERE `" & GlobalVariables.sourceStructure.char_guid_col(0) &
                    "`='" & newPlayer.Guid.ToString() & "'")
            End If
            If Not newPlayer.Gender = comparePlayer.Gender Then
                '// Gender changed
                runSQLCommand_characters_string(
                    "UPDATE `" & GlobalVariables.sourceStructure.character_tbl(0) & "` SET `" &
                    GlobalVariables.sourceStructure.char_gender_col(0) &
                    "`='" & newPlayer.Gender.ToString() & "' WHERE `" & GlobalVariables.sourceStructure.char_guid_col(0) &
                    "`='" & newPlayer.Guid.ToString() & "'")
            End If
            If Not newPlayer.Level = comparePlayer.Level Then
                '// Level changed
                runSQLCommand_characters_string(
                    "UPDATE `" & GlobalVariables.sourceStructure.character_tbl(0) & "` SET `" &
                    GlobalVariables.sourceStructure.char_level_col(0) &
                    "`='" & newPlayer.Level.ToString() & "' WHERE `" & GlobalVariables.sourceStructure.char_guid_col(0) &
                    "`='" & newPlayer.Guid.ToString() & "'")
            End If
            Dim itm2Create As New List(Of Item)
            Dim itmsEnchChanged As New List(Of Item)
            For Each armorItm As Item In newPlayer.ArmorItems
                If armorItm Is Nothing Then Continue For
                If armorItm.Id = 0 Then Continue For
                Dim entry As Item = comparePlayer.ArmorItems.Find(Function(item) item.Id = armorItm.Id)
                If entry Is Nothing Then
                    '// Item needs to be created
                    itm2Create.Add(entry)
                Else
                    '// Item already exists: Check if gems/enchantments changed
                    If Not armorItm.EnchantmentId = entry.EnchantmentId Then
                        itmsEnchChanged.Add(entry)
                    Else
                        If _
                            Not armorItm.Socket1Id = entry.Socket1Id Or Not armorItm.Socket2Id = entry.Socket2Id Or
                            Not armorItm.Socket3Id = entry.Socket3Id Then
                            itmsEnchChanged.Add(entry)
                        End If
                    End If
                End If
            Next
            Dim itm2Delete As List(Of Item) =
                    (From armorItm In comparePlayer.ArmorItems
                    Where GetCharacterArmorItem(newPlayer, armorItm.Slot, True) Is Nothing).ToList()
            If itm2Delete Is Nothing Then itm2Delete = New List(Of Item)()
            If itm2Create.Count > 0 Or itmsEnchChanged.Count > 0 Or itm2Delete.Count > 0 Then
                Dim mUpdateArmor As New UpdateArmorHandler
                mUpdateArmor.UpdateArmor(comparePlayer, newPlayer, itm2Create, itm2Delete, itmsEnchChanged)
            End If
            If Not newPlayer.PlayerGlyphsIndex = comparePlayer.PlayerGlyphsIndex Then
                Dim mUpdateGlyphs As New UpdateGlyphsHandler
                mUpdateGlyphs.UpdateGlyphs(comparePlayer, newPlayer)
            End If
            Dim result As Item = newPlayer.InventoryItems.Find(Function(item) Not item.UpdateRequest = 0)
            Dim resultZero As Item = newPlayer.InventoryZeroItems.Find(Function(item) Not item.UpdateRequest = 0)
            If Not result Is Nothing And Not resultZero Is Nothing Then
                Dim mUpdateInventory As New UpdateInventoryHandler
                mUpdateInventory.UpdateInventory(newPlayer)
            End If
            Dim mUpdateQuests As New UpdateQuestsHandler
            mUpdateQuests.UpdateQuestlog(comparePlayer, newPlayer)
            LogAppend("Update completed", "UpdateCharacterHandler_UpdateCharacter", True)
        End Sub
    End Class
End Namespace