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
'*      /Filename:      Updater
'*      /Description:   Updater
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.Net
Imports System.Xml
Imports System.IO
Imports System.Text
Imports System.Threading
Imports Microsoft.SqlServer.Server

Public Class Updater
    '// Declaration
    Private Const VersionUrl As String = "http://wowgeslauncher.bplaced.com/filemanager/namcore/ncversion.html"
    Private ReadOnly _client As New WebClient
    Private _ptMouseDownLocation As Point
    Private _files2Download As List(Of MyFile)
    Private _totalSize As Integer = 0
    Private _downloadedBytes As Double
    Private _lastValue As Integer = 0
    '// Declaration

    Private Sub me_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        If e.Button = MouseButtons.Left Then
            _ptMouseDownLocation = e.Location
        End If
    End Sub

    Private Sub me_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        If e.Button = MouseButtons.Left Then
            Location = New Point(e.Location.X - _ptMouseDownLocation.X + Location.X,
                                 e.Location.Y - _ptMouseDownLocation.Y + Location.Y)
        End If
    End Sub

    Private Sub highlighter_MouseEnter(sender As Object, e As EventArgs) _
        Handles highlighter1.MouseEnter, highlighter2.MouseEnter
        CType(sender, PictureBox).BackgroundImage = My.Resources.highlight1
    End Sub

    Private Sub highlighter_MouseLeave(sender As Object, e As EventArgs) _
        Handles highlighter1.MouseLeave, highlighter2.MouseLeave
        CType(sender, PictureBox).BackgroundImage = Nothing
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
            Location = New Point(e.Location.X - _ptMouseDownLocation.X + Location.X,
                                 e.Location.Y - _ptMouseDownLocation.Y + Location.Y)
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
        Dim myaioversion As Integer
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
                .Close()
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
            Dim xmlReader As XmlReader = New XmlTextReader(Application.StartupPath & "\Data\version.xml")
            With xmlReader
                Do While .Read
                    Select Case .NodeType
                        Case XmlNodeType.Element
                            If .AttributeCount > 0 Then
                                While .MoveToNextAttribute
                                    Select Case .Name
                                        Case "aioversion"
                                            myaioversion = CInt(.Value)
                                    End Select
                                End While
                            End If
                        Case XmlNodeType.Text
                        Case XmlNodeType.Comment
                    End Select
                Loop
                .Close()
            End With
            Dim aioversion As Integer = CInt(SplitString(source, "<aioversion>", "</aioversion>"))
            _files2Download = New List(Of MyFile)()
            If aioversion > myaioversion Then
                'Updates available!
                Dim fileCount As Integer = UBound(Split(source, "<file>"))
                Dim fileCounter As Integer = 0
                Dim fileSource As String = source
                Do
                    fileCounter += 1
                    Dim fileContext As String = SplitString(fileSource, "<file>", "</file>")
                    Try
                        Dim fname As String = SplitString(fileContext, "<name>", "</name>")
                        Dim version As Integer = CInt(SplitString(fileContext, "<build>", "</build>"))
                        Dim path As String = SplitString(fileContext, "<targetdir>", "</targetdir>")
                        Dim url As String = SplitString(fileContext, "<location>", "</location>")
                        Dim mysize As String = SplitString(fileContext, "<size>", "</size>")
                        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\" & path & fname) Then
                            Dim gfi As GetFileInformation =
                                    New GetFileInformation(Application.StartupPath & "\" & path & fname)
                            Dim fVersionFile As String = gfi.GetFileVersion
                            Dim fVersionStr() As String = fVersionFile.Split("."c)
                            Dim fVersion As Integer = CInt(fVersionStr(fVersionStr.Length - 1))
                            If version > fVersion Then
                                _totalSize += CInt(mysize)
                                _files2Download.Add(
                                    New MyFile _
                                                       With {.Name = fname, .Path = path, .Url = url,
                                                       .Size = CInt(mysize)})
                            End If
                        Else
                            _totalSize += CInt(mysize)
                            _files2Download.Add(New MyFile _
                                                   With {.Name = fname, .Path = path, .Url = url, .Size = CInt(mysize)})
                        End If
                    Finally
                        fileSource = fileSource.Replace("<file>" & fileContext & "</file>", "")
                    End Try
                Loop Until fileCounter = fileCount
                If Not _files2Download.Count = 0 Then
                    filestatus.Text = "Loading file 1/" & _files2Download.Count.ToString()
                    globalprogress_lbl.Text = "0 KB / " & _totalSize.ToString() & " KB"
                    subprogress_lbl.Text = "0 KB / " & _files2Download.Item(0).Size.ToString() & " KB"
                    currentfile.Text = _files2Download.Item(0).Name.ToString()
                End If
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
            Return CStr(useProxy.GetDefaultProxy.Address.Port)
            ' ReSharper restore VBWarnings::BC40008

        Catch 'catch the error when no proxy is specified in IE

            Return Nothing

        End Try
    End Function

    Private Sub start_bt_Click(sender As Object, e As EventArgs) Handles start_bt.Click
        globalprogress_bar.Maximum = _totalSize
        Dim trd As Thread = New Thread(DirectCast(Sub() StartDownloading(), ThreadStart))
        trd.Start()
    End Sub

    Private Sub StartDownloading()
        Dim cnt As Integer = 1
        GetTotalSize()
        For Each dFile As MyFile In _files2Download
            filestatus.BeginInvoke(New ChangeLabelText(AddressOf DelegateLabelTextChange), filestatus, "Loading file " & cnt.ToString & "/" & _files2Download.Count.ToString())
            currentfile.BeginInvoke(New ChangeLabelText(AddressOf DelegateLabelTextChange), currentfile, dFile.Name)
            DownloadItem(dFile.Url, dFile.Name & ".temp", Application.StartupPath & "\" & dFile.Path)
            delete(dFile.Path & dFile.Name)
            My.Computer.FileSystem.RenameFile(Application.StartupPath & "\" & dFile.Path & dFile.Name & ".temp",
                                              dFile.Name)
            cnt += 1
        Next
        If Not My.Computer.FileSystem.FileExists(Application.StartupPath & "\Data\NamCore Studio.exe") Then
        Else
            Process.Start(Application.StartupPath & "\Data\NamCore Studio.exe")
            Close()
        End If
    End Sub

    Delegate Sub ChangeLabelText(lbl As Label, txt As String)

    Private Sub DelegateLabelTextChange(lbl As Label, txt As String)
        lbl.Text = txt
        subprogress_lbl.Update()
        subprogress_bar.Update()
        globalprogress_lbl.Update()
        globalprogress_bar.Update()
        Application.DoEvents()
    End Sub

    Delegate Sub ChangeStatusValue(bar As ProgressBar, val As Integer)

    Private Sub DelegateStatusChange(bar As ProgressBar, val As Integer)
        Try
            If val = 0 Then bar.Value = 0
            bar.Value += val
        Catch ex As Exception

        End Try

    End Sub

    Delegate Sub ChangeStatusValueExpl(bar As ProgressBar, val As Integer)

    Private Sub DelegateStatusChangeExpl(bar As ProgressBar, val As Integer)
        Try
            bar.Value = val
        Catch ex As Exception

        End Try

    End Sub
    Delegate Sub ChangeStatusMax(bar As ProgressBar, val As Integer)

    Private Sub DelegateStatusMaxChange(bar As ProgressBar, val As Integer)
        Try
            bar.Maximum = val
        Catch ex As Exception

        End Try

    End Sub
    Private Sub delete(ByVal path As String)
        File.Delete(Application.StartupPath & "\" & path)
    End Sub

    Private Sub GetTotalSize()
        Dim webreq As HttpWebRequest
        Dim webresp As HttpWebResponse
        Dim max As Integer = 0
        Try
            ' Datei-Download via HTTP "anfordern"
            For Each onlineFile As MyFile In _files2Download
                webreq = CType(HttpWebRequest.Create(onlineFile.Url), HttpWebRequest)
                webresp = CType(webreq.GetResponse, HttpWebResponse)

                ' Download-Größe
                max += CInt(webresp.ContentLength)
                webreq.Abort()
                webresp.Close()
            Next
            globalprogress_bar.BeginInvoke(New ChangeStatusMax(AddressOf DelegateStatusMaxChange), globalprogress_bar, max)
        Catch

        End Try
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
        subprogress_bar.BeginInvoke(New ChangeStatusValue(AddressOf DelegateStatusChange), subprogress_bar, 0)
        Try
            ' Datei-Download via HTTP "anfordern"
            webreq = CType(HttpWebRequest.Create(sUrl), HttpWebRequest)
            webresp = CType(webreq.GetResponse, HttpWebResponse)

            ' Download-Größe
            subprogress_bar.BeginInvoke(New ChangeStatusMax(AddressOf DelegateStatusMaxChange), subprogress_bar, CInt(webresp.ContentLength))

            ' lokale Datei öffnen
            stream = New FileStream(strFolder & "\" & strFile, FileMode.Create)
            bReader = New BinaryReader(webresp.GetResponseStream)
            bWriter = New BinaryWriter(stream)

            ' Datei blockweise downloaden und lokal speichern
            Dim qSplit2 As String = SplitString("A" & (webresp.ContentLength / 1000).ToString(), "A", ",")
            Dim speedTimer As New Stopwatch
            Dim currentspeed As Double = -1
            Dim readings As Integer = 0
            Const circle As Integer = 500
            If _lastValue > 0 Then
                globalprogress_bar.BeginInvoke(New ChangeStatusValueExpl(AddressOf DelegateStatusChangeExpl), globalprogress_bar, _lastValue)
            End If
            Do
                speedTimer.Start()
                bytesRead = bReader.Read(buffer, 0, 1024)
                bWriter.Write(buffer, 0, bytesRead)
                speedTimer.Stop()
                subprogress_bar.BeginInvoke(New ChangeStatusValue(AddressOf DelegateStatusChange), subprogress_bar, bytesRead)
                globalprogress_bar.BeginInvoke(New ChangeStatusValue(AddressOf DelegateStatusChange), globalprogress_bar, bytesRead)
                Dim qSplit As String = SplitString("A" & (subprogress_bar.Value / 1000).ToString(), "A", ",")
                subprogress_lbl.BeginInvoke(New ChangeLabelText(AddressOf DelegateLabelTextChange), subprogress_lbl, TausenderPunkte(CDbl(qSplit)) & " KB" & " / " & TausenderPunkte(CDbl(qSplit2)) & " KB")
                globalprogress_lbl.BeginInvoke(New ChangeLabelText(AddressOf DelegateLabelTextChange), globalprogress_lbl, TausenderPunkte(CDbl(Math.Round((_downloadedBytes + subprogress_bar.Value) / 1000, 0))) & " KB" & " / " &
                                          TausenderPunkte(CDbl(Math.Round(globalprogress_bar.Maximum / 1000, 0))) & " KB")
                readings += 1
                If readings >= circle Then 'For increase precision, the speed it's calculated only every five cicles
                    currentspeed = Math.Round((1024 * circle / (speedTimer.ElapsedMilliseconds)), 2)
                    speedTimer.Reset()
                    readings = 0
                    speed.BeginInvoke(New ChangeLabelText(AddressOf DelegateLabelTextChange), speed, currentspeed.ToString() & " KB/s")
                End If
            Loop Until bytesRead = 0
            _downloadedBytes += subprogress_bar.Value
            _lastValue = globalprogress_bar.Value
            ' alle Dateien schließen
            bWriter.Close()
            bReader.Close()
            stream.Close()

        Catch ex As Exception

        End Try
    End Sub


    Private Function TausenderPunkte(Zahl As Double) As String
        Try
            Dim res As String = Zahl.ToString()
            Dim append As String = ""
            If res.Contains(",") Then
                append = res.Substring(res.IndexOf(","))
                res = res.Substring(0, res.IndexOf(","))
            End If
            Dim pkte As Integer = (res.Length - 1) \ 3
            Dim pre As String = res.Substring(0, res.Length Mod 3)
            res = res.Substring(res.Length Mod 3)
            For i = pkte - 1 To 0 Step -1
                res = res.Substring(0, i * 3) & "." & res.Substring(i * 3)
            Next
            Return pre & res & append
        Catch ex As Exception
            Return ""
        End Try

    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Not My.Computer.FileSystem.FileExists(Application.StartupPath & "\Data\NamCore Studio.exe") Then
        Else
            Process.Start(Application.StartupPath & "\Data\NamCore Studio.exe")
            Close()
        End If
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