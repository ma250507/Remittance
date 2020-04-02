Attribute VB_Name = "share"
Option Explicit
Public Const LogPath = "c:\"
Public InstanceId As Integer
 
Public ConnectionSuccess As Integer
Public ATMId As String

Public hc As New HostClient

Public Declare Function FindWindow Lib "user32" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As Long
Public Declare Function SetForegroundWindow Lib "user32" (ByVal hwnd As Long) As Long
Public Declare Function SetActiveWindow Lib "user32" (ByVal hwnd As Long) As Long
Public Declare Function DestroyWindow Lib "user32" (ByVal hwnd As Long) As Long

Public Const WM_CLOSE = &H10
Public Const INFINITE = &HFFFF      '  Infinite timeout

Public Type SYSTEMTIME
        wYear As Integer
        wMonth As Integer
        wDayOfWeek As Integer
        wDay As Integer
        wHour As Integer
        wMinute As Integer
        wSecond As Integer
        wMilliseconds As Integer
End Type
Public Declare Sub GetSystemTime Lib "kernel32" (lpSystemTime As SYSTEMTIME)

Public Declare Function IsWindow Lib "user32" (ByVal hwnd As Long) As Long
Public Declare Function PostMessage Lib "user32" Alias "PostMessageA" (ByVal hwnd As Long, ByVal wMsg As Long, ByVal wParam As Long, ByVal lParam As Long) As Long
Public Declare Function WaitForSingleObject Lib "kernel32" (ByVal hHandle As Long, ByVal dwMilliseconds As Long) As Long
Public Declare Function GetParent Lib "user32" (ByVal hwnd As Long) As Long
Public Declare Function GetWindowText Lib "user32" Alias "GetWindowTextA" (ByVal hwnd As Long, ByVal lpString As String, ByVal cch As Long) As Long
Public Declare Function GetWindowTextLength Lib "user32" Alias "GetWindowTextLengthA" (ByVal hwnd As Long) As Long
 
 
 
 
Sub Main()
Dim cmdData As String
Dim i As Integer
Dim tmpstr As String
Dim j As Integer
Dim k As Integer
Dim sett As New SettingsClass
Dim Apppath As String





If Right(App.Path, 1) = "\" Then

   Apppath = App.Path
Else
   Apppath = App.Path & "\"

End If







cmdData = Command()

ATMId = Left(cmdData, 3)

'''''If App.PrevInstance = True Then
'''''   hc.hlog "Another instance is there, will end Atmid=[" & ATMId & "] Exe name=" & App.EXEName
'''''   End
'''''End If
'init hc
sett.ReadSettings (Apppath & "HostSettings.ini")
 
frmDisplay.Caption = "NCR Client Access"
hc.HostIP = sett.HostIP ' "194.0.0.100"
hc.PWDEnabled = sett.PWDEnabled  ' True
hc.PWDWord = sett.PWDWord ' "Password"
hc.LoginWord = sett.LoginWord ' "User"
hc.LoginId = sett.LoginId ' "ATMNCR"
hc.LoginPWD = sett.LoginPWD ' "ATMNCR"
hc.NeedInitialCROrLf = sett.NeedInitialCROrLf ' False
hc.NeedAfterLoginCR = sett.NeedAfterLoginCR ' True
hc.NoOfAfterLoginCR = sett.NoOfAfterLoginCR ' 1
hc.CursorTopPosCol = sett.CursorTopPosCol ' 53
hc.CursorTopPosRow = sett.CursorTopPosRow ' 8
hc.HostAppIdentifierLen = sett.HostAppIdentifierLen ' 28
hc.HostAppIdenifirRow = sett.HostAppIdenifirRow ' 1
hc.HostAppIdentifierCol = sett.HostAppIdentifierCol ' 27
hc.InitialChr = sett.InitialChr ' 13
hc.ReceiveSendInitialChr = sett.ReceiveSendInitialChr ' " "
hc.RecieveChr = sett.RecieveChr ' "@"
hc.SendChr = sett.SendChr ' "$"
hc.ShowHostScreen = sett.ShowHostScreen ' True
hc.HostAppIdentifier = sett.HostAppIdentifier ' "BNA CASH DEPOSIT APPLICATION"
hc.AfterLoginIdChar = sett.AfterLoginIdChar ' 9 'tab
hc.ApplicationExitCharTimes = sett.ApplicationExitCharTimes ' 2
hc.ApplicationExitChar = sett.ApplicationExitChar ' 13
hc.ResponseDataCol = sett.ResponseDataCol ' 25
hc.ResponseDataRow = sett.ResponseDataRow ' 20
hc.ResponseDataLen = sett.ResponseDataLen ' 2
hc.WaitForhosRsponsePeriod = sett.WaitForhosRsponsePeriod ' 0.5
hc.HostTimeOut = sett.HostTimeOut ' 5
hc.LogPath = sett.LogPath ' "c:\BNA_Log\"




