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
'*      /Filename:      GlyphsInterface
'*      /Description:   Provides an interface to display character's glyphs
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.Drawing.Imaging
Imports NCFramework.Framework.Extension
Imports NCFramework.My
Imports NCFramework.Framework.Functions
Imports NamCore_Studio.Modules.Interface
Imports NCFramework.Framework.Modules
Imports NamCore_Studio.Forms.Extension
Imports libnc.Provider
Imports System.Net

Namespace Forms.Character
    Public Class GlyphsInterface
        Inherits EventTrigger

        '// Declaration
        Dim _controlLst As List(Of Control)
        Dim _pubGlyph As Glyph
        Dim _tempValue As String
        Dim _tempSender As Object
        Dim _tmpPic As Image
        Dim _tmpSenderPic As Object
        Dim _usePlayer As NCFramework.Framework.Modules.Character
        '// Declaration

        Private Sub Glyphs_interface_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            AddHandler highlighter2.Click, AddressOf highlighter2_Click
            Dim controlLst As List(Of Control)
            controlLst = FindAllChildren()
            For Each itemControl As Control In controlLst
                itemControl.SetDoubleBuffered()
            Next
        End Sub

        Public Sub PrepareGlyphsInterface(ByVal setId As Integer, ByVal account As Account)
            Dim player As NCFramework.Framework.Modules.Character
            If GlobalVariables.currentEditedCharSet Is Nothing Then
                player = DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet)
            Else
                player = DeepCloneHelper.DeepClone(GlobalVariables.currentEditedCharSet)
            End If
            If player.PlayerGlyphs Is Nothing Then player.PlayerGlyphs = New List(Of Glyph)
            _usePlayer = player
            _controlLst = New List(Of Control)
            _controlLst = FindAllChildren()
            For Each itemControl As Control In _controlLst
                Select Case True
                    Case TypeOf itemControl Is Label
                        If itemControl.Name.ToLower.EndsWith("_name") Then
                            Dim tempSlotName As String = ""
                            If itemControl.Name.ToLower.StartsWith("sec_") Then tempSlotName = "sec"
                            If itemControl.Name.ToLower.Contains("prim") Then tempSlotName = tempSlotName & "primglyph"
                            If itemControl.Name.ToLower.Contains("major") Then _
                                tempSlotName = tempSlotName & "majorglyph"
                            If itemControl.Name.ToLower.Contains("minor") Then _
                                tempSlotName = tempSlotName & "minorglyph"
                            If itemControl.Name.ToLower.Contains("1") Then tempSlotName = tempSlotName & "1"
                            If itemControl.Name.ToLower.Contains("2") Then tempSlotName = tempSlotName & "2"
                            If itemControl.Name.ToLower.Contains("3") Then tempSlotName = tempSlotName & "3"
                            Dim txt As String = LoadInfo(tempSlotName, 0)
                            DirectCast(itemControl, Label).Tag = _pubGlyph
                            DirectCast(itemControl, Label).Cursor = Cursors.IBeam
                            If _pubGlyph Is Nothing Then Continue For
                            If txt Is Nothing Then
                                txt = GetItemNameByItemId(_pubGlyph.Id, MySettings.Default.language)
                            End If
                            If Not txt Is Nothing Then
                                If txt.Length >= 30 Then
                                    Dim ccremove As Integer = txt.Length - 28
                                    txt = txt.Remove(28, ccremove) & "..."
                                End If
                                txt = txt.Replace("""", "")
                            End If
                            DirectCast(itemControl, Label).Text = txt
                        End If
                    Case TypeOf itemControl Is PictureBox
                        If itemControl.Name.ToLower.EndsWith("_pic") Then
                            Dim tempSlotName As String = ""
                            If itemControl.Name.ToLower.StartsWith("sec_") Then tempSlotName = "sec"
                            If itemControl.Name.ToLower.Contains("prim") Then tempSlotName = tempSlotName & "primglyph"
                            If itemControl.Name.ToLower.Contains("major") Then _
                                tempSlotName = tempSlotName & "majorglyph"
                            If itemControl.Name.ToLower.Contains("minor") Then _
                                tempSlotName = tempSlotName & "minorglyph"
                            If itemControl.Name.ToLower.Contains("1") Then tempSlotName = tempSlotName & "1"
                            If itemControl.Name.ToLower.Contains("2") Then tempSlotName = tempSlotName & "2"
                            If itemControl.Name.ToLower.Contains("3") Then tempSlotName = tempSlotName & "3"
                            Dim img As Image = LoadInfo(tempSlotName, 1)
                            If img Is Nothing Then
                                DirectCast(itemControl, PictureBox).Image = My.Resources.empty
                            Else
                                DirectCast(itemControl, PictureBox).Image = img
                            End If
                            DirectCast(itemControl, PictureBox).Tag = _pubGlyph
                        End If
                End Select
            Next
        End Sub

        Private Function LoadInfo(ByVal slot As String, ByVal infotype As Integer) As Object
            Dim glyphitm As Glyph = GetCharacterGlyph(_usePlayer, slot)
            _pubGlyph = glyphitm
            If glyphitm Is Nothing Then Return Nothing
            Select Case infotype
                Case 0 : Return glyphitm.name
                Case 1
                    If glyphitm.Image Is Nothing Then
                        Return GetItemIconByDisplayId(GetDisplayIdByItemId(glyphitm.Id), GlobalVariables.GlobalWebClient)
                    Else
                        Return glyphitm.Image
                    End If
                Case Else : Return Nothing
            End Select
        End Function

        Private Sub sec_minor_3_pic_Click(sender As Object, e As EventArgs) _
            Handles sec_prim_3_pic.Click, sec_prim_2_pic.Click, sec_prim_1_pic.Click, sec_minor_3_pic.Click,
                    sec_minor_2_pic.Click, sec_minor_1_pic.Click, sec_major_3_pic.Click, sec_major_2_pic.Click,
                    sec_major_1_pic.Click, prim_3_pic.Click, prim_2_pic.Click, prim_1_pic.Click, minor_3_pic.Click,
                    minor_2_pic.Click, minor_1_pic.Click, major_3_pic.Click, major_2_pic.Click, major_1_pic.Click
            If Not _tempSender Is Nothing Then
                _tempSender.visible = True
            End If
            changepanel.Location = New Point(4000, 4000)
            addpanel.Location = New Point(4000, 4000)
            Dim gly As Glyph = sender.tag
            If Not gly Is Nothing Then
                If Not gly.id = 0 Then
                    Process.Start("http://wowhead.com/item=" & gly.id)
                End If
            Else
                If Not _tempSender Is Nothing Then
                    _tempSender.visible = True
                End If
                For Each ctrl As Control In _controlLst
                    If TypeOf ctrl Is Label Then
                        If ctrl.Name.StartsWith(sender.name.replace("_pic", "")) Then
                            _tempSender = ctrl
                            _tmpSenderPic = sender
                            ctrl.Visible = False
                            addpanel.Location = New Point(ctrl.Location.X + glyph_panel.Location.X,
                                                          ctrl.Location.Y + glyph_panel.Location.Y)
                        End If
                    End If
                Next

            End If
        End Sub

        Private Sub SetBrightness(ByVal brightness As Single, ByVal g As Graphics, ByVal img As Image,
                                  ByVal r As Rectangle,
                                  ByRef picbox As PictureBox)
            ' Brightness should be -1 (black) to 0 (neutral) to 1 (white)
            Dim colorMatrixVal As Single()() = { _
                                                   New Single() {1, 0, 0, 0, 0},
                                                   New Single() {0, 1, 0, 0, 0},
                                                   New Single() {0, 0, 1, 0, 0},
                                                   New Single() {0, 0, 0, 1, 0},
                                                   New Single() {brightness, brightness, brightness, 0, 1}}

            Dim colorMatrix As New ColorMatrix(colorMatrixVal)
            Dim ia As New ImageAttributes

            ia.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap)

            g.DrawImage(img, r, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia)
            picbox.Refresh()
        End Sub

        Private Sub sec_minor_3_pic_MouseEnter(sender As Object, e As EventArgs) _
            Handles sec_prim_3_pic.MouseEnter, sec_prim_2_pic.MouseEnter, sec_prim_1_pic.MouseEnter,
                    sec_minor_3_pic.MouseEnter, sec_minor_2_pic.MouseEnter, sec_minor_1_pic.MouseEnter,
                    sec_major_3_pic.MouseEnter, sec_major_2_pic.MouseEnter, sec_major_1_pic.MouseEnter,
                    prim_3_pic.MouseEnter, prim_2_pic.MouseEnter, prim_1_pic.MouseEnter, minor_3_pic.MouseEnter,
                    minor_2_pic.MouseEnter, minor_1_pic.MouseEnter, major_3_pic.MouseEnter, major_2_pic.MouseEnter,
                    major_1_pic.MouseEnter
            If Not sender.image Is Nothing Then
                _tmpPic = sender.image
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
        End Sub

        Private Sub sec_minor_3_pic_MouseLeave(sender As Object, e As EventArgs) _
            Handles sec_prim_3_pic.MouseLeave, sec_prim_2_pic.MouseLeave, sec_prim_1_pic.MouseLeave,
                    sec_minor_3_pic.MouseLeave, sec_minor_2_pic.MouseLeave, sec_minor_1_pic.MouseLeave,
                    sec_major_3_pic.MouseLeave, sec_major_2_pic.MouseLeave, sec_major_1_pic.MouseLeave,
                    prim_3_pic.MouseLeave, prim_2_pic.MouseLeave, prim_1_pic.MouseLeave, minor_3_pic.MouseLeave,
                    minor_2_pic.MouseLeave, minor_1_pic.MouseLeave, major_3_pic.MouseLeave, major_2_pic.MouseLeave,
                    major_1_pic.MouseLeave
            If Not _tmpPic Is Nothing And Not sender.image Is Nothing Then
                Dim picbox As PictureBox = sender
                picbox.Image = _tmpPic
                picbox.Refresh()
                Application.DoEvents()
            End If
        End Sub

        Private Sub sec_prim_3_name_Click(sender As Object, e As EventArgs) _
            Handles sec_prim_3_name.Click, sec_prim_2_name.Click, sec_prim_1_name.Click, sec_minor_3_name.Click,
                    sec_minor_2_name.Click, sec_minor_1_name.Click, sec_major_3_name.Click, sec_major_2_name.Click,
                    sec_major_1_name.Click, prim_3_name.Click, prim_2_name.Click, prim_1_name.Click, minor_3_name.Click,
                    minor_2_name.Click, minor_1_name.Click, major_3_name.Click, major_2_name.Click, major_1_name.Click
            If sender.text = "" Or Not sender.name.contains("_name") Then Exit Sub
            Dim newPoint As New Point
            newPoint.X = sender.location.X + glyph_panel.Location.X
            newPoint.Y = sender.location.Y + glyph_panel.Location.Y
            changepanel.Location = newPoint
            addpanel.Location = New Point(4000, 4000)
            newPoint.X = 4000
            newPoint.Y = 4000
            PictureBox2.Visible = True
            If Not _tempSender Is Nothing Then
                _tempSender.visible = True
            End If
            _tempSender = sender
            sender.visible = False
            TextBox1.Text = sender.Tag.Id.ToString
            _tempValue = TextBox1.Text
        End Sub

        Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
            '// Delete glyph
            Dim newPoint As New Point
            Dim senderLabel As Label = _tempSender
            Dim senderTag As Glyph = DeepCloneHelper.DeepClone(senderLabel.Tag)
            newPoint.X = 4000
            newPoint.Y = 4000
            If TypeOf _tempSender Is Label Then
                Dim result = MsgBox(ResourceHandler.GetUserMessage("deleteitem"), vbYesNo,
                                    ResourceHandler.GetUserMessage("areyousure"))
                If result = MsgBoxResult.Yes Then

                    For Each ctrl As Control In _controlLst
                        If TypeOf ctrl Is PictureBox Then
                            If ctrl.Name.EndsWith("_pic") Then
                                If ctrl.Tag Is Nothing Then Continue For
                                If ctrl.Tag.Id = senderTag.Id And ctrl.Tag.Spec = senderTag.Spec Then
                                    Select Case True
                                        Case ctrl.Name.ToLower.EndsWith("_pic")
                                            DirectCast(ctrl, PictureBox).Tag = Nothing
                                            DirectCast(ctrl, PictureBox).Image = My.Resources.empty
                                    End Select
                                End If
                            End If
                        ElseIf TypeOf ctrl Is Label Then
                            If ctrl.Name.EndsWith("_name") Then
                                If ctrl.Tag Is Nothing Then Continue For
                                If ctrl.Tag.Id = senderTag.Id And ctrl.Tag.Spec = senderTag.Spec Then
                                    If ctrl.Name.ToLower.EndsWith("_name") Then
                                        DirectCast(ctrl, Label).Tag = Nothing
                                        DirectCast(ctrl, Label).Text = ""
                                    End If
                                End If
                            End If
                        End If
                    Next
                    If GlobalVariables.currentEditedCharSet Is Nothing Then
                        GlobalVariables.currentEditedCharSet =
                            DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet)
                    End If
                    Dim glyphIndex As Integer =
                            GlobalVariables.currentViewedCharSet.PlayerGlyphs.FindIndex(
                                Function(glyph) glyph.Slotname = senderTag.Slotname)
                    If glyphIndex <> - 1 Then
                        GlobalVariables.currentEditedCharSet.PlayerGlyphs.RemoveAt(glyphIndex)
                    End If
                    senderLabel.Text = Nothing
                    senderLabel.Tag = Nothing
                End If
            End If
            changepanel.Location = newPoint
            senderLabel.Visible = True
        End Sub

        Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
            '// Change glyph
            Dim newPoint As New Point
            Dim senderLabel As Label = _tempSender
            Dim senderTag As Glyph = DeepCloneHelper.DeepClone(_tempSender.Tag)
            newPoint.X = 4000
            newPoint.Y = 4000
            If Not TextBox1.Text = _tempValue Then
                If TypeOf _tempSender Is Label Then
                    Dim id As Integer = TryInt(TextBox1.Text)

                    If senderLabel.Name.ToLower.EndsWith("_name") Then
                        If Not GetItemInventorySlotByItemId(senderTag.Id) = GetItemInventorySlotByItemId(id) Then
                            MsgBox(ResourceHandler.GetUserMessage("itemclassinvalid"), MsgBoxStyle.Critical,
                                   ResourceHandler.GetUserMessage("Error"))
                        Else
                            Dim newGlyph As New Glyph
                            newGlyph.Id = id
                            newGlyph.Name = GetItemNameByItemId(id.ToString, MySettings.Default.language)
                            newGlyph.Image = GetItemIconByDisplayId(GetDisplayIdByItemId(id), GlobalVariables.GlobalWebClient)
                            newGlyph.Spec = senderTag.Spec
                            newGlyph.Slotname = senderTag.Slotname
                            newGlyph.Type = senderTag.Type
                            senderLabel.Tag = newGlyph
                            Dim txt As String = newGlyph.Name
                            If Not txt Is Nothing Then
                                If txt.Length >= 30 Then
                                    Dim ccremove As Integer = txt.Length - 28
                                    txt = txt.Remove(28, ccremove) & "..."
                                End If
                            End If
                            senderLabel.Text = txt
                            For Each ctrl As Control In _controlLst
                                Dim pictureBox = TryCast(ctrl, PictureBox)
                                If (pictureBox IsNot Nothing) Then
                                    If ctrl.Tag Is Nothing Then Continue For
                                    If ctrl.Tag.id = senderTag.Id Then
                                        pictureBox.Tag = senderTag
                                        Select Case True
                                            Case ctrl.Name.ToLower.EndsWith("_pic")
                                                pictureBox.Image = senderTag.Image
                                        End Select
                                    End If
                                End If

                            Next
                            If GlobalVariables.currentEditedCharSet Is Nothing Then _
                                GlobalVariables.currentEditedCharSet =
                                    DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet)
                            SetCharacterGlyph(GlobalVariables.currentEditedCharSet, newGlyph)
                        End If
                    End If

                End If
            End If
            changepanel.Location = newPoint
            senderLabel.Visible = True
        End Sub

        Private Sub Glyphs_interface_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
            changepanel.Location = New Point(4000, 4000)
            addpanel.Location = New Point(4000, 4000)
            If Not _tempSender Is Nothing Then
                _tempSender.visible = True
            End If
            TextBox1.Text = ""
            TextBox2.Text = ""
        End Sub

        Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
            '// Add glyph
            Dim senderPic As PictureBox = _tmpSenderPic
            If Not TextBox2.Text = "" Then
                Dim client As New WebClient
                client.CheckProxy()
                Dim qCode As String = client.DownloadString("http://wowhead.com/item=" & TextBox2.Text)
                If Not qCode.Contains("Glyph") Then

                    MsgBox(ResourceHandler.GetUserMessage("glyphnotfound"), MsgBoxStyle.Critical,
                           ResourceHandler.GetUserMessage("Error"))
                    Exit Sub
                Else
                    Dim gly As New Glyph
                    gly.id = TryInt(TextBox2.Text)
                    gly.Name = GetItemNameByItemId(gly.Id, MySettings.Default.language)
                    gly.Image = GetItemIconByDisplayId(GetDisplayIdByItemId(gly.Id), GlobalVariables.GlobalWebClient)
                    gly.slotname = ""
                    Dim slot As String = ""
                    If senderPic.Name.Contains("minor") Then gly.type = 1 : slot = "minor"
                    If senderPic.Name.Contains("major") Then gly.type = 2 : slot = "major"
                    If senderPic.Name.Contains("prim") Then gly.type = 3 : slot = "prim"
                    If senderPic.Name.Contains("sec") Then gly.spec = 1 : gly.slotname = "sec" Else gly.spec = 0
                    gly.slotname = gly.slotname & slot & "glyph"
                    If senderPic.Name.Contains("_1_") Then gly.slotname = gly.slotname & "1"
                    If senderPic.Name.Contains("_2_") Then gly.slotname = gly.slotname & "2"
                    If senderPic.Name.Contains("_3_") Then gly.slotname = gly.slotname & "3"
                    senderPic.Tag = gly
                    senderPic.Refresh()
                    If GlobalVariables.currentEditedCharSet Is Nothing Then _
                        GlobalVariables.currentEditedCharSet =
                            DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet)
                    AddCharacterGlyph(GlobalVariables.currentEditedCharSet, gly)
                    senderPic.Image = gly.image
                    DirectCast(_tempSender, Label).Text = gly.name
                    DirectCast(_tempSender, Label).Tag = gly
                    changepanel.Location = New Point(4000, 4000)
                    addpanel.Location = New Point(4000, 4000)
                    If Not _tempSender Is Nothing Then
                        _tempSender.visible = True
                    End If
                    TextBox1.Text = ""
                    TextBox2.Text = ""
                End If
            End If
        End Sub

        Private Sub highlighter2_Click(sender As Object, e As EventArgs)
            Close()
        End Sub
    End Class
End Namespace