''' <summary>
''' This object is for define properties for a temp file/folder.
''' </summary>
Public Class TempObject
    ' Variables
    Private tt As TempTypes
    Private tn As String
    Private td As String

    ' Enumerate TempObject types
    ''' <summary>
    ''' TempTypes may be :
    ''' <para>- Folder</para>
    ''' <para>- File</para>
    ''' </summary>
    Public Enum TempTypes
        Folder
        File
    End Enum

    ' What occurs when we create a new TempObject.
    Public Sub New(ByVal TempType As TempTypes, ByVal TempName As String, ByVal TempDir As String)
        tt = TempType
        tn = TempName
        td = TempDir
    End Sub

    ' Getters and setters
    Public Property TempType() As TempTypes
        Get
            Return tt
        End Get
        Set(value As TempTypes)
            tt = value
        End Set
    End Property
    Public Property TempName() As String
        Get
            Return tn
        End Get
        Set(value As String)
            tn = value
        End Set
    End Property
    Public Property TempDir() As String
        Get
            Return td
        End Get
        Set(value As String)
            td = value
        End Set
    End Property
    Public Property TempObj() As TempObject
        Get
            Return Me
        End Get
        Set(value As TempObject)
            tt = value.TempType
            tn = value.TempName
            td = value.TempDir
        End Set
    End Property
End Class
