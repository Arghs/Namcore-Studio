'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013-2016 NamCore Studio <https://github.com/megasus/Namcore-Studio>
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
'*      /Filename:      ChangelogInterface
'*      /Description:   Listing new features and changes
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.IO
Imports NamCore_Studio.Forms.Extension
Imports NCFramework.Framework.Logging

Namespace Forms
    Public Class ChangelogInterface
        Inherits EventTrigger

        Private Sub TemplateExplorer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            AddHandler highlighter2.Click, AddressOf highlighter2_Click
            LoadChangelog()
        End Sub

        Private Sub highlighter2_Click(sender As Object, e As EventArgs)
            continue_bt.PerformClick()
        End Sub

        Private Sub LoadChangelog()
            LogAppend("Loading changelog", "ChangelogInterface_LoadChangelog")
            version_lbl.Text = "NamCore Studio version " & My.Application.Info.Version.ToString() & " - Indev"
            Try
                Using sr As New StreamReader("Changes")
                    Dim line As String
                    line = sr.ReadToEnd()
                    changelog_txtbox.Text = ""
                    changelog_txtbox.AppendText(line)
                End Using
            Catch e As Exception
                LogAppend("Exception occured: " & e.ToString(), "ChangelogInterface_LoadChangelog", False, True)
            End Try
        End Sub

        Private Sub continue_bt_Click(sender As Object, e As EventArgs) Handles continue_bt.Click
            LogAppend("Trigger continue button click", "ChangelogInterface_continue_bt_Click", False)
            Close()
            Main.Show()
        End Sub
    End Class
End Namespace