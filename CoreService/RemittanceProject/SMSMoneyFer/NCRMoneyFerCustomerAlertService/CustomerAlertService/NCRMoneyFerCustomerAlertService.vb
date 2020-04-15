
Imports System.Threading
Imports System.io
Imports System.text

Public Class NCRMoneyFerCustomerAlertService


    Public Shared ClientCheckPointInterval As Integer = 5


    Public Shared ServiceId As String = "CustAlert"
    Public Shared BankArabicName As String = ""
    Public Shared BankName As String = ""
    Public Shared EnableSendingKey2 As Integer = 1
    Public Shared SMSDatabaseServerName As String = "."
    Public Shared SMSDatabaseName As String = "SMSMoneyFer"
    Public Shared SMSDatabaseUserid As String = "SMSUser"
    Public Shared SMSDatabasePassword As String = "SMSUser"
    Public Shared SMSDataBaseIntegratedSecurity As String = "False"
    Public Shared SMSDatabaseConnectionString As String = ""

    Public Shared BeneficiaryDepositSMSBody As String = ""
    Public Shared DepositorDepositSMSBody As String = ""
    Public Shared BeneficiaryWithdrawalSMSBody As String = ""
    Public Shared DepositorWithdrawalSMSBody As String = ""
    Public Shared DepositorTrxExpirationSMSBody_D As String = ""
    Public Shared DepositorTrxExpirationSMSBody_B As String = ""
    Public Shared DepositorRedemptionSMSBody As String = ""

    Public Shared ARBeneficiaryDepositSMSBody As String = ""
    Public Shared ARDepositorDepositSMSBody As String = ""
    Public Shared ARBeneficiaryWithdrawalSMSBody As String = ""
    Public Shared ARDepositorWithdrawalSMSBody As String = ""
    Public Shared ARDepositorTrxExpirationSMSBody_D As String = ""
    Public Shared ARDepositorTrxExpirationSMSBody_B As String = ""
    Public Shared ARDepositorRedemptionSMSBody As String = ""

    Public Shared SMSWebservice_URL As String = "http://172.16.21.83/SMS/SMSService.asmx"
    Public Shared SMSWebservice_UserId As String = ""
    Public Shared SMSWebservice_Password As String = ""
    Public Shared SMSWebservice_Channel = "XCS"
    Public Shared SMSWebservice_Mode = "Purchase"
    Public Shared SMSWebMobilePrefix = "+2"

    Public Shared SMSAlertingService As Integer = 1
    Public Shared SMSModemPortNumber As Integer

    Public Shared SMSServiceProvider As String = "unitedbank"
    Public Shared SMSServiceBasicURL As String = ""
    Public Shared SMSUBUser As String = "admin"
    Public Shared SMSUBPWD As String = "admin"
    Public Shared SMSUBPriority As String = "10"
    Public Shared SMSServiceSenderMobile As String = "UB"
    Public Shared SMSSendingTimeOut As Integer = 30 'seconds
    Public Shared LogLevel As Integer = 0
    Public Shared StopFlag As Boolean


    Public Shared SMSServiceDatabaseServerName As String
    Public Shared SMSServiceDatabaseName As String
    Public Shared SMSServiceDatabaseUserid As String
    Public Shared SMSServiceDatabasePassword As String
    Public Shared SMSServiceDataBaseIntegratedSecurity As String
    Public Shared SMSServiceDatabaseConnectionString As String = ""

    Public Shared HTTPIP As String
    Public Shared HTTPPort As String
    Public Shared HTTPSender As String
    Public Shared HTTPUsername As String
    Public Shared HTTPPassword As String

    Public Shared SMSHTTPUNIBank_URL As String
    Public Shared SMSHTTPUNIBank_From As String
    Public Shared SMSHTTPUNIBank_ClientReference As String
    Public Shared SMSHTTPUNIBank_ClientID As String
    Public Shared SMSHTTPUNIBank_ClientSecret As String
    Public Shared SMSHTTPUNIBank_RegisteredDelivery As String

    Public Shared SMSUserName As String
    Public Shared SMSPassword As String
    Public Shared URL As String
    Public Shared KEY As String
    Public Shared SenderName As String

    Private chktrxc As CheckTransactionsClass




    Public Sub MyStart()
        Dim newThreadStart As ThreadStart
        Dim newThread As Thread



        readConfig()
        StopFlag = False
        Try


            chktrxc = New CheckTransactionsClass
            newThreadStart = New ThreadStart(AddressOf chktrxc.Process)
            newThread = New Thread(newThreadStart)
            newThread.SetApartmentState(ApartmentState.STA)
            newThread.Start()
        Catch ex As Exception
            loglog("Error Starting Thread :" & ex.ToString, False)
        End Try

    End Sub

    Protected Overrides Sub OnStart(ByVal args() As String)

        MyStart()

    End Sub

    Protected Overrides Sub OnStop()
        StopFlag = True
    End Sub
    Private Sub readConfig()
        Dim xmlD As Xml.XmlDocument
        Dim items As Xml.XmlNodeList
        Dim item As Xml.XmlNode
        Dim tstr As String
        Dim tmpItem As Xml.XmlNode
        Dim cfgFile As String
        ' Dim xx As Integer
        Dim tmp As String
        Dim shortnameStart As Integer
        Dim prevIndex As Integer

        Try
            xmlD = New Xml.XmlDocument

            tmp = System.Reflection.Assembly.GetExecutingAssembly.Location
            shortnameStart = 0
            prevIndex = 0
            While shortnameStart <> -1
                prevIndex = shortnameStart
                shortnameStart = tmp.IndexOf("\", shortnameStart + 1)
            End While

            cfgFile = tmp.Substring(0, prevIndex + 1) & "config.xml"

            xmlD.Load(cfgFile)
        Catch ex As Exception
            loglog("Can Not Load Config.xml file ex:" & ex.Message, True)
            Return
        End Try

        Try
            items = Nothing
            item = Nothing
            items = xmlD.GetElementsByTagName("CustomerAlert")
            item = items(0)
            If Not item Is Nothing Then
                For Each tmpItem In item.ChildNodes

                    If tmpItem.Name.ToUpper() = "ClientCheckPointInterval".ToUpper() Then
                        tstr = tmpItem.InnerText
                        Try
                            ClientCheckPointInterval = CInt(tstr)
                        Catch ex As Exception
                            ClientCheckPointInterval = 5 'Minutes
                        End Try
                    End If
                    If tmpItem.Name.ToUpper() = "ServiceId".ToUpper() Then
                        ServiceId = tmpItem.InnerText
                    End If
                    If tmpItem.Name.ToUpper() = "BankArabicName".ToUpper() Then
                        BankArabicName = tmpItem.InnerText
                    End If
                    If tmpItem.Name.ToUpper() = "BankName".ToUpper() Then
                        BankName = tmpItem.InnerText
                    End If
                    If tmpItem.Name.ToUpper() = "LogLevel".ToUpper() Then
                        tstr = tmpItem.InnerText
                        Try
                            LogLevel = CInt(tstr)
                        Catch ex As Exception
                            LogLevel = 0
                        End Try
                    End If

                    If tmpItem.Name.ToUpper() = "EnableSendingKey2".ToUpper() Then
                        tstr = tmpItem.InnerText
                        Try
                            EnableSendingKey2 = CInt(tstr)
                        Catch ex As Exception
                            EnableSendingKey2 = 1 'default is to send
                        End Try
                    End If
                    If tmpItem.Name.ToUpper() = "SMSAlertingService".ToUpper() Then
                        tstr = tmpItem.InnerText
                        Try
                            SMSAlertingService = CInt(tstr)
                        Catch ex As Exception
                            SMSAlertingService = 1
                        End Try
                    End If
                Next
            End If
        Catch ex As Exception
            loglog("Error Reading [CustomerAlert] settings ex:" & ex.Message, True)
        End Try

        Try
            items = Nothing
            item = Nothing
            items = xmlD.GetElementsByTagName("SMSModem")
            item = items(0)
            If Not item Is Nothing Then
                For Each tmpItem In item.ChildNodes
                    If tmpItem.Name.ToUpper() = "SMSModemPortNumber".ToUpper() Then
                        tstr = tmpItem.InnerText
                        Try
                            SMSModemPortNumber = CInt(tstr)
                        Catch ex As Exception
                            SMSModemPortNumber = 1
                        End Try
                    End If


                Next
            End If
        Catch ex As Exception
            loglog("Error Reading [SMSModem] settings ex:" & ex.Message, True)
        End Try


        Try
            items = Nothing
            item = Nothing
            items = xmlD.GetElementsByTagName("SMSService")
            item = items(0)

            If Not item Is Nothing Then
                For Each tmpItem In item.ChildNodes

                    If tmpItem.Name.ToUpper() = "BasicURL".ToUpper() Then
                        SMSServiceBasicURL = tmpItem.InnerText

                    End If
                    If tmpItem.Name.ToUpper() = "Provider".ToUpper() Then
                        SMSServiceProvider = tmpItem.InnerText

                    End If
                    If tmpItem.Name.ToUpper() = "SenderMobile".ToUpper() Then
                        SMSServiceSenderMobile = tmpItem.InnerText

                    End If
                    If tmpItem.Name.ToUpper() = "User".ToUpper() Then
                        SMSUBUser = tmpItem.InnerText

                    End If
                    If tmpItem.Name.ToUpper() = "Password".ToUpper() Then
                        SMSUBPWD = tmpItem.InnerText

                    End If
                    If tmpItem.Name.ToUpper() = "Priority".ToUpper() Then
                        SMSUBPriority = tmpItem.InnerText

                    End If
                    If tmpItem.Name.ToUpper() = "SendingTimeOut".ToUpper() Then
                        tstr = tmpItem.InnerText
                        Try
                            SMSSendingTimeOut = CInt(tstr)
                        Catch ex As Exception
                            SMSSendingTimeOut = 30 'seconds
                        End Try

                    End If


                    If tmpItem.Name.ToUpper() = "BeneficiaryDepositSMSBody".ToUpper() Then
                        BeneficiaryDepositSMSBody = tmpItem.InnerText

                    End If
                    If tmpItem.Name.ToUpper() = "BeneficiaryWithdrawalSMSBody".ToUpper() Then
                        BeneficiaryWithdrawalSMSBody = tmpItem.InnerText
                    End If
                    If tmpItem.Name.ToUpper() = "DepositorDepositSMSBody".ToUpper() Then
                        DepositorDepositSMSBody = tmpItem.InnerText
                    End If
                    If tmpItem.Name.ToUpper() = "DepositorWithdrawalSMSBody".ToUpper() Then
                        DepositorWithdrawalSMSBody = tmpItem.InnerText
                    End If
                    If tmpItem.Name.ToUpper() = "DepositorTrxExpirationSMSBody_D".ToUpper() Then
                        DepositorTrxExpirationSMSBody_D = tmpItem.InnerText
                    End If
                    If tmpItem.Name.ToUpper() = "DepositorTrxExpirationSMSBody_B".ToUpper() Then
                        DepositorTrxExpirationSMSBody_B = tmpItem.InnerText
                    End If
                    If tmpItem.Name.ToUpper() = "DepositorRedemptionSMSBody".ToUpper() Then
                        DepositorRedemptionSMSBody = tmpItem.InnerText
                    End If


                    If tmpItem.Name.ToUpper() = "ARBeneficiaryDepositSMSBody".ToUpper() Then
                        ARBeneficiaryDepositSMSBody = tmpItem.InnerText

                    End If
                    If tmpItem.Name.ToUpper() = "ARBeneficiaryWithdrawalSMSBody".ToUpper() Then
                        ARBeneficiaryWithdrawalSMSBody = tmpItem.InnerText
                    End If
                    If tmpItem.Name.ToUpper() = "ARDepositorDepositSMSBody".ToUpper() Then
                        ARDepositorDepositSMSBody = tmpItem.InnerText
                    End If
                    If tmpItem.Name.ToUpper() = "ARDepositorWithdrawalSMSBody".ToUpper() Then
                        ARDepositorWithdrawalSMSBody = tmpItem.InnerText
                    End If
                    If tmpItem.Name.ToUpper() = "ARDepositorTrxExpirationSMSBody_D".ToUpper() Then
                        ARDepositorTrxExpirationSMSBody_D = tmpItem.InnerText
                    End If
                    If tmpItem.Name.ToUpper() = "ARDepositorTrxExpirationSMSBody_B".ToUpper() Then
                        ARDepositorTrxExpirationSMSBody_B = tmpItem.InnerText
                    End If
                    If tmpItem.Name.ToUpper() = "ARDepositorRedemptionSMSBody".ToUpper() Then
                        ARDepositorRedemptionSMSBody = tmpItem.InnerText
                    End If



                Next
            End If
        Catch ex As Exception
            loglog("Error Reading [SMSService] settings ex:" & ex.Message, True)
        End Try



        Try
            items = Nothing
            item = Nothing
            items = xmlD.GetElementsByTagName("SMSWebService")
            item = items(0)

            If Not item Is Nothing Then
                For Each tmpItem In item.ChildNodes

                    If tmpItem.Name.ToUpper() = "URL".ToUpper() Then
                        SMSWebservice_URL = tmpItem.InnerText

                    End If
                    If tmpItem.Name.ToUpper() = "UserId".ToUpper() Then
                        SMSWebservice_UserId = tmpItem.InnerText

                    End If
                    If tmpItem.Name.ToUpper() = "Password".ToUpper() Then
                        SMSWebservice_Password = tmpItem.InnerText
                    End If

                    If tmpItem.Name.ToUpper() = "Channel".ToUpper() Then
                        SMSWebservice_Channel = tmpItem.InnerText
                    End If

                    If tmpItem.Name.ToUpper() = "Mode".ToUpper() Then
                        SMSWebservice_Mode = tmpItem.InnerText
                    End If
                    If tmpItem.Name.ToUpper() = "MobilePrefix".ToUpper() Then
                        SMSWebMobilePrefix = tmpItem.InnerText
                    End If


                Next
            End If
        Catch ex As Exception
            loglog("Error Reading [SMSWebService] settings ex:" & ex.Message, True)
        End Try




        Try
            items = Nothing
            item = Nothing
            items = xmlD.GetElementsByTagName("SMSData")
            item = items(0)
            If Not item Is Nothing Then

                For Each tmpItem In item.ChildNodes


                    If tmpItem.Name.ToUpper() = "SMSDatabaseServerName".ToUpper() Then
                        SMSDatabaseServerName = tmpItem.InnerText
                    End If

                    If tmpItem.Name.ToUpper() = "SMSDatabaseName".ToUpper() Then
                        SMSDatabaseName = tmpItem.InnerText
                    End If
                    If tmpItem.Name.ToUpper() = "SMSDatabaseUserid".ToUpper() Then
                        SMSDatabaseUserid = tmpItem.InnerText
                    End If

                    If tmpItem.Name.ToUpper() = "SMSDatabasePassword".ToUpper() Then
                        SMSDatabasePassword = tmpItem.InnerText
                    End If
                    If tmpItem.Name.ToUpper() = "SMSDataBaseIntegratedSecurity".ToUpper() Then
                        SMSDataBaseIntegratedSecurity = tmpItem.InnerText
                    End If
                Next
            End If
        Catch ex As Exception
            loglog("Error Reading SMS database  settings ex:" & ex.Message, True)
        End Try

        SMSDatabaseConnectionString = ""
        SMSDatabaseConnectionString = "data source=" & SMSDatabaseServerName
        SMSDatabaseConnectionString += ";initial catalog=" & SMSDatabaseName
        SMSDatabaseConnectionString += ";integrated security=" & SMSDataBaseIntegratedSecurity
        SMSDatabaseConnectionString += ";user id=" & SMSDatabaseUserid
        SMSDatabaseConnectionString += ";password=" & SMSDatabasePassword

        Try
            items = Nothing
            item = Nothing
            items = xmlD.GetElementsByTagName("CIB_SMS_Service")
            item = items(0)
            If Not item Is Nothing Then

                For Each tmpItem In item.ChildNodes


                    If tmpItem.Name.ToUpper() = "SMSServiceDatabaseServerName".ToUpper() Then
                        SMSServiceDatabaseServerName = tmpItem.InnerText
                    End If

                    If tmpItem.Name.ToUpper() = "SMSServiceDatabaseName".ToUpper() Then
                        SMSServiceDatabaseName = tmpItem.InnerText
                    End If
                    If tmpItem.Name.ToUpper() = "SMSServiceDatabaseUserid".ToUpper() Then
                        SMSServiceDatabaseUserid = tmpItem.InnerText
                    End If

                    If tmpItem.Name.ToUpper() = "SMSServiceDatabasePassword".ToUpper() Then
                        SMSServiceDatabasePassword = tmpItem.InnerText
                    End If
                    If tmpItem.Name.ToUpper() = "SMSServiceDataBaseIntegratedSecurity".ToUpper() Then
                        SMSServiceDataBaseIntegratedSecurity = tmpItem.InnerText
                    End If
                Next
            End If
        Catch ex As Exception
            loglog("Error Reading SMS CIB Service database  settings ex:" & ex.Message, True)
        End Try

        SMSServiceDatabaseConnectionString = ""
        SMSServiceDatabaseConnectionString = "data source=" & SMSServiceDatabaseServerName
        SMSServiceDatabaseConnectionString += ";initial catalog=" & SMSServiceDatabaseName
        SMSServiceDatabaseConnectionString += ";integrated security=" & SMSServiceDataBaseIntegratedSecurity
        SMSServiceDatabaseConnectionString += ";user id=" & SMSServiceDatabaseUserid
        SMSServiceDatabaseConnectionString += ";password=" & SMSServiceDatabasePassword

        Try
            items = Nothing
            item = Nothing
            items = xmlD.GetElementsByTagName("SMSHTTP")
            item = items(0)
            If Not item Is Nothing Then

                For Each tmpItem In item.ChildNodes


                    If tmpItem.Name.ToUpper() = "HTTPIP".ToUpper() Then
                        HTTPIP = tmpItem.InnerText
                    End If

                    If tmpItem.Name.ToUpper() = "HTTPPort".ToUpper() Then
                        HTTPPort = tmpItem.InnerText
                    End If
                    If tmpItem.Name.ToUpper() = "HTTPSender".ToUpper() Then
                        HTTPSender = tmpItem.InnerText
                    End If

                    If tmpItem.Name.ToUpper() = "HTTPUsername".ToUpper() Then
                        HTTPUsername = tmpItem.InnerText
                    End If
                    If tmpItem.Name.ToUpper() = "HTTPPassword".ToUpper() Then
                        HTTPPassword = tmpItem.InnerText
                    End If
                Next
            End If
        Catch ex As Exception
            loglog("Error Reading Banque Misr HTTP settings ex:" & ex.Message, True)
        End Try

        Try
            items = Nothing
            item = Nothing
            items = xmlD.GetElementsByTagName("SMSHTTPUNIBank")
            item = items(0)
            If Not item Is Nothing Then

                For Each tmpItem In item.ChildNodes


                    If tmpItem.Name.ToUpper() = "URL".ToUpper() Then
                        SMSHTTPUNIBank_URL = tmpItem.InnerText
                    End If

                    If tmpItem.Name.ToUpper() = "From".ToUpper() Then
                        SMSHTTPUNIBank_From = tmpItem.InnerText
                    End If
                    If tmpItem.Name.ToUpper() = "ClientReference".ToUpper() Then
                        SMSHTTPUNIBank_ClientReference = tmpItem.InnerText
                    End If

                    If tmpItem.Name.ToUpper() = "ClientID".ToUpper() Then
                        SMSHTTPUNIBank_ClientID = tmpItem.InnerText
                    End If
                    If tmpItem.Name.ToUpper() = "ClientSecret".ToUpper() Then
                        SMSHTTPUNIBank_ClientSecret = tmpItem.InnerText
                    End If
                    If tmpItem.Name.ToUpper() = "RegisteredDelivery".ToUpper() Then
                        SMSHTTPUNIBank_RegisteredDelivery = tmpItem.InnerText
                    End If
                Next
            End If
        Catch ex As Exception
            loglog("Error Reading UniBank HTTP settings ex:" & ex.Message, True)
        End Try

        Try
            items = Nothing
            item = Nothing
            items = xmlD.GetElementsByTagName("SMSHTTPBM")
            item = items(0)
            If Not item Is Nothing Then

                For Each tmpItem In item.ChildNodes


                    If tmpItem.Name.ToUpper() = "URL".ToUpper() Then
                        URL = tmpItem.InnerText
                    End If

                    If tmpItem.Name.ToUpper() = "SMSUserName".ToUpper() Then
                        SMSUserName = tmpItem.InnerText
                    End If
                    If tmpItem.Name.ToUpper() = "SMSPassword".ToUpper() Then
                        SMSPassword = tmpItem.InnerText
                    End If

                    If tmpItem.Name.ToUpper() = "KEY".ToUpper() Then
                        KEY = tmpItem.InnerText
                    End If
                    If tmpItem.Name.ToUpper() = "SenderName".ToUpper() Then
                        SenderName = tmpItem.InnerText
                    End If

                Next
            End If
        Catch ex As Exception
            loglog("Error Reading BM HTTP settings ex:" & ex.Message, True)
        End Try

        items = Nothing
        item = Nothing

        xmlD = Nothing
        






    End Sub

    Shared Sub loglog(ByVal LogData As String, ByVal sepLine As Boolean)
        Dim fname As String
        Dim logdir As String
        Dim fulFilename As String
        Dim dinfo As DirectoryInfo
        Dim finfo As FileInfo
        Dim strmWrtr As StreamWriter
        'Dim cd As String
        Dim speratorLine As String = "=============================================================================================="

        fname = "CUSTOMERALERT_" + Now.Year.ToString("0000") & Now.Month.ToString("00") & Now.Day.ToString("00") & ".log"
        logdir = "c:\NCRAlertService_log\"

        Try
            If System.IO.Directory.Exists(logdir) = False Then
                dinfo = System.IO.Directory.CreateDirectory(logdir)
            Else
                dinfo = New DirectoryInfo(logdir)
            End If

        Catch ex As Exception
            Try
                logdir = System.Environment.GetEnvironmentVariable("tmp") & "\log"
                dinfo = Directory.CreateDirectory(logdir)
            Catch exx As Exception

                loge("Can Not Create Log:" & logdir & " ex:" & exx.Message)
                loge(LogData)
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
    Shared Sub loge(ByVal Data As String)
        Try
            If Not EventLog.SourceExists("CustomerAlertService") Then
                EventLog.CreateEventSource("CustomerAlertService", "Application")
            End If
            Dim ev As New EventLog
            ev.Source = "CustomerAlertService"
            ev.WriteEntry(Data, EventLogEntryType.Error)

        Catch ex As Exception
        End Try
    End Sub
End Class
