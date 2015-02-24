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
'*      /Filename:      CharacterQuestlogHandler
'*      /Description:   Contains functions for extracting information about the questlog 
'*                      of a specific character
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.Extension
Imports NCFramework.Framework.Database
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Modules

Namespace Framework.Core
    Public Class CharacterQuestlogHandler
        Public Sub GetCharacterQuestlog(ByVal characterGuid As Integer, ByVal setId As Integer, ByVal account As Account)
            LogAppend("Loading character questlog for characterGuid: " & characterGuid & " and setId: " & setId,
                      "CharacterQuestlogHandler_GetCharacterQuestlog", True)
            Dim player As Character = GetCharacterSetBySetId(setId, account)
            player.Quests = New List(Of Quest)()
            SetCharacterSet(setId, player, account)
            Select Case GlobalVariables.sourceCore
                Case Modules.Core.ARCEMU
                    LoadAtArcemu(characterGuid, setId, account)
                Case Modules.Core.TRINITY
                    Select Case GlobalVariables.sourceExpansion
                        Case Expansion.TBC
                            LoadAtTrinityTbc(characterGuid, setId, account)
                        Case Else
                            LoadAtTrinity(characterGuid, setId, account)
                    End Select
                Case Modules.Core.MANGOS
                    LoadAtMangos(characterGuid, setId, account)
            End Select
        End Sub

        Private Sub LoadAtArcemu(ByVal charguid As Integer, ByVal tarSetId As Integer, ByVal account As Account)
            LogAppend("Loading character questlog @LoadAtArcemu", "CharacterQuestlogHandler_LoadAtArcemu", False)
            Dim tempdt As DataTable =
                    ReturnDataTable(
                        "SELECT " & GlobalVariables.sourceStructure.qst_quest_col(0) & ", " &
                        GlobalVariables.sourceStructure.qst_completed_col(0) & ", " &
                        GlobalVariables.sourceStructure.qst_explored_col(0) &
                        ", " & GlobalVariables.sourceStructure.qst_timer_col(0) & ", " &
                        GlobalVariables.sourceStructure.qst_slot_col(0) & " FROM " &
                        GlobalVariables.sourceStructure.character_queststatus_tbl(0) &
                        " WHERE " & GlobalVariables.sourceStructure.qst_guid_col(0) & "='" & charguid.ToString() & "'")
            Dim player As Character = GetCharacterSetBySetId(tarSetId, account)
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
                            qst.Id = TryInt((tempdt.Rows(count).Item(0)).ToString)
                            qst.Status = TryInt((tempdt.Rows(count).Item(1)).ToString)
                            qst.Explored = TryInt((tempdt.Rows(count).Item(2)).ToString)
                            qst.Timer = TryInt((tempdt.Rows(count).Item(3)).ToString)
                            qst.Slot = TryInt((tempdt.Rows(count).Item(4)).ToString)
                            If player.Quests Is Nothing Then player.Quests = New List(Of Quest)()
                            player.Quests.Add(qst)
                        Loop Until partscounter = excounter - 1
                        count += 1
                    Loop Until count = lastcount
                Else
                    LogAppend("No quests found!", "CharacterQuestlogHandler_LoadAtArcemu", True)
                End If
            Catch ex As Exception
                LogAppend(
                    "Something went wrong while loading character questlog! -> skipping -> Exception is: ###START###" &
                    ex.ToString() & "###END###", "CharacterQuestlogHandler_LoadAtArcemu", True, True)
                Exit Sub
            End Try
            SetCharacterSet(tarSetId, player, account)
        End Sub

        Private Sub LoadAtTrinity(ByVal charguid As Integer, ByVal tarSetId As Integer, ByVal account As Account)
            LogAppend("Loading character questlog @LoadAtTrinity", "CharacterQuestlogHandler_LoadAtTrinity", False)
            Dim tempdt As DataTable =
                    ReturnDataTable(
                        "SELECT " & GlobalVariables.sourceStructure.qst_quest_col(0) & ", " &
                        GlobalVariables.sourceStructure.qst_status_col(0) & ", " &
                        GlobalVariables.sourceStructure.qst_explored_col(0) &
                        ", " & GlobalVariables.sourceStructure.qst_timer_col(0) & " FROM " &
                        GlobalVariables.sourceStructure.character_queststatus_tbl(0) & " WHERE " &
                        GlobalVariables.sourceStructure.qst_guid_col(0) &
                        "='" & charguid.ToString() & "'")
            Dim player As Character = GetCharacterSetBySetId(tarSetId, account)
            Try
                Dim lastcount As Integer = tempdt.Rows.Count
                Dim count As Integer = 0
                If Not lastcount = 0 Then
                    Do
                        Dim qst As New Quest
                        qst.Id = TryInt((tempdt.Rows(count).Item(0)).ToString)
                        qst.Status = TryInt((tempdt.Rows(count).Item(1)).ToString)
                        qst.Explored = TryInt((tempdt.Rows(count).Item(2)).ToString)
                        qst.Timer = TryInt((tempdt.Rows(count).Item(3)).ToString)
                        qst.Rewarded = 0
                        If player.Quests Is Nothing Then player.Quests = New List(Of Quest)()
                        player.Quests.Add(qst)
                        count += 1
                    Loop Until count = lastcount
                Else
                    LogAppend("No quests found!", "CharacterQuestlogHandler_LoadAtTrinity", True)
                End If
            Catch ex As Exception
                LogAppend(
                    "Something went wrong while loading character questlog! -> skipping -> Exception is: ###START###" &
                    ex.ToString() & "###END###", "CharacterQuestlogHandler_LoadAtTrinity", True, True)
            End Try
            Dim tempdt2 As DataTable =
                    ReturnDataTable(
                        "SELECT " & GlobalVariables.sourceStructure.qstre_quest_col(0) & " FROM " &
                        GlobalVariables.sourceStructure.character_queststatus_rewarded_tbl(0) & " WHERE " &
                        GlobalVariables.sourceStructure.qstre_guid_col(0) & "='" & charguid.ToString() & "'")
            Try
                Dim lastcount As Integer = tempdt2.Rows.Count
                Dim count As Integer = 0
                If Not lastcount = 0 Then
                    Do
                        Dim quest As String = (tempdt2.Rows(count).Item(0)).ToString
                        If Not quest = "" Then player.FinishedQuests.SafeAddRange(quest.Split(","c).ToList().ConvertAll(
                            Function(str) Integer.Parse(str)) _
                                                                                     .ToArray())
                        count += 1
                    Loop Until count = lastcount
                End If
            Catch ex As Exception
                LogAppend(
                    "Something went wrong while loading character finishedQuests! -> skipping -> Exception is: ###START###" &
                    ex.ToString() & "###END###", "CharacterQuestlogHandler_LoadAtTrinity", True, True)
            End Try
            SetCharacterSet(tarSetId, player, account)
        End Sub

        Private Sub LoadAtTrinityTbc(ByVal charguid As Integer, ByVal tarSetId As Integer, ByVal account As Account)
            LogAppend("Loading character questlog @LoadAtTrinityTBC", "CharacterQuestlogHandler_LoadAtTrinityTBC", False)
            Dim tempdt As DataTable =
                    ReturnDataTable(
                        "SELECT " & GlobalVariables.sourceStructure.qst_quest_col(0) & ", " &
                        GlobalVariables.sourceStructure.qst_status_col(0) & ", " &
                        GlobalVariables.sourceStructure.qst_explored_col(0) &
                        ", " & GlobalVariables.sourceStructure.qst_timer_col(0) & ", " &
                        GlobalVariables.sourceStructure.qst_rewarded_col(0) & " FROM " &
                        GlobalVariables.sourceStructure.character_queststatus_tbl(0) &
                        " WHERE " & GlobalVariables.sourceStructure.qst_guid_col(0) & "='" & charguid.ToString() & "'")
            Dim player As Character = GetCharacterSetBySetId(tarSetId, account)
            Try
                Dim lastcount As Integer = tempdt.Rows.Count
                Dim count As Integer = 0
                If Not lastcount = 0 Then
                    Do
                        Dim qst As New Quest
                        qst.Id = TryInt((tempdt.Rows(count).Item(0)).ToString)
                        qst.Status = TryInt((tempdt.Rows(count).Item(1)).ToString)
                        qst.Explored = TryInt((tempdt.Rows(count).Item(2)).ToString)
                        qst.Timer = TryInt((tempdt.Rows(count).Item(3)).ToString)
                        Dim rewarded As String = (tempdt.Rows(count).Item(4)).ToString
                        qst.Rewarded = TryInt(rewarded)
                        If rewarded = "1" Then
                            player.FinishedQuests.SafeAdd(qst.Id)
                        Else
                            If player.Quests Is Nothing Then player.Quests = New List(Of Quest)()
                            player.Quests.Add(qst)
                        End If
                        count += 1
                    Loop Until count = lastcount
                Else
                    LogAppend("No quests found!", "CharacterQuestlogHandler_LoadAtTrinityTBC", True)
                End If
            Catch ex As Exception
                LogAppend(
                    "Something went wrong while loading character questlog! -> skipping -> Exception is: ###START###" &
                    ex.ToString() & "###END###", "CharacterQuestlogHandler_LoadAtTrinityTBC", True, True)
            End Try
            SetCharacterSet(tarSetId, player, account)
        End Sub

        Private Sub LoadAtMangos(ByVal charguid As Integer, ByVal tarSetId As Integer, ByVal account As Account)
            LogAppend("Loading character questlog @LoadAtMangos", "CharacterQuestlogHandler_LoadAtMangos", False)
            Dim tempdt As DataTable =
                    ReturnDataTable(
                        "SELECT " & GlobalVariables.sourceStructure.qst_quest_col(0) & ", " &
                        GlobalVariables.sourceStructure.qst_status_col(0) & ", " &
                        GlobalVariables.sourceStructure.qst_explored_col(0) &
                        ", " & GlobalVariables.sourceStructure.qst_timer_col(0) & ", " &
                        GlobalVariables.sourceStructure.qst_rewarded_col(0) & " FROM " &
                        GlobalVariables.sourceStructure.character_queststatus_tbl(0) &
                        " WHERE " & GlobalVariables.sourceStructure.qst_guid_col(0) & "='" & charguid.ToString() & "'")
            Dim player As Character = GetCharacterSetBySetId(tarSetId, account)
            Try
                Dim lastcount As Integer = tempdt.Rows.Count
                Dim count As Integer = 0
                If Not lastcount = 0 Then
                    Do
                        Dim qst As New Quest
                        qst.Id = TryInt((tempdt.Rows(count).Item(0)).ToString)
                        qst.Status = TryInt((tempdt.Rows(count).Item(1)).ToString)
                        qst.Explored = TryInt((tempdt.Rows(count).Item(2)).ToString)
                        qst.Timer = TryInt((tempdt.Rows(count).Item(3)).ToString)
                        qst.Rewarded = TryInt((tempdt.Rows(count).Item(4)).ToString)
                        If qst.Rewarded = 1 Then
                            player.FinishedQuests.SafeAdd(qst.Id)
                        Else
                            If player.Quests Is Nothing Then player.Quests = New List(Of Quest)()
                            player.Quests.Add(qst)
                        End If
                        count += 1
                    Loop Until count = lastcount
                Else
                    LogAppend("No quests found!", "CharacterQuestlogHandler_LoadAtMangos", True)
                End If
            Catch ex As Exception
                LogAppend(
                    "Something went wrong while loading character questlog! -> skipping -> Exception is: ###START###" &
                    ex.ToString() & "###END###", "CharacterQuestlogHandler_LoadAtMangos", True, True)
                Exit Sub
            End Try
            SetCharacterSet(tarSetId, player, account)
        End Sub
    End Class
End Namespace