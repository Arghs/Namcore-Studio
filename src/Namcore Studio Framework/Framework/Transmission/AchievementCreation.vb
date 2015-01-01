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
'*      /Filename:      AchievementCreation
'*      /Description:   Includes functions for creating character achievements
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.Database
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Modules

Namespace Framework.Transmission
    Public Class AchievementCreation
        Public Sub AddCharacterAchievements(ByVal player As Character, Optional charguid As Integer = 0)
            If charguid = 0 Then charguid = player.Guid
            LogAppend("Adding achievements for character: " & charguid.ToString(),
                      "AchievementCreation_SetCharacterAchievements", True)
            Select Case GlobalVariables.sourceCore
                Case Modules.Core.ARCEMU, Modules.Core.TRINITY, Modules.Core.MANGOS
                    For Each av As Achievement In player.Achievements
                        runSQLCommand_characters_string(
                            "INSERT INTO `" & GlobalVariables.sourceStructure.character_achievement_tbl(0) &
                            "` ( `" & GlobalVariables.sourceStructure.av_guid_col(0) &
                            "`, `" & GlobalVariables.sourceStructure.av_achievement_col(0) &
                            "`, `" & GlobalVariables.sourceStructure.av_date_col(0) &
                            "` ) VALUES ( '" & charguid.ToString() &
                            "', '" & av.Id.ToString() &
                            "', '" & av.GainDate.ToString() &
                            "' )")
                    Next
            End Select
        End Sub
    End Class
End Namespace