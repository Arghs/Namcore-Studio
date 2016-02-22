'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013-2016 NamCore Studio <https://github.com/megasus/Namcore-Studio>
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
'*      /Filename:      GlyphParser
'*      /Description:   Contains functions for loading character glyphs from wow armory
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.Net
Imports libnc.Provider
Imports NCFramework.Framework.Extension
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Modules
Imports Newtonsoft.Json.Linq

Namespace Framework.Armory.Parser
    Public Class GlyphParser
        Public Sub LoadGlyphs(setId As Integer, apiLink As String, account As Account)
            Dim client As New WebClient
            client.CheckProxy()
            '// Retrieving character
            Dim player As Character = GetCharacterSetBySetId(setId, account)
            Try
                LogAppend("Loading character glyph information - setId: " & setId.ToString() & " - apiLink: " & apiLink,
                          "GlyphParser_LoadGlyphs", True)
                '// Using API to load glyph info
                Dim glyphContext As String = client.DownloadString(apiLink & "?fields=talents")
                If Not glyphContext.Contains("""glyphs"":") Then
                    LogAppend("No glyphs found!?", "GlyphParser_LoadGlyphs", True)
                    Exit Sub '// Skip if no glyphs
                End If
                Dim jResults As JObject = JObject.Parse(glyphContext)
                Dim results As List(Of JToken) = jResults.Children().ToList()
                Dim token =
                        CType(results.Find(Function(jtoken) CType(jtoken, JProperty).Name = "talents"),
                              JProperty)
                If token.HasChildren Then
                    For i = 0 To token.GetObjects().Count - 1
                        Dim glyphToken As JProperty =
                                token.GetObjects()(i).Children.Cast (Of JProperty).ToList().Find(
                                    Function(jProperty) jProperty.Name = "glyphs")
                        If glyphToken.HasItem("major") Then
                            Dim majorToken As List(Of JObject) = glyphToken.GetChild("major").GetObjects()
                            For z = 0 To majorToken.Count - 1
                                Dim singleGlyph As List(Of JProperty) =
                                        majorToken(z).Children.Cast (Of JProperty).ToList()
                                Dim pGlyph As New Glyph
                                With pGlyph
                                    .Id = CInt(singleGlyph.GetValue("item"))
                                    .Name = singleGlyph.GetValue("name")
                                    .Image = GetItemIconByItemId(.Id, GlobalVariables.GlobalWebClient)
                                    .Spec = i + 1
                                    .Type = Glyph.GlyphType.GLYTYPE_MAJOR
                                    .Slotname = "majorglyph" & (z + 1).ToString()
                                    If i = 1 Then .Slotname = "sec" & .Slotname
                                    AddCharacterGlyph(player, pGlyph)
                                    LogAppend("Loaded glyph " & .Name, "GlyphParser_LoadGlyphs", True)
                                End With
                            Next z
                        End If
                        If glyphToken.HasItem("minor") Then
                            Dim minorToken As List(Of JObject) = glyphToken.GetChild("minor").GetObjects()
                            For z = 0 To minorToken.Count - 1
                                Dim singleGlyph As List(Of JProperty) =
                                        minorToken(z).Children.Cast (Of JProperty).ToList()
                                Dim pGlyph As New Glyph
                                With pGlyph
                                    .Id = CInt(singleGlyph.GetValue("item"))
                                    .Name = singleGlyph.GetValue("name")
                                    .Image = GetItemIconByItemId(.Id, GlobalVariables.GlobalWebClient)
                                    .Spec = i + 1
                                    .Type = Glyph.GlyphType.GLYTYPE_MINOR
                                    .Slotname = "minorglyph" & (z + 1).ToString()
                                    If i = 1 Then .Slotname = "sec" & .Slotname
                                    AddCharacterGlyph(player, pGlyph)
                                    LogAppend("Loaded glyph " & .Name, "GlyphParser_LoadGlyphs", True)
                                End With
                            Next z
                        End If
                    Next i
                End If
            Catch ex As Exception
                LogAppend("Exception occured: " & vbNewLine & ex.ToString(), "GlyphParser_LoadGlyphs", False, True)
            End Try
            '// Saving changes to character
            SetCharacterSet(setId, player, GetAccountSetBySetId(player.AccountSet))
        End Sub
    End Class
End Namespace