Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        MessageBox.Show("Login Successful")
        Dim homePage As Home
        homePage = New Home
        homePage.Show()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim registerPage As Register
        registerPage = New Register
        registerPage.Show()
    End Sub
End Class
