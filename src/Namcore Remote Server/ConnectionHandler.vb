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
'*      /Description:   Handles TCP/IP connection
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.Net.Sockets
Imports System.IO
Imports System.Net
Imports MySql.Data.MySqlClient
Imports Namcore_Remote_Server.Logging
Imports Namcore_Remote_Server.mysqlHandler

Module ConnectionHandler
    Private server As TcpListener
    Private client As New TcpClient
    Private ipendpoint As IPEndPoint = New IPEndPoint(IPAddress.Any, 8000)
    Private list As New List(Of Connection)
    Dim cID As Integer = 0
    Dim app_version As String = My.Application.Info.Version.ToString()
    Dim time As String = Date.Now
    Private Structure Connection
        Dim stream As NetworkStream
        Dim streamw As StreamWriter
        Dim streamr As StreamReader
        Dim client_username As String
        Dim client_id As Integer
    End Structure
    Sub Main()
        Console.ForegroundColor = ConsoleColor.Green
        Console.Title = "Namcore Remote Server"
        Console.WriteLine("# Server Revision: " & app_version)
        LogMessage("# Chat-Server Revision: " + app_version, "server")
        Console.WriteLine("# <CTRL-C>")
        LogMessage("# <CTRL-C>", "server")
        Console.WriteLine("# © megasus/Alcanmage")
        LogMessage("# © megasus/Alcanmage", "server")
        Console.WriteLine("# Loaded at " & time)
        LogMessage("# Loaded at " & time, "server")
        Console.WriteLine("# Waiting for connections")
        LogMessage("# Waiting for connections", "server")
        Console.ForegroundColor = ConsoleColor.White
        server = New TcpListener(ipendpoint)
        server.Start()

        While True ' waiting for connection
            client = server.AcceptTcpClient

            Dim c As New Connection
            c.stream = client.GetStream
            c.streamr = New StreamReader(c.stream)
            c.streamw = New StreamWriter(c.stream)

            c.client_username = c.streamr.ReadLine
            cID += 1
            c.client_id = cID
            list.Add(c) ' add client
            Console.WriteLine(c.client_username & " has joined.")

            Dim t As New Threading.Thread(AddressOf ListenToConnection)
            t.Start(c)
        End While
    End Sub
    Private Sub ListenToConnection(ByVal con As Connection)
        Do
            Try
                Dim tmp As String = con.streamr.ReadLine
                'Console.WriteLine(con.client_username & ": " & tmp)
                Select Case True
                    Case tmp.StartsWith("/runcommand ")
                        Dim tmpconn As New MySqlConnection
                        Select Case readfield(tmp, "conn")
                            Case "sqlconn1auth"
                                tmpconn = sqlconn1auth
                            Case "sqlconn1characters"
                                tmpconn = sqlconn1characters
                            Case "sqlconn2auth"
                                tmpconn = sqlconn2auth
                            Case "sqlconn2characters"
                                tmpconn = sqlconn2characters
                        End Select
                        SendtoClient(con, runsqlcommand(tmpconn, readfield(tmp, "command")), tmp)
                    Case tmp.StartsWith("/opensql")
                        Dim tmpconn As New MySqlConnection
                        Select Case readfield(tmp, "conn")
                            Case "sqlconn1auth"
                                tmpconn = sqlconn1auth
                            Case "sqlconn1characters"
                                tmpconn = sqlconn1characters
                            Case "sqlconn2auth"
                                tmpconn = sqlconn2auth
                            Case "sqlconn2characters"
                                tmpconn = sqlconn2characters
                        End Select
                        openSQLconnection(tmpconn, readfield(tmp, "connectionstring"))
                        SendtoClient(con, "/next", tmp)
                    Case tmp.StartsWith("/closesql")
                        Dim tmpconn As New MySqlConnection
                        Select Case readfield(tmp, "conn")
                            Case "sqlconn1auth"
                                tmpconn = sqlconn1auth
                            Case "sqlconn1characters"
                                tmpconn = sqlconn1characters
                            Case "sqlconn2auth"
                                tmpconn = sqlconn2auth
                            Case "sqlconn2characters"
                                tmpconn = sqlconn2characters
                        End Select
                        closeSQLconnection(tmpconn)
                        SendtoClient(con, "/next", tmp)
                    Case tmp.StartsWith("/connstate")
                        Dim tmpconn As New MySqlConnection
                        Select Case readfield(tmp, "conn")
                            Case "sqlconn1auth"
                                tmpconn = sqlconn1auth
                            Case "sqlconn1characters"
                                tmpconn = sqlconn1characters
                            Case "sqlconn2auth"
                                tmpconn = sqlconn2auth
                            Case "sqlconn2characters"
                                tmpconn = sqlconn2characters
                        End Select
                        SendtoClient(con, tmpconn.State.ToString(), tmp)
                    Case Else : End Select

            Catch ' cancel connection
                list.Remove(con)
                Console.WriteLine(con.client_username & " has exit.")
                Exit Do
            End Try
        Loop
    End Sub
    Private Sub SendtoClient(ByVal conn As Connection, ByVal returnstring As String, ByVal tmpst As String)
        returnstring = returnstring.Replace(vbNewLine, "")
        conn.streamw.WriteLine(returnstring & "<respid>" & readfield(tmpst, "respid") & "</respid>")
        conn.streamw.Flush()
    End Sub
    Private Function Readfield(ByVal tmpstring As String, ByVal fieldname As String) As String
        Try
            Return Split(Split(tmpstring, "<" & fieldname & ">", 5)(1), "</" & fieldname & ">", 6)(0)
        Catch
            Return ""
        End Try
    End Function
End Module
