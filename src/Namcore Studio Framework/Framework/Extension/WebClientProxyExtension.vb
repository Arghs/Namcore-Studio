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
'*      /Filename:      WebClientProxyExtension
'*      /Description:   Extension to add proxy information to webclient
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.Runtime.CompilerServices
Imports System.Net

Namespace Framework.Extension
    Public Module WebClientProxyExtension
    ''' <summary>
    '''     proxy information handler
    ''' </summary>
                                         <Extension()>
        Public Function CheckProxy(ByRef client As WebClient) As WebClient
            Try
                If My.Settings.proxy_enabled = True Then
                    Client.Proxy = My.Settings.fullproxy
                    If My.Settings.proxy_defaultCredentials = False Then
                        Client.Proxy.Credentials = New NetworkCredential(My.Settings.proxy_uname, My.Settings.proxy_pass)
                    Else
                        Client.Proxy.Credentials = CredentialCache.DefaultCredentials
                    End If
                End If
                Return Client
            Catch ex As Exception
                Return Client
            End Try
        End Function
    End Module
End Namespace