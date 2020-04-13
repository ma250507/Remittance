Imports System.Data

Partial Class BulkWithdrowalTransactionsView
    Inherits System.Web.UI.Page
    Public RepObj As StateObj
    Dim MainFunctions As New General
    Dim RepDoc As New CrystalDecisions.CrystalReports.Engine.ReportDocument
    Dim MyConn As String = ConfigurationManager.ConnectionStrings("NCRMoneyFerConnection").ConnectionString
    Dim MainDT As DataTable
    Dim DetailsDT As DataTable
    Dim DS As DataSet
    Dim ReportTitleStr As String
    Dim ConnectFlag As Boolean
    Dim SelectFlag As Boolean
    Dim DetailsFalg As Boolean
    Private UserPerm As Permissions
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'Try
            '    UserPerm = New Permissions()
            '    UserPerm = Session("Perm")
            '    If (UserPerm.Reports <> "True") Then
            '        Response.Redirect("Login.aspx")
            '    End If
            'Catch ex As Exception
            '    Response.Redirect("Login.aspx")
            'End Try
            Try

                RepObj = Session("Obj")


                SelectFlag = MainFunctions.ConnectToDatabase(RepObj.QueryString, MyConn, MainDT)
                If (SelectFlag) Then
                    MainFunctions.loglog("Executing : " & RepObj.QueryString, True)
                    RepDoc.Load(AppDomain.CurrentDomain.BaseDirectory & "\" & RepObj.ReportName)
                    'RepDoc.Load(AppDomain.CurrentDomain.BaseDirectory & "\" & "Reports\Sub.rpt")
                    RepDoc.DataDefinition.FormulaFields.Item("Title").Text = "'" & RepObj.Title & "'"
                    RepDoc.DataDefinition.FormulaFields.Item("From").Text = "'" & RepObj.DFrom & "'"
                    RepDoc.DataDefinition.FormulaFields.Item("To").Text = "'" & RepObj.DTo & "'"
                    'ReportTitleStr = "Informed Report"
                    'RepDoc.DataDefinition.FormulaFields.Item("RPT_Param").Text = "'" & RepObj.Param & "'"
                    If (RepObj.Hasdetails) Then
                        DetailsFalg = MainFunctions.ConnectToDatabase(RepObj.DetailsQuery, MyConn, DetailsDT)
                        If (DetailsFalg) Then

                            DS = New DataSet
                            MainDT.TableName = "MyTable"
                            DetailsDT.TableName = "Details"
                            'DetailsDT.WriteXmlSchema("c:\Sub.xsd")
                            DS.Tables.Add(MainDT)
                            DS.Tables.Add(DetailsDT)
                            'DS.WriteXmlSchema("C:\Detailed.xsd")
                            'RepDoc.OpenSubreport(AppDomain.CurrentDomain.BaseDirectory & "\" & "Reports\Sub.rpt")
                            RepDoc.SetDataSource(DS)

                        Else
                            Lbl_Status.Text = "Error while generating the report, Please review the log for more details."
                            Exit Sub
                        End If

                    Else
                        MainDT.TableName = "MyTable"
                        MainDT.WriteXmlSchema("C:\BMRemittanceReport\BulkWithdrowalTransactions.xsd")
                        RepDoc.SetDataSource(MainDT)
                    End If
                    'DS.WriteXmlSchema("C:\Expired.xsd")
                    Session("MyReport") = RepDoc
                    CrystalReportViewer1.ReportSource = RepDoc
                Else
                    Lbl_Status.Text = "Error while generating the report, Please review the log for more details."
                    Exit Sub
                End If

            Catch ex As Exception
                MainFunctions.loglog("Generating Report:" & ex.ToString(), True)
                Lbl_Status.Text = "Unknown error:Please review the log."
            End Try
        Else
            CrystalReportViewer1.ReportSource = Session("MyReport")
        End If
    End Sub

    Protected Sub BTN_Back_Click(sender As Object, e As System.EventArgs) Handles BTN_Back.Click
        Response.Redirect("BulkTransactionReports.aspx")
    End Sub
End Class
