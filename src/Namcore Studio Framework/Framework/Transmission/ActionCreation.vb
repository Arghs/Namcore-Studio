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
'*      /Filename:      ActionCreation
'*      /Description:   Includes functions for setting up the action bars of a specific
'*                      character
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.Database
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Modules

Namespace Framework.Transmission
    Public Class ActionCreation
        Public Sub SetCharacterActions(ByVal player As Character, Optional charguid As Integer = 0)
            If charguid = 0 Then charguid = player.Guid
            LogAppend("Setting Actions for character: " & charguid.ToString(),
                      "ActionCreation_SetCharacterActions", True)
            Try
                Select Case GlobalVariables.targetCore
                    Case Modules.Core.ARCEMU
                        '  CreateAtArcemu(charguid, player)
                    Case Modules.Core.TRINITY
                        CreateAtTrinity(charguid, player)
                    Case Modules.Core.MANGOS
                        CreateAtMangos(charguid, player)
                End Select
            Catch ex As Exception
                LogAppend("Exception occured: " & ex.ToString(),
                          "ActionCreation_SetCharacterActions", False, True)
            End Try
        End Sub

        Private Sub CreateAtTrinity(ByVal characterguid As Integer, ByVal player As Character)
            LogAppend("Creating at Trinity", "ActionCreation_createAtTrinity", False)
            If Not player.Actions Is Nothing Then
                If Not player.Actions.Count = 0 Then
                    For Each action As Action In player.Actions
                        runSQLCommand_characters_string(
                            "INSERT INTO " & GlobalVariables.targetStructure.character_action_tbl(0) & " ( " &
                            GlobalVariables.targetStructure.action_guid_col(0) & ", " &
                            GlobalVariables.targetStructure.action_spec_col(0) & ", `" &
                            GlobalVariables.targetStructure.action_action_col(0) & "`, `" &
                            GlobalVariables.targetStructure.action_type_col(0) & "`, `" &
                            GlobalVariables.targetStructure.action_button_col(0) & "` ) VALUES ( '" &
                            characterguid.ToString() & "', '" & action.Spec.ToString &
                            "', '" & action.ActionId.ToString() & "', '" & action.ActionType.ToString &
                            "', '" & action.Button.ToString & "')", True)
                    Next
                Else : LogAppend("No Actions", "ActionCreation_createAtTrinity", False)
                End If
            Else : LogAppend("No Actions", "ActionCreation_createAtTrinity", False)
            End If
        End Sub

        Private Sub CreateAtMangos(ByVal characterguid As Integer, ByVal player As Character)
            LogAppend("Creating at Mangos", "ActionCreation_createAtMangos", False)
            If Not player.Actions Is Nothing Then
                If Not player.Actions.Count = 0 Then
                    For Each action As Action In player.Actions
                        runSQLCommand_characters_string(
                            "INSERT INTO " & GlobalVariables.targetStructure.character_action_tbl(0) & " ( " &
                            GlobalVariables.targetStructure.action_guid_col(0) & ", " &
                            GlobalVariables.targetStructure.action_spec_col(0) & ", `" &
                            GlobalVariables.targetStructure.action_action_col(0) & "`, `" &
                            GlobalVariables.targetStructure.action_type_col(0) & "`, `" &
                            GlobalVariables.targetStructure.action_button_col(0) & "` ) VALUES ( '" &
                            characterguid.ToString() & "', '" & action.Spec.ToString &
                            "', '" & action.ActionId.ToString() & "', '" & action.ActionType.ToString &
                            "', '" & action.Button.ToString & "')", True)
                    Next
                Else : LogAppend("No Actions", "ActionCreation_createAtMangos", False)
                End If
            Else : LogAppend("No Actions", "ActionCreation_createAtMangos", False)
            End If
        End Sub
    End Class
End Namespace