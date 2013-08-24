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
'*      /Filename:      AchievementParser
'*      /Description:   Contains functions for loading character achievement information 
'*                      from wow armory
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Imports NCFramework.EventLogging
Imports NCFramework.Conversions
Imports NCFramework.SpellItem_Information
Imports NCFramework.Basics
Imports System.Net

Public Class AchievementParser
    Public Sub loadAchievements(ByVal setId As Integer, ByVal apiLink As String)
        Dim client As New WebClient
        client.CheckProxy()
        '// Retrieving character
        Dim player As Character = GetCharacterSetBySetId(setId)
        player.Achievements = New List(Of Achievement)
        Try
            LogAppend("Loading character achievement information", "AchievementParser_loadAchievements", True)
            '// Using API to load achievement info
            Dim avContext As String = client.DownloadString(apiLink & "?fields=achievements")
            '// Splitting to create completed-achievements and timestamp string
            Dim avStr As String = splitString(avContext, "{""achievementsCompleted"":[", "],""") & ","
            Dim timeStr As String = splitString(avContext, """achievementsCompletedTimestamp"":[", "],""")
            If avStr.Length > 5 Then '// Should check if av count is > 0 // TODO Confirm
                Dim loopcounter As Integer = 0
                Dim excounter As Integer = UBound(Split(avStr, ","))
                Dim partsAV() As String = avStr.Split(","c)
                Dim partsTIME() As String = timeStr.Split(","c)
                Do
                    Dim avId As String = partsAV(loopcounter)
                    Dim timeStamp = partsTIME(loopcounter)
                    If timeStamp.Contains("000") Then
                        Try
                            timeStamp = timeStamp.Remove(timeStamp.Length - 3, 3)
                        Catch : End Try
                    End If
                    loopcounter += 1
                    LogAppend("Adding achievement " & avId & " with timestamp " & timeStamp, "AchievementParser_loadAchievements", False)
                    Dim av As New Achievement
                    av.Id = TryInt(avId)
                    av.GainDate = TryInt(timeStamp)
                    av.OwnerSet = setId
                    player.Achievements.Add(av)
                Loop Until loopcounter = excounter
                '// Saving changes to character
                SetCharacterSet(setId, player)
            End If
        Catch ex As Exception
            LogAppend("Exception occured: " & vbNewLine & ex.ToString(), "AchievementParser_loadAchievements", False)
        End Try
    End Sub
End Class
