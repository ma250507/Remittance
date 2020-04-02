VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "HostClient"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Attribute VB_Ext_KEY = "SavedWithClassBuilder6" ,"Yes"
Attribute VB_Ext_KEY = "Top_Level" ,"Yes"
Option Explicit
Const deltax = 50
Const deltay = 50
Const Statusheight = 300
Const WE_Period = 1

Public Enum TerminalIdConst
   wdVT52  'VT52 terminal.
   wdVT100 'VT100 terminal.
   wdVT101 'VT101 terminal.
   wdVT102 'VT102 terminal.
   wdVT125 'VT125 terminal.
   wdVT131 'VT131 terminal.
   wdVT220 ''VT220 terminal.
   wdVT240 'VT240 terminal.
   wdVT241 'VT241 terminal.
   wdVT320 'VT320 terminal.
   wdVT330 'VT330 terminal.
   wdVT340 'VT340 terminal.
   wdVT420 'VT420 terminal.
   wdTek401X '  Tek 401X terminal.
End Enum

'Property Variables:
Dim m_ClearScrenReqCode As String
Dim m_SendRecieveChrRow As Integer
Dim m_SendRecieveChrCol As Integer
Dim m_SendChr As String
Dim m_RecieveChr As String
Dim m_ReceiveSendInitialChr As String
Dim m_TerminalId As TerminalIdConst
Dim m_CursorTopPosCol As Integer
Dim m_CursorTopPosRow As Integer
Dim m_HostAppIdentifierLen As Integer
Dim m_NeedAfterLoginCR As Boolean
Dim m_NoOfAfterLoginCR As Integer
Dim m_NeedInitialCROrLf As Boolean
Dim m_InitialChr As Integer
Dim m_LoginWord As String
Dim m_PWDWord As String
Dim m_HostTimeOut As Long
Dim m_HostIsReady As Boolean
Dim m_HostIP As String
Dim m_TcpPort As Long
Dim m_LoginId As String
Dim m_LoginPWD As String
Dim m_HostAppIdentifier As String
Dim m_HostAppIdenifirRow As Integer
Dim m_HostAppIdentifierCol As Integer
Dim m_PWDEnabled As Boolean

Public LinesPerPage As Integer
Public ShowHostScreen As Boolean
Public LogPath As String

Public AfterLoginIdChar As Integer
Public ApplicationExitChar As Integer
Public ApplicationExitCharTimes As Integer
Public ResponseDataCol As Integer
Public ResponseDataRow As Integer
Public ResponseDataLen As Integer
Public WaitForhosRsponsePeriod As Single
Private ObjectXUnixDisplay1 As ObjectXUnixDisplay.ObjectXUnixDisplay
















Public Function Dissconnect() As Boolean
'Unload frmDisplay
Dissconnect = ObjectXUnixDisplay1.Disconnect
End Function
Public Property Get ReceiveSendInitialChr() As String
       ReceiveSendInitialChr = m_ReceiveSendInitialChr
End Property
Public Property Let ReceiveSendInitialChr(new_val As String)
       m_ReceiveSendInitialChr = new_val
End Property
Public Property Get TcpPort() As Long
    TcpPort = m_TcpPort
End Property

Public Property Let TcpPort(ByVal New_TcpPort As Long)
    m_TcpPort = New_TcpPort
    
End Property

Public Property Get LoginId() As String
    LoginId = m_LoginId
End Property

Public Property Let LoginId(ByVal New_LoginId As String)
    m_LoginId = New_LoginId
    
End Property

Public Property Get LoginPWD() As String
    LoginPWD = m_LoginPWD
End Property

Public Property Let LoginPWD(ByVal New_LoginPWD As String)
    m_LoginPWD = New_LoginPWD
    
End Property

Public Property Get HostAppIdentifier() As String
    HostAppIdentifier = m_HostAppIdentifier
End Property

Public Property Let HostAppIdentifier(ByVal New_HostAppIdentifier As String)
    m_HostAppIdentifier = New_HostAppIdentifier
    
