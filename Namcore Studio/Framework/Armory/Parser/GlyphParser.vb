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
'*      /Filename:      GlyphParser
'*      /Description:   Contains functions for loading character glyphs from wow armory
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Imports Namcore_Studio.EventLogging
Imports Namcore_Studio.Conversions
Imports Namcore_Studio.SpellItem_Information
Imports Namcore_Studio.Basics
Imports System.Net

Public Class GlyphParser
    Public Sub loadGlyphs(ByVal setID As Integer, ByVal apiLink As String)
        Dim client As New WebClient
        client.CheckProxy()
        '// Retrieving character
        Dim player As Character = GetCharacterSetBySetId(setID)
        Try
            LogAppend("Loading character glyph information", "GlyphParser_loadGlyphs", True)
            '// Using API to load glyph info
            Dim glyphContext As String = client.DownloadString(apiLink & "?fields=talents")
            Dim slotAddition As String
            Dim mainContext As String
            If Not glyphContext.Contains("""glyphs"":") Then Exit Sub '// Skip if no glyphs
            For i = 1 To 2
                Select Case i
                    Case 1 : mainContext = splitString(glyphContext, """glyphs"":", ",""spec"":") : slotAddition = "" '// Spec 0
                    Case 2 : mainContext = splitString(glyphContext, ",""spec"":", "}]}") : slotAddition = "sec" '// Spec 1
                    Case Else : mainContext = splitString(glyphContext, ",""spec"":", "}]}") : slotAddition = "sec" '// Spec 1
                End Select
                Dim gType As String = "minor"
                Dim loopCounter As Integer = 0
                Do
                    If mainContext.Contains("""" & gType & """") Then
                        Dim GlyphStr As String = splitString(mainContext, """" & gType & """:[", "]")
                        Dim exCounter As Integer = UBound(Split(GlyphStr, "{""glyph"""))
                        Dim counter As Integer = 0
                        GlyphStr = GlyphStr.Replace("},", "*")
                        If Not GlyphStr.Length <= 3 Then
                            Do
                                Dim newGlyph As New Glyph
                                Dim parts() As String = GlyphStr.Split("*"c)
                                newGlyph.id = TryInt(splitString(parts(counter), """item"":", ","""))
                                newGlyph.name = splitString(parts(counter), """name"":", ",""")
                                newGlyph.slotname = slotAddition & gType & "glyph" & (counter + 1).ToString()
                                newGlyph.image = GetIconByItemId(newGlyph.id)
                                newGlyph.type = loopCounter + 1
                                newGlyph.spec = i
                                AddCharacterGlyph(player, newGlyph)
                                counter += 1
                            Loop Until counter = exCounter
                        End If
                    End If
                    gType = "major"
                    loopCounter += 1
                Loop Until loopCounter = 2
            Next i
        Catch ex As Exception
            LogAppend("Exception occured: " & vbNewLine & ex.ToString(), "GlyphParser_loadGlyphs", False, True)
        End Try
        '// Saving changes to character
        SetCharacterSet(setID, player)
    End Sub
End Class
