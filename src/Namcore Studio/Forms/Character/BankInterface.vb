﻿'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013-2016 NamCore Studio <https://github.com/megasus/Namcore-Studio>
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
Imports System.Linq
Imports System.Threading
Imports libnc.Provider
Imports NamCore_Studio.Forms.Extension
Imports NamCore_Studio.Modules.Interface
Imports NCFramework.Framework.Extension
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Modules
Imports NCFramework.My
Imports NCFramework.My.Resources

Namespace Forms.Character
    Public Class BankInterface
        Inherits EventTrigger
        '// Declaration
        Private ReadOnly _context As SynchronizationContext = SynchronizationContext.Current
        Public Event Completed As EventHandler(Of CompletedEventArgs)

        Delegate Sub AddLayoutControlDelegate(layout As FlowLayoutPanel, ctrl As Control)

        Delegate Sub AddControlDelegate(parentControl As Control, ctrl As Control)

        Delegate Sub UpdateControlDelegate(bagPanel As Control, bagsInitialized As Boolean, potCharBag As Item)

        Dim _tmpImage As Image
        Dim _lastRemovePic As PictureBox
        Dim _currentBag As Item
        Dim _visibleActionControls As List(Of Control)
        Shared _actionBlocked As Boolean = False
        '// Declaration

        Protected Overridable Sub OnCompleted(e As CompletedEventArgs)
            RaiseEvent Completed(Me, e)
        End Sub

        Public Sub PrepareBankInterface(setId As Integer)
            '// Setting up interface
            _visibleActionControls = New List(Of Control)()
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
            Dim trd = New Thread(DirectCast(Sub() DoWork(), ThreadStart))
            If GlobalVariables.currentEditedCharSet Is Nothing Then _
                GlobalVariables.currentEditedCharSet = DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet)
            trd.Start()
        End Sub

        Private Sub DoWork()
            LogAppend("DoWork call", "BankInterface_DoWork")
            For i = 39 To 66
                '// Adding slots for main bank items
                LogAppend("Setting up main bank-itemslot " & i.ToString(), "BankInterface_DoWork")
                Dim itm As New Item
                itm.Slot = i
                itm.Bagguid = 0
                itm.Bag = 0
                Dim newItemPanel As New ItemPanel
                newItemPanel.Name = "bankitm_slot_" & i.ToString & "_panel"
                newItemPanel.Size = reference_itm_panel.Size
                newItemPanel.Padding = reference_itm_panel.Padding
                newItemPanel.Margin = reference_itm_panel.Margin
                newItemPanel.BackColor = reference_itm_panel.BackColor
                newItemPanel.Tag = itm
                Dim newItemPic As New PictureBox
                newItemPic.Name = "bankitm_slot_" & i.ToString & "_pic"
                newItemPic.Size = reference_itm_pic.Size
                newItemPic.BackgroundImage = reference_itm_pic.BackgroundImage
                newItemPic.BackgroundImageLayout = reference_itm_pic.BackgroundImageLayout
                newItemPanel.Controls.Add(newItemPic)
                newItemPic.Location = reference_itm_pic.Location
                newItemPic.Tag = itm
                Dim subItmRemovePic As New PictureBox
                subItmRemovePic.Name = "bankitm_slot_" & i.ToString() & "_remove"
                subItmRemovePic.Cursor = Cursors.Hand
                subItmRemovePic.Size = removeinventboxbig.Size
                newItemPanel.Controls.Add(subItmRemovePic)
                subItmRemovePic.Location = removeinventboxbig.Location
                subItmRemovePic.BackgroundImageLayout = ImageLayout.Stretch
                subItmRemovePic.BackgroundImage = My.Resources.add_
                subItmRemovePic.BackColor = removeinventboxbig.BackColor
                subItmRemovePic.Tag = New Object() {newItemPanel, newItemPic}
                subItmRemovePic.Visible = False
                subItmRemovePic.SetDoubleBuffered()
                subItmRemovePic.BringToFront()
                Dim subCountLabel As New Label
                subCountLabel.Text = ""
                subCountLabel.Name = "bankitm_slot_" & i.ToString() & "_count"
                subCountLabel.Cursor = Cursors.IBeam
                subCountLabel.Size = referenceCountBig.Size
                subCountLabel.Font = referenceCountBig.Font
                subCountLabel.BackColor = referenceCountBig.BackColor
                subCountLabel.Tag = itm
                newItemPanel.Controls.Add(subCountLabel)
                subCountLabel.Location = referenceCountBig.Location
                subCountLabel.Visible = False
                subCountLabel.SetDoubleBuffered()
                subCountLabel.BringToFront()
                InfoToolTip.SetToolTip(subItmRemovePic, TOOLTIP_ADD)
                Application.DoEvents()
                AddHandler subCountLabel.Click, AddressOf ChangeCount
                AddHandler newItemPanel.MouseEnter, AddressOf InventItem_MouseEnter
                AddHandler newItemPanel.MouseLeave, AddressOf InventItem_MouseLeave
                AddHandler subItmRemovePic.MouseClick, AddressOf removeinventbox_Click
                AddHandler subItmRemovePic.MouseEnter, AddressOf removeinventbox_MouseEnter
                AddHandler subItmRemovePic.MouseLeave, AddressOf removeinventbox_MouseLeave
                BankLayoutPanel.BeginInvoke(New AddLayoutControlDelegate(AddressOf DelegateLayoutControlAdding),
                                            BankLayoutPanel, newItemPanel)
            Next i
            For Each subctrl As Control In BackPanel.Controls
                If _
                    subctrl.Name.ToLower.Contains("panel") And subctrl.Name.ToLower.StartsWith("bag") And
                    Not subctrl.Name.ToLower.Contains("item") Then
                    Dim bagPanel = CType(subctrl, ItemPanel)
                    _actionBlocked = True
                    bagPanel.BeginInvoke(New UpdateControlDelegate(AddressOf DelegateControllUpdate),
                                         bagPanel, True, Nothing)
                    While _actionBlocked : End While
                End If
            Next
            If Not GlobalVariables.currentEditedCharSet.InventoryZeroItems Is Nothing Then
                For Each potCharBag As Item In GlobalVariables.currentEditedCharSet.InventoryZeroItems
                    '// Look for bank items & bags
                    potCharBag.BagItems = New List(Of Item)()
                    Select Case potCharBag.Slot
                        Case 67 To 73
                            '// Found a bag -> set up matching slot
                            LogAppend(
                                "Initializing bag (" & potCharBag.Id.ToString() & ") for slot " &
                                potCharBag.Slot.ToString(), "BankInterface_DoWork", True)
                            For Each subctrl As Control In BackPanel.Controls
                                If _
                                    subctrl.Name.ToLower.Contains("panel") And subctrl.Name.ToLower.StartsWith("bag") And
                                    Not subctrl.Name.ToLower.Contains("item") Then
                                    Dim bagPanel = CType(subctrl, ItemPanel)
                                    _actionBlocked = True
                                    bagPanel.BeginInvoke(New UpdateControlDelegate(AddressOf DelegateControllUpdate),
                                                         bagPanel, False, potCharBag)
                                    While _actionBlocked : End While
                                End If
                            Next
                        Case 39 To 66
                            '// Found a main bank item -> set up matching slot
                            LogAppend(
                                "Initializing main item (" & potCharBag.Id.ToString() & ") for slot " &
                                potCharBag.Slot.ToString(), "BankInterface_DoWork", True)
                            If potCharBag.Name Is Nothing Then _
                                potCharBag.Name = GetItemNameByItemId(potCharBag.Id, MySettings.Default.language)
                            If potCharBag.Image Is Nothing Then _
                                potCharBag.Image = GetItemIconByItemId(potCharBag.Id, GlobalVariables.GlobalWebClient)
                            If potCharBag.Rarity = Nothing Then _
                                potCharBag.Rarity = CType(GetItemQualityByItemId(potCharBag.Id), Item.RarityType)
                            Dim aentry As Control() =
                                    BankLayoutPanel.Controls.Find("bankitm_slot_" & potCharBag.Slot.ToString & "_panel",
                                                                  True)
                            Dim entry As Control = aentry(0)
                            Dim entryRemove As Control =
                                    entry.Controls.Find("bankitm_slot_" & potCharBag.Slot.ToString & "_remove", True)(0)
                            entry.BackColor = Getraritycolor(potCharBag.Rarity)
                            entry.Tag = potCharBag
                            entryRemove.BackColor = entry.BackColor
                            entryRemove.BackgroundImage = My.Resources.trash__delete__16x16
                            InfoToolTip.SetToolTip(entry, potCharBag.Name)
                            InfoToolTip.SetToolTip(entryRemove, TOOLTIP_REMOVE)
                            Dim pic As Control =
                                    entry.Controls.Find("bankitm_slot_" & potCharBag.Slot.ToString & "_pic", True)(0)
                            pic.BackgroundImage = potCharBag.Image
                            pic.Tag = potCharBag
                            pic.Cursor = Cursors.Hand
                            If _
                                potCharBag.Count > 1 OrElse
                                potCharBag.Count = 1 AndAlso GetItemMaxStackByItemId(potCharBag.Id) > 1 Then
                                '// Set up item stack label
                                Dim countLabel =
                                        CType(
                                            entry.Controls.Find("bankitm_slot_" & potCharBag.Slot.ToString() & "_count",
                                                                True)(0),
                                            Label)
                                countLabel.Text = potCharBag.Count.ToString()
                                countLabel.Tag = potCharBag
                                countLabel.Visible = True
                            End If
                            InfoToolTip.SetToolTip(pic, potCharBag.Name)
                    End Select
                Next
            End If
            ThreadExtensions.ScSend(_context, New Action(Of CompletedEventArgs)(AddressOf OnCompleted),
                                    New CompletedEventArgs())
        End Sub

        Private Sub ChangeCount(sender As Object, e As EventArgs)
            '// Changing item stack size
            Dim locLabel = CType(sender, Label)
            If locLabel.Visible = True Then
                Dim result As String = InputBox(MSG_ENTERITEMCOUNT,
                                                MSG_ITEMCOUNTCHANGE, locLabel.Text)
                If Not result = "" Then
                    LogAppend("User entered stack size: " & result, "BankInterface_ChangeCount")
                    Dim intResult As Integer = TryInt(result)
                    If intResult <> 0 Then
                        Dim itm = CType(locLabel.Tag, Item)
                        Dim maxStackSize As Integer = GetItemMaxStackByItemId(itm.Id)
                        '// Check if stack size within limit
                        If intResult > maxStackSize Then
                            '// Invalid stack size
                            LogAppend("Stack size above limit. Limit is: " & maxStackSize.ToString(),
                                      "BankInterface_ChangeCount")
                            MsgBox(MSG_STACKLIMITREACHED & " " & maxStackSize.ToString(),
                                   MsgBoxStyle.Critical, MSG_INVALIDENTRY)
                            Exit Sub
                        End If
                        '// Stack size valid -> applying to item
                        itm.Count = intResult
                        locLabel.Text = CType(itm.Count, String)
                        If GlobalVariables.currentEditedCharSet Is Nothing Then _
                            GlobalVariables.currentEditedCharSet =
                                DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet)
                        If itm.Bagguid = 0 Then
                            '// Apply changes to character
                            Dim oldItmIndex As Integer =
                                    GlobalVariables.currentEditedCharSet.InventoryZeroItems.FindIndex(
                                        Function(item) item.Slot = itm.Slot)
                            GlobalVariables.currentEditedCharSet.InventoryZeroItems(oldItmIndex) = itm
                        Else
                            If _currentBag.AddedBag = False Then
                                Dim oldItmIndex As Integer =
                                        GlobalVariables.currentEditedCharSet.InventoryItems.FindIndex(
                                            Function(item) item.Slot = itm.Slot AndAlso item.Bagguid = itm.Bagguid)
                                GlobalVariables.currentEditedCharSet.InventoryItems(oldItmIndex) = itm
                            End If
                            Dim inBagIndex As Integer =
                                    _currentBag.BagItems.FindIndex(
                                        Function(item) item.Slot = itm.Slot AndAlso item.Bagguid = itm.Bagguid)
                            _currentBag.BagItems(inBagIndex) = itm
                        End If
                    End If
                End If
            End If
        End Sub

        Private Sub DelegateControllUpdate(bagPanel As Control, initBags As Boolean, potCharBag As Item)
            '// Set up or initialize bag
            Dim realBagSlot As Integer = TryInt(SplitString(bagPanel.Name, "bag", "Panel"))
            LogAppend(
                "Updating bag panel with slot: " & realBagSlot.ToString() & " for item slot: " &
                potCharBag.Slot.ToString() & " - Initializing bags: " & initBags.ToString(),
                "BankInterface_DelegateControllUpdate")
            If initBags Then
                '// Only initializing bag slot (not set up)
                Dim subItmRemovePic As New PictureBox
                Dim tempItm As New Item
                tempItm.Slot = realBagSlot + 66
                bagPanel.Tag = tempItm
                subItmRemovePic.Name = "bag_" & (realBagSlot + 66).ToString() & "_remove"
                subItmRemovePic.Cursor = Cursors.Hand
                subItmRemovePic.Size = removeinventbox.Size
                bagPanel.Controls.Add(subItmRemovePic)
                subItmRemovePic.Location = removeinventbox.Location
                subItmRemovePic.BackgroundImageLayout = ImageLayout.Stretch
                subItmRemovePic.BackgroundImage = My.Resources.add_
                subItmRemovePic.BackColor = removeinventbox.BackColor
                subItmRemovePic.Tag =
                    {bagPanel,
                     bagPanel.Controls.Find("bag" & (realBagSlot).ToString() & "Pic", True)(
                         0)}
                subItmRemovePic.Visible = False
                subItmRemovePic.SetDoubleBuffered()
                subItmRemovePic.BringToFront()
                Application.DoEvents()
                InfoToolTip.SetToolTip(subItmRemovePic, TOOLTIP_ADD)
                AddHandler subItmRemovePic.MouseClick, AddressOf removeinventboxBag_Click
                AddHandler subItmRemovePic.MouseEnter, AddressOf removeinventbox_MouseEnter
                AddHandler subItmRemovePic.MouseLeave, AddressOf removeinventbox_MouseLeave
                AddHandler bagPanel.MouseEnter, AddressOf BagItem_MouseEnter
                AddHandler bagPanel.MouseLeave, AddressOf BagItem_MouseLeave
            Else
                '// Filling bag slot
                If bagPanel.Name.Contains((potCharBag.Slot - 66).ToString()) Then
                    Dim subItmRemovePic =
                            CType(bagPanel.Controls.Find("bag_" & (realBagSlot + 66).ToString() & "_remove", True)(0),
                                  PictureBox)
                    subItmRemovePic.BackgroundImage = My.Resources.trash__delete__16x16
                    InfoToolTip.SetToolTip(subItmRemovePic, TOOLTIP_REMOVE)
                    bagPanel.BackColor = Getraritycolor(GetItemQualityByItemId(potCharBag.Id))
                    For Each potBagItem As Item In _
                        GlobalVariables.currentEditedCharSet.InventoryItems
                        If potBagItem.Bagguid = potCharBag.Guid Then
                            If potBagItem.Name Is Nothing Then _
                                potBagItem.Name = GetItemNameByItemId(potBagItem.Id,
                                                                      MySettings.Default.language)
                            If potBagItem.Image Is Nothing Then _
                                potBagItem.Image = GetItemIconByItemId(potBagItem.Id, GlobalVariables.GlobalWebClient)
                            '// Adding items to bag pointer
                            potCharBag.BagItems.Add(potBagItem)
                        End If
                    Next
                    potCharBag.SlotCount = GetItemSlotCountByItemId(potCharBag.Id)
                    bagPanel.Tag = potCharBag
                    For Each myPic As PictureBox In bagPanel.Controls
                        If myPic.Name Is Nothing Then Continue For
                        If myPic.Name.EndsWith("_remove") Then Continue For
                        myPic.BackgroundImage = GetItemIconByItemId(potCharBag.Id, GlobalVariables.GlobalWebClient)
                        myPic.Tag = potCharBag
                    Next
                End If
            End If
            _actionBlocked = False
        End Sub

        Private Function GetRarityColor(rarity As Integer) As Color
            '// Get rarity color
            LogAppend("Loading item rarity color for value: " & rarity.ToString(), "BankInterface_GetRarityColor")
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
            Try
                LogAppend("Adding control '" & ctrl.Name & "' to layout '" & layoutPanel.Name & "'",
                          "BankInterface_DelegateLayoutControlAdding")
                ctrl.SetDoubleBuffered()
                layoutPanel.Controls.Add(ctrl)
                layoutPanel.Update()
                layoutPanel.Controls.SetChildIndex(layoutPanel.Controls(layoutPanel.Controls.Count - 1),
                                                   1)
            Catch ex As Exception
                LogAppend("Something went wrong while adding control: " & ex.ToString(),
                          "BankInterface_DelegateLayoutControlAdding", False, True)
            End Try
        End Sub

        Private Sub PrepareCompleted() Handles Me.Completed
            '// Interface has been initialized
            LogAppend("Interface has been prepared", "BankInterface_PrepareCompleted")
            CloseProcessStatus()
            Userwait.Close()
            Show()
            Visible = True
        End Sub

        Private Sub BagItem_MouseEnter(sender As Object, e As EventArgs)
            '// On bag item hover
            For i = _visibleActionControls.Count - 1 To 0 Step - 1
                '// Hide controls of previous hover
                _visibleActionControls(i).Visible = False
                _visibleActionControls.Remove(_visibleActionControls(i))
            Next
            Dim parentPanel = CType(sender, ItemPanel)
            Dim itm = CType(parentPanel.Tag, Item)
            Dim removePic =
                    CType(parentPanel.Controls.Find("bag_" & itm.Slot.ToString() & "_remove", True)(0), PictureBox)
            If Not removePic Is Nothing Then
                '// Add control pointer to list to allow referencing (hide) on new hover
                _visibleActionControls.Add(removePic)
                _lastRemovePic = removePic
                removePic.Visible = True
            End If
        End Sub

        Private Sub BagItem_MouseLeave(sender As Object, e As EventArgs)
            If _lastRemovePic IsNot Nothing Then
                _visibleActionControls.Remove(_lastRemovePic)
                _lastRemovePic.Visible = False
                Application.DoEvents()
            End If
        End Sub

        Private Sub OnBag_MouseOver(sender As Object, e As EventArgs) _
            Handles Bag7Pic.MouseEnter, bag6Pic.MouseEnter, bag5Pic.MouseEnter, bag4Pic.MouseEnter, bag3Pic.MouseEnter,
                    bag2Pic.MouseEnter, bag1Pic.MouseEnter
            '// On bag mouse hover
            If Not TryCast(sender, PictureBox).BackgroundImage Is Nothing Then
                '// Highlighting image
                _tmpImage = TryCast(sender, PictureBox).BackgroundImage
                Application.DoEvents()
                Dim picbx = CType(sender, PictureBox)
                Dim g As Graphics
                Dim img As Image
                Dim r As Rectangle
                img = picbx.BackgroundImage
                TryCast(sender, PictureBox).BackgroundImage = New Bitmap(picbx.Width, picbx.Height,
                                                                         PixelFormat.Format32bppArgb)
                g = Graphics.FromImage(picbx.BackgroundImage)
                r = New Rectangle(0, 0, picbx.Width, picbx.Height)
                g.DrawImage(img, r)
                SetBrightness(0.2, g, img, r, picbx)
            End If
        End Sub

        Private Sub OnBag_MouseLeave(sender As Object, e As EventArgs) _
            Handles Bag7Pic.MouseLeave, bag6Pic.MouseLeave, bag5Pic.MouseLeave, bag4Pic.MouseLeave, bag3Pic.MouseLeave,
                    bag2Pic.MouseLeave, bag1Pic.MouseLeave
            If Not _tmpImage Is Nothing And Not TryCast(sender, PictureBox).BackgroundImage Is Nothing Then
                '// Remove highlight
                Dim picbox = TryCast(sender, PictureBox)
                picbox.BackgroundImage = _tmpImage
                picbox.Refresh()
                Application.DoEvents()
            End If
        End Sub

        Private Sub SetBrightness(brightness As Single, g As Graphics, img As Image,
                                  r As Rectangle,
                                  ByRef picbox As PictureBox)
            '// Set brightness of image
            '// Brightness should be -1 (black) to 0 (neutral) to 1 (white)
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

        Private Sub BagOpen(sender As Object, e As EventArgs) _
            Handles Bag7Pic.Click, bag6Pic.Click, bag5Pic.Click, bag4Pic.Click, bag3Pic.Click, bag2Pic.Click,
                    bag1Pic.Click
            '// Opening bag

            Dim bag = CType(TryCast(sender, PictureBox).Tag, Item)
            If bag Is Nothing Then Exit Sub
            If bag.Id = 0 AndAlso bag.Slot > 0 Then Exit Sub
            _currentBag = bag
            BagItemPanel.Controls.Clear()
            LogAppend("Opening bag " & bag.Id.ToString(), "BankInterface_BagOpen")
            For z = 0 To bag.SlotCount - 1
                '// Initialiting item slots for bag
                LogAppend("Initializing item slot " & z.ToString(), "BankInterface_BagOpen")
                Dim itm As New Item
                itm.Slot = z
                itm.Bagguid = bag.Guid
                itm.Bag = bag.Id
                Dim newItmPanel As New ItemPanel
                newItmPanel.Size = referenceItmPanel.Size
                newItmPanel.Margin = referenceItmPanel.Margin
                newItmPanel.Name = "bankitm_slot_" & z.ToString() & "_panel"
                Dim subItmPic As New PictureBox
                subItmPic.Size = referenceItmPic.Size
                subItmPic.Name = "bankitm_slot_" & z.ToString() & "_pic"
                newItmPanel.Controls.Add(subItmPic)
                subItmPic.Location = referenceItmPic.Location
                subItmPic.BackgroundImageLayout = ImageLayout.Stretch
                subItmPic.BackgroundImage = referenceItmPic.BackgroundImage
                newItmPanel.BackColor = referenceItmPanel.BackColor
                newItmPanel.Tag = itm
                subItmPic.Tag = itm
                newItmPanel.SetDoubleBuffered()
                BagItemPanel.Controls.Add(newItmPanel)
                Dim subItmRemovePic As New PictureBox
                subItmRemovePic.Name = "bankitm_slot_" & z.ToString() & "_remove"
                subItmRemovePic.Cursor = Cursors.Hand
                subItmRemovePic.Size = removeinventbox.Size
                newItmPanel.Controls.Add(subItmRemovePic)
                subItmRemovePic.Location = removeinventbox.Location
                subItmRemovePic.BackgroundImageLayout = ImageLayout.Stretch
                subItmRemovePic.BackgroundImage = My.Resources.add_
                subItmRemovePic.BackColor = removeinventbox.BackColor
                subItmRemovePic.Tag = New Object() {newItmPanel, subItmPic}
                subItmRemovePic.Visible = False
                subItmRemovePic.SetDoubleBuffered()
                subItmRemovePic.BringToFront()
                Dim subCountLabel As New Label
                subCountLabel.Text = ""
                subCountLabel.Name = "bankitm_slot_" & z.ToString() & "_count"
                subCountLabel.Cursor = Cursors.IBeam
                subCountLabel.Size = referenceCount.Size
                subCountLabel.Font = referenceCount.Font
                subCountLabel.BackColor = referenceCount.BackColor
                subCountLabel.Tag = itm
                newItmPanel.Controls.Add(subCountLabel)
                subCountLabel.Location = referenceCount.Location
                subCountLabel.Visible = False
                subCountLabel.SetDoubleBuffered()
                subCountLabel.BringToFront()
                InfoToolTip.SetToolTip(newItmPanel, TOOLTIP_EMPTY)
                InfoToolTip.SetToolTip(subItmPic, TOOLTIP_EMPTY)
                InfoToolTip.SetToolTip(subItmRemovePic, TOOLTIP_ADD)
                BagItemPanel.Update()
                Application.DoEvents()
                newItmPanel.SetDoubleBuffered()
                AddHandler subCountLabel.Click, AddressOf ChangeCount
                AddHandler newItmPanel.MouseEnter, AddressOf InventItem_MouseEnter
                AddHandler newItmPanel.MouseLeave, AddressOf InventItem_MouseLeave
                AddHandler subItmRemovePic.MouseClick, AddressOf removeinventbox_Click
                AddHandler subItmRemovePic.MouseEnter, AddressOf removeinventbox_MouseEnter
                AddHandler subItmRemovePic.MouseLeave, AddressOf removeinventbox_MouseLeave
            Next z
            For Each itm As Item In bag.BagItems
                '// Fill slots with items
                SetInventorySlot(itm, itm.Slot)
            Next
        End Sub

        Private Sub InventItem_MouseEnter(sender As Object, e As EventArgs)
            '// Inventory item mouse hover
            For i = _visibleActionControls.Count - 1 To 0 Step - 1
                '// Hide controls of previous hover
                _visibleActionControls(i).Visible = False
                _visibleActionControls.Remove(_visibleActionControls(i))
            Next
            Dim parentPanel = CType(sender, ItemPanel)
            Dim itm = CType(parentPanel.Tag, Item)
            Dim removePic =
                    CType(parentPanel.Controls.Find("bankitm_slot_" & itm.Slot.ToString & "_remove", True)(0),
                          PictureBox)
            If Not removePic Is Nothing Then
                '// Add control pointer to list to allow referencing (hide) on new hover
                _visibleActionControls.Add(removePic)
                _lastRemovePic = removePic
                removePic.Visible = True
            End If
        End Sub

        Private Sub InventItem_MouseLeave(sender As Object, e As EventArgs)
            If _lastRemovePic IsNot Nothing Then
                _visibleActionControls.Remove(_lastRemovePic)
                _lastRemovePic.Visible = False
                Application.DoEvents()
            End If
        End Sub

        Private Sub removeinventbox_Click(sender As Object, e As EventArgs)
            '// Delete/Add inventory item
            Dim locPanel = CType(CType(TryCast(sender, PictureBox).Tag, Object())(0), ItemPanel)
            Dim locPic = CType(CType(TryCast(sender, PictureBox).Tag, Object())(1), PictureBox)
            Dim oldItm = CType(locPic.Tag, Item)
            If oldItm.Id = 0 Then
                '// Slot empty -> 'Add'-Mode
                Dim result As String = InputBox(MSG_ENTERITEMID, MSG_ADDITEM, "0")
                If result.Length = 0 Then
                    CType(sender, PictureBox).Visible = False
                Else
                    LogAppend("Adding new inventory item for slot " & oldItm.Slot.ToString() & " with id " & result,
                              "BankInterface_removeinventbox_Click")
                    Dim intResult As Integer = TryInt(result)
                    If intResult <> 0 Then
                        Dim checkName As String = GetItemNameByItemId(intResult, MySettings.Default.language)
                        If Not checkName = "Not found" Then
                            '// Valid item id entered
                            LogAppend("Item id is valid, loaded name: " & checkName,
                                      "BankInterface_removeinventbox_Click")
                            If GlobalVariables.currentEditedCharSet Is Nothing Then _
                                GlobalVariables.currentEditedCharSet =
                                    DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet)
                            Dim replaceItm As New Item()
                            '// Load item properties
                            replaceItm.Slot = oldItm.Slot
                            replaceItm.Id = intResult
                            replaceItm.Image = GetItemIconByItemId(replaceItm.Id, GlobalVariables.GlobalWebClient)
                            replaceItm.Name = checkName
                            replaceItm.Rarity = CType(GetItemQualityByItemId(replaceItm.Id), Item.RarityType)
                            replaceItm.Bag = oldItm.Bag
                            replaceItm.Bagguid = oldItm.Bagguid
                            '// Generate an available GUID
                            Dim newGuid = 1
                            Do
                                If GlobalVariables.nonUsableGuidList.Contains(newGuid) Then
                                    '// GUID already in use
                                    newGuid += 1
                                Else
                                    '// Found available GUID -> Mark as unavailable
                                    GlobalVariables.nonUsableGuidList.Add(newGuid)
                                    Exit Do
                                End If
                            Loop
                            replaceItm.Guid = newGuid
                            locPanel.BackColor = GetItemQualityColor(replaceItm.Rarity)
                            If replaceItm.Bagguid = 0 Then
                                '// Item is a bag or main bank item
                                LogAppend("Item is bag or main bank item", "BankInterface_removeinventbox_Click")
                                GlobalVariables.currentEditedCharSet.InventoryZeroItems.Add(replaceItm)
                            Else
                                '// Item is a bag item
                                LogAppend("Item is bag item with bagguid: " & replaceItm.Bagguid.ToString(),
                                          "BankInterface_removeinventbox_Click")
                                If _currentBag.AddedBag = False Then
                                    '// Bag is original / not user-created
                                    GlobalVariables.currentEditedCharSet.InventoryItems.Add(replaceItm)
                                End If
                                _currentBag.BagItems.Add(replaceItm)
                            End If
                            If GetItemMaxStackByItemId(replaceItm.Id) > 1 Then
                                '// Set up item stack size label
                                Dim countLabel =
                                        CType(
                                            locPanel.Controls.Find(
                                                "bankitm_slot_" & replaceItm.Slot.ToString() & "_count",
                                                True)(0),
                                            Label)
                                If Not countLabel Is Nothing Then
                                    countLabel.Text = "1"
                                    countLabel.Visible = True
                                    countLabel.Tag = replaceItm
                                End If
                            End If
                            locPic.Tag = replaceItm
                            locPanel.Tag = replaceItm
                            locPic.BackgroundImage = replaceItm.Image
                            InfoToolTip.SetToolTip(locPic, checkName)
                            InfoToolTip.SetToolTip(TryCast(sender, PictureBox), TOOLTIP_REMOVE)
                            TryCast(sender, PictureBox).BackgroundImage = My.Resources.trash__delete__16x16
                        Else
                            LogAppend("Item id is invalid", "BankInterface_removeinventbox_Click")
                            MsgBox(MSG_INVALIDITEMID, MsgBoxStyle.Critical, MSG_ERROR)
                        End If
                    Else
                        LogAppend("Item id is invalid", "BankInterface_removeinventbox_Click")
                        MsgBox(MSG_INVALIDITEMID, MsgBoxStyle.Critical, MSG_ERROR)
                    End If
                    TryCast(sender, PictureBox).Visible = False
                End If
            Else
                '// Slot not empty -> 'Remove'-Mode
                Dim result = MsgBox(MSG_DELETEITEM, vbYesNo, MSG_AREYOUSURE)
                If result = MsgBoxResult.Yes Then
                    If GlobalVariables.currentEditedCharSet Is Nothing Then _
                        GlobalVariables.currentEditedCharSet =
                            DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet)
                    locPanel.BackColor = referenceItmPanel.BackColor
                    locPic.BackgroundImage = referenceItmPic.BackgroundImage
                    Dim replaceItm As New Item()
                    replaceItm.Slot = oldItm.Slot
                    locPanel.Tag = replaceItm
                    locPic.Tag = replaceItm
                    InfoToolTip.SetToolTip(locPic, TOOLTIP_EMPTY)
                    InfoToolTip.SetToolTip(TryCast(sender, PictureBox), TOOLTIP_ADD)
                    If oldItm.Bagguid = 0 Then
                        GlobalVariables.currentEditedCharSet.InventoryZeroItems.RemoveAt(
                            GlobalVariables.currentEditedCharSet.InventoryZeroItems.FindIndex(
                                Function(item) item.Slot = oldItm.Slot))
                    Else
                        _currentBag.BagItems.Remove(_currentBag.BagItems.Find(Function(item) item.Slot = replaceItm.Slot))
                        If _currentBag.AddedBag = False Then
                            GlobalVariables.currentEditedCharSet.InventoryItems.RemoveAt(
                                GlobalVariables.currentEditedCharSet.InventoryItems.FindIndex(
                                    Function(item) item.Slot = oldItm.Slot AndAlso item.Bagguid = oldItm.Bagguid))
                        End If
                    End If
                    Dim countLabel =
                            CType(
                                locPanel.Controls.Find("bankitm_slot_" & replaceItm.Slot.ToString() & "_count", True)(0),
                                Label)
                    countLabel.Text = ""
                    countLabel.Visible = False
                    countLabel.Tag = replaceItm
                    TryCast(sender, PictureBox).BackgroundImage = My.Resources.add_
                End If
                TryCast(sender, PictureBox).Visible = False
            End If
        End Sub

        Private Sub removeinventboxBag_Click(sender As Object, e As EventArgs)
            Dim locPanel = CType(CType(TryCast(sender, PictureBox).Tag, Object())(0), ItemPanel)
            Dim locPic = CType(CType(TryCast(sender, PictureBox).Tag, Object())(1), PictureBox)
            Dim oldItm = CType(locPanel.Tag, Item)
            If oldItm.Id = 0 Then
                Dim result As String = InputBox(MSG_ENTERITEMID, MSG_ADDITEM, "0")
                If result.Length = 0 Then
                    TryCast(sender, PictureBox).Visible = False
                Else
                    Dim intResult As Integer = TryInt(result)
                    If intResult <> 0 Then
                        Dim checkName As String = GetItemNameByItemId(intResult, MySettings.Default.language)
                        If Not checkName = "Not found" AndAlso GetItemSlotCountByItemId(intResult) > 0 Then
                            If GlobalVariables.currentEditedCharSet Is Nothing Then _
                                GlobalVariables.currentEditedCharSet =
                                    DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet)
                            Dim replaceItm As New Item()
                            replaceItm.Slot = oldItm.Slot
                            replaceItm.Id = intResult
                            replaceItm.Image = GetItemIconByItemId(replaceItm.Id, GlobalVariables.GlobalWebClient)
                            replaceItm.Name = checkName
                            replaceItm.Rarity = CType(GetItemQualityByItemId(replaceItm.Id), Item.RarityType)
                            replaceItm.AddedBag = True
                            Dim newGuid = 1
                            Do
                                If GlobalVariables.nonUsableGuidList.Contains(newGuid) Then
                                    newGuid += 1
                                Else
                                    GlobalVariables.nonUsableGuidList.Add(newGuid)
                                    Exit Do
                                End If
                            Loop
                            replaceItm.Guid = newGuid
                            locPanel.BackColor = GetItemQualityColor(replaceItm.Rarity)
                            GlobalVariables.currentEditedCharSet.InventoryZeroItems.Add(replaceItm)
                            replaceItm.BagItems = New List(Of Item)()
                            replaceItm.SlotCount = GetItemSlotCountByItemId(replaceItm.Id)
                            locPic.Tag = replaceItm
                            locPanel.Tag = replaceItm
                            locPic.BackgroundImage = replaceItm.Image
                            InfoToolTip.SetToolTip(TryCast(sender, PictureBox), TOOLTIP_REMOVE)
                            TryCast(sender, PictureBox).BackgroundImage = My.Resources.trash__delete__16x16
                        Else
                            MsgBox(MSG_INVALIDITEMCLASS, MsgBoxStyle.Critical, MSG_ERROR)
                        End If
                    Else
                        MsgBox(MSG_INVALIDITEMID, MsgBoxStyle.Critical, MSG_ERROR)
                    End If
                    TryCast(sender, PictureBox).Visible = False
                End If
            Else
                Dim result = MsgBox(MSG_DELETEITEM, vbYesNo, MSG_AREYOUSURE)
                If result = MsgBoxResult.Yes Then
                    BagItemPanel.Controls.Clear()
                    _currentBag = Nothing
                    If GlobalVariables.currentEditedCharSet Is Nothing Then _
                        GlobalVariables.currentEditedCharSet =
                            DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet)
                    locPanel.BackColor = referenceItmPanel.BackColor
                    locPic.BackgroundImage = My.Resources.bag_empty
                    Dim replaceItm As New Item()
                    replaceItm.Slot = oldItm.Slot
                    locPanel.Tag = replaceItm
                    locPic.Tag = replaceItm
                    InfoToolTip.SetToolTip(TryCast(sender, PictureBox), TOOLTIP_ADD)
                    If Not oldItm.BagItems Is Nothing Then
                        For i = oldItm.BagItems.Count - 1 To 0 Step - 1
                            Dim thisItm As Item = oldItm.BagItems(i)
                            Dim resultItem As Item =
                                    GlobalVariables.currentEditedCharSet.InventoryItems.Find(
                                        Function(item) item.Bagguid = thisItm.Bagguid AndAlso item.Slot = thisItm.Slot)
                            If resultItem IsNot Nothing Then _
                                GlobalVariables.currentEditedCharSet.InventoryItems.Remove(resultItem)
                        Next
                    End If
                    GlobalVariables.currentEditedCharSet.InventoryZeroItems.RemoveAt(
                        GlobalVariables.currentEditedCharSet.InventoryZeroItems.FindIndex(
                            Function(item) item.Slot = oldItm.Slot))
                    TryCast(sender, PictureBox).BackgroundImage = My.Resources.add_
                End If
                TryCast(sender, PictureBox).Visible = False
            End If
        End Sub

        Private Sub removeinventbox_MouseEnter(sender As Object, e As EventArgs)
            If Not TryCast(sender, PictureBox).BackgroundImage Is Nothing Then
                _tmpImage = TryCast(sender, PictureBox).BackgroundImage
                Application.DoEvents()
                Dim picbx = TryCast(sender, PictureBox)
                Dim g As Graphics
                Dim img As Bitmap
                Dim r As Rectangle
                img = CType(picbx.BackgroundImage, Bitmap)
                TryCast(sender, PictureBox).BackgroundImage = New Bitmap(picbx.Width, picbx.Height,
                                                                         PixelFormat.Format32bppArgb)
                g = Graphics.FromImage(picbx.BackgroundImage)
                r = New Rectangle(0, 0, picbx.Width, picbx.Height)
                g.DrawImage(img, r)
                SetBrightness(0.2, g, img, r, picbx)
            End If
        End Sub

        Private Sub removeinventbox_MouseLeave(sender As Object, e As EventArgs)
            If Not _tmpImage Is Nothing And Not TryCast(sender, PictureBox).BackgroundImage Is Nothing Then
                Dim picbox = TryCast(sender, PictureBox)
                picbox.BackgroundImage = _tmpImage
                picbox.Refresh()
                Application.DoEvents()
            End If
        End Sub

        Private Sub SetInventorySlot(itm As Item, slot As Integer)
            For Each itmctrl As ItemPanel In _
                From itmctrl1 As Object In BagItemPanel.Controls
                    Where TryCast(itmctrl1, ItemPanel).Name.Contains("_" & slot.ToString() & "_")
                itmctrl.BackColor = GetItemQualityColor(GetItemQualityByItemId(itm.Id))
                itmctrl.Tag = itm
                If itm.Count > 1 OrElse itm.Count = 1 AndAlso GetItemMaxStackByItemId(itm.Id) > 1 Then
                    Dim countLabel =
                            CType(itmctrl.Controls.Find("bankitm_slot_" & slot.ToString() & "_count", True)(0), Label)
                    countLabel.Text = itm.Count.ToString()
                    countLabel.Tag = itm
                    countLabel.Visible = True
                End If
                InfoToolTip.SetToolTip(itmctrl, itm.Name)
                For Each itmPicCtrl As Control In itmctrl.Controls
                    If Not itmPicCtrl.Name Is Nothing Then
                        If itmPicCtrl.Name.EndsWith("_remove") Then
                            Dim itmPicBox = CType(itmPicCtrl, PictureBox)
                            If itm.Id <> 0 Then
                                itmPicBox.BackgroundImage = My.Resources.trash__delete__16x16
                                InfoToolTip.SetToolTip(itmPicBox, TOOLTIP_REMOVE)
                            Else
                                itmPicBox.BackgroundImage = My.Resources.add_
                                InfoToolTip.SetToolTip(itmPicBox, TOOLTIP_ADD)
                            End If
                            Continue For
                        End If
                    End If
                    If TypeOf itmPicCtrl Is PictureBox Then
                        Dim itmPicBox = CType(itmPicCtrl, PictureBox)
                        itmPicBox.BackgroundImage = itm.Image
                        itmPicBox.Tag = itm
                        InfoToolTip.SetToolTip(itmPicBox, itm.Name)
                    End If
                Next
                BagItemPanel.Update()
                Application.DoEvents()
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