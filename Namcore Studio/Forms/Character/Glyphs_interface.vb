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
'*      /Description:   Provides an interface to display character's glyphs
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Imports Namcore_Studio_Framework.Conversions
Imports Namcore_Studio_Framework.Basics
Imports System.Drawing.Imaging
Imports System.Resources
Imports Namcore_Studio_Framework.SpellItem_Information
Imports Namcore_Studio_Framework
Imports System.Net

Public Class Glyphs_interface
    Dim controlLST As List(Of Control)
    Dim pubGlyph As Glyph
    Dim tempValue As String
    Dim tempSender As Object
    Dim tmpPic As Image
    Dim tmpSenderPic As Object




    Private Sub Glyphs_interface_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim controlLST As List(Of Control)
        controlLST = FindAllChildren()
        For Each item_control As Control In controlLST
            item_control.SetDoubleBuffered()
        Next
    End Sub
    Public Sub prepareGlyphsInterface(ByVal setId As Integer)
        controlLST = New List(Of Control)
        controlLST = FindAllChildren()
        For Each item_control As Control In controlLST
            Select Case True
                Case TypeOf item_control Is Label
                    If item_control.Name.ToLower.EndsWith("_name") Then
                        Dim tempSlotName As String = ""
                        If item_control.Name.ToLower.StartsWith("sec_") Then tempSlotName = "sec"
                        If item_control.Name.ToLower.Contains("prim") Then tempSlotName = tempSlotName & "primglyph"
                        If item_control.Name.ToLower.Contains("major") Then tempSlotName = tempSlotName & "majorglyph"
                        If item_control.Name.ToLower.Contains("minor") Then tempSlotName = tempSlotName & "minorglyph"
                        If item_control.Name.ToLower.Contains("1") Then tempSlotName = tempSlotName & "1"
                        If item_control.Name.ToLower.Contains("2") Then tempSlotName = tempSlotName & "2"
                        If item_control.Name.ToLower.Contains("3") Then tempSlotName = tempSlotName & "3"
                        Dim txt As String = loadInfo(setId, tempSlotName, 0)
                        If Not txt Is Nothing Then
                            If txt.Length >= 30 Then
                                Dim ccremove As Integer = txt.Length - 28
                                txt = txt.Remove(28, ccremove) & "..."
                            End If
                            txt = txt.Replace("""", "")
                        End If
                        DirectCast(item_control, Label).Text = txt
                        DirectCast(item_control, Label).Tag = pubGlyph
                        DirectCast(item_control, Label).Cursor = Windows.Forms.Cursors.IBeam
                    End If
                Case TypeOf item_control Is PictureBox
                    If item_control.Name.ToLower.EndsWith("_pic") Then
                        Dim tempSlotName As String = ""
                        If item_control.Name.ToLower.StartsWith("sec_") Then tempSlotName = "sec"
                        If item_control.Name.ToLower.Contains("prim") Then tempSlotName = tempSlotName & "primglyph"
                        If item_control.Name.ToLower.Contains("major") Then tempSlotName = tempSlotName & "majorglyph"
                        If item_control.Name.ToLower.Contains("minor") Then tempSlotName = tempSlotName & "minorglyph"
                        If item_control.Name.ToLower.Contains("1") Then tempSlotName = tempSlotName & "1"
                        If item_control.Name.ToLower.Contains("2") Then tempSlotName = tempSlotName & "2"
                        If item_control.Name.ToLower.Contains("3") Then tempSlotName = tempSlotName & "3"
                        Dim img As Image = loadInfo(setId, tempSlotName, 1)
                        If img Is Nothing Then
                            DirectCast(item_control, PictureBox).Image = My.Resources.empty
                        Else
                            DirectCast(item_control, PictureBox).Image = img
                        End If

                        DirectCast(item_control, PictureBox).Tag = pubGlyph
                    End If
            End Select
        Next
    End Sub
    Private Function loadInfo(ByVal targetSet As Integer, ByVal slot As String, ByVal infotype As Integer) As Object
        Dim glyphitm As Glyph = GetCharacterGlyph(GetCharacterSetBySetId(targetSet), slot)
        pubGlyph = glyphitm
        If glyphitm Is Nothing Then Return Nothing
        Select Case infotype
            Case 0 : Return glyphitm.name
            Case 1
                If glyphitm.image Is Nothing Then
                    Return My.Resources.empty
                Else
                    Return glyphitm.image
                End If
            Case Else : Return Nothing
        End Select
    End Function

    Private Sub sec_minor_3_pic_Click(sender As Object, e As EventArgs) Handles sec_prim_3_pic.Click, sec_prim_2_pic.Click, sec_prim_1_pic.Click, sec_minor_3_pic.Click, sec_minor_2_pic.Click, sec_minor_1_pic.Click, sec_major_3_pic.Click, sec_major_2_pic.Click, sec_major_1_pic.Click, prim_3_pic.Click, prim_2_pic.Click, prim_1_pic.Click, minor_3_pic.Click, minor_2_pic.Click, minor_1_pic.Click, major_3_pic.Click, major_2_pic.Click, major_1_pic.Click
        If Not tempSender Is Nothing Then
            tempSender.visible = True
        End If
        changepanel.Location = New System.Drawing.Point(4000, 4000)
        addpanel.Location = New System.Drawing.Point(4000, 4000)
        Dim gly As Glyph = sender.tag
        If Not gly Is Nothing Then
            If Not gly.id = 0 Then
                Process.Start("http://wowhead.com/item=" & gly.id)
            End If
        Else
            If Not tempSender Is Nothing Then
                tempSender.visible = True
            End If
            For Each ctrl As Control In controlLST
                If TypeOf ctrl Is Label Then
                    If ctrl.Name.StartsWith(sender.name.replace("_pic", "")) Then
                        tempSender = ctrl
                        tmpSenderPic = sender
                        ctrl.Visible = False
                        addpanel.Location = ctrl.Location
                    End If
                End If
            Next

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

    Private Sub sec_minor_3_pic_MouseEnter(sender As Object, e As EventArgs) Handles sec_prim_3_pic.MouseEnter, sec_prim_2_pic.MouseEnter, sec_prim_1_pic.MouseEnter, sec_minor_3_pic.MouseEnter, sec_minor_2_pic.MouseEnter, sec_minor_1_pic.MouseEnter, sec_major_3_pic.MouseEnter, sec_major_2_pic.MouseEnter, sec_major_1_pic.MouseEnter, prim_3_pic.MouseEnter, prim_2_pic.MouseEnter, prim_1_pic.MouseEnter, minor_3_pic.MouseEnter, minor_2_pic.MouseEnter, minor_1_pic.MouseEnter, major_3_pic.MouseEnter, major_2_pic.MouseEnter, major_1_pic.MouseEnter
        If Not sender.image Is Nothing Then
            tmpPic = sender.image
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

    Private Sub sec_minor_3_pic_MouseLeave(sender As Object, e As EventArgs) Handles sec_prim_3_pic.MouseLeave, sec_prim_2_pic.MouseLeave, sec_prim_1_pic.MouseLeave, sec_minor_3_pic.MouseLeave, sec_minor_2_pic.MouseLeave, sec_minor_1_pic.MouseLeave, sec_major_3_pic.MouseLeave, sec_major_2_pic.MouseLeave, sec_major_1_pic.MouseLeave, prim_3_pic.MouseLeave, prim_2_pic.MouseLeave, prim_1_pic.MouseLeave, minor_3_pic.MouseLeave, minor_2_pic.MouseLeave, minor_1_pic.MouseLeave, major_3_pic.MouseLeave, major_2_pic.MouseLeave, major_1_pic.MouseLeave
        If Not tmpPic Is Nothing And Not sender.image Is Nothing Then
            Dim picbox As PictureBox = sender
            picbox.Image = tmpPic
            picbox.Refresh()
            Application.DoEvents()
        End If
    End Sub

    Private Sub sec_prim_3_name_Click(sender As Object, e As EventArgs) Handles sec_prim_3_name.Click, sec_prim_2_name.Click, sec_prim_1_name.Click, sec_minor_3_name.Click, sec_minor_2_name.Click, sec_minor_1_name.Click, sec_major_3_name.Click, sec_major_2_name.Click, sec_major_1_name.Click, prim_3_name.Click, prim_2_name.Click, prim_1_name.Click, minor_3_name.Click, minor_2_name.Click, minor_1_name.Click, major_3_name.Click, major_2_name.Click, major_1_name.Click
        If sender.text = "" Or Not sender.name.contains("_name") Then Exit Sub
        Dim newPoint As New System.Drawing.Point
        newPoint.X = sender.location.X
        newPoint.Y = sender.location.Y
        changepanel.Location = newPoint
        addpanel.Location = New System.Drawing.Point(4000, 4000)
        newPoint.X = 4000
        newPoint.Y = 4000
        PictureBox2.Visible = True
        If Not tempSender Is Nothing Then
            tempSender.visible = True
        End If
        tempSender = sender
        sender.visible = False
        TextBox1.Text = sender.tag.id.ToString
        tempValue = TextBox1.Text
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Dim newPoint As New System.Drawing.Point
        Dim senderLabel As Label = tempSender
        Dim tag As Glyph = senderLabel.Tag
        newPoint.X = 4000
        newPoint.Y = 4000
        If TypeOf tempSender Is Label Then
            Dim RM As New ResourceManager("Namcore_Studio_Framework.UserMessages", System.Reflection.Assembly.GetExecutingAssembly())
            Dim result = MsgBox(RM.GetString("deleteitem"), vbYesNo, RM.GetString("areyousure"))
            If result = Microsoft.VisualBasic.MsgBoxResult.Yes Then

                For Each ctrl As Control In controlLST
                    If TypeOf ctrl Is PictureBox Then
                        If ctrl.Name.EndsWith("_pic") Then
                            If ctrl.Tag Is Nothing Then Continue For
                            If ctrl.Tag.id = tag.id Then
                                DirectCast(ctrl, PictureBox).Tag = senderLabel.Tag
                                Select Case True
                                    Case ctrl.Name.ToLower.EndsWith("_pic")
                                        DirectCast(ctrl, PictureBox).Image = My.Resources.empty
                                End Select
                            End If
                        End If
                    ElseIf TypeOf ctrl Is Label Then
                        If ctrl.Name.EndsWith("_name") Then
                            If ctrl.Tag Is Nothing Then Continue For
                            If ctrl.Tag.id = tag.id Then
                                If ctrl.Name.ToLower.EndsWith("_name") Then
                                    DirectCast(ctrl, Label).Tag = Nothing
                                    DirectCast(ctrl, Label).Text = ""
                                End If
                            End If
                        End If
                    End If
                Next
                senderLabel.Text = Nothing
                senderLabel.Tag = Nothing
            End If
        End If
        changepanel.Location = newPoint
        senderLabel.Visible = True
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Dim newPoint As New System.Drawing.Point
        Dim senderLabel As Label = tempSender
        Dim tag As Glyph = tempSender.tag
        newPoint.X = 4000
        newPoint.Y = 4000
        If Not TextBox1.Text = tempValue Then
            If TypeOf tempSender Is Label Then
                Dim id As Integer = TryInt(TextBox1.Text)
                Dim RM As New ResourceManager("Namcore_Studio_Framework.UserMessages", System.Reflection.Assembly.GetExecutingAssembly())
                If senderLabel.Name.ToLower.EndsWith("_name") Then
                    If Not GetSlotByItemId(tag.id) = GetSlotByItemId(id) Then
                        MsgBox(RM.GetString("itemclassinvalid"), MsgBoxStyle.Critical, RM.GetString("Error"))
                    Else
                        Dim newGlyph As New Glyph
                        newGlyph.id = id
                        newGlyph.name = getNameOfItem(id.ToString)
                        newGlyph.image = GetIconByItemId(id)
                        newGlyph.spec = tag.spec
                        newGlyph.slotname = tag.slotname
                        newGlyph.type = tag.type
                        senderLabel.Tag = newGlyph
                        Dim txt As String = newGlyph.name
                        If Not txt Is Nothing Then
                            If txt.Length >= 30 Then
                                Dim ccremove As Integer = txt.Length - 28
                                txt = txt.Remove(28, ccremove) & "..."
                            End If
                        End If
                        senderLabel.Text = txt
                        For Each ctrl As Control In controlLST
                            If TypeOf ctrl Is PictureBox Then
                                If ctrl.Tag Is Nothing Then Continue For
                                If ctrl.Tag.id = senderLabel.Tag.id Then
                                    DirectCast(ctrl, PictureBox).Tag = senderLabel.Tag
                                    Select Case True
                                        Case ctrl.Name.ToLower.EndsWith("_pic")
                                            DirectCast(ctrl, PictureBox).Image = tag.image
                                    End Select
                                End If
                            End If

                        Next

                    End If
                End If

            End If
        End If
        changepanel.Location = newPoint
        senderLabel.Visible = True
    End Sub

    Private Sub Glyphs_interface_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        changepanel.Location = New System.Drawing.Point(4000, 4000)
        addpanel.Location = New System.Drawing.Point(4000, 4000)
        If Not tempSender Is Nothing Then
            tempSender.visible = True
        End If
        TextBox1.Text = ""
        TextBox2.Text = ""
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        Dim senderPic As PictureBox = tmpSenderPic
        If Not TextBox2.Text = "" Then
            Dim client As New WebClient
            client.CheckProxy()
            Dim qCode As String = client.DownloadString("http://wowhead.com/item=" & TextBox2.Text)
            If Not qCode.Contains("Glyph") Then
                Dim RM As New ResourceManager("Namcore_Studio_Framework.UserMessages", System.Reflection.Assembly.GetExecutingAssembly())
                MsgBox(RM.GetString("glyphnotfound"), MsgBoxStyle.Critical, RM.GetString("Error"))
                Exit Sub
            Else
                Dim gly As New Glyph
                gly.id = TryInt(TextBox2.Text)
                gly.name = getNameOfItem(gly.id)
                gly.image = GetIconByItemId(gly.id)
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
                DirectCast(senderPic, PictureBox).Image = gly.image
                DirectCast(tempSender, Label).Text = gly.name
                DirectCast(tempSender, Label).Tag = gly
                changepanel.Location = New System.Drawing.Point(4000, 4000)
                addpanel.Location = New System.Drawing.Point(4000, 4000)
                If Not tempSender Is Nothing Then
                    tempSender.visible = True
                End If
                TextBox1.Text = ""
                TextBox2.Text = ""
            End If
        End If
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

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class