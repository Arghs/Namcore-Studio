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
'*      /Filename:      AchievementsInterface
'*      /Description:   Provides an interface to display character achievement information
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.My
Imports NamCore_Studio.Modules
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Modules
Imports NamCore_Studio.Modules.Interface
Imports NCFramework.Framework.Extension
Imports NamCore_Studio.Forms.Extension
Imports System.Threading
Imports libnc.Provider
Imports System.Net
Imports System.Resources
Imports System.Reflection

Namespace Forms.Character
    Public Class AchievementsInterface
        Inherits EventTrigger

        '// Declaration
        Dim _currentCat As Integer = Nothing
        Dim _preCatControlLst As List(Of Control) = Nothing
        Dim _goon As Boolean = False
        Dim _tmpSender As Object
        Dim _mEvent As EventArgs
        Dim _state As String
        Dim _controlsToAdd As List(Of Control)

        Shared _doneAvIds As List(Of Integer)
        Shared _correctIds As List(Of Integer)
        Shared _colorTicker As Integer
        Shared _completed As Boolean

        Private ReadOnly _context As SynchronizationContext = SynchronizationContext.Current
        Public Event AvCompleted As EventHandler(Of CompletedEventArgs)
        Public Event FilterCompleted As EventHandler(Of CompletedEventArgs)
        Private WithEvents _mHandler As New TrdQueueHandler

        Delegate Sub AddControlDelegate()

        Delegate Sub UpdateControlDelegate(ctrl As Control)
        '// Declaration

        Private Sub Achievements_interface_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            AddHandler highlighter2.Click, AddressOf highlighter2_Click
            GlobalVariables.trdRunning = 0
            GlobalVariables.abortMe = False
            Dim controlLst As List(Of Control)
            controlLst = FindAllChildren()
            For Each itemControl As Control In controlLst
                '// Set all controls double buffered to prevent flickering
                itemControl.SetDoubleBuffered()
            Next
            waitpanel.Location = New Point(367, 219)
            subcat_combo.Enabled = False
            If GlobalVariables.currentEditedCharSet Is Nothing Then
                GlobalVariables.currentEditedCharSet = DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet)
            End If
        End Sub

        Protected Overridable Sub OnCompleted(ByVal e As CompletedEventArgs)
            RaiseEvent AvCompleted(Me, e)
        End Sub

        Protected Overridable Sub OnFilterCompleted(ByVal e As CompletedEventArgs)
            RaiseEvent FilterCompleted(Me, e)
        End Sub

        Public Sub catbt_click(sender As Object, e As EventArgs) _
            Handles cat_id_97_bt.Click, cat_id_96_bt.Click, cat_id_95_bt.Click, cat_id_92_bt.Click, cat_id_81_bt.Click,
                    cat_id_201_bt.Click, cat_id_169_bt.Click, cat_id_168_bt.Click, cat_id_155_bt.Click,
                    cat_id_15219_bt.Click, cat_id_15165_bt.Click
            '// Category selected
            LogAppend("catbt_click event raised for sender: " & TryCast(sender, Button).Name,
                      "AchievementsInterface_catbt_click")
            LogAppend(
                "Additional info - trdRunning: " & GlobalVariables.trdRunning.ToString() & " - abortMe: " &
                GlobalVariables.abortMe.ToString(), "AchievementsInterface_catbt_click")
            subcat_combo.Enabled = False
            subcat_combo.Text = ""
            Try
                _preCatControlLst = Nothing
                waitLabel.Text = ResourceHandler.GetUserMessage("loadingAv")
                waitpanel.Location = New Point(367, 219)
                _state = "catbt"
                _tmpSender = sender
                _mEvent = e
                _goon = False
                Application.DoEvents()
                If GlobalVariables.trdRunning > 0 Then
                    '// Currently loading achievements
                    '// -> Cancel current operation
                    callbacktimer.Stop()
                    GlobalVariables.abortMe = True
                    '// Callbacktimer will call this sub again when abortion successfull
                    callbacktimer.Enabled = True
                    callbacktimer.Start()
                    Exit Sub
                Else
                    _goon = True
                End If
                If _goon = True Then
                    '// No more threads running
                    _currentCat = TryInt(SplitString(TryCast(sender, Button).Name, "cat_id_", "_bt"))
                    subcat_combo.Items.Clear()
                    Application.DoEvents()
                    Dim tmpCatids As Integer()
                    '// Initializing matching subcategory ids for selected category
                    Select Case _currentCat
                        Case 96
                            '//Quests
                            tmpCatids = {14861, 15081, 14862, 14863, 15070, 15110}
                        Case 97
                            '//Exploration
                            tmpCatids = {14777, 14778, 14779, 14780, 15069, 15113}
                        Case 95
                            '//PvP
                            tmpCatids =
                                {14804, 14802, 14803, 14801, 15003, 14881, 15073, 15074, 15162, 15163, 15218, 14901,
                                 15075,
                                 15092, 165}
                        Case 168
                            '//Dungeons & Raids
                            tmpCatids = {14808, 14805, 14806, 14922, 15067, 15068, 15106, 15107, 15115}
                        Case 169
                            '//Professions
                            tmpCatids = {170, 171, 172, 15071}
                        Case 201
                            '//Reputation
                            tmpCatids = {14864, 14865, 14866, 15072, 15114}
                        Case 155
                            '//World Events
                            tmpCatids = {160, 187, 159, 163, 161, 162, 158, 14981, 156, 14941, 15101, 15202}
                        Case 15117
                            '//Pet Battles
                            tmpCatids = {15118, 15119, 15120}
                        Case Else
                            '// Invalid
                            tmpCatids = {}
                    End Select
                    Dim catCollection As New List(Of AvSubcategoy)
                    For i = 0 To tmpCatids.Length - 1
                        '// Loading localized names for subcategories
                        Try
                            catCollection.Add(
                                New AvSubcategoy _
                                                 With { _
                                                 .Text =
                                                 ResourceHandler.GetUserMessage("subcat" & tmpCatids(i).ToString),
                                                 .Id = tmpCatids(i)})
                        Catch ex As Exception
                            LogAppend("Exception while adding achievement subcategory item: " & ex.ToString,
                                      "Achievements_interface_catbt_click", False, True)
                        End Try
                    Next
                    '// Adding category names to combobox
                    subcat_combo.Items.Add(
                        New AvSubcategoy With {.Text = ResourceHandler.GetUserMessage("subcat0"), .Id = 0})
                    Try
                        For Each cat As AvSubcategoy In catCollection
                            subcat_combo.Items.Add(cat)
                        Next
                    Catch ex As Exception
                        subcat_combo.Items.Clear()
                    End Try
                    subcat_combo.Text = "Pick category"
                    _colorTicker = 0
                    _completed = False
                    '// Clean up AVLayoutPanel
                    Do
                        For Each subctrl As Control In AVLayoutPanel.Controls
                            AVLayoutPanel.Controls.Remove(subctrl)
                            subctrl.Dispose()
                        Next
                        Application.DoEvents()
                    Loop Until AVLayoutPanel.Controls.Count = 0
                    _correctIds = New List(Of Integer)()
                    _doneAvIds = New List(Of Integer)()
                    '// Setting up correct ids
                    _correctIds = GetAvIdListByMainCat(TryInt(SplitString(TryCast(sender, Button).Name, "cat_id_", "_bt")))
                    '// _correctIds will not contain every achievement id that matches the selected category
                    GlobalVariables.abortMe = False
                    '// Starting 2 operating threads
                    _mHandler.doOperate_av(sender, 1)
                    _mHandler.doOperate_av(sender, 2)
                End If
            Catch ex As Exception
                LogAppend("Something went wrong: " & ex.ToString(), "AchievementsInterface_catbt_click", False, True)
                GlobalVariables.trdRunning = 0
                GlobalVariables.abortMe = False
            End Try
        End Sub

        Private Sub deleteAv_click(sender As Object, e As EventArgs)
            '// Delete character achievement
            LogAppend("Deleting achievement. Sender: " & TryCast(sender, PictureBox).Name, "AchievementsInterface_deleteAv_click", False, False)
            If GlobalVariables.trdRunning > 0 Then
                '// Currently loading achievements -> Exit
                Exit Sub
            End If
            '// Get the achievement object which is referenced in sender's tag
            Dim charAv As Achievement = CType(TryCast(sender, PictureBox).Tag, Achievement)
            Dim msg As String = ResourceHandler.GetUserMessage("aus_deleteav")
            msg = msg.Replace("%avid%", charAv.Id.ToString)
            Dim result = MsgBox(msg, vbYesNo, ResourceHandler.GetUserMessage("areyousure"))
            If result = MsgBoxResult.Yes Then
                Try
                    Userwait.Show()
                    '// Locating achievement and matching controls
                    For Each subctrl As Control In AVLayoutPanel.Controls
                        If CType(subctrl.Tag, Achievement).Id = charAv.Id Then
                            '// Removing control from layout panel
                            AVLayoutPanel.Controls.Remove(subctrl)
                            subctrl.Dispose()
                            For Each av As Achievement In GlobalVariables.currentEditedCharSet.Achievements
                                If av.Id = charAv.Id Then
                                    '// Removing achievement from character achievements
                                    GlobalVariables.currentEditedCharSet.Achievements.Remove(av)
                                    Exit For
                                End If
                            Next
                            Exit For
                        End If
                    Next
                Catch ex As Exception
                    LogAppend("Something went wrong: " & ex.ToString(), "AchievementsInterface_deleteAv_click", False, True)
                End Try
            End If
            Userwait.Close()
        End Sub

        Public Function ContinueOperation(ByVal sender As Object, ByVal operationCount As Integer) As String
            _controlsToAdd = New List(Of Control)()
            If operationCount = 1 Then _
                LogAppend("Loading achievements", "AchievementsInterface_ContinueOperation", True)
            GlobalVariables.trdRunning += 1
            Try
                If operationCount = 2 Then
                    Thread.Sleep(2000)
                End If
                For Each charAv As Achievement In _
                    DeepCloneHelper.DeepClone(GlobalVariables.currentEditedCharSet).Achievements
                    If _doneAvIds.Contains(charAv.Id) Then
                        '// Achievement already added
                        Continue For
                    Else
                        _doneAvIds.Add(charAv.Id)
                    End If
                    If _correctIds.Contains(charAv.Id) Then
                        LogAppend(
                            "Operating achievement " & charAv.Id.ToString() & " / operationCount is:" &
                            operationCount.ToString(),
                            "Achievements_continueOperation", False)
                        If GlobalVariables.abortMe = True Then
                            '// Forced abortion
                            GlobalVariables.trdRunning -= 1
                            Return ""
                        End If
                        AddAvToLayout(charAv)
                    End If
                Next
                If operationCount = 1 Then
                    _completed = True
                    Application.DoEvents()
                Else
                    While Not _completed
                        '// Wait on first thread to finish
                    End While
                    GlobalVariables.trdRunning = 0
                    AVLayoutPanel.BeginInvoke(New AddControlDelegate(AddressOf DelegateControlAdding))
                End If
            Catch myex As Exception
                LogAppend("Something went wrong: " & myex.ToString(), "AchievementsInterface_ContinueOperation", False, True)
                LogAppend("Additional info - trdRunning: " & GlobalVariables.trdRunning.ToString() & " - abortMe: " &
                GlobalVariables.abortMe.ToString() & " - operationCount: " & operationCount.ToString(),
                "AchievementsInterface_ContinueOperation", False, True)
                GlobalVariables.trdRunning = 0
            End Try
            ThreadExtensions.ScSend(_context, New Action(Of CompletedEventArgs)(AddressOf OnCompleted),
                                    New CompletedEventArgs())
            Return ""
        End Function
        Private Sub OnCompleted() Handles Me.AvCompleted
            '// Set background color for each achievement panel
            Try
                For Each avPanel As Control In AVLayoutPanel.Controls
                    If _colorTicker = 1 Then
                        _colorTicker = 0
                        Application.DoEvents()
                        avPanel.BackColor = Color.FromArgb(110, 149, 190)
                    Else
                        _colorTicker = 1
                        Application.DoEvents()
                        avPanel.BackColor = Color.FromArgb(126, 144, 156)
                    End If
                Next
            Catch ex As Exception
                LogAppend("Something went wrong: " & ex.ToString(), "AchievementsInterface_OnCompleted", False, True)
                LogAppend("Additional info - trdRunning: " & GlobalVariables.trdRunning.ToString() & " - abortMe: " &
                GlobalVariables.abortMe.ToString(), "AchievementsInterface_ContinueOperation", False, True)
            End Try
            subcat_combo.Enabled = True
            Application.DoEvents()
        End Sub

        Private Sub DelegateControlAdding()
            If Not _controlsToAdd Is Nothing Then
                For Each addNewPanel As Control In _controlsToAdd
                    addNewPanel.SetDoubleBuffered()
                    AVLayoutPanel.Controls.Add(addNewPanel)
                    AVLayoutPanel.Controls.SetChildIndex(AVLayoutPanel.Controls(AVLayoutPanel.Controls.Count - 1),
                                                         1)
                Next
            End If
            If AVLayoutPanel.Controls.Count > 0 Then
                waitpanel.Location = New Point(4000, 4000)
            Else
                waitLabel.Text = ResourceHandler.GetUserMessage("noAvFound")
            End If
        End Sub

        Private Sub highlighter2_Click(sender As Object, e As EventArgs)
            LogAppend("Close button clicked", "AchievementsInterface_highlighter2_Click")
            _state = "closing"
            Close()
        End Sub

        Private Sub add_bt_Click(sender As Object, e As EventArgs) Handles add_bt.Click
            '// Add new achievement
            If GlobalVariables.trdRunning > 0 Then
                '// Currently loading achievements
                Exit Sub
            End If
            LogAppend("Adding new achievement", "AchievementsInterface_add_bt_Click")
            Dim retnvalue As Integer = TryInt(InputBox(ResourceHandler.GetUserMessage("enterAvId"), ResourceHandler.GetUserMessage("addAv"), "0"))
            Userwait.Show()
            Application.DoEvents()
            If Not retnvalue = 0 Then
                LogAppend("Checking validity of achievement id: " & retnvalue.ToString(), "AchievementsInterface_add_bt_Click")
                Dim client As New WebClient
                client.CheckProxy()
                Try
                    If _
                        Not _
                        client.DownloadString("http://wowhead.com/achievement=" & retnvalue.ToString()).Contains(
                            "<div id=""inputbox-error"">This achievement doesn't exist.</div>") Then
                        For Each opAv As Achievement In GlobalVariables.currentEditedCharSet.Achievements
                            If opAv.Id = retnvalue Then
                                LogAppend("Character has this achievement already", "AchievementsInterface_add_bt_Click")
                                MsgBox(ResourceHandler.GetUserMessage("achievementalreadypresent"), MsgBoxStyle.Critical, "Error")
                                Userwait.Close()
                                Exit Sub
                            End If
                        Next
                        LogAppend("Achievement id is valid and not yet added", "AchievementsInterface_add_bt_Click")
                        Dim charAv As New Achievement
                        charAv.Id = retnvalue
                        charAv.GainDate = Date.Today.ToTimeStamp()
                        GlobalVariables.currentEditedCharSet.Achievements.Add(charAv)
                        Dim catBt As Button =
                                CType(
                                    Controls(
                                        "cat_id_" &
                                        GetAvMainCategoryIdBySubCatId(GetAvSubCategoryById(charAv.Id)).ToString() &
                                        "_bt"), 
                                    Button)
                        catBt.PerformClick()
                        MsgBox(ResourceHandler.GetUserMessage("achievementadded"), , "Info")
                    Else
                        MsgBox(ResourceHandler.GetUserMessage("invalidavid"), MsgBoxStyle.Critical, "Error")
                    End If
                Catch ex As Exception
                    MsgBox(ResourceHandler.GetUserMessage("invalidavid"), MsgBoxStyle.Critical, "Error")
                End Try
            Else
                MsgBox(ResourceHandler.GetUserMessage("invalidavid"), MsgBoxStyle.Critical, "Error")
            End If
            Userwait.Close()
        End Sub

        Private Sub subcat_combo_SelectedIndexChanged(sender As Object, e As EventArgs) _
            Handles subcat_combo.SelectedIndexChanged
            If GlobalVariables.trdRunning > 0 Then
                subcat_combo.SelectedIndex = 0
                Exit Sub
            End If
            subcat_combo.Enabled = False
            If _preCatControlLst Is Nothing Then
                _preCatControlLst = New List(Of Control)
            Else
                AVLayoutPanel.Controls.Clear()
                For Each avPanel In _preCatControlLst
                    AVLayoutPanel.Controls.Add(avPanel)
                    avPanel.SetDoubleBuffered()
                    Application.DoEvents()
                Next
            End If
            Dim catid As Integer = CType(subcat_combo.SelectedItem, AvSubcategoy).Id
            If Not catid = 0 Then
                Dim removeCtrlLst As New List(Of Control)
                For Each subctrl As Control In AVLayoutPanel.Controls
                    _preCatControlLst.Add(subctrl)
                    Dim charAv As Achievement = CType(subctrl.Tag, Achievement)
                    If Not charAv.SubCategory = catid Then
                        Dim x As Control = subctrl
                        removeCtrlLst.Add(x)
                    End If
                Next
                For Each ctrl As Control In removeCtrlLst
                    AVLayoutPanel.Controls.Remove(ctrl)
                Next
            End If
            OnCompleted()
        End Sub

        Private Sub callbacktimer_Tick(sender As Object, e As EventArgs) Handles callbacktimer.Tick
            callbacktimer.Stop()
            If GlobalVariables.trdRunning = 0 Then
                _goon = True
                Select Case _state
                    Case "catbt"
                        catbt_click(_tmpSender, _mEvent)
                End Select
            Else
                callbacktimer.Start()
            End If
        End Sub

        Private Sub FilterResults(ByVal searchTxt As String)
            LogAppend("Filtering achievements", "Achievements_interface_FilterResults", True)
            GlobalVariables.trdRunning += 1
            Dim foundAvList As New List(Of Achievement)
            Dim searchId As Integer = TryInt(searchTxt)
            Dim searchName As String = ""
            If searchId = 0 Then
                searchName = searchTxt
            End If
            For i = 0 To GlobalVariables.currentEditedCharSet.Achievements.Count - 1
                Dim charAv As Achievement = GlobalVariables.currentEditedCharSet.Achievements(i)
                Try
                    If searchName = "" Then
                        '// Id
                        If charAv.Id = searchId Then
                            foundAvList.Add(charAv)
                        End If
                    Else
                        '// Name
                        If charAv.Name = Nothing Then
                            charAv.Name = GetAvNameById(charAv.Id, MySettings.Default.language)
                            GlobalVariables.currentEditedCharSet.Achievements(i) = charAv
                        End If
                        If charAv.Name.ToLower.Contains(searchName.ToLower()) Then
                            foundAvList.Add(charAv)
                        End If
                    End If
                Catch ex As Exception
                    LogAppend("Exception during achievement browsing: " & ex.ToString(),
                              "Achievements_interface_FilterResults", False, True)
                End Try
            Next i
            For Each charAv As Achievement In foundAvList
                AddAvToLayout(charAv)
            Next
            AVLayoutPanel.BeginInvoke(New AddControlDelegate(AddressOf DelegateControlAdding))
            GlobalVariables.trdRunning -= 1
            ThreadExtensions.ScSend(_context, New Action(Of CompletedEventArgs)(AddressOf OnFilterCompleted),
                                    New CompletedEventArgs())
        End Sub

        Private Sub AddAvToLayout(ByVal charAv As Achievement)
            If charAv.SubCategory = 0 Then charAv.SubCategory = GetAvSubCategoryById(charAv.Id)
            Dim avPanel As New Panel
            avPanel.Name = "av" & charAv.Id.ToString() & "_pnl"
            avPanel.Size = referencePanel.Size
            avPanel.Tag = charAv
            Dim avNameLable As New Label
            Dim cAvName As String
            If charAv.Name = Nothing Then
                cAvName = GetAvNameById(charAv.Id, MySettings.Default.language)
                charAv.Name = cAvName
            Else
                cAvName = charAv.Name
            End If
            avNameLable.Name = "av" & charAv.Id.ToString() & "_name_lbl"
            avNameLable.Text = cAvName & " - (" & charAv.Id.ToString & ")"
            Application.DoEvents()
            avNameLable.Tag = charAv
            avPanel.Controls.Add(avNameLable)
            avNameLable.Location = reference_name_lbl.Location
            avNameLable.Font = reference_name_lbl.Font
            avNameLable.BringToFront()
            avNameLable.AutoSize = True
            Dim avDescrLable As New Label
            Dim descr As String
            If charAv.Description = Nothing Then
                descr = GetAvDescriptionById(charAv.Id, MySettings.Default.language)
                charAv.Description = descr
            Else
                descr = charAv.Description
            End If
            avDescrLable.Name = "av" & charAv.Id.ToString() & "_descr_lbl"
            avDescrLable.Text = descr
            avDescrLable.Tag = charAv
            avPanel.Controls.Add(avDescrLable)
            avDescrLable.Location = reference_description_lbl.Location
            avDescrLable.Font = reference_description_lbl.Font
            avDescrLable.AutoSize = False
            avDescrLable.Size = reference_description_lbl.Size
            Dim avSubCatLable As New Label
            Dim subcat As String
            If charAv.SubCategoryName = Nothing Then
                subcat = GetAvCatNameById(GetAvSubCategoryById(charAv.Id), MySettings.Default.language)
                charAv.SubCategoryName = subcat
            Else
                subcat = charAv.SubCategoryName
            End If
            avSubCatLable.Name = "av" & charAv.Id.ToString() & "_subcat_lbl"
            avSubCatLable.Text = subcat
            avSubCatLable.Tag = charAv
            avPanel.Controls.Add(avSubCatLable)
            avSubCatLable.Location = reference_subcat_lbl.Location
            avSubCatLable.Font = reference_subcat_lbl.Font
            avSubCatLable.AutoSize = False
            avSubCatLable.RightToLeft = RightToLeft.Yes
            avSubCatLable.Size = reference_subcat_lbl.Size
            Dim avGainDateLabel As New Label
            Dim gaindate As String = charAv.GainDate.ToDate.ToString()
            avGainDateLabel.Name = "av" & charAv.Id.ToString() & "_date_lbl"
            avGainDateLabel.Text = gaindate
            avGainDateLabel.Tag = charAv
            avPanel.Controls.Add(avGainDateLabel)
            avGainDateLabel.Location = reference_date_lbl.Location
            avGainDateLabel.Font = reference_date_lbl.Font
            avGainDateLabel.AutoSize = False
            avGainDateLabel.RightToLeft = RightToLeft.Yes
            avGainDateLabel.Size = reference_date_lbl.Size
            Dim avIconPic As New PictureBox
            Dim avImage As Bitmap
            If charAv.Icon Is Nothing Then
                avImage = GetSpellIconById(GetAvSpellIdById((charAv.Id)), GlobalVariables.GlobalWebClient)
                charAv.Icon = avImage
            Else
                avImage = charAv.Icon
            End If
            avIconPic.Name = "av" & charAv.Id.ToString() & "_icon_pic"
            avIconPic.Image = avImage
            avIconPic.Tag = charAv
            avPanel.Controls.Add(avIconPic)
            avIconPic.Location = reference_icon_pic.Location
            avIconPic.SizeMode = PictureBoxSizeMode.StretchImage
            avIconPic.Size = reference_icon_pic.Size
            Application.DoEvents()
            Dim deletePic As New PictureBox
            deletePic.Name = "av" & charAv.Id.ToString() & "_delete_pic"
            deletePic.Image = reference_delete_pic.Image
            deletePic.Tag = charAv
            avPanel.Controls.Add(deletePic)
            deletePic.Location = reference_delete_pic.Location
            deletePic.SizeMode = PictureBoxSizeMode.StretchImage
            deletePic.Size = reference_delete_pic.Size
            deletePic.Cursor = Cursors.Hand
            avPanel.SetDoubleBuffered()
            AddHandler deletePic.Click, AddressOf deleteAv_click
            If _controlsToAdd Is Nothing Then _controlsToAdd = New List(Of Control)()
            _controlsToAdd.Add(avPanel)
            Application.DoEvents()
        End Sub

        Private Sub OnFilterCompleted() Handles Me.FilterCompleted
            Try
                For Each avPanel As Control In AVLayoutPanel.Controls
                    If _colorTicker = 1 Then
                        _colorTicker = 0
                        Application.DoEvents()
                        avPanel.BackColor = Color.FromArgb(110, 149, 190)
                    Else
                        _colorTicker = 1
                        Application.DoEvents()
                        avPanel.BackColor = Color.FromArgb(126, 144, 156) 'Color.SaddleBrown
                    End If
                Next
            Catch ex As Exception

            End Try
            search_bt.Enabled = True
            browse_tb.Text = "Enter achievement name or id"
            browse_tb.ForeColor = SystemColors.WindowFrame
            browse_tb.Enabled = True
            subcat_combo.Enabled = True
            Application.DoEvents()
        End Sub

        Private Sub browse_tb_Enter(sender As Object, e As EventArgs) Handles browse_tb.Enter
            If browse_tb.Text = "Enter achievement name or id" Then
                browse_tb.ForeColor = SystemColors.WindowText
                browse_tb.Text = ""
            End If
        End Sub

        Private Sub browse_tb_Leave(sender As Object, e As EventArgs) Handles browse_tb.Leave
            If browse_tb.Text = "" Then
                browse_tb.ForeColor = SystemColors.WindowFrame
                browse_tb.Text = "Enter achievement name or id"
            End If
        End Sub

        Private Sub search_bt_Click(sender As Object, e As EventArgs) Handles search_bt.Click
            If Not _controlsToAdd Is Nothing Then _controlsToAdd.Clear()
            waitpanel.Location = New Point(4000, 4000)
            search_bt.Enabled = False
            Dim browseTxt As String = browse_tb.Text
            browse_tb.Text = "Browsing achievements..."
            browse_tb.ForeColor = SystemColors.WindowFrame
            browse_tb.Enabled = False
            AVLayoutPanel.Controls.Clear()
            Application.DoEvents()
            Dim trd As Thread = New Thread(DirectCast(Sub() FilterResults(browseTxt), ThreadStart))
            trd.Start()
        End Sub
    End Class
End Namespace