End Property

Public Property Get HostAppIdenifirRow() As Integer
    HostAppIdenifirRow = m_HostAppIdenifirRow
End Property

Public Property Let HostAppIdenifirRow(ByVal New_HostAppIdenifirRow As Integer)
    m_HostAppIdenifirRow = New_HostAppIdenifirRow
    
End Property

Public Property Get HostAppIdentifierCol() As Integer
    HostAppIdentifierCol = m_HostAppIdentifierCol
End Property

Public Property Let HostAppIdentifierCol(ByVal New_HostAppIdentifierCol As Integer)
    m_HostAppIdentifierCol = New_HostAppIdentifierCol
    
End Property

Public Property Get PWDEnabled() As Boolean
    PWDEnabled = m_PWDEnabled
End Property

Public Property Let PWDEnabled(ByVal New_PWDEnabled As Boolean)
    m_PWDEnabled = New_PWDEnabled
    
End Property





'WARNING! DO NOT REMOVE OR MODIFY THE FOLLOWING COMMENTED LINES!
'MemberInfo=13,0,0,189.0.1.30
Public Property Get HostIP() As String
    HostIP = m_HostIP
End Property

Public Property Let HostIP(ByVal New_HostIP As String)
    m_HostIP = New_HostIP
  
End Property

'WARNING! DO NOT REMOVE OR MODIFY THE FOLLOWING COMMENTED LINES!
'MemberInfo=0,0,0,False
Public Property Get HostIsReady() As Boolean
    HostIsReady = m_HostIsReady
End Property

Public Property Let HostIsReady(ByVal New_HostIsReady As Boolean)
    m_HostIsReady = New_HostIsReady
   
End Property


'WARNING! DO NOT REMOVE OR MODIFY THE FOLLOWING COMMENTED LINES!
'MemberInfo=8,0,0,30
Public Property Get HostTimeOut() As Long
    HostTimeOut = m_HostTimeOut
End Property

Public Property Let HostTimeOut(ByVal New_HostTimeOut As Long)
    m_HostTimeOut = New_HostTimeOut
   
End Property

Private Sub DelayPeriod(period As Single)
Dim st As SYSTEMTIME
Dim StartTime As Double
Dim rtrn As Long
Dim thisTime As Double
Dim thisSecond As Single
Dim minuteAdjust As Single
Dim startM As Single
Dim startS As Single
Dim StartMS As Single
Dim lPeriod As Single
    lPeriod = period * 1000
     GetSystemTime st
      startM = st.wMinute
      StartMS = st.wMilliseconds
      startS = st.wSecond
     StartTime = startS * 1000
     StartTime = StartTime + st.wMilliseconds
     minuteAdjust = 0
     thisTime = StartTime
    
     While thisTime <= StartTime + lPeriod
           GetSystemTime st
           thisSecond = st.wSecond
            thisTime = thisSecond * 1000
            thisTime = thisTime + st.wMilliseconds
            If startM <> st.wMinute Then
               minuteAdjust = 60 - startS
               minuteAdjust = minuteAdjust * 1000
               minuteAdjust = minuteAdjust - StartMS
               
               StartTime = 0
               lPeriod = lPeriod - minuteAdjust
            End If
           DoEvents
     Wend
     
End Sub

'WARNING! DO NOT REMOVE OR MODIFY THE FOLLOWING COMMENTED LINES!
'MemberInfo=14
Public Function StartHostConnection() As Boolean
                
If DoLogin = True Then
   m_HostIsReady = True
   StartHostConnection = True
Else
   m_HostIsReady = False
   StartHostConnection = False
End If
                 

End Function
Public Function IsHostReady() As Integer

Dim StrsTr As String
Dim TimeStamp As Date
Dim dDiff As Long
Dim TimeOutFlag As Boolean

If ObjectXUnixDisplay1.HostConnected = False Then
    IsHostReady = 1
    m_HostIsReady = False
Exit Function
End If

