Imports Namcore_Studio.EventLogging
Imports Namcore_Studio.Conversions
Imports Namcore_Studio.SpellItem_Information
Imports Namcore_Studio.Basics
Public Class ItemParser
    Public Shared Sub loadItems(ByVal source As String, ByVal setId As Integer)
        Dim itemguid As Integer
        Dim slotname As String
        Dim itementry As Integer
        Dim itemslot As Integer
        Dim loopcounter As Integer = 0
        Dim slot_st
        Do
            Try
              
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
                        'slot 17 has been removed as of patch 5.0
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
    Private Function getItemInfo(ByVal slot As Integer, ByVal slotname As String, ByVal sourceCode As String) As Item
        Dim relevantItemContext As String = splitString(sourceCode, "<div data-id=""" & slot.ToString & """ data-type=", "</div>")
        Dim charItem As New Item
        '//Loading ID
        charItem.id = TryInt(splitString(relevantItemContext, "/item/", """ class=""item"""))
        If charItem.id = Nothing Then Return Nothing '//Item ID not found
        '//Loading Name
        charItem.name = splitString(relevantItemContext, "<span class=""name-shadow"">", "</span>")
        '//Loading Sockets
        Dim socketContext As String
        If relevantItemContext.Contains("<span class=""sockets"">") Then
            'sockets available
            socketContext = splitString(sourceCode & "</div>", "<span class=""sockets"">", "</div>")
            Dim socketCount As Integer = UBound(socketContext.Split("socket-"))

        End If
    End Function
End Class
