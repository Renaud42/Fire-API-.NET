Imports Fire_API.ref.Server

''' <summary>
''' Make requests to Fire-API and get response with this class.
''' <para>You need to create an instance of this class with something like : <code>Dim wrapper As new API()</code></para>
''' </summary>
Public Class API
    ' Variables
    Private cachedcontent As String

    ' Constructor
    Public Sub New(Optional cachedcontent As String = "")
        MyBase.New()
        Me.cachedcontent = cachedcontent
    End Sub

    ' Functions and subroutines
    ''' <summary>
    ''' Returns a specified information of the specified server
    ''' <para>Note 1 : It's highly recommended to perform this operation in a
    ''' <seealso cref="ComponentModel.BackgroundWorker"/> because this function
    ''' performs lengthy operations (depends on your connection, server
    ''' connection and computer components) so it could make your program
    ''' freeze.</para>
    ''' <para>Note 2 : To prevent refreshing each time you need an information
    ''' (it can be very long), you can set refreshfile param to False to cancel
    ''' refreshing and use last file downloaded to parse information (PS : if you
    ''' request fastly 2 different documents, don't forget to give a little pause
    ''' to your program after first file refresh, OR you can use another
    ''' instance of <seealso cref="API"/> class too and then there is no conflict).</para>
    ''' </summary>
    ''' <param name="name">The server that you want information about</param>
    ''' <param name="infotype">Type of information needed</param>
    Public Function GetServerInformation(name As ServerName, infotype As ServerInfoType, Optional refreshfile As Boolean = True)
        ' Getting required content on corresponding URL
        If My.Computer.Network.IsAvailable Or (Not My.Computer.Network.IsAvailable And Not refreshfile And cachedcontent <> String.Empty) Then
            If (Not refreshfile And cachedcontent = String.Empty) Or refreshfile Then
                cachedcontent = Requests.GetWebFileContent(GetServerStatusURL(name))
            End If

            ' JavaScript Object Notation Deserialization
            Dim jss As New Web.Script.Serialization.JavaScriptSerializer
            Dim response As Object = jss.DeserializeObject(cachedcontent)

            ' Returning corresponding information as a VB.NET JSON-equivalent type
            Select Case name
                Case ServerName.FIRE_SOFTWARES, ServerName.FIRE_NETWORK
                    Select Case infotype
                        Case ServerInfoType.CPU_LOAD_0
                            Return response("status")("cpu")(0)
                        Case ServerInfoType.CPU_LOAD_1
                            Return response("status")("cpu")(1)
                        Case ServerInfoType.CPU_LOAD_2
                            Return response("status")("cpu")(2)
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
                        Case ServerInfoType.ONLINE
                            Return response("status")("online")
                        Case ServerInfoType.RAM_MAX
                            Return response("status")("ram")("total")
                        Case ServerInfoType.RAM_PERCENT
                            Return response("status")("ram")("percent")
                        Case ServerInfoType.RAM_UNIT
                            Return response("status")("units")("ram")
                        Case ServerInfoType.RAM_USED
                            Return response("status")("ram")("used")
                    End Select
                Case ServerName.MUMBLE
                    Select Case infotype
                        Case ServerInfoType.ONLINE
                            Return response("status")("online")
                        Case ServerInfoType.PLAYER_COUNT
                            Return response("status")("players")("online")
                    End Select
                Case ServerName.MUMBLE_CVP
                    Select Case infotype
                        Case ServerInfoType.CHANNELS
                            Return response("root")("channels")
                        Case ServerInfoType.MEMBERS_ONLINE
                            Return response("users")
                        Case ServerInfoType.MUMBLE_X_CONNECT_URL
                            Return response("x_connecturl")
                        Case ServerInfoType.SERVER_NAME
                            Return response("name")
                        Case ServerInfoType.UPTIME
                            Return response("uptime")
                    End Select
                Case ServerName.DISCORD
                    Select Case infotype
                        Case ServerInfoType.CHANNELS
                            Return response("channels")
                        Case ServerInfoType.DISCORD_ID
                            Return response("id")
                        Case ServerInfoType.SERVER_NAME
                            Return response("name")
                        Case ServerInfoType.MEMBERS
                            Return response("members")
                    End Select
                Case ServerName.FRAMEWORK_STATUS, ServerName.API_STATUS
                    Select Case infotype
                        Case ServerInfoType.ONLINE
                            Return response("informations")("online")
                        Case ServerInfoType.VERSION
                            Return response("informations")("version")
                    End Select
            End Select

            ' If nothing returned, there's an error of server info type required
            Throw New WrongServerInfoTypeException(name, infotype)
        Else
            ' If there is no network available, there's a network unavailable exception
            Throw New NetworkUnavailableException
        End If
    End Function

    ''' <summary>
    ''' Get status URL corresponding to a server name
    ''' </summary>
    ''' <param name="name"><seealso cref="ServerName"/> to get status URL</param>
    ''' <returns></returns>
    Private Function GetServerStatusURL(name As ServerName)
        Select Case name
            Case ServerName.FIRE_SOFTWARES
                Return STATUS_0
            Case ServerName.FIRE_NETWORK
                Return STATUS_1
            Case ServerName.MUMBLE
                Return STATUS_2
            Case ServerName.MUMBLE_CVP
                Return STATUS_MUMBLE_CVP
            Case ServerName.API_STATUS
                Return STATUS_API
            Case ServerName.DISCORD
                Return STATUS_DISCORD
            Case ServerName.FRAMEWORK_STATUS
                Return STATUS_FRAMEWORK
            Case Else
                Throw New UnknownServerException
        End Select
    End Function
End Class
