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
<Serializable()> _
Public Class Item

    Public id As Integer
    Public name As String
    Public rarity As Integer '0=poor;1=common;2=uncommon;3=rare;4=epic;5=legendary;6=artifact/heirloom
    Public slotname As String
    Public slot As Integer
    Public socket1_id As Integer
    Public socket2_id As Integer
    Public socket3_id As Integer
    Public socket1_pic As Image
    Public socket2_pic As Image
    Public socket3_pic As Image
    Public socket1_name As String
    Public socket2_name As String
    Public socket3_name As String
    Public enchantment_type As Integer '1=spell;2=item
    Public enchantment_id As Integer
    Public enchantment_name As String
    Public enchstring As String
    Public image As Image
    Public Sub New()

    End Sub


End Class
<Serializable()> _
Public Class InventItem

    Public entry As Integer
    Public slot As Integer
    Public bag As Integer
    Public bagguid As Integer
    Public enchantstring As String
    Public count As Integer
    Public container As Integer
    Public guid As Integer

    Public Sub New()

    End Sub


End Class
<Serializable()> _
Public Class Glyph

    Public id As Integer
    Public type As Integer '1=minor;2=major;3=prime
    Public spec As Integer '1;2
    Public name As String
    Public slotname As String 'e.g. majorglyph1
    Public image As Image
    Public Sub New()

    End Sub


End Class
<Serializable()> _
Public Class Achievement

    Public Id As Integer
    Public GainDate As Integer
    Public OwnerSet As Integer
    Public SubCategory As Integer '//only for interfaces
    Public Sub New()

    End Sub


End Class
<Serializable()> _
Public Class Action

    Public Button As Integer
    Public Spec As Integer
    Public ActionId As Integer
    Public ActionType As Integer
    Public OwnerSet As Integer

    Public Sub New()

    End Sub


End Class
<Serializable()> _
Public Class Quest
    Public id As Integer
    Public status As Integer
    Public explored As Integer
    Public timer As Integer
    Public slot As Integer
    Public rewarded As Integer
    Public Sub New()

    End Sub
End Class
<Serializable()> _
Public Class Reputation
    Public faction As Integer
    Public flags As Integer
    Public standing As Integer
    Public name As String
    Public value As Integer
    Public max As Integer
    Public status As Integer '0=stranger; 1=acquaintance; 2=unfriendly; 3=neutral; 4=friendly; 5=honored; 6=revered; 7=exalted
    Public Sub New()

    End Sub
End Class
<Serializable()> _
Public Class Skill
    Public id As Integer
    Public value As Integer
    Public max As Integer

    Public Sub New()

    End Sub
End Class
<Serializable()> _
Public Class Spell
    Public id As Integer
    Public active As Integer
    Public disabled As Integer

    Public Sub New()

    End Sub
End Class
<Serializable()> _
Public Class Talent
    Public spell As Integer
    Public spec As Integer
    Public Sub New()

    End Sub
End Class