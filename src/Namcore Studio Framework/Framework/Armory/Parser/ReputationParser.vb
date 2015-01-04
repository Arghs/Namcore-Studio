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
'*      /Filename:      ReputationParser
'*      /Description:   Contains functions for loading character reputation information from 
'*                      wow armory
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports Newtonsoft.Json.Linq
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Extension
Imports NCFramework.Framework.Modules
Imports System.Net

Namespace Framework.Armory.Parser
    Public Class ReputationParser
        Public Sub LoadReputation(ByVal setId As Integer, ByVal apiLink As String, ByVal account As Account)
            Dim client As New WebClient
            client.CheckProxy()
            '// Retrieving character
            Dim player As Character = GetCharacterSetBySetId(setId, account)
            player.PlayerReputation = New List(Of Reputation)
            Try
                LogAppend(
                    "Loading character reputation information - setId: " & setId.ToString() & " - apiLink: " & apiLink,
                    "ReputationParser_LoadReputation", True)
                Dim reputationContext As String = client.DownloadString(apiLink & "?fields=reputation")
                If Not reputationContext.Contains("""reputation"":") Then
                    LogAppend("No reputation information found!?", "ReputationParser_LoadReputation", True)
                    Exit Sub '// Skip if no reputation info
                End If
                Dim jResults As JObject = JObject.Parse(reputationContext)
                Dim results As List(Of JToken) = jResults.Children().ToList()
                Dim token As JProperty =
                        CType(results.Find(Function(jtoken) CType(jtoken, JProperty).Name = "reputation"),
                              JProperty)
                If token.HasChildren Then
                    For i = 0 To token.GetObjects().Count - 1
                        Dim repToken As List(Of JProperty) = token.GetObjects()(i).Children.Cast(Of JProperty).ToList()
                        Dim pReputation As New Reputation
                        pReputation.Faction = CInt(repToken.GetValue("id"))
                        pReputation.Max = CInt(repToken.GetValue("max"))
                        pReputation.Value = CInt(repToken.GetValue("value"))
                        pReputation.Name = repToken.GetValue("name")
                        pReputation.Status = CType(CInt(repToken.GetValue("standing")), Reputation.RepStatus)
                        pReputation.Flags = Reputation.FlagEnum.FACTION_FLAG_VISIBLE
                        pReputation.Standing = pReputation.Value
                        With pReputation
                            If .Status > 3 Then .Standing += 3000
                            If .Status > 4 Then .Standing += 6000
                            If .Status > 5 Then .Standing += 12000
                            If .Status > 6 Then .Standing += 21000
                        End With
                        LogAppend(
                            "Adding reputation (factionID/standing): (" & pReputation.Faction.ToString() & "/" &
                            pReputation.Value.ToString() & ")",
                            "ReputationParser_LoadReputation", False)
                        player.PlayerReputation.Add(pReputation)
                    Next i
                End If
                LogAppend("Loaded " & player.PlayerReputation.Count.ToString & " factions",
                          "ReputationParser_LoadReputation", True)
                '// Saving changes to character
                SetCharacterSet(setId, player, GetAccountSetBySetId(player.AccountSet))
            Catch ex As Exception
                LogAppend("Something went wrong! -> Exception is: ###START###" & ex.ToString() & "###END###",
                          "ReputationParser_LoadReputation", False, True)
            End Try
        End Sub
    End Class
End Namespace