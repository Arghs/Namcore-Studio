'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013-2014 NamCore Studio <https://github.com/megasus/Namcore-Studio>
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
'*      /Filename:      FilterAccounts
'*      /Description:   Contains functions for filtering the account list
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NamCore_Studio.Modules.Interface
Imports NCFramework.Framework.Modules

Namespace Forms
    Public Class FilterAccounts
        '// Declaration
        Private _ptMouseDownLocation As Point
        '// Declaration      

        Public Overridable Sub me_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
            If e.Button = MouseButtons.Left Then
                _ptMouseDownLocation = e.Location
            End If
        End Sub

        Public Overridable Sub me_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
            If e.Button = MouseButtons.Left Then
                Location = New Point(e.Location.X - _ptMouseDownLocation.X + Location.X,
                                     e.Location.Y - _ptMouseDownLocation.Y + Location.Y)
            End If
        End Sub

        Private Sub ApplyFilter_Click(sender As Object, e As EventArgs) Handles ApplyFilter.Click
            GlobalVariables.modifiedAccTable = GlobalVariables.acctable.Copy
            GlobalVariables.modifiedCharTable = GlobalVariables.chartable.Copy
            If idcheck.Checked = True Then
                If idcombo1.SelectedIndex = - 1 Then GoTo SkipStatement0
                Dim insertstring As String = " " & idcombo1.SelectedItem.ToString() & " '" & idtxtbox1.Text & "'"
                Dim insertstring2 As String = ""
                If Not idcombo2.SelectedItem Is Nothing Then
                    insertstring2 = " AND " & GlobalVariables.sourceStructure.acc_id_col(0) & " " &
                                    idcombo2.SelectedItem.ToString & " '" & idtxtbox2.Text & "'"
                End If
                Dim foundRows() As DataRow
                Dim clonedDt As DataTable = GlobalVariables.modifiedAccTable.Copy
                foundRows = clonedDt.Select(GlobalVariables.sourceStructure.acc_id_col(0) & insertstring & insertstring2)
                GlobalVariables.modifiedAccTable.Rows.Clear()
                For i = 0 To foundRows.GetUpperBound(0)
                    GlobalVariables.modifiedAccTable.ImportRow(foundRows(i))
                Next i
            End If
            SkipStatement0:
            If namecheck.Checked = True Then
                Dim insertstring As String
                Select Case namecombo1.SelectedIndex - 1
                    Case 1 'todo
                        insertstring = " like '%" & nametxtbox1.Text & "%'"
                    Case 0
                        insertstring = " = '" & nametxtbox1.Text & "'"
                    Case Else : GoTo SkipStatement1
                End Select
                Dim foundRows() As DataRow
                Dim clonedDt As DataTable = GlobalVariables.modifiedAccTable.Copy
                foundRows = clonedDt.Select(GlobalVariables.sourceStructure.acc_name_col(0) & insertstring)
                GlobalVariables.modifiedAccTable.Rows.Clear()
                For i = 0 To foundRows.GetUpperBound(0)
                    GlobalVariables.modifiedAccTable.ImportRow(foundRows(i))
                Next i
            End If
            SkipStatement1:
            If gmcheck.Checked = True Then
                If gmcombo1.SelectedIndex = - 1 Then GoTo SkipStatement2
                Dim insertstring As String = " " & gmcombo1.SelectedItem.ToString() & " '" & gmtxtbox1.Text & "'"
                Dim insertstring2 As String = ""
                Dim gmlevelCol As String
                If GlobalVariables.sourceStructure.acc_gmlevel_col(0) = Nothing Then
                    gmlevelCol = GlobalVariables.sourceStructure.accAcc_gmLevel_col(0)
                Else
                    gmlevelCol = GlobalVariables.sourceStructure.acc_gmlevel_col(0)
                End If
                If Not gmcombo2.SelectedItem = Nothing Then
                    insertstring2 = " AND " & gmlevelCol & " " & gmcombo2.SelectedItem.ToString & " '" & gmtxtbox2.Text &
                                    "'"
                End If
                Dim foundRows() As DataRow
                Dim clonedDt As DataTable = GlobalVariables.modifiedAccTable.Copy
                foundRows = clonedDt.Select(gmlevelCol & insertstring & insertstring2)
                GlobalVariables.modifiedAccTable.Rows.Clear()
                For i = 0 To foundRows.GetUpperBound(0)
                    GlobalVariables.modifiedAccTable.ImportRow(foundRows(i))
                Next i
            End If
            SkipStatement2:
            If logincheck.Checked = True Then
                If logincombo1.SelectedIndex = - 1 Then GoTo SkipStatement3
                Dim insertstring As String = " " & logincombo1.SelectedItem.ToString() & " '" & datemin.Text & "'"
                Dim insertstring2 As String = ""
                GlobalVariables.sourceStructure.acc_lastlogin_col(0) = "last_login" 'todo
                If Not logincombo2.SelectedItem = Nothing Then
                    insertstring2 = " AND " & GlobalVariables.sourceStructure.acc_lastlogin_col(0) & " " &
                                    logincombo2.SelectedItem.ToString & " '" & datemax.Text & "'"
                End If
                'Watch datetime format: mangos requires yyy-MM-dd HH:mm:ss todo
                Dim foundRows() As DataRow
                Dim clonedDt As DataTable = GlobalVariables.modifiedAccTable.Copy
                foundRows =
                    clonedDt.Select(GlobalVariables.sourceStructure.acc_lastlogin_col(0) & insertstring & insertstring2)
                GlobalVariables.modifiedAccTable.Rows.Clear()
                For i = 0 To foundRows.GetUpperBound(0)
                    GlobalVariables.modifiedAccTable.ImportRow(foundRows(i))
                Next i
            End If
            SkipStatement3:
            If emailcheck.Checked = True Then
                Dim insertstring As String
                Select Case emailcombo1.SelectedIndex - 1
                    Case 1 'todo
                        insertstring = " like '%" & emailtxtbox1.Text & "%'"
                    Case 0
                        insertstring = " = '" & emailtxtbox1.Text & "'"
                    Case Else : GoTo SkipStatement4
                End Select
                Dim foundRows() As DataRow
                Dim clonedDt As DataTable = GlobalVariables.modifiedAccTable.Copy
                foundRows = clonedDt.Select("email" & insertstring)
                GlobalVariables.modifiedAccTable.Rows.Clear()
                For i = 0 To foundRows.GetUpperBound(0)
                    GlobalVariables.modifiedAccTable.ImportRow(foundRows(i))
                Next i
            End If
            SkipStatement4:
            For Each currentForm As Form In Application.OpenForms
                If currentForm.Name = "LiveView" Then
                    Dim myliveview As LiveView = DirectCast(currentForm, LiveView)
                    myliveview.Setaccountview(GlobalVariables.modifiedAccTable)
                End If
            Next
            Hide()
        End Sub

        Private Sub FilterAccounts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            For Each ctrl As Control In Controls
                ctrl.SetDoubleBuffered()
            Next
        End Sub

        Private Sub close_bt_Click(sender As Object, e As EventArgs) Handles close_bt.Click
            Close()
        End Sub
    End Class
End Namespace