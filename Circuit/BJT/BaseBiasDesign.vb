Imports Electronics.BJT.NPN
Imports Electronics.Components.Resistor

Public Class BaseBiasDesign

    Private _IC As Decimal
    Public Property IC() As Decimal
        Get
            Return _IC
        End Get
        Set(ByVal value As Decimal)
            _IC = value
        End Set
    End Property

    Private _RC As Decimal
    Public Property RC() As Decimal
        Get
            Return _RC
        End Get
        Set(ByVal value As Decimal)
            _RC = value
        End Set
    End Property

    Private _VRC As Decimal
    Public Property VRC() As Decimal
        Get
            Return _VRC
        End Get
        Set(ByVal value As Decimal)
            _VRC = value
        End Set
    End Property

    Private _IB As Decimal
    Public Property IB() As Decimal
        Get
            Return _IB
        End Get
        Set(ByVal value As Decimal)
            _IB = value
        End Set
    End Property

    Private _IE As Decimal
    Public Property IE() As Decimal
        Get
            Return _IE
        End Get
        Set(ByVal value As Decimal)
            _IE = value
        End Set
    End Property


    Public VCC%, VCE@, VRE@, Beta%
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


    Function SetIB() As Decimal
        Return Me.IC / Me.Beta
    End Function

    Function SetIE() As Decimal
        Return Me.IB * (Me.Beta + 1)
    End Function

    Function SetRC() As Decimal
        Dim _RC@ = CommonValue(Me.VRC / Me.IC)
        Return _RC
    End Function

    Function SetRE() As Decimal
        Dim _RE = CommonValue(Me.VRE / Me.Beta)
        Return _RE
    End Function

    Function SetVRB() As Decimal
        Return Me.VCC - Me.VBE - Me.VRE
    End Function

    'Function SetRB() As Decimal
    '    Dim _RB = CommonValue(Me.VRB / Me.IB)
    '    Return _RB
    'End Function

    'Sub Design()
    '    Me.SetRC()
    '    Me.SetRE()
    '    Me.SetRB()
    '    Dim verify As New BaseBias(Me.VCC, Me.RB(), Me.RC, Me.RE, Me.Beta)
    'End Sub

End Class
