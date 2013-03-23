'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013 Namcore Studio <https://github.com/megasus/Namcore-Studio>
'*
'* This program is free software; you can redistribute it and/or modify it
'* under the terms of the GNU General Public License as published by the
'* Free Software Foundation; either version 2 of the License, or (at your
'* option) any later version.
'*
'* This program is distributed in the hope that it will be useful, but WITHOUT
'* ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
'* FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for
'* more details.
'*
'* You should have received a copy of the GNU General Public License along
'* with this program. If not, see <http://www.gnu.org/licenses/>.
'*
'* Developed by Alcanmage/megasus
'*
'* //FileInfo//
'*      /Filename:      CharacterTalentsHandler
'*      /Description:   Contains functions for extracting information about the known
'*                      talents of a specific character
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Imports Namcore_Studio.EventLogging
Imports Namcore_Studio.Basics
Imports Namcore_Studio.GlobalVariables
Imports Namcore_Studio.CommandHandler
Imports Namcore_Studio.Conversions
Public Class CharacterTalentsHandler
    Private Shared SDatatable As New DataTable
    Public Shared Sub GetCharacterTalents(ByVal characterGuid As Integer, ByVal setId As Integer, ByVal accountId As Integer)
        LogAppend("Loading character talents for characterGuid: " & characterGuid & " and setId: " & setId, "CharacterTalentsHandler_GetCharacterTalents", True)
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
        SDatatable.Clear()
        SDatatable.Dispose()
        SDatatable = gettable()
        LogAppend("Loading character talents @loadAtArcemu", "CharacterTalentsHandler_loadAtArcemu", False)
        Dim templist As New List(Of String)
        Dim talentstring As String =
        runSQLCommand_characters_string("SELECT " & sourceStructure.char_talent1_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        If talentstring.Contains(",") Then
            Dim excounter As Integer = UBound(talentstring.Split(CChar(",")))
            Dim startcounter As Integer = 0
            Do
                Dim parts() As String = talentstring.Split(","c)
                Dim ctalentid As String = parts(startcounter)
                startcounter += 1
                Dim rurrentrank As String = (tryint(parts(startcounter)) + 1).ToString()
                startcounter += 1
                templist.Add("<spell>" & checkfield(ctalentid, rurrentrank) & "</spell><spec>0</spec>")
            Loop Until startcounter = excounter
        End If
        Dim talentstring2 As String =
        runSQLCommand_characters_string("SELECT " & sourceStructure.char_talent2_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
        If talentstring2.Contains(",") Then
            Dim excounter As Integer = UBound(talentstring2.Split(CChar(",")))
            Dim startcounter As Integer = 0
            Do
                Dim parts() As String = talentstring2.Split(","c)
                Dim ctalentid As String = parts(startcounter)
                startcounter += 1
                Dim rurrentrank As String = (tryint(parts(startcounter)) + 1).ToString()
                startcounter += 1
                templist.Add("<spell>" & checkfield(ctalentid, rurrentrank) & "</spell><spec>1</spec>")
            Loop Until startcounter = excounter
        End If
        SetTemporaryCharacterInformation("@character_talents", ConvertListToString(templist), tar_setId)
    End Sub
    Private Shared Sub loadAtTrinity(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)
        LogAppend("Loading character talents @loadAtTrinity", "CharacterTalentsHandler_loadAtTrinity", False)
        Dim tempdt As DataTable = ReturnDataTable("SELECT " & sourceStructure.talent_spell_col(0) & " FROM " & sourceStructure.character_talent_tbl(0) & " WHERE " &
                                                  sourceStructure.talent_guid_col(0) & "='" & charguid.ToString & "' AND " & sourceStructure.talent_spec_col(0) & "='0'")
        Dim templist As New List(Of String)
        Try
            Dim lastcount As Integer = tryint(Val(tempdt.Rows.Count.ToString))
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim spell As String = (tempdt.Rows(count).Item(0)).ToString
                    templist.Add("<spell>" & spell & "</spell><spec>0</spec>")
                    count += 1
                Loop Until count = lastcount
            Else
                LogAppend("No talents found (spec 0)!", "CharacterTalentsHandler_loadAtTrinity", True)
            End If
        Catch ex As Exception
            LogAppend("Something went wrong while loading character talents (spec 0)! -> skipping -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterTalentsHandler_loadAtTrinity", True, True)
            Exit Sub
        End Try
        Dim tempdt2 As DataTable = ReturnDataTable("SELECT " & sourceStructure.talent_spell_col(0) & " FROM " & sourceStructure.character_talent_tbl(0) & " WHERE " & sourceStructure.talent_guid_col(0) &
                                                   "='" & charguid.ToString & "' AND " & sourceStructure.talent_spec_col(0) & "='1'")
        Try
            Dim lastcount As Integer = tryint(Val(tempdt2.Rows.Count.ToString))
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim spell As String = (tempdt2.Rows(count).Item(0)).ToString
                    templist.Add("<spell>" & spell & "</spell><spec>1</spec>")
                    count += 1
                Loop Until count = lastcount
            Else
                LogAppend("No talents found (spec 1)!", "CharacterTalentsHandler_loadAtTrinity", True)
            End If
        Catch ex As Exception
            LogAppend("Something went wrong while loading character talents (spec 1)! -> skipping -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterTalentsHandler_loadAtTrinity", True, True)
            Exit Sub
        End Try
        SetTemporaryCharacterInformation("@character_talents", ConvertListToString(templist), tar_setId)
    End Sub
    Private Shared Sub loadAtTrinityTBC(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)

    End Sub
    Private Shared Sub loadAtMangos(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)
        SDatatable.Clear()
        SDatatable.Dispose()
        SDatatable = gettable()
        LogAppend("Loading character talents @loadAtMangos", "CharacterTalentsHandler_loadAtMangos", False)
        Dim tempdt As DataTable = ReturnDataTable("SELECT " & sourceStructure.talent_talent_col(0) & ", " & sourceStructure.talent_rank_col(0) & " FROM " & sourceStructure.character_talent_tbl(0) &
                                                  " WHERE " & sourceStructure.talent_spec_col(0) & "='" & charguid.ToString() & "' AND " & sourceStructure.talent_spec_col(0) & "='0'")
        Dim templist As New List(Of String)
        Try
            Dim lastcount As Integer = tryint(Val(tempdt.Rows.Count.ToString))
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim idtalent As String = (tempdt.Rows(count).Item(0)).ToString
                    Dim currentrank As String = (tempdt.Rows(count).Item(1)).ToString
                    templist.Add("<spell>" & checkfield(idtalent, currentrank) & "</spell><spec>0</spec>")
                    count += 1
                Loop Until count = lastcount
            Else
                LogAppend("No talents found (spec 0)!", "CharacterTalentsHandler_loadAtMangos", True)
            End If
        Catch ex As Exception
            LogAppend("Something went wrong while loading character talents (spec 0)! -> skipping -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterTalentsHandler_loadAtMangos", True, True)
            Exit Sub
        End Try
        Dim tempdt2 As DataTable = ReturnDataTable("SELECT " & sourceStructure.talent_talent_col(0) & ", " & sourceStructure.talent_rank_col(0) & " FROM " & sourceStructure.character_talent_tbl(0) &
                                                   " WHERE " & sourceStructure.talent_guid_col(0) & "='" & charguid.ToString() & "' AND " & sourceStructure.talent_spec_col(0) & "='1'")
        Try
            Dim lastcount As Integer = tryint(Val(tempdt2.Rows.Count.ToString))
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim idtalent As String = (tempdt2.Rows(count).Item(0)).ToString
                    Dim currentrank As String = (tempdt2.Rows(count).Item(1)).ToString
                    templist.Add("<spell>" & checkfield(idtalent, currentrank) & "</spell><spec>1</spec>")
                    count += 1
                Loop Until count = lastcount
            Else
                LogAppend("No talents found (spec 1)!", "CharacterTalentsHandler_loadAtMangos", True)
            End If
        Catch ex As Exception
            LogAppend("Something went wrong while loading character talents (spec 1)! -> skipping -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterTalentsHandler_loadAtMangos", True, True)
            Exit Sub
        End Try
        SetTemporaryCharacterInformation("@character_talents", ConvertListToString(templist), tar_setId)
    End Sub

    Private Shared Function gettable() As DataTable
        Try
            Dim dt As New DataTable()
            Dim stext As String = My.Resources.Talent
            Dim a() As String
            Dim strArray As String()
            a = Split(stext, vbNewLine)
            For i = 0 To UBound(a)
                strArray = a(i).Split(CChar(";"))
                If i = 0 Then
                    For Each value As String In strArray
                        dt.Columns.Add(value.Trim())
                    Next
                Else
                    Dim dr As DataRow = dt.NewRow()
                    dt.Rows.Add(strArray)
                End If
            Next i
            Return dt
        Catch ex As Exception
            LogAppend("Failed to load new talent datatable! -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterTalentsHandler_gettable", False, True)
            Return New DataTable
        End Try
    End Function
    Private Shared Function checkfield(ByVal lID As String, ByVal rank As String) As String
        LogAppend("Loading SpellId of Talent " & lID & " with rank " & rank, "CharacterTalentsHandler_checkfield", False)
        Dim _byRNK As String = executex("TalentId", lID, tryint(Val(rank)))
        If rank = "0" Then
            LogAppend("Talent Rank is 0 -> Returning " & lID & "clear", "CharacterTalentsHandler_checkfield", False)
            Return lID & "clear"
        ElseIf Not _byRNK = "-" Then
            LogAppend("SpellID is " & _byRNK, "CharacterTalentsHandler_checkfield", False)
            Return _byRNK
        Else
            LogAppend("SpellID " & lID & " not found! -> Returning 0", "CharacterTalentsHandler_checkfield", False, True)
            Return "0"
        End If
    End Function

    Private Shared Function executex(ByVal field As String, ByVal sID As String, ByVal rank As Integer) As String
        Try
            Dim foundRows() As DataRow
            foundRows = sdatatable.Select(field & " = '" & sID & "'")
            If foundRows.Length = 0 Then
                Return "-"
                Else
                Dim i As Integer
                Dim tmpreturn As String = "-"
                For i = 0 To foundRows.GetUpperBound(0)
                    tmpreturn = (foundRows(i)(rank)).ToString
                    Next i
                Return tmpreturn
            End If
        Catch ex As Exception
            LogAppend("Something went wrong while checking talent dt for TalentId " & sID & " & rank " & rank & " -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterTalentsHandler_executex", False, True)
            Return "-"
        End Try
    End Function
End Class
