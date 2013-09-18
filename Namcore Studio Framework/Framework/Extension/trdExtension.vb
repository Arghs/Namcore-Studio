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
'*      /Filename:      ThreadExtensions
'*      /Description:   Needed when using threadding
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.Threading

Namespace Framework.Extension

    Public Class CompletedEventArgs
        Inherits EventArgs
    End Class

    Public Class ThreadExtensions

        '// Declaration
        Private _args() As Object
        Private _delegateToInvoke As [Delegate]
        '// Declaration

        Public Shared Function QueueUserWorkItem(ByVal method As [Delegate], ByVal ParamArray args() As Object) As Boolean
            Return _
                ThreadPool.QueueUserWorkItem(AddressOf ProperDelegate,
                                             New ThreadExtensions With {._args = args, ._delegateToInvoke = method})
        End Function

        Public Shared Sub ScSend(ByVal sc As SynchronizationContext, ByVal del As [Delegate],
                                 ByVal ParamArray args() As Object)
            sc.Send(New SendOrPostCallback(AddressOf ProperDelegate),
                    New ThreadExtensions With {._args = args, ._delegateToInvoke = del})
        End Sub

        Private Shared Sub ProperDelegate(ByVal state As Object)
            Try
                Dim sd As ThreadExtensions = DirectCast(state, ThreadExtensions)
                sd._delegateToInvoke.DynamicInvoke(sd._args)
            Catch ex As Exception

            End Try
        End Sub
    End Class
End Namespace