VERSION 5.00
Begin VB.Form Form1 
   Caption         =   "Test NCR Digital Signer"
   ClientHeight    =   5985
   ClientLeft      =   60
   ClientTop       =   450
   ClientWidth     =   8460
   LinkTopic       =   "Form1"
   ScaleHeight     =   5985
   ScaleWidth      =   8460
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton cmdUsingOCX 
      Caption         =   "Test Using OCX ..."
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   178
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   6960
      TabIndex        =   15
      Top             =   5160
      Width           =   1455
   End
   Begin VB.CommandButton cmdVerify 
      Caption         =   "Verify ( Need Public Key)"
      Height          =   495
      Left            =   6240
      TabIndex        =   14
      Top             =   4320
      Width           =   2175
   End
   Begin VB.TextBox txtSignature 
      Height          =   1815
      Left            =   960
      Locked          =   -1  'True
      MultiLine       =   -1  'True
      ScrollBars      =   2  'Vertical
      TabIndex        =   12
      Top             =   3720
      Width           =   5175
   End
   Begin VB.CommandButton cmdSignData 
      Caption         =   "Sign ( Need Private Key)"
      Height          =   495
      Left            =   6240
      TabIndex        =   11
      Top             =   3720
      Width           =   2175
   End
   Begin VB.TextBox txtCertificateSubject 
      Height          =   375
      Left            =   960
      TabIndex        =   9
      Text            =   "NCRMoneyFerSystem"
      Top             =   120
      Width           =   2535
   End
   Begin VB.TextBox txtDecryptedData 
      Height          =   495
      Left            =   960
      TabIndex        =   6
      Top             =   3120
      Width           =   5175
   End
   Begin VB.CommandButton cmdDecrypt 
      Caption         =   "Decrypt ( Need Private Key)"
      Height          =   495
      Left            =   6240
      TabIndex        =   5
      Top             =   1200
      Width           =   2175
   End
   Begin VB.TextBox txtEncryptedData 
      Height          =   1815
      Left            =   960
      Locked          =   -1  'True
      MultiLine       =   -1  'True
      ScrollBars      =   2  'Vertical
      TabIndex        =   3
      Top             =   1200
      Width           =   5175
   End
   Begin VB.TextBox txtTextdata 
      Height          =   495
      Left            =   960
      TabIndex        =   1
      Text            =   "write the data you want to encrypt"
      Top             =   600
      Width           =   5175
   End
   Begin VB.CommandButton cmdEncrypt 
      Caption         =   "Encrypt ( Need Public Key)"
      Height          =   495
      Left            =   6240
      TabIndex        =   0
      Top             =   600
      Width           =   2175
   End
   Begin VB.Label Label5 
      Caption         =   "Signature"
      Height          =   495
      Left            =   120
      TabIndex        =   13
      Top             =   3720
      Width           =   735
   End
   Begin VB.Label Label4 
      Caption         =   "Certificate Subject"
      Height          =   495
      Left            =   120
      TabIndex        =   10
      Top             =   120
      Width           =   735
   End
   Begin VB.Label lblStatus 
      BackColor       =   &H0080FFFF&
      BorderStyle     =   1  'Fixed Single
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   9.75
         Charset         =   178
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H008080FF&
      Height          =   375
      Left            =   0
      TabIndex        =   8
      Top             =   5640
      Width           =   8415
   End
   Begin VB.Label Label3 
      Caption         =   "Decrypted Data"
      Height          =   495
      Left            =   120
      TabIndex        =   7
      Top             =   3120
      Width           =   735
   End
   Begin VB.Label Label2 
      Caption         =   "Encrypted Data"
      Height          =   495
      Left            =   120
      TabIndex        =   4
      Top             =   1320
      Width           =   735
   End
   Begin VB.Label Label1 
      Caption         =   "Text"
      Height          =   255
      Left            =   360
      TabIndex        =   2
      Top             =   720
      Width           =   375
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Private Sub Command1_Click()
Dim nd As New NCRDigitalSigner.DigitalSignature
Dim ss As String
Dim dd As String
Dim kk As String
Dim rtrn As Integer
kk = ""
  ss = "The Security System could not establish aThe Security System could not establish aThe Security System could not establish aThe Security System could not establish aThe Security System could not establish aThe Security System could not establish a"
      
