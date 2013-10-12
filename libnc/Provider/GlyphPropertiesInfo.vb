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
'*      /Filename:      GlyphPropertiesInfo
'*      /Description:   Provides glyph information
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports libnc.Main
Namespace Provider
    Public Module GlyphPropertiesInfo
        Public Function GetGlyphIdBySpellId(ByVal spellId As Integer, ByVal expansion As Integer) As Integer
            Dim useTable As DataTable
            Select Case expansion
                Case 3 : useTable = GlyphProperties0Csv
                Case 4 : useTable = GlyphProperties1Csv
                Case Else : useTable = GlyphProperties2Csv
            End Select
            Const targetField As Integer = 0
            Dim myResult As String = ExecuteCsvSearch(useTable, "SpellId", spellId.ToString(), targetField)(0)
            Dim returnResult As Integer
            If myResult = "-" Then myResult = 0
            Try
                returnResult = CInt(myResult)
            Catch
                returnResult = 0
            End Try
            Return returnResult
        End Function
        Public Function GetGlyphIdByItemId(ByVal itemId As Integer, ByVal expansion As Integer) As Integer
            '// Work around - solution
            '// Get name of item
            Dim itemName As String = GetItemNameByItemId(itemId, "de")
            itemName = itemName.Replace("'", "''")
            '// Get spell id by item name
            If itemName.Length > 1 Then
                Try
                    Dim foundRows() As DataRow
                    foundRows = SpellCsv.Select("SpellNameDE" & " = '" & itemName & "'")
                    If foundRows.Length = 0 Then
                        Return 0
                    Else
                        If foundRows.Count = 1 Then
                            Try
                                '// Get glyph id by item id
                                Return GetGlyphIdBySpellId(CInt(foundRows(0)(0).ToString()), expansion)
                            Catch ex As Exception
                                Return 0
                            End Try
                        End If
                        For i = 0 To foundRows.Count() - 1
                            '// Multiple results: If description value is greater than 1 > correct id
                            If (foundRows(i)(3)).ToString.Length > 1 Then
                                Try
                                    Return GetGlyphIdBySpellId(CInt((foundRows(i)(0)).ToString), expansion)
                                Catch ex As Exception
                                    Return 0
                                End Try
                            End If
                        Next i
                        Return 0
                    End If
                Catch ex As Exception
                    Return 0
                End Try
            Else
                Return 0
            End If
        End Function
        Public Function GetItemIdByGlyphId(ByVal glyphId As Integer, ByVal expansion As Integer) As Integer
            '// Work around - solution
            '// Get name of glyph spell
            Dim spellName As String = GetSpellNameBySpellId(GetSpellIdByGlyphId(glyphId, expansion), "de")
            '// Get item id by name
            If spellName.Length > 1 Then
                Try
                    Dim foundRows() As DataRow
                    foundRows = ItemSparseCsv.Select("ItemNameDE" & " = '" & spellName & "'")
                    If foundRows.Length = 0 Then
                        Return 0
                    Else
                        Try
                            Return CInt(foundRows(0)(0).ToString())
                        Catch ex As Exception
                            Return 0
                        End Try
                    End If
                Catch ex As Exception
                    Return 0
                End Try
            Else
                Return 0
            End If
        End Function
        Public Function GetSpellIdByGlyphId(ByVal glyphId As Integer, ByVal expansion As Integer) As Integer
            Dim useTable As DataTable
            Select Case expansion
                Case 3 : useTable = GlyphProperties0Csv
                Case 4 : useTable = GlyphProperties1Csv
                Case Else : useTable = GlyphProperties2Csv
            End Select
            Const targetField As Integer = 1
            Dim myResult As String = ExecuteCsvSearch(useTable, "GlyphId", glyphId.ToString(), targetField)(0)
            Dim returnResult As Integer
            If myResult = "-" Then myResult = 0
            Try
                returnResult = CInt(myResult)
            Catch
                returnResult = 0
            End Try
            Return returnResult
        End Function
        Public Function GetIconByGlyphId(ByVal glyphId As Integer, ByVal expansion As Integer) As Integer
            Dim useTable As DataTable
            Select Case expansion
                Case 3 : useTable = GlyphProperties0Csv
                Case 4 : useTable = GlyphProperties1Csv
                Case Else : useTable = GlyphProperties2Csv
            End Select
            Const targetField As Integer = 2
            Dim myResult As String = ExecuteCsvSearch(useTable, "GlyphId", glyphId.ToString(), targetField)(0)
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