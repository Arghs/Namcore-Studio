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
'*      /Filename:      InformationHandler
'*      /Description:   DBC information provider
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports libnc.Structures
Imports System.Reflection

Public Class InformationHandler
    Public Function GetSpellNameById(ByVal spellId As Integer, ByVal lang As String)
        If lang = "de" Then
            Try
                Dim entry As Spell = CliDB.SpellDB.Find(Function(spell) spell.Id = spellId)
                If entry Is Nothing Then
                    Return "not found"
                End If
                Dim myType As Type = GetType(Spell)
                Dim myFields As FieldInfo() = myType.GetFields((BindingFlags.Public Or BindingFlags.Instance))
                Return myFields(1).GetValue(entry)
            Catch ex As Exception
                Return "error"
            End Try
        Else
            'TODO
            Try
                Dim entry As Spell = CliDB.SpellDB.Find(Function(spell) spell.Id = spellId)
                If entry Is Nothing Then
                    Return "not found"
                End If
                Dim myType As Type = GetType(Spell)
                Dim myFields As FieldInfo() = myType.GetFields((BindingFlags.Public Or BindingFlags.Instance))
                Return myFields(1).GetValue(entry)
            Catch ex As Exception
                Return "error"
            End Try
        End If
    End Function

    Public Function GetSkillNameById(ByVal skillId As Integer, ByVal lang As String)
        If lang = "de" Then
            Try
                Dim entry As Skill = CliDB.SkillDB.Find(Function(skill) skill.Id = skillId)
                If entry Is Nothing Then
                    Return "not found"
                End If
                Dim myType As Type = GetType(Skill)
                Dim myFields As FieldInfo() = myType.GetFields((BindingFlags.Public Or BindingFlags.Instance))
                Dim value As String = myFields(2).GetValue(entry)
                Return value
            Catch ex As Exception
                Return "error"
            End Try
        Else
            'TODO
            Try
                Dim entry As Skill = CliDB.SkillDB.Find(Function(skill) skill.Id = skillId)
                If entry Is Nothing Then
                    Return "not found"
                End If
                Dim myType As Type = GetType(Skill)
                Dim myFields As FieldInfo() = myType.GetFields((BindingFlags.Public Or BindingFlags.Instance))
                Return myFields(2).GetValue(entry)
            Catch ex As Exception
                Return "error"
            End Try
        End If
    End Function
End Class
