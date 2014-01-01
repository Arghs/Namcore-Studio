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
'*      /Filename:      AccountOverview
'*      /Description:   Provides an interface to display account information
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.Core
Imports NamCore_Studio.Forms.Character
Imports NCFramework.Framework.Modules
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Database
Imports NamCore_Studio.Modules.Interface
Imports NamCore_Studio.Forms.Extension

Namespace Forms
    Public Class AccountOverview
        Inherits EventTrigger

        Private Sub highlighter2_Click(sender As Object, e As EventArgs)
            Close()
        End Sub

        Private Sub AccountOverview_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            AddHandler highlighter2.Click, AddressOf highlighter2_Click
        End Sub

        Public Sub prepare_interface(ByVal accountSet As Account)
            For Each subctrl As Control In Controls
                subctrl.SetDoubleBuffered()
            Next
            If Not GlobalVariables.templateMode = True Then
                Dim accHandler As New AccountHandler
                accHandler.LoadAccount(accountSet.Id, accountSet.SetIndex, accountSet)
                accountSet = GetAccountSetBySetId(accountSet.SetIndex)
            End If
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
        End Sub

        Private Sub characterview_MouseUp(sender As Object, e As MouseEventArgs) Handles characterview.MouseUp
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

        Private Sub characterview_SelectedIndexChanged(sender As Object, e As EventArgs) _
            Handles characterview.SelectedIndexChanged
        End Sub

        Private Sub exit_bt_Click(sender As Object, e As EventArgs) Handles exit_bt.Click
            Close()
        End Sub

        Private Sub EditToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles EditToolStripMenuItem1.Click
            'Please remember that a character which is loaded from a database needs to be completely stored temporarily
            NewProcessStatus()
            Dim charview As CharacterOverview = New CharacterOverview
            Dim player As NCFramework.Framework.Modules.Character = characterview.SelectedItems(0).Tag
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
                    MsgBox(
                        ResourceHandler.GetUserMessage("deletechar") & " (" &
                        characterview.SelectedItems(0).SubItems(2).Text & ")", vbYesNo,
                        ResourceHandler.GetUserMessage("areyousure"))
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

            Dim result = MsgBox(ResourceHandler.GetUserMessage("deletechar"), vbYesNo,
                                ResourceHandler.GetUserMessage("areyousure"))
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
    End Class
End Namespace