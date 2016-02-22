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
'*      /Filename:      AccountRemoveHandler
'*      /Description:   Contains functions for removing accounts
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports MySql.Data.MySqlClient
Imports NCFramework.Framework.Database
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Modules

Namespace Framework.Core.Remove
    Public Class AccountRemoveHandler
        Public Sub RemoveAccountFromDb(account As Account, core As Modules.Core,
                                       realmConnection As MySqlConnection, charConnection As MySqlConnection,
                                       dbstruc As DbStructure)
            LogAppend("Removing account " & account.Name & " from database", "AccountRemoveHandler_RemoveAccountFromDb")
            Select Case core
                Case Modules.Core.TRINITY
                    runSQLCommand_realm_string_setconn(
                        "DELETE FROM `" & dbstruc.account_tbl(0) & "` WHERE `" & dbstruc.acc_id_col(0) & "` = '" &
                        account.Id.ToString() & "'", realmConnection)
                    runSQLCommand_realm_string_setconn(
                        "DELETE FROM `" & dbstruc.accountAccess_tbl(0) & "` WHERE `" & dbstruc.accAcc_accid_col(0) &
                        "` = '" & account.Id.ToString() & "'", realmConnection)
                    runSQLCommand_realm_string_setconn(
                        "DELETE FROM `account_banned` WHERE `id` = '" & account.Id.ToString() & "'", realmConnection)
                    runSQLCommand_realm_string_setconn(
                        "DELETE FROM `realmcharacters` WHERE `acctid` = '" & account.Id.ToString() & "'",
                        realmConnection)
                    runSQLCommand_realm_string_setconn(
                        "DELETE FROM `rbac_account_permissions` WHERE `accountId` = '" & account.Id.ToString() & "'",
                        realmConnection)
                    Dim characterRemoveHandler As New CharacterRemoveHandler
                    For Each player As Character In account.Characters
                        characterRemoveHandler.RemoveCharacterFromDb(player, charConnection, dbstruc, core)
                    Next
                Case Modules.Core.MANGOS
                    runSQLCommand_realm_string_setconn(
                        "DELETE FROM `" & dbstruc.account_tbl(0) & "` WHERE `" & dbstruc.acc_id_col(0) & "` = '" &
                        account.Id.ToString() & "'", realmConnection)
                    runSQLCommand_realm_string_setconn(
                        "DELETE FROM `account_banned` WHERE `id` = '" & account.Id.ToString() & "'", realmConnection)
                    runSQLCommand_realm_string_setconn(
                        "DELETE FROM `realmcharacters` WHERE `acctid` = '" & account.Id.ToString() & "'",
                        realmConnection)
                    Dim characterRemoveHandler As New CharacterRemoveHandler
                    For Each player As Character In account.Characters
                        characterRemoveHandler.RemoveCharacterFromDb(player, charConnection, dbstruc, core)
                    Next
            End Select
        End Sub
    End Class
End Namespace