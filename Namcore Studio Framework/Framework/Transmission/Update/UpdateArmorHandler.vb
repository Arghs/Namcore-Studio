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
'*      /Filename:      UpdateArmorHandler
'*      /Description:   Handles character armor update requests
'++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.GlobalVariables
Public Class UpdateArmorHandler

    Public Sub UpdateArmor(ByVal player As Character, ByVal modPlayer As Character, ByVal createItm As List(Of Item), ByVal deleteItm As List(Of Item), enchItm As List(Of Item))
        LogAppend("Updating character armor", "UpdateArmorHandler_UpdateArmor", True)
        For Each armorItm As Item In createItm
            CreateItem(modPlayer, armorItm)
        Next
        For Each armorItm As Item In deleteItm
            DeleteItem(modPlayer, armorItm)
        Next
        Dim m_enchCreator As New EnchantmentsCreation
        For Each armorItm As Item In enchItm
            Select Case sourceCore
                Case "trinity"
                    Dim itmguid As Integer = TryInt(runSQLCommand_characters_string("SELECT `" & sourceStructure.invent_item_col(0) & "` FROM `" & sourceStructure.character_inventory_tbl(0) &
                                                                                    "` WHERE `" & sourceStructure.invent_guid_col(0) & "`='" & player.Guid.ToString() & "' AND `" &
                                                                                    sourceStructure.invent_slot_col(0) & "`='" & armorItm.slot.ToString() & "'"))
                    If Not itmguid = 0 Then
                        m_enchCreator.SetItemEnchantments(0, armorItm, itmguid, sourceCore, sourceStructure)
                    End If
            End Select

        Next
    End Sub
    Private Sub CreateItem(ByVal player As Character, ByVal itm2Add As Item)
        Select Case sourceCore
            Case "trinity"
                Dim newItemGuid As Integer = TryInt(runSQLCommand_characters_string("SELECT " & sourceStructure.itmins_guid_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " &
                                                                            sourceStructure.itmins_guid_col(0) & "=(SELECT MAX(" & sourceStructure.itmins_guid_col(0) & ") FROM " & sourceStructure.item_instance_tbl(0) & ")")) + 1
                runSQLCommand_characters_string("INSERT INTO " & sourceStructure.item_instance_tbl(0) & " ( " & sourceStructure.itmins_guid_col(0) & ", " & sourceStructure.itmins_itemEntry_col(0) & ", " &
                                           sourceStructure.itmins_ownerGuid_col(0) & ", " & sourceStructure.itmins_count_col(0) & ", " & sourceStructure.itmins_enchantments_col(0) &
                                           ", " & sourceStructure.itmins_durability_col(0) & " ) VALUES ( '" & newItemGuid.ToString() & "', '" & itm2Add.id & "', '" & player.Guid.ToString() &
                                           "', '1', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )")
                If ReturnResultCount("SELECT * FROM " & sourceStructure.character_inventory_tbl(0) & " WHERE " & sourceStructure.invent_guid_col(0) & "='" & player.Guid.ToString() & "' AND " &
                               sourceStructure.invent_slot_col(0) & " = '" & itm2Add.slot.ToString() & "'") > 0 Then
                    '// Item in this slot already exists: Deleting it
                    runSQLCommand_characters_string("DELETE FROM " & sourceStructure.character_inventory_tbl(0) & " WHERE " & sourceStructure.invent_guid_col(0) & " = '" & player.Guid.ToString() &
                                                     "' AND " & sourceStructure.invent_slot_col(0) & " = '" & itm2Add.slot.ToString() & "'")
                    runSQLCommand_characters_string("INSERT INTO " & sourceStructure.character_inventory_tbl(0) & " ( " & sourceStructure.invent_guid_col(0) & ", " & sourceStructure.invent_slot_col(0) &
                                                    ", " & sourceStructure.invent_item_col(0) & " ) VALUES ( '" & player.Guid.ToString() & "', '" & itm2Add.slot.ToString() & "', '" & newItemGuid.ToString() & "' )")
                Else
                    runSQLCommand_characters_string("INSERT INTO " & sourceStructure.character_inventory_tbl(0) & " ( " & sourceStructure.invent_guid_col(0) & ", " & sourceStructure.invent_slot_col(0) & ", " &
                                                    sourceStructure.invent_item_col(0) & " ) VALUES ( '" & player.Guid.ToString() & "', '" & itm2Add.slot.ToString() & "', '" & newItemGuid.ToString() & "' )")
                End If
                Dim m_enchCreator As New EnchantmentsCreation
                m_enchCreator.SetItemEnchantments(0, itm2Add, newItemGuid, targetCore, sourceStructure)
                '// Optional TODO: Set equipment cache

        End Select
    End Sub
    Private Sub DeleteItem(ByVal player As Character, ByVal itm2delete As Item)
        Select Case sourceCore
            Case "trinity"
                runSQLCommand_characters_string("DELETE FROM " & sourceStructure.character_inventory_tbl(0) & " WHERE " & sourceStructure.invent_guid_col(0) & " = '" & player.Guid.ToString() &
                                                    "' AND " & sourceStructure.invent_slot_col(0) & " = '" & itm2delete.slot.ToString() & "'")
        End Select

    End Sub

End Class
