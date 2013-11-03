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
'*      /Filename:      TemplateExplorer
'*      /Description:   TODO
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.Framework.TemplateSystem
Imports NCFramework.Framework.Modules
Imports Namcore_Studio.Modules.Interface

Namespace Forms

    Public Class TemplateExplorer

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
    End Class
End Namespace