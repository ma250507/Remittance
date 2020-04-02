Imports System.Data.SqlClient
Imports System.IO
Imports System.Data
Imports System.Diagnostics

Public Class General

    Public Function GetConnectionString() As String
        Dim Connectionstring As String
        Connectionstring = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
        Return Connectionstring
    End Function
    Public Function GetLogPath() As String

        Dim LogPath As String
        LogPath = System.Configuration.ConfigurationManager.AppSettings("LogPath")
        Return LogPath


    End Function
    Public Function GetTransactionBalanceReport(ByVal StartDate As DateTime, ByVal ConnectionString As String, ByRef DT As DataTable) As Boolean
        Dim ConnectionStr As String = ConnectionString  'ConnStrConfig
        Dim myConnection As SqlConnection
        Dim myCommand As SqlCommand
        Dim myAdapter As SqlDataAdapter
        Try
            myConnection = New SqlConnection(ConnectionStr)
            myCommand = New SqlCommand
            myConnection.Open()
            DT = New DataTable
            myCommand.Connection = myConnection
            myCommand.CommandText = "TransactionBalanceStatment"
            myCommand.CommandType = CommandType.StoredProcedure
            myCommand.Parameters.AddWithValue("@StartDate", StartDate)
            myAdapter = New SqlDataAdapter()
            myAdapter.SelectCommand = myCommand
            myAdapter.Fill(DT)
            myConnection.Close()
            Return True

            Return True
        Catch ex As Exception
            Return False

        End Try
    End Function
    Public Function InsertDB(ByVal Qstr As String, ByVal ConnectionString As String) As Boolean
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

            If Rowaffected = 1 Then
                Return True
            Else
                loglog("Insert Command Error Row affected =[" & Rowaffected & "]", True)

                Return False
            End If


        Catch ex As Exception
            loglog("Error In Inserting Qstr=[" & Qstr & "],ex=[" & ex.Message & "]", True)
            Return False
        End Try
    End Function
    Public Function UpdateDB(ByVal Qstr As String, ByVal ConnectionString As String) As Boolean
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

            If Rowaffected = 1 Then
                Return True
            Else
                loglog("Update Command= [" & Qstr & "] Error Row affected =[" & Rowaffected & "]", True)

                Return False
            End If


        Catch ex As Exception
            loglog("Error In Updating Qstr=[" & Qstr & "],ex=[" & ex.Message & "]", True)
            Return False
        End Try
    End Function
    Public Function UpdateDBRegistered(ByVal Qstr As String, ByVal ConnectionString As String) As Boolean
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

            If Rowaffected = 2 Then
                Return True
            Else
                loglog("Update Command= [" & Qstr & "] Error Row affected =[" & Rowaffected & "]", True)

                Return False
            End If


        Catch ex As Exception
            loglog("Error In Updating Qstr=[" & Qstr & "],ex=[" & ex.Message & "]", True)
            Return False
        End Try
    End Function
    Public Function DeletDB(ByVal Qstr As String, ByVal ConnectionString As String) As Boolean
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

            If Rowaffected = 1 Then
                Return True
            Else
                loglog("Delete Command= [" & Qstr & "] Error Row affected =[" & Rowaffected & "]", True)

                Return False
            End If


        Catch ex As Exception
            loglog("Error In Deleting Qstr=[" & Qstr & "],ex=[" & ex.Message & "]", True)
            Return False
        End Try
    End Function
    Public Function ConnectToDatabase(ByVal command As String, ByVal ConnectionString As String, ByRef DT As DataTable) As Boolean
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
            loglog("Failed To Connect To DataBase  " & ex.ToString, True)

            Return False
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

        fname = "NCR Remittance Portal" & Now.Day.ToString & "-" & Now.Month.ToString & "-" & Now.Year.ToString & ".Log"
        logdir = GetLogPath()


        Try
            If System.IO.Directory.Exists(logdir) = False Then
                dinfo = System.IO.Directory.CreateDirectory(logdir)
            Else
                dinfo = New DirectoryInfo(logdir)
            End If

        Catch ex As Exception
            Try
                logdir = System.Environment.GetEnvironmentVariable("tmp") & "\RemittancePortal_log"
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
            If Not System.Diagnostics.EventLog.SourceExists("RemittancePortal") Then
                System.Diagnostics.EventLog.CreateEventSource("RemittancePortal", "Application")
            End If
            Dim ev As New System.Diagnostics.EventLog
            ev.Source = "RemittancePortal"
            ev.WriteEntry(Data, eventType)

        Catch ex As Exception
        End Try
    End Sub
End Class
