'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013-2015 NamCore Studio <https://github.com/megasus/Namcore-Studio>
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
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Module Logging
    Public DisableTime As Boolean = False

    Public Sub Log(msg As String, Optional loglevel As LogLevel = 0, Optional time As Boolean = True,
                   Optional writeline As Boolean = True)
        Select Case loglevel
            Case LogLevel.NORMAL
                Console.ForegroundColor = ConsoleColor.DarkGreen
            Case LogLevel.INFO
                Console.ForegroundColor = ConsoleColor.Cyan
            Case LogLevel.LOW
                Console.ForegroundColor = ConsoleColor.Green
            Case LogLevel.WARNING
                Console.ForegroundColor = ConsoleColor.Yellow
            Case LogLevel.CRITICAL
                Console.ForegroundColor = ConsoleColor.Red
                Console.WriteLine()
        End Select
        If DisableTime Then time = False
        If msg.Length = 0 Then time = False
        If time Then msg = "[" & Now.TimeOfDay.ToString & "] " & msg
        If writeline Then
            Console.WriteLine(msg)
        Else
            Console.Write(msg)
        End If
    End Sub

    Dim _reportCounter As Integer = 0

    Public Sub ReportStatus(current As Integer, total As Integer)
        _reportCounter += 1
        If _reportCounter > 50 Or total - current <= 100 Then
            _reportCounter = 0
            Console.ForegroundColor = ConsoleColor.Green
            If current = total Then
                Console.WriteLine("{0}{1}Operating entry: {2}/{3}", vbCr, "[" & Now.TimeOfDay.ToString & "] ",
                                  current.ToString(), total.ToString())
            Else
                Console.Write("{0}{1}Operating entry: {2}/{3}", vbCr, "[" & Now.TimeOfDay.ToString & "] ",
                              current.ToString(), total.ToString())
            End If
        End If
    End Sub

    Public Enum LogLevel As Integer
        NORMAL = 0
        INFO = 1
        LOW = 2
        WARNING = 3
        CRITICAL = 4
    End Enum
End Module
