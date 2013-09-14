Imports NCFramework

Public Class LayoutHandler
    Public Function doOperate_av(ByVal sender As Object, ByVal cnt As Integer) As String
        For Each m_form As Form In Application.OpenForms
            If m_form.Name = "Achievements_interface" Then
                Dim x As Achievements_interface = m_form
                ThreadExtensions.QueueUserWorkItem(New Func(Of Object, Integer, String)(AddressOf x.continueOperation), sender, cnt)
            End If
        Next
    End Function
    Public Function doOperate_qst(ByVal cnt As Integer, ByVal qsts As List(Of Quest)) As String
        For Each m_form As Form In Application.OpenForms
            If m_form.Name = "Quests_interface" Then
                Dim x As Quests_interface = m_form
                ThreadExtensions.QueueUserWorkItem(New Func(Of Integer, List(Of Quest), String)(AddressOf x.continueOperation), cnt, qsts)
            End If
        Next
    End Function
    Public Function doOperate_spellSkill(ByVal targetSetId As Integer) As String
        For Each m_form As Form In Application.OpenForms
            If m_form.Name = "SpellSkill_interface" Then
                Dim x As SpellSkill_interface = m_form
                ThreadExtensions.QueueUserWorkItem(New Func(Of Integer, String)(AddressOf x.continueOperation), targetSetId)
            End If
        Next
    End Function
End Class
