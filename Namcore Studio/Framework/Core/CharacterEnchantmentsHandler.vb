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
'*      /Filename:      CharacterEnchantmentsHandler
'*      /Description:   Contains functions for extracting information about the enchantments 
'*                      of the equipped items of a specific character
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Imports Namcore_Studio.EventLogging
Imports Namcore_Studio.Basics
Imports Namcore_Studio.GlobalVariables
Imports Namcore_Studio.CommandHandler
Imports Namcore_Studio.Conversions
Imports Namcore_Studio.SpellItem_Information
Public Class CharacterEnchantmentsHandler
    Public Shared Sub HandleEnchantments(ByVal setId As Integer)
        LogAppend("Handling item enchantments for setId: " & setId, "CharacterEnchantmentsHandler_GetItemStats", True)
        Select Case sourceCore
            Case "arcemu"
                loadAtArcemu(setId)
            Case "trinity"
                loadAtTrinity(setId)
            Case "trinitytbc"
                loadAtTrinityTBC(setId)
            Case "mangos"
                loadAtMangos(setId)
            Case Else

        End Select

    End Sub
    Private Shared Sub loadAtArcemu(ByVal tar_setId As Integer)
        LogAppend("Handling item enchantments @loadAtArcemu", "CharacterEnchantmentsHandler_loadAtArcemu", False)
        Dim slotname(19) As String
        slotname = {"head", "neck", "shoulder", "back", "chest", "shirt", "tabard", "wrists", "main", "off", "distance", "hands", "waist", "legs", "feet", "finger1", "finger2", "trinket1", "trinket2"}
        Dim loopcounter As Integer = 0
        Do
            SetTemporaryCharacterInformation("@character_" & slotname(loopcounter) & "_enchant_text", ArcSplitEnchantString(GetTemporaryCharacterInformation("@character_" & slotname(loopcounter) & "_ench", tar_setId), "@character_" & slotname(loopcounter) & "_enchant_id", tar_setId), tar_setId)
            If Not loopcounter = 17 Or 18 Then
                SetTemporaryCharacterInformation("@character_" & slotname(loopcounter) & "_gem1_text", ArcSplitGemString(GetTemporaryCharacterInformation("@character_" & slotname(loopcounter) & "_ench", tar_setId), tar_setId, 29), tar_setId)
                SetTemporaryCharacterInformation("@character_" & slotname(loopcounter) & "_gem2_text", ArcSplitGemString(GetTemporaryCharacterInformation("@character_" & slotname(loopcounter) & "_ench", tar_setId), tar_setId, 32), tar_setId)
                SetTemporaryCharacterInformation("@character_" & slotname(loopcounter) & "_gem3_text", ArcSplitGemString(GetTemporaryCharacterInformation("@character_" & slotname(loopcounter) & "_ench", tar_setId), tar_setId, 35), tar_setId)
            End If
            If loopcounter = 13 Then
                Dim resultString As String = ArcSplitGemString(GetTemporaryCharacterInformation("@character_waist_ench", tar_setId), tar_setId, 38) = ""
                If Not resultString = "" Then SetTemporaryCharacterInformation("@character_beltbuckle", TryInt(resultString), tar_setId)
            End If
            loopcounter += 1
        Loop Until loopcounter = 18
        End Sub
    Private Shared Function ArcSplitEnchantString(ByVal input As String, ByVal obvalue As String, ByVal targetSetId As Integer) As String
        LogAppend("ArcSplitEnchantString call (input=" & input & " // obvalue=" & obvalue & ")", "CharacterEnchantmentsHandler_ArcSplitEnchantString", False)
        Try
            If input.Contains(";") Then
                Dim parts() As String = input.Split(";"c)
                If parts(0).Contains("0,0") Then
                    Dim parts2() As String = parts(0).Split(","c)
                    SetTemporaryCharacterInformation(obvalue, parts2(0), targetSetId)
                    Return GetEffectNameByEffectId(obvalue)
                ElseIf parts(1).Contains("0,0") Then
                    Dim parts2() As String = parts(1).Split(","c)
                    SetTemporaryCharacterInformation(obvalue, parts2(0), targetSetId)
                    Return GetEffectNameByEffectId(obvalue)
                ElseIf parts(2).Contains("0,0") Then
                    Dim parts2() As String = parts(2).Split(","c)
                    SetTemporaryCharacterInformation(obvalue, parts2(0), targetSetId)
                    Return GetEffectNameByEffectId(obvalue)
                ElseIf parts(3).Contains("0,0") Then
                    Dim parts2() As String = parts(3).Split(","c)
                    SetTemporaryCharacterInformation(obvalue, parts2(0), targetSetId)
                    Return GetEffectNameByEffectId(obvalue)
                ElseIf parts(4).Contains("0,0") Then
                    Dim parts2() As String = parts(4).Split(","c)
                    SetTemporaryCharacterInformation(obvalue, parts2(0), targetSetId)
                    Return GetEffectNameByEffectId(obvalue)
                ElseIf parts(5).Contains("0,0") Then
                    Dim parts2() As String = parts(5).Split(","c)
                    SetTemporaryCharacterInformation(obvalue, parts2(0), targetSetId)
                    Return GetEffectNameByEffectId(obvalue)
                Else
                    Return ""
                End If
            Else
                Return ""
            End If
        Catch ex As Exception
            LogAppend("Something went wrong while splitting enchantment string -> Returning nothing -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterEnchantmentsHandler_ArcSplitEnchantString", False, True)
            Return ""
        End Try
    End Function

    Private Shared Function ArcSplitGemString(ByVal input As String, ByVal targetSetId As Integer, ByVal position As Integer) As String
        LogAppend("ArcSplitGemString call (input=" & input & " // position=" & position.ToString() & ")", "CharacterEnchantmentsHandler_ArcSplitGemString", False)
        Dim obvalue As String
       Try
            Dim parts() As String = input.Split(";"c)
            Dim xvalue As String = ""
            If position = 23 Then
                xvalue = "0,1"
            ElseIf position = 29 Then
                xvalue = "0,2"
            ElseIf position = 32 Then
                xvalue = "0,3"
            ElseIf position = 35 Then
                xvalue = "0,4"
            ElseIf position = 38 Then
                xvalue = "0,6"
            Else
                xvalue = "0,1"
            End If
            If parts(0).Contains(xvalue) Then
                Dim parts2() As String = parts(0).Split(","c)
                obvalue = tryint(parts2(0))
                If xvalue = "0,6" Then Return parts2(0)
                Return GetEffectNameByEffectId(obvalue)
            ElseIf parts(1).Contains(xvalue) Then
                Dim parts2() As String = parts(1).Split(","c)
                obvalue = tryint(parts2(0))
                If xvalue = "0,6" Then Return parts2(0)
                Return GetEffectNameByEffectId(obvalue)
            ElseIf parts(2).Contains(xvalue) Then
                Dim parts2() As String = parts(2).Split(","c)
                obvalue = tryint(parts2(0))
                If xvalue = "0,6" Then Return parts2(0)
                Return GetEffectNameByEffectId(obvalue)
            ElseIf parts(3).Contains(xvalue) Then
                Dim parts2() As String = parts(3).Split(","c)
                obvalue = tryint(parts2(0))
                If xvalue = "0,6" Then Return parts2(0)
                Return GetEffectNameByEffectId(obvalue)
            ElseIf parts(4).Contains(xvalue) Then
                Dim parts2() As String = parts(4).Split(","c)
                obvalue = tryint(parts2(0))
                If xvalue = "0,6" Then Return parts2(0)
                Return GetEffectNameByEffectId(obvalue)
            ElseIf parts(5).Contains(xvalue) Then
                Dim parts2() As String = parts(5).Split(","c)
                obvalue = tryint(parts2(0))
                If xvalue = "0,6" Then Return parts2(0)
                Return GetEffectNameByEffectId(obvalue)
            Else
                Return ""
            End If
        Catch ex As Exception
            LogAppend("Something went wrong while splitting gem string -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterEnchantmentsHandler_ArcSplitGemString", False, True)
            Return ""
        End Try
    End Function
    Private Shared Sub loadAtTrinity(ByVal tar_setId As Integer)
        LogAppend("Handling item enchantments @loadAtTrinity", "CharacterEnchantmentsHandler_loadAtTrinity", False)
        Dim slotname(19) As String
        slotname = {"head", "neck", "shoulder", "back", "chest", "shirt", "tabard", "wrists", "main", "off", "distance", "hands", "waist", "legs", "feet", "finger1", "finger2", "trinket1", "trinket2"}
        Dim loopcounter As Integer = 0
        Do
            SetTemporaryCharacterInformation("@character_" & slotname(loopcounter) & "_enchant_text", TrinitySplitEnchantString(GetTemporaryCharacterInformation("@character_" & slotname(loopcounter) & "_ench", tar_setId), "@character_" & slotname(loopcounter) & "_enchant_id", tar_setId), tar_setId)
            If Not loopcounter = 17 Or 18 Then
                SetTemporaryCharacterInformation("@character_" & slotname(loopcounter) & "_gem1_text", TrinitySplitGemString(GetTemporaryCharacterInformation("@character_" & slotname(loopcounter) & "_ench", tar_setId), tar_setId, 6, "@character_" & slotname(loopcounter) & "_gem1_id"), tar_setId)
                SetTemporaryCharacterInformation("@character_" & slotname(loopcounter) & "_gem2_text", TrinitySplitGemString(GetTemporaryCharacterInformation("@character_" & slotname(loopcounter) & "_ench", tar_setId), tar_setId, 9, "@character_" & slotname(loopcounter) & "_gem2_id"), tar_setId)
                SetTemporaryCharacterInformation("@character_" & slotname(loopcounter) & "_gem3_text", TrinitySplitGemString(GetTemporaryCharacterInformation("@character_" & slotname(loopcounter) & "_ench", tar_setId), tar_setId, 12, "@character_" & slotname(loopcounter) & "_gem3_id"), tar_setId)
            End If
            If loopcounter = 13 Then
                '// TODO: Load belt buckle
            End If
            loopcounter += 1
        Loop Until loopcounter = 18
    End Sub
    Private Shared Function TrinitySplitEnchantString(ByVal input As String, ByRef obvalue As Integer, ByVal targetSetId As Integer) As String
        LogAppend("TrinitySplitEnchantString call (input=" & input & " // obvalue=" & obvalue & ")", "CharacterEnchantmentsHandler_TrinitySplitEnchantString", False)
        Try
            If input.Contains(" ") Then
                Dim parts() As String = input.Split(" "c)
                If Not parts(0) = "0" Then
                    SetTemporaryCharacterInformation(obvalue, parts(0), targetSetId)
                    Return GetEffectNameByEffectId(TryInt(parts(0)))
                Else
                    Return ""
                End If
            Else
                Return ""
            End If
        Catch ex As Exception
            LogAppend("Something went wrong while splitting enchantment string -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterEnchantmentsHandler_TrinitySplitEnchantString", False, True)
            Return ""
        End Try
    End Function

    Private Shared Function TrinitySplitGemString(ByVal input As String, ByVal targetSetId As Integer, ByVal position As Integer, ByVal obvalue As String) As String
        LogAppend("TrinitySplitGemString call (input=" & input & " // obvalue=" & obvalue & ")", "CharacterEnchantmentsHandler_TrinitySplitGemString", False)
        Try
            Dim parts() As String = input.Split(" "c)
            If Not parts(position) = "0" Then
                SetTemporaryCharacterInformation(obvalue, parts(0), targetSetId)
                Return GetEffectNameByEffectId(TryInt(parts(position)))
            Else
                Return ""
            End If
        Catch ex As Exception
            LogAppend("Something went wrong while splitting gem string -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterEnchantmentsHandler_TrinitySplitGemString", False, True)
            Return ""
        End Try
    End Function
    Private Shared Sub loadAtTrinityTBC(ByVal tar_setId As Integer)

    End Sub
    Private Shared Sub loadAtMangos(ByVal tar_setId As Integer)
        LogAppend("Handling item enchantments @loadAtMangos", "CharacterEnchantmentsHandler_loadAtMangos", False)
        Dim slotname(19) As String
        slotname = {"head", "neck", "shoulder", "back", "chest", "shirt", "tabard", "wrists", "main", "off", "distance", "hands", "waist", "legs", "feet", "finger1", "finger2", "trinket1", "trinket2"}
        Dim loopcounter As Integer = 0
        Do
            SetTemporaryCharacterInformation("@character_" & slotname(loopcounter) & "_enchant_text", MangosSplitEnchantString(GetTemporaryCharacterInformation("@character_" & slotname(loopcounter) & "_ench", tar_setId), "@character_" & slotname(loopcounter) & "_enchant_id", tar_setId), tar_setId)
            If Not loopcounter = 17 Or 18 Then
                SetTemporaryCharacterInformation("@character_" & slotname(loopcounter) & "_gem1_text", MangosSplitGemString(GetTemporaryCharacterInformation("@character_" & slotname(loopcounter) & "_ench", tar_setId), tar_setId, 29, "@character_" & slotname(loopcounter) & "_gem1_id"), tar_setId)
                SetTemporaryCharacterInformation("@character_" & slotname(loopcounter) & "_gem2_text", MangosSplitGemString(GetTemporaryCharacterInformation("@character_" & slotname(loopcounter) & "_ench", tar_setId), tar_setId, 32, "@character_" & slotname(loopcounter) & "_gem2_id"), tar_setId)
                SetTemporaryCharacterInformation("@character_" & slotname(loopcounter) & "_gem3_text", MangosSplitGemString(GetTemporaryCharacterInformation("@character_" & slotname(loopcounter) & "_ench", tar_setId), tar_setId, 35, "@character_" & slotname(loopcounter) & "_gem3_id"), tar_setId)
            End If
            If loopcounter = 13 Then
                '// TODO: Load belt buckle
            End If
            loopcounter += 1
        Loop Until loopcounter = 18
    End Sub
    Private Shared Function MangosSplitEnchantString(ByVal input As String, ByRef obvalue As Integer, ByVal targetSetId As Integer) As String
        LogAppend("MangosSplitEnchantString call (input=" & input & " // obvalue=" & obvalue & ")", "CharacterEnchantmentsHandler_MangosSplitEnchantString", False)
        Try
            If input.Contains(" ") Then
                Dim parts() As String = input.Split(" "c)
                If Not parts(22) = "0" Then
                    obvalue = CInt(parts(22))
                    Return GetEffectNameByEffectId(CInt(parts(22)))
                Else
                    Return ""
                End If
            Else
                Return ""
            End If
        Catch ex As Exception
            LogAppend("Something went wrong while splitting enchantment string -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterEnchantmentsHandler_MangosSplitEnchantString", False, True)
            Return ""
        End Try
    End Function

    Private Shared Function MangosSplitGemString(ByVal input As String, ByVal targetSetId As Integer, ByVal position As Integer, ByVal obvalue As String) As String
        LogAppend("MangosSplitGemString call (input=" & input & " // obvalue=" & obvalue & ")", "CharacterEnchantmentsHandler_MangosSplitGemString", False)
        Try
            Dim parts() As String = input.Split(" "c)
            If Not parts(position - 1) = "0" Then
                obvalue = CInt(parts(position - 1))
                Return GetEffectNameByEffectId(CInt(parts(position - 1)))
            Else
                Return ""
            End If
        Catch ex As Exception
            LogAppend("Something went wrong while splitting gem string -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterEnchantmentsHandler_MangosSplitGemString", False, True)
            Return ""
        End Try
    End Function
 End Class
