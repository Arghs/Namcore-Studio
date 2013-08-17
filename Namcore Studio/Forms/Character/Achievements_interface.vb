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
'*      /Filename:      Achievements_interface
'*      /Description:   Provides an interface to display character achievement information
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Imports Namcore_Studio.Basics
Imports Namcore_Studio.SpellItem_Information
Imports Namcore_Studio.Conversions
Imports Namcore_Studio.GlobalVariables
Imports Namcore_Studio.EventLogging
Imports System.Threading

Public Class Achievements_interface
    Dim currentpage As Integer
    Dim tarsetid As Integer
    Dim lastindex As Integer
    Dim itmlst1 As New List(Of ListViewItem)
    Dim itmlst2 As New List(Of ListViewItem)
  
    Private Sub Achievements_interface_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim controlLST As List(Of Control)
        controlLST = FindAllChildren()
        For Each item_control As Control In controlLST
            item_control.SetDoubleBuffered()
        Next
    End Sub
    Public Sub prepareInterface(ByVal setId As Integer)
        'If tempAchievementInfo Is Nothing Then tempAchievementInfo = New List(Of ListViewItem)
        'If tempAchievementInfoIndex Is Nothing Then tempAchievementInfoIndex = ""
        tarsetid = setId
        'Dim player As Character = GetCharacterSetBySetId(tarsetid)
        'trd1.IsBackground = True
        'trd2.IsBackground = True
        'If player.Achievements.Count > 50 Then
        '    trd1.Start()
        '    trd2.Start()
        'Else
        '    trd1.Start()
        'End If


    End Sub
 
    Private context As Threading.SynchronizationContext = Threading.SynchronizationContext.Current
    Public Event AVCompleted As EventHandler(Of CompletedEventArgs)
    Protected Overridable Sub OnCompleted(ByVal e As CompletedEventArgs)
        RaiseEvent AVCompleted(Me, e)
    End Sub
    Private WithEvents m_handler As New FlowLayoutPanelHandler
    Public Sub catbt_click(sender As Object, e As EventArgs) Handles cat_id_97_bt.Click, cat_id_96_bt.Click, cat_id_95_bt.Click, cat_id_92_bt.Click, cat_id_81_bt.Click, cat_id_201_bt.Click, cat_id_169_bt.Click, cat_id_168_bt.Click, cat_id_155_bt.Click, cat_id_15219_bt.Click, cat_id_15165_bt.Click
        correctIds = New List(Of Integer)()
        doneAvIds = New List(Of Integer)()
        correctIds = GetAvIdListByMainCat(TryInt(splitString(sender.name, "cat_id_", "_bt")))
        m_handler.doOperate_av(sender, 1)
        m_handler.doOperate_av(sender, 2)

    End Sub
   
    Private Sub deleteAv_click(sender As Object, e As EventArgs)

    End Sub
    Shared doneAvIds As List(Of Integer)
    Shared correctIds As List(Of Integer)

    Public Function continueOperation(ByVal sender As Object, ByVal operation_count As Integer) As String
        Dim catid As Integer = TryInt(splitString(sender.name, "cat_id_", "_bt"))
        Dim player As Character = GetCharacterSetBySetId(tarsetid)
        Dim colorTicker As Integer = 0
        Dim counter As Integer = 0

        For Each charAv As Achievement In player.Achievements
            If doneAvIds.Contains(charAv.Id) Then
                Continue For
            Else
                doneAvIds.Add(charAv.Id)
            End If
            If correctIds.Contains(charAv.Id) Then

                counter += 1
                Dim avPanel As New Panel
                avPanel.Name = "av" & charAv.Id.ToString() & "_pnl"
                avPanel.Size = referencePanel.Size
                avPanel.Tag = charAv
                If colorTicker = 0 Then
                    colorTicker = 1
                    avPanel.BackColor = Color.FromArgb(110, 149, 190)
                Else
                    colorTicker = 0
                    avPanel.BackColor = Color.FromArgb(126, 144, 156) 'Color.SaddleBrown
                End If
                Dim avNameLable As New Label
                Dim c_avName As String = GetAvNameById(charAv.Id)
                avNameLable.Name = "av" & charAv.Id.ToString() & "_name_lbl"
                avNameLable.Text = c_avName & " - (" & charAv.Id.ToString & ")"
                Application.DoEvents()
                avNameLable.Tag = charAv
                avPanel.Controls.Add(avNameLable)
                avNameLable.Location = reference_name_lbl.Location
                avNameLable.Font = reference_name_lbl.Font
                avNameLable.BringToFront()
                avNameLable.AutoSize = True
                Dim avDescrLable As New Label
                Dim descr As String = GetAvDescriptionById(charAv.Id)
                avDescrLable.Name = "av" & charAv.Id.ToString() & "_descr_lbl"
                avDescrLable.Text = descr
                avDescrLable.Tag = charAv
                avPanel.Controls.Add(avDescrLable)
                avDescrLable.Location = reference_description_lbl.Location
                avDescrLable.Font = reference_description_lbl.Font
                avDescrLable.AutoSize = False
                avDescrLable.Size = reference_description_lbl.Size
                Dim avSubCatLable As New Label
                Dim subcat As String = GetAvCategoryById(charAv.Id, True)
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
                Dim avImage As Image = GetAvIconById(charAv.Id)
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
                'If counter > 6 Then Exit For
            End If
        Next
        ThreadExtensions.ScSend(context, New Action(Of CompletedEventArgs)(AddressOf OnCompleted), New CompletedEventArgs())
    End Function
    Delegate Sub AddControlDelegate(panel2add As Panel)
    Private Sub DelegateControlAdding(addPanel As Panel)
        AVLayoutPanel.Controls.Add(addPanel)
    End Sub
End Class