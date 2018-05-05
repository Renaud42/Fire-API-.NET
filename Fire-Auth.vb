Public Class Fire_Auth
    ' Response
    Private Shared API_Response As String = ""

    ' Files downloaded from Initialize(InfoType As ServerInfoType) method.
    Private Shared JSON_MUMBLE_FILESTATUS As IO.File, JSON_MUMBLE_CVP_FILESTATUS As IO.File, JSON_VPS_FILESTATUS As IO.File

    ''' <summary>
    ''' Integrated locales.
    ''' </summary>
    Public Enum FireLocale
        Arabian
        Chinese
        English
        French
        Hindi
        Italian
        Japanese
        Russian
        Spanish
    End Enum

    ''' <summary>
    ''' Integrated themes.
    ''' </summary>
    Public Enum FireTheme
        Light
        Dark
    End Enum

    ''' <summary>
    ''' Type of server info required.
    ''' </summary>
    Public Enum ServerInfoType
        CPU_USE_1
        CPU_USE_2
        CPU_USE_AVERAGE
        DISK_MAX
        DISK_PERCENT
        DISK_USED
        MUMBLE_CHANNEL_VIEWER_PROTOCOL
        MUMBLE_ONLINE
        MUMBLE_UPTIME
        MUMBLE_USERS
        RAM_MAX
        RAM_PERCENT
        RAM_USED
        SERVER_ONLINE
    End Enum

    ''' <summary>
    ''' Spawn a window to connect with Fire-Softwares and get username + other stuff precised in params.
    ''' </summary>
    ''' <param name="FormLocale">Locale of the window.</param>
    ''' <param name="Theme">Theme of the window.</param>
    ''' <param name="Email">Do you need e-mail of the user ?</param>
    ''' <param name="Role">Do you need role of the user ?</param>
    ''' <param name="FireCoins">Do you need amount of Fire-Coins of the user ?</param>
    Public Shared Sub RegisterWithFireAPIWindow(FormLocale As FireLocale, Theme As FireTheme, Optional Email As Boolean = False, Optional Role As Boolean = False, Optional FireCoins As Boolean = False)
        Dim FireAuthWindow As New Fire_Auth_Window

        FireAuthWindow.ShowDialog()

        While API_Response = ""

        End While
    End Sub

    ''' <summary>
    ''' Return a specified information of the server.
    ''' </summary>
    ''' <param name="InfoType">Type of information needed.</param>
    Public Shared Function GetServerInformation(InfoType As ServerInfoType)
        ' Initializing the JSON file corresponding to the server of the specified ServerInfoType.
        Initialize(InfoType)

        Dim jss As New Web.Script.Serialization.JavaScriptSerializer

        Select Case InfoType
            ' TESTING
            Case ServerInfoType.CPU_USE_1
                Dim l As List(Of Object) = jss.Deserialize(Of List(Of Object))(IO.File.ReadAllText(Constants.API_Folder & Constants.API_ApplicationName & "\temp.json"))
                Return l.Count
            Case ServerInfoType.CPU_USE_2

            Case ServerInfoType.CPU_USE_AVERAGE

            Case ServerInfoType.DISK_MAX

            Case ServerInfoType.DISK_PERCENT

            Case Else
                Return "Invalid ServerInfoType."
        End Select
    End Function

    ''' <summary>
    ''' Initialize one JSON status file.
    ''' </summary>
    ''' <param name="InfoType"><seealso cref="ServerInfoType"/> to get file for.</param>
    ''' <param name="NetworkCheck">Check if an network connection is available else returns an <seealso cref="Exception"/> with message "Can't connect to the network."</param>
    Public Shared Sub Initialize(InfoType As ServerInfoType, Optional NetworkCheck As Boolean = True)
        If NetworkCheck Then
            If My.Computer.Network.IsAvailable Then
                InitializationWorker(InfoType)
            Else
                Throw New Exception("Can't connect to the network.")
            End If
        Else
            InitializationWorker(InfoType)
        End If
    End Sub

    ''' <summary>
    ''' Initialization worker.
    ''' </summary>
    ''' <param name="InfoType"><seealso cref="ServerInfoType"/> to get file for.</param>
    Private Shared Sub InitializationWorker(InfoType As ServerInfoType)
        Select Case InfoType
            Case ServerInfoType.CPU_USE_1, ServerInfoType.CPU_USE_2, ServerInfoType.CPU_USE_AVERAGE, ServerInfoType.DISK_MAX, ServerInfoType.DISK_PERCENT, ServerInfoType.DISK_USED, ServerInfoType.RAM_MAX, ServerInfoType.RAM_PERCENT, ServerInfoType.RAM_USED, ServerInfoType.SERVER_ONLINE
                DestroyTempJSON()
                My.Computer.Network.DownloadFile(Constants.JSON_VPS, Constants.API_Folder & Constants.API_ApplicationName & "\temp.json")
            Case ServerInfoType.MUMBLE_ONLINE, ServerInfoType.MUMBLE_USERS
                DestroyTempJSON()
                My.Computer.Network.DownloadFile(Constants.JSON_MUMBLE, Constants.API_Folder & Constants.API_ApplicationName & "\temp.json")
            Case ServerInfoType.MUMBLE_CHANNEL_VIEWER_PROTOCOL, ServerInfoType.MUMBLE_UPTIME
                DestroyTempJSON()
                My.Computer.Network.DownloadFile(Constants.JSON_MUMBLE_CVP, Constants.API_Folder & Constants.API_ApplicationName & "\temp.json")
        End Select
    End Sub

    ''' <summary>
    ''' Destroy temp JSON locally created status file.
    ''' </summary>
    Public Shared Sub DestroyTempJSON()
        If IO.File.Exists(Constants.API_Folder & Constants.API_ApplicationName & "\temp.json") Then
            IO.File.Delete(Constants.API_Folder & Constants.API_ApplicationName & "\temp.json")
        End If
    End Sub
End Class
