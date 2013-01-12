Imports Namcore_Studio.EventLogging
Imports Namcore_Studio.CommandHandler
Imports Namcore_Studio.GlobalVariables
Imports MySql.Data.MySqlClient
Public Class AccountCreation

    Public Shared Sub CreateNewAccount(ByVal checkexistance As Boolean, ByVal accname As String, ByVal passhas As String, Optional gmlevel As String = "A", Optional email As String = "", Optional flags As String = "0")
        LogAppend("Creating new account " & accname, "AccountCreation_CreateNewAccount", True)
        Select Case sourceCore
            Case "arcemu"
                Dim newid As Integer = runSQLCommand_realm_string("SELECT MAX(acct) + 1 FROM accounts", True)
                Dim sqlstring = "INSERT INTO accounts (acct, `login`, `encrypted_password`, `gm`, `email`, `flags`) " &
                                           "VALUES (@accid, @accname, @pass, @gm, @email, @flags)"
                Dim tempcommand As New MySqlCommand(sqlstring, TargetConnection_Realm)
                tempcommand.Parameters.AddWithValue("@accid", newid)
                tempcommand.Parameters.AddWithValue("@accname", accname)
                tempcommand.Parameters.AddWithValue("@pass", passhas)
                tempcommand.Parameters.AddWithValue("@gm", gmlevel)
                tempcommand.Parameters.AddWithValue("@email", email)
                tempcommand.Parameters.AddWithValue("@flags", flags)
                tempcommand.ExecuteNonQuery()
            Case "trinity"
                loadAtTrinity(characterGuid, setId, accountId)
            Case "trinitytbc"
                loadAtTrinityTBC(characterGuid, setId, accountId)
            Case "mangos"
                loadAtMangos(characterGuid, setId, accountId)
            Case Else

        End Select
    End Sub
End Class
