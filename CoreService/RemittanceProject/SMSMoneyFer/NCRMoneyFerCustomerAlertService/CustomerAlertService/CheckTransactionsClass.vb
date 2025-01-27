Imports System.Windows.Forms
Imports System.Threading


Public Class CheckTransactionsClass
    Private Trxs As New Collection
    Private ServiceId As String = "CustomerAlert"
    Public PortIsOpened As Boolean
    


    Public Const const_TransactionPhaseDeposit = 1
    Public Const const_TransactionPhaseWithdraw = 2
    Public Const const_TransactionResendSMS = 3
    Public Const const_TransactionResendExpired = 4
    Public Const const_TransactionPhaseRedemption = 5
    Public Const const_TransactionResendSMSExpired = 6
    Public Const const_ResendTypeD = 1
    Public Const const_ResendTypeB = 2
    Public Const const_ResendTypeBoth = 3
    Public Const const_ResendTypeDR = 4






    Public Sub Process()
        Dim ts As Date
        Dim trx As TrxClass
        Dim msgstrb As String = ""
        Dim msgstrd As String = ""


        Dim rtrnb As Integer
        Dim rtrnd As Integer
        Dim SMSSC As SMSSendingClass
        Dim lProvider As String
        Dim lType As String
        Dim lSender As String
        Dim lTimeOut As Integer
        Dim LogMessageSMS As String

        Dim SMSErrorDescription As String
        Try

            NCRMoneyFerCustomerAlertService.loglog("Starting SMS Alerting Service  ", True)
            SMSSC = New SMSSendingClass
            ts = Now
            NCRMoneyFerCustomerAlertService.StopFlag = False
            While NCRMoneyFerCustomerAlertService.StopFlag = False
                Try


                    If Now.Subtract(ts).TotalMinutes >= NCRMoneyFerCustomerAlertService.ClientCheckPointInterval Then

                        Trxs.Clear()
                        GetSMSTransactionsList()
                        NCRMoneyFerCustomerAlertService.loglog("There are " & Trxs.Count & " transactions will start sending SMS for them, AlertingService=[" & NCRMoneyFerCustomerAlertService.SMSAlertingService & "]", True)
                        For Each trx In Trxs
                            
                            If trx.TrxPhase = const_TransactionPhaseDeposit Then
                                msgstrb = trx.GetMessageB_Deposit()
                                msgstrd = trx.GetMessageD_Deposit()
                            ElseIf trx.TrxPhase = const_TransactionResendSMS Then
                                msgstrb = "(" & trx.ResendSMSFlag & ")" & trx.GetMessageB_Deposit()
                                msgstrd = "(" & trx.ResendSMSFlag & ")" & trx.GetMessageD_Deposit()


                            ElseIf trx.TrxPhase = const_TransactionPhaseWithdraw Then
                                msgstrb = trx.GetMessageB_Withdrawal()
                                msgstrd = trx.GetMessageD_Withdrawal()
                            ElseIf trx.TrxPhase = const_TransactionResendExpired Then

                                msgstrd = "(" & trx.ResendSMSFlag & ")" & trx.GetMessageExpiration_D
                                msgstrb = "(" & trx.ResendSMSFlag & ")" & trx.GetMessageExpiration_B
                            ElseIf trx.TrxPhase = const_TransactionPhaseRedemption Then
                                msgstrd = trx.GetMessageD_Redemption

                            ElseIf trx.TrxPhase = const_TransactionResendSMSExpired Then
                                msgstrd = "(" & trx.ResendSMSFlag & ")" & trx.GetMessageExpiration_D
                                msgstrb = "(" & trx.ResendSMSFlag & ")" & trx.GetMessageExpiration_B

                            End If

                            ''''classify sending ......
                            rtrnb = -1
                            rtrnd = -1
                            If trx.TrxPhase = const_TransactionPhaseDeposit _
                               Or trx.TrxPhase = const_TransactionPhaseWithdraw _
                               Or trx.TrxPhase = const_TransactionResendExpired Then
                                SMSSC.BasicURL = NCRMoneyFerCustomerAlertService.SMSServiceBasicURL
                                SMSSC.Status = ""
                                lProvider = NCRMoneyFerCustomerAlertService.SMSServiceProvider
                                lType = "Push"
                                lSender = NCRMoneyFerCustomerAlertService.SMSServiceSenderMobile

                                lTimeOut = NCRMoneyFerCustomerAlertService.SMSSendingTimeOut
                                SMSSC.Status = ""
                                trx.ResendType = const_ResendTypeBoth

                                rtrnb = SMSSC.SendSMS(lProvider, lType, msgstrb, lSender, trx.BeneficiaryMobile, trx.MSGBinary, lTimeOut, trx.SMSMessageId, trx.SMSSendingStatus)
                                If CustomerAlertService.NCRMoneyFerCustomerAlertService.LogLevel > 2 Then
                                    SMSErrorDescription = SMSSC.Status
                                Else
                                    SMSErrorDescription = ""
                                End If
                                If rtrnb = 0 Then
                                    If CustomerAlertService.NCRMoneyFerCustomerAlertService.LogLevel > 2 Then
                                        LogMessageSMS = "Successfully Sending the Message:" & vbNewLine & msgstrb & vbNewLine & " to phone [" & trx.BeneficiaryMobile & "]"
                                    Else
                                        LogMessageSMS = "Successfully Sending the Message:" & vbNewLine & " to phone [" & trx.BeneficiaryMobile & "]"
                                    End If
                                    NCRMoneyFerCustomerAlertService.loglog(LogMessageSMS, False)
                                Else
                                    If CustomerAlertService.NCRMoneyFerCustomerAlertService.LogLevel > 2 Then
                                        LogMessageSMS = "Fail to Send the Message:" & vbNewLine & msgstrb & vbNewLine & "to phone [" & trx.BeneficiaryMobile & "]" & vbNewLine & "SMS description:" & vbNewLine & "[" & SMSErrorDescription & "]" & vbNewLine & "============================================="
                                    Else
                                        LogMessageSMS = "Fail to Send the Message:" & vbNewLine & "to phone [" & trx.BeneficiaryMobile & "]" & vbNewLine & "SMS description:" & vbNewLine & "[" & SMSErrorDescription & "]" & vbNewLine & "============================================="
                                    End If
                                    NCRMoneyFerCustomerAlertService.loglog(LogMessageSMS, True)
                                End If

                                SMSSC.Status = ""
                                Thread.Sleep(1000)
                                rtrnd = SMSSC.SendSMS(lProvider, lType, msgstrd, lSender, trx.DepositorMobile, trx.MSGBinary, lTimeOut, trx.SMSMessageId, trx.SMSSendingStatus)

                                If CustomerAlertService.NCRMoneyFerCustomerAlertService.LogLevel > 2 Then
                                    SMSErrorDescription = SMSSC.Status
                                Else
                                    SMSErrorDescription = ""
                                End If

                                If rtrnd = 0 Then
                                    If CustomerAlertService.NCRMoneyFerCustomerAlertService.LogLevel > 2 Then
                                        LogMessageSMS = "Successfully Sending the Message:" & vbNewLine & msgstrd & vbNewLine & " to phone [" & trx.DepositorMobile & "]"
                                    Else
                                        LogMessageSMS = "Successfully Sending the Message:" & vbNewLine & " to phone [" & trx.DepositorMobile & "]"
                                    End If
                                    NCRMoneyFerCustomerAlertService.loglog(LogMessageSMS, False)
                                Else
                                    If CustomerAlertService.NCRMoneyFerCustomerAlertService.LogLevel > 2 Then
                                        LogMessageSMS = "Fail to Send the Message:" & vbNewLine & msgstrd & vbNewLine & " to phone [" & trx.DepositorMobile & "]" & vbNewLine & "SMS description: " & vbNewLine & "[" & SMSErrorDescription & "]" & vbNewLine & "==========================================="
                                    Else
                                        LogMessageSMS = "Fail to Send the Message:" & vbNewLine & " to phone [" & trx.DepositorMobile & "]" & vbNewLine & "SMS description: " & vbNewLine & "[" & SMSErrorDescription & "]" & vbNewLine & "==========================================="
                                    End If
                                    NCRMoneyFerCustomerAlertService.loglog(LogMessageSMS, True)
                                End If

                            Else  ' in case of resend request
                                SMSSC.BasicURL = NCRMoneyFerCustomerAlertService.SMSServiceBasicURL
                                SMSSC.Status = ""
                                lProvider = NCRMoneyFerCustomerAlertService.SMSServiceProvider
                                lType = "Push"
                                lSender = NCRMoneyFerCustomerAlertService.SMSServiceSenderMobile

                                lTimeOut = NCRMoneyFerCustomerAlertService.SMSSendingTimeOut
                                SMSSC.Status = ""
                                If trx.TrxPhase = const_TransactionResendSMS And _
                                   (trx.ResendType = const_ResendTypeBoth Or trx.ResendType = const_ResendTypeB) Then
                                    rtrnb = SMSSC.SendSMS(lProvider, lType, msgstrb, lSender, trx.BeneficiaryMobile, trx.MSGBinary, lTimeOut, trx.SMSMessageId, trx.SMSSendingStatus)
                                    If CustomerAlertService.NCRMoneyFerCustomerAlertService.LogLevel > 2 Then
                                        SMSErrorDescription = SMSSC.Status
                                    Else
                                        SMSErrorDescription = ""
                                    End If
                                    If rtrnb = 0 Then
                                        If CustomerAlertService.NCRMoneyFerCustomerAlertService.LogLevel > 2 Then
                                            LogMessageSMS = "Successfully Sending the Message:" & vbNewLine & msgstrb & vbNewLine & " to phone [" & trx.BeneficiaryMobile & "]"
                                        Else
                                            LogMessageSMS = "Successfully Sending the Message:" & vbNewLine & " to phone [" & trx.BeneficiaryMobile & "]"
                                        End If
                                        NCRMoneyFerCustomerAlertService.loglog(LogMessageSMS, False)
                                    Else
                                        If CustomerAlertService.NCRMoneyFerCustomerAlertService.LogLevel > 2 Then
                                            LogMessageSMS = "Fail to Send the Message:" & vbNewLine & msgstrb & vbNewLine & "to phone [" & trx.BeneficiaryMobile & "]" & vbNewLine & "SMS description:" & vbNewLine & "[" & SMSErrorDescription & "]" & vbNewLine & "============================================="
                                        Else
                                            LogMessageSMS = "Fail to Send the Message:" & vbNewLine & "to phone [" & trx.BeneficiaryMobile & "]" & vbNewLine & "SMS description:" & vbNewLine & "[" & SMSErrorDescription & "]" & vbNewLine & "============================================="
                                        End If
                                        NCRMoneyFerCustomerAlertService.loglog(LogMessageSMS, True)
                                    End If
                                    SMSSC.Status = ""
                                    Thread.Sleep(1000)
                                End If
                                If trx.TrxPhase = const_TransactionResendSMS And _
                                   (trx.ResendType = const_ResendTypeBoth Or trx.ResendType = const_ResendTypeD) Then

                                    rtrnd = SMSSC.SendSMS(lProvider, lType, msgstrd, lSender, trx.DepositorMobile, trx.MSGBinary, lTimeOut, trx.SMSMessageId, trx.SMSSendingStatus)
                                    If CustomerAlertService.NCRMoneyFerCustomerAlertService.LogLevel > 2 Then
                                        SMSErrorDescription = SMSSC.Status
                                    Else
                                        SMSErrorDescription = ""
                                    End If

                                    If rtrnd = 0 Then
                                        If CustomerAlertService.NCRMoneyFerCustomerAlertService.LogLevel > 2 Then
                                            LogMessageSMS = "Successfully Sending the Message:" & vbNewLine & msgstrd & vbNewLine & " to phone [" & trx.DepositorMobile & "]"
                                        Else
                                            LogMessageSMS = "Successfully Sending the Message:" & vbNewLine & " to phone [" & trx.DepositorMobile & "]"
                                        End If
                                        NCRMoneyFerCustomerAlertService.loglog(LogMessageSMS, False)
                                    Else
                                        If CustomerAlertService.NCRMoneyFerCustomerAlertService.LogLevel > 2 Then
                                            LogMessageSMS = "Fail to Send the Message:" & vbNewLine & msgstrd & vbNewLine & " to phone [" & trx.DepositorMobile & "]" & vbNewLine & "SMS description: " & vbNewLine & "[" & SMSErrorDescription & "]" & vbNewLine & "==========================================="
                                        Else
                                            LogMessageSMS = "Fail to Send the Message:" & vbNewLine & " to phone [" & trx.DepositorMobile & "]" & vbNewLine & "SMS description: " & vbNewLine & "[" & SMSErrorDescription & "]" & vbNewLine & "==========================================="
                                        End If
                                        NCRMoneyFerCustomerAlertService.loglog(LogMessageSMS, True)
                                    End If
                                End If
                                If trx.TrxPhase = const_TransactionPhaseRedemption _
                                  Or trx.TrxPhase = const_TransactionResendSMSExpired Then


                                    trx.ResendType = const_ResendTypeD
                                    rtrnd = SMSSC.SendSMS(lProvider, lType, msgstrd, lSender, trx.DepositorMobile, trx.MSGBinary, lTimeOut, trx.SMSMessageId, trx.SMSSendingStatus)
                                    If CustomerAlertService.NCRMoneyFerCustomerAlertService.LogLevel > 2 Then
                                        SMSErrorDescription = SMSSC.Status
                                    Else
                                        SMSErrorDescription = ""
                                    End If

                                    If rtrnd = 0 Then
                                        If CustomerAlertService.NCRMoneyFerCustomerAlertService.LogLevel > 2 Then
                                            LogMessageSMS = "Successfully Sending the Message:" & vbNewLine & msgstrd & vbNewLine & " to phone [" & trx.DepositorMobile & "]"
                                        Else
                                            LogMessageSMS = "Successfully Sending the Message:" & vbNewLine & " to phone [" & trx.DepositorMobile & "]"
                                        End If
                                        NCRMoneyFerCustomerAlertService.loglog(LogMessageSMS, False)
                                    Else
                                        If CustomerAlertService.NCRMoneyFerCustomerAlertService.LogLevel > 2 Then
                                            LogMessageSMS = "Fail to Send the Message:" & vbNewLine & msgstrd & vbNewLine & " to phone [" & trx.DepositorMobile & "]" & vbNewLine & "SMS description: " & vbNewLine & "[" & SMSErrorDescription & "]" & vbNewLine & "==========================================="
                                        Else
                                            LogMessageSMS = "Fail to Send the Message:" & vbNewLine & " to phone [" & trx.DepositorMobile & "]" & vbNewLine & "SMS description: " & vbNewLine & "[" & SMSErrorDescription & "]" & vbNewLine & "==========================================="
                                        End If
                                        NCRMoneyFerCustomerAlertService.loglog(LogMessageSMS, True)
                                    End If
                                End If

                            End If 'end of sending
                            'updating
                            If trx.ResendType = const_ResendTypeBoth Then
                                If rtrnd = 0 And rtrnb = 0 Then
                                    trx.UpdateTrx()
                                End If
                            End If
                            If trx.ResendType = const_ResendTypeB Then
                                If rtrnb = 0 Then
                                    trx.UpdateTrx()
                                End If
                            End If
                            If trx.ResendType = const_ResendTypeD Then
                                If rtrnd = 0 Then
                                    trx.UpdateTrx()
                                End If
                            End If



                            Application.DoEvents()
                        Next

                        ts = Now
                    End If
                    Application.DoEvents()
                    System.Threading.Thread.Sleep(10000)
                Catch ex As Exception

                End Try
            End While


        Catch ex As Exception
            NCRMoneyFerCustomerAlertService.loglog("Error in main Proccess ex:" & ex.ToString, False)

        End Try
    End Sub
   
    
    Public Function GetSMSTransactionsList() As Integer

        Dim c As New System.Data.SqlClient.SqlConnection
        Dim cTrxm As New System.Data.SqlClient.SqlConnection
        Dim r As System.Data.SqlClient.SqlDataReader
        Dim cm As New System.Data.SqlClient.SqlCommand
        Dim cmTrxm As New System.Data.SqlClient.SqlCommand
        Dim q As String
        Dim tmpStr As String
        Dim tmp As String = ""
        Dim ldate As String = ""
        Dim ltime As String = ""
        Dim ncrcrpt As New NCRCrypto
        Dim TrxQueries(0 To 10000) As String
        Dim raffected As Integer = 0
        Dim TrxCount As Integer = 0
        Dim i As Integer = 0
        Dim trx As TrxClass
        'Dim ss As OracleClient.OracleString

        Try

            c.ConnectionString = NCRMoneyFerCustomerAlertService.SMSDatabaseConnectionString
            c.Open()
            cm.Connection = c


            q = " select *," & const_TransactionPhaseDeposit & " as TrxPhase "
            q += " from transactions"
            q += " where  SMSSendingStatus is null and depositstatus='CONFIRMED'  "
            q += " Union "
            q += " select *," & const_TransactionPhaseWithdraw & " as TrxPhase "
            q += " from transactions"
            q += " where  WSendingStatus is null and Withdrawalstatus='CONFIRMED' and redemptionPIN is null  "

            q += " Union "
            q += " select *," & const_TransactionPhaseRedemption & " as TrxPhase "
            q += " from transactions"
            q += " where  WSendingStatus is null and Withdrawalstatus='CONFIRMED' and redemptionPIN is not null  "


            q += " Union "
            q += " select *," & const_TransactionResendSMS & " as TrxPhase "
            q += " from transactions"
            q += " where ResendSMSFlag % 2 = 1 and depositstatus='CONFIRMED' and Withdrawalstatus is null   "


            q += " Union "
            q += " select *," & const_TransactionResendExpired & " as TrxPhase "
            q += " from transactions"
            q += " where resendsmsflag % 2 = 1 and  depositstatus='EXPIRED' and Withdrawalstatus='EXPIRED'    "


            ' ''q += " Union "
            ' ''q += " select *," & const_TransactionResendSMSExpired & " as TrxPhase "
            ' ''q += " from transactions"
            ' ''q += " where resendsmsflag=1 and  depositstatus='EXPIRED' and Withdrawalstatus='EXPIRED'    "


            cm.CommandType = CommandType.Text
            cm.CommandText = q
            r = cm.ExecuteReader

            
            If r.HasRows Then
                Try

                    While r.Read()


                        trx = New TrxClass
                        'here you can set more trx data

                        trx.Amount = r("Amount")
                        trx.TransactionCode = r("TransactionCode")
                        trx.DepositorMobile = r("DepositorMobile")
                        trx.BeneficiaryMobile = r("BeneficiaryMobile")
                        If NCRMoneyFerCustomerAlertService.SMSAlertingService = 3 Then
                            If trx.DepositorMobile.Length <= 11 And trx.DepositorMobile.Substring(0, 1) <> "+" Then
                                trx.DepositorMobile = NCRMoneyFerCustomerAlertService.SMSWebMobilePrefix & trx.DepositorMobile
                            End If
                            If trx.BeneficiaryMobile.Length <= 11 And trx.BeneficiaryMobile.Substring(0, 1) <> "+" Then
                                trx.BeneficiaryMobile = NCRMoneyFerCustomerAlertService.SMSWebMobilePrefix & trx.BeneficiaryMobile
                            End If
                        End If
                        
                        tmpStr = r("DepositorPIN")
                        trx.DepositorPIN = ncrcrpt.eT3_Decrypr(tmpStr)
                        tmpStr = r("BeneficiaryPIN")
                        trx.BeneficiaryPIN = ncrcrpt.eT3_Decrypr(tmpStr)
                        Try
                            tmpStr = r("RedemptionPIN")
                            trx.RedemptionPIN = ncrcrpt.eT3_Decrypr(tmpStr)
                        Catch ex As Exception
                            trx.RedemptionPIN = ""
                        End Try


                        trx.TrxPhase = r("TrxPhase")
                        trx.ResendType = r("ResendTo")
                        trx.SMSLanguage = r("SMSLanguage")

                        trx.ResendSMSFlag = r("ResendSMSFlag")

                        Trxs.Add(trx)
                        Application.DoEvents()
                    End While


                    NCRMoneyFerCustomerAlertService.loglog("GetSMSTransactionList, returns " & Trxs.Count & " rows with query=[" & q & "]", False)

                Catch ex As Exception
                    NCRMoneyFerCustomerAlertService.loglog("Error Preparing Last List Of TRx for service " & ServiceId & " trxcount=" & TrxCount & " ex:" & ex.ToString, False)
                    Try
                        c.Close()
                    Catch ex2 As Exception
                    End Try
                    Return 7
                End Try


            Else
                NCRMoneyFerCustomerAlertService.loglog("No Transation is fount for Q=[" & q & "]", True)


            End If
            Try
                c.Close()
            Catch ex As Exception

            End Try

            Return 0

        Catch ex As Exception
            NCRMoneyFerCustomerAlertService.loglog("Error Getting last Transation for service " & serviceId & " exp:" & ex.ToString, False)

            Return 9
        End Try

    End Function
   
    Public Sub New()
        PortIsOpened = False
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
Public Class TrxClass
    Public TransactionCode As String
    Public CountryCode As String
    Public BankCode As String
    Public ATMId As String
    Public RequestType As String
    Public ATMDate As String
    Public ATMTime As String
    Public ATMTrxSequence As String
    Public DepositorMobile As String
    Public DepositorPIN As String
    Public BeneficiaryMobile As String
    Public BeneficiaryPIN As String
    Public RedemptionPIN As String
    Public Amount As String
    Public CurrencyCode As String
    Public DepositDateTime As String
    Public DepositActionReason As String
    Public DepositStatus As String
    Public WithdrawalStatus As String
    Public WithdrawalDateTime As String
    Public CancelStatus As String
    Public CancelDateTime As String
    Public OverallStatus As String = ""
    Public SMSSendingStatus As String = ""
    Public SMSMessageId As String = ""
    Public SMSSentDateTime As String
    Public TrxPhase As String
    Public ResendType As String
    Public ResendSMSFlag As Integer
    Public SMSsendingLanguage As String
    Private lSMSLanguage As String
    Public Property SMSLanguage() As String
        Get
            Return lSMSLanguage
        End Get
        Set(ByVal value As String)
            lSMSLanguage = value
            If lSMSLanguage = const_SMSLanguage_Arabic Then
                MSGBinary = "08"
            Else
                MSGBinary = "00"
            End If
        End Set
    End Property

    Public MSGBinary As String
    Const const_SMSLanguage_English = "E"
    Const const_SMSLanguage_Arabic = "A"

    

    Public Function BuildSMSBody(ByVal SMSTmplate As String) As String
        Dim pret As String
        Dim i As Integer
        Dim cc As String
        pret = ""
        Try
            i = 0
            If SMSTmplate.Trim = "" Then

                Return ""
            End If
            While i < SMSTmplate.Length
                cc = SMSTmplate.Substring(i, 1)
                If cc <> "[" Then
                    If cc = "]" Then
                        pret += cc
                        i += 1
                    Else

                        pret += cc
                        i += 1

                    End If
                Else
                    pret += cc
                    i += 1
                    If i + 1 <= SMSTmplate.Length Then
                        cc = SMSTmplate.Substring(i, 2)
                        pret += GetEquivalentValue(cc)
                        i += 2
                    End If

                End If
            End While


            Return pret

        Catch ex As Exception
            NCRMoneyFerCustomerAlertService.loglog("BuildSMSBody Error,ex:" & ex.ToString, False)
            Return ""
        End Try

    End Function
    Public Function BuildSMSBodyA(ByVal SMSTmplate As String) As String
        Dim pret As String
        Dim i As Integer
        Dim cc As String
        pret = ""
        Try
            i = 0
            If SMSTmplate.Trim = "" Then

                Return ""
            End If
            While i < SMSTmplate.Length
                cc = SMSTmplate.Substring(i, 1)
                If cc <> "[" Then
                    If cc = "]" Then
                        pret += " "
                        i += 1
                    Else

                        pret += cc
                        i += 1

                    End If
                Else
                    pret += " " 'cc
                    i += 1
                    If i + 1 <= SMSTmplate.Length Then
                        cc = SMSTmplate.Substring(i, 2)
                        pret += GetEquivalentValue(cc)
                        i += 2
                    End If

                End If
            End While


            Return pret

        Catch ex As Exception
            NCRMoneyFerCustomerAlertService.loglog("BuildSMSBody Error,ex:" & ex.ToString, False)
            Return ""
        End Try

    End Function

    Private Function GetEquivalentValue(ByVal symbole As String) As String
        Dim maskedMobNumber As String

        Select Case symbole
            Case "DM"
                maskedMobNumber = MaskedMobileNumber(DepositorMobile) ' "xxxxx" & (DepositorMobile & Space(5)).Substring(5).Trim
                Return maskedMobNumber
            Case "BM"
                maskedMobNumber = MaskedMobileNumber(BeneficiaryMobile) ' "xxxxx" & (BeneficiaryMobile & Space(5)).Substring(5).Trim
                Return maskedMobNumber
            Case "DP"
                Return DepositorPIN
            Case "BP"
                Return BeneficiaryPIN
            Case "TC"
                Return TransactionCode
            Case "RP"
                Return RedemptionPIN
            Case "CD"
                Return DepositorMobile
            Case "CB"
                Return BeneficiaryMobile
            Case "AB"
                Return NCRMoneyFerCustomerAlertService.BankArabicName
            Case "EB"
                Return NCRMoneyFerCustomerAlertService.BankName
            Case Else
                Return symbole
        End Select
    End Function
    Public Function MaskedMobileNumber(ByVal pMobileNumber As String) As String
        Dim tt As String
        Dim ll As Long

        ll = pMobileNumber.Length
        If ll > 5 Then
            tt = "xxxxxxxxxxxxxxxxxxxx".Substring(0, ll - 10) & pMobileNumber.Substring(ll - 5)
        Else
            tt = pMobileNumber
        End If
        Return tt
    End Function
    Public Function GetMessageB_Deposit() As String
        Dim msgstrb As String
        Dim maskedMobNumber As String
        Dim bankname As String

        If SMSLanguage = const_SMSLanguage_English Then
            msgstrb = BuildSMSBody(NCRMoneyFerCustomerAlertService.BeneficiaryDepositSMSBody)
            If msgstrb = "" Then
                maskedMobNumber = MaskedMobileNumber(DepositorMobile) ' "*****" & (DepositorMobile & Space(5)).Substring(5).Trim
                msgstrb = "A Given amount was transfered to you from [" & maskedMobNumber & "]. You can collect it from any United Bank ATM using:"
                msgstrb = "An amount was transfered from [" & maskedMobNumber & "]. Collect it using:"
                msgstrb += " Key 1: [" & BeneficiaryPIN & "]"
                msgstrb += " and Trx Code: [" & TransactionCode & "]"
                msgstrb += ". Ask the depositor for Key 2."
              
            End If

        Else
            msgstrb = BuildSMSBody(NCRMoneyFerCustomerAlertService.ARBeneficiaryDepositSMSBody)
            If msgstrb = "" Then
                bankname = NCRMoneyFerCustomerAlertService.BankArabicName
                maskedMobNumber = MaskedMobileNumber(DepositorMobile) ' "xxxxx" & (DepositorMobile & Space(5)).Substring(5).Trim
                msgstrb = " لك حوالة من" & " " & maskedMobNumber & "و يمكنك تحصيلها من أى صراف آلى " & bankname
                msgstrb += " باستخدام الآتى: الرقم السرى للمستفيد " & BeneficiaryPIN
                msgstrb += " و كود العملية " & TransactionCode
                msgstrb += "و اسأل المودع عن الرقم السرى الخاص به"
                ''' Kareem 07052014 CIB
                'msgstrb = "طرفنا حوالة من " & maskedMobNumber
                'msgstrb += "بكود " & TransactionCode
                'msgstrb += "استلامها خلال 5 ايام برقم سري " & BeneficiaryPIN & " "
                'msgstrb += "من أقرب ماكينة صرف آلي تابعة لل" & bankname

            End If
           
        End If

        Return msgstrb
    End Function
    Public Function GetMessageD_Deposit() As String
        Dim msgstrd As String
        Dim maskedMobNumber As String

        If SMSLanguage = const_SMSLanguage_English Then
            msgstrd = BuildSMSBody(NCRMoneyFerCustomerAlertService.DepositorDepositSMSBody)
            If msgstrd = "" Then
                maskedMobNumber = MaskedMobileNumber(BeneficiaryMobile) ' "*****" & (BeneficiaryMobile & Space(5)).Substring(5).Trim
                msgstrd = "Your Amount was transfered successfuly to  [" & maskedMobNumber & "] "
                msgstrd += "with Trx Code: " & TransactionCode
                msgstrd += " You must send "
                If NCRMoneyFerCustomerAlertService.EnableSendingKey2 > 0 Then
                    msgstrd += " Key 2: [" & DepositorPIN & "]"
                End If
                msgstrd += " to the beneficiary"
            End If

        Else
            msgstrd = BuildSMSBody(NCRMoneyFerCustomerAlertService.ARDepositorDepositSMSBody)
            If msgstrd = "" Then
                maskedMobNumber = MaskedMobileNumber(BeneficiaryMobile) ' "xxxxx" & (BeneficiaryMobile & Space(5)).Substring(5).Trim
                msgstrd = " تم تنفيذ حوالتك بنجاح إلى" & maskedMobNumber
                If NCRMoneyFerCustomerAlertService.EnableSendingKey2 > 0 Then
                    msgstrd += " برجاء ابلاغ المستفيد بالآتى: الرقم السرى للمودع " & DepositorPIN
                End If
                ''msgstrd += " و كود العملية " & TransactionCode
                '''Kareem 07052014 CIB
                'msgstrd = "تم تنفيذ الحواله إلي " & maskedMobNumber
                'msgstrd += " بكود " & TransactionCode
                'msgstrd += " ابلغ المستفيد بالرقم السرى" & DepositorPIN

            End If
            
            End If
            Return msgstrd
    End Function

    Public Function GetMessageB_Withdrawal() As String
        Dim msgstrb As String
        Dim maskedMobNumber As String

        If SMSLanguage = const_SMSLanguage_English Then
            msgstrb = BuildSMSBody(NCRMoneyFerCustomerAlertService.BeneficiaryWithdrawalSMSBody)
            If msgstrb = "" Then
                maskedMobNumber = MaskedMobileNumber(DepositorMobile) ' "*****" & (DepositorMobile & Space(5)).Substring(5).Trim
                msgstrb = "You've successfully collected the amount being transfered to you from "
                msgstrb += maskedMobNumber
                msgstrb += ",with Trx Code: " & TransactionCode
            End If
        Else
            'maskedMobNumber = "*****" & (DepositorMobile & Space(10)).Substring(5, 5)
            'msgstrb = "Amount Of money was collected by you from TUB ATM "
            'msgstrb += " Depositor mobile:" & maskedMobNumber
            'msgstrb += " Trx Code: " & TransactionCode

            msgstrb = BuildSMSBody(NCRMoneyFerCustomerAlertService.ARBeneficiaryWithdrawalSMSBody)
            If msgstrb = "" Then
                maskedMobNumber = MaskedMobileNumber(DepositorMobile) ' "xxxxx" & (DepositorMobile & Space(5)).Substring(5).Trim
                msgstrb = " لقد قمت بنجاح بتحصيل المبلغ المحول لك"
                msgstrb += " من المودع " & maskedMobNumber
                msgstrb += " علما بأن كود العملية " & TransactionCode

                '''''Kareem 07052014 CIB
                'msgstrb = "شكرا لاستخدامك خدمة تربو كاش. تم تأكيد سحب الحوالة"
                'msgstrb += " بكود رقم " & TransactionCode

            End If
           
        End If



        Return msgstrb
    End Function
    Public Function GetMessageD_Withdrawal() As String
        Dim msgstrd As String
        Dim maskedMobNumber As String

        If SMSLanguage = const_SMSLanguage_English Then
            msgstrd = BuildSMSBody(NCRMoneyFerCustomerAlertService.DepositorWithdrawalSMSBody)
            If msgstrd = "" Then
                maskedMobNumber = MaskedMobileNumber(BeneficiaryMobile) ' "*****" & (BeneficiaryMobile & Space(5)).Substring(5).Trim
                msgstrd = "The Beneficiary [" & maskedMobNumber & "] has successfully collected your transfered amount"
                msgstrd += " with Trx Code: " & TransactionCode
            End If
        Else
            msgstrd = BuildSMSBody(NCRMoneyFerCustomerAlertService.ARDepositorWithdrawalSMSBody)
            If msgstrd = "" Then
                maskedMobNumber = MaskedMobileNumber(BeneficiaryMobile) ' "xxxxx" & (BeneficiaryMobile & Space(5)).Substring(5).Trim
                msgstrd = " لقد تم بنجاح تحصيل حوالتك "
                msgstrd += " إلى " & maskedMobNumber
                msgstrd += " علما بأن كود العملية " & TransactionCode
                '''Kareem 07052014 CIB
                'msgstrd = "شكرا لاستخدام خدمة  تربو كاش.تم تأكيد سحب الحوالة "
                'msgstrd += "بكود" & TransactionCode
            End If
            ''maskedMobNumber = "*****" & (BeneficiaryMobile & Space(10)).Substring(5, 5)
            ''msgstrd = "Amount Of money was collected successfuly by  [" & maskedMobNumber & "]"
            ''msgstrd += " Trx Code: " & TransactionCode


            
        End If

        Return msgstrd
    End Function
    Public Function GetMessageD_Redemption() As String
        Dim msgstrd As String
        If SMSLanguage = const_SMSLanguage_English Then
            msgstrd = BuildSMSBody(NCRMoneyFerCustomerAlertService.DepositorRedemptionSMSBody)
            If msgstrd = "" Then

                msgstrd = "You've successfully redeemed your expired transfer with"
                msgstrd += " Trx Code: " & TransactionCode
            End If
        Else
            ''msgstrd = "Amount Of money was redeemed successfuly by  you"
            ''msgstrd += " Trx Code: " & TransactionCode
            msgstrd = BuildSMSBody(NCRMoneyFerCustomerAlertService.ARDepositorRedemptionSMSBody)
            If msgstrd = "" Then
                msgstrd = " لقد قمت بنجاح باسترداد مبلغ حوالتك الملغاة  "
                msgstrd += " علما بأن كود العملية " & TransactionCode
                ''''Kareem 07052014 CIB
                'msgstrd = "تم استرداد حوالتك بكود رقم" & TransactionCode & "بنجاح"
                'تم استرداد حوالتك بكود رقم xxxxxxxxxxxx بنجاح
            End If

           
        End If

        Return msgstrd
    End Function

    Public Function GetMessageExpiration_D() As String
        Dim msgstrd As String
        Dim maskedMobNumber As String
        Dim bankname As String
        If SMSLanguage = const_SMSLanguage_English Then
            msgstrd = BuildSMSBody(NCRMoneyFerCustomerAlertService.DepositorTrxExpirationSMSBody_D)
            If msgstrd = "" Then
                msgstrd = "Your transfer transaction with code [" & TransactionCode & "] has EXPIRED. "
                msgstrd += " You can redeem your money from any United Bank ATM using this redemption key " & RedemptionPIN
            End If
        Else
            ''maskedMobNumber = "*****" & (RedemptionPIN & Space(10)).Substring(5, 5)
            ''msgstrd = "The money transfer transaction [" & TransactionCode & "] is EXPIRED "
            ''msgstrd += " You can return your money at any TUB ATM using this redemption key " & RedemptionPIN
            msgstrd = BuildSMSBody(NCRMoneyFerCustomerAlertService.ARDepositorTrxExpirationSMSBody_D)
            If msgstrd = "" Then
                bankname = NCRMoneyFerCustomerAlertService.BankArabicName
                maskedMobNumber = MaskedMobileNumber(BeneficiaryMobile) ' "*****" & (BeneficiaryMobile & Space(5)).Substring(5).Trim
                msgstrd = "انتهت صلاحية حوالتك بكود" & TransactionCode
                msgstrd += "ويمكنك استلامها بالرقم السري" & RedemptionPIN
                msgstrd += "من أقرب ماكينة صرف آلي تابعة " & bankname
            End If
           

        End If

        Return msgstrd
    End Function

    Public Function GetMessageExpiration_B() As String
        Dim msgstrd As String
        Dim maskedMobNumber As String

        If SMSLanguage = const_SMSLanguage_English Then
            msgstrd = BuildSMSBody(NCRMoneyFerCustomerAlertService.DepositorTrxExpirationSMSBody_B)
            If msgstrd = "" Then
                msgstrd = "Your transfer transaction with code [" & TransactionCode & "] has EXPIRED. "

            End If
        Else
            ''maskedMobNumber = "*****" & (RedemptionPIN & Space(10)).Substring(5, 5)
            ''msgstrd = "The money transfer transaction [" & TransactionCode & "] is EXPIRED "
            ''msgstrd += " You can return your money at any TUB ATM using this redemption key " & RedemptionPIN
            msgstrd = BuildSMSBody(NCRMoneyFerCustomerAlertService.ARDepositorTrxExpirationSMSBody_B)
            If msgstrd = "" Then
                maskedMobNumber = MaskedMobileNumber(BeneficiaryMobile) ' "*****" & (BeneficiaryMobile & Space(5)).Substring(5).Trim
                msgstrd = "انتهت صلاحية حوالتك  بكود" & TransactionCode & "لايمكنك استلامها"

                ''انتهت صلاحية حوالتك  بكود xxxxxxxxxxxx لايمكنك استلامها
            End If
        


        End If

        Return msgstrd
    End Function

    Public Function UpdateTrx() As Integer

        Dim c As New System.Data.SqlClient.SqlConnection
        Dim cTrxm As New System.Data.SqlClient.SqlConnection
        Dim cm As New System.Data.SqlClient.SqlCommand
        Dim q As String
        Dim Subquery As String
        Dim raffected As Integer = 0
        Dim sqltrx As SqlClient.SqlTransaction
        Dim pSMSSendStatus As String = ""
        Try

            c.ConnectionString = NCRMoneyFerCustomerAlertService.SMSDatabaseConnectionString
            c.Open()
            cm.Connection = c


            If SMSSendingStatus = "4" Then
                pSMSSendStatus = "SENT"
            Else
                pSMSSendStatus = "Error " & SMSSendingStatus
            End If
            cm.CommandType = CommandType.Text
            If TrxPhase = CheckTransactionsClass.const_TransactionPhaseDeposit Then
                q = "Update  transactions set SMSSendingStatus='SENT', SMSSentDateTime=getdate() "
                Subquery = " Insert into TransactionNestedActions (TransactionCode,action,ActionDateTime,ActionReason,ActionStatus) "
                Subquery += " values ('" & TransactionCode & "','08',getdate(), 'SENDSMS_DP','" & pSMSSendStatus & "')"

            ElseIf TrxPhase = CheckTransactionsClass.const_TransactionPhaseWithdraw Then
                q = "Update  transactions set WSendingStatus='SENT', WSentDateTime=getdate() "
                Subquery = " Insert into TransactionNestedActions (TransactionCode,action,ActionDateTime,ActionReason,ActionStatus) "
                Subquery += " values ('" & TransactionCode & "','18',getdate(), 'SENDSMS_WP','" & pSMSSendStatus & "')"

            ElseIf TrxPhase = CheckTransactionsClass.const_TransactionPhaseRedemption Then
                q = "Update  transactions set WSendingStatus='SENT', WSentDateTime=getdate() "
                Subquery = " Insert into TransactionNestedActions (TransactionCode,action,ActionDateTime,ActionReason,ActionStatus) "
                Subquery += " values ('" & TransactionCode & "','48',getdate(), 'SENDSMS_RD','" & pSMSSendStatus & "')"


            ElseIf TrxPhase = CheckTransactionsClass.const_TransactionResendSMS Then
                q = "Update  transactions set ResendSMSFlag=ResendSMSFlag+1, ResendSMSDateTime=getdate() "
                Subquery = " Insert into TransactionNestedActions (TransactionCode,action,ActionDateTime,ActionReason,ActionStatus) "
                Subquery += " values ('" & TransactionCode & "','28',getdate(), 'SENDSMS_RS','" & pSMSSendStatus & "')"

            ElseIf TrxPhase = CheckTransactionsClass.const_TransactionResendExpired Then
                q = "Update  transactions set ResendSMSFlag=ResendSMSFlag+1, ResendSMSDateTime=getdate() "
                Subquery = " Insert into TransactionNestedActions (TransactionCode,action,ActionDateTime,ActionReason,ActionStatus) "
                Subquery += " values ('" & TransactionCode & "','38',getdate(), 'SENDSMS_EX','" & pSMSSendStatus & "')"

            Else
                NCRMoneyFerCustomerAlertService.loglog("UpdateTrx, Unknown trx Code=[" & TransactionCode & "]", False)
                c.Close()
                Return 1
            End If
            q += " where  transactioncode='" & TransactionCode & "'"

            sqltrx = c.BeginTransaction
            Try
                cm.Transaction = sqltrx
                cm.CommandText = q
                raffected = cm.ExecuteNonQuery()
                NCRMoneyFerCustomerAlertService.loglog("UpdateTrx,rows affected=[" & raffected & "] for updating trx q=[" & q & "]", False)

                cm.CommandText = Subquery
                raffected = cm.ExecuteNonQuery()

                sqltrx.Commit()

                Try
                    c.Close()
                Catch ex As Exception
                End Try

                Return 0
            Catch ex As Exception
                sqltrx.Rollback()
                NCRMoneyFerCustomerAlertService.loglog("Error Updating transaction [" & TransactionCode & "] phase=[" & TrxPhase & "] ex:" & ex.ToString, False)

                Return 8
            End Try




        Catch ex As Exception
            NCRMoneyFerCustomerAlertService.loglog("Error Updating transaction [" & TransactionCode & "] phase=[" & TrxPhase & "] ex:" & ex.ToString, False)

            Try
                c.Close()
            Catch ex2 As Exception
            End Try
            Return 9
        End Try





    End Function

    Public Sub New()

    End Sub
End Class
