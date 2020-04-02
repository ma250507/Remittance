Imports System.Security.Cryptography.X509Certificates
Imports System.Security.Cryptography
Imports System.Text

Public Class DigitalSignature
    Private Function MyAscii(ByVal hexstr As String, ByRef asciiBuff() As Byte) As Integer
        Dim ll As Integer
        Dim strstr As String = ""
        Dim i As Integer
        Dim j As Integer

        Try

            If hexstr.Length Mod 2 <> 0 Then
                loge("HexData has an odd length, can not be decrypted", EventLogEntryType.Error)
                Return 1
            End If
            ll = hexstr.Length \ 2
            ReDim asciiBuff(0 To ll - 1)
            j = 0
            For i = 0 To hexstr.Length - 1 Step 2

                strstr = (hexstr.Substring(i, 2))
                asciiBuff(j) = Integer.Parse(strstr, Globalization.NumberStyles.HexNumber)
                j += 1
            Next

            Return 0
        Catch ex As Exception
            loge("MyAscii, Exc:" & ex.ToString, EventLogEntryType.Error)
            Return 9
        End Try

    End Function

    Private Function MyHex(ByVal b() As Byte) As String
        Dim x As Byte
        Dim x1 As Byte
        Dim x2 As Byte
        Dim strstr As String = ""
        Dim i As Integer
        Try
            For i = 0 To b.Length - 1
                x = b(i)
                x1 = x And &HF
                x2 = (x And &HF0) >> 4
                strstr += (x2).ToString("X") & (x1).ToString("X")
            Next
            Return (strstr)

        Catch ex As Exception
            loge("MyHex, Exc:" & ex.ToString, EventLogEntryType.Error)
            Return ""
        End Try

    End Function
    Public Function Encrypt(ByVal TextData As String, ByRef EncData As String, ByVal SignatureSubject As String) As Integer

        Dim tstStore As System.Security.Cryptography.X509Certificates.X509Store
        Dim certs As X509Certificate2Collection
        Dim certpk As X509Certificate2
        Dim SelectedCertificate As X509Certificate2
        Dim buffer() As Byte
        Dim EncBuffer() As Byte
        Dim EncStr As String
        Dim pbKey As RSACryptoServiceProvider
        Dim mvTextData As String
        Dim TextDataPacketsCount As Integer
        Dim i As Integer

        Try
            tstStore = New X509Store(StoreName.My, StoreLocation.LocalMachine)
            tstStore.Open(OpenFlags.OpenExistingOnly)

            certs = tstStore.Certificates
            SelectedCertificate = Nothing
            For Each certpk In certs
                If certpk.Subject.Contains(SignatureSubject) = True Then
                    SelectedCertificate = certpk
                    Exit For
                End If


            Next

            If SelectedCertificate Is Nothing Then
                EncData = ""
                loge("Can not Find Certificate with Subject [" & SignatureSubject & "]", EventLogEntryType.Error)
                Return 1
            End If
            If SelectedCertificate.PublicKey Is Nothing Then
                EncData = ""
                loge("Can not Find Public Key within Certificate with Subject [" & SignatureSubject & "]", EventLogEntryType.Error)
                Return 2

            End If
            If SelectedCertificate.PublicKey.Key Is Nothing Then
                EncData = ""
                loge("Can not Find Public Key (Key) within Certificate with Subject [" & SignatureSubject & "]", EventLogEntryType.Error)
                Return 3

            End If
            pbKey = SelectedCertificate.PublicKey.Key
            TextDataPacketsCount = TextData.Length / 100
            If TextData.Length > TextDataPacketsCount * 100 Then
                TextDataPacketsCount += 1
            End If

            EncStr = ""
            For i = 0 To TextDataPacketsCount - 1
                If i * 100 + 100 < TextData.Length Then
                    mvTextData = TextData.Substring(i * 100, 100)
                Else
                    mvTextData = TextData.Substring(i * 100)
                End If

                buffer = Encoding.ASCII.GetBytes(mvTextData)
                EncBuffer = pbKey.Encrypt(buffer, False)
                EncStr += MyHex(EncBuffer)


            Next

            EncData = EncStr
            
            Return 0

        Catch ex As Exception
            loge("Digitally Signing, Encrypt, Exc:" & ex.ToString, EventLogEntryType.Error)
            Return 9
        End Try

    End Function
    Public Function Decrypt(ByRef TextData As String, ByVal EncData As String, ByVal SignatureSubject As String) As Integer

        Dim tstStore As System.Security.Cryptography.X509Certificates.X509Store
        Dim certs As X509Certificate2Collection
        Dim cert As X509Certificate2
        Dim SelectedCertificate As X509Certificate2
        Dim buffer() As Byte
        Dim EncBuffer(0) As Byte
        Dim mvPacketscount As Integer
        Dim pvKey As RSACryptoServiceProvider
        Dim rtrn As Integer
        Dim mvtextdata As String
        Dim mvEncdata As String
        Dim pvKeyXML As String
        Dim i As Integer

        Try
            tstStore = New X509Store(StoreName.My, StoreLocation.LocalMachine)
            tstStore.Open(OpenFlags.OpenExistingOnly)

            certs = tstStore.Certificates
            SelectedCertificate = Nothing
            For Each cert In certs
                If cert.Subject.Contains(SignatureSubject) = True Then
                    SelectedCertificate = cert
                    Exit For
                End If


            Next

            


            If SelectedCertificate Is Nothing Then
                EncData = ""
                loge("Can not Find Certificate with Subject [" & SignatureSubject & "]", EventLogEntryType.Error)
                Return 1
            End If

            If SelectedCertificate.PrivateKey Is Nothing Then

                loge("Can not Find Private Key within Certificate with Subject [" & SignatureSubject & "]", EventLogEntryType.Error)
                Return 2

            End If
            pvKeyXML = SelectedCertificate.PrivateKey.ToXmlString(True)
            pvKey = New RSACryptoServiceProvider
            pvKey.FromXmlString(pvKeyXML)
            mvtextdata = ""


            mvPacketscount = EncData.Length / 256
            If EncData.Length > mvPacketscount * 256 Then
                mvPacketscount += 1
            End If
            For i = 0 To mvPacketscount - 1
                If EncData.Length > i * 256 + 256 Then
                    mvEncdata = EncData.Substring(i * 256, 256)
                Else
                    mvEncdata = EncData.Substring(i * 256)
                End If

                rtrn = MyAscii(mvEncdata, EncBuffer)
                If rtrn <> 0 Then
                    Return 2
                End If
                buffer = pvKey.Decrypt(EncBuffer, False)
                mvtextdata += Encoding.ASCII.GetString(buffer)

            Next
            

            TextData = mvtextdata
            Return 0

        Catch ex As Exception
            loge("Digitally Signing, Decrypt, Exc:" & ex.ToString, EventLogEntryType.Error)
            Return 9
        End Try

    End Function
    Public Function Sign(ByVal TextData As String, ByRef SignedData As String, ByVal SignatureSubject As String) As Integer

        Dim tstStore As System.Security.Cryptography.X509Certificates.X509Store
        Dim certs As X509Certificate2Collection
        Dim cert As X509Certificate2
        Dim SelectedCertificate As X509Certificate2
        Dim buffer() As Byte
        Dim signedBuffer(0) As Byte
        Dim pvKey As RSACryptoServiceProvider
        Dim rtrn As Integer
        Dim pvKeyXML As String

        Try
            tstStore = New X509Store(StoreName.My, StoreLocation.LocalMachine)
            tstStore.Open(OpenFlags.OpenExistingOnly)

            certs = tstStore.Certificates
            SelectedCertificate = Nothing
            For Each cert In certs
                If cert.Subject.Contains(SignatureSubject) = True Then
                    SelectedCertificate = cert
                End If


            Next

            If SelectedCertificate Is Nothing Then
                loge("Can not Find Certificate with Subject [" & SignatureSubject & "]", EventLogEntryType.Error)
                Return 1
            End If

            If SelectedCertificate.PrivateKey Is Nothing Then

                loge("Can not Find Private Key within Certificate with Subject [" & SignatureSubject & "]", EventLogEntryType.Error)
                Return 2

            End If
            buffer = Encoding.ASCII.GetBytes(TextData)

            pvKeyXML = SelectedCertificate.PrivateKey.ToXmlString(True)
            pvKey = New RSACryptoServiceProvider
            pvKey.FromXmlString(pvKeyXML)


            signedBuffer = pvKey.SignData(buffer, New SHA1Managed)

            SignedData = MyHex(signedBuffer)

            Return 0

        Catch ex As Exception
            loge("Digitally Signing, Sign, Exc:" & ex.ToString, EventLogEntryType.Error)
            Return 9
        End Try

    End Function
    Public Function Verify(ByVal TextData As String, ByRef SignedData As String, ByVal SignatureSubject As String) As Integer

        Dim tstStore As System.Security.Cryptography.X509Certificates.X509Store
        Dim certs As X509Certificate2Collection
        Dim cert As X509Certificate2
        Dim SelectedCertificate As X509Certificate2
        Dim buffer() As Byte
        Dim signedBuffer(0) As Byte
        Dim pbKey As RSACryptoServiceProvider
        Dim rtrn As Integer
        Dim rtrnb As Boolean
        Try
            tstStore = New X509Store(StoreName.My, StoreLocation.LocalMachine)
            tstStore.Open(OpenFlags.OpenExistingOnly)

            certs = tstStore.Certificates
            SelectedCertificate = Nothing
            For Each cert In certs
                If cert.Subject.Contains(SignatureSubject) = True Then
                    SelectedCertificate = cert
                End If


            Next

            If SelectedCertificate Is Nothing Then

                loge("Can not Find Certificate with Subject [" & SignatureSubject & "]", EventLogEntryType.Error)
                Return 1
            End If

            If SelectedCertificate.PublicKey Is Nothing Then

                loge("Can not Find Public Key within Certificate with Subject [" & SignatureSubject & "]", EventLogEntryType.Error)
                Return 2

            End If
            If SelectedCertificate.PublicKey.Key Is Nothing Then

                loge("Can not Find Public Key (Key) within Certificate with Subject [" & SignatureSubject & "]", EventLogEntryType.Error)
                Return 2

            End If

            pbKey = SelectedCertificate.PublicKey.Key

            
            rtrn = MyAscii(SignedData, signedBuffer)
            buffer = Encoding.ASCII.GetBytes(TextData)
            rtrnb = pbKey.VerifyData(buffer, New SHA1Managed, signedBuffer)
            If rtrnb = True Then
                Return 0
            Else
                loge("Verify , VerifyData return false,", EventLogEntryType.Error)
                Return 3
            End If

        Catch ex As Exception
            loge("Digitally Signing, Verify, Exc:" & ex.ToString, EventLogEntryType.Error)
            Return 9
        End Try

    End Function

    Private Sub loge(ByVal Data As String, ByVal eventType As System.Diagnostics.EventLogEntryType)
        Try
            If Not System.Diagnostics.EventLog.SourceExists("NCRMoneyFer Service,Digital Signing") Then
                System.Diagnostics.EventLog.CreateEventSource("NCRMmoneyfer Service,Digital Signing", "Application")
            End If
            Dim ev As New System.Diagnostics.EventLog
            ev.Source = "NCRMoneyfer Service,Digital Signing"
            ev.WriteEntry(Data, eventType)

        Catch ex As Exception
        End Try
    End Sub

End Class
