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
'*      /Filename:      SpellItemInformation
'*      /Description:   Includes functions for locating certain item and spell information
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.Drawing
Imports System.Net
Imports System.Windows.Forms
Imports NCFramework.Framework.Extension
Imports libnc.Provider
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Modules

Namespace Framework.Functions

    Public Module MiscInfo
        Public Sub LoadWeaponType(ByVal itemid As Integer, ByVal tarSet As Integer)
            If Not itemid = 0 Then
                LogAppend("Loading weapon type of Item " & itemid.ToString, "SpellItem_Information_LoadWeaponType", False)
                Try
                    Dim client As New WebClient
                    client.CheckProxy()
                    Dim player As Character = GetCharacterSetBySetId(tarSet)
                    '// TODO
                    Dim excerpt As String = SplitString(client.DownloadString("http://www.wowhead.com/item=" & itemid.ToString & "&xml"),
                                "<subclass id=", "</subclass>")
                    Select Case True
                        Case excerpt.ToLower.Contains(" crossbow ") '5011
                            player.Spells.Add(New Spell With {.Active = 1, .Disabled = 0, .Id = 5011})
                            player.Skills.Add(New Skill With {.Value = 100, .Max = 100, .Id = 226})
                        Case excerpt.ToLower.Contains(" bow ")
                            player.Spells.Add(New Spell With {.Active = 1, .Disabled = 0, .Id = 264})
                            player.Skills.Add(New Skill With {.Value = 100, .Max = 100, .Id = 45})
                        Case excerpt.ToLower.Contains(" gun ")
                            player.Spells.Add(New Spell With {.Active = 1, .Disabled = 0, .Id = 266})
                            player.Skills.Add(New Skill With {.Value = 100, .Max = 100, .Id = 46})
                        Case excerpt.ToLower.Contains(" thrown ")
                            player.Spells.Add(New Spell With {.Active = 1, .Disabled = 0, .Id = 2764})
                            player.Spells.Add(New Spell With {.Active = 1, .Disabled = 0, .Id = 2567})
                            player.Skills.Add(New Skill With {.Value = 100, .Max = 100, .Id = 176})
                        Case excerpt.ToLower.Contains(" wands ")
                            player.Spells.Add(New Spell With {.Active = 1, .Disabled = 0, .Id = 5009})
                            player.Spells.Add(New Spell With {.Active = 1, .Disabled = 0, .Id = 5019})
                            player.Skills.Add(New Skill With {.Value = 100, .Max = 100, .Id = 228})
                        Case excerpt.ToLower.Contains(" sword ")
                            If excerpt.ToLower.Contains(" one-handed ") Then
                                player.Spells.Add(New Spell With {.Active = 1, .Disabled = 0, .Id = 201})
                                player.Skills.Add(New Skill With {.Value = 100, .Max = 100, .Id = 43})
                            Else
                                player.Spells.Add(New Spell With {.Active = 1, .Disabled = 0, .Id = 201})
                                player.Skills.Add(New Skill With {.Value = 100, .Max = 100, .Id = 43})
                                player.Spells.Add(New Spell With {.Active = 1, .Disabled = 0, .Id = 202})
                                player.Skills.Add(New Skill With {.Value = 100, .Max = 100, .Id = 55})
                            End If
                        Case excerpt.ToLower.Contains(" dagger ")
                            player.Spells.Add(New Spell With {.Active = 1, .Disabled = 0, .Id = 1180})
                            player.Skills.Add(New Skill With {.Value = 100, .Max = 100, .Id = 173})
                        Case excerpt.ToLower.Contains(" axe ")
                            If excerpt.ToLower.Contains(" one-handed ") Then
                                player.Spells.Add(New Spell With {.Active = 1, .Disabled = 0, .Id = 196})
                                player.Skills.Add(New Skill With {.Value = 100, .Max = 100, .Id = 44})
                            Else
                                player.Spells.Add(New Spell With {.Active = 1, .Disabled = 0, .Id = 197})
                                player.Skills.Add(New Skill With {.Value = 100, .Max = 100, .Id = 44})
                                player.Spells.Add(New Spell With {.Active = 1, .Disabled = 0, .Id = 196})
                                player.Skills.Add(New Skill With {.Value = 100, .Max = 100, .Id = 142})
                            End If
                        Case excerpt.ToLower.Contains(" mace ")
                            If excerpt.ToLower.Contains(" one-handed ") Then
                                player.Spells.Add(New Spell With {.Active = 1, .Disabled = 0, .Id = 198})
                                player.Skills.Add(New Skill With {.Value = 100, .Max = 100, .Id = 54})
                            Else
                                player.Skills.Add(New Skill With {.Value = 100, .Max = 100, .Id = 54})
                                player.Spells.Add(New Spell With {.Active = 1, .Disabled = 0, .Id = 198})
                                player.Skills.Add(New Skill With {.Value = 100, .Max = 100, .Id = 160})
                                player.Spells.Add(New Spell With {.Active = 1, .Disabled = 0, .Id = 199})
                            End If
                        Case excerpt.ToLower.Contains(" polearm ")
                            player.Spells.Add(New Spell With {.Active = 1, .Disabled = 0, .Id = 200})
                            player.Skills.Add(New Skill With {.Value = 100, .Max = 100, .Id = 229})
                        Case excerpt.ToLower.Contains(" staff ")
                            player.Spells.Add(New Spell With {.Active = 1, .Disabled = 0, .Id = 227})
                            player.Skills.Add(New Skill With {.Value = 100, .Max = 100, .Id = 136})
                    End Select
                Catch ex As Exception
                    LogAppend("Error while loading weapon type! -> Exception is: ###START###" & ex.ToString() & "###END###",
                              "SpellItem_Information_LoadWeaponType", False, True)
                End Try
            Else
            End If
        End Sub
    End Module
End Namespace