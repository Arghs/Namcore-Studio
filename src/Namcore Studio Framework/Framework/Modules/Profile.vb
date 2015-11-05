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
'*      /Filename:      Profile
'*      /Description:   Profile objects
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.Drawing

Namespace Framework.Modules
    <Serializable>
    Public Class Item
        Public Enum RarityType As Integer
            RARITY_POOR = 0
            RARITY_COMMON = 1
            RARITY_UNCOMMON = 2
            RARITY_RARE = 3
            RARITY_EPIC = 4
            RARITY_LEGENDARY = 5
            RARITY_ARTIFACT_HEIRLOOM = 6
        End Enum

        Public Enum EnchantmentTypes As Integer
            ENCHTYPE_SPELL = 1
            ENCHTYPE_ITEM = 2
        End Enum

        Public Id As Integer
        Public Guid As Integer
        Public Name As String
        Public Rarity As RarityType
        Public Slotname As String
        Public Slot As Integer
        Public Socket1Id As Integer
        Public Socket2Id As Integer
        Public Socket3Id As Integer
        Public Socket1Pic As Bitmap
        Public Socket2Pic As Bitmap
        Public Socket3Pic As Bitmap
        Public Socket1Name As String
        Public Socket2Name As String
        Public Socket3Name As String
        Public EnchantmentType As EnchantmentTypes
        Public EnchantmentId As Integer = 0
        Public EnchantmentName As String
        Public Enchstring As String
        Public Socket1Effectid As Integer
        Public Socket2Effectid As Integer
        Public Socket3Effectid As Integer
        Public EnchantmentEffectid As Integer
        Public Image As Bitmap
        Public AddedBag As Boolean = False
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

        Public Sub New()
        End Sub
    End Class

    <Serializable>
    Public Class Glyph
        Public Enum GlyphType As Integer
            GLYTYPE_MINOR = 1
            GLYTYPE_MAJOR = 2
            GLYTYPE_PRIME = 3
        End Enum

        Public Id As Integer
        Public Type As GlyphType
        Public Spec As Integer
        Public Name As String
        Public Slotname As String '// e.g. majorglyph1
        Public Image As Bitmap

        Public Sub New()
        End Sub
    End Class

    <Serializable>
    Public Class Achievement
        Public Id As Integer
        Public Name As String
        Public Description As String
        Public Icon As Bitmap
        Public GainDate As Integer
        Public OwnerSet As Integer
        Public SubCategory As Integer '//only for interfaces
        Public SubCategoryName As String

        Public Sub New()
        End Sub
    End Class

    <Serializable>
    Public Class Action
        Public Button As Integer
        Public Spec As Integer
        Public ActionId As Integer
        Public ActionType As Integer
        Public OwnerSet As Integer

        Public Sub New()
        End Sub
    End Class

    <Serializable>
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

    <Serializable>
    Public Class Reputation
        Public Enum RepStatus As Integer
            REPSTAT_STRANGER = 0
            REPSTAT_ACQUAINTANCE = 1
            REPSTAT_UNFRIENDLY = 2
            REPSTAT_NEUTRAL = 3
            REPSTAT_FRIENDLY = 4
            REPSTAT_HONORED = 5
            REPSTAT_REVERED = 6
            REPSTAT_EXALTED = 7
        End Enum

        Public Faction As Integer
        Public Flags As FlagEnum = FlagEnum.FACTION_FLAG_INVISIBLE
        Public Standing As Integer
        Public Name As String
        Public Value As Integer
        Public Max As Integer
        Public Status As RepStatus

        <Flags>
        Public Enum FlagEnum
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

    <Serializable>
    Public Class Skill
        Public Id As Integer
        Public Value As Integer
        Public Max As Integer
        Public Name As String

        Public Sub New()
        End Sub
    End Class

    <Serializable>
    Public Class Spell
        Public Id As Integer
        Public Active As Integer
        Public Disabled As Integer
        Public Name As String

        Public Sub New()
        End Sub
    End Class

    <Serializable>
    Public Class Talent
        Public Spell As Integer
        Public Spec As Integer

        Public Sub New()
        End Sub
    End Class

    <Serializable>
    Public Class Profession
        Public Id As Integer
        Public Name As String
        Public Max As Integer
        Public Iconname As String
        Public Rank As Integer
        Public Primary As Boolean
        Public Recipes As List(Of ProfessionSpell)

        Public Sub New()
        End Sub
    End Class

    <Serializable>
    Public Class ProfessionSpell
        Public SpellId As Integer
        Public Name As String
        Public MinSkill As Integer

        Public Sub New()
        End Sub
    End Class
End Namespace