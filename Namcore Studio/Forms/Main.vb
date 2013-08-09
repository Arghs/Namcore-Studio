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
'*      /Filename:      Main
'*      /Description:   Main menu
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Imports Namcore_Studio.GlobalVariables
Imports Namcore_Studio.EventLogging
Public Class Main

    Private Sub highlighter_MouseEnter(sender As Object, e As EventArgs) Handles highlighter1.MouseEnter, highlighter2.MouseEnter, highlighter3.MouseEnter, highlighter4.MouseEnter, highlighter5.MouseEnter
        sender.backgroundimage = My.Resources.highlight
    End Sub

    Private Sub highlighter_MouseLeave(sender As Object, e As EventArgs) Handles highlighter1.MouseLeave, highlighter2.MouseLeave, highlighter3.MouseLeave, highlighter4.MouseLeave, highlighter5.MouseLeave
        sender.backgroundimage = Nothing
    End Sub


    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "NamCore Studio - Development - " & My.Application.Info.Version.ToString() & " - © megasus 2013"
        version_lbl.Text = "NamCore Studio - Development - " & My.Application.Info.Version.ToString() & " - © megasus 2013"
        lastregion = "main"
        Process_status.Close()
        procStatus = New Process_status
        procStatus.Show()
        procStatus.ArmoryWorker = New System.ComponentModel.BackgroundWorker
        procStatus.ArmoryWorker.WorkerReportsProgress = True
        procStatus.ArmoryWorker.WorkerSupportsCancellation = True

        If My.Computer.FileSystem.FileExists(My.Computer.FileSystem.SpecialDirectories.Desktop & "/log.txt") Then
            My.Computer.FileSystem.DeleteFile(My.Computer.FileSystem.SpecialDirectories.Desktop & "/log.txt")
        End If
        LogAppend("NamCore Studio " & My.Application.Info.Version.ToString() & " loaded", "Main_Main_Load", False)
        LogAppend("System information:", "Main_Main_Load", False)
        LogAppend("/OS NAME: " & My.Computer.Info.OSFullName, "Main_Main_Load", False)
        LogAppend("/OS VERSION: " & My.Computer.Info.OSVersion, "Main_Main_Load", False)
        LogAppend("/OS LANGUAGE: " & My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName, "Main_Main_Load", False)
        LogAppend("/SYSTEM VERSION: " & System.Environment.Version.ToString(), "Main_Main_Load", False)
        LogAppend("/TOTAL PHYSICAL MEMORY: " & (My.Computer.Info.TotalPhysicalMemory / 100000000).ToString() & " GB", "Main_Main_Load", False)
        LogAppend("/TOTAL VIRTUAL MEMORY: " & (My.Computer.Info.TotalVirtualMemory / 100000000).ToString() & " GB", "Main_Main_Load", False)
        LogAppend("/SCREEN SIZE: " & Screen.PrimaryScreen.Bounds.Width.ToString & "x" & Screen.PrimaryScreen.Bounds.Height.ToString(), "Main_Main_Load", False)
        LogAppend("/APP STARTUP PATH: " & Application.StartupPath, "Main_Main_Load", False)
    End Sub

    Private Sub highlighter1_Click(sender As Object, e As EventArgs) Handles highlighter1.Click
        Me.Hide()
        Live_View.Show()
    End Sub

    Private Sub highlighter3_Click(sender As Object, e As EventArgs) Handles highlighter3.Click
        Me.Hide()
        Armory_interface.Show()
    End Sub
    Private ptMouseDownLocation As Point
    Private Sub Main_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            ptMouseDownLocation = e.Location
        End If
    End Sub

    Private Sub Main_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me.Location = e.Location - ptMouseDownLocation + Location
        End If
    End Sub

    Private Sub highlighter5_Click(sender As Object, e As EventArgs) Handles highlighter5.Click
        WindowState = FormWindowState.Minimized
    End Sub

    Private Sub highlighter4_Click(sender As Object, e As EventArgs) Handles highlighter4.Click
        Application.Exit()
    End Sub
End Class