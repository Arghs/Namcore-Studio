
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
'*      /Filename:      RepStandingExtension
'*      /Description:   Extension to update reputation standing/value/max
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.Runtime.CompilerServices
Imports NCFramework.Framework.Modules

Namespace Framework.Extension

    Public Module RepStandingExtension
        ''' <summary>
        '''     Updates reputation standing by value and status
        ''' </summary>
        <Extension()>
        Public Function UpdateStanding(rep As Reputation) As Reputation
            Try
                Select Case rep.status
                    Case 0 : rep.standing = rep.value - 42000
                    Case 1 : rep.standing = rep.value - 6000
                    Case 2 : rep.standing = rep.value - 3000
                    Case 3 : rep.standing = rep.value
                    Case 4 : rep.standing = 3000 + rep.value
                    Case 5 : rep.standing = 9000 + rep.value
                    Case 6 : rep.standing = 21000 + rep.value
                    Case 7 : rep.standing = 42000 + rep.value
                End Select
                Return rep
            Catch ex As Exception
                Return rep
            End Try
        End Function


        ''' <summary>
        '''     Updates reputation value, status and max by standing
        ''' </summary>
        <Extension()>
        Public Function UpdateValueMax(rep As Reputation) As Reputation
            Try
                If rep.standing >= -42000 Then rep.value = 42000 + rep.standing : rep.status = 0 : rep.max = 36000
                If rep.standing >= -6000 Then rep.value = 6000 + rep.standing : rep.status = 1 : rep.max = 3000
                If rep.standing >= -3000 Then rep.value = 3000 + rep.standing : rep.status = 2 : rep.max = 3000
                If rep.standing >= 0 Then rep.value = rep.standing : rep.status = 3 : rep.max = 3000
                If rep.standing >= 3000 Then rep.value = rep.standing - 3000 : rep.status = 4 : rep.max = 6000
                If rep.standing >= 9000 Then rep.value = rep.standing - 9000 : rep.status = 5 : rep.max = 12000
                If rep.standing >= 21000 Then rep.value = rep.standing - 21000 : rep.status = 6 : rep.max = 21000
                If rep.standing >= 42000 Then rep.value = rep.standing - 42000 : rep.status = 7 : rep.max = 999
                Return rep
            Catch ex As Exception
                Return rep
            End Try
        End Function
    End Module

End Namespace