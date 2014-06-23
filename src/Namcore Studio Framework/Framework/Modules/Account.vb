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
'*      /Filename:      Account
'*      /Description:   Account Object - account information class
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Namespace Framework.Modules
    <Serializable()>
    Public Class Account
        <Flags()>
        Public Enum ArcEmuFlag
            EXPANSION_CLASSIC = 0
            EXPANSION_TBC = 8
            EXPANSION_WOTLK = 16
            EXPANSION_WOTLK_TBC = 24
            EXPANSION_CATA = 32
        End Enum
        Public Id As Integer
        Public Name As String
        Public SetIndex As Integer
        Public Transcharlist As ArrayList
        Public ArcEmuPass As String
        Public PassHash As String
        Public ArcEmuFlags As ArcEmuFlag
        Public Locale As Integer
        Public ArcEmuGmLevel As String
        Public SessionKey As String
        Public LastLogin As DateTime
        Public LastIp As String
        Public Locked As Integer
        Public Email As String
        Public JoinDate As DateTime
        Public Expansion As Integer
        Public V As String
        Public S As String
        Public Core As Core
        Public SourceExpansion As Integer

        'Account Access
        Public GmLevel As Integer
        Public RealmId As Integer

        'Misc
        Public Characters As List(Of Character)
        Public IsArmory As Boolean = False
    End Class
End Namespace