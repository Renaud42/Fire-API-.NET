''' <summary>
''' Represents an error which throws when trying to use network when unavailable
''' </summary>
Public Class NetworkUnavailableException
    Inherits Exception

    ' Constructor
    Public Sub New()
        MyBase.New()
    End Sub

    ' Exception message
    Public Overrides ReadOnly Property Message() As String
        Get
            Return Messages.NETWORK_UNAVAILABLE_EXCEPTION
        End Get
    End Property
End Class
