''' <summary>
''' Constant utils class.
''' </summary>
Public Class Constants

    ''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''                   PI & EULER                   ''
    ''''''''''''''''''''''''''''''''''''''''''''''''''''

    Public Const Pi = 3.1415926535897931
    Public Const E = 2.7182818284590451
    Public Const Euler = 2.7182818284590451

    ''' <summary>
    ''' Calculate Pi.
    ''' </summary>
    Public Shared Function CalculatePi() As Double
        Dim result = 4 * Math.Atan(1)

        Return result
    End Function

    ''' <summary>
    ''' Calculate Euler.
    ''' </summary>
    ''' <param name="iterations">Number of iterations (default is maximum for 32bit and 64bit, 18 iterations).</param>
    Public Shared Function CalculateEuler(Optional iterations As Long = 18) As Double
        Dim denominator As Long = 2
        Dim lastmultiplier As Long = 2
        Dim result As Double = 2

        For i = 1 To iterations
            result += 1 / denominator
            denominator *= lastmultiplier + 1
            lastmultiplier += 1
        Next

        ' Example :  2 + 1/(1*2) + 1/(1*2*3) + 1/(1*2*3*4) etc..

        Return result
    End Function
End Class
