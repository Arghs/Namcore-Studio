Imports System.Resources
Imports System.Net
Imports Namcore_Studio.Basics
Imports Namcore_Studio.ArmoryHandler
Imports Namcore_Studio.GlobalVariables
Imports System.Threading
Imports Namcore_Studio.Interface_Operator
Imports System.Text
Imports Namcore_Studio.Process_status

Public Class Armory_interface
    Public Structure Data2Thread
        Public charLST As List(Of String)
    End Structure
    Private Sub addChar_bt_Click(sender As System.Object, e As System.EventArgs) Handles addChar_bt.Click
        Dim templink As String = "http://#replaceregion#.battle.net/wow/#replacelang#/character/#replacerealm#/#replacecharacter#/advanced"
        Dim RM As New ResourceManager("Namcore_Studio.UserMessages", System.Reflection.Assembly.GetExecutingAssembly())
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
            If My.Settings.language = "de" And globalregion.SelectedItem.ToString = "EU" Then
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
        'todo
    End Sub

    Private Sub addURL_bt_Click(sender As System.Object, e As System.EventArgs) Handles addURL_bt.Click
        Dim templink As String = url_tb.Text
        Dim RM As New ResourceManager("Namcore_Studio.UserMessages", System.Reflection.Assembly.GetExecutingAssembly())
        If Not templink.Contains(".battle.net/wow/") Then
            MsgBox(RM.GetString("invalidurl"), MsgBoxStyle.Critical, RM.GetString("attention"))
        Else
            templink = templink.Replace("/simple", "/advanced")
            If Not templink.StartsWith("http://") Then templink = "http://" & templink
            'Add battle.net maintenance check!!
            Dim client As New WebClient
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

    Private Sub load_bt_Click(sender As System.Object, e As System.EventArgs) Handles load_bt.Click
        trdrunnuing = True
        My.Settings.language = "de" 'todo for testing only
        Dim urllst As New List(Of String)
        For Each lstitm As ListViewItem In char_lst.Items
            urllst.Add(lstitm.SubItems(3).Text)
        Next
       
        Dim d As New Data2Thread() With {.charLST = urllst}
        procStatus.ArmoryWorker.RunWorkerAsync(d)
        procStatus.UpdateGui()
        'Dim armory As New ArmoryHandler
        'trd = New Thread(AddressOf armory.LoadArmoryCharacters)
        'trd.IsBackground = True
        'trd.Start(urllst)

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

        Process_status.Close()
        procStatus = New Process_status
        procStatus.Show()
        procStatus.ArmoryWorker = New System.ComponentModel.BackgroundWorker
        procStatus.ArmoryWorker.WorkerReportsProgress = True
        procStatus.ArmoryWorker.WorkerSupportsCancellation = True
    End Sub
End Class