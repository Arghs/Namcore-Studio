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
'*      /Filename:      UpdateProfessionsHandler
'*      /Description:   Handles character profession update requests
'++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.Extension
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Modules
Imports NCFramework.Framework.Transmission

Namespace Framework.Core.Update
    Public Class UpdateProfessionsHandler
        Public Sub UpdateProfessions(player As Character, modPlayer As Character)
            LogAppend("Updating character professions", "UpdateProfessionsHandler_UpdateProfessions", True)
            Dim skillSpellUpdateHandler As New UpdateSpellsSkillsHandler
            Dim profCreateHandler As New ProfessionCreation
            '// Any new professions?
            For Each prof As Profession In modPlayer.Professions
                Dim result As Profession = player.Professions.Find(Function(profession) profession.Id = prof.Id)
                If result Is Nothing Then profCreateHandler.AddUpdateProfession(prof, modPlayer, True) : Continue For
                If prof.Max <> result.Max Or prof.Rank <> result.Rank Then
                    profCreateHandler.AddUpdateProfession(prof, modPlayer, False)
                End If
                If Not prof.RecipeListsIdentical(result.Recipes) Then
                    '// Any deleted recipes?
                    If Not result.Recipes Is Nothing Then
                        For Each recipe As ProfessionSpell In result.Recipes
                            Dim recipeResult As ProfessionSpell =
                                    prof.Recipes.Find(Function(professionspell) professionspell.SpellId = recipe.SpellId)
                            If recipeResult Is Nothing Then
                                skillSpellUpdateHandler.DeleteRecipe(modPlayer, recipe)
                            End If
                        Next
                    End If
                    '// Any new recipes?
                    If Not prof.Recipes Is Nothing Then
                        For Each recipe As ProfessionSpell In prof.Recipes
                            Dim recipeResult As ProfessionSpell =
                                    result.Recipes.Find(
                                        Function(professionspell) professionspell.SpellId = recipe.SpellId)
                            If recipeResult Is Nothing Then
                                AddSpells(recipe.SpellId.ToString() & ",", modPlayer)
                            End If
                        Next
                    End If
                End If
            Next
            '// Any deleted professions?
            For Each prof As Profession In _
                From prof1 In player.Professions
                    Let result = modPlayer.Professions.Find(Function(profession) profession.Id = prof1.Id)
                    Where result Is Nothing Select prof1
                If Not prof.Recipes Is Nothing Then
                    For Each recipe As ProfessionSpell In prof.Recipes
                        skillSpellUpdateHandler.DeleteRecipe(modPlayer, recipe)
                    Next
                End If
                skillSpellUpdateHandler.DeleteProfession(modPlayer, prof)
            Next
        End Sub
    End Class
End Namespace