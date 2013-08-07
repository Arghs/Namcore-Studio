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
'*      /Filename:      CharacterReputationHandler
'*      /Description:   Contains functions for extracting information about the gained 
'*                      reputation of a specific character
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports Namcore_Studio.test1
Imports Namcore_Studio.EventLogging
Imports Namcore_Studio.Basics
Imports Namcore_Studio.GlobalVariables
Imports Namcore_Studio.CommandHandler
Imports Namcore_Studio.Conversions
Public Class CharacterReputationHandler
    Public Sub GetCharacterReputation(ByVal characterGuid As Integer, ByVal setId As Integer, ByVal accountId As Integer)
        LogAppend("Loading character reputation for characterGuid: " & characterGuid & " and setId: " & setId, "CharacterReputationHandler_GetCharacterReputation", True)
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
    Private Sub loadAtArcemu(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)
        LogAppend("Loading character reputation @loadAtArcemu", "CharacterReputationHandler_loadAtArcemu", False)
        Dim tempdt As DataTable = ReturnDataTable("SELECT " & sourceStructure.char_reputation_col(0) & " FROM " & sourceStructure.character_tbl(0) & " WHERE " & sourceStructure.char_guid_col(0) & "='" & charguid.ToString() & "'")
        Dim player As Character = GetCharacterSetBySetId(tar_setId)
        Try
            Dim lastcount As Integer = tempdt.Rows.Count
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim readedcode As String = (tempdt.Rows(count).Item(0)).ToString
                    Dim excounter As Integer = UBound(readedcode.Split(CChar(",")))
                    Dim loopcounter As Integer = 0
                    Dim finalcounter As Integer = CInt(excounter / 4)
                    Dim partscounter As Integer = 0
                    Do
                        Dim rep As New Reputation
                        Dim parts() As String = readedcode.Split(","c)
                        rep.faction = TryInt(parts(partscounter).ToString)
                        partscounter += 1
                        rep.flags = TryInt(parts(partscounter).ToString)
                        partscounter += 1
                        rep.standing = TryInt(parts(partscounter).ToString)
                        partscounter += 2
                        player.PlayerReputation.Add(rep)
                        loopcounter += 1
                    Loop Until loopcounter = finalcounter
                    count += 1
                Loop Until count = lastcount
            Else
                LogAppend("No reputation found!", "CharacterReputationHandler_loadAtArcemu", True)
            End If
        Catch ex As Exception
            LogAppend("Something went wrong while loading character reputation! -> skipping -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterReputationHandler_loadAtArcemu", True, True)
            Exit Sub
        End Try
        SetCharacterSet(tar_setId, player)
    End Sub
    Private Sub loadAtTrinity(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)
        LogAppend("Loading character reputation @loadAtTrinity", "CharacterReputationHandler_loadAtTrinity", False)
        Dim tempdt As DataTable = ReturnDataTable("SELECT " & sourceStructure.rep_faction_col(0) & ", " & sourceStructure.rep_standing_col(0) & ", " & sourceStructure.rep_flags_col(0) &
                                                  " FROM " & sourceStructure.character_reputation_tbl(0) & " WHERE " & sourceStructure.rep_guid_col(0) & "='" & charguid.ToString() & "'")
        Dim player As Character = GetCharacterSetBySetId(tar_setId)
        Try
            Dim lastcount As Integer = tempdt.Rows.Count
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim rep As New Reputation
                    rep.faction = TryInt((tempdt.Rows(count).Item(0)).ToString)
                    rep.standing = TryInt((tempdt.Rows(count).Item(1)).ToString)
                    rep.flags = TryInt((tempdt.Rows(count).Item(2)).ToString)
                    player.PlayerReputation.Add(rep)
                    count += 1
                Loop Until count = lastcount
            Else
                LogAppend("No reputation found!", "CharacterReputationHandler_loadAtTrinity", True)
            End If
        Catch ex As Exception
            LogAppend("Something went wrong while loading character reputation! -> skipping -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterReputationHandler_loadAtTrinity", True, True)
            Exit Sub
        End Try
        SetCharacterSet(tar_setId, player)
    End Sub
    Private Sub loadAtTrinityTBC(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)

    End Sub
    Private Sub loadAtMangos(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)
        LogAppend("Loading character reputation @loadAtMangos", "CharacterReputationHandler_loadAtMangos", False)
        Dim tempdt As DataTable = ReturnDataTable("SELECT " & sourceStructure.rep_faction_col(0) & ", " & sourceStructure.rep_standing_col(0) & ", " & sourceStructure.rep_flags_col(0) &
                                                  " FROM " & sourceStructure.character_reputation_tbl(0) & " WHERE " & sourceStructure.rep_guid_col(0) & "='" & charguid.ToString() & "'")
        Dim player As Character = GetCharacterSetBySetId(tar_setId)
        Try
            Dim lastcount As Integer = tempdt.Rows.Count
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim rep As New Reputation
                    rep.faction = TryInt((tempdt.Rows(count).Item(0)).ToString)
                    rep.standing = TryInt((tempdt.Rows(count).Item(1)).ToString)
                    rep.flags = TryInt((tempdt.Rows(count).Item(2)).ToString)
                    player.PlayerReputation.Add(rep)
                    count += 1
                Loop Until count = lastcount
            Else
                LogAppend("No reputation found!", "CharacterReputationHandler_loadAtMangos", True)
            End If
        Catch ex As Exception
            LogAppend("Something went wrong while loading character reputation! -> skipping -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterReputationHandler_loadAtMangos", True, True)
            Exit Sub
        End Try
        SetCharacterSet(tar_setId, player)
    End Sub
End Class
