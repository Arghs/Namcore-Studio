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
'*
'* //FileInfo//
'*      /Filename:      Conversions
'*      /Description:   Includes frequently used functions for converting various objects
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.Logging

Namespace Framework.Functions
    Public Module Conversions

        Public Function TryInt(ByVal mystring As String) As Integer
            Try
                Dim parseResult As Integer = CInt(Integer.TryParse(mystring, Nothing))
                If parseResult = 0 Then
                    Return 0
                Else
                    Return CInt(mystring)
                End If
            Catch ex As Exception
                LogAppend("Exception during TryInt() : " & ex.ToString(), "Conversions_TryInt", False, True)
                Return 0
            End Try
        End Function

        Public Function TryUInt(ByVal mystring As String) As UInteger
            Try
                Dim parseResult As Integer = CInt(Integer.TryParse(mystring, Nothing))
                If parseResult = 0 Then
                    Return 0
                Else
                    Return CUInt(mystring)
                End If
            Catch ex As Exception
                LogAppend("Exception during TryInt() : " & ex.ToString(), "Conversions_TryInt", False, True)
                Return 0
            End Try
        End Function

        Public Function TrySingle(ByVal mystring As String) As Single
            Try
                Return Convert.ToSingle(mystring)
            Catch ex As Exception
                LogAppend("Exception during TrySingle() : " & ex.ToString(), "Conversions_TrySingle", False, True)
                Return 0
            End Try
        End Function
    End Module
End Namespace