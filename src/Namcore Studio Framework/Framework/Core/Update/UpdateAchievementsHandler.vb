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
'*      /Filename:      UpdateAchievementsHandler
'*      /Description:   Handles character achievement update requests
'++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.Database
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Modules

Namespace Framework.Core.Update
    Public Class UpdateAchievementsHandler
        Public Sub UpdateAchievements(ByVal player As Character, ByVal modPlayer As Character)
            LogAppend("Updating character Achievements", "UpdateAchievementsHandler_UpdateAchievements", True)
            If GlobalVariables.sourceExpansion < 3 Then
                '// Cannot create Achievements in pre WotLK db
                LogAppend("Cannot create Achievements in pre WotLK db!", "UpdateAchievementsHandler_UpdateAchievements",
                          True, True)
                Exit Sub
            End If
            '// Any new Achievements?
            For Each playerAv As Achievement In _
                From playerAv1 In modPlayer.Achievements
                    Let result = player.Achievements.Find(Function(achievement) achievement.Id = playerAv1.Id)
                    Where result Is Nothing Select playerAv1
                CreateAchievement(modPlayer, playerAv)
            Next
            '// Any deleted Achievements?
            For Each playerAv In _
                From playerAv1 In player.Achievements
                    Let result = modPlayer.Achievements.Find(Function(achievement) achievement.Id = playerAv1.Id)
                    Where result Is Nothing Select playerAv1
                RemoveAchievement(modPlayer, playerAv)
            Next
        End Sub

        Private Sub CreateAchievement(ByVal newPlayer As Character, ByVal av As Achievement)
            LogAppend("Creating achievement with id " & av.Id.ToString(), "UpdateAchievementsHandler_CreateAchievement",
                      True)
            Select Case GlobalVariables.sourceCore
                Case Modules.Core.ARCEMU, Modules.Core.TRINITY, Modules.Core.MANGOS
                    runSQLCommand_characters_string(
                        "INSERT INTO `" & GlobalVariables.sourceStructure.character_achievement_tbl(0) &
                        "` ( `" & GlobalVariables.sourceStructure.av_guid_col(0) &
                        "`, `" & GlobalVariables.sourceStructure.av_achievement_col(0) &
                        "`, `" & GlobalVariables.sourceStructure.av_date_col(0) &
                        "` ) VALUES ( '" & newPlayer.Guid.ToString() &
                        "', '" & av.Id.ToString() &
                        "', '" & av.GainDate.ToString() &
                        "' )")
            End Select
        End Sub

        Private Sub RemoveAchievement(ByVal newPlayer As Character, ByVal av As Achievement)
            LogAppend("Removing achievement with id " & av.Id.ToString(), "UpdateAchievementsHandler_RemoveAchievement",
                      True)
            Select Case GlobalVariables.sourceCore
                Case Modules.Core.ARCEMU, Modules.Core.TRINITY, Modules.Core.MANGOS
                    runSQLCommand_characters_string(
                        "DELETE FROM `" & GlobalVariables.sourceStructure.character_achievement_tbl(0) &
                        "` WHERE `" & GlobalVariables.sourceStructure.av_guid_col(0) &
                        "` = '" & newPlayer.Guid.ToString() &
                        "' AND `" & GlobalVariables.sourceStructure.av_achievement_col(0) &
                        "` = '" & av.Id.ToString() & "'")
            End Select
        End Sub
    End Class
End Namespace