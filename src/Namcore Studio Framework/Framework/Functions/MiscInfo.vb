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
'*      /Filename:      SpellItemInformation
'*      /Description:   Includes functions for locating certain item and spell information
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.Drawing
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Modules
Imports libnc.Provider
Imports System.Resources
Imports System.Reflection
Imports NCFramework.My.Resources

Namespace Framework.Functions
    Public Module MiscInfo
        Public Sub LoadWeaponType(ByVal itemid As Integer, ByVal tarSet As Integer, ByVal account As Account)
            If Not itemid = 0 Then
                LogAppend("Loading weapon type of Item " & itemid.ToString, "SpellItem_Information_LoadWeaponType",
                          False)
                Dim subClass As Integer = GetItemSubClassById(itemid)
                Dim player As Character = GetCharacterSetBySetId(tarSet, account)
                If player.Spells Is Nothing Then player.Spells = New List(Of Spell)()
                If player.Skills Is Nothing Then player.Skills = New List(Of Skill)()
                Select Case subClass
                    Case 18 'Crossbows
                        player.Spells.Add(New Spell With {.Active = 1, .Disabled = 0, .Id = 5011})
                        player.Skills.Add(New Skill With {.Value = 100, .Max = 100, .Id = 226})
                    Case 2 'Bows
                        player.Spells.Add(New Spell With {.Active = 1, .Disabled = 0, .Id = 264})
                        player.Skills.Add(New Skill With {.Value = 100, .Max = 100, .Id = 45})
                    Case 3 'Guns
                        player.Spells.Add(New Spell With {.Active = 1, .Disabled = 0, .Id = 266})
                        player.Skills.Add(New Skill With {.Value = 100, .Max = 100, .Id = 46})
                    Case 16 'Thrown
                        player.Spells.Add(New Spell With {.Active = 1, .Disabled = 0, .Id = 2764})
                        player.Spells.Add(New Spell With {.Active = 1, .Disabled = 0, .Id = 2567})
                        player.Skills.Add(New Skill With {.Value = 100, .Max = 100, .Id = 176})
                    Case 19 'Wands
                        player.Spells.Add(New Spell With {.Active = 1, .Disabled = 0, .Id = 5009})
                        player.Spells.Add(New Spell With {.Active = 1, .Disabled = 0, .Id = 5019})
                        player.Skills.Add(New Skill With {.Value = 100, .Max = 100, .Id = 228})
                    Case 7 'One-handed swords
                        player.Spells.Add(New Spell With {.Active = 1, .Disabled = 0, .Id = 201})
                        player.Skills.Add(New Skill With {.Value = 100, .Max = 100, .Id = 43})
                    Case 8 'Two-handed swords
                        player.Spells.Add(New Spell With {.Active = 1, .Disabled = 0, .Id = 201})
                        player.Skills.Add(New Skill With {.Value = 100, .Max = 100, .Id = 43})
                        player.Spells.Add(New Spell With {.Active = 1, .Disabled = 0, .Id = 202})
                        player.Skills.Add(New Skill With {.Value = 100, .Max = 100, .Id = 55})
                    Case 15 'Daggers
                        player.Spells.Add(New Spell With {.Active = 1, .Disabled = 0, .Id = 1180})
                        player.Skills.Add(New Skill With {.Value = 100, .Max = 100, .Id = 173})
                    Case 0 'One-handed axes
                        player.Spells.Add(New Spell With {.Active = 1, .Disabled = 0, .Id = 196})
                        player.Skills.Add(New Skill With {.Value = 100, .Max = 100, .Id = 44})
                    Case 1 'Two-handed axes
                        player.Spells.Add(New Spell With {.Active = 1, .Disabled = 0, .Id = 197})
                        player.Skills.Add(New Skill With {.Value = 100, .Max = 100, .Id = 44})
                        player.Spells.Add(New Spell With {.Active = 1, .Disabled = 0, .Id = 196})
                        player.Skills.Add(New Skill With {.Value = 100, .Max = 100, .Id = 142})
                    Case 4 'One-handed maces
                        player.Spells.Add(New Spell With {.Active = 1, .Disabled = 0, .Id = 198})
                        player.Skills.Add(New Skill With {.Value = 100, .Max = 100, .Id = 54})
                    Case 5 'Two-handed maces
                        player.Skills.Add(New Skill With {.Value = 100, .Max = 100, .Id = 54})
                        player.Spells.Add(New Spell With {.Active = 1, .Disabled = 0, .Id = 198})
                        player.Skills.Add(New Skill With {.Value = 100, .Max = 100, .Id = 160})
                        player.Spells.Add(New Spell With {.Active = 1, .Disabled = 0, .Id = 199})
                    Case 6 'Polearms
                        player.Spells.Add(New Spell With {.Active = 1, .Disabled = 0, .Id = 200})
                        player.Skills.Add(New Skill With {.Value = 100, .Max = 100, .Id = 229})
                    Case 10 'Staves
                        player.Spells.Add(New Spell With {.Active = 1, .Disabled = 0, .Id = 227})
                        player.Skills.Add(New Skill With {.Value = 100, .Max = 100, .Id = 136})
                End Select
                SetCharacterSet(tarSet, player, account)
            End If
        End Sub

        Public Function GetSkillSpellIdBySkillRank(ByVal skillId As Integer, ByVal rank As Integer) As Integer
            Select Case skillId
                Case 129 '// First Aid
                    Select Case rank
                        Case 0 To 49 : Return 3273
                        Case 50 To 124 : Return 3274
                        Case 125 To 199 : Return 7924
                        Case 200 To 274 : Return 10846
                        Case 275 To 349 : Return 27028
                        Case 350 To 424 : Return 45542
                        Case 425 To 499 : Return 74559
                        Case 500 To 600 : Return 110406
                        Case Else : Return - 1
                    End Select
                Case 164 '// Blacksmithing
                    Select Case rank
                        Case 0 To 49 : Return 2018
                        Case 50 To 124 : Return 3100
                        Case 125 To 199 : Return 3538
                        Case 200 To 274 : Return 9785
                        Case 275 To 349 : Return 29844
                        Case 350 To 424 : Return 51300
                        Case 425 To 499 : Return 76666
                        Case 500 To 600 : Return 110396
                        Case Else : Return - 1
                    End Select
                Case 165 '// Leatherworking
                    Select Case rank
                        Case 0 To 49 : Return 2108
                        Case 50 To 124 : Return 3104
                        Case 125 To 199 : Return 3811
                        Case 200 To 274 : Return 10662
                        Case 275 To 349 : Return 32549
                        Case 350 To 424 : Return 51302
                        Case 425 To 499 : Return 81199
                        Case 500 To 600 : Return 110423
                        Case Else : Return - 1
                    End Select
                Case 171 '// Alchemy
                    Select Case rank
                        Case 0 To 49 : Return 2259
                        Case 50 To 124 : Return 3101
                        Case 125 To 199 : Return 3464
                        Case 200 To 274 : Return 11611
                        Case 275 To 349 : Return 28596
                        Case 350 To 424 : Return 51304
                        Case 425 To 499 : Return 80731
                        Case 500 To 600 : Return 105206
                        Case Else : Return - 1
                    End Select
                Case 182 '// Herbalism
                    Select Case rank
                        Case 0 To 49 : Return 2366
                        Case 50 To 124 : Return 2368
                        Case 125 To 199 : Return 3270
                        Case 200 To 274 : Return 11993
                        Case 275 To 349 : Return 28695
                        Case 350 To 424 : Return 50300
                        Case 425 To 499 : Return 74519
                        Case 500 To 600 : Return 110413
                        Case Else : Return - 1
                    End Select
                Case 185 '// Cooking
                    Select Case rank
                        Case 0 To 49 : Return 2550
                        Case 50 To 124 : Return 3102
                        Case 125 To 199 : Return 3413
                        Case 200 To 274 : Return 18260
                        Case 275 To 349 : Return 33359
                        Case 350 To 424 : Return 51296
                        Case 425 To 499 : Return 88053
                        Case 500 To 600 : Return 104381
                        Case Else : Return - 1
                    End Select
                Case 186 '// Mining
                    Select Case rank
                        Case 0 To 49 : Return 2575
                        Case 50 To 124 : Return 2576
                        Case 125 To 199 : Return 3564
                        Case 200 To 274 : Return 10248
                        Case 275 To 349 : Return 29354
                        Case 350 To 424 : Return 50310
                        Case 425 To 499 : Return 74517
                        Case 500 To 600 : Return 102161
                        Case Else : Return - 1
                    End Select
                Case 197 '// Tailoring
                    Select Case rank
                        Case 0 To 49 : Return 3908
                        Case 50 To 124 : Return 3909
                        Case 125 To 199 : Return 3910
                        Case 200 To 274 : Return 12180
                        Case 275 To 349 : Return 26790
                        Case 350 To 424 : Return 51309
                        Case 425 To 499 : Return 75156
                        Case 500 To 600 : Return 110426
                        Case Else : Return - 1
                    End Select
                Case 202 '// Engineering
                    Select Case rank
                        Case 0 To 49 : Return 4036
                        Case 50 To 124 : Return 4037
                        Case 125 To 199 : Return 4038
                        Case 200 To 274 : Return 12656
                        Case 275 To 349 : Return 30350
                        Case 350 To 424 : Return 51306
                        Case 425 To 499 : Return 82774
                        Case 500 To 600 : Return 110403
                        Case Else : Return - 1
                    End Select
                Case 333 '// Enchanting
                    Select Case rank
                        Case 0 To 49 : Return 7411
                        Case 50 To 124 : Return 7412
                        Case 125 To 199 : Return 7413
                        Case 200 To 274 : Return 13920
                        Case 275 To 349 : Return 28029
                        Case 350 To 424 : Return 51313
                        Case 425 To 499 : Return 74258
                        Case 500 To 600 : Return 110400
                        Case Else : Return - 1
                    End Select
                Case 356 '// Fishing
                    Select Case rank
                        Case 0 To 49 : Return 7620
                        Case 50 To 124 : Return 7731
                        Case 125 To 199 : Return 7732
                        Case 200 To 274 : Return 18248
                        Case 275 To 349 : Return 33095
                        Case 350 To 424 : Return 51294
                        Case 425 To 499 : Return 88868
                        Case 500 To 600 : Return 110410
                        Case Else : Return - 1
                    End Select
                Case 393 '// Skinning
                    Select Case rank
                        Case 0 To 49 : Return 8613
                        Case 50 To 124 : Return 8617
                        Case 125 To 199 : Return 8618
                        Case 200 To 274 : Return 10768
                        Case 275 To 349 : Return 32678
                        Case 350 To 424 : Return 50305
                        Case 425 To 499 : Return 74522
                        Case 500 To 600 : Return 102216
                        Case Else : Return - 1
                    End Select
                Case 755 '// Jewelcrafting
                    Select Case rank
                        Case 0 To 49 : Return 25229
                        Case 50 To 124 : Return 25230
                        Case 125 To 199 : Return 28894
                        Case 200 To 274 : Return 28895
                        Case 275 To 349 : Return 28897
                        Case 350 To 424 : Return 51311
                        Case 425 To 499 : Return 73318
                        Case 500 To 600 : Return 110420
                        Case Else : Return - 1
                    End Select
                Case 773 '// Inscription
                    Select Case rank
                        Case 0 To 49 : Return 45357
                        Case 50 To 124 : Return 45358
                        Case 125 To 199 : Return 45359
                        Case 200 To 274 : Return 45360
                        Case 275 To 349 : Return 45361
                        Case 350 To 424 : Return 45363
                        Case 425 To 499 : Return 86008
                        Case 500 To 600 : Return 110417
                        Case Else : Return - 1
                    End Select
                Case 794 '// Archaeology
                    Select Case rank
                        Case 0 To 49 : Return 78670
                        Case 50 To 124 : Return 88961
                        Case 125 To 199 : Return 89718
                        Case 200 To 274 : Return 8719
                        Case 275 To 349 : Return 89720
                        Case 350 To 424 : Return 89721
                        Case 425 To 499 : Return 89722
                        Case 500 To 600 : Return 110393
                        Case Else : Return - 1
                    End Select
                Case Else : Return - 1
            End Select
        End Function

        Public Function GetSkillSpecialSpellIdBySkill(ByVal skillId As Integer) As Integer()
            Select Case skillId
                Case 129 '// First Aid
                    Return Nothing
                Case 164 '// Blacksmithing
                    Return Nothing
                Case 165 '// Leatherworking
                    Return Nothing
                Case 171 '// Alchemy
                    Return Nothing
                Case 182 '// Herbalism
                    Return {2382}
                Case 185 '// Cooking
                    Return {818}
                Case 186 '// Mining
                    Return {2580, 2656}
                Case 197 '// Tailoring
                    Return Nothing
                Case 202 '// Engineering
                    Return {49383}
                Case 333 '// Enchanting
                    Return {13262}
                Case 356 '// Fishing
                    Return {43308}
                Case 393 '// Skinning
                    Return Nothing
                Case 755 '// Jewelcrafting
                    Return {31252}
                Case 773 '// Inscription
                    Return {51005}
                Case 794 '// Archaeology
                    Return {80451, 74268}
                Case Else : Return Nothing
            End Select
        End Function

        Public Function IsProfession(ByVal skillId As Integer) As Boolean
            Select Case skillId
                Case 171, 164, 333, 202, 182, 773, 755, 165, 186, 393, 197, 129, 185, 356, 794 : Return True
                Case Else : Return False
            End Select
        End Function

        Public Function GetRaceNameById(ByVal raceid As UInteger) As String
            LogAppend("Loading race name by id: " & raceid.ToString(), "Conversions_GetRaceNameById", False)
            Select Case raceid
                Case 1 : Return CHAR_RACE_HUMAN
                Case 2 : Return CHAR_RACE_ORC
                Case 3 : Return CHAR_RACE_DWARF
                Case 4 : Return CHAR_RACE_NIGHTELF
                Case 5 : Return CHAR_RACE_UNDEAD
                Case 6 : Return CHAR_RACE_TAUREN
                Case 7 : Return CHAR_RACE_GNOME
                Case 8 : Return CHAR_RACE_TROLL
                Case 9 : Return CHAR_RACE_GOBLIN
                Case 10 : Return CHAR_RACE_BLOODELF
                Case 11 : Return CHAR_RACE_DRAENEI
                Case 22 : Return CHAR_RACE_WORGEN
                Case 25, 26 : Return CHAR_RACE_PANDAREN
                Case Else _
                    : LogAppend("Invalid RaceId: " & raceid.ToString() & " // Returning nothing!",
                                "Conversions_GetRaceNameById")
                    Return Nothing
            End Select
        End Function

        Public Function GetClassNameById(ByVal classid As UInteger) As String
            LogAppend("Loading class name by id: " & classid.ToString(), "Conversions_GetClassNameById", False)
            Select Case classid
                Case 1 : Return CHAR_CLASS_WARRIOR
                Case 2 : Return CHAR_CLASS_PALADIN
                Case 3 : Return CHAR_CLASS_HUNTER
                Case 4 : Return CHAR_CLASS_ROGUE
                Case 5 : Return CHAR_CLASS_PRIEST
                Case 6 : Return CHAR_CLASS_DEATHKNIGHT
                Case 7 : Return CHAR_CLASS_SHAMAN
                Case 8 : Return CHAR_CLASS_MAGE
                Case 9 : Return CHAR_CLASS_WARLOCK
                Case 10 : Return CHAR_CLASS_MONK
                Case 11 : Return CHAR_CLASS_DRUID
                Case Else _
                    : LogAppend("Invalid ClassId: " & classid.ToString() & " // Returning nothing!",
                                "Conversions_GetClassNameById")
                    Return Nothing
            End Select
        End Function

        Public Function GetRaceIdByName(ByVal racename As String) As UInteger
            LogAppend("Loading race id by name: " & racename.ToString(), "Conversions_GetRaceIdByName", False)
            Select Case racename.ToLower()
                Case "human" : Return 1
                Case "orc" : Return 2
                Case "dwarf" : Return 3
                Case "night-elf" : Return 4
                Case "undead" : Return 5
                Case "tauren" : Return 6
                Case "gnome" : Return 7
                Case "troll" : Return 8
                Case "goblin" : Return 9
                Case "blood-elf" : Return 10
                Case "draenei" : Return 11
                Case "worgen" : Return 22
                Case "pandaren" : Return 25
                Case Else _
                    : LogAppend("Invalid Race name: " & racename & " // Returning nothing!", "Conversions_GetRaceIdByName")
                    Return Nothing
            End Select
        End Function

        Public Function GetClassIdByName(ByVal classname As String) As UInteger
            LogAppend("Loading class id by name: " & classname.ToString(), "Conversions_GetClassIdByName", False)
            Select Case classname
                Case "warrior" : Return 1
                Case "paladin" : Return 2
                Case "hunter" : Return 3
                Case "rogue" : Return 4
                Case "priest" : Return 5
                Case "death-knight" : Return 6
                Case "shaman" : Return 7
                Case "mage" : Return 8
                Case "warlock" : Return 9
                Case "monk" : Return 10
                Case "druid" : Return 11
                Case Else _
                    : LogAppend("Invalid Class name: " & classname & " // Returning nothing!",
                                "Conversions_GetClassIdByName")
                    Return Nothing
            End Select
        End Function

        Public Function GetProficiencyLevelNameByLevel(ByVal level As Integer) As String
            Dim rm As New ResourceManager("NCFramework.UserMessages", Assembly.GetExecutingAssembly())
            Select Case level
                Case 0 To 49 : Return rm.GetString("proficiency_1")
                Case 50 To 124 : Return rm.GetString("proficiency_2")
                Case 125 To 199 : Return rm.GetString("proficiency_3")
                Case 200 To 274 : Return rm.GetString("proficiency_4")
                Case 275 To 349 : Return rm.GetString("proficiency_5")
                Case 350 To 424 : Return rm.GetString("proficiency_6")
                Case 425 To 499 : Return rm.GetString("proficiency_7")
                Case 500 To 600 : Return rm.GetString("proficiency_8")
                Case 600 To 700 : Return rm.GetString("proficiency_9")
                Case Else : Return MSG_ERROR
            End Select
        End Function

        Public Function GetItemQualityColor(ByVal rarity As Integer) As Color
            Select Case rarity
                Case 0, 1 : Return Color.Gray
                Case 0, 1 : Return Color.White
                Case 2 : Return Color.LightGreen
                Case 3 : Return Color.DodgerBlue
                Case 4 : Return Color.DarkViolet
                Case 5 : Return Color.Orange
                Case 6, 7 : Return Color.Gold
                Case Else : Return Color.LawnGreen
            End Select
        End Function

        Public Function GetGenderNameById(ByVal genderId As UInteger) As String
            LogAppend("Loading gender name by id: " & genderId.ToString(), "Conversions_GetGenderNameById", False)
            Select Case genderid
                Case 0 : Return CHAR_GENDER_MALE
                Case 1 : Return CHAR_GENDER_FEMALE
                Case Else
                    LogAppend("Invalid GenderId: " & genderId.ToString() & " // Returning nothing!",
                               "Conversions_GetGenderNameById")
                    Return Nothing
            End Select
        End Function

        Public Function GetSlotIdByName(ByVal name As String) As Integer
            LogAppend("Loading item slot id by name: " & name, "Conversions_GetSlotIdByName", False)
            Select Case name.ToLower()
                Case "head"
                    Return 0
                Case "neck"
                    Return 1
                Case "shoulder"
                    Return 2
                Case "shirt"
                    Return 3
                Case "chest"
                    Return 4
                Case "waist"
                    Return 5
                Case "legs"
                    Return 6
                Case "feet"
                    Return 7
                Case "wrist"
                    Return 8
                Case "hands"
                    Return 9
                Case "finger1"
                    Return 10
                Case "finger2"
                    Return 11
                Case "trinket1"
                    Return 12
                Case "trinket2"
                    Return 13
                Case "back"
                    Return 14
                Case "mainhand"
                    Return 15
                Case "offhand"
                    Return 16
                Case "tabard"
                    Return 18
                Case Else
                    LogAppend("Invalid slot name: " & name, "Conversions_GetSlotIdByName", True, True)
                    Return -1
            End Select
        End Function
    End Module
End Namespace