'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013-2014 NamCore Studio <https://github.com/megasus/Namcore-Studio>
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
'*
'* //FileInfo//
'*      /Filename:      LiveView
'*      /Description:   Main interface with following functions:
'*                      -List all accounts and characters
'*                      -Editing/Deleting/Transferring accounts and characters
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.Linq
Imports System.IO
Imports NCFramework.Framework
Imports NamCore_Studio.Forms.Character
Imports NCFramework.Framework.Database
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Core
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Modules
Imports NCFramework.Framework.Extension
Imports NamCore_Studio.Modules.Interface
Imports System.Threading
Imports NCFramework.Framework.Transmission
Imports NCFramework.Framework.Core.Remove

Namespace Forms
    Public Class LiveView
        '// Declaration
        Private _cmpFileListViewComparer As ListViewComparer
        Private _cmpFileListViewComparer2 As ListViewComparer
        Dim _checkchangestatus As Boolean = False
        Dim _targetAcccharTable As DataTable
        Dim _stretching As Boolean = False
        Dim _moving As Boolean = False
        Dim _transmissionBlocked As Boolean = False
        Dim _completeCharacterItems As List(Of ListViewItem)

        Private _ptMouseDownLocation As Point
        Private ReadOnly _context As SynchronizationContext = SynchronizationContext.Current
        Public Event OnCoreLoaded As EventHandler(Of CompletedEventArgs)
        Public Event OnTransmissionCompleted As EventHandler(Of CompletedEventArgs)

        Delegate Sub Prep(ByVal id As Integer, ByVal nxt As Boolean)

        Dim _filePath As String = ""
        '// Declaration

        Protected Overridable Sub OnCoreCompleted(ByVal e As CompletedEventArgs)
            RaiseEvent OnCoreLoaded(Me, e)
        End Sub

        Protected Overridable Sub OnTransmissionEnded(ByVal e As CompletedEventArgs)
            RaiseEvent OnTransmissionCompleted(Me, e)
        End Sub

        Public Structure Data2Thread
            Public Lite As Boolean
        End Structure

        Private Sub closeBt_MouseEnter(sender As Object, e As EventArgs) Handles highlighter4.MouseEnter
            CType(sender, PictureBox).BackgroundImage = My.Resources.bt_close_light
        End Sub

        Private Sub closeBt_MouseLeave(sender As Object, e As EventArgs) Handles highlighter4.MouseLeave
            CType(sender, PictureBox).BackgroundImage = My.Resources.bt_close
        End Sub

        Private Sub minimizeBt_MouseEnter(sender As Object, e As EventArgs) Handles highlighter5.MouseEnter
            CType(sender, PictureBox).BackgroundImage = My.Resources.bt_minimize_light
        End Sub

        Private Sub minimizeBt_MouseLeave(sender As Object, e As EventArgs) Handles highlighter5.MouseLeave
            CType(sender, PictureBox).BackgroundImage = My.Resources.bt_minimize
        End Sub

        Private Sub settingsBt_MouseEnter(sender As Object, e As EventArgs) Handles settings_pic.MouseEnter
            CType(sender, PictureBox).BackgroundImage = My.Resources.bt_settings_light
        End Sub

        Private Sub settingsBt_MouseLeave(sender As Object, e As EventArgs) Handles settings_pic.MouseLeave
            CType(sender, PictureBox).BackgroundImage = My.Resources.bt_settings
        End Sub

        Private Sub aboutBt_MouseEnter(sender As Object, e As EventArgs) Handles about_pic.MouseEnter
            CType(sender, PictureBox).BackgroundImage = My.Resources.bt_about_light
        End Sub

        Private Sub aboutBt_MouseLeave(sender As Object, e As EventArgs) Handles about_pic.MouseLeave
            CType(sender, PictureBox).BackgroundImage = My.Resources.bt_about
        End Sub

        Private Sub connect_bt_Click(sender As Object, e As EventArgs) Handles connect_bt.Click
            GlobalVariables.con_operator = 1
            DbConnect.Show()
        End Sub

        Public Sub Loadaccountsandchars()
            _completeCharacterItems = New List(Of ListViewItem)()
            NewProcessStatus()
            _cmpFileListViewComparer = New ListViewComparer(accountview)
            _cmpFileListViewComparer2 = New ListViewComparer(characterview)
            _checkchangestatus = False
            GlobalVariables.sourceCore = "trinity" 'for testing only
            Dim mAcInfoProc As AccountCharacterInformationProcessing = New AccountCharacterInformationProcessing
            GlobalVariables.acctable = mAcInfoProc.ReturnAccountTable(GlobalVariables.GlobalConnection_Realm,
                                                                      GlobalVariables.sourceStructure)
            GlobalVariables.chartable = mAcInfoProc.ReturnCharacterTable(GlobalVariables.GlobalConnection,
                                                                         GlobalVariables.sourceStructure)
            GlobalVariables.chartable.Columns.Add("setId")
            GlobalVariables.chartable.Columns.Add("accountSetId")
            GlobalVariables.acctable.Columns.Add("setId")
            characterview.Items.Clear()
            accountview.Items.Clear()
            Dim genSet As Integer = 0
            Dim accountSetIndex As String = ""
            For Each rowitem As DataRow In GlobalVariables.acctable.Rows
                genSet += 1
                Dim accountHandler As New AccountHandler
                accountHandler.LoadAccount(TryInt(rowitem.Item(0).ToString()), genSet)
                Dim account As Account = GetAccountSetBySetId(genSet)
                Dim str(4) As String
                Dim itm As ListViewItem
                str(0) = rowitem.Item(0).ToString()
                str(1) = rowitem.Item(1).ToString()
                str(2) = rowitem.Item(2).ToString()
                str(3) = rowitem.Item(3).ToString()
                If IsDBNull(rowitem.Item(4)) Then
                    str(4) = ""
                Else
                    str(4) = rowitem.Item(4).ToString()
                End If
                itm = New ListViewItem(str)
                account.Characters = New List(Of NCFramework.Framework.Modules.Character)()
                itm.Tag = account
                accountSetIndex = accountSetIndex & "[accountId:" & str(0) & "@SetId:" & genSet.ToString() & "]"
                rowitem.Item(5) = genSet.ToString()
                accountview.Items.Add(itm)
                accountview.EnsureVisible(accountview.Items.Count - 1)
            Next
            accountview.Update()
            For Each rowitem As DataRow In GlobalVariables.chartable.Rows
                Dim player As New NCFramework.Framework.Modules.Character()
                player.Name = "Error"
                player.Guid = 0
                Dim str(6) As String
                Dim itm As ListViewItem
                player.Guid = TryInt(rowitem.Item(0).ToString())
                str(0) = rowitem.Item(0).ToString()
                str(1) = rowitem.Item(1).ToString()
                player.Name = rowitem.Item(2).ToString()
                str(2) = rowitem.Item(2).ToString()
                player.Race = TryInt(rowitem.Item(3).ToString())
                str(3) = GetRaceNameById(TryInt(rowitem.Item(3).ToString()))
                player.Cclass = TryInt(rowitem.Item(4).ToString())
                str(4) = GetClassNameById(TryInt(rowitem.Item(4).ToString()))
                player.Gender = TryInt(rowitem.Item(5).ToString())
                str(5) = GetGenderNameById(TryInt(rowitem.Item(5).ToString()))
                player.Level = TryInt(rowitem.Item(6).ToString())
                str(6) = rowitem.Item(6).ToString()
                itm = New ListViewItem(str)
                genSet += 1
                rowitem.Item(7) = genSet.ToString()
                player.SetIndex = genSet
                itm.Tag = player
                characterview.Items.Add(itm)
                characterview.EnsureVisible(characterview.Items.Count - 1)
                Dim accountSet As Integer = TryInt(SplitString(accountSetIndex, "[accountId:" & str(1) & "@SetId:", "]"))
                rowitem.Item(8) = accountSet.ToString()
                player.AccountSet = accountSet
                AddCharacterSet(genSet, player, GetAccountSetBySetId(accountSet))
            Next
            GlobalVariables.modifiedAccTable = GlobalVariables.acctable.Copy
            GlobalVariables.modifiedCharTable = GlobalVariables.chartable.Copy
            characterview.Sort()
            characterview.Update()
            _checkchangestatus = True
            With accountview.Items
                acctotal.Text = "(" & .Count.ToString() & " Accounts total)"
                If .Count > 8 Then
                    filter_acc.Visible = True
                Else
                    filter_acc.Visible = False
                End If
            End With
            With characterview.Items
                chartotal.Text = "(" & .Count.ToString() & " Characters total)"
                If .Count > 8 Then
                    filter_acc.Visible = True
                Else
                    filter_acc.Visible = False
                End If
            End With
            refreshdb.Visible = True
            CloseProcessStatus()
        End Sub

        Public Sub loadInformationSets_Armory()
            LogAppend("loadInformationSets_Armory call", "LiveView_loadInformationSets_Armory", False)
            '// Setting all controls double buffered
            Dim controlLst As List(Of Control)
            controlLst = FindAllChildren()
            For Each itemControl As Control In controlLst
                itemControl.SetDoubleBuffered()
            Next
            connect_bt.Visible = False
            filter_acc.Visible = False
            filter_char.Visible = False
            Dim genGuid As Integer = 1
            _checkchangestatus = False
            GlobalVariables.ModAccountSets = GlobalVariables.globChars.AccountSets
            GlobalVariables.acctable = New DataTable()
            GlobalVariables.chartable = New DataTable()
            characterview.Items.Clear()
            accountview.Items.Clear()
            Dim armoryAccount As Account = GetAccountSetBySetId(0)
            Dim str(4) As String
            Dim itm As ListViewItem
            str(0) = "0"
            str(1) = "Armory"
            str(2) = "-"
            str(3) = "-"
            str(4) = "-"
            For i = 0 To 4
                GlobalVariables.acctable.Columns.Add("")
            Next
            GlobalVariables.acctable.Rows.Add(str)
            itm = New ListViewItem(str)
            accountview.Items.Add(itm)
            accountview.EnsureVisible(accountview.Items.Count - 1)
            accountview.Update()
            For i = 0 To 6
                GlobalVariables.chartable.Columns.Add("")
            Next
            For Each player As NCFramework.Framework.Modules.Character In armoryAccount.Characters
                ' If infoSet = "" Then genGuid += 1 : Continue For // Needs alternative check
                Dim cLstr(6) As String
                Dim cLitm As ListViewItem
                cLstr(0) = genGuid.ToString
                cLstr(1) = "Armory"
                cLstr(2) = player.Name
                cLstr(3) = GetRaceNameById(player.Race)
                cLstr(4) = GetClassNameById(player.Cclass)
                cLstr(5) = GetGenderNameById(player.Gender)
                cLstr(6) = player.Level.ToString
                cLitm = New ListViewItem(cLstr)
                GlobalVariables.acctable.Rows.Add(str)
                cLitm.Tag = player
                LogAppend("Adding character to characterview using generated Guid " & genGuid.ToString,
                          "LiveView_loadInformationSets_Armory", False)
                characterview.Items.Add(cLitm)
                characterview.EnsureVisible(characterview.Items.Count - 1)
                'If Not player.SetIndex = genGuid Then Throw New Exception("Player SetId does not match generated SetIndex!")
                genGuid += 1
            Next
            characterview.Sort()
            characterview.Update()
            GlobalVariables.modifiedAccTable = GlobalVariables.acctable.Clone()
            GlobalVariables.modifiedCharTable = GlobalVariables.chartable.Clone()
            _checkchangestatus = True
            acctotal.Text = "(" & accountview.Items.Count.ToString() & " Accounts total)"
            chartotal.Text = "(" & characterview.Items.Count.ToString() & " Characters total)"
            CloseProcessStatus()
        End Sub

        Public Sub loadInformationSets_Template()
            LogAppend("loadInformationSets_Template call", "LiveView_loadInformationSets_Template", False)
            '// Setting all controls double buffered
            Dim controlLst As List(Of Control)
            controlLst = FindAllChildren()
            For Each itemControl As Control In controlLst
                itemControl.SetDoubleBuffered()
            Next
            connect_bt.Visible = False
            filter_acc.Visible = True
            filter_char.Visible = True
            _checkchangestatus = False
            GlobalVariables.ModAccountSets = GlobalVariables.globChars.AccountSets
            characterview.Items.Clear()
            accountview.Items.Clear()
            GlobalVariables.acctable = New DataTable()
            GlobalVariables.chartable = New DataTable()
            For i = 0 To 6
                GlobalVariables.acctable.Columns.Add("")
            Next
            For i = 0 To 6
                GlobalVariables.chartable.Columns.Add("")
            Next
            For Each playerAccount As Account In GlobalVariables.globChars.AccountSets
                Dim str(4) As String
                Dim itm As ListViewItem
                str(0) = playerAccount.Id.ToString()
                str(1) = playerAccount.Name
                str(2) = playerAccount.GmLevel.ToString()
                str(3) = playerAccount.LastLogin.ToString()
                str(4) = playerAccount.Email
                itm = New ListViewItem(str)
                itm.Tag = playerAccount
                GlobalVariables.acctable.Rows.Add(str)
                accountview.Items.Add(itm)
                accountview.EnsureVisible(accountview.Items.Count - 1)
                For Each playerCharacter As NCFramework.Framework.Modules.Character In playerAccount.Characters
                    Dim cLstr(6) As String
                    Dim cLitm As ListViewItem
                    cLstr(0) = playerCharacter.Guid.ToString()
                    cLstr(1) = playerCharacter.AccountName
                    cLstr(2) = playerCharacter.Name
                    cLstr(3) = GetRaceNameById(playerCharacter.Race)
                    cLstr(4) = GetClassNameById(playerCharacter.Cclass)
                    cLstr(5) = GetGenderNameById(playerCharacter.Gender)
                    cLstr(6) = playerCharacter.Level.ToString
                    cLitm = New ListViewItem(cLstr)
                    cLitm.Tag = playerCharacter
                    GlobalVariables.chartable.Rows.Add(cLstr)
                    LogAppend("Adding character to characterview", "LiveView_loadInformationSets_Template", False)
                    characterview.Items.Add(cLitm)
                    characterview.EnsureVisible(characterview.Items.Count - 1)
                Next
            Next
            accountview.Update()
            characterview.Sort()
            characterview.Update()
            GlobalVariables.modifiedAccTable = GlobalVariables.acctable.Clone()
            GlobalVariables.modifiedCharTable = GlobalVariables.chartable.Clone()
            _checkchangestatus = True
            acctotal.Text = "(" & accountview.Items.Count.ToString() & " Accounts total)"
            chartotal.Text = "(" & characterview.Items.Count.ToString() & " Characters total)"
            CloseProcessStatus()
        End Sub

        Public Sub Loadtargetaccountsandchars()
            GlobalVariables.targetCore = "trinity" 'todo for testing only
            Dim mAcInfoProc As AccountCharacterInformationProcessing = New AccountCharacterInformationProcessing
            _targetAcccharTable = mAcInfoProc.ReturnTargetAccCharTable(GlobalVariables.TargetConnection_Realm,
                                                                       GlobalVariables.targetStructure)
            target_accounts_tree.Nodes.Clear()
            For Each rowitem As DataRow In _targetAcccharTable.Rows
                Dim foundNode() As TreeNode = target_accounts_tree.Nodes.Find(rowitem(0).ToString(), False)
                If foundNode.Length = 0 Then
                    Dim newnode As New TreeNode
                    Dim newAccount As New Account
                    newAccount.Id = TryInt(rowitem.Item(0).ToString())
                    newAccount.Name = rowitem.Item(1).ToString()
                    With newnode
                        .Name = newAccount.Id.ToString()
                        .Text = newAccount.Name
                        .Tag = newAccount
                    End With
                    target_accounts_tree.Nodes.Add(newnode)
                End If
                If Not IsDBNull(rowitem(2)) Or Not IsDBNull(rowitem(3)) Then
                    Dim node As TreeNode = target_accounts_tree.Nodes.Find(rowitem(0).ToString(), False)(0)
                    Dim subNode As New TreeNode
                    Dim newCharacter As New NCFramework.Framework.Modules.Character
                    newCharacter.Name = rowitem(3).ToString()
                    With subNode
                        .Name = rowitem(2).ToString()
                        .Text = rowitem(3).ToString()
                        .Tag = newCharacter
                    End With
                    node.Nodes.Add(subNode)
                End If
            Next
            target_accounts_tree.Update()
            CloseProcessStatus()
        End Sub

        Public Sub PrepareTemplateCreation()
            createTemplate_bt.Visible = True
            target_accounts_tree.Nodes.Clear()
            info1_lbl.Visible = False
            info2_lbl.Visible = False
        End Sub

        Public Sub LoadTargetTemplate()
            target_accounts_tree.Nodes.Clear()
            For Each rowitem As DataRow In _targetAcccharTable.Rows
                Dim foundNode() As TreeNode = target_accounts_tree.Nodes.Find(rowitem(0).ToString(), False)
                If foundNode.Length = 0 Then
                    Dim newnode As New TreeNode
                    With newnode
                        .Name = rowitem.Item(0).ToString()
                        .Text = rowitem.Item(1).ToString()
                    End With
                    target_accounts_tree.Nodes.Add(newnode)
                Else
                    Dim node As TreeNode = target_accounts_tree.Nodes.Find(rowitem(0).ToString(), False)(0)
                    Dim subNode As New TreeNode
                    With subNode
                        .Name = rowitem(2).ToString()
                        .Text = rowitem(3).ToString()
                    End With

                    node.Nodes.Add(subNode)
                End If
            Next
            target_accounts_tree.Update()
            CloseProcessStatus()
        End Sub

        Public Sub Setaccountview(ByVal accounttable As DataTable)
            _checkchangestatus = False
            GlobalVariables.sourceCore = "trinity" '// for testing only
            characterview.Items.Clear()
            accountview.Items.Clear()
            For Each rowitem As DataRow In accounttable.Rows
                Dim str(4) As String
                Dim itm As ListViewItem
                str(0) = rowitem.Item(0).ToString()
                str(1) = rowitem.Item(1).ToString()
                str(2) = rowitem.Item(2).ToString()
                str(3) = rowitem.Item(3).ToString()
                str(4) = rowitem.Item(4).ToString()
                itm = New ListViewItem(str)
                itm.Tag = GetAccountSetBySetId(TryInt(rowitem.Item(5).ToString()))
                accountview.Items.Add(itm)
                accountview.EnsureVisible(accountview.Items.Count - 1)
            Next
            accountview.Update()
            For Each accrowitem As DataRow In accounttable.Rows
                Dim myaccid As String = accrowitem.Item(0).ToString()
                For Each rowitem As DataRow In GlobalVariables.chartable.Rows
                    If rowitem(1).ToString() = myaccid Then
                        Dim str(6) As String
                        Dim itm As ListViewItem
                        str(0) = rowitem.Item(0).ToString()
                        str(1) = rowitem.Item(1).ToString()
                        str(2) = rowitem.Item(2).ToString()
                        str(3) = GetRaceNameById(TryInt(rowitem.Item(3).ToString()))
                        str(4) = GetClassNameById(TryInt(rowitem.Item(4).ToString()))
                        str(5) = GetGenderNameById(TryInt(rowitem.Item(5).ToString()))
                        str(6) = rowitem.Item(6).ToString()
                        itm = New ListViewItem(str)
                        itm.Tag = GetCharacterSetBySetId(TryInt(rowitem.Item(7).ToString()),
                                                         GetAccountSetBySetId(TryInt(rowitem.Item(8).ToString())))
                        characterview.Items.Add(itm)
                        characterview.EnsureVisible(characterview.Items.Count - 1)
                    End If
                Next
            Next
            characterview.Update()
            _checkchangestatus = True
            acctotal.Text = "(" & accountview.Items.Count.ToString() & " Accounts total)"
            chartotal.Text = "(" & characterview.Items.Count.ToString() & " Characters total)"
        End Sub

        Public Sub Setcharacterview(ByVal charactertable As DataTable)
            _checkchangestatus = False
            GlobalVariables.sourceCore = "trinity" 'for testing only
            characterview.Items.Clear()
            accountview.Items.Clear()
            For Each rowitem As DataRow In GlobalVariables.modifiedAccTable.Rows
                Dim str(4) As String
                Dim itm As ListViewItem
                str(0) = rowitem.Item(0).ToString()
                str(1) = rowitem.Item(1).ToString()
                str(2) = rowitem.Item(2).ToString()
                str(3) = rowitem.Item(3).ToString()
                str(4) = rowitem.Item(4).ToString()
                itm = New ListViewItem(str)
                accountview.Items.Add(itm)
                accountview.EnsureVisible(accountview.Items.Count - 1)
            Next
            accountview.Update()
            For Each accrowitem As DataRow In GlobalVariables.modifiedAccTable.Rows
                Dim myaccid As String = accrowitem.Item(0).ToString()
                For Each rowitem As DataRow In charactertable.Rows
                    If rowitem(1).ToString() = myaccid Then
                        Dim str(6) As String
                        Dim itm As ListViewItem
                        str(0) = rowitem.Item(0).ToString()
                        str(1) = rowitem.Item(1).ToString()
                        str(2) = rowitem.Item(2).ToString()
                        str(3) = GetRaceNameById(TryInt(rowitem.Item(3).ToString()))
                        str(4) = GetClassNameById(TryInt(rowitem.Item(4).ToString()))
                        str(5) = GetGenderNameById(TryInt(rowitem.Item(5).ToString()))
                        str(6) = rowitem.Item(6).ToString()
                        itm = New ListViewItem(str)
                        itm.Tag = GetCharacterSetBySetId(TryInt(rowitem.Item(7).ToString()),
                                                         GetAccountSetBySetId(TryInt(rowitem.Item(8).ToString())))
                        characterview.Items.Add(itm)
                        characterview.EnsureVisible(characterview.Items.Count - 1)
                    End If
                Next
            Next
            characterview.Update()
            _checkchangestatus = True
            acctotal.Text = "(" & accountview.Items.Count.ToString() & " Accounts total)"
            chartotal.Text = "(" & characterview.Items.Count.ToString() & " Characters total)"
        End Sub

        Private Sub liveview_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            Dim controlLst As List(Of Control)
            controlLst = FindAllChildren()
            For Each itemControl As Control In controlLst
                itemControl.SetDoubleBuffered()
            Next
            GlobalVariables.accountsToCreate = New List(Of Account)
            GlobalVariables.charactersToCreate = New List(Of NCFramework.Framework.Modules.Character)
            _cmpFileListViewComparer = New ListViewComparer(accountview)
            _cmpFileListViewComparer2 = New ListViewComparer(characterview)
        End Sub

        Private Sub accountview_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles accountview.ColumnClick
            If e.Column = _cmpFileListViewComparer.SortColumn Then
                If _cmpFileListViewComparer.SortOrder = SortOrder.Ascending Then
                    _cmpFileListViewComparer.SortOrder = SortOrder.Descending
                Else
                    _cmpFileListViewComparer.SortOrder = SortOrder.Ascending
                End If
            Else
                _cmpFileListViewComparer.SortOrder = SortOrder.Ascending
            End If

            _cmpFileListViewComparer.SortColumn = e.Column
            accountview.Sort()
        End Sub

        Private Sub charactertview_ColumnClick(sender As Object, e As ColumnClickEventArgs) _
            Handles characterview.ColumnClick
            If e.Column = _cmpFileListViewComparer2.SortColumn Then
                If _cmpFileListViewComparer2.SortOrder = SortOrder.Ascending Then
                    _cmpFileListViewComparer2.SortOrder = SortOrder.Descending
                Else
                    _cmpFileListViewComparer2.SortOrder = SortOrder.Ascending
                End If
            Else
                _cmpFileListViewComparer2.SortOrder = SortOrder.Ascending
            End If

            _cmpFileListViewComparer2.SortColumn = e.Column
            characterview.Sort()
        End Sub

        Private Sub accountview_ItemChecked1(sender As Object, e As ItemCheckedEventArgs) _
            Handles accountview.ItemChecked
            If GlobalVariables.armoryMode = True Then Exit Sub '// only test
            If _checkchangestatus = True Then
                If _completeCharacterItems Is Nothing Then _completeCharacterItems = New List(Of ListViewItem)()
                If Not accountview.CheckedItems.Count = 0 Then
                    Dim validAccountIds As List(Of Integer) =
                            (From checkedrow As Object In accountview.CheckedItems
                            Select TryInt(TryCast(checkedrow, ListViewItem).SubItems(0).Text)).ToList()
                    For i = characterview.Items.Count - 1 To 0 Step -1
                        Dim itm As ListViewItem = characterview.Items(i)
                        itm.Checked = True
                        Dim match As Boolean = False
                        For Each myAccId As Integer In validAccountIds
                            If itm.SubItems(1).Text = myAccId.ToString() Then
                                match = True
                            End If
                        Next
                        If match = False Then
                            _completeCharacterItems.Add(CType(itm.Clone(), ListViewItem))
                            characterview.Items.Remove(itm)
                        End If
                    Next i
                    For i = _completeCharacterItems.Count - 1 To 0 Step -1
                        Dim itm As ListViewItem = _completeCharacterItems.Item(i)
                        itm.Checked = True
                        Dim match As Boolean = False
                        For Each myAccId As Integer In validAccountIds
                            If itm.SubItems(1).Text = myAccId.ToString() Then
                                match = True
                            End If
                        Next
                        If match = True Then
                            characterview.Items.Add(CType(itm.Clone, ListViewItem))
                            _completeCharacterItems.Remove(itm)
                        End If
                    Next i
                    characterview.Update()
                Else
                    For i = _completeCharacterItems.Count - 1 To 0 Step -1
                        Dim itm As ListViewItem = _completeCharacterItems.Item(i)
                        itm.Checked = False
                        characterview.Items.Add(CType(itm.Clone, ListViewItem))
                        _completeCharacterItems.Remove(itm)
                    Next i
                    For Each itm As ListViewItem In characterview.Items
                        itm.Checked = False
                    Next
                    characterview.Update()
                End If
            End If
            acctotal.Text = "(" & accountview.Items.Count.ToString() & " Accounts total)"
            chartotal.Text = "(" & characterview.Items.Count.ToString() & " Characters total)"
        End Sub

        Private Sub accountview_SelectedIndexChanged(sender As Object, e As EventArgs) _
            Handles accountview.SelectedIndexChanged
        End Sub

        Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) _
            Handles checkall_acc.LinkClicked
            For Each xitem As ListViewItem In accountview.Items
                xitem.Checked = True
            Next
        End Sub

        Private Sub uncheckall_acc_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) _
            Handles uncheckall_acc.LinkClicked
            For Each xitem As ListViewItem In accountview.Items
                xitem.Checked = False
            Next
        End Sub

        Private Sub filter_acc_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) _
            Handles filter_acc.LinkClicked
            FilterAccounts.Show()
        End Sub

        Private Sub SelectedAccountsToolStripMenuItem_Click(sender As Object, e As EventArgs) _
            Handles SelectedAccountsToolStripMenuItem.Click
            Dim result =
                    MsgBox(
                        ResourceHandler.GetUserMessage("deleteacc") & " (" &
                        accountview.SelectedItems(0).SubItems(1).Text &
                        ")", vbYesNo, ResourceHandler.GetUserMessage("areyousure"))
            If result = MsgBoxResult.Yes Then
                Dim accountId As String = accountview.SelectedItems(0).SubItems(0).Text
                For I = 0 To accountview.SelectedItems.Count - 1
                    Dim accountRemoveHandler As New AccountRemoveHandler
                    accountRemoveHandler.RemoveAccountFromDb(CType(accountview.SelectedItems(0).Tag, Account), GlobalVariables.sourceCore, GlobalVariables.GlobalConnection_Realm, GlobalVariables.GlobalConnection, GlobalVariables.sourceStructure)
                    accountview.SelectedItems(I).Remove()
                    Dim toBeRemovedRow As DataRow() =
                            GlobalVariables.acctable.Select(
                                GlobalVariables.sourceStructure.acc_id_col(0) & " = '" & accountId & "'")
                    If Not toBeRemovedRow.Length = 0 Then GlobalVariables.acctable.Rows.Remove(toBeRemovedRow(0))
                Next
                Setaccountview(GlobalVariables.acctable)
            End If
        End Sub

        Private Sub CheckedAccountsToolStripMenuItem1_Click(sender As Object, e As EventArgs) _
            Handles CheckedAccountsToolStripMenuItem1.Click
            Dim result = MsgBox(ResourceHandler.GetUserMessage("deleteacc"), vbYesNo,
                                ResourceHandler.GetUserMessage("areyousure"))
            If result = MsgBoxResult.Yes Then
                For Each itm As ListViewItem In accountview.CheckedItems
                    Dim accountRemoveHandler As New AccountRemoveHandler
                    accountRemoveHandler.RemoveAccountFromDb(CType(itm.Tag, Account), GlobalVariables.sourceCore, GlobalVariables.GlobalConnection_Realm, GlobalVariables.GlobalConnection, GlobalVariables.sourceStructure)
                    accountview.Items.Remove(itm)
                    Dim toBeRemovedRow As DataRow() =
                            GlobalVariables.acctable.Select(
                                GlobalVariables.sourceStructure.acc_id_col(0) & " = '" & itm.SubItems(0).Text & "'")
                    If Not toBeRemovedRow.Length = 0 Then GlobalVariables.acctable.Rows.Remove(toBeRemovedRow(0))
                Next
                Setaccountview(GlobalVariables.acctable)
            End If
        End Sub

        Private Sub EditToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditToolStripMenuItem.Click
            '// Please remember that an account which is loaded from a database needs to be completely stored temporarily
            If _transmissionBlocked Then
                MsgBox(ResourceHandler.GetUserMessage("errWaitTillTransmissionCompleted"), MsgBoxStyle.Critical,
                       "Warning")
            Else
                Dim accview As New AccountOverview
                Dim accTag As Account = CType(accountview.SelectedItems(0).Tag, Account)
                If GlobalVariables.armoryMode = False Then
                    Userwait.Show()
                    accview.prepare_interface(GetAccountSetBySetId(accTag.SetIndex))
                    Userwait.Close()
                    accview.Show()
                End If
            End If
        End Sub

        Private Sub filter_char_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) _
            Handles filter_char.LinkClicked
            FilterCharacters.Show()
        End Sub

        Private Sub connect_bt_target_Click(sender As Object, e As EventArgs) Handles connect_bt_target.Click
            TargetSelectInterface.Show()
        End Sub

        Private Sub SelectedCharacterToolStripMenuItem_Click(sender As Object, e As EventArgs) _
            Handles SelectedCharacterToolStripMenuItem.Click
            Dim result =
                    MsgBox(
                        ResourceHandler.GetUserMessage("deletechar") & " (" &
                        characterview.SelectedItems(0).SubItems(2).Text & ")", vbYesNo,
                        ResourceHandler.GetUserMessage("areyousure"))
            If result = MsgBoxResult.Yes Then
                Dim charId As String = characterview.SelectedItems(0).SubItems(0).Text
                For I = 0 To characterview.SelectedItems.Count - 1
                    Dim charRemoveHandler As New CharacterRemoveHandler
                    charRemoveHandler.RemoveCharacterFromDb(CType(characterview.SelectedItems(0).Tag, NCFramework.Framework.Modules.Character), GlobalVariables.GlobalConnection, GlobalVariables.sourceStructure, GlobalVariables.sourceCore)
                    characterview.SelectedItems(I).Remove()
                    Dim toBeRemovedRow As DataRow() =
                            GlobalVariables.chartable.Select(
                                GlobalVariables.sourceStructure.char_guid_col(0) & " = '" & charId & "'")
                    If Not toBeRemovedRow.Length = 0 Then GlobalVariables.chartable.Rows.Remove(toBeRemovedRow(0))
                Next
                Setaccountview(GlobalVariables.acctable)
            End If
        End Sub

        Private Sub CheckedCharactersToolStripMenuItem_Click(sender As Object, e As EventArgs) _
            Handles CheckedCharactersToolStripMenuItem.Click
            Dim result = MsgBox(ResourceHandler.GetUserMessage("deletechar"), vbYesNo,
                                ResourceHandler.GetUserMessage("areyousure"))
            If result = MsgBoxResult.Yes Then
                For Each itm As ListViewItem In characterview.CheckedItems
                    Dim charRemoveHandler As New CharacterRemoveHandler
                    charRemoveHandler.RemoveCharacterFromDb(CType(itm.Tag, NCFramework.Framework.Modules.Character), GlobalVariables.GlobalConnection, GlobalVariables.sourceStructure, GlobalVariables.sourceCore)
                    characterview.Items.Remove(itm)
                    Dim toBeRemovedRow As DataRow() =
                            GlobalVariables.chartable.Select(
                                GlobalVariables.sourceStructure.char_guid_col(0) & " = '" & itm.SubItems(0).Text & "'")
                    If Not toBeRemovedRow.Length = 0 Then GlobalVariables.chartable.Rows.Remove(toBeRemovedRow(0))
                Next
                Setaccountview(GlobalVariables.acctable)
            End If
        End Sub

        Private Sub accountview_ItemDrag(sender As Object, e As ItemDragEventArgs) Handles accountview.ItemDrag
            accountview.DoDragDrop(accountview.SelectedItems, DragDropEffects.Move)
        End Sub

        Private Sub characterview_ItemDrag(sender As Object, e As ItemDragEventArgs) Handles characterview.ItemDrag
            characterview.DoDragDrop(characterview.SelectedItems, DragDropEffects.Move)
        End Sub

        Private Sub characterview_MouseUp(sender As Object, e As MouseEventArgs) Handles characterview.MouseUp
            If e.Button = MouseButtons.Right Then
                If characterview.SelectedItems.Count = 0 And characterview.CheckedItems.Count = 0 Then Exit Sub
                If characterview.SelectedItems.Count = 0 Then
                    SelectedCharacterToolStripMenuItem.Enabled = False
                    SelectedCharacterToolStripMenuItem1.Enabled = False
                    CheckedCharactersToolStripMenuItem.Enabled = True
                    CheckedCharactersToolStripMenuItem1.Enabled = True
                    EditToolStripMenuItem1.Enabled = False
                Else
                    EditToolStripMenuItem1.Enabled = True
                    SelectedCharacterToolStripMenuItem.Enabled = True
                    SelectedCharacterToolStripMenuItem1.Enabled = True
                    CheckedCharactersToolStripMenuItem.Enabled = False
                    CheckedCharactersToolStripMenuItem1.Enabled = False
                End If
                If characterview.SelectedItems.Count > 0 Then
                    SelectedCharacterToolStripMenuItem.Enabled = True
                    SelectedCharacterToolStripMenuItem1.Enabled = True
                End If
                If characterview.CheckedItems.Count > 0 Then
                    CheckedCharactersToolStripMenuItem.Enabled = True
                    CheckedCharactersToolStripMenuItem1.Enabled = True
                End If
                If _
                    GlobalVariables.saveTemplateMode = False And
                    Not GlobalVariables.TargetConnection.State = ConnectionState.Open Then
                    CheckedCharactersToolStripMenuItem1.Enabled = False
                    SelectedCharacterToolStripMenuItem1.Enabled = False
                End If
                charactercontext.Show(characterview, e.X, e.Y)
            End If
        End Sub

        Private Sub target_accounts_tree_DragDrop(sender As Object, e As DragEventArgs) _
            Handles target_accounts_tree.DragDrop
            If e.Data.GetDataPresent(GetType(ListView.SelectedListViewItemCollection).ToString(), False) Then
                Dim loc As Point = (CType(sender, TreeView)).PointToClient(New Point(e.X, e.Y))
                Dim destNode As TreeNode = (CType(sender, TreeView)).GetNodeAt(loc)
                Dim tnNew As TreeNode
                Dim lstViewColl As ListView.SelectedListViewItemCollection =
                        CType(e.Data.GetData(GetType(ListView.SelectedListViewItemCollection)), 
                              ListView.SelectedListViewItemCollection)
                For Each lvItem As ListViewItem In lstViewColl
                    Dim account = TryCast(lvItem.Tag, Account)
                    If (account IsNot Nothing) Then
                        For Each itm As ListViewItem In accountview.Items
                            itm.Checked = False
                            If TryCast(itm.Tag, Account).SetIndex = account.SetIndex Then
                                itm.Checked = True
                            End If
                        Next
                        Dim accLst As New List(Of Account)
                        accLst.Add(CType(lvItem.Tag, Account))
                        TransAccounts(accLst)
                        Exit Sub
                    End If
                    tnNew = New TreeNode(lvItem.Text)
                    tnNew.Tag = lvItem.Tag
                    If Not destNode Is Nothing Then
                        If TypeOf destNode.Tag Is NCFramework.Framework.Modules.Character Then
                            destNode = destNode.Parent
                        End If
                        destNode.Nodes.Insert(destNode.Index + 1, tnNew)
                        destNode.Expand()
                        Dim player As NCFramework.Framework.Modules.Character = CType(lvItem.Tag, 
                                                                                      NCFramework.Framework.Modules.
                                Character)
                        player.TargetAccount = CType(destNode.Tag, Account)
                        Dim nodes As New List(Of String)
                        For Each parentNode As TreeNode In target_accounts_tree.Nodes
                            nodes.AddRange(GetChildren(parentNode))
                        Next
                        With tnNew
                            .Text = player.Name
                            If Not nodes.Contains("'" & player.Name & "'") Then
                                .BackColor = Color.Green
                            Else
                                .BackColor = Color.Yellow
                            End If
                        End With
                        GlobalVariables.charactersToCreate.Add(player)
                        Transfer_bt.Enabled = True
                        info1_lbl.Visible = True
                        info2_lbl.Visible = True
                    End If
                Next lvItem
            End If
        End Sub

        Private Sub target_accounts_tree_DragEnter(sender As Object, e As DragEventArgs) _
            Handles target_accounts_tree.DragEnter
            If e.Data.GetDataPresent(GetType(ListView.SelectedListViewItemCollection)) Then
                e.Effect = DragDropEffects.Move
            End If
        End Sub

        Private Sub characterview_GiveFeedback(ByVal sender As Object, ByVal e As GiveFeedbackEventArgs) _
            Handles characterview.GiveFeedback
            e.UseDefaultCursors = False
            If (e.Effect And DragDropEffects.Move) = DragDropEffects.Move Then
                Cursor.Current = Cursors.Cross
            Else
                Cursor.Current = Cursors.Default
            End If
        End Sub

        Private Sub accountview_GiveFeedback(ByVal sender As Object, ByVal e As GiveFeedbackEventArgs) _
            Handles accountview.GiveFeedback
            e.UseDefaultCursors = False
            If (e.Effect And DragDropEffects.Move) = DragDropEffects.Move Then
                Cursor.Current = Cursors.Cross
            Else
                Cursor.Current = Cursors.Default
            End If
        End Sub

        Private Sub RemoveToolStripMenuItem2_Click(sender As Object, e As EventArgs) _
            Handles RemoveToolStripMenuItem2.Click
            If Not target_accounts_tree.SelectedNode.BackColor = Color.Transparent Then
                If Not GlobalVariables.accountsToCreate Is Nothing Then
                    Dim accResult As Account =
                            GlobalVariables.accountsToCreate.Find(
                                Function(account) _
                                                                     account.SetIndex =
                                                                     TryCast(target_accounts_tree.SelectedNode.Tag, 
                                                                             Account).SetIndex)
                    If Not accResult Is Nothing Then GlobalVariables.accountsToCreate.Remove(accResult)
                End If
                If Not GlobalVariables.charactersToCreate Is Nothing Then
                    Dim charResult As List(Of NCFramework.Framework.Modules.Character) =
                            GlobalVariables.charactersToCreate.FindAll(
                                Function(character) _
                                                                          character.TargetAccount.Id =
                                                                          TryCast(target_accounts_tree.SelectedNode.Tag, 
                                                                                  Account).Id)
                    If Not charResult Is Nothing Then
                        For Each character As NCFramework.Framework.Modules.Character In charResult
                            GlobalVariables.charactersToCreate.Remove(character)
                        Next
                    End If
                End If
                target_accounts_tree.SelectedNode.Remove()
                Exit Sub
            End If
        End Sub

        Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
            If Not target_accounts_tree.SelectedNode.BackColor = Color.Transparent Then
                If Not GlobalVariables.charactersToCreate Is Nothing Then
                    Dim charResult As NCFramework.Framework.Modules.Character =
                            GlobalVariables.charactersToCreate.Find(
                                Function(character) _
                                                                       character.SetIndex =
                                                                       TryCast(target_accounts_tree.SelectedNode.Tag, 
                                                                               NCFramework.Framework.Modules.Character).
                                                                           SetIndex)
                    If Not charResult Is Nothing Then GlobalVariables.charactersToCreate.Remove(charResult)
                End If
                target_accounts_tree.SelectedNode.Remove()
                Exit Sub
            End If
        End Sub

        Private Sub SelectedAccountToolStripMenuItem_Click(sender As Object, e As EventArgs) _
            Handles SelectedAccountToolStripMenuItem.Click
            Dim temparray As New List(Of Account)
            Dim acc As Account = CType(accountview.SelectedItems(0).Tag, Account)
            temparray.Add(acc)
            For Each checkeditem As ListViewItem In accountview.CheckedItems
                checkeditem.Checked = False
            Next
            For Each checkeditem As ListViewItem In characterview.CheckedItems
                checkeditem.Checked = False
            Next
            accountview.SelectedItems(0).Checked = True
            TransAccounts(temparray)
        End Sub

        Private Sub CheckedAccountsToolStripMenuItem_Click(sender As Object, e As EventArgs) _
            Handles CheckedAccountsToolStripMenuItem.Click
            Dim temparray As List(Of Account) =
                    (From checkedAccount As Object In accountview.CheckedItems Select acc = TryCast(checkedAccount, ListViewItem).Tag).
                    Cast(Of Account)().ToList()
            TransAccounts(temparray)
        End Sub

        Public Sub TransAccounts(ByVal accounts As List(Of Account))
            Dim needtocreate As Boolean
            For Each playerAccount As Account In accounts
                needtocreate = True
                For Each accountnode As TreeNode In target_accounts_tree.Nodes
                    If accountnode.Text.ToLower() = playerAccount.Name.ToLower() Then
                        needtocreate = False
                    End If
                Next
                If needtocreate = True Then
                    Dim newaccnode As New TreeNode
                    Dim newacc As Account = DeepCloneHelper.DeepClone(playerAccount)
                    newacc.Characters = New List(Of NCFramework.Framework.Modules.Character)()
                    With newaccnode
                        .Text = newacc.Name
                        .Tag = newacc
                        .BackColor = Color.Green
                    End With
                    target_accounts_tree.Nodes.Add(newaccnode)
                    GlobalVariables.accountsToCreate.Add(newacc)
                End If
                For Each checkedChar As ListViewItem In characterview.CheckedItems
                    Dim thischar As NCFramework.Framework.Modules.Character = CType(checkedChar.Tag, 
                                                                                    NCFramework.Framework.Modules.
                            Character)
                    Dim newchar As NCFramework.Framework.Modules.Character = DeepCloneHelper.DeepClone(thischar)
                    If thischar.AccountSet = playerAccount.SetIndex Then
                        For Each targetaccount As TreeNode In target_accounts_tree.Nodes
                            If TryCast(targetaccount.Tag, Account).Name = playerAccount.Name Then
                                Dim newcharnode As New TreeNode
                                Dim nodes As New List(Of String)
                                For Each parentNode As TreeNode In target_accounts_tree.Nodes
                                    nodes.AddRange(GetChildren(parentNode))
                                Next
                                newchar.TargetAccount = TryCast(targetaccount.Tag, Account)
                                With newcharnode
                                    .Text = newchar.Name
                                    .Tag = newchar
                                    If Not nodes.Contains("'" & newchar.Name & "'") Then
                                        .BackColor = Color.Green
                                    Else
                                        .BackColor = Color.Yellow
                                    End If
                                End With
                                targetaccount.Nodes.Add(newcharnode)
                                GlobalVariables.charactersToCreate.Add(newchar)
                            End If
                        Next
                    End If
                Next
            Next
            Transfer_bt.Enabled = True
            info1_lbl.Visible = True
            info2_lbl.Visible = True
        End Sub

        Public Sub transChars_specificacc(ByVal accounts As ArrayList)
            For Each character As NCFramework.Framework.Modules.Character In GlobalVariables.trans_charlist
                For Each accountnode As TreeNode In target_accounts_tree.Nodes
                    For Each account() As String In accounts
                        If account(0).ToLower() = accountnode.Text.ToLower() Then
                            Dim newcharnode As New TreeNode
                            Dim nodes As New List(Of String)
                            For Each parentNode As TreeNode In target_accounts_tree.Nodes
                                nodes.AddRange(GetChildren(parentNode))
                            Next
                            character.TargetAccount = CType(accountnode.Tag, Account)
                            With newcharnode
                                .Text = character.Name
                                .Tag = character
                                If Not nodes.Contains("'" & character.Name & "'") Then
                                    .BackColor = Color.Green
                                Else
                                    .BackColor = Color.Yellow
                                End If
                            End With
                            accountnode.Nodes.Add(newcharnode)
                            GlobalVariables.charactersToCreate.Add(character)
                        End If
                    Next
                Next
            Next
            Transfer_bt.Enabled = True
            info1_lbl.Visible = True
            info2_lbl.Visible = True
        End Sub

        Public Sub transChars_allacc()
            For Each character As NCFramework.Framework.Modules.Character In GlobalVariables.trans_charlist
                For Each accountnode As TreeNode In target_accounts_tree.Nodes
                    Dim newcharnode As New TreeNode
                    Dim nodes As New List(Of String)
                    For Each parentNode As TreeNode In target_accounts_tree.Nodes
                        nodes.AddRange(GetChildren(parentNode))
                    Next
                    character.TargetAccount = CType(accountnode.Tag, Account)
                    With newcharnode
                        .Text = character.Name
                        .Tag = character
                        If Not nodes.Contains("'" & character.Name & "'") Then
                            .BackColor = Color.Green
                        Else
                            .BackColor = Color.Yellow
                        End If
                    End With
                    accountnode.Nodes.Add(newcharnode)
                Next
            Next
            Transfer_bt.Enabled = True
            info1_lbl.Visible = True
            info2_lbl.Visible = True
        End Sub

        Function GetChildren(parentNode As TreeNode) As List(Of String)
            Return (From childNode As Object In parentNode.Nodes Select "'" & TryCast(childNode, TreeNode).Text & "'").ToList()
        End Function

        Private Sub Transfer_bt_Click(sender As Object, e As EventArgs) Handles Transfer_bt.Click
            If _transmissionBlocked = False Then
                _transmissionBlocked = True
                NewProcessStatus()
                Dim trd As New Thread(AddressOf StartTransfer)
                trd.Start()
            Else
                MsgBox(ResourceHandler.GetUserMessage("errTransmissionRunning"), MsgBoxStyle.Critical, "Error")
            End If
        End Sub

        Private Sub StartTransfer()
            Dim mtransfer As New TransmissionHandler
            mtransfer.HandleMigrationRequests(GlobalVariables.armoryMode)
            ThreadExtensions.ScSend(_context, New Action(Of CompletedEventArgs)(AddressOf OnTransmissionEnded),
                                    New CompletedEventArgs())
        End Sub

        Private Sub SelectedCharacterToolStripMenuItem1_Click(sender As Object, e As EventArgs) _
            Handles SelectedCharacterToolStripMenuItem1.Click
            GlobalVariables.trans_charlist = New List(Of NCFramework.Framework.Modules.Character)()
            For I = 0 To characterview.SelectedItems.Count - 1
                GlobalVariables.trans_charlist.Add(CType(characterview.SelectedItems(I).Tag, 
                                                         NCFramework.Framework.Modules.Character))
            Next
            PrepChartrans.Show()
        End Sub

        Private Sub CheckedCharactersToolStripMenuItem1_Click(sender As Object, e As EventArgs) _
            Handles CheckedCharactersToolStripMenuItem1.Click
            GlobalVariables.trans_charlist = New List(Of NCFramework.Framework.Modules.Character)()
            For Each itm As ListViewItem In characterview.CheckedItems
                GlobalVariables.trans_charlist.Add(CType(itm.Tag, NCFramework.Framework.Modules.Character))
            Next
            PrepChartrans.Show()
        End Sub

        Private Sub checkall_char_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) _
            Handles checkall_char.LinkClicked
            For Each xitem As ListViewItem In characterview.Items
                xitem.Checked = True
            Next
        End Sub

        Private Sub uncheckall_char_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) _
            Handles uncheckall_char.LinkClicked
            For Each xitem As ListViewItem In characterview.Items
                xitem.Checked = False
            Next
        End Sub


        Private Sub accountview_MouseUp(sender As Object, e As MouseEventArgs) Handles accountview.MouseUp
            If e.Button = MouseButtons.Right Then
                If accountview.SelectedItems.Count = 0 And accountview.CheckedItems.Count = 0 Then Exit Sub
                If accountview.SelectedItems.Count = 0 Then
                    SelectedAccountToolStripMenuItem.Enabled = False
                    SelectedAccountsToolStripMenuItem.Enabled = False
                    CheckedAccountsToolStripMenuItem.Enabled = True
                    CheckedAccountsToolStripMenuItem1.Enabled = True
                    EditToolStripMenuItem1.Enabled = False
                Else
                    EditToolStripMenuItem1.Enabled = True
                    SelectedAccountToolStripMenuItem.Enabled = True
                    SelectedAccountsToolStripMenuItem.Enabled = True
                    CheckedAccountsToolStripMenuItem.Enabled = False
                    CheckedAccountsToolStripMenuItem1.Enabled = False
                End If
                If accountview.SelectedItems.Count > 0 And accountview.CheckedItems.Count > 0 Then
                    CheckedAccountsToolStripMenuItem.Enabled = True
                    CheckedAccountsToolStripMenuItem1.Enabled = True
                    SelectedAccountToolStripMenuItem.Enabled = True
                    SelectedAccountsToolStripMenuItem.Enabled = True
                End If
                If _
                    GlobalVariables.saveTemplateMode = False And
                    Not GlobalVariables.TargetConnection.State = ConnectionState.Open Then
                    SelectedAccountToolStripMenuItem.Enabled = False
                    CheckedAccountsToolStripMenuItem.Enabled = False
                End If
                accountcontext.Show(accountview, e.X, e.Y)
            End If
        End Sub

        Private Sub target_accounts_tree_MouseUp(sender As Object, e As MouseEventArgs) _
            Handles target_accounts_tree.MouseUp
            If e.Button = MouseButtons.Right Then
                Dim oItem As TreeNode = target_accounts_tree.GetNodeAt(e.X, e.Y)
                If oItem IsNot Nothing Then
                    target_accounts_tree.SelectedNode = oItem
                    If oItem.Level = 0 Then
                        targetacccontext.Show(target_accounts_tree, e.X, e.Y)
                    Else
                        targetcharcontext.Show(target_accounts_tree, e.X, e.Y)
                    End If
                End If
            End If
        End Sub

        Private Sub EditToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles EditToolStripMenuItem1.Click
            '// Please remember that a character which is loaded from a database needs to be completely stored temporarily
            If _transmissionBlocked Then
                MsgBox(ResourceHandler.GetUserMessage("errWaitTillTransmissionCompleted"), MsgBoxStyle.Critical,
                       "Warning")
            Else
                NewProcessStatus()
                Dim charview As CharacterOverview = New CharacterOverview
                Dim player As NCFramework.Framework.Modules.Character = CType(characterview.SelectedItems(0).Tag, 
                                                                              NCFramework.Framework.Modules.Character)
                If GlobalVariables.armoryMode = True Then
                    Userwait.Show()
                    charview.prepare_interface(GetAccountSetBySetId(player.AccountSet), player.SetIndex)
                Else
                    'todo load info
                    Userwait.Show()
                    charview.prepare_interface(GetAccountSetBySetId(player.AccountSet), player.SetIndex)
                End If
            End If
        End Sub

        Private Sub RemoveToolStripMenuItem1_Click(sender As Object, e As EventArgs) _
            Handles RemoveToolStripMenuItem1.Click
        End Sub

        Private Sub characterview_MouseDoubleClick(sender As Object, e As MouseEventArgs) _
            Handles characterview.MouseDoubleClick
            EditToolStripMenuItem1_Click(sender, e)
        End Sub

        Private Sub back_bt_Click(sender As Object, e As EventArgs) Handles back_bt.Click
            If _transmissionBlocked Then
                MsgBox(ResourceHandler.GetUserMessage("errWaitTillTransmissionCompleted"), MsgBoxStyle.Critical,
                       "Warning")
                Exit Sub
            End If
            GlobalVariables.lastregion = "liveview"
            Close()
            Main.Show()
            'todo
        End Sub


        Private Sub me_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
            If Cursor = Cursors.SizeWE Then
                If e.Button = MouseButtons.Left Then
                    _ptMouseDownLocation = e.Location
                End If
            Else
                If e.Button = MouseButtons.Left Then
                    _ptMouseDownLocation = e.Location
                End If
            End If
        End Sub

        Private Sub me_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
            If Not _stretching And Not _moving Then
                If e.X > Width - 10 And e.X < Width + 10 Then
                    If Cursor = Cursors.Default Then
                        Cursor = Cursors.SizeWE
                    End If
                Else
                    If Cursor = Cursors.SizeWE Then
                        Cursor = Cursors.Default
                    End If
                End If
            End If
            If Cursor = Cursors.SizeWE Then
                If e.Button = MouseButtons.Left Then
                    _stretching = True
                    Size = New Size(e.Location.X, Size.Height)
                    Application.DoEvents()
                    mainpanel.Size = New Size(Size.Width - 10, mainpanel.Size.Height)
                    Dim tmpwidth As Integer = CType(((Size.Width / 1920) * 9), Integer)
                    header.Location = New Point(tmpwidth, header.Location.Y)
                    header.Size = New Size(Size.Width - (2 * tmpwidth), header.Size.Height)
                    closepanel.Location = New Point(header.Size.Width - 125, closepanel.Location.Y)
                    Application.DoEvents()

                End If
            Else
                If e.Button = MouseButtons.Left Then
                    _moving = True
                    Location = New Point(e.Location.X - _ptMouseDownLocation.X + Location.X,
                                         e.Location.Y - _ptMouseDownLocation.Y + Location.Y)
                End If
            End If
        End Sub

        Private Sub liveview_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
            _stretching = False
            _moving = False
        End Sub

        Private Sub header_MouseDown(sender As Object, e As MouseEventArgs) Handles header.MouseDown
            If e.Button = MouseButtons.Left Then
                _ptMouseDownLocation = e.Location
            End If
        End Sub

        Private Sub header_MouseMove(sender As Object, e As MouseEventArgs) Handles header.MouseMove
            If e.Button = MouseButtons.Left Then
                _moving = True
                Location = New Point(e.Location.X - _ptMouseDownLocation.X + Location.X,
                                     e.Location.Y - _ptMouseDownLocation.Y + Location.Y)
            End If
        End Sub

        Private Sub header_MouseUp(sender As Object, e As MouseEventArgs) Handles header.MouseUp
            _moving = False
        End Sub

        Private Sub createTemplate_bt_Click(sender As Object, e As EventArgs) Handles createTemplate_bt.Click
            If target_accounts_tree.Nodes.Count = 0 Then
                MsgBox(ResourceHandler.GetUserMessage("noaccountsselected"), MsgBoxStyle.Critical, "Error")
            Else
                Dim locOfd As New SaveFileDialog()
                Dim writepath As String = ""
                With locOfd
                    .Filter = "NamCore Studio Template File (*.ncsf)|*.ncsf"
                    .Title = "Save template file"
                    .DefaultExt = ".ncsf"
                    .FileName = "Characters.ncsf"
                    .DefaultExt = "ncsf"
                    .CheckPathExists = True
                    If (.ShowDialog() = DialogResult.OK) Then
                        writepath = .FileName()
                    End If
                End With
                If writepath = "" Then
                    MsgBox(ResourceHandler.GetUserMessage("invalidFileName"), MsgBoxStyle.Critical, "Error")
                    Exit Sub
                Else
                    _filePath = writepath
                End If
                NewProcessStatus()
                Userwait.Show()
                Dim saveChars As New GlobalCharVars
                saveChars.AccountSets = New List(Of Account)()
                GlobalVariables.forceTemplateCharVars = False
                GlobalVariables.templateCharVars = saveChars
                For Each accountnode As TreeNode In target_accounts_tree.Nodes
                    Dim player As Account = CType(accountnode.Tag, Account)
                    AddAccountSet(player.SetIndex, player, saveChars)
                    For Each charnode As TreeNode In accountnode.Nodes
                        Dim playerchar As NCFramework.Framework.Modules.Character = CType(charnode.Tag, 
                                                                                          NCFramework.Framework.Modules.
                                Character)
                        AddCharacterSet(playerchar.SetIndex, playerchar, player)
                    Next
                Next
                GlobalVariables.templateCharVars = saveChars
                GlobalVariables.forceTemplateCharVars = True
                If GlobalVariables.armoryMode = False And GlobalVariables.templateMode = False Then
                    Dim loadHandlerThread As New Thread(AddressOf LoadCharacter)
                    loadHandlerThread.Start()
                End If
            End If
        End Sub

        Private Sub LoadCharacter()
            For i = 0 To GlobalVariables.templateCharVars.AccountSets.Count - 1
                Dim account As Account = GlobalVariables.templateCharVars.AccountSets(i)
                For z = 0 To account.Characters.Count - 1
                    Dim player As NCFramework.Framework.Modules.Character = account.Characters(z)
                    If player.Loaded = False Then
                        '//Load charset
                        LogAppend("Loading character from database", "LiveView_LoadCharacter", True)
                        Dim mCoreHandler As New CoreHandler
                        mCoreHandler.HandleLoadingRequests(account, player.SetIndex)
                    End If
                Next z
            Next i
            ThreadExtensions.ScSend(_context, New Action(Of CompletedEventArgs)(AddressOf OnCoreCompleted),
                                    New CompletedEventArgs())
        End Sub

        Private Sub OnCharacterLoaded() Handles Me.OnCoreLoaded
            GlobalVariables.forceTemplateCharVars = False
            LogAppend("Characters loaded!", "LiveView_OnCharacterLoaded", True)

            Dim mSerializer As Serializer = New Serializer
            Dim ms As MemoryStream = mSerializer.Serialize(GlobalVariables.globChars)
            Dim _
                fs As _
                    New StreamWriter(_filePath,
                                     CType(FileMode.OpenOrCreate, Boolean))
            fs.BaseStream.Write(ms.ToArray, 0, ms.ToArray.Length)
            fs.Close()
            ms.Close()
            LogAppend("Template file created!", "LiveView_OnCharacterLoaded", True)
            CloseProcessStatus()
            Userwait.Close()
            MsgBox(ResourceHandler.GetUserMessage("templateFileCreated"), MsgBoxStyle.Information, "Info")
        End Sub

        Private Sub TransmissionCompleted() Handles Me.OnTransmissionCompleted
            Transfer_bt.Enabled = False
            _transmissionBlocked = False
        End Sub

        Private Sub highlighter4_Click(sender As Object, e As EventArgs) Handles highlighter4.Click
            back_bt.PerformClick()
        End Sub

        Private Sub highlighter5_Click(sender As Object, e As EventArgs) Handles highlighter5.Click
            WindowState = FormWindowState.Minimized
        End Sub

        Private Sub settings_pic_Click(sender As Object, e As EventArgs) Handles settings_pic.Click
            SettingsInterface.Show()
        End Sub

        Private Sub about_pic_Click(sender As Object, e As EventArgs) Handles about_pic.Click
            About.Show()
        End Sub

        Private Sub refreshdb_Click(sender As Object, e As EventArgs) Handles refreshdb.Click
            Loadaccountsandchars()
        End Sub

        Private Sub GroupBox2_MouseEnter(sender As Object, e As EventArgs) _
            Handles GroupBox2.MouseEnter, target_accounts_tree.MouseEnter, characterview.MouseEnter, Panel1.MouseEnter,
                    header.MouseEnter
            If Not _stretching And Not _moving Then
                Cursor = Cursors.Default
            End If
        End Sub
    End Class
End Namespace