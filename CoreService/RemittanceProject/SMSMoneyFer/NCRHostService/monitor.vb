Public Class Monitor

    Private HostClientApplication As String = "NCRHostClientMF_AS400.exe"
    Private HostClientApplicationTitle As String = "NCR Client Access"
    Public Shared mvStopListening As Boolean
    Public Declare Auto Function FindWindow Lib "user32.dll" Alias "FindWindow" (ByVal lpClassName As String, ByVal lpWindowName As String) As Integer

    Public Declare Auto Function DestroyWindow Lib "user32" Alias "DestroyWindow" (ByVal hwnd As Integer) As Integer


    Public Sub doMonitor()
        NCRMoneyFer.log.loglog("doMonitor,Will Try starting HC Monitor servivce image name [" & HostClientApplication & "]", "HC_Monitor", False)

        While mvStopListening = False

            If CheckHostOffLineTime() = True Then
                DropMonitorClient()
            Else
                If Now.Minute Mod 5 = 0 Then
                    startHostclientApplication()
                End If
            End If
            

            Threading.Thread.Sleep(60000)
            System.Windows.Forms.Application.DoEvents()
        End While

    End Sub
    Function CheckHostOffLineTime() As Boolean
        Dim t1 As DateTime
        Dim t2 As DateTime
        Dim t As DateTime
        Try
            t1 = NCRMoneyFer.CONFIGClass.HostOffLineStartTime
            t2 = NCRMoneyFer.CONFIGClass.HostOffLineEndTime
            t = Now.ToShortTimeString
            If t > t1 And t < t2 Then
                NCRMoneyFer.log.loglog("CheckHostOffLineTime Host will go Offline so IF will be stopped, start time=[" & NCRMoneyFer.CONFIGClass.HostOffLineStartTime & "] end time=[" & NCRMoneyFer.CONFIGClass.HostOffLineEndTime & "]", False)
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            NCRMoneyFer.log.loglog("CheckHostOffLineTime Exception:" & ex.ToString, False)

            Return False
        End Try




    End Function

    Private Sub startHostclientApplication()
        Dim rtrn As Long
       

        Try
            rtrn = FindWindow(vbNullString, HostClientApplicationTitle)

            If rtrn = 0 Then
                NCRMoneyFer.log.loglog("Findwindow returns:" & rtrn, "HC_Monitor", False)
                NCRMoneyFer.log.loglog("Shell 1 command:[" & NCRMoneyFer.CONFIGClass.ServiceExecutablePath & HostClientApplication & "]", "HC_Monitor", False)
                Shell(NCRMoneyFer.CONFIGClass.ServiceExecutablePath & HostClientApplication)
                NCRMoneyFer.log.loglog("Shell 2 command:[" & NCRMoneyFer.CONFIGClass.ServiceExecutablePath & HostClientApplication & "]", "HC_Monitor", False)
            End If

        Catch ex As Exception
            NCRMoneyFer.log.loglog("Start Host Application Exception:" & ex.ToString, "HC_Monitor", False)
        End Try

    End Sub
    Public Sub DropMonitorClient()
        Dim p() As System.Diagnostics.Process
        

        Try
            p = System.Diagnostics.Process.GetProcessesByName("NCRHostClientMF_AS400")
            If p.Length > 0 Then
                p(0).Kill()
                NCRMoneyFer.log.loglog("DropMonitorClient, Successfully Kill process name [" & p(0).ProcessName & "]", "HC_Monitor", False)
            End If

        Catch ex As Exception
            NCRMoneyFer.log.loglog("DropMonitorClient process  [NCRHostClientMF_AS400] Exception:" & ex.ToString, "HC_Monitor", False)
        End Try

    End Sub
End Class
