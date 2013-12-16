'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013 NamCore Studio <https://github.com/megasus/Namcore-Studio>
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
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Database
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Modules

Namespace Framework.Core
    Public Class CharacterItemStatsHandler
        Public Sub GetItemStats(ByVal cItemguid As Integer, ByRef itm As Item, ByRef player As Character,
                                ByVal setId As Integer, ByVal account As Account)
            LogAppend("Loading character ItemStats for item: " & cItemguid.ToString() & " and setId: " & setId,
                      "CharacterItemStatssHandler_GetItemStats", True)
            Select Case GlobalVariables.sourceCore
                Case "arcemu"
                    LoadAtArcemu(cItemguid, setId, itm, player, account)
                Case "trinity"
                    LoadAtTrinity(cItemguid, setId, itm, player, account)
                Case "trinitytbc"
                    'todo  LoadAtTrinityTBC(CItemguid, setId, Itm, player)
                Case "mangos"
                    LoadAtMangos(cItemguid, setId, itm, player, account)
            End Select
        End Sub

        Private Sub LoadAtArcemu(ByVal item As Integer, ByVal tarSetId As Integer, ByRef itm As Item,
                                 ByRef player As Character, ByVal account As Account)
            LogAppend("Loading ItemStats @LoadAtArcemu", "CharacterItemStatsHandler_LoadAtArcemu", False)
            itm.Enchstring =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.itmins_enchantments_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.item_instance_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.itmins_guid_col(0) & "='" & item.ToString & "'")
            SetCharacterArmorItem(player, itm)
            SetCharacterSet(tarSetId, player, account)
        End Sub

        Private Sub LoadAtTrinity(ByVal item As Integer, ByVal tarSetId As Integer, ByRef itm As Item,
                                  ByRef player As Character, ByVal account As Account)
            LogAppend("Loading ItemStats @LoadAtTrinity", "CharacterItemStatsHandler_LoadAtTrinity", False)
            itm.Enchstring =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.itmins_enchantments_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.item_instance_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.itmins_guid_col(0) & "='" & item.ToString & "'")
            SetCharacterArmorItem(player, itm)
            SetCharacterSet(tarSetId, player, account)
        End Sub

        Private Sub LoadAtMangos(ByVal item As Integer, ByVal tarSetId As Integer, ByRef itm As Item,
                                 ByRef player As Character, ByVal account As Account)
            LogAppend("Loading character ItemStats @LoadAtMangos", "CharacterItemStatsHandler_LoadAtMangos", False)
            itm.Enchstring =
                runSQLCommand_characters_string(
                    "SELECT `" & GlobalVariables.sourceStructure.itmins_data_col(0) & "` FROM " &
                    GlobalVariables.sourceStructure.item_instance_tbl(0) & " WHERE " &
                    GlobalVariables.sourceStructure.itmins_guid_col(0) & "='" & item.ToString & "'")
            SetCharacterArmorItem(player, itm)
            SetCharacterSet(tarSetId, player, account)
        End Sub
    End Class
End Namespace