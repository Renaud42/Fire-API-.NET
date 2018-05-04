''' <summary>
''' Integrated JavaScript Object Notation decoder.
''' </summary>
Module FireJSONParser

    Public Function Decode(ByVal json As String) As Object
        Dim obj As Object = Nothing
        If JsonValue(json, obj) AndAlso json.Length = 0 Then Return obj
        Return Nothing
    End Function

    Dim WhiteSpace As Char() = (vbCrLf & vbTab & " " & Chr(160)).ToCharArray

    Private Sub Trim(ByRef s As String)
        s = s.Trim(WhiteSpace)
    End Sub

    Private Sub Substring(ByRef s As String, ByVal startIndex As Integer)
        s = s.Substring(startIndex)
    End Sub

    Private Function JsonValue(ByRef json As String, ByRef o As Object) As Boolean
        If JsonString(json, o) Then Return True
        If JsonNumber(json, o) Then Return True
        If JsonObject(json, o) Then Return True
        If JsonArray(json, o) Then Return True
        Trim(json)
        If json.StartsWith("true") Then o = True : Substring(json, 4) : Return True
        If json.StartsWith("false") Then o = False : Substring(json, 5) : Return True
        If json.StartsWith("null") Then o = Nothing : Substring(json, 4) : Return True
        Return False
    End Function

    Private EscapeChar As String = """\/" & vbCrLf & vbTab & vbBack & vbFormFeed
    Private Function JsonString(ByRef json As String, ByRef o As Object) As Boolean
        Trim(json)
        If Not json.StartsWith("""") Then Return False
        Dim a As String = ""
        Dim escape As Boolean = False
        Dim unicode As Integer = 0
        Dim hex As String = ""
        Dim index As Integer = 0
        While True
            index += 1
            If index >= json.Length Then
                json = "ERROR-STRING-LENGTH"
                o = ""
                Return True
            End If
            Dim c As Char = json(index)
            If escape AndAlso unicode > 0 Then
                unicode -= 1
                hex &= c
                If unicode = 0 Then
                    escape = False
                    c = ChrW(Val(hex))
                    a &= c
                End If
            ElseIf escape Then
                escape = False
                If EscapeChar.Contains(c) Then
                    a &= c
                ElseIf c = "u" Then
                    unicode = 4
                    hex = "&h"
                Else
                    json = "ERROR-STRING-ESCAPE"
                    o = ""
                    Return True
                End If
            ElseIf c = """" Then
                Substring(json, index + 1)
                o = a
                Return True
            Else
                a &= c
            End If
        End While
    End Function

    Private Function JsonNumber(ByRef json As String, ByRef o As Object) As Boolean
        Dim index As Integer = 0
        If json(index) = "-" Then index += 1
        If json(index) = "0" Then : index += 1
        ElseIf "123456789".Contains(json(index)) Then : index += 1
            While "0123456789".Contains(json(index)) : index += 1 : End While
        Else : Return False : End If
        If json(index) = "." Then : index += 1
            If "0123456789".Contains(json(index)) Then : index += 1
                While "0123456789".Contains(json(index)) : index += 1 : End While
            Else : Return False : End If
        End If
        If json(index) = "e" Or json(index) = "E" Then : index += 1
            If json(index) = "+" Or json(index) = "-" Then index += 1
            If "0123456789".Contains(json(index)) Then : index += 1
                While "0123456789".Contains(json(index)) : index += 1 : End While
            Else : Return False : End If
        End If
        o = Val(json.Substring(0, index))
        Substring(json, index)
        Return True
    End Function

    Private Function JsonObject(ByRef json As String, ByRef o As Object) As Boolean
        Dim key As Object = Nothing
        Dim value As Object = Nothing
        Trim(json)
        If Not json.StartsWith("{") Then Return False
        Dim a As New Dictionary(Of String, Object)
        o = a
        Substring(json, 1)
        While True
            Trim(json)
            If json.StartsWith("}") Then
                Substring(json, 1)
                Return True
            ElseIf a.Count > 0 Then
                If json.StartsWith(",") Then
                    Substring(json, 1)
                Else
                    json = "ERROR-OBJECT-VIRGULE"
                    Return True
                End If
            End If
            If Not JsonString(json, key) Then
                json = "ERROR-OBJECT-KEY"
                Return True
            End If
            Trim(json)
            If json.StartsWith(":") Then
                Substring(json, 1)
            Else
                json = "ERROR-OBJECT-DOT"
                Return True
            End If
            If Not JsonValue(json, value) Then
                json = "ERROR-OBJECT-VALUE"
                Return True
            End If
            a.Add(key, value)
        End While
    End Function

    Private Function JsonArray(ByRef json As String, ByRef o As Object) As Boolean
        Dim value As Object = Nothing
        Trim(json)
        If Not json.StartsWith("[") Then Return False
        Dim a As New ArrayList
        o = a
        Substring(json, 1)
        While True
            Trim(json)
            If json.StartsWith("]") Then
                Substring(json, 1)
                Return True
            ElseIf a.Count > 0 Then
                If json.StartsWith(",") Then
                    Substring(json, 1)
                Else
                    json = "ERROR-ARRAY-VIRGULE"
                    Return True
                End If
            End If
            If Not JsonValue(json, value) Then
                json = "ERROR-ARRAY-VALUE"
                Return True
            End If
            a.Add(value)
        End While
    End Function

End Module
