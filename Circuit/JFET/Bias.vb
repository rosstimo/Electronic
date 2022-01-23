Option Explicit On
Option Strict On
Imports System
Namespace JFET
    Namespace NChannel

        Public Class SelfBias
            Public RG%, RD%, RS%, RS1%, RS2%, VDD%, IDSS@, VGSOff@
            Public IG% = 0
            Public CommonSource As New CommonSource
            Public CommonDrain As New CommonDrain
            Public CommonGate As New CommonGate

            Sub Analize()
                getCommonSource()
                getCommonDrain()
                getCommonDrain()
            End Sub

            '''<summary>
            '''ID = IDSS*(1-(VGS/VGSOff))^2
            '''</summary>
            Function ID() As Decimal
                Dim _ID@, a@, b@, c@
                'a = 500
                'b = -5
                'c =0.008

                a = CDec((Me.IDSS * Me.RS ^ 2) / Me.VGSOff ^ 2)
                'b = ((2 * Me.IDSS * Me.RS) / Me.VGSOff) - 1
                'c = Me.IDSS
                b = ((2 * Me.IDSS * Me.RS * (1 - Me.VG)) / Me.VGSOff) - 1
                c = Me.IDSS - ((Me.IDSS * Me.VG * (2 + Me.VG)) / Me.VGSOff)

                'quadratic TODO add to math library return tuple or something
                _ID = CDec(-b - System.Math.Sqrt(b ^ 2 - (4 * a * c))) / (2 * a)

                'Console.WriteLine((_ID ^ 2 * a) + (_ID * b) + c)

                Return _ID
            End Function

            ''' <summary>
            ''' VRG=IG*RG
            ''' </summary>
            Function VRG() As Decimal
                Return Me.IG * Me.RG
            End Function

            Function VRS() As Decimal
                Return Me.ID * Me.RS
            End Function

            Function VRD() As Decimal
                Return Me.ID * Me.RD
            End Function

            ''' <summary>
            ''' 0=-VRG+VGS+VRS
            ''' VGS=VRG-VRS
            ''' </summary>
            Function VGS() As Decimal
                Return Me.VRG - Me.VRS
            End Function

            Function VDS() As Decimal
                Return Me.VDD - Me.VRD - Me.VRS
            End Function

            Function VG() As Decimal
                Return Me.VRG
            End Function

            Function VD() As Decimal
                Return Me.VDS + Me.VRS
            End Function

            Function VS() As Decimal
                Return Me.VRS
            End Function

            Function IDSat() As Double
                Return Me.VDD / (Me.RD + Me.RS)
            End Function

            'Common Source Amplifier
            Sub getCommonSource()
                If CommonSource.RL <= 0 Then
                    CommonSource.RL = 10000000
                End If
                Me.CommonSource.rSwamp = Me.RS1
                Me.CommonSource.rPrimeS = CDec((-1 * Me.VGSOff) / (2 * System.Math.Sqrt(Me.IDSS * Me.ID)))
                Me.CommonSource.zin = Me.RG
                Me.CommonSource.zout = Me.RD
                Me.CommonSource.rLAC = CDec((Me.RD ^ -1 + Me.CommonSource.RL ^ -1) ^ -1)
                Me.CommonSource.vinMax = Me.ID * (Me.CommonSource.rPrimeS + Me.CommonSource.rSwamp)
                Me.CommonSource.voutMax = Me.ID * Me.CommonSource.rLAC
                Me.CommonSource.Av = (Me.CommonSource.voutMax / Me.CommonSource.vinMax) * -1
                Me.CommonSource.Ai = Me.CommonSource.Av * (Me.CommonSource.zin / Me.CommonSource.RL)
                Me.CommonSource.Ap = Me.CommonSource.Av * Me.CommonSource.Ai
                'Me.CommonSource.idSatAC = CDec(Me.VDS / (Me.RD ^ -1 + Me.CommonSource.RL ^ -1) ^ -1) + Me.ID
                Me.CommonSource.idSatAC = CDec(Me.VDS / Me.CommonSource.rLAC) + Me.ID
                Me.CommonSource.vdsCutAC = Me.CommonSource.voutMax + Me.VDS
            End Sub

            'Common Drain Amplifier
            Sub getCommonDrain()
                If CommonDrain.RL <= 0 Then
                    CommonDrain.RL = 10000000
                End If
                Me.CommonDrain.rSwamp = Me.RS1
                Me.CommonDrain.rPrimeS = CDec((-1 * Me.VGSOff) / (2 * System.Math.Sqrt(Me.IDSS * Me.ID)))
                Me.CommonDrain.zin = Me.RG
                Me.CommonDrain.zout = CDec((Me.RS ^ -1 + Me.CommonDrain.rPrimeS ^ -1) ^ -1) 'TODO verify
                Me.CommonDrain.rLAC = CDec((Me.RS ^ -1 + Me.CommonDrain.RL ^ -1) ^ -1)
                Me.CommonDrain.vinMax = Me.ID * (Me.CommonDrain.rPrimeS + Me.CommonDrain.rSwamp) 'TODO verify
                Me.CommonDrain.voutMax = Me.ID * Me.CommonDrain.rLAC 'TODO verify
                Me.CommonDrain.Av = Me.CommonDrain.voutMax / Me.CommonDrain.vinMax
                Me.CommonDrain.Ai = Me.CommonDrain.Av * (Me.CommonDrain.zin / Me.CommonDrain.RL)
                Me.CommonDrain.Ap = Me.CommonDrain.Av * Me.CommonDrain.Ai
                Me.CommonDrain.idSatAC = CDec(Me.VDS / Me.CommonDrain.rLAC) + Me.ID 'TODO verify
                Me.CommonDrain.vdsCutAC = Me.CommonDrain.voutMax + Me.VDS
            End Sub

            'Common Gate Amplifier
            Sub getCommonGate()
                If CommonGate.RL <= 0 Then
                    CommonGate.RL = 10000000
                End If
                Me.CommonGate.rSwamp = Me.RS1
                Me.CommonGate.rPrimeS = CDec((-1 * Me.VGSOff) / (2 * System.Math.Sqrt(Me.IDSS * Me.ID)))
                Me.CommonGate.zin = CDec((Me.RS ^ -1 + (Me.CommonGate.rSwamp + Me.CommonGate.rPrimeS) ^ -1) ^ -1) 'TODO verify
                Me.CommonGate.zout = Me.RD
                Me.CommonGate.rLAC = CDec((Me.RD ^ -1 + Me.CommonGate.RL ^ -1) ^ -1)
                Me.CommonGate.voutMax = Me.ID * Me.CommonGate.rLAC 'TODO verify
                Me.CommonGate.vinMax = Me.ID * CDec((Me.RS ^ -1 + Me.CommonGate.RL ^ -1) ^ -1) 'TODO verify
                Me.CommonGate.Av = Me.CommonGate.voutMax / Me.CommonGate.vinMax
                Me.CommonGate.Ai = Me.CommonGate.Av * (Me.CommonGate.zin / Me.CommonGate.RL)
                Me.CommonGate.Ap = Me.CommonGate.Av * Me.CommonGate.Ai
                Me.CommonGate.idSatAC = CDec(Me.VDS / Me.CommonGate.rLAC) + Me.ID 'TODO verify
                Me.CommonGate.vdsCutAC = Me.CommonGate.voutMax + Me.VDS
            End Sub

        End Class

        Public Class UniversalBias
            Public R1%, R2%, RG%, RD%, RS%, RS1%, RS2%, VDD%, IDSS@, VGSOff@
            Public IG% = 0
            Public CommonSource As New CommonSource
            Public CommonDrain As New CommonDrain
            Public CommonGate As New CommonGate


            Function Guess() As Decimal
                Dim min@ = 0
                Dim max@ = Me.VGSOff
                Dim currentGuess@

                Dim _IDSS@ = 0.008@
                Dim _VGSOFF@ = -4
                Dim _VGS@
                Dim _RS@ = 4700
                Dim _VG@ = 3
                Dim _ID@
                Dim _IRS@
                max = _VGSOFF
                min = 0


                Do
                    _VGS = (max - min) / 2
                    _ID = CDec(_IDSS * (1 - (_VGS / _VGSOFF)) ^ 2)
                    _IRS = (_VG - _VGS) / _RS

                    Console.WriteLine($"max: {max}, min: {min}, VGS: {_VGS}, ID: {_ID}")
                    Console.WriteLine(_IRS)
                    Console.WriteLine(_ID)
                    Console.WriteLine(System.Math.Abs(_IRS - _ID))
                    Select Case _IRS
                        Case > _ID
                            max = _VGS
                        Case < _ID
                            min = _VGS
                        Case = _ID
                            ' Console.WriteLine("yeah")
                    End Select
                    ' Loop Until (Math.Abs(_VGS - max) < 1 * 10 ^ -6 Or Math.Abs(min - _VGS) < 1 * 10 ^ -6) 'Or _IRS <> _ID
                Loop Until System.Math.Abs(_IRS - _ID) <= 100 * 10 ^ -1

                'For i = 1 To 1000
                '    Console.WriteLine($"max: {max}, min: {min}, VGS: {_VGS}, ID: {_ID} count:{i}")
                '    _VGS = (max - min) / 2

                '    _ID = CDec(_IDSS * (1 - (_VGS / _VGSOFF)) ^ 2)
                '    _IRS = (_VG - _VGS) / _RS

                '    If Math.Abs(_VGS - max) > 1 * 10 ^ -6 And Math.Abs(min - _VGS) > 1 * 10 ^ -6 Then
                '        Select Case _IRS
                '            Case > _ID
                '                max = _VGS
                '            Case < _ID
                '                min = _VGS
                '            Case = _ID
                '                ' Console.WriteLine("yeah")
                '        End Select
                '    Else
                '        Console.WriteLine(Math.Abs(_ID - _IRS))
                '        Exit For
                '    End If

                'Next

                'Do
                '    _ID = _IDSS * (1 - (_VGS / _VGSOFF)) ^ 2
                '    _VGS -= 0.1@
                '    Console.WriteLine(_ID)
                'Loop While _ID >= (_VG - _VGS) / _RS



                Return 0
            End Function

            Function IR1() As Double
                Return Me.VDD / (Me.R1 + Me.R2)
            End Function

            Function VR1() As Double
                Return Me.IR1 * Me.R1
            End Function

            Function VR2() As Double
                Return Me.IR1 * Me.R2
            End Function

            ''' <summary>
            ''' 0=-VR2+VRG+VGS+VRS
            ''' 0=-VR2+0+VGS+VRS
            ''' 0=VGS+VRS-VR2
            ''' VR2-VRS=VGS
            ''' N-channel JFET: VR2 is lower than VRS therefore VGS should be a negative number.
            ''' VR2-VGS=VRS
            ''' VR2-VGS=ID*RS
            ''' (VR2-VGS)/RS=ID
            ''' Plot ID through RS as VGS changes 0V to VGSOff
            ''' The point where this line intercepts the parabola is the Q point
            ''' </summary>
            Function VGS() As Double
                Return Me.VR2 - Me.VRS
            End Function

            '''<summary>
            '''ID = IDSS*(1-(VGS/VGSOff))^2
            '''</summary>
            Function ID() As Double
                Dim _ID#, _IDSS#, _RS#, _VGSOFF#, _VG#, a#, b#, c#
                'a = 500
                'b = -5
                'c =0.008
                _IDSS = Me.IDSS
                _VGSOFF = Me.VGSOff
                _RS = Me.RS
                _VG = Me.VG
 _
                'TODO - Need update for universal bias
                a = (_IDSS * _RS ^ 2) / _VGSOFF ^ 2
                b = ((2 * _IDSS * _RS * (1 - _VG)) / _VGSOFF) - 1
                c = _IDSS - ((_IDSS * _VG * (2 + _VG)) / _VGSOFF)

                'quadratic TODO add to math library return tuple or something
                _ID = (-b - System.Math.Sqrt(b ^ 2 - (4 * a * c))) / (2 * a)
                '_ID = 0.002
                Console.WriteLine((_ID ^ 2 * a) + (_ID * b) + c)
                Return _ID
            End Function

            ''' <summary>
            ''' VRG=IG*RG
            ''' </summary>
            Function VRG() As Double
                Return Me.IG * Me.RG
            End Function

            Function VRS() As Double
                Return Me.ID * Me.RS
            End Function

            Function VRD() As Double
                Return Me.ID * Me.RD
            End Function

            Function VDS() As Double
                Return Me.VDD - Me.VRD - Me.VRS
            End Function

            Function VG() As Double
                Return Me.VR2
            End Function

            Function VD() As Double
                Return Me.VDS + Me.VRS
            End Function

            Function VS() As Double
                Return Me.VRS
            End Function

            Function IDSat() As Double
                Return Me.VDD / (Me.RD + Me.RS)
            End Function


        End Class
        Public Class Amplifier
            Public RL%, rgen%, rSwamp%, zin@, zout@, rLAC@, Av@, Ai@, Ap@, rPrimeS@, voutMax@, vinMax@, idSatAC@, vdsCutAC@

            Public Function AvdB() As Decimal 'CE, CB, CC
                Return CDec(20 * System.Math.Log10(System.Math.Abs(Me.Av)))
            End Function
            Public Function AidB() As Decimal 'CE, CB, CC
                Return CDec(20 * System.Math.Log10(System.Math.Abs(Me.Ai)))
            End Function
            Public Function ApdB() As Decimal 'CE, CB, CC
                Return CDec(10 * System.Math.Log10(Me.Ap))
            End Function

        End Class
        Public Class CommonSource
            Inherits Amplifier
            'Public RL%, rgen%, rSwamp%, zin@, zout@, rLAC@, Av@, Ai@, Ap@, rPrimeS@, voutMax@, vinMax@, idSatAC@, vdsCutAC@
        End Class

        Public Class CommonDrain
            Inherits Amplifier
            'Public RL%, rgen%, rSwamp%, zin@, zout@, rLAC@, Av@, Ai@, Ap@, rPrimeS@, voutMax@, vinMax@, idSatAC@, vdsCutAC@
        End Class

        Public Class CommonGate
            Inherits Amplifier
            'Public RL%, rgen%, rSwamp%, zin@, zout@, rLAC@, Av@, Ai@, Ap@, rPrimeS@, voutMax@, vinMax@, idSatAC@, vdsCutAC@
        End Class


        'FcL


    End Namespace
End Namespace