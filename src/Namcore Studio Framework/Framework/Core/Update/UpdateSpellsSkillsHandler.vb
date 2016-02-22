'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013-2016 NamCore Studio <https://github.com/megasus/Namcore-Studio>
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
'*      /Filename:      UpdateSpellsSkillsHandler
'*      /Description:   Handles character spells/skills update requests
'++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.Database
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Modules
Imports NCFramework.Framework.Transmission

Namespace Framework.Core.Update
    Public Class UpdateSpellsSkillsHandler
        Public Sub UpdateSpellsSkills(player As Character, modPlayer As Character)
            LogAppend("Updating character spells/skills", "UpdateSpellsSkillsHandler_UpdateSpellsSkills", True)
            '// Any new spells/skills?
            For Each spl As Spell In _
                From spl1 In modPlayer.Spells Let result = player.Spells.Find(Function(spell) spell.Id = spl1.Id)
                    Where result Is Nothing Select spl1
                AddSpells(spl.Id.ToString() & ",", modPlayer)
            Next
            For Each skl As Skill In modPlayer.Skills
                Dim result As Skill = player.Skills.Find(Function(skill) skill.Id = skl.Id)
                If result Is Nothing Then AddSkills(skl.ToString() & ",", modPlayer) : Continue For
                If skl.Value <> result.Value Or skl.Max <> result.Max Then
                    AddUpdateSkill(skl, modPlayer)
                End If
            Next
            '// Any deleted spells/skills?
            For Each spl As Spell In _
                From spl1 In player.Spells Let result = modPlayer.Spells.Find(Function(spell) spell.Id = spl1.Id)
                    Where result Is Nothing Select spl1
                DeleteSpell(modPlayer, spl)
            Next
            For Each skl As Skill In _
                From skl1 In player.Skills Let result = modPlayer.Skills.Find(Function(skill) skill.Id = skl1.Id)
                    Where result Is Nothing Select skl1
                DeleteSkill(modPlayer, skl)
            Next
        End Sub

        Public Sub DeleteSpell(player As Character, spell2Delete As Spell)
            Select Case GlobalVariables.sourceCore
                Case Modules.Core.TRINITY
                    runSQLCommand_characters_string(
                        "DELETE FROM " &
                        GlobalVariables.sourceStructure.character_spells_tbl(0) &
                        " WHERE `" & GlobalVariables.sourceStructure.spell_guid_col(0) & "` = '" &
                        player.Guid.ToString() & "' AND `" &
                        GlobalVariables.sourceStructure.spell_spell_col(0) &
                        "` = '" & spell2Delete.Id.ToString() & "'")
            End Select
        End Sub

        Public Sub DeleteRecipe(player As Character, spell2Delete As ProfessionSpell)
            Select Case GlobalVariables.sourceCore
                Case Modules.Core.TRINITY
                    runSQLCommand_characters_string(
                        "DELETE FROM " &
                        GlobalVariables.sourceStructure.character_spells_tbl(0) &
                        " WHERE `" & GlobalVariables.sourceStructure.spell_guid_col(0) & "` = '" &
                        player.Guid.ToString() & "' AND `" &
                        GlobalVariables.sourceStructure.spell_spell_col(0) &
                        "` = '" & spell2Delete.SpellId.ToString() & "'")
            End Select
        End Sub

        Public Sub DeleteSkill(player As Character, skill2Delete As Skill)
            Select Case GlobalVariables.sourceCore
                Case Modules.Core.TRINITY
                    runSQLCommand_characters_string(
                        "DELETE FROM " &
                        GlobalVariables.sourceStructure.character_skills_tbl(0) &
                        " WHERE `" & GlobalVariables.sourceStructure.skill_guid_col(0) & "` = '" &
                        player.Guid.ToString() & "' AND `" &
                        GlobalVariables.sourceStructure.skill_skill_col(0) &
                        "` = '" & skill2Delete.Id.ToString() & "'")
            End Select
        End Sub

        Public Sub DeleteProfession(player As Character, prof2Delete As Profession)
            Select Case GlobalVariables.sourceCore
                Case Modules.Core.TRINITY
                    runSQLCommand_characters_string(
                        "DELETE FROM " &
                        GlobalVariables.sourceStructure.character_skills_tbl(0) &
                        " WHERE `" & GlobalVariables.sourceStructure.skill_guid_col(0) & "` = '" &
                        player.Guid.ToString() & "' AND `" &
                        GlobalVariables.sourceStructure.skill_skill_col(0) &
                        "` = '" & prof2Delete.Id.ToString() & "'")
            End Select
        End Sub
    End Class
End Namespace