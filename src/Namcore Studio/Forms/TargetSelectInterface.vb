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
'*      /Filename:      TargetSelectInterface
'*      /Description:   Select target database/template
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.Linq
Imports NCFramework.Framework.Modules

Namespace Forms
    Public Class TargetSelectInterface
        '// Declaration
        Private _ptMouseDownLocation As Point
        '// Declaration

        Private Sub connect_bt_Click(sender As Object, e As EventArgs) Handles connect_bt.Click
            GlobalVariables.con_operator = 2
            DbConnect.Show()
            Close()
        End Sub

        Private Sub newtemplate_bt_Click(sender As Object, e As EventArgs) Handles newtemplate_bt.Click
            GlobalVariables.saveTemplateMode = True
            For Each liveview As Form In _
                From xform As Object In Application.OpenForms Where TryCast(xform, Form).Name = "LiveView" Select xform
                DirectCast(liveview, LiveView).PrepareTemplateCreation()
            Next
            Close()
        End Sub

        Private Sub me_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown, header.MouseDown
            If e.Button = MouseButtons.Left Then
                _ptMouseDownLocation = e.Location
            End If
        End Sub

        Private Sub me_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove, header.MouseMove
            If e.Button = MouseButtons.Left Then
                Location = New Point(e.Location.X - _ptMouseDownLocation.X + Location.X,
                                     e.Location.Y - _ptMouseDownLocation.Y + Location.Y)
            End If
        End Sub

        Private Sub highlighter2_Click(sender As Object, e As EventArgs) Handles highlighter2.Click
            Close()
        End Sub

        Private Sub closeBt_MouseEnter(sender As Object, e As EventArgs) Handles highlighter2.MouseEnter
            CType(sender, PictureBox).BackgroundImage = My.Resources.bt_close_light
        End Sub

        Private Sub closeBt_MouseLeave(sender As Object, e As EventArgs) Handles highlighter2.MouseLeave
            CType(sender, PictureBox).BackgroundImage = My.Resources.bt_close
        End Sub
    End Class
End Namespace