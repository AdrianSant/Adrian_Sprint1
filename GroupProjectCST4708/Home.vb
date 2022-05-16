Public Class HomeForm
    Private Sub HomeForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Using helper class
        Dim helper As New Utilities
        Dim id As String = helper.getID
        Dim name As String = helper.getFullName

        'set the user id and full name into their textboxes
        TextBox1.Text = id
        TextBox2.Text = name

        'Using helper class
        Dim table As String = "cpu_info"

        'Add data to panel 1
        Dim cpu1Name = helper.GetItemName(table, 0, 0)
        Dim cpu1Price = helper.GetItemPrice(table, 0, 0)
        Dim cpu1Manu = helper.GetItemManufacturer(table, 0, 0)
        Dim cpu1Core = helper.GetItemCore(table, 0, 0)
        Dim cpu1Speed = helper.GetItemSpeed(table, 0, 0)
        Dim cpu1Socket = helper.GetItemSocket(table, 0, 0)
        Dim cpu1Desc = helper.GetItemDescription(table, 0, 0)
        Label1.Text = "Name: " + cpu1Name
        Label2.Text = "Price: $" + cpu1Price.ToString
        Label3.Text = "Manufacturer: " + cpu1Manu
        Label4.Text = "Core Count: " + cpu1Core.ToString
        Label5.Text = "Clock Speed: " + cpu1Speed.ToString
        Label6.Text = "Socket Type: " + cpu1Socket
        Label7.Text = "Item Description: " + cpu1Desc

        'Add data to panel 2
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Dim cartPage As Cart
        cartPage = New Cart
        cartPage.Show()
    End Sub
End Class