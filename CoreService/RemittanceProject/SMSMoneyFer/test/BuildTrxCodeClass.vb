Public Class BuildTrxCodeClass

    Public Shared Function GetTransactionCode(ByRef pGeneratedTrxCode As String, ByVal LockThread As System.Threading.Thread) As Integer

        ''This function generate Id depending on the Date / Time up to seconds
        '' to be sure that no doublicate Id's are generated .... a sleep for one second
        '' is invoked and all threads asking for iD's are  locked .

        Dim yy As Integer
        Dim mon As Integer
        Dim dy As Integer
        Dim hh As Long
        Dim mm As Long
        Dim ss As Long
        Dim ff As Long
        Dim datecode As Integer
        Dim datecodestr As String
        Dim timecode As Integer
        Dim timecodestr As String
        Dim ts As Date
        Dim keys As New Collection
        Dim k11str As String
        Dim k12str As String = ""

        Try
            Try
                SyncLock LockThread
                    ts = Now
                    System.Threading.Thread.Sleep(1000)
                End SyncLock
            Catch ex As Exception
                'Log.loglog("GetTransactionCode, LockThread EX:" & ex.ToString, False)
            End Try


            yy = ts.Year - 2000
            yy = yy And &H7F
            mon = ts.Month
            dy = ts.Day
            yy = yy << 9
            mon = mon And &HF
            mon = mon << 5
            dy = dy And &H1F
            hh = ts.Hour
            mm = ts.Minute
            ss = ts.Second
            ff = ts.Millisecond
            hh = hh And &H1F
            hh = hh << 12
            mm = mm And &H3F
            mm = mm << 6
            ss = ss And &H3F
            ff = ff And &H3F
            datecode = yy + mon + dy
            timecode = hh + mm + ss + ff
            datecodestr = datecode.ToString("00000")
            timecodestr = timecode.ToString("000000")
            k11str = datecodestr & timecodestr
            addCheckDigit(k11str, k12str)
            pGeneratedTrxCode = k12str
            Return 0

        Catch ex As Exception
            'log.loglog("GeneratetrxCode, Ex:" & ex.ToString, False)
            Return 9
        End Try

    End Function
    Private Shared Sub addCheckDigit(ByVal idNoCD As String, ByRef idCD As String)
        Dim sid As String

        Dim sidpure As String
        Dim c1 As String
        Dim c2 As String
        Dim c3 As String
        Dim c4 As String
        Dim c5 As String
        Dim c6 As String
        Dim c7 As String
        Dim c8 As String
        Dim c9 As String
        Dim c10 As String
        Dim c11 As String

        Dim v1 As Integer
        Dim v2 As Integer
        Dim v3 As Integer
        Dim v4 As Integer
        Dim v5 As Integer
        Dim v6 As Integer
        Dim v7 As Integer
        Dim v8 As Integer
        Dim v9 As Integer
        Dim v10 As Integer
        Dim v11 As Integer

        Dim vt As Integer
        Dim cdg As Integer
        If idNoCD.Length < 11 Then
            idCD = ""
            Exit Sub
        End If
        sidpure = idNoCD
        c1 = sidpure.Substring(10, 1)
        c2 = sidpure.Substring(9, 1)
        c3 = sidpure.Substring(8, 1)
        c4 = sidpure.Substring(7, 1)
        c5 = sidpure.Substring(6, 1)
        c6 = sidpure.Substring(5, 1)
        c7 = sidpure.Substring(4, 1)
        c8 = sidpure.Substring(3, 1)
        c9 = sidpure.Substring(2, 1)
        c10 = sidpure.Substring(1, 1)
        c11 = sidpure.Substring(0, 1)

        v2 = Val(c2)
        v4 = Val(c4)
        v6 = Val(c6)
        v8 = Val(c8)
        v10 = Val(c10)
        v1 = 2 * Val(c1) : If (v1 >= 10) Then v1 = v1 - 9
        v3 = 2 * Val(c3) : If (v3 >= 10) Then v3 = v3 - 9
        v5 = 2 * Val(c5) : If (v5 >= 10) Then v5 = v5 - 9
        v7 = 2 * Val(c7) : If (v7 >= 10) Then v7 = v7 - 9
        v9 = 2 * Val(c9) : If (v9 >= 10) Then v9 = v9 - 9
        v11 = 2 * Val(c11) : If (v11 >= 10) Then v11 = v11 - 9
        vt = v1 + v2 + v3 + v4 + v5 + v6 + v7 + v8 + v9 + v10 + v11
        cdg = (10 - vt Mod 10) Mod 10
        sid = sidpure.Substring(0, 11) & cdg
        idCD = sid

    End Sub
End Class
