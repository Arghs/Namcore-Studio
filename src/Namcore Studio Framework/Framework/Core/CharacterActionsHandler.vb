'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013 NamCore Studio <https://github.com/megasus/Namcore-Studio>
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
'*      /Filename:      CharacterActionHandler
'*      /Description:   Contains functions for extracting information about the actionbar
'*                      configurations of a specific character
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.Database
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Modules

Namespace Framework.Core
    Public Class CharacterActionsHandler
        Public Sub GetCharacterActions(ByVal characterGuid As Integer, ByVal setId As Integer, ByVal account As Account)
            LogAppend("Loading character actions for characterGuid: " & characterGuid & " and setId: " & setId,
                      "CharacterActionssHandler_GetCharacterActions", True)
            Select Case GlobalVariables.sourceCore
                Case "arcemu"
                    LoadAtArcemu(characterGuid, setId, account)
                Case "trinity"
                    LoadAtTrinity(characterGuid, setId, account)
                Case "trinitytbc"
                    'todo LoadAtTrinityTBC(characterGuid, setId, accountId, account)
                Case "mangos"
                    LoadAtMangos(characterGuid, setId, account)

            End Select
        End Sub

        Private Sub LoadAtArcemu(ByVal charguid As Integer, ByVal tarSetId As Integer, ByVal account As Account)
            LogAppend("Loading character Actions @LoadAtArcemu", "CharacterActionsHandler_LoadAtArcemu", False)
            Dim tmpCharacter As Character = GetCharacterSetBySetId(tarSetId, account)
            tmpCharacter.ArcEmuAction1 =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_actions1_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) &
                    " WHERE " & GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            tmpCharacter.ArcEmuAction2 =
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.sourceStructure.char_actions2_col(0) & " FROM " &
                    GlobalVariables.sourceStructure.character_tbl(0) &
                    " WHERE " & GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            If tmpCharacter.Actions Is Nothing Then tmpCharacter.Actions = New List(Of Action)()
            Try
                Dim readedcode As String = tmpCharacter.ArcEmuAction1
                If Not readedcode.Length > 2 Then _
                    LogAppend("Warning! Actions1 seems to be invalid!", "CharacterActionsHandler_LoadAtArcemu", False, True)
                Dim excounter As Integer = UBound(readedcode.Split(CChar(",")))
                Dim loopcounter As Integer = 0
                Dim finalcounter As Integer = TryInt(excounter / 3)
                Const partscounter As Integer = 0
                Do
                    Dim act As New Action
                    Dim parts() As String = readedcode.Split(","c)
                    act.ActionId = TryInt(parts(partscounter).ToString)
                    act.Button = TryInt((loopcounter + 1).ToString)
                    act.OwnerSet = tarSetId
                    act.ActionType = 0
                    act.Spec = 0
                    tmpCharacter.Actions.Add(act)
                    loopcounter += 1
                Loop Until loopcounter = finalcounter
                Dim readedcode2 As String = tmpCharacter.ArcEmuAction2
                If Not readedcode2.Length > 2 Then _
                    LogAppend("Warning! Actions2 seems to be invalid!", "CharacterActionsHandler_LoadAtArcemu", False, True)
                Dim excounter2 As Integer = UBound(readedcode2.Split(CChar(",")))
                Dim loopcounter2 As Integer = 0
                Dim finalcounter2 As Integer = TryInt(excounter2 / 3)
                Dim partscounter2 As Integer = 0
                Do
                    Dim act As New Action
                    Dim parts() As String = readedcode2.Split(","c)
                    act.ActionId = TryInt(parts(partscounter2).ToString)
                    act.Button = TryInt((loopcounter2 + 1).ToString)
                    act.OwnerSet = tarSetId
                    act.ActionType = 0
                    act.Spec = 1
                    tmpCharacter.Actions.Add(act)
                    partscounter2 += 3
                    loopcounter2 += 1
                Loop Until loopcounter2 = finalcounter2
            Catch ex As Exception
                LogAppend(
                    "Something went wrong while loading character Actions! -> skipping -> Exception is: ###START###" &
                    ex.ToString() & "###END###", "CharacterActionsHandler_LoadAtArcemu", True, True)
            End Try
            SetCharacterSet(tarSetId, tmpCharacter, account)
        End Sub

        Private Sub LoadAtTrinity(ByVal charguid As Integer, ByVal tarSetId As Integer, ByVal account As Account)
            LogAppend("Loading character Actions @LoadAtTrinity", "CharacterActionsHandler_LoadAtTrinity", False)
            Dim tempdt As DataTable =
                    ReturnDataTable(
                        "SELECT " & GlobalVariables.sourceStructure.action_button_col(0) & ", `" &
                        GlobalVariables.sourceStructure.action_spec_col(0) & "`, " &
                        GlobalVariables.sourceStructure.action_action_col(0) &
                        ", `" & GlobalVariables.sourceStructure.action_type_col(0) & "` FROM " &
                        GlobalVariables.sourceStructure.character_action_tbl(0) & " WHERE " &
                        GlobalVariables.sourceStructure.action_guid_col(0) &
                        "='" & charguid.ToString() & "'")
            Dim tmpCharacter As Character = GetCharacterSetBySetId(tarSetId, account)
            If tmpCharacter.Actions Is Nothing Then tmpCharacter.Actions = New List(Of Action)()
            Try
                Dim lastcount As Integer = tempdt.Rows.Count
                Dim count As Integer = 0
                If Not lastcount = 0 Then
                    Do
                        Dim act As New Action
                        act.Button = TryInt((tempdt.Rows(count).Item(0)).ToString)
                        act.Spec = TryInt((tempdt.Rows(count).Item(1)).ToString)
                        act.ActionId = TryInt((tempdt.Rows(count).Item(2)).ToString)
                        act.ActionType = TryInt((tempdt.Rows(count).Item(3)).ToString)
                        act.OwnerSet = tarSetId
                        tmpCharacter.Actions.Add(act)
                        count += 1
                    Loop Until count = lastcount
                Else
                    LogAppend("No Actions found!", "CharacterActionsHandler_LoadAtTrinity", True)
                End If
            Catch ex As Exception
                LogAppend(
                    "Something went wrong while loading character Actions! -> skipping -> Exception is: ###START###" &
                    ex.ToString() & "###END###", "CharacterActionsHandler_LoadAtTrinity", True, True)
                Exit Sub
            End Try
            SetCharacterSet(tarSetId, tmpCharacter, account)
        End Sub

        Private Sub LoadAtMangos(ByVal charguid As Integer, ByVal tarSetId As Integer, ByVal account As Account)
            LogAppend("Loading character Actions @LoadAtMangos", "CharacterActionsHandler_LoadAtMangos", False)
            Dim tempdt As DataTable =
                    ReturnDataTable(
                        "SELECT " & GlobalVariables.sourceStructure.action_button_col(0) & ", `" &
                        GlobalVariables.sourceStructure.action_spec_col(0) & "`, " &
                        GlobalVariables.sourceStructure.action_action_col(0) &
                        ", `" & GlobalVariables.sourceStructure.action_type_col(0) & "` FROM " &
                        GlobalVariables.sourceStructure.character_action_tbl(0) & " WHERE " &
                        GlobalVariables.sourceStructure.action_guid_col(0) &
                        "='" & charguid.ToString() & "'")
            Dim tmpCharacter As Character = GetCharacterSetBySetId(tarSetId, account)
            If tmpCharacter.Actions Is Nothing Then tmpCharacter.Actions = New List(Of Action)()
            Try
                Dim lastcount As Integer = tempdt.Rows.Count
                Dim count As Integer = 0
                If Not lastcount = 0 Then
                    Do
                        Dim act As New Action
                        act.Button = TryInt((tempdt.Rows(count).Item(0)).ToString)
                        act.Spec = TryInt((tempdt.Rows(count).Item(1)).ToString)
                        act.ActionId = TryInt((tempdt.Rows(count).Item(2)).ToString)
                        act.ActionType = TryInt((tempdt.Rows(count).Item(3)).ToString)
                        act.OwnerSet = tarSetId
                        tmpCharacter.Actions.Add(act)
                        count += 1
                    Loop Until count = lastcount
                Else
                    LogAppend("No Actions found!", "CharacterActionsHandler_LoadAtMangos", True)
                End If
            Catch ex As Exception
                LogAppend(
                    "Something went wrong while loading character Actions! -> skipping -> Exception is: ###START###" &
                    ex.ToString() & "###END###", "CharacterActionsHandler_LoadAtMangos", True, True)
                Exit Sub
            End Try
            SetCharacterSet(tarSetId, tmpCharacter, account)
        End Sub
    End Class
End Namespace