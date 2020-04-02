VERSION 5.00
Begin VB.Form frmtest 
   Caption         =   "Form1"
   ClientHeight    =   3195
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   4680
   LinkTopic       =   "Form1"
   ScaleHeight     =   3195
   ScaleWidth      =   4680
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton Command1 
      Caption         =   "Command1"
      Height          =   1935
      Left            =   480
      TabIndex        =   0
      Top             =   360
      Width           =   3735
   End
End
Attribute VB_Name = "frmtest"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Sub Command1_Click()
Dim hc As New NCRHostClientMF_AS400.HostClient
Dim ret As Long
Dim req As String
Dim rep As String

hc.HostIP = "194.0.0.100"
hc.PWDEnabled = True
hc.PWDWord = "Password"
hc.LoginWord = "User"
hc.LoginId = "ATMNCR"
hc.LoginPWD = "ATMNCR"
hc.NeedAfterLoginCR = False
hc.NoOfAfterLoginCR = 0
hc.CursorTopPosCol = 53
hc.CursorTopPosRow = 8
hc.HostAppIdentifierLen = 28
hc.HostAppIdenifirRow = 1
hc.HostAppIdentifierCol = 27
hc.RecieveChr = "@"
hc.SendChr = "$"
hc.ShowHostScreen = True
hc.HostAppIdentifier = "BNA CASH DEPOSIT APPLICATION"
hc.AfterLoginIdChar = 9 'tab
hc.ApplicationExitCharTimes = 2
hc.ApplicationExitChar = 13
hc.ResponseDataCol = 25
hc.ResponseDataRow = 20
hc.ResponseDataLen = 2
hc.WaitForhosRsponsePeriod = 0.5
hc.HostTimeOut = 5
hc.ShowHostScreen = True

req = "2323533253425446476758587686786"
ret = hc.SendRecieve(req, rep)


End Sub
