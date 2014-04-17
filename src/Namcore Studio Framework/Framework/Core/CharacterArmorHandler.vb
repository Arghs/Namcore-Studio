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
'*      /Filename:      CharacterArmorHandler
'*      /Description:   Contains functions for extracting information about the equipped 
'*                      armor of a specific character
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Database
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Modules

Namespace Framework.Core
    Public Class CharacterArmorHandler
        Public Sub GetCharacterArmor(ByVal charguid As Integer, ByVal setId As Integer, ByVal account As Account)
            LogAppend("Loading character Armor for charguid: " & charguid & " and setId: " & setId,
                      "CharacterArmorHandler_GetCharacterArmor", True)
            Dim player As Character = GetCharacterSetBySetId(setId, account)
            player.ArmorItems = New List(Of Item)()
            SetCharacterSet(setId, player, account)
            Select Case GlobalVariables.sourceCore
                Case "arcemu"
                    LoadAtArcemu(charguid, setId, account)
                Case "trinity"
                    LoadAtTrinity(charguid, setId, account)
                Case "trinitytbc"
                    'todo LoadAtTrinityTBC(charguid, setId, accountId)
                Case "mangos"
                    LoadAtMangos(charguid, setId, account)
            End Select
        End Sub

        Private Sub LoadAtArcemu(ByVal charguid As Integer, ByVal tarSetId As Integer, ByVal account As Account)
            LogAppend("Loading character Armor @LoadAtArcemu", "CharacterArmorHandler_LoadAtArcemu", False)
            Dim tempdt As DataTable =
                    ReturnDataTable(
                        "SELECT " & GlobalVariables.sourceStructure.itmins_guid_col(0) & ", " &
                        GlobalVariables.sourceStructure.itmins_itemEntry_col(0) & ", " &
                        GlobalVariables.sourceStructure.itmins_slot_col(0) &
                        " FROM " & GlobalVariables.sourceStructure.item_instance_tbl(0) & " WHERE " &
                        GlobalVariables.sourceStructure.itmins_ownerGuid_col(0) & "='" & charguid.ToString &
                        "' AND " & GlobalVariables.sourceStructure.itmins_container_col(0) & "='-1'")
            Dim mItmStatsHandler As CharacterItemStatsHandler = New CharacterItemStatsHandler
            Dim itemguid As Integer
            Dim slotname As String
            Dim itementry As Integer
            Dim itemslot As Integer
            Dim entrycount As Integer = tempdt.Rows.Count
            Dim loopcounter As Integer = 0
            If entrycount = 0 Then
                LogAppend("No items found for character " & charguid.ToString & " -> Skipping",
                          "CharacterArmorHandler_LoadAtArcemu", True, False)
                Exit Sub
            End If
            Do
                Try
                    itemguid = TryInt((tempdt.Rows(loopcounter).Item(0)).ToString)
                    itementry = TryInt((tempdt.Rows(loopcounter).Item(1)).ToString)
                    itemslot = TryInt((tempdt.Rows(loopcounter).Item(2)).ToString)
                    If itemslot > 18 Then
                        loopcounter += 1
                        Continue Do
                    End If
                    slotname = GetItemSlotNameById(itemslot)
                    If itementry > 1 Then
                        Dim itm As New Item
                        itm.Slotname = slotname
                        itm.Slot = itemslot
                        itm.Id = itementry
                        itm.Guid = itemguid
                        Dim player As Character = GetCharacterSetBySetId(tarSetId, account)
                        player.ArmorItems.Add(itm)
                        SetCharacterSet(tarSetId, player, account)
                        mItmStatsHandler.GetItemStats(itemguid, itm, player, tarSetId,
                                                      GetAccountSetBySetId(player.AccountSet))
                        LoadWeaponType(itementry, tarSetId, account)
                    End If
                Catch ex As Exception
                    LogAppend("Something went wrong! -> Exception is: ###START###" & ex.ToString() & "###END###",
                              "CharacterArmorHandler_LoadAtArcemu", False, True)
                    loopcounter += 1
                    Continue Do
                End Try
                loopcounter += 1
            Loop Until loopcounter = entrycount
        End Sub

        Private Sub LoadAtTrinity(ByVal charguid As Integer, ByVal tarSetId As Integer, ByVal account As Account)
            LogAppend("Loading character Armor @LoadAtTrinity", "CharacterArmorHandler_LoadAtTrinity", False)
            Dim cmd As String = "SELECT " & GlobalVariables.sourceStructure.invent_item_col(0) & ", " &
                                GlobalVariables.sourceStructure.invent_slot_col(0) & ", " &
                                GlobalVariables.sourceStructure.itmins_itemEntry_col(0) &
                                " FROM `" & GlobalVariables.sourceStructure.character_inventory_tbl(0) & "` JOIN `" &
                                GlobalVariables.sourceStructure.item_instance_tbl(0) & "` ON `" &
                                GlobalVariables.sourceStructure.character_inventory_tbl(0) &
                                "`." & GlobalVariables.sourceStructure.invent_item_col(0) & " = " & "`" &
                                GlobalVariables.sourceStructure.item_instance_tbl(0) & "`." &
                                GlobalVariables.sourceStructure.itmins_guid_col(0) &
                                " WHERE `" & GlobalVariables.sourceStructure.character_inventory_tbl(0) & "`." &
                                GlobalVariables.sourceStructure.invent_guid_col(0) & "='" & charguid.ToString() &
                                "' AND " &
                                GlobalVariables.sourceStructure.invent_bag_col(0) &
                                "='0' AND " & GlobalVariables.sourceStructure.invent_slot_col(0) & " < '19'"
            Dim tempdt As DataTable = ReturnDataTable(cmd)
            Dim mItmStatsHandler As CharacterItemStatsHandler = New CharacterItemStatsHandler
            Dim itemguid As Integer
            Dim slotname As String
            Dim itementry As Integer
            Dim itemslot As Integer
            Dim entrycount As Integer = tempdt.Rows.Count
            Dim loopcounter As Integer = 0
            If entrycount = 0 Then
                LogAppend("No items found for character " & charguid.ToString & " -> Skipping",
                          "CharacterArmorHandler_LoadAtTrinity", True, False)
                Exit Sub
            End If
            Do
                Try
                    itemguid = TryInt((tempdt.Rows(loopcounter).Item(0)).ToString)
                    itemslot = TryInt((tempdt.Rows(loopcounter).Item(1)).ToString)
                    itementry = TryInt((tempdt.Rows(loopcounter).Item(2)).ToString)
                    slotname = GetItemSlotNameById(itemslot)
                    If itementry > 1 Then
                        Dim itm As New Item
                        itm.Slotname = slotname
                        itm.Slot = itemslot
                        itm.Id = itementry
                        itm.Guid = itemguid
                        Dim player As Character = GetCharacterSetBySetId(tarSetId, account)
                        player.ArmorItems.Add(itm)
                        SetCharacterSet(tarSetId, player, account)
                        mItmStatsHandler.GetItemStats(itemguid, itm, player, tarSetId,
                                                      GetAccountSetBySetId(player.AccountSet))
                    End If
                Catch ex As Exception
                    LogAppend("Something went wrong! -> Exception is: ###START###" & ex.ToString() & "###END###",
                              "CharacterArmorHandler_LoadAtTrinity", False, True)
                    loopcounter += 1
                    Continue Do
                End Try
                loopcounter += 1
            Loop Until loopcounter = entrycount
        End Sub

        Private Sub LoadAtMangos(ByVal charguid As Integer, ByVal tarSetId As Integer, ByVal account As Account)
            LogAppend("Loading character Armor @LoadAtMangos", "CharacterArmorHandler_LoadAtMangos", False)
            Dim tempdt As DataTable =
                    ReturnDataTable(
                        "SELECT " & GlobalVariables.sourceStructure.invent_item_col(0) & ", " &
                        GlobalVariables.sourceStructure.invent_slot_col(0) & ", " &
                        GlobalVariables.sourceStructure.invent_item_template_col(0) &
                        " FROM `" & GlobalVariables.sourceStructure.character_inventory_tbl(0) & "` WHERE " &
                        GlobalVariables.sourceStructure.invent_guid_col(0) & "='" & charguid.ToString() &
                        "' AND " & GlobalVariables.sourceStructure.invent_bag_col(0) & "='0' AND " &
                        GlobalVariables.sourceStructure.invent_slot_col(0) & " < '19'")
            Dim mItmStatsHandler As CharacterItemStatsHandler = New CharacterItemStatsHandler
            Dim itemguid As Integer
            Dim slotname As String
            Dim itementry As Integer
            Dim itemslot As Integer
            Dim entrycount As Integer = tempdt.Rows.Count
            Dim loopcounter As Integer = 0
            If entrycount = 0 Then
                LogAppend("No items found for character " & charguid.ToString & " -> Skipping",
                          "CharacterArmorHandler_LoadAtMangos", True, False)
                Exit Sub
            End If
            Do
                Try
                    itemguid = TryInt((tempdt.Rows(loopcounter).Item(0)).ToString)
                    itemslot = TryInt((tempdt.Rows(loopcounter).Item(1)).ToString)
                    itementry = TryInt((tempdt.Rows(loopcounter).Item(2)).ToString)
                    slotname = GetItemSlotNameById(itemslot)
                    If itementry > 1 Then
                        Dim itm As New Item
                        itm.Slotname = slotname
                        itm.Slot = itemslot
                        itm.Id = itementry
                        itm.Guid = itemguid
                        Dim player As Character = GetCharacterSetBySetId(tarSetId, account)
                        player.ArmorItems.Add(itm)
                        SetCharacterSet(tarSetId, player, account)
                        mItmStatsHandler.GetItemStats(itemguid, itm, player, tarSetId,
                                                      GetAccountSetBySetId(player.AccountSet))
                        LoadWeaponType(itementry, tarSetId, account)
                    End If
                Catch ex As Exception
                    LogAppend("Something went wrong! -> Exception is: ###START###" & ex.ToString() & "###END###",
                              "CharacterArmorHandler_LoadAtMangos", False, True)
                    loopcounter += 1
                    Continue Do
                End Try
                loopcounter += 1
            Loop Until loopcounter = entrycount
        End Sub

        Private Function GetItemSlotNameById(ByVal itemslot As Integer) As String
            Dim slotname As String
            Select Case itemslot
                Case 0 : slotname = "head"
                Case 1 : slotname = "neck"
                Case 2 : slotname = "shoulder"
                Case 3 : slotname = "shirt"
                Case 4 : slotname = "chest"
                Case 5 : slotname = "waist"
                Case 6 : slotname = "legs"
                Case 7 : slotname = "feet"
                Case 8 : slotname = "wrists"
                Case 9 : slotname = "hands"
                Case 10 : slotname = "finger1"
                Case 11 : slotname = "finger2"
                Case 12 : slotname = "trinket1"
                Case 13 : slotname = "trinket2"
                Case 14 : slotname = "back"
                Case 15 : slotname = "main"
                Case 16 : slotname = "off"
                Case 17 : slotname = "distance"
                Case 18 : slotname = "tabard"
                Case Else : slotname = "unknown"
            End Select
            Return slotname
        End Function
    End Class
End Namespace