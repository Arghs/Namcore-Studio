Imports Namcore_Studio.EventLogging
Imports Namcore_Studio.Basics
Imports Namcore_Studio.GlobalVariables
Imports Namcore_Studio.CommandHandler
Imports Namcore_Studio.SpellItem_Information
Imports Namcore_Studio.CharacterItemStatsHandler

Public Class CharacterArmorHandler

    Public Shared Sub GetCharacterArmor(ByVal charguid As Integer, ByVal setId As Integer, ByVal accountId As Integer)
        LogAppend("Loading character Armor for charguid: " & charguid & " and setId: " & setId, "CharacterArmorHandler_GetCharacterArmor", True)
        Select Case sourceCore
            Case "arcemu"
                loadAtArcemu(charguid, setId, accountId)
            Case "trinity"
                loadAtTrinity(charguid, setId, accountId)
            Case "trinitytbc"
                loadAtTrinityTBC(charguid, setId, accountId)
            Case "mangos"
                loadAtMangos(charguid, setId, accountId)
            Case Else

        End Select

    End Sub
    Private Shared Sub loadAtArcemu(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)
        LogAppend("Loading character Armor @loadAtArcemu", "CharacterArmorHandler_loadAtArcemu", False)
        Dim tempdt As DataTable = ReturnDataTable("SELECT guid, entry, slot FROM playeritems WHERE ownerguid='" & charguid.ToString & "' AND containerslot='-1'")
        Dim itemguid As Integer
        Dim slotname As String
        Dim itementry As Integer
        Dim itemslot As Integer
        Dim entrycount As Integer = CInt(Val(tempdt.Rows.Count))
        Dim loopcounter As Integer = 0
        If entrycount = 0 Then
            LogAppend("No items found for character " & charguid.ToString & " -> Skipping", "CharacterArmorHandler_loadAtArcemu", True, False)
            Exit Sub
        End If
        Do
            Try
                itemguid = CInt(Val((tempdt.Rows(loopcounter).Item(0)).ToString))
                itementry = CInt(Val((tempdt.Rows(loopcounter).Item(1)).ToString))
                itemslot = CInt(Val((tempdt.Rows(loopcounter).Item(2)).ToString))
                If itemslot > 18 Then
                    loopcounter += 1
                    Continue Do
                End If
                Select Case itemslot
                    Case 0
                        slotname = "head"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 1
                        slotname = "neck"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 2
                        slotname = "shoulder"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 3
                        slotname = "shirt"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 4
                        slotname = "chest"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 5
                        slotname = "waist"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 6
                        slotname = "legs"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 7
                        slotname = "feet"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 8
                        slotname = "wrists"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 9
                        slotname = "hands"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 10
                        slotname = "finger1"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 11
                        slotname = "finger2"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 12
                        slotname = "trinket1"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 13
                        slotname = "trinket2"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 14
                        slotname = "back"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 15
                        slotname = "main"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 16
                        slotname = "off"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 17
                        slotname = "distance"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 18
                        slotname = "tabard"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case Else : End Select

            Catch ex As Exception
                LogAppend("Something went wrong! -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterArmorHandler_loadAtArcemu", False, True)
                loopcounter += 1
                Continue Do
            End Try
            loopcounter += 1
        Loop Until loopcounter = entrycount
        End Sub
    Private Shared Sub loadAtTrinity(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)
        LogAppend("Loading character Armor @loadAtTrinity", "CharacterArmorHandler_loadAtTrinity", False)
        Dim tempdt As DataTable = ReturnDataTable("SELECT item, slot, itemEntry FROM `character_inventory` JOIN `item_instance` ON `character_inventory`.item = " &
                                                  "`item_instance`.guid WHERE `character_inventory`.guid='" & charguid.ToString() & "' AND bag='0' AND slot < '19'")
        Dim itemguid As Integer
        Dim slotname As String
        Dim itementry As Integer
        Dim itemslot As Integer
        Dim entrycount As Integer = CInt(Val(tempdt.Rows.Count))
        Dim loopcounter As Integer = 0
        If entrycount = 0 Then
            LogAppend("No items found for character " & charguid.ToString & " -> Skipping", "CharacterArmorHandler_loadAtTrinity", True, False)
            Exit Sub
        End If
        Do
            Try
                itemguid = CInt(Val((tempdt.Rows(loopcounter).Item(0)).ToString))
                itemslot = CInt(Val((tempdt.Rows(loopcounter).Item(1)).ToString))
                itementry = CInt(Val((tempdt.Rows(loopcounter).Item(2)).ToString))
                Select Case itemslot
                    Case 0
                        slotname = "head"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 1
                        slotname = "neck"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 2
                        slotname = "shoulder"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 3
                        slotname = "shirt"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 4
                        slotname = "chest"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 5
                        slotname = "waist"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 6
                        slotname = "legs"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 7
                        slotname = "feet"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 8
                        slotname = "wrists"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 9
                        slotname = "hands"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 10
                        slotname = "finger1"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 11
                        slotname = "finger2"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 12
                        slotname = "trinket1"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 13
                        slotname = "trinket2"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 14
                        slotname = "back"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 15
                        slotname = "main"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 16
                        slotname = "off"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 17
                        slotname = "distance"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 18
                        slotname = "tabard"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                        End If
                    Case Else : End Select

            Catch ex As Exception
                LogAppend("Something went wrong! -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterArmorHandler_loadAtTrinity", False, True)
                loopcounter += 1
                Continue Do
            End Try
            loopcounter += 1
        Loop Until loopcounter = entrycount
    End Sub
    Private Shared Sub loadAtTrinityTBC(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)

    End Sub
    Private Shared Sub loadAtMangos(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)
        LogAppend("Loading character Armor @loadAtMangos", "CharacterArmorHandler_loadAtMangos", False)
        Dim tempdt As DataTable = ReturnDataTable("SELECT item, slot, item_template FROM `character_inventory` WHERE guid='" & charguid.ToString() & "' AND bag='0' AND slot < '19'")
        Dim itemguid As Integer
        Dim slotname As String
        Dim itementry As Integer
        Dim itemslot As Integer
        Dim entrycount As Integer = CInt(Val(tempdt.Rows.Count))
        Dim loopcounter As Integer = 0
        If entrycount = 0 Then
            LogAppend("No items found for character " & charguid.ToString & " -> Skipping", "CharacterArmorHandler_loadAtMangos", True, False)
            Exit Sub
        End If
        Do
            Try
                itemguid = CInt(Val((tempdt.Rows(loopcounter).Item(0)).ToString))
                itemslot = CInt(Val((tempdt.Rows(loopcounter).Item(1)).ToString))
                itementry = CInt(Val((tempdt.Rows(loopcounter).Item(2)).ToString))
                Select Case itemslot
                    Case 0
                        slotname = "head"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 1
                        slotname = "neck"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 2
                        slotname = "shoulder"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 3
                        slotname = "shirt"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 4
                        slotname = "chest"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 5
                        slotname = "waist"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 6
                        slotname = "legs"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 7
                        slotname = "feet"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 8
                        slotname = "wrists"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 9
                        slotname = "hands"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 10
                        slotname = "finger1"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 11
                        slotname = "finger2"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 12
                        slotname = "trinket1"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 13
                        slotname = "trinket2"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 14
                        slotname = "back"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 15
                        slotname = "main"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 16
                        slotname = "off"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 17
                        slotname = "distance"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case 18
                        slotname = "tabard"
                        If itementry > 1 Then
                            SetTemporaryCharacterInformation("@character_" & slotname & "Id", itementry.ToString(), tar_setId)
                            GetItemStats(itemguid, slotname, tar_setId)
                            LoadWeaponType(itementry, tar_setId) : End If
                    Case Else : End Select
            Catch ex As Exception
                LogAppend("Something went wrong! -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterArmorHandler_loadAtMangos", False, True)
                loopcounter += 1
                Continue Do
            End Try
            loopcounter += 1
        Loop Until loopcounter = entrycount
    End Sub
End Class
