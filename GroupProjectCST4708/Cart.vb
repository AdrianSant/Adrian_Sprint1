Imports System.Data.SqlClient

Public Class Cart
    Private Sub Cart_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Using helper class
        Dim helper As New Utilities
        Dim numOfItems As Integer = helper.GetColumnCount()
        If numOfItems.Equals(0) Then
            'OnLoad the datagridview is set to nothing
            DataGridView1.DataSource = Nothing
        Else
            Dim id As String = helper.GetID
            Dim name As String = helper.GetFullName
            Dim card As String = helper.GetCardNum

            'set the user id and full name into their textboxes
            TextBox1.Text = name
            TextBox2.Text = id
            TextBox3.Text = card

            'Bind data from Orders to datagridview
            helper.BindToDGV(DataGridView1, "Orders")

            'Get total price before shipping and tax
            Dim result = helper.GetTotalPrice()
            Dim totalBeforeExtras As Double
            If result.Equals(0) Then
                totalBeforeExtras = 0.00
                TextBox4.Text = "$0.00"
                TextBox6.Text = "$0.00"
            Else
                totalBeforeExtras = result
            End If

            'Calculate Shipping Cost and add the data to textbox 5
            For i As Integer = 0 To numOfItems
                If numOfItems = 0 Then
                    TextBox5.Text = "$0.00"
                ElseIf numOfItems > 0 Then
                    Dim price As Double = (10 * i)
                    TextBox5.Text = "$" + Format(price, "#.00")
                End If
            Next
            Dim shippingCost As Double = TextBox5.Text

            'Calculate tax and add the data to textbox 4
            Dim totalAfterExtras As Double = (totalBeforeExtras * 0.08875)
            TextBox4.Text = "$" + Format(totalAfterExtras, "#.00")

            'Calculate total price after shipping and tax
            Dim finalTotal As Double = totalBeforeExtras + totalAfterExtras + shippingCost
            TextBox6.Text = "$" + Format(finalTotal, "#.00")
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Using helper class to delete the orders table and clear the datagridview after resetting the seed
        Dim helper As New Utilities
        Dim result = helper.DeleteOrdersTable()
        If result = False Then
            MessageBox.Show("Please add items to the cart before clearing")
        Else
            Dim reset As Boolean = helper.ResetSeed()
            If reset = True Then
                DataGridView1.DataSource = Nothing
                TextBox4.Text = "$0"
                TextBox5.Text = "$0"
                TextBox6.Text = "$0"
            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'Using helper class to send data to the transactions table, delete the orders table, and clear the datagridview after resetting the seed
        Dim helper As New Utilities

        If DataGridView1.Rows.Count > 0 Then
            Dim custID As Integer = TextBox2.Text
            Dim custName As String = TextBox1.Text
            Dim custCard As String = TextBox3.Text
            Dim custItems As Integer = helper.GetColumnCount()
            Dim custTotal As Double = TextBox6.Text
            helper.SendDataToTransactions(custID, custName, custCard, custItems, custTotal)
            Dim result = helper.DeleteOrdersTable()
            If result = False Then
                MessageBox.Show("Please add items to the cart before clearing")
            Else
                Dim reset As Boolean = helper.ResetSeed()
                If reset = True Then
                    DataGridView1.DataSource = Nothing
                    TextBox4.Text = "$0"
                    TextBox5.Text = "$0"
                    TextBox6.Text = "$0"
                End If
            End If
        Else
            MessageBox.Show("Please add items to the cart before checking out")
        End If
    End Sub
End Class