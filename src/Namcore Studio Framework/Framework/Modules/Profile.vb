'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013 Namcore Studio <https://github.com/megasus/Namcore-Studio>
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
'*      /Filename:      Item
'*      /Description:   Item/Glyph Object - item/glyph information class
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.Drawing

Namespace Framework.Modules

    <Serializable()>
    Public Class Item
        Public Id As Integer
        Public Guid As Integer
        Public Name As String
        Public Rarity As ItemRarityEnum '0=poor;1=common;2=uncommon;3=rare;4=epic;5=legendary;6=artifact/heirloom
        Public Slotname As String
        Public Slot As ItemSlotEnum
        Public Socket1Id As Integer
        Public Socket2Id As Integer
        Public Socket3Id As Integer
        Public Socket1Pic As Image
        Public Socket2Pic As Image
        Public Socket3Pic As Image
        Public Socket1Name As String
        Public Socket2Name As String
        Public Socket3Name As String
        Public EnchantmentType As ItemEnchTypeEnum '1=spell;2=item
        Public EnchantmentId As Integer
        Public EnchantmentName As String
        Public Enchstring As String
        Public Socket1Effectid As Integer
        Public Socket2Effectid As Integer
        Public Socket3Effectid As Integer
        Public EnchantmentEffectid As Integer
        Public Image As Image
        Public UpdateRequest As ItemUpdateEnum = ItemUpdateEnum.UPDATE_NO '0=no; 1=create; 2=delete; 3=update
        Public Bag As Integer
        Public Bagguid As Integer
        Public Enchantstring As String
        Public Count As Integer
        Public Container As Integer
        Public BagItems As List(Of Item)
        Public SlotCount As Integer
        Public Charges As String
        Public Duration As Integer
        Public Durability As Integer
        Public Enum ItemUpdateEnum As UInteger
            UPDATE_NO = 0
            UPDATE_CREATE = 1
            UPDATE_DELETE = 2
            UPDATE_UPDATE = 3
        End Enum
        Public Enum ItemEnchTypeEnum As UInteger
            ENCH_TYPE_SPELL = 1
            ENCH_TYPE_ITEM = 2
        End Enum
        Public Enum ItemRarityEnum As UInteger
            ITM_RARITY_POOR = 0
            ITM_RARITY_COMMON = 1
            ITM_RARITY_UNCOMMON = 2
            ITM_RARITY_RARE = 3
            ITM_RARITY_EPIC = 4
            ITM_RARITY_LEGENDARY = 5
            ITM_RARITY_ARTIFACT = 6
        End Enum
        Public Enum ItemSlotEnum As UInteger
            ITM_SLOT_HEAD = 0
            ITM_SLOT_NECK = 1
            ITM_SLOT_SHOULDERS = 2
            ITM_SLOT_BODY = 3
            ITM_SLOT_CHEST = 4
            ITM_SLOT_WAIST = 5
            ITM_SLOT_LEGS = 6
            ITM_SLOT_FEET = 7
            ITM_SLOT_WRISTS = 8
            ITM_SLOT_HANDS = 9
            ITM_SLOT_FINGER1 = 10
            ITM_SLOT_FINGER2 = 11
            ITM_SLOT_TRINKET1 = 12
            ITM_SLOT_TRINKET2 = 13
            ITM_SLOT_BACK = 14
            ITM_SLOT_MAIN_HAND = 15
            ITM_SLOT_OFF_HAND = 16
            ITM_SLOT_RANGED = 17
            ITM_SLOT_TABARD = 18
            ITM_SLOT_EQUIPPED_BAG1 = 19
            ITM_SLOT_EQUIPPED_BAG2 = 20
            ITM_SLOT_EQUIPPED_BAG3 = 21
            ITM_SLOT_EQUIPPED_BAG4 = 22
            ITM_SLOT_MAIN_BACKPACK1 = 23
            ITM_SLOT_MAIN_BACKPACK2 = 24
            ITM_SLOT_MAIN_BACKPACK3 = 25
            ITM_SLOT_MAIN_BACKPACK4 = 26
            ITM_SLOT_MAIN_BACKPACK5 = 27
            ITM_SLOT_MAIN_BACKPACK6 = 28
            ITM_SLOT_MAIN_BACKPACK7 = 29
            ITM_SLOT_MAIN_BACKPACK8 = 30
            ITM_SLOT_MAIN_BACKPACK9 = 31
            ITM_SLOT_MAIN_BACKPACK10 = 32
            ITM_SLOT_MAIN_BACKPACK11 = 33
            ITM_SLOT_MAIN_BACKPACK12 = 34
            ITM_SLOT_MAIN_BACKPACK13 = 35
            ITM_SLOT_MAIN_BACKPACK14 = 36
            ITM_SLOT_MAIN_BACKPACK15 = 37
            ITM_SLOT_MAIN_BACKPACK16 = 38
            ITM_SLOT_MAIN_BANK1 = 39
            ITM_SLOT_MAIN_BANK2 = 40
            ITM_SLOT_MAIN_BANK3 = 41
            ITM_SLOT_MAIN_BANK4 = 42
            ITM_SLOT_MAIN_BANK5 = 43
            ITM_SLOT_MAIN_BANK6 = 44
            ITM_SLOT_MAIN_BANK7 = 45
            ITM_SLOT_MAIN_BANK8 = 46
            ITM_SLOT_MAIN_BANK9 = 47
            ITM_SLOT_MAIN_BANK10 = 48
            ITM_SLOT_MAIN_BANK11 = 49
            ITM_SLOT_MAIN_BANK12 = 50
            ITM_SLOT_MAIN_BANK13 = 51
            ITM_SLOT_MAIN_BANK14 = 52
            ITM_SLOT_MAIN_BANK15 = 53
            ITM_SLOT_MAIN_BANK16 = 54
            ITM_SLOT_MAIN_BANK17 = 55
            ITM_SLOT_MAIN_BANK18 = 56
            ITM_SLOT_MAIN_BANK19 = 57
            ITM_SLOT_MAIN_BANK20 = 58
            ITM_SLOT_MAIN_BANK21 = 59
            ITM_SLOT_MAIN_BANK22 = 60
            ITM_SLOT_MAIN_BANK23 = 61
            ITM_SLOT_MAIN_BANK24 = 62
            ITM_SLOT_MAIN_BANK25 = 63
            ITM_SLOT_MAIN_BANK26 = 64
            ITM_SLOT_MAIN_BANK27 = 65
            ITM_SLOT_MAIN_BANK28 = 66
            ITM_SLOT_MAIN_BANK29 = 67
            ITM_SLOT_MAIN_BANK30 = 68
            ITM_SLOT_MAIN_BANK31 = 69
            ITM_SLOT_MAIN_BANK32 = 70
            ITM_SLOT_MAIN_BANK33 = 71
            ITM_SLOT_MAIN_BANK34 = 72
            ITM_SLOT_MAIN_BANK35 = 73
            ITM_SLOT_KEY1 = 86
            ITM_SLOT_KEY2 = 87
            ITM_SLOT_KEY3 = 88
            ITM_SLOT_KEY4 = 89
            ITM_SLOT_KEY5 = 90
            ITM_SLOT_KEY6 = 91
            ITM_SLOT_KEY7 = 92
            ITM_SLOT_KEY8 = 93
            ITM_SLOT_KEY9 = 94
            ITM_SLOT_KEY10 = 95
            ITM_SLOT_KEY11 = 96
            ITM_SLOT_KEY12 = 97
            ITM_SLOT_KEY13 = 98
            ITM_SLOT_KEY14 = 99
            ITM_SLOT_KEY15 = 100
            ITM_SLOT_KEY16 = 101
            ITM_SLOT_KEY17 = 102
            ITM_SLOT_KEY18 = 103
            ITM_SLOT_KEY19 = 104
            ITM_SLOT_KEY20 = 105
            ITM_SLOT_KEY21 = 106
            ITM_SLOT_KEY22 = 107
            ITM_SLOT_KEY23 = 108
            ITM_SLOT_KEY24 = 109
            ITM_SLOT_KEY25 = 110
            ITM_SLOT_KEY26 = 111
            ITM_SLOT_KEY27 = 112
            ITM_SLOT_KEY28 = 113
            ITM_SLOT_KEY29 = 114
            ITM_SLOT_KEY30 = 115
            ITM_SLOT_KEY31 = 116
            ITM_SLOT_KEY32 = 117
        End Enum

        Public Sub New()
        End Sub
    End Class

    <Serializable()>
    Public Class Glyph
        Public Id As Integer
        Public Type As Integer '1=minor;2=major;3=prime
        Public Spec As Integer '1;2
        Public Name As String
        Public Slotname As String 'e.g. majorglyph1
        Public Image As Image

        Public Sub New()
        End Sub
    End Class

    <Serializable()>
    Public Class Achievement
        Public Id As Integer
        Public Name As String
        Public Description As String
        Public Icon As Image
        Public GainDate As Integer
        Public OwnerSet As Integer
        Public SubCategory As Integer '//only for interfaces
        Public SubCategoryName As String

        Public Sub New()
        End Sub
    End Class

    <Serializable()>
    Public Class Action
        Public Button As Integer
        Public Spec As Integer
        Public ActionId As Integer
        Public ActionType As Integer
        Public OwnerSet As Integer

        Public Sub New()
        End Sub
    End Class

    <Serializable()>
    Public Class Quest
        Public Id As Integer
        Public Name As String
        Public Status As Integer
        Public Explored As Integer
        Public Timer As Integer
        Public Slot As Integer
        Public Rewarded As Integer

        Public Sub New()
        End Sub
    End Class

    <Serializable()>
    Public Class Reputation
        Public Faction As Integer
        Public Flags As FlagEnum = FlagEnum.FACTION_FLAG_INVISIBLE
        Public Standing As Integer
        Public Name As String
        Public Value As Integer
        Public Max As Integer
        Public Status As RepStatusEnum _
        '0=stranger; 1=acquaintance; 2=unfriendly; 3=neutral; 4=friendly; 5=honored; 6=revered; 7=exalted
        Public Enum RepStatusEnum
            STATUS_STRANGER = 0
            STATUS_ACQUAITANCE = 1
            STATUS_UNFRIENDLY = 2
            STATUS_NEUTRAL = 3
            STATUS_FRIENDLY = 4
            STATUS_HONORED = 5
            STATUS_REVERED = 6
            STATUS_EXALTED = 7
        End Enum
        <Flags> Public Enum FlagEnum
            FACTION_FLAG_INVISIBLE = 0
            FACTION_FLAG_VISIBLE = 1
            FACTION_FLAG_AT_WAR = 2
            FACTION_FLAG_HIDDEN = 4
            FACTION_FLAG_INVISIBLE_FORCED = 8
            FACTION_FLAG_PEACE_FORCED = 16
            FACTION_FLAG_INACTIVE = 32
            FACTION_FLAG_RIVAL = 64
            FACTION_FLAG_SPECIAL = 128
        End Enum
        Public Sub New()
        End Sub
    End Class

    <Serializable()>
    Public Class Skill
        Public Id As Integer
        Public Value As Integer
        Public Max As Integer
        Public Name As String

        Public Sub New()
        End Sub
    End Class

    <Serializable()>
    Public Class Spell
        Public Id As Integer
        Public Active As Integer
        Public Disabled As Integer
        Public Name As String

        Public Sub New()
        End Sub
    End Class

    <Serializable()>
    Public Class Talent
        Public Spell As Integer
        Public Spec As Integer

        Public Sub New()
        End Sub
    End Class

    <Serializable()>
    Public Class Profession
        Public Id As Integer
        Public Name As String
        Public Max As Integer
        Public Iconname As String
        Public Rank As Integer
        Public Primary As Boolean
        Public Recipes() As String

        Public Sub New()
        End Sub
    End Class
End Namespace