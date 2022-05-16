Public Class HomeForm
    Private Sub HomeForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Using helper class
        Dim helper As New Utilities
        Dim id As String = helper.getID
        Dim name As String = helper.getFullName

        'set the user id and full name into their textboxes
        TextBox1.Text = id
        TextBox2.Text = name
    End Sub

    Private Sub AllCategoriesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AllCategoriesToolStripMenuItem.Click

    End Sub

    Private Sub Button69_Click(sender As Object, e As EventArgs) Handles Button69.Click
        Dim cartPage As Cart
        cartPage = New Cart
        cartPage.Show()
    End Sub
End Class