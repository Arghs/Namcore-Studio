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
'*      /Filename:      AchievementsInterface
'*      /Description:   Provides an interface to display character achievement information
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports Namcore_Studio.Modules
Imports Namcore_Studio.Modules.Interface
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Modules.Interface
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Modules
Imports NCFramework.Framework.Extension
Imports Namcore_Studio.Forms.Extension
Imports libnc.Provider
Imports System.Threading
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

        Shared _doneAvIds As List(Of Integer)
        Shared _correctIds As List(Of Integer)
        Shared _colorTicker As Integer
        Shared _completed As Boolean

        Private ReadOnly _context As SynchronizationContext = SynchronizationContext.Current
        Public Event AvCompleted As EventHandler(Of CompletedEventArgs)
        Private WithEvents _mHandler As New TrdQueueHandler

        Delegate Sub AddControlDelegate(panel2Add As Panel)
        Delegate Sub UpdateControlDelegate(ctrl As Control)
        '// Declaration

        Private Sub Achievements_interface_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            AddHandler highlighter2.Click, AddressOf highlighter2_Click
            GlobalVariables.trdRunning = 0
            GlobalVariables.abortMe = False
            Dim controlLst As List(Of Control)
            controlLst = FindAllChildren()
            For Each itemControl As Control In controlLst
                itemControl.SetDoubleBuffered()
            Next
            waitpanel.Location = New Point(367, 219)
            subcat_combo.Enabled = False
        End Sub

        Protected Overridable Sub OnCompleted(ByVal e As CompletedEventArgs)
            RaiseEvent AvCompleted(Me, e)
        End Sub

        Public Sub catbt_click(sender As Object, e As EventArgs) _
            Handles cat_id_97_bt.Click, cat_id_96_bt.Click, cat_id_95_bt.Click, cat_id_92_bt.Click, cat_id_81_bt.Click,
                    cat_id_201_bt.Click, cat_id_169_bt.Click, cat_id_168_bt.Click, cat_id_155_bt.Click,
                    cat_id_15219_bt.Click, cat_id_15165_bt.Click
            subcat_combo.Enabled = False
            subcat_combo.Text = ""
            Try
                _preCatControlLst = Nothing
                waitpanel.Location = New Point(4000, 4000)
                _state = "catbt"
                _tmpSender = sender
                _mEvent = e
                _goon = False
                If GlobalVariables.trdRunning > 0 Then
                    '// Currently loading achievements
                    callbacktimer.Stop()
                    GlobalVariables.abortMe = True
                    callbacktimer.Enabled = True
                    callbacktimer.Start()
                    Exit Sub
                Else
                    _goon = True
                End If
                If _goon = True Then
                    _currentCat = TryInt(SplitString(sender.name, "cat_id_", "_bt"))
                    subcat_combo.Items.Clear()
                    Application.DoEvents()
                    Dim tmpCatids As Integer()
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
                            tmpCatids = {170, 171, 182, 15071}
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
                            tmpCatids = {}
                    End Select
                    Dim catCollection As New List(Of AvSubcategoy)

                    For i = 0 To tmpCatids.Length - 1
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
                    subcat_combo.Items.Add(
                        New AvSubcategoy With {.Text = ResourceHandler.GetUserMessage("subcat0"), .Id = 0})
                    Try
                        For Each cat As AvSubcategoy In catCollection
                            subcat_combo.Items.Add(cat)
                        Next
                    Catch ex As Exception
                        subcat_combo.Items.Clear()
                    End Try
                    _colorTicker = 0
                    _completed = False
                    Do
                        For Each subctrl As Control In AVLayoutPanel.Controls
                            AVLayoutPanel.Controls.Remove(subctrl)
                            subctrl.Dispose()
                        Next
                        Application.DoEvents()
                    Loop Until AVLayoutPanel.Controls.Count = 0
                    _correctIds = New List(Of Integer)()
                    _doneAvIds = New List(Of Integer)()
                    _correctIds = GetAvIdListByMainCat(TryInt(SplitString(sender.name, "cat_id_", "_bt")))
                    GlobalVariables.abortMe = False
                    _mHandler.doOperate_av(sender, 1)
                    _mHandler.doOperate_av(sender, 2)
                End If
            Catch ex As Exception
                GlobalVariables.trdRunning = 0
                GlobalVariables.abortMe = False
            End Try
        End Sub

        Private Sub deleteAv_click(sender As Object, e As EventArgs)
            '// Delete achievement

            If GlobalVariables.trdRunning > 0 Then
                '// Currently loading achievements
                Exit Sub
            End If
            Dim charAv As Achievement = sender.tag

            Dim msg As String = ResourceHandler.GetUserMessage("aus_deleteav")
            msg = msg.Replace("%avid%", charAv.Id.ToString)
            Dim result = MsgBox(msg, vbYesNo, ResourceHandler.GetUserMessage("areyousure"))
            If result = MsgBoxResult.Yes Then
                Userwait.Show()
                For Each subctrl In AVLayoutPanel.Controls
                    If subctrl.tag.id = charAv.Id Then
                        AVLayoutPanel.Controls.Remove(subctrl)
                        subctrl.Dispose()
                        For Each av As Achievement In GlobalVariables.currentViewedCharSet.Achievements
                            If av.Id = charAv.Id Then
                                If GlobalVariables.currentEditedCharSet Is Nothing Then
                                    GlobalVariables.currentEditedCharSet = GlobalVariables.currentViewedCharSet
                                    GlobalVariables.currentViewedCharSet.Achievements.Remove(av)
                                Else
                                    GlobalVariables.currentViewedCharSet.Achievements.Remove(av)
                                End If
                                Exit For
                            End If
                        Next
                        Exit For
                    End If
                Next
            End If
            Userwait.Close()
        End Sub

        Public Function ContinueOperation(ByVal sender As Object, ByVal operationCount As Integer) As String
            If operationCount = 1 Then _
                LogAppend("Loading achievements", "Achievements_interface_continueOperation", True)
            GlobalVariables.trdRunning += 1
            Try
                If operationCount = 2 Then
                    Thread.Sleep(2000)
                End If

                For Each charAv As Achievement In GlobalVariables.currentViewedCharSet.Achievements

                    If _doneAvIds.Contains(charAv.Id) Then
                        Continue For
                    Else
                        _doneAvIds.Add(charAv.Id)
                        Application.DoEvents()
                    End If
                    If _correctIds.Contains(charAv.Id) Then
                        LogAppend("Operating next av / operation_count is:" & operationCount.ToString(),
                                  "Achievements_continueOperation", False)
                        If GlobalVariables.abortMe = True Then
                            GlobalVariables.trdRunning -= 1
                            Exit Function
                        End If
                        If charAv.SubCategory = Nothing Then charAv.SubCategory = GetAvSubCategoryById(charAv.Id)
                        Dim avPanel As New Panel
                        avPanel.Name = "av" & charAv.Id.ToString() & "_pnl"
                        avPanel.Size = referencePanel.Size
                        avPanel.Tag = charAv
                        Dim avNameLable As New Label
                        Dim cAvName As String
                        If charAv.Name = Nothing Then
                            cAvName = GetAvNameById(charAv.Id, NCFramework.My.MySettings.Default.language)
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
                            descr = GetAvDescriptionById(charAv.Id, NCFramework.My.MySettings.Default.language)
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
                            subcat = GetAvCatNameById(GetAvSubCategoryById(charAv.Id), NCFramework.My.MySettings.Default.language)
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
                        Dim avImage As Image
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
                        AddHandler deletePic.Click, AddressOf deleteAv_click
                        AVLayoutPanel.BeginInvoke(New AddControlDelegate(AddressOf DelegateControlAdding), avPanel)
                        Application.DoEvents()
                    End If
                Next
                If operationCount = 1 Then
                    _completed = True
                    Application.DoEvents()
                Else
                    While Not _completed

                    End While

                    SetCharacterSet(GlobalVariables.currentViewedCharSetId, GlobalVariables.currentViewedCharSet)
                    Try
                        If _
                            AVLayoutPanel.Controls(AVLayoutPanel.Controls.Count - 2).BackColor =
                            Color.FromArgb(110, 149, 190) Then
                            AVLayoutPanel.Controls(AVLayoutPanel.Controls.Count - 1).BackColor = Color.FromArgb(126, 144,
                                                                                                                156)
                        Else
                            AVLayoutPanel.Controls(AVLayoutPanel.Controls.Count - 1).BackColor = Color.FromArgb(110, 149,
                                                                                                                190)
                        End If
                        AVLayoutPanel.BeginInvoke(New UpdateControlDelegate(AddressOf DelegateControlUpdating), AVLayoutPanel)
                    Catch ex As Exception
                        AVLayoutPanel.BeginInvoke(New UpdateControlDelegate(AddressOf DelegateControlUpdating), AVLayoutPanel)
                    End Try
                    GlobalVariables.trdRunning = 0
                End If
            Catch myex As Exception
                GlobalVariables.trdRunning = 0
            End Try
            ThreadExtensions.ScSend(_context, New Action(Of CompletedEventArgs)(AddressOf OnCompleted),
                                    New CompletedEventArgs())
            ' ReSharper disable VBWarnings::BC42105
        End Function
        ' ReSharper restore VBWarnings::BC42105
        Private Sub OnCompleted() Handles Me.AvCompleted
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
            subcat_combo.Enabled = True
            Application.DoEvents()
        End Sub
        Private Sub DelegateControlAdding(addNewPanel As Panel)
            addNewPanel.SetDoubleBuffered()
            AVLayoutPanel.Controls.Add(addNewPanel)
        End Sub
        Private Sub DelegateControlUpdating(ctrl As FlowLayoutPanel)
            ctrl.Update()
        End Sub

        Private Sub highlighter2_Click(sender As Object, e As EventArgs)
            _state = "closing"
            Close()
        End Sub

        Private Sub add_bt_Click(sender As Object, e As EventArgs) Handles add_bt.Click
            '// Add new achievement
            If GlobalVariables.trdRunning > 0 Then
                '// Currently loading achievements
                Exit Sub
            End If
            If _currentCat = Nothing Then Exit Sub
            Dim retnvalue As Integer = TryInt(InputBox("Enter achievement id: ", "Add achievement", "0"))
            Userwait.Show()
            Application.DoEvents()
            If Not retnvalue = 0 Then
                Dim client As New WebClient
                client.CheckProxy()
                Try
                    If _
                        Not _
                        client.DownloadString("http://wowhead.com/achievement=" & retnvalue.ToString()).Contains(
                            "<div id=""inputbox-error"">This achievement doesn't exist.</div>") Then
                        For Each opAv As Achievement In GlobalVariables.currentViewedCharSet.Achievements
                            If opAv.Id = retnvalue Then
                                Dim _
                                    rm2 As _
                                        New ResourceManager("NCFramework.UserMessages", Assembly.GetExecutingAssembly())
                                MsgBox(rm2.GetString("achievementalreadypresent"), MsgBoxStyle.Critical, "Error")
                                Userwait.Close()
                                Exit Sub
                            End If
                        Next
                        Dim charAv As New Achievement
                        charAv.Id = retnvalue
                        charAv.GainDate = toTimeStamp(Date.Today)
                        If _correctIds.Contains(charAv.Id) Then
                            charAv.SubCategory = GetAvSubCategoryById(charAv.Id)
                            Dim avPanel As New Panel
                            avPanel.Name = "av" & charAv.Id.ToString() & "_pnl"
                            avPanel.Size = referencePanel.Size
                            avPanel.Tag = charAv
                            Dim avNameLable As New Label
                            Dim cAvName As String
                            If charAv.Name = Nothing Then
                                cAvName = GetAvNameById(charAv.Id, NCFramework.My.MySettings.Default.language)
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
                                descr = GetAvDescriptionById(charAv.Id, NCFramework.My.MySettings.Default.language)
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
                                subcat = GetAvSubCategoryById(charAv.Id)
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
                            Dim gaindate As String = charAv.GainDate.toDate.ToString()
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
                            Dim avImage As Image
                            If charAv.Icon Is Nothing Then
                                avImage = GetSpellIconById(GetAvSpellIdById(charAv.Id), GlobalVariables.GlobalWebClient)
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
                            AddHandler deletePic.Click, AddressOf deleteAv_click
                            AVLayoutPanel.BeginInvoke(New AddControlDelegate(AddressOf DelegateControlAdding), avPanel)
                            Application.DoEvents()
                        End If
                        If GlobalVariables.currentEditedCharSet Is Nothing Then
                            GlobalVariables.currentEditedCharSet = GlobalVariables.currentViewedCharSet
                            GlobalVariables.currentEditedCharSet.Achievements.Add(charAv)
                        Else
                            GlobalVariables.currentEditedCharSet.Achievements.Add(charAv)
                        End If
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
            Dim catid As Integer = subcat_combo.SelectedItem.id.ToString
            If Not catid = 0 Then
                Dim removeCtrlLst As New List(Of Control)
                For Each subctrl As Control In AVLayoutPanel.Controls
                    _preCatControlLst.Add(subctrl)
                    Dim charAv As Achievement = subctrl.Tag
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
    End Class
End Namespace