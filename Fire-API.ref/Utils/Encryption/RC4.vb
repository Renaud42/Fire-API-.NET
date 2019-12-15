''' <summary>
''' Advanced custom integrated RC4 class
''' <para>BETA WIP : decrypt not working for now, but is Work In Progress, so
''' don't try to use it</para>
''' </summary>
Public Class RC4

    ''' <summary>
    ''' Function to encrypt data
    ''' </summary>
    ''' <param name="plaintext">Text to encrypt</param>
    ''' <param name="password">The password</param>
    Public Shared Function EncryptData(plaintext As String, password As String)
        Dim sbox(256), key(256)

        Dim temp As Integer

        Dim a As Integer
        Dim i = 0, j = 0, k As Integer

        Dim cipherby As Integer
        Dim cipher = ""

        Initialize(password, key, sbox)

        For a = 1 To Len(plaintext)
            i = (i + 1) Mod 256
            j = (j + sbox(i)) Mod 256

            ' Swapping values
            temp = sbox(i)
            sbox(i) = sbox(j)
            sbox(j) = temp

            k = sbox((sbox(i) + sbox(j)) Mod 256)

            cipherby = (Asc(Mid$(plaintext, a, 1))) Xor k
            If Len(Convert.ToInt32(cipherby, 16)) = 1 Then
                cipher &= "0" & Convert.ToInt32(cipherby, 16)
            Else
                cipher &= Convert.ToInt32(cipherby, 16)
            End If
        Next

        Return cipher
    End Function

    ''' <summary>
    ''' Function to decrypt data
    ''' </summary>
    ''' <param name="encryptedtext">Text to decrypt</param>
    ''' <param name="password">The password</param>
    Public Shared Function DecryptData(encryptedtext As String, password As String)
        Dim sbox(256) As Integer
        Dim key(256) As Integer

        Dim Text2 = ""

        Dim temp As Integer

        Dim a As Integer, i = 0, j = 0, k As Integer, w As Integer

        Dim cipherby As Integer
        Dim cipher = ""

        For w = 1 To Len(encryptedtext) Step 2
            Text2 &= Chr(Convert.ToInt32(Mid$(encryptedtext, w, 2), 16))
        Next

        Initialize(password, key, sbox)

        For a = 1 To Len(Text2)
            i = (i + 1) Mod 256
            j = (j + sbox(i)) Mod 256

            ' Swapping values
            temp = sbox(i)
            sbox(i) = sbox(j)
            sbox(j) = temp

            k = sbox((sbox(i) + sbox(j)) Mod 256)

            cipherby = Asc(Mid$(Text2, a, 1)) Xor k
            cipher &= Chr(cipherby)
        Next

        Return cipher
    End Function

    ''' <summary>
    ''' Internal function to initilize RC4
    ''' </summary>
    ''' <param name="password">The password</param>
    ''' <param name="key">The key</param>
    ''' <param name="sbox">The sbox</param>
    Private Shared Sub Initialize(password, ByRef key, ByRef sbox)
        Dim swap = 0, b = 0, length = Len(password)

        For a = 0 To 255
            key(a) = Asc(Mid$(password, a Mod length + 1, 1))
            sbox(a) = a
        Next

        For a = 0 To 255
            b = (b + sbox(a) + key(a)) Mod 256  ' Computing b

            ' Swapping values
            swap = sbox(a)
            sbox(a) = sbox(b)
            sbox(b) = swap
        Next
    End Sub
End Class
