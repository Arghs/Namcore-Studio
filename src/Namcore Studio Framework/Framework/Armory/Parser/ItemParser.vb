'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013-2015 NamCore Studio <https://github.com/megasus/Namcore-Studio>
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
'*      /Filename:      ItemParser
'*      /Description:   Contains functions for loading character items from wow armory
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports Newtonsoft.Json.Linq
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Extension
Imports NCFramework.Framework.Modules
Imports System.Net
Imports libnc.Provider
Imports NCFramework.Framework.Extension.Special

Namespace Framework.Armory.Parser
    Public Class ItemParser
        '// Declaration
        Private _lastStamp As Integer = 0
        '// Declaration

        Public Sub LoadItems(ByVal setId As Integer, ByVal apiLink As String, ByVal account As Account)
            Dim client As New WebClient
            client.CheckProxy()
            '// Retrieving character
            Dim player As Character = GetCharacterSetBySetId(setId, account)
            player.ArmorItems = New List(Of Item)()
            LogAppend("Loading character items - setId: " & setId.ToString(), "ItemParser_LoadItems", True)
            Try
                '// Using API to load item info
                Dim itemContext As String = client.DownloadString(apiLink & "?fields=items")
                If Not itemContext.Contains("""items"":") Then
                    LogAppend("No items found!?", "ItemParser_LoadItems", True)
                    Exit Sub
                End If
                Dim jResults As JObject = JObject.Parse(itemContext)
                Dim results As List(Of JToken) = jResults.Children().ToList()
                Dim token As JProperty = CType(results.Find(Function(jtoken) CType(jtoken, JProperty).Name = "items"),
                                               JProperty)
                If token.HasChildren() Then
                    For i As Integer = 0 To token.GetChildren.Count - 1
                        Dim chld As JProperty = token.GetChildren(i)
                        If Not chld.HasChildren Then Continue For
                        If Not chld.HasItem("id") Then Continue For
                        Dim playerItem As New Item
                        playerItem.Id = CInt(chld.GetItem("id"))
                        playerItem.Slot = GetSlotIdByName(chld.Name)
                        LogAppend("Now loading info for slot " & playerItem.Slot.ToString(), "ItemParser_LoadItems",
                                  True)
                        playerItem.Slotname = GetItemSlotNameBySlotId(playerItem.Slot)
                        Dim x As DateTime = Date.Now
                        If _lastStamp = 0 Then
                            _lastStamp = x.ToTimeStamp()
                        Else
                            _lastStamp += 1
                        End If
                        playerItem.Guid = _lastStamp
                        playerItem.Name = chld.GetItem("name")
                        playerItem.Rarity = CType(CInt(chld.GetItem("quality")), Item.RarityType)
                        If chld.HasItem("tooltipParams") Then
                            Dim tChlds As JProperty = chld.GetChild("tooltipParams")
                            If tChlds.HasItem("gem0") Then
                                LogAppend("Found gem socket @0", "ItemParser_LoadItems", False)
                                playerItem.Socket1Id = CInt(tChlds.GetItem("gem0"))
                                playerItem.Socket1Pic = GetItemIconByItemId(playerItem.Socket1Id, GlobalVariables.GlobalWebClient)
                                playerItem.Socket1Effectid = GetEffectIdByGemId(playerItem.Socket1Id)
                                playerItem.Socket1Name = GetEffectNameById(playerItem.Socket1Effectid, My.Settings.language)
                            End If
                            If tChlds.HasItem("gem1") Then
                                LogAppend("Found gem socket @1", "ItemParser_LoadItems", False)
                                playerItem.Socket2Id = CInt(tChlds.GetItem("gem1"))
                                playerItem.Socket2Pic = GetItemIconByItemId(playerItem.Socket2Id, GlobalVariables.GlobalWebClient)
                                playerItem.Socket2Effectid = GetEffectIdByGemId(playerItem.Socket2Id)
                                playerItem.Socket2Name = GetEffectNameById(playerItem.Socket2Effectid, My.Settings.language)
                            End If
                            If tChlds.HasItem("gem2") Then
                                LogAppend("Found gem socket @2", "ItemParser_LoadItems", False)
                                playerItem.Socket3Id = CInt(tChlds.GetItem("gem2"))
                                playerItem.Socket3Pic = GetItemIconByItemId(playerItem.Socket3Id, GlobalVariables.GlobalWebClient)
                                playerItem.Socket3Effectid = GetEffectIdByGemId(playerItem.Socket3Id)
                                playerItem.Socket3Name = GetEffectNameById(playerItem.Socket3Effectid, My.Settings.language)
                            End If
                            If tChlds.HasItem("enchant") Then
                                LogAppend("Found item enchantment", "ItemParser_LoadItems", False)
                                playerItem.EnchantmentEffectid = CInt(tChlds.GetItem("enchant"))
                                playerItem.EnchantmentId =
                                    GetEnchantmentIdAndTypeByEffectId(playerItem.EnchantmentEffectid)(0)
                                playerItem.EnchantmentType =
                                    CType(GetEnchantmentIdAndTypeByEffectId(playerItem.EnchantmentEffectid)(1),
                                          Item.EnchantmentTypes)
                                playerItem.EnchantmentName = GetEffectNameById(playerItem.EnchantmentEffectid, My.Settings.language)
                                If playerItem.EnchantmentType = Item.EnchantmentTypes.ENCHTYPE_ITEM Then
                                    LogAppend("Enchantment type: item!", "ItemParser_LoadItems", False)
                                Else
                                    LogAppend("Enchantment type: spell!", "ItemParser_LoadItems", False)
                                End If
                            End If
                        End If
                        player.ArmorItems.Add(playerItem)
                        Select Case playerItem.Slot
                            Case 15, 16
                                LoadWeaponType(playerItem.Id, setId, account)
                        End Select
                    Next
                End If
            Catch ex As Exception
                LogAppend("Something went wrong! -> Exception is: ###START###" & ex.ToString() & "###END###",
                          "ItemParser_LoadItems", False, True)
            End Try
        End Sub
    End Class
End Namespace