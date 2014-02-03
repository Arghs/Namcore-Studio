
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
'*      /Filename:      ProfessionExtension
'*      /Description:   Extension to compare professions
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.Runtime.CompilerServices
Imports NCFramework.Framework.Modules

Namespace Framework.Extension

    Module ProfessionExtension
        ''' <summary>
        '''     Updates reputation standing by value and status
        ''' </summary>
        <Extension()>
        Public Function RecipeListsIdentical(ByVal prof As Profession,
                                             ByVal recipeListB As List(Of ProfessionSpell)) As Boolean

            If prof.Recipes Is Nothing Then
                If recipeListB Is Nothing Then
                    Return True
                Else
                    Return False
                End If
            Else
                If recipeListB Is Nothing Then Return False
            End If
            If _
                (From recipe In prof.Recipes
                    Select recipeListB.Find(Function(professionspell) professionspell.SpellId = recipe.SpellId)).Any(
                        Function(result) result Is Nothing) Then
                Return False
            End If
            Return _
                (From recipe In recipeListB
                    Select prof.Recipes.Find(Function(professionspell) professionspell.SpellId = recipe.SpellId)).All(
                        Function(result) result IsNot Nothing)
        End Function
    End Module
End Namespace