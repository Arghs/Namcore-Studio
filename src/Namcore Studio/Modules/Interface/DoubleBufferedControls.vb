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
'*      /Filename:      DoubleBufferedControls
'*      /Description:   Extension that enables double buffering of controls
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.Runtime.CompilerServices
Imports System.Reflection

Namespace Modules.Interface
    Module DoubleBufferedControls
        <Extension()>
        Public Sub SetDoubleBuffered(control As Control)
            control.GetType().InvokeMember("DoubleBuffered", BindingFlags.SetProperty Or
                                                               BindingFlags.Instance Or BindingFlags.NonPublic, Nothing,
                                             control, New Object() {True})
        End Sub
    End Module
End Namespace