Public Class Admin
    Private Sub Admin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Using helper class
        Dim helper As New Utilities
        Dim id As String = helper.getID
        Dim name As String = helper.getFullName

        'set the user id and full name into their textboxes
        TextBox1.Text = id
        TextBox2.Text = name

        'Bind data from Orders to datagridview
        helper.BindToDGV(DataGridView1, "Transactions")
    End Sub
End Class