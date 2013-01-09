Imports Namcore_Studio.EventLogging
Imports Namcore_Studio.Basics
Imports Namcore_Studio.GlobalVariables
Imports Namcore_Studio.CommandHandler
Imports Namcore_Studio.Conversions
Public Class CharacterInventoryHandler
    Public Shared Sub GetCharacterInventory(ByVal characterGuid As Integer, ByVal setId As Integer, ByVal accountId As Integer)
        LogAppend("Loading character Inventory for characterGuid: " & characterGuid & " and setId: " & setId, "CharacterInventoryHandler_GetCharacterInventory", True)
        Select Case sourceCore
            Case "arcemu"
                loadAtArcemu(characterGuid, setId, accountId)
            Case "trinity"
                loadAtTrinity(characterGuid, setId, accountId)
            Case "trinitytbc"
                loadAtTrinityTBC(characterGuid, setId, accountId)
            Case "mangos"
                loadAtMangos(characterGuid, setId, accountId)
            Case Else

        End Select

    End Sub
    Private Shared Sub loadAtArcemu(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)
        LogAppend("Loading character Inventory @loadAtArcemu", "CharacterInventoryHandler_loadAtArcemu", False)
        Dim dt As DataTable = ReturnDataTable("SELECT slot FROM playeritems WHERE guid='" & charguid.ToString() & "'")
        Dim templistzero As New List(Of String)
        Dim templist As New List(Of String)
        Dim tmpext As Integer
        Dim slotlist As String = ""
        Try
            Dim lastcount As Integer = CInt(Val(dt.Rows.Count.ToString))
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim readedcode As String = (dt.Rows(count).Item(0)).ToString
                    If Not slotlist.Contains("#" & readedcode & "#") Then
                        slotlist = slotlist & "#" & readedcode & "#"
                        tmpext = CInt(Val(readedcode))
                        Dim numresults As Integer = ReturnCountResults("SELECT containerslot FROM playeritems WHERE ownerguid='" & charguid.ToString & "' AND slot='" & tmpext.ToString & "'")
                        If numresults = 1 Then
                            Dim containerslot As String = runSQLCommand_characters_string("SELECT containerslot FROM playeritems WHERE ownerguid='" & charguid.ToString & "' AND slot='" & tmpext.ToString & "'")
                            Dim bagguid As String = "-1"
                            If Not containerslot = "-1" Then
                                bagguid = runSQLCommand_characters_string("SELECT guid FROM playeritems WHERE ownerguid='" & charguid.ToString & "' AND slot='" & containerslot & "' AND containerslot='-1'")
                            End If
                            If bagguid = "-1" Then
                                If tmpext > 18 Then
                                    Dim bag As String = "0"
                                    Dim item As String = "0"
                                    Dim entryid As String
                                    Dim enchantments As String
                                    Dim itemcount As String = "1"
                                    bag = bagguid
                                    item = runSQLCommand_characters_string("SELECT guid FROM playeritems WHERE ownerguid='" & charguid.ToString & "' AND slot='" & tmpext.ToString & "' AND containerslot='-1'")
                                    entryid = runSQLCommand_characters_string("SELECT entry FROM playeritems WHERE guid = '" & item & "'")
                                    enchantments = runSQLCommand_characters_string("SELECT enchantments FROM playeritems WHERE guid='" & item & "'")
                                    itemcount = runSQLCommand_characters_string("SELECT count FROM playeritems WHERE guid='" & item & "'")
                                    templistzero.Add(
                                        "<slot>" & tmpext.ToString & "</slot>" &
                                        "<bag>" & bag & "</bag>" &
                                        "<bagguid>" & bagguid & "</bagguid>" &
                                        "<item>" & entryid & "</item>" &
                                        "<enchant>" & enchantments & "</enchant>" &
                                        "<count>" & itemcount & "</count>" &
                                        "<container>-1</container>" &
                                        "<oldguid>" & item & "</oldguid>")
                                End If
                            Else
                                Dim bag As String = "0"
                                Dim item As String = "0"
                                Dim entryid As String
                                Dim enchantments As String
                                Dim itemcount As String = "1"
                                bag = runSQLCommand_characters_string("SELECT entry FROM playeritems WHERE guid = '" & bagguid & "'")
                                item = runSQLCommand_characters_string("SELECT guid FROM playeritems WHERE ownerguid='" & charguid.ToString & "' AND slot='" & tmpext.ToString & "'")
                                entryid = runSQLCommand_characters_string("SELECT entry FROM playeritems WHERE guid = '" & item & "'")
                                enchantments = runSQLCommand_characters_string("SELECT enchantments FROM playeritems WHERE guid='" & item & "'")
                                itemcount = runSQLCommand_characters_string("SELECT count FROM playeritems WHERE guid='" & item & "'")
                                templist.Add(
                                         "<slot>" & tmpext.ToString & "</slot>" &
                                         "<bag>" & bag & "</bag>" &
                                         "<bagguid>" & bagguid & "</bagguid>" &
                                         "<item>" & entryid & "</item>" &
                                         "<enchant>" & enchantments & "</enchant>" &
                                         "<count>" & itemcount & "</count>" &
                                         "<container>-1</container>" &
                                         "<oldguid>" & item & "</oldguid>")
                            End If
                        Else
                            Dim containerslot As String = ReturnResultWithRow("SELECT containerslot FROM playeritems WHERE ownerguid='" & charguid.ToString & "' AND slot='" & tmpext.ToString & "'", "containerslot", 0)
                            Dim bagguid As String = "-1"
                            If Not containerslot = "-1" Then
                                bagguid = runSQLCommand_characters_string("SELECT guid FROM playeritems WHERE ownerguid='" & charguid.ToString & "' AND slot='" & containerslot & "' AND containerslot='-1'")
                            End If
                            If bagguid = "-1" Then
                                If tmpext > 18 Then
                                    Dim bag As String = "0"
                                    Dim item As String = "0"
                                    Dim entryid As String
                                    Dim enchantments As String
                                    Dim itemcount As String = "1"
                                    bag = bagguid
                                    item = runSQLCommand_characters_string("SELECT guid FROM playeritems WHERE ownerguid='" & charguid.ToString & "' AND slot='" & tmpext.ToString & "' AND containerslot='-1'")
                                    entryid = runSQLCommand_characters_string("SELECT entry FROM playeritems WHERE guid = '" & item & "'")
                                    enchantments = runSQLCommand_characters_string("SELECT enchantments FROM playeritems WHERE guid='" & item & "'")
                                    itemcount = runSQLCommand_characters_string("SELECT count FROM playeritems WHERE guid='" & item & "'")
                                    templistzero.Add(
                                        "<slot>" & tmpext.ToString & "</slot>" &
                                        "<bag>" & bag & "</bag>" &
                                        "<bagguid>" & bagguid & "</bagguid>" &
                                        "<item>" & entryid & "</item>" &
                                        "<enchant>" & enchantments & "</enchant>" &
                                        "<count>" & itemcount & "</count>" &
                                        "<container>-1</container>")
                                End If
                            Else
                                Dim bag As String = "0"
                                Dim item As String = "0"
                                Dim entryid As String
                                Dim enchantments As String
                                Dim itemcount As String = "1"
                                bag = runSQLCommand_characters_string("SELECT entry FROM playeritems WHERE guid = '" & bagguid & "'")
                                item = ReturnResultWithRow("SELECT guid FROM playeritems WHERE ownerguid='" & charguid.ToString & "' AND slot='" & tmpext.ToString & "'", "guid", 1)
                                entryid = runSQLCommand_characters_string("SELECT entry FROM playeritems WHERE guid = '" & item & "'")
                                enchantments = runSQLCommand_characters_string("SELECT enchantments FROM playeritems WHERE guid='" & item & "'")
                                itemcount = runSQLCommand_characters_string("SELECT count FROM playeritems WHERE guid='" & item & "'")
                                templist.Add(
                                       "<slot>" & tmpext.ToString & "</slot>" &
                                       "<bag>" & bag & "</bag>" &
                                       "<bagguid>" & bagguid & "</bagguid>" &
                                       "<item>" & entryid & "</item>" &
                                       "<enchant>" & enchantments & "</enchant>" &
                                       "<count>" & itemcount & "</count>" &
                                       "<container>-1</container>" &
                                       "<oldguid>" & item & "</oldguid>")
                            End If
                            Dim containerslot2 As String = ReturnResultWithRow("SELECT containerslot FROM playeritems WHERE ownerguid='" & charguid.ToString & "' AND slot='" & tmpext.ToString & "'", "containerslot", 1)
                            Dim bagguid2 As String = "-1"
                            If Not containerslot2 = "-1" Then
                                bagguid2 = runSQLCommand_characters_string("SELECT guid FROM playeritems WHERE ownerguid='" & charguid.ToString & "' AND slot='" & containerslot2 & "' AND containerslot='-1'")
                            End If
                            If bagguid2 = "-1" Then
                                If tmpext > 18 Then
                                    Dim bag As String = "0"
                                    Dim item As String = "0"
                                    Dim entryid As String
                                    Dim enchantments As String
                                    Dim itemcount As String = "1"
                                    bag = bagguid2
                                    item = ReturnResultWithRow("SELECT guid FROM playeritems WHERE ownerguid='" & charguid.ToString & "' AND slot='" & tmpext.ToString & "' AND containerslot='-1'", "guid", 1)
                                    entryid = runSQLCommand_characters_string("SELECT entry FROM playeritems WHERE guid = '" & item & "'")
                                    enchantments = runSQLCommand_characters_string("SELECT enchantments FROM playeritems WHERE guid='" & item & "'")
                                    itemcount = runSQLCommand_characters_string("SELECT count FROM playeritems WHERE guid='" & item & "'")
                                    templistzero.Add(
                                      "<slot>" & tmpext.ToString & "</slot>" &
                                      "<bag>" & bag & "</bag>" &
                                      "<bagguid>" & bagguid & "</bagguid>" &
                                      "<item>" & entryid & "</item>" &
                                      "<enchant>" & enchantments & "</enchant>" &
                                      "<count>" & itemcount & "</count>" &
                                      "<container>-1</container>")
                                End If
                            Else
                                Dim bag As String = "0"
                                Dim item As String = "0"
                                Dim entryid As String
                                Dim enchantments As String
                                Dim itemcount As String = "1"
                                bag = runSQLCommand_characters_string("SELECT entry FROM playeritems WHERE guid = '" & bagguid2 & "'")
                                item = ReturnResultWithRow("SELECT guid FROM playeritems WHERE ownerguid='" & charguid.ToString & "' AND slot='" & tmpext.ToString & "'", "guid", 1)
                                entryid = runSQLCommand_characters_string("SELECT entry FROM playeritems WHERE guid = '" & item & "'")
                                enchantments = runSQLCommand_characters_string("SELECT enchantments FROM playeritems WHERE guid='" & item & "'")
                                itemcount = runSQLCommand_characters_string("SELECT count FROM playeritems WHERE guid='" & item & "'")
                                templist.Add(
                                     "<slot>" & tmpext.ToString & "</slot>" &
                                     "<bag>" & bag & "</bag>" &
                                     "<bagguid>" & bagguid & "</bagguid>" &
                                     "<item>" & entryid & "</item>" &
                                     "<enchant>" & enchantments & "</enchant>" &
                                     "<count>" & itemcount & "</count>" &
                                     "<container>" & containerslot2 & "</container>" &
                                     "<oldguid>" & item & "</oldguid>")
                            End If
                        End If
                        count += 1
                    Else
                        count += 1
                    End If
                Loop Until count = lastcount
            End If
        Catch ex As Exception
            LogAppend("Something went wrong while loading character Inventory! -> skipping -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterInventoryHandler_loadAtArcemu", True, True)
            Exit Sub
        End Try
        SetTemporaryCharacterInformation("@character_inventory", ConvertListToString(templist), tar_setId)
        SetTemporaryCharacterInformation("@character_inventoryzero", ConvertListToString(templistzero), tar_setId)
    End Sub
    Private Shared Sub loadAtTrinity(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)
        LogAppend("Loading character Inventory @loadAtTrinity", "CharacterInventoryHandler_loadAtTrinity", False)
        Dim dt As DataTable = ReturnDataTable("SELECT item FROM character_inventory WHERE guid='" & charguid.ToString() & "'")
        Dim templist As New List(Of String)
        Dim templistzero As New List(Of String)
        Dim tmpext As Integer
        Try
            Dim lastcount As Integer = CInt(Val(dt.Rows.Count.ToString))
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim readedcode As String = (dt.Rows(count).Item(0)).ToString
                    tmpext = CInt(Val(readedcode))
                    Dim bagguid As String = runSQLCommand_characters_string("SELECT bag FROM character_inventory WHERE guid='" & charguid.ToString & "' AND item='" & tmpext.ToString & "'")
                    If CInt(bagguid) = 0 Then
                        If tmpext > 18 Then
                            Dim bag As String = "0"
                            Dim item As String = "0"
                            Dim entryid As String
                            Dim enchantments As String
                            Dim itemcount As String = "1"
                            Dim slot As String = "0"
                            bag = bagguid
                            item = tmpext.ToString()
                            entryid = runSQLCommand_characters_string("SELECT itemEntry FROM item_instance WHERE guid = '" & item & "'")
                            enchantments = runSQLCommand_characters_string("SELECT enchantments FROM item_instance WHERE guid = '" & item & "'")
                            itemcount = runSQLCommand_characters_string("Select `count` FROM item_instance WHERE guid='" & item & "'")
                            slot = runSQLCommand_characters_string("Select `slot` FROM character_inventory WHERE `item`='" & item & "'")
                            templistzero.Add(
                                "<slot>" & slot & "</slot>" &
                                "<bag>" & bag & "</bag>" &
                                "<bagguid>" & bagguid & "</bagguid>" &
                                "<item>" & entryid & "</item>" &
                                "<enchant>" & enchantments & "</enchant>" &
                                "<count>" & itemcount & "</count>" &
                                "<oldguid>" & item & "</oldguid>")
                        End If
                    Else
                        Dim bag As String = "0"
                        Dim item As String = "0"
                        Dim entryid As String
                        Dim enchantments As String
                        Dim itemcount As String = "1"
                        Dim slot As String = "0"
                        bag = runSQLCommand_characters_string("SELECT itemEntry FROM item_instance WHERE guid = '" & bagguid & "'")
                        item = tmpext.ToString
                        entryid = runSQLCommand_characters_string("SELECT itemEntry FROM item_instance WHERE guid = '" & item & "'")
                        enchantments = runSQLCommand_characters_string("SELECT enchantments FROM item_instance WHERE guid = '" & item & "'")
                        itemcount = runSQLCommand_characters_string("Select `count` FROM item_instance WHERE guid='" & item & "'")
                        slot = runSQLCommand_characters_string("Select `slot` FROM character_inventory WHERE `item`='" & item & "'")
                        templist.Add(
                            "<slot>" & slot & "</slot>" &
                            "<bag>" & bag & "</bag>" &
                            "<bagguid>" & bagguid & "</bagguid>" &
                            "<item>" & entryid & "</item>" &
                            "<enchant>" & enchantments & "</enchant>" &
                            "<count>" & itemcount & "</count>")
                    End If
                    count += 1
                Loop Until count = lastcount
            Else
                LogAppend("No Inventory found!", "CharacterInventoryHandler_loadAtTrinity", True)
            End If
        Catch ex As Exception
            LogAppend("Something went wrong while loading character Inventory! -> skipping -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterInventoryHandler_loadAtTrinity", True, True)
            Exit Sub
        End Try
        SetTemporaryCharacterInformation("@character_inventory", ConvertListToString(templist), tar_setId)
        SetTemporaryCharacterInformation("@character_inventoryzero", ConvertListToString(templistzero), tar_setId)
    End Sub
    Private Shared Sub loadAtTrinityTBC(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)

    End Sub
    Private Shared Sub loadAtMangos(ByVal charguid As Integer, ByVal tar_setId As Integer, ByVal tar_accountId As Integer)
        LogAppend("Loading character Inventory @loadAtMangos", "CharacterInventoryHandler_loadAtMangos", False)
        Dim dt As DataTable = ReturnDataTable("SELECT item FROM character_inventory WHERE guid='" & charguid.ToString() & "'")
        Dim templist As New List(Of String)
        Dim templistzero As New List(Of String)
        Dim tmpext As Integer
        Try
            Dim lastcount As Integer = CInt(Val(dt.Rows.Count.ToString))
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim readedcode As String = (dt.Rows(count).Item(0)).ToString
                    tmpext = CInt(Val(readedcode))
                    Dim bagguid As String = runSQLCommand_characters_string("SELECT bag FROM character_inventory WHERE guid='" & charguid.ToString & "' AND item='" & tmpext.ToString & "'")
                    If CInt(bagguid) = 0 Then
                        If tmpext > 18 Then
                            Dim bag As String = "0"
                            Dim item As String = "0"
                            Dim entryid As String
                            Dim enchantments As String
                            Dim itemcount As String = "1"
                            Dim slot As String = "0"
                            bag = bagguid
                            item = tmpext.ToString()
                            entryid = runSQLCommand_characters_string("SELECT item_template FROM character_inventory WHERE guid = '" & charguid.ToString & "' AND item='" & item & "'")
                            enchantments = runSQLCommand_characters_string("SELECT `data` FROM item_instance WHERE guid = '" & item & "'")
                            itemcount = splititemdata(enchantments, 14)
                            slot = runSQLCommand_characters_string("Select `slot` FROM character_inventory WHERE `item`='" & item & "'")
                            templistzero.Add(
                                "<slot>" & slot & "</slot>" &
                                "<bag>" & bag & "</bag>" &
                                "<bagguid>" & bagguid & "</bagguid>" &
                                "<item>" & entryid & "</item>" &
                                "<enchant>" & enchantments & "</enchant>" &
                                "<count>" & itemcount & "</count>" &
                                "<oldguid>" & item & "</oldguid>")
                        End If
                    Else
                        Dim bag As String = "0"
                        Dim item As String = "0"
                        Dim entryid As String
                        Dim enchantments As String
                        Dim itemcount As String = "1"
                        Dim slot As String = "0"
                        bag = runSQLCommand_characters_string("SELECT item_template FROM character_inventory WHERE item = '" & bagguid & "'")
                        item = tmpext.ToString
                        entryid = runSQLCommand_characters_string("SELECT item_template FROM character_inventory WHERE guid = '" & charguid.ToString & "' AND item='" & tmpext.ToString & "'")
                        enchantments = runSQLCommand_characters_string("SELECT `data` FROM item_instance WHERE guid = '" & item & "'")
                        itemcount = splititemdata(enchantments, 14)
                        slot = runSQLCommand_characters_string("Select `slot` FROM character_inventory WHERE `item`='" & item & "'")
                        templist.Add(
                            "<slot>" & slot & "</slot>" &
                            "<bag>" & bag & "</bag>" &
                            "<bagguid>" & bagguid & "</bagguid>" &
                            "<item>" & entryid & "</item>" &
                            "<enchant>" & enchantments & "</enchant>" &
                            "<count>" & itemcount & "</count>")
                    End If
                    count += 1
                Loop Until count = lastcount
            Else
                LogAppend("No Inventory found!", "CharacterInventoryHandler_loadAtMangos", True)
            End If
        Catch ex As Exception
            LogAppend("Something went wrong while loading character Inventory! -> skipping -> Exception is: ###START###" & ex.ToString() & "###END###", "CharacterInventoryHandler_loadAtMangos", True, True)
            Exit Sub
        End Try
        SetTemporaryCharacterInformation("@character_inventory", ConvertListToString(templist), tar_setId)
        SetTemporaryCharacterInformation("@character_inventoryzero", ConvertListToString(templistzero), tar_setId)
    End Sub
    Private Shared Function splititemdata(ByVal datastring As String, ByVal position As Integer) As String
        Try
            Dim parts() As String = datastring.Split(" "c)
            Return parts(position)
        Catch
            Return "1"
        End Try
    End Function
End Class
