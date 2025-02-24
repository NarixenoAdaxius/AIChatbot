Imports MongoDB.Bson
Imports MongoDB.Driver

Public Class Authentication
    Public Shared Function RegisterUser(username As String, password As String) As Boolean
        Dim filter = Builders(Of BsonDocument).Filter.Eq(Of String)("username", username)
        Dim existingUser = Database.Users.Find(filter).FirstOrDefault()

        If existingUser IsNot Nothing Then
            Return False ' Username already exists
        End If

        Dim newUser As New BsonDocument From {
            {"username", username},
            {"password", password} ' Hash in production
        }

        Database.Users.InsertOne(newUser)
        Return True
    End Function

    Public Shared Function LoginUser(username As String, password As String) As Boolean
        Dim filter = Builders(Of BsonDocument).Filter.Eq(Of String)("username", username)
        Dim user = Database.Users.Find(filter).FirstOrDefault()

        If user IsNot Nothing AndAlso user.GetValue("password").AsString = password Then
            Return True ' Login successful
        End If

        Return False ' Invalid credentials
    End Function
End Class
