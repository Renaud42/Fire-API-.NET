Imports System.Net
Imports System.Text

Public Class Fire_Auth
    Private Shared Response As String = ""
    Private Shared Mumble_Decoded As Object = Decode(Constants.JSON_MUMBLE), Mumble_CVP_Decoded As Object = Decode(Constants.JSON_MUMBLE_CVP), VPS_Decoded As Object = Decode(Constants.JSON_VPS)

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
        CPU_USE_0
        CPU_USE_1
        CPU_USE_2
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
    ''' <param name="AppAuthorName">Name of author (can be author, corporation or enterprise).</param>
    ''' <param name="Theme">Theme of the window.</param>
    ''' <param name="Email">Do you need e-mail of the user ?</param>
    ''' <param name="Role">Do you need role of the user ?</param>
    ''' <param name="FireCoins">Do you need amount of Fire-Coins of the user ?</param>
    Public Shared Sub RegisterWithFireAPIWindow(FormLocale As FireLocale, AppAuthorName As String, Theme As FireTheme, Optional Email As Boolean = False, Optional Role As Boolean = False, Optional FireCoins As Boolean = False)
        Dim FireAuthWindow As New Fire_Auth_Window

        FireAuthWindow.ShowDialog()

        While Response = ""

        End While
    End Sub

    Public Shared Function RegisterTask(Username As String, Password As String)

    End Function

    ''' <summary>
    ''' Return a specified information of the server.
    ''' </summary>
    ''' <param name="InfoType">Type of information needed.</param>
    Public Shared Function GetServerInformation(InfoType As ServerInfoType)
        Select Case InfoType
            Case ServerInfoType.CPU_USE_0
                Return VPS_Decoded
        End Select
    End Function
End Class
