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
'*      /Filename:      Filter_characters
'*      /Description:   Contains functions for filtering the character list
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework
Public Class Filter_characters

    Private Sub ApplyFilter_Click(sender As System.Object, e As System.EventArgs) Handles ApplyFilter.Click
        GlobalVariables.modifiedAccTable = GlobalVariables.acctable.Copy
        GlobalVariables.modifiedCharTable = GlobalVariables.chartable.Copy
        If guidcheck.Checked = True Then
            If guidcombo1.SelectedIndex = -1 Then GoTo SkipStatement0
            Dim insertstring As String = " " & guidcombo1.SelectedItem.ToString() & " '" & guidtxtbox1.Text & "'"
            Dim insertstring2 As String = ""
            If Not guidcombo2.SelectedItem = Nothing Then
                insertstring2 = " AND " & GlobalVariables.sourceStructure.char_guid_col(0) & " " & guidcombo2.SelectedItem.ToString & " '" & guidtxtbox2.Text & "'"
            End If
            Dim foundRows() As DataRow
            Dim clonedDT As DataTable = GlobalVariables.modifiedCharTable.Copy
            foundRows = clonedDT.Select(GlobalVariables.sourceStructure.char_guid_col(0) & insertstring & insertstring2)
            GlobalVariables.modifiedCharTable.Rows.Clear()
            For i = 0 To foundRows.GetUpperBound(0)
                GlobalVariables.modifiedCharTable.ImportRow(foundRows(i))
            Next i
        End If
SkipStatement0:
        If namecheck.Checked = True Then
            Dim insertstring As String
            Select Case namecombo1.SelectedIndex
                Case 0
                    insertstring = " like '%" & nametxtbox1.Text & "%'"
                Case 1
                    insertstring = " = '" & nametxtbox1.Text & "'"
                Case Else : GoTo SkipStatement1
            End Select
            Dim foundRows() As DataRow
            Dim clonedDT As DataTable = GlobalVariables.modifiedCharTable.Copy
            foundRows = clonedDT.Select(GlobalVariables.sourceStructure.char_name_col(0) & insertstring)
            GlobalVariables.modifiedCharTable.Rows.Clear()
            For i = 0 To foundRows.GetUpperBound(0)
                GlobalVariables.modifiedCharTable.ImportRow(foundRows(i))
            Next i
        End If
SkipStatement1:
        If racecheck.Checked = True Then
            Dim id As Integer = racecombo.SelectedIndex + 1
            Select Case id
                Case 12 : id = 21
                Case Else : GoTo SkipStatement2
            End Select
            Dim foundRows() As DataRow
            Dim clonedDT As DataTable = GlobalVariables.modifiedCharTable.Copy
            foundRows = clonedDT.Select(GlobalVariables.sourceStructure.char_race_col(0) & " = '" & id.ToString() & "'")
            GlobalVariables.modifiedCharTable.Rows.Clear()
            For i = 0 To foundRows.GetUpperBound(0)
                GlobalVariables.modifiedCharTable.ImportRow(foundRows(i))
            Next i
        End If
SkipStatement2:
        If classcheck.Checked = True Then
            Dim id As Integer = classcombo.SelectedIndex + 1
            If id = -1 Then GoTo SkipStatement3
            Dim foundRows() As DataRow
            Dim clonedDT As DataTable = GlobalVariables.modifiedCharTable.Copy
            foundRows = clonedDT.Select(GlobalVariables.sourceStructure.char_class_col(0) & " = '" & id.ToString() & "'")
            GlobalVariables.modifiedCharTable.Rows.Clear()
            For i = 0 To foundRows.GetUpperBound(0)
                GlobalVariables.modifiedCharTable.ImportRow(foundRows(i))
            Next i
        End If
SkipStatement3:
        If levelcheck.Checked = True Then
            If levelcombo1.SelectedIndex = -1 Then GoTo SkipStatement4
            Dim insertstring As String = " " & levelcombo1.SelectedItem.ToString() & " '" & leveltxtbox1.Text & "'"
            Dim insertstring2 As String = ""
            If Not levelcombo2.SelectedItem = Nothing Then
                insertstring2 = " AND " & GlobalVariables.sourceStructure.char_level_col(0) & " " & levelcombo2.SelectedItem.ToString & " '" & leveltxtbox2.Text & "'"
            End If
            Dim foundRows() As DataRow
            Dim clonedDT As DataTable = GlobalVariables.modifiedCharTable.Copy
            foundRows = clonedDT.Select(GlobalVariables.sourceStructure.char_level_col(0) & insertstring & insertstring2)
            GlobalVariables.modifiedCharTable.Rows.Clear()
            For i = 0 To foundRows.GetUpperBound(0)
                GlobalVariables.modifiedCharTable.ImportRow(foundRows(i))
            Next i
        End If
SkipStatement4:
        For Each CurrentForm As Form In Application.OpenForms
            If CurrentForm.Name = "Live_View" Then
                Dim liveview As Live_View = DirectCast(CurrentForm, Live_View)
                liveview.setcharacterview(GlobalVariables.modifiedCharTable)
            End If
        Next
        Me.Hide()
    End Sub

    Private Sub Filter_characters_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class