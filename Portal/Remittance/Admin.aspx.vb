Imports System.Data
Imports System.Data.SqlClient
Partial Class Admin
    Inherits System.Web.UI.Page
    Private Con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("NCRMoneyFerConnection").ConnectionString)
    Private app_base_dir As String = System.AppDomain.CurrentDomain.BaseDirectory
    Private Com As New SqlCommand("", Con)
    Private dr As SqlDataReader
    Private NcrCrypt As NCRCrypto
    Private UserPerm As Permissions
    Private MainFun As New General

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("Status") = "InValid_user" Or Session("Status") = Nothing) Then
            Response.Redirect("Login.aspx")
        End If
        If (IsPostBack <> True) Then
            Try
                UserPerm = New Permissions()
                UserPerm = Session("Perm")
                If (UserPerm.Administration <> "True") Then
                    Response.Redirect("Login.aspx")
                End If
            Catch ex As Exception
                Response.Redirect("Login.aspx")
            End Try
            txt_IP.Text = getPartConnectionString("Data Source", Con.ConnectionString)
            txt_DB.Text = getPartConnectionString("Initial Catalog", Con.ConnectionString)
            txt_UN.Text = getPartConnectionString("User ID", Con.ConnectionString)
            txt_PWD.Text = getPartConnectionString("Password ", Con.ConnectionString)

            Try
                Com.Connection = Con
                Con.Open()
            Catch ex As Exception
                Lbl_Status.Text = "Error while connecting to database."
                Lbl_Status.Visible = True
            End Try

            Com.CommandText = "select distinct countryname from country"
            dr = Com.ExecuteReader()
            drpd_Country.Items.Add("None")
            While (dr.Read())
                drpd_Country.Items.Add(dr(0))
            End While
            dr.Close()
            Con.Close()
        Else
            Lbl_Status.Visible = False
            AdminConBTN.Visible = False
        End If
    End Sub


#Region "DB"


    Private Function getPartConnectionString(ByVal part As String, ByVal _connectionString As String) As String

        Dim inicio As Integer
        Dim partTemp As String
        Dim partResult As String

        inicio = InStr(_connectionString, part) + Len(part) + 1

        For contPartConn As Integer = inicio To Len(_connectionString)
            partTemp = Mid(_connectionString, contPartConn, 1)
            If partTemp = ";" Then Exit For
            partResult += partTemp
        Next

        Return partResult
    End Function

    Protected Sub btn_Save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Save.Click

        Dim myConfiguration As Configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~")
        Try
            myConfiguration.ConnectionStrings.ConnectionStrings("NCRMoneyFerConnection").ConnectionString = "Data Source=" & txt_IP.Text & ";Integrated Security=False;Initial Catalog=" & txt_DB.Text & "; User ID=" & txt_UN.Text & "; Password =" & txt_PWD.Text & ""
            myConfiguration.Save()
            AdminConBTN.Visible = True
        Catch ex As Exception
            Lbl_Status.Text = "Error while saving: " & ex.Message & ""
            Lbl_Status.Visible = True
        End Try
        


    End Sub
#End Region
#Region "Bank"

    Protected Sub drpd_Country_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpd_Country.SelectedIndexChanged
        LoadBank(drpd_Country.SelectedItem.ToString())
    End Sub
    Public Sub LoadBank(ByVal CCode As String)
        Com = New SqlCommand()
        Try
            Con.Open()
        Catch ex As Exception
            Lbl_Status.Text = "Error while connecting to database."
            Lbl_Status.Visible = True
        End Try

        Com.Connection = Con
        Com.CommandText = "select bankname from bank where countrycode = (select countrycode from country where countryname='" & CCode & "')"
        drpd_Bank.Items.Clear()
        Try
            drpd_Bank.Items.Add("None")
            dr = Com.ExecuteReader()
            While (dr.Read())
                drpd_Bank.Items.Add(dr(0))
            End While
            dr.Close()
        Catch ex As Exception
            Lbl_Status.Text = "Error while loading Banks."
            Lbl_Status.Visible = True
        End Try
    End Sub

    Protected Sub drpd_Bank_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpd_Bank.SelectedIndexChanged

        LoadbankData(drpd_Country.SelectedItem.ToString(), drpd_Bank.SelectedItem.ToString())
    End Sub

    Public Sub LoadbankData(ByVal Country As String, ByVal Bank As String)
        Com = New SqlCommand()
        Try
            Con.Open()
        Catch ex As Exception
            Lbl_Status.Text = "Error while connecting to database."
            Lbl_Status.Visible = True
        End Try

        Com.Connection = Con
        Com.CommandText = "select MaxNotesCount,MaximumAmount,MinimumAmount,ReceiptLine1,ReceiptLine2,ReceiptLine3,StartAmount1,EndAmount1,CommissionAmount1,StartAmount2,EndAmount2,CommissionAmount2,MaximumDailyAmount,MaintenanceATM,RemittanceServicePort,RemittanceServiceIPAddress,MaximumKeyTrials,MaxreActivateTimes,DepositTransactionExpirationDays,MaximumMonthlyAmount,MaximumDailyCount  " & _
                          "from Bank " & _
                          "where bankcode=(select bankcode from bank where bankname='" & Bank & "') and countrycode=(select countrycode from country where countryname='" & Country & "')"
        Try
            dr = Com.ExecuteReader()
            While (dr.Read())
                If (IsDBNull(dr("MaxNotesCount"))) Then
                    txt_MXNC.Text = ""
                Else
                    txt_MXNC.Text = dr("MaxNotesCount")
                End If
                If (IsDBNull(dr("MaximumAmount"))) Then
                    txt_MXA.Text = ""
                Else
                    txt_MXA.Text = dr("MaximumAmount")
                End If
                If (IsDBNull(dr("MinimumAmount"))) Then
                    txt_MNA.Text = ""
                Else
                    txt_MNA.Text = dr("MinimumAmount")
                End If
                If (IsDBNull(dr("ReceiptLine1"))) Then
                    txt_RCPTL1.Text = ""
                Else
                    txt_RCPTL1.Text = dr("ReceiptLine1")
                End If
                If (IsDBNull(dr("ReceiptLine2"))) Then
                    txt_RCPTL2.Text = ""
                Else
                    txt_RCPTL2.Text = dr("ReceiptLine2")
                End If
                If (IsDBNull(dr("ReceiptLine3"))) Then
                    txt_RCPTL3.Text = ""
                Else
                    txt_RCPTL3.Text = dr("ReceiptLine3")
                End If
                If (IsDBNull(dr("StartAmount1"))) Then
                    txt_SA1.Text = ""
                Else
                    txt_SA1.Text = dr("StartAmount1")
                End If
                If (IsDBNull(dr("EndAmount1"))) Then
                    txt_EA1.Text = ""
                Else
                    txt_EA1.Text = dr("EndAmount1")
                End If
                If (IsDBNull(dr("CommissionAmount1"))) Then
                    txt_CA1.Text = ""
                Else
                    txt_CA1.Text = dr("CommissionAmount1")
                End If
                If (IsDBNull(dr("StartAmount2"))) Then
                    txt_SA2.Text = ""
                Else
                    txt_SA2.Text = dr("StartAmount2")
                End If
                If (IsDBNull(dr("EndAmount2"))) Then
                    txt_EA2.Text = ""
                Else
                    txt_EA2.Text = dr("EndAmount2")
                End If
                If (IsDBNull(dr("CommissionAmount2"))) Then
                    txt_CA2.Text = ""
                Else
                    txt_CA2.Text = dr("CommissionAmount2")
                End If
                If (IsDBNull(dr("MaximumDailyAmount"))) Then
                    txt_MXDA.Text = ""
                Else
                    txt_MXDA.Text = dr("MaximumDailyAmount")
                End If

                If (IsDBNull(dr("MaintenanceATM"))) Then
                    txt_MATM.Text = ""
                Else
                    txt_MATM.Text = dr("MaintenanceATM")
                End If

                If (IsDBNull(dr("RemittanceServicePort"))) Then
                    txt_RSP.Text = ""
                Else
                    txt_RSP.Text = dr("RemittanceServicePort")
                End If

                If (IsDBNull(dr("RemittanceServiceIPAddress"))) Then
                    txt_RSIP.Text = ""
                Else
                    txt_RSIP.Text = dr("RemittanceServiceIPAddress")
                End If

                If (IsDBNull(dr("MaximumKeyTrials"))) Then
                    txt_MXKeyTR.Text = ""
                Else
                    txt_MXKeyTR.Text = dr("MaximumKeyTrials")
                End If

                If (IsDBNull(dr("MaxreActivateTimes"))) Then
                    txt_MXReactivateTimes.Text = ""
                Else
                    txt_MXReactivateTimes.Text = dr("MaxreActivateTimes")
                End If


                If (IsDBNull(dr("DepositTransactionExpirationDays"))) Then
                    txt_DTExpirationDays.Text = ""
                Else
                    txt_DTExpirationDays.Text = dr("DepositTransactionExpirationDays")
                End If

                If (IsDBNull(dr("MaximumMonthlyAmount"))) Then
                    txt_MXMMAMT.Text = ""
                Else
                    txt_MXMMAMT.Text = dr("MaximumMonthlyAmount")
                End If

                If (IsDBNull(dr("MaximumDailyCount"))) Then
                    txt_DailyMAXCount.Text = ""
                Else
                    txt_DailyMAXCount.Text = dr("MaximumDailyCount")
                End If

            End While
            dr.Close()
            Con.Close()
        Catch ex As Exception
            Lbl_Status.Text = "Error while loading " & Bank & " Data."
            Lbl_Status.Visible = True
        End Try
    End Sub

    Protected Sub btn_Bsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Bsave.Click
        'txt_MXNC.Text = ""
        If (drpd_Country.SelectedItem.Text = "None" Or drpd_Bank.SelectedItem.Text = "None") Then
            Lbl_Status.Text = "Please select a country or a bank."
            Lbl_Status.Visible = True
            Exit Sub
        End If

        Dim ret As Integer
        Com.Connection = Con
        Con.Open()
        Com.CommandText = "update bank " & _
                          " set DepositTransactionExpirationDays='" & txt_DTExpirationDays.Text & "' ,MaxNotesCount='" & txt_MXNC.Text & "',MaximumAmount='" & txt_MXA.Text & "',MinimumAmount='" & txt_MNA.Text & "',ReceiptLine1='" & txt_RCPTL1.Text & "',ReceiptLine2='" & txt_RCPTL2.Text & "',ReceiptLine3='" & txt_RCPTL3.Text & "',StartAmount1='" & txt_SA1.Text & "',EndAmount1='" & txt_EA1.Text & "',CommissionAmount1='" & txt_CA1.Text & "',StartAmount2='" & txt_SA2.Text & "',EndAmount2='" & txt_EA2.Text & "',CommissionAmount2='" & txt_CA2.Text & "',MaximumDailyAmount='" & txt_MXDA.Text & "',MaintenanceATM='" & txt_MATM.Text & "',RemittanceServicePort='" & txt_RSP.Text & "' ,RemittanceServiceIPAddress='" & txt_RSIP.Text & "' ,MaximumKeyTrials='" & txt_MXKeyTR.Text & "' , MaxreActivateTimes='" & txt_MXReactivateTimes.Text & "' , MaximumMonthlyAmount = '" & txt_MXMMAMT.Text & "' , MaximumDailyCount = '" & txt_DailyMAXCount.Text & "'" & _
                          "where bankcode=(select bankcode from bank where bankname='" & drpd_Bank.SelectedItem.Text & "') and countrycode=(select countrycode from country where countryname='" & drpd_country.SelectedItem.Text & "')"
        Try
            ret = Com.ExecuteNonQuery()
        Catch ex As Exception
            Lbl_Status.Text = "Error while updating bank data"
            Lbl_Status.Visible = True
            Return
        End Try
        AdminConBTN.Visible = True
        Con.Close()
    End Sub
