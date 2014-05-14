Imports NCFramework.Framework.Modules
Imports NCFramework.Framework.Extension
Imports NCFramework.Framework.Functions
Imports System.Net

Namespace Forms
    Public Class LoginExplorer

        '// Declaration
        Private _ptMouseDownLocation As Point
        '// Declaration      

        Public Overridable Sub me_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
            If e.Button = MouseButtons.Left Then
                _ptMouseDownLocation = e.Location
            End If
        End Sub

        Public Overridable Sub me_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
            If e.Button = MouseButtons.Left Then
                Location = New Point(e.Location.X - _ptMouseDownLocation.X + Location.X,
                                     e.Location.Y - _ptMouseDownLocation.Y + Location.Y)
            End If
        End Sub

        Private Sub ok_bt_Click(sender As Object, e As EventArgs) Handles ok_bt.Click
            If UsernameTextBox.Text.Length < 4 OrElse PasswordTextBox.Text.Length < 4 Then
                MsgBox(ResourceHandler.GetUserMessage("invalidCredentialsEntered"), MsgBoxStyle.Critical, ResourceHandler.GetUserMessage("attention"))
            Else
                Dim userName As String = UsernameTextBox.Text.ToLower()
                Dim passHash As String = ClsHash.HashString(userName & ":" & PasswordTextBox.Text, ClsHash.Hash.SHA256).ToLower()
                Dim client As New WebClient
                client.CheckProxy()
                Try
                    Dim result As String = client.DownloadString("http://wowgeslauncher.bplaced.net/filemanager/namcore/service/login.php?username=" & userName & "&passhash=" & passHash)
                    Dim resultCode As Integer = TryInt(SplitString(result, "<b>", "</b>"))
                    Select resultCode
                        Case 1
                            '// User not found
                            MsgBox(ResourceHandler.GetUserMessage("invalidInfo"), MsgBoxStyle.Critical, ResourceHandler.GetUserMessage("attention"))
                        Case 2
                            '// Wrong passHash
                            MsgBox(ResourceHandler.GetUserMessage("invalidInfo"), MsgBoxStyle.Critical, ResourceHandler.GetUserMessage("attention"))
                        Case 5
                            '// Information valid
                            GlobalVariables.Te_flags = TryInt(SplitString(result, "<flags>", "</flags>"))
                            If GlobalVariables.Te_flags <> 3 Then
                                GlobalVariables.Te_isLogin = True
                                GlobalVariables.Te_userName = userName
                                GlobalVariables.Te_passHash = passHash
                                For Each myForm As Form In Application.OpenForms
                                    If myForm.Name = "TemplateExplorer" Then
                                        DirectCast(myForm, TemplateExplorer).SetLogin(True, userName)
                                    End If
                                Next
                                Close()
                            Else
                                MsgBox(ResourceHandler.GetUserMessage("accountLocked"), MsgBoxStyle.Critical, ResourceHandler.GetUserMessage("attention"))
                                Close()
                            End If
                    End Select
                Catch ex As Exception
                    MsgBox(ResourceHandler.GetUserMessage("loginFailed"), MsgBoxStyle.Critical, ResourceHandler.GetUserMessage("attention"))
                End Try
            End If
        End Sub

        Private Sub cancel_bt_Click(sender As Object, e As EventArgs) Handles cancel_bt.Click
            Close()
        End Sub
    End Class
End Namespace