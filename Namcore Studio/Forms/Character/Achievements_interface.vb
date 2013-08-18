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
Imports System.Net
Imports System.Resources

Public Class Achievements_interface
    Dim currentpage As Integer
    Dim tarsetid As Integer
    Dim lastindex As Integer
    Dim itmlst1 As New List(Of ListViewItem)
    Dim itmlst2 As New List(Of ListViewItem)
    Dim globplayer As Character
    Dim current_cat As Integer = Nothing
    Private Sub Achievements_interface_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim controlLST As List(Of Control)
        controlLST = FindAllChildren()
        For Each item_control As Control In controlLST
            item_control.SetDoubleBuffered()
        Next
    End Sub
    Public Sub prepareInterface(ByVal setId As Integer)
        tarsetid = setId
    End Sub
 
    Private context As Threading.SynchronizationContext = Threading.SynchronizationContext.Current
    Public Event AVCompleted As EventHandler(Of CompletedEventArgs)
    Protected Overridable Sub OnCompleted(ByVal e As CompletedEventArgs)
        RaiseEvent AVCompleted(Me, e)
    End Sub
    Private WithEvents m_handler As New FlowLayoutPanelHandler
    Public Sub catbt_click(sender As Object, e As EventArgs) Handles cat_id_97_bt.Click, cat_id_96_bt.Click, cat_id_95_bt.Click, cat_id_92_bt.Click, cat_id_81_bt.Click, cat_id_201_bt.Click, cat_id_169_bt.Click, cat_id_168_bt.Click, cat_id_155_bt.Click, cat_id_15219_bt.Click, cat_id_15165_bt.Click
        current_cat = TryInt(splitString(sender.name, "cat_id_", "_bt"))
        globplayer = GetCharacterSetBySetId(tarsetid)
        colorTicker = 0
        completed = False
        Do
            For Each subctrl As Control In AVLayoutPanel.Controls
                AVLayoutPanel.Controls.Remove(subctrl)
                subctrl.Dispose()
            Next
            Application.DoEvents()
        Loop Until AVLayoutPanel.Controls.Count = 0
        correctIds = New List(Of Integer)()
        doneAvIds = New List(Of Integer)()
        correctIds = GetAvIdListByMainCat(TryInt(splitString(sender.name, "cat_id_", "_bt")))
        m_handler.doOperate_av(sender, 1)
        m_handler.doOperate_av(sender, 2)

    End Sub
   
    Private Sub deleteAv_click(sender As Object, e As EventArgs)

    End Sub
    Shared avColorLst As ArrayList
    Shared doneAvIds As List(Of Integer)
    Shared correctIds As List(Of Integer)
    Shared colorTicker As Integer
    Shared completed As Boolean
    Public Function continueOperation(ByVal sender As Object, ByVal operation_count As Integer) As String
        ' Dim catid As Integer = TryInt(splitString(sender.name, "cat_id_", "_bt"))
        Dim player As Character = GetCharacterSetBySetId(tarsetid)
        For Each charAv As Achievement In player.Achievements
            If doneAvIds.Contains(charAv.Id) Then
                Continue For
            Else
                doneAvIds.Add(charAv.Id)
            End If
            If correctIds.Contains(charAv.Id) Then
                Dim avPanel As New Panel
                avPanel.Name = "av" & charAv.Id.ToString() & "_pnl"
                avPanel.Size = referencePanel.Size
                avPanel.Tag = charAv
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
            End If
        Next
        If operation_count = 1 Then
            completed = True
        Else
            While Not completed

            End While
            For Each avPanel As Control In AVLayoutPanel.Controls
                If colorTicker = 1 Then
                    colorTicker = 0
                    Application.DoEvents()
                    avPanel.BackColor = Color.FromArgb(110, 149, 190)
                Else
                    colorTicker = 1
                    Application.DoEvents()
                    avPanel.BackColor = Color.FromArgb(126, 144, 156) 'Color.SaddleBrown
                End If
            Next
        End If



            ThreadExtensions.ScSend(context, New Action(Of CompletedEventArgs)(AddressOf OnCompleted), New CompletedEventArgs())
    End Function
    Delegate Sub AddControlDelegate(panel2add As Panel)
    Private Sub DelegateControlAdding(addPanel As Panel)
        addPanel.SetDoubleBuffered()
        AVLayoutPanel.Controls.Add(addPanel)
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

    Private Sub header_MouseDown(sender As Object, e As MouseEventArgs) Handles header.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            ptMouseDownLocation = e.Location
        End If
    End Sub


    Private Sub header_MouseMove(sender As Object, e As MouseEventArgs) Handles header.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me.Location = e.Location - ptMouseDownLocation + Location
        End If
    End Sub

    Private Sub AVLayoutPanel_Paint(sender As Object, e As PaintEventArgs) Handles AVLayoutPanel.Paint

    End Sub

    Private Sub add_bt_Click(sender As Object, e As EventArgs) Handles add_bt.Click
        Dim retnvalue As Integer = TryInt(InputBox("Enter achievement id: ", "Add achievement", "0"))
        Userwait.Show()
        Application.DoEvents()
        If Not retnvalue = 0 Then
            Dim client As New WebClient
            Try
                If Not client.DownloadString("http://wowhead.com/achievement=" & retnvalue.ToString()).Contains("<div id=""inputbox-error"">This achievement doesn't exist.</div>") Then
                    For Each opAv As Achievement In globplayer.Achievements
                        If opAv.Id = retnvalue Then
                            Dim RM2 As New ResourceManager("Namcore_Studio.UserMessages", System.Reflection.Assembly.GetExecutingAssembly())
                            MsgBox(RM2.GetString("achievementalreadypresent"), MsgBoxStyle.Critical, "Error")
                            Userwait.Close()
                            Exit Sub
                        End If
                    Next
                    Dim charAv As New Achievement
                    charAv.Id = retnvalue
                    charAv.GainDate = toTimeStamp(Date.Today)
                   If correctIds.Contains(charAv.Id) Then
                        Dim avPanel As New Panel
                        avPanel.Name = "av" & charAv.Id.ToString() & "_pnl"
                        avPanel.Size = referencePanel.Size
                        avPanel.Tag = charAv
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
                    End If
                    Dim RM As New ResourceManager("Namcore_Studio.UserMessages", System.Reflection.Assembly.GetExecutingAssembly())
                    globplayer.Achievements.Add(charAv)
                    MsgBox(RM.GetString("achievementadded"), , "Info")
                Else
                    Dim RM As New ResourceManager("Namcore_Studio.UserMessages", System.Reflection.Assembly.GetExecutingAssembly())
                    MsgBox(RM.GetString("invalidavid"), MsgBoxStyle.Critical, "Error")
                End If
            Catch ex As Exception
                Dim RM As New ResourceManager("Namcore_Studio.UserMessages", System.Reflection.Assembly.GetExecutingAssembly())
                MsgBox(RM.GetString("invalidavid"), MsgBoxStyle.Critical, "Error")
            End Try

        Else
            Dim RM As New ResourceManager("Namcore_Studio.UserMessages", System.Reflection.Assembly.GetExecutingAssembly())
            MsgBox(RM.GetString("invalidavid"), MsgBoxStyle.Critical, "Error")
        End If
        Userwait.Close()
    End Sub
End Class