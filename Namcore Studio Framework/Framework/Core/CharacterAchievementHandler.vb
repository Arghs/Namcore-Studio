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
'*      /Filename:      CharacterAchievementHandler
'*      /Description:   Contains functions for extracting information about the character 
'*                      achievements
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.Database
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Modules

Namespace Framework.Core
    Public Class CharacterAchievementHandler
        Public Sub GetCharacterAchievement(ByVal characterGuid As Integer, ByVal setId As Integer,
                                           ByVal accountId As Integer)
            LogAppend("Loading character Achievement for characterGuid: " & characterGuid & " and setId: " & setId,
                      "CharacterAchievementsHandler_GetCharacterAchievement", True)
            Select Case GlobalVariables.sourceCore
                Case "arcemu"
                    LoadAtArcemu(characterGuid, setId)
                Case "trinity"
                    LoadAtTrinity(characterGuid, setId)
                Case "trinitytbc"
                    'todo LoadAtTrinityTBC(characterGuid, setId, accountId)
                Case "mangos"
                    LoadAtMangos(characterGuid, setId)

            End Select
        End Sub

        Private Sub LoadAtArcemu(ByVal charguid As Integer, ByVal tarSetId As Integer)
            LogAppend("Loading character Achievement @LoadAtArcemu", "CharacterAchievementHandler_LoadAtArcemu", False)
            Dim tmpCharacter As Character = GetCharacterSetBySetId(tarSetId)
            Dim tempdt As DataTable =
                    ReturnDataTable(
                        "SELECT " & GlobalVariables.sourceStructure.av_achievement_col(0) & ", `" &
                        GlobalVariables.sourceStructure.av_date_col(0) & "` FROM " &
                        GlobalVariables.sourceStructure.character_achievement_tbl(0) &
                        "` WHERE " & GlobalVariables.sourceStructure.av_guid_col(0) & "='" & charguid.ToString() & "'")
            If tmpCharacter.Achievements Is Nothing Then tmpCharacter.Achievements = New List(Of Achievement)()
            Try
                Dim lastcount As Integer = tempdt.Rows.Count
                Dim count As Integer = 0
                If Not lastcount = 0 Then
                    Do
                        Dim tmpAv As New Achievement
                        tmpAv.Id = TryInt((tempdt.Rows(count).Item(0)).ToString)
                        tmpAv.OwnerSet = tarSetId
                        tmpAv.GainDate = (tempdt.Rows(count).Item(1)).ToString
                        tmpCharacter.Achievements.Add(tmpAv)
                        count += 1
                    Loop Until count = lastcount
                Else
                    LogAppend("No Achievements found!", "CharacterAchievementHandler_LoadAtArcemu", True)
                End If
            Catch ex As Exception
                LogAppend(
                    "Something went wrong while loading character Achievements! -> skipping -> Exception is: ###START###" &
                    ex.ToString() & "###END###", "CharacterAchievementHandler_LoadAtArcemu", True, True)
                Exit Sub
            End Try
            SetCharacterSet(tarSetId, tmpCharacter)
        End Sub

        Private Sub LoadAtTrinity(ByVal charguid As Integer, ByVal tarSetId As Integer)
            LogAppend("Loading character Achievement @LoadAtTrinity", "CharacterAchievementHandler_LoadAtTrinity", False)
            Dim tmpCharacter As Character = GetCharacterSetBySetId(tarSetId)
            Dim tempdt As DataTable =
                    ReturnDataTable(
                        "SELECT " & GlobalVariables.sourceStructure.av_achievement_col(0) & ", `" &
                        GlobalVariables.sourceStructure.av_date_col(0) & "` FROM " &
                        GlobalVariables.sourceStructure.character_achievement_tbl(0) &
                        "` WHERE " & GlobalVariables.sourceStructure.av_guid_col(0) & "='" & charguid.ToString() & "'")
            If tmpCharacter.Achievements Is Nothing Then tmpCharacter.Achievements = New List(Of Achievement)()
            Try
                Dim lastcount As Integer = tempdt.Rows.Count
                Dim count As Integer = 0
                If Not lastcount = 0 Then
                    Do
                        Dim tmpAv As New Achievement
                        tmpAv.Id = TryInt((tempdt.Rows(count).Item(0)).ToString)
                        tmpAv.OwnerSet = tarSetId
                        tmpAv.GainDate = (tempdt.Rows(count).Item(1)).ToString
                        tmpCharacter.Achievements.Add(tmpAv)
                        count += 1
                    Loop Until count = lastcount
                Else
                    LogAppend("No Achievement found!", "CharacterAchievementHandler_LoadAtTrinity", True)
                End If
            Catch ex As Exception
                LogAppend(
                    "Something went wrong while loading character Achievement! -> skipping -> Exception is: ###START###" &
                    ex.ToString() & "###END###", "CharacterAchievementHandler_LoadAtTrinity", True, True)
                Exit Sub
            End Try
            SetCharacterSet(tarSetId, tmpCharacter)
        End Sub

        Private Sub LoadAtMangos(ByVal charguid As Integer, ByVal tarSetId As Integer)
            LogAppend("Loading character Achievement @LoadAtMangos", "CharacterAchievementHandler_LoadAtMangos", False)
            Dim tmpCharacter As Character = GetCharacterSetBySetId(tarSetId)
            Dim tempdt As DataTable =
                    ReturnDataTable(
                        "SELECT " & GlobalVariables.sourceStructure.av_achievement_col(0) & ", `" &
                        GlobalVariables.sourceStructure.av_date_col(0) & "` FROM " &
                        GlobalVariables.sourceStructure.character_achievement_tbl(0) &
                        "` WHERE " & GlobalVariables.sourceStructure.av_guid_col(0) & "='" & charguid.ToString() & "'")
            If tmpCharacter.Achievements Is Nothing Then tmpCharacter.Achievements = New List(Of Achievement)()
            Try
                Dim lastcount As Integer = tempdt.Rows.Count
                Dim count As Integer = 0
                If Not lastcount = 0 Then
                    Do
                        Dim tmpAv As New Achievement
                        tmpAv.Id = TryInt((tempdt.Rows(count).Item(0)).ToString)
                        tmpAv.OwnerSet = tarSetId
                        tmpAv.GainDate = (tempdt.Rows(count).Item(1)).ToString
                        tmpCharacter.Achievements.Add(tmpAv)
                        count += 1
                    Loop Until count = lastcount
                Else
                    LogAppend("No Achievement found!", "CharacterAchievementHandler_LoadAtMangos", True)
                End If
            Catch ex As Exception
                LogAppend(
                    "Something went wrong while loading character Achievement! -> skipping -> Exception is: ###START###" &
                    ex.ToString() & "###END###", "CharacterAchievementHandler_LoadAtMangos", True, True)
                Exit Sub
            End Try
            SetCharacterSet(tarSetId, tmpCharacter)
        End Sub
    End Class
End Namespace