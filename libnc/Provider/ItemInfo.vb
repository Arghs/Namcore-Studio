﻿'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
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
'*      /Filename:      ItemInfo
'*      /Description:   Provides item class information
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports libnc.Main
Namespace Provider
    Public Module ItemInfo
        Public Function GetItemSubClassById(ByVal itemId As Integer) As Integer
            Const targetField As Integer = 2
            Dim myResult As String = ExecuteCsvSearch(ItemCsv, "ItemId", itemId.ToString(), targetField)(0)
            Dim returnResult As Integer
            If myResult = "-" Then myResult = 0
            Try
                returnResult = CInt(myResult)
            Catch
                returnResult = 0
            End Try
            Return returnResult
        End Function
        Public Function GetItemMainClassBySubClassId(ByVal subModuleId As Integer) As Integer
            Const targetField As Integer = 1
            Dim myResult As String = ExecuteCsvSearch(ItemCsv, "SubModule", subModuleId.ToString(), targetField)(0)
            Dim returnResult As Integer
            If myResult = "-" Then myResult = 0
            Try
                returnResult = CInt(myResult)
            Catch
                returnResult = 0
            End Try
            Return returnResult
        End Function
    End Module
End Namespace