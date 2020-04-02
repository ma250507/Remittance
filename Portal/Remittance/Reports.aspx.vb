Imports System.Data.SqlClient
Imports System.Data
Partial Class Reports
    Inherits System.Web.UI.Page

    Private Con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("NCRMoneyFerConnection").ConnectionString)
    Private app_base_dir As String = System.AppDomain.CurrentDomain.BaseDirectory
    Private Com As New SqlCommand("", Con)
    Private dr As SqlDataReader
    Private DT As DataTable
    Private SubDT As DataTable
    Private CCode As String = ""
    Private Bcode As String = ""
    Private StrFrom As String = ""
    Private Strto As String = ""
    Private State As StateObj
    Private UserPerm As Permissions
    Public MainFun As New General
    Public Connection As String = ConfigurationManager.ConnectionStrings("NCRMoneyFerConnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If (Session("Status") = "InValid_user" Or Session("Status") = Nothing) Then
        '    Response.Redirect("Login.aspx")
        'End If
        If (IsPostBack <> True) Then
            'Try
            '    UserPerm = New Permissions()
            '    UserPerm = Session("Perm")
            '    If (UserPerm.Reports <> "True") Then
            '        Response.Redirect("Login.aspx")
            '    End If
            'Catch ex As Exception
            '    Response.Redirect("Login.aspx")
            'End Try
            Dim BankToSelect As String = ""
           
            Try
                Con.Open()
            Catch ex As Exception
                Lbl_Status.Text = "Could not connect to the database."
                Lbl_Status.Visible = True
            End Try

            For i As Integer = 0 To 23
                If ((i / 10) = 0) Then
                    drpd_FRM_HH.Items.Add("0" + i.ToString())
                    drpd_TO_HH.Items.Add("0" + i.ToString())
                Else
                    drpd_FRM_HH.Items.Add(i.ToString())
                    drpd_TO_HH.Items.Add(i.ToString())
                End If
            Next
            'Loadin Minutes
            For i As Integer = 0 To 59
                If ((i / 10) = 0) Then
                    drpd_FRM_MM.Items.Add("0" + i.ToString())
                    drpd_TO_MM.Items.Add("0" + i.ToString())
                Else
                    drpd_FRM_MM.Items.Add(i.ToString())
                    drpd_TO_MM.Items.Add(i.ToString())
                End If
            Next
            drpd_Country.Items.Clear()
            Com.CommandText = " select distinct countryname from country "
            Try
                dr = Com.ExecuteReader()
                While (dr.Read())
                    drpd_Country.Items.Add(dr(0))
                End While
                dr.Close()
            Catch ex As Exception
                Lbl_Status.Text = "Database Error."
                Lbl_Status.Visible = True
            End Try


            DPC_date1.Value = Date.Now().ToString("MM/dd/yyyy")
            DPC_date2.Value = Date.Now().ToString("MM/dd/yyyy")

            drpd_FRM_HH.SelectedIndex = drpd_FRM_HH.Items.IndexOf(drpd_FRM_HH.Items.FindByValue("14"))
            drpd_FRM_MM.SelectedIndex = drpd_FRM_MM.Items.IndexOf(drpd_FRM_MM.Items.FindByValue("00"))

            drpd_TO_HH.SelectedIndex = drpd_TO_HH.Items.IndexOf(drpd_TO_HH.Items.FindByValue("13"))
            drpd_TO_MM.SelectedIndex = drpd_TO_MM.Items.IndexOf(drpd_TO_MM.Items.FindByValue("59"))
            RB_RM.Checked = True
            BankToSelect = LoadBank()
            If (drpd_Bank.Items.Count >= 2) Then
                drpd_Bank.SelectedIndex = drpd_Bank.Items.IndexOf(drpd_Bank.Items.FindByValue(BankToSelect))
                LoadATM()
                If (drpd_ATM.Items.Count >= 2) Then
                    drpd_ATM.SelectedIndex = 0 'drpd_ATM.Items.IndexOf(drpd_ATM.Items.FindByValue("ALL"))
                    'drpd_ATM.Text = "ALL"
                End If
            End If

        Else
            Lbl_Status.Visible = False
        End If

    End Sub

    Public Sub PrepareDT(ByVal x As Integer)
        DT = New DataTable("MyTable")
        DT.Clear()
        DT.Rows.Clear()
        DT.Columns.Clear()
        Dim DT_dr As DataRow
        Dim Sequence As Integer = 0

        If (x = 1) Then
            DT.Columns.Add("TransactionCode", GetType(String))
            DT.Columns.Add("ATMID", GetType(String))
            DT.Columns.Add("RequestType", GetType(String))
            DT.Columns.Add("DepositorMobile", GetType(String))
            DT.Columns.Add("BeneficiaryMobile", GetType(String))
            DT.Columns.Add("Amount", GetType(Decimal))
            DT.Columns.Add("DepositDateTime", GetType(System.DateTime))
            DT.Columns.Add("RecordSeq", GetType(Integer))
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            ''''''''''''''''''''''Loading Data''''''''''''''''''''''''
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            While (dr.Read())
                DT_dr = DT.NewRow()

                DT_dr("TransactionCode") = dr.GetString(0)
                DT_dr("ATMID") = dr.GetString(1)
                DT_dr("RequestType") = dr.GetString(2)
                If (dr.IsDBNull(3)) Then
                    DT_dr("DepositorMobile") = ""
                Else
                    DT_dr("DepositorMobile") = dr.GetString(3)
                End If
                If (dr.IsDBNull(4)) Then
                    DT_dr("BeneficiaryMobile") = ""
                Else
                    DT_dr("BeneficiaryMobile") = dr.GetString(4)
                End If
                If (dr.IsDBNull(5)) Then
                    DT_dr("Amount") = 0
                Else
                    DT_dr("Amount") = dr.GetDecimal(5)
                End If
                If (dr.IsDBNull(6)) Then
                    DT_dr("DepositDateTime") = DBNull.Value
                Else
                    DT_dr("DepositDateTime") = dr.GetDateTime(6)
                End If




                DT_dr("RecordSeq") = Sequence
                Sequence += 1
                DT.Rows.Add(DT_dr)
            End While
            dr.Close()
        ElseIf (x = 2) Then
            Sequence = 0
            DT.Columns.Add("TransactionCode", GetType(String))
            DT.Columns.Add("ATMID", GetType(String))
            DT.Columns.Add("RequestType", GetType(String))
            DT.Columns.Add("DepositorMobile", GetType(String))
            DT.Columns.Add("BeneficiaryMobile", GetType(String))
            DT.Columns.Add("Amount", GetType(Decimal))
            DT.Columns.Add("SMSSendingStatus", GetType(String))
            DT.Columns.Add("SMSSentDateTime", GetType(DateTime))
            DT.Columns.Add("RecordSeq", GetType(Integer))
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            ''''''''''''''''''''''Loading Data''''''''''''''''''''''''
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            While (dr.Read())
                DT_dr = DT.NewRow()

                DT_dr("TransactionCode") = dr.GetString(0)
                DT_dr("ATMID") = dr.GetString(1)
                DT_dr("RequestType") = dr.GetString(2)
                If (dr.IsDBNull(3)) Then
                    DT_dr("DepositorMobile") = ""
                Else
                    DT_dr("DepositorMobile") = dr.GetString(3)
                End If
                If (dr.IsDBNull(4)) Then
                    DT_dr("BeneficiaryMobile") = ""
                Else
                    DT_dr("BeneficiaryMobile") = dr.GetString(4)
                End If
                If (dr.IsDBNull(5)) Then
                    DT_dr("Amount") = 0
                Else
                    DT_dr("Amount") = dr.GetDecimal(5)
                End If
                If (dr.IsDBNull(6)) Then
                    DT_dr("SMSSendingStatus") = DBNull.Value
                Else
                    DT_dr("SMSSendingStatus") = dr.GetString(6)
                End If
                If (dr.IsDBNull(7)) Then
                    DT_dr("SMSSentDateTime") = DBNull.Value
                Else
                    DT_dr("SMSSentDateTime") = dr.GetDateTime(7)
                End If


                DT_dr("RecordSeq") = Sequence
                Sequence += 1
                DT.Rows.Add(DT_dr)
            End While
            dr.Close()
        ElseIf (x = 3) Then
            Sequence = 0
            DT.Columns.Add("TransactionCode", GetType(String))
            DT.Columns.Add("ATMID", GetType(String))
            DT.Columns.Add("RequestType", GetType(String))
            DT.Columns.Add("DepositorMobile", GetType(String))
            DT.Columns.Add("BeneficiaryMobile", GetType(String))
            DT.Columns.Add("Amount", GetType(Decimal))
            DT.Columns.Add("SMSSendingStatus", GetType(String))
            DT.Columns.Add("SMSSentDateTime", GetType(DateTime))
            DT.Columns.Add("DepositStatus", GetType(String))
            DT.Columns.Add("WithdrawalStatus", GetType(String))
            DT.Columns.Add("RecordSeq", GetType(Integer))
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            ''''''''''''''''''''''Loading Data''''''''''''''''''''''''
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            While (dr.Read())
                DT_dr = DT.NewRow()

                DT_dr("TransactionCode") = dr.GetString(0)
                DT_dr("ATMID") = dr.GetString(1)
                DT_dr("RequestType") = dr.GetString(2)
                If (dr.IsDBNull(3)) Then
                    DT_dr("DepositorMobile") = ""
                Else
                    DT_dr("DepositorMobile") = dr.GetString(3)
                End If
                If (dr.IsDBNull(4)) Then
                    DT_dr("BeneficiaryMobile") = ""
                Else
                    DT_dr("BeneficiaryMobile") = dr.GetString(4)
                End If
                If (dr.IsDBNull(5)) Then
                    DT_dr("Amount") = 0
                Else
                    DT_dr("Amount") = dr.GetDecimal(5)
                End If
                If (dr.IsDBNull(6)) Then
                    DT_dr("SMSSendingStatus") = DBNull.Value
                Else
                    DT_dr("SMSSendingStatus") = dr.GetString(6)
                End If
                If (dr.IsDBNull(7)) Then
                    DT_dr("SMSSentDateTime") = DBNull.Value
                Else
                    DT_dr("SMSSentDateTime") = dr.GetDateTime(7)
                End If
                If (dr.IsDBNull(8)) Then
                    DT_dr("DepositStatus") = ""
                Else
                    DT_dr("DepositStatus") = dr.GetString(8)
                End If
                If (dr.IsDBNull(9)) Then
                    DT_dr("WithdrawalStatus") = ""
                Else
                    DT_dr("WithdrawalStatus") = dr.GetString(9)
                End If


                DT_dr("RecordSeq") = Sequence
                Sequence += 1
                DT.Rows.Add(DT_dr)

            End While
            dr.Close()
        ElseIf (x = 4) Then
            Sequence = 0
            DT.Columns.Add("TransactionCode", GetType(String))
            DT.Columns.Add("ATMID", GetType(String))
            DT.Columns.Add("DepositorMobile", GetType(String))
            DT.Columns.Add("BeneficiaryMobile", GetType(String))
            DT.Columns.Add("Amount", GetType(Decimal))
            DT.Columns.Add("RecordSeq", GetType(Integer))
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            ''''''''''''''''''''''Loading Data''''''''''''''''''''''''
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            While (dr.Read())
                DT_dr = DT.NewRow()

                DT_dr("TransactionCode") = dr.GetString(0)
                DT_dr("ATMID") = dr.GetString(1)

                If (dr.IsDBNull(2)) Then
                    DT_dr("DepositorMobile") = ""
                Else
                    DT_dr("DepositorMobile") = dr.GetString(2)
                End If
                If (dr.IsDBNull(3)) Then
                    DT_dr("BeneficiaryMobile") = ""
                Else
                    DT_dr("BeneficiaryMobile") = dr.GetString(3)
                End If
                If (dr.IsDBNull(4)) Then
                    DT_dr("Amount") = 0
                Else
                    DT_dr("Amount") = dr.GetDecimal(4)
                End If

                DT_dr("RecordSeq") = Sequence
                Sequence += 1
                DT.Rows.Add(DT_dr)

            End While
            dr.Close()

        ElseIf (x = 5) Then
            Sequence = 0
            DT.Columns.Add("Action", GetType(String))
            DT.Columns.Add("ActionDateTime", GetType(DateTime))
            DT.Columns.Add("ActionReason", GetType(String))
            DT.Columns.Add("ActionStatus", GetType(String))
            DT.Columns.Add("DispensedNotes", GetType(String))
            DT.Columns.Add("DispensedAmount", GetType(Integer))
            DT.Columns.Add("CommissionAmount", GetType(String))
            DT.Columns.Add("Cassette1", GetType(Integer))
            DT.Columns.Add("Cassette2", GetType(Integer))
            DT.Columns.Add("Cassette3", GetType(Integer))
            DT.Columns.Add("Cassette4", GetType(Integer))
            DT.Columns.Add("CommissionValue", GetType(Integer))
            DT.Columns.Add("CommissionPercent", GetType(Integer))
            DT.Columns.Add("RecordSeq", GetType(Integer))
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            ''''''''''''''''''''''Loading Data''''''''''''''''''''''''
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            While (dr.Read())
                DT_dr = DT.NewRow()

                DT_dr("TransactionCode") = dr.GetString(0)
                DT_dr("ATMID") = dr.GetString(1)
                DT_dr("RequestType") = dr.GetString(2)
                If (dr.IsDBNull(3)) Then
                    DT_dr("DepositorMobile") = ""
                Else
                    DT_dr("DepositorMobile") = dr.GetString(3)
                End If
                If (dr.IsDBNull(4)) Then
                    DT_dr("BeneficiaryMobile") = ""
                Else
                    DT_dr("BeneficiaryMobile") = dr.GetString(4)
                End If
                If (dr.IsDBNull(5)) Then
                    DT_dr("Amount") = 0
                Else
                    DT_dr("Amount") = dr.GetDecimal(5)
                End If
                If (dr.IsDBNull(6)) Then
                    DT_dr("SMSSendingStatus") = DBNull.Value
                Else
                    DT_dr("SMSSendingStatus") = dr.GetString(6)
                End If
                If (dr.IsDBNull(7)) Then
                    DT_dr("SMSSentDateTime") = DBNull.Value
                Else
                    DT_dr("SMSSentDateTime") = dr.GetDateTime(7)
                End If


                DT_dr("RecordSeq") = Sequence
                Sequence += 1
                DT.Rows.Add(DT_dr)
            End While
            dr.Close()
        End If
    End Sub

    Protected Sub btn_Search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Search.Click

        Dim strstr As String
        Dim Ret As Boolean
        Dim ATMstr As String = ""
        Dim MyReturn As Boolean
        Dim Branch As String

        State = New StateObj

        If (RB_CR.Checked) Then
            Try

                If (drpd_ATM.SelectedItem.Text = "ALL") Then
                    ATMstr = "%"
                Else
                    ATMstr = drpd_ATM.SelectedItem.ToString()
                End If


                Dim cmd As String = "select T.TransactionCode,T.DepositStatus ,T.WithdrawalStatus ,T.CancelStatus ,T.SMSSendingStatus ,T.WSendingStatus ,T.ResendSMSFlag,Amount ,T.ResendSMSDateTime, T.atmid as ATMName, T.DepositDateTime, T.WithdrawalDateTime, T.DepositorMobile, T.BeneficiaryMobile, T.PaymentType, T.DepositorID, ( Select count(*) from TransactionKeyCheckTrials where TransactionKeyCheckTrials.TransactionCode = T.TransactionCode and TrialFlag = 0 ) as 'KeyTrials'" & _
                                " from transactions T " & _
                                " where T.countrycode =( select countrycode from country where countryname='" & drpd_Country.SelectedItem.ToString() & "' ) and T.bankcode=(select bankcode from bank where bankname='" & drpd_Bank.SelectedItem.ToString() & "') and T.atmid like '" & ATMstr & "' and "

                Select Case drpdl_Dstatus.SelectedItem.Text
                    Case "Authorized"
                        cmd = cmd & "Depositstatus='Authorized'  and "
                    Case "Confirmed"
                        cmd = cmd & "Depositstatus='CONFIRMED' and "
                    Case "Canceled"
                        cmd = cmd & "Depositstatus='CANCELED' and "
                    Case "Expired"
                        cmd = cmd & "Depositstatus='EXPIRED' and "
                    Case "Held"
                        cmd = cmd & "Depositstatus='HOLD' and "
                    Case Else

                End Select

                Select Case drpdl_Wstatus0.SelectedItem.Text
                    Case "Authorized"
                        cmd = cmd & "withdrawalstatus='Authorized' and "
                    Case "Confirmed"
                        cmd = cmd & "withdrawalstatus='CONFIRMED' and "
                    Case "Canceled"
                        cmd = cmd & "withdrawalstatus='CANCELED'  and "
                    Case "Expired"
                        cmd = cmd & " withdrawalstatus='EXPIRED'  and "
                    Case "Held"
                        cmd = cmd & "withdrawalstatus='HOLD' and "
                    Case Else

                End Select

                Select Case Drpd_Amount.SelectedItem.Text
                    Case "Equal"
                        cmd = cmd & "Amount ='" & TXT_AMT.Text & "'  and "
                    Case "Less Than"
                        cmd = cmd & "Amount <'" & TXT_AMT.Text & "' and "
                    Case "Larger Than"
                        cmd = cmd & "Amount >'" & TXT_AMT.Text & "' and "
                    Case Else

                End Select

                Select Case drpdl_smsstatus.SelectedItem.Text
                    Case "Sent"
                        cmd = cmd & "SMSSendingStatus='Sent' and "
                    Case "UnSent"
                        cmd = cmd & "SMSSendingStatus is null and "
                    Case Else

                End Select

                Select Case drpd_TRXcode.SelectedItem.Text
                    Case "Start With"
                        cmd = cmd & "T.transactioncode like '" & txt_TRX_Code.Text & "%' and "
                    Case "Part Of"
                        cmd = cmd & "T.transactioncode like '%" & txt_TRX_Code.Text & "%' and "
                    Case "Ends With"
                        cmd = cmd & "T.transactioncode like '%" & txt_TRX_Code.Text & "' and "
                    Case "Exact"
                        cmd = cmd & "T.transactioncode = '" & txt_TRX_Code.Text & "' and "
                    Case Else

                End Select


                Ret = GetMobileNumbers(TXT_ID.Text, Drpd_ID.SelectedItem.Text, strstr)

                If Ret = True Then
                    cmd = cmd & "T.DepositorMobile in " & strstr & "and "
                End If

                Select Case drpd_TRXSeq.SelectedItem.Text
                    Case "Starts With"
                        cmd = cmd & "T.ATMTrxSequence like '" & txt_TRXSeq.Text & "%' and "
                    Case "Part Of"
                        cmd = cmd & "T.ATMTrxSequence like '%" & txt_TRXSeq.Text & "%' and "
                    Case "Ends With"
                        cmd = cmd & "T.ATMTrxSequence like '%" & txt_TRXSeq.Text & "' and "
                    Case "Exact"
                        cmd = cmd & "T.ATMTrxSequence = '" & txt_TRXSeq.Text & "' and "
                    Case Else

                End Select

                Select Case drpd_BM.SelectedItem.Text
                    Case "Starts With"
                        cmd = cmd & "BeneficiaryMobile like '" & txt_BM.Text & "%' and "
                    Case "Part Of"
                        cmd = cmd & "BeneficiaryMobile like '%" & txt_BM.Text & "%' and "
                    Case "Ends With"
                        cmd = cmd & "BeneficiaryMobile like '%" & txt_BM.Text & "' and "
                    Case "Exact"
                        cmd = cmd & "BeneficiaryMobile = '" & txt_BM.Text & "' and "
                    Case Else

                End Select

                Select Case drpd_DM.SelectedItem.Text
                    Case "Starts With"
                        cmd = cmd & "DepositorMobile like '" & txt_DM.Text & "%' and "
                    Case "Part Of"
                        cmd = cmd & "DepositorMobile like '%" & txt_DM.Text & "%' and "
                    Case "Ends With"
                        cmd = cmd & "DepositorMobile like '%" & txt_DM.Text & "' and "
                    Case "Exact"
                        cmd = cmd & "DepositorMobile = '" & txt_DM.Text & "' and "
                    Case Else

                End Select

                If (DPC_date1.Value = "" Or DPC_date2.Value = "") Then
                    Lbl_Status.Text = "Please select a time frame"
                    Lbl_Status.Visible = True
                    Return
                Else
                    StrFrom = DPC_date1.Value & " " & drpd_FRM_HH.SelectedItem.ToString() & ":" & drpd_FRM_MM.SelectedItem.ToString()
                    Strto = DPC_date2.Value & " " & drpd_TO_HH.SelectedItem.ToString() & ":" & drpd_TO_MM.SelectedItem.ToString()
                    cmd = cmd & " ((WithdrawalDateTime between '" & StrFrom & "' and '" & Strto & "') OR (depositdatetime between '" & StrFrom & "' and '" & Strto & "'))"
                End If



                'MyReturn = MainFun.ConnectToDatabase(cmd, Connection, DT)
                'If (MyReturn) Then

                'Session.Add("9", DT)
                State.ReportName = "Reports\Custom.rpt"
                State.Hasdetails = True
                State.QueryString = cmd
                State.DetailsQuery = "select *,(select requesttypeDescription from requesttype where requesttypecode = transactionnestedactions.Action) as 'ActionDescription' from transactionnestedactions"
                State.DFrom = StrFrom
                State.DTo = Strto
                State.Title = "Transactions For ATM:" & drpd_ATM.SelectedItem.Text
                State.ATM = ""
                Session.Add("Obj", State)
                Response.Redirect("FinalRPT.aspx")
                Return
                'End If

            Catch ex As Exception
                MainFun.loglog("Unknown error:" & ex.ToString(), True)
                Lbl_Status.Text = "Unknown Error."
                Lbl_Status.Visible = True
            End Try
        ElseIf (RB_RM.Checked) Then

            Dim selected_ATM As String = ""
            Dim State As New StateObj
            Try
                If (drpd_RPT_Type.SelectedValue = 7 Or drpd_RPT_Type.SelectedValue = 26 Or drpd_RPT_Type.SelectedValue = 12 Or drpd_RPT_Type.SelectedValue = 13 Or drpd_RPT_Type.SelectedValue = 14 Or drpd_RPT_Type.SelectedValue = 18 Or drpd_RPT_Type.SelectedValue = 19 Or drpd_RPT_Type.SelectedValue = 21) Then
                    Exit Try
                End If
                If (drpd_ATM.SelectedItem.Text = "ALL") Then
                    selected_ATM = "%"
                Else
                    selected_ATM = drpd_ATM.SelectedItem.Text
                End If
            Catch ex As Exception
                Lbl_Status.Text = "Please Select An ATM."
                Lbl_Status.Visible = True
                Return
            End Try
            State = New StateObj



            If (DPC_date1.Value = "" Or DPC_date2.Value = "") Then
                Lbl_Status.Text = "Please select a time frame"
                Lbl_Status.Visible = True
                Return
            Else
                StrFrom = DPC_date1.Value & " " & drpd_FRM_HH.SelectedItem.ToString() & ":" & drpd_FRM_MM.SelectedItem.ToString()
                Strto = DPC_date2.Value & " " & drpd_TO_HH.SelectedItem.ToString() & ":" & drpd_TO_MM.SelectedItem.ToString()
                'Com.Connection = Con
                'Con.Open()
            End If
            If (drpd_RPT_Type.SelectedValue = 5) Then 'Blocked Transactions
                State.ReportName = "Reports\Blocked.rpt"
                State.Hasdetails = True
                State.DetailsQuery = " Select * from TransactionNestedActions "
                State.QueryString = "select T.*,(select RequestTypeDescription from requesttype where RequestTypeCode=T.RequestType)as RequestType,ATM.ATMName,TN.CommissionAmount,TN.DispensedAmount" & _
                                    " FROM Transactions T inner join TransactionNestedActions TN on T.transactioncode=TN.transactioncode inner join ATM ON TN.CountryCode = ATM.CountryCode AND TN.BankCode = ATM.BankCode AND TN.ATMId = ATM.ATMId " & _
                                    " where T.atmid like '" & selected_ATM & "' and T.depositstatus='CONFIRMED' and T.withdrawalstatus='AUTHORIZED' and TN.[action]='02'and TN.actionstatus='AUTHORIZED' and  " & _
                                    " DATEDIFF(minute,T.depositdatetime,getdate()) > 15 "
                State.DFrom = StrFrom
                State.DTo = Strto
                State.Title = "Blocked Transactions ATM:" & drpd_ATM.SelectedItem.Text
                State.ATM = selected_ATM
                Session.Add("Obj", State)
                Response.Redirect("FinalRPT.aspx")
            ElseIf (drpd_RPT_Type.SelectedValue = 1) Then 'Confirmed Withdrawal Transactions
                State.ReportName = "Reports\CWithdrawals.rpt"
                State.Hasdetails = True
                State.DetailsQuery = " Select * from TransactionNestedActions "
                State.QueryString = "SELECT T.transactioncode,Tn.atmid,T.DepositorMobile,T.BeneficiaryMobile,T.Amount,T.SMSSendingStatus,T.SMSSentDateTime, ATM.ATMName,T.DepositDateTime,T.WithdrawalDateTime, TN.DispensedAmount" & _
                                    " FROM Transactions T inner join TransactionNestedActions TN on T.transactioncode=TN.transactioncode inner join ATM ON TN.CountryCode = ATM.CountryCode AND TN.BankCode = ATM.BankCode AND TN.ATMId = ATM.ATMId " & _
                                     " where withdrawalstatus ='CONFIRMED' and tn.ActionStatus = 'CONFIRMED' and  TN.[action]='12'and TN.actionstatus='CONFIRMED' and Tn.atmid like '" & selected_ATM & "' and TN.ActionDateTime between '" & StrFrom & "' and '" & Strto & "' "
                State.DFrom = StrFrom
                State.DTo = Strto
                State.Title = "Confirmed Withdrawal Transactions ATM:" & drpd_ATM.SelectedItem.Text
                State.ATM = selected_ATM
                Session.Add("Obj", State)
                Response.Redirect("FinalRPT.aspx")
            ElseIf (drpd_RPT_Type.SelectedValue = 2) Then 'Redemptions
                State.ReportName = "Reports\CRedemptions.rpt"
                State.Hasdetails = True
                State.DetailsQuery = " Select * from TransactionNestedActions "
                'State.QueryString = "select T.*,TN.ATMID,TN.CommissionAmount,TN.DispensedAmount ,ATM.ATMName" & _
                '                    " FROM Transactions T inner join TransactionNestedActions TN on T.transactioncode=TN.transactioncode inner join ATM ON TN.CountryCode = ATM.CountryCode AND TN.BankCode = ATM.BankCode AND TN.ATMId = ATM.ATMId " & _
                '                    " where T.depositstatus='EXPIRED' and T.withdrawalstatus='CONFIRMED'   and TN.[action]='17'and TN.actionstatus='CONFIRMED' and T.atmid like '" & selected_ATM & "' and TN.ActionDatetime between '" & StrFrom & "' and '" & Strto & "' "


                State.QueryString = "SELECT T.transactioncode,Tn.atmid,T.DepositorMobile,T.BeneficiaryMobile,T.Amount,T.SMSSendingStatus,T.SMSSentDateTime, ATM.ATMName,T.DepositDateTime,T.WithdrawalDateTime, TN.DispensedAmount" & _
                                    " FROM Transactions T inner join TransactionNestedActions TN on T.transactioncode=TN.transactioncode inner join ATM ON TN.CountryCode = ATM.CountryCode AND TN.BankCode = ATM.BankCode AND TN.ATMId = ATM.ATMId " & _
                                    " where T.depositstatus='EXPIRED' and T.withdrawalstatus='CONFIRMED'   and TN.[action]='17'and TN.actionstatus='CONFIRMED' and T.atmid like '" & selected_ATM & "' and TN.ActionDatetime between '" & StrFrom & "' and '" & Strto & "' "


                State.DFrom = StrFrom
                State.DTo = Strto
                State.Title = "Redeemed Transactions ATM:" & drpd_ATM.SelectedItem.Text
                State.ATM = selected_ATM
                Session.Add("Obj", State)
                Response.Redirect("FinalRPT.aspx")
            ElseIf (drpd_RPT_Type.SelectedValue = 4) Then 'Expired
                State.ReportName = "Reports\Expired.rpt"
                State.QueryString = "select T.*,ATM.ATMName" & _
                                    " FROM Transactions T inner join ATM ON T.CountryCode = ATM.CountryCode AND T.BankCode = ATM.BankCode AND T.ATMId = ATM.ATMId  " & _
                                    " where T.atmid like '" & selected_ATM & "' and T.depositstatus='EXPIRED' and T.withdrawalstatus='EXPIRED' and  T.depositdatetime between '" & StrFrom & "' and '" & Strto & "' "
                State.Hasdetails = True
                State.DetailsQuery = " Select * from TransactionNestedActions "
                State.DFrom = StrFrom
                State.DTo = Strto
                State.Title = "Expired Transactions ATM:" & drpd_ATM.SelectedItem.Text
                State.ATM = selected_ATM
                Session.Add("Obj", State)
                Response.Redirect("FinalRPT.aspx")
            ElseIf (drpd_RPT_Type.SelectedValue = 6) Then 'Confirmed Deposits
                State.ReportName = "Reports\CDeposit.rpt"

                'Before Aly Requirments (all deposits regardless withdrawal)
                'State.QueryString = "SELECT transactions.transactioncode,transactions.atmid,transactions.DepositorMobile,transactions.BeneficiaryMobile,transactions.SMSSendingStatus,transactions.SMSSentDateTime, ATM.ATMName,transactions.ReactivationCounter,transactions.DepositDateTime,transactions.WithdrawalDateTime, transactions.PaymentType,transactions.amount" & _
                '                    " FROM Transactions transactions inner join TransactionNestedActions TN on transactions.transactioncode=TN.transactioncode inner join ATM ON TN.CountryCode = ATM.CountryCode AND TN.BankCode = ATM.BankCode AND TN.ATMId = ATM.ATMId   " & _
                '                    " where depositstatus='CONFIRMED'and ( withdrawalstatus is null or withdrawalstatus= 'CANCELED')   and transactions.atmid like '" & selected_ATM & "' and TN.[action]='11'and TN.actionstatus='CONFIRMED'and TN.ActionDateTime between '" & StrFrom & "' and '" & Strto & "' "

                State.QueryString = "select * from DepositView where atmid like '" & selected_ATM & "' and ActionDateTime between convert(datetime,'" & StrFrom & "',120) and convert(datetime,'" & Strto & "',120) "

                State.DFrom = StrFrom
                State.DTo = Strto
                State.Title = "Confirmed Deposits Transactions ATM:" & drpd_ATM.SelectedItem.Text
                State.ATM = selected_ATM
                Session.Add("Obj", State)
                Response.Redirect("FinalRPT.aspx")
            ElseIf (drpd_RPT_Type.SelectedValue = 9) Then
                State.DFrom = StrFrom
                State.DTo = Strto
                State.Title = "Test ATM:" & drpd_ATM.SelectedItem.Text
                State.ATM = selected_ATM
                Session.Add("Obj", State)
                Response.Redirect("RPT.aspx?9")
            ElseIf (drpd_RPT_Type.SelectedValue = 10) Then
                State.ReportName = "Reports\w30000.rpt"
                State.QueryString = "select BeneficiaryMobile,sum(Amount) as Test" & _
                                    " from transactions " & _
                                    " where  depositdatetime between '" & StrFrom & "' and '" & Strto & "' " & _
                                    " group by BeneficiaryMobile " & _
                                    " having sum(Amount) > 30000 "

                State.DFrom = StrFrom
                State.DTo = Strto
                State.Title = "Test ATM:" & drpd_ATM.SelectedItem.Text
                State.ATM = selected_ATM
                Session.Add("Obj", State)
                Response.Redirect("FinalRPT.aspx")
            ElseIf (drpd_RPT_Type.SelectedValue = 11) Then
                State.ReportName = "Reports\Dep30000.rpt"
                State.QueryString = "select DepositorMobile,sum(Amount) as Test" & _
                                    " from transactions " & _
                                    " where  depositdatetime between '" & StrFrom & "' and '" & Strto & "' " & _
                                    " group by DepositorMobile " & _
                                    " having sum(Amount) > 30000 "

                State.DFrom = StrFrom
                State.DTo = Strto
                State.Title = "Test ATM:" & drpd_ATM.SelectedItem.Text
                State.ATM = selected_ATM
                Session.Add("Obj", State)
                Response.Redirect("FinalRPT.aspx")
            ElseIf (drpd_RPT_Type.SelectedValue = 7) Then
                State.ReportName = "Reports\AuditReport.rpt"
                UserPerm = Session("Perm")
                If (UserPerm.Administration = True) Then
                    Branch = "%"
                Else
                    Branch = Session("Branch")
                End If
                State.QueryString = "select * from useractions where branch like '" & Branch & "' and actiondatetime between '" & StrFrom & "' and '" & Strto & "'" & _
                                    " order by actiondatetime desc "
                State.DFrom = StrFrom
                State.DTo = Strto
                State.Title = "Test ATM" '& drpd_ATM.SelectedItem.Text
                State.ATM = selected_ATM
                'PrepareDT(2)
                'Session.Add("3", DT)
                Session.Add("Obj", State)
                Response.Redirect("FinalRPT.aspx")
            ElseIf (drpd_RPT_Type.SelectedValue = 12) Then
                State.ReportName = "Reports\BlockedDepositors.rpt"
                State.QueryString = " select * from blockedcustomers " & _
                                    " where DepositorOrBeneficiary=0 and unblocked=0 and blockdatetime between '" & StrFrom & "' and '" & Strto & "' "
                State.DFrom = StrFrom
                State.DTo = Strto
                State.Title = "Test ATM" '& drpd_ATM.SelectedItem.Text
                State.ATM = selected_ATM
                Session.Add("Obj", State)
                Response.Redirect("FinalRPT.aspx")
            ElseIf (drpd_RPT_Type.SelectedValue = 13) Then
                State.ReportName = "Reports\BlockedBeneficiaries.rpt"

                State.QueryString = "select * from blockedcustomers " & _
                                    " where DepositorOrBeneficiary=1 and unblocked=0 and blockdatetime between '" & StrFrom & "' and '" & Strto & "' "
                State.DFrom = StrFrom
                State.DTo = Strto
                State.Title = "Test ATM" '& drpd_ATM.SelectedItem.Text
                State.ATM = selected_ATM
                Session.Add("Obj", State)
                Response.Redirect("FinalRPT.aspx")
            ElseIf (drpd_RPT_Type.SelectedValue = 14) Then
                State.ReportName = "Reports\BlockingHistory.rpt"
                State.QueryString = "select * from blockedcustomers " & _
                                    " where unblocked=1 and blockdatetime between '" & StrFrom & "' and '" & Strto & "' "
                State.DFrom = StrFrom
                State.DTo = Strto
                State.Title = "Test ATM" '& drpd_ATM.SelectedItem.Text
                State.ATM = selected_ATM
                Session.Add("Obj", State)
                Response.Redirect("FinalRPT.aspx")
            ElseIf (drpd_RPT_Type.SelectedValue = 15) Then
                State.ReportName = "Reports\InvalidKeyTrials.rpt"
                State.QueryString = "select T.*,ATM.ATMName,(select MaximumKeyTrials from Bank B where B.countrycode = T.countrycode and B.BankCode=T.BankCode) as KeyTrials,(select Count(*) from TransactionKeyCheckTrials TKCT where TKCT.TransactionCode=T.TransactionCode and TrialFlag=0) as TransactionTrials" & _
                                    " FROM Transactions T inner join TransactionNestedActions TN on T.transactioncode=TN.transactioncode inner join ATM ON TN.CountryCode = ATM.CountryCode AND TN.BankCode = ATM.BankCode AND TN.ATMId = ATM.ATMId " & _
                                    " where T.atmid like '" & selected_ATM & "' and (T.DepositStatus='CONFIRMED' and T.WithdrawalStatus is null) OR ( T.DepositStatus = 'EXPIRED' and T.WithdrawalStatus = 'EXPIRED' ) and  T.depositdatetime between '" & StrFrom & "' and '" & Strto & "' "
                State.DFrom = StrFrom
                State.DTo = Strto
                State.Title = "Transactions with invalid Trials ATM:" & drpd_ATM.SelectedItem.Text
                State.ATM = selected_ATM
                Session.Add("Obj", State)
                Response.Redirect("FinalRPT.aspx")
            ElseIf (drpd_RPT_Type.SelectedValue = 18) Then
                State.ReportName = "Reports\Users.rpt"
                State.QueryString = "select users.userid,users.username,users.branch,users.ATM_ID ,groups.name " & _
                                    " from users inner join groups on users.group_id = groups.ID "
                State.DFrom = StrFrom
                State.DTo = Strto
                State.Title = "Users Report" '& drpd_ATM.SelectedItem.Text
                State.ATM = selected_ATM
                Session.Add("Obj", State)
                Response.Redirect("FinalRPT.aspx")
            ElseIf (drpd_RPT_Type.SelectedValue = 19) Then
                State.ReportName = "Reports\Terminals.rpt"
                State.QueryString = "select * from ATM "
                State.DFrom = StrFrom
                State.DTo = Strto
                State.Title = "Terminals Report" '& drpd_ATM.SelectedItem.Text
                State.ATM = selected_ATM
                Session.Add("Obj", State)
                Response.Redirect("FinalRPT.aspx")
            ElseIf (drpd_RPT_Type.SelectedValue = 20) Then
                State.ReportName = "Reports\Statistical.rpt"
                State.QueryString = "select * from Transactions "
                State.DFrom = StrFrom
                State.DTo = Strto
                State.Title = "Statistical Report" '& drpd_ATM.SelectedItem.Text
                State.ATM = selected_ATM
                Session.Add("Obj", State)
                Response.Redirect("FinalRPT.aspx")
            ElseIf (drpd_RPT_Type.SelectedValue = 21) Then
                State.ReportName = "Reports\RegisteredCustomers.rpt"
                State.QueryString = "select * from RegisteredCustomer "
                State.DFrom = StrFrom
                State.DTo = Strto
                State.Title = "Registered Customer Report" '& drpd_ATM.SelectedItem.Text
                State.ATM = selected_ATM
                Session.Add("Obj", State)
                Response.Redirect("FinalRPT.aspx")
            ElseIf (drpd_RPT_Type.SelectedValue = 22) Then
                State.ReportName = "Reports\Detailed.rpt"
                State.QueryString = "select * from detailedreport where  atmid like '" & selected_ATM & "' and ActionDateTime between '" & StrFrom & "' and '" & Strto & "' "
                State.DFrom = StrFrom
                State.DTo = Strto
                State.Title = "Detailed Report" '& drpd_ATM.SelectedItem.Text
                State.ATM = selected_ATM
                Session.Add("Obj", State)
                Response.Redirect("FinalRPT.aspx")
            ElseIf (drpd_RPT_Type.SelectedValue = 23) Then
                State.ReportName = "Reports\ATMTotals.rpt"
                'State.QueryString = "select ATMID,action, Sum(amount) as TotalDeposits, Sum(DispensedAmount) as TotalDispense, sum(CommissionAmount)as Totalcommission from ATMTotal where ActionDateTime between convert(Datetime,'" & StrFrom & "',120) and convert(Datetime,'" & Strto & "',120) group by atmid,action"
                State.QueryString = "select * from ATMTotal where atmid like '" & selected_ATM & "' and ActionDateTime between convert(Datetime,'" & StrFrom & "',120) and convert(Datetime,'" & Strto & "',120)"
                State.DFrom = StrFrom
                State.DTo = Strto
                State.Title = "ATM Totals" '& drpd_ATM.SelectedItem.Text
                State.ATM = selected_ATM
                Session.Add("Obj", State)
                Response.Redirect("FinalRPT.aspx")
            ElseIf (drpd_RPT_Type.SelectedValue = 24) Then 'Confirmed Deposits
                State.ReportName = "Reports\undispensed.rpt"
                'State.QueryString = "SELECT transactions.transactioncode,transactions.atmid,(select RequestTypeDescription from requesttype where RequestTypeCode=transactions.RequestType)as RequestType,transactions.DepositorMobile,transactions.BeneficiaryMobile,transactions.SMSSendingStatus,transactions.SMSSentDateTime, ATM.ATMName,transactions.ReactivationCounter,transactions.DepositDateTime,transactions.WithdrawalDateTime" & _
                '                    " FROM Transactions transactions inner join TransactionNestedActions TN on transactions.transactioncode=TN.transactioncode inner join ATM ON TN.CountryCode = ATM.CountryCode AND TN.BankCode = ATM.BankCode AND TN.ATMId = ATM.ATMId   " & _
                '                    " where ( depositstatus='CONFIRMED' or depositstatus='EXPIRED')and ( withdrawalstatus is null or withdrawalstatus= 'CANCELED')   and transactions.atmid like '" & selected_ATM & "' and TN.[action]='11'and TN.actionstatus='CONFIRMED'and TN.ActionDateTime between '" & StrFrom & "' and '" & Strto & "' "

                State.QueryString = " select T.TransactionCode,T.ATMid,T.Amount,T.DepositStatus,T.withdrawalstatus,T.DepositDateTime, T.DepositorMobile, T.BeneficiaryMobile, TN.DispensedAmount, TN.CommissionAmount " & _
                                    " from Transactions T inner join TransactionNestedActions TN on T.transactioncode=TN.transactioncode inner join ATM ON TN.CountryCode = ATM.CountryCode AND TN.BankCode = ATM.BankCode AND TN.ATMId = ATM.ATMId    " & _
                                    " where ( depositstatus='CONFIRMED' or depositstatus='EXPIRED')and ( withdrawalstatus is null or withdrawalstatus = 'EXPIRED' or withdrawalstatus = 'CANCELED') and TN.ActionDateTime between '" & StrFrom & "' and '" & Strto & "'   and T.atmid like '" & selected_ATM & "' and DispensedAmount <> 0" & _
                                    " group by T.TransactionCode,T.ATMid,T.Amount,T.DepositStatus,T.withdrawalstatus,T.DepositDateTime, T.DepositorMobile, T.BeneficiaryMobile,TN.DispensedAmount, TN.CommissionAmount "


                State.DFrom = StrFrom
                State.DTo = Strto
                State.Title = "Undispensed Transactions ATM:" & drpd_ATM.SelectedItem.Text
                State.ATM = selected_ATM
                Session.Add("Obj", State)
                Response.Redirect("FinalRPT.aspx")
            ElseIf (drpd_RPT_Type.SelectedValue = 25) Then
                State.ReportName = "Reports\AllDispense.rpt"
                'State.QueryString = "select ATMID,action, Sum(amount) as TotalDeposits, Sum(DispensedAmount) as TotalDispense, sum(CommissionAmount)as Totalcommission from ATMTotal where ActionDateTime between convert(Datetime,'" & StrFrom & "',120) and convert(Datetime,'" & Strto & "',120) group by atmid,action"
                State.QueryString = "select * from  ATMTotal where ActionDateTime between convert(Datetime,'" & StrFrom & "',120) and convert(Datetime,'" & Strto & "',120) and [Action]  in ('12','17','02') and atmid like '" & selected_ATM & "' "
                State.DFrom = StrFrom
                State.DTo = Strto
                State.Title = "All Dispensed Transactions" '& drpd_ATM.SelectedItem.Text
                State.ATM = selected_ATM
                Session.Add("Obj", State)
                Response.Redirect("FinalRPT.aspx")
            ElseIf drpd_RPT_Type.SelectedValue = 26 Then
                State.ReportName = "Reports\TransBalance.rpt"
                'State.QueryString = "select ATMID,action, Sum(amount) as TotalDeposits, Sum(DispensedAmount) as TotalDispense, sum(CommissionAmount)as Totalcommission from ATMTotal where ActionDateTime between convert(Datetime,'" & StrFrom & "',120) and convert(Datetime,'" & Strto & "',120) group by atmid,action"
                State.QueryString = ""
                State.DFrom = StrFrom
                State.DTo = Strto
                State.Title = "Transaction Deposit Balance Report" '& drpd_ATM.SelectedItem.Text
                State.ATM = ""
                Session.Add("Obj", State)
                Response.Redirect("TransBalanceRPTView.aspx")
            End If
        Else
            Lbl_Status.Visible = True
            Lbl_Status.Text = "Please select a function."
        End If
    End Sub
    Public Function LoadBank() As String
        Dim ret As String = ""
        Com = New SqlCommand()
        Try
            Con.Open()
        Catch ex As Exception
            'Lbl_Status.Text = "DB Error."
            'Lbl_Status.Visible = True
            'Return ""
        End Try

        Com.Connection = Con
        drpd_Bank.Items.Clear()
        Try
            Com.CommandText = "select bankname from bank where countrycode=( select countrycode from country where countryname='" & drpd_Country.SelectedItem.ToString() & "' )"
            drpd_Bank.Items.Add("None")
            dr = Com.ExecuteReader()
            While (dr.Read())
                drpd_Bank.Items.Add(dr(0))
                ret = dr(0)
            End While

            dr.Close()
        Catch ex As Exception
            Lbl_Status.Text = "Error while loading Banks."
            Lbl_Status.Visible = True
            Return ""
        End Try
        Return ret
    End Function
    Public Sub LoadATM()
        Dim ADMPerm As New Permissions
        Dim LoginMethod As String = System.Configuration.ConfigurationManager.AppSettings("LoginMethod")
        Com = New SqlCommand()
        Try
            Con.Open()
        Catch ex As Exception
            'Lbl_Status.Text = "DB Error."
            'Lbl_Status.Visible = True
            'Return
        End Try
        Com.Connection = Con
        drpd_ATM.Items.Clear()
        Try
            ADMPerm = Session("Perm")
            If (ADMPerm.Administration <> "True" And Session("AllATMs") = "False") Then
                If LoginMethod = "1" Then
                    Com.CommandText = "select atmid from atm where substring(TerminalID,2,3)='" & Session("Branch") & "'  and bankcode = (select bankcode from bank where bankname='" & drpd_Bank.SelectedItem.ToString() & "')"
                ElseIf LoginMethod = "2" Then
                    Com.CommandText = "select atmid from atm where substring(atmid,2,3)='" & Session("Branch") & "' and Isteller='" & Convert.ToInt32(CB_IsTeller.Checked) & "' and bankcode = (select bankcode from bank where bankname='" & drpd_Bank.SelectedItem.ToString() & "')"
                End If

            Else
                Com.CommandText = "select atmid from atm where  bankcode = (select bankcode from bank where bankname='" & drpd_Bank.SelectedItem.ToString() & "')"
                drpd_ATM.Items.Add("ALL")
                drpd_ATM.Items.Add("Don`t Care")

            End If

            
            dr = Com.ExecuteReader()
            While (dr.Read())
                drpd_ATM.Items.Add(dr(0))
            End While
            dr.Close()
        Catch ex As Exception
            Lbl_Status.Visible = True
            Lbl_Status.Text = "Error while Loading ATMs."
            Return
        End Try
        Session.Add("BCode", Bcode)
    End Sub
    Public Sub PrepareDT()
        Dim DTR As DataRow
        Dim Sequence As Integer = 0
        DT = New DataTable

        DT.Columns.Add("TransactionCode", GetType(String))
        DT.Columns.Add("DepositStatus", GetType(String))
        DT.Columns.Add("WithdrawalStatus", GetType(String))
        DT.Columns.Add("CancelStatus", GetType(String))
        DT.Columns.Add("SMSSendingStatus", GetType(String))
        DT.Columns.Add("WSendingStatus", GetType(String))
        DT.Columns.Add("ResendSMSFlag", GetType(Boolean))
        DT.Columns.Add("Amount", GetType(Integer))
        DT.Columns.Add("ResendSMSDateTime", GetType(DateTime))
        DT.Columns.Add("CommissionAmount", GetType(Integer))
        DT.Columns.Add("DispensedAmount", GetType(Integer))
        DT.Columns.Add("RecordSeq", GetType(Integer))
        DT.Columns.Add("ATMName", GetType(String))
        DT.Columns.Add("DepositDateTime", GetType(String))
        DT.Columns.Add("WithdrawalDateTime", GetType(String))

        While (dr.Read())
            DTR = DT.NewRow()
            If (IsDBNull(dr("TransactionCode"))) Then
                DTR("TransactionCode") = ""
            Else
                DTR("TransactionCode") = dr("TransactionCode")
            End If

            If (IsDBNull(dr("WithdrawalStatus"))) Then
                DTR("WithdrawalStatus") = ""
            Else
                DTR("WithdrawalStatus") = dr("WithdrawalStatus")
            End If
            If (IsDBNull(dr("CancelStatus"))) Then
                DTR("CancelStatus") = ""
            Else
                DTR("CancelStatus") = dr("CancelStatus")
            End If
            If (IsDBNull(dr("SMSSendingStatus"))) Then
                DTR("SMSSendingStatus") = ""
            Else
                DTR("SMSSendingStatus") = dr("SMSSendingStatus")
            End If

            If (IsDBNull(dr("WSendingStatus"))) Then
                DTR("WSendingStatus") = 0
            Else
                DTR("WSendingStatus") = dr("WSendingStatus")
            End If
            If (IsDBNull(dr("ResendSMSFlag"))) Then
                DTR("ResendSMSFlag") = 0
            Else
                DTR("ResendSMSFlag") = dr("ResendSMSFlag")
            End If

            If (IsDBNull(dr("Amount"))) Then
                DTR("Amount") = 0
            Else
                DTR("Amount") = dr("Amount")
            End If

            If (IsDBNull(dr("ResendSMSDateTime"))) Then
                DTR("ResendSMSDateTime") = DBNull.Value
            Else
                DTR("ResendSMSDateTime") = dr("ResendSMSDateTime")
            End If


            'If (IsDBNull(dr("CommissionAmount"))) Then
            '    DTR("CommissionAmount") = DBNull.Value
            'Else
            '    DTR("CommissionAmount") = dr("CommissionAmount")
            'End If


            'If (IsDBNull(dr("DispensedAmount"))) Then
            '    DTR("DispensedAmount") = DBNull.Value
            'Else
            '    DTR("DispensedAmount") = dr("DispensedAmount")
            'End If


            If (IsDBNull(dr("DepositStatus"))) Then
                DTR("DepositStatus") = ""
            Else
                DTR("DepositStatus") = dr("DepositStatus")
            End If

            If (IsDBNull(dr("ATMName"))) Then
                DTR("ATMName") = ""
            Else
                DTR("ATMName") = dr("ATMName")
            End If

            If (IsDBNull(dr("DepositDateTime"))) Then
                DTR("DepositDateTime") = ""
            Else
                DTR("DepositDateTime") = dr("DepositDateTime")
            End If

            If (IsDBNull(dr("WithdrawalDateTime"))) Then
                DTR("WithdrawalDateTime") = ""
            Else
                DTR("WithdrawalDateTime") = dr("WithdrawalDateTime")
            End If


            'DT.TableName = "Main"
            'DT.WriteXmlSchema("C:\Main.xsd")

            DTR("RecordSeq") = Sequence
            Sequence += 1
            DT.Rows.Add(DTR)
        End While
        dr.Close()
    End Sub
    Protected Sub drpd_Bank_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpd_Bank.SelectedIndexChanged
        LoadATM()
    End Sub

    Protected Sub CB_IsTeller_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CB_IsTeller.CheckedChanged
        LoadATM()
    End Sub

    Protected Sub drpd_Country_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpd_Country.SelectedIndexChanged
        LoadBank()
    End Sub

    Public Function GetMobileNumbers(ByVal ID As String, ByVal Index As String, ByRef StrStr As String) As Boolean

        Dim obj As New General
        Dim str As String

        Dim Ret As Boolean
        Dim Dt As DataTable
        Dim cmd As String
        Select Case Index

            Case "Start With"
                str = "select distinct MobileNumber from Historical_Register where ID  like '" & ID & "%'"
            Case "Part Of"
                str = "select distinct MobileNumber from Historical_Register where ID  like '%" & ID & "%'"

            Case "Ends With"
                str = "select distinct MobileNumber from Historical_Register where ID  like '%" & ID & "'"

            Case "Exact"
                str = "select distinct MobileNumber from Historical_Register where ID = '" & ID & "'"

            Case Else
                Return False
        End Select


        Ret = obj.ConnectToDatabase(str, Connection, Dt)
        If (Ret) Then



            StrStr = "("
            For i = 0 To Dt.Rows.Count - 1
                StrStr += "'"
                StrStr += Dt.Rows(i).Item(0)
                StrStr += "',"
            Next
            'StrStr = Left(StrStr, StrStr.Length - 1) & ")"
            'Return True
            Ret = obj.ConnectToDatabase("select MobileNumber from RegisteredCustomer where ID = '" & ID & "'", Connection, Dt)
            If (Ret) Then
                If (Dt.Rows.Count <= 0) Then
                    StrStr = "('')"
                    Return True
                Else
                    For i = 0 To Dt.Rows.Count - 1
                        StrStr += "'"
                        StrStr += Dt.Rows(i).Item(0)
                        StrStr += "',"
                    Next
                    StrStr = Left(StrStr, StrStr.Length - 1) & ")"
                    Return True
                End If

            Else
                Return False
            End If

        Else
        Return False
        End If

    End Function
End Class

