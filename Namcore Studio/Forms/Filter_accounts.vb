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
'*
'* Developed by Alcanmage/megasus
'*
'* //FileInfo//
'*      /Filename:      Filter_accounts
'*      /Description:   Contains functions for filtering the account list
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports Namcore_Studio.GlobalVariables
Public Class Filter_accounts

    Private Sub ApplyFilter_Click(sender As System.Object, e As System.EventArgs) Handles ApplyFilter.Click
        GlobalVariables.modifiedAccTable = GlobalVariables.acctable.Copy
        GlobalVariables.modifiedCharTable = GlobalVariables.chartable.Copy
        If idcheck.Checked = True Then
            If idcombo1.SelectedIndex = -1 Then GoTo SkipStatement0
            Dim insertstring As String = " " & idcombo1.SelectedItem.ToString() & " '" & idtxtbox1.Text & "'"
            Dim insertstring2 As String = ""
            If Not idcombo2.SelectedItem = Nothing Then
                insertstring2 = " AND " & sourceStructure.acc_id_col(0) & " " & idcombo2.SelectedItem.ToString & " '" & idtxtbox2.Text & "'"
            End If
            Dim foundRows() As DataRow
            Dim clonedDT As DataTable = GlobalVariables.modifiedAccTable.Copy
            foundRows = clonedDT.Select(sourceStructure.acc_id_col(0) & insertstring & insertstring2)
            GlobalVariables.modifiedAccTable.Rows.Clear()
            For i = 0 To foundRows.GetUpperBound(0)
                GlobalVariables.modifiedAccTable.ImportRow(foundRows(i))
            Next i
        End If
SkipStatement0:
        If namecheck.Checked = True Then
            Dim insertstring As String
            Select Case namecombo1.SelectedIndex
                Case 0 'todo
                    insertstring = " like '%" & nametxtbox1.Text & "%'"
                Case 1
                    insertstring = " = '" & nametxtbox1.Text & "'"
                Case Else : GoTo SkipStatement1
            End Select
            Dim foundRows() As DataRow
            Dim clonedDT As DataTable = GlobalVariables.modifiedAccTable.Copy
            foundRows = clonedDT.Select(sourceStructure.acc_name_col(0) & insertstring)
            GlobalVariables.modifiedAccTable.Rows.Clear()
            For i = 0 To foundRows.GetUpperBound(0)
                GlobalVariables.modifiedAccTable.ImportRow(foundRows(i))
            Next i
        End If
SkipStatement1:
        If gmcheck.Checked = True Then
            If gmcombo1.SelectedIndex = -1 Then GoTo SkipStatement2
            Dim insertstring As String = " " & gmcombo1.SelectedItem.ToString() & " '" & gmtxtbox1.Text & "'"
            Dim insertstring2 As String = ""
            Dim gmlevelCOL As String
            If sourceStructure.acc_gmlevel_col(0) = Nothing Then
                gmlevelCOL = sourceStructure.accAcc_gmLevel_col(0)
            Else
                gmlevelCOL = sourceStructure.acc_gmlevel_col(0)
            End If
            If Not gmcombo2.SelectedItem = Nothing Then
                insertstring2 = " AND " & gmlevelCOL & " " & gmcombo2.SelectedItem.ToString & " '" & gmtxtbox2.Text & "'"
            End If
            Dim foundRows() As DataRow
            Dim clonedDT As DataTable = GlobalVariables.modifiedAccTable.Copy
            foundRows = clonedDT.Select(gmlevelCOL & insertstring & insertstring2)
            GlobalVariables.modifiedAccTable.Rows.Clear()
            For i = 0 To foundRows.GetUpperBound(0)
                GlobalVariables.modifiedAccTable.ImportRow(foundRows(i))
            Next i
        End If
SkipStatement2:
        If logincheck.Checked = True Then
            If logincombo1.SelectedIndex = -1 Then GoTo SkipStatement3
            Dim insertstring As String = " " & logincombo1.SelectedItem.ToString() & " '" & datemin.Text & "'"
            Dim insertstring2 As String = ""
            sourceStructure.acc_lastlogin_col(0) = "last_login" 'todo
            If Not logincombo2.SelectedItem = Nothing Then
                insertstring2 = " AND " & sourceStructure.acc_lastlogin_col(0) & " " & logincombo2.SelectedItem.ToString & " '" & datemax.Text & "'"
            End If
            'Watch datetime format: mangos requires yyy-MM-dd HH:mm:ss todo
            Dim foundRows() As DataRow
            Dim clonedDT As DataTable = GlobalVariables.modifiedAccTable.Copy
            foundRows = clonedDT.Select(sourceStructure.acc_lastlogin_col(0) & insertstring & insertstring2)
            GlobalVariables.modifiedAccTable.Rows.Clear()
            For i = 0 To foundRows.GetUpperBound(0)
                GlobalVariables.modifiedAccTable.ImportRow(foundRows(i))
            Next i
        End If
SkipStatement3:
        If emailcheck.Checked = True Then
            Dim insertstring As String
            Select Case emailcombo1.SelectedIndex
                Case 0 'todo
                    insertstring = " like '%" & emailtxtbox1.Text & "%'"
                Case 1
                    insertstring = " = '" & emailtxtbox1.Text & "'"
                Case Else : GoTo SkipStatement4
            End Select
            Dim foundRows() As DataRow
            Dim clonedDT As DataTable = GlobalVariables.modifiedAccTable.Copy
            foundRows = clonedDT.Select("email" & insertstring)
            GlobalVariables.modifiedAccTable.Rows.Clear()
            For i = 0 To foundRows.GetUpperBound(0)
                GlobalVariables.modifiedAccTable.ImportRow(foundRows(i))
            Next i
        End If
SkipStatement4:
        For Each CurrentForm As Form In Application.OpenForms
            If CurrentForm.Name = "Live_View" Then
                Dim liveview As Live_View = DirectCast(CurrentForm, Live_View)
                liveview.setaccountview(GlobalVariables.modifiedAccTable)
            End If
        Next
        Me.Hide()
    End Sub

    Private Sub Filter_accounts_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
       
    End Sub
End Class