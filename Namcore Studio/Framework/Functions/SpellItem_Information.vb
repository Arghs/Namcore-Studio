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
'*      /Filename:      SpellItem_Information
'*      /Description:   Includes functions for locating certain item and spell information
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


Imports Namcore_Studio.GlobalVariables
Imports Namcore_Studio.Basics
Imports Namcore_Studio.EventLogging
Imports Namcore_Studio.Conversions
Imports System.Net
Public Class SpellItem_Information
    Public Shared Function GetGlyphIdByItemId(ByVal itemid As Integer) As Integer
        LogAppend("Loading GlyphId by ItemId " & itemid.ToString, "SpellItem_Information_GetGlyphIdByItemId", False)
        Dim xpacressource As String
        Try
            Select Case expansion
                Case 3
                    xpacressource = My.Resources.GlyphProperties_335
                Case 4
                    xpacressource = My.Resources.GlyphProperties_434
                Case Else
                    xpacressource = My.Resources.GlyphProperties_335
            End Select
            Dim client As New WebClient
            Return tryint(splitString(client.DownloadString("http://www.wowhead.com/spell=" & splitString(xpacressource, "<entry>" & itemid.ToString & "</entry><spell>", "</spell>")), ",""id"":", ",""level"""))
        Catch ex As Exception
            LogAppend("Error while loading GlyphId! -> Returning 0 -> Exception is: ###START###" & ex.ToString() & "###END###", "SpellItem_Information_GetGlyphIdByItemId", False, True)
            Return 0
        End Try
    End Function
    Public Shared Sub LoadWeaponType(ByVal itemid As Integer, ByVal tar_set As Integer)
        If Not itemid = 0 Then
            LogAppend("Loading weapon type of Item " & itemid.ToString, "SpellItem_Information_LoadWeaponType", False)
            Try
                Dim client As New WebClient
                Dim excerpt As String = splitString(client.DownloadString("http://www.wowhead.com/item=" & itemid.ToString & "&xml"), "<subclass id=", "</subclass>")
                Select Case True
                    Case excerpt.ToLower.Contains(" crossbow ")
                        AppendTemporaryCharacterInformation("@character_specialspells", "5011", tar_set)
                        AppendTemporaryCharacterInformation("@character_specialskills", "226", tar_set)
                    Case excerpt.ToLower.Contains(" bow ")
                        AppendTemporaryCharacterInformation("@character_specialspells", "264", tar_set)
                        AppendTemporaryCharacterInformation("@character_specialskills", "45", tar_set)
                    Case excerpt.ToLower.Contains(" gun ")
                        AppendTemporaryCharacterInformation("@character_specialspells", "266", tar_set)
                        AppendTemporaryCharacterInformation("@character_specialskills", "46", tar_set)
                    Case excerpt.ToLower.Contains(" thrown ")
                        AppendTemporaryCharacterInformation("@character_specialspells", "2764", tar_set)
                        AppendTemporaryCharacterInformation("@character_specialspells", "2567", tar_set)
                        AppendTemporaryCharacterInformation("@character_specialskills", "176", tar_set)
                    Case excerpt.ToLower.Contains(" wands ")
                        AppendTemporaryCharacterInformation("@character_specialspells", "5009", tar_set)
                        AppendTemporaryCharacterInformation("@character_specialspells", "5019", tar_set)
                        AppendTemporaryCharacterInformation("@character_specialskills", "228", tar_set)
                    Case excerpt.ToLower.Contains(" sword ")
                        If excerpt.ToLower.Contains(" one-handed ") Then
                            AppendTemporaryCharacterInformation("@character_specialspells", "201", tar_set)
                            AppendTemporaryCharacterInformation("@character_specialskills", "43", tar_set)
                        Else
                            AppendTemporaryCharacterInformation("@character_specialspells", "201", tar_set)
                            AppendTemporaryCharacterInformation("@character_specialskills", "43", tar_set)
                            AppendTemporaryCharacterInformation("@character_specialspells", "202", tar_set)
                            AppendTemporaryCharacterInformation("@character_specialskills", "55", tar_set)
                        End If
                    Case excerpt.ToLower.Contains(" dagger ")
                        AppendTemporaryCharacterInformation("@character_specialspells", "1180", tar_set)
                        AppendTemporaryCharacterInformation("@character_specialskills", "173", tar_set)
                    Case excerpt.ToLower.Contains(" axe ")
                        If excerpt.ToLower.Contains(" one-handed ") Then
                            AppendTemporaryCharacterInformation("@character_specialspells", "196", tar_set)
                            AppendTemporaryCharacterInformation("@character_specialskills", "44", tar_set)
                        Else
                            AppendTemporaryCharacterInformation("@character_specialspells", "197", tar_set)
                            AppendTemporaryCharacterInformation("@character_specialskills", "44", tar_set)
                            AppendTemporaryCharacterInformation("@character_specialspells", "196", tar_set)
                            AppendTemporaryCharacterInformation("@character_specialskills", "142", tar_set)
                        End If
                    Case excerpt.ToLower.Contains(" mace ")
                        If excerpt.ToLower.Contains(" one-handed ") Then
                            AppendTemporaryCharacterInformation("@character_specialspells", "198", tar_set)
                            AppendTemporaryCharacterInformation("@character_specialskills", "54", tar_set)
                        Else
                            AppendTemporaryCharacterInformation("@character_specialskills", "54", tar_set)
                            AppendTemporaryCharacterInformation("@character_specialspells", "198", tar_set)
                            AppendTemporaryCharacterInformation("@character_specialskills", "160", tar_set)
                            AppendTemporaryCharacterInformation("@character_specialspells", "199", tar_set)
                        End If
                    Case excerpt.ToLower.Contains(" polearm ")
                        AppendTemporaryCharacterInformation("@character_specialspells", "200", tar_set)
                        AppendTemporaryCharacterInformation("@character_specialskills", "229", tar_set)
                    Case excerpt.ToLower.Contains(" staff ")
                        AppendTemporaryCharacterInformation("@character_specialspells", "227", tar_set)
                        AppendTemporaryCharacterInformation("@character_specialskills", "136", tar_set)
                    Case Else : End Select
            Catch ex As Exception
                LogAppend("Error while loading weapon type! -> Exception is: ###START###" & ex.ToString() & "###END###", "SpellItem_Information_LoadWeaponType", False, True)
            End Try
        Else : End If
    End Sub
    Public Shared Function GetEffectNameByEffectId(ByVal effectid As Integer) As String
        LogAppend("Loading effectname by effectId: " & effectid.ToString, "SpellItem_Information_GetEffectNameByEffectId", False)
        If effectname_dt.Rows.Count = 0 Then
            Try
                effectname_dt.Clear()
                effectname_dt = New DataTable()
                Dim stext As String
                If My.Settings.language = "de" Then
                    stext = My.Resources.enchant_name_de
                Else
                    stext = My.Resources.enchant_name_en
                End If
                Dim a() As String
                Dim strArray As String()
                a = Split(stext, vbNewLine)
                For i = 0 To UBound(a)
                    strArray = a(i).Split(CChar(";"))
                    If i = 0 Then
                        For Each value As String In strArray
                            effectname_dt.Columns.Add(value.Trim())
                        Next
                    Else
                        Dim dr As DataRow = effectname_dt.NewRow()
                        effectname_dt.Rows.Add(strArray)
                    End If
                Next i
            Catch ex As Exception
                LogAppend("Error filling datatable! -> Exception is: ###START###" & ex.ToString() & "###END###", "SpellItem_Information_GetEffectNameByEffectId", False, True)
                Return "Error loading effectname"
                Exit Function
            End Try
        End If
        Dim nameresult As String = Execute("effectid", effectid.ToString(), effectname_dt)
        If nameresult = "-" Then
            LogAppend("Entry not found -> Returning error message", "SpellItem_Information_GetEffectNameByEffectId", False, True)
            Return "Error loading effectname"
        Else
            Return nameresult
        End If
    End Function
   
    Public Shared Function getNameOfItem(ByVal itemid As String) As String
        'If itemid = "-" Or itemid = "" Or itemid = "0" Then Return "-"
        'Try
        '    Dim nameresult As String = executex("itemid", itemid, Main.itemname_dt) 'todo
        '    If nameresult = "-" Then
        '        Return "Error loading itemname"
        '    Else
        '        Return nameresult
        '    End If
        'Catch ex As Exception
        '    Return "Error loading itemname"
        'End Try
    End Function
    Public Shared Function GetGemEffectName(ByVal socketid As Integer) As String
        'todo
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String
            If My.Settings.language = "de" Then
                quellcodeyx88 = clienyx88.DownloadString("http://de.wowhead.com/item=" & socketid.ToString & "&xml")
            Else
                quellcodeyx88 = clienyx88.DownloadString("http://www.wowhead.com/item=" & socketid.ToString & "&xml")
            End If
            Dim anfangyx88 As String = "<span class=""q1"">"
            Dim endeyx88 As String = "</span>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)


            If quellcodeSplityx88.Contains("<a href") Then
                Try
                    Dim anfangyx888 As String = "<a href="""
                    Dim endeyx888 As String = """>"
                    Dim quellcodeSplityx888 As String
                    quellcodeSplityx888 = Split(quellcodeSplityx88, anfangyx888, 5)(1)
                    quellcodeSplityx888 = Split(quellcodeSplityx888, endeyx888, 6)(0)
                    quellcodeSplityx88 = quellcodeSplityx88.Replace("<a href=""" & quellcodeSplityx888 & """>", "")
                    quellcodeSplityx88 = quellcodeSplityx88.Replace("</a>", "")
                    Return quellcodeSplityx88
                Catch ex As Exception
                    Return quellcodeSplityx88
                End Try
            Else
                Return quellcodeSplityx88
            End If
        Catch ex As Exception
            Return ""
        End Try

    End Function
    Private Shared Function Execute(ByVal field As String, ByVal isvalue As String, ByVal tempdatatable As DataTable, Optional secfield As Integer = 1) As String
        LogAppend("Browsing datatale (field = " & field & " // value = " & isvalue & ")", "SpellItem_Information_Execute", False)
        Try
            Dim foundRows() As DataRow
            foundRows = tempdatatable.Select(field & " = '" & isvalue & "'")
            If foundRows.Length = 0 Then
                Return "-"
            Else
                Dim i As Integer
                Dim tmpreturn As String = "-"
                For i = 0 To foundRows.GetUpperBound(0)
                    tmpreturn = (foundRows(i)(secfield)).ToString
                Next i
                Return tmpreturn
            End If
        Catch ex As Exception
            LogAppend("Error while browsing datatable! -> Exception is: ###START###" & ex.ToString() & "###END###", "SpellItem_Information_Execute", False, True)
            Return "-"
        End Try
    End Function
End Class
