Imports System.Data.SqlClient
Imports System.Data
Imports System.Math

Partial Class Registeration
    Inherits System.Web.UI.Page

    Private UserPerm As Permissions
    Public Obj As New General
    Public Connection As String
    Public DT As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Status As Boolean

        If (Session("Status") = "InValid_user" Or Session("Status") = Nothing) Then
            Response.Redirect("Login.aspx")
        End If

        If (Page.IsPostBack <> True) Then
            Try
                UserPerm = New Permissions()
                UserPerm = Session("Perm")
                If (UserPerm.Register <> "True") Then
                    Response.Redirect("Login.aspx")
                End If

            Catch ex As Exception
                Response.Redirect("Login.aspx")
            End Try
            Connection = ConfigurationManager.ConnectionStrings("NCRMoneyFerConnection").ConnectionString
            Session("NCRMoneyFerCon") = Connection
            Status = GetCustomers(DT, "")
            Session("CustomerDT") = DT
            If (Status) Then
                FillGrid(DT)
            Else
                Lbl_Status.Visible = True
                Lbl_Status.Text = "Cannot Get Customers"
            End If
        Else
            REGConBTN.Visible = False
            Lbl_Status.Text = ""
        End If
    End Sub

    Protected Function FillGrid(ByVal DT As DataTable) As Boolean
        If DT.Rows.Count > 0 Then

            GridView2.DataSource = DT
            GridView2.DataBind()
        Else
            Session("Flag") = True
            DT.Rows.Add(DT.NewRow)
            GridView2.DataSource = DT
            GridView2.DataBind()
            Dim columnsCount As Integer = GridView2.Columns.Count
            GridView2.Rows(0).Cells.Clear()
            GridView2.Rows(0).Cells.Add(New TableCell())
            GridView2.Rows(0).Cells(0).ColumnSpan = columnsCount
            GridView2.Rows(0).Cells(0).HorizontalAlign = HorizontalAlign.Center
            GridView2.Rows(0).Cells(0).ForeColor = System.Drawing.Color.Red
            GridView2.Rows(0).Cells(0).Font.Bold = True
            GridView2.Rows(0).Cells(0).Text = "Insert New Record"
        End If
    End Function

    Protected Function GetCustomers(ByRef DT As DataTable, ByVal MobileNumber As String) As Boolean

        Dim ConnectionString As String
        Dim Qstr As String
        Dim Status As Boolean
        Try
            

            DT = New DataTable
            ConnectionString = Session("NCRMoneyFerCon")
            If (MobileNumber = "") Then
                Qstr = "SELECT * FROM RegisteredCustomer where MobileNumber like '%' "
            Else
                Qstr = "SELECT * FROM RegisteredCustomer where MobileNumber= '" & MobileNumber & "'"
            End If

            Status = Obj.ConnectToDatabase(Qstr, ConnectionString, DT)

            If (DT.Rows.Count = 0) Then
                Lbl_Status.Text = "No Rows found."
                Return False
            End If

            If Status = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Obj.loglog("Error In getting RegisteredCustomers ex=[" & ex.ToString() & "]", True)
            Return False
        End Try
    End Function
    Public Function CheckMobileNumber(ByVal MobileNumber As String, ByVal ID As String) As Boolean

        Dim Qstr As String
        Dim Status As Boolean
        Dim CheckDigit As String
        Dim MainID As String
        Dim Total As Long = 0
        Dim Temp As String
        Dim Multiplier As Integer = 2

        Try
            DT = New DataTable

            Qstr = "SELECT count(*) FROM RegisteredCustomer where MobileNumber= '" & MobileNumber & "' or [Id] = '" & ID & "'"
            Status = Obj.ConnectToDatabase(Qstr, Session("NCRMoneyFerCon"), DT)
            Obj.loglog("Will execute: [" & Qstr & "]", True)
            If Status = True Then
                If (CInt(DT.Rows(0)(0)) <> 0) Then
                    Lbl_Status.Visible = True
                    Lbl_Status.Text = "This customer already exists."
                    Return False
                Else
                    ''''''''''''''''''Chcking the ID with the provided schema'''''''''''''''
                    'ID = "28206172100051"
                    CheckDigit = ID.Substring(ID.Length - 1)
                    MainID = ID.Substring(0, ID.Length - 1)
                    For i As Integer = 1 To MainID.Length
                        If (Multiplier = 8) Then
                            Multiplier = 2
                        End If
                        Temp = MainID.Substring(MainID.Length - i, 1)
                        Total = Total + CInt(Temp) * Multiplier
                        Multiplier = Multiplier + 1
                    Next
                    'MsgBox(Abs(11 - (Total Mod 11)) Mod 10)
                    If (CheckDigit = Abs(11 - (Total Mod 11)) Mod 10) Then
                        Return True
                    Else
                        Lbl_Status.Visible = True
                        Lbl_Status.Text = "This ID is not valid."
                        Return False
                    End If
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Obj.loglog("Error In getting registered count ex=[" & ex.ToString() & "]", True)
            Return False
        End Try
    End Function
    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim MobileNumber As String = (DirectCast(GridView2.FooterRow.FindControl("TXT_ADD_Mobile"), TextBox)).Text
        Dim Name As String = (DirectCast(GridView2.FooterRow.FindControl("TXT_ADD_Name"), TextBox)).Text
        'Dim RegisterationDate As String = (DirectCast(GridView2.FooterRow.FindControl("DPC_Date1"), HtmlInputText)).Value()
        Dim ID As String = (DirectCast(GridView2.FooterRow.FindControl("TXT_ADD_ID"), TextBox)).Text
        Dim Address As String = (DirectCast(GridView2.FooterRow.FindControl("TXT_ADD_ADDRESS"), TextBox)).Text
        Dim Staff As Boolean = (DirectCast(GridView2.FooterRow.FindControl("CHK_AStaff"), CheckBox)).Checked
        Dim BankCustomer As Boolean = (DirectCast(GridView2.FooterRow.FindControl("CHK_BankCustomer_A"), CheckBox)).Checked
        Dim Ret As Boolean


        Try
            If (CheckMobileNumber(MobileNumber, ID)) Then
                Ret = Obj.InsertDB("INSERT INTO [RegisteredCustomer] ([MobileNumber], [Name], [RegisteringDate], [ID], [Address], [Staff], [BankCustomer]) VALUES ('" & MobileNumber & "', '" & Name & "',GetDate(), '" & ID & "', '" & Address & "', '" & Staff & "', '" & BankCustomer & "')", Session("NCRMoneyFerCon"))
                If (Ret) Then
                    'Log(Session("user").ToString(), "Register Add", MobileNumber, Session("UserName"), Session("Branch"))
                    'REGConBTN.Visible = True
                    Ret = Obj.InsertDB("insert into useractions values('" & Session("user").ToString() & "','" & "Register Add" & "',getdate(),'" & MobileNumber & "','" & Session("UserName") & "' , '" & Session("Branch") & "')", Session("NCRMoneyFerCon"))
                    If (Ret) Then
                        REGConBTN.Visible = True
                        GetCustomers(DT, "")
                        Session("CustomerDT") = DT
                        FillGrid(DT)
                    Else
                        Lbl_Status.Visible = True
                        Lbl_Status.Text = "Error while Updating useractions"
                        Obj.loglog("Error while Updating useractions", True)
                    End If
                Else
                    Lbl_Status.Visible = True
                    Lbl_Status.Text = "Error while adding customer"
                End If
            Else
                
            End If
            
        Catch ex As Exception
            Lbl_Status.Visible = True
            Lbl_Status.Text = "Error while adding Customer."
            Obj.loglog("Error while adding Customer: " & ex.ToString(), True)
        End Try
    End Sub

    Protected Sub GridView2_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles GridView2.RowUpdating
        Dim MobileNumber As String = (DirectCast(GridView2.Rows(e.RowIndex).FindControl("TXT_MobileNumber"), TextBox)).Text
        'Dim Name As String = (DirectCast(GridView2.Rows(e.RowIndex).FindControl("TXT_EDIT_Name"), Label)).Text
        'Dim RegisterationDate As String = (DirectCast(GridView2.FooterRow.FindControl("DPC_Date1"), HtmlInputText)).Value()
        Dim ID As String = (DirectCast(GridView2.Rows(e.RowIndex).FindControl("TXT_EDIT_ID"), Label)).Text
        Dim Address As String = (DirectCast(GridView2.Rows(e.RowIndex).FindControl("TXT_EDIT_ADDRESS"), TextBox)).Text
        Dim Staff As Boolean = (DirectCast(GridView2.Rows(e.RowIndex).FindControl("CHK_EStaff"), CheckBox)).Checked
        Dim BankCustomer As Boolean = (DirectCast(GridView2.Rows(e.RowIndex).FindControl("CHK_BankCustomer_E"), CheckBox)).Checked
        Dim Rows As Integer
        Dim ret As Boolean

        Try
            ret = Obj.UpdateDBRegistered("UPDATE [RegisteredCustomer] SET  [MobileNumber] = '" & MobileNumber & "', [Address] = '" & Address & "', [Staff] = '" & Staff & "' , [BankCustomer] = '" & BankCustomer & "'  WHERE [ID] = '" & ID & "'", Session("NCRMoneyFerCon"))
            If (ret) Then

                ret = GetCustomers(DT, "")
                If ret = True Then
                    ret = Obj.InsertDB("insert into useractions values('" & Session("user").ToString() & "','" & "Register Edit" & "',getdate(),'" & MobileNumber & "','" & Session("UserName") & "' , '" & Session("Branch") & "')", Session("NCRMoneyFerCon"))
                    If (ret) Then
                        REGConBTN.Visible = True
                    Else
                        Lbl_Status.Visible = True
                        Lbl_Status.Text = "Error while Updating useractions"
                        Obj.loglog("Error while Updating useractions", True)
                    End If
                    GridView2.EditIndex = -1
                    FillGrid(DT)
                    Session("CustomerDT") = DT
                Else
                    Obj.loglog("Error In Getting Customers status = [" & ret & "]", True)
                End If
            Else
                Lbl_Status.Visible = True
                Lbl_Status.Text = "Error while updating Customer"
            End If
        Catch ex As Exception
            REGConBTN.Visible = False
            Lbl_Status.Visible = True
            'Lbl_Status.Text = "Error while updating Customer: " & ex.Message & ""
            Obj.loglog("Error While Updating Customer" & ex.ToString(), True)
        End Try
    End Sub


    'Public Sub Log(ByVal UserID As String, ByVal Action As String, ByVal TransactionCode As String, ByVal Name As String, ByVal Branch As String)
    '    Dim ret As Integer
    '    Dim Com As SqlCommand
    '    Dim Con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("NCRMoneyFerConnection").ConnectionString)

    '    Com = New SqlCommand()
    '    Com.CommandText = "insert into useractions values('" & UserID & "','" & Action & "',getdate(),'" & TransactionCode & "','" & Name & "' , '" & Branch & "')"
    '    Com.Connection = Con
    '    Try
    '        Con.Open()
    '        ret = Com.ExecuteNonQuery()

    '        If (ret <> 1) Then
    '            Lbl_Status.Text = "Error while logging history."
    '            Lbl_Status.Visible = True
    '            Return
    '        End If
    '        Con.Close()
    '    Catch ex As Exception
    '        Lbl_Status.Text = "An error occurred: " & ex.ToString() & ""
    '        Lbl_Status.Visible = True
    '    End Try

    'End Sub

    Protected Sub GridView2_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView2.RowDeleting
        Dim ID As String = (DirectCast(GridView2.Rows(e.RowIndex).FindControl("Label4"), Label)).Text
        Dim ret As Boolean

        Try
            ret = Obj.DeletDB("DELETE FROM [RegisteredCustomer] WHERE [ID] = '" & ID & "'", Session("NCRMoneyFerCon"))
            If (ret) Then
                ret = Obj.InsertDB("insert into useractions values('" & Session("user").ToString() & "','" & "Register Delete" & "',getdate(),'" & ID & "','" & Session("UserName") & "' , '" & Session("Branch") & "')", Session("NCRMoneyFerCon"))
                If (ret) Then
                    REGConBTN.Visible = True
                    GetCustomers(DT, "")
                    Session("CustomerDT") = DT
                    FillGrid(DT)
                Else
                    Lbl_Status.Visible = True
                    Lbl_Status.Text = "Error while Updating useractions"
                    Obj.loglog("Error while Updating useractions", True)
                End If
            Else
                Lbl_Status.Visible = True
                Lbl_Status.Text = "Error while deleting Customer"
            End If
        Catch ex As Exception
            Lbl_Status.Visible = True
            Lbl_Status.Text = "Error while deleting Customer"
            Obj.loglog("Error While deleting Customer" & ex.ToString(), True)
        End Try

    End Sub

    Protected Sub GridView2_RowEditing(sender As Object, e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridView2.RowEditing
        GridView2.EditIndex = e.NewEditIndex
        FillGrid(Session("CustomerDT"))
    End Sub

    Protected Sub GridView2_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView2.PageIndexChanging
        GridView2.PageIndex = e.NewPageIndex
        FillGrid(Session("CustomerDT"))
    End Sub

    Protected Sub GridView2_RowCancelingEdit(sender As Object, e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles GridView2.RowCancelingEdit
        GridView2.EditIndex = -1
        FillGrid(Session("CustomerDT"))
    End Sub

    Protected Sub Btn_Reg_Search_Click(sender As Object, e As System.EventArgs) Handles Btn_Reg_Search.Click
        Dim Status As Boolean
        If (TXT_Reg_MobileNum.Text.Trim() = "") Then
            Status = GetCustomers(DT, "")
        Else
            'If (Val(TXT_Reg_MobileNum.Text.Trim()) = -1) Then
            '    Lbl_Status.Text = "Please enter a valid mobile number."
            '    Lbl_Status.Visible = True
            'End If
            Status = GetCustomers(DT, TXT_Reg_MobileNum.Text)
        End If

        Session("CustomerDT") = DT
        If (Status) Then
            FillGrid(DT)
        Else
            Lbl_Status.Visible = True
            Lbl_Status.Text = "No Results for this mobile number."
        End If
    End Sub
End Class
