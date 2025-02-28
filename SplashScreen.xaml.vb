Imports System.Windows
Imports System.Windows.Threading

Namespace AIChatbot
    Partial Public Class SplashScreen
        Inherits Window

        Private ReadOnly splashTimer As DispatcherTimer

        Public Sub New()
            InitializeComponent()

            ' Set up a 3-second timer before switching to LoginWindow
            splashTimer = New DispatcherTimer()
            splashTimer.Interval = TimeSpan.FromSeconds(3)
            AddHandler splashTimer.Tick, AddressOf SplashTimer_Tick
            splashTimer.Start()
        End Sub

        Private Sub SplashTimer_Tick(sender As Object, e As EventArgs)
            splashTimer.Stop()

            ' Open the LoginWindow
            Dim login As New LoginWindow()
            login.Show()

            ' Close the SplashScreen
            Me.Close()
        End Sub
    End Class
End Namespace
