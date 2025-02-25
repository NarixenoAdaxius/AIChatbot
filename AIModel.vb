Imports System.Net.Http
Imports System.Text
Imports Newtonsoft.Json.Linq

Public Class AIModel
    Private Shared ReadOnly client As New HttpClient()

    ' Set your OpenAI API Key
    Private Const apiKey As String = "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"

    ' Function to get response from OpenAI API
    Public Shared Async Function GetAIResponse(prompt As String) As Task(Of String)
        Try
            Dim apiUrl As String = "https://api.openai.com/v1/chat/completions"
            Dim jsonBody As String = "{""model"":""gpt-4"",""messages"":[{""role"":""system"",""content"":""You are a helpful assistant.""},
                {""role"":""user"",""content"":""" & prompt & """}]}"

            Dim content As New StringContent(jsonBody, Encoding.UTF8, "application/json")
            content.Headers.Add("Authorization", "Bearer " & apiKey)

            ' Send request
            Dim response As HttpResponseMessage = Await client.PostAsync(apiUrl, content)
            Dim responseData As String = Await response.Content.ReadAsStringAsync()

            ' Log response
            Console.WriteLine("Response Status: " & response.StatusCode)
            Console.WriteLine("Response Content: " & responseData)

            ' Parse JSON response
            If response.IsSuccessStatusCode Then
                Dim jsonResponse As JObject = JObject.Parse(responseData)
                Return jsonResponse("choices")(0)("message")("content").ToString()
            Else
                Return "API Error: " & response.StatusCode & " - " & responseData
            End If
        Catch ex As Exception
            Return "Error: " & ex.Message
        End Try
    End Function
End Class
