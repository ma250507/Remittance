
Partial Class BlockedList
    Inherits System.Web.UI.Page

    Protected Sub ADD_UnBlock_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim MobileNumber As String = DirectCast(GridView2.FooterRow.FindControl("TXT_MobileNumber"), TextBox).Text
        Dim DepositorOrBeneficiary As String = DirectCast(GridView2.FooterRow.FindControl("drpd_DepOrBen"), DropDownList).SelectedValue
        Dim BlockReason As String = DirectCast(GridView2.FooterRow.FindControl("TXT_BlockReason"), TextBox).Text



        Try
            SqlDataSource2.InsertParameters("MobileNumber").DefaultValue = MobileNumber
            SqlDataSource2.InsertParameters("DepositorOrBeneficiary").DefaultValue = CType(DepositorOrBeneficiary, Boolean)
            SqlDataSource2.InsertParameters("BlockDateTime").DefaultValue = DateTime.Now()
            SqlDataSource2.InsertParameters("BlockReason").DefaultValue = BlockReason
            SqlDataSource2.InsertParameters("UnBlocked").DefaultValue = CType(0, Boolean)
            SqlDataSource2.InsertParameters("UserId").DefaultValue = Session("User")

            SqlDataSource2.Insert()

            BlockConBTN.Visible = True
        Catch ex As Exception
            Lbl_Status.Text = "Error While Adding."
            Lbl_Status.Visible = True
        End Try


    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsPostBack <> True) Then
            Dim UserPerm As Permissions
            UserPerm = New Permissions()
            Try
                UserPerm = New Permissions()
                UserPerm = Session("Perm")
                If (UserPerm.Maintenance <> "True") Then
                    UserPerm = Nothing
                    Response.Redirect("Login.aspx")
                End If
            Catch ex As Exception
                UserPerm = Nothing
                Response.Redirect("Login.aspx")
            End Try
        Else
            Lbl_Status.Visible = False
            BlockConBTN.Visible = False
        End If
    End Sub

    Protected Sub GridView2_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles GridView2.RowUpdating
        Dim MobileNumber As String = DirectCast(GridView2.Rows(e.RowIndex).FindControl("TextBox1"), TextBox).Text
        Dim DepositorOrBeneficiary As String = DirectCast(GridView2.Rows(e.RowIndex).FindControl("drpd_DepOrBen"), DropDownList).SelectedValue
        Dim BlockReason As String = DirectCast(GridView2.Rows(e.RowIndex).FindControl("TextBox2"), TextBox).Text
        Dim UnBlock As CheckBox = DirectCast(GridView2.Rows(e.RowIndex).FindControl("CheckBox2"), CheckBox)
        Dim ID As String = DirectCast(GridView2.Rows(e.RowIndex).FindControl("Label1"), Label).Text
        Dim BlockDateTime As String = DirectCast(GridView2.Rows(e.RowIndex).FindControl("Label6"), Label).Text

        Dim CMD As String = "UPDATE [BlockedCustomers] SET [MobileNumber] = '" & MobileNumber & "', [DepositorOrBeneficiary] = " & Convert.ToInt32(DepositorOrBeneficiary) & ", [BlockReason] = '" & BlockReason & "', [UnBlocked] = " & Convert.ToInt32(UnBlock.Checked) & ", [UserId] = '" & Session("User") & "'"


        If (UnBlock.Checked) Then
            CMD = CMD & " ,[UnBlockDateTime] = '" & DateTime.Now().ToString("yyyy-MM-dd HH:mm:ss") & "'  WHERE [ID] = '" & ID & "'"
        Else
            CMD = CMD & " ,[UnBlockDateTime] = NULL WHERE [ID] = '" & ID & "'"
        End If




        Try
            'MsgBox(CMD)
            SqlDataSource2.UpdateCommand = CMD
            SqlDataSource2.Update()
            BlockConBTN.Visible = True
        Catch ex As Exception
            Lbl_Status.Text = "Error While Updating."
            Lbl_Status.Visible = True
        End Try
    End Sub

    Protected Sub BlockConBTN_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BlockConBTN.Click
        Response.Redirect("Maintenance-Main.aspx")
    End Sub

    Protected Sub GridView2_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView2.RowCommand
        If (e.CommandName = "EBlockADD") Then
            Dim row As GridViewRow = DirectCast(e.CommandSource, Button).NamingContainer

            Dim MobileNumber As String = DirectCast(row.FindControl("TXT_EMobileNumber"), TextBox).Text
            Dim DepOrBen As String = DirectCast(row.FindControl("drpd_EDepOrBen"), DropDownList).SelectedValue
            Dim BlockReason As String = DirectCast(row.FindControl("TXT_EBlockReason"), TextBox).Text


            SqlDataSource2.InsertParameters("MobileNumber").DefaultValue = MobileNumber
            SqlDataSource2.InsertParameters("DepositorOrBeneficiary").DefaultValue = CType(DepOrBen, Boolean)
            SqlDataSource2.InsertParameters("BlockDateTime").DefaultValue = DateTime.Now()
            SqlDataSource2.InsertParameters("BlockReason").DefaultValue = BlockReason
            SqlDataSource2.InsertParameters("UnBlocked").DefaultValue = CType(0, Boolean)
            SqlDataSource2.InsertParameters("UserId").DefaultValue = Session("User")

            Try
                SqlDataSource2.Insert()
                BlockConBTN.Visible = True
            Catch ex As Exception
                Lbl_Status.Text = "Error While Adding ."
                Lbl_Status.Visible = True
            End Try

        End If
    End Sub
End Class
