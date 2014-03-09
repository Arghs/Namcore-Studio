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
'*      /Filename:      AchievementInfo
'*      /Description:   Provides achievement information
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports libnc.Main
Namespace Provider
    Public Module AchievementInfo
        Public Function GetAvNameById(ByVal avId As Integer, ByVal locale As String) As String
            CheckInit()
            Dim targetField As Integer = 1
            If locale = "en" Then
                targetField += 1
            End If
            Dim myResult As String = ExecuteCsvSearch(AchievementCsv, "AchievementId", avId.ToString(), targetField)(0)
            If myResult = "-" Then myResult = "Not found"
            Return myResult
        End Function
        Public Function GetAvDescriptionById(ByVal avId As Integer, ByVal locale As String) As String
            CheckInit()
            Dim targetField As Integer = 3
            If locale = "en" Then
                targetField += 1
            End If
            Dim myResult As String = ExecuteCsvSearch(AchievementCsv, "AchievementId", avId.ToString(), targetField)(0)
            If myResult = "-" Then myResult = "Not found"
            Return myResult
        End Function
        Public Function GetAvSubCategoryById(ByVal avId As Integer) As Integer
            CheckInit()
            Const targetField As Integer = 5
            Dim myResult As String = ExecuteCsvSearch(AchievementCsv, "AchievementId", avId.ToString(), targetField)(0)
            Dim returnResult As Integer
            If myResult = "-" Then myResult = "0"
            Try
                returnResult = CInt(myResult)
            Catch
                returnResult = 0
            End Try
            Return returnResult
        End Function
        Public Function GetAvSpellIdById(ByVal avId As Integer) As Integer
            CheckInit()
            Const targetField As Integer = 6
            Dim myResult As String = ExecuteCsvSearch(AchievementCsv, "AchievementId", avId.ToString(), targetField)(0)
            Dim returnResult As Integer
            If myResult = "-" Then myResult = "0"
            Try
                returnResult = CInt(myResult)
            Catch
                returnResult = 0
            End Try
            Return returnResult
        End Function
    End Module
End Namespace