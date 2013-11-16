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
'*      /Filename:      EventTrigger
'*      /Description:   Form event trigger extension
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Namespace Forms.Extension
    Public Class EventTrigger
        '// Declaration
        Private _ptMouseDownLocation As Point
        '// Declaration

        Public Overridable Sub Meload(ByVal sender As Object, e As EventArgs) Handles MyBase.Load
            header.Size = New Point(Size.Width - 9, header.Size.Height)
            closepanel.Location = New Point(header.Size.Width - 125, closepanel.Location.Y)
        End Sub

        Public Overridable Sub me_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
            If e.Button = MouseButtons.Left Then
                _ptMouseDownLocation = e.Location
            End If
        End Sub

        Public Overridable Sub me_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
            If e.Button = MouseButtons.Left Then
                Location = New Point(e.Location.X - _ptMouseDownLocation.X + Location.X,
                                     e.Location.Y - _ptMouseDownLocation.Y + Location.Y)
            End If
        End Sub

        Private Sub closeBt_MouseEnter(sender As Object, e As EventArgs) Handles highlighter2.MouseEnter
            sender.backgroundimage = My.Resources.bt_close_light
        End Sub
        Private Sub closeBt_MouseLeave(sender As Object, e As EventArgs) Handles highlighter2.MouseLeave
            sender.backgroundimage = My.Resources.bt_close
        End Sub
        Private Sub minimizeBt_MouseEnter(sender As Object, e As EventArgs) Handles highlighter1.MouseEnter
            sender.backgroundimage = My.Resources.bt_minimize_light
        End Sub
        Private Sub minimizeBt_MouseLeave(sender As Object, e As EventArgs) Handles highlighter1.MouseLeave
            sender.backgroundimage = My.Resources.bt_minimize
        End Sub
        Private Sub settingsBt_MouseEnter(sender As Object, e As EventArgs) Handles settings_bt.MouseEnter
            sender.backgroundimage = My.Resources.bt_settings_light
        End Sub
        Private Sub settingsBt_MouseLeave(sender As Object, e As EventArgs) Handles settings_bt.MouseLeave
            sender.backgroundimage = My.Resources.bt_settings
        End Sub
        Private Sub aboutBt_MouseEnter(sender As Object, e As EventArgs) Handles about_bt.MouseEnter
            sender.backgroundimage = My.Resources.bt_about_light
        End Sub
        Private Sub aboutBt_MouseLeave(sender As Object, e As EventArgs) Handles about_bt.MouseLeave
            sender.backgroundimage = My.Resources.bt_about
        End Sub

        Public Overridable Sub header_MouseDown(sender As Object, e As MouseEventArgs) Handles header.MouseDown
            If e.Button = MouseButtons.Left Then
                _ptMouseDownLocation = e.Location
            End If
        End Sub

        Public Overridable Sub header_MouseMove(sender As Object, e As MouseEventArgs) Handles header.MouseMove
            If e.Button = MouseButtons.Left Then
                Location = New Point(e.Location.X - _ptMouseDownLocation.X + Location.X,
                                     e.Location.Y - _ptMouseDownLocation.Y + Location.Y)
            End If
        End Sub


        Public Overridable Sub highlighter1_Click_1(sender As Object, e As EventArgs) Handles highlighter1.Click
            WindowState = FormWindowState.Minimized
        End Sub

        Public Overridable Sub settings_pic_Click(sender As Object, e As EventArgs) Handles settings_bt.Click
            SettingsInterface.Show()
        End Sub
    End Class
End Namespace