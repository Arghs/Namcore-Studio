Imports System.IO
Imports SevenZip.Compression.LZMA
Module Compression
    Public Sub Decompress(fInfo As FileInfo, fName As String)
        If My.Computer.FileSystem.FileExists(fInfo.Directory.FullName & "\" & fName) Then
            My.Computer.FileSystem.DeleteFile(fInfo.Directory.FullName & "\" & fName)
        End If
        Dim bData As Byte()
        Dim br As BinaryReader = New BinaryReader(System.IO.File.OpenRead(fInfo.FullName))
        bData = br.ReadBytes(CInt(br.BaseStream.Length))
        Dim ms As MemoryStream = New MemoryStream(bData, 0, bData.Length)
        ms.Write(bData, 0, bData.Length)
        Dim compressB As Byte() = SevenZipHelper.Decompress(ms.ToArray())
        Dim rw As New FileStream(fInfo.Directory.FullName & "\" & fName, FileMode.CreateNew)
        rw.Write(compressB, 0, compressB.Length)
        rw.Close()
    End Sub
End Module
