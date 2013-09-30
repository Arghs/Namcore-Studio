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
'*      /Filename:      GlyphPropertiesInfo
'*      /Description:   Provides glyph information
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports libnc.Main
Namespace Provider
    Public Class GlyphPropertiesInfo
        Public Function GetGlyphIdBySpellId(ByVal spellId As Integer, ByVal expansion As Integer) As Integer
            Dim useTable As DataTable
            Select Case expansion
                Case 3 : useTable = GlyphProperties0Csv
                Case 4 : useTable = GlyphProperties1Csv
                Case Else : useTable = GlyphProperties2Csv
            End Select
            Const targetField As Integer = 1
            Dim myResult As String = ExecuteCsvSearch(useTable, "SpellId", spellId.ToString(), targetField)(0)
            Dim returnResult As Integer
            If myResult = "-" Then returnResult = 0
            Try
                returnResult = CInt(myResult)
            Catch
                returnResult = 0
            End Try
            Return returnResult
        End Function
        Public Function GetSpellIdByGlyphId(ByVal glyphId As Integer, ByVal expansion As Integer) As Integer
            Dim useTable As DataTable
            Select Case expansion
                Case 3 : useTable = GlyphProperties0Csv
                Case 4 : useTable = GlyphProperties1Csv
                Case Else : useTable = GlyphProperties2Csv
            End Select
            Const targetField As Integer = 0
            Dim myResult As String = ExecuteCsvSearch(useTable, "GlyphId", glyphId.ToString(), targetField)(0)
            Dim returnResult As Integer
            If myResult = "-" Then returnResult = 0
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
            If myResult = "-" Then returnResult = 0
            Try
                returnResult = CInt(myResult)
            Catch
                returnResult = 0
            End Try
            Return returnResult
        End Function
    End Class
End Namespace