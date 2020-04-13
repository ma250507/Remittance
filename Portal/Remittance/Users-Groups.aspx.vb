Imports NCRCrypto

Partial Class Users_Groups
    Inherits System.Web.UI.Page


    Private UserPerm As Permissions
    Protected Sub GroupADD_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim NcrCrypt As NCRCrypto
        Dim ID As String = (DirectCast(GridView2.FooterRow.FindControl("TXT_GroupId"), TextBox)).Text
        Dim Name As String = (DirectCast(GridView2.FooterRow.FindControl("TXT_GroupName"), TextBox)).Text
        Dim Reports As CheckBox = (DirectCast(GridView2.FooterRow.FindControl("CB_Reports"), CheckBox))
        Dim Administration As CheckBox = (DirectCast(GridView2.FooterRow.FindControl("CB_Admin"), CheckBox))
        Dim Maintenance As CheckBox = (DirectCast(GridView2.FooterRow.FindControl("CB_Maintenance"), CheckBox))
        Dim Users As CheckBox = (DirectCast(GridView2.FooterRow.FindControl("CB_Users"), CheckBox))
        Dim Teller As CheckBox = (DirectCast(GridView2.FooterRow.FindControl("CB_Teller"), CheckBox))
        Dim Register As CheckBox = (DirectCast(GridView2.FooterRow.FindControl("CB_Registeration"), CheckBox))
        Dim BulkTransactionsReports As CheckBox = (DirectCast(GridView2.FooterRow.FindControl("CB_BulkTransactionsReports"), CheckBox))
        Dim ret As Integer = 0

        NcrCrypt = New NCRCrypto()
        If (ID = "" Or ID = Nothing Or Name = "" Or Name = Nothing) Then
            Lbl_Status.Visible = True
            Lbl_Status.Text = "Can`t insert empty fields"
            Return
        End If

        'SqlDataSource2.InsertParameters("ID").DefaultValue = ID
        'SqlDataSource2.InsertParameters("Name").DefaultValue = Name
        'SqlDataSource2.InsertParameters("Reports").DefaultValue = Reports.Checked
        'SqlDataSource2.InsertParameters("Users").DefaultValue = Users.Checked
        'SqlDataSource2.InsertParameters("Teller").DefaultValue = Teller.Checked
        'SqlDataSource2.InsertParameters("Maintenance").DefaultValue = Maintenance.Checked
        'SqlDataSource2.InsertParameters("Administration").DefaultValue = Administration.Checked
        'SqlDataSource2.InsertParameters("Registeration").DefaultValue = Register.Checked

        SqlDataSource2.InsertCommand = "INSERT INTO [Groups] ([ID], [Name], [Reports], [Maintenance], [Administration], [Users], [Teller], [Registeration],[BulkTransactionsReports] ) VALUES (" & ID & ",'" & Name & "'," & Convert.ToInt32(Reports.Checked) & ", " & Convert.ToInt32(Maintenance.Checked) & ", " & Convert.ToInt32(Administration.Checked) & ", " & Convert.ToInt32(Users.Checked) & ", " & Convert.ToInt32(Teller.Checked) & "," & Convert.ToInt32(Register.Checked) & "," & Convert.ToInt32(BulkTransactionsReports.Checked) & ")"

        Try
            SqlDataSource2.Insert()
            AdminConBTN.Visible = True
        Catch ex As Exception
            Lbl_Status.Visible = True
            Lbl_Status.Text = "Error while adding Grouop: " & ex.Message & ""
        End Try

    End Sub
    Protected Sub GridView2_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles GridView2.RowUpdating
        Dim ID As String = (DirectCast(GridView2.Rows(e.RowIndex).FindControl("Label1"), Label)).Text
        Dim Name As String = (DirectCast(GridView2.Rows(e.RowIndex).FindControl("TextBox1"), TextBox)).Text
        Dim Reports As CheckBox = (DirectCast(GridView2.Rows(e.RowIndex).FindControl("CheckBox1"), CheckBox))
        Dim Administration As CheckBox = (DirectCast(GridView2.Rows(e.RowIndex).FindControl("CheckBox2"), CheckBox))
        Dim Maintenance As CheckBox = (DirectCast(GridView2.Rows(e.RowIndex).FindControl("CheckBox3"), CheckBox))
        Dim Users As CheckBox = (DirectCast(GridView2.Rows(e.RowIndex).FindControl("CheckBox4"), CheckBox))
        Dim Teller As CheckBox = (DirectCast(GridView2.Rows(e.RowIndex).FindControl("CheckBox6"), CheckBox))
        Dim Register As CheckBox = (DirectCast(GridView2.Rows(e.RowIndex).FindControl("CheckBox8"), CheckBox))
        Dim BulkTransactionsReports As CheckBox = (DirectCast(GridView2.Rows(e.RowIndex).FindControl("cb_BulkTransactionsReports_Edit"), CheckBox))

        'SqlDataSource2.UpdateParameters("original_ID").DefaultValue = ID
        'SqlDataSource2.UpdateParameters("Name").DefaultValue = Name
        'SqlDataSource2.UpdateParameters("Reports").DefaultValue = Convert.ToInt32(Reports.Checked)
        'SqlDataSource2.UpdateParameters("Users").DefaultValue = CType(, Integer)
        'SqlDataSource2.UpdateParameters("Teller").DefaultValue = CType, Integer)
        'SqlDataSource2.UpdateParameters("Maintenance").DefaultValue = CType(, Integer)
        'SqlDataSource2.UpdateParameters("Administration").DefaultValue = CType(, Integer)
        'SqlDataSource2.UpdateParameters("Registeration").DefaultValue = CType(, Integer)

        SqlDataSource2.UpdateCommand = "UPDATE [Groups] SET [Name] = '" & Name & "', [Reports] = " & Convert.ToInt32(Reports.Checked) & ", [Maintenance] = " & Convert.ToInt32(Maintenance.Checked) & ", [Administration] = " & Convert.ToInt32(Administration.Checked) & "" &
                                       " , [Users] = " & Convert.ToInt32(Users.Checked) & ", [Teller] = " & Convert.ToInt32(Teller.Checked) & " , [Registeration] = " & Convert.ToInt32(Register.Checked) & " , [BulkTransactionsReports] = " & Convert.ToInt32(BulkTransactionsReports.Checked) & " WHERE [ID] = " & ID & ""

        Try

            SqlDataSource2.Update()
            AdminConBTN.Visible = True
        Catch ex As Exception
            Lbl_Status.Visible = True
            Lbl_Status.Text = "Error while editing Group: " & ex.Message & ""
        End Try

    End Sub
    Protected Sub AdminConBTN_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AdminConBTN.Click
        Response.Redirect("Default.aspx")
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If (Session("Status") = "InValid_user" Or Session("Status") = Nothing) Then
            Response.Redirect("Login.aspx")
        End If
        If (IsPostBack <> True) Then
            Try
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
            AdminConBTN.Visible = False
        End If
    End Sub
End Class
