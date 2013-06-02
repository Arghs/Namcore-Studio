Imports Namcore_Studio.Conversions
Imports Namcore_Studio.Basics
Public Class Glyphs_interface
    Dim controlLST As List(Of Control)
    Dim pubGlyph As Glyph
    Dim tempValue As String
    Dim tempSender As Object






    Private Sub Glyphs_interface_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub
    Public Sub prepareGlyphsInterface(ByVal setId As Integer)
        controlLST = New List(Of Control)
        controlLST = FindAllChildren()
        For Each item_control As Control In controlLST
            Select Case True
                Case TypeOf item_control Is Label
                    If item_control.Name.ToLower.EndsWith("_name") Then
                        Dim tempSlotName As String = ""
                        If item_control.Name.ToLower.StartsWith("sec_") Then tempSlotName = "sec"
                        If item_control.Name.ToLower.Contains("prim") Then tempSlotName = tempSlotName & "primglyph"
                        If item_control.Name.ToLower.Contains("major") Then tempSlotName = tempSlotName & "majorglyph"
                        If item_control.Name.ToLower.Contains("minor") Then tempSlotName = tempSlotName & "minorglyph"
                        If item_control.Name.ToLower.Contains("1") Then tempSlotName = tempSlotName & "1"
                        If item_control.Name.ToLower.Contains("2") Then tempSlotName = tempSlotName & "2"
                        If item_control.Name.ToLower.Contains("3") Then tempSlotName = tempSlotName & "3"
                        Dim txt As String = loadInfo(setId, tempSlotName, 0)
                        If Not txt Is Nothing Then
                            If txt.Length >= 30 Then
                                Dim ccremove As Integer = txt.Length - 28
                                txt = txt.Remove(28, ccremove) & "..."
                            End If
                            txt = txt.Replace("""", "")
                        End If
                        DirectCast(item_control, Label).Text = txt
                        DirectCast(item_control, Label).Tag = pubGlyph
                        DirectCast(item_control, Label).Cursor = Windows.Forms.Cursors.IBeam
                    End If
                Case TypeOf item_control Is PictureBox
                    If item_control.Name.ToLower.EndsWith("_pic") Then
                        Dim tempSlotName As String = ""
                        If item_control.Name.ToLower.StartsWith("sec_") Then tempSlotName = "sec"
                        If item_control.Name.ToLower.Contains("prim") Then tempSlotName = tempSlotName & "primglyph"
                        If item_control.Name.ToLower.Contains("major") Then tempSlotName = tempSlotName & "majorglyph"
                        If item_control.Name.ToLower.Contains("minor") Then tempSlotName = tempSlotName & "minorglyph"
                        If item_control.Name.ToLower.Contains("1") Then tempSlotName = tempSlotName & "1"
                        If item_control.Name.ToLower.Contains("2") Then tempSlotName = tempSlotName & "2"
                        If item_control.Name.ToLower.Contains("3") Then tempSlotName = tempSlotName & "3"
                        DirectCast(item_control, PictureBox).Image = loadInfo(setId, tempSlotName, 1)
                        DirectCast(item_control, PictureBox).Tag = pubGlyph
                    End If
            End Select
        Next
    End Sub
    Private Function loadInfo(ByVal targetSet As Integer, ByVal slot As String, ByVal infotype As Integer)
        Dim glyphitm As Glyph = GetTCI_Glyph(slot, targetSet)
        pubGlyph = glyphitm
        Select Case infotype
            Case 0 : Return glyphitm.name
            Case 1
                If glyphitm.image Is Nothing Then
                    Return My.Resources.empty
                Else
                    Return glyphitm.image
                End If
            Case Else : Return Nothing
        End Select
    End Function
End Class