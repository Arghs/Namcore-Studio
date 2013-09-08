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

Imports NCFramework.GlobalVariables
Imports System.Threading
Imports System.IO

Public Module EventLogging
    Public Delegate Sub IncomingEventDelegate(ByVal _event As String)
    Public lastprogress As Integer
    Public isbusy As Boolean = False
    Private m_userOut As Boolean
    Public Sub LogAppend(ByVal _event As String, ByVal location As String, Optional userOut As Boolean = False, Optional iserror As Boolean = False)
        m_userOut = userOut
        While isbusy
        End While
        isbusy = True
        appendStatus(_event, location, iserror)
        isbusy = False
    End Sub
    Private Sub appendStatus(ByVal _status As String, ByVal loc As String, Optional Iserror As Boolean = False)
        Dim timenow As String = Now.TimeOfDay.ToString
        Dim append As String = ""
        If Iserror Then append = "[ERROR]"
        eventlog = eventlog & vbNewLine & "[" & timenow & "]" & append & "[" & loc & "]" & _status
        If m_userOut = True Then
            proccessTXT = "[" & timenow & "]" & _status & vbNewLine & proccessTXT
            Dim fs As New StreamWriter(My.Computer.FileSystem.SpecialDirectories.Desktop & "/log.txt", FileMode.OpenOrCreate, System.Text.Encoding.Default)
            fs.WriteLine(eventlog)
            fs.Close()
            eventlog = ""
            Try
                procStatus.appendProc("[" & Now.TimeOfDay.ToString & "]" & _status)
            Catch ex As Exception

            End Try
        End If
    End Sub
  
End Module
