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
'*      /Filename:      ClsHash
'*      /Description:   Hash generation extension
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Imports System.IO
Imports System.Security.Cryptography
Imports System.Text

Namespace Framework.Functions

    Public Class ClsHash
        Public Enum Hash As Integer
            MD5 = 0
            SHA1 = 1
            SHA256 = 2
            SHA384 = 3
            SHA512 = 4
        End Enum

        Public Shared Function HashString(ByVal value As String, ByVal hash As Hash) As String
            Dim data(0) As Byte
            Dim hashValue(0) As Byte
            Dim result As String = ""
            Dim tmp As String

            Select Case Hash
                Case Hash.MD5
                    Dim md5 As New MD5CryptoServiceProvider
                    data = Encoding.ASCII.GetBytes(value)
                    hashValue = md5.ComputeHash(data)

                Case Hash.SHA1
                    Dim sha1 As New SHA1Managed
                    data = Encoding.ASCII.GetBytes(value)
                    hashValue = sha1.ComputeHash(data)

                Case Hash.SHA256
                    Dim sha256 As New SHA256Managed
                    data = Encoding.ASCII.GetBytes(value)
                    hashValue = sha256.ComputeHash(data)

                Case Hash.SHA384
                    Dim sha384 As New SHA384Managed
                    data = Encoding.ASCII.GetBytes(value)
                    hashValue = sha384.ComputeHash(data)

                Case Hash.SHA512
                    Dim sha512 As New SHA512Managed
                    data = Encoding.ASCII.GetBytes(value)
                    hashValue = sha512.ComputeHash(data)
            End Select

            For i As Integer = 0 To hashValue.Length - 1
                tmp = Hex(hashValue(i))
                If Len(tmp) = 1 Then tmp = "0" & tmp
                result += tmp
            Next
            Return result
        End Function

        Public Shared Function HashFile(ByVal file As String, ByVal hash As Hash) As String
            Dim fn As New FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read, 8192)
            Dim hashValue(0) As Byte
            Dim result As String = ""
            Dim tmp As String

            Select Case Hash
                Case Hash.MD5
                    Dim md5 As New MD5CryptoServiceProvider
                    md5.ComputeHash(fn)
                    hashValue = md5.Hash

                Case Hash.SHA1
                    Dim sha1 As New SHA1Managed
                    sha1.ComputeHash(fn)
                    hashValue = sha1.Hash

                Case Hash.SHA256
                    Dim sha256 As New SHA256Managed
                    sha256.ComputeHash(fn)
                    hashValue = sha256.Hash

                Case Hash.SHA384
                    Dim sha384 As New SHA384Managed
                    sha384.ComputeHash(fn)
                    hashValue = sha384.Hash

                Case Hash.SHA512
                    Dim sha512 As New SHA512Managed
                    sha512.ComputeHash(fn)
                    hashValue = sha512.Hash
            End Select
            fn.Close()

            For i As Integer = 0 To hashValue.Length - 1
                tmp = Hex(hashValue(i))
                If Len(tmp) = 1 Then tmp = "0" & tmp
                result += tmp
            Next
            Return result
        End Function
    End Class
End Namespace