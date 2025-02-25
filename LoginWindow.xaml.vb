Imports System.Windows
Imports AIChatbot

Namespace AIChatbot
    Partial Public Class LoginWindow
        Inherits Window

        Private Sub btnLogin_Click(sender As Object, e As RoutedEventArgs)
            Dim username As String = txtUsername.Text.Trim()
            Dim password As String = txtPassword.Password.Trim()

            If String.IsNullOrWhiteSpace(username) OrElse String.IsNullOrWhiteSpace(password) Then
                MessageBox.Show("Please enter both username and password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Warning)
                Return
            End If

            If AuthService.AuthenticateUser(username, password) Then
                MessageBox.Show("Login successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information)

                ' Open MainWindow and close LoginWindow
                Dim mainWindow As New MainWindow()
                mainWindow.Show()
                Me.Close()
            Else
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error)
            End If
        End Sub

        Private Sub btnRegister_Click(sender As Object, e As RoutedEventArgs)
            Dim username As String = txtUsername.Text.Trim()
            Dim password As String = txtPassword.Password.Trim()

            If String.IsNullOrWhiteSpace(username) OrElse String.IsNullOrWhiteSpace(password) Then
                MessageBox.Show("Please enter both username and password.", "Registration Failed", MessageBoxButton.OK, MessageBoxImage.Warning)
                Return
            End If

            If AuthService.RegisterUser(username, password) Then
                MessageBox.Show("Registration successful! You can now log in.", "Success", MessageBoxButton.OK, MessageBoxImage.Information)
            Else
                MessageBox.Show("Username already taken. Choose another one.", "Registration Failed", MessageBoxButton.OK, MessageBoxImage.Error)
            End If
        End Sub
    End Class
End Namespace
