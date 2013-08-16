Imports System.Runtime.CompilerServices
Imports System.Reflection
Module DoubleBufferedControls
    <Extension()> _
    Public Sub SetDoubleBuffered([Control] As Control)

        [Control].GetType().InvokeMember("DoubleBuffered", BindingFlags.SetProperty Or _
    BindingFlags.Instance Or BindingFlags.NonPublic, Nothing, [Control], New Object() {True})

    End Sub
End Module
