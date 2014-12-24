'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013-2014 NamCore Studio <https://github.com/megasus/Namcore-Studio>
'* Copyright (C) 2010-2013 TOM_RUS' dbcviewer <https://github.com/tomrus88/dbcviewer>
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
Namespace Reader
    Public Class DbHeader
        Private _mSignature As String
        Private _mRecordCount As UInteger
        Private _mFieldCount As UInteger
        Private _mRecordSize As UInteger
        Private _mStringBlockSize As UInteger

        Public Property Signature() As String
            Get
                Return _mSignature
            End Get
            Set
                _mSignature = Value
            End Set
        End Property

        Public Property RecordCount() As UInteger
            Get
                Return _mRecordCount
            End Get
            Set
                _mRecordCount = Value
            End Set
        End Property

        Public Property FieldCount() As UInteger
            Get
                Return _mFieldCount
            End Get
            Set
                _mFieldCount = Value
            End Set
        End Property

        Public Property RecordSize() As UInteger
            Get
                Return _mRecordSize
            End Get
            Set
                _mRecordSize = Value
            End Set
        End Property

        Public Property StringBlockSize() As UInteger
            Get
                Return _mStringBlockSize
            End Get
            Set
                _mStringBlockSize = Value
            End Set
        End Property
    End Class
End Namespace