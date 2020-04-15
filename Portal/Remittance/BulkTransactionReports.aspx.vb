Imports System.Data
Imports System.Data.SqlClient
Partial Class BulkTransactionReports
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
        If (Session("Status") = "InValid_user" Or Session("Status") = Nothing) Then
            Response.Redirect("Login.aspx")
        End If
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
            LoadRemitters()
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

            BankToSelect = LoadBank()
            If (drpd_Bank.Items.Count >= 2) Then
                drpd_Bank.SelectedIndex = drpd_Bank.Items.IndexOf(drpd_Bank.Items.FindByValue(BankToSelect))
                LoadATM()
                If (drpd_ATM.Items.Count >= 2) Then
                    drpd_ATM.SelectedIndex = 0 'drpd_ATM.Items.IndexOf(drpd_ATM.Items.FindByValue("ALL"))
                    'drpd_ATM.Text = "ALL"
                End If
            End If

            DPC_date1.Value = Date.Now().ToString("MM/dd/yyyy")
            DPC_date2.Value = Date.Now().ToString("MM/dd/yyyy")

            drpd_FRM_HH.SelectedIndex = drpd_FRM_HH.Items.IndexOf(drpd_FRM_HH.Items.FindByValue("00"))
            drpd_FRM_MM.SelectedIndex = drpd_FRM_MM.Items.IndexOf(drpd_FRM_MM.Items.FindByValue("00"))

            drpd_TO_HH.SelectedIndex = drpd_TO_HH.Items.IndexOf(drpd_TO_HH.Items.FindByValue("23"))
            drpd_TO_MM.SelectedIndex = drpd_TO_MM.Items.IndexOf(drpd_TO_MM.Items.FindByValue("59"))


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
        Dim ATMstr As String
        Dim MyReturn As Boolean
        Dim Branch As String
        Dim cmd As String = ""
        Dim WhereSTR As String = ""
        State = New StateObj


        If drpdl_Wstatus.SelectedItem.Text.ToLower = "New".ToLower Then
            cmd = "SELECT T.Amount,t.BeneficiaryName,t.NationalID,t.TransactionCode,t.TransactionDateTime,r.RemitterName " &
                " From [dbo].[NewTransactions] T " &
                " inner join Remitters r" &
                " on r.ID=t.RemitterID "
        Else
            cmd = "SELECT T.Amount,t.ATMDateTime,t.ATMId,t.ATMTransactionSequence,t.BeneficiaryName,t.CommissionAmount,t.DispensedAmount,t.DispensedNotes,t.NationalID,t.TransactionCode,t.TransactionDateTime,t.WithdrawalDateTime,t.WithdrawalStatus,r.RemitterName " &
                " From [dbo].[NewTransactions] T " &
                " left outer join ATM A " &
                " On t.ATMId = a.ATMId " &
                " left outer join Remitters r" &
                " on r.ID=t.RemitterID "

        End If

        Select Case drpdl_Wstatus.SelectedItem.Text
            Case "Authorized"
                WhereSTR = WhereSTR & "withdrawalstatus='Authorized' and "
            Case "Confirmed"
                WhereSTR = WhereSTR & "withdrawalstatus='CONFIRMED' and "
            Case "Canceled"
                WhereSTR = WhereSTR & "withdrawalstatus='CANCELED'  and "
            Case "Expired"
                WhereSTR = WhereSTR & " withdrawalstatus='EXPIRED'  and "

            Case Else

        End Select

        Select Case Drpd_Amount.SelectedItem.Text
            Case "Equal"
                WhereSTR = WhereSTR & "Amount =" & TXT_AMT.Text & "  and "
            Case "Less Than"
                WhereSTR = WhereSTR & "Amount <" & TXT_AMT.Text & " and "
            Case "Larger Than"
                WhereSTR = WhereSTR & "Amount >" & TXT_AMT.Text & " and "
            Case Else

        End Select

        Select Case drpd_TRXcode.SelectedItem.Text
            Case "Start With"
                WhereSTR = WhereSTR & "T.transactioncode like '" & txt_TRX_Code.Text & "%' and "
            Case "Part Of"
                WhereSTR = WhereSTR & "T.transactioncode like '%" & txt_TRX_Code.Text & "%' and "
            Case "Ends With"
                WhereSTR = WhereSTR & "T.transactioncode like '%" & txt_TRX_Code.Text & "' and "
            Case "Exact"
                WhereSTR = WhereSTR & "T.transactioncode = '" & txt_TRX_Code.Text & "' and "
            Case Else

        End Select

        Select Case drpd_TRXSeq.SelectedItem.Text
            Case "Starts With"
                WhereSTR = WhereSTR & "T.ATMTransactionSequence like '" & txt_TRXSeq.Text & "%' and "
            Case "Part Of"
                WhereSTR = WhereSTR & "T.ATMTransactionSequence like '%" & txt_TRXSeq.Text & "%' and "
            Case "Ends With"
                WhereSTR = WhereSTR & "T.ATMTransactionSequence like '%" & txt_TRXSeq.Text & "' and "
            Case "Exact"
                WhereSTR = WhereSTR & "T.ATMTransactionSequence = '" & txt_TRXSeq.Text & "' and "
            Case Else

        End Select

        Select Case drpd_NationalID.SelectedItem.Text
            Case "Start With"
                WhereSTR = WhereSTR & "T.NationalID like '" & txt_NationalID.Text & "%' and "
            Case "Part Of"
                WhereSTR = WhereSTR & "T.NationalID like '%" & txt_NationalID.Text & "%' and "
            Case "Ends With"
                WhereSTR = WhereSTR & "T.NationalID like '%" & txt_NationalID.Text & "' and "
            Case "Exact"
                WhereSTR = WhereSTR & "T.NationalID = '" & txt_NationalID.Text & "' and "
            Case Else

        End Select





        If drpdl_Wstatus.SelectedItem.Text.ToLower = "New".ToLower Then

            Dim str = " withdrawalstatus='' or   withdrawalstatus is null and "
            If Not String.IsNullOrEmpty(WhereSTR.Trim) Then
                WhereSTR = " Where " & str & WhereSTR
            Else
                WhereSTR = " Where " & str
            End If

            Dim query = cmd & WhereSTR
            If query.Trim().EndsWith("and") Then
                query = ReplaceLastOccurrence(query.Trim, "and", "")
            End If
            MainFun.loglog("query:" & query, True)
            State.ReportName = "Reports\NewBulkTransactions.rpt"
            State.Hasdetails = False
            State.DetailsQuery = ""
            State.QueryString = query
            State.DFrom = StrFrom
            State.DTo = Strto
            State.Title = "New Bulk Transactions"
            State.ATM = drpd_ATM.SelectedItem.Text
            Session.Add("Obj", State)
            Response.Redirect("NewBulkTransactionsReport.aspx")

        ElseIf drpdl_Wstatus.SelectedItem.Text.ToLower = "expired".ToLower Then

            If (drpd_ATM.SelectedItem.Text = "ALL") Then
                ATMstr = " t.ATMId like '%'"
            Else
                ATMstr = " t.ATMId = '" & drpd_ATM.SelectedItem.ToString() & "'"
            End If

            If (DPC_date1.Value = "" Or DPC_date2.Value = "") Then
                Lbl_Status.Text = "Please select a time frame"
                Lbl_Status.Visible = True
                Return
            Else
                StrFrom = DPC_date1.Value & " " & drpd_FRM_HH.SelectedItem.ToString() & ":" & drpd_FRM_MM.SelectedItem.ToString()
                Strto = DPC_date2.Value & " " & drpd_TO_HH.SelectedItem.ToString() & ":" & drpd_TO_MM.SelectedItem.ToString()
                WhereSTR = WhereSTR & " WithdrawalDateTime between '" & StrFrom & "' and '" & Strto & "' and"
            End If
            If Not String.IsNullOrEmpty(WhereSTR.Trim) Then
                WhereSTR = " Where " & WhereSTR & ATMstr

            End If
            Dim query = cmd & WhereSTR
            If query.Trim().EndsWith("and") Then
                query = ReplaceLastOccurrence(query.Trim, "and", "")
            End If

            MainFun.loglog("query:" & query, True)
            State.ReportName = "Reports\ExpiredTransactions.rpt"
            State.Hasdetails = False
            State.DetailsQuery = ""
            State.QueryString = query
            State.DFrom = StrFrom
            State.DTo = Strto
            State.Title = "Expired Bulk Transactions"
            State.ATM = ""
            Session.Add("Obj", State)
            Response.Redirect("BulkExpiredTransactionsView.aspx")

        ElseIf drpdl_Wstatus.SelectedItem.Text.ToLower = "all".ToLower Then

            If (drpd_ATM.SelectedItem.Text = "ALL") Then
                ATMstr = " ( t.ATMId is null or t.ATMId like '%') "
            Else
                ATMstr = " ( t.ATMId is null or t.ATMId = '" & drpd_ATM.SelectedItem.ToString() & "')"
            End If

            If (DPC_date1.Value = "" Or DPC_date2.Value = "") Then
                Lbl_Status.Text = "Please select a time frame"
                Lbl_Status.Visible = True
                Return
            Else
                StrFrom = DPC_date1.Value & " " & drpd_FRM_HH.SelectedItem.ToString() & ":" & drpd_FRM_MM.SelectedItem.ToString()
                Strto = DPC_date2.Value & " " & drpd_TO_HH.SelectedItem.ToString() & ":" & drpd_TO_MM.SelectedItem.ToString()
                WhereSTR = WhereSTR & " (WithdrawalDateTime is null or ( WithdrawalDateTime between '" & StrFrom & "' and '" & Strto & "')) and"
            End If
            If Not String.IsNullOrEmpty(WhereSTR.Trim) Then
                WhereSTR = " Where " & WhereSTR

            End If

            Dim query = cmd & WhereSTR & ATMstr
            If query.Trim().EndsWith("and") Then
                query = ReplaceLastOccurrence(query.Trim, "and", "")
            End If
            MainFun.loglog("query:" & query, True)
            State.ReportName = "Reports\BulkTransactions.rpt"
            State.Hasdetails = False
            State.DetailsQuery = ""
            State.QueryString = query
            State.DFrom = StrFrom
            State.DTo = Strto
            State.Title = "Bulk Transactions Report"
            State.ATM = ""
            Session.Add("Obj", State)
            Response.Redirect("BulkWithdrowalTransactionsView.aspx")

            Else

            If (drpd_ATM.SelectedItem.Text = "ALL") Then
                ATMstr = " t.ATMId like '%'"
            Else
                ATMstr = " t.ATMId = '" & drpd_ATM.SelectedItem.ToString() & "'"
            End If

            If (DPC_date1.Value = "" Or DPC_date2.Value = "") Then
                Lbl_Status.Text = "Please select a time frame"
                Lbl_Status.Visible = True
                Return
            Else
                StrFrom = DPC_date1.Value & " " & drpd_FRM_HH.SelectedItem.ToString() & ":" & drpd_FRM_MM.SelectedItem.ToString()
                Strto = DPC_date2.Value & " " & drpd_TO_HH.SelectedItem.ToString() & ":" & drpd_TO_MM.SelectedItem.ToString()
                WhereSTR = WhereSTR & " WithdrawalDateTime between '" & StrFrom & "' and '" & Strto & "' and"
            End If
            If Not String.IsNullOrEmpty(WhereSTR.Trim) Then
                WhereSTR = " Where " & WhereSTR

            End If

            Dim query = cmd & WhereSTR & ATMstr
            If query.Trim().EndsWith("and") Then
                query = ReplaceLastOccurrence(query.Trim, "and", "")
            End If
            MainFun.loglog("query:" & query, True)
            State.ReportName = "Reports\BulkTransactions.rpt"
            State.Hasdetails = False
            State.DetailsQuery = ""
            State.QueryString = query
            State.DFrom = StrFrom
        State.DTo = Strto
            State.Title = "Bulk Transactions Report"
            State.ATM = ""
            Session.Add("Obj", State)
            Response.Redirect("BulkWithdrowalTransactionsView.aspx")
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

    Protected Sub drpdl_Wstatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpdl_Wstatus.SelectedIndexChanged
        If drpdl_Wstatus.SelectedValue = "New" Then
            trFrom.Visible = False
            trTo.Visible = False
        Else
            trFrom.Visible = True
            trTo.Visible = True
        End If
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

    Public Sub LoadRemitters()
        Com = New SqlCommand()
        Try
            Con.Open()
        Catch ex As Exception
            'Lbl_Status.Text = "DB Error."
            'Lbl_Status.Visible = True
            'Return
        End Try
        Com.Connection = Con
        drpd_Remitters.Items.Clear()
        drpd_Remitters.Items.Add("ALL")
        Try


            Com.CommandText = "select R.ID,r.RemitterName from Remitters R"


            dr = Com.ExecuteReader()
            While (dr.Read())
                drpd_Remitters.Items.Add(New ListItem(dr(1), dr(0)))
            End While
            dr.Close()
        Catch ex As Exception
            Lbl_Status.Visible = True
            Lbl_Status.Text = "Error while Loading Remitters."
            Return
        End Try

    End Sub


    Public Function ReplaceLastOccurrence(ByVal source As String, ByVal searchText As String, ByVal replace As String) As String
        Dim position = source.LastIndexOf(searchText)
        If (position = -1) Then Return source
        Dim result = source.Remove(position, searchText.Length).Insert(position, replace)
        Return result
    End Function

End Class

