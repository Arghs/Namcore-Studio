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
'*      /Filename:      Settings_interface
'*      /Description:   Proxy settings
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.Net
Imports NCFramework
Public Class Settings_interface

    Private Sub manualproxy_radio_CheckedChanged(sender As Object, e As EventArgs) Handles manualproxy_radio.CheckedChanged
        Label1.Enabled = True
        Label2.Enabled = True
        port_ud.Enabled = True
        url_tb.Enabled = True
        defcred_cb.Enabled = True
    End Sub

    Private Sub port_ud_SelectedItemChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub noproxy_radio_CheckedChanged(sender As Object, e As EventArgs) Handles noproxy_radio.CheckedChanged
        Label1.Enabled = False
        Label2.Enabled = False
        port_ud.Enabled = False
        url_tb.Enabled = False
        defcred_cb.Enabled = False
    End Sub

    Private Sub detectproxy_radio_CheckedChanged(sender As Object, e As EventArgs) Handles detectproxy_radio.CheckedChanged
        Label1.Enabled = False
        Label2.Enabled = False
        port_ud.Enabled = False
        url_tb.Enabled = False
        defcred_cb.Enabled = False
    End Sub



    Private Sub Settings_interface_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If NCFramework.My.MySettings.Default.proxy_enabled = False Then
            noproxy_radio.Checked = True
        ElseIf NCFramework.My.MySettings.Default.proxy_detect = True Then
            detectproxy_radio.Checked = True
        Else
            manualproxy_radio.Checked = True
            url_tb.Text = NCFramework.My.MySettings.Default.proxy_host
            port_ud.Value = NCFramework.My.MySettings.Default.proxy_port
            If NCFramework.My.MySettings.Default.proxy_defaultCredentials = True Then
                defcred_cb.Checked = True
            Else
                defcred_cb.Checked = False
                username_tb.Text = NCFramework.My.MySettings.Default.proxy_uname
                password_tb.Text = NCFramework.My.MySettings.Default.proxy_pass
            End If
        End If
    End Sub

    Private Sub defcred_cb_CheckedChanged(sender As Object, e As EventArgs) Handles defcred_cb.CheckedChanged
        If defcred_cb.Checked = False Then
            Label3.Enabled = True
            Label4.Enabled = True
            username_tb.Enabled = True
            password_tb.Enabled = True
        Else
            Label3.Enabled = False
            Label4.Enabled = False
            username_tb.Enabled = False
            password_tb.Enabled = False
        End If
    End Sub

    Private Sub cat_id_92_bt_Click(sender As Object, e As EventArgs) Handles apply_bt.Click
     
        Try
            If noproxy_radio.Checked = True Then
                NCFramework.My.MySettings.Default.proxy_enabled = False
            ElseIf detectproxy_radio.Checked = True Then
                NCFramework.My.MySettings.Default.proxy_defaultCredentials = True
                NCFramework.My.MySettings.Default.proxy_enabled = True
                NCFramework.My.MySettings.Default.proxy_detect = True
                Dim webnet As New WebConnection
                Dim servername As String = webnet.GetProxyServerName()
                Dim serverport As String = webnet.GetProxyServerPort()
                If serverport Is Nothing Then
                    NCFramework.My.MySettings.Default.proxy_enabled = False
                Else
                    If servername Is Nothing Then
                        NCFramework.My.MySettings.Default.proxy_enabled = False
                    Else
                        NCFramework.My.MySettings.Default.proxy_host = servername
                        NCFramework.My.MySettings.Default.proxy_port = TryInt(serverport)
                        NCFramework.My.MySettings.Default.fullproxy = New WebProxy(servername & ":" & serverport)
                    End If
                End If
            ElseIf manualproxy_radio.Checked = True Then
                If url_tb.Text = "" Then
                    Exit Sub
                End If
                NCFramework.My.MySettings.Default.proxy_detect = False
                NCFramework.My.MySettings.Default.proxy_enabled = True
                NCFramework.My.MySettings.Default.proxy_host = url_tb.Text
                NCFramework.My.MySettings.Default.proxy_port = port_ud.Value
                NCFramework.My.MySettings.Default.fullproxy = New WebProxy(url_tb.Text & ":" & port_ud.Value.ToString())
                If defcred_cb.Checked = True Then
                    NCFramework.My.MySettings.Default.proxy_defaultCredentials = True
                Else
                    NCFramework.My.MySettings.Default.proxy_defaultCredentials = False
                    NCFramework.My.MySettings.Default.proxy_uname = username_tb.Text
                    NCFramework.My.MySettings.Default.proxy_pass = password_tb.Text
                End If
            End If

            Dim enc As New System.Text.UnicodeEncoding
            Dim XMLobj As Xml.XmlTextWriter = New Xml.XmlTextWriter("settings.xml", enc)

            With XMLobj
                .Formatting = Xml.Formatting.Indented
                .Indentation = 3
                .WriteStartDocument()
                .WriteStartElement("Proxy")
                .WriteAttributeString("enabled", NCFramework.My.MySettings.Default.proxy_enabled.ToString())
                .WriteAttributeString("defaultCredentials", NCFramework.My.MySettings.Default.proxy_defaultCredentials.ToString())
                .WriteAttributeString("autodetect", NCFramework.My.MySettings.Default.proxy_detect.ToString())
                .WriteAttributeString("host", NCFramework.My.MySettings.Default.proxy_host)
                .WriteAttributeString("port", NCFramework.My.MySettings.Default.proxy_port.ToString)
                .WriteAttributeString("uname", NCFramework.My.MySettings.Default.proxy_uname)
                .WriteAttributeString("pass", NCFramework.My.MySettings.Default.proxy_pass)
                .WriteEndElement()
                .Close()

            End With

            NCFramework.My.MySettings.Default.Save()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles close_bt.Click
        Me.Close()
    End Sub

   
End Class