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
Imports System.IO

Public Class EventLogging
    Public Delegate Sub IncomingEventDelegate(ByVal _event As String)
    Shared lastprogress As Integer
    Shared isbusy As Boolean = False
    Public Shared Sub LogAppend(ByVal _event As String, ByVal location As String, Optional userOut As Boolean = False, Optional iserror As Boolean = False)
        Dim x As UInt32 = 546
        While isbusy

        End While
        isbusy = True
        userOut = True
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
        isbusy = False
    End Sub
    Private Shared Sub appendStatus(ByVal _status As String, Optional progress As Integer = 0)
        proccessTXT = proccessTXT & _status & vbNewLine
        If lastprogress = Nothing Then lastprogress = 0
        If progress = 0 Then progress = lastprogress
        lastprogress = progress
        Dim fs As New StreamWriter(My.Computer.FileSystem.SpecialDirectories.Desktop & "/log.txt", FileMode.OpenOrCreate, System.Text.Encoding.Default)
        fs.WriteLine(_status)
        fs.Close()
        Try
            If Not procStatus.ArmoryWorker Is Nothing Then
                If procStatus.ArmoryWorker.IsBusy Then
                    Try
                        procStatus.ArmoryWorker.ReportProgress(progress)
                    Catch ex As Exception

                    End Try

                Else
                    procStatus.appendProc()
                End If
            ElseIf Not procStatus.TransferWorker Is Nothing Then
                If procStatus.TransferWorker.IsBusy Then
                    Try
                        procStatus.TransferWorker.ReportProgress(progress)
                    Catch ex As Exception

                    End Try

                Else
                    procStatus.appendProc()
                End If
            Else
                procStatus.appendProc()
            End If

        Catch ex As Exception

        End Try


    End Sub

    Public Shared Sub LogClear()
        eventlog = ""
    End Sub
End Class
