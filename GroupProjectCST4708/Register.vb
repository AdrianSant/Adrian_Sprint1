Public Class Register
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Store all values from user in separate strings
        Dim first As String = TextBox1.Text
        Dim last As String = TextBox2.Text
        Dim email As String = TextBox3.Text
        Dim pass As String = TextBox4.Text
        Dim card As String = TextBox5.Text

        'Using helper class
        Dim helper As New Utilities
        Dim result As Boolean = helper.registerCustomer(first, last, email, pass, card)

        'Show the result (success or not) and redirect to login page for the user to use their new credentials
        If result = False Then
            MessageBox.Show("Registration Unsuccessful")
        Else
            MessageBox.Show("Registration Successful")
        End If
    End Sub
End Class