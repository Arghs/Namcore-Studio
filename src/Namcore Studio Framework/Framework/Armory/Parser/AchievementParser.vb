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
'*      /Filename:      AchievementParser
'*      /Description:   Contains functions for loading character achievement information 
'*                      from wow armory
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.Net
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Extension
Imports NCFramework.Framework.Modules

Namespace Framework.Armory.Parser

    Public Class AchievementParser
        Public Sub LoadAchievements(ByVal setId As Integer, ByVal apiLink As String, ByVal account As Account)
            Dim client As New WebClient
            client.CheckProxy()
            '// Retrieving character
            Dim player As Character = GetCharacterSetBySetId(setId, account)
            player.Achievements = New List(Of Achievement)
            Try
                LogAppend("Loading character achievement information", "AchievementParser_loadAchievements", True)
                '// Using API to load achievement info
                Dim avContext As String = client.DownloadString(apiLink & "?fields=achievements")
                '// Splitting to create completed-achievements and timestamp string
                Dim avStr As String = SplitString(avContext, "{""achievementsCompleted"":[", "],""") & ","
                Dim timeStr As String = SplitString(avContext, """achievementsCompletedTimestamp"":[", "],""")
                If avStr.Length > 5 Then '// Should check if av count is > 0 // TODO Confirm
                    Dim loopcounter As Integer = 0
                    Dim excounter As Integer = UBound(Split(avStr, ","))
                    Dim partsAv() As String = avStr.Split(","c)
                    Dim partsTime() As String = timeStr.Split(","c)
                    Do
                        Dim avId As String = partsAv(loopcounter)
                        Dim timeStamp = partsTime(loopcounter)
                        If timeStamp.Contains("000") Then
                            Try
                                timeStamp = timeStamp.Remove(timeStamp.Length - 3, 3)
                            Catch tmpex As Exception
                                LogAppend(
                                    "Exception during timestamp splitting! - timeStamp/loopcounter/excounter: " & timeStamp &
                                    "/" & loopcounter.ToString & "/" &
                                    excounter.ToString() & " # Exception is: " & tmpex.ToString(),
                                    "AchievementParser_loadAchievements", False, True)
                            End Try
                        End If
                        loopcounter += 1
                        LogAppend("Adding achievement " & avId & " with timestamp " & timeStamp,
                                  "AchievementParser_loadAchievements", False)
                        Dim av As New Achievement
                        av.Id = TryInt(avId)
                        av.GainDate = TryInt(timeStamp)
                        av.OwnerSet = setId
                        player.Achievements.Add(av)
                    Loop Until loopcounter = excounter
                    LogAppend("Loaded " & loopcounter.ToString & " achievements!", "AchievementParser_loadAchievements", True)
                    '// Saving changes to character
                    SetCharacterSet(setId, player, GetAccountSetBySetId(player.AccountSet))
                End If
            Catch ex As Exception
                LogAppend("Exception occured: " & vbNewLine & ex.ToString(), "AchievementParser_loadAchievements", False,
                          True)
            End Try
        End Sub
    End Class
End Namespace