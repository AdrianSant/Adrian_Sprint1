Public Class HomeForm
    Private Sub HomeForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Using helper class
        Dim helper As New Utilities
        Dim id As String = helper.getID
        Dim name As String = helper.GetFullName

        'set the user id and full name into their textboxes
        Label102.Text = id
        Label104.Text = name

        'Using helper class
        Dim table1 As String = "cpu_info"
        Dim table2 As String = "gpu_info"
        Dim table3 As String = "ram_info"

        'Add data to panel 1
        Dim cpuName = helper.GetItemName(table1, 0, 0)
        Dim cpuPrice = helper.GetItemPrice(table1, 0, 0)
        Dim cpuManu = helper.GetItemManufacturer(table1, 0, 0)
        Dim cpuCore = helper.GetItemCore(table1, 0, 0)
        Dim cpuSpeed = helper.GetItemCPUSpeed(table1, 0, 0)
        Dim cpuSocket = helper.GetItemSocket(table1, 0, 0)
        Dim cpudesc = helper.GetItemDescription(table1, 0, 0)
        Label1.Text = "Name: " + cpuName
        Label2.Text = "Price: $" + cpuPrice.ToString
        Label3.Text = "Manufacturer: " + cpuManu
        Label4.Text = "Core Count: " + cpuCore.ToString
        Label5.Text = "Clock Speed: " + cpuSpeed.ToString
        Label6.Text = "Socket Type: " + cpuSocket
        Label7.Text = cpudesc

        'Add data to panel 2
        Dim gpuName = helper.GetItemName(table2, 0, 0)
        Dim gpuPrice = helper.GetItemPrice(table2, 0, 0)
        Dim gpuManu = helper.GetItemManufacturer(table2, 0, 0)
        Dim gpuMem = helper.GetItemVideoMemory(table2, 0, 0)
        Dim gpuInter = helper.GetItemInterface(table2, 0, 0)
        Dim gpuDesc = helper.GetItemDescription(table2, 0, 0)
        Label8.Text = "Name: " + gpuName
        Label9.Text = "Price: $" + gpuPrice.ToString
        Label10.Text = "Manufacturer: " + gpuManu
        Label11.Text = "Video Memory Capacity: " + gpuMem
        Label12.Text = "Interface Type: " + gpuInter
        Label13.Text = gpuDesc

        'Add data to panel 3
        Dim ramName = helper.GetItemName(table3, 0, 0)
        Dim ramPrice = helper.GetItemPrice(table3, 0, 0)
        Dim ramManu = helper.GetItemManufacturer(table3, 0, 0)
        Dim ramMem = helper.GetItemMemory(table3, 0, 0)
        Dim ramSpeed = helper.GetItemSpeed(table3, 0, 0)
        Dim ramDesc = helper.GetItemDescription(table3, 0, 0)
        Label14.Text = "Name: " + ramName
        Label15.Text = "Price: $" + ramPrice.ToString
        Label16.Text = "Manufacturer: " + ramManu
        Label17.Text = "Memory Capacity: " + ramMem
        Label18.Text = "Clock Speed: " + ramSpeed
        Label19.Text = ramDesc

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim cartPage As Cart
        cartPage = New Cart
        cartPage.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'Using helper class
        Dim helper As New Utilities
        Dim table1 As String = "cpu_info"
        Dim cpuName = helper.GetItemName(table1, 0, 0)
        Dim cpuPrice = helper.GetItemPrice(table1, 0, 0)
        Dim result As Boolean = helper.AddToCart(table1, cpuName, cpuPrice.ToString)

        If result = False Then
            MessageBox.Show("Item Not Added to Cart")
        Else
            MessageBox.Show("Item Added to Cart")
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'Using helper class
        Dim helper As New Utilities
        Dim table1 As String = "gpu_info"
        Dim gpuName = helper.GetItemName(table1, 0, 0)
        Dim gpuPrice = helper.GetItemPrice(table1, 0, 0)
        Dim result As Boolean = helper.AddToCart(table1, gpuName, gpuPrice.ToString)

        If result = False Then
            MessageBox.Show("Item Not Added to Cart")
        Else
            MessageBox.Show("Item Added to Cart")
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'Using helper class
        Dim helper As New Utilities
        Dim table1 As String = "ram_info"
        Dim ramName = helper.GetItemName(table1, 0, 0)
        Dim ramPrice = helper.GetItemPrice(table1, 0, 0)
        Dim result As Boolean = helper.AddToCart(table1, ramName, ramPrice.ToString)

        If result = False Then
            MessageBox.Show("Item Not Added to Cart")
        Else
            MessageBox.Show("Item Added to Cart")
        End If
    End Sub
End Class