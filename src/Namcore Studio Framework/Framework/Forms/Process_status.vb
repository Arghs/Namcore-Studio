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
'*      /Filename:      Process_status
'*      /Description:   Provides an interface to display operation status
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.Drawing
Imports System.Windows.Forms
Imports FastColoredTextBoxNS

Namespace Framework.Forms
    Public Class ProcessStatus
        '// Declaration
        Private Delegate Sub AppendTextBoxDelegate(ByVal txt As String, ByVal style As Style)

        Private _ptMouseDownLocation As Point
        '// Declartaion

        Private Sub close_bt_Click(sender As Object, e As EventArgs) Handles close_bt.Click
            Close()
        End Sub

        Public Sub AppendProc(txt As String, style As Style)
            If fctb.InvokeRequired Then
                fctb.Invoke(New AppendTextBoxDelegate(AddressOf AppendProc),
                                  New Object() {txt, style})
            Else
                txt = txt & vbCrLf
                fctb.BeginUpdate()
                fctb.Selection.BeginUpdate()
                Dim userSelection As Range = fctb.Selection.Clone()
                fctb.Selection.Start = If(fctb.LinesCount > 0, New Place(fctb(fctb.LinesCount - 1).Count, fctb.LinesCount - 1), New Place(0, 0))
                fctb.InsertText(txt, style)
                If Not userSelection.IsEmpty OrElse userSelection.Start.iLine < fctb.LinesCount - 2 Then
                    fctb.Selection.Start = userSelection.Start
                    fctb.Selection.[End] = userSelection.[End]
                Else
                    fctb.DoCaretVisible()
                End If
                fctb.Selection.EndUpdate()
                fctb.EndUpdate()
                If userSelection.IsEmpty OrElse Not userSelection.Start.iLine < fctb.LinesCount - 2 Then
                    fctb.GoEnd()
                End If
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
                Location = New Point(e.Location.X - _ptMouseDownLocation.X + Location.X,
                                     e.Location.Y - _ptMouseDownLocation.Y + Location.Y)
            End If
        End Sub

        Private Sub closeBt_MouseEnter(sender As Object, e As EventArgs) Handles highlighter2.MouseEnter
            CType(sender, PictureBox).BackgroundImage = My.Resources.bt_close_light
        End Sub

        Private Sub closeBt_MouseLeave(sender As Object, e As EventArgs) Handles highlighter2.MouseLeave
            CType(sender, PictureBox).BackgroundImage = My.Resources.bt_close
        End Sub

        Private Sub minimizeBt_MouseEnter(sender As Object, e As EventArgs) Handles highlighter1.MouseEnter
            CType(sender, PictureBox).BackgroundImage = My.Resources.bt_minimize_light
        End Sub

        Private Sub minimizeBt_MouseLeave(sender As Object, e As EventArgs) Handles highlighter1.MouseLeave
            CType(sender, PictureBox).BackgroundImage = My.Resources.bt_minimize
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
    End Class
End Namespace