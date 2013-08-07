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
'*      /Filename:      TalentCreation
'*      /Description:   Includes functions for setting up the known talents of a specific
'*                      character
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Imports Namcore_Studio.EventLogging
Imports Namcore_Studio.CommandHandler
Imports Namcore_Studio.GlobalVariables
Imports Namcore_Studio.Basics
Imports Namcore_Studio.Conversions
Public Class TalentCreation
    Private SDatatable As New DataTable
    Private TalentRank As String
    Private TalentRank2 As String
    Private TalentId As String

    Public Sub SetCharacterTalents(ByVal setId As Integer, Optional charguid As Integer = 0)
        If charguid = 0 Then charguid = characterGUID
        LogAppend("Setting Talents for character: " & charguid.ToString() & " // setId is : " & setId.ToString(), "TalentCreation_SetCharacterTalents", True)
        Select Case targetCore
            Case "arcemu"
                createAtArcemu(charguid, setId)
            Case "trinity"
                createAtTrinity(charguid, setId)
            Case "trinitytbc"

            Case "mangos"
                createAtMangos(charguid, setId)
            Case Else

        End Select
    End Sub
    Private Function LoadTalentTable() As DataTable
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
                    Dim dr As DataRow = dt.NewRow()
                    dt.Rows.Add(strArray)
                End If
            Next i
            Return dt
        Catch ex As Exception
            LogAppend("Failed to load new talent datatable! -> Exception is: ###START###" & ex.ToString() & "###END###", "TalentCreation_LoadTalentTable", False, True)
            Return New DataTable
        End Try
    End Function
    Private Function checkfield(ByVal lID As String) As String
        If Not executex("Rang1", lID) = "-" Then
            TalentRank = "0"
            TalentRank2 = "0"
            Return (executex("Rang1", lID))
        ElseIf Not executex("Rang2", lID) = "-" Then
            TalentRank = "1"
            TalentRank2 = "1"
            Return (executex("Rang2", lID))
        ElseIf Not executex("Rang3", lID) = "-" Then
            TalentRank = "2"
            TalentRank2 = "2"
            Return (executex("Rang3", lID))
        ElseIf Not executex("Rang4", lID) = "-" Then
            TalentRank = "3"
            TalentRank2 = "3"
            Return (executex("Rang4", lID))
        ElseIf Not executex("Rang5", lID) = "-" Then
            TalentRank = "4"
            TalentRank2 = "4"
            Return (executex("Rang5", lID))
        Else
            TalentRank = "0"
            TalentRank2 = "0"
            Return "0"
        End If
    End Function

    Private Function executex(ByVal field As String, ByVal sID As String) As String
        Try
            Dim foundRows() As DataRow
            foundRows = SDatatable.Select(field & " = '" & sID & "'")
            If foundRows.Length = 0 Then
                Return "-"
            Else
                Dim i As Integer
                Dim tmpreturn As String = "-"
                For i = 0 To foundRows.GetUpperBound(0)
                    tmpreturn = (foundRows(i)(0)).ToString
                Next i
                Return tmpreturn
            End If
        Catch ex As Exception
            LogAppend("Talent " & sID & " not found! -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterTalentsHandler_executex", False, True)
            Return "-"
        End Try
    End Function
    Private Sub createAtArcemu(ByVal characterguid As Integer, ByVal targetSetId As Integer)
        LogAppend("Creating at arcemu", "TalentCreation_createAtArcemu", False)
        TalentRank = Nothing
        TalentRank2 = Nothing
        SDatatable.Clear()
        SDatatable.Dispose()
        SDatatable = LoadTalentTable()
        Dim talentlist As String = Nothing
        Dim talentlist2 As String = Nothing
        Dim finaltalentstring As String = Nothing
        Dim finaltalentstring2 As String = Nothing
        Dim player As Character = GetCharacterSetBySetId(targetSetId)
        For Each tal As Talent In player.Talents
            Dim spellid As String = tal.spell.ToString
            If spellid.Contains("clear") Then
                TalentId = spellid.Replace("clear", "")
                Dim spec As String = tal.spec.ToString
                If spec = "0" Then
                    finaltalentstring = finaltalentstring & TalentId & ",0,"
                Else
                    finaltalentstring2 = finaltalentstring2 & TalentId & ",0,"
                End If
            Else
                TalentId = checkfield(spellid)
                Dim spec As String = tal.spec.ToString
                If spec = "0" Then
                    If talentlist.Contains(TalentId) Then
                        If talentlist.Contains(TalentId & "rank5") Then

                        ElseIf talentlist.Contains(TalentId & "rank4") Then
                            If CInt(Val(TalentRank)) <= 4 Then
                            Else
                                finaltalentstring = finaltalentstring.Replace(TalentId & ",0", (CInt(Val(TalentRank))).ToString)
                                finaltalentstring = finaltalentstring.Replace(TalentId & ",1", (CInt(Val(TalentRank))).ToString)
                                finaltalentstring = finaltalentstring.Replace(TalentId & ",2", (CInt(Val(TalentRank))).ToString)
                                finaltalentstring = finaltalentstring.Replace(TalentId & ",3", (CInt(Val(TalentRank))).ToString)
                                finaltalentstring = finaltalentstring.Replace(TalentId & ",4", (CInt(Val(TalentRank))).ToString)
                                talentlist = talentlist & " " & TalentId & "rank" & TalentRank
                            End If
                        ElseIf talentlist.Contains(TalentId & "rank3") Then
                            If CInt(Val(TalentRank)) <= 3 Then
                            Else
                                finaltalentstring = finaltalentstring.Replace(TalentId & ",0", (CInt(Val(TalentRank))).ToString)
                                finaltalentstring = finaltalentstring.Replace(TalentId & ",1", (CInt(Val(TalentRank))).ToString)
                                finaltalentstring = finaltalentstring.Replace(TalentId & ",2", (CInt(Val(TalentRank))).ToString)
                                finaltalentstring = finaltalentstring.Replace(TalentId & ",3", (CInt(Val(TalentRank))).ToString)
                                talentlist = talentlist & " " & TalentId & "rank" & TalentRank
                            End If
                        ElseIf talentlist.Contains(TalentId & "TalentRank2") Then
                            If CInt(Val(TalentRank)) <= 2 Then
                            Else
                                finaltalentstring = finaltalentstring.Replace(TalentId & ",0", (CInt(Val(TalentRank))).ToString)
                                finaltalentstring = finaltalentstring.Replace(TalentId & ",1", (CInt(Val(TalentRank))).ToString)
                                finaltalentstring = finaltalentstring.Replace(TalentId & ",2", (CInt(Val(TalentRank)) - 1).ToString)
                                talentlist = talentlist & " " & TalentId & "rank" & TalentRank
                            End If
                        ElseIf talentlist.Contains(TalentId & "rank1") Then
                            If CInt(Val(TalentRank)) <= 1 Then
                            Else
                                finaltalentstring = finaltalentstring.Replace(TalentId & ",0", (CInt(Val(TalentRank))).ToString)
                                finaltalentstring = finaltalentstring.Replace(TalentId & ",1", (CInt(Val(TalentRank))).ToString)
                                talentlist = talentlist & " " & TalentId & "rank" & TalentRank
                            End If
                        Else : End If
                    Else
                        finaltalentstring = finaltalentstring & TalentId & "," & (CInt(Val(TalentRank))).ToString & ","
                        talentlist = talentlist & " " & TalentId & "rank" & TalentRank
                    End If
                Else
                    If talentlist2.Contains(TalentId) Then
                        If talentlist2.Contains(TalentId & "rank5") Then

                        ElseIf talentlist2.Contains(TalentId & "rank4") Then
                            If CInt(Val(TalentRank2)) <= 4 Then
                            Else
                                finaltalentstring2 = finaltalentstring2.Replace(TalentId & ",0", (CInt(Val(TalentRank2))).ToString)
                                finaltalentstring2 = finaltalentstring2.Replace(TalentId & ",1", (CInt(Val(TalentRank2))).ToString)
                                finaltalentstring2 = finaltalentstring2.Replace(TalentId & ",2", (CInt(Val(TalentRank2))).ToString)
                                finaltalentstring2 = finaltalentstring2.Replace(TalentId & ",3", (CInt(Val(TalentRank2))).ToString)
                                finaltalentstring2 = finaltalentstring2.Replace(TalentId & ",4", (CInt(Val(TalentRank2))).ToString)
                                talentlist2 = talentlist2 & " " & TalentId & "rank" & TalentRank2
                            End If
                        ElseIf talentlist2.Contains(TalentId & "rank3") Then
                            If CInt(Val(TalentRank2)) <= 3 Then
                            Else
                                finaltalentstring2 = finaltalentstring2.Replace(TalentId & ",0", (CInt(Val(TalentRank2))).ToString)
                                finaltalentstring2 = finaltalentstring2.Replace(TalentId & ",1", (CInt(Val(TalentRank2))).ToString)
                                finaltalentstring2 = finaltalentstring2.Replace(TalentId & ",2", (CInt(Val(TalentRank2))).ToString)
                                finaltalentstring2 = finaltalentstring2.Replace(TalentId & ",3", (CInt(Val(TalentRank2))).ToString)
                                talentlist2 = talentlist2 & " " & TalentId & "rank" & TalentRank2
                            End If
                        ElseIf talentlist2.Contains(TalentId & "TalentRank22") Then
                            If CInt(Val(TalentRank2)) <= 2 Then
                            Else
                                finaltalentstring2 = finaltalentstring2.Replace(TalentId & ",0", (CInt(Val(TalentRank2))).ToString)
                                finaltalentstring2 = finaltalentstring2.Replace(TalentId & ",1", (CInt(Val(TalentRank2))).ToString)
                                finaltalentstring2 = finaltalentstring2.Replace(TalentId & ",2", (CInt(Val(TalentRank2)) - 1).ToString)
                                talentlist2 = talentlist2 & " " & TalentId & "rank" & TalentRank2
                            End If
                        ElseIf talentlist2.Contains(TalentId & "rank1") Then
                            If CInt(Val(TalentRank2)) <= 1 Then
                            Else
                                finaltalentstring2 = finaltalentstring2.Replace(TalentId & ",0", (CInt(Val(TalentRank2))).ToString)
                                finaltalentstring2 = finaltalentstring2.Replace(TalentId & ",1", (CInt(Val(TalentRank2))).ToString)
                                talentlist2 = talentlist2 & " " & TalentId & "rank" & TalentRank2
                            End If
                        Else : End If
                    Else
                        finaltalentstring2 = finaltalentstring2 & TalentId & "," & (CInt(Val(TalentRank2))).ToString & ","
                        talentlist2 = talentlist2 & " " & TalentId & "rank" & TalentRank2
                    End If
                End If
            End If
            runSQLCommand_characters_string("UPDATE " & targetStructure.character_tbl(0) & " SET " & targetStructure.char_talent1_col(0) & "='" & finaltalentstring & "' WHERE " & targetStructure.char_guid_col(0) &
                                            "='" & characterguid.ToString() & "'", True)
            runSQLCommand_characters_string("UPDATE " & targetStructure.character_tbl(0) & " SET " & targetStructure.char_talent2_col(0) & "='" & finaltalentstring2 & "' WHERE " & targetStructure.char_guid_col(0) &
                                            "='" & characterguid.ToString() & "'", True)
        Next
    End Sub
    Private Sub createAtTrinity(ByVal characterguid As Integer, ByVal targetSetId As Integer)
        LogAppend("Creating at Trinity", "TalentCreation_createAtTrinity", False)
        Dim player As Character = GetCharacterSetBySetId(targetSetId)
        For Each tal As Talent In player.Talents
            runSQLCommand_characters_string("INSERT INTO " & targetStructure.character_talent_tbl(0) & " ( " & targetStructure.talent_guid_col(0) & ", " & targetStructure.talent_spell_col(0) & ", " &
                                            targetStructure.talent_spec_col(0) & " ) VALUES ( '" & characterguid.ToString() & "', '" & tal.spell.ToString & "', '" &
                                            tal.spec.ToString & "')", True)
        Next
    End Sub
    Private Sub createAtMangos(ByVal characterguid As Integer, ByVal targetSetId As Integer)
        LogAppend("Creating at Mangos", "TalentCreation_createAtMangos", False)
        TalentRank = Nothing
        TalentRank2 = Nothing
        SDatatable.Clear()
        SDatatable.Dispose()
        SDatatable = LoadTalentTable()
        Dim talentlist As String = Nothing
        Dim talentlist2 As String = Nothing
        Dim finaltalentstring As String = Nothing
        Dim finaltalentstring2 As String = Nothing
        Dim player As Character = GetCharacterSetBySetId(targetSetId)
        For Each tal As Talent In player.Talents
            TalentId = tal.spell.ToString
            Dim spec As String = tal.spec.ToString
            If spec = "0" Then
                If talentlist.Contains(TalentId) Then
                    If talentlist.Contains(TalentId & "rank5") Then
                    ElseIf talentlist.Contains(TalentId & "rank4") Then
                        If CInt(Val(TalentRank)) <= 4 Then
                        Else
                            runSQLCommand_characters_string("UPDATE " & targetStructure.character_talent_tbl(0) & " SET " & targetStructure.talent_rank_col(0) & "='" & TalentRank & "' WHERE " &
                                                            targetStructure.talent_guid_col(0) & "='" & characterguid.ToString & "' AND " & targetStructure.talent_talent_col(0) & "='" & TalentId & "' AND " &
                                                            targetStructure.talent_spec_col(0) & "='0'", True)
                            talentlist = talentlist & " " & TalentId & "rank" & TalentRank
                        End If
                    ElseIf talentlist.Contains(TalentId & "rank3") Then
                        If CInt(Val(TalentRank)) <= 3 Then
                        Else
                            runSQLCommand_characters_string("UPDATE " & targetStructure.character_talent_tbl(0) & " SET " & targetStructure.talent_rank_col(0) & "='" & TalentRank & "' WHERE " &
                                                            targetStructure.talent_guid_col(0) & "='" & characterguid.ToString & "' AND " & targetStructure.talent_talent_col(0) & "='" & TalentId & "' AND " &
                                                            targetStructure.talent_spec_col(0) & "='0'", True)
                            talentlist = talentlist & " " & TalentId & "rank" & TalentRank
                        End If
                    ElseIf talentlist.Contains(TalentId & "rank2") Then
                        If CInt(Val(TalentRank)) <= 2 Then
                        Else
                            runSQLCommand_characters_string("UPDATE " & targetStructure.character_talent_tbl(0) & " SET " & targetStructure.talent_rank_col(0) & "='" & TalentRank & "' WHERE " &
                                                            targetStructure.talent_guid_col(0) & "='" & characterguid.ToString & "' AND " & targetStructure.talent_talent_col(0) & "='" & TalentId & "' AND " &
                                                            targetStructure.talent_spec_col(0) & "='0'", True)
                            talentlist = talentlist & " " & TalentId & "rank" & TalentRank
                        End If
                    ElseIf talentlist.Contains(TalentId & "rank1") Then
                        If CInt(Val(TalentRank)) <= 1 Then
                        Else
                            runSQLCommand_characters_string("UPDATE " & targetStructure.character_talent_tbl(0) & " SET " & targetStructure.talent_rank_col(0) & "='" & TalentRank & "' WHERE " &
                                                            targetStructure.talent_guid_col(0) & "='" & characterguid.ToString & "' AND " & targetStructure.talent_talent_col(0) & "='" & TalentId & "' AND " &
                                                            targetStructure.talent_spec_col(0) & "='0'", True)
                            talentlist = talentlist & " " & TalentId & "rank" & TalentRank
                        End If
                    Else : End If
                Else
                    runSQLCommand_characters_string("INSERT INTO " & targetStructure.character_talent_tbl(0) & " ( " & targetStructure.talent_guid_col(0) & ", " & targetStructure.talent_talent_col(0) & ", " &
                                                    targetStructure.talent_rank_col(0) & ", " & targetStructure.talent_spec_col(0) & " ) VALUES ( '" & characterguid.ToString & "', '" & TalentId & "', '" &
                                                    TalentRank & "', '0' )", True)
                    talentlist = talentlist & " " & TalentId & "rank" & TalentRank
                End If
            Else
                If talentlist2.Contains(TalentId) Then
                    If talentlist2.Contains(TalentId & "rank5") Then
                    ElseIf talentlist2.Contains(TalentId & "rank4") Then
                        If CInt(Val(TalentRank2)) <= 4 Then
                        Else
                            runSQLCommand_characters_string("UPDATE " & targetStructure.character_talent_tbl(0) & " SET " & targetStructure.talent_rank_col(0) & "='" & TalentRank2 & "' WHERE " &
                                                            targetStructure.talent_guid_col(0) & "='" & characterguid.ToString & "' AND " & targetStructure.talent_talent_col(0) & "='" & TalentId & "' AND " &
                                                            targetStructure.talent_spec_col(0) & "='1'", True)
                            talentlist2 = talentlist2 & " " & TalentId & "rank" & TalentRank2
                        End If
                    ElseIf talentlist2.Contains(TalentId & "rank3") Then
                        If CInt(Val(TalentRank2)) <= 3 Then
                        Else
                            runSQLCommand_characters_string("UPDATE " & targetStructure.character_talent_tbl(0) & " SET " & targetStructure.talent_rank_col(0) & "='" & TalentRank2 & "' WHERE " &
                                                            targetStructure.talent_guid_col(0) & "='" & characterguid.ToString & "' AND " & targetStructure.talent_talent_col(0) & "='" & TalentId & "' AND " &
                                                            targetStructure.talent_spec_col(0) & "='1'", True)
                            talentlist2 = talentlist2 & " " & TalentId & "rank" & TalentRank2
                        End If
                    ElseIf talentlist2.Contains(TalentId & "rank2") Then
                        If CInt(Val(TalentRank2)) <= 2 Then
                        Else
                            runSQLCommand_characters_string("UPDATE " & targetStructure.character_talent_tbl(0) & " SET " & targetStructure.talent_rank_col(0) & "='" & TalentRank2 & "' WHERE " &
                                                            targetStructure.talent_guid_col(0) & "='" & characterguid.ToString & "' AND " & targetStructure.talent_talent_col(0) & "='" & TalentId & "' AND " &
                                                            targetStructure.talent_spec_col(0) & "='1'", True)
                            talentlist2 = talentlist2 & " " & TalentId & "rank" & TalentRank2
                        End If
                    ElseIf talentlist2.Contains(TalentId & "rank1") Then
                        If CInt(Val(TalentRank2)) <= 1 Then
                        Else
                            runSQLCommand_characters_string("UPDATE " & targetStructure.character_talent_tbl(0) & " SET " & targetStructure.talent_rank_col(0) & "='" & TalentRank2 & "' WHERE " &
                                                            targetStructure.talent_guid_col(0) & "='" & characterguid.ToString & "' AND " & targetStructure.talent_talent_col(0) & "='" & TalentId & "' AND " &
                                                            targetStructure.talent_spec_col(0) & "='1'", True)
                            talentlist2 = talentlist2 & " " & TalentId & "rank" & TalentRank2
                        End If
                    Else : End If
                Else
                    runSQLCommand_characters_string("INSERT INTO " & targetStructure.character_talent_tbl(0) & " ( " & targetStructure.talent_guid_col(0) & ", " & targetStructure.talent_talent_col(0) & ", " &
                                                    targetStructure.talent_rank_col(0) & ", " & targetStructure.talent_spec_col(0) & " ) VALUES ( '" & characterguid.ToString & "', '" & TalentId & "', '" & TalentRank2 & "', '1' )", True)
                    talentlist2 = talentlist2 & " " & TalentId & "rank" & TalentRank2
                End If
            End If
        Next
    End Sub
End Class
