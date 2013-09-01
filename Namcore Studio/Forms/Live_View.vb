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
'*                      -List all accounts and characters
'*                      -Editing/Deleting/Transferring accounts and characters
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Imports NCFramework.ConnectionHandler
Imports NCFramework.GlobalVariables
Imports NCFramework.Account_CharacterInformationProcessing
Imports NCFramework.Basics
Imports NCFramework.CommandHandler
Imports NCFramework.Conversions
Imports NCFramework.GlobalCharVars
Imports NCFramework.ResourceHandler
Imports NCFramework
Imports System.Resources

Public Class Live_View
    Private cmpFileListViewComparer As ListViewComparer
    Dim checkchangestatus As Boolean = False
    Dim target_accchar_table As DataTable
    Dim stretching As Boolean = False
    Dim moving As Boolean = False
    Public Structure Data2Thread
        Public lite As Boolean
    End Structure
    Private Sub connect_bt_Click(sender As System.Object, e As System.EventArgs) Handles connect_bt.Click
        con_operator = 1
        DB_connect.Show()
    End Sub
    Public Sub loadaccountsandchars()
        checkchangestatus = False
        sourceCore = "trinity" 'for testing only
        Dim m_acInfoProc As Account_CharacterInformationProcessing = New Account_CharacterInformationProcessing
        acctable = m_acInfoProc.returnAccountTable(GlobalConnection_Realm, sourceStructure)
        chartable = m_acInfoProc.returnCharacterTable(GlobalConnection, sourceStructure)
        modifiedAccTable = acctable.Copy
        modifiedCharTable = chartable.Copy
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
        Dim genSet As Integer = 0
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
            genSet += 1
            itm.Tag = genSet
            characterview.Items.Add(itm)
            characterview.EnsureVisible(characterview.Items.Count - 1)
        Next
        characterview.Sort()
        characterview.Update()
        checkchangestatus = True
        acctotal.Text = "(" & accountview.Items.Count.ToString() & " Accounts total)"
        chartotal.Text = "(" & characterview.Items.Count.ToString() & " Characters total)"
    End Sub

    Public Sub loadInformationSets_Armory()
        Dim controlLST As List(Of Control)
        controlLST = FindAllChildren()
        For Each item_control As Control In controlLST
            item_control.SetDoubleBuffered()
        Next
        connect_bt.Visible = False
        filter_acc.Visible = False
        filter_char.Visible = False
        Dim genGuid As Integer = 1
        checkchangestatus = False
        sourceCore = "armory" 'for testing only       
        ModCharacterSets = globChars.CharacterSets
        ModCharacterSetsIndex = globChars.CharacterSetsIndex
        characterview.Items.Clear()
        accountview.Items.Clear()
        Dim str(4) As String
        Dim itm As ListViewItem
        str(0) = "0"
        str(1) = "Armory"
        str(2) = "-"
        str(3) = "-"
        str(4) = "-"
        itm = New ListViewItem(str)
        accountview.Items.Add(itm)
        accountview.EnsureVisible(accountview.Items.Count - 1)
        accountview.Update()
        For Each player As Character In globChars.CharacterSets
            ' If infoSet = "" Then genGuid += 1 : Continue For // Needs alternative check
            Dim CLstr(6) As String
            Dim CLitm As ListViewItem
            CLstr(0) = genGuid.ToString
            CLstr(1) = "Armory"
            CLstr(2) = player.Name
            CLstr(3) = GetRaceNameById(player.Race)
            CLstr(4) = GetClassNameById(player.Cclass)
            CLstr(5) = GetGenderNameById(player.Gender)
            CLstr(6) = player.Level.ToString
            CLitm = New ListViewItem(CLstr)
            CLitm.Tag = genGuid
            characterview.Items.Add(CLitm)
            characterview.EnsureVisible(characterview.Items.Count - 1)
            'If Not player.SetIndex = genGuid Then Throw New Exception("Player SetId does not match generated SetIndex!")
            genGuid += 1
        Next
        characterview.Sort()
        characterview.Update()
        checkchangestatus = True
        acctotal.Text = "(" & accountview.Items.Count.ToString() & " Accounts total)"
        chartotal.Text = "(" & characterview.Items.Count.ToString() & " Characters total)"
    End Sub
    Public Sub loadtargetaccountsandchars()
        targetCore = "trinity" 'todo for testing only
        Dim m_acInfoProc As Account_CharacterInformationProcessing = New Account_CharacterInformationProcessing
        target_accchar_table = m_acInfoProc.returnTargetAccCharTable(TargetConnection_Realm, targetStructure)
        target_accounts_tree.Nodes.Clear()
        For Each rowitem As DataRow In target_accchar_table.Rows
            Dim foundNode() As TreeNode = target_accounts_tree.Nodes.Find(rowitem(0), False)
            If foundNode.Length = 0 Then
                Dim newnode As New TreeNode
                With newnode
                    .Name = rowitem.Item(0)
                    .Text = rowitem.Item(1)
                End With
                target_accounts_tree.Nodes.Add(newnode)
            Else
                Dim Node As TreeNode = target_accounts_tree.Nodes.Find(rowitem(0), False)(0)
                Dim SubNode As New TreeNode
                With SubNode
                    .Name = rowitem(2)
                    .Text = rowitem(3)
                End With

                Node.Nodes.Add(SubNode)
            End If
        Next
        target_accounts_tree.Update()
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
        Dim controlLST As List(Of Control)
        controlLST = FindAllChildren()
        For Each item_control As Control In controlLST
            item_control.SetDoubleBuffered()
        Next
        createAccountsIndex = New List(Of Integer)
        accountInfo = New List(Of Account)
        charactersToCreate = New List(Of String)
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
        If armoryMode = True Then Exit Sub 'only test
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
                For Each listitem As ListViewItem In characterview.Items
                    listitem.Checked = True
                Next
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
        'Dim RM as New ResourceManager("NCFramework.UserMessages", System.Reflection.Assembly.GetExecutingAssembly())
        Dim result = MsgBox(GetUserMessage("deleteacc") & " (" & accountview.SelectedItems(0).SubItems(1).Text & ")", vbYesNo, GetUserMessage("areyousure"))
        If result = Microsoft.VisualBasic.MsgBoxResult.Yes Then
            Dim accountId As String = accountview.SelectedItems(0).SubItems(0).Text
            For I = 0 To accountview.SelectedItems.Count - 1
                accountview.SelectedItems(I).Remove()
                Dim toBeRemovedRow As DataRow() = acctable.Select(sourceStructure.acc_id_col(0) & " = '" & accountId & "'")
                If Not toBeRemovedRow.Length = 0 Then acctable.Rows.Remove(toBeRemovedRow(0))
                runSQLCommand_realm_string_setconn("DELETE FROM `" & sourceStructure.account_tbl(0) & "` WHERE " & sourceStructure.acc_id_col(0) & "='" & accountId & "'", GlobalConnection_Realm)
                runSQLCommand_characters_string_setconn("DELETE FROM `" & sourceStructure.character_tbl(0) & "` WHERE " & sourceStructure.char_accountId_col(0) & "='" & accountId & "'", GlobalConnection)
            Next
            setaccountview(acctable)
        End If
    End Sub

    Private Sub CheckedAccountsToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles CheckedAccountsToolStripMenuItem1.Click
        'Dim RM as New ResourceManager("NCFramework.UserMessages", System.Reflection.Assembly.GetExecutingAssembly())
        Dim result = MsgBox(GetUserMessage("deleteacc"), vbYesNo, GetUserMessage("areyousure"))
        If result = Microsoft.VisualBasic.MsgBoxResult.Yes Then
            For Each itm As ListViewItem In accountview.CheckedItems
                accountview.Items.Remove(itm)
                Dim toBeRemovedRow As DataRow() = acctable.Select(sourceStructure.acc_id_col(0) & " = '" & itm.SubItems(0).Text & "'")
                If Not toBeRemovedRow.Length = 0 Then acctable.Rows.Remove(toBeRemovedRow(0))
                runSQLCommand_realm_string_setconn("DELETE FROM `" & sourceStructure.account_tbl(0) & "` WHERE " & sourceStructure.acc_id_col(0) & "='" & itm.SubItems(0).Text & "'", GlobalConnection_Realm)
                runSQLCommand_characters_string_setconn("DELETE FROM `" & sourceStructure.character_tbl(0) & "` WHERE " & sourceStructure.char_accountId_col(0) & "='" & itm.SubItems(0).Text & "'", GlobalConnection)
            Next
            setaccountview(acctable)
        End If
    End Sub

    Private Sub EditToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles EditToolStripMenuItem.Click
        'todo
    End Sub
    Private Sub accountview_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles accountview.MouseDown

    End Sub

    Private Sub filter_char_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles filter_char.LinkClicked
        Filter_characters.Show()
    End Sub


    Private Sub getlogin_bt_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub connect_bt_target_Click(sender As System.Object, e As System.EventArgs) Handles connect_bt_target.Click
        con_operator = 2
        DB_connect.Show()
    End Sub

    Private Sub accountcontext_Opening(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles accountcontext.Opening

    End Sub

    Private Sub RemoveToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles RemoveToolStripMenuItem.Click

    End Sub

    Private Sub SelectedCharacterToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SelectedCharacterToolStripMenuItem.Click
        'Dim RM as New ResourceManager("NCFramework.UserMessages", System.Reflection.Assembly.GetExecutingAssembly())
        Dim result = MsgBox(GetUserMessage("deletechar") & " (" & characterview.SelectedItems(0).SubItems(2).Text & ")", vbYesNo, GetUserMessage("areyousure"))
        If result = Microsoft.VisualBasic.MsgBoxResult.Yes Then
            Dim charId As String = characterview.SelectedItems(0).SubItems(0).Text
            For I = 0 To characterview.SelectedItems.Count - 1
                characterview.SelectedItems(I).Remove()
                Dim toBeRemovedRow As DataRow() = chartable.Select(sourceStructure.char_guid_col(0) & " = '" & charId & "'")
                If Not toBeRemovedRow.Length = 0 Then chartable.Rows.Remove(toBeRemovedRow(0))
                runSQLCommand_characters_string_setconn("DELETE FROM `" & sourceStructure.character_tbl(0) & "` WHERE " & sourceStructure.char_guid_col(0) & "='" & charId & "'", GlobalConnection)
            Next
            setaccountview(acctable)
        End If
    End Sub

    Private Sub CheckedCharactersToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CheckedCharactersToolStripMenuItem.Click
        'Dim RM as New ResourceManager("NCFramework.UserMessages", System.Reflection.Assembly.GetExecutingAssembly())
        Dim result = MsgBox(GetUserMessage("deletechar"), vbYesNo, GetUserMessage("areyousure"))
        If result = Microsoft.VisualBasic.MsgBoxResult.Yes Then
            For Each itm As ListViewItem In characterview.CheckedItems
                characterview.Items.Remove(itm)
                Dim toBeRemovedRow As DataRow() = chartable.Select(sourceStructure.char_guid_col(0) & " = '" & itm.SubItems(0).Text & "'")
                If Not toBeRemovedRow.Length = 0 Then chartable.Rows.Remove(toBeRemovedRow(0))
                runSQLCommand_characters_string_setconn("DELETE FROM `" & sourceStructure.character_tbl(0) & "` WHERE " & sourceStructure.char_guid_col(0) & "='" & itm.SubItems(0).Text & "'", GlobalConnection)
            Next
            setaccountview(acctable)
        End If
    End Sub

    Private Sub targetacccontext_Opening(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles targetacccontext.Opening

    End Sub

    Private Sub characterview_DragDrop(sender As Object, e As DragEventArgs) Handles characterview.DragDrop

    End Sub

    Private Sub characterview_ItemDrag(sender As Object, e As ItemDragEventArgs) Handles characterview.ItemDrag
        characterview.DoDragDrop(characterview.SelectedItems, DragDropEffects.Move)
    End Sub

    Private Sub characterview_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles characterview.MouseDown

    End Sub

    Private Sub characterview_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles characterview.MouseUp
        If e.Button = MouseButtons.Right Then
            If characterview.SelectedItems.Count = 0 And characterview.CheckedItems.Count = 0 Then Exit Sub
            If characterview.SelectedItems.Count = 0 Then
                SelectedCharacterToolStripMenuItem.Enabled = False
                SelectedCharacterToolStripMenuItem1.Enabled = False
                CheckedCharactersToolStripMenuItem.Enabled = True
                CheckedCharactersToolStripMenuItem1.Enabled = True
                EditToolStripMenuItem1.Enabled = False
            Else
                EditToolStripMenuItem1.Enabled = True
                SelectedCharacterToolStripMenuItem.Enabled = True
                SelectedCharacterToolStripMenuItem1.Enabled = True
                CheckedCharactersToolStripMenuItem.Enabled = False
                CheckedCharactersToolStripMenuItem1.Enabled = False
            End If
            If characterview.SelectedItems.Count > 0 Then
                SelectedCharacterToolStripMenuItem.Enabled = True
                SelectedCharacterToolStripMenuItem1.Enabled = True
            End If
            If characterview.CheckedItems.Count > 0 Then
                CheckedCharactersToolStripMenuItem.Enabled = True
                CheckedCharactersToolStripMenuItem1.Enabled = True
            End If
            If Not TargetConnection.State = ConnectionState.Open Then
                CheckedCharactersToolStripMenuItem1.Enabled = False
                SelectedCharacterToolStripMenuItem1.Enabled = False
            End If
            charactercontext.Show(characterview, e.X, e.Y)
        End If
    End Sub

    Private Sub characterview_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles characterview.SelectedIndexChanged

    End Sub

    Private Sub target_accounts_tree_AfterSelect(sender As System.Object, e As System.Windows.Forms.TreeViewEventArgs) Handles target_accounts_tree.AfterSelect

    End Sub

    Private Sub target_accounts_tree_DragDrop(sender As Object, e As DragEventArgs) Handles target_accounts_tree.DragDrop
        If e.Data.GetDataPresent(GetType(ListView.SelectedListViewItemCollection).ToString(), False) Then
            Dim loc As Point = (CType(sender, TreeView)).PointToClient(New Point(e.X, e.Y))
            Dim destNode As TreeNode = (CType(sender, TreeView)).GetNodeAt(loc)
            Dim tnNew As TreeNode

            Dim lstViewColl As ListView.SelectedListViewItemCollection = CType(e.Data.GetData(GetType(ListView.SelectedListViewItemCollection)), ListView.SelectedListViewItemCollection)
            For Each lvItem As ListViewItem In lstViewColl
                tnNew = New TreeNode(lvItem.Text)
                tnNew.Tag = lvItem.Tag
                If Not destNode Is Nothing Then
                    destNode.Nodes.Insert(destNode.Index + 1, tnNew)
                    destNode.Expand()
                    Dim tempAccList As New ArrayList
                    Dim tmpAccount(2) As String
                    tmpAccount(1) = destNode.Text
                    tempAccList.Add(tmpAccount)
                    Dim newchar As New Character(lvItem.SubItems(2).Text, TryInt(lvItem.SubItems(0).Text))
                    Dim nodes As New List(Of String)
                    For Each parentNode As TreeNode In target_accounts_tree.Nodes
                        nodes.AddRange(GetChildren(parentNode))
                    Next
                    With tnNew
                        .Text = newchar.Name
                        If Not nodes.Contains("'" & newchar.name & "'") Then
                            .BackColor = Color.Green
                        Else
                            .BackColor = Color.Yellow
                        End If
                    End With
                    charactersToCreate.Add("{AccountId}" & destNode.Name & "{/AccountId}{setId}" & lvItem.Tag.ToString & "{/setId}")

                    ' Remove this line if you want to only copy items
                    ' from ListView and not move them
                    lvItem.Remove()

                End If

            Next lvItem
        End If
    End Sub

    Private Sub target_accounts_tree_DragEnter(sender As Object, e As DragEventArgs) Handles target_accounts_tree.DragEnter
        If e.Data.GetDataPresent(GetType(ListView.SelectedListViewItemCollection)) Then
            e.Effect = DragDropEffects.Move
        End If
    End Sub
    Private Sub characterview_GiveFeedback(ByVal sender As Object, ByVal e As GiveFeedbackEventArgs) Handles characterview.GiveFeedback
        e.UseDefaultCursors = False
        If (e.Effect And DragDropEffects.Move) = DragDropEffects.Move Then
            Cursor.Current = Cursors.Cross
        Else
            Cursor.Current = Cursors.Default
        End If
    End Sub
    Private Sub target_accounts_tree_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles target_accounts_tree.MouseDown

    End Sub

    Private Sub RemoveToolStripMenuItem2_Click(sender As System.Object, e As System.EventArgs) Handles RemoveToolStripMenuItem2.Click
        'Dim RM as New ResourceManager("NCFramework.UserMessages", System.Reflection.Assembly.GetExecutingAssembly())
        Dim result = MsgBox(GetUserMessage("deleteacc") & " (" & target_accounts_tree.SelectedNode.Text & ")", vbYesNo, GetUserMessage("areyousure"))
        If result = Microsoft.VisualBasic.MsgBoxResult.Yes Then
            Dim accountId As String = target_accounts_tree.SelectedNode.Name
            If Not target_accounts_tree.SelectedNode.BackColor = Color.Transparent Then
                target_accounts_tree.SelectedNode.Remove()
                Exit Sub
            End If
            Dim toBeRemovedRow As DataRow() = target_accchar_table.Select(targetStructure.acc_id_col(0) & " = '" & accountId & "'")
            If Not toBeRemovedRow.Length = 0 Then target_accchar_table.Rows.Remove(toBeRemovedRow(0))
            target_accounts_tree.SelectedNode.Remove()
            runSQLCommand_realm_string_setconn("DELETE FROM `" & targetStructure.account_tbl(0) & "` WHERE " & targetStructure.acc_id_col(0) & "='" & accountId & "'", TargetConnection_Realm)
            runSQLCommand_characters_string_setconn("DELETE FROM `" & targetStructure.character_tbl(0) & "` WHERE " & targetStructure.char_accountId_col(0) & "='" & accountId & "'", TargetConnection)

        End If
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripMenuItem1.Click
        'Dim RM as New ResourceManager("NCFramework.UserMessages", System.Reflection.Assembly.GetExecutingAssembly())
        Dim result = MsgBox(GetUserMessage("deleteacc") & " (" & target_accounts_tree.SelectedNode.Text & ")", vbYesNo, GetUserMessage("areyousure"))
        If result = Microsoft.VisualBasic.MsgBoxResult.Yes Then
            Dim accountId As String = target_accounts_tree.SelectedNode.Name
            If Not target_accounts_tree.SelectedNode.BackColor = Color.Transparent Then
                target_accounts_tree.SelectedNode.Remove()
                Exit Sub
            End If
            target_accounts_tree.SelectedNode.Remove()
            Dim toBeRemovedRow As DataRow() = target_accchar_table.Select(targetStructure.acc_id_col(0) & " = '" & accountId & "'")
            If Not toBeRemovedRow.Length = 0 Then target_accchar_table.Rows.Remove(toBeRemovedRow(0))
            runSQLCommand_characters_string_setconn("DELETE FROM `" & targetStructure.character_tbl(0) & "` WHERE " & targetStructure.char_accountId_col(0) & "='" & accountId & "'", TargetConnection)
        End If
    End Sub

    Private Sub SelectedAccountToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SelectedAccountToolStripMenuItem.Click
        Dim temparray As New ArrayList
        Dim acc(2) As String
        acc(0) = accountview.SelectedItems(0).SubItems(0).Text
        acc(1) = accountview.SelectedItems(0).SubItems(1).Text
        temparray.Add(acc)
        For Each checkeditem As ListViewItem In accountview.CheckedItems
            checkeditem.Checked = False
        Next
        For Each checkeditem As ListViewItem In characterview.CheckedItems
            checkeditem.Checked = False
        Next
        accountview.SelectedItems(0).Checked = True
        transAccounts(temparray)
    End Sub
    Private Sub CheckedAccountsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CheckedAccountsToolStripMenuItem.Click
        Dim temparray As New ArrayList

        For Each CheckedAccount As ListViewItem In accountview.CheckedItems
            Dim acc(2) As String
            acc(0) = CheckedAccount.SubItems(0).Text
            acc(1) = CheckedAccount.SubItems(1).Text
            temparray.Add(acc)
        Next
        transAccounts(temparray)

    End Sub
    Public Sub transAccounts(ByVal accounts As ArrayList)
        Dim needtocreate As Boolean
        For Each NAccount() As String In accounts
            needtocreate = True
            For Each accountnode As TreeNode In target_accounts_tree.Nodes
                If accountnode.Text.ToLower() = NAccount(1).ToString().ToLower() Then
                    needtocreate = False
                End If
            Next
            If needtocreate = True Then
                Dim newaccnode As New TreeNode
                Dim newacc As New Account(NAccount(1).ToString(), TryInt(NAccount(0).ToString()))
                Dim nodes As New List(Of String)
                With newaccnode
                    .Text = newacc.name
                    .Tag = newacc
                    .BackColor = Color.Green
                End With
                target_accounts_tree.Nodes.Add(newaccnode)
                accountInfo.Add(newacc)
                createAccountsIndex.Add(accountInfo.Count - 1)
            End If

            For Each CheckedChar As ListViewItem In characterview.CheckedItems
                Dim newchar As New Character(CheckedChar.SubItems(2).Text, TryInt(CheckedChar.SubItems(0).Text))
                If CheckedChar.SubItems(1).Text = NAccount(0).ToString() Then
                    For Each targetaccount As TreeNode In target_accounts_tree.Nodes
                        If targetaccount.Text = NAccount(1).ToString() Then
                            Dim newcharnode As New TreeNode
                            Dim nodes As New List(Of String)
                            For Each parentNode As TreeNode In target_accounts_tree.Nodes
                                nodes.AddRange(GetChildren(parentNode))
                            Next
                            With newcharnode
                                .Text = newchar.name
                                .Tag = newchar
                                If Not nodes.Contains("'" & newchar.name & "'") Then
                                    .BackColor = Color.Green
                                Else
                                    .BackColor = Color.Yellow
                                End If
                            End With
                            targetaccount.Nodes.Add(newcharnode)
                            charactersToCreate.Add("{AccountId}" & targetaccount.Name & "{/AccountId}{setId}" & CheckedChar.Tag.ToString() & "{/setId}")
                            '  targetaccount.Tag.transcharlist.Add(newchar)
                        End If
                    Next

                End If
            Next
        Next
        Transfer_bt.Enabled = True
    End Sub
    Public Sub transChars_specificacc(ByVal accounts As ArrayList)
        For Each character() As String In trans_charlist
            For Each accountnode As TreeNode In target_accounts_tree.Nodes
                For Each account() As String In accounts
                    If account(1).ToLower() = accountnode.Text.ToLower() Then
                        Dim newcharnode As New TreeNode
                        Dim newchar As New Character(character(1), TryInt(character(0)))
                        Dim nodes As New List(Of String)
                        For Each parentNode As TreeNode In target_accounts_tree.Nodes
                            nodes.AddRange(GetChildren(parentNode))
                        Next
                        With newcharnode
                            .Text = newchar.name
                            .Tag = newchar
                            If Not nodes.Contains("'" & newchar.name & "'") Then
                                .BackColor = Color.Green
                            Else
                                .BackColor = Color.Yellow
                            End If
                        End With
                        accountnode.Nodes.Add(newcharnode)
                        charactersToCreate.Add("{AccountId}" & accountnode.Name & "{/AccountId}{setId}" & character(2) & "{/setId}")

                    End If
                Next
            Next
        Next
        Transfer_bt.Enabled = True
    End Sub
    Public Sub transChars_allacc()
        For Each character() As String In trans_charlist
            For Each accountnode As TreeNode In target_accounts_tree.Nodes
                Dim newcharnode As New TreeNode
                Dim newchar As New Character(character(1), TryInt(character(0)))
                Dim nodes As New List(Of String)
                For Each parentNode As TreeNode In target_accounts_tree.Nodes
                    nodes.AddRange(GetChildren(parentNode))
                Next
                With newcharnode
                    .Text = newchar.name
                    .Tag = newchar
                    If Not nodes.Contains("'" & newchar.name & "'") Then
                        .BackColor = Color.Green
                    Else
                        .BackColor = Color.Yellow
                    End If
                End With
                accountnode.Nodes.Add(newcharnode)

            Next
        Next
        Transfer_bt.Enabled = True
    End Sub
    Function GetChildren(parentNode As TreeNode) As List(Of String)
        Dim nodes As List(Of String) = New List(Of String)
        For Each childNode As TreeNode In parentNode.Nodes
            nodes.Add("'" & childNode.Text & "'")
        Next
        Return nodes
    End Function



    Private Sub Transfer_bt_Click(sender As System.Object, e As System.EventArgs) Handles Transfer_bt.Click

        Try
            For Each CurrentForm As Form In Application.OpenForms
                If CurrentForm.Name = "Process_status" Then
                    Dim stat As Process_status = DirectCast(CurrentForm, Process_status)
                    stat.Close()
                End If
            Next
        Catch ex As Exception

        End Try

        procStatus = New Process_status
        procStatus.Show()

        If armoryMode Then
            Dim d As New Data2Thread() With {.lite = True}


        Else

        End If

    End Sub

    Private Sub SelectedCharacterToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles SelectedCharacterToolStripMenuItem1.Click
        trans_charlist = New ArrayList()
        Dim charId As String = characterview.SelectedItems(0).SubItems(0).Text
        For I = 0 To characterview.SelectedItems.Count - 1
            Dim character(2) As String
            character(0) = charId
            character(1) = characterview.SelectedItems(0).SubItems(2).Text
            character(2) = characterview.SelectedItems(0).Tag.ToString
            trans_charlist.Add(character)
        Next
        Prep_chartrans.Show()
    End Sub

    Private Sub CheckedCharactersToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles CheckedCharactersToolStripMenuItem1.Click
        trans_charlist = New ArrayList()
        For Each itm As ListViewItem In characterview.CheckedItems
            Dim character(2) As String
            character(0) = itm.SubItems(0).Text
            character(1) = itm.SubItems(2).Text
            character(2) = itm.Tag.ToString
            trans_charlist.Add(character)
        Next
        Prep_chartrans.Show()
    End Sub

    Private Sub charactercontext_Opening(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles charactercontext.Opening

    End Sub

    Private Sub checkall_char_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles checkall_char.LinkClicked
        For Each xitem As ListViewItem In characterview.Items
            xitem.Checked = True
        Next
    End Sub

    Private Sub uncheckall_char_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles uncheckall_char.LinkClicked
        For Each xitem As ListViewItem In characterview.Items
            xitem.Checked = False
        Next
    End Sub


    Private Sub accountview_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles accountview.MouseUp
        If e.Button = MouseButtons.Right Then
            If accountview.SelectedItems.Count = 0 And accountview.CheckedItems.Count = 0 Then Exit Sub
            If accountview.SelectedItems.Count = 0 Then
                SelectedAccountToolStripMenuItem.Enabled = False
                SelectedAccountsToolStripMenuItem.Enabled = False
                CheckedAccountsToolStripMenuItem.Enabled = True
                CheckedAccountsToolStripMenuItem1.Enabled = True
                EditToolStripMenuItem1.Enabled = False
            Else
                EditToolStripMenuItem1.Enabled = True
                SelectedAccountToolStripMenuItem.Enabled = True
                SelectedAccountsToolStripMenuItem.Enabled = True
                CheckedAccountsToolStripMenuItem.Enabled = False
                CheckedAccountsToolStripMenuItem1.Enabled = False
            End If
            If accountview.SelectedItems.Count > 0 And accountview.CheckedItems.Count > 0 Then
                CheckedAccountsToolStripMenuItem.Enabled = True
                CheckedAccountsToolStripMenuItem1.Enabled = True
                SelectedAccountToolStripMenuItem.Enabled = True
                SelectedAccountsToolStripMenuItem.Enabled = True
            End If
            If Not TargetConnection.State = ConnectionState.Open Then
                SelectedAccountToolStripMenuItem.Enabled = False
                CheckedAccountsToolStripMenuItem.Enabled = False
            End If
            accountcontext.Show(accountview, e.X, e.Y)
        End If
    End Sub

    Private Sub target_accounts_tree_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles target_accounts_tree.MouseUp
        If e.Button = MouseButtons.Right Then
            Dim oItem As TreeNode = target_accounts_tree.GetNodeAt(e.X, e.Y)
            If oItem IsNot Nothing Then
                If oItem.Level = 0 Then
                    targetacccontext.Show(target_accounts_tree, e.X, e.Y)
                Else
                    targetcharcontext.Show(target_accounts_tree, e.X, e.Y)
                End If

            End If
        End If
    End Sub


    Private Sub EditToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles EditToolStripMenuItem1.Click
        Dim charview As New CharacterOverview
        Dim setId As Integer = characterview.SelectedItems(0).Tag
        If armoryMode = True Then
            Userwait.Show()
            charview.prepare_interface(setId)
            Userwait.Close()
            charview.Show()
        Else
            'todo load info

            Userwait.Show()
            charview.prepare_interface(setId)
            Userwait.Close()
            charview.Show()
        End If

    End Sub

    Private Sub RemoveToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles RemoveToolStripMenuItem1.Click

    End Sub

    Private Sub characterview_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles characterview.MouseDoubleClick
        If Not characterview.SelectedItems.Count = 0 Then
            Dim charview As New CharacterOverview
            Dim setId As Integer = characterview.SelectedItems(0).Tag
            If armoryMode = True Then
                Userwait.Show()
                charview.prepare_interface(setId)
                Userwait.Close()
                charview.Show()
            Else
                'todo load info

                Userwait.Show()
                charview.prepare_interface(setId)
                Userwait.Close()
                charview.Show()
            End If
        End If
    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub



    Private Sub back_bt_Click(sender As Object, e As EventArgs) Handles back_bt.Click
        lastregion = "liveview"
        Me.Close()
        Main.Show()
        'todo
    End Sub
    Private ptMouseDownLocation As Point
    Private Sub me_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        If Cursor = Cursors.SizeWE Then
            If e.Button = Windows.Forms.MouseButtons.Left Then
                ptMouseDownLocation = e.Location
            End If
        Else
            If e.Button = Windows.Forms.MouseButtons.Left Then
                ptMouseDownLocation = e.Location
            End If
        End If

    End Sub



    Private Sub me_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        If Not stretching And Not moving Then
            If e.X > Width - 10 And e.X < Width + 10 Then
                If Cursor = Cursors.Default Then
                    Cursor = Cursors.SizeWE
                End If
            Else
                If Cursor = Cursors.SizeWE Then
                    Cursor = Cursors.Default
                End If
            End If
        End If
        If Cursor = Cursors.SizeWE Then
            If e.Button = Windows.Forms.MouseButtons.Left Then
                stretching = True
                Size = New System.Drawing.Size(e.Location.X, Size.Height)
                Application.DoEvents()
                mainpanel.Size = New System.Drawing.Size(Size.Width - 27, mainpanel.Size.Height)
                Dim tmpwidth As Integer = (Size.Width / 1920) * 8
                header.Location = New System.Drawing.Point(tmpwidth, header.Location.Y)
                header.Size = New System.Drawing.Size(Size.Width - (2 * tmpwidth), header.Size.Height)
                closepanel.Location = New System.Drawing.Point(header.Size.Width - 68, closepanel.Location.Y)
                Application.DoEvents()

            End If
        Else
            If e.Button = Windows.Forms.MouseButtons.Left Then
                moving = True
                Me.Location = e.Location - ptMouseDownLocation + Location
            End If
        End If

    End Sub
    Private Sub highlighter_MouseEnter(sender As Object, e As EventArgs) Handles highlighter1.MouseEnter, highlighter2.MouseEnter
        sender.backgroundimage = My.Resources.highlight
    End Sub

    Private Sub highlighter_MouseLeave(sender As Object, e As EventArgs) Handles highlighter1.MouseLeave, highlighter2.MouseLeave
        sender.backgroundimage = Nothing
    End Sub
    Private Sub highlighter1_Click(sender As Object, e As EventArgs) Handles highlighter1.Click
        WindowState = FormWindowState.Minimized
    End Sub

    Private Sub highlighter2_Click(sender As Object, e As EventArgs) Handles highlighter2.Click
        back_bt.PerformClick()
    End Sub

    Private Sub Live_View_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        stretching = False
        moving = False
    End Sub

    Private Sub header_MouseDown(sender As Object, e As MouseEventArgs) Handles header.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            ptMouseDownLocation = e.Location
        End If
    End Sub


    Private Sub header_MouseMove(sender As Object, e As MouseEventArgs) Handles header.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            moving = True
            Me.Location = e.Location - ptMouseDownLocation + Location
        End If
    End Sub

    Private Sub header_MouseUp(sender As Object, e As MouseEventArgs) Handles header.MouseUp
        moving = False
    End Sub


    Private Sub header_Paint(sender As Object, e As PaintEventArgs) Handles header.Paint

    End Sub
End Class
