Imports Namcore_Studio.Basics
Imports Namcore_Studio.Conversions
Public Class Quests_interface

    Public Sub prepareInterface(ByVal setId As Integer)
        Dim p As Character = GetCharacterSetBySetId(setId)
        Dim qst() As String = p.FinishedQuests.Split(","c)
        Dim cnt As Integer = 0
        While cnt < qst.Length
            Dim str(3) As String
            str(0) = qst(cnt)
            str(1) = getNameOfQuest(str(0))
            str(2) = "1"
            str(3) = "1"
            Dim itm As New ListViewItem(str)
            itm.Tag = TryInt(str(0))
            qst_lst.Items.Add(itm)
            cnt += 1
        End While
    End Sub
    Private Sub Quests_interface_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class