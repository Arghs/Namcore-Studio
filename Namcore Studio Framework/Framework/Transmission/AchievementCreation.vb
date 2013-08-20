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
'*      /Filename:      AchievementCreation
'*      /Description:   Includes functions for creating character achievements
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Imports Namcore_Studio.EventLogging
Imports Namcore_Studio.CommandHandler
Imports Namcore_Studio.GlobalVariables
Imports Namcore_Studio.Basics
Imports Namcore_Studio.Conversions
Public Class AchievementCreation
    Public Shared Sub SetCharacterAchievements(ByVal setId As Integer, Optional charguid As Integer = 0)
        If charguid = 0 Then charguid = characterGUID
        LogAppend("Setting Achievements for character: " & charguid.ToString() & " // setId is : " & setId.ToString(), "AchievementCreation_SetCharacterAchievements", True)
        Dim characterAvList As List(Of String) = ConvertStringToList(GetTemporaryCharacterInformation("@character_achievement_list", setId))
        For Each avstring As String In characterAvList
            runSQLCommand_characters_string("INSERT INTO character_achievement ( guid, achievement, date ) VALUES ( '" & charguid.ToString() & "', '" & splitList(avstring, "av") & "', '" &
                                            splitList(avstring, "date") & "')")
        Next
    End Sub
    Public Shared Sub AddCharacterAchievement(ByVal setId As Integer, ByVal avid As Integer, Optional charguid As Integer = 0)
        If charguid = 0 Then charguid = characterGUID
        LogAppend("Adding Achievement " & avid.ToString() & " for character: " & charguid.ToString() & " // setId is : " & setId.ToString(), "AchievementCreation_AddCharacterAchievement", True)
        Dim characterAvList As List(Of String) = ConvertStringToList(GetTemporaryCharacterInformation("@character_achievement_list", setId))
        runSQLCommand_characters_string("INSERT INTO character_achievement ( guid, achievement, date ) VALUES ( '" & charguid.ToString() & "', '" & avid.ToString() & "', '" &
                                        (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds.ToString() & "')")
    End Sub
End Class
