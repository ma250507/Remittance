
Public Class Queues
    Public Function SendMessage(ByVal QPath As String, ByVal msgData As String, ByVal TerminalId As String) As Integer
        Dim msg As New MSMQ.MSMQMessage
        Dim q As MSMQ.MSMQQueue
        Dim qi As New MSMQ.MSMQQueueInfo

        qi.PathName = QPath


        Try


            qi.Create(, True)
            
            log.loglog("SendMessage, Q " & QPath & " created.", TerminalId, False)
        Catch ex As Exception
            log.loglog("SendMessage,creating Q " & QPath & " exp:" & ex.ToString, TerminalId, False)
        End Try

        Try

            q = qi.Open(MSMQ.MQACCESS.MQ_SEND_ACCESS, 0)
            msg.Body = msgData
            msg.Send(q)
            log.loglog("SendMessage, Successfully Writing Message to ACCS Q:[" & QPath & "]" & vbNewLine & "Data:[" & msgData & "]", TerminalId, False)
            Return 0
        Catch ex As Exception
            log.loglog("SendMessage, to " & QPath & " Exception:" & ex.ToString, TerminalId, False)
            Return 9
        End Try

    End Function
    Public Function CreateMessage(ByVal QPath As String, ByVal TerminalId As String) As Integer
        Dim msg As New MSMQ.MSMQMessage
        Dim cmpInt As Integer
        Dim ct As Date
        Dim qi As New MSMQ.MSMQQueueInfo
        Dim defaultCT As Date = "01/01/1970 02:00:00 AM"

        qi.PathName = QPath
        ct = qi.CreateTime

        cmpInt = ct.CompareTo(defaultCT)
        If cmpInt <= 0 Then
            Try
                qi.Create()
                log.loglog("CreateMessage, Q " & QPath & " created. at:" & qi.CreateTime, TerminalId, False)
                Return 0
            Catch ex As Exception
                log.loglog("SendMessage, to " & QPath & " Exception:" & ex.ToString, TerminalId, False)
                Return 9
            End Try
        Else
            log.loglog("CreateMessage, Q " & QPath & " already created. at: " & qi.CreateTime, TerminalId, False)
            Return 0
        End If



    End Function
    Public Function ReceiveMessage(ByVal QPath As String, ByRef msgData As String, ByVal TerminalId As String) As Integer
        Dim msg As New MSMQ.MSMQMessage
        Dim q As MSMQ.MSMQQueue
        Dim qi As New MSMQ.MSMQQueueInfo

        qi.PathName = QPath
        Try
            q = qi.Open(MSMQ.MQACCESS.MQ_RECEIVE_ACCESS, 0)
            msg = Nothing
            msg = q.Receive(ReceiveTimeout:=1000)
            If msg Is Nothing Then
                Return 2
            End If
            msgData = msg.Body
            log.loglog("ReceiveMessage, Successfully Read Message to ACCS Q:[" & QPath & "]" & vbNewLine & "Data:[" & msgData & "]", TerminalId, False)
            Return 0
        Catch ex As Exception
            log.loglog("ReceiveMessage, to " & QPath & " Exception:" & ex.ToString, TerminalId, False)
            Return 9
        End Try

    End Function
    Public Function ClearQ(ByVal QPath As String, ByVal TerminalId As String) As Integer
        Dim msg As New MSMQ.MSMQMessage
        Dim q As MSMQ.MSMQQueue
        Dim qi As New MSMQ.MSMQQueueInfo
        qi.PathName = QPath
        Try

            q = qi.Open(MSMQ.MQACCESS.MQ_RECEIVE_ACCESS, 0)
            msg = Nothing
            msg = q.Receive(ReceiveTimeout:=1)
            While Not (msg Is Nothing)
                msg = q.Receive(ReceiveTimeout:=1)
                If Not msg Is Nothing Then
                    log.loglog("ClearQ, Will ignore Q:[" & QPath & "]" & vbNewLine & "Data:[" & msg.Body & "]", TerminalId, False)
                End If
            End While
            Return 0
        Catch ex As Exception
            log.loglog("ReceiveMessage, to " & QPath & " Exception:" & ex.ToString, TerminalId, False)
            Return 9
        End Try

    End Function
End Class
