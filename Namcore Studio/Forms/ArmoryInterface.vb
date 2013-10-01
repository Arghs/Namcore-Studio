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
'*      /Filename:      ArmoryInterface
'*      /Description:   Provides an interface to load characters from WoW Armory
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.Linq
Imports Namcore_Studio.Modules.Interface
Imports NCFramework.Framework.TemplateSystem
Imports NCFramework.Framework.Modules
Imports NCFramework.My
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Extension
Imports NCFramework.Framework.Armory
Imports Namcore_Studio.Forms.Extension
Imports System.Net

Namespace Forms
    Public Class ArmoryInterface
        Inherits EventTrigger

        '// Declaration
        Private WithEvents _mHandler As New ArmoryHandler
        '// Declaration

        Public Structure Data2Thread
            Public CharLst As List(Of String)
        End Structure

        Private Sub addChar_bt_Click(sender As Object, e As EventArgs) Handles addChar_bt.Click
            LogAppend("Adding character", "Armory_interface_addChar_bt_Click", False)
            Dim templink As String =
                    "http://#replaceregion#.battle.net/wow/#replacelang#/character/#replacerealm#/#replacecharacter#/advanced"

            If globalregion.SelectedItem Is Nothing Then
                MsgBox(ResourceHandler.GetUserMessage("regionnotset"), MsgBoxStyle.Critical,
                       ResourceHandler.GetUserMessage("attention"))
            ElseIf realmname.Text = "" Then
                MsgBox(ResourceHandler.GetUserMessage("realmnamenotset"), MsgBoxStyle.Critical,
                       ResourceHandler.GetUserMessage("attention"))
            ElseIf charname.Text = "" Then
                MsgBox(ResourceHandler.GetUserMessage("charnamenotset"), MsgBoxStyle.Critical,
                       ResourceHandler.GetUserMessage("attention"))
            Else
                templink = templink.Replace("#replaceregion#", globalregion.SelectedItem.ToString)
                templink = templink.Replace("#replacerealm#", realmname.Text)
                templink = templink.Replace("#replacecharacter#", charname.Text)
                If MySettings.Default.language = "de" And globalregion.SelectedItem.ToString = "EU" Then
                    templink = templink.Replace("#replacelang#", "de")
                Else
                    If globalregion.SelectedItem.ToString = "KR" Then
                        templink = templink.Replace("#replacelang#", "ko")
                    ElseIf globalregion.SelectedItem.ToString = "TW" Then
                        templink = templink.Replace("#replacelang#", "zh")
                    Else
                        templink = templink.Replace("#replacelang#", "en")
                    End If

                End If
                'Add battle.net maintenance check!!
                Dim client As New WebClient
                client.CheckProxy()
                Dim checkcode As String = Nothing
                Try
                    checkcode = client.DownloadString(templink)
                Catch ex As Exception
                    If ex.ToString.Contains("404") Then
                        LogAppend("Character not found - error 404!", "Armory_interface_addChar_bt_Click", False, True)
                        MsgBox(ResourceHandler.GetUserMessage("charnotfound"), MsgBoxStyle.Critical,
                               ResourceHandler.GetUserMessage("attention"))
                        Exit Sub
                    End If
                End Try
                If checkcode Is Nothing Then
                    LogAppend("Checkcode is nothing - prevent NullReferenceException",
                              "Armory_interface_addChar_bt_Click",
                              False, True)
                    Exit Sub
                End If
                If checkcode.Contains("error=503") Then
                    LogAppend("Character not found - error 503", "Armory_interface_addChar_bt_Click", False, True)
                    MsgBox(ResourceHandler.GetUserMessage("charnotfound"), MsgBoxStyle.Critical,
                           ResourceHandler.GetUserMessage("attention"))
                    Exit Sub
                End If
                Dim str(3) As String
                Dim itm As ListViewItem
                str(0) = globalregion.SelectedItem.ToString.ToUpper()
                str(1) = realmname.Text.ToUpper()
                str(2) = charname.Text.ToUpper()
                str(3) = templink
                itm = New ListViewItem(str)
                char_lst.Items.Add(itm)
                char_lst.EnsureVisible(char_lst.Items.Count - 1)
                char_lst.Update()
                realmname.Text = ""
                charname.Text = ""
                url_tb.Text = ""
                If char_lst.Items.Count > 0 Then
                    load_bt.Enabled = True
                Else
                    load_bt.Enabled = False
                End If

            End If
        End Sub

        Private Sub back_bt_Click(sender As Object, e As EventArgs) Handles back_bt.Click
            LogAppend("Trigger back button click", "Armory_interface_back_bt_Click", False)
            If GlobalVariables.lastregion = "main" Or GlobalVariables.lastregion = "liveview" Then
                Close()
                Main.Show()
            End If
            'todo
        End Sub

        Private Sub addURL_bt_Click(sender As Object, e As EventArgs) Handles addURL_bt.Click
            LogAppend("Adding character via URL", "Armory_interface_addURL_bt_Click", False)
            Dim templink As String = url_tb.Text

            If Not templink.Contains(".battle.net/wow/") Then
                LogAppend("Invalid URL Exception", "Armory_interface_addURL_bt_Click", False, True)
                MsgBox(ResourceHandler.GetUserMessage("invalidurl"), MsgBoxStyle.Critical,
                       ResourceHandler.GetUserMessage("attention"))
            Else
                templink = templink.Replace("/simple", "/advanced")
                If Not templink.StartsWith("http://") Then templink = "http://" & templink
                'Add battle.net maintenance check!!
                Dim client As New WebClient
                client.CheckProxy()
                Dim checkcode As String = Nothing
                Try
                    checkcode = client.DownloadString(templink)
                Catch ex As Exception
                    If ex.ToString.Contains("404") Then
                        LogAppend("Character not found - error 404", "Armory_interface_addURL_bt_Click", False, True)
                        MsgBox(ResourceHandler.GetUserMessage("charnotfound"), MsgBoxStyle.Critical,
                               ResourceHandler.GetUserMessage("attention"))
                        Exit Sub
                    End If
                End Try
                If checkcode Is Nothing Then
                    LogAppend("Checkcode is nothing - prevent NullReferenceException",
                              "Armory_interface_addURL_bt_Click",
                              False, True)
                    Exit Sub
                End If
                If checkcode.Contains("error=503") Then
                    LogAppend("Character not found - error 503", "Armory_interface_addURL_bt_Click", False, True)
                    MsgBox(ResourceHandler.GetUserMessage("charnotfound"), MsgBoxStyle.Critical,
                           ResourceHandler.GetUserMessage("attention"))
                    Exit Sub
                End If
                Dim str(3) As String
                Dim itm As ListViewItem
                str(0) = SplitString(templink.ToLower, "http://".ToLower, ".battle.net".ToLower).ToUpper()
                str(1) = SplitString(templink.ToLower, "/character/".ToLower, "/".ToLower).ToUpper()
                str(2) = SplitString(templink.ToLower, str(1).ToLower & "/", "/advanced".ToLower).ToUpper()
                str(3) = templink
                itm = New ListViewItem(str)
                char_lst.Items.Add(itm)
                char_lst.EnsureVisible(char_lst.Items.Count - 1)
                char_lst.Update()
                realmname.Text = ""
                charname.Text = ""
                url_tb.Text = ""
                If char_lst.Items.Count > 0 Then
                    load_bt.Enabled = True
                Else
                    load_bt.Enabled = False
                End If
            End If
        End Sub

        Private Sub load_bt_Click(sender As Object, e As EventArgs) Handles load_bt.Click
            LogAppend("Trigger load button click", "Armory_interface_load_bt_Click", False)
            GlobalVariables.lastregion = "armoryparser"
            GlobalVariables.globChars.CharacterSets = New List(Of NCFramework.Framework.Modules.Character)
            GlobalVariables.trdrunnuing = True
            MySettings.Default.language = "de" 'todo for testing only
            Dim urllst As List(Of String) =
                    (From lstitm As ListViewItem In char_lst.Items Select lstitm.SubItems(3).Text).ToList()
            LogAppend("Urlcount: " & urllst.Count.ToString(), "Armory_interface_load_bt_Click", False)
            NewProcessStatus()
            _mHandler.LoadArmoryCharacters(urllst)
            Close()
        End Sub

        Private Sub m_handler_CountCompleted(ByVal sender As Object, ByVal e As CompletedEventArgs) _
            Handles _mHandler.Completed
            prepareLive_armory()
        End Sub

        Private Sub char_lst_MouseDown(sender As Object, e As MouseEventArgs) Handles char_lst.MouseDown
            If e.Button = MouseButtons.Right Then
                Dim oItem As ListViewItem = char_lst.GetItemAt(e.X, e.Y)
                If oItem IsNot Nothing Then
                    For I = 0 To char_lst.SelectedItems.Count - 1
                        ContextMenuStrip1.Show(char_lst, e.X, e.Y)
                    Next
                End If
            End If
        End Sub

        Private Sub removeItem_Click(sender As Object, e As EventArgs) Handles removeItem.Click
            For I = 0 To char_lst.SelectedItems.Count - 1
                char_lst.SelectedItems(I).Remove()
            Next
            If char_lst.Items.Count = 0 Then
                load_bt.Enabled = False
            End If
        End Sub

        Private Sub url_tb_KeyDown(sender As Object, e As KeyEventArgs) Handles url_tb.KeyDown
            If e.KeyCode = Keys.Enter And ActiveControl.Name Is "url_tb" Then
                addURL_bt_Click(sender, e)
                e.Handled = True
            End If
        End Sub

        Private Sub Armory_interface_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            LogAppend("Armory_interface loading event", "Armory_interface_Load", False)
            AddHandler highlighter2.Click, AddressOf highlighter2_Click
            Dim controlLst As List(Of Control)
            controlLst = FindAllChildren()
            For Each itemControl As Control In controlLst
                itemControl.SetDoubleBuffered()
            Next
            'todo
            GlobalVariables.offlineExtension = False
            GlobalVariables.globChars = New GlobalCharVars()
        End Sub


        Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
            GlobalVariables.LoadingTemplate = True
            Dim mSerializer As Serializer = New Serializer
            GlobalVariables.globChars = mSerializer.DeSerialize("", New GlobalCharVars)
            prepareLive_armory()
        End Sub

        Private Sub highlighter2_Click(sender As Object, e As EventArgs)
            back_bt.PerformClick()
        End Sub
    End Class
End Namespace