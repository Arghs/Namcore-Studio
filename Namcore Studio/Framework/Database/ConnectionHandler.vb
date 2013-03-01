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
'*      /Filename:      ConnectionHandler
'*      /Description:   Handles MySQL connections
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Imports MySql.Data.MySqlClient
Imports Namcore_Studio.EventLogging
Imports Namcore_Studio.Basics
Public Class ConnectionHandler
    Public Shared Sub OpenNewMySQLConnection(ByVal targetconnection As MySqlConnection, serverstring As String)
        LogAppend("Opening new MySQL connection (target: " & targetconnection.ToString() & " with connectionstring: " & serverstring, "ConnectionHandler_OpenNewMySQLConnection", True)
        If targetconnection.State = True Then
            LogAppend("MySQL connection already open! -> Closing it now", "ConnectionHandler_OpenNewMySQLConnection", True)
            Try
                targetconnection.Close()
                targetconnection.Dispose()
                LogAppend("MySQL connection is now closed!", "ConnectionHandler_OpenNewMySQLConnection", False)
            Catch ex As MySqlException
                LogAppend("MySQL connection could not be closed! -> Aborting process -> Exception is: ###START###" & ex.ToString() & "###END###", "ConnectionHandler_OpenNewMySQLConnection", True, True)
                AbortProcess()
                Exit Sub
            End Try
        End If
        LogAppend("Setting MySQL connectionstring to: " & serverstring & vbNewLine & "and trying to open new connection", "ConnectionHandler_OpenNewMySQLConnection", False)
        targetconnection.ConnectionString = serverstring
        Try
            targetconnection.Open()
            LogAppend("MySQL connection is now open!", "ConnectionHandler_OpenNewMySQLConnection", True)
        Catch ex As MySqlException
            targetconnection.Close()
            targetconnection.Dispose()
            LogAppend("MySQL connection could not be opened! -> Aborting process -> Exception is: ###START###" & ex.ToString() & "###END###", "ConnectionHandler_OpenNewMySQLConnection", True, True)
            AbortProcess()
            Exit Sub
        End Try
    End Sub
    Public Shared Function TestConnection(ByVal connectionstring As String) As Boolean
        Dim SQLConnection As New MySqlConnection
        Try
            SQLConnection.Close()
            SQLConnection.Dispose()
        Catch ex As Exception

        End Try
        SQLConnection.ConnectionString = connectionstring
        Try

            If SQLConnection.State = ConnectionState.Closed Then
                SQLConnection.Open()
                SQLConnection.Close()
                SQLConnection.Dispose()
                Return True
            Else
                SQLConnection.Close()
                SQLConnection.Dispose()
                Return False
            End If
        Catch ex As Exception
            Try
                SQLConnection.Close()
                SQLConnection.Dispose()
            Catch
            End Try
            Return False
        End Try
    End Function
End Class
