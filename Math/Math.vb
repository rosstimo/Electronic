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

    Public Function vdB(result@, Optional reference@ = 0.001@) As Decimal
        Return CDec(20 * System.Math.Log10(reference / result))
    End Function

    Public Function idB(result@, reference@) As Decimal
        Return CDec(20 * System.Math.Log10(reference / result))
    End Function

    Public Function zdB(result@, reference@) As Decimal
        Return CDec(20 * System.Math.Log10(reference / result))
    End Function

    Public Function pdB(result@, Optional reference@ = 0.001@) As Decimal
        Return CDec(10 * System.Math.Log10(reference / result))
    End Function

    ''' <summary>
    ''' Extracts the mantissa and exponent values from a given value expressed 
    ''' in engineering notation. Engineering notation rules state that the 
    ''' mantissa must be a value from 1-999 and the power of 10 exponent must be 0 or a 
    ''' multiple of 3.
    ''' </summary>
    ''' <param name="value@"></param>
    ''' <returns>Returns a tuple containing the mantissa and the power of 10 exponent</returns>
    Public Shared Function EngineeringNotationValues(value@) As (mantissa@, exponent%)
        'A 0 value means nothing to do
        If value <> 0 Then
            'Use built in function to get scientific notation string do most of the work
            Dim _val() As String = Split(value.ToString("e"), "e")
            Dim mantissa@ = CDec(_val(0))
            Dim exponent% = CInt(_val(1))
            Dim result = (mantissa, exponent)
            'Shift decimal point and decrement exponent until a multiple of 3
            Do While result.exponent Mod 3 <> 0
                result.exponent -= 1
                result.mantissa *= 10
            Loop
            Return result
        Else
            Return (0, 0)
        End If
    End Function

    ''' <summary>
    ''' returns the metric prefix for a given engineering notation exponent. 
    ''' returns e[exponent] if metric unit not determined.
    ''' </summary>
    ''' <param name="exponent"></param>
    ''' <returns></returns>
    Private Shared Function metricPrefix(exponent As Integer) As String
        Select Case exponent
            Case 9
                Return "G"
            Case 6
                Return "M"
            Case 3
                Return "k"
            Case 0
                Return ""
            Case -3
                Return "m"
            Case -6
                Return ChrW(&HB5)'TODO use/verify UTF-8 0x00b5
            Case -9
                Return "n"
            Case -12
                Return "p"
            Case Else
                Return $"e{exponent}"
        End Select
    End Function

    ''' <summary>
    ''' Converts a value to Engineering Notation and determines the metric prefix for the SI unit'
    ''' </summary>
    ''' <param name="value">numerical value</param>
    ''' <param name="SIUnit">SI Unit symbol</param>
    ''' <returns></returns>
    Public Shared Function EngineeringNotationMetricUnit(value As Decimal, SIUnit As String) As String() 'returns number in engineering format with base unit
        Dim eng(3) As String
        Dim _values = EngineeringNotationValues(value)
        eng(0) = CStr(_values.mantissa)
        eng(1) = metricPrefix(_values.exponent)
        eng(2) = SIUnit
        eng(3) = $"{_values.mantissa.ToString("#.###")}{ eng(1)}{SIUnit}"
        Return eng
    End Function

    ''' <summary>
    ''' Converts a value to engineering notation with metric prefix for the SI unit'
    ''' </summary>
    ''' <param name="value">numerical value</param>
    ''' <param name="SIUnit">SI Unit symbol</param>
    ''' <returns>returns Engineering formatted string</returns>
    Public Shared Function Pretty(value As Decimal, SIUnit As String) As String
        Return EngineeringNotationMetricUnit(value, SIUnit)(3)
    End Function


End Class



