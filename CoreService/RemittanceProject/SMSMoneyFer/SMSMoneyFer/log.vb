Imports System.io
Imports System.text

Public Class log

    Shared Sub loglog(ByVal LogData As String, ByVal sepLine As Boolean)
        Dim fname As String
        Dim logdir As String
        Dim fulFilename As String
        Dim dinfo As DirectoryInfo
        Dim finfo As FileInfo
        Dim strmWrtr As StreamWriter
        'Dim cd As String
        Dim speratorLine As String = "====================================================="
        fname = "NCRMoneyFer_" & Now.ToString("yyyyMMdd") & ".log"
        logdir = CONFIGClass.LogPath


        Try
            If System.IO.Directory.Exists(logdir) = False Then
                dinfo = System.IO.Directory.CreateDirectory(logdir)
            Else
                dinfo = New DirectoryInfo(logdir)
            End If

        Catch ex As Exception
            Try
                logdir = System.Environment.GetEnvironmentVariable("tmp") & "\NCRMoneyFer_log"
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

    Shared Sub loglog(ByVal LogData As String, ByVal TerminalId As String, ByVal sepLine As Boolean)
        Dim fname As String
        Dim logdir As String
        Dim fulFilename As String
        Dim dinfo As DirectoryInfo
        Dim finfo As FileInfo
        Dim strmWrtr As StreamWriter
        Dim lTerminalId As String = ""

        'Dim cd As String
        Dim speratorLine As String = "====================================================="

        lTerminalId = TerminalId.Trim
        fname = "NCRMoneyFer_" & Now.ToString("yyyyMMdd") & "_" & lTerminalId & ".log"
        logdir = CONFIGClass.LogPath


        Try
            If System.IO.Directory.Exists(logdir) = False Then
                dinfo = System.IO.Directory.CreateDirectory(logdir)
            Else
                dinfo = New DirectoryInfo(logdir)
            End If

        Catch ex As Exception
            Try
                logdir = System.Environment.GetEnvironmentVariable("tmp") & "\NCRMoneyFer_log"
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
            If Not System.Diagnostics.EventLog.SourceExists("NCRMoneyFer Service") Then
                System.Diagnostics.EventLog.CreateEventSource("NCRMoneyfer Service", "Application")
            End If
            Dim ev As New System.Diagnostics.EventLog
            ev.Source = "NCRMoneyfer Service"
            ev.WriteEntry(Data, eventType)

        Catch ex As Exception
        End Try
    End Sub

End Class
