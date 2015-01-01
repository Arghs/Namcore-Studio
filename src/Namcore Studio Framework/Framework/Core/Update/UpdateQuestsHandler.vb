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
'*      /Filename:      UpdateQuestsHandler
'*      /Description:   Handles character glyph update requests
'++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.Database
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Modules

Namespace Framework.Core.Update
    Public Class UpdateQuestsHandler
        '// Declaration
        Dim _excounter As Integer
        Dim _quests() As String
        '// Declaration

        Public Sub UpdateQuestlog(ByVal player As Character, ByVal modPlayer As Character)
            LogAppend("Updating character questlog", "UpdateQuestsHandler_UpdateQuestlog", True)
            '// Any new quests?
            For Each qst As Quest In modPlayer.Quests
                Dim result As Quest = player.Quests.Find(Function(quest) quest.Id = qst.Id)
                If result Is Nothing Then CreateQuest(modPlayer, qst) : Continue For
                If result.Status <> qst.Status Or result.Explored <> qst.Explored Then CreateQuest(modPlayer, qst, True)
            Next
            If Not modPlayer.FinishedQuests Is Nothing Then
                _excounter = UBound(Split(modPlayer.FinishedQuests, ","))
                _quests = modPlayer.FinishedQuests.Split(","c)
                For i = 0 To _excounter - 1
                    Dim thisQuest As String = _quests(i)
                    If _
                        Not player.FinishedQuests.StartsWith(thisQuest & ",") AndAlso
                        Not player.FinishedQuests.Contains("," & thisQuest & ",") Then
                        CreateQuest(modPlayer, New Quest With {.Id = TryInt(thisQuest), .Status = 1, .Rewarded = 1})
                    End If
                Next
            End If
            '// Any deleted quests?
            For Each qst As Quest In player.Quests
                Dim result As Quest = modPlayer.Quests.Find(Function(quest) quest.Id = qst.Id)
                If result Is Nothing Then DeleteQuest(modPlayer, qst)
            Next
            If Not player.FinishedQuests Is Nothing Then
                _excounter = UBound(Split(player.FinishedQuests, ","))
                _quests = player.FinishedQuests.Split(","c)
                For i = 0 To _excounter - 1
                    Dim thisQuest As String = _quests(i)
                    If _
                        Not modPlayer.FinishedQuests.StartsWith(thisQuest & ",") AndAlso
                        Not modPlayer.FinishedQuests.Contains("," & thisQuest & ",") Then
                        DeleteQuest(modPlayer, New Quest With {.Id = TryInt(thisQuest)})
                    End If
                Next
            End If
        End Sub

        Private Sub CreateQuest(ByVal player As Character, ByVal qst2Add As Quest, Optional update As Boolean = False)
            Select Case GlobalVariables.sourceCore
                Case Modules.Core.TRINITY
                    If qst2Add.Rewarded = 1 Then
                        runSQLCommand_characters_string(
                            "INSERT IGNORE INTO " &
                            GlobalVariables.targetStructure.character_queststatus_rewarded_tbl(0) &
                            " ( `" & GlobalVariables.targetStructure.qstre_guid_col(0) & "`, `" &
                            GlobalVariables.targetStructure.qstre_quest_col(0) &
                            "` ) VALUES ( '" & player.Guid.ToString() & "', '" & qst2Add.Id.ToString() & "' )")
                    Else
                        If update = True Then
                            runSQLCommand_characters_string(
                                "UPDATE " & GlobalVariables.targetStructure.character_queststatus_tbl(0) & " SET ( " &
                                GlobalVariables.targetStructure.qst_quest_col(0) & ", `" &
                                GlobalVariables.targetStructure.qst_status_col(0) & "`, `" &
                                GlobalVariables.targetStructure.qst_explored_col(0) & "` ) VALUES (  '" &
                                qst2Add.Id.ToString & "', '" & qst2Add.Status.ToString() &
                                "', '" & qst2Add.Explored.ToString & "')  WHERE `" &
                                GlobalVariables.targetStructure.qst_guid_col(0) & "` = '" & player.Guid.ToString() &
                                "' AND `" &
                                GlobalVariables.targetStructure.qst_quest_col(0) &
                                "` = '" & qst2Add.Id.ToString() & "'")
                        Else
                            runSQLCommand_characters_string(
                                "INSERT INTO " & GlobalVariables.targetStructure.character_queststatus_tbl(0) & " ( " &
                                GlobalVariables.targetStructure.qst_guid_col(0) & ", " &
                                GlobalVariables.targetStructure.qst_quest_col(0) & ", `" &
                                GlobalVariables.targetStructure.qst_status_col(0) & "`, `" &
                                GlobalVariables.targetStructure.qst_explored_col(0) & "` ) VALUES ( '" &
                                player.Guid.ToString() & "', '" & qst2Add.Id.ToString &
                                "', '" & qst2Add.Status.ToString() & "', '" & qst2Add.Explored.ToString & "')")
                        End If
                    End If
            End Select
        End Sub

        Private Sub DeleteQuest(ByVal player As Character, ByVal quest2Delete As Quest)
            Select Case GlobalVariables.sourceCore
                Case Modules.Core.TRINITY
                    If quest2Delete.Rewarded = 1 Then
                        runSQLCommand_characters_string(
                            "DELETE FROM " &
                            GlobalVariables.targetStructure.character_queststatus_rewarded_tbl(0) &
                            " WHERE `" & GlobalVariables.targetStructure.qstre_guid_col(0) & "` = '" &
                            player.Guid.ToString() & "' AND `" &
                            GlobalVariables.targetStructure.qstre_quest_col(0) &
                            "` = '" & quest2Delete.Id.ToString() & "'")
                    Else
                        runSQLCommand_characters_string(
                            "DELETE FROM " &
                            GlobalVariables.targetStructure.character_queststatus_tbl(0) &
                            " WHERE `" & GlobalVariables.targetStructure.qst_guid_col(0) & "` = '" &
                            player.Guid.ToString() & "' AND `" &
                            GlobalVariables.targetStructure.qst_quest_col(0) &
                            "` = '" & quest2Delete.Id.ToString() & "'")
                    End If
            End Select
        End Sub
    End Class
End Namespace