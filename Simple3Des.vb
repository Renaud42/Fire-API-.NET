Public NotInheritable Class Simple3Des

    ''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''             CUSTOM SIMPLE3DES CLASS            ''
    ''''''''''''''''''''''''''''''''''''''''''''''''''''

    Private TripleDes As New System.Security.Cryptography.TripleDESCryptoServiceProvider

    ''' <summary>
    ''' Function to truncate hash with the key/password, the length and the selected encryption algorithm.
    ''' </summary>
    ''' <param name="key">Key/password to encrypt the text.</param>
    ''' <param name="length">Chain length.</param>
    ''' <param name="enc">Encryption algorithm to encrypt the text.</param>
    Private Function TruncateHash(ByVal key As String, ByVal length As Integer, ByVal enc As Fire_API_ref.EncryptionAlgorithm) As Byte()
        ' Creating variables for all encryption types.
        Dim md5 As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim sha1 As New System.Security.Cryptography.SHA1CryptoServiceProvider
        Dim sha256 As New System.Security.Cryptography.SHA256CryptoServiceProvider
        Dim sha384 As New System.Security.Cryptography.SHA384CryptoServiceProvider
        Dim sha512 As New System.Security.Cryptography.SHA512CryptoServiceProvider

        ' Hash the key.
        Dim keyBytes() As Byte = System.Text.Encoding.Unicode.GetBytes(key)
        Dim hash() As Byte

        Select Case enc
            Case Fire_API_ref.EncryptionAlgorithm.MD5
                hash = md5.ComputeHash(keyBytes)
            Case Fire_API_ref.EncryptionAlgorithm.SHA1
                hash = sha1.ComputeHash(keyBytes)
            Case Fire_API_ref.EncryptionAlgorithm.SHA256
                hash = sha256.ComputeHash(keyBytes)
            Case Fire_API_ref.EncryptionAlgorithm.SHA384
                hash = sha384.ComputeHash(keyBytes)
            Case Fire_API_ref.EncryptionAlgorithm.SHA512
                hash = sha512.ComputeHash(keyBytes)
        End Select

        ' Truncate or pad the hash.
        ReDim Preserve hash(length - 1)
        Return hash
    End Function

    ' What occurs when we create a new Simple3Des wrapper.
    Sub New(ByVal key As String, ByVal enc As Fire_API_ref.EncryptionAlgorithm)
        ' Initialize the crypto provider.
        TripleDes.Key = TruncateHash(key, TripleDes.KeySize \ 8, enc)
        TripleDes.IV = TruncateHash("", TripleDes.BlockSize \ 8, enc)
    End Sub

    Public Function EncryptData(ByVal plaintext As String) As String
        ' Convert the plaintext string to a byte array.
        Dim plaintextBytes() As Byte = System.Text.Encoding.Unicode.GetBytes(plaintext)

        ' Create the stream.
        Dim ms As New System.IO.MemoryStream
        ' Create the encoder to write to the stream.
        Dim encStream As New System.Security.Cryptography.CryptoStream(ms, TripleDes.CreateEncryptor(), Security.Cryptography.CryptoStreamMode.Write)

        ' Use the crypto stream to write the byte array to the stream.
        encStream.Write(plaintextBytes, 0, plaintextBytes.Length)
        encStream.FlushFinalBlock()

        ' Convert the encrypted stream to a printable string.
        Return Convert.ToBase64String(ms.ToArray)
    End Function

    Public Function DecryptData(ByVal encryptedtext As String) As String
        ' Convert the encrypted text string to a byte array.
        Dim encryptedBytes() As Byte = Convert.FromBase64String(encryptedtext)

        ' Create the stream.
        Dim ms As New System.IO.MemoryStream
        ' Create the decoder to write to the stream.
        Dim decStream As New System.Security.Cryptography.CryptoStream(ms, TripleDes.CreateDecryptor(), Security.Cryptography.CryptoStreamMode.Write)

        ' Use the crypto stream to write the byte array to the stream.
        decStream.Write(encryptedBytes, 0, encryptedBytes.Length)
        decStream.FlushFinalBlock()

        ' Convert the plaintext stream to a string.
        Return System.Text.Encoding.Unicode.GetString(ms.ToArray)
    End Function
End Class
