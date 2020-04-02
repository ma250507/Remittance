Imports Microsoft.VisualBasic
Imports System.Collections


Public Class ErrorCodes
    Public Errors As Hashtable

    Public Sub New()
        Errors = New Hashtable()
        Errors.Add("00001", "Message Parsing Error")
        Errors.Add("00002", "Unknown Request Type")
        Errors.Add("00003", "Can Not Get Host TrxCode")
        Errors.Add("00004", "DataBase Error")
        Errors.Add("00005", "Can Not Get PINs")
        Errors.Add("00006", "Sorry,the inserted transaction code is invalid")
        Errors.Add("00007", "Pador Null Amount Value")
        Errors.Add("00008", "No Expired Transactions Or DB Error")
        Errors.Add("00016", "ATM IP Address Not Matched Error")
        Errors.Add("00017", "ATM IP Address Is Not Teller")
        Errors.Add("00018", "Cassettes Values Errors")
        Errors.Add("00019", "Sorry, the inserted amount doesn't comply with the service allowable amounts")
        Errors.Add("00020", "Today Total Deposit Amount Exceeds Max Allowed Value")

        Errors.Add("00026", "Sorry,Please Validate the supplied information then try again")
        Errors.Add("00027", "Sorry,You cannot collect this money transfer,Please contact the customer service center 19200")
        Errors.Add("00028", "Sorry,We Cannot Process this Money Transfer")
        Errors.Add("00029", "Sorry,We Cannot Process this Money Transfer")

        Errors.Add("00101", "Sorry,You cannot collect this money transfer,Please contact the customer service center 19200")
        Errors.Add("00102", "No Confirmed Deposist Transaction")
        Errors.Add("00103", "Not Expired Deposit Transaction")
        Errors.Add("00104", "Not Canceled Or Null Withdrawal Transaction")
        Errors.Add("00105", "Not Canceled Or Expired Withdrawal Transaction")
        Errors.Add("00106", "Expired Transaction")
        Errors.Add("00107", "Reactivation Limit Reached")
        Errors.Add("00117", "Error Creating ReplyQ")
        Errors.Add("00118", "Error sending RequestQ")
        Errors.Add("00119", "Error Receive TimeOut")
        Errors.Add("00120", "Error Receive Error")
        Errors.Add("00121", "Error Header Not Match Error")
        Errors.Add("00122", "Error Parsing Reply Error")
        Errors.Add("00123", "Error Host Reply TimeOut")
        Errors.Add("00199", "Error Host Receiving Reply")
        Errors.Add("00510", "Error Host Suspend Mode")
        Errors.Add("00511", "Error Host InComplete Trx")
        Errors.Add("00500", "Host NoError")



    End Sub
End Class