#End Region
    'Protected Sub btn_logout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_logout.Click
    '    Session.Abandon()
    '    Response.Redirect("~/login.aspx")
    'End Sub

    'Protected Sub btn_AHome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_AHome.Click
    '    Response.Redirect("~/Default.aspx")
    'End Sub

#Region "Groups"
    ''Groups are hidden and visible now under users
    'Protected Sub GroupADD_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim ID As String = (DirectCast(GridView2.FooterRow.FindControl("TXT_GroupId"), TextBox)).Text
    '    Dim Name As String = (DirectCast(GridView2.FooterRow.FindControl("TXT_GroupName"), TextBox)).Text
    '    Dim Reports As CheckBox = (DirectCast(GridView2.FooterRow.FindControl("CB_Reports"), CheckBox))
    '    Dim Administration As CheckBox = (DirectCast(GridView2.FooterRow.FindControl("CB_Admin"), CheckBox))
    '    Dim Maintenance As CheckBox = (DirectCast(GridView2.FooterRow.FindControl("CB_Maintenance"), CheckBox))
    '    Dim Users As CheckBox = (DirectCast(GridView2.FooterRow.FindControl("CB_Users"), CheckBox))
    '    Dim Teller As CheckBox = (DirectCast(GridView2.FooterRow.FindControl("CB_Teller"), CheckBox))
    '    Dim Register As CheckBox = (DirectCast(GridView2.FooterRow.FindControl("CB_Registeration"), CheckBox))

    '    Dim ret As Integer = 0

    '    NcrCrypt = New NCRCrypto()
    '    If (ID = "" Or ID = Nothing Or Name = "" Or Name = Nothing) Then
    '        Lbl_Status.Visible = True
    '        Lbl_Status.Text = "Can`t insert empty fields"
    '        Return
    '    End If

    '    'SqlDataSource2.InsertParameters("ID").DefaultValue = ID
    '    'SqlDataSource2.InsertParameters("Name").DefaultValue = Name
    '    'SqlDataSource2.InsertParameters("Reports").DefaultValue = Reports.Checked
    '    'SqlDataSource2.InsertParameters("Users").DefaultValue = Users.Checked
    '    'SqlDataSource2.InsertParameters("Teller").DefaultValue = Teller.Checked
    '    'SqlDataSource2.InsertParameters("Maintenance").DefaultValue = Maintenance.Checked
    '    'SqlDataSource2.InsertParameters("Administration").DefaultValue = Administration.Checked
    '    'SqlDataSource2.InsertParameters("Registeration").DefaultValue = Register.Checked

    '    SqlDataSource2.InsertCommand = "INSERT INTO [Groups] ([ID], [Name], [Reports], [Maintenance], [Administration], [Users], [Teller], [Registeration] ) VALUES (" & ID & ",'" & Name & "'," & Convert.ToInt32(Reports.Checked) & ", " & Convert.ToInt32(Maintenance.Checked) & ", " & Convert.ToInt32(Administration.Checked) & ", " & Convert.ToInt32(Users.Checked) & ", " & Convert.ToInt32(Teller.Checked) & "," & Convert.ToInt32(Register.Checked) & ")"

    '    Try
    '        SqlDataSource2.Insert()
    '        AdminConBTN.Visible = True
    '    Catch ex As Exception
    '        Lbl_Status.Visible = True
    '        Lbl_Status.Text = "Error while adding Grouop: " & ex.Message & ""
    '    End Try

    'End Sub
    'Protected Sub GridView2_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles GridView2.RowUpdating
    '    Dim ID As String = (DirectCast(GridView2.Rows(e.RowIndex).FindControl("Label1"), Label)).Text
    '    Dim Name As String = (DirectCast(GridView2.Rows(e.RowIndex).FindControl("TextBox1"), TextBox)).Text
    '    Dim Reports As CheckBox = (DirectCast(GridView2.Rows(e.RowIndex).FindControl("CheckBox1"), CheckBox))
    '    Dim Administration As CheckBox = (DirectCast(GridView2.Rows(e.RowIndex).FindControl("CheckBox2"), CheckBox))
    '    Dim Maintenance As CheckBox = (DirectCast(GridView2.Rows(e.RowIndex).FindControl("CheckBox3"), CheckBox))
    '    Dim Users As CheckBox = (DirectCast(GridView2.Rows(e.RowIndex).FindControl("CheckBox4"), CheckBox))
    '    Dim Teller As CheckBox = (DirectCast(GridView2.Rows(e.RowIndex).FindControl("CheckBox6"), CheckBox))
    '    Dim Register As CheckBox = (DirectCast(GridView2.Rows(e.RowIndex).FindControl("CheckBox8"), CheckBox))


    '    'SqlDataSource2.UpdateParameters("original_ID").DefaultValue = ID
    '    'SqlDataSource2.UpdateParameters("Name").DefaultValue = Name
    '    'SqlDataSource2.UpdateParameters("Reports").DefaultValue = Convert.ToInt32(Reports.Checked)
    '    'SqlDataSource2.UpdateParameters("Users").DefaultValue = CType(, Integer)
    '    'SqlDataSource2.UpdateParameters("Teller").DefaultValue = CType, Integer)
    '    'SqlDataSource2.UpdateParameters("Maintenance").DefaultValue = CType(, Integer)
    '    'SqlDataSource2.UpdateParameters("Administration").DefaultValue = CType(, Integer)
    '    'SqlDataSource2.UpdateParameters("Registeration").DefaultValue = CType(, Integer)

    '    SqlDataSource2.UpdateCommand = "UPDATE [Groups] SET [Name] = '" & Name & "', [Reports] = " & Convert.ToInt32(Reports.Checked) & ", [Maintenance] = " & Convert.ToInt32(Maintenance.Checked) & ", [Administration] = " & Convert.ToInt32(Administration.Checked) & "" & _
    '                                   " , [Users] = " & Convert.ToInt32(Users.Checked) & ", [Teller] = " & Convert.ToInt32(Teller.Checked) & " , [Registeration] = " & Convert.ToInt32(Register.Checked) & " WHERE [ID] = " & ID & ""

    '    Try

    '        SqlDataSource2.Update()
    '        AdminConBTN.Visible = True
    '    Catch ex As Exception
    '        Lbl_Status.Visible = True
    '        Lbl_Status.Text = "Error while editing Group: " & ex.Message & ""
    '    End Try

    'End Sub
