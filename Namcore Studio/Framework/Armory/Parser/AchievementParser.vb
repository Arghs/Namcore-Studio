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

Imports Namcore_Studio.EventLogging
Imports Namcore_Studio.Conversions
Imports Namcore_Studio.SpellItem_Information
Imports Namcore_Studio.Basics
Imports System.Net

Public Class AchievementParser
    Public Shared Sub loadAchievements(ByVal setId As Integer, ByVal apiLink As String)
        Dim client As New WebClient
        Dim avLst As New List(Of String)
        Try
            Dim avContext As String = client.DownloadString(apiLink & "?fields=achievements")
            Dim avStr As String = splitString(avContext, "{""achievementsCompleted"":[", "],""") & ","
            Dim timeStr As String = splitString(avContext, """achievementsCompletedTimestamp"":[", "],""")
            If Not avStr.Length > 5 Then
                Dim loopcounter As Integer = 0
                Dim excounter As Integer = UBound(Split(avStr, ","))
                Dim partsAV() As String = avStr.Split(","c)
                Dim partsTIME() As String = TimeString.Split(","c)
                Do
                    Dim avId As String = partsAV(loopcounter)
                    Dim timeStamp = partsTIME(loopcounter)
                    If timestamp.Contains("000") Then
                        Try
                            timestamp = timestamp.Remove(timestamp.Length - 3, 3)
                        Catch : End Try
                    End If
                    loopcounter += 1
                    avLst.Add("<av>" & avId & "</av><date>" & timeStamp & "</date>")
                Loop Until loopcounter = excounter
                SetTemporaryCharacterInformation("@character_achievements", ConvertListToString(avLst), setId)
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
