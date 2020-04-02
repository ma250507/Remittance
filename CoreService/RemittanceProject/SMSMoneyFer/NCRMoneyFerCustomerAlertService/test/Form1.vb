
Public Class Form1
    Dim cs As New CustomerAlertService.CheckTransactionsClass

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
         ' Dim sr As New CustomerAlertService.CustomerAlertService
        Dim rtrn As Integer
        'cs.StoreOasisATMLastTransactionDateTime("CustomerAlert")
        'cs.GetOasisATMLastTransactionsList("CustomerAlert")
        rtrn = cs.SendSMS("06450631062D06280627", "0101755196")
       


    End Sub
End Class
