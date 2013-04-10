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
'*      /Filename:      ArmoryHandler
'*      /Description:   Contains functions for parsing character information from wow armory
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.Net
Imports System.Text
Imports Namcore_Studio.Basics
Imports Namcore_Studio.Conversions
Public Class ArmoryHandler

    Public Shared Sub LoadArmoryCharacters(ByVal LinkList As List(Of String))
        Dim setId As Integer = 0
        Dim CharacterContext As String
        Dim APILink As String
        Dim CharacterName As String
        Dim Client As New WebClient
        For Each ArmoryLink As String In LinkList
            Try
                CharacterContext = Client.DownloadString(ArmoryLink)
                Dim b() As Byte = Encoding.Default.GetBytes(CharacterContext)
                CharacterContext = Encoding.UTF8.GetString(b)
            Catch ex As Exception
                Continue For
            End Try
            CharacterName = splitString(CharacterContext, "<meta property=""og:title"" content=""", " @ ")
            APILink = "http://" & splitString(ArmoryLink, "http://", ".battle") & ".battle.net/api/wow/character/" & splitString(ArmoryLink, "/character/", "/") & "/" & CharacterName
            setId += 1
            SetTemporaryCharacterInformation("@character_name", CharacterName, setId)
            SetTemporaryCharacterInformation("@character_level", splitString(CharacterContext, "<span class=""level""><strong>", "</strong></span>"), setId)
            SetTemporaryCharacterInformation("@character_gender", splitString(Client.DownloadString(APILink), """gender"":", ","""), setId)
            SetTemporaryCharacterInformation("@character_race", GetRaceIdByName(splitString(CharacterContext, "/game/race/", """ class=")), setId)
            SetTemporaryCharacterInformation("@character_class", GetClassIdByName(splitString(CharacterContext, "/game/class/", """ class=")), setId)
        Next
    End Sub
End Class
