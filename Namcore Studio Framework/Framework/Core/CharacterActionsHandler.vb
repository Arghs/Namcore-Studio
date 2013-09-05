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
'*      /Filename:      CharacterActionHandler
'*      /Description:   Contains functions for extracting information about the actionbar
'*                      configurations of a specific character
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Imports NCFramework.EventLogging
Imports NCFramework.Basics
Imports NCFramework.GlobalVariables
Imports NCFramework.CommandHandler
Imports NCFramework.Conversions
Public Class CharacterActionsHandler
    Public Sub GetCharacterActions(ByVal characterGuid As Integer, ByVal setId As Integer, ByVal accountId As Integer)
        LogAppend("Loading character actions for characterGuid: " & characterGuid & " and setId: " & setId, "CharacterActionssHandler_GetCharacterActions", True)
        Select Case sourceCore
            Case "arcemu"
                loadAtArcemu(characterGuid, setId, accountId)
            Case "trinity"
                loadAtTrinity(characterGuid, setId, accountId)
            Case "trinitytbc"
                loadAtTrinityTBC(characterGuid, setId, accountId)
            Case "mangos"
                loadAtMangos(characterGuid, setId, accountId)
            Case Else

        End Select

    End Sub
    Private Sub loadAtArcemu(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)
        LogAppend("Loading character Actions @loadAtArcemu", "CharacterActionsHandler_loadAtArcemu", False)
        Dim tmpCharacter As Character = GetCharacterSetBySetId(tar_setId)
        tmpCharacter.ArcEmuAction1 = runSQLCommand_characters_string("SELECT " & sourceStructure.char_actions1_col(0) & " FROM " & sourceStructure.character_tbl(0) &
                                                                                                     " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        tmpCharacter.ArcEmuAction2 = runSQLCommand_characters_string("SELECT " & sourceStructure.char_actions2_col(0) & " FROM " & sourceStructure.character_tbl(0) &
                                                                                                    " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        If tmpCharacter.Actions Is Nothing Then tmpCharacter.Actions = New List(Of Action)()
        Try
            Dim readedcode As String = tmpCharacter.ArcEmuAction1
            If Not readedcode.Length > 2 Then LogAppend("Warning! Actions1 seems to be invalid!", "CharacterActionsHandler_loadAtArcemu", False, True)
            Dim excounter As Integer = UBound(readedcode.Split(CChar(",")))
            Dim loopcounter As Integer = 0
            Dim finalcounter As Integer = tryint(excounter / 3)
            Const partscounter As Integer = 0
            Do
                Dim act As New Action
                Dim parts() As String = readedcode.Split(","c)
                act.ActionId = TryInt(parts(partscounter).ToString)
                act.Button = TryInt((loopcounter + 1).ToString)
                act.OwnerSet = tar_setId
                act.ActionType = 0
                act.Spec = 0
                tmpCharacter.Actions.Add(act)
                loopcounter += 1
            Loop Until loopcounter = finalcounter
            Dim readedcode2 As String = tmpCharacter.ArcEmuAction2
            If Not readedcode2.Length > 2 Then LogAppend("Warning! Actions2 seems to be invalid!", "CharacterActionsHandler_loadAtArcemu", False, True)
            Dim excounter2 As Integer = UBound(readedcode2.Split(CChar(",")))
            Dim loopcounter2 As Integer = 0
            Dim finalcounter2 As Integer = tryint(excounter2 / 3)
            Dim partscounter2 As Integer = 0
            Do
                Dim act As New Action
                Dim parts() As String = readedcode2.Split(","c)
                act.ActionId = TryInt(parts(partscounter2).ToString)
                act.Button = TryInt((loopcounter2 + 1).ToString)
                act.OwnerSet = tar_setId
                act.ActionType = 0
                act.Spec = 1
                tmpCharacter.Actions.Add(act)
                partscounter2 += 3
                loopcounter2 += 1
            Loop Until loopcounter2 = finalcounter2
        Catch ex As Exception
            LogAppend("Something went wrong while loading character Actions! -> skipping -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterActionsHandler_loadAtArcemu", True, True)
        End Try
        SetCharacterSet(tar_setId, tmpCharacter)
    End Sub
    Private Sub loadAtTrinity(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)
        LogAppend("Loading character Actions @loadAtTrinity", "CharacterActionsHandler_loadAtTrinity", False)
        Dim tempdt As DataTable = ReturnDataTable("SELECT " & sourceStructure.action_button_col(0) & ", `" & sourceStructure.action_spec_col(0) & "`, " & sourceStructure.action_action_col(0) &
                                                  ", `" & sourceStructure.action_type_col(0) & "` FROM " & sourceStructure.character_action_tbl(0) & " WHERE " & sourceStructure.action_guid_col(0) &
                                                  "='" & charguid.ToString() & "'")
        Dim tmpCharacter As Character = GetCharacterSetBySetId(tar_setId)
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
                    act.OwnerSet = tar_setId
                    tmpCharacter.Actions.Add(act)
                    count += 1
                Loop Until count = lastcount
            Else
                LogAppend("No Actions found!", "CharacterActionsHandler_loadAtTrinity", True)
            End If
        Catch ex As Exception
            LogAppend("Something went wrong while loading character Actions! -> skipping -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterActionsHandler_loadAtTrinity", True, True)
            Exit Sub
        End Try
        SetCharacterSet(tar_setId, tmpCharacter)
    End Sub
    Private Sub loadAtTrinityTBC(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)

    End Sub
    Private Sub loadAtMangos(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)
        LogAppend("Loading character Actions @loadAtMangos", "CharacterActionsHandler_loadAtMangos", False)
        Dim tempdt As DataTable = ReturnDataTable("SELECT " & sourceStructure.action_button_col(0) & ", `" & sourceStructure.action_spec_col(0) & "`, " & sourceStructure.action_action_col(0) &
                                                  ", `" & sourceStructure.action_type_col(0) & "` FROM " & sourceStructure.character_action_tbl(0) & " WHERE " & sourceStructure.action_guid_col(0) &
                                                  "='" & charguid.ToString() & "'")
        Dim tmpCharacter As Character = GetCharacterSetBySetId(tar_setId)
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
                    act.OwnerSet = tar_setId
                    tmpCharacter.Actions.Add(act)
                    count += 1
                Loop Until count = lastcount
            Else
                LogAppend("No Actions found!", "CharacterActionsHandler_loadAtMangos", True)
            End If
        Catch ex As Exception
            LogAppend("Something went wrong while loading character Actions! -> skipping -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterActionsHandler_loadAtMangos", True, True)
            Exit Sub
        End Try
        SetCharacterSet(tar_setId, tmpCharacter)
    End Sub
End Class
