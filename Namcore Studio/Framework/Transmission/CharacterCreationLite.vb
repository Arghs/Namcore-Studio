'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013 Namcore Studio <https://github.com/megasus/Namcore-Studio>
'*
'* This program is free software; you can redistribute it and/or modify it
'* under the terms of the GNU General Public License as published by the
'* Free Software Foundation; either version 2 of the License, or (at your
'* option) any later version.
'*
'* This program is distributed in the hope that it will be useful, but WITHOUT
'* ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
'* FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for
'* more details.
'*
'* You should have received a copy of the GNU General Public License along
'* with this program. If not, see <http://www.gnu.org/licenses/>.
'*
'* Developed by Alcanmage/megasus
'*
'* //FileInfo//
'*      /Filename:      CharacterCreationLite
'*      /Description:   Includes functions for creating a new character which has been
'*                      parsed from the wow armory
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Imports Namcore_Studio.EventLogging
Imports Namcore_Studio.Basics
Imports Namcore_Studio.GlobalVariables
Imports Namcore_Studio.Conversions
Imports Namcore_Studio.CommandHandler
Imports Namcore_Studio.SpellCreation
Imports Namcore_Studio.SkillCreation
Imports MySql.Data.MySqlClient
Public Class CharacterCreationLite
    Public Shared Sub CreateNewLiteCharacter(ByVal charname As String, ByVal accountName As String, ByVal setId As Integer, Optional forceNameChange As Boolean = False)
        LogAppend("Creating new character: " & charname & " for account : " & accountName, "CharacterCreationLite_CreateNewLiteCharacter", True)
        Select Case targetCore
            Case "arcemu"
                createAtArcemu(charname, accountName, setId, forceNameChange)
            Case "trinity"
                createAtTrinity(charname, accountName, setId, forceNameChange)
            Case "trinitytbc"

            Case "mangos"
                createAtMangos(charname, accountName, setId, forceNameChange)
            Case Else

        End Select
    End Sub
    Public Shared Function CharacterExist(ByVal charname As String) As Boolean
        Select Case targetCore
            Case "arcemu"
                If TryInt(ReturnResultCount("SELECT * FROM characters WHERE name='" & charname & "'", True)) = 0 Then Return False Else Return True
            Case "trinity"
                If TryInt(ReturnResultCount("SELECT * FROM characters WHERE name='" & charname & "'", True)) = 0 Then Return False Else Return True
            Case "trinitytbc"
                If TryInt(ReturnResultCount("SELECT * FROM characters WHERE name='" & charname & "'", True)) = 0 Then Return False Else Return True
            Case "mangos"
                If TryInt(ReturnResultCount("SELECT * FROM characters WHERE name='" & charname & "'", True)) = 0 Then Return False Else Return True
            Case Else : End Select
    End Function

    Private Shared Sub createAtArcemu(ByVal charactername As String, ByVal accname As String, ByVal targetSetId As Integer, ByVal NameChange As Boolean)
        LogAppend("Creating at arcemu", "CharacterCreationLite_createAtArcemu", False)
        Dim newcharguid As Integer = TryInt(runSQLCommand_characters_string("SELECT guid FROM characters WHERE guid=(SELECT MAX(guid) FROM characters)", True)) + 1
        Dim accid As Integer = TryInt(runSQLCommand_realm_string("SELECT acct FROM accounts WHERE login='" & accname & "'", True))
        Const sqlstring As String = "INSERT INTO characters ( `acct`, `guid`, `name`, `race`, `class`, `gender`, `level`, `xp`, `gold`, current_hp, `bytes`, `positionX`, " &
                                    "positionY, positionZ, orientation, mapId, taximask, playedtime ) " &
                                    "VALUES ( @accid, @guid, @name, @race, @class, @gender, @level, '0', '0', '1000', @pBytes, '-14305.7', '514.08', '10', '4.30671', '0', '0 0 0 0 0 0 0 0 0 0 0 0 ', '98 98 5 ' )"
        Dim tempcommand As New MySqlCommand(sqlstring, TargetConnection_Realm)
        tempcommand.Parameters.AddWithValue("@accid", accid.ToString())
        tempcommand.Parameters.AddWithValue("@guid", newcharguid.ToString())
        tempcommand.Parameters.AddWithValue("@name", charactername)
        tempcommand.Parameters.AddWithValue("@class", GetTemporaryCharacterInformation("@character_class", targetSetId))
        tempcommand.Parameters.AddWithValue("@race", GetTemporaryCharacterInformation("@character_race", targetSetId))
        tempcommand.Parameters.AddWithValue("@gender", GetTemporaryCharacterInformation("@character_gender", targetSetId))
        tempcommand.Parameters.AddWithValue("@level", GetTemporaryCharacterInformation("@character_level", targetSetId))
        tempcommand.Parameters.AddWithValue("@pBytes", GetTemporaryCharacterInformation("@character_playerBytes", targetSetId))
        Try
            tempcommand.ExecuteNonQuery()
            If NameChange = True Then
                runSQLCommand_characters_string("UPDATE characters SET forced_rename_pending='1' WHERE guid='" & newcharguid.ToString & "'", True)
            Else
                If CharacterExist(charactername) = True Then
                    runSQLCommand_characters_string("UPDATE characters SET forced_rename_pending='1' WHERE guid='" & newcharguid.ToString & "'", True)
                End If
            End If
            'Creating hearthstone
            LogAppend("Creating character hearthstone", "CharacterCreationLite_createAtArcemu", False)
            Dim newitemguid As Integer = (TryInt(runSQLCommand_characters_string("SELECT guid FROM playeritems WHERE guid=(SELECT MAX(guid) FROM playeritems)", True)) + 1)
            runSQLCommand_characters_string("INSERT INTO playeritems ( ownerguid, guid, entry, flags, containerslot, slot ) VALUES ( '" & accid.ToString() &
                "', '" & newitemguid.ToString() & "', '6948', '1', '-1', '23' )", True)
            'Adding special skills & spells
            AddSpells("6603,")
            Dim cRace As Integer = TryInt(GetTemporaryCharacterInformation("@character_race", targetSetId))
            Dim cClass As Integer = TryInt(GetTemporaryCharacterInformation("@character_class", targetSetId))
            If cRace = 1 Then
                If cClass = 1 Then
                    AddSkills("26,1,1,43,1,5,54,1,5,55,1,5,95,1,5,162,1,5,413,1,1,414,1,1,415,1,1,433,1,1,754,1,1,")
                    AddSpells("78,81,107,196,198,201,202,203,204,522,668,2382,2457,2479,3050,3127,3365,5301,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,8737,9077,9078,9116,9125,20597,20598,20599,20864,21651,21652,22027,22810,58985,59752,")
                ElseIf cClass = 2 Then
                    AddSkills("54,1,5,95,1,5,160,1,5,162,1,5,413,1,1,414,1,1,415,1,1,433,1,1,594,1,1,754,1,1,")
                    AddSpells("81,107,198,199,203,204,522,635,668,2382,2479,3050,3127,3365,5301,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,8737,9077,9078,9116,9125,20597,20598,20599,20864,21084,21651,21652,22027,22810,27762,34082,58985,59752,")
                ElseIf cClass = 4 Then
                    AddSkills("38,1,1,95,1,5,162,1,5,173,1,5,176,1,5,253,1,1,414,1,1,415,1,1,754,1,1,")
                    AddSpells("81,203,204,522,668,674,1180,1752,2098,2382,2479,2567,2764,3050,3127,3365,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,9077,9078,9125,16092,20597,20598,20599,20864,21184,21651,21652,22027,22810,58985,59752,")
                ElseIf cClass = 5 Then
                    AddSkills("54,1,5,56,1,1,95,1,5,136,1,5,162,1,5,228,1,5,415,1,1,754,1,1,")
                    AddSpells("81,198,203,204,227,522,585,668,2050,2382,2479,3050,3127,3365,5009,5019,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,9078,9125,20597,20598,20599,20864,21651,21652,22027,22810,58985,59752,")
                ElseIf cClass = 6 Then
                    AddSkills("43,270,275,44,270,275,55,270,275,95,270,275,129,270,300,162,270,275,172,270,275,229,270,275,293,1,1,413,1,1,414,1,1,415,1,1,754,1,1,762,150,150,770,1,1,771,1,1,772,1,1,")
                    AddSpells("81,196,197,200,201,202,203,204,522,668,674,750,2382,2479,3050,3127,3275,3276,3277,3278,3365,6233,6246,6247,6477,6478,6603,7266,7267,7355,7928,7929,7934,8386,8737,9077,9078,9125,10840,10841,10846,18629,18630,20597,20598,20599,20864,21651,21652,22027,22810,33391,45462,45477,45902,47541,48266,49410,49576,52665,58985,59752,59879,59921,61455,")
                ElseIf cClass = 8 Then
                    AddSkills("43,270,275,44,270,275,55,270,275,95,270,275,118,1,1,129,270,300,162,270,275,172,270,275,183,1,1,229,270,275,293,1,1,413,1,1,414,1,1,415,1,1,754,1,1,762,150,150,770,1,1,771,1,1,772,1,1,")
                    AddSpells("81,196,197,200,201,202,203,204,522,668,669,670,671,672,674,750,813,2382,2479,3050,3127,3275,3276,3277,3278,3365,6233,6246,6247,6477,6478,6603,7266,7267,7341,7355,7928,7929,7934,8386,8737,9077,9078,9125,10840,10841,10846,17737,18629,18630,20597,20598,20599,20864,21651,21652,22027,22810,29932,33391,45462,45477,45902,47541,48266,49410,49576,52665,58985,59752,59879,59921,61455,")
                ElseIf cClass = 9 Then
                    AddSkills("43,270,275,44,270,275,55,270,275,95,270,275,118,1,1,129,270,300,162,270,275,172,270,275,183,1,1,229,270,275,293,1,1,413,1,1,414,1,1,415,1,1,754,1,1,762,150,150,770,1,1,771,1,1,772,1,1,")
                    AddSpells("81,196,197,200,201,202,203,204,522,668,669,670,671,672,674,750,813,2382,2479,3050,3127,3275,3276,3277,3278,3365,6233,6246,6247,6477,6478,6603,7266,7267,7341,7355,7928,7929,7934,8386,8737,9077,9078,9125,10840,10841,10846,17737,18629,18630,20597,20598,20599,20864,21651,21652,22027,22810,29932,33391,45462,45477,45902,47541,48266,49410,49576,52665,58985,59752,59879,59921,61455,")
                End If
            ElseIf cRace = 2 Then
                If cClass = 1 Then
                    AddSkills("26,1,1,43,1,5,44,1,5,55,1,5,95,1,5,125,1,1,162,1,5,172,1,5,183,1,1,413,1,1,414,1,1,415,1,1,433,1,1,")
                    AddSpells("78,81,107,196,197,201,202,203,204,522,668,669,670,671,672,813,2382,2457,2479,3050,3127,3365,5301,6233,6246,6247,6477,6478,6603,7266,7267,7341,7355,8386,8737,9077,9078,9116,9125,17737,20572,20573,20574,21651,21652,22027,22810,29932,54562,")
                ElseIf cClass = 3 Then
                    AddSkills("44,1,5,45,1,5,51,1,1,95,1,5,125,1,1,162,1,5,163,1,1,172,1,5,414,1,1,415,1,1,")
                    AddSpells("78,81,107,196,197,201,202,203,204,522,668,669,670,671,672,813,2382,2457,2479,3050,3127,3365,5301,6233,6246,6247,6477,6478,6603,7266,7267,7341,7355,8386,8737,9077,9078,9116,9125,17737,20572,20573,20574,21651,21652,22027,22810,29932,54562,")
                ElseIf cClass = 4 Then
                    AddSkills("38,1,1,95,1,5,125,1,1,162,1,5,173,1,5,176,1,5,253,1,1,414,1,1,415,1,1,")
                    AddSpells("81,203,204,522,669,674,1180,1752,2098,2382,2479,2567,2764,3050,3127,3365,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,9077,9078,9125,16092,20572,20573,20574,21184,21651,21652,22027,22810,54562,")
                ElseIf cClass = 6 Then
                    AddSkills("43,270,275,44,270,275,55,270,275,95,270,275,125,1,1,129,270,300,162,270,275,172,270,275,229,270,275,293,1,1,413,1,1,414,1,1,415,1,1,762,150,150,770,1,1,771,1,1,772,1,1,")
                    AddSpells("81,196,197,200,201,202,203,204,522,669,674,750,2382,2479,3050,3127,3275,3276,3277,3278,3365,6233,6246,6247,6477,6478,6603,7266,7267,7355,7928,7929,7934,8386,8737,9077,9078,9125,10840,10841,10846,18629,18630,20572,20573,20574,21651,21652,22027,22810,33391,45462,45477,45902,47541,48266,49410,49576,52665,54562,59879,59921,61455,")
                ElseIf cClass = 7 Then
                    AddSkills("54,1,5,95,1,5,125,1,1,136,1,5,162,1,5,375,1,1,414,1,1,415,1,1,433,1,1,573,1,1,")
                    AddSpells("81,107,198,203,204,227,331,403,522,669,2382,2479,3050,3127,3365,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,9077,9078,9116,9125,20573,20574,21651,21652,22027,22810,27763,33697,54562,")
                ElseIf cClass = 9 Then
                    AddSkills("95,1,5,125,1,1,136,1,5,162,1,5,173,1,5,228,1,5,354,1,1,415,1,1,593,1,1,")
                    AddSpells("81,203,204,227,522,669,686,687,1180,2382,2479,3050,3127,3365,5009,5019,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,9078,9125,20573,20574,21651,21652,22027,22810,33702,54562,")
                End If
            ElseIf cRace = 3 Then
                If cClass = 1 Then
                    AddSkills("26,1,1,44,1,5,54,1,5,55,1,5,95,1,5,101,1,1,162,1,5,172,1,5,413,1,1,414,1,1,415,1,1,433,1,1,")
                    AddSpells("78,81,107,196,197,198,202,203,204,522,668,672,2382,2457,2479,2481,3050,3127,3365,5301,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,8737,9077,9078,9116,9125,20594,20595,20596,21651,21652,22027,22810,59224,")
                ElseIf cClass = 2 Then
                    AddSkills("54,1,5,95,1,5,101,1,1,160,1,5,162,1,5,413,1,1,414,1,1,415,1,1,433,1,1,594,1,1,")
                    AddSpells("81,107,198,199,203,204,522,635,668,672,2382,2479,2481,3050,3127,3365,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,8737,9077,9078,9116,9125,20594,20595,20596,21084,21651,21652,22027,22810,27762,34082,59224,")
                ElseIf cClass = 3 Then
                    AddSkills("44,1,5,46,1,5,51,1,1,95,1,5,101,1,1,162,1,5,163,1,1,172,1,5,414,1,1,415,1,1,")
                    AddSpells("75,81,196,197,203,204,266,522,668,672,2382,2479,2481,2973,3018,3050,3127,3365,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,9077,9078,9125,13358,20594,20595,20596,21651,21652,22027,22810,24949,59224,")
                ElseIf cClass = 4 Then
                    AddSkills("38,1,1,95,1,5,101,1,1,162,1,5,173,1,5,176,1,5,253,1,1,414,1,1,415,1,1,")
                    AddSpells("81,203,204,522,668,672,674,1180,1752,2098,2382,2479,2481,2567,2764,3050,3127,3365,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,9077,9078,9125,16092,20594,20595,20596,21184,21651,21652,22027,22810,59224,")
                ElseIf cClass = 5 Then
                    AddSkills("54,1,5,56,1,1,95,1,5,101,1,1,136,1,5,162,1,5,228,1,5,415,1,1,")
                    AddSpells("81,198,203,204,227,522,585,668,672,2050,2382,2479,2481,3050,3127,3365,5009,5019,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,9078,9125,20594,20595,20596,21651,21652,22027,22810,59224,")
                ElseIf cClass = 6 Then
                    AddSkills("43,270,275,44,270,275,55,270,275,95,270,275,101,1,1,129,270,300,162,270,275,172,270,275,229,270,275,293,1,1,413,1,1,414,1,1,415,1,1,762,150,150,770,1,1,771,1,1,772,1,1,")
                    AddSpells("81,196,197,200,201,202,203,204,522,668,672,674,750,2382,2479,2481,3050,3127,3275,3276,3277,3278,3365,6233,6246,6247,6477,6478,6603,7266,7267,7355,7928,7929,7934,8386,8737,9077,9078,9125,10840,10841,10846,18629,18630,20594,20595,20596,21651,21652,22027,22810,33391,45462,45477,45902,47541,48266,49410,49576,52665,59224,59879,59921,61455,")
                End If
            ElseIf cRace = 4 Then
                If cClass = 1 Then
                    AddSkills("26,1,1,43,1,5,54,1,5,55,1,5,95,1,5,126,1,1,162,1,5,173,1,5,413,1,1,414,1,1,415,1,1,433,1,1,")
                    AddSpells("78,81,107,198,201,202,203,204,522,668,671,1180,2382,2457,2479,3050,3127,3365,5301,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,8737,9077,9078,9116,9125,20582,20583,20585,21009,21651,21652,22027,22810,58984,")
                ElseIf cClass = 3 Then
                    AddSkills("45,1,5,51,1,1,95,1,5,126,1,1,162,1,5,163,1,1,172,1,5,173,1,5,414,1,1,415,1,1,")
                    AddSpells("75,81,197,203,204,264,522,668,671,1180,2382,2479,2973,3018,3050,3127,3365,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,9077,9078,9125,13358,20582,20583,20585,21009,21651,21652,22027,22810,24949,58984,")
                ElseIf cClass = 4 Then
                    AddSkills("38,1,1,95,1,5,126,1,1,162,1,5,173,1,5,176,1,5,253,1,1,414,1,1,415,1,1,")
                    AddSpells("81,203,204,522,668,671,674,1180,1752,2098,2382,2479,2567,2764,3050,3127,3365,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,9077,9078,9125,16092,20582,20583,20585,21009,21184,21651,21652,22027,22810,58984,")
                ElseIf cClass = 5 Then
                    AddSkills("54,1,5,56,1,1,95,1,5,126,1,1,136,1,5,162,1,5,228,1,5,415,1,1,")
                    AddSpells("81,198,203,204,227,522,585,668,671,2050,2382,2479,3050,3127,3365,5009,5019,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,9078,9125,20582,20583,20585,21009,21651,21652,22027,22810,58984,")
                ElseIf cClass = 6 Then
                    AddSkills("43,270,275,44,270,275,55,270,275,95,270,275,126,1,1,129,270,300,162,270,275,172,270,275,229,270,275,293,1,1,413,1,1,414,1,1,415,1,1,762,150,150,770,1,1,771,1,1,772,1,1,")
                    AddSpells("81,196,197,200,201,202,203,204,522,668,671,674,750,2382,2479,3050,3127,3275,3276,3277,3278,3365,6233,6246,6247,6477,6478,6603,7266,7267,7355,7928,7929,7934,8386,8737,9077,9078,9125,10840,10841,10846,18629,18630,20582,20583,20585,21009,21651,21652,22027,22810,33391,45462,45477,45902,47541,48266,49410,49576,52665,58984,59879,59921,61455,")
                ElseIf cClass = 11 Then
                    AddSkills("95,1,5,126,1,1,136,1,5,162,1,5,173,1,5,414,1,1,415,1,1,573,1,1,574,1,1,")
                    AddSpells("81,203,204,227,522,668,671,1180,2382,2479,3050,3127,3365,5176,5185,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,9077,9078,9125,20582,20583,20585,21009,21651,21652,22027,22810,27764,58984,")
                End If
            ElseIf cRace = 5 Then
                If cClass = 1 Then
                    AddSkills("26,1,1,43,1,5,55,1,5,95,1,5,162,1,5,173,1,5,220,1,1,413,1,1,414,1,1,415,1,1,433,1,1,")
                    AddSpells("78,81,107,201,202,203,204,522,669,1180,2382,2457,2479,3050,3127,3365,5227,5301,6233,6246,6247,6477,6478,6603,7266,7267,7355,7744,8386,8737,9077,9078,9116,9125,17737,20577,20579,21651,21652,22027,22810,")
                ElseIf cClass = 4 Then
                    AddSkills("38,1,1,95,1,5,162,1,5,173,1,5,176,1,5,220,1,1,253,1,1,414,1,1,415,1,1,")
                    AddSpells("81,203,204,522,669,674,1180,1752,2098,2382,2479,2567,2764,3050,3127,3365,5227,6233,6246,6247,6477,6478,6603,7266,7267,7355,7744,8386,9077,9078,9125,16092,17737,20577,20579,21184,21651,21652,22027,22810,")
                ElseIf cClass = 5 Then
                    AddSkills("54,1,5,56,1,1,95,1,5,136,1,5,162,1,5,220,1,1,228,1,5,415,1,1,")
                    AddSpells("81,198,203,204,227,522,585,669,2050,2382,2479,3050,3127,3365,5009,5019,5227,6233,6246,6247,6477,6478,6603,7266,7267,7355,7744,8386,9078,9125,17737,20577,20579,21651,21652,22027,22810,")
                ElseIf cClass = 6 Then
                    AddSkills("43,270,275,44,270,275,55,270,275,95,270,275,129,270,300,162,270,275,172,270,275,220,1,1,229,270,275,293,1,1,413,1,1,414,1,1,415,1,1,762,150,150,770,1,1,771,1,1,772,1,1,")
                    AddSpells("81,196,197,200,201,202,203,204,522,669,674,750,2382,2479,3050,3127,3275,3276,3277,3278,3365,5227,6233,6246,6247,6477,6478,6603,7266,7267,7355,7744,7928,7929,7934,8386,8737,9077,9078,9125,10840,10841,10846,17737,18629,18630,20577,20579,21651,21652,22027,22810,33391,45462,45477,45902,47541,48266,49410,49576,52665,59879,59921,61455,")
                ElseIf cClass = 8 Then
                    AddSkills("6,1,1,8,1,1,95,1,5,136,1,5,162,1,5,220,1,1,228,1,5,415,1,1,")
                    AddSpells("81,133,168,203,204,227,522,669,2382,2479,3050,3127,3365,5009,5019,5227,6233,6246,6247,6477,6478,6603,7266,7267,7355,7744,8386,9078,9125,17737,20577,20579,21651,21652,22027,22810,")
                ElseIf cClass = 9 Then
                    AddSkills("95,1,5,136,1,5,162,1,5,173,1,5,220,1,1,228,1,5,354,1,1,415,1,1,593,1,1,")
                    AddSpells("81,203,204,227,522,669,686,687,1180,2382,2479,3050,3127,3365,5009,5019,5227,6233,6246,6247,6477,6478,6603,7266,7267,7355,7744,8386,9078,9125,17737,20577,20579,21651,21652,22027,22810,")
                End If
            ElseIf cRace = 6 Then
                If cClass = 1 Then
                    AddSkills("26,1,1,44,1,5,54,1,5,55,1,5,95,1,5,124,1,1,160,1,5,162,1,5,413,1,1,414,1,1,415,1,1,433,1,1,")
                    AddSpells("78,81,107,196,198,199,202,203,204,522,669,670,2382,2457,2479,3050,3127,3365,5301,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,8737,9077,9078,9116,9125,20549,20550,20551,20552,21651,21652,22027,22810,")
                ElseIf cClass = 3 Then
                    AddSkills("44,1,5,46,1,5,51,1,1,95,1,5,124,1,1,162,1,5,163,1,1,172,1,5,414,1,1,415,1,1,")
                    AddSpells("75,81,196,197,203,204,266,522,669,670,2382,2479,2973,3018,3050,3127,3365,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,9077,9078,9125,13358,20549,20550,20551,20552,21651,21652,22027,22810,24949,")
                ElseIf cClass = 6 Then
                    AddSkills("43,270,275,44,270,275,55,270,275,95,270,275,124,1,1,129,270,300,162,270,275,172,270,275,229,270,275,293,1,1,413,1,1,414,1,1,415,1,1,762,150,150,770,1,1,771,1,1,772,1,1,")
                    AddSpells("81,196,197,200,201,202,203,204,522,669,670,674,750,2382,2479,3050,3127,3275,3276,3277,3278,3365,6233,6246,6247,6477,6478,6603,7266,7267,7355,7928,7929,7934,8386,8737,9077,9078,9125,10840,10841,10846,18629,18630,20549,20550,20551,20552,21651,21652,22027,22810,33391,45462,45477,45902,47541,48266,49410,49576,52665,59879,59921,61455,")
                ElseIf cClass = 7 Then
                    AddSkills("54,1,5,95,1,5,124,1,1,136,1,5,162,1,5,375,1,1,414,1,1,415,1,1,433,1,1,573,1,1,")
                    AddSpells("81,107,198,203,204,227,331,403,522,669,670,2382,2479,3050,3127,3365,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,9077,9078,9116,9125,20549,20550,20551,20552,21651,21652,22027,22810,27763,")
                ElseIf cClass = 11 Then
                    AddSkills("54,1,5,95,1,5,124,1,1,136,1,5,162,1,5,414,1,1,415,1,1,573,1,1,574,1,1,")
                    AddSpells("81,198,203,204,227,522,669,670,2382,2479,3050,3127,3365,5176,5185,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,9077,9078,9125,20549,20550,20551,20552,21651,21652,22027,22810,27764,")
                End If
            ElseIf cRace = 7 Then
                If cClass = 1 Then
                    AddSkills("26,1,1,43,1,5,54,1,5,55,1,5,95,1,5,162,1,5,173,1,5,413,1,1,414,1,1,415,1,1,433,1,1,753,1,1,")
                    AddSpells("78,81,107,198,201,202,203,204,522,668,1180,2382,2457,2479,3050,3127,3365,5301,6233,6246,6247,6477,6478,6603,7266,7267,7340,7355,8386,8737,9077,9078,9116,9125,20589,20591,20592,20593,21651,21652,22027,22810,")
                ElseIf cClass = 4 Then
                    AddSkills("38,1,1,95,1,5,162,1,5,173,1,5,176,1,5,253,1,1,414,1,1,415,1,1,753,1,1,")
                    AddSpells("81,203,204,522,668,674,1180,1752,2098,2382,2479,2567,2764,3050,3127,3365,6233,6246,6247,6477,6478,6603,7266,7267,7340,7355,8386,9077,9078,9125,16092,20589,20591,20592,20593,21184,21651,21652,22027,22810,")
                ElseIf cClass = 6 Then
                    AddSkills("43,270,275,44,270,275,55,270,275,95,270,275,129,270,300,162,270,275,172,270,275,229,270,275,293,1,1,413,1,1,414,1,1,415,1,1,753,1,1,762,150,150,770,1,1,771,1,1,772,1,1,")
                    AddSpells("81,196,197,200,201,202,203,204,522,668,674,750,2382,2479,3050,3127,3275,3276,3277,3278,3365,6233,6246,6247,6477,6478,6603,7266,7267,7340,7355,7928,7929,7934,8386,8737,9077,9078,9125,10840,10841,10846,18629,18630,20589,20591,20592,20593,21651,21652,22027,22810,33391,45462,45477,45902,47541,48266,49410,49576,52665,59879,59921,61455,")
                ElseIf cClass = 8 Then
                    AddSkills("6,1,1,8,1,1,95,1,5,136,1,5,162,1,5,228,1,5,415,1,1,753,1,1,")
                    AddSpells("81,133,168,203,204,227,522,668,2382,2479,3050,3127,3365,5009,5019,6233,6246,6247,6477,6478,6603,7266,7267,7340,7355,8386,9078,9125,20589,20591,20592,20593,21651,21652,22027,22810,")
                ElseIf cClass = 9 Then
                    AddSkills("95,1,5,136,1,5,162,1,5,173,1,5,228,1,5,354,1,1,415,1,1,593,1,1,753,1,1,")
                    AddSpells("81,203,204,227,522,668,686,687,1180,2382,2479,3050,3127,3365,5009,5019,6233,6246,6247,6477,6478,6603,7266,7267,7340,7355,8386,9078,9125,20589,20591,20592,20593,21651,21652,22027,22810,")
                End If
            ElseIf cRace = 8 Then
                If cClass = 1 Then
                    AddSkills("26,1,1,44,1,5,55,1,5,95,1,5,162,1,5,173,1,5,176,1,5,413,1,1,414,1,1,415,1,1,433,1,1,733,1,1,")
                    AddSpells("78,81,107,196,202,203,204,522,669,1180,2382,2457,2479,2567,2764,3050,3127,3365,5301,6233,6246,6247,6477,6478,6603,7266,7267,7341,7355,8386,8737,9077,9078,9116,9125,20555,20557,20558,21651,21652,22027,22810,26290,26297,58943,")
                ElseIf cClass = 3 Then
                    AddSkills("44,1,5,45,1,5,51,1,1,95,1,5,162,1,5,163,1,1,172,1,5,414,1,1,415,1,1,733,1,1,")
                    AddSpells("75,81,196,197,203,204,264,522,669,2382,2479,2973,3018,3050,3127,3365,6233,6246,6247,6477,6478,6603,7266,7267,7341,7355,8386,9077,9078,9125,13358,20555,20557,20558,21651,21652,22027,22810,24949,26290,26297,58943,")
                ElseIf cClass = 4 Then
                    AddSkills("38,1,1,95,1,5,162,1,5,173,1,5,176,1,5,253,1,1,414,1,1,415,1,1,733,1,1,")
                    AddSpells("81,203,204,522,669,674,1180,1752,2098,2382,2479,2567,2764,3050,3127,3365,6233,6246,6247,6477,6478,6603,7266,7267,7341,7355,8386,9077,9078,9125,16092,20555,20557,20558,21184,21651,21652,22027,22810,26290,26297,58943,")
                ElseIf cClass = 5 Then
                    AddSkills("54,1,5,56,1,1,95,1,5,136,1,5,162,1,5,228,1,5,415,1,1,733,1,1,")
                    AddSpells("81,198,203,204,227,522,585,669,2050,2382,2479,3050,3127,3365,5009,5019,6233,6246,6247,6477,6478,6603,7266,7267,7341,7355,8386,9078,9125,20555,20557,20558,21651,21652,22027,22810,26290,26297,58943,")
                ElseIf cClass = 6 Then
                    AddSkills("43,270,275,44,270,275,55,270,275,95,270,275,129,270,300,162,270,275,172,270,275,229,270,275,293,1,1,413,1,1,414,1,1,415,1,1,733,1,1,762,150,150,770,1,1,771,1,1,772,1,1,")
                    AddSpells("81,196,197,200,201,202,203,204,522,669,674,750,2382,2479,3050,3127,3275,3276,3277,3278,3365,6233,6246,6247,6477,6478,6603,7266,7267,7341,7355,7928,7929,7934,8386,8737,9077,9078,9125,10840,10841,10846,18629,18630,20555,20557,20558,21651,21652,22027,22810,26290,26297,33391,45462,45477,45902,47541,48266,49410,49576,52665,58943,59879,59921,61455,")
                ElseIf cClass = 7 Then
                    AddSkills("54,1,5,95,1,5,136,1,5,162,1,5,375,1,1,414,1,1,415,1,1,573,1,1,733,1,1,")
                    AddSpells("81,107,198,203,204,227,331,403,522,669,2382,2479,3050,3127,3365,6233,6246,6247,6477,6478,6603,7266,7267,7341,7355,8386,9077,9078,9116,9125,20555,20557,20558,21651,21652,22027,22810,26290,26297,27763,58943,")
                ElseIf cClass = 8 Then
                    AddSkills("6,1,1,8,1,1,95,1,5,136,1,5,162,1,5,228,1,5,415,1,1,733,1,1,")
                    AddSpells("81,133,168,203,204,227,522,669,2382,2479,3050,3127,3365,5009,5019,6233,6246,6247,6477,6478,6603,7266,7267,7341,7355,8386,9078,9125,20555,20557,20558,21651,21652,22027,22810,26290,26297,58943,")
                End If
            ElseIf cRace = 10 Then
                If cClass = 2 Then
                    AddSkills("43,1,5,55,1,5,95,1,5,162,1,5,413,1,1,414,1,1,415,1,1,433,1,1,594,1,1,756,1,1,")
                    AddSpells("81,107,201,202,203,204,522,635,669,813,822,2382,2479,3050,3127,3365,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,8737,9077,9078,9116,9125,21084,21651,21652,22027,22810,25046,27762,28877,34082,")
                ElseIf cClass = 3 Then
                    AddSkills("45,1,5,51,1,1,95,1,5,162,1,5,163,1,1,172,1,5,173,1,5,414,1,1,415,1,1,756,1,1,")
                    AddSpells("75,81,197,203,204,264,522,669,813,822,1180,2382,2479,2973,3018,3050,3127,3365,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,9077,9078,9125,13358,21651,21652,22027,22810,24949,25046,28877,")
                ElseIf cClass = 4 Then
                    AddSkills("38,1,1,95,1,5,162,1,5,173,1,5,176,1,5,253,1,1,414,1,1,415,1,1,756,1,1,")
                    AddSpells("81,203,204,522,669,674,813,822,1180,1752,2098,2382,2479,2567,2764,3050,3127,3365,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,9077,9078,9125,16092,21184,21651,21652,22027,22810,25046,28877,")
                ElseIf cClass = 5 Then
                    AddSkills("54,1,5,56,1,1,95,1,5,136,1,5,162,1,5,228,1,5,415,1,1,756,1,1,")
                    AddSpells("81,198,203,204,227,522,585,669,813,822,2050,2382,2479,3050,3127,3365,5009,5019,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,9078,9125,21651,21652,22027,22810,28730,28877,")
                ElseIf cClass = 6 Then
                    AddSkills("43,270,275,44,270,275,55,270,275,95,270,275,129,270,300,162,270,275,172,270,275,229,270,275,293,1,1,413,1,1,414,1,1,415,1,1,756,1,1,762,150,150,770,1,1,771,1,1,772,1,1,")
                    AddSpells("81,196,197,200,201,202,203,204,522,669,674,750,813,822,2382,2479,3050,3127,3275,3276,3277,3278,3365,6233,6246,6247,6477,6478,6603,7266,7267,7355,7928,7929,7934,8386,8737,9077,9078,9125,10840,10841,10846,18629,18630,21651,21652,22027,22810,28877,33391,45462,45477,45902,47541,48266,49410,49576,50613,52665,59879,59921,61455,")
                ElseIf cClass = 8 Then
                    AddSkills("6,1,1,8,1,1,95,1,5,136,1,5,162,1,5,228,1,5,415,1,1,756,1,1,")
                    AddSpells("81,133,168,203,204,227,522,669,813,822,2382,2479,3050,3127,3365,5009,5019,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,9078,9125,21651,21652,22027,22810,28730,28877,")
                ElseIf cClass = 9 Then
                    AddSkills("95,1,5,136,1,5,162,1,5,173,1,5,228,1,5,354,1,1,415,1,1,593,1,1,756,1,1,")
                    AddSpells("81,203,204,227,522,669,686,687,813,822,1180,2382,2479,3050,3127,3365,5009,5019,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,9078,9125,21651,21652,22027,22810,28730,28877,")
                End If
            ElseIf cRace = 11 Then
                If cClass = 1 Then
                    AddSkills("26,1,1,43,1,5,54,1,5,55,1,5,95,1,5,162,1,5,413,1,1,414,1,1,415,1,1,433,1,1,760,1,1,")
                    AddSpells("78,81,107,198,201,202,203,204,522,668,2382,2457,2479,3050,3127,3365,5301,6233,6246,6247,6477,6478,6562,6603,7266,7267,7355,8386,8737,9077,9078,9116,9125,20579,21651,21652,22027,22810,28875,28880,29932,32215,")
                ElseIf cClass = 2 Then
                    AddSkills("54,1,5,95,1,5,160,1,5,162,1,5,413,1,1,414,1,1,415,1,1,433,1,1,594,1,1,760,1,1,")
                    AddSpells("81,107,198,199,203,204,522,635,668,2382,2479,3050,3127,3365,6233,6246,6247,6477,6478,6562,6603,7266,7267,7355,8386,8737,9077,9078,9116,9125,20579,21084,21651,21652,22027,22810,27762,28875,28880,29932,34082,")
                ElseIf cClass = 3 Then
                    AddSkills("43,1,5,51,1,1,95,1,5,162,1,5,163,1,1,172,1,5,226,1,5,414,1,1,415,1,1,760,1,1,")
                    AddSpells("75,81,197,201,203,204,522,668,2382,2479,2973,3018,3050,3127,3365,5011,6233,6246,6247,6477,6478,6562,6603,7266,7267,7355,8386,9077,9078,9125,13358,20579,21651,21652,22027,22810,24949,28875,28880,29932,")
                ElseIf cClass = 5 Then
                    AddSkills("54,1,5,56,1,1,95,1,5,136,1,5,162,1,5,228,1,5,415,1,1,760,1,1,")
                    AddSpells("81,198,203,204,227,522,585,668,2050,2382,2479,3050,3127,3365,5009,5019,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,9078,9125,20579,21651,21652,22027,22810,28875,28878,28880,29932,")
                ElseIf cClass = 6 Then
                    AddSkills("43,270,275,44,270,275,55,270,275,95,270,275,129,270,300,162,270,275,172,270,275,229,270,275,293,1,1,413,1,1,414,1,1,415,1,1,760,1,1,762,150,150,770,1,1,771,1,1,772,1,1,")
                    AddSpells("81,196,197,200,201,202,203,204,522,668,674,750,2382,2479,3050,3127,3275,3276,3277,3278,3365,6233,6246,6247,6477,6478,6562,6603,7266,7267,7355,7928,7929,7934,8386,8737,9077,9078,9125,10840,10841,10846,18629,18630,20579,21651,21652,22027,22810,28875,28880,29932,33391,45462,45477,45902,47541,48266,49410,49576,52665,59879,59921,61455,")
                ElseIf cClass = 7 Then
                    AddSkills("54,1,5,95,1,5,136,1,5,162,1,5,375,1,1,414,1,1,415,1,1,433,1,1,573,1,1,760,1,1,")
                    AddSpells("81,107,198,203,204,227,331,403,522,668,2382,2479,3050,3127,3365,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,9077,9078,9116,9125,20579,21651,21652,22027,22810,27763,28875,28878,28880,29932,")
                ElseIf cClass = 8 Then
                    AddSkills("6,1,1,8,1,1,95,1,5,136,1,5,162,1,5,228,1,5,415,1,1,760,1,1,")
                    AddSpells("81,133,168,203,204,227,522,668,2382,2479,3050,3127,3365,5009,5019,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,9078,9125,20579,21651,21652,22027,22810,28875,28878,28880,29932,")
                End If
            End If
            'Setting tutorials
            runSQLCommand_characters_string("INSERT INTO `tutorials` ( playerId ) VALUES ( " & accid.ToString() & " )", True)
        Catch ex As Exception
            LogAppend("Something went wrong while creating the account -> Skipping! -> Error message is: " & ex.ToString(), "CharacterCreationLite_createAtArcemu", False, True)
        End Try
    End Sub
    Private Shared Sub createAtTrinity(ByVal charactername As String, ByVal accname As String, ByVal targetSetId As Integer, ByVal NameChange As Boolean)
        LogAppend("Creating at Trinity", "CharacterCreationLite_createAtTrinity", False)
        Dim newcharguid As Integer = TryInt(runSQLCommand_characters_string("SELECT guid FROM characters WHERE guid=(SELECT MAX(guid) FROM characters)", True)) + 1
        Dim accid As Integer = TryInt(runSQLCommand_realm_string("SELECT `id` FROM account WHERE username='" & accname & "'", True))
        Const sqlstring As String = "INSERT INTO characters ( `guid`, `account`, `name`, `race`, `class`, `gender`, `level`, `xp`, `money`, `playerBytes`, `position_x`, position_y, position_z, map, orientation, taximask, cinematic, `health` ) VALUES " &
            "( @guid, @accid, @name, @race, @class, @gender, @level, '0', '0', @pBytes, '-14306', '515', '10', '0', '5', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1', '1000' )"
        Dim tempcommand As New MySqlCommand(sqlstring, TargetConnection_Realm)
        tempcommand.Parameters.AddWithValue("@accid", accid.ToString())
        tempcommand.Parameters.AddWithValue("@guid", newcharguid.ToString())
        tempcommand.Parameters.AddWithValue("@name", charactername)
        tempcommand.Parameters.AddWithValue("@class", GetTemporaryCharacterInformation("@character_class", targetSetId))
        tempcommand.Parameters.AddWithValue("@race", GetTemporaryCharacterInformation("@character_race", targetSetId))
        tempcommand.Parameters.AddWithValue("@gender", GetTemporaryCharacterInformation("@character_gender", targetSetId))
        tempcommand.Parameters.AddWithValue("@level", GetTemporaryCharacterInformation("@character_level", targetSetId))
        tempcommand.Parameters.AddWithValue("@pBytes", GetTemporaryCharacterInformation("@character_playerBytes", targetSetId))
        Try
            tempcommand.ExecuteNonQuery()
            If NameChange = True Then
                runSQLCommand_characters_string("UPDATE characters SET at_login='1' WHERE guid='" & newcharguid.ToString & "'", True)
            Else
                If CharacterExist(charactername) = True Then
                    runSQLCommand_characters_string("UPDATE characters SET at_login='1' WHERE guid='" & newcharguid.ToString & "'", True)
                End If
            End If
            'Creating hearthstone
            LogAppend("Creating character hearthstone", "CharacterCreationLite_createAtArcemu", False)
            Dim newitemguid As Integer = (TryInt(runSQLCommand_characters_string("SELECT guid FROM item_instance WHERE guid=(SELECT MAX(guid) FROM item_instance)", True)) + 1)
            runSQLCommand_characters_string("INSERT INTO item_instance ( guid, itemEntry, owner_guid, count, charges, enchantments, durability ) VALUES ( '" &
               newitemguid.ToString & "', '6948', '" & accid.ToString() & "', '1', '0 0 0 0 0 ', '" & newitemguid.ToString() &
               " 1191182336 3 6948 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )", True)
            runSQLCommand_characters_string("INSERT INTO character_inventory ( guid, bag, `slot`, `item` ) VALUES ( '" & accid.ToString() & "', '0', '23', '" & newitemguid.ToString() & "')", True)
        Catch ex As Exception
            LogAppend("Something went wrong while creating the account -> Skipping! -> Error message is: " & ex.ToString(), "CharacterCreationLite_createAtTrinity", False, True)
        End Try
    End Sub
    Private Shared Sub createAtMangos(ByVal charactername As String, ByVal accname As String, ByVal targetSetId As Integer, ByVal NameChange As Boolean)
        LogAppend("Creating at Mangos", "CharacterCreationLite_createAtMangos", False)
        Dim newcharguid As Integer = TryInt(runSQLCommand_characters_string("SELECT guid FROM characters WHERE guid=(SELECT MAX(guid) FROM characters)", True)) + 1
        Dim accid As Integer = TryInt(runSQLCommand_realm_string("SELECT `id` FROM account WHERE username='" & accname & "'", True))
        Const sqlstring As String = "INSERT INTO characters ( `guid`, `account`, `name`, `race`, `class`, `gender`, `level`, `xp`, `money`, `playerBytes`, `position_x`, position_y, position_z, map, orientation, taximask `health` ) VALUES " &
            "( @guid, @accid, @name, @race, @class, @gender, @level, '0', '0', @pBytes, '-14305.7', '514.08', '10', '0', '4.30671', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 ','1000' )"
        Dim tempcommand As New MySqlCommand(sqlstring, TargetConnection_Realm)
        tempcommand.Parameters.AddWithValue("@accid", accid.ToString())
        tempcommand.Parameters.AddWithValue("@guid", newcharguid.ToString())
        tempcommand.Parameters.AddWithValue("@name", charactername)
        tempcommand.Parameters.AddWithValue("@pBytes", GetTemporaryCharacterInformation("@character_playerBytes", targetSetId))
        tempcommand.Parameters.AddWithValue("@class", GetTemporaryCharacterInformation("@character_class", targetSetId))
        tempcommand.Parameters.AddWithValue("@race", GetTemporaryCharacterInformation("@character_race", targetSetId))
        tempcommand.Parameters.AddWithValue("@gender", GetTemporaryCharacterInformation("@character_gender", targetSetId))
        tempcommand.Parameters.AddWithValue("@level", GetTemporaryCharacterInformation("@character_level", targetSetId))
        Try
            tempcommand.ExecuteNonQuery()
            If NameChange = True Then
                runSQLCommand_characters_string("UPDATE characters SET at_login='1' WHERE guid='" & newcharguid.ToString & "'", True)
            Else
                If CharacterExist(charactername) = True Then
                    runSQLCommand_characters_string("UPDATE characters SET at_login='1' WHERE guid='" & newcharguid.ToString & "'", True)
                End If
            End If
            'Creating hearthstone
            LogAppend("Creating character hearthstone", "CharacterCreationLite_createAtArcemu", False)
            Dim newitemguid As Integer = (TryInt(runSQLCommand_characters_string("SELECT guid FROM item_instance WHERE guid=(SELECT MAX(guid) FROM item_instance))", True)) + 1)
            If expansion >= 3 Then
                runSQLCommand_characters_string("INSERT INTO item_instance ( guid, owner_guid, data ) VALUES ( '" & newitemguid.ToString() & "', '" & accid.ToString() & "', '" & newitemguid.ToString() &
                                                " 1191182336 3 6948 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ' )", True)
            Else
                'MaNGOS < 3.3 Core: watch data length
                runSQLCommand_characters_string(
                    "INSERT INTO item_instance ( guid, owner_guid, data ) VALUES ( '" & newitemguid.ToString() & "', '" & accid.ToString() & "', '" & newitemguid.ToString() &
                    " 1191182336 3 6948 1065353216 0 8 0 8 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ' )", True)
            End If
            runSQLCommand_characters_string("INSERT INTO character_inventory ( guid, bag, slot, item, item_template ) VALUES ( '" & accid.ToString() & "', '0', '23', '" & newitemguid.ToString() & "', '6948')")
        Catch ex As Exception
            LogAppend("Something went wrong while creating the account -> Skipping! -> Error message is: " & ex.ToString(), "CharacterCreationLite_createAtMangos", False, True)
        End Try
    End Sub
End Class
