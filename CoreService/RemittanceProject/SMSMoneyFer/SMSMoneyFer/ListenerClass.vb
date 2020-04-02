Imports System.Threading
Imports System.Net.Sockets
Imports System.Diagnostics
Imports System.Net
Imports System.io
Imports System.text


Public Class ListnerClass

    Private mvListener As System.Net.Sockets.TcpListener
    Public Shared mvStopListening As Boolean
    Private mvLocalPort As Long = 1000
    Private mvMaxConCurrentConnections As Long = 10
    Private mvMaxConnectionInactiveTime As Long = 10000 'milliseconds

    Public Shared MAX_RECEIVE_LENGTH As Long = 4096

    Public Shared mvCurrentNumberOfConnections As Long = 0
    

    Public Sub StopListner()
        mvStopListening = True
    End Sub
    Public Shared Function StopListnerFlag() As Boolean
        Return mvStopListening
    End Function

    Public Sub Start()
        Dim newThreadStart As ThreadStart
        Dim newThread As Thread
        Dim ipHostInfo As IPHostEntry
        Dim ipAddress As IPAddress
        Dim remoteIPAddress As IPAddress
        Dim remotePort As String
        Dim MTC As MainTaskClass

        Try

            ipHostInfo = Dns.GetHostEntry(Dns.GetHostName())
            ipAddress = ipHostInfo.AddressList(0)

            mvListener = New System.Net.Sockets.TcpListener(ipAddress, mvLocalPort)
            mvListener.Start()
            log.loglog("NCRMoneyFer starts listening  on port: " & mvLocalPort, False)
            mvStopListening = False

            While Not mvStopListening
                Try
                    If mvListener.Pending() Then
                        Dim t As Socket = Nothing
                        t = mvListener.AcceptSocket()
                        remoteIPAddress = Net.IPAddress.Parse(CType(t.RemoteEndPoint, IPEndPoint).Address.ToString())
                        remotePort = CType(t.RemoteEndPoint, IPEndPoint).Port.ToString()
                        log.loglog("Client " + remoteIPAddress.ToString() + " Ask for connect on Port " + remotePort, True)


                        If mvCurrentNumberOfConnections >= mvMaxConCurrentConnections Then
                            t.Close()
                            log.loglog("No free rooms, will refuse connection request current connections=" & mvCurrentNumberOfConnections & " MAX=" & mvMaxConCurrentConnections, False)
                        Else
                            '''''''''
                            log.loglog("There is free rooms, will accept connection request current connections=" & mvCurrentNumberOfConnections & " MAX=" & mvMaxConCurrentConnections, False)
                            MTC = New MainTaskClass(t, remoteIPAddress, remotePort, mvMaxConnectionInactiveTime, Thread.CurrentThread)
                            newThreadStart = New ThreadStart(AddressOf MTC.MainTask)
                            newThread = New Thread(newThreadStart)
                            newThread.Start()

                            SyncLock System.Threading.Thread.CurrentThread
                                mvCurrentNumberOfConnections += 1
                            End SyncLock
                            '''''''''''''''''''
                        End If



                    End If


                Catch ThAEx As ThreadAbortException
                    SyncLock System.Threading.Thread.CurrentThread
                        mvCurrentNumberOfConnections -= 1
                    End SyncLock
                    If mvCurrentNumberOfConnections < 0 Then
                        mvCurrentNumberOfConnections = 0
                    End If
                Catch ex1 As Exception
                    log.loglog("Start Exception ex1:" + ex1.Message, False)
                End Try

                Thread.Sleep(500)
            End While
        Catch e As Exception

        End Try
    End Sub


    


    Public Sub New(ByVal mPort As Long, ByVal mMaxConnectionInactiveTime As Long, ByVal mMaxConcurrentConnections As Long)
        mvMaxConCurrentConnections = mMaxConcurrentConnections
        mvLocalPort = mPort
        mvMaxConnectionInactiveTime = mMaxConnectionInactiveTime * 1000

        

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class

Public Class MainTaskClass
    Private mvSocket As Socket
    Private mvLockThread As Thread
    Private mvIPAddress As IPAddress
    Private mvPort As String
    Private mvMaxConnectionInactiveTime As Long
    Private mvLengthPartLen As Integer = 4
    Private mvThisATmId As String
    Dim dl As Threading.TimerCallback
    Dim ti As Threading.Timer
    Dim st As StatClass
    Dim allStr As String = ""

    Public Sub New(ByVal NewSocket As Socket, ByVal IP As IPAddress, ByVal Port As String, ByVal mMaxConnectionInactiveTime As Long, ByVal pLockThread As Thread)
        mvSocket = NewSocket
        mvIPAddress = IP
        mvPort = Port
        mvMaxConnectionInactiveTime = mMaxConnectionInactiveTime
        mvLockThread = pLockThread
    End Sub
    Public Sub MainTask()
        Dim maxDalaLen As Long
        Dim receivedLen As Long
        Dim i As Long
        Dim messageLengthPart As String
        Dim actualMessageLength As Long
        Dim rcvBuf(ListnerClass.MAX_RECEIVE_LENGTH) As Byte
        Dim rcvBuf_LenPart(mvLengthPartLen) As Byte
        Dim mvLengthPartFormatter As String = "0000"
        Dim oneBuf(2) As Byte
        Dim IncomingDataStr As String

        Dim OutGoingReply As String
        Dim OutGoingReplyArray() As Byte
        Dim rtrn As Integer
        Dim mvMessage As MessageClass
        Dim ascii As Encoding = Encoding.ASCII
        'Dim mvMessage As MessageClass

        maxDalaLen = ListnerClass.MAX_RECEIVE_LENGTH
        log.loglog("Main task Started:", False)
        Try
            mvLengthPartFormatter = New String("0", mvLengthPartLen)
        Catch ex As Exception
            mvLengthPartFormatter = "0000"
        End Try
        While True

            Try
                st = New StatClass
                dl = New Threading.TimerCallback(AddressOf timerTask)
                ti = New Threading.Timer(dl, st, Timeout.Infinite, mvMaxConnectionInactiveTime)  'disabled
            Catch ex_t As Exception
                log.loglog("Main task Timer Initialization Error,ex=" + ex_t.Message, False)
                Abort_Thread()
                Return
            End Try


tryAgain:
            messageLengthPart = ""
            allstr = ""
            For i = 1 To mvLengthPartLen
                Try
                    ti.Change(mvMaxConnectionInactiveTime, mvMaxConnectionInactiveTime)    'enable
                    receivedLen = mvSocket.Receive(oneBuf, 1, SocketFlags.None)

                    ti.Change(Timeout.Infinite, mvMaxConnectionInactiveTime)  'disable


                    If receivedLen < 1 Then
                        log.loglog("Main task receiving first " & mvLengthPartLen & " byte receiveLen=" + receivedLen, False)
                        Abort_Thread()
                        Return
                    End If
                    messageLengthPart = messageLengthPart + ascii.GetChars(oneBuf)(0)
                    allStr += ascii.GetChars(oneBuf)(0)
                Catch ex11 As Exception
                    log.loglog("Main task receiving first " & mvLengthPartLen & " byte Error,ex=" + ex11.Message, False)
                    Abort_Thread()
                    Return
                End Try
            Next i



            actualMessageLength = 0
            Try
                actualMessageLength = Integer.Parse(messageLengthPart)
            Catch exx As Exception
                log.loglog("Main task can not Pars Message length(" + messageLengthPart.ToString() + "  ex:" + exx.Message, False)
            End Try

            If actualMessageLength < 1 Then
                log.loglog("Main task received invalid actualMessageLength=" + actualMessageLength.ToString() + " will ignore it", False)
                GoTo tryAgain
            End If
            log.loglog("Received Actual Length:" + actualMessageLength.ToString(), False)
            For i = 0 To actualMessageLength - 1
                Try
                    ti.Change(mvMaxConnectionInactiveTime, mvMaxConnectionInactiveTime)    'enable
                    receivedLen = mvSocket.Receive(oneBuf, 1, SocketFlags.None)
                    ti.Change(Timeout.Infinite, mvMaxConnectionInactiveTime)   'disable

                    If receivedLen < 1 Then
                        log.loglog("Main task receiving message body error.  ReceiveLen=" + receivedLen, False)
                        Abort_Thread()
                        Return
                    End If


                Catch ex11 As Exception
                    log.loglog("Main task receiving message body Error,ex=" + ex11.Message, False)
                    Abort_Thread()
                    Return
                End Try
                rcvBuf(i) = oneBuf(0)
                allStr += ascii.GetChars(oneBuf)(0)
            Next i
            Try
                IncomingDataStr = Encoding.ASCII.GetString(rcvBuf, 0, actualMessageLength)
            Catch ex As Exception
                log.loglog("Main task error in converting buffer to string ex:" + ex.Message + " will ignore it, waiting for new message ", False)
                GoTo tryAgain
            End Try
            log.loglog("Incoming data:" + IncomingDataStr, False)


            '''''''' Dycrept received data '''''''''''''''''''''''''''''''''''''''''''''

            '''''''' handle incoming request incomingdataStr '''''''''''''''''''''''''''''''
            mvMessage = Nothing
            mvMessage = New MessageClass
            MessageClass.LockThread = mvLockThread
            mvMessage.ATMIPAddress = mvIPAddress.ToString
            rtrn = mvMessage.DoRequest(IncomingDataStr)
            If rtrn = 0 Then
                OutGoingReply = mvMessage.GetOutgoingReplyData
                '''''''''''''''''''Encrypt OutGoing data ''''''''''''''''''

                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                OutGoingReply = Format(OutGoingReply.Length, mvLengthPartFormatter) & OutGoingReply
                log.loglog("Reply data:[" & OutGoingReply & "]", False)
               
                Try
                    OutGoingReplyArray = Encoding.ASCII.GetBytes(OutGoingReply)
                    mvSocket.Send(OutGoingReplyArray)
                Catch exs As Exception
                    log.loglog("sending Reply Error:" & exs.Message, False)
                End Try
            Else
                OutGoingReply = Space(20) & "00" & Format(rtrn, "00000")
                '''''''''''''''''''Encrypt OutGoing data ''''''''''''''''''

                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                OutGoingReply = Format(OutGoingReply.Length, mvLengthPartFormatter) & OutGoingReply
                log.loglog("Error,Reply data:[" & OutGoingReply & "]", False)
                Try
                    OutGoingReplyArray = Encoding.ASCII.GetBytes(OutGoingReply)
                    mvSocket.Send(OutGoingReplyArray)
                Catch exs As Exception
                    log.loglog("sending Reply Error:" & exs.Message, False)
                End Try

            End If
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''



            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


            Thread.Sleep(100)


        End While
        Abort_Thread()
    End Sub
    Private Sub Abort_Thread()



        SyncLock Thread.CurrentThread
            ListnerClass.mvCurrentNumberOfConnections -= 1
        End SyncLock
        If Not ti Is Nothing Then
            ti.Dispose()
        End If
        Try
            mvSocket.Close()
        Catch exp As Exception
        End Try
        Try
            Thread.CurrentThread.Abort()
        Catch ex As Exception

        End Try
    End Sub

    Protected Overrides Sub Finalize()

        MyBase.Finalize()
    End Sub
    Sub timerTask(ByVal stat As Object)
        Dim st As StatClass

        Try
            st = CType(stat, StatClass)
        Catch ex1 As Exception
            log.loglog("timerTask Stat Object Conversion error ex:" & ex1.Message, False)
            stat = Nothing
        End Try
        Try
            mvSocket.Close()
            log.loglog("timerTask close connection due to TimeOut", False)
            log.loglog("AllStr=[" & allStr & "]", False)
            st = Nothing
        Catch ex2 As Exception
            Abort_Thread()
            log.loglog("timerTask close connection error ex:" & ex2.Message, False)
            st = Nothing
        End Try
    End Sub
End Class
Class StatClass
End Class
