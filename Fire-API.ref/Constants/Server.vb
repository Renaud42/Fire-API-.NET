''' <summary>
''' Server-related constants
''' </summary>
Public Class Server
    ' -------------------------------------------
    ' ENUMS
    ' -------------------------------------------
    ''' <summary>
    ''' Server names
    ''' </summary>
    Public Enum ServerName
        ''' <summary>
        ''' Fire-Softwares server
        ''' <para>Fire-Softwares identifier : #0</para>
        ''' <para>Hostname : firesoftwares</para>
        ''' </summary>
        FIRE_SOFTWARES
        ''' <summary>
        ''' Fire-Network server
        ''' <para>Fire-Softwares identifier : #1</para>
        ''' <para>Hostname : firenetwork</para>
        ''' </summary>
        FIRE_NETWORK
        ''' <summary>
        ''' Fire-Softwares / Fire-Network Mumble server
        ''' <para>Fire-Softwares identifier : #2</para>
        ''' <para>Host : mumble08</para>
        ''' </summary>
        MUMBLE

        ''' <summary>
        ''' Mumble Channel Viewer Protocol
        ''' </summary>
        MUMBLE_CVP = 996
        ''' <summary>
        ''' Discord status
        ''' </summary>
        DISCORD = 997
        ''' <summary>
        ''' Fire-API Web-Framework status
        ''' </summary>
        FRAMEWORK_STATUS = 998
        ''' <summary>
        ''' Fire-API status
        ''' </summary>
        API_STATUS = 999
    End Enum

    ''' <summary>
    ''' Type of server info required
    ''' </summary>
    Public Enum ServerInfoType
        CHANNELS
        CPU_LOAD_0
        CPU_LOAD_1
        CPU_LOAD_2
        DISCORD_ID
        DISK_MAX
        DISK_PERCENT
        DISK_UNIT
        DISK_USED
        HOSTNAME
        MEMBERS
        MEMBERS_ONLINE
        MUMBLE_X_CONNECT_URL
        ONLINE
        PLAYER_COUNT
        RAM_MAX
        RAM_PERCENT
        RAM_UNIT
        RAM_USED
        SERVER_NAME
        SERVER_STATUS
        UPTIME
        VERSION
    End Enum

    ' -------------------------------------------
    ' CONSTANTS
    ' -------------------------------------------
    ''' <summary>
    ''' Server #0 status file URL
    ''' </summary>
    Public Const STATUS_0 = "https://panel.omgserv.com/json/256696/status"
    ''' <summary>
    ''' Server #1 status file URL
    ''' </summary>
    Public Const STATUS_1 = "https://panel.omgserv.com/json/180278/status"
    ''' <summary>
    ''' Server #2 status file URL
    ''' </summary>
    Public Const STATUS_2 = "https://panel.omgserv.com/json/239015/status"
    ''' <summary>
    ''' Fire-API status file URL
    ''' </summary>
    Public Const STATUS_API = "https://api.fire-softwares.ga/api.json"
    ''' <summary>
    ''' Discord status file URL
    ''' </summary>
    Public Const STATUS_DISCORD = "https://canary.discordapp.com/api/guilds/139106057522774016/widget.json"
    ''' <summary>
    ''' Fire-API Web-Framework status file URL
    ''' </summary>
    Public Const STATUS_FRAMEWORK = "https://api.fire-softwares.ga/framework.php"
    ''' <summary>
    ''' Mumble Channel Viewer Protocol URL
    ''' </summary>
    Public Const STATUS_MUMBLE_CVP = "https://panel.omgserv.com/viewer/239015?cvp"
End Class
