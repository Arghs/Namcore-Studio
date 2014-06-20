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
'*      /Filename:      GlobalVariables
'*      /Description:   This file contains the main variables
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports System.Windows.Forms
Imports NCFramework.Framework.Forms
Imports MySql.Data.MySqlClient
Imports System.Threading
Imports System.Net

Namespace Framework.Modules
    Public Class GlobalVariables
        ' ReSharper disable InconsistentNaming
        Public Shared DebugMode As Boolean = False
        Public Shared LoadingTemplate As Boolean = False
        Public Shared DeserializationSuccessfull As Boolean = False
        Public Shared lastregion As String
        Public Shared TempCharacter As Character
        Public Shared globChars As GlobalCharVars
        Public Shared ModAccountSets As List(Of Account)
        Public Shared sourceCore As Core 'Modules.Core.ARCEMU, Modules.Core.TRINITY, Modules.Core.MANGOS
        Public Shared targetCore As Core 'Modules.Core.ARCEMU, Modules.Core.TRINITY, Modules.Core.MANGOS
        Public Shared sourceExpansion As Integer '1=classic, 2=tbc,...
        Public Shared targetExpansion As Integer '1=classic, 2=tbc,...
        Public Shared eventlog As String
        Public Shared eventlog_full As String
        Public Shared effectname_dt As DataTable
        Public Shared itemname_dt As DataTable
        Public Shared GlobalConnection As New MySqlConnection
        Public Shared GlobalConnection_Realm As New MySqlConnection
        Public Shared GlobalConnection_Info As New MySqlConnection
        Public Shared TargetConnection As New MySqlConnection
        Public Shared TargetConnection_Realm As New MySqlConnection
        Public Shared TargetConnection_Info As New MySqlConnection
        Public Shared GlobalConnectionString As String
        Public Shared GlobalConnectionString_Realm As String
        Public Shared TargetConnectionString As String
        Public Shared TargetConnectionString_Realm As String
        Public Shared TargetConnRealmDBname As String
        Public Shared TargetConnCharactersDBname As String
        Public Shared acctable As DataTable
        Public Shared chartable As DataTable
        Public Shared modifiedAccTable As DataTable
        Public Shared modifiedCharTable As DataTable
        'Public Shared modifiedCharInfo As List(Of String)
        Public Shared nonUsableGuidList As List(Of Integer)
        Public Shared armoryMode As Boolean = False
        Public Shared templateMode As Boolean = False
        Public Shared con_operator As Integer
        Public Shared trans_charlist As List(Of Character)
        Public Shared trans_acclist As List(Of Account)
        Public Shared sourceStructure As DbStructure
        Public Shared targetStructure As DbStructure
        Public Shared trd As Thread
        Public Shared GlobalWebClient As WebClient
        Public Shared currentViewedCharSetId As Integer
        Public Shared currentViewedCharSet As Character
        Public Shared currentEditedCharSet As Character
        Public Shared editedCharsIndex As List(Of Integer())
        Public Shared editedCharSets As List(Of Character)
        Public Shared trdrunnuing As Boolean
        Public Shared saveTemplateMode As Boolean = False
        Public Shared procStatus As ProcessStatus
        Public Shared tempItemInfo As List(Of Item)
        Public Shared tempItemInfoIndex As List(Of String())
        Public Shared tempGlyphInfo As List(Of Glyph)
        Public Shared tempGlyphInfoIndex As List(Of String())
        Public Shared accountsToCreate As List(Of Account)
        Public Shared charactersToCreate As List(Of Character)
        Public Shared tempAchievementInfo As List(Of ListViewItem)
        Public Shared tempAchievementInfoIndex As String
        Public Shared offlineExtension As Boolean
        Public Shared forceTargetConnectionUsage As Boolean
        Public Shared forceTemplateCharVars As Boolean
        Public Shared templateCharVars As GlobalCharVars
        Public Shared trdRunning As Integer = 0
        Public Shared abortMe As Boolean = False
        Public Shared proccessTXT As String
        Public Shared tempAvTable As DataTable
        Public Shared tempAvCatTable As DataTable
        Public Shared tempDisplayInfoTable As DataTable
        Public Shared tempQuestNameTable As DataTable
        Public Shared tempAvMainCatTable As DataTable
        Public Shared tempAvIconTable As DataTable
        Public Shared tempFactionTable As DataTable
        Public Shared tempItemSparseDE As DataTable
        ' ReSharper restore InconsistentNaming
    End Class

    <Serializable()>
    Public Class GlobalCharVars
        Public AccountSets As List(Of Account)
    End Class
End Namespace