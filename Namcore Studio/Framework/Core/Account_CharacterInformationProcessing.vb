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
                Return ReturnDataTable_setconn("SELECT `" & sourceStructure.acc_id_col(0) & "`, `" & sourceStructure.acc_name_col(0) & "`, `" & sourceStructure.acc_gmlevel_col(0) &
                                               "`, `" & sourceStructure.acc_lastlogin_col(0) & "`, `" & sourceStructure.acc_email_col(0) & "` FROM " & sourceStructure.account_tbl(0), sqlconnection)
            Case "trinity"
                Return ReturnDataTable_setconn("SELECT " & sourceStructure.account_tbl(0) & ".`" & sourceStructure.acc_id_col(0) & "`, `" & sourceStructure.acc_name_col(0) &
                                               "`, `" & sourceStructure.accountAccess_tbl(0) & "`." & sourceStructure.accAcc_gmLevel_col(0) & ", `" & sourceStructure.acc_lastlogin_col(0) &
                                               "`, `" & sourceStructure.acc_email_col(0) & "` FROM " & sourceStructure.account_tbl(0) & " JOIN `" & sourceStructure.accountAccess_tbl(0) &
                                               "` ON `" & sourceStructure.account_tbl(0) & "`." & sourceStructure.acc_id_col(0) & " = `" & sourceStructure.accountAccess_tbl(0) &
                                               "`" & sourceStructure.accAcc_accid_col(0) & "", sqlconnection)
            Case "trinitytbc"
                'todo
            Case "mangos"
                Return ReturnDataTable_setconn("SELECT `" & sourceStructure.acc_id_col(0) & "`, `" & sourceStructure.acc_name_col(0) & "`, `" & sourceStructure.acc_gmlevel_col(0) &
                                               "`, " & sourceStructure.acc_lastlogin_col(0) & "`, `" & sourceStructure.acc_email_col(0) & "` FROM " & sourceStructure.account_tbl(0), sqlconnection)
            Case Else

        End Select

    End Function

    Public Shared Function returnCharacterTable(ByVal sqlconnection As MySqlConnection) As DataTable
        Select Case sourceCore
            Case "arcemu"
                Return ReturnDataTable_setconn("SELECT " & sourceStructure.char_guid_col(0) & ", " & sourceStructure.char_accountId_col(0) & ", " & sourceStructure.char_name_col(0) &
                                               ", " & sourceStructure.char_race_col(0) & ", " & sourceStructure.char_class_col(0) & ", " & sourceStructure.char_gender_col(0) &
                                               ", " & sourceStructure.char_level_col(0) & " FROM characters", sqlconnection)
            Case "trinity"
                Return ReturnDataTable_setconn("SELECT " & sourceStructure.char_guid_col(0) & ", " & sourceStructure.char_accountId_col(0) & ", " & sourceStructure.char_name_col(0) &
                                               ", " & sourceStructure.char_race_col(0) & ", " & sourceStructure.char_class_col(0) & ", " & sourceStructure.char_gender_col(0) &
                                               ", " & sourceStructure.char_level_col(0) & " FROM characters", sqlconnection)
            Case "trinitytbc"
                Return ReturnDataTable_setconn("SELECT", sqlconnection)
            Case "mangos"
                Return ReturnDataTable_setconn("SELECT " & sourceStructure.char_guid_col(0) & ", " & sourceStructure.char_accountId_col(0) & ", " & sourceStructure.char_name_col(0) &
                                               ", " & sourceStructure.char_race_col(0) & ", " & sourceStructure.char_class_col(0) & ", " & sourceStructure.char_gender_col(0) &
                                               ", " & sourceStructure.char_level_col(0) & " FROM characters", sqlconnection)
            Case Else

        End Select
    End Function
    Public Shared Function returnTargetAccCharTable(ByVal sqlconnection As MySqlConnection) As DataTable
        Select Case targetCore
            Case "arcemu"
                Return ReturnDataTable_setconn("SELECT u1.`" & sourceStructure.acc_id_col(0) & "`, u1.`" & sourceStructure.acc_name_col(0) & "`, u2.`" & sourceStructure.char_guid_col(0) &
                                               "`, u2.`" & sourceStructure.char_name_col(0) & "` FROM " & TargetConnRealmDBname & "." & sourceStructure.account_tbl(0) &
                                               " u1 LEFT JOIN " & TargetConnCharactersDBname & "." & sourceStructure.character_tbl(0) & " u2 ON u2.`" & sourceStructure.char_accountId_col(0) &
                                               "` = u1.`" & sourceStructure.acc_id_col(0) & "`", sqlconnection)
            Case "trinity"
                Return ReturnDataTable_setconn("SELECT u1.`" & sourceStructure.acc_id_col(0) & "`, u1.`" & sourceStructure.acc_name_col(0) & "`, u2.`" & sourceStructure.char_guid_col(0) &
                                               "`, u2.`" & sourceStructure.char_name_col(0) & "` FROM " & TargetConnRealmDBname & "." & sourceStructure.account_tbl(0) &
                                               " u1 LEFT JOIN " & TargetConnCharactersDBname & "." & sourceStructure.character_tbl(0) & " u2 ON u2.`" & sourceStructure.char_accountId_col(0) &
                                               "` = u1.`" & sourceStructure.acc_id_col(0) & "`", sqlconnection)
            Case "trinitytbc"
                'todo
            Case "mangos"
                Return ReturnDataTable_setconn("SELECT u1.`" & sourceStructure.acc_id_col(0) & "`, u1.`" & sourceStructure.acc_name_col(0) & "`, u2.`" & sourceStructure.char_guid_col(0) &
                                               "`, u2.`" & sourceStructure.char_name_col(0) & "` FROM " & TargetConnRealmDBname & "." & sourceStructure.account_tbl(0) &
                                               " u1 LEFT JOIN " & TargetConnCharactersDBname & "." & sourceStructure.character_tbl(0) & " u2 ON u2.`" & sourceStructure.char_accountId_col(0) &
                                               "` = u1.`" & sourceStructure.acc_id_col(0) & "`", sqlconnection)
            Case Else

        End Select

    End Function
End Class
