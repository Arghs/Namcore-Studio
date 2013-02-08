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
'*      /Filename:      ArmorCreation
'*      /Description:   Includes functions for creating the equipped items of a specific
'*                      character
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Imports Namcore_Studio.EventLogging
Imports Namcore_Studio.CommandHandler
Imports Namcore_Studio.GlobalVariables
Imports Namcore_Studio.Basics
Imports Namcore_Studio.Conversions
Imports Namcore_Studio.SkillCreation
Imports Namcore_Studio.SpellCreation
Imports System.Text.RegularExpressions
Public Class ArmorCreation
    Public Shared Sub AddCharacterArmor(ByVal setId As Integer, Optional charguid As Integer = 0)
        If charguid = 0 Then charguid = characterGUID
        LogAppend("Adding armor to character: " & charguid.ToString() & " // setId is : " & setId.ToString(), "ArmorCreation_AddCharacterArmor", True)
        Select Case sourceCore
            Case "arcemu"
                createAtArcemu(charguid, setId)
            Case "trinity"
                createAtTrinity(charguid, setId)
            Case "trinitytbc"

            Case "mangos"
                createAtMangos(charguid, setId)
            Case Else

        End Select
    End Sub
    Private Shared Sub createAtArcemu(ByVal characterguid As Integer, ByVal targetSetId As Integer)
        LogAppend("Creating armor at arcemu", "ArmorCreation_createAtArcemu", False)
        LogAppend("Adding weapon specific spells and skills", "ArmorCreation_createAtArcemu", False)
        'Adding weapon specific spells and skills
        Dim cClass As Integer = TryInt(GetTemporaryCharacterInformation("@character_class", targetSetId))
        If cClass = 1 Or cClass = 2 Or cClass = 6 Then
            AddSpells("750,")
            AddSkills("293,")
        ElseIf cClass = 1 Or cClass = 2 Or cClass = 3 Or cClass = 6 Or cClass = 7 Then
            AddSpells("8737,")
            AddSkills("413,")
        ElseIf cClass = 1 Or cClass = 2 Or cClass = 3 Or cClass = 4 Or cClass = 6 Or cClass = 7 Or cClass = 11 Then
            AddSpells("9077,")
            AddSkills("414,")
        Else : End If
        LogAppend("Adding items", "ArmorCreation_createAtArcemu", False)
        Dim itemid As Integer
        Dim itemtypelist(18) As String
        itemtypelist(0) = "head" : itemtypelist(1) = "neck" : itemtypelist(2) = "shoulder" : itemtypelist(3) = "shirt" : itemtypelist(4) = "chest" : itemtypelist(5) = "waist" : itemtypelist(6) = "legs" : itemtypelist(7) = "feet"
        itemtypelist(8) = "wrists" : itemtypelist(9) = "hands" : itemtypelist(10) = "finger1" : itemtypelist(11) = "finger2" : itemtypelist(12) = "trinket1" : itemtypelist(13) = "trinket2" : itemtypelist(14) = "back"
        itemtypelist(15) = "main" : itemtypelist(16) = "off" : itemtypelist(17) = "distance" : itemtypelist(18) = "tabard"
        'Build item type string
        Dim finalItemString As String = ""
        For Each newItemType As String In itemtypelist
            finalItemString = finalItemString & newItemType & " 0 "
        Next
        Dim typeCounter As Integer = 0
        Dim newItemGuid As Integer = TryInt(runSQLCommand_characters_string("SELECT guid FROM playeritems WHERE guid=(SELECT MAX(guid) FROM playeritems)"))
        For Each newItemType As String In itemtypelist
            itemid = TryInt(GetTemporaryCharacterInformation("@character_" & newItemType & "Id", targetSetId))
            If itemid = 0 Then Continue For
            newItemGuid += 1
            finalItemString = finalItemString.Replace(newItemType, itemid.ToString())
            If ReturnResultCount("SELECT * FROM playeritems WHERE ownerguid='" & characterguid.ToString() & "' AND slot = '" & typeCounter.ToString() & "' AND containerslot='-1')") > 0 Then
                runSQLCommand_characters_string("DELETE FROM playeritems WHERE ownerguid = '" & characterguid.ToString() & "' AND slot = '" & typeCounter.ToString() & "'")
                runSQLCommand_characters_string("INSERT INTO playeritems ( guid, ownerguid, entry, containerslot, slot) VALUES ( '" & newItemGuid.ToString() & "', '" & characterguid & "', '" &
                                                itemid.ToString & "', '-1', '" & typeCounter.ToString() & "' )")
            Else
                runSQLCommand_characters_string("INSERT INTO playeritems ( guid, ownerguid, entry, containerslot, slot) VALUES ( '" & newItemGuid.ToString() & "', '" & characterguid & "', '" &
                                               itemid.ToString & "', '-1', '" & typeCounter.ToString() & "' )")
            End If
            typeCounter += 1
        Next
        If Not Regex.IsMatch(finalItemString, "^[0-9 ]+$") Then
            For Each itemtype As String In itemtypelist
                finalItemString = finalItemString.Replace(itemtype, "0")
            Next
        End If
    End Sub
  Private Shared Sub createAtTrinity(ByVal characterguid As Integer, ByVal targetSetId As Integer)
        LogAppend("Creating armor at trinity", "ArmorCreation_createAtTrinity", False)
        LogAppend("Adding weapon specific spells and skills", "ArmorCreation_createAtTrinity", False)
        'Adding weapon specific spells and skills
        Dim cClass As Integer = TryInt(GetTemporaryCharacterInformation("@character_class", targetSetId))
        If cClass = 1 Or cClass = 2 Or cClass = 6 Then
            AddSpells("750,")
            AddSkills("293,")
        ElseIf cClass = 1 Or cClass = 2 Or cClass = 3 Or cClass = 6 Or cClass = 7 Then
            AddSpells("8737,")
            AddSkills("413,")
        ElseIf cClass = 1 Or cClass = 2 Or cClass = 3 Or cClass = 4 Or cClass = 6 Or cClass = 7 Or cClass = 11 Then
            AddSpells("9077,")
            AddSkills("414,")
        Else : End If
        LogAppend("Adding items", "ArmorCreation_createAtTrinity", False)
        Dim itemid As Integer
        Dim itemtypelist(18) As String
        itemtypelist(0) = "head" : itemtypelist(1) = "neck" : itemtypelist(2) = "shoulder" : itemtypelist(3) = "shirt" : itemtypelist(4) = "chest" : itemtypelist(5) = "waist" : itemtypelist(6) = "legs" : itemtypelist(7) = "feet"
        itemtypelist(8) = "wrists" : itemtypelist(9) = "hands" : itemtypelist(10) = "finger1" : itemtypelist(11) = "finger2" : itemtypelist(12) = "trinket1" : itemtypelist(13) = "trinket2" : itemtypelist(14) = "back"
        itemtypelist(15) = "main" : itemtypelist(16) = "off" : itemtypelist(17) = "distance" : itemtypelist(18) = "tabard"
        'Build item type string
        Dim finalItemString As String = ""
        For Each newItemType As String In itemtypelist
            finalItemString = finalItemString & newItemType & " 0 "
        Next
        Dim typeCounter As Integer = 0
        Dim newItemGuid As Integer = TryInt(runSQLCommand_characters_string("SELECT guid FROM item_instance WHERE guid=(SELECT MAX(guid) FROM item_instance)"))
        For Each newItemType As String In itemtypelist
            itemid = TryInt(GetTemporaryCharacterInformation("@character_" & newItemType & "Id", targetSetId))
            If itemid = 0 Then Continue For
            newItemGuid += 1
            finalItemString = finalItemString.Replace(newItemType, itemid.ToString())
            runSQLCommand_characters_string("INSERT INTO item_instance ( guid, itemEntry, owner_guid, count, charges, enchantments, durability ) VALUES ( '" &
                                            newItemGuid.ToString() & "', '" & itemid & "', '" & characterguid.ToString() &
                                            "', '1', '0 0 0 0 0 ', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )")
            If ReturnResultCount("SELECT * FROM character_inventory WHERE guid='" & characterguid.ToString() & "' AND slot = '" & typeCounter.ToString() & "')") > 0 Then
                runSQLCommand_characters_string("DELETE FROM character_inventory WHERE guid = '" & characterguid.ToString() & "' AND slot = '" & typeCounter.ToString() & "'")
                runSQLCommand_characters_string("INSERT INTO character_inventory ( guid, slot, item ) VALUES ( '" & characterguid.ToString() & "', '" & typeCounter.ToString() & "', '" & newItemGuid.ToString() & "' )")
            Else
                runSQLCommand_characters_string("INSERT INTO character_inventory ( guid, slot, item ) VALUES ( '" & characterguid.ToString() & "', '" & typeCounter.ToString() & "', '" & newItemGuid.ToString() & "' )")
            End If
            typeCounter += 1
        Next
        If Not Regex.IsMatch(finalItemString, "^[0-9 ]+$") Then
            For Each itemtype As String In itemtypelist
                finalItemString = finalItemString.Replace(itemtype, "0")
            Next
        End If
        runSQLCommand_characters_string("UPDATE characters SET equipmentCache='" & finalItemString & "' WHERE (guid='" & characterguid.ToString() & "')")
    End Sub
    Private Shared Sub createAtMangos(ByVal characterguid As Integer, ByVal targetSetId As Integer)
        LogAppend("Creating armor at mangos", "ArmorCreation_createAtMangos", False)
        LogAppend("Adding weapon specific spells and skills", "ArmorCreation_createAtMangos", False)
        'Adding weapon specific spells and skills
        Dim cClass As Integer = TryInt(GetTemporaryCharacterInformation("@character_class", targetSetId))
        If cClass = 1 Or cClass = 2 Or cClass = 6 Then
            AddSpells("750,")
            AddSkills("293,")
        ElseIf cClass = 1 Or cClass = 2 Or cClass = 3 Or cClass = 6 Or cClass = 7 Then
            AddSpells("8737,")
            AddSkills("413,")
        ElseIf cClass = 1 Or cClass = 2 Or cClass = 3 Or cClass = 4 Or cClass = 6 Or cClass = 7 Or cClass = 11 Then
            AddSpells("9077,")
            AddSkills("414,")
        Else : End If
        LogAppend("Adding items", "ArmorCreation_createAtMangos", False)
        Dim itemid As Integer
        Dim itemtypelist(18) As String
        itemtypelist(0) = "head" : itemtypelist(1) = "neck" : itemtypelist(2) = "shoulder" : itemtypelist(3) = "shirt" : itemtypelist(4) = "chest" : itemtypelist(5) = "waist" : itemtypelist(6) = "legs" : itemtypelist(7) = "feet"
        itemtypelist(8) = "wrists" : itemtypelist(9) = "hands" : itemtypelist(10) = "finger1" : itemtypelist(11) = "finger2" : itemtypelist(12) = "trinket1" : itemtypelist(13) = "trinket2" : itemtypelist(14) = "back"
        itemtypelist(15) = "main" : itemtypelist(16) = "off" : itemtypelist(17) = "distance" : itemtypelist(18) = "tabard"
        'Build item type string
        Dim finalItemString As String = ""
        For Each newItemType As String In itemtypelist
            finalItemString = finalItemString & newItemType & " 0 "
        Next
        Dim typeCounter As Integer = 0
        Dim newItemGuid As Integer = TryInt(runSQLCommand_characters_string("SELECT guid FROM item_instance WHERE guid=(SELECT MAX(guid) FROM item_instance)"))
        For Each newItemType As String In itemtypelist
            itemid = TryInt(GetTemporaryCharacterInformation("@character_" & newItemType & "Id", targetSetId))
            If itemid = 0 Then Continue For
            newItemGuid += 1
            finalItemString = finalItemString.Replace(newItemType, itemid.ToString())
            If expansion >= 3 Then
                runSQLCommand_characters_string("INSERT INTO item_instance ( guid, owner_guid, data) VALUES ( '" & newItemGuid.ToString() & "', '" & characterguid.ToString() &
                                                "', '" & newItemGuid.ToString() & " 1191182336 3 " & itemid.ToString() &
                                                " 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 100 100 0 0 ')")
            Else
                runSQLCommand_characters_string("INSERT INTO item_instance ( guid, owner_guid, data) VALUES ( '" & newItemGuid.ToString() & "', '" & characterguid.ToString() &
                                                "', '" & newItemGuid.ToString() & " 1191182336 3 " & itemid.ToString() &
                                                " 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 100 100 0 0 ')")
            End If
            If ReturnResultCount("SELECT * FROM character_inventory WHERE guid='" & characterguid.ToString() & "' AND slot = '" & typeCounter.ToString() & "')") > 0 Then
                runSQLCommand_characters_string("DELETE FROM character_inventory WHERE guid = '" & characterguid.ToString() & "' AND slot = '" & typeCounter.ToString() & "'")
                runSQLCommand_characters_string("INSERT INTO character_inventory ( guid, bag, slot, item, item_template ) VALUES " &
                                                "( '" & characterguid.ToString() & "', '0', '" & typeCounter.ToString() & "', '" & newItemGuid.ToString() & "', '" & itemid.ToString & "' )")
            Else
                runSQLCommand_characters_string("INSERT INTO character_inventory ( guid, bag, slot, item, item_template ) VALUES " &
                                                 "( '" & characterguid.ToString() & "', '0', '" & typeCounter.ToString() & "', '" & newItemGuid.ToString() & "', '" & itemid.ToString & "' )")
            End If
            typeCounter += 1
        Next
        If Not Regex.IsMatch(finalItemString, "^[0-9 ]+$") Then
            For Each itemtype As String In itemtypelist
                finalItemString = finalItemString.Replace(itemtype, "0")
            Next
        End If
        runSQLCommand_characters_string("UPDATE characters SET equipmentCache='" & finalItemString & "' WHERE (guid='" & characterguid.ToString() & "')")
    End Sub
End Class
