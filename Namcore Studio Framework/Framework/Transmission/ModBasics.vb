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
'*      /Filename:      ModBasics
'*      /Description:   Includes functions for modifying basic character information
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.Database
Imports NCFramework.Framework.Modules
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Functions

Namespace Framework.Transmission

    Public Class ModBasics
        Public Sub SetCharacterGender(ByVal gender As Integer, ByVal setId As Integer, Optional charguid As Integer = 0)
            If charguid = 0 Then charguid = GetCharacterSetBySetId(setId).Guid
            LogAppend("Setting character gender to : " & gender.ToString() & " // charguid is : " & charguid.ToString(),
                      "ModBasics_SetCharacterGender", True)
            Select Case GlobalVariables.targetCore
                Case "arcemu"
                    runSQLCommand_characters_string(
                        "UPDATE `" & GlobalVariables.targetStructure.character_tbl(0) & "` SET " &
                        GlobalVariables.targetStructure.char_gender_col(0) & "='" & gender.ToString() & "' WHERE " &
                        GlobalVariables.targetStructure.char_guid_col(0) &
                        "='" & charguid.ToString() & "'", True)
                Case "trinity"
                    runSQLCommand_characters_string(
                        "UPDATE `" & GlobalVariables.targetStructure.character_tbl(0) & "` SET " &
                        GlobalVariables.targetStructure.char_gender_col(0) & "='" & gender.ToString() & "' WHERE " &
                        GlobalVariables.targetStructure.char_guid_col(0) &
                        "='" & charguid.ToString() & "'", True)
                Case "trinitytbc"

                Case "mangos"
                    runSQLCommand_characters_string(
                        "UPDATE `" & GlobalVariables.targetStructure.character_tbl(0) & "` SET " &
                        GlobalVariables.targetStructure.char_gender_col(0) & "='" & gender.ToString() & "' WHERE " &
                        GlobalVariables.targetStructure.char_guid_col(0) &
                        "='" & charguid.ToString() & "'", True)
            End Select
        End Sub

        Public Sub SetCharacterRace(ByVal race As Integer, ByVal setId As Integer, Optional charguid As Integer = 0)
            If charguid = 0 Then charguid = GetCharacterSetBySetId(setId).Guid
            LogAppend("Setting character race to : " & race.ToString() & " // charguid is : " & charguid.ToString(),
                      "ModBasics_SetCharacterRace", True)
            Select Case GlobalVariables.targetCore
                Case "arcemu"
                    runSQLCommand_characters_string(
                        "UPDATE `" & GlobalVariables.targetStructure.character_tbl(0) & "` SET " &
                        GlobalVariables.targetStructure.char_race_col(0) & "='" & race.ToString() & "' WHERE " &
                        GlobalVariables.targetStructure.char_guid_col(0) &
                        "='" & charguid.ToString() & "'", True)
                Case "trinity"
                    runSQLCommand_characters_string(
                        "UPDATE `" & GlobalVariables.targetStructure.character_tbl(0) & "` SET " &
                        GlobalVariables.targetStructure.char_race_col(0) & "='" & race.ToString() & "' WHERE " &
                        GlobalVariables.targetStructure.char_guid_col(0) &
                        "='" & charguid.ToString() & "'", True)
                Case "trinitytbc"

                Case "mangos"
                    runSQLCommand_characters_string(
                        "UPDATE `" & GlobalVariables.targetStructure.character_tbl(0) & "` SET " &
                        GlobalVariables.targetStructure.char_race_col(0) & "='" & race.ToString() & "' WHERE " &
                        GlobalVariables.targetStructure.char_guid_col(0) &
                        "='" & charguid.ToString() & "'", True)
               End Select
        End Sub

        Public Sub SetCharacterLevel(ByVal level As Integer, ByVal setId As Integer, Optional charguid As Integer = 0)
            If charguid = 0 Then charguid = GetCharacterSetBySetId(setId).Guid
            LogAppend("Setting character level to : " & level.ToString() & " // charguid is : " & charguid.ToString(),
                      "ModBasics_SetCharacterLevel", True)
            Select Case GlobalVariables.targetCore
                Case "arcemu"
                    runSQLCommand_characters_string(
                        "UPDATE `" & GlobalVariables.targetStructure.character_tbl(0) & "` SET " &
                        GlobalVariables.targetStructure.char_level_col(0) & "='" & level.ToString() & "' WHERE " &
                        GlobalVariables.targetStructure.char_guid_col(0) &
                        "='" & charguid.ToString() & "'", True)
                Case "trinity"
                    runSQLCommand_characters_string(
                        "UPDATE `" & GlobalVariables.targetStructure.character_tbl(0) & "` SET " &
                        GlobalVariables.targetStructure.char_level_col(0) & "='" & level.ToString() & "' WHERE " &
                        GlobalVariables.targetStructure.char_guid_col(0) &
                        "='" & charguid.ToString() & "'", True)
                Case "trinitytbc"

                Case "mangos"
                    runSQLCommand_characters_string(
                        "UPDATE `" & GlobalVariables.targetStructure.character_tbl(0) & "` SET " &
                        GlobalVariables.targetStructure.char_level_col(0) & "='" & level.ToString() & "' WHERE " &
                        GlobalVariables.targetStructure.char_guid_col(0) &
                        "='" & charguid.ToString() & "'", True)
               End Select
        End Sub

        Public Sub SetCharacterClass(ByVal cclass As Integer, ByVal setId As Integer, Optional charguid As Integer = 0)
            If charguid = 0 Then charguid = GetCharacterSetBySetId(setId).Guid
            LogAppend("Setting character gender to : " & Cclass.ToString() & " // charguid is : " & charguid.ToString(),
                      "ModBasics_SetCharacterClass", True)
            Select Case GlobalVariables.targetCore
                Case "arcemu"
                    runSQLCommand_characters_string(
                        "UPDATE `" & GlobalVariables.targetStructure.character_tbl(0) & "` SET " &
                        GlobalVariables.targetStructure.char_class_col(0) & "='" & Cclass.ToString() & "' WHERE " &
                        GlobalVariables.targetStructure.char_guid_col(0) &
                        "='" & charguid.ToString() & "'", True)
                Case "trinity"
                    runSQLCommand_characters_string(
                        "UPDATE `" & GlobalVariables.targetStructure.character_tbl(0) & "` SET " &
                        GlobalVariables.targetStructure.char_class_col(0) & "='" & Cclass.ToString() & "' WHERE " &
                        GlobalVariables.targetStructure.char_guid_col(0) &
                        "='" & charguid.ToString() & "'", True)
                Case "trinitytbc"

                Case "mangos"
                    runSQLCommand_characters_string(
                        "UPDATE `" & GlobalVariables.targetStructure.character_tbl(0) & "` SET " &
                        GlobalVariables.targetStructure.char_class_col(0) & "='" & Cclass.ToString() & "' WHERE " &
                        GlobalVariables.targetStructure.char_guid_col(0) &
                        "='" & charguid.ToString() & "'", True)
               End Select
        End Sub

        Public Sub SetCharacterGold(ByVal gold As Integer, ByVal setId As Integer, Optional charguid As Integer = 0)
            If charguid = 0 Then charguid = GetCharacterSetBySetId(setId).Guid
            LogAppend("Setting character gold to : " & gold.ToString() & " // charguid is : " & charguid.ToString(),
                      "ModBasics_SetCharacterGold", True)
            Select Case GlobalVariables.targetCore
                Case "arcemu"
                    runSQLCommand_characters_string(
                        "UPDATE `" & GlobalVariables.targetStructure.character_tbl(0) & "` SET " &
                        GlobalVariables.targetStructure.char_gold_col(0) & "='" & gold.ToString() & "' WHERE " &
                        GlobalVariables.targetStructure.char_guid_col(0) &
                        "='" & charguid.ToString() & "'", True)
                Case "trinity"
                    runSQLCommand_characters_string(
                        "UPDATE `" & GlobalVariables.targetStructure.character_tbl(0) & "` SET " &
                        GlobalVariables.targetStructure.char_gold_col(0) & "='" & gold.ToString() & "' WHERE " &
                        GlobalVariables.targetStructure.char_guid_col(0) &
                        "='" & charguid.ToString() & "'", True)
                Case "trinitytbc"

                Case "mangos"
                    runSQLCommand_characters_string(
                        "UPDATE `" & GlobalVariables.targetStructure.character_tbl(0) & "` SET " &
                        GlobalVariables.targetStructure.char_gold_col(0) & "='" & gold.ToString() & "' WHERE " &
                        GlobalVariables.targetStructure.char_guid_col(0) &
                        "='" & charguid.ToString() & "'", True)
                 End Select
        End Sub
    End Class
End Namespace