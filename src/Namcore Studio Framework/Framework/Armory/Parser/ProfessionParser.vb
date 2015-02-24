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
'*      /Filename:      ProfessionParser
'*      /Description:   Contains functions for loading character profession information 
'*                      from wow armory
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.My
Imports Newtonsoft.Json.Linq
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Extension
Imports NCFramework.Framework.Modules
Imports System.Net
Imports libnc.Provider

Namespace Framework.Armory.Parser
    Public Class ProfessionParser
        Public Sub LoadProfessions(ByVal setId As Integer, ByVal apiLink As String, ByVal account As Account)
            Dim client As New WebClient
            client.CheckProxy()
            '// Retrieving character
            Dim player As Character = GetCharacterSetBySetId(setId, account)
            player.Professions = New List(Of Profession)
            Try
                LogAppend(
                    "Loading character profession information - setId: " & setId.ToString() & " - apiLink: " & apiLink,
                    "ProfessionParser_LoadProfessions", True)
                '// Using API to load profession info
                Dim pfContext As String = client.DownloadString(apiLink & "?fields=professions")
                If Not pfContext.Contains("""professions"":") Then
                    LogAppend("No profession information found!?", "ProfessionParser_LoadProfessions", True)
                    Exit Sub '// Skip if no professions
                End If
                Dim jResults As JObject = JObject.Parse(pfContext)
                Dim results As List(Of JToken) = jResults.Children().ToList()
                Dim token As JProperty =
                        CType(results.Find(Function(jtoken) CType(jtoken, JProperty).Name = "professions"),
                              JProperty)
                If token.HasChildren Then
                    For i = 0 To 1
                        Dim specToken As JProperty
                        If i = 0 Then
                            specToken = token.GetChild("primary")
                        Else
                            specToken = token.GetChild("secondary")
                        End If
                        If specToken.HasChildren() Then
                            For z = 0 To specToken.GetObjects().Count - 1
                                Dim profToken As List(Of JProperty) =
                                        specToken.GetObjects()(z).Children.Cast (Of JProperty).ToList()
                                Dim pProfession As New Profession
                                pProfession.Id = CInt(profToken.GetValue("id"))
                                pProfession.Primary = Not CBool(i)
                                pProfession.Iconname = profToken.GetValue("icon")
                                pProfession.Name = profToken.GetValue("name")
                                pProfession.Max = CInt(profToken.GetValue("max"))
                                pProfession.Rank = CInt(profToken.GetValue("rank"))
                                If pProfession.Rank = 0 Then Continue For
                                pProfession.Recipes = New List(Of ProfessionSpell)()
                                Dim recipes() As Integer =
                                        profToken.GetValues("recipes").ToList().ConvertAll(
                                            Function(str) Integer.Parse(str)) _
                                        .ToArray()
                                If Not recipes Is Nothing Then
                                    For Each recipeId As Integer In recipes
                                        pProfession.Recipes.Add(
                                            New ProfessionSpell() _
                                                                   With {.SpellId = recipeId,
                                                                   .Name =
                                                                   GetSpellNameBySpellId(.SpellId,
                                                                                         MySettings.Default.language),
                                                                   .MinSkill = GetMinimumSkillBySpellId(.SpellId)})
                                    Next
                                Else
                                    LogAppend("No recipes found for profession: " & pProfession.Id.ToString,
                                              "ProfessionParser_LoadProfessions", True)
                                End If
                                Dim specialSpells() As Integer = GetSkillSpecialSpellIdBySkill(pProfession.Id)
                                If Not specialSpells Is Nothing Then
                                    For Each spellId In specialSpells
                                        LogAppend("Adding special profession spell " & spellId.ToString,
                                                  "ProfessionParser_LoadProfessions", True)
                                        player.Spells.Add(New Spell With {.Active = 1, .Disabled = 0, .Id = spellId})
                                    Next
                                End If
                                LogAppend("Adding profession with id " & pProfession.Id.ToString,
                                          "ProfessionParser_LoadProfessions", True)
                                player.Professions.Add(pProfession)
                            Next z
                        End If
                    Next i
                    LogAppend("Loaded " & player.Professions.Count.ToString & " professions",
                              "ProfessionParser_LoadProfessions",
                              True)
                End If

                '// Saving changes to character
                SetCharacterSet(setId, player, GetAccountSetBySetId(player.AccountSet))
            Catch ex As Exception
                LogAppend("Exception occured: " & vbNewLine & ex.ToString(), "ProfessionParser_loadProfessions", False)
            End Try
        End Sub
    End Class
End Namespace