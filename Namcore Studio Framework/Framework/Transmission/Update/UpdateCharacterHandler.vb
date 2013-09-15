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
'*      /Filename:      UpdateCharacterHandler
'*      /Description:   Handles character update requests
'++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.GlobalVariables


Public Class UpdateCharacterHandler
    Public Sub UpdateCharacter(ByVal ComparePlayer As Character, ByVal NewPlayer As Character)
        LogAppend("Updating character " & ComparePlayer.Name, "UpdateCharacterHandler_UpdateCharacter", True)
        If GlobalConnection.State = ConnectionState.Closed Then GlobalConnection.Open()
        If GlobalConnection_Realm.State = ConnectionState.Closed Then GlobalConnection_Realm.Open()
        forceTargetConnectionUsage = False
        If Not NewPlayer.AccountId = ComparePlayer.AccountId Then
            '// Account changed
            Select Case sourceCore
                Case "trinity"
                    runSQLCommand_characters_string("UPDATE `" & sourceStructure.character_tbl(0) & "` SET `" & sourceStructure.char_accountId_col(0) &
                                                    "`='" & NewPlayer.AccountId.ToString() & "' WHERE `" & sourceStructure.char_guid_col(0) & "`='" & NewPlayer.Guid.ToString() & "'")
                Case Else
                    runSQLCommand_characters_string("UPDATE `" & sourceStructure.character_tbl(0) & "` SET `" & sourceStructure.char_accountId_col(0) &
                                                    "`='" & NewPlayer.AccountId.ToString() & "' WHERE `" & sourceStructure.char_guid_col(0) & "`='" & NewPlayer.Guid.ToString() & "'")
            End Select

        End If
        If Not NewPlayer.Name = ComparePlayer.Name Then
            '// Name changed
            runSQLCommand_characters_string("UPDATE `" & sourceStructure.character_tbl(0) & "` SET `" & sourceStructure.char_name_col(0) &
                                                  "`='" & NewPlayer.Name & "' WHERE `" & sourceStructure.char_guid_col(0) & "`='" & NewPlayer.Guid.ToString() & "'")
        End If
        If Not NewPlayer.Race = ComparePlayer.Race Then
            '// Race changed
            runSQLCommand_characters_string("UPDATE `" & sourceStructure.character_tbl(0) & "` SET `" & sourceStructure.char_race_col(0) &
                                                  "`='" & NewPlayer.Race.ToString() & "' WHERE `" & sourceStructure.char_guid_col(0) & "`='" & NewPlayer.Guid.ToString() & "'")
        End If
        If Not NewPlayer.Cclass = ComparePlayer.Cclass Then
            '// Class changed
            runSQLCommand_characters_string("UPDATE `" & sourceStructure.character_tbl(0) & "` SET `" & sourceStructure.char_class_col(0) &
                                                  "`='" & NewPlayer.cclass.ToString() & "' WHERE `" & sourceStructure.char_guid_col(0) & "`='" & NewPlayer.Guid.ToString() & "'")
        End If
        If Not NewPlayer.Gender = ComparePlayer.Gender Then
            '// Gender changed
            runSQLCommand_characters_string("UPDATE `" & sourceStructure.character_tbl(0) & "` SET `" & sourceStructure.char_gender_col(0) &
                                                  "`='" & NewPlayer.Gender.ToString() & "' WHERE `" & sourceStructure.char_guid_col(0) & "`='" & NewPlayer.Guid.ToString() & "'")
        End If
        If Not NewPlayer.Level = ComparePlayer.Level Then
            '// Level changed
            runSQLCommand_characters_string("UPDATE `" & sourceStructure.character_tbl(0) & "` SET `" & sourceStructure.char_level_col(0) &
                                                  "`='" & NewPlayer.Level.ToString() & "' WHERE `" & sourceStructure.char_guid_col(0) & "`='" & NewPlayer.Guid.ToString() & "'")
        End If
        Dim itm2create As New List(Of Item)
        Dim itmsEnchChanged As New List(Of Item)
        For Each armorItm As Item In NewPlayer.ArmorItems
            Dim entry As Item = ComparePlayer.ArmorItems.Find(Function(item) item.id = armorItm.id)
            If entry Is Nothing Then
                '// Item needs to be created
                itm2create.Add(entry)
            Else
                '// Item already exists: Check if gems/enchantments changed
                If Not armorItm.enchantment_id = entry.enchantment_id Then
                    itmsEnchChanged.Add(entry)
                Else
                    If Not armorItm.socket1_id = entry.socket1_id Or Not armorItm.socket2_id = entry.socket2_id Or Not armorItm.socket3_id = entry.socket3_id Then
                        itmsEnchChanged.Add(entry)
                    End If
                End If

            End If
        Next
        Dim itm2delete As List(Of Item) = (From armorItm In ComparePlayer.ArmorItems Where GetCharacterArmorItem(NewPlayer, armorItm.slot, True) Is Nothing).ToList()
        If itm2delete Is Nothing Then itm2delete = New List(Of Item)()
        If itm2create.Count > 0 Or itmsEnchChanged.Count > 0 Or itm2delete.Count > 0 Then
            Dim m_updateArmor As New UpdateArmorHandler
            m_updateArmor.UpdateArmor(ComparePlayer, NewPlayer, itm2create, itm2delete, itmsEnchChanged)
        End If
    End Sub
End Class
