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
'*F:\Projekte\Visual Studio\Namcore-Studio\Namcore Studio Framework\Framework\Functions\SpellItemInformation.vb
'* Developed by Alcanmage/megasus
'*
'* //FileInfo//
'*      /Filename:      IconInfo
'*      /Description:   Provides icon information for spells and items
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports libnc.Main
Imports System.Drawing

Namespace Provider
    Public Module IconInfo
        Public Function GetSpellIconById(ByVal spellId As Integer) As Image
            Const targetField As Integer = 1
            Dim myResult As String = ExecuteCsvSearch(SpellIconCsv, "SpellId", spellId.ToString(), targetField)(0)
            If myResult = "-" Then
                Return My.Resources.INV_Misc_QuestionMark
            End If
            Try
                Return libncadvanced.My.Resources.ResourceManager.GetObject(myResult)
            Catch
                Return My.Resources.INV_Misc_QuestionMark
            End Try
        End Function
        Public Function GetItemIconById(ByVal itemId As Integer) As Image
            Const targetField As Integer = 1
            Dim myResult As String = ExecuteCsvSearch(ItemDisplayInfoCsv, "ItemId", itemId.ToString(), targetField)(0)
            If myResult = "-" Then
                Return My.Resources.INV_Misc_QuestionMark
            End If
            Try
                Return libncadvanced.My.Resources.ResourceManager.GetObject(myResult)
            Catch
                Return My.Resources.INV_Misc_QuestionMark
            End Try
        End Function
    End Module
End Namespace