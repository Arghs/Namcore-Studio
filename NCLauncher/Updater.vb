Imports System.Net

Public Class Updater
    Private version_url As String = "http://wowgeslauncher.bplaced.com/filemanager/namcore/ncversion.html"
    Private client As New WebClient
    Private ptMouseDownLocation As Point
    Private Sub me_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            ptMouseDownLocation = e.Location
        End If
    End Sub

    Private Sub me_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me.Location = e.Location - ptMouseDownLocation + Location
        End If
    End Sub
    Private Sub highlighter_MouseEnter(sender As Object, e As EventArgs) Handles highlighter1.MouseEnter, highlighter2.MouseEnter
        sender.backgroundimage = My.Resources.highlight1
    End Sub

    Private Sub highlighter_MouseLeave(sender As Object, e As EventArgs) Handles highlighter1.MouseLeave, highlighter2.MouseLeave
        sender.backgroundimage = Nothing
    End Sub

    Private Sub highlighter2_Click(sender As Object, e As EventArgs) Handles highlighter2.Click
        Me.Close()
    End Sub

    Private Sub highlighter1_Click(sender As Object, e As EventArgs) Handles highlighter1.Click
        WindowState = FormWindowState.Minimized
    End Sub

    Private Sub header_MouseDown(sender As Object, e As MouseEventArgs) Handles header.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            ptMouseDownLocation = e.Location
        End If
    End Sub


    Private Sub header_MouseMove(sender As Object, e As MouseEventArgs) Handles header.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me.Location = e.Location - ptMouseDownLocation + Location
        End If
    End Sub

    Private Sub Updater_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim proxyenabled As Boolean = False
        Dim defaultCredentials As Boolean = True
        Dim autodetect As Boolean = False
        Dim host As String = ""
        Dim port As Integer = 80
        Dim uname As String = ""
        Dim pass As String = ""
        Dim fullProxy As New WebProxy
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Data\settings.xml") Then
            Dim XMLReader As Xml.XmlReader = New Xml.XmlTextReader(Application.StartupPath & "\Data\settings.xml")
            With XMLReader
                Do While .Read
                    Select Case .NodeType
                        Case Xml.XmlNodeType.Element
                            If .AttributeCount > 0 Then
                                While .MoveToNextAttribute
                                    Select Case .Name
                                        Case "enabled"
                                            If .Value = "true" Then
                                                proxyenabled = True
                                            Else
                                                proxyenabled = False
                                            End If
                                        Case "defaultCredentials"
                                            If .Value = "true" Then
                                                defaultCredentials = True
                                            Else
                                                defaultCredentials = False
                                            End If
                                        Case "autodetect"
                                            If .Value = "true" Then
                                                autodetect = True
                                            Else
                                                autodetect = False
                                            End If
                                        Case "host"
                                            host = .Value
                                        Case "port"
                                            Try
                                                port = CInt(.Value)
                                            Catch ex As Exception
                                                port = 80
                                            End Try
                                        Case "uname"
                                            uname = .Value
                                        Case "pass"
                                            pass = .Value
                                    End Select
                                End While
                            End If
                        Case Xml.XmlNodeType.Text
                        Case Xml.XmlNodeType.Comment
                    End Select
                Loop
                .Close()  '
            End With
            If proxyenabled = True Then
                If autodetect = True Then
                    Dim servername As String = GetProxyServerName()
                    Dim serverport As String = GetProxyServerPort()
                    If serverport Is Nothing Then
                        proxyenabled = False
                    Else
                        If servername Is Nothing Then
                            proxyenabled = False
                        Else
                            host = servername
                            port = CInt(serverport)
                            fullProxy = New WebProxy(servername & ":" & serverport)
                        End If
                    End If
                End If
            End If
            Try
                If proxyenabled = True Then
                    client.Proxy = fullProxy
                    If defaultCredentials = False Then
                        client.Credentials = New NetworkCredential(uname, pass)
                    Else
                        client.Credentials = CredentialCache.DefaultCredentials
                    End If
                End If
            Catch ex As Exception

            End Try
        End If
        Dim source As String = ""
        Try
            source = client.DownloadString(version_url)
        Catch ex As Exception
            MsgBox("Webclient connection failed! NamCore Studio requires a constant internet connection. Please make sure you have access to the internet." &
                   vbNewLine & "If your computer is behind a proxy server, please configure NamCore Studio by clicking 'Settings' on the upcoming interface", MsgBoxStyle.Exclamation, "Failed to connect")
        End Try
        If Not source = "" Then
            Dim myaioversion As Integer = 0
            Dim XMLReader As Xml.XmlReader = New Xml.XmlTextReader(Application.StartupPath & "\Data\settings.xml")
            With XMLReader
                Do While .Read
                    Select Case .NodeType
                        Case Xml.XmlNodeType.Element
                            If .AttributeCount > 0 Then
                                While .MoveToNextAttribute
                                    Select Case .Name
                                        Case "version"
                                            myaioversion = CInt(.Value)
                                    End Select
                                End While
                            End If
                        Case Xml.XmlNodeType.Text
                        Case Xml.XmlNodeType.Comment
                    End Select
                Loop
                .Close()
            End With
            Dim aioversion As Integer = CInt(splitString(source, "<aioversion>", "</aioversion>"))
            If aioversion > myaioversion Then
                'Updates available!
            End If
        End If
        If Not My.Computer.FileSystem.FileExists(Application.StartupPath & "\Data\Namcore Studio.exe") Then
        Else
            Process.Start(Application.StartupPath & "\Data\Namcore Studio.exe")
            Me.Close()
        End If

    End Sub
    Public Function splitString(ByVal source As String, ByVal start As String, ByVal ending As String) As String
        If source Is Nothing Or start Is Nothing Or ending Is Nothing Then

            Return Nothing
        End If

        Try
            Dim quellcode As String = source
            Dim _start As String = start
            Dim _end As String = ending
            Dim quellcodeSplit As String
            quellcodeSplit = Split(quellcode, _start, 5)(1)
            Return Split(quellcodeSplit, _end, 6)(0)
        Catch ex As Exception

            Return Nothing
        End Try
    End Function
    Private Function GetProxyServerName() As String

        Dim UseProxy As New Net.WebProxy()
        Try 'if no proxy is specified, an exception is 
            'thrown by the frameworks and must be caught

            Return UseProxy.GetDefaultProxy.Address.Host

        Catch 'catch the error when no proxy is specified in IE

            Return Nothing

        End Try

    End Function


    Private Function GetProxyServerPort() As String

        Dim UseProxy As New Net.WebProxy()

        Try 'if no proxy is specified, an exception is 
            'thrown by the frameworks and must be caught

            Return UseProxy.GetDefaultProxy.Address.Port

        Catch 'catch the error when no proxy is specified in IE

            Return Nothing

        End Try

    End Function
End Class
