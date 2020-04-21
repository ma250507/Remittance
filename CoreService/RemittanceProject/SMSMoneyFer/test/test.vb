Imports System
Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports GsmComm.GsmCommunication
Imports GsmComm.PduConverter

Public Class test

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Dim x As New NCRMoneyFer.NCRMoneyFerService
        'x.MyStart()

        Dim mc As New NCRMoneyFer.MessageClass
        Dim rtrn As Integer
        Dim ssttrr As String
        rtrn = NCRMoneyFer.CONFIGClass.readConfig()
        ssttrr = "  199  TUB        20019999907/03/201117:40:4700000001690100607199                    0101755196                    20             EGP                                                                                     E0              999999                                                                                                                                 "
        rtrn = mc.DoRequest(ssttrr)




    End Sub






    ' ''Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
    ' ''    Dim ss As New SMSSendingClass
    ' ''    Dim mid As String = ""
    ' ''    Dim ret As Integer
    ' ''    Dim sts As String = ""

    ' ''    ret = ss.SendSMS("unitedbank", "Push", "First Class Test", "0194636017", "20101755196", "00", 30, mid, sts)

    ' ''    MsgBox("ret=" & ret & vbNewLine & "Status=" & ss.Status)

    ' ''End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim cas As New NCRMoneyFer.NCRMoneyFerService

        cas.MyStart()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim ms As New NCRMoneyFer.MessageClass

        Dim rtrn As Integer
        Dim pAmount As Integer
        Dim pdispensedAmount As Integer
        Dim pcommAmount As Integer
        Dim dispstring As String = ""
        pAmount = TextBox1.Text

        rtrn = ms.GetDispensedNotes_10(pAmount, pdispensedAmount, pcommAmount, dispstring)
        MsgBox("rtrn=" & rtrn & " pdisp=" & pdispensedAmount & " comm=" & pcommAmount & " str=" & dispstring)


    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim tc As New CustomerAlertService.TrxClass
        Dim xx As String
        Dim msgstrb As String
        tc.DepositorPIN = "1234"
        tc.BeneficiaryPIN = "5678"
        tc.DepositorMobile = ("01234567890")
        tc.BeneficiaryMobile = "09876543210"
        tc.TransactionCode = "7777777777777"
        tc.RedemptionPIN = "6543"
        tc.SMSLanguage = "A"

        msgstrb = " لك حوالة من" & "[DM]" & "يمكنك تحصيلها من أى صراف ألى للبنك المتحد "
        msgstrb += " الكود الأول " & "[BP]"
        msgstrb += " و كود الحركة " & "[TC]"
        msgstrb += " اسأل المرسل عن الكود الثانى"

        ' msgstrb = " اسأل المرسل عن الكود الثانى [TC] و كود الحركة [BP] يمكنك تحصيلها من أى صراف ألى للبنك المتحد الكود الأول [DM] لك حوالة من"
        msgstrb = "لك حوالة من" & "[DM]" & "يمكنك تحصيلها من أى صراف ألى للبنك المتحد الكود الأول" '& "و كود الحركة" & "[BM]" & "اسأل المرسل عن الكود الثانى" & "[TC]"



        xx = tc.BuildSMSBody(msgstrb)
        MsgBox(xx)
        xx = tc.BuildSMSBody("A Given amount was transfered to you from [DM]. You can collect it from any United Bank ATM using: Key 1: [BP] and Trx Code: [TC]. Ask the depositor for Key 2.")
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim xx As String
        Dim uc As String = ""
        Dim buf() As Byte
        Dim i As Integer
        Dim msgstrb As String
        xx = "ABCD"

        msgstrb = " لك حوالة من" & "*****56789" & "يمكنك تحصيلها من أى صراف ألى للبنك المتحد "
        msgstrb += " الكود الأول " & "1234"
        msgstrb += " كود الحركة " & "1111111111111"
        msgstrb += " اسأل المرسل عن الكود الثانى"
        MsgBox(msgstrb)
        buf = System.Text.Encoding.Unicode.GetBytes(msgstrb)
        For i = 0 To buf.Length - 1 Step 2
            uc += buf(i + 1).ToString("X2") & buf(i).ToString("X2")
        Next
        MsgBox(uc)
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Dim ss As New CustomerAlertService.SMSSendingClass
        Dim mid As String = ""
        Dim ret As Integer
        Dim sts As String = ""

        ret = ss.SendSMS("unitedbank", "Push", "First Class Test", "0194636017", "20101755196", "08", 30, mid, sts)

        MsgBox("ret=" & ret & vbNewLine & "Status=" & ss.Status)

    End Sub

    Private Sub test_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Dim st1 As String
        Dim st2 As String
        Dim t1 As DateTime
        Dim t2 As DateTime
        Dim t As DateTime
        st1 = "20:12:00"
        st2 = "23:1:00"

        t1 = st1
        t2 = st2
        t = Now.ToShortTimeString
        If t > t1 And t < t2 Then
            MsgBox("Between")
        End If

    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Dim x As New NCRMoneyFer.MessageClass

        Dim rtrn As Integer
        Dim ss As String
        Dim xx As String

        xx = "مصرف المتحد"
        ss = "و يمكنك تحصيلها من أى صراف آلى لل" & xx
        MsgBox(ss)
        NCRMoneyFer.CONFIGClass.readConfig()
        x.CCTrack2 = "4946069111111203=14041211000065100000"
        x.DebitAccountNumber = "010000002011771164"
        x.Amount = "100"
        rtrn = x.DODebitAccountAuthorization()

    End Sub

    Private Sub btniso_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btniso.Click
        Dim instr1 As String
        Dim isom As New NCRMoneyFer.iso8583
        Dim mcobj As New NCRMoneyFer.MessageClass
        Dim vreq As String = ""
        Dim rtrn As Integer
        Dim vrep As String = ""
        NCRMoneyFer.CONFIGClass.readConfig()
        mcobj.CCTrack2 = "5178020148753232=15112011108486300000"
        'mcobj.CCTrack2 = "5441117737633907=1404101000016319"
        mcobj.Amount = 10
        mcobj.Currency = "818"
        mcobj.ATMId = "25981400"
        mcobj.DebitAccountNumber = "00000000000000000"
        rtrn = mcobj.BuildIsoRequest_CC(vreq)
        If rtrn = 0 Then
            rtrn = mcobj.ContactSwitch(vreq, "10.40.13.33", vrep)
            If rtrn = 0 Then
                rtrn = isom.Parse(vrep)
            Else
                MsgBox("Contact Host error " & rtrn)
            End If
        End If



        'instr1 = "ISO0250000000210B63A80012E818010000000001000000340000300000001000000000001000012261725361725361725361226011901191181800627220374946069111111203=14041211000065100000000000000000      52BM04320520 BM EL04400000000000000000000000000000000000000000000000016+000            1181800494606006R  0                   "
        'rtrn = isom.Parse(instr1)
        MsgBox(isom.toString)
    End Sub

    Private Sub btnArabiocxml_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnArabiocxml.Click
        Dim xmlD As Xml.XmlDocument
        Dim items As Xml.XmlNodeList
        Dim item As Xml.XmlNode
        Dim tstr As String
        Dim tmpItem As Xml.XmlNode
        Dim cfgFile As String
        ' Dim xx As Integer
        Dim tmp As String
        Dim shortnameStart As Integer
        Dim prevIndex As Integer

        Try
            xmlD = New Xml.XmlDocument

            tmp = System.Reflection.Assembly.GetExecutingAssembly.Location
            shortnameStart = 0
            prevIndex = 0
            While shortnameStart <> -1
                prevIndex = shortnameStart
                shortnameStart = tmp.IndexOf("\", shortnameStart + 1)
            End While

            cfgFile = tmp.Substring(0, prevIndex + 1) & "config.xml"

            xmlD.Load(cfgFile)
        Catch ex As Exception
            MsgBox("Can Not Load Config.xml file ex:" & ex.ToString)
            Return
        End Try

        Try
            items = Nothing
            item = Nothing
            items = xmlD.GetElementsByTagName("BusinessRules")
            item = items(0)
            For Each tmpItem In item.ChildNodes

                If tmpItem.Name.ToUpper() = "BankArabicName".ToUpper() Then
                    tstr = tmpItem.InnerText

                End If

            Next

        Catch ex As Exception
            MsgBox("Error Reading [CustomerAlert] settings " & ex.ToString)
        End Try


    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Dim pdu As GsmComm.PduConverter.SmsSubmitPdu
        Dim port As Integer = 5
        Dim baudrate As Integer = 2400
        Dim timeout As Integer = 300 'ms
        Dim GSMPin As String = "0000"
        Dim cmm As GsmComm.GsmCommunication.GsmCommMain






        cmm = Nothing
        port = 6
        baudrate = 2400
        ''timeout = PTimeOut
        cmm = New GsmCommMain(port, baudrate, timeout)
        cmm.Open()


        pdu = New SmsSubmitPdu("kareem Test", "00201282840749", "")
        cmm.SendMessage(pdu)
        cmm.Close()


    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        'Dim sa As New NCR.EG.Remittance.BulkUploader.RemBulkFileService()
        'NCR.EG.Remittance.BulkUploader.ConfigClass.ReadConfig()
        'sa.MyStart()
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        'NCRMoneyFer.CONFIGClass.readConfig()
        'Dim mc As New NCRMoneyFer.MessageClass
        ''mc.NewDeActivateProcess()
        'mc.DoRequestNew("CON0000666613/04/202016:20:475123451234511666666")

        Dim filebuffer As Byte()
        Dim fileStream As Stream
        fileStream = File.OpenRead("C:\\NCR\\Mena Anis\\1.txt")
        ' Alocate memory space for the file
        ReDim filebuffer(fileStream.Length)
        fileStream.Read(filebuffer, 0, fileStream.Length)
        ' Open a TCP/IP Connection and send the data
        Dim clientSocket As New TcpClient("192.168.1.86", 1009)
        Dim networkStream As NetworkStream
        networkStream = clientSocket.GetStream()
        networkStream.Write(filebuffer, 0, fileStream.Length)
    End Sub
End Class
