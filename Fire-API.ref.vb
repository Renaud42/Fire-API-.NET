Public Class Fire_API_ref

    ' TODO : EXCEPTIONS CUSTOM + FINIR CUSTOM PARAMS

    ''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''                  TEMP FOLDERS                  ''
    ''''''''''''''''''''''''''''''''''''''''''''''''''''

    ''' <summary>
    ''' The algorithm of encryption used.
    ''' </summary>
    Public Enum EncryptionAlgorithm
        SHA1
        SHA256
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
    ''' Generate/regenerate the temp folder/file by.
    ''' </summary>
    Public Sub GenerateTemp(TempObj As TempObject)
        If TempObj.TempType = TempObject.TempTypes.File Then
            Try
                ' Try to create or overwrite the file.
                Dim fs As System.IO.FileStream = System.IO.File.Create(TempObj.TempDir + TempObj.TempName)
                ' Close the stream.
                fs.Close()
            Catch ex As System.Exception
                Throw ex
            End Try
        Else
            Try
                ' Try to create the directory.
                System.IO.Directory.CreateDirectory(TempObj.TempDir + TempObj.TempName)
            Catch ex As System.Exception
                Throw ex
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Remove a temp folder/file by giving the identifier.
    ''' </summary>
    Public Sub RemoveTemp(TempObj As TempObject)
        If TempObj.TempType = TempObject.TempTypes.File Then
            Try
                ' Try to delete the file.
                System.IO.File.Delete(TempObj.TempDir + TempObj.TempName)
            Catch ex As System.Exception
                Throw ex
            End Try
        Else
            Try
                ' Try to delete the directory.
                System.IO.Directory.Delete(TempObj.TempDir + TempObj.TempName)
            Catch ex As System.Exception
                Throw ex
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Remove all temp folders/files.
    ''' </summary>
    Public Sub RemoveAllTemp(TempDictionary As System.Collections.Generic.List(Of Fire_API.ref.TempObject))
        ' Loop for remove all the temp folders/files.
        Dim i As Long = 1
        While (i < TempDictionary.Count)
            RemoveTemp(TempDictionary.Item(i - 1))
            i += 1
        End While
    End Sub

    ''' <summary>
    ''' Edit a temp file by giving the identifier and the content of the file.
    ''' </summary>
    Public Sub EditTempFile(TempObj As Fire_API.ref.TempObject, content As String, append As Boolean, encoding As System.Text.Encoding)
        If TempObj.TempType = Fire_API.ref.TempObject.TempTypes.File Then
            Try
                ' Write into the file
                My.Computer.FileSystem.WriteAllText(TempObj.TempDir + TempObj.TempName, content, append, encoding)
            Catch ex As System.Exception
                Throw ex
            End Try
        Else
            Throw New System.Exception
        End If
    End Sub

    ''' <summary>
    ''' Get the content of a temp file.
    ''' </summary>
    Public Function GetTempFileContent(TempObj As Fire_API.ref.TempObject, encoding As System.Text.Encoding) As String
        If TempObj.TempType = TempObject.TempTypes.File Then
            Try
                Return System.IO.File.ReadAllText(TempObj.TempDir + TempObj.TempName, encoding)
            Catch ex As System.Exception
                Throw ex
            End Try
        Else
            Throw New System.Exception
        End If
    End Function

    ''' <summary>
    ''' Rename a temp folder by giving the identifier.
    ''' </summary>
    Public Sub RenameTemp(TempObj As TempObject, newName As String)
        Try
            Rename(TempObj.TempDir + TempObj.TempName, TempObj.TempDir + newName)
            TempObj.TempName = newName
        Catch ex As System.Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Encrypt a temp file.
    ''' </summary>
    Public Sub EncryptFile(TempObj As Fire_API.ref.TempObject, enc As EncryptionAlgorithm, enctype As EncryptionType, baseEncoding As System.Text.Encoding, password As String)
        If TempObj.TempType = TempObject.TempTypes.File Then
            ' Declarating the wrapper.
            Dim wrapper As New Simple3Des(password)
            If enctype = EncryptionType.FileContent Then
                Dim encryptedText As String = wrapper.EncryptData(GetTempFileContent(TempObj, baseEncoding), enc)

                ' We encrypt the file with the wrapper of the selected algorithm.
                Try
                    ' We edit the file with the encrypted text of the wrapper of the selected algorithm.
                    EditTempFile(TempObj, encryptedText, False, baseEncoding)
                Catch ex As System.Exception
                    Throw ex
                End Try
            ElseIf enctype = EncryptionType.FileName Then
                Dim encryptedTextFN As String = wrapper.EncryptData(TempObj.TempName, enc)

                ' We rename the file with the encrypted text of the wrapper of the selected algorithm.
                RenameTemp(TempObj, encryptedTextFN)
            Else
                Dim encryptedText As String = wrapper.EncryptData(GetTempFileContent(TempObj, baseEncoding), enc)
                Dim encryptedTextFN As String = wrapper.EncryptData(TempObj.TempName, enc)

                ' We encrypt the file with the wrapper of the selected algorithm.
                Try
                    ' We edit the file with the encrypted text of the wrapper of the selected algorithm.
                    EditTempFile(TempObj, encryptedText, False, baseEncoding)
                Catch ex As System.Exception
                    Throw ex
                End Try

                ' We rename the file with the encrypted text of the wrapper of the selected algorithm.
                RenameTemp(TempObj, encryptedTextFN)
            End If
        Else
            Throw New System.Exception
        End If
    End Sub

    ''' <summary>
    ''' Decrypt a temp file.
    ''' </summary>
    Public Sub DecryptFile(TempObj As Fire_API.ref.TempObject, enc As EncryptionAlgorithm, dectype As EncryptionType, baseEncoding As System.Text.Encoding, password As String)
        If TempObj.TempType = TempObject.TempTypes.File Then
            ' Declarating the wrapper.
            Dim wrapper As New Simple3Des(password)
            If dectype = EncryptionType.FileContent Then
                ' We decrypt the content with DecryptData method
                Try
                    Dim plainText As String = wrapper.DecryptData(GetTempFileContent(TempObj, baseEncoding), enc)
                    ' We edit the content of the file with the decrypted text of the wrapper of the selected algorithm.
                    EditTempFile(TempObj, plainText, False, baseEncoding)
                Catch ex As System.Exception
                    Throw ex
                End Try
            ElseIf dectype = EncryptionType.FileName Then
                ' We decrypt the name with DecryptData method
                Try
                    Dim plainTextFN As String = wrapper.DecryptData(TempObj.TempName, enc)
                    ' We rename the file with the decrypted text of the wrapper of the selected algorithm.
                    RenameTemp(TempObj, plainTextFN)
                Catch ex As System.Exception
                    Throw ex
                End Try
            Else
                ' We decrypt the content with DecryptData method
                Try
                    Dim plainText As String = wrapper.DecryptData(GetTempFileContent(TempObj, baseEncoding), enc)
                    EditTempFile(TempObj, plainText, False, baseEncoding)
                Catch ex As System.Exception
                    Throw ex
                End Try

                ' We decrypt the name with DecryptData method
                Try
                    Dim plainTextFN As String = wrapper.DecryptData(TempObj.TempName, enc)
                    ' We rename the file with the decrypted text of the wrapper of the selected algorithm.
                    RenameTemp(TempObj, plainTextFN)
                Catch ex As System.Exception
                    Throw ex
                End Try
            End If
        Else
            Throw New System.Exception
        End If
    End Sub
End Class
