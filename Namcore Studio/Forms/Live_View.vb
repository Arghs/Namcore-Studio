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
'*      /Filename:      Live_View
'*      /Description:   Main interface with following functions:
'*                      -Connect to source/target database
'*                      -List all accounts and characters
'*                      -Editing/Deleting/Transferring accounts and characters
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Imports Namcore_Studio.ConnectionHandler
Imports Namcore_Studio.GlobalVariables
Imports Namcore_Studio.Account_CharacterInformationProcessing
Imports Namcore_Studio.CommandHandler
Imports Namcore_Studio.Conversions
Public Class Live_View
    Private cmpFileListViewComparer As ListViewComparer
    Dim checkchangestatus As Boolean = False
    Private Sub connect_bt_Click(sender As System.Object, e As System.EventArgs) Handles connect_bt.Click
        cmpFileListViewComparer = New ListViewComparer(accountview)
        If defaultconn_radio.Checked = True Then
            If TestConnection("server=" & db_address_txtbox.Text & ";Port=" & port_txtbox.Text & ";User id=" & userid_txtbox.Text & ";Password=" & password_txtbox.Text & ";Database=" & realmdbname_txtbox.Text) = True Then
                If TestConnection("server=" & db_address_txtbox.Text & ";Port=" & port_txtbox.Text & ";User id=" & userid_txtbox.Text & ";Password=" & password_txtbox.Text & ";Database=" & chardbname_txtbox.Text) = True Then
                    GlobalConnectionString = "server=" & db_address_txtbox.Text & ";Port=" & port_txtbox.Text & ";User id=" & userid_txtbox.Text & ";Password=" & password_txtbox.Text & ";Database=" & chardbname_txtbox.Text
                    GlobalConnectionString_Realm = "server=" & db_address_txtbox.Text & ";Port=" & port_txtbox.Text & ";User id=" & userid_txtbox.Text & ";Password=" & password_txtbox.Text & ";Database=" & realmdbname_txtbox.Text
                    OpenNewMySQLConnection(GlobalConnection, GlobalConnectionString)
                    OpenNewMySQLConnection(GlobalConnection_Realm, GlobalConnectionString_Realm)
                    loadaccountsandchars()
                Else

                End If

            Else

            End If
        Else

        End If

    End Sub
    Public Sub loadaccountsandchars()
        checkchangestatus = False
        sourceCore = "trinity" 'for testing only
        acctable = returnAccountTable(GlobalConnection_Realm)
        chartable = returnCharacterTable(GlobalConnection)
        characterview.Items.Clear()
        accountview.Items.Clear()
        For Each rowitem As DataRow In acctable.Rows
            Dim str(4) As String
            Dim itm As ListViewItem
            str(0) = rowitem.Item(0)
            str(1) = rowitem.Item(1)
            str(2) = rowitem.Item(2)
            str(3) = rowitem.Item(3)
            str(4) = rowitem.Item(4)
            itm = New ListViewItem(str)
            accountview.Items.Add(itm)
            accountview.EnsureVisible(accountview.Items.Count - 1)
        Next
        accountview.Update()
        For Each rowitem As DataRow In chartable.Rows
            Dim str(6) As String
            Dim itm As ListViewItem
            str(0) = rowitem.Item(0)
            str(1) = rowitem.Item(1)
            str(2) = rowitem.Item(2)
            str(3) = GetRaceNameById(TryInt(rowitem.Item(3)))
            str(4) = GetClassNameById(TryInt(rowitem.Item(4)))
            str(5) = GetGenderNameById(TryInt(rowitem.Item(5)))
            str(6) = rowitem.Item(6)
            itm = New ListViewItem(str)
            characterview.Items.Add(itm)
            characterview.EnsureVisible(characterview.Items.Count - 1)
        Next
        characterview.Sort()
        characterview.Update()
        checkchangestatus = True
        acctotal.Text = "(" & accountview.Items.Count.ToString() & " Accounts total)"
        chartotal.Text = "(" & characterview.Items.Count.ToString() & " Characters total)"
    End Sub
    Public Sub setaccountview(ByVal accounttable As DataTable)
        checkchangestatus = False
        sourceCore = "trinity" 'for testing only

        characterview.Items.Clear()
        accountview.Items.Clear()
        For Each rowitem As DataRow In accounttable.Rows
            Dim str(4) As String
            Dim itm As ListViewItem
            str(0) = rowitem.Item(0)
            str(1) = rowitem.Item(1)
            str(2) = rowitem.Item(2)
            str(3) = rowitem.Item(3)
            str(4) = rowitem.Item(4)
            itm = New ListViewItem(str)
            accountview.Items.Add(itm)
            accountview.EnsureVisible(accountview.Items.Count - 1)
        Next
        accountview.Update()
        For Each accrowitem As DataRow In accounttable.Rows
            Dim accid As String = accrowitem.Item(0)
            For Each rowitem As DataRow In chartable.Rows
                If rowitem(1) = accid Then
                    Dim str(6) As String
                    Dim itm As ListViewItem
                    str(0) = rowitem.Item(0)
                    str(1) = rowitem.Item(1)
                    str(2) = rowitem.Item(2)
                    str(3) = GetRaceNameById(TryInt(rowitem.Item(3)))
                    str(4) = GetClassNameById(TryInt(rowitem.Item(4)))
                    str(5) = GetGenderNameById(TryInt(rowitem.Item(5)))
                    str(6) = rowitem.Item(6)
                    itm = New ListViewItem(str)
                    characterview.Items.Add(itm)
                    characterview.EnsureVisible(characterview.Items.Count - 1)
                End If
            Next
        Next
        characterview.Update()
        checkchangestatus = True
        acctotal.Text = "(" & accountview.Items.Count.ToString() & " Accounts total)"
        chartotal.Text = "(" & characterview.Items.Count.ToString() & " Characters total)"
    End Sub
    Public Sub setcharacterview(ByVal charactertable As DataTable)
        checkchangestatus = False
        sourceCore = "trinity" 'for testing only
        characterview.Items.Clear()
        accountview.Items.Clear()
        For Each rowitem As DataRow In modifiedAccTable.Rows
            Dim str(4) As String
            Dim itm As ListViewItem
            str(0) = rowitem.Item(0)
            str(1) = rowitem.Item(1)
            str(2) = rowitem.Item(2)
            str(3) = rowitem.Item(3)
            str(4) = rowitem.Item(4)
            itm = New ListViewItem(str)
            accountview.Items.Add(itm)
            accountview.EnsureVisible(accountview.Items.Count - 1)
        Next
        accountview.Update()
        For Each accrowitem As DataRow In modifiedAccTable.Rows
            Dim accid As String = accrowitem.Item(0)
            For Each rowitem As DataRow In charactertable.Rows
                If rowitem(1) = accid Then
                    Dim str(6) As String
                    Dim itm As ListViewItem
                    str(0) = rowitem.Item(0)
                    str(1) = rowitem.Item(1)
                    str(2) = rowitem.Item(2)
                    str(3) = GetRaceNameById(TryInt(rowitem.Item(3)))
                    str(4) = GetClassNameById(TryInt(rowitem.Item(4)))
                    str(5) = GetGenderNameById(TryInt(rowitem.Item(5)))
                    str(6) = rowitem.Item(6)
                    itm = New ListViewItem(str)
                    characterview.Items.Add(itm)
                    characterview.EnsureVisible(characterview.Items.Count - 1)
                End If
            Next
        Next
        characterview.Update()
        checkchangestatus = True
        acctotal.Text = "(" & accountview.Items.Count.ToString() & " Accounts total)"
        chartotal.Text = "(" & characterview.Items.Count.ToString() & " Characters total)"
    End Sub
    Private Sub Live_View_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub accountview_ColumnClick(sender As Object, e As System.Windows.Forms.ColumnClickEventArgs) Handles accountview.ColumnClick
        If e.Column = cmpFileListViewComparer.SortColumn Then
            If cmpFileListViewComparer.SortOrder = SortOrder.Ascending Then
                cmpFileListViewComparer.SortOrder = SortOrder.Descending
            Else
                cmpFileListViewComparer.SortOrder = SortOrder.Ascending
            End If
        Else
            cmpFileListViewComparer.SortOrder = SortOrder.Ascending
        End If

        cmpFileListViewComparer.SortColumn = e.Column
        accountview.Sort()
    End Sub


    Private Sub accountview_ItemChecked1(sender As Object, e As System.Windows.Forms.ItemCheckedEventArgs) Handles accountview.ItemChecked
        If checkchangestatus = True Then
            If Not accountview.CheckedItems.Count = 0 Then
                characterview.Items.Clear()


                For Each checkedrow As ListViewItem In accountview.CheckedItems
                    Dim accid As String = checkedrow.SubItems(0).Text
                    For Each rowitem As DataRow In modifiedCharTable.Rows
                        If rowitem(1) = accid Then
                            Dim str(6) As String
                            Dim itm As ListViewItem
                            str(0) = rowitem.Item(0)
                            str(1) = rowitem.Item(1)
                            str(2) = rowitem.Item(2)
                            str(3) = GetRaceNameById(TryInt(rowitem.Item(3)))
                            str(4) = GetClassNameById(TryInt(rowitem.Item(4)))
                            str(5) = GetGenderNameById(TryInt(rowitem.Item(5)))
                            str(6) = rowitem.Item(6)
                            itm = New ListViewItem(str)
                            characterview.Items.Add(itm)
                            characterview.EnsureVisible(characterview.Items.Count - 1)
                        End If
                    Next
                Next
                characterview.Update()
            Else
                characterview.Items.Clear()
                For Each listitems As ListViewItem In accountview.Items
                    Dim accid As String = listitems.SubItems(0).Text
                    For Each rowitem As DataRow In modifiedCharTable.Rows
                        If rowitem(1) = accid Then
                            Dim str(6) As String
                            Dim itm As ListViewItem
                            str(0) = rowitem.Item(0)
                            str(1) = rowitem.Item(1)
                            str(2) = rowitem.Item(2)
                            str(3) = GetRaceNameById(TryInt(rowitem.Item(3)))
                            str(4) = GetClassNameById(TryInt(rowitem.Item(4)))
                            str(5) = GetGenderNameById(TryInt(rowitem.Item(5)))
                            str(6) = rowitem.Item(6)
                            itm = New ListViewItem(str)
                            characterview.Items.Add(itm)
                            characterview.EnsureVisible(characterview.Items.Count - 1)
                        End If
                    Next
                Next
                characterview.Update()
            End If
        End If
        acctotal.Text = "(" & accountview.Items.Count.ToString() & " Accounts total)"
        chartotal.Text = "(" & characterview.Items.Count.ToString() & " Characters total)"
    End Sub

    Private Sub accountview_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles accountview.SelectedIndexChanged

    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles checkall_acc.LinkClicked
        For Each xitem As ListViewItem In accountview.Items
            xitem.Checked = True
        Next
    End Sub

    Private Sub uncheckall_acc_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles uncheckall_acc.LinkClicked
        For Each xitem As ListViewItem In accountview.Items
            xitem.Checked = False
        Next
    End Sub



    Private Sub filter_acc_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles filter_acc.LinkClicked
        Filter_accounts.Show()
    End Sub

    Private Sub SelectedAccountsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SelectedAccountsToolStripMenuItem.Click
        Dim result = MsgBox(My.Resources.UserMessages.deleteacc, vbYesNo, My.Resources.UserMessages.areyousure)
        If result = Microsoft.VisualBasic.MsgBoxResult.Yes Then
            Dim accountId As String = accountview.SelectedItems(0).SubItems(0).Text
            For I = 0 To accountview.SelectedItems.Count - 1
                accountview.SelectedItems(I).Remove()
                GlobalVariables.acc_id_columnname = "id" 'todo
                Dim toBeRemovedRow As DataRow() = acctable.Select(GlobalVariables.acc_id_columnname & " = '" & accountId & "'")
                If Not toBeRemovedRow.Length = 0 Then acctable.Rows.Remove(toBeRemovedRow(0))
                runSQLCommand_realm_string_setconn("DELETE FROM `" & account_tablename & "` WHERE " & acc_id_columnname & "='" & accountId & "'", GlobalConnection_Realm)
            Next
            setaccountview(acctable)
        End If
    End Sub

    Private Sub CheckedAccountsToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles CheckedAccountsToolStripMenuItem1.Click
        Dim result = MsgBox(My.Resources.UserMessages.deleteacc, vbYesNo, My.Resources.UserMessages.areyousure)
        If result = Microsoft.VisualBasic.MsgBoxResult.Yes Then
            For Each itm As ListViewItem In accountview.CheckedItems
                accountview.Items.Remove(itm)
                GlobalVariables.acc_id_columnname = "id" 'todo
                Dim toBeRemovedRow As DataRow() = acctable.Select(GlobalVariables.acc_id_columnname & " = '" & itm.SubItems(0).Text & "'")
                If Not toBeRemovedRow.Length = 0 Then acctable.Rows.Remove(toBeRemovedRow(0))
                runSQLCommand_realm_string_setconn("DELETE FROM `" & account_tablename & "` WHERE " & acc_id_columnname & "='" & itm.SubItems(0).Text & "'", GlobalConnection_Realm)
            Next
            setaccountview(acctable)
        End If
    End Sub

    Private Sub EditToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles EditToolStripMenuItem.Click
        'todo
    End Sub
    Private Sub accountview_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles accountview.MouseDown
        If e.Button = MouseButtons.Right Then
            Dim oItem As ListViewItem = accountview.GetItemAt(e.X, e.Y)
            If oItem IsNot Nothing Then
                For I = 0 To accountview.SelectedItems.Count - 1
                    accountcontext.Show(accountview, e.X, e.Y)
                Next
            End If
        End If
    End Sub

    Private Sub filter_char_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles filter_char.LinkClicked
        Filter_characters.Show()
    End Sub

   
End Class
