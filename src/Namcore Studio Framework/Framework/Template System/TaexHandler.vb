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
'*      /Filename:      TaexHandler
'*      /Description:   Template Explorer database handler
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.Net
Imports NCFramework.Framework.Modules
Imports NCFramework.Framework.Extension
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Functions
Namespace Framework
    Public Class TaexHandler
        Public Function LoadTemplateEntries(Optional count As Integer = 0) As List(Of TemplateEntry)
            Dim teClient As New WebClient
            teClient.CheckProxy()
            Try
                Dim xml As String = teClient.DownloadString("http://wowgeslauncher.bplaced.net/filemanager/namcore/service/get.php")
                Dim templateEntryLst As New List(Of TemplateEntry)
                While xml.Contains("<entry>")
                    Dim entryContext As String = SplitString(xml, "<entry>", "</entry>")
                    Dim entry As New TemplateEntry
                    entry.Id = TryInt(SplitString(entryContext, "<entry_guid>", "</entry_guid>"))
                    entry.OwnerGuid = TryInt(SplitString(entryContext, "<entry_ownerguid>", "</entry_ownerguid>"))
                    entry.OwnerName = SplitString(entryContext, "<entry_ownername>", "</entry_ownername>")
                    entry.Name = SplitString(entryContext, "<entry_name>", "</entry_name>")
                    entry.Description = SplitString(entryContext, "<entry_description>", "</entry_description>")
                    entry.Data = SplitString(entryContext, "<entry_data>", "</entry_data>")
                    entry.DownloadCount = TryInt(SplitString(entryContext, "<entry_downloadcount>", "</entry_downloadcount>"))
                    entry.Rating = TryInt(SplitString(entryContext, "<entry_rating>", "</entry_rating>"))
                    entry.CreatedDate = DateTime.Parse(SplitString(entryContext, "<entry_creationdate>", "</entry_creationdate>"))
                    entry.FileGuid = TryInt(SplitString(entryContext, "<entry_fileguid>", "</entry_fileguid>"))
                    xml = xml.Replace("<entry>" & entryContext & "</entry>", "")
                    templateEntryLst.Add(entry)
                    If templateEntryLst.Count >= count And count <> 0 Then Exit While
                End While
                Return templateEntryLst
            Catch ex As Exception
                LogAppend("Exception during template entry loading", "TaexHandler_LodTemplateEntries", False, True)
                Return Nothing
            End Try
        End Function
    End Class
End Namespace