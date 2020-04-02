VERSION 5.00
Object = "{12477D63-AAE9-11CE-9BB7-444553540000}#2.3#0"; "WdUXDsp.Ocx"
Begin VB.Form frmDisplay 
   Caption         =   "Display"
   ClientHeight    =   5610
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   7245
   LinkTopic       =   "Form1"
   ScaleHeight     =   5610
   ScaleWidth      =   7245
   StartUpPosition =   3  'Windows Default
   Begin VB.Frame Frame1 
      Enabled         =   0   'False
      Height          =   4815
      Left            =   120
      TabIndex        =   2
      Top             =   120
      Width           =   7095
      Begin ObjectXUnixDisplay.ObjectXUnixDisplay ObjectXUnixDisplay1 
         Height          =   4455
         Left            =   120
         TabIndex        =   3
         Top             =   240
         Width           =   6855
         DisplayTop      =   16
         DisplayLeft     =   8
         DisplayHeight   =   297
         DisplayWidth    =   457
         AutoFontMinimumWidth=   5
         CursorBlinkRate =   300
         CursorSize      =   2
         CursorVisible   =   -1  'True
         HostInterface.AutoConnect=   0   'False
         HostInterface.AutoDisconnect=   0   'False
         HostInterface.ExitOnDisconnect=   0   'False
         HostInterface.Name=   "WallData.Telnet"
         HostInterface.Interface.IpAddr=   ""
         HostInterface.Interface.TcpPort=   0
         HostInterface.Interface.Timeout=   0
         HostInterface.Interface.SslEnabled=   0   'False
         HostInterface.Interface.SslAuthentication=   0   'False
         HostInterface.Interface.CacheTimeout=   16124164
         HostInterfaceConfiguration=   1
         HostOffLine     =   0   'False
         TerminalId      =   11
         SplitDisplay    =   0
         HoldSession     =   0   'False
         LocalEcho       =   0   'False
         CursorRow       =   1
         CursorColumn    =   1
         UnixFileTransfer.Operation=   1
         UnixFileTransfer.Type=   0
         UnixFileTransfer.Protocol=   0
         UnixFileTransfer.AutoCallingProtocol=   0   'False
         UnixFileTransfer.Mode=   8
         UnixFileTransfer.Environment=   0
         UnixFileTransfer.LocalName=   ""
         UnixFileTransfer.RemoteName=   ""
         UnixFileTransfer.ToHostProgram=   ""
         UnixFileTransfer.FromHostProgram=   ""
         Clipboard.TextFormat=   -1  'True
         Clipboard.BIFFFormat=   -1  'True
         Clipboard.BitmapFormat=   -1  'True
         Clipboard.PasteLinkFormat=   -1  'True
         Clipboard.ParseDataOnBoundaries=   1
         Clipboard.PromptOnParse=   0   'False
         Clipboard.PasteTextWrapping=   -1  'True
         Watermark.Name  =   ""
         Watermark.Visible=   0   'False
         Watermark.Style =   0
         Watermark.TextBackground=   1
         Keyboard.MapFile=   ""
         WatermarkConfiguration=   1
         ClipboardConfiguration=   0
         KeyboardConfiguration=   1
         ScreenIdVersion =   0
         BackColor       =   0
         ReportInformation=   0   'False
         EventVersion    =   1
         PrintMode       =   0
         LoggingMode     =   0   'False
         ANSITermSupport =   0   'False
         LinesPerPage    =   24
         TextAutoWrap    =   0   'False
         LicenseScheme   =   0
         OnScreenStatusLine=   0
         ScreenScrollLines=   24
         HistoryLines    =   400
         ControlCodeSize =   0
         InterpretControlCodes=   0
         BackspaceKeyMode=   0
         EnterKeyMode    =   0
         CursorKeyMode   =   0
         NumericKeypadMode=   0
         F1LocalKeyMode  =   0
         F2LocalKeyMode  =   0
         F3LocalKeyMode  =   0
         F4LocalKeyMode  =   0
         F5LocalKeyMode  =   0
         CharacterSet    =   0
         SupplementalCharSet=   1
         NRCSet          =   0
         EnableAnswerback=   -1  'True
         VerticalCoupling=   -1  'True
         HorizontalCoupling=   0   'False
         PageCoupling    =   -1  'True
         LockUserDefinedKeys=   0   'False
         LockUserFeatures=   0   'False
         MarginBell      =   0   'False
         PrintingStyle   =   0
         PrintTranslateNRC=   0   'False
         PrintFinalFormFeed=   -1  'True
         PrinterTimeout  =   2
      End
   End
   Begin VB.Timer Timer2 
      Enabled         =   0   'False
      Interval        =   1000
      Left            =   1680
      Top             =   5040
   End
   Begin VB.Timer Timer1 
      Enabled         =   0   'False
      Interval        =   2000
      Left            =   2160
      Top             =   5040
   End
   Begin VB.CommandButton Command1 
      Cancel          =   -1  'True
      Caption         =   "&Hide"
      Height          =   495
      Left            =   240
      TabIndex        =   0
      Top             =   5040
      Width           =   1215
   End
   Begin VB.Shape SHPStatus 
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   495
      Left            =   6240
      Top             =   5040
      Width           =   495
   End
   Begin VB.Label lblStatus 
      Height          =   495
      Left            =   2760
      TabIndex        =   1
      Top             =   5040
      Width           =   3255
   End
