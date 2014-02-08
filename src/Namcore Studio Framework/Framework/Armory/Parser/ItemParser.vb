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
'*      /Filename:      ItemParser
'*      /Description:   Contains functions for loading character items from wow armory
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Modules
Imports libnc.Provider
Imports NCFramework.Framework.Extension.Special

Namespace Framework.Armory.Parser
    Public Class ItemParser
        '// Declaration
        Private _lastStamp As Integer = 0
        '// Declaration

        Public Sub LoadItems(ByVal source As String, ByVal setId As Integer, ByVal account As Account)
            Dim slotname As String
            Dim itemslot As Integer = 0
            '// Retrieving character
            Dim player As Character = GetCharacterSetBySetId(setId, account)
            LogAppend("Loading character items", "ItemParser_loadItems", True)
            Do
                Try
                    If itemslot = 17 Then
                        itemslot += 1
                        Continue Do
                    End If
                    If itemslot > 18 Then '// item slot 19+ not existent
                        Exit Do
                    End If
                    '// Loading item + info for each slot and add them to character
                    LogAppend("Now loading info for slot " & itemslot.ToString(), "ItemParser_loadItems", True)
                    slotname = GetItemSlotNameBySlotId(itemslot)
                    Dim charItem As Item = GetItemInfo(itemslot, slotname, source)
                    If Not charItem Is Nothing Then
                        player.ArmorItems.Add(charItem)
                        If itemslot = 15 Or itemslot = 16 Then
                            LoadWeaponType(charItem.Id, setId, account)
                        End If
                    End If
                Catch ex As Exception
                    LogAppend("Something went wrong! -> Exception is: ###START###" & ex.ToString() & "###END###",
                              "ItemParser_loadItems", False, True)
                Finally
                    itemslot += 1
                End Try
            Loop Until itemslot = 19
        End Sub

        Private Function GetItemInfo(ByVal slot As Integer, ByVal slotname As String, ByVal sourceCode As String) _
            As Item
            LogAppend("Loading item information (slot: " & slot.ToString() & ")", "ItemParser_loadItems", True)
            Dim endString As String
            If slot = 16 Then
                endString = "<script type=""text/javascript"">"
            Else
                endString = "<div data-id"
            End If
            Dim relevantItemContext As String = SplitString(sourceCode,
                                                            "<div data-id=""" & slot.ToString & """ data-type",
                                                            endString)
            If relevantItemContext Is Nothing Then
                LogAppend("RelevantItemContext is nothing - prevent NullReferenceException - Item not found",
                          "ItemParser_loadItems", False, True)
                Return New Item
            End If
            If Not relevantItemContext.Contains("/item/") Then Return Nothing '//Not item
            Dim charItem As New Item
            '// Loading main Item Info
            charItem.Id = TryInt(SplitString(relevantItemContext, "/item/", """ class=""item"""))
            Dim x As DateTime = Date.Now
            If _lastStamp = 0 Then
                _lastStamp = x.ToTimeStamp()
            Else
                _lastStamp += 1
            End If
            charItem.Guid = _lastStamp
            charItem.Slotname = slotname
            charItem.Slot = slot
            If charItem.Id = Nothing Then
                LogAppend("Item Id is 0 - Failed to load", "ItemParser_loadItems", False, True)
                Return New Item
            End If '// Item ID not found
            charItem.Name = SplitString(relevantItemContext, "<span class=""name-shadow"">", "</span>")
            If GlobalVariables.offlineExtension = True Then _
                charItem.Image = GetItemIconByDisplayId(GetDisplayIdByItemId(charItem.Id), GlobalVariables.GlobalWebClient) Else 
            charItem.Image = LoadImageFromUrl(SplitString(relevantItemContext, "<img src=""", """ alt"))
            charItem.Rarity = TryInt(SplitString(relevantItemContext, "item-quality-", """ style="))
            Dim socketContext As String
            If relevantItemContext.Contains("<span class=""sockets"">") Then
                LogAppend("Item gems active!", "ItemParser_loadItems", False)
                '// Gems active
                socketContext = SplitString(relevantItemContext & "</div>", "<span class=""sockets"">", "</div>")
                Dim socketCount As Integer = UBound(Split(socketContext, "socket-"))
                LogAppend("socketCount: " & socketCount.ToString(), "ItemParser_loadItems", False)
                Dim oneSocketContext As String = SplitString(socketContext, "<span class=""icon-socket",
                                                             "<span class=""frame"">")
                If Not oneSocketContext.Length <= 49 Then
                    charItem.Socket1Id = TryInt(SplitString(oneSocketContext, "/item/", """ class="))
                    If GlobalVariables.offlineExtension = True Then _
                        charItem.Socket1Pic = GetItemIconByDisplayId(GetDisplayIdByItemId(charItem.Socket1Id), GlobalVariables.GlobalWebClient) Else 
                    charItem.Socket1Pic = LoadImageFromUrl(SplitString(oneSocketContext,
                                                                       "<img src=""", """ alt="""))
                    charItem.Socket1Effectid = GetEffectIdByGemId(charItem.Socket1Id)
                    charItem.Socket1Name = GetEffectNameById(charItem.Socket1Effectid, My.Settings.language)
                Else
                    LogAppend("oneSocketContext length is less then 49/ is: " & oneSocketContext.Length,
                              "ItemParser_loadItems", False, True)
                End If
                If socketCount > 1 Then
                    socketContext = Replace(socketContext,
                                            "<span class=""icon-socket" & oneSocketContext & "<span class=""frame"">",
                                            Nothing, , 1)
                    oneSocketContext = SplitString(socketContext, "<span class=""icon-socket", "<span class=""frame"">")
                    charItem.Socket2Id = TryInt(SplitString(oneSocketContext, "/item/", """ class="))
                    If GlobalVariables.offlineExtension = True Then _
                        charItem.Socket2Pic = GetItemIconByDisplayId(GetDisplayIdByItemId(charItem.Socket2Id), GlobalVariables.GlobalWebClient) Else 
                    charItem.Socket2Pic = LoadImageFromUrl(SplitString(oneSocketContext,
                                                                       "<img src=""", """ alt="""))
                    charItem.Socket2Effectid = GetEffectIdByGemId(charItem.Socket2Id)
                    charItem.Socket2Name = GetEffectNameById(charItem.Socket2Effectid, My.Settings.language)
                    If socketCount > 2 Then
                        socketContext = Replace(socketContext,
                                                "<span class=""icon-socket" & oneSocketContext &
                                                "<span class=""frame"">",
                                                Nothing, , 1)
                        oneSocketContext = SplitString(socketContext, "<span class=""icon-socket",
                                                       "<span class=""frame"">")
                        charItem.Socket3Id = TryInt(SplitString(oneSocketContext, "/item/", """ class="))
                        If GlobalVariables.offlineExtension = True Then _
                            charItem.Socket3Pic = GetItemIconByDisplayId(GetDisplayIdByItemId(charItem.Socket3Id), GlobalVariables.GlobalWebClient) _
                            Else 
                        charItem.Socket3Pic = LoadImageFromUrl(SplitString(oneSocketContext,
                                                                           "<img src=""", """ alt="""))
                        charItem.Socket3Effectid = GetEffectIdByGemId(charItem.Socket3Id)
                        charItem.Socket3Name = GetEffectNameById(charItem.Socket3Effectid, My.Settings.language)
                    End If
                End If
            Else
                LogAppend("No gems found!?", "ItemParser_loadItems", False)
            End If
            If relevantItemContext.Contains("<span class=""enchant-") Then
                LogAppend("Enchantment active!", "ItemParser_loadItems", False)
                '// Enchantment active
                Dim enchantContext As String = SplitString(relevantItemContext, " class=""enchant color", "</span>")
                If enchantContext.Contains("data-spell=") Then
                    LogAppend("Enchantment type: spell!", "ItemParser_loadItems", False)
                    '//enchantment type: spell
                    charItem.EnchantmentId = TryInt(SplitString(enchantContext, """ data-spell=""", """>"))
                    charItem.EnchantmentName = SplitString(enchantContext,
                                                           """ data-spell=""" & charItem.EnchantmentId.ToString & """>",
                                                           "</span>")
                    charItem.EnchantmentType = 1
                    charItem.EnchantmentEffectid = GetEffectIdBySpellId(charItem.EnchantmentId)
                Else
                    LogAppend("Enchantment type: item!", "ItemParser_loadItems", False)
                    '//enchantment type: item
                    charItem.EnchantmentId = TryInt(SplitString(enchantContext, "/item/", """>"))
                    charItem.EnchantmentName = SplitString(enchantContext,
                                                           "/item/" & charItem.EnchantmentId.ToString & """>", "</a>")
                    charItem.EnchantmentType = 2
                    charItem.EnchantmentEffectid = GetEffectIdBySpellId(GetItemSpellIdByItemId(charItem.EnchantmentId))
                End If
            Else
                LogAppend("Enchantment not found!?", "ItemParser_loadItems", False)
            End If
            LogAppend("Returning item!", "ItemParser_loadItems", False)
            Return charItem
        End Function
    End Class
End Namespace