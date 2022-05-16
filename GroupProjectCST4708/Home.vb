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
        Dim table1 As String = "cpu_info"
        Dim table2 As String = "gpu_info"
        Dim table3 As String = "ram_info"

        'Add data to panel 1
        Dim cpuName = helper.GetItemName(table1, 0, 0)
        Dim cpuPrice = helper.GetItemPrice(table1, 0, 0)
        Dim cpuManu = helper.GetItemManufacturer(table1, 0, 0)
        Dim cpuCore = helper.GetItemCore(table1, 0, 0)
        Dim cpuSpeed = helper.GetItemSpeed(table1, 0, 0)
        Dim cpuSocket = helper.GetItemSocket(table1, 0, 0)
        Dim cpudesc = helper.GetItemDescription(table1, 0, 0)
        Label1.Text = "Name: " + cpuName
        Label2.Text = "Price: $" + cpuPrice.ToString
        Label3.Text = "Manufacturer: " + cpuManu
        Label4.Text = "Core Count: " + cpuCore.ToString
        Label5.Text = "Clock Speed: " + cpuSpeed.ToString
        Label6.Text = "Socket Type: " + cpuSocket
        Label7.Text = "Item Description: " + cpudesc

        'Add data to panel 2
        Dim gpuName = helper.GetItemName(table2, 0, 0)
        Dim gpuPrice = helper.GetItemPrice(table2, 0, 0)
        Dim gpuManu = helper.GetItemManufacturer(table2, 0, 0)
        Dim gpuMem = helper.GetItemVideoMemory(table2, 0, 0)
        Dim gpuInter = helper.GetItemInterface(table2, 0, 0)
        Dim gpuDesc = helper.GetItemDescription(table2, 0, 0)
        Label14.Text = "Name: " + gpuName
        Label13.Text = "Price: $" + gpuPrice.ToString
        Label12.Text = "Manufacturer: " + gpuManu
        Label11.Text = "Video Memory Capacity: " + gpuMem
        Label10.Text = "Interface Type: " + gpuInter
        Label9.Text = "Item Description: " + gpuDesc

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Dim cartPage As Cart
        cartPage = New Cart
        cartPage.Show()
    End Sub
End Class