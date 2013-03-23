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

Imports Namcore_Studio.EventLogging
Imports Namcore_Studio.Basics
Imports Namcore_Studio.GlobalVariables
Imports Namcore_Studio.CommandHandler
Imports Namcore_Studio.Conversions
Public Class CharacterAchievementHandler
    Public Shared Sub GetCharacterAchievement(ByVal characterGuid As Integer, ByVal setId As Integer, ByVal accountId As Integer)
        LogAppend("Loading character Achievement for characterGuid: " & characterGuid & " and setId: " & setId, "CharacterAchievementsHandler_GetCharacterAchievement", True)
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
    Private Shared Sub loadAtArcemu(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)
        LogAppend("Loading character Achievement @loadAtArcemu", "CharacterAchievementHandler_loadAtArcemu", False)
        Dim tempdt As DataTable = ReturnDataTable("SELECT " & sourceStructure.av_achievement_col(0) & ", `" & sourceStructure.av_date_col(0) & "` FROM " & sourceStructure.character_achievement_tbl(0) & "` WHERE " & sourceStructure.av_guid_col(0) & "='" & charguid.ToString() & "'")
        Dim templist As New List(Of String)
        Try
            Dim lastcount As Integer = tryint(Val(tempdt.Rows.Count.ToString))
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim avid As String = (tempdt.Rows(count).Item(0)).ToString
                    Dim xdate As String = (tempdt.Rows(count).Item(1)).ToString
                    templist.Add("<av>" & avid & "</av><date>" & xdate & "</date>")
                    count += 1
                Loop Until count = lastcount
            Else
                LogAppend("No Achievements found!", "CharacterAchievementHandler_loadAtArcemu", True)
            End If
        Catch ex As Exception
            LogAppend("Something went wrong while loading character Achievements! -> skipping -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterAchievementHandler_loadAtArcemu", True, True)
            Exit Sub
        End Try
        SetTemporaryCharacterInformation("@character_achievements", ConvertListToString(templist), tar_setId)
    End Sub
    Private Shared Sub loadAtTrinity(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)
        LogAppend("Loading character Achievement @loadAtTrinity", "CharacterAchievementHandler_loadAtTrinity", False)
        Dim tempdt As DataTable = ReturnDataTable("SELECT " & sourceStructure.av_achievement_col(0) & ", `" & sourceStructure.av_date_col(0) & "` FROM " & sourceStructure.character_achievement_tbl(0) & "` WHERE " & sourceStructure.av_guid_col(0) & "='" & charguid.ToString() & "'")
        Dim templist As New List(Of String)
        Try
            Dim lastcount As Integer = tryint(Val(tempdt.Rows.Count.ToString))
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim avid As String = (tempdt.Rows(count).Item(0)).ToString
                    Dim xdate As String = (tempdt.Rows(count).Item(1)).ToString
                    templist.Add("<av>" & avid & "</av><date>" & xdate & "</date>")
                    count += 1
                Loop Until count = lastcount
            Else
                LogAppend("No Achievement found!", "CharacterAchievementHandler_loadAtTrinity", True)
            End If
        Catch ex As Exception
            LogAppend("Something went wrong while loading character Achievement! -> skipping -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterAchievementHandler_loadAtTrinity", True, True)
            Exit Sub
        End Try
        SetTemporaryCharacterInformation("@character_achievements", ConvertListToString(templist), tar_setId)
    End Sub
    Private Shared Sub loadAtTrinityTBC(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)

    End Sub
    Private Shared Sub loadAtMangos(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)
        LogAppend("Loading character Achievement @loadAtMangos", "CharacterAchievementHandler_loadAtMangos", False)
        Dim tempdt As DataTable = ReturnDataTable("SELECT " & sourceStructure.av_achievement_col(0) & ", `" & sourceStructure.av_date_col(0) & "` FROM " & sourceStructure.character_achievement_tbl(0) & "` WHERE " & sourceStructure.av_guid_col(0) & "='" & charguid.ToString() & "'")
        Dim templist As New List(Of String)
        Try
            Dim lastcount As Integer = tryint(Val(tempdt.Rows.Count.ToString))
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim avid As String = (tempdt.Rows(count).Item(0)).ToString
                    Dim xdate As String = (tempdt.Rows(count).Item(1)).ToString
                    templist.Add("<av>" & avid & "</av><date>" & xdate & "</date>")
                    count += 1
                Loop Until count = lastcount
            Else
                LogAppend("No Achievement found!", "CharacterAchievementHandler_loadAtMangos", True)
            End If
        Catch ex As Exception
            LogAppend("Something went wrong while loading character Achievement! -> skipping -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterAchievementHandler_loadAtMangos", True, True)
            Exit Sub
        End Try
        SetTemporaryCharacterInformation("@character_achievements", ConvertListToString(templist), tar_setId)
    End Sub
End Class
