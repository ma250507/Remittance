Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient

Partial Class Actions
    Inherits System.Web.UI.Page
    Private Con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("NCRMoneyFerConnection").ConnectionString)
    Private Com As SqlCommand
    Private dr As SqlDataReader

    Private app_base_dir As String = System.AppDomain.CurrentDomain.BaseDirectory


    Private ipHostInfo As IPHostEntry
    Private ipAddress As IPAddress
    Private remoteEP As IPEndPoint
    Private msgToSend As String
    Private bytes(1024) As Byte
    Private Socksender As Socket
    Private Tran As TRX
    Private msg As Byte()
    Private bytesRec As Integer
    Private Reply As String
    Private ConfirmedReply As String
    Private SelectedTrx As String = ""
    Private perm As Permissions
    Private LstOfErrors As ErrorCodes
    Private UserPerm As Permissions
    'Private KeyTrials As Integer = 0
    Private MaximumKeyTrials As Integer = 0
    Public ExtendATMid As String = System.Configuration.ConfigurationManager.AppSettings("ExtendATMid")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("Status") = "InValid_user" Or Session("Status") = Nothing) Then
            Response.Redirect("Login.aspx")
        End If
        If (IsPostBack <> True) Then
            Try
                UserPerm = New Permissions()
                UserPerm = Session("Perm")
                If (UserPerm.Maintenance <> "True") Then
                    Response.Redirect("Login.aspx")
                End If
                If (UserPerm.Administration = "True" Or UserPerm.Maintenance = "True") Then
                    Btn_Expire.Visible = True
                Else
                    Btn_Expire.Visible = False
                End If
            Catch ex As Exception
                Response.Redirect("Login.aspx")
            End Try

            ipHostInfo = Dns.Resolve(Dns.GetHostName())
            ipAddress = ipHostInfo.AddressList(0)
            ViewState.Add("MyIP", ipAddress.ToString())



            SelectedTrx = Session("Trx")
            SqlDataSource2.SelectParameters("TransactionCode").DefaultValue = SelectedTrx
            GridView1.DataBind()
            If GridView1.Rows.Count = 0 Then
                Lbl_TH.Visible = False
            Else
                Lbl_TH.Visible = True
            End If

            Com = New SqlCommand("select TransactionCode, (select CountryName from country where countrycode=Transactions.countrycode) as 'CountryName',Bankcode,(select Atmname from atm where atm.atmid=Transactions.AtmId) as 'AtmId',RequestType,AtmDate,AtmTime,AtmTrxSequence,DepositorMobile,BeneficiaryMobile,Amount,DepositStatus,(select ActionDateTime from TransactionNestedActions where [Action] = '01' and TransactionCode = '" & Session("Trx") & "') as 'DepositDateTime',WithdrawalStatus,WithdrawalDateTime,SMSSendingStatus,SMSSentDateTime,WSendingStatus,WSentDateTime,ResendSMSFlag,ResendSMSDateTime,(Select Top 1 ActionDateTime  from TransactionNestedActions inner join transactions on TransactionNestedActions.TransactionCode=transactions.TransactionCode where TransactionNestedActions.TransactionCode='" & Session("Trx") & "' and Action='17' order by ActionDateTime desc) as 'RedemptionDT',(Select count(*) from TransactionKeyCheckTrials where TransactionKeyCheckTrials.TransactionCode = transactions.TransactionCode and TrialFlag = 0 ) as 'KeyTrials', ReActivationCounter,(select MaximumKeytrials from Bank where countrycode=Transactions.countrycode and BankCode=Transactions.BankCode ) as MaximumKeyTrials from transactions where transactioncode='" & Session("Trx") & "'", Con)
            Try
                Con.Open()
                dr = Com.ExecuteReader()
            Catch ex As Exception
                Lbl_Status.Visible = True
                Lbl_Status.Text = ex.Message
                Return
            End Try



            While (dr.Read())
                Lbl_TC.Text = dr("TransactionCode")
                Lbl_CC.Text = dr("countryname")
                Lbl_BC.Text = dr("BankCode")
                'Lbl_AId.Text = dr("AtmID")
                Lbl_TRXSeq.Text = dr("ATMTrxSequence")
                If (IsDBNull(dr("AtmID"))) Then
                    Lbl_AId.Text = ""
                Else
                    Lbl_AId.Text = dr("AtmID")
                End If
                If (IsDBNull(dr("DepositorMobile"))) Then
                    Lbl_DM.Text = ""
                Else
                    Lbl_DM.Text = dr("DepositorMobile")
                End If
                If (IsDBNull(dr("BeneficiaryMobile"))) Then
                    Lbl_BM.Text = ""
                Else
                    Lbl_BM.Text = dr("BeneficiaryMobile")
                End If
                If (IsDBNull(dr("Amount"))) Then
                    Lbl_AMT.Text = ""
                Else
                    Lbl_AMT.Text = dr("Amount")
                End If
                If (IsDBNull(dr("DepositStatus"))) Then
                    Lbl_DS.Text = ""
                Else
                    Lbl_DS.Text = dr("DepositStatus")
                End If
                If (IsDBNull(dr("DepositDateTime"))) Then
                    Lbl_DDT.Text = ""
                Else
                    Lbl_DDT.Text = dr("DepositDateTime")
                End If
                If (IsDBNull(dr("WithdrawalStatus"))) Then
                    Lbl_WS.Text = ""
                Else
                    Lbl_WS.Text = dr("WithdrawalStatus")
                End If
                If (IsDBNull(dr("WithdrawalDateTime"))) Then
                    Lbl_WDT.Text = ""
                Else
                    Lbl_WDT.Text = dr("WithdrawalDateTime")
                End If
                If (IsDBNull(dr("SMSSendingStatus"))) Then
                    Lbl_TAS.Text = ""
                Else
                    Lbl_TAS.Text = dr("SMSSendingStatus")
                End If
                If (IsDBNull(dr("SMSSentDateTime"))) Then
                    Lbl_ADT.Text = ""
                Else
                    Lbl_ADT.Text = dr("SMSSentDateTime")
                End If

                If (IsDBNull(dr("WSendingStatus"))) Then
                    Lbl_ADT.Text = ""
                Else
                    Lbl_ADT.Text = dr("WSendingStatus")
                End If
                If (IsDBNull(dr("WSentDateTime"))) Then
                    Lbl_ADT.Text = ""
                Else
                    Lbl_ADT.Text = dr("WSentDateTime")
                End If
                If (IsDBNull(dr("ResendSMSFlag"))) Then
                    Lbl_ADT.Text = ""
                Else
                    Lbl_ADT.Text = dr("ResendSMSFlag")
                End If
                If (IsDBNull(dr("ResendSMSDateTime"))) Then
                    Lbl_ADT.Text = ""
                Else
                    Lbl_ADT.Text = dr("ResendSMSDateTime")
                End If
                'If (IsDBNull(dr("RedemptionPIN"))) Then
                '    Lbl_RS.Text = ""
                'Else
                '    Lbl_RS.Text = "Redeemed"
                'End If KeyTrials
                If (IsDBNull(dr("RedemptionDT"))) Then
                    Lbl_RDT.Text = ""
                    Lbl_RS.Text = ""
                Else
                    Lbl_RS.Text = "Redeemed"
                    Lbl_RDT.Text = dr("RedemptionDT")
                End If


                If (IsDBNull(dr("KeyTrials"))) Then
                    Lbl_WithdrawalTrials.Text = 0
                Else
                    Lbl_WithdrawalTrials.Text = dr("KeyTrials")
                End If

                If (IsDBNull(dr("ReActivationCounter"))) Then
                    Lbl_ReActivateC.Text = 0
                Else
                    Lbl_ReActivateC.Text = dr("ReActivationCounter")
                End If

                If (IsDBNull(dr("MaximumKeyTrials"))) Then
                    MaximumKeyTrials = 0
                Else
                    MaximumKeyTrials = dr("MaximumKeyTrials")
                End If
                Session.Add("KT", MaximumKeyTrials)


            End While
            'Try
            '    dr.Close()
            '    Com.CommandText = "Select ActionDateTime as RedemptionDT  from TransactionNestedActions inner join transactions on TransactionNestedActions.TransactionCode=transactions.TransactionCode where TransactionNestedActions.TransactionCode='" & SelectedTrx & "' and Action='17' order by ActionDateTime desc"
            '    dr = Com.ExecuteReader()
            '    If (IsDBNull(dr(0))) Then
            '        Lbl_RDT.Text = ""
            '    Else
            '        Lbl_RDT.Text = dr(0)
            '    End If
            'Catch ex As Exception
            '    Lbl_Status.Visible = True
            '    Lbl_Status.Text = "Error:11"
            'End Try
            Try
                dr.Close()
                Com.CommandText = "select countrycode from country where countryname='" & Lbl_CC.Text & "'"
                dr = Com.ExecuteReader()
                While (dr.Read())
                    Session.Add("CCode", dr(0))
                End While
            Catch ex As Exception
                Lbl_Status.Visible = True
                Lbl_Status.Text = "Error:10"
            End Try
            Try
                dr.Close()
                Con.Close()
            Catch ex As Exception
                Lbl_Status.Visible = True
                Lbl_Status.Text = ex.Message
            End Try


            SetAvailability()
            'SetPermissions()
        Else
            SetAvailability()
            'SetPermissions()
            Lbl_Status.Visible = False
            BTNConfirm.Visible = False
            SelectedTrx = Session("Trx")
            SqlDataSource2.SelectParameters("TransactionCode").DefaultValue = SelectedTrx
            GridView1.DataBind()
        End If


    End Sub
    Public Sub SetAvailability()
        If (Lbl_WithdrawalTrials.Text >= Session("KT")) Then
            Btn_ResetKT.Enabled = True
        ElseIf (Lbl_WithdrawalTrials.Text < Session("KT")) Then
            Btn_ResetKT.Enabled = False
        End If
        Select Case Lbl_DS.Text
            Case "CANCELED"
                Toggle(False, False, False, False, False, False)
            Case "AUTHORIZED"
                Toggle(False, False, False, False, False, False)
            Case "CONFIRMED", "Confirmed"
                Select Case Lbl_WS.Text
                    Case ""
                        Toggle(False, True, False, False, True, True)
                        drpd_Resend.Items(4).Enabled = False
                    Case "AUTHORIZED"
                        Toggle(False, False, False, True, False, False)
                    Case "CONFIRMED"
                        Toggle(False, False, False, False, False, False)
                    Case "CANCELED"
                        Toggle(False, True, False, False, False, False)
                End Select
            Case "EXPIRED"
                Select Case Lbl_WS.Text
                    Case "AUTHORIZED"
                        Toggle(False, False, False, True, False, False)
                    Case "EXPIRED"
                        Toggle(True, False, False, False, False, False)
                    Case "CANCELED"
                        Toggle(False, False, False, False, False, False)
                    Case "CONFIRMED"
                        If (Lbl_RS.Text <> "" And Lbl_RDT.Text <> "") Then
                            Toggle(False, False, False, False, False, False)
                        Else
                            Toggle(False, False, False, False, True, False)
                            drpd_Resend.Items(1).Enabled = False
                            drpd_Resend.Items(2).Enabled = False
                            drpd_Resend.Items(3).Enabled = False
                        End If
                End Select
            Case "HOLD"
                Select Case Lbl_WS.Text
                    Case "HOLD"
                        Toggle(False, False, True, False, False, True)
                End Select
            Case Else
                Lbl_Status.Text = "Could Not Detect transaction status,Manual selection should be taken"
                Lbl_Status.Visible = True
                Exit Select
        End Select
    End Sub
    Public Sub Toggle(ByVal Activate As Boolean, ByVal Hold As Boolean, ByVal UnHold As Boolean, ByVal Unblock As Boolean, ByVal Resend As Boolean, ByVal Expire As Boolean)
        btn_Activate.Enabled = Activate
        btn_Hold.Enabled = Hold
        btn_Unhold.Enabled = UnHold
        Btn_Unblock.Disabled = Not Unblock
        btn_Resend0.Disabled = Not Resend
        Btn_Expire.Enabled = Expire
    End Sub
    Public Sub SetPermissions()
        'perm = Session("Perm")
        'If (perm.Activate <> "True") Then
        '    btn_Activate.Enabled = False
        'End If
        'If (perm.Hold <> "True") Then
        '    btn_Hold.Enabled = False

        'End If
        'If (perm.Unhold <> "True") Then
        '    btn_Unhold.Enabled = False

        'End If
        'If (perm.Resend <> "True") Then
        '    btn_Resend0.Disabled = True

        'End If
        'If (perm.Unblock <> "True") Then
        '    Btn_Unblock.Disabled = True
        'End If
    End Sub
    Private Function PrepareCommands(ByVal ReqType As String, ByVal Trxcode As String) As Integer

        'If txt_TRX_Code.Text = "" Then
        '    Lbl_Status.Text = "Please Enter a valid transaction code"
        '    Lbl_Status.Visible = True
        '    Return
        'End If

        LstOfErrors = New ErrorCodes

        Try
            Socksender = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            ipAddress = ipAddress.Parse(Session("ServiceIP")) 'ipAddress.Parse(CStr(ViewState("MyIP"))) 'ipAddress.Parse("153.57.235.57") 
            remoteEP = New IPEndPoint(ipAddress, Session("ServicePort"))
            Tran = New TRX()
            Socksender.Connect(remoteEP)

        Catch ex As Exception
            Lbl_Status.Text = "Error: Could not Connect to the MONEYFER Service."
            Lbl_Status.Visible = True
            Return -1
        End Try

        If ExtendATMid = "No" Then
            Tran.AtmId = Tran.Spaces(Session("MainATM"), 5) '"TEL01" 'Tran.Spaces(Session("ATMID"), 5)
            Tran.BankCode = Tran.Spaces(Lbl_BC.Text, 5) 'Tran.Spaces(Session("BCode"), 5)
            Tran.CountryCode = Tran.Spaces(Session("CCode"), 10)
        Else
            Tran.AtmId = Tran.Spaces(Session("MainATM"), 10) '"TEL01" 'Tran.Spaces(Session("ATMID"), 5)
            Tran.BankCode = Tran.Spaces(Lbl_BC.Text, 5) 'Tran.Spaces(Session("BCode"), 5)
            Tran.CountryCode = Tran.Spaces(Session("CCode"), 10)
        End If
        
        Tran.RequestType = ReqType
        Tran.ResponseCode = "     "
        Tran.AtmDate = Tran.Spaces(System.DateTime.Now.ToString("dd/MM/yyyy"), 10)
        Tran.AtmTime = Tran.Spaces(System.DateTime.Now.ToString("hh:mm:ss"), 8)
        Tran.TrxSequence = Tran.Spaces("", 10)
        Tran.DepositorMobile = Tran.Spaces("", 20)
        Tran.DepositorPin = Tran.Spaces("", 10)
        Tran.BeneficiaryMobile = Tran.Spaces("", 20)
        Tran.BeneficiaryPin = Tran.Spaces("", 10)
        Tran.Amount = Tran.Spaces("", 15)
        Tran.Currency = Tran.Spaces("", 5)
        Tran.HostTransactionCode = Tran.Spaces(Trxcode, 20)
        Tran.ActionReason = Tran.Spaces("", 25)
        Tran.DispNotes = Tran.Spaces("", 8)
        Tran.DispensedAmount = Tran.Spaces("", 15)
        Tran.CommissionAmount = Tran.Spaces("", 15)
        Tran.SMSLang = Tran.Spaces("", 1)
        Tran.MinimumValue = Tran.Spaces("", 15)
        Tran.MaximumValue = Tran.Spaces("", 15)
        Tran.ReceiptLine1 = Tran.Spaces("", 40)
        Tran.ReceiptLine2 = Tran.Spaces("", 40)
        Tran.ReceiptLine3 = Tran.Spaces("", 40)





        msg = Encoding.ASCII.GetBytes(Tran.ToString)
        Try
            Socksender.Send(msg)
        Catch ex As Exception
            Lbl_Status.Text = "Error:" & ex.ToString
            Lbl_Status.Visible = True
            Return -1
        End Try
        System.Threading.Thread.Sleep(1000)
        Try
            bytesRec = Socksender.Receive(bytes)
            Reply = Encoding.ASCII.GetString(bytes, 0, bytesRec)
            If ExtendATMid = "No" Then
                If (Reply.Substring(26, 5) <> "00000") Then
                    Lbl_Status.Text = "Error:" & CType(LstOfErrors.Errors(Reply.Substring(26, 5)), String)
                    Lbl_Status.Visible = True
                    Return -1

                Else
                    'Lbl_Status.Text = "DONE"
                    'Lbl_Status.Visible = True
                    BTNConfirm.Visible = True
                    GridView1.DataBind()
                    Return 0
                End If
            Else
                If (Reply.Substring(31, 5) <> "00000") Then
                    Lbl_Status.Text = "Error:" & CType(LstOfErrors.Errors(Reply.Substring(26, 5)), String)
                    Lbl_Status.Visible = True
                    Return -1

                Else
                    'Lbl_Status.Text = "DONE"
                    'Lbl_Status.Visible = True
                    BTNConfirm.Visible = True
                    GridView1.DataBind()
                    Return 0
                End If
            End If
            
        Catch ex As Exception
            Lbl_Status.Text = "Error:" & ex.Message
            Lbl_Status.Visible = True
            Return -1
        End Try
        Try
            Socksender.Shutdown(SocketShutdown.Both)
            Socksender.Close()
        Catch ex As Exception
            Lbl_Status.Text = "Error:" & ex.ToString
            Lbl_Status.Visible = True
            Return -1
        End Try
        Return 0
    End Function

    Protected Sub btn_Activate0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Activate.Click
        Dim ret As Integer
        Lbl_Status.Visible = False
        ret = PrepareCommands("13", Session("Trx").ToString())
        If (ret = 0) Then
            Log(Session("user").ToString(), "Activate", Session("Trx").ToString(), Session("UserName"), Session("Branch"))
            Lbl_DS.Text = "CONFIRMED"
            Lbl_WS.Text = ""
            SetAvailability()
            SetPermissions()
        End If
    End Sub

    Protected Sub btn_Hold0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Hold.Click
        Dim ret As Integer
        Lbl_Status.Visible = False
        ret = PrepareCommands("04", Session("Trx").ToString())
        If (ret = 0) Then
            Log(Session("user").ToString(), "Hold", Session("Trx").ToString(), Session("UserName"), Session("Branch"))
            Lbl_DS.Text = "HOLD"
            Lbl_WS.Text = "HOLD"
            SetAvailability()
            SetPermissions()
        End If

    End Sub

    Protected Sub btn_Unhold0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Unhold.Click
        Dim ret As Integer
        Lbl_Status.Visible = False
        ret = PrepareCommands("14", Session("Trx").ToString())
        Log(Session("user").ToString(), "UnHold", Session("Trx").ToString(), Session("UserName"), Session("Branch"))
        If (ret = 0) Then
            Lbl_DS.Text = "CONFIRMED"
            Lbl_WS.Text = ""
            SetAvailability()
            SetPermissions()
        End If
    End Sub

    'Protected Sub btn_Resend1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Resend0.Click

    '    Lbl_Status.Visible = False
    '    drpd_Resend.Visible = True


    'End Sub

    'Protected Sub Btn_Unblock0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btn_Unblock.Click
    '    PrepareCommands("09", Session("Trx").ToString())
    '    Log(Session("user").ToString(), "UnBlock", Session("Trx").ToString())
    'End Sub

    Protected Sub drpd_Resend_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpd_Resend.SelectedIndexChanged


    End Sub

    Public Sub Log(ByVal UserID As String, ByVal Action As String, ByVal TransactionCode As String, ByVal Name As String, ByVal Branch As String)
        Dim ret As Integer
        Com = New SqlCommand()
        Com.CommandText = "insert into useractions values('" & UserID & "','" & Action & "',getdate(),'" & TransactionCode & "','" & Name & "' , '" & Branch & "')"
        Com.Connection = Con
        Try
            Con.Open()
            ret = Com.ExecuteNonQuery()

            If (ret <> 1) Then
                Lbl_Status.Text = "Error while logging history."
                Lbl_Status.Visible = True
                Return
            End If
            Con.Close()
        Catch ex As Exception
            Lbl_Status.Text = "An error occurred: " & ex.ToString() & ""
            Lbl_Status.Visible = True
        End Try

    End Sub


    Private Sub MessageBox(ByVal strMsg As String)
        Dim lbl As New Label
        lbl.Text = "<script language='javascript'>" & Environment.NewLine _
                   & "var res=window.confirm(" & "'" & strMsg & "'" & ")</script>"
        Page.Controls.Add(lbl)

    End Sub

    Protected Sub Btn_UnblockConfirm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btn_UnblockConfirm.Click
        PrepareCommands(drpd_Unblock.SelectedItem.Value, Session("Trx").ToString())
        'drpd_Unblock.Visible = False
        'Btn_UnblockConfirm.Visible = False
        Log(Session("user").ToString(), "UnBlock", Session("Trx").ToString(), Session("UserName"), Session("Branch"))
        SetAvailability()
        SetPermissions()
    End Sub

    Protected Sub Btn_ResendConfirm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btn_ResendConfirm.Click
        PrepareCommands(drpd_Resend.SelectedItem.Value, Session("Trx").ToString())
        'drpd_Resend.Visible = False
        'Btn_ResendConfirm.Visible = False
        Log(Session("user").ToString(), "Resend", Session("Trx").ToString(), Session("UserName"), Session("Branch"))
        SetAvailability()
        SetPermissions()
    End Sub

    Protected Sub BTNConfirm_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BTNConfirm.Click
        Response.Redirect("Maintenance.aspx")
    End Sub

    Protected Sub Btn_ResetKT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btn_ResetKT.Click
        Dim ret As Integer
        Lbl_Status.Visible = False
        ret = PrepareCommands("29", Session("Trx").ToString())
        If (ret = 0) Then
            Log(Session("user").ToString(), "Reset Key Trials", Session("Trx").ToString(), Session("UserName"), Session("Branch"))
            Lbl_WithdrawalTrials.Text = "0"
            SetAvailability()
            SetPermissions()
        End If
    End Sub

    Protected Sub Btn_Expire_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btn_Expire.Click
        Dim ret As Integer
        Lbl_Status.Visible = False
        ret = PrepareCommands("23", Session("Trx").ToString())
        If (ret = 0) Then
            Log(Session("user").ToString(), "Manual Expiration", Session("Trx").ToString(), Session("UserName"), Session("Branch"))
            Lbl_DS.Text = "EXPIRED"
            Lbl_WS.Text = "EXPIRED"
            SetAvailability()
            SetPermissions()
        End If
    End Sub
End Class
