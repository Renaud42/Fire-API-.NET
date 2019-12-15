''' <summary>
''' Represents an error which throws when a wrong <seealso cref="Server.ServerInfoType"/> is specified
''' </summary>
Public Class WrongServerInfoTypeException
    Inherits Exception

    ' Fields
    Dim name As Server.ServerName
    Dim infotype As Server.ServerInfoType

    ' Constructor
    Public Sub New(name As Server.ServerName, infotype As Server.ServerInfoType)
        MyBase.New()
        Me.name = name
        Me.infotype = infotype
    End Sub

    ' Exception message
    Public Overrides ReadOnly Property Message() As String
        Get
            Return Messages.WRONG_SERVER_INFO_TYPE_EXCEPTION.Replace("%name%", name.ToString).Replace("%info-type%", infotype.ToString)
        End Get
    End Property
End Class
