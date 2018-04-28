''' <summary>
''' Definitively best class ever.
''' </summary>
Public Class Fire_API_ref

    ''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''                  TEMP FOLDERS                  ''
    ''''''''''''''''''''''''''''''''''''''''''''''''''''

    ''' <summary>
    ''' The algorithm of encryption used.
    ''' </summary>
    Public Enum EncryptionAlgorithm
        MD5
        SHA1
        SHA256
        SHA384
        SHA512
    End Enum

    ''' <summary>
    ''' <see cref="EncryptionType"/> may be :
    ''' <para>- <see cref="FileContent"/> (only the content of the file)</para>
    ''' <para>- <see cref="FileName"/> (only the name of the file)</para>
    ''' <para>- <see cref="Both"/> (FileName + FileContent)</para>
    ''' </summary>
    Public Enum EncryptionType
        FileContent
        FileName
        Both
    End Enum

    ''' <summary>
    ''' <see cref="WriteMethod"/> may be :
    ''' <para>- <see cref="Overwrite"/> (will overwrite the file or value if already exists)</para>
    ''' <para>- <see cref="NoOverwrite"/> (will NOT overwrite the file or value and will throw an IOException if the file or value exists)</para>
    ''' </summary>
    Public Enum WriteMethod
        Overwrite
        NoOverwrite
    End Enum

    ''' <summary>
    ''' Generate/regenerate the temp folder/file by giving the <see cref="TempObject"/> and optionally the <see cref="WriteMethod"/>.
    ''' </summary>
    ''' <param name="TempObj">The TempObject to generate.</param>
    ''' <param name="method">The method to use if the file/folder already exists.</param>
    Public Sub GenerateTemp(TempObj As TempObject, Optional method As WriteMethod = WriteMethod.NoOverwrite)
        If TempObj.TempType = TempObject.TempTypes.File Then
            Try
                ' Try to create or overwrite the file.
                Dim fs As IO.FileStream = IO.File.Create(TempObj.TempDir + TempObj.TempName)
                ' Close the stream.
                fs.Close()
            Catch ioex As IO.IOException
                Try
                    If method = WriteMethod.Overwrite Then
                        ' Force delete
                        IO.File.Delete(TempObj.TempDir + TempObj.TempName)

                        ' Try to create or overwrite the file.
                        Dim fs As IO.FileStream = IO.File.Create(TempObj.TempDir + TempObj.TempName)
                        ' Close the stream.
                        fs.Close()
                    Else
                        Throw ioex
                    End If
                Catch ex As Exception
                    Throw ex
                End Try
            Catch ex As Exception
                Throw ex
            End Try
        Else
            Try
                ' Try to create the directory.
                IO.Directory.CreateDirectory(TempObj.TempDir + TempObj.TempName)
            Catch ioex As IO.IOException
                Try
                    If method = WriteMethod.Overwrite Then
                        ' Force delete
                        IO.File.Delete(TempObj.TempDir + TempObj.TempName)

                        ' Try to create the directory.
                        IO.Directory.CreateDirectory(TempObj.TempDir + TempObj.TempName)
                    Else
                        Throw ioex
                    End If
                Catch ex As Exception
                    Throw ex
                End Try
            Catch ex As Exception
                Throw ex
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Generate/regenerate the temp folder/file by giving the <see cref="List(Of TempObject)"/> and optionally the <see cref="WriteMethod"/>.
    ''' </summary>
    ''' <param name="TempList">The List(Of TempObject) to generate.</param>
    ''' <param name="method">The method to use if the file/folder already exists.</param>
    Public Sub GenerateAllTemp(TempList As Collections.Generic.List(Of Fire_API.ref.TempObject), Optional method As WriteMethod = WriteMethod.NoOverwrite)
        ' Loop for remove all the temp folders/files.
        Dim i As Long = 1
        While (i <= TempList.Count)
            GenerateTemp(TempList.Item(i - 1), method)
            i += 1
        End While
    End Sub

    ''' <summary>
    ''' Remove a temp folder/file by giving the <see cref="TempObject"/>.
    ''' </summary>
    ''' <param name="TempObj">The TempObject to remove.</param>
    Public Sub RemoveTemp(TempObj As TempObject)
        If TempObj.TempType = TempObject.TempTypes.File Then
            Try
                ' Try to delete the file.
                IO.File.Delete(TempObj.TempDir + TempObj.TempName)
            Catch ex As Exception
                Throw ex
            End Try
        Else
            Try
                ' Try to delete the directory.
                IO.Directory.Delete(TempObj.TempDir + TempObj.TempName)
            Catch ex As Exception
                Throw ex
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Remove multiple temp folders/files at once.
    ''' </summary>
    ''' <param name="TempList">The <see cref="List(Of TempObject)"/> with all <see cref="TempObject"/>s stored in to remove.</param>
    Public Sub RemoveTemps(TempList As Collections.Generic.List(Of TempObject))
        ' Loop for remove all the temp folders/files.
        Dim i As Long = 1
        While (i <= TempList.Count)
            RemoveTemp(TempList.Item(i - 1))
            i += 1
        End While
    End Sub

    ''' <summary>
    ''' Edit a temp file by giving the <see cref="TempObject"/>, the <see cref="String"/> who the content is stored in, the append <see cref="Boolean"/> and the <see cref="Text.Encoding"/> for the file.
    ''' </summary>
    ''' <param name="TempObj">The <see cref="TempObject"/> to edit.</param>
    ''' <param name="content">The content to add or store into file.</param>
    ''' <param name="append">True if you want to add lines, False if you want to replace all the content with <code>content</code> variable.</param>
    ''' <param name="encoding">The <see cref="Text.Encoding"/> to use to edit the file.</param>
    Public Sub EditTempFile(TempObj As Fire_API.ref.TempObject, content As String, append As Boolean, encoding As Text.Encoding)
        If TempObj.TempType = Fire_API.ref.TempObject.TempTypes.File Then
            Try
                ' Write into the file
                My.Computer.FileSystem.WriteAllText(TempObj.TempDir + TempObj.TempName, content, append, encoding)
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
    ''' <param name="TempObj">The <see cref="TempObject"/> to get file content.</param>
    ''' <param name="encoding">The <see cref="Text.Encoding"/> to use to get the file content.</param>
    Public Function GetTempFileContent(TempObj As Fire_API.ref.TempObject, encoding As Text.Encoding) As String
        If TempObj.TempType = TempObject.TempTypes.File Then
            Try
                Return IO.File.ReadAllText(TempObj.TempDir + TempObj.TempName, encoding)
            Catch ex As Exception
                Throw ex
            End Try
        Else
            Throw New Exception
        End If
    End Function

    ''' <summary>
    ''' Rename a temp file/folder.
    ''' </summary>
    ''' <param name="TempObj">The <see cref="TempObject"/> to rename.</param>
    ''' <param name="newName">The new name of the file/folder.</param>
    ''' <param name="method">Optionally, the <see cref="WriteMethod"/> to use for rename.</param>
    Public Sub RenameTemp(TempObj As TempObject, newName As String, Optional method As WriteMethod = WriteMethod.NoOverwrite)
        ' Replaces "/" for catching bugs with the encryption system
        newName = newName.Replace("/", ",")

        Try
            ' Try to rename
            Rename(TempObj.TempDir + TempObj.TempName, TempObj.TempDir + newName)
            TempObj.TempName = newName
        Catch ioex As IO.IOException
            If method = WriteMethod.Overwrite Then
                Try
                    If TempObj.TempType = TempObject.TempTypes.File Then
                        ' Force delete
                        IO.File.Delete(TempObj.TempDir + newName)
                    Else
                        ' Force delete
                        IO.Directory.Delete(TempObj.TempDir + newName)
                    End If

                    ' Then re-rename
                    Rename(TempObj.TempDir + TempObj.TempName, TempObj.TempDir + newName)
                    TempObj.TempName = newName
                Catch ex As Exception
                    Throw ex
                End Try
            Else
                Throw ioex
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Encrypt a temp file.
    ''' <para>
    ''' WARNING : The encryption works with TripleDES (see <see cref="Simple3Des"/> class) combined with the <see cref="EncryptionAlgorithm"/> that you choose.
    ''' When encrypt a file name with this method, the "/" characters of the hash will be automatically replaced with "," because Windows doesn't accept file names with "/".
    ''' </para>
    ''' </summary>
    ''' <param name="TempObj">The <see cref="TempObject"/> to encrypt (need to be a file).</param>
    ''' <param name="enc">The <see cref="EncryptionAlgorithm"/> to use.</param>
    ''' <param name="enctype">The <see cref="EncryptionType"/> to use.</param>
    ''' <param name="baseEncoding">The <see cref="Text.Encoding"/> of base to use.</param>
    ''' <param name="WriteMethod">The <see cref="WriteMethod"/> to use.</param>
    ''' <param name="password">The password to use (you need to remember it, or you can't decrypt your files).</param>
    Public Sub EncryptFile(TempObj As Fire_API.ref.TempObject, enc As EncryptionAlgorithm, enctype As EncryptionType, baseEncoding As Text.Encoding, password As String, Optional WriteMethod As WriteMethod = WriteMethod.NoOverwrite)
        If TempObj.TempType = TempObject.TempTypes.File Then
            If enc <= EncryptionAlgorithm.SHA512 And enc >= EncryptionAlgorithm.MD5 Then
                ' Declarating the wrapper.
                Dim wrapper As New Simple3Des(password, enc)

                If enctype = EncryptionType.FileContent Then
                    Dim encryptedText As String = wrapper.EncryptData(GetTempFileContent(TempObj, baseEncoding))

                    ' We encrypt the file with the wrapper of the selected algorithm.
                    Try
                        ' We edit the file with the encrypted text of the wrapper of the selected algorithm.
                        EditTempFile(TempObj, encryptedText, False, baseEncoding)
                    Catch ex As Exception
                        Throw ex
                    End Try
                ElseIf enctype = EncryptionType.FileName Then
                    Dim encryptedTextFN As String = wrapper.EncryptData(TempObj.TempName)

                    ' We rename the file with the encrypted text of the wrapper of the selected algorithm.
                    RenameTemp(TempObj, encryptedTextFN, WriteMethod)
                Else
                    Dim encryptedText As String = wrapper.EncryptData(GetTempFileContent(TempObj, baseEncoding))
                    Dim encryptedTextFN As String = wrapper.EncryptData(TempObj.TempName)

                    ' We encrypt the file with the wrapper of the selected algorithm.
                    Try
                        ' We edit the file with the encrypted text of the wrapper of the selected algorithm.
                        EditTempFile(TempObj, encryptedText, False, baseEncoding)
                    Catch ex As Exception
                        Throw ex
                    End Try

                    ' We rename the file with the encrypted text of the wrapper of the selected algorithm.
                    RenameTemp(TempObj, encryptedTextFN, WriteMethod)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Encrypt temp files.
    ''' <para>
    ''' WARNING : The encryption works with TripleDES (see <see cref="Simple3Des"/> class) combined with the <see cref="EncryptionAlgorithm"/> that you choose.
    ''' When encrypt a file name with this method, the "/" characters of the hash will be automatically replaced with "," because Windows doesn't accept file names with "/".
    ''' </para>
    ''' </summary>
    ''' <param name="TempList">The <see cref="List(Of TempObject)"/> to encrypt (only files will be encrypted).</param>
    ''' <param name="enc">The <see cref="EncryptionAlgorithm"/> to use.</param>
    ''' <param name="enctype">The <see cref="EncryptionType"/> to use.</param>
    ''' <param name="baseEncoding">The <see cref="Text.Encoding"/> of base to use.</param>
    ''' <param name="WriteMethod">The <see cref="WriteMethod"/> to use.</param>
    ''' <param name="password">The password to use (you need to remember it, or you can't decrypt your files).</param>
    Public Sub EncryptFiles(TempList As Collections.Generic.List(Of Fire_API.ref.TempObject), enc As EncryptionAlgorithm, enctype As EncryptionType, baseEncoding As Text.Encoding, password As String, Optional WriteMethod As WriteMethod = WriteMethod.NoOverwrite)
        ' Loop for encrypt all the temp files.
        Dim i As Long = 1
        While (i <= TempList.Count)
            EncryptFile(TempList.Item(i - 1), enc, enctype, baseEncoding, password, WriteMethod)
            i += 1
        End While
    End Sub

    ''' <summary>
    ''' Decrypt a temp file.
    ''' </summary>
    ''' <param name="TempObj">The <see cref="TempObject"/> to decrypt (need to be a file).</param>
    ''' <param name="enc">The <see cref="EncryptionAlgorithm"/> to use.</param>
    ''' <param name="dectype">The <see cref="EncryptionType"/> to use.</param>
    ''' <param name="baseEncoding">The <see cref="Text.Encoding"/> of base to use.</param>
    ''' <param name="WriteMethod">The <see cref="WriteMethod"/> to use.</param>
    ''' <param name="password">The password used to encrypt the file.</param>
    Public Sub DecryptFile(TempObj As Fire_API.ref.TempObject, enc As EncryptionAlgorithm, dectype As EncryptionType, baseEncoding As Text.Encoding, password As String, Optional WriteMethod As WriteMethod = WriteMethod.NoOverwrite)
        If TempObj.TempType = TempObject.TempTypes.File Then
            ' Declarating the wrapper.
            Dim wrapper As New Simple3Des(password, enc)
            If dectype = EncryptionType.FileContent Then
                ' We decrypt the content with DecryptData method
                Try
                    Dim plainText As String = wrapper.DecryptData(GetTempFileContent(TempObj, baseEncoding))

                    ' We edit the content of the file with the decrypted text of the wrapper of the selected algorithm.
                    EditTempFile(TempObj, plainText, False, baseEncoding)
                Catch ex As Exception
                    Throw ex
                End Try
            ElseIf dectype = EncryptionType.FileName Then
                ' We decrypt the name with DecryptData method
                Try
                    Dim plainTextFN As String = wrapper.DecryptData(TempObj.TempName.Replace(",", "/"))

                    ' We rename the file with the decrypted text of the wrapper of the selected algorithm.
                    RenameTemp(TempObj, plainTextFN, WriteMethod)
                Catch ex As Exception
                    Throw ex
                End Try
            Else
                ' We decrypt the content with DecryptData method
                Try
                    Dim plainText As String = wrapper.DecryptData(GetTempFileContent(TempObj, baseEncoding))
                    EditTempFile(TempObj, plainText, False, baseEncoding)
                Catch ex As Exception
                    Throw ex
                End Try

                ' We decrypt the name with DecryptData method
                Try
                    Dim plainTextFN As String = wrapper.DecryptData(TempObj.TempName.Replace(",", "/"))

                    ' We rename the file with the decrypted text of the wrapper of the selected algorithm.
                    RenameTemp(TempObj, plainTextFN, WriteMethod)
                Catch ex As Exception
                    Throw ex
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    ''' Decrypt temp files.
    ''' </summary>
    ''' <param name="TempList">The <see cref="List(Of TempObject)"/> to decrypt (only files will be decrypted).</param>
    ''' <param name="enc">The <see cref="EncryptionAlgorithm"/> to use.</param>
    ''' <param name="dectype">The <see cref="EncryptionType"/> to use.</param>
    ''' <param name="baseEncoding">The <see cref="Text.Encoding"/> of base to use.</param>
    ''' <param name="WriteMethod">The <see cref="WriteMethod"/> to use.</param>
    ''' <param name="password">The password used to encrypt the files.</param>
    Public Sub DecryptFiles(TempList As Collections.Generic.List(Of Fire_API.ref.TempObject), enc As EncryptionAlgorithm, dectype As EncryptionType, baseEncoding As Text.Encoding, password As String, Optional WriteMethod As WriteMethod = WriteMethod.NoOverwrite)
        ' Loop for encrypt all the temp files.
        Dim i As Long = 1
        While (i <= TempList.Count)
            DecryptFile(TempList.Item(i - 1), enc, dectype, baseEncoding, password, WriteMethod)
            i += 1
        End While
    End Sub
End Class
