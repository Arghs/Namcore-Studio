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
'*      /Filename:      PrepChartrans
'*      /Description:   Character transfer interface
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.Functions

Namespace Forms
    Public Class PrepChartrans
        Private Sub Prep_chartrans_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        End Sub

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
                            If currentForm.Name = "Live_View" Then
                                Dim liveview As LiveView = DirectCast(currentForm, LiveView)
                                liveview.transChars_specificacc(tempAccList)
                            End If
                        Next
                    End If
                Next

            Else
                For Each currentForm As Form In Application.OpenForms
                    If currentForm.Name = "Live_View" Then
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

        Private Sub all_radio_CheckedChanged(sender As Object, e As EventArgs) Handles all_radio.CheckedChanged
            If all_radio.Checked = True Then specific_radio.Checked = False
        End Sub
    End Class
End Namespace