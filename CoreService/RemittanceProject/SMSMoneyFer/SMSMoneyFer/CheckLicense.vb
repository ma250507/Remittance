Imports System.Data.SqlClient
Imports System.IO
Imports System.Threading

Public Class CheckLicense
    Private StartTimerCallBackObj As Threading.TimerCallback
    Private StartTimerObject As Threading.Timer
    Private StartStatClassObject As StatClass
    Private StartConnectionAtiveTime As Long
    Public Sub start()
        loglog("NCR Checking License Process started", True)
        StartConnectionAtiveTime = 1800000
        StartStatClassObject = New StatClass
        StartTimerCallBackObj = New Threading.TimerCallback(AddressOf Timertask)
        StartTimerObject = New Threading.Timer(StartTimerCallBackObj, StartStatClassObject, Timeout.Infinite, Timeout.Infinite)  'disabled
        StartTimerObject.Change(StartConnectionAtiveTime, StartConnectionAtiveTime)    'enable
        loglog("NCR Checking License Process started", True)
    End Sub
    Public Sub Timertask(ByVal stat As Object)

        Dim st As StatClass2
        Dim Ret As Boolean
        Dim Ret2 As Boolean
        Dim Ret3 As Boolean
        Dim ATMsCount As Integer
        Dim DisabledATMno As Integer
        Try
            st = CType(stat, StatClass2)
        Catch ex1 As Exception
            log.loglog("StarttimerTask Stat Object Conversion error ex:" & ex1.Message, False)
            stat = Nothing
        End Try
        Try
            loglog("NCR Checking License Process Begin Checking", True)
            StartTimerObject.Change(Timeout.Infinite, StartConnectionAtiveTime)  'disable

            Ret = ReadLicenseFile(ATMsCount)
            If Ret = True Then
                Ret2 = GetLicensedATMs(ATMsCount, DisabledATMno)
                If Ret2 = True Then
                    If DisabledATMno > 0 Then
                        Ret3 = UpdateATMsbyLicense(ATMsCount)
                        If Ret3 = False Then
                            Abort_Thread()

                        End If
                    End If
                Else
                    Abort_Thread()
                End If
            Else
                Abort_Thread()
            End If

            StartTimerObject.Change(StartConnectionAtiveTime, StartConnectionAtiveTime)    'enable
        Catch ex As Exception
            loglog("NCR Checking License Process error = " & ex.Message, True)
            Abort_Thread()

        End Try

    End Sub
    Private Sub Abort_Thread()



       

        If Not StartTimerObject Is Nothing Then
            StartTimerObject.Dispose()
        End If
       
        Try

            loglog("Will Stop Remittance Service", True)
            NCRMoneyFerService.mvATMListener.StopListner()
            NCRMoneyFerService.mvATMListener = Nothing
            Thread.CurrentThread.Abort()
            End
        Catch ex As Exception
            LogLog("Abort Tread error ex = " & ex.ToString, True)
        End Try
    End Sub
    Private Function UpdateATMsbyLicense(ByVal ATMsCount As Integer) As Boolean
        Dim Qstr As String
        Dim ret As Boolean

        Try
            Qstr = "update i "
            Qstr += "set i.ATMIPAddress = NULL "
            Qstr += "from [SMSMoneyFer].[dbo].[atm] i "
            Qstr += "where not exists (Select * from "
            Qstr += "(SELECT  TOP " & ATMsCount & " * "
            Qstr += "FROM [SMSMoneyFer].[dbo].[ATM] where ATMIPAddress is not NULL order by ATMId ) y "
            Qstr += "where i.atmid = y.ATMId) "

            ret = InsertDB(Qstr, CONFIGClass.ConnectionString)
            If ret = False Then
                loglog("Error In Checking License error in database", False)
                
                Return False
            End If
            Return True
        Catch ex As Exception
            loglog("UpdateATMsByLicense Error connection string:[" & CONFIGClass.ConnectionString & "] Ex:" & ex.ToString, False)
            Return False
        End Try
    End Function
    Private Function ReadLicenseFile(ByRef ATMsCount As Integer) As Boolean
        Dim sr As StreamReader
        Dim Str As String
        Dim Body As String
        Dim enc As NewCrypto
        Dim CheckSum As String
        Dim byteArr() As Byte
        Dim MsgData As String
        Dim cfgFile As String = ""
        Dim tmp As String
        Dim shortnameStart As Integer
        Dim prevIndex As Integer
        Dim ServiceExecutablePath As String
        Dim CheckSumByte As Byte
        Dim FinalStr As String
        Dim j As Integer = 0
        Dim TempStr As String
        Dim x As Integer
        Try


            tmp = System.Reflection.Assembly.GetExecutingAssembly.Location
            shortnameStart = 0
            prevIndex = tmp.LastIndexOf("\")

            ServiceExecutablePath = tmp.Substring(0, prevIndex + 1)
            cfgFile = ServiceExecutablePath & "License\Remittance.lic"
            Try
                sr = New StreamReader(cfgFile)
            Catch ex As Exception
                loglog("NCR Check LIC Error In Checking License file not found ", True)
                Return False
            End Try

            Str = sr.ReadLine
            sr.Close()

            If Str.Length <= 0 Then
                loglog("NCR Check LIC Error In Checking License file is empty length = 0 ", True)
                Return False
            End If
            enc = New NewCrypto

            enc.Key = "pXdQxZc0EZFr14Rt"
            enc.IV = "VcdKoVeR"

            Body = enc.Decrypt(Str)
            Body = Body.Substring(4)
            ReDim byteArr((Body.Length / 2) - 1)

            For x = 0 To byteArr.Length - 1
                byteArr(x) = "&H" & Body.Substring(j, 2)
                TempStr += ChrW(CInt("&H" & Body.Substring(j, 2)))
                j = j + 2
            Next
            For x = 0 To byteArr.Length - 2
                CheckSumByte = CheckSumByte Xor byteArr(x)

            Next
            If CheckSumByte = byteArr(byteArr.Length - 1) Then
                FinalStr = TempStr.Substring(0, TempStr.Length - 1)
                MsgData = "Remittance License for"
                MsgData += "Bank ID = " & FinalStr.Substring(0, 10).Trim & vbCrLf
                MsgData += "StartDate" & FinalStr.Substring(10, 14).Trim & vbCrLf
                MsgData += "ENdDate" & FinalStr.Substring(24, 14).Trim & vbCrLf
                MsgData += "NoOfATMs" & FinalStr.Substring(38, 10).Trim & vbCrLf
                loglog(MsgData, True)
                Try
                    ATMsCount = CInt(FinalStr.Substring(38, 10).Trim)
                Catch ex As Exception
                    loglog("NCR Check LIC Error In No of ATMs issue =  " & FinalStr.Substring(38, 10).Trim, True)
                    Return False
                End Try

            Else
                loglog("NCR Check LIC Error In Checking License file CheckSum issue ", True)
                Return False

            End If
            Return True
        Catch ex As Exception
            loglog("NCR Check LIC Error In Checking License path" & cfgFile & ",ex=[" & ex.Message & "]", True)

            Return False
        End Try
    End Function
    Private Function GetLicensedATMs(ByVal ATMCount As Integer, ByRef disableAtmNo As Integer) As Boolean
        Dim QSTR As String
        Dim Ret As String
        Dim DT As DataTable
        Try
            QSTR = "Select Count(*) as ATMCount "
            QSTR += "from [SMSMoneyFer].[dbo].[atm] i "
            QSTR += "where not exists (Select * from "
            QSTR += "(SELECT  TOP " & ATMCount & " * "
            QSTR += "FROM [SMSMoneyFer].[dbo].[ATM] where ATMIPAddress is not NULL order by ATMId ) y "
            QSTR += "where i.atmid = y.ATMId) "
            Ret = SelectFromDB(QSTR, CONFIGClass.ConnectionString, DT)

            If Ret = True Then
                If DT.Rows.Count > 0 Then
                    loglog("NIC License No Of ATMs Will be disabled = " & DT.Rows(0).Item(0), True)
                    disableAtmNo = CInt(DT.Rows(0).Item(0))
                    Return True
                Else
                    loglog("NIC License No Of ATMs not returned empty Result", True)
                    Return False
                End If

            Else
                loglog("NCR Check LIC Error In GetLicensedATMs error in selection Qstr = " & QSTR, True)
                Return False
            End If
        Catch ex As Exception
            loglog("NCR Check LIC Error In GetLicensedATMs error = " & ex.Message, True)
            Return False
        End Try
    End Function
    Private Function InsertDB(ByVal Qstr As String, ByVal ConnectionString As String) As Boolean
        Try
            Dim Con As SqlConnection
            Dim Cmd As New SqlCommand
            Dim Rowaffected As Integer

            Con = New SqlConnection(ConnectionString)
            Con.Open()

            Cmd.Connection = Con
            Cmd.CommandType = CommandType.Text
            Cmd.CommandText = Qstr

            Rowaffected = Cmd.ExecuteNonQuery()
            Con.Close()

            If Rowaffected >= 1 Then
                'log.loglog("Executing: [" & Qstr & "]", True)
                loglog("NCR Check LIC Update Command Row affected =[" & Rowaffected & "]", True)
                Return True
            Else
                'log.loglog("Executing: [" & Qstr & "]", True)
                loglog("NCR Check LIC Update Command Error Row affected =[" & Rowaffected & "]", True)

                Return False
            End If


        Catch ex As Exception
            loglog("NCR Check LIC Error In Update Qstr=[" & Qstr & "],ex=[" & ex.Message & "]", True)

            Return False
        End Try

    End Function
    Shared Sub loglog(ByVal LogData As String, ByVal sepLine As Boolean)
        Dim fname As String
        Dim logdir As String
        Dim fulFilename As String
        Dim dinfo As DirectoryInfo
        Dim finfo As FileInfo
        Dim strmWrtr As StreamWriter
        'Dim cd As String
        Dim speratorLine As String = "====================================================="
        fname = "NCRMoneyFerLicense_" & Now.ToString("yyyyMMdd") & ".log"
        logdir = CONFIGClass.LogPath


        Try
            If System.IO.Directory.Exists(logdir) = False Then
                dinfo = System.IO.Directory.CreateDirectory(logdir)
            Else
                dinfo = New DirectoryInfo(logdir)
            End If

        Catch ex As Exception
            Try
                logdir = System.Environment.GetEnvironmentVariable("tmp") & "\NCRMoneyFerLicense_log"
                dinfo = Directory.CreateDirectory(logdir)
                loge("Can Not Create Log:" & CONFIGClass.LogPath & " ex:" & ex.Message, EventLogEntryType.Error)

            Catch exx As Exception

                loge("Can Not Create Log:" & CONFIGClass.LogPath & " ex:" & exx.Message, EventLogEntryType.Error)
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
    Shared Sub loge(ByVal Data As String, ByVal eventType As System.Diagnostics.EventLogEntryType)
        Try
            If Not System.Diagnostics.EventLog.SourceExists("NCRMoneyFer License Service") Then
                System.Diagnostics.EventLog.CreateEventSource("NCRMoneyfer License Service", "Application")
            End If
            Dim ev As New System.Diagnostics.EventLog
            ev.Source = "NCRMoneyfer License Service"
            ev.WriteEntry(Data, eventType)

        Catch ex As Exception
        End Try
    End Sub
    Public Shared Function SelectFromDB(ByVal command As String, ByVal ConnectionString As String, ByRef DT As DataTable) As Boolean
        Dim ConnectionStr As String = ConnectionString  'ConnStrConfig
        Dim myConnection As SqlConnection
        Dim myCommand As SqlCommand
        Dim myAdapter As SqlDataAdapter

        Try

            myConnection = New SqlConnection(ConnectionStr)
            myCommand = New SqlCommand(command, myConnection)
            myConnection.Open()
            DT = New DataTable
            myAdapter = New SqlDataAdapter()
            myAdapter.SelectCommand = myCommand
            myAdapter.Fill(DT)
            myConnection.Close()
            Return True
        Catch ex As Exception
            loglog("Failed To Select from DataBase  " & ex.ToString, True)
            Return False
        End Try


    End Function
End Class
Class StatClass2
End Class
