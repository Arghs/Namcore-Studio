'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013 NamCore Studio <https://github.com/megasus/Namcore-Studio>
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
'*      /Filename:      CharacterSkillsHandler
'*      /Description:   Contains functions for extracting information about the skills of a
'*                      specific character
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Database
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Modules

Namespace Framework.Core
    Public Class CharacterSkillsHandler
        Public Sub GetCharacterSkills(ByVal characterGuid As Integer, ByVal setId As Integer, ByVal account As Account)
            LogAppend("Loading character skills for characterGuid: " & characterGuid & " and setId: " & setId,
                      "CharacterSkillsHandler_GetCharacterSkills", True)
            Select Case GlobalVariables.sourceCore
                Case "arcemu"
                    LoadAtArcemu(characterGuid, setId, account)
                Case "trinity"
                    LoadAtTrinity(characterGuid, setId, account)
                Case "trinitytbc"
                    'todo  LoadAtTrinityTBC(characterGuid, setId, accountId)
                Case "mangos"
                    LoadAtMangos(characterGuid, setId, account)
            End Select
        End Sub

        Private Sub LoadAtArcemu(ByVal charguid As Integer, ByVal tarSetId As Integer, ByVal account As Account)
            LogAppend("Loading character skills @LoadAtArcemu", "CharacterSkillsHandler_LoadAtArcemu", False)
            Dim tempdt As DataTable =
                    ReturnDataTable(
                        "SELECT " & GlobalVariables.sourceStructure.char_skills_col(0) & " FROM " &
                        GlobalVariables.sourceStructure.character_tbl(0) & " WHERE " &
                        GlobalVariables.sourceStructure.char_guid_col(0) & "='" & charguid.ToString() & "'")
            Dim player As Character = GetCharacterSetBySetId(tarSetId, account)
            Try
                Dim lastcount As Integer = tempdt.Rows.Count
                Dim count As Integer = 0
                If Not lastcount = 0 Then
                    Do
                        Dim readedcode As String = (tempdt.Rows(count).Item(0)).ToString
                        Dim excounter As Integer = UBound(readedcode.Split(CChar(";")))
                        Dim loopcounter As Integer = 0
                        Dim finalcounter As Integer = TryInt(excounter / 3)
                        Dim partscounter As Integer = 0
                        Do
                            Dim skl As New Skill
                            Dim parts() As String = readedcode.Split(","c)
                            skl.Id = TryInt(parts(partscounter).ToString)
                            partscounter += 1
                            skl.Value = TryInt(parts(partscounter).ToString)
                            partscounter += 1
                            skl.Max = TryInt(parts(partscounter).ToString)
                            partscounter += 1
                            If player.Skills Is Nothing Then player.Skills = New List(Of Skill)()
                            player.Skills.Add(skl)
                            loopcounter += 1
                        Loop Until loopcounter = finalcounter
                        count += 1
                    Loop Until count = lastcount
                Else
                    LogAppend("No skills found!", "CharacterSkillsHandler_LoadAtArcemu", True)
                End If
            Catch ex As Exception
                LogAppend(
                    "Something went wrong while loading character skills! -> skipping -> Exception is: ###START###" &
                    ex.ToString() & "###END###", "CharacterSkillsHandler_LoadAtArcemu", True, True)
                Exit Sub
            End Try
            SetCharacterSet(tarSetId, player, account)
        End Sub

        Private Sub LoadAtTrinity(ByVal charguid As Integer, ByVal tarSetId As Integer, ByVal account As Account)
            LogAppend("Loading character skills @LoadAtTrinity", "CharacterSkillsHandler_LoadAtTrinity", False)
            Dim tempdt As DataTable =
                    ReturnDataTable(
                        "SELECT " & GlobalVariables.sourceStructure.skill_skill_col(0) & ", `" &
                        GlobalVariables.sourceStructure.skill_value_col(0) & "`, " &
                        GlobalVariables.sourceStructure.skill_max_col(0) &
                        " FROM " & GlobalVariables.sourceStructure.character_skills_tbl(0) & " WHERE " &
                        GlobalVariables.sourceStructure.skill_guid_col(0) & "='" & charguid.ToString() & "'")
            Dim player As Character = GetCharacterSetBySetId(tarSetId, account)
            Try
                Dim lastcount As Integer = tempdt.Rows.Count
                Dim count As Integer = 0
                If Not lastcount = 0 Then
                    Do
                        Dim skl As New Skill
                        skl.Id = TryInt((tempdt.Rows(count).Item(0)).ToString)
                        skl.Value = TryInt((tempdt.Rows(count).Item(1)).ToString)
                        skl.Max = TryInt((tempdt.Rows(count).Item(2)).ToString)
                        If player.Skills Is Nothing Then player.Skills = New List(Of Skill)()
                        player.Skills.Add(skl)
                        count += 1
                    Loop Until count = lastcount
                Else
                    LogAppend("No skills found!", "CharacterSkillsHandler_LoadAtTrinity", True)
                End If
            Catch ex As Exception
                LogAppend(
                    "Something went wrong while loading character skills! -> skipping -> Exception is: ###START###" &
                    ex.ToString() & "###END###", "CharacterSkillsHandler_LoadAtTrinity", True, True)
                Exit Sub
            End Try
            SetCharacterSet(tarSetId, player, account)
        End Sub

        Private Sub LoadAtMangos(ByVal charguid As Integer, ByVal tarSetId As Integer, ByVal account As Account)
            LogAppend("Loading character skills @LoadAtMangos", "CharacterSkillsHandler_LoadAtMangos", False)
            Dim tempdt As DataTable =
                    ReturnDataTable(
                        "SELECT " & GlobalVariables.sourceStructure.skill_skill_col(0) & ", `" &
                        GlobalVariables.sourceStructure.skill_value_col(0) & "`, " &
                        GlobalVariables.sourceStructure.skill_max_col(0) &
                        " FROM " & GlobalVariables.sourceStructure.character_skills_tbl(0) & " WHERE " &
                        GlobalVariables.sourceStructure.skill_guid_col(0) & "='" & charguid.ToString() & "'")
            Dim player As Character = GetCharacterSetBySetId(tarSetId, account)
            Try
                Dim lastcount As Integer = tempdt.Rows.Count
                Dim count As Integer = 0
                If Not lastcount = 0 Then
                    Do
                        Dim skl As New Skill
                        skl.Id = TryInt((tempdt.Rows(count).Item(0)).ToString)
                        skl.Value = TryInt((tempdt.Rows(count).Item(1)).ToString)
                        skl.Max = TryInt((tempdt.Rows(count).Item(2)).ToString)
                        If player.Skills Is Nothing Then player.Skills = New List(Of Skill)()
                        player.Skills.Add(skl)
                        count += 1
                    Loop Until count = lastcount
                Else
                    LogAppend("No skills found!", "CharacterSkillsHandler_LoadAtMangos", True)
                End If
            Catch ex As Exception
                LogAppend(
                    "Something went wrong while loading character skills! -> skipping -> Exception is: ###START###" &
                    ex.ToString() & "###END###", "CharacterSkillsHandler_LoadAtMangos", True, True)
                Exit Sub
            End Try
            SetCharacterSet(tarSetId, player, account)
        End Sub
    End Class
End Namespace