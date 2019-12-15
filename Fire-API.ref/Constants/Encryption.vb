''' <summary>
''' Encryption-related constants
''' </summary>
Public Class Encryption
    ''' <summary>
    ''' Cryptographic hash algorithms
    ''' </summary>
    Public Enum HashingAlgorithm
        ''' <summary>
        ''' Message Digest 5 ; 128-bit output - 64 rounds
        ''' </summary>
        MD5
        ''' <summary>
        ''' Secure Hash Algorithm ; 160-bit output - 80 rounds
        ''' </summary>
        SHA1
        ''' <summary>
        ''' Secure Hash Algorithm 256-bit ; 256-bit output - 64 rounds
        ''' </summary>
        SHA256
        ''' <summary>
        ''' SHA384 : Secure Hash Algorithm 384-bit ; 384-bit output - 80 rounds
        ''' </summary>
        SHA384
        ''' <summary>
        ''' SHA512 : Secure Hash Algorithm 512-bit ; 512-bit output - 80 rounds
        ''' </summary>
        SHA512
    End Enum

    ''' <summary>
    ''' Type of file encryption
    ''' </summary>
    Public Enum EncryptionType
        ''' <summary>
        ''' Only file content
        ''' </summary>
        FILE_CONTENT
        ''' <summary>
        ''' Only file name
        ''' </summary>
        FILE_NAME
        ''' <summary>
        ''' File content and file name
        ''' </summary>
        BOTH
    End Enum
End Class
