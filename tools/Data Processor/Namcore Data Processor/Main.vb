
Module Main
    Public AvDic As Dictionary(Of Integer, String())

    Sub InitDics()
        AvDic = New Dictionary(Of Integer, String())()
        AvDic.Add(7, {"string", "name"})
    End Sub
    Sub Main()
        InitDics()
        Dim avTable As DataTable = ReadDb("Achievement.dbc", AvDic)
        Console.Read()

    End Sub

End Module
