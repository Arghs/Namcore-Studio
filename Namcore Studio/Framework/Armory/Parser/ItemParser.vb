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
'*      /Filename:      ItemParser
'*      /Description:   Contains functions for loading character items from wow armory
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Imports Namcore_Studio.EventLogging
Imports Namcore_Studio.Conversions
Imports Namcore_Studio.SpellItem_Information
Imports Namcore_Studio.Basics
Public Class ItemParser
    Public Shared Sub loadItems(ByVal source As String, ByVal setId As Integer)
        Dim slotname As String
        Dim itemslot As Integer
        Do
            Try
              If itemslot > 18 Then
                    Exit Do
                End If
                Select Case itemslot
                    Case 0
                        slotname = "head"
                        Dim charItem As Item = getItemInfo(itemslot, slotname, source)
                        If Not charItem Is Nothing Then SetTCI_Item(getItemInfo(itemslot, slotname, source), setId)
                    Case 1
                        slotname = "neck"
                        Dim charItem As Item = getItemInfo(itemslot, slotname, source)
                        If Not charItem Is Nothing Then SetTCI_Item(getItemInfo(itemslot, slotname, source), setId)
                    Case 2
                        slotname = "shoulder"
                        Dim charItem As Item = getItemInfo(itemslot, slotname, source)
                        If Not charItem Is Nothing Then SetTCI_Item(getItemInfo(itemslot, slotname, source), setId)
                    Case 3
                        slotname = "shirt"
                        Dim charItem As Item = getItemInfo(itemslot, slotname, source)
                        If Not charItem Is Nothing Then SetTCI_Item(getItemInfo(itemslot, slotname, source), setId)
                    Case 4
                        slotname = "chest"
                        Dim charItem As Item = getItemInfo(itemslot, slotname, source)
                        If Not charItem Is Nothing Then SetTCI_Item(getItemInfo(itemslot, slotname, source), setId)
                    Case 5
                        slotname = "waist"
                        Dim charItem As Item = getItemInfo(itemslot, slotname, source)
                        If Not charItem Is Nothing Then SetTCI_Item(getItemInfo(itemslot, slotname, source), setId)
                    Case 6
                        slotname = "legs"
                        Dim charItem As Item = getItemInfo(itemslot, slotname, source)
                        If Not charItem Is Nothing Then SetTCI_Item(getItemInfo(itemslot, slotname, source), setId)
                    Case 7
                        slotname = "feet"
                        Dim charItem As Item = getItemInfo(itemslot, slotname, source)
                        If Not charItem Is Nothing Then SetTCI_Item(getItemInfo(itemslot, slotname, source), setId)
                    Case 8
                        slotname = "wrists"
                        Dim charItem As Item = getItemInfo(itemslot, slotname, source)
                        If Not charItem Is Nothing Then SetTCI_Item(getItemInfo(itemslot, slotname, source), setId)
                    Case 9
                        slotname = "hands"
                        Dim charItem As Item = getItemInfo(itemslot, slotname, source)
                        If Not charItem Is Nothing Then SetTCI_Item(getItemInfo(itemslot, slotname, source), setId)
                    Case 10
                        slotname = "finger1"
                        Dim charItem As Item = getItemInfo(itemslot, slotname, source)
                        If Not charItem Is Nothing Then SetTCI_Item(getItemInfo(itemslot, slotname, source), setId)
                    Case 11
                        slotname = "finger2"
                        Dim charItem As Item = getItemInfo(itemslot, slotname, source)
                        If Not charItem Is Nothing Then SetTCI_Item(getItemInfo(itemslot, slotname, source), setId)
                    Case 12
                        slotname = "trinket1"
                        Dim charItem As Item = getItemInfo(itemslot, slotname, source)
                        If Not charItem Is Nothing Then SetTCI_Item(getItemInfo(itemslot, slotname, source), setId)
                    Case 13
                        slotname = "trinket2"
                        Dim charItem As Item = getItemInfo(itemslot, slotname, source)
                        If Not charItem Is Nothing Then SetTCI_Item(getItemInfo(itemslot, slotname, source), setId)
                    Case 14
                        slotname = "back"
                        Dim charItem As Item = getItemInfo(itemslot, slotname, source)
                        If Not charItem Is Nothing Then SetTCI_Item(getItemInfo(itemslot, slotname, source), setId)
                    Case 15
                        slotname = "main"
                        Dim charItem As Item = getItemInfo(itemslot, slotname, source)
                        If Not charItem Is Nothing Then
                            SetTCI_Item(getItemInfo(itemslot, slotname, source), setId)
                            LoadWeaponType(charItem.id, setId)
                        End If
                    Case 16
                        slotname = "off"
                        Dim charItem As Item = getItemInfo(itemslot, slotname, source)
                        If Not charItem Is Nothing Then
                            SetTCI_Item(getItemInfo(itemslot, slotname, source), setId)
                            LoadWeaponType(charItem.id, setId)
                        End If
                    Case 17
                        'slot 17 has been removed as of patch 5.0
                    Case 18
                        slotname = "tabard"
                        Dim charItem As Item = getItemInfo(itemslot, slotname, source)
                        If Not charItem Is Nothing Then SetTCI_Item(getItemInfo(itemslot, slotname, source), setId)
                End Select
            Catch ex As Exception
                LogAppend("Something went wrong! -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterArmorHandler_loadAtArcemu", False, True)
                itemslot += 1
                Continue Do
            End Try
            itemslot += 1
        Loop Until itemslot = 19
    End Sub
    Private Shared Function GetItemInfo(ByVal slot As Integer, ByVal slotname As String, ByVal sourceCode As String) As Item
        Dim endString As String
        If slot = 16 Then
            endString = "<script type=""text/javascript"">"
        Else
            endString = "<div data-id"
        End If
        Dim relevantItemContext As String = splitString(sourceCode, "<div data-id=""" & slot.ToString & """ data-type", endString)
        Dim charItem As New Item
        '//Loading main Item Info
        charItem.id = TryInt(splitString(relevantItemContext, "/item/", """ class=""item"""))
        charItem.slotname = slotname
        If charItem.id = Nothing Then Return Nothing '//Item ID not found
        charItem.name = splitString(relevantItemContext, "<span class=""name-shadow"">", "</span>")
        charItem.image = LoadImageFromUrl(splitString(relevantItemContext, "<img src=""", """ alt"))
        charItem.rarity = TryInt(splitString(relevantItemContext, "item-quality-", """ style="))
        Dim socketContext As String
        If relevantItemContext.Contains("<span class=""sockets"">") Then
            '//sockets active
            socketContext = splitString(sourceCode & "</div>", "<span class=""sockets"">", "</div>")
            Dim socketCount As Integer = UBound(socketContext.Split("socket-"))
            Dim oneSocketContext As String = splitString(socketContext, "<span class=""icon-socket", "<span class=""frame"">")
            charItem.socket1_id = TryInt(splitString(oneSocketContext, "/item/", """ class="))
            charItem.socket1_img = LoadImageFromUrl(splitString(oneSocketContext, "<img src=""", """ alt"))
            charItem.socket1_name = GetGemEffectName(charItem.socket1_id)
            If socketCount > 1 Then
                socketContext = socketContext.Replace(oneSocketContext, Nothing)
                oneSocketContext = splitString(socketContext, "<span class=""icon-socket", "<span class=""frame"">")
                charItem.socket2_id = TryInt(splitString(oneSocketContext, "/item/", """ class="))
                charItem.socket2_img = LoadImageFromUrl(splitString(oneSocketContext, "<img src=""", """ alt"))
                charItem.socket2_name = GetGemEffectName(charItem.socket2_id)
                If socketCount > 2 Then
                    socketContext = socketContext.Replace(oneSocketContext, Nothing)
                    oneSocketContext = splitString(socketContext, "<span class=""icon-socket", "<span class=""frame"">")
                    charItem.socket3_id = TryInt(splitString(oneSocketContext, "/item/", """ class="))
                    charItem.socket3_img = LoadImageFromUrl(splitString(oneSocketContext, "<img src=""", """ alt"))
                    charItem.socket3_name = GetGemEffectName(charItem.socket3_id)
                End If
            End If
        End If
        If relevantItemContext.Contains("<span class=""enchant-") Then
            '//enchantment active
            Dim enchantContext As String = splitString(relevantItemContext, "<span class=""enchant color", "</span>")
            If enchantContext.Contains("data-spell=") Then
                '//enchantment type: spell
                charItem.enchantment_id = TryInt(splitString(enchantContext, """ data-spell=""", """>"))
                charItem.enchantment_name = splitString(enchantContext, """ data-spell=""" & charItem.enchantment_id.ToString & """>", "</span>")
                charItem.enchantment_type = 1
            Else
                '//enchantment type: item
                charItem.enchantment_id = TryInt(splitString(enchantContext, "/item/", """>"))
                charItem.enchantment_name = splitString(enchantContext, "/item/" & charItem.enchantment_id.ToString & """>", "</a>")
                charItem.enchantment_type = 2
            End If
        End If
        Return charItem
    End Function
End Class
