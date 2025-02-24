Imports System.Windows
Imports System.Threading.Tasks

Namespace AIChatbot ' Ensure this matches the project namespace
    Partial Public Class MainWindow ' Add Partial and Public
        Inherits Window

        Private Sub btnSend_Click(sender As Object, e As RoutedEventArgs)
            Dim userMessage As String = txtUserInput.Text
            If String.IsNullOrWhiteSpace(userMessage) Then Exit Sub

            lstChat.Items.Add("You: " & userMessage)
            txtUserInput.Clear()

            ' Get AI Response Asynchronously
            Task.Run(Sub()
                         Dim botResponse As String = AIModel.GetAIResponse(userMessage)
                         Me.Dispatcher.Invoke(Sub() lstChat.Items.Add("Bot: " & botResponse))
                     End Sub)
        End Sub
    End Class
End Namespace
