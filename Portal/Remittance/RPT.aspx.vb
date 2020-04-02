Imports System.Data
Imports System.Data.SqlClient
Imports CrystalDecisions.Shared
Partial Class RPT
    Inherits System.Web.UI.Page


    Private UserPerm As Permissions
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim Rpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument()
        Dim SubRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument()
        Dim app_Base As String = AppDomain.CurrentDomain.BaseDirectory
        Dim i As Integer = 0
        Dim s As StateObj
        Dim myParam As New ParameterField
        Dim myDiscreteValue As New ParameterDiscreteValue
        Dim myviewer As New CrystalDecisions.Web.CrystalReportViewer
        Dim ParamFields As ParameterFields = Me.CrystalReportViewer2.ParameterFieldInfo
        Dim PField As ParameterField
        Dim PFieldValue As ParameterDiscreteValue
        Dim myConnectionInfo As New ConnectionInfo
        Dim myConnectionString As String = ConfigurationManager.ConnectionStrings("NCRMoneyFerConnection").ConnectionString

        If (IsPostBack <> True) Then
            Try
                UserPerm = New Permissions()
                UserPerm = Session("Perm")
                If (UserPerm.Reports <> "True") Then
                    Response.Redirect("Login.aspx")
                End If
            Catch ex As Exception
                Response.Redirect("Login.aspx")
            End Try
            Try
                s = CType(Session("Obj"), StateObj)
                ParamFields.Clear()



                PField = New ParameterField
                PFieldValue = New ParameterDiscreteValue
                PField.Name = "From"
                PFieldValue.Value = s.DFrom
                PField.CurrentValues.Add(PFieldValue)
                ParamFields.Add(PField)

                PField = New ParameterField
                PFieldValue = New ParameterDiscreteValue
                PField.Name = "Title"
                PFieldValue.Value = s.Title
                PField.CurrentValues.Add(PFieldValue)
                ParamFields.Add(PField)

                PField = New ParameterField
                PFieldValue = New ParameterDiscreteValue
                PField.Name = "To"
                PFieldValue.Value = s.DTo
                PField.CurrentValues.Add(PFieldValue)
                ParamFields.Add(PField)

                PField = New ParameterField
                PFieldValue = New ParameterDiscreteValue
                PField.Name = "ATM"
                PFieldValue.Value = s.ATM
                PField.CurrentValues.Add(PFieldValue)
                ParamFields.Add(PField)

                myConnectionInfo.ServerName = getPartConnectionString("Data Source", myConnectionString)
                myConnectionInfo.DatabaseName = getPartConnectionString("Initial Catalog", myConnectionString)
                myConnectionInfo.UserID = getPartConnectionString("User ID", myConnectionString)
                myConnectionInfo.Password = getPartConnectionString("Password ", myConnectionString)
            Catch ex As Exception
                Lbl_Status.Text = "Unknown Error: -2"
                Lbl_Status.Visible = True
                Return
                ' MsgBox("Error Updating Page", MsgBoxStyle.OkOnly, "NCR Money Fer Portal")
            End Try
            Try
                i = CInt(Request.QueryString(0))
            Catch ex As Exception
                Lbl_Status.Text = "Unknown Error: -3"
                Lbl_Status.Visible = True
                Return
            End Try

            If (i = 1) Then 'Confirmed Withdrawal Transactions
                Rpt.FileName = app_Base + "Reports\CWithdrawals.rpt"
            ElseIf (i = 2) Then
                Rpt.FileName = app_Base + "Reports\CRedemptions.rpt"
            ElseIf (i = 5) Then
                Rpt.FileName = app_Base + "Reports\Blocked.rpt"
            ElseIf (i = 4) Then
                Rpt.FileName = app_Base + "Reports\Expired.rpt"
            ElseIf (i = 6) Then 'confirmed deposits
                Rpt.FileName = app_Base + "Reports\CDeposit.rpt"
            ElseIf (i = 9) Then
                Rpt.FileName = app_Base + "Reports\Custom.rpt"
                Rpt.SetDataSource(CType(Session("9"), DataTable))
            ElseIf (i = 7) Then
                Rpt.FileName = app_Base + "Reports\AuditReport.rpt"
            ElseIf (i = 10) Then
                Rpt.FileName = app_Base + "Reports\w30000.rpt"
            ElseIf (i = 11) Then
                Rpt.FileName = app_Base + "Reports\Dep30000.rpt"
            ElseIf (i = 12) Then
                Rpt.FileName = app_Base + "Reports\BlockedDepositors.rpt"
            ElseIf (i = 13) Then
                Rpt.FileName = app_Base + "Reports\BlockedBeneficiaries.rpt"
            ElseIf (i = 14) Then
                Rpt.FileName = app_Base + "Reports\BlockingHistory.rpt"
            ElseIf (i = 15) Then
                Rpt.FileName = app_Base + "Reports\InvalidKeyTrials.rpt"
            ElseIf (i = 18) Then
                Rpt.FileName = app_Base + "Reports\Users.rpt"
            ElseIf (i = 19) Then
                Rpt.FileName = app_Base + "Reports\Terminals.rpt"
            Else
                Response.Redirect("Default.aspx")
            End If

            'SubRpt.FileName = app_Base + "Reports\Sub.rpt"
            'SubRpt.DataSourceConnections(0).SetConnection(myConnectionInfo.ServerName, myConnectionInfo.DatabaseName, myConnectionInfo.UserID, myConnectionInfo.Password)
            'SubRpt.SetDatabaseLogon(myConnectionInfo.UserID, myConnectionInfo.Password, myConnectionInfo.ServerName, myConnectionInfo.DatabaseName)
            'SubRpt.DataSourceConnections(0).IntegratedSecurity = False


            Rpt.DataSourceConnections(0).SetConnection(myConnectionInfo.ServerName, myConnectionInfo.DatabaseName, myConnectionInfo.UserID, myConnectionInfo.Password)
            'Rpt.SetDatabaseLogon(myConnectionInfo.UserID, myConnectionInfo.Password, myConnectionInfo.ServerName, myConnectionInfo.DatabaseName)
            Rpt.DataSourceConnections(0).IntegratedSecurity = True
            Session.Add("RPT", Rpt)

            CrystalReportViewer2.ParameterFieldInfo = ParamFields
            CrystalReportViewer2.ReportSource = Rpt
            CrystalReportViewer2.DataBind()
        Else
            CrystalReportViewer2.ParameterFieldInfo = ParamFields
            CrystalReportViewer2.ReportSource = Session("RPT")
            CrystalReportViewer2.DataBind()
        End If


    End Sub

    Protected Sub CrystalReportViewer1_Init(ByVal sender As Object, ByVal e As System.EventArgs)
        CrystalReportViewer2.ReportSource = Session("RPT")
        CrystalReportViewer2.DataBind()
    End Sub

    Protected Sub CrystalReportViewer1_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        CrystalReportViewer2.ReportSource = Session("RPT")
        CrystalReportViewer2.DataBind()
    End Sub

    Protected Sub CrystalReportViewer1_Navigate(ByVal source As Object, ByVal e As CrystalDecisions.Web.NavigateEventArgs)
        CrystalReportViewer2.ReportSource = Session("RPT")
        CrystalReportViewer2.DataBind()
    End Sub

    Protected Sub CrystalReportViewer1_Search(ByVal source As Object, ByVal e As CrystalDecisions.Web.SearchEventArgs)
        CrystalReportViewer2.ReportSource = Session("RPT")
        CrystalReportViewer2.DataBind()
    End Sub
    Private Function getPartConnectionString(ByVal part As String, ByVal _connectionString As String) As String

        Dim inicio As Integer
        Dim partTemp As String
        Dim partResult As String

        inicio = InStr(_connectionString, part) + Len(part) + 1

        For contPartConn As Integer = inicio To Len(_connectionString)
            partTemp = Mid(_connectionString, contPartConn, 1)
            If partTemp = ";" Then Exit For
            partResult += partTemp
        Next

        Return partResult
    End Function




    Protected Sub BTN_Back_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BTN_Back.Click
        Response.Redirect("Reports.aspx")
    End Sub
End Class
