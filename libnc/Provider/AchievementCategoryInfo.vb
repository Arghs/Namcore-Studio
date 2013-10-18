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
'*      /Filename:      AchievementCategoryInfo
'*      /Description:   Provides achievement category information
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports libnc.Main
Namespace Provider
    Public Module AchievementCategoryInfo
        Public Function GetAvMainCategoryIdBySubCatId(ByVal catId As Integer) As Integer
            CheckInit()
            Const targetField As Integer = 1
            Dim myResult As Integer = ExecuteCsvSearch(AchievementCategoryCsv, "CategoryId", catId.ToString(), targetField)(0)
            Dim returnResult As Integer
            If myResult = "-" Then myResult = 0
            Try
                returnResult = CInt(myResult)
            Catch
                returnResult = 0
            End Try
            If returnResult = -1 Then returnResult = catId '// SubcategoryId = MainCategoryId
            Return returnResult
        End Function
        Public Function GetAvCatNameById(ByVal catId As Integer, ByVal locale As String) As String
            CheckInit()
            Dim targetField As Integer = 2
            If locale = "en" Then
                targetField += 1
            End If
            Dim myResult As String = ExecuteCsvSearch(AchievementCategoryCsv, "CategoryId", catId.ToString(), targetField)(0)
            If myResult = "-" Then myResult = "Not found"
            Return myResult
        End Function
        Public Function GetAvIdListByMainCat(ByVal mainCatid As Integer) As List(Of Integer)
            CheckInit()
            Const targetField As Integer = 0
            Dim subCategoryList As New List(Of Integer)
            Dim myResult As String() = ExecuteCsvSearch(AchievementCategoryCsv, "MainCatId", mainCatid.ToString(), targetField)
            If myResult(0) = "-" Then
                subCategoryList.Add(mainCatid)
            Else
                For i = 0 To myResult.Length - 1
                    Try
                        If Not myResult(i) Is Nothing Then subCategoryList.Add(CInt(myResult(i)))
                    Catch : End Try
                Next i
            End If
            Dim myNextResults As New List(Of Integer)
            For i = 0 To subCategoryList.Count - 1
                Dim myNextResult As String() = ExecuteCsvSearch(AchievementCsv, "CategoryId", subCategoryList(i), 0)
                If myNextResult(0) = "-" Then Return Nothing
                For z = 0 To myNextResult.Length
                    Try
                        myNextResults.Add(CInt(myNextResult(z)))
                    Catch : End Try
                Next z
            Next i
            Return myNextResults
        End Function
    End Module
End Namespace