<System.ComponentModel.RunInstaller(True)> Partial Class ProjectInstaller
    Inherits System.Configuration.Install.Installer

    'Installer overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Component Designer
    'It can be modified using the Component Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.NCRMoneyFerServicePI = New System.ServiceProcess.ServiceProcessInstaller
        Me.NCRMoneyFerServiceI = New System.ServiceProcess.ServiceInstaller
        '
        'NCRMoneyFerServicePI
        '
        Me.NCRMoneyFerServicePI.Account = System.ServiceProcess.ServiceAccount.LocalSystem
        Me.NCRMoneyFerServicePI.Password = Nothing
        Me.NCRMoneyFerServicePI.Username = Nothing
        '
        'NCRMoneyFerServiceI
        '
        Me.NCRMoneyFerServiceI.ServiceName = "NCRMoneyFerService"
        '
        'ProjectInstaller
        '
        Me.Installers.AddRange(New System.Configuration.Install.Installer() {Me.NCRMoneyFerServicePI, Me.NCRMoneyFerServiceI})

    End Sub
    Friend WithEvents NCRMoneyFerServicePI As System.ServiceProcess.ServiceProcessInstaller
    Friend WithEvents NCRMoneyFerServiceI As System.ServiceProcess.ServiceInstaller

End Class
