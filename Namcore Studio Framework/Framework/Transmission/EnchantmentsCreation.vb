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
'*      /Filename:      EnchantmentsCreation
'*      /Description:   Includes functions for setting enchantments of an equipped item of
'*                      a specific character
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.Database
Imports NCFramework.Framework.Module

Namespace Framework.Transmission

    Public Class EnchantmentsCreation
        Public Sub SetItemEnchantments(ByVal setId As Integer, ByVal itm As Item, ByVal itmGuid As Integer,
                                       ByVal core As String, ByVal dbstruc As DBStructure)
            SetGem1(itm, itmGuid, core, dbstruc)
            SetGem2(itm, itmGuid, core, dbstruc)
            SetGem3(itm, itmGuid, core, dbstruc)
            SetEnch(itm, itmGuid, core, dbstruc)
        End Sub

        Private Sub SetGem1(ByVal myItem As Item, ByVal myItemGuid As Integer, ByVal myCore As String,
                            ByVal myStructure As DBStructure)
            Select Case myCore
                Case "trinity"
                    If Not myItem.Socket1Id = Nothing Then
                        Dim enchString As String =
                                runSQLCommand_characters_string(
                                    "SELECT `" & myStructure.itmins_enchantments_col(0) & "` FROM `" &
                                    myStructure.item_instance_tbl(0) &
                                    "` WHERE `" & myStructure.itmins_guid_col(0) & "`='" & myItemGuid.ToString & "'")
                        If IsDBNull(enchString) Or enchString.Length < 5 Then
                            enchString = "0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 "
                        End If
                        Dim parts() As String = enchString.Split(" "c)
                        If myItem.Socket1Effectid = Nothing Then
                            'TODO
                        End If
                        parts(6) = myItem.Socket1Effectid.ToString()
                        enchString = String.Join(" ", parts)
                        runSQLCommand_characters_string(
                            "UPDATE `" & myStructure.item_instance_tbl(0) & "` SET `" &
                            myStructure.itmins_enchantments_col(0) & "`='" &
                            enchString & "' WHERE `" & myStructure.invent_guid_col(0) & "`='" & myItemGuid.ToString & "'")
                    End If
            End Select
        End Sub

        Private Sub SetGem2(ByVal myItem As Item, ByVal myItemGuid As Integer, ByVal myCore As String,
                            ByVal myStructure As DBStructure)
            Select Case myCore
                Case "trinity"
                    If Not myItem.Socket2Id = Nothing Then
                        Dim enchString As String =
                                runSQLCommand_characters_string(
                                    "SELECT `" & myStructure.itmins_enchantments_col(0) & "` FROM `" &
                                    myStructure.item_instance_tbl(0) &
                                    "` WHERE `" & myStructure.itmins_guid_col(0) & "`='" & myItemGuid.ToString & "'")
                        If IsDBNull(enchString) Or enchString.Length < 5 Then
                            enchString = "0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 "
                        End If
                        Dim parts() As String = enchString.Split(" "c)
                        If myItem.Socket2Effectid = Nothing Then
                            'TODO
                        End If
                        parts(9) = myItem.Socket2Effectid.ToString()
                        enchString = String.Join(" ", parts)
                        runSQLCommand_characters_string(
                            "UPDATE `" & myStructure.item_instance_tbl(0) & "` SET `" &
                            myStructure.itmins_enchantments_col(0) & "`='" &
                            enchString & "' WHERE `" & myStructure.invent_guid_col(0) & "`='" & myItemGuid.ToString & "'")
                    End If
            End Select
        End Sub

        Private Sub SetGem3(ByVal myItem As Item, ByVal myItemGuid As Integer, ByVal myCore As String,
                            ByVal myStructure As DBStructure)
            Select Case myCore
                Case "trinity"
                    If Not myItem.Socket3Id = Nothing Then
                        Dim enchString As String =
                                runSQLCommand_characters_string(
                                    "SELECT `" & myStructure.itmins_enchantments_col(0) & "` FROM `" &
                                    myStructure.item_instance_tbl(0) &
                                    "` WHERE `" & myStructure.itmins_guid_col(0) & "`='" & myItemGuid.ToString & "'")
                        If IsDBNull(enchString) Or enchString.Length < 5 Then
                            enchString = "0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 "
                        End If
                        Dim parts() As String = enchString.Split(" "c)
                        If myItem.Socket3Effectid = Nothing Then
                            'TODO
                        End If
                        parts(12) = myItem.Socket3Effectid.ToString()
                        enchString = String.Join(" ", parts)
                        runSQLCommand_characters_string(
                            "UPDATE `" & myStructure.item_instance_tbl(0) & "` SET `" &
                            myStructure.itmins_enchantments_col(0) & "`='" &
                            enchString & "' WHERE `" & myStructure.invent_guid_col(0) & "`='" & myItemGuid.ToString & "'")
                    End If
            End Select
        End Sub

        Private Sub SetEnch(ByVal myItem As Item, ByVal myItemGuid As Integer, ByVal myCore As String,
                            ByVal myStructure As DBStructure)
            Select Case myCore
                Case "trinity"
                    If Not myItem.EnchantmentId = Nothing Then
                        Dim enchString As String =
                                runSQLCommand_characters_string(
                                    "SELECT `" & myStructure.itmins_enchantments_col(0) & "` FROM `" &
                                    myStructure.item_instance_tbl(0) &
                                    "` WHERE `" & myStructure.itmins_guid_col(0) & "`='" & myItemGuid.ToString & "'")
                        If IsDBNull(enchString) Or enchString.Length < 5 Then
                            enchString = "0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 "
                        End If
                        Dim parts() As String = enchString.Split(" "c)
                        If myItem.EnchantmentEffectid = Nothing Then
                            'TODO
                        End If
                        parts(0) = myItem.EnchantmentEffectid.ToString()
                        enchString = String.Join(" ", parts)
                        runSQLCommand_characters_string(
                            "UPDATE `" & myStructure.item_instance_tbl(0) & "` SET `" &
                            myStructure.itmins_enchantments_col(0) & "`='" &
                            enchString & "' WHERE `" & myStructure.invent_guid_col(0) & "`='" & myItemGuid.ToString & "'")
                    End If
            End Select
        End Sub
    End Class
End Namespace