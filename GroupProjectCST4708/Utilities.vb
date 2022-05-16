Imports System.Data.SqlClient

Public Class Utilities
    Dim myConn As SqlConnection
    Dim loginCmd As SqlCommand
    Dim registerCmd As SqlCommand
    Dim getIDCmd As SqlCommand
    Dim getNameCmd As SqlCommand
    Dim getCardCmd As SqlCommand
    Dim getCPUCmd As SqlCommand
    Dim getGPUCmd As SqlCommand
    Dim getRAMCmd As SqlCommand
    Dim myAdapter As New SqlDataAdapter
    Dim myReader As SqlDataReader
    Dim mydt As DataTable
    Dim cpuList As ArrayList
    Dim gpuList As ArrayList
    Dim ramList As ArrayList

    Public Function authorizeLogin(email As String, pass As String) As String
        'Connect to the db, select the email and password, compare it to the input from user, return true if matches, false if not
        myConn = New SqlConnection()
        myConn.ConnectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\palad\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\CST4708_GroupProject.mdf';Integrated Security=True;Connect Timeout=30"
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
                If email.Equals("adrian.santos@mail.citytech.cuny.edu") And pass.Equals("password1") Then
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

    Public Function registerCustomer(first As String, last As String, email As String, pass As String, card As String)
        'Connect to the db, add all values passed to this function into the db, return data
        myConn = New SqlConnection()
        mydt = New DataTable()
        myConn.ConnectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\palad\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\CST4708_GroupProject.mdf';Integrated Security=True;Connect Timeout=30"
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

        'if registration is successful, returns succcess. if not, returns unsuccessful
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

    Public Function getID()
        'Connect to the db, select the user id# matching their email address and password
        myConn = New SqlConnection()
        myConn.ConnectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\palad\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\CST4708_GroupProject.mdf';Integrated Security=True;Connect Timeout=30"
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
        Return False
        myConn.Close()
    End Function

    Public Function getFullName()
        'Connect to the db, select the user first and last name matching their email address and password
        myConn = New SqlConnection()
        myConn.ConnectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\palad\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\CST4708_GroupProject.mdf';Integrated Security=True;Connect Timeout=30"
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
        Return False
        myConn.Close()
    End Function

    Public Function getCardNum()
        'Connect to the db, select the card info matching their email address and password
        myConn = New SqlConnection()
        myConn.ConnectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\palad\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\CST4708_GroupProject.mdf';Integrated Security=True;Connect Timeout=30"
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
        Return False
        myConn.Close()
    End Function

    Public Function getCPU()
        'Connect to the db, select everything where the itemType = CPU, put the info in an array list for later
        myConn = New SqlConnection()
        myConn.ConnectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\palad\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\CST4708_GroupProject.mdf';Integrated Security=True;Connect Timeout=30"
        myConn.Open()
        'MessageBox.Show("Open Successful")
        getCPUCmd = New SqlCommand()
        getCPUCmd.CommandText = "SELECT * FROM cpu_info;"
        getCPUCmd.Connection = myConn
        myAdapter.SelectCommand = getCPUCmd
        myReader = getCPUCmd.ExecuteReader()


        While myReader.Read
            Dim cardInfo As String = myReader.GetValue(0)
            Return cardInfo
        End While

        'Default value
        Return False
        myConn.Close()
    End Function

End Class
