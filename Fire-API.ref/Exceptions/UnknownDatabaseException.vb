''' <summary>
''' Represents an error which throws when trying to use a wrong database
''' </summary>
Public Class UnknownDatabaseException
    Inherits Exception

    ' Fields
    Private dbname As String

    ' Constructor
    Public Sub New(dbname As String)
        MyBase.New()
        Me.dbname = dbname
    End Sub

    ' Exception message
    Public Overrides ReadOnly Property Message() As String
        Get
            Return Messages.UNKNOWN_DATABASE_EXCEPTION.Replace("%database%", dbname)
        End Get
    End Property
End Class
