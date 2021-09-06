Public Class Clampers

    Private _vgen As Decimal
    Public Property vgen() As Decimal
        Get
            Return _vgen
        End Get
        Set(ByVal value As Decimal)
            _vgen = value
        End Set
    End Property

    Private _vC As Decimal
    Public Property vC() As Decimal
        Get
            Return _vC
        End Get
        Set(ByVal value As Decimal)
            _vC = value
        End Set
    End Property

    Private _vD As Decimal
    Public Property vD() As Decimal
        Get
            Return _vD
        End Get
        Set(ByVal value As Decimal)
            _vD = value
        End Set
    End Property

    Const VFB As Decimal = 0.7D




    Sub Answer(value As Decimal, feedback As String)
        Dim cloze$ = "{1:NUMERICAL:="
        cloze &= $"{value}:{value * 0.005} " & "}"

    End Sub


End Class
