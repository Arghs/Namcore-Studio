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
'*      /Filename:      ArmorCreation
'*      /Description:   Includes functions for creating the equipped items of a specific
'*                      character
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.Database
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Modules
Imports System.Text.RegularExpressions

Namespace Framework.Transmission
    Public Class ArmorCreation
        Public Sub AddCharacterArmor(ByVal player As Character, Optional charguid As Integer = 0)
            If charguid = 0 Then charguid = player.Guid
            LogAppend("Adding armor to character: " & charguid.ToString(),
                      "ArmorCreation_AddCharacterArmor", True)
            Try
                Select Case GlobalVariables.targetCore
                    Case "arcemu"
                        CreateAtArcemu(charguid, player)
                    Case "trinity"
                        CreateAtTrinity(charguid, player)
                    Case "trinitytbc"

                    Case "mangos"
                        CreateAtMangos(charguid, player)
                End Select
            Catch ex As Exception
                LogAppend("Exception occured: " & ex.ToString(),
                     "ArmorCreation_AddCharacterArmor", False, True)
            End Try
        End Sub

        Private Sub CreateAtArcemu(ByVal characterguid As Integer, ByVal player As Character)
            LogAppend("Creating armor at arcemu", "ArmorCreation_createAtArcemu", False)
            LogAppend("Adding weapon specific spells and skills", "ArmorCreation_createAtArcemu", False)
            'Adding weapon specific spells and skills
            Dim cClass As Integer = player.Cclass
            If cClass = 1 Or cClass = 2 Or cClass = 6 Then
                AddSpells("750,", player)
                AddSkills("293,", player)
            ElseIf cClass = 1 Or cClass = 2 Or cClass = 3 Or cClass = 6 Or cClass = 7 Then
                AddSpells("8737,", player)
                AddSkills("413,", player)
            ElseIf cClass = 1 Or cClass = 2 Or cClass = 3 Or cClass = 4 Or cClass = 6 Or cClass = 7 Or cClass = 11 Then
                AddSpells("9077,", player)
                AddSkills("414,", player)
            Else
            End If
            LogAppend("Adding items", "ArmorCreation_createAtArcemu", False)
            Dim itemid As Integer
            Dim itemtypelist(18) As String
            itemtypelist(0) = "head"
            itemtypelist(1) = "neck"
            itemtypelist(2) = "shoulder"
            itemtypelist(3) = "shirt"
            itemtypelist(4) = "chest"
            itemtypelist(5) = "waist"
            itemtypelist(6) = "legs"
            itemtypelist(7) = "feet"
            itemtypelist(8) = "wrists"
            itemtypelist(9) = "hands"
            itemtypelist(10) = "finger1"
            itemtypelist(11) = "finger2"
            itemtypelist(12) = "trinket1"
            itemtypelist(13) = "trinket2"
            itemtypelist(14) = "back"
            itemtypelist(15) = "main"
            itemtypelist(16) = "off"
            itemtypelist(17) = "distance"
            itemtypelist(18) = "tabard"
            'Build item type string
            Dim finalItemString As String = itemtypelist.Aggregate("",
                                                                   Function(current, newItemType) _
                                                                      current & newItemType & " 0 ")
            Dim typeCounter As Integer = -1
            Dim newItemGuid As Integer =
                    TryInt(
                        runSQLCommand_characters_string(
                            "SELECT " & GlobalVariables.targetStructure.itmins_guid_col(0) & " FROM " &
                            GlobalVariables.targetStructure.item_instance_tbl(0) & " WHERE " &
                            GlobalVariables.targetStructure.itmins_guid_col(0) &
                            "=(SELECT MAX(" & GlobalVariables.targetStructure.itmins_guid_col(0) & ") FROM " &
                            GlobalVariables.targetStructure.item_instance_tbl(0) & ")"))
            For Each newItemType As String In itemtypelist
                typeCounter += 1
                Dim itm As Item = GetCharacterArmorItem(player, newItemType)
                If itemid = 0 Then Continue For
                itemid = itm.Id
                If itemid = 0 Then Continue For
                newItemGuid += 1
                finalItemString = finalItemString.Replace(newItemType, itemid.ToString())
                If _
                    ReturnResultCount(
                        "SELECT * FROM " & GlobalVariables.targetStructure.item_instance_tbl(0) & " WHERE " &
                        GlobalVariables.targetStructure.itmins_ownerGuid_col(0) & "='" & characterguid.ToString() &
                        "' AND " &
                        GlobalVariables.targetStructure.itmins_slot_col(0) & " = '" & typeCounter.ToString() & "' AND " &
                        GlobalVariables.targetStructure.itmins_container_col(0) & "='-1'") > 0 Then
                    runSQLCommand_characters_string(
                        "DELETE FROM " & GlobalVariables.targetStructure.item_instance_tbl(0) & " WHERE " &
                        GlobalVariables.targetStructure.itmins_ownerGuid_col(0) & " = '" & characterguid.ToString() &
                        "' AND " & GlobalVariables.targetStructure.itmins_slot_col(0) & " = '" & typeCounter.ToString() &
                        "'")
                    runSQLCommand_characters_string(
                        "INSERT INTO " & GlobalVariables.targetStructure.item_instance_tbl(0) & " ( " &
                        GlobalVariables.targetStructure.itmins_guid_col(0) & ", " &
                        GlobalVariables.targetStructure.itmins_ownerGuid_col(0) &
                        ", " & GlobalVariables.targetStructure.itmins_itemEntry_col(0) & ", " &
                        GlobalVariables.targetStructure.itmins_container_col(0) & ", " &
                        GlobalVariables.targetStructure.itmins_slot_col(0) &
                        ") VALUES ( '" & newItemGuid.ToString() & "', '" & characterguid & "', '" &
                        itemid.ToString & "', '-1', '" & typeCounter.ToString() & "' )")
                Else
                    runSQLCommand_characters_string(
                        "INSERT INTO " & GlobalVariables.targetStructure.item_instance_tbl(0) & " ( " &
                        GlobalVariables.targetStructure.itmins_guid_col(0) & ", " &
                        GlobalVariables.targetStructure.itmins_ownerGuid_col(0) &
                        ", " & GlobalVariables.targetStructure.itmins_itemEntry_col(0) & ", " &
                        GlobalVariables.targetStructure.itmins_container_col(0) & ", " &
                        GlobalVariables.targetStructure.itmins_slot_col(0) &
                        ") VALUES ( '" & newItemGuid.ToString() & "', '" & characterguid & "', '" &
                        itemid.ToString & "', '-1', '" & typeCounter.ToString() & "' )")
                End If
                Dim mEnchCreator As New EnchantmentsCreation
                mEnchCreator.SetItemEnchantments(player, itm, newItemGuid, GlobalVariables.targetCore,
                                                 GlobalVariables.targetStructure)
            Next
            If Not Regex.IsMatch(finalItemString, "^[0-9 ]+$") Then
                'TODO finalItemString = itemtypelist.Aggregate(finalItemString, Function(current, itemtype) current.Replace(itemtype, "0"))
            End If
        End Sub

        Private Sub CreateAtTrinity(ByVal characterguid As Integer, ByVal player As Character)
            LogAppend("Creating armor at trinity", "ArmorCreation_createAtTrinity", False)
            LogAppend("Adding weapon specific spells and skills", "ArmorCreation_createAtTrinity", False)
            'Adding weapon specific spells and skills
            Dim cClass As Integer = player.Cclass
            If cClass = 1 Or cClass = 2 Or cClass = 6 Then
                AddSpells("750,", player)
                AddSkills("293,", player)
            ElseIf cClass = 1 Or cClass = 2 Or cClass = 3 Or cClass = 6 Or cClass = 7 Then
                AddSpells("8737,", player)
                AddSkills("413,", player)
            ElseIf cClass = 1 Or cClass = 2 Or cClass = 3 Or cClass = 4 Or cClass = 6 Or cClass = 7 Or cClass = 11 Then
                AddSpells("9077,", player)
                AddSkills("414,", player)
            Else
            End If
            LogAppend("Adding items", "ArmorCreation_createAtTrinity", False)
            Dim itemid As Integer
            Dim itemtypelist(18) As String
            itemtypelist(0) = "head"
            itemtypelist(1) = "neck"
            itemtypelist(2) = "shoulder"
            itemtypelist(3) = "shirt"
            itemtypelist(4) = "chest"
            itemtypelist(5) = "waist"
            itemtypelist(6) = "legs"
            itemtypelist(7) = "feet"
            itemtypelist(8) = "wrists"
            itemtypelist(9) = "hands"
            itemtypelist(10) = "finger1"
            itemtypelist(11) = "finger2"
            itemtypelist(12) = "trinket1"
            itemtypelist(13) = "trinket2"
            itemtypelist(14) = "back"
            itemtypelist(15) = "main"
            itemtypelist(16) = "off"
            itemtypelist(17) = "distance"
            itemtypelist(18) = "tabard"
            'Build item type string
            Dim finalItemString As String = itemtypelist.Aggregate("",
                                                                   Function(current, newItemType) _
                                                                      current & newItemType & " 0 ")
            Dim typeCounter As Integer = -1
            Dim newItemGuid As Integer =
                    TryInt(
                        runSQLCommand_characters_string(
                            "SELECT " & GlobalVariables.targetStructure.itmins_guid_col(0) & " FROM " &
                            GlobalVariables.targetStructure.item_instance_tbl(0) & " WHERE " &
                            GlobalVariables.targetStructure.itmins_guid_col(0) & "=(SELECT MAX(" &
                            GlobalVariables.targetStructure.itmins_guid_col(0) & ") FROM " &
                            GlobalVariables.targetStructure.item_instance_tbl(0) & ")"))
            For Each newItemType As String In itemtypelist
                Dim itm As Item = GetCharacterArmorItem(player, newItemType)
                typeCounter += 1
                If itm Is Nothing Then Continue For
                itemid = itm.Id
                If itemid = 0 Then Continue For
                newItemGuid += 1
                finalItemString = finalItemString.Replace(newItemType, itemid.ToString())
                runSQLCommand_characters_string(
                    "INSERT INTO " & GlobalVariables.targetStructure.item_instance_tbl(0) & " ( " &
                    GlobalVariables.targetStructure.itmins_guid_col(0) & ", " &
                    GlobalVariables.targetStructure.itmins_itemEntry_col(0) & ", " &
                    GlobalVariables.targetStructure.itmins_ownerGuid_col(0) & ", " &
                    GlobalVariables.targetStructure.itmins_count_col(0) & ", " &
                    GlobalVariables.targetStructure.itmins_enchantments_col(0) &
                    ", " & GlobalVariables.targetStructure.itmins_durability_col(0) & " ) VALUES ( '" &
                    newItemGuid.ToString() & "', '" & itemid & "', '" & characterguid.ToString() &
                    "', '1', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )")
                If _
                    ReturnResultCount(
                        "SELECT * FROM " & GlobalVariables.targetStructure.character_inventory_tbl(0) & " WHERE " &
                        GlobalVariables.targetStructure.invent_guid_col(0) & "='" & characterguid.ToString() & "' AND " &
                        GlobalVariables.targetStructure.invent_slot_col(0) & " = '" & typeCounter.ToString() & "'") > 0 _
                    Then
                    runSQLCommand_characters_string(
                        "DELETE FROM " & GlobalVariables.targetStructure.character_inventory_tbl(0) & " WHERE " &
                        GlobalVariables.targetStructure.invent_guid_col(0) & " = '" & characterguid.ToString() &
                        "' AND " & GlobalVariables.targetStructure.invent_slot_col(0) & " = '" & typeCounter.ToString() &
                        "'")
                    runSQLCommand_characters_string(
                        "INSERT INTO " & GlobalVariables.targetStructure.character_inventory_tbl(0) & " ( " &
                        GlobalVariables.targetStructure.invent_guid_col(0) & ", " &
                        GlobalVariables.targetStructure.invent_slot_col(0) &
                        ", " & GlobalVariables.targetStructure.invent_item_col(0) & " ) VALUES ( '" &
                        characterguid.ToString() & "', '" & typeCounter.ToString() & "', '" & newItemGuid.ToString() &
                        "' )")
                Else
                    runSQLCommand_characters_string(
                        "INSERT INTO " & GlobalVariables.targetStructure.character_inventory_tbl(0) & " ( " &
                        GlobalVariables.targetStructure.invent_guid_col(0) & ", " &
                        GlobalVariables.targetStructure.invent_slot_col(0) & ", " &
                        GlobalVariables.targetStructure.invent_item_col(0) & " ) VALUES ( '" & characterguid.ToString() &
                        "', '" & typeCounter.ToString() & "', '" & newItemGuid.ToString() & "' )")
                End If
                Dim mEnchCreator As New EnchantmentsCreation
                mEnchCreator.SetItemEnchantments(player, itm, newItemGuid, GlobalVariables.targetCore,
                                                 GlobalVariables.targetStructure)
            Next
            If Not Regex.IsMatch(finalItemString, "^[0-9 ]+$") Then
                finalItemString = itemtypelist.Aggregate(finalItemString,
                                                         Function(current, itemtype) current.Replace(itemtype, "0"))
            End If
            runSQLCommand_characters_string(
                "UPDATE " & GlobalVariables.targetStructure.character_tbl(0) & " SET " &
                GlobalVariables.targetStructure.char_equipmentCache_col(0) & "='" & finalItemString & "' WHERE (" &
                GlobalVariables.targetStructure.char_guid_col(0) & "='" & characterguid.ToString() & "')")
        End Sub

        Private Sub CreateAtMangos(ByVal characterguid As Integer, ByVal player As Character)
            LogAppend("Creating armor at mangos", "ArmorCreation_createAtMangos", False)
            LogAppend("Adding weapon specific spells and skills", "ArmorCreation_createAtMangos", False)
            'Adding weapon specific spells and skills
            Dim cClass As Integer = player.Cclass
            If cClass = 1 Or cClass = 2 Or cClass = 6 Then
                AddSpells("750,", player)
                AddSkills("293,", player)
            ElseIf cClass = 1 Or cClass = 2 Or cClass = 3 Or cClass = 6 Or cClass = 7 Then
                AddSpells("8737,", player)
                AddSkills("413,", player)
            ElseIf cClass = 1 Or cClass = 2 Or cClass = 3 Or cClass = 4 Or cClass = 6 Or cClass = 7 Or cClass = 11 Then
                AddSpells("9077,", player)
                AddSkills("414,", player)
            Else
            End If
            LogAppend("Adding items", "ArmorCreation_createAtMangos", False)
            Dim itemid As Integer
            Dim itemtypelist(18) As String
            itemtypelist(0) = "head"
            itemtypelist(1) = "neck"
            itemtypelist(2) = "shoulder"
            itemtypelist(3) = "shirt"
            itemtypelist(4) = "chest"
            itemtypelist(5) = "waist"
            itemtypelist(6) = "legs"
            itemtypelist(7) = "feet"
            itemtypelist(8) = "wrists"
            itemtypelist(9) = "hands"
            itemtypelist(10) = "finger1"
            itemtypelist(11) = "finger2"
            itemtypelist(12) = "trinket1"
            itemtypelist(13) = "trinket2"
            itemtypelist(14) = "back"
            itemtypelist(15) = "main"
            itemtypelist(16) = "off"
            itemtypelist(17) = "distance"
            itemtypelist(18) = "tabard"
            'Build item type string
            Dim finalItemString As String = itemtypelist.Aggregate("",
                                                                   Function(current, newItemType) _
                                                                      current & newItemType & " 0 ")
            Dim typeCounter As Integer = -1
            Dim newItemGuid As Integer =
                    TryInt(
                        runSQLCommand_characters_string(
                            "SELECT " & GlobalVariables.targetStructure.itmins_guid_col(0) & " FROM " &
                            GlobalVariables.targetStructure.item_instance_tbl(0) & " WHERE " &
                            GlobalVariables.targetStructure.itmins_guid_col(0) & "=(SELECT MAX(" &
                            GlobalVariables.targetStructure.itmins_guid_col(0) & ") FROM " &
                            GlobalVariables.targetStructure.item_instance_tbl(0) & ")"))
            For Each newItemType As String In itemtypelist
                typeCounter += 1
                Dim itm As Item = GetCharacterArmorItem(player, newItemType)
                If itemid = 0 Then Continue For
                itemid = itm.Id
                If itemid = 0 Then Continue For
                newItemGuid += 1
                finalItemString = finalItemString.Replace(newItemType, itemid.ToString())
                If GlobalVariables.targetExpansion >= 3 Then
                    runSQLCommand_characters_string(
                        "INSERT INTO " & GlobalVariables.targetStructure.item_instance_tbl(0) & " ( " &
                        GlobalVariables.targetStructure.itmins_guid_col(0) & ", " &
                        GlobalVariables.targetStructure.itmins_ownerGuid_col(0) &
                        ", " & GlobalVariables.targetStructure.itmins_data_col(0) & ") VALUES ( '" &
                        newItemGuid.ToString() &
                        "', '" & characterguid.ToString() &
                        "', '" & newItemGuid.ToString() & " 1191182336 3 " & itemid.ToString() &
                        " 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 100 100 0 0 ')")
                Else
                    runSQLCommand_characters_string(
                        "INSERT INTO " & GlobalVariables.targetStructure.item_instance_tbl(0) & " ( " &
                        GlobalVariables.targetStructure.itmins_guid_col(0) & ", " &
                        GlobalVariables.targetStructure.itmins_ownerGuid_col(0) &
                        ", " & GlobalVariables.targetStructure.itmins_data_col(0) & ") VALUES ( '" &
                        newItemGuid.ToString() &
                        "', '" & characterguid.ToString() &
                        "', '" & newItemGuid.ToString() & " 1191182336 3 " & itemid.ToString() &
                        " 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 100 100 0 0 ')")
                End If
                If _
                    ReturnResultCount(
                        "SELECT * FROM " & GlobalVariables.targetStructure.character_inventory_tbl(0) & " WHERE " &
                        GlobalVariables.targetStructure.invent_guid_col(0) & "='" & characterguid.ToString() & "' AND " &
                        GlobalVariables.targetStructure.invent_slot_col(0) & " = '" & typeCounter.ToString() & "'") > 0 _
                    Then
                    runSQLCommand_characters_string(
                        "DELETE FROM " & GlobalVariables.targetStructure.character_inventory_tbl(0) & " WHERE " &
                        GlobalVariables.targetStructure.invent_guid_col(0) & " = '" & characterguid.ToString() &
                        "' AND " & GlobalVariables.targetStructure.invent_slot_col(0) & " = '" & typeCounter.ToString() &
                        "'")
                    runSQLCommand_characters_string(
                        "INSERT INTO " & GlobalVariables.targetStructure.character_inventory_tbl(0) & " ( " &
                        GlobalVariables.targetStructure.invent_guid_col(0) & ", " &
                        GlobalVariables.targetStructure.invent_bag_col(0) &
                        ", " & GlobalVariables.targetStructure.invent_slot_col(0) & ", " &
                        GlobalVariables.targetStructure.invent_item_col(0) & ", " &
                        GlobalVariables.targetStructure.invent_item_template_col(0) & " ) VALUES " &
                        "( '" & characterguid.ToString() & "', '0', '" & typeCounter.ToString() & "', '" &
                        newItemGuid.ToString() & "', '" & itemid.ToString & "' )")
                Else
                    runSQLCommand_characters_string(
                        "INSERT INTO " & GlobalVariables.targetStructure.character_inventory_tbl(0) & " ( " &
                        GlobalVariables.targetStructure.invent_guid_col(0) & ", " &
                        GlobalVariables.targetStructure.invent_bag_col(0) &
                        ", " & GlobalVariables.targetStructure.invent_slot_col(0) & ", " &
                        GlobalVariables.targetStructure.invent_item_col(0) & ", " &
                        GlobalVariables.targetStructure.invent_item_template_col(0) & " ) VALUES " &
                        "( '" & characterguid.ToString() & "', '0', '" & typeCounter.ToString() & "', '" &
                        newItemGuid.ToString() & "', '" & itemid.ToString & "' )")
                End If
                Dim mEnchCreator As New EnchantmentsCreation
                mEnchCreator.SetItemEnchantments(player, itm, newItemGuid, GlobalVariables.targetCore,
                                                 GlobalVariables.targetStructure)
            Next
            If Not Regex.IsMatch(finalItemString, "^[0-9 ]+$") Then
                finalItemString = itemtypelist.Aggregate(finalItemString,
                                                         Function(current, itemtype) current.Replace(itemtype, "0"))
            End If
            runSQLCommand_characters_string(
                "UPDATE " & GlobalVariables.targetStructure.character_tbl(0) & " SET " &
                GlobalVariables.targetStructure.char_equipmentCache_col(0) & "='" & finalItemString & "' WHERE (" &
                GlobalVariables.targetStructure.char_guid_col(0) &
                "='" & characterguid.ToString() & "')")
        End Sub
    End Class
End Namespace