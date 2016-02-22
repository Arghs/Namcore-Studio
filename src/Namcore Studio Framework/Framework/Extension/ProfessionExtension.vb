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
'*      /Filename:      ProfessionExtension
'*      /Description:   Extension to compare professions
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.Runtime.CompilerServices
Imports NCFramework.Framework.Modules

Namespace Framework.Extension
    Public Module ProfessionExtension
        ''' <summary>
        '''     Updates reputation standing by value and status
        ''' </summary>
        <Extension>
        Public Function RecipeListsIdentical(prof As Profession,
                                             recipeListB As List(Of ProfessionSpell)) As Boolean

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

        <Extension>
        Public Function UpdateMax(ByRef prof As Profession) As Profession
            Select Case prof.Rank
                Case 0 To 75 : prof.Max = 75
                Case 75 To 150 : prof.Max = 150
                Case 150 To 225 : prof.Max = 225
                Case 225 To 300 : prof.Max = 300
                Case 300 To 375 : prof.Max = 375
                Case 375 To 450 : prof.Max = 450
                Case 450 To 525 : prof.Max = 525
                Case 525 To 600 : prof.Max = 600
            End Select
            Return prof
        End Function
    End Module
End Namespace