hc.hlog "Start with ATMid=" & ATMId

frmDisplay.Show
DoEvents
frmDisplay.Timer2.Interval = 1000
frmDisplay.Timer2.Enabled = True

End Sub














Public Sub DB_log(data As String, sep1 As Boolean)
Dim Apppath As String
Dim fileSize As Long
Dim fileNo As Long
Dim FileName As String
Dim sepline As String

On Error GoTo errh
Apppath = LogPath
If Right(Apppath, 1) <> "\" Then Apppath = Apppath & "\"
FileName = Apppath & "db_log"
If Dir(FileName, vbDirectory) = "" Then MkDir FileName
FileName = FileName & "\NCR_db.log"
If Dir(FileName) <> "" Then
  fileSize = FileLen(FileName)
Else
   fileSize = 0
End If
   fileNo = FreeFile
   If fileSize >= 10000000 Then
       Open FileName For Output As #fileNo
   Else
        Open FileName For Append As #fileNo
   End If
  If sep1 = True Then
   sepline = "****************** " & Format(Now, "dd/mm/yyyy hh:mm:ss") & " **************"
   Print #fileNo, sepline
  End If
  
  Print #fileNo, data
  
  
   Close #fileNo
Exit Sub
errh:
     
'do nothing

End Sub



Public Function GetNoneFlagedTransaction(pTrxs As TrxCollection) As Integer
Dim trxcode As String
Dim Cn As ADODB.Connection
Dim rs As ADODB.Recordset
Dim trx As TrxClass
Dim recordpf As Boolean

On Error GoTo errh
Set Cn = New ADODB.Connection
Cn.ConnectionString = "dsn=MoneyFer;uid=SMSUser;PWD=SMSUser"
Cn.Open Cn.ConnectionString
Set rs = New ADODB.Recordset
rs.Open " select * from hostupdateview order by transactioncode, actiondatetime  ", Cn

While Not rs.EOF
     Set trx = Nothing
     Set trx = New TrxClass
     If Not IsNull(rs("TransactionCode")) Then
       On Error GoTo nexttrx
        recordpf = False
        trxcode = ""
        trx.TransactionCode = rs("TransactionCode")
        trxcode = trx.TransactionCode
        trx.DepositorMobile = rs("DepositorMobile")
        trx.BeneficiaryMobile = rs("BeneficiaryMobile")
        trx.Amount = rs("Amount")
        trx.CurrencyCode = "818" 'rs("CurrencyCode")
        trx.isTeller = rs("isTeller")
        trx.DepositStatus = rs("DepositStatus")
        If Not IsNull(rs("withdrawalStatus")) Then
           trx.withdrawalStatus = rs("withdrawalStatus")
        Else
           trx.withdrawalStatus = ""
        End If
        trx.CountryCode = rs("CountryCode")
        trx.BankCode = rs("BankCode")
        trx.ATMId = rs("ATMId")
        trx.ACTION = rs("ACTION")
        trx.actiondatetime = rs("actiondatetime")
        trx.ActionDate = rs("ActionDate")
        trx.ActionTime = rs("ActionTime")
        trx.ATMTrxSequence = rs("ATMTrxSequence")
        trx.DispensedAmount = rs("DispensedAmount")
        trx.CommissionAmount = rs("CommissionAmount")
        trx.Dispensedcurrencycode = "818" '  rs("Dispensedcurrencycode")
        trx.Dispensedrate = rs("Dispensedrate")

        If trx.ACTION = "11" Then
           trx.UpdateFlagQueryString = " DepositHostFlag=1 "
        ElseIf trx.ACTION = "12" Or trx.ACTION = "17" Then
           trx.UpdateFlagQueryString = " withdrawalHostFlag=1 "
        Else
        trx.UpdateFlagQueryString = ""
        End If
       pTrxs.Add trx, trx.TransactionCode & trx.ACTION & trx.actiondatetime
     Else
      
     End If
      recordpf = True
   
nexttrx:
     If recordpf = False Then
         Call DB_log("Current Record Error TRXCode=[" & trxcode & "] err:" & Err.Description, False)
         
     End If
     rs.MoveNext
    DoEvents
Wend

  rs.Close
  Cn.Close
GetNoneFlagedTransaction = 0
      
Exit Function
errh:

      DB_log "GetNoneFlagedTransaction, Error:" & Err.Description, False
      GetNoneFlagedTransaction = 9
      ''Cn.Close

End Function
