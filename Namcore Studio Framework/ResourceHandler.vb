Public Class ResourceHandler
    Public Shared Function GetUserMessage(ByVal field As String)
        Try
            Dim rm As New Resources.ResourceManager("Namcore_Studio_Framework.UserMessages", System.Reflection.Assembly.GetExecutingAssembly())
            Return rm.GetString(field)
        Catch ex As Exception
            Return ""
        End Try

    End Function
End Class
