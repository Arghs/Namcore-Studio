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
'*      /Filename:      CommandHandler
'*      /Description:   Handles and logs MySQL commands and exceptions
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Imports MySql.Data.MySqlClient
Imports Namcore_Studio_Framework.EventLogging
Imports Namcore_Studio_Framework.GlobalVariables
Imports Namcore_Studio_Framework.Conversions
Public Module CommandHandler
    Public Function runSQLCommand_characters_string(ByVal command As String, Optional useTargetConnection As Boolean = False) As String
        LogAppend("Executing new MySQL command. Command is: " & command, "CommandHandler_runSQLCommand_characters_string", False)
        Dim conn As MySqlConnection
        If forceTargetConnectionUsage Then useTargetConnection = True
        If useTargetConnection = False Then
            conn = GlobalConnection
        Else
            conn = TargetConnection
        End If
        Dim da As New MySqlDataAdapter(command, conn)
        Dim dt As New DataTable
        Try
            da.Fill(dt)
            Dim lastcount As Integer = dt.Rows.Count
            If Not lastcount = 0 Then
                LogAppend("Results: " & lastcount.ToString(), "CommandHandler_runSQLCommand_characters_string", False)
                Dim readed As String = (dt.Rows(0).Item(0)).ToString
                If readed = "DBnull" Then
                    LogAppend("Readed DBnull -> returning nothing", "CommandHandler_runSQLCommand_characters_string", False)
                    Return ""
                Else
                    LogAppend("Result is: " & readed, "CommandHandler_runSQLCommand_characters_string", False)
                    Return readed
                End If
            Else
                LogAppend("0 Results -> returning nothing", "CommandHandler_runSQLCommand_characters_string", False)
                Return ""
            End If
        Catch ex As MySqlException
            LogAppend("MySQL query has not been executed! -> Returning nothing -> Exception is: ###START###" & ex.ToString() & "###END###", "CommandHandler_runSQLCommand_characters_string", True, True)
            Return ""
        End Try
    End Function
    Public Function runSQLCommand_realm_string(ByVal command As String, Optional useTargetConnection As Boolean = False) As String
        LogAppend("Executing new MySQL command. Command is: " & command, "CommandHandler_runSQLCommand_realm_string", False)
        Dim conn As MySqlConnection
        If forceTargetConnectionUsage Then useTargetConnection = True
        If useTargetConnection = False Then
            conn = GlobalConnection_Realm
        Else
            conn = TargetConnection_Realm
        End If
        Dim da As New MySqlDataAdapter(command, conn)
        Dim dt As New DataTable
        Try
            da.Fill(dt)
            Dim lastcount As Integer = dt.Rows.Count
            If Not lastcount = 0 Then
                LogAppend("Results: " & lastcount.ToString(), "CommandHandler_runSQLCommand_realm_string", False)
                Dim readed As String = (dt.Rows(0).Item(0)).ToString
                If readed = "DBnull" Then
                    LogAppend("Readed DBnull -> returning nothing", "CommandHandler_runSQLCommand_realm_string", False)
                    Return ""
                Else
                    LogAppend("Result is: " & readed, "CommandHandler_runSQLCommand_realm_string", False)
                    Return readed
                End If
            Else
                LogAppend("0 Results -> returning nothing", "CommandHandler_runSQLCommand_realm_string", False)
                Return ""
            End If
        Catch ex As MySqlException
            LogAppend("MySQL query has not been executed! -> Returning nothing -> Exception is: ###START###" & ex.ToString() & "###END###", "CommandHandler_runSQLCommand_realm_string", True, True)
            Return ""
        End Try
    End Function
    Public Function runSQLCommand_characters_string_setconn(ByVal command As String, ByVal TargetConnection As MySqlConnection) As String
        LogAppend("Executing new MySQL command. Command is: " & command, "CommandHandler_runSQLCommand_characters_string", False)
        Dim conn As MySqlConnection
        conn = TargetConnection
        Dim da As New MySqlDataAdapter(command, conn)
        Dim dt As New DataTable
        Try
            da.Fill(dt)
            Dim lastcount As Integer = dt.Rows.Count
            If Not lastcount = 0 Then
                LogAppend("Results: " & lastcount.ToString(), "CommandHandler_runSQLCommand_characters_string", False)
                Dim readed As String = (dt.Rows(0).Item(0)).ToString
                If readed = "DBnull" Then
                    LogAppend("Readed DBnull -> returning nothing", "CommandHandler_runSQLCommand_characters_string", False)
                    Return ""
                Else
                    LogAppend("Result is: " & readed, "CommandHandler_runSQLCommand_characters_string", False)
                    Return readed
                End If
            Else
                LogAppend("0 Results -> returning nothing", "CommandHandler_runSQLCommand_characters_string", False)
                Return ""
            End If
        Catch ex As MySqlException
            LogAppend("MySQL query has not been executed! -> Returning nothing -> Exception is: ###START###" & ex.ToString() & "###END###", "CommandHandler_runSQLCommand_characters_string", True, True)
            Return ""
        End Try
    End Function
    Public Function runSQLCommand_realm_string_setconn(ByVal command As String, ByVal TargetConnection As MySqlConnection) As String
        LogAppend("Executing new MySQL command. Command is: " & command, "CommandHandler_runSQLCommand_realm_string", False)
        Dim conn As MySqlConnection
        conn = TargetConnection_Realm
        Dim da As New MySqlDataAdapter(command, conn)
        Dim dt As New DataTable
        Try
            da.Fill(dt)
            Dim lastcount As Integer = dt.Rows.Count
            If Not lastcount = 0 Then
                LogAppend("Results: " & lastcount.ToString(), "CommandHandler_runSQLCommand_realm_string", False)
                Dim readed As String = (dt.Rows(0).Item(0)).ToString
                If readed = "DBnull" Then
                    LogAppend("Readed DBnull -> returning nothing", "CommandHandler_runSQLCommand_realm_string", False)
                    Return ""
                Else
                    LogAppend("Result is: " & readed, "CommandHandler_runSQLCommand_realm_string", False)
                    Return readed
                End If
            Else
                LogAppend("0 Results -> returning nothing", "CommandHandler_runSQLCommand_realm_string", False)
                Return ""
            End If
        Catch ex As MySqlException
            LogAppend("MySQL query has not been executed! -> Returning nothing -> Exception is: ###START###" & ex.ToString() & "###END###", "CommandHandler_runSQLCommand_realm_string", True, True)
            Return ""
        End Try
    End Function
    Public Function ReturnDataTable(ByVal command As String, Optional useTargetConnection As Boolean = False) As DataTable
        LogAppend("Executing new MySQL command. Command is: " & command, "CommandHandler_ReturnDataTable", False)
        Dim conn As MySqlConnection
        If forceTargetConnectionUsage Then useTargetConnection = True
        If useTargetConnection = False Then
            conn = GlobalConnection
        Else
            conn = TargetConnection
        End If
        Dim da As New MySqlDataAdapter(command, conn)
        Dim dt As New DataTable
        Try
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogAppend("Failed to fill DataTable! -> Returning nothing -> Exception is: ###START###" & ex.ToString() & "###END###", "CommandHandler_ReturnDataTable", True, True)
            Return dt
        End Try
    End Function
    Public Function ReturnDataTable_setconn(ByVal command As String, ByVal TargetConnection As MySqlConnection) As DataTable
        LogAppend("Executing new MySQL command. Command is: " & command, "CommandHandler_ReturnDataTable", False)
        Dim conn As MySqlConnection
        conn = TargetConnection
        Dim da As New MySqlDataAdapter(command, conn)
        Dim dt As New DataTable
        Try
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogAppend("Failed to fill DataTable! -> Returning nothing -> Exception is: ###START###" & ex.ToString() & "###END###", "CommandHandler_ReturnDataTable", True, True)
            Return dt
        End Try
    End Function
    Public Function ReturnCountResults(ByVal command As String, Optional useTargetConnection As Boolean = False) As Integer
        LogAppend("Executing new MySQL command. Command is: " & command, "CommandHandler_ReturnCountResult", False)
        Dim conn As MySqlConnection
        If forceTargetConnectionUsage Then useTargetConnection = True
        If useTargetConnection = False Then
            conn = GlobalConnection
        Else
            conn = TargetConnection
        End If
        Dim da As New MySqlDataAdapter(command, conn)
        Dim dt As New DataTable
        Try
            da.Fill(dt)
            Dim lastcount As Integer = dt.Rows.Count
            Return lastcount
        Catch ex As Exception
            LogAppend("Failed to fill DataTable! -> Returning nothing -> Exception is: ###START###" & ex.ToString() & "###END###", "CommandHandler_ReturnCountResult", True, True)
            Return 0
        End Try
    End Function
    Public Function ReturnResultWithRow(ByVal command As String, ByVal spalte As String, ByVal row As Integer, Optional useTargetConnection As Boolean = False) As String
        LogAppend("Executing new MySQL command. Column is: " & spalte & " / Row is: " & row.ToString & " / Command is: " & command, "CommandHandler_ReturnResultWithRow", False)
        Dim conn As MySqlConnection
        If forceTargetConnectionUsage Then useTargetConnection = True
        If useTargetConnection = False Then
            conn = GlobalConnection
        Else
            conn = TargetConnection
        End If
        Dim da As New MySqlDataAdapter(command, conn)
        Dim dt As New DataTable
        Try
            da.Fill(dt)
            Dim lastcount As Integer = dt.Rows.Count
            If Not lastcount = 0 Then
                Dim readed As String = (dt.Rows(row).Item(0)).ToString
                If readed = "DBnull" Then
                    Return ""
                Else
                    Return readed
                End If
            Else
                Return ""
            End If
        Catch ex As Exception
            LogAppend("Failed to fill DataTable! -> Returning nothing -> Exception is: ###START###" & ex.ToString() & "###END###", "CommandHandler_ReturnResultWithRow", True, True)
            Return ""
        End Try
    End Function
    Public Function ReturnResultCount(ByVal command As String, Optional useTargetConnection As Boolean = False) As Integer
        LogAppend("Executing new MySQL command.", "CommandHandler_ReturnResultCount", False)
        Dim conn As MySqlConnection
        If forceTargetConnectionUsage Then useTargetConnection = True
        If useTargetConnection = False Then
            conn = GlobalConnection
        Else
            conn = TargetConnection
        End If
        Dim da As New MySqlDataAdapter(command, conn)
        Dim dt As New DataTable
        Try
            da.Fill(dt)
            Return dt.Rows.Count
        Catch ex As Exception
            LogAppend("Failed to fill DataTable! -> Returning nothing -> Exception is: ###START###" & ex.ToString() & "###END###", "CommandHandler_ReturnResultCount", True, True)
            Return 0
        End Try
    End Function
End Module
