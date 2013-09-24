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
'*      /Filename:      Spell
'*      /Description:   Spell dbc structure
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Namespace Structures
    Public Class Spell
        Public Id As UInteger
        Public Name As String
        Public SubText As Unused
        Public Description As Unused
        Public AuraDescription As Unused
        Public RuneCostId As UInteger
        Public SpellMissileId As UInteger
        Public SpellDescriptionVariableId As UInteger
        Public Unknown As Single
        Public SpellScalingId As UInteger
        Public SpellAuraOptionsId As UInteger
        Public SpellAuraRestrictionsId As UInteger
        Public SpellCastingRequirementsId As UInteger
        Public SpellCategoriesId As UInteger
        Public SpellClassOptionsId As UInteger
        Public SpellCooldownsId As UInteger
        Public SpellEquippedItemsId As UInteger
        Public SpellInterruptsId As UInteger
        Public SpellLevelsId As UInteger
        Public SpellReagentsId As UInteger
        Public SpellShapeshiftId As UInteger
        Public SpellTargetRestrictionsId As UInteger
        Public SpellTotemsId As UInteger
        Public ResearchProjectId As UInteger
        Public SpellMiscId As UInteger
    End Class
End Namespace