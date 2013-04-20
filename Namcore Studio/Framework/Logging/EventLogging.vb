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
'*      /Filename:      EventLogging
'*      /Description:   Handles logging of events and exceptions
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Imports Namcore_Studio.GlobalVariables
Imports Namcore_Studio.Process_status
Imports System.Threading

Public Class EventLogging
    Private Shared loadCharThread As System.Threading.Thread
    Private Shared proc As System.Threading.Thread
    Private Delegate Sub AppendDelegate(ByRef _event As String)
    Public Sub LogAppend(ByVal _event As String, ByVal location As String, Optional userOut As Boolean = False, Optional iserror As Boolean = False)
        If iserror = False Then
            If userOut = True Then
                appendStatus("[" & Now.TimeOfDay.ToString & "]" & _event)
                eventlog = eventlog & vbNewLine & "[" & Now.TimeOfDay.ToString & "]" & _event
                eventlog_full = eventlog_full & vbNewLine & "[" & Date.Today.ToString & " " & Now.TimeOfDay.ToString & "]" & _event
            Else
                eventlog_full = eventlog_full & vbNewLine & "[" & Date.Today.ToString & " " & Now.TimeOfDay.ToString & "]" & _event
            End If
        Else
            If userOut = True Then
                appendStatus("[" & Now.TimeOfDay.ToString & "]" & _event)
                eventlog = eventlog & vbNewLine & "[" & Now.TimeOfDay.ToString & "]" & _event
                eventlog_full = eventlog_full & vbNewLine & "[" & Date.Today.ToString & " " & Now.TimeOfDay.ToString & "]" & _event
            Else
                eventlog_full = eventlog_full & vbNewLine & "[" & Date.Today.ToString & " " & Now.TimeOfDay.ToString & "]" & _event
            End If
        End If
    End Sub
    Private Sub appendStatus(ByVal _status As String)

        d.BeginInvoke("hello world", New AsyncCallback(AddressOf MyCallback), Nothing)
        If Not loadCharThread Is Nothing Then
            For Each CurrentForm As Form In Application.OpenForms
                If CurrentForm.Name = "Process_status" Then
                    Dim statusWindow As Process_status = DirectCast(CurrentForm, Process_status)
                    If statusWindow.InvokeRequired Then
                        statusWindow.Invoke(New AppendDelegate(AddressOf ))
                    End If

                End If
            Next
        Else
            loadCharThread = New System.Threading.Thread(AddressOf appendStatus)
            loadCharThread.Start(_status)
        End If
        proc = New s
        Process_status.Show()
    End Sub
    Private Sub test()

    End Sub
    Public Sub LogClear()
        eventlog = ""
    End Sub
End Class