#End Region


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

    Protected Sub ATMADD_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ID As String = (DirectCast(GridView3.FooterRow.FindControl("Txt_ATMID"), TextBox)).Text
        Dim Location As String = (DirectCast(GridView3.FooterRow.FindControl("drpd_AATMBr"), TextBox)).Text
        Dim CountryCode As String = (DirectCast(GridView3.FooterRow.FindControl("drpd_CountryCode"), DropDownList)).SelectedValue
        Dim BankCode As String = (DirectCast(GridView3.FooterRow.FindControl("drpd_BankCode"), DropDownList)).SelectedValue
        Dim Cassitte1 As String = (DirectCast(GridView3.FooterRow.FindControl("Txt_Cassitte1"), TextBox)).Text
        Dim Cassitte2 As String = (DirectCast(GridView3.FooterRow.FindControl("Txt_Cassitte2"), TextBox)).Text
        Dim Cassitte3 As String = (DirectCast(GridView3.FooterRow.FindControl("Txt_Cassitte3"), TextBox)).Text
        Dim Cassitte4 As String = (DirectCast(GridView3.FooterRow.FindControl("Txt_Cassitte4"), TextBox)).Text
        Dim ATMIPAddress As String = (DirectCast(GridView3.FooterRow.FindControl("Txt_ATMIPAddress"), TextBox)).Text
        Dim Teller As CheckBox = (DirectCast(GridView3.FooterRow.FindControl("CB_IsTeller"), CheckBox))
        Dim ATMName As String = (DirectCast(GridView3.FooterRow.FindControl("TXT_ATMName"), TextBox)).Text
        Dim TerminalID As String = (DirectCast(GridView3.FooterRow.FindControl("TXT_ADD_TermID"), TextBox)).Text
        Dim MerchantID As String = (DirectCast(GridView3.FooterRow.FindControl("TXT_ADD_MerchantID"), TextBox)).Text

        Dim ret As Integer = 0

        NcrCrypt = New NCRCrypto()
        If (ID = "" Or ID = Nothing) Then
            Lbl_Status.Visible = True
            Lbl_Status.Text = "Terminal ID is required."
            Return
        End If

        SqlDataSource1.InsertParameters("ATMId").DefaultValue = ID
        SqlDataSource1.InsertParameters("ATMLocation").DefaultValue = Location
        SqlDataSource1.InsertParameters("CountryCode").DefaultValue = CountryCode
        SqlDataSource1.InsertParameters("BankCode").DefaultValue = BankCode
        SqlDataSource1.InsertParameters("Cassitte1Value").DefaultValue = Cassitte1
        SqlDataSource1.InsertParameters("Cassitte2Value").DefaultValue = Cassitte2
        SqlDataSource1.InsertParameters("Cassitte3Value").DefaultValue = Cassitte3
        SqlDataSource1.InsertParameters("Cassitte4Value").DefaultValue = Cassitte4
        SqlDataSource1.InsertParameters("ATMIPAddress").DefaultValue = ATMIPAddress
        SqlDataSource1.InsertParameters("IsTeller").DefaultValue = Teller.Checked
        SqlDataSource1.InsertParameters("ATMName").DefaultValue = ATMName
        SqlDataSource1.InsertParameters("TerminalID").DefaultValue = TerminalID
        SqlDataSource1.InsertParameters("MerchantID").DefaultValue = MerchantID

        Try
            SqlDataSource1.Insert()
            Log(Session("user").ToString(), "ATM ADD", ID, Session("UserName"), Session("Branch"))
            AdminConBTN.Visible = True
        Catch ex As Exception
            Lbl_Status.Visible = True
            Lbl_Status.Text = "Error while adding ATM: " & ex.Message & ""
        End Try

    End Sub


    Protected Sub GridView3_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView3.RowDeleting
        Dim ID As String = (DirectCast(GridView3.Rows(e.RowIndex).FindControl("Label1"), Label)).Text
        Dim Location As String = (DirectCast(GridView3.Rows(e.RowIndex).FindControl("Label2"), Label)).Text
        Dim CountryCode As String = (DirectCast(GridView3.Rows(e.RowIndex).FindControl("Label3"), Label)).Text
        Dim BankCode As String = (DirectCast(GridView3.Rows(e.RowIndex).FindControl("Label4"), Label)).Text
        Dim Cassitte1 As String = (DirectCast(GridView3.Rows(e.RowIndex).FindControl("Label5"), Label)).Text
        Dim Cassitte2 As String = (DirectCast(GridView3.Rows(e.RowIndex).FindControl("Label6"), Label)).Text
        Dim Cassitte3 As String = (DirectCast(GridView3.Rows(e.RowIndex).FindControl("Label7"), Label)).Text
        Dim Cassitte4 As String = (DirectCast(GridView3.Rows(e.RowIndex).FindControl("Label8"), Label)).Text
        Dim ATMIPAddress As String = (DirectCast(GridView3.Rows(e.RowIndex).FindControl("Label9"), Label)).Text
        Dim Teller As CheckBox = (DirectCast(GridView3.Rows(e.RowIndex).FindControl("CheckBox1"), CheckBox))


        SqlDataSource1.DeleteParameters("original_ATMId").DefaultValue = ID
        SqlDataSource1.DeleteParameters("original_ATMLocation").DefaultValue = Location
        SqlDataSource1.DeleteParameters("original_CountryCode").DefaultValue = CountryCode
        SqlDataSource1.DeleteParameters("original_BankCode").DefaultValue = BankCode
        SqlDataSource1.DeleteParameters("original_Cassitte1Value").DefaultValue = Cassitte1
        SqlDataSource1.DeleteParameters("original_Cassitte2Value").DefaultValue = Cassitte2
        SqlDataSource1.DeleteParameters("original_Cassitte3Value").DefaultValue = Cassitte3
        SqlDataSource1.DeleteParameters("original_Cassitte4Value").DefaultValue = Cassitte4
        SqlDataSource1.DeleteParameters("original_ATMIPAddress").DefaultValue = ATMIPAddress
        SqlDataSource1.DeleteParameters("original_IsTeller").DefaultValue = Teller.Checked

        'SqlDataSource1.ConflictDetection = ConflictOptions.OverwriteChanges
        Try
            SqlDataSource1.Delete()
            AdminConBTN.Visible = True
        Catch ex As Exception
            Lbl_Status.Visible = True
            Lbl_Status.Text = "Error while adding Grouop: " & ex.Message & ""
        End Try
    End Sub

    Protected Sub GridView3_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles GridView3.RowUpdating
        Dim ID As String = (DirectCast(GridView3.Rows(e.RowIndex).FindControl("Label1"), Label)).Text
        Dim Location As String = (DirectCast(GridView3.Rows(e.RowIndex).FindControl("drpd_EATMBr"), TextBox)).Text
        Dim CountryCode As String = (DirectCast(GridView3.Rows(e.RowIndex).FindControl("Label2"), Label)).Text
        Dim BankCode As String = (DirectCast(GridView3.Rows(e.RowIndex).FindControl("Label3"), Label)).Text
        Dim Cassitte1 As String = (DirectCast(GridView3.Rows(e.RowIndex).FindControl("TextBox2"), TextBox)).Text
        Dim Cassitte2 As String = (DirectCast(GridView3.Rows(e.RowIndex).FindControl("TextBox3"), TextBox)).Text
        Dim Cassitte3 As String = (DirectCast(GridView3.Rows(e.RowIndex).FindControl("TextBox4"), TextBox)).Text
        Dim Cassitte4 As String = (DirectCast(GridView3.Rows(e.RowIndex).FindControl("TextBox5"), TextBox)).Text
        Dim ATMIPAddress As String = (DirectCast(GridView3.Rows(e.RowIndex).FindControl("TextBox6"), TextBox)).Text
        Dim Teller As CheckBox = (DirectCast(GridView3.Rows(e.RowIndex).FindControl("CheckBox1"), CheckBox))
        Dim ATMName As String = (DirectCast(GridView3.Rows(e.RowIndex).FindControl("TextBox7"), TextBox)).Text

        If (Location Is Nothing) Then
            Location = " "
        End If
        SqlDataSource1.UpdateParameters("original_ATMId").DefaultValue = ID
        SqlDataSource1.UpdateParameters("ATMLocation").DefaultValue = Location
        SqlDataSource1.UpdateParameters("original_CountryCode").DefaultValue = CountryCode
        SqlDataSource1.UpdateParameters("original_BankCode").DefaultValue = BankCode
        SqlDataSource1.UpdateParameters("Cassitte1Value").DefaultValue = Cassitte1
        SqlDataSource1.UpdateParameters("Cassitte2Value").DefaultValue = Cassitte2
        SqlDataSource1.UpdateParameters("Cassitte3Value").DefaultValue = Cassitte3
        SqlDataSource1.UpdateParameters("Cassitte4Value").DefaultValue = Cassitte4
        SqlDataSource1.UpdateParameters("ATMIPAddress").DefaultValue = ATMIPAddress
        SqlDataSource1.UpdateParameters("IsTeller").DefaultValue = Teller.Checked
        SqlDataSource1.UpdateParameters("ATMName").DefaultValue = Teller.Checked
        Try
            SqlDataSource1.Update()
            Log(Session("user").ToString(), "ATM Edit", ID, Session("UserName"), Session("Branch"))
            AdminConBTN.Visible = True
        Catch ex As Exception
            Lbl_Status.Visible = True
            Lbl_Status.Text = "Error while adding Grouop: " & ex.Message & ""
        End Try

    End Sub

    Protected Sub AdminConBTN_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AdminConBTN.Click
        Response.Redirect("Default.aspx")
    End Sub


    Protected Sub LNK_BRADD_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim CountryCode As String = (DirectCast(GridView4.FooterRow.FindControl("drpd_BRcc"), DropDownList)).SelectedValue
        Dim BankCode As String = (DirectCast(GridView4.FooterRow.FindControl("drpd_BRbc"), DropDownList)).SelectedValue
        Dim BranchName As String = (DirectCast(GridView4.FooterRow.FindControl("Txt_BranchName"), TextBox)).Text
        Dim Address As String = (DirectCast(GridView4.FooterRow.FindControl("Txt_BranchAddress"), TextBox)).Text





        If (BranchName = "" Or BranchName = Nothing) Then
            Lbl_Status.Visible = True
            Lbl_Status.Text = "Can`t insert empty fields."
            Return
        End If

        SqlDataSource3.InsertParameters("CountryCode").DefaultValue = CountryCode
        SqlDataSource3.InsertParameters("BankCode").DefaultValue = BankCode
        SqlDataSource3.InsertParameters("BranchName").DefaultValue = BranchName
        SqlDataSource3.InsertParameters("Address").DefaultValue = Address

        Try
            SqlDataSource3.Insert()
            AdminConBTN.Visible = True
        Catch ex As Exception
            Lbl_Status.Visible = True
            Lbl_Status.Text = "Error while adding Branch: " & ex.Message & ""
        End Try
    End Sub


    Protected Sub GridView4_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView4.RowCommand
        If (e.CommandName = "EmptyBranch") Then
            Dim row As GridViewRow = DirectCast(e.CommandSource, Button).NamingContainer

            Dim BC As String = DirectCast(row.FindControl("drpd_EBankCode"), DropDownList).Text
            Dim CC As String = DirectCast(row.FindControl("drpd_ECountryCode"), DropDownList).Text
            Dim BRName As String = DirectCast(row.FindControl("TXT_BranchName"), TextBox).Text
            Dim Address As String = DirectCast(row.FindControl("TXT_Address"), TextBox).Text

            SqlDataSource3.InsertParameters("CountryCode").DefaultValue = CC
            SqlDataSource3.InsertParameters("BankCode").DefaultValue = BC
            SqlDataSource3.InsertParameters("BranchName").DefaultValue = BRName
            SqlDataSource3.InsertParameters("Address").DefaultValue = Address

            Try
                SqlDataSource3.Insert()
                AdminConBTN.Visible = True
            Catch ex As Exception
                Lbl_Status.Text = "Error While Adding User."
                Lbl_Status.Visible = True
            End Try
            NcrCrypt = Nothing
        End If
    End Sub


    Public Function ConvertToBit(ByVal Str As String) As String
        Dim ret As String = "0"

        If (Str = "False") Then
            ret = "0"
        Else
            ret = "1"
        End If

        Return ret
    End Function

    Public Function CheckNULL(ByVal Val As Object) As String
        If (IsDBNull(Val)) Then
            Return ""
        Else
            Return Val
        End If
    End Function

    Protected Sub Btn_ATM_Search_Click(sender As Object, e As System.EventArgs) Handles Btn_ATM_Search.Click
        Dim DT As DataTable
        Try
            If (TXT_ATM_Search.Text = "") Then
                Lbl_Status.Text = "Please enter a valid ATM ID."
                Lbl_Status.Visible = True
            Else
                If (TXT_ATM_Search.Text = "ALL") Then
                    SqlDataSource1.SelectCommand = "Select * from ATM"
                    Session("ATMSearch") = False
                    Session("ATMSearchId") = "ALL"
                Else
                    SqlDataSource1.SelectCommand = "Select * from ATM where ATMID = '" & TXT_ATM_Search.Text & "'"
                    Session("ATMSearch") = True
                    Session("ATMSearchId") = TXT_ATM_Search.Text
                End If
                'MainFun.ConnectToDatabase("Select * from ATM where ATMID = '" & TXT_ATM_Search.Text & "'", Con.ConnectionString, DT)
                'GridView3.DataSource = DT
                'GridView3.DataBind()

                GridView3.DataBind()
            End If
        Catch ex As Exception
            Lbl_Status.Text = "Error: " & ex.ToString()
            Lbl_Status.Visible = True
        End Try
    End Sub

    Protected Sub GridView3_RowEditing(sender As Object, e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridView3.RowEditing
        If (Session("ATMSearch")) Then
            SqlDataSource1.SelectCommand = "Select * from ATM where ATMID = '" & Session("ATMSearchId") & "'"
        Else
            SqlDataSource1.SelectCommand = "Select * from ATM"
        End If
        GridView3.EditIndex = e.NewEditIndex
        GridView3.DataBind()
    End Sub

    Protected Sub GridView3_RowCancelingEdit(sender As Object, e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles GridView3.RowCancelingEdit
        If (Session("ATMSearch")) Then
            SqlDataSource1.SelectCommand = "Select * from ATM where ATMID = '" & Session("ATMSearchId") & "'"
        Else
            SqlDataSource1.SelectCommand = "Select * from ATM"
        End If
        GridView3.EditIndex = -1
        GridView3.DataBind()
    End Sub
End Class
