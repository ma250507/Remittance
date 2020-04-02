Imports System.Net
Imports System.Net.Sockets
Imports System.Data
Imports System.Data.SqlClient
Imports CrystalDecisions.Shared

Partial Class Teller
    Inherits System.Web.UI.Page

    Private ipHostInfo As IPHostEntry
    Private ipAddress As IPAddress
    Private remoteEP As IPEndPoint
    Private msgToSend As String
    Private bytes(1024) As Byte
    Private Socksender As New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
    Private Tran As TRX
    Private msg As Byte()
    Private bytesRec As Integer
    Private Reply As String
    Private ConfirmedReply As String
    Private Com As SqlCommand
    Private Con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("NCRMoneyFerConnection").ConnectionString)
    Private Dr As SqlDataReader
    Private Dec As New NCRCrypto
    Private Dt As DataTable
    Private LstOfErrors As ErrorCodes
    Private UserPerm As Permissions
    

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("Status") = "InValid_user" Or Session("Status") = Nothing) Then
            Response.Redirect("Login.aspx")
        End If
        If (IsPostBack <> True) Then
            Try
                UserPerm = New Permissions()
                UserPerm = Session("Perm")
                If (UserPerm.Teller <> "True") Then
                    Response.Redirect("Login.aspx")
                End If
            Catch ex As Exception
                Response.Redirect("Login.aspx")
            End Try
            Com = New SqlCommand("", Con)
            Try
                Con.Open()
                Com.CommandText = "select bankcode,countrycode " _
                                       & " from atm " _
                                       & " where atmid='TEL01'"
                Dr = Com.ExecuteReader()
                While (Dr.Read())
                    Session.Add("BCode", Dr.GetString(0))
                    Session.Add("CCode", Dr.GetString(1))
                End While
                Dr.Close()

                Com.CommandText = "select currencycode from currency"
                Dr = Com.ExecuteReader()
                While (Dr.Read())
                    DrpdCurr.Items.Add(Dr.GetString(0))
                    DropDownList1.Items.Add(Dr.GetString(0))
                End While
                Dr.Close()
            Catch ex As Exception
                Lbl_Status.Text = "DB Error:" & ex.Message & ""
                Lbl_Status.Visible = True
            End Try
            Try
                ipHostInfo = Dns.Resolve(Dns.GetHostName())
                ipAddress = ipHostInfo.AddressList(0)
                ViewState.Add("MyIP", ipAddress.ToString())
            Catch ex As Exception
                Lbl_Status.Text = "UnKnown Error:" & ex.Message & ""
                Lbl_Status.Visible = True
            End Try
        Else
            Lbl_Status.Visible = False

        End If
    End Sub


    Protected Sub Btn_Confirm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btn_Confirm.Click
        If (RB_Deposit.Checked) Then
            Tran = New TRX
            Tran.AtmId = "TEL01" 'Tran.Spaces(Lbl_AId.Text, 5)
            Tran.BankCode = Tran.Spaces(Session("BCode"), 5)
            Tran.CountryCode = Tran.Spaces(Session("CCode"), 10)
            Tran.RequestType = "01"
            Tran.ResponseCode = "     "
            Tran.AtmDate = Tran.Spaces(System.DateTime.Now.ToString("dd/MM/yyyy"), 10)
            Tran.AtmTime = Tran.Spaces(System.DateTime.Now.ToString("hh:mm:ss"), 8)
            Tran.TrxSequence = Tran.Spaces("", 10)
            Tran.DepositorMobile = Tran.Spaces(DepMobileTxt.Text, 20)
            Tran.DepositorPin = Tran.Spaces(BenMobileTxt.Text, 10)
            Tran.BeneficiaryMobile = Tran.Spaces("", 20)
            Tran.BeneficiaryPin = Tran.Spaces("", 10)
            Tran.Amount = Tran.Spaces(AmountTxt.Text, 15)
            Tran.Currency = Tran.Spaces(DrpdCurr.Text, 5)
            Tran.HostTransactionCode = Tran.Spaces("", 20)
            Tran.ActionReason = Tran.Spaces("", 25)
            Tran.DispNotes = Tran.Spaces("", 8)
            Tran.Rest = Tran.Spaces("", 31)
            Session.Add("DPTrx", Tran)
            Dim rtrn As Integer = -99
            Dim con_rtrn As Integer = -99
            Try
                rtrn = PrepareCommands(Tran)
                If (rtrn < 0) Then
                    'Lbl_Status.Text = "Authorization Error:" & rtrn
                    'Lbl_Status.Visible = True
                ElseIf (rtrn = 0) Then
                    Tran = Session("DPTrx")
                    Tran.RequestType = "11"
                    Tran.TrxSequence = CStr(ViewState("Trxsequence"))
                    Tran.HostTransactionCode = trxCodetxt.Text
                    con_rtrn = PrepareCommands(Tran)
                    If con_rtrn < 0 Then
                        'Lbl_Status.Text = "Confirmation Error:" & con_rtrn
                        'Lbl_Status.Visible = True
                    Else
                        ViewRcpt()
                    End If
                End If

            Catch ex As Exception
                Lbl_Status.Text = "UnKnown Error:" & con_rtrn
                Lbl_Status.Visible = True
            End Try

        ElseIf (RB_Withdraw.Checked) Then

            Tran = New TRX
            Tran.AtmId = "TEL01" 'Tran.Spaces(Lbl_AId.Text, 5)
            Tran.BankCode = Tran.Spaces(Session("BCode"), 5)
            Tran.CountryCode = Tran.Spaces(Session("CCode"), 10)
            Tran.RequestType = "02"
            Tran.ResponseCode = "     "
            Tran.AtmDate = Tran.Spaces(System.DateTime.Now.ToString("dd/MM/yyyy"), 10)
            Tran.AtmTime = Tran.Spaces(System.DateTime.Now.ToString("hh:mm:ss"), 8)
            Tran.TrxSequence = Tran.Spaces("", 10)
            Tran.DepositorMobile = Tran.Spaces(txt_DepMobile.Text, 20)
            Tran.DepositorPin = Tran.Spaces(txt_DepPin.Text, 10)
            Tran.BeneficiaryMobile = Tran.Spaces(txt_BenMobile.Text, 20)
            Tran.BeneficiaryPin = Tran.Spaces(txt_BenPin.Text, 10)
            Tran.Amount = Tran.Spaces(AmountTxt.Text, 15)
            Tran.Currency = Tran.Spaces(DropDownList1.Text, 5)
            Tran.HostTransactionCode = Tran.Spaces(txt_Trxode.Text, 20)
            Tran.ActionReason = Tran.Spaces("", 25)
            Tran.DispNotes = Tran.Spaces("", 8)
            Tran.Rest = Tran.Spaces("", 31)
            Session.Add("DPTrx", Tran)
            Dim rtrn As Integer = -99
            Dim con_rtrn As Integer = -99
            Try
                rtrn = PrepareCommands(Tran)
                If (rtrn < 0) Then
                    'Lbl_Status.Text = "Authorization Error:" & rtrn
                    'Lbl_Status.Visible = True
                ElseIf (rtrn = 0) Then
                    Tran = Session("DPTrx")
                    Tran.RequestType = "12"
                    Tran.TrxSequence = CStr(ViewState("Trxsequence"))
                    con_rtrn = PrepareCommands(Tran)
                    If con_rtrn < 0 Then
                        'Lbl_Status.Text = "Confirmation Error:" & con_rtrn
                        'Lbl_Status.Visible = True
                    Else
                        ViewRcpt()
                    End If
                End If

            Catch ex As Exception
                Lbl_Status.Text = "UnKnown Error:" & con_rtrn
                Lbl_Status.Visible = True
            End Try

        Else
            Lbl_Status.Text = "Please choose a function."
            Lbl_Status.Visible = True
            Return
        End If
    End Sub

    Private Function PrepareCommands(ByVal MyTrx As TRX) As Integer

        'If txt_TRX_Code.Text = "" Then
        '    Lbl_Status.Text = "Please Enter a valid transaction code"
        '    Lbl_Status.Visible = True
        '    Return
        'End If
        LstOfErrors = New ErrorCodes

        Try
            Socksender = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            ipAddress = ipAddress.Parse(CStr(ViewState("MyIP"))) 'ipAddress.Parse("153.57.235.57") 
            remoteEP = New IPEndPoint(ipAddress, 1009)
            Tran = New TRX()
            Socksender.Connect(remoteEP)

        Catch ex As Exception
            Lbl_Status.Text = "Error: Could not Connect to the MoneyFer Service." '& ex.ToString
            Lbl_Status.Visible = True
            Return -1
        End Try




        msg = Encoding.ASCII.GetBytes(MyTrx.ToString)
        Try
            Socksender.Send(msg)
        Catch ex As Exception
            Lbl_Status.Text = "Error while sending the message to the service" & ex.ToString
            Lbl_Status.Visible = True
            Return -1
        End Try
        System.Threading.Thread.Sleep(1000)
        Try
            bytesRec = Socksender.Receive(bytes)
            Reply = Encoding.ASCII.GetString(bytes, 0, bytesRec)
            If (Reply.Substring(26, 5) <> "00000") Then
                Lbl_Status.Text = "Error:" & CType(LstOfErrors.Errors(Reply.Substring(26, 5)), String)
                Lbl_Status.Visible = True
                Return -1
            Else
                trxCodetxt.Text = Reply.Substring(139, 20)
                If (MyTrx.RequestType = "01" Or MyTrx.RequestType = "02") Then
                    ViewState.Add("DP", Reply.Substring(79, 10))
                    ViewState.Add("Trxsequence", Reply.Substring(185, 11))
                    ViewState.Add("BP", Reply.Substring(89, 10))
                End If
                Lbl_Status.Text = "DONE"
                Lbl_Status.Visible = True
                If (MyTrx.RequestType = "11" Or MyTrx.RequestType = "12") Then
                    PrepareDT(Reply)
                End If
                Return 0
            End If
        Catch ex As Exception
            Lbl_Status.Text = "Error:While receiving Packets from the MoneyFer service."
            Lbl_Status.Visible = True
            Return -1
        End Try
        Try
            Socksender.Shutdown(SocketShutdown.Both)
            Socksender.Close()

        Catch ex As Exception
            Lbl_Status.Text = "Error while closing the connection" & ex.ToString
            Lbl_Status.Visible = True
            Return -1
        End Try
    End Function
    Public Function ViewRcpt() As Integer
        Dim Rpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument()
        Dim ParamFields As ParameterFields = Me.CrystalReportViewer1.ParameterFieldInfo
        Dim PField As ParameterField
        Dim PFieldValue As ParameterDiscreteValue
        Dim i As Integer = 1

        Com = New SqlCommand("", Con)
        Com.CommandText = "select ReceiptLine1,ReceiptLine2,ReceiptLine3 from bank where bankcode='" & Session("BCode") & "'"
        Con.Open()
        Dr = Com.ExecuteReader()
        While (Dr.Read())
            PField = New ParameterField
            PFieldValue = New ParameterDiscreteValue
            PField.Name = "L1"
            PFieldValue.Value = Dr.GetString(0)
            PField.CurrentValues.Add(PFieldValue)
            ParamFields.Add(PField)

            PField = New ParameterField
            PFieldValue = New ParameterDiscreteValue
            PField.Name = "L2"
            PFieldValue.Value = Dr.GetString(1)
            PField.CurrentValues.Add(PFieldValue)
            ParamFields.Add(PField)

            PField = New ParameterField
            PFieldValue = New ParameterDiscreteValue
            PField.Name = "L3"
            PFieldValue.Value = Dr.GetString(2)
            PField.CurrentValues.Add(PFieldValue)
            ParamFields.Add(PField)
        End While
        Try
            If (RB_Deposit.Checked) Then
                Rpt.FileName = System.AppDomain.CurrentDomain.BaseDirectory & "Reports\Rec.rpt"
                Rpt.SetDataSource(Dt)
            ElseIf (RB_Withdraw.Checked) Then
                Rpt.FileName = System.AppDomain.CurrentDomain.BaseDirectory & "Reports\Rec2.rpt"
                Rpt.SetDataSource(Dt)
            End If
            Session.Add("Rpt", Rpt)
            CrystalReportViewer1.ParameterFieldInfo = ParamFields
            CrystalReportViewer1.Visible = True
            CrystalReportViewer1.ReportSource = Rpt
            CrystalReportViewer1.DataBind()
        Catch ex As Exception
            Lbl_Status.Text = "Internal Error Please Try again Later :" & ex.ToString & ""
            Lbl_Status.Visible = True
            Return -1
        End Try

    End Function
    Public Sub PrepareDT(ByVal Reply As String)
        Dt = New DataTable("Receipt")
        Dim DT_Dr As DataRow
        Dt.Columns.Clear()
        Dt.Columns.Add("Date", GetType(String))
        Dt.Columns.Add("Time", GetType(String))
        Dt.Columns.Add("ATMId", GetType(String))
        Dt.Columns.Add("DepositorMobile", GetType(String))
        Dt.Columns.Add("BeneficiaryMobile", GetType(String))
        Dt.Columns.Add("DepositorPin", GetType(String))
        Dt.Columns.Add("BeneficiaryPin", GetType(String))
        Dt.Columns.Add("TransactionSequenceNumber", GetType(String))
        Dt.Columns.Add("TransactioCode", GetType(String))
        Dt.Columns.Add("DepositedAmount", GetType(String))

        'Dt.WriteXmlSchema(System.AppDomain.CurrentDomain.BaseDirectory & "\Rec.xml")

        DT_Dr = Dt.NewRow()
        DT_Dr("Date") = Reply.Substring(31, 10)
        DT_Dr("Time") = Reply.Substring(41, 8)
        DT_Dr("ATMId") = Reply.Substring(4, 5)
        If (RB_Deposit.Checked) Then
            DT_Dr("DepositorMobile") = DepMobileTxt.Text   'Reply.Substring(59, 20)
            DT_Dr("BeneficiaryMobile") = BenMobileTxt.Text 'Reply.Substring(89, 20)
            DT_Dr("DepositorPin") = CStr(ViewState("DP"))
            DT_Dr("BeneficiaryPin") = CStr(ViewState("BP"))
        ElseIf (RB_Withdraw.Checked) Then
            DT_Dr("DepositorMobile") = txt_DepMobile.Text   'Reply.Substring(59, 20)
            DT_Dr("BeneficiaryMobile") = txt_BenMobile.Text 'Reply.Substring(89, 20)
            DT_Dr("DepositorPin") = txt_DepPin.Text
            DT_Dr("BeneficiaryPin") = txt_BenPin.Text
        End If

        DT_Dr("TransactionSequenceNumber") = CStr(ViewState("Trxsequence"))
        DT_Dr("TransactioCode") = Reply.Substring(139, 20)
        DT_Dr("DepositedAmount") = Reply.Substring(119, 15)

        Dt.Rows.Add(DT_Dr)

    End Sub

    Protected Sub Btn_New_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btn_New.Click
        If (RB_Deposit.Checked) Then
            trxCodetxt.Text = ""
            AmountTxt.Text = ""
            DepMobileTxt.Text = ""
            BenMobileTxt.Text = ""
        ElseIf (RB_Withdraw.Checked) Then
            txt_DepMobile.Text = ""   'Reply.Substring(59, 20)
            txt_BenMobile.Text = "" 'Reply.Substring(89, 20)
            txt_DepPin.Text = ""
            txt_BenPin.Text = ""
            txt_Amount.Text = ""
            txt_Trxode.Text = ""
        End If
        CrystalReportViewer1.Visible = False
    End Sub
End Class

