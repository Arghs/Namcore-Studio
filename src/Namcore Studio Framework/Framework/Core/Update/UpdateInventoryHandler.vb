﻿'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
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
'*      /Filename:      UpdateInventoryHandler
'*      /Description:   Handles character armor update requests
'++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.Database
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Modules
Imports NCFramework.Framework.Transmission

Namespace Framework.Core.Update
    Public Class UpdateInventoryHandler
        Public Sub UpdateInventory(comparePlayer As Character, modPlayer As Character)
            LogAppend("Updating character inventory", "UpdateInventoryHandler_UpdateInventory", True)
            '// Any deleted items?
            For Each itm As Item In comparePlayer.InventoryZeroItems
                Select Case itm.Slot
                    Case 19 To 22, 67 To 73, 23 To 38, 39 To 66
                        Dim result As Item =
                                modPlayer.InventoryZeroItems.Find(
                                    Function(item) item.Slot = itm.Slot AndAlso item.Guid = itm.Guid)
                        If result Is Nothing Then
                            DeleteItem(modPlayer, itm, True)
                        End If
                End Select
            Next
            '// Any new items?
            For Each itm As Item In modPlayer.InventoryZeroItems
                Select Case itm.Slot
                    Case 19 To 22, 67 To 73, 23 To 38, 39 To 66
                        Dim result As Item =
                                comparePlayer.InventoryZeroItems.Find(
                                    Function(item) item.Slot = itm.Slot AndAlso item.Guid = itm.Guid)
                        If result Is Nothing Then
                            CreateItem(modPlayer, itm, True)
                        ElseIf result.Count <> itm.Count Or result.Guid <> itm.Guid Then
                            UpdateItem(modPlayer, itm)
                        End If
                End Select
            Next
            '// Any deleted items?
            For Each itm As Item In comparePlayer.InventoryItems
                Dim result As Item =
                        modPlayer.InventoryItems.Find(
                            Function(item) _
                                                         item.Slot = itm.Slot AndAlso item.Bagguid = itm.Bagguid AndAlso
                                                         item.Id = itm.Id)
                If result Is Nothing Then
                    DeleteItem(modPlayer, itm, False)
                End If
            Next
            '// Any new items?
            For Each itm As Item In modPlayer.InventoryItems
                Dim result As Item =
                        comparePlayer.InventoryItems.Find(
                            Function(item) _
                                                             item.Slot = itm.Slot AndAlso item.Bagguid = itm.Bagguid AndAlso
                                                             item.Id = itm.Id)
                If result Is Nothing Then
                    CreateItem(modPlayer, itm, False)
                ElseIf result.Count <> itm.Count Or result.Guid <> itm.Guid Then
                    UpdateItem(modPlayer, itm)
                End If
            Next
        End Sub

        Private Sub CreateItem(player As Character, itm2Add As Item, zero As Boolean)
            Select Case GlobalVariables.sourceCore
                Case Modules.Core.ARCEMU
                    Dim newItemGuid As String =
                            ((TryInt(
                                runSQLCommand_characters_string(
                                    "SELECT " & GlobalVariables.targetStructure.itmins_guid_col(0) & " FROM " &
                                    GlobalVariables.targetStructure.item_instance_tbl(0) & " WHERE " &
                                    GlobalVariables.targetStructure.itmins_guid_col(0) &
                                    "=(SELECT MAX(" & GlobalVariables.targetStructure.itmins_guid_col(0) & ") FROM " &
                                    GlobalVariables.targetStructure.item_instance_tbl(0) & ")")) + 1)).ToString
                    runSQLCommand_characters_string(
                        "DELETE FROM `" & GlobalVariables.targetStructure.item_instance_tbl(0) &
                        "` WHERE `" & GlobalVariables.targetStructure.itmins_ownerGuid_col(0) & "`='" &
                        player.Guid.ToString() &
                        "' AND `" & GlobalVariables.targetStructure.itmins_slot_col(0) & "`='" & itm2Add.Slot.ToString() &
                        " AND `" & GlobalVariables.targetStructure.itmins_container_col(0) & "`='" &
                        itm2Add.Container.ToString())
                    runSQLCommand_characters_string(
                        "INSERT INTO " & GlobalVariables.targetStructure.item_instance_tbl(0) & " ( " &
                        GlobalVariables.targetStructure.itmins_guid_col(0) & ", " &
                        GlobalVariables.targetStructure.itmins_ownerGuid_col(0) & ", " &
                        GlobalVariables.targetStructure.itmins_itemEntry_col(0) & ", " &
                        GlobalVariables.targetStructure.itmins_count_col(0) & ", " &
                        GlobalVariables.targetStructure.itmins_container_col(0) & ", " &
                        GlobalVariables.targetStructure.itmins_slot_col(0) &
                        " ) VALUES ( '" &
                        newItemGuid & "', '" &
                        player.Guid.ToString() & "', '" &
                        itm2Add.Id.ToString() & "', '" &
                        itm2Add.Count.ToString() & "', '" &
                        itm2Add.Container.ToString() & "', '" &
                        itm2Add.Slot.ToString() & "' )")
                    Dim mEnchCreator As New EnchantmentsCreation
                    mEnchCreator.SetItemEnchantments(Nothing, itm2Add, TryInt(newItemGuid), GlobalVariables.targetCore,
                                                     GlobalVariables.sourceStructure)
                Case Modules.Core.TRINITY
                    Dim newItemGuid As Integer = TryInt(
                        runSQLCommand_characters_string(
                            "SELECT " & GlobalVariables.sourceStructure.itmins_guid_col(0) & " FROM " &
                            GlobalVariables.sourceStructure.item_instance_tbl(0) & " WHERE " &
                            GlobalVariables.sourceStructure.itmins_guid_col(0) & "=(SELECT MAX(" &
                            GlobalVariables.sourceStructure.itmins_guid_col(0) & ") FROM " &
                            GlobalVariables.sourceStructure.item_instance_tbl(0) & ")")) + 1
                    runSQLCommand_characters_string(
                        "INSERT INTO " & GlobalVariables.sourceStructure.item_instance_tbl(0) & " ( " &
                        GlobalVariables.sourceStructure.itmins_guid_col(0) & ", " &
                        GlobalVariables.sourceStructure.itmins_itemEntry_col(0) & ", " &
                        GlobalVariables.sourceStructure.itmins_ownerGuid_col(0) & ", " &
                        GlobalVariables.sourceStructure.itmins_count_col(0) & ", " &
                        GlobalVariables.sourceStructure.itmins_enchantments_col(0) &
                        ", " & GlobalVariables.sourceStructure.itmins_durability_col(0) & " ) VALUES ( '" &
                        newItemGuid.ToString() & "', '" & itm2Add.Id.ToString & "', '" & player.Guid.ToString() &
                        "', '" & itm2Add.Count &
                        "', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )")
                    If _
                        ReturnResultCount(
                            "SELECT * FROM " & GlobalVariables.sourceStructure.character_inventory_tbl(0) & " WHERE " &
                            GlobalVariables.sourceStructure.invent_guid_col(0) & "='" & player.Guid.ToString() &
                            "' AND " &
                            GlobalVariables.sourceStructure.invent_slot_col(0) & " = '" & itm2Add.Slot.ToString() & "'") >
                        0 _
                        Then
                        '// Item in this slot already exists: Deleting it
                        runSQLCommand_characters_string(
                            "DELETE FROM " & GlobalVariables.sourceStructure.character_inventory_tbl(0) & " WHERE " &
                            GlobalVariables.sourceStructure.invent_guid_col(0) & " = '" & player.Guid.ToString() &
                            "' AND " & GlobalVariables.sourceStructure.invent_slot_col(0) & " = '" &
                            itm2Add.Slot.ToString() &
                            "' AND " & GlobalVariables.sourceStructure.invent_bag_col(0) & " = '" &
                            itm2Add.Bagguid.ToString() &
                            "'")
                    End If
                    Select Case zero
                        Case True
                            runSQLCommand_characters_string(
                                "INSERT INTO " & GlobalVariables.sourceStructure.character_inventory_tbl(0) & " ( " &
                                GlobalVariables.sourceStructure.invent_guid_col(0) & ", " &
                                GlobalVariables.sourceStructure.invent_slot_col(0) & ", " &
                                GlobalVariables.sourceStructure.invent_bag_col(0) & ", " &
                                GlobalVariables.sourceStructure.invent_item_col(0) & " ) VALUES ( '" &
                                player.Guid.ToString() & "', '" & itm2Add.Slot.ToString() & "', '0', '" &
                                newItemGuid.ToString() & "' )")
                            If itm2Add.BagItems IsNot Nothing AndAlso itm2Add.AddedBag = True Then
                                For Each bagItem As Item In itm2Add.BagItems
                                    bagItem.Bagguid = newItemGuid
                                    CreateItem(player, bagItem, False)
                                Next
                            End If
                        Case False
                            runSQLCommand_characters_string(
                                "INSERT INTO " & GlobalVariables.sourceStructure.character_inventory_tbl(0) & " ( " &
                                GlobalVariables.sourceStructure.invent_guid_col(0) & ", " &
                                GlobalVariables.sourceStructure.invent_slot_col(0) & ", " &
                                GlobalVariables.sourceStructure.invent_bag_col(0) & ", " &
                                GlobalVariables.sourceStructure.invent_item_col(0) & " ) VALUES ( '" &
                                player.Guid.ToString() & "', '" & itm2Add.Slot.ToString() & "', '" &
                                itm2Add.Bagguid.ToString & "', '" & newItemGuid.ToString() & "' )")
                    End Select
                    Dim mEnchCreator As New EnchantmentsCreation
                    mEnchCreator.SetItemEnchantments(Nothing, itm2Add, newItemGuid, GlobalVariables.targetCore,
                                                     GlobalVariables.sourceStructure)
                    '// Optional TODO: Set equipment cache
                Case Modules.Core.MANGOS
                    Const enchString =
                              "0 1191182336 3 0 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 3753 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 100 100 0 0 "
                    Dim newItemGuid As Integer =
                            ((TryInt(
                                runSQLCommand_characters_string(
                                    "SELECT " & GlobalVariables.targetStructure.itmins_guid_col(0) & " FROM " &
                                    GlobalVariables.targetStructure.item_instance_tbl(0) & " WHERE " &
                                    GlobalVariables.targetStructure.itmins_guid_col(0) &
                                    "=(SELECT MAX(" & GlobalVariables.targetStructure.itmins_guid_col(0) & ") FROM " &
                                    GlobalVariables.targetStructure.item_instance_tbl(0) & ")")) + 1))

                    Dim parts() As String = enchString.Split(" "c)
                    parts(0) = newItemGuid.ToString()
                    parts(3) = itm2Add.Id.ToString()
                    parts(14) = "1"
                    Dim myEnchString As String = String.Join(" ", parts)
                    runSQLCommand_characters_string(
                        "INSERT INTO " & GlobalVariables.targetStructure.item_instance_tbl(0) & " ( " &
                        GlobalVariables.targetStructure.itmins_guid_col(0) & ", " &
                        GlobalVariables.targetStructure.itmins_ownerGuid_col(0) & ", " &
                        GlobalVariables.targetStructure.itmins_data_col(0) & " ) VALUES ( '" &
                        newItemGuid.ToString() & "', '" &
                        player.Guid.ToString() & "', '" &
                        myEnchString & "' )")
                    If _
                        ReturnResultCount(
                            "SELECT * FROM " & GlobalVariables.sourceStructure.character_inventory_tbl(0) & " WHERE " &
                            GlobalVariables.sourceStructure.invent_guid_col(0) & "='" & player.Guid.ToString() &
                            "' AND " &
                            GlobalVariables.sourceStructure.invent_slot_col(0) & " = '" & itm2Add.Slot.ToString() & "'") >
                        0 _
                        Then
                        '// Item in this slot already exists: Deleting it
                        runSQLCommand_characters_string(
                            "DELETE FROM " & GlobalVariables.sourceStructure.character_inventory_tbl(0) & " WHERE " &
                            GlobalVariables.sourceStructure.invent_guid_col(0) & " = '" & player.Guid.ToString() &
                            "' AND " & GlobalVariables.sourceStructure.invent_slot_col(0) & " = '" &
                            itm2Add.Slot.ToString() &
                            "' AND " & GlobalVariables.sourceStructure.invent_bag_col(0) & " = '" &
                            itm2Add.Bagguid.ToString() &
                            "'")
                    End If
                    runSQLCommand_characters_string(
                        "INSERT INTO " & GlobalVariables.targetStructure.character_inventory_tbl(0) & " ( " &
                        GlobalVariables.targetStructure.invent_guid_col(0) & ", " &
                        GlobalVariables.targetStructure.invent_bag_col(0) & ", `" &
                        GlobalVariables.targetStructure.invent_slot_col(0) & "`, `" &
                        GlobalVariables.targetStructure.invent_item_col(0) & "`, `" &
                        GlobalVariables.targetStructure.invent_item_template_col(0) & "` ) VALUES ( '" &
                        player.Guid.ToString() & "', '" &
                        itm2Add.Bagguid & "', '" &
                        itm2Add.Slot.ToString() & "', '" &
                        newItemGuid.ToString() & "', '" &
                        itm2Add.Id.ToString() & "')")
                    Dim mEnchCreator As New EnchantmentsCreation
                    mEnchCreator.SetItemEnchantments(Nothing, itm2Add, newItemGuid, GlobalVariables.targetCore,
                                                     GlobalVariables.sourceStructure)
                    '// Optional TODO: Set equipment cache
            End Select
        End Sub

        Private Sub DeleteItem(player As Character, itm2Delete As Item, zero As Boolean)
            Select Case GlobalVariables.sourceCore
                Case Modules.Core.ARCEMU
                    runSQLCommand_characters_string(
                        "DELETE FROM `" & GlobalVariables.targetStructure.item_instance_tbl(0) &
                        "` WHERE `" & GlobalVariables.targetStructure.itmins_ownerGuid_col(0) & "`='" &
                        player.Guid.ToString() &
                        "' AND `" & GlobalVariables.targetStructure.itmins_slot_col(0) & "`='" &
                        itm2Delete.Slot.ToString() &
                        " AND `" & GlobalVariables.targetStructure.itmins_container_col(0) & "`='" &
                        itm2Delete.Container.ToString() & "'")
                Case Modules.Core.TRINITY
                    Select Case zero
                        Case True
                            runSQLCommand_characters_string(
                                "DELETE FROM " & GlobalVariables.sourceStructure.character_inventory_tbl(0) & " WHERE " &
                                GlobalVariables.sourceStructure.invent_guid_col(0) & " = '" & player.Guid.ToString() &
                                "' AND " & GlobalVariables.sourceStructure.invent_slot_col(0) & " = '" &
                                itm2Delete.Slot.ToString() &
                                "' AND " & GlobalVariables.sourceStructure.invent_bag_col(0) & " = '0'")
                        Case False
                            runSQLCommand_characters_string(
                                "DELETE FROM " & GlobalVariables.sourceStructure.character_inventory_tbl(0) & " WHERE " &
                                GlobalVariables.sourceStructure.invent_guid_col(0) & " = '" & player.Guid.ToString() &
                                "' AND " & GlobalVariables.sourceStructure.invent_slot_col(0) & " = '" &
                                itm2Delete.Slot.ToString() &
                                "' AND " & GlobalVariables.sourceStructure.invent_bag_col(0) & " = '" &
                                itm2Delete.Bagguid.ToString & "'")
                    End Select
                    runSQLCommand_characters_string(
                        "DELETE FROM " & GlobalVariables.sourceStructure.item_instance_tbl(0) & " WHERE " &
                        GlobalVariables.sourceStructure.itmins_ownerGuid_col(0) & " = '" & player.Guid.ToString() &
                        "' AND " & GlobalVariables.sourceStructure.itmins_guid_col(0) & " = '" &
                        itm2Delete.Guid.ToString() &
                        "'")
                Case Modules.Core.MANGOS
                    Select Case zero
                        Case True
                            runSQLCommand_characters_string(
                                "DELETE FROM " & GlobalVariables.sourceStructure.character_inventory_tbl(0) & " WHERE " &
                                GlobalVariables.sourceStructure.invent_guid_col(0) & " = '" & player.Guid.ToString() &
                                "' AND " & GlobalVariables.sourceStructure.invent_slot_col(0) & " = '" &
                                itm2Delete.Slot.ToString() &
                                "' AND " & GlobalVariables.sourceStructure.invent_bag_col(0) & " = '0'")
                        Case False
                            runSQLCommand_characters_string(
                                "DELETE FROM " & GlobalVariables.sourceStructure.character_inventory_tbl(0) & " WHERE " &
                                GlobalVariables.sourceStructure.invent_guid_col(0) & " = '" & player.Guid.ToString() &
                                "' AND " & GlobalVariables.sourceStructure.invent_slot_col(0) & " = '" &
                                itm2Delete.Slot.ToString() &
                                "' AND " & GlobalVariables.sourceStructure.invent_bag_col(0) & " = '" &
                                itm2Delete.Bagguid.ToString & "'")
                    End Select
                    runSQLCommand_characters_string(
                        "DELETE FROM " & GlobalVariables.sourceStructure.item_instance_tbl(0) & " WHERE " &
                        GlobalVariables.sourceStructure.itmins_ownerGuid_col(0) & " = '" & player.Guid.ToString() &
                        "' AND " & GlobalVariables.sourceStructure.itmins_guid_col(0) & " = '" &
                        itm2Delete.Guid.ToString() &
                        "'")
            End Select
        End Sub

        Private Sub UpdateItem(player As Character, itm2Update As Item)
            Select Case GlobalVariables.sourceCore
                Case Modules.Core.ARCEMU
                    runSQLCommand_characters_string(
                        "UPDATE " & GlobalVariables.sourceStructure.item_instance_tbl(0) & " SET `" &
                        GlobalVariables.sourceStructure.itmins_count_col(0) & "`='" & itm2Update.Count.ToString() &
                        "', `" &
                        GlobalVariables.sourceStructure.itmins_container_col(0) & "`='" &
                        itm2Update.Container.ToString() & "', `" &
                        GlobalVariables.sourceStructure.itmins_slot_col(0) & "`='" & itm2Update.Slot.ToString() &
                        "' WHERE `" &
                        GlobalVariables.sourceStructure.itmins_ownerGuid_col(0) & "`='" & player.Guid.ToString() &
                        "' AND `" &
                        GlobalVariables.sourceStructure.itmins_guid_col(0) & "`='" & itm2Update.Guid.ToString() & "'")
                    Dim mEnchHandler As New EnchantmentsCreation
                    mEnchHandler.SetItemEnchantments(player, itm2Update, itm2Update.Guid,
                                                     GlobalVariables.sourceCore, GlobalVariables.sourceStructure)
                Case Modules.Core.TRINITY
                    runSQLCommand_characters_string("UPDATE `" & GlobalVariables.sourceStructure.item_instance_tbl(0) &
                                                    "` SET " & GlobalVariables.sourceStructure.itmins_count_col(0) &
                                                    "='" &
                                                    itm2Update.Count.ToString & "'" &
                                                    " WHERE " & GlobalVariables.sourceStructure.itmins_ownerGuid_col(0) &
                                                    " = '" & player.Guid.ToString() &
                                                    "' AND " & GlobalVariables.sourceStructure.itmins_guid_col(0) &
                                                    " = '" & itm2Update.Guid.ToString() & "'")
                    Dim mEnchHandler As New EnchantmentsCreation
                    mEnchHandler.SetItemEnchantments(player, itm2Update, itm2Update.Guid,
                                                     GlobalVariables.sourceCore, GlobalVariables.sourceStructure)
                Case Modules.Core.MANGOS
                    Dim mEnchHandler As New EnchantmentsCreation
                    mEnchHandler.SetItemEnchantments(player, itm2Update, itm2Update.Guid,
                                                     GlobalVariables.sourceCore, GlobalVariables.sourceStructure)
            End Select
        End Sub
    End Class
End Namespace