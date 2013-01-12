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
        Dim secglyphstring As String = runSQLCommand_characters_string("SELECT glyphs2 from characters WHERE guid='" & charguid.ToString & "'")
       'Spec 0
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
        'Spec 1
        Try
            Dim parts() As String = secglyphstring.Split(","c)
            Dim prevglyphid As Integer = CInt(Val(parts(0)))
            If prevglyphid > 1 Then SetTemporaryCharacterInformation("@character_secmajorglyph1", GetGlyphIdByItemId(prevglyphid), tar_setId)
        Catch ex As Exception
            LogAppend("Error while loading secmajorglyph1! -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterGlyphsHandler_loadAtArcemu", False, True)
        End Try
        Try
            Dim parts() As String = secglyphstring.Split(","c)
            Dim prevglyphid As Integer = CInt(Val(parts(3)))
            If prevglyphid > 1 Then SetTemporaryCharacterInformation("@character_secmajorglyph2", GetGlyphIdByItemId(prevglyphid), tar_setId)
        Catch ex As Exception
            LogAppend("Error while loading secmajorglyph2! -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterGlyphsHandler_loadAtArcemu", False, True)
        End Try
        Try
            Dim parts() As String = secglyphstring.Split(","c)
            Dim prevglyphid As Integer = CInt(Val(parts(5)))
            If prevglyphid > 1 Then SetTemporaryCharacterInformation("@character_secmajorglyph3", GetGlyphIdByItemId(prevglyphid), tar_setId)
        Catch ex As Exception
            LogAppend("Error while loading secmajorglyph3! -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterGlyphsHandler_loadAtArcemu", False, True)
        End Try
        Try
            Dim parts() As String = secglyphstring.Split(","c)
            Dim prevglyphid As Integer = CInt(Val(parts(1)))
            If prevglyphid > 1 Then SetTemporaryCharacterInformation("@character_secminorglyph1", GetGlyphIdByItemId(prevglyphid), tar_setId)
        Catch ex As Exception
            LogAppend("Error while loading secminorglyph1! -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterGlyphsHandler_loadAtArcemu", False, True)
        End Try
        Try
            Dim parts() As String = secglyphstring.Split(","c)
            Dim prevglyphid As Integer = CInt(Val(parts(2)))
            If prevglyphid > 1 Then SetTemporaryCharacterInformation("@character_secminorglyph2", GetGlyphIdByItemId(prevglyphid), tar_setId)
        Catch ex As Exception
            LogAppend("Error while loading secminorglyph2! -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterGlyphsHandler_loadAtArcemu", False, True)
        End Try
        Try
            Dim parts() As String = secglyphstring.Split(","c)
            Dim prevglyphid As Integer = CInt(Val(parts(4)))
            If prevglyphid > 1 Then SetTemporaryCharacterInformation("@character_secminorglyph3", GetGlyphIdByItemId(prevglyphid), tar_setId)
        Catch ex As Exception
            LogAppend("Error while loading secminorglyph3! -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterGlyphsHandler_loadAtArcemu", False, True)
        End Try
      End Sub
    Private Shared Sub loadAtTrinity(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)
        LogAppend("Loading character Glyphs @loadAtTrinity", "CharacterGlyphsHandler_loadAtTrinity", False)
        Dim tempdt As New DataTable
        Dim tempdtsec As New DataTable
        If expansion = 3 Then
            tempdt = ReturnDataTable("SELECT glyph1, glyph2, glyph3, glyph4, glyph5, glyph6 FROM character_glyphs WHERE guid='" & charguid.ToString & "' AND spec='0'")
            tempdtsec = ReturnDataTable("SELECT glyph1, glyph2, glyph3, glyph4, glyph5, glyph6 FROM character_glyphs WHERE guid='" & charguid.ToString & "' AND spec='1'")
        Else
            tempdt = ReturnDataTable("SELECT glyph1, glyph2, glyph3, glyph4, glyph5, glyph6, glyph7, glyph8, glyph9 FROM character_glyphs WHERE guid='" & charguid.ToString & "' AND spec='0'")
            tempdtsec = ReturnDataTable("SELECT glyph1, glyph2, glyph3, glyph4, glyph5, glyph6, glyph7, glyph8, glyph9 FROM character_glyphs WHERE guid='" & charguid.ToString & "' AND spec='1'")
        End If
        Dim prevglyphid As Integer
        Dim lastcount As Integer = CInt(Val(tempdt.Rows.Count.ToString))
        If Not lastcount = 0 Then
            prevglyphid = CInt(Val((tempdt.Rows(0).Item(0)).ToString))
            If prevglyphid > 1 Then SetTemporaryCharacterInformation("@character_majorglyph1", GetGlyphIdByItemId(prevglyphid), tar_setId)
            prevglyphid = CInt(Val((tempdt.Rows(0).Item(1)).ToString))
            If prevglyphid > 1 Then SetTemporaryCharacterInformation("@character_minorglyph1", GetGlyphIdByItemId(prevglyphid), tar_setId)
            prevglyphid = CInt(Val((tempdt.Rows(0).Item(2)).ToString))
            If prevglyphid > 1 Then SetTemporaryCharacterInformation("@character_minorglyph2", GetGlyphIdByItemId(prevglyphid), tar_setId)
            prevglyphid = CInt(Val((tempdt.Rows(0).Item(3)).ToString))
            If prevglyphid > 1 Then SetTemporaryCharacterInformation("@character_majorglyph2", GetGlyphIdByItemId(prevglyphid), tar_setId)
            prevglyphid = CInt(Val((tempdt.Rows(0).Item(4)).ToString))
            If prevglyphid > 1 Then SetTemporaryCharacterInformation("@character_minorglyph3", GetGlyphIdByItemId(prevglyphid), tar_setId)
            prevglyphid = CInt(Val((tempdt.Rows(0).Item(5)).ToString))
            If prevglyphid > 1 Then SetTemporaryCharacterInformation("@character_majorglyph3", GetGlyphIdByItemId(prevglyphid), tar_setId)
            If expansion = 4 Then
                prevglyphid = CInt(Val((tempdt.Rows(0).Item(6)).ToString))
                If prevglyphid > 1 Then SetTemporaryCharacterInformation("@character_primeglyph1", GetGlyphIdByItemId(prevglyphid), tar_setId)
                prevglyphid = CInt(Val((tempdt.Rows(0).Item(7)).ToString))
                If prevglyphid > 1 Then SetTemporaryCharacterInformation("@character_primeglyph2", GetGlyphIdByItemId(prevglyphid), tar_setId)
                prevglyphid = CInt(Val((tempdt.Rows(0).Item(8)).ToString))
                If prevglyphid > 1 Then SetTemporaryCharacterInformation("@character_primeglyph3", GetGlyphIdByItemId(prevglyphid), tar_setId)
            End If
        Else
            LogAppend("No Glyphs found (spec 0)!", "CharacterGlyphsHandler_loadAtTrinity", True)
        End If
        lastcount = CInt(Val(tempdtsec.Rows.Count.ToString))
        If Not lastcount = 0 Then
            prevglyphid = CInt(Val((tempdtsec.Rows(0).Item(0)).ToString))
            If prevglyphid > 1 Then SetTemporaryCharacterInformation("@character_secmajorglyph1", GetGlyphIdByItemId(prevglyphid), tar_setId)
            prevglyphid = CInt(Val((tempdtsec.Rows(0).Item(1)).ToString))
            If prevglyphid > 1 Then SetTemporaryCharacterInformation("@character_secminorglyph1", GetGlyphIdByItemId(prevglyphid), tar_setId)
            prevglyphid = CInt(Val((tempdtsec.Rows(0).Item(2)).ToString))
            If prevglyphid > 1 Then SetTemporaryCharacterInformation("@character_secminorglyph2", GetGlyphIdByItemId(prevglyphid), tar_setId)
            prevglyphid = CInt(Val((tempdtsec.Rows(0).Item(3)).ToString))
            If prevglyphid > 1 Then SetTemporaryCharacterInformation("@character_secmajorglyph2", GetGlyphIdByItemId(prevglyphid), tar_setId)
            prevglyphid = CInt(Val((tempdtsec.Rows(0).Item(4)).ToString))
            If prevglyphid > 1 Then SetTemporaryCharacterInformation("@character_secminorglyph3", GetGlyphIdByItemId(prevglyphid), tar_setId)
            prevglyphid = CInt(Val((tempdtsec.Rows(0).Item(5)).ToString))
            If prevglyphid > 1 Then SetTemporaryCharacterInformation("@character_secmajorglyph3", GetGlyphIdByItemId(prevglyphid), tar_setId)
            If expansion = 4 Then
                prevglyphid = CInt(Val((tempdtsec.Rows(0).Item(6)).ToString))
                If prevglyphid > 1 Then SetTemporaryCharacterInformation("@character_secprimeglyph1", GetGlyphIdByItemId(prevglyphid), tar_setId)
                prevglyphid = CInt(Val((tempdtsec.Rows(0).Item(7)).ToString))
                If prevglyphid > 1 Then SetTemporaryCharacterInformation("@character_secprimeglyph2", GetGlyphIdByItemId(prevglyphid), tar_setId)
                prevglyphid = CInt(Val((tempdtsec.Rows(0).Item(8)).ToString))
                If prevglyphid > 1 Then SetTemporaryCharacterInformation("@character_secprimeglyph3", GetGlyphIdByItemId(prevglyphid), tar_setId)
            End If
        Else
            LogAppend("No Glyphs found (spec 1)!", "CharacterGlyphsHandler_loadAtTrinity", True)
        End If
        End Sub
    Private Shared Sub loadAtTrinityTBC(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)

    End Sub
    Private Shared Sub loadAtMangos(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)
        LogAppend("Loading character Glyphs @loadAtMangos", "CharacterGlyphsHandler_loadAtMangos", False)
        Dim tempdt As DataTable = ReturnDataTable("SELECT glyph, slot, spec FROM character_glyphs WHERE guid='" & charguid.ToString & "'")
        Dim prevglyphid As Integer
        Dim slot As Integer
        Dim spec As Integer
        Dim resultquantity As Integer = tempdt.Rows.Count
        Dim proccounter As Integer = 0
        Do
            Try
                prevglyphid = CInt(Val((tempdt.Rows(proccounter).Item(0)).ToString))
                If prevglyphid > 1 Then
                    slot = CInt(Val((tempdt.Rows(proccounter).Item(1)).ToString))
                    spec = CInt(Val((tempdt.Rows(proccounter).Item(2)).ToString))
                    Select Case spec
                        Case 0
                            Select Case slot
                                Case 0
                                    SetTemporaryCharacterInformation("@character_majorglyph1", GetGlyphIdByItemId(prevglyphid), tar_setId)
                                Case 1
                                    SetTemporaryCharacterInformation("@character_minorglyph2", GetGlyphIdByItemId(prevglyphid), tar_setId)
                                Case 2
                                    SetTemporaryCharacterInformation("@character_minorglyph3", GetGlyphIdByItemId(prevglyphid), tar_setId)
                                Case 3
                                    SetTemporaryCharacterInformation("@character_majorglyph2", GetGlyphIdByItemId(prevglyphid), tar_setId)
                                Case 4
                                    SetTemporaryCharacterInformation("@character_minorglyph1", GetGlyphIdByItemId(prevglyphid), tar_setId)
                                Case 5
                                    SetTemporaryCharacterInformation("@character_majorglyph3", GetGlyphIdByItemId(prevglyphid), tar_setId)
                                Case 6
                                    SetTemporaryCharacterInformation("@character_primeglyph1", GetGlyphIdByItemId(prevglyphid), tar_setId)
                                Case 7
                                    SetTemporaryCharacterInformation("@character_primeglyph2", GetGlyphIdByItemId(prevglyphid), tar_setId)
                                Case 8
                                    SetTemporaryCharacterInformation("@character_primeglyph3", GetGlyphIdByItemId(prevglyphid), tar_setId)
                                Case Else : End Select
                        Case 1
                            Select Case slot
                                Case 0
                                    SetTemporaryCharacterInformation("@character_secmajorglyph1", GetGlyphIdByItemId(prevglyphid), tar_setId)
                                Case 1
                                    SetTemporaryCharacterInformation("@character_secminorglyph2", GetGlyphIdByItemId(prevglyphid), tar_setId)
                                Case 2
                                    SetTemporaryCharacterInformation("@character_secminorglyph3", GetGlyphIdByItemId(prevglyphid), tar_setId)
                                Case 3
                                    SetTemporaryCharacterInformation("@character_secmajorglyph2", GetGlyphIdByItemId(prevglyphid), tar_setId)
                                Case 4
                                    SetTemporaryCharacterInformation("@character_secminorglyph1", GetGlyphIdByItemId(prevglyphid), tar_setId)
                                Case 5
                                    SetTemporaryCharacterInformation("@character_secmajorglyph3", GetGlyphIdByItemId(prevglyphid), tar_setId)
                                Case 6
                                    SetTemporaryCharacterInformation("@character_secprimeglyph1", GetGlyphIdByItemId(prevglyphid), tar_setId)
                                Case 7
                                    SetTemporaryCharacterInformation("@character_secprimeglyph2", GetGlyphIdByItemId(prevglyphid), tar_setId)
                                Case 8
                                    SetTemporaryCharacterInformation("@character_secprimeglyph3", GetGlyphIdByItemId(prevglyphid), tar_setId)
                                Case Else : End Select
                        Case Else : End Select
                   End If
            Catch ex As Exception
                LogAppend("Something went wrong while loading character Glyphs -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterGlyphsHandler_loadAtMangos", True, True)
            End Try
            proccounter += 1
        Loop Until proccounter = resultquantity
    End Sub
End Class
