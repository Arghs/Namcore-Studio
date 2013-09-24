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
'*      /Filename:      AccountCharacterInformationProcessing
'*      /Description:   Prepares basic account and character information for Live_View
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.Database
Imports NCFramework.Framework.Modules
Imports MySql.Data.MySqlClient

Namespace Framework.Core

    Public Class AccountCharacterInformationProcessing
        Public Function ReturnAccountTable(ByVal sqlconnection As MySqlConnection, ByVal struc As DBStructure) As DataTable
            Select Case GlobalVariables.sourceCore
                Case "arcemu"
                    Return _
                        ReturnDataTable_setconn(
                            "SELECT `" & Struc.acc_id_col(0) & "`, `" & Struc.acc_name_col(0) & "`, `" &
                            Struc.acc_gmlevel_col(0) &
                            "`, `" & Struc.acc_lastlogin_col(0) & "`, `" & Struc.acc_email_col(0) & "` FROM " &
                            Struc.account_tbl(0), sqlconnection)
                Case "trinity"
                    Return _
                        ReturnDataTable_setconn(
                            "SELECT " & Struc.account_tbl(0) & ".`" & Struc.acc_id_col(0) & "`, `" & Struc.acc_name_col(0) &
                            "`, `" & Struc.accountAccess_tbl(0) & "`." & Struc.accAcc_gmLevel_col(0) & ", `" &
                            Struc.acc_lastlogin_col(0) &
                            "`, `" & Struc.acc_email_col(0) & "` FROM " & Struc.account_tbl(0) & " JOIN `" &
                            Struc.accountAccess_tbl(0) &
                            "` ON `" & Struc.account_tbl(0) & "`." & Struc.acc_id_col(0) & " = `" &
                            Struc.accountAccess_tbl(0) &
                            "`.`" & Struc.accAcc_accid_col(0) & "`", sqlconnection)
                Case "trinitytbc"
                    'todo
                    Return Nothing
                Case "mangos"
                    Return _
                        ReturnDataTable_setconn(
                            "SELECT `" & Struc.acc_id_col(0) & "`, `" & Struc.acc_name_col(0) & "`, `" &
                            Struc.acc_gmlevel_col(0) &
                            "`, " & Struc.acc_lastlogin_col(0) & "`, `" & Struc.acc_email_col(0) & "` FROM " &
                            Struc.account_tbl(0), sqlconnection)
                Case Else
                    Return Nothing
            End Select
        End Function

        Public Function ReturnCharacterTable(ByVal sqlconnection As MySqlConnection, ByVal struc As DBStructure) _
            As DataTable
            Select Case GlobalVariables.sourceCore
                Case "arcemu"
                    Return _
                        ReturnDataTable_setconn(
                            "SELECT " & Struc.char_guid_col(0) & ", " & Struc.char_accountId_col(0) & ", " &
                            Struc.char_name_col(0) &
                            ", " & Struc.char_race_col(0) & ", " & Struc.char_class_col(0) & ", " & Struc.char_gender_col(0) &
                            ", " & Struc.char_level_col(0) & " FROM characters", sqlconnection)
                Case "trinity"
                    Return _
                        ReturnDataTable_setconn(
                            "SELECT " & Struc.char_guid_col(0) & ", " & Struc.char_accountId_col(0) & ", " &
                            Struc.char_name_col(0) &
                            ", " & Struc.char_race_col(0) & ", " & Struc.char_class_col(0) & ", " & Struc.char_gender_col(0) &
                            ", " & Struc.char_level_col(0) & " FROM characters", sqlconnection)
                Case "trinitytbc"
                    Return ReturnDataTable_setconn("SELECT", sqlconnection)
                Case "mangos"
                    Return _
                        ReturnDataTable_setconn(
                            "SELECT " & Struc.char_guid_col(0) & ", " & Struc.char_accountId_col(0) & ", " &
                            Struc.char_name_col(0) &
                            ", " & Struc.char_race_col(0) & ", " & Struc.char_class_col(0) & ", " & Struc.char_gender_col(0) &
                            ", " & Struc.char_level_col(0) & " FROM characters", sqlconnection)
                Case Else
                    Return Nothing
            End Select
        End Function

        Public Function ReturnTargetAccCharTable(ByVal sqlconnection As MySqlConnection, ByVal struc As DBStructure) _
            As DataTable
            Select Case GlobalVariables.targetCore
                Case "arcemu"
                    Return _
                        ReturnDataTable_setconn(
                            "SELECT u1.`" & Struc.acc_id_col(0) & "`, u1.`" & Struc.acc_name_col(0) & "`, u2.`" &
                            Struc.char_guid_col(0) &
                            "`, u2.`" & Struc.char_name_col(0) & "` FROM " & GlobalVariables.TargetConnRealmDBname & "." &
                            Struc.account_tbl(0) &
                            " u1 LEFT JOIN " & GlobalVariables.TargetConnCharactersDBname & "." & Struc.character_tbl(0) &
                            " u2 ON u2.`" & Struc.char_accountId_col(0) &
                            "` = u1.`" & Struc.acc_id_col(0) & "`", sqlconnection)
                Case "trinity"
                    Return _
                        ReturnDataTable_setconn(
                            "SELECT u1.`" & Struc.acc_id_col(0) & "`, u1.`" & Struc.acc_name_col(0) & "`, u2.`" &
                            Struc.char_guid_col(0) &
                            "`, u2.`" & Struc.char_name_col(0) & "` FROM " & GlobalVariables.TargetConnRealmDBname & "." &
                            Struc.account_tbl(0) &
                            " u1 LEFT JOIN " & GlobalVariables.TargetConnCharactersDBname & "." & Struc.character_tbl(0) &
                            " u2 ON u2.`" & Struc.char_accountId_col(0) &
                            "` = u1.`" & Struc.acc_id_col(0) & "`", sqlconnection)
                Case "trinitytbc"
                    'todo
                    Return Nothing
                Case "mangos"
                    Return _
                        ReturnDataTable_setconn(
                            "SELECT u1.`" & Struc.acc_id_col(0) & "`, u1.`" & Struc.acc_name_col(0) & "`, u2.`" &
                            Struc.char_guid_col(0) &
                            "`, u2.`" & Struc.char_name_col(0) & "` FROM " & GlobalVariables.TargetConnRealmDBname & "." &
                            Struc.account_tbl(0) &
                            " u1 LEFT JOIN " & GlobalVariables.TargetConnCharactersDBname & "." & Struc.character_tbl(0) &
                            " u2 ON u2.`" & Struc.char_accountId_col(0) &
                            "` = u1.`" & Struc.acc_id_col(0) & "`", sqlconnection)
                Case Else
                    Return Nothing
            End Select
        End Function
    End Class
End Namespace