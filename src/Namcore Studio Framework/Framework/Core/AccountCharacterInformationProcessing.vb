'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013-2014 NamCore Studio <https://github.com/megasus/Namcore-Studio>
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
'*      /Filename:      AccountCharacterInformationProcessing
'*      /Description:   Prepares basic account and character information for liveview
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.Database
Imports NCFramework.Framework.Modules
Imports MySql.Data.MySqlClient

Namespace Framework.Core
    Public Class AccountCharacterInformationProcessing
        Public Function ReturnAccountTable(ByVal sqlconnection As MySqlConnection, ByVal struc As DbStructure) _
            As DataTable
            '// Provides a DataTable listing detected accounts
            Select Case GlobalVariables.sourceCore
                Case Modules.Core.ARCEMU
                    Return _
                        ReturnDataTable_setconn(
                            "SELECT `" & struc.acc_id_col(0) & "`, `" & struc.acc_name_col(0) & "`, `" &
                            struc.acc_gmlevel_col(0) &
                            "`, `" & struc.acc_lastlogin_col(0) & "`, `" & struc.acc_email_col(0) & "` FROM " &
                            struc.account_tbl(0), sqlconnection)
                Case Modules.Core.TRINITY
                    Select Case CInt(GlobalVariables.sourceExpansion)
                        Case Is > 2
                            Return ReturnDataTable_setconn(
                                "SELECT " & struc.account_tbl(0) & ".`" & struc.acc_id_col(0) & "`, `" &
                                struc.acc_name_col(0) &
                                "`, `" & struc.accountAccess_tbl(0) & "`." & struc.accAcc_gmLevel_col(0) & ", `" &
                                struc.acc_lastlogin_col(0) &
                                "`, `" & struc.acc_email_col(0) & "` FROM " & struc.account_tbl(0) & " JOIN `" &
                                struc.accountAccess_tbl(0) &
                                "` ON `" & struc.account_tbl(0) & "`." & struc.acc_id_col(0) & " = `" &
                                struc.accountAccess_tbl(0) &
                                "`.`" & struc.accAcc_accid_col(0) & "`", sqlconnection)
                        Case Else
                            Return Nothing
                            '// TODO: EXPANSION SUPPORT
                    End Select
                Case Modules.Core.MANGOS
                    Return _
                        ReturnDataTable_setconn(
                            "SELECT `" & struc.acc_id_col(0) & "`, `" & struc.acc_name_col(0) & "`, `" &
                            struc.acc_gmlevel_col(0) &
                            "`, `" & struc.acc_lastlogin_col(0) & "`, `" & struc.acc_email_col(0) & "` FROM " &
                            struc.account_tbl(0), sqlconnection)
                Case Else
                    Return Nothing
            End Select
        End Function

        Public Function ReturnCharacterTable(ByVal sqlconnection As MySqlConnection, ByVal struc As DbStructure) _
            As DataTable
            '// Provides a DataTable listing detected characters
            Select Case GlobalVariables.sourceCore
                Case Modules.Core.ARCEMU
                    Return _
                        ReturnDataTable_setconn(
                            "SELECT " & struc.char_guid_col(0) & ", " & struc.char_accountId_col(0) & ", " &
                            struc.char_name_col(0) &
                            ", " & struc.char_race_col(0) & ", " & struc.char_class_col(0) & ", " &
                            struc.char_gender_col(0) &
                            ", " & struc.char_level_col(0) & " FROM characters", sqlconnection)
                Case Modules.Core.TRINITY
                    Select Case CInt(GlobalVariables.sourceExpansion)
                        Case Is > 2
                            Return _
                                ReturnDataTable_setconn(
                                    "SELECT " & struc.char_guid_col(0) & ", " & struc.char_accountId_col(0) & ", " &
                                    struc.char_name_col(0) &
                                    ", " & struc.char_race_col(0) & ", " & struc.char_class_col(0) & ", " &
                                    struc.char_gender_col(0) &
                                    ", " & struc.char_level_col(0) & " FROM characters", sqlconnection)
                        Case Else
                            Return Nothing
                            '// TODO: EXPANSION SUPPORT
                    End Select
                Case Modules.Core.MANGOS
                    Return _
                        ReturnDataTable_setconn(
                            "SELECT " & struc.char_guid_col(0) & ", " & struc.char_accountId_col(0) & ", " &
                            struc.char_name_col(0) &
                            ", " & struc.char_race_col(0) & ", " & struc.char_class_col(0) & ", " &
                            struc.char_gender_col(0) &
                            ", " & struc.char_level_col(0) & " FROM characters", sqlconnection)
                Case Else
                    Return Nothing
            End Select
        End Function

        Public Function ReturnTargetAccCharTable(ByVal sqlconnection As MySqlConnection, ByVal struc As DbStructure) _
            As DataTable
            '// Provides a DataTable listing detected accounts/characters on target database
            Select Case GlobalVariables.targetCore
                Case Modules.Core.ARCEMU
                    Return _
                        ReturnDataTable_setconn(
                            "SELECT u1.`" & struc.acc_id_col(0) & "`, u1.`" & struc.acc_name_col(0) & "`, u2.`" &
                            struc.char_guid_col(0) &
                            "`, u2.`" & struc.char_name_col(0) & "` FROM " & GlobalVariables.TargetConnRealmDBname & "." &
                            struc.account_tbl(0) &
                            " u1 LEFT JOIN " & GlobalVariables.TargetConnCharactersDBname & "." & struc.character_tbl(0) &
                            " u2 ON u2.`" & struc.char_accountId_col(0) &
                            "` = u1.`" & struc.acc_id_col(0) & "`", sqlconnection)
                Case Modules.Core.TRINITY
                    Select Case CInt(GlobalVariables.targetExpansion)
                        Case Is > 2
                            Return _
                                ReturnDataTable_setconn(
                                    "SELECT u1.`" & struc.acc_id_col(0) & "`, u1.`" & struc.acc_name_col(0) & "`, u2.`" &
                                    struc.char_guid_col(0) &
                                    "`, u2.`" & struc.char_name_col(0) & "` FROM " &
                                    GlobalVariables.TargetConnRealmDBname & "." &
                                    struc.account_tbl(0) &
                                    " u1 LEFT JOIN " & GlobalVariables.TargetConnCharactersDBname & "." &
                                    struc.character_tbl(0) &
                                    " u2 ON u2.`" & struc.char_accountId_col(0) &
                                    "` = u1.`" & struc.acc_id_col(0) & "`", sqlconnection)
                        Case Else
                            Return Nothing
                            '// TODO: EXPANSION SUPPORT
                    End Select

                Case Modules.Core.MANGOS
                    Return _
                        ReturnDataTable_setconn(
                            "SELECT u1.`" & struc.acc_id_col(0) & "`, u1.`" & struc.acc_name_col(0) & "`, u2.`" &
                            struc.char_guid_col(0) &
                            "`, u2.`" & struc.char_name_col(0) & "` FROM " & GlobalVariables.TargetConnRealmDBname & "." &
                            struc.account_tbl(0) &
                            " u1 LEFT JOIN " & GlobalVariables.TargetConnCharactersDBname & "." & struc.character_tbl(0) &
                            " u2 ON u2.`" & struc.char_accountId_col(0) &
                            "` = u1.`" & struc.acc_id_col(0) & "`", sqlconnection)
                Case Else
                    Return Nothing
            End Select
        End Function
    End Class
End Namespace