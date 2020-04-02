Imports System.Data
Imports System.Data.SqlClient
Partial Class Users_Main
    Inherits System.Web.UI.Page

    Protected Sub Btn_Next_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btn_Next.Click

        If (RD_Add.Checked) Then
            If (DirectCast(Session("Perm"), Permissions).Administration = "True") Then
                SqlDataSource1.SelectCommand = "select Name,ID from groups"
            Else
                SqlDataSource1.SelectCommand = "select Name,ID from groups where administration=0"
            End If
            Lbl_GroupTitle.Visible = True
            drpd_Groups.Visible = True
            Btn_GroupNext.Visible = True
        ElseIf (RD_View.Checked) Then
            Response.Redirect("Users-View.aspx")
        ElseIf (RD_Group.Checked) Then
            Response.Redirect("Users-Groups.aspx")
        ElseIf (RD_Unlock.Checked) Then
            Response.Redirect("Users-Unlock.aspx")
        End If
    End Sub

    Protected Sub Btn_GroupNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btn_GroupNext.Click
        Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("NCRMoneyFerConnection").ConnectionString)
        Dim Com As New SqlCommand("", con)
        Dim Reader As SqlDataReader
        Dim IsTeller As String
        Try
            con.Open()
            Com.CommandText = "Select teller from groups where ID='" & drpd_Groups.SelectedValue & "'"
            Reader = Com.ExecuteReader()
            While (Reader.Read())
                IsTeller = Reader.GetBoolean(0)
            End While
            Reader.Close()
            con.Close()

            If (IsTeller = "True") Then
                Session.Add("GroupId", drpd_Groups.SelectedValue)
                Response.Redirect("Users-AddTeller.aspx")
            ElseIf (IsTeller = "False") Then
                Session.Add("GroupId", drpd_Groups.SelectedValue)
                Response.Redirect("Users-AddNormal.aspx")
            End If
        Catch ex As Exception
            Lbl_Status.Text = "DB Error."
            Lbl_Status.Visible = True
        End Try
       


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
        End If
        
    End Sub
End Class
