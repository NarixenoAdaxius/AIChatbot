Imports System.Collections.ObjectModel
Imports System.Threading.Tasks
Imports System.Windows
Imports System.ComponentModel

Namespace AIChatbot
    ''' <summary>
    ''' Message class to represent chat messages
    ''' </summary>
    Public Class ChatMessage
        Implements INotifyPropertyChanged

        Private _content As String
        Private _isUserMessage As Boolean

        Public Property Content As String
            Get
                Return _content
            End Get
            Set(value As String)
                _content = value
                OnPropertyChanged("Content")
            End Set
        End Property

        Public Property IsUserMessage As Boolean
            Get
                Return _isUserMessage
            End Get
            Set(value As Boolean)
                _isUserMessage = value
                OnPropertyChanged("IsUserMessage")
            End Set
        End Property

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Protected Sub OnPropertyChanged(propertyName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub
    End Class

    Partial Public Class MainWindow
        Inherits Window

        Private _messages As ObservableCollection(Of ChatMessage)

        Public Sub New()
            InitializeComponent()

            ' Initialize message collection
            _messages = New ObservableCollection(Of ChatMessage)()
            lstChat.ItemsSource = _messages

            ' Register converter in resources if not done in XAML
            If Not Resources.Contains("MessageBackgroundConverter") Then
                Resources.Add("MessageBackgroundConverter", New MessageBackgroundConverter())
            End If

            ' Add welcome message
            AddBotMessage("Hello! How can I assist you today?")
        End Sub

        Private Async Sub btnSend_Click(sender As Object, e As RoutedEventArgs)
            Dim userMessage As String = txtUserInput.Text.Trim()

            If String.IsNullOrWhiteSpace(userMessage) Then
                Return
            End If

            ' Add user message to chat
            AddUserMessage(userMessage)

            ' Clear input
            txtUserInput.Clear()

            ' Set focus back to input box
            txtUserInput.Focus()

            ' Get AI Response Asynchronously
            Await GetBotResponse(userMessage)
        End Sub

        Private Sub AddUserMessage(message As String)
            Dim chatMessage As New ChatMessage With {
                .Content = message,
                .IsUserMessage = True
            }

            _messages.Add(chatMessage)
            ScrollToBottom()
        End Sub

        Private Sub AddBotMessage(message As String)
            Dim chatMessage As New ChatMessage With {
                .Content = message,
                .IsUserMessage = False
            }

            _messages.Add(chatMessage)
            ScrollToBottom()
        End Sub

        Private Async Function GetBotResponse(userMessage As String) As Task
            ' Simulate typing indicator or loading state
            Dim typingMessage As New ChatMessage With {
                .Content = "Typing...",
                .IsUserMessage = False
            }

            _messages.Add(typingMessage)
            ScrollToBottom()

            ' Get AI Response
            Dim botResponse As String = Await AIModel.GetAIResponse(userMessage)

            ' Remove typing indicator
            _messages.Remove(typingMessage)

            ' Add actual response
            AddBotMessage(botResponse)
        End Function

        Private Sub ScrollToBottom()
            ' Find the ScrollViewer that contains the lstChat
            Dim scrollViewer = TryCast(lstChat.Parent, System.Windows.Controls.ScrollViewer)

            If scrollViewer IsNot Nothing Then
                ' Use dispatcher to ensure UI update happens after layout
                Dispatcher.InvokeAsync(Sub()
                                           scrollViewer.ScrollToEnd()
                                       End Sub, System.Windows.Threading.DispatcherPriority.Background)
            End If
        End Sub

        ' Handle Enter key press in text box
        Private Sub txtUserInput_KeyDown(sender As Object, e As System.Windows.Input.KeyEventArgs) Handles txtUserInput.KeyDown
            If e.Key = System.Windows.Input.Key.Enter Then
                btnSend_Click(sender, New RoutedEventArgs())
                e.Handled = True
            End If
        End Sub

        ' Logout
        Private Sub Logout_Click(sender As Object, e As RoutedEventArgs)
            Dim splashScreen As New SplashScreen()
            splashScreen.Show()
            Me.Close()
        End Sub

    End Class

    ''' <summary>
    ''' Converter for message background colors
    ''' </summary>
    Public Class MessageBackgroundConverter
        Implements System.Windows.Data.IValueConverter

        Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
            Dim isUserMessage As Boolean = DirectCast(value, Boolean)
            Return If(isUserMessage,
                      New System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(103, 58, 183)),
                      New System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(66, 66, 66)))
        End Function

        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace