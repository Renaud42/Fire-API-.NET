''' <summary>
''' All Fire-API messages
''' </summary>
Public Class Messages
    ' -------------------------------------------
    ' EXCEPTIONS
    ' -------------------------------------------
    ''' <summary>
    ''' Message returned by an <seealso cref="EncryptionException"/>
    ''' </summary>
    Public Const ENCRYPTION_EXCEPTION = "Can't use Triple DES with this encryption algorithm."
    ''' <summary>
    ''' Message returned by a <seealso cref="NetworkUnavailableException"/>
    ''' </summary>
    Public Const NETWORK_UNAVAILABLE_EXCEPTION = "Network unavailable."
    ''' <summary>
    ''' Message returned by a <seealso cref="UnknownDatabaseException"/>
    ''' </summary>
    Public Const UNKNOWN_DATABASE_EXCEPTION = "Unknown database ""%database%""."
    ''' <summary>
    ''' Message returned by an <seealso cref="UnknownServerException"/>
    ''' </summary>
    Public Const UNKNOWN_SERVER_EXCEPTION = "Unknown server name."
    ''' <summary>
    ''' Message returned when can't check for updates
    ''' </summary>
    Public Const UPDATE_CHECK_EXCEPTION = "Can't check for updates :"
    ''' <summary>
    ''' Message returned by a <seealso cref="WrongServerInfoTypeException"/>
    ''' </summary>
    Public Const WRONG_SERVER_INFO_TYPE_EXCEPTION = "Wrong server info type ""%info-type%"" specified for ""%name%"" server."

    ' -------------------------------------------
    ' PREFIXES
    ' -------------------------------------------
    ''' <summary>
    ''' Fire-API logs regular prefix
    ''' </summary>
    Public Const API_PREFIX = "[Fire-API]"
    ''' <summary>
    ''' Fire-API logs error prefix
    ''' </summary>
    Public Const API_ERROR_PREFIX = "[Fire-API Error]"
    ''' <summary>
    ''' Fire-API logs warning prefix
    ''' </summary>
    Public Const API_WARNING_PREFIX = "[Fire-API Warning]"

    ' -------------------------------------------
    ' SUCCESS
    ' -------------------------------------------
    ''' <summary>
    ''' Message shown when Fire-API is successfully loaded with main class initilization
    ''' </summary>
    Public Const API_HELLO = "Fire-API version %api-version% by Fire-Softwares successfully loaded !"
    ''' <summary>
    ''' Message shown when your Fire-API version is the lastest stable
    ''' </summary>
    Public Const API_UP_TO_DATE = "You're running the lastest version of Fire-API (%api-version%), nice !"

    ' -------------------------------------------
    ' WARNINGS
    ' -------------------------------------------
    Public Const API_BETA = "Warning : you are using a beta / unstable build of Fire-API (%api-version%). If you don't know why you see this warning, go back to lastest stable version (%lastest-stable%) on %api-update-page%. Or else you're in the future of Fire-API 8)"
    ''' <summary>
    ''' Message shown when a Fire-API update is available
    ''' </summary>
    Public Const API_UPDATE_AVAILABLE = "New update available : Fire-API version %new-version%. You can get the update here : %api-update-page%"
End Class
