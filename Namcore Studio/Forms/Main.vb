Imports Namcore_Studio.GlobalVariables
Public Class Main

    Private Sub highlighter_MouseEnter(sender As Object, e As EventArgs) Handles highlighter1.MouseEnter, highlighter2.MouseEnter, highlighter3.MouseEnter
        sender.backgroundimage = My.Resources.highlight
    End Sub

    Private Sub highlighter_MouseLeave(sender As Object, e As EventArgs) Handles highlighter1.MouseLeave, highlighter2.MouseLeave, highlighter3.MouseLeave
        sender.backgroundimage = Nothing
    End Sub


    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "NamCore Studio - Development - " & My.Application.Info.Version.ToString() & " - © megasus 2013"
        lastregion = "main"
    End Sub

    Private Sub highlighter1_Click(sender As Object, e As EventArgs) Handles highlighter1.Click
        Me.Hide()
        Live_View.Show()
    End Sub

    Private Sub highlighter3_Click(sender As Object, e As EventArgs) Handles highlighter3.Click
        Me.Hide()
        Armory_interface.Show()
    End Sub
End Class