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
'*      /Filename:      Glyphs_interface
'*      /Description:   Provides an interface to display character's questlog
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Imports Namcore_Studio.Basics
Imports Namcore_Studio.Conversions
Imports Namcore_Studio.SpellItem_Information
Imports System.Threading


Public Class Quests_interface
    Shared addlst As New List(Of ListViewItem)
    Shared addlst2 As New List(Of ListViewItem)
    Shared arr1 As Array
    Shared arr2 As Array
   
    Public Sub prepareInterface(ByVal setId As Integer)
        Dim p As Character = GetCharacterSetBySetId(setId)
        Dim qst() As String = p.FinishedQuests.Split(","c)
        Dim cnt As Integer = 0
        Dim par1 As Integer = qst.Length / 2
        'Dim trd As New Thread(AddressOf dopart1)
        'Dim trd2 As New Thread(AddressOf dopart2)
        'trd.IsBackground = True
        'trd2.IsBackground = True
        'arr1 = {0, par1, qst}
        'arr2 = {par1 + 1, qst.Length, qst}
        'trd.Start()
        ''trd2.Start()
        'While trd.IsAlive
        '    'wait
        'End While
        'While trd2.IsAlive
        '    'wait
        'End While
        'For Each itm As ListViewItem In addlst
        '    qst_lst.Items.Add(itm)
        'Next
        'For Each itm As ListViewItem In addlst2
        '    qst_lst.Items.Add(itm)
        'Next
        While cnt < qst.Length
            Dim str(3) As String
            str(0) = qst(cnt)
            Dim qstname As String = GetQuestNameById(TryInt(str(0)))
            If qstname = "error" Then
                str(1) = "not loaded" 'getNameOfQuest(str(0))
            Else
                str(1) = qstname
            End If

            str(2) = "1"
            str(3) = "1"
            Dim itm As New ListViewItem(str)
            itm.Tag = TryInt(str(0))
            qst_lst.Items.Add(itm)
            cnt += 1
        End While
    End Sub
    Private Sub dopart1()
        Dim startend As Array = arr1
        Dim cnt As Integer = startend(0)
        Dim qst() As String = startend(2)
        While cnt < startend(1)
            Dim str(3) As String
            str(0) = qst(cnt)

            Dim qstname As String = GetQuestNameById(TryInt(str(0)))
            If qstname = "error" Then
                str(1) = "not loaded" 'getNameOfQuest(str(0))
            Else
                str(1) = qstname
            End If

            str(2) = "1"
            str(3) = "1"
            Dim itm As New ListViewItem(str)
            itm.Tag = TryInt(str(0))
            addlst.Add(itm)
            cnt += 1
        End While
    End Sub
    Private Sub dopart2()
        Thread.Sleep(15000)
        Dim startend As Array = arr2
        Dim cnt As Integer = startend(0)
        Dim qst() As String = startend(2)
        While cnt < startend(1)
            Dim str(3) As String
            str(0) = qst(cnt)

            Dim qstname As String = GetQuestNameById(TryInt(str(0)))
            If qstname = "error" Then
                str(1) = "not loaded" 'getNameOfQuest(str(0))
            Else
                str(1) = qstname
            End If

            str(2) = "1"
            str(3) = "1"
            Dim itm As New ListViewItem(str)
            itm.Tag = TryInt(str(0))
            addlst2.Add(itm)
            cnt += 1
        End While
    End Sub
    Private Sub Quests_interface_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class