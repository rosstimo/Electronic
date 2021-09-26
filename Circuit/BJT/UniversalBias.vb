Imports cap = Electronics.Components.Capacitor
Imports res = Electronics.Components.Resistor
Imports eMath = Electronics.Math

Namespace BJT
    Namespace NPN

        Public Class UniversalBias
            Public VCC%, R1%, R2%, RC%, RE%, Beta%
            Public Rd% 'decoupling resistor
            Public Rb% 'bootstrap resister
            Const VBE = 0.7@

            Public CommonEmitter As New CommonEmitter
            Public CommonBase As New CommonBase

            Public Sub New(Optional ByVal VCC% = 24%,
                           Optional ByVal R1% = 120000%,
                           Optional ByVal R2% = 22000%,
                           Optional ByVal RC% = 3300%,
                           Optional ByVal RE% = 820%,
                           Optional ByVal Beta% = 200%)

                Me.VCC = VCC
                Me.R1 = R1
                Me.R2 = R2
                Me.RC = RC
                Me.RE = RE
                Me.Beta = Beta

            End Sub

            Function IB() As Decimal

                Dim a1@, b1@, k1@, a2@, b2@, k2@
                Dim a@, b@
                Dim results = (a, b)
                a1 = Me.R1 + Me.R2 + Me.Rd
                b1 = -1 * (Me.R2 - (Me.Rd * (Me.Beta)))
                k1 = Me.VCC
                a2 = Me.R1 + Me.Rd
                b2 = (Me.RE * (Me.Beta + 1)) + (Me.Rd * (Me.Beta))
                k2 = Me.VCC - Me.VBE

                results = eMath.SolveSimultaneousEquation(a1@, b1@, k1@, a2@, b2@, k2@)

                Return results.b

            End Function

            Function IC() As Decimal
                Return Me.IB * Me.Beta
            End Function

            Function IE() As Decimal
                Return Me.IB * (Me.Beta + 1)
            End Function

            Function VRd() As Decimal
                Return Me.IRd * Me.Rd
            End Function

            Function IRd() As Decimal
                Return (Me.IC + Me.IR1)
            End Function

            Function VRC() As Decimal
                Return Me.IC * Me.RC
            End Function

            Function VRE() As Decimal
                Return Me.IE * Me.RE
            End Function

            Function VR2() As Decimal
                Return Me.VBE + Me.VRE
            End Function

            Function VR1() As Decimal
                Return Me.IR1 * Me.R1
            End Function

            Function VCE() As Decimal
                Return Me.VCC - Me.VRd - Me.VRC - Me.VRE
            End Function

            Function IR1() As Decimal
                Return Me.IR2 + Me.IB
            End Function

            Function IR2() As Decimal
                Return Me.VR2 / Me.R2
            End Function

            Function ICSat() As Decimal
                Return (Me.VCC - Me.VRd) / (Me.RC + Me.RE)
            End Function

            Function VCECutoff() As Decimal
                Return Me.VCC - Me.VRd
            End Function

            Sub GetCommonEmiter()
                With Me.CommonEmitter
                    .rPrimeE = CDec(0.026 / Me.IE)
                    .zin = CDec((Me.R1 ^ -1 + Me.R2 ^ -1 + ((Me.Beta + 1) * (.rPrimeE + .rSwamp)) ^ -1) ^ -1)
                    .zout = Me.RC
                    .rLAC = CDec((.zout ^ -1 + .RL ^ -1) ^ -1)
                    .voutMax = Me.IC * (.rLAC)
                    .vinMax = Me.IE * (.rPrimeE + .rSwamp)
                    .Av = -1 * (.voutMax / .vinMax)
                    .Ai = .Av * (.zin / .RL)
                    .Ap = .Av * .Ai
                    .icSatAC = (Me.VCE / .rLAC) + Me.IC
                    .vceCutAC = Me.VCE + .voutMax
                    'frequency response
                    .rEBypass = Me.RE - .rSwamp
                    .rThCIn = .rgen + .zin
                    .rThCOut = .zout + .RL
                    .rThCBypass = res.Parallel({ .rEBypass, .rSwamp + .rPrimeE + (res.Parallel({Me.R1, Me.R2, .rgen}) / (Me.Beta + 1))})
                    .fCIn = cap.F(.rThCIn, .CIn)
                    .fCOut = cap.F(.rThCOut, .COut)
                    .fCBypass = cap.F(.rThCBypass, .CBypass)
                    .fCLow = CDec(System.Math.Sqrt(.fCIn ^ 2 + .fCOut ^ 2 + .fCBypass ^ 2))
                End With
            End Sub

            Sub GetCommonBase()
                With Me.CommonBase
                    .rPrimeE = CDec(0.026 / Me.IE)
                    .rEBypass = Me.RE - .rSwamp
                    .zin = res.Parallel(.rEBypass, (.rSwamp + .rPrimeE))
                    .zout = Me.RC
                    .rLAC = res.Parallel(.zout, .RL)
                    .voutMax = Me.IC * (.rLAC)
                    .vinMax = Me.IE * (.rPrimeE + .rSwamp)
                    .Av = .voutMax / .vinMax
                    .Ai = .Av * (.zin / .RL)
                    .Ap = .Av * .Ai
                    .icSatAC = (Me.VCE / .rLAC) + Me.IC
                    .vceCutAC = Me.VCE + .voutMax
                    'frequency response
                    .rThCIn = .rgen + .zin
                    .rThCOut = .zout + .RL
                    .rThCBypass = res.Parallel({Me.R1, Me.R2, ((.rPrimeE + .rSwamp + res.Parallel({ .rEBypass, .rgen})) * (Me.Beta + 1))})
                    .fCIn = cap.F(.rThCIn, .CIn)
                    .fCOut = cap.F(.rThCOut, .COut)
                    .fCBypass = cap.F(.rThCBypass, .CBypass)
                    .fCLow = CDec(System.Math.Sqrt(.fCIn ^ 2 + .fCOut ^ 2 + .fCBypass ^ 2))
                End With
            End Sub

        End Class
    End Namespace
End Namespace
