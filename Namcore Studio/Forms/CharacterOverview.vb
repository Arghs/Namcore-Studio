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
Imports Namcore_Studio.WinformControlExtensions
Public Class CharacterOverview
    Dim controlLST As List(Of Control)
    Private Sub CharacterOverview_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        controlLST = New List(Of Control)
        controlLST = InventoryPanel.FindAllChildren
    End Sub
  
   
   
    Public Sub prepare_interface(ByVal setId As Integer)
        For Each item_control As Control In controlLST
            Select Case True
                Case TypeOf item_control Is Label
                    If Not item_control.Name.ToLower.Contains("enchant") Then
                        Dim slot As Integer = TryInt(splitString(item_control.Name, "slot_", "_name"))
                        DirectCast(item_control, Label).Text = loadInfo(setId, slot, 0)
                    Else
                        Dim slot As Integer = TryInt(splitString(item_control.Name, "slot_", "_enchant"))
                        DirectCast(item_control, Label).Text = loadInfo(setId, slot, 1)
                    End If
                Case TypeOf item_control Is PictureBox
                    If Not item_control.Name.ToLower.Contains("gem") Then
                        Dim slot As Integer = TryInt(splitString(item_control.Name, "slot_", "_pic"))
                        DirectCast(item_control, PictureBox).Image = loadInfo(setId, slot, 2)
                    Else
                        Dim slot As Integer = TryInt(splitString(item_control.Name, "slot_", "_gem"))
                        Dim gem As Integer = TryInt(splitString(item_control.Name, "gem", "_pic"))
                        DirectCast(item_control, PictureBox).Image = loadInfo(setId, slot, 2 + gem)
                    End If
                Case TypeOf item_control Is Panel
                    If Not item_control.Name.ToLower.EndsWith("panel") Then
                        Dim slot As Integer = TryInt(splitString(item_control.Name, "slot_", "_color"))
                        DirectCast(item_control, Panel).BackColor = loadInfo(setId, slot, 6)
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
                If itm.socket1_pic Is Nothing Then
                    Return Nothing
                Else
                    Return itm.socket1_pic
                End If
            Case 4
                If itm.socket2_pic Is Nothing Then
                    Return Nothing
                Else
                    Return itm.socket2_pic
                End If
            Case 5
                If itm.socket3_pic Is Nothing Then
                    Return Nothing
                Else
                    Return itm.socket3_pic
                End If
            Case 6
                Select Case itm.rarity
                    Case 0, 1 : Return System.Drawing.Color.Gray
                    Case 0, 1 : Return System.Drawing.Color.White
                    Case 2 : Return System.Drawing.Color.Green
                    Case 3 : Return System.Drawing.Color.DarkBlue
                    Case 4 : Return System.Drawing.Color.Violet
                    Case 5 : Return System.Drawing.Color.Orange
                    Case 6 : Return System.Drawing.Color.Gold
                    Case Else : Return System.Drawing.Color.Green
                End Select
            Case Else : Return Nothing
        End Select
    End Function
End Class