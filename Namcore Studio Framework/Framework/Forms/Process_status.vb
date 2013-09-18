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
'*      /Filename:      Process_status
'*      /Description:   Provides an interface to display operation status
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.Windows.Forms
Imports System.Drawing
Imports NCFramework.Framework.Module

Namespace Framework.Forms

    Public Class ProcessStatus

        '// Declaration
        Private Delegate Sub AppendTextBoxDelegate(ByVal txt As String)
        Private _ptMouseDownLocation As Point
        '// Declartaion

        Private Sub close_bt_Click(sender As System.Object, e As EventArgs) Handles close_bt.Click
            Close()
        End Sub

        Public Sub AppendProc(ByVal status As String)
            If process_tb.InvokeRequired Then
                process_tb.Invoke(New AppendTextBoxDelegate(AddressOf AppendProc), New Object() {GlobalVariables.proccessTXT})
            Else
                process_tb.Text = GlobalVariables.proccessTXT
            End If
            Application.DoEvents()
        End Sub
        
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

        Private Sub highlighter_MouseEnter(sender As Object, e As EventArgs) Handles highlighter1.MouseEnter, highlighter2.MouseEnter
            sender.backgroundimage = My.Resources.highlight
        End Sub

        Private Sub highlighter_MouseLeave(sender As Object, e As EventArgs) Handles highlighter1.MouseLeave, highlighter2.MouseLeave
            sender.backgroundimage = Nothing
        End Sub

        Private Sub highlighter2_Click(sender As Object, e As EventArgs) Handles highlighter2.Click
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
        
    End Class
End Namespace