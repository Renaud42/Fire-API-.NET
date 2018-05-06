Public Class Fire_Auth
    ' Response
    Private Shared API_Response As String = ""

    ''' <summary>
    ''' Type of server info required.
    ''' </summary>
    Public Enum ServerInfoType
        CPU_USE_1
        CPU_USE_2
        CPU_USE_AVERAGE
        DISK_MAX
        DISK_PERCENT
        DISK_UNIT
        DISK_USED
        HOSTNAME
        MUMBLE_ONLINE
        MUMBLE_USERS
        RAM_MAX
        RAM_PERCENT
        RAM_UNIT
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
    Public Overloads Shared Function RegisterWithFireAPIWindow(FormLocale As Constants.FireLocale, Theme As Constants.FireTheme, Optional Email As Boolean = False, Optional Role As Boolean = False, Optional FireCoins As Boolean = False)
        Dim FireAuthWindow As New Fire_Auth_Window

        LocaleWorker(FormLocale, Email, Role, FireCoins, FireAuthWindow)

        FireAuthWindow.ShowDialog()

        While API_Response = ""

        End While

        Return API_Response
    End Function

    ''' <summary>
    ''' Spawn a window to connect with Fire-Softwares and get username + other stuff precised in params.
    ''' </summary>
    ''' <param name="FormLocale">Locale of the window.</param>
    ''' <param name="CustomTheme">Custom theme of the window (see documentation for themes @ https://api.fire-softwares.ga).</param>
    ''' <param name="Email">Do you need e-mail of the user ?</param>
    ''' <param name="Role">Do you need role of the user ?</param>
    ''' <param name="FireCoins">Do you need amount of Fire-Coins of the user ?</param>
    Public Overloads Shared Function RegisterWithFireAPIWindow(FormLocale As Constants.FireLocale, CustomTheme As IO.File, Optional Email As Boolean = False, Optional Role As Boolean = False, Optional FireCoins As Boolean = False)
        Dim FireAuthWindow As New Fire_Auth_Window

        LocaleWorker(FormLocale, Email, Role, FireCoins, FireAuthWindow)

        FireAuthWindow.ShowDialog()

        While API_Response = ""

        End While

        Return API_Response
    End Function

    ''' <summary>
    ''' Some tasks about locale.
    ''' </summary>
    Private Shared Sub LocaleWorker(FormLocale As Constants.FireLocale, Email As Boolean, Role As Boolean, FireCoins As Boolean, FireAuthWindow As Fire_Auth_Window)
        Dim PermissionsString As String = Constants.ReturnTranslation(Constants.FireTranslations.InformationAccessWarning, FormLocale)

        If Email Then
            PermissionsString &= Constants.ReturnTranslation(Constants.FireTranslations.Email, FormLocale) + ", "
        End If
        If Role Then
            PermissionsString &= Constants.ReturnTranslation(Constants.FireTranslations.Role, FormLocale) + ", "
        End If
        If FireCoins Then
            PermissionsString &= "Fire-Coins, "
        End If

        PermissionsString = PermissionsString.Substring(0, PermissionsString.Length - 2)

        FireAuthWindow.AuthorizationLbl.Text = PermissionsString
        FireAuthWindow.UsernameTxtBox.Text = Constants.ReturnTranslation(Constants.FireTranslations.Username, FormLocale)
        FireAuthWindow.LoginButton.Text = Constants.ReturnTranslation(Constants.FireTranslations.Login, FormLocale)
    End Sub

    ''' <summary>
    ''' Return a specified information of the server.
    ''' </summary>
    ''' <param name="InfoType">Type of information needed.</param>
    Public Shared Function GetServerInformation(InfoType As ServerInfoType)
        ' Initializing the JSON file corresponding to the server of the specified ServerInfoType.
        Initialize(InfoType)

        Dim jss As New Web.Script.Serialization.JavaScriptSerializer
        Dim response As Object

        response = jss.DeserializeObject(IO.File.ReadAllText(Constants.API_Folder & Constants.API_ApplicationName & "\temp.json"))

        Select Case InfoType
            Case ServerInfoType.CPU_USE_1
                Return response("status")("cpu")("1")
            Case ServerInfoType.CPU_USE_2
                Return response("status")("cpu")("2")
            Case ServerInfoType.CPU_USE_AVERAGE
                Return response("status")("cpu")("0")
            Case ServerInfoType.DISK_MAX
                Return response("status")("disk")("total")
            Case ServerInfoType.DISK_PERCENT
                Return response("status")("disk")("percent")
            Case ServerInfoType.DISK_UNIT
                Return response("status")("units")("disk")
            Case ServerInfoType.DISK_USED
                Return response("status")("disk")("used")
            Case ServerInfoType.HOSTNAME
                Return response("status")("hostname")
            Case ServerInfoType.MUMBLE_ONLINE
                Return response("status")("online")
            Case ServerInfoType.MUMBLE_USERS
                Return response("status")("players")("online")
            Case ServerInfoType.RAM_MAX
                Return response("status")("ram")("total")
            Case ServerInfoType.RAM_PERCENT
                Return response("status")("ram")("percent")
            Case ServerInfoType.RAM_UNIT
                Return response("status")("units")("ram")
            Case ServerInfoType.RAM_USED
                Return response("status")("ram")("used")
            Case ServerInfoType.SERVER_ONLINE
                Return response("status")("online")
            Case Else
                Throw New Exception("Invalid ServerInfoType.")
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
            Case ServerInfoType.CPU_USE_1, ServerInfoType.CPU_USE_2, ServerInfoType.CPU_USE_AVERAGE, ServerInfoType.DISK_MAX, ServerInfoType.DISK_PERCENT, ServerInfoType.DISK_UNIT, ServerInfoType.DISK_USED, ServerInfoType.HOSTNAME, ServerInfoType.RAM_MAX, ServerInfoType.RAM_PERCENT, ServerInfoType.RAM_UNIT, ServerInfoType.RAM_USED, ServerInfoType.SERVER_ONLINE
                Fire_API_ref.DestroyTempJSON()
                My.Computer.Network.DownloadFile(Constants.JSON_VPS, Constants.API_Folder & Constants.API_ApplicationName & "\temp.json")
            Case ServerInfoType.MUMBLE_ONLINE, ServerInfoType.MUMBLE_USERS
                Fire_API_ref.DestroyTempJSON()
                My.Computer.Network.DownloadFile(Constants.JSON_MUMBLE, Constants.API_Folder & Constants.API_ApplicationName & "\temp.json")
            Case Else
                Throw New Exception("Can't initialize the file corresponding to ServerInfoType """ & InfoType & """")
        End Select
    End Sub
End Class
