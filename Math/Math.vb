Option Explicit On
Option Strict On

Public Class Math


    ''' <summary>
    ''' The default range is 0 - 10.
    ''' The maximum number must be greater than minimum number.
    ''' </summary>
    ''' <param name="max%"></param>
    ''' <param name="min%"></param>
    ''' <returns>Returns a random integer within a range defined by the max and min arguments.</returns>
    ''' <exception cref="System.ArgumentException">Thrown when <c>max > min</c></exception>
    Public Shared Function RandomNumberInRange(Optional max% = 10%, Optional min% = 0%) As Integer
        Dim _max% = max - min
        If _max < 0 Then
            Throw New System.ArgumentException("Maximum number must be greater than minimum number")
        End If
        Randomize(DateTime.Now.Millisecond)
        Return CInt(System.Math.Floor(Rnd() * (_max + 1))) + min
    End Function



    '''<summary>
    '''solves two simultaneous equations: a1 + b1 = k1,  a2 + b2 = k2 
    '''</summary>
    '''<returns>
    '''returns tuple with (aTermResult, bTermResult) as decimal
    '''</returns>
    Public Shared Function SolveSimultaneousEquation(ByVal a1@, ByVal b1@, ByVal k1@, ByVal a2@, ByVal b2@, ByVal k2@) As (aTermResult@, bTermResult@)
        Dim aTermResult@, bTermResult@, numerator@, denominator@
        Dim result = (aTermResult, bTermResult)
        denominator = (a1 * b2) - (b1 * a2)
        numerator = (k1 * b2) - (b1 * k2)
        result.aTermResult = (numerator / denominator)
        numerator = (a1 * k2) - (k1 * a2)
        result.bTermResult = (numerator / denominator)
        Return result
    End Function


    'Format
    Private Shared Function CEngNotation(doubleValue As Double) As String
        'found this code at: http://mibifici.blogspot.com/2012/02/engineering-notation-in-vbavb6.html
        Dim x As Double    ' --- Original Double (Floating-point)
        Dim y As Double    ' --- Mantissa
        Dim n As Long      ' --- Exponent
        Dim str As String = "0"
        Dim sign As String

        x = doubleValue
        If x <> 0 Then

            If x < 0 Then
                ' --- x *must* be positive for log function to work
                x = x * -1
                sign = "-"    ' --- we need to preserve the sign for output string
            End If

            n = 3 * CLng((System.Math.Log10(x) / System.Math.Log10(1000)))   ' --- calculate Exponent...
            '     (Converts: log-base-e to log-base-10)
            y = x / (10 ^ n)                     ' --- calculate Mantissa.

            If y < 1 Then                        ' --- if Mantissa <1 then...
                n = n - 3                        ' --- ...adjust Exponent and...
                y = x / (10 ^ n)                 ' --- ...recalculate Mantissa.
            End If
            ' --- Create output string (special treatment when Exponent of zero; don't append "e")
            str = sign & y & CStr(If(n <> 0, "e" & CStr(If(n > 0, "+", "")) & n, ""))

        End If

        Return str


    End Function

    Private Shared Function metricPrefix(num As String) As String
        Dim can() As String
        If InStr(num, "e") > 0 Then
            can = Split(num, "e")
            Select Case can(1)
                Case "+9"
                    can(1) = "G"
                Case "+6"
                    can(1) = "M"
                Case "+3"
                    can(1) = "k"
                Case "-3"
                    can(1) = "m"
                Case "-6"
                    can(1) = ChrW(&HB5)'TODO use/verify UTF-8
                Case "-9"
                    can(1) = "n"
                Case "-12"
                    can(1) = "p"
            End Select
        Else
            ReDim can(1)
            can(1) = ""
            can(0) = num
        End If
        Return can(1)
    End Function


    Private Shared Function fix(mantissa As String) As String
        Dim temp() As String = Split(mantissa, ".")
        If Len(mantissa) > 6 Then
            Select Case Len(temp(0))
                Case 1
                    temp(1) = Left(temp(1), 4)
                Case 2
                    temp(1) = Left(temp(1), 3)
                Case 3
                    temp(1) = Left(temp(1), 2)
                Case Else

            End Select
        End If
        Return $"{temp(0)}.{temp(1)}"
    End Function

    ''' <summary>
    ''' Converts a value to Engineering notation and determines the metric prefix for the SI unit'
    ''' </summary>
    ''' <param name="value">numerical value</param>
    ''' <param name="SIUnit">SI Unit symbol</param>
    ''' <returns></returns>
    Public Shared Function EngineeringNotationMetricUnit(value As Double, SIUnit As String) As String() 'returns number in engineering format with base unit
        Dim _EngineeringNotationMetricUnit(3) As String
        _EngineeringNotationMetricUnit(0) = CEngNotation(value)
        _EngineeringNotationMetricUnit(1) = metricPrefix(_EngineeringNotationMetricUnit(0))
        _EngineeringNotationMetricUnit(2) = SIUnit
        _EngineeringNotationMetricUnit(3) = fix(_EngineeringNotationMetricUnit(0)) & _EngineeringNotationMetricUnit(1) & _EngineeringNotationMetricUnit(2) 'TODO
        Return _EngineeringNotationMetricUnit
    End Function

End Class



