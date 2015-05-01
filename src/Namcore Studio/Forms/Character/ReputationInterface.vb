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
'*      /Filename:      ReputationInterface
'*      /Description:   Provides an interface to display character reputation information
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.Extension
Imports NCFramework.My
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Modules
Imports NamCore_Studio.Modules.Interface
Imports NCFramework.Framework.Logging
Imports NamCore_Studio.Forms.Extension
Imports libnc.Provider
Imports NCFramework.My.Resources
Imports System.Net

Namespace Forms.Character
    Public Class ReputationInterface
        Inherits EventTrigger

        '// Declaration
        Private Const PanelMaxLength As Integer = 454
        Private Const PanelMinLength As Integer = 5
        Dim _lasttxtvalue As String
        Dim _valueisok As Boolean = False
        '// Declaration

        Public Sub PrepareRepInterface(ByVal setId As Integer)
            LogAppend("Loading reputation", "Reputation_interface_prepareRepInterface", True)
            Dim controlLst As List(Of Control)
            controlLst = FindAllChildren()
            For Each itemControl As Control In controlLst
                itemControl.SetDoubleBuffered()
            Next
            Dim colorTicker As Integer = 0
            RepLayoutPanel.Controls.Add(addpanel)
            If GlobalVariables.currentEditedCharSet Is Nothing Then
                GlobalVariables.currentEditedCharSet = DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet)
            End If
            If MySettings.Default.language = "de" Then
                'todo
                Dim cnt As Integer = 0
                Do

                    reference_standing_combo.Items.Add(ResourceHandler.GetLocalizedString("standing_" & cnt.ToString))
                    cnt += 1
                Loop Until cnt = 8
            Else
                Dim cnt As Integer = 0
                Do

                    reference_standing_combo.Items.Add(ResourceHandler.GetLocalizedString("standing_" & cnt.ToString))
                    cnt += 1
                Loop Until cnt = 8
            End If
            Try
                GlobalVariables.currentEditedCharSet.PlayerReputation.Sort(
                    Function(x, y) String.Compare(x.Name, y.Name, StringComparison.Ordinal))
            Catch ex As Exception

            End Try
            Dim newSet As NCFramework.Framework.Modules.Character = GlobalVariables.currentEditedCharSet
            For Each pRepu As Reputation In newSet.PlayerReputation
                If (pRepu.Flags And Reputation.FlagEnum.FACTION_FLAG_VISIBLE) = Reputation.FlagEnum.FACTION_FLAG_VISIBLE _
                    Then
                    Dim repPanel As New Panel
                    repPanel.Name = "rep" & pRepu.Faction.ToString() & "_pnl"
                    repPanel.Size = referencePanel.Size
                    repPanel.Tag = pRepu
                    If colorTicker = 0 Then
                        colorTicker = 1
                        repPanel.BackColor = Color.FromArgb(110, 149, 190)
                    Else
                        colorTicker = 0
                        repPanel.BackColor = Color.FromArgb(126, 144, 156)
                    End If
                    Dim repNameLable As New Label
                    repNameLable.Name = "rep" & pRepu.Faction.ToString() & "_name_lbl"
                    Dim factionName As String = pRepu.Name
                    If factionName Is Nothing Then
                        factionName = GetFactionNameById(pRepu.Faction, MySettings.Default.language)
                        pRepu.Name = factionName
                    End If
                    repNameLable.Text = factionName
                    repNameLable.Tag = pRepu
                    repPanel.Controls.Add(repNameLable)
                    repNameLable.Location = reference_name_lbl.Location
                    Dim sliderBgPanel As New Panel
                    sliderBgPanel.Name = "rep" & pRepu.Faction.ToString() & "_sliderBg_pnl"
                    sliderBgPanel.Size = reference_sliderbg_panel.Size
                    sliderBgPanel.BackgroundImage = My.Resources.repbg1
                    sliderBgPanel.BackgroundImageLayout = ImageLayout.Stretch
                    sliderBgPanel.Tag = pRepu
                    Dim progressPanel As New Panel
                    progressPanel.Name = "rep" & pRepu.Faction.ToString() & "_progress_pnl"
                    progressPanel.Size = reference_percentage_panel.Size
                    progressPanel.BackColor = Color.Yellow
                    progressPanel.Tag = pRepu
                    sliderBgPanel.Controls.Add(progressPanel)
                    progressPanel.Location = reference_percentage_panel.Location
                    repPanel.Controls.Add(sliderBgPanel)
                    If pRepu.Max = 0 Then pRepu.Max = 1
                    SetPanelPercentage(progressPanel, CType((pRepu.Value/pRepu.Max), Decimal))
                    sliderBgPanel.Location = reference_sliderbg_panel.Location
                    Dim slider As New TrackBar
                    slider.Name = "rep" & pRepu.Faction.ToString() & "_slider"
                    slider.Maximum = pRepu.Max
                    If pRepu.Value > pRepu.Max Then pRepu.Value = pRepu.Max
                    slider.Value = pRepu.Value
                    slider.Size = reference_trackbar.Size
                    slider.Tag = pRepu
                    slider.TickStyle = TickStyle.None
                    repPanel.Controls.Add(slider)
                    slider.Location = reference_trackbar.Location
                    AddHandler slider.Scroll, AddressOf slider_slide '//Use MouseUp event for better performance 
                    Dim valueTxtbox As New TextBox
                    valueTxtbox.Name = "rep" & pRepu.Faction.ToString() & "_value_txtbox"
                    valueTxtbox.Size = reference_txtbox.Size
                    valueTxtbox.Text = pRepu.Value.ToString()
                    valueTxtbox.Tag = pRepu
                    repPanel.Controls.Add(valueTxtbox)
                    valueTxtbox.Location = reference_txtbox.Location
                    AddHandler valueTxtbox.TextChanged, AddressOf txt_changed
                    AddHandler valueTxtbox.Enter, AddressOf txt_enter
                    AddHandler valueTxtbox.Leave, AddressOf txt_left
                    Dim valueLbl As New Label
                    valueLbl.Name = "rep" & pRepu.Faction.ToString() & "_value_lbl"
                    valueLbl.Text = pRepu.Value.ToString & "/" & pRepu.Max.ToString()
                    valueLbl.Font = reference_counter_lbl.Font
                    valueLbl.ForeColor = reference_counter_lbl.ForeColor
                    valueLbl.BackColor = reference_counter_lbl.BackColor
                    valueLbl.Tag = pRepu
                    valueLbl.AutoSize = True
                    repPanel.Controls.Add(valueLbl)
                    valueLbl.Location = reference_counter_lbl.Location
                    valueLbl.BringToFront()
                    RepLayoutPanel.Controls.Add(repPanel)
                    Dim standingCombo As New ComboBox
                    standingCombo.Name = "rep" & pRepu.Faction.ToString() & "_standing_combo"
                    standingCombo.Location = reference_standing_combo.Location
                    For Each itm As String In reference_standing_combo.Items
                        standingCombo.Items.Add(itm)
                    Next
                    standingCombo.SelectedIndex = pRepu.Status
                    Select Case pRepu.Status
                        Case Reputation.RepStatus.REPSTAT_STRANGER : progressPanel.BackColor = Color.DarkRed
                        Case Reputation.RepStatus.REPSTAT_ACQUAINTANCE : progressPanel.BackColor = Color.Red
                        Case Reputation.RepStatus.REPSTAT_UNFRIENDLY : progressPanel.BackColor = Color.Red
                        Case Reputation.RepStatus.REPSTAT_NEUTRAL : progressPanel.BackColor = Color.Yellow
                        Case Reputation.RepStatus.REPSTAT_FRIENDLY : progressPanel.BackColor = Color.Green
                        Case Reputation.RepStatus.REPSTAT_HONORED : progressPanel.BackColor = Color.Green
                        Case Reputation.RepStatus.REPSTAT_REVERED : progressPanel.BackColor = Color.Green
                        Case Reputation.RepStatus.REPSTAT_EXALTED : progressPanel.BackColor = Color.LightGreen
                    End Select
                    standingCombo.Tag = pRepu
                    repPanel.Controls.Add(standingCombo)
                    AddHandler standingCombo.SelectedIndexChanged, AddressOf StandingChanged
                End If
            Next
            CloseProcessStatus()
        End Sub

        Private Sub slider_slide(ByVal sender As Object, ByVal e As EventArgs)
            Dim slider As TrackBar = CType(sender, TrackBar)
            If Not slider.Value = 0 Then
                For Each pCtrl As Control In RepLayoutPanel.Controls
                    If pCtrl.Name.Contains("rep" & CType(slider.Tag, Reputation).Faction.ToString() & "_pnl") Then
                        Dim namectrl() As Control =
                                pCtrl.Controls.Find(
                                    "rep" & CType(slider.Tag, Reputation).Faction.ToString() & "_value_txtbox", True)
                        Dim aCtrl As Control
                        For Each aCtrl In namectrl
                            DirectCast(aCtrl, TextBox).Text = slider.Value.ToString()
                        Next
                        Dim namectrl2() As Control =
                                pCtrl.Controls.Find(
                                    "rep" & CType(slider.Tag, Reputation).Faction.ToString() & "_value_lbl", True)
                        Dim aCtrl2 As Control
                        For Each aCtrl2 In namectrl2
                            DirectCast(aCtrl2, Label).Text = slider.Value.ToString() & "/" & slider.Maximum.ToString
                        Next
                        For Each subCtrl As Control In pCtrl.Controls
                            If _
                                subCtrl.Name.Contains(
                                    "rep" & CType(slider.Tag, Reputation).Faction.ToString() & "_sliderBg_pnl") Then
                                For Each subsubCtrl As Control In subCtrl.Controls
                                    If _
                                        subsubCtrl.Name.Contains(
                                            "rep" & CType(slider.Tag, Reputation).Faction.ToString() & "_progress_pnl") _
                                        Then
                                        SetPanelPercentage(CType(subsubCtrl, Panel),
                                                           CType((slider.Value/slider.Maximum), Decimal))
                                        Dim loc As Integer =
                                                GlobalVariables.currentEditedCharSet.PlayerReputation.FindIndex(
                                                    Function(rep) rep.Faction = CType(pCtrl.Tag, Reputation).Faction)
                                        CType(pCtrl.Tag, Reputation).Value = slider.Value
                                        Dim thisRep As Reputation = CType(pCtrl.Tag, Reputation)
                                        pCtrl.Tag = thisRep.UpdateStanding()
                                        GlobalVariables.currentEditedCharSet.PlayerReputation(loc) = CType(pCtrl.Tag,
                                                                                                           Reputation)
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            Else
                For Each pCtrl As Control In RepLayoutPanel.Controls
                    If pCtrl.Name.Contains("rep" & CType(slider.Tag, Reputation).Faction.ToString() & "_pnl") Then
                        Dim namectrl() As Control =
                                pCtrl.Controls.Find(
                                    "rep" & CType(slider.Tag, Reputation).Faction.ToString() & "_value_txtbox", True)
                        Dim aCtrl As Control
                        For Each aCtrl In namectrl
                            DirectCast(aCtrl, TextBox).Text = slider.Value.ToString()
                        Next
                        Dim namectrl2() As Control =
                                pCtrl.Controls.Find(
                                    "rep" & CType(slider.Tag, Reputation).Faction.ToString() & "_value_lbl", True)
                        Dim aCtrl2 As Control
                        For Each aCtrl2 In namectrl2
                            DirectCast(aCtrl2, Label).Text = slider.Value.ToString() & "/" & slider.Maximum.ToString
                        Next
                        For Each subCtrl As Control In pCtrl.Controls
                            If _
                                subCtrl.Name.Contains(
                                    "rep" & CType(slider.Tag, Reputation).Faction.ToString() & "_sliderBg_pnl") Then
                                For Each subsubCtrl As Control In subCtrl.Controls
                                    If _
                                        subsubCtrl.Name.Contains(
                                            "rep" & CType(slider.Tag, Reputation).Faction.ToString() & "_progress_pnl") _
                                        Then
                                        SetPanelPercentage(CType(subsubCtrl, Panel), 0)
                                        Dim loc As Integer =
                                                GlobalVariables.currentEditedCharSet.PlayerReputation.FindIndex(
                                                    Function(rep) (pCtrl.Tag.Equals(rep)))
                                        CType(slider.Tag, Reputation).Value = slider.Value
                                        Dim thisRep As Reputation = CType(slider.Tag, Reputation)
                                        pCtrl.Tag = thisRep.UpdateStanding()
                                        GlobalVariables.currentEditedCharSet.PlayerReputation(loc) = CType(slider.Tag,
                                                                                                           Reputation)
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            End If
        End Sub

        Private Sub SetPanelPercentage(ByRef repPanel As Panel, ByVal percentage As Decimal)
            Const lengthrange As Integer = PanelMaxLength - PanelMinLength
            repPanel.Size = CType(New Point(CType((percentage*lengthrange + PanelMinLength), Integer),
                                            repPanel.Size.Height),
                                  Size)
        End Sub

        Private Sub StandingChanged(sender As Object, e As EventArgs)
            Dim combo As ComboBox = CType(sender, ComboBox)
            Dim max As Integer
            Dim col As Color
            Select Case combo.SelectedIndex
                Case 0 : col = Color.DarkRed
                    max = 8400
                Case 1 : col = Color.Red
                    max = 3000
                Case 2 : col = Color.Red
                    max = 3000
                Case 3 : col = Color.Yellow
                    max = 3000
                Case 4 : col = Color.Green
                    max = 6000
                Case 5 : col = Color.Green
                    max = 12000
                Case 6 : col = Color.Green
                    max = 21000
                Case 7 : col = Color.LightGreen
                    max = 999
            End Select

            For Each pCtrl As Control In RepLayoutPanel.Controls
                If pCtrl.Name.Contains("rep" & CType(combo.Tag, Reputation).Faction.ToString() & "_pnl") Then
                    Dim loc As Integer =
                            GlobalVariables.currentEditedCharSet.PlayerReputation.FindIndex(
                                Function(rep) rep.Faction = CType(pCtrl.Tag, Reputation).Faction)
                    CType(pCtrl.Tag, Reputation).Value = 0
                    CType(pCtrl.Tag, Reputation).Max = max
                    CType(pCtrl.Tag, Reputation).Status = CType(combo.SelectedIndex, Reputation.RepStatus)
                    Dim thisRep As Reputation = CType(pCtrl.Tag, Reputation)
                    pCtrl.Tag = thisRep.UpdateStanding()
                    GlobalVariables.currentEditedCharSet.PlayerReputation(loc) = CType(pCtrl.Tag, Reputation)
                    DirectCast(FindControl("rep" & CType(combo.Tag, Reputation).Faction.ToString() & "_slider", pCtrl),
                               TrackBar).Value = 0
                    DirectCast(FindControl("rep" & CType(combo.Tag, Reputation).Faction.ToString() & "_slider", pCtrl),
                               TrackBar).Maximum =
                        max
                    DirectCast(FindControl("rep" & CType(combo.Tag, Reputation).Faction.ToString() & "_value_lbl", pCtrl),
                               Label).Text =
                        "0/" &
                        max.
                            ToString
                    DirectCast(FindControl("rep" & CType(combo.Tag, Reputation).Faction.ToString() & "_value_txtbox",
                                           pCtrl),
                               TextBox).Text _
                        =
                        "0"
                    Dim xctrl As Control =
                            FindControl("rep" & CType(combo.Tag, Reputation).Faction.ToString() & "_sliderBg_pnl", pCtrl)
                    Dim progresspanel As Control =
                            FindControl("rep" & CType(combo.Tag, Reputation).Faction.ToString() & "_progress_pnl",
                                        xctrl)
                    DirectCast(progresspanel, Panel).BackColor = col
                    SetPanelPercentage(CType(progresspanel, Panel), 0)
                End If
            Next
        End Sub

        Private Sub txt_enter(sender As Object, e As EventArgs)
            _lasttxtvalue = CType(sender, TextBox).Text
        End Sub

        Private Sub txt_changed(sender As Object, e As EventArgs)
            _valueisok = False
            Dim txtbox As TextBox = CType(sender, TextBox)
            If Not txtbox.Text.Length < 1 Then
                Dim int As Integer = TryInt(txtbox.Text)
                If Not int = 0 OrElse txtbox.Text = "0" Then
                    For Each pCtrl As Control In RepLayoutPanel.Controls
                        If pCtrl.Name.Contains("rep" & CType(txtbox.Tag, Reputation).Faction.ToString() & "_pnl") Then
                            If int <= CType(pCtrl.Tag, Reputation).Max And int >= 0 Then
                                Dim loc As Integer =
                                        GlobalVariables.currentEditedCharSet.PlayerReputation.FindIndex(
                                            Function(rep) rep.Faction = CType(pCtrl.Tag, Reputation).Faction)
                                DirectCast(
                                    FindControl("rep" & CType(pCtrl.Tag, Reputation).Faction.ToString() & "_slider",
                                                pCtrl),
                                    TrackBar).
                                    Value = int
                                DirectCast(
                                    FindControl("rep" & CType(pCtrl.Tag, Reputation).Faction.ToString() & "_value_lbl",
                                                pCtrl),
                                    Label).
                                    Text = int.ToString() & "/" & CType(pCtrl.Tag, Reputation).Max.ToString
                                Dim xctrl As Control =
                                        FindControl(
                                            "rep" & CType(pCtrl.Tag, Reputation).Faction.ToString() & "_sliderBg_pnl",
                                            pCtrl)
                                Dim progresspanel As Control =
                                        FindControl(
                                            "rep" & CType(pCtrl.Tag, Reputation).Faction.ToString() & "_progress_pnl",
                                            xctrl)
                                SetPanelPercentage(CType(progresspanel, Panel),
                                                   CType((int/CType(pCtrl.Tag, Reputation).Max), Decimal))
                                CType(pCtrl.Tag, Reputation).Value = int
                                Dim thisRep As Reputation = CType(pCtrl.Tag, Reputation)
                                pCtrl.Tag = thisRep.UpdateStanding()
                                _valueisok = True
                                GlobalVariables.currentEditedCharSet.PlayerReputation(loc) = CType(pCtrl.Tag, Reputation)
                            End If
                        End If
                    Next
                End If
            End If
        End Sub

        Private Sub txt_left(sender As Object, e As EventArgs)
            Dim txtbox As TextBox = CType(sender, TextBox)
            If Not _valueisok Then
                txtbox.Text = _lasttxtvalue
            End If
        End Sub

        Private Function FindControl(ByVal controlname As String, ByVal repuControl As Control) As Control
            Dim namectrl() As Control = repuControl.Controls.Find(controlname, True)
            Return namectrl(0)
        End Function

        Private Sub Reputation_interface_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            AddHandler highlighter2.Click, AddressOf highlighter2_Click
        End Sub

        Private Sub add_pic_Click(sender As Object, e As EventArgs) Handles add_pic.Click
            Dim retnvalue As Integer = TryInt(InputBox(MSG_ENTERFACTIONID, MSG_ADDFACTION, "0"))
            Userwait.Show()
            Application.DoEvents()
            If Not retnvalue = 0 Then
                Dim client As New WebClient
                client.CheckProxy()
                Try
                    If _
                        Not _
                        client.DownloadString("http://www.wowdb.com/factions/" & retnvalue.ToString()).Contains(
                            "<title>Not found - WowDB</title>") Then
                        For Each opRepu As Reputation In GlobalVariables.currentEditedCharSet.PlayerReputation
                            If opRepu.Faction = retnvalue Then

                                MsgBox(MSG_FACTIONALREADYPRESENT, MsgBoxStyle.Critical, MSG_ERROR)
                                Userwait.Close()
                                Exit Sub
                            End If
                        Next
                        Dim pRepu As New Reputation
                        With pRepu
                            .Faction = retnvalue
                            .Flags = CType(1, Reputation.FlagEnum)
                            .Name = GetFactionNameById(.Faction, MySettings.Default.language)
                            .Max = 3000
                            .Standing = 0
                            .Status = Reputation.RepStatus.REPSTAT_NEUTRAL
                            .Value = 0
                        End With
                        Dim repPanel As New Panel
                        repPanel.Name = "rep" & pRepu.Faction.ToString() & "_pnl"
                        repPanel.Size = referencePanel.Size
                        repPanel.Tag = pRepu
                        repPanel.BackColor = Color.Silver
                        Dim repNameLable As New Label
                        repNameLable.Name = "rep" & pRepu.Faction.ToString() & "_name_lbl"
                        repNameLable.Text = pRepu.Name
                        repNameLable.Tag = pRepu
                        repPanel.Controls.Add(repNameLable)
                        repNameLable.Location = reference_name_lbl.Location
                        Dim sliderBgPanel As New Panel
                        sliderBgPanel.Name = "rep" & pRepu.Faction.ToString() & "_sliderBg_pnl"
                        sliderBgPanel.Size = reference_sliderbg_panel.Size
                        sliderBgPanel.BackgroundImage = My.Resources.repbg1
                        sliderBgPanel.BackgroundImageLayout = ImageLayout.Stretch
                        sliderBgPanel.Tag = pRepu
                        Dim progressPanel As New Panel
                        progressPanel.Name = "rep" & pRepu.Faction.ToString() & "_progress_pnl"
                        progressPanel.Size = reference_percentage_panel.Size
                        progressPanel.BackColor = Color.Yellow
                        progressPanel.Tag = pRepu
                        sliderBgPanel.Controls.Add(progressPanel)
                        progressPanel.Location = reference_percentage_panel.Location
                        repPanel.Controls.Add(sliderBgPanel)
                        SetPanelPercentage(progressPanel, CType((pRepu.Value/pRepu.Max), Decimal))
                        sliderBgPanel.Location = reference_sliderbg_panel.Location
                        Dim slider As New TrackBar
                        slider.Name = "rep" & pRepu.Faction.ToString() & "_slider"
                        slider.Maximum = pRepu.Max
                        slider.Value = pRepu.Value
                        slider.Size = reference_trackbar.Size
                        slider.Tag = pRepu
                        slider.TickStyle = TickStyle.None
                        repPanel.Controls.Add(slider)
                        slider.Location = reference_trackbar.Location
                        AddHandler slider.Scroll, AddressOf slider_slide '//Use MouseUp event for better performance 
                        Dim valueTxtbox As New TextBox
                        valueTxtbox.Name = "rep" & pRepu.Faction.ToString() & "_value_txtbox"
                        valueTxtbox.Size = reference_txtbox.Size
                        valueTxtbox.Text = pRepu.Value.ToString()
                        valueTxtbox.Tag = pRepu
                        repPanel.Controls.Add(valueTxtbox)
                        valueTxtbox.Location = reference_txtbox.Location
                        AddHandler valueTxtbox.TextChanged, AddressOf txt_changed
                        AddHandler valueTxtbox.Enter, AddressOf txt_enter
                        AddHandler valueTxtbox.Leave, AddressOf txt_left
                        Dim valueLbl As New Label
                        valueLbl.Name = "rep" & pRepu.Faction.ToString() & "_value_lbl"
                        valueLbl.Text = pRepu.Value.ToString & "/" & pRepu.Max.ToString()
                        valueLbl.Font = reference_counter_lbl.Font
                        valueLbl.ForeColor = reference_counter_lbl.ForeColor
                        valueLbl.BackColor = reference_counter_lbl.BackColor
                        valueLbl.Tag = pRepu
                        valueLbl.AutoSize = True
                        repPanel.Controls.Add(valueLbl)
                        valueLbl.Location = reference_counter_lbl.Location
                        valueLbl.BringToFront()
                        RepLayoutPanel.Controls.Add(repPanel)
                        RepLayoutPanel.Controls.SetChildIndex(RepLayoutPanel.Controls(RepLayoutPanel.Controls.Count - 1),
                                                              1)
                        Dim standingCombo As New ComboBox
                        standingCombo.Name = "rep" & pRepu.Faction.ToString() & "_standing_combo"
                        standingCombo.Location = reference_standing_combo.Location
                        For Each itm As String In reference_standing_combo.Items
                            standingCombo.Items.Add(itm)
                        Next
                        standingCombo.SelectedIndex = pRepu.Status
                        Select Case pRepu.Status
                            Case Reputation.RepStatus.REPSTAT_STRANGER : progressPanel.BackColor = Color.DarkRed
                            Case Reputation.RepStatus.REPSTAT_ACQUAINTANCE : progressPanel.BackColor = Color.Red
                            Case Reputation.RepStatus.REPSTAT_UNFRIENDLY : progressPanel.BackColor = Color.Red
                            Case Reputation.RepStatus.REPSTAT_NEUTRAL : progressPanel.BackColor = Color.Yellow
                            Case Reputation.RepStatus.REPSTAT_FRIENDLY : progressPanel.BackColor = Color.Green
                            Case Reputation.RepStatus.REPSTAT_HONORED : progressPanel.BackColor = Color.Green
                            Case Reputation.RepStatus.REPSTAT_REVERED : progressPanel.BackColor = Color.Green
                            Case Reputation.RepStatus.REPSTAT_EXALTED : progressPanel.BackColor = Color.LightGreen
                        End Select
                        standingCombo.Tag = pRepu

                        standingCombo.Text = ResourceHandler.GetLocalizedString("standing_" & pRepu.Status.ToString)
                        repPanel.Controls.Add(standingCombo)
                        AddHandler standingCombo.SelectedIndexChanged, AddressOf StandingChanged
                        GlobalVariables.currentEditedCharSet.PlayerReputation.Add(pRepu)
                        MsgBox(MSG_FACTIONADDED, , MSG_INFO)
                    Else

                        MsgBox(MSG_INVALIDFACTIONID, MsgBoxStyle.Critical, MSG_ERROR)
                    End If
                Catch ex As Exception

                    MsgBox(MSG_INVALIDFACTIONID, MsgBoxStyle.Critical, MSG_ERROR)
                End Try

            Else

                MsgBox(MSG_INVALIDFACTIONID, MsgBoxStyle.Critical, MSG_ERROR)
            End If
            Userwait.Close()
        End Sub

        Private Sub highlighter2_Click(sender As Object, e As EventArgs)
            Close()
        End Sub
    End Class
End Namespace