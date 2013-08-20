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
'*      /Filename:      ReplaceItemExtension
'*      /Description:   Extension to update an existing item by new id
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Imports Namcore_Studio_Framework.SpellItem_Information
Imports Namcore_Studio_Framework
Module ReplaceItemExtension
    ''' <summary>
    ''' Replaces an Item
    ''' </summary>
    <System.Runtime.CompilerServices.Extension()>
    Public Function ReplaceItem(ByRef SourceItem As Item, ByVal newitemid As Integer) As Item
        Dim itm As Item
        itm = SourceItem
        itm.id = newitemid
        itm.name = getNameOfItem(newitemid.ToString())
        itm.image = GetIconByItemId(newitemid)
        itm.rarity = GetRarityByItemId(newitemid)
        Return itm

    End Function

End Module
