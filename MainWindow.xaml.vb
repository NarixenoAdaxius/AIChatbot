Imports System.Threading.Tasks
Imports System.Windows

Namespace AIChatbot
    Partial Public Class MainWindow
        Inherits Window

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Async Sub btnSend_Click(sender As Object, e As RoutedEventArgs)
            Dim userMessage As String = txtUserInput.Text
            If String.IsNullOrWhiteSpace(userMessage) Then Exit Sub

            lstChat.Items.Add("You: " & userMessage)
            txtUserInput.Clear()

            ' Get AI Response Asynchronously
            Dim botResponse As String = Await AIModel.GetAIResponse(userMessage)
            lstChat.Items.Add("Bot: " & botResponse)
        End Sub
    End Class
End Namespace
