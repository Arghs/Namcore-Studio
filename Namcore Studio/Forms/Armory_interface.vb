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
'*      /Filename:      Armory_interface
'*      /Description:   Provides an interface to load characters from WoW Armory
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Imports System.Resources
Imports System.Net
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Linq
Imports Namcore_Studio_Framework.Basics
Imports Namcore_Studio_Framework.GlobalVariables
Imports Namcore_Studio_Framework.GlobalCharVars
Imports Namcore_Studio_Framework.Serializer
Imports System.Threading
Imports Namcore_Studio_Framework
Imports System.Text
Imports Namcore_Studio_Framework.WebConnection
Imports Namcore_Studio_Framework.WebClientProxyExtension


Public Class Armory_interface
    Public Structure Data2Thread
        Public charLST As List(Of String)
    End Structure
    Private Sub addChar_bt_Click(sender As System.Object, e As System.EventArgs) Handles addChar_bt.Click
        Dim templink As String = "http://#replaceregion#.battle.net/wow/#replacelang#/character/#replacerealm#/#replacecharacter#/advanced"
        Dim RM As New ResourceManager("Namcore_Studio_Framework.UserMessages", System.Reflection.Assembly.GetExecutingAssembly())
        If globalregion.SelectedItem Is Nothing Then
            MsgBox(RM.GetString("regionnotset"), MsgBoxStyle.Critical, RM.GetString("attention"))
        ElseIf realmname.Text = "" Then
            MsgBox(RM.GetString("realmnamenotset"), MsgBoxStyle.Critical, RM.GetString("attention"))
        ElseIf charname.Text = "" Then
            MsgBox(RM.GetString("charnamenotset"), MsgBoxStyle.Critical, RM.GetString("attention"))
        Else
            templink = templink.Replace("#replaceregion#", globalregion.SelectedItem.ToString)
            templink = templink.Replace("#replacerealm#", realmname.Text)
            templink = templink.Replace("#replacecharacter#", charname.Text)
            If Namcore_Studio_Framework.My.MySettings.Default.language = "de" And globalregion.SelectedItem.ToString = "EU" Then
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
                    MsgBox(RM.GetString("charnotfound"), MsgBoxStyle.Critical, RM.GetString("attention"))
                    Exit Sub
                End If
            End Try

            If checkcode.Contains("error=503") Then
                MsgBox(RM.GetString("charnotfound"), MsgBoxStyle.Critical, RM.GetString("attention"))
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

    Private Sub back_bt_Click(sender As System.Object, e As System.EventArgs) Handles back_bt.Click
        If lastregion = "main" Then
            Me.Close()
            Main.Show()
        End If
        'todo
    End Sub

    Private Sub addURL_bt_Click(sender As System.Object, e As System.EventArgs) Handles addURL_bt.Click
        Dim templink As String = url_tb.Text
        Dim RM As New ResourceManager("Namcore_Studio_Framework.UserMessages", System.Reflection.Assembly.GetExecutingAssembly())
        If Not templink.Contains(".battle.net/wow/") Then
            MsgBox(RM.GetString("invalidurl"), MsgBoxStyle.Critical, RM.GetString("attention"))
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
                    MsgBox(RM.GetString("charnotfound"), MsgBoxStyle.Critical, RM.GetString("attention"))
                    Exit Sub
                End If
            End Try
            If checkcode Is Nothing Then
                Exit Sub
            End If
            If checkcode.Contains("error=503") Then
                MsgBox(RM.GetString("charnotfound"), MsgBoxStyle.Critical, RM.GetString("attention"))
                Exit Sub
            End If
            Dim str(3) As String
            Dim itm As ListViewItem
            str(0) = splitString(templink, "http://", ".battle.net").ToUpper()
            str(1) = splitString(templink, "/character/", "/").ToUpper()
            str(2) = splitString(templink, str(1).ToLower & "/", "/advanced").ToUpper()
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
    Private WithEvents m_handler As New ArmoryHandler
    Private Sub load_bt_Click(sender As System.Object, e As System.EventArgs) Handles load_bt.Click
        lastregion = "armoryparser"
        globChars.CharacterSets = New List(Of Character)
        trdrunnuing = True
        Namcore_Studio_Framework.My.MySettings.Default.language = "de" 'todo for testing only
        Dim urllst As List(Of String) = (From lstitm As ListViewItem In char_lst.Items Select lstitm.SubItems(3).Text).ToList()
        m_handler.LoadArmoryCharacters(urllst)
        Me.Close()
    End Sub
    Private Sub m_handler_CountCompleted(ByVal sender As Object, ByVal e As CompletedEventArgs) Handles m_handler.Completed
        prepareLive_armory()
    End Sub
    Private Sub char_lst_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles char_lst.MouseDown
        If e.Button = MouseButtons.Right Then
            Dim oItem As ListViewItem = char_lst.GetItemAt(e.X, e.Y)
            If oItem IsNot Nothing Then
                For I = 0 To char_lst.SelectedItems.Count - 1
                    ContextMenuStrip1.Show(char_lst, e.X, e.Y)
                Next
            End If
        End If
    End Sub
    Private Sub removeItem_Click(sender As System.Object, e As System.EventArgs) Handles removeItem.Click
        For I = 0 To char_lst.SelectedItems.Count - 1
            char_lst.SelectedItems(I).Remove()
        Next
        If char_lst.Items.Count = 0 Then
            load_bt.Enabled = False
        End If
    End Sub

    Private Sub url_tb_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles url_tb.KeyDown
        If e.KeyCode = Keys.Enter And Me.ActiveControl.Name Is "url_tb" Then
            addURL_bt_Click(sender, e)
            e.Handled = True
        End If
    End Sub


    Private Sub Armory_interface_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim controlLST As List(Of Control)
        controlLST = FindAllChildren()
        For Each item_control As Control In controlLST
            item_control.SetDoubleBuffered()
        Next
        'todo
        offlineExtension = False
        globChars = New GlobalCharVars()



    End Sub
    Private ptMouseDownLocation As Point
    Private Sub me_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            ptMouseDownLocation = e.Location
        End If
    End Sub

    Private Sub me_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me.Location = e.Location - ptMouseDownLocation + Location
        End If
    End Sub
    Private Sub highlighter_MouseEnter(sender As Object, e As EventArgs) Handles highlighter1.MouseEnter, highlighter2.MouseEnter
        sender.backgroundimage = My.Resources.highlight
    End Sub

    Private Sub highlighter_MouseLeave(sender As Object, e As EventArgs) Handles highlighter1.MouseLeave, highlighter2.MouseLeave
        sender.backgroundimage = Nothing
    End Sub




    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Visual Basic .NET
        ' Gets a reference to the same assembly that 
        ' contains the type that is creating the ResourceManager.
        Dim myAssembly As System.Reflection.Assembly
        myAssembly = Me.GetType.Assembly

        ' Gets a reference to a different assembly.
        '  Dim myOtherAssembly As System.Reflection.Assembly
        '    myOtherAssembly = System.Reflection.Assembly.Load("UserMessages")

        ' Creates the ResourceManager.
        Dim myManager As New System.Resources.ResourceManager("Namcore_Studio_Framework.UserMessages", System.Reflection.Assembly.GetExecutingAssembly())

        Dim myString As System.String
        myString = myManager.GetString("human")
        Dim dstr As String = ""
        Dim m_serializer As Serializer = New Serializer
        globChars = m_serializer.DeSerialize(dstr, New GlobalCharVars)
        'temporaryCharacterInformation.Add(RichTextBox1.Text)
        'Dim prepLive As New Interface_Operator
        prepareLive_armory()
    End Sub

    Private Sub highlighter1_Click(sender As Object, e As EventArgs) Handles highlighter1.Click
        WindowState = FormWindowState.Minimized
    End Sub

    Private Sub highlighter2_Click(sender As Object, e As EventArgs) Handles highlighter2.Click
        back_bt.PerformClick()
    End Sub

    Private Sub header_MouseDown(sender As Object, e As MouseEventArgs) Handles header.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            ptMouseDownLocation = e.Location
        End If
    End Sub


    Private Sub header_MouseMove(sender As Object, e As MouseEventArgs) Handles header.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me.Location = e.Location - ptMouseDownLocation + Location
        End If
    End Sub

   
   
    
    Private Sub header_Paint(sender As Object, e As PaintEventArgs) Handles header.Paint

    End Sub
End Class