If ObjectXUnixDisplay1.ReadyToSend = False Then
    IsHostReady = 2
    m_HostIsReady = False
    Exit Function
End If
If ObjectXUnixDisplay1.CursorRow <> m_CursorTopPosRow Or ObjectXUnixDisplay1.CursorColumn <> m_CursorTopPosCol Then
   IsHostReady = 3
   m_HostIsReady = False
   Exit Function
End If
        

 StrsTr = ""
 TimeStamp = Now
 TimeOutFlag = False
 ObjectXUnixDisplay1.GetScreen StrsTr, m_HostAppIdenifirRow, m_HostAppIdentifierCol, m_HostAppIdentifierLen
                    While Mid(StrsTr, 1, m_HostAppIdentifierLen) <> m_HostAppIdentifier And Not TimeOutFlag
                       dDiff = DateDiff("S", TimeStamp, Now)
                       If dDiff >= 0 Then
                           TimeOutFlag = True
                           IsHostReady = 4
                           m_HostIsReady = False
                           Exit Function
                        End If
                        ObjectXUnixDisplay1.GetScreen StrsTr, m_HostAppIdenifirRow, m_HostAppIdentifierCol, m_HostAppIdentifierLen
                        DoEvents
                    Wend
            
   
   IsHostReady = 0
   m_HostIsReady = True
End Function
Public Function GetCurrentScreenData() As String
     Dim StrsTr As String
     StrsTr = ""
     ObjectXUnixDisplay1.GetScreen StrsTr, 1, 1, 1920
     GetCurrentScreenData = StrsTr
End Function




