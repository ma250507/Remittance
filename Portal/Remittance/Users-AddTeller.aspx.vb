Imports System.Data
Imports System.Data.SqlClient

Partial Class Users_AddTeller
    Inherits System.Web.UI.Page

    Private NcrCrypt As NCRCrypto
    Private Con As New SqlConnection(ConfigurationManager.ConnectionStrings("NCRMoneyFerConnection").ConnectionString)
    Private dr As SqlDataReader
    Private Com As SqlCommand
    Protected Sub ADD_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim ID As String = (DirectCast(GridView3.FooterRow.FindControl("Txt_UserId"), TextBox)).Text
        Dim Name As String = (DirectCast(GridView3.FooterRow.FindControl("Txt_UserName"), TextBox)).Text
        Dim Password As String = (DirectCast(GridView3.FooterRow.FindControl("Txt_Password"), TextBox)).Text
        'Dim GroupID As String = (DirectCast(GridView3.FooterRow.FindControl("drpd_Groups"), DropDownList)).SelectedValue
        Dim ATMID As String = (DirectCast(GridView3.FooterRow.FindControl("Txt_ATMID"), TextBox)).Text
        Dim Countrycode As String = (DirectCast(GridView3.FooterRow.FindControl("drpd_CountryCode"), DropDownList)).SelectedValue
        Dim BankCode As String = (DirectCast(GridView3.FooterRow.FindControl("drpd_BankCode"), DropDownList)).SelectedValue
        'Dim IP As String = (DirectCast(GridView3.FooterRow.FindControl("Txt_IPAddress"), TextBox)).Text
        Dim Teller As CheckBox = (DirectCast(GridView3.FooterRow.FindControl("CB_IsTeller"), CheckBox))
        Dim Branch As String = (DirectCast(GridView3.FooterRow.FindControl("drpd_BR"), DropDownList)).SelectedItem.Text
        Dim TellerIPAddress As String = (DirectCast(GridView3.FooterRow.FindControl("TXT_ADD_TellerIPAddress"), TextBox)).Text
        Dim AllATMs As Boolean = (DirectCast(GridView3.FooterRow.FindControl("CHB_AllATMs_ADD"), CheckBox)).Checked

        Com = New SqlCommand

        Dim myTrans As SqlTransaction
        'Dim TellerCheck As String
        Dim intTeller As Integer

        If (Teller.Checked) Then
            intTeller = 1
        Else
            intTeller = 0
        End If

        Try
            Con.Open()
            myTrans = Con.BeginTransaction(IsolationLevel.ReadCommitted, "SampleTransaction")
            Com.Connection = Con
            Com.Transaction = myTrans
        Catch ex As Exception
            Lbl_Status.Text = "Unable to open the connection or to begin the transaction."
            Lbl_Status.Visible = True
        End Try

        Dim ret As Integer = 0

        NcrCrypt = New NCRCrypto()
        If (ID = "" Or ID = Nothing Or Password = "" Or Password = Nothing) Then
            Lbl_Status.Visible = True
            Lbl_Status.Text = "Can`t insert empty fields"
            Return
        End If

        Try
            'If (TellerCheck = "True") Then
            Com.CommandText = "Insert into ATM ([ATMId],[ATMLocation],[CountryCode],[BankCode],[Cassitte1Value],[Cassitte2Value],[Cassitte3Value],[Cassitte4Value],[ATMIPAddress],[IsTeller],[ATMName],[TerminalId]) VALUES ('" & ATMID & "', '" & Branch & "','" & Countrycode & "','" & BankCode & "','0','0','0','0','" & Session("ServiceIP") & "','" & intTeller & "','" & Name & "','" & ATMID & "')"
            'MsgBox(Com.CommandText)
            Com.ExecuteNonQuery()
            Com.CommandText = "Insert into users ([UserId],[UserName],[Password],[Group_ID],[ATM_ID],[CountryCode],[BankCode],[Branch],[FirstTime],[TellerIPAddress],[AllATMs],[Locked]) VALUES ('" & ID & "', '" & Name & "','" & NcrCrypt.eT3_Encrypt(Password) & "','" & Session("GroupID") & "','" & ATMID & "','" & Countrycode & "','" & BankCode & "','" & Branch & "',0,'" & TellerIPAddress & "', '" & AllATMs & "',0)"
            'MsgBox(Com.CommandText)
            Com.ExecuteNonQuery()
            'Else
            '    Com.CommandText = "Insert into users VALUES ('" & ID & "', '" & Name & "','" & NcrCrypt.eT3_Encrypt(Password) & "','" & GroupID & "','','','')"
            '    Com.ExecuteNonQuery()
            'End If

            myTrans.Commit()

            UsersConBtn.Visible = True
            GridView3.DataBind()
            Log(Session("User"), "User Add", Name, Session("UserName"), Session("Branch"))
        Catch myex As Exception

            Try
                myTrans.Rollback("SampleTransaction")
            Catch ex As SqlException
                If Not myTrans.Connection Is Nothing Then
                    Lbl_Status.Text = "An exception of type " & ex.GetType().ToString() & _
                                       " was encountered while attempting to roll back the transaction."
                    Lbl_Status.Visible = True
                End If
            End Try
            If (myex.Message.Contains("duplicate")) Then
                Lbl_Status.Text = "Another User or Terminal exists in the Database Please Review first the users and contact your administrator."
                Lbl_Status.Visible = True
            Else
                Lbl_Status.Text = "An exception of type " & myex.Message & " was encountered while inserting the data, No record was written to database."
                Lbl_Status.Visible = True
            End If


        Finally
            Con.Close()
        End Try
    End Sub
    Public Sub Log(ByVal UserID As String, ByVal Action As String, ByVal TransactionCode As String, ByVal Name As String, ByVal Branch As String)
        Dim ret As Integer
        Dim Com = New SqlCommand()
        Dim Con = New SqlConnection(ConfigurationManager.ConnectionStrings("NCRMoneyFerConnection").ConnectionString)
        Try
            Com.CommandText = "insert into useractions values('" & UserID & "','" & Action & "',getdate(),'" & TransactionCode & "','" & Name & "', '" & Branch & "')"
            Com.Connection = Con
            Con.Open()
            ret = Com.ExecuteNonQuery()
        Catch ex As Exception
            Lbl_Status.Text = "Error while Logging Event :" & ex.Message & ""
            Lbl_Status.Visible = True
        End Try


        If (ret <> 1) Then
            Lbl_Status.Text = "Error while logging Event."
            Lbl_Status.Visible = True
            Return
        End If
        Con.Close()
    End Sub
    Protected Sub GridView3_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles GridView3.RowUpdating
        Dim ID As String = (DirectCast(GridView3.Rows(e.RowIndex).FindControl("Label1"), Label)).Text
        Dim Name As String = (DirectCast(GridView3.Rows(e.RowIndex).FindControl("TextBox1"), TextBox)).Text
        Dim Password As String = (DirectCast(GridView3.Rows(e.RowIndex).FindControl("TextBox3"), TextBox)).Text
        'Dim GroupID As String = (DirectCast(GridView3.Rows(e.RowIndex).FindControl("drpd_EditGroups"), DropDownList)).SelectedValue
        'Dim IP As String = (DirectCast(GridView3.Rows(e.RowIndex).FindControl("TextBox7"), TextBox)).Text
        Dim Teller As CheckBox = (DirectCast(GridView3.Rows(e.RowIndex).FindControl("CheckBox1"), CheckBox))
        Dim ATMID As String = (DirectCast(GridView3.Rows(e.RowIndex).FindControl("Label12"), Label)).Text
        Dim Branch As String = (DirectCast(GridView3.Rows(e.RowIndex).FindControl("drpd_Br"), DropDownList)).SelectedItem.Text
        Dim TellerIPAddress As String = (DirectCast(GridView3.Rows(e.RowIndex).FindControl("TXT_EDT_TellerIPAddress"), TextBox)).Text
        Dim AllATMs As Boolean = (DirectCast(GridView3.Rows(e.RowIndex).FindControl("CHB_AllATMs_EDIT"), CheckBox)).Checked
        Dim ret As Integer = -1
        Dim intTeller As Integer
        Dim RealOrNot As String 'To check if the password is encrypted or NOT
        Dim FirstTime As Integer

        NcrCrypt = New NCRCrypto
        If (Password.Length > 10) Then
            RealOrNot = Password
            Lbl_Notification.Text = "Original Password Saved."
            FirstTime = 1
        Else
            RealOrNot = NcrCrypt.eT3_Encrypt(Password)
            Lbl_Notification.Text = "Original Password Updated."
            FirstTime = 0
        End If




        If (Teller.Checked) Then
            intTeller = 1
        Else
            intTeller = 0
        End If


        SqlDataSource1.UpdateCommand = "BEGIN TRANSACTION " & _
                                       "UPDATE [Users] SET [UserName] = @Name , [Password] = @Password ,[Branch] = @Branch ,[FirstTime] = @FirstTime,TellerIPAddress = @TellerIPAddress ,AllATMs = @AllATMS " & _
                                       "WHERE [UserId] = @original_UserId " & _
                                         "IF @@ERROR != 0 " & _
                                            "BEGIN " & _
                                            "ROLLBACK TRANSACTION " & _
                                            "Return " & _
                                            "End " & _
                                            "Else " & _
                                       "UPDATE atm SET  IsTeller=@Teller ,[ATMLocation] = @Branch " & _
                                       "WHERE [ATMID] = @ATM_ID " & _
                                         "IF @@ERROR != 0 " & _
                                            "BEGIN " & _
                                            "ROLLBACK TRANSACTION " & _
                                            "Return " & _
                                            "End " & _
                                                "Else " & _
                                                    "COMMIT TRANSACTION " & _
                                                    "GO"

        SqlDataSource1.UpdateParameters.Add("Name", Name)
        SqlDataSource1.UpdateParameters.Add("Password", RealOrNot)
        'SqlDataSource1.UpdateParameters.Add("Group_ID", GroupID)
        SqlDataSource1.UpdateParameters.Add("original_UserId", ID)
        'SqlDataSource1.UpdateParameters.Add("IP", IP)
        SqlDataSource1.UpdateParameters.Add("Teller", intTeller)
        SqlDataSource1.UpdateParameters.Add("ATM_ID", ATMID)
        SqlDataSource1.UpdateParameters.Add("Branch", Branch)
        SqlDataSource1.UpdateParameters.Add("FirstTime", FirstTime)
        SqlDataSource1.UpdateParameters.Add("TellerIPAddress", TellerIPAddress)
        SqlDataSource1.UpdateParameters.Add("AllATMs", AllATMs)



        Try
            ret = SqlDataSource1.Update()
            If (ret = 2) Then
                Log(Session("User"), "User Edit", ID, Session("UserName"), Session("Branch"))
                UsersConBtn.Visible = True
                Lbl_Notification.Visible = True
            End If
        Catch ex As Exception
            Lbl_Status.Visible = True
            Lbl_Status.Text = "Error while Updating user: " & ex.Message & ""
        End Try
    End Sub

    Protected Sub GridView3_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView3.RowDeleting

        Dim ID As String = (DirectCast(GridView3.Rows(e.RowIndex).FindControl("Label1"), Label)).Text
        Dim ATMID As String = (DirectCast(GridView3.Rows(e.RowIndex).FindControl("Label5"), Label)).Text
        Dim ret As Integer = -1
        
        SqlDataSource1.DeleteCommand = "BEGIN TRANSACTION OuterTran " & _
                                       "DELETE FROM users WHERE userid=@UserId " & _
                                       "COMMIT TRANSACTION OuterTran "
        '"DELETE FROM atm WHERE atmid=@AtmId " & _

        SqlDataSource1.DeleteParameters.Add("UserId", ID)
        SqlDataSource1.DeleteParameters.Add("AtmId", ATMID)

        Try
            ret = SqlDataSource1.Delete()
            If (ret = 1) Then
                Log(Session("User"), "User Delete", ID, Session("UserName"), Session("Branch"))
                UsersConBtn.Visible = True
            End If
        Catch ex As Exception
            Lbl_Status.Visible = True
            Lbl_Status.Text = "Error while Deleting user: " & ex.Message & ""
        End Try
        If (ID = Session("User")) Then
            Response.Redirect("Login.aspx")
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsPostBack <> True) Then
            Try
                Dim UserPerm As Permissions
                UserPerm = New Permissions()
                UserPerm = Session("Perm")
                If (UserPerm.Users <> "True") Then
                    Response.Redirect("Login.aspx")
                End If
            Catch ex As Exception
                Response.Redirect("Login.aspx")
            End Try
        Else
            Lbl_Status.Visible = False
            UsersConBtn.Visible = False
            Lbl_Notification.Visible = False
        End If
    End Sub

    Protected Sub UsersConBtn_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles UsersConBtn.Click
        Response.Redirect("Users-Main.aspx")
    End Sub

    Protected Sub GridView3_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView3.RowCommand
        If (e.CommandName = "EmptyNew") Then
            Dim row As GridViewRow = DirectCast(e.CommandSource, Button).NamingContainer

            Dim ID As String = (DirectCast(row.FindControl("Txt_ID"), TextBox)).Text
            Dim Name As String = (DirectCast(row.FindControl("Txt_Name"), TextBox)).Text
            Dim Password As String = (DirectCast(row.FindControl("Txt_PWD"), TextBox)).Text
            Dim ATMID As String = (DirectCast(row.FindControl("Txt_ATMID"), TextBox)).Text
            Dim Countrycode As String = (DirectCast(row.FindControl("drpd_CC"), DropDownList)).SelectedValue
            Dim BankCode As String = (DirectCast(row.FindControl("drpd_BC"), DropDownList)).SelectedValue
            Dim Teller As CheckBox = (DirectCast(row.FindControl("CB_Tel"), CheckBox))
            Dim Branch As String = (DirectCast(row.FindControl("drpd_Br"), DropDownList)).SelectedItem.Text
            Dim TellerIPAddress As String = (DirectCast(row.FindControl("Txt_IPAddress"), TextBox)).Text
            Dim AllATMs As Boolean = (DirectCast(row.FindControl("CHB_AllATMs_eADD"), CheckBox)).Checked

            Com = New SqlCommand

            Dim myTrans As SqlTransaction
            'Dim TellerCheck As String
            Dim intTeller As Integer

            If (Teller.Checked) Then
                intTeller = 1
            Else
                intTeller = 0
            End If
            'Try
            '    Con.Open()
            '    Com.Connection = Con
            '    Com.CommandText = "select teller from groups where id='" & GroupID & "'"
            '    dr = Com.ExecuteReader()
            '    While (dr.Read())
            '        TellerCheck = dr(0).ToString()
            '    End While
            '    dr.Close()
            '    Con.Close()
            'Catch ex As Exception
            '    Lbl_Status.Text = "Unable to open the connection or DB Error."
            '    Lbl_Status.Visible = True
            'End Try

            Try
                Con.Open()
                myTrans = Con.BeginTransaction(IsolationLevel.ReadCommitted, "SampleTransaction")
                Com.Connection = Con
                Com.Transaction = myTrans
            Catch ex As Exception
                Lbl_Status.Text = "Unable to open the connection or to begin the transaction."
                Lbl_Status.Visible = True
            End Try

            Dim ret As Integer = 0

            NcrCrypt = New NCRCrypto()
            If (ID = "" Or ID = Nothing Or Password = "" Or Password = Nothing) Then
                Lbl_Status.Visible = True
                Lbl_Status.Text = "Can`t insert empty fields"
                Return
            End If

            Try
                'If (TellerCheck = "True") Then             ('" & ATMID & "', '" & Branch & "','" & Countrycode & "','" & BankCode & "','200','100','50','20','" & Session("ServiceIP") & "','" & intTeller & "','" & Name & "')"
                'Com.CommandText = "Insert into ATM  VALUES ('" & ATMID & "', '" & Branch & "','" & Countrycode & "','" & BankCode & "','200','100','50','20','" & Session("ServiceIP") & "','" & intTeller & "','" & Name & "')"
                'Com.ExecuteNonQuery()
                'Com.CommandText = "Insert into users VALUES ('" & ID & "', '" & Name & "','" & NcrCrypt.eT3_Encrypt(Password) & "','" & Session("GroupID") & "','" & ATMID & "','" & Countrycode & "','" & BankCode & "','" & Branch & "',0,'" & TellerIPAddress & "', '" & AllATMs & "')"
                'Com.ExecuteNonQuery()



                Com.CommandText = "Insert into ATM ([ATMId],[ATMLocation],[CountryCode],[BankCode],[Cassitte1Value],[Cassitte2Value],[Cassitte3Value],[Cassitte4Value],[ATMIPAddress],[IsTeller],[ATMName],[TerminalId]) VALUES ('" & ATMID & "', '" & Branch & "','" & Countrycode & "','" & BankCode & "','0','0','0','0','" & Session("ServiceIP") & "','" & intTeller & "','" & Name & "','" & ATMID & "')"
                'MsgBox(Com.CommandText)
                Com.ExecuteNonQuery()
                Com.CommandText = "Insert into users ([UserId],[UserName],[Password],[Group_ID],[ATM_ID],[CountryCode],[BankCode],[Branch],[FirstTime],[TellerIPAddress],[AllATMs],[Locked]) VALUES ('" & ID & "', '" & Name & "','" & NcrCrypt.eT3_Encrypt(Password) & "','" & Session("GroupID") & "','" & ATMID & "','" & Countrycode & "','" & BankCode & "','" & Branch & "',0,'" & TellerIPAddress & "', '" & AllATMs & "',0)"
                'MsgBox(Com.CommandText)
                Com.ExecuteNonQuery()



                'Else
                '    Com.CommandText = "Insert into users VALUES ('" & ID & "', '" & Name & "','" & NcrCrypt.eT3_Encrypt(Password) & "','" & GroupID & "','','','')"
                '    Com.ExecuteNonQuery()
                'End If

                myTrans.Commit()

                UsersConBtn.Visible = True
                GridView3.DataBind()
                Log(Session("User"), "User Add", ID, Session("UserName"), Session("Branch"))
            Catch myex As Exception

                Try
                    myTrans.Rollback("SampleTransaction")
                Catch ex As SqlException
                    If Not myTrans.Connection Is Nothing Then
                        Lbl_Status.Text = "An exception of type " & ex.GetType().ToString() & _
                                           " was encountered while attempting to roll back the transaction."
                        Lbl_Status.Visible = True
                    End If
                End Try
                If (myex.Message.Contains("duplicate")) Then
                    Lbl_Status.Text = "Another User or Terminal exists in the Database Please Review first the users and contact your administrator."
                    Lbl_Status.Visible = True
                Else
                    Lbl_Status.Text = "An exception of type " & myex.Message & " was encountered while inserting the data, No record was written to database."
                    Lbl_Status.Visible = True
                End If


            Finally
                Con.Close()
            End Try

        End If
    End Sub
    Public Function CheckNULL(ByVal Val As Object) As Boolean
        If (IsDBNull(Val)) Then
            Return False
        Else
            Return Val
        End If
    End Function
End Class
