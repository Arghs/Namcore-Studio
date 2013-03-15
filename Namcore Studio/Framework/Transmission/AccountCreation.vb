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
'*      /Filename:      AccountCreation
'*      /Description:   Includes functions for creating new accounts
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Imports Namcore_Studio.EventLogging
Imports Namcore_Studio.CommandHandler
Imports Namcore_Studio.GlobalVariables
Imports Namcore_Studio.Basics
Imports MySql.Data.MySqlClient
Public Class AccountCreation
    Public Shared Sub CreateNewAccount(ByVal accname As String, ByVal passhas As String, ByVal realmid As Integer, ByVal setId As Integer, Optional gmlevel As String = "A", Optional email As String = "", Optional flags As String = "0")
        LogAppend("Creating new account " & accname, "AccountCreation_CreateNewAccount", True)
        Select Case targetCore
            Case "arcemu"
                If ReturnResultCount("SELECT `login` FROM accounts WHERE `login`='" & accname & "'") = 0 Then
                    LogAppend("Account " & accname & " does not exist -> Creating it", "AccountCreation_CreateNewAccount", False)
                    'TODO multi realm support
                    Dim newid As Integer = runSQLCommand_realm_string("SELECT MAX(acct) + 1 FROM accounts", True)
                    Const sqlstring As String = "INSERT INTO accounts (acct, `login`, `encrypted_password`, `gm`, `email`, `flags`) " &
                                               "VALUES (@accid, @accname, @pass, @gm, @email, @flags)"
                    Dim tempcommand As New MySqlCommand(sqlstring, TargetConnection_Realm)
                    tempcommand.Parameters.AddWithValue("@accid", newid)
                    tempcommand.Parameters.AddWithValue("@accname", accname)
                    tempcommand.Parameters.AddWithValue("@pass", passhas)
                    tempcommand.Parameters.AddWithValue("@gm", gmlevel)
                    tempcommand.Parameters.AddWithValue("@email", email)
                    tempcommand.Parameters.AddWithValue("@flags", flags)
                    Try
                        tempcommand.ExecuteNonQuery()
                    Catch ex As Exception
                        LogAppend("Something went wrong while creating the account -> Skipping! -> Error message is: " & ex.ToString(), "AccountCreation_CreateNewAccount", False, True)
                    End Try
                Else
                    LogAppend("Account " & accname & " does exist -> Leaving it untouched!", "AccountCreation_CreateNewAccount", False)
                End If
            Case "trinity"
                If ReturnResultCount("SELECT `username` FROM accounts WHERE `username`='" & accname & "'") = 0 Then
                    LogAppend("Account " & accname & " does not exist -> Creating it", "AccountCreation_CreateNewAccount", False)
                    Dim newid As Integer = runSQLCommand_realm_string("SELECT MAX(acct) + 1 FROM accounts", True)
                    Const sqlstring As String = "INSERT INTO accounts (id, `username`, `sha_pass_hash`, `email`, `joindate`, `expansion`) " &
                                               "VALUES (@accid, @accname, @pass, @email, @joindate, @expansion)"
                    Dim tempcommand As New MySqlCommand(sqlstring, TargetConnection_Realm)
                    tempcommand.Parameters.AddWithValue("@accid", newid)
                    tempcommand.Parameters.AddWithValue("@accname", accname)
                    tempcommand.Parameters.AddWithValue("@pass", passhas)
                    tempcommand.Parameters.AddWithValue("@email", email)
                    tempcommand.Parameters.AddWithValue("@joindate", GetTemporaryCharacterInformation("@account_joindate", setId))
                    tempcommand.Parameters.AddWithValue("@expansion", GetTemporaryCharacterInformation("@account_expansion", setId))
                    Try
                        tempcommand.ExecuteNonQuery()
                    Catch ex As Exception
                        LogAppend("Something went wrong while creating the account -> Skipping! -> Error message is: " & ex.ToString(), "AccountCreation_CreateNewAccount", False, True)
                        Exit Sub
                    End Try
                    If realmid = 0 Then
                        realmid = -1
                    ElseIf realmid = Nothing Then
                        realmid = -1
                    Else : End If
                    runSQLCommand_realm_string("INSERT INTO account_access ( id, gmlevel, RealmID ) VALUES ( '" & newid & "', '" & gmlevel & "', '" & realmid.ToString & "' )", True)
                Else
                    LogAppend("Account " & accname & " does exist -> Leaving it untouched!", "AccountCreation_CreateNewAccount", False)
                End If
            Case "trinitytbc"
                'TODO
            Case "mangos"
                If ReturnResultCount("SELECT `username` FROM accounts WHERE `username`='" & accname & "'") = 0 Then
                    LogAppend("Account " & accname & " does not exist -> Creating it", "AccountCreation_CreateNewAccount", False)
                    If realmid = 0 Then
                        realmid = -1
                    ElseIf realmid = Nothing Then
                        realmid = -1
                    Else : End If
                    Dim newid As Integer = runSQLCommand_realm_string("SELECT MAX(acct) + 1 FROM accounts", True)
                    Const sqlstring As String = "INSERT INTO accounts (id, `username`, `sha_pass_hash`, `email`, `joindate`, `expansion`, active_realm_id) " &
                                               "VALUES (@accid, @accname, @pass, @email, @joindate, @expansion, @realmid)"
                    Dim tempcommand As New MySqlCommand(sqlstring, TargetConnection_Realm)
                    tempcommand.Parameters.AddWithValue("@accid", newid)
                    tempcommand.Parameters.AddWithValue("@accname", accname)
                    tempcommand.Parameters.AddWithValue("@pass", passhas)
                    tempcommand.Parameters.AddWithValue("@email", email)
                    tempcommand.Parameters.AddWithValue("@joindate", GetTemporaryCharacterInformation("@account_joindate", setId))
                    tempcommand.Parameters.AddWithValue("@expansion", GetTemporaryCharacterInformation("@account_expansion", setId))
                    tempcommand.Parameters.AddWithValue("@realmid", realmid)
                    Try
                        tempcommand.ExecuteNonQuery()
                    Catch ex As Exception
                        LogAppend("Something went wrong while creating the account -> Skipping! -> Error message is: " & ex.ToString(), "AccountCreation_CreateNewAccount", False, True)
                        Exit Sub
                    End Try
                Else
                    LogAppend("Account " & accname & " does exist -> Leaving it untouched!", "AccountCreation_CreateNewAccount", False)
                End If
            Case Else : End Select
    End Sub
End Class
