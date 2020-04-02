Imports System.Threading

Public Class NCRHostService
    Private mvmonitor As Monitor
    Public Sub MyStart()
        Dim rtrn As Integer

        Dim newThreadStartHC As ThreadStart
        Dim newThreadHC As Thread


        Try
            rtrn = NCRMoneyFer.CONFIGClass.readConfig()
            If rtrn <> 0 Then
                NCRMoneyFer.log.loge("Reading Configuration error will stop the service ... need to be restated", EventLogEntryType.Error)
                Return
            End If

        Catch ex As Exception
            NCRMoneyFer.log.loge("MyStart, Exception :" & vbNewLine & ex.ToString, EventLogEntryType.Error)
            Return
        End Try

        Try

            mvmonitor = New Monitor
            newThreadStartHC = New ThreadStart(AddressOf mvmonitor.doMonitor)
            newThreadHC = New Thread(newThreadStartHC)
            newThreadHC.Start()

        Catch ee As Exception
            NCRMoneyFer.log.loglog("On Start, starting HostClient Monitor service error:" & ee.ToString(), "HC_Monitor", True)

        End Try


    End Sub
    Protected Overrides Sub OnStart(ByVal args() As String)
        Monitor.mvStopListening = False
        MyStart()
    End Sub

    Protected Overrides Sub OnStop()
        Try
            Monitor.mvStopListening = True
            mvmonitor.DropMonitorClient()

        Catch ex As Exception

        End Try

    End Sub

End Class
