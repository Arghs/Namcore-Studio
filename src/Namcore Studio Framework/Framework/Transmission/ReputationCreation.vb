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
'*      /Filename:      ReputationCreation
'*      /Description:   Includes functions for setting the faction reputations of a specific
'*                      character
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.Database
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Modules

Namespace Framework.Transmission
    Public Class ReputationCreation
        Public Sub AddCharacterReputation(ByVal player As Character,
                                          Optional charguid As Integer = 0)
            If charguid = 0 Then charguid = player.Guid
            LogAppend("Adding reputation to character: " & charguid.ToString(),
                      "ReputationCreation_AddCharacterReputation", True)
            Try
                Select Case GlobalVariables.targetCore
                    Case "arcemu"
                        CreateAtArcemu(charguid, player)
                    Case "trinity", "mangos", "trinitytbc"
                        CreateAtTrinity(charguid, player)
                End Select
            Catch ex As Exception
                LogAppend("Exception occured: " & ex.ToString(),
                "ReputationCreation_AddCharacterReputation", False, True)
            End Try
        End Sub

        Private Sub CreateAtArcemu(ByVal characterguid As Integer, ByVal player As Character)
            LogAppend("Adding reputation to character at arcemu", "ReputationCreation_CreateAtArcemu", False)
            Dim useStructure As DbStructure = GlobalVariables.targetStructure
            Dim repString As String = ""
            If player.PlayerReputation Is Nothing Then player.PlayerReputation = New List(Of Reputation)()
            Dim cnt As Integer = 0
            For Each playerReputation As Reputation In player.PlayerReputation '// TODO: Needs validation
                repString &= playerReputation.Faction.ToString() & "," &
                             playerReputation.Flags.GetHashCode.ToString() & ",0," &
                             playerReputation.Standing.ToString()
                cnt += 1
            Next
            runSQLCommand_characters_string(
                "UPDATE `" & useStructure.character_tbl(0) &
                "` SET `" & useStructure.char_reputation_col(0) &
                "` = '" & repString &
                "' WHERE `" & useStructure.char_guid_col(0) & "` = '" & characterguid.ToString() & "'")
            LogAppend("Added " & cnt.ToString() & " faction reputations", "ReputationCreation_createAtArcemu", True)
        End Sub

        Private Sub CreateAtTrinity(ByVal characterguid As Integer, ByVal player As Character)
            LogAppend("Adding reputation to character at trinity", "ReputationCreation_createAtTrinity", False)
            Dim useStructure As DbStructure = GlobalVariables.targetStructure
            If player.PlayerReputation Is Nothing Then player.PlayerReputation = New List(Of Reputation)()
            Dim cnt As Integer = 0
            For Each playerReputation As Reputation In player.PlayerReputation
                runSQLCommand_characters_string(
                    "INSERT INTO `" & useStructure.character_reputation_tbl(0) &
                    "` ( `" & useStructure.rep_guid_col(0) &
                    "`, `" & useStructure.rep_faction_col(0) &
                    "`, `" & useStructure.rep_standing_col(0) &
                    "`, `" & useStructure.rep_flags_col(0) &
                    "` ) VALUES ( '" & characterguid.ToString() &
                    "', '" & playerReputation.Faction.ToString() &
                    "', '" & playerReputation.Standing.ToString() &
                    "', '" & playerReputation.Flags.GetHashCode().ToString() & "' )")
                cnt += 1
            Next
            LogAppend("Added " & cnt.ToString() & " faction reputations", "ReputationCreation_createAtTrinity", True)
        End Sub
    End Class
End Namespace