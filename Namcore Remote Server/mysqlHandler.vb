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
        If targetConnection.State = False Then
            targetConnection.ConnectionString = connectionstring
            targetConnection.Open()
        End If
    End Sub
    Public Shared Sub closeSQLconnection(ByRef targetConnection As MySqlConnection)
        If targetConnection.State = True Then
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
