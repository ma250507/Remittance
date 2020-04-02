VERSION 5.00
Begin VB.UserControl NCRDigitalSignerOCX 
   ClientHeight    =   720
   ClientLeft      =   0
   ClientTop       =   0
   ClientWidth     =   780
   ScaleHeight     =   720
   ScaleWidth      =   780
End
Attribute VB_Name = "NCRDigitalSignerOCX"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Public LastReturn As Integer
Public Function Encrypt(ByVal TextData As String, ByVal CertificateSubject As String) As String

Dim nd As New NCRDigitalSigner.DigitalSignature
Dim ss As String
Dim dd As String
Dim rtrn As Integer
dd = ""
ss = TextData
 LastReturn = -1
If ss = "" Then
  
   Encrypt = ""
   LastReturn = 11
  Exit Function
End If
rtrn = nd.Encrypt(ss, dd, CertificateSubject)

If rtrn = 0 Then
   
    Encrypt = dd
Else
    
    Encrypt = ""
End If
LastReturn = rtrn


End Function
Public Function Decrypt(ByVal EncryptedData As String, ByVal CertificateSubject As String) As String

Dim nd As New NCRDigitalSigner.DigitalSignature
Dim ss As String
Dim dd As String
Dim rtrn As Integer
dd = EncryptedData
ss = ""
LastReturn = -1
If dd = "" Then
   LastReturn = 11
   Decrypt = ""
  Exit Function
End If
rtrn = nd.Decrypt(ss, dd, CertificateSubject)

If rtrn = 0 Then
    Decrypt = ss
Else
   Decrypt = ""
End If
LastReturn = rtrn


End Function
Public Function Sign(ByVal TextData As String, ByVal CertificateSubject As String) As String

Dim nd As New NCRDigitalSigner.DigitalSignature
Dim ss As String
Dim dd As String
Dim rtrn As Integer
dd = ""
ss = TextData
      LastReturn = -1
If ss = "" Then
   
   Sign = ""
   LastReturn = 11
  Exit Function
End If
rtrn = nd.Sign(ss, dd, CertificateSubject)

If rtrn = 0 Then
    Sign = dd
 Else
   Sign = ""
End If
LastReturn = rtrn


End Function
Public Function Verify(ByVal TextData As String, ByVal SignedData As String, ByVal CertificateSubject As String) As Integer

Dim nd As New NCRDigitalSigner.DigitalSignature
Dim ss As String
Dim dd As String
Dim rtrn As Integer
dd = SignedData
ss = TextData
      
If ss = "" Or dd = "" Then
   
   Verify = 11
  Exit Function
End If
rtrn = nd.Verify(ss, dd, CertificateSubject)

Verify = rtrn


End Function

Private Sub UserControl_Initialize()

End Sub
