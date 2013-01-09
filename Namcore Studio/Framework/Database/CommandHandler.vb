Imports MySql.Data.MySqlClient
Imports Namcore_Studio.EventLogging
Imports Namcore_Studio.GlobalVariables
Public Class CommandHandler
    Public Shared Function runSQLCommand_characters_string(ByVal command As String) As String
        LogAppend("Executing new MySQL command. Command is: " & command, "CommandHandler_runSQLCommand_characters_string", False)
        Dim da As New MySqlDataAdapter(command, GlobalConnection)
        Dim dt As New DataTable
        Try
            da.Fill(dt)
            Dim lastcount As Integer = CInt(Val(dt.Rows.Count.ToString))
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
    Public Shared Function runSQLCommand_realm_string(ByVal command As String) As String
        LogAppend("Executing new MySQL command. Command is: " & command, "CommandHandler_runSQLCommand_realm_string", False)
        Dim da As New MySqlDataAdapter(command, GlobalConnection_Realm)
        Dim dt As New DataTable
        Try
            da.Fill(dt)
            Dim lastcount As Integer = CInt(Val(dt.Rows.Count.ToString))
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
    Public Shared Function ReturnDataTable(ByVal command As String) As DataTable
        LogAppend("Executing new MySQL command. Command is: " & command, "CommandHandler_ReturnDataTable", False)
        Dim da As New MySqlDataAdapter(command, GlobalConnection)
        Dim dt As New DataTable
        Try
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogAppend("Failed to fill DataTable! -> Returning nothing -> Exception is: ###START###" & ex.ToString() & "###END###", "CommandHandler_ReturnDataTable", True, True)
            Return dt
        End Try
    End Function
    Public Shared Function ReturnCountResults(ByVal command As String) As Integer
        LogAppend("Executing new MySQL command. Command is: " & command, "CommandHandler_ReturnCountResult", False)
        Dim da As New MySqlDataAdapter(command, GlobalConnection)
        Dim dt As New DataTable
        Try
            da.Fill(dt)
            Dim lastcount As Integer = CInt(Val(dt.Rows.Count.ToString))
            Return lastcount
        Catch ex As Exception
            LogAppend("Failed to fill DataTable! -> Returning nothing -> Exception is: ###START###" & ex.ToString() & "###END###", "CommandHandler_ReturnCountResult", True, True)
            Return 0
        End Try
    End Function
    Public Shared Function ReturnResultWithRow(ByVal command As String, ByVal spalte As String, ByVal row As Integer) As String
        LogAppend("Executing new MySQL command. Column is: " & spalte & " / Row is: " & row.ToString & " / Command is: " & command, "CommandHandler_ReturnResultWithRow", False)
        Dim da As New MySqlDataAdapter(command, GlobalConnection)
        Dim dt As New DataTable
        Try
            da.Fill(dt)
            Dim lastcount As Integer = CInt(Val(dt.Rows.Count.ToString))
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
End Class
