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
'*      /Filename:      CliDb
'*      /Description:   Client dbc handler
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports libnc.Structures
Imports libnc.Reader

Public Class CliDb
    Public Shared Property Count() As Integer
        Get
            Return _mCount
        End Get
        Set(value As Integer)
            _mCount = value
        End Set
    End Property

    Private Shared _mCount As Integer

    Public Shared SpellDb As List(Of Spell)
    Public Shared SkillDb As List(Of Skill)

    Public Shared Sub Initialize()
        SpellDB = DbReader.Read (Of Spell)(My.Resources.SpellDE)
        SkillDB = DbReader.Read (Of Skill)(My.Resources.SkillLineDE)
    End Sub
End Class
