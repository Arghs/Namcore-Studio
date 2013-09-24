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
Imports NCFramework.Framework.Database
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Modules

Namespace Framework.Core
    Public Class CharacterTalentsHandler

        '// Declaration
        Private _sDatatable As New DataTable
        '// Declaration

        Public Sub GetCharacterTalents(ByVal characterGuid As Integer, ByVal setId As Integer, ByVal accountId As Integer)
            LogAppend("Loading character talents for characterGuid: " & characterGuid & " and setId: " & setId,
                      "CharacterTalentsHandler_GetCharacterTalents", True)
            Select Case GlobalVariables.sourceCore
                Case "arcemu"
                    LoadAtArcemu(characterGuid, setId)
                Case "trinity"
                    LoadAtTrinity(characterGuid, setId)
                Case "trinitytbc"
                    'todo  LoadAtTrinityTBC(characterGuid, setId, accountId)
                Case "mangos"
                    LoadAtMangos(characterGuid, setId)
            End Select
        End Sub

        Private Sub LoadAtArcemu(ByVal charguid As Integer, ByVal tarSetId As Integer)
            _sDatatable.Clear()
            _sDatatable.Dispose()
            _sDatatable = gettable()
            LogAppend("Loading character talents @LoadAtArcemu", "CharacterTalentsHandler_LoadAtArcemu", False)
            Dim player As Character = GetCharacterSetBySetId(tarSetId)
            If player.Talents Is Nothing Then player.Talents = New List(Of Talent)()
            Dim talentstring As String =
                    runSQLCommand_characters_string(
                        "SELECT " & GlobalVariables.sourceStructure.char_talent1_col(0) & " FROM " &
                        GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                        GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            If talentstring.Contains(",") Then
                Dim excounter As Integer = UBound(talentstring.Split(CChar(",")))
                Dim startcounter As Integer = 0
                Do
                    Dim parts() As String = talentstring.Split(","c)
                    Dim ctalentid As String = parts(startcounter)
                    startcounter += 1
                    Dim rurrentrank As String = (TryInt(parts(startcounter)) + 1).ToString()
                    startcounter += 1
                    Dim tal As New Talent
                    tal.spell = TryInt(checkfield(ctalentid, rurrentrank))
                    tal.spec = 0
                    player.Talents.Add(tal)
                Loop Until startcounter = excounter
            End If
            Dim talentstring2 As String =
                    runSQLCommand_characters_string(
                        "SELECT " & GlobalVariables.sourceStructure.char_talent2_col(0) & " FROM " &
                        GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                        GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString & "'")
            If talentstring2.Contains(",") Then
                Dim excounter As Integer = UBound(talentstring2.Split(CChar(",")))
                Dim startcounter As Integer = 0
                Do
                    Dim parts() As String = talentstring2.Split(","c)
                    Dim ctalentid As String = parts(startcounter)
                    startcounter += 1
                    Dim rurrentrank As String = (TryInt(parts(startcounter)) + 1).ToString()
                    startcounter += 1
                    Dim tal As New Talent
                    tal.spell = TryInt(checkfield(ctalentid, rurrentrank))
                    tal.spec = 1
                    player.Talents.Add(tal)
                Loop Until startcounter = excounter
            End If
            SetCharacterSet(tarSetId, player)
        End Sub

        Private Sub LoadAtTrinity(ByVal charguid As Integer, ByVal tarSetId As Integer)
            LogAppend("Loading character talents @LoadAtTrinity", "CharacterTalentsHandler_LoadAtTrinity", False)
            Dim tempdt As DataTable =
                    ReturnDataTable(
                        "SELECT " & GlobalVariables.sourceStructure.talent_spell_col(0) & " FROM " &
                        GlobalVariables.sourceStructure.character_talent_tbl(0) & " WHERE " &
                        GlobalVariables.sourceStructure.talent_guid_col(0) & "='" & charguid.ToString & "' AND " &
                        GlobalVariables.sourceStructure.talent_spec_col(0) & "='0'")
            Dim player As Character = GetCharacterSetBySetId(tarSetId)
            If player.Talents Is Nothing Then player.Talents = New List(Of Talent)()
            Try
                Dim lastcount As Integer = tempdt.Rows.Count
                Dim count As Integer = 0
                If Not lastcount = 0 Then
                    Do
                        Dim spell As String = (tempdt.Rows(count).Item(0)).ToString
                        Dim tal As New Talent
                        tal.spell = TryInt(spell)
                        tal.spec = 0
                        player.Talents.Add(tal)
                        count += 1
                    Loop Until count = lastcount
                Else
                    LogAppend("No talents found (spec 0)!", "CharacterTalentsHandler_LoadAtTrinity", True)
                End If
            Catch ex As Exception
                LogAppend(
                    "Something went wrong while loading character talents (spec 0)! -> skipping -> Exception is: ###START###" &
                    ex.ToString() & "###END###", "CharacterTalentsHandler_LoadAtTrinity", True, True)
                Exit Sub
            End Try
            Dim tempdt2 As DataTable =
                    ReturnDataTable(
                        "SELECT " & GlobalVariables.sourceStructure.talent_spell_col(0) & " FROM " &
                        GlobalVariables.sourceStructure.character_talent_tbl(0) & " WHERE " &
                        GlobalVariables.sourceStructure.talent_guid_col(0) &
                        "='" & charguid.ToString & "' AND " & GlobalVariables.sourceStructure.talent_spec_col(0) & "='1'")
            Try
                Dim lastcount As Integer = tempdt2.Rows.Count
                Dim count As Integer = 0
                If Not lastcount = 0 Then
                    Do
                        Dim spell As String = (tempdt2.Rows(count).Item(0)).ToString
                        Dim tal As New Talent
                        tal.spell = TryInt(spell)
                        tal.spec = 1
                        player.Talents.Add(tal)
                        count += 1
                    Loop Until count = lastcount
                Else
                    LogAppend("No talents found (spec 1)!", "CharacterTalentsHandler_LoadAtTrinity", True)
                End If
            Catch ex As Exception
                LogAppend(
                    "Something went wrong while loading character talents (spec 1)! -> skipping -> Exception is: ###START###" &
                    ex.ToString() & "###END###", "CharacterTalentsHandler_LoadAtTrinity", True, True)
                Exit Sub
            End Try
            SetCharacterSet(tarSetId, player)
        End Sub

        Private Sub LoadAtMangos(ByVal charguid As Integer, ByVal tarSetId As Integer)
            _sDatatable.Clear()
            _sDatatable.Dispose()
            _sDatatable = gettable()
            LogAppend("Loading character talents @LoadAtMangos", "CharacterTalentsHandler_LoadAtMangos", False)
            Dim tempdt As DataTable =
                    ReturnDataTable(
                        "SELECT " & GlobalVariables.sourceStructure.talent_talent_col(0) & ", " &
                        GlobalVariables.sourceStructure.talent_rank_col(0) & " FROM " &
                        GlobalVariables.sourceStructure.character_talent_tbl(0) &
                        " WHERE " & GlobalVariables.sourceStructure.talent_spec_col(0) & "='" & charguid.ToString() &
                        "' AND " & GlobalVariables.sourceStructure.talent_spec_col(0) & "='0'")
            Dim player As Character = GetCharacterSetBySetId(tarSetId)
            If player.Talents Is Nothing Then player.Talents = New List(Of Talent)()
            Try
                Dim lastcount As Integer = tempdt.Rows.Count
                Dim count As Integer = 0
                If Not lastcount = 0 Then
                    Do
                        Dim idtalent As String = (tempdt.Rows(count).Item(0)).ToString
                        Dim currentrank As String = (tempdt.Rows(count).Item(1)).ToString
                        Dim tal As New Talent
                        tal.spell = TryInt(checkfield(idtalent, currentrank))
                        tal.spec = 0
                        player.Talents.Add(tal)
                        count += 1
                    Loop Until count = lastcount
                Else
                    LogAppend("No talents found (spec 0)!", "CharacterTalentsHandler_LoadAtMangos", True)
                End If
            Catch ex As Exception
                LogAppend(
                    "Something went wrong while loading character talents (spec 0)! -> skipping -> Exception is: ###START###" &
                    ex.ToString() & "###END###", "CharacterTalentsHandler_LoadAtMangos", True, True)
                Exit Sub
            End Try
            Dim tempdt2 As DataTable =
                    ReturnDataTable(
                        "SELECT " & GlobalVariables.sourceStructure.talent_talent_col(0) & ", " &
                        GlobalVariables.sourceStructure.talent_rank_col(0) & " FROM " &
                        GlobalVariables.sourceStructure.character_talent_tbl(0) &
                        " WHERE " & GlobalVariables.sourceStructure.talent_guid_col(0) & "='" & charguid.ToString() &
                        "' AND " & GlobalVariables.sourceStructure.talent_spec_col(0) & "='1'")
            Try
                Dim lastcount As Integer = tempdt2.Rows.Count
                Dim count As Integer = 0
                If Not lastcount = 0 Then
                    Do
                        Dim idtalent As String = (tempdt2.Rows(count).Item(0)).ToString
                        Dim currentrank As String = (tempdt2.Rows(count).Item(1)).ToString
                        Dim tal As New Talent
                        tal.spell = TryInt(checkfield(idtalent, currentrank))
                        tal.spec = 1
                        player.Talents.Add(tal)
                        count += 1
                    Loop Until count = lastcount
                Else
                    LogAppend("No talents found (spec 1)!", "CharacterTalentsHandler_LoadAtMangos", True)
                End If
            Catch ex As Exception
                LogAppend(
                    "Something went wrong while loading character talents (spec 1)! -> skipping -> Exception is: ###START###" &
                    ex.ToString() & "###END###", "CharacterTalentsHandler_LoadAtMangos", True, True)
                Exit Sub
            End Try
            SetCharacterSet(tarSetId, player)
        End Sub

        Private Function GetTable() As DataTable
            Try
                Dim dt As New DataTable()
                Dim stext As String = libnc.My.Resources.talent
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
                        '  Dim dr As DataRow = dt.NewRow() // never used??
                        dt.Rows.Add(strArray)
                    End If
                Next i
                Return dt
            Catch ex As Exception
                LogAppend("Failed to load new talent datatable! -> Exception is: ###START###" & ex.ToString() & "###END###",
                          "CharacterTalentsHandler_gettable", False, True)
                Return New DataTable
            End Try
        End Function

        Private Function Checkfield(ByVal lId As String, ByVal rank As String) As String
            LogAppend("Loading SpellId of Talent " & lID & " with rank " & rank, "CharacterTalentsHandler_checkfield", False)
            Dim byRnk As String = executex("TalentId", lID, TryInt(rank))
            If rank = "0" Then
                LogAppend("Talent Rank is 0 -> Returning " & lID & "clear", "CharacterTalentsHandler_checkfield", False)
                Return lID & "clear"
            ElseIf Not byRnk = "-" Then
                LogAppend("SpellID is " & byRnk, "CharacterTalentsHandler_checkfield", False)
                Return byRnk
            Else
                LogAppend("SpellID " & lID & " not found! -> Returning 0", "CharacterTalentsHandler_checkfield", False, True)
                Return "0"
            End If
        End Function

        Private Function Executex(ByVal field As String, ByVal sId As String, ByVal rank As Integer) As String
            Try
                Dim foundRows() As DataRow
                foundRows = _sDatatable.Select(field & " = '" & sID & "'")
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
                LogAppend(
                    "Something went wrong while checking talent dt for TalentId " & sID & " & rank " & rank &
                    " -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterTalentsHandler_executex", False,
                    True)
                Return "-"
            End Try
        End Function
    End Class
End Namespace