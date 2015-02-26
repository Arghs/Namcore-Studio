
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
'*      /Filename:      ProfileExtension
'*      /Description:   Profiles extension
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.Runtime.CompilerServices
Imports NCFramework.Framework.Modules

Namespace Framework.Extension
    Public Module ProfileExtension
    ''' <summary>
    '''     Removes item enchantments
    ''' </summary>
                                  <Extension()>
        Public Function RemoveEnchantments(ByRef itm As Item) As Item
            itm.EnchantmentEffectid = Nothing
            itm.EnchantmentId = Nothing
            itm.EnchantmentName = Nothing
            itm.EnchantmentType = Nothing
            Return itm
        End Function


        ''' <summary>
        '''     Removes item gems by position
        ''' </summary>
        <Extension()>
        Public Function RemoveGem(ByRef itm As Item, ByVal position As Integer) As Item
            Select Case position
                Case 1
                    itm.Socket1Effectid = Nothing
                    itm.Socket1Id = Nothing
                    itm.Socket1Name = Nothing
                    itm.Socket1Pic = Nothing
                Case 2
                    itm.Socket2Effectid = Nothing
                    itm.Socket2Id = Nothing
                    itm.Socket2Name = Nothing
                    itm.Socket2Pic = Nothing
                Case 3
                    itm.Socket3Effectid = Nothing
                    itm.Socket3Id = Nothing
                    itm.Socket3Name = Nothing
                    itm.Socket3Pic = Nothing
            End Select
            Return itm
        End Function
    End Module
End Namespace