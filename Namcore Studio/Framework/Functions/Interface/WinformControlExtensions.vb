Module WinformControlExtensions
    ''' <summary>
    ''' Recursively find all child controls for a form
    ''' </summary>
    ''' <param name="StartingContainer"><c><seealso cref="System.Windows.Forms.Form">Form
    ''' </seealso></c> that is the starting container to check for children.</param>
    ''' <returns><c><seealso cref="List(Of System.Windows.Forms.Control)">List(Of Control)
    ''' </seealso></c> that contains a reference to all child controls.</returns>
    ''' <remarks>If you put this module in a separate namespace from your form, Visual Studio 
    ''' 2010 does not recognize the extension to the form.</remarks>
    <System.Runtime.CompilerServices.Extension()>
    Public  Function FindAllChildren(ByRef StartingContainer As System.Windows.Forms.Form) As List(Of System.Windows.Forms.Control)
        Dim Children As New List(Of System.Windows.Forms.Control)

        Dim oControl As System.Windows.Forms.Control
        For Each oControl In StartingContainer.Controls
            Children.Add(oControl)
            If oControl.HasChildren Then
                Children.AddRange(oControl.FindAllChildren())
            End If
        Next

        Return Children
    End Function
    ''' <summary>
    ''' Recursively find all child controls for a control
    ''' </summary>
    ''' <param name="StartingContainer"><c><seealso cref="System.Windows.Forms.Control">Control
    ''' </seealso></c> that is the starting container to check for children.</param>
    ''' <returns><c><seealso cref="List(Of System.Windows.Forms.Control)">List(Of Control)
    ''' </seealso></c> that contains a reference to all child controls.</returns>
    ''' <remarks></remarks>
    <System.Runtime.CompilerServices.Extension()>
    Public Function FindAllChildren(ByRef StartingContainer As System.Windows.Forms.Control) As List(Of System.Windows.Forms.Control)
        Dim Children As New List(Of System.Windows.Forms.Control)

        If StartingContainer.HasChildren = False Then
            Return Nothing
        Else
            Dim oControl As System.Windows.Forms.Control
            For Each oControl In StartingContainer.Controls
                Children.Add(oControl)
                If oControl.HasChildren Then
                    Children.AddRange(oControl.FindAllChildren())
                End If
            Next
        End If

        Return Children
    End Function
End Module