rtrn = nd.Encrypt(ss, dd, "NCRMoneyFerSystem")
rtrn = nd.decrypt(kk, dd, "NCRMoneyFerSystem")
MsgBox kk
dd = ""
rtrn = nd.sign(ss, dd, "NCRMoneyFerSystem")
rtrn = nd.Verify(ss, dd, "NCRMoneyFerSystem")



End Sub

Private Sub cmdDecrypt_Click()
Dim nd As New NCRDigitalSigner.DigitalSignature
Dim ss As String
Dim dd As String
Dim rtrn As Integer
ss = ""
txtDecryptedData = ""
dd = txtEncryptedData.Text
      
If dd = "" Then
   MsgBox "Please Enter text data to be decrypted", vbCritical
  Exit Sub
End If
rtrn = nd.decrypt(ss, dd, txtCertificateSubject.Text)



If rtrn = 0 Then
   txtDecryptedData.Text = ss
   lblStatus.Caption = "Decrypt.. Success ,Return value =" & rtrn
   Else
   lblStatus.Caption = "Decrypt.. Failure ,Return value =" & rtrn
End If

End Sub

Private Sub cmdEncrypt_Click()
Dim nd As New NCRDigitalSigner.DigitalSignature
Dim ss As String
Dim dd As String
Dim kk As String
Dim rtrn As Integer
kk = ""
txtEncryptedData = ""
ss = txtTextdata.Text
      
If ss = "" Then
   MsgBox "Please Enter text data to be encrypted", vbCritical
  Exit Sub
End If
rtrn = nd.Encrypt(ss, dd, txtCertificateSubject.Text)



If rtrn = 0 Then
   txtEncryptedData.Text = dd
   lblStatus.Caption = "Encrypt.. Success ,Return value =" & rtrn
   Else
   lblStatus.Caption = "Encrypt.. Failure ,Return value =" & rtrn
End If
End Sub

Private Sub cmdSignData_Click()
Dim nd As New NCRDigitalSigner.DigitalSignature
Dim ss As String
Dim dd As String
Dim kk As String
Dim rtrn As Integer
kk = ""
txtSignature = ""
ss = txtTextdata.Text
      
If ss = "" Then
   MsgBox "Please Enter text data to be Signed", vbCritical
  Exit Sub
End If
rtrn = nd.sign(ss, dd, txtCertificateSubject.Text)



If rtrn = 0 Then
   txtSignature.Text = dd
   lblStatus.Caption = "Sign.. Success ,Return value =" & rtrn
   Else
   lblStatus.Caption = "Sign.. Failure ,Return value =" & rtrn
End If

End Sub

Private Sub cmdUsingOCX_Click()
Form2.Show 1
End Sub

Private Sub cmdVerify_Click()
Dim nd As New NCRDigitalSigner.DigitalSignature
Dim ss As String
Dim dd As String
Dim kk As String
Dim rtrn As Integer
kk = ""

ss = txtTextdata.Text
dd = txtSignature.Text
If ss = "" Then
   MsgBox "Please Enter text data to be verified", vbCritical
  Exit Sub
End If
If dd = "" Then
   MsgBox "Please Enter signature data to be verified", vbCritical
  Exit Sub
End If

rtrn = nd.Verify(ss, dd, txtCertificateSubject.Text)



If rtrn = 0 Then
  
   lblStatus.Caption = "Verify.. Success ,Return value =" & rtrn
   Else
   lblStatus.Caption = "Verify.. Failure ,Return value =" & rtrn
End If

End Sub