End
Attribute VB_Name = "frmDisplay"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Dim FirstTime As Boolean



Private Sub Command1_Click()
Me.Hide
End Sub

Private Sub Form_Activate()
DoEvents
    If FirstTime = True Then
      FirstTime = False
      Timer2.Interval = 1000
      Timer2.Enabled = True
    End If
End Sub

Private Sub Form_Load()
FirstTime = True
End Sub

Private Sub Form_Unload(Cancel As Integer)
End
End Sub

Private Sub ObjectXUnixDisplay1_AfterConnect(ByVal Success As Long)
ConnectionSuccess = Success
lblStatus.Caption = "After Connect Success=" & Success
End Sub

Private Sub ObjectXUnixDisplay1_AfterDisconnect(ByVal Success As Long)
lblStatus.Caption = "After DisConnect Success=" & Success
End Sub

Private Sub Timer1_Timer()
Dim InterfaceErrorWindowHndler As Long
Dim retlong  As Long
Dim retlong1  As Long
Dim ParentWinH As Long
Dim parentName As String
Dim ParentNameLenth As Long
Dim LL As Long
Dim retry As Long

DoEvents

     Timer1.Enabled = False
     InterfaceErrorWindowHndler = FindWindow(vbNullString, "Interface Error")
     If InterfaceErrorWindowHndler = 0 Then
        InterfaceErrorWindowHndler = FindWindow(0&, "Telnet Application")
     End If
     If InterfaceErrorWindowHndler <> 0 Then
       
       If InterfaceErrorWindowHndler <> 0 And IsWindow(InterfaceErrorWindowHndler) = 1 Then
               retlong = PostMessage(InterfaceErrorWindowHndler, WM_CLOSE, vbNull, vbNull)
               retlong1 = WaitForSingleObject(InterfaceErrorWindowHndler, 1000) 'INFINITE)
               DoEvents
              
        End If
    End If
   Timer1.Enabled = True

End Sub

Private Sub Timer2_Timer()
    
    Dim ret As Integer
      Dim reqData As String
    Dim hret As Long
    Dim rcvData As String
    Dim repBody As String
    Dim trxs As TrxCollection
    Dim ExitReq As String
    
    ExitReq = Space(110) & "#"
    Dim trx As TrxClass
          Timer2.Enabled = False
          DoEvents
          Set trxs = New TrxCollection
          
          ret = GetNoneFlagedTransaction(trxs)
        If trxs.Count < 1 Then
            lblStatus.Caption = "There is no transaction for host update..."
        Else
          For Each trx In trxs
          
          
                hc.hlog ("Will Process Request trxcode =[" & trx.TransactionCode & "] on host")
                reqData = ""
                reqData = trx.HostRequestData()
                hret = hc.SendRecieve(reqData, rcvData)
                If hret = 0 Then
                   If rcvData = "00" Or rcvData = "12" Then
                      Call trx.SetTrxHostFalg(1, rcvData)
                   Else
                      hc.hlog "Host Response for trx [" & trx.TransactionCode & "] is [" & rcvData & "]"
                      Call trx.SetTrxHostFalg(0, rcvData)
                   End If
                Else
                 hc.hlog "sendReceive for trx [" & trx.TransactionCode & "] returns [" & hret & "]"
                End If
                 Set trx = Nothing
                 DoEvents
           Next
            hc.hlog ("Will Send Application Exit Request  =[" & ExitReq & "] ")
            If hc.IsHostReady = 0 Then
                hret = hc.SendRecieve(ExitReq, rcvData)
           End If
           hc.Dissconnect
         End If
         
           Set trxs = Nothing
         Timer2.Enabled = True
End Sub
