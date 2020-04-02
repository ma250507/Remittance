Imports System.Data.SqlClient

Partial Class Users_AddNormal
    Inherits System.Web.UI.Page


    Protected Sub ADD_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim NcrCrypt As New NCRCrypto
        Dim ID As String = DirectCast(GridView3.FooterRow.FindControl("Txt_UserId"), TextBox).Text
        Dim Name As String = DirectCast(GridView3.FooterRow.FindControl("Txt_UserName"), TextBox).Text
        Dim Password As String = DirectCast(GridView3.FooterRow.FindControl("Txt_Password"), TextBox).Text
        'Dim GroupID As String = DirectCast(GridView3.FooterRow.FindControl("drpd_Groups"), DropDownList).SelectedValue
        Dim Branch As String = DirectCast(GridView3.FooterRow.FindControl("drpd_br"), DropDownList).SelectedItem.Text
        'Dim TellerIPAddress As String = DirectCast(GridView3.FooterRow.FindControl("TXT_ADD_TellerIPAddress"), TextBox).Text
        Dim CountryCode As String = DirectCast(GridView3.FooterRow.FindControl("drpd_CountryCode"), DropDownList).SelectedItem.Value
        Dim BankCode As String = DirectCast(GridView3.FooterRow.FindControl("drpd_BankCode"), DropDownList).SelectedItem.Value
        Dim AllATMs As Boolean = DirectCast(GridView3.FooterRow.FindControl("CHB_AllATMs_ADD"), CheckBox).Checked
        Dim ret As Integer = -1

        SqlDataSource1.InsertParameters("UserId").DefaultValue = ID
        SqlDataSource1.InsertParameters("UserName").DefaultValue = Name
        SqlDataSource1.InsertParameters("Password").DefaultValue = NcrCrypt.eT3_Encrypt(Password)
        SqlDataSource1.InsertParameters("Group_ID").DefaultValue = Session("GroupId")
        SqlDataSource1.InsertParameters("Branch").DefaultValue = Branch
        SqlDataSource1.InsertParameters("CountryCode").DefaultValue = CountryCode
        SqlDataSource1.InsertParameters("BankCode").DefaultValue = BankCode
        SqlDataSource1.InsertParameters("AllATMs").DefaultValue = AllATMs
        'SqlDataSource1.InsertParameters("TellerIPAddress").DefaultValue = TellerIPAddress

        Try
            ret = SqlDataSource1.Insert()
            If (ret = 1) Then
                Log(Session("User"), "User Add", ID, Session("UserName"), Session("Branch"))
                UsersConBtn.Visible = True
            End If

        Catch ex As Exception
            Lbl_Status.Text = "Error While Adding User : " & ex.Message & ""
            Lbl_Status.Visible = True
        End Try
        NcrCrypt = Nothing
    End Sub
    Public Sub Log(ByVal UserID As String, ByVal Action As String, ByVal TransactionCode As String, ByVal Name As String, ByVal Branch As String)
        Dim ret As Integer
        Dim Com = New SqlCommand()
        Dim Con = New SqlConnection(ConfigurationManager.ConnectionStrings("NCRMoneyFerConnection").ConnectionString)
        Try
            Com.CommandText = "insert into useractions values('" & UserID & "','" & Action & "',getdate(),'" & TransactionCode & "','" & Name & "' , '" & Branch & "')"
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

    Protected Sub GridView3_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles GridView3.RowUpdating
        Dim NcrCrypt As New NCRCrypto
        Dim ID As String = DirectCast(GridView3.Rows(e.RowIndex).FindControl("Label1"), Label).Text
        Dim Name As String = DirectCast(GridView3.Rows(e.RowIndex).FindControl("TextBox1"), TextBox).Text
        Dim Password As String = DirectCast(GridView3.Rows(e.RowIndex).FindControl("TextBox3"), TextBox).Text
        Dim Branch As String = DirectCast(GridView3.Rows(e.RowIndex).FindControl("drpd_Br"), DropDownList).SelectedItem.Text
        'Dim TellerIPAddress As String = DirectCast(GridView3.Rows(e.RowIndex).FindControl("TXT_EDT_TellerIPAddress"), TextBox).Text
        Dim CountryCode As String = DirectCast(GridView3.Rows(e.RowIndex).FindControl("drpd_CountryCode"), DropDownList).SelectedItem.Value
        Dim BankCode As String = DirectCast(GridView3.Rows(e.RowIndex).FindControl("drpd_BankCode"), DropDownList).SelectedItem.Value
        Dim AllATMs As Boolean = DirectCast(GridView3.Rows(e.RowIndex).FindControl("CHB_AllATMs_EDIT"), CheckBox).Checked

        Dim ret As Integer = -1

        'Dim GroupID As String = DirectCast(GridView3.Rows(e.RowIndex).FindControl("drpd_EditGroups"), DropDownList).SelectedValue
        Dim RealOrNot As String 'To check if the password is encrypted or NOT
        Dim FirstTime As Integer

        If (Password.Length > 10) Then
            RealOrNot = Password
            Lbl_Notification.Text = "Original Password Saved."
            FirstTime = 1
        Else
            RealOrNot = NcrCrypt.eT3_Encrypt(Password)
            Lbl_Notification.Text = "Original Password Updated."
            FirstTime = 0
        End If
        SqlDataSource1.UpdateParameters("original_UserId").DefaultValue = ID
        SqlDataSource1.UpdateParameters("UserName").DefaultValue = Name
        SqlDataSource1.UpdateParameters("Password").DefaultValue = RealOrNot
        SqlDataSource1.UpdateParameters("Branch").DefaultValue = Branch
        SqlDataSource1.UpdateParameters("FirstTime").DefaultValue = FirstTime
        'SqlDataSource1.UpdateParameters("TellerIPAddress").DefaultValue = TellerIPAddress
        SqlDataSource1.UpdateParameters("CountryCode").DefaultValue = CountryCode
        SqlDataSource1.UpdateParameters("BankCode").DefaultValue = BankCode
        SqlDataSource1.UpdateParameters("AllATMs").DefaultValue = AllATMs
        'SqlDataSource1.UpdateParameters("Group_ID").DefaultValue = GroupID

        Try

            ret = SqlDataSource1.Update()
            If (ret = 1) Then
                Log(Session("User"), "User Edit", ID, Session("UserName"), Session("Branch"))
                UsersConBtn.Visible = True
                Lbl_Notification.Visible = True
            End If

        Catch ex As Exception
            Lbl_Status.Text = "Error While Updating User."
            Lbl_Status.Visible = True
        End Try

    End Sub

    Protected Sub GridView3_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView3.RowDeleting
        Dim ID As String = DirectCast(GridView3.Rows(e.RowIndex).FindControl("Label1"), Label).Text
        Dim ret As Integer = -1
        SqlDataSource1.DeleteParameters("original_UserId").DefaultValue = ID

        Try
            ret = SqlDataSource1.Delete()
            If (ret = 1) Then
                Log(Session("User"), "User Delete", ID, Session("UserName"), Session("Branch"))
                UsersConBtn.Visible = True
            End If
        Catch ex As Exception
            Lbl_Status.Text = "Error While Deleting User."
            Lbl_Status.Visible = True
        End Try
    End Sub

    Protected Sub UsersConBtn_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles UsersConBtn.Click
        Response.Redirect("Users-Main.aspx")
    End Sub

    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Protected Sub GridView3_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView3.RowCommand
        If (e.CommandName = "New") Then
            Dim row As GridViewRow = DirectCast(e.CommandSource, LinkButton).NamingContainer
            Dim ID As String = DirectCast(row.FindControl("TXTEUID"), TextBox).Text
            Dim Name As String = DirectCast(row.FindControl("TXTEUName"), TextBox).Text
            Dim Password As String = DirectCast(row.FindControl("TXTEPassword"), TextBox).Text
            Dim Branch As String = DirectCast(row.FindControl("drpd_Br"), DropDownList).SelectedItem.Text

            Dim CountryCode As String = DirectCast(row.FindControl("drpd_CountryCode"), DropDownList).SelectedItem.Value
            Dim BankCode As String = DirectCast(row.FindControl("drpd_BankCode"), DropDownList).SelectedItem.Value
            Dim AllATMs As Boolean = DirectCast(row.FindControl("CHB_AllATMs_eADD"), CheckBox).Checked
            Dim ret As Integer = -1


            Dim NcrCrypt As New NCRCrypto

            SqlDataSource1.InsertParameters("UserId").DefaultValue = ID
            SqlDataSource1.InsertParameters("UserName").DefaultValue = Name
            SqlDataSource1.InsertParameters("Password").DefaultValue = NcrCrypt.eT3_Encrypt(Password)
            SqlDataSource1.InsertParameters("Group_ID").DefaultValue = Session("GroupId")
            SqlDataSource1.InsertParameters("Branch").DefaultValue = Branch
            SqlDataSource1.InsertParameters("CountryCode").DefaultValue = CountryCode
            SqlDataSource1.InsertParameters("BankCode").DefaultValue = BankCode
            SqlDataSource1.InsertParameters("AllATMs").DefaultValue = AllATMs

            Try
                ret = SqlDataSource1.Insert()
                If (ret = 1) Then
                    Log(Session("User"), "User Add", Name, Session("UserName"), Session("Branch"))
                    UsersConBtn.Visible = True
                End If
            Catch ex As Exception
                Lbl_Status.Text = "Error While Adding User."
                Lbl_Status.Visible = True
            End Try
            NcrCrypt = Nothing
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
