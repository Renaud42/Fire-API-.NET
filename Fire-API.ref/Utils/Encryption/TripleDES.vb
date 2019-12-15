Imports Fire_API.ref.Encryption
Imports System.Security.Cryptography
Imports System.Text.Encoding

''' <summary>
''' Advanced custom integrated TripleDES class
''' <para>Initialization : </para>
''' <para><code>Dim wrapper As New Simple3Des(password, hashingalgorithm)</code></para>
''' <para>Encrypt text :</para>
''' <para><code>Dim encryptedText As String = wrapper.EncryptData(string)</code></para>
''' <para>Decrypt text :</para>
''' <para><code>Dim decryptedText As String = wrapper.DecryptData(encryptedString)</code></para>
''' </summary>
Public NotInheritable Class TripleDES
    Private TripleDes As New TripleDESCryptoServiceProvider

    ''' <summary>
    ''' Function to truncate hash with the key/password, the length and the selected encryption algorithm.
    ''' </summary>
    ''' <param name="key">Key/password to encrypt the text</param>
    ''' <param name="length">Chain length</param>
    ''' <param name="hashingalgorithm">Encryption algorithm to encrypt the text</param>
    ''' <returns>Truncated with corresponding hashing algorithm hash</returns>
    Private Function TruncateHash(ByVal key As String, ByVal length As Integer, ByVal hashingalgorithm As HashingAlgorithm) As Byte()
        Dim keyBytes() As Byte = Unicode.GetBytes(key)  ' Hash the key
        Dim hash() As Byte

        Dim csp     ' Variable used to store Crypto Service Provider type class

        ' Computing hash with corresponding encryption algorithm
        Select Case hashingalgorithm
            Case hashingalgorithm.MD5
                csp = New MD5CryptoServiceProvider
            Case hashingalgorithm.SHA1
                csp = New SHA1CryptoServiceProvider
            Case hashingalgorithm.SHA256
                csp = New SHA256CryptoServiceProvider
            Case hashingalgorithm.SHA384
                csp = New SHA384CryptoServiceProvider
            Case hashingalgorithm.SHA512
                csp = New SHA512CryptoServiceProvider
            Case Else
                Throw New EncryptionException
        End Select

        ' Computing hash with corresponding Crypto Service Provider
        hash = csp.ComputeHash(keyBytes)

        ReDim Preserve hash(length - 1)     ' Truncate or pad the hash
        Return hash
    End Function

    ''' <summary>
    ''' Creates new TripleDES wrapper
    ''' </summary>
    ''' <param name="key">The password</param>
    ''' <param name="hashingalgorithm">The hashing algorithm to use</param>
    Sub New(ByVal key As String, ByVal hashingalgorithm As HashingAlgorithm)
        Try
            ' Initialize the crypto provider
            TripleDes.Key = TruncateHash(key, TripleDes.KeySize \ 8, hashingalgorithm)   ' Key
            TripleDes.IV = TruncateHash("", TripleDes.BlockSize \ 8, hashingalgorithm)   ' Initialization Vector
        Catch ex As Exception
            Throw New EncryptionException
        End Try
    End Sub

    ''' <summary>
    ''' Function to encrypt data
    ''' </summary>
    ''' <param name="plaintext">Text to encrypt</param>
    ''' <returns>Text encrypted</returns>
    Public Function EncryptData(ByVal plaintext As String) As String
        ' Convert plain text to a byte array
        Dim plaintextBytes() As Byte = Unicode.GetBytes(plaintext)

        ' Create the stream
        Dim ms As New System.IO.MemoryStream
        ' Create the encoder to write to the stream
        Dim encStream As New CryptoStream(ms, TripleDes.CreateEncryptor(), CryptoStreamMode.Write)

        ' Use the crypto stream to write the byte array to the stream
        encStream.Write(plaintextBytes, 0, plaintextBytes.Length)
        encStream.FlushFinalBlock()

        ' Convert the encrypted stream to a printable string
        Return System.Convert.ToBase64String(ms.ToArray)
    End Function

    ''' <summary>
    ''' Function to decrypt data
    ''' </summary>
    ''' <param name="encryptedtext">Hash to decrypt</param>
    ''' <returns>Text decrypted</returns>
    Public Function DecryptData(ByVal encryptedtext As String) As String
        ' Convert the encrypted text string to a byte array
        Dim encryptedBytes() As Byte = System.Convert.FromBase64String(encryptedtext)

        ' Create the stream
        Dim ms As New System.IO.MemoryStream
        ' Create the decoder to write to the stream
        Dim decStream As New CryptoStream(ms, TripleDes.CreateDecryptor(), CryptoStreamMode.Write)

        ' Use the crypto stream to write the byte array to the stream
        decStream.Write(encryptedBytes, 0, encryptedBytes.Length)
        decStream.FlushFinalBlock()

        ' Convert the plain text stream to a string
        Return Unicode.GetString(ms.ToArray)
    End Function
End Class
