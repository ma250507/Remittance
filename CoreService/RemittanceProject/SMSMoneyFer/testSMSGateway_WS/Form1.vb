Public Class Form1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim smsobj As New NBESMSWS.SMSService

        Dim userName As String
        Dim pASSWORD As String
        Dim channel As String
        Dim language As String
        Dim mode As String
        Dim receiverMobile As String
        Dim Body As String

        Dim transDate As Date
        Dim p1, p2, p3, p4, p5 As String
        Dim ret As String


        Try

        
            userName = txtBoxUserName.Text
            pASSWORD = txtBoxPassword.Text

            channel = txtBoxChannel.Text
            language = txtboxLanguage.Text
            mode = txtBoxMode.Text
            receiverMobile = txtBoxReceiverMobile.Text
            Body = txtboxBody.Text
            transDate = Now
            
            p1 = ""
            p2 = ""
            p3 = ""
            p4 = ""
            p5 = ""
           
            Dim ParamList() As String = {Body}

            smsobj.Url = txtBoxURL.Text
            ret = smsobj.SendSMS(userName, pASSWORD, channel, language, mode, receiverMobile, ParamList, transDate, p1, p2, p3, p4, p5)
            MsgBox("RET=[" & ret & "]")

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try







    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim status As String = ""
        Dim StatusText As String
        Dim DocumentText As String
        Dim SendingStatus As String
        Dim MsgId As String
        'Dim finalURL As String
        'Dim ts As Date
        ' Dim TSpan As TimeSpan
        Dim lastPos As Integer
        Dim AllresponseText As String
        Dim comaLastPos As Integer
        Dim ComaPos As Integer
        Dim reformatedReceipienntnumber As String = ""
        'Dim lRecepient As String
        Dim pMSGTEXT As String = ""


        StatusText = "Done"
        DocumentText = "MessageID=SAR-2011,Credit balance=58388,Status=4,Mobile=20117200082<!DOCTYPE "
        status += vbNewLine & "SendSMS,Doc Complete StatusText=[" & StatusText & "] DocumentText=[" & DocumentText & "]"
        If StatusText.ToUpper <> "DONE" Then
            status += vbNewLine & "SendSMS,Bad Navigation Status [" & StatusText & "]"


        End If

        lastPos = DocumentText.IndexOf("<!")
        If lastPos < 2 Then
            status += vbNewLine & "SendSMS,Bad response [" & DocumentText & "]"

        End If

        Try


            comaLastPos = -1

            AllresponseText = DocumentText.Substring(0, lastPos - 1)
            lastPos = AllresponseText.IndexOf("MessageID")
            If lastPos < 0 Then
                status += vbNewLine & "SendSMS,Bad response no MessageID tag [" & DocumentText & "]"

            End If
            ComaPos = AllresponseText.IndexOf(",", comaLastPos + 1)
            MsgId = AllresponseText.Substring(lastPos + 10, ComaPos - lastPos - 10)


            comaLastPos = ComaPos


            lastPos = AllresponseText.IndexOf("Status")
            If lastPos < 0 Then
                status += vbNewLine & "SendSMS,Bad response no Status tag [" & DocumentText & "]"
            End If
            ComaPos = AllresponseText.IndexOf(",", lastPos + 1)
            SendingStatus = AllresponseText.Substring(lastPos + 7, ComaPos - lastPos - 7)

            status += vbNewLine & "SendSMS, Message Sent Successfully MessageID=[" & MsgId & "] Status=[" & SendingStatus & "]"
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub

    Private Sub UBSMSTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UBSMSTest.Click
        Dim ub As New UBSMS.SendSMS2
        Dim mob As String = ""
        Dim msgtxt As String = ""
        Dim usr As String = ""
        Dim pwd As String = ""
        Dim prtry As String = ""
        Dim rtrn As String = ""
        Try
            ub.Url = txtUBURL.Text
            mob = txtUBMob.Text
            msgtxt = txtUBBody.Text
            usr = txtUBUser.Text
            pwd = txtUBPWD.Text
            prtry = txtUBPriority.Text

            rtrn = ub.Send_SMS(mob, msgtxt, "", "", "", usr, pwd, prtry)
            MsgBox(rtrn)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        
    End Sub

    Private Sub txtUBPWD_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUBPWD.TextChanged

    End Sub
End Class
