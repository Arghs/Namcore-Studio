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
'*      /Filename:      CharacterItemStatsHandler
'*      /Description:   Contains functions for extracting information about the enchantments 
'*                      of all items in the inventory of a specific character
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Imports Namcore_Studio.EventLogging
Imports Namcore_Studio.Basics
Imports Namcore_Studio.GlobalVariables
Imports Namcore_Studio.CommandHandler
Imports Namcore_Studio.Conversions
Public Class CharacterItemStatsHandler
    Public Sub GetItemStats(ByVal CItemguid As Integer, ByRef Itm As Item, ByRef player As Character, ByVal setId As Integer)
        LogAppend("Loading character ItemStats for item: " & CItemguid.ToString() & " and setId: " & setId, "CharacterItemStatssHandler_GetItemStats", True)
        Select Case sourceCore
            Case "arcemu"
                loadAtArcemu(CItemguid, setId, Itm, player)
            Case "trinity"
                loadAtTrinity(CItemguid, setId, Itm, player)
            Case "trinitytbc"
                loadAtTrinityTBC(CItemguid, setId, Itm, player)
            Case "mangos"
                loadAtMangos(CItemguid, setId, Itm, player)
            Case Else

        End Select

    End Sub
    Private Sub loadAtArcemu(ByVal item As Integer, ByVal tar_setId As Integer, ByRef itm As Item, ByRef player As Character)
        LogAppend("Loading ItemStats @loadAtArcemu", "CharacterItemStatsHandler_loadAtArcemu", False)
        itm.enchstring = runSQLCommand_characters_string("SELECT " & sourceStructure.itmins_enchantments_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " & sourceStructure.itmins_guid_col(0) & "='" & item.ToString & "'")
        SetCharacterArmorItem(player, itm)
        SetCharacterSet(tar_setId, player)
    End Sub
    Private Sub loadAtTrinity(ByVal item As Integer, ByVal tar_setId As Integer, ByRef itm As Item, ByRef player As Character)
        LogAppend("Loading ItemStats @loadAtTrinity", "CharacterItemStatsHandler_loadAtTrinity", False)
        itm.enchstring = runSQLCommand_characters_string("SELECT " & sourceStructure.itmins_enchantments_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " & sourceStructure.itmins_guid_col(0) & "='" & item.ToString & "'")
        SetCharacterArmorItem(player, itm)
        SetCharacterSet(tar_setId, player)
    End Sub
    Private Sub loadAtTrinityTBC(ByVal item As Integer, ByVal tar_setId As Integer, ByRef itm As Item, ByRef player As Character)

    End Sub
    Private Sub loadAtMangos(ByVal item As Integer, ByVal tar_setId As Integer, ByRef itm As Item, ByRef player As Character)
        LogAppend("Loading character ItemStats @loadAtMangos", "CharacterItemStatsHandler_loadAtMangos", False)
        itm.enchstring = runSQLCommand_characters_string("SELECT `" & sourceStructure.itmins_data_col(0) & "` FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " & sourceStructure.itmins_guid_col(0) & "='" & item.ToString & "'")
        SetCharacterArmorItem(player, itm)
        SetCharacterSet(tar_setId, player)
    End Sub
End Class
