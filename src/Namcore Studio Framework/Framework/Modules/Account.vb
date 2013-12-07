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
'*      /Filename:      Account
'*      /Description:   Account Object - account information class
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Namespace Framework.Modules
    <Serializable()>
    Public Class Account
        Public Id As Integer
        Public Name As String
        Public SetIndex As Integer
        Public Transcharlist As ArrayList
        Public ArcEmuPass As String
        Public PassHash As String
        Public ArcEmuFlags As Integer
        Public Locale As Integer
        Public ArcEmuGmLevel As String
        Public SessionKey As String
        Public LastLogin As DateTime
        Public Email As String
        Public JoinDate As Integer
        Public Expansion As Integer
        Public V As String
        Public S As String
        Public Core As String
        Public SourceExpansion As Integer

        'Account Access
        Public GmLevel As Integer
        Public RealmId As Integer

        'Misc
        Public Characters As List(Of Character)
        Public CharactersIndex As String

        Public Sub New(accname As String, accountid As Integer)
            Name = accname
            Id = accountid
        End Sub

        Public Function ShallowCopy() As Account
            Return DirectCast(MemberwiseClone(), Account)
        End Function
    End Class
End Namespace