Imports System.ServiceProcess
Imports System.Threading

Public Class NCRMoneyFerService

    Public Shared mvATMListener As ListnerClass
    Private mvMessage As MessageClass
    Private mvchecklicense As CheckLicense
    Private mvATMListenerLocalPort As Long = 1000
    Private mvMaxConnectionInactiveTime As Long = 90 'seconds
    Private mvMaxConCurrentConnections As Long = 10


    Protected Overrides Sub OnStart(ByVal args() As String)
        MyStart()

    End Sub

    Protected Overrides Sub OnStop()
        Try
            mvATMListener.StopListner()
            mvATMListener = Nothing

        Catch ex As Exception

        End Try

    End Sub
    Public Sub MyStart()

        Dim rtrn As Integer
        Dim newThreadStart As ThreadStart
        Dim newThread As Thread
        Dim newThreadStartDP As ThreadStart
        Dim newThreadDP As Thread
        Dim newThreadStartCheckLic As ThreadStart
        Dim newThreadCheckLic As Thread
        'Dim mvmonitor As Monitor
        'Dim newThreadStartHC As ThreadStart
        'Dim newThreadHC As Thread



        Try
            rtrn = CONFIGClass.readConfig()
            If rtrn <> 0 Then
                log.loge("Reading Configuration error will stop the service ... need to be restated", EventLogEntryType.Error)
                Return
            End If

        Catch ex As Exception
            log.loge("MyStart, Exception :" & vbNewLine & ex.ToString, EventLogEntryType.Error)
            Return
        End Try

        mvATMListenerLocalPort = CONFIGClass.LocalPort
        mvMaxConnectionInactiveTime = CONFIGClass.ClientTimeOut
        mvMaxConCurrentConnections = CONFIGClass.MaxConnections
        Try

            mvATMListener = New ListnerClass(mvATMListenerLocalPort, mvMaxConnectionInactiveTime, mvMaxConCurrentConnections)
            newThreadStart = New ThreadStart(AddressOf mvATMListener.Start)
            newThread = New Thread(newThreadStart)
            newThread.Start()

        Catch ee As Exception
            log.loglog("NCRMoneyFer, On Start service error:" & ee.ToString(), True)

        End Try

        Try

            mvMessage = New MessageClass
            newThreadStartDP = New ThreadStart(AddressOf mvMessage.DeActivateProcess)
            newThreadDP = New Thread(newThreadStartDP)
            newThreadDP.Start()

        Catch ee As Exception
            log.loglog("NCRMoneyFer, On Start, starting DeActivate service error:" & ee.ToString(), True)

        End Try

        Try
            mvchecklicense = New CheckLicense
            newThreadStartCheckLic = New ThreadStart(AddressOf mvchecklicense.start)
            newThreadCheckLic = New Thread(newThreadStartCheckLic)
            newThreadCheckLic.Start()
        Catch ex As Exception
            log.loglog("NCRMoneyFer, On Start, starting CheckLicense service error:" & ex.ToString(), True)
        End Try

        Try

            mvMessage = New MessageClass
            newThreadStartDP = New ThreadStart(AddressOf mvMessage.NewDeActivateProcess)
            newThreadDP = New Thread(newThreadStartDP)
            newThreadDP.Start()

        Catch ee As Exception
            log.loglog("NCRMoneyFer, On Start, starting DeActivate service error:" & ee.ToString(), True)

        End Try



    End Sub

End Class
