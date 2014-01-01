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
'*      /Filename:      Date2Timestamp
'*      /Description:   Extension to convert a date to timestamp and vv
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.Runtime.CompilerServices

Namespace Framework.Extension.Special
    Module Date2Timestamp
        ''' <summary>
        '''     timestamp converter
        ''' </summary>
        <Extension()>
        Public Function ToDate(ByRef stamp As Integer) As DateTime
            Try
                Dim span As TimeSpan
                Dim startdate As Date = #1/1/1970#
                If Stamp = 0 Then Return startdate
                span = New TimeSpan(0, 0, Stamp)
                Return startdate.Add(span)
            Catch ex As Exception
                Return DateTime.Today
            End Try
        End Function
        <Extension()>
        Public Function ToTimeStamp(ByRef dt As DateTime) As Integer
            Try
                Dim startdate As DateTime = #1/1/1970#
                Dim spanne As TimeSpan
                spanne = dt.Subtract(startdate)
                Return CType(Math.Abs(spanne.TotalSeconds()), Integer)
            Catch ex As Exception
                Return 0
            End Try
        End Function
    End Module
End Namespace