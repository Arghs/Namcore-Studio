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
Imports Namcore_Studio.SpellItem_Information
Imports System.Resources

Public Class CharacterOverview
    Dim controlLST As List(Of Control)
    Dim pubItm As Item
    Dim tempValue As String
    Dim tempSender As Object
    Private Sub CharacterOverview_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub



    Public Sub prepare_interface(ByVal setId As Integer)
        controlLST = New List(Of Control)
        controlLST = FindAllChildren()
        For Each item_control As Control In controlLST
            Select Case True
                Case TypeOf item_control Is Label
                    If item_control.Name.ToLower.Contains("_name") Then
                        Dim slot As Integer = TryInt(splitString(item_control.Name, "slot_", "_name"))
                        DirectCast(item_control, Label).Text = loadInfo(setId, slot, 0)
                        DirectCast(item_control, Label).Tag = pubItm
                        DirectCast(item_control, Label).Cursor = Windows.Forms.Cursors.IBeam
                    ElseIf item_control.Name.ToLower.EndsWith("enchant") Then
                        Dim slot As Integer = TryInt(splitString(item_control.Name, "slot_", "_enchant"))
                        DirectCast(item_control, Label).Text = loadInfo(setId, slot, 1)
                        DirectCast(item_control, Label).Tag = pubItm
                        DirectCast(item_control, Label).Cursor = Windows.Forms.Cursors.IBeam
                    End If
                Case TypeOf item_control Is PictureBox
                    If item_control.Name.ToLower.Contains("_pic") And Not item_control.Name.ToLower.Contains("gem") Then
                        Dim slot As Integer = TryInt(splitString(item_control.Name, "slot_", "_pic"))
                        DirectCast(item_control, PictureBox).Image = loadInfo(setId, slot, 2)
                        DirectCast(item_control, PictureBox).Tag = pubItm
                    ElseIf item_control.Name.ToLower.Contains("gem") Then
                        Dim slot As Integer = TryInt(splitString(item_control.Name, "slot_", "_gem"))
                        Dim gem As Integer = TryInt(splitString(item_control.Name, "gem", "_pic"))
                        DirectCast(item_control, PictureBox).Image = loadInfo(setId, slot, 2 + gem)
                        DirectCast(item_control, PictureBox).Tag = pubItm
                    End If
                Case TypeOf item_control Is Panel
                    If item_control.Name.ToLower.EndsWith("color") Then
                        Dim slot As Integer = TryInt(splitString(item_control.Name, "slot_", "_color"))
                        DirectCast(item_control, Panel).BackColor = loadInfo(setId, slot, 6)
                        DirectCast(item_control, Panel).Tag = pubItm
                    End If

            End Select
        Next

    End Sub
    Private Function loadInfo(ByVal targetSet As Integer, ByVal slot As Integer, ByVal infotype As Integer)
        Dim itm As Item = GetTCI_Item(slot.ToString, targetSet)
        pubItm = itm
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
                    Case 2 : Return System.Drawing.Color.LightGreen
                    Case 3 : Return System.Drawing.Color.DodgerBlue
                    Case 4 : Return System.Drawing.Color.DarkViolet
                    Case 5 : Return System.Drawing.Color.Orange
                    Case 6 : Return System.Drawing.Color.Gold
                    Case Else : Return System.Drawing.Color.LawnGreen
                End Select
            Case Else : Return Nothing
        End Select
    End Function

    Private Sub label_Click(sender As System.Object, e As System.EventArgs) Handles slot_9_name.Click, slot_9_enchant.Click, slot_8_name.Click, slot_8_enchant.Click, slot_7_name.Click, slot_7_enchant.Click, slot_6_name.Click, slot_6_enchant.Click, slot_5_name.Click, slot_5_enchant.Click, slot_4_name.Click, slot_4_enchant.Click, slot_3_name.Click, slot_3_enchant.Click, slot_2_name.Click, slot_2_enchant.Click, slot_18_name.Click, slot_18_enchant.Click, slot_17_name.Click, slot_17_enchant.Click, slot_16_name.Click, slot_16_enchant.Click, slot_15_name.Click, slot_15_enchant.Click, slot_14_name.Click, slot_14_enchant.Click, slot_13_name.Click, slot_13_enchant.Click, slot_12_name.Click, slot_12_enchant.Click, slot_11_name.Click, slot_11_enchant.Click, slot_10_name.Click, slot_10_enchant.Click, slot_1_name.Click, slot_1_enchant.Click, slot_0_name.Click, slot_0_enchant.Click
        Dim newPoint As New System.Drawing.Point
        newPoint.X = sender.location.X + InventoryPanel.Location.X
        newPoint.Y = sender.location.Y + InventoryPanel.Location.Y
        changePanel.Location = newPoint
        If Not tempSender Is Nothing Then
            tempSender.visible = True
        End If
        tempSender = sender
        sender.visible = False
        If sender.name.contains("_name") Then
            TextBox1.Text = sender.tag.id.ToString
        Else
            TextBox1.Text = sender.tag.enchantment_id.ToString
        End If
        tempValue = TextBox1.Text
    End Sub

    Private Sub PictureBox1_Click(sender As System.Object, e As System.EventArgs) Handles PictureBox1.Click
        Dim newPoint As New System.Drawing.Point
        Dim tmpsender As Object = tempSender
        Dim senderLabel As Label = tempSender
        newPoint.X = 4000
        newPoint.Y = 4000
        If Not TextBox1.Text = tempValue Then
            If TypeOf tempSender Is Label Then
                Dim id As Integer = TryInt(TextBox1.Text)
                Dim RM As New ResourceManager("Namcore_Studio.UserMessages", System.Reflection.Assembly.GetExecutingAssembly())
                If Not GetSlotByItemId(tempSender.tag.id) = GetSlotByItemId(id) Then
                    MsgBox(RM.GetString("itemclassinvalid"), MsgBoxStyle.Critical, RM.GetString("Error"))
                Else
                    Dim newitm As Item = tempSender.tag
                    newitm.ReplaceItem(id)
                    senderLabel.Tag = newitm
                    senderLabel.Text = senderLabel.Tag.name
                    For Each ctrl As Control In controlLST
                        If TypeOf ctrl Is PictureBox Then
                            If ctrl.Tag Is Nothing Then Continue For
                            If ctrl.Tag.id = senderLabel.Tag.id Then
                                DirectCast(ctrl, PictureBox).Tag = senderLabel.Tag
                                Select Case True
                                    Case ctrl.Name.ToLower.EndsWith("_pic") And Not ctrl.Name.ToLower.Contains("gem")
                                        DirectCast(ctrl, PictureBox).Image = senderLabel.Tag.image
                                    Case ctrl.Name.ToLower.Contains("gem1")
                                        DirectCast(ctrl, PictureBox).Image = senderLabel.Tag.socket1_pic
                                    Case ctrl.Name.ToLower.Contains("gem2")
                                        DirectCast(ctrl, PictureBox).Image = senderLabel.Tag.socket2_pic
                                    Case ctrl.Name.ToLower.Contains("gem3")
                                        DirectCast(ctrl, PictureBox).Image = senderLabel.Tag.socket3_pic
                                End Select
                            End If
                        ElseIf TypeOf ctrl Is Panel Then
                            If ctrl.Tag Is Nothing Then Continue For
                            If ctrl.Tag.id = senderLabel.Tag.id Then
                                If ctrl.Name.ToLower.EndsWith("color") Then
                                    DirectCast(ctrl, Panel).BackColor = getraritycolor(senderLabel.Tag.rarity)
                                    DirectCast(ctrl, Panel).Tag = senderLabel.Tag
                                End If
                            End If
                        ElseIf TypeOf ctrl Is Label Then
                            If ctrl.Tag Is Nothing Then Continue For
                            If ctrl.Tag.id = senderLabel.Tag.id Then
                                If ctrl.Name.ToLower.EndsWith("_enchant") Then
                                    DirectCast(ctrl, Label).Tag = senderLabel.Tag
                                End If
                            End If
                        End If

                    Next

                End If
            End If
        End If
        changePanel.Location = newPoint
        senderLabel.visible = True
    End Sub
    Private Function getraritycolor(ByVal rarity As Integer) As System.Drawing.Color
        Select Case rarity
            Case 0, 1 : Return System.Drawing.Color.Gray
            Case 0, 1 : Return System.Drawing.Color.White
            Case 2 : Return System.Drawing.Color.LightGreen
            Case 3 : Return System.Drawing.Color.DodgerBlue
            Case 4 : Return System.Drawing.Color.DarkViolet
            Case 5 : Return System.Drawing.Color.Orange
            Case 6 : Return System.Drawing.Color.Gold
            Case Else : Return System.Drawing.Color.LawnGreen
        End Select
    End Function
End Class