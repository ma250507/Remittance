VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "TrxClass"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Attribute VB_Ext_KEY = "SavedWithClassBuilder6" ,"Yes"
Attribute VB_Ext_KEY = "Top_Level" ,"Yes"
Option Explicit
Public TransactionCode        As String
Public DepositorMobile        As String
Public BeneficiaryMobile      As String
Public Amount                 As String
Public CurrencyCode           As String
Public isTeller               As Boolean
Public DepositStatus          As String
Public withdrawalStatus       As String
Public CountryCode            As String
Public BankCode               As String
Public ATMId                  As String
Public ACTION                 As String
Public actiondatetime         As String
Public ActionDate             As String
Public ActionTime             As String
Public ATMTrxSequence         As String
Public DispensedAmount        As String
Public CommissionAmount       As String
Public Dispensedcurrencycode  As String
Public Dispensedrate          As String
Public UpdateFlagQueryString  As String
Public Function HostRequestData() As String
     Dim ttt As String
     Dim xdate As String
     Dim xAmount As String
     Dim xCurrency As String
     Dim xCommission As String
     Dim xResponse As String
     Dim xTrxType As String
     ttt = ""
     xCommission = ""
     xAmount = ""
     xCurrency = ""
     xTrxType = ""
     ttt = ttt & Left(Me.DepositorMobile & Space(19), 19)
     ttt = ttt & Left(Me.BeneficiaryMobile & Space(19), 19)
     ttt = ttt & Left(Me.TransactionCode & Space(12), 12)
     If Me.ACTION = "11" Then
        xAmount = Me.Amount
        xCommission = Me.CommissionAmount
        xCurrency = Me.CurrencyCode
        xTrxType = "TD"
       ElseIf Me.ACTION = "12" Then
         xAmount = Me.DispensedAmount
         xCommission = Space(15)
         xCurrency = Me.Dispensedcurrencycode
         xTrxType = "TW"
       ElseIf Me.ACTION = "17" Then
         xAmount = Me.DispensedAmount
         xCommission = Space(15)
         xCurrency = Me.Dispensedcurrencycode
         xTrxType = "TR"
     End If
     
      ttt = ttt & Left(xAmount & Space(15), 15)
      ttt = ttt & Left(xCommission & Space(15), 15)
     
     xdate = Mid(Me.ActionDate, 7, 2) & Mid(Me.ActionDate, 5, 2) & Mid(Me.ActionDate, 1, 4)
     ttt = ttt & Left(xdate & Space(8), 8)
     If Me.isTeller = True Then
        ttt = ttt & "TLR"
     Else
        ttt = ttt & "ATM"
     End If
     ttt = ttt & Left(Me.ActionTime & Space(8), 8)
     ttt = ttt & Right("0000" & Me.ATMId, 4)
     ttt = ttt & Left(xCurrency & Space(3), 3)
     xResponse = Space(2)
     ttt = ttt & xResponse
     ttt = ttt & xTrxType
        
     HostRequestData = ttt
     
End Function



Public Function SetTrxHostFalg(Flage As Integer, HostResponse As String) As Integer

Dim updateStr As String
Dim Cn As ADODB.Connection
Dim trx As TrxClass
Dim affectedrows  As Integer

On Error GoTo errh
Set Cn = New ADODB.Connection
Cn.ConnectionString = "dsn=MoneyFer;uid=SMSUser;PWD=SMSUser"
Cn.Open Cn.ConnectionString

If Me.ACTION = "11" Then
    updateStr = " update transactions set  DepositHostFlag=" & Flage & ", HostDResponse='" & HostResponse & "' ,HostDUpdateTime=getdate()  where transactioncode='" & Me.TransactionCode & "'"
ElseIf Me.ACTION = "12" Or Me.ACTION = "17" Then
    updateStr = " update transactions set withdrawalHostFlag=" & Flage & ", HostWResponse='" & HostResponse & "' ,HostWUpdateTime=getdate() where transactioncode='" & Me.TransactionCode & "'"
End If

Call Cn.Execute(updateStr, affectedrows)
If affectedrows <> 1 Then
   Call DB_log("SetTrxHostFlag,trxcode=" & Me.TransactionCode & " rows affected =[" & affectedrows & "]", False)
   

End If

SetTrxHostFalg = 0
Exit Function
errh:

SetTrxHostFalg = 9
Call DB_log("SetTrxHostFlag,trxcode=" & Me.TransactionCode & " err:" & Err.Description, False)




End Function
