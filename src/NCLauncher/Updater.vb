'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013 NamCore Studio <https://github.com/megasus/Namcore-Studio>
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
'*      /Filename:      Updater
'*      /Description:   Updater
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.Net
Imports System.Xml
Imports System.IO
Imports System.Text

Public Class Updater

    '// Declaration
    Private Const VersionUrl As String = "http://wowgeslauncher.bplaced.com/filemanager/namcore/ncversion.html"
    Private ReadOnly _client As New WebClient
    Private _ptMouseDownLocation As Point
    Private _files2Download As List(Of MyFile)
    Private _totalSize As Integer = 0
    '// Declaration

    Private Sub me_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        If e.Button = MouseButtons.Left Then
            _ptMouseDownLocation = e.Location
        End If
    End Sub

    Private Sub me_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        If e.Button = MouseButtons.Left Then
            Location = New Point(e.Location.X - _ptMouseDownLocation.X + Location.X, e.Location.Y - _ptMouseDownLocation.Y + Location.Y)
        End If
    End Sub

    Private Sub highlighter_MouseEnter(sender As Object, e As EventArgs) _
        Handles highlighter1.MouseEnter, highlighter2.MouseEnter
        sender.backgroundimage = My.Resources.highlight1
    End Sub

    Private Sub highlighter_MouseLeave(sender As Object, e As EventArgs) _
        Handles highlighter1.MouseLeave, highlighter2.MouseLeave
        sender.backgroundimage = Nothing
    End Sub

    Private Sub highlighter2_Click(sender As Object, e As EventArgs) Handles highlighter2.Click
        Close()
    End Sub

    Private Sub highlighter1_Click(sender As Object, e As EventArgs) Handles highlighter1.Click
        WindowState = FormWindowState.Minimized
    End Sub

    Private Sub header_MouseDown(sender As Object, e As MouseEventArgs) Handles header.MouseDown
        If e.Button = MouseButtons.Left Then
            _ptMouseDownLocation = e.Location
        End If
    End Sub

    Private Sub header_MouseMove(sender As Object, e As MouseEventArgs) Handles header.MouseMove
        If e.Button = MouseButtons.Left Then
            Location = New Point(e.Location.X - _ptMouseDownLocation.X + Location.X, e.Location.Y - _ptMouseDownLocation.Y + Location.Y)
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
            Dim xmlReader As XmlReader = New XmlTextReader(Application.StartupPath & "\Data\settings.xml")
            With xmlReader
                Do While .Read
                    Select Case .NodeType
                        Case XmlNodeType.Element
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
                        Case XmlNodeType.Text
                        Case XmlNodeType.Comment
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
                            fullProxy = New WebProxy(servername & ":" & serverport)
                        End If
                    End If
                End If
            Else
                Dim servername As String = host
                Dim serverport As String = port.ToString()
                If serverport Is Nothing Then
                    proxyenabled = False
                Else
                    If servername Is Nothing Then
                        proxyenabled = False
                    ElseIf servername = "" Then
                        proxyenabled = False
                    Else
                        fullProxy = New WebProxy(servername & ":" & serverport)
                    End If
                End If
            End If
            Try
                If proxyenabled = True Then
                    _client.Proxy = fullProxy
                    If defaultCredentials = False Then
                        _client.Proxy.Credentials = New NetworkCredential(uname, pass)
                    Else
                        _client.Proxy.Credentials = CredentialCache.DefaultCredentials
                    End If
                End If
            Catch ex As Exception

            End Try
        End If
        Dim source As String = ""
        Try
            source = _client.DownloadString(VersionUrl)
        Catch ex As Exception
            MsgBox(
                "Webclient connection failed! NamCore Studio requires a constant internet connection. Please make sure you have access to the internet." &
                vbNewLine &
                "If your computer is behind a proxy server, please configure NamCore Studio by clicking 'Settings' on the upcoming interface",
                MsgBoxStyle.Exclamation, "Failed to connect")
        End Try
        If Not source = "" Then
            Dim xmlReader As XmlReader = New XmlTextReader(Application.StartupPath & "\Data\settings.xml")
            With xmlReader
                Do While .Read
                    Select Case .NodeType
                        Case XmlNodeType.Element
                            If .AttributeCount > 0 Then
                                While .MoveToNextAttribute
                                    Select Case .Name
                                        Case "version"
                                    End Select
                                End While
                            End If
                        Case XmlNodeType.Text
                        Case XmlNodeType.Comment
                    End Select
                Loop
                .Close()
            End With
            Dim aioversion As Integer = CInt(splitString(source, "<aioversion>", "</aioversion>"))
            _files2Download = New List(Of MyFile)()
            If aioversion = 10000 Then 'deactivated > myaioversion Then
                'Updates available!
                Dim fileCount As Integer = UBound(Split(source, "<file>"))
                Dim fileCounter As Integer = 0
                Dim fileSource As String = source
                Do
                    fileCounter += 1
                    Dim fileContext As String = splitString(fileSource, "<file>", "</file>")
                    Try
                        Dim fname As String = splitString(fileContext, "<name>", "</name>")
                        Dim version As Integer = CInt(splitString(fileContext, "<version>", "</version>").Replace(".",
                                                                                                                  ""))
                        Dim path As String = splitString(fileContext, "<path>", "</path>")
                        Dim url As String = splitString(fileContext, "<url>", "</url>")
                        Dim mysize As String = splitString(fileContext, "<size>", "</size>")
                        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\" & path & fname) Then
                            Dim gfi As GetFileInformation =
                                    New GetFileInformation(Application.StartupPath & "\" & path & fname)
                            Dim fVersion As Integer = CInt(gfi.GetFileVersion.ToString().Replace(".", ""))
                            If version > fVersion Then
                                _totalSize += mysize
                                _files2Download.Add(New MyFile _
                                                      With {.name = fname, .path = path, .url = url, .size = mysize})
                            End If
                        Else
                            _totalSize += mysize
                            _files2Download.Add(New MyFile With {.name = fname, .path = path, .url = url, .size = mysize})
                        End If
                    Finally
                        fileSource = fileSource.Replace("<file>" & fileContext & "</file>", "")
                    End Try
                Loop Until fileCounter = fileCount
                filestatus.Text = "Loading file 1/" & _files2Download.Count.ToString()
                globalprogress_lbl.Text = "0 KB / " & _totalSize.ToString() & " KB"
                subprogress_lbl.Text = "0 KB / " & _files2Download.Item(0).size.ToString() & " KB"
                currentfile.Text = _files2Download.Item(0).name.ToString()
            Else
                If Not My.Computer.FileSystem.FileExists(Application.StartupPath & "\Data\NamCore Studio.exe") Then
                Else
                    Process.Start(Application.StartupPath & "\Data\NamCore Studio.exe")
                    Close()
                End If
            End If

        Else
            If Not My.Computer.FileSystem.FileExists(Application.StartupPath & "\Data\NamCore Studio.exe") Then
            Else
                Process.Start(Application.StartupPath & "\Data\NamCore Studio.exe")
                Close()
            End If
        End If
    End Sub

    Public Function SplitString(ByVal source As String, ByVal start As String, ByVal ending As String) As String
        If source Is Nothing Or start Is Nothing Or ending Is Nothing Then

            Return Nothing
        End If

        Try
            Dim quellcode As String = source
            Dim mystart As String = start
            Dim myend As String = ending
            Dim quellcodeSplit As String
            quellcodeSplit = Split(quellcode, mystart, 5)(1)
            Return Split(quellcodeSplit, myend, 6)(0)
        Catch ex As Exception

            Return Nothing
        End Try
    End Function

    Private Function GetProxyServerName() As String

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


    Private Function GetProxyServerPort() As String

        ' ReSharper disable UnusedVariable
        Dim useProxy As New WebProxy()
        ' ReSharper restore UnusedVariable

        Try 'if no proxy is specified, an exception is 
            'thrown by the frameworks and must be caught

            ' ReSharper disable VBWarnings::BC40008
            Return useProxy.GetDefaultProxy.Address.Port
            ' ReSharper restore VBWarnings::BC40008

        Catch 'catch the error when no proxy is specified in IE

            Return Nothing

        End Try
    End Function

    Private Sub start_bt_Click(sender As Object, e As EventArgs) Handles start_bt.Click
        globalprogress_bar.Maximum = _totalSize
        Const cnt As Integer = 1
        For Each dFile As MyFile In _files2Download
            filestatus.Text = "Loading file " & cnt.ToString & "/" & _files2Download.Count.ToString()
            currentfile.Text = dFile.name
            DownloadItem(dFile.url, dFile.name & ".temp", Application.StartupPath & "\" & dFile.path)
            delete(dFile.path & dFile.name)
            My.Computer.FileSystem.RenameFile(Application.StartupPath & "\" & dFile.path & dFile.name & ".temp",
                                              dFile.name)
        Next
    End Sub

    Private Sub delete(ByVal path As String)
        File.Delete(Application.StartupPath & "\" & path)
    End Sub

    Private Sub DownloadItem(ByVal sUrl As String,
                             ByVal strFile As String, ByVal strFolder As String)

        Dim webreq As HttpWebRequest
        Dim webresp As HttpWebResponse
        Dim bReader As BinaryReader
        Dim bWriter As BinaryWriter
        Dim stream As FileStream
        Dim buffer() As Byte = New Byte(1024) {}
        Dim bytesRead As Integer
        subprogress_bar.Value = 0

        Try
            ' Datei-Download via HTTP "anfordern"
            webreq = HttpWebRequest.Create(sURL)
            webresp = webreq.GetResponse

            ' Download-Größe
            subprogress_bar.Maximum = webresp.ContentLength

            ' lokale Datei öffnen
            stream = New FileStream(Application.StartupPath & strFolder & "\" & strFile, FileMode.Create)
            bReader = New BinaryReader(webresp.GetResponseStream)
            bWriter = New BinaryWriter(stream)

            ' Datei blockweise downloaden und lokal speichern
            Dim qSplit2 As String = SplitString("A" & (webresp.ContentLength / 1000).ToString(), "A", ",")
            Do
                bytesRead = bReader.Read(buffer, 0, 1024)
                bWriter.Write(buffer, 0, bytesRead)
                subprogress_bar.Value += bytesRead
                globalprogress_bar.Value += bytesRead
                Dim qSplit As String = SplitString("A" & (subprogress_bar.Value.ToString / 1000).ToString(), "A", ",")
                subprogress_lbl.Text = qSplit & " KB" & " / " & qSplit2 & " KB"
                globalprogress_lbl.Text = subprogress_bar.Value.ToString() & " KB" & " / " &
                                          subprogress_bar.Maximum.ToString() & " KB"
                subprogress_lbl.Update()
                subprogress_bar.Update()
                globalprogress_lbl.Update()
                globalprogress_bar.Update()
            Loop Until bytesRead = 0

            ' alle Dateien schließen
            bWriter.Close()
            bReader.Close()
            stream.Close()

        Catch ex As Exception

        End Try
    End Sub
End Class

Public Class GetFileInformation
    Implements IDisposable

    Private ReadOnly _fvi As FileVersionInfo

    Public Sub New(ByVal filename As String)
        _fvi = FileVersionInfo.GetVersionInfo(filename)
    End Sub

    Public Overrides Function ToString() As String
        Dim sb As New StringBuilder

        sb.Append("\n\tGetFilename:		" & GetFilename())
        sb.Append("\n\tGetFileDescription:	" & GetFileDescription())
        sb.Append("\n\tGetFileVersion:	" & GetFileVersion())
        sb.Append("\n\tGetCompanyName:	" & GetCompanyName())
        sb.Append("\n\tGetLanguage:	" & GetLanguage())
        sb.Append("\n\tGetProductName:	" & GetProductName())
        sb.Append("\n\tGetInternalName:	" & GetInternalName())
        sb.Append("\n\tGetComments:	" & GetComments())
        sb.Replace("\n", vbCrLf)
        sb.Replace("\tGet", vbTab)
        Return sb.ToString()
    End Function

    Public Function GetFilename() As String
        Return _fvi.FileName
    End Function

    Public Function GetFileDescription() As String
        Return _fvi.FileDescription
    End Function

    Public Function GetFileVersion() As String
        Return _fvi.FileVersion
    End Function

    Public Function GetCompanyName() As String
        Return _fvi.CompanyName
    End Function

    Public Function GetLanguage() As String
        Return _fvi.Language
    End Function

    Public Function GetProductName() As String
        Return _fvi.ProductName
    End Function

    Public Function GetInternalName() As String
        Return _fvi.InternalName
    End Function

    Public Function GetComments() As String
        Return _fvi.Comments
    End Function

    Public Sub Dispose() Implements IDisposable.Dispose
        '
    End Sub
End Class

Public Class MyFile
    Public Name As String
    Public Url As String
    Public Path As String
    Public Size As Integer

    Sub New()
    End Sub
End Class