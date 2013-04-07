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
'*      /Description:   Item Object - item information class
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


Public Class Item

    Public id As Integer
    Public name As String
    Public rarity As Integer '1=poor;2=common;3=uncommon;3=rare;4=epic;5=legendary;6=artifact/heirloom
    Public slotname As String
    Public socket1_id As Integer
    Public socket2_id As Integer
    Public socket3_id As Integer
    Public socket1_name As String
    Public socket2_name As String
    Public socket3_name As String
    Public socket1_img As Image
    Public socket2_img As Image
    Public socket3_img As Image
    Public enchantment_type As Integer '1=spell;2=item
    Public enchantment_id As Integer
    Public enchantment_name As String
    Public image As Image
    Public Sub New()

    End Sub


End Class
