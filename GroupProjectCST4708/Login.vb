Public Class Login
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Instance Variables
        Dim email, password As String

        'Gets the email and password from user input
        email = TextBox1.Text.Trim()
        password = TextBox2.Text.Trim()

        'Using helper class
        Dim helper As New Utilities
        Dim result As String = helper.authorizeLogin(email, password)

        'Statement to check whether the email and password match data from the customers table
        If result = "Unsuccessful" Then
            MessageBox.Show("Login Unsuccessful")
        ElseIf result = "Admin" Then
            MessageBox.Show("Logged in As: Admin")
            Dim adminPage As Admin
            adminPage = New Admin
            adminPage.Show()
        Else
            MessageBox.Show("Logged in As: Customer")
            Dim homePage As HomeForm
            homePage = New HomeForm
            homePage.Show()
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim registerPage As Register
        registerPage = New Register
        registerPage.Show()
    End Sub
End Class
