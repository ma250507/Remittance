Imports System.IO
Imports System.Text

Public Class iso8583


    Private mvMessage As String
    Private mvFieldValue(128) As String
    '                                        *11111111111111111111111111111111111111111111*  *22222222222222222222222222222222222222222222*  *33333333333333333333333333333333333333333333*  *44444444444444444444444444444444444444444444*  *55555555555555555555555555555555555555555555*  *66666666666666666666666666666666666666666666*  *77777777777777777777777777777777777777777777*  *88888888888888888888888888888888888888888888*
    '                                     0  1  2  3  4  5  6  7  8  9 10 11 12 13 14 15 16  1  2  3  4  5  6  7  8  9 10 11 12 13 14 15 16  1  2  3  4  5  6  7  8  9 10 11 12 13 14 15 16  1  2  3  4  5  6  7  8  9 10 11 12 13 14 15 16  1  2  3  4  5  6  7  8  9 10 11 12 13 14 15 16  1  2  3  4  5  6  7  8  9 10 11 12 13 14 15 16  1  2  3  4  5  6  7  8  9 10 11 12 13 14 15 16  1  2  3  4  5  6  7  8  9 10 11 12 13 14 15 16
    Private mvFieldLenTyp() As Integer = {0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 2, 3, 0, 0, 0, 0, 0, 0, 0, 2, 2, 0, 3, 3, 0, 0, 0, 0, 0, 3, 3, 0, 3, 3, 3, 3, 3, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 2, 2, 3, 3, 3, 3, 3, 0, 0, 3, 0, 3, 0, 0, 0, 0, 0, 0, 3, 0, 3, 3, 0, 0, 3, 3, 0}

    '                                          *1111111111111111111111111111111111111111111111111*  *2222222222222222222222222222222222222222222222*  *3333333333333333333333333333333333333333333333333333333333* *4444444444444444444444444444444444444444444444444444444* *55555555555555555555555555555555555555555555*  *6666666666666666666666666666666666666666666666*  *7777777777777777777777777777777777777777777777777777777777777* *88888888888888888888888888888888888888888888888888888*
    '                                      0   1   2  3   4   5   6   7  8  9 10 11 12 13 14 15 16  1  2  3  4  5  6  7  8  9 10 11 12 13 14 15  16   1  2   3   4    5   6  7  8   9 10   11  12  13 14  15   16  1  2  3  4  5   6    7   8  9  10  11  12  13  14  15 16  1  2  3  4  5  6  7  8  9 10 11 12 13 14 15 16  1  2  3  4  5  6  7  8  9  10 11 12 13 14  15 16  1  2   3  4   5   6   7  8    9    10   11   12  13 14  15  16  1   2  3  4  5  6  7  8  9 10   11 12  13   14   15 16
    Private mvFieldLength() As Integer = {16, 16, 19, 6, 12, 12, 12, 10, 8, 8, 8, 6, 6, 4, 4, 4, 4, 4, 4, 3, 3, 3, 3, 3, 0, 2, 2, 1, 9, 9, 9, 9, 11, 11, 28, 37, 104, 12, 6, 2, 3, 16, 15, 40, 25, 76, 0, 100, 25, 3, 3, 3, 8, 16, 120, 88, 0, 3, 10, 17, 16, 100, 0, 50, 8, 0, 0, 2, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 42, 0, 0, 0, 0, 42, 0, 0, 25, 0, 11, 0, 28, 28, 100, 255, 255, 255, 255, 0, 0, 255, 0, 11, 0, 0, 0, 0, 0, 0, 6, 0, 11, 50, 0, 432, 100, 100, 8}
    Private mvHeaderValue As String = CONFIGClass.ISOHeader
    Private mvTypeValue As String
    Private mvFieldIsFound(128) As Boolean
    Private Const Base24ISO8583HeaderLength As Integer = 12
    Private Const Base24ISO8583TypeLength As Integer = 4





    Public Function Parse(ByVal Message As String) As Integer
        Dim currentPos As Integer
        Dim mvFirstBitMapFirstHalfByte As String
        Dim mvFirstBitMapFirstHalfByteBinary As String
        Dim mvCurrentChar As String
        Dim mvCurrentCharBinary As String
        Dim char_indx As Integer
        Dim bit_indx As Integer
        Dim mvCurrentBit As String
        Dim mvFieldIndex As Integer
        Dim mvAllBitMap As String
        Dim mvCurrFieldLenPartStr As String
        Dim mvCurrFieldLen As Integer
        Dim mvtakenLength As Integer
        Dim mvTakenStaring As String

        mvMessage = Message
        If mvMessage.Length < Base24ISO8583HeaderLength Then
            log.loglog("Input Messahe [" & mvMessage & "] has Length is less than Header Length:" & Base24ISO8583HeaderLength, True)
            Return 1
        End If
        currentPos = 0
        Try
            mvHeaderValue = mvMessage.Substring(currentPos, Base24ISO8583HeaderLength)
        Catch ex As Exception
            log.loglog("Basd Input Messahe [" & mvMessage & "] exp:" & ex.ToString, True)
            Return 1
        End Try
        currentPos += Base24ISO8583HeaderLength
        Try
            mvTypeValue = mvMessage.Substring(currentPos, Base24ISO8583TypeLength)
        Catch ex As Exception
            log.loglog("Bad Input Messahe [" & mvMessage & "] exp:" & ex.ToString, True)
            Return 2
        End Try

        currentPos += Base24ISO8583TypeLength
        mvFieldValue(0) = "0000000000000000" ' first BitMap
        mvFieldValue(1) = "0000000000000000" ' 2nd    BitMap
        Try
            mvFieldValue(0) = mvMessage.Substring(currentPos, mvFieldLength(0))
        Catch ex As Exception
            log.loglog("Bad Input Messahe [" & mvMessage & "] exp:" & ex.ToString, True)
            Return 3
        End Try
        currentPos += mvFieldLength(0)

        mvFirstBitMapFirstHalfByte = mvFieldValue(0).Substring(0, 1)
        mvFirstBitMapFirstHalfByteBinary = HexStr2Integer(mvFirstBitMapFirstHalfByte)

        If mvFirstBitMapFirstHalfByteBinary.Substring(0, 1) = "1" Then '''' second Bit map part is here
            Try
                mvFieldValue(1) = mvMessage.Substring(currentPos, mvFieldLength(1))
                mvFieldIsFound(1) = True
            Catch ex As Exception
                log.loglog("Bad Input Messahe [" & mvMessage & "] exp:" & ex.ToString, True)
                Return 4
            End Try
            currentPos += mvFieldLength(1)
        Else
            mvFieldIsFound(1) = False
        End If




        Try
            mvAllBitMap = mvFieldValue(0) & mvFieldValue(1)
            For char_indx = 0 To 31
                mvCurrentChar = mvAllBitMap.Substring(char_indx, 1)
                mvCurrentCharBinary = HexStr2Integer(mvCurrentChar)
                For bit_indx = 1 To 4
                    mvFieldIndex = char_indx * 4 + bit_indx

                    mvCurrentBit = mvCurrentCharBinary.Substring(bit_indx - 1, 1)
                    If mvCurrentBit = "1" And mvFieldIndex > 1 Then 'field is found and not 2nd biot map one
                        mvFieldIsFound(mvFieldIndex) = True
                        If mvFieldLenTyp(mvFieldIndex) = 0 Then ' ''fixed length field
                            mvtakenLength = mvFieldLength(mvFieldIndex)
                            mvTakenStaring = mvMessage.Substring(currentPos, mvtakenLength)
                            mvFieldValue(mvFieldIndex) = mvTakenStaring
                            currentPos += mvtakenLength
                        Else 'var length field
                            mvCurrFieldLenPartStr = mvMessage.Substring(currentPos, mvFieldLenTyp(mvFieldIndex))
                            Try
                                mvCurrFieldLen = Integer.Parse(mvCurrFieldLenPartStr)
                            Catch exParse As Exception
                                log.loglog("can not parse Length part of the field:" & mvFieldIndex & " value:" & mvCurrFieldLenPartStr & " exParse:" & exParse.ToString, True)
                                Return 5
                            End Try
                            currentPos += mvFieldLenTyp(mvFieldIndex)
                            mvTakenStaring = mvMessage.Substring(currentPos, mvCurrFieldLen)
                            mvFieldValue(mvFieldIndex) = mvTakenStaring
                            currentPos += mvCurrFieldLen
                        End If
                    Else ' fireld is not found
                        If mvFieldIndex <> 1 Then
                            mvFieldValue(mvFieldIndex) = ""
                            mvFieldIsFound(mvFieldIndex) = False
                        End If
                    End If

                Next

            Next
        Catch ex As Exception
            log.loglog(" parsing error stop at field:" & mvFieldIndex & "  ex:" & ex.ToString, True)
            Return 9

        End Try
        Return 0

    End Function
    
    
    Public Function getDataField(ByVal FieldIndex As Integer) As String
        Return mvFieldValue(FieldIndex)
    End Function
    Public Function getHeader() As String
        Return mvHeaderValue
    End Function
    Public Function getMessageType() As String
        Return mvTypeValue
    End Function


    Private Function HexStr2Integer(ByVal HexString As String) As String
        Select Case HexString
            Case "0"
                Return "0000"
            Case "1"
                Return "0001"
            Case "2"
                Return "0010"
            Case "3"
                Return "0011"
            Case "4"
                Return "0100"
            Case "5"
                Return "0101"
            Case "6"
                Return "0110"
            Case "7"
                Return "0111"
            Case "8"
                Return "1000"
            Case "9"
                Return "1001"
            Case "A", "a"
                Return "1010"
            Case "B", "b"
                Return "1011"
            Case "C", "c"
                Return "1100"
            Case "D", "d"
                Return "1101"
            Case "E", "e"
                Return "1110"
            Case "F", "f"
                Return "1111"
            Case Else
                Return "0000"
        End Select
    End Function

    Public Overrides Function toString() As String
        Dim i As Integer
        Dim fs As String

        fs = ""
        fs += "Iso Header        =[" & mvHeaderValue & "]" & vbNewLine
        fs += "Iso Type          =[" & mvTypeValue & "]" & vbNewLine
        fs += "Iso FirstBM       =[" & mvFieldValue(0) & "]" & vbNewLine
        If mvFieldIsFound(1) = True Then
            fs += "Iso SecondBM      =[" & mvFieldValue(1) & "]" & vbNewLine
        End If
        For i = 2 To 128
            If mvFieldIsFound(i) = True Then
                fs += "Data Element(" & i.ToString("000") & ") =[" & mvFieldValue(i) & "]" & vbNewLine
            End If
        Next
        Return fs
    End Function



    Public Sub New()
        Dim i As Integer
        For i = 0 To 128
            mvFieldValue(i) = ""
        Next
    End Sub
End Class


