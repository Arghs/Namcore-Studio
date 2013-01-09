Imports Namcore_Studio.EventLogging
Imports Namcore_Studio.Basics
Imports Namcore_Studio.GlobalVariables
Imports Namcore_Studio.CommandHandler
Imports Namcore_Studio.Conversions
Imports Namcore_Studio.SpellItem_Information
Public Class CharacterGlyphsHandler

    Public Shared Sub GetCharacterGlyphs(ByVal charguid As Integer, ByVal setId As Integer, ByVal accountId As Integer)
        LogAppend("Loading character Glyphs for charguid: " & charguid & " and setId: " & setId, "CharacterGlyphsHandler_GetCharacterGlyphs", True)
        Select Case sourceCore
            Case "arcemu"
                loadAtArcemu(charguid, setId, accountId)
            Case "trinity"
                loadAtTrinity(charguid, setId, accountId)
            Case "trinitytbc"
                loadAtTrinityTBC(charguid, setId, accountId)
            Case "mangos"
                loadAtMangos(charguid, setId, accountId)
            Case Else

        End Select

    End Sub
    Private Shared Sub loadAtArcemu(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)
        LogAppend("Loading character Glyphs @loadAtArcemu", "CharacterGlyphsHandler_loadAtArcemu", False)
        Dim glyphname As String = ""
        Dim glyphpic As Image = My.Resources.empty
        Dim glyphstring As String = runSQLCommand_characters_string("SELECT glyphs1 from characters WHERE guid='" & charguid.ToString & "'")
        Try
            Dim parts() As String = glyphstring.Split(","c)
            Dim prevglyphid As Integer = CInt(Val(parts(0)))
            If prevglyphid > 1 Then SetTemporaryCharacterInformation("@character_majorglyph1", GetGlyphIdByItemId(prevglyphid), tar_setId)
        Catch ex As Exception
            LogAppend("Error while loading majorglyph1! -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterGlyphsHandler_loadAtArcemu", False, True)
        End Try
        Try
            Dim parts() As String = glyphstring.Split(","c)
            Dim prevglyphid As Integer = CInt(Val(parts(3)))
            If prevglyphid > 1 Then SetTemporaryCharacterInformation("@character_majorglyph2", GetGlyphIdByItemId(prevglyphid), tar_setId)
        Catch ex As Exception
            LogAppend("Error while loading majorglyph2! -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterGlyphsHandler_loadAtArcemu", False, True)
        End Try
        Try
            Dim parts() As String = glyphstring.Split(","c)
            Dim prevglyphid As Integer = CInt(Val(parts(5)))
            If prevglyphid > 1 Then SetTemporaryCharacterInformation("@character_majorglyph3", GetGlyphIdByItemId(prevglyphid), tar_setId)
        Catch ex As Exception
            LogAppend("Error while loading majorglyph3! -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterGlyphsHandler_loadAtArcemu", False, True)
        End Try
        Try
            Dim parts() As String = glyphstring.Split(","c)
            Dim prevglyphid As Integer = CInt(Val(parts(1)))
            If prevglyphid > 1 Then SetTemporaryCharacterInformation("@character_minorglyph1", GetGlyphIdByItemId(prevglyphid), tar_setId)
        Catch ex As Exception
            LogAppend("Error while loading minorglyph1! -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterGlyphsHandler_loadAtArcemu", False, True)
        End Try
        Try
            Dim parts() As String = glyphstring.Split(","c)
            Dim prevglyphid As Integer = CInt(Val(parts(2)))
            If prevglyphid > 1 Then SetTemporaryCharacterInformation("@character_minorglyph2", GetGlyphIdByItemId(prevglyphid), tar_setId)
        Catch ex As Exception
            LogAppend("Error while loading minorglyph2! -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterGlyphsHandler_loadAtArcemu", False, True)
        End Try
        Try
            Dim parts() As String = glyphstring.Split(","c)
            Dim prevglyphid As Integer = CInt(Val(parts(4)))
            If prevglyphid > 1 Then SetTemporaryCharacterInformation("@character_minorglyph3", GetGlyphIdByItemId(prevglyphid), tar_setId)
        Catch ex As Exception
            LogAppend("Error while loading minorglyph3! -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterGlyphsHandler_loadAtArcemu", False, True)
        End Try
      End Sub
    Private Shared Sub loadAtTrinity(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)
        LogAppend("Loading character Glyphs @loadAtTrinity", "CharacterGlyphsHandler_loadAtTrinity", False)
        Dim tempdt As DataTable = ReturnDataTable("SELECT spell FROM character_talent WHERE guid='" & charguid.ToString & "' AND spec='0'")
        Dim templist As New List(Of String)
        Try
            Dim lastcount As Integer = CInt(Val(tempdt.Rows.Count.ToString))
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim spell As String = (tempdt.Rows(count).Item(0)).ToString
                    templist.Add("<spell>" & spell & "</spell><spec>0</spec>")
                    count += 1
                Loop Until count = lastcount
            Else
                LogAppend("No Glyphs found (spec 0)!", "CharacterGlyphsHandler_loadAtTrinity", True)
            End If
        Catch ex As Exception
            LogAppend("Something went wrong while loading character Glyphs (spec 0)! -> skipping -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterGlyphsHandler_loadAtTrinity", True, True)
            Exit Sub
        End Try
        Dim tempdt2 As DataTable = ReturnDataTable("SELECT spell FROM character_talent WHERE guid='" & charguid.ToString & "' AND spec='1'")
        Try
            Dim lastcount As Integer = CInt(Val(tempdt2.Rows.Count.ToString))
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim spell As String = (tempdt2.Rows(count).Item(0)).ToString
                    templist.Add("<spell>" & spell & "</spell><spec>1</spec>")
                    count += 1
                Loop Until count = lastcount
            Else
                LogAppend("No Glyphs found (spec 1)!", "CharacterGlyphsHandler_loadAtTrinity", True)
            End If
        Catch ex As Exception
            LogAppend("Something went wrong while loading character Glyphs (spec 1)! -> skipping -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterGlyphsHandler_loadAtTrinity", True, True)
            Exit Sub
        End Try
        SetTemporaryCharacterInformation("@character_Glyphs", ConvertListToString(templist), tar_setId)
    End Sub
    Private Shared Sub loadAtTrinityTBC(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)

    End Sub
    Private Shared Sub loadAtMangos(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)

        LogAppend("Loading character Glyphs @loadAtMangos", "CharacterGlyphsHandler_loadAtMangos", False)
        Dim tempdt As DataTable = ReturnDataTable("SELECT talent_id, current_rank FROM charactertalent WHERE guid='" & charguid.ToString() & "' AND spec='0'")
        Dim templist As New List(Of String)
        Try
            Dim lastcount As Integer = CInt(Val(tempdt.Rows.Count.ToString))
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim idtalent As String = (tempdt.Rows(count).Item(0)).ToString
                    Dim currentrank As String = (tempdt.Rows(count).Item(1)).ToString
                    '     templist.Add("<spell>" & checkfield(idtalent, currentrank) & "</spell><spec>0</spec>")
                    count += 1
                Loop Until count = lastcount
            Else
                LogAppend("No Glyphs found (spec 0)!", "CharacterGlyphsHandler_loadAtMangos", True)
            End If
        Catch ex As Exception
            LogAppend("Something went wrong while loading character Glyphs (spec 0)! -> skipping -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterGlyphsHandler_loadAtMangos", True, True)
            Exit Sub
        End Try
        Dim tempdt2 As DataTable = ReturnDataTable("SELECT talent_id, current_rank FROM charactertalent WHERE guid='" & charguid.ToString() & "' AND spec='1'")
        Try
            Dim lastcount As Integer = CInt(Val(tempdt2.Rows.Count.ToString))
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim idtalent As String = (tempdt2.Rows(count).Item(0)).ToString
                    Dim currentrank As String = (tempdt2.Rows(count).Item(1)).ToString
                    '  templist.Add("<spell>" & checkfield(idtalent, currentrank) & "</spell><spec>1</spec>")
                    count += 1
                Loop Until count = lastcount
            Else
                LogAppend("No Glyphs found (spec 1)!", "CharacterGlyphsHandler_loadAtMangos", True)
            End If
        Catch ex As Exception
            LogAppend("Something went wrong while loading character Glyphs (spec 1)! -> skipping -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterGlyphsHandler_loadAtMangos", True, True)
            Exit Sub
        End Try
        SetTemporaryCharacterInformation("@character_Glyphs", ConvertListToString(templist), tar_setId)
    End Sub





End Class
