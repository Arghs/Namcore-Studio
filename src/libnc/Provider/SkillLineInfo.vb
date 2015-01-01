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
'*      /Filename:      SkillLineInfo
'*      /Description:   Provides skill information
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports libnc.Main
Namespace Provider
    Public Module SkillLineInfo
        Public Function GetSkillNameById(ByVal skillId As Integer, ByVal locale As String) As String
            CheckInit()
            Dim targetField As Integer = 1
            If locale = "en" Then targetField += 1
            Dim myResult As String = ExecuteCsvSearch(SkillLineCsv, "SkillId", skillId.ToString(), targetField)(0)
            If myResult = "-" Then myResult = "Not found"
            Return myResult
        End Function
        Public Function GetMinimumSkillBySpellId(ByVal spellId As Integer) As Integer
            CheckInit()
            Const targetField As Integer = 2
            Dim myResult As String = ExecuteCsvSearch(SkillLineAbilityCsv, "SpellId", spellId.ToString(), targetField)(0)
            If myResult = "-" Then myResult = "0"
            Try
                Return CInt(myResult)
            Catch ex As Exception
                Return 0
            End Try
        End Function
        Public Function GetSkillIdBySpellId(ByVal spellId As Integer) As Integer
            CheckInit()
            Const targetField As Integer = 0
            Dim myResult As String = ExecuteCsvSearch(SkillLineAbilityCsv, "SpellId", spellId.ToString(), targetField)(0)
            If myResult = "-" Then myResult = "0"
            Try
                Return CInt(myResult)
            Catch ex As Exception
                Return 0
            End Try
        End Function
        Public Function GetSkillLineAbility() As DataTable
            CheckInit()
            Return SkillLineAbilityCsv
        End Function
    End Module
End Namespace