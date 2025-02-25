Imports MongoDB.Bson
Imports MongoDB.Driver
Imports System.Security.Cryptography
Imports System.Text
Imports System.Windows

Public Class AuthService
    ' Collection reference
    Private Shared ReadOnly UsersCollection As IMongoCollection(Of BsonDocument) = Database.Users

    ' Secure Hashing with SHA-256
    Private Shared Function HashPassword(password As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            Dim bytes As Byte() = Encoding.UTF8.GetBytes(password)
            Dim hash As Byte() = sha256.ComputeHash(bytes)
            Return Convert.ToBase64String(hash)
        End Using
    End Function

    ' Register User
    Public Shared Function RegisterUser(username As String, password As String) As Boolean

        Try
            ' Check if user already exists
            Dim filter = Builders(Of BsonDocument).Filter.Eq(Of String)("Username", username)
            Dim existingUser = UsersCollection.Find(filter).FirstOrDefault()

            If existingUser IsNot Nothing Then
                MessageBox.Show("Username already taken. Choose another one.", "Registration Failed", MessageBoxButton.OK, MessageBoxImage.Warning)
                Return False
            End If

            ' Hash password before storing
            Dim hashedPassword As String = HashPassword(password)

            Dim newUser As New BsonDocument From {
                {"Username", username},
                {"PasswordHash", hashedPassword}
            }

            UsersCollection.InsertOne(newUser)
            MessageBox.Show("Registration successful! You can now log in.", "Success", MessageBoxButton.OK, MessageBoxImage.Information)
            Return True

        Catch ex As MongoAuthenticationException
            MessageBox.Show("MongoDB Authentication Failed: " & ex.Message, "Database Error", MessageBoxButton.OK, MessageBoxImage.Error)
            Return False
        Catch ex As Exception
            MessageBox.Show("Database Error: " & ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error)
            Return False
        End Try
    End Function

    ' Authenticate Login
    Public Shared Function AuthenticateUser(username As String, password As String) As Boolean

        Try
            Dim filter = Builders(Of BsonDocument).Filter.Eq(Of String)("Username", username)
            Dim userDoc = UsersCollection.Find(filter).FirstOrDefault()

            If userDoc IsNot Nothing Then
                Dim storedHash = userDoc("PasswordHash").AsString
                Return storedHash = HashPassword(password) ' Compare hashed passwords
            End If

            Return False
        Catch ex As Exception
            MessageBox.Show("Database Error: " & ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error)
            Return False
        End Try
    End Function
End Class
