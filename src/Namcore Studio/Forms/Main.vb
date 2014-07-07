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
'*      /Filename:      Main
'*      /Description:   Main menu
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework
Imports NCFramework.Framework.Extension
Imports NCFramework.Framework.Functions
Imports NamCore_Studio.Modules.Interface
Imports NCFramework.My
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Modules
Imports Microsoft.Win32
Imports System.Text
Imports System.Xml
Imports System.Net

Namespace Forms
    Public Class Main
        '// Declaration
        Private Const Aioversion As Integer = 1
        Private _ptMouseDownLocation As Point
        '// Declaration
        Private Sub closeBt_MouseEnter(sender As Object, e As EventArgs) Handles highlighter4.MouseEnter
            TryCast(sender, PictureBox).BackgroundImage = My.Resources.bt_close_light
        End Sub

        Private Sub closeBt_MouseLeave(sender As Object, e As EventArgs) Handles highlighter4.MouseLeave
            TryCast(sender, PictureBox).BackgroundImage = My.Resources.bt_close
        End Sub

        Private Sub minimizeBt_MouseEnter(sender As Object, e As EventArgs) Handles highlighter5.MouseEnter
            TryCast(sender, PictureBox).BackgroundImage = My.Resources.bt_minimize_light
        End Sub

        Private Sub minimizeBt_MouseLeave(sender As Object, e As EventArgs) Handles highlighter5.MouseLeave
            TryCast(sender, PictureBox).BackgroundImage = My.Resources.bt_minimize
        End Sub

        Private Sub settingsBt_MouseEnter(sender As Object, e As EventArgs) Handles settings_pic.MouseEnter
            TryCast(sender, PictureBox).BackgroundImage = My.Resources.bt_settings_light
        End Sub

        Private Sub settingsBt_MouseLeave(sender As Object, e As EventArgs) Handles settings_pic.MouseLeave
            TryCast(sender, PictureBox).BackgroundImage = My.Resources.bt_settings
        End Sub

        Private Sub aboutBt_MouseEnter(sender As Object, e As EventArgs) Handles about_pic.MouseEnter
            TryCast(sender, PictureBox).BackgroundImage = My.Resources.bt_about_light
        End Sub

        Private Sub aboutBt_MouseLeave(sender As Object, e As EventArgs) Handles about_pic.MouseLeave
            TryCast(sender, PictureBox).BackgroundImage = My.Resources.bt_about
        End Sub

        Private Sub liveViewBt_MouseEnter(sender As Object, e As EventArgs) Handles highlighter1.MouseEnter
            TryCast(sender, PictureBox).BackgroundImage = My.Resources.bt_liveview_light
        End Sub

        Private Sub liveViewBt_MouseLeave(sender As Object, e As EventArgs) Handles highlighter1.MouseLeave
            TryCast(sender, PictureBox).BackgroundImage = Nothing
        End Sub

        Private Sub templateExplorerBt_MouseEnter(sender As Object, e As EventArgs) Handles highlighter2.MouseEnter
            TryCast(sender, PictureBox).BackgroundImage = My.Resources.bt_templateexplorer_light
        End Sub

        Private Sub templateExplorerBt_MouseLeave(sender As Object, e As EventArgs) Handles highlighter2.MouseLeave
            TryCast(sender, PictureBox).BackgroundImage = Nothing
        End Sub

        Private Sub armoryParserBt_MouseEnter(sender As Object, e As EventArgs) Handles highlighter3.MouseEnter
            TryCast(sender, PictureBox).BackgroundImage = My.Resources.bt_armoryparser_light
        End Sub

        Private Sub armoryParserBt_MouseLeave(sender As Object, e As EventArgs) Handles highlighter3.MouseLeave
            TryCast(sender, PictureBox).BackgroundImage = Nothing
        End Sub

        Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            Userwait.Show()
            Application.DoEvents()

#If CONFIG = "Debug" Then
            GlobalVariables.DebugMode = True
