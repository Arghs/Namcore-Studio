'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013 NamCore Studio <https://github.com/megasus/Namcore-Studio>
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
'*      /Filename:      mysqlHandler
'*      /Description:   Handles MySQL connection and commands
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Imports MySql.Data.MySqlClient
Imports System.Xml
Imports System.IO
Imports System.Text

Public Class mysqlHandler
    Public Shared sqlconn1auth As New MySqlConnection
    Public Shared sqlconn1characters As New MySqlConnection
    Public Shared sqlconn2auth As New MySqlConnection
    Public Shared sqlconn2characters As New MySqlConnection
    Public Shared Sub openSQLconnection(ByRef targetConnection As MySqlConnection, ByVal connectionstring As String)
        If targetConnection.State = ConnectionState.Closed Then
            targetConnection.ConnectionString = connectionstring
            targetConnection.Open()
        End If
    End Sub
    Public Shared Sub closeSQLconnection(ByRef targetConnection As MySqlConnection)
        If targetConnection.State = ConnectionState.Open Then
            targetConnection.Close()
            targetConnection.Dispose()
        End If
    End Sub
    Public Shared Function runsqlcommand(ByVal connection As MySqlConnection, ByVal command As String) As String
        Dim da As New MySqlDataAdapter(command, connection)
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim loopcounter As Integer = 0
        Try
            Dim sb = New StringBuilder()
            Dim xml As String
            dt = ds.Tables.Add("primary")
            da.Fill(dt)
            Using writer = New StringWriter(sb)
                ds.WriteXml(writer)
                xml = sb.ToString()
            End Using
            Return xml
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function ConvertDataTableToXMLString(ByVal table As DataTable) As String
        Dim sw As New System.IO.StringWriter
        Dim xmltw As New XmlTextWriter(sw)
        Dim ds As New DataSet("MY_DATA_SET")
        table.TableName = "MY_DATA"
        ds.Tables.Add(table)
        ds.WriteXml(xmltw, XmlWriteMode.IgnoreSchema)
        Return sw.ToString
    End Function
End Class
