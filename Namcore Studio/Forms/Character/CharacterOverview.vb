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
Imports System.Drawing.Imaging
Imports Namcore_Studio.GlobalVariables
Imports Namcore_Studio.Basics
Imports Namcore_Studio.Conversions
Imports Namcore_Studio.SpellItem_Information
Imports System.Threading
Imports System.Resources
Imports System.Net

Public Class CharacterOverview
    Dim controlLST As List(Of Control)
    Dim pubItm As Item
    Dim tempValue As String
    Dim tempSender As Object
    Dim tmpSetId As Integer
    Dim tmpImage As Image
    Private Shared doneControls As List(Of Control)
    Dim Evaluator As Thread
    Shared loadComplete As Boolean = False
    Dim tmpSenderPic As Object
    Dim currentSet As Integer
    Private Sub CharacterOverview_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Delegate Sub Prep(ByVal id As Integer, ByVal nxt As Boolean)


    Public Sub prepare_interface(ByVal setId As Integer)
        InventoryPanel.SetDoubleBuffered()
        currentSet = setId
        Dim player As Character = GetCharacterSetBySetId(setId)
        If player.PlayerGlyphsIndex Is Nothing Then Glyphs_bt.Enabled = False
        doneControls = New List(Of Control)
        'Evaluator = New Thread(Sub() Me.goprep(setId, False))
        'Evaluator.Start()
        goprep(setId, False)
    End Sub
    Private Sub goprep(ByVal setId As Integer, ByVal nxt As Boolean)
        tmpSetId = setId
        controlLST = New List(Of Control)
        controlLST = FindAllChildren()
        Dim player As Character = GetCharacterSetBySetId(setId)
        charname_lbl.Text = player.Name
        level_lbl.Text = player.Level.ToString
        class_lbl.Text = GetClassNameById(player.Cclass)
        race_lbl.Text = GetRaceNameById(player.Race)
        If nxt = True Then controlLST.Reverse()
        Try
            For Each item_control As Control In controlLST
                item_control.SetDoubleBuffered()
                Dim tmpdone As List(Of Control) = doneControls
                If tmpdone.Contains(item_control) Then
                    Continue For
                Else
                    doneControls.Add(item_control)
                End If
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
                            If DirectCast(item_control, PictureBox).Image Is Nothing Then DirectCast(item_control, PictureBox).Image = My.Resources.empty
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
            Application.DoEvents()
            loadComplete = True
            Evaluator.Abort()
        Catch ex As Exception

        End Try


    End Sub
    Private Function loadInfo(ByVal targetSet As Integer, ByVal slot As Integer, ByVal infotype As Integer)
        Dim itm As Item = GetCharacterArmorItem(GetCharacterSetBySetId(targetSet), slot.ToString, True)
        pubItm = itm
        If itm Is Nothing Then Return Nothing
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
        addpanel.Location = newPoint
        addpanel.Location = newPoint
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
                    client.CheckProxy()
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

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Glyphs_bt.Click
        Glyphs_interface.Close()
        Dim glyphInterface As New Glyphs_interface
        Userwait.Show()
        Application.DoEvents()
        glyphInterface.prepareGlyphsInterface(tmpSetId)
        glyphInterface.Show()
        Userwait.Close()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        MsgBox(cnt.ToString())
    End Sub

    Private Sub exit_bt_Click(sender As System.Object, e As System.EventArgs) Handles exit_bt.Click
        Glyphs_interface.Close()
        Me.Close()
    End Sub

    Private Sub ItemClick(sender As System.Object, e As System.EventArgs) Handles slot_9_pic.Click, slot_8_pic.Click, slot_7_pic.Click, slot_6_pic.Click, slot_5_pic.Click, slot_4_pic.Click, slot_3_pic.Click, slot_2_pic.Click, slot_18_pic.Click, slot_17_pic.Click, slot_16_pic.Click, slot_15_pic.Click, slot_14_pic.Click, slot_13_pic.Click, slot_12_pic.Click, slot_11_pic.Click, slot_10_pic.Click, slot_1_pic.Click, slot_0_pic.Click
        Try
            Dim itemId As Integer = sender.tag.id
            Process.Start("http://wowhead.com/item=" & itemId.ToString())
        Catch ex As Exception

        End Try

        If Not tempSender Is Nothing Then
            tempSender.visible = True
        End If
        changepanel.Location = New System.Drawing.Point(4000, 4000)
        racepanel.Location = New System.Drawing.Point(4000, 4000)
        classpanel.Location = New System.Drawing.Point(4000, 4000)
        addpanel.Location = New System.Drawing.Point(4000, 4000)

     
            For Each ctrl As Control In controlLST
                If TypeOf ctrl Is Label Then
                If ctrl.Name.StartsWith(sender.name.replace("_pic", "")) And ctrl.Name.EndsWith("_name") Then
                    If ctrl.Text = "" Then
                        tempSender = ctrl
                        tmpSenderPic = sender
                        ctrl.Visible = False
                        Dim pnt As New System.Drawing.Point
                        pnt.X = ctrl.Location.X + InventoryPanel.Location.X
                        pnt.Y = ctrl.Location.Y + InventoryPanel.Location.Y
                        addpanel.Location = pnt
                    End If
                End If
            End If
        Next


    End Sub
    Dim cnt As Integer
    Private Sub slot_0_pic_MouseEnter(sender As System.Object, e As System.EventArgs) Handles slot_9_pic.MouseEnter, slot_8_pic.MouseEnter, slot_7_pic.MouseEnter, slot_6_pic.MouseEnter, slot_5_pic.MouseEnter, slot_4_pic.MouseEnter, slot_3_pic.MouseEnter, slot_2_pic.MouseEnter, slot_18_pic.MouseEnter, slot_17_pic.MouseEnter, slot_16_pic.MouseEnter, slot_15_pic.MouseEnter, slot_14_pic.MouseEnter, slot_13_pic.MouseEnter, slot_12_pic.MouseEnter, slot_11_pic.MouseEnter, slot_10_pic.MouseEnter, slot_1_pic.MouseEnter, slot_0_pic.MouseEnter

        If Not loadComplete = False Then
            If Not sender.image Is Nothing Then
                cnt += 1
                tmpImage = sender.image
                Application.DoEvents()
                Dim picbx As PictureBox = sender
                Dim g As Graphics
                Dim img As Image
                Dim r As Rectangle
                img = picbx.Image
                sender.Image = New Bitmap(picbx.Width, picbx.Height, PixelFormat.Format32bppArgb)
                g = Graphics.FromImage(picbx.Image)
                r = New Rectangle(0, 0, picbx.Width, picbx.Height)
                g.DrawImage(img, r)
                setBrightness(0.2, g, img, r, picbx)
            End If
        End If
    End Sub

    Private Sub slot_0_pic_MouseLeave(sender As System.Object, e As System.EventArgs) Handles slot_9_pic.MouseLeave, slot_8_pic.MouseLeave, slot_7_pic.MouseLeave, slot_6_pic.MouseLeave, slot_5_pic.MouseLeave, slot_4_pic.MouseLeave, slot_3_pic.MouseLeave, slot_2_pic.MouseLeave, slot_18_pic.MouseLeave, slot_17_pic.MouseLeave, slot_16_pic.MouseLeave, slot_15_pic.MouseLeave, slot_14_pic.MouseLeave, slot_13_pic.MouseLeave, slot_12_pic.MouseLeave, slot_11_pic.MouseLeave, slot_10_pic.MouseLeave, slot_1_pic.MouseLeave, slot_0_pic.MouseLeave
        If Not tmpImage Is Nothing And Not sender.image Is Nothing Then
            Dim picbox As PictureBox = sender
            picbox.Image = tmpImage
            picbox.Refresh()
            Application.DoEvents()
        End If

    End Sub
    Private Sub setBrightness(ByVal Brightness As Single, ByVal g As Graphics, ByVal img As Image, ByVal r As Rectangle, ByRef picbox As PictureBox)
        ' Brightness should be -1 (black) to 0 (neutral) to 1 (white)
        Dim colorMatrixVal As Single()() = { _
           New Single() {1, 0, 0, 0, 0}, _
           New Single() {0, 1, 0, 0, 0}, _
           New Single() {0, 0, 1, 0, 0}, _
           New Single() {0, 0, 0, 1, 0}, _
           New Single() {Brightness, Brightness, Brightness, 0, 1}}

        Dim colorMatrix As New ColorMatrix(colorMatrixVal)
        Dim ia As New ImageAttributes

        ia.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap)

        g.DrawImage(img, r, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia)
        picbox.Refresh()
    End Sub

    Private Sub CharacterOverview_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        changepanel.Location = New System.Drawing.Point(4000, 4000)
        addpanel.Location = New System.Drawing.Point(4000, 4000)
        racepanel.Location = New System.Drawing.Point(4000, 4000)
        classpanel.Location = New System.Drawing.Point(4000, 4000)
        If Not tempSender Is Nothing Then
            tempSender.visible = True
        End If
        TextBox1.Text = ""
        TextBox2.Text = ""
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        Dim senderPic As PictureBox = tmpSenderPic
        If Not TextBox2.Text = "" Then
            Dim meSlot As String = tempSender.name
            meSlot = meSlot.Replace("slot_", "") : meSlot = meSlot.Replace("_name", "")
            If Not GetSlotByItemId(TryInt(TextBox2.Text)) = TryInt(meSlot) Then
                Dim RM As New ResourceManager("Namcore_Studio.UserMessages", System.Reflection.Assembly.GetExecutingAssembly())
                MsgBox(RM.GetString("itemclassinvalid"), MsgBoxStyle.Critical, RM.GetString("Error"))
                Exit Sub
            Else
                Dim itm As New Item
                itm.id = TryInt(TextBox2.Text)
                itm.name = getNameOfItem(itm.id)
                itm.image = GetIconByItemId(itm.id)
                itm.rarity = GetRarityByItemId(itm.id)
                itm.slot = TryInt(meSlot)
                itm.slotname = GetSlotNameBySlotId(itm.slot)
                If itm.slot = 15 Or itm.slot = 16 Then LoadWeaponType(itm.id, currentSet)
                senderPic.Tag = itm
                DirectCast(senderPic, PictureBox).Image = itm.image
                senderPic.Refresh()
                DirectCast(tempSender, Label).Text = itm.name
                DirectCast(tempSender, Label).Tag = itm
                changepanel.Location = New System.Drawing.Point(4000, 4000)
                addpanel.Location = New System.Drawing.Point(4000, 4000)
                racepanel.Location = New System.Drawing.Point(4000, 4000)
                classpanel.Location = New System.Drawing.Point(4000, 4000)
                If Not tempSender Is Nothing Then
                    tempSender.visible = True
                End If
                TextBox1.Text = ""
                TextBox2.Text = ""
            End If
        End If
    End Sub

    Private Sub load_bt_Click(sender As Object, e As EventArgs) Handles av_bt.Click
        Achievements_interface.Close()
        Dim avinterface As New Achievements_interface
        Userwait.Show()
        Application.DoEvents()
        avinterface.prepareInterface(tmpSetId)
        avinterface.Show()
        Userwait.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Reputation_interface.Close()
        Dim repinterface As New Reputation_interface
        Userwait.Show()
        Application.DoEvents()
        repinterface.prepareRepInterface(tmpSetId)
        repinterface.Show()
        Userwait.Close()
    End Sub

    Private Sub Quests_bt_Click(sender As Object, e As EventArgs) Handles Quests_bt.Click
        Quests_interface.Close()
        Dim qstInterface As New Quests_interface
        Userwait.Show()
        Application.DoEvents()
        qstInterface.prepareInterface(tmpSetId)
        qstInterface.Show()
        Userwait.Close()
    End Sub
    Private ptMouseDownLocation As Point
    Private Sub me_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            ptMouseDownLocation = e.Location
        End If
    End Sub

    Private Sub me_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me.Location = e.Location - ptMouseDownLocation + Location
        End If
    End Sub
    Private Sub highlighter_MouseEnter(sender As Object, e As EventArgs) Handles highlighter1.MouseEnter, highlighter2.MouseEnter
        sender.backgroundimage = My.Resources.highlight
    End Sub

    Private Sub highlighter_MouseLeave(sender As Object, e As EventArgs) Handles highlighter1.MouseLeave, highlighter2.MouseLeave
        sender.backgroundimage = Nothing
    End Sub

    Private Sub highlighter2_Click(sender As Object, e As EventArgs) Handles highlighter2.Click
        Me.Close()
    End Sub

    Private Sub highlighter1_Click(sender As Object, e As EventArgs) Handles highlighter1.Click
        WindowState = FormWindowState.Minimized
    End Sub

   
    Private Sub header_MouseDown(sender As Object, e As MouseEventArgs) Handles header.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            ptMouseDownLocation = e.Location
        End If
    End Sub


    Private Sub header_MouseMove(sender As Object, e As MouseEventArgs) Handles header.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me.Location = e.Location - ptMouseDownLocation + Location
        End If
    End Sub
End Class