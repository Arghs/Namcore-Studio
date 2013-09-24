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
Imports System.IO
Imports System.Text
Imports NCFramework.Framework.Modules

Namespace Framework.Logging

    Public Module EventLogging

        '// Declaration
        Public Delegate Sub IncomingEventDelegate(ByVal [event] As String)
        Public Lastprogress As Integer
        Public Isbusy As Boolean = False
        Private _mUserOut As Boolean
        '// Declaration

        Public Sub LogAppend(ByVal myevent As String, ByVal location As String, Optional userOut As Boolean = False,
                             Optional iserror As Boolean = False)
            _mUserOut = userOut
            While Isbusy
            End While
            Isbusy = True
            appendStatus(myevent, location, iserror)
            Isbusy = False
        End Sub

        Private Sub AppendStatus(ByVal status As String, ByVal loc As String, Optional iserror As Boolean = False)
            Dim timenow As String = Now.TimeOfDay.ToString
            Dim append As String = ""
            If iserror Then append = "[ERROR]"
            GlobalVariables.eventlog = GlobalVariables.eventlog & vbNewLine & "[" & timenow & "]" & append & "[" & loc & "]" &
                                       status
            If _mUserOut = True Then
                GlobalVariables.proccessTXT = "[" & timenow & "]" & status & vbNewLine & GlobalVariables.proccessTXT
                Dim _
                    fs As _
                        New StreamWriter(My.Computer.FileSystem.SpecialDirectories.Desktop & "/log.txt",
                                         FileMode.OpenOrCreate, Encoding.Default)
                fs.WriteLine(GlobalVariables.eventlog)
                fs.Close()
                GlobalVariables.eventlog = ""
                Try
                    GlobalVariables.procStatus.AppendProc("[" & Now.TimeOfDay.ToString & "]" & status)
                Catch ex As Exception

                End Try
            End If
        End Sub
    End Module
End Namespace