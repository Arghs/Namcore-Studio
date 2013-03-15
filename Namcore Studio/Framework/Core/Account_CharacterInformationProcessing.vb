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
'*      /Filename:      Account_CharacterInformationProcessing
'*      /Description:   Prepares basic account and character information for Live_View
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports MySql.Data.MySqlClient
Imports Namcore_Studio.CommandHandler
Imports Namcore_Studio.GlobalVariables
Public Class Account_CharacterInformationProcessing

    Public Shared Function returnAccountTable(ByVal sqlconnection As MySqlConnection) As DataTable
        Select Case sourceCore
            Case "arcemu"
                Return ReturnDataTable_setconn("SELECT acct, `login`, `gm`, `lastlogin`, `email` FROM accounts", sqlconnection)
            Case "trinity"
                Return ReturnDataTable_setconn("SELECT account.`id`, `username`, `account_access`.gmlevel, `last_login`, `email` FROM account JOIN `account_access` ON `account`.id = `account_access`.id", sqlconnection)
            Case "trinitytbc"
                'todo
            Case "mangos"
                Return ReturnDataTable_setconn("SELECT `id`, `username`, `gmlevel`, `last_login`, `email` FROM account", sqlconnection)
            Case Else

        End Select

    End Function

    Public Shared Function returnCharacterTable(ByVal sqlconnection As MySqlConnection) As DataTable
        Select Case sourceCore
            Case "arcemu"
                Return ReturnDataTable_setconn("SELECT guid, acct, name, race, class, gender, level FROM characters", sqlconnection)
            Case "trinity"
                Return ReturnDataTable_setconn("SELECT guid, account, name, race, class, gender, level FROM characters", sqlconnection)
            Case "trinitytbc"
                Return ReturnDataTable_setconn("SELECT", sqlconnection)
            Case "mangos"
                Return ReturnDataTable_setconn("SELECT guid, account, name, race, class, gender, level FROM characters", sqlconnection)
            Case Else

        End Select
    End Function
    Public Shared Function returnTargetAccCharTable(ByVal sqlconnection As MySqlConnection) As DataTable
        Select Case targetCore
            Case "arcemu"
                Return ReturnDataTable_setconn("SELECT u1.`acct`, u1.`login`, u2.`guid`, u2.`name` FROM " & TargetConnRealmDBname & ".accounts u1 LEFT JOIN " & TargetConnCharactersDBname & ".characters u2 ON u2.`acct` = u1.`acct`", sqlconnection)
            Case "trinity"
                Return ReturnDataTable_setconn("SELECT u1.`id`, u1.`username`, u2.`guid`, u2.`name` FROM " & TargetConnRealmDBname & ".account u1 LEFT JOIN " & TargetConnCharactersDBname & ".characters u2 ON u2.`account` = u1.`id`", sqlconnection)
            Case "trinitytbc"
                'todo
            Case "mangos"
                Return ReturnDataTable_setconn("SELECT u1.`id`, u1.`username`, u2.`guid`, u2.`name` FROM " & TargetConnRealmDBname & ".account u1 LEFT JOIN " & TargetConnCharactersDBname & ".characters u2 ON u2.`account` = u1.`id`", sqlconnection)
            Case Else

        End Select

    End Function
End Class
