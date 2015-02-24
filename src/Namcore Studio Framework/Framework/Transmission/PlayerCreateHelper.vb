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
'*      /Filename:      PlayerCreateHelper
'*      /Description:   Provides functions to load correct spells/items for character 
'*                      races/classes
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Modules
Imports libnc.Provider

Namespace Framework.Transmission
    '// Declaration
    <Flags>
    Public Enum ChrRaces
        PLAYER_RACE_ALL = 0
        PLAYER_RACE_HUMAN = 1
        PLAYER_RACE_ORC = 2
        PLAYER_RACE_DWARF = 4
        PLAYER_RACE_NIGHT_ELF = 8
        PLAYER_RACE_UNDEAD = 16
        PLAYER_RACE_TAUREN = 32
        PLAYER_RACE_GNOME = 64
        PLAYER_RACE_TROLL = 128
        PLAYER_RACE_GOBLIN = 256
        PLAYER_RACE_BLOOD_ELF = 512
        PLAYER_RACE_DRAENEI = 1024
        PLAYER_RACE_FEL_ORC = 2048
        PLAYER_RACE_NAGA = 4096
        PLAYER_RACE_BROKEN = 8192
        PLAYER_RACE_SKELETON = 16384
        PLAYER_RACE_VRYKUL = 32768
        PLAYER_RACE_TUSKARR = 65536
        PLAYER_RACE_FOREST_TROLL = 131072
        PLAYER_RACE_TAUNKA = 262144
        PLAYER_RACE_NORTHREND_SKELETON = 524288
        PLAYER_RACE_ICE_TROLL = 1048576
        PLAYER_RACE_WORGEN = 2097152
    End Enum

    Public Enum ChrRaceIds
        PLAYER_RACE_ALL = 0
        PLAYER_RACE_HUMAN = 1
        PLAYER_RACE_ORC = 2
        PLAYER_RACE_DWARF = 3
        PLAYER_RACE_NIGHT_ELF = 4
        PLAYER_RACE_UNDEAD = 5
        PLAYER_RACE_TAUREN = 6
        PLAYER_RACE_GNOME = 7
        PLAYER_RACE_TROLL = 8
        PLAYER_RACE_GOBLIN = 9
        PLAYER_RACE_BLOOD_ELF = 10
        PLAYER_RACE_DRAENEI = 11
        PLAYER_RACE_FEL_ORC = 12
        PLAYER_RACE_NAGA = 13
        PLAYER_RACE_BROKEN = 14
        PLAYER_RACE_SKELETON = 15
        PLAYER_RACE_VRYKUL = 16
        PLAYER_RACE_TUSKARR = 17
        PLAYER_RACE_FOREST_TROLL = 18
        PLAYER_RACE_TAUNKA = 19
        PLAYER_RACE_NORTHREND_SKELETON = 20
        PLAYER_RACE_ICE_TROLL = 21
        PLAYER_RACE_WORGEN = 22
    End Enum

    <Flags>
    Public Enum ChrClasses
        PLAYER_CLASS_ALL = 0
        PLAYER_CLASS_WARRIOR = 1
        PLAYER_CLASS_PALADIN = 2
        PLAYER_CLASS_HUNTER = 4
        PLAYER_CLASS_ROGUE = 8
        PLAYER_CLASS_PRIEST = 16
        PLAYER_CLASS_DEATH_KNIGHT = 32
        PLAYER_CLASS_SHAMAN = 64
        PLAYER_CLASS_MAGE = 128
        PLAYER_CLASS_WARLOCK = 256
        PLAYER_CLASS_DRUID = 1024
    End Enum

    Public Enum ChrClassIds
        PLAYER_CLASS_ALL = 0
        PLAYER_CLASS_WARRIOR = 1
        PLAYER_CLASS_PALADIN = 2
        PLAYER_CLASS_HUNTER = 3
        PLAYER_CLASS_ROGUE = 4
        PLAYER_CLASS_PRIEST = 5
        PLAYER_CLASS_DEATH_KNIGHT = 6
        PLAYER_CLASS_SHAMAN = 7
        PLAYER_CLASS_MAGE = 8
        PLAYER_CLASS_WARLOCK = 9
        PLAYER_CLASS_DRUID = 11
    End Enum
    '// Declaration
    Public Module PlayerCreateHelper
        Public Sub GetRaceSpells(ByRef player As Character)
            LogAppend("Loading race specific spells for player " & player.Name, "PlayerCreateHelper_GetRaceSpells")
            Try
                Dim thisRace As ChrRaceIds = CType(player.Race, ChrRaceIds)
                Dim thisRaceBit As ChrRaces = CType(thisRace, ChrRaces)
                Dim newSpellList As New List(Of Spell)
                Dim spellsDt As DataTable = GetCreateInfoTable()
                For Each spellEntry As DataRow In spellsDt.Rows
                    Try
                        If spellEntry.Table.Columns.Count < 2 Then Continue For
                        For i = 0 To spellEntry.Table.Columns.Count - 1
                            If IsDBNull(spellEntry(i)) Then Continue For
                        Next
                        If TryInt(CStr(spellEntry(0))) = 0 Then
                            '// 0: Every race
                            LogAppend("Adding race specific spell: " & spellEntry(3).ToString(),
                                      "PlayerCreateHelper_GetRaceSpells")
                            newSpellList.Add(
                                New Spell With
                                                {.Active = 1, .Disabled = 0, .Id = TryInt(spellEntry(2).ToString()),
                                                .Name = spellEntry(3).ToString()})
                            Continue For
                        End If
                        Dim raceMask As ChrRaces = CType(TryInt(spellEntry(0).ToString()), ChrRaces)
                        If (raceMask And thisRaceBit) = thisRaceBit Then
                            LogAppend("Adding race specific spell: " & spellEntry(3).ToString(),
                                      "PlayerCreateHelper_GetRaceSpells")
                            newSpellList.Add(
                                New Spell With
                                                {.Active = 1, .Disabled = 0, .Id = TryInt(spellEntry(2).ToString()),
                                                .Name = spellEntry(3).ToString()})
                        End If
                    Catch ex As Exception
                        LogAppend("Exception occured " & ex.ToString(), "PlayerCreateHelper_GetRaceSpells", False, True)
                    End Try
                Next
                If player.Spells Is Nothing Then player.Spells = New List(Of Spell)()
                player.Spells.AddRange(newSpellList)
            Catch ex As Exception
                LogAppend("Exception occured " & ex.ToString(), "PlayerCreateHelper_GetRaceSpells", False, True)
            End Try
        End Sub

        Public Sub GetClassSpells(ByRef player As Character)
            LogAppend("Loading class specific spells for player " & player.Name, "PlayerCreateHelper_GetClassSpells")
            Try
                Dim thisClass As ChrClassIds = CType(player.Cclass, ChrClassIds)
                Dim thisClassBit As ChrClasses = CType(thisClass, ChrClasses)
                Dim newSpellList As New List(Of Spell)
                Dim spellsDt As DataTable = GetCreateInfoTable()
                For Each spellEntry As DataRow In spellsDt.Rows
                    Try
                        If spellEntry.Table.Columns.Count < 2 Then Continue For
                        For i = 0 To spellEntry.Table.Columns.Count - 1
                            If IsDBNull(spellEntry(i)) Then Continue For
                        Next
                        If TryInt(spellEntry(1).ToString()) = 0 Then
                            LogAppend("Adding class specific spell: " & spellEntry(3).ToString(),
                                      "PlayerCreateHelper_GetRaceSpells")
                            newSpellList.Add(
                                New Spell With
                                                {.Active = 1, .Disabled = 0, .Id = TryInt(spellEntry(2).ToString()),
                                                .Name = spellEntry(3).ToString()})
                            Continue For
                        End If
                        Dim classMask As ChrClasses = CType(TryInt(spellEntry(1).ToString()), ChrClasses)
                        If (classMask And thisClassBit) = thisClassBit Then
                            LogAppend("Adding class specific spell: " & spellEntry(3).ToString(),
                                      "PlayerCreateHelper_GetRaceSpells")
                            newSpellList.Add(
                                New Spell With
                                                {.Active = 1, .Disabled = 0, .Id = TryInt(spellEntry(2).ToString()),
                                                .Name = spellEntry(3).ToString()})
                        End If
                    Catch ex As Exception
                        LogAppend("Exception occured " & ex.ToString(), "PlayerCreateHelper_GetClassSpells", False, True)
                    End Try
                Next
                If player.Spells Is Nothing Then player.Spells = New List(Of Spell)()
                player.Spells.AddRange(newSpellList)
            Catch ex As Exception
                LogAppend("Exception occured " & ex.ToString(), "PlayerCreateHelper_GetClassSpells", False, True)
            End Try
        End Sub
    End Module
End Namespace