Public Class Components

    Public Class Resistor

        Private _value As Decimal
        Public Property value As Integer
            Get
                Return _value
            End Get
            Set(ByVal value As Integer)
                _value = value
            End Set
        End Property

        Private _powerRating As Decimal
        Public Property powerRating As Integer
            Get
                Return _powerRating
            End Get
            Set(ByVal powerRating As Integer)
                _powerRating = powerRating
            End Set
        End Property

        Private Shared commonValues() As Single = {1.0, 1.1, 1.2, 1.3, 1.5, 1.6, 1.8, 2.0, 2.2, 2.4, 2.7, 3.0, 3.3, 3.6, 3.9, 4.3, 4.7, 5.1, 5.6, 6.2, 6.8, 7.5, 8.2, 9.1}
        Private Shared multipliers() As Single = {0.01, 0.1, 0, 1, 2, 3, 4, 5}

        'Sub New(Optional ByVal value% = -1%, Optional ByVal powerRating! = 0.25!)
        '    If value < 0 Then
        '        Me.value = Me.randomValue
        '    End If
        '    Me.powerRating = powerRating
        'End Sub

        Public Shared Function CommonValue(value@) As Integer
            Dim _commonValue As Integer = 0

            For i = UBound(multipliers) To 0 Step -1
                If 10 ^ multipliers(i) <= value Then
                    For j = UBound(commonValues) To 0 Step -1
                        'Console.WriteLine(commonValues(j) * 10 ^ multipliers(i))
                        If commonValues(j) * 10 ^ multipliers(i) <= value Then
                            _commonValue = commonValues(j) * 10 ^ multipliers(i)
                            Exit For
                        End If
                    Next
                    Exit For
                End If
            Next

            Return _commonValue
        End Function

        Private Function randomValue() As Integer
            Dim digits% = commonValues(Electronics.Math.RandomNumberInRange(UBound(commonValues)))
            Dim multiplier! = multipliers(Electronics.Math.RandomNumberInRange(UBound(multipliers)))
            Return digits * 10 ^ multiplier
        End Function

        Public Shared Function valueFormatted() As String
            Return ""
        End Function

        Public Overloads Shared Function Parallel(branchOne@, branchTwo@) As Decimal
            Return CDec(branchOne ^ -1 + branchTwo ^ -1) ^ -1
        End Function

        Public Overloads Shared Function Parallel(branches() As Decimal) As Decimal
            Dim result@ = branches(0)
            Try
                For i = 1 To UBound(branches)
                    result = CDec(result ^ -1 + branches(i) ^ -1) ^ -1
                Next
            Catch
                'most likely a singe element array
                'will simply return the value of branches(0)
            End Try
            Return result
        End Function

    End Class

    Public Class ZenerDiode
        Public Shared Sub junkx()
            'Dim zeners As String() = System.IO.File.ReadAllLines("C:\Users\rosstimo\OneDrive\Sync\github\AMP\eleclib\Resources\zener.csv") 'System.IO.File.ReadAllLines(My.Resources.Zener)

            'For Each str As String In SortQuery(zeners, 2)
            'Console.WriteLine(str.PadRight(6))
            'Next

        End Sub

        Shared Function SortQuery(
        ByVal source As IEnumerable(Of String),
        ByVal num As Integer) As IEnumerable(Of String)

            Dim scoreQuery = From line In source
                             Let fields = line.Split(New Char() {","})
                             Order By fields(num) Descending
                             Select line

            Return scoreQuery
        End Function

        Public Shared Function count() As Integer

            Return My.Resources.Zener.Length
        End Function

        Public Shared Sub junk()
            Using MyReader As New Microsoft.VisualBasic.
                      FileIO.TextFieldParser(
                        "C:\Users\rosstimo\OneDrive\Sync\github\AMP\Electronic\Resources\zener.csv")
                MyReader.TextFieldType = FileIO.FieldType.Delimited
                MyReader.SetDelimiters(",")
                Dim currentRow As String()
                While Not MyReader.EndOfData
                    Try
                        currentRow = MyReader.ReadFields()
                        Dim currentField As String
                        For Each currentField In currentRow
                            Console.Write(currentField.PadRight(10))
                        Next
                        Console.WriteLine()
                    Catch ex As Microsoft.VisualBasic.
                    FileIO.MalformedLineException
                        Console.WriteLine("Line " & ex.Message &
            "is not valid and will be skipped.")
                    End Try
                End While
            End Using

        End Sub

    End Class

    Public Class Capacitor
        Public value@, tolerance@
        Public Shared Function F(R@, C@) As Decimal
            Return CDec((2 * System.Math.PI * R * C) ^ -1)
        End Function

        Public Shared Function C(Xc@, f@) As Decimal
            Return (2 * System.Math.PI * Xc * f) ^ -1
        End Function

        Public Function Xc(f@, C@) As Decimal
            Return CDec((2 * System.Math.PI * C * f) ^ -1)
        End Function
    End Class



End Class
