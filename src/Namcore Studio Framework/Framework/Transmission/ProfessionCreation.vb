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
'*      /Filename:      ProfessionCreation
'*      /Description:   Includes functions for adding professions of a specific
'*                      character
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Database
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Modules

Namespace Framework.Transmission
    Public Class ProfessionCreation
        Public Sub AddCharacterProfessions(ByVal player As Character)
            LogAppend("Adding character professions", "ProfessionCreation_AddCharacterProfessions")
            If Not player.Professions Is Nothing Then
                For Each prof As Profession In player.Professions
                    AddUpdateProfession(prof, player, True, True)
                Next
            End If
        End Sub

        Public Sub AddUpdateProfession(ByVal prof As Profession, ByVal player As Character, ByVal newAdding As Boolean,
                          Optional forceTargetCore As Boolean = False)
            'TODO
            Dim useCore As Modules.Core
            Dim useStructure As DbStructure
            If forceTargetCore Then
                useCore = GlobalVariables.targetCore
                useStructure = GlobalVariables.targetStructure
            Else
                useCore = GlobalVariables.sourceCore
                useStructure = GlobalVariables.sourceStructure
            End If
            LogAppend("Adding Profession " & prof.Id.ToString(), "ProfessionCreation_AddUpdateProfession")
            If newAdding Then
                AddProfession(prof, player, forceTargetCore)
                If Not prof.Recipes Is Nothing Then
                    For Each recipe As ProfessionSpell In prof.Recipes
                        AddSpells(recipe.SpellId.ToString() & ",", player, forceTargetCore)
                    Next
                End If
            Else
                AddProfession(prof, player, forceTargetCore)
                If Not prof.Recipes Is Nothing Then
                    For Each recipe As ProfessionSpell In prof.Recipes
                        Select Case useCore
                            Case Modules.Core.TRINITY
                                runSQLCommand_characters_string(
                                    "INSERT IGNORE INTO `" & useStructure.character_spells_tbl(0) & "` ( `" &
                                    useStructure.spell_guid_col(0) & "`, `" &
                                    useStructure.spell_spell_col(0) & "`, `" &
                                    useStructure.spell_active_col(0) & "`, `" &
                                    useStructure.spell_disabled_col(0) &
                                    "` ) VALUES ( '" &
                                    player.CreatedGuid.ToString & "', '" &
                                    recipe.SpellId.ToString() & "', '1', '0' ) on duplicate key update `" &
                                    useStructure.spell_active_col(0) & "`=values(`" &
                                    useStructure.spell_active_col(0) & "`), `" &
                                    useStructure.spell_disabled_col(0) & "`=values(`" &
                                    useStructure.spell_disabled_col(0) & "`)", forceTargetCore)
                        End Select
                    Next
                End If

            End If
        End Sub
    End Class
End Namespace