Attribute VB_Name = "ini"
Option Explicit

Public Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As Any, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Long, ByVal lpFileName As String) As Long
Public Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As Any, ByVal lpString As Any, ByVal lpFileName As String) As Long
Public Function GetSecKeyValue(FileName As String, SectionName As String, KeyName As String, DefaultValue As String) As String
Dim ret As Long
Dim nSize As Long
Dim MyKeys As String

Dim StrsTr As String * 1024
nSize = 1024
MyKeys = Chr(34) & KeyName & Chr(34)


If SectionName = "" Or KeyName = "" Or FileName = "" Then
   GetSecKeyValue = ""
   Exit Function
End If

ret = GetPrivateProfileString(SectionName, MyKeys, DefaultValue, StrsTr, nSize, FileName)

If ret = 0 Then

GetSecKeyValue = ""
Else

 GetSecKeyValue = Left(StrsTr, ret)
End If


End Function

Public Function PutSecKeyValue(FileName As String, SectionName As String, KeyName As String, StrValue As String) As Boolean
Dim ret As Long
Dim nSize As Long
Dim MyKeys As String
Dim MyValue As String

Dim StrsTr As String * 1024
nSize = 1024
MyKeys = Chr(34) & KeyName & Chr(34)
MyValue = Chr(34) & StrValue & Chr(34)
If SectionName = "" Or KeyName = "" Or FileName = "" Then
   PutSecKeyValue = False
   Exit Function
End If
ret = WritePrivateProfileString(SectionName, MyKeys, MyValue, FileName)

If ret = 0 Then
PutSecKeyValue = False
Else
 PutSecKeyValue = True
End If




End Function

