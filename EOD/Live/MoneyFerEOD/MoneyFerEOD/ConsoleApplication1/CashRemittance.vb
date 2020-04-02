Module CashRemittance

    Sub Main()
        Dim EODDate As String
        Dim CreateTimeFlag, GetEODFlag, WriteFileFlag, WriteFileFlagDBT, WriteFileFlagCRDT, UpdateFlag, UpdateFlagDBT, UpdateFlagCRDT, SplitFlag As Boolean
        Dim DT As DataTable
        Dim CardlessRows, DebitRows, CreditRows As DataRow()
        ReadConfig()
        CreateTimeFlag = CreateEODDateTime(EODTime, EODDate)
        If CreateTimeFlag = True Then
            GetEODFlag = GetMoneyFerEODData(EODDate, DT)
            If GetEODFlag = True Then
                'WriteFileFlag = WriteEODFile(DT)
                SplitFlag = SplitDT(DT, CardlessRows, DebitRows, CreditRows)
                If (SplitFlag = True) Then
                    WriteFileFlag = WriteEODFile(CardlessRows, "Cardless")
                    WriteFileFlagDBT = WriteEODFile(DebitRows, "Debit")
                    WriteFileFlagCRDT = WriteEODFile(CreditRows, "Credit")
                    If (WriteFileFlag) Then
                        UpdateFlag = UpdateEODData(CardlessRows)
                        If UpdateFlag = True Then
                            loglog("EOD File Created Successfully For Cardless Transactions For Date = " & EODDate, True)
                        Else
                            loglog("EOD File Creation Failed For Cardless Transactions For Date = " & EODDate, True)
                            System.IO.File.Delete(FilePath)
                        End If
                    Else
                        Exit Sub
                    End If
                    If (WriteFileFlagDBT) Then
                        UpdateFlagDBT = UpdateEODData(DebitRows)
                        If UpdateFlagDBT = True Then
                            loglog("EOD File Created Successfully For Debit Transactions For Date = " & EODDate, True)
                        Else
                            loglog("EOD File Creation Failed For Debit Transactions For Date = " & EODDate, True)
                            System.IO.File.Delete(FilePath)
                        End If
                    Else
                        Exit Sub
                    End If
                    If (WriteFileFlagCRDT) Then
                        UpdateFlagCRDT = UpdateEODData(CreditRows)
                        If UpdateFlagCRDT = True Then
                            loglog("EOD File Created Successfully For Credit Transactions For Date = " & EODDate, True)
                        Else
                            loglog("EOD File Creation Failed For Credit Transactions For Date = " & EODDate, True)
                            System.IO.File.Delete(FilePath)
                        End If
                    Else
                        Exit Sub
                    End If
                End If
            Else
                Exit Sub
            End If
        Else
            Exit Sub
        End If
    End Sub

End Module
