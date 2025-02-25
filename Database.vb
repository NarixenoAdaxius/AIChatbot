Imports MongoDB.Bson
Imports MongoDB.Driver

Public Class Database
    Private Shared ReadOnly ConnectionString As String = "mongodb+srv://narixeno:Ia9P7GLIY7P9BctH@backendtest.i5ewjcx.mongodb.net/?retryWrites=true&w=majority&appName=backendtest"

    Public Shared ReadOnly Client As New MongoClient(ConnectionString)
    Public Shared ReadOnly Database As IMongoDatabase = Client.GetDatabase("AIChatbotDB")
    Public Shared ReadOnly Users As IMongoCollection(Of BsonDocument) = Database.GetCollection(Of BsonDocument)("Users")
End Class
