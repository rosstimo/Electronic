Public Class CommonEmitter
    Public rgen@, zin@, zout@, RL@, rLAC@, rPrimeE@,
        voutMax@, vinMax@, icSatAC@, vceCutAC@,
        Av@, Ai@, Ap@,
        rSwamp@, rEBypass@, CIn@, COut@, CBypass@,
        rThCIn@, rThCOut@, rThCBypass@,
        fCIn@, fCOut@, fCBypass@, fCLow@


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

Public Class CommonCollector
    Public RL%, rgen@, rSwamp@, zin@, zout@, rLAC@, Av@, Ai@, Ap@, rPrimeE@, voutMax@, vinMax@, icSatAC@, vceCutAC@
End Class

Public Class CommonBase
    Public rgen@, zin@, zout@, RL@, rLAC@, rPrimeE@,
        voutMax@, vinMax@, icSatAC@, vceCutAC@,
        Av@, Ai@, Ap@,
        rSwamp@, rEBypass@, CIn@, COut@, CBypass@,
        rThCIn@, rThCOut@, rThCBypass@,
        fCIn@, fCOut@, fCBypass@, fCLow@

    Public Function AvdB() As Decimal 'CE, CB, CC
        Return CDec(20 * System.Math.Log10(Me.Av))
    End Function
    Public Function AidB() As Decimal 'CE, CB, CC
        Return CDec(20 * System.Math.Log10(Me.Ai))
    End Function
    Public Function ApdB() As Decimal 'CE, CB, CC
        Return CDec(10 * System.Math.Log10(Me.Ap))
    End Function
End Class
