Imports System.Data.SqlClient
Imports System.io
Imports System.Net.Mail



Module Main
    Public mvLogPath
    Public EODTime
    Public EODPath As String
    Public FilePath As String
    Public ConnStrConfig
    Public WithDrawalHostFlag
    Public DepositHostFlag
    Public DepositCode
    Public WithdrawalCode
    Public ConfirmationCode
    Public AuthorizationCode
    Public DebitPIN1
    Public DebitPIN2
    Public DebitPIN3
    Public DebitPIN4


    Public Function ConnectToDatabase(ByVal command As String, ByVal ConnectionString As String, ByRef DT As DataTable) As Boolean
        Dim ConnectionStr As String = ConnectionString  'ConnStrConfig
        Dim myConnection As SqlConnection
        Dim myCommand As SqlCommand
        Dim myAdapter As SqlDataAdapter
        Dim RowsAffected As Integer
        Try

            'Instantiate the Connection object
            myConnection = New SqlConnection(ConnectionStr)
            'Instantiate the Command object
            myCommand = New SqlCommand(command, myConnection)
            myConnection.Open()
            'Instantiate  DataSet object
            DT = New DataTable
            'Instantiate  DataAdapter object
            myAdapter = New SqlDataAdapter()
            'Set DataAdapter command properties
            myAdapter.SelectCommand = myCommand
            'Populate the Dataset
            myAdapter.Fill(DT)
            myConnection.Close()


            ConnectToDatabase = True
        Catch ex As Exception
            ConnectToDatabase = False
            loglog("Failed To Connect To DataBase  " & ex.ToString, True)
        End Try
        Return ConnectToDatabase

    End Function
    Public Function GetMoneyFerEODData(ByVal EODdatetime As String, ByRef DT As DataTable) As Boolean
        Dim QSTR As String
        Dim Flag As Boolean
        
        Try

        
            QSTR = "Select * from NBEHostUpdateView where ActionDateTime <= '" & EODdatetime & "' order by ActionDateTime"
            Flag = ConnectToDatabase(QSTR, ConnStrConfig, DT)
            If Flag = True Then
                loglog("Executing [" & QSTR & "] count = " & DT.Rows.Count & " For EODdate = " & EODdatetime, True)
                loglog("MoneyFerEODData count = " & DT.Rows.Count & " For EODdate = " & EODdatetime, True)
                GetMoneyFerEODData = True
            Else
                GetMoneyFerEODData = False
            End If




        Catch ex As Exception
            GetMoneyFerEODData = False
            loglog("GetMoneyFerEODData Error ex = " & ex.Message, True)
        End Try
    End Function

    Public Function SplitDT(ByVal MainDT As DataTable, ByRef CardlessRows As DataRow(), ByRef DebitRows As DataRow(), ByRef CreditRows As DataRow()) As Boolean
        Dim DebitDT As DataTable
        Dim CreditDT As DataTable
        Dim CardlessDT As DataTable
        'Dim CardlessRows As DataRow()
        'Dim DebitRows As DataRow()
        'Dim CreditRows As DataRow()
        Dim i As Integer
        Dim DebRef As String
        Dim PayType As String
        Dim CCTrackTemp As String
        Dim NewEnc As NewCrypto
        Dim Temp As String
        Dim CCTrack2 As String
        Try

            For i = 0 To MainDT.Rows.Count - 1
                PayType = MainDT.Rows(i).Item("PaymentType")
                If PayType = "DEBITACCOUNT" Then
                    DebRef = MainDT.Rows(i).Item("DepositorID")
                    If DebRef.Length > 192 Then
                        CCTrackTemp = DebRef.Substring(0, 192).Trim()

                        NewEnc = New NewCrypto
                        NewEnc.Key = "msiDMWNyrGULLJQh"
                        NewEnc.IV = "BS0P1FvF"
                        Temp = NewEnc.Decrypt(CCTrackTemp)
                        If Temp.Length > 0 Then
                            CCTrack2 = Temp
                            CCTrackTemp = DebRef.Substring(192)
                            CCTrackTemp = (CCTrack2 & Space(40)).Substring(0, 40) & CCTrackTemp
                            MainDT.Rows(i).Item("DepositorID") = CCTrackTemp
                        Else
                            CCTrack2 = DebRef
                            'Return cnst_ErrCode_DecryptionError
                            loglog("Error While Decrypting  = " & DebRef, True)
                            MainDT.Rows(i).Delete()

                        End If
                    End If
                End If


            Next
            DebitDT = MainDT.Clone()
            CreditDT = MainDT.Clone()
            CardlessDT = MainDT.Clone()

            CardlessRows = MainDT.Select("PaymentType = 'CASH'")
            DebitRows = MainDT.Select("PaymentType = 'DEBITACCOUNT' and DepositorID like '" & DebitPIN1 & "%' or DepositorID like '" & DebitPIN2 & "%' or DepositorID like '" & DebitPIN3 & "%' or DepositorID like '" & DebitPIN4 & "%'")
            CreditRows = MainDT.Select("PaymentType = 'DEBITACCOUNT' and DepositorID not like '" & DebitPIN1 & "%' and DepositorID not like '" & DebitPIN2 & "%' and DepositorID not like '" & DebitPIN3 & "%' and DepositorID not like '" & DebitPIN4 & "%'")

            SplitDT = True

        Catch ex As Exception
            SplitDT = False
            loglog("Error while splitting the main datatable :" & ex.ToString(), True)
        End Try


    End Function
    Public Function CreateEODDateTime(ByVal Time As String, ByRef EODdatetime As String) As Boolean
        Try
            'EODdatetime = Now.ToString("dd/MM/yyyy ") & Time
            EODdatetime = CStr(Format(CDate(Now.Year & "-" & Now.Month & "-" & Now.Day & " " & Time), "yyyy-MM-dd HH:mm:ss"))
            CreateEODDateTime = True
        Catch ex As Exception

            CreateEODDateTime = False
            loglog("CreateEODDateTime Error ex = " & ex.Message, True)
        End Try
    End Function
    Public Function UpdateEODData(ByVal Rows As DataRow()) As Boolean
        Dim i As Integer
        Dim QSTR As String
        Dim Flag As Boolean
        Dim DTF As DataTable
        Dim myConnection As SqlConnection
        Dim myCommand As SqlCommand
        Dim RowsAffected As String

        Try
            myConnection = New SqlConnection(ConnStrConfig)
            myConnection.Open()
            myCommand = New SqlCommand
            myCommand.Connection = myConnection
            myCommand.Transaction = myConnection.BeginTransaction
            For i = 0 To Rows.Length - 1
                'If Rows(i).Item("Action") = "11" Then
                '    'QSTR = " update transactions set DepositHostFlag=" & DepositHostFlag & ", HostDResponse='00',HostDUpdateTime=getdate() where transactioncode='" & Rows(i).Item("TransactionCode") & "'"
                'ElseIf Rows(i).Item("Action") = "11" Or Rows(i).Item("Action") = "12" Or Rows(i).Item("Action") = "17" Or Rows(i).Item("Action") = "02" Or Rows(i).Item("Action") = "07" Then
                '    QSTR = " update transactions set DepositHostFlag=" & DepositHostFlag & ",WithDrawalHostFlag= WithDrawalHostFlag + 1 , HostDResponse='00',HostDUpdateTime=getdate() where transactioncode='" & Rows(i).Item("TransactionCode") & "'"
                'End If

                If Rows(i).Item("Action") = "11" Then
                    QSTR = " update transactions set DepositHostFlag=" & DepositHostFlag & ", HostDResponse='00',HostDUpdateTime=getdate() where transactioncode='" & Rows(i).Item("TransactionCode") & "'"
                ElseIf Rows(i).Item("Action") = "12" Or Rows(i).Item("Action") = "17" Or Rows(i).Item("Action") = "02" Or Rows(i).Item("Action") = "07" Then
                    QSTR = " update transactions set DepositHostFlag=" & DepositHostFlag & ",WithDrawalHostFlag= WithDrawalHostFlag | 1 , HostDResponse='00',HostDUpdateTime=getdate() where transactioncode='" & Rows(i).Item("TransactionCode") & "'"
                End If

                loglog("Updating Transaction [" & Rows(i).Item("TransactionCode") & "] WithdrawalHostFlag was ", True)
                myCommand.CommandText = QSTR
                RowsAffected = myCommand.ExecuteNonQuery()

                If RowsAffected < 1 Then
                    myCommand.Transaction.Rollback()
                    UpdateEODData = False
                    loglog("UpdateEODData Error in line : " & i & "With TransactionCode [" & Rows(i).Item("TransactionCode") & "]", True)
                    Exit Function
                End If
            Next
            myCommand.Transaction.Commit()
            myConnection.Close()

            UpdateEODData = True
        Catch ex As Exception
            myCommand.Transaction.Rollback()
            myConnection.Close()
            UpdateEODData = False
            loglog("UpdateEODData Error ex = " & ex.ToString() & i, True)
        End Try
    End Function
    Public Function WriteEODFile(ByVal Rows As DataRow(), ByVal Type As String) As Boolean
        Dim i As Integer
        Dim Transactioncode, ATMID, Depositormobile, Beneficiarymobile As String
        Dim amount, ActionDate, ActionTime, ActionStatus, CommissionAmount, Action As String
        Dim Isteller, AtmTrxsequence, DispensedNotes As String
        Dim ExtraInformation As String = ""
        Dim StrAll As String
        Dim MyCont As String
        Dim StrWrite As String
        Dim SR As IO.StreamWriter
       
        Try

            If Rows.Length >= 1 Then
                For i = 0 To Rows.Length - 1
                    StrAll = ""
                    'loglog("1", True)
                    If Rows(i).Item("Transactioncode") = "" Then
                        Transactioncode = "000000000000"
                    Else
                        Transactioncode = Rows(i).Item("Transactioncode")
                    End If
                    'loglog("2", True)
                    Transactioncode = Format(CLng(Transactioncode), "000000000000")
                    Transactioncode = Transactioncode.Substring(0, 12)
                    'loglog("3", True)
                    ATMID = Rows(i).Item("ATMId") & "00000000"
                    ATMID = ATMID.Substring(0, 8)
                    'loglog("4", True)
                    Depositormobile = Rows(i).Item("Depositormobile") & "000000000000000"
                    Depositormobile = Depositormobile.Substring(0, 15)
                    'loglog("5", True)
                    Beneficiarymobile = Rows(i).Item("Beneficiarymobile") & "000000000000000"
                    Beneficiarymobile = Beneficiarymobile.Substring(0, 15)
                    'loglog("6", True)
                    If Rows(i).Item("amount") Is DBNull.Value Then
                        amount = "0"
                    Else
                        amount = Rows(i).Item("amount")
                    End If
                    'loglog("7", True)
                    'amount = Format(CLng(amount), "000000") & "00"
                    'amount = amount.Substring(0, 8)




                    ActionDate = Rows(i).Item("ActionDate")
                    ActionTime = Replace(Rows(i).Item("ActionTime"), ":", "")
                    'loglog("8", True)
                    'ActionStatus = DT.Rows(i).Item("ActionStatus")
                    If Rows(i).Item("CommissionAmount") Is DBNull.Value Then
                        CommissionAmount = "0"
                    Else
                        CommissionAmount = Rows(i).Item("CommissionAmount")
                    End If
                    'loglog("9", True)

                    'If DT.Rows(i).Item("Action") = "11" Or DT.Rows(i).Item("Action") = "01" Then
                    '    'Action = "DEPOSIT" & "          "
                    '    Action = DepositCode & "0000000000"
                    '    Action = Action.Substring(0, 10)
                    'ElseIf DT.Rows(i).Item("Action") = "12" Or DT.Rows(i).Item("Action") = "17" Or DT.Rows(i).Item("Action") = "07" Or DT.Rows(i).Item("Action") = "02" Then
                    '    'Action = "WITHDRAWAL" & "          "
                    '    Action = WithdrawalCode & "0000000000"
                    '    Action = Action.Substring(0, 10)
                    'End If

                    'If DT.Rows(i).Item("ActionStatus") = "AUTHORIZED" Then
                    '    ActionStatus = AuthorizationCode & "0000000000"
                    '    ActionStatus = ActionStatus.Substring(0, 10)
                    'ElseIf DT.Rows(i).Item("ActionStatus") = "CONFIRMED" Then
                    '    ActionStatus = ConfirmationCode & "0000000000"
                    '    ActionStatus = ActionStatus.Substring(0, 10)
                    'End If

                    Action = Rows(i).Item("Action") & "00000"
                    'If (Action = "1200000" Or Action = "1700000" Or Action = "1100000") Then
                    amount = CInt(amount) - CInt(CommissionAmount)
                    'amount = Format(CLng(amount), "000000") & "00"
                    'amount = amount.Substring(0, 8)
                    'End If
                    'loglog("10", True)
                    amount = Format(CLng(amount), "000000") & "00"
                    amount = amount.Substring(0, 8)
                    'loglog("11", True)
                    Action = Action.Substring(0, 5)
                    CommissionAmount = Format(CLng(CommissionAmount), "000000").ToString & "00"
                    CommissionAmount = CommissionAmount.Substring(0, 8)
                    'loglog("12", True)

                    If Rows(i).Item("Isteller") Is DBNull.Value Then
                        Isteller = "000000" & "000000"
                        Isteller = Isteller.Substring(0, 6)
                    ElseIf Rows(i).Item("Isteller") = "1" Then
                        Isteller = "TELLER" & "000000"
                        Isteller = Isteller.Substring(0, 6)
                    Else
                        Isteller = "ATM" & "000000"
                        Isteller = Isteller.Substring(0, 6)
                    End If
                    'loglog("13", True)
                    If Rows(i).Item("AtmTrxsequence") Is DBNull.Value Then
                        AtmTrxsequence = "0000000000"
                    ElseIf AtmTrxsequence = "" Then
                        AtmTrxsequence = "0000000000"
                    Else
                        AtmTrxsequence = Rows(i).Item("AtmTrxsequence")
                    End If
                    'loglog("14", True)
                    AtmTrxsequence = Format(CLng(AtmTrxsequence), "0000000000").ToString
                    If Rows(i).Item("DispensedNotes") Is DBNull.Value Then
                        DispensedNotes = "0"
                    Else
                        DispensedNotes = Rows(i).Item("DispensedNotes")
                        If DispensedNotes.Trim = "" Then
                            DispensedNotes = "0"
                        Else
                            DispensedNotes = Rows(i).Item("DispensedNotes")
                        End If
                    End If
                    'loglog("15", True)

                    DispensedNotes = Format(CLng(DispensedNotes), "00000000").ToString

                    If Rows(i).Item("Action") Is DBNull.Value Then
                        DispensedNotes = "00000000"
                    ElseIf Rows(i).Item("Action") = "11" Or Rows(i).Item("Action") = "01" Then
                        DispensedNotes = "00000000"
                    End If
                    'loglog("16", True)
                    If (Type = "Debit") Then
                        'ExtraInformation = Rows(i).Item("DepositorID").ToString().Substring(Rows(i).Item("DepositorID").ToString().LastIndexOf("-") + 1)
                        ExtraInformation = Rows(i).Item("DepositorID").ToString().Substring(40, 19)
                        FilePath = EODPath & "CashRemittanceEOD_Debit" & Now.Day & Now.Month & Now.Year & Now.Hour & Now.Minute & ".txt"
                    ElseIf (Type = "Credit") Then
                        
                        ExtraInformation = Rows(i).Item("DepositorID").ToString().Substring(0, 16) & Rows(i).Item("DepositorID").ToString().Substring(60, 10).Trim() & Rows(i).Item("MerchantID").ToString()
                        FilePath = EODPath & "CashRemittanceEOD_Credit" & Now.Day & Now.Month & Now.Year & Now.Hour & Now.Minute & ".txt"
                    ElseIf (Type = "Cardless") Then
                        FilePath = EODPath & "CashRemittanceEOD_Cardless" & Now.Day & Now.Month & Now.Year & Now.Hour & Now.Minute & ".txt"
                    End If
                    'loglog("17", True)
                    StrAll = Transactioncode & ATMID & Depositormobile & Beneficiarymobile
                    StrAll = StrAll & amount & ActionDate & ActionTime '& ActionStatus
                    StrAll = StrAll & CommissionAmount & Action & Isteller & AtmTrxsequence & DispensedNotes & ExtraInformation & vbNewLine

                    StrWrite = StrWrite & StrAll
                Next
                If Right(EODPath, 1) <> "\" Then
                    EODPath = EODPath & "\"
                End If

                SR = New StreamWriter(FilePath)
                SR.Write(StrWrite)
                SR.Close()
                WriteEODFile = True
            Else
                loglog("WriteEODFile ERROR: No Record Found", True)

                If (Type = "Debit") Then
                    'ExtraInformation = Rows(i).Item("DepositorID").ToString().Substring(Rows(i).Item("DepositorID").ToString().LastIndexOf("-") + 1)
                    MyCont = "00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000"
                    FilePath = EODPath & "CashRemittanceEOD_Debit" & Now.Day & Now.Month & Now.Year & Now.Hour & Now.Minute & ".txt"
                ElseIf (Type = "Credit") Then
                    MyCont = "00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000"
                    FilePath = EODPath & "CashRemittanceEOD_Credit" & Now.Day & Now.Month & Now.Year & Now.Hour & Now.Minute & ".txt"
                ElseIf (Type = "Cardless") Then
                    MyCont = "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000"
                    FilePath = EODPath & "CashRemittanceEOD_Cardless" & Now.Day & Now.Month & Now.Year & Now.Hour & Now.Minute & ".txt"
                End If

                'loglog("WriteEODFile ERROR: No Record Found ", True)
                If Right(EODPath, 1) <> "\" Then
                    EODPath = EODPath & "\"
                End If

                SR = New StreamWriter(FilePath)
                SR.Write(MyCont)
                SR.Close()
                WriteEODFile = True
            End If
        Catch ex As Exception
            loglog("WriteEODFile Error for type [" & Type & "] ex = " & ex.ToString() & i, True)
            WriteEODFile = False
        End Try
    End Function

    Public Sub loglog(ByVal LogData As String, ByVal sepLine As Boolean)
        Dim fname As String
        Dim logdir As String
        Dim fulFilename As String
        Dim dinfo As DirectoryInfo
        Dim finfo As FileInfo
        Dim strmWrtr As StreamWriter
        'Dim cd As String
        Dim speratorLine As String = "====================================================="

        fname = "CashRemittanceEOD" & Now.Day & "-" & Now.Month & "-" & Now.Year & ".log"
        logdir = mvLogPath


        Try
            If System.IO.Directory.Exists(logdir) = False Then
                dinfo = System.IO.Directory.CreateDirectory(logdir)
            Else
                dinfo = New DirectoryInfo(logdir)
            End If

        Catch ex As Exception
            Try
                logdir = System.Environment.GetEnvironmentVariable("tmp") & "\CashRemittanceEOD_log"
                dinfo = Directory.CreateDirectory(logdir)
                loge("Can Not Create Log:" & logdir & " ex:" & ex.Message, EventLogEntryType.Error)

            Catch exx As Exception

                loge("Can Not Create Log:" & logdir & " ex:" & exx.Message, EventLogEntryType.Error)
                loge(LogData, EventLogEntryType.Information)
                Return
            End Try
        End Try


        Try
            fulFilename = dinfo.FullName & "\" & fname
            finfo = New FileInfo(fulFilename)

            If finfo.Exists() = False Then
                strmWrtr = finfo.CreateText()
            Else
                If finfo.Length < 10000000 Then
                    strmWrtr = finfo.AppendText()
                Else
                    strmWrtr = finfo.CreateText()
                End If
            End If
            If sepLine = True Then
                strmWrtr.WriteLine(speratorLine)
                strmWrtr.WriteLine(Now.ToString())
            End If

            strmWrtr.WriteLine(LogData)
            strmWrtr.Flush()
            strmWrtr.Close()
            strmWrtr = Nothing
            finfo = Nothing
            dinfo = Nothing
        Catch ex As Exception

            Return
        End Try

    End Sub
    Public Sub loge(ByVal Data As String, ByVal eventType As System.Diagnostics.EventLogEntryType)
        Try
            If Not System.Diagnostics.EventLog.SourceExists("CashRemittanceEOD") Then
                System.Diagnostics.EventLog.CreateEventSource("CashRemittanceEOD", "Application")
            End If
            Dim ev As New System.Diagnostics.EventLog
            ev.Source = "CashRemittanceEOD"
            ev.WriteEntry(Data, eventType)

        Catch ex As Exception
        End Try
    End Sub
    Public Sub ReadConfig()
        Dim XmlD As Xml.XmlDocument
        Dim items As Xml.XmlNodeList
        Dim item As Xml.XmlNode
        Dim str As String
        Dim TmpItem As Xml.XmlNode
        Dim ConfgFile As String

        Try
            XmlD = New Xml.XmlDocument


            ConfgFile = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) & "\config.xml"
            XmlD.Load(ConfgFile)

        Catch ex As Exception
            loge("Can Not Load Config File", EventLogEntryType.Error)

            Exit Sub

        End Try
        Try
            items = Nothing
            item = Nothing
            items = XmlD.GetElementsByTagName("CashRemittanceEODConfig")

            item = items(0)
            For Each TmpItem In item.ChildNodes
                If TmpItem.Name.ToUpper = "LogPath".ToUpper Then
                    mvLogPath = TmpItem.InnerText

                End If
                If TmpItem.Name.ToUpper = "Time".ToUpper Then
                    EODTime = TmpItem.InnerText

                End If
                If TmpItem.Name.ToUpper = "EODPath".ToUpper Then
                    EODPath = TmpItem.InnerText

                End If
                If TmpItem.Name.ToUpper = "ConnectionString".ToUpper Then
                    ConnStrConfig = TmpItem.InnerText
                End If
                If TmpItem.Name.ToUpper = "WithDrawalHostFlag".ToUpper Then
                    WithDrawalHostFlag = TmpItem.InnerText
                End If
                If TmpItem.Name.ToUpper = "DepositHostFlag".ToUpper Then
                    DepositHostFlag = TmpItem.InnerText
                End If


                Try
                    If TmpItem.Name.ToUpper = "DepositCode".ToUpper Then
                        DepositCode = TmpItem.InnerText
                    End If
                Catch ex As Exception
                    loglog("Can not read Deposit code, it will be 'DEPOSIT'", True)
                    DepositCode = "DEPOSIT"
                End Try
                
                Try
                    If TmpItem.Name.ToUpper = "WithdrawalCode".ToUpper Then
                        WithdrawalCode = TmpItem.InnerText
                    End If
                Catch ex As Exception
                    loglog("Can not read Withdrawal code, it will be 'WITHDRAWAL'", True)
                    WithdrawalCode = "WITHDRAWAL"
                End Try


                Try
                    If TmpItem.Name.ToUpper = "ConfirmationCode".ToUpper Then
                        ConfirmationCode = TmpItem.InnerText
                    End If
                Catch ex As Exception
                    loglog("Can not read Confirmation code, it will be 'CONFIRMED'", True)
                    ConfirmationCode = "CONFIRMED"
                End Try


                Try
                    If TmpItem.Name.ToUpper = "AuthorizationCode".ToUpper Then
                        AuthorizationCode = TmpItem.InnerText
                    End If
                Catch ex As Exception
                    loglog("Can not read Confirmation code, it will be 'Authorized'", True)
                    AuthorizationCode = "Authorized"
                End Try

                Try
                    If TmpItem.Name.ToUpper = "DebitPIN1".ToUpper Then
                        DebitPIN1 = TmpItem.InnerText
                    End If
                Catch ex As Exception
                    loglog("Can not read DebitPIN1, it will be 'XXXXXX'", True)
                    DebitPIN1 = "XXXXXX"
                End Try

                Try
                    If TmpItem.Name.ToUpper = "DebitPIN2".ToUpper Then
                        DebitPIN2 = TmpItem.InnerText
                    End If
                Catch ex As Exception
                    loglog("Can not read DebitPIN2, it will be 'XXXXXX'", True)
                    DebitPIN2 = "XXXXXX"
                End Try
                Try
                    If TmpItem.Name.ToUpper = "DebitPIN3".ToUpper Then
                        DebitPIN3 = TmpItem.InnerText
                    End If
                Catch ex As Exception
                    loglog("Can not read DebitPIN3, it will be 'XXXXXX'", True)
                    DebitPIN3 = "XXXXXX"
                End Try
                Try
                    If TmpItem.Name.ToUpper = "DebitPIN4".ToUpper Then
                        DebitPIN4 = TmpItem.InnerText
                    End If
                Catch ex As Exception
                    loglog("Can not read DebitPIN4, it will be 'XXXXXX'", True)
                    DebitPIN4 = "XXXXXX"
                End Try

            Next

        Catch ex As Exception
            loge("Error Reading CashRemittanceEOD Setting ex:" & ex.Message, EventLogEntryType.Error)
        End Try

    End Sub
End Module