#End If
            If My.Settings.Registered = False Then
                Try
                    My.Computer.Registry.ClassesRoot.CreateSubKey(".ncsf") _
                        .SetValue("", "NamcoreStudioFile", RegistryValueKind.String)
                    My.Computer.Registry.ClassesRoot.CreateSubKey("NamcoreStudioFile\shell\open\command") _
                        .SetValue("", Application.ExecutablePath & " ""%l"" ", RegistryValueKind.String)
                Catch ex As Exception
                    LogAppend("Unable to register file extension - Check admin rights", "Main_Main_Load", True, True)
                End Try
                My.Settings.Registered = True
            End If
            My.Settings.Save()
            If GlobalVariables.DebugMode = True Then
                MySettings.Default.server_authdb = "arc_auth"
                MySettings.Default.server_chardb = "arc_characters"
                MySettings.Default.Save()
            End If
            If Not My.Computer.FileSystem.FileExists(Application.StartupPath & "\version.xml") Then
                Dim enc As New UnicodeEncoding
                Dim xmLobj As XmlTextWriter = New XmlTextWriter("version.xml", enc)
                With xmLobj
                    .Formatting = Formatting.Indented
                    .Indentation = 3
                    .WriteStartDocument()
                    .WriteStartElement("Common")
                    .WriteAttributeString("aioversion", Aioversion.ToString())
                    .WriteEndElement()
                    .Close()
                End With
            End If
            Dim controlLst As List(Of Control)
            controlLst = FindAllChildren()
            For Each itemControl As Control In controlLst
                itemControl.SetDoubleBuffered()
            Next
            Text = "NamCore Studio - Development - " & My.Application.Info.Version.ToString() & " - © megasus 2014"
            version_lbl.Text = "NamCore Studio - Development - " & My.Application.Info.Version.ToString() &
                               " - © megasus 2014"
            If GlobalVariables.DebugMode Then
                NewProcessStatus()
            End If
            GlobalVariables.lastregion = "main"
            If MySettings.Default.proxy_enabled = True Then
                If MySettings.Default.proxy_detect = True Then
                    Dim webnet As New WebConnection
                    Dim servername As String = webnet.GetProxyServerName()
                    Dim serverport As String = webnet.GetProxyServerPort()
                    If serverport Is Nothing Then
                        MySettings.Default.proxy_enabled = False
                    Else
                        If servername Is Nothing Then
                            MySettings.Default.proxy_enabled = False
                        Else
                            MySettings.Default.proxy_host = servername
                            MySettings.Default.proxy_port = TryInt(serverport)
                            MySettings.Default.fullproxy = New WebProxy(servername & ":" & serverport)
                        End If
                    End If
                Else
                    Dim servername As String = MySettings.Default.proxy_host
                    Dim serverport As String = MySettings.Default.proxy_port.ToString()
                    If serverport Is Nothing Then
                        MySettings.Default.proxy_enabled = False
                    Else
                        If servername Is Nothing Then
                            MySettings.Default.proxy_enabled = False
                        Else
                            MySettings.Default.proxy_host = servername
                            MySettings.Default.proxy_port = TryInt(serverport)
                            MySettings.Default.fullproxy = New WebProxy(servername & ":" & serverport)
                        End If
                    End If
                End If
            End If
            If My.Computer.FileSystem.FileExists(Application.StartupPath & "/EventLog.log") Then
                My.Computer.FileSystem.DeleteFile(Application.StartupPath & "/EventLog.log")
            End If
            MySettings.Default.language = "en" 'todo for testing only
            LogAppend("NamCore Studio " & My.Application.Info.Version.ToString() & " loaded", "Main_Main_Load", True)
            Dim frameworkVersion As FileVersionInfo =
                    FileVersionInfo.GetVersionInfo(Application.StartupPath + "\NCFramework.dll")
            Dim libncVersion As FileVersionInfo = FileVersionInfo.GetVersionInfo(Application.StartupPath + "\libnc.dll")
            Dim libncadvancedVersion As FileVersionInfo =
                    FileVersionInfo.GetVersionInfo(Application.StartupPath + "\libncadvanced.dll")
            LogAppend("Using NCFramework.dll version " & frameworkVersion.FileVersion.ToString(), "Main_Main_Load",
                      False)
            LogAppend("Using libnc.dll version " & libncVersion.FileVersion.ToString(), "Main_Main_Load", False)
            LogAppend("Using libncadvanced.dll version " & libncadvancedVersion.FileVersion.ToString(), "Main_Main_Load",
                      False)
            LogAppend("System information:", "Main_Main_Load", False)
            LogAppend("/OS NAME: " & My.Computer.Info.OSFullName, "Main_Main_Load", False)
            LogAppend("/OS VERSION: " & My.Computer.Info.OSVersion, "Main_Main_Load", False)
            LogAppend("/OS LANGUAGE: " & My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName,
                      "Main_Main_Load", False)
            LogAppend("/SYSTEM VERSION: " & Environment.Version.ToString(), "Main_Main_Load", False)
            LogAppend("/PROCESSOR COUNT: " & (Environment.ProcessorCount).ToString(), "Main_Main_Load", False)
            LogAppend(
                "/AVAILABLE PHYSICAL MEMORY: " & (My.Computer.Info.AvailablePhysicalMemory/1000000000).ToString() &
                " GB",
                "Main_Main_Load", False)
            LogAppend(
                "/SCREEN SIZE: " & Screen.PrimaryScreen.Bounds.Width.ToString & "x" &
                Screen.PrimaryScreen.Bounds.Height.ToString(), "Main_Main_Load", False)
            LogAppend("/APP STARTUP PATH: " & Application.StartupPath, "Main_Main_Load", False)
            InitializeDbc()
            GlobalVariables.GlobalWebClient = New WebClient()
            GlobalVariables.GlobalWebClient = GlobalVariables.GlobalWebClient.CheckProxy()
            Userwait.Close()
            ExecuteParams()
        End Sub

        Public Sub ExecuteParams()
            Dim args As String()
            args = Environment.GetCommandLineArgs()
            For i As Integer = 1 To args.Length - 1
                Select Case args(i).ToLower
                    Case "update"
                        Hide()
                        ChangelogInterface.Show()
                    Case "dev"
                        GlobalVariables.DebugMode = True
                    Case Else
                        If args(i).EndsWith(".ncsf") Then
                            LogAppend("Opening .ncsf file", "Main_ExecuteParams")
                            Application.DoEvents()
                            GlobalVariables.LoadingTemplate = True
                            Dim mSerializer As Serializer = New Serializer
                            GlobalVariables.globChars = mSerializer.DeSerialize(args(i), New GlobalCharVars)
                            prepareLive_template()
                            HideTimer.Start()
                        End If
                End Select
            Next
        End Sub

        Private Sub highlighter1_Click(sender As Object, e As EventArgs) Handles highlighter1.Click
            LogAppend("Trigger Live View click", "Main_highlighter1_Click", False)
            Hide()
            LiveView.Show()
        End Sub

        Private Sub highlighter3_Click(sender As Object, e As EventArgs) Handles highlighter3.Click
            LogAppend("Trigger Armory Parser click", "Main_highlighter3_Click", False)
            Hide()
            ArmoryInterface.Show()
        End Sub

        Private Sub Main_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
            If e.Button = MouseButtons.Left Then
                _ptMouseDownLocation = e.Location
            End If
        End Sub

        Private Sub Main_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
            If e.Button = MouseButtons.Left Then
                Location = New Point(e.Location.X - _ptMouseDownLocation.X + Location.X,
                                     e.Location.Y - _ptMouseDownLocation.Y + Location.Y)
            End If
        End Sub

        Private Sub highlighter5_Click(sender As Object, e As EventArgs) Handles highlighter5.Click
            WindowState = FormWindowState.Minimized
        End Sub

        Private Sub highlighter4_Click(sender As Object, e As EventArgs) Handles highlighter4.Click
            libnc.Main.ForceAppExit = True
            Application.Exit()
        End Sub

        Private Sub highlighter2_Click(sender As Object, e As EventArgs) Handles highlighter2.Click
            LogAppend("Trigger Template Explorer click", "Main_highlighter2_Click", False)
            Hide()
            TemplateExplorer.Show()
        End Sub

        Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles settings_pic.Click
            SettingsInterface.Show()
        End Sub

        Private Sub about_pic_Click(sender As Object, e As EventArgs) Handles about_pic.Click
            About.Show()
        End Sub

        Private Sub HideTimer_Tick(sender As Object, e As EventArgs) Handles HideTimer.Tick
            If Visible = True Then
                Hide()
                HideTimer.Stop()
            End If
        End Sub
    End Class
End Namespace