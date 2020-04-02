VERSION 5.00
Object = "{248DD890-BB45-11CF-9ABC-0080C7E7B78D}#1.0#0"; "MSWINSCK.OCX"
Begin VB.Form Form1 
   ClientHeight    =   5565
   ClientLeft      =   60
   ClientTop       =   450
   ClientWidth     =   8310
   LinkTopic       =   "Form1"
   ScaleHeight     =   5565
   ScaleWidth      =   8310
   StartUpPosition =   3  'Windows Default
   Begin VB.TextBox txtPort 
      Height          =   375
      Left            =   1680
      TabIndex        =   18
      Text            =   "1009"
      Top             =   120
      Width           =   1095
   End
   Begin VB.TextBox txtIPAddress 
      Height          =   375
      Left            =   240
      TabIndex        =   17
      Text            =   "10.20.30.64"
      Top             =   120
      Width           =   1215
   End
   Begin VB.CommandButton Command11 
      Caption         =   "UnHold"
      Height          =   615
      Left            =   4320
      TabIndex        =   16
      Top             =   5520
      Width           =   735
   End
   Begin VB.CommandButton Command10 
      Caption         =   "Hold"
      Height          =   975
      Left            =   4320
      TabIndex        =   15
      Top             =   4440
      Width           =   975
   End
   Begin VB.CommandButton Command9 
      Caption         =   "ReActivate"
      Height          =   975
      Left            =   3240
      TabIndex        =   14
      Top             =   4440
      Width           =   975
   End
   Begin VB.TextBox Text3 
      Height          =   495
      Left            =   1440
      TabIndex        =   13
      Text            =   "5334"
      Top             =   5040
      Width           =   1215
   End
   Begin VB.TextBox Text2 
      Height          =   495
      Left            =   120
      TabIndex        =   12
      Text            =   "5334"
      Top             =   5040
      Width           =   1095
   End
   Begin VB.CommandButton Command8 
      Caption         =   "Cancel Withdraw"
      Height          =   1215
      Left            =   5640
      TabIndex        =   11
      Top             =   4800
      Width           =   1095
   End
   Begin VB.CommandButton Command7 
      Caption         =   "Confirm Withdraw"
      Height          =   1215
      Left            =   6840
      TabIndex        =   10
      Top             =   3480
      Width           =   1095
   End
   Begin VB.CommandButton Command6 
      Caption         =   "send Withdraw"
      Height          =   1215
      Left            =   5640
      TabIndex        =   9
      Top             =   3480
      Width           =   1095
   End
   Begin VB.TextBox txtHTC 
      Height          =   495
      Left            =   120
      TabIndex        =   8
      Text            =   "053630434040"
      Top             =   4440
      Width           =   3015
   End
   Begin VB.CommandButton Command5 
      Caption         =   "Close"
      Height          =   615
      Left            =   4680
      TabIndex        =   7
      Top             =   720
      Width           =   855
   End
   Begin VB.CommandButton Command4 
      Caption         =   "Cancel deposit"
      Height          =   1215
      Left            =   5640
      TabIndex        =   6
      Top             =   2040
      Width           =   1095
   End
   Begin VB.CommandButton Command3 
      Caption         =   "confirm deposit"
      Height          =   1215
      Left            =   6840
      TabIndex        =   5
      Top             =   2040
      Width           =   1095
   End
   Begin VB.TextBox txtatm 
      Height          =   495
      Left            =   3240
      TabIndex        =   4
      Text            =   "001"
      Top             =   720
      Width           =   855
   End
   Begin VB.TextBox Text1 
      Height          =   2895
      Left            =   120
      MultiLine       =   -1  'True
      ScrollBars      =   2  'Vertical
      TabIndex        =   3
      Top             =   1440
      Width           =   5415
   End
   Begin VB.CommandButton Command2 
      Caption         =   "send deposit"
      Height          =   1215
      Left            =   6840
      TabIndex        =   2
      Top             =   720
      Width           =   1095
   End
   Begin VB.CommandButton Command1 
      Caption         =   "Connect"
      Height          =   1215
      Left            =   5640
      TabIndex        =   0
      Top             =   720
      Width           =   1095
   End
   Begin MSWinsockLib.Winsock Winsock1 
      Left            =   2400
      Top             =   720
      _ExtentX        =   741
      _ExtentY        =   741
      _Version        =   393216
   End
   Begin VB.Label lblStatus 
      Height          =   495
      Left            =   120
      TabIndex        =   1
      Top             =   720
      Width           =   2055
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Dim trxCode As String
Private Sub Command1_Click()
Winsock1.Connect txtIPAddress, txtPort

