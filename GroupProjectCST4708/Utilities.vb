Imports System.Data.SqlClient

Public Class Utilities
    ReadOnly connectionString As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\CST4708_Team4\CST4708_GroupProject.mdf';Integrated Security=True;Connect Timeout=30"
    Dim myConn As SqlConnection 
    Dim loginCmd As SqlCommand
    Dim registerCmd As SqlCommand
    Dim getIDCmd As SqlCommand
    Dim getNameCmd As SqlCommand
    Dim getCardCmd As SqlCommand
    Dim getDataCmd As SqlCommand
    Dim getCountCmd As SqlCommand
    Dim getTotalCmd As SqlCommand
    Dim resetCmd As SqlCommand
    Dim sendCmd As SqlCommand
    Dim addOrdersCmd As SqlCommand
    Dim deleteOrdersCmd As SqlCommand
    Dim myAdapter As New SqlDataAdapter
    Dim myReader As SqlDataReader
    Dim dataSet As New DataSet
    Dim mydt As DataTable

    Public Function AuthorizeLogin(email As String, pass As String) As String
        'Connect to the db, select the email and password, compare it to the input from user, return true if matches, false if not
        myConn = New SqlConnection()
        myConn.ConnectionString = connectionString
        myConn.Open()
        'MessageBox.Show("Open Successful")
        loginCmd = New SqlCommand()
        loginCmd.CommandText = "SELECT email, password FROM Customers WHERE email = @email and password= @pass;"
        loginCmd.Parameters.Add("@email", SqlDbType.VarChar, 50, "email")
        loginCmd.Parameters.Add("@pass", SqlDbType.VarChar, 50, "pass")
        loginCmd.Parameters("@email").Value = email
        loginCmd.Parameters("@pass").Value = pass
        loginCmd.Connection = myConn
        myAdapter.SelectCommand = loginCmd
        myReader = loginCmd.ExecuteReader()

        While myReader.Read
            If myReader.HasRows = True Then
                'MessageBox.Show("Returned: True")
                'If an admin logged in
                If email.Equals("adrian") And pass.Equals("password1") Then
                    Return "Admin"
                End If

                'If a customer logged in
                Return "Customer"
            End If
        End While
        'MessageBox.Show("Returned: False")
        Return "Unsuccessful"

        myConn.Close()
    End Function

    Public Function RegisterCustomer(first As String, last As String, email As String, pass As String, card As String)
        'Connect to the db, add all values passed to this function into the db, return data
        myConn = New SqlConnection()
        mydt = New DataTable()
        myConn.ConnectionString = connectionString
        myConn.Open()
        'MessageBox.Show("Open Successful")
        registerCmd = New SqlCommand()
        registerCmd.CommandText = "INSERT INTO Customers(firstName, lastName, email, password, cardInfo) Values (@first, @last, @email, @pass, @card);"
        registerCmd.Parameters.Add("@first", SqlDbType.VarChar, 50, "firstName")
        registerCmd.Parameters.Add("@last", SqlDbType.VarChar, 50, "lastName")
        registerCmd.Parameters.Add("@email", SqlDbType.VarChar, 50, "email")
        registerCmd.Parameters.Add("@pass", SqlDbType.VarChar, 50, "password")
        registerCmd.Parameters.Add("@card", SqlDbType.VarChar, 50, "cardInfo")
        registerCmd.Parameters("@first").Value = first
        registerCmd.Parameters("@last").Value = last
        registerCmd.Parameters("@email").Value = email
        registerCmd.Parameters("@pass").Value = pass
        registerCmd.Parameters("@card").Value = card
        registerCmd.Connection = myConn
        myAdapter.UpdateCommand = registerCmd

        'if registration is successful, returns true. if not, returns false
        Dim result As Boolean
        If (registerCmd.ExecuteNonQuery().Equals(1)) Then
            myAdapter.Update(mydt)
            result = True
        Else
            result = False
        End If

        Return result

        myConn.Close()
    End Function

    Public Function GetID()
        'Connect to the db, select the user id# matching their email address and password
        myConn = New SqlConnection()
        myConn.ConnectionString = connectionString
        myConn.Open()
        'MessageBox.Show("Open Successful")
        getIDCmd = New SqlCommand()
        getIDCmd.CommandText = "SELECT id FROM Customers WHERE email = @email AND password = @pass;"
        getIDCmd.Parameters.Add("@email", SqlDbType.VarChar, 50, "email")
        getIDCmd.Parameters("@email").Value = Login.TextBox1.Text
        getIDCmd.Parameters.Add("@pass", SqlDbType.VarChar, 50, "password")
        getIDCmd.Parameters("@pass").Value = Login.TextBox2.Text
        getIDCmd.Connection = myConn
        myAdapter.SelectCommand = getIDCmd
        myReader = getIDCmd.ExecuteReader()

        While myReader.Read
            Dim userID As Integer = myReader.GetValue(0)
            Return userID
        End While

        'Default value
        Return "GetID() Error"
        myConn.Close()
    End Function

    Public Function GetFullName() As String
        'Connect to the db, select the user first and last name matching their email address and password
        myConn = New SqlConnection()
        myConn.ConnectionString = connectionString
        myConn.Open()
        'MessageBox.Show("Open Successful")
        getNameCmd = New SqlCommand()
        getNameCmd.CommandText = "SELECT firstName, lastName FROM Customers WHERE email = @email AND password = @pass;"
        getNameCmd.Parameters.Add("@email", SqlDbType.VarChar, 50, "email")
        getNameCmd.Parameters("@email").Value = Login.TextBox1.Text
        getNameCmd.Parameters.Add("@pass", SqlDbType.VarChar, 50, "password")
        getNameCmd.Parameters("@pass").Value = Login.TextBox2.Text
        getNameCmd.Connection = myConn
        myAdapter.SelectCommand = getNameCmd
        myReader = getNameCmd.ExecuteReader()

        While myReader.Read
            Dim userFullName As String = myReader.GetValue(0) + " " + myReader.GetValue(1)
            Return userFullName
        End While

        'Default value
        Return "GetFullName() Error"
        myConn.Close()
    End Function

    Public Function GetCardNum() As String
        'Connect to the db, select the card info matching their email address and password
        myConn = New SqlConnection()
        myConn.ConnectionString = connectionString
        myConn.Open()
        'MessageBox.Show("Open Successful")
        getCardCmd = New SqlCommand()
        getCardCmd.CommandText = "SELECT cardInfo FROM Customers WHERE email = @email AND password = @pass;"
        getCardCmd.Parameters.Add("@email", SqlDbType.VarChar, 50, "email")
        getCardCmd.Parameters("@email").Value = Login.TextBox1.Text
        getCardCmd.Parameters.Add("@pass", SqlDbType.VarChar, 50, "password")
        getCardCmd.Parameters("@pass").Value = Login.TextBox2.Text
        getCardCmd.Connection = myConn
        myAdapter.SelectCommand = getCardCmd
        myReader = getCardCmd.ExecuteReader()

        While myReader.Read
            Dim cardInfo As String = myReader.GetValue(0)
            Return cardInfo
        End While

        'Default value
        Return "GetCardNum() Error"
        myConn.Close()
    End Function

    Public Function GetDTData(dt As String, ByVal recordnum As Integer, ByVal fieldnum As Integer)
        'Connect to the db, select everything from specified data table, return dataset with data in it
        dataSet.Reset()
        Dim table As String = dt
        myConn = New SqlConnection()
        myConn.ConnectionString = connectionString
        myConn.Open()
        'MessageBox.Show("Open Successful")
        getDataCmd = New SqlCommand()
        getDataCmd.CommandText = "SELECT * FROM " + table + ";"
        getDataCmd.Connection = myConn
        myAdapter.SelectCommand = getDataCmd
        myAdapter.Fill(dataSet, table)
        Return dataSet.Tables(table).Rows(recordnum).Item(fieldnum)
        myConn.Close()
    End Function

    Public Function GetItemName(dt As String, ByVal recordnum As Integer, ByVal fieldnum As Integer)
        'Connect to the db, select everything from specified data table, return dataset with data in it
        dataSet.Reset()
        Dim table As String = dt
        myConn = New SqlConnection()
        myConn.ConnectionString = connectionString
        myConn.Open()
        'MessageBox.Show("Open Successful")
        getDataCmd = New SqlCommand()
        getDataCmd.CommandText = "SELECT name FROM " + table + ";"
        getDataCmd.Connection = myConn
        myAdapter.SelectCommand = getDataCmd
        myAdapter.Fill(dataSet, table)
        Return dataSet.Tables(table).Rows(recordnum).Item(fieldnum)
        myConn.Close()
    End Function

    Public Function GetItemPrice(dt As String, ByVal recordnum As Integer, ByVal fieldnum As Integer)
        'Connect to the db, select everything from specified data table, return dataset with data in it
        dataSet.Reset()
        Dim table As String = dt
        myConn = New SqlConnection()
        myConn.ConnectionString = connectionString
        myConn.Open()
        'MessageBox.Show("Open Successful")
        getDataCmd = New SqlCommand()
        getDataCmd.CommandText = "SELECT price FROM " + table + ";"
        getDataCmd.Connection = myConn
        myAdapter.SelectCommand = getDataCmd
        myAdapter.Fill(dataSet, table)
        Return dataSet.Tables(table).Rows(recordnum).Item(fieldnum)
        myConn.Close()
    End Function

    Public Function GetItemManufacturer(dt As String, ByVal recordnum As Integer, ByVal fieldnum As Integer)
        'Connect to the db, select everything from specified data table, return dataset with data in it
        dataSet.Reset()
        Dim table As String = dt
        myConn = New SqlConnection()
        myConn.ConnectionString = connectionString
        myConn.Open()
        'MessageBox.Show("Open Successful")
        getDataCmd = New SqlCommand()
        getDataCmd.CommandText = "SELECT manufacturer FROM " + table + ";"
        getDataCmd.Connection = myConn
        myAdapter.SelectCommand = getDataCmd
        myAdapter.Fill(dataSet, table)
        Return dataSet.Tables(table).Rows(recordnum).Item(fieldnum)
        myConn.Close()
    End Function

    Public Function GetItemCore(dt As String, ByVal recordnum As Integer, ByVal fieldnum As Integer)
        'Connect to the db, select everything from specified data table, return dataset with data in it
        dataSet.Reset()
        Dim table As String = dt
        myConn = New SqlConnection()
        myConn.ConnectionString = connectionString
        myConn.Open()
        'MessageBox.Show("Open Successful")
        getDataCmd = New SqlCommand()
        getDataCmd.CommandText = "SELECT core FROM " + table + ";"
        getDataCmd.Connection = myConn
        myAdapter.SelectCommand = getDataCmd
        myAdapter.Fill(dataSet, table)
        Return dataSet.Tables(table).Rows(recordnum).Item(fieldnum)
        myConn.Close()
    End Function

    Public Function GetItemCPUSpeed(dt As String, ByVal recordnum As Integer, ByVal fieldnum As Integer)
        'Connect to the db, select everything from specified data table, return dataset with data in it
        dataSet.Reset()
        Dim table As String = dt
        myConn = New SqlConnection()
        myConn.ConnectionString = connectionString
        myConn.Open()
        'MessageBox.Show("Open Successful")
        getDataCmd = New SqlCommand()
        getDataCmd.CommandText = "SELECT [cpu speed] speed FROM " + table + ";"
        getDataCmd.Connection = myConn
        myAdapter.SelectCommand = getDataCmd
        myAdapter.Fill(dataSet, table)
        Return dataSet.Tables(table).Rows(recordnum).Item(fieldnum)
        myConn.Close()
    End Function

    Public Function GetItemSocket(dt As String, ByVal recordnum As Integer, ByVal fieldnum As Integer)
        'Connect to the db, select everything from specified data table, return dataset with data in it
        dataSet.Reset()
        Dim table As String = dt
        myConn = New SqlConnection()
        myConn.ConnectionString = connectionString
        myConn.Open()
        'MessageBox.Show("Open Successful")
        getDataCmd = New SqlCommand()
        getDataCmd.CommandText = "SELECT [cpu socket] FROM " + table + ";"
        getDataCmd.Connection = myConn
        myAdapter.SelectCommand = getDataCmd
        myAdapter.Fill(dataSet, table)
        Return dataSet.Tables(table).Rows(recordnum).Item(fieldnum)
        myConn.Close()
    End Function

    Public Function GetItemInterface(dt As String, ByVal recordnum As Integer, ByVal fieldnum As Integer)
        'Connect to the db, select everything from specified data table, return dataset with data in it
        dataSet.Reset()
        Dim table As String = dt
        myConn = New SqlConnection()
        myConn.ConnectionString = connectionString
        myConn.Open()
        'MessageBox.Show("Open Successful")
        getDataCmd = New SqlCommand()
        getDataCmd.CommandText = "SELECT interface FROM " + table + ";"
        getDataCmd.Connection = myConn
        myAdapter.SelectCommand = getDataCmd
        myAdapter.Fill(dataSet, table)
        Return dataSet.Tables(table).Rows(recordnum).Item(fieldnum)
        myConn.Close()
    End Function

    Public Function GetItemVideoMemory(dt As String, ByVal recordnum As Integer, ByVal fieldnum As Integer)
        'Connect to the db, select everything from specified data table, return dataset with data in it
        dataSet.Reset()
        Dim table As String = dt
        myConn = New SqlConnection()
        myConn.ConnectionString = connectionString
        myConn.Open()
        'MessageBox.Show("Open Successful")
        getDataCmd = New SqlCommand()
        getDataCmd.CommandText = "SELECT [video memory capacity] FROM " + table + ";"
        getDataCmd.Connection = myConn
        myAdapter.SelectCommand = getDataCmd
        myAdapter.Fill(dataSet, table)
        Return dataSet.Tables(table).Rows(recordnum).Item(fieldnum)
        myConn.Close()
    End Function

    Public Function GetItemMemory(dt As String, ByVal recordnum As Integer, ByVal fieldnum As Integer)
        'Connect to the db, select everything from specified data table, return dataset with data in it
        dataSet.Reset()
        Dim table As String = dt
        myConn = New SqlConnection()
        myConn.ConnectionString = connectionString
        myConn.Open()
        'MessageBox.Show("Open Successful")
        getDataCmd = New SqlCommand()
        getDataCmd.CommandText = "SELECT memory FROM " + table + ";"
        getDataCmd.Connection = myConn
        myAdapter.SelectCommand = getDataCmd
        myAdapter.Fill(dataSet, table)
        Return dataSet.Tables(table).Rows(recordnum).Item(fieldnum)
        myConn.Close()
    End Function

    Public Function GetItemSpeed(dt As String, ByVal recordnum As Integer, ByVal fieldnum As Integer)
        'Connect to the db, select everything from specified data table, return dataset with data in it
        dataSet.Reset()
        Dim table As String = dt
        myConn = New SqlConnection()
        myConn.ConnectionString = connectionString
        myConn.Open()
        'MessageBox.Show("Open Successful")
        getDataCmd = New SqlCommand()
        getDataCmd.CommandText = "SELECT memory FROM " + table + ";"
        getDataCmd.Connection = myConn
        myAdapter.SelectCommand = getDataCmd
        myAdapter.Fill(dataSet, table)
        Return dataSet.Tables(table).Rows(recordnum).Item(fieldnum)
        myConn.Close()
    End Function

    Public Function GetItemDescription(dt As String, ByVal recordnum As Integer, ByVal fieldnum As Integer)
        'Connect to the db, select everything from specified data table, return dataset with data in it
        dataSet.Reset()
        Dim table As String = dt
        myConn = New SqlConnection()
        myConn.ConnectionString = connectionString
        myConn.Open()
        'MessageBox.Show("Open Successful")
        getDataCmd = New SqlCommand()
        getDataCmd.CommandText = "SELECT description FROM " + table + ";"
        getDataCmd.Connection = myConn
        myAdapter.SelectCommand = getDataCmd
        myAdapter.Fill(dataSet, table)
        Return dataSet.Tables(table).Rows(recordnum).Item(fieldnum)
        myConn.Close()
    End Function

    Public Function AddToCart(table As String, name As String, price As String)
        'Connect to the db, add the item to orders table
        myConn = New SqlConnection()
        mydt = New DataTable()
        myConn.ConnectionString = connectionString
        myConn.Open()
        'MessageBox.Show("Open Successful")
        addOrdersCmd = New SqlCommand()
        Dim tableName As String = table + ".name"
        Dim tablePrice As String = table + ".price"
        addOrdersCmd.CommandText = "INSERT INTO Orders(name, price) SELECT " + tableName + "," + tablePrice + " FROM " + table + " WHERE " + tableName + " = '" + name + "' AND " + tablePrice + " = '" + price + "';"
        'addOrdersCmd.Parameters.Add("@name", SqlDbType.VarChar, 50, "name")
        'addOrdersCmd.Parameters.Add("@price", SqlDbType.VarChar, 50, "price")
        'addOrdersCmd.Parameters("@name").Value = name
        'addOrdersCmd.Parameters("@price").Value = price
        addOrdersCmd.Connection = myConn
        myAdapter.UpdateCommand = addOrdersCmd

        'if the item is successfully added to orders, returns true. if not, returns false
        Dim result As Boolean
        If (addOrdersCmd.ExecuteNonQuery().Equals(1)) Then
            myAdapter.Update(mydt)
            'MessageBox.Show("Returned True")
            result = True
        Else
            'MessageBox.Show("Returned False")
            result = False
        End If

        Return result

        myConn.Close()
    End Function

    Public Function DeleteOrdersTable()
        'Connect to the db, delete all rows in the Orders table
        myConn = New SqlConnection()
        mydt = New DataTable()
        myConn.ConnectionString = connectionString
        myConn.Open()
        'MessageBox.Show("Open Successful")
        deleteOrdersCmd = New SqlCommand()
        deleteOrdersCmd.CommandText = "DELETE FROM Orders;"
        deleteOrdersCmd.Connection = myConn
        myAdapter.UpdateCommand = deleteOrdersCmd

        'if the table is clear, returns true. if not, returns false
        Dim result As Boolean
        If (deleteOrdersCmd.ExecuteNonQuery().Equals(0)) Then
            result = False
        Else
            myAdapter.Update(mydt)
            MessageBox.Show("Cart Clear")
            result = True
        End If

        Return result

        myConn.Close()
    End Function

    Public Function GetColumnCount()
        'Connect to the db, return number of items in a column
        myConn = New SqlConnection()
        mydt = New DataTable()
        myConn.ConnectionString = connectionString
        myConn.Open()
        'MessageBox.Show("Open Successful")
        getCountCmd = New SqlCommand()
        getCountCmd.CommandText = "SELECT COUNT(id) FROM Orders;"
        getCountCmd.Connection = myConn
        myAdapter.SelectCommand = getCountCmd

        'get the number of items in the id column, return the number
        Dim count As Integer = getCountCmd.ExecuteScalar()
        Return count

        myConn.Close()
    End Function

    Public Function ResetSeed()
        'Connect to the db, reset ident seed of id field to 1
        myConn = New SqlConnection()
        mydt = New DataTable()
        myConn.ConnectionString = connectionString
        myConn.Open()
        'MessageBox.Show("Open Successful")
        resetCmd = New SqlCommand()
        resetCmd.CommandText = "DBCC CHECKIDENT (Orders, RESEED, 0);"
        resetCmd.Connection = myConn
        myAdapter.SelectCommand = resetCmd

        'If reset successful, return true. if not, return false
        Dim result As Boolean
        If (resetCmd.ExecuteNonQuery().Equals(0)) Then
            'MessageBox.Show("False")
            result = False
        Else
            myAdapter.Update(mydt)
            'MessageBox.Show("True")
            result = True
        End If

        Return result

        myConn.Close()
    End Function

    Public Sub BindToDGV(dgv As DataGridView, table As String)
        'Bind dataset to datagridview1
        Using con As New SqlConnection(connectionString)
            Using cmd As New SqlCommand("SELECT * FROM " + table + ";", con)
                cmd.CommandType = CommandType.Text
                Using sda As New SqlDataAdapter(cmd)
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        dgv.DataSource = dt
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Public Function GetTotalPrice()
        'Connect to the db, return total price of items in the price column
        myConn = New SqlConnection()
        mydt = New DataTable()
        myConn.ConnectionString = connectionString
        myConn.Open()
        'MessageBox.Show("Open Successful")
        getTotalCmd = New SqlCommand()
        getTotalCmd.CommandText = "SELECT COALESCE(SUM(price),0) FROM Orders;"
        getTotalCmd.Connection = myConn
        myAdapter.SelectCommand = getTotalCmd

        'get the sum of values in the price column, return the number
        Dim sum = getTotalCmd.ExecuteScalar()
        Return sum

        myConn.Close()
    End Function

    Public Function SendDataToTransactions(id As Integer, name As String, card As String, totalItems As Integer, totalPrice As Double)
        'Connect to the db, send the order data to transactions table
        myConn = New SqlConnection()
        mydt = New DataTable()
        myConn.ConnectionString = connectionString
        myConn.Open()
        'MessageBox.Show("Open Successful")
        sendCmd = New SqlCommand()
        sendCmd.CommandText = "INSERT INTO Transactions(customerID, customerName, cardNumber, numberOfItemsSold, totalPrice) Values (@id, @name, @card, @items, @price);"
        sendCmd.Parameters.Add("@id", SqlDbType.Int, 100, "customerID")
        sendCmd.Parameters.Add("@name", SqlDbType.VarChar, 100, "customerName")
        sendCmd.Parameters.Add("@card", SqlDbType.VarChar, 50, "cardNumber")
        sendCmd.Parameters.Add("@items", SqlDbType.VarChar, 500, "numberOfItemsSold")
        sendCmd.Parameters.Add("@price", SqlDbType.VarChar, 500, "totalPrice")
        sendCmd.Parameters("@id").Value = id
        sendCmd.Parameters("@name").Value = name
        sendCmd.Parameters("@card").Value = card
        sendCmd.Parameters("@items").Value = totalItems
        sendCmd.Parameters("@price").Value = totalPrice
        sendCmd.Connection = myConn
        myAdapter.UpdateCommand = sendCmd

        'if data is successfully sent, returns true. if not, returns false
        Dim result As Boolean
        If (sendCmd.ExecuteNonQuery().Equals(1)) Then
            myAdapter.Update(mydt)
            result = True
        Else
            result = False
        End If

        Return result

        myConn.Close()
    End Function
End Class