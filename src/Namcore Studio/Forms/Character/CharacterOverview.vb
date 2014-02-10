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
Imports libnc.Provider
Imports System.Net
Imports NCFramework.Framework.Core.Update

Namespace Forms.Character
    Public Class CharacterOverview
        Inherits EventTrigger

        '// Declaration
        Dim _controlLst As List(Of Control)
        Dim _pubItm As Item
        Dim _tempValue As String
        Dim _tempSender As Object
        Dim _tmpSetId As Integer
        Dim _tmpImage As Bitmap
        Dim _tmpSenderPic As Object
        Dim _currentSet As Integer
        Dim _currentAccount As Account

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

        Public Sub prepare_interface(ByVal account As Account, ByVal setId As Integer)
            LogAppend("prepare_interface call", "CharacterOverview_prepare_interface", False)
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
            If GlobalVariables.currentViewedCharSet Is Nothing Or GlobalVariables.currentViewedCharSet.Loaded = False _
                Then
                If GlobalVariables.armoryMode = False And GlobalVariables.templateMode = False Then
                    '//Load charset
                    LogAppend("Loading character from database", "CharacterOverview_prepare_interface", True)
                    Dim loadHandlerThread As New Thread(AddressOf LoadCharacter)
                    loadHandlerThread.Start(setId)
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
            Dim zeroBagItems As New List(Of Item)
            If Not DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet).InventoryZeroItems Is Nothing Then
                For Each potCharBag As Item In _
                    DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet).InventoryZeroItems
                    potCharBag.BagItems = New List(Of Item)()
                    Select Case potCharBag.Slot
                        Case 19, 20, 21, 22
                            For Each subctrl As Control In GroupBox2.Controls
                                If subctrl.Name.Contains((potCharBag.Slot - 17).ToString()) Then
                                    If subctrl.Name.ToLower.Contains("panel") Then
                                        Dim bagPanel As Panel = subctrl
                                        bagPanel.BackColor = GetItemQualityColor(GetItemQualityByItemId(potCharBag.Id))
                                        For Each potBagItem As Item In _
                                            DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet).
                                                InventoryItems
                                            If potBagItem.Bagguid = potCharBag.Guid Then
                                                If potBagItem.Name Is Nothing Then _
                                                    potBagItem.Name = GetItemNameByItemId(potBagItem.Id,
                                                                                          MySettings.Default.language)
                                                If potBagItem.Image Is Nothing Then _
                                                    potBagItem.Image = GetItemIconByDisplayId(GetDisplayIdByItemId(potBagItem.Id),
                                                                                       GlobalVariables.GlobalWebClient)
                                                potCharBag.BagItems.Add(potBagItem)
                                            End If
                                        Next
                                        potCharBag.SlotCount = GetItemSlotCountByItemId(potCharBag.Id)
                                        bagPanel.Tag = potCharBag
                                        For Each myPic As PictureBox In subctrl.Controls
                                            myPic.BackgroundImage = GetItemIconByDisplayId(GetDisplayIdByItemId(potCharBag.Id),
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
                                potCharBag.Image = GetItemIconByDisplayId(GetDisplayIdByItemId(potCharBag.Id), GlobalVariables.GlobalWebClient)
                            zeroBagItems.Add(potCharBag)
                    End Select
                Next
                For Each subctrl As Control In GroupBox2.Controls
                    If subctrl.Name.Contains("1") Then
                        If subctrl.Name.ToLower.Contains("panel") Then
                            Dim bagPanel As Panel = subctrl
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
                                Dim txt As String = LoadInfo(setId, slot, 0)
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
                                Dim txt As String = LoadInfo(setId, slot, 1)
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
                                DirectCast(itemControl, PictureBox).Image = LoadInfo(setId, slot, 2)
                                If DirectCast(itemControl, PictureBox).Image Is Nothing Then _
                                    DirectCast(itemControl, PictureBox).Image = My.Resources.empty
                                DirectCast(itemControl, PictureBox).Tag = _pubItm
                            ElseIf itemControl.Name.ToLower.Contains("gem") Then
                                Dim slot As Integer = TryInt(SplitString(itemControl.Name, "slot_", "_gem"))
                                Dim gem As Integer = TryInt(SplitString(itemControl.Name, "gem", "_pic"))
                                Dim img As Bitmap = LoadInfo(setId, slot, 2 + gem)
                                DirectCast(itemControl, PictureBox).Tag = _pubItm
                                If Not _pubItm Is Nothing Then
                                    If img Is Nothing Then
                                        DirectCast(itemControl, PictureBox).Image = My.Resources.add_
                                        DirectCast(itemControl, PictureBox).Cursor = Cursors.Hand
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
                                DirectCast(itemControl, Panel).BackColor = LoadInfo(setId, slot, 6)
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

        Private Function LoadInfo(ByVal targetSet As Integer, ByVal slot As Integer, ByVal infotype As Integer)
            LogAppend("Loading info for slot " & slot.ToString, "CharacterOverview_LoadInfo", True)
            _pubItm = New Item
            Dim itm As Item = GetCharacterArmorItem(GetCharacterSetBySetId(targetSet, _currentAccount), slot.ToString,
                                                    True)
            _pubItm = itm
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
                        itm.Image = GetItemIconByDisplayId(GetDisplayIdByItemId(itm.Id), GlobalVariables.GlobalWebClient)
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
                    If itm.Rarity = Nothing Then itm.Rarity = GetItemQualityByItemId(itm.Id)
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
            If sender.text = "" Then Exit Sub
            Dim tagItm As Item = sender.Tag
            If tagItm.EnchantmentId = 0 Then tagItm.EnchantmentId = GetSpellIdByEffectId(tagItm.EnchantmentEffectid)
            If tagItm.EnchantmentType = 2 Then
                Dim tmpItmId As Integer = GetItemIdBySpellId(tagItm.EnchantmentEffectid)
                If tmpItmId <> 0 Then tagItm.EnchantmentId = tmpItmId
            End If
            sender.Tag = tagItm
            TextBox1.Text = ""
            Try
                Dim newPoint As New Point
                newPoint.X = sender.location.X + InventoryPanel.Location.X
                newPoint.Y = sender.location.Y + InventoryPanel.Location.Y
                changepanel.Location = newPoint
                newPoint.X = 4000
                newPoint.Y = 4000
                classpanel.Location = newPoint
                racepanel.Location = newPoint
                addpanel.Location = newPoint
                PictureBox2.Visible = True
                If Not _tempSender Is Nothing Then
                    _tempSender.visible = True
                End If
                _tempSender = sender
                sender.visible = False
                If sender.name.contains("_name") Then
                    TextBox1.Text = sender.Tag.Id.ToString
                ElseIf sender.name.contains("_lbl") Then
                    TextBox1.Text = sender.text
                Else
                    Dim itm As Item = sender.tag
                    TextBox1.Text = itm.EnchantmentId.ToString
                End If
                _tempValue = TextBox1.Text
            Catch
            End Try
        End Sub

        Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
            'Change value
            Dim newPoint As New Point
            Dim senderLabel As Label = _tempSender
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
                            itemcontext.Contains("subclass Guid=""6"">") Then
                            founditem = True
                            itemname = SplitString(itemcontext, "<name><![CDATA[", "]]></name>")
                            itemname = itemname.Replace("&#x20;", " ")
                        End If
                        If founditem = foundspell = True Then
                            selectenchpanel.Location = changepanel.Location
                            spellench.Text = "Spell: " & spellname
                            spellench.Tag = spellname
                            itmench.Text = "Item: " & itemname
                            itmench.Tag = itemname
                        ElseIf founditem = True Then
                            Dim itm As Item = senderLabel.Tag
                            senderLabel.Text = itemname
                            itm.EnchantmentType = 1
                            itm.EnchantmentId = TryInt(TextBox1.Text)
                            itm.EnchantmentName = itemname
                            senderLabel.Tag = itm
                            If GlobalVariables.currentEditedCharSet Is Nothing Then _
                                GlobalVariables.currentEditedCharSet =
                                    DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet)
                            SetCharacterArmorItem(GlobalVariables.currentEditedCharSet, senderLabel.Tag)
                        ElseIf foundspell = True Then
                            Dim itm As Item = senderLabel.Tag
                            senderLabel.Text = spellname
                            itm.EnchantmentType = 0
                            itm.EnchantmentId = TryInt(TextBox1.Text)
                            itm.EnchantmentName = spellname
                            senderLabel.Tag = itm
                            If GlobalVariables.currentEditedCharSet Is Nothing Then _
                                GlobalVariables.currentEditedCharSet =
                                    DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet)
                            SetCharacterArmorItem(GlobalVariables.currentEditedCharSet, senderLabel.Tag)
                        Else

                        End If
                    Else
                        If Not GetItemInventorySlotByItemId(_tempSender.tag.Guid) = GetItemInventorySlotByItemId(Id) _
                            Then
                            MsgBox(ResourceHandler.GetUserMessage("itemclassinvalGuid"), MsgBoxStyle.Critical,
                                   ResourceHandler.GetUserMessage("Error"))
                        Else
                            Dim newitm As Item = _tempSender.tag
                            newitm.ReplaceItem(Id)
                            senderLabel.Tag = newitm
                            Dim txt As String = senderLabel.Tag.name
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
                                    If ctrl.Tag.Guid = senderLabel.Tag.Guid Then
                                        DirectCast(ctrl, PictureBox).Tag = senderLabel.Tag
                                        Select Case True
                                            Case _
                                                ctrl.Name.ToLower.EndsWith("_pic") And
                                                Not ctrl.Name.ToLower.Contains("gem")
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
                                    If ctrl.Tag.Guid = senderLabel.Tag.Guid Then
                                        If ctrl.Name.ToLower.EndsWith("color") Then
                                            DirectCast(ctrl, Panel).BackColor =
                                                GetItemQualityColor(senderLabel.Tag.rarity)
                                            DirectCast(ctrl, Panel).Tag = senderLabel.Tag
                                        End If
                                    End If
                                ElseIf TypeOf ctrl Is Label Then
                                    If ctrl.Tag Is Nothing Then Continue For
                                    If ctrl.Tag.Guid = senderLabel.Tag.Guid Then
                                        If ctrl.Name.ToLower.EndsWith("_enchant") Then
                                            DirectCast(ctrl, Label).Tag = senderLabel.Tag
                                        End If
                                    End If
                                End If

                            Next
                            If GlobalVariables.currentEditedCharSet Is Nothing Then _
                                GlobalVariables.currentEditedCharSet =
                                    DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet)
                            SetCharacterArmorItem(GlobalVariables.currentEditedCharSet, senderLabel.Tag)
                        End If
                    End If

                End If
            End If
            changepanel.Location = newPoint
            senderLabel.Visible = True
        End Sub

        Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
            'Detele item
            Dim newPoint As New Point
            Dim senderLabel As Label = _tempSender
            newPoint.X = 4000
            newPoint.Y = 4000
            Dim tempSender = TryCast(_tempSender, Label)
            If (tempSender IsNot Nothing) Then
                If Not senderLabel.Name.ToLower.EndsWith("_enchant") Then

                    Dim result = MsgBox(ResourceHandler.GetUserMessage("deleteitem"), vbYesNo,
                                        ResourceHandler.GetUserMessage("areyousure"))
                    If result = MsgBoxResult.Yes Then
                        For Each ctrl As Control In _controlLst
                            If TypeOf ctrl Is PictureBox Then
                                If ctrl.Tag Is Nothing Then Continue For
                                If ctrl.Tag.Guid = senderLabel.Tag.Guid Then
                                    DirectCast(ctrl, PictureBox).Tag = senderLabel.Tag
                                    Select Case True
                                        Case _
                                            ctrl.Name.ToLower.EndsWith("_pic") And Not ctrl.Name.ToLower.Contains("gem")
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
                                    If ctrl.Tag.Guid = senderLabel.Tag.Guid Then
                                        DirectCast(ctrl, Panel).BackColor = SystemColors.ActiveBorder
                                        DirectCast(ctrl, Panel).Tag = Nothing
                                    End If
                                End If
                            ElseIf TypeOf ctrl Is Label Then
                                If ctrl.Tag Is Nothing Then Continue For
                                If ctrl.Tag.Guid = senderLabel.Tag.Guid Then
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
                        RemoveCharacterArmorItem(GlobalVariables.currentEditedCharSet, senderLabel.Tag)
                        senderLabel.Text = Nothing
                        senderLabel.Tag = Nothing
                    End If
                Else
                    If GlobalVariables.currentEditedCharSet Is Nothing Then _
                           GlobalVariables.currentEditedCharSet =
                               DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet)
                    Dim pubItem As Item = tempSender.Tag
                    pubItem.RemoveEnchantments()
                    tempSender.Tag = pubItem
                    tempSender.Text = "+"
                    senderLabel.Cursor = Cursors.Hand
                    Dim relevantControls As Control() = _controlLst.FindAll(Function(control) control.Tag IsNot Nothing).ToArray()
                    Dim matchControls As Control() = Array.FindAll(relevantControls, Function(control) control.Tag.Guid = pubItem.Guid)
                    If Not matchControls Is Nothing Then
                        For i = 0 To matchControls.Length - 1
                            matchControls(i).Tag = pubItem
                        Next
                    End If
                End If
            End If
            changepanel.Location = newPoint
            senderLabel.Visible = True
        End Sub

        Private Sub race_lbl_Click(sender As Object, e As EventArgs) Handles race_lbl.Click
            racepanel.Location = New Point(sender.location.x + +GroupBox1.Location.X,
                                           sender.location.y + GroupBox1.Location.Y)
            Dim newpoint As New Point
            newpoint.X = 4000
            newpoint.Y = 4000
            classpanel.Location = newpoint
            changepanel.Location = newpoint
            genderpanel.Location = newpoint
            PictureBox2.Visible = False
            If Not _tempSender Is Nothing Then
                _tempSender.visible = True
            End If
            _tempSender = sender
            sender.visible = False
            racecombo.Text = sender.text
            _tempValue = racecombo.Text
        End Sub

        Private Sub class_lbl_Click(sender As Object, e As EventArgs) Handles class_lbl.Click
            classpanel.Location = New Point(sender.location.x + +GroupBox1.Location.X,
                                            sender.location.y + GroupBox1.Location.Y)
            Dim newpoint As New Point
            newpoint.X = 4000
            newpoint.Y = 4000
            racepanel.Location = newpoint
            changepanel.Location = newpoint
            genderpanel.Location = newpoint
            PictureBox2.Visible = False
            If Not _tempSender Is Nothing Then
                _tempSender.visible = True
            End If
            _tempSender = sender
            sender.visible = False
            classcombo.Text = sender.text
            _tempValue = classcombo.Text
        End Sub

        Private Sub charname_lbl_Click(sender As Object, e As EventArgs) Handles charname_lbl.Click
            changepanel.Location = sender.location
            Dim newpoint As New Point
            newpoint.X = 4000
            newpoint.Y = 4000
            classpanel.Location = newpoint
            racepanel.Location = newpoint
            genderpanel.Location = newpoint
            PictureBox2.Visible = False
            If Not _tempSender Is Nothing Then
                _tempSender.visible = True
            End If
            _tempSender = sender
            sender.visible = False
            TextBox1.Text = sender.text
            _tempValue = TextBox1.Text
        End Sub

        Private Sub level_lbl_Click(sender As Object, e As EventArgs) Handles level_lbl.Click
            changepanel.Location = New Point(sender.location.x + +GroupBox1.Location.X,
                                             sender.location.y + GroupBox1.Location.Y)
            Dim newpoint As New Point
            newpoint.X = 4000
            newpoint.Y = 4000
            classpanel.Location = newpoint
            racepanel.Location = newpoint
            genderpanel.Location = newpoint
            PictureBox2.Visible = False
            If Not _tempSender Is Nothing Then
                _tempSender.visible = True
            End If
            _tempSender = sender
            sender.visible = False
            TextBox1.Text = sender.text
            _tempValue = TextBox1.Text
        End Sub

        Private Sub gender_lbl_Click(sender As Object, e As EventArgs) Handles gender_lbl.Click
            genderpanel.Location = New Point(sender.location.x + +GroupBox1.Location.X,
                                             sender.location.y + GroupBox1.Location.Y)
            Dim newpoint As New Point
            newpoint.X = 4000
            newpoint.Y = 4000
            classpanel.Location = newpoint
            racepanel.Location = newpoint
            changepanel.Location = newpoint
            PictureBox2.Visible = False
            If Not _tempSender Is Nothing Then
                _tempSender.visible = True
            End If
            _tempSender = sender
            sender.visible = False
            gendercombo.Text = sender.text
            _tempValue = gendercombo.Text
        End Sub

        Private Sub classrefresh_Click(sender As Object, e As EventArgs) Handles classrefresh.Click
            Dim newPoint As New Point
            Dim senderLabel As Label = _tempSender
            newPoint.X = 4000
            newPoint.Y = 4000
            If Not classcombo.SelectedText = _tempValue And Not classcombo.Text = _tempValue Then
                senderLabel.Text = classcombo.SelectedText
            End If
            classpanel.Location = newPoint
            senderLabel.Visible = True
            If GlobalVariables.currentEditedCharSet Is Nothing Then _
                GlobalVariables.currentEditedCharSet = DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet)
            GlobalVariables.currentEditedCharSet.Cclass = GetClassIdByName(senderLabel.Text)
        End Sub

        Private Sub racerefresh_Click(sender As Object, e As EventArgs) Handles racerefresh.Click
            Dim newPoint As New Point
            Dim senderLabel As Label = _tempSender
            newPoint.X = 4000
            newPoint.Y = 4000
            If Not racecombo.SelectedText = _tempValue And Not racecombo.Text = _tempValue Then
                senderLabel.Text = racecombo.SelectedText
            End If
            racepanel.Location = newPoint
            senderLabel.Visible = True
            If GlobalVariables.currentEditedCharSet Is Nothing Then _
                GlobalVariables.currentEditedCharSet = DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet)
            GlobalVariables.currentEditedCharSet.Race = GetRaceIdByName(senderLabel.Text)
        End Sub

        Private Sub genderrefresh_Click(sender As Object, e As EventArgs) Handles genderrefresh.Click
            Dim newPoint As New Point
            Dim senderLabel As Label = _tempSender
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
                GlobalVariables.currentEditedCharSet.Gender = 0
            Else
                GlobalVariables.currentEditedCharSet.Gender = 1
            End If
        End Sub

        Private Sub itmench_Click(sender As Object, e As EventArgs) Handles itmench.Click
            Dim newPoint As New Point
            Dim senderLabel As Label = _tempSender
            newPoint.X = 4000
            newPoint.Y = 4000
            senderLabel.Text = itmench.Tag
            senderLabel.Tag.enchantment_type = 1
            senderLabel.Tag.enchantment_id = TextBox1.Text
            senderLabel.Tag.enchantment_name = itmench.Tag
            selectenchpanel.Location = newPoint
        End Sub

        Private Sub spellench_Click(sender As Object, e As EventArgs) Handles spellench.Click
            Dim newPoint As New Point
            Dim senderLabel As Label = _tempSender
            newPoint.X = 4000
            newPoint.Y = 4000
            senderLabel.Text = spellench.Tag
            senderLabel.Tag.enchantment_type = 0
            senderLabel.Tag.enchantment_id = TextBox1.Text
            senderLabel.Tag.enchantment_name = spellench.Tag
            selectenchpanel.Location = newPoint
        End Sub

        Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Glyphs_bt.Click
            Dim mywindow As New GlyphsInterface
            For Each currentForm As Form In _
                From currentForm1 As Form In Application.OpenForms Where currentForm1.Name = "GlyphsInterface"
                mywindow = DirectCast(currentForm, GlyphsInterface)
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
            If Not sender.tag Is Nothing Then
                Try
                    Dim itemId As Integer = sender.tag.Id
                    Process.Start("http://wowhead.com/item=" & itemId.ToString())
                Catch ex As Exception

                End Try
            End If
            If Not _tempSender Is Nothing Then
                _tempSender.visible = True
            End If
            changepanel.Location = New Point(4000, 4000)
            racepanel.Location = New Point(4000, 4000)
            classpanel.Location = New Point(4000, 4000)
            addpanel.Location = New Point(4000, 4000)
            genderpanel.Location = New Point(4000, 4000)
            For Each ctrl As Label In _
                From ctrl1 In _controlLst.OfType(Of Label)()
                    Where ctrl1.Name.StartsWith(sender.name.replace("_pic", "")) And ctrl1.Name.EndsWith("_name")
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
                If Not sender.image Is Nothing Then
                    _tmpImage = sender.image
                    Application.DoEvents()
                    Dim picbx As PictureBox = sender
                    Dim g As Graphics
                    Dim img As Bitmap
                    Dim r As Rectangle
                    img = picbx.Image
                    sender.Image = New Bitmap(picbx.Width, picbx.Height, PixelFormat.Format32bppArgb)
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
            If Not _tmpImage Is Nothing And Not sender.image Is Nothing Then
                Dim picbox As PictureBox = sender
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
                _tempSender.visible = True
            End If
            TextBox1.Text = ""
            TextBox2.Text = ""
        End Sub

        Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
            'Add item
            Dim senderPic As PictureBox = _tmpSenderPic
            If Not TextBox2.Text = "" Then
                Dim meSlot As String = _tempSender.name
                meSlot = meSlot.Replace("slot_", "")
                meSlot = meSlot.Replace("_name", "")
                If Not GetItemInventorySlotByItemId(TryInt(TextBox2.Text)) = TryInt(meSlot) Then

                    MsgBox(ResourceHandler.GetUserMessage("itemclassinvalid"), MsgBoxStyle.Critical,
                           ResourceHandler.GetUserMessage("Error"))
                    Exit Sub
                Else
                    Dim itm As New Item
                    itm.Id = TryInt(TextBox2.Text)
                    Dim x As DateTime = Date.Now
                    itm.Guid = x.ToTimeStamp()
                    itm.Name = GetItemNameByItemId(itm.Id, MySettings.Default.language)
                    itm.Image = GetItemIconByDisplayId(GetDisplayIdByItemId(itm.Id), GlobalVariables.GlobalWebClient)
                    itm.Rarity = GetItemQualityByItemId(itm.Id)
                    itm.Slot = TryInt(meSlot)
                    itm.Slotname = GetItemInventorySlotByItemId(itm.Slot)
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
                        _tempSender.visible = True
                    End If
                    TextBox1.Text = ""
                    TextBox2.Text = ""
                End If
            End If
        End Sub

        Private Sub av_bt_Click(sender As Object, e As EventArgs) Handles av_bt.Click
            Dim mywindow As New AchievementsInterface
            For Each currentForm As Form In _
                From currentForm1 As Form In Application.OpenForms Where currentForm1.Name = "AchievementsInterface"
                mywindow = DirectCast(currentForm, AchievementsInterface)
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
            For Each currentForm As Form In _
                From currentForm1 As Form In Application.OpenForms Where currentForm1.Name = "ReputationInterface"
                mywindow = DirectCast(currentForm, ReputationInterface)
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
            For Each currentForm As Form In _
                From currentForm1 As Form In Application.OpenForms Where currentForm1.Name = "QuestsInterface"
                mywindow = DirectCast(currentForm, QuestsInterface)
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
            If GlobalVariables.currentEditedCharSet Is Nothing Then

            Else
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
                    Userwait.Close()
                End If
            End If
        End Sub

        Private Sub Button4_Click(sender As Object, e As EventArgs) Handles spellsskills_bt.Click
            NewProcessStatus()
            Dim mywindow As New SpellSkillInterface
            For Each currentForm As Form In _
                From currentForm1 As Form In Application.OpenForms Where currentForm1.Name = "SpellSkillInterface"
                mywindow = DirectCast(currentForm, SpellSkillInterface)
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
            Dim myPic As PictureBox = sender
            Dim itm As Item = sender.tag
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
                Dim retnvalue As Integer = TryInt(InputBox(ResourceHandler.GetUserMessage("enterGemId"),
                                                           ResourceHandler.GetUserMessage("gemAdding"), "0"))
                If Not retnvalue = 0 Then
                    If GlobalVariables.currentEditedCharSet Is Nothing Then _
                        GlobalVariables.currentEditedCharSet =
                            DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet)
                    Dim client As New WebClient
                    client.CheckProxy()
                    Dim effectId As Integer = GetEffectIdByGemId(retnvalue)
                    If effectId = Nothing Or effectId = 0 Then
                        MsgBox(ResourceHandler.GetUserMessage("invalidGemError"), MsgBoxStyle.Critical, "Error")
                        Exit Sub
                    Else
                        Select Case True
                            Case myPic.Name.Contains("gem1")
                                itm.Socket1Effectid = effectId
                                itm.Socket1Id = retnvalue
                                itm.Socket1Name = GetItemNameByItemId(retnvalue, MySettings.Default.language)
                                itm.Socket1Pic = GetItemIconByDisplayId(GetDisplayIdByItemId(retnvalue), GlobalVariables.GlobalWebClient)
                            Case myPic.Name.Contains("gem2")
                                itm.Socket2Effectid = effectId
                                itm.Socket2Id = retnvalue
                                itm.Socket2Name = GetItemNameByItemId(retnvalue, MySettings.Default.language)
                                itm.Socket2Pic = GetItemIconByDisplayId(GetDisplayIdByItemId(retnvalue), GlobalVariables.GlobalWebClient)
                            Case myPic.Name.Contains("gem3")
                                itm.Socket2Effectid = effectId
                                itm.Socket3Effectid = effectId
                                itm.Socket3Id = retnvalue
                                itm.Socket3Name = GetItemNameByItemId(retnvalue, MySettings.Default.language)
                                itm.Socket3Pic = GetItemIconByDisplayId(GetDisplayIdByItemId(retnvalue), GlobalVariables.GlobalWebClient)
                        End Select
                        sender.tag = itm
                        myPic.Image = itm.Image
                        myPic.Refresh()
                        SetCharacterArmorItem(GlobalVariables.currentEditedCharSet, itm)
                        MsgBox(ResourceHandler.GetUserMessage("gemAdded"), MsgBoxStyle.Information,
                               ResourceHandler.GetUserMessage("gemAdding"))
                    End If
                End If
            End If
        End Sub

        Private Sub BagOpen(sender As Object, e As EventArgs) _
            Handles bag5Pic.Click, bag4Pic.Click, bag3Pic.Click, bag2Pic.Click, bag1Pic.Click
            Dim bag As Item = sender.tag
            If bag Is Nothing Then Exit Sub
            InventoryLayout.Controls.Clear()
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
                InventoryLayout.Controls.Add(newItmPanel)
                InfoToolTip.SetToolTip(newItmPanel, "Empty")
                InfoToolTip.SetToolTip(subItmPic, "Empty")
                InventoryLayout.Update()
                Application.DoEvents()
            Next z
            For Each itm As Item In bag.BagItems
                Dim reduceVal As UInteger = 0
                If sender.name = "bag1Pic" Then reduceVal = 23
                SetInventorySlot(itm, itm.Slot - reduceVal)
            Next
            GroupBox2.Size = New Size(GroupBox2.Size.Width, 122 + InventoryLayout.Size.Height - 13)
        End Sub

        Private Sub SetInventorySlot(ByVal itm As Item, ByVal slot As Integer)
            For Each itmctrl As Panel In InventoryLayout.Controls
                If itmctrl.Name.Contains("_" & slot.ToString() & "_") Then
                    itmctrl.BackColor = GetItemQualityColor(GetItemQualityByItemId(itm.Id))
                    itmctrl.Tag = itm
                    InfoToolTip.SetToolTip(itmctrl, itm.Name)
                    For Each itmPicBox As PictureBox In itmctrl.Controls
                        itmPicBox.BackgroundImage = itm.Image
                        itmPicBox.Tag = itm
                        InfoToolTip.SetToolTip(itmPicBox, itm.Name)
                    Next
                    InventoryLayout.Update()
                    Application.DoEvents()
                End If
            Next
        End Sub

        Private Sub reset_bt_Click(sender As Object, e As EventArgs) Handles reset_bt.Click
            GlyphsInterface.Close()
            AchievementsInterface.Close()
            QuestsInterface.Close()
            ReputationInterface.Close()
            SpellSkillInterface.Close()
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
            For Each currentForm As Form In _
                From currentForm1 As Form In Application.OpenForms Where currentForm1.Name = "BankInterface"
                mywindow = DirectCast(currentForm, BankInterface)
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
            For Each currentForm As Form In _
                From currentForm1 As Form In Application.OpenForms Where currentForm1.Name = "ProfessionsInterface"
                mywindow = DirectCast(currentForm, ProfessionsInterface)
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
    End Class
End Namespace