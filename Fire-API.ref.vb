Imports System.IO
Imports System.Text

Public Class Fire_API_ref

    ' TODO : EXCEPTIONS CUSTOM + FINIR CUSTOM PARAMS

    ''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''                  TEMP FOLDERS                  ''
    ''''''''''''''''''''''''''''''''''''''''''''''''''''

    ' Variables
    Public ReadOnly TypeFolder As Boolean = False
    Public ReadOnly TypeFile As Boolean = True
    Public LastNumber As Long
    Public TempIDs As Dictionary(Of Integer, TempObject)

    ''' <summary>
    ''' The algorithm of encryption used.
    ''' </summary>
    Public Enum EncryptionAlgorithm
        SHA1
    End Enum

    ''' <summary>
    ''' EncryptionType may be :
    ''' <para>- FileContent (only the content of the file)</para>
    ''' <para>- FileName (only the name of the file)</para>
    ''' <para>- Both (FileName + FileContent)</para>
    ''' </summary>
    Public Enum EncryptionType
        FileContent
        FileName
        Both
    End Enum

    ''' <summary>
    ''' Create a new temp folder/file.
    ''' </summary>
    ''' <param name="TempObj">The TempObject with the temp file/folder properties.</param>
    ''' <param name="generate">If setted to true, the temp file/golder will be automatically generated.</param>
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
    ''' Remove all temp folders/files.
    ''' </summary>
    Public Sub RemoveAllTemp()
        ' Loop for remove all the temp folders/files.
        Dim i As Long = 1
        While (i < TempIDs.Count)
            RemoveTemp(i)
            i += 1
        End While
    End Sub

    ''' <summary>
    ''' Edit a temp file by giving the identifier and the content of the file.
    ''' </summary>
    Public Sub EditTempFile(id As Long, content As String, append As Boolean, encoding As Encoding)
        If (TempIDs.Item(id).TempType = TempObject.TempTypes.File) Then
            Try
                ' Write into the file
                My.Computer.FileSystem.WriteAllText(TempIDs.Item(id).TempDir + TempIDs.Item(id).TempName, content, append, encoding)
            Catch ex As Exception
                Throw ex
            End Try
        Else
            Throw New Exception
        End If
    End Sub

    ''' <summary>
    ''' Get the content of a temp file.
    ''' </summary>
    Public Function GetTempFileContent(id As Long, encoding As Encoding) As String
        If (TempIDs.Item(id).TempType = TempObject.TempTypes.File) Then
            Try
                Return File.ReadAllText(TempIDs.Item(id).TempDir + TempIDs.Item(id).TempName, encoding)
            Catch ex As Exception
                Throw ex
            End Try
        Else
            Throw New Exception
        End If
    End Function

    ''' <summary>
    ''' Rename a temp folder by giving the identifier.
    ''' </summary>
    Public Sub RenameTemp(id As Long, TempObj As TempObject, newName As String)
        Try
            Rename(TempObj.TempDir + TempObj.TempName, TempObj.TempDir + newName)
            TempObj.TempName = newName
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Encrypt a temp file.
    ''' </summary>
    Public Sub EncryptFile(id As Long, enc As EncryptionAlgorithm, enctype As EncryptionType, baseEncoding As Encoding, password As String)
        If TempIDs.ContainsKey(id) Then
            If TempIDs.Item(id).TempType = TempObject.TempTypes.Folder Then
                ' Declarating the wrapper.
                Dim wrapper As New Simple3Des(password)
                If enctype = EncryptionType.FileContent Then
                    Dim encryptedText As String = wrapper.EncryptData(GetTempFileContent(id, baseEncoding), enc)

                    ' We encrypt the file with the wrapper of the selected algorithm.
                    Try
                        ' We edit the file with the encrypted text of the wrapper of the selected algorithm.
                        EditTempFile(id, encryptedText, False, baseEncoding)
                    Catch ex As Exception
                        Throw ex
                    End Try
                ElseIf enctype = EncryptionType.FileName Then
                    Dim encryptedTextFN As String = wrapper.EncryptData(TempIDs.Item(id).TempName, enc)

                    ' We rename the file with the encrypted text of the wrapper of the selected algorithm.
                    RenameTemp(id, TempIDs.Item(id), encryptedTextFN)
                Else
                    Dim encryptedText As String = wrapper.EncryptData(GetTempFileContent(id, baseEncoding), enc)
                    Dim encryptedTextFN As String = wrapper.EncryptData(TempIDs.Item(id).TempName, enc)

                    ' We encrypt the file with the wrapper of the selected algorithm.
                    Try
                        ' We edit the file with the encrypted text of the wrapper of the selected algorithm.
                        EditTempFile(id, encryptedText, False, baseEncoding)
                    Catch ex As Exception
                        Throw ex
                    End Try

                    ' We rename the file with the encrypted text of the wrapper of the selected algorithm.
                    RenameTemp(id, TempIDs.Item(id), encryptedTextFN)
                End If
            Else
                Throw New Exception
            End If
        Else
            Throw New Exception
        End If
    End Sub

    ''' <summary>
    ''' Decrypt a temp file.
    ''' </summary>
    Public Sub DecryptFile(id As Long, enc As EncryptionAlgorithm, dectype As EncryptionType, baseEncoding As Encoding, password As String)
        If TempIDs.ContainsKey(id) Then
            If TempIDs.Item(id).TempType = TempObject.TempTypes.Folder Then
                ' Declarating the wrapper.
                Dim wrapper As New Simple3Des(password)
                If dectype = EncryptionType.FileContent Then
                    ' We decrypt the content with DecryptData method
                    Try
                        Dim plainText As String = wrapper.DecryptData(GetTempFileContent(id, baseEncoding), enc)
                        ' We edit the content of the file with the decrypted text of the wrapper of the selected algorithm.
                        EditTempFile(id, plainText, False, baseEncoding)
                    Catch ex As Exception
                        Throw ex
                    End Try
                ElseIf dectype = EncryptionType.FileName Then
                    ' We decrypt the name with DecryptData method
                    Try
                        Dim plainTextFN As String = wrapper.DecryptData(TempIDs.Item(id).TempName, enc)
                        ' We rename the file with the decrypted text of the wrapper of the selected algorithm.
                        RenameTemp(id, TempIDs.Item(id), plainTextFN)
                    Catch ex As Exception
                        Throw ex
                    End Try
                Else
                    ' We decrypt the content with DecryptData method
                    Try
                        Dim plainText As String = wrapper.DecryptData(GetTempFileContent(id, baseEncoding), enc)
                        EditTempFile(id, plainText, False, baseEncoding)
                    Catch ex As Exception
                        Throw ex
                    End Try

                    ' We decrypt the name with DecryptData method
                    Try
                        Dim plainTextFN As String = wrapper.DecryptData(TempIDs.Item(id).TempName, enc)
                        ' We rename the file with the decrypted text of the wrapper of the selected algorithm.
                        RenameTemp(id, TempIDs.Item(id), plainTextFN)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End If
            Else
                Throw New Exception
            End If
        Else
            Throw New Exception
        End If
    End Sub
End Class
