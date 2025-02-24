Imports RestSharp
Imports Newtonsoft.Json.Linq

Public Class AIModel
    Public Shared Function GetAIResponse(prompt As String) As String
        Try
            Dim client As New RestClient("http://localhost:11434/api/generate")
            Dim request As New RestRequest(Method.Post)

            request.AddJsonBody(New With {
                .model = "llama3",
                .prompt = prompt,
                .stream = False
            })

            Dim response As RestResponse = client.Execute(request)

            ' Check if the response is valid
            If response.StatusCode = System.Net.HttpStatusCode.OK Then
                Dim jsonResponse As JObject = JObject.Parse(response.Content)
                Return jsonResponse("response").ToString()
            Else
                Return "Error: " & response.StatusDescription
            End If
        Catch ex As Exception
            Return "Error: " & ex.Message
        End Try
    End Function
End Class
