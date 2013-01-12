Imports Namcore_Studio.EventLogging
Imports Namcore_Studio.Basics
Imports Namcore_Studio.GlobalVariables
Imports Namcore_Studio.CommandHandler
Imports Namcore_Studio.Conversions
Public Class CharacterQuestlogHandler
    Public Shared Sub GetCharacterQuestlog(ByVal characterGuid As Integer, ByVal setId As Integer, ByVal accountId As Integer)
        LogAppend("Loading character questlog for characterGuid: " & characterGuid & " and setId: " & setId, "CharacterQuestlogHandler_GetCharacterQuestlog", True)
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
        LogAppend("Loading character questlog @loadAtArcemu", "CharacterQuestlogHandler_loadAtArcemu", False)
        Dim tempdt As DataTable = ReturnDataTable("SELECT quest_id, completed, explored_area1, expirytimy, slot FROM questlog WHERE player_guid='" & charguid.ToString() & "'")
        Dim templist As New List(Of String)
        Try
            Dim lastcount As Integer = tryint(Val(tempdt.Rows.Count.ToString))
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim readedcode As String = (tempdt.Rows(count).Item(0)).ToString
                    Dim excounter As Integer = UBound(readedcode.Split(CChar(",")))
                    Dim partscounter As Integer = 0
                    Do
                        Dim quest As String = (tempdt.Rows(count).Item(0)).ToString
                        Dim status As String = (tempdt.Rows(count).Item(1)).ToString
                        Dim explored As String = (tempdt.Rows(count).Item(2)).ToString
                        Dim timer As String = (tempdt.Rows(count).Item(3)).ToString
                        Dim slot As String = (tempdt.Rows(count).Item(4)).ToString
                        templist.Add("<quest>" & quest & "</quest><status>" & status & "</status><explored>" & explored & "</explored><timer>" & timer & "</timer><slot>" & slot & "</slot>")
                    Loop Until partscounter = excounter - 1
                    count += 1
                Loop Until count = lastcount
            Else
                LogAppend("No quests found!", "CharacterQuestlogHandler_loadAtArcemu", True)
            End If
        Catch ex As Exception
            LogAppend("Something went wrong while loading character questlog! -> skipping -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterQuestlogHandler_loadAtArcemu", True, True)
            Exit Sub
        End Try
        SetTemporaryCharacterInformation("@character_questlog", ConvertListToString(templist), tar_setId)
    End Sub
    Private Shared Sub loadAtTrinity(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)
        LogAppend("Loading character questlog @loadAtTrinity", "CharacterQuestlogHandler_loadAtTrinity", False)
        Dim tempdt As DataTable = ReturnDataTable("SELECT quest, status, explored, timer FROM character_queststatus WHERE guid='" & charguid.ToString() & "'")
        Dim templist As New List(Of String)
        Try
            Dim lastcount As Integer = tryint(Val(tempdt.Rows.Count.ToString))
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim quest As String = (tempdt.Rows(count).Item(0)).ToString
                    Dim status As String = (tempdt.Rows(count).Item(1)).ToString
                    Dim explored As String = (tempdt.Rows(count).Item(2)).ToString
                    Dim timer As String = (tempdt.Rows(count).Item(3)).ToString
                    templist.Add("<quest>" & quest & "</quest><status>" & status & "</status><explored>" & explored & "</explored><timer>" & timer & "</timer>")
                    count += 1
                Loop Until count = lastcount
            Else
                LogAppend("No quests found!", "CharacterQuestlogHandler_loadAtTrinity", True)
            End If
        Catch ex As Exception
            LogAppend("Something went wrong while loading character questlog! -> skipping -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterQuestlogHandler_loadAtTrinity", True, True)
        End Try
        Dim tempdt2 As DataTable = ReturnDataTable("SELECT quest FROM character_queststatus_rewarded WHERE guid='" & charguid.ToString() & "'")
        Try
            Dim lastcount As Integer = tryint(Val(tempdt2.Rows.Count.ToString))
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim quest As String = (tempdt2.Rows(count).Item(0)).ToString
                    If Not quest = "" Then AppendTemporaryCharacterInformation("@character_finishedQuests", quest & ",", tar_setId)
                    count += 1
                Loop Until count = lastcount
            End If
        Catch ex As Exception
            LogAppend("Something went wrong while loading character finishedQuests! -> skipping -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterQuestlogHandler_loadAtTrinity", True, True)
        End Try
        SetTemporaryCharacterInformation("@character_questlog", ConvertListToString(templist), tar_setId)
    End Sub
    Private Shared Sub loadAtTrinityTBC(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)
        LogAppend("Loading character questlog @loadAtTrinityTBC", "CharacterQuestlogHandler_loadAtTrinityTBC", False)
        Dim tempdt As DataTable = ReturnDataTable("SELECT quest, status, explored, timer, rewarded FROM character_queststatus WHERE guid='" & charguid.ToString() & "'")
        Dim templist As New List(Of String)
        Try
            Dim lastcount As Integer = tryint(Val(tempdt.Rows.Count.ToString))
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim quest As String = (tempdt.Rows(count).Item(0)).ToString
                    Dim status As String = (tempdt.Rows(count).Item(1)).ToString
                    Dim explored As String = (tempdt.Rows(count).Item(2)).ToString
                    Dim timer As String = (tempdt.Rows(count).Item(3)).ToString
                    Dim rewarded As String = (tempdt.Rows(count).Item(4)).ToString
                    If rewarded = "1" Then
                        AppendTemporaryCharacterInformation("@character_finishedQuests", quest & ",", tar_setId)
                    Else
                        templist.Add("<quest>" & quest & "</quest><status>" & status & "</status><explored>" & explored & "</explored><timer>" & timer & "</timer>")
                    End If

                    count += 1
                Loop Until count = lastcount
            Else
                LogAppend("No quests found!", "CharacterQuestlogHandler_loadAtTrinityTBC", True)
            End If
        Catch ex As Exception
            LogAppend("Something went wrong while loading character questlog! -> skipping -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterQuestlogHandler_loadAtTrinityTBC", True, True)
        End Try
        SetTemporaryCharacterInformation("@character_questlog", ConvertListToString(templist), tar_setId)
    End Sub
    Private Shared Sub loadAtMangos(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)
        LogAppend("Loading character questlog @loadAtMangos", "CharacterQuestlogHandler_loadAtMangos", False)
        Dim tempdt As DataTable = ReturnDataTable("SELECT quest, status, explored, timer, rewarded FROM character_spell WHERE guid='" & charguid.ToString() & "'")
        Dim templist As New List(Of String)
        Try
            Dim lastcount As Integer = tryint(Val(tempdt.Rows.Count.ToString))
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim quest As String = (tempdt.Rows(count).Item(0)).ToString
                    Dim status As String = (tempdt.Rows(count).Item(1)).ToString
                    Dim explored As String = (tempdt.Rows(count).Item(2)).ToString
                    Dim timer As String = (tempdt.Rows(count).Item(3)).ToString
                    Dim rewarded As String = (tempdt.Rows(count).Item(4)).ToString
                    If rewarded = "1" Then
                        AppendTemporaryCharacterInformation("@character_finishedQuests", quest & ",", tar_setId)
                    Else
                        templist.Add("<quest>" & quest & "</quest><status>" & status & "</status><explored>" & explored & "</explored><timer>" & timer & "</timer>")
                    End If
                    count += 1
                Loop Until count = lastcount
            Else
                LogAppend("No quests found!", "CharacterQuestlogHandler_loadAtMangos", True)
            End If
        Catch ex As Exception
            LogAppend("Something went wrong while loading character questlog! -> skipping -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterQuestlogHandler_loadAtMangos", True, True)
            Exit Sub
        End Try
        SetTemporaryCharacterInformation("@character_questlog", ConvertListToString(templist), tar_setId)
    End Sub
End Class
