﻿'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
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
'*      /Filename:      AccountOverview
'*      /Description:   Provides an interface to display account information
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NamCore_Studio.Forms.Character
Imports NamCore_Studio.Forms.Extension
Imports NamCore_Studio.Modules.Interface
Imports NCFramework.Framework.Core
Imports NCFramework.Framework.Core.Update
Imports NCFramework.Framework.Database
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Modules
Imports NCFramework.My.Resources

Namespace Forms
    Public Class AccountOverview
        Inherits EventTrigger

        '// Declaration
        Private _currentViewedAccountSet As Account
        Private _currentEditedAccountSet As Account
        Private _initComplete As Boolean = False
        Private _cmpFileListViewComparer As ListViewComparer
        '// Declaration


        Private Sub highlighter2_Click(sender As Object, e As EventArgs)
            Close()
        End Sub

        Private Sub AccountOverview_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            AddHandler highlighter2.Click, AddressOf highlighter2_Click
        End Sub

        Public Sub prepare_interface(accountSet As Account)
            LogAppend("prepare_interface call", "AccountOverview_prepare_interface", False)
            _cmpFileListViewComparer = New ListViewComparer(characterview)
            _initComplete = False
            For Each subctrl As Control In Controls
                subctrl.SetDoubleBuffered()
            Next
            If Not GlobalVariables.templateMode = True Then
                Dim accHandler As New AccountHandler
                accHandler.LoadAccount(accountSet.Id, accountSet.SetIndex, accountSet)
                accountSet = GetAccountSetBySetId(accountSet.SetIndex)
            End If
            characterview.Items.Clear()
            mail_lbl.Text = ""
            joindate_lbl.Text = accountSet.JoinDate.ToString()
            lastip_lbl.Text = accountSet.LastIp
            lastlogin_lbl.Text = accountSet.LastLogin.ToString()
            accname_lbl.Text = accountSet.Name
            expansion_ud.Value = accountSet.Expansion
            mail_lbl.Text = accountSet.Email
            If accountSet.Locked = 1 Then
                lockaccount_cb.Checked = True
            Else
                lockaccount_cb.Checked = False
            End If
            characterview.BeginUpdate()
            For Each player As NCFramework.Framework.Modules.Character In accountSet.Characters
                Dim str(6) As String
                str(0) = player.Guid.ToString()
                str(1) = player.Name
                str(2) = GetRaceNameById(player.Race)
                str(3) = GetClassNameById(player.Cclass)
                str(4) = GetGenderNameById(player.Gender)
                str(5) = player.Level.ToString()
                Dim itm As New ListViewItem(str)
                itm.Tag = player
                characterview.Items.Add(itm)
            Next
            characterview.EndUpdate()
            reset_bt.Enabled = False
            savechanges_bt.Enabled = False
            _currentViewedAccountSet = accountSet
            _currentEditedAccountSet = DeepCloneHelper.DeepClone(_currentViewedAccountSet)
            _initComplete = True
        End Sub

        Private Sub characterview_MouseUp(sender As Object, e As MouseEventArgs) Handles characterview.MouseUp
            changepanel.Location = New Point(4000, 4000)
            If Not changepanel.Tag Is Nothing Then CType(changepanel.Tag, Label).Visible = True
            If e.Button = MouseButtons.Right Then
                If characterview.SelectedItems.Count = 0 And characterview.CheckedItems.Count = 0 Then Exit Sub
                If characterview.SelectedItems.Count = 0 Then
                    SelectedCharacterToolStripMenuItem.Enabled = False
                    CheckedCharactersToolStripMenuItem.Enabled = True
                    EditToolStripMenuItem1.Enabled = False
                Else
                    EditToolStripMenuItem1.Enabled = True
                    SelectedCharacterToolStripMenuItem.Enabled = True
                    CheckedCharactersToolStripMenuItem.Enabled = False
                End If
                If characterview.SelectedItems.Count > 0 Then
                    SelectedCharacterToolStripMenuItem.Enabled = True
                End If
                If characterview.CheckedItems.Count > 0 Then
                    CheckedCharactersToolStripMenuItem.Enabled = True
                End If
                charactercontext.Show(characterview, e.X, e.Y)
            End If
        End Sub

        Private Sub characterview_ColumnClick(sender As Object, e As ColumnClickEventArgs) _
            Handles characterview.ColumnClick
            If e.Column = _cmpFileListViewComparer.SortColumn Then
                If _cmpFileListViewComparer.SortOrder = SortOrder.Ascending Then
                    _cmpFileListViewComparer.SortOrder = SortOrder.Descending
                Else
                    _cmpFileListViewComparer.SortOrder = SortOrder.Ascending
                End If
            Else
                _cmpFileListViewComparer.SortOrder = SortOrder.Ascending
            End If

            _cmpFileListViewComparer.SortColumn = e.Column
            characterview.Sort()
        End Sub

        Private Sub characterview_SelectedIndexChanged(sender As Object, e As EventArgs) _
            Handles characterview.SelectedIndexChanged
        End Sub

        Private Sub exit_bt_Click(sender As Object, e As EventArgs) Handles exit_bt.Click
            Close()
        End Sub

        Private Sub EditToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles EditToolStripMenuItem1.Click
            'Please remember that a character which is loaded from a database needs to be completely stored temporarily
            NewProcessStatus()
            Dim charview = New CharacterOverview
            Dim player = CType(characterview.SelectedItems(0).Tag,
                               NCFramework.Framework.Modules.Character)
            If GlobalVariables.armoryMode = True Then
                Userwait.Show()
                charview.prepare_interface(GetAccountSetBySetId(player.AccountSet), player.SetIndex)
            Else
                'todo load info
                Userwait.Show()
                charview.prepare_interface(GetAccountSetBySetId(player.AccountSet), player.SetIndex)
            End If
        End Sub

        Private Sub SelectedCharacterToolStripMenuItem_Click(sender As Object, e As EventArgs) _
            Handles SelectedCharacterToolStripMenuItem.Click
            Dim result =
                    MsgBox(MSG_DELETECHARACTER & " (" &
                           characterview.SelectedItems(0).SubItems(2).Text & ")", vbYesNo,
                           MSG_AREYOUSURE)
            If result = MsgBoxResult.Yes Then
                Dim charId As String = characterview.SelectedItems(0).SubItems(0).Text
                For I = 0 To characterview.SelectedItems.Count - 1
                    characterview.SelectedItems(I).Remove()
                    Dim toBeRemovedRow As DataRow() =
                            GlobalVariables.chartable.Select(
                                GlobalVariables.sourceStructure.char_guid_col(0) & " = '" & charId & "'")
                    If Not toBeRemovedRow.Length = 0 Then GlobalVariables.chartable.Rows.Remove(toBeRemovedRow(0))
                    runSQLCommand_characters_string_setconn(
                        "DELETE FROM `" & GlobalVariables.sourceStructure.character_tbl(0) & "` WHERE " &
                        GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charId & "'",
                        GlobalVariables.GlobalConnection)
                Next
            End If
        End Sub

        Private Sub CheckedCharactersToolStripMenuItem_Click(sender As Object, e As EventArgs) _
            Handles CheckedCharactersToolStripMenuItem.Click
            Dim result = MsgBox(MSG_DELETECHARACTER, vbYesNo, MSG_AREYOUSURE)
            If result = MsgBoxResult.Yes Then
                For Each itm As ListViewItem In characterview.CheckedItems
                    characterview.Items.Remove(itm)
                    Dim toBeRemovedRow As DataRow() =
                            GlobalVariables.chartable.Select(
                                GlobalVariables.sourceStructure.char_guid_col(0) & " = '" & itm.SubItems(0).Text & "'")
                    If Not toBeRemovedRow.Length = 0 Then GlobalVariables.chartable.Rows.Remove(toBeRemovedRow(0))
                    runSQLCommand_characters_string_setconn(
                        "DELETE FROM `" & GlobalVariables.sourceStructure.character_tbl(0) & "` WHERE " &
                        GlobalVariables.sourceStructure.char_guid_col(0) & "='" & itm.SubItems(0).Text & "'",
                        GlobalVariables.GlobalConnection)
                Next
            End If
        End Sub

        Private Sub UpdateButtons()
            savechanges_bt.Enabled = True
            reset_bt.Enabled = True
        End Sub

        Private Sub TextChangeRequest(sender As Object, e As EventArgs) Handles mail_lbl.Click
            Dim oldSenderLabel = CType(changepanel.Tag, Label)
            If Not oldSenderLabel Is Nothing Then oldSenderLabel.Visible = True
            Dim senderLabel = CType(sender, Label)
            changepanel.Location = senderLabel.Location
            changepanel.Tag = senderLabel
            changeText_tb.Text = ""
            changeText_tb.Text = senderLabel.Text
            senderLabel.Visible = False
        End Sub

        Private Sub updatePic_Click(sender As Object, e As EventArgs) Handles updatePic.Click
            Dim senderLabel = CType(changepanel.Tag, Label)
            Select Case senderLabel.Name
                Case mail_lbl.Name
                    If changeText_tb.Text.Length > 0 AndAlso Not changeText_tb.Text.Contains("@") Then
                        MsgBox(MSG_INVALIDEMAIL, MsgBoxStyle.Critical, MSG_ERROR)
                    Else
                        mail_lbl.Text = changeText_tb.Text
                        _currentEditedAccountSet.Email = changeText_tb.Text
                        changepanel.Location = New Point(4000, 4000)
                        senderLabel.Visible = True
                        UpdateButtons()
                    End If
            End Select
        End Sub

        Private Sub lockaccount_cb_CheckedChanged(sender As Object, e As EventArgs) _
            Handles lockaccount_cb.CheckedChanged
            If _initComplete Then
                changepanel.Location = New Point(4000, 4000)
                If Not changepanel.Tag Is Nothing Then CType(changepanel.Tag, Label).Visible = True
                _currentEditedAccountSet.Locked = CType(lockaccount_cb.Checked, Integer)
                UpdateButtons()
            End If
        End Sub

        Private Sub expansion_ud_ValueChanged(sender As Object, e As EventArgs) Handles expansion_ud.ValueChanged
            If _initComplete Then
                changepanel.Location = New Point(4000, 4000)
                If Not changepanel.Tag Is Nothing Then CType(changepanel.Tag, Label).Visible = True
                _currentEditedAccountSet.Expansion = CType(expansion_ud.Value, Integer)
                UpdateButtons()
            End If
        End Sub

        Private Sub reset_bt_Click(sender As Object, e As EventArgs) Handles reset_bt.Click
            changepanel.Location = New Point(4000, 4000)
            If Not changepanel.Tag Is Nothing Then CType(changepanel.Tag, Label).Visible = True
            prepare_interface(_currentViewedAccountSet)
        End Sub

        Private Sub savechanges_bt_Click(sender As Object, e As EventArgs) Handles savechanges_bt.Click
            changepanel.Location = New Point(4000, 4000)
            If Not changepanel.Tag Is Nothing Then CType(changepanel.Tag, Label).Visible = True
            NewProcessStatus()
            Dim updateHandler As New UpdateAccountHandler
            updateHandler.UpdateAccount(_currentViewedAccountSet, _currentEditedAccountSet)
            LiveView.LiveViewInstance.UpdateAccount(_currentViewedAccountSet)
            reset_bt.Enabled = False
            savechanges_bt.Enabled = False
            LogAppend("Completed account update", "AccountOverview_savechanges_bt_Click", False)
            CloseProcessStatus()
            MsgBox(MSG_ACCOUNTUPDATECOMPLETE, , MSG_INFO)
        End Sub

        Private Sub AccountOverview_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
            changepanel.Location = New Point(4000, 4000)
            If Not changepanel.Tag Is Nothing Then CType(changepanel.Tag, Label).Visible = True
        End Sub
    End Class
End Namespace