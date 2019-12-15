''' <summary>
''' Represents an error which throws when trying to use a wrong <seealso cref="Server.ServerName"/>
''' </summary>
Public Class UnknownServerException
    Inherits Exception

    ' Constructor
    Public Sub New()
        MyBase.New()
    End Sub

    ' Exception message
    Public Overrides ReadOnly Property Message() As String
        Get
            Return Messages.UNKNOWN_SERVER_EXCEPTION
        End Get
    End Property
End Class
