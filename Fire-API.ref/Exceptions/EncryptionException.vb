''' <summary>
''' Represents an error which throws when the encryption algorithm is not supported
''' </summary>
Public Class EncryptionException
    Inherits Exception

    ' Constructor
    Public Sub New()
        MyBase.New()
    End Sub

    ' Exception message
    Public Overrides ReadOnly Property Message() As String
        Get
            Return Messages.ENCRYPTION_EXCEPTION
        End Get
    End Property
End Class
