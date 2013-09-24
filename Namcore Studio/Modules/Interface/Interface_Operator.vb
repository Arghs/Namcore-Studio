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
'*      /Filename:      InterfaceOperator
'*      /Description:   Includes operations for rendering user interfaces
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.IO
Imports Namcore_Studio.Forms
Imports NCFramework.Framework.Modules
Imports NCFramework

Namespace Modules.Interface
    Public Module InterfaceOperator
        Public Sub prepareLive_armory()
            Dim mSerializer As Serializer = New Serializer
            Dim ms As MemoryStream = mSerializer.Serialize(GlobalVariables.globChars)
            If My.Computer.FileSystem.FileExists(My.Computer.FileSystem.SpecialDirectories.Desktop & "/tryit.txt") Then
                My.Computer.FileSystem.DeleteFile(My.Computer.FileSystem.SpecialDirectories.Desktop & "/tryit.txt")
            End If
            Dim _
                fs As _
                    New StreamWriter(My.Computer.FileSystem.SpecialDirectories.Desktop & "/tryit.txt",
                                     FileMode.OpenOrCreate)
            fs.BaseStream.Write(ms.ToArray, 0, ms.ToArray.Length)
            fs.Close()
            ms.Close()
            GlobalVariables.armoryMode = True
            LiveView.Close()
            Dim myliveview As New LiveView
            myliveview.loadInformationSets_Armory()
            myliveview.Show()
        End Sub
    End Module
End Namespace