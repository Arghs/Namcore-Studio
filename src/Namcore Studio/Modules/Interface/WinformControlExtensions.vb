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
'*      /Filename:      WinformControlExtension
'*      /Description:   Extension to find all child controls for a form
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.Runtime.CompilerServices

Namespace Modules.Interface
    Module WinformControlExtensions
        ''' <summary>
        '''     Recursively find all child controls for a form
        ''' </summary>
        ''' <param name="startingContainer">
        '''     <c>
        '''         <seealso cref="System.Windows.Forms.Form">
        '''             Form
        '''         </seealso>
        '''     </c>
        '''     that is the starting container to check for children.
        ''' </param>
        ''' <returns>
        '''     <c>
        '''         <seealso cref="List(Of System.Windows.Forms.Control)">
        '''             List(Of Control)
        '''         </seealso>
        '''     </c>
        '''     that contains a reference to all child controls.
        ''' </returns>
        ''' <remarks>
        '''     If you put this module in a separate namespace from your form, Visual Studio
        '''     2010 does not recognize the extension to the form.
        ''' </remarks>
        <Extension>
        Public Function FindAllChildren(ByRef startingContainer As Form) As List(Of Control)
            Dim children As New List(Of Control)

            Dim oControl As Control
            For Each oControl In StartingContainer.Controls
                children.Add(oControl)
                If oControl.HasChildren Then
                    children.AddRange(oControl.FindAllChildren())
                End If
            Next

            Return children
        End Function


        ''' <summary>
        '''     Recursively find all child controls for a control
        ''' </summary>
        ''' <param name="startingContainer">
        '''     <c>
        '''         <seealso cref="System.Windows.Forms.Control">
        '''             Control
        '''         </seealso>
        '''     </c>
        '''     that is the starting container to check for children.
        ''' </param>
        ''' <returns>
        '''     <c>
        '''         <seealso cref="List(Of System.Windows.Forms.Control)">
        '''             List(Of Control)
        '''         </seealso>
        '''     </c>
        '''     that contains a reference to all child controls.
        ''' </returns>
        ''' <remarks></remarks>
        <Extension>
        Public Function FindAllChildren(ByRef startingContainer As Control) As List(Of Control)
            Dim children As New List(Of Control)

            If StartingContainer.HasChildren = False Then
                Return Nothing
            Else
                Dim oControl As Control
                For Each oControl In StartingContainer.Controls
                    children.Add(oControl)
                    If oControl.HasChildren Then
                        children.AddRange(oControl.FindAllChildren())
                    End If
                Next
            End If

            Return children
        End Function
    End Module
End Namespace