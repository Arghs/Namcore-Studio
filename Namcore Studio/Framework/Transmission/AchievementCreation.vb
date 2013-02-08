Imports Namcore_Studio.EventLogging
Imports Namcore_Studio.CommandHandler
Imports Namcore_Studio.GlobalVariables
Imports Namcore_Studio.Basics
Imports Namcore_Studio.Conversions
Public Class AchievementCreation
    Public Shared Sub SetCharacterAchievements(ByVal setId As Integer, Optional charguid As Integer = 0)
        If charguid = 0 Then charguid = characterGUID
        LogAppend("Setting Achievements for character: " & charguid.ToString() & " // setId is : " & setId.ToString(), "AchievementCreation_SetCharacterAchievements", True)
        Dim characterAvList As List(Of String) = ConvertStringToList(GetTemporaryCharacterInformation("@character_achievement_list", setId))
        For Each avstring As String In characterAvList
            runSQLCommand_characters_string("INSERT INTO character_achievement ( guid, achievement, date ) VALUES ( '" & charguid.ToString() & "', '" & splitList(avstring, "av") & "', '" &
                                            splitList(avstring, "date") & "')")
        Next
    End Sub
    Public Shared Sub AddCharacterAchievement(ByVal setId As Integer, ByVal avid As Integer, Optional charguid As Integer = 0)
        If charguid = 0 Then charguid = characterGUID
        LogAppend("Adding Achievement " & avid.ToString() & " for character: " & charguid.ToString() & " // setId is : " & setId.ToString(), "AchievementCreation_AddCharacterAchievement", True)
        Dim characterAvList As List(Of String) = ConvertStringToList(GetTemporaryCharacterInformation("@character_achievement_list", setId))
        runSQLCommand_characters_string("INSERT INTO character_achievement ( guid, achievement, date ) VALUES ( '" & charguid.ToString() & "', '" & avid.ToString() & "', '" &
                                        (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds.ToString() & "')")
    End Sub
End Class
