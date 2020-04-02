Public Class Form1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim enc As New NCRCrypto.NCRCrypto
        Dim pdata As String
        Dim edata As String
        pdata = TextBox1.Text
       
        edata = enc.eT3_Encrypt(pdata)
        TextBox2.Text = edata
    End Sub
End Class
