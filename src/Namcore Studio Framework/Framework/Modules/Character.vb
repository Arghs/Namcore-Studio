'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013 NamCore Studio <https://github.com/megasus/Namcore-Studio>
'*
'* This program is free software; you can redistribute it and/or modify it
'* under the terms of the GNU General Public License as published by the
'* Free Software Foundation; either version 2 of the License, or (at your
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
Namespace Framework.Modules
    <Serializable()>
    Public Class Character

        <Flags> Public Enum PlayerFlagsEnum As UInteger
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
        <Flags> Public Enum PlayerExtraFlags As UInteger
            PLAYER_EXTRA_GM_ON = 1
            PLAYER_EXTRA_GM_ACCEPT_WHISPERS = 2
            PLAYER_EXTRA_ACCEPT_WHISPERS = 4
            PLAYER_EXTRA_TAXICHEAT = 8
            PLAYER_EXTRA_GM_INVISIBLE = 16
            PLAYER_EXTRA_GM_CHAT = 32
            PLAYER_EXTRA_HAS_310_FLYER = 64
            PLAYER_EXTRA_PVP_DEATH = 128
        End Enum
        <Flags> Public Enum PlayerAtLoginEnum As UInteger
            AT_LOGIN_RENAME = 1
            AT_LOGIN_RESET_SPELLS = 2
            AT_LOGIN_RESET_TALENTS = 4
            AT_LOGIN_CUSTOMIZE = 8
            AT_LOGIN_RESET_PET_TALENTS = 16
            AT_LOGIN_FIRST = 32
            AT_LOGIN_CHANGE_FACTION = 64
            AT_LOGIN_CHANGE_RACE = 128
        End Enum
        Public Loaded As Boolean = False
        Public SourceCore As String
        Public SourceExpansion As Integer
        Public SetIndex As Integer
        Public Guid As Integer
        Public Name As String
        Public Level As Integer
        Public Race As Integer
        Public Cclass As Integer
        Public Gender As Integer
        Public Xp As Integer
        Public Gold As String
        Public PlayerBytes As Integer
        Public PlayerBytes2 As Integer
        Public PlayerFlags As PlayerFlagsEnum
        Public PositionX As Integer
        Public PositionY As Integer
        Public PositionZ As Integer
        Public Map As Integer
        Public InstanceId As Integer
        Public InstanceModeMask As Integer
        Public Orientation As Integer
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

        'Misc

        Public CreatedGuid As Integer
        Public ArmorItems As List(Of Item)
        Public ArmorItemsIndex As String
        Public InventoryItems As List(Of Item)
        Public InventoryZeroItems As List(Of Item)
        Public Quests As List(Of Quest)
        Public PlayerGlyphs As List(Of Glyph)
        Public PlayerGlyphsIndex As String
        Public Achievements As List(Of Achievement)
        Public Actions As List(Of Action)
        Public BeltBuckle As Integer
        Public PlayerReputation As List(Of Reputation)
        Public Skills As List(Of Skill)
        Public Spells As List(Of Spell)
        Public Talents As List(Of Talent)
        Public Professions As List(Of Profession)
        Public AllInfoLoaded As Boolean = False

    End Class
End Namespace