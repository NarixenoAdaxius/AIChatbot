Imports RestSharp
Imports Newtonsoft.Json.Linq

Public Class AIModel
    Public Shared Function GetAIResponse(prompt As String) As String
        Try
            ' Create REST client
            Dim client As New RestClient("http://localhost:11434/api/generate")
            Dim request As New RestRequest(Method.Post)

            ' Add JSON request body
            request.AddJsonBody(New With {
                .model = "llama3",
                .prompt = prompt,
                .stream = False
            })

            ' Execute request and get response
            Dim response As RestResponse = client.Execute(request)

            ' Log response to debug
            Console.WriteLine("Response Status: " & response.StatusCode)
            Console.WriteLine("Response Content: " & response.Content)

            ' Check if response is valid
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
