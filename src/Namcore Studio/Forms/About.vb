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
'*      /Filename:      About
'*      /Description:   Software information / About
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Namespace Forms
    Public Class About
        '// Declaration
        Private _ptMouseDownLocation As Point
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
        Private Sub closeBt_MouseEnter(sender As Object, e As EventArgs) Handles highlighter4.MouseEnter
            sender.backgroundimage = My.Resources.bt_close_light
        End Sub
        Private Sub closeBt_MouseLeave(sender As Object, e As EventArgs) Handles highlighter4.MouseLeave
            sender.backgroundimage = My.Resources.bt_close
        End Sub
        Private Sub minimizeBt_MouseEnter(sender As Object, e As EventArgs) Handles highlighter3.MouseEnter
            sender.backgroundimage = My.Resources.bt_minimize_light
        End Sub
        Private Sub minimizeBt_MouseLeave(sender As Object, e As EventArgs) Handles highlighter3.MouseLeave
            sender.backgroundimage = My.Resources.bt_minimize
        End Sub

        Private Sub highlighter1_Click(sender As Object, e As EventArgs) Handles highlighter3.Click
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

        Private Sub highlighter4_Click(sender As Object, e As EventArgs) Handles highlighter4.Click
            Close()
        End Sub

        Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
            Process.Start("https://github.com/megasus/Namcore-Studio")
        End Sub

        Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
            Process.Start("http://namcorestudio.com/")
        End Sub

        Private Sub About_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            version_lbl.Text = "Version " & My.Application.Info.Version.ToString() & " (Indev)"
        End Sub

        Private Sub LinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
            Process.Start("https://github.com/megasus/Namcore-Studio/issues?milestone=1&state=open")
        End Sub
    End Class
End Namespace