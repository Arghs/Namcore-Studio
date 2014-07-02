'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013-2014 NamCore Studio <https://github.com/megasus/Namcore-Studio>
'*
'* This program is free software; you can redistribute it and/or modify it
'* under the terms of the GNU General Public License as published by the
'* Free Software Foundation; either version 3 of the License, or (at your
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
Imports NCFramework.Framework.Database
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Modules

Namespace Framework.Transmission
    Public Class TalentCreation
        '// Declaration
        Private _sDatatable As New DataTable
        Private _talentRank As String
        Private _talentRank2 As String
        Private _talentId As String
        '// Declaration

        Public Sub SetCharacterTalents(ByVal player As Character, Optional charguid As Integer = 0)
            If charguid = 0 Then charguid = player.Guid
            LogAppend("Setting Talents for character: " & charguid.ToString(),
                      "TalentCreation_SetCharacterTalents", True)
            Try
                Select Case GlobalVariables.targetCore
                    Case Modules.Core.ARCEMU
                        CreateAtArcemu(charguid, player)
                    Case Modules.Core.TRINITY
                        CreateAtTrinity(charguid, player)
                    Case Modules.Core.MANGOS
                        CreateAtMangos(charguid, player)
                End Select
            Catch ex As Exception
                LogAppend("Exception occured: " & ex.ToString(),
               "TalentCreation_SetCharacterTalents", False, True)
            End Try
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
                        dt.Rows.Add(strArray)
                    End If
                Next i
                Return dt
            Catch ex As Exception
                LogAppend(
                    "Failed to load new talent datatable! -> Exception is: ###START###" & ex.ToString() & "###END###",
                    "TalentCreation_LoadTalentTable", False, True)
                Return New DataTable
            End Try
        End Function

        Private Function Checkfield(ByVal lId As String) As String
            If Not executex("Rang1", lID) = "-" Then
                _talentRank = "0"
                _talentRank2 = "0"
                Return (executex("Rang1", lID))
            ElseIf Not executex("Rang2", lID) = "-" Then
                _talentRank = "1"
                _talentRank2 = "1"
                Return (executex("Rang2", lID))
            ElseIf Not executex("Rang3", lID) = "-" Then
                _talentRank = "2"
                _talentRank2 = "2"
                Return (executex("Rang3", lID))
            ElseIf Not executex("Rang4", lID) = "-" Then
                _talentRank = "3"
                _talentRank2 = "3"
                Return (executex("Rang4", lID))
            ElseIf Not executex("Rang5", lID) = "-" Then
                _talentRank = "4"
                _talentRank2 = "4"
                Return (executex("Rang5", lID))
            Else
                _talentRank = "0"
                _talentRank2 = "0"
                Return "0"
            End If
        End Function

        Private Function Executex(ByVal field As String, ByVal sId As String) As String
            Try
                Dim foundRows() As DataRow
                foundRows = _sDatatable.Select(field & " = '" & sID & "'")
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
                LogAppend("Talent " & sID & " not found! -> Exception is: ###START###" & ex.ToString() & "###END###",
                          "CharacterTalentsHandler_executex", False, True)
                Return "-"
            End Try
        End Function

        Private Sub CreateAtArcemu(ByVal characterguid As Integer, ByVal player As Character)
            LogAppend("Creating at arcemu", "TalentCreation_createAtArcemu", False)
            _talentRank = Nothing
            _talentRank2 = Nothing
            _sDatatable.Clear()
            _sDatatable.Dispose()
            _sDatatable = LoadTalentTable()
            Dim talentlist As String = Nothing
            Dim talentlist2 As String = Nothing
            Dim finaltalentstring As String = Nothing
            Dim finaltalentstring2 As String = Nothing
            For Each tal As Talent In player.Talents
                Dim spellid As String = tal.Spell.ToString
                If spellid.Contains("clear") Then
                    _talentId = spellid.Replace("clear", "")
                    Dim spec As String = tal.Spec.ToString
                    If spec = "0" Then
                        finaltalentstring = finaltalentstring & _talentId & ",0,"
                    Else
                        finaltalentstring2 = finaltalentstring2 & _talentId & ",0,"
                    End If
                Else
                    _talentId = Checkfield(spellid)
                    Dim spec As String = tal.Spec.ToString
                    If spec = "0" Then
                        If talentlist.Contains(_talentId) Then
                            If talentlist.Contains(_talentId & "rank5") Then

                            ElseIf talentlist.Contains(_talentId & "rank4") Then
                                If CInt(Val(_talentRank)) <= 4 Then
                                Else
                                    finaltalentstring = finaltalentstring.Replace(_talentId & ",0",
                                                                                  (CInt(Val(_talentRank))).ToString)
                                    finaltalentstring = finaltalentstring.Replace(_talentId & ",1",
                                                                                  (CInt(Val(_talentRank))).ToString)
                                    finaltalentstring = finaltalentstring.Replace(_talentId & ",2",
                                                                                  (CInt(Val(_talentRank))).ToString)
                                    finaltalentstring = finaltalentstring.Replace(_talentId & ",3",
                                                                                  (CInt(Val(_talentRank))).ToString)
                                    finaltalentstring = finaltalentstring.Replace(_talentId & ",4",
                                                                                  (CInt(Val(_talentRank))).ToString)
                                    talentlist = talentlist & " " & _talentId & "rank" & _talentRank
                                End If
                            ElseIf talentlist.Contains(_talentId & "rank3") Then
                                If CInt(Val(_talentRank)) <= 3 Then
                                Else
                                    finaltalentstring = finaltalentstring.Replace(_talentId & ",0",
                                                                                  (CInt(Val(_talentRank))).ToString)
                                    finaltalentstring = finaltalentstring.Replace(_talentId & ",1",
                                                                                  (CInt(Val(_talentRank))).ToString)
                                    finaltalentstring = finaltalentstring.Replace(_talentId & ",2",
                                                                                  (CInt(Val(_talentRank))).ToString)
                                    finaltalentstring = finaltalentstring.Replace(_talentId & ",3",
                                                                                  (CInt(Val(_talentRank))).ToString)
                                    talentlist = talentlist & " " & _talentId & "rank" & _talentRank
                                End If
                            ElseIf talentlist.Contains(_talentId & "TalentRank2") Then
                                If CInt(Val(_talentRank)) <= 2 Then
                                Else
                                    finaltalentstring = finaltalentstring.Replace(_talentId & ",0",
                                                                                  (CInt(Val(_talentRank))).ToString)
                                    finaltalentstring = finaltalentstring.Replace(_talentId & ",1",
                                                                                  (CInt(Val(_talentRank))).ToString)
                                    finaltalentstring = finaltalentstring.Replace(_talentId & ",2",
                                                                                  (CInt(Val(_talentRank)) - 1).ToString)
                                    talentlist = talentlist & " " & _talentId & "rank" & _talentRank
                                End If
                            ElseIf talentlist.Contains(_talentId & "rank1") Then
                                If CInt(Val(_talentRank)) <= 1 Then
                                Else
                                    finaltalentstring = finaltalentstring.Replace(_talentId & ",0",
                                                                                  (CInt(Val(_talentRank))).ToString)
                                    finaltalentstring = finaltalentstring.Replace(_talentId & ",1",
                                                                                  (CInt(Val(_talentRank))).ToString)
                                    talentlist = talentlist & " " & _talentId & "rank" & _talentRank
                                End If
                            Else
                            End If
                        Else
                            finaltalentstring = finaltalentstring & _talentId & "," & (CInt(Val(_talentRank))).ToString &
                                                ","
                            talentlist = talentlist & " " & _talentId & "rank" & _talentRank
                        End If
                    Else
                        If talentlist2.Contains(_talentId) Then
                            If talentlist2.Contains(_talentId & "rank5") Then

                            ElseIf talentlist2.Contains(_talentId & "rank4") Then
                                If CInt(Val(_talentRank2)) <= 4 Then
                                Else
                                    finaltalentstring2 = finaltalentstring2.Replace(_talentId & ",0",
                                                                                    (CInt(Val(_talentRank2))).ToString)
                                    finaltalentstring2 = finaltalentstring2.Replace(_talentId & ",1",
                                                                                    (CInt(Val(_talentRank2))).ToString)
                                    finaltalentstring2 = finaltalentstring2.Replace(_talentId & ",2",
                                                                                    (CInt(Val(_talentRank2))).ToString)
                                    finaltalentstring2 = finaltalentstring2.Replace(_talentId & ",3",
                                                                                    (CInt(Val(_talentRank2))).ToString)
                                    finaltalentstring2 = finaltalentstring2.Replace(_talentId & ",4",
                                                                                    (CInt(Val(_talentRank2))).ToString)
                                    talentlist2 = talentlist2 & " " & _talentId & "rank" & _talentRank2
                                End If
                            ElseIf talentlist2.Contains(_talentId & "rank3") Then
                                If CInt(Val(_talentRank2)) <= 3 Then
                                Else
                                    finaltalentstring2 = finaltalentstring2.Replace(_talentId & ",0",
                                                                                    (CInt(Val(_talentRank2))).ToString)
                                    finaltalentstring2 = finaltalentstring2.Replace(_talentId & ",1",
                                                                                    (CInt(Val(_talentRank2))).ToString)
                                    finaltalentstring2 = finaltalentstring2.Replace(_talentId & ",2",
                                                                                    (CInt(Val(_talentRank2))).ToString)
                                    finaltalentstring2 = finaltalentstring2.Replace(_talentId & ",3",
                                                                                    (CInt(Val(_talentRank2))).ToString)
                                    talentlist2 = talentlist2 & " " & _talentId & "rank" & _talentRank2
                                End If
                            ElseIf talentlist2.Contains(_talentId & "TalentRank22") Then
                                If CInt(Val(_talentRank2)) <= 2 Then
                                Else
                                    finaltalentstring2 = finaltalentstring2.Replace(_talentId & ",0",
                                                                                    (CInt(Val(_talentRank2))).ToString)
                                    finaltalentstring2 = finaltalentstring2.Replace(_talentId & ",1",
                                                                                    (CInt(Val(_talentRank2))).ToString)
                                    finaltalentstring2 = finaltalentstring2.Replace(_talentId & ",2",
                                                                                    (CInt(Val(_talentRank2)) - 1).
                                                                                       ToString)
                                    talentlist2 = talentlist2 & " " & _talentId & "rank" & _talentRank2
                                End If
                            ElseIf talentlist2.Contains(_talentId & "rank1") Then
                                If CInt(Val(_talentRank2)) <= 1 Then
                                Else
                                    finaltalentstring2 = finaltalentstring2.Replace(_talentId & ",0",
                                                                                    (CInt(Val(_talentRank2))).ToString)
                                    finaltalentstring2 = finaltalentstring2.Replace(_talentId & ",1",
                                                                                    (CInt(Val(_talentRank2))).ToString)
                                    talentlist2 = talentlist2 & " " & _talentId & "rank" & _talentRank2
                                End If
                            Else
                            End If
                        Else
                            finaltalentstring2 = finaltalentstring2 & _talentId & "," &
                                                 (CInt(Val(_talentRank2))).ToString &
                                                 ","
                            talentlist2 = talentlist2 & " " & _talentId & "rank" & _talentRank2
                        End If
                    End If
                End If
                runSQLCommand_characters_string(
                    "UPDATE " & GlobalVariables.targetStructure.character_tbl(0) & " SET " &
                    GlobalVariables.targetStructure.char_talent1_col(0) & "='" & finaltalentstring & "' WHERE " &
                    GlobalVariables.targetStructure.char_guid_col(0) &
                    "='" & characterguid.ToString() & "'", True)
                runSQLCommand_characters_string(
                    "UPDATE " & GlobalVariables.targetStructure.character_tbl(0) & " SET " &
                    GlobalVariables.targetStructure.char_talent2_col(0) & "='" & finaltalentstring2 & "' WHERE " &
                    GlobalVariables.targetStructure.char_guid_col(0) &
                    "='" & characterguid.ToString() & "'", True)
            Next
        End Sub

        Private Sub CreateAtTrinity(ByVal characterguid As Integer, ByVal player As Character)
            LogAppend("Creating at Trinity", "TalentCreation_createAtTrinity", False)
            For Each tal As Talent In player.Talents
                runSQLCommand_characters_string(
                    "INSERT INTO " & GlobalVariables.targetStructure.character_talent_tbl(0) & " ( " &
                    GlobalVariables.targetStructure.talent_guid_col(0) & ", " &
                    GlobalVariables.targetStructure.talent_spell_col(0) & ", " &
                    GlobalVariables.targetStructure.talent_spec_col(0) & " ) VALUES ( '" & characterguid.ToString() &
                    "', '" &
                    tal.Spell.ToString & "', '" &
                    tal.Spec.ToString & "')", True)
            Next
        End Sub

        Private Sub CreateAtMangos(ByVal characterguid As Integer, ByVal player As Character)
            LogAppend("Creating at Mangos", "TalentCreation_createAtMangos", False)
            _talentRank = Nothing
            _talentRank2 = Nothing
            _sDatatable.Clear()
            _sDatatable.Dispose()
            _sDatatable = LoadTalentTable()
            Dim talentlist As String = Nothing
            Dim talentlist2 As String = Nothing
            For Each tal As Talent In player.Talents
                _talentId = tal.Spell.ToString
                Dim spec As String = tal.Spec.ToString
                If spec = "0" Then
                    If talentlist.Contains(_talentId) Then
                        If talentlist.Contains(_talentId & "rank5") Then
                        ElseIf talentlist.Contains(_talentId & "rank4") Then
                            If CInt(Val(_talentRank)) <= 4 Then
                            Else
                                runSQLCommand_characters_string(
                                    "UPDATE " & GlobalVariables.targetStructure.character_talent_tbl(0) & " SET " &
                                    GlobalVariables.targetStructure.talent_rank_col(0) & "='" & _talentRank & "' WHERE " &
                                    GlobalVariables.targetStructure.talent_guid_col(0) & "='" & characterguid.ToString &
                                    "' AND " & GlobalVariables.targetStructure.talent_talent_col(0) & "='" & _talentId &
                                    "' AND " &
                                    GlobalVariables.targetStructure.talent_spec_col(0) & "='0'", True)
                                talentlist = talentlist & " " & _talentId & "rank" & _talentRank
                            End If
                        ElseIf talentlist.Contains(_talentId & "rank3") Then
                            If CInt(Val(_talentRank)) <= 3 Then
                            Else
                                runSQLCommand_characters_string(
                                    "UPDATE " & GlobalVariables.targetStructure.character_talent_tbl(0) & " SET " &
                                    GlobalVariables.targetStructure.talent_rank_col(0) & "='" & _talentRank & "' WHERE " &
                                    GlobalVariables.targetStructure.talent_guid_col(0) & "='" & characterguid.ToString &
                                    "' AND " & GlobalVariables.targetStructure.talent_talent_col(0) & "='" & _talentId &
                                    "' AND " &
                                    GlobalVariables.targetStructure.talent_spec_col(0) & "='0'", True)
                                talentlist = talentlist & " " & _talentId & "rank" & _talentRank
                            End If
                        ElseIf talentlist.Contains(_talentId & "rank2") Then
                            If CInt(Val(_talentRank)) <= 2 Then
                            Else
                                runSQLCommand_characters_string(
                                    "UPDATE " & GlobalVariables.targetStructure.character_talent_tbl(0) & " SET " &
                                    GlobalVariables.targetStructure.talent_rank_col(0) & "='" & _talentRank & "' WHERE " &
                                    GlobalVariables.targetStructure.talent_guid_col(0) & "='" & characterguid.ToString &
                                    "' AND " & GlobalVariables.targetStructure.talent_talent_col(0) & "='" & _talentId &
                                    "' AND " &
                                    GlobalVariables.targetStructure.talent_spec_col(0) & "='0'", True)
                                talentlist = talentlist & " " & _talentId & "rank" & _talentRank
                            End If
                        ElseIf talentlist.Contains(_talentId & "rank1") Then
                            If CInt(Val(_talentRank)) <= 1 Then
                            Else
                                runSQLCommand_characters_string(
                                    "UPDATE " & GlobalVariables.targetStructure.character_talent_tbl(0) & " SET " &
                                    GlobalVariables.targetStructure.talent_rank_col(0) & "='" & _talentRank & "' WHERE " &
                                    GlobalVariables.targetStructure.talent_guid_col(0) & "='" & characterguid.ToString &
                                    "' AND " & GlobalVariables.targetStructure.talent_talent_col(0) & "='" & _talentId &
                                    "' AND " &
                                    GlobalVariables.targetStructure.talent_spec_col(0) & "='0'", True)
                                talentlist = talentlist & " " & _talentId & "rank" & _talentRank
                            End If
                        Else
                        End If
                    Else
                        runSQLCommand_characters_string(
                            "INSERT INTO " & GlobalVariables.targetStructure.character_talent_tbl(0) & " ( " &
                            GlobalVariables.targetStructure.talent_guid_col(0) & ", " &
                            GlobalVariables.targetStructure.talent_talent_col(0) & ", " &
                            GlobalVariables.targetStructure.talent_rank_col(0) & ", " &
                            GlobalVariables.targetStructure.talent_spec_col(0) & " ) VALUES ( '" &
                            characterguid.ToString &
                            "', '" & _talentId & "', '" &
                            _talentRank & "', '0' )", True)
                        talentlist = talentlist & " " & _talentId & "rank" & _talentRank
                    End If
                Else
                    If talentlist2.Contains(_talentId) Then
                        If talentlist2.Contains(_talentId & "rank5") Then
                        ElseIf talentlist2.Contains(_talentId & "rank4") Then
                            If CInt(Val(_talentRank2)) <= 4 Then
                            Else
                                runSQLCommand_characters_string(
                                    "UPDATE " & GlobalVariables.targetStructure.character_talent_tbl(0) & " SET " &
                                    GlobalVariables.targetStructure.talent_rank_col(0) & "='" & _talentRank2 &
                                    "' WHERE " &
                                    GlobalVariables.targetStructure.talent_guid_col(0) & "='" & characterguid.ToString &
                                    "' AND " & GlobalVariables.targetStructure.talent_talent_col(0) & "='" & _talentId &
                                    "' AND " &
                                    GlobalVariables.targetStructure.talent_spec_col(0) & "='1'", True)
                                talentlist2 = talentlist2 & " " & _talentId & "rank" & _talentRank2
                            End If
                        ElseIf talentlist2.Contains(_talentId & "rank3") Then
                            If CInt(Val(_talentRank2)) <= 3 Then
                            Else
                                runSQLCommand_characters_string(
                                    "UPDATE " & GlobalVariables.targetStructure.character_talent_tbl(0) & " SET " &
                                    GlobalVariables.targetStructure.talent_rank_col(0) & "='" & _talentRank2 &
                                    "' WHERE " &
                                    GlobalVariables.targetStructure.talent_guid_col(0) & "='" & characterguid.ToString &
                                    "' AND " & GlobalVariables.targetStructure.talent_talent_col(0) & "='" & _talentId &
                                    "' AND " &
                                    GlobalVariables.targetStructure.talent_spec_col(0) & "='1'", True)
                                talentlist2 = talentlist2 & " " & _talentId & "rank" & _talentRank2
                            End If
                        ElseIf talentlist2.Contains(_talentId & "rank2") Then
                            If CInt(Val(_talentRank2)) <= 2 Then
                            Else
                                runSQLCommand_characters_string(
                                    "UPDATE " & GlobalVariables.targetStructure.character_talent_tbl(0) & " SET " &
                                    GlobalVariables.targetStructure.talent_rank_col(0) & "='" & _talentRank2 &
                                    "' WHERE " &
                                    GlobalVariables.targetStructure.talent_guid_col(0) & "='" & characterguid.ToString &
                                    "' AND " & GlobalVariables.targetStructure.talent_talent_col(0) & "='" & _talentId &
                                    "' AND " &
                                    GlobalVariables.targetStructure.talent_spec_col(0) & "='1'", True)
                                talentlist2 = talentlist2 & " " & _talentId & "rank" & _talentRank2
                            End If
                        ElseIf talentlist2.Contains(_talentId & "rank1") Then
                            If CInt(Val(_talentRank2)) <= 1 Then
                            Else
                                runSQLCommand_characters_string(
                                    "UPDATE " & GlobalVariables.targetStructure.character_talent_tbl(0) & " SET " &
                                    GlobalVariables.targetStructure.talent_rank_col(0) & "='" & _talentRank2 &
                                    "' WHERE " &
                                    GlobalVariables.targetStructure.talent_guid_col(0) & "='" & characterguid.ToString &
                                    "' AND " & GlobalVariables.targetStructure.talent_talent_col(0) & "='" & _talentId &
                                    "' AND " &
                                    GlobalVariables.targetStructure.talent_spec_col(0) & "='1'", True)
                                talentlist2 = talentlist2 & " " & _talentId & "rank" & _talentRank2
                            End If
                        Else
                        End If
                    Else
                        runSQLCommand_characters_string(
                            "INSERT INTO " & GlobalVariables.targetStructure.character_talent_tbl(0) & " ( " &
                            GlobalVariables.targetStructure.talent_guid_col(0) & ", " &
                            GlobalVariables.targetStructure.talent_talent_col(0) & ", " &
                            GlobalVariables.targetStructure.talent_rank_col(0) & ", " &
                            GlobalVariables.targetStructure.talent_spec_col(0) & " ) VALUES ( '" &
                            characterguid.ToString &
                            "', '" & _talentId & "', '" & _talentRank2 & "', '1' )", True)
                        talentlist2 = talentlist2 & " " & _talentId & "rank" & _talentRank2
                    End If
                End If
            Next
        End Sub
    End Class
End Namespace