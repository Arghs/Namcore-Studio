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
'*      /Filename:      WebConnection
'*      /Description:   Provides functions to get default proxy information
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.Net

Namespace Framework.Functions
    Public Class WebConnection
        Public Function GetProxyServerName() As String

            ' ReSharper disable UnusedVariable
            Dim useProxy As New WebProxy()
            ' ReSharper restore UnusedVariable
            Try 'if no proxy is specified, an exception is 
                'thrown by the frameworks and must be caught

                ' ReSharper disable VBWarnings::BC40008
                Return useProxy.GetDefaultProxy.Address.Host
                ' ReSharper restore VBWarnings::BC40008

            Catch 'catch the error when no proxy is specified in IE

                Return Nothing

            End Try
        End Function


        Public Function GetProxyServerPort() As String

            ' ReSharper disable UnusedVariable
            Dim useProxy As New WebProxy()
            ' ReSharper restore UnusedVariable

            Try 'if no proxy is specified, an exception is 
                'thrown by the frameworks and must be caught

                ' ReSharper disable VBWarnings::BC40008
                Return CStr(useProxy.GetDefaultProxy.Address.Port)
                ' ReSharper restore VBWarnings::BC40008

            Catch 'catch the error when no proxy is specified in IE

                Return Nothing

            End Try
        End Function
    End Class
End Namespace