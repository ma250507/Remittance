VERSION 5.00
Begin VB.UserControl CheckCheckDigit 
   ClientHeight    =   855
   ClientLeft      =   0
   ClientTop       =   0
   ClientWidth     =   915
   ScaleHeight     =   855
   ScaleWidth      =   915
End
Attribute VB_Name = "CheckCheckDigit"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True


Public Function CheckCheckDigit(TrxCode As String) As String
Dim x As String
Dim y As String
        x = TrxCode
        Call addCheckDigit(x, y)
        If x = y Then
           CheckCheckDigit = "YES"
        Else
           CheckCheckDigit = "NO"
        End If

End Function


    Private Sub addCheckDigit(ByVal idNoCD As String, ByRef idCD As String)
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
        If Len(idNoCD) < 11 Then
            idCD = ""
            Exit Sub
        End If
        sidpure = idNoCD
        c1 = Mid(sidpure, 11, 1)
        c2 = Mid(sidpure, 10, 1)
        c3 = Mid(sidpure, 9, 1)
        c4 = Mid(sidpure, 8, 1)
        c5 = Mid(sidpure, 7, 1)
        c6 = Mid(sidpure, 6, 1)
        c7 = Mid(sidpure, 5, 1)
        c8 = Mid(sidpure, 4, 1)
        c9 = Mid(sidpure, 3, 1)
        c10 = Mid(sidpure, 2, 1)
        c11 = Mid(sidpure, 1, 1)

        v2 = Val(c2)
        v4 = Val(c4)
        v6 = Val(c6)
        v8 = Val(c8)
        v10 = Val(c10)
        v1 = 2 * Val(c1): If (v1 >= 10) Then v1 = v1 - 9
        v3 = 2 * Val(c3): If (v3 >= 10) Then v3 = v3 - 9
        v5 = 2 * Val(c5): If (v5 >= 10) Then v5 = v5 - 9
        v7 = 2 * Val(c7): If (v7 >= 10) Then v7 = v7 - 9
        v9 = 2 * Val(c9): If (v9 >= 10) Then v9 = v9 - 9
        v11 = 2 * Val(c11): If (v11 >= 10) Then v11 = v11 - 9
        vt = v1 + v2 + v3 + v4 + v5 + v6 + v7 + v8 + v9 + v10 + v11
        cdg = (10 - vt Mod 10) Mod 10
        sid = Left(sidpure, 11) & cdg
        idCD = sid

    End Sub


