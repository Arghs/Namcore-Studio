Imports NCFramework

Public Class FlowLayoutPanelHandler
    Public Function doOperate_av(ByVal sender As Object, ByVal cnt As Integer) As String
        For Each m_form As Form In Application.OpenForms
            If m_form.Name = "Achievements_interface" Then
                Dim x As Achievements_interface = m_form
                ThreadExtensions.QueueUserWorkItem(New Func(Of Object, Integer, String)(AddressOf x.continueOperation), sender, cnt)
            End If
        Next
    End Function
    Public Function doOperate_qst(ByVal cnt As Integer, qsts() As String) As String
        For Each m_form As Form In Application.OpenForms
            If m_form.Name = "Quests_interface" Then
                Dim x As Quests_interface = m_form
                ThreadExtensions.QueueUserWorkItem(New Func(Of Integer, String(), String)(AddressOf x.continueOperation), cnt, qsts)
            End If
        Next
    End Function
End Class
