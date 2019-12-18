Imports System.Net

''' <summary>
''' Some useful stuff about requests
''' </summary>
Public Class Requests
    ''' <summary>
    ''' Get a Web file content
    ''' </summary>
    ''' <param name="URL">URL to the file</param>
    ''' <returns>Content of file</returns>
    Public Shared Function GetWebFileContent(URL As String)
        ' Creating a new web request on URL
        Dim request As WebRequest = WebRequest.Create(URL)

        ' Getting response
        Using response As WebResponse = request.GetResponse()
            ' And then reading the response to return it's content
            Using sr As New IO.StreamReader(response.GetResponseStream())
                Dim content As String = sr.ReadToEnd()
                Return content
            End Using
        End Using
    End Function
End Class
