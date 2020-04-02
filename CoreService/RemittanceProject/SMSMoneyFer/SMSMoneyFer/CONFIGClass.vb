
Public Class CONFIGClass
    'listener settings
    Public Shared LocalPort As Integer
    Public Shared ClientTimeOut As Integer
    Public Shared MaxConnections As Integer
    Public Shared LogPath As String
    Public Shared CheckATMIPMatch As Integer

    Public Shared PINLength As Integer = 4



    'database settings
    Public Shared DataSource As String
    Public Shared InitialCatalog As String
    Public Shared IntegratedSecurity As String
    Public Shared UserId As String
    Public Shared password As String
    Public Shared ConnectionString As String

    Public Shared ServiceExecutablePath As String

    'Rules
    'Public Shared DepositTransactionExpirationDays As Integer = 2
    Public Shared ATMIdLength As Integer = 5
    Public Shared BankIdLength As Integer = 5
    Public Shared CountryCodeLength As Integer = 10
    Public Shared CheckAmountFlag As Integer = 0
    Public Shared LowestDenom As Integer = 5
    Public Shared DepositTransactionExpirationCheckPeriodMinutes As Integer = 10
    Public Shared HostOffLineStartTime As String '"HH:mm:ss"
    Public Shared HostOffLineEndTime As String '"HH:mm:ss"
    Public Shared LocalCountry As String = ""
    Public Shared LocalBank As String = ""
    Public Shared CheckForUncertainWithdrawalFlag As Integer = 0
    Public Shared UnCertainActionReason As String = ""
    Public Shared DepositorMustRegister As Integer = 1
    Public Shared BeneficiaryMustRegister As Integer = 1
    Public Shared WhenCheckBeneficiaryRegisteration As Integer = 1 '1= during deposit phase, 2 =during withdrawal phase
    Public Shared UseBeneficiaryAsId As Integer = 0
    Public Shared UseAmountAsDPIN As Integer = 0
    Public Shared ApplyLimitsOnBeneficiary As Integer = 0
   
    Public Shared SwitchIP As String
    Public Shared SwitchPort As String
    Public Shared ISOHeader As String = ""
    Public Shared ISORequestId As String = ""
    Public Shared ProcessingCode As String = ""
    Public Shared DataElement18 As String = ""
    Public Shared DataElement32 As String = ""
    Public Shared DataElement41 As String = ""
    Public Shared DataElement22 As String = "0500"
    Public Shared ChipOrMagneticIndicatorIndexInTrack2 As Integer = 5
    Public Shared MagneticIndicatorIndicatorValue As String = "1"
    Public Shared SwitchTimeOut As Integer = 30




  
   


    Public Shared Function readConfig() As Integer
        Dim xmlD As Xml.XmlDocument
        Dim items As Xml.XmlNodeList
        Dim item As Xml.XmlNode
        Dim tstr As String
        Dim tmpItem As Xml.XmlNode
        Dim cfgFile As String = ""
        Dim tmp As String
        Dim shortnameStart As Integer
        Dim prevIndex As Integer

        Try

            tmp = System.Reflection.Assembly.GetExecutingAssembly.Location
            shortnameStart = 0
            prevIndex = tmp.LastIndexOf("\")

            ServiceExecutablePath = tmp.Substring(0, prevIndex + 1)
            cfgFile = ServiceExecutablePath & "config.xml"

            xmlD = New Xml.XmlDocument
            xmlD.Load(cfgFile)

        Catch ex As Exception
            log.loge("Can Not Load " & cfgFile & "  ex:" & ex.Message, EventLogEntryType.Error)
            Return 1
        End Try
        log.loge("Configuration File loaded successfully file: " & cfgFile, EventLogEntryType.Information)
        Try
            items = Nothing
            item = Nothing
            items = xmlD.GetElementsByTagName("Database")


            item = items(0)

            For Each tmpItem In item.ChildNodes
                If tmpItem.Name.ToUpper() = "DataSource".ToUpper() Then
                    DataSource = tmpItem.InnerText
                End If
                If tmpItem.Name.ToUpper() = "InitialCatalog".ToUpper() Then
                    InitialCatalog = tmpItem.InnerText
                End If
                If tmpItem.Name.ToUpper() = "IntegratedSecurity".ToUpper() Then
                    IntegratedSecurity = tmpItem.InnerText
                End If
                If tmpItem.Name.ToUpper() = "UserId".ToUpper() Then
                    UserId = tmpItem.InnerText
                End If
                If tmpItem.Name.ToUpper() = "Password".ToUpper() Then
                    password = tmpItem.InnerText
                End If


            Next




            ConnectionString = "data source=" & DataSource
            ConnectionString = ConnectionString & ";initial catalog=" & InitialCatalog
            ConnectionString = ConnectionString & ";integrated security=" & IntegratedSecurity
            ConnectionString = ConnectionString & ";user id=" & UserId
            Dim pPwd As String = ""
            If Decrypt(password, pPwd) = False Then
                log.loge("Can Not Decrypt Database PWD", EventLogEntryType.Information)
                Return 2
            End If
            ConnectionString = ConnectionString & ";password=" & pPwd
            'log.loge("Database Connection Settings :[" & ConnectionString & "]", EventLogEntryType.Information)
        Catch ex As Exception
            log.loge("Error Reading Database Connection Settings ex:" & ex.Message, EventLogEntryType.Error)
        End Try

        Try
            items = Nothing
            item = Nothing
            items = xmlD.GetElementsByTagName("Listener")
            item = items(0)
            For Each tmpItem In item.ChildNodes
                If tmpItem.Name.ToUpper() = "MaxConnections".ToUpper() Then
                    tstr = tmpItem.InnerText
                    Try
                        MaxConnections = CInt(tstr)
                    Catch ex As Exception
                        MaxConnections = 10
                    End Try
                End If

                If tmpItem.Name.ToUpper() = "LocalPort".ToUpper() Then
                    tstr = tmpItem.InnerText
                    Try
                        LocalPort = CLng(tstr)
                    Catch ex As Exception
                        LocalPort = 1000
                    End Try
                End If



                If tmpItem.Name.ToUpper() = "ClientTimeOut".ToUpper() Then
                    tstr = tmpItem.InnerText
                    Try
                        ClientTimeOut = CInt(tstr)
                    Catch ex As Exception
                        ClientTimeOut = 90 'seconds
                    End Try
                End If

                If tmpItem.Name.ToUpper() = "CheckATMIPMatch".ToUpper() Then
                    tstr = tmpItem.InnerText
                    Try
                        CheckATMIPMatch = CLng(tstr)
                    Catch ex As Exception
                        CheckATMIPMatch = 0 'do not check
                    End Try
                End If
                If tmpItem.Name.ToUpper() = "LogPath".ToUpper() Then
                    LogPath = tmpItem.InnerText
                End If

            Next

        Catch ex As Exception
            log.loge("Error Reading ATM Listener socket settings ex:" & ex.Message, EventLogEntryType.Error)
        End Try


        Try
            items = Nothing
            item = Nothing
            items = xmlD.GetElementsByTagName("BusinessRules")
            item = items(0)
            For Each tmpItem In item.ChildNodes
               
                If tmpItem.Name.ToUpper() = "ATMIdLength".ToUpper() Then
                    tstr = tmpItem.InnerText
                    Try
                        ATMIdLength = CInt(tstr)
                    Catch ex As Exception
                        ATMIdLength = 5
                    End Try
                End If
                If tmpItem.Name.ToUpper() = "BankIdLength".ToUpper() Then
                    tstr = tmpItem.InnerText
                    Try
                        BankIdLength = CInt(tstr)
                    Catch ex As Exception
                        BankIdLength = 5
                    End Try
                End If
                If tmpItem.Name.ToUpper() = "CountryCodeLength".ToUpper() Then
                    tstr = tmpItem.InnerText
                    Try
                        CountryCodeLength = CInt(tstr)
                    Catch ex As Exception
                        CountryCodeLength = 10
                    End Try
                End If

                If tmpItem.Name.ToUpper() = "DepositTransactionExpirationCheckPeriodMinutes".ToUpper() Then
                    tstr = tmpItem.InnerText
                    Try
                        DepositTransactionExpirationCheckPeriodMinutes = CInt(tstr)
                    Catch ex As Exception
                        DepositTransactionExpirationCheckPeriodMinutes = 10
                    End Try
                End If
                If tmpItem.Name.ToUpper() = "CheckForUncertainWithdrawalFlag".ToUpper() Then
                    tstr = tmpItem.InnerText
                    Try
                        CheckForUncertainWithdrawalFlag = CInt(tstr)
                    Catch ex As Exception
                        CheckForUncertainWithdrawalFlag = 0
                    End Try
                End If
                

                If tmpItem.Name.ToUpper() = "DepositorMustRegister".ToUpper() Then
                    tstr = tmpItem.InnerText
                    Try
                        DepositorMustRegister = CInt(tstr)
                    Catch ex As Exception
                        DepositorMustRegister = 1
                    End Try
                End If

                If tmpItem.Name.ToUpper() = "BeneficiaryMustRegister".ToUpper() Then
                    tstr = tmpItem.InnerText
                    Try
                        BeneficiaryMustRegister = CInt(tstr)
                    Catch ex As Exception
                        BeneficiaryMustRegister = 1
                    End Try
                End If
                If tmpItem.Name.ToUpper() = "ApplyLimitsOnBeneficiary".ToUpper() Then
                    tstr = tmpItem.InnerText
                    Try
                        ApplyLimitsOnBeneficiary = CInt(tstr)
                    Catch ex As Exception
                        ApplyLimitsOnBeneficiary = 0
                    End Try
                End If
                If tmpItem.Name.ToUpper() = "WhenCheckBeneficiaryRegisteration".ToUpper() Then
                    tstr = tmpItem.InnerText
                    Try
                        WhenCheckBeneficiaryRegisteration = CInt(tstr)
                    Catch ex As Exception
                        WhenCheckBeneficiaryRegisteration = 1
                    End Try
                End If
                If tmpItem.Name.ToUpper() = "UseBeneficiaryAsId".ToUpper() Then
                    tstr = tmpItem.InnerText
                    Try
                        UseBeneficiaryAsId = CInt(tstr)
                    Catch ex As Exception
                        UseBeneficiaryAsId = 0
                    End Try
                End If
                If tmpItem.Name.ToUpper() = "UseAmountAsDPIN".ToUpper() Then
                    tstr = tmpItem.InnerText
                    Try
                        UseAmountAsDPIN = CInt(tstr)
                    Catch ex As Exception
                        UseAmountAsDPIN = 0
                    End Try
                End If
                If tmpItem.Name.ToUpper() = "UnCertainActionReason".ToUpper() Then
                    UnCertainActionReason = tmpItem.InnerText
                End If
                If tmpItem.Name.ToUpper() = "HostOffLineStartTime".ToUpper() Then
                    HostOffLineStartTime = tmpItem.InnerText

                End If
                If tmpItem.Name.ToUpper() = "HostOffLineEndTime".ToUpper() Then
                    HostOffLineEndTime = tmpItem.InnerText

                End If
                If tmpItem.Name.ToUpper() = "LocalBank".ToUpper() Then
                    LocalBank = tmpItem.InnerText

                End If
                If tmpItem.Name.ToUpper() = "LocalCountry".ToUpper() Then
                    LocalCountry = tmpItem.InnerText

                End If
                If tmpItem.Name.ToUpper = "CheckAmount".ToUpper Then
                    tstr = tmpItem.InnerText
                    Try
                        CheckAmountFlag = CInt(tstr)
                    Catch ex As Exception
                        CheckAmountFlag = 0
                    End Try
                End If
                If tmpItem.Name.ToUpper = "LowestDenom".ToUpper Then
                    tstr = tmpItem.InnerText
                    Try
                        LowestDenom = CInt(tstr)
                    Catch ex As Exception
                        LowestDenom = 5
                    End Try
                End If
            Next

        Catch ex As Exception
            log.loge("Error Reading Business Rules  settings ex:" & ex.Message, EventLogEntryType.Error)
        End Try

        Try
            items = Nothing
            item = Nothing
            items = xmlD.GetElementsByTagName("CardBased")


            item = items(0)

            For Each tmpItem In item.ChildNodes
                If tmpItem.Name.ToUpper() = "SwitchIP".ToUpper() Then
                    SwitchIP = tmpItem.InnerText
                End If
                If tmpItem.Name.ToUpper() = "SwitchPort".ToUpper() Then
                    SwitchPort = tmpItem.InnerText
                End If
                If tmpItem.Name.ToUpper() = "ISOHeader".ToUpper() Then
                    ISOHeader = tmpItem.InnerText
                End If
                If tmpItem.Name.ToUpper() = "ISORequestId".ToUpper() Then
                    ISORequestId = tmpItem.InnerText
                End If
                If tmpItem.Name.ToUpper() = "ProcessingCode".ToUpper() Then
                    ProcessingCode = tmpItem.InnerText
                End If
                If tmpItem.Name.ToUpper() = "DataElement18".ToUpper() Then
                    DataElement18 = tmpItem.InnerText
                End If
                If tmpItem.Name.ToUpper() = "DataElement32".ToUpper() Then
                    DataElement32 = tmpItem.InnerText
                End If
                If tmpItem.Name.ToUpper() = "DataElement41".ToUpper() Then
                    DataElement41 = tmpItem.InnerText
                End If
                If tmpItem.Name.ToUpper() = "SwitchTimeOut".ToUpper() Then
                    tstr = tmpItem.InnerText
                    Try
                        SwitchTimeOut = Integer.Parse(tstr)
                    Catch ex As Exception
                        SwitchTimeOut = 30
                    End Try
                End If
                If tmpItem.Name.ToUpper() = "MagneticIndicatorIndicatorValue".ToUpper() Then
                    MagneticIndicatorIndicatorValue = tmpItem.InnerText
                End If
                If tmpItem.Name.ToUpper() = "DataElement22".ToUpper() Then
                    DataElement22 = tmpItem.InnerText
                End If
                If tmpItem.Name.ToUpper() = "ChipOrMagneticIndicatorIndexInTrack2".ToUpper() Then
                    tstr = tmpItem.InnerText
                    Try
                        ChipOrMagneticIndicatorIndexInTrack2 = Integer.Parse(tstr)
                    Catch ex As Exception
                        ChipOrMagneticIndicatorIndexInTrack2 = 5
                    End Try
                End If



            Next



        Catch ex As Exception
            log.loge("Error Reading CardBased Parameters Settings ex:" & ex.Message, EventLogEntryType.Error)
        End Try





        xmlD = Nothing
        items = Nothing
        item = Nothing
        Return 0
    End Function
    Public Shared Function Decrypt(ByVal edata As String, ByRef pdata As String) As Boolean

        Try
            Dim et As NCRCrypto.NCRCrypto
            et = New NCRCrypto.NCRCrypto
            
            pdata = et.eT3_Decrypr(edata)
            Return True
        Catch ex As Exception
            log.loge("Decryption error ex:" & ex.Message, EventLogEntryType.Error)
            Return False
        End Try
    End Function


End Class
