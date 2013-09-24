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
'*      /Filename:      DbHeader
'*      /Description:   DBC header information
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Namespace Reader
    Public Class DbHeader
        Public Property Signature() As String
            Get
                Return _mSignature
            End Get
            Set(value As String)
                _mSignature = value
            End Set
        End Property
        Private _mSignature As String
        Public Property RecordCount() As UInteger
            Get
                Return _mRecordCount
            End Get
            Set(value As UInteger)
                _mRecordCount = value
            End Set
        End Property
        Private _mRecordCount As UInteger
        Public Property FieldCount() As UInteger
            Get
                Return _mFieldCount
            End Get
            Set(value As UInteger)
                _mFieldCount = value
            End Set
        End Property
        Private _mFieldCount As UInteger
        Public Property RecordSize() As UInteger
            Get
                Return _mRecordSize
            End Get
            Set(value As UInteger)
                _mRecordSize = value
            End Set
        End Property
        Private _mRecordSize As UInteger
        Public Property StringBlockSize() As UInteger
            Get
                Return _mStringBlockSize
            End Get
            Set(value As UInteger)
                _mStringBlockSize = value
            End Set
        End Property
        Private _mStringBlockSize As UInteger

        Public ReadOnly Property IsValidDbcFile() As Boolean
            Get
                Return Signature = "WDBC"
            End Get
        End Property
        Public ReadOnly Property IsValidDb2File() As Boolean
            Get
                Return Signature = "WDB2"
            End Get
        End Property
    End Class
End Namespace