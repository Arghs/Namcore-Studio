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
'*      /Filename:      CharacterItemStatsHandler
'*      /Description:   Contains functions for extracting information about the enchantments 
'*                      of all items in the inventory of a specific character
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Imports Namcore_Studio.EventLogging
Imports Namcore_Studio.Basics
Imports Namcore_Studio.GlobalVariables
Imports Namcore_Studio.CommandHandler
Imports Namcore_Studio.Conversions
Public Class CharacterItemStatsHandler
    Public Shared Sub GetItemStats(ByVal CItemguid As Integer, ByVal saveVar As String, ByVal setId As Integer)
        LogAppend("Loading character ItemStats for item: " & CItemguid.ToString() & " and setId: " & setId, "CharacterItemStatssHandler_GetItemStats", True)
        Select Case sourceCore
            Case "arcemu"
                loadAtArcemu(CItemguid, setId, saveVar)
            Case "trinity"
                loadAtTrinity(CItemguid, setId, saveVar)
            Case "trinitytbc"
                loadAtTrinityTBC(CItemguid, setId, saveVar)
            Case "mangos"
                loadAtMangos(CItemguid, setId, saveVar)
            Case Else

        End Select

    End Sub
    Private Shared Sub loadAtArcemu(ByVal item As Integer, ByVal tar_setId As Integer, ByVal info_var As String)
        LogAppend("Loading ItemStats @loadAtArcemu", "CharacterItemStatsHandler_loadAtArcemu", False)
        Try
            SetTemporaryCharacterInformation("@character_" & info_var & "_ench", runSQLCommand_characters_string("SELECT " & sourceStructure.itmins_enchantments_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " & sourceStructure.itmins_guid_col(0) & "='" & item.ToString & "'"), tar_setId)
        Catch ex As Exception
            SetTemporaryCharacterInformation("@character_" & info_var & "_ench", "-1", tar_setId)
        End Try
        End Sub
    Private Shared Sub loadAtTrinity(ByVal item As Integer, ByVal tar_setId As Integer, ByVal info_var As String)
        LogAppend("Loading ItemStats @loadAtTrinity", "CharacterItemStatsHandler_loadAtTrinity", False)
        Try
            SetTemporaryCharacterInformation("@character_" & info_var & "_ench", runSQLCommand_characters_string("SELECT " & sourceStructure.itmins_enchantments_col(0) & " FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " & sourceStructure.itmins_guid_col(0) & "='" & item.ToString & "'"), tar_setId)
        Catch ex As Exception
            SetTemporaryCharacterInformation("@character_" & info_var & "_ench", "-1", tar_setId)
        End Try
       End Sub
    Private Shared Sub loadAtTrinityTBC(ByVal item As Integer, ByVal tar_setId As Integer, ByVal info_var As String)

    End Sub
    Private Shared Sub loadAtMangos(ByVal item As Integer, ByVal tar_setId As Integer, ByVal info_var As String)
        LogAppend("Loading character ItemStats @loadAtMangos", "CharacterItemStatsHandler_loadAtMangos", False)
        Try
            SetTemporaryCharacterInformation("@character_" & info_var & "_ench", runSQLCommand_characters_string("SELECT `" & sourceStructure.itmins_data_col(0) & "` FROM " & sourceStructure.item_instance_tbl(0) & " WHERE " & sourceStructure.itmins_guid_col(0) & "='" & item.ToString & "'"), tar_setId)
        Catch ex As Exception
            SetTemporaryCharacterInformation("@character_" & info_var & "_ench", "-1", tar_setId)
        End Try
    End Sub
End Class
