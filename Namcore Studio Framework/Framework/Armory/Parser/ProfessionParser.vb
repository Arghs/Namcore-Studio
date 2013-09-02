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
'*      /Filename:      ProfessionParser
'*      /Description:   Contains functions for loading character profession information 
'*                      from wow armory
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


Imports System.Net

Public Class ProfessionParser
    Public Sub loadProfessions(ByVal setId As Integer, ByVal apiLink As String)
        Dim client As New WebClient
        client.CheckProxy()
        '// Retrieving character
        Dim player As Character = GetCharacterSetBySetId(setId)
        player.Professions = New List(Of Profession)
        Dim pProf As Profession
        Try
            LogAppend("Loading character profession information", "ProfessionParser_loadProfessions", True)
            '// Using API to load profession info
            Dim pfContext As String = client.DownloadString(apiLink & "?fields=professions")
            If pfContext Is Nothing Then
                LogAppend("pfContext is nothing - Failed to load Professions API", "ProfessionParser_loadProfessions", False, True)
                Exit Sub
            Else
                LogAppend("pfContext loaded - length: " & pfContext.Length.ToString(), "ProfessionParser_loadProfessions", False)
            End If
            Dim pfStr As String = splitString(pfContext, """professions"":{", "}}") & ","
            Dim primaryPf As String = splitString(pfStr, """primary"":[", "}],")
            Dim secondaryPf As String = splitString(pfStr, """secondary"":[", "}],")
            Dim usePfString As String = primaryPf
            Do
                pProf = New Profession()
                Dim excounter As Integer = UBound(Split(usePfString, "}")) + 1
                Dim partsPf() As String = usePfString.Split("}"c)
                Dim loopcounter As Integer = 0
                Do
                    If usePfString = primaryPf Then
                        pProf.primary = True
                    Else
                        pProf.primary = False
                    End If
                    pProf.iconname = splitString(partsPf(loopcounter), """icon"":""", """,")
                    pProf.id = TryInt(splitString(partsPf(loopcounter), """id"":", ","))
                    pProf.max = TryInt(splitString(partsPf(loopcounter), """max"":", ","))
                    pProf.name = splitString(partsPf(loopcounter), """name"":""", """,")
                    pProf.rank = TryInt(splitString(partsPf(loopcounter), """rank"":", ","))
                    Dim recipes As String = splitString(partsPf(loopcounter), """recipes"":[", "]")
                    If recipes.Length > 3 Then
                        pProf.recipes = recipes.Split(",")
                    End If
                    player.Professions.Add(pProf)
                    loopcounter += 1
                Loop Until loopcounter = excounter
                If usePfString = secondaryPf Then
                    Exit Do
                Else
                    usePfString = secondaryPf
                End If
            Loop
            '// Saving changes to character
            SetCharacterSet(setId, player)
        Catch ex As Exception
            LogAppend("Exception occured: " & vbNewLine & ex.ToString(), "ProfessionParser_loadProfessions", False)
        End Try
    End Sub
End Class
