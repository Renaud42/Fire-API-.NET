Imports Fire_API.ref.Encryption

''' <summary>
''' <seealso cref="ExtraFile"/> class defines extra-functions for files/directorys
''' </summary>
Public Class ExtraFile
    ''' <summary>
    ''' Encrypt a file
    ''' <para>
    ''' WARNING : The encryption works with TripleDES (see <see cref="TripleDES"/> class) combined with the <see cref="Encryption.HashingAlgorithm"/> that you choose.
    ''' When encrypt a file name with this method, the "/" characters of the hash will be automatically replaced with "," because Windows doesn't accept file names with "/".
    ''' </para>
    ''' </summary>
    ''' <param name="filePath">Path to the file</param>
    ''' <param name="hashingalgorithm">Hashing algorithm used to encrypt file</param>
    ''' <param name="enctype">Encryption type used (file content, file name, both)</param>
    ''' <param name="encoding">File encoding (default, use <seealso cref="Text.Encoding.Default"/>)</param>
    ''' <param name="password">TripleDES encryption password</param>
    Public Shared Sub EncryptFileWithTripleDES(filePath As String, hashingAlgorithm As HashingAlgorithm, encType As EncryptionType, encoding As Text.Encoding, password As String)
        If hashingAlgorithm <= HashingAlgorithm.SHA512 And hashingAlgorithm >= HashingAlgorithm.MD5 Then
            ' Initializing the wrapper
            Dim wrapper As New TripleDES(password, hashingAlgorithm)

            If IO.File.Exists(filePath) Then
                If encType = EncryptionType.FILE_CONTENT Then
                    Dim encryptedText As String = wrapper.EncryptData(IO.File.ReadAllText(filePath, encoding))

                    ' We edit the file with the encrypted text of the wrapper of the selected algorithm
                    My.Computer.FileSystem.WriteAllText(filePath, encryptedText, False, encoding)
                ElseIf encType = EncryptionType.FILE_NAME Then
                    Dim encryptedFileName As String = wrapper.EncryptData(filePath.Split("\\").Last).Replace("/", ",")

                    ' We rename the file with the encrypted text of the wrapper of the selected algorithm
                    My.Computer.FileSystem.RenameFile(filePath, encryptedFileName)
                Else
                    ' Encrypt file content
                    EncryptFileWithTripleDES(filePath, hashingAlgorithm, EncryptionType.FILE_CONTENT, encoding, password)
                    ' Then encrypt file name
                    EncryptFileWithTripleDES(filePath, hashingAlgorithm, EncryptionType.FILE_NAME, encoding, password)
                End If
            Else
                Throw New IO.FileNotFoundException
            End If
        End If
    End Sub

    ''' <summary>
    ''' Decrypt a file
    ''' <para>
    ''' WARNING : The encryption works with TripleDES (see <see cref="TripleDES"/> class) combined with the <see cref="Encryption.HashingAlgorithm"/> that you choose.
    ''' When decrypt a file name with this method, the "," characters of the hash will be automatically restored with "/" because Windows doesn't accept file names with "/".
    ''' </para>
    ''' </summary>
    ''' <param name="filePath">Path to the file</param>
    ''' <param name="hashingalgorithm">Hashing algorithm used to encrypt file</param>
    ''' <param name="enctype">Encryption type used (file content, file name, both)</param>
    ''' <param name="encoding">File encoding (default, use <seealso cref="Text.Encoding.Default"/>)</param>
    ''' <param name="password">TripleDES encryption password</param>
    Public Shared Sub DecryptFileWithTripleDES(filePath As String, hashingAlgorithm As HashingAlgorithm, encType As EncryptionType, encoding As Text.Encoding, password As String)
        If hashingAlgorithm <= HashingAlgorithm.SHA512 And hashingAlgorithm >= HashingAlgorithm.MD5 Then
            ' Initializing the wrapper
            Dim wrapper As New TripleDES(password, hashingAlgorithm)

            If IO.File.Exists(filePath) Then
                If encType = EncryptionType.FILE_CONTENT Then
                    Dim decryptedText As String = wrapper.DecryptData(IO.File.ReadAllText(filePath, encoding))

                    ' We edit the file with the decrypted text of the wrapper of the selected algorithm
                    My.Computer.FileSystem.WriteAllText(filePath, decryptedText, False, encoding)
                ElseIf encType = EncryptionType.FILE_NAME Then
                    Dim decryptedFileName As String = wrapper.DecryptData(filePath.Split("\\").Last.Replace(",", "/"))

                    ' We rename the file with the decrypted text of the wrapper of the selected algorithm
                    My.Computer.FileSystem.RenameFile(filePath, decryptedFileName)
                Else
                    ' Encrypt file content
                    DecryptFileWithTripleDES(filePath, hashingAlgorithm, EncryptionType.FILE_CONTENT, encoding, password)
                    ' Then encrypt file name
                    DecryptFileWithTripleDES(filePath, hashingAlgorithm, EncryptionType.FILE_NAME, encoding, password)
                End If
            Else
                Throw New IO.FileNotFoundException
            End If
        End If
    End Sub
End Class
