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
        Dim itemguid As Integer
        Dim slotname As String
        Dim itementry As Integer
        Dim itemslot As Integer
        Dim loopcounter As Integer = 0
        Dim slot_st
        Do
            Try
              
                If itemslot > 18 Then
                    loopcounter += 1
                    Continue Do
                End If
                Select Case itemslot
                    Case 0
                        slotname = "head"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 1
                        slotname = "neck"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 2
                        slotname = "shoulder"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 3
                        slotname = "shirt"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 4
                        slotname = "chest"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 5
                        slotname = "waist"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 6
                        slotname = "legs"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 7
                        slotname = "feet"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 8
                        slotname = "wrists"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 9
                        slotname = "hands"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 10
                        slotname = "finger1"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 11
                        slotname = "finger2"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 12
                        slotname = "trinket1"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 13
                        slotname = "trinket2"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 14
                        slotname = "back"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 15
                        slotname = "main"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 16
                        slotname = "off"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 17
                        'slot 17 has been removed as of patch 5.0
                    Case 18
                        slotname = "tabard"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case Else : End Select

            Catch ex As Exception
                LogAppend("Something went wrong! -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterArmorHandler_loadAtArcemu", False, True)
                loopcounter += 1
                Continue Do
            End Try
            loopcounter += 1
        Loop Until loopcounter = entrycount

    End Sub
    Private Function getItemInfo(ByVal slot As Integer, ByVal slotname As String, ByVal sourceCode As String) As Item
        Dim relevantItemContext As String = splitString(sourceCode, "<div data-id=""" & slot.ToString & """ data-type=", "</div>")
        Dim charItem As New Item
        '//Loading ID
        charItem.id = TryInt(splitString(relevantItemContext, "/item/", """ class=""item"""))
        If charItem.id = Nothing Then Return Nothing '//Item ID not found
        '//Loading Name
        charItem.name = splitString(relevantItemContext, "<span class=""name-shadow"">", "</span>")
        charItem.image = LoadImageFromUrl(splitString(relevantItemContext, "<img src=""", """ alt"))
        '//Loading Sockets
        Dim socketContext As String
        If relevantItemContext.Contains("<span class=""sockets"">") Then
            'sockets available
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
            Dim enchantContext As String = splitString(relevantItemContext, "<span class=""enchant color", "</span>")
            charItem.enchantment_id = TryInt(splitString(enchantContext, "/item/", """>"))
            charItem.enchantment_name = splitString(enchantContext, "/item/" & charItem.enchantment_id.ToString & """>", "</a>")
            charItem.enchantment_type = 2
        End If
    End Function
End Class
