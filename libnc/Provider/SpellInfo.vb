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
'*F:\Projekte\Visual Studio\Namcore-Studio\Namcore Studio Framework\Framework\Functions\SpellItemInformation.vb
'* Developed by Alcanmage/megasus
'*
'* //FileInfo//
'*      /Filename:      SpellInfo
'*      /Description:   Provides gereral spell information
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports libnc.Main
Namespace Provider
    Public Module SpellInfo
        Public Function GetSpellNameBySpellId(ByVal spellId As Integer, ByVal locale As String) As String
            Dim targetField As Integer = 1
            If locale = "en" Then targetField += 1
            Dim myResult As String = ExecuteCsvSearch(SpellCsv, "SpellId", spellId.ToString(), targetField)(0)
            If myResult = "-" Then myResult = "Not found"
            Return myResult
        End Function
        Public Function GetEffectIdBySpellId(ByVal spellId As Integer) As Integer
            Const targetField As Integer = 1
            Dim myResult As String = ExecuteCsvSearch(SpellEffectCsv, "SpellId", spellId.ToString(), targetField)(0)
            Dim returnResult As Integer
            If myResult = "-" Then returnResult = 0
            Try
                returnResult = CInt(myResult)
            Catch
                returnResult = 0
            End Try
            Return returnResult
        End Function
        Public Function GetEffectIdByGemId(ByVal gemId As Integer) As Integer
            Const targetField As Integer = 0
            Dim myResult As String = ExecuteCsvSearch(SpellEnchantCsv, "GemId", gemId.ToString(), targetField)(0)
            Dim returnResult As Integer
            If myResult = "-" Then returnResult = 0
            Try
                returnResult = CInt(myResult)
            Catch
                returnResult = 0
            End Try
            Return returnResult
        End Function
        Public Function GetEffectNameById(ByVal effectId As Integer, ByVal locale As String) As String
            Dim targetField As Integer = 5
            If locale = "en" Then targetField += 1
            Dim myResult As String = ExecuteCsvSearch(SpellEnchantCsv, "EffectId", effectId.ToString(), targetField)(0)
            If myResult = "-" Then
                myResult = "Not found"
            End If
            Return myResult
            Dim points1 As Integer = 0
            Dim points2 As Integer = 0
            Dim points3 As Integer = 0
            Try
                points1 = CInt(ExecuteCsvSearch(SpellEnchantCsv, "EffectId", effectId.ToString(), 2)(0))
                points2 = CInt(ExecuteCsvSearch(SpellEnchantCsv, "EffectId", effectId.ToString(), 3)(0))
                points3 = CInt(ExecuteCsvSearch(SpellEnchantCsv, "EffectId", effectId.ToString(), 4)(0))
            Catch : End Try
            myResult = myResult.Replace("$k1", points1.ToString)
            myResult = myResult.Replace("$k2", points2.ToString)
            myResult = myResult.Replace("$k3", points3.ToString)
            If myResult.Contains("$") Then
                Try
                    myResult = myResult.Replace("$" & SplitString(myResult, "$", "s") & "s", "")
                Catch : End Try
            End If
            Return myResult
        End Function
    End Module
End Namespace