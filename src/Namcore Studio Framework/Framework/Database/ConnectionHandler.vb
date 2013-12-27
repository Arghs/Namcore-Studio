'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013 NamCore Studio <https://github.com/megasus/Namcore-Studio>
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
'*      /Filename:      ConnectionHandler
'*      /Description:   Handles MySQL connections
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.Logging
Imports MySql.Data.MySqlClient

Namespace Framework.Database
    Public Module ConnectionHandler
        Public Sub OpenNewMySqlConnection(ByVal targetconnection As MySqlConnection, serverstring As String)
            LogAppend(
                "Opening new MySQL connection (target: " & targetconnection.ToString() & " with connectionstring: " &
                serverstring, "ConnectionHandler_OpenNewMySQLConnection", True)
            If targetconnection.State = ConnectionState.Open Then
                LogAppend("MySQL connection already open! -> Closing it now", "ConnectionHandler_OpenNewMySQLConnection",
                          True)
                Try
                    targetconnection.Close()
                    targetconnection.Dispose()
                    LogAppend("MySQL connection is now closed!", "ConnectionHandler_OpenNewMySQLConnection", False)
                Catch ex As MySqlException
                    LogAppend(
                        "MySQL connection could not be closed! -> Aborting process -> Exception is: ###START###" &
                        ex.ToString() & "###END###", "ConnectionHandler_OpenNewMySQLConnection", True, True)
                    Exit Sub
                End Try
            End If
            LogAppend(
                "Setting MySQL connectionstring to: " & serverstring & vbNewLine & "and trying to open new connection",
                "ConnectionHandler_OpenNewMySQLConnection", False)
            targetconnection.ConnectionString = serverstring
            Try
                targetconnection.Open()
                LogAppend("MySQL connection is now open!", "ConnectionHandler_OpenNewMySQLConnection", True)
            Catch ex As MySqlException
                targetconnection.Close()
                targetconnection.Dispose()
                LogAppend(
                    "MySQL connection could not be opened! -> Aborting process -> Exception is: ###START###" &
                    ex.ToString() &
                    "###END###", "ConnectionHandler_OpenNewMySQLConnection", True, True)
                Exit Sub
            End Try
        End Sub

        Public Function TestConnection(ByVal connectionstring As String) As Boolean
            Dim sqlConnection As New MySqlConnection
            Try
                sqlConnection.Close()
                sqlConnection.Dispose()
            Catch ex As Exception

            End Try
            sqlConnection.ConnectionString = connectionstring
            Try

                If sqlConnection.State = ConnectionState.Closed Then
                    sqlConnection.Open()
                    sqlConnection.Close()
                    sqlConnection.Dispose()
                    Return True
                Else
                    sqlConnection.Close()
                    sqlConnection.Dispose()
                    Return False
                End If
            Catch ex As Exception
                Try
                    sqlConnection.Close()
                    sqlConnection.Dispose()
                Catch
                End Try
                Return False
            End Try
        End Function

        Public Function TestConnectionAndReturn(ByVal connectionstring As String) As MySqlConnection
            Dim sqlConnection As New MySqlConnection
            Try
                sqlConnection.Close()
                sqlConnection.Dispose()
            Catch ex As Exception

            End Try
            sqlConnection.ConnectionString = connectionstring
            Try

                If sqlConnection.State = ConnectionState.Closed Then
                    sqlConnection.Open()
                    Return sqlConnection
                Else
                    sqlConnection.Close()
                    sqlConnection.Dispose()
                    Return Nothing
                End If
            Catch ex As Exception
                Try
                    sqlConnection.Close()
                    sqlConnection.Dispose()
                Catch
                End Try
                Return Nothing
            End Try
        End Function
    End Module
End Namespace