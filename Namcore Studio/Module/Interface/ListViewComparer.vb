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
'*      /Filename:      ListViewComparer
'*      /Description:   Sorting Listview controls
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Public Class ListViewComparer
    Implements IComparer
    Private intColumn As Integer
    Private soSortOrder As SortOrder
    Private cicComparer As CaseInsensitiveComparer

    Public Sub New(ByVal ParentListView As ListView)
        intColumn = 0
        soSortOrder = SortOrder.None
        cicComparer = New CaseInsensitiveComparer()
        ParentListView.ListViewItemSorter = Me
    End Sub

    Public Property SortOrder() As SortOrder
        Get
            Return soSortOrder
        End Get
        Set(ByVal value As SortOrder)
            soSortOrder = value
        End Set
    End Property

    Public Property SortColumn() As Integer
        Get
            Return intColumn
        End Get
        Set(ByVal value As Integer)
            intColumn = value
        End Set
    End Property

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
        Dim compareResult As Integer
        Dim listviewX As ListViewItem, listviewY As ListViewItem
        listviewX = CType(x, ListViewItem)
        listviewY = CType(y, ListViewItem)

        compareResult = cicComparer.Compare(listviewX.SubItems(intColumn).Text, listviewY.SubItems(intColumn).Text)
        If soSortOrder = SortOrder.Ascending Then
            Return compareResult
        ElseIf soSortOrder = SortOrder.Descending Then
            Return compareResult * -1
        Else
            Return 0
        End If
    End Function
End Class