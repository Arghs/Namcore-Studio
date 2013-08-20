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

Public Class CompletedEventArgs
    Inherits EventArgs
End Class
Public Class ThreadExtensions
    Private args() As Object
    Private DelegateToInvoke As [Delegate]

    Public Shared Function QueueUserWorkItem(ByVal method As [Delegate], ByVal ParamArray args() As Object) As Boolean
        Return Threading.ThreadPool.QueueUserWorkItem(AddressOf ProperDelegate, New ThreadExtensions With {.args = args, .DelegateToInvoke = method})
    End Function

    Public Shared Sub ScSend(ByVal sc As Threading.SynchronizationContext, ByVal del As [Delegate], ByVal ParamArray args() As Object)
        sc.Send(New Threading.SendOrPostCallback(AddressOf ProperDelegate), New ThreadExtensions With {.args = args, .DelegateToInvoke = del})
    End Sub

    Private Shared Sub ProperDelegate(ByVal state As Object)
        Dim sd As ThreadExtensions = DirectCast(state, ThreadExtensions)
        sd.DelegateToInvoke.DynamicInvoke(sd.args)
    End Sub
End Class