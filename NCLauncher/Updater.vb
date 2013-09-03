Imports System.Net
Imports System.IO
Public Class Updater
    Private version_url As String = "http://wowgeslauncher.bplaced.com/filemanager/namcore/ncversion.html"
    Private client As New WebClient
    Private ptMouseDownLocation As Point
    Private files2Download As List(Of MyFile)
    Private totalSize As Integer = 0
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
                        client.Proxy.Credentials = New NetworkCredential(uname, pass)
                    Else
                        client.Proxy.Credentials = CredentialCache.DefaultCredentials
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
            files2Download = New List(Of MyFile)()
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
                        Dim version As Integer = CInt(splitString(fileContext, "<version>", "</version>").Replace(".", ""))
                        Dim path As String = splitString(fileContext, "<path>", "</path>")
                        Dim url As String = splitString(fileContext, "<url>", "</url>")
                        Dim size As String = splitString(fileContext, "<size>", "</size>")
                        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\" & path & fname) Then
                            Dim gfi As GetFileInformation = New GetFileInformation(Application.StartupPath & "\" & path & fname)
                            Dim f_version As Integer = CInt(gfi.GetFileVersion.ToString().Replace(".", ""))
                            If version > f_version Then
                                totalSize += size
                                files2Download.Add(New MyFile With {.name = fname, .path = path, .url = url, .size = size})
                            End If
                        Else
                            totalSize += size
                            files2Download.Add(New MyFile With {.name = fname, .path = path, .url = url, .size = size})
                        End If
                    Finally
                        fileSource = fileSource.Replace("<file>" & fileContext & "</file>", "")
                    End Try
                Loop Until fileCounter = fileCount
                filestatus.Text = "Loading file 1/" & files2Download.Count.ToString()
                globalprogress_lbl.Text = "0 KB / " & totalSize.ToString() & " KB"
                subprogress_lbl.Text = "0 KB / " & files2Download.Item(0).size.ToString() & " KB"
                currentfile.Text = files2Download.Item(0).name.ToString()
            Else
                If Not My.Computer.FileSystem.FileExists(Application.StartupPath & "\Data\Namcore Studio.exe") Then
                Else
                    Process.Start(Application.StartupPath & "\Data\Namcore Studio.exe")
                    Me.Close()
                End If
            End If

        Else
            If Not My.Computer.FileSystem.FileExists(Application.StartupPath & "\Data\Namcore Studio.exe") Then
            Else
                Process.Start(Application.StartupPath & "\Data\Namcore Studio.exe")
                Me.Close()
            End If
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

    Private Sub start_bt_Click(sender As Object, e As EventArgs) Handles start_bt.Click
        globalprogress_bar.Maximum = totalSize
        Dim cnt As Integer = 1
        For Each dFile As MyFile In files2Download
            filestatus.Text = "Loading file " & cnt.ToString & "/" & files2Download.Count.ToString()
            currentfile.Text = dFile.name
            DownloadItem(dFile.url, dFile.name & ".temp", Application.StartupPath & "\" & dFile.path)
            delete(dFile.path & dFile.name)
            My.Computer.FileSystem.RenameFile(Application.StartupPath & "\" & dFile.path & dFile.name & ".temp", dFile.name)
        Next
    End Sub
    Private Sub delete(ByVal path As String)
        System.IO.File.Delete(Application.StartupPath & "\" & path)
    End Sub
    Private Sub DownloadItem(ByVal sURL As String, _
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
            Dim qSplit2 As String = splitString("A" & (webresp.ContentLength / 1000).ToString(), "A", ",")
            Do
                bytesRead = bReader.Read(buffer, 0, 1024)
                bWriter.Write(buffer, 0, bytesRead)
                subprogress_bar.Value += bytesRead
                globalprogress_bar.Value += bytesRead
                Dim qSplit As String = splitString("A" & (subprogress_bar.Value.ToString / 1000).ToString(), "A", ",")
                subprogress_lbl.Text = qSplit & " KB" & " / " & qSplit2 & " KB"
                globalprogress_lbl.Text = subprogress_bar.Value.ToString() & " KB" & " / " & subprogress_bar.Maximum.ToString() & " KB"
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

    Private fvi As System.Diagnostics.FileVersionInfo

    Public Sub New(ByVal Filename As String)
        fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(Filename)
    End Sub

    Public Overrides Function ToString() As String
        Dim sb As New System.Text.StringBuilder

        sb.Append("\n\tGetFilename:		" & Me.GetFilename)
        sb.Append("\n\tGetFileDescription:	" & Me.GetFileDescription)
        sb.Append("\n\tGetFileVersion:	" & Me.GetFileVersion)
        sb.Append("\n\tGetCompanyName:	" & Me.GetCompanyName)
        sb.Append("\n\tGetLanguage:	" & Me.GetLanguage)
        sb.Append("\n\tGetProductName:	" & Me.GetProductName)
        sb.Append("\n\tGetInternalName:	" & Me.GetInternalName)
        sb.Append("\n\tGetComments:	" & Me.GetComments)
        sb.Replace("\n", vbCrLf)
        sb.Replace("\tGet", vbTab)
        Return sb.ToString()
    End Function

    Public Function GetFilename() As String
        Return fvi.FileName
    End Function

    Public Function GetFileDescription() As String
        Return fvi.FileDescription
    End Function

    Public Function GetFileVersion() As String
        Return fvi.FileVersion
    End Function

    Public Function GetCompanyName() As String
        Return fvi.CompanyName
    End Function

    Public Function GetLanguage() As String
        Return fvi.Language
    End Function

    Public Function GetProductName() As String
        Return fvi.ProductName
    End Function

    Public Function GetInternalName() As String
        Return fvi.InternalName
    End Function

    Public Function GetComments() As String
        Return fvi.Comments
    End Function

    Public Sub Dispose() Implements System.IDisposable.Dispose
        '
    End Sub

End Class

Public Class MyFile
    Public name As String
    Public url As String
    Public path As String
    Public size As Integer
    Sub New()

    End Sub
End Class