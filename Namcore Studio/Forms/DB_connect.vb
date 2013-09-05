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
'*      /Filename:      DB_connect
'*      /Description:   Interface for opening a database connection
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.ResourceHandler
Imports NCFramework.ConnectionHandler
Imports NCFramework.GlobalVariables
Imports MySql.Data.MySqlClient
Imports NCFramework.Account_CharacterInformationProcessing
Imports NCFramework.CommandHandler
Imports NCFramework
Imports System.Resources

Public Class DB_connect

    Private cmpFileListViewComparer As ListViewComparer
    Dim checkchangestatus As Boolean = False
    Dim struc_check As New dbStruc_check
    Private Sub connect_bt_Click(sender As System.Object, e As System.EventArgs) Handles connect_bt.Click
        Select Case con_operator
            Case 1 'Source connection @live_view
                globChars = New GlobalCharVars()
                globChars.CharacterSets = New List(Of Character)
                armoryMode = False
                templateMode = False
                con_operator = 0
                cmpFileListViewComparer = New ListViewComparer(Live_View.accountview)
                If defaultconn_radio.Checked = True Then
                    If TestConnection("server=" & db_address_txtbox.Text & ";Port=" & port_txtbox.Text & ";User id=" & userid_txtbox.Text & ";Password=" & password_txtbox.Text & ";Database=" & realmdbname_txtbox.Text) = True Then
                        If TestConnection("server=" & db_address_txtbox.Text & ";Port=" & port_txtbox.Text & ";User id=" & userid_txtbox.Text & ";Password=" & password_txtbox.Text & ";Database=" & chardbname_txtbox.Text) = True Then
                            GlobalConnectionString = "server=" & db_address_txtbox.Text & ";Port=" & port_txtbox.Text & ";User id=" & userid_txtbox.Text & ";Password=" & password_txtbox.Text & ";Database=" & chardbname_txtbox.Text & ";Convert Zero Datetime=True"
                            GlobalConnectionString_Realm = "server=" & db_address_txtbox.Text & ";Port=" & port_txtbox.Text & ";User id=" & userid_txtbox.Text & ";Password=" & password_txtbox.Text & ";Database=" & realmdbname_txtbox.Text & ";Convert Zero Datetime=True"
                            OpenNewMySQLConnection(GlobalConnection, GlobalConnectionString)
                            OpenNewMySQLConnection(GlobalConnection_Realm, GlobalConnectionString_Realm)
                            GlobalConnection_Info.ConnectionString = "server=" & db_address_txtbox.Text & ";Port=" & port_txtbox.Text & ";User id=" & userid_txtbox.Text & ";Password=" & password_txtbox.Text & ";Database=information_schema"
                            struc_check.startCheck("trinity", 3, GlobalConnection, GlobalConnection_Realm, GlobalConnection_Info, chardbname_txtbox.Text, realmdbname_txtbox.Text, False) 'todo
                            For Each CurrentForm As Form In Application.OpenForms
                                If CurrentForm.Name = "Live_View" Then
                                    Dim liveview As Live_View = DirectCast(CurrentForm, Live_View)
                                    liveview.accountview.Items.Clear()
                                    liveview.characterview.Items.Clear()
                                    liveview.loadaccountsandchars()
                                End If
                            Next
                        Else

                        End If

                    Else

                    End If
                Else

                End If
            Case 2 'Target connection @live_view
                con_operator = 0
                If defaultconn_radio.Checked = True Then
                    If TestConnection("server=" & db_address_txtbox.Text & ";Port=" & port_txtbox.Text & ";User id=" & userid_txtbox.Text & ";Password=" & password_txtbox.Text & ";Database=" & realmdbname_txtbox.Text) = True Then
                        If TestConnection("server=" & db_address_txtbox.Text & ";Port=" & port_txtbox.Text & ";User id=" & userid_txtbox.Text & ";Password=" & password_txtbox.Text & ";Database=" & chardbname_txtbox.Text) = True Then
                            TargetConnectionString = "server=" & db_address_txtbox.Text & ";Port=" & port_txtbox.Text & ";User id=" & userid_txtbox.Text & ";Password=" & password_txtbox.Text & ";Database=" & chardbname_txtbox.Text & ";Convert Zero Datetime=True"
                            TargetConnectionString_Realm = "server=" & db_address_txtbox.Text & ";Port=" & port_txtbox.Text & ";User id=" & userid_txtbox.Text & ";Password=" & password_txtbox.Text & ";Database=" & realmdbname_txtbox.Text & ";Convert Zero Datetime=True"
                            OpenNewMySQLConnection(TargetConnection, TargetConnectionString)
                            OpenNewMySQLConnection(TargetConnection_Realm, TargetConnectionString_Realm)
                            TargetConnRealmDBname = realmdbname_txtbox.Text
                            TargetConnCharactersDBname = chardbname_txtbox.Text
                            TargetConnection_Info.ConnectionString = "server=" & db_address_txtbox.Text & ";Port=" & port_txtbox.Text & ";User id=" & userid_txtbox.Text & ";Password=" & password_txtbox.Text & ";Database=information_schema"
                            struc_check.startCheck("trinity", 3, TargetConnection, TargetConnection_Realm, TargetConnection_Info, chardbname_txtbox.Text, realmdbname_txtbox.Text, True) 'todo
                            For Each CurrentForm As Form In Application.OpenForms
                                If CurrentForm.Name = "Live_View" Then
                                    Dim liveview As Live_View = DirectCast(CurrentForm, Live_View)
                                    liveview.target_accounts_tree.Nodes.Clear()
                                    liveview.loadtargetaccountsandchars()
                                End If
                            Next
                        Else

                        End If

                    Else

                    End If
                Else

                End If
        End Select
        Me.Close()


    End Sub

    Private Sub DB_connect_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Dim RM as New ResourceManager("NCFramework.UserMessages", System.Reflection.Assembly.GetExecutingAssembly())
        Select Case con_operator
            Case 1 : connect_header_label.Text = GetUserMessage("connecttosource")
            Case 2 : connect_header_label.Text = GetUserMessage("connecttotarget")
        End Select
    End Sub
End Class