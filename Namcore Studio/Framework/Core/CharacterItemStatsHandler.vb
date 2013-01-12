Imports Namcore_Studio.EventLogging
Imports Namcore_Studio.Basics
Imports Namcore_Studio.GlobalVariables
Imports Namcore_Studio.CommandHandler
Imports Namcore_Studio.Conversions
Public Class CharacterItemStatsHandler
    Public Shared Sub GetItemStats(ByVal CItemguid As Integer, ByVal saveVar As String, ByVal setId As Integer)
        LogAppend("Loading character ItemStats for item: " & CItemguid.ToString() & " and setId: " & setId, "CharacterItemStatssHandler_GetItemStats", True)
        Select Case sourceCore
            Case "arcemu"
                loadAtArcemu(CItemguid, setId, saveVar)
            Case "trinity"
                loadAtTrinity(CItemguid, setId, saveVar)
            Case "trinitytbc"
                loadAtTrinityTBC(CItemguid, setId, saveVar)
            Case "mangos"
                loadAtMangos(CItemguid, setId, saveVar)
            Case Else

        End Select

    End Sub
    Private Shared Sub loadAtArcemu(ByVal item As Integer, ByVal tar_setId As Integer, ByVal info_var As String)
        LogAppend("Loading ItemStats @loadAtArcemu", "CharacterItemStatsHandler_loadAtArcemu", False)
        Try
            SetTemporaryCharacterInformation("@character_" & info_var & "_ench", runSQLCommand_characters_string("SELECT enchantments FROM playeritems WHERE guid='" & item.ToString & "'"), tar_setId)
        Catch ex As Exception
            SetTemporaryCharacterInformation("@character_" & info_var & "_ench", "-1", tar_setId)
        End Try
        End Sub
    Private Shared Sub loadAtTrinity(ByVal item As Integer, ByVal tar_setId As Integer, ByVal info_var As String)
        LogAppend("Loading ItemStats @loadAtTrinity", "CharacterItemStatsHandler_loadAtTrinity", False)
        Try
            SetTemporaryCharacterInformation("@character_" & info_var & "_ench", runSQLCommand_characters_string("SELECT enchantments FROM item_instance WHERE guid='" & item.ToString & "'"), tar_setId)
        Catch ex As Exception
            SetTemporaryCharacterInformation("@character_" & info_var & "_ench", "-1", tar_setId)
        End Try
       End Sub
    Private Shared Sub loadAtTrinityTBC(ByVal item As Integer, ByVal tar_setId As Integer, ByVal info_var As String)

    End Sub
    Private Shared Sub loadAtMangos(ByVal item As Integer, ByVal tar_setId As Integer, ByVal info_var As String)
        LogAppend("Loading character ItemStats @loadAtMangos", "CharacterItemStatsHandler_loadAtMangos", False)
        Try
            SetTemporaryCharacterInformation("@character_" & info_var & "_ench", runSQLCommand_characters_string("SELECT `data` FROM item_instance WHERE guid='" & item.ToString & "'"), tar_setId)
        Catch ex As Exception
            SetTemporaryCharacterInformation("@character_" & info_var & "_ench", "-1", tar_setId)
        End Try
    End Sub
End Class
