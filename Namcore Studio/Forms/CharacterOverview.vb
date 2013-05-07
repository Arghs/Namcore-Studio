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
'*      /Filename:      CharacterOverview
'*      /Description:   Displays character information
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports Namcore_Studio.GlobalVariables
Imports Namcore_Studio.Basics
Imports Namcore_Studio.Conversions
Public Class CharacterOverview

    Private Sub CharacterOverview_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Private Sub prepare_interface(ByVal setId As Integer)
        For Each item_control As Control In Me.Controls
            Select Case True
                Case TypeOf item_control Is Label
                    If Not item_control.Name.Contains("enchant") Then
                        Dim slot As Integer = tryint(splitString(item_control.Name, "slot_", "_name"))
                        DirectCast(item_control, Label).Text = loadInfo(setId, slot, 0)
                    Else
                        Dim slot As Integer = TryInt(splitString(item_control.Name, "slot_", "_enchant"))
                        DirectCast(item_control, Label).Text = loadInfo(setId, slot, 1)
                    End If
                Case TypeOf item_control Is PictureBox
                    If Not item_control.Name.Contains("gem") Then
                        Dim slot As Integer = TryInt(splitString(item_control.Name, "slot_", "_pic"))
                        DirectCast(item_control, Label).Text = loadInfo(setId, slot, 2)
                    Else
                        Dim slot As Integer = TryInt(splitString(item_control.Name, "slot_", "_gem"))
                        Dim gem As Integer = TryInt(splitString(item_control.Name, "gem", "_pic"))
                        DirectCast(item_control, Label).Text = loadInfo(setId, slot, 2 + gem)
                    End If
                Case TypeOf item_control Is Panel
                    If Not item_control.Name.EndsWith("panel") Then
                        Dim slot As Integer = TryInt(splitString(item_control.Name, "slot_", "_color"))
                        DirectCast(item_control, Label).Text = loadInfo(setId, slot, 6)
                    End If
            End Select
        Next
       
    End Sub
    Private Function loadInfo(ByVal targetSet As Integer, ByVal slot As Integer, ByVal infotype As Integer)
        Dim itm As Item = GetTCI_Item(slot.ToString, targetSet)
        Select Case infotype
            Case 0 : Return itm.name
            Case 1 : Return itm.enchantment_name
            Case 2
                If itm.image Is Nothing Then
                    Return My.Resources.empty
                Else
                    Return itm.image
                End If
            Case 3

        End Select
    End Function
End Class