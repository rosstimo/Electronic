Public Class BaseBiasDesign

    Public VCC%, VRC@, VCE@, VRE@, IC@, Beta%
    Const VBE = 0.7@

    Public Sub New(
                   Optional VCC% = 24%,
                   Optional VRC@ = 0.45,
                   Optional VCE@ = 0.45,
                   Optional VRE@ = 0.1,
                   Optional IC@ = 0.001,
                   Optional Beta@ = 200%)

        Me.VCC = VCC
        Me.VRC = Me.VCC * VRC
        Me.VCE = Me.VCC * VCE
        Me.VRE = Me.VCC * VRE
        Me.IC = IC
        Me.Beta = Beta

    End Sub

    Function IB() As Decimal
        Return Me.IC / Me.Beta
    End Function

    Function IE() As Decimal
        Return Me.IB * (Me.Beta + 1)
    End Function

    Function RC() As Decimal
        Return Me.VRC / Me.IC
    End Function

    Function RE() As Decimal
        Return Me.VRE / Me.Beta
    End Function

    Function VRB() As Decimal
        Return Me.VCC - Me.VBE - Me.VRE
    End Function

    Function RB() As Decimal
        Return Me.VRB / Me.IB
    End Function

    Sub Design()
        Me.RC()
    End Sub

End Class
