'MODEM IMPORTS ================
Imports GsmComm.GsmCommunication
Imports GsmComm.PduConverter
''===============================


Public Class SMSSendingClass
    Private WithEvents w As System.Windows.Forms.WebBrowser
    Private DocCompleteFlag As Boolean
    Public BasicURL As String = "http://10.0.0.201/PreSMS/BulkHttp.aspx"
    Public Status As String

    Private ubsmsws As UBSMSWS.SendSMS2


    'MODEM DECLARATION
    Public PortIsOpened As Boolean
    Private cmm As GsmComm.GsmCommunication.GsmCommMain
    '=====================================================

    Public Function ReformatMobileNumber(ByVal pNumber As String) As String

        Dim mvPreFix As String
        Try
            mvPreFix = pNumber.Substring(0, 3)
            If mvPreFix = "015" Then
                mvPreFix = pNumber.Substring(0, 4)
            End If
            Select Case mvPreFix
                Case "010", "016", "019", "0151"
                    Return pNumber
                Case Else
                    Return "2" & pNumber
            End Select
        Catch ex As Exception
            Status += vbNewLine & "Reformat Number Ex:" & ex.ToString
            Return pNumber

        End Try




    End Function
    Public Function SendSMS(ByVal Provider As String, ByVal Type As String, ByVal MSGTEXT As String, ByVal SenderMobile As String, ByVal RecepientMobile As String, ByVal Binary As String, ByVal TimeOut As Integer, ByRef MsgId As String, ByRef SendingStatus As String) As Integer
        Dim rtrn As Integer
        Select Case NCRMoneyFerCustomerAlertService.SMSAlertingService
            Case 1
                rtrn = SendSMS_Modem(Provider, Type, MSGTEXT, SenderMobile, RecepientMobile, Binary, TimeOut, MsgId, SendingStatus)
            Case 2
                'rtrn = SendSMSUB(Provider, Type, MSGTEXT, SenderMobile, RecepientMobile, Binary, TimeOut, MsgId, SendingStatus)
                rtrn = SendSMSUBWS(MSGTEXT, RecepientMobile, TimeOut, SendingStatus)
            Case 3
                rtrn = SendSMSNBE(Provider, Type, MSGTEXT, SenderMobile, RecepientMobile, Binary, TimeOut, MsgId, SendingStatus)
            Case 4
                rtrn = SendSMS_CIB(MSGTEXT, RecepientMobile, SendingStatus)
            Case 5
                rtrn = SendSMS_BanqueMisr(MSGTEXT, RecepientMobile, SendingStatus)
            Case 6
                rtrn = SendSMS_UniBank(MSGTEXT, RecepientMobile, SendingStatus)
            Case Else
                Status = "SMSAlertingService =" & NCRMoneyFerCustomerAlertService.SMSAlertingService & " not supported"
                rtrn = 9
        End Select
        Return rtrn
      
    End Function

    Public Function SendSMS_Modem(ByVal Provider As String, ByVal Type As String, ByVal MSGTEXT As String, ByVal SenderMobile As String, ByVal RecepientMobile As String, ByVal Binary As String, ByVal PTimeOut As Integer, ByRef MsgId As String, ByRef SendingStatus As String) As Integer
        Dim pdu As GsmComm.PduConverter.SmsSubmitPdu
        Dim port As Integer = 5
        Dim baudrate As Integer = 9600
        Dim timeout As Integer = 300 'ms
        Dim GSMPin As String = "0000"

        Try
            NCRMoneyFerCustomerAlertService.loglog("SendSMS_Modem, Binary=[" & Binary & "]", False)

            If Not PortIsOpened Then
                Try
                    cmm.Close()
                Catch ex As Exception

                End Try


                cmm = Nothing
                port = NCRMoneyFerCustomerAlertService.SMSModemPortNumber ''  6 '' CustomerAlertService.GSMModemPort
                baudrate = 9600
                ''timeout = PTimeOut
                cmm = New GsmCommMain(port, baudrate, timeout)
                cmm.Open()
                PortIsOpened = True
            End If

            pdu = New SmsSubmitPdu(MSGTEXT, RecepientMobile, "")
            cmm.SendMessage(pdu)
            'cmm.Close()
            Status += "Sending_Modem, Send complete"
            Return 0
        Catch ex As Exception
            Status += vbNewLine + "Sending_Modem SMS Exception :" & ex.ToString
            Try
                Try
                    cmm.Close()

                Catch ex1 As Exception

                End Try
                cmm = Nothing
                PortIsOpened = False
                Return 1
            Catch ex2 As Exception
                Return 2
            End Try
        End Try


    End Function
    Private Function GetUCString(ByVal DataStr As String) As String
        Dim i As Integer
        Dim buf() As Byte
        Dim tmp As String = ""
        Try
            buf = System.Text.Encoding.Unicode.GetBytes(DataStr)
            For i = 0 To buf.Length - 1 Step 2
                tmp += buf(i + 1).ToString("X2") & buf(i).ToString("X2")

            Next
            Return tmp
        Catch ex As Exception
            Return ""
        End Try
    End Function
    
    Public Function SendSMSUB(ByVal Provider As String, ByVal Type As String, ByVal MSGTEXT As String, ByVal SenderMobile As String, ByVal RecepientMobile As String, ByVal Binary As String, ByVal TimeOut As Integer, ByRef MsgId As String, ByRef SendingStatus As String) As Integer
        Dim finalURL As String
        Dim ts As Date
        Dim TSpan As TimeSpan
        Dim lastPos As Integer
        Dim AllresponseText As String
        Dim comaLastPos As Integer
        Dim ComaPos As Integer
        Dim reformatedReceipienntnumber As String = ""
        Dim lRecepient As String
        Dim pMSGTEXT As String = ""

        Try

            NCRMoneyFerCustomerAlertService.loglog("SendSMSUB, Binary=[" & Binary & "]", False)

            If Binary = "08" Then
                pMSGTEXT = GetUCString(MSGTEXT)
            Else
                pMSGTEXT = MSGTEXT
            End If
            Status = ""
            lRecepient = RecepientMobile
            reformatedReceipienntnumber = ReformatMobileNumber(lRecepient)
            If w Is Nothing Then
                w = New System.Windows.Forms.WebBrowser
            End If


            finalURL = BasicURL & "?Sender=" & SenderMobile & "&Number=" & reformatedReceipienntnumber
            finalURL += "&Binary=" & Binary & "&Provider=" & Provider
            finalURL += "&Code=" & pMSGTEXT & "&Type=" & Type

            NCRMoneyFerCustomerAlertService.loglog("SendSMSUB will try URL=[" & finalURL & "]", False)
            Status += vbNewLine & "SendSMSUB will try URL=[" & finalURL & "]"
            w.Navigate(finalURL)
            DocCompleteFlag = False
            ts = Now
            While DocCompleteFlag = False
                System.Threading.Thread.Sleep(1000)
                System.Windows.Forms.Application.DoEvents()
                TSpan = Now.Subtract(ts)
                If TSpan.TotalSeconds >= TimeOut Then
                    Status += vbNewLine & "SendSMSUB,Timeout..."
                    w = Nothing
                    Return 1
                End If
            End While
            Status += vbNewLine & "SendSMSUB,Doc Complete StatusText=[" & w.StatusText & "] DocumentText=[" & w.DocumentText & "]"
            If w.StatusText.ToUpper <> "DONE" Then
                Status += vbNewLine & "SendSMSUB,Bad Navigation Status [" & w.StatusText & "]"
                w = Nothing
                Return 2
            End If

            lastPos = w.DocumentText.IndexOf("<!")
            If lastPos < 2 Then
                Status += vbNewLine & "SendSMSUB,Bad response [" & w.DocumentText & "]"
                w = Nothing
                Return 3

            End If

            Try

                comaLastPos = -1

                AllresponseText = w.DocumentText.Substring(0, lastPos - 1)
                lastPos = AllresponseText.IndexOf("MessageID")
                If lastPos < 0 Then
                    Status += vbNewLine & "SendSMSUB,Bad response no MessageID tag [" & w.DocumentText & "]"
                    w = Nothing
                    Return 4
                End If
                ComaPos = AllresponseText.IndexOf(",", comaLastPos + 1)
                MsgId = AllresponseText.Substring(lastPos + 10, ComaPos - lastPos - 10)


                comaLastPos = ComaPos


                lastPos = AllresponseText.IndexOf("Status")
                If lastPos < 0 Then
                    Status += vbNewLine & "SendSMSUB,Bad response no Status tag [" & w.DocumentText & "]"
                    w = Nothing
                    Return 6
                End If
                ComaPos = AllresponseText.IndexOf(",", lastPos + 1)
                SendingStatus = AllresponseText.Substring(lastPos + 7, ComaPos - lastPos - 7)

                Status += vbNewLine & "SendSMSUB, Message Sent Successfully MessageID=[" & MsgId & "] Status=[" & SendingStatus & "]"
                w = Nothing
                Return 0



            Catch ex As Exception
                Status += vbNewLine & "SendSMSUB,Error parsing response ex:[" & ex.ToString & "]"
                w = Nothing
                Return 5

            End Try





        Catch ex As Exception
            Status += vbNewLine & "SendSMSUB Exception:" & ex.ToString
            w = Nothing
            Return 9
        End Try




    End Function
    Public Function SendSMSUBWS(ByVal MSGTEXT As String, ByVal RecepientMobile As String, ByVal TimeOut As Integer, ByRef SendingStatus As String) As Integer


        Dim lRecepient As String = ""
        Dim pMSGTEXT As String = ""
        Dim rtrnstr As String


        Try

            pMSGTEXT = MSGTEXT
            Status = ""
            lRecepient = RecepientMobile

            If ubsmsws Is Nothing Then
                ubsmsws = New UBSMSWS.SendSMS2

            End If


            ubsmsws.Url = NCRMoneyFerCustomerAlertService.SMSServiceBasicURL

            NCRMoneyFerCustomerAlertService.loglog("SendSMSUB will try URL=[" & ubsmsws.Url & "] User=[" & NCRMoneyFerCustomerAlertService.SMSUBUser & "] PWD=[" & NCRMoneyFerCustomerAlertService.SMSUBPWD & "] Priority=[" & NCRMoneyFerCustomerAlertService.SMSUBPriority & "]", False)
            Status += vbNewLine & "SendSMSUBWS will try URL=[" & ubsmsws.Url & "]"

            rtrnstr = ubsmsws.Send_SMS(lRecepient, pMSGTEXT, "", "", "", NCRMoneyFerCustomerAlertService.SMSUBUser, NCRMoneyFerCustomerAlertService.SMSUBPWD, NCRMoneyFerCustomerAlertService.SMSUBPriority)

            If rtrnstr.ToUpper <> "SUCCESS" Then
                Status += vbNewLine & "SendSMSUBWS,Bad response Status [" & rtrnstr & "]"
                ubsmsws = Nothing
                Return 2
            End If


            SendingStatus = "4"
            Status += vbNewLine & "SendSMSUBWS, Message Sent Successfully Mobile=[" & lRecepient & "] Status=[" & rtrnstr & "] Body =[" & pMSGTEXT & "]"
            ubsmsws = Nothing
            Return 0







        Catch ex As Exception
            Status += vbNewLine & "SendSMSUBWS Exception:" & ex.ToString
            ubsmsws = Nothing
            Return 9
        End Try




    End Function

    Public Function SendSMSNBE(ByVal Provider As String, ByVal Type As String, ByVal MSGTEXT As String, ByVal SenderMobile As String, ByVal RecepientMobile As String, ByVal Binary As String, ByVal TimeOut As Integer, ByRef MsgId As String, ByRef SendingStatus As String) As Integer

        Dim smsobj As New NBESMSWebservice.SMSService

        Dim userName As String
        Dim pASSWORD As String
        Dim channel As String
        Dim language As String
        Dim mode As String
        Dim receiverMobile As String
        Dim Body As String

        Dim transDate As Date
        Dim p1, p2, p3, p4, p5 As String
        Dim ret As String


        Try

            NCRMoneyFerCustomerAlertService.loglog("SendSMSNBE, Binary=[" & Binary & "]", False)

            userName = NCRMoneyFerCustomerAlertService.SMSWebservice_UserId
            pASSWORD = NCRMoneyFerCustomerAlertService.SMSWebservice_Password

            channel = NCRMoneyFerCustomerAlertService.SMSWebservice_Channel
            If Binary = "08" Then
                language = "EN"
            Else
                language = "AR"
            End If

            mode = NCRMoneyFerCustomerAlertService.SMSWebservice_Mode
            receiverMobile = RecepientMobile
            Body = MSGTEXT
            transDate = Now

            p1 = ""
            p2 = ""
            p3 = ""
            p4 = ""
            p5 = ""

            Dim ParamList() As String = {Body}

            smsobj.Url = NCRMoneyFerCustomerAlertService.SMSWebservice_URL
            ret = smsobj.SendSMS(userName, pASSWORD, channel, language, mode, receiverMobile, ParamList, transDate, p1, p2, p3, p4, p5)

            Status += vbNewLine & "SendSMSNBE Ret:" & "RET=[" & ret & "]"
            NCRMoneyFerCustomerAlertService.loglog("SendSMSNBE, Servivce sending return =[" & ret & "] ", False)
            If ret.ToUpper() = "Success".ToUpper Then
                SendingStatus = 4
                Return 0
            Else
                Return 1
            End If

        Catch ex As Exception
            Status += vbNewLine & "SendSMSNBE Exception:" & ex.ToString
            Return 9
        End Try





    End Function

    Public Function SendSMS_CIB(ByVal MSGTEXT As String, ByVal RecepientMobile As String, ByRef SendingStatus As String) As Integer



        Dim c As New System.Data.SqlClient.SqlConnection
        Dim cTrxm As New System.Data.SqlClient.SqlConnection
        Dim cm As New System.Data.SqlClient.SqlCommand
        Dim q As String
        Dim raffected As Integer = 0
        Dim pSMSSendStatus As String = ""
        Try

            c.ConnectionString = NCRMoneyFerCustomerAlertService.SMSServiceDatabaseConnectionString
            c.Open()
            cm.Connection = c
            cm.CommandType = CommandType.Text
            q = " Insert into Data (Camp_id, Data, Inserted_Time, Message) "
            q += " values ('00000000-0000-0000-0000-0000000000000000','" & RecepientMobile & "',getdate(),'" & MSGTEXT & "')"

            cm.CommandText = q
            raffected = cm.ExecuteNonQuery()
            NCRMoneyFerCustomerAlertService.loglog("SendSMS_CIB,rows affected=[" & raffected & "] for updating trx q=[" & q & "]", False)

            
            Try
                c.Close()
            Catch ex As Exception
            End Try

            If raffected < 1 Then
                Return 1
            End If

            Return 0




        Catch ex As Exception
            NCRMoneyFerCustomerAlertService.loglog("Error SendSMS_CIB q=[" & q & "] ex:" & ex.ToString, False)

            Try
                c.Close()
            Catch ex2 As Exception
            End Try
            Return 9
        End Try




    End Function

    Public Function SendSMS_BanqueMisr(ByVal MSGTEXT As String, ByVal RecepientMobile As String, ByRef SendingStatus As String) As Integer
        Dim client As New Net.WebClient
        Dim MsgSTR As String
        Dim URLStr As String
        Dim STR As String
        Try
            MsgSTR = MSGTEXT.Replace(" ", "%20")
            MsgSTR = MsgSTR.Replace("&", "%26")
            URLStr = "http://" & NCRMoneyFerCustomerAlertService.HTTPIP & ":" & NCRMoneyFerCustomerAlertService.HTTPPort & "/?PhoneNumber=2" & RecepientMobile & Chr(38) & "Sender=" & NCRMoneyFerCustomerAlertService.HTTPSender & Chr(38) & "Text=" & MsgSTR & Chr(38) & "user=" & NCRMoneyFerCustomerAlertService.HTTPUsername & Chr(38) & "password=" & NCRMoneyFerCustomerAlertService.HTTPPassword
            NCRMoneyFerCustomerAlertService.loglog("URL : " & URLStr, True)
            client.Proxy = Net.WebRequest.GetSystemWebProxy
            STR = client.DownloadString(URLStr)
            SendingStatus = "4"
            Return 0
        Catch ex As Exception
            NCRMoneyFerCustomerAlertService.loglog("SEND BanqueMisr error ex =" & ex.ToString, True)
            Return 9
        End Try
        

    End Function
    Public Function SendSMS_UniBank(ByVal MSGTEXT As String, ByVal RecepientMobile As String, ByRef SendingStatus As String) As Integer
        Dim client As New Net.WebClient
        Dim MsgSTR As String
        Dim URLStr As String
        Dim STR As String
        Try
            MsgSTR = MSGTEXT.Replace(" ", "%20")
            MsgSTR = MsgSTR.Replace("&", "%26")
            URLStr = NCRMoneyFerCustomerAlertService.SMSHTTPUNIBank_URL & "?"
            URLStr = URLStr & "From=" & NCRMoneyFerCustomerAlertService.SMSHTTPUNIBank_From & "&"
            URLStr = URLStr & "To=233" & RecepientMobile.Substring(1, RecepientMobile.Length - 1) & "&"
            URLStr = URLStr & "Content=" & MSGTEXT & "&"
            URLStr = URLStr & "ClientReference=" & NCRMoneyFerCustomerAlertService.SMSHTTPUNIBank_ClientReference & "&"
            URLStr = URLStr & "ClientId=" & NCRMoneyFerCustomerAlertService.SMSHTTPUNIBank_ClientID & "&"
            URLStr = URLStr & "ClientSecret=" & NCRMoneyFerCustomerAlertService.SMSHTTPUNIBank_ClientSecret & "&"
            URLStr = URLStr & "RegisteredDelivery=" & NCRMoneyFerCustomerAlertService.SMSHTTPUNIBank_RegisteredDelivery

            NCRMoneyFerCustomerAlertService.loglog("URL : " & URLStr, True)
            client.Proxy = Net.WebRequest.GetSystemWebProxy
            STR = client.DownloadString(URLStr)
            SendingStatus = "4"
            Return 0
        Catch ex As Exception
            NCRMoneyFerCustomerAlertService.loglog("SEND UNIBANK error ex =" & ex.ToString, True)
            Return 9
        End Try


    End Function




    Private Sub w_DocumentCompleted(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles w.DocumentCompleted
        DocCompleteFlag = True
    End Sub
End Class
