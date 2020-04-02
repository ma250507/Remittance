
Partial Class Teller_Main
    Inherits System.Web.UI.Page

    Private UserPerm As Permissions
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
        Else

        End If
    End Sub

    Protected Sub Btn_Next_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btn_Next.Click
        If (RD_Deposit.Checked) Then
            Response.Redirect("Deposit.aspx")
        ElseIf (RD_Withdraw.Checked) Then
            Response.Redirect("Withdraw.aspx")
        ElseIf (RD_Redemption.Checked) Then
            Response.Redirect("Redemption.aspx")
        End If

    End Sub
End Class
