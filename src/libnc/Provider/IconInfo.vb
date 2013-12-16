'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013 NamCore Studio <https://github.com/megasus/Namcore-Studio>
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
'*      /Filename:      IconInfo
'*      /Description:   Provides icon information for spells and items
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports libnc.Main
Imports System.Drawing
Imports System.Net

Namespace Provider
    Public Module IconInfo
        Public Function GetSpellIconById(ByVal spellId As Integer, ByVal client As WebClient) As Image
            CheckInit()
            '// Not working: CSV does not contain spell ids but icon ids
            Const targetField As Integer = 1
            Dim myResult As String = ExecuteCsvSearch(SpellIconCsv, "SpellId", spellId.ToString(), targetField)(0)
            If myResult = "-" Then
                Return My.Resources.INV_Misc_QuestionMark
            End If
            myResult = myResult.ToLower().Replace("INTERFACE/SPELLBOOK", "")
            If myResult = "" Then Return My.Resources.INV_Misc_QuestionMark
            Dim pic As Image = libncadvanced.My.Resources.ResourceManager.GetObject(myResult.ToLower())
            If pic Is Nothing Then
                Dim onlinePic As Image =
                        LoadImageFromUrl("http://wow.zamimg.com/images/wow/icons/large/" & myResult.ToLower() & ".jpg", client)
                If onlinePic Is Nothing Then
                    Return My.Resources.INV_Misc_QuestionMark
                Else
                    Return onlinePic
                End If
            Else
                Return pic
            End If
        End Function
        Public Function GetItemIconById(ByVal itemId As Integer, ByVal client As WebClient, Optional forceOnline As Boolean = True) As Image
            CheckInit()
            Const targetField As Integer = 1
            Dim myResult As String = ""
            If forceOnline = False Then
                myResult = ExecuteCsvSearch(ItemDisplayInfoCsv, "ItemId", itemId.ToString(), targetField)(0)
                If myResult Is Nothing Then
                    Try
                        Dim itemContext As String =
                                client.DownloadString("http://www.wowhead.com/item=" & itemId.ToString & "&xml")
                        Try
                            myResult = SplitString(itemContext, "<icon displayId=""" & SplitString(itemContext, "<icon displayId=""", """>") & """>", "</icon>")
                        Catch ex As Exception
                            Return My.Resources.INV_Misc_QuestionMark
                        End Try
                    Catch
                        Return My.Resources.INV_Misc_QuestionMark
                    End Try
                End If
                If myResult = "-" Then
                    Try
                        Dim itemContext As String =
                                client.DownloadString("http://www.wowhead.com/item=" & itemId.ToString & "&xml")
                        Try
                            myResult = SplitString(itemContext, "<icon displayId=""" & SplitString(itemContext, "<icon displayId=""", """>") & """>", "</icon>")
                        Catch ex As Exception
                            Return My.Resources.INV_Misc_QuestionMark
                        End Try
                    Catch
                        Return My.Resources.INV_Misc_QuestionMark
                    End Try
                End If
            Else
                If myResult Is Nothing Then
                    Try
                        Dim itemContext As String =
                                client.DownloadString("http://www.wowhead.com/item=" & itemId.ToString & "&xml")
                        Try
                            myResult = SplitString(itemContext, "<icon displayId=""" & SplitString(itemContext, "<icon displayId=""", """>") & """>", "</icon>")
                        Catch ex As Exception
                            Return My.Resources.INV_Misc_QuestionMark
                        End Try
                    Catch
                        Return My.Resources.INV_Misc_QuestionMark
                    End Try
                End If
                If myResult = "" Then
                    Try
                        Dim itemContext As String =
                                client.DownloadString("http://www.wowhead.com/item=" & itemId.ToString & "&xml")
                        Try
                            myResult = SplitString(itemContext, "<icon displayId=""" & SplitString(itemContext, "<icon displayId=""", """>") & """>", "</icon>")
                        Catch ex As Exception
                            Return My.Resources.INV_Misc_QuestionMark
                        End Try
                    Catch
                        Return My.Resources.INV_Misc_QuestionMark
                    End Try
                End If
            End If
            If myResult IsNot Nothing Then
                Dim pic As Image = libncadvanced.My.Resources.ResourceManager.GetObject(myResult.ToLower())
                If pic Is Nothing Then
                    Dim onlinePic As Image =
                            LoadImageFromUrl("http://wow.zamimg.com/images/wow/icons/large/" & myResult.ToLower() & ".jpg", client)
                    If onlinePic Is Nothing Then
                        Return My.Resources.INV_Misc_QuestionMark
                    Else
                        Return onlinePic
                    End If
                Else
                    Return pic
                End If
            Else : Return My.Resources.INV_Misc_QuestionMark
            End If
        End Function
        Private Function LoadImageFromUrl(ByRef url As String, ByVal client As WebClient) As Image
            Try
                Dim request As HttpWebRequest = DirectCast(HttpWebRequest.Create(url), HttpWebRequest)
                request.Proxy = client.Proxy
                Dim response As HttpWebResponse = DirectCast(request.GetResponse, HttpWebResponse)
                Dim img As Image = Image.FromStream(response.GetResponseStream())
                response.Close()
                Return img
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
    End Module
End Namespace