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
'*      /Filename:      AccountHandler
'*      /Description:   Contains functions for loading account
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Database
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Modules
Imports NCFramework.Framework.Extension.Special

Namespace Framework.Core

    Public Class AccountHandler

        '// Declaration
        Private _tempResult As String
        '// Declaration

        Public Sub LoadAccount(ByVal accountId As Integer, ByVal tarSetId As Integer)

            Select Case GlobalVariables.sourceCore
                Case "arcemu"
                    _tempResult =
                        runSQLCommand_realm_string(
                            "SELECT " & GlobalVariables.sourceStructure.acc_name_col(0) & " FROM " &
                            GlobalVariables.sourceStructure.account_tbl(0) & " WHERE " &
                            GlobalVariables.sourceStructure.acc_id_col(0) & "='" & accountId.ToString & "'")
                    Dim tmpAccount As New Account(_tempResult, accountId)
                    tmpAccount.SourceExpansion = GlobalVariables.sourceExpansion
                    tmpAccount.Core = GlobalVariables.sourceCore
                    LogAppend(
                        "Loaded account name info for accountId: " & accountId.ToString & " and setId: " & tarSetId &
                        " // result is: " & _tempResult, "AccountHandler_LoadAccount", True)
                    _tempResult =
                        runSQLCommand_realm_string(
                            "SELECT " & GlobalVariables.sourceStructure.acc_arcemuPass_col(0) & " FROM " &
                            GlobalVariables.sourceStructure.account_tbl(0) & " WHERE " &
                            GlobalVariables.sourceStructure.acc_id_col(0) & "='" & accountId.ToString & "'")
                    tmpAccount.ArcEmuPass = _tempResult
                    LogAppend(
                        "Loaded account arcemuPass info for accountId: " & accountId.ToString & " and setId: " & tarSetId &
                        " // result is: " & _tempResult, "AccountHandler_LoadAccount", False)
                    _tempResult =
                        runSQLCommand_realm_string(
                            "SELECT " & GlobalVariables.sourceStructure.acc_passHash_col(0) & " FROM " &
                            GlobalVariables.sourceStructure.account_tbl(0) & " WHERE " &
                            GlobalVariables.sourceStructure.acc_id_col(0) & "='" & accountId.ToString & "'")
                    tmpAccount.PassHash = _tempResult
                    LogAppend(
                        "Loaded account passHash info for accountId: " & accountId.ToString & " and setId: " & tarSetId &
                        " // result is: " & _tempResult, "AccountHandler_LoadAccount", False)
                    _tempResult =
                        runSQLCommand_realm_string(
                            "SELECT " & GlobalVariables.sourceStructure.acc_arcemuFlags_col(0) & " FROM " &
                            GlobalVariables.sourceStructure.account_tbl(0) & " WHERE " &
                            GlobalVariables.sourceStructure.acc_id_col(0) & "='" & accountId.ToString & "'")
                    tmpAccount.ArcEmuFlags = TryInt(_tempResult)
                    LogAppend(
                        "Loaded account arcemuFlags info for accountId: " & accountId.ToString & " and setId: " & tarSetId &
                        " // result is: " & _tempResult, "AccountHandler_LoadAccount", False)
                    _tempResult =
                        runSQLCommand_realm_string(
                            "SELECT " & GlobalVariables.sourceStructure.acc_locale_col(0) & " FROM " &
                            GlobalVariables.sourceStructure.account_tbl(0) & " WHERE " &
                            GlobalVariables.sourceStructure.acc_id_col(0) & "='" & accountId.ToString & "'")
                    tmpAccount.Locale = TryInt(_tempResult)
                    LogAppend(
                        "Loaded account locale info for accountId: " & accountId.ToString & " and setId: " & tarSetId &
                        " // result is: " & _tempResult, "AccountHandler_LoadAccount", False)
                    _tempResult =
                        runSQLCommand_realm_string(
                            "SELECT " & GlobalVariables.sourceStructure.acc_arcemuGmLevel_col(0) & " FROM " &
                            GlobalVariables.sourceStructure.account_tbl(0) & " WHERE " &
                            GlobalVariables.sourceStructure.acc_id_col(0) & "='" & accountId.ToString & "'")
                    tmpAccount.ArcEmuGmLevel = _tempResult
                    LogAppend(
                        "Loaded account arcemuGmLevel info for accountId: " & accountId.ToString & " and setId: " & tarSetId &
                        " // result is: " & _tempResult, "AccountHandler_LoadAccount", False)
                    _tempResult =
                     runSQLCommand_realm_string(
                         "SELECT " & GlobalVariables.sourceStructure.acc_email_col(0) & " FROM " &
                         GlobalVariables.sourceStructure.account_tbl(0) & " WHERE " &
                         GlobalVariables.sourceStructure.acc_id_col(0) & "='" & accountId.ToString & "'")
                    tmpAccount.Email = _tempResult
                    LogAppend(
                        "Loaded account email for accountId: " & accountId.ToString & " and setId: " & tarSetId &
                        " // result is: " & _tempResult, "AccountHandler_LoadAccount", False)
                    _tempResult =
                       runSQLCommand_realm_string(
                           "SELECT " & GlobalVariables.sourceStructure.acc_lastlogin_col(0) & " FROM " &
                           GlobalVariables.sourceStructure.account_tbl(0) & " WHERE " &
                           GlobalVariables.sourceStructure.acc_id_col(0) & "='" & accountId.ToString & "'")
                    tmpAccount.LastLogin = TryInt(_tempResult).ToDate
                    LogAppend(
                        "Loaded account last login info for accountId: " & accountId.ToString & " and setId: " & tarSetId &
                        " // result is: " & _tempResult, "AccountHandler_LoadAccount", False)
                    'todo Expansion!
                    Dim templevel As Integer
                    Select Case _tempResult
                        Case "AZ"
                            templevel = 4
                        Case "A"
                            templevel = 3
                        Case "0"
                            templevel = 0
                        Case Else
                            templevel = 0
                    End Select
                    tmpAccount.GmLevel = templevel
                    tmpAccount.RealmId = 0 'TODO multi realm support
                    tmpAccount.SetIndex = tarSetId
                    AddAccountSet(tarSetId, tmpAccount)
                Case "trinity"
                    _tempResult =
                        runSQLCommand_realm_string(
                                    "SELECT " & GlobalVariables.sourceStructure.acc_name_col(0) & " FROM " &
                                    GlobalVariables.sourceStructure.account_tbl(0) & " WHERE " &
                                    GlobalVariables.sourceStructure.acc_id_col(0) & "='" & accountId.ToString & "'")
                    Dim tmpAccount As New Account(_tempResult, accountId)
                    tmpAccount.SourceExpansion = GlobalVariables.sourceExpansion
                    tmpAccount.Core = GlobalVariables.sourceCore
                    LogAppend(
                        "Loaded account name info for accountId: " & accountId.ToString & " and setId: " & tarSetId &
                        " // result is: " & _tempResult, "AccountHandler_LoadAccount", True)
                    _tempResult =
                        runSQLCommand_realm_string(
                            "SELECT " & GlobalVariables.sourceStructure.acc_passHash_col(0) & " FROM " &
                            GlobalVariables.sourceStructure.account_tbl(0) & " WHERE " &
                            GlobalVariables.sourceStructure.acc_id_col(0) & "='" & accountId.ToString & "'")
                    tmpAccount.PassHash = _tempResult
                    LogAppend(
                        "Loaded account passHash info for accountId: " & accountId.ToString & " and setId: " & tarSetId &
                        " // result is: " & _tempResult, "AccountHandler_LoadAccount", False)
                    _tempResult =
                        runSQLCommand_realm_string(
                            "SELECT " & GlobalVariables.sourceStructure.acc_sessionkey_col(0) & " FROM " &
                            GlobalVariables.sourceStructure.account_tbl(0) & " WHERE " &
                            GlobalVariables.sourceStructure.acc_id_col(0) & "='" & accountId.ToString & "'")
                    tmpAccount.SessionKey = _tempResult
                    LogAppend(
                        "Loaded account sessionkey info for accountId: " & accountId.ToString & " and setId: " & tarSetId &
                        " // result is: " & _tempResult, "AccountHandler_LoadAccount", False)
                    _tempResult =
                        runSQLCommand_realm_string(
                            "SELECT " & GlobalVariables.sourceStructure.acc_locale_col(0) & " FROM " &
                            GlobalVariables.sourceStructure.account_tbl(0) & " WHERE " &
                            GlobalVariables.sourceStructure.acc_id_col(0) & "='" & accountId.ToString & "'")
                    tmpAccount.Locale = TryInt(_tempResult)
                    LogAppend(
                        "Loaded account locale info for accountId: " & accountId.ToString & " and setId: " & tarSetId &
                        " // result is: " & _tempResult, "AccountHandler_LoadAccount", False)
                    _tempResult =
                        runSQLCommand_realm_string(
                            "SELECT " & GlobalVariables.sourceStructure.acc_joindate_col(0) & " FROM " &
                            GlobalVariables.sourceStructure.account_tbl(0) & " WHERE " &
                            GlobalVariables.sourceStructure.acc_id_col(0) & "='" & accountId.ToString & "'")
                    tmpAccount.JoinDate = TryInt(_tempResult)
                    LogAppend(
                        "Loaded account joindate info for accountId: " & accountId.ToString & " and setId: " & tarSetId &
                        " // result is: " & _tempResult, "AccountHandler_LoadAccount", False)
                    _tempResult =
                       runSQLCommand_realm_string(
                           "SELECT " & GlobalVariables.sourceStructure.acc_email_col(0) & " FROM " &
                           GlobalVariables.sourceStructure.account_tbl(0) & " WHERE " &
                           GlobalVariables.sourceStructure.acc_id_col(0) & "='" & accountId.ToString & "'")
                    tmpAccount.Email = _tempResult
                    LogAppend(
                        "Loaded account email for accountId: " & accountId.ToString & " and setId: " & tarSetId &
                        " // result is: " & _tempResult, "AccountHandler_LoadAccount", False)
                    _tempResult =
                       runSQLCommand_realm_string(
                           "SELECT " & GlobalVariables.sourceStructure.acc_lastlogin_col(0) & " FROM " &
                           GlobalVariables.sourceStructure.account_tbl(0) & " WHERE " &
                           GlobalVariables.sourceStructure.acc_id_col(0) & "='" & accountId.ToString & "'")
                    tmpAccount.LastLogin = TryInt(_tempResult).ToDate
                    LogAppend(
                        "Loaded account last login info for accountId: " & accountId.ToString & " and setId: " & tarSetId &
                        " // result is: " & _tempResult, "AccountHandler_LoadAccount", False)
                    'todo Expansion!
                    'Account Access Table
                    _tempResult =
                        runSQLCommand_realm_string(
                            "SELECT " & GlobalVariables.sourceStructure.accAcc_gmLevel_col(0) & " FROM " &
                            GlobalVariables.sourceStructure.accountAccess_tbl(0) & " WHERE " &
                            GlobalVariables.sourceStructure.accAcc_accid_col(0) & "='" & accountId.ToString & "'")
                    tmpAccount.GmLevel = TryInt(_tempResult)
                    LogAppend(
                        "Loaded accountAccess gmlevel info for accountId: " & accountId.ToString & " and setId: " & tarSetId &
                        " // result is: " & _tempResult, "AccountHandler_LoadAccount", False)
                    _tempResult =
                        runSQLCommand_realm_string(
                            "SELECT " & GlobalVariables.sourceStructure.accAcc_realmId_col(0) & " FROM " &
                            GlobalVariables.sourceStructure.accountAccess_tbl(0) & " WHERE " &
                            GlobalVariables.sourceStructure.accAcc_accid_col(0) & "='" & accountId.ToString & "'")
                    tmpAccount.RealmId = TryInt(_tempResult)
                    LogAppend(
                        "Loaded accountAccess realmId info for accountId: " & accountId.ToString & " and setId: " & tarSetId &
                        " // result is: " & _tempResult, "AccountHandler_LoadAccount", False)
                    tmpAccount.SetIndex = tarSetId
                    AddAccountSet(tarSetId, tmpAccount)
                Case "trinitytbc"
                    _tempResult =
                        runSQLCommand_realm_string(
                            "SELECT " & GlobalVariables.sourceStructure.acc_name_col(0) & " FROM " &
                            GlobalVariables.sourceStructure.account_tbl(0) & " WHERE " &
                            GlobalVariables.sourceStructure.acc_id_col(0) & "='" & accountId.ToString & "'")
                    Dim tmpAccount As New Account(_tempResult, accountId)
                    tmpAccount.SourceExpansion = GlobalVariables.sourceExpansion
                    tmpAccount.Core = GlobalVariables.sourceCore
                    LogAppend(
                        "Loaded account name info for accountId: " & accountId.ToString & " and setId: " & tarSetId &
                        " // result is: " & _tempResult, "AccountHandler_LoadAccount", True)
                    _tempResult =
                        runSQLCommand_realm_string(
                            "SELECT " & GlobalVariables.sourceStructure.acc_passHash_col(0) & " FROM " &
                            GlobalVariables.sourceStructure.account_tbl(0) & " WHERE " &
                            GlobalVariables.sourceStructure.acc_id_col(0) & "='" & accountId.ToString & "'")
                    tmpAccount.PassHash = _tempResult
                    LogAppend(
                        "Loaded account passHash info for accountId: " & accountId.ToString & " and setId: " & tarSetId &
                        " // result is: " & _tempResult, "AccountHandler_LoadAccount", False)
                    _tempResult =
                        runSQLCommand_realm_string(
                            "SELECT " & GlobalVariables.sourceStructure.acc_sessionkey_col(0) & " FROM " &
                            GlobalVariables.sourceStructure.account_tbl(0) & " WHERE " &
                            GlobalVariables.sourceStructure.acc_id_col(0) & "='" & accountId.ToString & "'")
                    tmpAccount.SessionKey = _tempResult
                    LogAppend(
                        "Loaded account sessionkey info for accountId: " & accountId.ToString & " and setId: " & tarSetId &
                        " // result is: " & _tempResult, "AccountHandler_LoadAccount", False)
                    _tempResult =
                        runSQLCommand_realm_string(
                            "SELECT " & GlobalVariables.sourceStructure.acc_locale_col(0) & " FROM " &
                            GlobalVariables.sourceStructure.account_tbl(0) & " WHERE " &
                            GlobalVariables.sourceStructure.acc_id_col(0) & "='" & accountId.ToString & "'")
                    tmpAccount.Locale = TryInt(_tempResult)
                    LogAppend(
                        "Loaded account locale info for accountId: " & accountId.ToString & " and setId: " & tarSetId &
                        " // result is: " & _tempResult, "AccountHandler_LoadAccount", False)
                    _tempResult =
                        runSQLCommand_realm_string(
                            "SELECT " & GlobalVariables.sourceStructure.acc_joindate_col(0) & " FROM " &
                            GlobalVariables.sourceStructure.account_tbl(0) & " WHERE " &
                            GlobalVariables.sourceStructure.acc_id_col(0) & "='" & accountId.ToString & "'")
                    tmpAccount.JoinDate = TryInt(_tempResult)
                    LogAppend(
                        "Loaded account joindate info for accountId: " & accountId.ToString & " and setId: " & tarSetId &
                        " // result is: " & _tempResult, "AccountHandler_LoadAccount", False)
                    _tempResult =
                        runSQLCommand_realm_string(
                            "SELECT " & GlobalVariables.sourceStructure.acc_v_col(0) & " FROM " &
                            GlobalVariables.sourceStructure.account_tbl(0) & " WHERE " &
                            GlobalVariables.sourceStructure.acc_id_col(0) & "='" & accountId.ToString & "'")
                    tmpAccount.V = _tempResult
                    LogAppend(
                        "Loaded account v info for accountId: " & accountId.ToString & " and setId: " & tarSetId &
                        " // result is: " & _tempResult, "AccountHandler_LoadAccount", False)
                    _tempResult =
                        runSQLCommand_realm_string(
                            "SELECT " & GlobalVariables.sourceStructure.acc_s_col(0) & " FROM " &
                            GlobalVariables.sourceStructure.account_tbl(0) & " WHERE " &
                            GlobalVariables.sourceStructure.acc_id_col(0) & "='" & accountId.ToString & "'")
                    tmpAccount.S = _tempResult
                    LogAppend(
                        "Loaded account s info for accountId: " & accountId.ToString & " and setId: " & tarSetId &
                        " // result is: " & _tempResult, "AccountHandler_LoadAccount", False)
                    _tempResult =
                     runSQLCommand_realm_string(
                         "SELECT " & GlobalVariables.sourceStructure.acc_email_col(0) & " FROM " &
                         GlobalVariables.sourceStructure.account_tbl(0) & " WHERE " &
                         GlobalVariables.sourceStructure.acc_id_col(0) & "='" & accountId.ToString & "'")
                    tmpAccount.Email = _tempResult
                    LogAppend(
                        "Loaded account email for accountId: " & accountId.ToString & " and setId: " & tarSetId &
                        " // result is: " & _tempResult, "AccountHandler_LoadAccount", False)
                    _tempResult =
                       runSQLCommand_realm_string(
                           "SELECT " & GlobalVariables.sourceStructure.acc_lastlogin_col(0) & " FROM " &
                           GlobalVariables.sourceStructure.account_tbl(0) & " WHERE " &
                           GlobalVariables.sourceStructure.acc_id_col(0) & "='" & accountId.ToString & "'")
                    tmpAccount.LastLogin = TryInt(_tempResult).ToDate
                    LogAppend(
                        "Loaded account last login info for accountId: " & accountId.ToString & " and setId: " & tarSetId &
                        " // result is: " & _tempResult, "AccountHandler_LoadAccount", False)
                    'todo Expansion!
                    'Account Access Table
                    _tempResult =
                        runSQLCommand_realm_string(
                            "SELECT " & GlobalVariables.sourceStructure.accAcc_gmLevel_col(0) & " FROM " &
                            GlobalVariables.sourceStructure.accountAccess_tbl(0) & " WHERE " &
                            GlobalVariables.sourceStructure.accAcc_accid_col(0) & "='" & accountId.ToString & "'")
                    tmpAccount.GmLevel = TryInt(_tempResult)
                    LogAppend(
                        "Loaded accountAccess gmlevel info for accountId: " & accountId.ToString & " and setId: " & tarSetId &
                        " // result is: " & _tempResult, "AccountHandler_LoadAccount", False)
                    _tempResult =
                        runSQLCommand_realm_string(
                            "SELECT " & GlobalVariables.sourceStructure.accAcc_realmId_col(0) & " FROM " &
                            GlobalVariables.sourceStructure.accountAccess_tbl(0) & " WHERE " &
                            GlobalVariables.sourceStructure.accAcc_accid_col(0) & "='" & accountId.ToString & "'")
                    tmpAccount.RealmId = TryInt(_tempResult)
                    LogAppend(
                        "Loaded accountAccess realmId info for accountId: " & accountId.ToString & " and setId: " & tarSetId &
                        " // result is: " & _tempResult, "AccountHandler_LoadAccount", False)
                    tmpAccount.SetIndex = tarSetId
                    AddAccountSet(tarSetId, tmpAccount)

                Case "mangos"
                    'Account Table
                    _tempResult =
                        runSQLCommand_realm_string(
                            "SELECT " & GlobalVariables.sourceStructure.acc_name_col(0) & " FROM " &
                            GlobalVariables.sourceStructure.account_tbl(0) & " WHERE " &
                            GlobalVariables.sourceStructure.acc_id_col(0) & "='" & accountId.ToString & "'")
                    Dim tmpAccount As New Account(_tempResult, accountId)
                    tmpAccount.SourceExpansion = GlobalVariables.sourceExpansion
                    tmpAccount.Core = GlobalVariables.sourceCore
                    LogAppend(
                        "Loaded account name info for accountId: " & accountId.ToString & " and setId: " & tarSetId &
                        " // result is: " & _tempResult, "AccountHandler_LoadAccount", True)
                    _tempResult =
                        runSQLCommand_realm_string(
                            "SELECT " & GlobalVariables.sourceStructure.acc_passHash_col(0) & " FROM " &
                            GlobalVariables.sourceStructure.account_tbl(0) & " WHERE " &
                            GlobalVariables.sourceStructure.acc_id_col(0) & "='" & accountId.ToString & "'")
                    tmpAccount.PassHash = _tempResult
                    LogAppend(
                        "Loaded account passHash info for accountId: " & accountId.ToString & " and setId: " & tarSetId &
                        " // result is: " & _tempResult, "AccountHandler_LoadAccount", False)
                    _tempResult =
                        runSQLCommand_realm_string(
                            "SELECT " & GlobalVariables.sourceStructure.acc_v_col(0) & " FROM " &
                            GlobalVariables.sourceStructure.account_tbl(0) & " WHERE " &
                            GlobalVariables.sourceStructure.acc_id_col(0) & "='" & accountId.ToString & "'")
                    tmpAccount.V = _tempResult
                    LogAppend(
                        "Loaded account v info for accountId: " & accountId.ToString & " and setId: " & tarSetId &
                        " // result is: " & _tempResult, "AccountHandler_LoadAccount", False)
                    _tempResult =
                        runSQLCommand_realm_string(
                            "SELECT " & GlobalVariables.sourceStructure.acc_s_col(0) & " FROM " &
                            GlobalVariables.sourceStructure.account_tbl(0) & " WHERE " &
                            GlobalVariables.sourceStructure.acc_id_col(0) & "='" & accountId.ToString & "'")
                    tmpAccount.S = _tempResult
                    LogAppend(
                        "Loaded account s info for accountId: " & accountId.ToString & " and setId: " & tarSetId &
                        " // result is: " & _tempResult, "AccountHandler_LoadAccount", False)
                    _tempResult =
                        runSQLCommand_realm_string(
                            "SELECT " & GlobalVariables.sourceStructure.acc_sessionkey_col(0) & " FROM " &
                            GlobalVariables.sourceStructure.account_tbl(0) & " WHERE " &
                            GlobalVariables.sourceStructure.acc_id_col(0) & "='" & accountId.ToString & "'")
                    tmpAccount.SessionKey = _tempResult
                    LogAppend(
                        "Loaded account sessionkey info for accountId: " & accountId.ToString & " and setId: " & tarSetId &
                        " // result is: " & _tempResult, "AccountHandler_LoadAccount", False)
                    _tempResult =
                        runSQLCommand_realm_string(
                            "SELECT " & GlobalVariables.sourceStructure.acc_locale_col(0) & " FROM " &
                            GlobalVariables.sourceStructure.account_tbl(0) & " WHERE " &
                            GlobalVariables.sourceStructure.acc_id_col(0) & "='" & accountId.ToString & "'")
                    tmpAccount.Locale = TryInt(_tempResult)
                    LogAppend(
                        "Loaded account locale info for accountId: " & accountId.ToString & " and setId: " & tarSetId &
                        " // result is: " & _tempResult, "AccountHandler_LoadAccount", False)
                    _tempResult =
                        runSQLCommand_realm_string(
                            "SELECT " & GlobalVariables.sourceStructure.acc_joindate_col(0) & " FROM " &
                            GlobalVariables.sourceStructure.account_tbl(0) & " WHERE " &
                            GlobalVariables.sourceStructure.acc_id_col(0) & "='" & accountId.ToString & "'")
                    tmpAccount.JoinDate = TryInt(_tempResult)
                    LogAppend(
                        "Loaded account joindate info for accountId: " & accountId.ToString & " and setId: " & tarSetId &
                        " // result is: " & _tempResult, "AccountHandler_LoadAccount", False)
                    _tempResult =
                        runSQLCommand_realm_string(
                            "SELECT " & GlobalVariables.sourceStructure.accAcc_gmLevel_col(0) & " FROM " &
                            GlobalVariables.sourceStructure.account_tbl(0) & " WHERE " &
                            GlobalVariables.sourceStructure.acc_id_col(0) & "='" & accountId.ToString & "'")
                    tmpAccount.GmLevel = TryInt(_tempResult)
                    LogAppend(
                        "Loaded account gmlevel info for accountId: " & accountId.ToString & " and setId: " & tarSetId &
                        " // result is: " & _tempResult, "AccountHandler_LoadAccount", False)
                    _tempResult =
                        runSQLCommand_realm_string(
                            "SELECT " & GlobalVariables.sourceStructure.accAcc_realmId_col(0) & " FROM " &
                            GlobalVariables.sourceStructure.account_tbl(0) & " WHERE " &
                            GlobalVariables.sourceStructure.acc_id_col(0) & "='" & accountId.ToString & "'")
                    tmpAccount.RealmId = TryInt(_tempResult)
                    LogAppend(
                        "Loaded account realmId info for accountId: " & accountId.ToString & " and setId: " & tarSetId &
                        " // result is: " & _tempResult, "AccountHandler_LoadAccount", False)
                    _tempResult =
                     runSQLCommand_realm_string(
                         "SELECT " & GlobalVariables.sourceStructure.acc_email_col(0) & " FROM " &
                         GlobalVariables.sourceStructure.account_tbl(0) & " WHERE " &
                         GlobalVariables.sourceStructure.acc_id_col(0) & "='" & accountId.ToString & "'")
                    tmpAccount.Email = _tempResult
                    LogAppend(
                        "Loaded account email for accountId: " & accountId.ToString & " and setId: " & tarSetId &
                        " // result is: " & _tempResult, "AccountHandler_LoadAccount", False)
                    _tempResult =
                       runSQLCommand_realm_string(
                           "SELECT " & GlobalVariables.sourceStructure.acc_lastlogin_col(0) & " FROM " &
                           GlobalVariables.sourceStructure.account_tbl(0) & " WHERE " &
                           GlobalVariables.sourceStructure.acc_id_col(0) & "='" & accountId.ToString & "'")
                    tmpAccount.LastLogin = TryInt(_tempResult).ToDate
                    LogAppend(
                        "Loaded account last login info for accountId: " & accountId.ToString & " and setId: " & tarSetId &
                        " // result is: " & _tempResult, "AccountHandler_LoadAccount", False)
                    'todo Expansion!
                    tmpAccount.SetIndex = tarSetId
                    AddAccountSet(tarSetId, tmpAccount)
            End Select
        End Sub
    End Class
End Namespace