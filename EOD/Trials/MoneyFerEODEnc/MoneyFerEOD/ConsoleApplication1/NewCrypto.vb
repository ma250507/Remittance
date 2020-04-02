Imports System.Security.Cryptography
Imports System.Text
Imports System.IO
Public Class NewCrypto
    Private Const KeyStr As String = "MXxRxCz0ZZXr17Rw"
    Private Const IVStr As String = "TveGowev"
    Private mvKey As String = "MXxRxCz0ZZXr17Rw"
    Private mvIV As String = "TveGowev"

    Public Property Key() As String
        Get
            Return mvKey

        End Get
        Set(ByVal Value As String)
            mvKey = Value
        End Set
    End Property
    Public Property IV() As String
        Get
            Return mvIV
        End Get
        Set(ByVal Value As String)
            mvIV = Value
        End Set
    End Property
    Public Function Encrypt(ByVal pData As String) As String
        Dim MyEncoding As Encoding
        Dim myRijndael As New RijndaelManaged
        Dim encrypted() As Byte
        Dim toEncrypt() As Byte
        Dim key() As Byte
        Dim IV() As Byte

        Dim i As Long
        Dim fh As Integer
        Dim sh As Integer
        Dim fhc As String
        Dim shc As String
        Dim encdata As String

        Try
            If pData = "" Then
                Return ""
            End If

            MyEncoding = Encoding.Unicode
            key = MyEncoding.GetBytes((mvKey & KeyStr).Substring(0, KeyStr.Length()).ToCharArray())
            IV = MyEncoding.GetBytes((mvIV & IVStr).Substring(0, IVStr.Length()).ToCharArray())

            Dim encryptor As ICryptoTransform = myRijndael.CreateEncryptor(key, IV)
            Dim msEncrypt As New MemoryStream
            Dim csEncrypt As New CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)

            'Convert the data to a byte array.
            toEncrypt = MyEncoding.GetBytes(pData.ToCharArray())

            'Write all data to the crypto stream and flush it.
            csEncrypt.Write(toEncrypt, 0, toEncrypt.Length)
            csEncrypt.FlushFinalBlock()

            encrypted = msEncrypt.ToArray()
            encdata = ""
            For i = 0 To encrypted.Length - 1
                fh = (encrypted(i) And 240) >> 4
                sh = encrypted(i) And 15
                fhc = hb2chr(fh)
                shc = hb2chr(sh)
                encdata = encdata & fhc & shc
            Next

            Return encdata
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Decrypt(ByVal eData As String) As String
        Dim roundtrip As String
        Dim MyEncoding As Encoding
        Dim myRijndael As New RijndaelManaged
        Dim fromEncrypt() As Byte

        Dim encrypted1() As Byte
        'Dim toEncrypt() As Byte
        Dim lkey() As Byte
        Dim lIV() As Byte

        Dim i, j As Long
        Dim fh As Integer
        Dim sh As Integer
        Dim fhc As String
        Dim shc As String

        Dim s As Integer
        Try
            If eData = "" Then
                Return ""
            End If

            s = eData.Length \ 2
            ReDim encrypted1(s - 1)
            i = 0
            For i = 0 To eData.Length - 1 Step 2
                fhc = eData.Substring(i, 1)
                shc = eData.Substring(i + 1, 1)
                fh = chr2hb(fhc)
                sh = chr2hb(shc)
                encrypted1(j) = (fh * 16 + sh)
                j += 1
            Next


            MyEncoding = Encoding.Unicode
            lkey = MyEncoding.GetBytes((mvKey & KeyStr).Substring(0, KeyStr.Length()).ToCharArray())
            lIV = MyEncoding.GetBytes((mvIV & IVStr).Substring(0, IVStr.Length()).ToCharArray())
            'Get an encryptor.
            Dim decryptor As ICryptoTransform = myRijndael.CreateDecryptor(lkey, lIV)

            'Now decrypt the previously encrypted message using the decryptor
            ' obtained in the above step.
            Dim msDecrypt As New MemoryStream(encrypted1, 0, encrypted1.Length)
            Dim csDecrypt As New CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)

            fromEncrypt = New Byte(encrypted1.Length - 1) {}

            'Read the data out of the crypto stream.

            j = csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length)
            ReDim Preserve fromEncrypt(j - 1)
            'Convert the byte array back into a string.


            roundtrip = MyEncoding.GetString(fromEncrypt)

            Return roundtrip
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function chr2hb(ByVal chr As String) As Integer
        Select Case chr
            Case "A"
                Return 0
            Case "B"
                Return 1
            Case "C"
                Return 2
            Case "D"
                Return 3
            Case "E"
                Return 4
            Case "F"
                Return 5
            Case "G"
                Return 6
            Case "H"
                Return 7
            Case "I"
                Return 8
            Case "J"
                Return 9
            Case "Z"
                Return 10
            Case "W"
                Return 11
            Case "Y"
                Return 12
            Case "X"
                Return 13
            Case "V"
                Return 14
            Case "U"
                Return 15
            Case Else
                Return -1
        End Select
    End Function
    Private Function hb2chr(ByVal hb As Integer) As String
        Select Case hb
            Case 0
                Return "A"
            Case 1
                Return "B"
            Case 2
                Return "C"
            Case 3
                Return "D"
            Case 4
                Return "E"
            Case 5
                Return "F"
            Case 6
                Return "G"
            Case 7
                Return "H"
            Case 8
                Return "I"
            Case 9
                Return "J"
            Case 10
                Return "Z"
            Case 11
                Return "W"
            Case 12
                Return "Y"
            Case 13
                Return "X"
            Case 14
                Return "V"
            Case 15
                Return "U"
            Case Else
                Return ""
        End Select
    End Function

End Class
