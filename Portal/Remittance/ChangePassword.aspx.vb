Imports System.Data.SqlClient
Imports System.Data.Sql
Partial Class ChangePassword
    Inherits System.Web.UI.Page


    Private Gen_Comm As SqlCommand
    Private Con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("NCRMoneyFerConnection").ConnectionString)
    Private dread As SqlDataReader
    Private NcrCrypt As NCRCrypto
    Protected Sub ChangePasswordPushButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Row As Integer = 0
        Dim UserID As String
        Dim UserName As String
        Try
            NcrCrypt = New NCRCrypto()
            Con.Open()
            Gen_Comm = New SqlCommand("select count(*),userid,UserName from users where username='" & Session("User") & "' and password='" & NcrCrypt.eT3_Encrypt(ChangePassword1.CurrentPassword) & "' group by userid , UserName ", Con)
            dread = Gen_Comm.ExecuteReader()
            While (dread.Read())
                Row = dread(0)
                If (Row <> 1) Then
                    ChangePassword1.ChangePasswordFailureText = "Your old password does not match."
                    dread.Close()
                    Con.Close()
                    Exit While
                Else
                    UserID = dread.GetString(1)
                    UserName = dread.GetString(2)
                    dread.Close()
                    Gen_Comm.CommandText = "Update users set [password]='" & NcrCrypt.eT3_Encrypt(ChangePassword1.NewPassword) & "' where userid='" & UserID & "' and username='" & UserName & "' "
                    Row = Gen_Comm.ExecuteNonQuery()
                    If (Row <> 1) Then
                        ChangePassword1.ChangePasswordFailureText = "Could not change your Password."
                        Exit While
                    Else
                        ChangePassword1.ChangePasswordFailureText = "Your Password is changed succesfully."
                        Exit While
                    End If
                End If
            End While
            
        Catch ex As Exception
            ChangePassword1.ChangePasswordFailureText = "Error while changing your password: -1"
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsPostBack) Then
            ChangePassword1.ChangePasswordFailureText = ""
        End If
    End Sub

    Protected Sub CancelPushButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Response.Redirect("Default.aspx")
    End Sub
End Class
