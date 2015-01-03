'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013-2015 NamCore Studio <https://github.com/megasus/Namcore-Studio>
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
'*      /Filename:      AchievementParser
'*      /Description:   Contains functions for loading character achievement information 
'*                      from wow armory
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports Newtonsoft.Json.Linq
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Extension
Imports NCFramework.Framework.Modules
Imports System.Net

Namespace Framework.Armory.Parser
    Public Class AchievementParser
        Public Sub LoadAchievements(ByVal setId As Integer, ByVal apiLink As String, ByVal account As Account)
            Dim client As New WebClient
            client.CheckProxy()
            '// Retrieving character
            Dim player As Character = GetCharacterSetBySetId(setId, account)
            player.Achievements = New List(Of Achievement)
            Try
                LogAppend(
                    "Loading character achievement information - setId: " & setId.ToString() & " - apiLink: " & apiLink,
                    "AchievementParser_LoadAchievements", True)
                '// Using API to load achievement info
                Dim avContext As String = client.DownloadString(apiLink & "?fields=achievements")
                If Not avContext.Contains("""achievements"":") Then
                    LogAppend("No achievements found!?", "AchievementParser_LoadAchievements", True)
                    Exit Sub
                End If
                Dim jResults As JObject = JObject.Parse(avContext)
                Dim results As List(Of JToken) = jResults.Children().ToList()
                Dim token As JProperty =
                        CType(results.Find(Function(jtoken) CType(jtoken, JProperty).Name = "achievements"),
                              JProperty)
                If token.HasChildren() Then
                    Dim completedAvs() As Integer =
                            token.GetValues("achievementsCompleted").ToList().ConvertAll(
                                Function(str) Integer.Parse(str)) _
                            .ToArray()
                    Dim completedStamps() As Long =
                            token.GetValues("achievementsCompletedTimestamp").ToList().ConvertAll(
                                Function(str) Long.Parse(str)).ToArray()
                    If Not completedAvs Is Nothing AndAlso Not completedStamps Is Nothing Then
                        For i = 0 To completedAvs.Count - 1
                            Dim playerAv As New Achievement
                            playerAv.Id = completedAvs(i)
                            If completedStamps.Count < i Then
                                LogAppend(
                                    "No timestamp found for achievement: " & playerAv.Id.ToString() & " @" &
                                    i.ToString(), "AchievementParser_LoadAchievements", True, True)
                                Exit Sub
                            End If
                            playerAv.GainDate = CInt(completedStamps(i) / 1000)
                            playerAv.OwnerSet = setId
                            player.Achievements.Add(playerAv)
                        Next i
                    End If
                End If
                LogAppend("Loaded " & player.Achievements.Count.ToString & " achievements!",
                          "AchievementParser_LoadAchievements",
                          True)

                SetCharacterSet(setId, player, GetAccountSetBySetId(player.AccountSet))

            Catch ex As Exception
                LogAppend("Exception occured: " & vbNewLine & ex.ToString(), "AchievementParser_LoadAchievements", False,
                          True)
            End Try
        End Sub
    End Class
End Namespace