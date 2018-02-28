''' <summary>
''' Represents an error to throw when the encryption algorithm is not supported in a class.
''' </summary>
Public Class EncryptionException
    Inherits Exception

    Public Sub New()
        MyBase.New()
    End Sub

    Public Overrides ReadOnly Property Message() As String
        Get
            Return "Can't use this class with this encryption algorithm."
        End Get
    End Property
End Class
