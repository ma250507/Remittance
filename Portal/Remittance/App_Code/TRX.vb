Imports Microsoft.VisualBasic

Public Class TRX
    Public AtmId As String
    Public BankCode As String
    Public CountryCode As String
    Public RequestType As String
    Public ResponseCode As String
    Public AtmDate As String
    Public AtmTime As String
    Public TrxSequence As String
    Public DepositorMobile As String
    Public DepositorPin As String
    Public BeneficiaryMobile As String
    Public BeneficiaryPin As String
    Public Amount As String
    Public Currency As String
    Public HostTransactionCode As String
    Public ActionReason As String
    Public DispNotes As String
    Public DispensedAmount As String
    Public CommissionAmount As String
    Public SMSLang As Char
    Public MinimumValue As String
    Public MaximumValue As String
    Public ReceiptLine1 As String
    Public ReceiptLine2 As String
    Public ReceiptLine3 As String
    Public ID As String
    Public Rest As String
    Public TRXLength As String




    Public Function Spaces(ByVal Text As String, ByVal Length As Integer) As String
        Dim str As String
        str = Text
        For i As Integer = str.Length To Length - 1
            str = str & " "
        Next
        Return str
    End Function

    Public Overrides Function ToString() As String
        Dim lll As String
        Dim msg As String

        msg = Me.AtmId & Me.BankCode & Me.CountryCode & Me.RequestType & Me.ResponseCode & Me.AtmDate & Me.AtmTime & Me.TrxSequence _
                & Me.DepositorMobile & Me.DepositorPin & Me.BeneficiaryMobile & Me.BeneficiaryPin & Me.Amount & Me.Currency & Me.HostTransactionCode & Me.ActionReason _
                & Me.DispNotes & Me.DispensedAmount & Me.CommissionAmount & Me.SMSLang & Me.MinimumValue & Me.MaximumValue & Me.ReceiptLine1 & Me.ReceiptLine2 & Me.ReceiptLine3 & Me.ID
        lll = msg.Length.ToString("0000")
        msg = lll & msg
        Return msg
    End Function


End Class
