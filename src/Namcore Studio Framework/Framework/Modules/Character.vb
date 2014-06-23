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
'*      /Filename:      Character
'*      /Description:   Character Object - character information class
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports libnc.Provider
Imports NCFramework.Framework.Logging

Namespace Framework.Modules
    <Serializable()>
    Public Class Character
        <Flags>
        Public Enum PlayerFlagsEnum As UInteger
            PLAYER_FLAGS_GROUP_LEADER = 1
            PLAYER_FLAGS_AFK = 2
            PLAYER_FLAGS_DND = 4
            PLAYER_FLAGS_GM = 8
            PLAYER_FLAGS_GHOST = 16
            PLAYER_FLAGS_RESTING = 32
            PLAYER_FLAGS_UNK7 = 64
            PLAYER_FLAGS_UNK8 = 128
            PLAYER_FLAGS_CONTESTED_PVP = 256
            PLAYER_FLAGS_IN_PVP = 512
            PLAYER_FLAGS_HIDE_HELM = 1024
            PLAYER_FLAGS_HIDE_CLOAK = 2048
            PLAYER_FLAGS_PLAYED_LONG_TIME = 4096
            PLAYER_FLAGS_TOO_LONG = 8192
            PLAYER_FLAGS_IS_OUT_OF_BOUNDS = 16384
            PLAYER_FLAGS_DEVELOPER = 32768
            PLAYER_FLAGS_UNK17 = 65536
            PLAYER_FLAGS_TAXI_BENCHMARK = 131072
            PLAYER_FLAGS_PVP_TIMER = 262144
            PLAYER_FLAGS_UNK20 = 524288
            PLAYER_FLAGS_UNK21 = 1048576
            PLAYER_FLAGS_UNK22 = 2097152
            PLAYER_FLAGS_COMMENTATOR2 = 4194304
            PLAYER_ALLOW_ONLY_ABILITY = 8388608
            PLAYER_FLAGS_UNK25 = 16777216
            PLAYER_FLAGS_NO_XP_GAIN = 33554432
        End Enum

        <Flags>
        Public Enum PlayerExtraFlags As UInteger
            PLAYER_EXTRA_GM_ON = 1
            PLAYER_EXTRA_GM_ACCEPT_WHISPERS = 2
            PLAYER_EXTRA_ACCEPT_WHISPERS = 4
            PLAYER_EXTRA_TAXICHEAT = 8
            PLAYER_EXTRA_GM_INVISIBLE = 16
            PLAYER_EXTRA_GM_CHAT = 32
            PLAYER_EXTRA_HAS_310_FLYER = 64
            PLAYER_EXTRA_PVP_DEATH = 128
        End Enum

        <Flags>
        Public Enum PlayerAtLoginEnum As UInteger
            AT_LOGIN_RENAME = 1
            AT_LOGIN_RESET_SPELLS = 2
            AT_LOGIN_RESET_TALENTS = 4
            AT_LOGIN_CUSTOMIZE = 8
            AT_LOGIN_RESET_PET_TALENTS = 16
            AT_LOGIN_FIRST = 32
            AT_LOGIN_CHANGE_FACTION = 64
            AT_LOGIN_CHANGE_RACE = 128
        End Enum

        Public Enum ClassId As UInteger
            PET_TALENTS = 0
            WARRIOR = 1
            PALADIN = 2
            HUNTER = 3
            ROGUE = 4
            PRIEST = 5
            DEATHKNIGHT = 6
            SHAMAN = 7
            MAGE = 8
            WARLOCK = 9
            DRUID = 11
            MONK = 12
        End Enum

        Public Enum RaceId As UInteger
            NONE = 0
            HUMAN = 1
            ORC = 2
            DWARF = 3
            NIGHTELF = 4
            UNDEAD = 5
            TAUREN = 6
            GNOME = 7
            TROLL = 8
            GOBLIN = 9
            BLOODELF = 10
            DRAENEI = 11
            WORGEN = 22
            PANDAREN = 24
        End Enum

        Public Enum GenderType As UInteger
            MALE = 0
            FEMALE = 1
            NEUTRAL = 2
        End Enum

        Public LoadedDateTime As DateTime
        Public Loaded As Boolean = False
        Public SourceCore As Core
        Public SourceExpansion As Expansion
        Public SetIndex As Integer
        Public Guid As Integer
        Public Name As String
        Public Level As Integer
        Private _charRace As RaceId
        Public Overloads Property Race() As RaceId
            Get
                Return _charRace
            End Get
            Set(value As RaceId)
                _charRace = value
            End Set
        End Property
        Public Overloads Property Race(ByVal id As UInteger) As UInteger
            Get
                Return _charRace
            End Get
            Set(value As UInteger)
                _charRace = CType(value, RaceId)
            End Set
        End Property
        Private _charClass As ClassId
        Public Overloads Property Cclass() As ClassId
            Get
                Return _charClass
            End Get
            Set(value As ClassId)
                _charClass = value
            End Set
        End Property
        Public Overloads Property Cclass(ByVal id As UInteger) As UInteger
            Get
                Return _charClass
            End Get
            Set(value As UInteger)
                _charClass = CType(value, ClassId)
            End Set
        End Property
        Private _charGender As GenderType
        Public Property Gender() As GenderType
            Get
                Return _charGender
            End Get
            Set(value As GenderType)
                _charGender = value
            End Set
        End Property
        Public Property Gender(ByVal id As UInteger) As UInteger
            Get
                Return _charGender
            End Get
            Set(value As UInteger)
                _charGender = CType(value, GenderType)
            End Set
        End Property
        Public Xp As Integer
        Public Gold As Integer
        Public PlayerBytes As Integer
        Public PlayerBytes2 As Integer
        Public PlayerFlags As PlayerFlagsEnum
        Public PositionX As Single
        Public PositionY As Single
        Public PositionZ As Single
        Public Map As Integer
        Public InstanceId As Integer
        Public InstanceModeMask As Integer
        Public Orientation As Single
        Public Taximask As String
        Public Cinematic As Integer
        Public TotalTime As String
        Public LevelTime As Integer
        Public StableSlots As Integer
        Public Zone As Integer
        Public ArenaPoints As Integer
        Public TotalHonorPoints As Integer
        Public TotalKills As Integer
        Public ChosenTitle As Integer
        Public WatchedFaction As Integer
        Public Health As Integer
        Public SpecCount As Integer
        Public ActiveSpec As Integer
        Public ExploredZones As String
        Public KnownTitles As String
        Public ArcEmuTalentPoints As String
        Public FinishedQuests As String
        Public CustomFaction As Integer
        Public BindMapId As Integer
        Public BindZoneId As Integer
        Public BindPositionX As Integer
        Public BindPositionY As Integer
        Public BindPositionZ As Integer
        Public HomeBind As String
        Public ExtraFlags As PlayerExtraFlags
        Public AtLogin As PlayerAtLoginEnum
        Public KnownCurrencies As Integer
        Public ActionBars As Integer
        Public ArcEmuAction1 As String
        Public ArcEmuAction2 As String

        'Account

        Public AccountId As Integer
        Public AccountName As String
        Public AccountSet As Integer
        Public TargetAccount As Account

        'Misc

        Public RenamePending As Boolean = False
        Public CreatedGuid As Integer
        Public ArmorItems As List(Of Item)
        Public InventoryItems As List(Of Item)
        Public InventoryZeroItems As List(Of Item)
        Public Quests As List(Of Quest)
        Public PlayerGlyphs As List(Of Glyph)
        Public Achievements As List(Of Achievement)
        Public Actions As List(Of Action)
        Public BeltBuckle As Integer
        Public PlayerReputation As List(Of Reputation)
        Public Skills As List(Of Skill)
        Public Spells As List(Of Spell)
        Public Talents As List(Of Talent)
        Public Professions As List(Of Profession)
        Public AllInfoLoaded As Boolean = False

        Public Sub AddRecipeToProfession(ByVal skillId As Integer, ByVal spellId As Integer)
            If Professions Is Nothing Then
                Professions = New List(Of Profession)()
                Exit Sub
            End If
            Try
                Dim profIndex As Integer = Professions.FindIndex(Function(profession) profession.Id = skillId)
                If Not profIndex = -1 Then
                    If Professions(profIndex).Recipes Is Nothing Then _
                        Professions(profIndex).Recipes = New List(Of ProfessionSpell)()
                    Professions(profIndex).Recipes.Add(
                        New ProfessionSpell _
                                                          With {.SpellId = spellId,
                                                          .MinSkill = GetMinimumSkillBySpellId(spellId)})
                End If
            Catch ex As Exception
                LogAppend("Failed to add recipe: " & ex.ToString(), "Character_AddRecipeToProfession", False, True)
            End Try
        End Sub

        Public Sub RemoveRecipeFromProfession(ByVal skillId As Integer, ByVal spellId As Integer)
            If Professions Is Nothing Then
                Professions = New List(Of Profession)()
                Exit Sub
            End If
            Dim profIndex As Integer = Professions.FindIndex(Function(profession) profession.Id = skillId)
            If Not profIndex = -1 Then
                If Professions(profIndex).Recipes Is Nothing Then
                    Professions(profIndex).Recipes = New List(Of ProfessionSpell)()
                    Exit Sub
                End If
                Dim recipeIndex As Integer =
                        Professions(profIndex).Recipes.FindIndex(
                            Function(professionSpell) professionSpell.SpellId = spellId)
                If Not recipeIndex = -1 Then
                    Professions(profIndex).Recipes.RemoveAt(recipeIndex)
                End If
            End If
        End Sub

        Public Sub SetPlayerBytes(Optional skinColor As Integer = 0, Optional faceStyle As Integer = 0,
                                  Optional hairStyle As Integer = 0, Optional hairColor As Integer = 0)
            If skinColor = 0 Then skinColor = PlayerBytes Mod 256
            If faceStyle = 0 Then faceStyle = (PlayerBytes >> 8) Mod 256
            If hairStyle = 0 Then hairStyle = (PlayerBytes >> 16) Mod 256
            If hairColor = 0 Then hairColor = (PlayerBytes >> 24) Mod 256
            PlayerBytes = skinColor Or faceStyle << 8 Or hairStyle << 16 Or hairColor << 24
        End Sub

        Public Sub SetPlayerBytes2(ByVal facialHair As Integer)
            PlayerBytes2 = facialHair Mod 256
        End Sub
    End Class
End Namespace