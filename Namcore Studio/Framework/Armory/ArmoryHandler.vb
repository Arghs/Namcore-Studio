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
Imports Namcore_Studio.ItemParser
Imports Namcore_Studio.ReputationParser
Imports Namcore_Studio.AchievementParser
Imports Namcore_Studio.GlyphParser
Imports Namcore_Studio.EventLogging
Imports Namcore_Studio.Interface_Operator
Imports Namcore_Studio.GlobalVariables
Imports System.Threading

Public Class ArmoryHandler
   
    Public Shared Sub LoadArmoryCharacters(ByVal LinkList As List(Of String))
        LogAppend("Loading characters from Armory (" & LinkList.Count.ToString() & " character/s)", "ArmoryHandler_LoadArmoryCharacters", True)
        Dim setId As Integer = 0
        Dim CharacterContext As String
        Dim APILink As String
        Dim CharacterName As String
        Dim Client As New WebClient
        For Each ArmoryLink As String In LinkList
            Try
                LogAppend("URL is " & ArmoryLink, "ArmoryHandler_LoadArmoryCharacters", False)
                CharacterContext = Client.DownloadString(ArmoryLink)
                Dim b() As Byte = Encoding.Default.GetBytes(CharacterContext)
                CharacterContext = Encoding.UTF8.GetString(b)
            Catch ex As Exception
                LogAppend("Failed to load character context. Exception is: " & vbNewLine & ex.ToString(), "ArmoryHandler_LoadArmoryCharacters", True, True)
                Continue For
            End Try
            CharacterContext = CharacterContext.Replace("&#39;", "")
            CharacterName = splitString(CharacterContext, "<meta property=""og:title"" content=""", " @ ")

            APILink = "http://" & splitString(ArmoryLink, "http://", ".battle") & ".battle.net/api/wow/character/" & splitString(ArmoryLink, "/character/", "/") & "/" & CharacterName
            setId += 1
            LogAppend("Loading character " & CharacterName & " //ident is " & setId.ToString(), "ArmoryHandler_LoadArmoryCharacters", True)
            LogAppend("Loading basic character information", "ArmoryHandler_LoadArmoryCharacters", True)
            Dim Player As New Character(CharacterName, 0)
            Player.AccountId = 0
            Player.AccountName = "Armory"
            Player.Level = TryInt(splitString(CharacterContext, "<span class=""level""><strong>", "</strong></span>"))
            Player.Gender = TryInt(splitString(Client.DownloadString(APILink), """gender"":", ","""))
            Player.Race = TryInt(splitString(Client.DownloadString(APILink), """race"":", ","""))
            Player.Cclass = TryInt(GetClassIdByName(splitString(CharacterContext, "/game/class/", """ class=")))
            '// Character appearance
            Try
                LogAppend("Loading character appearance information", "ArmoryHandler_LoadArmoryCharacters", True)
                Dim appearanceContext As String = Client.DownloadString(APILink & "?fields=appearance")
                Dim app_face As String = Hex$(Long.Parse(splitString(appearanceContext, """faceVariation"":", ",")))
                Dim app_skin As String = Hex$(Long.Parse(splitString(appearanceContext, """skinColor"":", ",")))
                Dim app_hairStyle As String = Hex$(Long.Parse(splitString(appearanceContext, """hairVariation"":", ",")))
                Dim app_hairColor As String = Hex$(Long.Parse(splitString(appearanceContext, """hairColor"":", ",")))
                Dim app_featureVar As String = Hex$(Long.Parse(splitString(appearanceContext, """featureVariation"":", ",")))
                If app_face.ToString.Length = 1 Then app_face = 0 & app_face
                If app_skin.ToString.Length = 1 Then app_skin = 0 & app_skin
                If app_hairStyle.ToString.Length = 1 Then app_hairStyle = 0 & app_hairStyle
                If app_hairColor.ToString.Length = 1 Then app_hairColor = 0 & app_hairColor
                If app_featureVar.Length = 1 Then app_featureVar = "0" & app_featureVar 'todo //not used
                Dim byteStr As String = ((app_hairColor) & (app_hairStyle) & (app_face) & (app_skin)).ToString
                Player.PlayerBytes = TryInt((CLng("&H" & byteStr).ToString))
            Catch ex As Exception
                LogAppend("Exception occured: " & vbNewLine & ex.ToString(), "ArmoryHandler_LoadArmoryCharacters", False, True)
            End Try
            LogAppend("Loading character's finished quests", "ArmoryHandler_LoadArmoryCharacters", True)
            Player.FinishedQuests = splitString(Client.DownloadString(APILink & "?fields=quests") & ",", """quests"":[", "]}")
            Player.SetIndex = setId
            AddCharacterSet(setId, Player)
            Dim m_reputationParser As ReputationParser = New ReputationParser
            m_reputationParser.loadReputation(setId, APILink)
            Dim m_glyphParser As GlyphParser = New GlyphParser
            m_glyphParser.loadGlyphs(setId, APILink)
            Dim m_achievementParser As AchievementParser = New AchievementParser
            m_achievementParser.loadAchievements(setId, APILink)
            Dim m_itemParser As ItemParser = New ItemParser
            m_itemParser.loadItems(CharacterContext, setId)
            LogAppend("Character loaded!", "ArmoryHandler_LoadArmoryCharacters", True)
        Next
        LogAppend("All characters loaded!", "ArmoryHandler_LoadArmoryCharacters", True)

    End Sub
    
End Class
