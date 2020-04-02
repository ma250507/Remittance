Imports System.Data.SqlClient
Imports SecurityService.SecurityServiceSoapClient

Partial Class Login
    Inherits System.Web.UI.Page
    
    Dim SecurityObj As New SecurityService.SecurityServiceSoapClient
    Public ProgId As String = ConfigurationManager.ConnectionStrings("ProgId").ToString
    'Public GroupDataEntry As String = ConfigurationManager.ConnectionStrings("GroupDataEntry").ToString
    Dim NumberOfTrials As Integer = 0
    Protected Sub Btn_Login_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btn_Login.Click
        Dim Gen_Comm As SqlCommand
        Dim Con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("NCRMoneyFerConnection").ConnectionString)
        Dim dread As SqlDataReader
        Dim name As String
        Dim password As String
        Dim Valid As String = "Invalid_user"
        Dim Enc As NCRCrypto
        Dim Perm As Permissions
        Dim UserID As String = ""
        Dim GroupID As String = ""
        Dim ATMID As String = ""
        Dim CountryCode As String = ""
        Dim BankCode As String = ""
        Dim Com As SqlCommand
        Dim MaintenanceATM As String
        Dim RemittanceServicePort As String
        Dim RemittanceServiceIP As String
        Dim FirstTime As String
        Dim TellerIPAddress As String
        Dim Branch As String
        Dim AllATMs As String
        Dim ClientIP As String = Request.UserHostAddress()
        Dim Str As String
        Dim Group As Array
        Dim MainFun As New General
        Dim LoginMethod As String
        Dim LockCount As String
        Dim CurrentLockCount As Integer

        If (txt_UsrName.Text = "" Or txt_Password.Text = "") Then
            lbl_Error.Visible = True
            lbl_Error.Text = "Please Enter Valid Data."
            Return
        End If
        Enc = New NCRCrypto()
        Perm = New Permissions
        Gen_Comm = New SqlCommand()
        Com = New SqlCommand()

       

        name = txt_UsrName.Text

        LoginMethod = System.Configuration.ConfigurationManager.AppSettings("LoginMethod")
        LockCount = System.Configuration.ConfigurationManager.AppSettings("UsersLockCount")

        If (LoginMethod = "2") Then
            Try
                password = txt_Password.Text

                Group = SecurityObj.getGroup(name, password, ProgId)
                Session("User") = name
                If (Mid(Group(0), 1, 1) = "#") Then
                    If (Mid(Group(0), 2, 1) = "3") Then
                        If (CInt(Session("NumberOfTrials")) = 5) Then
                            Str = SecurityObj.StopUser(name, ProgId)
                        Else
                            Session("NumberOfTrials") = CInt(Session("NumberOfTrials")) + 1
                        End If
                    End If
                    If (Mid(Group(0), 2, 1) = "6") Then
                        Valid = "Valid_user"
                        Session.Add("Status", Valid)
                        Response.Redirect("ChangePWD.aspx")
                    End If
                    If (Mid(Group(0), 2, 1) = "7") Then
                        Valid = "Valid_user"
                        Session.Add("Status", Valid)
                        Response.Redirect("ChangePWD.aspx")
                    End If
                    lbl_Error.Text = Group(1).ToString()
                    lbl_Error.Visible = True
                Else
                    MainFun.loglog("valid user, return from security module is: [" & Group(0) & "-" & Group(1) & Group(2) & "-" & Group(3) & "-" & Group(4) & "]", True)
                    Valid = "Valid_user"
                    Session.Clear()
                    Session.Add("Status", Valid)


                    Con.Open()
                    Com.Connection = Con
                    Gen_Comm.Connection = Con
                    Gen_Comm.CommandText = "Select Users.Branch,Users.AllATMs from Users where [Userid]='" + name + "' "
                    dread = Gen_Comm.ExecuteReader()

                    While (dread.Read())
                        Try
                            Valid = "Valid_user"
                            Try
                                Branch = dread.GetString(0)
                            Catch ex As Exception
                                Branch = ""
                            End Try

                            Try
                                AllATMs = dread.GetBoolean(1).ToString()
                            Catch ex As Exception
                                AllATMs = ""
                            End Try
                        Catch ex As Exception
                            lbl_Error.Text = "Error :" & ex.ToString()
                            lbl_Error.Visible = True
                        End Try

                    End While

                    dread.Close()
                    Con.Close()




                    Con.Open()
                    Com.Connection = Con
                    Gen_Comm.Connection = Con
                    Gen_Comm.CommandText = "select Reports,Maintenance,Administration,Users,Name,Teller,Registeration " & _
                                           "from Groups " & _
                                           "where Name='" & Group(4).ToString() & "' "
                    dread = Gen_Comm.ExecuteReader()
                    While (dread.Read())
                        Perm.Reports = dread(0).ToString()
                        Perm.Maintenance = dread(1).ToString()
                        Perm.Administration = dread(2).ToString()
                        Perm.Users = dread(3).ToString()
                        Perm.GroupName = dread(4).ToString()
                        Perm.Teller = dread(5).ToString()
                        Perm.Register = dread(6).ToString()
                    End While
                    dread.Close()
                    Con.Close()

                    If (Perm.Maintenance = "True" Or Perm.Teller = "True") Then
                        If (Perm.Teller = "True") Then
                            Try
                                If (TellerIPAddress.Substring(0, TellerIPAddress.LastIndexOf(".")) <> ClientIP.Substring(0, ClientIP.LastIndexOf("."))) Then
                                    lbl_Error.Text = "Sorry you can not login please refer to the security officer."
                                    lbl_Error.Visible = True
                                    Return
                                End If
                            Catch ex As Exception
                                lbl_Error.Text = "Error while validating IP Address, make sure it does exist or contact the security officer."
                                lbl_Error.Visible = True
                                Return
                            End Try
                        End If
                        'Com.CommandText = "select MaintenanceATM,RemittanceServicePort,RemittanceServiceIPAddress from bank where BankCode='" & BankCode & "' and CountryCode='" & CountryCode & "'"
                        Con.Open()
                        Com.CommandText = "select MaintenanceATM,RemittanceServicePort,RemittanceServiceIPAddress from bank where BankCode='NBE' and CountryCode='20'"
                        dread = Com.ExecuteReader()
                        While (dread.Read())
                            MaintenanceATM = dread(0)
                            RemittanceServicePort = dread(1)
                            RemittanceServiceIP = dread(2)
                        End While
                        Session.Add("MainATM", MaintenanceATM)
                        Session.Add("ServicePort", RemittanceServicePort)
                        Session.Add("ServiceIP", RemittanceServiceIP)
                    End If
                    dread.Close()
                    Con.Close()
                    'If (Perm.Users = "True") Then
                    '    Com.CommandText = "select RemittanceServiceIPAddress from bank where BankCode='" & BankCode & "' and CountryCode='" & CountryCode & "'"
                    '    dread = Com.ExecuteReader()
                    '    While (dread.Read())
                    '        RemittanceServiceIP = dread(0)
                    '    End While
                    '    Session.Add("ServiceIP", RemittanceServiceIP)
                    'End If
                    'dread.Close()
                    Session.Add("User", Group(3).ToString())
                    Session.Add("Perm", Perm)
                    Session.Add("ATMID", ATMID)
                    Session.Add("UserName", name)
                    Session.Add("Branch", Group(1).ToString().Substring(0, 3))
                    Session.Add("AllATMs", AllATMs)



                    Response.Redirect("Default.aspx?Status")
                End If
            Catch ex As Exception
                lbl_Error.Text = "UnExcepected Error: " & ex.Message
                lbl_Error.Visible = True
            End Try
        ElseIf (LoginMethod = "1") Then
            Try
                Session.Add("MainATM", MaintenanceATM)
                password = Enc.eT3_Encrypt(txt_Password.Text)

                Con.Open()
                Com.Connection = Con
                Gen_Comm.Connection = Con

                Gen_Comm.CommandText = "Select Locked from Users where [UserName]='" + name + "'"
                dread = Gen_Comm.ExecuteReader()

                If (dread.HasRows) Then
                    While (dread.Read())
                        If dread.GetBoolean(0) Then
                            lbl_Error.Text = "User is locked."
                            lbl_Error.Visible = True
                            Return
                        End If
                    End While

                End If
                dread.Close()
                Gen_Comm.CommandText = "Select count(*),userid,group_id,atm_id,(Select BankCode from branches where branchname=users.branch) as BankCode,(Select CountryCode from branches where branchname=users.branch) as CountryCode,FirstTime,users.TellerIPAddress,Users.Branch,Users.AllATMs from Users where [UserName]='" + name + "' and [Password]='" + password + "'  group by userid , group_id,atm_id,CountryCode,BankCode,FirstTime,users.branch,users.TellerIPAddress,Users.AllATMs"
                dread = Gen_Comm.ExecuteReader()
                If (dread.HasRows <> True) Then
                    lbl_Error.Text = "Invalid Username Or Password"

                    If (Session("lockcount") = LockCount) Then
                        dread.Close()
                        Gen_Comm.CommandText = "update users set locked='True' where username='" & name & "'"
                        If Gen_Comm.ExecuteNonQuery() = 1 Then
                            lbl_Error.Text = "User is locked."
                        End If


                    End If
                    Session("lockcount") = CInt(Session("lockcount")) + 1

                    lbl_Error.Visible = True
                    Return
                End If
                While (dread.Read())

                    If (dread.GetInt32(0) <> 1) Then
                        lbl_Error.Text = "Invalid Username Or Password"
                        If (CurrentLockCount = LockCount) Then
                            Gen_Comm.CommandText = "update users set locked='True' where userid='" & name & "'"
                            Gen_Comm.ExecuteNonQuery()
                        End If
                        Session("lockcount") = CInt(Session("lockcount")) + 1


                    Else
                        Try
                            Valid = "Valid_user"
                            UserID = dread.GetString(1)
                            GroupID = dread.GetString(2)
                            Try
                                ATMID = dread.GetString(3)
                            Catch ex As Exception
                                ATMID = ""
                            End Try
                            Try
                                CountryCode = dread.GetString(5)
                            Catch ex As Exception
                                CountryCode = ""
                            End Try
                            Try
                                BankCode = dread.GetString(4)
                            Catch ex As Exception
                                BankCode = ""
                            End Try
                            Try
                                FirstTime = dread.GetBoolean(6)

                            Catch ex As Exception
                                FirstTime = ""
                            End Try


                            Try
                                TellerIPAddress = dread.GetString(7)
                            Catch ex As Exception
                                TellerIPAddress = ""
                            End Try

                            Try
                                Branch = dread.GetString(8)
                            Catch ex As Exception
                                Branch = ""
                            End Try

                            Try
                                AllATMs = dread.GetBoolean(9).ToString()
                            Catch ex As Exception
                                AllATMs = ""
                            End Try
                        Catch ex As Exception
                            lbl_Error.Text = "Error:" & ex.ToString()
                            lbl_Error.Visible = True
                        End Try
                    End If
                End While

                'If (IsDBNull(dread.GetString(3))) Then
                '    ATMID = ""
                'Else
                '    ATMID = dread.GetString(3)
                'End If
                'If (IsDBNull(dread.GetString(4))) Then
                '    CountryCode = ""
                'Else
                '    CountryCode = dread.GetString(4)
                'End If
                'If (IsDBNull(dread.GetString(5))) Then
                '    BankCode = ""
                'Else
                '    BankCode = dread.GetString(5)
                'End If
                Session.Clear()
                dread.Close()
                If (ATMID <> "" And CountryCode <> "" And BankCode <> "") Then
                    Com.CommandText = "select Count(*) from atm where ATMId='" & ATMID & "' and CountryCode='" & CountryCode & "' and BankCode='" & BankCode & "'"
                    dread = Com.ExecuteReader()
                    If (dread.HasRows <> True) Then
                        lbl_Error.Text = "Your UserName is attached to a terminal id that not exist, Please Contact your administrator"
                        lbl_Error.Visible = True
                        Return
                    End If
                    While (dread.Read())
                        If (dread.GetInt32(0) <> 1) Then
                            lbl_Error.Text = "Your UserName is attached to a terminal id that not exist, Please Contact your administrator"
                            lbl_Error.Visible = True
                            Return
                        End If
                    End While
                End If



                Session.Clear()
                Session.Add("Status", Valid)
                dread.Close()

                Gen_Comm.CommandText = "select Reports,Maintenance,Administration,Users,Name,Teller,Registeration " & _
                                       "from Groups " & _
                                       "where ID='" & GroupID & "' "
                dread = Gen_Comm.ExecuteReader()
                While (dread.Read())
                    Perm.Reports = dread(0).ToString()
                    Perm.Maintenance = dread(1).ToString()
                    Perm.Administration = dread(2).ToString()
                    Perm.Users = dread(3).ToString()
                    Perm.GroupName = dread(4).ToString()
                    Perm.Teller = dread(5).ToString()
                    Perm.Register = dread(6).ToString()
                End While
                dread.Close()

                If (Perm.Maintenance = "True" Or Perm.Teller = "True") Then
                    If (Perm.Teller = "True") Then
                        Try
                            If (TellerIPAddress.Substring(0, TellerIPAddress.LastIndexOf(".")) <> ClientIP.Substring(0, ClientIP.LastIndexOf("."))) Then
                                lbl_Error.Text = "Sorry you can not login please refer to the security officer."
                                lbl_Error.Visible = True
                                Return
                            End If
                        Catch ex As Exception
                            lbl_Error.Text = "Error while validating IP Address, make sure it does exist or contact the security officer."
                            lbl_Error.Visible = True
                            Return
                        End Try
                    End If
                    Com.CommandText = "select MaintenanceATM,RemittanceServicePort,RemittanceServiceIPAddress from bank where BankCode='" & BankCode & "' and CountryCode='" & CountryCode & "'"
                    dread = Com.ExecuteReader()
                    While (dread.Read())
                        MaintenanceATM = dread(0)
                        RemittanceServicePort = dread(1)
                        RemittanceServiceIP = dread(2)
                    End While
                    Session.Add("MainATM", MaintenanceATM)
                    Session.Add("ServicePort", RemittanceServicePort)
                    Session.Add("ServiceIP", RemittanceServiceIP)
                End If
                dread.Close()
                If (Perm.Users = "True") Then
                    Com.CommandText = "select RemittanceServiceIPAddress from bank where BankCode='" & BankCode & "' and CountryCode='" & CountryCode & "'"
                    dread = Com.ExecuteReader()
                    While (dread.Read())
                        RemittanceServiceIP = dread(0)
                    End While
                    Session.Add("ServiceIP", RemittanceServiceIP)
                End If
                dread.Close()






                Session.Add("User", UserID)
                Session.Add("Perm", Perm)
                Session.Add("ATMID", ATMID)
                Session.Add("UserName", name)
                Session.Add("FirstTime", FirstTime)
                Session.Add("Branch", Branch)
                Session.Add("AllATMs", AllATMs)
                If (FirstTime = False) Then
                    Response.Redirect("ChangePWD.aspx")
                End If


                'Try
                '    If (FirstTime = "False") Then
                '        Try
                '            Com.CommandText = "Update users set FirstTime=1 where userid='" & UserID & "' and UserName='" & name & "'"
                '            Com.ExecuteNonQuery()
                '        Catch ex As Exception
                '            lbl_Error.Text = "Unknown Error:-4"
                '            lbl_Error.Visible = True
                '            Return
                '        End Try
                '        Con.Close()
                '        Response.Redirect("ChangePWD.aspx")
                '    End If
                'Catch ex As Exception
                '    lbl_Error.Text = "Unknown Error:-3"
                '    lbl_Error.Visible = True
                'End Try
                Try
                    Gen_Comm = Nothing
                    Con = Nothing
                    dread = Nothing
                    name = Nothing
                    password = Nothing
                    Valid = Nothing
                    Enc = Nothing
                    Perm = Nothing
                    UserID = Nothing
                    GroupID = Nothing
                    ATMID = Nothing
                    CountryCode = Nothing
                    BankCode = Nothing
                    Com = Nothing
                    MaintenanceATM = Nothing
                    RemittanceServicePort = Nothing
                Catch ex As Exception
                    lbl_Error.Text = "Unknown Error:-2"
                    lbl_Error.Visible = True
                End Try
                Response.Redirect("Default.aspx?Status")
            Catch ex As Exception
                'Con.Close()
                lbl_Error.Text = "DB Error." & ex.ToString()
                lbl_Error.Visible = True
            End Try
        End If






    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsPostBack) Then
            lbl_Error.Text = ""
        End If
    End Sub

    
End Class
