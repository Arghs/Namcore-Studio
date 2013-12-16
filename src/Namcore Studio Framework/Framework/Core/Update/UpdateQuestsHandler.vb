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
'*      /Filename:      UpdateQuestsHandler
'*      /Description:   Handles character glyph update requests
'++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Modules
Imports NCFramework.Framework.Functions

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
                If result Is Nothing Then CreateQuest(modPlayer, qst)
            Next
            If Not modPlayer.FinishedQuests Is Nothing Then
                _excounter = UBound(Split(modPlayer.FinishedQuests, ","))
                _quests = modPlayer.FinishedQuests.Split(","c)
                For i = 0 To _excounter - 1
                    Dim thisQuest As String = _quests(i)
                    If Not player.FinishedQuests.StartsWith(thisQuest & ",") AndAlso Not player.FinishedQuests.Contains("," & thisQuest & ",") Then
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
                    If Not modPlayer.FinishedQuests.StartsWith(thisQuest & ",") AndAlso Not modPlayer.FinishedQuests.Contains("," & thisQuest & ",") Then
                        DeleteQuest(modPlayer, New Quest With {.Id = TryInt(thisQuest)})
                    End If
                Next
            End If
        End Sub

        Private Sub CreateQuest(ByVal player As Character, ByVal qst2Add As Quest)
            Select Case GlobalVariables.sourceCore
                Case "trinity"

            End Select
        End Sub

        Private Sub DeleteQuest(ByVal player As Character, ByVal quest2Delete As Quest)
            Select Case GlobalVariables.sourceCore
                Case "trinity"

            End Select
        End Sub
    End Class
End Namespace