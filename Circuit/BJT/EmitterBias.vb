Namespace BJT
    Namespace NPN

        Public Class EmitterBias
            Public VCC%, VEE%, RB%, RC%, RE%, Beta%
            Public RE1%, RE2%
            'Public RBootstrap% 'Bootstrap resister
            Const VBE = 0.7@

            Public Sub New(Optional ByVal VCC% = 12%,
                   Optional ByVal VEE% = 12%,
                   Optional ByVal RB% = 330000%,
                   Optional ByVal RC% = 2200%,
                   Optional ByVal RE1% = 680%,
                   Optional ByVal RE2% = 0%,
                   Optional ByVal Beta% = 200%)

                Me.VCC = VCC
                Me.VEE = VEE
                Me.RB = RB
                Me.RC = RC
                Me.RE1 = RE1
                Me.RE2 = RE2
                Me.Beta = Beta

            End Sub

            Function IB() As Decimal
                Return (Me.VEE - VBE) / (Me.RB + ((Me.Beta + 1) * (Me.RE1 + Me.RE2)))
            End Function

            Function IC() As Decimal
                Return Me.IB * Me.Beta
            End Function

            Function IE() As Decimal
                Return Me.IB * (Me.Beta + 1)
            End Function

            Function VRB() As Decimal
                Return Me.IB * Me.RB
            End Function

            Function VRC() As Decimal
                Return Me.IC * Me.RC
            End Function

            Function VRE1() As Decimal
                Return Me.IE * Me.RE1
            End Function

            Function VRE2() As Decimal
                Return Me.IE * Me.RE2
            End Function

            Function VCE() As Decimal
                Return Me.VCC + Me.VEE - Me.VRC - Me.VRE1 - Me.VRE2
            End Function

            Function ICSat() As Decimal
                Return (Me.VCC + Me.VEE) / (Me.RC + Me.RE1 + Me.RE2)
            End Function

            Function VCECutoff() As Decimal
                Return Me.VCC + Me.VEE
            End Function

        End Class

    End Namespace
End Namespace
