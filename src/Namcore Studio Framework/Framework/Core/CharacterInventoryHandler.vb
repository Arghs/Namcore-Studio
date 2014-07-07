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
'*      /Filename:      CharacterInventoryHandler
'*      /Description:   Contains functions for extracting information about the items in 
'*                      the inventory of a specific character
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.Database
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Modules

Namespace Framework.Core
    Public Class CharacterInventoryHandler
        Public Sub GetCharacterInventory(ByVal characterGuid As Integer, ByVal setId As Integer,
                                         ByVal account As Account)
            LogAppend("Loading character Inventory for characterGuid: " & characterGuid & " and setId: " & setId,
                      "CharacterInventoryHandler_GetCharacterInventory", True)
            Dim player As Character = GetCharacterSetBySetId(setId, account)
            player.InventoryItems = New List(Of Item)()
            player.InventoryZeroItems = New List(Of Item)()
            SetCharacterSet(setId, player, account)
            Select Case GlobalVariables.sourceCore
                Case Modules.Core.ARCEMU
                    LoadAtArcemu(characterGuid, setId, account)
                Case Modules.Core.TRINITY
                    LoadAtTrinity(characterGuid, setId, account)
                Case Modules.Core.MANGOS
                    LoadAtMangos(characterGuid, setId, account)
            End Select
        End Sub

        Private Sub LoadAtArcemu(ByVal charguid As Integer, ByVal tarSetId As Integer, ByVal account As Account)
            LogAppend("Loading character Inventory @LoadAtArcemu", "CharacterInventoryHandler_LoadAtArcemu", False)
            Dim dt As DataTable =
                    ReturnDataTable(
                        "SELECT " & GlobalVariables.sourceStructure.itmins_slot_col(0) & " FROM " &
                        GlobalVariables.sourceStructure.item_instance_tbl(0) & " WHERE " &
                        GlobalVariables.sourceStructure.itmins_guid_col(0) & "='" & charguid.ToString() & "'")
            Dim tmpext As Integer
            Dim player As Character = GetCharacterSetBySetId(tarSetId, account)
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
                            Dim numresults As Integer =
                                    ReturnCountResults(
                                        "SELECT " & GlobalVariables.sourceStructure.itmins_container_col(0) & " FROM " &
                                        GlobalVariables.sourceStructure.item_instance_tbl(0) & " WHERE " &
                                        GlobalVariables.sourceStructure.itmins_ownerGuid_col(0) & "='" &
                                        charguid.ToString &
                                        "' AND " & GlobalVariables.sourceStructure.itmins_slot_col(0) & "='" &
                                        tmpext.ToString & "'")
                            If numresults = 1 Then
                                Dim containerslot As String =
                                        runSQLCommand_characters_string(
                                            "SELECT " & GlobalVariables.sourceStructure.itmins_container_col(0) &
                                            " FROM " &
                                            GlobalVariables.sourceStructure.item_instance_tbl(0) & " WHERE " &
                                            GlobalVariables.sourceStructure.itmins_ownerGuid_col(0) & "='" &
                                            charguid.ToString & "' AND " &
                                            GlobalVariables.sourceStructure.itmins_slot_col(0) &
                                            "='" & tmpext.ToString & "'")
                                Dim bagguid As String = "-1"
                                If Not containerslot = "-1" Then
                                    bagguid =
                                        runSQLCommand_characters_string(
                                            "SELECT " & GlobalVariables.sourceStructure.itmins_guid_col(0) & " FROM " &
                                            GlobalVariables.sourceStructure.item_instance_tbl(0) & " WHERE " &
                                            GlobalVariables.sourceStructure.itmins_ownerGuid_col(0) &
                                            "='" & charguid.ToString & "' AND " &
                                            GlobalVariables.sourceStructure.itmins_slot_col(0) & "='" & containerslot &
                                            "' AND " & GlobalVariables.sourceStructure.itmins_container_col(0) &
                                            "='-1'")
                                End If
                                If bagguid = "-1" Then
                                    If tmpext > 18 Then
                                        Dim bag As String
                                        Dim item As String
                                        Dim entryid As String
                                        Dim enchantments As String
                                        Dim itemcount As String

                                        bag = bagguid
                                        item =
                                            runSQLCommand_characters_string(
                                                "SELECT " & GlobalVariables.sourceStructure.itmins_guid_col(0) &
                                                " FROM " &
                                                GlobalVariables.sourceStructure.item_instance_tbl(0) & " WHERE " &
                                                GlobalVariables.sourceStructure.itmins_ownerGuid_col(0) &
                                                "='" & charguid.ToString & "' AND " &
                                                GlobalVariables.sourceStructure.itmins_slot_col(0) & "='" &
                                                tmpext.ToString &
                                                "' AND " & GlobalVariables.sourceStructure.itmins_container_col(0) &
                                                "='-1'")
                                        entryid =
                                            runSQLCommand_characters_string(
                                                "SELECT " & GlobalVariables.sourceStructure.itmins_itemEntry_col(0) &
                                                " FROM " & GlobalVariables.sourceStructure.item_instance_tbl(0) &
                                                " WHERE " &
                                                GlobalVariables.sourceStructure.itmins_guid_col(0) & " = '" & item & "'")
                                        enchantments =
                                            runSQLCommand_characters_string(
                                                "SELECT " & GlobalVariables.sourceStructure.itmins_enchantments_col(0) &
                                                " FROM " & GlobalVariables.sourceStructure.item_instance_tbl(0) &
                                                " WHERE " &
                                                GlobalVariables.sourceStructure.itmins_guid_col(0) & "='" & item & "'")
                                        itemcount =
                                            runSQLCommand_characters_string(
                                                "SELECT " & GlobalVariables.sourceStructure.itmins_count_col(0) &
                                                " FROM " &
                                                GlobalVariables.sourceStructure.item_instance_tbl(0) & " WHERE " &
                                                GlobalVariables.sourceStructure.itmins_guid_col(0) & "='" & item & "'")
                                        Dim newItm As New Item
                                        newItm.Slot = TryInt(tmpext.ToString)
                                        newItm.Bag = TryInt(bag)
                                        newItm.Bagguid = TryInt(bagguid)
                                        newItm.Id = TryInt(entryid)
                                        newItm.Enchantstring = enchantments
                                        newItm.Count = TryInt(itemcount)
                                        newItm.Container = - 1
                                        newItm.Guid = TryInt(item)
                                        If player.InventoryZeroItems Is Nothing Then _
                                            player.InventoryZeroItems = New List(Of Item)()
                                        player.InventoryZeroItems.Add(newItm)
                                        SetCharacterSet(tarSetId, player, account)
                                    End If
                                Else
                                    Dim bag As String
                                    Dim item As String
                                    Dim entryid As String
                                    Dim enchantments As String
                                    Dim itemcount As String
                                    bag =
                                        runSQLCommand_characters_string(
                                            "SELECT " & GlobalVariables.sourceStructure.itmins_itemEntry_col(0) &
                                            " FROM " &
                                            GlobalVariables.sourceStructure.item_instance_tbl(0) & " WHERE " &
                                            GlobalVariables.sourceStructure.itmins_guid_col(0) &
                                            " = '" & bagguid & "'")
                                    item =
                                        runSQLCommand_characters_string(
                                            "SELECT " & GlobalVariables.sourceStructure.itmins_guid_col(0) & " FROM " &
                                            GlobalVariables.sourceStructure.item_instance_tbl(0) & " WHERE " &
                                            GlobalVariables.sourceStructure.itmins_ownerGuid_col(0) &
                                            "='" & charguid.ToString & "' AND " &
                                            GlobalVariables.sourceStructure.itmins_slot_col(0) & "='" & tmpext.ToString &
                                            "'")
                                    entryid =
                                        runSQLCommand_characters_string(
                                            "SELECT " & GlobalVariables.sourceStructure.itmins_itemEntry_col(0) &
                                            " FROM " &
                                            GlobalVariables.sourceStructure.item_instance_tbl(0) & " WHERE " &
                                            GlobalVariables.sourceStructure.itmins_guid_col(0) &
                                            " = '" & item & "'")
                                    enchantments =
                                        runSQLCommand_characters_string(
                                            "SELECT " & GlobalVariables.sourceStructure.itmins_enchantments_col(0) &
                                            " FROM " & GlobalVariables.sourceStructure.item_instance_tbl(0) & " WHERE " &
                                            GlobalVariables.sourceStructure.itmins_guid_col(0) & "='" & item & "'")
                                    itemcount =
                                        runSQLCommand_characters_string(
                                            "SELECT " & GlobalVariables.sourceStructure.itmins_count_col(0) & " FROM " &
                                            GlobalVariables.sourceStructure.item_instance_tbl(0) & " WHERE " &
                                            GlobalVariables.sourceStructure.itmins_guid_col(0) &
                                            "='" & item & "'")
                                    Dim newItm As New Item
                                    newItm.Slot = TryInt(tmpext.ToString)
                                    newItm.Bag = TryInt(bag)
                                    newItm.Bagguid = TryInt(bagguid)
                                    newItm.Id = TryInt(entryid)
                                    newItm.Enchantstring = enchantments
                                    newItm.Count = TryInt(itemcount)
                                    newItm.Container = - 1
                                    newItm.Guid = TryInt(item)
                                    If player.InventoryItems Is Nothing Then _
                                        player.InventoryItems = New List(Of Item)()
                                    player.InventoryItems.Add(newItm)
                                    SetCharacterSet(tarSetId, player, account)
                                End If
                            Else
                                Dim containerslot As String =
                                        ReturnResultWithRow(
                                            "SELECT " & GlobalVariables.sourceStructure.itmins_container_col(0) &
                                            " FROM " &
                                            GlobalVariables.sourceStructure.item_instance_tbl(0) & " WHERE " &
                                            GlobalVariables.sourceStructure.itmins_ownerGuid_col(0) & "='" &
                                            charguid.ToString & "' AND " &
                                            GlobalVariables.sourceStructure.itmins_slot_col(0) & "='" & tmpext.ToString &
                                            "'", GlobalVariables.sourceStructure.itmins_container_col(0), 0)
                                Dim bagguid As String = "-1"
                                If Not containerslot = "-1" Then
                                    bagguid =
                                        runSQLCommand_characters_string(
                                            "SELECT " & GlobalVariables.sourceStructure.itmins_guid_col(0) & " FROM " &
                                            GlobalVariables.sourceStructure.item_instance_tbl(0) & " WHERE " &
                                            GlobalVariables.sourceStructure.itmins_ownerGuid_col(0) &
                                            "='" & charguid.ToString & "' AND " &
                                            GlobalVariables.sourceStructure.itmins_slot_col(0) & "='" & containerslot &
                                            "' AND " & GlobalVariables.sourceStructure.itmins_container_col(0) &
                                            "='-1'")
                                End If
                                If bagguid = "-1" Then
                                    If tmpext > 18 Then
                                        Dim bag As String
                                        Dim item As String
                                        Dim entryid As String
                                        Dim enchantments As String
                                        Dim itemcount As String
                                        bag = bagguid
                                        item =
                                            runSQLCommand_characters_string(
                                                "SELECT " & GlobalVariables.sourceStructure.itmins_guid_col(0) &
                                                " FROM " &
                                                GlobalVariables.sourceStructure.item_instance_tbl(0) & " WHERE " &
                                                GlobalVariables.sourceStructure.itmins_ownerGuid_col(0) &
                                                "='" & charguid.ToString & "' AND " &
                                                GlobalVariables.sourceStructure.itmins_slot_col(0) & "='" &
                                                tmpext.ToString &
                                                "' AND " & GlobalVariables.sourceStructure.itmins_container_col(0) &
                                                "='-1'")
                                        entryid =
                                            runSQLCommand_characters_string(
                                                "SELECT " & GlobalVariables.sourceStructure.itmins_itemEntry_col(0) &
                                                " FROM " & GlobalVariables.sourceStructure.item_instance_tbl(0) &
                                                " WHERE " &
                                                GlobalVariables.sourceStructure.itmins_guid_col(0) & " = '" & item & "'")
                                        enchantments =
                                            runSQLCommand_characters_string(
                                                "SELECT " & GlobalVariables.sourceStructure.itmins_enchantments_col(0) &
                                                " FROM " & GlobalVariables.sourceStructure.item_instance_tbl(0) &
                                                " WHERE " &
                                                GlobalVariables.sourceStructure.itmins_guid_col(0) & "='" & item & "'")
                                        itemcount =
                                            runSQLCommand_characters_string(
                                                "SELECT " & GlobalVariables.sourceStructure.itmins_count_col(0) &
                                                " FROM " &
                                                GlobalVariables.sourceStructure.item_instance_tbl(0) & " WHERE " &
                                                GlobalVariables.sourceStructure.itmins_guid_col(0) &
                                                "='" & item & "'")
                                        Dim newItm As New Item
                                        newItm.Slot = TryInt(tmpext.ToString)
                                        newItm.Bag = TryInt(bag)
                                        newItm.Bagguid = TryInt(bagguid)
                                        newItm.Id = TryInt(entryid)
                                        newItm.Enchantstring = enchantments
                                        newItm.Count = TryInt(itemcount)
                                        newItm.Container = - 1
                                        newItm.Guid = TryInt(item)
                                        If player.InventoryZeroItems Is Nothing Then _
                                            player.InventoryZeroItems = New List(Of Item)()
                                        player.InventoryZeroItems.Add(newItm)
                                        SetCharacterSet(tarSetId, player, account)
                                    End If
                                Else
                                    Dim bag As String
                                    Dim item As String
                                    Dim entryid As String
                                    Dim enchantments As String
                                    Dim itemcount As String
                                    bag =
                                        runSQLCommand_characters_string(
                                            "SELECT " & GlobalVariables.sourceStructure.itmins_itemEntry_col(0) &
                                            " FROM " &
                                            GlobalVariables.sourceStructure.item_instance_tbl(0) & " WHERE " &
                                            GlobalVariables.sourceStructure.itmins_guid_col(0) &
                                            " = '" & bagguid & "'")
                                    item =
                                        ReturnResultWithRow(
                                            "SELECT " & GlobalVariables.sourceStructure.itmins_guid_col(0) & " FROM " &
                                            GlobalVariables.sourceStructure.item_instance_tbl(0) & " WHERE " &
                                            GlobalVariables.sourceStructure.itmins_ownerGuid_col(0) &
                                            "='" & charguid.ToString & "' AND " &
                                            GlobalVariables.sourceStructure.itmins_slot_col(0) & "='" & tmpext.ToString &
                                            "'", GlobalVariables.sourceStructure.itmins_guid_col(0), 1)
                                    entryid =
                                        runSQLCommand_characters_string(
                                            "SELECT " & GlobalVariables.sourceStructure.itmins_itemEntry_col(0) &
                                            " FROM " &
                                            GlobalVariables.sourceStructure.item_instance_tbl(0) & " WHERE " &
                                            GlobalVariables.sourceStructure.itmins_guid_col(0) &
                                            " = '" & item & "'")
                                    enchantments =
                                        runSQLCommand_characters_string(
                                            "SELECT " & GlobalVariables.sourceStructure.itmins_enchantments_col(0) &
                                            " FROM " & GlobalVariables.sourceStructure.item_instance_tbl(0) & " WHERE " &
                                            GlobalVariables.sourceStructure.itmins_guid_col(0) & "='" & item & "'")
                                    itemcount =
                                        runSQLCommand_characters_string(
                                            "SELECT " & GlobalVariables.sourceStructure.itmins_count_col(0) & " FROM " &
                                            GlobalVariables.sourceStructure.item_instance_tbl(0) & " WHERE " &
                                            GlobalVariables.sourceStructure.itmins_guid_col(0) &
                                            "='" & item & "'")
                                    Dim newItm As New Item
                                    newItm.Slot = TryInt(tmpext.ToString)
                                    newItm.Bag = TryInt(bag)
                                    newItm.Bagguid = TryInt(bagguid)
                                    newItm.Id = TryInt(entryid)
                                    newItm.Enchantstring = enchantments
                                    newItm.Count = TryInt(itemcount)
                                    newItm.Container = - 1
                                    newItm.Guid = TryInt(item)
                                    If player.InventoryItems Is Nothing Then _
                                        player.InventoryItems = New List(Of Item)()
                                    player.InventoryItems.Add(newItm)
                                    SetCharacterSet(tarSetId, player, account)
                                End If
                                Dim containerslot2 As String =
                                        ReturnResultWithRow(
                                            "SELECT " & GlobalVariables.sourceStructure.itmins_container_col(0) &
                                            " FROM " &
                                            GlobalVariables.sourceStructure.item_instance_tbl(0) & " WHERE " &
                                            GlobalVariables.sourceStructure.itmins_ownerGuid_col(0) & "='" &
                                            charguid.ToString & "' AND " &
                                            GlobalVariables.sourceStructure.itmins_slot_col(0) & "='" & tmpext.ToString &
                                            "'", GlobalVariables.sourceStructure.itmins_container_col(0), 1)
                                Dim bagguid2 As String = "-1"
                                If Not containerslot2 = "-1" Then
                                    bagguid2 =
                                        runSQLCommand_characters_string(
                                            "SELECT " & GlobalVariables.sourceStructure.itmins_guid_col(0) & " FROM " &
                                            GlobalVariables.sourceStructure.item_instance_tbl(0) & " WHERE " &
                                            GlobalVariables.sourceStructure.itmins_ownerGuid_col(0) &
                                            "='" & charguid.ToString & "' AND " &
                                            GlobalVariables.sourceStructure.itmins_slot_col(0) & "='" & containerslot2 &
                                            "' AND " & GlobalVariables.sourceStructure.itmins_container_col(0) &
                                            "='-1'")
                                End If
                                If bagguid2 = "-1" Then
                                    If tmpext > 18 Then
                                        Dim bag As String
                                        Dim item As String
                                        Dim entryid As String
                                        Dim enchantments As String
                                        Dim itemcount As String
                                        bag = bagguid2
                                        item =
                                            ReturnResultWithRow(
                                                "SELECT " & GlobalVariables.sourceStructure.itmins_guid_col(0) &
                                                " FROM " &
                                                GlobalVariables.sourceStructure.item_instance_tbl(0) & " WHERE " &
                                                GlobalVariables.sourceStructure.itmins_ownerGuid_col(0) & "='" &
                                                charguid.ToString & "' AND " &
                                                GlobalVariables.sourceStructure.itmins_slot_col(0) & "='" &
                                                tmpext.ToString &
                                                "' AND " & GlobalVariables.sourceStructure.itmins_container_col(0) &
                                                "='-1'", GlobalVariables.sourceStructure.itmins_guid_col(0), 1)
                                        entryid =
                                            runSQLCommand_characters_string(
                                                "SELECT " & GlobalVariables.sourceStructure.itmins_itemEntry_col(0) &
                                                " FROM " & GlobalVariables.sourceStructure.item_instance_tbl(0) &
                                                " WHERE " &
                                                GlobalVariables.sourceStructure.itmins_guid_col(0) & " = '" & item & "'")
                                        enchantments =
                                            runSQLCommand_characters_string(
                                                "SELECT " & GlobalVariables.sourceStructure.itmins_enchantments_col(0) &
                                                " FROM " & GlobalVariables.sourceStructure.item_instance_tbl(0) &
                                                " WHERE " &
                                                GlobalVariables.sourceStructure.itmins_guid_col(0) & "='" & item & "'")
                                        itemcount =
                                            runSQLCommand_characters_string(
                                                "SELECT " & GlobalVariables.sourceStructure.itmins_count_col(0) &
                                                " FROM " &
                                                GlobalVariables.sourceStructure.item_instance_tbl(0) & " WHERE " &
                                                GlobalVariables.sourceStructure.itmins_guid_col(0) & "='" & item & "'")
                                        Dim newItm As New Item
                                        newItm.Slot = TryInt(tmpext.ToString)
                                        newItm.Bag = TryInt(bag)
                                        newItm.Bagguid = TryInt(bagguid)
                                        newItm.Id = TryInt(entryid)
                                        newItm.Enchantstring = enchantments
                                        newItm.Count = TryInt(itemcount)
                                        newItm.Container = - 1
                                        newItm.Guid = TryInt(item)
                                        If player.InventoryZeroItems Is Nothing Then _
                                            player.InventoryZeroItems = New List(Of Item)()
                                        player.InventoryZeroItems.Add(newItm)
                                        SetCharacterSet(tarSetId, player, account)
                                    End If
                                Else
                                    Dim bag As String
                                    Dim item As String
                                    Dim entryid As String
                                    Dim enchantments As String
                                    Dim itemcount As String
                                    bag =
                                        runSQLCommand_characters_string(
                                            "SELECT " & GlobalVariables.sourceStructure.itmins_itemEntry_col(0) &
                                            " FROM " &
                                            GlobalVariables.sourceStructure.item_instance_tbl(0) & " WHERE " &
                                            GlobalVariables.sourceStructure.itmins_guid_col(0) &
                                            " = '" & bagguid2 & "'")
                                    item =
                                        ReturnResultWithRow(
                                            "SELECT " & GlobalVariables.sourceStructure.itmins_guid_col(0) & " FROM " &
                                            GlobalVariables.sourceStructure.item_instance_tbl(0) & " WHERE " &
                                            GlobalVariables.sourceStructure.itmins_ownerGuid_col(0) & "='" &
                                            charguid.ToString & "' AND " &
                                            GlobalVariables.sourceStructure.itmins_slot_col(0) & "='" & tmpext.ToString &
                                            "'", GlobalVariables.sourceStructure.itmins_guid_col(0), 1)
                                    entryid =
                                        runSQLCommand_characters_string(
                                            "SELECT " & GlobalVariables.sourceStructure.itmins_itemEntry_col(0) &
                                            " FROM " &
                                            GlobalVariables.sourceStructure.item_instance_tbl(0) & " WHERE " &
                                            GlobalVariables.sourceStructure.itmins_guid_col(0) &
                                            " = '" & item & "'")
                                    enchantments =
                                        runSQLCommand_characters_string(
                                            "SELECT " & GlobalVariables.sourceStructure.itmins_enchantments_col(0) &
                                            " FROM " & GlobalVariables.sourceStructure.item_instance_tbl(0) & " WHERE " &
                                            GlobalVariables.sourceStructure.itmins_guid_col(0) & "='" & item & "'")
                                    itemcount =
                                        runSQLCommand_characters_string(
                                            "SELECT " & GlobalVariables.sourceStructure.itmins_count_col(0) & " FROM " &
                                            GlobalVariables.sourceStructure.item_instance_tbl(0) & " WHERE " &
                                            GlobalVariables.sourceStructure.itmins_guid_col(0) &
                                            "='" & item & "'")
                                    Dim newItm As New Item
                                    newItm.Slot = TryInt(tmpext.ToString)
                                    newItm.Bag = TryInt(bag)
                                    newItm.Bagguid = TryInt(bagguid)
                                    newItm.Id = TryInt(entryid)
                                    newItm.Enchantstring = enchantments
                                    newItm.Count = TryInt(itemcount)
                                    newItm.Container = TryInt(containerslot2)
                                    newItm.Guid = TryInt(item)
                                    If player.InventoryItems Is Nothing Then _
                                        player.InventoryItems = New List(Of Item)()
                                    player.InventoryItems.Add(newItm)
                                    SetCharacterSet(tarSetId, player, account)
                                End If
                            End If
                            count += 1
                        Else
                            count += 1
                        End If
                    Loop Until count = lastcount
                End If
            Catch ex As Exception
                LogAppend(
                    "Something went wrong while loading character Inventory! -> skipping -> Exception is: ###START###" &
                    ex.ToString() & "###END###", "CharacterInventoryHandler_LoadAtArcemu", True, True)
                Exit Sub
            End Try
        End Sub

        Private Sub LoadAtTrinity(ByVal charguid As Integer, ByVal tarSetId As Integer, ByVal account As Account)
            LogAppend("Loading character Inventory @LoadAtTrinity", "CharacterInventoryHandler_LoadAtTrinity", False)
            Dim inventoryDt As DataTable = ReturnDataTable(
                "SELECT * FROM " &
                GlobalVariables.sourceStructure.character_inventory_tbl(0) & " WHERE " &
                GlobalVariables.sourceStructure.invent_guid_col(0) & "='" & charguid.ToString() & "'")
            Dim itemInstanceDt As DataTable = ReturnDataTable(
                "SELECT `" & GlobalVariables.sourceStructure.itmins_guid_col(0) & "`, `" &
                GlobalVariables.sourceStructure.itmins_itemEntry_col(0) & "`, `" &
                GlobalVariables.sourceStructure.itmins_ownerGuid_col(0) & "`, `" &
                GlobalVariables.sourceStructure.itmins_count_col(0) & "`, `" &
                GlobalVariables.sourceStructure.itmins_enchantments_col(0) & "` FROM " &
                GlobalVariables.sourceStructure.item_instance_tbl(0) & " WHERE " &
                GlobalVariables.sourceStructure.itmins_ownerGuid_col(0) & "='" & charguid.ToString() & "'")
            Dim player As Character = GetCharacterSetBySetId(tarSetId, account)
            If inventoryDt Is Nothing Or itemInstanceDt Is Nothing Then
                LogAppend("No inventory items found", "CharacterInventoryHandler_LoadAtTrinity", False)
                Exit Sub
            End If
            Try
                Dim lastcount As Integer = inventoryDt.Rows.Count
                Dim count As Integer = 0
                If Not lastcount = 0 Then
                    Do
                        Dim itemGuid As Integer = TryInt(inventoryDt.Rows(count).Item(3).ToString())
                        Dim bagGuid As Integer = TryInt(
                            SafeExecuteDataTableSearch(inventoryDt,
                                                       GlobalVariables.sourceStructure.invent_guid_col(0) & " = '" &
                                                       EscapeLikeValue(charguid.ToString) & "' AND " &
                                                       GlobalVariables.sourceStructure.invent_item_col(0) & " = '" &
                                                       EscapeLikeValue(itemGuid.ToString()) & "'", 1))

                        If bagGuid = 0 Then
                            Try
                                Dim slot As Integer
                                slot = TryInt(
                                    SafeExecuteDataTableSearch(inventoryDt,
                                                               GlobalVariables.sourceStructure.invent_item_col(0) &
                                                               " = '" &
                                                               EscapeLikeValue(itemGuid.ToString()) & "'", 2))
                                If slot > 18 Then
                                    Dim bag As String
                                    Dim item As String
                                    Dim entryid As String
                                    Dim enchantments As String
                                    Dim itemcount As String
                                    bag = bagGuid.ToString()
                                    item = itemGuid.ToString()
                                    entryid =
                                        SafeExecuteDataTableSearch(itemInstanceDt,
                                                                   GlobalVariables.sourceStructure.itmins_guid_col(0) &
                                                                   " = '" &
                                                                   EscapeLikeValue(item) & "'", 1)
                                    If entryid Is Nothing Then
                                        count += 1
                                        Continue Do
                                    End If
                                    enchantments =
                                        SafeExecuteDataTableSearch(itemInstanceDt,
                                                                   GlobalVariables.sourceStructure.itmins_guid_col(0) &
                                                                   " = '" &
                                                                   EscapeLikeValue(item) & "'", 4)
                                    itemcount =
                                        SafeExecuteDataTableSearch(itemInstanceDt,
                                                                   GlobalVariables.sourceStructure.itmins_guid_col(0) &
                                                                   " = '" &
                                                                   EscapeLikeValue(item) & "'", 3)

                                    Dim newItm As New Item
                                    newItm.Slot = slot
                                    newItm.Bag = TryInt(bag)
                                    newItm.Bagguid = bagGuid
                                    newItm.Id = TryInt(entryid)
                                    newItm.Enchantstring = enchantments
                                    newItm.Count = TryInt(itemcount)
                                    newItm.Guid = TryInt(item)
                                    If player.InventoryZeroItems Is Nothing Then _
                                        player.InventoryZeroItems = New List(Of Item)()
                                    player.InventoryZeroItems.Add(newItm)
                                End If
                            Catch ex As Exception
                                LogAppend(
                                    "Something went wrong while loading character Inventory! -> skipping -> Exception is: ###START###" &
                                    ex.ToString() & "###END###", "CharacterInventoryHandler_LoadAtTrinity", True, True)
                            End Try
                        Else
                            Try
                                Dim bag As String
                                Dim item As String
                                Dim entryid As String
                                Dim enchantments As String
                                Dim itemcount As String
                                Dim slot As String
                                bag =
                                    SafeExecuteDataTableSearch(itemInstanceDt,
                                                               GlobalVariables.sourceStructure.itmins_guid_col(0) &
                                                               " = '" &
                                                               EscapeLikeValue(bagGuid.ToString()) & "'", 1)
                                item = itemGuid.ToString
                                entryid =
                                    SafeExecuteDataTableSearch(itemInstanceDt,
                                                               GlobalVariables.sourceStructure.itmins_guid_col(0) &
                                                               " = '" &
                                                               EscapeLikeValue(item) & "'", 1)
                                enchantments =
                                    SafeExecuteDataTableSearch(itemInstanceDt,
                                                               GlobalVariables.sourceStructure.itmins_guid_col(0) &
                                                               " = '" &
                                                               EscapeLikeValue(item) & "'", 4)
                                itemcount =
                                    SafeExecuteDataTableSearch(itemInstanceDt,
                                                               GlobalVariables.sourceStructure.itmins_guid_col(0) &
                                                               " = '" &
                                                               EscapeLikeValue(item) & "'", 3)
                                slot =
                                    SafeExecuteDataTableSearch(inventoryDt,
                                                               GlobalVariables.sourceStructure.invent_item_col(0) &
                                                               " = '" &
                                                               EscapeLikeValue(itemGuid.ToString()) & "'", 2)
                                Dim newItm As New Item
                                newItm.Slot = TryInt(slot)
                                newItm.Bag = TryInt(bag)
                                newItm.Bagguid = bagGuid
                                newItm.Id = TryInt(entryid)
                                newItm.Enchantstring = enchantments
                                newItm.Count = TryInt(itemcount)
                                newItm.Guid = TryInt(item)
                                If player.InventoryItems Is Nothing Then player.InventoryItems = New List(Of Item)()
                                player.InventoryItems.Add(newItm)
                            Catch ex As Exception
                                LogAppend(
                                    "Something went wrong while loading character Inventory! -> skipping -> Exception is: ###START###" &
                                    ex.ToString() & "###END###", "CharacterInventoryHandler_LoadAtTrinity", True, True)
                            End Try
                        End If
                        count += 1
                    Loop Until count = lastcount
                Else
                    LogAppend("No Inventory found!", "CharacterInventoryHandler_LoadAtTrinity", True)
                End If
            Catch ex As Exception
                LogAppend(
                    "Something went wrong while loading character Inventory! -> skipping -> Exception is: ###START###" &
                    ex.ToString() & "###END###", "CharacterInventoryHandler_LoadAtTrinity", True, True)
                Exit Sub
            End Try
            SetCharacterSet(tarSetId, player, account)
        End Sub

        Private Sub LoadAtMangos(ByVal charguid As Integer, ByVal tarSetId As Integer, ByVal account As Account)
            LogAppend("Loading character Inventory @LoadAtMangos", "CharacterInventoryHandler_LoadAtMangos", False)
            Dim dt As DataTable =
                    ReturnDataTable(
                        "SELECT " & GlobalVariables.sourceStructure.invent_item_col(0) & " FROM " &
                        GlobalVariables.sourceStructure.character_inventory_tbl(0) & " WHERE " &
                        GlobalVariables.sourceStructure.invent_guid_col(0) & "='" & charguid.ToString() & "'")
            Dim tmpext As Integer
            Dim player As Character = GetCharacterSetBySetId(tarSetId, account)
            Try
                Dim lastcount As Integer = TryInt(dt.Rows.Count.ToString)
                Dim count As Integer = 0
                If Not lastcount = 0 Then
                    Do
                        Dim readedcode As String = (dt.Rows(count).Item(0)).ToString
                        tmpext = TryInt(readedcode)
                        Dim bagguid As String =
                                runSQLCommand_characters_string(
                                    "SELECT " & GlobalVariables.sourceStructure.invent_bag_col(0) & " FROM " &
                                    GlobalVariables.sourceStructure.character_inventory_tbl(0) & " WHERE " &
                                    GlobalVariables.sourceStructure.invent_guid_col(0) & "='" & charguid.ToString &
                                    "' AND " &
                                    GlobalVariables.sourceStructure.invent_item_col(0) & "='" & tmpext.ToString & "'")
                        If TryInt(bagguid) = 0 Then
                            If tmpext > 18 Then
                                Dim bag As String
                                Dim item As String
                                Dim entryid As String
                                Dim enchantments As String
                                Dim itemcount As String
                                Dim slot As String
                                bag = bagguid
                                item = tmpext.ToString()
                                entryid =
                                    runSQLCommand_characters_string(
                                        "SELECT " & GlobalVariables.sourceStructure.invent_item_template_col(0) &
                                        " FROM " &
                                        GlobalVariables.sourceStructure.character_inventory_tbl(0) & " WHERE " &
                                        GlobalVariables.sourceStructure.invent_guid_col(0) & " = '" & charguid.ToString &
                                        "' AND " & GlobalVariables.sourceStructure.invent_item_col(0) & "='" & item &
                                        "'")
                                enchantments =
                                    runSQLCommand_characters_string(
                                        "SELECT `" & GlobalVariables.sourceStructure.itmins_data_col(0) & "` FROM " &
                                        GlobalVariables.sourceStructure.item_instance_tbl(0) & " WHERE " &
                                        GlobalVariables.sourceStructure.itmins_guid_col(0) & " = '" & item & "'")
                                itemcount = Splititemdata(enchantments, 14)
                                slot =
                                    runSQLCommand_characters_string(
                                        "Select `" & GlobalVariables.sourceStructure.invent_slot_col(0) & "` FROM " &
                                        GlobalVariables.sourceStructure.character_inventory_tbl(0) & " WHERE `" &
                                        GlobalVariables.sourceStructure.invent_item_col(0) & "`='" & item & "'")
                                Dim newItm As New Item
                                newItm.Slot = TryInt(slot)
                                newItm.Bag = TryInt(bag)
                                newItm.Bagguid = TryInt(bagguid)
                                newItm.Id = TryInt(entryid)
                                newItm.Enchantstring = enchantments
                                newItm.Count = TryInt(itemcount)
                                newItm.Guid = TryInt(item)
                                If player.InventoryZeroItems Is Nothing Then _
                                    player.InventoryZeroItems = New List(Of Item)()
                                player.InventoryZeroItems.Add(newItm)
                            End If
                        Else
                            Dim bag As String
                            Dim item As String
                            Dim entryid As String
                            Dim enchantments As String
                            Dim itemcount As String
                            Dim slot As String
                            bag =
                                runSQLCommand_characters_string(
                                    "SELECT " & GlobalVariables.sourceStructure.invent_item_template_col(0) & " FROM " &
                                    GlobalVariables.sourceStructure.character_inventory_tbl(0) & " WHERE " &
                                    GlobalVariables.sourceStructure.invent_item_col(0) & " = '" & bagguid & "'")
                            item = tmpext.ToString
                            entryid =
                                runSQLCommand_characters_string(
                                    "SELECT " & GlobalVariables.sourceStructure.invent_item_template_col(0) & " FROM " &
                                    GlobalVariables.sourceStructure.character_inventory_tbl(0) & " WHERE " &
                                    GlobalVariables.sourceStructure.invent_guid_col(0) & " = '" & charguid.ToString &
                                    "' AND " & GlobalVariables.sourceStructure.invent_item_col(0) & "='" &
                                    tmpext.ToString &
                                    "'")
                            enchantments =
                                runSQLCommand_characters_string(
                                    "SELECT `" & GlobalVariables.sourceStructure.itmins_data_col(0) & "` FROM " &
                                    GlobalVariables.sourceStructure.item_instance_tbl(0) & " WHERE " &
                                    GlobalVariables.sourceStructure.itmins_guid_col(0) & " = '" & item & "'")
                            itemcount = Splititemdata(enchantments, 14)
                            slot =
                                runSQLCommand_characters_string(
                                    "Select `" & GlobalVariables.sourceStructure.invent_slot_col(0) & "` FROM " &
                                    GlobalVariables.sourceStructure.character_inventory_tbl(0) & " WHERE `" &
                                    GlobalVariables.sourceStructure.invent_item_col(0) & "`='" & item & "'")
                            Dim newItm As New Item
                            newItm.Slot = TryInt(slot)
                            newItm.Bag = TryInt(bag)
                            newItm.Bagguid = TryInt(bagguid)
                            newItm.Id = TryInt(entryid)
                            newItm.Enchantstring = enchantments
                            newItm.Count = TryInt(itemcount)
                            newItm.Guid = TryInt(item)
                            If player.InventoryItems Is Nothing Then player.InventoryItems = New List(Of Item)()
                            player.InventoryItems.Add(newItm)
                        End If
                        count += 1
                    Loop Until count = lastcount
                Else
                    LogAppend("No Inventory found!", "CharacterInventoryHandler_LoadAtMangos", True)
                End If
            Catch ex As Exception
                LogAppend(
                    "Something went wrong while loading character Inventory! -> skipping -> Exception is: ###START###" &
                    ex.ToString() & "###END###", "CharacterInventoryHandler_LoadAtMangos", True, True)
                Exit Sub
            End Try
            SetCharacterSet(tarSetId, player, account)
        End Sub

        Private Function Splititemdata(ByVal datastring As String, ByVal position As Integer) As String
            Try
                Dim parts() As String = datastring.Split(" "c)
                Return parts(position)
            Catch
                Return "1"
            End Try
        End Function
    End Class
End Namespace