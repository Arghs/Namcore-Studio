'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013 NamCore Studio <https://github.com/megasus/Namcore-Studio>
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
'*      /Filename:      TemplateExplorer
'*      /Description:   TODO
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NamCore_Studio.Forms.Extension
Imports NCFramework.Framework.TemplateSystem
Imports NCFramework.Framework.Modules
Imports NamCore_Studio.Modules.Interface
Imports NCFramework.Framework.Logging

Namespace Forms

    Public Class TemplateExplorer
        Inherits EventTrigger

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
    End Class
End Namespace