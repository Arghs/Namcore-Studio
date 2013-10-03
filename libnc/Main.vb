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
'*      /Filename:      Main
'*      /Description:   Initializing csv & common functions
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Public Class Main
    '// Declaration
    Public Shared AchievementCsv As DataTable
    Public Shared AchievementCategoryCsv As DataTable
    Public Shared FactionCsv As DataTable
    Public Shared GlyphProperties0Csv As DataTable
    Public Shared GlyphProperties1Csv As DataTable
    Public Shared GlyphProperties2Csv As DataTable
    Public Shared ItemCsv As DataTable
    Public Shared ItemDisplayInfoCsv As DataTable
    Public Shared ItemSparseCsv As DataTable
    Public Shared QuestNameCsv As DataTable
    Public Shared SkillLineCsv As DataTable
    Public Shared SpellCsv As DataTable
    Public Shared SpellEffectCsv As DataTable
    Public Shared SpellEnchantCsv As DataTable
    Public Shared SpellIconCsv As DataTable
    Public Shared TalentCsv As DataTable
    '// Declaration

    Public Shared Sub Initialize()
        FillDataTable(My.Resources.Achievement, AchievementCsv)
        FillDataTable(My.Resources.AchievementCategory, AchievementCategoryCsv)
        FillDataTable(My.Resources.Faction, FactionCsv)
        FillDataTable(My.Resources.GlyphProperties0, GlyphProperties0Csv)
        FillDataTable(My.Resources.GlyphProperties1, GlyphProperties1Csv)
        FillDataTable(My.Resources.GlyphProperties2, GlyphProperties2Csv)
        FillDataTable(My.Resources.Item, ItemCsv)
        FillDataTable(My.Resources.ItemDisplayInfo, ItemDisplayInfoCsv)
        FillDataTable(My.Resources.ItemSparse, ItemSparseCsv)
        FillDataTable(My.Resources.questname, QuestNameCsv)
        FillDataTable(My.Resources.SkillLine, SkillLineCsv)
        FillDataTable(My.Resources.Spell, SpellCsv)
        FillDataTable(My.Resources.SpellEffect, SpellEffectCsv)
        FillDataTable(My.Resources.SpellEnchant, SpellEnchantCsv)
        FillDataTable(My.Resources.SpellIcon, SpellIconCsv)
        FillDataTable(My.Resources.talent, TalentCsv)
    End Sub
    Public Shared Sub FillDataTable(ByVal csv As String, ByRef targetTable As DataTable)
        Try
            targetTable = New DataTable()
            Dim a() As String
            Dim strArray As String()
            a = Split(csv, vbNewLine)
            For i = 0 To UBound(a)
                strArray = a(i).Split(CChar("£"))
                If i = 0 Then
                    For Each value As String In strArray
                        targetTable.Columns.Add(value.Trim())
                    Next
                Else
                    targetTable.Rows.Add(strArray)
                End If
            Next i
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub
    Public Shared Function ExecuteCsvSearch(ByVal dt As DataTable, ByVal startfield As String, ByVal startvalue As String, ByVal targetfield As Integer) As String()
        Try
            Dim foundRows() As DataRow
            foundRows = dt.Select(startfield & " = '" & startvalue & "'")
            If foundRows.Length = 0 Then
                Return {"-"}
            Else
                Dim resultArray(foundRows.Count()) As String
                resultArray(0) = "-"
                For i = 0 To foundRows.Count() - 1
                    resultArray(i) = (foundRows(i)(targetfield)).ToString
                Next i
                Return resultArray
            End If
        Catch ex As Exception
            Return {"-"}
        End Try
    End Function
    Public Shared Function SplitString(ByVal source As String, ByVal start As String, ByVal ending As String) As String
        If source Is Nothing Or start Is Nothing Or ending Is Nothing Then
            Return Nothing
        End If
        Try
            Dim quellcode As String = source
            Dim mystart As String = start
            Dim myend As String = ending
            Dim quellcodeSplit As String
            quellcodeSplit = Split(quellcode, mystart, 5)(1)
            Return Split(quellcodeSplit, myend, 6)(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class
