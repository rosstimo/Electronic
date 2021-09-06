Namespace BJT
    Namespace NPN

        Public Class BaseBias
            Public VCC%, RB%, RC%, RE%, Beta%
            Const VBE = 0.7@

            Public Sub New(Optional ByVal VCC% = 25%,
                   Optional ByVal RB% = 820000%,
                   Optional ByVal RC% = 4000%,
                   Optional ByVal RE% = 1000%,
                   Optional ByVal Beta% = 100%)

                Me.VCC = VCC
                Me.RB = RB
                Me.RC = RC
                Me.RE = RE
                Me.Beta = Beta

            End Sub

            Function IB() As Decimal
                Return (Me.VCC - VBE) / (Me.RB + ((Me.Beta + 1) * Me.RE))
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

            Function VRE() As Decimal
                Return Me.IE * Me.RE
            End Function

            Function VCE() As Decimal
                Return Me.VCC - Me.VRC - Me.VRE
            End Function

            Function ICSat() As Decimal
                Return Me.VCC / (Me.RC + Me.RE)
            End Function

            Function VCECutoff() As Decimal
                Return Me.VCC
            End Function

        End Class

    End Namespace
End Namespace