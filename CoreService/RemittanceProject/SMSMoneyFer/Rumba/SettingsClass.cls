VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "SettingsClass"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Attribute VB_Ext_KEY = "SavedWithClassBuilder6" ,"Yes"
Attribute VB_Ext_KEY = "Top_Level" ,"Yes"
Option Explicit

Public HostIP As String ' "194.0.0.100"
Public PWDEnabled As Boolean ' True
Public PWDWord As String ' "Password"
Public LoginWord As String ' "User"
Public LoginId As String ' "ATMNCR"
Public encLoginId As String
Public LoginPWD As String ' "ATMNCR"
Public encLoginPWD As String

Public NeedInitialCROrLf As Boolean ' False
Public NeedAfterLoginCR As Boolean 'True
Public NoOfAfterLoginCR As Integer '1
Public CursorTopPosCol  As Integer ' 53
Public CursorTopPosRow  As Integer ' 8
Public HostAppIdentifierLen  As Integer ' 28
Public HostAppIdenifirRow  As Integer ' 1
Public HostAppIdentifierCol  As Integer ' 27
Public InitialChr  As Integer ' 13
Public ReceiveSendInitialChr As String ' " "
Public RecieveChr As String ' "@"
Public SendChr As String ' "$"
Public ShowHostScreen As Boolean ' True
Public HostAppIdentifier As String ' "BNA CASH DEPOSIT APPLICATION"
Public AfterLoginIdChar  As Integer ' 9 'tab
Public ApplicationExitCharTimes  As Integer ' 2
Public ApplicationExitChar  As Integer '13
Public ResponseDataCol  As Integer '25
Public ResponseDataRow  As Integer ' 20
Public ResponseDataLen  As Integer ' 2
Public WaitForhosRsponsePeriod  As Single ' 0.5
Public HostTimeOut  As Integer ' 5
Public LogPath As String ' "c:\BNA_Log\"

Public Function ReadSettings(SettingsFile As String) As Integer


