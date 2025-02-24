Imports System.Net.Http
Imports Newtonsoft.Json.Linq
Imports System.Text

Public Class AIModel
    Private Shared ReadOnly client As New HttpClient()

    Public Shared Async Function GetAIResponse(prompt As String) As Task(Of String)
        Try
            Dim apiUrl As String = "http://localhost:11434/api/generate"
            Dim jsonBody As String = "{""model"":""llama3"",""prompt"":""" & prompt & """,""stream"":false}"
            Dim content As New StringContent(jsonBody, Encoding.UTF8, "application/json")

            ' Send request
            Dim response As HttpResponseMessage = Await client.PostAsync(apiUrl, content)

            ' Log response
            Console.WriteLine("Response Status: " & response.StatusCode)
            Dim responseData As String = Await response.Content.ReadAsStringAsync()
            Console.WriteLine("Response Content: " & responseData)

            ' Check if response is successful
            If response.IsSuccessStatusCode Then
                Dim jsonResponse As JObject = JObject.Parse(responseData)
                Return jsonResponse("response").ToString()
            Else
                Return "Error: " & response.StatusCode
            End If
        Catch ex As Exception
            Return "Error: " & ex.Message
        End Try
    End Function
End Class
