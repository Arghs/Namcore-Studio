Imports Namcore_Studio.EventLogging
Imports Namcore_Studio.Basics
Imports Namcore_Studio.GlobalVariables
Imports Namcore_Studio.CommandHandler
Imports Namcore_Studio.Conversions
Public Class CharacterActionsHandler
    Public Shared Sub GetCharacterActions(ByVal characterGuid As Integer, ByVal setId As Integer, ByVal accountId As Integer)
        LogAppend("Loading character actions for characterGuid: " & characterGuid & " and setId: " & setId, "CharacterActionssHandler_GetCharacterActions", True)
        Select Case sourceCore
            Case "arcemu"
                loadAtArcemu(characterGuid, setId, accountId)
            Case "trinity"
                loadAtTrinity(characterGuid, setId, accountId)
            Case "trinitytbc"
                loadAtTrinityTBC(characterGuid, setId, accountId)
            Case "mangos"
                loadAtMangos(characterGuid, setId, accountId)
            Case Else

        End Select

    End Sub
    Private Shared Sub loadAtArcemu(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)
        LogAppend("Loading character Actions @loadAtArcemu", "CharacterActionsHandler_loadAtArcemu", False)
        SetTemporaryCharacterInformation("@character_arcemuAction1", runSQLCommand_characters_string("SELECT actions1 FROM characters WHERE guid='" & charguid.ToString & "'"), tar_setId)
        SetTemporaryCharacterInformation("@character_arcemuAction2", runSQLCommand_characters_string("SELECT actions2 FROM characters WHERE guid='" & charguid.ToString & "'"), tar_setId)
        Dim templist As New List(Of String)
        Try
            Dim readedcode As String = GetTemporaryCharacterInformation("@character_arcemuAction1", tar_setId)
            If Not readedcode.Length > 2 Then LogAppend("Warning! Actions1 seems to be invalid!", "CharacterActionsHandler_loadAtArcemu", False, True)
            Dim excounter As Integer = UBound(readedcode.Split(CChar(",")))
            Dim loopcounter As Integer = 0
            Dim finalcounter As Integer = CInt(excounter / 3)
            Dim partscounter As Integer = 0
            Do
                Dim parts() As String = readedcode.Split(","c)
                Dim Action As String = parts(partscounter).ToString
                Dim gbutton As String = (loopcounter + 1).ToString
                templist.Add("<action>" & Action & "</action><spec>0</spec><button>" & gbutton & "</button><type>0</type>")
                loopcounter += 1
            Loop Until loopcounter = finalcounter
            Dim readedcode2 As String = GetTemporaryCharacterInformation("@character_arcemuAction2", tar_setId)
            If Not readedcode2.Length > 2 Then LogAppend("Warning! Actions2 seems to be invalid!", "CharacterActionsHandler_loadAtArcemu", False, True)
            Dim excounter2 As Integer = UBound(readedcode2.Split(CChar(",")))
            Dim loopcounter2 As Integer = 0
            Dim finalcounter2 As Integer = CInt(excounter2 / 3)
            Dim partscounter2 As Integer = 0
            Do
                Dim parts() As String = readedcode2.Split(","c)
                Dim Action As String = parts(partscounter2).ToString
                Dim gbutton As String = (loopcounter2 + 1).ToString
                partscounter2 += 3
                templist.Add("<action>" & Action & "</action><spec>1</spec><button>" & gbutton & "</button><type>0</type>")
                loopcounter2 += 1
            Loop Until loopcounter2 = finalcounter2
        Catch ex As Exception
            LogAppend("Something went wrong while loading character Actions! -> skipping -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterActionsHandler_loadAtArcemu", True, True)
        End Try
        SetTemporaryCharacterInformation("@character_actions", ConvertListToString(templist), tar_setId)
    End Sub
    Private Shared Sub loadAtTrinity(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)
        LogAppend("Loading character Actions @loadAtTrinity", "CharacterActionsHandler_loadAtTrinity", False)
        Dim tempdt As DataTable = ReturnDataTable("SELECT button, `spec`, action, `type` FROM character_action WHERE guid='" & charguid.ToString() & "'")
        Dim templist As New List(Of String)
        Try
            Dim lastcount As Integer = CInt(Val(tempdt.Rows.Count.ToString))
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim gbutton As String = (tempdt.Rows(count).Item(0)).ToString
                    Dim spec As String = (tempdt.Rows(count).Item(1)).ToString
                    Dim action As String = (tempdt.Rows(count).Item(2)).ToString
                    Dim atype As String = (tempdt.Rows(count).Item(3)).ToString
                    templist.Add("<action>" & action & "</action><spec>" & spec & "</spec><button>" & gbutton & "</button><type>" & atype & "</type>")
                    count += 1
                Loop Until count = lastcount
            Else
                LogAppend("No Actions found!", "CharacterActionsHandler_loadAtTrinity", True)
            End If
        Catch ex As Exception
            LogAppend("Something went wrong while loading character Actions! -> skipping -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterActionsHandler_loadAtTrinity", True, True)
            Exit Sub
        End Try
        SetTemporaryCharacterInformation("@character_actions", ConvertListToString(templist), tar_setId)
    End Sub
    Private Shared Sub loadAtTrinityTBC(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)

    End Sub
    Private Shared Sub loadAtMangos(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)
        LogAppend("Loading character Actions @loadAtMangos", "CharacterActionsHandler_loadAtMangos", False)
        Dim tempdt As DataTable = ReturnDataTable("SELECT button, `spec`, action, `type` FROM character_action WHERE guid='" & charguid.ToString() & "'")
        Dim templist As New List(Of String)
        Try
            Dim lastcount As Integer = CInt(Val(tempdt.Rows.Count.ToString))
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim gbutton As String = (tempdt.Rows(count).Item(0)).ToString
                    Dim spec As String = (tempdt.Rows(count).Item(1)).ToString
                    Dim action As String = (tempdt.Rows(count).Item(2)).ToString
                    Dim atype As String = (tempdt.Rows(count).Item(3)).ToString
                    templist.Add("<action>" & action & "</action><spec>" & spec & "</spec><button>" & gbutton & "</button><type>" & atype & "</type>")
                    count += 1
                Loop Until count = lastcount
            Else
                LogAppend("No Actions found!", "CharacterActionsHandler_loadAtMangos", True)
            End If
        Catch ex As Exception
            LogAppend("Something went wrong while loading character Actions! -> skipping -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterActionsHandler_loadAtMangos", True, True)
            Exit Sub
        End Try
        SetTemporaryCharacterInformation("@character_actions", ConvertListToString(templist), tar_setId)
    End Sub
End Class
