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
'*      /Filename:      ProfessionParser
'*      /Description:   Contains functions for loading character profession information 
'*                      from wow armory
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
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
            Dim pProf As Profession
            Try
                LogAppend("Loading character profession information", "ProfessionParser_loadProfessions", True)
                '// Using API to load profession info
                Dim pfContext As String = client.DownloadString(apiLink & "?fields=professions")
                If pfContext Is Nothing Then
                    LogAppend("pfContext is nothing - Failed to load Professions API",
                              "ProfessionParser_loadProfessions",
                              False, True)
                    Exit Sub
                Else
                    LogAppend("pfContext loaded - length: " & pfContext.Length.ToString(),
                              "ProfessionParser_loadProfessions", False)
                End If
                Dim pfStr As String = SplitString(pfContext, """professions"":{", "]},"""")") & "#end#"
                Dim primaryPf As String = SplitString(pfStr, """primary"":[", """secondary""")
                Dim secondaryPf As String = SplitString(pfStr, """secondary"":[", "#end#")
                Dim usePfString As String = primaryPf
                Do
                    Dim excounter As Integer = UBound(Split(usePfString, "}")) + 1
                    Dim partsPf() As String = usePfString.Split("}"c)
                    Dim loopcounter As Integer = 0
                    Do
                        pProf = New Profession()
                        pProf.Recipes = New List(Of ProfessionSpell)()
                        If usePfString = primaryPf Then
                            pProf.Primary = True
                        Else
                            pProf.Primary = False
                        End If
                        Dim myPart As String = partsPf(loopcounter)
                        If myPart.Length < 29 Then
                            loopcounter += 1
                        Else
                            pProf.Id = TryInt(SplitString(myPart, """id"":", ","))
                            pProf.Iconname = SplitString(myPart, """icon"":""", """,")
                            pProf.Max = TryInt(SplitString(myPart, """max"":", ","))
                            pProf.Name = SplitString(myPart, """name"":""", """,")
                            pProf.Rank = TryInt(SplitString(myPart, """rank"":", ","))
                            If pProf.Rank = 0 Then
                                loopcounter += 1
                                Continue Do
                            End If
                            LogAppend("Adding profession with id " & pProf.Id.ToString,
                                      "ProfessionParser_loadProfessions", True)
                            Dim recipes As String = SplitString(myPart, """recipes"":[", "]")
                            If recipes.Length > 3 Then
                                Dim useRecipes As String() = recipes.Split(","c)
                                For i = 0 To useRecipes.Length - 1
                                    pProf.Recipes.Add(
                                        New ProfessionSpell() _
                                                         With {.SpellId = TryInt(useRecipes(i)), .Name = "",
                                                         .MinSkill = GetMinimumSkillBySpellId(TryInt(useRecipes(i)))})
                                Next
                            End If
                            If player.Spells Is Nothing Then player.Spells = New List(Of Spell)()
                            player.Spells.Add(New Spell _
                                                 With {.Active = 1, .Disabled = 0,
                                                 .Id = GetSkillSpellIdBySkillRank(pProf.Id, pProf.Rank)})
                            Dim specialSpells() As Integer = GetSkillSpecialSpellIdBySkill(pProf.Id)
                            If Not specialSpells Is Nothing Then
                                For i = 0 To specialSpells.Length - 1
                                    LogAppend("Adding special profession spell " & specialSpells(i).ToString,
                                              "ProfessionParser_loadProfessions", True)
                                    player.Spells.Add(New Spell _
                                                         With {.Active = 1, .Disabled = 0, .Id = specialSpells(i)})
                                Next
                            End If
                            player.Professions.Add(pProf)
                            loopcounter += 1
                        End If
                    Loop Until loopcounter = excounter
                    LogAppend("Loaded " & player.Professions.Count.ToString & " professions",
                              "ProfessionParser_loadProfessions",
                              True)
                    If usePfString = secondaryPf Then
                        Exit Do
                    Else
                        usePfString = secondaryPf
                    End If
                Loop
                '// Saving changes to character
                SetCharacterSet(setId, player, GetAccountSetBySetId(player.AccountSet))
            Catch ex As Exception
                LogAppend("Exception occured: " & vbNewLine & ex.ToString(), "ProfessionParser_loadProfessions", False)
            End Try
        End Sub
    End Class
End Namespace