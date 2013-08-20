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
'*      /Filename:      date2timestamp
'*      /Description:   Extension to convert a date to timestamp and vv
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Imports Namcore_Studio_Framework.SpellItem_Information
Module date2timestamp
    ''' <summary>
    ''' timestamp converter
    ''' </summary>
    <System.Runtime.CompilerServices.Extension()>
    Public Function toDate(ByRef Stamp As Integer) As DateTime
        Try
            Dim Span As TimeSpan
            Dim Startdate As Date = #1/1/1970#
            If Stamp = 0 Then Return Startdate
            Span = New TimeSpan(0, 0, Stamp)
            Return Startdate.Add(Span)
        Catch ex As Exception
            Return DateTime.Today
        End Try

    End Function
    Public Function toTimeStamp(ByRef dt As DateTime) As Integer
        Try
            Dim Startdate As DateTime = #1/1/1970#
            Dim Spanne As TimeSpan
            Spanne = dt.Subtract(Startdate)
            Return CType(Math.Abs(Spanne.TotalSeconds()), Integer)
        Catch ex As Exception
            Return 0
        End Try
    End Function
End Module