Public Function DoLogin() As Integer
     Dim CRvar  As String
     Dim LFvar   As String
     Dim tmptmp As String
     Dim i As Integer
    ' Dim WaitForhosRsponsePeriod As Single
     Dim TimeStamp As Date
     
     Dim TimeOutFlag As Boolean
     Dim StrsTr As String
    
     Dim retcode As Long
     Dim ddif As Long
     CRvar = Chr(13)
     LFvar = Chr(10)
     
    
     
    
         ' dissconnect
         ObjectXUnixDisplay1.Disconnect
         ObjectXUnixDisplay1.ClearCommunications
         ObjectXUnixDisplay1.ClearDisplay
         
         ' reconnect
         
          'Dim TNInterface As ObjectXTelnet
            ' if unix host
            Dim TNInterface As Object
            ObjectXUnixDisplay1.HostInterface.Name = "WallData.TelNet"
            Set TNInterface = ObjectXUnixDisplay1.HostInterface.Interface
             TNInterface.IpAddr = m_HostIP
             TNInterface.TcpPort = m_TcpPort
             ''' note dose not work for wan case
            ' TNInterface.Timeout = m_HostTimeOut
             TNInterface.Commit
             'ObjectXUnixDisplay1.LinesPerPage = LinesPerPage
             
             hlog "IP=" & m_HostIP '& "  User=" & m_LoginId & " PWD=" & LoginPWD
             
             
             
             ObjectXUnixDisplay1.Disconnect
             ConnectionSuccess = -1
             frmDisplay.Timer1.Enabled = True
              
             retcode = ObjectXUnixDisplay1.Connect
             frmDisplay.Timer1.Enabled = False
             If retcode <> 0 Or ConnectionSuccess <> 0 Then
                DoLogin = 1
                hlog "Connect Method failed for Host:" & m_HostIP & " retcode=" & retcode & " connectionSuccess=" & ConnectionSuccess
                
                Exit Function
             End If
             DelayPeriod WaitForhosRsponsePeriod

             
             TimeStamp = Now
            
             TimeOutFlag = False
             ' send CR charactr
             If m_NeedInitialCROrLf = True Then
                ObjectXUnixDisplay1.SendKeys Chr(InitialChr)
             End If
             'read all screen to sarch for Login word
             StrsTr = ""
             
             ObjectXUnixDisplay1.GetScreen StrsTr, 1, 1, 1920
             While InStr(1, StrsTr, m_LoginWord) = 0 And Not TimeOutFlag
                    ddif = DateDiff("S", TimeStamp, Now)
                    If ddif >= m_HostTimeOut Then
                           TimeOutFlag = True
                           DoLogin = 2
                           hlog "Can Not Find Login Word:" & m_LoginWord & vbNewLine & "Screen :" & vbNewLine & StrsTr
                           
                           Exit Function
                     End If
                     ObjectXUnixDisplay1.GetScreen StrsTr, 1, 1, 1920
                     DoEvents
              Wend
              
              ObjectXUnixDisplay1.SendKeys m_LoginId & Chr(AfterLoginIdChar)
              DelayPeriod WaitForhosRsponsePeriod
              
              If m_PWDEnabled = True Then
                     
                    TimeStamp = Now
                    
                    TimeOutFlag = False
                    ObjectXUnixDisplay1.GetScreen StrsTr, 1, 1, 1920
                    While InStr(1, StrsTr, m_PWDWord) = 0 And Not TimeOutFlag
                       ddif = DateDiff("S", TimeStamp, Now)
                       If ddif > m_HostTimeOut Then
                           TimeOutFlag = True
                           
                           DoLogin = 3
                           hlog "Can Not Find PWD Word:" & m_PWDWord & vbNewLine & "Screen :" & vbNewLine & StrsTr
                           Exit Function
                        End If
                        ObjectXUnixDisplay1.GetScreen StrsTr, 1, 1, 1920
                        DoEvents
                   Wend
  
                  ObjectXUnixDisplay1.SendKeys m_LoginPWD & CRvar
                  DelayPeriod WaitForhosRsponsePeriod
              End If
         'if need      any Enter character (13) to reach appliction screen
         If NeedAfterLoginCR = True Then
            For i = 1 To m_NoOfAfterLoginCR
            ObjectXUnixDisplay1.SendKeys CRvar
            DelayPeriod WaitForhosRsponsePeriod
            Next i
         End If
         ' search fr progra idntifir
                   
             TimeStamp = Now
             TimeOutFlag = False
                 frmDisplay.lblStatus = "Waiting :[" & m_HostAppIdentifier & "] title"
                 
                  ObjectXUnixDisplay1.GetScreen StrsTr, m_HostAppIdenifirRow, m_HostAppIdentifierCol, m_HostAppIdentifierLen
                    While Mid(StrsTr, 1, m_HostAppIdentifierLen) <> m_HostAppIdentifier And Not TimeOutFlag
                      ddif = DateDiff("S", TimeStamp, Now)
                       If ddif > m_HostTimeOut Then
                           TimeOutFlag = True
                           DoLogin = 4
                           hlog "Can Not Find App. Identifier:" & m_HostAppIdentifier & " at row=" & m_HostAppIdenifirRow & "  and col=" & m_HostAppIdentifierCol & " of Len =" & m_HostAppIdentifierLen & vbNewLine & "Screen :"
                          tmptmp = ""
                          ObjectXUnixDisplay1.GetScreen StrsTr, 1, 1, 1900
                          For i = 1 To Len(StrsTr) Step 80
                             tmptmp = tmptmp & vbNewLine & Mid(StrsTr, i, 80)
                             
                           DoEvents
                          Next i
                            hlog tmptmp
                           Exit Function
                        End If
                        ObjectXUnixDisplay1.GetScreen StrsTr, m_HostAppIdenifirRow, m_HostAppIdentifierCol, m_HostAppIdentifierLen
                        DoEvents
                   Wend
              
                   DoLogin = 0
                    frmDisplay.lblStatus = "Login OK"
                
              
End Function
Public Property Get LoginWord() As String
    LoginWord = m_LoginWord
End Property

Public Property Let LoginWord(ByVal New_LoginWord As String)
    m_LoginWord = New_LoginWord
    
End Property

Public Property Get PWDWord() As String
    PWDWord = m_PWDWord
End Property

Public Property Let PWDWord(ByVal New_PWDWord As String)
    m_PWDWord = New_PWDWord
    
End Property

Public Property Get NeedInitialCROrLf() As Boolean
    NeedInitialCROrLf = m_NeedInitialCROrLf
End Property

Public Property Let NeedInitialCROrLf(ByVal New_NeedInitialCROrLf As Boolean)
    m_NeedInitialCROrLf = New_NeedInitialCROrLf
    
End Property



Public Property Get InitialChr() As Integer
    InitialChr = m_InitialChr
End Property

Public Property Let InitialChr(ByVal New_Initialchr As Integer)
    m_InitialChr = New_Initialchr
    
End Property


Public Property Get NeedAfterLoginCR() As Boolean
    NeedAfterLoginCR = m_NeedAfterLoginCR
End Property

Public Property Let NeedAfterLoginCR(ByVal New_NeedAfterLoginCR As Boolean)
    m_NeedAfterLoginCR = New_NeedAfterLoginCR
    
End Property


Public Property Get NoOfAfterLoginCR() As Integer
    NoOfAfterLoginCR = m_NoOfAfterLoginCR
End Property

Public Property Let NoOfAfterLoginCR(ByVal New_NoOfAfterLoginCR As Integer)
    m_NoOfAfterLoginCR = New_NoOfAfterLoginCR
    
End Property

Public Property Get HostAppIdentifierLen() As Long
    HostAppIdentifierLen = m_HostAppIdentifierLen
End Property

Public Property Let HostAppIdentifierLen(ByVal New_HostAppIdentifierLen As Long)
    m_HostAppIdentifierLen = New_HostAppIdentifierLen
    
End Property

Public Property Get CursorTopPosCol() As Integer
    CursorTopPosCol = m_CursorTopPosCol
End Property

Public Property Let CursorTopPosCol(ByVal New_CursorTopPosCol As Integer)
    m_CursorTopPosCol = New_CursorTopPosCol
    
End Property

Public Property Get CursorTopPosRow() As Integer
    CursorTopPosRow = m_CursorTopPosRow
End Property

Public Property Let CursorTopPosRow(ByVal New_CursorTopPosRow As Integer)
    m_CursorTopPosRow = New_CursorTopPosRow
   
End Property

Public Property Get TerminalId() As Integer
    TerminalId = m_TerminalId
End Property

Public Property Let TerminalId(ByVal New_TerminalId As Integer)
    m_TerminalId = New_TerminalId
   
End Property

Public Property Get SendRecieveChrRow() As Integer
    SendRecieveChrRow = m_SendRecieveChrRow
End Property

Public Property Let SendRecieveChrRow(ByVal New_SendRecieveChrRow As Integer)
    m_SendRecieveChrRow = New_SendRecieveChrRow
  
End Property

Public Property Get SendRecieveChrCol() As Integer
    SendRecieveChrCol = m_SendRecieveChrCol
End Property

Public Property Let SendRecieveChrCol(ByVal New_SendRecieveChrCol As Integer)
    m_SendRecieveChrCol = New_SendRecieveChrCol
    
End Property

Public Property Get SendChr() As String
    SendChr = m_SendChr
End Property

Public Property Let SendChr(ByVal New_SendChr As String)
    m_SendChr = New_SendChr
   
End Property

Public Property Get RecieveChr() As String
    RecieveChr = m_RecieveChr
End Property

Public Property Let RecieveChr(ByVal New_RecieveChr As String)
    m_RecieveChr = New_RecieveChr
   
End Property

Public Property Get ClearScreenReqCode() As String
    ClearScreenReqCode = m_ClearScrenReqCode
End Property

Public Property Let ClearScreenReqCode(ByVal New_ClearScreenReqCode As String)
    m_ClearScrenReqCode = New_ClearScreenReqCode
   
End Property


Private Sub Class_Initialize()

  m_ClearScrenReqCode = "777"
  m_SendRecieveChrRow = 24
  m_SendRecieveChrCol = 79
  m_SendChr = "$"
  m_RecieveChr = "*"
  m_TerminalId = wdVT220
  m_CursorTopPosCol = 11
  m_CursorTopPosRow = 24
  m_HostAppIdentifierLen = 7
  m_NeedAfterLoginCR = False
  m_NoOfAfterLoginCR = 3
  m_NeedInitialCROrLf = False
  m_InitialChr = 13
  m_ReceiveSendInitialChr = " "
  m_LoginWord = "login:"
  m_PWDWord = "Password:"
  m_HostTimeOut = 30
  m_HostIsReady = False
  m_HostIP = "192.127.143.108"
  m_TcpPort = 23
  m_LoginId = "root"
  m_LoginPWD = "root"
  m_HostAppIdentifier = "Welcome"
  m_HostAppIdenifirRow = 1
  m_HostAppIdentifierCol = 1
  m_PWDEnabled = True
  LinesPerPage = 24
  ShowHostScreen = True
  AfterLoginIdChar = 13
  ApplicationExitChar = 13
  ApplicationExitCharTimes = 1
  Set ObjectXUnixDisplay1 = frmDisplay.ObjectXUnixDisplay1
End Sub

Public Sub hlog(DataStr As String)
Dim fl As Long
Dim fname As String
Dim shortN As String
Dim pLogPath As String
Dim lineStr As String
Dim i As Integer
On Error Resume Next

If Right(LogPath, 1) <> "\" Then
   pLogPath = LogPath & "\"
   Else
   pLogPath = LogPath
End If


fl = 0
shortN = Format(Date, "ddmmyy_") & ATMId & ".Log"
If Dir(pLogPath, vbDirectory) = "" Then
   MkDir pLogPath
End If
fname = pLogPath & shortN
  

    Open fname For Append As #1
          Print #1, "========================================================="
          Print #1, Format(Date, "dd/mm/yyyy") & " " & Format(Time, "hh:mm:ss")
        
              
              Print #1, DataStr
         
         
    Close #1

End Sub


Private Sub Class_Terminate()
ObjectXUnixDisplay1.Disconnect
Unload frmDisplay
End Sub


Public Function SendRecieve(ByVal DataStr As String, ByRef RecievedDataSr As String) As Long
Dim StrsTr As String
Dim TimeStamp As Date
Dim TimeOutFlag As Boolean
Dim retcode As Long
Dim dDiff As Long
Dim i As Integer
Dim tmp As String
Dim tmptmp As String
Dim ret As Integer

On Error GoTo errh
frmDisplay.SHPStatus.FillColor = vbRed
If ShowHostScreen = True Then frmDisplay.Show
ret = IsHostReady
If ret <> 0 Then
    If DoLogin <> 0 Then
    SendRecieve = -1
    hlog "DoLogin Failed,See Previouse Error User=" '& m_LoginId & "  PWD=" & m_LoginPWD
    frmDisplay.SHPStatus.FillColor = vbGreen
    frmDisplay.lblStatus = "Login failed"
    Exit Function
   End If
End If


StrsTr = ""
TimeStamp = Now
TimeOutFlag = False

 frmDisplay.lblStatus = "Waiting :[" & m_ReceiveSendInitialChr & "] flag"
ObjectXUnixDisplay1.GetScreen StrsTr, m_SendRecieveChrRow, m_SendRecieveChrCol, 1
 While Mid(StrsTr, 1, 1) <> m_ReceiveSendInitialChr And Not TimeOutFlag
       dDiff = DateDiff("S", TimeStamp, Now)
       If dDiff >= m_HostTimeOut Then
           TimeOutFlag = True
           SendRecieve = -3
           frmDisplay.SHPStatus.FillColor = vbGreen
           frmDisplay.lblStatus = "No receive character - error"
           ObjectXUnixDisplay1.GetScreen tmp, 1, 1, 1920
           hlog "can not find ReceiveSendInitialChr:[" & m_ReceiveSendInitialChr & "] at row=" & m_SendRecieveChrRow & " and col=" & m_SendRecieveChrCol & " with Data=" & StrsTr & vbNewLine & "Screen="
           tmptmp = ""
           For i = 1 To Len(tmp) Step 80
               tmptmp = tmptmp & vbNewLine & Mid(tmp, i, 80)
           Next i
           hlog tmptmp
           
           Exit Function
       End If
        DoEvents
       ObjectXUnixDisplay1.GetScreen StrsTr, m_SendRecieveChrRow, m_SendRecieveChrCol, 1
  
  Wend
  
  tmp = DataStr & m_SendChr
retcode = ObjectXUnixDisplay1.SendKeys(tmp)
retcode = ObjectXUnixDisplay1.SendKeys(Chr(13))

If retcode <> 0 Then
       SendRecieve = -4
           frmDisplay.SHPStatus.FillColor = vbGreen
           frmDisplay.lblStatus = "Writing data  - error"
       hlog "SendKeys Failed Data:" & DataStr
       Exit Function
 End If
frmDisplay.lblStatus = "Data Was Writen"
DelayPeriod 0.5

StrsTr = ""
TimeStamp = Now
TimeOutFlag = False
frmDisplay.lblStatus = "Waiting :[" & m_RecieveChr & "] Finish processing flag"
ObjectXUnixDisplay1.GetScreen StrsTr, m_SendRecieveChrRow, m_SendRecieveChrCol, 1
 While Mid(StrsTr, 1, 1) <> m_RecieveChr And Not TimeOutFlag
       dDiff = DateDiff("S", TimeStamp, Now)
       If dDiff >= m_HostTimeOut Then
           TimeOutFlag = True
           TimeOutFlag = True
           SendRecieve = -5
           frmDisplay.SHPStatus.FillColor = vbGreen
           frmDisplay.lblStatus = "No receive character - error"
           ObjectXUnixDisplay1.GetScreen tmp, 1, 1, 1920
           hlog "can not find RecieveChar:" & m_RecieveChr & " at row=" & m_SendRecieveChrRow & " and col=" & m_SendRecieveChrCol & " with Data=" & StrsTr & vbNewLine & "Screen="
           tmptmp = ""
           For i = 1 To Len(tmp) Step 80
               tmptmp = tmptmp & vbNewLine & Mid(tmp, i, 80)
           Next i
           hlog tmptmp
         
           Exit Function
       End If
       ObjectXUnixDisplay1.GetScreen StrsTr, m_SendRecieveChrRow, m_SendRecieveChrCol, 1
       DoEvents
  Wend
  SendRecieve = 0
  
  
  ObjectXUnixDisplay1.GetScreen StrsTr, ResponseDataRow, ResponseDataCol, ResponseDataLen
  ObjectXUnixDisplay1.GetScreen tmp, 1, 1, 1920
  hlog "Success Response data at row=" & ResponseDataRow & " and col=" & ResponseDataCol & " =" & StrsTr & vbNewLine & "Screen=" & vbNewLine
  tmptmp = ""
  For i = 1 To Len(tmp) Step 80
      tmptmp = tmptmp & vbNewLine & Mid(tmp, i, 80)
  Next i
  hlog tmptmp
         
  RecievedDataSr = Trim(Left(StrsTr, ResponseDataLen))
  frmDisplay.lblStatus = "Send Receive Complete received response:" & RecievedDataSr
  frmDisplay.SHPStatus.FillColor = vbGreen
'''For i = 1 To ApplicationExitCharTimes
'''RetCode = ObjectXUnixDisplay1.SendKeys(Chr(ApplicationExitChar))
'''DelayPeriod 2
'''Next i
 Exit Function
errh:
    SendRecieve = -9
    hlog "SendReceive exception Error:" & Err.Description
End Function

Public Function ExitApplication() As Integer
Dim i As Integer
Dim retcode As Integer

For i = 1 To ApplicationExitCharTimes
retcode = ObjectXUnixDisplay1.SendKeys(Chr(ApplicationExitChar))
DelayPeriod 2
Next i


End Function
