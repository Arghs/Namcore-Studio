'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013-2015 NamCore Studio <https://github.com/megasus/Namcore-Studio>
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
'*      /Filename:      UpdateAccountHandler
'*      /Description:   Handles Account update requests
'++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.Database
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Modules

Namespace Framework.Core.Update
    Public Class UpdateAccountHandler
        Public Sub UpdateAccount(ByVal comparePlayer As Account, ByVal newPlayer As Account)
            LogAppend("Updating Account " & comparePlayer.Name, "UpdateAccountHandler_UpdateAccount", True)
            If GlobalVariables.GlobalConnection.State = ConnectionState.Closed Then _
                GlobalVariables.GlobalConnection.Open()
            If GlobalVariables.GlobalConnection_Realm.State = ConnectionState.Closed Then _
                GlobalVariables.GlobalConnection_Realm.Open()
            GlobalVariables.forceTargetConnectionUsage = False
            Try
                If Not newPlayer.Email = comparePlayer.Email Then
                    '// Email changed
                    runSQLCommand_realm_string(
                        "UPDATE `" & GlobalVariables.sourceStructure.account_tbl(0) & "` SET `" &
                        GlobalVariables.sourceStructure.acc_email_col(0) &
                        "`='" & newPlayer.Email & "' WHERE `" & GlobalVariables.sourceStructure.acc_id_col(0) &
                        "`='" & newPlayer.Id.ToString() & "'")
                End If
                If Not newPlayer.Expansion = comparePlayer.Expansion Then
                    '// Expansion changed
                    runSQLCommand_realm_string(
                        "UPDATE `" & GlobalVariables.sourceStructure.account_tbl(0) & "` SET `" &
                        GlobalVariables.sourceStructure.acc_expansion_col(0) &
                        "`='" & newPlayer.Expansion.ToString() & "' WHERE `" &
                        GlobalVariables.sourceStructure.acc_id_col(0) &
                        "`='" & newPlayer.Id.ToString() & "'")
                End If
                If Not newPlayer.Locked = comparePlayer.Locked Then
                    '// Locked status changed
                    runSQLCommand_realm_string(
                        "UPDATE `" & GlobalVariables.sourceStructure.account_tbl(0) & "` SET `" &
                        GlobalVariables.sourceStructure.acc_locked_col(0) &
                        "`='" & newPlayer.Locked.ToString() & "' WHERE `" &
                        GlobalVariables.sourceStructure.acc_id_col(0) &
                        "`='" & newPlayer.Id.ToString() & "'")
                End If
            Catch ex As Exception
                LogAppend("Exception occured: " & ex.ToString(), "UpdateAccountHandler_UpdateAccount", True, True)
            End Try
        End Sub
    End Class
End Namespace