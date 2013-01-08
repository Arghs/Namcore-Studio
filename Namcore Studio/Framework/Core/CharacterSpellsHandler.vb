Imports Namcore_Studio.EventLogging
Imports Namcore_Studio.Basics
Imports Namcore_Studio.GlobalVariables
Imports Namcore_Studio.CommandHandler
Imports Namcore_Studio.Conversions
Public Class CharacterSpellsHandler
    Public Shared Sub GetCharacterSpells(ByVal characterGuid As Integer, ByVal setId As Integer, ByVal accountId As Integer)
        LogAppend("Loading character spells for characterGuid: " & characterGuid & " and setId: " & setId, "CharacterSpellsHandler_GetCharacterSpells", True)
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
        LogAppend("Loading character spells @loadAtArcemu", "CharacterSpellsHandler_loadAtArcemu", False)
        Dim tempdt As DataTable = ReturnDataTable("SELECT spells FROM characters WHERE guid='" & charguid.ToString() & "'")
        Dim templist As New List(Of String)
        Try
            Dim lastcount As Integer = CInt(Val(tempdt.Rows.Count.ToString))
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim readedcode As String = (tempdt.Rows(count).Item(0)).ToString
                    Dim excounter As Integer = UBound(readedcode.Split(CChar(",")))
                    Dim partscounter As Integer = 0
                    Do
                        Dim parts() As String = readedcode.Split(","c)
                        Dim spell As String = parts(partscounter).ToString
                        partscounter += 1
                        LogAppend("Adding spellId: " & spell, "CharacterSpellsHandler_LoadAtArcemu", True)
                        templist.Add("<spell>" & spell & "</spell><active>1</active><disabled>0</disabled>")
                    Loop Until partscounter = excounter - 1
                    count += 1
                Loop Until count = lastcount
            Else
                LogAppend("No spells found!", "CharacterSpellsHandler_loadAtArcemu", True)
            End If
        Catch ex As Exception
            LogAppend("Something went wrong while loading character spells! -> skipping -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterSpellsHandler_loadAtArcemu", True, True)
            Exit Sub
        End Try
        SetTemporaryCharacterInformation("@character_spells", ConvertListToString(templist), tar_setId)
      End Sub
    Private Shared Sub loadAtTrinity(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)
        LogAppend("Loading character spells @loadAtTrinity", "CharacterSpellsHandler_loadAtTrinity", False)
        Dim tempdt As DataTable = ReturnDataTable("SELECT spell, active, disabled FROM character_spell WHERE guid='" & charguid.ToString() & "'")
        Dim templist As New List(Of String)
        Try
            Dim lastcount As Integer = CInt(Val(tempdt.Rows.Count.ToString))
            Dim count As Integer = 0
            If Not lastcount = 0 Then
               Do
                    Dim readedcode As String = (tempdt.Rows(count).Item(0)).ToString
                    Dim spell As String = readedcode
                    Dim active As String = (tempdt.Rows(count).Item(1)).ToString
                    Dim disabled As String = (tempdt.Rows(count).Item(2)).ToString
                    templist.Add("<spell>" & spell & "</spell><active>" & active & "</active><disabled>" & disabled & "</disabled>")
                   count += 1
                Loop Until count = lastcount
            Else
                LogAppend("No spells found!", "CharacterSpellsHandler_loadAtTrinity", True)
            End If
        Catch ex As Exception
            LogAppend("Something went wrong while loading character spells! -> skipping -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterSpellsHandler_loadAtTrinity", True, True)
            Exit Sub
        End Try
        SetTemporaryCharacterInformation("@character_spells", ConvertListToString(templist), tar_setId)
    End Sub
    Private Shared Sub loadAtTrinityTBC(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)

    End Sub
    Private Shared Sub loadAtMangos(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)
        LogAppend("Loading character spells @loadAtMangos", "CharacterSpellsHandler_loadAtMangos", False)
        Dim tempdt As DataTable = ReturnDataTable("SELECT spell, active, disabled FROM character_spell WHERE guid='" & charguid.ToString() & "'")
        Dim templist As New List(Of String)
        Try
            Dim lastcount As Integer = CInt(Val(tempdt.Rows.Count.ToString))
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim readedcode As String = (tempdt.Rows(count).Item(0)).ToString
                    Dim spell As String = readedcode
                    Dim active As String = (tempdt.Rows(count).Item(1)).ToString
                    Dim disabled As String = (tempdt.Rows(count).Item(2)).ToString
                    templist.Add("<spell>" & spell & "</spell><active>" & active & "</active><disabled>" & disabled & "</disabled>")
                    count += 1
                Loop Until count = lastcount
            Else
                LogAppend("No spells found!", "CharacterSpellsHandler_loadAtMangos", True)
            End If
        Catch ex As Exception
            LogAppend("Something went wrong while loading character spells! -> skipping -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterSpellsHandler_loadAtMangos", True, True)
            Exit Sub
        End Try
        SetTemporaryCharacterInformation("@character_spells", ConvertListToString(templist), tar_setId)
    End Sub
End Class
