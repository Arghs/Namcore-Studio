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
Imports System.Net

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
        charname_lbl.Text = GetTemporaryCharacterInformation("@character_name", setId)
        level_lbl.Text = GetTemporaryCharacterInformation("@character_level", setId)
        class_lbl.Text = GetClassNameById(TryInt(GetTemporaryCharacterInformation("@character_class", setId)))
        race_lbl.Text = GetRaceNameById(GetTemporaryCharacterInformation("@character_race", setId))
        For Each item_control As Control In controlLST
            Select Case True
                Case TypeOf item_control Is Label
                    If item_control.Name.ToLower.Contains("_name") Then
                        Dim slot As Integer = TryInt(splitString(item_control.Name, "slot_", "_name"))
                        Dim txt As String = loadInfo(setId, slot, 0)
                        If Not txt Is Nothing Then
                            If txt.Length >= 25 Then
                                Dim ccremove As Integer = txt.Length - 23
                                txt = txt.Remove(23, ccremove) & "..."
                            End If
                        End If
                        DirectCast(item_control, Label).Text = txt
                        DirectCast(item_control, Label).Tag = pubItm
                        DirectCast(item_control, Label).Cursor = Windows.Forms.Cursors.IBeam
                    ElseIf item_control.Name.ToLower.EndsWith("enchant") Then
                        Dim slot As Integer = TryInt(splitString(item_control.Name, "slot_", "_enchant"))
                        Dim txt As String = loadInfo(setId, slot, 1)
                        If Not txt Is Nothing Then
                            If txt.Length >= 25 Then
                                Dim ccremove As Integer = txt.Length - 23
                                txt = txt.Remove(23, ccremove) & "..."
                            End If
                        End If
                        If txt Is Nothing Then
                            txt = "+"
                            DirectCast(item_control, Label).Cursor = Windows.Forms.Cursors.Hand
                        ElseIf txt = "" Then
                            txt = "+"
                            DirectCast(item_control, Label).Cursor = Windows.Forms.Cursors.Hand
                        Else
                            DirectCast(item_control, Label).Cursor = Windows.Forms.Cursors.IBeam
                        End If
                        DirectCast(item_control, Label).Text = txt
                        DirectCast(item_control, Label).Tag = pubItm
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
        If sender.text = "" Then Exit Sub
        Dim newPoint As New System.Drawing.Point
        newPoint.X = sender.location.X + InventoryPanel.Location.X
        newPoint.Y = sender.location.Y + InventoryPanel.Location.Y
        changepanel.Location = newPoint
        newPoint.X = 4000
        newPoint.Y = 4000
        classpanel.Location = newPoint
        racepanel.Location = newPoint
        PictureBox2.Visible = True
        If Not tempSender Is Nothing Then
            tempSender.visible = True
        End If
        tempSender = sender
        sender.visible = False
        If sender.name.contains("_name") Then
            TextBox1.Text = sender.tag.id.ToString
        ElseIf sender.name.contains("_lbl") Then
            TextBox1.Text = sender.text
        Else

            TextBox1.Text = sender.tag.enchantment_id.ToString
        End If
        tempValue = TextBox1.Text
    End Sub

    Private Sub PictureBox1_Click(sender As System.Object, e As System.EventArgs) Handles PictureBox1.Click
        Dim newPoint As New System.Drawing.Point
        Dim senderLabel As Label = tempSender
        newPoint.X = 4000
        newPoint.Y = 4000
        If Not TextBox1.Text = tempValue Then
            If TypeOf tempSender Is Label Then
                Dim id As Integer = TryInt(TextBox1.Text)
                Dim RM As New ResourceManager("Namcore_Studio.UserMessages", System.Reflection.Assembly.GetExecutingAssembly())
                If senderLabel.Name.ToLower.EndsWith("charname_lbl") Then
                    If TextBox1.Text = "" Then

                    Else
                        senderLabel.Text = TextBox1.Text
                    End If
                ElseIf senderLabel.Name.ToLower.EndsWith("level_lbl") Then
                    If TextBox1.Text = "" Then

                    Else
                        Dim newlvl As Integer = TryInt(TextBox1.Text)
                        If newlvl = 0 Then

                        Else
                            senderLabel.Text = TextBox1.Text
                        End If
                    End If
                ElseIf senderLabel.Name.ToLower.EndsWith("_enchant") Then
                    Dim client As New WebClient
                    Dim spellcontext As String
                    Dim foundspell As Boolean = False
                    Dim spellname As String
                    Dim itemcontext As String
                    Dim founditem As Boolean = False
                    Dim itemname As String
                    Try
                        spellcontext = client.DownloadString("http://www.wowhead.com/spell=" & TextBox1.Text)
                    Catch ex As Exception
                        spellcontext = ""
                    End Try
                    If Not spellcontext = "" And Not spellcontext.Contains("<div id=""inputbox-error"">This spell doesn't exist.</div>") And spellcontext.Contains(""">Enchant Item") Then
                        foundspell = True
                        spellname = splitString(spellcontext, "<meta property=""og&#x3A;title"" content=""", """ />")
                        spellname = spellname.Replace("&#x20;", " ")
                    End If
                    Try
                        itemcontext = client.DownloadString("http://www.wowhead.com/item=" & TextBox1.Text & "&xml")
                    Catch ex As Exception
                        itemcontext = ""
                    End Try
                    If Not itemcontext = "" And Not itemcontext.Contains("<div id=""inputbox-error"">This item doesn't exist or is not yet in the database.</div>") And itemcontext.Contains("subclass id=""6"">") Then
                        founditem = True
                        itemname = splitString(itemcontext, "<name><![CDATA[", "]]></name>")
                        itemname = itemname.Replace("&#x20;", " ")
                    End If
                    If founditem = foundspell = True Then
                        selectenchpanel.Location = changepanel.Location
                        spellench.Text = "Spell: " & spellname
                        spellench.Tag = spellname
                        itmench.Text = "Item: " & itemname
                        itmench.Tag = itemname
                    ElseIf founditem = True Then
                        senderLabel.Text = itemname
                        senderLabel.Tag.enchantment_type = 1
                        senderLabel.Tag.enchantment_id = TextBox1.Text
                        senderLabel.Tag.enchantment_name = itemname
                    ElseIf foundspell = True Then
                        senderLabel.Text = spellname
                        senderLabel.Tag.enchantment_type = 0
                        senderLabel.Tag.enchantment_id = TextBox1.Text
                        senderLabel.Tag.enchantment_name = spellname
                    Else

                    End If
                Else
                    If Not GetSlotByItemId(tempSender.tag.id) = GetSlotByItemId(id) Then
                        MsgBox(RM.GetString("itemclassinvalid"), MsgBoxStyle.Critical, RM.GetString("Error"))
                    Else
                        Dim newitm As Item = tempSender.tag
                        newitm.ReplaceItem(id)
                        senderLabel.Tag = newitm
                        Dim txt As String = senderLabel.Tag.name
                        If Not txt Is Nothing Then
                            If txt.Length >= 25 Then
                                Dim ccremove As Integer = txt.Length - 23
                                txt = txt.Remove(23, ccremove) & "..."
                            End If
                        End If
                        senderLabel.Text = txt
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
        End If
        changepanel.Location = newPoint
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

    Private Sub PictureBox2_Click(sender As System.Object, e As System.EventArgs) Handles PictureBox2.Click
        Dim newPoint As New System.Drawing.Point
        Dim senderLabel As Label = tempSender
        newPoint.X = 4000
        newPoint.Y = 4000

        If TypeOf tempSender Is Label Then
            If Not tempSender.text.tolower.endswith("_enchant") Then
                Dim RM As New ResourceManager("Namcore_Studio.UserMessages", System.Reflection.Assembly.GetExecutingAssembly())
                Dim result = MsgBox(RM.GetString("deleteitem"), vbYesNo, RM.GetString("areyousure"))
                If result = Microsoft.VisualBasic.MsgBoxResult.Yes Then

                    For Each ctrl As Control In controlLST
                        If TypeOf ctrl Is PictureBox Then
                            If ctrl.Tag Is Nothing Then Continue For
                            If ctrl.Tag.id = senderLabel.Tag.id Then
                                DirectCast(ctrl, PictureBox).Tag = senderLabel.Tag
                                Select Case True
                                    Case ctrl.Name.ToLower.EndsWith("_pic") And Not ctrl.Name.ToLower.Contains("gem")
                                        DirectCast(ctrl, PictureBox).Image = My.Resources.empty
                                    Case ctrl.Name.ToLower.Contains("gem1")
                                        DirectCast(ctrl, PictureBox).Image = Nothing
                                    Case ctrl.Name.ToLower.Contains("gem2")
                                        DirectCast(ctrl, PictureBox).Image = Nothing
                                    Case ctrl.Name.ToLower.Contains("gem3")
                                        DirectCast(ctrl, PictureBox).Image = Nothing
                                End Select
                            End If
                        ElseIf TypeOf ctrl Is Panel Then
                            If ctrl.Tag Is Nothing Then Continue For
                            If ctrl.Tag.id = senderLabel.Tag.id Then
                                If ctrl.Name.ToLower.EndsWith("color") Then
                                    DirectCast(ctrl, Panel).BackColor = SystemColors.ActiveBorder
                                    DirectCast(ctrl, Panel).Tag = Nothing
                                End If
                            End If
                        ElseIf TypeOf ctrl Is Label Then
                            If ctrl.Tag Is Nothing Then Continue For
                            If ctrl.Tag.id = senderLabel.Tag.id Then
                                If ctrl.Name.ToLower.EndsWith("_enchant") Then
                                    DirectCast(ctrl, Label).Tag = Nothing
                                    DirectCast(ctrl, Label).Text = ""
                                End If
                            End If
                        End If

                    Next
                    senderLabel.Text = Nothing
                    senderLabel.Tag = Nothing
                End If
            Else
                DirectCast(tempSender, Label).Tag = Nothing
                DirectCast(tempSender, Label).Text = ""
            End If
        End If
        changepanel.Location = newPoint
        senderLabel.Visible = True
    End Sub

    Private Sub race_lbl_Click(sender As System.Object, e As System.EventArgs) Handles race_lbl.Click
        racepanel.Location = sender.location
        Dim newpoint As New Point
        newPoint.X = 4000
        newPoint.Y = 4000
        classpanel.Location = newPoint
        changepanel.Location = newpoint
        PictureBox2.Visible = False
        If Not tempSender Is Nothing Then
            tempSender.visible = True
        End If
        tempSender = sender
        sender.visible = False
        racecombo.Text = sender.text
        tempValue = racecombo.Text
    End Sub

    Private Sub class_lbl_Click(sender As System.Object, e As System.EventArgs) Handles class_lbl.Click
        classpanel.Location = sender.location
        Dim newpoint As New Point
        newpoint.X = 4000
        newpoint.Y = 4000
        racepanel.Location = newpoint
        changepanel.Location = newpoint
        PictureBox2.Visible = False
        If Not tempSender Is Nothing Then
            tempSender.visible = True
        End If
        tempSender = sender
        sender.visible = False
        classcombo.Text = sender.text
        tempValue = classcombo.Text
    End Sub

    Private Sub charname_lbl_Click(sender As System.Object, e As System.EventArgs) Handles charname_lbl.Click
        changepanel.Location = sender.location
        Dim newpoint As New Point
        newpoint.X = 4000
        newpoint.Y = 4000
        classpanel.Location = newpoint
        racepanel.Location = newpoint
        PictureBox2.Visible = False
        If Not tempSender Is Nothing Then
            tempSender.visible = True
        End If
        tempSender = sender
        sender.visible = False
        TextBox1.Text = sender.text
        tempValue = TextBox1.Text
    End Sub

    Private Sub level_lbl_Click(sender As System.Object, e As System.EventArgs) Handles level_lbl.Click
        changepanel.Location = sender.location
        Dim newpoint As New Point
        newpoint.X = 4000
        newpoint.Y = 4000
        classpanel.Location = newpoint
        racepanel.Location = newpoint
        PictureBox2.Visible = False
        If Not tempSender Is Nothing Then
            tempSender.visible = True
        End If
        tempSender = sender
        sender.visible = False
        TextBox1.Text = sender.text
        tempValue = TextBox1.Text
    End Sub

    Private Sub classrefresh_Click(sender As System.Object, e As System.EventArgs) Handles classrefresh.Click
        Dim newPoint As New System.Drawing.Point
        Dim senderLabel As Label = tempSender
        newPoint.X = 4000
        newPoint.Y = 4000
        If Not classcombo.SelectedText = tempValue And Not classcombo.Text = tempValue Then
            senderLabel.Text = classcombo.SelectedText
        End If
        classpanel.Location = newPoint
        senderLabel.Visible = True
    End Sub

    Private Sub racerefresh_Click(sender As System.Object, e As System.EventArgs) Handles racerefresh.Click
        Dim newPoint As New System.Drawing.Point
        Dim senderLabel As Label = tempSender
        newPoint.X = 4000
        newPoint.Y = 4000
        If Not racecombo.SelectedText = tempValue And Not racecombo.Text = tempValue Then
            senderLabel.Text = racecombo.SelectedText
        End If
        racepanel.Location = newPoint
        senderLabel.Visible = True
    End Sub

    Private Sub itmench_Click(sender As System.Object, e As System.EventArgs) Handles itmench.Click
        Dim newPoint As New System.Drawing.Point
        Dim senderLabel As Label = tempSender
        newPoint.X = 4000
        newPoint.Y = 4000
        senderLabel.Text = itmench.Tag
        senderLabel.Tag.enchantment_type = 1
        senderLabel.Tag.enchantment_id = TextBox1.Text
        senderLabel.Tag.enchantment_name = itmench.Tag
        selectenchpanel.Location = newPoint
    End Sub

    Private Sub spellench_Click(sender As System.Object, e As System.EventArgs) Handles spellench.Click
        Dim newPoint As New System.Drawing.Point
        Dim senderLabel As Label = tempSender
        newPoint.X = 4000
        newPoint.Y = 4000
        senderLabel.Text = spellench.Tag
        senderLabel.Tag.enchantment_type = 0
        senderLabel.Tag.enchantment_id = TextBox1.Text
        senderLabel.Tag.enchantment_name = spellench.Tag
        selectenchpanel.Location = newPoint
    End Sub

    Private Sub bagpanel_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles bagpanel.Paint

    End Sub
End Class