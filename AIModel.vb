Imports System.Net.Http
Imports System.Text
Imports System.Threading.Tasks
Imports Newtonsoft.Json.Linq

Public Class AIModel
    Private Shared ReadOnly client As New HttpClient()

    Public Shared Async Function GetAIResponse(prompt As String) As Task(Of String)
        Try
            ' OpenAI API URL
            Dim apiUrl As String = "https://api.openai.com/v1/chat/completions"
            Dim apiKey As String = "" ' Replace with your actual API key

            ' Create request body
            Dim jsonBody As String = "{""model"":""gpt-3.5-turbo"", ""messages"":[{""role"":""user"", ""content"":""" & prompt & """}], ""temperature"":0.7}"
            Dim content As New StringContent(jsonBody, Encoding.UTF8, "application/json")

            ' Set OpenAI API Headers
            client.DefaultRequestHeaders.Clear() ' Clear any previous headers
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " & apiKey)

            ' Send request
            Dim response As HttpResponseMessage = Await client.PostAsync(apiUrl, content)

            ' Read response
            Dim responseData As String = Await response.Content.ReadAsStringAsync()
            Console.WriteLine("Response Content: " & responseData) ' Debugging

            ' Check if response is successful
            If response.IsSuccessStatusCode Then
                Dim jsonResponse As JObject = JObject.Parse(responseData)
                Return jsonResponse("choices")(0)("message")("content").ToString()
            Else
                Return "API Error: " & response.StatusCode
            End If
        Catch ex As Exception
            Return "Error: " & ex.Message
        End Try
    End Function
End Class
