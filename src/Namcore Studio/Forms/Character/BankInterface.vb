'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013 NamCore Studio <https://github.com/megasus/Namcore-Studio>
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
'*      /Filename:      BankInterface
'*      /Description:   Provides an interface to display character bank information
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.Drawing.Imaging
Imports NamCore_Studio.Modules.Interface
Imports NCFramework.Framework.Extension
Imports NCFramework.Framework.Modules
Imports NCFramework.Framework.Functions
Imports NCFramework
Imports NCFramework.Framework.Logging
Imports NamCore_Studio.Forms.Extension
Imports libnc.Provider
Imports System.Threading

Namespace Forms.Character
    Public Class BankInterface
        Inherits EventTrigger
        '// Declaration
        Private ReadOnly _context As SynchronizationContext = SynchronizationContext.Current
        Public Event Completed As EventHandler(Of CompletedEventArgs)
        Delegate Sub AddLayoutControlDelegate(layout As FlowLayoutPanel, ctrl As Control)
        Dim _tmpImage As Image
        '// Declaration

        Protected Overridable Sub OnCompleted(ByVal e As CompletedEventArgs)
            RaiseEvent Completed(Me, e)
        End Sub
        Public Sub PrepareBankInterface(ByVal setId As Integer)
            Hide()
            NewProcessStatus()
            Userwait.Show()
            LogAppend("Loading bank items", "BankInterface_PrepareBankInterface", True)
            InfoToolTip.AutoPopDelay = 5000
            InfoToolTip.InitialDelay = 1000
            InfoToolTip.ReshowDelay = 500
            Dim controlLst As List(Of Control)
            controlLst = FindAllChildren()
            For Each itemControl As Control In controlLst
                itemControl.SetDoubleBuffered()
            Next
            Dim trd As New Thread(AddressOf DoWork)
            trd.Start(GlobalVariables.currentViewedCharSet)
        End Sub
        Private Sub DoWork(ByVal charSet As Framework.Modules.Character)
            For i = 39 To 66
                Dim newItemPanel As New Panel
                newItemPanel.Name = "bankitm_slot_" & i.ToString & "_panel"
                newItemPanel.Size = reference_itm_panel.Size
                newItemPanel.Padding = reference_itm_panel.Padding
                newItemPanel.Margin = reference_itm_panel.Margin
                newItemPanel.BackColor = reference_itm_panel.BackColor
                Dim newItemPic As New PictureBox
                newItemPic.Name = "bankitm_slot_" & i.ToString & "_pic"
                newItemPic.Size = reference_itm_pic.Size
                newItemPic.BackgroundImage = reference_itm_pic.BackgroundImage
                newItemPic.BackgroundImageLayout = reference_itm_pic.BackgroundImageLayout
                newItemPanel.Controls.Add(newItemPic)
                newItemPic.Location = reference_itm_pic.Location
                AddHandler newItemPic.MouseHover, AddressOf OnItem_MouseOver
                BankLayoutPanel.BeginInvoke(New AddLayoutControlDelegate(AddressOf DelegateLayoutControlAdding), BankLayoutPanel, newItemPanel)
            Next i
            If Not GlobalVariables.currentViewedCharSet.InventoryZeroItems Is Nothing Then
                For Each potCharBag As Item In GlobalVariables.currentViewedCharSet.InventoryZeroItems
                    potCharBag.BagItems = New List(Of Item)()
                    Select Case potCharBag.Slot
                        Case 67 To 73
                            For Each subctrl As Control In BackPanel.Controls
                                If subctrl.Name.Contains((potCharBag.Slot - 66).ToString()) Then
                                    If subctrl.Name.ToLower.Contains("panel") And subctrl.Name.ToLower.StartsWith("bag") Then
                                        Dim bagPanel As Panel = subctrl
                                        bagPanel.BackColor = Getraritycolor(GetItemQualityByItemId(potCharBag.Id))
                                        For Each potBagItem As Item In GlobalVariables.currentViewedCharSet.InventoryItems
                                            If potBagItem.Bagguid = potCharBag.Guid Then
                                                If potBagItem.Name Is Nothing Then potBagItem.Name = GetItemNameByItemId(potBagItem.Id, NCFramework.My.MySettings.Default.language)
                                                If potBagItem.Image Is Nothing Then potBagItem.Image = GetItemIconById(potBagItem.Id, GlobalVariables.GlobalWebClient)
                                                potCharBag.BagItems.Add(potBagItem)
                                            End If
                                        Next
                                        potCharBag.SlotCount = GetItemSlotCountByItemId(potCharBag.Id)
                                        bagPanel.Tag = potCharBag
                                        For Each myPic As PictureBox In subctrl.Controls
                                            myPic.BackgroundImage = GetItemIconById(potCharBag.Id, GlobalVariables.GlobalWebClient)
                                            myPic.Tag = potCharBag
                                        Next
                                    End If
                                End If
                            Next
                        Case 39 To 66
                            If potCharBag.Name Is Nothing Then potCharBag.Name = GetItemNameByItemId(potCharBag.Id, NCFramework.My.MySettings.Default.language)
                            If potCharBag.Image Is Nothing Then potCharBag.Image = GetItemIconById(potCharBag.Id, GlobalVariables.GlobalWebClient)
                            If potCharBag.Rarity = Nothing Then potCharBag.Rarity = GetItemQualityByItemId(potCharBag.Id)
                            Dim entry As Control = BankLayoutPanel.Controls.Find("bankitm_slot_" & potCharBag.Slot.ToString & "_panel", False)(0)
                            entry.BackColor = Getraritycolor(potCharBag.Rarity)
                            entry.Tag = potCharBag
                            InfoToolTip.SetToolTip(entry, potCharBag.Name)
                            Dim pic As Control = entry.Controls(0)
                            pic.BackgroundImage = potCharBag.Image
                            pic.Tag = potCharBag
                            pic.Cursor = Cursors.Hand
                            InfoToolTip.SetToolTip(pic, potCharBag.Name)
                    End Select
                Next
            End If
            ThreadExtensions.ScSend(_context, New Action(Of CompletedEventArgs)(AddressOf OnCompleted),
                                    New CompletedEventArgs())
        End Sub

        Private Function Getraritycolor(ByVal rarity As Integer) As Color
            Select Case rarity
                Case 0, 1 : Return Color.Gray
                Case 0, 1 : Return Color.White
                Case 2 : Return Color.LightGreen
                Case 3 : Return Color.DodgerBlue
                Case 4 : Return Color.DarkViolet
                Case 5 : Return Color.Orange
                Case 6 : Return Color.Gold
                Case Else : Return Color.LawnGreen
            End Select
        End Function

        Private Sub DelegateLayoutControlAdding(layoutPanel As FlowLayoutPanel, ctrl As Control)
            ctrl.SetDoubleBuffered()
            layoutPanel.Controls.Add(ctrl)
            layoutPanel.Update()
            layoutPanel.Controls.SetChildIndex(layoutPanel.Controls(layoutPanel.Controls.Count - 1),
                                                             1)
        End Sub
        Private Sub PrepareCompleted() Handles Me.Completed
            CloseProcessStatus()
            Userwait.Close()
            Show()
            Visible = True
        End Sub

        Private Sub OnItem_MouseOver(sender As Object, e As EventArgs)

        End Sub

        Private Sub OnBag_MouseOver(sender As Object, e As EventArgs) Handles Bag7Pic.MouseEnter, bag6Pic.MouseEnter, bag5Pic.MouseEnter, bag4Pic.MouseEnter, bag3Pic.MouseEnter, bag2Pic.MouseEnter, bag1Pic.MouseEnter
            If Not sender.BackgroundImage Is Nothing Then
                _tmpImage = sender.BackgroundImage
                Application.DoEvents()
                Dim picbx As PictureBox = sender
                Dim g As Graphics
                Dim img As Image
                Dim r As Rectangle
                img = picbx.BackgroundImage
                sender.BackgroundImage = New Bitmap(picbx.Width, picbx.Height, PixelFormat.Format32bppArgb)
                g = Graphics.FromImage(picbx.BackgroundImage)
                r = New Rectangle(0, 0, picbx.Width, picbx.Height)
                g.DrawImage(img, r)
                SetBrightness(0.2, g, img, r, picbx)
            End If
        End Sub

        Private Sub OnBag_MouseLeave(sender As Object, e As EventArgs) Handles Bag7Pic.MouseLeave, bag6Pic.MouseLeave, bag5Pic.MouseLeave, bag4Pic.MouseLeave, bag3Pic.MouseLeave, bag2Pic.MouseLeave, bag1Pic.MouseLeave
            If Not _tmpImage Is Nothing And Not sender.BackgroundImage Is Nothing Then
                Dim picbox As PictureBox = sender
                picbox.BackgroundImage = _tmpImage
                picbox.Refresh()
                Application.DoEvents()
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

        Private Sub BagOpen(sender As Object, e As EventArgs) Handles Bag7Pic.Click, bag6Pic.Click, bag5Pic.Click, bag4Pic.Click, bag3Pic.Click, bag2Pic.Click, bag1Pic.Click
            Dim bag As Item = sender.tag
            If bag Is Nothing Then Exit Sub
            BagItemPanel.Controls.Clear()
            For z = 0 To bag.SlotCount - 1
                Dim itm As New Item
                itm.Slot = z
                Dim newItmPanel As New Panel
                newItmPanel.Size = referenceItmPanel.Size
                newItmPanel.Margin = referenceItmPanel.Margin
                newItmPanel.Name = "slot_" & z.ToString() & "_panel"
                Dim subItmPic As New PictureBox
                subItmPic.Size = referenceItmPic.Size
                newItmPanel.Controls.Add(subItmPic)
                subItmPic.Location = referenceItmPic.Location
                subItmPic.BackgroundImageLayout = ImageLayout.Stretch
                subItmPic.BackgroundImage = referenceItmPic.BackgroundImage
                newItmPanel.BackColor = referenceItmPanel.BackColor
                newItmPanel.Tag = itm
                subItmPic.Tag = itm
                newItmPanel.SetDoubleBuffered()
                BagItemPanel.Controls.Add(newItmPanel)
                InfoToolTip.SetToolTip(newItmPanel, "Empty")
                InfoToolTip.SetToolTip(subItmPic, "Empty")
                BagItemPanel.Update()
                Application.DoEvents()
            Next z
            For Each itm As Item In bag.BagItems
                SetInventorySlot(itm, itm.Slot)
            Next
        End Sub
        Private Sub SetInventorySlot(ByVal itm As Item, ByVal slot As Integer)
            For Each itmctrl As Panel In BagItemPanel.Controls
                If itmctrl.Name.Contains("_" & slot.ToString() & "_") Then
                    itmctrl.BackColor = Getraritycolor(GetItemQualityByItemId(itm.Id))
                    itmctrl.Tag = itm
                    InfoToolTip.SetToolTip(itmctrl, itm.Name)
                    For Each itmPicBox As PictureBox In itmctrl.Controls
                        itmPicBox.BackgroundImage = itm.Image
                        itmPicBox.Tag = itm
                        InfoToolTip.SetToolTip(itmPicBox, itm.Name)
                    Next
                    BagItemPanel.Update()
                    Application.DoEvents()
                End If
            Next
        End Sub
        Private Sub BankInterface_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            Hide()
            Application.DoEvents()
            AddHandler highlighter2.Click, AddressOf highlighter2_Click
        End Sub

        Private Sub highlighter2_Click(sender As Object, e As EventArgs)
            Close()
        End Sub
    End Class
End Namespace