'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013-2016 NamCore Studio <https://github.com/megasus/Namcore-Studio>
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
'*      /Filename:      UpdateReputationHandler
'*      /Description:   Handles character faction reputation update requests
'++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.Database
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Modules

Namespace Framework.Core.Update
    Public Class UpdateReputationHandler
        Public Sub UpdateReputation(player As Character, modPlayer As Character)
            LogAppend("Updating character Reputation", "UpdateReputationHandler_UpdateReputation", True)
            '// Any new factions?
            For Each fac As Reputation In _
                From fac1 In modPlayer.PlayerReputation
                    Let result = player.PlayerReputation.Find(Function(reputation) reputation.Faction = fac1.Faction)
                    Where result Is Nothing Select fac1
                AddReputation(modPlayer, fac)
            Next
            '// And deleted factions?
            For Each fac As Reputation In _
                From fac1 In player.PlayerReputation
                    Let result = modPlayer.PlayerReputation.Find(Function(reputation) reputation.Faction = fac1.Faction)
                    Where result Is Nothing Select fac1
                DeleteReputation(modPlayer, fac)
            Next
            '// Any modified factions?
            For Each fac As Reputation In _
                From fac1 In modPlayer.PlayerReputation
                    Let result = player.PlayerReputation.Find(Function(reputation) reputation.Faction = fac1.Faction)
                    Where Not result Is Nothing Where fac1.Flags <> result.Flags Or fac1.Standing <> result.Standing
                    Select fac1
                ModReputation(modPlayer, fac)
            Next
        End Sub

        Private Sub AddReputation(player As Character, fac As Reputation)
            Select Case GlobalVariables.sourceCore
                Case Modules.Core.ARCEMU
                    Dim repString As String = runSQLCommand_characters_string(
                        "SELECT `" & GlobalVariables.sourceStructure.char_reputation_col(0) &
                        "` FROM `" & GlobalVariables.sourceStructure.character_tbl(0) &
                        "` WHERE `" & GlobalVariables.sourceStructure.char_guid_col(0) &
                        "` = '" & player.Guid.ToString() & "'")
                    If repString Is Nothing Then repString = ""
                    repString &= fac.Faction.ToString() & "," &
                                 fac.Flags.GetHashCode.ToString() & ",0," &
                                 fac.Standing.ToString() '// TODO: Confirm
                    runSQLCommand_characters_string(
                        "UPDATE `" & GlobalVariables.sourceStructure.character_tbl(0) &
                        "` SET `" & GlobalVariables.sourceStructure.char_reputation_col(0) &
                        "` = '" & repString &
                        "' WHERE `" & GlobalVariables.sourceStructure.char_guid_col(0) & "` = '" &
                        player.Guid.ToString() & "'")
                Case Modules.Core.TRINITY, Modules.Core.MANGOS
                    runSQLCommand_characters_string(
                        "INSERT INTO `" & GlobalVariables.sourceStructure.character_reputation_tbl(0) &
                        "` ( `" & GlobalVariables.sourceStructure.rep_guid_col(0) &
                        "`, `" & GlobalVariables.sourceStructure.rep_faction_col(0) &
                        "`, `" & GlobalVariables.sourceStructure.rep_standing_col(0) &
                        "`, `" & GlobalVariables.sourceStructure.rep_flags_col(0) &
                        "` ) VALUES ( '" & player.Guid.ToString() &
                        "', '" & fac.Faction.ToString() &
                        "', '" & fac.Standing.ToString() &
                        "', '" & fac.Flags.GetHashCode().ToString() & "' )")
            End Select
            LogAppend("Added faction reputations", "UpdateReputationHandler_AddReputation", True)
        End Sub

        Private Sub DeleteReputation(player As Character, fac As Reputation)
            Select Case GlobalVariables.sourceCore
                Case Modules.Core.ARCEMU
                    Dim repString As String = runSQLCommand_characters_string(
                        "SELECT `" & GlobalVariables.sourceStructure.char_reputation_col(0) &
                        "` FROM `" & GlobalVariables.sourceStructure.character_tbl(0) &
                        "` WHERE `" & GlobalVariables.sourceStructure.char_guid_col(0) &
                        "` = '" & player.Guid.ToString() & "'")
                    If Not repString Is Nothing And repString.Contains(",") Then
                        Dim exCount As Integer = UBound(Split(repString, ","))
                        Dim parts() As String = repString.Split(","c)
                        For i = 0 To exCount Step 4
                            '// Find faction
                            If parts(i) = fac.Faction.ToString() Then
                                repString =
                                    repString.Replace(
                                        "," & parts(i) & "," & parts(i + 1) & "," & parts(i + 2) & "," & parts(i + 3) &
                                        ",", ",")
                                Exit For
                            End If
                        Next
                        runSQLCommand_characters_string(
                            "UPDATE `" & GlobalVariables.sourceStructure.character_tbl(0) &
                            "` SET `" & GlobalVariables.sourceStructure.char_reputation_col(0) &
                            "` = '" & repString &
                            "' WHERE `" & GlobalVariables.sourceStructure.char_guid_col(0) & "` = '" &
                            player.Guid.ToString() & "'")
                    End If
                Case Modules.Core.TRINITY, Modules.Core.MANGOS
                    runSQLCommand_characters_string(
                        "DELETE FROM `" & GlobalVariables.sourceStructure.character_reputation_tbl(0) &
                        " WHERE `" & GlobalVariables.sourceStructure.rep_guid_col(0) &
                        "` = '" & player.Guid.ToString() & "' AND `" &
                        GlobalVariables.sourceStructure.rep_faction_col(0) & "` = '" & fac.Faction.ToString() & "'")
            End Select
            LogAppend("Removed faction reputation", "UpdateReputationHandler_DeleteReputation", True)
        End Sub

        Private Sub ModReputation(player As Character, fac As Reputation)
            Select Case GlobalVariables.sourceCore
                Case Modules.Core.ARCEMU
                    Dim repString As String = runSQLCommand_characters_string(
                        "SELECT `" & GlobalVariables.sourceStructure.char_reputation_col(0) &
                        "` FROM `" & GlobalVariables.sourceStructure.character_tbl(0) &
                        "` WHERE `" & GlobalVariables.sourceStructure.char_guid_col(0) &
                        "` = '" & player.Guid.ToString() & "'")
                    If Not repString Is Nothing And repString.Contains(",") Then
                        Dim exCount As Integer = UBound(Split(repString, ","))
                        Dim parts() As String = repString.Split(","c)
                        For i = 0 To exCount Step 4
                            '// Find faction
                            If parts(i) = fac.Faction.ToString() Then
                                parts(i + 1) = fac.Flags.GetHashCode.ToString() '// TODO: Confirm
                                parts(i + 2) = "0"
                                parts(i + 3) = fac.Standing.ToString()
                                Exit For
                            End If
                        Next
                        repString = String.Join(",", parts)
                        runSQLCommand_characters_string(
                            "UPDATE `" & GlobalVariables.sourceStructure.character_tbl(0) &
                            "` SET `" & GlobalVariables.sourceStructure.char_reputation_col(0) &
                            "` = '" & repString &
                            "' WHERE `" & GlobalVariables.sourceStructure.char_guid_col(0) & "` = '" &
                            player.Guid.ToString() & "'")

                    End If
                Case Modules.Core.TRINITY, Modules.Core.MANGOS
                    runSQLCommand_characters_string(
                        "UPDATE `" & GlobalVariables.sourceStructure.character_reputation_tbl(0) &
                        "` SET  `" & GlobalVariables.sourceStructure.rep_standing_col(0) &
                        "` = '" & fac.Standing.ToString() &
                        "', `" & GlobalVariables.sourceStructure.rep_flags_col(0) &
                        "` = '" & fac.Flags.GetHashCode.ToString() &
                        "' WHERE `" & GlobalVariables.sourceStructure.rep_guid_col(0) &
                        "` = '" & player.Guid.ToString() &
                        "' AND `" & GlobalVariables.sourceStructure.rep_faction_col(0) &
                        "` = '" & fac.Faction.ToString() & "'")
            End Select
            LogAppend("Modified faction reputation", "UpdateReputationHandler_ModReputation", True)
        End Sub
    End Class
End Namespace