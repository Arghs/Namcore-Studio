'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013-2016 NamCore Studio <https://github.com/megasus/Namcore-Studio>
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
'*      /Filename:      ListViewComparer
'*      /Description:   Sorting Listview controls
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Namespace Modules.Interface
    Public Class ListViewComparer
        Implements IComparer

        '// Declaration
        Private _intColumn As Integer
        Private _soSortOrder As SortOrder
        Private ReadOnly _cicComparer As CaseInsensitiveComparer
        '// Declaration

        Public Sub New(parentListView As ListView)
            _intColumn = 0
            _soSortOrder = SortOrder.None
            _cicComparer = New CaseInsensitiveComparer()
            ParentListView.ListViewItemSorter = Me
        End Sub

        Public Property SortOrder As SortOrder
            Get
                Return _soSortOrder
            End Get
            Set
                _soSortOrder = value
            End Set
        End Property

        Public Property SortColumn As Integer
            Get
                Return _intColumn
            End Get
            Set
                _intColumn = value
            End Set
        End Property

        Public Function Compare(x As Object, y As Object) As Integer Implements IComparer.Compare
            Dim compareResult As Integer
            Dim listviewX As ListViewItem, listviewY As ListViewItem
            listviewX = CType(x, ListViewItem)
            listviewY = CType(y, ListViewItem)

            compareResult = _cicComparer.Compare(listviewX.SubItems(_intColumn).Text,
                                                 listviewY.SubItems(_intColumn).Text)
            If _soSortOrder = SortOrder.Ascending Then
                Return compareResult
            ElseIf _soSortOrder = SortOrder.Descending Then
                Return compareResult*- 1
            Else
                Return 0
            End If
        End Function
    End Class
End Namespace