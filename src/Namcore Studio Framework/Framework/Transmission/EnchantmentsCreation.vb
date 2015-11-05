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
'*      /Filename:      EnchantmentsCreation
'*      /Description:   Includes functions for setting enchantments of an equipped item of
'*                      a specific character
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports libnc.Provider
Imports NCFramework.Framework.Database
Imports NCFramework.Framework.Modules

Namespace Framework.Transmission
    Public Class EnchantmentsCreation
        '// Declaration
        Private _locEnchString As String
        '// Declaration

        Public Sub SetItemEnchantments(playerCharacter As Character, itm As Item, itmGuid As Integer,
                                       core As Modules.Core, dbstruc As DbStructure)
            _locEnchString = ""
            Select Case core
                Case Modules.Core.TRINITY
                    _locEnchString =
                        runSQLCommand_characters_string(
                            "SELECT `" & dbstruc.itmins_enchantments_col(0) & "` FROM `" &
                            dbstruc.item_instance_tbl(0) &
                            "` WHERE `" & dbstruc.itmins_guid_col(0) & "`='" & itmGuid.ToString & "'")
            End Select
            SetGem1(itm, itmGuid, core, dbstruc)
            SetGem2(itm, itmGuid, core, dbstruc)
            SetGem3(itm, itmGuid, core, dbstruc)
            SetEnch(itm, itmGuid, core, dbstruc)
        End Sub

        Private Sub SetGem1(myItem As Item, myItemGuid As Integer, myCore As Modules.Core,
                            myStructure As DbStructure)
            Select Case myCore
                Case Modules.Core.TRINITY
                    If IsDBNull(_locEnchString) Or _locEnchString.Length < 5 Then
                        _locEnchString = "0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 "
                    End If
                    Dim parts() As String = _locEnchString.Split(" "c)
                    If myItem.Socket1Id <> 0 Then
                        If myItem.Socket1Effectid = 0 Then
                            myItem.Socket1Effectid = GetEffectIdByGemId(myItem.Socket1Id)
                        End If
                    End If
                    parts(6) = myItem.Socket1Effectid.ToString()
                    _locEnchString = String.Join(" ", parts)
                    runSQLCommand_characters_string(
                        "UPDATE `" & myStructure.item_instance_tbl(0) & "` SET `" &
                        myStructure.itmins_enchantments_col(0) & "`='" &
                        _locEnchString & "' WHERE `" & myStructure.invent_guid_col(0) & "`='" & myItemGuid.ToString &
                        "'")
            End Select
        End Sub

        Private Sub SetGem2(myItem As Item, myItemGuid As Integer, myCore As Modules.Core,
                            myStructure As DbStructure)
            Select Case myCore
                Case Modules.Core.TRINITY
                    If IsDBNull(_locEnchString) Or _locEnchString.Length < 5 Then
                        _locEnchString = "0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 "
                    End If
                    Dim parts() As String = _locEnchString.Split(" "c)
                    If myItem.Socket2Id <> 0 Then
                        If myItem.Socket2Effectid = 0 Then
                            myItem.Socket2Effectid = GetEffectIdByGemId(myItem.Socket2Id)
                        End If
                    End If
                    parts(9) = myItem.Socket2Effectid.ToString()
                    _locEnchString = String.Join(" ", parts)
                    runSQLCommand_characters_string(
                        "UPDATE `" & myStructure.item_instance_tbl(0) & "` SET `" &
                        myStructure.itmins_enchantments_col(0) & "`='" &
                        _locEnchString & "' WHERE `" & myStructure.invent_guid_col(0) & "`='" & myItemGuid.ToString &
                        "'")
            End Select
        End Sub

        Private Sub SetGem3(myItem As Item, myItemGuid As Integer, myCore As Modules.Core,
                            myStructure As DbStructure)
            Select Case myCore
                Case Modules.Core.TRINITY
                    If IsDBNull(_locEnchString) Or _locEnchString.Length < 5 Then
                        _locEnchString = "0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 "
                    End If
                    Dim parts() As String = _locEnchString.Split(" "c)
                    If myItem.Socket3Id <> 0 Then
                        If myItem.Socket3Effectid = 0 Then
                            myItem.Socket3Effectid = GetEffectIdByGemId(myItem.Socket3Id)
                        End If
                    End If
                    parts(12) = myItem.Socket3Effectid.ToString()
                    _locEnchString = String.Join(" ", parts)
                    runSQLCommand_characters_string(
                        "UPDATE `" & myStructure.item_instance_tbl(0) & "` SET `" &
                        myStructure.itmins_enchantments_col(0) & "`='" &
                        _locEnchString & "' WHERE `" & myStructure.invent_guid_col(0) & "`='" & myItemGuid.ToString &
                        "'")
            End Select
        End Sub

        Private Sub SetEnch(myItem As Item, myItemGuid As Integer, myCore As Modules.Core,
                            myStructure As DbStructure)
            Select Case myCore
                Case Modules.Core.TRINITY
                    If IsDBNull(_locEnchString) Or _locEnchString.Length < 5 Then
                        _locEnchString = "0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 "
                    End If
                    Dim parts() As String = _locEnchString.Split(" "c)
                    If myItem.EnchantmentId <> 0 Then
                        If myItem.EnchantmentEffectid = 0 Then
                            If myItem.EnchantmentType = 1 Then
                                myItem.EnchantmentEffectid = GetEffectIdBySpellId(myItem.EnchantmentId,
                                                                                  GlobalVariables.targetExpansion)
                            Else
                                myItem.EnchantmentEffectid =
                                    GetEffectIdBySpellId(GetItemSpellIdByItemId(myItem.EnchantmentId),
                                                         GlobalVariables.targetExpansion)
                            End If
                        End If
                    End If
                    parts(0) = myItem.EnchantmentEffectid.ToString()
                    _locEnchString = String.Join(" ", parts)
                    runSQLCommand_characters_string(
                        "UPDATE `" & myStructure.item_instance_tbl(0) & "` SET `" &
                        myStructure.itmins_enchantments_col(0) & "`='" &
                        _locEnchString & "' WHERE `" & myStructure.invent_guid_col(0) & "`='" & myItemGuid.ToString &
                        "'")
            End Select
        End Sub
    End Class
End Namespace