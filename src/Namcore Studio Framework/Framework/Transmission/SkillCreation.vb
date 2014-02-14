'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013-2014 NamCore Studio <https://github.com/megasus/Namcore-Studio>
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
'*      /Filename:      SkillCreation
'*      /Description:   Includes functions for setting up the known skills of a specific
'*                      character
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.Database
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Modules

Namespace Framework.Transmission
    Public Module SkillCreation
        Public Sub AddSkills(ByVal skillsstring As String, ByVal player As Character,
                             Optional forceTargetCore As Boolean = False)
            'TODO
            Dim useCore As String
            Dim useStructure As DbStructure
            If forceTargetCore Then
                useCore = GlobalVariables.targetCore
                useStructure = GlobalVariables.targetStructure
            Else
                useCore = GlobalVariables.sourceCore
                useStructure = GlobalVariables.sourceStructure
            End If
            Dim mySkills() As String = skillsstring.Split(","c)
            Dim skillCount As Integer = UBound(Split(skillsstring, ","))
            For i = 0 To skillCount - 1
                Dim mySkill As String = mySkills(i)
                LogAppend("Adding Skill " & mySkill, "SkillCreation_AddSkills")
                Select Case useCore
                    Case "trinity"
                        runSQLCommand_characters_string(
                            "INSERT IGNORE INTO `" & useStructure.character_skills_tbl(0) & "`( `" &
                            useStructure.skill_guid_col(0) & "`, `" &
                            useStructure.skill_skill_col(0) & "`, `" &
                            useStructure.skill_value_col(0) & "`, `" &
                            useStructure.skill_max_col(0) &
                            "` ) VALUES ( '" &
                            player.CreatedGuid.ToString & "', '" &
                            mySkill & "', '1', '1' )", forceTargetCore)
                End Select
            Next
        End Sub

        Public Sub AddUpdateSkill(ByVal skill As Skill, ByVal player As Character,
                          Optional forceTargetCore As Boolean = False)
            'TODO
            Dim useCore As String
            Dim useStructure As DbStructure
            If forceTargetCore Then
                useCore = GlobalVariables.targetCore
                useStructure = GlobalVariables.targetStructure
            Else
                useCore = GlobalVariables.sourceCore
                useStructure = GlobalVariables.sourceStructure
            End If
            LogAppend("Adding Skill " & skill.Id.ToString(), "SkillCreation_AddUpdateSkill")
            Select Case useCore
                Case "trinity"
                    runSQLCommand_characters_string(
                        "INSERT IGNORE INTO `" & useStructure.character_skills_tbl(0) & "` ( `" &
                        useStructure.skill_guid_col(0) & "`, `" &
                        useStructure.skill_skill_col(0) & "`, `" &
                        useStructure.skill_value_col(0) & "`, `" &
                        useStructure.skill_max_col(0) &
                        "` ) VALUES ( '" &
                        player.CreatedGuid.ToString & "', '" &
                        skill.Id.ToString() & "', '" & skill.Value.ToString() & "', '" & skill.Max.ToString() & "' ) on duplicate key update `" &
                        useStructure.skill_value_col(0) & "`=values(`" &
                        useStructure.skill_value_col(0) & "`), `" &
                        useStructure.skill_max_col(0) & "`=values(`" &
                        useStructure.skill_max_col(0) & "`)", forceTargetCore)
            End Select
        End Sub

        Public Sub AddProfession(ByVal prof As Profession, ByVal player As Character,
                         Optional forceTargetCore As Boolean = False)
            'TODO
            Dim useCore As String
            Dim useStructure As DbStructure
            If forceTargetCore Then
                useCore = GlobalVariables.targetCore
                useStructure = GlobalVariables.targetStructure
            Else
                useCore = GlobalVariables.sourceCore
                useStructure = GlobalVariables.sourceStructure
            End If
            LogAppend("Adding Skill " & prof.Id.ToString(), "SkillCreation_AddUpdateSkill")
            Select Case useCore
                Case "trinity"
                    runSQLCommand_characters_string(
                        "INSERT IGNORE INTO `" & useStructure.character_skills_tbl(0) & "` ( `" &
                        useStructure.skill_guid_col(0) & "`, `" &
                        useStructure.skill_skill_col(0) & "`, `" &
                        useStructure.skill_value_col(0) & "`, `" &
                        useStructure.skill_max_col(0) &
                        "` ) VALUES ( '" &
                        player.CreatedGuid.ToString & "', '" &
                        prof.Id.ToString() & "', '" & prof.Rank.ToString() & "', '" & prof.Max.ToString() & "' ) on duplicate key update `" &
                        useStructure.skill_value_col(0) & "`=values(`" &
                        useStructure.skill_value_col(0) & "`), `" &
                        useStructure.skill_max_col(0) & "`=values(`" &
                        useStructure.skill_max_col(0) & "`)", forceTargetCore)
            End Select
        End Sub

        Public Sub AddCharacterSkills(ByVal player As Character)
            'TODO
            Select Case GlobalVariables.targetCore
                Case "trinity", "mangos"
                    If Not player.Skills Is Nothing Then
                        For Each skl As Skill In player.Skills
                            LogAppend("Adding Skill " & skl.Id, "SkillCreation_AddCharacterSkills")
                            runSQLCommand_characters_string(
                                "INSERT IGNORE INTO `" & GlobalVariables.targetStructure.character_skills_tbl(0) & "`( `" &
                                GlobalVariables.targetStructure.skill_guid_col(0) & "`, `" &
                                GlobalVariables.targetStructure.skill_skill_col(0) & "`, `" &
                                GlobalVariables.targetStructure.skill_value_col(0) & "`, `" &
                                GlobalVariables.targetStructure.skill_max_col(0) &
                                "` ) VALUES ( '" &
                                player.CreatedGuid.ToString & "', '" &
                                skl.Id.ToString() & "', '" & skl.Value.ToString() & "', '" & skl.Max.ToString() & "' )",
                                True)
                        Next
                    End If
                    If Not player.Professions Is Nothing Then
                        For Each skl As Profession In player.Professions
                            LogAppend("Adding profession " & skl.Id, "SkillCreation_AddCharacterSkills")
                            runSQLCommand_characters_string(
                                "INSERT IGNORE INTO `" & GlobalVariables.targetStructure.character_skills_tbl(0) & "`( `" &
                                GlobalVariables.targetStructure.skill_guid_col(0) & "`, `" &
                                GlobalVariables.targetStructure.skill_skill_col(0) & "`, `" &
                                GlobalVariables.targetStructure.skill_value_col(0) & "`, `" &
                                GlobalVariables.targetStructure.skill_max_col(0) &
                                "` ) VALUES ( '" &
                                player.CreatedGuid.ToString & "', '" &
                                skl.Id.ToString() & "', '" & skl.Rank.ToString() & "', '" & skl.Max.ToString() & "' )",
                                True)
                        Next
                    End If
            End Select
        End Sub
    End Module
End Namespace