Dim etmp As String
Dim tmp As String
Dim x As New xEnc

 HostIP = GetSecKeyValue(SettingsFile, "Host", "HostIp", "194.0.0.100")
 PWDEnabled = GetSecKeyValue(SettingsFile, "Host", "PWDEnabled", True)
 PWDWord = GetSecKeyValue(SettingsFile, "Host", "PWDWord", "Password")
 LoginWord = GetSecKeyValue(SettingsFile, "Host", "LoginWord", "User")
 
 etmp = GetSecKeyValue(SettingsFile, "Host", "LoginId", "ATMNCR")
 encLoginId = etmp
 Call x.BitsIntoByte(etmp, "TrueLife", tmp)
 LoginId = tmp
 
 etmp = GetSecKeyValue(SettingsFile, "Host", "LoginPWD", "ATMNCR")
 encLoginPWD = etmp
 Call x.BitsIntoByte(etmp, "TrueLife", tmp)
 LoginPWD = tmp
 
 
 NeedInitialCROrLf = GetSecKeyValue(SettingsFile, "Host", "NeedInitialCROrLf", False)
 NeedAfterLoginCR = GetSecKeyValue(SettingsFile, "Host", "NeedAfterLoginCR", True)
 NoOfAfterLoginCR = GetSecKeyValue(SettingsFile, "Host", "NoOfAfterLoginCR", 1)
 CursorTopPosCol = GetSecKeyValue(SettingsFile, "Host", "CursorTopPosCol", 25)
 CursorTopPosRow = GetSecKeyValue(SettingsFile, "Host", "CursorTopPosRow", 4)
 HostAppIdentifierLen = GetSecKeyValue(SettingsFile, "Host", "HostAppIdentifierLen", 28)
 HostAppIdenifirRow = GetSecKeyValue(SettingsFile, "Host", "HostAppIdenifirRow", 1)
 HostAppIdentifierCol = GetSecKeyValue(SettingsFile, "Host", "HostAppIdentifierCol", 27)
 InitialChr = GetSecKeyValue(SettingsFile, "Host", "InitialChr", 13)
 ReceiveSendInitialChr = GetSecKeyValue(SettingsFile, "Host", "ReceiveSendInitialChr", " ")
 RecieveChr = GetSecKeyValue(SettingsFile, "Host", "RecieveChr", "@")
 SendChr = GetSecKeyValue(SettingsFile, "Host", "SendChr", "$")
 ShowHostScreen = GetSecKeyValue(SettingsFile, "Host", "ShowHostScreen", True)
 HostAppIdentifier = GetSecKeyValue(SettingsFile, "Host", "HostAppIdentifier", "BNA CASH DEPOSIT APPLICATION")
 AfterLoginIdChar = GetSecKeyValue(SettingsFile, "Host", "AfterLoginIdChar", 9)    'tab
 ApplicationExitCharTimes = GetSecKeyValue(SettingsFile, "Host", "ApplicationExitCharTimes", 2)
 ApplicationExitChar = GetSecKeyValue(SettingsFile, "Host", "ApplicationExitChar", 13)
 ResponseDataCol = GetSecKeyValue(SettingsFile, "Host", "ResponseDataCol", 25)
 ResponseDataRow = GetSecKeyValue(SettingsFile, "Host", "ResponseDataRow", 20)
 ResponseDataLen = GetSecKeyValue(SettingsFile, "Host", "ResponseDataLen", 2)
 WaitForhosRsponsePeriod = GetSecKeyValue(SettingsFile, "Host", "WaitForhosRsponsePeriod", 0.5)
 HostTimeOut = GetSecKeyValue(SettingsFile, "Host", "HostTimeOut", 5)
 LogPath = GetSecKeyValue(SettingsFile, "Host", "LogPath", "c:\BNA_Log\")

  WriteSettings SettingsFile
  


End Function
Public Function WriteSettings(SettingsFile As String) As Integer
Dim tt As String
  Call PutSecKeyValue(SettingsFile, "Host", "HostIp", HostIP)
  tt = PWDEnabled
  Call PutSecKeyValue(SettingsFile, "Host", "PWDEnabled", tt)
  Call PutSecKeyValue(SettingsFile, "Host", "PWDWord", PWDWord)
  Call PutSecKeyValue(SettingsFile, "Host", "LoginWord", LoginWord)
  Call PutSecKeyValue(SettingsFile, "Host", "LoginId", LoginId)
  Call PutSecKeyValue(SettingsFile, "Host", "LoginPWD", LoginPWD)
  tt = NeedInitialCROrLf
  Call PutSecKeyValue(SettingsFile, "Host", "NeedInitialCROrLf", tt)
  tt = NeedAfterLoginCR
  Call PutSecKeyValue(SettingsFile, "Host", "NeedAfterLoginCR", tt)
  tt = NoOfAfterLoginCR
  Call PutSecKeyValue(SettingsFile, "Host", "NoOfAfterLoginCR", tt)
  tt = CursorTopPosCol
  Call PutSecKeyValue(SettingsFile, "Host", "CursorTopPosCol", tt)
  tt = CursorTopPosRow
  Call PutSecKeyValue(SettingsFile, "Host", "CursorTopPosRow", tt)
  tt = HostAppIdentifierLen
  Call PutSecKeyValue(SettingsFile, "Host", "HostAppIdentifierLen", tt)
   tt = HostAppIdenifirRow
  Call PutSecKeyValue(SettingsFile, "Host", "HostAppIdenifirRow", tt)
  tt = HostAppIdentifierCol
  Call PutSecKeyValue(SettingsFile, "Host", "HostAppIdentifierCol", tt)
  tt = InitialChr
  Call PutSecKeyValue(SettingsFile, "Host", "InitialChr", tt)
  Call PutSecKeyValue(SettingsFile, "Host", "ReceiveSendInitialChr", ReceiveSendInitialChr)
  Call PutSecKeyValue(SettingsFile, "Host", "RecieveChr", RecieveChr)
  Call PutSecKeyValue(SettingsFile, "Host", "SendChr", SendChr)
  tt = ShowHostScreen
  Call PutSecKeyValue(SettingsFile, "Host", "ShowHostScreen", tt)
  Call PutSecKeyValue(SettingsFile, "Host", "HostAppIdentifier", HostAppIdentifier)
  tt = AfterLoginIdChar
  Call PutSecKeyValue(SettingsFile, "Host", "AfterLoginIdChar", tt)    'tab
  tt = ApplicationExitCharTimes
  Call PutSecKeyValue(SettingsFile, "Host", "ApplicationExitCharTimes", tt)
  tt = ApplicationExitChar
  Call PutSecKeyValue(SettingsFile, "Host", "ApplicationExitChar", tt)
  tt = ResponseDataCol
  Call PutSecKeyValue(SettingsFile, "Host", "ResponseDataCol", tt)
  tt = ResponseDataRow
  Call PutSecKeyValue(SettingsFile, "Host", "ResponseDataRow", tt)
  tt = ResponseDataLen
  Call PutSecKeyValue(SettingsFile, "Host", "ResponseDataLen", tt)
  tt = WaitForhosRsponsePeriod
  Call PutSecKeyValue(SettingsFile, "Host", "WaitForhosRsponsePeriod", tt)
  tt = HostTimeOut
  Call PutSecKeyValue(SettingsFile, "Host", "HostTimeOut", tt)
  Call PutSecKeyValue(SettingsFile, "Host", "LogPath", LogPath)




End Function
Private Sub Class_Initialize()
 HostIP = "194.0.0.100"
 PWDEnabled = True
 PWDWord = "Password"
 LoginWord = "User"
 LoginId = "ATMNCR"
 LoginPWD = "ATMNCR"
 NeedInitialCROrLf = False
 NeedAfterLoginCR = True
 NoOfAfterLoginCR = 1
 CursorTopPosCol = 53
 CursorTopPosRow = 8
 HostAppIdentifierLen = 28
 HostAppIdenifirRow = 1
 HostAppIdentifierCol = 27
 InitialChr = 13
 ReceiveSendInitialChr = " "
 RecieveChr = "@"
 SendChr = "$"
 ShowHostScreen = True
 HostAppIdentifier = "BNA CASH DEPOSIT APPLICATION"
 AfterLoginIdChar = 9 'tab
 ApplicationExitCharTimes = 2
 ApplicationExitChar = 13
 ResponseDataCol = 25
 ResponseDataRow = 20
 ResponseDataLen = 2
 WaitForhosRsponsePeriod = 0.5
 HostTimeOut = 5
 LogPath = "c:\BNA_Log\"

End Sub