End Sub

Private Sub Command10_Click()
Dim ss As String

ss = Left(txtatm & Space(5), 5)
ss = ss & Left("TUB" & Space(5), 5)
ss = ss & Left("20" & Space(10), 10)
ss = ss & "04"
ss = ss & Space(5)
ss = ss & Format(Now, "yyyy/MM/dd")
ss = ss & Format(Now, "HH:mm:ss")
ss = ss & "1020304050"
ss = ss & "0020101010101       "
ss = ss & Left(Text2 & Space(10), 10)
ss = ss & "0020121314151       "
ss = ss & Left(Text3 & Space(10), 10)
ss = ss & Left("1000" & Space(15), 15)
ss = ss & "EGP  "
ss = ss & Left(txtHTC & Space(20), 20)
ss = ss & Space(25)
ss = ss & Space(8)
ss = Format(Len(ss), "0000") & ss
Winsock1.SendData ss


End Sub

Private Sub Command11_Click()
Dim ss As String

ss = Left(txtatm & Space(5), 5)
ss = ss & Left("TUB" & Space(5), 5)
ss = ss & Left("20" & Space(10), 10)
ss = ss & "14"
ss = ss & Space(5)
ss = ss & Format(Now, "yyyy/MM/dd")
ss = ss & Format(Now, "HH:mm:ss")
ss = ss & "1020304050"
ss = ss & "0020101010101       "
ss = ss & Left(Text2 & Space(10), 10)
ss = ss & "0020121314151       "
ss = ss & Left(Text3 & Space(10), 10)
ss = ss & Left("1000" & Space(15), 15)
ss = ss & "EGP  "
ss = ss & Left(txtHTC & Space(20), 20)
ss = ss & Space(25)
ss = ss & Space(8)
ss = Format(Len(ss), "0000") & ss
Winsock1.SendData ss

End Sub

Private Sub Command2_Click()
Dim ss As String

ss = Left(txtatm & Space(5), 5)
ss = ss & Left("TUB" & Space(5), 5)
ss = ss & Left("20" & Space(10), 10)
ss = ss & "01"
ss = ss & Space(5)
ss = ss & Format(Now, "yyyy/MM/dd")
ss = ss & Format(Now, "HH:mm:ss")
ss = ss & "1020304050"
ss = ss & "0020101010101       "
ss = ss & "          "
ss = ss & "0020121314151       "
ss = ss & "          "
ss = ss & Left("1000" & Space(15), 15)
ss = ss & "EGP  "
ss = ss & "20202020202020202020"
ss = ss & Space(25)
ss = ss & Space(8)
ss = Format(Len(ss), "0000") & ss
Winsock1.SendData ss


End Sub

Private Sub Command3_Click()
Dim ss As String

ss = Left(txtatm & Space(5), 5)
ss = ss & Left("TUB" & Space(5), 5)
ss = ss & Left("20" & Space(10), 10)
ss = ss & "11"
ss = ss & Space(5)
ss = ss & Format(Now, "yyyy/MM/dd")
ss = ss & Format(Now, "HH:mm:ss")
ss = ss & "1020304050"
ss = ss & "0020101010101       "
ss = ss & Left(Text2 & Space(10), 10)
ss = ss & "0020121314151       "
ss = ss & Left(Text3 & Space(10), 10)
ss = ss & Left("1000" & Space(15), 15)
ss = ss & "EGP  "
ss = ss & Left(txtHTC & Space(20), 20)
ss = ss & Space(25)
ss = ss & Space(8)
ss = Format(Len(ss), "0000") & ss
Winsock1.SendData ss

End Sub

Private Sub Command4_Click()
Dim ss As String

ss = Left(txtatm & Space(5), 5)
ss = ss & Left("TUB" & Space(5), 5)
ss = ss & Left("20" & Space(10), 10)
ss = ss & "21"
ss = ss & Space(5)
ss = ss & Format(Now, "yyyy/MM/dd")
ss = ss & Format(Now, "HH:mm:ss")
ss = ss & "1020304050"
ss = ss & "0020101010101       "
ss = ss & Left(Text2 & Space(10), 10)
ss = ss & "0020121314151       "
ss = ss & Left(Text3 & Space(10), 10)
ss = ss & Left("1000" & Space(15), 15)
ss = ss & "EGP  "
ss = ss & Left(txtHTC & Space(20), 20)
ss = ss & Space(25)
ss = ss & Space(8)
ss = Format(Len(ss), "0000") & ss
Winsock1.SendData ss

