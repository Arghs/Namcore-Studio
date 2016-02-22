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
'*      /Filename:      CommandHandler
'*      /Description:   Handles and logs MySQL commands and exceptions
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.ComponentModel
Imports MySql.Data.MySqlClient
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Modules

Namespace Framework.Database
    <Localizable(False)>
    Public Module CommandHandler
        '// Declaration
        Public TempTable1Name As String
        Public TempTable1 As DataTable
        Public TempTable2Name As String
        Public TempTable2 As DataTable
        '// Declaration
        Public Function runSQLCommand_characters_string(command As String,
                                                        Optional useTargetConnection As Boolean = False) As String
            If Not command.Contains("SELECT MAX(") AndAlso command.ToLower.StartsWith("select") Then
                Dim table As String = SplitString(command.ToLower(), "from ", " where")
                table = table.Replace("`", "")
                LogAppend("Table is: " & table, "CommandHandler_runSQLCommand_characters_string")
                If Not table = TempTable1Name Then
                    TempTable1Name = table
                    TempTable1 = ReturnDataTable("SELECT * FROM `" & table & "`", useTargetConnection)
                End If
                Dim column As String = SplitString(command.ToLower(), "select ", " from")
                column = column.Replace("`", "")
                Dim args As String()
                args = command.Split(" "c)
                Dim newcommand = ""
                For i = 0 To args.Length - 1
                    If args(i) = "WHERE" Then
                        While i < args.Length - 1
                            i += 1
                            Dim tmpStr As String = args(i)
                            If tmpStr.ToLower() = "and" Then tmpStr = " " & tmpStr & " "
                            newcommand &= tmpStr
                        End While
                        Exit For
                    End If
                Next i
                newcommand = newcommand.Replace("`", "")
                Dim foundRows() As DataRow
                Try
                    foundRows = TempTable1.Select(newcommand)
                    LogAppend("New command/column: " & newcommand & " / " & column,
                              "CommandHandler_runSQLCommand_characters_string")
                    If foundRows.Length = 0 Then
                        LogAppend("0 Results -> returning nothing", "CommandHandler_runSQLCommand_characters_string",
                                  False)
                        Return ""
                    Else
                        Dim result = foundRows(0).Item(column)
                        If IsDBNull(result) Then
                            LogAppend("Readed DBnull -> returning nothing",
                                      "CommandHandler_runSQLCommand_characters_string",
                                      False)
                            Return ""
                        Else
                            Return result.ToString()
                        End If
                    End If
                Catch ex As Exception
                    LogAppend(
                        "Exception occured: ###START###" &
                        ex.ToString() &
                        "###END###", "CommandHandler_runSQLCommand_characters_string", True, True)
                    Return ""
                End Try
            Else
                Dim useConn As MySqlConnection = GlobalVariables.GlobalConnection
                If useTargetConnection Then useConn = GlobalVariables.TargetConnection
                If GlobalVariables.forceTargetConnectionUsage Then useConn = GlobalVariables.TargetConnection
                Return RunUsualCommand(command, useConn)
            End If
        End Function

        Private Function RunUsualCommand(command As String, conn As MySqlConnection) As String
            LogAppend("Executing new MySQL command. Command is: " & command,
                      "CommandHandler_RunUsualCommand", False)
            Dim da As New MySqlDataAdapter(command, conn)
            Dim dt As New DataTable
            Try
                da.Fill(dt)
                Dim lastcount As Integer = dt.Rows.Count
                If Not lastcount = 0 Then
                    LogAppend("Results: " & lastcount.ToString(), "CommandHandler_RunUsualCommand",
                              False)
                    Dim readed As String = (dt.Rows(0).Item(0)).ToString
                    If readed = "DBnull" Then
                        LogAppend("Readed DBnull -> returning nothing", "CommandHandler_RunUsualCommand",
                                  False)
                        Return ""
                    Else
                        LogAppend("Result is: " & readed, "CommandHandler_RunUsualCommand", False)
                        Return readed
                    End If
                Else
                    LogAppend("0 Results -> returning nothing", "CommandHandler_RunUsualCommand", False)
                    Return ""
                End If
            Catch ex As MySqlException
                LogAppend(
                    "MySQL query has not been executed! -> Returning nothing -> Exception is: ###START###" &
                    ex.ToString() &
                    "###END###", "CommandHandler_RunUsualCommand", True, True)
                Return ""
            End Try
        End Function

        Public Function runSQLCommand_realm_string(command As String,
                                                   Optional useTargetConnection As Boolean = False) _
            As String
            If Not command.Contains("SELECT MAX(") AndAlso command.ToLower.StartsWith("select") Then
                Dim table As String = SplitString(command.ToLower(), "from ", " where")
                table = table.Replace("`", "")
                LogAppend("Table is: " & table, "CommandHandler_runSQLCommand_realm_string")
                If Not table = TempTable2Name Then
                    TempTable2Name = table
                    TempTable2 = ReturnDataTableRealm("SELECT * FROM `" & table & "`", useTargetConnection)
                End If
                Dim column As String = SplitString(command.ToLower(), "select ", " from")
                column = column.Replace("`", "")
                Dim args As String()
                args = command.Split(" "c)
                Dim newcommand = ""
                For i = 0 To args.Length - 1
                    If args(i) = "WHERE" Then
                        While i < args.Length - 1
                            i += 1
                            Dim tmpStr As String = args(i)
                            If tmpStr.ToLower() = "and" Then tmpStr = " " & tmpStr & " "
                            newcommand &= tmpStr
                        End While
                        Exit For
                    End If
                Next i
                newcommand = newcommand.Replace("`", "")
                Dim foundRows() As DataRow
                Try
                    foundRows = TempTable2.Select(newcommand)
                    LogAppend("New command/column: " & newcommand & " / " & column,
                              "CommandHandler_runSQLCommand_realm_string")
                    If foundRows.Length = 0 Then
                        LogAppend("0 Results -> returning nothing", "CommandHandler_runSQLCommand_realm_string", False)
                        Return ""
                    Else
                        Dim result = foundRows(0).Item(column)
                        If IsDBNull(result) Then
                            LogAppend("Readed DBnull -> returning nothing", "CommandHandler_runSQLCommand_realm_string",
                                      False)
                            Return ""
                        Else
                            Return result.ToString()
                        End If
                    End If
                Catch ex As Exception
                    LogAppend(
                        "Exception occured: ###START###" &
                        ex.ToString() &
                        "###END###", "CommandHandler_runSQLCommand_realm_string", True, True)
                    Return ""
                End Try
            Else
                Dim useConn As MySqlConnection = GlobalVariables.GlobalConnection_Realm
                If useTargetConnection Then useConn = GlobalVariables.TargetConnection_Realm
                If GlobalVariables.forceTargetConnectionUsage Then useConn = GlobalVariables.TargetConnection
                Return RunUsualCommand(command, useConn)
            End If
        End Function

        <Localizable(False)>
        Public Function runSQLCommand_characters_string_setconn(command As String,
                                                                targetConnection As MySqlConnection) As String
            If Not command.Contains("SELECT MAX(") AndAlso command.ToLower.StartsWith("select") Then
                Dim table As String = SplitString(command.ToLower(), "from ", " where")
                table = table.Replace("`", "")
                LogAppend("Table is: " & table, "CommandHandler_runSQLCommand_characters_string_setconn")
                If Not table = TempTable1Name Then
                    TempTable1Name = table
                    TempTable1 = ReturnDataTable_setconn("SELECT * FROM `" & table & "`", targetConnection)
                End If
                Dim column As String = SplitString(command.ToLower(), "select ", " from")
                column = column.Replace("`", "")
                Dim args As String()
                args = command.Split(" "c)
                Dim newcommand = ""
                For i = 0 To args.Length - 1
                    If args(i) = "WHERE" Then
                        While i < args.Length - 1
                            i += 1
                            Dim tmpStr As String = args(i)
                            If tmpStr.ToLower() = "and" Then tmpStr = " " & tmpStr & " "
                            newcommand &= tmpStr
                        End While
                        Exit For
                    End If
                Next i
                newcommand = newcommand.Replace("`", "")
                Dim foundRows() As DataRow
                Try
                    foundRows = TempTable1.Select(newcommand)
                    LogAppend("New command/column: " & newcommand & " / " & column,
                              "CommandHandler_runSQLCommand_characters_string_setconn")
                    If foundRows.Length = 0 Then
                        LogAppend("0 Results -> returning nothing",
                                  "CommandHandler_runSQLCommand_characters_string_setconn", False)
                        Return ""
                    Else
                        Dim result = foundRows(0).Item(column)
                        If IsDBNull(result) Then
                            LogAppend("Readed DBnull -> returning nothing",
                                      "CommandHandler_runSQLCommand_characters_string_setconn",
                                      False)
                            Return ""
                        Else
                            Return result.ToString()
                        End If
                    End If
                Catch ex As Exception
                    LogAppend(
                        "Exception occured: ###START###" &
                        ex.ToString() &
                        "###END###", "CommandHandler_runSQLCommand_characters_string_setconn", True, True)
                    Return ""
                End Try
            Else
                Return RunUsualCommand(command, targetConnection)
            End If
        End Function

        Public Function runSQLCommand_realm_string_setconn(command As String,
                                                           targetConnection As MySqlConnection) As String
            Return runSQLCommand_characters_string_setconn(command, targetConnection)
        End Function

        Public Function ReturnDataTable(command As String, Optional useTargetConnection As Boolean = False) _
            As DataTable
            LogAppend("Executing new MySQL command. Command is: " & command, "CommandHandler_ReturnDataTable", False)
            Dim conn As MySqlConnection
            If GlobalVariables.forceTargetConnectionUsage Then useTargetConnection = True
            If useTargetConnection = False Then
                conn = GlobalVariables.GlobalConnection
            Else
                conn = GlobalVariables.TargetConnection
            End If
            Dim da As New MySqlDataAdapter(command, conn)
            Dim dt As New DataTable
            Try
                da.Fill(dt)
                Return dt
            Catch ex As Exception
                LogAppend(
                    "Failed to fill DataTable! -> Returning nothing -> Exception is: ###START###" & ex.ToString() &
                    "###END###", "CommandHandler_ReturnDataTable", True, True)
                Return dt
            End Try
        End Function

        Public Function ReturnDataTableRealm(command As String, Optional useTargetConnection As Boolean = False) _
            As DataTable
            LogAppend("Executing new MySQL command. Command is: " & command, "CommandHandler_ReturnDataTableRealm",
                      False)
            Dim conn As MySqlConnection
            If GlobalVariables.forceTargetConnectionUsage Then useTargetConnection = True
            If useTargetConnection = False Then
                conn = GlobalVariables.GlobalConnection_Realm
            Else
                conn = GlobalVariables.TargetConnection_Realm
            End If
            Dim da As New MySqlDataAdapter(command, conn)
            Dim dt As New DataTable
            Try
                da.Fill(dt)
                For Each dtRow As DataRow In dt.Rows
                    For i = 0 To dt.Rows(0).Table.Columns.Count - 1
                        If IsDBNull(dtRow.Item(i)) Then dtRow.Item(i) = ""
                    Next
                Next
                Return dt
            Catch ex As Exception
                LogAppend(
                    "Failed to fill DataTable! -> Returning nothing -> Exception is: ###START###" & ex.ToString() &
                    "###END###", "CommandHandler_ReturnDataTableRealm", True, True)
                Return dt
            End Try
        End Function

        Public Function ReturnDataTable_setconn(command As String, targetConnection As MySqlConnection) _
            As DataTable
            LogAppend("Executing new MySQL command. Command is: " & command, "CommandHandler_ReturnDataTable", False)
            Dim conn As MySqlConnection
            conn = targetConnection
            Dim da As New MySqlDataAdapter(command, conn)
            Dim dt As New DataTable
            Try
                da.Fill(dt)
                Return dt
            Catch ex As Exception
                LogAppend(
                    "Failed to fill DataTable! -> Returning nothing -> Exception is: ###START###" & ex.ToString() &
                    "###END###", "CommandHandler_ReturnDataTable", True, True)
                Return dt
            End Try
        End Function

        Public Function ReturnCountResults(command As String, Optional useTargetConnection As Boolean = False) _
            As Integer
            LogAppend("Executing new MySQL command. Command is: " & command, "CommandHandler_ReturnCountResult", False)
            Dim conn As MySqlConnection
            If GlobalVariables.forceTargetConnectionUsage Then useTargetConnection = True
            If useTargetConnection = False Then
                conn = GlobalVariables.GlobalConnection
            Else
                conn = GlobalVariables.TargetConnection
            End If
            Dim da As New MySqlDataAdapter(command, conn)
            Dim dt As New DataTable
            Try
                da.Fill(dt)
                Dim lastcount As Integer = dt.Rows.Count
                Return lastcount
            Catch ex As Exception
                LogAppend(
                    "Failed to fill DataTable! -> Returning nothing -> Exception is: ###START###" & ex.ToString() &
                    "###END###", "CommandHandler_ReturnCountResult", True, True)
                Return 0
            End Try
        End Function

        Public Function ReturnResultWithRow(command As String, spalte As String, row As Integer,
                                            Optional useTargetConnection As Boolean = False) As String
            LogAppend(
                "Executing new MySQL command. Column is: " & spalte & " / Row is: " & row.ToString & " / Command is: " &
                command, "CommandHandler_ReturnResultWithRow", False)
            Dim conn As MySqlConnection
            If GlobalVariables.forceTargetConnectionUsage Then useTargetConnection = True
            If useTargetConnection = False Then
                conn = GlobalVariables.GlobalConnection
            Else
                conn = GlobalVariables.TargetConnection
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
                LogAppend(
                    "Failed to fill DataTable! -> Returning nothing -> Exception is: ###START###" & ex.ToString() &
                    "###END###", "CommandHandler_ReturnResultWithRow", True, True)
                Return ""
            End Try
        End Function

        Public Function ReturnResultCount(command As String, Optional useTargetConnection As Boolean = False) _
            As Integer
            LogAppend("Executing new MySQL command.", "CommandHandler_ReturnResultCount", False)
            Dim conn As MySqlConnection
            If GlobalVariables.forceTargetConnectionUsage Then useTargetConnection = True
            If useTargetConnection = False Then
                conn = GlobalVariables.GlobalConnection
            Else
                conn = GlobalVariables.TargetConnection
            End If
            Dim da As New MySqlDataAdapter(command, conn)
            Dim dt As New DataTable
            Try
                da.Fill(dt)
                Return dt.Rows.Count
            Catch ex As Exception
                LogAppend(
                    "Failed to fill DataTable! -> Returning nothing -> Exception is: ###START###" & ex.ToString() &
                    "###END###", "CommandHandler_ReturnResultCount", True, True)
                Return 0
            End Try
        End Function
    End Module
End Namespace