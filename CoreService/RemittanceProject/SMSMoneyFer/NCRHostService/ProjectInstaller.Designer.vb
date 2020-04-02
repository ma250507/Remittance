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
        Me.NCRHostServiceSPI = New System.ServiceProcess.ServiceProcessInstaller
        Me.NCRHostServiceSI = New System.ServiceProcess.ServiceInstaller
        '
        'NCRHostServiceSPI
        '
        Me.NCRHostServiceSPI.Account = System.ServiceProcess.ServiceAccount.LocalService
        Me.NCRHostServiceSPI.Password = Nothing
        Me.NCRHostServiceSPI.Username = Nothing
        '
        'NCRHostServiceSI
        '
        Me.NCRHostServiceSI.ServiceName = "NCRHostService"
        '
        'ProjectInstaller
        '
        Me.Installers.AddRange(New System.Configuration.Install.Installer() {Me.NCRHostServiceSPI, Me.NCRHostServiceSI})

    End Sub
    Friend WithEvents NCRHostServiceSPI As System.ServiceProcess.ServiceProcessInstaller
    Friend WithEvents NCRHostServiceSI As System.ServiceProcess.ServiceInstaller

End Class
