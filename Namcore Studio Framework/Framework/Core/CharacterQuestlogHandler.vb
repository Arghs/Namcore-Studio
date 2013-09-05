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
'*      /Filename:      CharacterQuestlogHandler
'*      /Description:   Contains functions for extracting information about the questlog 
'*                      of a specific character
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Imports NCFramework.EventLogging
Imports NCFramework.Basics
Imports NCFramework.GlobalVariables
Imports NCFramework.CommandHandler
Imports NCFramework.Conversions
    Public Class CharacterQuestlogHandler
    Public Sub GetCharacterQuestlog(ByVal characterGuid As Integer, ByVal setId As Integer, ByVal accountId As Integer)
        LogAppend("Loading character questlog for characterGuid: " & characterGuid & " and setId: " & setId, "CharacterQuestlogHandler_GetCharacterQuestlog", True)
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
        LogAppend("Loading character questlog @loadAtArcemu", "CharacterQuestlogHandler_loadAtArcemu", False)
        Dim tempdt As DataTable = ReturnDataTable("SELECT " & sourceStructure.qst_quest_col(0) & ", " & sourceStructure.qst_completed_col(0) & ", " & sourceStructure.qst_explored_col(0) &
                                                  ", " & sourceStructure.qst_timer_col(0) & ", " & sourceStructure.qst_slot_col(0) & " FROM " & sourceStructure.character_queststatus_tbl(0) &
                                                  " WHERE " & sourceStructure.qst_guid_col(0) & "='" & charguid.ToString() & "'")
        Dim player As Character = GetCharacterSetBySetId(tar_setId)
        Try
            Dim lastcount As Integer = TryInt(tempdt.Rows.Count.ToString)
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim readedcode As String = (tempdt.Rows(count).Item(0)).ToString
                    Dim excounter As Integer = UBound(readedcode.Split(CChar(",")))
                    Const partscounter As Integer = 0
                    Do
                        Dim qst As New Quest
                        qst.id = TryInt((tempdt.Rows(count).Item(0)).ToString)
                        qst.status = TryInt((tempdt.Rows(count).Item(1)).ToString)
                        qst.explored = TryInt((tempdt.Rows(count).Item(2)).ToString)
                        qst.timer = TryInt((tempdt.Rows(count).Item(3)).ToString)
                        qst.slot = TryInt((tempdt.Rows(count).Item(4)).ToString)
                        If player.Quests Is Nothing Then player.Quests = New List(Of Quest)()
                        player.Quests.Add(qst)
                    Loop Until partscounter = excounter - 1
                    count += 1
                Loop Until count = lastcount
            Else
                LogAppend("No quests found!", "CharacterQuestlogHandler_loadAtArcemu", True)
            End If
        Catch ex As Exception
            LogAppend("Something went wrong while loading character questlog! -> skipping -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterQuestlogHandler_loadAtArcemu", True, True)
            Exit Sub
        End Try
        SetCharacterSet(tar_setId, player)
    End Sub
    Private Sub loadAtTrinity(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)
        LogAppend("Loading character questlog @loadAtTrinity", "CharacterQuestlogHandler_loadAtTrinity", False)
        Dim tempdt As DataTable = ReturnDataTable("SELECT " & sourceStructure.qst_quest_col(0) & ", " & sourceStructure.qst_status_col(0) & ", " & sourceStructure.qst_explored_col(0) &
                                                  ", " & sourceStructure.qst_timer_col(0) & " FROM " & sourceStructure.character_queststatus_tbl(0) & " WHERE " & sourceStructure.qst_guid_col(0) &
                                                  "='" & charguid.ToString() & "'")
        Dim player As Character = GetCharacterSetBySetId(tar_setId)
        Try
            Dim lastcount As Integer = tempdt.Rows.Count
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim qst As New Quest
                    qst.id = TryInt((tempdt.Rows(count).Item(0)).ToString)
                    qst.status = TryInt((tempdt.Rows(count).Item(1)).ToString)
                    qst.explored = TryInt((tempdt.Rows(count).Item(2)).ToString)
                    qst.timer = TryInt((tempdt.Rows(count).Item(3)).ToString)
                    If player.Quests Is Nothing Then player.Quests = New List(Of Quest)()
                    player.Quests.Add(qst)
                    count += 1
                Loop Until count = lastcount
            Else
                LogAppend("No quests found!", "CharacterQuestlogHandler_loadAtTrinity", True)
            End If
        Catch ex As Exception
            LogAppend("Something went wrong while loading character questlog! -> skipping -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterQuestlogHandler_loadAtTrinity", True, True)
        End Try
        Dim tempdt2 As DataTable = ReturnDataTable("SELECT " & sourceStructure.qstre_quest_col(0) & " FROM " & sourceStructure.character_queststatus_rewarded_tbl(0) & " WHERE " &
                                                   sourceStructure.qstre_guid_col(0) & "='" & charguid.ToString() & "'")
        Try
            Dim lastcount As Integer = tempdt2.Rows.Count
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim quest As String = (tempdt2.Rows(count).Item(0)).ToString
                    If Not quest = "" Then player.FinishedQuests = quest & ","
                    count += 1
                Loop Until count = lastcount
            End If
        Catch ex As Exception
            LogAppend("Something went wrong while loading character finishedQuests! -> skipping -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterQuestlogHandler_loadAtTrinity", True, True)
        End Try
        SetCharacterSet(tar_setId, player)
    End Sub
    Private Sub loadAtTrinityTBC(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)
        LogAppend("Loading character questlog @loadAtTrinityTBC", "CharacterQuestlogHandler_loadAtTrinityTBC", False)
        Dim tempdt As DataTable = ReturnDataTable("SELECT " & sourceStructure.qst_quest_col(0) & ", " & sourceStructure.qst_status_col(0) & ", " & sourceStructure.qst_explored_col(0) &
                                                  ", " & sourceStructure.qst_timer_col(0) & ", " & sourceStructure.qst_rewarded_col(0) & " FROM " & sourceStructure.character_queststatus_tbl(0) &
                                                  " WHERE " & sourceStructure.qst_guid_col(0) & "='" & charguid.ToString() & "'")
        Dim player As Character = GetCharacterSetBySetId(tar_setId)
        Try
            Dim lastcount As Integer = tempdt.Rows.Count
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim qst As New Quest
                    qst.id = TryInt((tempdt.Rows(count).Item(0)).ToString)
                    qst.status = TryInt((tempdt.Rows(count).Item(1)).ToString)
                    qst.explored = TryInt((tempdt.Rows(count).Item(2)).ToString)
                    qst.timer = TryInt((tempdt.Rows(count).Item(3)).ToString)
                    Dim rewarded As String = (tempdt.Rows(count).Item(4)).ToString
                    qst.rewarded = TryInt(rewarded)
                    If rewarded = "1" Then
                        player.FinishedQuests = qst.id & ","
                    Else
                        If player.Quests Is Nothing Then player.Quests = New List(Of Quest)()
                        player.Quests.Add(qst)
                    End If
                    count += 1
                Loop Until count = lastcount
            Else
                LogAppend("No quests found!", "CharacterQuestlogHandler_loadAtTrinityTBC", True)
            End If
        Catch ex As Exception
            LogAppend("Something went wrong while loading character questlog! -> skipping -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterQuestlogHandler_loadAtTrinityTBC", True, True)
        End Try
        SetCharacterSet(tar_setId, player)
    End Sub
    Private Sub loadAtMangos(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)
        LogAppend("Loading character questlog @loadAtMangos", "CharacterQuestlogHandler_loadAtMangos", False)
        Dim tempdt As DataTable = ReturnDataTable("SELECT " & sourceStructure.qst_quest_col(0) & ", " & sourceStructure.qst_status_col(0) & ", " & sourceStructure.qst_explored_col(0) &
                                                  ", " & sourceStructure.qst_timer_col(0) & ", " & sourceStructure.qst_rewarded_col(0) & " FROM " & sourceStructure.character_queststatus_tbl(0) &
                                                  " WHERE " & sourceStructure.qst_guid_col(0) & "='" & charguid.ToString() & "'")
        Dim player As Character = GetCharacterSetBySetId(tar_setId)
        Try
            Dim lastcount As Integer = tempdt.Rows.Count
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim qst As New Quest
                    qst.id = TryInt((tempdt.Rows(count).Item(0)).ToString)
                    qst.status = TryInt((tempdt.Rows(count).Item(1)).ToString)
                    qst.explored = TryInt((tempdt.Rows(count).Item(2)).ToString)
                    qst.timer = TryInt((tempdt.Rows(count).Item(3)).ToString)
                    Dim rewarded As String = (tempdt.Rows(count).Item(4)).ToString
                    qst.rewarded = TryInt(rewarded)
                    If rewarded = "1" Then
                        player.FinishedQuests = qst.id & ","
                    Else
                        If player.Quests Is Nothing Then player.Quests = New List(Of Quest)()
                        player.Quests.Add(qst)
                    End If
                    count += 1
                Loop Until count = lastcount
            Else
                LogAppend("No quests found!", "CharacterQuestlogHandler_loadAtMangos", True)
            End If
        Catch ex As Exception
            LogAppend("Something went wrong while loading character questlog! -> skipping -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterQuestlogHandler_loadAtMangos", True, True)
            Exit Sub
        End Try
        SetCharacterSet(tar_setId, player)
    End Sub
       
    End Class
