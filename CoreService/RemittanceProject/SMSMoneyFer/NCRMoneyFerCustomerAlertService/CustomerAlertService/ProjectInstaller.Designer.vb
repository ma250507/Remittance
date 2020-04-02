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
        Me.NCRMoneyFerCustomerAlertSPInstaller = New System.ServiceProcess.ServiceProcessInstaller
        Me.NCRMoneyFerCustomerAlertSInstaller = New System.ServiceProcess.ServiceInstaller
        '
        'NCRMoneyFerCustomerAlertSPInstaller
        '
        Me.NCRMoneyFerCustomerAlertSPInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem
        Me.NCRMoneyFerCustomerAlertSPInstaller.Password = Nothing
        Me.NCRMoneyFerCustomerAlertSPInstaller.Username = Nothing
        '
        'NCRMoneyFerCustomerAlertSInstaller
        '
        Me.NCRMoneyFerCustomerAlertSInstaller.ServiceName = "NCRMoneyFerCustomerAlertService"
        '
        'ProjectInstaller
        '
        Me.Installers.AddRange(New System.Configuration.Install.Installer() {Me.NCRMoneyFerCustomerAlertSPInstaller, Me.NCRMoneyFerCustomerAlertSInstaller})

    End Sub
    Friend WithEvents NCRMoneyFerCustomerAlertSPInstaller As System.ServiceProcess.ServiceProcessInstaller
    Friend WithEvents NCRMoneyFerCustomerAlertSInstaller As System.ServiceProcess.ServiceInstaller

End Class
