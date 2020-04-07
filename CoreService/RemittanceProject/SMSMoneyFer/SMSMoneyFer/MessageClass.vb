Imports System.Net.Sockets
Imports System.Net
Imports System.text

Public Class MessageClass
    Public Declare Auto Function FindWindow Lib "user32.dll" _
             Alias "FindWindow" (ByVal lpClassName As String, _
                                 ByVal lpWindowName As String) As Integer


    Const cnst_RequestType_DepositAuthorization As String = "01"
    Const cnst_RequestType_DepositConfirmation As String = "11"
    Const cnst_RequestType_DepositCancelation As String = "21"


    ''  Const cnst_RequestType_DepositAuthorizeHostError As String = "31"
    ''  Const cnst_RequestType_DepositCancelHostError As String = "41"

    Const cnst_RequestType_CBDebitAmountAuthorization As String = "0A"

    Const cnst_RequestType_WithdrawalAuthorization As String = "02"
    Const cnst_RequestType_WithdrawalConfirmation As String = "12"
    Const cnst_RequestType_WithdrawalCancelation As String = "22"
    Const cnst_RequestType_WithdrawalUnCertain As String = "32"




    Const cnst_RequestType_DepositExpiration As String = "03"
    Const cnst_RequestType_DepositReActivation As String = "13"
    Const cnst_RequestType_DepositForceExpiration As String = "23"



    Const cnst_RequestType_DepositHold As String = "04"
    Const cnst_RequestType_DepositUnHold As String = "14"

    Const cnst_RequestType_CommissionInformation As String = "05"
    Const cnst_RequestType_CheckRegistration As String = "15"


    Const cnst_RequestType_ResendSMSBoth As String = "06"
    Const cnst_RequestType_ResendSMSDepositor As String = "16"
    Const cnst_RequestType_ResendSMSBeneficiery As String = "26"
    Const cnst_RequestType_ResendSMSRedemption As String = "36"

    Const cnst_RequestType_RedemptionAutorization As String = "07"
    Const cnst_RequestType_RedemptionConfirmation As String = "17"
    Const cnst_RequestType_RedemptionCancelation As String = "27"
    Const cnst_RequestType_RedemptionUnCertain As String = "37"
    Const cnst_RequestType_RedemptionCancelHostError As String = "47"

    Const cnst_RequestType_SendSMSdepositPhase As String = "08"
    Const cnst_RequestType_SendSMSWithdrawalPhase As String = "18"
    Const cnst_RequestType_SendSMSResendSMS As String = "28"
    Const cnst_RequestType_SendSMSExpired As String = "38"

    Const cnst_RequestType_UnBLock As String = "09"
    Const cnst_RequestType_ManuallyConfirm As String = "19"
    Const cnst_RequestType_ResetKeyTrials As String = "29"
    Const cnst_RequestType_ExpiredManuallyConfirm As String = "39"


    Const cnst_RequestType_NewWithdrawalAuthorization As String = "N1"
    Const cnst_RequestType_NewWithdrawalConfirmation As String = "N2"
    Const cnst_RequestType_NewWithdrawalCancelation As String = "N3"
    Const cnst_RequestType_NewWithdrawalUnCertain As String = "N4"


    Const cnst_ErrCode_MessageParsingError As Integer = 1
    Const cnst_ErrCode_UnknownRequestType As Integer = 2
    Const cnst_ErrCode_CanNotGetHostTrxCode As Integer = 3
    Const cnst_ErrCode_DataBaseError As Integer = 4
    Const cnst_ErrCode_CanNotGetPINs As Integer = 5
    Const cnst_ErrCode_OriginalRequestNotFound As Integer = 6
    Const cnst_ErrCode_OriginalRequestDetailsNotFound As Integer = 36
    Const cnst_ErrCode_WrongPINs As Integer = 26
    Const cnst_ErrCode_ManyKeyTrilas As Integer = 27
    Const cnst_ErrCode_PadorNullAmountValue As Integer = 7
    Const cnst_ErrCode_BlockedBenificiary As Integer = 28
    Const cnst_ErrCode_BlockedDepositor As Integer = 29
    Const cnst_ErrCode_UnRegisterdDepositor As Integer = 30
    Const cnst_ErrCode_UnRegisterdBeneficiery As Integer = 30

    Const cnst_ErrCode_NoExpiredTransactionsOrDBError As Integer = 8
    Const cnst_ErrCode_ATMIPAddressNotMatchedError As Integer = 16
    Const cnst_ErrCode_ATMIPAddressIsNotTeller As Integer = 17



    Const cnst_ErrCode_CassettesValuesErrors As Integer = 18
    Const cnst_ErrCode_CalculateDispensedNotesError As Integer = 19

    Const cnst_ErrCode_TodayTotalDepositAmountExceedsMaxAllowedValue As Integer = 20
    Const cnst_ErrCode_ToMonthTotalDepositAmountExceedsMaxAllowedValue As Integer = 21
    Const cnst_ErrCode_TodayTotalDepositAmountExceedsMaxAllowedCount As Integer = 22
    Const cnst_ErrCode_ToMonthTotalDepositAmountExceedsMaxMonthlyAllowedValue As Integer = 23

    Const cnst_ErrCode_BadStatusForRequiredRequestType As Integer = 101
    Const cnst_ErrCode_NoConfirmedDeposistTransaction As Integer = 101 ' 102
    Const cnst_ErrCode_NotExpiredDepositTransaction As Integer = 101 '103
    Const cnst_ErrCode_NotCanceledOrNullWithdrawalTransaction As Integer = 101 ' 104
    Const cnst_ErrCode_NotCanceledOrExpiredWithdrawalTransaction As Integer = 101 ' 105
    Const cnst_ErrCode_ExpiredTransaction As Integer = 106
    Const cnst_ErrCode_ReActivationCountExceeded As Integer = 107

    Const cnst_ErrCode_ErrCreatingReplyQ As Integer = 117
    Const cnst_ErrCode_ErrsendingRequestQ As Integer = 118
    Const cnst_ErrCode_ErrReceiveTimeOut As Integer = 119
    Const cnst_ErrCode_ErrReceiveError As Integer = 120
    Const cnst_ErrCode_ErrHeaderNotMatchError As Integer = 121
    Const cnst_ErrCode_ErrParsingReplyError As Integer = 122
    Const cnst_ErrCode_ErrHostReplyTimeOut As Integer = 123
    Const cnst_ErrCode_ErrHostreceivingReply As Integer = 199
    Const cnst_ErrCode_ErrAmountReply As Integer = 200


    Const cnst_ErrCode_ErrHostSuspendMode = 510
    Const cnst_ErrCode_ErrHostInCompleteTrx = 511
    Const cnst_ErrCode_HostNoError = 500


    Const cnst_ErrCode_DebitAccountAuthorizationError As Integer = 9000
    Const cnst_ErrCode_DebitAccountAuthorizationBError As Integer = 900

    Const cnst_ResendTypeBoth = 3
    Const cnst_ResendTypeD = 1
    Const cnst_ResendTypeB = 2
    Const cnst_ResendTypeDR = 4

    Const cnst_ActionStatus_AUTHORIZED As String = "AUTHORIZED"
    Const cnst_ActionStatus_CONFIRMED As String = "CONFIRMED"
    Const cnst_ActionStatus_UnCertain As String = "UNCERTAIN"
    Const cnst_ActionStatus_CANCELED As String = "CANCELED"
    Const cnst_ActionStatus_UNBLOCKED As String = "UNBLOCKED"
    Const cnst_ActionStatus_EXPIRED As String = "EXPIRED"
    Const cnst_ActionStatus_HOLD As String = "HOLD"
    Const cnst_ActionStatus_UNHOLD As String = "UNHOLD"
    Const cnst_ActionStatus_RESENDSMSBoth As String = "RESENDSMSBoth"
    Const cnst_ActionStatus_RESENDSMSD As String = "RESENDSMSD"
    Const cnst_ActionStatus_RESENDSMSB As String = "RESENDSMSB"
    Const cnst_ActionStatus_REACTIVATED As String = "REACTIVATED"
    Const cnst_ActionStatus_ExpiredManualCONFIRMED As String = "EMCONFIRMED"


    Const cnst_PaymentType_CASH = "CASH"
    Const cnst_PaymentType_DBTAccount = "DEBITACCOUNT"


    Public ATMId As String '5
    Public BankId As String '5
    Public Country As String '10

    Public RequestType As String '2
    Public ResponseCode As String '5
    Public ATMDate As String '10
    Public ATMTime As String '8
    Public ATMDateTime As DateTime
    Public TransactionSequence As String '10
    Public DepositorMobile As String '20
    Public DepositorPIN As String '10
    Public BeneficiaryMobile As String '20
    Public BeneficiaryPIN As String '10
    Public RedempltionPIN As String '10
    Public Amount As String '15
    Public Currency As String '5
    Public Hosttransactioncode As String '20
    Public ActionReason As String '25
    Public ActionStatus As String '25
    Public DispensedNotes As String '8
    Public DispensedAmount As Integer '15
    Public CommissionAmount As Integer '15
    Public SMSLanguage As String '  'E' "A"
    Public MinimumValue As String '15
    Public MaximumValue As String '15
    Public ReceiptLine1 As String '40
    Public ReceiptLine2 As String '40
    Public ReceiptLine3 As String '40
    Public ExtraData As String '40
    Public DispensedRate As Double = 1
    Public NationalID As String

    Public ATMIPAddress As String


    Private mvOutGoingReplyData As String
    Private mvOutGoingNewReplyData As String
    Private mvISTeller As Boolean
    ''' shared object to ensure locking during trx code generation
    Public Shared LockThread As Threading.Thread
    ''' '''''''''''''''''''''''''''''''''''''''''''''''''

    Private mvATMCassitteValue(4) As Integer
    Private mvATMPhysicalCassitteValue(4) As Integer
    Private mvATMCassitteType(4) As Integer
    Private mvMaxNotesCount As Integer
    Private mvDepositCurrency As String

    Private mvCommissionValue1 As Integer = 10
    Private mvCommissionLL1 As Integer = 10
    Private mvCommissionUL1 As Integer = 500
    Private mvCommissionValue2 As Integer = 20
    Private mvCommissionLL2 As Integer = 501
    Private mvCommissionUL2 As Integer = 3000
    Private mvMaximumDailyAmount As Integer = 5000
    Private mvMaximumMonthlyAmount As Integer = 5000
    Private mvMaximumDailyCount As Integer = 5
    Private mvMaximumKeyTrials As Integer = 3

    Private mvMaxReActivateTimes As Integer = 3
    Private mvDeposittrxExpDays As Integer = 2

    Public DebitAccountNumber As String = ""
    Public CBTDataElement39 As String
    Public CBTDataElement38 As String
    Public CCTrack2 As String = ""



    Public Function GetOutgoingReplyData() As String
        Return mvOutGoingReplyData
    End Function

    Public Function GetOutgoingNewReplyData() As String
        Return mvOutGoingNewReplyData
    End Function
    Private Function ArrangeCassettesValues() As Integer
        Dim i As Integer
        Dim j As Integer
        Dim tmpValue As Integer
        Dim tmpType As Integer

        Try

            For j = 1 To 3
                For i = 1 To 3
                    tmpValue = mvATMCassitteValue(i)
                    tmpType = mvATMCassitteType(i)
                    If tmpValue > mvATMCassitteValue(i + 1) Then
                        mvATMCassitteValue(i) = mvATMCassitteValue(i + 1)
                        mvATMCassitteValue(i + 1) = tmpValue
                        mvATMCassitteType(i) = mvATMCassitteType(i + 1)
                        mvATMCassitteType(i + 1) = tmpType


                    End If

                Next i

            Next j

            Return (0)
        Catch ex As Exception
            log.loglog("ArrangeCassettesValues Error  Ex:" & ex.ToString, False)
            Return 9
        End Try
    End Function
    Private Function GetCassettesInfo() As Integer
        Dim cn As System.Data.SqlClient.SqlConnection
        Dim cmd As System.Data.SqlClient.SqlCommand
        Dim dr As System.Data.SqlClient.SqlDataReader
        Dim Qstr As String

        'Dim noOfRows As Integer
        Dim ret As Integer

        Try
            Qstr = "select * from bankatmview where "
            Qstr = Qstr & " ATMId='" & ATMId & "' and bankcode='" & CONFIGClass.LocalBank & "' and countrycode='" & CONFIGClass.LocalCountry & "'"

            cn = New System.Data.SqlClient.SqlConnection(CONFIGClass.ConnectionString)
            cn.Open()


            cmd = New System.Data.SqlClient.SqlCommand(Qstr, cn)

            dr = cmd.ExecuteReader()
            If dr.HasRows = True Then
                dr.Read()
                Try


                    mvATMCassitteValue(1) = dr("Cassitte1Value")
                    mvATMPhysicalCassitteValue(1) = mvATMCassitteValue(1)
                    mvATMCassitteType(1) = 1
                    mvATMCassitteValue(2) = dr("Cassitte2Value")
                    mvATMPhysicalCassitteValue(2) = mvATMCassitteValue(2)
                    mvATMCassitteType(2) = 2
                    mvATMCassitteValue(3) = dr("Cassitte3Value")
                    mvATMPhysicalCassitteValue(3) = mvATMCassitteValue(3)
                    mvATMCassitteType(3) = 3
                    mvATMCassitteValue(4) = dr("Cassitte4Value")
                    mvATMPhysicalCassitteValue(4) = mvATMCassitteValue(4)
                    mvATMCassitteType(4) = 4

                    mvMaxNotesCount = dr("MaxNotesCount")
                    MaximumValue = dr("MaximumAmount")
                    MinimumValue = dr("MinimumAmount")

                    mvCommissionLL1 = dr("StartAmount1")
                    mvCommissionUL1 = dr("EndAmount1")
                    mvCommissionValue1 = dr("CommissionAmount1")
                    mvCommissionLL2 = dr("StartAmount2")
                    mvCommissionUL2 = dr("EndAmount2")
                    mvCommissionValue2 = dr("CommissionAmount2")

                    mvMaximumDailyAmount = dr("MaximumDailyAmount")
                    mvMaximumMonthlyAmount = dr("MaximumMonthlyAmount")
                    mvMaximumDailyCount = dr("MaximumDailyCount")

                    Try
                        mvMaximumKeyTrials = dr("MaximumKeyTrials")
                    Catch ex As Exception
                        mvMaximumKeyTrials = 3
                    End Try
                    Try
                        mvMaxReActivateTimes = dr("MaxreActivateTimes")
                    Catch ex As Exception
                        mvMaxReActivateTimes = 3
                    End Try

                    Try
                        mvDeposittrxExpDays = dr("DepositTransactionExpirationDays")
                    Catch ex As Exception
                        mvDeposittrxExpDays = 2
                    End Try


                    Try
                        ReceiptLine1 = dr("ReceiptLine1")
                    Catch ex As Exception
                        ReceiptLine1 = ""
                    End Try
                    Try
                        ReceiptLine2 = dr("ReceiptLine2")
                    Catch ex As Exception
                        ReceiptLine2 = ""
                    End Try
                    Try
                        ReceiptLine3 = dr("ReceiptLine3")
                    Catch ex As Exception
                        ReceiptLine3 = ""
                    End Try




                    ret = ArrangeCassettesValues()

                    ret = ret
                Catch exr As Exception
                    log.loglog("GetCassettesInfo Error Exr:" & exr.ToString, False)
                    ret = 2
                End Try
            Else
                log.loglog("GetCassettesInfo Error:No Rows for Country,Bank,ATMId= [" & Country & "],[" & BankId & "],[" & ATMId & "]", False)
                ret = 1
            End If
            dr.Close()
            cn.Close()
            cn = Nothing
            cmd = Nothing
            dr = Nothing
            Return ret

        Catch ex As Exception
            log.loglog("GetCassettesInfo Error connection string:[" & CONFIGClass.ConnectionString & "] Ex:" & ex.ToString, False)
            Return 9
        End Try

    End Function



    Private Function GetBankInfo() As Integer
        Dim cn As System.Data.SqlClient.SqlConnection
        Dim cmd As System.Data.SqlClient.SqlCommand
        Dim dr As System.Data.SqlClient.SqlDataReader
        Dim Qstr As String

        'Dim noOfRows As Integer
        Dim ret As Integer

        Try
            Qstr = "select * from bank  where "
            Qstr = Qstr & "  bankcode='" & CONFIGClass.LocalBank & "' and countrycode='" & CONFIGClass.LocalCountry & "'"


            cn = New System.Data.SqlClient.SqlConnection(CONFIGClass.ConnectionString)
            cn.Open()


            cmd = New System.Data.SqlClient.SqlCommand(Qstr, cn)

            dr = cmd.ExecuteReader()
            If dr.HasRows = True Then
                dr.Read()
                Try




                    mvMaxNotesCount = dr("MaxNotesCount")
                    MaximumValue = dr("MaximumAmount")
                    MinimumValue = dr("MinimumAmount")

                    mvCommissionLL1 = dr("StartAmount1")
                    mvCommissionUL1 = dr("EndAmount1")
                    mvCommissionValue1 = dr("CommissionAmount1")
                    mvCommissionLL2 = dr("StartAmount2")
                    mvCommissionUL2 = dr("EndAmount2")
                    mvCommissionValue2 = dr("CommissionAmount2")

                    mvMaximumDailyAmount = dr("MaximumDailyAmount")
                    mvMaximumMonthlyAmount = dr("MaximumMonthlyAmount")
                    mvMaximumDailyCount = dr("MaximumDailyCount")

                    Try
                        mvMaximumKeyTrials = dr("MaximumKeyTrials")
                    Catch ex As Exception
                        mvMaximumKeyTrials = 3
                    End Try
                    Try
                        mvMaxReActivateTimes = dr("MaxreActivateTimes")
                    Catch ex As Exception
                        mvMaxReActivateTimes = 3
                    End Try

                    Try
                        mvDeposittrxExpDays = dr("DepositTransactionExpirationDays")
                    Catch ex As Exception
                        mvDeposittrxExpDays = 2
                    End Try


                    Try
                        ReceiptLine1 = dr("ReceiptLine1")
                    Catch ex As Exception
                        ReceiptLine1 = ""
                    End Try
                    Try
                        ReceiptLine2 = dr("ReceiptLine2")
                    Catch ex As Exception
                        ReceiptLine2 = ""
                    End Try
                    Try
                        ReceiptLine3 = dr("ReceiptLine3")
                    Catch ex As Exception
                        ReceiptLine3 = ""
                    End Try


                    ret = ret
                Catch exr As Exception
                    log.loglog("GetBankInfo Error Exr:" & exr.ToString, False)
                    ret = 2
                End Try
            Else
                log.loglog("GetBankInfo Error:No Rows Q=[" & Qstr & "]", False)
                ret = 1
            End If
            dr.Close()
            cn.Close()
            cn = Nothing
            cmd = Nothing
            dr = Nothing
            Return ret

        Catch ex As Exception
            log.loglog("GetBankInfo Error connection string:[" & CONFIGClass.ConnectionString & "] Ex:" & ex.ToString, False)
            Return 9
        End Try

    End Function

    Private Function GetDetailedResponseCode(ByVal pAction As String, ByRef DetailedErrorCode As Integer) As Integer
        Dim cn As System.Data.SqlClient.SqlConnection
        Dim cmd As System.Data.SqlClient.SqlCommand
        Dim dr As System.Data.SqlClient.SqlDataReader
        Dim Qstr As String

        Dim lDepositStatus As String = ""
        Dim lWithdrawalStatus As String = ""



        Try
            Qstr = "select * from transactions where "
            Qstr = Qstr & " transactioncode='" & Hosttransactioncode & "' "

            cn = New System.Data.SqlClient.SqlConnection(CONFIGClass.ConnectionString)
            cn.Open()

            DetailedErrorCode = cnst_ErrCode_BadStatusForRequiredRequestType

            cmd = New System.Data.SqlClient.SqlCommand(Qstr, cn)

            dr = cmd.ExecuteReader()
            If dr.HasRows = True Then
                dr.Read()

                lDepositStatus = dr("DepositStatus")
                If IsDBNull(dr("WithdrawalStatus")) Then
                    lWithdrawalStatus = ""
                Else
                    lWithdrawalStatus = dr("WithdrawalStatus")
                End If


                Select Case pAction
                    Case cnst_RequestType_RedemptionAutorization
                        If lDepositStatus <> cnst_ActionStatus_EXPIRED Then
                            DetailedErrorCode = cnst_ErrCode_NoConfirmedDeposistTransaction
                        Else
                            If lWithdrawalStatus <> cnst_ActionStatus_EXPIRED And lWithdrawalStatus <> cnst_ActionStatus_CANCELED Then
                                DetailedErrorCode = cnst_ErrCode_NotCanceledOrExpiredWithdrawalTransaction
                            End If
                        End If

                    Case cnst_RequestType_WithdrawalAuthorization
                        If lDepositStatus <> cnst_ActionStatus_CONFIRMED Then
                            DetailedErrorCode = cnst_ErrCode_NoConfirmedDeposistTransaction
                        Else
                            If lWithdrawalStatus <> "" And lWithdrawalStatus <> cnst_ActionStatus_CANCELED Then
                                DetailedErrorCode = cnst_ErrCode_NotCanceledOrNullWithdrawalTransaction
                            End If
                        End If

                End Select


            Else
                log.loglog("GetDetailedResponseCode, Error Transaction [" & Hosttransactioncode & "] is not found", False)
                DetailedErrorCode = cnst_ErrCode_OriginalRequestNotFound

            End If
            dr.Close()
            cn.Close()
            cn = Nothing
            cmd = Nothing
            dr = Nothing
            Return 0

        Catch ex As Exception
            log.loglog("GetDetailedResponseCode Error connection string [" & CONFIGClass.ConnectionString & "] Ex:" & ex.ToString, False)
            Return 9
        End Try

    End Function
    Private Function GetDetailedReActivationResponseCode(ByVal pAction As String, ByRef DetailedErrorCode As Integer) As Integer
        Dim cn As System.Data.SqlClient.SqlConnection
        Dim cmd As System.Data.SqlClient.SqlCommand
        Dim dr As System.Data.SqlClient.SqlDataReader
        Dim Qstr As String

        Dim lDepositStatus As String = ""
        Dim lWithdrawalStatus As String = ""
        Dim lReActivationCounter As Integer



        Try
            Qstr = "select * from transactions where "
            Qstr = Qstr & " transactioncode='" & Hosttransactioncode & "' "

            cn = New System.Data.SqlClient.SqlConnection(CONFIGClass.ConnectionString)
            cn.Open()

            DetailedErrorCode = cnst_ErrCode_BadStatusForRequiredRequestType

            cmd = New System.Data.SqlClient.SqlCommand(Qstr, cn)

            dr = cmd.ExecuteReader()
            If dr.HasRows = True Then
                dr.Read()

                lDepositStatus = dr("DepositStatus")
                If IsDBNull(dr("WithdrawalStatus")) Then
                    lWithdrawalStatus = ""
                Else
                    lWithdrawalStatus = dr("WithdrawalStatus")
                End If
                If IsDBNull(dr("ReActivationCounter")) Then
                    lReActivationCounter = 0
                Else
                    lReActivationCounter = dr("ReActivationCounter")
                End If
                log.loglog("GetDetailedReActivationResponseCode, lDepositStatus=[" & lDepositStatus & "] lWithdrawalStatus=[" & lWithdrawalStatus & "] lReActivationCounter=]" & lReActivationCounter, False)


                Select Case pAction
                    Case cnst_RequestType_DepositReActivation
                        If lDepositStatus <> cnst_ActionStatus_EXPIRED Then
                            DetailedErrorCode = cnst_ErrCode_NoConfirmedDeposistTransaction
                        Else
                            If lWithdrawalStatus <> cnst_ActionStatus_EXPIRED Then
                                DetailedErrorCode = cnst_ErrCode_NotCanceledOrExpiredWithdrawalTransaction
                            Else
                                If lReActivationCounter >= mvMaxReActivateTimes Then
                                    DetailedErrorCode = cnst_ErrCode_ReActivationCountExceeded
                                End If
                            End If
                        End If



                End Select


            Else
                log.loglog("GetDetailedReActivationResponseCode, Error Transaction [" & Hosttransactioncode & "] is not found", False)
                DetailedErrorCode = cnst_ErrCode_OriginalRequestNotFound
            End If
            dr.Close()
            cn.Close()
            cn = Nothing
            cmd = Nothing
            dr = Nothing
            Return 0

        Catch ex As Exception
            log.loglog("GetDetailedReActivationResponseCode Error connection string:[" & CONFIGClass.ConnectionString & "] Ex:" & ex.ToString, False)
            Return 9
        End Try

    End Function
    Private Function GetPreCalculatedDispensedNotesAndCommission(ByRef pDispensedAmount As Integer, ByRef pCommissionAmount As Integer, ByRef pNotesString As String) As Integer
        Dim cn As System.Data.SqlClient.SqlConnection
        Dim cmd As System.Data.SqlClient.SqlCommand
        Dim dr As System.Data.SqlClient.SqlDataReader
        Dim Qstr As String
        Dim rtrn As Integer
        Dim lDepositStatus As String = ""
        Dim lWithdrawalStatus As String = ""



        Try
            Qstr = "select * from transactionnestedactions where "
            Qstr = Qstr & " transactioncode='" & Hosttransactioncode & "' "
            Qstr = Qstr & "and action='" & cnst_RequestType_DepositAuthorization & "' "
            Qstr = Qstr & "and actionstatus='" & cnst_ActionStatus_AUTHORIZED & "' "


            cn = New System.Data.SqlClient.SqlConnection(CONFIGClass.ConnectionString)
            cn.Open()

            cmd = New System.Data.SqlClient.SqlCommand(Qstr, cn)

            dr = cmd.ExecuteReader()
            If dr.HasRows = True Then
                dr.Read()
                pDispensedAmount = dr("DispensedAmount")
                pCommissionAmount = dr("CommissionAmount")
                pNotesString = dr("DispensedNotes")
                mvATMPhysicalCassitteValue(1) = dr("Cassette1")
                mvATMPhysicalCassitteValue(2) = dr("Cassette2")
                mvATMPhysicalCassitteValue(3) = dr("Cassette3")
                mvATMPhysicalCassitteValue(4) = dr("Cassette4")
                rtrn = 0
            Else
                log.loglog("GetPreCalculatedDispensedNotesAndCommission, Error Transaction [" & Hosttransactioncode & "] is not found", False)
                rtrn = cnst_ErrCode_OriginalRequestDetailsNotFound
            End If
            dr.Close()
            cn.Close()
            cn = Nothing
            cmd = Nothing
            dr = Nothing
            Return rtrn

        Catch ex As Exception
            log.loglog("GetPreCalculatedDispensedNotesAndCommission Error connection string:[" & CONFIGClass.ConnectionString & "] Ex:" & ex.ToString, False)
            Return 9
        End Try

    End Function

    Private Function isExpired(ByVal pTrxCode As String) As Integer
        Dim cn As System.Data.SqlClient.SqlConnection
        Dim cmd As System.Data.SqlClient.SqlCommand
        Dim dr As System.Data.SqlClient.SqlDataReader
        Dim Qstr As String
        Dim rtrF As Integer
        Dim lDepositStatus As String = ""
        Dim lWithdrawalStatus As String = ""
        Dim blockingperiod As Long = 0


        Try


            Qstr = "select * from transactions where "
            Qstr = Qstr & " transactioncode='" & pTrxCode & "' "

            cn = New System.Data.SqlClient.SqlConnection(CONFIGClass.ConnectionString)
            cn.Open()
            cmd = New System.Data.SqlClient.SqlCommand(Qstr, cn)

            dr = cmd.ExecuteReader()
            If dr.HasRows = True Then
                dr.Read()

                lDepositStatus = dr("DepositStatus")
                If IsDBNull(dr("WithdrawalStatus")) Then
                    lWithdrawalStatus = ""
                Else
                    lWithdrawalStatus = dr("WithdrawalStatus")
                End If

                If lDepositStatus = cnst_ActionStatus_EXPIRED And (lWithdrawalStatus = cnst_ActionStatus_EXPIRED Or lWithdrawalStatus = cnst_ActionStatus_CANCELED) Then
                    rtrF = cnst_ErrCode_ExpiredTransaction
                Else
                    log.loglog("isExpired, Error Transaction [" & pTrxCode & "] is not expired, will concider it unExpired", False)
                    rtrF = cnst_ErrCode_NotExpiredDepositTransaction

                End If
                dr.Close()
                cn.Close()
                cn = Nothing
                cmd = Nothing
                dr = Nothing
                Return rtrF
            End If
        Catch ex As Exception
            log.loglog("isExpired Error connection string:[" & CONFIGClass.ConnectionString & "] Ex:" & ex.ToString, False)
            Return cnst_ErrCode_DataBaseError
        End Try

    End Function

    Private Function isExpiredNew(ByVal pTrxCode As String) As Integer
        Dim cn As System.Data.SqlClient.SqlConnection
        Dim cmd As System.Data.SqlClient.SqlCommand
        Dim dr As System.Data.SqlClient.SqlDataReader
        Dim Qstr As String
        Dim rtrF As Integer

        Try


            Qstr = "select * from NewTransactions where "
            Qstr = Qstr & " transactioncode='" & pTrxCode & "' "

            cn = New System.Data.SqlClient.SqlConnection(CONFIGClass.ConnectionString)
            cn.Open()
            cmd = New System.Data.SqlClient.SqlCommand(Qstr, cn)

            dr = cmd.ExecuteReader()
            If dr.HasRows = True Then
                dr.Read()


                Dim lWithdrawalStatus As String

                If IsDBNull(dr("WithdrawalStatus")) Then
                    lWithdrawalStatus = ""
                Else
                    lWithdrawalStatus = dr("WithdrawalStatus")
                End If

                If lWithdrawalStatus = cnst_ActionStatus_EXPIRED Or lWithdrawalStatus = cnst_ActionStatus_CANCELED Then
                    rtrF = cnst_ErrCode_ExpiredTransaction
                Else
                    log.loglog("isExpiredNew, Error Transaction [" & pTrxCode & "] is not expired, will concider it unExpired", False)
                    rtrF = cnst_ErrCode_NotExpiredDepositTransaction

                End If
                dr.Close()
                cn.Close()
                cn = Nothing
                cmd = Nothing
                dr = Nothing
                Return rtrF
            End If
        Catch ex As Exception
            log.loglog("isExpiredNew Error connection string:[" & CONFIGClass.ConnectionString & "] Ex:" & ex.ToString, False)
            Return cnst_ErrCode_DataBaseError
        End Try

    End Function

    '' ''Function GetDispensedNotes(ByVal pAmount As Integer, ByRef pDispensedAmount As Integer, ByRef pCommissionAmount As Integer, ByRef pNotesString As String) As Integer
    '' ''    Dim lAmount As Integer
    '' ''    Dim _4thAmount As Integer
    '' ''    Dim _3rdAmount As Integer
    '' ''    Dim _2ndAmount As Integer
    '' ''    Dim _1stAmount As Integer
    '' ''    Dim _NotesCount As Integer
    '' ''    Dim NotesCount(4) As Integer
    '' ''    Dim restAmt As Integer
    '' ''    Dim tmpSingl As Single
    '' ''    Dim i As Integer
    '' ''    Dim tmpStr As String
    '' ''    Dim lCommissionAmount As Integer
    '' ''    Dim AllDispensedNotesCount As Integer
    '' ''    Try

    '' ''        log.loglog("GetDispensedNotes, transfered Amount =[" & pAmount & "]", False)

    '' ''        NotesCount(1) = 0 : NotesCount(2) = 0 : NotesCount(3) = 0 : NotesCount(4) = 0
    '' ''        lCommissionAmount = mvCommissionValue
    '' ''        lAmount = pAmount - lCommissionAmount

    '' ''        log.loglog("GetDispensedNotes (1- Commission Value= " & mvCommissionValue & ") Amount=[" & lAmount & "] Commission Amount=" & lCommissionAmount, False)
    '' ''        tmpSingl = lAmount
    '' ''        restAmt = Int(tmpSingl * mvCommissionPercent / 100)


    '' ''        lAmount = lAmount - restAmt

    '' ''        lCommissionAmount += restAmt
    '' ''        log.loglog("GetDispensedNotes (2- Commission Percent= " & mvCommissionPercent & " Equ=" & restAmt & ") Amount=[" & lAmount & "] Commission Amount=" & lCommissionAmount, False)

    '' ''        restAmt = 0
    '' ''        restAmt = lAmount Mod mvATMCassitteValue(4)
    '' ''        _4thAmount = lAmount - restAmt

    '' ''        lAmount = restAmt
    '' ''        restAmt = lAmount Mod mvATMCassitteValue(3)
    '' ''        _3rdAmount = lAmount - restAmt

    '' ''        lAmount = restAmt
    '' ''        restAmt = lAmount Mod mvATMCassitteValue(2)
    '' ''        _2ndAmount = lAmount - restAmt

    '' ''        lAmount = restAmt
    '' ''        restAmt = lAmount Mod mvATMCassitteValue(1)
    '' ''        _1stAmount = lAmount - restAmt

    '' ''        lCommissionAmount += restAmt

    '' ''        log.loglog("GetDispensedNotes (3- Notes Fraction= " & restAmt & ") Amount=[" & lAmount & "] Commission Amount=" & lCommissionAmount, False)

    '' ''        _NotesCount = _4thAmount / mvATMCassitteValue(4)
    '' ''        NotesCount(mvATMCassitteType(4)) = _NotesCount

    '' ''        _NotesCount = _3rdAmount / mvATMCassitteValue(3)
    '' ''        NotesCount(mvATMCassitteType(3)) = _NotesCount

    '' ''        _NotesCount = _2ndAmount / mvATMCassitteValue(2)
    '' ''        NotesCount(mvATMCassitteType(2)) = _NotesCount

    '' ''        _NotesCount = _1stAmount / mvATMCassitteValue(1)
    '' ''        NotesCount(mvATMCassitteType(1)) = _NotesCount
    '' ''        tmpStr = ""

    '' ''        AllDispensedNotesCount = 0
    '' ''        For i = 1 To 4

    '' ''            tmpStr += NotesCount(i).ToString("00")
    '' ''            AllDispensedNotesCount += NotesCount(i)
    '' ''        Next
    '' ''        pNotesString = tmpStr
    '' ''        pCommissionAmount = lCommissionAmount
    '' ''        If AllDispensedNotesCount > mvMaxNotesCount Then
    '' ''            log.loglog("GetDispensedNotes Error Overall dispensed notes count (" & AllDispensedNotesCount & ") is more than Max Allowed (" & mvMaxNotesCount & ") ", False)
    '' ''            Return 8

    '' ''        End If
    '' ''        pDispensedAmount = _4thAmount + _3rdAmount + _2ndAmount + _1stAmount
    '' ''        log.loglog("GetDispensedNotes Finally Dispensed Amount=[" & pDispensedAmount & "] Commission=[" & pCommissionAmount & "] DispensedNores=[" & pNotesString & "] Notes=[" & mvATMPhysicalCassitteValue(1) & "," & mvATMPhysicalCassitteValue(2) & "," & mvATMPhysicalCassitteValue(3) & "," & mvATMPhysicalCassitteValue(4) & "]", False)


    '' ''        Return 0
    '' ''    Catch ex As Exception
    '' ''        log.loglog("GetDispensedNotes Error  Ex:" & ex.ToString, False)
    '' ''        Return 9
    '' ''    End Try



    '' ''End Function

    Function GetDispensedNotes_10(ByVal pAmount As Integer, ByRef pDispensedAmount As Integer, ByRef pCommissionAmount As Integer, ByRef pNotesString As String) As Integer
        Dim lAmount As Integer
        Dim _4thAmount As Integer
        Dim _3rdAmount As Integer
        Dim _2ndAmount As Integer
        Dim _1stAmount As Integer
        Dim _NotesCount As Integer
        Dim _50Amount As Integer
        Dim _20Amount As Integer
        Dim NotesCount(4) As Integer
        Dim restAmt As Integer
        Dim inproperAmount As Integer
        Dim i As Integer
        Dim tmpStr As String
        Dim lCommissionAmount As Integer

        Dim AllDispensedNotesCount As Integer


        Try


            log.loglog("GetDispensedNotes, transfered Amount =[" & pAmount & "]", False)

            NotesCount(1) = 0 : NotesCount(2) = 0 : NotesCount(3) = 0 : NotesCount(4) = 0
            If pAmount <= mvCommissionUL1 And pAmount >= mvCommissionLL1 Then
                lCommissionAmount = mvCommissionValue1
            ElseIf pAmount > mvCommissionLL2 And pAmount <= mvCommissionUL2 Then
                lCommissionAmount = mvCommissionValue2
            Else
                log.loglog("GetDispensedNotes_10 error, out of rang Amount ...", False)
                Return cnst_ErrCode_CalculateDispensedNotesError
            End If

            lAmount = pAmount - lCommissionAmount


            log.loglog("GetDispensedNotes_10 (1- Commission Amount=[" & lAmount & "] Commission Amount=" & lCommissionAmount, False)
            'if the request is from a teller then no dispensed notes calculations are required
            If mvISTeller = True Then
                pDispensedAmount = lAmount
                pNotesString = New String(" ", 8)
                pCommissionAmount = lCommissionAmount
                Return 0
            End If

            If mvATMCassitteValue(1) = 20 And _
               mvATMCassitteValue(2) = 50 Then
                ''If mvATMCassitteValue(1) > 10 Then 'adjust for l.e 10 denomination
                inproperAmount = lAmount Mod 100
                Select Case inproperAmount
                    Case 80, 60
                        lAmount = lAmount - inproperAmount
                    Case 10, 30
                        inproperAmount = inproperAmount + 100
                        lAmount = lAmount - inproperAmount
                    Case Else
                        inproperAmount = 0
                End Select
            Else
                inproperAmount = 0
            End If
            log.loglog("GetDispensedNotes_10  final amount=" & lAmount & " + " & inproperAmount, False)
            restAmt = 0
            restAmt = lAmount Mod mvATMCassitteValue(4)
            _4thAmount = lAmount - restAmt

            lAmount = restAmt
            restAmt = lAmount Mod mvATMCassitteValue(3)
            _3rdAmount = lAmount - restAmt

            lAmount = restAmt
            restAmt = lAmount Mod mvATMCassitteValue(2)
            _2ndAmount = lAmount - restAmt

            lAmount = restAmt
            restAmt = lAmount Mod mvATMCassitteValue(1)
            _1stAmount = lAmount - restAmt

            If restAmt > 0 Then
                lCommissionAmount += restAmt

            End If
            Select Case inproperAmount
                Case 80
                    _50Amount = 0
                    _20Amount = 80
                Case 60
                    _50Amount = 0
                    _20Amount = 60
                Case 110
                    _50Amount = 50
                    _20Amount = 60
                Case 130
                    _50Amount = 50
                    _20Amount = 80
                Case Else

                    _50Amount = 0
                    _20Amount = 0
            End Select

            If _50Amount > 0 And mvATMCassitteValue(2) <> 50 Then
                log.loglog("GetDispensedNotes_10 error, Second Cassette type is not 50 ...", False)
                Return cnst_ErrCode_CalculateDispensedNotesError


            End If
            If _20Amount > 0 And mvATMCassitteValue(1) <> 20 Then
                log.loglog("GetDispensedNotes_10 error, first Cassette type is not 20 ...", False)
                Return cnst_ErrCode_CalculateDispensedNotesError


            End If

            _1stAmount += _20Amount

            _2ndAmount += _50Amount




            log.loglog("GetDispensedNotes_10 Amount=" & lAmount & " Commission Amount=" & lCommissionAmount, False)
            log.loglog("GetDispensedNotes_10 Amount Cassette1=" & _1stAmount, False)
            log.loglog("GetDispensedNotes_10 Amount Cassette2=" & _2ndAmount, False)
            log.loglog("GetDispensedNotes_10 Amount Cassette3=" & _3rdAmount, False)
            log.loglog("GetDispensedNotes_10 Amount Cassette4=" & _4thAmount, False)


            _NotesCount = _4thAmount / mvATMCassitteValue(4)
            NotesCount(mvATMCassitteType(4)) = _NotesCount

            _NotesCount = _3rdAmount / mvATMCassitteValue(3)
            NotesCount(mvATMCassitteType(3)) = _NotesCount

            _NotesCount = _2ndAmount / mvATMCassitteValue(2)
            NotesCount(mvATMCassitteType(2)) = _NotesCount

            _NotesCount = _1stAmount / mvATMCassitteValue(1)
            NotesCount(mvATMCassitteType(1)) = _NotesCount
            tmpStr = ""

            AllDispensedNotesCount = 0
            For i = 1 To 4

                tmpStr += NotesCount(i).ToString("00")
                AllDispensedNotesCount += NotesCount(i)
            Next
            pNotesString = tmpStr
            pCommissionAmount = lCommissionAmount
            If AllDispensedNotesCount > mvMaxNotesCount Then
                log.loglog("GetDispensedNotes_10 Error Overall dispensed notes count (" & AllDispensedNotesCount & ") is more than Max Allowed (" & mvMaxNotesCount & ") ", False)
                Return 8

            End If
            pDispensedAmount = _4thAmount + _3rdAmount + _2ndAmount + _1stAmount
            log.loglog("GetDispensedNotes_10 Finally Dispensed Amount=[" & pDispensedAmount & "] Commission=[" & pCommissionAmount & "] DispensedNores=[" & pNotesString & "] Notes=[" & mvATMPhysicalCassitteValue(1) & "," & mvATMPhysicalCassitteValue(2) & "," & mvATMPhysicalCassitteValue(3) & "," & mvATMPhysicalCassitteValue(4), False)


            Return 0
        Catch ex As Exception
            log.loglog("GetDispensedNotes_10 Error  Ex:" & ex.ToString, False)
            Return 9
        End Try



    End Function

    Public Function DoRequestNew(incomingDataStr As String) As Integer
        Dim rtrn As Integer
        Dim enc As New NCRCrypto.NCRCrypto
        Dim lActionCode As String = ""
        rtrn = lNewParseRequest(incomingDataStr)
        If rtrn <> 0 Then
            Return rtrn
        End If


        '================================= Validate ATM IP Address if required
        If CONFIGClass.CheckATMIPMatch > 0 Then
            rtrn = lValidateATMIPAddress(ATMId, ATMIPAddress)
            If rtrn <> 0 Then
                ResponseCode = cnst_ErrCode_ATMIPAddressNotMatchedError.ToString("00000")
                lNewFormReply(mvOutGoingNewReplyData)
                Return 0
            End If
        End If
        '===========================================================================


        rtrn = GetCassettesInfo()
        If rtrn <> 0 Then
            ResponseCode = cnst_ErrCode_CassettesValuesErrors.ToString("00000")
            lNewFormReply(mvOutGoingNewReplyData)
            Return 0
        End If



        Select Case RequestType


            Case cnst_RequestType_NewWithdrawalAuthorization

                rtrn = isTransactionNew()
                If rtrn <> 0 Then
                    ResponseCode = rtrn.ToString("00000")
                    lNewFormReply(mvOutGoingNewReplyData)
                    Return 0
                End If

                rtrn = lCheckPINsNew()
                If rtrn <> 0 Then
                    ResponseCode = rtrn.ToString("00000")
                    lNewFormReply(mvOutGoingNewReplyData)
                    Return 0

                End If


                rtrn = isExpiredNew(Hosttransactioncode)
                If rtrn = cnst_ErrCode_ExpiredTransaction Then
                    ResponseCode = cnst_ErrCode_ExpiredTransaction.ToString("00000")
                    lNewFormReply(mvOutGoingNewReplyData)
                    Return 0

                End If


                'calclate Dispensed Notes Here

                DispensedNotes = ""
                DispensedAmount = 0
                CommissionAmount = 0

                rtrn = GetDispensedNotes_10(Amount, DispensedAmount, CommissionAmount, DispensedNotes)

                If rtrn <> 0 Then
                    ResponseCode = cnst_ErrCode_CalculateDispensedNotesError.ToString("00000")
                    lNewFormReply(mvOutGoingNewReplyData)
                    Return 0
                End If


                rtrn = lUpdateRequestNew(cnst_RequestType_WithdrawalAuthorization)

                If rtrn <> 0 Then
                    ResponseCode = rtrn.ToString("00000")
                    lNewFormReply(mvOutGoingNewReplyData)
                    Return 0
                End If

                ResponseCode = "00000"
                lNewFormReply(mvOutGoingNewReplyData)
                Return 0


            Case cnst_RequestType_NewWithdrawalConfirmation

                rtrn = isTransactionNew()
                If rtrn <> 0 Then
                    ResponseCode = rtrn.ToString("00000")
                    lNewFormReply(mvOutGoingNewReplyData)
                    Return 0
                End If

                rtrn = lCheckPINs_Teller()
                If rtrn <> 0 Then

                    ResponseCode = cnst_ErrCode_WrongPINs.ToString("00000")
                    lNewFormReply(mvOutGoingNewReplyData)
                    Return 0

                End If

                DispensedNotes = ""
                DispensedAmount = 0
                CommissionAmount = 0

                rtrn = GetDispensedNotes_10(Amount, DispensedAmount, CommissionAmount, DispensedNotes)

                If rtrn <> 0 Then
                    ResponseCode = cnst_ErrCode_CalculateDispensedNotesError.ToString("00000")
                    lNewFormReply(mvOutGoingNewReplyData)
                    Return 0
                End If


                lActionCode = cnst_RequestType_WithdrawalConfirmation
                If CONFIGClass.CheckForUncertainWithdrawalFlag = 1 Then
                    If ActionReason.ToUpper.Contains(CONFIGClass.UnCertainActionReason.ToUpper) = True Then
                        lActionCode = cnst_RequestType_WithdrawalUnCertain
                    End If
                End If

                rtrn = lUpdateRequestNew(lActionCode)
                BeneficiaryPIN = ""
                DepositorPIN = ""

                If rtrn <> 0 Then
                    ResponseCode = cnst_ErrCode_DataBaseError.ToString("00000")
                    lNewFormReply(mvOutGoingNewReplyData)
                    Return 0
                End If

                ResponseCode = "00000"
                lNewFormReply(mvOutGoingNewReplyData)
                Return 0


            Case cnst_RequestType_NewWithdrawalCancelation

                rtrn = isTransactionNew()
                If rtrn <> 0 Then
                    ResponseCode = rtrn.ToString("00000")
                    lNewFormReply(mvOutGoingNewReplyData)
                    Return 0
                End If

                rtrn = lCheckPINs_Teller()
                If rtrn <> 0 Then
                    ResponseCode = cnst_ErrCode_WrongPINs.ToString("00000")
                    lNewFormReply(mvOutGoingNewReplyData)
                    Return 0

                End If
                rtrn = lUpdateRequestNew(cnst_RequestType_WithdrawalCancelation)

                If rtrn <> 0 Then

                    ResponseCode = cnst_ErrCode_DataBaseError.ToString("00000")
                    lNewFormReply(mvOutGoingNewReplyData)
                    Return 0
                End If

                ResponseCode = "00000"
                lNewFormReply(mvOutGoingNewReplyData)
                Return 0

            Case Else
                ResponseCode = cnst_ErrCode_UnknownRequestType.ToString("00000")
                lNewFormReply(mvOutGoingNewReplyData)
                Return 0
        End Select
    End Function

    Private Function lNewParseRequest(lIncomingRequestData As String) As Integer
        Dim ll As Integer
        Dim curPos As Integer

        If lIncomingRequestData.Length = 0 Then
            log.loglog("New Parsing Request: bad request length", False)
            Return 1
        End If
        Try
            Dim FS = Chr(28)
            Dim lIncomingRequestDataString As String() = lIncomingRequestData.Split(New Char() {FS})
            RequestType = lIncomingRequestDataString(0)
            log.loglog("New Parsing RequestType: " & RequestType, False)

            ATMId = lIncomingRequestDataString(1)
            log.loglog("New Parsing ATMId: " & ATMId, False)

            ATMDateTime = DateTime.Parse(lIncomingRequestDataString(2))
            log.loglog("New Parsing ATMDateTime: " & ATMDateTime, False)

            TransactionSequence = lIncomingRequestDataString(3)
            log.loglog("New Parsing TransactionSequence: " & TransactionSequence & toString(), False)

            ResponseCode = lIncomingRequestDataString(4)
            log.loglog("New Parsing ResponseCode: " & ResponseCode, False)

            NationalID = lIncomingRequestDataString(5)
            log.loglog("New Parsing NationalID: " & NationalID, False)

            Return 0
        Catch ex As Exception

            log.loglog("New Parsing Request Error:" & ex.ToString, False)
            Return (9)
        End Try

    End Function

    Public Function DoRequest(ByVal pIncomingRequestData As String) As Integer
        Dim rtrn As Integer
        Dim cbRtrn As Integer
        Dim brtrn As Boolean
        Dim enc As New NCRCrypto.NCRCrypto
        Dim tmpDPin As String = ""
        Dim tmpBPIn As String = ""
        Dim tmpRPIn As String = ""
        Dim lActionCode As String = ""
        Dim tmpStr As String






        rtrn = lParseRequest(pIncomingRequestData)
        If rtrn <> 0 Then
            Return rtrn
        End If


        '================================= Validate ATM IP Address if required
        If CONFIGClass.CheckATMIPMatch > 0 Then
            rtrn = lValidateATMIPAddress(ATMId, ATMIPAddress)
            If rtrn <> 0 Then
                ResponseCode = cnst_ErrCode_ATMIPAddressNotMatchedError.ToString("00000")
                lFormReply(mvOutGoingReplyData)
                Return 0
            End If
        End If
        '===========================================================================


        log.loglog("Dorequest, Parsed Requestdata:" & vbNewLine & toString(), True)

        rtrn = GetCassettesInfo()
        If rtrn <> 0 Then
            ResponseCode = cnst_ErrCode_CassettesValuesErrors.ToString("00000")
            lFormReply(mvOutGoingReplyData)
            Return 0
        End If


        tmpDPin = DepositorPIN.Trim
        tmpBPIn = BeneficiaryPIN.Trim
        tmpRPIn = RedempltionPIN.Trim

        DepositorPIN = enc.eT3_Encrypt(tmpDPin)
        BeneficiaryPIN = enc.eT3_Encrypt(tmpBPIn)
        RedempltionPIN = enc.eT3_Encrypt(tmpRPIn)

        '=== using Amount as Depositor PIN =======================================
        If DepositorPIN = "" Then
            If CONFIGClass.UseAmountAsDPIN > 0 Then
                If RequestType = cnst_RequestType_WithdrawalAuthorization Or
                         RequestType = cnst_RequestType_WithdrawalConfirmation Or
                         RequestType = cnst_RequestType_WithdrawalCancelation Then
                    log.loglog("DoRequest, empty DepostorPin with UseAmountAsDPIN =[" & CONFIGClass.UseAmountAsDPIN & "] will use amount [" & Amount & "] to authenticate trx", False)
                    tmpStr = ""
                    rtrn = getDPINByAmount(Amount, tmpStr)
                    If rtrn <> 0 Then
                        ResponseCode = cnst_ErrCode_OriginalRequestDetailsNotFound.ToString("00000")
                        lFormReply(mvOutGoingReplyData)
                        Return 0
                    End If
                    DepositorPIN = tmpStr
                End If
            End If
        End If
        '========================================================================


        '=== using Beneficiary Mobile instead of transacxtion code DeMO purbos
        If CONFIGClass.UseBeneficiaryAsId > 0 Then
            If RequestType = cnst_RequestType_WithdrawalAuthorization Then
                tmpStr = ""
                rtrn = getTransactionCodeByBeneficiary(Hosttransactioncode.Trim, tmpStr)
                If rtrn <> 0 Then
                    ResponseCode = cnst_ErrCode_OriginalRequestDetailsNotFound.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0
                End If
                Hosttransactioncode = tmpStr
            End If
        End If
        '========================================================================




        Select Case RequestType
            Case cnst_RequestType_CheckRegistration
                If CONFIGClass.DepositorMustRegister = 1 Then
                    brtrn = IsRegisteredMobile(DepositorMobile)
                    If brtrn <> True Then
                        ResponseCode = cnst_ErrCode_UnRegisterdDepositor.ToString("00000")
                        lFormReply(mvOutGoingReplyData)
                        Return 0
                    End If
                End If
                If CONFIGClass.BeneficiaryMustRegister = 1 Then
                    If CONFIGClass.WhenCheckBeneficiaryRegisteration = 1 Then
                        brtrn = IsRegisteredMobile(BeneficiaryMobile)
                        If brtrn <> True Then
                            ResponseCode = cnst_ErrCode_UnRegisterdBeneficiery.ToString("00000")
                            lFormReply(mvOutGoingReplyData)
                            Return 0
                        End If
                    End If
                End If
                ResponseCode = "00000"
                lFormReply(mvOutGoingReplyData)
                Return 0

            Case cnst_RequestType_DepositAuthorization
                If CONFIGClass.DepositorMustRegister = 1 Then
                    brtrn = IsRegisteredMobile(DepositorMobile)
                    If brtrn <> True Then
                        ResponseCode = cnst_ErrCode_UnRegisterdDepositor.ToString("00000")
                        lFormReply(mvOutGoingReplyData)
                        Return 0
                    End If
                End If
                If CONFIGClass.BeneficiaryMustRegister = 1 Then
                    If CONFIGClass.WhenCheckBeneficiaryRegisteration = 1 Then
                        brtrn = IsRegisteredMobile(BeneficiaryMobile)
                        If brtrn <> True Then
                            ResponseCode = cnst_ErrCode_UnRegisterdBeneficiery.ToString("00000")
                            lFormReply(mvOutGoingReplyData)
                            Return 0
                        End If
                    End If
                End If
                'check for blocking mobilrs
                rtrn = checkBlockedMobile(DepositorMobile, 0)
                If rtrn <> 0 Then
                    ResponseCode = cnst_ErrCode_BlockedDepositor.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0
                End If
                rtrn = checkBlockedMobile(BeneficiaryMobile, 1)
                If rtrn <> 0 Then
                    ResponseCode = cnst_ErrCode_BlockedBenificiary.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0
                End If
                '' kareem Nour 17/2/2015
                If CONFIGClass.CheckAmountFlag > 0 Then
                    rtrn = CheckAmount(Amount)
                    If rtrn <> 0 Then
                        ResponseCode = cnst_ErrCode_ErrAmountReply.ToString("00000")
                        lFormReply(mvOutGoingReplyData)
                        Return 0
                    End If
                End If


                '' end kareem Nour
                'pre step is to check daily max amount per mobile
                rtrn = lValidateMaximumDailyAmount(DepositorMobile, "D", Amount)
                If rtrn <> 0 Then
                    log.loglog("DoRequest, lValidateMaximumDailyAmount retuen [" & rtrn & "] for depositor mobile [" & DepositorMobile & "] and amount [" & Amount & "]", False)
                    ResponseCode = cnst_ErrCode_TodayTotalDepositAmountExceedsMaxAllowedValue.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0
                End If
                If CONFIGClass.ApplyLimitsOnBeneficiary > 0 Then
                    'pre step is to check daily max amount per mobile
                    rtrn = lValidateMaximumDailyAmount(BeneficiaryMobile, "B", Amount)
                    If rtrn <> 0 Then
                        log.loglog("DoRequest, lValidateMaximumDailyAmount retuen [" & rtrn & "] for Beneficiary Mobile [" & DepositorMobile & "] and amount [" & Amount & "]", False)
                        ResponseCode = cnst_ErrCode_TodayTotalDepositAmountExceedsMaxAllowedValue.ToString("00000")
                        lFormReply(mvOutGoingReplyData)
                        Return 0
                    End If

                End If



                'pre step is to check daily max count per mobile
                rtrn = lValidateMaximumDailyCount(DepositorMobile, "D")
                If rtrn <> 0 Then
                    log.loglog("DoRequest, lValidateMaximumDailyCount retuen [" & rtrn & "] for depositor mobile [" & DepositorMobile & "]", False)
                    ResponseCode = cnst_ErrCode_TodayTotalDepositAmountExceedsMaxAllowedCount.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0
                End If
                If CONFIGClass.ApplyLimitsOnBeneficiary > 0 Then
                    'pre step is to check daily max count per mobile
                    rtrn = lValidateMaximumDailyCount(BeneficiaryMobile, "B")
                    If rtrn <> 0 Then
                        log.loglog("DoRequest, lValidateMaximumDailyCount retuen [" & rtrn & "] for Bebeficiary mobile [" & DepositorMobile & "]", False)
                        ResponseCode = cnst_ErrCode_TodayTotalDepositAmountExceedsMaxAllowedCount.ToString("00000")
                        lFormReply(mvOutGoingReplyData)
                        Return 0
                    End If
                End If


                'pre step is to check Monthly max max per mobile
                rtrn = lValidateMaximumMonthlyAmount(DepositorMobile, "D", Amount)
                If rtrn <> 0 Then
                    log.loglog("DoRequest, lValidateMaximumMonthlyAmount retuen [" & rtrn & "] for depositor mobile [" & DepositorMobile & "] and amount [" & Amount & "]", False)
                    ResponseCode = cnst_ErrCode_ToMonthTotalDepositAmountExceedsMaxMonthlyAllowedValue.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0
                End If
                If CONFIGClass.ApplyLimitsOnBeneficiary > 0 Then
                    'pre step is to check Monthly max max per mobile
                    rtrn = lValidateMaximumMonthlyAmount(BeneficiaryMobile, "B", Amount)
                    If rtrn <> 0 Then
                        log.loglog("DoRequest, lValidateMaximumMonthlyAmount retuen [" & rtrn & "] for Beneficiary Mobile  [" & DepositorMobile & "] and amount [" & Amount & "]", False)
                        ResponseCode = cnst_ErrCode_ToMonthTotalDepositAmountExceedsMaxMonthlyAllowedValue.ToString("00000")
                        lFormReply(mvOutGoingReplyData)
                        Return 0
                    End If
                End If

                'first step is to generate host transaction code
                rtrn = BuildTrxCodeClass.GetTransactionCode(Hosttransactioncode, LockThread)
                If rtrn <> 0 Then
                    ResponseCode = cnst_ErrCode_CanNotGetHostTrxCode.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0
                End If
                'generate random PINs
                rtrn = BuildTrxCodeClass.GetRandomPIN(DepositorPIN, CONFIGClass.PINLength)
                If rtrn <> 0 Then
                    ResponseCode = cnst_ErrCode_CanNotGetPINs.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0
                End If
                rtrn = BuildTrxCodeClass.GetRandomPIN(BeneficiaryPIN, CONFIGClass.PINLength)
                If rtrn <> 0 Then
                    ResponseCode = cnst_ErrCode_CanNotGetPINs.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0
                End If
                tmpDPin = DepositorPIN
                tmpBPIn = BeneficiaryPIN

                DepositorPIN = enc.eT3_Encrypt(DepositorPIN)
                BeneficiaryPIN = enc.eT3_Encrypt(BeneficiaryPIN)
                log.loglog("DoRequest, DepostorPin=[" & tmpDPin & "] BenificiaryPin=[" & tmpBPIn & "]", False)



                DispensedNotes = ""
                DispensedAmount = 0
                CommissionAmount = 0
                rtrn = GetDispensedNotes_10(Amount, DispensedAmount, CommissionAmount, DispensedNotes)
                If rtrn <> 0 Then
                    ResponseCode = cnst_ErrCode_CalculateDispensedNotesError.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0
                End If


                rtrn = lInsertRequest()
                If rtrn <> 0 Then
                    ResponseCode = cnst_ErrCode_DataBaseError.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0
                End If




                BeneficiaryPIN = ""
                'No PINs will be sent back to client
                DepositorPIN = tmpDPin
                ResponseCode = "00000"
                lFormReply(mvOutGoingReplyData)
                Return 0


            Case cnst_RequestType_CBDebitAmountAuthorization

                mvDepositCurrency = ""
                rtrn = isTransaction(mvDepositCurrency)
                If rtrn <> 0 Then
                    BeneficiaryPIN = ""
                    DepositorPIN = ""
                    ResponseCode = cnst_ErrCode_OriginalRequestNotFound.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0
                End If




                CBTDataElement38 = ""
                CBTDataElement39 = ""

                cbRtrn = DODebitAccountAuthorization()

                ActionStatus = (cnst_ErrCode_DebitAccountAuthorizationError + cbRtrn).ToString
                rtrn = lUpdateRequest(cnst_RequestType_CBDebitAmountAuthorization)
                BeneficiaryPIN = ""
                DepositorPIN = ""

                If cbRtrn <> 0 Then
                    ResponseCode = (cnst_ErrCode_DebitAccountAuthorizationError + cbRtrn).ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0
                End If
                If rtrn <> 0 Then
                    ResponseCode = cnst_ErrCode_DataBaseError.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0
                End If

                ResponseCode = "00000"
                lFormReply(mvOutGoingReplyData)
                Return 0



            Case cnst_RequestType_DepositConfirmation

                mvDepositCurrency = ""
                rtrn = isTransaction(mvDepositCurrency)
                If rtrn <> 0 Then
                    BeneficiaryPIN = ""
                    DepositorPIN = ""
                    ResponseCode = cnst_ErrCode_OriginalRequestNotFound.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0
                End If


                rtrn = lCheckPINs()
                If rtrn <> 0 Then
                    BeneficiaryPIN = ""
                    DepositorPIN = ""
                    ResponseCode = cnst_ErrCode_WrongPINs.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0

                End If


                DispensedNotes = ""
                DispensedAmount = 0
                CommissionAmount = 0
                rtrn = GetDispensedNotes_10(Amount, DispensedAmount, CommissionAmount, DispensedNotes)
                If rtrn <> 0 Then
                    ResponseCode = cnst_ErrCode_CalculateDispensedNotesError.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0
                End If

                rtrn = lUpdateRequest(cnst_RequestType_DepositConfirmation)
                BeneficiaryPIN = ""
                DepositorPIN = ""

                If rtrn <> 0 Then
                    ResponseCode = cnst_ErrCode_DataBaseError.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0
                End If

                ResponseCode = "00000"
                lFormReply(mvOutGoingReplyData)
                Return 0



            Case cnst_RequestType_DepositCancelation

                rtrn = isTransaction(mvDepositCurrency)
                If rtrn <> 0 Then
                    BeneficiaryPIN = ""
                    DepositorPIN = ""
                    ResponseCode = cnst_ErrCode_OriginalRequestNotFound.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0
                End If


                rtrn = lCheckPINs()
                If rtrn <> 0 Then
                    BeneficiaryPIN = ""
                    DepositorPIN = ""
                    ResponseCode = cnst_ErrCode_WrongPINs.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0
                End If
                rtrn = lUpdateRequest(cnst_RequestType_DepositCancelation)
                BeneficiaryPIN = ""
                DepositorPIN = ""

                If rtrn <> 0 Then
                    ResponseCode = cnst_ErrCode_DataBaseError.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0
                End If

                ResponseCode = "00000"
                lFormReply(mvOutGoingReplyData)
                Return 0

            Case cnst_RequestType_WithdrawalAuthorization

                mvDepositCurrency = ""
                rtrn = isTransaction(mvDepositCurrency)
                If rtrn <> 0 Then
                    BeneficiaryPIN = ""
                    DepositorPIN = ""
                    ResponseCode = rtrn.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0
                End If
                If CONFIGClass.BeneficiaryMustRegister = 1 Then
                    If CONFIGClass.WhenCheckBeneficiaryRegisteration = 2 Then
                        brtrn = IsRegisteredMobile(BeneficiaryMobile)
                        If brtrn <> True Then
                            ResponseCode = cnst_ErrCode_UnRegisterdBeneficiery.ToString("00000")
                            lFormReply(mvOutGoingReplyData)
                            Return 0
                        End If
                    End If
                End If

                rtrn = lCheckPINs_Teller()
                If rtrn <> 0 Then
                    BeneficiaryPIN = ""
                    DepositorPIN = ""
                    ResponseCode = cnst_ErrCode_WrongPINs.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0

                End If

                rtrn = isExpired(Hosttransactioncode)
                If rtrn = cnst_ErrCode_ExpiredTransaction Then
                    BeneficiaryPIN = ""
                    DepositorPIN = ""

                    ResponseCode = cnst_ErrCode_ExpiredTransaction.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0

                End If


                'calclate Dispensed Notes Here

                DispensedNotes = ""
                DispensedAmount = 0
                CommissionAmount = 0

                'If Currency = mvDepositCurrency Then
                'rtrn = GetPreCalculatedDispensedNotesAndCommission(DispensedAmount, CommissionAmount, DispensedNotes)
                'Else
                rtrn = GetDispensedNotes_10(Amount, DispensedAmount, CommissionAmount, DispensedNotes)
                'End If


                If rtrn <> 0 Then
                    ResponseCode = cnst_ErrCode_CalculateDispensedNotesError.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0
                End If


                rtrn = lUpdateRequest(cnst_RequestType_WithdrawalAuthorization)
                BeneficiaryPIN = ""
                DepositorPIN = ""

                If rtrn <> 0 Then
                    ResponseCode = rtrn.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0
                End If

                ResponseCode = "00000"
                lFormReply(mvOutGoingReplyData)
                Return 0


            Case cnst_RequestType_WithdrawalConfirmation
                mvDepositCurrency = ""

                rtrn = isTransaction(mvDepositCurrency)
                If rtrn <> 0 Then
                    BeneficiaryPIN = ""
                    DepositorPIN = ""
                    ResponseCode = rtrn.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0
                End If

                rtrn = lCheckPINs_Teller()
                If rtrn <> 0 Then
                    BeneficiaryPIN = ""
                    DepositorPIN = ""
                    ResponseCode = cnst_ErrCode_WrongPINs.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0

                End If

                DispensedNotes = ""
                DispensedAmount = 0
                CommissionAmount = 0

                rtrn = GetDispensedNotes_10(Amount, DispensedAmount, CommissionAmount, DispensedNotes)

                If rtrn <> 0 Then
                    ResponseCode = cnst_ErrCode_CalculateDispensedNotesError.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0
                End If


                lActionCode = cnst_RequestType_WithdrawalConfirmation
                If CONFIGClass.CheckForUncertainWithdrawalFlag = 1 Then
                    If ActionReason.ToUpper.Contains(CONFIGClass.UnCertainActionReason.ToUpper) = True Then
                        lActionCode = cnst_RequestType_WithdrawalUnCertain
                    End If
                End If

                rtrn = lUpdateRequest(lActionCode)
                BeneficiaryPIN = ""
                DepositorPIN = ""

                If rtrn <> 0 Then
                    ResponseCode = cnst_ErrCode_DataBaseError.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0
                End If

                ResponseCode = "00000"
                lFormReply(mvOutGoingReplyData)
                Return 0


            Case cnst_RequestType_WithdrawalCancelation
                mvDepositCurrency = ""
                rtrn = isTransaction(mvDepositCurrency)
                If rtrn <> 0 Then
                    BeneficiaryPIN = ""
                    DepositorPIN = ""
                    ResponseCode = rtrn.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0
                End If

                rtrn = lCheckPINs_Teller()
                If rtrn <> 0 Then
                    BeneficiaryPIN = ""
                    DepositorPIN = ""
                    ResponseCode = cnst_ErrCode_WrongPINs.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0

                End If
                rtrn = lUpdateRequest(cnst_RequestType_WithdrawalCancelation)
                BeneficiaryPIN = ""
                DepositorPIN = ""

                If rtrn <> 0 Then

                    ResponseCode = cnst_ErrCode_DataBaseError.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0
                End If

                ResponseCode = "00000"
                lFormReply(mvOutGoingReplyData)
                Return 0


            Case cnst_RequestType_RedemptionAutorization
                'mvDepositCurrency = ""
                'rtrn = isTransactionRedemption(mvDepositCurrency)
                'If rtrn <> 0 Then
                '    BeneficiaryPIN = ""
                '    DepositorPIN = ""
                '    ResponseCode = rtrn.ToString("00000")
                '    lFormReply(mvOutGoingReplyData)
                '    Return 0
                'End If

                rtrn = lCheckPINs_Redemption()
                If rtrn <> 0 Then
                    BeneficiaryPIN = ""
                    DepositorPIN = ""
                    RedempltionPIN = ""
                    ResponseCode = cnst_ErrCode_WrongPINs.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0

                End If

                'calclate Dispensed Notes Here

                DispensedNotes = ""
                DispensedAmount = 0
                CommissionAmount = 0

                ' If mvDepositCurrency = Currency Then
                'rtrn = GetPreCalculatedDispensedNotesAndCommission(DispensedAmount, CommissionAmount, DispensedNotes)
                'Else
                rtrn = GetDispensedNotes_10(Amount, DispensedAmount, CommissionAmount, DispensedNotes)
                'End If


                If rtrn <> 0 Then
                    ResponseCode = cnst_ErrCode_CalculateDispensedNotesError.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0
                End If

                rtrn = lUpdateRequest(cnst_RequestType_RedemptionAutorization)
                BeneficiaryPIN = ""
                DepositorPIN = ""
                RedempltionPIN = ""
                If rtrn <> 0 Then
                    ResponseCode = rtrn.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0
                End If

                ResponseCode = "00000"
                lFormReply(mvOutGoingReplyData)
                Return 0
            Case cnst_RequestType_RedemptionConfirmation
                mvDepositCurrency = ""
                rtrn = isTransaction(mvDepositCurrency)
                If rtrn <> 0 Then
                    BeneficiaryPIN = ""
                    DepositorPIN = ""
                    ResponseCode = rtrn.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0
                End If

                rtrn = lCheckPINs_Redemption()
                If rtrn <> 0 Then
                    BeneficiaryPIN = ""
                    DepositorPIN = ""
                    RedempltionPIN = ""
                    ResponseCode = cnst_ErrCode_WrongPINs.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0

                End If

                DispensedNotes = ""
                DispensedAmount = 0
                CommissionAmount = 0
                rtrn = GetDispensedNotes_10(Amount, DispensedAmount, CommissionAmount, DispensedNotes)
                If rtrn <> 0 Then
                    ResponseCode = cnst_ErrCode_CalculateDispensedNotesError.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0
                End If

                lActionCode = cnst_RequestType_RedemptionConfirmation
                If CONFIGClass.CheckForUncertainWithdrawalFlag = 1 Then
                    If ActionReason.ToUpper.Contains(CONFIGClass.UnCertainActionReason.ToUpper) = True Then
                        lActionCode = cnst_RequestType_RedemptionUnCertain
                    End If
                End If

                rtrn = lUpdateRequest(lActionCode)
                BeneficiaryPIN = ""
                DepositorPIN = ""
                RedempltionPIN = ""
                If rtrn <> 0 Then
                    ResponseCode = cnst_ErrCode_DataBaseError.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0
                End If

                ResponseCode = "00000"
                lFormReply(mvOutGoingReplyData)
                Return 0

            Case cnst_RequestType_RedemptionCancelation
                mvDepositCurrency = ""
                rtrn = isTransaction(mvDepositCurrency)
                If rtrn <> 0 Then
                    BeneficiaryPIN = ""
                    DepositorPIN = ""
                    ResponseCode = rtrn.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0
                End If

                rtrn = lCheckPINs_Redemption()
                If rtrn <> 0 Then
                    BeneficiaryPIN = ""
                    DepositorPIN = ""
                    RedempltionPIN = ""
                    ResponseCode = cnst_ErrCode_WrongPINs.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0
                End If
                rtrn = lUpdateRequest(cnst_RequestType_RedemptionCancelation)
                BeneficiaryPIN = ""
                DepositorPIN = ""
                RedempltionPIN = ""
                If rtrn <> 0 Then
                    ResponseCode = cnst_ErrCode_DataBaseError.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0
                End If

                ResponseCode = "00000"
                lFormReply(mvOutGoingReplyData)
                Return 0




            Case cnst_RequestType_DepositReActivation

                If mvISTeller = False Then
                    BeneficiaryPIN = ""
                    DepositorPIN = ""
                    RedempltionPIN = ""
                    ResponseCode = cnst_ErrCode_ATMIPAddressIsNotTeller.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0

                End If
                mvDepositCurrency = ""
                rtrn = isTransaction(mvDepositCurrency)
                If rtrn <> 0 Then
                    BeneficiaryPIN = ""
                    DepositorPIN = ""
                    ResponseCode = rtrn.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0
                End If


                rtrn = lCheckPINs_Teller()

                If rtrn <> 0 Then
                    BeneficiaryPIN = ""
                    DepositorPIN = ""
                    RedempltionPIN = ""
                    ResponseCode = cnst_ErrCode_WrongPINs.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0

                End If
                rtrn = lUpdateRequest(RequestType)
                BeneficiaryPIN = ""
                DepositorPIN = ""
                RedempltionPIN = ""
                If rtrn <> 0 Then

                    ResponseCode = rtrn.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0
                End If

                ResponseCode = "00000"
                lFormReply(mvOutGoingReplyData)
                Return 0
            Case cnst_RequestType_DepositHold,
                                 cnst_RequestType_DepositUnHold, cnst_RequestType_UnBLock,
                                 cnst_RequestType_ManuallyConfirm, cnst_RequestType_ExpiredManuallyConfirm, cnst_RequestType_DepositForceExpiration

                If mvISTeller = False Then
                    BeneficiaryPIN = ""
                    DepositorPIN = ""
                    RedempltionPIN = ""
                    ResponseCode = cnst_ErrCode_ATMIPAddressIsNotTeller.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0

                End If
                mvDepositCurrency = ""
                rtrn = isTransaction(mvDepositCurrency)
                If rtrn <> 0 Then
                    BeneficiaryPIN = ""
                    DepositorPIN = ""
                    ResponseCode = rtrn.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0
                End If


                rtrn = lCheckPINs_Teller()

                If rtrn <> 0 Then
                    BeneficiaryPIN = ""
                    DepositorPIN = ""
                    RedempltionPIN = ""
                    ResponseCode = cnst_ErrCode_WrongPINs.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0

                End If
                rtrn = lUpdateRequest(RequestType)
                BeneficiaryPIN = ""
                DepositorPIN = ""
                RedempltionPIN = ""
                If rtrn <> 0 Then

                    ResponseCode = cnst_ErrCode_DataBaseError.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0
                End If

                ResponseCode = "00000"
                lFormReply(mvOutGoingReplyData)
                Return 0



            Case cnst_RequestType_ResendSMSBoth, cnst_RequestType_ResendSMSDepositor, cnst_RequestType_ResendSMSBeneficiery, cnst_RequestType_ResendSMSRedemption

                If mvISTeller = False Then
                    BeneficiaryPIN = ""
                    DepositorPIN = ""
                    ResponseCode = cnst_ErrCode_ATMIPAddressIsNotTeller.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0

                End If

                rtrn = lUpdateRequest(RequestType)
                BeneficiaryPIN = ""
                DepositorPIN = ""

                If rtrn <> 0 Then

                    ResponseCode = cnst_ErrCode_DataBaseError.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0
                End If

                ResponseCode = "00000"
                lFormReply(mvOutGoingReplyData)
                Return 0



            Case cnst_RequestType_CommissionInformation

                rtrn = GetDispensedNotes_10(Amount, DispensedAmount, CommissionAmount, DispensedNotes)

                If rtrn <> 0 Then
                    ResponseCode = cnst_ErrCode_CalculateDispensedNotesError.ToString("00000")
                    lFormReply(mvOutGoingReplyData)
                    Return 0
                End If
                ReceiptLine1 = (mvCommissionLL1 & Space(10)).Substring(0, 10)
                ReceiptLine1 += (mvCommissionUL1 & Space(10)).Substring(0, 10)
                ReceiptLine1 += (mvCommissionValue1 & Space(10)).Substring(0, 10)
                ReceiptLine1 += Space(10)

                ReceiptLine2 = (mvCommissionLL2 & Space(10)).Substring(0, 10)
                ReceiptLine2 += (mvCommissionUL2 & Space(10)).Substring(0, 10)
                ReceiptLine2 += (mvCommissionValue2 & Space(10)).Substring(0, 10)
                ReceiptLine2 += Space(10)


                ResponseCode = rtrn.ToString("00000")
                lFormReply(mvOutGoingReplyData)
                Return 0
            Case cnst_RequestType_ResetKeyTrials
                rtrn = ResetkTransactionKeyTrials()
                ResponseCode = rtrn.ToString("00000")
                lFormReply(mvOutGoingReplyData)
                Return 0
            Case Else
                ResponseCode = cnst_ErrCode_UnknownRequestType.ToString("00000")
                lFormReply(mvOutGoingReplyData)
                Return 0
        End Select

    End Function
    Private Function lInsertRequest() As Integer
        Dim cn As System.Data.SqlClient.SqlConnection
        Dim cmd As System.Data.SqlClient.SqlCommand
        Dim Qstr As String = ""
        Dim RowsAffected As Integer
        Dim Subquery As String = ""
        Dim sqltrx As SqlClient.SqlTransaction
        Try
            Qstr = "insert into transactions (TransactionCode, CountryCode, BankCode ,  ATMId  ,RequestType, ATMDate, ATMTime,ATMTrxSequence, DepositorMobile,DepositorPIN,BeneficiaryMobile,BeneficiaryPIN,Amount, CurrencyCode ,DepositDateTime,DepositStatus,SMSLanguage,depositorId)"
            Qstr = Qstr & " Values ('" & Hosttransactioncode & "','" & Country & "','" & BankId & "','" & ATMId & "','" & RequestType & "','" & ATMDate & "','" & ATMTime & "' ,'" & TransactionSequence & "','" & DepositorMobile & "','" & DepositorPIN & "','" & BeneficiaryMobile & "','" & BeneficiaryPIN & "'," & Amount & ",'" & Currency & "',getdate(),'" & cnst_ActionStatus_AUTHORIZED & "','" & SMSLanguage & "','" & ExtraData & "')"




            Subquery = " Insert into TransactionNestedActions (TransactionCode,action,ActionDateTime,ActionReason,ActionStatus,DispensedNotes,DispensedAmount,CommissionAmount,Cassette1,Cassette2,Cassette3,Cassette4,CountryCode,BankCode,ATMId,ATMTrxSequence,DispensedCurrencyCode,DispensedRate) "
            Subquery += " values ('" & Hosttransactioncode & "','" & cnst_RequestType_DepositAuthorization & "',getdate(), '" & ActionReason & "','" & cnst_ActionStatus_AUTHORIZED & "','" & DispensedNotes & "'," & DispensedAmount & "," & CommissionAmount & "," & mvATMPhysicalCassitteValue(1) & "," & mvATMPhysicalCassitteValue(2) & "," & mvATMPhysicalCassitteValue(3) & "," & mvATMPhysicalCassitteValue(4) & ",'" & Country & "','" & BankId & "','" & ATMId & "','" & TransactionSequence & "','" & Currency & "'," & DispensedRate & " )"



            cn = New System.Data.SqlClient.SqlConnection(CONFIGClass.ConnectionString)
            cn.Open()
            sqltrx = cn.BeginTransaction
            cmd = New System.Data.SqlClient.SqlCommand(Qstr, cn)
            cmd.Transaction = sqltrx
            RowsAffected = cmd.ExecuteNonQuery()
            If RowsAffected < 1 Then
                log.loglog("lInsertRequest Error with Qstr=[" & Qstr & "]  no rows affected", False)
                sqltrx.Rollback()
                Return cnst_ErrCode_DataBaseError

            Else
                cmd.CommandText = Subquery
                RowsAffected = cmd.ExecuteNonQuery()
                If RowsAffected < 1 Then
                    log.loglog("lInsertRequest Error with Subq=[" & Subquery & "]  no rows affected", False)
                    sqltrx.Rollback()
                    Return cnst_ErrCode_DataBaseError


                End If
            End If
            sqltrx.Commit()
            cn.Close()
            cn = Nothing
            cmd = Nothing
            Return 0
        Catch ex As Exception
            log.loglog("lInsertRequest Error with Qstr=[" & Qstr & "]" & vbNewLine & "SUBQ=[" & Subquery & "]" & vbNewLine & "Ex:" & ex.ToString, False)
            Return 9
        End Try

    End Function

    Private Function lInsertKeyCkecTrial() As Integer
        Dim cn As System.Data.SqlClient.SqlConnection
        Dim cmd As System.Data.SqlClient.SqlCommand
        Dim Qstr As String = ""
        Dim RowsAffected As Integer
        Try
            Qstr = "insert into TransactionKeyCheckTrials (TransactionCode)"
            Qstr = Qstr & " Values ('" & Hosttransactioncode & "')"





            cn = New System.Data.SqlClient.SqlConnection(CONFIGClass.ConnectionString)
            cn.Open()
            cmd = New System.Data.SqlClient.SqlCommand(Qstr, cn)
            RowsAffected = cmd.ExecuteNonQuery()
            If RowsAffected < 1 Then
                log.loglog("lInsertKeyCkecTrial Error with Qstr=[" & Qstr & "]  no rows affected", False)
                Return cnst_ErrCode_DataBaseError
            End If
            cn.Close()
            cn = Nothing
            cmd = Nothing
            Return 0
        Catch ex As Exception
            log.loglog("lInsertKeyCkecTrial Error with Qstr=[" & Qstr & "]" & vbNewLine & "Ex:" & ex.ToString, False)
            Return 9
        End Try

    End Function

    Private Function lNewInsertKeyCkecTrial() As Integer
        Dim cn As System.Data.SqlClient.SqlConnection
        Dim cmd As System.Data.SqlClient.SqlCommand
        Dim Qstr As String = ""
        Dim RowsAffected As Integer
        Try
            Qstr = "insert into NewTransactionKeyCheckTrials (TransactionCode)"
            Qstr = Qstr & " Values ('" & Hosttransactioncode & "')"





            cn = New System.Data.SqlClient.SqlConnection(CONFIGClass.ConnectionString)
            cn.Open()
            cmd = New System.Data.SqlClient.SqlCommand(Qstr, cn)
            RowsAffected = cmd.ExecuteNonQuery()
            If RowsAffected < 1 Then
                log.loglog("lNewInsertKeyCkecTrial Error with Qstr=[" & Qstr & "]  no rows affected", False)
                Return cnst_ErrCode_DataBaseError
            End If
            cn.Close()
            cn = Nothing
            cmd = Nothing
            Return 0
        Catch ex As Exception
            log.loglog("lNewInsertKeyCkecTrial Error with Qstr=[" & Qstr & "]" & vbNewLine & "Ex:" & ex.ToString, False)
            Return 9
        End Try


    End Function
    Private Function lUpdateRequest(ByVal pAction As String) As Integer
        Dim cn As System.Data.SqlClient.SqlConnection
        Dim cmd As System.Data.SqlClient.SqlCommand
        Dim Qstr As String = ""
        Dim RowsAffected As Integer
        Dim Subquery As String = ""
        Dim sqltrx As SqlClient.SqlTransaction
        Dim detailedErrorCode As Integer
        Dim rtrn As Integer
        Try
            Qstr = "update transactions  set "

            If ExtraData <> "" And pAction <> cnst_RequestType_CBDebitAmountAuthorization Then
                Qstr += " BeneficiaryID='" & ExtraData & "',"

            End If
            Select Case pAction
                Case cnst_RequestType_CBDebitAmountAuthorization
                    If CCTrack2 <> "" Then
                        Qstr += " DepositorID='" & (CCTrack2 & Space(40)).Substring(0, 40) & (DebitAccountNumber & Space(20)).Substring(0, 20) & (CBTDataElement38 & Space(10)).Substring(0, 10) & (CBTDataElement39 & Space(10)).Substring(0, 10) & "',"
                    End If

                    Qstr += " PaymentType='" & cnst_PaymentType_DBTAccount & "' "
                    Qstr += "where DepositStatus ='" & cnst_ActionStatus_AUTHORIZED & "' "
                    Subquery = " Insert into TransactionNestedActions (TransactionCode,action,ActionDateTime,ActionReason,ActionStatus,DispensedNotes,DispensedAmount,CommissionAmount,Cassette1,Cassette2,Cassette3,Cassette4,CountryCode,BankCode,ATMId,ATMTrxSequence,DispensedCurrencyCode,DispensedRate) "
                    Subquery += " values ('" & Hosttransactioncode & "','" & pAction & "',getdate(), '" & ActionReason & "','" & ActionStatus & "','" & DispensedNotes & "'," & DispensedAmount & "," & CommissionAmount & "," & mvATMPhysicalCassitteValue(1) & "," & mvATMPhysicalCassitteValue(2) & "," & mvATMPhysicalCassitteValue(3) & "," & mvATMPhysicalCassitteValue(4) & ",'" & Country & "','" & BankId & "','" & ATMId & "','" & TransactionSequence & "','" & Currency & "'," & DispensedRate & " )"


                Case cnst_RequestType_DepositConfirmation
                    Qstr += " DepositStatus='" & cnst_ActionStatus_CONFIRMED & "' "
                    Qstr += "where DepositStatus ='" & cnst_ActionStatus_AUTHORIZED & "' "
                    Subquery = " Insert into TransactionNestedActions (TransactionCode,action,ActionDateTime,ActionReason,ActionStatus,DispensedNotes,DispensedAmount,CommissionAmount,Cassette1,Cassette2,Cassette3,Cassette4,CountryCode,BankCode,ATMId,ATMTrxSequence,DispensedCurrencyCode,DispensedRate) "
                    Subquery += " values ('" & Hosttransactioncode & "','" & pAction & "',getdate(), '" & ActionReason & "','" & cnst_ActionStatus_CONFIRMED & "','" & DispensedNotes & "'," & DispensedAmount & "," & CommissionAmount & "," & mvATMPhysicalCassitteValue(1) & "," & mvATMPhysicalCassitteValue(2) & "," & mvATMPhysicalCassitteValue(3) & "," & mvATMPhysicalCassitteValue(4) & ",'" & Country & "','" & BankId & "','" & ATMId & "','" & TransactionSequence & "','" & Currency & "'," & DispensedRate & " )"
                Case cnst_RequestType_DepositCancelation
                    Qstr += " DepositStatus='" & cnst_ActionStatus_CANCELED & "' "
                    Qstr += ",CancelStatus='" & cnst_ActionStatus_CANCELED & "' "
                    Qstr += ",CancelDateTime=getdate()  "
                    Qstr += "where DepositStatus ='" & cnst_ActionStatus_AUTHORIZED & "' "

                    Subquery = " Insert into TransactionNestedActions (TransactionCode,action,ActionDateTime,ActionReason,ActionStatus,CountryCode,BankCode,ATMId) "
                    Subquery += " values ('" & Hosttransactioncode & "','" & pAction & "',getdate(), '" & ActionReason & "','" & cnst_ActionStatus_CANCELED & "','" & Country & "','" & BankId & "','" & ATMId & "' )"

                Case cnst_RequestType_WithdrawalAuthorization
                    Qstr += " WithdrawalStatus='" & cnst_ActionStatus_AUTHORIZED & "' "
                    Qstr += " ,WithdrawalDateTime=getdate() "
                    Qstr += " where DepositStatus ='" & cnst_ActionStatus_CONFIRMED & "' "
                    Qstr += " and ( WithdrawalStatus is null or WithdrawalStatus =  '" & cnst_ActionStatus_CANCELED & "')"
                    Qstr += " and BeneficiaryPIN ='" & BeneficiaryPIN & "' "
                    Qstr += " and DepositorPIN ='" & DepositorPIN & "' "
                    Subquery = " Insert into TransactionNestedActions (TransactionCode,action,ActionDateTime,ActionReason,ActionStatus,DispensedNotes,DispensedAmount,CommissionAmount,Cassette1,Cassette2,Cassette3,Cassette4,CountryCode,BankCode,ATMId,ATMTrxSequence,DispensedCurrencyCode,DispensedRate) "
                    Subquery += " values ('" & Hosttransactioncode & "','" & pAction & "',getdate(), '" & ActionReason & "','" & cnst_ActionStatus_AUTHORIZED & "','" & DispensedNotes & "'," & DispensedAmount & "," & CommissionAmount & "," & mvATMPhysicalCassitteValue(1) & "," & mvATMPhysicalCassitteValue(2) & "," & mvATMPhysicalCassitteValue(3) & "," & mvATMPhysicalCassitteValue(4) & ",'" & Country & "','" & BankId & "','" & ATMId & "','" & TransactionSequence & "','" & Currency & "'," & DispensedRate & " )"

                Case cnst_RequestType_WithdrawalConfirmation
                    Qstr += " WithdrawalStatus='" & cnst_ActionStatus_CONFIRMED & "' "
                    Qstr += " where DepositStatus ='" & cnst_ActionStatus_CONFIRMED & "' "
                    Qstr += " and WithdrawalStatus ='" & cnst_ActionStatus_AUTHORIZED & "' "
                    Qstr += " and BeneficiaryPIN ='" & BeneficiaryPIN & "' "
                    Qstr += " and DepositorPIN ='" & DepositorPIN & "' "
                    Subquery = " Insert into TransactionNestedActions (TransactionCode,action,ActionDateTime,ActionReason,ActionStatus,DispensedNotes,DispensedAmount,CommissionAmount,Cassette1,Cassette2,Cassette3,Cassette4,CountryCode,BankCode,ATMId,ATMTrxSequence,DispensedCurrencyCode,DispensedRate) "
                    Subquery += " values ('" & Hosttransactioncode & "','" & pAction & "',getdate(), '" & ActionReason & "','" & cnst_ActionStatus_CONFIRMED & "','" & DispensedNotes & "'," & DispensedAmount & "," & CommissionAmount & "," & mvATMPhysicalCassitteValue(1) & "," & mvATMPhysicalCassitteValue(2) & "," & mvATMPhysicalCassitteValue(3) & "," & mvATMPhysicalCassitteValue(4) & ",'" & Country & "','" & BankId & "','" & ATMId & "','" & TransactionSequence & "','" & Currency & "'," & DispensedRate & " )"
                Case cnst_RequestType_WithdrawalUnCertain
                    Qstr += " WithdrawalStatus= WithdrawalStatus "
                    Qstr += " where DepositStatus ='" & cnst_ActionStatus_CONFIRMED & "' "
                    Qstr += " and WithdrawalStatus ='" & cnst_ActionStatus_AUTHORIZED & "' "
                    Qstr += " and BeneficiaryPIN ='" & BeneficiaryPIN & "' "
                    Qstr += " and DepositorPIN ='" & DepositorPIN & "' "
                    Subquery = " Insert into TransactionNestedActions (TransactionCode,action,ActionDateTime,ActionReason,ActionStatus,DispensedNotes,DispensedAmount,CommissionAmount,Cassette1,Cassette2,Cassette3,Cassette4,CountryCode,BankCode,ATMId,ATMTrxSequence,DispensedCurrencyCode,DispensedRate) "
                    Subquery += " values ('" & Hosttransactioncode & "','" & pAction & "',getdate(), '" & ActionReason & "','" & cnst_ActionStatus_UnCertain & "','" & DispensedNotes & "'," & DispensedAmount & "," & CommissionAmount & "," & mvATMPhysicalCassitteValue(1) & "," & mvATMPhysicalCassitteValue(2) & "," & mvATMPhysicalCassitteValue(3) & "," & mvATMPhysicalCassitteValue(4) & ",'" & Country & "','" & BankId & "','" & ATMId & "','" & TransactionSequence & "','" & Currency & "'," & DispensedRate & " )"


                Case cnst_RequestType_WithdrawalCancelation
                    Qstr += " WithdrawalStatus='" & cnst_ActionStatus_CANCELED & "' "
                    Qstr += " where DepositStatus ='" & cnst_ActionStatus_CONFIRMED & "' "
                    Qstr += " and WithdrawalStatus ='" & cnst_ActionStatus_AUTHORIZED & "' "
                    Qstr += " and BeneficiaryPIN ='" & BeneficiaryPIN & "' "
                    Qstr += " and DepositorPIN ='" & DepositorPIN & "' "
                    Subquery = " Insert into TransactionNestedActions (TransactionCode,action,ActionDateTime,ActionReason,ActionStatus,CountryCode,BankCode,ATMId) "
                    Subquery += " values ('" & Hosttransactioncode & "','" & pAction & "',getdate(), '" & ActionReason & "','" & cnst_ActionStatus_CANCELED & "','" & Country & "','" & BankId & "','" & ATMId & "' )"



                Case cnst_RequestType_RedemptionAutorization
                    Qstr += " WithdrawalStatus='" & cnst_ActionStatus_AUTHORIZED & "' "
                    Qstr += " ,WithdrawalDateTime=getdate() "
                    Qstr += " where DepositStatus ='" & cnst_ActionStatus_EXPIRED & "' "
                    Qstr += " and ( WithdrawalStatus =  '" & cnst_ActionStatus_EXPIRED & "'"
                    Qstr += "   or WithdrawalStatus =  '" & cnst_ActionStatus_CANCELED & "') "
                    Qstr += " and redemptionPIN ='" & RedempltionPIN & "' "
                    Qstr += " and DepositorMobile ='" & DepositorMobile & "' "
                    Qstr += " and BeneficiaryMobile ='" & BeneficiaryMobile & "' "

                    Subquery = " Insert into TransactionNestedActions (TransactionCode,action,ActionDateTime,ActionReason,ActionStatus,DispensedNotes,DispensedAmount,CommissionAmount,Cassette1,Cassette2,Cassette3,Cassette4,CountryCode,BankCode,ATMId,ATMTrxSequence,DispensedCurrencyCode,DispensedRate) "
                    Subquery += " values ('" & Hosttransactioncode & "','" & pAction & "',getdate(), '" & ActionReason & "','" & cnst_ActionStatus_AUTHORIZED & "','" & DispensedNotes & "'," & DispensedAmount & "," & CommissionAmount & "," & mvATMPhysicalCassitteValue(1) & "," & mvATMPhysicalCassitteValue(2) & "," & mvATMPhysicalCassitteValue(3) & "," & mvATMPhysicalCassitteValue(4) & ",'" & Country & "','" & BankId & "','" & ATMId & "','" & TransactionSequence & "','" & Currency & "'," & DispensedRate & " )"


                Case cnst_RequestType_RedemptionConfirmation
                    Qstr += " WithdrawalStatus='" & cnst_ActionStatus_CONFIRMED & "' "
                    Qstr += " where DepositStatus ='" & cnst_ActionStatus_EXPIRED & "' "
                    Qstr += " and WithdrawalStatus ='" & cnst_ActionStatus_AUTHORIZED & "' "
                    Qstr += " and redemptionPIN ='" & RedempltionPIN & "' "

                    Subquery = " Insert into TransactionNestedActions (TransactionCode,action,ActionDateTime,ActionReason,ActionStatus,DispensedNotes,DispensedAmount,CommissionAmount,Cassette1,Cassette2,Cassette3,Cassette4,CountryCode,BankCode,ATMId,ATMTrxSequence,DispensedCurrencyCode,DispensedRate) "
                    Subquery += " values ('" & Hosttransactioncode & "','" & pAction & "',getdate(), '" & ActionReason & "','" & cnst_ActionStatus_CONFIRMED & "','" & DispensedNotes & "'," & DispensedAmount & "," & CommissionAmount & "," & mvATMPhysicalCassitteValue(1) & "," & mvATMPhysicalCassitteValue(2) & "," & mvATMPhysicalCassitteValue(3) & "," & mvATMPhysicalCassitteValue(4) & ",'" & Country & "','" & BankId & "','" & ATMId & "','" & TransactionSequence & "','" & Currency & "'," & DispensedRate & " )"

                Case cnst_RequestType_RedemptionUnCertain
                    Qstr += " WithdrawalStatus=WithdrawalStatus "
                    Qstr += " where DepositStatus ='" & cnst_ActionStatus_EXPIRED & "' "
                    Qstr += " and WithdrawalStatus ='" & cnst_ActionStatus_AUTHORIZED & "' "
                    Qstr += " and redemptionPIN ='" & RedempltionPIN & "' "

                    Subquery = " Insert into TransactionNestedActions (TransactionCode,action,ActionDateTime,ActionReason,ActionStatus,DispensedNotes,DispensedAmount,CommissionAmount,Cassette1,Cassette2,Cassette3,Cassette4,CountryCode,BankCode,ATMId,ATMTrxSequence,DispensedCurrencyCode,DispensedRate) "
                    Subquery += " values ('" & Hosttransactioncode & "','" & pAction & "',getdate(), '" & ActionReason & "','" & cnst_ActionStatus_UnCertain & "','" & DispensedNotes & "'," & DispensedAmount & "," & CommissionAmount & "," & mvATMPhysicalCassitteValue(1) & "," & mvATMPhysicalCassitteValue(2) & "," & mvATMPhysicalCassitteValue(3) & "," & mvATMPhysicalCassitteValue(4) & ",'" & Country & "','" & BankId & "','" & ATMId & "','" & TransactionSequence & "','" & Currency & "'," & DispensedRate & " )"


                Case cnst_RequestType_RedemptionCancelation
                    Qstr += " WithdrawalStatus='" & cnst_ActionStatus_CANCELED & "' "
                    Qstr += " where DepositStatus ='" & cnst_ActionStatus_EXPIRED & "' "
                    Qstr += " and WithdrawalStatus ='" & cnst_ActionStatus_AUTHORIZED & "' "
                    Qstr += " and RedemptionPIN ='" & RedempltionPIN & "' "

                    Subquery = " Insert into TransactionNestedActions (TransactionCode,action,ActionDateTime,ActionReason,ActionStatus,countrycode,bankcode,atmid) "
                    Subquery += " values ('" & Hosttransactioncode & "','" & pAction & "',getdate(), '" & ActionReason & "','" & cnst_ActionStatus_CANCELED & "','" & Country & "','" & BankId & "','" & ATMId & "' )"



                Case cnst_RequestType_DepositReActivation
                    Qstr += " DepositStatus='" & cnst_ActionStatus_CONFIRMED & "' "
                    Qstr += " ,WithdrawalStatus=NULL "
                    Qstr += " ,DepositDateTime=getdate() "
                    Qstr += " ,ReActivationCounter=ReActivationCounter + 1 "
                    Qstr += " where DepositStatus ='" & cnst_ActionStatus_EXPIRED & "' "
                    Qstr += " and WithdrawalStatus ='" & cnst_ActionStatus_EXPIRED & "' "
                    Qstr += " and ReActivationCounter < " & mvMaxReActivateTimes  ''CONFIGClass.ExpiredTransactionMaxReActivationTimes
                    Subquery = " Insert into TransactionNestedActions (TransactionCode,action,ActionDateTime,ActionReason,ActionStatus,countrycode,bankcode,atmid) "
                    Subquery += " values ('" & Hosttransactioncode & "','" & pAction & "',getdate(), '" & ActionReason & "','" & cnst_ActionStatus_REACTIVATED & "','" & Country & "','" & BankId & "','" & ATMId & "' )"


                Case cnst_RequestType_DepositHold
                    Qstr += " DepositStatus='" & cnst_ActionStatus_HOLD & "' "
                    Qstr += " ,WithdrawalStatus='" & cnst_ActionStatus_HOLD & "' "
                    Qstr += " where DepositStatus ='" & cnst_ActionStatus_CONFIRMED & "' "
                    Qstr += " and ( WithdrawalStatus is null or WithdrawalStatus =  '" & cnst_ActionStatus_CANCELED & "')"
                    Subquery = " Insert into TransactionNestedActions (TransactionCode,action,ActionDateTime,ActionReason,ActionStatus,countrycode,bankcode,atmid) "
                    Subquery += " values ('" & Hosttransactioncode & "','" & pAction & "',getdate(), '" & ActionReason & "','" & cnst_ActionStatus_HOLD & "','" & Country & "','" & BankId & "','" & ATMId & "' )"

                Case cnst_RequestType_DepositUnHold
                    Qstr += " DepositStatus='" & cnst_ActionStatus_CONFIRMED & "' "
                    Qstr += " ,WithdrawalStatus=NULL "
                    Qstr += " where DepositStatus ='" & cnst_ActionStatus_HOLD & "' "
                    Qstr += " and  WithdrawalStatus =  '" & cnst_ActionStatus_HOLD & "' "
                    Subquery = " Insert into TransactionNestedActions (TransactionCode,action,ActionDateTime,ActionReason,ActionStatus,countrycode,bankcode,atmid) "
                    Subquery += " values ('" & Hosttransactioncode & "','" & pAction & "',getdate(), '" & ActionReason & "','" & cnst_ActionStatus_UNHOLD & "','" & Country & "','" & BankId & "','" & ATMId & "' )"

                Case cnst_RequestType_ResendSMSBoth
                    Qstr += " ResendSMSFlag=ResendSMSFlag+1, resendTo= " & cnst_ResendTypeBoth
                    Qstr += " where DepositStatus ='" & cnst_ActionStatus_CONFIRMED & "' "
                    Qstr += " and  WithdrawalStatus is null "
                    Subquery = " Insert into TransactionNestedActions (TransactionCode,action,ActionDateTime,ActionReason,ActionStatus,countrycode,bankcode,atmid) "
                    Subquery += " values ('" & Hosttransactioncode & "','" & pAction & "',getdate(), '" & ActionReason & "','" & cnst_ActionStatus_RESENDSMSBoth & "','" & Country & "','" & BankId & "','" & ATMId & "' )"

                Case cnst_RequestType_ResendSMSDepositor
                    Qstr += " ResendSMSFlag=ResendSMSFlag+1, resendTo= " & cnst_ResendTypeD
                    Qstr += " where DepositStatus ='" & cnst_ActionStatus_CONFIRMED & "' "
                    Qstr += " and  WithdrawalStatus is null "
                    Subquery = " Insert into TransactionNestedActions (TransactionCode,action,ActionDateTime,ActionReason,ActionStatus,countrycode,bankcode,atmid) "
                    Subquery += " values ('" & Hosttransactioncode & "','" & pAction & "',getdate(), '" & ActionReason & "','" & cnst_ActionStatus_RESENDSMSD & "','" & Country & "','" & BankId & "','" & ATMId & "' )"


                Case cnst_RequestType_ResendSMSBeneficiery
                    Qstr += " ResendSMSFlag=ResendSMSFlag+1, resendTo= " & cnst_ResendTypeB
                    Qstr += " where DepositStatus ='" & cnst_ActionStatus_CONFIRMED & "' "
                    Qstr += " and  WithdrawalStatus is null "
                    Subquery = " Insert into TransactionNestedActions (TransactionCode,action,ActionDateTime,ActionReason,ActionStatus,countrycode,bankcode,atmid) "
                    Subquery += " values ('" & Hosttransactioncode & "','" & pAction & "',getdate(), '" & ActionReason & "','" & cnst_ActionStatus_RESENDSMSB & "','" & Country & "','" & BankId & "','" & ATMId & "' )"

                Case cnst_RequestType_ResendSMSRedemption
                    Qstr += " ResendSMSFlag=ResendSMSFlag+1, resendTo= " & cnst_ResendTypeDR
                    Qstr += " where DepositStatus ='" & cnst_ActionStatus_CONFIRMED & "' "
                    Qstr += " and  WithdrawalStatus is null "
                    Subquery = " Insert into TransactionNestedActions (TransactionCode,action,ActionDateTime,ActionReason,ActionStatus,countrycode,bankcode,atmid) "
                    Subquery += " values ('" & Hosttransactioncode & "','" & pAction & "',getdate(), '" & ActionReason & "','" & cnst_ActionStatus_RESENDSMSB & "','" & Country & "','" & BankId & "','" & ATMId & "' )"



                Case cnst_RequestType_UnBLock
                    Qstr += " WithdrawalStatus='" & cnst_ActionStatus_CANCELED & "', withdrawalHostFlag = withdrawalHostFlag + withdrawalHostFlag % 2 "
                    Qstr += " where (DepositStatus ='" & cnst_ActionStatus_CONFIRMED & "' or DepositStatus ='" & cnst_ActionStatus_EXPIRED & "')"
                    Qstr += " and WithdrawalStatus ='" & cnst_ActionStatus_AUTHORIZED & "' "
                    Qstr += " and datediff(minute, Withdrawaldatetime, getdate()) >=5 "

                    Subquery = " Insert into TransactionNestedActions (TransactionCode,action,ActionDateTime,ActionReason,ActionStatus,CountryCode,BankCode,ATMId) "
                    Subquery += " values ('" & Hosttransactioncode & "','" & pAction & "',getdate(), '" & ActionReason & "','" & cnst_ActionStatus_UNBLOCKED & "','" & Country & "','" & BankId & "','" & ATMId & "' )"

                Case cnst_RequestType_ManuallyConfirm
                    Qstr += " WithdrawalStatus='" & cnst_ActionStatus_CONFIRMED & "' "
                    Qstr += " where (DepositStatus ='" & cnst_ActionStatus_CONFIRMED & "'  or DepositStatus ='" & cnst_ActionStatus_EXPIRED & "')"
                    Qstr += " and WithdrawalStatus ='" & cnst_ActionStatus_AUTHORIZED & "' "
                    Qstr += " and datediff(minute, Withdrawaldatetime, getdate()) >=5 "

                    Subquery = " Insert into TransactionNestedActions (TransactionCode,action,ActionDateTime,ActionReason,ActionStatus,CountryCode,BankCode,ATMId) "
                    Subquery += " values ('" & Hosttransactioncode & "','" & pAction & "',getdate(), '" & ActionReason & "','" & cnst_ActionStatus_CONFIRMED & "','" & Country & "','" & BankId & "','" & ATMId & "' )"

                Case cnst_RequestType_ExpiredManuallyConfirm
                    Qstr += " WithdrawalStatus='" & cnst_ActionStatus_ExpiredManualCONFIRMED & "' "
                    Qstr += " where (DepositStatus ='" & cnst_ActionStatus_EXPIRED & "')"
                    Qstr += " and WithdrawalStatus ='" & cnst_ActionStatus_EXPIRED & "' "
                    Qstr += " and datediff(minute, Withdrawaldatetime, getdate()) >=5 "

                    Subquery = " Insert into TransactionNestedActions (TransactionCode,action,ActionDateTime,ActionReason,ActionStatus,CountryCode,BankCode,ATMId) "
                    Subquery += " values ('" & Hosttransactioncode & "','" & pAction & "',getdate(), '" & ActionReason & "','" & cnst_ActionStatus_ExpiredManualCONFIRMED & "','" & Country & "','" & BankId & "','" & ATMId & "' )"

                Case cnst_RequestType_DepositForceExpiration
                    rtrn = ForceUpdateRequestSetExpired(False)
                    Return rtrn
                Case Else
                    log.loglog("lUpdateRequest Error unknown action [" & pAction & "]", False)
                    Return 8
            End Select


            Qstr += " and TransactionCode='" & Hosttransactioncode & "'"



            cn = New System.Data.SqlClient.SqlConnection(CONFIGClass.ConnectionString)
            cn.Open()
            sqltrx = cn.BeginTransaction

            cmd = New System.Data.SqlClient.SqlCommand(Qstr, cn)
            cmd.Transaction = sqltrx
            RowsAffected = cmd.ExecuteNonQuery()
            If RowsAffected < 1 Then
                log.loglog("lUpdateRequest Requet type =[" & pAction & "] Error with Qstr=[" & Qstr & "]  no rows affected", False)
                sqltrx.Rollback()

                If pAction = cnst_RequestType_RedemptionAutorization Or _
                   pAction = cnst_RequestType_WithdrawalAuthorization Then
                    rtrn = GetDetailedResponseCode(pAction, detailedErrorCode)
                    If rtrn = 0 Then
                        Return detailedErrorCode
                    Else
                        Return cnst_ErrCode_BadStatusForRequiredRequestType
                    End If

                End If

                If pAction = cnst_RequestType_DepositReActivation Then
                    rtrn = GetDetailedReActivationResponseCode(pAction, detailedErrorCode)
                    log.loglog("lUpdateRequest,GetDetailedReActivationResponseCode returns [" & rtrn & "] detailedErrorCode=[" & detailedErrorCode & "] pAction=[" & pAction, False)

                    If rtrn = 0 Then
                        Return detailedErrorCode
                    Else
                        Return cnst_ErrCode_BadStatusForRequiredRequestType
                    End If

                Else
                    Return cnst_ErrCode_BadStatusForRequiredRequestType
                End If


            End If
            cmd.CommandText = Subquery
            RowsAffected = cmd.ExecuteNonQuery()
            If RowsAffected < 1 Then
                log.loglog("lUpdateRequest Error with subquery =[" & Subquery & "]  no rows insreted", False)
                sqltrx.Rollback()
                Return cnst_ErrCode_BadStatusForRequiredRequestType
            End If
            sqltrx.Commit()
            cn.Close()
            cn = Nothing
            cmd = Nothing
            Return 0
        Catch ex As Exception
            log.loglog("lUpdateRequest Error with Qstr=[" & Qstr & "]" & vbNewLine & "Subqueru=[" & Subquery & "] Ex:" & ex.ToString, False)
            Return 9
        End Try

    End Function
    Private Function lUpdateRequestNew(ByVal pAction As String) As Integer
        Dim cn As System.Data.SqlClient.SqlConnection
        Dim cmd As System.Data.SqlClient.SqlCommand
        Dim Qstr As String = ""
        Dim RowsAffected As Integer
        Dim Subquery As String = ""
        Dim sqltrx As SqlClient.SqlTransaction
        Try
            Qstr = "update NewTransactions  set "

            Select Case pAction

                Case cnst_RequestType_NewWithdrawalAuthorization
                    Qstr += " WithdrawalStatus='" & cnst_ActionStatus_AUTHORIZED & "' "
                    Qstr += " ,WithdrawalDateTime=getdate() "
                    Qstr += " ,ATMId= '" & ATMId & "' "
                    Qstr += " ,ATMDateTime= '" & ATMDateTime & "' "
                    Qstr += " ,ATMTransactionSequence= '" & TransactionSequence & "' "
                    Qstr += " ,CommissionAmount= '" & CommissionAmount & "' "
                    Qstr += " ,CassettesDispensedNotes= '" & DispensedNotes & "' "
                    Qstr += " ,DispensedAmount= '" & DispensedAmount & "' "
                    Qstr += " where ( WithdrawalStatus is null or WithdrawalStatus =  '" & cnst_ActionStatus_CANCELED & "')"
                    Qstr += " and TransactionCode ='" & Hosttransactioncode & "' "
                    Subquery = " Insert into NewTransactionNestedActions (TransactionCode,action,ActionDateTime,ActionReason,ActionStatus,DispensedNotes,DispensedAmount,CommissionAmount,Cassette1,Cassette2,Cassette3,Cassette4,CountryCode,BankCode,ATMId,ATMTrxSequence,DispensedCurrencyCode,DispensedRate) "
                    Subquery += " values ('" & Hosttransactioncode & "','" & pAction & "',getdate(), '" & ActionReason & "','" & cnst_ActionStatus_AUTHORIZED & "','" & DispensedNotes & "'," & DispensedAmount & "," & CommissionAmount & "," & mvATMPhysicalCassitteValue(1) & "," & mvATMPhysicalCassitteValue(2) & "," & mvATMPhysicalCassitteValue(3) & "," & mvATMPhysicalCassitteValue(4) & ",'" & CONFIGClass.LocalCountry & "','" & CONFIGClass.LocalBank & "','" & ATMId & "','" & TransactionSequence & "','" & Currency & "'," & DispensedRate & " )"

                Case cnst_RequestType_NewWithdrawalConfirmation
                    Qstr += " WithdrawalStatus='" & cnst_ActionStatus_CONFIRMED & "' "
                    Qstr += " ,WithdrawalDateTime= '" & ATMDateTime & "' "
                    Qstr += " where WithdrawalStatus ='" & cnst_ActionStatus_AUTHORIZED & "' "
                    Qstr += " and TransactionCode ='" & Hosttransactioncode & "' "
                    Subquery = " Insert into NewTransactionNestedActions (TransactionCode,action,ActionDateTime,ActionReason,ActionStatus,DispensedNotes,DispensedAmount,CommissionAmount,Cassette1,Cassette2,Cassette3,Cassette4,CountryCode,BankCode,ATMId,ATMTrxSequence,DispensedCurrencyCode,DispensedRate) "
                    Subquery += " values ('" & Hosttransactioncode & "','" & pAction & "',getdate(), '" & ActionReason & "','" & cnst_ActionStatus_CONFIRMED & "','" & DispensedNotes & "'," & DispensedAmount & "," & CommissionAmount & "," & mvATMPhysicalCassitteValue(1) & "," & mvATMPhysicalCassitteValue(2) & "," & mvATMPhysicalCassitteValue(3) & "," & mvATMPhysicalCassitteValue(4) & ",'" & CONFIGClass.LocalCountry & "','" & CONFIGClass.LocalBank & "','" & ATMId & "','" & TransactionSequence & "','" & Currency & "'," & DispensedRate & " )"
                Case cnst_RequestType_NewWithdrawalUnCertain
                    Qstr += " WithdrawalStatus='" & cnst_ActionStatus_UnCertain & "' "
                    Qstr += " ,WithdrawalDateTime= '" & ATMDateTime & "' "
                    Qstr += " where WithdrawalStatus ='" & cnst_ActionStatus_AUTHORIZED & "' "
                    Qstr += " and TransactionCode ='" & Hosttransactioncode & "' "

                    Subquery = " Insert into NewTransactionNestedActions (TransactionCode,action,ActionDateTime,ActionReason,ActionStatus,DispensedNotes,DispensedAmount,CommissionAmount,Cassette1,Cassette2,Cassette3,Cassette4,CountryCode,BankCode,ATMId,ATMTrxSequence,DispensedCurrencyCode,DispensedRate) "
                    Subquery += " values ('" & Hosttransactioncode & "','" & pAction & "',getdate(), '" & ActionReason & "','" & cnst_ActionStatus_UnCertain & "','" & DispensedNotes & "'," & DispensedAmount & "," & CommissionAmount & "," & mvATMPhysicalCassitteValue(1) & "," & mvATMPhysicalCassitteValue(2) & "," & mvATMPhysicalCassitteValue(3) & "," & mvATMPhysicalCassitteValue(4) & ",'" & CONFIGClass.LocalCountry & "','" & CONFIGClass.LocalBank & "','" & ATMId & "','" & TransactionSequence & "','" & Currency & "'," & DispensedRate & " )"


                Case cnst_RequestType_NewWithdrawalCancelation
                    Qstr += " WithdrawalStatus='" & cnst_ActionStatus_CANCELED & "' "
                    Qstr += " ,WithdrawalDateTime= '" & ATMDateTime & "' "
                    Qstr += " where  WithdrawalStatus ='" & cnst_ActionStatus_AUTHORIZED & "' "
                    Qstr += " and TransactionCode ='" & Hosttransactioncode & "' "

                    Subquery = " Insert into NewTransactionNestedActions (TransactionCode,action,ActionDateTime,ActionReason,ActionStatus,CountryCode,BankCode,ATMId) "
                    Subquery += " values ('" & Hosttransactioncode & "','" & pAction & "',getdate(), '" & ActionReason & "','" & cnst_ActionStatus_CANCELED & "','" & CONFIGClass.LocalCountry & "','" & CONFIGClass.LocalBank & "','" & ATMId & "' )"



                Case Else
                    log.loglog("lUpdateRequestNew Error unknown action [" & pAction & "]", False)
                    Return 8
            End Select

            cn = New System.Data.SqlClient.SqlConnection(CONFIGClass.ConnectionString)
            cn.Open()
            sqltrx = cn.BeginTransaction

            cmd = New System.Data.SqlClient.SqlCommand(Qstr, cn)
            cmd.Transaction = sqltrx
            RowsAffected = cmd.ExecuteNonQuery()
            If RowsAffected < 1 Then
                log.loglog("lUpdateRequestNew Requet type =[" & pAction & "] Error with Qstr=[" & Qstr & "]  no rows affected", False)
                sqltrx.Rollback()

            End If
            cmd.CommandText = Subquery
            RowsAffected = cmd.ExecuteNonQuery()
            If RowsAffected < 1 Then
                log.loglog("lUpdateRequestNew Error with subquery =[" & Subquery & "]  no rows insreted", False)
                sqltrx.Rollback()
                Return cnst_ErrCode_BadStatusForRequiredRequestType
            End If
            sqltrx.Commit()
            cn.Close()
            cn = Nothing
            cmd = Nothing
            Return 0
        Catch ex As Exception
            log.loglog("lUpdateRequestNew Error with Qstr=[" & Qstr & "]" & vbNewLine & "Subqueru=[" & Subquery & "] Ex:" & ex.ToString, False)
            Return 9
        End Try

    End Function
    Private Function InsertExceptionActions(ByVal pAction As String, ByVal pActionStatus As String) As Integer
        Dim cn As System.Data.SqlClient.SqlConnection
        Dim cmd As System.Data.SqlClient.SqlCommand

        Dim RowsAffected As Integer
        Dim Subquery As String = ""


        Try
            Subquery = " Insert into TransactionExceptionActions (TransactionCode,action,ActionDateTime,ActionStatus,CountryCode,BankCode,ATMId,ATMDate,ATMTime,DepositorMobile) "
            Subquery += " values ('" & Hosttransactioncode & "','" & pAction & "',getdate(), '" & pActionStatus & "','" & Country & "','" & BankId & "','" & ATMId & "','" & ATMDate & "','" & ATMTime & "','" & DepositorMobile & "')"


            cn = New System.Data.SqlClient.SqlConnection(CONFIGClass.ConnectionString)
            cn.Open()


            cmd = New System.Data.SqlClient.SqlCommand(Subquery, cn)

            RowsAffected = cmd.ExecuteNonQuery()
            If RowsAffected < 1 Then
                log.loglog("InsertExceptionActions Requet type =[" & pAction & "] Error with query=[" & Subquery & "]  no rows affected", False)

            End If
            cn.Close()
            cn = Nothing
            cmd = Nothing
            Return 0
        Catch ex As Exception
            log.loglog("InsertExceptionActions Error with Qstr=[" & Subquery & "] Ex:" & ex.ToString, False)
            Return 9
        End Try

    End Function
    Private Function UpdateRequestSetExpired(ByVal logFlag As Boolean) As Integer
        Dim cn As System.Data.SqlClient.SqlConnection
        Dim cmd As System.Data.SqlClient.SqlCommand
        Dim SQstr As String = ""
        Dim Qstr As String = ""
        Dim RowsAffected As Integer
        Dim Subquery As String = ""
        Dim sqltrx As SqlClient.SqlTransaction
        Dim encRedemptionPin As String
        Dim rtrn As Integer
        Dim enc As NCRCrypto.NCRCrypto
        Dim expCount As Long
        Dim ds As DataSet
        Dim da As System.Data.SqlClient.SqlDataAdapter
        Dim thisTxCode As String
        Try


            cn = New System.Data.SqlClient.SqlConnection(CONFIGClass.ConnectionString)
            cn.Open()

            SQstr = "select * from transactions "
            SQstr += " where ( DepositStatus ='" & cnst_ActionStatus_CONFIRMED & "'  or DepositStatus ='" & cnst_ActionStatus_HOLD & "') "
            SQstr += " and ( WithdrawalStatus is null or WithdrawalStatus =  '" & cnst_ActionStatus_CANCELED & "' or WithdrawalStatus =  '" & cnst_ActionStatus_HOLD & "')"
            SQstr += " and datediff(" & Chr(34) & "D" & Chr(34) & ", depositdatetime, getdate()) >= " & mvDeposittrxExpDays

            da = New System.Data.SqlClient.SqlDataAdapter(SQstr, cn)
            ds = New DataSet
            da.Fill(ds)



            If ds.Tables.Count > 0 Then
                log.loglog("UpdateRequestSetexpired Will Set (" & ds.Tables(0).Rows.Count & ") trx as expired query [" & SQstr & "]", "DeActivate", False)

                If ds.Tables(0).Rows.Count > 0 Then
                    For expCount = 0 To ds.Tables(0).Rows.Count - 1
                        thisTxCode = ""
                        thisTxCode = ds.Tables(0).Rows(expCount).Item("TransactionCode").ToString()

                        rtrn = BuildTrxCodeClass.GetRandomPIN(RedempltionPIN, CONFIGClass.PINLength)
                        If rtrn <> 0 Then
                            log.loglog("UpdateRequestSetexpired Error fetching new Expiration PIN", "DeActivate", False)
                            Return 7
                        End If
                        enc = New NCRCrypto.NCRCrypto
                        encRedemptionPin = enc.eT3_Encrypt(RedempltionPIN)
                        Qstr = "update transactions  set "
                        Qstr += " DepositStatus='" & cnst_ActionStatus_EXPIRED & "' "
                        Qstr += ",WithdrawalStatus='" & cnst_ActionStatus_EXPIRED & "'  "
                        Qstr += ",resendSMSFlag=resendSMSFlag+1 ,  RedemptionPIN='" & encRedemptionPin & "' "
                        Qstr += " where transactioncode='" & thisTxCode & "'"
                        '            Qstr += " and (DepositStatus ='" & cnst_ActionStatus_CONFIRMED & "'  or DepositStatus ='" & cnst_ActionStatus_HOLD & "') "
                        '           Qstr += " and ( WithdrawalStatus is null or WithdrawalStatus =  '" & cnst_ActionStatus_CANCELED & "' or WithdrawalStatus =  '" & cnst_ActionStatus_HOLD & "')"
                        '          Qstr += " and datediff(" & Chr(34) & "D" & Chr(34) & ", depositdatetime, getdate()) >= " & mvDeposittrxExpDays


                        Subquery = " insert into  transactionnestedactions "
                        Subquery += " select  TransactionCode,'03', getdate(),'Expiration','EXPIRED',NULL ,0,0,0,0,0,0,NULL,NULL,NULL,NULL,NULL,NULL  from transactions "
                        Subquery += " where transactioncode='" & thisTxCode & "'"
                        '         Subquery += " and (DepositStatus ='" & cnst_ActionStatus_CONFIRMED & "') "
                        '        Subquery += " and ( WithdrawalStatus is null or WithdrawalStatus =  '" & cnst_ActionStatus_CANCELED & "')"
                        '       Subquery += " and datediff(" & Chr(34) & "D" & Chr(34) & ", depositdatetime, getdate()) >= " & mvDeposittrxExpDays




                        sqltrx = cn.BeginTransaction

                        cmd = New System.Data.SqlClient.SqlCommand(Subquery, cn)
                        cmd.Transaction = sqltrx

                        RowsAffected = cmd.ExecuteNonQuery()
                        If RowsAffected < 1 Then
                            If logFlag Then
                                log.loglog("UpdateRequestSetexpired Error (1) with subquery=[" & Subquery & "]  no rows affected", "DeActivate", True)
                            End If
                            sqltrx.Rollback()
                            GoTo nextRow
                            'Return cnst_ErrCode_NoExpiredTransactionsOrDBError
                        End If
                        cmd.CommandText = Qstr
                        RowsAffected = cmd.ExecuteNonQuery()
                        If RowsAffected < 1 Then
                            If logFlag Then
                                log.loglog("UpdateRequestSetexpired Error (2) with Qstr =[" & Qstr & "]  no rows insreted", "DeActivate", True)
                            End If
                            sqltrx.Rollback()
                            GoTo nextRow
                            'Return cnst_ErrCode_NoExpiredTransactionsOrDBError
                        End If
                        sqltrx.Commit()



