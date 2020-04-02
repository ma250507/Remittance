Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Partial Class Maintenance
    Inherits System.Web.UI.Page


    Private Con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("NCRMoneyFerConnection").ConnectionString)
    Private Com As SqlCommand
    Private dr As SqlDataReader

    Private app_base_dir As String = System.AppDomain.CurrentDomain.BaseDirectory



    Private msg As Byte()
    Private bytesRec As Integer
    Private Reply As String
    Private ConfirmedReply As String
    Private Dt As DataTable
    Private ATMID As String = ""
    Private BankCode As String = ""
    Private CountryCode As String = ""
    Private trxs As ArrayList
    Private CCode As String = ""
    Private Bcode As String = ""
    Private StrFrom As String = ""
    Private Strto As String = ""
    Private StrTrxCode As String = ""
    Private StrBM As String = ""
    Private StrDM As String = ""
    Private SelectedTrx As String = ""
    Private UserPerm As Permissions


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim BankToSelect As String = ""
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
            Catch ex As Exception
                Response.Redirect("Login.aspx")
            End Try
            Com = New SqlCommand("", Con)
            Con.Open()
            'Com.CommandText = "select atmid,bankcode,countrycode " _
            '            & " from atm " _
            '            & " where atmipaddress='" & CType(ViewState("MyIP"), String) & "'"
            'dr = Com.ExecuteReader()
            'While (dr.Read())
            '    ATMID = dr.GetString(0)
            '    BankCode = dr.GetString(1)
            '    CountryCode = dr.GetString(2)
            'End While
            'ViewState.Add("AId", ATMID)
            'ViewState.Add("BC", BankCode)
            'ViewState.Add("CC", CountryCode)
            'dr.Close()

            drpd_Country.Items.Clear()
            Com.CommandText = " select distinct countryname from country "
            dr = Com.ExecuteReader()
            While (dr.Read())
                drpd_Country.Items.Add(dr(0))
            End While
            dr.Close()
            BankToSelect = LoadBank()
            If (drpd_Bank.Items.Count >= 2) Then
                drpd_Bank.SelectedIndex = drpd_Bank.Items.IndexOf(drpd_Bank.Items.FindByValue(BankToSelect))
                LoadATM()
                If (drpd_ATM.Items.Count >= 2) Then
                    drpd_ATM.SelectedIndex = 0 'drpd_ATM.Items.IndexOf(drpd_ATM.Items.FindByValue("ALL"))
                    drpd_ATM.Text = "ALL"
                End If
            End If
            DPC_date1.Value = Date.Now().ToString("MM/dd/yyyy")
            DPC_date2.Value = Date.Now().ToString("MM/dd/yyyy")
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

            drpd_TO_HH.SelectedIndex = drpd_TO_HH.Items.IndexOf(drpd_TO_HH.Items.FindByValue("23"))
            drpd_TO_MM.SelectedIndex = drpd_TO_MM.Items.IndexOf(drpd_TO_MM.Items.FindByValue("59"))
        Else
            Lbl_Status.Visible = False
        End If

    End Sub

    Protected Sub PrepareDT()
        'Enable()
        'Clear()
        'While (dr.Read())
        '    Label1.Text = dr.GetString(0)
        '    Label2.Text = dr.GetString(1)
        '    Label3.Text = dr.GetString(2)
        '    Label4.Text = dr.GetString(3)
        '    Label5.Text = dr.GetString(4)
        '    Label6.Text = dr.GetString(5)
        '    Label7.Text = dr.GetString(6)
        '    Label8.Text = dr.GetString(7)
        '    If (dr.IsDBNull(8)) Then
        '        Label9.Text = ""
        '    Else
        '        Label9.Text = dr.GetString(8)
        '    End If



        '    If (dr.IsDBNull(10)) Then
        '        Label11.Text = ""
        '    Else
        '        Label11.Text = dr.GetString(10)
        '    End If



        '    If (dr.IsDBNull(12)) Then
        '        Label13.Text = ""
        '    Else
        '        Label13.Text = dr.GetDecimal(12)
        '    End If

        '    If (dr.IsDBNull(13)) Then
        '        Label14.Text = ""
        '    Else
        '        Label14.Text = dr.GetString(13)
        '    End If
        '    If (dr.IsDBNull(14)) Then
        '        Label15.Text = ""
        '    Else
        '        Label15.Text = dr.GetDateTime(14).ToString()
        '    End If
        '    If (dr.IsDBNull(15)) Then
        '        Label16.Text = ""
        '    Else
        '        Label16.Text = dr.GetString(15)
        '    End If
        '    If (dr.IsDBNull(16)) Then
        '        Label17.Text = ""
        '    Else
        '        Label17.Text = dr.GetString(16)
        '    End If
        '    If (dr.IsDBNull(17)) Then
        '        Label18.Text = ""
        '    Else
        '        Label18.Text = dr.GetString(17)
        '    End If
        '    If (dr.IsDBNull(18)) Then
        '        Label19.Text = ""
        '    Else
        '        Label19.Text = dr.GetDateTime(18).ToString()
        '    End If
        '    If (dr.IsDBNull(19)) Then
        '        Label20.Text = ""
        '    Else
        '        Label20.Text = dr.GetString(19)
        '    End If
        '    If (dr.IsDBNull(20)) Then
        '        Label21.Text = ""
        '    Else
        '        Label21.Text = dr.GetDateTime(20).ToString()
        '    End If
        '    If (dr.IsDBNull(21)) Then
        '        Label22.Text = ""
        '    Else
        '        Label22.Text = dr.GetString(21)
        '    End If
        '    If (dr.IsDBNull(22)) Then
        '        Label23.Text = ""
        '    Else
        '        Label23.Text = dr.GetString(22)
        '    End If
        '    If (dr.IsDBNull(23)) Then
        '        Label24.Text = ""
        '    Else
        '        Label24.Text = dr.GetDateTime(23).ToString
        '    End If
        'End While
    End Sub
    Protected Sub btn_Search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Search.Click
        Dim ATMstr As String = ""
        Try
            If (drpd_ATM.SelectedItem.ToString() = "ALL") Then
                ATMstr = "%"
            Else
                ATMstr = drpd_ATM.SelectedItem.ToString()
            End If
        Catch ex As Exception
            Lbl_Status.Visible = True
            Lbl_Status.Text = "Please Select an ATM."
            Return
        End Try


        Dim cmd As String = "select TransactionCode,DepositStatus,WithdrawalStatus ,SMSSendingStatus as 'SMS Sent',WSendingStatus as 'Beneficiary Alert' from transactions " & _
                            "where countrycode ='" & Session("CCode") & "' and bankcode='" & Session("Bcode") & "' and atmid like '" & ATMstr & "' and "


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
                'Case "ALL"
                '    cmd = cmd & "Depositstatus like '%' and "
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
                'Case "ALL"
                '    cmd = cmd & "withdrawalstatus like '%' and "
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
                cmd = cmd & "transactioncode like '" & txt_TRX_Code.Text & "%' and "
            Case "Part Of"
                cmd = cmd & "transactioncode like '%" & txt_TRX_Code.Text & "%' and "
            Case "Ends With"
                cmd = cmd & "transactioncode like '%" & txt_TRX_Code.Text & "' and "
            Case "Exact"
                cmd = cmd & "transactioncode = '" & txt_TRX_Code.Text & "' and "
            Case Else

        End Select
        Select Case drpd_BM.SelectedItem.Text
            Case "Start With"
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
            Case "Start With"
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
            cmd = cmd & " depositdatetime between '" & StrFrom & "' and '" & Strto & "' "
        End If


        SqlDataSource1.ConnectionString = ConfigurationManager.ConnectionStrings("NCRMoneyFerConnection").ConnectionString
        SqlDataSource1.SelectCommand = cmd
        SqlDataSource1.Select(System.Web.UI.DataSourceSelectArguments.Empty)
        GridView1.DataBind()

        Lbl_Status.Text = ""
        'End If



        'End If
    End Sub



    'Private Sub MessageBox(ByVal strMsg As String)
    '    Dim lbl As New Label
    '    lbl.Text = "<script language='javascript'>" & Environment.NewLine _
    '               & "window.alert(" & "'" & strMsg & "'" & ")</script>"
    '    Page.Controls.Add(lbl)

    'End Sub



    'Protected Sub GridView1_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.DataBound
    '    Try


    '        Dim cbHeader As New CheckBox()
    '        cbHeader = CType(GridView1.HeaderRow.FindControl("gvhcb"), CheckBox)

    '        'Run the ChangeCheckBoxState client-side function whenever the
    '        'header checkbox is checked/unchecked
    '        cbHeader.Attributes("onclick") = "ChangeAllCheckBoxStates(this.checked);"

    '        'Add the CheckBox's ID to the client-side CheckBoxIDs array
    '        Dim ArrayValues As New List(Of String)
    '        ArrayValues.Add(String.Concat("'", cbHeader.ClientID, "'"))

    '        For Each gvr As GridViewRow In GridView1.Rows
    '            'Get a programmatic reference to the CheckBox control
    '            Dim cb As CheckBox = CType(gvr.FindControl("gvrcb"), CheckBox)

    '            'If the checkbox is unchecked, ensure that the Header CheckBox is unchecked
    '            cb.Attributes("onclick") = "ChangeHeaderAsNeeded();"

    '            'Add the CheckBox's ID to the client-side CheckBoxIDs array
    '            ArrayValues.Add(String.Concat("'", cb.ClientID, "'"))
    '        Next

    '        'Output the array to the Literal control (CheckBoxIDsArray)

    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Public Sub Resending(ByVal TrxCode As String)
    '    Com = New SqlCommand()
    '    Con.Open()
    '    Com.CommandText = "update transactions  " & _
    '                        " set resendsmsflag='1',resendsmsdatetime=getdate() " & _
    '                        " where transactioncode='" & TrxCode & "'and withdrawalstatus is NULL and Depositstatus ='Confirmed' and Cancelstatus is null "
    '    Com.Connection = Con

    '    Com.ExecuteNonQuery()
    '    Con.Close()

    'End Sub

    Protected Sub drpd_Country_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpd_Country.SelectedIndexChanged

        LoadBank()
    End Sub
    Public Function LoadBank() As String
        Dim ret As String = ""
        Com = New SqlCommand()
        Try
            Con.Open()
        Catch ex As Exception
           
        End Try

        Com.Connection = Con
        Com.CommandText = "select countrycode from country where countryname='" & drpd_Country.SelectedItem.ToString() & "'"
        dr = Com.ExecuteReader()
        drpd_Bank.Items.Clear()
        Try
            While (dr.Read())
                CCode = dr(0)
            End While
            dr.Close()
            Com.CommandText = "select bankname from bank where countrycode='" & CCode & "'"
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
        End Try
        Session.Add("CCode", CCode)
        Return ret
    End Function
    Protected Sub drpd_Bank_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpd_Bank.SelectedIndexChanged
        LoadATM()
    End Sub
    Public Sub LoadATM()
        Com = New SqlCommand()
        Try
            Con.Open()
        Catch ex As Exception

        End Try
        Com.Connection = Con
        Com.CommandText = "select bankcode from bank where bankname='" & drpd_Bank.SelectedItem.ToString() & "'"
        dr = Com.ExecuteReader()
        drpd_ATM.Items.Clear()
        Try
            While (dr.Read())
                Bcode = dr(0)
            End While
            dr.Close()
            Com.CommandText = "select atmid from atm where bankcode='" & Bcode & "' and isteller='" & Convert.ToInt32(CB_IsTeller.Checked) & "'"
            drpd_ATM.Items.Add("ALL")
            drpd_ATM.Items.Add("Don`t Care")
            dr = Com.ExecuteReader()
            While (dr.Read())
                drpd_ATM.Items.Add(dr(0))
            End While
            dr.Close()
        Catch ex As Exception
            Lbl_Status.Text = "Error while loading ATMs."
            Lbl_Status.Visible = True
        End Try
        Session.Add("BCode", Bcode)
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        GridView1.Columns(0).ToString()
        SelectedTrx = GridView1.SelectedValue
        Session.Add("Trx", SelectedTrx)
        Response.Redirect("Actions.aspx")
    End Sub



    Protected Sub CB_IsTeller_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CB_IsTeller.CheckedChanged
        LoadATM()
    End Sub
End Class
