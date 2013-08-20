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
'*      /Filename:      CharacterInventoryHandler
'*      /Description:   Contains functions for extracting information about the items in 
'*                      the inventory of a specific character
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Imports Namcore_Studio_Framework.EventLogging
Imports Namcore_Studio_Framework.Basics
Imports Namcore_Studio_Framework.GlobalVariables
Imports Namcore_Studio_Framework.CommandHandler
Imports Namcore_Studio_Framework.Conversions
Public Class CharacterInventoryHandler
    Public Sub GetCharacterInventory(ByVal characterGuid As Integer, ByVal setId As Integer, ByVal accountId As Integer)
        LogAppend("Loading character Inventory for characterGuid: " & characterGuid & " and setId: " & setId, "CharacterInventoryHandler_GetCharacterInventory", True)
        Select Case sourceCore
            Case "arcemu"
                loadAtArcemu(characterGuid, setId, accountId)
            Case "trinity"
                loadAtTrinity(characterGuid, setId, accountId)
            Case "trinitytbc"
                loadAtTrinityTBC(characterGuid, setId, accountId)
            Case "mangos"
                loadAtMangos(characterGuid, setId, accountId)
            Case Else

        End Select

    End Sub
    Private Sub loadAtArcemu(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)
        LogAppend("Loading character Inventory @loadAtArcemu", "CharacterInventoryHandler_loadAtArcemu", False)
        Dim dt As DataTable = ReturnDataTable("SELECT " & sourceStructure.itmins_slot_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " & sourceStructure.itmins_guid_col(0) & "='" & charguid.ToString() & "'")
        Dim templistzero As New List(Of String)
        Dim templist As New List(Of String)
        Dim tmpext As Integer
        Dim player As Character = GetCharacterSetBySetId(tar_setId)
        Dim slotlist As String = ""
        Try
            Dim lastcount As Integer = TryInt(dt.Rows.Count.ToString)
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim readedcode As String = (dt.Rows(count).Item(0)).ToString
                    If Not slotlist.Contains("#" & readedcode & "#") Then
                        slotlist = slotlist & "#" & readedcode & "#"
                        tmpext = TryInt(readedcode)
                        Dim numresults As Integer = ReturnCountResults("SELECT " & sourceStructure.itmins_container_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " &
                                                                       sourceStructure.itmins_ownerGuid_col(0) & "='" & charguid.ToString & "' AND " & sourceStructure.itmins_slot_col(0) & "='" & tmpext.ToString & "'")
                        If numresults = 1 Then
                            Dim containerslot As String = runSQLCommand_characters_string("SELECT " & sourceStructure.itmins_container_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " &
                                                                                          sourceStructure.itmins_ownerGuid_col(0) & "='" & charguid.ToString & "' AND " & sourceStructure.itmins_slot_col(0) &
                                                                                          "='" & tmpext.ToString & "'")
                            Dim bagguid As String = "-1"
                            If Not containerslot = "-1" Then
                                bagguid = runSQLCommand_characters_string("SELECT " & sourceStructure.itmins_guid_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " & sourceStructure.itmins_ownerGuid_col(0) &
                                                                          "='" & charguid.ToString & "' AND " & sourceStructure.itmins_slot_col(0) & "='" & containerslot & "' AND " & sourceStructure.itmins_container_col(0) &
                                                                          "='-1'")
                            End If
                            If bagguid = "-1" Then
                                If tmpext > 18 Then
                                    Dim bag As String = "0"
                                    Dim item As String = "0"
                                    Dim entryid As String
                                    Dim enchantments As String
                                    Dim itemcount As String = "1"

                                    bag = bagguid
                                    item = runSQLCommand_characters_string("SELECT " & sourceStructure.itmins_guid_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " & sourceStructure.itmins_ownerGuid_col(0) &
                                                                           "='" & charguid.ToString & "' AND " & sourceStructure.itmins_slot_col(0) & "='" & tmpext.ToString & "' AND " & sourceStructure.itmins_container_col(0) &
                                                                           "='-1'")
                                    entryid = runSQLCommand_characters_string("SELECT " & sourceStructure.itmins_itemEntry_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " &
                                                                              sourceStructure.itmins_guid_col(0) & " = '" & item & "'")
                                    enchantments = runSQLCommand_characters_string("SELECT " & sourceStructure.itmins_enchantments_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " &
                                                                                   sourceStructure.itmins_guid_col(0) & "='" & item & "'")
                                    itemcount = runSQLCommand_characters_string("SELECT " & sourceStructure.itmins_count_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " &
                                                                                sourceStructure.itmins_guid_col(0) & "='" & item & "'")
                                    Dim newItm As New InventItem
                                    newItm.slot = TryInt(tmpext.ToString)
                                    newItm.bag = TryInt(bag)
                                    newItm.bagguid = TryInt(bagguid)
                                    newItm.entry = TryInt(entryid)
                                    newItm.enchantstring = enchantments
                                    newItm.count = TryInt(itemcount)
                                    newItm.container = -1
                                    newItm.guid = TryInt(item)
                                    player.InventoryZeroItems.Add(newItm)
                                    SetCharacterSet(tar_setId, player)
                                End If
                            Else
                                Dim bag As String = "0"
                                Dim item As String = "0"
                                Dim entryid As String
                                Dim enchantments As String
                                Dim itemcount As String = "1"
                                bag = runSQLCommand_characters_string("SELECT " & sourceStructure.itmins_itemEntry_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " & sourceStructure.itmins_guid_col(0) &
                                                                      " = '" & bagguid & "'")
                                item = runSQLCommand_characters_string("SELECT " & sourceStructure.itmins_guid_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " & sourceStructure.itmins_ownerGuid_col(0) &
                                                                       "='" & charguid.ToString & "' AND " & sourceStructure.itmins_slot_col(0) & "='" & tmpext.ToString & "'")
                                entryid = runSQLCommand_characters_string("SELECT " & sourceStructure.itmins_itemEntry_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " & sourceStructure.itmins_guid_col(0) &
                                                                          " = '" & item & "'")
                                enchantments = runSQLCommand_characters_string("SELECT " & sourceStructure.itmins_enchantments_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " &
                                                                               sourceStructure.itmins_guid_col(0) & "='" & item & "'")
                                itemcount = runSQLCommand_characters_string("SELECT " & sourceStructure.itmins_count_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " & sourceStructure.itmins_guid_col(0) &
                                                                            "='" & item & "'")
                                Dim newItm As New InventItem
                                newItm.slot = TryInt(tmpext.ToString)
                                newItm.bag = TryInt(bag)
                                newItm.bagguid = TryInt(bagguid)
                                newItm.entry = TryInt(entryid)
                                newItm.enchantstring = enchantments
                                newItm.count = TryInt(itemcount)
                                newItm.container = -1
                                newItm.guid = TryInt(item)
                                player.InventoryItems.Add(newItm)
                                SetCharacterSet(tar_setId, player)
                            End If
                        Else
                            Dim containerslot As String = ReturnResultWithRow("SELECT " & sourceStructure.itmins_container_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " &
                                                                              sourceStructure.itmins_ownerGuid_col(0) & "='" & charguid.ToString & "' AND " & sourceStructure.itmins_slot_col(0) & "='" & tmpext.ToString &
                                                                              "'", sourceStructure.itmins_container_col(0), 0)
                            Dim bagguid As String = "-1"
                            If Not containerslot = "-1" Then
                                bagguid = runSQLCommand_characters_string("SELECT " & sourceStructure.itmins_guid_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " & sourceStructure.itmins_ownerGuid_col(0) &
                                                                          "='" & charguid.ToString & "' AND " & sourceStructure.itmins_slot_col(0) & "='" & containerslot & "' AND " & sourceStructure.itmins_container_col(0) &
                                                                          "='-1'")
                            End If
                            If bagguid = "-1" Then
                                If tmpext > 18 Then
                                    Dim bag As String = "0"
                                    Dim item As String = "0"
                                    Dim entryid As String
                                    Dim enchantments As String
                                    Dim itemcount As String = "1"
                                    bag = bagguid
                                    item = runSQLCommand_characters_string("SELECT " & sourceStructure.itmins_guid_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " & sourceStructure.itmins_ownerGuid_col(0) &
                                                                           "='" & charguid.ToString & "' AND " & sourceStructure.itmins_slot_col(0) & "='" & tmpext.ToString & "' AND " & sourceStructure.itmins_container_col(0) &
                                                                           "='-1'")
                                    entryid = runSQLCommand_characters_string("SELECT " & sourceStructure.itmins_itemEntry_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " &
                                                                              sourceStructure.itmins_guid_col(0) & " = '" & item & "'")
                                    enchantments = runSQLCommand_characters_string("SELECT " & sourceStructure.itmins_enchantments_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " &
                                                                                   sourceStructure.itmins_guid_col(0) & "='" & item & "'")
                                    itemcount = runSQLCommand_characters_string("SELECT " & sourceStructure.itmins_count_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " & sourceStructure.itmins_guid_col(0) &
                                                                                "='" & item & "'")
                                    Dim newItm As New InventItem
                                    newItm.slot = TryInt(tmpext.ToString)
                                    newItm.bag = TryInt(bag)
                                    newItm.bagguid = TryInt(bagguid)
                                    newItm.entry = TryInt(entryid)
                                    newItm.enchantstring = enchantments
                                    newItm.count = TryInt(itemcount)
                                    newItm.container = -1
                                    newItm.guid = TryInt(item)
                                    player.InventoryZeroItems.Add(newItm)
                                    SetCharacterSet(tar_setId, player)
                                End If
                            Else
                                Dim bag As String = "0"
                                Dim item As String = "0"
                                Dim entryid As String
                                Dim enchantments As String
                                Dim itemcount As String = "1"
                                bag = runSQLCommand_characters_string("SELECT " & sourceStructure.itmins_itemEntry_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " & sourceStructure.itmins_guid_col(0) &
                                                                      " = '" & bagguid & "'")
                                item = ReturnResultWithRow("SELECT " & sourceStructure.itmins_guid_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " & sourceStructure.itmins_ownerGuid_col(0) &
                                                           "='" & charguid.ToString & "' AND " & sourceStructure.itmins_slot_col(0) & "='" & tmpext.ToString & "'", sourceStructure.itmins_guid_col(0), 1)
                                entryid = runSQLCommand_characters_string("SELECT " & sourceStructure.itmins_itemEntry_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " & sourceStructure.itmins_guid_col(0) &
                                                                          " = '" & item & "'")
                                enchantments = runSQLCommand_characters_string("SELECT " & sourceStructure.itmins_enchantments_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " &
                                                                               sourceStructure.itmins_guid_col(0) & "='" & item & "'")
                                itemcount = runSQLCommand_characters_string("SELECT " & sourceStructure.itmins_count_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " & sourceStructure.itmins_guid_col(0) &
                                                                            "='" & item & "'")
                                Dim newItm As New InventItem
                                newItm.slot = TryInt(tmpext.ToString)
                                newItm.bag = TryInt(bag)
                                newItm.bagguid = TryInt(bagguid)
                                newItm.entry = TryInt(entryid)
                                newItm.enchantstring = enchantments
                                newItm.count = TryInt(itemcount)
                                newItm.container = -1
                                newItm.guid = TryInt(item)
                                player.InventoryItems.Add(newItm)
                                SetCharacterSet(tar_setId, player)
                            End If
                            Dim containerslot2 As String = ReturnResultWithRow("SELECT " & sourceStructure.itmins_container_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " &
                                                                               sourceStructure.itmins_ownerGuid_col(0) & "='" & charguid.ToString & "' AND " & sourceStructure.itmins_slot_col(0) & "='" & tmpext.ToString &
                                                                               "'", sourceStructure.itmins_container_col(0), 1)
                            Dim bagguid2 As String = "-1"
                            If Not containerslot2 = "-1" Then
                                bagguid2 = runSQLCommand_characters_string("SELECT " & sourceStructure.itmins_guid_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " & sourceStructure.itmins_ownerGuid_col(0) &
                                                                           "='" & charguid.ToString & "' AND " & sourceStructure.itmins_slot_col(0) & "='" & containerslot2 & "' AND " & sourceStructure.itmins_container_col(0) &
                                                                           "='-1'")
                            End If
                            If bagguid2 = "-1" Then
                                If tmpext > 18 Then
                                    Dim bag As String = "0"
                                    Dim item As String = "0"
                                    Dim entryid As String
                                    Dim enchantments As String
                                    Dim itemcount As String = "1"
                                    bag = bagguid2
                                    item = ReturnResultWithRow("SELECT " & sourceStructure.itmins_guid_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " & sourceStructure.itmins_ownerGuid_col(0) & "='" &
                                                               charguid.ToString & "' AND " & sourceStructure.itmins_slot_col(0) & "='" & tmpext.ToString & "' AND " & sourceStructure.itmins_container_col(0) &
                                                               "='-1'", sourceStructure.itmins_guid_col(0), 1)
                                    entryid = runSQLCommand_characters_string("SELECT " & sourceStructure.itmins_itemEntry_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " &
                                                                              sourceStructure.itmins_guid_col(0) & " = '" & item & "'")
                                    enchantments = runSQLCommand_characters_string("SELECT " & sourceStructure.itmins_enchantments_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " &
                                                                                   sourceStructure.itmins_guid_col(0) & "='" & item & "'")
                                    itemcount = runSQLCommand_characters_string("SELECT " & sourceStructure.itmins_count_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " &
                                                                                sourceStructure.itmins_guid_col(0) & "='" & item & "'")
                                    Dim newItm As New InventItem
                                    newItm.slot = TryInt(tmpext.ToString)
                                    newItm.bag = TryInt(bag)
                                    newItm.bagguid = TryInt(bagguid)
                                    newItm.entry = TryInt(entryid)
                                    newItm.enchantstring = enchantments
                                    newItm.count = TryInt(itemcount)
                                    newItm.container = -1
                                    newItm.guid = TryInt(item)
                                    player.InventoryZeroItems.Add(newItm)
                                    SetCharacterSet(tar_setId, player)
                                End If
                            Else
                                Dim bag As String = "0"
                                Dim item As String = "0"
                                Dim entryid As String
                                Dim enchantments As String
                                Dim itemcount As String = "1"
                                bag = runSQLCommand_characters_string("SELECT " & sourceStructure.itmins_itemEntry_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " & sourceStructure.itmins_guid_col(0) &
                                                                      " = '" & bagguid2 & "'")
                                item = ReturnResultWithRow("SELECT " & sourceStructure.itmins_guid_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " & sourceStructure.itmins_ownerGuid_col(0) & "='" &
                                                           charguid.ToString & "' AND " & sourceStructure.itmins_slot_col(0) & "='" & tmpext.ToString & "'", sourceStructure.itmins_guid_col(0), 1)
                                entryid = runSQLCommand_characters_string("SELECT " & sourceStructure.itmins_itemEntry_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " & sourceStructure.itmins_guid_col(0) &
                                                                          " = '" & item & "'")
                                enchantments = runSQLCommand_characters_string("SELECT " & sourceStructure.itmins_enchantments_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " &
                                                                               sourceStructure.itmins_guid_col(0) & "='" & item & "'")
                                itemcount = runSQLCommand_characters_string("SELECT " & sourceStructure.itmins_count_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " & sourceStructure.itmins_guid_col(0) &
                                                                            "='" & item & "'")
                                Dim newItm As New InventItem
                                newItm.slot = TryInt(tmpext.ToString)
                                newItm.bag = TryInt(bag)
                                newItm.bagguid = TryInt(bagguid)
                                newItm.entry = TryInt(entryid)
                                newItm.enchantstring = enchantments
                                newItm.count = TryInt(itemcount)
                                newItm.container = TryInt(containerslot2)
                                newItm.guid = TryInt(item)
                                player.InventoryItems.Add(newItm)
                                SetCharacterSet(tar_setId, player)
                            End If
                        End If
                        count += 1
                    Else
                        count += 1
                    End If
                Loop Until count = lastcount
            End If
        Catch ex As Exception
            LogAppend("Something went wrong while loading character Inventory! -> skipping -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterInventoryHandler_loadAtArcemu", True, True)
            Exit Sub
        End Try

    End Sub
    Private Sub loadAtTrinity(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)
        LogAppend("Loading character Inventory @loadAtTrinity", "CharacterInventoryHandler_loadAtTrinity", False)
        Dim dt As DataTable = ReturnDataTable("SELECT " & sourceStructure.invent_item_col(0) & " FROM " & sourceStructure.character_inventory_tbl(0) & " WHERE " & sourceStructure.invent_guid_col(0) & "='" & charguid.ToString() & "'")
        Dim templist As New List(Of String)
        Dim templistzero As New List(Of String)
        Dim tmpext As Integer
        Dim player As Character = GetCharacterSetBySetId(tar_setId)
        Try
            Dim lastcount As Integer = dt.Rows.Count
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim readedcode As String = (dt.Rows(count).Item(0)).ToString
                    tmpext = TryInt(readedcode)
                    Dim bagguid As String = runSQLCommand_characters_string("SELECT " & sourceStructure.invent_bag_col(0) & " FROM " & sourceStructure.character_inventory_tbl(0) & " WHERE " & sourceStructure.invent_guid_col(0) & "='" & charguid.ToString & "' AND " & sourceStructure.invent_item_col(0) & "='" & tmpext.ToString & "'")
                    If TryInt(bagguid) = 0 Then
                        If tmpext > 18 Then
                            Dim bag As String = "0"
                            Dim item As String = "0"
                            Dim entryid As String
                            Dim enchantments As String
                            Dim itemcount As String = "1"
                            Dim slot As String = "0"
                            bag = bagguid
                            item = tmpext.ToString()
                            entryid = runSQLCommand_characters_string("SELECT " & sourceStructure.itmins_itemEntry_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " & sourceStructure.itmins_guid_col(0) & " = '" & item & "'")
                            enchantments = runSQLCommand_characters_string("SELECT " & sourceStructure.itmins_enchantments_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " & sourceStructure.itmins_guid_col(0) & " = '" & item & "'")
                            itemcount = runSQLCommand_characters_string("Select `" & sourceStructure.itmins_count_col(0) & "` FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " & sourceStructure.itmins_guid_col(0) & "='" & item & "'")
                            slot = runSQLCommand_characters_string("Select `" & sourceStructure.invent_slot_col(0) & "` FROM " & sourceStructure.character_inventory_tbl(0) & " WHERE `" & sourceStructure.invent_item_col(0) & "`='" & item & "'")
                            Dim newItm As New InventItem
                            newItm.slot = TryInt(slot)
                            newItm.bag = TryInt(bag)
                            newItm.bagguid = TryInt(bagguid)
                            newItm.entry = TryInt(entryid)
                            newItm.enchantstring = enchantments
                            newItm.count = TryInt(itemcount)
                            newItm.guid = TryInt(item)
                            player.InventoryZeroItems.Add(newItm)
                        End If
                    Else
                        Dim bag As String = "0"
                        Dim item As String = "0"
                        Dim entryid As String
                        Dim enchantments As String
                        Dim itemcount As String = "1"
                        Dim slot As String = "0"
                        bag = runSQLCommand_characters_string("SELECT " & sourceStructure.itmins_itemEntry_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " & sourceStructure.itmins_guid_col(0) & " = '" & bagguid & "'")
                        item = tmpext.ToString
                        entryid = runSQLCommand_characters_string("SELECT " & sourceStructure.itmins_itemEntry_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " & sourceStructure.itmins_guid_col(0) & " = '" & item & "'")
                        enchantments = runSQLCommand_characters_string("SELECT " & sourceStructure.itmins_enchantments_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " & sourceStructure.itmins_guid_col(0) & " = '" & item & "'")
                        itemcount = runSQLCommand_characters_string("Select `" & sourceStructure.itmins_count_col(0) & "` FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " & sourceStructure.itmins_guid_col(0) & "='" & item & "'")
                        slot = runSQLCommand_characters_string("Select `" & sourceStructure.invent_slot_col(0) & "` FROM " & sourceStructure.character_inventory_tbl(0) & " WHERE `" & sourceStructure.invent_item_col(0) & "`='" & item & "'")
                        Dim newItm As New InventItem
                        newItm.slot = TryInt(slot)
                        newItm.bag = TryInt(bag)
                        newItm.bagguid = TryInt(bagguid)
                        newItm.entry = TryInt(entryid)
                        newItm.enchantstring = enchantments
                        newItm.count = TryInt(itemcount)
                        newItm.guid = TryInt(item)
                        player.InventoryItems.Add(newItm)
                    End If
                    count += 1
                Loop Until count = lastcount
            Else
                LogAppend("No Inventory found!", "CharacterInventoryHandler_loadAtTrinity", True)
            End If
        Catch ex As Exception
            LogAppend("Something went wrong while loading character Inventory! -> skipping -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterInventoryHandler_loadAtTrinity", True, True)
            Exit Sub
        End Try
        SetCharacterSet(tar_setId, player)
    End Sub
    Private Sub loadAtTrinityTBC(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)

    End Sub
    Private Sub loadAtMangos(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)
        LogAppend("Loading character Inventory @loadAtMangos", "CharacterInventoryHandler_loadAtMangos", False)
        Dim dt As DataTable = ReturnDataTable("SELECT " & sourceStructure.invent_item_col(0) & " FROM " & sourceStructure.character_inventory_tbl(0) & " WHERE " & sourceStructure.invent_guid_col(0) & "='" & charguid.ToString() & "'")
        Dim templist As New List(Of String)
        Dim templistzero As New List(Of String)
        Dim tmpext As Integer
        Dim player As Character = GetCharacterSetBySetId(tar_setId)
        Try
            Dim lastcount As Integer = TryInt(dt.Rows.Count.ToString)
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim readedcode As String = (dt.Rows(count).Item(0)).ToString
                    tmpext = TryInt(readedcode)
                    Dim bagguid As String = runSQLCommand_characters_string("SELECT " & sourceStructure.invent_bag_col(0) & " FROM " & sourceStructure.character_inventory_tbl(0) & " WHERE " & sourceStructure.invent_guid_col(0) & "='" & charguid.ToString & "' AND " & sourceStructure.invent_item_col(0) & "='" & tmpext.ToString & "'")
                    If tryint(bagguid) = 0 Then
                        If tmpext > 18 Then
                            Dim bag As String = "0"
                            Dim item As String = "0"
                            Dim entryid As String
                            Dim enchantments As String
                            Dim itemcount As String = "1"
                            Dim slot As String = "0"
                            bag = bagguid
                            item = tmpext.ToString()
                            entryid = runSQLCommand_characters_string("SELECT " & sourceStructure.invent_item_template_col(0) & " FROM " & sourceStructure.character_inventory_tbl(0) & " WHERE " & sourceStructure.invent_guid_col(0) & " = '" & charguid.ToString & "' AND " & sourceStructure.invent_item_col(0) & "='" & item & "'")
                            enchantments = runSQLCommand_characters_string("SELECT `" & sourceStructure.itmins_data_col(0) & "` FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " & sourceStructure.itmins_guid_col(0) & " = '" & item & "'")
                            itemcount = splititemdata(enchantments, 14)
                            slot = runSQLCommand_characters_string("Select `" & sourceStructure.invent_slot_col(0) & "` FROM " & sourceStructure.character_inventory_tbl(0) & " WHERE `" & sourceStructure.invent_item_col(0) & "`='" & item & "'")
                            Dim newItm As New InventItem
                            newItm.slot = TryInt(slot)
                            newItm.bag = TryInt(bag)
                            newItm.bagguid = TryInt(bagguid)
                            newItm.entry = TryInt(entryid)
                            newItm.enchantstring = enchantments
                            newItm.count = TryInt(itemcount)
                            newItm.guid = TryInt(item)
                            player.InventoryZeroItems.Add(newItm)
                        End If
                    Else
                        Dim bag As String = "0"
                        Dim item As String = "0"
                        Dim entryid As String
                        Dim enchantments As String
                        Dim itemcount As String = "1"
                        Dim slot As String = "0"
                        bag = runSQLCommand_characters_string("SELECT " & sourceStructure.invent_item_template_col(0) & " FROM " & sourceStructure.character_inventory_tbl(0) & " WHERE " & sourceStructure.invent_item_col(0) & " = '" & bagguid & "'")
                        item = tmpext.ToString
                        entryid = runSQLCommand_characters_string("SELECT " & sourceStructure.invent_item_template_col(0) & " FROM " & sourceStructure.character_inventory_tbl(0) & " WHERE " & sourceStructure.invent_guid_col(0) & " = '" & charguid.ToString & "' AND " & sourceStructure.invent_item_col(0) & "='" & tmpext.ToString & "'")
                        enchantments = runSQLCommand_characters_string("SELECT `" & sourceStructure.itmins_data_col(0) & "` FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " & sourceStructure.itmins_guid_col(0) & " = '" & item & "'")
                        itemcount = splititemdata(enchantments, 14)
                        slot = runSQLCommand_characters_string("Select `" & sourceStructure.invent_slot_col(0) & "` FROM " & sourceStructure.character_inventory_tbl(0) & " WHERE `" & sourceStructure.invent_item_col(0) & "`='" & item & "'")
                        Dim newItm As New InventItem
                        newItm.slot = TryInt(slot)
                        newItm.bag = TryInt(bag)
                        newItm.bagguid = TryInt(bagguid)
                        newItm.entry = TryInt(entryid)
                        newItm.enchantstring = enchantments
                        newItm.count = TryInt(itemcount)
                        newItm.guid = TryInt(item)
                        player.InventoryItems.Add(newItm)
                    End If
                    count += 1
                Loop Until count = lastcount
            Else
                LogAppend("No Inventory found!", "CharacterInventoryHandler_loadAtMangos", True)
            End If
        Catch ex As Exception
            LogAppend("Something went wrong while loading character Inventory! -> skipping -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterInventoryHandler_loadAtMangos", True, True)
            Exit Sub
        End Try
        SetCharacterSet(tar_setId, player)
    End Sub
    Private Function splititemdata(ByVal datastring As String, ByVal position As Integer) As String
        Try
            Dim parts() As String = datastring.Split(" "c)
            Return parts(position)
        Catch
            Return "1"
        End Try
    End Function
End Class
