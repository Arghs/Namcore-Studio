'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013 NamCore Studio <https://github.com/megasus/Namcore-Studio>
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
'*      /Filename:      PrepChartrans
'*      /Description:   Character transfer interface
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.Functions

Namespace Forms
    Public Class PrepChartrans
        '// Declaration
        Private _ptMouseDownLocation As Point
        '// Declaration

        Private Sub ApplyTrans_Click(sender As Object, e As EventArgs) Handles ApplyTrans.Click
            Dim tempAccList As New ArrayList

            If specific_radio.Checked = True Then
                If accnames_txtbox.Lines.Length = 0 Then _
                    MsgBox(ResourceHandler.GetUserMessage("noaccentered"), MsgBoxStyle.Critical,
                           ResourceHandler.GetUserMessage("errorbox")) : Exit Sub
                Dim sLines() As String = accnames_txtbox.Lines
                Dim removecount As Integer
                For i As Integer = 0 To sLines.Length - 1
                    If sLines(i) = "" Then
                        removecount += 1
                        If removecount = sLines.Length Then _
                            MsgBox(ResourceHandler.GetUserMessage("noaccentered"), MsgBoxStyle.Critical,
                                   ResourceHandler.GetUserMessage("errorbox")) : Exit Sub
                    Else
                        Dim tmpAccount(2) As String
                        tmpAccount(1) = sLines(i)
                        tempAccList.Add(tmpAccount)
                        For Each currentForm As Form In Application.OpenForms
                            If currentForm.Name = "liveview" Then
                                Dim liveview As LiveView = DirectCast(currentForm, LiveView)
                                liveview.transChars_specificacc(tempAccList)
                            End If
                        Next
                    End If
                Next

            Else
                For Each currentForm As Form In Application.OpenForms
                    If currentForm.Name = "liveview" Then
                        Dim liveview As LiveView = DirectCast(currentForm, LiveView)
                        liveview.transChars_allacc()
                    End If
                Next
            End If
            Close()
        End Sub

        Private Sub specific_radio_CheckedChanged(sender As Object, e As EventArgs) _
            Handles specific_radio.CheckedChanged
            If specific_radio.Checked = True Then all_radio.Checked = False
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
        Private Sub all_radio_CheckedChanged(sender As Object, e As EventArgs) Handles all_radio.CheckedChanged
            If all_radio.Checked = True Then specific_radio.Checked = False
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
    End Class
End Namespace