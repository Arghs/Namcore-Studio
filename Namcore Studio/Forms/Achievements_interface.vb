Imports Namcore_Studio.Basics
Imports Namcore_Studio.SpellItem_Information
Imports Namcore_Studio.Conversions


Public Class Achievements_interface
    Dim currentpage As Integer
    Dim tarsetid As Integer
    Dim lastindex As Integer
    Private Sub Achievements_interface_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Public Sub prepareInterface(ByVal setId As Integer)
        tarsetid = setId
        Dim player As Character = GetCharacterSetBySetId(setId)
        Dim cnt As Integer = 0
        For Each av As Achievement In player.Achievements
            cnt += 1
            Dim str(5) As String
            str(0) = av.Id.ToString()
            str(1) = GetAvNameById(av.Id)
            str(2) = GetAvCategoryById(av.Id)
            str(3) = GetAvCategoryById(av.Id, True)
            str(4) = av.GainDate.toDate.ToString()
            str(5) = GetAvDescriptionById(av.Id)
            Dim itm As New ListViewItem(str)
            itm.Tag = av
            avcompleted_lst.Items.Add(itm)
            If cnt = 100 Then
                lastindex = 100
                nxt100_bt.Enabled = True
                Exit For
            End If
        Next
        currentpage = 1
        resultcnt_lbl.Text = "Displaying " & cnt.ToString & " out of " & player.Achievements.Count.ToString() & " results!"
    End Sub

    Private Sub nxt100_bt_Click(sender As Object, e As EventArgs) Handles nxt100_bt.Click
        Dim player As Character = GetCharacterSetBySetId(tarsetid)
        Dim cnt As Integer = 0
        Dim index As Integer = lastindex
        prev100_bt.Enabled = True
        Do
            If index >= player.Achievements.Count Then
                nxt100_bt.Enabled = False
                Exit Do
            End If
            Dim av As Achievement = player.Achievements(index)
            cnt += 1
            Dim str(5) As String
            str(0) = av.Id.ToString()
            str(1) = GetAvNameById(av.Id)
            str(2) = GetAvCategoryById(av.Id)
            str(3) = GetAvCategoryById(av.Id, True)
            str(4) = av.GainDate.toDate.ToString()
            str(5) = GetAvDescriptionById(av.Id)
            Dim itm As New ListViewItem(str)
            itm.Tag = av
            avcompleted_lst.Items.Add(itm)
            If cnt = 100 Then
                lastindex = index
                nxt100_bt.Enabled = True
                Exit Do
            End If
            index += 1
        Loop
        currentpage += 1
        resultcnt_lbl.Text = "Displaying " & cnt.ToString & " out of " & player.Achievements.Count.ToString() & " results!"
    End Sub
End Class