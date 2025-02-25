Imports System.Globalization
Imports System.Windows.Data
Imports System.Windows.Media

Public Class MessageBackgroundConverter
    Implements IValueConverter

    Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
        Dim isUserMessage As Boolean = CType(value, Boolean)
        Return If(isUserMessage, New SolidColorBrush(Colors.DeepSkyBlue), New SolidColorBrush(Colors.Gray))
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
        Throw New NotImplementedException()
    End Function
End Class
