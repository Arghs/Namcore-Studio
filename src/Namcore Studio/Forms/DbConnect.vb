'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
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
'*      /Filename:      DBconnect
'*      /Description:   Interface for opening a database connection
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.Linq
Imports MySql.Data.MySqlClient
Imports NamCore_Studio.Forms.Extension
Imports NamCore_Studio.Modules.Interface
Imports NCFramework.Framework.Database
Imports NCFramework.Framework.Modules
Imports NCFramework.My
Imports NCFramework.My.Resources

Namespace Forms
    Public Class DbConnect
        Inherits EventTrigger

        '// Declaration
        ReadOnly _strucCheck As New DbStrucCheck
        Dim _catchError As Boolean = False
        '// Declaration

        Private Sub connect_bt_Click(sender As Object, e As EventArgs) Handles connect_bt.Click
            _catchError = False
            Select Case GlobalVariables.con_operator
                Case 1 'Source connection @liveview
                    If trinity335picker.Checked = True Then GlobalVariables.sourceCore = Core.TRINITY
                    If mangos335picker.Checked = True Then GlobalVariables.sourceCore = Core.MANGOS
                    GlobalVariables.globChars = New GlobalCharVars()
                    GlobalVariables.globChars.AccountSets = New List(Of Account)
                    GlobalVariables.armoryMode = False
                    GlobalVariables.templateMode = False
                    GlobalVariables.GlobalConnectionString = ""
                    GlobalVariables.GlobalConnectionString_Realm = ""
                    If defaultconn_radio.Checked = True Then
                        If _
                            TestConnection(
                                "server=" & db_address_txtbox.Text & ";Port=" & port_ud.Value.ToString & ";User id=" &
                                userid_txtbox.Text & ";Password=" & password_txtbox.Text & ";Database=" &
                                realmdbname_combo.Text) = True Then
                            If _
                                TestConnection(
                                    "server=" & db_address_txtbox.Text & ";Port=" & port_ud.Value.ToString & ";User id=" &
                                    userid_txtbox.Text & ";Password=" & password_txtbox.Text & ";Database=" &
                                    chardbname_combo.Text) = True Then
                                GlobalVariables.GlobalConnectionString = "server=" & db_address_txtbox.Text & ";Port=" &
                                                                         port_ud.Value.ToString & ";User id=" &
                                                                         userid_txtbox.Text & ";Password=" &
                                                                         password_txtbox.Text & ";Database=" &
                                                                         chardbname_combo.Text &
                                                                         ";Convert Zero Datetime=True"
                                GlobalVariables.GlobalConnectionString_Realm = "server=" & db_address_txtbox.Text &
                                                                               ";Port=" &
                                                                               port_ud.Value.ToString & ";User id=" &
                                                                               userid_txtbox.Text & ";Password=" &
                                                                               password_txtbox.Text & ";Database=" &
                                                                               realmdbname_combo.Text &
                                                                               ";Convert Zero Datetime=True"
                                OpenNewMySqlConnection(GlobalVariables.GlobalConnection,
                                                       GlobalVariables.GlobalConnectionString)
                                OpenNewMySqlConnection(GlobalVariables.GlobalConnection_Realm,
                                                       GlobalVariables.GlobalConnectionString_Realm)
                                GlobalVariables.GlobalConnection_Info.ConnectionString = "server=" &
                                                                                         db_address_txtbox.Text &
                                                                                         ";Port=" &
                                                                                         port_ud.Value.ToString &
                                                                                         ";User id=" &
                                                                                         userid_txtbox.Text &
                                                                                         ";Password=" &
                                                                                         password_txtbox.Text &
                                                                                         ";Database=information_schema"
                                _strucCheck.StartCheck(GlobalVariables.sourceCore, Expansion.WOTLK,
                                                       GlobalVariables.GlobalConnection,
                                                       GlobalVariables.GlobalConnection_Realm,
                                                       GlobalVariables.GlobalConnection_Info, chardbname_combo.Text,
                                                       realmdbname_combo.Text, False) 'todo
                                Dim liveview As New LiveView
                                Try
                                    For Each currentForm As Form In _
                                        From currentForm1 As Object In Application.OpenForms
                                            Where TryCast(currentForm1, Form).Name = "LiveView"
                                        liveview = DirectCast(currentForm, LiveView)
                                    Next
                                Catch ex As Exception
                                End Try
                                If Not liveview Is Nothing Then
                                    liveview.accountview.Items.Clear()
                                    liveview.characterview.Items.Clear()
                                    liveview.Loadaccountsandchars()
                                End If
                            Else
                                MsgBox(MSG_FAILEDCONNECTCHARACTER, MsgBoxStyle.Critical,
                                       MSG_ERROR)
                                _catchError = True
                            End If
                        Else
                            MsgBox(MSG_FAILEDCONNECTREALM, MsgBoxStyle.Critical,
                                   MSG_ERROR)
                            _catchError = True
                        End If
                    Else

                    End If
                Case 2 'Target connection @liveview
                    If trinity335picker.Checked = True Then GlobalVariables.targetCore = Core.TRINITY
                    If mangos335picker.Checked = True Then GlobalVariables.targetCore = Core.MANGOS
                    GlobalVariables.TargetConnectionString = ""
                    GlobalVariables.TargetConnectionString_Realm = ""
                    If defaultconn_radio.Checked = True Then
                        If _
                            TestConnection(
                                "server=" & db_address_txtbox.Text & ";Port=" & port_ud.Value.ToString & ";User id=" &
                                userid_txtbox.Text & ";Password=" & password_txtbox.Text & ";Database=" &
                                realmdbname_combo.Text) = True Then
                            If _
                                TestConnection(
                                    "server=" & db_address_txtbox.Text & ";Port=" & port_ud.Value.ToString & ";User id=" &
                                    userid_txtbox.Text & ";Password=" & password_txtbox.Text & ";Database=" &
                                    chardbname_combo.Text) = True Then
                                GlobalVariables.TargetConnectionString = "server=" & db_address_txtbox.Text & ";Port=" &
                                                                         port_ud.Value.ToString & ";User id=" &
                                                                         userid_txtbox.Text & ";Password=" &
                                                                         password_txtbox.Text & ";Database=" &
                                                                         chardbname_combo.Text &
                                                                         ";Convert Zero Datetime=True"
                                GlobalVariables.TargetConnectionString_Realm = "server=" & db_address_txtbox.Text &
                                                                               ";Port=" &
                                                                               port_ud.Value.ToString & ";User id=" &
                                                                               userid_txtbox.Text & ";Password=" &
                                                                               password_txtbox.Text & ";Database=" &
                                                                               realmdbname_combo.Text &
                                                                               ";Convert Zero Datetime=True"
                                OpenNewMySqlConnection(GlobalVariables.TargetConnection,
                                                       GlobalVariables.TargetConnectionString)
                                OpenNewMySqlConnection(GlobalVariables.TargetConnection_Realm,
                                                       GlobalVariables.TargetConnectionString_Realm)
                                GlobalVariables.TargetConnRealmDBname = realmdbname_combo.Text
                                GlobalVariables.TargetConnCharactersDBname = chardbname_combo.Text
                                GlobalVariables.TargetConnection_Info.ConnectionString = "server=" &
                                                                                         db_address_txtbox.Text &
                                                                                         ";Port=" &
                                                                                         port_ud.Value.ToString &
                                                                                         ";User id=" &
                                                                                         userid_txtbox.Text &
                                                                                         ";Password=" &
                                                                                         password_txtbox.Text &
                                                                                         ";Database=information_schema"
                                _strucCheck.StartCheck(GlobalVariables.targetCore, Expansion.WOTLK,
                                                       GlobalVariables.TargetConnection,
                                                       GlobalVariables.TargetConnection_Realm,
                                                       GlobalVariables.TargetConnection_Info, chardbname_combo.Text,
                                                       realmdbname_combo.Text, True) 'todo
                                Dim liveview As New LiveView
                                Try
                                    For Each currentForm As Form In _
                                        From currentForm1 As Object In Application.OpenForms
                                            Where TryCast(currentForm1, Form).Name = "LiveView"
                                        liveview = DirectCast(currentForm, LiveView)
                                    Next
                                Catch ex As Exception
                                End Try
                                If Not liveview Is Nothing Then
                                    liveview.target_accounts_tree.Nodes.Clear()
                                    liveview.Loadtargetaccountsandchars()
                                End If
                            Else
                                MsgBox(MSG_FAILEDCONNECTCHARACTER, MsgBoxStyle.Critical,
                                       MSG_ERROR)
                                _catchError = True
                            End If
                        Else
                            MsgBox(MSG_FAILEDCONNECTREALM, MsgBoxStyle.Critical,
                                   MSG_ERROR)
                            _catchError = True
                        End If
                    End If
            End Select
            If _catchError = False Then
                Close()
            End If
        End Sub

        Private Sub DB_connect_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            AddHandler highlighter2.Click, AddressOf highlighter2_click
            Select Case GlobalVariables.con_operator
                Case 1 : connect_header_label.Text = MSG_CONNECTSOURCE
                Case 2 : connect_header_label.Text = MSG_CONNECTTARGET
            End Select
            Dim controlLst As List(Of Control)
            controlLst = FindAllChildren()
            For Each itemControl As Control In controlLst
                itemControl.SetDoubleBuffered()
            Next
            If MySettings.Default.server_defaultconn = True Then
                defaultconn_radio.Checked = True
                db_address_txtbox.Text = MySettings.Default.server_address
                port_ud.Value = MySettings.Default.server_port
                userid_txtbox.Text = MySettings.Default.server_login
                password_txtbox.Text = MySettings.Default.server_pass
                chardbname_combo.Text = MySettings.Default.server_chardb
                realmdbname_combo.Text = MySettings.Default.server_authdb
            Else
                viaserver_radio.Checked = True
                serveraddress_txtbox.Text = MySettings.Default.server_ncremoteaddress
                rmuser_txtbox.Text = MySettings.Default.server_nclogin
            End If
        End Sub

        Private Sub highlighter2_click(sender As Object, e As EventArgs)
            Close()
        End Sub

        Private Sub savelogin_bt_Click(sender As Object, e As EventArgs) Handles savelogin_bt.Click
            If defaultconn_radio.Checked = True Then
                MySettings.Default.server_defaultconn = True
                MySettings.Default.server_address = db_address_txtbox.Text
                MySettings.Default.server_port = CType(port_ud.Value, Integer)
                MySettings.Default.server_login = userid_txtbox.Text
                MySettings.Default.server_pass = password_txtbox.Text
                MySettings.Default.server_chardb = chardbname_combo.Text
                MySettings.Default.server_authdb = realmdbname_combo.Text
                MySettings.Default.Save()
            Else
                MySettings.Default.server_defaultconn = False
                MySettings.Default.server_ncremoteaddress = serveraddress_txtbox.Text
                MySettings.Default.server_nclogin = rmuser_txtbox.Text
                MySettings.Default.Save()
            End If
        End Sub

        Private Sub selectdb_Browse(sender As Object, e As EventArgs) _
            Handles realmdbname_combo.DropDown, chardbname_combo.DropDown
            Dim combo = CType(sender, ComboBox)
            combo.Items.Clear()
            Dim cmd As MySqlCommand
            Dim dr As MySqlDataReader
            Dim thisSqlConnection As MySqlConnection = TestConnectionAndReturn(
                "server=" & db_address_txtbox.Text & ";Port=" & port_ud.Value.ToString & ";User id=" &
                userid_txtbox.Text & ";Password=" & password_txtbox.Text & ";Database=mysql")
            If thisSqlConnection IsNot Nothing Then
                cmd = New MySqlCommand("Show Databases", thisSqlConnection)
                dr = cmd.ExecuteReader
                If dr.HasRows Then
                    Do While dr.Read
                        combo.Items.Add(dr.Item("Database").ToString)
                    Loop
                End If
                thisSqlConnection.Close()
                thisSqlConnection.Dispose()
            End If
        End Sub

        Private Sub dbname_combo_SelectedIndexChanged(sender As Object, e As EventArgs) _
            Handles realmdbname_combo.SelectedIndexChanged, chardbname_combo.SelectedIndexChanged
            Dim combo = CType(sender, ComboBox)
            combo.Text = combo.SelectedItem.ToString()
        End Sub
    End Class
End Namespace