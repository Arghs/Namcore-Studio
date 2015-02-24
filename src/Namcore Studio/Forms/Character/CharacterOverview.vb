'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013-2015 NamCore Studio <https://github.com/megasus/Namcore-Studio>
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
'*      /Filename:      CharacterOverview
'*      /Description:   Displays character information
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.Linq
Imports System.Drawing.Imaging
Imports NamCore_Studio.Modules
Imports NCFramework.My
Imports NCFramework.Framework.Core
Imports NCFramework.Framework.Functions
Imports NamCore_Studio.Modules.Interface
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Extension
Imports NCFramework.Framework.Modules
Imports NamCore_Studio.Forms.Extension
Imports System.Threading
Imports NCFramework.My.Resources
Imports libnc.Provider
Imports System.Net
Imports NCFramework.Framework.Core.Update

Namespace Forms.Character
    Public Class CharacterOverview
        Inherits EventTrigger

        '// Declaration
        Dim _controlLst As List(Of Control)
        Dim _inventoryControlLst As List(Of Control)
        Dim _pubItm As Item
        Dim _tempValue As String
        Dim _tempSender As Object
        Dim _tmpSetId As Integer
        Dim _tmpImage As Bitmap
        Dim _tmpSenderPic As Object
        Dim _currentSet As Integer
        Dim _currentAccount As Account
        Dim _currentBag As Item
        Dim _lastRemovePic As PictureBox
        Dim _visibleActionControls As List(Of Control)

        Shared _loadComplete As Boolean = False
        Shared _doneControls As List(Of Control)

        Private ReadOnly _context As SynchronizationContext = SynchronizationContext.Current
        Public Event PrepareCompleted As EventHandler(Of CompletedEventArgs)
        Public Event OnCoreLoaded As EventHandler(Of CompletedEventArgs)

        Delegate Sub Prep(ByVal id As Integer, ByVal nxt As Boolean)

        Protected Overridable Sub OnCompleted(ByVal e As CompletedEventArgs)
            RaiseEvent PrepareCompleted(Me, e)
        End Sub

        Protected Overridable Sub OnCoreCompleted(ByVal e As CompletedEventArgs)
            RaiseEvent OnCoreLoaded(Me, e)
        End Sub
        '// Declaration

        Public Sub prepare_interface(ByVal account As Account, ByVal setId As Integer,
                                     Optional forceLoadChar As Boolean = False)
            LogAppend("prepare_interface call", "CharacterOverview_prepare_interface", False)
            _visibleActionControls = New List(Of Control)()
            InfoToolTip.AutoPopDelay = 5000
            InfoToolTip.InitialDelay = 1000
            InfoToolTip.ReshowDelay = 500
            InventoryPanel.SetDoubleBuffered()
            _currentSet = setId
            GlobalVariables.currentViewedCharSetId = Nothing
            GlobalVariables.currentViewedCharSet = Nothing
            GlobalVariables.currentEditedCharSet = Nothing
            GlobalVariables.currentViewedCharSetId = setId
            GlobalVariables.currentViewedCharSet = GetCharacterSetBySetId(setId, account)
            _currentAccount = account
            If forceLoadChar Then GlobalVariables.currentViewedCharSet.Loaded = False
            If GlobalVariables.currentViewedCharSet Is Nothing Or GlobalVariables.currentViewedCharSet.Loaded = False _
                Then
                If GlobalVariables.armoryMode = False And GlobalVariables.templateMode = False Then
                    '//Load charset
                    LogAppend("Loading character from database", "CharacterOverview_prepare_interface", True)
                    Dim loadHandlerThread As Thread = New Thread(DirectCast(Sub() LoadCharacter(setId), ThreadStart))
                    loadHandlerThread.Start()
                    Exit Sub
                End If
            End If
            _doneControls = New List(Of Control)
            Goprep(setId, False)
            LogAppend("Character loaded!", "CharacterOverview_prepare_interface", True)
        End Sub

        Private Sub LoadCharacter(ByVal setId As Integer)
            Dim mCoreHandler As New CoreHandler
            mCoreHandler.HandleLoadingRequests(_currentAccount, setId)
            ThreadExtensions.ScSend(_context, New Action(Of CompletedEventArgs)(AddressOf OnCoreCompleted),
                                    New CompletedEventArgs())
        End Sub

        Private Sub OnCharacterLoaded() Handles Me.OnCoreLoaded
            GlobalVariables.currentViewedCharSet.Loaded = True
            SetCharacterSet(_currentSet, GlobalVariables.currentViewedCharSet, _currentAccount)
            _doneControls = New List(Of Control)
            Goprep(_currentSet, False)
            LogAppend("Character loaded!", "CharacterOverview_prepare_interface", True)
        End Sub

        Private Sub OnCompleted() Handles Me.PrepareCompleted
            Show()
            Userwait.Close()
            CloseProcessStatus()
        End Sub

        Private Sub Goprep(ByVal setId As Integer, ByVal nxt As Boolean)
            _tmpSetId = setId
            _controlLst = New List(Of Control)
            _controlLst = FindAllChildren()
            charname_lbl.Text = DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet).Name
            level_lbl.Text = DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet).Level.ToString
            class_lbl.Text = GetClassNameById(DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet).Cclass)
            race_lbl.Text = GetRaceNameById(DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet).Race)
            gender_lbl.Text = GetGenderNameById(DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet).Gender)
            loadedat_lbl.Text = GlobalVariables.currentViewedCharSet.LoadedDateTime.ToString()
            gold_txtbox.Text = (GlobalVariables.currentViewedCharSet.Gold/10000).ToString()
            Dim zeroBagItems As New List(Of Item)
            GlobalVariables.nonUsableGuidList = New List(Of Integer)()
            For Each subctrl As Control In GroupBox2.Controls
                If subctrl.Name.ToLower.Contains("panel") Then
                    Dim bagPanel As ItemPanel = TryCast(subctrl, ItemPanel)
                    Dim realBagSlot As Integer = TryInt(SplitString(subctrl.Name, "bag", "Panel")) + 17
                    Dim subItmRemovePic As New PictureBox
                    If realBagSlot = 18 Then Continue For
                    Dim tempItm As New Item
                    tempItm.Slot = realBagSlot
                    bagPanel.Tag = tempItm
                    subItmRemovePic.Name = "bag_" & realBagSlot.ToString() & "_remove"
                    subItmRemovePic.Cursor = Cursors.Hand
                    subItmRemovePic.Size = removeinventbox.Size
                    bagPanel.Controls.Add(subItmRemovePic)
                    subItmRemovePic.Location = removeinventbox.Location
                    subItmRemovePic.BackgroundImageLayout = ImageLayout.Stretch
                    subItmRemovePic.BackgroundImage = My.Resources.add_
                    subItmRemovePic.BackColor = removeinventbox.BackColor
                    subItmRemovePic.Tag =
                        {bagPanel,
                         subctrl.Controls.Find("bag" & (realBagSlot - 17).ToString() & "Pic", True)(
                             0)}
                    subItmRemovePic.Visible = False
                    subItmRemovePic.SetDoubleBuffered()
                    subItmRemovePic.BringToFront()
                    InfoToolTip.SetToolTip(subItmRemovePic, TOOLTIP_ADD)
                    AddHandler subItmRemovePic.MouseClick, AddressOf removeinventboxBag_Click
                    AddHandler subItmRemovePic.MouseEnter, AddressOf removeinventbox_MouseEnter
                    AddHandler subItmRemovePic.MouseLeave, AddressOf removeinventbox_MouseLeave
                    AddHandler bagPanel.MouseEnter, AddressOf BagItem_MouseEnter
                    AddHandler bagPanel.MouseLeave, AddressOf BagItem_MouseLeave
                End If
            Next
            If Not DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet).InventoryZeroItems Is Nothing Then

                For Each potCharBag As Item In _
                    DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet).InventoryZeroItems
                    potCharBag.BagItems = New List(Of Item)()
                    GlobalVariables.nonUsableGuidList.Add(potCharBag.Guid)
                    Select Case potCharBag.Slot
                        Case 19 To 22
                            For Each subctrl As Control In GroupBox2.Controls
                                If subctrl.Name.ToLower.Contains("panel") Then
                                    Dim bagPanel As ItemPanel = TryCast(subctrl, ItemPanel)
                                    Dim realBagSlot As Integer = TryInt(SplitString(subctrl.Name, "bag", "Panel")) + 17
                                    If subctrl.Name.Contains((potCharBag.Slot - 17).ToString()) Then
                                        Dim subItmRemovePic As PictureBox =
                                                CType(
                                                    bagPanel.Controls.Find("bag_" & realBagSlot.ToString() & "_remove",
                                                                           True)(0),
                                                    PictureBox)
                                        subItmRemovePic.BackgroundImage = My.Resources.trash__delete__16x16
                                        InfoToolTip.SetToolTip(subItmRemovePic, TOOLTIP_REMOVE)
                                        bagPanel.BackColor = GetItemQualityColor(GetItemQualityByItemId(potCharBag.Id))
                                        For Each potBagItem As Item In _
                                            DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet).
                                                InventoryItems
                                            If potBagItem.Bagguid = potCharBag.Guid Then
                                                If potBagItem.Name Is Nothing Then _
                                                    potBagItem.Name = GetItemNameByItemId(potBagItem.Id,
                                                                                          MySettings.Default.language)
                                                If potBagItem.Image Is Nothing Then _
                                                    potBagItem.Image = GetItemIconByItemId(potBagItem.Id,
                                                                                           GlobalVariables.
                                                                                              GlobalWebClient)
                                                potCharBag.BagItems.Add(potBagItem)
                                            End If
                                        Next
                                        potCharBag.SlotCount = GetItemSlotCountByItemId(potCharBag.Id)
                                        bagPanel.Tag = potCharBag
                                        For Each myPic As PictureBox In subctrl.Controls
                                            If myPic.Name Is Nothing Then Continue For
                                            If myPic.Name.EndsWith("_remove") Then Continue For
                                            myPic.BackgroundImage = GetItemIconByItemId(potCharBag.Id,
                                                                                        GlobalVariables.GlobalWebClient)
                                            myPic.Tag = potCharBag
                                        Next
                                    End If
                                End If
                            Next
                        Case 23 To 38
                            If potCharBag.Name Is Nothing Then _
                                potCharBag.Name = GetItemNameByItemId(potCharBag.Id, MySettings.Default.language)
                            If potCharBag.Image Is Nothing Then _
                                potCharBag.Image = GetItemIconByItemId(potCharBag.Id, GlobalVariables.GlobalWebClient)
                            zeroBagItems.Add(potCharBag)
                    End Select
                Next
                For Each subctrl As Control In GroupBox2.Controls
                    If subctrl.Name.Contains("1") Then
                        If subctrl.Name.ToLower.Contains("panel") Then
                            Dim bagPanel As ItemPanel = CType(subctrl, ItemPanel)
                            Dim bag As New Item
                            bag.BagItems = New List(Of Item)()
                            For Each myItem In zeroBagItems
                                bag.BagItems.Add(myItem)
                            Next
                            bag.SlotCount = 16
                            bagPanel.Tag = bag
                            For Each myPic As PictureBox In subctrl.Controls
                                myPic.Tag = bag
                            Next
                        End If
                    End If
                Next
            End If
            If nxt = True Then _controlLst.Reverse()
            Try
                '// Set controls double buffered
                For Each itemControl As Control In _controlLst
                    itemControl.SetDoubleBuffered()
                    Dim tmpdone As List(Of Control) = _doneControls
                    If tmpdone.Contains(itemControl) Then
                        Continue For
                    Else
                        _doneControls.Add(itemControl)
                    End If
                    Select Case True
                        Case TypeOf itemControl Is Label
                            If itemControl.Name.ToLower.Contains("_name") Then
                                Dim slot As Integer = TryInt(SplitString(itemControl.Name, "slot_", "_name"))
                                Dim txt As String = CStr(LoadInfo(setId, slot, 0))
                                If Not txt Is Nothing Then
                                    InfoToolTip.SetToolTip(itemControl, txt)
                                    If txt.Length >= 25 Then
                                        Dim ccremove As Integer = txt.Length - 23
                                        txt = txt.Remove(23, ccremove) & "..."
                                    End If
                                End If
                                DirectCast(itemControl, Label).Text = txt
                                DirectCast(itemControl, Label).Tag = _pubItm
                                DirectCast(itemControl, Label).Cursor = Cursors.IBeam
                            ElseIf itemControl.Name.ToLower.EndsWith("enchant") Then
                                Dim slot As Integer = TryInt(SplitString(itemControl.Name, "slot_", "_enchant"))
                                Dim txt As String = CStr(LoadInfo(setId, slot, 1))
                                If Not txt Is Nothing Then
                                    InfoToolTip.SetToolTip(itemControl, txt)
                                    If txt.Length >= 25 Then
                                        Dim ccremove As Integer = txt.Length - 23
                                        txt = txt.Remove(23, ccremove) & "..."
                                    End If
                                End If
                                If _pubItm IsNot Nothing Then
                                    If txt Is Nothing Then
                                        txt = "+"
                                        DirectCast(itemControl, Label).Cursor = Cursors.Hand
                                    ElseIf txt = "" Then
                                        txt = "+"
                                        DirectCast(itemControl, Label).Cursor = Cursors.Hand
                                    Else
                                        DirectCast(itemControl, Label).Cursor = Cursors.IBeam
                                    End If
                                Else
                                    txt = ""
                                End If
                                DirectCast(itemControl, Label).Text = txt
                                DirectCast(itemControl, Label).Tag = _pubItm

                            End If
                        Case TypeOf itemControl Is PictureBox
                            If _
                                itemControl.Name.ToLower.Contains("_pic") And
                                Not itemControl.Name.ToLower.Contains("gem") Then
                                Dim slot As Integer = TryInt(SplitString(itemControl.Name, "slot_", "_pic"))
                                DirectCast(itemControl, PictureBox).Image = CType(LoadInfo(setId, slot, 2), Image)
                                If DirectCast(itemControl, PictureBox).Image Is Nothing Then _
                                    DirectCast(itemControl, PictureBox).Image = My.Resources.empty
                                DirectCast(itemControl, PictureBox).Tag = _pubItm
                            ElseIf itemControl.Name.ToLower.Contains("gem") Then
                                Dim slot As Integer = TryInt(SplitString(itemControl.Name, "slot_", "_gem"))
                                Dim gem As Integer = TryInt(SplitString(itemControl.Name, "gem", "_pic"))
                                Dim img As Bitmap = CType(LoadInfo(setId, slot, 2 + gem), Bitmap)
                                DirectCast(itemControl, PictureBox).Tag = _pubItm
                                If Not _pubItm Is Nothing Then
                                    DirectCast(itemControl, PictureBox).Cursor = Cursors.Hand
                                    If img Is Nothing Then
                                        DirectCast(itemControl, PictureBox).Image = My.Resources.add_
                                    Else
                                        DirectCast(itemControl, PictureBox).Image = img
                                    End If
                                    Select Case gem
                                        Case 1
                                            If Not _pubItm.Socket1Name Is Nothing Then _
                                                InfoToolTip.SetToolTip(itemControl, _pubItm.Socket1Name)
                                        Case 2
                                            If Not _pubItm.Socket2Name Is Nothing Then _
                                                InfoToolTip.SetToolTip(itemControl, _pubItm.Socket2Name)
                                        Case 3
                                            If Not _pubItm.Socket3Name Is Nothing Then _
                                                InfoToolTip.SetToolTip(itemControl, _pubItm.Socket3Name)
                                    End Select
                                Else

                                End If
                            End If
                        Case TypeOf itemControl Is Panel
                            If itemControl.Name.ToLower.EndsWith("color") Then
                                Dim slot As Integer = TryInt(SplitString(itemControl.Name, "slot_", "_color"))
                                DirectCast(itemControl, Panel).BackColor = CType(LoadInfo(setId, slot, 6), Color)
                                If _pubItm Is Nothing Then _
                                    DirectCast(itemControl, Panel).BackColor = SystemColors.ActiveBorder
                                DirectCast(itemControl, Panel).Tag = _pubItm
                            End If

                    End Select
                Next
                Application.DoEvents()
                _loadComplete = True
                ThreadExtensions.ScSend(_context, New Action(Of CompletedEventArgs)(AddressOf OnCompleted),
                                        New CompletedEventArgs())
            Catch ex As Exception
                LogAppend("Exception occoured: " & ex.ToString, "CharacterOverview_Goprep", True)
                _loadComplete = True
                ThreadExtensions.ScSend(_context, New Action(Of CompletedEventArgs)(AddressOf OnCompleted),
                                        New CompletedEventArgs())
            End Try
        End Sub

        Private Function LoadInfo(ByVal targetSet As Integer, ByVal slot As Integer, ByVal infotype As Integer) _
            As Object
            LogAppend("Loading info for slot " & slot.ToString, "CharacterOverview_LoadInfo", True)
            _pubItm = New Item
            Dim itm As Item = GetCharacterArmorItem(GetCharacterSetBySetId(targetSet, _currentAccount), slot.ToString,
                                                    True)
            If itm Is Nothing Then
                _pubItm = itm
            Else
                _pubItm = DeepCloneHelper.DeepClone(itm)
            End If
            If itm Is Nothing Then Return Nothing
            Select Case infotype
                Case 0
                    If itm.Name Is Nothing Then
                        itm.Name = GetItemNameByItemId(itm.Id, MySettings.Default.language)
                    End If
                    Return itm.Name
                Case 1
                    Return itm.EnchantmentName
                Case 2
                    If itm.Image Is Nothing Then
                        itm.Image = GetItemIconByItemId(itm.Id, GlobalVariables.GlobalWebClient)
                    End If
                    Return itm.Image

                Case 3
                    If itm.Socket1Pic Is Nothing Then
                        Return Nothing
                    Else
                        Return itm.Socket1Pic
                    End If
                Case 4
                    If itm.Socket2Pic Is Nothing Then
                        Return Nothing
                    Else
                        Return itm.Socket2Pic
                    End If
                Case 5
                    If itm.Socket3Pic Is Nothing Then
                        Return Nothing
                    Else
                        Return itm.Socket3Pic
                    End If
                Case 6
                    If itm.Rarity = Nothing Then itm.Rarity = CType(GetItemQualityByItemId(itm.Id), Item.RarityType)
                    Return GetItemQualityColor(itm.Rarity)
                Case Else : Return Nothing
            End Select
        End Function

        Private Sub label_Click(sender As Object, e As EventArgs) _
            Handles slot_9_name.Click, slot_9_enchant.Click, slot_8_name.Click, slot_8_enchant.Click, slot_7_name.Click,
                    slot_7_enchant.Click, slot_6_name.Click, slot_6_enchant.Click, slot_5_name.Click,
                    slot_5_enchant.Click,
                    slot_4_name.Click, slot_4_enchant.Click, slot_3_name.Click, slot_3_enchant.Click, slot_2_name.Click,
                    slot_2_enchant.Click, slot_18_name.Click, slot_18_enchant.Click, slot_17_name.Click,
                    slot_17_enchant.Click, slot_16_name.Click, slot_16_enchant.Click, slot_15_name.Click,
                    slot_15_enchant.Click, slot_14_name.Click, slot_14_enchant.Click, slot_13_name.Click,
                    slot_13_enchant.Click, slot_12_name.Click, slot_12_enchant.Click, slot_11_name.Click,
                    slot_11_enchant.Click, slot_10_name.Click, slot_10_enchant.Click, slot_1_name.Click,
                    slot_1_enchant.Click, slot_0_name.Click, slot_0_enchant.Click
            Dim senderLabel As Label = TryCast(sender, Label)
            If senderLabel.Text = "" Then Exit Sub
            Dim tagItm As Item = CType(senderLabel.Tag, Item)
            If Not senderLabel.Text = "+" Then
                If tagItm.EnchantmentId = 0 Then _
                    tagItm.EnchantmentId = GetSpellIdByEffectId(tagItm.EnchantmentEffectid,
                                                                GlobalVariables.sourceExpansion)
                If tagItm.EnchantmentType = 2 Then
                    Dim tmpItmId As Integer = GetItemIdBySpellId(tagItm.EnchantmentEffectid)
                    If tmpItmId <> 0 Then tagItm.EnchantmentId = tmpItmId
                End If
            End If
            senderLabel.Tag = tagItm
            TextBox1.Text = ""
            Try
                Dim newPoint As New Point
                newPoint.X = senderLabel.Location.X + InventoryPanel.Location.X
                newPoint.Y = senderLabel.Location.Y + InventoryPanel.Location.Y
                changepanel.Location = newPoint
                newPoint.X = 4000
                newPoint.Y = 4000
                classpanel.Location = newPoint
                racepanel.Location = newPoint
                addpanel.Location = newPoint
                PictureBox2.Visible = True
                If Not _tempSender Is Nothing Then
                    TryCast(_tempSender, Control).Visible = True
                End If
                _tempSender = sender
                senderLabel.Visible = False
                If senderLabel.Name.Contains("_name") Then
                    TextBox1.Text = CType(senderLabel.Tag, Item).Id.ToString
                ElseIf senderLabel.Name.Contains("_lbl") Then
                    TextBox1.Text = senderLabel.Text
                Else
                    Dim itm As Item = CType(senderLabel.Tag, Item)
                    TextBox1.Text = itm.EnchantmentId.ToString
                End If
                _tempValue = TextBox1.Text
            Catch
            End Try
        End Sub

        Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
            'Change value
            Dim newPoint As New Point
            Dim senderLabel As Label = Nothing
            Dim senderPic As PictureBox = Nothing
            If TypeOf (_tempSender) Is Label Then
                senderLabel = CType(_tempSender, Label)
            ElseIf TypeOf (_tempSender) Is PictureBox Then
                senderPic = CType(_tempSender, PictureBox)
            Else : Exit Sub
            End If
            newPoint.X = 4000
            newPoint.Y = 4000
            If Not TextBox1.Text = _tempValue Then
                If TypeOf _tempSender Is Label Then
                    Dim id As Integer = TryInt(TextBox1.Text)
                    If senderLabel.Name.ToLower.EndsWith("charname_lbl") Then
                        If TextBox1.Text = "" Then

                        Else
                            If GlobalVariables.currentEditedCharSet Is Nothing Then _
                                GlobalVariables.currentEditedCharSet =
                                    DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet)
                            GlobalVariables.currentEditedCharSet.Name = TextBox1.Text
                            senderLabel.Text = TextBox1.Text
                            InfoToolTip.SetToolTip(senderLabel, TextBox1.Text)
                        End If
                    ElseIf senderLabel.Name.ToLower.EndsWith("level_lbl") Then
                        If TextBox1.Text = "" Then

                        Else
                            Dim newlvl As Integer = TryInt(TextBox1.Text)
                            If newlvl = 0 Then

                            Else
                                If GlobalVariables.currentEditedCharSet Is Nothing Then _
                                    GlobalVariables.currentEditedCharSet =
                                        DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet)
                                GlobalVariables.currentEditedCharSet.Level = newlvl
                                senderLabel.Text = TextBox1.Text
                            End If
                        End If
                    ElseIf senderLabel.Name.ToLower.EndsWith("_enchant") Then
                        Dim client As New WebClient
                        client.CheckProxy()
                        Dim spellcontext As String
                        Dim foundspell As Boolean = False
                        Dim spellname As String = ""
                        Dim itemcontext As String
                        Dim founditem As Boolean = False
                        Dim itemname As String = ""
                        Try
                            spellcontext = client.DownloadString("http://www.wowhead.com/spell=" & TextBox1.Text)
                        Catch ex As Exception
                            spellcontext = ""
                        End Try
                        If _
                            Not spellcontext = "" And
                            Not spellcontext.Contains("<div Guid=""inputbox-error"">This spell doesn't exist.</div>") And
                            spellcontext.Contains(""">Enchant Item") Then
                            foundspell = True
                            spellname = SplitString(spellcontext, "<meta property=""og&#x3A;title"" content=""", """ />")
                            spellname = spellname.Replace("&#x20;", " ")
                        End If
                        Try
                            itemcontext = client.DownloadString("http://www.wowhead.com/item=" & TextBox1.Text & "&xml")
                        Catch ex As Exception
                            itemcontext = ""
                        End Try
                        If _
                            Not itemcontext = "" And
                            Not _
                            itemcontext.Contains(
                                "<div Guid=""inputbox-error"">This item doesn't exist or is not yet in the database.</div>") And
                            itemcontext.Contains("</class><subclass id=""6"">") Then
                            founditem = True
                            itemname = SplitString(itemcontext, "<name><![CDATA[", "]]></name>")
                            itemname = itemname.Replace("&#x20;", " ")
                        End If
                        If founditem = True AndAlso foundspell = True Then
                            selectenchpanel.Location = changepanel.Location
                            spellench.Text = "Spell: " & spellname
                            spellench.Tag = spellname
                            itmench.Text = "Item: " & itemname
                            itmench.Tag = itemname
                        ElseIf founditem = True Then
                            Dim itm As Item = CType(senderLabel.Tag, Item)
                            senderLabel.Text = itemname
                            itm.EnchantmentType = Item.EnchantmentTypes.ENCHTYPE_SPELL
                            itm.EnchantmentId = TryInt(TextBox1.Text)
                            itm.EnchantmentName = itemname
                            senderLabel.Tag = itm
                            InfoToolTip.SetToolTip(senderLabel, itm.EnchantmentName)
                            If GlobalVariables.currentEditedCharSet Is Nothing Then _
                                GlobalVariables.currentEditedCharSet =
                                    DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet)
                            SetCharacterArmorItem(GlobalVariables.currentEditedCharSet, CType(senderLabel.Tag, Item))
                        ElseIf foundspell = True Then
                            Dim itm As Item = CType(senderLabel.Tag, Item)
                            itm.EnchantmentId = TryInt(TextBox1.Text)
                            itm.EnchantmentEffectid = GetEffectIdBySpellId(itm.EnchantmentId,
                                                                           GlobalVariables.sourceExpansion)
                            itm.EnchantmentName = GetEffectNameById(itm.EnchantmentEffectid, MySettings.Default.language)
                            itm.EnchantmentType = 0
                            senderLabel.Text = itm.EnchantmentName
                            senderLabel.Tag = itm
                            InfoToolTip.SetToolTip(senderLabel, itm.EnchantmentName)
                            If GlobalVariables.currentEditedCharSet Is Nothing Then _
                                GlobalVariables.currentEditedCharSet =
                                    DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet)
                            SetCharacterArmorItem(GlobalVariables.currentEditedCharSet, CType(senderLabel.Tag, Item))
                        Else
                            MsgBox(MSG_INVALIDITEMCLASS, MsgBoxStyle.Critical, MSG_ERROR)
                        End If
                    Else
                        If _
                            Not _
                            GetItemInventorySlotByItemId(CType(senderLabel.Tag, Item).Guid) =
                            GetItemInventorySlotByItemId(id) _
                            Then
                            MsgBox(MSG_INVALIDITEMCLASS, MsgBoxStyle.Critical, MSG_ERROR)
                        Else
                            Dim newitm As Item = CType(senderLabel.Tag, Item)
                            newitm.ReplaceItem(id)
                            senderLabel.Tag = newitm
                            Dim txt As String = CType(senderLabel.Tag, Item).Name
                            If Not txt Is Nothing Then
                                If txt.Length >= 25 Then
                                    Dim ccremove As Integer = txt.Length - 23
                                    txt = txt.Remove(23, ccremove) & "..."
                                End If
                            End If
                            senderLabel.Text = txt
                            For Each ctrl As Control In _controlLst
                                If TypeOf ctrl Is PictureBox Then
                                    If ctrl.Tag Is Nothing Then Continue For
                                    If CType(ctrl.Tag, Item).Guid = CType(senderLabel.Tag, Item).Guid Then
                                        DirectCast(ctrl, PictureBox).Tag = senderLabel.Tag
                                        Select Case True
                                            Case _
                                                ctrl.Name.ToLower.EndsWith("_pic") And
                                                Not ctrl.Name.ToLower.Contains("gem")
                                                DirectCast(ctrl, PictureBox).Image = CType(senderLabel.Tag, Item).Image
                                            Case ctrl.Name.ToLower.Contains("gem1")
                                                DirectCast(ctrl, PictureBox).Image =
                                                    CType(senderLabel.Tag, Item).Socket1Pic
                                            Case ctrl.Name.ToLower.Contains("gem2")
                                                DirectCast(ctrl, PictureBox).Image =
                                                    CType(senderLabel.Tag, Item).Socket1Pic
                                            Case ctrl.Name.ToLower.Contains("gem3")
                                                DirectCast(ctrl, PictureBox).Image =
                                                    CType(senderLabel.Tag, Item).Socket3Pic
                                        End Select
                                    End If
                                ElseIf TypeOf ctrl Is Panel Then
                                    If ctrl.Tag Is Nothing Then Continue For
                                    If CType(ctrl.Tag, Item).Guid = CType(senderLabel.Tag, Item).Guid Then
                                        If ctrl.Name.ToLower.EndsWith("color") Then
                                            DirectCast(ctrl, Panel).BackColor =
                                                GetItemQualityColor(CType(senderLabel.Tag, Item).Rarity)
                                            DirectCast(ctrl, Panel).Tag = senderLabel.Tag
                                        End If
                                    End If
                                ElseIf TypeOf ctrl Is Label Then
                                    If ctrl.Tag Is Nothing Then Continue For
                                    If CType(ctrl.Tag, Item).Guid = CType(senderLabel.Tag, Item).Guid Then
                                        If ctrl.Name.ToLower.EndsWith("_enchant") Then
                                            DirectCast(ctrl, Label).Tag = senderLabel.Tag
                                        End If
                                    End If
                                End If

                            Next
                            If GlobalVariables.currentEditedCharSet Is Nothing Then _
                                GlobalVariables.currentEditedCharSet =
                                    DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet)
                            SetCharacterArmorItem(GlobalVariables.currentEditedCharSet, CType(senderLabel.Tag, Item))
                        End If
                    End If
                ElseIf TypeOf _tempSender Is PictureBox Then
                    If senderPic IsNot Nothing Then
                        If senderPic.Name.ToLower.Contains("_gem") Then
                            Dim client As New WebClient
                            client.CheckProxy()
                            Dim gemContext As String
                            Dim foundgem As Boolean = False
                            Try
                                gemContext =
                                    client.DownloadString("http://www.wowhead.com/item=" & TextBox1.Text & "&xml")
                            Catch ex As Exception
                                gemContext = ""
                            End Try
                            If _
                                Not gemContext = "" And
                                Not gemContext.Contains("<error>Item not found!</error>") And
                                gemContext.Contains("<class id=""3"">") Then
                                foundgem = True
                            End If
                            If foundgem = True Then
                                If GlobalVariables.currentEditedCharSet Is Nothing Then _
                                    GlobalVariables.currentEditedCharSet =
                                        DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet)
                                Dim itm As Item = CType(senderPic.Tag, Item)
                                Dim socketId As Integer = TryInt(TextBox1.Text)
                                Select Case True
                                    Case senderPic.Name.Contains("gem1")
                                        itm.Socket1Id = socketId
                                        itm.Socket1Effectid = GetEffectIdByGemId(socketId)
                                        itm.Socket1Name = GetEffectNameById(itm.Socket1Effectid,
                                                                            MySettings.Default.language)
                                        itm.Socket1Pic = GetItemIconByItemId(socketId, GlobalVariables.GlobalWebClient)
                                        senderPic.Image = itm.Socket1Pic
                                    Case senderPic.Name.Contains("gem2")
                                        itm.Socket2Id = socketId
                                        itm.Socket2Effectid = GetEffectIdByGemId(socketId)
                                        itm.Socket2Name = GetEffectNameById(itm.Socket2Effectid,
                                                                            MySettings.Default.language)
                                        itm.Socket2Pic = GetItemIconByItemId(socketId, GlobalVariables.GlobalWebClient)
                                        senderPic.Image = itm.Socket2Pic
                                    Case senderPic.Name.Contains("gem3")
                                        itm.Socket3Id = socketId
                                        itm.Socket3Effectid = GetEffectIdByGemId(socketId)
                                        itm.Socket3Name = GetEffectNameById(itm.Socket3Effectid,
                                                                            MySettings.Default.language)
                                        itm.Socket3Pic = GetItemIconByItemId(socketId, GlobalVariables.GlobalWebClient)
                                        senderPic.Image = itm.Socket3Pic
                                End Select
                                senderPic.Refresh()
                                senderPic.Tag = itm
                                Dim relevantControls As Control() =
                                        _controlLst.FindAll(Function(control) control.Tag IsNot Nothing).ToArray()
                                Dim matchControls As Control() = Array.FindAll(relevantControls,
                                                                               Function(control) _
                                                                                  CType(control.Tag, Item).Guid =
                                                                                  itm.Guid)
                                If Not matchControls Is Nothing Then
                                    For i = 0 To matchControls.Length - 1
                                        matchControls(i).Tag = itm
                                    Next
                                End If
                                SetCharacterArmorItem(GlobalVariables.currentEditedCharSet, itm)
                            Else
                                MsgBox(MSG_INVALIDITEMCLASS, MsgBoxStyle.Critical, MSG_ERROR)
                            End If
                        End If
                    End If
                End If
            End If
            changepanel.Location = newPoint
            If senderLabel IsNot Nothing Then senderLabel.Visible = True
        End Sub

        Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
            'Detele item
            Dim newPoint As New Point
            Dim senderLabel As Label = Nothing
            Dim senderPic As PictureBox = Nothing
            If TypeOf (_tempSender) Is Label Then
                senderLabel = CType(_tempSender, Label)
            ElseIf TypeOf (_tempSender) Is PictureBox Then
                senderPic = CType(_tempSender, PictureBox)
            Else : Exit Sub
            End If
            newPoint.X = 4000
            newPoint.Y = 4000
            If senderLabel IsNot Nothing Then
                Dim tempSender = TryCast(_tempSender, Label)
                If (tempSender IsNot Nothing) Then
                    If Not senderLabel.Name.ToLower.EndsWith("_enchant") Then

                        Dim result = MsgBox(MSG_DELETEITEM, vbYesNo, MSG_AREYOUSURE)
                        If result = MsgBoxResult.Yes Then
                            For Each ctrl As Control In _controlLst
                                If TypeOf ctrl Is PictureBox Then
                                    If ctrl.Tag Is Nothing Then Continue For
                                    If CType(ctrl.Tag, Item).Guid = CType(senderLabel.Tag, Item).Guid Then
                                        DirectCast(ctrl, PictureBox).Tag = senderLabel.Tag
                                        Select Case True
                                            Case _
                                                ctrl.Name.ToLower.EndsWith("_pic") And
                                                Not ctrl.Name.ToLower.Contains("gem")
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
                                    If ctrl.Name.ToLower.EndsWith("color") Then
                                        If CType(ctrl.Tag, Item).Guid = CType(senderLabel.Tag, Item).Guid Then
                                            DirectCast(ctrl, Panel).BackColor = SystemColors.ActiveBorder
                                            DirectCast(ctrl, Panel).Tag = Nothing
                                        End If
                                    End If
                                ElseIf TypeOf ctrl Is Label Then
                                    If ctrl.Tag Is Nothing Then Continue For
                                    If CType(ctrl.Tag, Item).Guid = CType(senderLabel.Tag, Item).Guid Then
                                        If ctrl.Name.ToLower.EndsWith("_enchant") Then
                                            DirectCast(ctrl, Label).Tag = Nothing
                                            DirectCast(ctrl, Label).Text = ""
                                        End If
                                    End If
                                End If
                            Next
                            If GlobalVariables.currentEditedCharSet Is Nothing Then _
                                GlobalVariables.currentEditedCharSet =
                                    DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet)
                            RemoveCharacterArmorItem(GlobalVariables.currentEditedCharSet, CType(senderLabel.Tag, Item))
                            senderLabel.Text = Nothing
                            senderLabel.Tag = Nothing
                        End If
                    Else
                        If GlobalVariables.currentEditedCharSet Is Nothing Then _
                            GlobalVariables.currentEditedCharSet =
                                DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet)
                        Dim pubItem As Item = CType(tempSender.Tag, Item)
                        pubItem.RemoveEnchantments()
                        SetCharacterArmorItem(GlobalVariables.currentEditedCharSet, pubItem)
                        tempSender.Tag = pubItem
                        tempSender.Text = "+"
                        senderLabel.Cursor = Cursors.Hand
                        Dim relevantControls As Control() =
                                _controlLst.FindAll(Function(control) control.Tag IsNot Nothing).ToArray()
                        Dim matchControls As Control() = Array.FindAll(relevantControls,
                                                                       Function(control) _
                                                                          CType(control.Tag, Item).Guid = pubItem.Guid)
                        If Not matchControls Is Nothing Then
                            For i = 0 To matchControls.Length - 1
                                matchControls(i).Tag = pubItem
                            Next
                        End If
                    End If
                End If
            ElseIf senderPic IsNot Nothing Then
                '// Delete Gem
                If GlobalVariables.currentEditedCharSet Is Nothing Then _
                    GlobalVariables.currentEditedCharSet =
                        DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet)
                Dim pubItem As Item = CType(senderPic.Tag, Item)
                pubItem.RemoveGem(TryInt(SplitString(senderPic.Name, "_gem", "_")))
                SetCharacterArmorItem(GlobalVariables.currentEditedCharSet, pubItem)
                senderPic.Image = My.Resources.add_
                senderPic.Tag = pubItem
                Dim relevantControls As Control() =
                        _controlLst.FindAll(Function(control) control.Tag IsNot Nothing).ToArray()
                Dim matchControls As Control() = Array.FindAll(relevantControls,
                                                               Function(control) _
                                                                  CType(control.Tag, Item).Guid = pubItem.Guid)
                If Not matchControls Is Nothing Then
                    For i = 0 To matchControls.Length - 1
                        matchControls(i).Tag = pubItem
                    Next
                End If
            End If
            changepanel.Location = newPoint
            If senderLabel IsNot Nothing Then senderLabel.Visible = True
        End Sub

        Private Sub race_lbl_Click(sender As Object, e As EventArgs) Handles race_lbl.Click
            racepanel.Location = New Point(TryCast(sender, Label).Location.X + GroupBox1.Location.X,
                                           TryCast(sender, Label).Location.Y + GroupBox1.Location.Y)
            Dim newpoint As New Point
            newpoint.X = 4000
            newpoint.Y = 4000
            classpanel.Location = newpoint
            changepanel.Location = newpoint
            genderpanel.Location = newpoint
            PictureBox2.Visible = False
            If Not _tempSender Is Nothing Then
                TryCast(_tempSender, Control).Visible = True
            End If
            _tempSender = sender
            TryCast(sender, Label).Visible = False
            racecombo.Text = TryCast(sender, Label).Text
            _tempValue = racecombo.Text
        End Sub

        Private Sub class_lbl_Click(sender As Object, e As EventArgs) Handles class_lbl.Click
            classpanel.Location = New Point(TryCast(sender, Label).Location.X + GroupBox1.Location.X,
                                            TryCast(sender, Label).Location.Y + GroupBox1.Location.Y)
            Dim newpoint As New Point
            newpoint.X = 4000
            newpoint.Y = 4000
            racepanel.Location = newpoint
            changepanel.Location = newpoint
            genderpanel.Location = newpoint
            PictureBox2.Visible = False
            If Not _tempSender Is Nothing Then
                TryCast(_tempSender, Control).Visible = True
            End If
            _tempSender = sender
            TryCast(sender, Label).Visible = False
            classcombo.Text = TryCast(sender, Label).Text
            _tempValue = classcombo.Text
        End Sub

        Private Sub charname_lbl_Click(sender As Object, e As EventArgs) Handles charname_lbl.Click
            changepanel.Location = TryCast(sender, Label).Location
            Dim newpoint As New Point
            newpoint.X = 4000
            newpoint.Y = 4000
            classpanel.Location = newpoint
            racepanel.Location = newpoint
            genderpanel.Location = newpoint
            PictureBox2.Visible = False
            If Not _tempSender Is Nothing Then
                TryCast(_tempSender, Control).Visible = True
            End If
            _tempSender = sender
            TryCast(sender, Label).Visible = False
            TextBox1.Text = TryCast(sender, Label).Text
            _tempValue = TextBox1.Text
        End Sub

        Private Sub level_lbl_Click(sender As Object, e As EventArgs) Handles level_lbl.Click
            changepanel.Location = New Point(TryCast(sender, Label).Location.X + GroupBox1.Location.X,
                                             TryCast(sender, Label).Location.Y + GroupBox1.Location.Y)
            Dim newpoint As New Point
            newpoint.X = 4000
            newpoint.Y = 4000
            classpanel.Location = newpoint
            racepanel.Location = newpoint
            genderpanel.Location = newpoint
            PictureBox2.Visible = False
            If Not _tempSender Is Nothing Then
                TryCast(_tempSender, Control).Visible = True
            End If
            _tempSender = sender
            TryCast(sender, Label).Visible = False
            TextBox1.Text = TryCast(sender, Label).Text
            _tempValue = TextBox1.Text
        End Sub

        Private Sub gender_lbl_Click(sender As Object, e As EventArgs) Handles gender_lbl.Click
            genderpanel.Location = New Point(TryCast(sender, Label).Location.X + GroupBox1.Location.X,
                                             TryCast(sender, Label).Location.Y + GroupBox1.Location.Y)
            Dim newpoint As New Point
            newpoint.X = 4000
            newpoint.Y = 4000
            classpanel.Location = newpoint
            racepanel.Location = newpoint
            changepanel.Location = newpoint
            PictureBox2.Visible = False
            If Not _tempSender Is Nothing Then
                TryCast(_tempSender, Control).Visible = True
            End If
            _tempSender = sender
            TryCast(sender, Label).Visible = False
            gendercombo.Text = TryCast(sender, Label).Text
            _tempValue = gendercombo.Text
        End Sub

        Private Sub classrefresh_Click(sender As Object, e As EventArgs) Handles classrefresh.Click
            Dim newPoint As New Point
            Dim senderLabel As Label = CType(_tempSender, Label)
            newPoint.X = 4000
            newPoint.Y = 4000
            If Not classcombo.SelectedText = _tempValue And Not classcombo.Text = _tempValue Then
                senderLabel.Text = classcombo.SelectedText
            End If
            classpanel.Location = newPoint
            senderLabel.Visible = True
            If GlobalVariables.currentEditedCharSet Is Nothing Then _
                GlobalVariables.currentEditedCharSet = DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet)
            GlobalVariables.currentEditedCharSet.Cclass(0) = GetClassIdByName(senderLabel.Text)
        End Sub

        Private Sub racerefresh_Click(sender As Object, e As EventArgs) Handles racerefresh.Click
            Dim newPoint As New Point
            Dim senderLabel As Label = CType(_tempSender, Label)
            newPoint.X = 4000
            newPoint.Y = 4000
            If Not racecombo.SelectedText = _tempValue And Not racecombo.Text = _tempValue Then
                senderLabel.Text = racecombo.SelectedText
            End If
            racepanel.Location = newPoint
            senderLabel.Visible = True
            If GlobalVariables.currentEditedCharSet Is Nothing Then _
                GlobalVariables.currentEditedCharSet = DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet)
            GlobalVariables.currentEditedCharSet.Race(0) = GetRaceIdByName(senderLabel.Text)
        End Sub

        Private Sub genderrefresh_Click(sender As Object, e As EventArgs) Handles genderrefresh.Click
            Dim newPoint As New Point
            Dim senderLabel As Label = CType(_tempSender, Label)
            newPoint.X = 4000
            newPoint.Y = 4000
            If Not gendercombo.SelectedText = _tempValue And Not gendercombo.Text = _tempValue Then
                senderLabel.Text = gendercombo.SelectedText
            End If
            genderpanel.Location = newPoint
            senderLabel.Visible = True
            If GlobalVariables.currentEditedCharSet Is Nothing Then _
                GlobalVariables.currentEditedCharSet = DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet)
            If senderLabel.Text.StartsWith("M") Then
                GlobalVariables.currentEditedCharSet.Gender(0) = 0
            Else
                GlobalVariables.currentEditedCharSet.Gender(0) = 1
            End If
        End Sub

        Private Sub itmench_Click(sender As Object, e As EventArgs) Handles itmench.Click
            Dim newPoint As New Point
            Dim senderLabel As Label = CType(_tempSender, Label)
            newPoint.X = 4000
            newPoint.Y = 4000
            senderLabel.Text = CStr(itmench.Tag)
            CType(senderLabel.Tag, Item).EnchantmentType = Item.EnchantmentTypes.ENCHTYPE_SPELL
            CType(senderLabel.Tag, Item).EnchantmentId = TryInt(TextBox1.Text)
            CType(senderLabel.Tag, Item).EnchantmentName = CStr(itmench.Tag)
            selectenchpanel.Location = newPoint
        End Sub

        Private Sub spellench_Click(sender As Object, e As EventArgs) Handles spellench.Click
            Dim newPoint As New Point
            Dim senderLabel As Label = CType(_tempSender, Label)
            newPoint.X = 4000
            newPoint.Y = 4000
            senderLabel.Text = CStr(spellench.Tag)
            CType(senderLabel.Tag, Item).EnchantmentType = Item.EnchantmentTypes.ENCHTYPE_SPELL
            CType(senderLabel.Tag, Item).EnchantmentId = TryInt(TextBox1.Text)
            CType(senderLabel.Tag, Item).EnchantmentName = CStr(spellench.Tag)
            selectenchpanel.Location = newPoint
        End Sub

        Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Glyphs_bt.Click
            Dim mywindow As New GlyphsInterface
            For Each currentForm As Form In Application.OpenForms
                If currentForm.Name = "GlyphsInterface" Then
                    mywindow = DirectCast(currentForm, GlyphsInterface)
                End If
            Next
            If Not mywindow Is Nothing Then
                mywindow.Close()
            End If
            Dim glyphInterface As New GlyphsInterface
            Userwait.Show()
            Application.DoEvents()
            glyphInterface.PrepareGlyphsInterface(_tmpSetId, _currentAccount)
            glyphInterface.Show()
            Userwait.Close()
        End Sub

        Private Sub exit_bt_Click(sender As Object, e As EventArgs) Handles exit_bt.Click
            GlyphsInterface.Close()
            AchievementsInterface.Close()
            QuestsInterface.Close()
            ReputationInterface.Close()
            SpellSkillInterface.Close()
            Close()
        End Sub

        Private Sub ItemClick(sender As Object, e As EventArgs) _
            Handles slot_9_pic.Click, slot_8_pic.Click, slot_7_pic.Click, slot_6_pic.Click, slot_5_pic.Click,
                    slot_4_pic.Click, slot_3_pic.Click, slot_2_pic.Click, slot_18_pic.Click, slot_17_pic.Click,
                    slot_16_pic.Click, slot_15_pic.Click, slot_14_pic.Click, slot_13_pic.Click, slot_12_pic.Click,
                    slot_11_pic.Click, slot_10_pic.Click, slot_1_pic.Click, slot_0_pic.Click
            If Not TryCast(sender, PictureBox).Tag Is Nothing Then
                Try
                    Dim itemId As Integer = CType(TryCast(sender, PictureBox).Tag, Item).Id
                    Process.Start("http://wowhead.com/item=" & itemId.ToString())
                Catch ex As Exception

                End Try
            End If
            If Not _tempSender Is Nothing Then
                TryCast(_tempSender, Control).Visible = True
            End If
            changepanel.Location = New Point(4000, 4000)
            racepanel.Location = New Point(4000, 4000)
            classpanel.Location = New Point(4000, 4000)
            addpanel.Location = New Point(4000, 4000)
            genderpanel.Location = New Point(4000, 4000)
            For Each ctrl As Label In _
                From ctrl1 In _controlLst.OfType (Of Label)()
                    Where _
                        ctrl1.Name.StartsWith(TryCast(sender, PictureBox).Name.Replace("_pic", "")) And
                        ctrl1.Name.EndsWith("_name")
                    Where ctrl1.Text = ""
                _tempSender = ctrl
                _tmpSenderPic = sender
                ctrl.Visible = False
                Dim pnt As New Point
                pnt.X = ctrl.Location.X + InventoryPanel.Location.X
                pnt.Y = ctrl.Location.Y + InventoryPanel.Location.Y
                addpanel.Location = pnt
            Next
        End Sub

        Private Sub slot_0_pic_MouseEnter(sender As Object, e As EventArgs) _
            Handles slot_9_pic.MouseEnter, slot_8_pic.MouseEnter, slot_7_pic.MouseEnter, slot_6_pic.MouseEnter,
                    slot_5_pic.MouseEnter, slot_4_pic.MouseEnter, slot_3_pic.MouseEnter, slot_2_pic.MouseEnter,
                    slot_18_pic.MouseEnter, slot_17_pic.MouseEnter, slot_16_pic.MouseEnter, slot_15_pic.MouseEnter,
                    slot_14_pic.MouseEnter, slot_13_pic.MouseEnter, slot_12_pic.MouseEnter, slot_11_pic.MouseEnter,
                    slot_10_pic.MouseEnter, slot_1_pic.MouseEnter, slot_0_pic.MouseEnter

            If Not _loadComplete = False Then
                If Not TryCast(sender, PictureBox).Image Is Nothing Then
                    _tmpImage = CType(TryCast(sender, PictureBox).Image, Bitmap)
                    Application.DoEvents()
                    Dim picbx As PictureBox = TryCast(sender, PictureBox)
                    Dim g As Graphics
                    Dim img As Bitmap
                    Dim r As Rectangle
                    img = CType(picbx.Image, Bitmap)
                    TryCast(sender, PictureBox).Image = New Bitmap(picbx.Width, picbx.Height,
                                                                   PixelFormat.Format32bppArgb)
                    g = Graphics.FromImage(picbx.Image)
                    r = New Rectangle(0, 0, picbx.Width, picbx.Height)
                    g.DrawImage(img, r)
                    SetBrightness(0.2, g, img, r, picbx)
                End If
            End If
        End Sub

        Private Sub slot_0_pic_MouseLeave(sender As Object, e As EventArgs) _
            Handles slot_9_pic.MouseLeave, slot_8_pic.MouseLeave, slot_7_pic.MouseLeave, slot_6_pic.MouseLeave,
                    slot_5_pic.MouseLeave, slot_4_pic.MouseLeave, slot_3_pic.MouseLeave, slot_2_pic.MouseLeave,
                    slot_18_pic.MouseLeave, slot_17_pic.MouseLeave, slot_16_pic.MouseLeave, slot_15_pic.MouseLeave,
                    slot_14_pic.MouseLeave, slot_13_pic.MouseLeave, slot_12_pic.MouseLeave, slot_11_pic.MouseLeave,
                    slot_10_pic.MouseLeave, slot_1_pic.MouseLeave, slot_0_pic.MouseLeave
            If Not _tmpImage Is Nothing And Not TryCast(sender, PictureBox).Image Is Nothing Then
                Dim picbox As PictureBox = TryCast(sender, PictureBox)
                picbox.Image = _tmpImage
                picbox.Refresh()
                Application.DoEvents()
            End If
        End Sub

        Private Sub SetBrightness(ByVal brightness As Single, ByVal g As Graphics, ByVal img As Bitmap,
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

        Private Sub CharacterOverview_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
            changepanel.Location = New Point(4000, 4000)
            addpanel.Location = New Point(4000, 4000)
            racepanel.Location = New Point(4000, 4000)
            classpanel.Location = New Point(4000, 4000)
            genderpanel.Location = New Point(4000, 4000)
            If Not _tempSender Is Nothing Then
                TryCast(_tempSender, Control).Visible = True
            End If
            TextBox1.Text = ""
            TextBox2.Text = ""
        End Sub

        Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
            'Add item
            Dim senderPic As PictureBox = CType(_tmpSenderPic, PictureBox)
            If Not TextBox2.Text = "" Then
                Dim meSlot As String = TryCast(sender, Label).Name
                meSlot = meSlot.Replace("slot_", "")
                meSlot = meSlot.Replace("_name", "")
                If Not GetItemInventorySlotByItemId(TryInt(TextBox2.Text)) = TryInt(meSlot) Then
                    MsgBox(MSG_INVALIDITEMCLASS, MsgBoxStyle.Critical, MSG_ERROR)
                    Exit Sub
                Else
                    Dim itm As New Item
                    itm.Id = TryInt(TextBox2.Text)
                    Dim x As DateTime = Date.Now
                    itm.Guid = x.ToTimeStamp()
                    itm.Name = GetItemNameByItemId(itm.Id, MySettings.Default.language)
                    itm.Image = GetItemIconByItemId(itm.Id, GlobalVariables.GlobalWebClient)
                    itm.Rarity = CType(GetItemQualityByItemId(itm.Id), Item.RarityType)
                    itm.Slot = TryInt(meSlot)
                    itm.Slotname = CStr(GetItemInventorySlotByItemId(itm.Slot))
                    If itm.Slot = 15 Or itm.Slot = 16 Then LoadWeaponType(itm.Id, _currentSet, _currentAccount)
                    senderPic.Tag = itm
                    senderPic.Image = itm.Image
                    senderPic.Refresh()
                    DirectCast(_tempSender, Label).Text = itm.Name
                    DirectCast(_tempSender, Label).Tag = itm
                    If GlobalVariables.currentEditedCharSet Is Nothing Then _
                        GlobalVariables.currentEditedCharSet =
                            DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet)
                    GlobalVariables.currentEditedCharSet.ArmorItems.Add(itm)
                    changepanel.Location = New Point(4000, 4000)
                    addpanel.Location = New Point(4000, 4000)
                    racepanel.Location = New Point(4000, 4000)
                    classpanel.Location = New Point(4000, 4000)
                    If Not _tempSender Is Nothing Then
                        TryCast(sender, Label).Visible = True
                    End If
                    TextBox1.Text = ""
                    TextBox2.Text = ""
                End If
            End If
        End Sub

        Private Sub av_bt_Click(sender As Object, e As EventArgs) Handles av_bt.Click
            Dim mywindow As New AchievementsInterface
            For Each currentForm As Form In Application.OpenForms
                If currentForm.Name = "AchievementsInterface" Then
                    mywindow = DirectCast(currentForm, AchievementsInterface)
                End If
            Next
            If Not mywindow Is Nothing Then
                mywindow.Close()
            End If
            Dim avinterface As New AchievementsInterface
            Application.DoEvents()
            avinterface.Show()
        End Sub

        Private Sub Button2_Click(sender As Object, e As EventArgs) Handles rep_bt.Click
            NewProcessStatus()
            Dim mywindow As New ReputationInterface
            For Each currentForm As Form In Application.OpenForms
                If currentForm.Name = "ReputationInterface" Then
                    mywindow = DirectCast(currentForm, ReputationInterface)
                End If
            Next
            If Not mywindow Is Nothing Then
                mywindow.Close()
            End If
            Dim repinterface As New ReputationInterface
            Userwait.Show()
            Application.DoEvents()
            repinterface.PrepareRepInterface(_tmpSetId)
            repinterface.Show()
            Userwait.Close()
        End Sub

        Private Sub Quests_bt_Click(sender As Object, e As EventArgs) Handles Quests_bt.Click
            NewProcessStatus()
            Dim mywindow As New QuestsInterface
            For Each currentForm As Form In Application.OpenForms
                If currentForm.Name = "QuestsInterface" Then
                    mywindow = DirectCast(currentForm, QuestsInterface)
                End If
            Next
            If Not mywindow Is Nothing Then
                mywindow.Close()
            End If
            Dim qstInterface As New QuestsInterface
            Userwait.Show()
            Application.DoEvents()
            qstInterface.Show()
            qstInterface.PrepareInterface(_tmpSetId)
        End Sub

        Private Sub highlighter2_Click(sender As Object, e As EventArgs)
            Close()
        End Sub

        Private Sub InventoryPanel_MouseDown(sender As Object, e As MouseEventArgs) Handles InventoryPanel.MouseDown
            If e.Button = MouseButtons.Left Then
                CharacterOverview_MouseDown(sender, e)
            End If
        End Sub

        Private Sub savechanges_bt_Click(sender As Object, e As EventArgs) Handles savechanges_bt.Click
            If Not GlobalVariables.currentEditedCharSet Is Nothing Then
                If GlobalVariables.editedCharSets Is Nothing Then
                    GlobalVariables.editedCharSets = New List(Of NCFramework.Framework.Modules.Character)()
                End If
                If GlobalVariables.editedCharsIndex Is Nothing Then _
                    GlobalVariables.editedCharsIndex = New List(Of Integer())()
                For Each indexEntry As Integer() In _
                    From indexEntry1 In GlobalVariables.editedCharsIndex
                        Where indexEntry1(0) = GlobalVariables.currentEditedCharSet.Guid
                    GlobalVariables.editedCharSets.Item(indexEntry(1)) = GlobalVariables.currentEditedCharSet
                    Exit For
                Next
                GlobalVariables.editedCharsIndex.Add(
                    {GlobalVariables.currentEditedCharSet.Guid, GlobalVariables.editedCharSets.Count})
                GlobalVariables.editedCharSets.Add(GlobalVariables.currentEditedCharSet)
                If GlobalVariables.armoryMode = False And GlobalVariables.templateMode = False Then
                    NewProcessStatus()
                    Userwait.Show()
                    Dim updateHandler As New UpdateCharacterHandler
                    updateHandler.UpdateCharacter(GlobalVariables.currentViewedCharSet,
                                                  GlobalVariables.currentEditedCharSet)
                    GlobalVariables.currentViewedCharSet = GlobalVariables.currentEditedCharSet
                    SetCharacterSet(GlobalVariables.currentViewedCharSetId, GlobalVariables.currentViewedCharSet,
                                    _currentAccount)
                    GlobalVariables.currentEditedCharSet = Nothing
                    Userwait.Close()
                ElseIf GlobalVariables.armoryMode = True Then
                    GlobalVariables.currentViewedCharSet = GlobalVariables.currentEditedCharSet
                    SetCharacterSet(GlobalVariables.currentViewedCharSetId, GlobalVariables.currentViewedCharSet,
                                    _currentAccount)
                    GlobalVariables.currentEditedCharSet = Nothing
                End If
                LiveView.LiveViewInstance.UpdateCharacter(GlobalVariables.currentViewedCharSet)
            End If
        End Sub

        Private Sub Button4_Click(sender As Object, e As EventArgs) Handles spellsskills_bt.Click
            NewProcessStatus()
            Dim mywindow As New SpellSkillInterface
            For Each currentForm As Form In Application.OpenForms
                If currentForm.Name = "SpellSkillInterface" Then
                    mywindow = DirectCast(currentForm, SpellSkillInterface)
                End If
            Next
            If Not mywindow Is Nothing Then
                mywindow.Close()
            End If
            Dim mspellskillInterface As New SpellSkillInterface
            Userwait.Show()
            Application.DoEvents()
            mspellskillInterface.Show()
            mspellskillInterface.PrepareInterface(_tmpSetId)
        End Sub

        Private Sub CharacterOverview_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            AddHandler highlighter2.Click, AddressOf highlighter2_Click
        End Sub

        Private Sub GemClick(sender As Object, e As EventArgs) _
            Handles slot_9_gem3_pic.Click, slot_9_gem2_pic.Click, slot_9_gem1_pic.Click, slot_8_gem3_pic.Click,
                    slot_8_gem2_pic.Click, slot_8_gem1_pic.Click, slot_7_gem3_pic.Click, slot_7_gem2_pic.Click,
                    slot_7_gem1_pic.Click, slot_6_gem3_pic.Click, slot_6_gem2_pic.Click, slot_6_gem1_pic.Click,
                    slot_5_gem3_pic.Click, slot_5_gem2_pic.Click, slot_5_gem1_pic.Click, slot_4_gem3_pic.Click,
                    slot_4_gem2_pic.Click, slot_4_gem1_pic.Click, slot_3_gem3_pic.Click, slot_3_gem2_pic.Click,
                    slot_3_gem1_pic.Click, slot_2_gem3_pic.Click, slot_2_gem2_pic.Click, slot_2_gem1_pic.Click,
                    slot_18_gem3_pic.Click, slot_18_gem2_pic.Click, slot_18_gem1_pic.Click, slot_14_gem3_pic.Click,
                    slot_14_gem2_pic.Click, slot_14_gem1_pic.Click, slot_13_gem3_pic.Click, slot_13_gem2_pic.Click,
                    slot_13_gem1_pic.Click, slot_12_gem3_pic.Click, slot_12_gem2_pic.Click, slot_12_gem1_pic.Click,
                    slot_11_gem3_pic.Click, slot_11_gem2_pic.Click, slot_11_gem1_pic.Click, slot_10_gem3_pic.Click,
                    slot_10_gem2_pic.Click, slot_10_gem1_pic.Click, slot_1_gem3_pic.Click, slot_1_gem2_pic.Click,
                    slot_1_gem1_pic.Click, slot_0_gem3_pic.Click, slot_0_gem2_pic.Click, slot_0_gem1_pic.Click,
                    slot_17_gem3_pic.Click, slot_17_gem2_pic.Click, slot_17_gem1_pic.Click, slot_16_gem3_pic.Click,
                    slot_16_gem2_pic.Click, slot_16_gem1_pic.Click, slot_15_gem3_pic.Click, slot_15_gem2_pic.Click,
                    slot_15_gem1_pic.Click
            Dim myPic As PictureBox = CType(sender, PictureBox)
            Dim itm As Item = CType(myPic.Tag, Item)
            Dim allowAdding As Boolean = False
            If itm IsNot Nothing Then
                Select Case True
                    Case myPic.Name.Contains("gem1")
                        If itm.Socket1Id = Nothing Then
                            '// Empty socket: Allow adding
                            allowAdding = True
                        Else
                            allowAdding = False
                        End If
                    Case myPic.Name.Contains("gem2")
                        If itm.Socket2Id = Nothing Then
                            '// Empty socket: Allow adding
                            allowAdding = True
                        Else
                            allowAdding = False
                        End If
                    Case myPic.Name.Contains("gem3")
                        If itm.Socket3Id = Nothing Then
                            '// Empty socket: Allow adding
                            allowAdding = True
                        Else
                            allowAdding = False
                        End If
                End Select
            End If
            If allowAdding = True Then
                Dim retnvalue As Integer = TryInt(InputBox(MSG_ENTERGEMID, MSG_ADDGEM, "0"))
                If Not retnvalue = 0 Then
                    If GlobalVariables.currentEditedCharSet Is Nothing Then _
                        GlobalVariables.currentEditedCharSet =
                            DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet)
                    Dim client As New WebClient
                    client.CheckProxy()
                    Dim effectId As Integer = GetEffectIdByGemId(retnvalue)
                    If effectId = Nothing Or effectId = 0 Then
                        MsgBox(MSG_INVALIDGEMID, MsgBoxStyle.Critical, MSG_ERROR)
                        Exit Sub
                    Else
                        Select Case True
                            Case myPic.Name.Contains("gem1")
                                itm.Socket1Effectid = effectId
                                itm.Socket1Id = retnvalue
                                itm.Socket1Name = GetItemNameByItemId(retnvalue, MySettings.Default.language)
                                itm.Socket1Pic = GetItemIconByItemId(retnvalue, GlobalVariables.GlobalWebClient)
                                myPic.Image = itm.Socket1Pic
                            Case myPic.Name.Contains("gem2")
                                itm.Socket2Effectid = effectId
                                itm.Socket2Id = retnvalue
                                itm.Socket2Name = GetItemNameByItemId(retnvalue, MySettings.Default.language)
                                itm.Socket2Pic = GetItemIconByItemId(retnvalue, GlobalVariables.GlobalWebClient)
                                myPic.Image = itm.Socket2Pic
                            Case myPic.Name.Contains("gem3")
                                itm.Socket2Effectid = effectId
                                itm.Socket3Effectid = effectId
                                itm.Socket3Id = retnvalue
                                itm.Socket3Name = GetItemNameByItemId(retnvalue, MySettings.Default.language)
                                itm.Socket3Pic = GetItemIconByItemId(retnvalue, GlobalVariables.GlobalWebClient)
                                myPic.Image = itm.Socket3Pic
                        End Select
                        myPic.Tag = itm
                        myPic.Refresh()
                        SetCharacterArmorItem(GlobalVariables.currentEditedCharSet, itm)
                        MsgBox(MSG_GEMADDED, MsgBoxStyle.Information, MSG_ADDGEM)
                    End If
                End If
            Else
                Dim newPoint As New Point
                newPoint.X = myPic.Location.X + InventoryPanel.Location.X + 21
                newPoint.Y = myPic.Location.Y + InventoryPanel.Location.Y
                changepanel.Location = newPoint
                newPoint.X = 4000
                newPoint.Y = 4000
                classpanel.Location = newPoint
                racepanel.Location = newPoint
                addpanel.Location = newPoint
                PictureBox2.Visible = True
                If Not _tempSender Is Nothing Then
                    TryCast(_tempSender, Control).Visible = True
                End If
                _tempSender = sender
                If TypeOf (sender) Is Label Then myPic.Visible = False
                Select Case True
                    Case myPic.Name.Contains("gem1")
                        TextBox1.Text = itm.Socket1Id.ToString()
                    Case myPic.Name.Contains("gem2")
                        TextBox1.Text = itm.Socket2Id.ToString()
                    Case myPic.Name.Contains("gem3")
                        TextBox1.Text = itm.Socket2Id.ToString()
                End Select
                _tempValue = TextBox1.Text
            End If
        End Sub

        Private Sub BagOpen(sender As Object, e As EventArgs) _
            Handles bag5Pic.Click, bag4Pic.Click, bag3Pic.Click, bag2Pic.Click, bag1Pic.Click
            _inventoryControlLst = New List(Of Control)()
            Dim bag As Item = CType(TryCast(sender, PictureBox).Tag, Item)
            Dim reduceVal As UInteger = 0
            If TryCast(sender, PictureBox).Name = "bag1Pic" Then reduceVal = 23
            If bag Is Nothing Then Exit Sub
            If bag.Id = 0 AndAlso bag.Slot > 0 Then Exit Sub
            _currentBag = bag
            InventoryLayout.Controls.Clear()
            For z = 0 To bag.SlotCount - 1
                Dim itm As New Item
                itm.Slot = CInt(z + reduceVal)
                itm.Bagguid = bag.Guid
                itm.Bag = bag.Id
                Dim newItmPanel As New ItemPanel
                newItmPanel.Size = referenceItmPanel.Size
                newItmPanel.Margin = referenceItmPanel.Margin
                newItmPanel.Name = "slot_" & (z + reduceVal).ToString() & "_panel"
                Dim subItmPic As New PictureBox
                subItmPic.Cursor = Cursors.Hand
                subItmPic.Size = referenceItmPic.Size
                newItmPanel.Controls.Add(subItmPic)
                subItmPic.Location = referenceItmPic.Location
                subItmPic.BackgroundImageLayout = ImageLayout.Stretch
                subItmPic.BackgroundImage = referenceItmPic.BackgroundImage
                newItmPanel.BackColor = referenceItmPanel.BackColor
                newItmPanel.Tag = itm
                subItmPic.Tag = itm
                newItmPanel.SetDoubleBuffered()
                Dim subItmRemovePic As New PictureBox
                subItmRemovePic.Name = "slot_" & (z + reduceVal).ToString() & "_remove"
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
                InventoryLayout.Controls.Add(newItmPanel)
                subItmRemovePic.BringToFront()
                Dim subCountLabel As New Label
                subCountLabel.Text = ""
                subCountLabel.Name = "slot_" & (z + reduceVal).ToString() & "_count"
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
                InventoryLayout.Update()
                AddHandler subCountLabel.Click, AddressOf ChangeCount
                AddHandler newItmPanel.MouseEnter, AddressOf InventItem_MouseEnter
                AddHandler newItmPanel.MouseLeave, AddressOf InventItem_MouseLeave
                AddHandler subItmRemovePic.MouseClick, AddressOf removeinventbox_Click
                AddHandler subItmRemovePic.MouseEnter, AddressOf removeinventbox_MouseEnter
                AddHandler subItmRemovePic.MouseLeave, AddressOf removeinventbox_MouseLeave
                Application.DoEvents()
            Next z
            For Each ctrl As Control In InventoryLayout.Controls
                _inventoryControlLst.Add(ctrl)
            Next
            For Each itm As Item In bag.BagItems
                SetInventorySlot(itm, itm.Slot)
            Next
            GroupBox2.Size = New Size(GroupBox2.Size.Width, 122 + InventoryLayout.Size.Height - 13)
        End Sub

        Private Sub ChangeCount(sender As Object, e As EventArgs)
            Dim locLabel As Label = CType(sender, Label)
            If locLabel.Visible = True Then
                Dim result As String = InputBox(MSG_ENTERITEMCOUNT, MSG_ITEMCOUNTCHANGE, locLabel.Text)
                If Not result = "" Then
                    Dim intResult As Integer = TryInt(result)
                    If intResult <> 0 Then
                        Dim itm As Item = CType(locLabel.Tag, Item)
                        Dim maxStackSize As Integer = GetItemMaxStackByItemId(itm.Id)
                        If intResult > maxStackSize Then
                            MsgBox(MSG_STACKLIMITREACHED & " " & maxStackSize.ToString(),
                                   MsgBoxStyle.Critical, MSG_INVALIDENTRY)
                            Exit Sub
                        End If
                        itm.Count = intResult
                        locLabel.Text = CStr(itm.Count)
                        If GlobalVariables.currentEditedCharSet Is Nothing Then _
                            GlobalVariables.currentEditedCharSet =
                                DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet)
                        If itm.Bagguid = 0 Then
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

        Private Sub BagItem_MouseEnter(sender As Object, e As EventArgs)
            If Not _loadComplete = False Then
                For i = _visibleActionControls.Count - 1 To 0 Step - 1
                    _visibleActionControls(i).Visible = False
                    _visibleActionControls.Remove(_visibleActionControls(i))
                Next
                Dim parentPanel As ItemPanel = CType(sender, ItemPanel)
                Dim itm As Item = CType(parentPanel.Tag, Item)
                Dim removePic As PictureBox =
                        CType(parentPanel.Controls.Find("bag_" & itm.Slot.ToString() & "_remove", True)(0), PictureBox)
                If Not removePic Is Nothing Then
                    _visibleActionControls.Add(removePic)
                    _lastRemovePic = removePic
                    removePic.Visible = True
                End If
            End If
        End Sub

        Private Sub BagItem_MouseLeave(sender As Object, e As EventArgs)
            If _lastRemovePic IsNot Nothing Then
                _visibleActionControls.Remove(_lastRemovePic)
                _lastRemovePic.Visible = False
                Application.DoEvents()
            End If
        End Sub

        Private Sub InventItem_MouseEnter(sender As Object, e As EventArgs)
            If Not _loadComplete = False Then
                For i = _visibleActionControls.Count - 1 To 0 Step - 1
                    _visibleActionControls(i).Visible = False
                    _visibleActionControls.Remove(_visibleActionControls(i))
                Next
                Dim parentPanel As ItemPanel = CType(sender, ItemPanel)
                Dim removePic As PictureBox = CType(parentPanel.Controls(1), PictureBox)
                If Not removePic Is Nothing Then
                    _visibleActionControls.Add(removePic)
                    _lastRemovePic = removePic
                    removePic.Visible = True
                End If
            End If
        End Sub

        Private Sub InventItem_MouseLeave(sender As Object, e As EventArgs)
            If _lastRemovePic IsNot Nothing Then
                _visibleActionControls.Remove(_lastRemovePic)
                _lastRemovePic.Visible = False
                Application.DoEvents()
            End If
        End Sub

        Private Sub SetInventorySlot(ByVal itm As Item, ByVal slot As Integer)
            For Each itmctrl As ItemPanel In _
                From itmctrl1 As Object In InventoryLayout.Controls
                    Where TryCast(itmctrl1, ItemPanel).Name.Contains("_" & slot.ToString() & "_")
                itmctrl.BackColor = GetItemQualityColor(GetItemQualityByItemId(itm.Id))
                itmctrl.Tag = itm
                If itm.Count > 1 OrElse itm.Count = 1 AndAlso GetItemMaxStackByItemId(itm.Id) > 1 Then
                    Dim countLabel As Label = CType(itmctrl.Controls.Find("slot_" & slot.ToString() & "_count", True)(0),
                                                    Label)
                    countLabel.Text = itm.Count.ToString()
                    countLabel.Tag = itm
                    countLabel.Visible = True
                End If
                InfoToolTip.SetToolTip(itmctrl, itm.Name)
                For Each itmPicCtrl As Control In itmctrl.Controls
                    If Not itmPicCtrl.Name Is Nothing Then
                        If itmPicCtrl.Name.EndsWith("_remove") Then
                            Dim itmPicBox As PictureBox = CType(itmPicCtrl, PictureBox)
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
                        Dim itmPicBox As PictureBox = CType(itmPicCtrl, PictureBox)
                        itmPicBox.BackgroundImage = itm.Image
                        itmPicBox.Tag = itm
                        InfoToolTip.SetToolTip(itmPicBox, itm.Name)
                    End If
                Next
                InventoryLayout.Update()
                Application.DoEvents()
            Next
        End Sub

        Private Sub reset_bt_Click(sender As Object, e As EventArgs) Handles reset_bt.Click
            For i = Application.OpenForms.Count - 1 To 0 Step - 1
                Dim openForm As Form = Application.OpenForms(i)
                Select Case True
                    Case TypeOf openForm Is GlyphsInterface, TypeOf openForm Is AchievementsInterface,
                        TypeOf openForm Is QuestsInterface, TypeOf openForm Is ReputationInterface,
                        TypeOf openForm Is QuestsInterface, TypeOf openForm Is ReputationInterface,
                        TypeOf openForm Is SpellSkillInterface, TypeOf openForm Is ProfessionsInterface
                        openForm.Close()
                End Select
            Next i
            Hide()
            NewProcessStatus()
            Userwait.Show()
            Dim newOverview As New CharacterOverview
            GlobalVariables.currentEditedCharSet = Nothing
            newOverview.prepare_interface(_currentAccount, GlobalVariables.currentViewedCharSetId)
            Userwait.Close()
            newOverview.Show()
            Close()
        End Sub

        Private Sub bank_bt_Click(sender As Object, e As EventArgs) Handles bank_bt.Click
            Dim mywindow As New BankInterface
            For Each currentForm As Form In Application.OpenForms
                If currentForm.Name = "BankInterface" Then
                    mywindow = DirectCast(currentForm, BankInterface)
                End If
            Next
            If Not mywindow Is Nothing Then
                mywindow.Close()
            End If
            Dim bankInt As New BankInterface
            bankInt.Visible = False
            bankInt.Show()
            bankInt.PrepareBankInterface(GlobalVariables.currentViewedCharSetId)
        End Sub

        Private Sub professions_bt_Click(sender As Object, e As EventArgs) Handles professions_bt.Click
            NewProcessStatus()
            Dim mywindow As New ProfessionsInterface
            For Each currentForm As Form In Application.OpenForms
                If currentForm.Name = "ProfessionsInterface" Then
                    mywindow = DirectCast(currentForm, ProfessionsInterface)
                End If
            Next
            If Not mywindow Is Nothing Then
                mywindow.Close()
            End If
            Dim profinterface As New ProfessionsInterface
            Userwait.Show()
            Application.DoEvents()
            profinterface.Show()
            profinterface.PrepareInterface(_tmpSetId)
            Userwait.Close()
        End Sub

        Private Sub removeinventbox_Click(sender As Object, e As EventArgs)
            Dim locPanel As ItemPanel = TryCast(CType(TryCast(sender, PictureBox).Tag, Object())(0), ItemPanel)
            Dim locPic As PictureBox = TryCast(CType(TryCast(sender, PictureBox).Tag, Object())(1), PictureBox)
            Dim oldItm As Item = CType(locPic.Tag, Item)
            If oldItm.Id = 0 Then
                Dim result As String = InputBox(MSG_ENTERITEMID, MSG_ADDITEM, "0")
                If result.Length = 0 Then
                    TryCast(sender, PictureBox).Visible = False
                Else
                    Dim intResult As Integer = TryInt(result)
                    If intResult <> 0 Then
                        Dim checkName As String = GetItemNameByItemId(intResult, MySettings.Default.language)
                        If Not checkName = "Not found" Then
                            If GlobalVariables.currentEditedCharSet Is Nothing Then _
                                GlobalVariables.currentEditedCharSet =
                                    DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet)
                            Dim replaceItm As New Item()
                            replaceItm.Slot = oldItm.Slot
                            replaceItm.Id = intResult
                            replaceItm.Image = GetItemIconByItemId(replaceItm.Id, GlobalVariables.GlobalWebClient)
                            replaceItm.Name = checkName
                            replaceItm.Rarity = CType(GetItemQualityByItemId(replaceItm.Id), Item.RarityType)
                            replaceItm.Bag = oldItm.Bag
                            replaceItm.Bagguid = oldItm.Bagguid
                            Dim newGuid As Integer = 1
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
                            If replaceItm.Bagguid = 0 Then
                                GlobalVariables.currentEditedCharSet.InventoryZeroItems.Add(replaceItm)
                            Else
                                If _currentBag.AddedBag = False Then
                                    GlobalVariables.currentEditedCharSet.InventoryItems.Add(replaceItm)
                                End If
                            End If
                            _currentBag.BagItems.Add(replaceItm)
                            If GetItemMaxStackByItemId(replaceItm.Id) > 1 Then
                                Dim countLabel As Label =
                                        CType(
                                            locPanel.Controls.Find("slot_" & replaceItm.Slot.ToString() & "_count", True)(
                                                0),
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
                            MsgBox(MSG_INVALIDITEMID, MsgBoxStyle.Critical, MSG_ERROR)
                        End If
                    Else
                        MsgBox(MSG_INVALIDITEMID, MsgBoxStyle.Critical, MSG_ERROR)
                    End If
                    TryCast(sender, PictureBox).Visible = False
                End If
            Else
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
                    _currentBag.BagItems.Remove(_currentBag.BagItems.Find(Function(item) item.Slot = replaceItm.Slot))
                    Dim countLabel As Label =
                            CType(locPanel.Controls.Find("slot_" & replaceItm.Slot.ToString() & "_count", True)(0),
                                  Label)
                    countLabel.Text = ""
                    countLabel.Visible = False
                    countLabel.Tag = replaceItm
                    If oldItm.Bagguid = 0 Then
                        GlobalVariables.currentEditedCharSet.InventoryZeroItems.RemoveAt(
                            GlobalVariables.currentEditedCharSet.InventoryZeroItems.FindIndex(
                                Function(item) item.Slot = oldItm.Slot))
                    Else
                        If _currentBag.AddedBag = False Then
                            GlobalVariables.currentEditedCharSet.InventoryItems.RemoveAt(
                                GlobalVariables.currentEditedCharSet.InventoryItems.FindIndex(
                                    Function(item) item.Slot = oldItm.Slot AndAlso item.Bagguid = oldItm.Bagguid))
                        End If
                    End If

                    TryCast(sender, PictureBox).BackgroundImage = My.Resources.add_
                End If
                TryCast(sender, PictureBox).Visible = False
            End If
        End Sub

        Private Sub removeinventboxBag_Click(sender As Object, e As EventArgs)
            Dim locPanel As ItemPanel = TryCast(CType(TryCast(sender, PictureBox).Tag, Object())(0), ItemPanel)
            Dim locPic As PictureBox = TryCast(CType(TryCast(sender, PictureBox).Tag, Object())(1), PictureBox)
            Dim oldItm As Item = CType(locPanel.Tag, Item)
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
                            Dim newGuid As Integer = 1
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
                    BagOpen(bag1Pic, New EventArgs())
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
            If Not _loadComplete = False Then
                If Not TryCast(sender, PictureBox).BackgroundImage Is Nothing Then
                    _tmpImage = CType(TryCast(sender, PictureBox).BackgroundImage, Bitmap)
                    Application.DoEvents()
                    Dim picbx As PictureBox = TryCast(sender, PictureBox)
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
            End If
        End Sub

        Private Sub removeinventbox_MouseLeave(sender As Object, e As EventArgs)
            If Not _tmpImage Is Nothing And Not TryCast(sender, PictureBox).BackgroundImage Is Nothing Then
                Dim picbox As PictureBox = TryCast(sender, PictureBox)
                picbox.BackgroundImage = _tmpImage
                picbox.Refresh()
                Application.DoEvents()
            End If
        End Sub

        Private Sub refreshchar_Click(sender As Object, e As EventArgs) Handles refreshchar.Click
            Dim result = MsgBox(MSG_RELOADCHARACTER, MsgBoxStyle.YesNo, MSG_AREYOUSURE)
            If result = MsgBoxResult.Yes Then
                For i = Application.OpenForms.Count - 1 To 0 Step - 1
                    Dim openForm As Form = Application.OpenForms(i)
                    Select Case True
                        Case TypeOf openForm Is GlyphsInterface, TypeOf openForm Is AchievementsInterface,
                            TypeOf openForm Is QuestsInterface, TypeOf openForm Is ReputationInterface,
                            TypeOf openForm Is QuestsInterface, TypeOf openForm Is ReputationInterface,
                            TypeOf openForm Is SpellSkillInterface, TypeOf openForm Is ProfessionsInterface
                            openForm.Close()
                    End Select
                Next i
                Hide()
                NewProcessStatus()
                Userwait.Show()
                Dim newOverview As New CharacterOverview
                GlobalVariables.currentEditedCharSet = Nothing
                newOverview.prepare_interface(_currentAccount, GlobalVariables.currentViewedCharSetId, True)
                Close()
            End If
        End Sub

        Private Sub refreshgold_Click(sender As Object, e As EventArgs) Handles refreshgold.Click
            Dim goldInt As Integer = TryInt(gold_txtbox.Text)
            gold_txtbox.Text = goldInt.ToString()
            If GlobalVariables.currentEditedCharSet Is Nothing Then _
                GlobalVariables.currentEditedCharSet = DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet)
            GlobalVariables.currentEditedCharSet.Gold = goldInt*10000
            Focus()
        End Sub
    End Class
End Namespace