Module Logging
    Public Sub Log(msg As String, Optional loglevel As LogLevel = 0)
        Select Case loglevel
            Case LogLevel.NORMAL
                Console.ForegroundColor = ConsoleColor.Green
            Case LogLevel.INFO
                Console.ForegroundColor = ConsoleColor.Cyan
            Case LogLevel.WARNING
                Console.ForegroundColor = ConsoleColor.Yellow
            Case LogLevel.CRITICAL
                Console.ForegroundColor = ConsoleColor.Red
        End Select
        Console.WriteLine(msg)
    End Sub
    Public Enum LogLevel As Integer
        NORMAL = 0
        INFO = 1
        WARNING = 2
        CRITICAL = 3
    End Enum
End Module
