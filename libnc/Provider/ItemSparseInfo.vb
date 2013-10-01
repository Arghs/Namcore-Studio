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
'*      /Filename:      ItemSparseInfo
'*      /Description:   Provides gereral item information
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports libnc.Main
Namespace Provider
    Public Module ItemSparseInfo
        Public Function GetItemQualityByItemId(ByVal itemId As Integer) As Integer
            Const targetField As Integer = 1
            Dim myResult As String = ExecuteCsvSearch(ItemSparseCsv, "ItemId", itemId.ToString(), targetField)(0)
            Dim returnResult As Integer
            If myResult = "-" Then returnResult = 0
            Try
                returnResult = CInt(myResult)
            Catch
                returnResult = 0
            End Try
            Return returnResult
        End Function
        Public Function GetItemInventorySlotByItemId(ByVal itemId As Integer) As Integer
            Const targetField As Integer = 2
            Dim myResult As String = ExecuteCsvSearch(ItemSparseCsv, "ItemId", itemId.ToString(), targetField)(0)
            Dim returnResult As Integer
            If myResult = "-" Then returnResult = 0
            Try
                returnResult = CInt(myResult)
            Catch
                returnResult = 0
            End Try
            Return returnResult
        End Function
        Public Function GetItemMaxStackByItemId(ByVal itemId As Integer) As Integer
            Const targetField As Integer = 3
            Dim myResult As String = ExecuteCsvSearch(ItemSparseCsv, "ItemId", itemId.ToString(), targetField)(0)
            Dim returnResult As Integer
            If myResult = "-" Then returnResult = 0
            Try
                returnResult = CInt(myResult)
            Catch
                returnResult = 0
            End Try
            Return returnResult
        End Function
        Public Function GetItemSlotCountByItemId(ByVal itemId As Integer) As Integer
            Const targetField As Integer = 4
            Dim myResult As String = ExecuteCsvSearch(ItemSparseCsv, "ItemId", itemId.ToString(), targetField)(0)
            Dim returnResult As Integer
            If myResult = "-" Then returnResult = 0
            Try
                returnResult = CInt(myResult)
            Catch
                returnResult = 0
            End Try
            Return returnResult
        End Function
        Public Function GetItemSpellIdByItemId(ByVal itemId As Integer) As Integer
            Const targetField As Integer = 5
            Dim myResult As String = ExecuteCsvSearch(ItemSparseCsv, "ItemId", itemId.ToString(), targetField)(0)
            Dim returnResult As Integer
            If myResult = "-" Then returnResult = 0
            Try
                returnResult = CInt(myResult)
            Catch
                returnResult = 0
            End Try
            Return returnResult
        End Function
        Public Function GetItemBagFamilyByItemId(ByVal itemId As Integer) As Integer
            Const targetField As Integer = 8
            Dim myResult As String = ExecuteCsvSearch(ItemSparseCsv, "ItemId", itemId.ToString(), targetField)(0)
            Dim returnResult As Integer
            If myResult = "-" Then returnResult = 0
            Try
                returnResult = CInt(myResult)
            Catch
                returnResult = 0
            End Try
            Return returnResult
        End Function
        Public Function GetItemNameByItemId(ByVal itemId As Integer, ByVal locale As String) As String
            Dim targetField As Integer = 6
            If locale = "en" Then targetField += 1
            Dim myResult As String = ExecuteCsvSearch(ItemSparseCsv, "ItemId", itemId.ToString(), targetField)(0)
            If myResult = "-" Then myResult = "Not found"
            Return myResult
        End Function
        Public Function GetItemSlotNameBySlotId(ByVal slotId As Integer) As Integer
            If slotid = 0 Then Return Nothing
            Select Case slotid
                Case 0
                    Return "head"
                Case 1
                    Return "neck"
                Case 2
                    Return "shoulder"
                Case 3
                    Return "shirt"
                Case 4
                    Return "chest"
                Case 5
                    Return "waist"
                Case 6
                    Return "legs"
                Case 7
                    Return "feet"
                Case 8
                    Return "wrists"
                Case 9
                    Return "hands"
                Case 10
                    Return "finger1"
                Case 11
                    Return "finger2"
                Case 12
                    Return "trinket1"
                Case 13
                    Return "trinket2"
                Case 14
                    Return "back"
                Case 15
                    Return "main"
                Case 16
                    Return "off"
                Case 17
                    Return Nothing
                    'slot 17 has been removed as of patch 5.0
                Case 18
                    Return "tabard"
                Case Else : Return Nothing
            End Select
        End Function
    End Module
End Namespace