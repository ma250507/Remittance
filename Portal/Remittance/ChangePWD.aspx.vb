Imports System.Data
Imports System.Data.SqlClient
Partial Class ChangePWD
    Inherits System.Web.UI.Page

    Private Gen_Comm As SqlCommand
    Private Con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("NCRMoneyFerConnection").ConnectionString)
    Private dread As SqlDataReader
    Private NcrCrypt As NCRCrypto
   

    Protected Sub CancelPushButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CancelPushButton.Click
        Response.Redirect("Default.aspx")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsPostBack <> True) Then
            If (Session("status") = Nothing) Then
                Response.Redirect("Login.aspx")
            End If
            If (Session("FirstTime") = False) Then
                CancelPushButton.Visible = False
            End If
        Else
            If (ImageButton1.Visible = True) Then
                System.Threading.Thread.Sleep(3000)
                Response.Redirect("Default.aspx")
            End If
            FailureText.Text = ""
            ImageButton1.Visible = False
        End If
    End Sub

    Protected Sub ChPWD_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChPWD.Click
        Dim Row As Integer = 0
        Dim UserID As String
        Dim UserName As String
        Try
            NcrCrypt = New NCRCrypto()
            Con.Open()
            Gen_Comm = New SqlCommand("select count(*),userid,UserName from users where userid='" & Session("User") & "' and password='" & NcrCrypt.eT3_Encrypt(CurrentPassword.Text) & "' group by userid , UserName ", Con)
            dread = Gen_Comm.ExecuteReader()
            If (Not dread.HasRows) Then
                FailureText.Text = "Your old password does not match."
            End If
            While (dread.Read())
                Row = dread(0)
                If (Row <> 1) Then
                    FailureText.Text = "Your old password does not match."
                    dread.Close()
                    Con.Close()
                    Exit While
                Else
                    UserID = dread.GetString(1)
                    UserName = dread.GetString(2)
                    dread.Close()
                    Gen_Comm.CommandText = "Update users set [password]='" & NcrCrypt.eT3_Encrypt(NewPassword.Text) & "' , FirstTime=1 where userid='" & UserID & "' and username='" & UserName & "' "
                    Row = Gen_Comm.ExecuteNonQuery()
                    If (Row <> 1) Then
                        FailureText.Text = "Could not change your Password."
                        dread.Close()
                        Con.Close()
                        Exit While
                    Else
                        'FailureText.Text = "Your Password is changed succesfully."
                        dread.Close()
                        Con.Close()
                        Session("FirstTime") = True
                        ImageButton1.Visible = True
                        ChPWD.visible = False
                        CancelPushButton.visible = False
                        Exit While
                    End If
                End If
            End While
        Catch ex As Exception
            FailureText.Text = "Error while changing your password: -1"
        End Try
    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Response.Redirect("Default.aspx")
    End Sub


  
End Class
