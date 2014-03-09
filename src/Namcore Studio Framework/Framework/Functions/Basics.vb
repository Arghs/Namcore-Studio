'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013-2014 NamCore Studio <https://github.com/megasus/Namcore-Studio>
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
'*      /Filename:      Basics
'*      /Description:   Includes basic and frequently used functions
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.Drawing
Imports System.Windows.Forms
Imports NCFramework.Framework.Forms
Imports libnc
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Modules
Imports NCFramework.Framework.Database
Imports System.Net
Imports System.Text

Namespace Framework.Functions
    Public Module Basics
        '// Declaration
        Public Tmpset As Integer
        '// Declaration

        Public Sub InitializeDbc()
            LogAppend("Initializing DBC files", "Basics_InitializeDBC", True)
            Main.Initialize()
        End Sub

        Public Sub ResetTempDataTables()
            TempTable1 = Nothing
            TempTable1Name = ""
            TempTable2 = Nothing
            TempTable2Name = ""
        End Sub

        Public Sub AddAccountSet(ByVal setId As Integer, ByVal player As Account,
                                 Optional globChars As GlobalCharVars = Nothing)
            Dim useChars As GlobalCharVars
            If GlobalVariables.forceTemplateCharVars = False Then
                useChars = GlobalVariables.globChars
            Else
                useChars = GlobalVariables.templateCharVars
            End If
            If useChars.AccountSets Is Nothing Then useChars.AccountSets = New List(Of Account)()
            Dim accountSet As Integer = useChars.AccountSets.FindIndex(Function(account) account.SetIndex = setId)
            If accountSet <> -1 Then
                SetAccountSet(setId, player)
            Else
                useChars.AccountSets.Add(player)
            End If
        End Sub

        Public Function GetAccountSetBySetId(ByVal setId As Integer) As Account
            Dim useChars As GlobalCharVars
            If GlobalVariables.forceTemplateCharVars = False Then
                useChars = GlobalVariables.globChars
            Else
                useChars = GlobalVariables.templateCharVars
            End If
            Dim accountSet As Integer = useChars.AccountSets.FindIndex(Function(account) account.SetIndex = setId)
            If accountSet <> -1 Then
                Return useChars.AccountSets(accountSet)
            Else
                Return Nothing
            End If
        End Function

        Public Sub SetAccountSet(ByVal setId As Integer, ByVal playerAccount As Account)
            Dim useChars As GlobalCharVars
            If GlobalVariables.forceTemplateCharVars = False Then
                useChars = GlobalVariables.globChars
            Else
                useChars = GlobalVariables.templateCharVars
            End If
            If useChars.AccountSets Is Nothing Then useChars.AccountSets = New List(Of Account)()
            Dim accountSet As Integer = useChars.AccountSets.FindIndex(Function(account) account.SetIndex = setId)
            If accountSet <> -1 Then
                useChars.AccountSets(accountSet) = playerAccount
            End If
        End Sub

        Public Function GetCharacterSetBySetId(ByVal setId As Integer, ByVal playerAccount As Account) As Character
            Dim charSet As Integer = playerAccount.Characters.FindIndex(Function(character) character.SetIndex = setId)
            If charSet <> -1 Then
                Return playerAccount.Characters(charSet)
            Else
                Return Nothing
            End If
        End Function

        Public Sub AddCharacterSet(ByVal setId As Integer, ByVal player As Character, ByVal playerAccount As Account)
            player.SetIndex = setId
            playerAccount.Characters.Add(player)
        End Sub

        Public Sub SetCharacterSet(ByVal setId As Integer, ByVal playerCharacter As Character, ByVal playerAccount As Account)
            Dim charSet As Integer = PlayerAccount.Characters.FindIndex(Function(character) character.SetIndex = setId)
            If charSet <> -1 Then
                PlayerAccount.Characters(charSet) = playerCharacter
            End If
        End Sub

        Public Sub RemoveCharacterArmorItem(ByRef player As Character, ByVal itm As Item)
            If player.ArmorItems Is Nothing Then player.ArmorItems = New List(Of Item)
            Dim itmIndex As Integer = player.ArmorItems.FindIndex(Function(item) item.Slot = itm.Slot)
            If itmIndex <> -1 Then
                player.ArmorItems.RemoveAt(itmIndex)
            End If
        End Sub

        Public Sub SetCharacterArmorItem(ByRef player As Character, ByVal itm As Item)
            Dim itmIndex As Integer = player.ArmorItems.FindIndex(Function(item) item.Slot = itm.Slot)
            If itmIndex = -1 Then itmIndex = player.ArmorItems.FindIndex(Function(item) item.Slotname = itm.Slotname)
            If itmIndex <> -1 Then
                player.ArmorItems(itmIndex) = itm
            End If
        End Sub

        Public Function GetCharacterArmorItem(ByVal player As Character, ByVal slot As String,
                                              Optional isint As Boolean = False) As Item
            Dim itmIndex As Integer
            If isint Then
                itmIndex = player.ArmorItems.FindIndex(Function(item) item.Slot = TryInt(slot))
            Else
                itmIndex = player.ArmorItems.FindIndex(Function(item) item.Slotname = slot)
            End If
            If itmIndex <> -1 Then
                Return player.ArmorItems(itmIndex)
            Else
                Return Nothing
            End If
        End Function

        Public Sub AddCharacterGlyph(ByRef player As Character, ByVal gly As Glyph)
            If player.PlayerGlyphs Is Nothing Then player.PlayerGlyphs = New List(Of Glyph)
            player.PlayerGlyphs.Add(gly)
        End Sub

        Public Sub SetCharacterGlyph(ByRef player As Character, ByVal glph As Glyph)
            Dim glyphIndex As Integer = player.PlayerGlyphs.FindIndex(Function(glyph) glyph.Slotname = glph.Slotname)
            If glyphIndex <> -1 Then
                player.PlayerGlyphs(glyphIndex) = glph
            End If
        End Sub

        Public Function GetCharacterGlyph(ByVal player As Character, ByVal slot As String) As Glyph
            If player.PlayerGlyphs Is Nothing Then Return Nothing
            Dim glyphIndex As Integer = player.PlayerGlyphs.FindIndex(Function(glyph) glyph.Slotname = slot)
            If glyphIndex <> -1 Then
                Return player.PlayerGlyphs(glyphIndex)
            Else
                Return Nothing
            End If
        End Function

        Public Function SplitString(ByVal source As String, ByVal start As String, ByVal ending As String) As String
            If source Is Nothing Or start Is Nothing Or ending Is Nothing Then
                LogAppend("Failed to split a string: source might be nothing", "Basics_splitString", False, True)
                Return Nothing
            End If
            LogAppend(
                "Splitting a string. Sourcelength/-/Start/-/End: " & source.Length.ToString & "/-/" & start & "/-/" &
                ending,
                "Basics_splitString", False)
            Try
                Dim quellcode As String = source
                Dim mystart As String = start
                Dim myend As String = ending
                Dim quellcodeSplit As String
                quellcodeSplit = Split(quellcode, mystart, 5)(1)
                Return Split(quellcodeSplit, myend, 6)(0)
            Catch ex As Exception
                LogAppend(
                    "Error while splitting string! -> Returning nothing -> Exception is: ###START###" & ex.ToString() &
                    "###END###", "Basics_splitString", False, True)
                Return Nothing
            End Try
        End Function

        Public Function SplitList(ByVal source As String, ByVal category As String) As String
            LogAppend("Splitting a list. Sourcelength/-/Start/-/End: " & source.Length.ToString & "/-/" & category,
                      "Basics_splitList", False)
            Try
                Dim quellcode As String = source
                Dim mystart As String = "<" & category & ">"
                Dim myend As String = "</" & category & ">"
                Dim quellcodeSplit As String
                quellcodeSplit = Split(quellcode, mystart, 5)(1)
                quellcodeSplit = Split(quellcodeSplit, myend, 6)(0)
                Return quellcodeSplit
            Catch ex As Exception
                LogAppend(
                    "Error while splitting list! -> Returning nothing -> Exception is: ###START###" & ex.ToString() &
                    "###END###", "Basics_splitList", False, True)
                Return Nothing
            End Try
        End Function

        Public Function LoadImageFromUrl(ByRef url As String) As Bitmap
            LogAppend("Loading image from url: " & url, "Basics_LoadImageFromUrl", False)
            Try
                Dim request As HttpWebRequest = DirectCast(HttpWebRequest.Create(url), HttpWebRequest)
                request.Proxy = GlobalVariables.GlobalWebClient.Proxy
                Dim response As HttpWebResponse = DirectCast(request.GetResponse, HttpWebResponse)
                Dim img As Bitmap = CType(Image.FromStream(response.GetResponseStream()), Bitmap)
                response.Close()
                Return img
            Catch ex As Exception
                LogAppend("Error while loading image: " & ex.ToString(), "Basics_LoadImageFromUrl", False, True)
                Return Nothing
            End Try
        End Function

        Public Function ExecuteDataTableSearch(ByVal dt As DataTable, ByVal startfield As String,
                                               ByVal startvalue As String, ByVal targetfield As Integer) As String()
            Try
                Dim foundRows() As DataRow
                foundRows = dt.Select(startfield & " = '" & EscapeLikeValue(startvalue) & "'")
                If foundRows.Length = 0 Then
                    Return {"-"}
                Else
                    Dim resultArray(foundRows.Count()) As String
                    resultArray(0) = "-"
                    For i = 0 To foundRows.Count() - 1
                        resultArray(i) = (foundRows(i)(targetfield)).ToString
                    Next i
                    Return resultArray
                End If
            Catch ex As Exception
                Return {"-"}
            End Try
        End Function

        Public Function ExecuteDataTableSearch(ByVal dt As DataTable, ByVal command As String) As List(Of String())
            Try
                Dim foundRows() As DataRow
                foundRows = dt.Select(command)
                If foundRows.Length = 0 Then
                    Return Nothing
                Else
                    Dim resultList As New List(Of String())
                   
                    For i = 0 To foundRows.Count() - 1
                        Dim colCount As Integer = foundRows(i).Table.Columns.Count()
                        Dim resultArray(colCount - 1) As String
                        For z = 0 To colCount - 1
                            resultArray(z) = (foundRows(i)(z)).ToString
                        Next z
                        resultList.Add(resultArray)
                    Next i
                    Return resultList
                End If
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Function EscapeLikeValue(ByVal value As String) As String
            Dim sb As New StringBuilder(value.Length)
            For i = 0 To value.Length - 1
                Dim c As Char = value(i)
                Select Case c
                    Case "]"c
                    Case "]"c, "["c, "%"c, "*"c
                        sb.Append("[").Append(c).Append("]")
                        Exit Select
                    Case "'"c
                        sb.Append("''")
                        Exit Select
                    Case Else
                        sb.Append(c)
                        Exit Select
                End Select
            Next
            Return sb.ToString()
        End Function
        Public Sub CloseProcessStatus()
            If GlobalVariables.DebugMode = False Then
                Try
                    For Each myForm As Form In _
                        From myForm1 As Object In Application.OpenForms Where CType(myForm1, Form).Name = "ProcessStatus"
                        myForm.Close()
                        Application.DoEvents()
                    Next
                Catch ex As Exception :
                End Try
            End If
        End Sub

        Public Sub NewProcessStatus()
            Try
                For Each myForm As Form In _
                     From myForm1 As Object In Application.OpenForms Where CType(myForm1, Form).Name = "ProcessStatus"
                    If GlobalVariables.DebugMode = True Then
                        Exit Sub
                    Else
                        Application.DoEvents()
                        myForm.Close()
                    End If
                Next
            Catch ex As Exception :
            End Try
            GlobalVariables.procStatus = New ProcessStatus
            GlobalVariables.procStatus.Show()
            Application.DoEvents()
        End Sub
    End Module
End Namespace