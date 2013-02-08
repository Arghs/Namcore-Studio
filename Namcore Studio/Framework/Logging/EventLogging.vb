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

Public Class EventLogging

    Public Shared Sub LogAppend(ByVal _event As String, ByVal location As String, Optional userOut As Boolean = False, Optional iserror As Boolean = False)
        If iserror = False Then
            If userOut = True Then
                eventlog = eventlog & vbNewLine & "[" & Now.TimeOfDay.ToString & "]" & _event
                eventlog_full = eventlog_full & vbNewLine & "[" & Date.Today.ToString & " " & Now.TimeOfDay.ToString & "]" & _event
            Else

            End If
        Else

        End If
    End Sub

    Public Shared Sub LogClear()
        eventlog = ""
    End Sub
End Class
