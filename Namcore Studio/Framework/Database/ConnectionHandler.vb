Imports MySql.Data.MySqlClient
Imports Namcore_Studio.EventLogging
Imports Namcore_Studio.Basics
Public Class ConnectionHandler


    Public Sub OpenNewMySQLConnection(ByVal targetconnection As MySqlConnection, serverstring As String)
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
End Class
