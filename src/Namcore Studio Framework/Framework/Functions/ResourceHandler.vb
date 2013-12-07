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
'*      /Filename:      ResourceHandler
'*      /Description:   Provides access to the UserMessages resource
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Namespace Framework.Functions
    Public Class ResourceHandler
        Public Shared Function GetUserMessage(ByVal field As String)
            Try
                Dim rm As New System.Resources.ResourceManager("NCFramework.UserMessages", Reflection.Assembly.GetExecutingAssembly())
                Return rm.GetString(field)
            Catch ex As Exception
                Return ""
            End Try

        End Function
    End Class
End Namespace