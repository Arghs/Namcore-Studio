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
        <Extension>
        Public Function UpdateStanding(rep As Reputation) As Reputation
            Try
                Select Case rep.status
                    Case Reputation.RepStatus.REPSTAT_STRANGER : rep.Standing = rep.Value - 42000
                    Case Reputation.RepStatus.REPSTAT_ACQUAINTANCE : rep.Standing = rep.Value - 6000
                    Case Reputation.RepStatus.REPSTAT_UNFRIENDLY : rep.Standing = rep.Value - 3000
                    Case Reputation.RepStatus.REPSTAT_NEUTRAL : rep.Standing = rep.Value
                    Case Reputation.RepStatus.REPSTAT_FRIENDLY : rep.Standing = 3000 + rep.Value
                    Case Reputation.RepStatus.REPSTAT_HONORED : rep.Standing = 9000 + rep.Value
                    Case Reputation.RepStatus.REPSTAT_REVERED : rep.Standing = 21000 + rep.Value
                    Case Reputation.RepStatus.REPSTAT_EXALTED : rep.Standing = 42000 + rep.Value
                End Select
                Return rep
            Catch ex As Exception
                Return rep
            End Try
        End Function


        ''' <summary>
        '''     Updates reputation value, status and max by standing
        ''' </summary>
        <Extension>
        Public Function UpdateValueMax(rep As Reputation) As Reputation
            Try
                If rep.Standing >= - 42000 Then _
                    rep.Value = 42000 + rep.Standing : rep.Status = Reputation.RepStatus.REPSTAT_STRANGER : _
                        rep.Max = 36000
                If rep.Standing >= - 6000 Then _
                    rep.Value = 6000 + rep.Standing : rep.Status = Reputation.RepStatus.REPSTAT_ACQUAINTANCE : _
                        rep.Max = 3000
                If rep.Standing >= - 3000 Then _
                    rep.Value = 3000 + rep.Standing : rep.Status = Reputation.RepStatus.REPSTAT_UNFRIENDLY : _
                        rep.Max = 3000
                If rep.Standing >= 0 Then _
                    rep.Value = rep.Standing : rep.Status = Reputation.RepStatus.REPSTAT_NEUTRAL : rep.Max = 3000
                If rep.Standing >= 3000 Then _
                    rep.Value = rep.Standing - 3000 : rep.Status = Reputation.RepStatus.REPSTAT_FRIENDLY : _
                        rep.Max = 6000
                If rep.Standing >= 9000 Then _
                    rep.Value = rep.Standing - 9000 : rep.Status = Reputation.RepStatus.REPSTAT_HONORED : _
                        rep.Max = 12000
                If rep.Standing >= 21000 Then _
                    rep.Value = rep.Standing - 21000 : rep.Status = Reputation.RepStatus.REPSTAT_REVERED : _
                        rep.Max = 21000
                If rep.Standing >= 42000 Then _
                    rep.Value = rep.Standing - 42000 : rep.Status = Reputation.RepStatus.REPSTAT_EXALTED : rep.Max = 999
                Return rep
            Catch ex As Exception
                Return rep
            End Try
        End Function
    End Module
End Namespace