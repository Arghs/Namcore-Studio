'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013-2015 NamCore Studio <https://github.com/megasus/Namcore-Studio>
'*
'* This program is free software; you can redistribute it and/or modify it
'* under the terms of the GNU General Public License as published by the
'* Free Software Foundation; either version 3 of the License, or (at your
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
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.Threading
Imports System.IO
Imports System.Text
Imports System.Reflection

Module Main
    Public AchievementCatDeDic As Dictionary(Of Integer, String())
    Public AchievementCatEnDic As Dictionary(Of Integer, String())
    Public AchievementDeDic As Dictionary(Of Integer, String())
    Public AchievementEnDic As Dictionary(Of Integer, String())
    Public AreaTableDic As Dictionary(Of Integer, String())
    Public EnchantmentDeDic As Dictionary(Of Integer, String())
    Public EnchantmentEnDic As Dictionary(Of Integer, String())
    Public FactionDeDic As Dictionary(Of Integer, String())
    Public FactionEnDic As Dictionary(Of Integer, String())
    Public FileDataDic As Dictionary(Of Integer, String())
    Public GlyphProps0Dic As Dictionary(Of Integer, String())
    Public GlyphProps1Dic As Dictionary(Of Integer, String())
    Public GlyphProps2Dic As Dictionary(Of Integer, String())
    Public GlyphProps3Dic As Dictionary(Of Integer, String())
    Public ItemAppearanceDic As Dictionary(Of Integer, String())
    Public ItemDic As Dictionary(Of Integer, String())
    Public ItemModifiedAppearanceDic As Dictionary(Of Integer, String())
    Public ItemSparseDeDic As Dictionary(Of Integer, String())
    Public ItemSparseEnDic As Dictionary(Of Integer, String())
    Public SkillLineAbilityDic As Dictionary(Of Integer, String())
    Public SkillLineDeDic As Dictionary(Of Integer, String())
    Public SkillLineEnDic As Dictionary(Of Integer, String())
    Public SpellDeDic As Dictionary(Of Integer, String())
    Public SpellEnDic As Dictionary(Of Integer, String())
    Public SpellIconDic As Dictionary(Of Integer, String())
    Public SpellLkDic As Dictionary(Of Integer, String())
    Public SplEffectDic As Dictionary(Of Integer, String())

    Sub InitDics()
        Log("Initializing structures...", LogLevel.NORMAL, , False)
        AchievementCatDeDic = New Dictionary(Of Integer, String())()
        AchievementCatDeDic.Add(0, {"int", "Id"})
        AchievementCatDeDic.Add(1, {"int", "MainCatId"})
        AchievementCatDeDic.Add(2, {"string", "Name"})

        AchievementCatEnDic = New Dictionary(Of Integer, String())()
        AchievementCatEnDic.Add(0, {"int", "Id"})
        AchievementCatEnDic.Add(1, {"int", "MainCatId"})
        AchievementCatEnDic.Add(2, {"string", "Name"})

        AchievementDeDic = New Dictionary(Of Integer, String())()
        AchievementDeDic.Add(0, {"int", "Id"})
        AchievementDeDic.Add(4, {"string", "Name"})
        AchievementDeDic.Add(5, {"string", "Description"})
        AchievementDeDic.Add(6, {"int", "CategoryId"})
        AchievementDeDic.Add(10, {"int", "IconId"})

        AchievementEnDic = New Dictionary(Of Integer, String())()
        AchievementEnDic.Add(0, {"int", "Id"})
        AchievementEnDic.Add(4, {"string", "Name"})
        AchievementEnDic.Add(5, {"string", "Description"})
        AchievementEnDic.Add(6, {"int", "CategoryId"})
        AchievementEnDic.Add(10, {"int", "IconId"})

        AreaTableDic = New Dictionary(Of Integer, String())()
        AreaTableDic.Add(0, {"int", "Id"})
        AreaTableDic.Add(10, {"string", "Name"})

        EnchantmentDeDic = New Dictionary(Of Integer, String())()
        EnchantmentDeDic.Add(0, {"int", "EffectId"})
        EnchantmentDeDic.Add(5, {"int", "Points1"})
        EnchantmentDeDic.Add(6, {"int", "Points2"})
        EnchantmentDeDic.Add(7, {"int", "Points3"})
        EnchantmentDeDic.Add(11, {"string", "EffectName"})
        EnchantmentDeDic.Add(14, {"int", "GemId"})

        EnchantmentEnDic = New Dictionary(Of Integer, String())()
        EnchantmentEnDic.Add(0, {"int", "EffectId"})
        EnchantmentEnDic.Add(5, {"int", "Points1"})
        EnchantmentEnDic.Add(6, {"int", "Points2"})
        EnchantmentEnDic.Add(7, {"int", "Points3"})
        EnchantmentEnDic.Add(11, {"string", "EffectName"})
        EnchantmentEnDic.Add(14, {"int", "GemId"})

        FactionDeDic = New Dictionary(Of Integer, String())()
        FactionDeDic.Add(0, {"int", "Id"})
        FactionDeDic.Add(1, {"int", "ReputationIndex"})
        FactionDeDic.Add(23, {"string", "Name"})

        FactionEnDic = New Dictionary(Of Integer, String())()
        FactionEnDic.Add(0, {"int", "Id"})
        FactionEnDic.Add(1, {"int", "ReputationIndex"})
        FactionEnDic.Add(23, {"string", "Name"})

        FileDataDic = New Dictionary(Of Integer, String())()
        FileDataDic.Add(0, {"int", "FileId"})
        FileDataDic.Add(1, {"string", "Name"})
        FileDataDic.Add(2, {"string", "Path"})

        GlyphProps0Dic = New Dictionary(Of Integer, String())()
        GlyphProps0Dic.Add(0, {"int", "Id"})
        GlyphProps0Dic.Add(1, {"int", "SpellId"})
        GlyphProps0Dic.Add(3, {"int", "Icon"})

        GlyphProps1Dic = New Dictionary(Of Integer, String())()
        GlyphProps1Dic.Add(0, {"int", "Id"})
        GlyphProps1Dic.Add(1, {"int", "SpellId"})
        GlyphProps1Dic.Add(3, {"int", "Icon"})

        GlyphProps2Dic = New Dictionary(Of Integer, String())()
        GlyphProps2Dic.Add(0, {"int", "Id"})
        GlyphProps2Dic.Add(1, {"int", "SpellId"})
        GlyphProps2Dic.Add(3, {"int", "Icon"})

        GlyphProps3Dic = New Dictionary(Of Integer, String())()
        GlyphProps3Dic.Add(0, {"int", "Id"})
        GlyphProps3Dic.Add(1, {"int", "SpellId"})
        GlyphProps3Dic.Add(3, {"int", "Icon"})

        ItemAppearanceDic = New Dictionary(Of Integer, String())()
        ItemAppearanceDic.Add(0, {"int", "AppearanceId"})
        ItemAppearanceDic.Add(2, {"int", "FileId"})

        ItemDic = New Dictionary(Of Integer, String())()
        ItemDic.Add(0, {"int", "Id"})
        ItemDic.Add(1, {"int", "ItemClass"})
        ItemDic.Add(2, {"int", "SubClass"})
        ItemDic.Add(7, {"int", "FileId"})

        ItemModifiedAppearanceDic = New Dictionary(Of Integer, String())()
        ItemModifiedAppearanceDic.Add(1, {"int", "ItemId"})
        ItemModifiedAppearanceDic.Add(3, {"int", "AppearanceId"})

        ItemSparseDeDic = New Dictionary(Of Integer, String())()
        ItemSparseDeDic.Add(0, {"int", "Id"})
        ItemSparseDeDic.Add(1, {"int", "Quality"})
        ItemSparseDeDic.Add(10, {"int", "InventoryType"})
        ItemSparseDeDic.Add(22, {"int", "MaxStack"})
        ItemSparseDeDic.Add(24, {"int", "SlotCount"})
        ItemSparseDeDic.Add(70, {"string", "ItemName"})
        ItemSparseDeDic.Add(87, {"int", "BagFamily"})

        ItemSparseEnDic = New Dictionary(Of Integer, String())()
        ItemSparseEnDic.Add(0, {"int", "Id"})
        ItemSparseEnDic.Add(1, {"int", "Quality"})
        ItemSparseEnDic.Add(10, {"int", "InventoryType"})
        ItemSparseEnDic.Add(22, {"int", "MaxStack"})
        ItemSparseEnDic.Add(24, {"int", "SlotCount"})
        ItemSparseEnDic.Add(70, {"string", "ItemName"})
        ItemSparseEnDic.Add(87, {"int", "BagFamily"})

        SkillLineAbilityDic = New Dictionary(Of Integer, String())()
        SkillLineAbilityDic.Add(0, {"int", "Id"})
        SkillLineAbilityDic.Add(1, {"int", "SkillId"})
        SkillLineAbilityDic.Add(2, {"int", "SpellId"})
        SkillLineAbilityDic.Add(8, {"int", "MaxSkill"})
        SkillLineAbilityDic.Add(9, {"int", "MinSkill"})

        SkillLineDeDic = New Dictionary(Of Integer, String())()
        SkillLineDeDic.Add(0, {"int", "Id"})
        SkillLineDeDic.Add(2, {"string", "Name"})

        SkillLineEnDic = New Dictionary(Of Integer, String())()
        SkillLineEnDic.Add(0, {"int", "Id"})
        SkillLineEnDic.Add(2, {"string", "Name"})

        SpellDeDic = New Dictionary(Of Integer, String())()
        SpellDeDic.Add(0, {"int", "Id"})
        SpellDeDic.Add(1, {"string", "Attributes"})
        SpellDeDic.Add(3, {"string", "Description"})

        SpellEnDic = New Dictionary(Of Integer, String())()
        SpellEnDic.Add(0, {"int", "Id"})
        SpellEnDic.Add(1, {"string", "Attributes"})
        SpellEnDic.Add(3, {"string", "Description"})

        SpellIconDic = New Dictionary(Of Integer, String())()
        SpellIconDic.Add(0, {"int", "Id"})
        SpellIconDic.Add(1, {"string", "Icon"})

        SpellLkDic = New Dictionary(Of Integer, String())()
        SpellLkDic.Add(0, {"int", "Id"})
        SpellLkDic.Add(110, {"int", "EffectMiscValue"})

        SplEffectDic = New Dictionary(Of Integer, String())()
        SplEffectDic.Add(11, {"int", "ItemId"})
        SplEffectDic.Add(13, {"int", "EffectId"})
        SplEffectDic.Add(27, {"int", "SpellId"})

        Thread.Sleep(1000)

        Log(" finished!", LogLevel.NORMAL, False)
    End Sub

    Sub Main()
        DisableTime = True
        Log(New String("-", 50), LogLevel.INFO)
        Log("* Welcome to the NamCore Data Processor (" & My.Application.Info.Version.ToString() & ")", LogLevel.INFO)
        Log("* Copyright (C) 2013-2015 NamCore Studio", LogLevel.INFO)
        Log("* Developed by Alcanmage/megasus", LogLevel.INFO)
        Log(New String("-", 50), LogLevel.INFO)
        Log("", LogLevel.INFO)
        Log("* Available commands:", LogLevel.INFO)
        Log("* /start [$skipvalue]", LogLevel.INFO)
        Log(vbTab & "Start extracting dbc data", LogLevel.INFO)
        Log("* /structure", LogLevel.INFO)
        Log(vbTab & "Lists required files", LogLevel.INFO)
        Log("* /exit", LogLevel.INFO)
        Log(vbTab & "Exit application", LogLevel.INFO)
        Do
            Log("", LogLevel.INFO)
            Log("Waiting for command...", LogLevel.NORMAL)
            Console.ForegroundColor = ConsoleColor.White
            Dim command As String = Console.ReadLine()
            InitDics()
            'Dim results As String = ""
            'For Each dra As FileInfo In New DirectoryInfo("C:\xampp\htdocs\DBFilesClient\").GetFiles()
            '    For Each val As String In (GetStringTable(dra.FullName)).Values
            '        If val.ToLower().Contains("mus_6") Then
            '            results &= val & vbNewLine
            '        End If
            '    Next
            'Next
            Dim mpaths As String = (From dtRow As DataRow In ReadDb("\Test\FileData2.dbc", FileDataDic).Rows Where dtRow("Name").ToString().ToLower().Contains("mus_6")).Aggregate("", Function(current, dtRow) current & (dtRow("Path") & dtRow("Name") & vbNewLine))
            Console.WriteLine("Found {0} relevant music entries", mpaths.Split(vbNewLine).Length - 1)
            Dim mapPaths As String = (From dtRow As DataRow In ReadDb("\Test\FileData2.dbc", FileDataDic).Rows Where dtRow("Path").ToString().ToLower().Contains("interface\worldmap")).Aggregate("", Function(current, dtRow) current & (dtRow("Path") & dtRow("Name") & vbNewLine))
            Console.WriteLine("Found {0} map entries", mapPaths.Split(vbNewLine).Length - 1)
            Dim areaPaths As String = ReadDb("\Test\AreaTable2.dbc", AreaTableDic).Rows.Cast(Of DataRow)().Aggregate("", Function(current, dtRow) current & (dtRow("Name") & vbNewLine))
            Console.WriteLine("Found {0} area entries", areaPaths.Split(vbNewLine).Length - 1)
            If command.StartsWith("/exit") Then
                End
            ElseIf command.StartsWith("/structure") Then
                Log("", LogLevel.INFO)
                Log("* Please make sure that DBC files and directories are located here:", LogLevel.INFO)
                Log("(" & Environment.CurrentDirectory & ")", LogLevel.INFO)
                Log("", LogLevel.INFO)
                Log("* Required files:", LogLevel.INFO)
                Log("", LogLevel.INFO)
                Log("  German localization", LogLevel.INFO)
                Log(vbTab & "\DE\Achievement.dbc", LogLevel.INFO)
                Log(vbTab & "\DE\Achievement_Category.dbc", LogLevel.INFO)
                Log(vbTab & "\DE\Faction.dbc", LogLevel.INFO)
                Log(vbTab & "\DE\Item-sparse.dbc", LogLevel.INFO)
                Log(vbTab & "\DE\SkillLine.dbc", LogLevel.INFO)
                Log(vbTab & "\DE\Spell.dbc", LogLevel.INFO)
                Log(vbTab & "\DE\SpellItemEnchantment.dbc", LogLevel.INFO)
                Log("", LogLevel.INFO)
                Log("  English localization", LogLevel.INFO)
                Log(vbTab & "\EN\Achievement.dbc", LogLevel.INFO)
                Log(vbTab & "\EN\Achievement_Category.dbc", LogLevel.INFO)
                Log(vbTab & "\EN\Faction.dbc", LogLevel.INFO)
                Log(vbTab & "\EN\Item-sparse.dbc", LogLevel.INFO)
                Log(vbTab & "\EN\SkillLine.dbc", LogLevel.INFO)
                Log(vbTab & "\EN\Spell.dbc", LogLevel.INFO)
                Log(vbTab & "\EN\SpellItemEnchantment.dbc", LogLevel.INFO)
                Log("", LogLevel.INFO)
                Log("  Shared", LogLevel.INFO)
                Log(vbTab & "\Shared\FileData.dbc", LogLevel.INFO)
                Log(vbTab & "\Shared\GlyphProperties0.dbc  (WotLK GlyphProperties.dbc)", LogLevel.INFO)
                Log(vbTab & "\Shared\GlyphProperties1.dbc  (Cata GlyphProperties.dbc)", LogLevel.INFO)
                Log(vbTab & "\Shared\GlyphProperties2.dbc  (MoP GlyphProperties.dbc)", LogLevel.INFO)
                Log(vbTab & "\Shared\GlyphProperties3.dbc  (WoD GlyphProperties.dbc)", LogLevel.INFO)
                Log(vbTab & "\Shared\Item.db2", LogLevel.INFO)
                Log(vbTab & "\Shared\ItemAppearance.db2", LogLevel.INFO)
                Log(vbTab & "\Shared\ItemModifiedAppearance.db2", LogLevel.INFO)
                Log(vbTab & "\Shared\SkillLineAbility.dbc", LogLevel.INFO)
                Log(vbTab & "\Shared\SpellEffect.dbc", LogLevel.INFO)
                Log(vbTab & "\Shared\SpellIcon.dbc", LogLevel.INFO)
                Log(vbTab & "\Shared\SpellWotLK.dbc (WotLK Spell.dbc)", LogLevel.INFO)
            ElseIf command.Length = 0 Then
                Continue Do
            ElseIf command.StartsWith("/start") Then
                If command.Contains(" ") Then
                    Dim parts() As String = command.Split(" "c)
                    If parts(1).Length > 0 Then
                        If IsNumeric(parts(1)) Then
                            Process(CInt(parts(1)))
                        Else
                            Log("Invalid syntax!", LogLevel.WARNING)
                            Log("* /start [$skipvalue]", LogLevel.INFO)
                        End If
                    Else
                        Log("Invalid syntax!", LogLevel.WARNING)
                        Log("* /start [$skipvalue]", LogLevel.INFO)
                    End If
                Else
                    Process()
                End If
            Else
                Log("Unknown command!", LogLevel.WARNING)
            End If
        Loop
    End Sub
    Sub DataTable2CSV(ByVal table As DataTable, ByVal filename As String)
        DataTable2CSV(table, filename, vbTab)
    End Sub
    Sub DataTable2CSV(ByVal table As DataTable, ByVal filename As String,
    ByVal sepChar As String)
        Dim writer As System.IO.StreamWriter
        Try
            writer = New System.IO.StreamWriter(filename)

            ' first write a line with the columns name
            Dim sep As String = ""
            Dim builder As New System.Text.StringBuilder
            For Each col As DataColumn In table.Columns
                builder.Append(sep).Append(col.ColumnName)
                sep = sepChar
            Next
            writer.WriteLine(builder.ToString())

            ' then write all the rows
            For Each row As DataRow In table.Rows
                sep = ""
                builder = New System.Text.StringBuilder

                For Each col As DataColumn In table.Columns
                    builder.Append(sep).Append(row(col.ColumnName))
                    sep = sepChar
                Next
                writer.WriteLine(builder.ToString())
            Next
        Finally
            If Not writer Is Nothing Then writer.Close()
        End Try
    End Sub
    Sub Process(Optional skipvalue As Integer = 0)
        DisableTime = False
        InitDics()
        Dim dbcPath As String
        Log("Start converting dbc files", LogLevel.NORMAL)

        If skipvalue < 1 Then
            Log("Converting Item-sparse.db2", LogLevel.NORMAL)
            Dim itemSparseDe As DataTable = Nothing
            Dim itemSparseEn As DataTable = Nothing
            dbcPath = "\DE\Item-sparse.db2"
            If CheckExistence(dbcPath) Then
                itemSparseDe = ReadDb(dbcPath, ItemSparseDeDic)
            End If
            dbcPath = "\EN\Item-sparse.db2"
            If CheckExistence(dbcPath) Then
                itemSparseEn = ReadDb(dbcPath, ItemSparseEnDic)
            End If
            If itemSparseDe IsNot Nothing And itemSparseEn IsNot Nothing Then
                Dim completeContent As String =
                        "ItemId£Quality£InventoryType£MaxStack£SlotCount£SpellId£ItemNameDE£ItemNameEN£BagFamily"
                Try
                    For i = 0 To itemSparseDe.Rows.Count - 1
                        ReportStatus(i + 1, itemSparseDe.Rows.Count)
                        Dim entry As DataRow = itemSparseDe(i)
                        completeContent &= vbNewLine &
                                           ContentBuilder(entry, "Id", "Quality", "InventoryType", "MaxStack",
                                                          "SlotCount", "ItemName") & "£" &
                                           LinkedContent(entry, itemSparseEn, "Id", "ItemName") & "£" &
                                           entry("BagFamily")
                    Next i
                    FileWriter("ItemSparse.csv", completeContent)
                Catch ex As Exception
                    Log("Something went wrong: " & ex.ToString(), LogLevel.CRITICAL)
                End Try
            Else
                Log("Cannot proceed with this file!", LogLevel.CRITICAL)
            End If
        Else
            Log("Skipping Item-sparse.db2", LogLevel.NORMAL)
        End If

        If skipvalue < 2 Then
            Log("Converting Item.db2", LogLevel.NORMAL)
            Dim itemDb2 As DataTable = Nothing
            Dim fileData As DataTable = Nothing
            Dim itemModAppearance As DataTable = Nothing
            Dim itemAppearance As DataTable = Nothing

            dbcPath = "\Shared\Item.db2"
            If CheckExistence(dbcPath) Then
                itemDb2 = ReadDb(dbcPath, ItemDic)
            End If
            dbcPath = "\Shared\FileData.dbc"
            If CheckExistence(dbcPath) Then
                fileData = ReadDb(dbcPath, FileDataDic)
            End If
            dbcPath = "\Shared\ItemModifiedAppearance.db2"
            If CheckExistence(dbcPath) Then
                itemModAppearance = ReadDb(dbcPath, ItemModifiedAppearanceDic)
            End If
            dbcPath = "\Shared\ItemAppearance.db2"
            If CheckExistence(dbcPath) Then
                itemAppearance = ReadDb(dbcPath, ItemAppearanceDic)
            End If

            If _
                itemDb2 IsNot Nothing And fileData IsNot Nothing And itemModAppearance IsNot Nothing And
                itemAppearance IsNot Nothing Then
                Dim completeContent As String = "ItemId£Class£SubClass£Icon"
                Try
                    For i = 0 To itemDb2.Rows.Count - 1
                        ReportStatus(i + 1, itemDb2.Rows.Count)
                        Dim entry As DataRow = itemDb2(i)
                        Dim lContent As String = LinkedContent(entry, fileData, "FileId", "Name")
                        If lContent.Length > 0 Then
                            lContent = lContent.Remove(lContent.Length - 4)
                        Else
                            Dim dr() As DataRow = ExecuteDtSearch(itemAppearance, "AppearanceId",
                                                                  LinkedContent(entry, itemModAppearance, "Id", "ItemId",
                                                                                "AppearanceId"))
                            If dr IsNot Nothing Then
                                lContent = LinkedContent(dr(0), fileData, "FileId", "Name")
                                If lContent.Length > 0 Then lContent = lContent.Remove(lContent.Length - 4)
                            End If
                        End If
                        completeContent &= vbNewLine & ContentBuilder(entry, "Id", "ItemClass", "SubClass") & "£" &
                                           lContent
                    Next i
                    FileWriter("Item.csv", completeContent)
                Catch ex As Exception
                    Log("Something went wrong: " & ex.ToString(), LogLevel.CRITICAL)
                End Try
            Else
                Log("Cannot proceed with this file!", LogLevel.CRITICAL)
            End If
        Else
            Log("Skipping Item.db2", LogLevel.NORMAL)
        End If

        If skipvalue < 3 Then
            Log("Converting Achievement.dbc", LogLevel.NORMAL)
            Dim achievementDe As DataTable = Nothing
            Dim achievementEn As DataTable = Nothing
            dbcPath = "\DE\Achievement.dbc"
            If CheckExistence(dbcPath) Then
                achievementDe = ReadDb(dbcPath, AchievementDeDic)
            End If
            dbcPath = "\EN\Achievement.dbc"
            If CheckExistence(dbcPath) Then
                achievementEn = ReadDb(dbcPath, AchievementEnDic)
            End If
            If achievementDe IsNot Nothing And achievementEn IsNot Nothing Then
                Dim completeContent As String =
                        "AchievementId£NameDE£NameEN£DescriptionDE£DescriptionEN£CategoryId£IconId"
                Try
                    For i = 0 To achievementDe.Rows.Count - 1
                        ReportStatus(i + 1, achievementDe.Rows.Count)
                        Dim entry As DataRow = achievementDe(i)
                        completeContent &= vbNewLine & ContentBuilder(entry, "Id", "Name") & "£" &
                                           LinkedContent(entry, achievementEn, "Id", "Name") & "£" &
                                           entry("Description") & "£" &
                                           LinkedContent(entry, achievementEn, "Id", "Description") & "£" &
                                           entry("CategoryId") & "£" & entry("IconId")
                    Next i
                    FileWriter("Achievement.csv", completeContent)
                Catch ex As Exception
                    Log("Something went wrong: " & ex.ToString(), LogLevel.CRITICAL)
                End Try
            Else
                Log("Cannot proceed with this file!", LogLevel.CRITICAL)
            End If
        Else
            Log("Skipping Achievement.dbc", LogLevel.NORMAL)
        End If

        If skipvalue < 4 Then
            Log("Converting Achievement_Category.dbc", LogLevel.NORMAL)
            Dim avCatDe As DataTable = Nothing
            Dim avCatEn As DataTable = Nothing
            dbcPath = "\DE\Achievement_Category.dbc"
            If CheckExistence(dbcPath) Then
                avCatDe = ReadDb(dbcPath, AchievementCatDeDic)
            End If
            dbcPath = "\EN\Achievement_Category.dbc"
            If CheckExistence(dbcPath) Then
                avCatEn = ReadDb(dbcPath, AchievementCatEnDic)
            End If
            If avCatDe IsNot Nothing And avCatEn IsNot Nothing Then
                Dim completeContent As String = "CategoryId£MainCatId£NameDE£NameEN"
                Try
                    For i = 0 To avCatDe.Rows.Count - 1
                        ReportStatus(i + 1, avCatDe.Rows.Count)
                        Dim entry As DataRow = avCatDe(i)
                        completeContent &= vbNewLine & ContentBuilder(entry, "Id", "MainCatId", "Name") & "£" &
                                           LinkedContent(entry, avCatEn, "Id", "Name")
                    Next i
                    FileWriter("AchievementCategory.csv", completeContent)
                Catch ex As Exception
                    Log("Something went wrong: " & ex.ToString(), LogLevel.CRITICAL)
                End Try
            Else
                Log("Cannot proceed with this file!", LogLevel.CRITICAL)
            End If
        Else
            Log("Skipping Achievement_Category.dbc", LogLevel.NORMAL)
        End If

        If skipvalue < 5 Then
            Log("Converting SpellIcon.dbc", LogLevel.NORMAL)
            Dim spellIcon As DataTable = Nothing
            dbcPath = "\Shared\SpellIcon.dbc"
            If CheckExistence(dbcPath) Then
                spellIcon = ReadDb(dbcPath, SpellIconDic)
            End If
            If spellIcon IsNot Nothing Then
                Dim completeContent As String = "IconId£Icon"
                Try
                    For i = 0 To spellIcon.Rows.Count - 1
                        ReportStatus(i + 1, spellIcon.Rows.Count)
                        Dim entry As DataRow = spellIcon(i)
                        completeContent &= vbNewLine & ContentBuilder(entry, "Id", "Icon")
                    Next i
                    FileWriter("SpellIcon.csv", completeContent)
                Catch ex As Exception
                    Log("Something went wrong: " & ex.ToString(), LogLevel.CRITICAL)
                End Try
            Else
                Log("Cannot proceed with this file!", LogLevel.CRITICAL)
            End If
        Else
            Log("Skipping SpellIcon.dbc", LogLevel.NORMAL)
        End If

        If skipvalue < 6 Then
            Log("Converting Faction.dbc", LogLevel.NORMAL)
            Dim factionDe As DataTable = Nothing
            Dim factionEn As DataTable = Nothing
            dbcPath = "\DE\Faction.dbc"
            If CheckExistence(dbcPath) Then
                factionDe = ReadDb(dbcPath, FactionDeDic)
            End If
            dbcPath = "\EN\Faction.dbc"
            If CheckExistence(dbcPath) Then
                factionEn = ReadDb(dbcPath, FactionEnDic)
            End If
            If factionDe IsNot Nothing And factionEn IsNot Nothing Then
                Dim completeContent As String = "FactionId£Index£NameDE£NameEN"
                Try
                    For i = 0 To factionDe.Rows.Count - 1
                        ReportStatus(i + 1, factionDe.Rows.Count)
                        Dim entry As DataRow = factionDe(i)
                        completeContent &= vbNewLine & ContentBuilder(entry, "Id", "ReputationIndex", "Name") & "£" &
                                           LinkedContent(entry, factionEn, "Id", "Name")
                    Next i
                    FileWriter("Faction.csv", completeContent)
                Catch ex As Exception
                    Log("Something went wrong: " & ex.ToString(), LogLevel.CRITICAL)
                End Try
            Else
                Log("Cannot proceed with this file!", LogLevel.CRITICAL)
            End If
        Else
            Log("Skipping Faction.dbc", LogLevel.NORMAL)
        End If

        If skipvalue < 7 Then
            Log("Converting GlyphProperties0.dbc", LogLevel.NORMAL)
            Dim glyphProps As DataTable = Nothing
            dbcPath = "\Shared\GlyphProperties0.dbc"
            If CheckExistence(dbcPath) Then
                glyphProps = ReadDb(dbcPath, GlyphProps0Dic)
            End If
            If glyphProps IsNot Nothing Then
                Dim completeContent As String = "GlyphId£SpellId£Icon"
                Try
                    For i = 0 To glyphProps.Rows.Count - 1
                        ReportStatus(i + 1, glyphProps.Rows.Count)
                        Dim entry As DataRow = glyphProps(i)
                        completeContent &= vbNewLine & ContentBuilder(entry, "Id", "SpellId", "Icon")
                    Next i
                    FileWriter("GlyphProperties0.csv", completeContent)
                Catch ex As Exception
                    Log("Something went wrong: " & ex.ToString(), LogLevel.CRITICAL)
                End Try
            Else
                Log("Cannot proceed with this file!", LogLevel.CRITICAL)
            End If

            Log("Converting GlyphProperties1.dbc", LogLevel.NORMAL)
            Dim glyphProps1 As DataTable = Nothing
            dbcPath = "\Shared\GlyphProperties1.dbc"
            If CheckExistence(dbcPath) Then
                glyphProps1 = ReadDb(dbcPath, GlyphProps1Dic)
            End If
            If glyphProps1 IsNot Nothing Then
                Dim completeContent As String = "GlyphId£SpellId£Icon"
                Try
                    For i = 0 To glyphProps1.Rows.Count - 1
                        ReportStatus(i + 1, glyphProps1.Rows.Count)
                        Dim entry As DataRow = glyphProps1(i)
                        completeContent &= vbNewLine & ContentBuilder(entry, "Id", "SpellId", "Icon")
                    Next i
                    FileWriter("GlyphProperties1.csv", completeContent)
                Catch ex As Exception
                    Log("Something went wrong: " & ex.ToString(), LogLevel.CRITICAL)
                End Try
            Else
                Log("Cannot proceed with this file!", LogLevel.CRITICAL)
            End If

            Log("Converting GlyphProperties2.dbc", LogLevel.NORMAL)
            Dim glyphProps2 As DataTable = Nothing
            dbcPath = "\Shared\GlyphProperties2.dbc"
            If CheckExistence(dbcPath) Then
                glyphProps2 = ReadDb(dbcPath, GlyphProps2Dic)
            End If
            If glyphProps2 IsNot Nothing Then
                Dim completeContent As String = "GlyphId£SpellId£Icon"
                Try
                    For i = 0 To glyphProps2.Rows.Count - 1
                        ReportStatus(i + 1, glyphProps2.Rows.Count)
                        Dim entry As DataRow = glyphProps2(i)
                        completeContent &= vbNewLine & ContentBuilder(entry, "Id", "SpellId", "Icon")
                    Next i
                    FileWriter("GlyphProperties2.csv", completeContent)
                Catch ex As Exception
                    Log("Something went wrong: " & ex.ToString(), LogLevel.CRITICAL)
                End Try
            Else
                Log("Cannot proceed with this file!", LogLevel.CRITICAL)
            End If

            Log("Converting GlyphProperties3.dbc", LogLevel.NORMAL)
            Dim glyphProps3 As DataTable = Nothing
            dbcPath = "\Shared\GlyphProperties3.dbc"
            If CheckExistence(dbcPath) Then
                glyphProps3 = ReadDb(dbcPath, GlyphProps2Dic)
            End If
            If glyphProps3 IsNot Nothing Then
                Dim completeContent As String = "GlyphId£SpellId£Icon"
                Try
                    For i = 0 To glyphProps3.Rows.Count - 1
                        ReportStatus(i + 1, glyphProps3.Rows.Count)
                        Dim entry As DataRow = glyphProps3(i)
                        completeContent &= vbNewLine & ContentBuilder(entry, "Id", "SpellId", "Icon")
                    Next i
                    FileWriter("GlyphProperties3.csv", completeContent)
                Catch ex As Exception
                    Log("Something went wrong: " & ex.ToString(), LogLevel.CRITICAL)
                End Try
            Else
                Log("Cannot proceed with this file!", LogLevel.CRITICAL)
            End If
        Else
            Log("Skipping GlyphProperties.dbc", LogLevel.NORMAL)
        End If

        If skipvalue < 8 Then
            Log("Converting SkillLine.dbc", LogLevel.NORMAL)
            Dim skillLineDe As DataTable = Nothing
            Dim skillLineEn As DataTable = Nothing
            dbcPath = "\DE\SkillLine.dbc"
            If CheckExistence(dbcPath) Then
                skillLineDe = ReadDb(dbcPath, SkillLineDeDic)
            End If
            dbcPath = "\EN\SkillLine.dbc"
            If CheckExistence(dbcPath) Then
                skillLineEn = ReadDb(dbcPath, SkillLineEnDic)
            End If
            If skillLineDe IsNot Nothing And skillLineEn IsNot Nothing Then
                Dim completeContent As String = "SkillId£NameDE£NameEN"
                Try
                    For i = 0 To skillLineDe.Rows.Count - 1
                        ReportStatus(i + 1, skillLineDe.Rows.Count)
                        Dim entry As DataRow = skillLineDe(i)
                        completeContent &= vbNewLine & ContentBuilder(entry, "Id", "Name") & "£" &
                                           LinkedContent(entry, skillLineEn, "Id", "Name")
                    Next i
                    FileWriter("SkillLine.csv", completeContent)
                Catch ex As Exception
                    Log("Something went wrong: " & ex.ToString(), LogLevel.CRITICAL)
                End Try
            Else
                Log("Cannot proceed with this file!", LogLevel.CRITICAL)
            End If
        Else
            Log("Skipping SkillLine.dbc", LogLevel.NORMAL)
        End If

        If skipvalue < 9 Then
            Log("Converting Spell.dbc, SpellItemEnchantment.dbc and SpellEffect.dbc", LogLevel.NORMAL)
            Dim spellDe As DataTable = Nothing
            Dim spellEn As DataTable = Nothing
            Dim spellWotLk As DataTable = Nothing
            Dim effectDb As DataTable = Nothing
            Dim enchantmentDe As DataTable = Nothing
            Dim enchantmentEn As DataTable = Nothing

            dbcPath = "\DE\Spell.dbc"
            If CheckExistence(dbcPath) Then
                spellDe = ReadDb(dbcPath, SpellDeDic)
            End If
            dbcPath = "\EN\Spell.dbc"
            If CheckExistence(dbcPath) Then
                spellEn = ReadDb(dbcPath, SpellEnDic)
            End If
            dbcPath = "\DE\SpellItemEnchantment.dbc"
            If CheckExistence(dbcPath) Then
                enchantmentDe = ReadDb(dbcPath, EnchantmentDeDic)
            End If
            dbcPath = "\EN\SpellItemEnchantment.dbc"
            If CheckExistence(dbcPath) Then
                enchantmentEn = ReadDb(dbcPath, EnchantmentEnDic)
            End If
            dbcPath = "\Shared\SpellEffect.dbc"
            If CheckExistence(dbcPath) Then
                effectDb = ReadDb(dbcPath, SplEffectDic)
            End If
            dbcPath = "\Shared\SpellWotLK.dbc"
            If CheckExistence(dbcPath) Then
                spellWotLk = ReadDb(dbcPath, SpellLkDic)
            End If

            If spellDe IsNot Nothing And spellEn IsNot Nothing Then
                Dim completeContent As String = "SpellId£SpellNameDE£SpellNameEN£DescriptionDE"
                Try
                    For i = 0 To spellDe.Rows.Count - 1
                        ReportStatus(i + 1, spellDe.Rows.Count)
                        Dim entry As DataRow = spellDe(i)
                        Dim description As String = ""
                        If entry("Description").ToString().Length > 1 Then
                            description = "isavaliable"
                        End If
                        completeContent &= vbNewLine & ContentBuilder(entry, "Id", "Attributes") & "£" &
                                           LinkedContent(entry, spellEn, "Id", "Attributes") & "£" & description
                    Next i
                    FileWriter("Spell.csv", completeContent)
                Catch ex As Exception
                    Log("Something went wrong: " & ex.ToString(), LogLevel.CRITICAL)
                End Try
            Else
                Log("Cannot proceed with this file!", LogLevel.CRITICAL)
            End If

            If enchantmentDe IsNot Nothing And enchantmentEn IsNot Nothing Then
                Dim completeContent As String = "EffectId£GemId£Points1£Points2£Points3£EffectNameDE£EffectNameEN"
                Try
                    For i = 0 To enchantmentDe.Rows.Count - 1
                        ReportStatus(i + 1, enchantmentDe.Rows.Count)
                        Dim entry As DataRow = enchantmentDe(i)
                        completeContent &= vbNewLine &
                                           ContentBuilder(entry, "EffectId", "GemId", "Points1", "Points2", "Points3",
                                                          "EffectName") & "£" &
                                           LinkedContent(entry, enchantmentEn, "EffectId", "EffectName")
                    Next i
                    FileWriter("SpellEnchant.csv", completeContent)
                Catch ex As Exception
                    Log("Something went wrong: " & ex.ToString(), LogLevel.CRITICAL)
                End Try
            Else
                Log("Cannot proceed with this file!", LogLevel.CRITICAL)
            End If

            If effectDb IsNot Nothing And spellWotLk IsNot Nothing Then
                Dim completeContent As String = "ItemId£EffectId£SpellId£SpellId335"
                Try
                    For i = 0 To effectDb.Rows.Count - 1
                        ReportStatus(i + 1, effectDb.Rows.Count)
                        Dim entry As DataRow = effectDb(i)
                        completeContent &= vbNewLine & ContentBuilder(entry, "ItemId", "EffectId", "SpellId") & "£" &
                                           LinkedContent(entry, spellWotLk, "EffectId", "EffectMiscValue", "Id")
                    Next i
                    FileWriter("SpellEffect.csv", completeContent)
                Catch ex As Exception
                    Log("Something went wrong: " & ex.ToString(), LogLevel.CRITICAL)
                End Try
            Else
                Log("Cannot proceed with this file!", LogLevel.CRITICAL)
            End If
        Else
            Log("Skipping Spell.dbc, SpellItemEnchantment.dbc and SpellEffect.dbc", LogLevel.NORMAL)
        End If

        If skipvalue < 10 Then
            Log("Converting SkillLineAbility.dbc", LogLevel.NORMAL)
            Dim skillLine As DataTable = Nothing
            dbcPath = "\Shared\SkillLineAbility.dbc"
            If CheckExistence(dbcPath) Then
                skillLine = ReadDb(dbcPath, SkillLineAbilityDic)
            End If
            If skillLine IsNot Nothing Then
                Dim completeContent As String = "SkillId£SpellId£MinSkill"
                Try
                    For i = 0 To skillLine.Rows.Count - 1
                        ReportStatus(i + 1, skillLine.Rows.Count)
                        Dim entry As DataRow = skillLine(i)
                        completeContent &= vbNewLine & ContentBuilder(entry, "SkillId", "SpellId", "MinSkill")
                    Next i
                    FileWriter("SkillLineAbility.csv", completeContent)
                Catch ex As Exception
                    Log("Something went wrong: " & ex.ToString(), LogLevel.CRITICAL)
                End Try
            Else
                Log("Cannot proceed with this file!", LogLevel.CRITICAL)
            End If
        Else
            Log("Skipping SkillLineAbility.dbc", LogLevel.NORMAL)
        End If

        Log("")
        Log("Processor has finished! Press Enter to exit...")
        Console.ReadLine()
        End
    End Sub

    Sub FileWriter(name As String, content As String)
        Log("Writing file " & name, LogLevel.LOW)
        Dim path As String = Environment.CurrentDirectory & "\" & name
        Try
            Dim fileStream As FileStream = New FileStream(path, FileMode.OpenOrCreate, FileAccess.Write)
            Dim streamWriter As StreamWriter = New StreamWriter(fileStream, Encoding.UTF8)
            streamWriter.WriteLine(content)
            streamWriter.Close()
            fileStream.Close()
        Catch ex As Exception
            Log("Something went wrong: " & ex.ToString(), LogLevel.CRITICAL)
        End Try
    End Sub

    Function CheckExistence(path As String) As Boolean
        If My.Computer.FileSystem.FileExists(Environment.CurrentDirectory & path) Then
            Return True
        Else
            Log("File not found @ '" & Environment.CurrentDirectory & path & "'!", LogLevel.CRITICAL)
            Return False
        End If
    End Function

    Function ContentBuilder(dr As DataRow, ParamArray args As Object()) As String
        Dim content As String = args.Cast(Of String)().Aggregate("", Function(current, arg) current & (dr(arg) & "£"))
        If content.Length > 0 Then content = content.Remove(content.Length - 1)
        Return content
    End Function

    Function LinkedContent(refRow As DataRow, linkDt As DataTable, bindingAnchor As String, targetField As String) _
        As String
        Return LinkedContent(refRow, linkDt, bindingAnchor, bindingAnchor, targetField)
    End Function

    Function LinkedContent(refRow As DataRow, linkDt As DataTable, refBindingAnchor As String,
                           linkBindingAnchor As String, targetField As String) As String
        Dim dr() As DataRow = ExecuteDtSearch(linkDt, linkBindingAnchor, refRow(refBindingAnchor).ToString())
        If dr IsNot Nothing Then
            Return dr(0)(targetField)
        End If
        Return ""
    End Function


    Function ExecuteDtSearch(ByVal dt As DataTable, ByVal startfield As String, ByVal startvalue As String) As DataRow()
        Try
            Dim foundRows() As DataRow
            foundRows = dt.Select(startfield & " = '" & startvalue & "'")
            If foundRows.Length = 0 Then
                ' Log("",, False)
                ' Log("No matching results found!", LogLevel.WARNING)
                Return Nothing
            Else
                Return foundRows
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Module