nextRow:
                    Next 'rows.count
                Else
                    'no transaction is there
                    log.loglog("UpdateRequestSetexpired Error no expired trx are there", "DeActivate", False)
                End If
            Else
                'no tables is there
                log.loglog("UpdateRequestSetexpired Error no tables is there", "DeActivate", False)
            End If ' ds.Tables.Count > 0




            Try
                da.Dispose()
                da = Nothing
            Catch ex As Exception
            End Try

            cn.Close()
            cn = Nothing
            cmd = Nothing

            Return 0
        Catch ex As Exception
            log.loglog("UpdateRequestSetexpired Error (3) with Qstr=[" & Qstr & "]" & vbNewLine & "Subqueru=[" & Subquery & "] Ex:" & ex.ToString, "DeActivate", True)
            Return 9
        End Try

    End Function
    Private Function UpdateRequestSetExpiredNew(ByVal logFlag As Boolean) As Integer
        Dim cn As System.Data.SqlClient.SqlConnection
        Dim cmd As System.Data.SqlClient.SqlCommand
        Dim SQstr As String = ""
        Dim Qstr As String = ""
        Dim RowsAffected As Integer
        Dim Subquery As String = ""
        Dim sqltrx As SqlClient.SqlTransaction
        Dim expCount As Long
        Dim ds As DataSet
        Dim da As System.Data.SqlClient.SqlDataAdapter
        Dim thisTxCode As String
        Try


            cn = New System.Data.SqlClient.SqlConnection(CONFIGClass.ConnectionString)
            cn.Open()

            SQstr = "select * from NewTransactions "
            SQstr += " where ( WithdrawalStatus is null or WithdrawalStatus =  '" & cnst_ActionStatus_CANCELED & "' or WithdrawalStatus =  '" & cnst_ActionStatus_HOLD & "')"

            SQstr += " And datediff(" & Chr(34) & "D" & Chr(34) & ", TransactionDateTime, getdate()) >= " & mvDeposittrxExpDays

            da = New System.Data.SqlClient.SqlDataAdapter(SQstr, cn)
            ds = New DataSet
            da.Fill(ds)



            If ds.Tables.Count > 0 Then
                log.loglog("UpdateRequestSetExpiredNew Will Set (" & ds.Tables(0).Rows.Count & ") trx as expired query [" & SQstr & "]", "DeActivate", False)

                If ds.Tables(0).Rows.Count > 0 Then
                    For expCount = 0 To ds.Tables(0).Rows.Count - 1
                        thisTxCode = ""
                        thisTxCode = ds.Tables(0).Rows(expCount).Item("TransactionCode").ToString()

                        Qstr = "update NewTransactions  set "
                        Qstr += "WithdrawalStatus='" & cnst_ActionStatus_EXPIRED & "'  "
                        Qstr += " where transactioncode='" & thisTxCode & "'"

                        Subquery = " insert into  NewTransactionNestedActions "
                        Subquery += " select  TransactionCode,'03', getdate(),'Expiration','EXPIRED',NULL ,0,0,0,0,0,0,NULL,NULL,NULL,NULL,NULL,NULL  from NewTransactions "
                        Subquery += " where transactioncode='" & thisTxCode & "'"

                        sqltrx = cn.BeginTransaction

                        cmd = New System.Data.SqlClient.SqlCommand(Subquery, cn)
                        cmd.Transaction = sqltrx

                        RowsAffected = cmd.ExecuteNonQuery()
                        If RowsAffected < 1 Then
                            If logFlag Then
                                log.loglog("UpdateRequestSetexpired Error (1) with subquery=[" & Subquery & "]  no rows affected", "DeActivate", True)
                            End If
                            sqltrx.Rollback()
                            GoTo nextRow
                            'Return cnst_ErrCode_NoExpiredTransactionsOrDBError
                        End If
                        cmd.CommandText = Qstr
                        RowsAffected = cmd.ExecuteNonQuery()
                        If RowsAffected < 1 Then
                            If logFlag Then
                                log.loglog("UpdateRequestSetExpiredNew Error (2) with Qstr =[" & Qstr & "]  no rows insreted", "DeActivate", True)
                            End If
                            sqltrx.Rollback()
                            GoTo nextRow
                            'Return cnst_ErrCode_NoExpiredTransactionsOrDBError
                        End If
                        sqltrx.Commit()



