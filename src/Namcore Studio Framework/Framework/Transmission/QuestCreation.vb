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
'*      /Filename:      QuestCreation
'*      /Description:   Includes functions for setting up the questlog of a specific
'*                      character
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.Database
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Modules

Namespace Framework.Transmission

    Public Class QuestCreation
        Public Sub SetCharacterQuests(ByVal setId As Integer, ByVal account As Account, Optional charguid As Integer = 0)
            If charguid = 0 Then charguid = GetCharacterSetBySetId(setId, account).Guid
            LogAppend("Setting quests for character: " & charguid.ToString() & " // setId is : " & setId.ToString(),
                      "QuestCreation_SetCharacterQuests", True)
            Select Case GlobalVariables.targetCore
                Case "arcemu"
                    CreateAtArcemu(charguid, setId, account)
                Case "trinity"
                    CreateAtTrinity(charguid, setId, account)
                Case "trinitytbc"

                Case "mangos"
                    CreateAtMangos(charguid, setId, account)
            End Select
        End Sub

        Private Sub CreateAtArcemu(ByVal characterguid As Integer, ByVal targetSetId As Integer, ByVal account As Account)
            LogAppend("Creating at arcemu", "QuestCreation_createAtArcemu", False)
            Dim lastslot As Integer = TryInt(
                runSQLCommand_characters_string(
                    "SELECT " & GlobalVariables.targetStructure.qst_slot_col(0) & " FROM " &
                    GlobalVariables.targetStructure.character_queststatus_tbl(0) & " WHERE " &
                    GlobalVariables.targetStructure.qst_guid_col(0) & "='" & characterguid.ToString() & "' AND " &
                    GlobalVariables.targetStructure.qst_slot_col(0) &
                    "=(SELECT MAX(" & GlobalVariables.targetStructure.qst_slot_col(0) & ") FROM " &
                    GlobalVariables.targetStructure.character_tbl(0) & ")", True)) + 1
            Dim player As Character = GetCharacterSetBySetId(targetSetId, account)
            If Not player.Quests Is Nothing Then
                If Not player.Quests.Count = 0 Then
                    For Each qst As Quest In player.Quests
                        Dim explored As String = qst.Explored.ToString
                        If explored = Nothing Then explored = ""
                        Dim tmpcommand As String = "INSERT INTO " &
                                                   GlobalVariables.targetStructure.character_queststatus_tbl(0) & " ( " &
                                                   GlobalVariables.targetStructure.qst_guid_col(0) & ", " &
                                                   GlobalVariables.targetStructure.qst_quest_col(0) & ", " &
                                                   GlobalVariables.targetStructure.qst_slot_col(0) & ", `" &
                                                   GlobalVariables.targetStructure.qst_completed_col(0) & "`, `" &
                                                   GlobalVariables.targetStructure.qst_explored_col(0) & "` ) VALUES ( '" &
                                                   characterguid.ToString() & "', '" &
                                                   qst.Id.ToString & "', '" & lastslot.ToString & "', '" &
                                                   qst.Status.ToString & "'," & " '" & explored & "')"
                        runSQLCommand_characters_string(tmpcommand, True)
                        lastslot += 1
                    Next
                Else : LogAppend("No quests in questlog", "QuestCreation_createAtArcemu", False)
                End If
            Else : LogAppend("No quests in questlog", "QuestCreation_createAtArcemu", False)
            End If
            Dim finishedQuestsString As String = player.FinishedQuests
            If Not finishedQuestsString = "" Then _
                runSQLCommand_characters_string(
                    "UPDATE characters SET finished_quests='" & finishedQuestsString & "' WHERE guid='" &
                    characterguid.ToString() & "'", True)
        End Sub

        Private Sub CreateAtTrinity(ByVal characterguid As Integer, ByVal targetSetId As Integer, ByVal account As Account)
            LogAppend("Creating at Trinity", "QuestCreation_createAtTrinity", False)
            Dim player As Character = GetCharacterSetBySetId(targetSetId, account)
            If Not player.Quests Is Nothing Then
                If Not player.Quests.Count = 0 Then
                    For Each qst As Quest In player.Quests
                        Dim queststatus As String = qst.Status.ToString
                        If queststatus = "0" Then queststatus = "1"
                        runSQLCommand_characters_string(
                            "INSERT INTO " & GlobalVariables.targetStructure.character_queststatus_tbl(0) & " ( " &
                            GlobalVariables.targetStructure.qst_guid_col(0) & ", " &
                            GlobalVariables.targetStructure.qst_quest_col(0) & ", `" &
                            GlobalVariables.targetStructure.qst_status_col(0) & "`, `" &
                            GlobalVariables.targetStructure.qst_explored_col(0) & "` ) VALUES ( '" &
                            characterguid.ToString() & "', '" & qst.Id.ToString &
                            "', '" & queststatus & "', '" & qst.Explored.ToString & "')", True)
                    Next
                Else : LogAppend("No quests in questlog", "QuestCreation_createAtTrinity", False)
                End If
            Else : LogAppend("No quests in questlog", "QuestCreation_createAtTrinity", False)
            End If
            Dim finishedQuestsString As String = player.FinishedQuests
            If Not finishedQuestsString = "" Then
                Try
                    Dim parts() As String = finishedQuestsString.Split(","c)
                    Dim excounter As Integer = UBound(finishedQuestsString.Split(CChar(",")))
                    Dim startcounter As Integer = 0
                    Do
                        Dim questid As String = parts(startcounter)
                        runSQLCommand_characters_string(
                            "INSERT IGNORE INTO " & GlobalVariables.targetStructure.character_queststatus_rewarded_tbl(0) &
                            " ( `" & GlobalVariables.targetStructure.qstre_guid_col(0) & "`, `" &
                            GlobalVariables.targetStructure.qstre_quest_col(0) &
                            "` ) VALUES ( '" & characterguid.ToString() & "', '" & questid & "' )", True)
                        startcounter += 1
                    Loop Until startcounter = excounter
                Catch
                End Try
            End If
        End Sub

        Private Sub CreateAtMangos(ByVal characterguid As Integer, ByVal targetSetId As Integer, ByVal account As Account)
            LogAppend("Creating at Mangos", "QuestCreation_createAtMangos", False)
            Dim player As Character = GetCharacterSetBySetId(targetSetId, account)
            If Not player.Quests Is Nothing Then
                If Not player.Quests.Count = 0 Then
                    For Each qst As Quest In player.Quests
                        Dim queststatus As String = qst.Status.ToString
                        If queststatus = "0" Then queststatus = "1"
                        runSQLCommand_characters_string(
                            "INSERT INTO " & GlobalVariables.targetStructure.character_queststatus_tbl(0) & " ( " &
                            GlobalVariables.targetStructure.qst_guid_col(0) & ", " &
                            GlobalVariables.targetStructure.qst_quest_col(0) & ", `" &
                            GlobalVariables.targetStructure.qst_status_col(0) & "`, `" &
                            GlobalVariables.targetStructure.qst_explored_col(0) & "` ) VALUES ( '" &
                            characterguid.ToString() & "', '" & qst.Id.ToString &
                            "', '" & queststatus & "', '" & qst.Explored.ToString & "')", True)
                    Next
                Else : LogAppend("No quests in questlog", "QuestCreation_createAtMangos", False)
                End If
            Else : LogAppend("No quests in questlog", "QuestCreation_createAtMangos", False)
            End If
            Dim finishedQuestsString As String = player.FinishedQuests
            If Not finishedQuestsString = "" Then
                Try
                    Dim parts() As String = finishedQuestsString.Split(","c)
                    Dim excounter As Integer = UBound(finishedQuestsString.Split(CChar(",")))
                    Dim startcounter As Integer = 0
                    Do
                        Dim questid As String = parts(startcounter)
                        runSQLCommand_characters_string(
                            "INSERT INTO " & GlobalVariables.targetStructure.character_queststatus_tbl(0) & " ( " &
                            GlobalVariables.targetStructure.qst_guid_col(0) & ", " &
                            GlobalVariables.targetStructure.qst_quest_col(0) & ", `" &
                            GlobalVariables.targetStructure.qst_status_col(0) & "`, `" &
                            GlobalVariables.targetStructure.qst_rewarded_col(0) & "` ) VALUES ( '" &
                            characterguid.ToString() & "', '" & questid & "', '1', '1')", True)
                        startcounter += 1
                    Loop Until startcounter = excounter
                Catch
                End Try
            End If
        End Sub
    End Class
End Namespace