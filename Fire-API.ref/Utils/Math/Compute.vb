''' <summary>
''' Computations algorithm are implemented in this class
''' </summary>
Public Class Compute
    ' Constructor
    Public Sub New()
        MyBase.New()
    End Sub

    ' -------------------------------------------
    ' FUNCTIONS & METHODS
    ' -------------------------------------------
    ''' <summary>
    ''' Compute Champernowne String with specified digits on 32-bit platform
    ''' </summary>
    ''' <param name="digits">Number of digits to compute</param>
    Public Function CalculateChampernowneString32(digits As UInteger) As String
        Dim result As String = String.Empty, x As UInteger = 1

        While result.Length < digits
            If result.Length < digits Then
                Dim current = 0

                While current < x.ToString.Length
                    If result.Length = digits Then
                        current = x.ToString.Length
                    Else
                        result += String.Empty & x.ToString.Substring(current, 1)
                        current += 1
                    End If
                End While

                x += 1
            End If
        End While

        Return "0." + result
    End Function

    ''' <summary>
    ''' Compute Champernowne String with specified digits on 64-bit platform
    ''' </summary>
    ''' <param name="digits">Number of digits to compute</param>
    Public Function CalculateChampernowneString64(digits As ULong) As String
        Dim Result As String = String.Empty, x As ULong = 1

        While Result.Length < digits
            If Result.Length < digits Then
                Dim current = 0

                While current < x.ToString.Length
                    If Result.Length = digits Then
                        current = x.ToString.Length
                    Else
                        Result += String.Empty & x.ToString.Substring(current, 1)
                        current += 1
                    End If
                End While

                x += 1
            End If
        End While

        Return "0." + Result
    End Function
End Class
