'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013-2015 NamCore Studio <https://github.com/megasus/Namcore-Studio>
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
'*      /Filename:      TemplateEntry
'*      /Description:   TemplateEntry Object
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Namespace Framework.Modules
    <Serializable()>
    Public Class TemplateEntry
        Public Id As Integer
        Public Name As String
        Public Description As String
        Public OwnerGuid As Integer
        Public OwnerName As String
        Public CreatedDate As DateTime
        Public DownloadCount As Integer
        Public Rating As Integer
        Public DownloadLocation As String
        Public Data As String
        Public FileGuid As Integer
    End Class
End Namespace