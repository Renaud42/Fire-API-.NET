Imports System.IO

Public Class Fire_API_ref

    ''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''                  TEMP FOLDERS                  ''
    ''''''''''''''''''''''''''''''''''''''''''''''''''''

    ' Variables
    Public ReadOnly TypeFolder As Boolean = False
    Public ReadOnly TypeFile As Boolean = True
    Public LastNumber As Long
    Public TempIDs As Dictionary(Of Integer, TempObject)

    Public Enum EncryptionAlgorithm
        MD5
        MD4
        SHA256
        SHA512
        RC4
        JSON
    End Enum

    ''' <summary>
    ''' Create a new temp folder/file.
    ''' </summary>
    Public Sub AddTemp(TempObj As TempObject, generate As Boolean)
        LastNumber += 1

        ' Saving new folder/file properties as new TempObject.
        TempIDs.Add(LastNumber, TempObj)

        ' If the option generate was enabled the file will generate automatically.
        If generate = True Then
            GenerateTemp(LastNumber)
        End If
    End Sub

    ''' <summary>
    ''' Generate/regenerate the temp folder/file by.
    ''' </summary>
    Public Sub GenerateTemp(id As Long)
        If (TempIDs.Item(id).TempType = TempObject.TempTypes.File) Then
            Try
                ' Try to create or overwrite the file.
                Dim fs As FileStream = File.Create(TempIDs.Item(id).TempDir + TempIDs.Item(id).TempName)
                ' Close the stream.
                fs.Close()
            Catch ex As Exception
                Throw ex
            End Try
        Else
            Try
                ' Try to create the directory.
                Dim di As DirectoryInfo = Directory.CreateDirectory(TempIDs.Item(id).TempDir)
            Catch ex As Exception
                Throw ex
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Remove a temp folder/file by giving the identifier.
    ''' </summary>
    Public Sub RemoveTemp(id As Long)
        If (TempIDs.Item(id).TempType = TempObject.TempTypes.File) Then
            Try
                ' Try to delete the file.
                File.Delete(TempIDs.Item(id).TempDir + TempIDs.Item(id).TempName)
            Catch ex As Exception
                Throw ex
            End Try
        Else
            Try
                ' Try to delete the directory.
                Directory.Delete(TempIDs.Item(id).TempDir)
            Catch ex As Exception
                Throw ex
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Edit a temp folder/file by giving the identifier.
    ''' </summary>
    Public Sub EditTempFile(id As Long, content As String)
        If (TempIDs.Item(id).TempType = TempObject.TempTypes.File) Then
            Try
                ' Create a file to write to.
                Using sw As StreamWriter = File.CreateText(TempIDs.Item(id).TempDir + TempIDs.Item(id).TempName)
                    sw.Write(content)
                    sw.Flush()
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        Else
            Throw New Exception
        End If
    End Sub

    Public Sub RenameTempFolder(id As Long)

    End Sub

    Public Sub GetTempFileContent(id As String)

    End Sub

    ''' <summary>
    ''' Encrypt a temp file.
    ''' </summary>
    Public Sub EncryptFile(algo As EncryptionAlgorithm, id As Long)
        If TempIDs.ContainsKey(id) Then
            If TempIDs.Item(id).TempType = TempObject.TempTypes.File Then
                ' We encrypt the file with the selected algorithm.
                Select Case algo
                    Case EncryptionAlgorithm.MD5

                End Select
            Else
                Throw New Exception
            End If
        Else
            Throw New Exception
        End If
    End Sub
End Class
