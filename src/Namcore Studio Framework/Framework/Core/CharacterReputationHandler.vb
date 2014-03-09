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
'*      /Filename:      CharacterReputationHandler
'*      /Description:   Contains functions for extracting information about the gained 
'*                      reputation of a specific character
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.Extension
Imports NCFramework.Framework.Database
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Modules

Namespace Framework.Core
    Public Class CharacterReputationHandler
        Public Sub GetCharacterReputation(ByVal characterGuid As Integer, ByVal setId As Integer,
                                          ByVal account As Account)
            LogAppend("Loading character reputation for characterGuid: " & characterGuid & " and setId: " & setId,
                      "CharacterReputationHandler_GetCharacterReputation", True)
            Dim player As Character = GetCharacterSetBySetId(setId, account)
            player.PlayerReputation = New List(Of Reputation)()
            SetCharacterSet(setId, player, account)
            Select Case GlobalVariables.sourceCore
                Case "arcemu"
                    LoadAtArcemu(characterGuid, setId, account)
                Case "trinity"
                    LoadAtTrinity(characterGuid, setId, account)
                Case "trinitytbc"
                    'todo LoadAtTrinityTBC(characterGuid, setId, accountId)
                Case "mangos"
                    LoadAtMangos(characterGuid, setId, account)
            End Select
        End Sub

        Private Sub LoadAtArcemu(ByVal charguid As Integer, ByVal tarSetId As Integer, ByVal account As Account)
            LogAppend("Loading character reputation @LoadAtArcemu", "CharacterReputationHandler_LoadAtArcemu", False)
            Dim tempdt As DataTable =
                    ReturnDataTable(
                        "SELECT " & GlobalVariables.sourceStructure.char_reputation_col(0) & " FROM " &
                        GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                        GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString() & "'")
            Dim player As Character = GetCharacterSetBySetId(tarSetId, account)
            Try
                Dim lastcount As Integer = tempdt.Rows.Count
                Dim count As Integer = 0
                If Not lastcount = 0 Then
                    Do
                        Dim readedcode As String = (tempdt.Rows(count).Item(0)).ToString
                        Dim excounter As Integer = UBound(readedcode.Split(CChar(",")))
                        Dim loopcounter As Integer = 0
                        Dim finalcounter As Integer = CInt(excounter/4)
                        Dim partscounter As Integer = 0
                        Do
                            Dim rep As New Reputation
                            Dim parts() As String = readedcode.Split(","c)
                            rep.Faction = TryInt(parts(partscounter).ToString)
                            partscounter += 1
                            rep.Flags = CType(TryInt(parts(partscounter).ToString), Reputation.FlagEnum)
                            partscounter += 1
                            rep.Standing = TryInt(parts(partscounter).ToString)
                            partscounter += 2
                            rep.UpdateValueMax()
                            If player.PlayerReputation Is Nothing Then _
                                player.PlayerReputation = New List(Of Reputation)()
                            player.PlayerReputation.Add(rep)
                            loopcounter += 1
                        Loop Until loopcounter = finalcounter
                        count += 1
                    Loop Until count = lastcount
                Else
                    LogAppend("No reputation found!", "CharacterReputationHandler_LoadAtArcemu", True)
                End If
            Catch ex As Exception
                LogAppend(
                    "Something went wrong while loading character reputation! -> skipping -> Exception is: ###START###" &
                    ex.ToString() & "###END###", "CharacterReputationHandler_LoadAtArcemu", True, True)
                Exit Sub
            End Try
            SetCharacterSet(tarSetId, player, account)
        End Sub

        Private Sub LoadAtTrinity(ByVal charguid As Integer, ByVal tarSetId As Integer, ByVal account As Account)
            LogAppend("Loading character reputation @LoadAtTrinity", "CharacterReputationHandler_LoadAtTrinity", False)
            Dim tempdt As DataTable =
                    ReturnDataTable(
                        "SELECT " & GlobalVariables.sourceStructure.rep_faction_col(0) & ", " &
                        GlobalVariables.sourceStructure.rep_standing_col(0) & ", " &
                        GlobalVariables.sourceStructure.rep_flags_col(0) &
                        " FROM " & GlobalVariables.sourceStructure.character_reputation_tbl(0) & " WHERE " &
                        GlobalVariables.sourceStructure.rep_guid_col(0) & "='" & charguid.ToString() & "'")
            Dim player As Character = GetCharacterSetBySetId(tarSetId, account)
            Try
                Dim lastcount As Integer = tempdt.Rows.Count
                Dim count As Integer = 0
                If Not lastcount = 0 Then
                    Do
                        Dim rep As New Reputation
                        rep.Faction = TryInt((tempdt.Rows(count).Item(0)).ToString)
                        rep.Standing = TryInt((tempdt.Rows(count).Item(1)).ToString)
                        rep.Flags = CType(TryInt((tempdt.Rows(count).Item(2)).ToString), Reputation.FlagEnum)
                        rep.UpdateValueMax()
                        If player.PlayerReputation Is Nothing Then player.PlayerReputation = New List(Of Reputation)()
                        player.PlayerReputation.Add(rep)
                        count += 1
                    Loop Until count = lastcount
                Else
                    LogAppend("No reputation found!", "CharacterReputationHandler_LoadAtTrinity", True)
                End If
            Catch ex As Exception
                LogAppend(
                    "Something went wrong while loading character reputation! -> skipping -> Exception is: ###START###" &
                    ex.ToString() & "###END###", "CharacterReputationHandler_LoadAtTrinity", True, True)
                Exit Sub
            End Try
            SetCharacterSet(tarSetId, player, account)
        End Sub

        Private Sub LoadAtMangos(ByVal charguid As Integer, ByVal tarSetId As Integer, ByVal account As Account)
            LogAppend("Loading character reputation @LoadAtMangos", "CharacterReputationHandler_LoadAtMangos", False)
            Dim tempdt As DataTable =
                    ReturnDataTable(
                        "SELECT " & GlobalVariables.sourceStructure.rep_faction_col(0) & ", " &
                        GlobalVariables.sourceStructure.rep_standing_col(0) & ", " &
                        GlobalVariables.sourceStructure.rep_flags_col(0) &
                        " FROM " & GlobalVariables.sourceStructure.character_reputation_tbl(0) & " WHERE " &
                        GlobalVariables.sourceStructure.rep_guid_col(0) & "='" & charguid.ToString() & "'")
            Dim player As Character = GetCharacterSetBySetId(tarSetId, account)
            Try
                Dim lastcount As Integer = tempdt.Rows.Count
                Dim count As Integer = 0
                If Not lastcount = 0 Then
                    Do
                        Dim rep As New Reputation
                        rep.Faction = TryInt((tempdt.Rows(count).Item(0)).ToString)
                        rep.Standing = TryInt((tempdt.Rows(count).Item(1)).ToString)
                        rep.Flags = CType(TryInt((tempdt.Rows(count).Item(2)).ToString), Reputation.FlagEnum)
                        rep.UpdateValueMax()
                        If player.PlayerReputation Is Nothing Then player.PlayerReputation = New List(Of Reputation)()
                        player.PlayerReputation.Add(rep)
                        count += 1
                    Loop Until count = lastcount
                Else
                    LogAppend("No reputation found!", "CharacterReputationHandler_LoadAtMangos", True)
                End If
            Catch ex As Exception
                LogAppend(
                    "Something went wrong while loading character reputation! -> skipping -> Exception is: ###START###" &
                    ex.ToString() & "###END###", "CharacterReputationHandler_LoadAtMangos", True, True)
                Exit Sub
            End Try
            SetCharacterSet(tarSetId, player, account)
        End Sub
    End Class
End Namespace