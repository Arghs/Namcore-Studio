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
'*      /Filename:      ModBasics
'*      /Description:   Includes functions for modifying basic character information
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Imports Namcore_Studio.EventLogging
Imports Namcore_Studio.CommandHandler
Imports Namcore_Studio.GlobalVariables
Public Class ModBasics
    Public Shared Sub SetCharacterGender(ByVal gender As Integer, ByVal setId As Integer, Optional charguid As Integer = 0)
        If charguid = 0 Then charguid = characterGUID
        LogAppend("Setting character gender to : " & gender.ToString() & " // charguid is : " & charguid.ToString(), "ModBasics_SetCharacterGender", True)
        Select Case sourceCore
            Case "arcemu"
                runSQLCommand_characters_string("UPDATE `characters` SET gender='" & gender.ToString() & "' WHERE guid='" & charguid.ToString() & "'", True)
            Case "trinity"
                runSQLCommand_characters_string("UPDATE `characters` SET gender='" & gender.ToString() & "' WHERE guid='" & charguid.ToString() & "'", True)
            Case "trinitytbc"

            Case "mangos"
                runSQLCommand_characters_string("UPDATE `characters` SET gender='" & gender.ToString() & "' WHERE guid='" & charguid.ToString() & "'", True)
            Case Else

        End Select
    End Sub
    Public Shared Sub SetCharacterRace(ByVal race As Integer, ByVal setId As Integer, Optional charguid As Integer = 0)
        If charguid = 0 Then charguid = characterGUID
        LogAppend("Setting character race to : " & race.ToString() & " // charguid is : " & charguid.ToString(), "ModBasics_SetCharacterRace", True)
        Select Case sourceCore
            Case "arcemu"
                runSQLCommand_characters_string("UPDATE `characters` SET race='" & race.ToString() & "' WHERE guid='" & charguid.ToString() & "'", True)
            Case "trinity"
                runSQLCommand_characters_string("UPDATE `characters` SET race='" & race.ToString() & "' WHERE guid='" & charguid.ToString() & "'", True)
            Case "trinitytbc"

            Case "mangos"
                runSQLCommand_characters_string("UPDATE `characters` SET race='" & race.ToString() & "' WHERE guid='" & charguid.ToString() & "'", True)
            Case Else

        End Select
    End Sub
    Public Shared Sub SetCharacterLevel(ByVal level As Integer, ByVal setId As Integer, Optional charguid As Integer = 0)
        If charguid = 0 Then charguid = characterGUID
        LogAppend("Setting character level to : " & level.ToString() & " // charguid is : " & charguid.ToString(), "ModBasics_SetCharacterLevel", True)
        Select Case sourceCore
            Case "arcemu"
                runSQLCommand_characters_string("UPDATE `characters` SET level='" & level.ToString() & "' WHERE guid='" & charguid.ToString() & "'", True)
            Case "trinity"
                runSQLCommand_characters_string("UPDATE `characters` SET level='" & level.ToString() & "' WHERE guid='" & charguid.ToString() & "'", True)
            Case "trinitytbc"

            Case "mangos"
                runSQLCommand_characters_string("UPDATE `characters` SET level='" & level.ToString() & "' WHERE guid='" & charguid.ToString() & "'", True)
            Case Else

        End Select
    End Sub
    Public Shared Sub SetCharacterClass(ByVal Cclass As Integer, ByVal setId As Integer, Optional charguid As Integer = 0)
        If charguid = 0 Then charguid = characterGUID
        LogAppend("Setting character gender to : " & Cclass.ToString() & " // charguid is : " & charguid.ToString(), "ModBasics_SetCharacterClass", True)
        Select Case sourceCore
            Case "arcemu"
                runSQLCommand_characters_string("UPDATE `characters` SET class='" & Cclass.ToString() & "' WHERE guid='" & charguid.ToString() & "'", True)
            Case "trinity"
                runSQLCommand_characters_string("UPDATE `characters` SET class='" & Cclass.ToString() & "' WHERE guid='" & charguid.ToString() & "'", True)
            Case "trinitytbc"

            Case "mangos"
                runSQLCommand_characters_string("UPDATE `characters` SET class='" & Cclass.ToString() & "' WHERE guid='" & charguid.ToString() & "'", True)
            Case Else

        End Select
    End Sub
    Public Shared Sub SetCharacterGold(ByVal gold As Integer, ByVal setId As Integer, Optional charguid As Integer = 0)
        If charguid = 0 Then charguid = characterGUID
        LogAppend("Setting character gold to : " & gold.ToString() & " // charguid is : " & charguid.ToString(), "ModBasics_SetCharacterGold", True)
        Select Case sourceCore
            Case "arcemu"
                runSQLCommand_characters_string("UPDATE `characters` SET gold='" & gold.ToString() & "' WHERE guid='" & charguid.ToString() & "'", True)
            Case "trinity"
                runSQLCommand_characters_string("UPDATE `characters` SET money='" & gold.ToString() & "' WHERE guid='" & charguid.ToString() & "'", True)
            Case "trinitytbc"

            Case "mangos"
                runSQLCommand_characters_string("UPDATE `characters` SET money='" & gold.ToString() & "' WHERE guid='" & charguid.ToString() & "'", True)
            Case Else

        End Select
    End Sub
End Class
