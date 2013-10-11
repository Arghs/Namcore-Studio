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
        Public Rarity As Integer '0=poor;1=common;2=uncommon;3=rare;4=epic;5=legendary;6=artifact/heirloom
        Public Slotname As String
        Public Slot As Integer
        Public Socket1Id As Integer
        Public Socket2Id As Integer
        Public Socket3Id As Integer
        Public Socket1Pic As Image
        Public Socket2Pic As Image
        Public Socket3Pic As Image
        Public Socket1Name As String
        Public Socket2Name As String
        Public Socket3Name As String
        Public EnchantmentType As Integer '1=spell;2=item
        Public EnchantmentId As Integer
        Public EnchantmentName As String
        Public Enchstring As String
        Public Socket1Effectid As Integer
        Public Socket2Effectid As Integer
        Public Socket3Effectid As Integer
        Public EnchantmentEffectid As Integer
        Public Image As Image

        Public Sub New()
        End Sub
    End Class

    <Serializable()>
    Public Class InventItem
        Public UpdateRequest As Integer = 0 '0=no; 1=create; 2=delete; 3=update
        Public Entry As Integer
        Public Slot As Integer
        Public Bag As Integer
        Public Bagguid As Integer
        Public Enchantstring As String
        Public Count As Integer
        Public Container As Integer
        Public Guid As Integer
        Public Socket1Id As Integer
        Public Socket2Id As Integer
        Public Socket3Id As Integer
        Public Socket1Effectid As Integer
        Public Socket2Effectid As Integer
        Public Socket3Effectid As Integer
        Public EnchantmentId As Integer
        Public EnchantmentEffectid As Integer
        Public BagItems As List(Of InventItem)
        Public Image As Image
        Public Name As String
        Public SlotCount As Integer
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
        Public Flags As Integer
        Public Standing As Integer
        Public Name As String
        Public Value As Integer
        Public Max As Integer
        Public Status As Integer _
        '0=stranger; 1=acquaintance; 2=unfriendly; 3=neutral; 4=friendly; 5=honored; 6=revered; 7=exalted
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