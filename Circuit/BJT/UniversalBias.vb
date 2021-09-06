Namespace BJT
    Namespace NPN

        Public Class UniversalBias
            Public VCC%, R1%, R2%, RC%, RE%, Beta%
            Const VBE = 0.7@

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

            ''' <summary>
            '''    | a Term     | b Term                | k Term
            '''----------------------------------------------------
            ''' 1  |IR1(R1+R2)  | - IB(R2) =            | VCC
            ''' 2  |IR1(R1)     | + IB(RB+RE(beta+1)) = | VCC - VBE
            ''' </summary>
            Function IB() As Decimal

                Dim a1@, b1@, k1@, a2@, b2@, k2@
                Dim a@, b@
                Dim results = (a, b)
                a1 = Me.R1 + Me.R2
                b1 = Me.R2 * -1
                k1 = Me.VCC
                a2 = Me.R1
                b2 = Me.RE * (Me.Beta + 1)
                k2 = Me.VCC - Me.VBE

                results = Electronics.Math.SolveSimultaneousEquation(a1@, b1@, k1@, a2@, b2@, k2@)

                Return results.b
            End Function

            Function IC() As Decimal
                Return Me.IB * Me.Beta
            End Function

            Function IE() As Decimal
                Return Me.IB * (Me.Beta + 1)
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
                Return Me.VCC - Me.VR2
            End Function

            Function VCE() As Decimal
                Return Me.VCC - Me.VRC - Me.VRE
            End Function

            Function IR1() As Decimal
                Return Me.VR1 / Me.R1
            End Function

            Function IR2() As Decimal
                Return Me.VR2 / Me.R2
            End Function

            Function ICSat() As Decimal
                Return (Me.VCC) / (Me.RC + Me.RE)
            End Function

            Function VCECutoff() As Decimal
                Return Me.VCC
            End Function

        End Class

    End Namespace
End Namespace
