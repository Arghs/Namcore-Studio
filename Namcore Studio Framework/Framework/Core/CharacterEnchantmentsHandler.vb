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
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Module

Namespace Framework.Core
    Public Class CharacterEnchantmentsHandler
        Public Sub HandleEnchantments(ByVal setId As Integer)
            LogAppend("Handling item enchantments for setId: " & setId, "CharacterEnchantmentsHandler_GetItemStats", True)
            Select Case GlobalVariables.sourceCore
                Case "arcemu"
                    LoadAtArcemu(setId)
                Case "trinity"
                    LoadAtTrinity(setId)
                Case "trinitytbc"
                    'todo LoadAtTrinityTBC(setId)
                Case "mangos"
                    LoadAtMangos(setId)
            End Select
        End Sub

        Private Sub LoadAtArcemu(ByVal tarSetId As Integer)
            LogAppend("Handling item enchantments @LoadAtArcemu", "CharacterEnchantmentsHandler_LoadAtArcemu", False)
            Dim slotname(19) As String
            slotname =
                {"head", "neck", "shoulder", "back", "chest", "shirt", "tabard", "wrists", "main", "off", "distance",
                 "hands", "waist", "legs", "feet", "finger1", "finger2", "trinket1", "trinket2"}
            Dim loopcounter As Integer = 0
            Dim player As Character = GetCharacterSetBySetId(tarSetId)

            Do
                Dim itm As Item = GetCharacterArmorItem(player, slotname(loopcounter))
                If itm Is Nothing Then
                    loopcounter += 1
                    Continue Do
                End If
                itm.EnchantmentName = ArcSplitEnchantString(itm.enchstring, player, itm)
                If Not loopcounter = 17 Or Not loopcounter = 18 Then
                    itm.Socket1Name = ArcSplitGemString(itm.enchstring, 29)
                    itm.Socket2Name = ArcSplitGemString(itm.enchstring, 32)
                    itm.Socket3Name = ArcSplitGemString(itm.enchstring, 35)
                End If
                If loopcounter = 13 Then
                    Dim resultString As String = ArcSplitGemString(itm.enchstring, 38)
                    If Not resultString = "" Then player.BeltBuckle = TryInt(resultString)
                End If
                loopcounter += 1
                SetCharacterArmorItem(player, itm)
                SetCharacterSet(tarSetId, player)
            Loop Until loopcounter = 18
        End Sub

        Private Function ArcSplitEnchantString(ByVal input As String, ByVal player As Character, ByRef itm As Item) _
            As String
            LogAppend(
                "ArcSplitEnchantString call (input=" & input & " // character name=" & player.Name & " // itemslot= " &
                itm.Slotname & ")", "CharacterEnchantmentsHandler_ArcSplitEnchantString", False)
            Try
                If input.Contains(";") Then
                    Dim parts() As String = input.Split(";"c)
                    If parts(0).Contains("0,0") Then
                        Dim parts2() As String = parts(0).Split(","c)
                        itm.EnchantmentId = TryInt(parts2(0))
                        Return GetEffectNameByEffectId(parts2(0))
                    ElseIf parts(1).Contains("0,0") Then
                        Dim parts2() As String = parts(1).Split(","c)
                        itm.EnchantmentId = TryInt(parts2(0))
                        Return GetEffectNameByEffectId(parts2(0))
                    ElseIf parts(2).Contains("0,0") Then
                        Dim parts2() As String = parts(2).Split(","c)
                        itm.EnchantmentId = TryInt(parts2(0))
                        Return GetEffectNameByEffectId(parts2(0))
                    ElseIf parts(3).Contains("0,0") Then
                        Dim parts2() As String = parts(3).Split(","c)
                        itm.EnchantmentId = TryInt(parts2(0))
                        Return GetEffectNameByEffectId(parts2(0))
                    ElseIf parts(4).Contains("0,0") Then
                        Dim parts2() As String = parts(4).Split(","c)
                        itm.EnchantmentId = TryInt(parts2(0))
                        Return GetEffectNameByEffectId(parts2(0))
                    ElseIf parts(5).Contains("0,0") Then
                        Dim parts2() As String = parts(5).Split(","c)
                        itm.EnchantmentId = TryInt(parts2(0))
                        Return GetEffectNameByEffectId(parts2(0))
                    Else
                        Return ""
                    End If
                Else
                    Return ""
                End If
            Catch ex As Exception
                LogAppend(
                    "Something went wrong while splitting enchantment string -> Returning nothing -> Exception is: ###START###" &
                    ex.ToString() & "###END###", "CharacterEnchantmentsHandler_ArcSplitEnchantString", False, True)
                Return ""
            End Try
        End Function

        Private Function ArcSplitGemString(ByVal input As String, ByVal position As Integer) _
            As String
            LogAppend("ArcSplitGemString call (input=" & input & " // position=" & position.ToString() & ")",
                      "CharacterEnchantmentsHandler_ArcSplitGemString", False)
            Dim obvalue As Integer
            Try
                Dim parts() As String = input.Split(";"c)
                Dim xvalue As String
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
                    obvalue = TryInt(parts2(0))
                    If xvalue = "0,6" Then Return parts2(0)
                    Return GetEffectNameByEffectId(obvalue)
                ElseIf parts(1).Contains(xvalue) Then
                    Dim parts2() As String = parts(1).Split(","c)
                    obvalue = TryInt(parts2(0))
                    If xvalue = "0,6" Then Return parts2(0)
                    Return GetEffectNameByEffectId(obvalue)
                ElseIf parts(2).Contains(xvalue) Then
                    Dim parts2() As String = parts(2).Split(","c)
                    obvalue = TryInt(parts2(0))
                    If xvalue = "0,6" Then Return parts2(0)
                    Return GetEffectNameByEffectId(obvalue)
                ElseIf parts(3).Contains(xvalue) Then
                    Dim parts2() As String = parts(3).Split(","c)
                    obvalue = TryInt(parts2(0))
                    If xvalue = "0,6" Then Return parts2(0)
                    Return GetEffectNameByEffectId(obvalue)
                ElseIf parts(4).Contains(xvalue) Then
                    Dim parts2() As String = parts(4).Split(","c)
                    obvalue = TryInt(parts2(0))
                    If xvalue = "0,6" Then Return parts2(0)
                    Return GetEffectNameByEffectId(obvalue)
                ElseIf parts(5).Contains(xvalue) Then
                    Dim parts2() As String = parts(5).Split(","c)
                    obvalue = TryInt(parts2(0))
                    If xvalue = "0,6" Then Return parts2(0)
                    Return GetEffectNameByEffectId(obvalue)
                Else
                    Return ""
                End If
            Catch ex As Exception
                LogAppend(
                    "Something went wrong while splitting gem string -> Exception is: ###START###" & ex.ToString() &
                    "###END###", "CharacterEnchantmentsHandler_ArcSplitGemString", False, True)
                Return ""
            End Try
        End Function

        Private Sub LoadAtTrinity(ByVal tarSetId As Integer)
            LogAppend("Handling item enchantments @LoadAtTrinity", "CharacterEnchantmentsHandler_LoadAtTrinity", False)
            Dim slotname(19) As String
            slotname =
                {"head", "neck", "shoulder", "back", "chest", "shirt", "tabard", "wrists", "main", "off", "distance",
                 "hands", "waist", "legs", "feet", "finger1", "finger2", "trinket1", "trinket2"}
            Dim loopcounter As Integer = 0
            Dim player As Character = GetCharacterSetBySetId(tarSetId)
            Do
                Dim itm As Item
                itm = GetCharacterArmorItem(player, slotname(loopcounter))
                If itm Is Nothing Then
                    loopcounter += 1
                    Continue Do
                End If
                itm.EnchantmentName = TrinitySplitEnchantString(itm.enchstring, itm)
                If Not loopcounter = 17 Or Not loopcounter = 18 Then
                    itm.Socket1Name = TrinitySplitGemString(itm.enchstring, 6, itm, 1)
                    itm.Socket2Name = TrinitySplitGemString(itm.enchstring, 9, itm, 2)
                    itm.Socket3Name = TrinitySplitGemString(itm.enchstring, 12, itm, 3)
                End If
                If loopcounter = 13 Then
                    '// TODO: Load belt buckle
                End If
                loopcounter += 1
                SetCharacterArmorItem(player, itm)
                SetCharacterSet(tarSetId, player)
            Loop Until loopcounter = 18
        End Sub

        Private Function TrinitySplitEnchantString(ByVal input As String, ByRef itm As Item) _
            As String
            LogAppend("TrinitySplitEnchantString call (input=" & input & " // itemslot=" & itm.Slotname & ")",
                      "CharacterEnchantmentsHandler_TrinitySplitEnchantString", False)
            Try
                If input.Contains(" ") Then
                    Dim parts() As String = input.Split(" "c)
                    If Not parts(0) = "0" Then
                        itm.EnchantmentId = TryInt(parts(0))
                        Return GetEffectNameByEffectId(TryInt(parts(0)))
                    Else
                        Return ""
                    End If
                Else
                    Return ""
                End If
            Catch ex As Exception
                LogAppend(
                    "Something went wrong while splitting enchantment string -> Exception is: ###START###" & ex.ToString() &
                    "###END###", "CharacterEnchantmentsHandler_TrinitySplitEnchantString", False, True)
                Return ""
            End Try
        End Function

        Private Function TrinitySplitGemString(ByVal input As String,
                                               ByVal position As Integer, ByRef itm As Item, ByVal gemnum As Integer) _
            As String
            LogAppend("TrinitySplitGemString call (input=" & input & " // itemslot=" & itm.Slotname & ")",
                      "CharacterEnchantmentsHandler_TrinitySplitGemString", False)
            Try
                Dim parts() As String = input.Split(" "c)
                If Not parts(position) = "0" Then
                    Select Case gemnum
                        Case 1 : itm.Socket1Id = TryInt(parts(0))
                        Case 2 : itm.Socket2Id = TryInt(parts(0))
                        Case 3 : itm.Socket3Id = TryInt(parts(0))
                    End Select
                    Return GetEffectNameByEffectId(TryInt(parts(position)))
                Else
                    Return ""
                End If
            Catch ex As Exception
                LogAppend(
                    "Something went wrong while splitting gem string -> Exception is: ###START###" & ex.ToString() &
                    "###END###", "CharacterEnchantmentsHandler_TrinitySplitGemString", False, True)
                Return ""
            End Try
        End Function

        Private Sub LoadAtMangos(ByVal tarSetId As Integer)
            LogAppend("Handling item enchantments @LoadAtMangos", "CharacterEnchantmentsHandler_LoadAtMangos", False)
            Dim slotname(19) As String
            slotname =
                {"head", "neck", "shoulder", "back", "chest", "shirt", "tabard", "wrists", "main", "off", "distance",
                 "hands", "waist", "legs", "feet", "finger1", "finger2", "trinket1", "trinket2"}
            Dim loopcounter As Integer = 0
            Dim player As Character = GetCharacterSetBySetId(tarSetId)
            Do
                Dim itm As Item = GetCharacterArmorItem(player, slotname(loopcounter))
                If itm Is Nothing Then
                    loopcounter += 1
                    Continue Do
                End If
                itm.EnchantmentName = MangosSplitEnchantString(itm.enchstring, itm)
                If Not loopcounter = 17 Or Not loopcounter = 18 Then
                    itm.Socket1Name = MangosSplitGemString(itm.enchstring, 29, itm, 1)
                    itm.Socket2Name = MangosSplitGemString(itm.enchstring, 32, itm, 2)
                    itm.Socket3Name = MangosSplitGemString(itm.enchstring, 35, itm, 3)
                End If
                If loopcounter = 13 Then
                    '// TODO: Load belt buckle
                End If
                loopcounter += 1
                SetCharacterArmorItem(player, itm)
                SetCharacterSet(tarSetId, player)
            Loop Until loopcounter = 18
        End Sub

        Private Function MangosSplitEnchantString(ByVal input As String, ByRef itm As Item) _
            As String
            LogAppend("MangosSplitEnchantString call (input=" & input & " // itemslot=" & itm.Slotname & ")",
                      "CharacterEnchantmentsHandler_MangosSplitEnchantString", False)
            Try
                If input.Contains(" ") Then
                    Dim parts() As String = input.Split(" "c)
                    If Not parts(22) = "0" Then
                        itm.EnchantmentId = CInt(parts(22))
                        Return GetEffectNameByEffectId(CInt(parts(22)))
                    Else
                        Return ""
                    End If
                Else
                    Return ""
                End If
            Catch ex As Exception
                LogAppend(
                    "Something went wrong while splitting enchantment string -> Exception is: ###START###" & ex.ToString() &
                    "###END###", "CharacterEnchantmentsHandler_MangosSplitEnchantString", False, True)
                Return ""
            End Try
        End Function

        Private Function MangosSplitGemString(ByVal input As String, ByVal position As Integer,
                                              ByRef itm As Item, ByVal gemnum As Integer) As String
            LogAppend("MangosSplitGemString call (input=" & input & " // slotname=" & itm.Slotname & ")",
                      "CharacterEnchantmentsHandler_MangosSplitGemString", False)
            Try
                Dim parts() As String = input.Split(" "c)
                If Not parts(position - 1) = "0" Then
                    Select Case gemnum
                        Case 1 : itm.Socket1Id = CInt(parts(position - 1))
                        Case 2 : itm.Socket2Id = CInt(parts(position - 1))
                        Case 3 : itm.Socket3Id = CInt(parts(position - 1))
                    End Select
                    Return GetEffectNameByEffectId(CInt(parts(position - 1)))
                Else
                    Return ""
                End If
            Catch ex As Exception
                LogAppend(
                    "Something went wrong while splitting gem string -> Exception is: ###START###" & ex.ToString() &
                    "###END###", "CharacterEnchantmentsHandler_MangosSplitGemString", False, True)
                Return ""
            End Try
        End Function
    End Class
End Namespace