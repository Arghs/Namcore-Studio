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
'*      /Filename:      TemplateExplorer
'*      /Description:   TODO
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework
Imports NCFramework.Framework.Modules
Imports NamCore_Studio.Modules.Interface
Imports NamCore_Studio.Forms.Extension
Imports ShaperRater

Namespace Forms
    Public Class TemplateExplorer
        Inherits EventTrigger
        '// Declaration
        Delegate Sub AddControlDelegate(ctrlLst As List(Of Control))

        Private WithEvents _mHandler As New TrdQueueHandler
        '// Declaration
        Private Sub openfile_bt_Click(sender As Object, e As EventArgs) Handles openfile_bt.Click
            Dim locOfd As New OpenFileDialog()
            Dim locPath As String
            With locOfd
                .Filter = "NamCore Studio Template File (*.ncsf)|*.ncsf"
                .Title = "Select template file"
                .DefaultExt = ".ncsf"
                .Multiselect = False
                .CheckFileExists = True
                .CheckPathExists = True
                If (.ShowDialog() = DialogResult.OK) Then
                    locPath = .FileName()
                    If Not locPath = "" Then
                        GlobalVariables.LoadingTemplate = True
                        Dim mSerializer As Serializer = New Serializer
                        GlobalVariables.globChars = mSerializer.DeSerialize(locPath, New GlobalCharVars)
                        Hide()
                        prepareLive_template()
                    End If
                End If
            End With
        End Sub

        Private Sub TemplateExplorer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            AddHandler highlighter2.Click, AddressOf highlighter2_Click
            _mHandler.doOperate_templateExplorer()
        End Sub

        Private Sub highlighter2_Click(sender As Object, e As EventArgs)
            back_bt.PerformClick()
        End Sub

        Private Sub back_bt_Click(sender As Object, e As EventArgs) Handles back_bt.Click
            LogAppend("Trigger back button click", "TemplateExplorer_highlighter2_Click", False)
            If GlobalVariables.lastregion = "main" Or GlobalVariables.lastregion = "liveview" Then
                Close()
                Main.Show()
            End If
        End Sub

        Public Function ContinueOperation() As String
            Dim ctrlLst As New List(Of Control)
            Dim tHandler As New TaexHandler
            Dim entryList As List(Of TemplateEntry) = tHandler.LoadTemplateEntries()
            If entryList IsNot Nothing Then
                For Each entry As TemplateEntry In entryList
                    Dim templatePanel As New Panel
                    With templatePanel
                        .Size = reference_template_panel.Size
                        .BackColor = reference_template_panel.BackColor
                        .Tag = entry
                        .Name = "template_" & entry.Id.ToString() & "_panel"
                        .Location = New Point(0, 0)
                        .SetDoubleBuffered()
                    End With
                    Dim nameLbl As New Label
                    templatePanel.Controls.Add(nameLbl)
                    With nameLbl
                        .Tag = entry
                        .Name = "template_" & entry.Id.ToString() & "_name_lbl"
                        .Text = entry.Name
                        .Font = reference_templatename_lbl.Font
                        .ForeColor = reference_templatename_lbl.ForeColor
                        .Size = reference_templatename_lbl.Size
                        .Location = reference_templatename_lbl.Location
                        .BackColor = reference_templatename_lbl.BackColor
                    End With
                    Dim authorLbl As New Label
                    templatePanel.Controls.Add(authorLbl)
                    With authorLbl
                        .Tag = entry
                        .Name = "template_" & entry.Id.ToString() & "_author_lbl"
                        .Text = "By " & entry.OwnerName
                        .Font = reference_author_lbl.Font
                        .ForeColor = reference_author_lbl.ForeColor
                        .Size = reference_author_lbl.Size
                        .Location = reference_author_lbl.Location
                        .BackColor = reference_author_lbl.BackColor
                    End With
                    Dim dateLbl As New Label
                    templatePanel.Controls.Add(dateLbl)
                    With dateLbl
                        .Tag = entry
                        .Name = "template_" & entry.Id.ToString() & "_date_lbl"
                        .Text = entry.CreatedDate.ToString("yyyy-MM-dd")
                        .Font = reference_date_lbl.Font
                        .ForeColor = reference_date_lbl.ForeColor
                        .Size = reference_date_lbl.Size
                        .Location = reference_date_lbl.Location
                        .BackColor = reference_date_lbl.BackColor
                        .AutoSize = reference_date_lbl.AutoSize
                    End With
                    Dim downloadBt As New Button
                    templatePanel.Controls.Add(downloadBt)
                    With downloadBt
                        .Tag = entry
                        .Name = "template_" & entry.Id.ToString() & "_download_bt"
                        .Text = "Download"
                        .Font = reference_download_bt.Font
                        .ForeColor = reference_download_bt.ForeColor
                        .Size = reference_download_bt.Size
                        .Location = reference_download_bt.Location
                        .BackColor = reference_download_bt.BackColor
                        .FlatStyle = reference_download_bt.FlatStyle
                    End With
                    AddHandler downloadBt.Click, AddressOf Download_Click
                    Dim ratingRater As New Rater
                    templatePanel.Controls.Add(ratingRater)
                    With ratingRater
                        .Tag = entry
                        .Name = "template_" & entry.Id.ToString() & "_rating_rater"
                        .Font = reference_rating_rater.Font
                        .ForeColor = reference_rating_rater.ForeColor
                        .Size = reference_rating_rater.Size
                        .Location = reference_rating_rater.Location
                        .BackColor = reference_rating_rater.BackColor
                        .ShapeBorderEmptyColor = reference_rating_rater.ShapeBorderEmptyColor
                        .ShapeBorderFilledColor = reference_rating_rater.ShapeBorderFilledColor
                        .ShapeBorderHoverColor = reference_rating_rater.ShapeBorderHoverColor
                        .ShapeColorEmpty = reference_rating_rater.ShapeColorEmpty
                        .ShapeColorFill = reference_rating_rater.ShapeColorFill
                        .ShapeColorHover = reference_rating_rater.ShapeColorHover
                        .RadiusInner = 0.0!
                        .RadiusOuter = 10.0!
                        .CurrentRating = entry.Rating
                        .LabelShow = False
                        .Shape = reference_rating_rater.Shape
                        .SetDoubleBuffered()
                    End With
                    Dim downloadLbl As New Label
                    templatePanel.Controls.Add(downloadLbl)
                    With downloadLbl
                        .Tag = entry
                        .Name = "template_" & entry.Id.ToString() & "_downloads_lbl"
                        .Text = entry.DownloadCount.ToString() & " Downloads"
                        .Font = reference_downloadcounter_lbl.Font
                        .ForeColor = reference_downloadcounter_lbl.ForeColor
                        .Size = reference_downloadcounter_lbl.Size
                        .Location = reference_downloadcounter_lbl.Location
                        .BackColor = reference_downloadcounter_lbl.BackColor
                        .TextAlign = reference_downloadcounter_lbl.TextAlign
                        .AutoSize = reference_downloadcounter_lbl.AutoSize
                    End With
                    Dim descrPanel As New Panel
                    templatePanel.Controls.Add(descrPanel)
                    With descrPanel
                        .Tag = entry
                        .Name = "template_" & entry.Id.ToString() & "_description_panel"
                        .Font = reference_description_panel.Font
                        .ForeColor = reference_description_panel.ForeColor
                        .Size = reference_description_panel.Size
                        .Location = reference_description_panel.Location
                        .BackColor = reference_description_panel.BackColor
                        .AutoSize = reference_description_panel.AutoSize
                        .AutoScroll = reference_description_panel.AutoScroll
                    End With
                    Dim descrLbl As New Label
                    descrPanel.Controls.Add(descrLbl)
                    With descrLbl
                        .Tag = entry
                        .Name = "template_" & entry.Id.ToString() & "_description_lbl"
                        .Text = entry.Description
                        .Font = reference_description_lbl.Font
                        .ForeColor = reference_description_lbl.ForeColor
                        .Size = reference_description_lbl.Size
                        .Location = reference_description_lbl.Location
                        .BackColor = reference_description_lbl.BackColor
                        .TextAlign = reference_description_lbl.TextAlign
                        .AutoSize = reference_description_lbl.AutoSize
                    End With
                    ctrlLst.Add(templatePanel)
                Next
                template_layout_panel.BeginInvoke(New AddControlDelegate(AddressOf DelegateControlAdding), ctrlLst)
                Return ""
            Else : Return ""
            End If
        End Function

        Private Sub DelegateControlAdding(ctrlLst As IEnumerable(Of Control))
            If Not ctrlLst Is Nothing Then
                For Each addNewPanel As Control In ctrlLst
                    addNewPanel.SetDoubleBuffered()
                    template_layout_panel.Controls.Add(addNewPanel)
                    template_layout_panel.Controls.SetChildIndex(
                        template_layout_panel.Controls(template_layout_panel.Controls.Count - 1),
                        1)
                Next
            End If
        End Sub

        Private Sub Download_Click(sender As Object, e As EventArgs)

        End Sub
    End Class
End Namespace