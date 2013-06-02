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
'*      /Filename:      GlobalVariables
'*      /Description:   This file contains the main variables
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Imports MySql.Data.MySqlClient
Imports System.Threading

Public Class GlobalVariables
    Public Shared temporaryCharacterInformation As List(Of String)
    Public Shared sourceCore As String '"arcemu", "trinity", "mangos"
    Public Shared targetCore As String '"arcemu", "trinity", "mangos"
    Public Shared expansion As Integer '1=classic, 2=tbc,...
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
    Public Shared characterGUID As Integer
    Public Shared acctable As DataTable
    Public Shared chartable As DataTable
    Public Shared modifiedAccTable As DataTable
    Public Shared modifiedCharTable As DataTable
    Public Shared modifiedCharInfo As List(Of String)
    Public Shared armoryMode As Boolean
    Public Shared con_operator As Integer
    Public Shared trans_charlist As ArrayList
    Public Shared trans_acclist As ArrayList
    Public Shared sourceStructure As DBStructure
    Public Shared targetStructure As DBStructure
    Public Shared trd As Thread
    Public Shared trdrunnuing As Boolean
    Public Shared procStatus As Process_status
    Public Shared tempItemInfo As List(Of Item)
    Public Shared tempItemInfoIndex As List(Of String())
    Public Shared accountInfo As List(Of Account)
    Public Shared createAccountsIndex As List(Of Integer)
    Public Shared charactersToCreate As List(Of String)
End Class