nextRow:
                    Next 'rows.count
                Else
                    'no transaction is there
                    log.loglog("UpdateRequestSetExpiredNew Error no expired trx are there", "DeActivate", False)
                End If
            Else
                'no tables is there
                log.loglog("UpdateRequestSetExpiredNew Error no tables is there", "DeActivate", False)
            End If ' ds.Tables.Count > 0




            Try
                da.Dispose()
                da = Nothing
            Catch ex As Exception
            End Try

            cn.Close()
            cn = Nothing
            cmd = Nothing

            Return 0
        Catch ex As Exception
            log.loglog("UpdateRequestSetExpiredNew Error (3) with Qstr=[" & Qstr & "]" & vbNewLine & "Subqueru=[" & Subquery & "] Ex:" & ex.ToString, "DeActivate", True)
            Return 9
        End Try

    End Function
    Private Function ForceUpdateRequestSetExpired(ByVal logFlag As Boolean) As Integer
        Dim cn As System.Data.SqlClient.SqlConnection
        Dim cmd As System.Data.SqlClient.SqlCommand
        Dim Qstr As String = ""
        Dim RowsAffected As Integer
        Dim Subquery As String = ""
        Dim sqltrx As SqlClient.SqlTransaction
        Dim encRedemptionPin As String
        Dim rtrn As Integer
        Dim enc As NCRCrypto.NCRCrypto
        Try

            rtrn = BuildTrxCodeClass.GetRandomPIN(RedempltionPIN, CONFIGClass.PINLength)
            If rtrn <> 0 Then
                log.loglog("ForceUpdateRequestSetExpired Error fetching new Expiration PIN", "DeActivate", False)
                Return 7
            End If
            enc = New NCRCrypto.NCRCrypto
            encRedemptionPin = enc.eT3_Encrypt(RedempltionPIN)
            Qstr = "update transactions  set "
            Qstr += " DepositStatus='" & cnst_ActionStatus_EXPIRED & "' "
            Qstr += ",WithdrawalStatus='" & cnst_ActionStatus_EXPIRED & "'  "
            Qstr += ",resendSMSFlag=resendSMSFlag+1 ,  RedemptionPIN='" & encRedemptionPin & "' "
            Qstr += " where (DepositStatus ='" & cnst_ActionStatus_CONFIRMED & "'  or DepositStatus ='" & cnst_ActionStatus_HOLD & "') "
            Qstr += " and ( WithdrawalStatus is null or WithdrawalStatus =  '" & cnst_ActionStatus_CANCELED & "' or WithdrawalStatus =  '" & cnst_ActionStatus_HOLD & "')"
            Qstr += " and TransactionCode='" & Hosttransactioncode & "'"

            'Qstr += " and datediff(" & Chr(34) & "D" & Chr(34) & ", depositdatetime, getdate()) >= " & mvDeposittrxExpDays


            Subquery = " insert into  transactionnestedactions "
            Subquery += " select  TransactionCode,'23', getdate(),'ForceExpiration','EXPIRED',NULL ,0,0,0,0,0,0,NULL,NULL,NULL,NULL,NULL,NULL  from transactions "
            Subquery += " where (DepositStatus ='" & cnst_ActionStatus_CONFIRMED & "'  or DepositStatus ='" & cnst_ActionStatus_HOLD & "') "
            Subquery += " and ( WithdrawalStatus is null or WithdrawalStatus =  '" & cnst_ActionStatus_CANCELED & "')"
            Subquery += " and TransactionCode='" & Hosttransactioncode & "'"

            'Subquery += " and datediff(" & Chr(34) & "D" & Chr(34) & ", depositdatetime, getdate()) >= " & mvDeposittrxExpDays



            cn = New System.Data.SqlClient.SqlConnection(CONFIGClass.ConnectionString)
            cn.Open()
            sqltrx = cn.BeginTransaction

            cmd = New System.Data.SqlClient.SqlCommand(Subquery, cn)
            cmd.Transaction = sqltrx

            RowsAffected = cmd.ExecuteNonQuery()
            If RowsAffected < 1 Then
                If logFlag Then
                    log.loglog("ForceUpdateRequestSetExpired Error (1) with subquery=[" & Subquery & "]  no rows affected", "DeActivate", True)
                End If
                sqltrx.Rollback()
                Return cnst_ErrCode_NoExpiredTransactionsOrDBError
            End If
            cmd.CommandText = Qstr
            RowsAffected = cmd.ExecuteNonQuery()
            If RowsAffected < 1 Then
                If logFlag Then

                    log.loglog("ForceUpdateRequestSetExpired Error (2) with Qstr =[" & Qstr & "]  no rows insreted", "DeActivate", True)

                End If
                sqltrx.Rollback()

                Return cnst_ErrCode_NoExpiredTransactionsOrDBError
            End If
            sqltrx.Commit()
            cn.Close()
            cn = Nothing
            cmd = Nothing
            Return 0
        Catch ex As Exception
            log.loglog("ForceUpdateRequestSetExpired Error (3) with Qstr=[" & Qstr & "]" & vbNewLine & "Subqueru=[" & Subquery & "] Ex:" & ex.ToString, "DeActivate", True)
            Return 9
        End Try

    End Function

    Public Sub NewDeActivateProcess()
        Dim LastProcessingTime As Date
        Dim rtrn As Integer
        Dim logF As Boolean = False
        Dim logF1 As Boolean = False
        Dim logfNoRows As Boolean = True
        LastProcessingTime = Now
        log.loglog("NewDeActivateProcess, Will Start ...", "NewDeActivateProcess", False)
        While Not ListnerClass.StopListnerFlag
            Try
                'read configuration rules parameters  
                GetBankInfo()
                '''''''''''''''''''''''''''''''''''
                If Now.Subtract(LastProcessingTime).TotalMinutes > CONFIGClass.DepositTransactionExpirationCheckPeriodMinutes Then
                    LastProcessingTime = Now
                    rtrn = UpdateRequestSetExpiredNew(logfNoRows)
                    If rtrn = 8 Then
                        logfNoRows = False
                    Else
                        logfNoRows = True
                    End If
                    If rtrn = 0 Then
                        If Not logF1 Then
                            logF1 = True
                            log.loglog("NewDeActivateProcess, UpdateRequestSetExpired run successfully...", "DeActivate", True)
                        End If
                    Else
                        logF1 = False
                    End If

                End If
                Threading.Thread.Sleep(20000)
                logF = False
            Catch ex As Exception
                If Not logF Then
                    logF = True
                    log.loglog("NewDeActivateProcess, Ex:" & ex.ToString, "NewDeActivateProcess", False)
                End If
            End Try
        End While




    End Sub
    Sub DeActivateProcess()
        Dim LastProcessingTime As Date
        Dim rtrn As Integer
        Dim logF As Boolean = False
        Dim logF1 As Boolean = False
        Dim logfNoRows As Boolean = True
        LastProcessingTime = Now
        log.loglog("DeActivateProcess, Will Start ...", "DeActivate", False)
        While Not ListnerClass.StopListnerFlag
            Try
                'read configuration rules parameters  
                GetBankInfo()
                '''''''''''''''''''''''''''''''''''
                If Now.Subtract(LastProcessingTime).TotalMinutes > CONFIGClass.DepositTransactionExpirationCheckPeriodMinutes Then
                    LastProcessingTime = Now
                    rtrn = UpdateRequestSetExpired(logfNoRows)
                    If rtrn = 8 Then
                        logfNoRows = False
                    Else
                        logfNoRows = True
                    End If
                    If rtrn = 0 Then
                        If Not logF1 Then
                            logF1 = True
                            log.loglog("DeActivateProcess, UpdateRequestSetExpired run successfully...", "DeActivate", True)
                        End If
                    Else
                        logF1 = False
                    End If

                End If
                Threading.Thread.Sleep(20000)
                logF = False
            Catch ex As Exception
                If Not logF Then
                    logF = True
                    log.loglog("DeActivateProcess, Ex:" & ex.ToString, "DeActivate", False)
                End If
            End Try
        End While




    End Sub
    Private Function getTransactionCodeByBeneficiary(ByVal pBeneficieryMobile As String, ByRef ptransactionCode As String) As Integer
        Dim cn As System.Data.SqlClient.SqlConnection
        Dim cmd As System.Data.SqlClient.SqlCommand
        Dim Qstr As String = ""
        Dim r As SqlClient.SqlDataReader
        Dim Subquery As String = ""
        Dim rtrn As String
        Try





            Qstr = "select * from transactions  "
            Qstr += " where "
            Qstr += " BeneficiaryMobile='" & pBeneficieryMobile & "'"
            Qstr += " and BeneficiaryPIN='" & BeneficiaryPIN & "'"
            Qstr += " and DepositorPIN='" & DepositorPIN & "'"
            Qstr += " and   DepositStatus ='" & cnst_ActionStatus_CONFIRMED & "' "
            Qstr += " and ( WithdrawalStatus is null or WithdrawalStatus =  '" & cnst_ActionStatus_CANCELED & "')"

            cn = New System.Data.SqlClient.SqlConnection(CONFIGClass.ConnectionString)
            cn.Open()


            cmd = New System.Data.SqlClient.SqlCommand(Qstr, cn)

            r = cmd.ExecuteReader
            If Not r.HasRows Then
                log.loglog("getTransactionCodeByBeneficiary Error with Qstr=[" & Qstr & "]  no rows are there", False)
                rtrn = cnst_ErrCode_OriginalRequestNotFound
                cn.Close()
                cn = Nothing
                cmd = Nothing
            Else

                r.Read()
                ptransactionCode = r("TransactionCode")
                cn.Close()
                cn = Nothing
                cmd = Nothing
                rtrn = 0

            End If


            Return rtrn
        Catch ex As Exception
            log.loglog("getTransactionCodeByBeneficiary Error with Qstr=[" & Qstr & "] Ex:" & ex.ToString, False)
            Return 9
        End Try

    End Function
    Private Function getDPINByAmount(ByVal pDispensedAmount As String, ByRef pDPIN As String) As Integer
        Dim cn As System.Data.SqlClient.SqlConnection
        Dim cmd As System.Data.SqlClient.SqlCommand
        Dim Qstr As String = ""
        Dim r As SqlClient.SqlDataReader
        Dim Subquery As String = ""
        Dim rtrn As String
        Try





            Qstr = "select * from transactions t "
            Qstr += " where "
            Qstr += " ( TransactionCode='" & Hosttransactioncode & "' or BeneficiaryMobile='" & BeneficiaryMobile & "' ) "
            Qstr += " and BeneficiaryPIN='" & BeneficiaryPIN & "'"
            Qstr += " and   DepositStatus ='" & cnst_ActionStatus_CONFIRMED & "' "
            Qstr += " and ( WithdrawalStatus is null "
            Qstr += "       or WithdrawalStatus = '" & cnst_ActionStatus_CANCELED & "' "
            Qstr += "       or WithdrawalStatus =  '" & cnst_ActionStatus_AUTHORIZED & "' "
            Qstr += "      )"
            Qstr += " and exists ( select * from TransactionNestedActions tn "
            Qstr += "              where tn.TransactionCode=t.TransactionCode "
            Qstr += "              and   tn.Action='" & cnst_RequestType_DepositConfirmation & "'"
            Qstr += "              and   tn.DispensedAmount=" & pDispensedAmount & " "
            Qstr += "             )"

            cn = New System.Data.SqlClient.SqlConnection(CONFIGClass.ConnectionString)
            cn.Open()


            cmd = New System.Data.SqlClient.SqlCommand(Qstr, cn)

            r = cmd.ExecuteReader
            If Not r.HasRows Then
                log.loglog("getDPINByAmount Error with Qstr=[" & Qstr & "]  no rows are there", False)
                rtrn = cnst_ErrCode_OriginalRequestNotFound
                cn.Close()
                cn = Nothing
                cmd = Nothing
            Else

                r.Read()
                pDPIN = r("DepositorPIN")
                cn.Close()
                cn = Nothing
                cmd = Nothing
                rtrn = 0

            End If


            Return rtrn
        Catch ex As Exception
            log.loglog("getDPINByAmount Error with Qstr=[" & Qstr & "] Ex:" & ex.ToString, False)
            Return 9
        End Try

    End Function

    Private Function isTransactionNew() As Integer
        Dim cn As System.Data.SqlClient.SqlConnection
        Dim cmd As System.Data.SqlClient.SqlCommand
        Dim Qstr As String = ""
        Dim r As SqlClient.SqlDataReader
        Dim Subquery As String = ""
        Dim rtrn As String
        Try

            Qstr = "select * from NewTransactions  "
            Qstr += " where "
            Qstr += " TransactionCode='" & Hosttransactioncode & "'"


            cn = New System.Data.SqlClient.SqlConnection(CONFIGClass.ConnectionString)
            cn.Open()


            cmd = New System.Data.SqlClient.SqlCommand(Qstr, cn)

            r = cmd.ExecuteReader
            If Not r.HasRows Then
                log.loglog("isTransactionNew Error with Qstr=[" & Qstr & "]  no rows are there", False)
                rtrn = cnst_ErrCode_OriginalRequestNotFound
                cn.Close()
                cn = Nothing
                cmd = Nothing
            Else

                cn.Close()
                cn = Nothing
                cmd = Nothing
                rtrn = checkNewTransactionKeyTrials()
            End If


            Return rtrn
        Catch ex As Exception
            log.loglog("isTransactionNew Error with Qstr=[" & Qstr & "] Ex:" & ex.ToString, False)
            Return 9
        End Try

    End Function
    Private Function isTransaction(ByRef pDepositCurrency As String) As Integer
        Dim cn As System.Data.SqlClient.SqlConnection
        Dim cmd As System.Data.SqlClient.SqlCommand
        Dim Qstr As String = ""
        Dim r As SqlClient.SqlDataReader
        Dim Subquery As String = ""
        Dim rtrn As String
        Try

            Qstr = "select * from transactions  "
            Qstr += " where "
            Qstr += " TransactionCode='" & Hosttransactioncode & "'"


            cn = New System.Data.SqlClient.SqlConnection(CONFIGClass.ConnectionString)
            cn.Open()


            cmd = New System.Data.SqlClient.SqlCommand(Qstr, cn)

            r = cmd.ExecuteReader
            If Not r.HasRows Then
                log.loglog("lCheckPINs Error with Qstr=[" & Qstr & "]  no rows are there", False)
                rtrn = cnst_ErrCode_OriginalRequestNotFound
                cn.Close()
                cn = Nothing
                cmd = Nothing
            Else

                r.Read()
                pDepositCurrency = r("CurrencyCode")
                If Not IsDBNull(r("SMSLanguage")) Then
                    SMSLanguage = r("SMSLanguage")
                End If

                If Not IsDBNull(r("BeneficiaryMobile")) Then
                    BeneficiaryMobile = r("BeneficiaryMobile")
                End If

                cn.Close()
                cn = Nothing
                cmd = Nothing
                rtrn = checkTransactionKeyTrials()



            End If


            Return rtrn
        Catch ex As Exception
            log.loglog("isTransaction Error with Qstr=[" & Qstr & "] Ex:" & ex.ToString, False)
            Return 9
        End Try

    End Function
    'Private Function isTransactionRedemption(ByRef pDepositCurrency As String) As Integer
    '    Dim cn As System.Data.SqlClient.SqlConnection
    '    Dim cmd As System.Data.SqlClient.SqlCommand
    '    Dim Qstr As String = ""
    '    Dim r As SqlClient.SqlDataReader
    '    Dim Subquery As String = ""
    '    Dim rtrn As String
    '    Try





    '        Qstr = "select * from transactions  "
    '        Qstr += " where "
    '        Qstr += " TransactionCode='" & Hosttransactioncode & "' "
    '        Qstr += " and BeneficiaryMobile ='" & BeneficiaryMobile & "' "
    '        Qstr += " and DepositorMobile ='" & DepositorMobile & "'"

    '        cn = New System.Data.SqlClient.SqlConnection(CONFIGClass.ConnectionString)
    '        cn.Open()


    '        cmd = New System.Data.SqlClient.SqlCommand(Qstr, cn)

    '        r = cmd.ExecuteReader
    '        If Not r.HasRows Then
    '            log.loglog("isTransactionRedemption Error with Qstr=[" & Qstr & "]  no rows are there", False)
    '            rtrn = cnst_ErrCode_OriginalRequestNotFound
    '            cn.Close()
    '            cn = Nothing
    '            cmd = Nothing
    '        Else

    '            r.Read()
    '            pDepositCurrency = r("CurrencyCode")
    '            If Not IsDBNull(r("SMSLanguage")) Then
    '                SMSLanguage = r("SMSLanguage")
    '            End If

    '            'If Not IsDBNull(r("BeneficiaryMobile")) Then
    '            '    BeneficiaryMobile = r("BeneficiaryMobile")
    '            'End If

    '            cn.Close()
    '            cn = Nothing
    '            cmd = Nothing
    '            rtrn = checkTransactionKeyTrials()



    '        End If


    '        Return rtrn
    '    Catch ex As Exception
    '        log.loglog("isTransactionRedemption Error with Qstr=[" & Qstr & "] Ex:" & ex.ToString, False)
    '        Return 9
    '    End Try

    'End Function
    Private Function checkTransactionKeyTrials() As Integer
        Dim cn As System.Data.SqlClient.SqlConnection
        Dim cmd As System.Data.SqlClient.SqlCommand
        Dim Qstr As String = ""
        Dim r As SqlClient.SqlDataReader
        Dim Subquery As String = ""
        Dim rtrn As String
        Try
            Qstr = "select count(*) as trials from TransactionKeyCheckTrials "
            Qstr += " where "
            Qstr += " TransactionCode='" & Hosttransactioncode & "'"
            Qstr += " and TrialFlag=0"


            cn = New System.Data.SqlClient.SqlConnection(CONFIGClass.ConnectionString)
            cn.Open()


            cmd = New System.Data.SqlClient.SqlCommand(Qstr, cn)

            r = cmd.ExecuteReader
            If Not r.HasRows Then
                log.loglog("checkTransactionKeyTrials Error with Qstr=[" & Qstr & "]  no rows are there", False)
                rtrn = 0
            Else
                r.Read()
                If r("trials") >= mvMaximumKeyTrials Then
                    rtrn = cnst_ErrCode_ManyKeyTrilas
                Else
                    rtrn = 0
                End If
            End If

            cn.Close()
            cn = Nothing
            cmd = Nothing
            Return rtrn
        Catch ex As Exception
            log.loglog("checkTransactionKeyTrials Error with Qstr=[" & Qstr & "] Ex:" & ex.ToString, False)
            Return 9
        End Try

    End Function

    Private Function checkNewTransactionKeyTrials() As Integer
        Dim cn As System.Data.SqlClient.SqlConnection
        Dim cmd As System.Data.SqlClient.SqlCommand
        Dim Qstr As String = ""
        Dim r As SqlClient.SqlDataReader
        Dim Subquery As String = ""
        Dim rtrn As String
        Try
            Qstr = "select count(*) as trials from NewTransactionKeyCheckTrials "
            Qstr += " where "
            Qstr += " TransactionCode='" & Hosttransactioncode & "'"
            Qstr += " and TrialFlag=0"


            cn = New System.Data.SqlClient.SqlConnection(CONFIGClass.ConnectionString)
            cn.Open()


            cmd = New System.Data.SqlClient.SqlCommand(Qstr, cn)

            r = cmd.ExecuteReader
            If Not r.HasRows Then
                log.loglog("checkNewTransactionKeyTrials Error with Qstr=[" & Qstr & "]  no rows are there", False)
                rtrn = 0
            Else
                r.Read()
                If r("trials") >= mvMaximumKeyTrials Then
                    rtrn = cnst_ErrCode_ManyKeyTrilas
                Else
                    rtrn = 0
                End If
            End If

            cn.Close()
            cn = Nothing
            cmd = Nothing
            Return rtrn
        Catch ex As Exception
            log.loglog("checkNewTransactionKeyTrials Error with Qstr=[" & Qstr & "] Ex:" & ex.ToString, False)
            Return 9
        End Try

    End Function

    Private Function checkBlockedMobile(ByVal pMobileNumber As String, ByVal DepositorOrBenificiary As Integer) As Integer
        Dim cn As System.Data.SqlClient.SqlConnection
        Dim cmd As System.Data.SqlClient.SqlCommand
        Dim Qstr As String = ""
        Dim r As SqlClient.SqlDataReader
        Dim Subquery As String = ""
        Dim rtrn As String

        Try
            Qstr = "select *  from BlockedCustomers "
            Qstr += " where "
            Qstr += " MobileNumber='" & pMobileNumber & "'"
            Qstr += " and UnBlocked=0 and DepositorOrBeneficiary=" & DepositorOrBenificiary


            cn = New System.Data.SqlClient.SqlConnection(CONFIGClass.ConnectionString)
            cn.Open()


            cmd = New System.Data.SqlClient.SqlCommand(Qstr, cn)

            r = cmd.ExecuteReader
            If r.HasRows = True Then
                rtrn = 1
                log.loglog("checkBlockedMobile Mobile =[" & pMobileNumber & "] is blocked", False)
            Else
                ' r.Read()
                'dbDorB = r("DepositorOrBeneficiary")
                'If DepositorOrBenificiary <> dbDorB Then
                ' rtrn = 0
                'Else

                rtrn = 0
                'End If

            End If

            cn.Close()
            cn = Nothing
            cmd = Nothing
            Return rtrn
        Catch ex As Exception
            log.loglog("checkBlockedMobile Error with Qstr=[" & Qstr & "] Ex:" & ex.ToString, False)
            Return 9
        End Try

    End Function
    Private Function IsRegisteredMobile(ByVal pMobileNumber As String) As Boolean
        Dim cn As System.Data.SqlClient.SqlConnection
        Dim cmd As System.Data.SqlClient.SqlCommand
        Dim Qstr As String = ""
        Dim r As SqlClient.SqlDataReader
        Dim Subquery As String = ""
        Dim rtrn As String

        Try
            Qstr = "select *  from RegisteredCustomer "
            Qstr += " where "
            Qstr += " MobileNumber='" & pMobileNumber & "'"


            cn = New System.Data.SqlClient.SqlConnection(CONFIGClass.ConnectionString)
            cn.Open()


            cmd = New System.Data.SqlClient.SqlCommand(Qstr, cn)

            r = cmd.ExecuteReader
            If r.HasRows = True Then
                rtrn = True

            Else

                log.loglog("IsRegisteredMobile Mobile =[" & pMobileNumber & "] is not registered", False)
                rtrn = False


            End If

            cn.Close()
            cn = Nothing
            cmd = Nothing
            Return rtrn
        Catch ex As Exception
            log.loglog("IsRegisteredMobile Error with Qstr=[" & Qstr & "] Ex:" & ex.ToString, False)
            Return False
        End Try

    End Function


    Private Function ResetkTransactionKeyTrials() As Integer
        Dim cn As System.Data.SqlClient.SqlConnection
        Dim cmd As System.Data.SqlClient.SqlCommand
        Dim Qstr As String = ""
        Dim r As Integer
        Dim Subquery As String = ""
        'Dim rtrn As String
        Try
            Qstr = "update TransactionKeyCheckTrials  set TrialFlag=1"
            Qstr += " where "
            Qstr += " TransactionCode='" & Hosttransactioncode & "'"



            cn = New System.Data.SqlClient.SqlConnection(CONFIGClass.ConnectionString)
            cn.Open()


            cmd = New System.Data.SqlClient.SqlCommand(Qstr, cn)

            r = cmd.ExecuteNonQuery
            If r < 1 Then
                log.loglog("ResetkTransactionKeyTrials Error with Qstr=[" & Qstr & "]  no rows are affected", False)

            End If

            cn.Close()
            cn = Nothing
            cmd = Nothing
            Return 0
        Catch ex As Exception
            log.loglog("ResetkTransactionKeyTrials Error with Qstr=[" & Qstr & "] Ex:" & ex.ToString, False)
            Return 9
        End Try

    End Function

    Private Function ResetNewTransactionKeyTrials() As Integer
        Dim cn As System.Data.SqlClient.SqlConnection
        Dim cmd As System.Data.SqlClient.SqlCommand
        Dim Qstr As String = ""
        Dim r As Integer
        Dim Subquery As String = ""
        'Dim rtrn As String
        Try
            Qstr = "update NewTransactionKeyCheckTrials  set TrialFlag=1"
            Qstr += " where "
            Qstr += " TransactionCode='" & Hosttransactioncode & "'"



            cn = New System.Data.SqlClient.SqlConnection(CONFIGClass.ConnectionString)
            cn.Open()


            cmd = New System.Data.SqlClient.SqlCommand(Qstr, cn)

            r = cmd.ExecuteNonQuery
            If r < 1 Then
                log.loglog("ResetNewTransactionKeyTrials Error with Qstr=[" & Qstr & "]  no rows are affected", False)

            End If

            cn.Close()
            cn = Nothing
            cmd = Nothing
            Return 0
        Catch ex As Exception
            log.loglog("ResetNewTransactionKeyTrials Error with Qstr=[" & Qstr & "] Ex:" & ex.ToString, False)
            Return 9
        End Try

    End Function

    Private Function lCheckPINsNew() As Integer
        Dim cn As System.Data.SqlClient.SqlConnection
        Dim cmd As System.Data.SqlClient.SqlCommand
        Dim Qstr As String = ""
        Dim r As SqlClient.SqlDataReader
        Dim Subquery As String = ""
        Dim rtrn As Integer
        Try
            Qstr = "select * from NewTransactions  "
            Qstr += " where "
            Qstr += " TransactionCode='" & Hosttransactioncode & "'"
            Qstr += " and   BankCode='" & CONFIGClass.LocalBank & "'"
            Qstr += " and   CountryCode='" & CONFIGClass.LocalCountry & "'"
            cn = New System.Data.SqlClient.SqlConnection(CONFIGClass.ConnectionString)
            cn.Open()


            cmd = New System.Data.SqlClient.SqlCommand(Qstr, cn)

            r = cmd.ExecuteReader
            If Not r.HasRows Then
                log.loglog("lCheckPINsNew Error with Qstr=[" & Qstr & "]  no rows are there", False)
                lNewInsertKeyCkecTrial()
                rtrn = cnst_ErrCode_WrongPINs
            Else
                r.Read()
                Try
                    Amount = r("Amount")
                    rtrn = 0
                Catch ex As Exception
                    log.loglog("lCheckPINsNew bad or null Amount  with Qstr=[" & Qstr & "]  ex:[" & ex.ToString & "]", False)
                    rtrn = cnst_ErrCode_PadorNullAmountValue
                End Try

            End If

            cn.Close()
            cn = Nothing
            cmd = Nothing
            Return rtrn
        Catch ex As Exception
            log.loglog("lCheckPINsNew Error with Qstr=[" & Qstr & "] Ex:" & ex.ToString, False)
            Return 9
        End Try

    End Function
    Private Function lCheckPINs() As Integer
        Dim cn As System.Data.SqlClient.SqlConnection
        Dim cmd As System.Data.SqlClient.SqlCommand
        Dim Qstr As String = ""
        Dim r As SqlClient.SqlDataReader
        Dim Subquery As String = ""
        Dim rtrn As Integer
        Try
            Qstr = "select * from transactions  "
            Qstr += " where "
            Qstr += " TransactionCode='" & Hosttransactioncode & "'"
            If RequestType = cnst_RequestType_WithdrawalAuthorization Or _
               RequestType = cnst_RequestType_WithdrawalConfirmation Or _
               RequestType = cnst_RequestType_WithdrawalCancelation Then
                Qstr += " and BeneficiaryPIN ='" & BeneficiaryPIN & "' "
                Qstr += " and   DepositorPIN ='" & DepositorPIN & "' "

            End If

            Qstr += " and   BankCode='" & BankId & "'"
            Qstr += " and   ATMId='" & ATMId & "'"
            Qstr += " and   CountryCode='" & Country & "'"
            cn = New System.Data.SqlClient.SqlConnection(CONFIGClass.ConnectionString)
            cn.Open()


            cmd = New System.Data.SqlClient.SqlCommand(Qstr, cn)

            r = cmd.ExecuteReader
            If Not r.HasRows Then
                log.loglog("lCheckPINs Error with Qstr=[" & Qstr & "]  no rows are there", False)
                lInsertKeyCkecTrial()
                rtrn = cnst_ErrCode_WrongPINs
            Else
                r.Read()
                Try
                    Amount = r("Amount")
                    rtrn = 0
                Catch ex As Exception
                    log.loglog("lCheckPINs bad or null Amount  with Qstr=[" & Qstr & "]  ex:[" & ex.ToString & "]", False)
                    rtrn = cnst_ErrCode_PadorNullAmountValue
                End Try

            End If

            cn.Close()
            cn = Nothing
            cmd = Nothing
            Return rtrn
        Catch ex As Exception
            log.loglog("lCheckPINs Error with Qstr=[" & Qstr & "] Ex:" & ex.ToString, False)
            Return 9
        End Try

    End Function
    Private Function lCheckPINs_Teller() As Integer
        Dim cn As System.Data.SqlClient.SqlConnection
        Dim cmd As System.Data.SqlClient.SqlCommand
        Dim Qstr As String = ""
        Dim r As SqlClient.SqlDataReader
        Dim Subquery As String = ""
        Dim rtrn As Integer
        Try
            Qstr = "select * from transactions  "
            Qstr += " where "
            Qstr += " TransactionCode='" & Hosttransactioncode & "'"
            If RequestType = cnst_RequestType_WithdrawalAuthorization Or _
               RequestType = cnst_RequestType_WithdrawalConfirmation Or _
               RequestType = cnst_RequestType_WithdrawalCancelation Then
                Qstr += " and BeneficiaryPIN ='" & BeneficiaryPIN & "' "
                Qstr += " and   DepositorPIN ='" & DepositorPIN & "' "

            End If

            cn = New System.Data.SqlClient.SqlConnection(CONFIGClass.ConnectionString)
            cn.Open()


            cmd = New System.Data.SqlClient.SqlCommand(Qstr, cn)

            r = cmd.ExecuteReader
            If Not r.HasRows Then
                log.loglog("lCheckPINs_Teller Error with Qstr=[" & Qstr & "]  no rows are there", False)
                lInsertKeyCkecTrial()
                rtrn = cnst_ErrCode_WrongPINs
            Else
                r.Read()
                Try
                    Amount = r("Amount")
                    DepositorMobile = r("DepositorMobile")
                    BeneficiaryMobile = r("BeneficiaryMobile")
                    rtrn = 0
                Catch ex As Exception
                    log.loglog("lCheckPINs_Teller bad or null Amount  with Qstr=[" & Qstr & "]  ex:[" & ex.ToString & "]", False)
                    rtrn = (cnst_ErrCode_PadorNullAmountValue)
                End Try

            End If

            cn.Close()
            cn = Nothing
            cmd = Nothing
            Return rtrn
        Catch ex As Exception
            log.loglog("lCheckPINs_Teller Error with Qstr=[" & Qstr & "] Ex:" & ex.ToString, False)
            Return 9
        End Try

    End Function
    Private Function lCheckPINs_Redemption() As Integer
        Dim cn As System.Data.SqlClient.SqlConnection
        Dim cmd As System.Data.SqlClient.SqlCommand
        Dim Qstr As String = ""
        Dim r As SqlClient.SqlDataReader
        Dim Subquery As String = ""
        Dim rtrn As Integer
        Try
            Qstr = "select * from transactions  "
            Qstr += " where "
            Qstr += " TransactionCode='" & Hosttransactioncode & "'"
            If RequestType = cnst_RequestType_RedemptionAutorization Or _
               RequestType = cnst_RequestType_RedemptionConfirmation Or _
               RequestType = cnst_RequestType_RedemptionCancelation Then
                Qstr += " and redemptionPIN ='" & RedempltionPIN & "' "
                Qstr += " and BeneficiaryMobile ='" & BeneficiaryMobile & "' "
                Qstr += " and DepositorMobile ='" & DepositorMobile & "' "

            End If

            cn = New System.Data.SqlClient.SqlConnection(CONFIGClass.ConnectionString)
            cn.Open()


            cmd = New System.Data.SqlClient.SqlCommand(Qstr, cn)

            r = cmd.ExecuteReader
            If Not r.HasRows Then
                log.loglog("lCheckPINs_Teller Error with Qstr=[" & Qstr & "]  no rows are there", False)
                lInsertKeyCkecTrial()
                rtrn = cnst_ErrCode_WrongPINs
            Else
                r.Read()
                Try
                    Amount = r("Amount")
                    DepositorMobile = r("DepositorMobile")
                    BeneficiaryMobile = r("BeneficiaryMobile")
                    rtrn = 0
                Catch ex As Exception
                    log.loglog("lCheckPINs_Teller bad or null Amount  with Qstr=[" & Qstr & "]  ex:[" & ex.ToString & "]", False)
                    rtrn = cnst_ErrCode_PadorNullAmountValue
                End Try

            End If

            cn.Close()
            cn = Nothing
            cmd = Nothing
            Return rtrn
        Catch ex As Exception
            log.loglog("lCheckPINs_Teller Error with Qstr=[" & Qstr & "] Ex:" & ex.ToString, False)
            Return 9
        End Try

    End Function


    Private Function lValidateATMIPAddress(ByVal pATMId As String, ByVal pRemoteIP As String) As Integer
        Dim cn As System.Data.SqlClient.SqlConnection
        Dim cmd As System.Data.SqlClient.SqlCommand
        Dim dr As System.Data.SqlClient.SqlDataReader
        Dim Qstr As String
        Dim storedIP As String = ""
        Dim ret As Integer

        Try
            Qstr = "select * from ATM where "
            Qstr = Qstr & " ATMId='" & pATMId & "' and bankcode='" & CONFIGClass.LocalBank & "' and CountryCode='" & CONFIGClass.LocalCountry & "'"

            cn = New System.Data.SqlClient.SqlConnection(CONFIGClass.ConnectionString)
            cn.Open()


            cmd = New System.Data.SqlClient.SqlCommand(Qstr, cn)

            dr = cmd.ExecuteReader()
            If dr.HasRows = True Then
                dr.Read()
                Try

                    storedIP = dr("ATMIPAddress")
                    If storedIP = pRemoteIP Then
                        mvISTeller = dr("IsTeller")
                        ret = 0
                    Else
                        log.loglog("lValidateATMIPAddress Error stored Ip=[" & storedIP & "] and remoteIp=[" & pRemoteIP & "]", False)
                        ret = 7
                    End If
                Catch exr As Exception
                    log.loglog("lValidateATMIPAddress Error Exr:" & exr.ToString, False)
                    ret = 2
                End Try
            Else
                log.loglog("lValidateATMIPAddress Error:No Rows for ATMId=" & pATMId, False)
                ret = 1
            End If
            dr.Close()
            cn.Close()
            cn = Nothing
            cmd = Nothing
            dr = Nothing
            Return ret

        Catch ex As Exception
            log.loglog("lValidateATMIPAddress Error connection string:[" & CONFIGClass.ConnectionString & "] Ex:" & ex.ToString, False)
            Return 9
        End Try

    End Function
    Private Function lValidateMaximumDailyCount(ByVal pMobile As String, ByVal pDepositororBeneficiary As String) As Integer
        Dim cn As System.Data.SqlClient.SqlConnection
        Dim cmd As System.Data.SqlClient.SqlCommand
        Dim dr As System.Data.SqlClient.SqlDataReader
        Dim Qstr As String
        Dim totaldaydepositCount As Integer
        Dim ret As Integer

        Try
            Qstr = " select count(*)as TodayTotalDepositCount  "
            Qstr += " from transactions "
            If pDepositororBeneficiary = "D" Then
                Qstr += " where depositormobile like '%" & pMobile.Trim & "'"
            Else
                Qstr += " where BeneficiaryMobile like '%" & pMobile.Trim & "'"
            End If

            Qstr += " and   depositstatus = 'CONFIRMED' "
            Qstr += " and   year(depositdatetime) = year(getdate())"
            Qstr += " and   month(depositdatetime) = month(getdate())"
            Qstr += " and   day(depositdatetime) = day(getdate())"

            cn = New System.Data.SqlClient.SqlConnection(CONFIGClass.ConnectionString)
            cn.Open()


            cmd = New System.Data.SqlClient.SqlCommand(Qstr, cn)

            dr = cmd.ExecuteReader()
            If dr.HasRows = True Then
                dr.Read()
                Try
                    If IsDBNull(dr("TodayTotalDepositCount")) Then
                        ret = 0
                    Else
                        totaldaydepositCount = dr("TodayTotalDepositCount")
                        If totaldaydepositCount >= mvMaximumDailyCount Then
                            log.loglog("lValidateMaximumDailyCount, Today Total deposit for mobile [" & pMobile & "] is [" & totaldaydepositCount & "] which is greater than max allowed value [" & mvMaximumDailyCount & "] ", False)
                            ret = cnst_ErrCode_TodayTotalDepositAmountExceedsMaxAllowedCount
                        Else
                            ret = 0
                        End If
                    End If


                Catch exr As Exception
                    log.loglog("lValidateMaximumDailyCount Error Exr:" & exr.ToString, False)
                    ret = 0
                End Try
            Else
                ret = 0
            End If
            dr.Close()
            cn.Close()
            cn = Nothing
            cmd = Nothing
            dr = Nothing
            Return ret

        Catch ex As Exception
            log.loglog("lValidateMaximumDailyCount Error connection string:[" & CONFIGClass.ConnectionString & "] Ex:" & ex.ToString, False)
            Return 9
        End Try

    End Function

    Private Function lValidateMaximumDailyAmount(ByVal pMobile As String, ByVal pDepositororBeneficiary As String, ByVal pCurrentDepositAmount As Integer) As Integer
        Dim cn As System.Data.SqlClient.SqlConnection
        Dim cmd As System.Data.SqlClient.SqlCommand
        Dim dr As System.Data.SqlClient.SqlDataReader
        Dim Qstr As String
        Dim totaldaydeposit As Integer
        Dim ret As Integer

        Try
            Qstr = " select sum(amount)as TodayTotalDepositValue  "
            Qstr += " from transactions "
            If pDepositororBeneficiary = "D" Then
                Qstr += " where depositormobile like '%" & pMobile.Trim & "'"
            Else
                Qstr += " where BeneficiaryMobile like '%" & pMobile.Trim & "'"
            End If

            Qstr += "and   depositstatus = 'CONFIRMED' "
            Qstr += " and   year(depositdatetime) = year(getdate())"
            Qstr += " and   month(depositdatetime) = month(getdate())"
            Qstr += " and   day(depositdatetime) = day(getdate())"

            cn = New System.Data.SqlClient.SqlConnection(CONFIGClass.ConnectionString)
            cn.Open()


            cmd = New System.Data.SqlClient.SqlCommand(Qstr, cn)

            dr = cmd.ExecuteReader()
            If dr.HasRows = True Then
                dr.Read()
                Try

                    If IsDBNull(dr("TodayTotalDepositValue")) Then
                        ret = 0
                    Else
                        totaldaydeposit = dr("TodayTotalDepositValue") + pCurrentDepositAmount
                        If totaldaydeposit > mvMaximumDailyAmount Then
                            log.loglog("lValidateMaximumDailyAmount, Today Total deposit for mobile [" & pMobile & "] is [" & totaldaydeposit & "] which is greater than max allowed value [" & mvMaximumDailyAmount & "] ", False)
                            ret = cnst_ErrCode_TodayTotalDepositAmountExceedsMaxAllowedValue

                        End If
                    End If


                Catch exr As Exception
                    log.loglog("lValidateMaximumDailyAmount Error Exr:" & exr.ToString, False)
                    ret = 0
                End Try
            Else
                ret = 0
            End If
            dr.Close()
            cn.Close()
            cn = Nothing
            cmd = Nothing
            dr = Nothing
            Return ret

        Catch ex As Exception
            log.loglog("lValidateMaximumDailyAmount Error connection string:[" & CONFIGClass.ConnectionString & "] Ex:" & ex.ToString, False)
            Return 9
        End Try

    End Function


    Private Function lValidateMaximumMonthlyAmount(ByVal pMobile As String, ByVal pDepositororBeneficiary As String, ByVal pCurrentDepositAmount As Integer) As Integer
        Dim cn As System.Data.SqlClient.SqlConnection
        Dim cmd As System.Data.SqlClient.SqlCommand
        Dim dr As System.Data.SqlClient.SqlDataReader
        Dim Qstr As String
        Dim totaldaydeposit As Integer
        Dim ret As Integer

        Try
            Qstr = " select sum(amount)as ToMonthTotalDepositValue  "
            Qstr += " from transactions "
            If pDepositororBeneficiary = "D" Then
                Qstr += " where depositormobile like '%" & pMobile.Trim & "'"
            Else
                Qstr += " where BeneficiaryMobile like '%" & pMobile.Trim & "'"
            End If
            Qstr += "and   depositstatus = 'CONFIRMED' "
            Qstr += " and   year(depositdatetime) = year(getdate())"
            Qstr += " and   month(depositdatetime) = month(getdate())"

            cn = New System.Data.SqlClient.SqlConnection(CONFIGClass.ConnectionString)
            cn.Open()


            cmd = New System.Data.SqlClient.SqlCommand(Qstr, cn)

            dr = cmd.ExecuteReader()
            If dr.HasRows = True Then
                dr.Read()
                Try

                    If IsDBNull(dr("ToMonthTotalDepositValue")) Then
                        ret = 0
                    Else
                        totaldaydeposit = dr("ToMonthTotalDepositValue") + pCurrentDepositAmount
                        If totaldaydeposit > mvMaximumMonthlyAmount Then
                            log.loglog("lValidateMaximumMonthlyAmount, Today Total deposit for mobile [" & pMobile & "] is [" & totaldaydeposit & "] which is greater than max allowed value [" & mvMaximumDailyAmount & "] ", False)
                            ret = cnst_ErrCode_ToMonthTotalDepositAmountExceedsMaxAllowedValue

                        End If
                    End If


                Catch exr As Exception
                    log.loglog("lValidateMaximumMonthlyAmount Error Exr:" & exr.ToString, False)
                    ret = 0
                End Try
            Else
                ret = 0
            End If
            dr.Close()
            cn.Close()
            cn = Nothing
            cmd = Nothing
            dr = Nothing
            Return ret

        Catch ex As Exception
            log.loglog("lValidateMaximumMonthlyAmount Error connection string:[" & CONFIGClass.ConnectionString & "] Ex:" & ex.ToString, False)
            Return 9
        End Try

    End Function


    Private Function lParseRequest(ByVal lIncomingRequestData As String) As Integer
        Dim ll As Integer
        Dim curPos As Integer

        If lIncomingRequestData.Length = 0 Then
            log.loglog("Parsing Request: bad request length", False)
            Return 1
        End If
        Try

            RedempltionPIN = ""
            DepositorPIN = ""
            BeneficiaryPIN = ""
            curPos = 0
            ATMId = lIncomingRequestData.Substring(curPos, CONFIGClass.ATMIdLength).Trim() : curPos += CONFIGClass.ATMIdLength
            BankId = lIncomingRequestData.Substring(curPos, CONFIGClass.BankIdLength).Trim() : curPos += CONFIGClass.BankIdLength
            Country = lIncomingRequestData.Substring(curPos, CONFIGClass.CountryCodeLength).Trim() : curPos += CONFIGClass.CountryCodeLength
            RequestType = lIncomingRequestData.Substring(curPos, 2).Trim() : curPos += 2
            ResponseCode = lIncomingRequestData.Substring(curPos, 5).Trim() : curPos += 5
            ATMDate = lIncomingRequestData.Substring(curPos, 10).Trim() : curPos += 10
            ATMTime = lIncomingRequestData.Substring(curPos, 8).Trim() : curPos += 8
            TransactionSequence = lIncomingRequestData.Substring(curPos, 10).Trim() : curPos += 10
            DepositorMobile = lIncomingRequestData.Substring(curPos, 20).Trim() : curPos += 20
            DepositorPIN = lIncomingRequestData.Substring(curPos, 10).Trim() : curPos += 10
            BeneficiaryMobile = lIncomingRequestData.Substring(curPos, 20).Trim() : curPos += 20
            BeneficiaryPIN = lIncomingRequestData.Substring(curPos, 10).Trim() : curPos += 10
            Amount = lIncomingRequestData.Substring(curPos, 15).Trim() : curPos += 15
            Currency = lIncomingRequestData.Substring(curPos, 5).Trim() : curPos += 5
            Hosttransactioncode = lIncomingRequestData.Substring(curPos, 20).Trim() : curPos += 20
            ActionReason = lIncomingRequestData.Substring(curPos, 25).Trim() : curPos += 25
            DispensedNotes = Val(lIncomingRequestData.Substring(curPos, 8).Trim()) : curPos += 8
            DispensedAmount = Val(lIncomingRequestData.Substring(curPos, 15).Trim()) : curPos += 15
            CommissionAmount = Val(lIncomingRequestData.Substring(curPos, 15).Trim()) : curPos += 15
            SMSLanguage = lIncomingRequestData.Substring(curPos, 1).Trim() : curPos += 1
            If SMSLanguage.Trim.ToUpper <> "A" Then
                SMSLanguage = "E"
            End If
            MinimumValue = "" ' lIncomingRequestData.Substring(curPos, 15).Trim():curPos+=15
            MaximumValue = "" ' lIncomingRequestData.Substring(curPos, 15).Trim():curPos+=15
            ReceiptLine1 = "" ''lIncomingRequestData.Substring(curPos, 40).Trim():curPos+=40
            ReceiptLine2 = "" ''lIncomingRequestData.Substring(curPos, 40).Trim():curPos+=40
            ReceiptLine3 = "" ' lIncomingRequestData.Substring(curPos, 40).Trim():curPos+=40
            curPos += 150

            If lIncomingRequestData.Length >= curPos + 60 Then
                ExtraData = lIncomingRequestData.Substring(curPos, 60)
                If RequestType = cnst_RequestType_CBDebitAmountAuthorization Then
                    CCTrack2 = ExtraData.Substring(0, 40).Trim
                    If CCTrack2.Substring(0, 1) = ";" Then
                        CCTrack2 = CCTrack2.Substring(1)
                    End If
                    If CCTrack2.LastIndexOf("?") = CCTrack2.Length - 1 Then
                        CCTrack2 = CCTrack2.Substring(0, CCTrack2.Length - 1)
                    End If
                    DebitAccountNumber = ExtraData.Substring(40, 20).Trim
                    ll = 19 - DebitAccountNumber.Length
                    If ll > 0 And ll < 20 Then
                        DebitAccountNumber = "0000000000000000000".Substring(0, ll) & DebitAccountNumber
                    End If
                    ExtraData = ExtraData.Trim
                    ExtraData = ""
                End If

            Else
                ExtraData = ""
            End If




            Amount = Val(Amount)

            If RequestType = cnst_RequestType_RedemptionAutorization Or _
                RequestType = cnst_RequestType_RedemptionConfirmation Or _
                RequestType = cnst_RequestType_RedemptionCancelation Then
                RedempltionPIN = DepositorPIN
            End If

            Return 0
        Catch ex As Exception
            log.loglog("Parsing Result:" & vbNewLine & toString(), False)
            log.loglog("Parsing Request Error:" & ex.ToString, False)
            Return (9)
        End Try

    End Function

    Private Function lFormReply(ByRef loutGoingReplyData As String) As Integer
        Dim rspCodeInt As Integer

        Try
            '------------- just log all activities done on the atm ------
            Try
                rspCodeInt = Val(ResponseCode)
                If rspCodeInt <> 0 Then

                    InsertExceptionActions(RequestType, rspCodeInt)
                End If

            Catch ex As Exception

            End Try
            '---------------------------------------------------------------

            loutGoingReplyData = ""
            loutGoingReplyData += (ATMId & Space(5)).Substring(0, CONFIGClass.ATMIdLength)
            loutGoingReplyData += (BankId & Space(5)).Substring(0, CONFIGClass.BankIdLength)
            loutGoingReplyData += (Country & Space(10)).Substring(0, CONFIGClass.CountryCodeLength)
            loutGoingReplyData += (RequestType & Space(2)).Substring(0, 2)
            loutGoingReplyData += (ResponseCode & Space(5)).Substring(0, 5)
            loutGoingReplyData += (ATMDate & Space(10)).Substring(0, 10)
            loutGoingReplyData += (ATMTime & Space(8)).Substring(0, 8)
            loutGoingReplyData += (TransactionSequence & Space(10)).Substring(0, 10)
            loutGoingReplyData += (DepositorMobile & Space(20)).Substring(0, 20)
            loutGoingReplyData += (DepositorPIN & Space(10)).Substring(0, 10)
            loutGoingReplyData += (BeneficiaryMobile & Space(20)).Substring(0, 20)
            loutGoingReplyData += (BeneficiaryPIN & Space(10)).Substring(0, 10)
            loutGoingReplyData += (Amount & Space(15)).Substring(0, 15)
            loutGoingReplyData += (Currency & Space(5)).Substring(0, 5)
            loutGoingReplyData += (Hosttransactioncode & Space(20)).Substring(0, 20)
            loutGoingReplyData += (ActionReason & Space(25)).Substring(0, 25)
            loutGoingReplyData += (DispensedNotes & Space(8)).Substring(0, 8)
            loutGoingReplyData += (DispensedAmount & Space(15)).Substring(0, 15)
            loutGoingReplyData += (CommissionAmount & Space(15)).Substring(0, 15)
            loutGoingReplyData += (SMSLanguage & Space(1)).Substring(0, 1)
            loutGoingReplyData += (MinimumValue & Space(15)).Substring(0, 15)
            loutGoingReplyData += (MaximumValue & Space(15)).Substring(0, 15)
            loutGoingReplyData += (ReceiptLine1 & Space(40)).Substring(0, 40)
            loutGoingReplyData += (ReceiptLine2 & Space(40)).Substring(0, 40)
            loutGoingReplyData += (ReceiptLine3 & Space(40)).Substring(0, 40)





            Return 0
        Catch ex As Exception
            log.loglog("lFormReply Error:" & vbNewLine & toString(), False)
            log.loglog("lFormReply Error:" & ex.ToString, False)
            Return (9)
        End Try

    End Function

    Private Function lNewFormReply(ByRef loutGoingNewReplyData As String) As Integer
        Dim rspCodeInt As Integer

        Try
            '------------- just log all activities done on the atm ------
            Try
                rspCodeInt = Val(ResponseCode)


            Catch ex As Exception

            End Try
            '---------------------------------------------------------------
             Dim fs = Chr(28)
            loutGoingNewReplyData = ""
            loutGoingNewReplyData += RequestType
            loutGoingNewReplyData += fs
            loutGoingNewReplyData += ATMId
            loutGoingNewReplyData += fs
            loutGoingNewReplyData += ATMDateTime
            loutGoingNewReplyData += fs
            loutGoingNewReplyData += TransactionSequence
            loutGoingNewReplyData += fs
            loutGoingNewReplyData += ResponseCode
            loutGoingNewReplyData += fs
            loutGoingNewReplyData += DispensedAmount
            loutGoingNewReplyData += fs
            loutGoingNewReplyData += CommissionAmount
            loutGoingNewReplyData += fs
            loutGoingNewReplyData += DispensedNotes


            Return 0
        Catch ex As Exception
            log.loglog("lNewFormReply Error:" & vbNewLine & toString(), False)
            log.loglog("lNewFormReply Error:" & ex.ToString, False)
            Return (9)
        End Try

    End Function

    Public Overrides Function toString() As String
        Dim rtrnStr As String

        rtrnStr = "ATMId=[" & ATMId & "]" & vbNewLine
        rtrnStr += "BankId=[" & BankId & "]" & vbNewLine
        rtrnStr += "Country=[" & Country & "]" & vbNewLine
        rtrnStr += "RequestType=[" & RequestType & "]" & vbNewLine
        rtrnStr += "ResponseCode=[" & ResponseCode & "]" & vbNewLine
        rtrnStr += "ATMDate=[" & ATMDate & "]" & vbNewLine
        rtrnStr += "ATMTime=[" & ATMTime & "]" & vbNewLine
        rtrnStr += "TransactionSequence=[" & TransactionSequence & "]" & vbNewLine
        rtrnStr += "DepositorMobile=[" & DepositorMobile & "]" & vbNewLine
        rtrnStr += "DepositorPIN=[" & DepositorPIN & "]" & vbNewLine
        rtrnStr += "BeneficiaryMobile=[" & BeneficiaryMobile & "]" & vbNewLine
        rtrnStr += "BeneficiaryPIN=[" & BeneficiaryPIN & "]" & vbNewLine
        rtrnStr += "Amount=[" & Amount & "]" & vbNewLine
        rtrnStr += "Currency=[" & Currency & "]" & vbNewLine
        rtrnStr += "Hosttransactioncode=[" & Hosttransactioncode & "]" & vbNewLine
        rtrnStr += "ActionReason=[" & ActionReason & "]" & vbNewLine
        rtrnStr += "DispensedNotes=[" & DispensedNotes & "]" & vbNewLine


        rtrnStr += "DispensedAmount=[" & DispensedAmount & "]" & vbNewLine
        rtrnStr += "CommissionAmount=[" & CommissionAmount & "]" & vbNewLine
        rtrnStr += "SMSLanguage=[" & SMSLanguage & "]" & vbNewLine

        rtrnStr += "MinimumValue=[" & MinimumValue & "]" & vbNewLine
        rtrnStr += "MaximumValue=[" & MaximumValue & "]" & vbNewLine

        rtrnStr += "ReceiptLine1=[" & ReceiptLine1 & "]" & vbNewLine
        rtrnStr += "ReceiptLine2=[" & ReceiptLine2 & "]" & vbNewLine
        rtrnStr += "ReceiptLine3=[" & ReceiptLine3 & "]" & vbNewLine

        rtrnStr += "CCTrack2=[" & CCTrack2 & "]" & vbNewLine
        rtrnStr += "DebitAccountNumber=[" & DebitAccountNumber & "]" & vbNewLine
        rtrnStr += "ExtraData=[" & ExtraData & "]" & vbNewLine


        rtrnStr += "IsTeller=[" & mvISTeller & "]" & vbNewLine


        Return rtrnStr
    End Function

    Public Function DODebitAccountAuthorization() As Integer
        Dim req As String = ""
        Dim rep As String = ""
        Dim rtrn As Integer
        Dim isom As iso8583
        Dim de39 As Integer
        Dim de38 As String
        Try
            rtrn = BuildIsoRequest(req)
            If rtrn <> 0 Then
                Return 1
            End If
            rtrn = ContactSwitch(req, CONFIGClass.SwitchIP, rep)
            If rtrn <> 0 Then
                Return 2
            End If


            isom = New iso8583
            rtrn = isom.Parse(rep)

            If rtrn = 0 Then
                log.loglog("DODebitAccountAuthorization, Parsed data:" & vbNewLine & isom.toString, True)

                de39 = Val(isom.getDataField(39))
                de38 = isom.getDataField(38)
                CBTDataElement38 = de38
                CBTDataElement39 = de39

                If de39 <> 0 Then
                    Return cnst_ErrCode_DebitAccountAuthorizationBError + de39
                End If

            Else
                log.loglog("DODebitAccountAuthorization, Error parsing Iso Reply...[" & rep & "]", False)
                Return 3
            End If

            Return 0

        Catch ex As Exception
            log.loglog("DODebitAccountAuthorization, Error :" & ex.ToString, False)

            Return 9

        End Try








    End Function
    Public Function CheckAmount(ByVal Amunt As String) As Integer
        Dim i As Long
        Dim pAMount As Long


        Try
            pAMount = CLng(Amunt)
            If pAMount = 0 Then
                Return 2
            End If
            i = pAMount Mod CONFIGClass.LowestDenom
            If i = 0 Then
                Return 0
            Else
                Return 1
            End If




            Return 0
        Catch ex As Exception
            log.loglog("CheckAmount,Exception :" & ex.ToString, False)
            Return 9
        End Try
    End Function
    Private Function CheckChip(ByVal pCCTrack2 As String) As Boolean
        Dim sepPos As Integer
        Dim indcChar As String
        Try
            sepPos = pCCTrack2.IndexOf("=")
            If sepPos < 0 Then
                Return False
            End If

            If pCCTrack2.Length < sepPos + CONFIGClass.ChipOrMagneticIndicatorIndexInTrack2 Then
                log.loglog("CheckChip, CCtrack2 is shorter than " & sepPos + CONFIGClass.ChipOrMagneticIndicatorIndexInTrack2, False)
                Return False
            End If
            indcChar = pCCTrack2.Substring(sepPos + CONFIGClass.ChipOrMagneticIndicatorIndexInTrack2, 1)
            log.loglog("CheckChip, sub CCTrack is [" & pCCTrack2.Substring(sepPos) & "]  has Magnetic Indecator is  [" & indcChar & "] at relative position [" & CONFIGClass.ChipOrMagneticIndicatorIndexInTrack2 & "]", False)
            If indcChar = CONFIGClass.MagneticIndicatorIndicatorValue Then
                Return False
            Else
                Return True
            End If

        Catch ex As Exception
            log.loglog("CheckChip, Exp:" & ex.ToString, False)
            Return False
        End Try
    End Function
    Public Function BuildIsoRequest(ByRef pReqStr As String) As Integer
        Dim magIndc As Boolean
        Dim dt, req, tan, mmdd, F43 As String
        Try


            dt = Format(Now, "MMddHHmmss")
            mmdd = Format(Now, "MMdd")

            tan = Format(Now, "HHmmss")
            req = ""
            req = CONFIGClass.ISOHeader ' "ISO005000000"
            req = req & CONFIGClass.ISORequestId  '"0200"
            magIndc = Not CheckChip(CCTrack2)
            If magIndc = True Then
                req = req & "B238C00120A18010" & "000000000" & "4" & "000000"
            Else
                req = req & "B238C40120A18010" & "000000000" & "4" & "000000"
            End If


            req = req & CONFIGClass.ProcessingCode ' "000000" '3
            'req = req & "000000001000" '4
            req = req & Val(Amount).ToString("0000000000") & "00" '4
            req = req & dt '7
            req = req & tan '11
            req = req & tan '12
            req = req & mmdd '13
            req = req & mmdd '17
            req = req & CONFIGClass.DataElement18 ' "5999" '18
            If magIndc = False Then ' add de 22 
                req = req & (CONFIGClass.DataElement22 & "000").Substring(0, 3)
            End If
            req = req & CONFIGClass.DataElement32.Length.ToString("00") & CONFIGClass.DataElement32 ' "11" & "81800627220" '32
            'req = req & "37" & "4946069111111203=14041211000065100000" '35

            req = req & CCTrack2.Trim.Length.ToString("00") & CCTrack2
            'req = req & CONFIGClass.DataElement41 ' "BM04320520      " ' 41
            req = req & (ATMId & "                ").Substring(0, 16) ' "BM04320520      " ' 41
            'req = req & "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX".Substring(0, 40) '43
            '' Kareem Nour 1082014
            F43 = "BM REMITTANCE" & Space(40)
            F43 = F43.Substring(0, 38) & "EG"
            req = req & F43 '43
            req = req & "044" & "1" & Space(23) & "4" & New String("0", 19) '48
            req = req & "818" ' 49
            'req = req & "012" & "MISRMISR+000" '60
            req = req & "012" & "NBE NBE +000" '60
            'req = req & "18" & "010000002011771164" '102
            req = req & DebitAccountNumber.Length.ToString("00") & DebitAccountNumber '102
            pReqStr = req
            Return 0

        Catch ex As Exception
            log.loglog("BuildIsoRequest,Exception :" * ex.ToString, False)
            pReqStr = ""
            Return 9
        End Try

    End Function

    Public Function ContactSwitch(ByVal ReqData As String, ByVal pSwitchIP As String, ByRef RepData As String) As Integer
        Dim lipa As IPHostEntry
        Dim lep As IPEndPoint = Nothing
        Dim i As Integer
        Dim ll As Integer
        Dim msg() As Byte
        Dim rep(1000) As Byte
        Dim repStr As String = ""
        Dim rcvLen As Integer
        Dim s As Socket
        Dim repString As String = ""
        Dim pCheckPointPeriod As String = ""
        Dim pSendingMinute As String = ""
        Dim pSendingATMModeInterval As String = ""
        Dim pwaitBeforeSendingtime As String = ""
        Dim lReqData As String
        Dim atmPort As Long = 4466
        Dim hLengthStr As String
        Dim hLength As Long
        Dim oneByte(2) As Byte
        Dim lTimeOut As Long
        Dim HexReq As String
        Try

            Try
                Try
                    atmPort = Integer.Parse(CONFIGClass.SwitchPort)
                Catch ex As Exception
                    log.loglog("ContactSwitch, ATMIP=" & pSwitchIP & " Config port:" & CONFIGClass.SwitchPort & " failed ex=" & ex.ToString, False)
                    s = Nothing
                    Return 1
                End Try

                lipa = Dns.GetHostEntry(pSwitchIP.Trim)
                log.loglog("ContactSwitch,GetHostEntry for IP [" & pSwitchIP.Trim & "] return HostName=" & lipa.HostName & " List Count=" & lipa.AddressList.Length, False)

            Catch ex As Exception
                lipa = Dns.GetHostByAddress(pSwitchIP.Trim)
                log.loglog("ContactSwitch,GetHostByAddress for IP [" & pSwitchIP.Trim & "] return HostName=" & lipa.HostName & " List Count=" & lipa.AddressList.Length, False)

            End Try
            lep = New IPEndPoint(lipa.AddressList(0), atmPort)

            s = New Socket(lep.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp)
            s.Connect(lep)
        Catch ex As Exception
            log.loglog("ContactSwitch, ATMIP=" & pSwitchIP & " port:" & atmPort & " failed ex=" & ex.ToString, False)
            s = Nothing
            Return 2
        End Try
        Try
            lReqData = "00" & ReqData
            msg = Encoding.ASCII.GetBytes(lReqData)
            ll = ReqData.Length

            msg(0) = ll \ 256
            msg(1) = ll Mod 256
            HexReq = ""
            For i = 0 To msg.Length - 1
                HexReq += msg(i).ToString("X") & " "
            Next
        Catch ex As Exception
            log.loglog("ContactSwitch, Error Encoding Req data  ex=" & ex.ToString, False)
            Try
                s.Close()
            Catch ex2 As Exception
                log.loglog("ContactSwitch,Can Not Close socket  ex:" & ex2.ToString, False)
            End Try
            Return 3
        End Try




        Try
            log.loglog("ContactSwitch,About Sending Request data:[" & HexReq & "]" & vbNewLine & "text [" & ReqData & "]", False)
            i = s.Send(msg, 0, msg.Length, SocketFlags.None)
            lTimeOut = CONFIGClass.SwitchTimeOut * 1000
            s.ReceiveTimeout = lTimeOut ' 20000 'one second
            hLengthStr = ""
            hLength = 0
            HexReq = ""
            For i = 0 To 1
                rcvLen = s.Receive(oneByte, 1, SocketFlags.None)
                rep(i) = oneByte(0)
                HexReq += rep(i).ToString("X") & " "

            Next

            hLength = rep(0) * 256
            hLength += rep(1)

            For i = 0 To hLength - 1
                rcvLen = s.Receive(oneByte, 1, SocketFlags.None)
                rep(i) = oneByte(0)
                HexReq += rep(i).ToString("X") & " "

            Next


            repStr = Encoding.ASCII.GetString(rep, 0, hLength)

            RepData = repStr
            log.loglog("ContactSwitch,Receive Host Reply [" & HexReq & "]" & vbNewLine & " text [" & repStr, False)

            s.Close()
            Return 0

        Catch ex As Exception
            log.loglog("ContactSwitch,Receive Host Reply Exception:" & ex.ToString, False)
            s.Close()
            Return 9
        End Try




    End Function

    '' ''Private Sub startHostclientApplication(ByVal ATMId As String)
    '' ''    Dim rtrn As Long

    '' ''    Try
    '' ''        rtrn = FindWindow(vbNullString, "NCR Client Access - " & ATMId)
    '' ''        log.loglog("Findwindow returns:" & rtrn, ATMId, False)
    '' ''        If rtrn = 0 Then
    '' ''            log.loglog("Shell 1 command:[" & CONFIGClass.ServiceExecutablePath & CONFIGClass.HostClientApplication & "]", ATMId, False)
    '' ''            Shell(ConfigClass.ServiceExecutablePath & ConfigClass.HostClientApplication & " " & ATMId)
    '' ''            log.loglog("Shell 2 command:[" & CONFIGClass.ServiceExecutablePath & CONFIGClass.HostClientApplication & "]", ATMId, False)
    '' ''        End If

    '' ''    Catch ex As Exception
    '' ''        log.loglog("Start Host Application Exception:" & ex.ToString, ATMId, False)
    '' ''    End Try

    '' ''End Sub
    '' ''Private Function lGetHostFormatedRequest(ByVal pHostRequestType As String) As String
    '' ''    Dim retStr As String = ""

    '' ''    retStr = Right(Space(19) & DepositorMobile.Trim, 19)
    '' ''    retStr += Right(Space(19) & BeneficiaryMobile.Trim, 19)
    '' ''    retStr += Right(("0000000000" & TransactionSequence), 12)
    '' ''    retStr += Right(("000000000000000" & Amount), 15)
    '' ''    retStr += Right("000000000000000" & CommissionAmount, 15)
    '' ''    retStr += Now.ToString("ddMMyyyy")
    '' ''    retStr += Now.ToString("hh:mm:ss")
    '' ''    retStr += Left(ATMId & Space(3), 3)
    '' ''    retStr += Right(("   " & Currency), 3)
    '' ''    retStr += "  " 'response
    '' ''    retStr += Right(("00" & pHostRequestType), 2)


    '' ''    Return retStr
    '' ''End Function

    '' ''Private Function ContactHost(ByVal pReqType As String) As Integer
    '' ''    Dim rtrn As Integer
    '' ''    Dim mvHostRequest As String
    '' ''    Dim QTimeStamp As String
    '' ''    Dim FinalQdata As String
    '' ''    Dim q As Queues
    '' ''    Dim ts As Date
    '' ''    Dim dd As Long
    '' ''    Dim lHostReply As String = ""
    '' ''    Dim lHostResponse As String = "599"

    '' ''    Try


    '' ''        q = New Queues
    '' ''        mvHostRequest = lGetHostFormatedRequest(pReqType)
    '' ''        'clear request Q for this machine
    '' ''        rtrn = q.ClearQ(CONFIGClass.RequestQueueBasicPath & ATMId, ATMId)
    '' ''        'clear replies q
    '' ''        rtrn = q.ClearQ(CONFIGClass.ReplyQueueBasicPath & ATMId, ATMId)
    '' ''        'create replies q if not there
    '' ''        rtrn = q.CreateMessage(CONFIGClass.ReplyQueueBasicPath & ATMId, ATMId)

    '' ''        If rtrn <> 0 Then

    '' ''            Return cnst_ErrCode_ErrCreatingReplyQ
    '' ''        End If
    '' ''        QTimeStamp = (Now.ToString("dd MMM yyyy HH:mm:ss") & Space(30)).Substring(0, 30)
    '' ''        FinalQdata = QTimeStamp & CONFIGClass.WaitingReplyTimeout.ToString("0000000000") & mvHostRequest
    '' ''        rtrn = q.SendMessage(CONFIGClass.RequestQueueBasicPath & ATMId, FinalQdata, ATMId)
    '' ''        'start host client application
    '' ''        startHostclientApplication(ATMId)

    '' ''        If rtrn <> 0 Then
    '' ''            Return cnst_ErrCode_ErrsendingRequestQ
    '' ''        End If
    '' ''        log.loglog("Dorequest wait for reply, Start Litening on Q[" & CONFIGClass.ReplyQueueBasicPath & ATMId & "]", False)
    '' ''        ts = Now
    '' ''        rtrn = q.ReceiveMessage(CONFIGClass.ReplyQueueBasicPath & ATMId, lHostReply, ATMId)
    '' ''        While rtrn = 2
    '' ''            dd = DateDiff("S", ts, Now)
    '' ''            If dd >= CONFIGClass.WaitingReplyTimeout Then

    '' ''                Return cnst_ErrCode_ErrHostReplyTimeOut
    '' ''            End If
    '' ''            rtrn = q.ReceiveMessage(CONFIGClass.ReplyQueueBasicPath & ATMId, lHostReply, ATMId)

    '' ''        End While
    '' ''        log.loglog("DoRequest Receive Reply rtrn=" & rtrn & " Replydata=[" & lHostReply & "]", ATMId, False)


    '' ''        If rtrn <> 0 Then
    '' ''            Return cnst_ErrCode_ErrReceiveError
    '' ''        Else
    '' ''            lHostResponse = "5" & Mid(lHostReply, 41, 2)
    '' ''        End If
    '' ''        log.loglog("DoRequest Receive Reply:, rtrn=" & rtrn & " Response=[" & lHostResponse & "] Replydata=[" & lHostReply & "]", ATMId, False)
    '' ''        Return Val(lHostResponse)

    '' ''    Catch ex As Exception
    '' ''        log.loglog("DoRequest Receive Reply exp:" & ex.ToString, ATMId, False)
    '' ''        Return cnst_ErrCode_ErrHostreceivingReply


    '' ''    End Try

    '' ''End Function

    Public Function BuildIsoRequest_CC(ByRef pReqStr As String) As Integer
        Dim magIndc As Boolean
        Dim dt, req, tan, mmdd As String
        Try


            dt = Format(Now, "MMddHHmmss")
            mmdd = Format(Now, "MMdd")

            tan = Format(Now, "HHmmss")
            req = ""
            req = CONFIGClass.ISOHeader ' "ISO005000000"
            req = req & CONFIGClass.ISORequestId  '"0200"
            magIndc = Not CheckChip(CCTrack2)
            If magIndc = True Then
                req = req & "B238C00120A18010" & "000000000" & "0" & "000000"
            Else
                req = req & "B238C40120A18010" & "000000000" & "0" & "000000"
            End If


            req = req & CONFIGClass.ProcessingCode ' "000000" '3
            'req = req & "000000001000" '4
            req = req & Val(Amount).ToString("0000000000") & "00" '4
            req = req & dt '7
            req = req & tan '11
            req = req & tan '12
            req = req & mmdd '13
            req = req & mmdd '17
            req = req & CONFIGClass.DataElement18 ' "5999" '18
            If magIndc = False Then ' add de 22 
                req = req & (CONFIGClass.DataElement22 & "000").Substring(0, 3)
            End If
            req = req & CONFIGClass.DataElement32.Length.ToString("00") & CONFIGClass.DataElement32 ' "11" & "81800627220" '32
            'req = req & "37" & "4946069111111203=14041211000065100000" '35

            req = req & CCTrack2.Trim.Length.ToString("00") & CCTrack2
            'req = req & CONFIGClass.DataElement41 ' "BM04320520      " ' 41
            req = req & (ATMId & "                ").Substring(0, 16) ' "BM04320520      " ' 41
            req = req & "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX".Substring(0, 40) '43
            req = req & "044" & "1" & Space(23) & "4" & New String("0", 19) '48
            req = req & "818" ' 49
            'req = req & "012" & "MISRMISR+000" '60
            req = req & "012" & "NBE NBE +000" '60
            'req = req & "18" & "010000002011771164" '102
            'req = req & DebitAccountNumber.Length.ToString("00") & DebitAccountNumber '102
            pReqStr = req
            Return 0

        Catch ex As Exception
            log.loglog("BuildIsoRequest,Exception :" * ex.ToString, False)
            pReqStr = ""
            Return 9
        End Try

    End Function

End Class
