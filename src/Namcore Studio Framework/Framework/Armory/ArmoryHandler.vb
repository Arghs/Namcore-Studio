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
'*      /Filename:      ArmoryHandler
'*      /Description:   Contains functions for parsing character information from wow armory
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.Threading
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Modules
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Extension
Imports System.Net
Imports System.Text
Imports NCFramework.Framework.Armory.Parser

Namespace Framework.Armory
    Public Class ArmoryHandler
        '// Declaration
        Private ReadOnly _context As SynchronizationContext = SynchronizationContext.Current
        Public Event Completed As EventHandler(Of CompletedEventArgs)
        '// Declaration

        Protected Overridable Sub OnCompleted(ByVal e As CompletedEventArgs)
            RaiseEvent Completed(Me, e)
        End Sub

        Public Sub LoadArmoryCharacters(ByVal urllst As List(Of String))
            ThreadExtensions.QueueUserWorkItem(New Func(Of List(Of String), String)(AddressOf DoLoad), urllst)
        End Sub

        Public Function DoLoad(ByVal linkList As List(Of String)) As String
            LogAppend("Loading characters from Armory (" & LinkList.Count.ToString() & " character/s)",
                      "ArmoryHandler_DoLoad", True)
            Dim setId As Integer = 0
            Dim characterContext As String
            Dim apiLink As String
            Dim characterName As String
            Dim client As New WebClient
            client.CheckProxy()
            Dim armoryAccount As New Account() With {.Name = "Armory", .Id = 0}
            armoryAccount.Characters = New List(Of Character)()
            armoryAccount.SetIndex = 0
            armoryAccount.SourceExpansion = 5
            armoryAccount.Core = "armory"
            AddAccountSet(0, armoryAccount)
            For Each armoryLink As String In linkList
                Try
                    LogAppend("URL is " & armoryLink, "ArmoryHandler_DoLoad", False)
                    characterContext = client.DownloadString(armoryLink)
                    Dim b() As Byte = Encoding.Default.GetBytes(characterContext)
                    characterContext = Encoding.UTF8.GetString(b)
                Catch ex As Exception
                    LogAppend("Failed to load character context. Exception is: " & vbNewLine & ex.ToString(),
                              "ArmoryHandler_DoLoad", True, True)
                    Continue For
                End Try
                Try
                    Dim realm As String = SplitString(armoryLink, "/character/", "/")
                    characterContext = characterContext.Replace("&#39;", "")
                    characterName = SplitString(armoryLink, "/" & realm & "/", "/")
                    apiLink = "http://" & SplitString(armoryLink, "http://", ".battle") &
                              ".battle.net/api/wow/character/" &
                              SplitString(armoryLink, "/character/", "/") & "/" & characterName
                    Dim apiContext As String = client.DownloadString(apiLink)
                    setId += 1
                    LogAppend("Loading character " & characterName & " //ident is " & setId.ToString(),
                              "ArmoryHandler_DoLoad", True)
                    LogAppend("Loading basic character information", "ArmoryHandler_DoLoad", True)
                    Dim player As New Character()
                    player.Name = characterName
                    player.Guid = 0
                    player.AccountId = 0
                    player.AccountName = "Armory"
                    player.Level = TryInt(SplitString(apiContext, """level"":", ","))
                    player.Gender = TryInt(SplitString(apiContext, """gender"":", ","))
                    player.Race = TryInt(SplitString(apiContext, """race"":", ","))
                    player.Cclass = TryInt(SplitString(apiContext, """class"":", ","))
                    player.SourceCore = "armory"
                    player.SourceExpansion = 5
                    player.LoadedDateTime = DateTime.Now
                    '// Character appearance
                    Try
                        LogAppend("Loading character appearance information", "ArmoryHandler_DoLoad", True)
                        Dim appearanceContext As String = client.DownloadString(apiLink & "?fields=appearance")
                        Dim appFace As String = Hex$(Long.Parse(SplitString(appearanceContext, """faceVariation"":", ",")))
                        Dim appSkin As String = Hex$(Long.Parse(SplitString(appearanceContext, """skinColor"":", ",")))
                        Dim appHairStyle As String = Hex$(Long.Parse(SplitString(appearanceContext, """hairVariation"":",
                                                                                 ",")))
                        Dim appHairColor As String = Hex$(Long.Parse(SplitString(appearanceContext, """hairColor"":",
                                                                                 ",")))
                        Dim appFeatureVar As String = Hex$(Long.Parse(SplitString(appearanceContext,
                                                                                  """featureVariation"":", ",")))
                        If appFace.ToString.Length = 1 Then appFace = 0 & appFace
                        If appSkin.ToString.Length = 1 Then appSkin = 0 & appSkin
                        If appHairStyle.ToString.Length = 1 Then appHairStyle = 0 & appHairStyle
                        If appHairColor.ToString.Length = 1 Then appHairColor = 0 & appHairColor
                        ' ReSharper disable RedundantAssignment
                        If appFeatureVar.Length = 1 Then appFeatureVar = "0" & appFeatureVar 'todo //not used
                        ' ReSharper restore RedundantAssignment
                        Dim byteStr As String = ((appHairColor) & (appHairStyle) & (appFace) & (appSkin)).ToString
                        player.PlayerBytes = TryInt((CLng("&H" & byteStr).ToString))
                    Catch ex As Exception
                        LogAppend("Exception occured: " & vbNewLine & ex.ToString(), "ArmoryHandler_DoLoad", False, True)
                    End Try
                    LogAppend("Loading character's finished quests", "ArmoryHandler_DoLoad", True)
                    player.FinishedQuests = SplitString(client.DownloadString(apiLink & "?fields=quests") & ",",
                                                        """quests"":[", "]}")
                    player.InventoryZeroItems = New List(Of Item)()
                    player.InventoryZeroItems.Add(New Item _
                                                     With {.Id = 6948, .Count = 1, .Bag = 0, .Container = 0, .Slot = 23,
                                                     .Guid = 0}) '// Adding hearthstone
                    player.SetIndex = setId
                    AddCharacterSet(setId, player, armoryAccount)
                    Dim mReputationParser As ReputationParser = New ReputationParser
                    mReputationParser.LoadReputation(setId, apiLink, armoryAccount)
                    Dim mGlyphParser As GlyphParser = New GlyphParser
                    mGlyphParser.LoadGlyphs(setId, apiLink, armoryAccount)
                    Dim mAchievementParser As AchievementParser = New AchievementParser
                    mAchievementParser.LoadAchievements(setId, apiLink, armoryAccount)
                    Dim mProfessionParser As ProfessionParser = New ProfessionParser
                    mProfessionParser.LoadProfessions(setId, apiLink, armoryAccount)
                    Dim mItemParser As ItemParser = New ItemParser
                    mItemParser.LoadItems(characterContext, setId, armoryAccount)
                    SetCharacterSet(setId, player, armoryAccount)
                    SetAccountSet(armoryAccount.SetIndex, armoryAccount)
                    LogAppend("Character loaded!", "ArmoryHandler_DoLoad", True)
                Catch ex As Exception
                    LogAppend("Exception during character loading!: " & ex.ToString(), "ArmoryHandler_DoLoad", True,
                              True)
                End Try
            Next
            LogAppend("All characters loaded!", "ArmoryHandler_DoLoad", True)
            ThreadExtensions.ScSend(_context, New Action(Of CompletedEventArgs)(AddressOf OnCompleted),
                                    New CompletedEventArgs())
            ' ReSharper disable VBWarnings::BC42105
        End Function
        ' ReSharper restore VBWarnings::BC42105
    End Class
End Namespace