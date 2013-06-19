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
'*      /Filename:      Interface_Operator
'*      /Description:   Includes operations for rendering user interfaces
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.IO
Imports Namcore_Studio.GlobalVariables
Imports Namcore_Studio.Basics
Imports Namcore_Studio.Serializer
Public Class Interface_Operator
    Public Shared Sub prepareLive_armory()
        ' Dim x As String = temporaryCharacterInformation.Item(1)

        Dim ms As MemoryStream = Serialize(globChars)
        If My.Computer.FileSystem.FileExists(My.Computer.FileSystem.SpecialDirectories.Desktop & "/tryit.txt") Then
            My.Computer.FileSystem.DeleteFile(My.Computer.FileSystem.SpecialDirectories.Desktop & "/tryit.txt")
        End If
        Dim fs As New StreamWriter(My.Computer.FileSystem.SpecialDirectories.Desktop & "/tryit.txt", FileMode.OpenOrCreate)
        fs.BaseStream.Write(ms.ToArray, 0, ms.ToArray.Length)
        fs.Close()
        ms.Close()
        '   Dim x As String =
        armoryMode = True
        Live_View.Close()
        Dim liveview As New Live_View
        liveview.Show()
        liveview.loadInformationSets_Armory()

    End Sub
    Public Shared Sub loadNamesAndPics()

    End Sub
End Class
