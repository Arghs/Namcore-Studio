Imports Namcore_Studio.Basics
Imports Namcore_Studio.SpellItem_Information
Imports Namcore_Studio.Conversions
Imports Namcore_Studio.GlobalVariables
Imports Namcore_Studio.EventLogging
Imports System.Threading
Public Class Achievements_interface
    Dim currentpage As Integer
    Dim tarsetid As Integer
    Dim lastindex As Integer
    Dim itmlst1 As New List(Of ListViewItem)
    Dim itmlst2 As New List(Of ListViewItem)
    Dim trd1 As New Thread(AddressOf loadpart1)
    Dim trd2 As New Thread(AddressOf loadpart2)
    Private Sub Achievements_interface_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Public Sub prepareInterface(ByVal setId As Integer)
        If tempAchievementInfo Is Nothing Then tempAchievementInfo = New List(Of ListViewItem)
        If tempAchievementInfoIndex Is Nothing Then tempAchievementInfoIndex = ""
        tarsetid = setId
        Dim player As Character = GetCharacterSetBySetId(tarsetid)
        trd1.IsBackground = True
        trd2.IsBackground = True
        If player.Achievements.Count > 50 Then
            trd1.Start()
            trd2.Start()
        Else
            trd1.Start()
        End If
    

    End Sub
    Private Sub loadpart1()
        Dim part1InfoIndex As String = tempAchievementInfoIndex
        Dim part1Info As List(Of ListViewItem) = tempAchievementInfo
        Dim player As Character = GetCharacterSetBySetId(tarsetid)
        Dim cnt As Integer = 0
        For Each av As Achievement In player.Achievements
            cnt += 1

            If Not part1InfoIndex.Contains("avid:" & av.Id.ToString & "|@") Then
                Try
                    Dim str(5) As String
                    str(0) = av.Id.ToString()
                    str(1) = GetAvNameById(av.Id)
                    str(2) = GetAvCategoryById(av.Id)
                    str(3) = GetAvCategoryById(av.Id, True)
                    str(4) = av.GainDate.toDate.ToString()
                    str(5) = GetAvDescriptionById(av.Id)
                    Dim itm As New ListViewItem(str)
                    itm.Tag = av
                    part1Info.Add(itm)
                    part1InfoIndex = part1InfoIndex & "[avid:" & av.Id.ToString & "|@" & (part1Info.Count - 1).ToString & "]"
                    itmlst1.Add(itm)
                Catch ex As Exception
                    LogAppend("loadprt1 ERROR: " & ex.ToString, "loadpart1", True)
                End Try

            Else

                Try
                    Dim itm As New ListViewItem
                    itm = part1Info.Item(TryInt(splitString(part1InfoIndex, "[avid:" & av.Id.ToString & "|@", "]")))
                    itmlst1.Add(itm)
                Catch ex As Exception
                    MsgBox(ex.ToString)
                End Try

            End If
            If cnt = 50 Then
                lastindex = 50
                nxt100_bt.Enabled = True
                Exit For
            End If
        Next
        For Each itm As ListViewItem In itmlst1
            avcompleted_lst.Items.Add(itm)
        Next
        tempAchievementInfo = part1Info
        tempAchievementInfoIndex = part1InfoIndex
        currentpage = 1
        resultcnt_lbl.Text = "Displaying " & cnt.ToString & " out of " & player.Achievements.Count.ToString() & " results!"
        Application.DoEvents()
        trd1.Abort()
    End Sub
    Private Sub loadpart2()
        Thread.Sleep(1000)
        Dim part2InfoIndex As String = tempAchievementInfoIndex
        Dim part2Info As List(Of ListViewItem) = tempAchievementInfo
        Dim player As Character = GetCharacterSetBySetId(tarsetid)
        Dim cnt As Integer = 0
        Dim index As Integer = 50
        Do
            If index >= player.Achievements.Count Then
                nxt100_bt.Enabled = False
                Exit Do
            End If
            Dim av As Achievement = player.Achievements(index)
            cnt += 1
            If Not part2InfoIndex.Contains("avid:" & av.Id.ToString & "|@") Then
                Try
                    Dim str(5) As String
                    str(0) = av.Id.ToString()
                    str(1) = GetAvNameById(av.Id)
                    str(2) = GetAvCategoryById(av.Id)
                    str(3) = GetAvCategoryById(av.Id, True)
                    str(4) = av.GainDate.toDate.ToString()
                    str(5) = GetAvDescriptionById(av.Id)
                    Dim itm As New ListViewItem(str)
                    itm.Tag = av
                    part2Info.Add(itm)
                    part2InfoIndex = part2InfoIndex & "[avid:" & av.Id.ToString & "|@" & (part2Info.Count - 1).ToString & "]"
                    itmlst2.Add(itm)
                Catch ex As Exception
                    LogAppend("loadprt2 ERROR: " & ex.ToString, "loadpart1", True)
                End Try

            Else
                Try

                    Dim itm As New ListViewItem
                    itm = part2Info.Item(TryInt(splitString(part2InfoIndex, "[avid:" & av.Id.ToString & "|@", "]")))
                    itmlst2.Add(itm)
                Catch ex As Exception
                    MsgBox(ex.ToString)
                End Try

            End If
            If cnt = 50 Then
                nxt100_bt.Enabled = True
                Exit Do
            End If
            index += 1

        Loop
        While trd1.IsAlive

        End While
        For Each itm As ListViewItem In itmlst2
            avcompleted_lst.Items.Add(itm)
        Next
        lastindex = index
        tempAchievementInfo.AddRange(part2Info)
        tempAchievementInfoIndex = tempAchievementInfoIndex & part2InfoIndex
        resultcnt_lbl.Text = "Displaying " & lastindex.ToString & " out of " & player.Achievements.Count.ToString() & " results!"
        trd2.Abort()
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
        resultcnt_lbl.Text = "Displaying " & lastindex.ToString & " out of " & player.Achievements.Count.ToString() & " results!"
    End Sub
End Class