End Sub

Private Sub Command5_Click()
Winsock1.Close
End Sub

Private Sub Command6_Click()
Dim ss As String

ss = Left(txtatm & Space(5), 5)
ss = ss & Left("TUB" & Space(5), 5)
ss = ss & Left("20" & Space(10), 10)
ss = ss & "02"
ss = ss & Space(5)
ss = ss & Format(Now, "yyyy/MM/dd")
ss = ss & Format(Now, "HH:mm:ss")
ss = ss & "1020304050"
ss = ss & "0020101010101       "
ss = ss & Left(Text2 & Space(10), 10)
ss = ss & "0020121314151       "
ss = ss & Left(Text3 & Space(10), 10)
ss = ss & Left("1000" & Space(15), 15)
ss = ss & "EGP  "
ss = ss & Left(txtHTC & Space(20), 20)
ss = ss & Space(25)
ss = ss & Space(8)
ss = Format(Len(ss), "0000") & ss
Winsock1.SendData ss

End Sub

Private Sub Command7_Click()
Dim ss As String

ss = Left(txtatm & Space(5), 5)
ss = ss & Left("TUB" & Space(5), 5)
ss = ss & Left("20" & Space(10), 10)
ss = ss & "12"
ss = ss & Space(5)
ss = ss & Format(Now, "yyyy/MM/dd")
ss = ss & Format(Now, "HH:mm:ss")
ss = ss & "1020304050"
ss = ss & "0020101010101       "
ss = ss & Left(Text2 & Space(10), 10)
ss = ss & "0020121314151       "
ss = ss & Left(Text3 & Space(10), 10)
ss = ss & Left("1000" & Space(15), 15)
ss = ss & "EGP  "
ss = ss & Left(txtHTC & Space(20), 20)
ss = ss & Space(25)
ss = ss & Space(8)
ss = Format(Len(ss), "0000") & ss
Winsock1.SendData ss

End Sub

Private Sub Command8_Click()
Dim ss As String

ss = Left(txtatm & Space(5), 5)
ss = ss & Left("TUB" & Space(5), 5)
ss = ss & Left("20" & Space(10), 10)
ss = ss & "22"
ss = ss & Space(5)
ss = ss & Format(Now, "yyyy/MM/dd")
ss = ss & Format(Now, "HH:mm:ss")
ss = ss & "1020304050"
ss = ss & "0020101010101       "
ss = ss & Left(Text2 & Space(10), 10)
ss = ss & "0020121314151       "
ss = ss & Left(Text3 & Space(10), 10)
ss = ss & Left("1000" & Space(15), 15)
ss = ss & "EGP  "
ss = ss & Left(txtHTC & Space(20), 20)
ss = ss & Space(25)
ss = ss & Space(8)
ss = Format(Len(ss), "0000") & ss
Winsock1.SendData ss

End Sub

Private Sub Command9_Click()
Dim ss As String

ss = Left(txtatm & Space(5), 5)
ss = ss & Left("TUB" & Space(5), 5)
ss = ss & Left("20" & Space(10), 10)
ss = ss & "13"
ss = ss & Space(5)
ss = ss & Format(Now, "yyyy/MM/dd")
ss = ss & Format(Now, "HH:mm:ss")
ss = ss & "1020304050"
ss = ss & "0020101010101       "
ss = ss & Left(Text2 & Space(10), 10)
ss = ss & "0020121314151       "
ss = ss & Left(Text3 & Space(10), 10)
ss = ss & Left("1000" & Space(15), 15)
ss = ss & "EGP  "
ss = ss & Left(txtHTC & Space(20), 20)
ss = ss & Space(25)
ss = ss & Space(8)
ss = Format(Len(ss), "0000") & ss
Winsock1.SendData ss

End Sub

Private Sub Winsock1_Close()
lblStatus.Caption = "closed"
End Sub

Private Sub Winsock1_Connect()
lblStatus.Caption = "Connected"
End Sub

Private Sub Winsock1_DataArrival(ByVal bytesTotal As Long)
Dim dd As String
Dim reqtype As String


Winsock1.GetData dd

Text1.Text = dd
reqtype = Mid(dd, 25, 2)
If reqtype = "01" Then
    trxCode = Mid(dd, 140, 20)
    txtHTC.Text = trxCode
End If
End Sub

