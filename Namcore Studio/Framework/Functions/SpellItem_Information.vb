Imports Namcore_Studio.GlobalVariables
Imports Namcore_Studio.Basics
Imports Namcore_Studio.EventLogging
Imports System.Net
Public Class SpellItem_Information
    Public Shared Function GetGlyphIdByItemId(ByVal itemid As Integer) As Integer
        LogAppend("Loading GlyphId by ItemId " & itemid.ToString, "SpellItem_Information_GetGlyphIdByItemId", False)
        Dim xpacressource As String
        Try
            Select Case expansion
                Case 3
                    xpacressource = My.Resources.GlyphProperties_335
                Case 4
                    xpacressource = My.Resources.GlyphProperties_434
                Case Else
                    xpacressource = My.Resources.GlyphProperties_335
            End Select
            Dim client As New WebClient
            Return CInt(splitString(client.DownloadString("http://www.wowhead.com/spell=" & splitString(xpacressource, "<entry>" & itemid.ToString & "</entry><spell>", "</spell>")), ",""id"":", ",""level"""))
        Catch ex As Exception
            LogAppend("Error while loading GlyphId! -> Returning 0 -> Exception is: ###START###" & ex.ToString() & "###END###", "SpellItem_Information_GetGlyphIdByItemId", False, True)
            Return 0
        End Try
    End Function
    Public Shared Sub LoadWeaponType(ByVal itemid As Integer, ByVal tar_set As Integer)
        If Not itemid = 0 Then
            LogAppend("Loading weapon type of Item " & itemid.ToString, "SpellItem_Information_LoadWeaponType", False)
            Try
                Dim client As New WebClient
                Dim excerpt As String = splitString(client.DownloadString("http://www.wowhead.com/item=" & itemid.ToString & "&xml"), "<subclass id=", "</subclass>")
                Select Case True
                    Case excerpt.ToLower.Contains(" crossbow ")
                        AppendTemporaryCharacterInformation("@character_specialspells", "5011", tar_set)
                        AppendTemporaryCharacterInformation("@character_specialskills", "226", tar_set)
                    Case excerpt.ToLower.Contains(" bow ")
                        AppendTemporaryCharacterInformation("@character_specialspells", "264", tar_set)
                        AppendTemporaryCharacterInformation("@character_specialskills", "45", tar_set)
                    Case excerpt.ToLower.Contains(" gun ")
                        AppendTemporaryCharacterInformation("@character_specialspells", "266", tar_set)
                        AppendTemporaryCharacterInformation("@character_specialskills", "46", tar_set)
                    Case excerpt.ToLower.Contains(" thrown ")
                        AppendTemporaryCharacterInformation("@character_specialspells", "2764", tar_set)
                        AppendTemporaryCharacterInformation("@character_specialspells", "2567", tar_set)
                        AppendTemporaryCharacterInformation("@character_specialskills", "176", tar_set)
                    Case excerpt.ToLower.Contains(" wands ")
                        AppendTemporaryCharacterInformation("@character_specialspells", "5009", tar_set)
                        AppendTemporaryCharacterInformation("@character_specialspells", "5019", tar_set)
                        AppendTemporaryCharacterInformation("@character_specialskills", "228", tar_set)
                    Case excerpt.ToLower.Contains(" sword ")
                        If excerpt.ToLower.Contains(" one-handed ") Then
                            AppendTemporaryCharacterInformation("@character_specialspells", "201", tar_set)
                            AppendTemporaryCharacterInformation("@character_specialskills", "43", tar_set)
                        Else
                            AppendTemporaryCharacterInformation("@character_specialspells", "201", tar_set)
                            AppendTemporaryCharacterInformation("@character_specialskills", "43", tar_set)
                            AppendTemporaryCharacterInformation("@character_specialspells", "202", tar_set)
                            AppendTemporaryCharacterInformation("@character_specialskills", "55", tar_set)
                        End If
                    Case excerpt.ToLower.Contains(" dagger ")
                        AppendTemporaryCharacterInformation("@character_specialspells", "1180", tar_set)
                        AppendTemporaryCharacterInformation("@character_specialskills", "173", tar_set)
                    Case excerpt.ToLower.Contains(" axe ")
                        If excerpt.ToLower.Contains(" one-handed ") Then
                            AppendTemporaryCharacterInformation("@character_specialspells", "196", tar_set)
                            AppendTemporaryCharacterInformation("@character_specialskills", "44", tar_set)
                        Else
                            AppendTemporaryCharacterInformation("@character_specialspells", "197", tar_set)
                            AppendTemporaryCharacterInformation("@character_specialskills", "44", tar_set)
                            AppendTemporaryCharacterInformation("@character_specialspells", "196", tar_set)
                            AppendTemporaryCharacterInformation("@character_specialskills", "142", tar_set)
                        End If
                    Case excerpt.ToLower.Contains(" mace ")
                        If excerpt.ToLower.Contains(" one-handed ") Then
                            AppendTemporaryCharacterInformation("@character_specialspells", "198", tar_set)
                            AppendTemporaryCharacterInformation("@character_specialskills", "54", tar_set)
                        Else
                            AppendTemporaryCharacterInformation("@character_specialskills", "54", tar_set)
                            AppendTemporaryCharacterInformation("@character_specialspells", "198", tar_set)
                            AppendTemporaryCharacterInformation("@character_specialskills", "160", tar_set)
                            AppendTemporaryCharacterInformation("@character_specialspells", "199", tar_set)
                        End If
                    Case excerpt.ToLower.Contains(" polearm ")
                        AppendTemporaryCharacterInformation("@character_specialspells", "200", tar_set)
                        AppendTemporaryCharacterInformation("@character_specialskills", "229", tar_set)
                    Case excerpt.ToLower.Contains(" staff ")
                        AppendTemporaryCharacterInformation("@character_specialspells", "227", tar_set)
                        AppendTemporaryCharacterInformation("@character_specialskills", "136", tar_set)
                    Case Else : End Select
            Catch ex As Exception
                LogAppend("Error while loading weapon type! -> Exception is: ###START###" & ex.ToString() & "###END###", "SpellItem_Information_LoadWeaponType", False, True)
            End Try
        Else

        End If
    End Sub
End Class
