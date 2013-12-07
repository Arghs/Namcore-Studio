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
Imports NCFramework.Framework.Modules
Imports libnc.Provider
Namespace Framework.Core
    Public Class CharacterEnchantmentsHandler
        Public Sub HandleEnchantments(ByVal setId As Integer, ByVal account As Account)
            LogAppend("Handling item enchantments for setId: " & setId, "CharacterEnchantmentsHandler_GetItemStats", True)
            Select Case GlobalVariables.sourceCore
                Case "arcemu"
                    LoadAtArcemu(setId, account)
                Case "trinity"
                    LoadAtTrinity(setId, account)
                Case "trinitytbc"
                    'todo LoadAtTrinityTBC(setId)
                Case "mangos"
                    LoadAtMangos(setId, account)
            End Select
        End Sub

        Private Sub LoadAtArcemu(ByVal tarSetId As Integer, ByVal account As Account)
            LogAppend("Handling item enchantments @LoadAtArcemu", "CharacterEnchantmentsHandler_LoadAtArcemu", False)
            Dim slotname(19) As String
            slotname =
                {"head", "neck", "shoulder", "back", "chest", "shirt", "tabard", "wrists", "main", "off", "distance",
                 "hands", "waist", "legs", "feet", "finger1", "finger2", "trinket1", "trinket2"}
            Dim loopcounter As Integer = 0
            Dim player As Character = GetCharacterSetBySetId(tarSetId, account)

            Do
                Dim itm As Item = GetCharacterArmorItem(player, slotname(loopcounter))
                If itm Is Nothing Then
                    loopcounter += 1
                    Continue Do
                End If
                itm.EnchantmentName = ArcSplitEnchantString(itm.Enchstring, player, itm)
                If Not loopcounter = 17 Or Not loopcounter = 18 Then
                    itm.Socket1Effectid = ArcSplitGemString(itm.Enchstring, 29)
                    itm.Socket1Id = GetGemIdByEffectId(itm.Socket1Effectid)
                    itm.Socket1Name = GetEffectNameById(itm.Socket1Effectid, My.Settings.language)
                    itm.Socket1Pic = GetItemIconById(itm.Socket1Id, GlobalVariables.GlobalWebClient)
                    itm.Socket2Effectid = ArcSplitGemString(itm.Enchstring, 32)
                    itm.Socket2Id = GetGemIdByEffectId(itm.Socket2Effectid)
                    itm.Socket2Name = GetEffectNameById(itm.Socket2Effectid, My.Settings.language)
                    itm.Socket2Pic = GetItemIconById(itm.Socket2Id, GlobalVariables.GlobalWebClient)
                    itm.Socket3Effectid = ArcSplitGemString(itm.Enchstring, 35)
                    itm.Socket3Id = GetGemIdByEffectId(itm.Socket3Effectid)
                    itm.Socket3Name = GetEffectNameById(itm.Socket3Effectid, My.Settings.language)
                    itm.Socket3Pic = GetItemIconById(itm.Socket3Id, GlobalVariables.GlobalWebClient)
                End If
                If loopcounter = 13 Then
                    Dim resultString As String = ArcSplitGemString(itm.Enchstring, 38)
                    If Not resultString = "" Then player.BeltBuckle = TryInt(resultString)
                End If
                loopcounter += 1
                SetCharacterArmorItem(player, itm)
                SetCharacterSet(tarSetId, player, account)
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
                        itm.EnchantmentEffectid = TryInt(parts2(0))
                        Dim enchArray As Integer() = GetEnchantmentIdAndTypeByEffectId(itm.EnchantmentEffectid)
                        itm.EnchantmentId = enchArray(0)
                        itm.EnchantmentType = enchArray(1)
                        Return GetEffectNameById(itm.EnchantmentEffectid, My.Settings.language)
                    ElseIf parts(1).Contains("0,0") Then
                        Dim parts2() As String = parts(1).Split(","c)
                        itm.EnchantmentEffectid = TryInt(parts2(0))
                       Dim enchArray As Integer() = GetEnchantmentIdAndTypeByEffectId(itm.EnchantmentEffectid)
                        itm.EnchantmentId = enchArray(0)
                        itm.EnchantmentType = enchArray(1)
                        Return GetEffectNameById(itm.EnchantmentEffectid, My.Settings.language)
                    ElseIf parts(2).Contains("0,0") Then
                        Dim parts2() As String = parts(2).Split(","c)
                        itm.EnchantmentEffectid = TryInt(parts2(0))
                        Dim enchArray As Integer() = GetEnchantmentIdAndTypeByEffectId(itm.EnchantmentEffectid)
                        itm.EnchantmentId = enchArray(0)
                        itm.EnchantmentType = enchArray(1)
                        Return GetEffectNameById(itm.EnchantmentEffectid, My.Settings.language)
                    ElseIf parts(3).Contains("0,0") Then
                        Dim parts2() As String = parts(3).Split(","c)
                        itm.EnchantmentEffectid = TryInt(parts2(0))
                       Dim enchArray As Integer() = GetEnchantmentIdAndTypeByEffectId(itm.EnchantmentEffectid)
                        itm.EnchantmentId = enchArray(0)
                        itm.EnchantmentType = enchArray(1)
                        Return GetEffectNameById(itm.EnchantmentEffectid, My.Settings.language)
                    ElseIf parts(4).Contains("0,0") Then
                        Dim parts2() As String = parts(4).Split(","c)
                        itm.EnchantmentEffectid = TryInt(parts2(0))
                        Dim enchArray As Integer() = GetEnchantmentIdAndTypeByEffectId(itm.EnchantmentEffectid)
                        itm.EnchantmentId = enchArray(0)
                        itm.EnchantmentType = enchArray(1)
                        Return GetEffectNameById(itm.EnchantmentEffectid, My.Settings.language)
                    ElseIf parts(5).Contains("0,0") Then
                        Dim parts2() As String = parts(5).Split(","c)
                        itm.EnchantmentEffectid = TryInt(parts2(0))
                        Dim enchArray As Integer() = GetEnchantmentIdAndTypeByEffectId(itm.EnchantmentEffectid)
                        itm.EnchantmentId = enchArray(0)
                        itm.EnchantmentType = enchArray(1)
                        Return GetEffectNameById(itm.EnchantmentEffectid, My.Settings.language)
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
            As Integer
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
                    Return obvalue
                ElseIf parts(1).Contains(xvalue) Then
                    Dim parts2() As String = parts(1).Split(","c)
                    obvalue = TryInt(parts2(0))
                    If xvalue = "0,6" Then Return parts2(0)
                    Return obvalue
                ElseIf parts(2).Contains(xvalue) Then
                    Dim parts2() As String = parts(2).Split(","c)
                    obvalue = TryInt(parts2(0))
                    If xvalue = "0,6" Then Return parts2(0)
                    Return obvalue
                ElseIf parts(3).Contains(xvalue) Then
                    Dim parts2() As String = parts(3).Split(","c)
                    obvalue = TryInt(parts2(0))
                    If xvalue = "0,6" Then Return parts2(0)
                    Return obvalue
                ElseIf parts(4).Contains(xvalue) Then
                    Dim parts2() As String = parts(4).Split(","c)
                    obvalue = TryInt(parts2(0))
                    If xvalue = "0,6" Then Return parts2(0)
                    Return obvalue
                ElseIf parts(5).Contains(xvalue) Then
                    Dim parts2() As String = parts(5).Split(","c)
                    obvalue = TryInt(parts2(0))
                    If xvalue = "0,6" Then Return parts2(0)
                    Return obvalue
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

        Private Sub LoadAtTrinity(ByVal tarSetId As Integer, ByVal account As Account)
            LogAppend("Handling item enchantments @LoadAtTrinity", "CharacterEnchantmentsHandler_LoadAtTrinity", False)
            Dim slotname(19) As String
            slotname =
                {"head", "neck", "shoulder", "back", "chest", "shirt", "tabard", "wrists", "main", "off", "distance",
                 "hands", "waist", "legs", "feet", "finger1", "finger2", "trinket1", "trinket2"}
            Dim loopcounter As Integer = 0
            Dim player As Character = GetCharacterSetBySetId(tarSetId, account)
            Do
                Dim itm As Item
                itm = GetCharacterArmorItem(player, slotname(loopcounter))
                If itm Is Nothing Then
                    loopcounter += 1
                    Continue Do
                End If
                itm.EnchantmentName = TrinitySplitEnchantString(itm.Enchstring, itm)
                If Not loopcounter = 17 Or Not loopcounter = 18 Then
                    itm.Socket1Name = TrinitySplitGemString(itm.Enchstring, 6, itm, 1)
                    itm.Socket2Name = TrinitySplitGemString(itm.Enchstring, 9, itm, 2)
                    itm.Socket3Name = TrinitySplitGemString(itm.Enchstring, 12, itm, 3)
                End If
                If loopcounter = 13 Then
                    '// TODO: Load belt buckle
                End If
                loopcounter += 1
                SetCharacterArmorItem(player, itm)
                SetCharacterSet(tarSetId, player, account)
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
                        itm.EnchantmentEffectid = TryInt(parts(0))
                        Dim enchArray As Integer() = GetEnchantmentIdAndTypeByEffectId(itm.EnchantmentEffectid)
                        itm.EnchantmentId = enchArray(0)
                        itm.EnchantmentType = enchArray(1)
                        Return GetEffectNameById(itm.EnchantmentEffectid, My.Settings.language)
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
                        Case 1
                            itm.Socket1Effectid = TryInt(parts(position))
                            itm.Socket1Id = GetGemIdByEffectId(itm.Socket1Effectid)
                            itm.Socket1Pic = GetItemIconById(itm.Socket1Id, GlobalVariables.GlobalWebClient)
                            Return GetEffectNameById(itm.Socket1Effectid, My.Settings.language)
                        Case 2
                            itm.Socket2Effectid = TryInt(parts(position))
                            itm.Socket2Id = GetGemIdByEffectId(itm.Socket2Effectid)
                            itm.Socket2Pic = GetItemIconById(itm.Socket2Id, GlobalVariables.GlobalWebClient)
                            Return GetEffectNameById(itm.Socket2Effectid, My.Settings.language)
                        Case 3
                            itm.Socket3Effectid = TryInt(parts(position))
                            itm.Socket3Id = GetGemIdByEffectId(itm.Socket3Effectid)
                            itm.Socket3Pic = GetItemIconById(itm.Socket3Id, GlobalVariables.GlobalWebClient)
                            Return GetEffectNameById(itm.Socket3Effectid, My.Settings.language)
                    End Select
                    Return "Not found"
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

        Private Sub LoadAtMangos(ByVal tarSetId As Integer, ByVal account As Account)
            LogAppend("Handling item enchantments @LoadAtMangos", "CharacterEnchantmentsHandler_LoadAtMangos", False)
            Dim slotname(19) As String
            slotname =
                {"head", "neck", "shoulder", "back", "chest", "shirt", "tabard", "wrists", "main", "off", "distance",
                 "hands", "waist", "legs", "feet", "finger1", "finger2", "trinket1", "trinket2"}
            Dim loopcounter As Integer = 0
            Dim player As Character = GetCharacterSetBySetId(tarSetId, account)
            Do
                Dim itm As Item = GetCharacterArmorItem(player, slotname(loopcounter))
                If itm Is Nothing Then
                    loopcounter += 1
                    Continue Do
                End If
                itm.EnchantmentName = MangosSplitEnchantString(itm.Enchstring, itm)
                If Not loopcounter = 17 Or Not loopcounter = 18 Then
                    itm.Socket1Name = MangosSplitGemString(itm.Enchstring, 29, itm, 1)
                    itm.Socket2Name = MangosSplitGemString(itm.Enchstring, 32, itm, 2)
                    itm.Socket3Name = MangosSplitGemString(itm.Enchstring, 35, itm, 3)
                End If
                If loopcounter = 13 Then
                    '// TODO: Load belt buckle
                End If
                loopcounter += 1
                SetCharacterArmorItem(player, itm)
                SetCharacterSet(tarSetId, player, account)
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
                        itm.EnchantmentEffectid = TryInt(parts(22))
                        Dim enchArray As Integer() = GetEnchantmentIdAndTypeByEffectId(itm.EnchantmentEffectid)
                        itm.EnchantmentId = enchArray(0)
                        itm.EnchantmentType = enchArray(1)
                        Return GetEffectNameById(itm.EnchantmentEffectid, My.Settings.language)
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
                        Case 1
                            itm.Socket1Effectid = TryInt(parts(position - 1))
                            itm.Socket1Id = GetGemIdByEffectId(itm.Socket1Effectid)
                            itm.Socket1Pic = GetItemIconById(itm.Socket1Id, GlobalVariables.GlobalWebClient)
                            Return GetEffectNameById(itm.Socket1Effectid, My.Settings.language)
                        Case 2
                            itm.Socket2Effectid = TryInt(parts(position - 1))
                            itm.Socket2Id = GetGemIdByEffectId(itm.Socket2Effectid)
                            itm.Socket2Pic = GetItemIconById(itm.Socket2Id, GlobalVariables.GlobalWebClient)
                            Return GetEffectNameById(itm.Socket2Effectid, My.Settings.language)
                        Case 3
                            itm.Socket3Effectid = TryInt(parts(position - 1))
                            itm.Socket3Id = GetGemIdByEffectId(itm.Socket3Effectid)
                            itm.Socket3Pic = GetItemIconById(itm.Socket3Id, GlobalVariables.GlobalWebClient)
                            Return GetEffectNameById(itm.Socket3Effectid, My.Settings.language)
                    End Select
                    Return "Not found"
                Else
                    Return ""
                End If
            Catch ex As Exception
                LogAppend("Something went wrong while splitting gem string -> Exception is: ###START###" & ex.ToString() &
                                   "###END###", "CharacterEnchantmentsHandler_MangosSplitGemString", False, True)
                Return ""
            End Try
        End Function
    End Class
End Namespace