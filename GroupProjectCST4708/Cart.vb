Public Class Cart
    Private Sub Cart_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Using helper class
        Dim helper As New Utilities
        Dim id As String = helper.getID
        Dim name As String = helper.getFullName
        Dim card As String = helper.getCardNum

        'set the user id and full name into their textboxes
        TextBox1.Text = name
        TextBox2.Text = id
        TextBox3.Text = card

    End Sub

End Class