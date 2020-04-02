
Partial Class MasterPage
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsPostBack <> True) Then
            Dim perm As Permissions
            perm = Session("Perm")
            'Dim content As ContentPlaceHolder
            'content = Page.Master.FindControl("ContentPlaceHolder5")


            Dim ctrl As New Control
            If (perm Is Nothing) Then
                Admin.Visible = False
                Maintenance.Visible = False
                Report.Visible = False
                Users.Visible = False
                Teller.Visible = False
                Register.Visible = False
                Btn_ChangePassword.Visible = False
                Btn_Home.Visible = False
                Btn_LogOut.Visible = False
            Else
                If (perm.Administration = "True") Then
                    Admin.Visible = True
                Else
                    Admin.Visible = False
                End If
                If (perm.Maintenance = "True") Then
                    Maintenance.Visible = True
                Else
                    Maintenance.Visible = False
                End If
                If (perm.Reports = "True") Then
                    Report.Visible = True
                Else
                    Report.Visible = False
                End If
                If (perm.Users = "True") Then
                    Users.Visible = True
                Else
                    Users.Visible = False
                End If
                If (perm.Teller = "True") Then
                    Teller.Visible = True
                Else
                    Teller.Visible = False
                End If

                If (perm.Register = "True") Then
                    Register.Visible = True
                Else
                    Register.Visible = False
                End If
                Btn_ChangePassword.Visible = True
            End If
        Else

        End If
    End Sub

    Protected Sub Btn_LogOut_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btn_LogOut.Click
        Session.Abandon()
        Response.Redirect("~/login.aspx")
    End Sub

    Protected Sub Btn_Home_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btn_Home.Click
        Response.Redirect("~/Default.aspx")
    End Sub

    Protected Sub Btn_ChangePassword_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btn_ChangePassword.Click
        'Response.Redirect("~/ChangePassword.aspx")
        Response.Redirect("~/ChangePWD.aspx")
    End Sub
End Class

