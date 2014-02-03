Imports System.Runtime.CompilerServices
Imports NCFramework.Framework.Modules

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
