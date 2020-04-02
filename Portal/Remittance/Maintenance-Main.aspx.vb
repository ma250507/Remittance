
Partial Class Maintenance_Main
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsPostBack <> True) Then
            Dim UserPerm As Permissions
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

        End If
    End Sub

    Protected Sub Btn_Next_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btn_Next.Click
        If (RD_Maintenance.Checked) Then
            Response.Redirect("Maintenance.aspx")
        ElseIf (RD_Blocked.Checked) Then
            Response.Redirect("BlockedList.aspx")
        End If
    End Sub
End Class
