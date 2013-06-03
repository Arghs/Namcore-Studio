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
'*      /Filename:      Character
'*      /Description:   Character Object - character information class
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Public Class Character
    Public SourceCore As String
    Public SetIndex As Integer
    Public Guid As Integer
    Public Name As String
    Public Level As Integer
    Public Race As Integer
    Public Cclass As Integer
    Public Gender As Integer
    Public Xp As Integer
    Public Gold As String
    Public PlayerBytes As Integer
    Public PlayerBytes2 As Integer
    Public PlayerFlags As Integer
    Public PositionX As Integer
    Public PositionY As Integer
    Public PositionZ As Integer
    Public Map As Integer
    Public InstanceId As Integer
    Public InstanceModeMask As Integer
    Public Orientation As Integer
    Public Taximask As String
    Public Cinematic As Integer
    Public TotalTime As String
    Public LevelTime As Integer
    Public StableSlots As Integer
    Public Zone As Integer
    Public ArenaPoints As Integer
    Public TotalHonorPoints As Integer
    Public TotalKills As Integer
    Public ChosenTitle As Integer
    Public WatchedFaction As Integer
    Public Health As Integer
    Public SpecCount As Integer
    Public ActiveSpec As Integer
    Public ExploredZones As String
    Public KnownTitles As String
    Public ArcEmuTalentPoints As String
    Public FinishedQuests As String
    Public CustomFaction As Integer
    Public BindMapId As Integer
    Public BindZoneId As Integer
    Public BindPositionX As Integer
    Public BindPositionY As Integer
    Public BindPositionZ As Integer
    Public HomeBind As String
    Public ExtraFlags As Integer
    Public AtLogin As Integer
    Public KnownCurrencies As Integer
    Public ActionBars As Integer

    'Account
    Public AccountId As Integer
    Public AccountName As String
    Public ArcEmuPass As String
    Public PassHash As String
    Public ArcEmuFlags As Integer
    Public Locale As Integer
    Public ArcEmuGmLevel As String
    Public SessionKey As String
    Public JoinDate As Integer
    Public V As String
    Public S As String

    'Account Access
    Public GmLevel As Integer
    Public RealmId As Integer

    'Misc

    Public InventoryItems As List(Of Item)
    Public InventoryItemsIndex As String
    Public PlayerGlyphs As List(Of Glyph)
    Public PlayerGlyphsIndex As String
    Public Achievements As List(Of Achievement)
    Public Actions As List(Of Action)

    Public Sub New(charname As String, charguid As Integer)
        Name = charname
        Guid = charguid
    End Sub
End Class
